Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmGSTReversalEntry
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
        'SalesGST.Master.Show()
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
    Public WithEvents cmdPopulate As System.Windows.Forms.Button
    Public WithEvents txtOGSTDate As System.Windows.Forms.TextBox
    Public WithEvents txtOGSTNo As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents frmOClaimDetail As System.Windows.Forms.GroupBox
    Public WithEvents txtJVNo As System.Windows.Forms.TextBox
    Public WithEvents txtJVDate As System.Windows.Forms.TextBox
    Public WithEvents chkFinalPost As System.Windows.Forms.CheckBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtGSTNo As System.Windows.Forms.TextBox
    Public WithEvents txtGSTDate As System.Windows.Forms.TextBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents txtAlreadyReversalAmt As System.Windows.Forms.TextBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtBillAmount As System.Windows.Forms.TextBox
    Public WithEvents txtTaxableAmount As System.Windows.Forms.TextBox
    Public WithEvents txtGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtCGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtIGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtSGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblModvatAmount As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtReversalAmount As System.Windows.Forms.TextBox
    Public WithEvents cboDebitAccount As System.Windows.Forms.ComboBox
    Public WithEvents txtInterestAmount As System.Windows.Forms.TextBox
    Public WithEvents txtNetReversalGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtReversalIGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtReversalSGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtReversalCGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents cboReversalRule As System.Windows.Forms.ComboBox
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lblVNOSeq As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblJVMkey As System.Windows.Forms.Label
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents LblBookCode As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblVNo As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGSTReversalEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdPopulate = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.frmOClaimDetail = New System.Windows.Forms.GroupBox()
        Me.txtOGSTDate = New System.Windows.Forms.TextBox()
        Me.txtOGSTNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtJVNo = New System.Windows.Forms.TextBox()
        Me.txtJVDate = New System.Windows.Forms.TextBox()
        Me.chkFinalPost = New System.Windows.Forms.CheckBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtGSTNo = New System.Windows.Forms.TextBox()
        Me.txtGSTDate = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtAlreadyReversalAmt = New System.Windows.Forms.TextBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.txtBillAmount = New System.Windows.Forms.TextBox()
        Me.txtTaxableAmount = New System.Windows.Forms.TextBox()
        Me.txtGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtCGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtIGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtSGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtBillDate = New System.Windows.Forms.TextBox()
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblModvatAmount = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtReversalAmount = New System.Windows.Forms.TextBox()
        Me.cboDebitAccount = New System.Windows.Forms.ComboBox()
        Me.txtInterestAmount = New System.Windows.Forms.TextBox()
        Me.txtNetReversalGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtReversalIGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtReversalSGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtReversalCGSTAmount = New System.Windows.Forms.TextBox()
        Me.cboReversalRule = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblVNOSeq = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblJVMkey = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.LblBookCode = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblVNo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraFront.SuspendLayout()
        Me.frmOClaimDetail.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdPopulate
        '
        Me.cmdPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulate.Image = CType(resources.GetObject("cmdPopulate.Image"), System.Drawing.Image)
        Me.cmdPopulate.Location = New System.Drawing.Point(502, 14)
        Me.cmdPopulate.Name = "cmdPopulate"
        Me.cmdPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulate.Size = New System.Drawing.Size(53, 21)
        Me.cmdPopulate.TabIndex = 7
        Me.cmdPopulate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPopulate, "Show Record")
        Me.cmdPopulate.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(564, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 35
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
        Me.CmdView.Location = New System.Drawing.Point(498, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 34
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
        Me.CmdPreview.Location = New System.Drawing.Point(432, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 38
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
        Me.cmdPrint.Location = New System.Drawing.Point(365, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 37
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(299, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 33
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(233, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 36
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(166, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 32
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
        Me.cmdModify.Location = New System.Drawing.Point(99, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 31
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
        Me.cmdAdd.Location = New System.Drawing.Point(32, 10)
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
        Me.FraFront.Controls.Add(Me.frmOClaimDetail)
        Me.FraFront.Controls.Add(Me.txtJVNo)
        Me.FraFront.Controls.Add(Me.txtJVDate)
        Me.FraFront.Controls.Add(Me.chkFinalPost)
        Me.FraFront.Controls.Add(Me.txtRemarks)
        Me.FraFront.Controls.Add(Me.txtGSTNo)
        Me.FraFront.Controls.Add(Me.txtGSTDate)
        Me.FraFront.Controls.Add(Me.chkCancelled)
        Me.FraFront.Controls.Add(Me.Frame2)
        Me.FraFront.Controls.Add(Me.Frame4)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.lblVNOSeq)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.lblJVMkey)
        Me.FraFront.Controls.Add(Me.LblMKey)
        Me.FraFront.Controls.Add(Me.LblBookCode)
        Me.FraFront.Controls.Add(Me.Label26)
        Me.FraFront.Controls.Add(Me.lblVNo)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -2)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(669, 439)
        Me.FraFront.TabIndex = 41
        Me.FraFront.TabStop = False
        '
        'frmOClaimDetail
        '
        Me.frmOClaimDetail.BackColor = System.Drawing.SystemColors.Control
        Me.frmOClaimDetail.Controls.Add(Me.cmdPopulate)
        Me.frmOClaimDetail.Controls.Add(Me.txtOGSTDate)
        Me.frmOClaimDetail.Controls.Add(Me.txtOGSTNo)
        Me.frmOClaimDetail.Controls.Add(Me.Label11)
        Me.frmOClaimDetail.Controls.Add(Me.Label10)
        Me.frmOClaimDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmOClaimDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frmOClaimDetail.Location = New System.Drawing.Point(0, 36)
        Me.frmOClaimDetail.Name = "frmOClaimDetail"
        Me.frmOClaimDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.frmOClaimDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frmOClaimDetail.Size = New System.Drawing.Size(669, 45)
        Me.frmOClaimDetail.TabIndex = 51
        Me.frmOClaimDetail.TabStop = False
        '
        'txtOGSTDate
        '
        Me.txtOGSTDate.AcceptsReturn = True
        Me.txtOGSTDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOGSTDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOGSTDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOGSTDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOGSTDate.ForeColor = System.Drawing.Color.Blue
        Me.txtOGSTDate.Location = New System.Drawing.Point(387, 14)
        Me.txtOGSTDate.MaxLength = 0
        Me.txtOGSTDate.Name = "txtOGSTDate"
        Me.txtOGSTDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOGSTDate.Size = New System.Drawing.Size(103, 20)
        Me.txtOGSTDate.TabIndex = 6
        '
        'txtOGSTNo
        '
        Me.txtOGSTNo.AcceptsReturn = True
        Me.txtOGSTNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtOGSTNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOGSTNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOGSTNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOGSTNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOGSTNo.Location = New System.Drawing.Point(119, 14)
        Me.txtOGSTNo.MaxLength = 0
        Me.txtOGSTNo.Name = "txtOGSTNo"
        Me.txtOGSTNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOGSTNo.Size = New System.Drawing.Size(113, 20)
        Me.txtOGSTNo.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(281, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(102, 14)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Original Claim Date :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(21, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(93, 14)
        Me.Label10.TabIndex = 52
        Me.Label10.Text = "Original Claim No :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtJVNo
        '
        Me.txtJVNo.AcceptsReturn = True
        Me.txtJVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtJVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJVNo.Enabled = False
        Me.txtJVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJVNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtJVNo.Location = New System.Drawing.Point(119, 414)
        Me.txtJVNo.MaxLength = 0
        Me.txtJVNo.Name = "txtJVNo"
        Me.txtJVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJVNo.Size = New System.Drawing.Size(97, 20)
        Me.txtJVNo.TabIndex = 29
        '
        'txtJVDate
        '
        Me.txtJVDate.AcceptsReturn = True
        Me.txtJVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtJVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJVDate.Enabled = False
        Me.txtJVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtJVDate.Location = New System.Drawing.Point(393, 414)
        Me.txtJVDate.MaxLength = 0
        Me.txtJVDate.Name = "txtJVDate"
        Me.txtJVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJVDate.Size = New System.Drawing.Size(97, 20)
        Me.txtJVDate.TabIndex = 30
        '
        'chkFinalPost
        '
        Me.chkFinalPost.AutoSize = True
        Me.chkFinalPost.BackColor = System.Drawing.SystemColors.Control
        Me.chkFinalPost.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFinalPost.Enabled = False
        Me.chkFinalPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFinalPost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFinalPost.Location = New System.Drawing.Point(502, 17)
        Me.chkFinalPost.Name = "chkFinalPost"
        Me.chkFinalPost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFinalPost.Size = New System.Drawing.Size(69, 18)
        Me.chkFinalPost.TabIndex = 3
        Me.chkFinalPost.Text = "FinalPost"
        Me.chkFinalPost.UseVisualStyleBackColor = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(119, 377)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(371, 33)
        Me.txtRemarks.TabIndex = 28
        '
        'txtGSTNo
        '
        Me.txtGSTNo.AcceptsReturn = True
        Me.txtGSTNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSTNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtGSTNo.Location = New System.Drawing.Point(119, 14)
        Me.txtGSTNo.MaxLength = 0
        Me.txtGSTNo.Name = "txtGSTNo"
        Me.txtGSTNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTNo.Size = New System.Drawing.Size(113, 20)
        Me.txtGSTNo.TabIndex = 1
        '
        'txtGSTDate
        '
        Me.txtGSTDate.AcceptsReturn = True
        Me.txtGSTDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSTDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTDate.ForeColor = System.Drawing.Color.Blue
        Me.txtGSTDate.Location = New System.Drawing.Point(387, 14)
        Me.txtGSTDate.MaxLength = 0
        Me.txtGSTDate.Name = "txtGSTDate"
        Me.txtGSTDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTDate.Size = New System.Drawing.Size(103, 20)
        Me.txtGSTDate.TabIndex = 2
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(583, 17)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(73, 18)
        Me.chkCancelled.TabIndex = 4
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtAlreadyReversalAmt)
        Me.Frame2.Controls.Add(Me.txtVNo)
        Me.Frame2.Controls.Add(Me.txtVDate)
        Me.Frame2.Controls.Add(Me.txtBillAmount)
        Me.Frame2.Controls.Add(Me.txtTaxableAmount)
        Me.Frame2.Controls.Add(Me.txtGSTAmount)
        Me.Frame2.Controls.Add(Me.txtCGSTAmount)
        Me.Frame2.Controls.Add(Me.txtIGSTAmount)
        Me.Frame2.Controls.Add(Me.txtSGSTAmount)
        Me.Frame2.Controls.Add(Me.txtBillDate)
        Me.Frame2.Controls.Add(Me.txtBillNo)
        Me.Frame2.Controls.Add(Me.txtSupplier)
        Me.Frame2.Controls.Add(Me.Label22)
        Me.Frame2.Controls.Add(Me.Label21)
        Me.Frame2.Controls.Add(Me.Label20)
        Me.Frame2.Controls.Add(Me.Label18)
        Me.Frame2.Controls.Add(Me.Label17)
        Me.Frame2.Controls.Add(Me.Label16)
        Me.Frame2.Controls.Add(Me.lblModvatAmount)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.Label12)
        Me.Frame2.Controls.Add(Me.Label14)
        Me.Frame2.Controls.Add(Me.Label13)
        Me.Frame2.Controls.Add(Me.lblCust)
        Me.Frame2.Enabled = False
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 76)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(669, 147)
        Me.Frame2.TabIndex = 54
        Me.Frame2.TabStop = False
        '
        'txtAlreadyReversalAmt
        '
        Me.txtAlreadyReversalAmt.AcceptsReturn = True
        Me.txtAlreadyReversalAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtAlreadyReversalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlreadyReversalAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAlreadyReversalAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAlreadyReversalAmt.ForeColor = System.Drawing.Color.Blue
        Me.txtAlreadyReversalAmt.Location = New System.Drawing.Point(581, 122)
        Me.txtAlreadyReversalAmt.MaxLength = 0
        Me.txtAlreadyReversalAmt.Name = "txtAlreadyReversalAmt"
        Me.txtAlreadyReversalAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlreadyReversalAmt.Size = New System.Drawing.Size(83, 20)
        Me.txtAlreadyReversalAmt.TabIndex = 19
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVNo.Location = New System.Drawing.Point(120, 12)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(113, 20)
        Me.txtVNo.TabIndex = 8
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.Color.Blue
        Me.txtVDate.Location = New System.Drawing.Point(388, 12)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(103, 20)
        Me.txtVDate.TabIndex = 9
        '
        'txtBillAmount
        '
        Me.txtBillAmount.AcceptsReturn = True
        Me.txtBillAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtBillAmount.Location = New System.Drawing.Point(119, 78)
        Me.txtBillAmount.MaxLength = 0
        Me.txtBillAmount.Name = "txtBillAmount"
        Me.txtBillAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillAmount.Size = New System.Drawing.Size(113, 20)
        Me.txtBillAmount.TabIndex = 13
        '
        'txtTaxableAmount
        '
        Me.txtTaxableAmount.AcceptsReturn = True
        Me.txtTaxableAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTaxableAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTaxableAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTaxableAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxableAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtTaxableAmount.Location = New System.Drawing.Point(387, 78)
        Me.txtTaxableAmount.MaxLength = 0
        Me.txtTaxableAmount.Name = "txtTaxableAmount"
        Me.txtTaxableAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTaxableAmount.Size = New System.Drawing.Size(103, 20)
        Me.txtTaxableAmount.TabIndex = 14
        '
        'txtGSTAmount
        '
        Me.txtGSTAmount.AcceptsReturn = True
        Me.txtGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtGSTAmount.Location = New System.Drawing.Point(387, 122)
        Me.txtGSTAmount.MaxLength = 0
        Me.txtGSTAmount.Name = "txtGSTAmount"
        Me.txtGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTAmount.Size = New System.Drawing.Size(103, 20)
        Me.txtGSTAmount.TabIndex = 18
        '
        'txtCGSTAmount
        '
        Me.txtCGSTAmount.AcceptsReturn = True
        Me.txtCGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtCGSTAmount.Location = New System.Drawing.Point(119, 100)
        Me.txtCGSTAmount.MaxLength = 0
        Me.txtCGSTAmount.Name = "txtCGSTAmount"
        Me.txtCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCGSTAmount.Size = New System.Drawing.Size(113, 20)
        Me.txtCGSTAmount.TabIndex = 15
        '
        'txtIGSTAmount
        '
        Me.txtIGSTAmount.AcceptsReturn = True
        Me.txtIGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtIGSTAmount.Location = New System.Drawing.Point(119, 122)
        Me.txtIGSTAmount.MaxLength = 0
        Me.txtIGSTAmount.Name = "txtIGSTAmount"
        Me.txtIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIGSTAmount.Size = New System.Drawing.Size(113, 20)
        Me.txtIGSTAmount.TabIndex = 17
        '
        'txtSGSTAmount
        '
        Me.txtSGSTAmount.AcceptsReturn = True
        Me.txtSGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtSGSTAmount.Location = New System.Drawing.Point(387, 100)
        Me.txtSGSTAmount.MaxLength = 0
        Me.txtSGSTAmount.Name = "txtSGSTAmount"
        Me.txtSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSGSTAmount.Size = New System.Drawing.Size(103, 20)
        Me.txtSGSTAmount.TabIndex = 16
        '
        'txtBillDate
        '
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Location = New System.Drawing.Point(387, 34)
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Name = "txtBillDate"
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.Size = New System.Drawing.Size(103, 20)
        Me.txtBillDate.TabIndex = 11
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtBillNo.Location = New System.Drawing.Point(119, 34)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(113, 20)
        Me.txtBillNo.TabIndex = 10
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplier.Location = New System.Drawing.Point(119, 56)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(371, 20)
        Me.txtSupplier.TabIndex = 12
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(497, 124)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(77, 14)
        Me.Label22.TabIndex = 75
        Me.Label22.Text = "Reversal Amt :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(80, 14)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(34, 14)
        Me.Label21.TabIndex = 73
        Me.Label21.Text = "VNo :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(340, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(43, 14)
        Me.Label20.TabIndex = 72
        Me.Label20.Text = "VDate :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(21, 82)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(93, 13)
        Me.Label18.TabIndex = 70
        Me.Label18.Text = "Bill Amount :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(280, 83)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(103, 13)
        Me.Label17.TabIndex = 69
        Me.Label17.Text = "Taxable Amount : "
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(264, 125)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(119, 13)
        Me.Label16.TabIndex = 68
        Me.Label16.Text = "Total GST Amount :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModvatAmount
        '
        Me.lblModvatAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblModvatAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModvatAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModvatAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModvatAmount.Location = New System.Drawing.Point(21, 104)
        Me.lblModvatAmount.Name = "lblModvatAmount"
        Me.lblModvatAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModvatAmount.Size = New System.Drawing.Size(93, 13)
        Me.lblModvatAmount.TabIndex = 60
        Me.lblModvatAmount.Text = "CGST Amount :"
        Me.lblModvatAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(21, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(93, 13)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "IGST Amount :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(290, 105)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(93, 13)
        Me.Label12.TabIndex = 58
        Me.Label12.Text = "SGST Amount : "
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(332, 37)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(51, 14)
        Me.Label14.TabIndex = 57
        Me.Label14.Text = "Bill Date :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(72, 36)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(42, 14)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "Bill No :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(30, 60)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(84, 14)
        Me.lblCust.TabIndex = 55
        Me.lblCust.Text = "Account Name :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtReversalAmount)
        Me.Frame4.Controls.Add(Me.cboDebitAccount)
        Me.Frame4.Controls.Add(Me.txtInterestAmount)
        Me.Frame4.Controls.Add(Me.txtNetReversalGSTAmount)
        Me.Frame4.Controls.Add(Me.txtReversalIGSTAmount)
        Me.Frame4.Controls.Add(Me.txtReversalSGSTAmount)
        Me.Frame4.Controls.Add(Me.txtReversalCGSTAmount)
        Me.Frame4.Controls.Add(Me.cboReversalRule)
        Me.Frame4.Controls.Add(Me.Label23)
        Me.Frame4.Controls.Add(Me.Label19)
        Me.Frame4.Controls.Add(Me.Label15)
        Me.Frame4.Controls.Add(Me.Label9)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Controls.Add(Me.Label5)
        Me.Frame4.Controls.Add(Me.Label7)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 218)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(669, 155)
        Me.Frame4.TabIndex = 61
        Me.Frame4.TabStop = False
        '
        'txtReversalAmount
        '
        Me.txtReversalAmount.AcceptsReturn = True
        Me.txtReversalAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtReversalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReversalAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReversalAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReversalAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtReversalAmount.Location = New System.Drawing.Point(119, 62)
        Me.txtReversalAmount.MaxLength = 0
        Me.txtReversalAmount.Name = "txtReversalAmount"
        Me.txtReversalAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReversalAmount.Size = New System.Drawing.Size(113, 20)
        Me.txtReversalAmount.TabIndex = 22
        '
        'cboDebitAccount
        '
        Me.cboDebitAccount.BackColor = System.Drawing.SystemColors.Window
        Me.cboDebitAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDebitAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDebitAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDebitAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDebitAccount.Location = New System.Drawing.Point(118, 38)
        Me.cboDebitAccount.Name = "cboDebitAccount"
        Me.cboDebitAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDebitAccount.Size = New System.Drawing.Size(371, 22)
        Me.cboDebitAccount.TabIndex = 21
        '
        'txtInterestAmount
        '
        Me.txtInterestAmount.AcceptsReturn = True
        Me.txtInterestAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtInterestAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInterestAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInterestAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInterestAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInterestAmount.Location = New System.Drawing.Point(387, 106)
        Me.txtInterestAmount.MaxLength = 0
        Me.txtInterestAmount.Name = "txtInterestAmount"
        Me.txtInterestAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInterestAmount.Size = New System.Drawing.Size(103, 20)
        Me.txtInterestAmount.TabIndex = 26
        '
        'txtNetReversalGSTAmount
        '
        Me.txtNetReversalGSTAmount.AcceptsReturn = True
        Me.txtNetReversalGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetReversalGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetReversalGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetReversalGSTAmount.Enabled = False
        Me.txtNetReversalGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetReversalGSTAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNetReversalGSTAmount.Location = New System.Drawing.Point(119, 128)
        Me.txtNetReversalGSTAmount.MaxLength = 0
        Me.txtNetReversalGSTAmount.Name = "txtNetReversalGSTAmount"
        Me.txtNetReversalGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetReversalGSTAmount.Size = New System.Drawing.Size(113, 20)
        Me.txtNetReversalGSTAmount.TabIndex = 27
        '
        'txtReversalIGSTAmount
        '
        Me.txtReversalIGSTAmount.AcceptsReturn = True
        Me.txtReversalIGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtReversalIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReversalIGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReversalIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReversalIGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtReversalIGSTAmount.Location = New System.Drawing.Point(119, 106)
        Me.txtReversalIGSTAmount.MaxLength = 0
        Me.txtReversalIGSTAmount.Name = "txtReversalIGSTAmount"
        Me.txtReversalIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReversalIGSTAmount.Size = New System.Drawing.Size(113, 20)
        Me.txtReversalIGSTAmount.TabIndex = 25
        '
        'txtReversalSGSTAmount
        '
        Me.txtReversalSGSTAmount.AcceptsReturn = True
        Me.txtReversalSGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtReversalSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReversalSGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReversalSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReversalSGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtReversalSGSTAmount.Location = New System.Drawing.Point(387, 84)
        Me.txtReversalSGSTAmount.MaxLength = 0
        Me.txtReversalSGSTAmount.Name = "txtReversalSGSTAmount"
        Me.txtReversalSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReversalSGSTAmount.Size = New System.Drawing.Size(103, 20)
        Me.txtReversalSGSTAmount.TabIndex = 24
        '
        'txtReversalCGSTAmount
        '
        Me.txtReversalCGSTAmount.AcceptsReturn = True
        Me.txtReversalCGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtReversalCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReversalCGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReversalCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReversalCGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtReversalCGSTAmount.Location = New System.Drawing.Point(119, 84)
        Me.txtReversalCGSTAmount.MaxLength = 0
        Me.txtReversalCGSTAmount.Name = "txtReversalCGSTAmount"
        Me.txtReversalCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReversalCGSTAmount.Size = New System.Drawing.Size(113, 20)
        Me.txtReversalCGSTAmount.TabIndex = 23
        '
        'cboReversalRule
        '
        Me.cboReversalRule.BackColor = System.Drawing.SystemColors.Window
        Me.cboReversalRule.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboReversalRule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReversalRule.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReversalRule.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboReversalRule.Location = New System.Drawing.Point(119, 14)
        Me.cboReversalRule.Name = "cboReversalRule"
        Me.cboReversalRule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboReversalRule.Size = New System.Drawing.Size(371, 22)
        Me.cboReversalRule.TabIndex = 20
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(9, 64)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(105, 13)
        Me.Label23.TabIndex = 74
        Me.Label23.Text = "Reversal Amount :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(34, 40)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(80, 14)
        Me.Label19.TabIndex = 71
        Me.Label19.Text = "Debit Account :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(290, 110)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(93, 13)
        Me.Label15.TabIndex = 67
        Me.Label15.Text = "Interest Paid :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(3, 129)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(111, 13)
        Me.Label9.TabIndex = 66
        Me.Label9.Text = "Net Reversal Amt :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(282, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(101, 13)
        Me.Label4.TabIndex = 65
        Me.Label4.Text = "Reversal SGST :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(15, 108)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(99, 13)
        Me.Label5.TabIndex = 64
        Me.Label5.Text = "Reversal  IGST :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(15, 86)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(99, 13)
        Me.Label7.TabIndex = 63
        Me.Label7.Text = "Reversal CGST :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(48, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(66, 14)
        Me.Label2.TabIndex = 62
        Me.Label2.Text = "Under Rule :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(302, 416)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(93, 13)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "JV Date : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVNOSeq
        '
        Me.lblVNOSeq.BackColor = System.Drawing.SystemColors.Control
        Me.lblVNOSeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVNOSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVNOSeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVNOSeq.Location = New System.Drawing.Point(612, 382)
        Me.lblVNOSeq.Name = "lblVNOSeq"
        Me.lblVNOSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVNOSeq.Size = New System.Drawing.Size(43, 11)
        Me.lblVNOSeq.TabIndex = 49
        Me.lblVNOSeq.Text = "lblVNOSeq"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(22, 416)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "JV No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblJVMkey
        '
        Me.lblJVMkey.AutoSize = True
        Me.lblJVMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblJVMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblJVMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJVMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblJVMkey.Location = New System.Drawing.Point(554, 382)
        Me.lblJVMkey.Name = "lblJVMkey"
        Me.lblJVMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblJVMkey.Size = New System.Drawing.Size(55, 14)
        Me.lblJVMkey.TabIndex = 47
        Me.lblJVMkey.Text = "lblJVMkey"
        '
        'LblMKey
        '
        Me.LblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMKey.Location = New System.Drawing.Point(558, 400)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(55, 11)
        Me.LblMKey.TabIndex = 46
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        'LblBookCode
        '
        Me.LblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.LblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblBookCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblBookCode.Location = New System.Drawing.Point(616, 398)
        Me.LblBookCode.Name = "LblBookCode"
        Me.LblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblBookCode.Size = New System.Drawing.Size(37, 13)
        Me.LblBookCode.TabIndex = 45
        Me.LblBookCode.Text = "LblBookCode"
        Me.LblBookCode.Visible = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(59, 381)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(55, 14)
        Me.Label26.TabIndex = 44
        Me.Label26.Text = "Remarks :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVNo
        '
        Me.lblVNo.AutoSize = True
        Me.lblVNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblVNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVNo.Location = New System.Drawing.Point(68, 16)
        Me.lblVNo.Name = "lblVNo"
        Me.lblVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVNo.Size = New System.Drawing.Size(46, 14)
        Me.lblVNo.TabIndex = 43
        Me.lblVNo.Text = "Ref No :"
        Me.lblVNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(348, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(35, 14)
        Me.Label6.TabIndex = 42
        Me.Label6.Text = "Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(669, 437)
        Me.SprdView.TabIndex = 40
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 432)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(669, 51)
        Me.Frame3.TabIndex = 39
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 39
        '
        'FrmGSTReversalEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(670, 484)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGSTReversalEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "GST Reversal Entry"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.frmOClaimDetail.ResumeLayout(False)
        Me.frmOClaimDetail.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class