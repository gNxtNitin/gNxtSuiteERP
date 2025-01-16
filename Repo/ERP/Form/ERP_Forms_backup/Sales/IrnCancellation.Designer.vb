Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmIRNCancellation
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
	Public WithEvents txtCancelDate As System.Windows.Forms.TextBox
	Public WithEvents txtCancelReason As System.Windows.Forms.TextBox
	Public WithEvents txtCancelRemark As System.Windows.Forms.TextBox
	Public WithEvents txtIRNNo As System.Windows.Forms.TextBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmIRNCancellation))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtCancelReason = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optSaleReturn = New System.Windows.Forms.RadioButton()
        Me.optInvoice = New System.Windows.Forms.RadioButton()
        Me.optCreditNote = New System.Windows.Forms.RadioButton()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtCancelDate = New System.Windows.Forms.TextBox()
        Me.txtCancelRemark = New System.Windows.Forms.TextBox()
        Me.txtIRNNo = New System.Windows.Forms.TextBox()
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
        Me.GroupBox1.SuspendLayout()
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
        Me.txtCancelReason.Location = New System.Drawing.Point(126, 76)
        Me.txtCancelReason.MaxLength = 0
        Me.txtCancelReason.Name = "txtCancelReason"
        Me.txtCancelReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCancelReason.Size = New System.Drawing.Size(253, 20)
        Me.txtCancelReason.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtCancelReason, "1 : Duplicate, 2: Order Cancelled, 3 : Data Entry Mistake, 4: Other")
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(68, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(52, 13)
        Me.Label19.TabIndex = 15
        Me.Label19.Text = "IRN  No :"
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
        Me.cmdSave.TabIndex = 5
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
        Me.cmdClose.TabIndex = 6
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.GroupBox1)
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
        Me.FraFront.Size = New System.Drawing.Size(387, 296)
        Me.FraFront.TabIndex = 10
        Me.FraFront.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.optSaleReturn)
        Me.GroupBox1.Controls.Add(Me.optInvoice)
        Me.GroupBox1.Controls.Add(Me.optCreditNote)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(384, 49)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Selection"
        '
        'optSaleReturn
        '
        Me.optSaleReturn.AutoSize = True
        Me.optSaleReturn.BackColor = System.Drawing.SystemColors.Control
        Me.optSaleReturn.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSaleReturn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSaleReturn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSaleReturn.Location = New System.Drawing.Point(253, 20)
        Me.optSaleReturn.Name = "optSaleReturn"
        Me.optSaleReturn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSaleReturn.Size = New System.Drawing.Size(82, 17)
        Me.optSaleReturn.TabIndex = 9
        Me.optSaleReturn.Text = "Sale Return"
        Me.optSaleReturn.UseVisualStyleBackColor = False
        '
        'optInvoice
        '
        Me.optInvoice.AutoSize = True
        Me.optInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.optInvoice.Checked = True
        Me.optInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.optInvoice.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optInvoice.Location = New System.Drawing.Point(26, 20)
        Me.optInvoice.Name = "optInvoice"
        Me.optInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optInvoice.Size = New System.Drawing.Size(61, 17)
        Me.optInvoice.TabIndex = 8
        Me.optInvoice.TabStop = True
        Me.optInvoice.Text = "Invoice"
        Me.optInvoice.UseVisualStyleBackColor = False
        '
        'optCreditNote
        '
        Me.optCreditNote.AutoSize = True
        Me.optCreditNote.BackColor = System.Drawing.SystemColors.Control
        Me.optCreditNote.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCreditNote.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCreditNote.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCreditNote.Location = New System.Drawing.Point(123, 20)
        Me.optCreditNote.Name = "optCreditNote"
        Me.optCreditNote.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCreditNote.Size = New System.Drawing.Size(84, 17)
        Me.optCreditNote.TabIndex = 7
        Me.optCreditNote.Text = "Credit Note"
        Me.optCreditNote.UseVisualStyleBackColor = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AcceptsReturn = True
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerName.Enabled = False
        Me.txtCustomerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerName.Location = New System.Drawing.Point(8, 121)
        Me.txtCustomerName.MaxLength = 0
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerName.Size = New System.Drawing.Size(371, 20)
        Me.txtCustomerName.TabIndex = 19
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtCancelDate)
        Me.Frame1.Controls.Add(Me.txtCancelReason)
        Me.Frame1.Controls.Add(Me.txtCancelRemark)
        Me.Frame1.Controls.Add(Me.txtIRNNo)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.Label19)
        Me.Frame1.Controls.Add(Me.Label29)
        Me.Frame1.Controls.Add(Me.Label30)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 139)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(387, 157)
        Me.Frame1.TabIndex = 12
        Me.Frame1.TabStop = False
        '
        'txtCancelDate
        '
        Me.txtCancelDate.AcceptsReturn = True
        Me.txtCancelDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCancelDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCancelDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCancelDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCancelDate.Location = New System.Drawing.Point(126, 120)
        Me.txtCancelDate.MaxLength = 0
        Me.txtCancelDate.Name = "txtCancelDate"
        Me.txtCancelDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCancelDate.Size = New System.Drawing.Size(253, 20)
        Me.txtCancelDate.TabIndex = 20
        '
        'txtCancelRemark
        '
        Me.txtCancelRemark.AcceptsReturn = True
        Me.txtCancelRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtCancelRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCancelRemark.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCancelRemark.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCancelRemark.Location = New System.Drawing.Point(126, 98)
        Me.txtCancelRemark.MaxLength = 0
        Me.txtCancelRemark.Name = "txtCancelRemark"
        Me.txtCancelRemark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCancelRemark.Size = New System.Drawing.Size(253, 20)
        Me.txtCancelRemark.TabIndex = 4
        '
        'txtIRNNo
        '
        Me.txtIRNNo.AcceptsReturn = True
        Me.txtIRNNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtIRNNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIRNNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIRNNo.Enabled = False
        Me.txtIRNNo.ForeColor = System.Drawing.Color.Blue
        Me.txtIRNNo.Location = New System.Drawing.Point(126, 12)
        Me.txtIRNNo.MaxLength = 0
        Me.txtIRNNo.Multiline = True
        Me.txtIRNNo.Name = "txtIRNNo"
        Me.txtIRNNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIRNNo.Size = New System.Drawing.Size(253, 61)
        Me.txtIRNNo.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(48, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Cancel Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(34, 78)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(86, 13)
        Me.Label29.TabIndex = 14
        Me.Label29.Text = "Cancel Reason :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(34, 100)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(86, 13)
        Me.Label30.TabIndex = 13
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
        Me.txtBillNo.Location = New System.Drawing.Point(158, 74)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(137, 20)
        Me.txtBillNo.TabIndex = 1
        '
        'TxtBillDate
        '
        Me.TxtBillDate.AllowPromptAsInput = False
        Me.TxtBillDate.Enabled = False
        Me.TxtBillDate.Location = New System.Drawing.Point(158, 96)
        Me.TxtBillDate.Mask = "##/##/####"
        Me.TxtBillDate.Name = "TxtBillDate"
        Me.TxtBillDate.Size = New System.Drawing.Size(95, 20)
        Me.TxtBillDate.TabIndex = 17
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
        Me.lblMkey.TabIndex = 24
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
        Me.lblBookType.TabIndex = 23
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
        Me.lblBookCode.TabIndex = 22
        Me.lblBookCode.Text = "lblBookCode"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(79, 100)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(74, 13)
        Me.Label20.TabIndex = 18
        Me.Label20.Text = "Invoice Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(24, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(129, 13)
        Me.Label1.TabIndex = 11
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
        Me.Frame3.Location = New System.Drawing.Point(0, 285)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(387, 51)
        Me.Frame3.TabIndex = 7
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 7
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
        Me.lblType.TabIndex = 16
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
        Me.lblSODates.TabIndex = 9
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
        Me.lblSONos.TabIndex = 8
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'FrmIRNCancellation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(388, 337)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmIRNCancellation"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Tax Invoice & IRN Cancellation"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents optSaleReturn As RadioButton
    Public WithEvents optInvoice As RadioButton
    Public WithEvents optCreditNote As RadioButton
#End Region
End Class