Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmDrCrNoteGST
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
    Public WithEvents SprdPostingDetail As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraPostingDtl As System.Windows.Forms.GroupBox
    Public WithEvents cboGSTStatus As System.Windows.Forms.ComboBox
    Public WithEvents txtVType As System.Windows.Forms.TextBox
    Public WithEvents cmdResetMRR As System.Windows.Forms.Button
    Public WithEvents txtMRRDate As System.Windows.Forms.TextBox
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents cmdMRRNoSearch As System.Windows.Forms.Button
    Public WithEvents cmdBillNoSearch As System.Windows.Forms.Button
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents cboPopulateFrom As System.Windows.Forms.ComboBox
    Public WithEvents cmdVNoSearch As System.Windows.Forms.Button
    Public WithEvents txtPurVNo As System.Windows.Forms.TextBox
    Public WithEvents txtPurVDate As System.Windows.Forms.TextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblDNFrom As System.Windows.Forms.Label
    Public WithEvents lblPurVNO As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtReason As System.Windows.Forms.TextBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents txtVNoSuffix As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents chkAproved As System.Windows.Forms.CheckBox
    Public WithEvents txtCreditAccount As System.Windows.Forms.TextBox
    Public WithEvents txtDebitAccount As System.Windows.Forms.TextBox
    Public WithEvents txtIGSTRefundAmount As System.Windows.Forms.TextBox
    Public WithEvents txtSGSTRefundAmount As System.Windows.Forms.TextBox
    Public WithEvents txtCGSTRefundAmount As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents FraRefund As System.Windows.Forms.GroupBox
    Public WithEvents txtRecdDate As System.Windows.Forms.TextBox
    Public WithEvents txtPartyDNDate As System.Windows.Forms.TextBox
    Public WithEvents txtPartyDNNo As System.Windows.Forms.TextBox
    Public WithEvents txtNarration As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdExp As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblTotOthAmount As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblTotIGSTAmount As System.Windows.Forms.Label
    Public WithEvents lblServiceAmount As System.Windows.Forms.Label
    Public WithEvents lblServicePercentage As System.Windows.Forms.Label
    Public WithEvents lblTotQty As System.Windows.Forms.Label
    Public WithEvents lblTotPackQtyCap As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblTotItemValue As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblTotCGSTAmount As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblTotSGSTAmount As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents LblBookCode As System.Windows.Forms.Label
    Public WithEvents lblTotCharges As System.Windows.Forms.Label
    Public WithEvents lblTotFreight As System.Windows.Forms.Label
    Public WithEvents lblTotExpAmt As System.Windows.Forms.Label
    Public WithEvents lblEDPercentage As System.Windows.Forms.Label
    Public WithEvents lblSTPercentage As System.Windows.Forms.Label
    Public WithEvents lblTotTaxableAmt As System.Windows.Forms.Label
    Public WithEvents lblDiscount As System.Windows.Forms.Label
    Public WithEvents lblSurcharge As System.Windows.Forms.Label
    Public WithEvents lblRO As System.Windows.Forms.Label
    Public WithEvents lblMSC As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents txtVNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents lblDNCNSeqType As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents lblDCType As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblVNo As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents lblPayDate As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPostingHead As System.Windows.Forms.Button
    Public WithEvents cmdBarCode As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents txtSTRefundDate As System.Windows.Forms.TextBox
    Public WithEvents lblSODates As System.Windows.Forms.Label
    Public WithEvents lblSONos As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDrCrNoteGST))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdResetMRR = New System.Windows.Forms.Button()
        Me.cmdMRRNoSearch = New System.Windows.Forms.Button()
        Me.cmdBillNoSearch = New System.Windows.Forms.Button()
        Me.cmdVNoSearch = New System.Windows.Forms.Button()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPostingHead = New System.Windows.Forms.Button()
        Me.cmdBarCode = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdBillToSearch = New System.Windows.Forms.Button()
        Me.FraPostingDtl = New System.Windows.Forms.GroupBox()
        Me.SprdPostingDetail = New AxFPSpreadADO.AxfpSpread()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.cboGSTStatus = New System.Windows.Forms.ComboBox()
        Me.txtVType = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtMRRDate = New System.Windows.Forms.TextBox()
        Me.txtMRRNo = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.cboPopulateFrom = New System.Windows.Forms.ComboBox()
        Me.txtPurVNo = New System.Windows.Forms.TextBox()
        Me.txtPurVDate = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblDNFrom = New System.Windows.Forms.Label()
        Me.lblPurVNO = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.txtVNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.chkAproved = New System.Windows.Forms.CheckBox()
        Me.txtCreditAccount = New System.Windows.Forms.TextBox()
        Me.txtDebitAccount = New System.Windows.Forms.TextBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.FraRefund = New System.Windows.Forms.GroupBox()
        Me.txtIGSTRefundAmount = New System.Windows.Forms.TextBox()
        Me.txtSGSTRefundAmount = New System.Windows.Forms.TextBox()
        Me.txtCGSTRefundAmount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtRecdDate = New System.Windows.Forms.TextBox()
        Me.txtPartyDNDate = New System.Windows.Forms.TextBox()
        Me.txtPartyDNNo = New System.Windows.Forms.TextBox()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdExp = New AxFPSpreadADO.AxfpSpread()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblTotOthAmount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTotIGSTAmount = New System.Windows.Forms.Label()
        Me.lblServiceAmount = New System.Windows.Forms.Label()
        Me.lblServicePercentage = New System.Windows.Forms.Label()
        Me.lblTotQty = New System.Windows.Forms.Label()
        Me.lblTotPackQtyCap = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTotCGSTAmount = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblTotSGSTAmount = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.LblBookCode = New System.Windows.Forms.Label()
        Me.lblTotCharges = New System.Windows.Forms.Label()
        Me.lblTotFreight = New System.Windows.Forms.Label()
        Me.lblTotExpAmt = New System.Windows.Forms.Label()
        Me.lblEDPercentage = New System.Windows.Forms.Label()
        Me.lblSTPercentage = New System.Windows.Forms.Label()
        Me.lblTotTaxableAmt = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblSurcharge = New System.Windows.Forms.Label()
        Me.lblRO = New System.Windows.Forms.Label()
        Me.lblMSC = New System.Windows.Forms.Label()
        Me.txtVNoPrefix = New System.Windows.Forms.TextBox()
        Me.lblDNCNSeqType = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblVNo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.lblPayDate = New System.Windows.Forms.Label()
        Me.lblDCType = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.txtSTRefundDate = New System.Windows.Forms.TextBox()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.FraPostingDtl.SuspendLayout()
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraFront.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.FraRefund.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdResetMRR
        '
        Me.cmdResetMRR.BackColor = System.Drawing.SystemColors.Control
        Me.cmdResetMRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdResetMRR.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdResetMRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdResetMRR.Image = CType(resources.GetObject("cmdResetMRR.Image"), System.Drawing.Image)
        Me.cmdResetMRR.Location = New System.Drawing.Point(788, 150)
        Me.cmdResetMRR.Name = "cmdResetMRR"
        Me.cmdResetMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdResetMRR.Size = New System.Drawing.Size(27, 19)
        Me.cmdResetMRR.TabIndex = 89
        Me.cmdResetMRR.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdResetMRR, "Preview")
        Me.cmdResetMRR.UseVisualStyleBackColor = False
        '
        'cmdMRRNoSearch
        '
        Me.cmdMRRNoSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdMRRNoSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMRRNoSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMRRNoSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMRRNoSearch.Image = CType(resources.GetObject("cmdMRRNoSearch.Image"), System.Drawing.Image)
        Me.cmdMRRNoSearch.Location = New System.Drawing.Point(186, 109)
        Me.cmdMRRNoSearch.Name = "cmdMRRNoSearch"
        Me.cmdMRRNoSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMRRNoSearch.Size = New System.Drawing.Size(23, 21)
        Me.cmdMRRNoSearch.TabIndex = 92
        Me.cmdMRRNoSearch.TabStop = False
        Me.cmdMRRNoSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdMRRNoSearch, "Seach Pending DC")
        Me.cmdMRRNoSearch.UseVisualStyleBackColor = False
        '
        'cmdBillNoSearch
        '
        Me.cmdBillNoSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBillNoSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBillNoSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBillNoSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBillNoSearch.Image = CType(resources.GetObject("cmdBillNoSearch.Image"), System.Drawing.Image)
        Me.cmdBillNoSearch.Location = New System.Drawing.Point(186, 77)
        Me.cmdBillNoSearch.Name = "cmdBillNoSearch"
        Me.cmdBillNoSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillNoSearch.Size = New System.Drawing.Size(23, 21)
        Me.cmdBillNoSearch.TabIndex = 91
        Me.cmdBillNoSearch.TabStop = False
        Me.cmdBillNoSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBillNoSearch, "Seach Pending DC")
        Me.cmdBillNoSearch.UseVisualStyleBackColor = False
        '
        'cmdVNoSearch
        '
        Me.cmdVNoSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdVNoSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdVNoSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdVNoSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdVNoSearch.Image = CType(resources.GetObject("cmdVNoSearch.Image"), System.Drawing.Image)
        Me.cmdVNoSearch.Location = New System.Drawing.Point(186, 47)
        Me.cmdVNoSearch.Name = "cmdVNoSearch"
        Me.cmdVNoSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdVNoSearch.Size = New System.Drawing.Size(23, 21)
        Me.cmdVNoSearch.TabIndex = 80
        Me.cmdVNoSearch.TabStop = False
        Me.cmdVNoSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdVNoSearch, "Seach Pending DC")
        Me.cmdVNoSearch.UseVisualStyleBackColor = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(703, 150)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(81, 14)
        Me.Label18.TabIndex = 90
        Me.Label18.Text = "Reset Bill No :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label18, "AWB/RRP No.")
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(756, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 21
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
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(690, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 20
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
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(624, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 19
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(557, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 18
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
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(491, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 17
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdPostingHead
        '
        Me.cmdPostingHead.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPostingHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPostingHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPostingHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPostingHead.Image = CType(resources.GetObject("cmdPostingHead.Image"), System.Drawing.Image)
        Me.cmdPostingHead.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPostingHead.Location = New System.Drawing.Point(424, 10)
        Me.cmdPostingHead.Name = "cmdPostingHead"
        Me.cmdPostingHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPostingHead.Size = New System.Drawing.Size(67, 37)
        Me.cmdPostingHead.TabIndex = 64
        Me.cmdPostingHead.Text = "&Posting Detail"
        Me.cmdPostingHead.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPostingHead, "Delete")
        Me.cmdPostingHead.UseVisualStyleBackColor = False
        '
        'cmdBarCode
        '
        Me.cmdBarCode.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBarCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBarCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBarCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBarCode.Image = CType(resources.GetObject("cmdBarCode.Image"), System.Drawing.Image)
        Me.cmdBarCode.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdBarCode.Location = New System.Drawing.Point(358, 10)
        Me.cmdBarCode.Name = "cmdBarCode"
        Me.cmdBarCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBarCode.Size = New System.Drawing.Size(67, 37)
        Me.cmdBarCode.TabIndex = 16
        Me.cmdBarCode.Text = "&Refund"
        Me.cmdBarCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBarCode, "Delete")
        Me.cmdBarCode.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(291, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 15
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
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSave.Location = New System.Drawing.Point(224, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 14
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
        Me.cmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdModify.Location = New System.Drawing.Point(158, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 13
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
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdd.Location = New System.Drawing.Point(92, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdBillToSearch
        '
        Me.cmdBillToSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBillToSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBillToSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBillToSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBillToSearch.Image = CType(resources.GetObject("cmdBillToSearch.Image"), System.Drawing.Image)
        Me.cmdBillToSearch.Location = New System.Drawing.Point(456, 100)
        Me.cmdBillToSearch.Name = "cmdBillToSearch"
        Me.cmdBillToSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillToSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdBillToSearch.TabIndex = 147
        Me.cmdBillToSearch.TabStop = False
        Me.cmdBillToSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBillToSearch, "Search")
        Me.cmdBillToSearch.UseVisualStyleBackColor = False
        '
        'FraPostingDtl
        '
        Me.FraPostingDtl.BackColor = System.Drawing.SystemColors.Control
        Me.FraPostingDtl.Controls.Add(Me.SprdPostingDetail)
        Me.FraPostingDtl.Enabled = False
        Me.FraPostingDtl.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPostingDtl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPostingDtl.Location = New System.Drawing.Point(0, 196)
        Me.FraPostingDtl.Name = "FraPostingDtl"
        Me.FraPostingDtl.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPostingDtl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPostingDtl.Size = New System.Drawing.Size(414, 207)
        Me.FraPostingDtl.TabIndex = 65
        Me.FraPostingDtl.TabStop = False
        Me.FraPostingDtl.Visible = False
        '
        'SprdPostingDetail
        '
        Me.SprdPostingDetail.DataSource = Nothing
        Me.SprdPostingDetail.Location = New System.Drawing.Point(2, 9)
        Me.SprdPostingDetail.Name = "SprdPostingDetail"
        Me.SprdPostingDetail.OcxState = CType(resources.GetObject("SprdPostingDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPostingDetail.Size = New System.Drawing.Size(407, 195)
        Me.SprdPostingDetail.TabIndex = 66
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.cmdBillToSearch)
        Me.FraFront.Controls.Add(Me.txtBillTo)
        Me.FraFront.Controls.Add(Me.Label37)
        Me.FraFront.Controls.Add(Me.cboGSTStatus)
        Me.FraFront.Controls.Add(Me.txtVType)
        Me.FraFront.Controls.Add(Me.cmdResetMRR)
        Me.FraFront.Controls.Add(Me.Frame1)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.txtReason)
        Me.FraFront.Controls.Add(Me.chkCancelled)
        Me.FraFront.Controls.Add(Me.txtVNo)
        Me.FraFront.Controls.Add(Me.txtVNoSuffix)
        Me.FraFront.Controls.Add(Me.txtVDate)
        Me.FraFront.Controls.Add(Me.chkAproved)
        Me.FraFront.Controls.Add(Me.txtCreditAccount)
        Me.FraFront.Controls.Add(Me.txtDebitAccount)
        Me.FraFront.Controls.Add(Me.Frame6)
        Me.FraFront.Controls.Add(Me.txtVNoPrefix)
        Me.FraFront.Controls.Add(Me.lblDNCNSeqType)
        Me.FraFront.Controls.Add(Me.Label14)
        Me.FraFront.Controls.Add(Me.Label18)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.Label26)
        Me.FraFront.Controls.Add(Me.lblVNo)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.lblPayDate)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -4)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(906, 574)
        Me.FraFront.TabIndex = 28
        Me.FraFront.TabStop = False
        '
        'txtBillTo
        '
        Me.txtBillTo.AcceptsReturn = True
        Me.txtBillTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillTo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillTo.Location = New System.Drawing.Point(81, 101)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(375, 22)
        Me.txtBillTo.TabIndex = 145
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(20, 106)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(56, 13)
        Me.Label37.TabIndex = 146
        Me.Label37.Text = "Location :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboGSTStatus
        '
        Me.cboGSTStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboGSTStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGSTStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGSTStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGSTStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGSTStatus.Location = New System.Drawing.Point(81, 147)
        Me.cboGSTStatus.Name = "cboGSTStatus"
        Me.cboGSTStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGSTStatus.Size = New System.Drawing.Size(405, 22)
        Me.cboGSTStatus.TabIndex = 97
        '
        'txtVType
        '
        Me.txtVType.AcceptsReturn = True
        Me.txtVType.BackColor = System.Drawing.SystemColors.Window
        Me.txtVType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVType.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtVType.Location = New System.Drawing.Point(81, 35)
        Me.txtVType.MaxLength = 0
        Me.txtVType.Name = "txtVType"
        Me.txtVType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVType.Size = New System.Drawing.Size(34, 20)
        Me.txtVType.TabIndex = 1
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtMRRDate)
        Me.Frame1.Controls.Add(Me.txtMRRNo)
        Me.Frame1.Controls.Add(Me.cmdMRRNoSearch)
        Me.Frame1.Controls.Add(Me.cmdBillNoSearch)
        Me.Frame1.Controls.Add(Me.txtBillNo)
        Me.Frame1.Controls.Add(Me.txtBillDate)
        Me.Frame1.Controls.Add(Me.cboPopulateFrom)
        Me.Frame1.Controls.Add(Me.cmdVNoSearch)
        Me.Frame1.Controls.Add(Me.txtPurVNo)
        Me.Frame1.Controls.Add(Me.txtPurVDate)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Controls.Add(Me.Label8)
        Me.Frame1.Controls.Add(Me.Label12)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.lblDNFrom)
        Me.Frame1.Controls.Add(Me.lblPurVNO)
        Me.Frame1.Controls.Add(Me.Label10)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(512, 6)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(396, 138)
        Me.Frame1.TabIndex = 77
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Populate From"
        '
        'txtMRRDate
        '
        Me.txtMRRDate.AcceptsReturn = True
        Me.txtMRRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRDate.Enabled = False
        Me.txtMRRDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRDate.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRDate.Location = New System.Drawing.Point(290, 109)
        Me.txtMRRDate.MaxLength = 0
        Me.txtMRRDate.Name = "txtMRRDate"
        Me.txtMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRDate.Size = New System.Drawing.Size(97, 20)
        Me.txtMRRDate.TabIndex = 94
        '
        'txtMRRNo
        '
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRNo.Location = New System.Drawing.Point(74, 109)
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.Size = New System.Drawing.Size(110, 20)
        Me.txtMRRNo.TabIndex = 93
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillNo.Location = New System.Drawing.Point(74, 78)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(110, 20)
        Me.txtBillNo.TabIndex = 86
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Enabled = False
        Me.txtBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(290, 78)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(97, 20)
        Me.txtBillDate.TabIndex = 85
        '
        'cboPopulateFrom
        '
        Me.cboPopulateFrom.BackColor = System.Drawing.SystemColors.Window
        Me.cboPopulateFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPopulateFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPopulateFrom.Enabled = False
        Me.cboPopulateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPopulateFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPopulateFrom.Location = New System.Drawing.Point(74, 14)
        Me.cboPopulateFrom.Name = "cboPopulateFrom"
        Me.cboPopulateFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPopulateFrom.Size = New System.Drawing.Size(312, 22)
        Me.cboPopulateFrom.TabIndex = 83
        '
        'txtPurVNo
        '
        Me.txtPurVNo.AcceptsReturn = True
        Me.txtPurVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurVNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPurVNo.Location = New System.Drawing.Point(74, 47)
        Me.txtPurVNo.MaxLength = 0
        Me.txtPurVNo.Name = "txtPurVNo"
        Me.txtPurVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurVNo.Size = New System.Drawing.Size(110, 20)
        Me.txtPurVNo.TabIndex = 79
        '
        'txtPurVDate
        '
        Me.txtPurVDate.AcceptsReturn = True
        Me.txtPurVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurVDate.Enabled = False
        Me.txtPurVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtPurVDate.Location = New System.Drawing.Point(290, 47)
        Me.txtPurVDate.MaxLength = 0
        Me.txtPurVDate.Name = "txtPurVDate"
        Me.txtPurVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurVDate.Size = New System.Drawing.Size(97, 20)
        Me.txtPurVDate.TabIndex = 78
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(12, 112)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(54, 14)
        Me.Label9.TabIndex = 96
        Me.Label9.Text = "MRR No :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(224, 113)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(61, 14)
        Me.Label8.TabIndex = 95
        Me.Label8.Text = "MRR Date:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(20, 82)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(46, 14)
        Me.Label12.TabIndex = 88
        Me.Label12.Text = "Bill No :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(232, 81)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(53, 14)
        Me.Label7.TabIndex = 87
        Me.Label7.Text = "Bill Date:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDNFrom
        '
        Me.lblDNFrom.AutoSize = True
        Me.lblDNFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblDNFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDNFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDNFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDNFrom.Location = New System.Drawing.Point(24, 18)
        Me.lblDNFrom.Name = "lblDNFrom"
        Me.lblDNFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDNFrom.Size = New System.Drawing.Size(42, 14)
        Me.lblDNFrom.TabIndex = 84
        Me.lblDNFrom.Text = "From :"
        Me.lblDNFrom.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPurVNO
        '
        Me.lblPurVNO.AutoSize = True
        Me.lblPurVNO.BackColor = System.Drawing.SystemColors.Control
        Me.lblPurVNO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPurVNO.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurVNO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPurVNO.Location = New System.Drawing.Point(31, 50)
        Me.lblPurVNO.Name = "lblPurVNO"
        Me.lblPurVNO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPurVNO.Size = New System.Drawing.Size(35, 14)
        Me.lblPurVNO.TabIndex = 82
        Me.lblPurVNO.Text = "VNo :"
        Me.lblPurVNO.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(243, 51)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(42, 14)
        Me.Label10.TabIndex = 81
        Me.Label10.Text = "VDate:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Enabled = False
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(81, 10)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(403, 22)
        Me.cboDivision.TabIndex = 6
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReason.Location = New System.Drawing.Point(81, 124)
        Me.txtReason.MaxLength = 0
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(403, 20)
        Me.txtReason.TabIndex = 7
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(617, 150)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(80, 18)
        Me.chkCancelled.TabIndex = 9
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.Color.Blue
        Me.txtVNo.Location = New System.Drawing.Point(138, 35)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(116, 20)
        Me.txtVNo.TabIndex = 2
        '
        'txtVNoSuffix
        '
        Me.txtVNoSuffix.AcceptsReturn = True
        Me.txtVNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNoSuffix.Enabled = False
        Me.txtVNoSuffix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNoSuffix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVNoSuffix.Location = New System.Drawing.Point(177, 34)
        Me.txtVNoSuffix.MaxLength = 0
        Me.txtVNoSuffix.Name = "txtVNoSuffix"
        Me.txtVNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoSuffix.Size = New System.Drawing.Size(10, 20)
        Me.txtVNoSuffix.TabIndex = 33
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtVDate.Location = New System.Drawing.Point(404, 35)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(79, 20)
        Me.txtVDate.TabIndex = 3
        '
        'chkAproved
        '
        Me.chkAproved.AutoSize = True
        Me.chkAproved.BackColor = System.Drawing.SystemColors.Control
        Me.chkAproved.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAproved.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAproved.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkAproved.Location = New System.Drawing.Point(513, 150)
        Me.chkAproved.Name = "chkAproved"
        Me.chkAproved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAproved.Size = New System.Drawing.Size(80, 18)
        Me.chkAproved.TabIndex = 8
        Me.chkAproved.Text = "Approved"
        Me.chkAproved.UseVisualStyleBackColor = False
        '
        'txtCreditAccount
        '
        Me.txtCreditAccount.AcceptsReturn = True
        Me.txtCreditAccount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreditAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreditAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreditAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditAccount.ForeColor = System.Drawing.Color.Blue
        Me.txtCreditAccount.Location = New System.Drawing.Point(81, 79)
        Me.txtCreditAccount.MaxLength = 0
        Me.txtCreditAccount.Name = "txtCreditAccount"
        Me.txtCreditAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreditAccount.Size = New System.Drawing.Size(403, 20)
        Me.txtCreditAccount.TabIndex = 5
        '
        'txtDebitAccount
        '
        Me.txtDebitAccount.AcceptsReturn = True
        Me.txtDebitAccount.BackColor = System.Drawing.SystemColors.Window
        Me.txtDebitAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDebitAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDebitAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDebitAccount.ForeColor = System.Drawing.Color.Blue
        Me.txtDebitAccount.Location = New System.Drawing.Point(81, 57)
        Me.txtDebitAccount.MaxLength = 0
        Me.txtDebitAccount.Name = "txtDebitAccount"
        Me.txtDebitAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDebitAccount.Size = New System.Drawing.Size(403, 20)
        Me.txtDebitAccount.TabIndex = 4
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.FraPostingDtl)
        Me.Frame6.Controls.Add(Me.FraRefund)
        Me.Frame6.Controls.Add(Me.txtRecdDate)
        Me.Frame6.Controls.Add(Me.txtPartyDNDate)
        Me.Frame6.Controls.Add(Me.txtPartyDNNo)
        Me.Frame6.Controls.Add(Me.txtNarration)
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Controls.Add(Me.SprdExp)
        Me.Frame6.Controls.Add(Me.Label21)
        Me.Frame6.Controls.Add(Me.Label20)
        Me.Frame6.Controls.Add(Me.Label15)
        Me.Frame6.Controls.Add(Me.Label11)
        Me.Frame6.Controls.Add(Me.lblTotOthAmount)
        Me.Frame6.Controls.Add(Me.Label5)
        Me.Frame6.Controls.Add(Me.lblTotIGSTAmount)
        Me.Frame6.Controls.Add(Me.lblServiceAmount)
        Me.Frame6.Controls.Add(Me.lblServicePercentage)
        Me.Frame6.Controls.Add(Me.lblTotQty)
        Me.Frame6.Controls.Add(Me.lblTotPackQtyCap)
        Me.Frame6.Controls.Add(Me.lblNetAmount)
        Me.Frame6.Controls.Add(Me.Label13)
        Me.Frame6.Controls.Add(Me.lblTotItemValue)
        Me.Frame6.Controls.Add(Me.Label16)
        Me.Frame6.Controls.Add(Me.lblTotCGSTAmount)
        Me.Frame6.Controls.Add(Me.Label17)
        Me.Frame6.Controls.Add(Me.lblTotSGSTAmount)
        Me.Frame6.Controls.Add(Me.Label34)
        Me.Frame6.Controls.Add(Me.LblMKey)
        Me.Frame6.Controls.Add(Me.LblBookCode)
        Me.Frame6.Controls.Add(Me.lblTotCharges)
        Me.Frame6.Controls.Add(Me.lblTotFreight)
        Me.Frame6.Controls.Add(Me.lblTotExpAmt)
        Me.Frame6.Controls.Add(Me.lblEDPercentage)
        Me.Frame6.Controls.Add(Me.lblSTPercentage)
        Me.Frame6.Controls.Add(Me.lblTotTaxableAmt)
        Me.Frame6.Controls.Add(Me.lblDiscount)
        Me.Frame6.Controls.Add(Me.lblSurcharge)
        Me.Frame6.Controls.Add(Me.lblRO)
        Me.Frame6.Controls.Add(Me.lblMSC)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 169)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(906, 403)
        Me.Frame6.TabIndex = 34
        Me.Frame6.TabStop = False
        '
        'FraRefund
        '
        Me.FraRefund.BackColor = System.Drawing.SystemColors.Control
        Me.FraRefund.Controls.Add(Me.txtIGSTRefundAmount)
        Me.FraRefund.Controls.Add(Me.txtSGSTRefundAmount)
        Me.FraRefund.Controls.Add(Me.txtCGSTRefundAmount)
        Me.FraRefund.Controls.Add(Me.Label4)
        Me.FraRefund.Controls.Add(Me.Label2)
        Me.FraRefund.Controls.Add(Me.Label19)
        Me.FraRefund.Enabled = False
        Me.FraRefund.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraRefund.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraRefund.Location = New System.Drawing.Point(2, 328)
        Me.FraRefund.Name = "FraRefund"
        Me.FraRefund.Padding = New System.Windows.Forms.Padding(0)
        Me.FraRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraRefund.Size = New System.Drawing.Size(233, 77)
        Me.FraRefund.TabIndex = 57
        Me.FraRefund.TabStop = False
        Me.FraRefund.Visible = False
        '
        'txtIGSTRefundAmount
        '
        Me.txtIGSTRefundAmount.AcceptsReturn = True
        Me.txtIGSTRefundAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtIGSTRefundAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIGSTRefundAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIGSTRefundAmount.Enabled = False
        Me.txtIGSTRefundAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIGSTRefundAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtIGSTRefundAmount.Location = New System.Drawing.Point(160, 54)
        Me.txtIGSTRefundAmount.MaxLength = 0
        Me.txtIGSTRefundAmount.Name = "txtIGSTRefundAmount"
        Me.txtIGSTRefundAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIGSTRefundAmount.Size = New System.Drawing.Size(69, 20)
        Me.txtIGSTRefundAmount.TabIndex = 75
        '
        'txtSGSTRefundAmount
        '
        Me.txtSGSTRefundAmount.AcceptsReturn = True
        Me.txtSGSTRefundAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSGSTRefundAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSGSTRefundAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSGSTRefundAmount.Enabled = False
        Me.txtSGSTRefundAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSGSTRefundAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtSGSTRefundAmount.Location = New System.Drawing.Point(160, 32)
        Me.txtSGSTRefundAmount.MaxLength = 0
        Me.txtSGSTRefundAmount.Name = "txtSGSTRefundAmount"
        Me.txtSGSTRefundAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSGSTRefundAmount.Size = New System.Drawing.Size(69, 20)
        Me.txtSGSTRefundAmount.TabIndex = 73
        '
        'txtCGSTRefundAmount
        '
        Me.txtCGSTRefundAmount.AcceptsReturn = True
        Me.txtCGSTRefundAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCGSTRefundAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCGSTRefundAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCGSTRefundAmount.Enabled = False
        Me.txtCGSTRefundAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCGSTRefundAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtCGSTRefundAmount.Location = New System.Drawing.Point(160, 10)
        Me.txtCGSTRefundAmount.MaxLength = 0
        Me.txtCGSTRefundAmount.Name = "txtCGSTRefundAmount"
        Me.txtCGSTRefundAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCGSTRefundAmount.Size = New System.Drawing.Size(69, 20)
        Me.txtCGSTRefundAmount.TabIndex = 23
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(30, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(127, 14)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "IGST Refund Amount :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(26, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(131, 14)
        Me.Label2.TabIndex = 74
        Me.Label2.Text = "SGST Refund Amount :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(26, 12)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(132, 14)
        Me.Label19.TabIndex = 58
        Me.Label19.Text = "CGST Refund Amount :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtRecdDate
        '
        Me.txtRecdDate.AcceptsReturn = True
        Me.txtRecdDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecdDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecdDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecdDate.Enabled = False
        Me.txtRecdDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecdDate.ForeColor = System.Drawing.Color.Blue
        Me.txtRecdDate.Location = New System.Drawing.Point(121, 379)
        Me.txtRecdDate.MaxLength = 0
        Me.txtRecdDate.Name = "txtRecdDate"
        Me.txtRecdDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecdDate.Size = New System.Drawing.Size(111, 20)
        Me.txtRecdDate.TabIndex = 103
        '
        'txtPartyDNDate
        '
        Me.txtPartyDNDate.AcceptsReturn = True
        Me.txtPartyDNDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyDNDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyDNDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyDNDate.Enabled = False
        Me.txtPartyDNDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyDNDate.ForeColor = System.Drawing.Color.Blue
        Me.txtPartyDNDate.Location = New System.Drawing.Point(121, 359)
        Me.txtPartyDNDate.MaxLength = 0
        Me.txtPartyDNDate.Name = "txtPartyDNDate"
        Me.txtPartyDNDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyDNDate.Size = New System.Drawing.Size(111, 20)
        Me.txtPartyDNDate.TabIndex = 100
        '
        'txtPartyDNNo
        '
        Me.txtPartyDNNo.AcceptsReturn = True
        Me.txtPartyDNNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyDNNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyDNNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyDNNo.Enabled = False
        Me.txtPartyDNNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyDNNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPartyDNNo.Location = New System.Drawing.Point(121, 339)
        Me.txtPartyDNNo.MaxLength = 0
        Me.txtPartyDNNo.Name = "txtPartyDNNo"
        Me.txtPartyDNNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyDNNo.Size = New System.Drawing.Size(111, 20)
        Me.txtPartyDNNo.TabIndex = 99
        '
        'txtNarration
        '
        Me.txtNarration.AcceptsReturn = True
        Me.txtNarration.BackColor = System.Drawing.SystemColors.Window
        Me.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNarration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNarration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNarration.Location = New System.Drawing.Point(2, 255)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Multiline = True
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(231, 73)
        Me.txtNarration.TabIndex = 11
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 10)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(900, 240)
        Me.SprdMain.TabIndex = 10
        '
        'SprdExp
        '
        Me.SprdExp.DataSource = Nothing
        Me.SprdExp.Location = New System.Drawing.Point(239, 257)
        Me.SprdExp.Name = "SprdExp"
        Me.SprdExp.OcxState = CType(resources.GetObject("SprdExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdExp.Size = New System.Drawing.Size(455, 141)
        Me.SprdExp.TabIndex = 12
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(48, 383)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(67, 14)
        Me.Label21.TabIndex = 104
        Me.Label21.Text = "Recd Date :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(4, 363)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(103, 14)
        Me.Label20.TabIndex = 102
        Me.Label20.Text = "Party DN/CN Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(3, 343)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(93, 14)
        Me.Label15.TabIndex = 101
        Me.Label15.Text = "Party DN/CN No :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(707, 359)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(91, 14)
        Me.Label11.TabIndex = 71
        Me.Label11.Text = "Other Amount :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotOthAmount
        '
        Me.lblTotOthAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotOthAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotOthAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotOthAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotOthAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotOthAmount.Location = New System.Drawing.Point(802, 354)
        Me.lblTotOthAmount.Name = "lblTotOthAmount"
        Me.lblTotOthAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotOthAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblTotOthAmount.TabIndex = 70
        Me.lblTotOthAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(713, 339)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(85, 14)
        Me.Label5.TabIndex = 69
        Me.Label5.Text = "IGST Amount :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotIGSTAmount
        '
        Me.lblTotIGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotIGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotIGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotIGSTAmount.Location = New System.Drawing.Point(802, 334)
        Me.lblTotIGSTAmount.Name = "lblTotIGSTAmount"
        Me.lblTotIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGSTAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblTotIGSTAmount.TabIndex = 68
        Me.lblTotIGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblServiceAmount
        '
        Me.lblServiceAmount.AutoSize = True
        Me.lblServiceAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblServiceAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblServiceAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServiceAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblServiceAmount.Location = New System.Drawing.Point(140, 162)
        Me.lblServiceAmount.Name = "lblServiceAmount"
        Me.lblServiceAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblServiceAmount.Size = New System.Drawing.Size(91, 14)
        Me.lblServiceAmount.TabIndex = 63
        Me.lblServiceAmount.Text = "lblServiceAmount"
        Me.lblServiceAmount.Visible = False
        '
        'lblServicePercentage
        '
        Me.lblServicePercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblServicePercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblServicePercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServicePercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblServicePercentage.Location = New System.Drawing.Point(140, 178)
        Me.lblServicePercentage.Name = "lblServicePercentage"
        Me.lblServicePercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblServicePercentage.Size = New System.Drawing.Size(38, 13)
        Me.lblServicePercentage.TabIndex = 62
        Me.lblServicePercentage.Text = "lblServicePercentage"
        Me.lblServicePercentage.Visible = False
        '
        'lblTotQty
        '
        Me.lblTotQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotQty.Location = New System.Drawing.Point(802, 256)
        Me.lblTotQty.Name = "lblTotQty"
        Me.lblTotQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotQty.Size = New System.Drawing.Size(99, 17)
        Me.lblTotQty.TabIndex = 55
        Me.lblTotQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotPackQtyCap
        '
        Me.lblTotPackQtyCap.AutoSize = True
        Me.lblTotPackQtyCap.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotPackQtyCap.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotPackQtyCap.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotPackQtyCap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotPackQtyCap.Location = New System.Drawing.Point(738, 260)
        Me.lblTotPackQtyCap.Name = "lblTotPackQtyCap"
        Me.lblTotPackQtyCap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotPackQtyCap.Size = New System.Drawing.Size(60, 14)
        Me.lblTotPackQtyCap.TabIndex = 56
        Me.lblTotPackQtyCap.Text = "Total Qty :"
        Me.lblTotPackQtyCap.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(802, 374)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblNetAmount.TabIndex = 54
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(720, 377)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(78, 14)
        Me.Label13.TabIndex = 53
        Me.Label13.Text = "Net Amount :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotItemValue
        '
        Me.lblTotItemValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotItemValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotItemValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotItemValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotItemValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotItemValue.Location = New System.Drawing.Point(802, 277)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(99, 17)
        Me.lblTotItemValue.TabIndex = 52
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(727, 279)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(71, 14)
        Me.Label16.TabIndex = 51
        Me.Label16.Text = "Item Value :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotCGSTAmount
        '
        Me.lblTotCGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotCGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotCGSTAmount.Location = New System.Drawing.Point(802, 294)
        Me.lblTotCGSTAmount.Name = "lblTotCGSTAmount"
        Me.lblTotCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGSTAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblTotCGSTAmount.TabIndex = 50
        Me.lblTotCGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(708, 299)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(90, 14)
        Me.Label17.TabIndex = 49
        Me.Label17.Text = "CGST Amount :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotSGSTAmount
        '
        Me.lblTotSGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotSGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotSGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotSGSTAmount.Location = New System.Drawing.Point(802, 314)
        Me.lblTotSGSTAmount.Name = "lblTotSGSTAmount"
        Me.lblTotSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGSTAmount.Size = New System.Drawing.Size(99, 19)
        Me.lblTotSGSTAmount.TabIndex = 48
        Me.lblTotSGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label34.Location = New System.Drawing.Point(709, 319)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(89, 14)
        Me.Label34.TabIndex = 47
        Me.Label34.Text = "SGST Amount :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblMKey
        '
        Me.LblMKey.AutoSize = True
        Me.LblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMKey.Location = New System.Drawing.Point(304, 180)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(48, 14)
        Me.LblMKey.TabIndex = 46
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        'LblBookCode
        '
        Me.LblBookCode.AutoSize = True
        Me.LblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.LblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblBookCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblBookCode.Location = New System.Drawing.Point(304, 164)
        Me.LblBookCode.Name = "LblBookCode"
        Me.LblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblBookCode.Size = New System.Drawing.Size(70, 14)
        Me.LblBookCode.TabIndex = 45
        Me.LblBookCode.Text = "LblBookCode"
        Me.LblBookCode.Visible = False
        '
        'lblTotCharges
        '
        Me.lblTotCharges.AutoSize = True
        Me.lblTotCharges.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCharges.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCharges.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCharges.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotCharges.Location = New System.Drawing.Point(382, 196)
        Me.lblTotCharges.Name = "lblTotCharges"
        Me.lblTotCharges.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCharges.Size = New System.Drawing.Size(13, 14)
        Me.lblTotCharges.TabIndex = 44
        Me.lblTotCharges.Text = "0"
        Me.lblTotCharges.Visible = False
        '
        'lblTotFreight
        '
        Me.lblTotFreight.AutoSize = True
        Me.lblTotFreight.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotFreight.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotFreight.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotFreight.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotFreight.Location = New System.Drawing.Point(460, 196)
        Me.lblTotFreight.Name = "lblTotFreight"
        Me.lblTotFreight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotFreight.Size = New System.Drawing.Size(13, 14)
        Me.lblTotFreight.TabIndex = 43
        Me.lblTotFreight.Text = "0"
        Me.lblTotFreight.Visible = False
        '
        'lblTotExpAmt
        '
        Me.lblTotExpAmt.AutoSize = True
        Me.lblTotExpAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotExpAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotExpAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotExpAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotExpAmt.Location = New System.Drawing.Point(380, 214)
        Me.lblTotExpAmt.Name = "lblTotExpAmt"
        Me.lblTotExpAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotExpAmt.Size = New System.Drawing.Size(13, 14)
        Me.lblTotExpAmt.TabIndex = 42
        Me.lblTotExpAmt.Text = "0"
        Me.lblTotExpAmt.Visible = False
        '
        'lblEDPercentage
        '
        Me.lblEDPercentage.AutoSize = True
        Me.lblEDPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblEDPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEDPercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEDPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEDPercentage.Location = New System.Drawing.Point(460, 216)
        Me.lblEDPercentage.Name = "lblEDPercentage"
        Me.lblEDPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEDPercentage.Size = New System.Drawing.Size(13, 14)
        Me.lblEDPercentage.TabIndex = 41
        Me.lblEDPercentage.Text = "0"
        Me.lblEDPercentage.Visible = False
        '
        'lblSTPercentage
        '
        Me.lblSTPercentage.AutoSize = True
        Me.lblSTPercentage.BackColor = System.Drawing.SystemColors.Control
        Me.lblSTPercentage.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSTPercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSTPercentage.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSTPercentage.Location = New System.Drawing.Point(384, 232)
        Me.lblSTPercentage.Name = "lblSTPercentage"
        Me.lblSTPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSTPercentage.Size = New System.Drawing.Size(13, 14)
        Me.lblSTPercentage.TabIndex = 40
        Me.lblSTPercentage.Text = "0"
        Me.lblSTPercentage.Visible = False
        '
        'lblTotTaxableAmt
        '
        Me.lblTotTaxableAmt.AutoSize = True
        Me.lblTotTaxableAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotTaxableAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotTaxableAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotTaxableAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotTaxableAmt.Location = New System.Drawing.Point(470, 236)
        Me.lblTotTaxableAmt.Name = "lblTotTaxableAmt"
        Me.lblTotTaxableAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotTaxableAmt.Size = New System.Drawing.Size(13, 14)
        Me.lblTotTaxableAmt.TabIndex = 39
        Me.lblTotTaxableAmt.Text = "0"
        Me.lblTotTaxableAmt.Visible = False
        '
        'lblDiscount
        '
        Me.lblDiscount.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiscount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiscount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDiscount.Location = New System.Drawing.Point(304, 196)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiscount.Size = New System.Drawing.Size(59, 11)
        Me.lblDiscount.TabIndex = 38
        Me.lblDiscount.Text = "lblDiscount"
        Me.lblDiscount.Visible = False
        '
        'lblSurcharge
        '
        Me.lblSurcharge.BackColor = System.Drawing.SystemColors.Control
        Me.lblSurcharge.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSurcharge.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSurcharge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSurcharge.Location = New System.Drawing.Point(304, 212)
        Me.lblSurcharge.Name = "lblSurcharge"
        Me.lblSurcharge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSurcharge.Size = New System.Drawing.Size(47, 11)
        Me.lblSurcharge.TabIndex = 37
        Me.lblSurcharge.Text = "lblSurcharge"
        Me.lblSurcharge.Visible = False
        '
        'lblRO
        '
        Me.lblRO.BackColor = System.Drawing.SystemColors.Control
        Me.lblRO.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRO.Location = New System.Drawing.Point(304, 228)
        Me.lblRO.Name = "lblRO"
        Me.lblRO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRO.Size = New System.Drawing.Size(41, 11)
        Me.lblRO.TabIndex = 36
        Me.lblRO.Text = "lblRO"
        Me.lblRO.Visible = False
        '
        'lblMSC
        '
        Me.lblMSC.BackColor = System.Drawing.SystemColors.Control
        Me.lblMSC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMSC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMSC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMSC.Location = New System.Drawing.Point(304, 240)
        Me.lblMSC.Name = "lblMSC"
        Me.lblMSC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMSC.Size = New System.Drawing.Size(49, 11)
        Me.lblMSC.TabIndex = 35
        Me.lblMSC.Text = "lblMSC"
        Me.lblMSC.Visible = False
        '
        'txtVNoPrefix
        '
        Me.txtVNoPrefix.AcceptsReturn = True
        Me.txtVNoPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNoPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNoPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNoPrefix.Enabled = False
        Me.txtVNoPrefix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNoPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVNoPrefix.Location = New System.Drawing.Point(114, 35)
        Me.txtVNoPrefix.MaxLength = 0
        Me.txtVNoPrefix.Name = "txtVNoPrefix"
        Me.txtVNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoPrefix.Size = New System.Drawing.Size(25, 20)
        Me.txtVNoPrefix.TabIndex = 22
        '
        'lblDNCNSeqType
        '
        Me.lblDNCNSeqType.AutoSize = True
        Me.lblDNCNSeqType.BackColor = System.Drawing.SystemColors.Control
        Me.lblDNCNSeqType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDNCNSeqType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDNCNSeqType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDNCNSeqType.Location = New System.Drawing.Point(390, 62)
        Me.lblDNCNSeqType.Name = "lblDNCNSeqType"
        Me.lblDNCNSeqType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDNCNSeqType.Size = New System.Drawing.Size(87, 14)
        Me.lblDNCNSeqType.TabIndex = 105
        Me.lblDNCNSeqType.Text = "lblDNCNSeqType"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(3, 151)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(73, 14)
        Me.Label14.TabIndex = 98
        Me.Label14.Text = "GST Status :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(20, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(56, 14)
        Me.Label1.TabIndex = 67
        Me.Label1.Text = "Division :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(22, 127)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(54, 14)
        Me.Label26.TabIndex = 61
        Me.Label26.Text = "Reason :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVNo
        '
        Me.lblVNo.AutoSize = True
        Me.lblVNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblVNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVNo.Location = New System.Drawing.Point(0, 38)
        Me.lblVNo.Name = "lblVNo"
        Me.lblVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVNo.Size = New System.Drawing.Size(76, 14)
        Me.lblVNo.TabIndex = 32
        Me.lblVNo.Text = "Voucher No :"
        Me.lblVNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(349, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(37, 14)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(9, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(67, 14)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Credit A/c :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(15, 60)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(61, 14)
        Me.lblCust.TabIndex = 29
        Me.lblCust.Text = "Debit A/c :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPayDate
        '
        Me.lblPayDate.AutoSize = True
        Me.lblPayDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblPayDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPayDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPayDate.Location = New System.Drawing.Point(684, 78)
        Me.lblPayDate.Name = "lblPayDate"
        Me.lblPayDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPayDate.Size = New System.Drawing.Size(57, 14)
        Me.lblPayDate.TabIndex = 60
        Me.lblPayDate.Text = "lblPayDate"
        Me.lblPayDate.Visible = False
        '
        'lblDCType
        '
        Me.lblDCType.BackColor = System.Drawing.SystemColors.Control
        Me.lblDCType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDCType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDCType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDCType.Location = New System.Drawing.Point(844, 422)
        Me.lblDCType.Name = "lblDCType"
        Me.lblDCType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDCType.Size = New System.Drawing.Size(47, 15)
        Me.lblDCType.TabIndex = 72
        Me.lblDCType.Text = "lblDCType"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdPostingHead)
        Me.Frame3.Controls.Add(Me.cmdBarCode)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.txtSTRefundDate)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 568)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(910, 51)
        Me.Frame3.TabIndex = 24
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(56, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 65
        '
        'txtSTRefundDate
        '
        Me.txtSTRefundDate.AcceptsReturn = True
        Me.txtSTRefundDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSTRefundDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSTRefundDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSTRefundDate.Enabled = False
        Me.txtSTRefundDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSTRefundDate.ForeColor = System.Drawing.Color.Blue
        Me.txtSTRefundDate.Location = New System.Drawing.Point(2, 28)
        Me.txtSTRefundDate.MaxLength = 0
        Me.txtSTRefundDate.Name = "txtSTRefundDate"
        Me.txtSTRefundDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSTRefundDate.Size = New System.Drawing.Size(69, 20)
        Me.txtSTRefundDate.TabIndex = 59
        Me.txtSTRefundDate.Visible = False
        '
        'lblSODates
        '
        Me.lblSODates.BackColor = System.Drawing.SystemColors.Control
        Me.lblSODates.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSODates.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSODates.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSODates.Location = New System.Drawing.Point(596, 32)
        Me.lblSODates.Name = "lblSODates"
        Me.lblSODates.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSODates.Size = New System.Drawing.Size(17, 9)
        Me.lblSODates.TabIndex = 27
        Me.lblSODates.Text = "lblSODates"
        Me.lblSODates.Visible = False
        '
        'lblSONos
        '
        Me.lblSONos.BackColor = System.Drawing.SystemColors.Control
        Me.lblSONos.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSONos.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSONos.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSONos.Location = New System.Drawing.Point(590, 14)
        Me.lblSONos.Name = "lblSONos"
        Me.lblSONos.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSONos.Size = New System.Drawing.Size(23, 9)
        Me.lblSONos.TabIndex = 26
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'UltraGrid1
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.Color.White
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance1
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.White
        Appearance2.BackColor2 = System.Drawing.Color.White
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.UltraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.GroupByBox.Hidden = True
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UltraGrid1.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UltraGrid1.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance8
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.UltraGrid1.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UltraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance11
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.UltraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(4, 3)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(903, 568)
        Me.UltraGrid1.TabIndex = 89
        '
        'FrmDrCrNoteGST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.lblDCType)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDrCrNoteGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Debit Note (GST)"
        Me.FraPostingDtl.ResumeLayout(False)
        CType(Me.SprdPostingDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.FraRefund.ResumeLayout(False)
        Me.FraRefund.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub

    Public WithEvents cmdBillToSearch As Button
    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label37 As Label
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
#End Region
End Class