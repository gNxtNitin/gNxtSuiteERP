Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmeWayBillWebtel
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
        'Me.MDIParent = SalesGST.Master
        'SalesGST.Master.Show
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
	Public WithEvents txteWayValidupto As System.Windows.Forms.TextBox
	Public WithEvents txteWayBillDate As System.Windows.Forms.TextBox
	Public WithEvents cmdResetID As System.Windows.Forms.Button
	Public WithEvents txteWayBillNo As System.Windows.Forms.TextBox
	Public WithEvents txtResponseId As System.Windows.Forms.TextBox
	Public WithEvents cmdDistance As System.Windows.Forms.Button
	Public WithEvents cboDocType As System.Windows.Forms.ComboBox
	Public WithEvents cboSubType As System.Windows.Forms.ComboBox
	Public WithEvents txtSuppCustCode As System.Windows.Forms.TextBox
	Public WithEvents cboVehicleType As System.Windows.Forms.ComboBox
	Public WithEvents cboTransmode As System.Windows.Forms.ComboBox
	Public WithEvents txtVehicleNo As System.Windows.Forms.TextBox
	Public WithEvents txtTransportDocNo As System.Windows.Forms.TextBox
	Public WithEvents txtTransDocDate As System.Windows.Forms.TextBox
	Public WithEvents txtPreInvoice As System.Windows.Forms.TextBox
	Public WithEvents txtTransportCode As System.Windows.Forms.TextBox
	Public WithEvents txtTransName As System.Windows.Forms.TextBox
	Public WithEvents txtDistance As System.Windows.Forms.TextBox
	Public WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
	Public WithEvents txtSupplierName As System.Windows.Forms.TextBox
	Public WithEvents txtInvoiceDate As System.Windows.Forms.TextBox
	Public WithEvents lblIRNNo As System.Windows.Forms.Label
	Public WithEvents lblShippedFromCode As System.Windows.Forms.Label
	Public WithEvents lblDespatchFrom As System.Windows.Forms.Label
	Public WithEvents lblShippedCode As System.Windows.Forms.Label
	Public WithEvents lblShippedToSameParty As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblFilepath As System.Windows.Forms.Label
	Public WithEvents lbleWayType As System.Windows.Forms.Label
	Public WithEvents lblInvoiceSeqType As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents lblMKey As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents FraTop As System.Windows.Forms.GroupBox
	Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents lblIGSTAmt As System.Windows.Forms.Label
	Public WithEvents _Label_3 As System.Windows.Forms.Label
	Public WithEvents lblSGSTAmt As System.Windows.Forms.Label
	Public WithEvents _Label_2 As System.Windows.Forms.Label
	Public WithEvents lblCGSTAmt As System.Windows.Forms.Label
	Public WithEvents _Label_1 As System.Windows.Forms.Label
	Public WithEvents _Label_0 As System.Windows.Forms.Label
	Public WithEvents lblTaxableAmount As System.Windows.Forms.Label
	Public WithEvents lblNetAmount As System.Windows.Forms.Label
	Public WithEvents _Label_29 As System.Windows.Forms.Label
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Frabot As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents FraCmd As System.Windows.Forms.GroupBox
    Public WithEvents Label As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmeWayBillWebtel))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdResetID = New System.Windows.Forms.Button()
        Me.cmdDistance = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.Frabot = New System.Windows.Forms.GroupBox()
        Me.txteWayValidupto = New System.Windows.Forms.TextBox()
        Me.txteWayBillDate = New System.Windows.Forms.TextBox()
        Me.txteWayBillNo = New System.Windows.Forms.TextBox()
        Me.txtResponseId = New System.Windows.Forms.TextBox()
        Me.FraTop = New System.Windows.Forms.GroupBox()
        Me.cboDocType = New System.Windows.Forms.ComboBox()
        Me.cboSubType = New System.Windows.Forms.ComboBox()
        Me.txtSuppCustCode = New System.Windows.Forms.TextBox()
        Me.cboVehicleType = New System.Windows.Forms.ComboBox()
        Me.cboTransmode = New System.Windows.Forms.ComboBox()
        Me.txtVehicleNo = New System.Windows.Forms.TextBox()
        Me.txtTransportDocNo = New System.Windows.Forms.TextBox()
        Me.txtTransDocDate = New System.Windows.Forms.TextBox()
        Me.txtPreInvoice = New System.Windows.Forms.TextBox()
        Me.txtTransportCode = New System.Windows.Forms.TextBox()
        Me.txtTransName = New System.Windows.Forms.TextBox()
        Me.txtDistance = New System.Windows.Forms.TextBox()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.txtInvoiceDate = New System.Windows.Forms.TextBox()
        Me.lblIRNNo = New System.Windows.Forms.Label()
        Me.lblShippedFromCode = New System.Windows.Forms.Label()
        Me.lblDespatchFrom = New System.Windows.Forms.Label()
        Me.lblShippedCode = New System.Windows.Forms.Label()
        Me.lblShippedToSameParty = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFilepath = New System.Windows.Forms.Label()
        Me.lbleWayType = New System.Windows.Forms.Label()
        Me.lblInvoiceSeqType = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblIGSTAmt = New System.Windows.Forms.Label()
        Me._Label_3 = New System.Windows.Forms.Label()
        Me.lblSGSTAmt = New System.Windows.Forms.Label()
        Me._Label_2 = New System.Windows.Forms.Label()
        Me.lblCGSTAmt = New System.Windows.Forms.Label()
        Me._Label_1 = New System.Windows.Forms.Label()
        Me._Label_0 = New System.Windows.Forms.Label()
        Me.lblTaxableAmount = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me._Label_29 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraCmd = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.txtBillToLocation = New System.Windows.Forms.TextBox()
        Me.Frabot.SuspendLayout()
        Me.FraTop.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraCmd.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdResetID
        '
        Me.cmdResetID.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetID.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdResetID.Enabled = False
        Me.cmdResetID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdResetID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetID.Image = CType(resources.GetObject("cmdResetID.Image"), System.Drawing.Image)
        Me.cmdResetID.Location = New System.Drawing.Point(324, 346)
        Me.cmdResetID.Name = "cmdResetID"
        Me.cmdResetID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdResetID.Size = New System.Drawing.Size(27, 19)
        Me.cmdResetID.TabIndex = 16
        Me.cmdResetID.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdResetID, "Preview")
        Me.cmdResetID.UseVisualStyleBackColor = False
        '
        'cmdDistance
        '
        Me.cmdDistance.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDistance.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDistance.Enabled = False
        Me.cmdDistance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDistance.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDistance.Image = CType(resources.GetObject("cmdDistance.Image"), System.Drawing.Image)
        Me.cmdDistance.Location = New System.Drawing.Point(620, 82)
        Me.cmdDistance.Name = "cmdDistance"
        Me.cmdDistance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDistance.Size = New System.Drawing.Size(27, 19)
        Me.cmdDistance.TabIndex = 70
        Me.cmdDistance.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDistance, "Preview")
        Me.cmdDistance.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(606, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 26
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(540, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 25
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(474, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 24
        Me.CmdPreview.Text = "EWB &Print"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(408, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 23
        Me.cmdPrint.Text = "&EWB Create"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(342, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 22
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(276, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 21
        Me.cmdSavePrint.Text = "Save&& Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(210, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 20
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(144, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 13
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(78, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'Frabot
        '
        Me.Frabot.BackColor = System.Drawing.SystemColors.Control
        Me.Frabot.Controls.Add(Me.txteWayValidupto)
        Me.Frabot.Controls.Add(Me.txteWayBillDate)
        Me.Frabot.Controls.Add(Me.cmdResetID)
        Me.Frabot.Controls.Add(Me.txteWayBillNo)
        Me.Frabot.Controls.Add(Me.txtResponseId)
        Me.Frabot.Controls.Add(Me.FraTop)
        Me.Frabot.Controls.Add(Me.Frame2)
        Me.Frabot.Controls.Add(Me.lblIGSTAmt)
        Me.Frabot.Controls.Add(Me._Label_3)
        Me.Frabot.Controls.Add(Me.lblSGSTAmt)
        Me.Frabot.Controls.Add(Me._Label_2)
        Me.Frabot.Controls.Add(Me.lblCGSTAmt)
        Me.Frabot.Controls.Add(Me._Label_1)
        Me.Frabot.Controls.Add(Me._Label_0)
        Me.Frabot.Controls.Add(Me.lblTaxableAmount)
        Me.Frabot.Controls.Add(Me.lblNetAmount)
        Me.Frabot.Controls.Add(Me._Label_29)
        Me.Frabot.Controls.Add(Me.Label17)
        Me.Frabot.Controls.Add(Me.Label16)
        Me.Frabot.Controls.Add(Me.Label13)
        Me.Frabot.Controls.Add(Me.Label12)
        Me.Frabot.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frabot.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frabot.Location = New System.Drawing.Point(0, -6)
        Me.Frabot.Name = "Frabot"
        Me.Frabot.Padding = New System.Windows.Forms.Padding(0)
        Me.Frabot.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frabot.Size = New System.Drawing.Size(751, 417)
        Me.Frabot.TabIndex = 29
        Me.Frabot.TabStop = False
        '
        'txteWayValidupto
        '
        Me.txteWayValidupto.AcceptsReturn = True
        Me.txteWayValidupto.BackColor = System.Drawing.SystemColors.Window
        Me.txteWayValidupto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteWayValidupto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteWayValidupto.Enabled = False
        Me.txteWayValidupto.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteWayValidupto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txteWayValidupto.Location = New System.Drawing.Point(347, 392)
        Me.txteWayValidupto.MaxLength = 0
        Me.txteWayValidupto.Name = "txteWayValidupto"
        Me.txteWayValidupto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteWayValidupto.Size = New System.Drawing.Size(135, 20)
        Me.txteWayValidupto.TabIndex = 19
        '
        'txteWayBillDate
        '
        Me.txteWayBillDate.AcceptsReturn = True
        Me.txteWayBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txteWayBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteWayBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteWayBillDate.Enabled = False
        Me.txteWayBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteWayBillDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txteWayBillDate.Location = New System.Drawing.Point(109, 368)
        Me.txteWayBillDate.MaxLength = 0
        Me.txteWayBillDate.Name = "txteWayBillDate"
        Me.txteWayBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteWayBillDate.Size = New System.Drawing.Size(137, 20)
        Me.txteWayBillDate.TabIndex = 18
        '
        'txteWayBillNo
        '
        Me.txteWayBillNo.AcceptsReturn = True
        Me.txteWayBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txteWayBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteWayBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteWayBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteWayBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txteWayBillNo.Location = New System.Drawing.Point(109, 390)
        Me.txteWayBillNo.MaxLength = 0
        Me.txteWayBillNo.Name = "txteWayBillNo"
        Me.txteWayBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteWayBillNo.Size = New System.Drawing.Size(135, 20)
        Me.txteWayBillNo.TabIndex = 17
        '
        'txtResponseId
        '
        Me.txtResponseId.AcceptsReturn = True
        Me.txtResponseId.BackColor = System.Drawing.SystemColors.Window
        Me.txtResponseId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResponseId.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtResponseId.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResponseId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtResponseId.Location = New System.Drawing.Point(109, 346)
        Me.txtResponseId.MaxLength = 0
        Me.txtResponseId.Name = "txtResponseId"
        Me.txtResponseId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtResponseId.Size = New System.Drawing.Size(213, 20)
        Me.txtResponseId.TabIndex = 15
        '
        'FraTop
        '
        Me.FraTop.BackColor = System.Drawing.SystemColors.Control
        Me.FraTop.Controls.Add(Me.txtBillToLocation)
        Me.FraTop.Controls.Add(Me.cmdDistance)
        Me.FraTop.Controls.Add(Me.cboDocType)
        Me.FraTop.Controls.Add(Me.cboSubType)
        Me.FraTop.Controls.Add(Me.txtSuppCustCode)
        Me.FraTop.Controls.Add(Me.cboVehicleType)
        Me.FraTop.Controls.Add(Me.cboTransmode)
        Me.FraTop.Controls.Add(Me.txtVehicleNo)
        Me.FraTop.Controls.Add(Me.txtTransportDocNo)
        Me.FraTop.Controls.Add(Me.txtTransDocDate)
        Me.FraTop.Controls.Add(Me.txtPreInvoice)
        Me.FraTop.Controls.Add(Me.txtTransportCode)
        Me.FraTop.Controls.Add(Me.txtTransName)
        Me.FraTop.Controls.Add(Me.txtDistance)
        Me.FraTop.Controls.Add(Me.txtInvoiceNo)
        Me.FraTop.Controls.Add(Me.txtSupplierName)
        Me.FraTop.Controls.Add(Me.txtInvoiceDate)
        Me.FraTop.Controls.Add(Me.lblIRNNo)
        Me.FraTop.Controls.Add(Me.lblShippedFromCode)
        Me.FraTop.Controls.Add(Me.lblDespatchFrom)
        Me.FraTop.Controls.Add(Me.lblShippedCode)
        Me.FraTop.Controls.Add(Me.lblShippedToSameParty)
        Me.FraTop.Controls.Add(Me.Label1)
        Me.FraTop.Controls.Add(Me.lblFilepath)
        Me.FraTop.Controls.Add(Me.lbleWayType)
        Me.FraTop.Controls.Add(Me.lblInvoiceSeqType)
        Me.FraTop.Controls.Add(Me.Label15)
        Me.FraTop.Controls.Add(Me.Label14)
        Me.FraTop.Controls.Add(Me.Label11)
        Me.FraTop.Controls.Add(Me.Label10)
        Me.FraTop.Controls.Add(Me.Label9)
        Me.FraTop.Controls.Add(Me.Label8)
        Me.FraTop.Controls.Add(Me.Label7)
        Me.FraTop.Controls.Add(Me.Label6)
        Me.FraTop.Controls.Add(Me.Label5)
        Me.FraTop.Controls.Add(Me.Label4)
        Me.FraTop.Controls.Add(Me.lblMKey)
        Me.FraTop.Controls.Add(Me.Label3)
        Me.FraTop.Controls.Add(Me.Label2)
        Me.FraTop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTop.Location = New System.Drawing.Point(0, 2)
        Me.FraTop.Name = "FraTop"
        Me.FraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTop.Size = New System.Drawing.Size(751, 173)
        Me.FraTop.TabIndex = 31
        Me.FraTop.TabStop = False
        '
        'cboDocType
        '
        Me.cboDocType.BackColor = System.Drawing.SystemColors.Window
        Me.cboDocType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDocType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocType.Enabled = False
        Me.cboDocType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDocType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDocType.Location = New System.Drawing.Point(482, 34)
        Me.cboDocType.Name = "cboDocType"
        Me.cboDocType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDocType.Size = New System.Drawing.Size(137, 22)
        Me.cboDocType.TabIndex = 48
        '
        'cboSubType
        '
        Me.cboSubType.BackColor = System.Drawing.SystemColors.Window
        Me.cboSubType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSubType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSubType.Enabled = False
        Me.cboSubType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSubType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSubType.Location = New System.Drawing.Point(120, 34)
        Me.cboSubType.Name = "cboSubType"
        Me.cboSubType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSubType.Size = New System.Drawing.Size(119, 22)
        Me.cboSubType.TabIndex = 46
        '
        'txtSuppCustCode
        '
        Me.txtSuppCustCode.AcceptsReturn = True
        Me.txtSuppCustCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppCustCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppCustCode.Enabled = False
        Me.txtSuppCustCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppCustCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSuppCustCode.Location = New System.Drawing.Point(532, 60)
        Me.txtSuppCustCode.MaxLength = 0
        Me.txtSuppCustCode.Name = "txtSuppCustCode"
        Me.txtSuppCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppCustCode.Size = New System.Drawing.Size(87, 20)
        Me.txtSuppCustCode.TabIndex = 43
        '
        'cboVehicleType
        '
        Me.cboVehicleType.BackColor = System.Drawing.SystemColors.Window
        Me.cboVehicleType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVehicleType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboVehicleType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboVehicleType.Location = New System.Drawing.Point(482, 146)
        Me.cboVehicleType.Name = "cboVehicleType"
        Me.cboVehicleType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboVehicleType.Size = New System.Drawing.Size(137, 22)
        Me.cboVehicleType.TabIndex = 12
        '
        'cboTransmode
        '
        Me.cboTransmode.BackColor = System.Drawing.SystemColors.Window
        Me.cboTransmode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTransmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTransmode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTransmode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTransmode.Location = New System.Drawing.Point(120, 80)
        Me.cboTransmode.Name = "cboTransmode"
        Me.cboTransmode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTransmode.Size = New System.Drawing.Size(187, 22)
        Me.cboTransmode.TabIndex = 5
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.AcceptsReturn = True
        Me.txtVehicleNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicleNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicleNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVehicleNo.Location = New System.Drawing.Point(120, 148)
        Me.txtVehicleNo.MaxLength = 0
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicleNo.Size = New System.Drawing.Size(187, 20)
        Me.txtVehicleNo.TabIndex = 11
        '
        'txtTransportDocNo
        '
        Me.txtTransportDocNo.AcceptsReturn = True
        Me.txtTransportDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransportDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransportDocNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransportDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransportDocNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTransportDocNo.Location = New System.Drawing.Point(120, 126)
        Me.txtTransportDocNo.MaxLength = 0
        Me.txtTransportDocNo.Name = "txtTransportDocNo"
        Me.txtTransportDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransportDocNo.Size = New System.Drawing.Size(187, 20)
        Me.txtTransportDocNo.TabIndex = 9
        '
        'txtTransDocDate
        '
        Me.txtTransDocDate.AcceptsReturn = True
        Me.txtTransDocDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransDocDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransDocDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransDocDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransDocDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTransDocDate.Location = New System.Drawing.Point(482, 126)
        Me.txtTransDocDate.MaxLength = 0
        Me.txtTransDocDate.Name = "txtTransDocDate"
        Me.txtTransDocDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransDocDate.Size = New System.Drawing.Size(137, 20)
        Me.txtTransDocDate.TabIndex = 10
        '
        'txtPreInvoice
        '
        Me.txtPreInvoice.AcceptsReturn = True
        Me.txtPreInvoice.BackColor = System.Drawing.SystemColors.Window
        Me.txtPreInvoice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreInvoice.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPreInvoice.Enabled = False
        Me.txtPreInvoice.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreInvoice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPreInvoice.Location = New System.Drawing.Point(120, 12)
        Me.txtPreInvoice.MaxLength = 0
        Me.txtPreInvoice.Name = "txtPreInvoice"
        Me.txtPreInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPreInvoice.Size = New System.Drawing.Size(19, 20)
        Me.txtPreInvoice.TabIndex = 1
        '
        'txtTransportCode
        '
        Me.txtTransportCode.AcceptsReturn = True
        Me.txtTransportCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransportCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransportCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransportCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransportCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTransportCode.Location = New System.Drawing.Point(482, 104)
        Me.txtTransportCode.MaxLength = 0
        Me.txtTransportCode.Name = "txtTransportCode"
        Me.txtTransportCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransportCode.Size = New System.Drawing.Size(137, 20)
        Me.txtTransportCode.TabIndex = 8
        '
        'txtTransName
        '
        Me.txtTransName.AcceptsReturn = True
        Me.txtTransName.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTransName.Location = New System.Drawing.Point(120, 104)
        Me.txtTransName.MaxLength = 0
        Me.txtTransName.Name = "txtTransName"
        Me.txtTransName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransName.Size = New System.Drawing.Size(187, 20)
        Me.txtTransName.TabIndex = 7
        '
        'txtDistance
        '
        Me.txtDistance.AcceptsReturn = True
        Me.txtDistance.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDistance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDistance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDistance.Location = New System.Drawing.Point(482, 82)
        Me.txtDistance.MaxLength = 0
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDistance.Size = New System.Drawing.Size(137, 20)
        Me.txtDistance.TabIndex = 6
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.AcceptsReturn = True
        Me.txtInvoiceNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInvoiceNo.Location = New System.Drawing.Point(138, 12)
        Me.txtInvoiceNo.MaxLength = 0
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvoiceNo.Size = New System.Drawing.Size(101, 20)
        Me.txtInvoiceNo.TabIndex = 2
        '
        'txtSupplierName
        '
        Me.txtSupplierName.AcceptsReturn = True
        Me.txtSupplierName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierName.Enabled = False
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSupplierName.Location = New System.Drawing.Point(120, 60)
        Me.txtSupplierName.MaxLength = 0
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierName.Size = New System.Drawing.Size(409, 20)
        Me.txtSupplierName.TabIndex = 4
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.AcceptsReturn = True
        Me.txtInvoiceDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvoiceDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInvoiceDate.Location = New System.Drawing.Point(482, 12)
        Me.txtInvoiceDate.MaxLength = 0
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.ReadOnly = True
        Me.txtInvoiceDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvoiceDate.Size = New System.Drawing.Size(137, 20)
        Me.txtInvoiceDate.TabIndex = 3
        '
        'lblIRNNo
        '
        Me.lblIRNNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblIRNNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIRNNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIRNNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIRNNo.Location = New System.Drawing.Point(650, 41)
        Me.lblIRNNo.Name = "lblIRNNo"
        Me.lblIRNNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIRNNo.Size = New System.Drawing.Size(69, 11)
        Me.lblIRNNo.TabIndex = 71
        Me.lblIRNNo.Text = "lblIRNNo"
        '
        'lblShippedFromCode
        '
        Me.lblShippedFromCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblShippedFromCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShippedFromCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShippedFromCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShippedFromCode.Location = New System.Drawing.Point(646, 28)
        Me.lblShippedFromCode.Name = "lblShippedFromCode"
        Me.lblShippedFromCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShippedFromCode.Size = New System.Drawing.Size(59, 13)
        Me.lblShippedFromCode.TabIndex = 69
        Me.lblShippedFromCode.Text = "lblDespatchFromAdd1"
        Me.lblShippedFromCode.Visible = False
        '
        'lblDespatchFrom
        '
        Me.lblDespatchFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblDespatchFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDespatchFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDespatchFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDespatchFrom.Location = New System.Drawing.Point(642, 12)
        Me.lblDespatchFrom.Name = "lblDespatchFrom"
        Me.lblDespatchFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDespatchFrom.Size = New System.Drawing.Size(73, 17)
        Me.lblDespatchFrom.TabIndex = 68
        Me.lblDespatchFrom.Text = "lblDespatchFrom"
        Me.lblDespatchFrom.Visible = False
        '
        'lblShippedCode
        '
        Me.lblShippedCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblShippedCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShippedCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShippedCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShippedCode.Location = New System.Drawing.Point(642, 100)
        Me.lblShippedCode.Name = "lblShippedCode"
        Me.lblShippedCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShippedCode.Size = New System.Drawing.Size(63, 9)
        Me.lblShippedCode.TabIndex = 67
        Me.lblShippedCode.Text = "lblShippedCode"
        '
        'lblShippedToSameParty
        '
        Me.lblShippedToSameParty.BackColor = System.Drawing.SystemColors.Control
        Me.lblShippedToSameParty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShippedToSameParty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShippedToSameParty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShippedToSameParty.Location = New System.Drawing.Point(640, 114)
        Me.lblShippedToSameParty.Name = "lblShippedToSameParty"
        Me.lblShippedToSameParty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShippedToSameParty.Size = New System.Drawing.Size(69, 11)
        Me.lblShippedToSameParty.TabIndex = 66
        Me.lblShippedToSameParty.Text = "lblShippedToSameParty"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(48, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(69, 14)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Invoice No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFilepath
        '
        Me.lblFilepath.AutoSize = True
        Me.lblFilepath.BackColor = System.Drawing.SystemColors.Control
        Me.lblFilepath.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFilepath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilepath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilepath.Location = New System.Drawing.Point(694, 136)
        Me.lblFilepath.Name = "lblFilepath"
        Me.lblFilepath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFilepath.Size = New System.Drawing.Size(44, 14)
        Me.lblFilepath.TabIndex = 54
        Me.lblFilepath.Text = "Filepath"
        Me.lblFilepath.Visible = False
        '
        'lbleWayType
        '
        Me.lbleWayType.AutoSize = True
        Me.lbleWayType.BackColor = System.Drawing.SystemColors.Control
        Me.lbleWayType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbleWayType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbleWayType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbleWayType.Location = New System.Drawing.Point(626, 132)
        Me.lbleWayType.Name = "lbleWayType"
        Me.lbleWayType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbleWayType.Size = New System.Drawing.Size(68, 14)
        Me.lbleWayType.TabIndex = 51
        Me.lbleWayType.Text = "lbleWayType"
        '
        'lblInvoiceSeqType
        '
        Me.lblInvoiceSeqType.AutoSize = True
        Me.lblInvoiceSeqType.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvoiceSeqType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInvoiceSeqType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceSeqType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvoiceSeqType.Location = New System.Drawing.Point(624, 154)
        Me.lblInvoiceSeqType.Name = "lblInvoiceSeqType"
        Me.lblInvoiceSeqType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvoiceSeqType.Size = New System.Drawing.Size(93, 14)
        Me.lblInvoiceSeqType.TabIndex = 50
        Me.lblInvoiceSeqType.Text = "lblInvoiceSeqType"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(379, 38)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(98, 14)
        Me.Label15.TabIndex = 49
        Me.Label15.Text = "Document Type :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(14, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(103, 14)
        Me.Label14.TabIndex = 47
        Me.Label14.Text = "Sub Supply Type :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(47, 150)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(70, 14)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Vehicle No :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(394, 150)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(82, 14)
        Me.Label10.TabIndex = 41
        Me.Label10.Text = "Vehicle Type :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(1, 128)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(116, 14)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "Transporter Doc No:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(360, 128)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(114, 14)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "Transport Doc Date:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(398, 106)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(80, 14)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Transport ID :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(7, 108)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(113, 14)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "Transporter Name :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(419, 84)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(60, 14)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Distance :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(41, 86)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(78, 14)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Trans Mode :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(354, 12)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 34
        Me.lblMKey.Text = "lblMKey"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(21, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(103, 14)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Customer Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(441, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(37, 14)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdMain)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 168)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(751, 177)
        Me.Frame2.TabIndex = 30
        Me.Frame2.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(747, 165)
        Me.SprdMain.TabIndex = 14
        '
        'lblIGSTAmt
        '
        Me.lblIGSTAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblIGSTAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIGSTAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIGSTAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIGSTAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIGSTAmt.Location = New System.Drawing.Point(658, 368)
        Me.lblIGSTAmt.Name = "lblIGSTAmt"
        Me.lblIGSTAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIGSTAmt.Size = New System.Drawing.Size(91, 19)
        Me.lblIGSTAmt.TabIndex = 65
        Me.lblIGSTAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_3
        '
        Me._Label_3.AutoSize = True
        Me._Label_3.BackColor = System.Drawing.SystemColors.Control
        Me._Label_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_3, CType(3, Short))
        Me._Label_3.Location = New System.Drawing.Point(619, 370)
        Me._Label_3.Name = "_Label_3"
        Me._Label_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_3.Size = New System.Drawing.Size(38, 14)
        Me._Label_3.TabIndex = 64
        Me._Label_3.Text = "IGST :"
        Me._Label_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSGSTAmt
        '
        Me.lblSGSTAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblSGSTAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSGSTAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSGSTAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSGSTAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSGSTAmt.Location = New System.Drawing.Point(530, 368)
        Me.lblSGSTAmt.Name = "lblSGSTAmt"
        Me.lblSGSTAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSGSTAmt.Size = New System.Drawing.Size(79, 19)
        Me.lblSGSTAmt.TabIndex = 63
        Me.lblSGSTAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_2
        '
        Me._Label_2.AutoSize = True
        Me._Label_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_2, CType(2, Short))
        Me._Label_2.Location = New System.Drawing.Point(485, 370)
        Me._Label_2.Name = "_Label_2"
        Me._Label_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_2.Size = New System.Drawing.Size(42, 14)
        Me._Label_2.TabIndex = 62
        Me._Label_2.Text = "SGST :"
        Me._Label_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCGSTAmt
        '
        Me.lblCGSTAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblCGSTAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCGSTAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCGSTAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCGSTAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCGSTAmt.Location = New System.Drawing.Point(402, 368)
        Me.lblCGSTAmt.Name = "lblCGSTAmt"
        Me.lblCGSTAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCGSTAmt.Size = New System.Drawing.Size(79, 19)
        Me.lblCGSTAmt.TabIndex = 61
        Me.lblCGSTAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_1
        '
        Me._Label_1.AutoSize = True
        Me._Label_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_1, CType(1, Short))
        Me._Label_1.Location = New System.Drawing.Point(359, 370)
        Me._Label_1.Name = "_Label_1"
        Me._Label_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_1.Size = New System.Drawing.Size(43, 14)
        Me._Label_1.TabIndex = 60
        Me._Label_1.Text = "CGST :"
        Me._Label_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_0
        '
        Me._Label_0.AutoSize = True
        Me._Label_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_0, CType(0, Short))
        Me._Label_0.Location = New System.Drawing.Point(578, 352)
        Me._Label_0.Name = "_Label_0"
        Me._Label_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_0.Size = New System.Drawing.Size(80, 14)
        Me._Label_0.TabIndex = 59
        Me._Label_0.Text = "Taxable Amt :"
        Me._Label_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTaxableAmount
        '
        Me.lblTaxableAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTaxableAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTaxableAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTaxableAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxableAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTaxableAmount.Location = New System.Drawing.Point(658, 348)
        Me.lblTaxableAmount.Name = "lblTaxableAmount"
        Me.lblTaxableAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTaxableAmount.Size = New System.Drawing.Size(91, 19)
        Me.lblTaxableAmount.TabIndex = 58
        Me.lblTaxableAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(658, 388)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(91, 19)
        Me.lblNetAmount.TabIndex = 57
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label_29
        '
        Me._Label_29.AutoSize = True
        Me._Label_29.BackColor = System.Drawing.SystemColors.Control
        Me._Label_29.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label_29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label_29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label.SetIndex(Me._Label_29, CType(29, Short))
        Me._Label_29.Location = New System.Drawing.Point(570, 390)
        Me._Label_29.Name = "_Label_29"
        Me._Label_29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label_29.Size = New System.Drawing.Size(78, 14)
        Me._Label_29.TabIndex = 55
        Me._Label_29.Text = "Net Amount :"
        Me._Label_29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(247, 392)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(94, 14)
        Me.Label17.TabIndex = 53
        Me.Label17.Text = "Valid Upto Date :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(7, 370)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(88, 14)
        Me.Label16.TabIndex = 52
        Me.Label16.Text = "eWay Bill Date :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(26, 392)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(78, 14)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "eWay Bill No :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(31, 348)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(79, 14)
        Me.Label12.TabIndex = 44
        Me.Label12.Text = "Response ID:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(751, 411)
        Me.SprdView.TabIndex = 28
        '
        'FraCmd
        '
        Me.FraCmd.BackColor = System.Drawing.SystemColors.Control
        Me.FraCmd.Controls.Add(Me.CmdClose)
        Me.FraCmd.Controls.Add(Me.CmdView)
        Me.FraCmd.Controls.Add(Me.CmdPreview)
        Me.FraCmd.Controls.Add(Me.cmdPrint)
        Me.FraCmd.Controls.Add(Me.CmdDelete)
        Me.FraCmd.Controls.Add(Me.cmdSavePrint)
        Me.FraCmd.Controls.Add(Me.CmdSave)
        Me.FraCmd.Controls.Add(Me.CmdModify)
        Me.FraCmd.Controls.Add(Me.CmdAdd)
        Me.FraCmd.Controls.Add(Me.Report1)
        Me.FraCmd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCmd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraCmd.Location = New System.Drawing.Point(0, 406)
        Me.FraCmd.Name = "FraCmd"
        Me.FraCmd.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCmd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCmd.Size = New System.Drawing.Size(751, 51)
        Me.FraCmd.TabIndex = 27
        Me.FraCmd.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(22, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 27
        '
        'txtBillToLocation
        '
        Me.txtBillToLocation.AcceptsReturn = True
        Me.txtBillToLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillToLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillToLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillToLocation.Enabled = False
        Me.txtBillToLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillToLocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillToLocation.Location = New System.Drawing.Point(620, 60)
        Me.txtBillToLocation.MaxLength = 0
        Me.txtBillToLocation.Name = "txtBillToLocation"
        Me.txtBillToLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillToLocation.Size = New System.Drawing.Size(87, 20)
        Me.txtBillToLocation.TabIndex = 72
        '
        'frmeWayBillWebtel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(751, 458)
        Me.Controls.Add(Me.Frabot)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraCmd)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmeWayBillWebtel"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "e-Way Bill (Webtel)"
        Me.Frabot.ResumeLayout(False)
        Me.Frabot.PerformLayout()
        Me.FraTop.ResumeLayout(False)
        Me.FraTop.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraCmd.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        SprdView.DataSource = Nothing ' CType(ADataPPOMain, MSDATASRC.DataSource)
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdView.DataSource = Nothing
	End Sub

    Public WithEvents txtBillToLocation As TextBox
#End Region
End Class