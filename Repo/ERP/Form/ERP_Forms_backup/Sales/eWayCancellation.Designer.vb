Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmeWayCancellation
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
	Public WithEvents txtCustomerName As System.Windows.Forms.TextBox
	Public WithEvents txtTransporter As System.Windows.Forms.TextBox
	Public WithEvents txtVehicle As System.Windows.Forms.TextBox
	Public WithEvents txtCancelDate As System.Windows.Forms.TextBox
	Public WithEvents txtCancelReason As System.Windows.Forms.TextBox
	Public WithEvents txtCancelRemark As System.Windows.Forms.TextBox
	Public WithEvents txtEWayBillNo As System.Windows.Forms.TextBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents Label29 As System.Windows.Forms.Label
	Public WithEvents Label30 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents TxtBillDate As System.Windows.Forms.MaskedTextBox
	Public WithEvents lblMkey As System.Windows.Forms.Label
	Public WithEvents lblBookType As System.Windows.Forms.Label
	Public WithEvents lblBookCode As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmeWayCancellation))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtCancelReason = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtTransporter = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.txtCancelDate = New System.Windows.Forms.TextBox()
        Me.txtCancelRemark = New System.Windows.Forms.TextBox()
        Me.txtEWayBillNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.TxtBillDate = New System.Windows.Forms.MaskedTextBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblBookCode = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCancelReason
        '
        Me.txtCancelReason.AcceptsReturn = True
        Me.txtCancelReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtCancelReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCancelReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCancelReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCancelReason.Location = New System.Drawing.Point(126, 84)
        Me.txtCancelReason.MaxLength = 0
        Me.txtCancelReason.Name = "txtCancelReason"
        Me.txtCancelReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCancelReason.Size = New System.Drawing.Size(253, 20)
        Me.txtCancelReason.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtCancelReason, "1 : Duplicate, 2: Order Cancelled, 3 : Data Entry Mistake, 4: Others")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(24, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Transporter Name :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label4, "AWB/RRP No.")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(57, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Vehicle No :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label3, "AWB/RRP No.")
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(45, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(77, 13)
        Me.Label19.TabIndex = 18
        Me.Label19.Text = "eWay Bill  No :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label19, "AWB/RRP No.")
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
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
        Me.FraFront.Controls.Add(Me.txtCustomerName)
        Me.FraFront.Controls.Add(Me.Frame1)
        Me.FraFront.Controls.Add(Me.txtBillNo)
        Me.FraFront.Controls.Add(Me.TxtBillDate)
        Me.FraFront.Controls.Add(Me.lblMkey)
        Me.FraFront.Controls.Add(Me.lblBookType)
        Me.FraFront.Controls.Add(Me.lblBookCode)
        Me.FraFront.Controls.Add(Me.Label20)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(387, 245)
        Me.FraFront.TabIndex = 13
        Me.FraFront.TabStop = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AcceptsReturn = True
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerName.Enabled = False
        Me.txtCustomerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerName.Location = New System.Drawing.Point(8, 60)
        Me.txtCustomerName.MaxLength = 0
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerName.Size = New System.Drawing.Size(371, 20)
        Me.txtCustomerName.TabIndex = 22
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtTransporter)
        Me.Frame1.Controls.Add(Me.txtVehicle)
        Me.Frame1.Controls.Add(Me.txtCancelDate)
        Me.Frame1.Controls.Add(Me.txtCancelReason)
        Me.Frame1.Controls.Add(Me.txtCancelRemark)
        Me.Frame1.Controls.Add(Me.txtEWayBillNo)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.Label19)
        Me.Frame1.Controls.Add(Me.Label29)
        Me.Frame1.Controls.Add(Me.Label30)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 88)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(387, 157)
        Me.Frame1.TabIndex = 15
        Me.Frame1.TabStop = False
        '
        'txtTransporter
        '
        Me.txtTransporter.AcceptsReturn = True
        Me.txtTransporter.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransporter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransporter.Enabled = False
        Me.txtTransporter.ForeColor = System.Drawing.Color.Blue
        Me.txtTransporter.Location = New System.Drawing.Point(126, 60)
        Me.txtTransporter.MaxLength = 0
        Me.txtTransporter.Multiline = True
        Me.txtTransporter.Name = "txtTransporter"
        Me.txtTransporter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransporter.Size = New System.Drawing.Size(253, 21)
        Me.txtTransporter.TabIndex = 4
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.Enabled = False
        Me.txtVehicle.ForeColor = System.Drawing.Color.Blue
        Me.txtVehicle.Location = New System.Drawing.Point(126, 36)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Multiline = True
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(253, 21)
        Me.txtVehicle.TabIndex = 3
        '
        'txtCancelDate
        '
        Me.txtCancelDate.AcceptsReturn = True
        Me.txtCancelDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCancelDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCancelDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCancelDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCancelDate.Location = New System.Drawing.Point(126, 128)
        Me.txtCancelDate.MaxLength = 0
        Me.txtCancelDate.Name = "txtCancelDate"
        Me.txtCancelDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCancelDate.Size = New System.Drawing.Size(253, 20)
        Me.txtCancelDate.TabIndex = 7
        '
        'txtCancelRemark
        '
        Me.txtCancelRemark.AcceptsReturn = True
        Me.txtCancelRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtCancelRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCancelRemark.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCancelRemark.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCancelRemark.Location = New System.Drawing.Point(126, 106)
        Me.txtCancelRemark.MaxLength = 0
        Me.txtCancelRemark.Name = "txtCancelRemark"
        Me.txtCancelRemark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCancelRemark.Size = New System.Drawing.Size(253, 20)
        Me.txtCancelRemark.TabIndex = 6
        '
        'txtEWayBillNo
        '
        Me.txtEWayBillNo.AcceptsReturn = True
        Me.txtEWayBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEWayBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEWayBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEWayBillNo.Enabled = False
        Me.txtEWayBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtEWayBillNo.Location = New System.Drawing.Point(126, 12)
        Me.txtEWayBillNo.MaxLength = 0
        Me.txtEWayBillNo.Multiline = True
        Me.txtEWayBillNo.Name = "txtEWayBillNo"
        Me.txtEWayBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEWayBillNo.Size = New System.Drawing.Size(253, 21)
        Me.txtEWayBillNo.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(50, 130)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Cancel Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(36, 86)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(86, 13)
        Me.Label29.TabIndex = 17
        Me.Label29.Text = "Cancel Reason :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(36, 108)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(86, 13)
        Me.Label30.TabIndex = 16
        Me.Label30.Text = "Cancel Remark :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(150, 12)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(145, 20)
        Me.txtBillNo.TabIndex = 1
        '
        'TxtBillDate
        '
        Me.TxtBillDate.AllowPromptAsInput = False
        Me.TxtBillDate.Enabled = False
        Me.TxtBillDate.Location = New System.Drawing.Point(150, 34)
        Me.TxtBillDate.Mask = "##/##/####"
        Me.TxtBillDate.Name = "TxtBillDate"
        Me.TxtBillDate.Size = New System.Drawing.Size(95, 20)
        Me.TxtBillDate.TabIndex = 20
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(316, 50)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(47, 11)
        Me.lblMkey.TabIndex = 26
        Me.lblMkey.Text = "lblMkey"
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(314, 28)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(45, 9)
        Me.lblBookType.TabIndex = 25
        Me.lblBookType.Text = "lblBookType"
        '
        'lblBookCode
        '
        Me.lblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookCode.Location = New System.Drawing.Point(312, 12)
        Me.lblBookCode.Name = "lblBookCode"
        Me.lblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookCode.Size = New System.Drawing.Size(55, 13)
        Me.lblBookCode.TabIndex = 24
        Me.lblBookCode.Text = "lblBookCode"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(71, 38)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(74, 13)
        Me.Label20.TabIndex = 21
        Me.Label20.Text = "Invoice Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(14, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(131, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Invoice No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.lblType)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 234)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(387, 51)
        Me.Frame3.TabIndex = 10
        Me.Frame3.TabStop = False
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
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Location = New System.Drawing.Point(118, 22)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.Size = New System.Drawing.Size(41, 13)
        Me.lblType.TabIndex = 19
        Me.lblType.Text = "lblType"
        '
        'lblSODates
        '
        Me.lblSODates.BackColor = System.Drawing.SystemColors.Control
        Me.lblSODates.Cursor = System.Windows.Forms.Cursors.Default
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
        Me.lblSONos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSONos.Location = New System.Drawing.Point(320, 14)
        Me.lblSONos.Name = "lblSONos"
        Me.lblSONos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSONos.Size = New System.Drawing.Size(23, 9)
        Me.lblSONos.TabIndex = 11
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'FrmeWayCancellation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(388, 286)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmeWayCancellation"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "eWay Bill Cancellation"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class