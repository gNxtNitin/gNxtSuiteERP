Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmReversalVoucher
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
	Public WithEvents txtNarration As System.Windows.Forms.TextBox
	Public WithEvents txtVDate As System.Windows.Forms.TextBox
	Public WithEvents cboVoucher As System.Windows.Forms.ComboBox
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents txtFYear As System.Windows.Forms.TextBox
	Public WithEvents txtVno As System.Windows.Forms.TextBox
	Public WithEvents lblMKey As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
	Public WithEvents txtPartyName As System.Windows.Forms.TextBox
	Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents lblAccount As System.Windows.Forms.Label
	Public WithEvents fraAccounts As System.Windows.Forms.GroupBox
	Public WithEvents lblVType As System.Windows.Forms.Label
	Public WithEvents lblBookCode As System.Windows.Forms.Label
	Public WithEvents lblNewBookType As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblPaymentDetail As System.Windows.Forms.Label
	Public WithEvents LblNetAmt As System.Windows.Forms.Label
	Public WithEvents LblNet As System.Windows.Forms.Label
	Public WithEvents LblCrAmt As System.Windows.Forms.Label
	Public WithEvents LblCr As System.Windows.Forms.Label
	Public WithEvents LblDrAmt As System.Windows.Forms.Label
	Public WithEvents LblDr As System.Windows.Forms.Label
	Public WithEvents FraTrn As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents CmdClear As System.Windows.Forms.Button
	Public WithEvents lblBookType As System.Windows.Forms.Label
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReversalVoucher))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdClear = New System.Windows.Forms.Button()
        Me.FraTrn = New System.Windows.Forms.GroupBox()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.cboVoucher = New System.Windows.Forms.ComboBox()
        Me.txtFYear = New System.Windows.Forms.TextBox()
        Me.txtVno = New System.Windows.Forms.TextBox()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.fraAccounts = New System.Windows.Forms.GroupBox()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.lblVType = New System.Windows.Forms.Label()
        Me.lblBookCode = New System.Windows.Forms.Label()
        Me.lblNewBookType = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPaymentDetail = New System.Windows.Forms.Label()
        Me.LblNetAmt = New System.Windows.Forms.Label()
        Me.LblNet = New System.Windows.Forms.Label()
        Me.LblCrAmt = New System.Windows.Forms.Label()
        Me.LblCr = New System.Windows.Forms.Label()
        Me.LblDrAmt = New System.Windows.Forms.Label()
        Me.LblDr = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraTrn.SuspendLayout()
        Me.fraTop1.SuspendLayout()
        Me.fraAccounts.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Location = New System.Drawing.Point(490, 52)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(77, 29)
        Me.cmdShow.TabIndex = 5
        Me.cmdShow.TabStop = False
        Me.cmdShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Search")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdClose.Location = New System.Drawing.Point(654, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 9
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdSave.Location = New System.Drawing.Point(342, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(129, 37)
        Me.CmdSave.TabIndex = 8
        Me.CmdSave.Text = "&Generate Reversal"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdClear
        '
        Me.CmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClear.Image = CType(resources.GetObject("CmdClear.Image"), System.Drawing.Image)
        Me.CmdClear.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdClear.Location = New System.Drawing.Point(60, 11)
        Me.CmdClear.Name = "CmdClear"
        Me.CmdClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClear.Size = New System.Drawing.Size(97, 37)
        Me.CmdClear.TabIndex = 0
        Me.CmdClear.Text = "&Add New"
        Me.CmdClear.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClear, "Add New Record")
        Me.CmdClear.UseVisualStyleBackColor = False
        '
        'FraTrn
        '
        Me.FraTrn.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrn.Controls.Add(Me.txtNarration)
        Me.FraTrn.Controls.Add(Me.fraTop1)
        Me.FraTrn.Controls.Add(Me.fraAccounts)
        Me.FraTrn.Controls.Add(Me.lblVType)
        Me.FraTrn.Controls.Add(Me.lblBookCode)
        Me.FraTrn.Controls.Add(Me.lblNewBookType)
        Me.FraTrn.Controls.Add(Me.Label1)
        Me.FraTrn.Controls.Add(Me.lblPaymentDetail)
        Me.FraTrn.Controls.Add(Me.LblNetAmt)
        Me.FraTrn.Controls.Add(Me.LblNet)
        Me.FraTrn.Controls.Add(Me.LblCrAmt)
        Me.FraTrn.Controls.Add(Me.LblCr)
        Me.FraTrn.Controls.Add(Me.LblDrAmt)
        Me.FraTrn.Controls.Add(Me.LblDr)
        Me.FraTrn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrn.Location = New System.Drawing.Point(0, -6)
        Me.FraTrn.Name = "FraTrn"
        Me.FraTrn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrn.Size = New System.Drawing.Size(751, 417)
        Me.FraTrn.TabIndex = 10
        Me.FraTrn.TabStop = False
        '
        'txtNarration
        '
        Me.txtNarration.AcceptsReturn = True
        Me.txtNarration.BackColor = System.Drawing.SystemColors.Window
        Me.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNarration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNarration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNarration.Location = New System.Drawing.Point(70, 354)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Multiline = True
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(327, 59)
        Me.txtNarration.TabIndex = 7
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtVDate)
        Me.fraTop1.Controls.Add(Me.cboVoucher)
        Me.fraTop1.Controls.Add(Me.cmdShow)
        Me.fraTop1.Controls.Add(Me.txtFYear)
        Me.fraTop1.Controls.Add(Me.txtVno)
        Me.fraTop1.Controls.Add(Me.lblMKey)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, 6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(751, 91)
        Me.fraTop1.TabIndex = 11
        Me.fraTop1.TabStop = False
        Me.fraTop1.Text = "Reversal Detail"
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Enabled = False
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtVDate.Location = New System.Drawing.Point(377, 40)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(101, 20)
        Me.txtVDate.TabIndex = 3
        '
        'cboVoucher
        '
        Me.cboVoucher.BackColor = System.Drawing.SystemColors.Window
        Me.cboVoucher.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboVoucher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVoucher.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboVoucher.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cboVoucher.Location = New System.Drawing.Point(140, 16)
        Me.cboVoucher.Name = "cboVoucher"
        Me.cboVoucher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboVoucher.Size = New System.Drawing.Size(338, 22)
        Me.cboVoucher.Sorted = True
        Me.cboVoucher.TabIndex = 1
        Me.cboVoucher.TabStop = False
        '
        'txtFYear
        '
        Me.txtFYear.AcceptsReturn = True
        Me.txtFYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtFYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFYear.ForeColor = System.Drawing.Color.Blue
        Me.txtFYear.Location = New System.Drawing.Point(140, 62)
        Me.txtFYear.MaxLength = 0
        Me.txtFYear.Name = "txtFYear"
        Me.txtFYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFYear.Size = New System.Drawing.Size(159, 20)
        Me.txtFYear.TabIndex = 4
        '
        'txtVno
        '
        Me.txtVno.AcceptsReturn = True
        Me.txtVno.BackColor = System.Drawing.SystemColors.Window
        Me.txtVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVno.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVno.ForeColor = System.Drawing.Color.Blue
        Me.txtVno.Location = New System.Drawing.Point(140, 40)
        Me.txtVno.MaxLength = 0
        Me.txtVno.Name = "txtVno"
        Me.txtVno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVno.Size = New System.Drawing.Size(159, 20)
        Me.txtVno.TabIndex = 2
        '
        'lblMKey
        '
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(610, 20)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(101, 25)
        Me.lblMKey.TabIndex = 28
        Me.lblMKey.Text = "lblMKey"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(336, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(37, 14)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(50, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(75, 14)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Book Name :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(42, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(80, 14)
        Me.Label5.TabIndex = 17
        Me.Label5.Text = "Finacial Year :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(49, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(76, 14)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Voucher No :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraAccounts
        '
        Me.fraAccounts.BackColor = System.Drawing.SystemColors.Control
        Me.fraAccounts.Controls.Add(Me.txtPartyName)
        Me.fraAccounts.Controls.Add(Me.SprdMain)
        Me.fraAccounts.Controls.Add(Me.lblAccount)
        Me.fraAccounts.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAccounts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAccounts.Location = New System.Drawing.Point(0, 94)
        Me.fraAccounts.Name = "fraAccounts"
        Me.fraAccounts.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAccounts.Size = New System.Drawing.Size(751, 259)
        Me.fraAccounts.TabIndex = 13
        Me.fraAccounts.TabStop = False
        Me.fraAccounts.Text = "Voucher Details"
        '
        'txtPartyName
        '
        Me.txtPartyName.AcceptsReturn = True
        Me.txtPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyName.Enabled = False
        Me.txtPartyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartyName.Location = New System.Drawing.Point(183, 10)
        Me.txtPartyName.MaxLength = 0
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyName.Size = New System.Drawing.Size(431, 20)
        Me.txtPartyName.TabIndex = 29
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 34)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(744, 220)
        Me.SprdMain.TabIndex = 6
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAccount.Location = New System.Drawing.Point(108, 13)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccount.Size = New System.Drawing.Size(74, 14)
        Me.lblAccount.TabIndex = 30
        Me.lblAccount.Text = "Bank Name :"
        Me.lblAccount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVType
        '
        Me.lblVType.BackColor = System.Drawing.SystemColors.Control
        Me.lblVType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVType.Location = New System.Drawing.Point(518, 384)
        Me.lblVType.Name = "lblVType"
        Me.lblVType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVType.Size = New System.Drawing.Size(45, 21)
        Me.lblVType.TabIndex = 33
        Me.lblVType.Text = "lblVType"
        '
        'lblBookCode
        '
        Me.lblBookCode.AutoSize = True
        Me.lblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookCode.Location = New System.Drawing.Point(432, 386)
        Me.lblBookCode.Name = "lblBookCode"
        Me.lblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookCode.Size = New System.Drawing.Size(66, 14)
        Me.lblBookCode.TabIndex = 32
        Me.lblBookCode.Text = "lblBookCode"
        Me.lblBookCode.Visible = False
        '
        'lblNewBookType
        '
        Me.lblNewBookType.AutoSize = True
        Me.lblNewBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblNewBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNewBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNewBookType.Location = New System.Drawing.Point(440, 362)
        Me.lblNewBookType.Name = "lblNewBookType"
        Me.lblNewBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNewBookType.Size = New System.Drawing.Size(87, 14)
        Me.lblNewBookType.TabIndex = 31
        Me.lblNewBookType.Text = "lblNewBookType"
        Me.lblNewBookType.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 354)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(65, 15)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Narration :"
        '
        'lblPaymentDetail
        '
        Me.lblPaymentDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaymentDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaymentDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentDetail.Location = New System.Drawing.Point(712, 396)
        Me.lblPaymentDetail.Name = "lblPaymentDetail"
        Me.lblPaymentDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentDetail.Size = New System.Drawing.Size(27, 17)
        Me.lblPaymentDetail.TabIndex = 26
        Me.lblPaymentDetail.Text = "lblPaymentDetail"
        Me.lblPaymentDetail.Visible = False
        '
        'LblNetAmt
        '
        Me.LblNetAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblNetAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNetAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetAmt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LblNetAmt.Location = New System.Drawing.Point(616, 396)
        Me.LblNetAmt.Name = "LblNetAmt"
        Me.LblNetAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNetAmt.Size = New System.Drawing.Size(95, 17)
        Me.LblNetAmt.TabIndex = 25
        Me.LblNetAmt.Text = "LblNetAmt"
        Me.LblNetAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblNet
        '
        Me.LblNet.AutoSize = True
        Me.LblNet.BackColor = System.Drawing.SystemColors.Control
        Me.LblNet.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNet.ForeColor = System.Drawing.Color.Black
        Me.LblNet.Location = New System.Drawing.Point(586, 398)
        Me.LblNet.Name = "LblNet"
        Me.LblNet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNet.Size = New System.Drawing.Size(31, 14)
        Me.LblNet.TabIndex = 24
        Me.LblNet.Text = "Net :"
        '
        'LblCrAmt
        '
        Me.LblCrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblCrAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCrAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCrAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCrAmt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LblCrAmt.Location = New System.Drawing.Point(616, 378)
        Me.LblCrAmt.Name = "LblCrAmt"
        Me.LblCrAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCrAmt.Size = New System.Drawing.Size(95, 17)
        Me.LblCrAmt.TabIndex = 23
        Me.LblCrAmt.Text = "LblCrAmt"
        Me.LblCrAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblCr
        '
        Me.LblCr.AutoSize = True
        Me.LblCr.BackColor = System.Drawing.SystemColors.Control
        Me.LblCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCr.ForeColor = System.Drawing.Color.Black
        Me.LblCr.Location = New System.Drawing.Point(594, 380)
        Me.LblCr.Name = "LblCr"
        Me.LblCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCr.Size = New System.Drawing.Size(26, 14)
        Me.LblCr.TabIndex = 22
        Me.LblCr.Text = "Cr :"
        '
        'LblDrAmt
        '
        Me.LblDrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblDrAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblDrAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDrAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDrAmt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LblDrAmt.Location = New System.Drawing.Point(616, 360)
        Me.LblDrAmt.Name = "LblDrAmt"
        Me.LblDrAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDrAmt.Size = New System.Drawing.Size(95, 17)
        Me.LblDrAmt.TabIndex = 21
        Me.LblDrAmt.Text = "LblDrAmt"
        Me.LblDrAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblDr
        '
        Me.LblDr.AutoSize = True
        Me.LblDr.BackColor = System.Drawing.SystemColors.Control
        Me.LblDr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDr.ForeColor = System.Drawing.Color.Black
        Me.LblDr.Location = New System.Drawing.Point(592, 362)
        Me.LblDr.Name = "LblDr"
        Me.LblDr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDr.Size = New System.Drawing.Size(25, 14)
        Me.LblDr.TabIndex = 20
        Me.LblDr.Text = "Dr :"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 12
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdClear)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 406)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(751, 51)
        Me.FraMovement.TabIndex = 14
        Me.FraMovement.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(8, 22)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(45, 15)
        Me.lblBookType.TabIndex = 16
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(752, 411)
        Me.SprdView.TabIndex = 15
        '
        'frmReversalVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(752, 457)
        Me.Controls.Add(Me.FraTrn)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReversalVoucher"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Reversal Voucher Individual"
        Me.FraTrn.ResumeLayout(False)
        Me.FraTrn.PerformLayout()
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.fraAccounts.ResumeLayout(False)
        Me.fraAccounts.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdView.DataSource = Nothing
	End Sub
#End Region 
End Class