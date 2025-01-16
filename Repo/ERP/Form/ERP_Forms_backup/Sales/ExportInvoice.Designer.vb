Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmExportInvoice
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
    Public WithEvents chkDC As System.Windows.Forms.CheckBox
    Public WithEvents chkExciseInvoice As System.Windows.Forms.CheckBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtCurrency As System.Windows.Forms.TextBox
    Public WithEvents txtInvPrefix As System.Windows.Forms.TextBox
    Public WithEvents txtCurrFactor As System.Windows.Forms.TextBox
    Public WithEvents txtContainerNo As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtDestination As System.Windows.Forms.TextBox
    Public WithEvents txtOrigin As System.Windows.Forms.TextBox
    Public WithEvents txtBuyerDate As System.Windows.Forms.TextBox
    Public WithEvents txtBuyerNo As System.Windows.Forms.TextBox
    Public WithEvents txtExciseBillDate As System.Windows.Forms.TextBox
    Public WithEvents txtExciseBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtBuyerName As System.Windows.Forms.TextBox
    Public WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Public WithEvents TxtCustomerName As System.Windows.Forms.TextBox
    Public WithEvents txtInvNo As System.Windows.Forms.TextBox
    Public WithEvents txtInvDate As System.Windows.Forms.TextBox
    Public WithEvents txtPackDate As System.Windows.Forms.TextBox
    Public WithEvents txtPackNo As System.Windows.Forms.TextBox
    Public WithEvents txtIECNo As System.Windows.Forms.TextBox
    Public WithEvents cmdPackNo As System.Windows.Forms.Button
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents LblMkey As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Frasupp As System.Windows.Forms.GroupBox
    Public WithEvents txtOtherAmt As System.Windows.Forms.TextBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents txtDiscAmount As System.Windows.Forms.TextBox
    Public WithEvents txtDiscPer As System.Windows.Forms.TextBox
    Public WithEvents SprdOther As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTInfo_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtAdvLicDate As System.Windows.Forms.TextBox
    Public WithEvents txtAdvLicNo As System.Windows.Forms.TextBox
    Public WithEvents txtAgreement As System.Windows.Forms.TextBox
    Public WithEvents txtCarriage As System.Windows.Forms.TextBox
    Public WithEvents txtPlace As System.Windows.Forms.TextBox
    Public WithEvents txtFlight As System.Windows.Forms.TextBox
    Public WithEvents txtLoading As System.Windows.Forms.TextBox
    Public WithEvents txtDischarge As System.Windows.Forms.TextBox
    Public WithEvents txtFinalDestination As System.Windows.Forms.TextBox
    Public WithEvents txtPayments As System.Windows.Forms.TextBox
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents _SSTInfo_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents txtNotifyParty3 As System.Windows.Forms.TextBox
    Public WithEvents txtNotifyParty2 As System.Windows.Forms.TextBox
    Public WithEvents txtNotifyParty1 As System.Windows.Forms.TextBox
    Public WithEvents cmdBank As System.Windows.Forms.Button
    Public WithEvents txtCreditBankAddress As System.Windows.Forms.TextBox
    Public WithEvents txtADCode As System.Windows.Forms.TextBox
    Public WithEvents txtCreditBank As System.Windows.Forms.TextBox
    Public WithEvents txtCustomerBank As System.Windows.Forms.TextBox
    Public WithEvents txtAccountNo As System.Windows.Forms.TextBox
    Public WithEvents txtSwiftCode As System.Windows.Forms.TextBox
    Public WithEvents txtFurtherBank As System.Windows.Forms.TextBox
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents _SSTInfo_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents SSTInfo As System.Windows.Forms.TabControl
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents lblTotAmount As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents lblTotAmount_INR As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Frasprd As System.Windows.Forms.GroupBox
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmExportInvoice))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdPackNo = New System.Windows.Forms.Button()
        Me.cmdBank = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdsearchConsinee = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.Frasupp = New System.Windows.Forms.GroupBox()
        Me.txtConsigneeAddress = New System.Windows.Forms.TextBox()
        Me.txtBuyerAddress = New System.Windows.Forms.TextBox()
        Me.txtShipTo = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkDC = New System.Windows.Forms.CheckBox()
        Me.chkExciseInvoice = New System.Windows.Forms.CheckBox()
        Me.txtCurrency = New System.Windows.Forms.TextBox()
        Me.txtInvPrefix = New System.Windows.Forms.TextBox()
        Me.txtCurrFactor = New System.Windows.Forms.TextBox()
        Me.txtContainerNo = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtDestination = New System.Windows.Forms.TextBox()
        Me.txtOrigin = New System.Windows.Forms.TextBox()
        Me.txtBuyerDate = New System.Windows.Forms.TextBox()
        Me.txtBuyerNo = New System.Windows.Forms.TextBox()
        Me.txtExciseBillDate = New System.Windows.Forms.TextBox()
        Me.txtExciseBillNo = New System.Windows.Forms.TextBox()
        Me.txtBuyerName = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtInvNo = New System.Windows.Forms.TextBox()
        Me.txtInvDate = New System.Windows.Forms.TextBox()
        Me.txtPackDate = New System.Windows.Forms.TextBox()
        Me.txtPackNo = New System.Windows.Forms.TextBox()
        Me.txtIECNo = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblMkey = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Frasprd = New System.Windows.Forms.GroupBox()
        Me.txtOtherAmt = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.txtDiscAmount = New System.Windows.Forms.TextBox()
        Me.txtDiscPer = New System.Windows.Forms.TextBox()
        Me.SSTInfo = New System.Windows.Forms.TabControl()
        Me._SSTInfo_TabPage3 = New System.Windows.Forms.TabPage()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTInfo_TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkREXDeclaration = New System.Windows.Forms.CheckBox()
        Me.txtAdvLicDate = New System.Windows.Forms.TextBox()
        Me.txtAdvLicNo = New System.Windows.Forms.TextBox()
        Me.txtAgreement = New System.Windows.Forms.TextBox()
        Me.txtCarriage = New System.Windows.Forms.TextBox()
        Me.txtPlace = New System.Windows.Forms.TextBox()
        Me.txtFlight = New System.Windows.Forms.TextBox()
        Me.txtLoading = New System.Windows.Forms.TextBox()
        Me.txtDischarge = New System.Windows.Forms.TextBox()
        Me.txtFinalDestination = New System.Windows.Forms.TextBox()
        Me.txtPayments = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me._SSTInfo_TabPage0 = New System.Windows.Forms.TabPage()
        Me.SprdOther = New AxFPSpreadADO.AxfpSpread()
        Me._SSTInfo_TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtNotifyParty3 = New System.Windows.Forms.TextBox()
        Me.txtNotifyParty2 = New System.Windows.Forms.TextBox()
        Me.txtNotifyParty1 = New System.Windows.Forms.TextBox()
        Me.txtCreditBankAddress = New System.Windows.Forms.TextBox()
        Me.txtADCode = New System.Windows.Forms.TextBox()
        Me.txtCreditBank = New System.Windows.Forms.TextBox()
        Me.txtCustomerBank = New System.Windows.Forms.TextBox()
        Me.txtAccountNo = New System.Windows.Forms.TextBox()
        Me.txtSwiftCode = New System.Windows.Forms.TextBox()
        Me.txtFurtherBank = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblTotAmount = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.lblTotAmount_INR = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        Me.Frasupp.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frasprd.SuspendLayout()
        Me.SSTInfo.SuspendLayout()
        Me._SSTInfo_TabPage3.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTInfo_TabPage1.SuspendLayout()
        Me._SSTInfo_TabPage0.SuspendLayout()
        CType(Me.SprdOther, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTInfo_TabPage2.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdPackNo
        '
        Me.cmdPackNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPackNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPackNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPackNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPackNo.Image = CType(resources.GetObject("cmdPackNo.Image"), System.Drawing.Image)
        Me.cmdPackNo.Location = New System.Drawing.Point(240, 36)
        Me.cmdPackNo.Name = "cmdPackNo"
        Me.cmdPackNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPackNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdPackNo.TabIndex = 5
        Me.cmdPackNo.TabStop = False
        Me.cmdPackNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPackNo, "Search")
        Me.cmdPackNo.UseVisualStyleBackColor = False
        '
        'cmdBank
        '
        Me.cmdBank.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBank.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBank.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBank.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBank.Image = CType(resources.GetObject("cmdBank.Image"), System.Drawing.Image)
        Me.cmdBank.Location = New System.Drawing.Point(325, 8)
        Me.cmdBank.Name = "cmdBank"
        Me.cmdBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBank.Size = New System.Drawing.Size(23, 19)
        Me.cmdBank.TabIndex = 104
        Me.cmdBank.TabStop = False
        Me.cmdBank.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBank, "Search")
        Me.cmdBank.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(640, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 52
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(574, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 51
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(508, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 50
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(441, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 49
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(375, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 48
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(307, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 47
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(240, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 46
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(173, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 45
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(106, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdsearchConsinee
        '
        Me.cmdsearchConsinee.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchConsinee.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchConsinee.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchConsinee.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchConsinee.Image = CType(resources.GetObject("cmdsearchConsinee.Image"), System.Drawing.Image)
        Me.cmdsearchConsinee.Location = New System.Drawing.Point(457, 97)
        Me.cmdsearchConsinee.Name = "cmdsearchConsinee"
        Me.cmdsearchConsinee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchConsinee.Size = New System.Drawing.Size(24, 21)
        Me.cmdsearchConsinee.TabIndex = 94
        Me.cmdsearchConsinee.TabStop = False
        Me.cmdsearchConsinee.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchConsinee, "Search")
        Me.cmdsearchConsinee.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.Frasupp)
        Me.FraFront.Controls.Add(Me.Frasprd)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -4)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(924, 447)
        Me.FraFront.TabIndex = 55
        Me.FraFront.TabStop = False
        '
        'Frasupp
        '
        Me.Frasupp.BackColor = System.Drawing.SystemColors.Control
        Me.Frasupp.Controls.Add(Me.txtConsigneeAddress)
        Me.Frasupp.Controls.Add(Me.txtBuyerAddress)
        Me.Frasupp.Controls.Add(Me.txtShipTo)
        Me.Frasupp.Controls.Add(Me.Label18)
        Me.Frasupp.Controls.Add(Me.txtBillTo)
        Me.Frasupp.Controls.Add(Me.Label44)
        Me.Frasupp.Controls.Add(Me.cmdsearchConsinee)
        Me.Frasupp.Controls.Add(Me.Frame1)
        Me.Frasupp.Controls.Add(Me.txtCurrency)
        Me.Frasupp.Controls.Add(Me.txtInvPrefix)
        Me.Frasupp.Controls.Add(Me.txtCurrFactor)
        Me.Frasupp.Controls.Add(Me.txtContainerNo)
        Me.Frasupp.Controls.Add(Me.txtRemarks)
        Me.Frasupp.Controls.Add(Me.txtDestination)
        Me.Frasupp.Controls.Add(Me.txtOrigin)
        Me.Frasupp.Controls.Add(Me.txtBuyerDate)
        Me.Frasupp.Controls.Add(Me.txtBuyerNo)
        Me.Frasupp.Controls.Add(Me.txtExciseBillDate)
        Me.Frasupp.Controls.Add(Me.txtExciseBillNo)
        Me.Frasupp.Controls.Add(Me.txtBuyerName)
        Me.Frasupp.Controls.Add(Me.txtCustomerCode)
        Me.Frasupp.Controls.Add(Me.TxtCustomerName)
        Me.Frasupp.Controls.Add(Me.txtInvNo)
        Me.Frasupp.Controls.Add(Me.txtInvDate)
        Me.Frasupp.Controls.Add(Me.txtPackDate)
        Me.Frasupp.Controls.Add(Me.txtPackNo)
        Me.Frasupp.Controls.Add(Me.txtIECNo)
        Me.Frasupp.Controls.Add(Me.cmdPackNo)
        Me.Frasupp.Controls.Add(Me.Label25)
        Me.Frasupp.Controls.Add(Me.Label24)
        Me.Frasupp.Controls.Add(Me.Label23)
        Me.Frasupp.Controls.Add(Me.Label21)
        Me.Frasupp.Controls.Add(Me.Label20)
        Me.Frasupp.Controls.Add(Me.Label19)
        Me.Frasupp.Controls.Add(Me.Label17)
        Me.Frasupp.Controls.Add(Me.Label16)
        Me.Frasupp.Controls.Add(Me.Label13)
        Me.Frasupp.Controls.Add(Me.Label1)
        Me.Frasupp.Controls.Add(Me.Label8)
        Me.Frasupp.Controls.Add(Me.Label4)
        Me.Frasupp.Controls.Add(Me.LblMkey)
        Me.Frasupp.Controls.Add(Me.Label12)
        Me.Frasupp.Controls.Add(Me.Label11)
        Me.Frasupp.Controls.Add(Me.Label22)
        Me.Frasupp.Controls.Add(Me.Label14)
        Me.Frasupp.Controls.Add(Me.Label15)
        Me.Frasupp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasupp.Location = New System.Drawing.Point(0, 0)
        Me.Frasupp.Name = "Frasupp"
        Me.Frasupp.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasupp.Size = New System.Drawing.Size(924, 188)
        Me.Frasupp.TabIndex = 0
        Me.Frasupp.TabStop = False
        '
        'txtConsigneeAddress
        '
        Me.txtConsigneeAddress.AcceptsReturn = True
        Me.txtConsigneeAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtConsigneeAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConsigneeAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConsigneeAddress.Enabled = False
        Me.txtConsigneeAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsigneeAddress.ForeColor = System.Drawing.Color.Blue
        Me.txtConsigneeAddress.Location = New System.Drawing.Point(118, 119)
        Me.txtConsigneeAddress.MaxLength = 0
        Me.txtConsigneeAddress.Name = "txtConsigneeAddress"
        Me.txtConsigneeAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtConsigneeAddress.Size = New System.Drawing.Size(526, 20)
        Me.txtConsigneeAddress.TabIndex = 14
        '
        'txtBuyerAddress
        '
        Me.txtBuyerAddress.AcceptsReturn = True
        Me.txtBuyerAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtBuyerAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBuyerAddress.Enabled = False
        Me.txtBuyerAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuyerAddress.ForeColor = System.Drawing.Color.Blue
        Me.txtBuyerAddress.Location = New System.Drawing.Point(118, 77)
        Me.txtBuyerAddress.MaxLength = 0
        Me.txtBuyerAddress.Name = "txtBuyerAddress"
        Me.txtBuyerAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBuyerAddress.Size = New System.Drawing.Size(526, 20)
        Me.txtBuyerAddress.TabIndex = 9
        '
        'txtShipTo
        '
        Me.txtShipTo.AcceptsReturn = True
        Me.txtShipTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtShipTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShipTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShipTo.Enabled = False
        Me.txtShipTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtShipTo.Location = New System.Drawing.Point(546, 98)
        Me.txtShipTo.MaxLength = 0
        Me.txtShipTo.Name = "txtShipTo"
        Me.txtShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShipTo.Size = New System.Drawing.Size(97, 20)
        Me.txtShipTo.TabIndex = 12
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(489, 100)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(54, 14)
        Me.Label18.TabIndex = 122
        Me.Label18.Text = "Location :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label18.Visible = False
        '
        'txtBillTo
        '
        Me.txtBillTo.AcceptsReturn = True
        Me.txtBillTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillTo.Enabled = False
        Me.txtBillTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillTo.Location = New System.Drawing.Point(546, 56)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(97, 20)
        Me.txtBillTo.TabIndex = 7
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(490, 59)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(54, 14)
        Me.Label44.TabIndex = 121
        Me.Label44.Text = "Location :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label44.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkDC)
        Me.Frame1.Controls.Add(Me.chkExciseInvoice)
        Me.Frame1.Enabled = False
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(695, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(229, 35)
        Me.Frame1.TabIndex = 93
        Me.Frame1.TabStop = False
        '
        'chkDC
        '
        Me.chkDC.BackColor = System.Drawing.SystemColors.Control
        Me.chkDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDC.Location = New System.Drawing.Point(4, 14)
        Me.chkDC.Name = "chkDC"
        Me.chkDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDC.Size = New System.Drawing.Size(111, 15)
        Me.chkDC.TabIndex = 95
        Me.chkDC.Text = "Despatch Note"
        Me.chkDC.UseVisualStyleBackColor = False
        '
        'chkExciseInvoice
        '
        Me.chkExciseInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.chkExciseInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExciseInvoice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExciseInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExciseInvoice.Location = New System.Drawing.Point(116, 14)
        Me.chkExciseInvoice.Name = "chkExciseInvoice"
        Me.chkExciseInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExciseInvoice.Size = New System.Drawing.Size(111, 15)
        Me.chkExciseInvoice.TabIndex = 94
        Me.chkExciseInvoice.Text = "Excise Invoice"
        Me.chkExciseInvoice.UseVisualStyleBackColor = False
        '
        'txtCurrency
        '
        Me.txtCurrency.AcceptsReturn = True
        Me.txtCurrency.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurrency.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrency.ForeColor = System.Drawing.Color.Blue
        Me.txtCurrency.Location = New System.Drawing.Point(854, 136)
        Me.txtCurrency.MaxLength = 0
        Me.txtCurrency.Name = "txtCurrency"
        Me.txtCurrency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurrency.Size = New System.Drawing.Size(65, 20)
        Me.txtCurrency.TabIndex = 19
        '
        'txtInvPrefix
        '
        Me.txtInvPrefix.AcceptsReturn = True
        Me.txtInvPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvPrefix.Enabled = False
        Me.txtInvPrefix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvPrefix.ForeColor = System.Drawing.Color.Blue
        Me.txtInvPrefix.Location = New System.Drawing.Point(118, 14)
        Me.txtInvPrefix.MaxLength = 0
        Me.txtInvPrefix.Name = "txtInvPrefix"
        Me.txtInvPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvPrefix.Size = New System.Drawing.Size(59, 20)
        Me.txtInvPrefix.TabIndex = 0
        '
        'txtCurrFactor
        '
        Me.txtCurrFactor.AcceptsReturn = True
        Me.txtCurrFactor.BackColor = System.Drawing.SystemColors.Window
        Me.txtCurrFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrFactor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurrFactor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCurrFactor.ForeColor = System.Drawing.Color.Blue
        Me.txtCurrFactor.Location = New System.Drawing.Point(744, 136)
        Me.txtCurrFactor.MaxLength = 0
        Me.txtCurrFactor.Name = "txtCurrFactor"
        Me.txtCurrFactor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurrFactor.Size = New System.Drawing.Size(109, 20)
        Me.txtCurrFactor.TabIndex = 18
        '
        'txtContainerNo
        '
        Me.txtContainerNo.AcceptsReturn = True
        Me.txtContainerNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtContainerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContainerNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtContainerNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContainerNo.ForeColor = System.Drawing.Color.Blue
        Me.txtContainerNo.Location = New System.Drawing.Point(744, 76)
        Me.txtContainerNo.MaxLength = 0
        Me.txtContainerNo.Name = "txtContainerNo"
        Me.txtContainerNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtContainerNo.Size = New System.Drawing.Size(175, 20)
        Me.txtContainerNo.TabIndex = 10
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtRemarks.Location = New System.Drawing.Point(580, 157)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(339, 20)
        Me.txtRemarks.TabIndex = 22
        '
        'txtDestination
        '
        Me.txtDestination.AcceptsReturn = True
        Me.txtDestination.BackColor = System.Drawing.SystemColors.Window
        Me.txtDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDestination.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDestination.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDestination.ForeColor = System.Drawing.Color.Blue
        Me.txtDestination.Location = New System.Drawing.Point(744, 116)
        Me.txtDestination.MaxLength = 0
        Me.txtDestination.Name = "txtDestination"
        Me.txtDestination.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDestination.Size = New System.Drawing.Size(175, 20)
        Me.txtDestination.TabIndex = 15
        '
        'txtOrigin
        '
        Me.txtOrigin.AcceptsReturn = True
        Me.txtOrigin.BackColor = System.Drawing.SystemColors.Window
        Me.txtOrigin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrigin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOrigin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrigin.ForeColor = System.Drawing.Color.Blue
        Me.txtOrigin.Location = New System.Drawing.Point(744, 96)
        Me.txtOrigin.MaxLength = 0
        Me.txtOrigin.Name = "txtOrigin"
        Me.txtOrigin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOrigin.Size = New System.Drawing.Size(175, 20)
        Me.txtOrigin.TabIndex = 13
        '
        'txtBuyerDate
        '
        Me.txtBuyerDate.AcceptsReturn = True
        Me.txtBuyerDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBuyerDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBuyerDate.Enabled = False
        Me.txtBuyerDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuyerDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBuyerDate.Location = New System.Drawing.Point(376, 162)
        Me.txtBuyerDate.MaxLength = 0
        Me.txtBuyerDate.Name = "txtBuyerDate"
        Me.txtBuyerDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBuyerDate.Size = New System.Drawing.Size(81, 20)
        Me.txtBuyerDate.TabIndex = 21
        '
        'txtBuyerNo
        '
        Me.txtBuyerNo.AcceptsReturn = True
        Me.txtBuyerNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBuyerNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBuyerNo.Enabled = False
        Me.txtBuyerNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuyerNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBuyerNo.Location = New System.Drawing.Point(118, 162)
        Me.txtBuyerNo.MaxLength = 0
        Me.txtBuyerNo.Name = "txtBuyerNo"
        Me.txtBuyerNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBuyerNo.Size = New System.Drawing.Size(121, 20)
        Me.txtBuyerNo.TabIndex = 20
        '
        'txtExciseBillDate
        '
        Me.txtExciseBillDate.AcceptsReturn = True
        Me.txtExciseBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtExciseBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExciseBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExciseBillDate.Enabled = False
        Me.txtExciseBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExciseBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtExciseBillDate.Location = New System.Drawing.Point(376, 142)
        Me.txtExciseBillDate.MaxLength = 0
        Me.txtExciseBillDate.Name = "txtExciseBillDate"
        Me.txtExciseBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExciseBillDate.Size = New System.Drawing.Size(81, 20)
        Me.txtExciseBillDate.TabIndex = 17
        '
        'txtExciseBillNo
        '
        Me.txtExciseBillNo.AcceptsReturn = True
        Me.txtExciseBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtExciseBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExciseBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExciseBillNo.Enabled = False
        Me.txtExciseBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExciseBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtExciseBillNo.Location = New System.Drawing.Point(118, 142)
        Me.txtExciseBillNo.MaxLength = 0
        Me.txtExciseBillNo.Name = "txtExciseBillNo"
        Me.txtExciseBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExciseBillNo.Size = New System.Drawing.Size(121, 20)
        Me.txtExciseBillNo.TabIndex = 16
        '
        'txtBuyerName
        '
        Me.txtBuyerName.AcceptsReturn = True
        Me.txtBuyerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBuyerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuyerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBuyerName.Enabled = False
        Me.txtBuyerName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuyerName.ForeColor = System.Drawing.Color.Blue
        Me.txtBuyerName.Location = New System.Drawing.Point(118, 56)
        Me.txtBuyerName.MaxLength = 0
        Me.txtBuyerName.Name = "txtBuyerName"
        Me.txtBuyerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBuyerName.Size = New System.Drawing.Size(339, 20)
        Me.txtBuyerName.TabIndex = 6
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.AcceptsReturn = True
        Me.txtCustomerCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerCode.Enabled = False
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Blue
        Me.txtCustomerCode.Location = New System.Drawing.Point(744, 56)
        Me.txtCustomerCode.MaxLength = 0
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerCode.Size = New System.Drawing.Size(175, 20)
        Me.txtCustomerCode.TabIndex = 8
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.AcceptsReturn = True
        Me.TxtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCustomerName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustomerName.ForeColor = System.Drawing.Color.Blue
        Me.TxtCustomerName.Location = New System.Drawing.Point(118, 98)
        Me.TxtCustomerName.MaxLength = 0
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCustomerName.Size = New System.Drawing.Size(339, 20)
        Me.TxtCustomerName.TabIndex = 11
        '
        'txtInvNo
        '
        Me.txtInvNo.AcceptsReturn = True
        Me.txtInvNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvNo.ForeColor = System.Drawing.Color.Blue
        Me.txtInvNo.Location = New System.Drawing.Point(176, 14)
        Me.txtInvNo.MaxLength = 0
        Me.txtInvNo.Name = "txtInvNo"
        Me.txtInvNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvNo.Size = New System.Drawing.Size(63, 20)
        Me.txtInvNo.TabIndex = 1
        '
        'txtInvDate
        '
        Me.txtInvDate.AcceptsReturn = True
        Me.txtInvDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvDate.ForeColor = System.Drawing.Color.Blue
        Me.txtInvDate.Location = New System.Drawing.Point(376, 14)
        Me.txtInvDate.MaxLength = 0
        Me.txtInvDate.Name = "txtInvDate"
        Me.txtInvDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvDate.Size = New System.Drawing.Size(81, 20)
        Me.txtInvDate.TabIndex = 2
        '
        'txtPackDate
        '
        Me.txtPackDate.AcceptsReturn = True
        Me.txtPackDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPackDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPackDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPackDate.Enabled = False
        Me.txtPackDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackDate.ForeColor = System.Drawing.Color.Blue
        Me.txtPackDate.Location = New System.Drawing.Point(376, 36)
        Me.txtPackDate.MaxLength = 0
        Me.txtPackDate.Name = "txtPackDate"
        Me.txtPackDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPackDate.Size = New System.Drawing.Size(81, 20)
        Me.txtPackDate.TabIndex = 4
        '
        'txtPackNo
        '
        Me.txtPackNo.AcceptsReturn = True
        Me.txtPackNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPackNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPackNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPackNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPackNo.Location = New System.Drawing.Point(118, 36)
        Me.txtPackNo.MaxLength = 0
        Me.txtPackNo.Name = "txtPackNo"
        Me.txtPackNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPackNo.Size = New System.Drawing.Size(121, 20)
        Me.txtPackNo.TabIndex = 3
        '
        'txtIECNo
        '
        Me.txtIECNo.AcceptsReturn = True
        Me.txtIECNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtIECNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIECNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIECNo.Enabled = False
        Me.txtIECNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIECNo.ForeColor = System.Drawing.Color.Blue
        Me.txtIECNo.Location = New System.Drawing.Point(744, 36)
        Me.txtIECNo.MaxLength = 0
        Me.txtIECNo.Name = "txtIECNo"
        Me.txtIECNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIECNo.Size = New System.Drawing.Size(175, 20)
        Me.txtIECNo.TabIndex = 5
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(645, 138)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(92, 14)
        Me.Label25.TabIndex = 85
        Me.Label25.Text = "Currency Factor :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(659, 78)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(78, 14)
        Me.Label24.TabIndex = 84
        Me.Label24.Text = "Container No. :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(520, 159)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(55, 14)
        Me.Label23.TabIndex = 83
        Me.Label23.Text = "Remarks :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(671, 118)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(66, 14)
        Me.Label21.TabIndex = 81
        Me.Label21.Text = "Destination :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(655, 98)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(82, 14)
        Me.Label20.TabIndex = 80
        Me.Label20.Text = "Origin Country :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(332, 164)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(35, 14)
        Me.Label19.TabIndex = 79
        Me.Label19.Text = "Date :"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(24, 164)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(89, 14)
        Me.Label17.TabIndex = 78
        Me.Label17.Text = "Buyer Order No :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(332, 144)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(35, 14)
        Me.Label16.TabIndex = 77
        Me.Label16.Text = "Date :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(36, 144)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(77, 14)
        Me.Label13.TabIndex = 76
        Me.Label13.Text = "Excise Bill No :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(38, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(75, 14)
        Me.Label1.TabIndex = 65
        Me.Label1.Text = "Buyer Name  :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(699, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(38, 14)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Code :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(49, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(64, 14)
        Me.Label4.TabIndex = 59
        Me.Label4.Text = "Consignee :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblMkey
        '
        Me.LblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMkey.Location = New System.Drawing.Point(264, 14)
        Me.LblMkey.Name = "LblMkey"
        Me.LblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMkey.Size = New System.Drawing.Size(31, 11)
        Me.LblMkey.TabIndex = 58
        Me.LblMkey.Text = "MKEY"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(332, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(35, 14)
        Me.Label12.TabIndex = 75
        Me.Label12.Text = "Date :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(50, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(63, 14)
        Me.Label11.TabIndex = 74
        Me.Label11.Text = "Invoice No :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(690, 38)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(47, 14)
        Me.Label22.TabIndex = 82
        Me.Label22.Text = "IEC No. :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(47, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(66, 14)
        Me.Label14.TabIndex = 61
        Me.Label14.Text = "Packing No :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(334, 38)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(35, 14)
        Me.Label15.TabIndex = 60
        Me.Label15.Text = "Date :"
        '
        'Frasprd
        '
        Me.Frasprd.BackColor = System.Drawing.SystemColors.Control
        Me.Frasprd.Controls.Add(Me.txtOtherAmt)
        Me.Frasprd.Controls.Add(Me.chkCancelled)
        Me.Frasprd.Controls.Add(Me.txtDiscAmount)
        Me.Frasprd.Controls.Add(Me.txtDiscPer)
        Me.Frasprd.Controls.Add(Me.SSTInfo)
        Me.Frasprd.Controls.Add(Me.Label43)
        Me.Frasprd.Controls.Add(Me.Label39)
        Me.Frasprd.Controls.Add(Me.Label38)
        Me.Frasprd.Controls.Add(Me.lblTotAmount)
        Me.Frasprd.Controls.Add(Me.Label31)
        Me.Frasprd.Controls.Add(Me.lblTotAmount_INR)
        Me.Frasprd.Controls.Add(Me.Label29)
        Me.Frasprd.Controls.Add(Me.lblTotQty)
        Me.Frasprd.Controls.Add(Me.Label28)
        Me.Frasprd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasprd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasprd.Location = New System.Drawing.Point(0, 185)
        Me.Frasprd.Name = "Frasprd"
        Me.Frasprd.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasprd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasprd.Size = New System.Drawing.Size(924, 261)
        Me.Frasprd.TabIndex = 63
        Me.Frasprd.TabStop = False
        '
        'txtOtherAmt
        '
        Me.txtOtherAmt.AcceptsReturn = True
        Me.txtOtherAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtOtherAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOtherAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOtherAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtherAmt.ForeColor = System.Drawing.Color.Blue
        Me.txtOtherAmt.Location = New System.Drawing.Point(388, 215)
        Me.txtOtherAmt.MaxLength = 0
        Me.txtOtherAmt.Name = "txtOtherAmt"
        Me.txtOtherAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOtherAmt.Size = New System.Drawing.Size(99, 20)
        Me.txtOtherAmt.TabIndex = 1
        Me.txtOtherAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.Red
        Me.chkCancelled.Location = New System.Drawing.Point(6, 215)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(84, 20)
        Me.chkCancelled.TabIndex = 110
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'txtDiscAmount
        '
        Me.txtDiscAmount.AcceptsReturn = True
        Me.txtDiscAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiscAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDiscAmount.Enabled = False
        Me.txtDiscAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtDiscAmount.Location = New System.Drawing.Point(648, 215)
        Me.txtDiscAmount.MaxLength = 0
        Me.txtDiscAmount.Name = "txtDiscAmount"
        Me.txtDiscAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDiscAmount.Size = New System.Drawing.Size(99, 20)
        Me.txtDiscAmount.TabIndex = 2
        Me.txtDiscAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDiscPer
        '
        Me.txtDiscPer.AcceptsReturn = True
        Me.txtDiscPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiscPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDiscPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscPer.ForeColor = System.Drawing.Color.Blue
        Me.txtDiscPer.Location = New System.Drawing.Point(564, 215)
        Me.txtDiscPer.MaxLength = 0
        Me.txtDiscPer.Name = "txtDiscPer"
        Me.txtDiscPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDiscPer.Size = New System.Drawing.Size(37, 20)
        Me.txtDiscPer.TabIndex = 42
        Me.txtDiscPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SSTInfo
        '
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage3)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage1)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage0)
        Me.SSTInfo.Controls.Add(Me._SSTInfo_TabPage2)
        Me.SSTInfo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.SSTInfo.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTInfo.Location = New System.Drawing.Point(2, 8)
        Me.SSTInfo.Name = "SSTInfo"
        Me.SSTInfo.SelectedIndex = 0
        Me.SSTInfo.Size = New System.Drawing.Size(922, 206)
        Me.SSTInfo.TabIndex = 0
        '
        '_SSTInfo_TabPage3
        '
        Me._SSTInfo_TabPage3.Controls.Add(Me.SprdMain)
        Me._SSTInfo_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage3.Name = "_SSTInfo_TabPage3"
        Me._SSTInfo_TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me._SSTInfo_TabPage3.Size = New System.Drawing.Size(914, 180)
        Me._SSTInfo_TabPage3.TabIndex = 3
        Me._SSTInfo_TabPage3.Text = "Packing Details"
        Me._SSTInfo_TabPage3.UseVisualStyleBackColor = True
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(3, 3)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(908, 174)
        Me.SprdMain.TabIndex = 21
        '
        '_SSTInfo_TabPage1
        '
        Me._SSTInfo_TabPage1.Controls.Add(Me.chkREXDeclaration)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtAdvLicDate)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtAdvLicNo)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtAgreement)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtCarriage)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtPlace)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtFlight)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtLoading)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtDischarge)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtFinalDestination)
        Me._SSTInfo_TabPage1.Controls.Add(Me.txtPayments)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label36)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label35)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label26)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label2)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label3)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label5)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label6)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label7)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label9)
        Me._SSTInfo_TabPage1.Controls.Add(Me.Label10)
        Me._SSTInfo_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage1.Name = "_SSTInfo_TabPage1"
        Me._SSTInfo_TabPage1.Size = New System.Drawing.Size(914, 180)
        Me._SSTInfo_TabPage1.TabIndex = 1
        Me._SSTInfo_TabPage1.Text = "Terms && Conditions"
        '
        'chkREXDeclaration
        '
        Me.chkREXDeclaration.AutoSize = True
        Me.chkREXDeclaration.BackColor = System.Drawing.SystemColors.Control
        Me.chkREXDeclaration.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkREXDeclaration.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkREXDeclaration.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkREXDeclaration.Location = New System.Drawing.Point(138, 8)
        Me.chkREXDeclaration.Name = "chkREXDeclaration"
        Me.chkREXDeclaration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkREXDeclaration.Size = New System.Drawing.Size(130, 20)
        Me.chkREXDeclaration.TabIndex = 103
        Me.chkREXDeclaration.Text = "REX Declaration"
        Me.chkREXDeclaration.UseVisualStyleBackColor = False
        '
        'txtAdvLicDate
        '
        Me.txtAdvLicDate.AcceptsReturn = True
        Me.txtAdvLicDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvLicDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvLicDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvLicDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvLicDate.ForeColor = System.Drawing.Color.Blue
        Me.txtAdvLicDate.Location = New System.Drawing.Point(277, 142)
        Me.txtAdvLicDate.MaxLength = 0
        Me.txtAdvLicDate.Name = "txtAdvLicDate"
        Me.txtAdvLicDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvLicDate.Size = New System.Drawing.Size(81, 20)
        Me.txtAdvLicDate.TabIndex = 31
        '
        'txtAdvLicNo
        '
        Me.txtAdvLicNo.AcceptsReturn = True
        Me.txtAdvLicNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvLicNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvLicNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvLicNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvLicNo.ForeColor = System.Drawing.Color.Blue
        Me.txtAdvLicNo.Location = New System.Drawing.Point(137, 142)
        Me.txtAdvLicNo.MaxLength = 0
        Me.txtAdvLicNo.Name = "txtAdvLicNo"
        Me.txtAdvLicNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvLicNo.Size = New System.Drawing.Size(93, 20)
        Me.txtAdvLicNo.TabIndex = 30
        '
        'txtAgreement
        '
        Me.txtAgreement.AcceptsReturn = True
        Me.txtAgreement.BackColor = System.Drawing.SystemColors.Window
        Me.txtAgreement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAgreement.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAgreement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAgreement.ForeColor = System.Drawing.Color.Blue
        Me.txtAgreement.Location = New System.Drawing.Point(138, 120)
        Me.txtAgreement.MaxLength = 0
        Me.txtAgreement.Name = "txtAgreement"
        Me.txtAgreement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAgreement.Size = New System.Drawing.Size(601, 20)
        Me.txtAgreement.TabIndex = 29
        '
        'txtCarriage
        '
        Me.txtCarriage.AcceptsReturn = True
        Me.txtCarriage.BackColor = System.Drawing.SystemColors.Window
        Me.txtCarriage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarriage.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCarriage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarriage.ForeColor = System.Drawing.Color.Blue
        Me.txtCarriage.Location = New System.Drawing.Point(138, 32)
        Me.txtCarriage.MaxLength = 0
        Me.txtCarriage.Name = "txtCarriage"
        Me.txtCarriage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCarriage.Size = New System.Drawing.Size(221, 20)
        Me.txtCarriage.TabIndex = 22
        '
        'txtPlace
        '
        Me.txtPlace.AcceptsReturn = True
        Me.txtPlace.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlace.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlace.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlace.ForeColor = System.Drawing.Color.Blue
        Me.txtPlace.Location = New System.Drawing.Point(560, 32)
        Me.txtPlace.MaxLength = 0
        Me.txtPlace.Name = "txtPlace"
        Me.txtPlace.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlace.Size = New System.Drawing.Size(179, 20)
        Me.txtPlace.TabIndex = 23
        '
        'txtFlight
        '
        Me.txtFlight.AcceptsReturn = True
        Me.txtFlight.BackColor = System.Drawing.SystemColors.Window
        Me.txtFlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFlight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFlight.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFlight.ForeColor = System.Drawing.Color.Blue
        Me.txtFlight.Location = New System.Drawing.Point(138, 54)
        Me.txtFlight.MaxLength = 0
        Me.txtFlight.Name = "txtFlight"
        Me.txtFlight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFlight.Size = New System.Drawing.Size(221, 20)
        Me.txtFlight.TabIndex = 24
        '
        'txtLoading
        '
        Me.txtLoading.AcceptsReturn = True
        Me.txtLoading.BackColor = System.Drawing.SystemColors.Window
        Me.txtLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLoading.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLoading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoading.ForeColor = System.Drawing.Color.Blue
        Me.txtLoading.Location = New System.Drawing.Point(560, 54)
        Me.txtLoading.MaxLength = 0
        Me.txtLoading.Name = "txtLoading"
        Me.txtLoading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLoading.Size = New System.Drawing.Size(179, 20)
        Me.txtLoading.TabIndex = 25
        '
        'txtDischarge
        '
        Me.txtDischarge.AcceptsReturn = True
        Me.txtDischarge.BackColor = System.Drawing.SystemColors.Window
        Me.txtDischarge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDischarge.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDischarge.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDischarge.ForeColor = System.Drawing.Color.Blue
        Me.txtDischarge.Location = New System.Drawing.Point(138, 76)
        Me.txtDischarge.MaxLength = 0
        Me.txtDischarge.Name = "txtDischarge"
        Me.txtDischarge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDischarge.Size = New System.Drawing.Size(221, 20)
        Me.txtDischarge.TabIndex = 26
        '
        'txtFinalDestination
        '
        Me.txtFinalDestination.AcceptsReturn = True
        Me.txtFinalDestination.BackColor = System.Drawing.SystemColors.Window
        Me.txtFinalDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFinalDestination.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFinalDestination.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFinalDestination.ForeColor = System.Drawing.Color.Blue
        Me.txtFinalDestination.Location = New System.Drawing.Point(560, 76)
        Me.txtFinalDestination.MaxLength = 0
        Me.txtFinalDestination.Name = "txtFinalDestination"
        Me.txtFinalDestination.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFinalDestination.Size = New System.Drawing.Size(179, 20)
        Me.txtFinalDestination.TabIndex = 27
        '
        'txtPayments
        '
        Me.txtPayments.AcceptsReturn = True
        Me.txtPayments.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayments.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPayments.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayments.ForeColor = System.Drawing.Color.Blue
        Me.txtPayments.Location = New System.Drawing.Point(138, 98)
        Me.txtPayments.MaxLength = 0
        Me.txtPayments.Name = "txtPayments"
        Me.txtPayments.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPayments.Size = New System.Drawing.Size(601, 20)
        Me.txtPayments.TabIndex = 28
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(233, 144)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(35, 14)
        Me.Label36.TabIndex = 102
        Me.Label36.Text = "Date :"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(15, 144)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(117, 14)
        Me.Label35.TabIndex = 101
        Me.Label35.Text = "Advance  Licence No :"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(66, 122)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(66, 14)
        Me.Label26.TabIndex = 86
        Me.Label26.Text = "Agreement :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(39, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(93, 14)
        Me.Label2.TabIndex = 73
        Me.Label2.Text = "Pre-Carriage By  :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(390, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(165, 14)
        Me.Label3.TabIndex = 72
        Me.Label3.Text = "Place of Receipt by Pre-Carrier  :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(30, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(102, 14)
        Me.Label5.TabIndex = 71
        Me.Label5.Text = "Vessel / Flight No.  :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(466, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(89, 14)
        Me.Label6.TabIndex = 70
        Me.Label6.Text = "Port of Loading  :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(32, 78)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(100, 14)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "Port of Discharge  :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(464, 78)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(91, 14)
        Me.Label9.TabIndex = 68
        Me.Label9.Text = "Final Destination :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(46, 100)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(86, 14)
        Me.Label10.TabIndex = 67
        Me.Label10.Text = "Payment Terms :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTInfo_TabPage0
        '
        Me._SSTInfo_TabPage0.Controls.Add(Me.SprdOther)
        Me._SSTInfo_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage0.Name = "_SSTInfo_TabPage0"
        Me._SSTInfo_TabPage0.Size = New System.Drawing.Size(914, 180)
        Me._SSTInfo_TabPage0.TabIndex = 0
        Me._SSTInfo_TabPage0.Text = "Other Details"
        '
        'SprdOther
        '
        Me.SprdOther.DataSource = Nothing
        Me.SprdOther.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdOther.Location = New System.Drawing.Point(0, 0)
        Me.SprdOther.Name = "SprdOther"
        Me.SprdOther.OcxState = CType(resources.GetObject("SprdOther.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdOther.Size = New System.Drawing.Size(914, 180)
        Me.SprdOther.TabIndex = 0
        '
        '_SSTInfo_TabPage2
        '
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtNotifyParty3)
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtNotifyParty2)
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtNotifyParty1)
        Me._SSTInfo_TabPage2.Controls.Add(Me.cmdBank)
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtCreditBankAddress)
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtADCode)
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtCreditBank)
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtCustomerBank)
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtAccountNo)
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtSwiftCode)
        Me._SSTInfo_TabPage2.Controls.Add(Me.txtFurtherBank)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Label42)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Label41)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Label40)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Label37)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Label27)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Label30)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Label32)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Label33)
        Me._SSTInfo_TabPage2.Controls.Add(Me.Label34)
        Me._SSTInfo_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTInfo_TabPage2.Name = "_SSTInfo_TabPage2"
        Me._SSTInfo_TabPage2.Size = New System.Drawing.Size(914, 180)
        Me._SSTInfo_TabPage2.TabIndex = 2
        Me._SSTInfo_TabPage2.Text = "Bank Detail"
        '
        'txtNotifyParty3
        '
        Me.txtNotifyParty3.AcceptsReturn = True
        Me.txtNotifyParty3.BackColor = System.Drawing.SystemColors.Window
        Me.txtNotifyParty3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotifyParty3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNotifyParty3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotifyParty3.ForeColor = System.Drawing.Color.Blue
        Me.txtNotifyParty3.Location = New System.Drawing.Point(108, 154)
        Me.txtNotifyParty3.MaxLength = 0
        Me.txtNotifyParty3.Name = "txtNotifyParty3"
        Me.txtNotifyParty3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNotifyParty3.Size = New System.Drawing.Size(601, 20)
        Me.txtNotifyParty3.TabIndex = 41
        '
        'txtNotifyParty2
        '
        Me.txtNotifyParty2.AcceptsReturn = True
        Me.txtNotifyParty2.BackColor = System.Drawing.SystemColors.Window
        Me.txtNotifyParty2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotifyParty2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNotifyParty2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotifyParty2.ForeColor = System.Drawing.Color.Blue
        Me.txtNotifyParty2.Location = New System.Drawing.Point(108, 130)
        Me.txtNotifyParty2.MaxLength = 0
        Me.txtNotifyParty2.Name = "txtNotifyParty2"
        Me.txtNotifyParty2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNotifyParty2.Size = New System.Drawing.Size(601, 20)
        Me.txtNotifyParty2.TabIndex = 39
        '
        'txtNotifyParty1
        '
        Me.txtNotifyParty1.AcceptsReturn = True
        Me.txtNotifyParty1.BackColor = System.Drawing.SystemColors.Window
        Me.txtNotifyParty1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNotifyParty1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNotifyParty1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNotifyParty1.ForeColor = System.Drawing.Color.Blue
        Me.txtNotifyParty1.Location = New System.Drawing.Point(108, 106)
        Me.txtNotifyParty1.MaxLength = 0
        Me.txtNotifyParty1.Name = "txtNotifyParty1"
        Me.txtNotifyParty1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNotifyParty1.Size = New System.Drawing.Size(601, 20)
        Me.txtNotifyParty1.TabIndex = 38
        '
        'txtCreditBankAddress
        '
        Me.txtCreditBankAddress.AcceptsReturn = True
        Me.txtCreditBankAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditBankAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditBankAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreditBankAddress.Enabled = False
        Me.txtCreditBankAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditBankAddress.ForeColor = System.Drawing.Color.Blue
        Me.txtCreditBankAddress.Location = New System.Drawing.Point(351, 8)
        Me.txtCreditBankAddress.MaxLength = 0
        Me.txtCreditBankAddress.Name = "txtCreditBankAddress"
        Me.txtCreditBankAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditBankAddress.Size = New System.Drawing.Size(357, 20)
        Me.txtCreditBankAddress.TabIndex = 33
        '
        'txtADCode
        '
        Me.txtADCode.AcceptsReturn = True
        Me.txtADCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtADCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtADCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtADCode.Enabled = False
        Me.txtADCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtADCode.ForeColor = System.Drawing.Color.Blue
        Me.txtADCode.Location = New System.Drawing.Point(108, 32)
        Me.txtADCode.MaxLength = 0
        Me.txtADCode.Name = "txtADCode"
        Me.txtADCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtADCode.Size = New System.Drawing.Size(217, 20)
        Me.txtADCode.TabIndex = 34
        '
        'txtCreditBank
        '
        Me.txtCreditBank.AcceptsReturn = True
        Me.txtCreditBank.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditBank.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreditBank.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditBank.ForeColor = System.Drawing.Color.Blue
        Me.txtCreditBank.Location = New System.Drawing.Point(108, 8)
        Me.txtCreditBank.MaxLength = 0
        Me.txtCreditBank.Name = "txtCreditBank"
        Me.txtCreditBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditBank.Size = New System.Drawing.Size(217, 20)
        Me.txtCreditBank.TabIndex = 32
        '
        'txtCustomerBank
        '
        Me.txtCustomerBank.AcceptsReturn = True
        Me.txtCustomerBank.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerBank.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerBank.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerBank.ForeColor = System.Drawing.Color.Blue
        Me.txtCustomerBank.Location = New System.Drawing.Point(108, 106)
        Me.txtCustomerBank.MaxLength = 0
        Me.txtCustomerBank.Name = "txtCustomerBank"
        Me.txtCustomerBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerBank.Size = New System.Drawing.Size(221, 20)
        Me.txtCustomerBank.TabIndex = 36
        Me.txtCustomerBank.Visible = False
        '
        'txtAccountNo
        '
        Me.txtAccountNo.AcceptsReturn = True
        Me.txtAccountNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccountNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAccountNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAccountNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountNo.ForeColor = System.Drawing.Color.Blue
        Me.txtAccountNo.Location = New System.Drawing.Point(530, 80)
        Me.txtAccountNo.MaxLength = 0
        Me.txtAccountNo.Name = "txtAccountNo"
        Me.txtAccountNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAccountNo.Size = New System.Drawing.Size(179, 20)
        Me.txtAccountNo.TabIndex = 37
        '
        'txtSwiftCode
        '
        Me.txtSwiftCode.AcceptsReturn = True
        Me.txtSwiftCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSwiftCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSwiftCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSwiftCode.Enabled = False
        Me.txtSwiftCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSwiftCode.ForeColor = System.Drawing.Color.Blue
        Me.txtSwiftCode.Location = New System.Drawing.Point(530, 32)
        Me.txtSwiftCode.MaxLength = 0
        Me.txtSwiftCode.Name = "txtSwiftCode"
        Me.txtSwiftCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSwiftCode.Size = New System.Drawing.Size(179, 20)
        Me.txtSwiftCode.TabIndex = 44
        '
        'txtFurtherBank
        '
        Me.txtFurtherBank.AcceptsReturn = True
        Me.txtFurtherBank.BackColor = System.Drawing.SystemColors.Window
        Me.txtFurtherBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFurtherBank.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFurtherBank.Enabled = False
        Me.txtFurtherBank.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFurtherBank.ForeColor = System.Drawing.Color.Blue
        Me.txtFurtherBank.Location = New System.Drawing.Point(108, 56)
        Me.txtFurtherBank.MaxLength = 0
        Me.txtFurtherBank.Name = "txtFurtherBank"
        Me.txtFurtherBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFurtherBank.Size = New System.Drawing.Size(601, 20)
        Me.txtFurtherBank.TabIndex = 35
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(19, 156)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(84, 14)
        Me.Label42.TabIndex = 109
        Me.Label42.Text = "Notify Party 3# :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(16, 132)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(87, 14)
        Me.Label41.TabIndex = 108
        Me.Label41.Text = "Notify Party 2#  :"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(16, 108)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(87, 14)
        Me.Label40.TabIndex = 107
        Me.Label40.Text = "Notify Party 1#  :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(44, 34)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(59, 14)
        Me.Label37.TabIndex = 103
        Me.Label37.Text = "AD Code  :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(21, 10)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(82, 14)
        Me.Label27.TabIndex = 100
        Me.Label27.Text = "Credit Bank To :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(14, 82)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(89, 14)
        Me.Label30.TabIndex = 99
        Me.Label30.Text = "Customer Bank  :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(376, 82)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(148, 14)
        Me.Label32.TabIndex = 98
        Me.Label32.Text = "Customer Bank Account No  :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(454, 34)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(70, 14)
        Me.Label33.TabIndex = 97
        Me.Label33.Text = "Swift Code  :"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(7, 58)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(96, 14)
        Me.Label34.TabIndex = 96
        Me.Label34.Text = "Further Credit To  :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(315, 217)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(46, 14)
        Me.Label43.TabIndex = 111
        Me.Label43.Text = "Others :"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label39.Location = New System.Drawing.Point(603, 219)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(17, 14)
        Me.Label39.TabIndex = 106
        Me.Label39.Text = "%"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label38.Location = New System.Drawing.Point(502, 217)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(55, 14)
        Me.Label38.TabIndex = 105
        Me.Label38.Text = "Discount :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotAmount
        '
        Me.lblTotAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotAmount.Location = New System.Drawing.Point(649, 237)
        Me.lblTotAmount.Name = "lblTotAmount"
        Me.lblTotAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotAmount.Size = New System.Drawing.Size(99, 17)
        Me.lblTotAmount.TabIndex = 92
        Me.lblTotAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label31.Location = New System.Drawing.Point(563, 238)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(74, 14)
        Me.Label31.TabIndex = 91
        Me.Label31.Text = "Total Amount :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotAmount_INR
        '
        Me.lblTotAmount_INR.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotAmount_INR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotAmount_INR.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotAmount_INR.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotAmount_INR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotAmount_INR.Location = New System.Drawing.Point(389, 237)
        Me.lblTotAmount_INR.Name = "lblTotAmount_INR"
        Me.lblTotAmount_INR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotAmount_INR.Size = New System.Drawing.Size(99, 17)
        Me.lblTotAmount_INR.TabIndex = 90
        Me.lblTotAmount_INR.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label29.Location = New System.Drawing.Point(302, 238)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(77, 14)
        Me.Label29.TabIndex = 89
        Me.Label29.Text = "Amount (INR) :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(69, 237)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(99, 17)
        Me.lblTotQty.TabIndex = 88
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label28.Location = New System.Drawing.Point(6, 238)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(55, 14)
        Me.Label28.TabIndex = 87
        Me.Label28.Text = "Total Qty :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 56
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 2)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(920, 441)
        Me.SprdView.TabIndex = 56
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 440)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(924, 51)
        Me.Frame3.TabIndex = 53
        Me.Frame3.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(4, 18)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(33, 17)
        Me.lblBookType.TabIndex = 62
        Me.lblBookType.Text = "lblBookType"
        '
        'FrmExportInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(926, 483)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmExportInvoice"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Export Invoice"
        Me.FraFront.ResumeLayout(False)
        Me.Frasupp.ResumeLayout(False)
        Me.Frasupp.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frasprd.ResumeLayout(False)
        Me.Frasprd.PerformLayout()
        Me.SSTInfo.ResumeLayout(False)
        Me._SSTInfo_TabPage3.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTInfo_TabPage1.ResumeLayout(False)
        Me._SSTInfo_TabPage1.PerformLayout()
        Me._SSTInfo_TabPage0.ResumeLayout(False)
        CType(Me.SprdOther, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTInfo_TabPage2.ResumeLayout(False)
        Me._SSTInfo_TabPage2.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AdataItem, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Friend WithEvents _SSTInfo_TabPage3 As TabPage
    Public WithEvents cmdsearchConsinee As Button
    Public WithEvents txtShipTo As TextBox
    Public WithEvents Label18 As Label
    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label44 As Label
    Public WithEvents txtBuyerAddress As TextBox
    Public WithEvents txtConsigneeAddress As TextBox
    Public WithEvents chkREXDeclaration As CheckBox
#End Region
End Class