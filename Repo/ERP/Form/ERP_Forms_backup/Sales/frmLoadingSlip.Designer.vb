Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLoadingSlip
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

        'InventoryGST.Master.Show
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
    Public WithEvents cmdSearchTrip As System.Windows.Forms.Button
    Public WithEvents txtGRNo As System.Windows.Forms.TextBox
    Public WithEvents txtGRDate As System.Windows.Forms.TextBox
    Public WithEvents chkAckReceipt As System.Windows.Forms.CheckBox
    Public WithEvents txtAckDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents fraReceipt As System.Windows.Forms.GroupBox
    Public WithEvents txtTotPendingBills As System.Windows.Forms.TextBox
    Public WithEvents txtTotBills As System.Windows.Forms.TextBox
    Public WithEvents chkWOCollection As System.Windows.Forms.CheckBox
    Public WithEvents chkThirdParty As System.Windows.Forms.CheckBox
    Public WithEvents txtTripNo As System.Windows.Forms.TextBox
    Public WithEvents txtTripDate As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtVehicleType As System.Windows.Forms.TextBox
    Public WithEvents _optShow_4 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents cmdPopulateSuppBill As System.Windows.Forms.Button
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents FraShow As System.Windows.Forms.GroupBox
    Public WithEvents txtVehicleNo As System.Windows.Forms.TextBox
    Public WithEvents txtCSlipDate As System.Windows.Forms.TextBox
    Public WithEvents txtCSlipNo As System.Windows.Forms.TextBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents txtTransporterName As System.Windows.Forms.TextBox
    Public WithEvents txtSlipDate As System.Windows.Forms.TextBox
    Public WithEvents txtInDateTime As System.Windows.Forms.MaskedTextBox
    Public WithEvents _optFreightType_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optFreightType_1 As System.Windows.Forms.RadioButton
    Public WithEvents fraFreightType As System.Windows.Forms.GroupBox
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents lblAck As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraTop As System.Windows.Forms.GroupBox
    Public WithEvents txtTripAmount As System.Windows.Forms.TextBox
    Public WithEvents txtTollTax As System.Windows.Forms.TextBox
    Public WithEvents txtNetAmount As System.Windows.Forms.TextBox
    Public WithEvents txtOthCharges As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdMainOth As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblPacket As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblTotItemQty As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
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
    Public WithEvents optFreightType As VB6.RadioButtonArray
    Public WithEvents optShow As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoadingSlip))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchTrip = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdPopulateSuppBill = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.cmdPopulateBillAll = New System.Windows.Forms.Button()
        Me.cmdShowBarcode = New System.Windows.Forms.Button()
        Me.Frabot = New System.Windows.Forms.GroupBox()
        Me.FraTop = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtBarCode = New System.Windows.Forms.TextBox()
        Me.txtVehicleNo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtGRNo = New System.Windows.Forms.TextBox()
        Me.txtGRDate = New System.Windows.Forms.TextBox()
        Me.fraReceipt = New System.Windows.Forms.GroupBox()
        Me.chkAckReceipt = New System.Windows.Forms.CheckBox()
        Me.txtAckDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtTotPendingBills = New System.Windows.Forms.TextBox()
        Me.txtTotBills = New System.Windows.Forms.TextBox()
        Me.chkWOCollection = New System.Windows.Forms.CheckBox()
        Me.chkThirdParty = New System.Windows.Forms.CheckBox()
        Me.txtTripNo = New System.Windows.Forms.TextBox()
        Me.txtTripDate = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtVehicleType = New System.Windows.Forms.TextBox()
        Me.FraShow = New System.Windows.Forms.GroupBox()
        Me._optShow_4 = New System.Windows.Forms.RadioButton()
        Me._optShow_3 = New System.Windows.Forms.RadioButton()
        Me._optShow_2 = New System.Windows.Forms.RadioButton()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCSlipDate = New System.Windows.Forms.TextBox()
        Me.txtCSlipNo = New System.Windows.Forms.TextBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.txtTransporterName = New System.Windows.Forms.TextBox()
        Me.txtSlipDate = New System.Windows.Forms.TextBox()
        Me.txtInDateTime = New System.Windows.Forms.MaskedTextBox()
        Me.fraFreightType = New System.Windows.Forms.GroupBox()
        Me._optFreightType_0 = New System.Windows.Forms.RadioButton()
        Me._optFreightType_1 = New System.Windows.Forms.RadioButton()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblAck = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblNetWt = New System.Windows.Forms.Label()
        Me.txtTearWt = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtGrossWt = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtTripAmount = New System.Windows.Forms.TextBox()
        Me.txtTollTax = New System.Windows.Forms.TextBox()
        Me.txtNetAmount = New System.Windows.Forms.TextBox()
        Me.txtOthCharges = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdMainOth = New AxFPSpreadADO.AxfpSpread()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblPacket = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTotItemQty = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraCmd = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.optFreightType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frabot.SuspendLayout()
        Me.FraTop.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.fraReceipt.SuspendLayout()
        Me.FraShow.SuspendLayout()
        Me.fraFreightType.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMainOth, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraCmd.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optFreightType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchTrip
        '
        Me.cmdSearchTrip.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchTrip.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchTrip.Enabled = False
        Me.cmdSearchTrip.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchTrip.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchTrip.Image = CType(resources.GetObject("cmdSearchTrip.Image"), System.Drawing.Image)
        Me.cmdSearchTrip.Location = New System.Drawing.Point(230, 38)
        Me.cmdSearchTrip.Name = "cmdSearchTrip"
        Me.cmdSearchTrip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchTrip.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchTrip.TabIndex = 4
        Me.cmdSearchTrip.TabStop = False
        Me.cmdSearchTrip.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchTrip, "Search")
        Me.cmdSearchTrip.UseVisualStyleBackColor = False
        Me.cmdSearchTrip.Visible = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(301, 39)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearch.TabIndex = 9
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdPopulateSuppBill
        '
        Me.cmdPopulateSuppBill.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPopulateSuppBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulateSuppBill.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulateSuppBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulateSuppBill.Location = New System.Drawing.Point(219, 51)
        Me.cmdPopulateSuppBill.Name = "cmdPopulateSuppBill"
        Me.cmdPopulateSuppBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulateSuppBill.Size = New System.Drawing.Size(77, 34)
        Me.cmdPopulateSuppBill.TabIndex = 23
        Me.cmdPopulateSuppBill.TabStop = False
        Me.cmdPopulateSuppBill.Text = "Populate"
        Me.ToolTip1.SetToolTip(Me.cmdPopulateSuppBill, "Search")
        Me.cmdPopulateSuppBill.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(686, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 32
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(620, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 31
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
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(554, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 30
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(488, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 29
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(422, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 28
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(356, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 27
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(290, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 26
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(224, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 25
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(158, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'cmdPopulateBillAll
        '
        Me.cmdPopulateBillAll.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPopulateBillAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulateBillAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulateBillAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulateBillAll.Location = New System.Drawing.Point(219, 11)
        Me.cmdPopulateBillAll.Name = "cmdPopulateBillAll"
        Me.cmdPopulateBillAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulateBillAll.Size = New System.Drawing.Size(77, 34)
        Me.cmdPopulateBillAll.TabIndex = 88
        Me.cmdPopulateBillAll.TabStop = False
        Me.cmdPopulateBillAll.Text = "Populate All "
        Me.ToolTip1.SetToolTip(Me.cmdPopulateBillAll, "Search")
        Me.cmdPopulateBillAll.UseVisualStyleBackColor = False
        '
        'cmdShowBarcode
        '
        Me.cmdShowBarcode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdShowBarcode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShowBarcode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShowBarcode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShowBarcode.Location = New System.Drawing.Point(6, 59)
        Me.cmdShowBarcode.Name = "cmdShowBarcode"
        Me.cmdShowBarcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShowBarcode.Size = New System.Drawing.Size(149, 22)
        Me.cmdShowBarcode.TabIndex = 24
        Me.cmdShowBarcode.TabStop = False
        Me.cmdShowBarcode.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.cmdShowBarcode, "Search")
        Me.cmdShowBarcode.UseVisualStyleBackColor = False
        '
        'Frabot
        '
        Me.Frabot.BackColor = System.Drawing.SystemColors.Control
        Me.Frabot.Controls.Add(Me.FraTop)
        Me.Frabot.Controls.Add(Me.Frame2)
        Me.Frabot.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frabot.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frabot.Location = New System.Drawing.Point(-2, -6)
        Me.Frabot.Name = "Frabot"
        Me.Frabot.Padding = New System.Windows.Forms.Padding(0)
        Me.Frabot.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frabot.Size = New System.Drawing.Size(912, 578)
        Me.Frabot.TabIndex = 35
        Me.Frabot.TabStop = False
        '
        'FraTop
        '
        Me.FraTop.BackColor = System.Drawing.SystemColors.Control
        Me.FraTop.Controls.Add(Me.GroupBox1)
        Me.FraTop.Controls.Add(Me.cmdsearch)
        Me.FraTop.Controls.Add(Me.txtVehicleNo)
        Me.FraTop.Controls.Add(Me.Label6)
        Me.FraTop.Controls.Add(Me.cmdSearchTrip)
        Me.FraTop.Controls.Add(Me.txtGRNo)
        Me.FraTop.Controls.Add(Me.txtGRDate)
        Me.FraTop.Controls.Add(Me.fraReceipt)
        Me.FraTop.Controls.Add(Me.txtTotPendingBills)
        Me.FraTop.Controls.Add(Me.txtTotBills)
        Me.FraTop.Controls.Add(Me.chkWOCollection)
        Me.FraTop.Controls.Add(Me.chkThirdParty)
        Me.FraTop.Controls.Add(Me.txtTripNo)
        Me.FraTop.Controls.Add(Me.txtTripDate)
        Me.FraTop.Controls.Add(Me.txtRemarks)
        Me.FraTop.Controls.Add(Me.txtVehicleType)
        Me.FraTop.Controls.Add(Me.FraShow)
        Me.FraTop.Controls.Add(Me.txtCSlipDate)
        Me.FraTop.Controls.Add(Me.txtCSlipNo)
        Me.FraTop.Controls.Add(Me.txtSlipNo)
        Me.FraTop.Controls.Add(Me.txtTransporterName)
        Me.FraTop.Controls.Add(Me.txtSlipDate)
        Me.FraTop.Controls.Add(Me.txtInDateTime)
        Me.FraTop.Controls.Add(Me.fraFreightType)
        Me.FraTop.Controls.Add(Me.Label23)
        Me.FraTop.Controls.Add(Me.Label22)
        Me.FraTop.Controls.Add(Me.Label21)
        Me.FraTop.Controls.Add(Me.Label20)
        Me.FraTop.Controls.Add(Me.lblAck)
        Me.FraTop.Controls.Add(Me.Label14)
        Me.FraTop.Controls.Add(Me.Label13)
        Me.FraTop.Controls.Add(Me.Label12)
        Me.FraTop.Controls.Add(Me.Label10)
        Me.FraTop.Controls.Add(Me.lblBookType)
        Me.FraTop.Controls.Add(Me.Label9)
        Me.FraTop.Controls.Add(Me.Label8)
        Me.FraTop.Controls.Add(Me.Label5)
        Me.FraTop.Controls.Add(Me.Label4)
        Me.FraTop.Controls.Add(Me.lblMKey)
        Me.FraTop.Controls.Add(Me.Label3)
        Me.FraTop.Controls.Add(Me.Label1)
        Me.FraTop.Controls.Add(Me.Label2)
        Me.FraTop.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTop.Location = New System.Drawing.Point(2, 0)
        Me.FraTop.Name = "FraTop"
        Me.FraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTop.Size = New System.Drawing.Size(910, 199)
        Me.FraTop.TabIndex = 37
        Me.FraTop.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.cmdShowBarcode)
        Me.GroupBox1.Controls.Add(Me.txtBarCode)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(741, 112)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(166, 87)
        Me.GroupBox1.TabIndex = 87
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Barcode"
        '
        'txtBarCode
        '
        Me.txtBarCode.AcceptsReturn = True
        Me.txtBarCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBarCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBarCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBarCode.Location = New System.Drawing.Point(8, 18)
        Me.txtBarCode.MaxLength = 0
        Me.txtBarCode.Multiline = True
        Me.txtBarCode.Name = "txtBarCode"
        Me.txtBarCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBarCode.Size = New System.Drawing.Size(149, 39)
        Me.txtBarCode.TabIndex = 19
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.AcceptsReturn = True
        Me.txtVehicleNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicleNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicleNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVehicleNo.Location = New System.Drawing.Point(126, 39)
        Me.txtVehicleNo.MaxLength = 0
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicleNo.Size = New System.Drawing.Size(173, 22)
        Me.txtVehicleNo.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(56, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Vehicle No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGRNo
        '
        Me.txtGRNo.AcceptsReturn = True
        Me.txtGRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtGRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGRNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtGRNo.Location = New System.Drawing.Point(126, 91)
        Me.txtGRNo.MaxLength = 0
        Me.txtGRNo.Name = "txtGRNo"
        Me.txtGRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRNo.Size = New System.Drawing.Size(119, 22)
        Me.txtGRNo.TabIndex = 13
        '
        'txtGRDate
        '
        Me.txtGRDate.AcceptsReturn = True
        Me.txtGRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtGRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGRDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtGRDate.Location = New System.Drawing.Point(436, 91)
        Me.txtGRDate.MaxLength = 0
        Me.txtGRDate.Name = "txtGRDate"
        Me.txtGRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRDate.Size = New System.Drawing.Size(105, 22)
        Me.txtGRDate.TabIndex = 14
        '
        'fraReceipt
        '
        Me.fraReceipt.BackColor = System.Drawing.SystemColors.Control
        Me.fraReceipt.Controls.Add(Me.chkAckReceipt)
        Me.fraReceipt.Controls.Add(Me.txtAckDate)
        Me.fraReceipt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraReceipt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraReceipt.Location = New System.Drawing.Point(610, 6)
        Me.fraReceipt.Name = "fraReceipt"
        Me.fraReceipt.Padding = New System.Windows.Forms.Padding(0)
        Me.fraReceipt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraReceipt.Size = New System.Drawing.Size(290, 53)
        Me.fraReceipt.TabIndex = 62
        Me.fraReceipt.TabStop = False
        Me.fraReceipt.Text = "Acknowledgement Receipt Date && Time"
        '
        'chkAckReceipt
        '
        Me.chkAckReceipt.BackColor = System.Drawing.SystemColors.Control
        Me.chkAckReceipt.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAckReceipt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAckReceipt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAckReceipt.Location = New System.Drawing.Point(6, 12)
        Me.chkAckReceipt.Name = "chkAckReceipt"
        Me.chkAckReceipt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAckReceipt.Size = New System.Drawing.Size(133, 16)
        Me.chkAckReceipt.TabIndex = 64
        Me.chkAckReceipt.Text = " Receipt (Yes / No)"
        Me.chkAckReceipt.UseVisualStyleBackColor = False
        '
        'txtAckDate
        '
        Me.txtAckDate.AllowPromptAsInput = False
        Me.txtAckDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAckDate.Location = New System.Drawing.Point(4, 28)
        Me.txtAckDate.Mask = "##/##/#### ##:##"
        Me.txtAckDate.Name = "txtAckDate"
        Me.txtAckDate.Size = New System.Drawing.Size(113, 22)
        Me.txtAckDate.TabIndex = 63
        '
        'txtTotPendingBills
        '
        Me.txtTotPendingBills.AcceptsReturn = True
        Me.txtTotPendingBills.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotPendingBills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotPendingBills.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotPendingBills.Enabled = False
        Me.txtTotPendingBills.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotPendingBills.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotPendingBills.Location = New System.Drawing.Point(378, 173)
        Me.txtTotPendingBills.MaxLength = 0
        Me.txtTotPendingBills.Multiline = True
        Me.txtTotPendingBills.Name = "txtTotPendingBills"
        Me.txtTotPendingBills.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotPendingBills.Size = New System.Drawing.Size(51, 19)
        Me.txtTotPendingBills.TabIndex = 59
        '
        'txtTotBills
        '
        Me.txtTotBills.AcceptsReturn = True
        Me.txtTotBills.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotBills.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotBills.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotBills.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotBills.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotBills.Location = New System.Drawing.Point(126, 173)
        Me.txtTotBills.MaxLength = 0
        Me.txtTotBills.Multiline = True
        Me.txtTotBills.Name = "txtTotBills"
        Me.txtTotBills.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotBills.Size = New System.Drawing.Size(49, 19)
        Me.txtTotBills.TabIndex = 57
        '
        'chkWOCollection
        '
        Me.chkWOCollection.BackColor = System.Drawing.SystemColors.Control
        Me.chkWOCollection.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWOCollection.Enabled = False
        Me.chkWOCollection.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWOCollection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWOCollection.Location = New System.Drawing.Point(777, 93)
        Me.chkWOCollection.Name = "chkWOCollection"
        Me.chkWOCollection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWOCollection.Size = New System.Drawing.Size(113, 17)
        Me.chkWOCollection.TabIndex = 22
        Me.chkWOCollection.Text = "W/o Collection"
        Me.chkWOCollection.UseVisualStyleBackColor = False
        Me.chkWOCollection.Visible = False
        '
        'chkThirdParty
        '
        Me.chkThirdParty.AutoSize = True
        Me.chkThirdParty.BackColor = System.Drawing.SystemColors.Control
        Me.chkThirdParty.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkThirdParty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkThirdParty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkThirdParty.Location = New System.Drawing.Point(550, 93)
        Me.chkThirdParty.Name = "chkThirdParty"
        Me.chkThirdParty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkThirdParty.Size = New System.Drawing.Size(187, 17)
        Me.chkThirdParty.TabIndex = 10
        Me.chkThirdParty.Text = "Freight not to paid - Third Party"
        Me.chkThirdParty.UseVisualStyleBackColor = False
        '
        'txtTripNo
        '
        Me.txtTripNo.AcceptsReturn = True
        Me.txtTripNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTripNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTripNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTripNo.Enabled = False
        Me.txtTripNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTripNo.Location = New System.Drawing.Point(126, 39)
        Me.txtTripNo.MaxLength = 0
        Me.txtTripNo.Name = "txtTripNo"
        Me.txtTripNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTripNo.Size = New System.Drawing.Size(101, 22)
        Me.txtTripNo.TabIndex = 6
        Me.txtTripNo.Visible = False
        '
        'txtTripDate
        '
        Me.txtTripDate.AcceptsReturn = True
        Me.txtTripDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtTripDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTripDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTripDate.Enabled = False
        Me.txtTripDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTripDate.Location = New System.Drawing.Point(436, 39)
        Me.txtTripDate.MaxLength = 0
        Me.txtTripDate.Name = "txtTripDate"
        Me.txtTripDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTripDate.Size = New System.Drawing.Size(105, 22)
        Me.txtTripDate.TabIndex = 7
        Me.txtTripDate.Visible = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(126, 148)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(303, 19)
        Me.txtRemarks.TabIndex = 18
        '
        'txtVehicleType
        '
        Me.txtVehicleType.AcceptsReturn = True
        Me.txtVehicleType.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicleType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicleType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicleType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVehicleType.Location = New System.Drawing.Point(436, 65)
        Me.txtVehicleType.MaxLength = 0
        Me.txtVehicleType.Name = "txtVehicleType"
        Me.txtVehicleType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicleType.Size = New System.Drawing.Size(105, 22)
        Me.txtVehicleType.TabIndex = 12
        '
        'FraShow
        '
        Me.FraShow.BackColor = System.Drawing.SystemColors.Control
        Me.FraShow.Controls.Add(Me.cmdPopulateBillAll)
        Me.FraShow.Controls.Add(Me._optShow_4)
        Me.FraShow.Controls.Add(Me._optShow_3)
        Me.FraShow.Controls.Add(Me._optShow_2)
        Me.FraShow.Controls.Add(Me.cmdPopulateSuppBill)
        Me.FraShow.Controls.Add(Me.txtRefNo)
        Me.FraShow.Controls.Add(Me._optShow_1)
        Me.FraShow.Controls.Add(Me._optShow_0)
        Me.FraShow.Controls.Add(Me.Label7)
        Me.FraShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraShow.Location = New System.Drawing.Point(436, 112)
        Me.FraShow.Name = "FraShow"
        Me.FraShow.Padding = New System.Windows.Forms.Padding(0)
        Me.FraShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraShow.Size = New System.Drawing.Size(301, 87)
        Me.FraShow.TabIndex = 45
        Me.FraShow.TabStop = False
        Me.FraShow.Text = "Show From"
        '
        '_optShow_4
        '
        Me._optShow_4.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_4, CType(4, Short))
        Me._optShow_4.Location = New System.Drawing.Point(16, 38)
        Me._optShow_4.Name = "_optShow_4"
        Me._optShow_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_4.Size = New System.Drawing.Size(113, 20)
        Me._optShow_4.TabIndex = 87
        Me._optShow_4.Text = "Vendor Rejection"
        Me._optShow_4.UseVisualStyleBackColor = False
        '
        '_optShow_3
        '
        Me._optShow_3.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_3.Enabled = False
        Me._optShow_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_3, CType(3, Short))
        Me._optShow_3.Location = New System.Drawing.Point(171, 15)
        Me._optShow_3.Name = "_optShow_3"
        Me._optShow_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_3.Size = New System.Drawing.Size(51, 20)
        Me._optShow_3.TabIndex = 85
        Me._optShow_3.Text = "Gate"
        Me._optShow_3.UseVisualStyleBackColor = False
        Me._optShow_3.Visible = False
        '
        '_optShow_2
        '
        Me._optShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_2.Enabled = False
        Me._optShow_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_2, CType(2, Short))
        Me._optShow_2.Location = New System.Drawing.Point(139, 38)
        Me._optShow_2.Name = "_optShow_2"
        Me._optShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_2.Size = New System.Drawing.Size(51, 21)
        Me._optShow_2.TabIndex = 54
        Me._optShow_2.Text = "MRR"
        Me._optShow_2.UseVisualStyleBackColor = False
        Me._optShow_2.Visible = False
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRefNo.Location = New System.Drawing.Point(73, 60)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(119, 22)
        Me.txtRefNo.TabIndex = 21
        '
        '_optShow_1
        '
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(79, 15)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(103, 21)
        Me._optShow_1.TabIndex = 20
        Me._optShow_1.Text = "RGP / NRGP"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        '_optShow_0
        '
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Checked = True
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(16, 15)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(72, 21)
        Me._optShow_0.TabIndex = 19
        Me._optShow_0.TabStop = True
        Me._optShow_0.Text = "Invoice"
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(16, 62)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "Ref No :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCSlipDate
        '
        Me.txtCSlipDate.AcceptsReturn = True
        Me.txtCSlipDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCSlipDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCSlipDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCSlipDate.Enabled = False
        Me.txtCSlipDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSlipDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCSlipDate.Location = New System.Drawing.Point(436, 39)
        Me.txtCSlipDate.MaxLength = 0
        Me.txtCSlipDate.Name = "txtCSlipDate"
        Me.txtCSlipDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCSlipDate.Size = New System.Drawing.Size(105, 22)
        Me.txtCSlipDate.TabIndex = 5
        Me.txtCSlipDate.Visible = False
        '
        'txtCSlipNo
        '
        Me.txtCSlipNo.AcceptsReturn = True
        Me.txtCSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCSlipNo.Enabled = False
        Me.txtCSlipNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCSlipNo.Location = New System.Drawing.Point(126, 39)
        Me.txtCSlipNo.MaxLength = 0
        Me.txtCSlipNo.Name = "txtCSlipNo"
        Me.txtCSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCSlipNo.Size = New System.Drawing.Size(101, 22)
        Me.txtCSlipNo.TabIndex = 3
        Me.txtCSlipNo.Visible = False
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(126, 13)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(101, 22)
        Me.txtSlipNo.TabIndex = 1
        '
        'txtTransporterName
        '
        Me.txtTransporterName.AcceptsReturn = True
        Me.txtTransporterName.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransporterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransporterName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransporterName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTransporterName.Location = New System.Drawing.Point(126, 65)
        Me.txtTransporterName.MaxLength = 0
        Me.txtTransporterName.Name = "txtTransporterName"
        Me.txtTransporterName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransporterName.Size = New System.Drawing.Size(173, 22)
        Me.txtTransporterName.TabIndex = 11
        '
        'txtSlipDate
        '
        Me.txtSlipDate.AcceptsReturn = True
        Me.txtSlipDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipDate.Enabled = False
        Me.txtSlipDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipDate.Location = New System.Drawing.Point(436, 13)
        Me.txtSlipDate.MaxLength = 0
        Me.txtSlipDate.Name = "txtSlipDate"
        Me.txtSlipDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipDate.Size = New System.Drawing.Size(105, 22)
        Me.txtSlipDate.TabIndex = 2
        '
        'txtInDateTime
        '
        Me.txtInDateTime.AllowPromptAsInput = False
        Me.txtInDateTime.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInDateTime.Location = New System.Drawing.Point(712, 62)
        Me.txtInDateTime.Mask = "##/##/#### ##:##"
        Me.txtInDateTime.Name = "txtInDateTime"
        Me.txtInDateTime.Size = New System.Drawing.Size(132, 22)
        Me.txtInDateTime.TabIndex = 83
        '
        'fraFreightType
        '
        Me.fraFreightType.BackColor = System.Drawing.SystemColors.Control
        Me.fraFreightType.Controls.Add(Me._optFreightType_0)
        Me.fraFreightType.Controls.Add(Me._optFreightType_1)
        Me.fraFreightType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFreightType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraFreightType.Location = New System.Drawing.Point(126, 112)
        Me.fraFreightType.Name = "fraFreightType"
        Me.fraFreightType.Padding = New System.Windows.Forms.Padding(0)
        Me.fraFreightType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraFreightType.Size = New System.Drawing.Size(303, 32)
        Me.fraFreightType.TabIndex = 15
        Me.fraFreightType.TabStop = False
        '
        '_optFreightType_0
        '
        Me._optFreightType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optFreightType_0.Checked = True
        Me._optFreightType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFreightType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFreightType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFreightType.SetIndex(Me._optFreightType_0, CType(0, Short))
        Me._optFreightType_0.Location = New System.Drawing.Point(54, 10)
        Me._optFreightType_0.Name = "_optFreightType_0"
        Me._optFreightType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFreightType_0.Size = New System.Drawing.Size(71, 16)
        Me._optFreightType_0.TabIndex = 16
        Me._optFreightType_0.TabStop = True
        Me._optFreightType_0.Text = "Regular"
        Me._optFreightType_0.UseVisualStyleBackColor = False
        '
        '_optFreightType_1
        '
        Me._optFreightType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optFreightType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFreightType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFreightType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFreightType.SetIndex(Me._optFreightType_1, CType(1, Short))
        Me._optFreightType_1.Location = New System.Drawing.Point(170, 10)
        Me._optFreightType_1.Name = "_optFreightType_1"
        Me._optFreightType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFreightType_1.Size = New System.Drawing.Size(89, 16)
        Me._optFreightType_1.TabIndex = 17
        Me._optFreightType_1.TabStop = True
        Me._optFreightType_1.Text = "Premium"
        Me._optFreightType_1.UseVisualStyleBackColor = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(46, 124)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(76, 13)
        Me.Label23.TabIndex = 86
        Me.Label23.Text = "Freight Type :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(610, 64)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(100, 13)
        Me.Label22.TabIndex = 84
        Me.Label22.Text = "In Date && In Time :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(76, 95)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(46, 13)
        Me.Label21.TabIndex = 82
        Me.Label21.Text = "GR No :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(377, 95)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(55, 13)
        Me.Label20.TabIndex = 81
        Me.Label20.Text = "GR Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAck
        '
        Me.lblAck.AutoSize = True
        Me.lblAck.BackColor = System.Drawing.SystemColors.Control
        Me.lblAck.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAck.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAck.Location = New System.Drawing.Point(310, 44)
        Me.lblAck.Name = "lblAck"
        Me.lblAck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAck.Size = New System.Drawing.Size(0, 13)
        Me.lblAck.TabIndex = 61
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(178, 177)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(204, 13)
        Me.Label14.TabIndex = 60
        Me.Label14.Text = "Total Pending No/s of Bills to be enter:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(18, 177)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(104, 13)
        Me.Label13.TabIndex = 58
        Me.Label13.Text = "Total No/s of Bills :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Enabled = False
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(14, 42)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(108, 13)
        Me.Label12.TabIndex = 56
        Me.Label12.Text = "TransporterTrip No :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label12.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Enabled = False
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(392, 41)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(40, 13)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = " Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label10.Visible = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(235, 16)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(103, 15)
        Me.lblBookType.TabIndex = 53
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(65, 152)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "Remarks :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(357, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "Vehicle Type :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Enabled = False
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(395, 43)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Date :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label5.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Enabled = False
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(18, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "Collection Slip No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label4.Visible = False
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(250, 96)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(49, 13)
        Me.lblMKey.TabIndex = 41
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(18, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Transporter Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(72, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Slip No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(356, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Date && Time :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.Label26)
        Me.Frame2.Controls.Add(Me.lblNetWt)
        Me.Frame2.Controls.Add(Me.txtTearWt)
        Me.Frame2.Controls.Add(Me.Label25)
        Me.Frame2.Controls.Add(Me.txtGrossWt)
        Me.Frame2.Controls.Add(Me.Label24)
        Me.Frame2.Controls.Add(Me.txtTripAmount)
        Me.Frame2.Controls.Add(Me.txtTollTax)
        Me.Frame2.Controls.Add(Me.txtNetAmount)
        Me.Frame2.Controls.Add(Me.txtOthCharges)
        Me.Frame2.Controls.Add(Me.SprdMain)
        Me.Frame2.Controls.Add(Me.SprdMainOth)
        Me.Frame2.Controls.Add(Me.lblModDate)
        Me.Frame2.Controls.Add(Me.Label48)
        Me.Frame2.Controls.Add(Me.lblAddDate)
        Me.Frame2.Controls.Add(Me.Label45)
        Me.Frame2.Controls.Add(Me.lblModUser)
        Me.Frame2.Controls.Add(Me.Label46)
        Me.Frame2.Controls.Add(Me.lblAddUser)
        Me.Frame2.Controls.Add(Me.Label44)
        Me.Frame2.Controls.Add(Me.Label19)
        Me.Frame2.Controls.Add(Me.Label18)
        Me.Frame2.Controls.Add(Me.Label17)
        Me.Frame2.Controls.Add(Me.Label15)
        Me.Frame2.Controls.Add(Me.Label11)
        Me.Frame2.Controls.Add(Me.lblPacket)
        Me.Frame2.Controls.Add(Me.Label16)
        Me.Frame2.Controls.Add(Me.lblTotItemQty)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(1, 192)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(910, 387)
        Me.Frame2.TabIndex = 36
        Me.Frame2.TabStop = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(773, 367)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(49, 13)
        Me.Label26.TabIndex = 86
        Me.Label26.Text = "Net Wt :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetWt
        '
        Me.lblNetWt.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetWt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetWt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetWt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetWt.Location = New System.Drawing.Point(828, 367)
        Me.lblNetWt.Name = "lblNetWt"
        Me.lblNetWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetWt.Size = New System.Drawing.Size(73, 17)
        Me.lblNetWt.TabIndex = 85
        Me.lblNetWt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTearWt
        '
        Me.txtTearWt.AcceptsReturn = True
        Me.txtTearWt.BackColor = System.Drawing.SystemColors.Window
        Me.txtTearWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTearWt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTearWt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTearWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTearWt.Location = New System.Drawing.Point(828, 343)
        Me.txtTearWt.MaxLength = 0
        Me.txtTearWt.Name = "txtTearWt"
        Me.txtTearWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTearWt.Size = New System.Drawing.Size(73, 22)
        Me.txtTearWt.TabIndex = 83
        Me.txtTearWt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(770, 343)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(52, 13)
        Me.Label25.TabIndex = 84
        Me.Label25.Text = "Tear Wt :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGrossWt
        '
        Me.txtGrossWt.AcceptsReturn = True
        Me.txtGrossWt.BackColor = System.Drawing.SystemColors.Window
        Me.txtGrossWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrossWt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGrossWt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrossWt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtGrossWt.Location = New System.Drawing.Point(828, 319)
        Me.txtGrossWt.MaxLength = 0
        Me.txtGrossWt.Name = "txtGrossWt"
        Me.txtGrossWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGrossWt.Size = New System.Drawing.Size(73, 22)
        Me.txtGrossWt.TabIndex = 81
        Me.txtGrossWt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(762, 323)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(60, 13)
        Me.Label24.TabIndex = 82
        Me.Label24.Text = "Gross Wt :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTripAmount
        '
        Me.txtTripAmount.AcceptsReturn = True
        Me.txtTripAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTripAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTripAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTripAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTripAmount.Location = New System.Drawing.Point(94, 319)
        Me.txtTripAmount.MaxLength = 0
        Me.txtTripAmount.Name = "txtTripAmount"
        Me.txtTripAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTripAmount.Size = New System.Drawing.Size(73, 22)
        Me.txtTripAmount.TabIndex = 68
        Me.txtTripAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTollTax
        '
        Me.txtTollTax.AcceptsReturn = True
        Me.txtTollTax.BackColor = System.Drawing.SystemColors.Window
        Me.txtTollTax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTollTax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTollTax.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTollTax.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTollTax.Location = New System.Drawing.Point(398, 319)
        Me.txtTollTax.MaxLength = 0
        Me.txtTollTax.Name = "txtTollTax"
        Me.txtTollTax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTollTax.Size = New System.Drawing.Size(59, 22)
        Me.txtTollTax.TabIndex = 67
        Me.txtTollTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNetAmount
        '
        Me.txtNetAmount.AcceptsReturn = True
        Me.txtNetAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetAmount.Enabled = False
        Me.txtNetAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNetAmount.Location = New System.Drawing.Point(538, 319)
        Me.txtNetAmount.MaxLength = 0
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetAmount.Size = New System.Drawing.Size(73, 22)
        Me.txtNetAmount.TabIndex = 66
        Me.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOthCharges
        '
        Me.txtOthCharges.AcceptsReturn = True
        Me.txtOthCharges.BackColor = System.Drawing.SystemColors.Window
        Me.txtOthCharges.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOthCharges.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOthCharges.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthCharges.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOthCharges.Location = New System.Drawing.Point(266, 319)
        Me.txtOthCharges.MaxLength = 0
        Me.txtOthCharges.Name = "txtOthCharges"
        Me.txtOthCharges.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOthCharges.Size = New System.Drawing.Size(73, 22)
        Me.txtOthCharges.TabIndex = 65
        Me.txtOthCharges.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 11)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(908, 193)
        Me.SprdMain.TabIndex = 24
        '
        'SprdMainOth
        '
        Me.SprdMainOth.DataSource = Nothing
        Me.SprdMainOth.Location = New System.Drawing.Point(0, 206)
        Me.SprdMainOth.Name = "SprdMainOth"
        Me.SprdMainOth.OcxState = CType(resources.GetObject("SprdMainOth.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainOth.Size = New System.Drawing.Size(908, 109)
        Me.SprdMainOth.TabIndex = 24
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(539, 343)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(61, 19)
        Me.lblModDate.TabIndex = 80
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(478, 343)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(61, 13)
        Me.Label48.TabIndex = 79
        Me.Label48.Text = "Mod Date:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(267, 343)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(71, 19)
        Me.lblAddDate.TabIndex = 78
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(204, 343)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(61, 13)
        Me.Label45.TabIndex = 77
        Me.Label45.Text = "Add Date :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(399, 343)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(59, 19)
        Me.lblModUser.TabIndex = 76
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(339, 343)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(60, 13)
        Me.Label46.TabIndex = 75
        Me.Label46.Text = "Mod User:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(93, 343)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(73, 19)
        Me.lblAddUser.TabIndex = 74
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(17, 343)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(60, 13)
        Me.Label44.TabIndex = 73
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(5, 323)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(92, 13)
        Me.Label19.TabIndex = 72
        Me.Label19.Text = "Freight Amount :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(341, 323)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(51, 13)
        Me.Label18.TabIndex = 71
        Me.Label18.Text = "Toll Tax :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(462, 323)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(74, 13)
        Me.Label17.TabIndex = 70
        Me.Label17.Text = "Net Amount :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(170, 323)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(90, 13)
        Me.Label15.TabIndex = 69
        Me.Label15.Text = "Others Charges :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(609, 343)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(68, 13)
        Me.Label11.TabIndex = 52
        Me.Label11.Text = "No of Pack :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPacket
        '
        Me.lblPacket.BackColor = System.Drawing.SystemColors.Control
        Me.lblPacket.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPacket.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPacket.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPacket.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPacket.Location = New System.Drawing.Point(682, 343)
        Me.lblPacket.Name = "lblPacket"
        Me.lblPacket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPacket.Size = New System.Drawing.Size(67, 17)
        Me.lblPacket.TabIndex = 51
        Me.lblPacket.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(620, 323)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(57, 13)
        Me.Label16.TabIndex = 50
        Me.Label16.Text = "Item Qty :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotItemQty
        '
        Me.lblTotItemQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotItemQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotItemQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotItemQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotItemQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotItemQty.Location = New System.Drawing.Point(682, 319)
        Me.lblTotItemQty.Name = "lblTotItemQty"
        Me.lblTotItemQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemQty.Size = New System.Drawing.Size(67, 17)
        Me.lblTotItemQty.TabIndex = 49
        Me.lblTotItemQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(908, 572)
        Me.SprdView.TabIndex = 34
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
        Me.FraCmd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCmd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraCmd.Location = New System.Drawing.Point(0, 568)
        Me.FraCmd.Name = "FraCmd"
        Me.FraCmd.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCmd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCmd.Size = New System.Drawing.Size(910, 52)
        Me.FraCmd.TabIndex = 33
        Me.FraCmd.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(22, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 33
        '
        'optFreightType
        '
        '
        'optShow
        '
        '
        'frmLoadingSlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.Frabot)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraCmd)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoadingSlip"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Loading Slip"
        Me.Frabot.ResumeLayout(False)
        Me.FraTop.ResumeLayout(False)
        Me.FraTop.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.fraReceipt.ResumeLayout(False)
        Me.fraReceipt.PerformLayout()
        Me.FraShow.ResumeLayout(False)
        Me.FraShow.PerformLayout()
        Me.fraFreightType.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMainOth, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraCmd.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optFreightType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataPPOMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Public WithEvents Label26 As Label
    Public WithEvents lblNetWt As Label
    Public WithEvents txtTearWt As TextBox
    Public WithEvents Label25 As Label
    Public WithEvents txtGrossWt As TextBox
    Public WithEvents Label24 As Label
    Public WithEvents cmdPopulateBillAll As Button
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents txtBarCode As TextBox
    Public WithEvents cmdShowBarcode As Button
#End Region
End Class