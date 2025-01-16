Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAtrn
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
        'Me.MDIParent = Payroll.Master

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
    Public WithEvents chkReverseCharge As System.Windows.Forms.CheckBox
    Public WithEvents chkPnL As System.Windows.Forms.CheckBox
    Public WithEvents txtExpDate As System.Windows.Forms.TextBox
    Public WithEvents txtTDSDeductOn As System.Windows.Forms.TextBox
    Public WithEvents txtESIDeductOn As System.Windows.Forms.TextBox
    Public WithEvents txtSTDSDeductOn As System.Windows.Forms.TextBox
    Public WithEvents ChkTDSRO As System.Windows.Forms.CheckBox
    Public WithEvents ChkESIRO As System.Windows.Forms.CheckBox
    Public WithEvents ChkSTDSRO As System.Windows.Forms.CheckBox
    Public WithEvents txtJVTDSAmount As System.Windows.Forms.TextBox
    Public WithEvents txtJVTDSRate As System.Windows.Forms.TextBox
    Public WithEvents chkTDS As System.Windows.Forms.CheckBox
    Public WithEvents txtESIAmount As System.Windows.Forms.TextBox
    Public WithEvents txtESIRate As System.Windows.Forms.TextBox
    Public WithEvents chkESI As System.Windows.Forms.CheckBox
    Public WithEvents txtSTDSAmount As System.Windows.Forms.TextBox
    Public WithEvents txtSTDSRate As System.Windows.Forms.TextBox
    Public WithEvents ChkSTDS As System.Windows.Forms.CheckBox
    Public WithEvents txtJVVNO As System.Windows.Forms.TextBox
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents FraTDSDeduction As System.Windows.Forms.GroupBox
    Public WithEvents _ssTab_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtServNo As System.Windows.Forms.TextBox
    Public WithEvents chkServTaxRefund As System.Windows.Forms.CheckBox
    Public WithEvents chkPLA As System.Windows.Forms.CheckBox
    Public WithEvents chkServTaxClaim As System.Windows.Forms.CheckBox
    Public WithEvents chkSTClaim As System.Windows.Forms.CheckBox
    Public WithEvents chkCapital As System.Windows.Forms.CheckBox
    Public WithEvents chkSuppBill As System.Windows.Forms.CheckBox
    Public WithEvents txtModvatNo As System.Windows.Forms.TextBox
    Public WithEvents txtSTRefundNo As System.Windows.Forms.TextBox
    Public WithEvents chkModvat As System.Windows.Forms.CheckBox
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents FraSuppBill As System.Windows.Forms.GroupBox
    Public WithEvents _ssTab_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents txtImpBillDate As System.Windows.Forms.TextBox
    Public WithEvents txtImpPartyName As System.Windows.Forms.TextBox
    Public WithEvents txtImpBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtImpMRRNo As System.Windows.Forms.TextBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _ssTab_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents txtExpBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtExpPartyName As System.Windows.Forms.TextBox
    Public WithEvents txtExpBillDate As System.Windows.Forms.TextBox
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _ssTab_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents txtServiceTaxAmount As System.Windows.Forms.TextBox
    Public WithEvents txtServiceTaxPer As System.Windows.Forms.TextBox
    Public WithEvents txtRecipientPer As System.Windows.Forms.TextBox
    Public WithEvents txtProviderPer As System.Windows.Forms.TextBox
    Public WithEvents txtServiceOn As System.Windows.Forms.TextBox
    Public WithEvents cmdServProvided As System.Windows.Forms.Button
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _ssTab_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents ssTab As System.Windows.Forms.TabControl
    Public WithEvents txtNarration As System.Windows.Forms.TextBox
    Public WithEvents chkISLowerDed As System.Windows.Forms.CheckBox
    Public WithEvents txtPName As System.Windows.Forms.TextBox
    Public WithEvents txtSection As System.Windows.Forms.TextBox
    Public WithEvents chkExempted As System.Windows.Forms.CheckBox
    Public WithEvents txtVD As System.Windows.Forms.TextBox
    Public WithEvents txtAmountPaid As System.Windows.Forms.TextBox
    Public WithEvents txtTdsRate As System.Windows.Forms.TextBox
    Public WithEvents txtExempted As System.Windows.Forms.TextBox
    Public WithEvents txtTDSAmount As System.Windows.Forms.TextBox
    Public WithEvents TxtTDSAccount As System.Windows.Forms.TextBox
    Public WithEvents cmdTDSHide As System.Windows.Forms.Button
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents FraTDSFrame As System.Windows.Forms.GroupBox
    Public WithEvents txtVNoSuffix As System.Windows.Forms.TextBox
    Public WithEvents txtVType As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtVno As System.Windows.Forms.TextBox
    Public WithEvents txtVNo1 As System.Windows.Forms.TextBox
    Public WithEvents txtPartyName As System.Windows.Forms.TextBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents chkChqDeposit As System.Windows.Forms.CheckBox
    Public WithEvents ChkACPayee As System.Windows.Forms.CheckBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblReversalMkey As System.Windows.Forms.Label
    Public WithEvents lblReversalVoucher As System.Windows.Forms.Label
    Public WithEvents lblReversalMade As System.Windows.Forms.Label
    Public WithEvents lblSaleBillNo As System.Windows.Forms.Label
    Public WithEvents lblELYear As System.Windows.Forms.Label
    Public WithEvents lblAcBalDCDiv As System.Windows.Forms.Label
    Public WithEvents lblAcBalAmtDiv As System.Windows.Forms.Label
    Public WithEvents lblEmpCode As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents lblServiceTaxDetail As System.Windows.Forms.Label
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
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblBookBalDC As System.Windows.Forms.Label
    Public WithEvents LblDate_Issue_Receive As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblAccount As System.Windows.Forms.Label
    Public WithEvents lblBookBalAmt As System.Windows.Forms.Label
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
    Public WithEvents ADataGrid As VB6.ADODC
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAtrn))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdServProvided = New System.Windows.Forms.Button()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.TxtTDSAccount = New System.Windows.Forms.TextBox()
        Me.cmdsearch = New System.Windows.Forms.Button()
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
        Me.txtPopulateVNo = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.CmdPopFromFile = New System.Windows.Forms.Button()
        Me.chkReverseCharge = New System.Windows.Forms.CheckBox()
        Me.chkPnL = New System.Windows.Forms.CheckBox()
        Me.txtExpDate = New System.Windows.Forms.TextBox()
        Me.ssTab = New System.Windows.Forms.TabControl()
        Me._ssTab_TabPage0 = New System.Windows.Forms.TabPage()
        Me.FraTDSDeduction = New System.Windows.Forms.GroupBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txtTDSSection = New System.Windows.Forms.TextBox()
        Me.txtTDSDeductOn = New System.Windows.Forms.TextBox()
        Me.txtESIDeductOn = New System.Windows.Forms.TextBox()
        Me.txtSTDSDeductOn = New System.Windows.Forms.TextBox()
        Me.ChkTDSRO = New System.Windows.Forms.CheckBox()
        Me.ChkESIRO = New System.Windows.Forms.CheckBox()
        Me.ChkSTDSRO = New System.Windows.Forms.CheckBox()
        Me.txtJVTDSAmount = New System.Windows.Forms.TextBox()
        Me.txtJVTDSRate = New System.Windows.Forms.TextBox()
        Me.chkTDS = New System.Windows.Forms.CheckBox()
        Me.txtESIAmount = New System.Windows.Forms.TextBox()
        Me.txtESIRate = New System.Windows.Forms.TextBox()
        Me.chkESI = New System.Windows.Forms.CheckBox()
        Me.txtSTDSAmount = New System.Windows.Forms.TextBox()
        Me.txtSTDSRate = New System.Windows.Forms.TextBox()
        Me.ChkSTDS = New System.Windows.Forms.CheckBox()
        Me.txtJVVNO = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me._ssTab_TabPage1 = New System.Windows.Forms.TabPage()
        Me.FraSuppBill = New System.Windows.Forms.GroupBox()
        Me.txtServNo = New System.Windows.Forms.TextBox()
        Me.chkServTaxRefund = New System.Windows.Forms.CheckBox()
        Me.chkPLA = New System.Windows.Forms.CheckBox()
        Me.chkServTaxClaim = New System.Windows.Forms.CheckBox()
        Me.chkSTClaim = New System.Windows.Forms.CheckBox()
        Me.chkCapital = New System.Windows.Forms.CheckBox()
        Me.chkSuppBill = New System.Windows.Forms.CheckBox()
        Me.txtModvatNo = New System.Windows.Forms.TextBox()
        Me.txtSTRefundNo = New System.Windows.Forms.TextBox()
        Me.chkModvat = New System.Windows.Forms.CheckBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me._ssTab_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtImpBillDate = New System.Windows.Forms.TextBox()
        Me.txtImpPartyName = New System.Windows.Forms.TextBox()
        Me.txtImpBillNo = New System.Windows.Forms.TextBox()
        Me.txtImpMRRNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me._ssTab_TabPage3 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtExpBillNo = New System.Windows.Forms.TextBox()
        Me.txtExpPartyName = New System.Windows.Forms.TextBox()
        Me.txtExpBillDate = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me._ssTab_TabPage4 = New System.Windows.Forms.TabPage()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtServiceTaxAmount = New System.Windows.Forms.TextBox()
        Me.txtServiceTaxPer = New System.Windows.Forms.TextBox()
        Me.txtRecipientPer = New System.Windows.Forms.TextBox()
        Me.txtProviderPer = New System.Windows.Forms.TextBox()
        Me.txtServiceOn = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.FraTDSFrame = New System.Windows.Forms.GroupBox()
        Me.chkISLowerDed = New System.Windows.Forms.CheckBox()
        Me.txtPName = New System.Windows.Forms.TextBox()
        Me.txtSection = New System.Windows.Forms.TextBox()
        Me.chkExempted = New System.Windows.Forms.CheckBox()
        Me.txtVD = New System.Windows.Forms.TextBox()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.txtTdsRate = New System.Windows.Forms.TextBox()
        Me.txtExempted = New System.Windows.Forms.TextBox()
        Me.txtTDSAmount = New System.Windows.Forms.TextBox()
        Me.cmdTDSHide = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtVNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtVType = New System.Windows.Forms.TextBox()
        Me.TxtVDate = New System.Windows.Forms.TextBox()
        Me.txtVno = New System.Windows.Forms.TextBox()
        Me.txtVNo1 = New System.Windows.Forms.TextBox()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.chkChqDeposit = New System.Windows.Forms.CheckBox()
        Me.ChkACPayee = New System.Windows.Forms.CheckBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblReversalMkey = New System.Windows.Forms.Label()
        Me.lblReversalVoucher = New System.Windows.Forms.Label()
        Me.lblReversalMade = New System.Windows.Forms.Label()
        Me.lblSaleBillNo = New System.Windows.Forms.Label()
        Me.lblELYear = New System.Windows.Forms.Label()
        Me.lblAcBalDCDiv = New System.Windows.Forms.Label()
        Me.lblAcBalAmtDiv = New System.Windows.Forms.Label()
        Me.lblEmpCode = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblServiceTaxDetail = New System.Windows.Forms.Label()
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBookBalDC = New System.Windows.Forms.Label()
        Me.LblDate_Issue_Receive = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.lblBookBalAmt = New System.Windows.Forms.Label()
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
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog()
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog()
        Me.FraTrans.SuspendLayout()
        Me.ssTab.SuspendLayout()
        Me._ssTab_TabPage0.SuspendLayout()
        Me.FraTDSDeduction.SuspendLayout()
        Me._ssTab_TabPage1.SuspendLayout()
        Me.FraSuppBill.SuspendLayout()
        Me._ssTab_TabPage2.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me._ssTab_TabPage3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me._ssTab_TabPage4.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.FraTDSFrame.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraGridView.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdServProvided
        '
        Me.cmdServProvided.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdServProvided.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdServProvided.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdServProvided.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdServProvided.Image = CType(resources.GetObject("cmdServProvided.Image"), System.Drawing.Image)
        Me.cmdServProvided.Location = New System.Drawing.Point(282, 12)
        Me.cmdServProvided.Name = "cmdServProvided"
        Me.cmdServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdServProvided.Size = New System.Drawing.Size(27, 19)
        Me.cmdServProvided.TabIndex = 54
        Me.cmdServProvided.TabStop = False
        Me.cmdServProvided.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdServProvided, "Search")
        Me.cmdServProvided.UseVisualStyleBackColor = False
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(56, 12)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(225, 20)
        Me.txtServProvided.TabIndex = 53
        Me.ToolTip1.SetToolTip(Me.txtServProvided, "Press F1 For Help")
        '
        'TxtTDSAccount
        '
        Me.TxtTDSAccount.AcceptsReturn = True
        Me.TxtTDSAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtTDSAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTDSAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtTDSAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTDSAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtTDSAccount.Location = New System.Drawing.Point(140, 12)
        Me.TxtTDSAccount.MaxLength = 0
        Me.TxtTDSAccount.Name = "TxtTDSAccount"
        Me.TxtTDSAccount.ReadOnly = True
        Me.TxtTDSAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtTDSAccount.Size = New System.Drawing.Size(343, 20)
        Me.TxtTDSAccount.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.TxtTDSAccount, "Press F1 For Help")
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(523, 13)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(23, 21)
        Me.cmdsearch.TabIndex = 1
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
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
        Me.cmdClose.TabIndex = 73
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
        Me.cmdBillDetail.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdBillDetail.Location = New System.Drawing.Point(604, 10)
        Me.cmdBillDetail.Name = "cmdBillDetail"
        Me.cmdBillDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillDetail.Size = New System.Drawing.Size(67, 37)
        Me.cmdBillDetail.TabIndex = 72
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
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(537, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 71
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
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(471, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 70
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
        Me.cmdPrint.Location = New System.Drawing.Point(404, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 69
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
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(338, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 68
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
        Me.cmdAuthorised.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAuthorised.Location = New System.Drawing.Point(272, 10)
        Me.cmdAuthorised.Name = "cmdAuthorised"
        Me.cmdAuthorised.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAuthorised.Size = New System.Drawing.Size(67, 37)
        Me.cmdAuthorised.TabIndex = 127
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
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(206, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 67
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
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSave.Location = New System.Drawing.Point(139, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 66
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
        Me.cmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdModify.Location = New System.Drawing.Point(72, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 65
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
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdd.Location = New System.Drawing.Point(5, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 64
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'FraTrans
        '
        Me.FraTrans.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrans.Controls.Add(Me.txtPopulateVNo)
        Me.FraTrans.Controls.Add(Me.Label31)
        Me.FraTrans.Controls.Add(Me.CmdPopFromFile)
        Me.FraTrans.Controls.Add(Me.chkReverseCharge)
        Me.FraTrans.Controls.Add(Me.chkPnL)
        Me.FraTrans.Controls.Add(Me.txtExpDate)
        Me.FraTrans.Controls.Add(Me.ssTab)
        Me.FraTrans.Controls.Add(Me.txtNarration)
        Me.FraTrans.Controls.Add(Me.FraTDSFrame)
        Me.FraTrans.Controls.Add(Me.txtVNoSuffix)
        Me.FraTrans.Controls.Add(Me.txtVType)
        Me.FraTrans.Controls.Add(Me.cmdsearch)
        Me.FraTrans.Controls.Add(Me.TxtVDate)
        Me.FraTrans.Controls.Add(Me.txtVno)
        Me.FraTrans.Controls.Add(Me.txtVNo1)
        Me.FraTrans.Controls.Add(Me.txtPartyName)
        Me.FraTrans.Controls.Add(Me.chkCancelled)
        Me.FraTrans.Controls.Add(Me.chkChqDeposit)
        Me.FraTrans.Controls.Add(Me.ChkACPayee)
        Me.FraTrans.Controls.Add(Me.Report1)
        Me.FraTrans.Controls.Add(Me.SprdMain)
        Me.FraTrans.Controls.Add(Me.lblReversalMkey)
        Me.FraTrans.Controls.Add(Me.lblReversalVoucher)
        Me.FraTrans.Controls.Add(Me.lblReversalMade)
        Me.FraTrans.Controls.Add(Me.lblSaleBillNo)
        Me.FraTrans.Controls.Add(Me.lblELYear)
        Me.FraTrans.Controls.Add(Me.lblAcBalDCDiv)
        Me.FraTrans.Controls.Add(Me.lblAcBalAmtDiv)
        Me.FraTrans.Controls.Add(Me.lblEmpCode)
        Me.FraTrans.Controls.Add(Me.Label23)
        Me.FraTrans.Controls.Add(Me.lblServiceTaxDetail)
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
        Me.FraTrans.Controls.Add(Me.Label3)
        Me.FraTrans.Controls.Add(Me.Label2)
        Me.FraTrans.Controls.Add(Me.lblBookBalDC)
        Me.FraTrans.Controls.Add(Me.LblDate_Issue_Receive)
        Me.FraTrans.Controls.Add(Me.Label1)
        Me.FraTrans.Controls.Add(Me.lblAccount)
        Me.FraTrans.Controls.Add(Me.lblBookBalAmt)
        Me.FraTrans.Controls.Add(Me.LblTotal)
        Me.FraTrans.Controls.Add(Me.LblDr)
        Me.FraTrans.Controls.Add(Me.LblDrAmt)
        Me.FraTrans.Controls.Add(Me.LblCr)
        Me.FraTrans.Controls.Add(Me.LblCrAmt)
        Me.FraTrans.Controls.Add(Me.LblNet)
        Me.FraTrans.Controls.Add(Me.LblNetAmt)
        Me.FraTrans.Controls.Add(Me.lblPaymentDetail)
        Me.FraTrans.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrans.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrans.Location = New System.Drawing.Point(-2, -6)
        Me.FraTrans.Name = "FraTrans"
        Me.FraTrans.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrans.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrans.Size = New System.Drawing.Size(910, 578)
        Me.FraTrans.TabIndex = 74
        Me.FraTrans.TabStop = False
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
        Me.txtPopulateVNo.Location = New System.Drawing.Point(661, 15)
        Me.txtPopulateVNo.MaxLength = 0
        Me.txtPopulateVNo.Name = "txtPopulateVNo"
        Me.txtPopulateVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPopulateVNo.Size = New System.Drawing.Size(106, 20)
        Me.txtPopulateVNo.TabIndex = 158
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(557, 18)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(101, 14)
        Me.Label31.TabIndex = 159
        Me.Label31.Text = "Copy From VNo. :"
        '
        'CmdPopFromFile
        '
        Me.CmdPopFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPopFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopFromFile.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopFromFile.Location = New System.Drawing.Point(780, 12)
        Me.CmdPopFromFile.Name = "CmdPopFromFile"
        Me.CmdPopFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopFromFile.Size = New System.Drawing.Size(121, 23)
        Me.CmdPopFromFile.TabIndex = 157
        Me.CmdPopFromFile.Text = "Populate From File"
        Me.CmdPopFromFile.UseVisualStyleBackColor = False
        '
        'chkReverseCharge
        '
        Me.chkReverseCharge.AutoSize = True
        Me.chkReverseCharge.BackColor = System.Drawing.SystemColors.Control
        Me.chkReverseCharge.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkReverseCharge.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReverseCharge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReverseCharge.Location = New System.Drawing.Point(8, 390)
        Me.chkReverseCharge.Name = "chkReverseCharge"
        Me.chkReverseCharge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkReverseCharge.Size = New System.Drawing.Size(157, 18)
        Me.chkReverseCharge.TabIndex = 152
        Me.chkReverseCharge.Text = "Reverse Charge Applicable"
        Me.chkReverseCharge.UseVisualStyleBackColor = False
        '
        'chkPnL
        '
        Me.chkPnL.AutoSize = True
        Me.chkPnL.BackColor = System.Drawing.SystemColors.Control
        Me.chkPnL.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPnL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPnL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPnL.Location = New System.Drawing.Point(613, 43)
        Me.chkPnL.Name = "chkPnL"
        Me.chkPnL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPnL.Size = New System.Drawing.Size(74, 18)
        Me.chkPnL.TabIndex = 140
        Me.chkPnL.Text = "P && L Flag"
        Me.chkPnL.UseVisualStyleBackColor = False
        '
        'txtExpDate
        '
        Me.txtExpDate.AcceptsReturn = True
        Me.txtExpDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExpDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpDate.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtExpDate.Location = New System.Drawing.Point(467, 43)
        Me.txtExpDate.MaxLength = 0
        Me.txtExpDate.Name = "txtExpDate"
        Me.txtExpDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpDate.Size = New System.Drawing.Size(70, 20)
        Me.txtExpDate.TabIndex = 7
        '
        'ssTab
        '
        Me.ssTab.Controls.Add(Me._ssTab_TabPage0)
        Me.ssTab.Controls.Add(Me._ssTab_TabPage1)
        Me.ssTab.Controls.Add(Me._ssTab_TabPage2)
        Me.ssTab.Controls.Add(Me._ssTab_TabPage3)
        Me.ssTab.Controls.Add(Me._ssTab_TabPage4)
        Me.ssTab.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ssTab.ItemSize = New System.Drawing.Size(42, 18)
        Me.ssTab.Location = New System.Drawing.Point(4, 409)
        Me.ssTab.Name = "ssTab"
        Me.ssTab.SelectedIndex = 4
        Me.ssTab.Size = New System.Drawing.Size(408, 171)
        Me.ssTab.TabIndex = 19
        '
        '_ssTab_TabPage0
        '
        Me._ssTab_TabPage0.Controls.Add(Me.FraTDSDeduction)
        Me._ssTab_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._ssTab_TabPage0.Name = "_ssTab_TabPage0"
        Me._ssTab_TabPage0.Size = New System.Drawing.Size(400, 145)
        Me._ssTab_TabPage0.TabIndex = 0
        Me._ssTab_TabPage0.Text = "Deduction"
        '
        'FraTDSDeduction
        '
        Me.FraTDSDeduction.BackColor = System.Drawing.SystemColors.Control
        Me.FraTDSDeduction.Controls.Add(Me.Label50)
        Me.FraTDSDeduction.Controls.Add(Me.txtTDSSection)
        Me.FraTDSDeduction.Controls.Add(Me.txtTDSDeductOn)
        Me.FraTDSDeduction.Controls.Add(Me.txtESIDeductOn)
        Me.FraTDSDeduction.Controls.Add(Me.txtSTDSDeductOn)
        Me.FraTDSDeduction.Controls.Add(Me.ChkTDSRO)
        Me.FraTDSDeduction.Controls.Add(Me.ChkESIRO)
        Me.FraTDSDeduction.Controls.Add(Me.ChkSTDSRO)
        Me.FraTDSDeduction.Controls.Add(Me.txtJVTDSAmount)
        Me.FraTDSDeduction.Controls.Add(Me.txtJVTDSRate)
        Me.FraTDSDeduction.Controls.Add(Me.chkTDS)
        Me.FraTDSDeduction.Controls.Add(Me.txtESIAmount)
        Me.FraTDSDeduction.Controls.Add(Me.txtESIRate)
        Me.FraTDSDeduction.Controls.Add(Me.chkESI)
        Me.FraTDSDeduction.Controls.Add(Me.txtSTDSAmount)
        Me.FraTDSDeduction.Controls.Add(Me.txtSTDSRate)
        Me.FraTDSDeduction.Controls.Add(Me.ChkSTDS)
        Me.FraTDSDeduction.Controls.Add(Me.txtJVVNO)
        Me.FraTDSDeduction.Controls.Add(Me.Label41)
        Me.FraTDSDeduction.Controls.Add(Me.Label40)
        Me.FraTDSDeduction.Controls.Add(Me.Label42)
        Me.FraTDSDeduction.Controls.Add(Me.Label43)
        Me.FraTDSDeduction.Controls.Add(Me.Label46)
        Me.FraTDSDeduction.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTDSDeduction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTDSDeduction.Location = New System.Drawing.Point(2, -3)
        Me.FraTDSDeduction.Name = "FraTDSDeduction"
        Me.FraTDSDeduction.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTDSDeduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTDSDeduction.Size = New System.Drawing.Size(398, 147)
        Me.FraTDSDeduction.TabIndex = 109
        Me.FraTDSDeduction.TabStop = False
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(255, 9)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(43, 14)
        Me.Label50.TabIndex = 139
        Me.Label50.Text = "Section"
        '
        'txtTDSSection
        '
        Me.txtTDSSection.AcceptsReturn = True
        Me.txtTDSSection.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSSection.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSSection.Enabled = False
        Me.txtTDSSection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSSection.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSSection.Location = New System.Drawing.Point(255, 24)
        Me.txtTDSSection.MaxLength = 0
        Me.txtTDSSection.Name = "txtTDSSection"
        Me.txtTDSSection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSSection.Size = New System.Drawing.Size(63, 20)
        Me.txtTDSSection.TabIndex = 138
        Me.txtTDSSection.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTDSDeductOn
        '
        Me.txtTDSDeductOn.AcceptsReturn = True
        Me.txtTDSDeductOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSDeductOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSDeductOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSDeductOn.Enabled = False
        Me.txtTDSDeductOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSDeductOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSDeductOn.Location = New System.Drawing.Point(60, 24)
        Me.txtTDSDeductOn.MaxLength = 0
        Me.txtTDSDeductOn.Name = "txtTDSDeductOn"
        Me.txtTDSDeductOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSDeductOn.Size = New System.Drawing.Size(71, 20)
        Me.txtTDSDeductOn.TabIndex = 21
        Me.txtTDSDeductOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtESIDeductOn
        '
        Me.txtESIDeductOn.AcceptsReturn = True
        Me.txtESIDeductOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtESIDeductOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIDeductOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIDeductOn.Enabled = False
        Me.txtESIDeductOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIDeductOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESIDeductOn.Location = New System.Drawing.Point(60, 44)
        Me.txtESIDeductOn.MaxLength = 0
        Me.txtESIDeductOn.Name = "txtESIDeductOn"
        Me.txtESIDeductOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIDeductOn.Size = New System.Drawing.Size(71, 20)
        Me.txtESIDeductOn.TabIndex = 26
        Me.txtESIDeductOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSTDSDeductOn
        '
        Me.txtSTDSDeductOn.AcceptsReturn = True
        Me.txtSTDSDeductOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTDSDeductOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTDSDeductOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTDSDeductOn.Enabled = False
        Me.txtSTDSDeductOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTDSDeductOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSTDSDeductOn.Location = New System.Drawing.Point(60, 64)
        Me.txtSTDSDeductOn.MaxLength = 0
        Me.txtSTDSDeductOn.Name = "txtSTDSDeductOn"
        Me.txtSTDSDeductOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSDeductOn.Size = New System.Drawing.Size(71, 20)
        Me.txtSTDSDeductOn.TabIndex = 31
        Me.txtSTDSDeductOn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ChkTDSRO
        '
        Me.ChkTDSRO.BackColor = System.Drawing.SystemColors.Control
        Me.ChkTDSRO.Checked = True
        Me.ChkTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkTDSRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkTDSRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkTDSRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkTDSRO.Location = New System.Drawing.Point(236, 26)
        Me.ChkTDSRO.Name = "ChkTDSRO"
        Me.ChkTDSRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkTDSRO.Size = New System.Drawing.Size(15, 13)
        Me.ChkTDSRO.TabIndex = 24
        Me.ChkTDSRO.UseVisualStyleBackColor = False
        '
        'ChkESIRO
        '
        Me.ChkESIRO.BackColor = System.Drawing.SystemColors.Control
        Me.ChkESIRO.Checked = True
        Me.ChkESIRO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkESIRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkESIRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkESIRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkESIRO.Location = New System.Drawing.Point(236, 46)
        Me.ChkESIRO.Name = "ChkESIRO"
        Me.ChkESIRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkESIRO.Size = New System.Drawing.Size(15, 13)
        Me.ChkESIRO.TabIndex = 29
        Me.ChkESIRO.UseVisualStyleBackColor = False
        '
        'ChkSTDSRO
        '
        Me.ChkSTDSRO.BackColor = System.Drawing.SystemColors.Control
        Me.ChkSTDSRO.Checked = True
        Me.ChkSTDSRO.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkSTDSRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkSTDSRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSTDSRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkSTDSRO.Location = New System.Drawing.Point(236, 66)
        Me.ChkSTDSRO.Name = "ChkSTDSRO"
        Me.ChkSTDSRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkSTDSRO.Size = New System.Drawing.Size(15, 13)
        Me.ChkSTDSRO.TabIndex = 34
        Me.ChkSTDSRO.UseVisualStyleBackColor = False
        '
        'txtJVTDSAmount
        '
        Me.txtJVTDSAmount.AcceptsReturn = True
        Me.txtJVTDSAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtJVTDSAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJVTDSAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJVTDSAmount.Enabled = False
        Me.txtJVTDSAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJVTDSAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtJVTDSAmount.Location = New System.Drawing.Point(172, 24)
        Me.txtJVTDSAmount.MaxLength = 0
        Me.txtJVTDSAmount.Name = "txtJVTDSAmount"
        Me.txtJVTDSAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJVTDSAmount.Size = New System.Drawing.Size(63, 20)
        Me.txtJVTDSAmount.TabIndex = 23
        Me.txtJVTDSAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtJVTDSRate
        '
        Me.txtJVTDSRate.AcceptsReturn = True
        Me.txtJVTDSRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtJVTDSRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJVTDSRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJVTDSRate.Enabled = False
        Me.txtJVTDSRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJVTDSRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtJVTDSRate.Location = New System.Drawing.Point(132, 24)
        Me.txtJVTDSRate.MaxLength = 0
        Me.txtJVTDSRate.Name = "txtJVTDSRate"
        Me.txtJVTDSRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJVTDSRate.Size = New System.Drawing.Size(39, 20)
        Me.txtJVTDSRate.TabIndex = 22
        Me.txtJVTDSRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkTDS
        '
        Me.chkTDS.AutoSize = True
        Me.chkTDS.BackColor = System.Drawing.SystemColors.Control
        Me.chkTDS.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTDS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTDS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTDS.Location = New System.Drawing.Point(6, 26)
        Me.chkTDS.Name = "chkTDS"
        Me.chkTDS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTDS.Size = New System.Drawing.Size(46, 18)
        Me.chkTDS.TabIndex = 20
        Me.chkTDS.Text = "TDS"
        Me.chkTDS.UseVisualStyleBackColor = False
        '
        'txtESIAmount
        '
        Me.txtESIAmount.AcceptsReturn = True
        Me.txtESIAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtESIAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIAmount.Enabled = False
        Me.txtESIAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESIAmount.Location = New System.Drawing.Point(172, 44)
        Me.txtESIAmount.MaxLength = 0
        Me.txtESIAmount.Name = "txtESIAmount"
        Me.txtESIAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIAmount.Size = New System.Drawing.Size(63, 20)
        Me.txtESIAmount.TabIndex = 28
        Me.txtESIAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtESIRate
        '
        Me.txtESIRate.AcceptsReturn = True
        Me.txtESIRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtESIRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIRate.Enabled = False
        Me.txtESIRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESIRate.Location = New System.Drawing.Point(132, 44)
        Me.txtESIRate.MaxLength = 0
        Me.txtESIRate.Name = "txtESIRate"
        Me.txtESIRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIRate.Size = New System.Drawing.Size(39, 20)
        Me.txtESIRate.TabIndex = 27
        Me.txtESIRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkESI
        '
        Me.chkESI.AutoSize = True
        Me.chkESI.BackColor = System.Drawing.SystemColors.Control
        Me.chkESI.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkESI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkESI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkESI.Location = New System.Drawing.Point(6, 46)
        Me.chkESI.Name = "chkESI"
        Me.chkESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkESI.Size = New System.Drawing.Size(41, 18)
        Me.chkESI.TabIndex = 25
        Me.chkESI.Text = "ESI"
        Me.chkESI.UseVisualStyleBackColor = False
        '
        'txtSTDSAmount
        '
        Me.txtSTDSAmount.AcceptsReturn = True
        Me.txtSTDSAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTDSAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTDSAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTDSAmount.Enabled = False
        Me.txtSTDSAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTDSAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSTDSAmount.Location = New System.Drawing.Point(172, 64)
        Me.txtSTDSAmount.MaxLength = 0
        Me.txtSTDSAmount.Name = "txtSTDSAmount"
        Me.txtSTDSAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSAmount.Size = New System.Drawing.Size(63, 20)
        Me.txtSTDSAmount.TabIndex = 33
        Me.txtSTDSAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSTDSRate
        '
        Me.txtSTDSRate.AcceptsReturn = True
        Me.txtSTDSRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTDSRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTDSRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTDSRate.Enabled = False
        Me.txtSTDSRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTDSRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSTDSRate.Location = New System.Drawing.Point(132, 64)
        Me.txtSTDSRate.MaxLength = 0
        Me.txtSTDSRate.Name = "txtSTDSRate"
        Me.txtSTDSRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTDSRate.Size = New System.Drawing.Size(39, 20)
        Me.txtSTDSRate.TabIndex = 32
        Me.txtSTDSRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ChkSTDS
        '
        Me.ChkSTDS.AutoSize = True
        Me.ChkSTDS.BackColor = System.Drawing.SystemColors.Control
        Me.ChkSTDS.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkSTDS.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkSTDS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkSTDS.Location = New System.Drawing.Point(6, 66)
        Me.ChkSTDS.Name = "ChkSTDS"
        Me.ChkSTDS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkSTDS.Size = New System.Drawing.Size(53, 18)
        Me.ChkSTDS.TabIndex = 30
        Me.ChkSTDS.Text = "STDS"
        Me.ChkSTDS.UseVisualStyleBackColor = False
        '
        'txtJVVNO
        '
        Me.txtJVVNO.AcceptsReturn = True
        Me.txtJVVNO.BackColor = System.Drawing.SystemColors.Window
        Me.txtJVVNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJVVNO.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJVVNO.Enabled = False
        Me.txtJVVNO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJVVNO.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtJVVNO.Location = New System.Drawing.Point(108, 85)
        Me.txtJVVNO.MaxLength = 0
        Me.txtJVVNO.Name = "txtJVVNO"
        Me.txtJVVNO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJVVNO.Size = New System.Drawing.Size(127, 20)
        Me.txtJVVNO.TabIndex = 35
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(184, 8)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(44, 14)
        Me.Label41.TabIndex = 114
        Me.Label41.Text = "Amount"
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(132, 8)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(47, 14)
        Me.Label40.TabIndex = 113
        Me.Label40.Text = "Rate(%)"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(232, 8)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(22, 14)
        Me.Label42.TabIndex = 112
        Me.Label42.Text = "RO"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label43.Location = New System.Drawing.Point(62, 8)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(58, 14)
        Me.Label43.TabIndex = 111
        Me.Label43.Text = "Deduct On"
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Enabled = False
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(8, 87)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(89, 14)
        Me.Label46.TabIndex = 110
        Me.Label46.Text = "JV Voucher No  :"
        '
        '_ssTab_TabPage1
        '
        Me._ssTab_TabPage1.Controls.Add(Me.FraSuppBill)
        Me._ssTab_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._ssTab_TabPage1.Name = "_ssTab_TabPage1"
        Me._ssTab_TabPage1.Size = New System.Drawing.Size(400, 145)
        Me._ssTab_TabPage1.TabIndex = 1
        Me._ssTab_TabPage1.Text = "Modvat"
        '
        'FraSuppBill
        '
        Me.FraSuppBill.BackColor = System.Drawing.SystemColors.Control
        Me.FraSuppBill.Controls.Add(Me.txtServNo)
        Me.FraSuppBill.Controls.Add(Me.chkServTaxRefund)
        Me.FraSuppBill.Controls.Add(Me.chkPLA)
        Me.FraSuppBill.Controls.Add(Me.chkServTaxClaim)
        Me.FraSuppBill.Controls.Add(Me.chkSTClaim)
        Me.FraSuppBill.Controls.Add(Me.chkCapital)
        Me.FraSuppBill.Controls.Add(Me.chkSuppBill)
        Me.FraSuppBill.Controls.Add(Me.txtModvatNo)
        Me.FraSuppBill.Controls.Add(Me.txtSTRefundNo)
        Me.FraSuppBill.Controls.Add(Me.chkModvat)
        Me.FraSuppBill.Controls.Add(Me.Label24)
        Me.FraSuppBill.Controls.Add(Me.Label12)
        Me.FraSuppBill.Controls.Add(Me.Label13)
        Me.FraSuppBill.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraSuppBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraSuppBill.Location = New System.Drawing.Point(2, -3)
        Me.FraSuppBill.Name = "FraSuppBill"
        Me.FraSuppBill.Padding = New System.Windows.Forms.Padding(0)
        Me.FraSuppBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraSuppBill.Size = New System.Drawing.Size(398, 149)
        Me.FraSuppBill.TabIndex = 115
        Me.FraSuppBill.TabStop = False
        '
        'txtServNo
        '
        Me.txtServNo.AcceptsReturn = True
        Me.txtServNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtServNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServNo.Enabled = False
        Me.txtServNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServNo.Location = New System.Drawing.Point(260, 84)
        Me.txtServNo.MaxLength = 0
        Me.txtServNo.Name = "txtServNo"
        Me.txtServNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServNo.Size = New System.Drawing.Size(45, 20)
        Me.txtServNo.TabIndex = 45
        Me.txtServNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkServTaxRefund
        '
        Me.chkServTaxRefund.AutoSize = True
        Me.chkServTaxRefund.BackColor = System.Drawing.SystemColors.Control
        Me.chkServTaxRefund.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkServTaxRefund.Enabled = False
        Me.chkServTaxRefund.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkServTaxRefund.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkServTaxRefund.Location = New System.Drawing.Point(230, 28)
        Me.chkServTaxRefund.Name = "chkServTaxRefund"
        Me.chkServTaxRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkServTaxRefund.Size = New System.Drawing.Size(121, 18)
        Me.chkServTaxRefund.TabIndex = 42
        Me.chkServTaxRefund.Text = "Service Tax Refund"
        Me.chkServTaxRefund.UseVisualStyleBackColor = False
        '
        'chkPLA
        '
        Me.chkPLA.AutoSize = True
        Me.chkPLA.BackColor = System.Drawing.SystemColors.Control
        Me.chkPLA.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPLA.Enabled = False
        Me.chkPLA.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPLA.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPLA.Location = New System.Drawing.Point(306, 12)
        Me.chkPLA.Name = "chkPLA"
        Me.chkPLA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPLA.Size = New System.Drawing.Size(46, 18)
        Me.chkPLA.TabIndex = 39
        Me.chkPLA.Text = "PLA"
        Me.chkPLA.UseVisualStyleBackColor = False
        '
        'chkServTaxClaim
        '
        Me.chkServTaxClaim.AutoSize = True
        Me.chkServTaxClaim.BackColor = System.Drawing.SystemColors.Control
        Me.chkServTaxClaim.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkServTaxClaim.Enabled = False
        Me.chkServTaxClaim.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkServTaxClaim.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkServTaxClaim.Location = New System.Drawing.Point(112, 28)
        Me.chkServTaxClaim.Name = "chkServTaxClaim"
        Me.chkServTaxClaim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkServTaxClaim.Size = New System.Drawing.Size(111, 18)
        Me.chkServTaxClaim.TabIndex = 41
        Me.chkServTaxClaim.Text = "Service Tax Claim"
        Me.chkServTaxClaim.UseVisualStyleBackColor = False
        '
        'chkSTClaim
        '
        Me.chkSTClaim.AutoSize = True
        Me.chkSTClaim.BackColor = System.Drawing.SystemColors.Control
        Me.chkSTClaim.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSTClaim.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSTClaim.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSTClaim.Location = New System.Drawing.Point(6, 28)
        Me.chkSTClaim.Name = "chkSTClaim"
        Me.chkSTClaim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSTClaim.Size = New System.Drawing.Size(101, 18)
        Me.chkSTClaim.TabIndex = 40
        Me.chkSTClaim.Text = "Sales Tax Claim"
        Me.chkSTClaim.UseVisualStyleBackColor = False
        '
        'chkCapital
        '
        Me.chkCapital.AutoSize = True
        Me.chkCapital.BackColor = System.Drawing.SystemColors.Control
        Me.chkCapital.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCapital.Enabled = False
        Me.chkCapital.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCapital.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCapital.Location = New System.Drawing.Point(230, 12)
        Me.chkCapital.Name = "chkCapital"
        Me.chkCapital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCapital.Size = New System.Drawing.Size(58, 18)
        Me.chkCapital.TabIndex = 38
        Me.chkCapital.Text = "Capital"
        Me.chkCapital.UseVisualStyleBackColor = False
        '
        'chkSuppBill
        '
        Me.chkSuppBill.AutoSize = True
        Me.chkSuppBill.BackColor = System.Drawing.SystemColors.Control
        Me.chkSuppBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSuppBill.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSuppBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSuppBill.Location = New System.Drawing.Point(6, 12)
        Me.chkSuppBill.Name = "chkSuppBill"
        Me.chkSuppBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSuppBill.Size = New System.Drawing.Size(70, 18)
        Me.chkSuppBill.TabIndex = 36
        Me.chkSuppBill.Text = "Supp. Bill"
        Me.chkSuppBill.UseVisualStyleBackColor = False
        '
        'txtModvatNo
        '
        Me.txtModvatNo.AcceptsReturn = True
        Me.txtModvatNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtModvatNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModvatNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModvatNo.Enabled = False
        Me.txtModvatNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModvatNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModvatNo.Location = New System.Drawing.Point(92, 64)
        Me.txtModvatNo.MaxLength = 0
        Me.txtModvatNo.Name = "txtModvatNo"
        Me.txtModvatNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModvatNo.Size = New System.Drawing.Size(45, 20)
        Me.txtModvatNo.TabIndex = 43
        Me.txtModvatNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSTRefundNo
        '
        Me.txtSTRefundNo.AcceptsReturn = True
        Me.txtSTRefundNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTRefundNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTRefundNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTRefundNo.Enabled = False
        Me.txtSTRefundNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTRefundNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSTRefundNo.Location = New System.Drawing.Point(92, 84)
        Me.txtSTRefundNo.MaxLength = 0
        Me.txtSTRefundNo.Name = "txtSTRefundNo"
        Me.txtSTRefundNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTRefundNo.Size = New System.Drawing.Size(45, 20)
        Me.txtSTRefundNo.TabIndex = 44
        Me.txtSTRefundNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkModvat
        '
        Me.chkModvat.AutoSize = True
        Me.chkModvat.BackColor = System.Drawing.SystemColors.Control
        Me.chkModvat.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkModvat.Enabled = False
        Me.chkModvat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkModvat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkModvat.Location = New System.Drawing.Point(112, 12)
        Me.chkModvat.Name = "chkModvat"
        Me.chkModvat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkModvat.Size = New System.Drawing.Size(61, 18)
        Me.chkModvat.TabIndex = 37
        Me.chkModvat.Text = "Modvat"
        Me.chkModvat.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(174, 86)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(74, 14)
        Me.Label24.TabIndex = 139
        Me.Label24.Text = "Serv. Tax No :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(18, 66)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(64, 14)
        Me.Label12.TabIndex = 117
        Me.Label12.Text = "Modvat No :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(8, 86)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(70, 14)
        Me.Label13.TabIndex = 116
        Me.Label13.Text = "ST Claim No :"
        '
        '_ssTab_TabPage2
        '
        Me._ssTab_TabPage2.Controls.Add(Me.Frame2)
        Me._ssTab_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._ssTab_TabPage2.Name = "_ssTab_TabPage2"
        Me._ssTab_TabPage2.Size = New System.Drawing.Size(400, 145)
        Me._ssTab_TabPage2.TabIndex = 2
        Me._ssTab_TabPage2.Text = "Import Detail"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtImpBillDate)
        Me.Frame2.Controls.Add(Me.txtImpPartyName)
        Me.Frame2.Controls.Add(Me.txtImpBillNo)
        Me.Frame2.Controls.Add(Me.txtImpMRRNo)
        Me.Frame2.Controls.Add(Me.Label22)
        Me.Frame2.Controls.Add(Me.Label19)
        Me.Frame2.Controls.Add(Me.Label20)
        Me.Frame2.Controls.Add(Me.Label21)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(4, -1)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(396, 147)
        Me.Frame2.TabIndex = 118
        Me.Frame2.TabStop = False
        '
        'txtImpBillDate
        '
        Me.txtImpBillDate.AcceptsReturn = True
        Me.txtImpBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtImpBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpBillDate.Enabled = False
        Me.txtImpBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpBillDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpBillDate.Location = New System.Drawing.Point(80, 82)
        Me.txtImpBillDate.MaxLength = 0
        Me.txtImpBillDate.Name = "txtImpBillDate"
        Me.txtImpBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpBillDate.Size = New System.Drawing.Size(101, 20)
        Me.txtImpBillDate.TabIndex = 49
        '
        'txtImpPartyName
        '
        Me.txtImpPartyName.AcceptsReturn = True
        Me.txtImpPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtImpPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpPartyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpPartyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpPartyName.Location = New System.Drawing.Point(80, 16)
        Me.txtImpPartyName.MaxLength = 0
        Me.txtImpPartyName.Name = "txtImpPartyName"
        Me.txtImpPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpPartyName.Size = New System.Drawing.Size(229, 20)
        Me.txtImpPartyName.TabIndex = 46
        '
        'txtImpBillNo
        '
        Me.txtImpBillNo.AcceptsReturn = True
        Me.txtImpBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtImpBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpBillNo.Enabled = False
        Me.txtImpBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpBillNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpBillNo.Location = New System.Drawing.Point(80, 60)
        Me.txtImpBillNo.MaxLength = 0
        Me.txtImpBillNo.Name = "txtImpBillNo"
        Me.txtImpBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpBillNo.Size = New System.Drawing.Size(101, 20)
        Me.txtImpBillNo.TabIndex = 48
        '
        'txtImpMRRNo
        '
        Me.txtImpMRRNo.AcceptsReturn = True
        Me.txtImpMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtImpMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpMRRNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpMRRNo.Location = New System.Drawing.Point(80, 38)
        Me.txtImpMRRNo.MaxLength = 0
        Me.txtImpMRRNo.Name = "txtImpMRRNo"
        Me.txtImpMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpMRRNo.Size = New System.Drawing.Size(101, 20)
        Me.txtImpMRRNo.TabIndex = 47
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(4, 82)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(51, 14)
        Me.Label22.TabIndex = 122
        Me.Label22.Text = "Bill Date :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(4, 18)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(68, 14)
        Me.Label19.TabIndex = 121
        Me.Label19.Text = "Party Name :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(4, 60)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(42, 14)
        Me.Label20.TabIndex = 120
        Me.Label20.Text = "Bill No :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(4, 40)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(54, 14)
        Me.Label21.TabIndex = 119
        Me.Label21.Text = "MRR No. :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_ssTab_TabPage3
        '
        Me._ssTab_TabPage3.Controls.Add(Me.Frame1)
        Me._ssTab_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._ssTab_TabPage3.Name = "_ssTab_TabPage3"
        Me._ssTab_TabPage3.Size = New System.Drawing.Size(400, 145)
        Me._ssTab_TabPage3.TabIndex = 3
        Me._ssTab_TabPage3.Text = "Export Detail"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtExpBillNo)
        Me.Frame1.Controls.Add(Me.txtExpPartyName)
        Me.Frame1.Controls.Add(Me.txtExpBillDate)
        Me.Frame1.Controls.Add(Me.Label18)
        Me.Frame1.Controls.Add(Me.Label17)
        Me.Frame1.Controls.Add(Me.Label16)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(2, -4)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(398, 150)
        Me.Frame1.TabIndex = 123
        Me.Frame1.TabStop = False
        '
        'txtExpBillNo
        '
        Me.txtExpBillNo.AcceptsReturn = True
        Me.txtExpBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExpBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpBillNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExpBillNo.Location = New System.Drawing.Point(80, 38)
        Me.txtExpBillNo.MaxLength = 0
        Me.txtExpBillNo.Name = "txtExpBillNo"
        Me.txtExpBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpBillNo.Size = New System.Drawing.Size(101, 20)
        Me.txtExpBillNo.TabIndex = 51
        '
        'txtExpPartyName
        '
        Me.txtExpPartyName.AcceptsReturn = True
        Me.txtExpPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExpPartyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpPartyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExpPartyName.Location = New System.Drawing.Point(80, 16)
        Me.txtExpPartyName.MaxLength = 0
        Me.txtExpPartyName.Name = "txtExpPartyName"
        Me.txtExpPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpPartyName.Size = New System.Drawing.Size(229, 20)
        Me.txtExpPartyName.TabIndex = 50
        '
        'txtExpBillDate
        '
        Me.txtExpBillDate.AcceptsReturn = True
        Me.txtExpBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExpBillDate.Enabled = False
        Me.txtExpBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpBillDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExpBillDate.Location = New System.Drawing.Point(80, 60)
        Me.txtExpBillDate.MaxLength = 0
        Me.txtExpBillDate.Name = "txtExpBillDate"
        Me.txtExpBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpBillDate.Size = New System.Drawing.Size(101, 20)
        Me.txtExpBillDate.TabIndex = 52
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(4, 38)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(42, 14)
        Me.Label18.TabIndex = 126
        Me.Label18.Text = "Bill No :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(4, 18)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(68, 14)
        Me.Label17.TabIndex = 125
        Me.Label17.Text = "Cust. Name :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(4, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(51, 14)
        Me.Label16.TabIndex = 124
        Me.Label16.Text = "Bill Date :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_ssTab_TabPage4
        '
        Me._ssTab_TabPage4.Controls.Add(Me.Frame4)
        Me._ssTab_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._ssTab_TabPage4.Name = "_ssTab_TabPage4"
        Me._ssTab_TabPage4.Size = New System.Drawing.Size(400, 145)
        Me._ssTab_TabPage4.TabIndex = 4
        Me._ssTab_TabPage4.Text = "Service Detail"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtServiceTaxAmount)
        Me.Frame4.Controls.Add(Me.txtServiceTaxPer)
        Me.Frame4.Controls.Add(Me.txtRecipientPer)
        Me.Frame4.Controls.Add(Me.txtProviderPer)
        Me.Frame4.Controls.Add(Me.txtServiceOn)
        Me.Frame4.Controls.Add(Me.cmdServProvided)
        Me.Frame4.Controls.Add(Me.txtServProvided)
        Me.Frame4.Controls.Add(Me.Label30)
        Me.Frame4.Controls.Add(Me.Label29)
        Me.Frame4.Controls.Add(Me.Label28)
        Me.Frame4.Controls.Add(Me.Label27)
        Me.Frame4.Controls.Add(Me.Label26)
        Me.Frame4.Controls.Add(Me.Label25)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(3, -5)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(397, 151)
        Me.Frame4.TabIndex = 143
        Me.Frame4.TabStop = False
        '
        'txtServiceTaxAmount
        '
        Me.txtServiceTaxAmount.AcceptsReturn = True
        Me.txtServiceTaxAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtServiceTaxAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServiceTaxAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServiceTaxAmount.Enabled = False
        Me.txtServiceTaxAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceTaxAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServiceTaxAmount.Location = New System.Drawing.Point(228, 56)
        Me.txtServiceTaxAmount.MaxLength = 0
        Me.txtServiceTaxAmount.Name = "txtServiceTaxAmount"
        Me.txtServiceTaxAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServiceTaxAmount.Size = New System.Drawing.Size(81, 20)
        Me.txtServiceTaxAmount.TabIndex = 57
        '
        'txtServiceTaxPer
        '
        Me.txtServiceTaxPer.AcceptsReturn = True
        Me.txtServiceTaxPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtServiceTaxPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServiceTaxPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServiceTaxPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceTaxPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServiceTaxPer.Location = New System.Drawing.Point(96, 56)
        Me.txtServiceTaxPer.MaxLength = 0
        Me.txtServiceTaxPer.Name = "txtServiceTaxPer"
        Me.txtServiceTaxPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServiceTaxPer.Size = New System.Drawing.Size(49, 20)
        Me.txtServiceTaxPer.TabIndex = 56
        '
        'txtRecipientPer
        '
        Me.txtRecipientPer.AcceptsReturn = True
        Me.txtRecipientPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecipientPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecipientPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecipientPer.Enabled = False
        Me.txtRecipientPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecipientPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRecipientPer.Location = New System.Drawing.Point(228, 78)
        Me.txtRecipientPer.MaxLength = 0
        Me.txtRecipientPer.Name = "txtRecipientPer"
        Me.txtRecipientPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecipientPer.Size = New System.Drawing.Size(49, 20)
        Me.txtRecipientPer.TabIndex = 59
        '
        'txtProviderPer
        '
        Me.txtProviderPer.AcceptsReturn = True
        Me.txtProviderPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtProviderPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProviderPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProviderPer.Enabled = False
        Me.txtProviderPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProviderPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProviderPer.Location = New System.Drawing.Point(96, 78)
        Me.txtProviderPer.MaxLength = 0
        Me.txtProviderPer.Name = "txtProviderPer"
        Me.txtProviderPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProviderPer.Size = New System.Drawing.Size(49, 20)
        Me.txtProviderPer.TabIndex = 58
        '
        'txtServiceOn
        '
        Me.txtServiceOn.AcceptsReturn = True
        Me.txtServiceOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtServiceOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServiceOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServiceOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServiceOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServiceOn.Location = New System.Drawing.Point(96, 34)
        Me.txtServiceOn.MaxLength = 0
        Me.txtServiceOn.Name = "txtServiceOn"
        Me.txtServiceOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServiceOn.Size = New System.Drawing.Size(83, 20)
        Me.txtServiceOn.TabIndex = 55
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(150, 58)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(70, 14)
        Me.Label30.TabIndex = 149
        Me.Label30.Text = "Service Tax :"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(4, 58)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(83, 14)
        Me.Label29.TabIndex = 148
        Me.Label29.Text = "Service Tax % :"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(152, 80)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(70, 14)
        Me.Label28.TabIndex = 147
        Me.Label28.Text = "Recipient % :"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(4, 80)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(66, 14)
        Me.Label27.TabIndex = 146
        Me.Label27.Text = "Provider % :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(4, 36)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(67, 14)
        Me.Label26.TabIndex = 145
        Me.Label26.Text = "Service On :"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(4, 14)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(50, 14)
        Me.Label25.TabIndex = 144
        Me.Label25.Text = "Service :"
        '
        'txtNarration
        '
        Me.txtNarration.AcceptsReturn = True
        Me.txtNarration.BackColor = System.Drawing.SystemColors.Window
        Me.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNarration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNarration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNarration.Location = New System.Drawing.Point(423, 410)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Multiline = True
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(285, 120)
        Me.txtNarration.TabIndex = 60
        '
        'FraTDSFrame
        '
        Me.FraTDSFrame.BackColor = System.Drawing.SystemColors.Control
        Me.FraTDSFrame.Controls.Add(Me.chkISLowerDed)
        Me.FraTDSFrame.Controls.Add(Me.txtPName)
        Me.FraTDSFrame.Controls.Add(Me.txtSection)
        Me.FraTDSFrame.Controls.Add(Me.chkExempted)
        Me.FraTDSFrame.Controls.Add(Me.txtVD)
        Me.FraTDSFrame.Controls.Add(Me.txtAmountPaid)
        Me.FraTDSFrame.Controls.Add(Me.txtTdsRate)
        Me.FraTDSFrame.Controls.Add(Me.txtExempted)
        Me.FraTDSFrame.Controls.Add(Me.txtTDSAmount)
        Me.FraTDSFrame.Controls.Add(Me.TxtTDSAccount)
        Me.FraTDSFrame.Controls.Add(Me.cmdTDSHide)
        Me.FraTDSFrame.Controls.Add(Me.Label11)
        Me.FraTDSFrame.Controls.Add(Me.Label10)
        Me.FraTDSFrame.Controls.Add(Me.Label4)
        Me.FraTDSFrame.Controls.Add(Me.Label5)
        Me.FraTDSFrame.Controls.Add(Me.Label6)
        Me.FraTDSFrame.Controls.Add(Me.Label7)
        Me.FraTDSFrame.Controls.Add(Me.Label8)
        Me.FraTDSFrame.Controls.Add(Me.Label9)
        Me.FraTDSFrame.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTDSFrame.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FraTDSFrame.Location = New System.Drawing.Point(252, 98)
        Me.FraTDSFrame.Name = "FraTDSFrame"
        Me.FraTDSFrame.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTDSFrame.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTDSFrame.Size = New System.Drawing.Size(493, 173)
        Me.FraTDSFrame.TabIndex = 95
        Me.FraTDSFrame.TabStop = False
        Me.FraTDSFrame.Visible = False
        '
        'chkISLowerDed
        '
        Me.chkISLowerDed.AutoSize = True
        Me.chkISLowerDed.BackColor = System.Drawing.SystemColors.Control
        Me.chkISLowerDed.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkISLowerDed.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkISLowerDed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkISLowerDed.Location = New System.Drawing.Point(260, 154)
        Me.chkISLowerDed.Name = "chkISLowerDed"
        Me.chkISLowerDed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkISLowerDed.Size = New System.Drawing.Size(109, 18)
        Me.chkISLowerDed.TabIndex = 150
        Me.chkISLowerDed.Text = "Lower Deduction"
        Me.chkISLowerDed.UseVisualStyleBackColor = False
        '
        'txtPName
        '
        Me.txtPName.AcceptsReturn = True
        Me.txtPName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPName.Location = New System.Drawing.Point(140, 34)
        Me.txtPName.MaxLength = 0
        Me.txtPName.Name = "txtPName"
        Me.txtPName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPName.Size = New System.Drawing.Size(343, 20)
        Me.txtPName.TabIndex = 10
        '
        'txtSection
        '
        Me.txtSection.AcceptsReturn = True
        Me.txtSection.BackColor = System.Drawing.SystemColors.Window
        Me.txtSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSection.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSection.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSection.Location = New System.Drawing.Point(140, 56)
        Me.txtSection.MaxLength = 0
        Me.txtSection.Name = "txtSection"
        Me.txtSection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSection.Size = New System.Drawing.Size(343, 20)
        Me.txtSection.TabIndex = 11
        '
        'chkExempted
        '
        Me.chkExempted.AutoSize = True
        Me.chkExempted.BackColor = System.Drawing.SystemColors.Control
        Me.chkExempted.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExempted.Enabled = False
        Me.chkExempted.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExempted.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExempted.Location = New System.Drawing.Point(260, 138)
        Me.chkExempted.Name = "chkExempted"
        Me.chkExempted.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExempted.Size = New System.Drawing.Size(73, 18)
        Me.chkExempted.TabIndex = 17
        Me.chkExempted.Text = "Exempted"
        Me.chkExempted.UseVisualStyleBackColor = False
        '
        'txtVD
        '
        Me.txtVD.AcceptsReturn = True
        Me.txtVD.BackColor = System.Drawing.SystemColors.Window
        Me.txtVD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVD.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVD.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVD.Location = New System.Drawing.Point(140, 78)
        Me.txtVD.MaxLength = 0
        Me.txtVD.Name = "txtVD"
        Me.txtVD.ReadOnly = True
        Me.txtVD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVD.Size = New System.Drawing.Size(113, 20)
        Me.txtVD.TabIndex = 12
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.AcceptsReturn = True
        Me.txtAmountPaid.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountPaid.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmountPaid.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmountPaid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmountPaid.Location = New System.Drawing.Point(140, 100)
        Me.txtAmountPaid.MaxLength = 0
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmountPaid.Size = New System.Drawing.Size(113, 20)
        Me.txtAmountPaid.TabIndex = 13
        '
        'txtTdsRate
        '
        Me.txtTdsRate.AcceptsReturn = True
        Me.txtTdsRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtTdsRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTdsRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTdsRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTdsRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTdsRate.Location = New System.Drawing.Point(140, 122)
        Me.txtTdsRate.MaxLength = 0
        Me.txtTdsRate.Name = "txtTdsRate"
        Me.txtTdsRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTdsRate.Size = New System.Drawing.Size(113, 20)
        Me.txtTdsRate.TabIndex = 14
        '
        'txtExempted
        '
        Me.txtExempted.AcceptsReturn = True
        Me.txtExempted.BackColor = System.Drawing.SystemColors.Window
        Me.txtExempted.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExempted.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExempted.Enabled = False
        Me.txtExempted.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExempted.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExempted.Location = New System.Drawing.Point(140, 144)
        Me.txtExempted.MaxLength = 0
        Me.txtExempted.Name = "txtExempted"
        Me.txtExempted.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExempted.Size = New System.Drawing.Size(113, 20)
        Me.txtExempted.TabIndex = 16
        '
        'txtTDSAmount
        '
        Me.txtTDSAmount.AcceptsReturn = True
        Me.txtTDSAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSAmount.Enabled = False
        Me.txtTDSAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSAmount.Location = New System.Drawing.Point(370, 122)
        Me.txtTDSAmount.MaxLength = 0
        Me.txtTDSAmount.Name = "txtTDSAmount"
        Me.txtTDSAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSAmount.Size = New System.Drawing.Size(113, 20)
        Me.txtTDSAmount.TabIndex = 15
        '
        'cmdTDSHide
        '
        Me.cmdTDSHide.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTDSHide.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTDSHide.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTDSHide.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTDSHide.Location = New System.Drawing.Point(424, 146)
        Me.cmdTDSHide.Name = "cmdTDSHide"
        Me.cmdTDSHide.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTDSHide.Size = New System.Drawing.Size(65, 24)
        Me.cmdTDSHide.TabIndex = 18
        Me.cmdTDSHide.Text = "&OK"
        Me.cmdTDSHide.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(8, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(68, 14)
        Me.Label11.TabIndex = 103
        Me.Label11.Text = "Party Name :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(8, 56)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(49, 14)
        Me.Label10.TabIndex = 102
        Me.Label10.Text = "Section :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(92, 14)
        Me.Label4.TabIndex = 101
        Me.Label4.Text = "Date of Payment :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 100)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(113, 14)
        Me.Label5.TabIndex = 100
        Me.Label5.Text = "Amount Paid/Credited:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(8, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(86, 14)
        Me.Label6.TabIndex = 99
        Me.Label6.Text = "Deduction Rate :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(8, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(104, 14)
        Me.Label7.TabIndex = 98
        Me.Label7.Text = "Exemption Cert. No :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(260, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(98, 14)
        Me.Label8.TabIndex = 97
        Me.Label8.Text = "Deducted Amount :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(8, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(106, 14)
        Me.Label9.TabIndex = 96
        Me.Label9.Text = "TDS Account Name :"
        '
        'txtVNoSuffix
        '
        Me.txtVNoSuffix.AcceptsReturn = True
        Me.txtVNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNoSuffix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNoSuffix.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtVNoSuffix.Location = New System.Drawing.Point(354, 43)
        Me.txtVNoSuffix.MaxLength = 0
        Me.txtVNoSuffix.Name = "txtVNoSuffix"
        Me.txtVNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoSuffix.Size = New System.Drawing.Size(28, 20)
        Me.txtVNoSuffix.TabIndex = 6
        '
        'txtVType
        '
        Me.txtVType.AcceptsReturn = True
        Me.txtVType.BackColor = System.Drawing.SystemColors.Window
        Me.txtVType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVType.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtVType.Location = New System.Drawing.Point(203, 43)
        Me.txtVType.MaxLength = 0
        Me.txtVType.Name = "txtVType"
        Me.txtVType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVType.Size = New System.Drawing.Size(44, 20)
        Me.txtVType.TabIndex = 4
        '
        'TxtVDate
        '
        Me.TxtVDate.AcceptsReturn = True
        Me.TxtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.TxtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVDate.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.TxtVDate.Location = New System.Drawing.Point(90, 43)
        Me.TxtVDate.MaxLength = 0
        Me.TxtVDate.Name = "TxtVDate"
        Me.TxtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtVDate.Size = New System.Drawing.Size(70, 20)
        Me.TxtVDate.TabIndex = 2
        '
        'txtVno
        '
        Me.txtVno.AcceptsReturn = True
        Me.txtVno.BackColor = System.Drawing.SystemColors.Window
        Me.txtVno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVno.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVno.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtVno.Location = New System.Drawing.Point(300, 43)
        Me.txtVno.MaxLength = 0
        Me.txtVno.Name = "txtVno"
        Me.txtVno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVno.Size = New System.Drawing.Size(54, 20)
        Me.txtVno.TabIndex = 5
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
        Me.txtVNo1.Location = New System.Drawing.Point(248, 43)
        Me.txtVNo1.MaxLength = 0
        Me.txtVNo1.Name = "txtVNo1"
        Me.txtVNo1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo1.Size = New System.Drawing.Size(51, 20)
        Me.txtVNo1.TabIndex = 3
        Me.txtVNo1.Visible = False
        '
        'txtPartyName
        '
        Me.txtPartyName.AcceptsReturn = True
        Me.txtPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartyName.Location = New System.Drawing.Point(90, 13)
        Me.txtPartyName.MaxLength = 0
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyName.Size = New System.Drawing.Size(431, 20)
        Me.txtPartyName.TabIndex = 0
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCancelled.Location = New System.Drawing.Point(541, 43)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(73, 18)
        Me.chkCancelled.TabIndex = 63
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'chkChqDeposit
        '
        Me.chkChqDeposit.AutoSize = True
        Me.chkChqDeposit.BackColor = System.Drawing.SystemColors.Control
        Me.chkChqDeposit.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkChqDeposit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkChqDeposit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkChqDeposit.Location = New System.Drawing.Point(786, 559)
        Me.chkChqDeposit.Name = "chkChqDeposit"
        Me.chkChqDeposit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkChqDeposit.Size = New System.Drawing.Size(102, 18)
        Me.chkChqDeposit.TabIndex = 62
        Me.chkChqDeposit.Text = "Cheque Deposit"
        Me.chkChqDeposit.UseVisualStyleBackColor = False
        '
        'ChkACPayee
        '
        Me.ChkACPayee.AutoSize = True
        Me.ChkACPayee.BackColor = System.Drawing.SystemColors.Control
        Me.ChkACPayee.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkACPayee.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkACPayee.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkACPayee.Location = New System.Drawing.Point(786, 535)
        Me.ChkACPayee.Name = "ChkACPayee"
        Me.ChkACPayee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkACPayee.Size = New System.Drawing.Size(102, 18)
        Me.ChkACPayee.TabIndex = 61
        Me.ChkACPayee.Text = "A/C Payee Only"
        Me.ChkACPayee.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(98, 148)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 153
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 70)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(902, 314)
        Me.SprdMain.TabIndex = 8
        '
        'lblReversalMkey
        '
        Me.lblReversalMkey.AutoSize = True
        Me.lblReversalMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblReversalMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblReversalMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReversalMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReversalMkey.Location = New System.Drawing.Point(626, 412)
        Me.lblReversalMkey.Name = "lblReversalMkey"
        Me.lblReversalMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblReversalMkey.Size = New System.Drawing.Size(85, 14)
        Me.lblReversalMkey.TabIndex = 156
        Me.lblReversalMkey.Text = "lblReversalMkey"
        '
        'lblReversalVoucher
        '
        Me.lblReversalVoucher.AutoSize = True
        Me.lblReversalVoucher.BackColor = System.Drawing.SystemColors.Control
        Me.lblReversalVoucher.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblReversalVoucher.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReversalVoucher.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReversalVoucher.Location = New System.Drawing.Point(460, 412)
        Me.lblReversalVoucher.Name = "lblReversalVoucher"
        Me.lblReversalVoucher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblReversalVoucher.Size = New System.Drawing.Size(101, 14)
        Me.lblReversalVoucher.TabIndex = 155
        Me.lblReversalVoucher.Text = "lblReversalVoucher"
        '
        'lblReversalMade
        '
        Me.lblReversalMade.AutoSize = True
        Me.lblReversalMade.BackColor = System.Drawing.SystemColors.Control
        Me.lblReversalMade.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblReversalMade.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReversalMade.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReversalMade.Location = New System.Drawing.Point(276, 412)
        Me.lblReversalMade.Name = "lblReversalMade"
        Me.lblReversalMade.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblReversalMade.Size = New System.Drawing.Size(86, 14)
        Me.lblReversalMade.TabIndex = 154
        Me.lblReversalMade.Text = "lblReversalMade"
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
        Me.lblSaleBillNo.TabIndex = 153
        '
        'lblELYear
        '
        Me.lblELYear.BackColor = System.Drawing.SystemColors.Control
        Me.lblELYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblELYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblELYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblELYear.Location = New System.Drawing.Point(398, 436)
        Me.lblELYear.Name = "lblELYear"
        Me.lblELYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblELYear.Size = New System.Drawing.Size(31, 15)
        Me.lblELYear.TabIndex = 151
        Me.lblELYear.Text = "lblELYear"
        '
        'lblAcBalDCDiv
        '
        Me.lblAcBalDCDiv.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcBalDCDiv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAcBalDCDiv.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcBalDCDiv.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcBalDCDiv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAcBalDCDiv.Location = New System.Drawing.Point(880, 420)
        Me.lblAcBalDCDiv.Name = "lblAcBalDCDiv"
        Me.lblAcBalDCDiv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcBalDCDiv.Size = New System.Drawing.Size(27, 20)
        Me.lblAcBalDCDiv.TabIndex = 142
        Me.lblAcBalDCDiv.Text = "lblAcBalDCDiv"
        '
        'lblAcBalAmtDiv
        '
        Me.lblAcBalAmtDiv.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcBalAmtDiv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAcBalAmtDiv.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcBalAmtDiv.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcBalAmtDiv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAcBalAmtDiv.Location = New System.Drawing.Point(784, 420)
        Me.lblAcBalAmtDiv.Name = "lblAcBalAmtDiv"
        Me.lblAcBalAmtDiv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcBalAmtDiv.Size = New System.Drawing.Size(95, 20)
        Me.lblAcBalAmtDiv.TabIndex = 141
        Me.lblAcBalAmtDiv.Text = "lblAcBalAmtDiv"
        Me.lblAcBalAmtDiv.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblEmpCode
        '
        Me.lblEmpCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmpCode.Location = New System.Drawing.Point(606, 394)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmpCode.Size = New System.Drawing.Size(53, 13)
        Me.lblEmpCode.TabIndex = 138
        Me.lblEmpCode.Text = "lblEmpCode"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(387, 46)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(80, 14)
        Me.Label23.TabIndex = 137
        Me.Label23.Text = "Expense Date :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblServiceTaxDetail
        '
        Me.lblServiceTaxDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblServiceTaxDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblServiceTaxDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceTaxDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblServiceTaxDetail.Location = New System.Drawing.Point(498, 390)
        Me.lblServiceTaxDetail.Name = "lblServiceTaxDetail"
        Me.lblServiceTaxDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblServiceTaxDetail.Size = New System.Drawing.Size(67, 11)
        Me.lblServiceTaxDetail.TabIndex = 136
        Me.lblServiceTaxDetail.Text = "lblServiceTaxDetail"
        Me.lblServiceTaxDetail.Visible = False
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(681, 559)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 135
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(622, 561)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(62, 15)
        Me.Label48.TabIndex = 134
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
        Me.lblAddDate.Location = New System.Drawing.Point(681, 541)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 133
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(621, 543)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 132
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
        Me.lblModUser.Location = New System.Drawing.Point(485, 559)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 131
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(425, 561)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(63, 15)
        Me.Label15.TabIndex = 130
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
        Me.lblAddUser.Location = New System.Drawing.Point(485, 541)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 129
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(426, 543)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(64, 15)
        Me.Label44.TabIndex = 128
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
        Me.lblAcBalAmt.Location = New System.Drawing.Point(784, 389)
        Me.lblAcBalAmt.Name = "lblAcBalAmt"
        Me.lblAcBalAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcBalAmt.Size = New System.Drawing.Size(95, 20)
        Me.lblAcBalAmt.TabIndex = 106
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
        Me.lblAcBalDC.Location = New System.Drawing.Point(880, 389)
        Me.lblAcBalDC.Name = "lblAcBalDC"
        Me.lblAcBalDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcBalDC.Size = New System.Drawing.Size(27, 20)
        Me.lblAcBalDC.TabIndex = 105
        Me.lblAcBalDC.Text = "lblAcBalDC"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(689, 391)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(90, 14)
        Me.Label14.TabIndex = 104
        Me.Label14.Text = "A/c Current Bal. :"
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(426, 432)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(65, 19)
        Me.lblBookType.TabIndex = 87
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(684, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(70, 14)
        Me.Label3.TabIndex = 78
        Me.Label3.Text = "Current Bal. :"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(423, 391)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(65, 15)
        Me.Label2.TabIndex = 84
        Me.Label2.Text = "Narration :"
        '
        'lblBookBalDC
        '
        Me.lblBookBalDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookBalDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBookBalDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookBalDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookBalDC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBookBalDC.Location = New System.Drawing.Point(870, 43)
        Me.lblBookBalDC.Name = "lblBookBalDC"
        Me.lblBookBalDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookBalDC.Size = New System.Drawing.Size(29, 20)
        Me.lblBookBalDC.TabIndex = 80
        Me.lblBookBalDC.Text = "lblBookBalDC"
        '
        'LblDate_Issue_Receive
        '
        Me.LblDate_Issue_Receive.AutoSize = True
        Me.LblDate_Issue_Receive.BackColor = System.Drawing.SystemColors.Control
        Me.LblDate_Issue_Receive.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDate_Issue_Receive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate_Issue_Receive.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDate_Issue_Receive.Location = New System.Drawing.Point(6, 46)
        Me.LblDate_Issue_Receive.Name = "LblDate_Issue_Receive"
        Me.LblDate_Issue_Receive.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDate_Issue_Receive.Size = New System.Drawing.Size(79, 14)
        Me.LblDate_Issue_Receive.TabIndex = 76
        Me.LblDate_Issue_Receive.Text = "Voucher Date :"
        Me.LblDate_Issue_Receive.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(164, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(37, 14)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "VNo. :"
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAccount.Location = New System.Drawing.Point(6, 16)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccount.Size = New System.Drawing.Size(84, 14)
        Me.lblAccount.TabIndex = 75
        Me.lblAccount.Text = "Account Name :"
        Me.lblAccount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBookBalAmt
        '
        Me.lblBookBalAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookBalAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBookBalAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookBalAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookBalAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBookBalAmt.Location = New System.Drawing.Point(758, 43)
        Me.lblBookBalAmt.Name = "lblBookBalAmt"
        Me.lblBookBalAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookBalAmt.Size = New System.Drawing.Size(109, 20)
        Me.lblBookBalAmt.TabIndex = 79
        Me.lblBookBalAmt.Text = "lblBookBalAmt"
        Me.lblBookBalAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblTotal
        '
        Me.LblTotal.AutoSize = True
        Me.LblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.LblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.ForeColor = System.Drawing.Color.Black
        Me.LblTotal.Location = New System.Drawing.Point(11, 223)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotal.Size = New System.Drawing.Size(35, 14)
        Me.LblTotal.TabIndex = 81
        Me.LblTotal.Text = "Total :"
        '
        'LblDr
        '
        Me.LblDr.AutoSize = True
        Me.LblDr.BackColor = System.Drawing.SystemColors.Control
        Me.LblDr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDr.ForeColor = System.Drawing.Color.Black
        Me.LblDr.Location = New System.Drawing.Point(755, 454)
        Me.LblDr.Name = "LblDr"
        Me.LblDr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDr.Size = New System.Drawing.Size(24, 14)
        Me.LblDr.TabIndex = 82
        Me.LblDr.Text = "Dr :"
        '
        'LblDrAmt
        '
        Me.LblDrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblDrAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblDrAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDrAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDrAmt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LblDrAmt.Location = New System.Drawing.Point(784, 451)
        Me.LblDrAmt.Name = "LblDrAmt"
        Me.LblDrAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDrAmt.Size = New System.Drawing.Size(95, 17)
        Me.LblDrAmt.TabIndex = 83
        Me.LblDrAmt.Text = "LblDrAmt"
        Me.LblDrAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblCr
        '
        Me.LblCr.AutoSize = True
        Me.LblCr.BackColor = System.Drawing.SystemColors.Control
        Me.LblCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCr.ForeColor = System.Drawing.Color.Black
        Me.LblCr.Location = New System.Drawing.Point(755, 482)
        Me.LblCr.Name = "LblCr"
        Me.LblCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCr.Size = New System.Drawing.Size(24, 14)
        Me.LblCr.TabIndex = 85
        Me.LblCr.Text = "Cr :"
        '
        'LblCrAmt
        '
        Me.LblCrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblCrAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCrAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCrAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCrAmt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LblCrAmt.Location = New System.Drawing.Point(784, 479)
        Me.LblCrAmt.Name = "LblCrAmt"
        Me.LblCrAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCrAmt.Size = New System.Drawing.Size(95, 17)
        Me.LblCrAmt.TabIndex = 86
        Me.LblCrAmt.Text = "LblCrAmt"
        Me.LblCrAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblNet
        '
        Me.LblNet.AutoSize = True
        Me.LblNet.BackColor = System.Drawing.SystemColors.Control
        Me.LblNet.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNet.ForeColor = System.Drawing.Color.Black
        Me.LblNet.Location = New System.Drawing.Point(750, 510)
        Me.LblNet.Name = "LblNet"
        Me.LblNet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNet.Size = New System.Drawing.Size(29, 14)
        Me.LblNet.TabIndex = 88
        Me.LblNet.Text = "Net :"
        '
        'LblNetAmt
        '
        Me.LblNetAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblNetAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNetAmt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetAmt.ForeColor = System.Drawing.SystemColors.Highlight
        Me.LblNetAmt.Location = New System.Drawing.Point(784, 507)
        Me.LblNetAmt.Name = "LblNetAmt"
        Me.LblNetAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNetAmt.Size = New System.Drawing.Size(95, 17)
        Me.LblNetAmt.TabIndex = 89
        Me.LblNetAmt.Text = "LblNetAmt"
        Me.LblNetAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPaymentDetail
        '
        Me.lblPaymentDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaymentDetail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaymentDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentDetail.Location = New System.Drawing.Point(880, 507)
        Me.lblPaymentDetail.Name = "lblPaymentDetail"
        Me.lblPaymentDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentDetail.Size = New System.Drawing.Size(27, 17)
        Me.lblPaymentDetail.TabIndex = 90
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
        Me.fraGridView.Size = New System.Drawing.Size(910, 580)
        Me.fraGridView.TabIndex = 91
        Me.fraGridView.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(908, 570)
        Me.SprdView.TabIndex = 92
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
        Me.Frame3.Size = New System.Drawing.Size(910, 51)
        Me.Frame3.TabIndex = 93
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
        Me.lblSR.TabIndex = 108
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
        Me.lblLoanDetail.TabIndex = 107
        Me.lblLoanDetail.Text = "lblLoanDetail"
        '
        'lblYM
        '
        Me.lblYM.AutoSize = True
        Me.lblYM.BackColor = System.Drawing.SystemColors.Control
        Me.lblYM.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblYM.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblYM.Location = New System.Drawing.Point(1, 10)
        Me.lblYM.Name = "lblYM"
        Me.lblYM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblYM.Size = New System.Drawing.Size(33, 14)
        Me.lblYM.TabIndex = 94
        Me.lblYM.Text = "lblYM"
        Me.lblYM.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblYM.Visible = False
        '
        'ADataGrid
        '
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.CommandTimeout = 0
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.ConnectionString = Nothing
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Location = New System.Drawing.Point(0, 0)
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.Name = "ADataGrid"
        Me.ADataGrid.Size = New System.Drawing.Size(313, 33)
        Me.ADataGrid.TabIndex = 94
        Me.ADataGrid.Text = "Adodc"
        '
        'frmAtrn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraTrans)
        Me.Controls.Add(Me.fraGridView)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.ADataGrid)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-238, 2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAtrn"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Transaction"
        Me.FraTrans.ResumeLayout(False)
        Me.FraTrans.PerformLayout()
        Me.ssTab.ResumeLayout(False)
        Me._ssTab_TabPage0.ResumeLayout(False)
        Me.FraTDSDeduction.ResumeLayout(False)
        Me.FraTDSDeduction.PerformLayout()
        Me._ssTab_TabPage1.ResumeLayout(False)
        Me.FraSuppBill.ResumeLayout(False)
        Me.FraSuppBill.PerformLayout()
        Me._ssTab_TabPage2.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me._ssTab_TabPage3.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me._ssTab_TabPage4.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.FraTDSFrame.ResumeLayout(False)
        Me.FraTDSFrame.PerformLayout()
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
    Public WithEvents Label50 As Label
    Public WithEvents txtTDSSection As TextBox
    Public WithEvents txtPopulateVNo As TextBox
    Public WithEvents Label31 As Label
#End Region
End Class