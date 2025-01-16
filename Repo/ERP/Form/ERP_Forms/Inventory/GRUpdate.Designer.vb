Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmGRUpdate
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
    Public WithEvents txtBillNoTo As System.Windows.Forms.TextBox
    Public WithEvents txtBillNoPrefixTo As System.Windows.Forms.TextBox
    Public WithEvents txtGRAmount As System.Windows.Forms.TextBox
    Public WithEvents txtTransporterBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtVehicle As System.Windows.Forms.TextBox
    Public WithEvents txtCarriers As System.Windows.Forms.TextBox
    Public WithEvents TxtGRNo As System.Windows.Forms.TextBox
    Public WithEvents txtTransporterBillDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents TxtGRDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtBillNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblInvHeading As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblType As System.Windows.Forms.Label
    Public WithEvents lblSODates As System.Windows.Forms.Label
    Public WithEvents lblSONos As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGRUpdate))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.txtBillNoTo = New System.Windows.Forms.TextBox()
        Me.txtBillNoPrefixTo = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtGRAmount = New System.Windows.Forms.TextBox()
        Me.txtTransporterBillNo = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.txtCarriers = New System.Windows.Forms.TextBox()
        Me.TxtGRNo = New System.Windows.Forms.TextBox()
        Me.txtTransporterBillDate = New System.Windows.Forms.MaskedTextBox()
        Me.TxtGRDate = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtBillNoPrefix = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblInvHeading = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.txtBillNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtBillNoSuffixTo = New System.Windows.Forms.TextBox()
        Me.FraFront.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(17, 130)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "GR Amount :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label5, "AWB/RRP No.")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(9, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Trans Bill No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label4, "AWB/RRP No.")
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(8, 20)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(46, 13)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "GR No :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label19, "AWB/RRP No.")
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(4, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 8
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(316, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 9
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.txtBillNoSuffixTo)
        Me.FraFront.Controls.Add(Me.txtBillNoSuffix)
        Me.FraFront.Controls.Add(Me.txtBillNoTo)
        Me.FraFront.Controls.Add(Me.txtBillNoPrefixTo)
        Me.FraFront.Controls.Add(Me.Frame1)
        Me.FraFront.Controls.Add(Me.txtBillNoPrefix)
        Me.FraFront.Controls.Add(Me.txtBillNo)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.lblInvHeading)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(387, 245)
        Me.FraFront.TabIndex = 13
        Me.FraFront.TabStop = False
        '
        'txtBillNoTo
        '
        Me.txtBillNoTo.AcceptsReturn = True
        Me.txtBillNoTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoTo.Enabled = False
        Me.txtBillNoTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNoTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoTo.Location = New System.Drawing.Point(217, 68)
        Me.txtBillNoTo.MaxLength = 0
        Me.txtBillNoTo.Name = "txtBillNoTo"
        Me.txtBillNoTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoTo.Size = New System.Drawing.Size(73, 22)
        Me.txtBillNoTo.TabIndex = 24
        Me.txtBillNoTo.Visible = False
        '
        'txtBillNoPrefixTo
        '
        Me.txtBillNoPrefixTo.AcceptsReturn = True
        Me.txtBillNoPrefixTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoPrefixTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoPrefixTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoPrefixTo.Enabled = False
        Me.txtBillNoPrefixTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNoPrefixTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoPrefixTo.Location = New System.Drawing.Point(148, 68)
        Me.txtBillNoPrefixTo.MaxLength = 0
        Me.txtBillNoPrefixTo.Name = "txtBillNoPrefixTo"
        Me.txtBillNoPrefixTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoPrefixTo.Size = New System.Drawing.Size(68, 22)
        Me.txtBillNoPrefixTo.TabIndex = 23
        Me.txtBillNoPrefixTo.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtGRAmount)
        Me.Frame1.Controls.Add(Me.txtTransporterBillNo)
        Me.Frame1.Controls.Add(Me.txtVehicle)
        Me.Frame1.Controls.Add(Me.txtCarriers)
        Me.Frame1.Controls.Add(Me.TxtGRNo)
        Me.Frame1.Controls.Add(Me.txtTransporterBillDate)
        Me.Frame1.Controls.Add(Me.TxtGRDate)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.Label19)
        Me.Frame1.Controls.Add(Me.Label20)
        Me.Frame1.Controls.Add(Me.Label29)
        Me.Frame1.Controls.Add(Me.Label30)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 88)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(387, 157)
        Me.Frame1.TabIndex = 16
        Me.Frame1.TabStop = False
        '
        'txtGRAmount
        '
        Me.txtGRAmount.AcceptsReturn = True
        Me.txtGRAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtGRAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGRAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGRAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtGRAmount.Location = New System.Drawing.Point(91, 126)
        Me.txtGRAmount.MaxLength = 0
        Me.txtGRAmount.Name = "txtGRAmount"
        Me.txtGRAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRAmount.Size = New System.Drawing.Size(113, 22)
        Me.txtGRAmount.TabIndex = 27
        '
        'txtTransporterBillNo
        '
        Me.txtTransporterBillNo.AcceptsReturn = True
        Me.txtTransporterBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransporterBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransporterBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransporterBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtTransporterBillNo.Location = New System.Drawing.Point(92, 98)
        Me.txtTransporterBillNo.MaxLength = 0
        Me.txtTransporterBillNo.Name = "txtTransporterBillNo"
        Me.txtTransporterBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransporterBillNo.Size = New System.Drawing.Size(113, 22)
        Me.txtTransporterBillNo.TabIndex = 6
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVehicle.Location = New System.Drawing.Point(92, 44)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(285, 22)
        Me.txtVehicle.TabIndex = 4
        '
        'txtCarriers
        '
        Me.txtCarriers.AcceptsReturn = True
        Me.txtCarriers.BackColor = System.Drawing.SystemColors.Window
        Me.txtCarriers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarriers.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCarriers.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarriers.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCarriers.Location = New System.Drawing.Point(92, 70)
        Me.txtCarriers.MaxLength = 0
        Me.txtCarriers.Name = "txtCarriers"
        Me.txtCarriers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCarriers.Size = New System.Drawing.Size(285, 22)
        Me.txtCarriers.TabIndex = 5
        '
        'TxtGRNo
        '
        Me.TxtGRNo.AcceptsReturn = True
        Me.TxtGRNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGRNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRNo.ForeColor = System.Drawing.Color.Blue
        Me.TxtGRNo.Location = New System.Drawing.Point(92, 16)
        Me.TxtGRNo.MaxLength = 0
        Me.TxtGRNo.Name = "TxtGRNo"
        Me.TxtGRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGRNo.Size = New System.Drawing.Size(113, 22)
        Me.TxtGRNo.TabIndex = 2
        '
        'txtTransporterBillDate
        '
        Me.txtTransporterBillDate.AllowPromptAsInput = False
        Me.txtTransporterBillDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterBillDate.Location = New System.Drawing.Point(273, 98)
        Me.txtTransporterBillDate.Mask = "##/##/####"
        Me.txtTransporterBillDate.Name = "txtTransporterBillDate"
        Me.txtTransporterBillDate.Size = New System.Drawing.Size(81, 22)
        Me.txtTransporterBillDate.TabIndex = 7
        '
        'TxtGRDate
        '
        Me.TxtGRDate.AllowPromptAsInput = False
        Me.TxtGRDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRDate.Location = New System.Drawing.Point(273, 16)
        Me.TxtGRDate.Mask = "##/##/####"
        Me.TxtGRDate.Name = "TxtGRDate"
        Me.TxtGRDate.Size = New System.Drawing.Size(81, 22)
        Me.TxtGRDate.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(231, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(209, 20)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(55, 13)
        Me.Label20.TabIndex = 19
        Me.Label20.Text = "GR Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(8, 46)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(48, 13)
        Me.Label29.TabIndex = 18
        Me.Label29.Text = "Vehicle :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(8, 72)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(52, 13)
        Me.Label30.TabIndex = 17
        Me.Label30.Text = "Carriers :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBillNoPrefix
        '
        Me.txtBillNoPrefix.AcceptsReturn = True
        Me.txtBillNoPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoPrefix.Enabled = False
        Me.txtBillNoPrefix.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNoPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoPrefix.Location = New System.Drawing.Point(148, 38)
        Me.txtBillNoPrefix.MaxLength = 0
        Me.txtBillNoPrefix.Name = "txtBillNoPrefix"
        Me.txtBillNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoPrefix.Size = New System.Drawing.Size(68, 22)
        Me.txtBillNoPrefix.TabIndex = 0
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Enabled = False
        Me.txtBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(217, 38)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(73, 22)
        Me.txtBillNo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Enabled = False
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(-32, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(171, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Invoice No To :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label2.Visible = False
        '
        'lblInvHeading
        '
        Me.lblInvHeading.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvHeading.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInvHeading.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvHeading.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvHeading.Location = New System.Drawing.Point(98, 72)
        Me.lblInvHeading.Name = "lblInvHeading"
        Me.lblInvHeading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvHeading.Size = New System.Drawing.Size(59, 15)
        Me.lblInvHeading.TabIndex = 15
        Me.lblInvHeading.Text = "Label6"
        Me.lblInvHeading.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(-32, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(171, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Invoice No From :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.lblMKey)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.lblType)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 234)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(387, 51)
        Me.Frame3.TabIndex = 10
        Me.Frame3.TabStop = False
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(173, 19)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(49, 13)
        Me.lblMKey.TabIndex = 27
        Me.lblMKey.Text = "lblMKey"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 10
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Location = New System.Drawing.Point(118, 22)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.Size = New System.Drawing.Size(44, 13)
        Me.lblType.TabIndex = 26
        Me.lblType.Text = "lblType"
        '
        'lblSODates
        '
        Me.lblSODates.BackColor = System.Drawing.SystemColors.Control
        Me.lblSODates.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSODates.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSODates.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSODates.Location = New System.Drawing.Point(326, 32)
        Me.lblSODates.Name = "lblSODates"
        Me.lblSODates.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSODates.Size = New System.Drawing.Size(17, 9)
        Me.lblSODates.TabIndex = 12
        Me.lblSODates.Text = "lblSODates"
        Me.lblSODates.Visible = False
        '
        'lblSONos
        '
        Me.lblSONos.BackColor = System.Drawing.SystemColors.Control
        Me.lblSONos.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSONos.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSONos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSONos.Location = New System.Drawing.Point(320, 14)
        Me.lblSONos.Name = "lblSONos"
        Me.lblSONos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSONos.Size = New System.Drawing.Size(23, 9)
        Me.lblSONos.TabIndex = 11
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'txtBillNoSuffix
        '
        Me.txtBillNoSuffix.AcceptsReturn = True
        Me.txtBillNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoSuffix.Enabled = False
        Me.txtBillNoSuffix.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNoSuffix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoSuffix.Location = New System.Drawing.Point(292, 38)
        Me.txtBillNoSuffix.MaxLength = 0
        Me.txtBillNoSuffix.Name = "txtBillNoSuffix"
        Me.txtBillNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoSuffix.Size = New System.Drawing.Size(68, 22)
        Me.txtBillNoSuffix.TabIndex = 26
        '
        'txtBillNoSuffixTo
        '
        Me.txtBillNoSuffixTo.AcceptsReturn = True
        Me.txtBillNoSuffixTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNoSuffixTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNoSuffixTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNoSuffixTo.Enabled = False
        Me.txtBillNoSuffixTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNoSuffixTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNoSuffixTo.Location = New System.Drawing.Point(292, 68)
        Me.txtBillNoSuffixTo.MaxLength = 0
        Me.txtBillNoSuffixTo.Name = "txtBillNoSuffixTo"
        Me.txtBillNoSuffixTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoSuffixTo.Size = New System.Drawing.Size(68, 22)
        Me.txtBillNoSuffixTo.TabIndex = 27
        Me.txtBillNoSuffixTo.Visible = False
        '
        'FrmGRUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(388, 286)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGRUpdate"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Transporter GR Update"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents lblMKey As Label
    Public WithEvents txtBillNoSuffixTo As TextBox
    Public WithEvents txtBillNoSuffix As TextBox
#End Region
End Class