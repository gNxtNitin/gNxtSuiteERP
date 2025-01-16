Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmGRNUpdate
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
    Public WithEvents txtShortageQty As System.Windows.Forms.TextBox
    Public WithEvents txtRejectedQty As System.Windows.Forms.TextBox
    Public WithEvents txtReceivedQty As System.Windows.Forms.TextBox
    Public WithEvents txtAcceptedQty As System.Windows.Forms.TextBox
    Public WithEvents TxtGRNNo As System.Windows.Forms.TextBox
    Public WithEvents TxtGRNDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtBillNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblType As System.Windows.Forms.Label
    Public WithEvents lblSODates As System.Windows.Forms.Label
    Public WithEvents lblSONos As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGRNUpdate))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.txtBillNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtBillQty = New System.Windows.Forms.TextBox()
        Me.lblBillQty = New System.Windows.Forms.Label()
        Me.txtBillAmount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtInvoiceDate = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkClear = New System.Windows.Forms.CheckBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtShortageQty = New System.Windows.Forms.TextBox()
        Me.txtRejectedQty = New System.Windows.Forms.TextBox()
        Me.txtReceivedQty = New System.Windows.Forms.TextBox()
        Me.txtAcceptedQty = New System.Windows.Forms.TextBox()
        Me.TxtGRNNo = New System.Windows.Forms.TextBox()
        Me.TxtGRNDate = New System.Windows.Forms.MaskedTextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtBillNoPrefix = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.Label5.Location = New System.Drawing.Point(245, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(80, 13)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "Shortage Qty :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label5, "AWB/RRP No.")
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(58, 20)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(54, 13)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "GRN No :"
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
        Me.cmdSave.TabIndex = 0
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
        Me.cmdClose.Location = New System.Drawing.Point(438, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 9
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(55, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Remarks :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label3, "AWB/RRP No.")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(34, 74)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Rejected Qty :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.SprdMain)
        Me.FraFront.Controls.Add(Me.txtBillNoSuffix)
        Me.FraFront.Controls.Add(Me.txtBillQty)
        Me.FraFront.Controls.Add(Me.lblBillQty)
        Me.FraFront.Controls.Add(Me.txtBillAmount)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.txtInvoiceDate)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Frame1)
        Me.FraFront.Controls.Add(Me.txtBillNoPrefix)
        Me.FraFront.Controls.Add(Me.txtBillNo)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(623, 408)
        Me.FraFront.TabIndex = 13
        Me.FraFront.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 222)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(618, 180)
        Me.SprdMain.TabIndex = 0
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
        Me.txtBillNoSuffix.Location = New System.Drawing.Point(299, 14)
        Me.txtBillNoSuffix.MaxLength = 0
        Me.txtBillNoSuffix.Name = "txtBillNoSuffix"
        Me.txtBillNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoSuffix.Size = New System.Drawing.Size(80, 22)
        Me.txtBillNoSuffix.TabIndex = 27
        '
        'txtBillQty
        '
        Me.txtBillQty.AcceptsReturn = True
        Me.txtBillQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillQty.Enabled = False
        Me.txtBillQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillQty.ForeColor = System.Drawing.Color.Blue
        Me.txtBillQty.Location = New System.Drawing.Point(288, 65)
        Me.txtBillQty.MaxLength = 0
        Me.txtBillQty.Name = "txtBillQty"
        Me.txtBillQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillQty.Size = New System.Drawing.Size(72, 22)
        Me.txtBillQty.TabIndex = 25
        '
        'lblBillQty
        '
        Me.lblBillQty.AutoSize = True
        Me.lblBillQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillQty.Location = New System.Drawing.Point(234, 69)
        Me.lblBillQty.Name = "lblBillQty"
        Me.lblBillQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillQty.Size = New System.Drawing.Size(50, 13)
        Me.lblBillQty.TabIndex = 26
        Me.lblBillQty.Text = "Bill Qty :"
        Me.lblBillQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBillAmount
        '
        Me.txtBillAmount.AcceptsReturn = True
        Me.txtBillAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillAmount.Enabled = False
        Me.txtBillAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtBillAmount.Location = New System.Drawing.Point(116, 65)
        Me.txtBillAmount.MaxLength = 0
        Me.txtBillAmount.Name = "txtBillAmount"
        Me.txtBillAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillAmount.Size = New System.Drawing.Size(113, 22)
        Me.txtBillAmount.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(40, 69)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(72, 13)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Bill Amount :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.AllowPromptAsInput = False
        Me.txtInvoiceDate.Enabled = False
        Me.txtInvoiceDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceDate.Location = New System.Drawing.Point(116, 40)
        Me.txtInvoiceDate.Mask = "##/##/####"
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.Size = New System.Drawing.Size(81, 22)
        Me.txtInvoiceDate.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(36, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Invoice Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkClear)
        Me.Frame1.Controls.Add(Me.txtRemarks)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.txtShortageQty)
        Me.Frame1.Controls.Add(Me.txtRejectedQty)
        Me.Frame1.Controls.Add(Me.txtReceivedQty)
        Me.Frame1.Controls.Add(Me.txtAcceptedQty)
        Me.Frame1.Controls.Add(Me.TxtGRNNo)
        Me.Frame1.Controls.Add(Me.TxtGRNDate)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label19)
        Me.Frame1.Controls.Add(Me.Label20)
        Me.Frame1.Controls.Add(Me.Label29)
        Me.Frame1.Controls.Add(Me.Label30)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 91)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(623, 128)
        Me.Frame1.TabIndex = 16
        Me.Frame1.TabStop = False
        '
        'chkClear
        '
        Me.chkClear.AutoSize = True
        Me.chkClear.BackColor = System.Drawing.SystemColors.Control
        Me.chkClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkClear.Location = New System.Drawing.Point(410, 20)
        Me.chkClear.Name = "chkClear"
        Me.chkClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkClear.Size = New System.Drawing.Size(51, 18)
        Me.chkClear.TabIndex = 31
        Me.chkClear.Text = "Clear"
        Me.chkClear.UseVisualStyleBackColor = False
        Me.chkClear.Visible = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtRemarks.Location = New System.Drawing.Point(116, 97)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(326, 22)
        Me.txtRemarks.TabIndex = 6
        '
        'txtShortageQty
        '
        Me.txtShortageQty.AcceptsReturn = True
        Me.txtShortageQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtShortageQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShortageQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShortageQty.Enabled = False
        Me.txtShortageQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShortageQty.ForeColor = System.Drawing.Color.Blue
        Me.txtShortageQty.Location = New System.Drawing.Point(329, 70)
        Me.txtShortageQty.MaxLength = 0
        Me.txtShortageQty.Name = "txtShortageQty"
        Me.txtShortageQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShortageQty.Size = New System.Drawing.Size(113, 22)
        Me.txtShortageQty.TabIndex = 5
        '
        'txtRejectedQty
        '
        Me.txtRejectedQty.AcceptsReturn = True
        Me.txtRejectedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtRejectedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRejectedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRejectedQty.Enabled = False
        Me.txtRejectedQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRejectedQty.ForeColor = System.Drawing.Color.Blue
        Me.txtRejectedQty.Location = New System.Drawing.Point(116, 70)
        Me.txtRejectedQty.MaxLength = 0
        Me.txtRejectedQty.Name = "txtRejectedQty"
        Me.txtRejectedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRejectedQty.Size = New System.Drawing.Size(113, 22)
        Me.txtRejectedQty.TabIndex = 4
        '
        'txtReceivedQty
        '
        Me.txtReceivedQty.AcceptsReturn = True
        Me.txtReceivedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtReceivedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReceivedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReceivedQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceivedQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReceivedQty.Location = New System.Drawing.Point(116, 43)
        Me.txtReceivedQty.MaxLength = 0
        Me.txtReceivedQty.Name = "txtReceivedQty"
        Me.txtReceivedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReceivedQty.Size = New System.Drawing.Size(113, 22)
        Me.txtReceivedQty.TabIndex = 2
        '
        'txtAcceptedQty
        '
        Me.txtAcceptedQty.AcceptsReturn = True
        Me.txtAcceptedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtAcceptedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcceptedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAcceptedQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcceptedQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAcceptedQty.Location = New System.Drawing.Point(329, 43)
        Me.txtAcceptedQty.MaxLength = 0
        Me.txtAcceptedQty.Name = "txtAcceptedQty"
        Me.txtAcceptedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAcceptedQty.Size = New System.Drawing.Size(113, 22)
        Me.txtAcceptedQty.TabIndex = 3
        '
        'TxtGRNNo
        '
        Me.TxtGRNNo.AcceptsReturn = True
        Me.TxtGRNNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGRNNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGRNNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGRNNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRNNo.ForeColor = System.Drawing.Color.Blue
        Me.TxtGRNNo.Location = New System.Drawing.Point(116, 16)
        Me.TxtGRNNo.MaxLength = 0
        Me.TxtGRNNo.Name = "TxtGRNNo"
        Me.TxtGRNNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGRNNo.Size = New System.Drawing.Size(113, 22)
        Me.TxtGRNNo.TabIndex = 0
        '
        'TxtGRNDate
        '
        Me.TxtGRNDate.AllowPromptAsInput = False
        Me.TxtGRNDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRNDate.Location = New System.Drawing.Point(298, 16)
        Me.TxtGRNDate.Mask = "##/##/####"
        Me.TxtGRNDate.Name = "TxtGRNDate"
        Me.TxtGRNDate.Size = New System.Drawing.Size(81, 22)
        Me.TxtGRNDate.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(234, 20)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(63, 13)
        Me.Label20.TabIndex = 19
        Me.Label20.Text = "GRN Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(32, 46)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(80, 13)
        Me.Label29.TabIndex = 18
        Me.Label29.Text = "Received Qty :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(244, 46)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(81, 13)
        Me.Label30.TabIndex = 17
        Me.Label30.Text = "Accepted Qty :"
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
        Me.txtBillNoPrefix.Location = New System.Drawing.Point(116, 14)
        Me.txtBillNoPrefix.MaxLength = 0
        Me.txtBillNoPrefix.Name = "txtBillNoPrefix"
        Me.txtBillNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNoPrefix.Size = New System.Drawing.Size(80, 22)
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
        Me.txtBillNo.Location = New System.Drawing.Point(198, 14)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(100, 22)
        Me.txtBillNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(7, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(105, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Invoice No :"
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
        Me.Frame3.Location = New System.Drawing.Point(0, 395)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(508, 51)
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
        Me.lblSONos.TabIndex = 1
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'FrmGRNUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(626, 449)
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
        Me.Name = "FrmGRNUpdate"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Customer GRN Update"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents txtInvoiceDate As MaskedTextBox
    Public WithEvents Label2 As Label
    Public WithEvents txtRemarks As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents txtBillAmount As TextBox
    Public WithEvents Label6 As Label
    Public WithEvents lblMKey As Label
    Public WithEvents txtBillQty As TextBox
    Public WithEvents lblBillQty As Label
    Public WithEvents chkClear As CheckBox
    Public WithEvents txtBillNoSuffix As TextBox
#End Region
End Class