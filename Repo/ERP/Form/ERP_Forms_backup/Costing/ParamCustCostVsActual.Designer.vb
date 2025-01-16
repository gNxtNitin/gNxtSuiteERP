Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamCustCostVsActual
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
	Public WithEvents chkAllRM As System.Windows.Forms.CheckBox
	Public WithEvents cmdsearchRM As System.Windows.Forms.Button
	Public WithEvents txtRM As System.Windows.Forms.TextBox
	Public WithEvents txtProduct As System.Windows.Forms.TextBox
	Public WithEvents cmdsearchProduct As System.Windows.Forms.Button
	Public WithEvents chkAllProduct As System.Windows.Forms.CheckBox
	Public WithEvents chkAllCustomer As System.Windows.Forms.CheckBox
	Public WithEvents cmdsearchCustomer As System.Windows.Forms.Button
	Public WithEvents txtCustomer As System.Windows.Forms.TextBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents LblDateFrom As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamCustCostVsActual))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearchRM = New System.Windows.Forms.Button()
        Me.txtRM = New System.Windows.Forms.TextBox()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.cmdsearchProduct = New System.Windows.Forms.Button()
        Me.cmdsearchCustomer = New System.Windows.Forms.Button()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAllRM = New System.Windows.Forms.CheckBox()
        Me.chkAllProduct = New System.Windows.Forms.CheckBox()
        Me.chkAllCustomer = New System.Windows.Forms.CheckBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblDateFrom = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.FraAccount.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdsearchRM
        '
        Me.cmdsearchRM.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchRM.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchRM.Enabled = False
        Me.cmdsearchRM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchRM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchRM.Image = CType(resources.GetObject("cmdsearchRM.Image"), System.Drawing.Image)
        Me.cmdsearchRM.Location = New System.Drawing.Point(571, 55)
        Me.cmdsearchRM.Name = "cmdsearchRM"
        Me.cmdsearchRM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchRM.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearchRM.TabIndex = 19
        Me.cmdsearchRM.TabStop = False
        Me.cmdsearchRM.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchRM, "Search")
        Me.cmdsearchRM.UseVisualStyleBackColor = False
        '
        'txtRM
        '
        Me.txtRM.AcceptsReturn = True
        Me.txtRM.BackColor = System.Drawing.SystemColors.Window
        Me.txtRM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRM.Enabled = False
        Me.txtRM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRM.Location = New System.Drawing.Point(234, 55)
        Me.txtRM.MaxLength = 0
        Me.txtRM.Name = "txtRM"
        Me.txtRM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRM.Size = New System.Drawing.Size(335, 20)
        Me.txtRM.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.txtRM, "Press F1 For Help")
        '
        'txtProduct
        '
        Me.txtProduct.AcceptsReturn = True
        Me.txtProduct.BackColor = System.Drawing.SystemColors.Window
        Me.txtProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProduct.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProduct.Enabled = False
        Me.txtProduct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProduct.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProduct.Location = New System.Drawing.Point(234, 33)
        Me.txtProduct.MaxLength = 0
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProduct.Size = New System.Drawing.Size(335, 20)
        Me.txtProduct.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.txtProduct, "Press F1 For Help")
        '
        'cmdsearchProduct
        '
        Me.cmdsearchProduct.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchProduct.Enabled = False
        Me.cmdsearchProduct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchProduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchProduct.Image = CType(resources.GetObject("cmdsearchProduct.Image"), System.Drawing.Image)
        Me.cmdsearchProduct.Location = New System.Drawing.Point(571, 33)
        Me.cmdsearchProduct.Name = "cmdsearchProduct"
        Me.cmdsearchProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchProduct.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearchProduct.TabIndex = 15
        Me.cmdsearchProduct.TabStop = False
        Me.cmdsearchProduct.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchProduct, "Search")
        Me.cmdsearchProduct.UseVisualStyleBackColor = False
        '
        'cmdsearchCustomer
        '
        Me.cmdsearchCustomer.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchCustomer.Enabled = False
        Me.cmdsearchCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchCustomer.Image = CType(resources.GetObject("cmdsearchCustomer.Image"), System.Drawing.Image)
        Me.cmdsearchCustomer.Location = New System.Drawing.Point(571, 12)
        Me.cmdsearchCustomer.Name = "cmdsearchCustomer"
        Me.cmdsearchCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchCustomer.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearchCustomer.TabIndex = 2
        Me.cmdsearchCustomer.TabStop = False
        Me.cmdsearchCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchCustomer, "Search")
        Me.cmdsearchCustomer.UseVisualStyleBackColor = False
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Enabled = False
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomer.Location = New System.Drawing.Point(234, 12)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(335, 20)
        Me.txtCustomer.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtCustomer, "Press F1 For Help")
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(123, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 7
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(63, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 6
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(184, 9)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 8
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 5
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkAllRM)
        Me.FraAccount.Controls.Add(Me.cmdsearchRM)
        Me.FraAccount.Controls.Add(Me.txtRM)
        Me.FraAccount.Controls.Add(Me.txtProduct)
        Me.FraAccount.Controls.Add(Me.cmdsearchProduct)
        Me.FraAccount.Controls.Add(Me.chkAllProduct)
        Me.FraAccount.Controls.Add(Me.chkAllCustomer)
        Me.FraAccount.Controls.Add(Me.cmdsearchCustomer)
        Me.FraAccount.Controls.Add(Me.txtCustomer)
        Me.FraAccount.Controls.Add(Me.txtDateFrom)
        Me.FraAccount.Controls.Add(Me.Label3)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.LblDateFrom)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, -4)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(749, 78)
        Me.FraAccount.TabIndex = 9
        Me.FraAccount.TabStop = False
        '
        'chkAllRM
        '
        Me.chkAllRM.AutoSize = True
        Me.chkAllRM.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllRM.Checked = True
        Me.chkAllRM.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllRM.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllRM.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllRM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllRM.Location = New System.Drawing.Point(600, 59)
        Me.chkAllRM.Name = "chkAllRM"
        Me.chkAllRM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllRM.Size = New System.Drawing.Size(43, 16)
        Me.chkAllRM.TabIndex = 20
        Me.chkAllRM.Text = "ALL"
        Me.chkAllRM.UseVisualStyleBackColor = False
        '
        'chkAllProduct
        '
        Me.chkAllProduct.AutoSize = True
        Me.chkAllProduct.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllProduct.Checked = True
        Me.chkAllProduct.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllProduct.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllProduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllProduct.Location = New System.Drawing.Point(600, 37)
        Me.chkAllProduct.Name = "chkAllProduct"
        Me.chkAllProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllProduct.Size = New System.Drawing.Size(43, 16)
        Me.chkAllProduct.TabIndex = 14
        Me.chkAllProduct.Text = "ALL"
        Me.chkAllProduct.UseVisualStyleBackColor = False
        '
        'chkAllCustomer
        '
        Me.chkAllCustomer.AutoSize = True
        Me.chkAllCustomer.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCustomer.Checked = True
        Me.chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCustomer.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCustomer.Location = New System.Drawing.Point(600, 16)
        Me.chkAllCustomer.Name = "chkAllCustomer"
        Me.chkAllCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCustomer.Size = New System.Drawing.Size(43, 16)
        Me.chkAllCustomer.TabIndex = 3
        Me.chkAllCustomer.Text = "ALL"
        Me.chkAllCustomer.UseVisualStyleBackColor = False
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(80, 13)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(80, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(89, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(141, 12)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Raw Material / BOP / Paint :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(181, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(49, 12)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Product :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblDateFrom
        '
        Me.LblDateFrom.AutoSize = True
        Me.LblDateFrom.BackColor = System.Drawing.SystemColors.Control
        Me.LblDateFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDateFrom.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDateFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDateFrom.Location = New System.Drawing.Point(1, 16)
        Me.LblDateFrom.Name = "LblDateFrom"
        Me.LblDateFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDateFrom.Size = New System.Drawing.Size(64, 12)
        Me.LblDateFrom.TabIndex = 13
        Me.LblDateFrom.Text = "As on Date :"
        Me.LblDateFrom.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 7.47!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(172, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(58, 12)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Customer :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 70)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(749, 346)
        Me.Frame4.TabIndex = 10
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(749, 333)
        Me.SprdMain.TabIndex = 4
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 5
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(502, 410)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 49)
        Me.FraMovement.TabIndex = 11
        Me.FraMovement.TabStop = False
        '
        'frmParamCustCostVsActual
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 459)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(5, 25)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamCustCostVsActual"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Customer Costing Vs Actual Register"
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
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