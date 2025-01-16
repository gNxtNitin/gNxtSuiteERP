Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmLCDiscEntry
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
    Public WithEvents chkGSTClaim As System.Windows.Forms.CheckBox
    Public WithEvents txtLCVDate As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchLC As System.Windows.Forms.Button
    Public WithEvents txtLCVNo As System.Windows.Forms.TextBox
    Public WithEvents txtDiscAmount As System.Windows.Forms.TextBox
    Public WithEvents txtAdvBankName As System.Windows.Forms.TextBox
    Public WithEvents txtModvatDate As System.Windows.Forms.TextBox
    Public WithEvents txtModvatNo As System.Windows.Forms.TextBox
    Public WithEvents txtBankName As System.Windows.Forms.TextBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents lblIGSTRefundAmount As System.Windows.Forms.Label
    Public WithEvents lblCGSTRefundAmount As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents lblSGSTRefundAmount As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtRefDate As System.Windows.Forms.TextBox
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents txtLCAmount As System.Windows.Forms.TextBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents txtBankVNoSuffix As System.Windows.Forms.TextBox
    Public WithEvents txtLCNo As System.Windows.Forms.TextBox
    Public WithEvents txtLCDate As System.Windows.Forms.TextBox
    Public WithEvents txtChqNo As System.Windows.Forms.TextBox
    Public WithEvents txtChqDate As System.Windows.Forms.TextBox
    Public WithEvents txtBookBalAmt As System.Windows.Forms.TextBox
    Public WithEvents txtVType As System.Windows.Forms.TextBox
    Public WithEvents txtBankVDate As System.Windows.Forms.TextBox
    Public WithEvents txtBankVNo As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblPaymentDetail As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents lblBankMKey As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblTotSGSTAmount As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblTotCGSTAmount As System.Windows.Forms.Label
    Public WithEvents lblTotIGSTAmount As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblTotItemValue As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblNetAmount As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtVNoPrefix As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents lblLCMkey As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents Label56 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label59 As System.Windows.Forms.Label
    Public WithEvents lblPurchaseVNo As System.Windows.Forms.Label
    Public WithEvents lblVNo As System.Windows.Forms.Label
    Public WithEvents lblVDate As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents AdoDCMain As VB6.ADODC
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
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblSODates As System.Windows.Forms.Label
    Public WithEvents lblSONos As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLCDiscEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdSearchLC = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.chkGSTClaim = New System.Windows.Forms.CheckBox()
        Me.txtLCVDate = New System.Windows.Forms.TextBox()
        Me.txtLCVNo = New System.Windows.Forms.TextBox()
        Me.txtDiscAmount = New System.Windows.Forms.TextBox()
        Me.txtAdvBankName = New System.Windows.Forms.TextBox()
        Me.txtModvatDate = New System.Windows.Forms.TextBox()
        Me.txtModvatNo = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblIGSTRefundAmount = New System.Windows.Forms.Label()
        Me.lblCGSTRefundAmount = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblSGSTRefundAmount = New System.Windows.Forms.Label()
        Me.txtRefDate = New System.Windows.Forms.TextBox()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.txtLCAmount = New System.Windows.Forms.TextBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.txtBankVNoSuffix = New System.Windows.Forms.TextBox()
        Me.txtLCNo = New System.Windows.Forms.TextBox()
        Me.txtLCDate = New System.Windows.Forms.TextBox()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.txtChqDate = New System.Windows.Forms.TextBox()
        Me.txtBookBalAmt = New System.Windows.Forms.TextBox()
        Me.txtVType = New System.Windows.Forms.TextBox()
        Me.txtBankVDate = New System.Windows.Forms.TextBox()
        Me.txtBankVNo = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblPaymentDetail = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.lblBankMKey = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblTotSGSTAmount = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblTotCGSTAmount = New System.Windows.Forms.Label()
        Me.lblTotIGSTAmount = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTotItemValue = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblNetAmount = New System.Windows.Forms.Label()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtVNoPrefix = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblLCMkey = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.lblPurchaseVNo = New System.Windows.Forms.Label()
        Me.lblVNo = New System.Windows.Forms.Label()
        Me.lblVDate = New System.Windows.Forms.Label()
        Me.AdoDCMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblSODates = New System.Windows.Forms.Label()
        Me.lblSONos = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdSearchLC
        '
        Me.CmdSearchLC.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchLC.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchLC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchLC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchLC.Image = CType(resources.GetObject("CmdSearchLC.Image"), System.Drawing.Image)
        Me.CmdSearchLC.Location = New System.Drawing.Point(206, 34)
        Me.CmdSearchLC.Name = "CmdSearchLC"
        Me.CmdSearchLC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchLC.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchLC.TabIndex = 82
        Me.CmdSearchLC.TabStop = False
        Me.CmdSearchLC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchLC, "Seach Pending DC")
        Me.CmdSearchLC.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(608, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 30
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
        Me.CmdView.Location = New System.Drawing.Point(542, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 29
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
        Me.CmdPreview.Location = New System.Drawing.Point(476, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 28
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
        Me.cmdPrint.Location = New System.Drawing.Point(409, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 27
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(343, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 26
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
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(277, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 25
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
        Me.cmdSave.Location = New System.Drawing.Point(210, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 24
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
        Me.cmdModify.Location = New System.Drawing.Point(143, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 23
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
        Me.cmdAdd.Location = New System.Drawing.Point(76, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.chkGSTClaim)
        Me.FraFront.Controls.Add(Me.txtLCVDate)
        Me.FraFront.Controls.Add(Me.CmdSearchLC)
        Me.FraFront.Controls.Add(Me.txtLCVNo)
        Me.FraFront.Controls.Add(Me.txtDiscAmount)
        Me.FraFront.Controls.Add(Me.txtAdvBankName)
        Me.FraFront.Controls.Add(Me.txtModvatDate)
        Me.FraFront.Controls.Add(Me.txtModvatNo)
        Me.FraFront.Controls.Add(Me.txtBankName)
        Me.FraFront.Controls.Add(Me.Frame6)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.txtVNoPrefix)
        Me.FraFront.Controls.Add(Me.txtVDate)
        Me.FraFront.Controls.Add(Me.txtVNo)
        Me.FraFront.Controls.Add(Me.Label21)
        Me.FraFront.Controls.Add(Me.lblLCMkey)
        Me.FraFront.Controls.Add(Me.Label20)
        Me.FraFront.Controls.Add(Me.Label15)
        Me.FraFront.Controls.Add(Me.Label11)
        Me.FraFront.Controls.Add(Me.Label38)
        Me.FraFront.Controls.Add(Me.Label56)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label59)
        Me.FraFront.Controls.Add(Me.lblPurchaseVNo)
        Me.FraFront.Controls.Add(Me.lblVNo)
        Me.FraFront.Controls.Add(Me.lblVDate)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(751, 451)
        Me.FraFront.TabIndex = 35
        Me.FraFront.TabStop = False
        '
        'chkGSTClaim
        '
        Me.chkGSTClaim.AutoSize = True
        Me.chkGSTClaim.BackColor = System.Drawing.SystemColors.Control
        Me.chkGSTClaim.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGSTClaim.Enabled = False
        Me.chkGSTClaim.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGSTClaim.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGSTClaim.Location = New System.Drawing.Point(650, 34)
        Me.chkGSTClaim.Name = "chkGSTClaim"
        Me.chkGSTClaim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGSTClaim.Size = New System.Drawing.Size(75, 18)
        Me.chkGSTClaim.TabIndex = 87
        Me.chkGSTClaim.Text = "GST Claim"
        Me.chkGSTClaim.UseVisualStyleBackColor = False
        '
        'txtLCVDate
        '
        Me.txtLCVDate.AcceptsReturn = True
        Me.txtLCVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtLCVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLCVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLCVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLCVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtLCVDate.Location = New System.Drawing.Point(306, 34)
        Me.txtLCVDate.MaxLength = 0
        Me.txtLCVDate.Name = "txtLCVDate"
        Me.txtLCVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLCVDate.Size = New System.Drawing.Size(73, 20)
        Me.txtLCVDate.TabIndex = 84
        '
        'txtLCVNo
        '
        Me.txtLCVNo.AcceptsReturn = True
        Me.txtLCVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtLCVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLCVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLCVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLCVNo.ForeColor = System.Drawing.Color.Blue
        Me.txtLCVNo.Location = New System.Drawing.Point(112, 34)
        Me.txtLCVNo.MaxLength = 0
        Me.txtLCVNo.Name = "txtLCVNo"
        Me.txtLCVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLCVNo.Size = New System.Drawing.Size(91, 20)
        Me.txtLCVNo.TabIndex = 80
        '
        'txtDiscAmount
        '
        Me.txtDiscAmount.AcceptsReturn = True
        Me.txtDiscAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiscAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiscAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDiscAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtDiscAmount.Location = New System.Drawing.Point(479, 74)
        Me.txtDiscAmount.MaxLength = 0
        Me.txtDiscAmount.Name = "txtDiscAmount"
        Me.txtDiscAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDiscAmount.Size = New System.Drawing.Size(101, 20)
        Me.txtDiscAmount.TabIndex = 9
        '
        'txtAdvBankName
        '
        Me.txtAdvBankName.AcceptsReturn = True
        Me.txtAdvBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdvBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdvBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdvBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvBankName.ForeColor = System.Drawing.Color.Blue
        Me.txtAdvBankName.Location = New System.Drawing.Point(112, 74)
        Me.txtAdvBankName.MaxLength = 0
        Me.txtAdvBankName.Name = "txtAdvBankName"
        Me.txtAdvBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdvBankName.Size = New System.Drawing.Size(269, 20)
        Me.txtAdvBankName.TabIndex = 8
        '
        'txtModvatDate
        '
        Me.txtModvatDate.AcceptsReturn = True
        Me.txtModvatDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtModvatDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModvatDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModvatDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModvatDate.ForeColor = System.Drawing.Color.Blue
        Me.txtModvatDate.Location = New System.Drawing.Point(650, 54)
        Me.txtModvatDate.MaxLength = 0
        Me.txtModvatDate.Name = "txtModvatDate"
        Me.txtModvatDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModvatDate.Size = New System.Drawing.Size(73, 20)
        Me.txtModvatDate.TabIndex = 7
        '
        'txtModvatNo
        '
        Me.txtModvatNo.AcceptsReturn = True
        Me.txtModvatNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtModvatNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModvatNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModvatNo.Enabled = False
        Me.txtModvatNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModvatNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtModvatNo.Location = New System.Drawing.Point(479, 54)
        Me.txtModvatNo.MaxLength = 0
        Me.txtModvatNo.Name = "txtModvatNo"
        Me.txtModvatNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModvatNo.Size = New System.Drawing.Size(101, 20)
        Me.txtModvatNo.TabIndex = 6
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Enabled = False
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.Color.Blue
        Me.txtBankName.Location = New System.Drawing.Point(112, 54)
        Me.txtBankName.MaxLength = 0
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(269, 20)
        Me.txtBankName.TabIndex = 5
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.Frame1)
        Me.Frame6.Controls.Add(Me.txtRefDate)
        Me.Frame6.Controls.Add(Me.txtRefNo)
        Me.Frame6.Controls.Add(Me.txtLCAmount)
        Me.Frame6.Controls.Add(Me.txtSupplier)
        Me.Frame6.Controls.Add(Me.txtBankVNoSuffix)
        Me.Frame6.Controls.Add(Me.txtLCNo)
        Me.Frame6.Controls.Add(Me.txtLCDate)
        Me.Frame6.Controls.Add(Me.txtChqNo)
        Me.Frame6.Controls.Add(Me.txtChqDate)
        Me.Frame6.Controls.Add(Me.txtBookBalAmt)
        Me.Frame6.Controls.Add(Me.txtVType)
        Me.Frame6.Controls.Add(Me.txtBankVDate)
        Me.Frame6.Controls.Add(Me.txtBankVNo)
        Me.Frame6.Controls.Add(Me.txtRemarks)
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Controls.Add(Me.lblPaymentDetail)
        Me.Frame6.Controls.Add(Me.Label14)
        Me.Frame6.Controls.Add(Me.Label12)
        Me.Frame6.Controls.Add(Me.Label4)
        Me.Frame6.Controls.Add(Me.lblCust)
        Me.Frame6.Controls.Add(Me.lblBankMKey)
        Me.Frame6.Controls.Add(Me.Label10)
        Me.Frame6.Controls.Add(Me.Label9)
        Me.Frame6.Controls.Add(Me.Label8)
        Me.Frame6.Controls.Add(Me.Label7)
        Me.Frame6.Controls.Add(Me.Label6)
        Me.Frame6.Controls.Add(Me.Label3)
        Me.Frame6.Controls.Add(Me.Label1)
        Me.Frame6.Controls.Add(Me.lblBookType)
        Me.Frame6.Controls.Add(Me.Label26)
        Me.Frame6.Controls.Add(Me.lblTotSGSTAmount)
        Me.Frame6.Controls.Add(Me.Label2)
        Me.Frame6.Controls.Add(Me.Label17)
        Me.Frame6.Controls.Add(Me.lblTotCGSTAmount)
        Me.Frame6.Controls.Add(Me.lblTotIGSTAmount)
        Me.Frame6.Controls.Add(Me.Label45)
        Me.Frame6.Controls.Add(Me.LblMKey)
        Me.Frame6.Controls.Add(Me.Label16)
        Me.Frame6.Controls.Add(Me.lblTotItemValue)
        Me.Frame6.Controls.Add(Me.Label13)
        Me.Frame6.Controls.Add(Me.lblNetAmount)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 98)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(751, 353)
        Me.Frame6.TabIndex = 40
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Other Charges"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.Label22)
        Me.Frame1.Controls.Add(Me.lblIGSTRefundAmount)
        Me.Frame1.Controls.Add(Me.lblCGSTRefundAmount)
        Me.Frame1.Controls.Add(Me.Label19)
        Me.Frame1.Controls.Add(Me.Label18)
        Me.Frame1.Controls.Add(Me.lblSGSTRefundAmount)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(572, 268)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(179, 79)
        Me.Frame1.TabIndex = 69
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "GST Credit"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(47, 56)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(36, 14)
        Me.Label22.TabIndex = 75
        Me.Label22.Text = "IGST :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblIGSTRefundAmount
        '
        Me.lblIGSTRefundAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblIGSTRefundAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIGSTRefundAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIGSTRefundAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIGSTRefundAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblIGSTRefundAmount.Location = New System.Drawing.Point(89, 54)
        Me.lblIGSTRefundAmount.Name = "lblIGSTRefundAmount"
        Me.lblIGSTRefundAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIGSTRefundAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblIGSTRefundAmount.TabIndex = 74
        Me.lblIGSTRefundAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCGSTRefundAmount
        '
        Me.lblCGSTRefundAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblCGSTRefundAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCGSTRefundAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCGSTRefundAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCGSTRefundAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCGSTRefundAmount.Location = New System.Drawing.Point(89, 14)
        Me.lblCGSTRefundAmount.Name = "lblCGSTRefundAmount"
        Me.lblCGSTRefundAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCGSTRefundAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblCGSTRefundAmount.TabIndex = 73
        Me.lblCGSTRefundAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(42, 16)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(41, 14)
        Me.Label19.TabIndex = 72
        Me.Label19.Text = "CGST :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(42, 36)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(41, 14)
        Me.Label18.TabIndex = 71
        Me.Label18.Text = "SGST :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSGSTRefundAmount
        '
        Me.lblSGSTRefundAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblSGSTRefundAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSGSTRefundAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSGSTRefundAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSGSTRefundAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblSGSTRefundAmount.Location = New System.Drawing.Point(89, 34)
        Me.lblSGSTRefundAmount.Name = "lblSGSTRefundAmount"
        Me.lblSGSTRefundAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSGSTRefundAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblSGSTRefundAmount.TabIndex = 70
        Me.lblSGSTRefundAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtRefDate
        '
        Me.txtRefDate.AcceptsReturn = True
        Me.txtRefDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDate.ForeColor = System.Drawing.Color.Blue
        Me.txtRefDate.Location = New System.Drawing.Point(284, 226)
        Me.txtRefDate.MaxLength = 0
        Me.txtRefDate.Name = "txtRefDate"
        Me.txtRefDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefDate.Size = New System.Drawing.Size(73, 20)
        Me.txtRefDate.TabIndex = 66
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.Color.Blue
        Me.txtRefNo.Location = New System.Drawing.Point(106, 226)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(119, 20)
        Me.txtRefNo.TabIndex = 65
        '
        'txtLCAmount
        '
        Me.txtLCAmount.AcceptsReturn = True
        Me.txtLCAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtLCAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLCAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLCAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLCAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtLCAmount.Location = New System.Drawing.Point(106, 266)
        Me.txtLCAmount.MaxLength = 0
        Me.txtLCAmount.Name = "txtLCAmount"
        Me.txtLCAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLCAmount.Size = New System.Drawing.Size(119, 20)
        Me.txtLCAmount.TabIndex = 21
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplier.Location = New System.Drawing.Point(106, 286)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(269, 20)
        Me.txtSupplier.TabIndex = 20
        '
        'txtBankVNoSuffix
        '
        Me.txtBankVNoSuffix.AcceptsReturn = True
        Me.txtBankVNoSuffix.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankVNoSuffix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankVNoSuffix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankVNoSuffix.Enabled = False
        Me.txtBankVNoSuffix.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankVNoSuffix.ForeColor = System.Drawing.Color.Blue
        Me.txtBankVNoSuffix.Location = New System.Drawing.Point(204, 186)
        Me.txtBankVNoSuffix.MaxLength = 0
        Me.txtBankVNoSuffix.Name = "txtBankVNoSuffix"
        Me.txtBankVNoSuffix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankVNoSuffix.Size = New System.Drawing.Size(21, 20)
        Me.txtBankVNoSuffix.TabIndex = 14
        '
        'txtLCNo
        '
        Me.txtLCNo.AcceptsReturn = True
        Me.txtLCNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtLCNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLCNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLCNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLCNo.ForeColor = System.Drawing.Color.Blue
        Me.txtLCNo.Location = New System.Drawing.Point(106, 246)
        Me.txtLCNo.MaxLength = 0
        Me.txtLCNo.Name = "txtLCNo"
        Me.txtLCNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLCNo.Size = New System.Drawing.Size(119, 20)
        Me.txtLCNo.TabIndex = 18
        '
        'txtLCDate
        '
        Me.txtLCDate.AcceptsReturn = True
        Me.txtLCDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtLCDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLCDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLCDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLCDate.ForeColor = System.Drawing.Color.Blue
        Me.txtLCDate.Location = New System.Drawing.Point(284, 246)
        Me.txtLCDate.MaxLength = 0
        Me.txtLCDate.Name = "txtLCDate"
        Me.txtLCDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLCDate.Size = New System.Drawing.Size(73, 20)
        Me.txtLCDate.TabIndex = 19
        '
        'txtChqNo
        '
        Me.txtChqNo.AcceptsReturn = True
        Me.txtChqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqNo.ForeColor = System.Drawing.Color.Blue
        Me.txtChqNo.Location = New System.Drawing.Point(106, 206)
        Me.txtChqNo.MaxLength = 0
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqNo.Size = New System.Drawing.Size(119, 20)
        Me.txtChqNo.TabIndex = 16
        '
        'txtChqDate
        '
        Me.txtChqDate.AcceptsReturn = True
        Me.txtChqDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqDate.ForeColor = System.Drawing.Color.Blue
        Me.txtChqDate.Location = New System.Drawing.Point(284, 206)
        Me.txtChqDate.MaxLength = 0
        Me.txtChqDate.Name = "txtChqDate"
        Me.txtChqDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqDate.Size = New System.Drawing.Size(73, 20)
        Me.txtChqDate.TabIndex = 17
        '
        'txtBookBalAmt
        '
        Me.txtBookBalAmt.AcceptsReturn = True
        Me.txtBookBalAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtBookBalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBookBalAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBookBalAmt.Enabled = False
        Me.txtBookBalAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBookBalAmt.ForeColor = System.Drawing.Color.Blue
        Me.txtBookBalAmt.Location = New System.Drawing.Point(106, 166)
        Me.txtBookBalAmt.MaxLength = 0
        Me.txtBookBalAmt.Name = "txtBookBalAmt"
        Me.txtBookBalAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBookBalAmt.Size = New System.Drawing.Size(119, 20)
        Me.txtBookBalAmt.TabIndex = 11
        Me.txtBookBalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtVType
        '
        Me.txtVType.AcceptsReturn = True
        Me.txtVType.BackColor = System.Drawing.SystemColors.Window
        Me.txtVType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVType.Enabled = False
        Me.txtVType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVType.ForeColor = System.Drawing.Color.Blue
        Me.txtVType.Location = New System.Drawing.Point(106, 186)
        Me.txtVType.MaxLength = 0
        Me.txtVType.Name = "txtVType"
        Me.txtVType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVType.Size = New System.Drawing.Size(45, 20)
        Me.txtVType.TabIndex = 12
        '
        'txtBankVDate
        '
        Me.txtBankVDate.AcceptsReturn = True
        Me.txtBankVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankVDate.Enabled = False
        Me.txtBankVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBankVDate.Location = New System.Drawing.Point(284, 186)
        Me.txtBankVDate.MaxLength = 0
        Me.txtBankVDate.Name = "txtBankVDate"
        Me.txtBankVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankVDate.Size = New System.Drawing.Size(73, 20)
        Me.txtBankVDate.TabIndex = 15
        '
        'txtBankVNo
        '
        Me.txtBankVNo.AcceptsReturn = True
        Me.txtBankVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankVNo.Enabled = False
        Me.txtBankVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankVNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBankVNo.Location = New System.Drawing.Point(152, 186)
        Me.txtBankVNo.MaxLength = 0
        Me.txtBankVNo.Name = "txtBankVNo"
        Me.txtBankVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankVNo.Size = New System.Drawing.Size(51, 20)
        Me.txtBankVNo.TabIndex = 13
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(106, 306)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(425, 20)
        Me.txtRemarks.TabIndex = 22
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 14)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(745, 149)
        Me.SprdMain.TabIndex = 10
        '
        'lblPaymentDetail
        '
        Me.lblPaymentDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaymentDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentDetail.Location = New System.Drawing.Point(438, 238)
        Me.lblPaymentDetail.Name = "lblPaymentDetail"
        Me.lblPaymentDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentDetail.Size = New System.Drawing.Size(65, 13)
        Me.lblPaymentDetail.TabIndex = 86
        Me.lblPaymentDetail.Text = "lblPaymentDetail"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(245, 228)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(35, 14)
        Me.Label14.TabIndex = 68
        Me.Label14.Text = "Date :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(56, 228)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(46, 14)
        Me.Label12.TabIndex = 67
        Me.Label12.Text = "Ref No :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(37, 268)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(65, 14)
        Me.Label4.TabIndex = 64
        Me.Label4.Text = "LC Amount :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(50, 288)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(52, 14)
        Me.lblCust.TabIndex = 63
        Me.lblCust.Text = "Supplier :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBankMKey
        '
        Me.lblBankMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblBankMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBankMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankMKey.Location = New System.Drawing.Point(446, 202)
        Me.lblBankMKey.Name = "lblBankMKey"
        Me.lblBankMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBankMKey.Size = New System.Drawing.Size(115, 17)
        Me.lblBankMKey.TabIndex = 62
        Me.lblBankMKey.Text = "lblBankMKey"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(60, 248)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(42, 14)
        Me.Label10.TabIndex = 61
        Me.Label10.Text = "LC No :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(245, 248)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(35, 14)
        Me.Label9.TabIndex = 60
        Me.Label9.Text = "Date :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(36, 208)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(66, 14)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Cheque No :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(245, 208)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(35, 14)
        Me.Label7.TabIndex = 58
        Me.Label7.Text = "Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(23, 168)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(79, 14)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "Bank Balance :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(245, 188)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(35, 14)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(41, 188)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "Bank VNo :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(414, 202)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(64, 14)
        Me.lblBookType.TabIndex = 53
        Me.lblBookType.Text = "lblBookType"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(47, 308)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(55, 14)
        Me.Label26.TabIndex = 52
        Me.Label26.Text = "Remarks :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotSGSTAmount
        '
        Me.lblTotSGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotSGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotSGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotSGSTAmount.Location = New System.Drawing.Point(650, 206)
        Me.lblTotSGSTAmount.Name = "lblTotSGSTAmount"
        Me.lblTotSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotSGSTAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblTotSGSTAmount.TabIndex = 51
        Me.lblTotSGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(603, 210)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(41, 14)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "SGST :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(603, 188)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(41, 14)
        Me.Label17.TabIndex = 49
        Me.Label17.Text = "CGST :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotCGSTAmount
        '
        Me.lblTotCGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotCGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotCGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotCGSTAmount.Location = New System.Drawing.Point(650, 186)
        Me.lblTotCGSTAmount.Name = "lblTotCGSTAmount"
        Me.lblTotCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotCGSTAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblTotCGSTAmount.TabIndex = 48
        Me.lblTotCGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotIGSTAmount
        '
        Me.lblTotIGSTAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotIGSTAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotIGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotIGSTAmount.Location = New System.Drawing.Point(650, 226)
        Me.lblTotIGSTAmount.Name = "lblTotIGSTAmount"
        Me.lblTotIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotIGSTAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblTotIGSTAmount.TabIndex = 47
        Me.lblTotIGSTAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label45.Location = New System.Drawing.Point(608, 228)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(36, 14)
        Me.Label45.TabIndex = 46
        Me.Label45.Text = "IGST :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblMKey
        '
        Me.LblMKey.AutoSize = True
        Me.LblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMKey.Location = New System.Drawing.Point(370, 204)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(48, 14)
        Me.LblMKey.TabIndex = 45
        Me.LblMKey.Text = "LblMKey"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(582, 168)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(62, 14)
        Me.Label16.TabIndex = 44
        Me.Label16.Text = "Item Value :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotItemValue
        '
        Me.lblTotItemValue.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotItemValue.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotItemValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTotItemValue.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotItemValue.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotItemValue.Location = New System.Drawing.Point(650, 167)
        Me.lblTotItemValue.Name = "lblTotItemValue"
        Me.lblTotItemValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTotItemValue.Size = New System.Drawing.Size(85, 17)
        Me.lblTotItemValue.TabIndex = 43
        Me.lblTotItemValue.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(576, 248)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(68, 14)
        Me.Label13.TabIndex = 42
        Me.Label13.Text = "Net Amount :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNetAmount
        '
        Me.lblNetAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetAmount.Location = New System.Drawing.Point(650, 246)
        Me.lblNetAmount.Name = "lblNetAmount"
        Me.lblNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetAmount.Size = New System.Drawing.Size(85, 19)
        Me.lblNetAmount.TabIndex = 41
        Me.lblNetAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(446, 12)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(129, 22)
        Me.cboDivision.TabIndex = 4
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
        Me.txtVNoPrefix.Location = New System.Drawing.Point(112, 14)
        Me.txtVNoPrefix.MaxLength = 0
        Me.txtVNoPrefix.Name = "txtVNoPrefix"
        Me.txtVNoPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoPrefix.Size = New System.Drawing.Size(41, 20)
        Me.txtVNoPrefix.TabIndex = 1
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtVDate.Location = New System.Drawing.Point(307, 14)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(73, 20)
        Me.txtVDate.TabIndex = 3
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.Color.Blue
        Me.txtVNo.Location = New System.Drawing.Point(153, 14)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(77, 20)
        Me.txtVNo.TabIndex = 2
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(238, 36)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(35, 14)
        Me.Label21.TabIndex = 85
        Me.Label21.Text = "Date :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLCMkey
        '
        Me.lblLCMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblLCMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLCMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLCMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLCMkey.Location = New System.Drawing.Point(624, 84)
        Me.lblLCMkey.Name = "lblLCMkey"
        Me.lblLCMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLCMkey.Size = New System.Drawing.Size(83, 15)
        Me.lblLCMkey.TabIndex = 83
        Me.lblLCMkey.Text = "lblLCMkey"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(28, 36)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(50, 14)
        Me.Label20.TabIndex = 81
        Me.Label20.Text = "LC VNo :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(426, 76)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(50, 14)
        Me.Label15.TabIndex = 79
        Me.Label15.Text = "Amount :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(6, 76)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(92, 14)
        Me.Label11.TabIndex = 78
        Me.Label11.Text = "Adv. Bank Name :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(422, 56)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(50, 14)
        Me.Label38.TabIndex = 77
        Me.Label38.Text = "GST No :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.SystemColors.Control
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(603, 58)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(35, 14)
        Me.Label56.TabIndex = 76
        Me.Label56.Text = "Date :"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(67, 14)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "Bank Name :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.BackColor = System.Drawing.SystemColors.Control
        Me.Label59.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label59.Location = New System.Drawing.Point(384, 14)
        Me.Label59.Name = "Label59"
        Me.Label59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label59.Size = New System.Drawing.Size(50, 14)
        Me.Label59.TabIndex = 39
        Me.Label59.Text = "Division :"
        Me.Label59.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPurchaseVNo
        '
        Me.lblPurchaseVNo.AutoSize = True
        Me.lblPurchaseVNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblPurchaseVNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPurchaseVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurchaseVNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPurchaseVNo.Location = New System.Drawing.Point(194, 14)
        Me.lblPurchaseVNo.Name = "lblPurchaseVNo"
        Me.lblPurchaseVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPurchaseVNo.Size = New System.Drawing.Size(0, 14)
        Me.lblPurchaseVNo.TabIndex = 38
        '
        'lblVNo
        '
        Me.lblVNo.AutoSize = True
        Me.lblVNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblVNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVNo.Location = New System.Drawing.Point(5, 16)
        Me.lblVNo.Name = "lblVNo"
        Me.lblVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVNo.Size = New System.Drawing.Size(70, 14)
        Me.lblVNo.TabIndex = 37
        Me.lblVNo.Text = "Voucher No :"
        Me.lblVNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVDate
        '
        Me.lblVDate.AutoSize = True
        Me.lblVDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblVDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVDate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVDate.Location = New System.Drawing.Point(239, 16)
        Me.lblVDate.Name = "lblVDate"
        Me.lblVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVDate.Size = New System.Drawing.Size(35, 14)
        Me.lblVDate.TabIndex = 36
        Me.lblVDate.Text = "Date :"
        Me.lblVDate.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AdoDCMain
        '
        Me.AdoDCMain.BackColor = System.Drawing.SystemColors.Window
        Me.AdoDCMain.CommandTimeout = 0
        Me.AdoDCMain.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AdoDCMain.ConnectionString = Nothing
        Me.AdoDCMain.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AdoDCMain.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdoDCMain.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AdoDCMain.Location = New System.Drawing.Point(56, 270)
        Me.AdoDCMain.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AdoDCMain.Name = "AdoDCMain"
        Me.AdoDCMain.Size = New System.Drawing.Size(313, 33)
        Me.AdoDCMain.TabIndex = 36
        Me.AdoDCMain.Text = "Adodc1"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(749, 443)
        Me.SprdView.TabIndex = 32
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
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.lblSODates)
        Me.Frame3.Controls.Add(Me.lblSONos)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 440)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(751, 51)
        Me.Frame3.TabIndex = 31
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 31
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
        Me.lblSODates.TabIndex = 34
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
        Me.lblSONos.TabIndex = 33
        Me.lblSONos.Text = "lblSONos"
        Me.lblSONos.Visible = False
        '
        'FrmLCDiscEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(752, 492)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.AdoDCMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmLCDiscEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "LC Discount Entry"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class