Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAtrnBulk
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
	Public WithEvents txtVType As System.Windows.Forms.TextBox
	Public WithEvents cmdsearch As System.Windows.Forms.Button
	Public WithEvents TxtVDate As System.Windows.Forms.TextBox
	Public WithEvents txtPartyName As System.Windows.Forms.TextBox
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents sprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblSaleBillNo As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents lblBookBalDC As System.Windows.Forms.Label
	Public WithEvents LblDate_Issue_Receive As System.Windows.Forms.Label
	Public WithEvents lblAccount As System.Windows.Forms.Label
	Public WithEvents lblBookBalAmt As System.Windows.Forms.Label
	Public WithEvents LblTotal As System.Windows.Forms.Label
	Public WithEvents FraTrans As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents fraGridView As System.Windows.Forms.GroupBox
	Public WithEvents cmdAdd As System.Windows.Forms.Button
	Public WithEvents cmdBillDetails As System.Windows.Forms.Button
	Public WithEvents txtRows As System.Windows.Forms.TextBox
	Public WithEvents cmdInsertRow As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdSave As System.Windows.Forms.Button
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents lblPaymentDetail As System.Windows.Forms.Label
	Public WithEvents lblBillDetails As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents lblBookType As System.Windows.Forms.Label
	Public WithEvents lblSR As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAtrnBulk))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdBillDetails = New System.Windows.Forms.Button()
        Me.txtRows = New System.Windows.Forms.TextBox()
        Me.cmdInsertRow = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.FraTrans = New System.Windows.Forms.GroupBox()
        Me.txtVType = New System.Windows.Forms.TextBox()
        Me.TxtVDate = New System.Windows.Forms.TextBox()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.sprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblSaleBillNo = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblBookBalDC = New System.Windows.Forms.Label()
        Me.LblDate_Issue_Receive = New System.Windows.Forms.Label()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.lblBookBalAmt = New System.Windows.Forms.Label()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.fraGridView = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblPaymentDetail = New System.Windows.Forms.Label()
        Me.lblBillDetails = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblSR = New System.Windows.Forms.Label()
        Me.FraTrans.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraGridView.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(532, 34)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(23, 21)
        Me.cmdsearch.TabIndex = 1
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdd.Location = New System.Drawing.Point(292, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 27
        Me.cmdAdd.Text = "&Add New"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Save Voucher")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdBillDetails
        '
        Me.cmdBillDetails.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBillDetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBillDetails.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBillDetails.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBillDetails.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdBillDetails.Location = New System.Drawing.Point(359, 10)
        Me.cmdBillDetails.Name = "cmdBillDetails"
        Me.cmdBillDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillDetails.Size = New System.Drawing.Size(67, 37)
        Me.cmdBillDetails.TabIndex = 26
        Me.cmdBillDetails.Text = "&Calculate Bill Details"
        Me.cmdBillDetails.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBillDetails, "Print Voucher")
        Me.cmdBillDetails.UseVisualStyleBackColor = False
        '
        'txtRows
        '
        Me.txtRows.AcceptsReturn = True
        Me.txtRows.BackColor = System.Drawing.SystemColors.Window
        Me.txtRows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRows.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRows.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRows.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRows.Location = New System.Drawing.Point(74, 12)
        Me.txtRows.MaxLength = 0
        Me.txtRows.Name = "txtRows"
        Me.txtRows.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRows.Size = New System.Drawing.Size(71, 20)
        Me.txtRows.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.txtRows, "Press F1 For Help")
        '
        'cmdInsertRow
        '
        Me.cmdInsertRow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdInsertRow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdInsertRow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdInsertRow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdInsertRow.Image = CType(resources.GetObject("cmdInsertRow.Image"), System.Drawing.Image)
        Me.cmdInsertRow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdInsertRow.Location = New System.Drawing.Point(146, 10)
        Me.cmdInsertRow.Name = "cmdInsertRow"
        Me.cmdInsertRow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdInsertRow.Size = New System.Drawing.Size(67, 21)
        Me.cmdInsertRow.TabIndex = 21
        Me.cmdInsertRow.Text = "Row Insert"
        Me.cmdInsertRow.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdInsertRow, "Show Record")
        Me.cmdInsertRow.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(671, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 6
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(605, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 5
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview Voucher")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(538, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print Voucher")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSave.Location = New System.Drawing.Point(471, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 3
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Voucher")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'FraTrans
        '
        Me.FraTrans.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrans.Controls.Add(Me.txtVType)
        Me.FraTrans.Controls.Add(Me.cmdsearch)
        Me.FraTrans.Controls.Add(Me.TxtVDate)
        Me.FraTrans.Controls.Add(Me.txtPartyName)
        Me.FraTrans.Controls.Add(Me.Report1)
        Me.FraTrans.Controls.Add(Me.sprdMain)
        Me.FraTrans.Controls.Add(Me.Label1)
        Me.FraTrans.Controls.Add(Me.lblSaleBillNo)
        Me.FraTrans.Controls.Add(Me.Label3)
        Me.FraTrans.Controls.Add(Me.lblBookBalDC)
        Me.FraTrans.Controls.Add(Me.LblDate_Issue_Receive)
        Me.FraTrans.Controls.Add(Me.lblAccount)
        Me.FraTrans.Controls.Add(Me.lblBookBalAmt)
        Me.FraTrans.Controls.Add(Me.LblTotal)
        Me.FraTrans.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrans.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrans.Location = New System.Drawing.Point(0, -6)
        Me.FraTrans.Name = "FraTrans"
        Me.FraTrans.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrans.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrans.Size = New System.Drawing.Size(749, 416)
        Me.FraTrans.TabIndex = 7
        Me.FraTrans.TabStop = False
        '
        'txtVType
        '
        Me.txtVType.AcceptsReturn = True
        Me.txtVType.BackColor = System.Drawing.SystemColors.Window
        Me.txtVType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVType.Enabled = False
        Me.txtVType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVType.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtVType.Location = New System.Drawing.Point(648, 34)
        Me.txtVType.MaxLength = 0
        Me.txtVType.Name = "txtVType"
        Me.txtVType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVType.Size = New System.Drawing.Size(70, 20)
        Me.txtVType.TabIndex = 24
        '
        'TxtVDate
        '
        Me.TxtVDate.AcceptsReturn = True
        Me.TxtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.TxtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVDate.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.TxtVDate.Location = New System.Drawing.Point(99, 12)
        Me.TxtVDate.MaxLength = 0
        Me.TxtVDate.Name = "TxtVDate"
        Me.TxtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtVDate.Size = New System.Drawing.Size(70, 20)
        Me.TxtVDate.TabIndex = 2
        '
        'txtPartyName
        '
        Me.txtPartyName.AcceptsReturn = True
        Me.txtPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartyName.Location = New System.Drawing.Point(99, 34)
        Me.txtPartyName.MaxLength = 0
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyName.Size = New System.Drawing.Size(431, 20)
        Me.txtPartyName.TabIndex = 0
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(418, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 25
        '
        'sprdMain
        '
        Me.sprdMain.DataSource = Nothing
        Me.sprdMain.Location = New System.Drawing.Point(2, 64)
        Me.sprdMain.Name = "sprdMain"
        Me.sprdMain.OcxState = CType(resources.GetObject("sprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdMain.Size = New System.Drawing.Size(743, 349)
        Me.sprdMain.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(597, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(47, 14)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "VType :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSaleBillNo
        '
        Me.lblSaleBillNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblSaleBillNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSaleBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaleBillNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSaleBillNo.Location = New System.Drawing.Point(364, 272)
        Me.lblSaleBillNo.Name = "lblSaleBillNo"
        Me.lblSaleBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSaleBillNo.Size = New System.Drawing.Size(33, 9)
        Me.lblSaleBillNo.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(518, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(78, 14)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Current Bal. :"
        '
        'lblBookBalDC
        '
        Me.lblBookBalDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookBalDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBookBalDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookBalDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookBalDC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBookBalDC.Location = New System.Drawing.Point(716, 12)
        Me.lblBookBalDC.Name = "lblBookBalDC"
        Me.lblBookBalDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookBalDC.Size = New System.Drawing.Size(29, 20)
        Me.lblBookBalDC.TabIndex = 12
        Me.lblBookBalDC.Text = "lblBookBalDC"
        '
        'LblDate_Issue_Receive
        '
        Me.LblDate_Issue_Receive.AutoSize = True
        Me.LblDate_Issue_Receive.BackColor = System.Drawing.SystemColors.Control
        Me.LblDate_Issue_Receive.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDate_Issue_Receive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate_Issue_Receive.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDate_Issue_Receive.Location = New System.Drawing.Point(6, 13)
        Me.LblDate_Issue_Receive.Name = "LblDate_Issue_Receive"
        Me.LblDate_Issue_Receive.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDate_Issue_Receive.Size = New System.Drawing.Size(86, 14)
        Me.LblDate_Issue_Receive.TabIndex = 9
        Me.LblDate_Issue_Receive.Text = "Voucher Date :"
        Me.LblDate_Issue_Receive.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAccount.Location = New System.Drawing.Point(24, 37)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccount.Size = New System.Drawing.Size(74, 14)
        Me.lblAccount.TabIndex = 8
        Me.lblAccount.Text = "Bank Name :"
        Me.lblAccount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBookBalAmt
        '
        Me.lblBookBalAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookBalAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBookBalAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookBalAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookBalAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBookBalAmt.Location = New System.Drawing.Point(597, 12)
        Me.lblBookBalAmt.Name = "lblBookBalAmt"
        Me.lblBookBalAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookBalAmt.Size = New System.Drawing.Size(117, 20)
        Me.lblBookBalAmt.TabIndex = 11
        Me.lblBookBalAmt.Text = "lblBookBalAmt"
        Me.lblBookBalAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblTotal
        '
        Me.LblTotal.AutoSize = True
        Me.LblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.LblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.ForeColor = System.Drawing.Color.Black
        Me.LblTotal.Location = New System.Drawing.Point(11, 223)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotal.Size = New System.Drawing.Size(39, 14)
        Me.LblTotal.TabIndex = 13
        Me.LblTotal.Text = "Total :"
        '
        'fraGridView
        '
        Me.fraGridView.BackColor = System.Drawing.SystemColors.Control
        Me.fraGridView.Controls.Add(Me.SprdView)
        Me.fraGridView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraGridView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraGridView.Location = New System.Drawing.Point(0, -6)
        Me.fraGridView.Name = "fraGridView"
        Me.fraGridView.Padding = New System.Windows.Forms.Padding(0)
        Me.fraGridView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraGridView.Size = New System.Drawing.Size(749, 417)
        Me.fraGridView.TabIndex = 14
        Me.fraGridView.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(745, 407)
        Me.SprdView.TabIndex = 15
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.cmdBillDetails)
        Me.Frame3.Controls.Add(Me.txtRows)
        Me.Frame3.Controls.Add(Me.cmdInsertRow)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.Label4)
        Me.Frame3.Controls.Add(Me.lblPaymentDetail)
        Me.Frame3.Controls.Add(Me.lblBillDetails)
        Me.Frame3.Controls.Add(Me.Label2)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblSR)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 406)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(751, 51)
        Me.Frame3.TabIndex = 16
        Me.Frame3.TabStop = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(10, 32)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(197, 15)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Press line no for delete any row."
        '
        'lblPaymentDetail
        '
        Me.lblPaymentDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaymentDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentDetail.Location = New System.Drawing.Point(246, 38)
        Me.lblPaymentDetail.Name = "lblPaymentDetail"
        Me.lblPaymentDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentDetail.Size = New System.Drawing.Size(29, 7)
        Me.lblPaymentDetail.TabIndex = 29
        Me.lblPaymentDetail.Text = "Label4"
        '
        'lblBillDetails
        '
        Me.lblBillDetails.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillDetails.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillDetails.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDetails.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillDetails.Location = New System.Drawing.Point(238, 18)
        Me.lblBillDetails.Name = "lblBillDetails"
        Me.lblBillDetails.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillDetails.Size = New System.Drawing.Size(25, 13)
        Me.lblBillDetails.TabIndex = 28
        Me.lblBillDetails.Text = "False"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Add Rows :"
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(252, 16)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(65, 19)
        Me.lblBookType.TabIndex = 19
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblSR
        '
        Me.lblSR.BackColor = System.Drawing.SystemColors.Control
        Me.lblSR.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSR.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSR.Location = New System.Drawing.Point(720, 20)
        Me.lblSR.Name = "lblSR"
        Me.lblSR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSR.Size = New System.Drawing.Size(25, 15)
        Me.lblSR.TabIndex = 17
        '
        'frmAtrnBulk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(750, 458)
        Me.Controls.Add(Me.FraTrans)
        Me.Controls.Add(Me.fraGridView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(-233, 7)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAtrnBulk"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Bank Payment - Bulk"
        Me.FraTrans.ResumeLayout(False)
        Me.FraTrans.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraGridView.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
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