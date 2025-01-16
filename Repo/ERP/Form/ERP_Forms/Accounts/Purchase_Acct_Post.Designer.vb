Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPurchase_Acct_Post
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
    Public WithEvents cmdSearchPO As System.Windows.Forms.Button
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Public WithEvents lblAccountCode As System.Windows.Forms.Label
    Public WithEvents lblSaleBillNo As System.Windows.Forms.Label
    Public WithEvents lblTotCGSTAmount As System.Windows.Forms.Label
    Public WithEvents lblMRRDate As System.Windows.Forms.Label
    Public WithEvents lblTotIGSTRefund As System.Windows.Forms.Label
    Public WithEvents lblTotSGSTRefund As System.Windows.Forms.Label
    Public WithEvents lblTotCGSTRefund As System.Windows.Forms.Label
    Public WithEvents lblIsGSTRefund As System.Windows.Forms.Label
    Public WithEvents lblTRNType As System.Windows.Forms.Label
    Public WithEvents lblBillDate As System.Windows.Forms.Label
    Public WithEvents lblBillNo As System.Windows.Forms.Label
    Public WithEvents lblBookSubType As System.Windows.Forms.Label
    Public WithEvents LblBookCode As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraTrn As System.Windows.Forms.GroupBox
    Public WithEvents ADataGrid As VB6.ADODC
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents lblDivisionCode As System.Windows.Forms.Label
    Public WithEvents lblSaleBillDate As System.Windows.Forms.Label
    Public WithEvents lblTotIGSTAmount As System.Windows.Forms.Label
    Public WithEvents lblTotSGSTAmount As System.Windows.Forms.Label
    Public WithEvents lblNetExpAmount As System.Windows.Forms.Label
    Public WithEvents lblRemarks As System.Windows.Forms.Label
    Public WithEvents lblNarration As System.Windows.Forms.Label
    Public WithEvents lblDueDate As System.Windows.Forms.Label
    Public WithEvents LblFOC As System.Windows.Forms.Label
    Public WithEvents lblCancelled As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents lblItemValue As System.Windows.Forms.Label
    Public WithEvents lblSuppCustCode As System.Windows.Forms.Label
    Public WithEvents lblCurRowNo As System.Windows.Forms.Label
    Public WithEvents lblPOType As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPurchase_Acct_Post))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchPO = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.FraTrn = New System.Windows.Forms.GroupBox()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.lblAccountCode = New System.Windows.Forms.Label()
        Me.lblSaleBillNo = New System.Windows.Forms.Label()
        Me.lblTotCGSTAmount = New System.Windows.Forms.Label()
        Me.lblMRRDate = New System.Windows.Forms.Label()
        Me.lblTotIGSTRefund = New System.Windows.Forms.Label()
        Me.lblTotSGSTRefund = New System.Windows.Forms.Label()
        Me.lblTotCGSTRefund = New System.Windows.Forms.Label()
        Me.lblIsGSTRefund = New System.Windows.Forms.Label()
        Me.lblTRNType = New System.Windows.Forms.Label()
        Me.lblBillDate = New System.Windows.Forms.Label()
        Me.lblBillNo = New System.Windows.Forms.Label()
        Me.lblBookSubType = New System.Windows.Forms.Label()
        Me.LblBookCode = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblDivisionCode = New System.Windows.Forms.Label()
        Me.lblSaleBillDate = New System.Windows.Forms.Label()
        Me.lblTotIGSTAmount = New System.Windows.Forms.Label()
        Me.lblTotSGSTAmount = New System.Windows.Forms.Label()
        Me.lblNetExpAmount = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.lblNarration = New System.Windows.Forms.Label()
        Me.lblDueDate = New System.Windows.Forms.Label()
        Me.LblFOC = New System.Windows.Forms.Label()
        Me.lblCancelled = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.lblItemValue = New System.Windows.Forms.Label()
        Me.lblSuppCustCode = New System.Windows.Forms.Label()
        Me.lblCurRowNo = New System.Windows.Forms.Label()
        Me.lblPOType = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraTrn.SuspendLayout()
        Me.fraTop1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchPO
        '
        Me.cmdSearchPO.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPO.Image = CType(resources.GetObject("cmdSearchPO.Image"), System.Drawing.Image)
        Me.cmdSearchPO.Location = New System.Drawing.Point(160, 10)
        Me.cmdSearchPO.Name = "cmdSearchPO"
        Me.cmdSearchPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPO.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchPO.TabIndex = 1
        Me.cmdSearchPO.TabStop = False
        Me.cmdSearchPO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPO, "Search")
        Me.cmdSearchPO.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdClose.Location = New System.Drawing.Point(630, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 7
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
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(564, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 6
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdSave.Location = New System.Drawing.Point(88, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 5
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'FraTrn
        '
        Me.FraTrn.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrn.Controls.Add(Me.fraTop1)
        Me.FraTrn.Controls.Add(Me.SprdMain)
        Me.FraTrn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrn.Location = New System.Drawing.Point(0, -4)
        Me.FraTrn.Name = "FraTrn"
        Me.FraTrn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrn.Size = New System.Drawing.Size(751, 417)
        Me.FraTrn.TabIndex = 8
        Me.FraTrn.TabStop = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.cmdSearchPO)
        Me.fraTop1.Controls.Add(Me.txtPODate)
        Me.fraTop1.Controls.Add(Me.txtPONo)
        Me.fraTop1.Controls.Add(Me.txtSupplierName)
        Me.fraTop1.Controls.Add(Me.txtCode)
        Me.fraTop1.Controls.Add(Me.lblAccountCode)
        Me.fraTop1.Controls.Add(Me.lblSaleBillNo)
        Me.fraTop1.Controls.Add(Me.lblTotCGSTAmount)
        Me.fraTop1.Controls.Add(Me.lblMRRDate)
        Me.fraTop1.Controls.Add(Me.lblTotIGSTRefund)
        Me.fraTop1.Controls.Add(Me.lblTotSGSTRefund)
        Me.fraTop1.Controls.Add(Me.lblTotCGSTRefund)
        Me.fraTop1.Controls.Add(Me.lblIsGSTRefund)
        Me.fraTop1.Controls.Add(Me.lblTRNType)
        Me.fraTop1.Controls.Add(Me.lblBillDate)
        Me.fraTop1.Controls.Add(Me.lblBillNo)
        Me.fraTop1.Controls.Add(Me.lblBookSubType)
        Me.fraTop1.Controls.Add(Me.LblBookCode)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, 0)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(751, 59)
        Me.fraTop1.TabIndex = 9
        Me.fraTop1.TabStop = False
        '
        'txtPODate
        '
        Me.txtPODate.AcceptsReturn = True
        Me.txtPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPODate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPODate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPODate.Location = New System.Drawing.Point(266, 10)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(75, 20)
        Me.txtPODate.TabIndex = 2
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPONo.Location = New System.Drawing.Point(66, 10)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(93, 20)
        Me.txtPONo.TabIndex = 0
        '
        'txtSupplierName
        '
        Me.txtSupplierName.AcceptsReturn = True
        Me.txtSupplierName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierName.Enabled = False
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplierName.Location = New System.Drawing.Point(66, 32)
        Me.txtSupplierName.MaxLength = 0
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierName.Size = New System.Drawing.Size(281, 20)
        Me.txtSupplierName.TabIndex = 3
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.Blue
        Me.txtCode.Location = New System.Drawing.Point(440, 32)
        Me.txtCode.MaxLength = 0
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.Size = New System.Drawing.Size(55, 20)
        Me.txtCode.TabIndex = 4
        '
        'lblAccountCode
        '
        Me.lblAccountCode.AutoSize = True
        Me.lblAccountCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccountCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAccountCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAccountCode.Location = New System.Drawing.Point(518, 46)
        Me.lblAccountCode.Name = "lblAccountCode"
        Me.lblAccountCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccountCode.Size = New System.Drawing.Size(83, 14)
        Me.lblAccountCode.TabIndex = 46
        Me.lblAccountCode.Text = "lblAccountCode"
        Me.lblAccountCode.Visible = False
        '
        'lblSaleBillNo
        '
        Me.lblSaleBillNo.AutoSize = True
        Me.lblSaleBillNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblSaleBillNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSaleBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaleBillNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSaleBillNo.Location = New System.Drawing.Point(360, 20)
        Me.lblSaleBillNo.Name = "lblSaleBillNo"
        Me.lblSaleBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSaleBillNo.Size = New System.Drawing.Size(64, 14)
        Me.lblSaleBillNo.TabIndex = 44
        Me.lblSaleBillNo.Text = "lblSaleBillNo"
        Me.lblSaleBillNo.Visible = False
        '
        'lblTotCGSTAmount
        '
        Me.lblTotCGSTAmount.AutoSize = True
        Me.lblTotCGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCGSTAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotCGSTAmount.Location = New System.Drawing.Point(582, 12)
        Me.lblTotCGSTAmount.Name = "lblTotCGSTAmount"
        Me.lblTotCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGSTAmount.Size = New System.Drawing.Size(95, 14)
        Me.lblTotCGSTAmount.TabIndex = 40
        Me.lblTotCGSTAmount.Text = "lblTotCGSTAmount"
        Me.lblTotCGSTAmount.Visible = False
        '
        'lblMRRDate
        '
        Me.lblMRRDate.AutoSize = True
        Me.lblMRRDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblMRRDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMRRDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRRDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMRRDate.Location = New System.Drawing.Point(560, 34)
        Me.lblMRRDate.Name = "lblMRRDate"
        Me.lblMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMRRDate.Size = New System.Drawing.Size(61, 14)
        Me.lblMRRDate.TabIndex = 39
        Me.lblMRRDate.Text = "lblMRRDate"
        Me.lblMRRDate.Visible = False
        '
        'lblTotIGSTRefund
        '
        Me.lblTotIGSTRefund.AutoSize = True
        Me.lblTotIGSTRefund.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGSTRefund.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGSTRefund.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotIGSTRefund.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotIGSTRefund.Location = New System.Drawing.Point(668, 38)
        Me.lblTotIGSTRefund.Name = "lblTotIGSTRefund"
        Me.lblTotIGSTRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGSTRefund.Size = New System.Drawing.Size(89, 14)
        Me.lblTotIGSTRefund.TabIndex = 38
        Me.lblTotIGSTRefund.Text = "lblTotIGSTRefund"
        Me.lblTotIGSTRefund.Visible = False
        '
        'lblTotSGSTRefund
        '
        Me.lblTotSGSTRefund.AutoSize = True
        Me.lblTotSGSTRefund.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGSTRefund.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGSTRefund.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotSGSTRefund.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotSGSTRefund.Location = New System.Drawing.Point(364, 36)
        Me.lblTotSGSTRefund.Name = "lblTotSGSTRefund"
        Me.lblTotSGSTRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGSTRefund.Size = New System.Drawing.Size(94, 14)
        Me.lblTotSGSTRefund.TabIndex = 37
        Me.lblTotSGSTRefund.Text = "lblTotSGSTRefund"
        Me.lblTotSGSTRefund.Visible = False
        '
        'lblTotCGSTRefund
        '
        Me.lblTotCGSTRefund.AutoSize = True
        Me.lblTotCGSTRefund.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGSTRefund.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGSTRefund.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCGSTRefund.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotCGSTRefund.Location = New System.Drawing.Point(476, 12)
        Me.lblTotCGSTRefund.Name = "lblTotCGSTRefund"
        Me.lblTotCGSTRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGSTRefund.Size = New System.Drawing.Size(94, 14)
        Me.lblTotCGSTRefund.TabIndex = 36
        Me.lblTotCGSTRefund.Text = "lblTotCGSTRefund"
        Me.lblTotCGSTRefund.Visible = False
        '
        'lblIsGSTRefund
        '
        Me.lblIsGSTRefund.AutoSize = True
        Me.lblIsGSTRefund.BackColor = System.Drawing.SystemColors.Control
        Me.lblIsGSTRefund.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIsGSTRefund.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIsGSTRefund.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIsGSTRefund.Location = New System.Drawing.Point(380, 14)
        Me.lblIsGSTRefund.Name = "lblIsGSTRefund"
        Me.lblIsGSTRefund.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIsGSTRefund.Size = New System.Drawing.Size(81, 14)
        Me.lblIsGSTRefund.TabIndex = 35
        Me.lblIsGSTRefund.Text = "lblIsGSTRefund"
        Me.lblIsGSTRefund.Visible = False
        '
        'lblTRNType
        '
        Me.lblTRNType.BackColor = System.Drawing.SystemColors.Control
        Me.lblTRNType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTRNType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTRNType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTRNType.Location = New System.Drawing.Point(686, 18)
        Me.lblTRNType.Name = "lblTRNType"
        Me.lblTRNType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTRNType.Size = New System.Drawing.Size(37, 13)
        Me.lblTRNType.TabIndex = 24
        Me.lblTRNType.Text = "lblTRNType"
        Me.lblTRNType.Visible = False
        '
        'lblBillDate
        '
        Me.lblBillDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillDate.Location = New System.Drawing.Point(604, 40)
        Me.lblBillDate.Name = "lblBillDate"
        Me.lblBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillDate.Size = New System.Drawing.Size(51, 17)
        Me.lblBillDate.TabIndex = 23
        Me.lblBillDate.Text = "lblBillDate"
        Me.lblBillDate.Visible = False
        '
        'lblBillNo
        '
        Me.lblBillNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillNo.Location = New System.Drawing.Point(514, 32)
        Me.lblBillNo.Name = "lblBillNo"
        Me.lblBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillNo.Size = New System.Drawing.Size(33, 19)
        Me.lblBillNo.TabIndex = 22
        Me.lblBillNo.Text = "lblBillNo"
        Me.lblBillNo.Visible = False
        '
        'lblBookSubType
        '
        Me.lblBookSubType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookSubType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookSubType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookSubType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookSubType.Location = New System.Drawing.Point(604, 20)
        Me.lblBookSubType.Name = "lblBookSubType"
        Me.lblBookSubType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookSubType.Size = New System.Drawing.Size(67, 25)
        Me.lblBookSubType.TabIndex = 21
        Me.lblBookSubType.Text = "lblBookSubType"
        Me.lblBookSubType.Visible = False
        '
        'LblBookCode
        '
        Me.LblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.LblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblBookCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblBookCode.Location = New System.Drawing.Point(528, 14)
        Me.LblBookCode.Name = "LblBookCode"
        Me.LblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblBookCode.Size = New System.Drawing.Size(43, 17)
        Me.LblBookCode.TabIndex = 20
        Me.LblBookCode.Text = "LblBookCode"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(225, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(37, 14)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(14, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "VNo :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(28, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(41, 14)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Party :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(402, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(42, 14)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Code :"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(1, 63)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(749, 349)
        Me.SprdMain.TabIndex = 19
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
        Me.ADataGrid.Location = New System.Drawing.Point(0, 56)
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.Name = "ADataGrid"
        Me.ADataGrid.Size = New System.Drawing.Size(113, 23)
        Me.ADataGrid.TabIndex = 9
        Me.ADataGrid.Text = "Adodc1"
        Me.ADataGrid.Visible = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 10
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.lblDivisionCode)
        Me.FraMovement.Controls.Add(Me.lblSaleBillDate)
        Me.FraMovement.Controls.Add(Me.lblTotIGSTAmount)
        Me.FraMovement.Controls.Add(Me.lblTotSGSTAmount)
        Me.FraMovement.Controls.Add(Me.lblNetExpAmount)
        Me.FraMovement.Controls.Add(Me.lblRemarks)
        Me.FraMovement.Controls.Add(Me.lblNarration)
        Me.FraMovement.Controls.Add(Me.lblDueDate)
        Me.FraMovement.Controls.Add(Me.LblFOC)
        Me.FraMovement.Controls.Add(Me.lblCancelled)
        Me.FraMovement.Controls.Add(Me.lblNetAmount)
        Me.FraMovement.Controls.Add(Me.lblItemValue)
        Me.FraMovement.Controls.Add(Me.lblSuppCustCode)
        Me.FraMovement.Controls.Add(Me.lblCurRowNo)
        Me.FraMovement.Controls.Add(Me.lblPOType)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 406)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(751, 51)
        Me.FraMovement.TabIndex = 12
        Me.FraMovement.TabStop = False
        '
        'lblDivisionCode
        '
        Me.lblDivisionCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblDivisionCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDivisionCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivisionCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivisionCode.Location = New System.Drawing.Point(714, 36)
        Me.lblDivisionCode.Name = "lblDivisionCode"
        Me.lblDivisionCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDivisionCode.Size = New System.Drawing.Size(9, 7)
        Me.lblDivisionCode.TabIndex = 45
        Me.lblDivisionCode.Text = "lblDivisionCode"
        Me.lblDivisionCode.Visible = False
        '
        'lblSaleBillDate
        '
        Me.lblSaleBillDate.AutoSize = True
        Me.lblSaleBillDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblSaleBillDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSaleBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSaleBillDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSaleBillDate.Location = New System.Drawing.Point(62, 16)
        Me.lblSaleBillDate.Name = "lblSaleBillDate"
        Me.lblSaleBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSaleBillDate.Size = New System.Drawing.Size(73, 14)
        Me.lblSaleBillDate.TabIndex = 43
        Me.lblSaleBillDate.Text = "lblSaleBillDate"
        Me.lblSaleBillDate.Visible = False
        '
        'lblTotIGSTAmount
        '
        Me.lblTotIGSTAmount.AutoSize = True
        Me.lblTotIGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotIGSTAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotIGSTAmount.Location = New System.Drawing.Point(232, 14)
        Me.lblTotIGSTAmount.Name = "lblTotIGSTAmount"
        Me.lblTotIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGSTAmount.Size = New System.Drawing.Size(90, 14)
        Me.lblTotIGSTAmount.TabIndex = 42
        Me.lblTotIGSTAmount.Text = "lblTotIGSTAmount"
        Me.lblTotIGSTAmount.Visible = False
        '
        'lblTotSGSTAmount
        '
        Me.lblTotSGSTAmount.AutoSize = True
        Me.lblTotSGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotSGSTAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTotSGSTAmount.Location = New System.Drawing.Point(518, 36)
        Me.lblTotSGSTAmount.Name = "lblTotSGSTAmount"
        Me.lblTotSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGSTAmount.Size = New System.Drawing.Size(95, 14)
        Me.lblTotSGSTAmount.TabIndex = 41
        Me.lblTotSGSTAmount.Text = "lblTotSGSTAmount"
        Me.lblTotSGSTAmount.Visible = False
        '
        'lblNetExpAmount
        '
        Me.lblNetExpAmount.AutoSize = True
        Me.lblNetExpAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetExpAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetExpAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetExpAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNetExpAmount.Location = New System.Drawing.Point(482, 18)
        Me.lblNetExpAmount.Name = "lblNetExpAmount"
        Me.lblNetExpAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetExpAmount.Size = New System.Drawing.Size(88, 14)
        Me.lblNetExpAmount.TabIndex = 34
        Me.lblNetExpAmount.Text = "lblNetExpAmount"
        Me.lblNetExpAmount.Visible = False
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.lblRemarks.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRemarks.Location = New System.Drawing.Point(452, 32)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRemarks.Size = New System.Drawing.Size(59, 14)
        Me.lblRemarks.TabIndex = 33
        Me.lblRemarks.Text = "lblRemarks"
        Me.lblRemarks.Visible = False
        '
        'lblNarration
        '
        Me.lblNarration.AutoSize = True
        Me.lblNarration.BackColor = System.Drawing.SystemColors.Control
        Me.lblNarration.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNarration.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNarration.Location = New System.Drawing.Point(380, 36)
        Me.lblNarration.Name = "lblNarration"
        Me.lblNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNarration.Size = New System.Drawing.Size(61, 14)
        Me.lblNarration.TabIndex = 32
        Me.lblNarration.Text = "lblNarration"
        Me.lblNarration.Visible = False
        '
        'lblDueDate
        '
        Me.lblDueDate.AutoSize = True
        Me.lblDueDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDueDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDueDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDueDate.Location = New System.Drawing.Point(424, 14)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDueDate.Size = New System.Drawing.Size(58, 14)
        Me.lblDueDate.TabIndex = 31
        Me.lblDueDate.Text = "lblDueDate"
        Me.lblDueDate.Visible = False
        '
        'LblFOC
        '
        Me.LblFOC.AutoSize = True
        Me.LblFOC.BackColor = System.Drawing.SystemColors.Control
        Me.LblFOC.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblFOC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFOC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblFOC.Location = New System.Drawing.Point(338, 32)
        Me.LblFOC.Name = "LblFOC"
        Me.LblFOC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblFOC.Size = New System.Drawing.Size(37, 14)
        Me.LblFOC.TabIndex = 30
        Me.LblFOC.Text = "Lblfoc"
        Me.LblFOC.Visible = False
        '
        'lblCancelled
        '
        Me.lblCancelled.AutoSize = True
        Me.lblCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.lblCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCancelled.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCancelled.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCancelled.Location = New System.Drawing.Point(348, 14)
        Me.lblCancelled.Name = "lblCancelled"
        Me.lblCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCancelled.Size = New System.Drawing.Size(64, 14)
        Me.lblCancelled.TabIndex = 29
        Me.lblCancelled.Text = "lblCancelled"
        Me.lblCancelled.Visible = False
        '
        'lblNetAmount
        '
        Me.lblNetAmount.AutoSize = True
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNetAmount.Location = New System.Drawing.Point(260, 34)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(70, 14)
        Me.lblNetAmount.TabIndex = 28
        Me.lblNetAmount.Text = "lblNetAmount"
        Me.lblNetAmount.Visible = False
        '
        'lblItemValue
        '
        Me.lblItemValue.AutoSize = True
        Me.lblItemValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemValue.Location = New System.Drawing.Point(266, 14)
        Me.lblItemValue.Name = "lblItemValue"
        Me.lblItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemValue.Size = New System.Drawing.Size(63, 14)
        Me.lblItemValue.TabIndex = 27
        Me.lblItemValue.Text = "lblItemValue"
        '
        'lblSuppCustCode
        '
        Me.lblSuppCustCode.AutoSize = True
        Me.lblSuppCustCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuppCustCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSuppCustCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuppCustCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSuppCustCode.Location = New System.Drawing.Point(168, 32)
        Me.lblSuppCustCode.Name = "lblSuppCustCode"
        Me.lblSuppCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSuppCustCode.Size = New System.Drawing.Size(89, 14)
        Me.lblSuppCustCode.TabIndex = 26
        Me.lblSuppCustCode.Text = "lblSuppCustCode"
        Me.lblSuppCustCode.Visible = False
        '
        'lblCurRowNo
        '
        Me.lblCurRowNo.AutoSize = True
        Me.lblCurRowNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblCurRowNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCurRowNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurRowNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurRowNo.Location = New System.Drawing.Point(168, 10)
        Me.lblCurRowNo.Name = "lblCurRowNo"
        Me.lblCurRowNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCurRowNo.Size = New System.Drawing.Size(70, 14)
        Me.lblCurRowNo.TabIndex = 25
        Me.lblCurRowNo.Text = "lblCurRowNo"
        Me.lblCurRowNo.Visible = False
        '
        'lblPOType
        '
        Me.lblPOType.BackColor = System.Drawing.SystemColors.Control
        Me.lblPOType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPOType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPOType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPOType.Location = New System.Drawing.Point(10, 28)
        Me.lblPOType.Name = "lblPOType"
        Me.lblPOType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPOType.Size = New System.Drawing.Size(71, 17)
        Me.lblPOType.TabIndex = 18
        Me.lblPOType.Text = "lblPOType"
        Me.lblPOType.Visible = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(684, 16)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(47, 21)
        Me.lblBookType.TabIndex = 17
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(10, 18)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 16
        Me.lblMkey.Text = "lblMkey"
        Me.lblMkey.Visible = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(752, 411)
        Me.SprdView.TabIndex = 13
        '
        'frmPurchase_Acct_Post
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(752, 457)
        Me.Controls.Add(Me.FraTrn)
        Me.Controls.Add(Me.ADataGrid)
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
        Me.Name = "frmPurchase_Acct_Post"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Account Posting Reset in Purchase Voucher"
        Me.FraTrn.ResumeLayout(False)
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
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