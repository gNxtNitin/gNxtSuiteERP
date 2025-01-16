Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmProvisionVoucher
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
	Public WithEvents txtPopulateVNo As System.Windows.Forms.TextBox
	Public WithEvents txtExpDate As System.Windows.Forms.TextBox
	Public WithEvents txtNarration As System.Windows.Forms.TextBox
	Public WithEvents txtVNoSuffix As System.Windows.Forms.TextBox
	Public WithEvents txtVType As System.Windows.Forms.TextBox
	Public WithEvents TxtVDate As System.Windows.Forms.TextBox
	Public WithEvents txtVno As System.Windows.Forms.TextBox
	Public WithEvents txtVNo1 As System.Windows.Forms.TextBox
	Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label23 As System.Windows.Forms.Label
	Public WithEvents lblModDate As System.Windows.Forms.Label
	Public WithEvents Label48 As System.Windows.Forms.Label
	Public WithEvents lblAddDate As System.Windows.Forms.Label
	Public WithEvents Label45 As System.Windows.Forms.Label
	Public WithEvents lblModUser As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents lblAddUser As System.Windows.Forms.Label
	Public WithEvents Label44 As System.Windows.Forms.Label
	Public WithEvents lblAcBalAmt As System.Windows.Forms.Label
	Public WithEvents lblAcBalDC As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents lblBookType As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents LblDate_Issue_Receive As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents LblTotal As System.Windows.Forms.Label
	Public WithEvents LblDr As System.Windows.Forms.Label
	Public WithEvents LblDrAmt As System.Windows.Forms.Label
	Public WithEvents LblCr As System.Windows.Forms.Label
	Public WithEvents LblCrAmt As System.Windows.Forms.Label
	Public WithEvents LblNet As System.Windows.Forms.Label
	Public WithEvents LblNetAmt As System.Windows.Forms.Label
	Public WithEvents lblPaymentDetail As System.Windows.Forms.Label
	Public WithEvents FraTrans As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents fraGridView As System.Windows.Forms.GroupBox
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdBillDetail As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdDelete As System.Windows.Forms.Button
	Public WithEvents cmdAuthorised As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents cmdSave As System.Windows.Forms.Button
	Public WithEvents cmdModify As System.Windows.Forms.Button
	Public WithEvents cmdAdd As System.Windows.Forms.Button
	Public WithEvents lblSR As System.Windows.Forms.Label
	Public WithEvents lblLoanDetail As System.Windows.Forms.Label
	Public WithEvents lblYM As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProvisionVoucher))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdBillDetail = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdAuthorised = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.FraTrans = New System.Windows.Forms.GroupBox()
        Me.CmdPopFromFile = New System.Windows.Forms.Button()
        Me.txtPopulateVNo = New System.Windows.Forms.TextBox()
        Me.txtExpDate = New System.Windows.Forms.TextBox()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.txtVNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtVType = New System.Windows.Forms.TextBox()
        Me.TxtVDate = New System.Windows.Forms.TextBox()
        Me.txtVno = New System.Windows.Forms.TextBox()
        Me.txtVNo1 = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblAcBalAmt = New System.Windows.Forms.Label()
        Me.lblAcBalDC = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LblDate_Issue_Receive = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.LblDr = New System.Windows.Forms.Label()
        Me.LblDrAmt = New System.Windows.Forms.Label()
        Me.LblCr = New System.Windows.Forms.Label()
        Me.LblCrAmt = New System.Windows.Forms.Label()
        Me.LblNet = New System.Windows.Forms.Label()
        Me.LblNetAmt = New System.Windows.Forms.Label()
        Me.lblPaymentDetail = New System.Windows.Forms.Label()
        Me.fraGridView = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblSR = New System.Windows.Forms.Label()
        Me.lblLoanDetail = New System.Windows.Forms.Label()
        Me.lblYM = New System.Windows.Forms.Label()
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog()
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog()
        Me.FraTrans.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraGridView.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(754, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 19
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdBillDetail
        '
        Me.cmdBillDetail.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBillDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBillDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBillDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBillDetail.Image = CType(resources.GetObject("cmdBillDetail.Image"), System.Drawing.Image)
        Me.cmdBillDetail.Location = New System.Drawing.Point(687, 10)
        Me.cmdBillDetail.Name = "cmdBillDetail"
        Me.cmdBillDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillDetail.Size = New System.Drawing.Size(67, 37)
        Me.cmdBillDetail.TabIndex = 18
        Me.cmdBillDetail.Text = "Bi&ll Detail"
        Me.cmdBillDetail.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBillDetail, "View Transaction Listings")
        Me.cmdBillDetail.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(620, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 17
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Transaction Listings")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(554, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 16
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
        Me.cmdPrint.Location = New System.Drawing.Point(487, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 15
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print Voucher")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(421, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 14
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete Voucher")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdAuthorised
        '
        Me.cmdAuthorised.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAuthorised.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAuthorised.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAuthorised.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAuthorised.Image = CType(resources.GetObject("cmdAuthorised.Image"), System.Drawing.Image)
        Me.cmdAuthorised.Location = New System.Drawing.Point(355, 10)
        Me.cmdAuthorised.Name = "cmdAuthorised"
        Me.cmdAuthorised.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAuthorised.Size = New System.Drawing.Size(67, 37)
        Me.cmdAuthorised.TabIndex = 42
        Me.cmdAuthorised.Text = "A&uthorised"
        Me.cmdAuthorised.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAuthorised, "Save & Print Voucher")
        Me.cmdAuthorised.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(289, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 13
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print Voucher")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(222, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 12
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Voucher")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(155, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 11
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify Voucher")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(88, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 10
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'FraTrans
        '
        Me.FraTrans.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrans.Controls.Add(Me.CmdPopFromFile)
        Me.FraTrans.Controls.Add(Me.txtPopulateVNo)
        Me.FraTrans.Controls.Add(Me.txtExpDate)
        Me.FraTrans.Controls.Add(Me.txtNarration)
        Me.FraTrans.Controls.Add(Me.txtVNoSuffix)
        Me.FraTrans.Controls.Add(Me.txtVType)
        Me.FraTrans.Controls.Add(Me.TxtVDate)
        Me.FraTrans.Controls.Add(Me.txtVno)
        Me.FraTrans.Controls.Add(Me.txtVNo1)
        Me.FraTrans.Controls.Add(Me.chkCancelled)
        Me.FraTrans.Controls.Add(Me.Report1)
        Me.FraTrans.Controls.Add(Me.SprdMain)
        Me.FraTrans.Controls.Add(Me.Label3)
        Me.FraTrans.Controls.Add(Me.Label23)
        Me.FraTrans.Controls.Add(Me.lblModDate)
        Me.FraTrans.Controls.Add(Me.Label48)
        Me.FraTrans.Controls.Add(Me.lblAddDate)
        Me.FraTrans.Controls.Add(Me.Label45)
        Me.FraTrans.Controls.Add(Me.lblModUser)
        Me.FraTrans.Controls.Add(Me.Label15)
        Me.FraTrans.Controls.Add(Me.lblAddUser)
        Me.FraTrans.Controls.Add(Me.Label44)
        Me.FraTrans.Controls.Add(Me.lblAcBalAmt)
        Me.FraTrans.Controls.Add(Me.lblAcBalDC)
        Me.FraTrans.Controls.Add(Me.Label14)
        Me.FraTrans.Controls.Add(Me.lblBookType)
        Me.FraTrans.Controls.Add(Me.Label2)
        Me.FraTrans.Controls.Add(Me.LblDate_Issue_Receive)
        Me.FraTrans.Controls.Add(Me.Label1)
        Me.FraTrans.Controls.Add(Me.LblTotal)
        Me.FraTrans.Controls.Add(Me.LblDr)
        Me.FraTrans.Controls.Add(Me.LblDrAmt)
        Me.FraTrans.Controls.Add(Me.LblCr)
        Me.FraTrans.Controls.Add(Me.LblCrAmt)
        Me.FraTrans.Controls.Add(Me.LblNet)
        Me.FraTrans.Controls.Add(Me.LblNetAmt)
        Me.FraTrans.Controls.Add(Me.lblPaymentDetail)
        Me.FraTrans.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrans.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrans.Location = New System.Drawing.Point(0, -4)
        Me.FraTrans.Name = "FraTrans"
        Me.FraTrans.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrans.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrans.Size = New System.Drawing.Size(904, 572)
        Me.FraTrans.TabIndex = 20
        Me.FraTrans.TabStop = False
        '
        'CmdPopFromFile
        '
        Me.CmdPopFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPopFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopFromFile.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopFromFile.Location = New System.Drawing.Point(761, 12)
        Me.CmdPopFromFile.Name = "CmdPopFromFile"
        Me.CmdPopFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopFromFile.Size = New System.Drawing.Size(134, 23)
        Me.CmdPopFromFile.TabIndex = 152
        Me.CmdPopFromFile.Text = "Populate From File"
        Me.CmdPopFromFile.UseVisualStyleBackColor = False
        '
        'txtPopulateVNo
        '
        Me.txtPopulateVNo.AcceptsReturn = True
        Me.txtPopulateVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPopulateVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPopulateVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPopulateVNo.Enabled = False
        Me.txtPopulateVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPopulateVNo.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtPopulateVNo.Location = New System.Drawing.Point(668, 14)
        Me.txtPopulateVNo.MaxLength = 0
        Me.txtPopulateVNo.Name = "txtPopulateVNo"
        Me.txtPopulateVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPopulateVNo.Size = New System.Drawing.Size(78, 20)
        Me.txtPopulateVNo.TabIndex = 0
        '
        'txtExpDate
        '
        Me.txtExpDate.AcceptsReturn = True
        Me.txtExpDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExpDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpDate.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtExpDate.Location = New System.Drawing.Point(441, 14)
        Me.txtExpDate.MaxLength = 0
        Me.txtExpDate.Name = "txtExpDate"
        Me.txtExpDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpDate.Size = New System.Drawing.Size(70, 20)
        Me.txtExpDate.TabIndex = 6
        '
        'txtNarration
        '
        Me.txtNarration.AcceptsReturn = True
        Me.txtNarration.BackColor = System.Drawing.SystemColors.Window
        Me.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNarration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNarration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNarration.Location = New System.Drawing.Point(4, 488)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Multiline = True
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(413, 75)
        Me.txtNarration.TabIndex = 8
        '
        'txtVNoSuffix
        '
        Me.txtVNoSuffix.AcceptsReturn = True
        Me.txtVNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNoSuffix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNoSuffix.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtVNoSuffix.Location = New System.Drawing.Point(316, 14)
        Me.txtVNoSuffix.MaxLength = 0
        Me.txtVNoSuffix.Name = "txtVNoSuffix"
        Me.txtVNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoSuffix.Size = New System.Drawing.Size(28, 20)
        Me.txtVNoSuffix.TabIndex = 5
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
        Me.txtVType.Location = New System.Drawing.Point(218, 14)
        Me.txtVType.MaxLength = 0
        Me.txtVType.Name = "txtVType"
        Me.txtVType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVType.Size = New System.Drawing.Size(44, 20)
        Me.txtVType.TabIndex = 3
        '
        'TxtVDate
        '
        Me.TxtVDate.AcceptsReturn = True
        Me.TxtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.TxtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVDate.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.TxtVDate.Location = New System.Drawing.Point(99, 14)
        Me.TxtVDate.MaxLength = 0
        Me.TxtVDate.Name = "TxtVDate"
        Me.TxtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtVDate.Size = New System.Drawing.Size(70, 20)
        Me.TxtVDate.TabIndex = 1
        '
        'txtVno
        '
        Me.txtVno.AcceptsReturn = True
        Me.txtVno.BackColor = System.Drawing.SystemColors.Window
        Me.txtVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVno.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVno.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtVno.Location = New System.Drawing.Point(262, 14)
        Me.txtVno.MaxLength = 0
        Me.txtVno.Name = "txtVno"
        Me.txtVno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVno.Size = New System.Drawing.Size(54, 20)
        Me.txtVno.TabIndex = 4
        '
        'txtVNo1
        '
        Me.txtVNo1.AcceptsReturn = True
        Me.txtVNo1.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo1.Enabled = False
        Me.txtVNo1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtVNo1.Location = New System.Drawing.Point(225, 14)
        Me.txtVNo1.MaxLength = 0
        Me.txtVNo1.Name = "txtVNo1"
        Me.txtVNo1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo1.Size = New System.Drawing.Size(36, 20)
        Me.txtVNo1.TabIndex = 2
        Me.txtVNo1.Visible = False
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.Red
        Me.chkCancelled.Location = New System.Drawing.Point(576, 549)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(80, 18)
        Me.chkCancelled.TabIndex = 9
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(98, 148)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 10
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 43)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(902, 424)
        Me.SprdMain.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(543, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(121, 14)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Populate From VNo. :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(349, 17)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(87, 14)
        Me.Label23.TabIndex = 51
        Me.Label23.Text = "Expense Date :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(500, 545)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 50
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(432, 547)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 49
        Me.Label48.Text = "Mod Date:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(500, 527)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 48
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(431, 529)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(64, 15)
        Me.Label45.TabIndex = 47
        Me.Label45.Text = "Add Date :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(500, 507)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 46
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(431, 509)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(64, 15)
        Me.Label15.TabIndex = 45
        Me.Label15.Text = "Mod User:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(500, 489)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 44
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(430, 491)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(65, 15)
        Me.Label44.TabIndex = 43
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAcBalAmt
        '
        Me.lblAcBalAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcBalAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAcBalAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcBalAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcBalAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAcBalAmt.Location = New System.Drawing.Point(772, 489)
        Me.lblAcBalAmt.Name = "lblAcBalAmt"
        Me.lblAcBalAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcBalAmt.Size = New System.Drawing.Size(95, 20)
        Me.lblAcBalAmt.TabIndex = 39
        Me.lblAcBalAmt.Text = "lblAcBalAmt"
        Me.lblAcBalAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAcBalDC
        '
        Me.lblAcBalDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcBalDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAcBalDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcBalDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcBalDC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAcBalDC.Location = New System.Drawing.Point(868, 489)
        Me.lblAcBalDC.Name = "lblAcBalDC"
        Me.lblAcBalDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcBalDC.Size = New System.Drawing.Size(27, 20)
        Me.lblAcBalDC.TabIndex = 38
        Me.lblAcBalDC.Text = "lblAcBalDC"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(670, 491)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(98, 14)
        Me.Label14.TabIndex = 37
        Me.Label14.Text = "A/c Current Bal. :"
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(572, 491)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(65, 19)
        Me.lblBookType.TabIndex = 29
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 470)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(65, 15)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Narration :"
        '
        'LblDate_Issue_Receive
        '
        Me.LblDate_Issue_Receive.AutoSize = True
        Me.LblDate_Issue_Receive.BackColor = System.Drawing.SystemColors.Control
        Me.LblDate_Issue_Receive.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDate_Issue_Receive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate_Issue_Receive.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDate_Issue_Receive.Location = New System.Drawing.Point(3, 17)
        Me.LblDate_Issue_Receive.Name = "LblDate_Issue_Receive"
        Me.LblDate_Issue_Receive.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDate_Issue_Receive.Size = New System.Drawing.Size(86, 14)
        Me.LblDate_Issue_Receive.TabIndex = 21
        Me.LblDate_Issue_Receive.Text = "Voucher Date :"
        Me.LblDate_Issue_Receive.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(175, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(38, 14)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "VNo. :"
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
        Me.LblTotal.TabIndex = 23
        Me.LblTotal.Text = "Total :"
        '
        'LblDr
        '
        Me.LblDr.AutoSize = True
        Me.LblDr.BackColor = System.Drawing.SystemColors.Control
        Me.LblDr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDr.ForeColor = System.Drawing.Color.Black
        Me.LblDr.Location = New System.Drawing.Point(743, 512)
        Me.LblDr.Name = "LblDr"
        Me.LblDr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDr.Size = New System.Drawing.Size(25, 14)
        Me.LblDr.TabIndex = 24
        Me.LblDr.Text = "Dr :"
        '
        'LblDrAmt
        '
        Me.LblDrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblDrAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblDrAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDrAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDrAmt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LblDrAmt.Location = New System.Drawing.Point(772, 510)
        Me.LblDrAmt.Name = "LblDrAmt"
        Me.LblDrAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDrAmt.Size = New System.Drawing.Size(95, 17)
        Me.LblDrAmt.TabIndex = 25
        Me.LblDrAmt.Text = "LblDrAmt"
        Me.LblDrAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblCr
        '
        Me.LblCr.AutoSize = True
        Me.LblCr.BackColor = System.Drawing.SystemColors.Control
        Me.LblCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCr.ForeColor = System.Drawing.Color.Black
        Me.LblCr.Location = New System.Drawing.Point(742, 530)
        Me.LblCr.Name = "LblCr"
        Me.LblCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCr.Size = New System.Drawing.Size(26, 14)
        Me.LblCr.TabIndex = 27
        Me.LblCr.Text = "Cr :"
        '
        'LblCrAmt
        '
        Me.LblCrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblCrAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCrAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCrAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCrAmt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LblCrAmt.Location = New System.Drawing.Point(772, 528)
        Me.LblCrAmt.Name = "LblCrAmt"
        Me.LblCrAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCrAmt.Size = New System.Drawing.Size(95, 17)
        Me.LblCrAmt.TabIndex = 28
        Me.LblCrAmt.Text = "LblCrAmt"
        Me.LblCrAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblNet
        '
        Me.LblNet.AutoSize = True
        Me.LblNet.BackColor = System.Drawing.SystemColors.Control
        Me.LblNet.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNet.ForeColor = System.Drawing.Color.Black
        Me.LblNet.Location = New System.Drawing.Point(737, 548)
        Me.LblNet.Name = "LblNet"
        Me.LblNet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNet.Size = New System.Drawing.Size(31, 14)
        Me.LblNet.TabIndex = 30
        Me.LblNet.Text = "Net :"
        '
        'LblNetAmt
        '
        Me.LblNetAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblNetAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNetAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetAmt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LblNetAmt.Location = New System.Drawing.Point(772, 546)
        Me.LblNetAmt.Name = "LblNetAmt"
        Me.LblNetAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNetAmt.Size = New System.Drawing.Size(95, 17)
        Me.LblNetAmt.TabIndex = 31
        Me.LblNetAmt.Text = "LblNetAmt"
        Me.LblNetAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPaymentDetail
        '
        Me.lblPaymentDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaymentDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaymentDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentDetail.Location = New System.Drawing.Point(868, 546)
        Me.lblPaymentDetail.Name = "lblPaymentDetail"
        Me.lblPaymentDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentDetail.Size = New System.Drawing.Size(27, 17)
        Me.lblPaymentDetail.TabIndex = 32
        Me.lblPaymentDetail.Text = "lblPaymentDetail"
        Me.lblPaymentDetail.Visible = False
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
        Me.fraGridView.Size = New System.Drawing.Size(905, 576)
        Me.fraGridView.TabIndex = 33
        Me.fraGridView.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(902, 566)
        Me.SprdView.TabIndex = 34
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.cmdBillDetail)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdAuthorised)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.lblSR)
        Me.Frame3.Controls.Add(Me.lblLoanDetail)
        Me.Frame3.Controls.Add(Me.lblYM)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 569)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(908, 51)
        Me.Frame3.TabIndex = 35
        Me.Frame3.TabStop = False
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
        Me.lblSR.TabIndex = 41
        '
        'lblLoanDetail
        '
        Me.lblLoanDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblLoanDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLoanDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoanDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLoanDetail.Location = New System.Drawing.Point(8, 28)
        Me.lblLoanDetail.Name = "lblLoanDetail"
        Me.lblLoanDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLoanDetail.Size = New System.Drawing.Size(19, 13)
        Me.lblLoanDetail.TabIndex = 40
        Me.lblLoanDetail.Text = "lblLoanDetail"
        '
        'lblYM
        '
        Me.lblYM.AutoSize = True
        Me.lblYM.BackColor = System.Drawing.SystemColors.Control
        Me.lblYM.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblYM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblYM.Location = New System.Drawing.Point(1, 10)
        Me.lblYM.Name = "lblYM"
        Me.lblYM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblYM.Size = New System.Drawing.Size(37, 14)
        Me.lblYM.TabIndex = 36
        Me.lblYM.Text = "lblYM"
        Me.lblYM.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblYM.Visible = False
        '
        'frmProvisionVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(907, 621)
        Me.Controls.Add(Me.FraTrans)
        Me.Controls.Add(Me.fraGridView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-238, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProvisionVoucher"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Provision Voucher"
        Me.FraTrans.ResumeLayout(False)
        Me.FraTrans.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents CmdPopFromFile As Button
    Public WithEvents CommonDialogColor As ColorDialog
    Public WithEvents CommonDialogPrint As PrintDialog
    Public WithEvents CommonDialogSave As SaveFileDialog
    Public WithEvents CommonDialogOpen As OpenFileDialog
    Public WithEvents CommonDialogFont As FontDialog
#End Region
End Class