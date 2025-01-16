Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmGSTEntry
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
    Public WithEvents txtNetTransferAmount As System.Windows.Forms.TextBox
    Public WithEvents cboRCIGSTDC As System.Windows.Forms.ComboBox
    Public WithEvents cboRCSGSTDC As System.Windows.Forms.ComboBox
    Public WithEvents cboRCCGSTDC As System.Windows.Forms.ComboBox
    Public WithEvents txtRCCGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtRCSGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtRCIGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents cboIGSTDC As System.Windows.Forms.ComboBox
    Public WithEvents cboSGSTDC As System.Windows.Forms.ComboBox
    Public WithEvents cboCGSTDC As System.Windows.Forms.ComboBox
    Public WithEvents txtJVNo As System.Windows.Forms.TextBox
    Public WithEvents txtJVDate As System.Windows.Forms.TextBox
    Public WithEvents chkFinalPost As System.Windows.Forms.CheckBox
    Public WithEvents txtSGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtIGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents cboTransferType As System.Windows.Forms.ComboBox
    Public WithEvents txtCGSTAmount As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtGSTNo As System.Windows.Forms.TextBox
    Public WithEvents txtGSTDate As System.Windows.Forms.TextBox
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents lblNetDC As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblVNOSeq As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblJVMkey As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents LblBookCode As System.Windows.Forms.Label
    Public WithEvents lblModvatAmount As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblVNo As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGSTEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
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
        Me.txtNetTransferAmount = New System.Windows.Forms.TextBox()
        Me.cboRCIGSTDC = New System.Windows.Forms.ComboBox()
        Me.cboRCSGSTDC = New System.Windows.Forms.ComboBox()
        Me.cboRCCGSTDC = New System.Windows.Forms.ComboBox()
        Me.txtRCCGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtRCSGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtRCIGSTAmount = New System.Windows.Forms.TextBox()
        Me.cboIGSTDC = New System.Windows.Forms.ComboBox()
        Me.cboSGSTDC = New System.Windows.Forms.ComboBox()
        Me.cboCGSTDC = New System.Windows.Forms.ComboBox()
        Me.txtJVNo = New System.Windows.Forms.TextBox()
        Me.txtJVDate = New System.Windows.Forms.TextBox()
        Me.chkFinalPost = New System.Windows.Forms.CheckBox()
        Me.txtSGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtIGSTAmount = New System.Windows.Forms.TextBox()
        Me.cboTransferType = New System.Windows.Forms.ComboBox()
        Me.txtCGSTAmount = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtGSTNo = New System.Windows.Forms.TextBox()
        Me.txtGSTDate = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.lblNetDC = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblVNOSeq = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblJVMkey = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.LblBookCode = New System.Windows.Forms.Label()
        Me.lblModvatAmount = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblVNo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraFront.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.cmdClose.TabIndex = 27
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
        Me.CmdView.TabIndex = 26
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
        Me.CmdPreview.TabIndex = 30
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
        Me.cmdPrint.TabIndex = 29
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
        Me.cmdDelete.TabIndex = 25
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
        Me.cmdSavePrint.TabIndex = 28
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
        Me.cmdModify.Location = New System.Drawing.Point(99, 10)
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
        Me.FraFront.Controls.Add(Me.txtNetTransferAmount)
        Me.FraFront.Controls.Add(Me.cboRCIGSTDC)
        Me.FraFront.Controls.Add(Me.cboRCSGSTDC)
        Me.FraFront.Controls.Add(Me.cboRCCGSTDC)
        Me.FraFront.Controls.Add(Me.txtRCCGSTAmount)
        Me.FraFront.Controls.Add(Me.txtRCSGSTAmount)
        Me.FraFront.Controls.Add(Me.txtRCIGSTAmount)
        Me.FraFront.Controls.Add(Me.cboIGSTDC)
        Me.FraFront.Controls.Add(Me.cboSGSTDC)
        Me.FraFront.Controls.Add(Me.cboCGSTDC)
        Me.FraFront.Controls.Add(Me.txtJVNo)
        Me.FraFront.Controls.Add(Me.txtJVDate)
        Me.FraFront.Controls.Add(Me.chkFinalPost)
        Me.FraFront.Controls.Add(Me.txtSGSTAmount)
        Me.FraFront.Controls.Add(Me.txtIGSTAmount)
        Me.FraFront.Controls.Add(Me.cboTransferType)
        Me.FraFront.Controls.Add(Me.txtCGSTAmount)
        Me.FraFront.Controls.Add(Me.txtRemarks)
        Me.FraFront.Controls.Add(Me.txtGSTNo)
        Me.FraFront.Controls.Add(Me.txtGSTDate)
        Me.FraFront.Controls.Add(Me.chkCancelled)
        Me.FraFront.Controls.Add(Me.txtSupplier)
        Me.FraFront.Controls.Add(Me.lblNetDC)
        Me.FraFront.Controls.Add(Me.Label9)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.lblVNOSeq)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.lblJVMkey)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.LblMKey)
        Me.FraFront.Controls.Add(Me.LblBookCode)
        Me.FraFront.Controls.Add(Me.lblModvatAmount)
        Me.FraFront.Controls.Add(Me.Label26)
        Me.FraFront.Controls.Add(Me.lblVNo)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(669, 287)
        Me.FraFront.TabIndex = 33
        Me.FraFront.TabStop = False
        '
        'txtNetTransferAmount
        '
        Me.txtNetTransferAmount.AcceptsReturn = True
        Me.txtNetTransferAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetTransferAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetTransferAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetTransferAmount.Enabled = False
        Me.txtNetTransferAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetTransferAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNetTransferAmount.Location = New System.Drawing.Point(108, 224)
        Me.txtNetTransferAmount.MaxLength = 0
        Me.txtNetTransferAmount.Name = "txtNetTransferAmount"
        Me.txtNetTransferAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetTransferAmount.Size = New System.Drawing.Size(113, 19)
        Me.txtNetTransferAmount.TabIndex = 17
        '
        'cboRCIGSTDC
        '
        Me.cboRCIGSTDC.BackColor = System.Drawing.SystemColors.Window
        Me.cboRCIGSTDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRCIGSTDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRCIGSTDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRCIGSTDC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRCIGSTDC.Location = New System.Drawing.Point(222, 200)
        Me.cboRCIGSTDC.Name = "cboRCIGSTDC"
        Me.cboRCIGSTDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRCIGSTDC.Size = New System.Drawing.Size(55, 22)
        Me.cboRCIGSTDC.TabIndex = 16
        '
        'cboRCSGSTDC
        '
        Me.cboRCSGSTDC.BackColor = System.Drawing.SystemColors.Window
        Me.cboRCSGSTDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRCSGSTDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRCSGSTDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRCSGSTDC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRCSGSTDC.Location = New System.Drawing.Point(222, 176)
        Me.cboRCSGSTDC.Name = "cboRCSGSTDC"
        Me.cboRCSGSTDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRCSGSTDC.Size = New System.Drawing.Size(55, 22)
        Me.cboRCSGSTDC.TabIndex = 14
        '
        'cboRCCGSTDC
        '
        Me.cboRCCGSTDC.BackColor = System.Drawing.SystemColors.Window
        Me.cboRCCGSTDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRCCGSTDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRCCGSTDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRCCGSTDC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRCCGSTDC.Location = New System.Drawing.Point(222, 154)
        Me.cboRCCGSTDC.Name = "cboRCCGSTDC"
        Me.cboRCCGSTDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRCCGSTDC.Size = New System.Drawing.Size(55, 22)
        Me.cboRCCGSTDC.TabIndex = 12
        '
        'txtRCCGSTAmount
        '
        Me.txtRCCGSTAmount.AcceptsReturn = True
        Me.txtRCCGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtRCCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRCCGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRCCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRCCGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtRCCGSTAmount.Location = New System.Drawing.Point(108, 154)
        Me.txtRCCGSTAmount.MaxLength = 0
        Me.txtRCCGSTAmount.Name = "txtRCCGSTAmount"
        Me.txtRCCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRCCGSTAmount.Size = New System.Drawing.Size(113, 19)
        Me.txtRCCGSTAmount.TabIndex = 11
        '
        'txtRCSGSTAmount
        '
        Me.txtRCSGSTAmount.AcceptsReturn = True
        Me.txtRCSGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtRCSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRCSGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRCSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRCSGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtRCSGSTAmount.Location = New System.Drawing.Point(108, 177)
        Me.txtRCSGSTAmount.MaxLength = 0
        Me.txtRCSGSTAmount.Name = "txtRCSGSTAmount"
        Me.txtRCSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRCSGSTAmount.Size = New System.Drawing.Size(113, 19)
        Me.txtRCSGSTAmount.TabIndex = 13
        '
        'txtRCIGSTAmount
        '
        Me.txtRCIGSTAmount.AcceptsReturn = True
        Me.txtRCIGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtRCIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRCIGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRCIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRCIGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtRCIGSTAmount.Location = New System.Drawing.Point(108, 201)
        Me.txtRCIGSTAmount.MaxLength = 0
        Me.txtRCIGSTAmount.Name = "txtRCIGSTAmount"
        Me.txtRCIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRCIGSTAmount.Size = New System.Drawing.Size(113, 19)
        Me.txtRCIGSTAmount.TabIndex = 15
        '
        'cboIGSTDC
        '
        Me.cboIGSTDC.BackColor = System.Drawing.SystemColors.Window
        Me.cboIGSTDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboIGSTDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIGSTDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboIGSTDC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboIGSTDC.Location = New System.Drawing.Point(222, 130)
        Me.cboIGSTDC.Name = "cboIGSTDC"
        Me.cboIGSTDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboIGSTDC.Size = New System.Drawing.Size(55, 22)
        Me.cboIGSTDC.TabIndex = 10
        '
        'cboSGSTDC
        '
        Me.cboSGSTDC.BackColor = System.Drawing.SystemColors.Window
        Me.cboSGSTDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSGSTDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSGSTDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSGSTDC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSGSTDC.Location = New System.Drawing.Point(222, 108)
        Me.cboSGSTDC.Name = "cboSGSTDC"
        Me.cboSGSTDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSGSTDC.Size = New System.Drawing.Size(55, 22)
        Me.cboSGSTDC.TabIndex = 8
        '
        'cboCGSTDC
        '
        Me.cboCGSTDC.BackColor = System.Drawing.SystemColors.Window
        Me.cboCGSTDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCGSTDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCGSTDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCGSTDC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCGSTDC.Location = New System.Drawing.Point(222, 86)
        Me.cboCGSTDC.Name = "cboCGSTDC"
        Me.cboCGSTDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCGSTDC.Size = New System.Drawing.Size(55, 22)
        Me.cboCGSTDC.TabIndex = 6
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
        Me.txtJVNo.Location = New System.Drawing.Point(566, 244)
        Me.txtJVNo.MaxLength = 0
        Me.txtJVNo.Name = "txtJVNo"
        Me.txtJVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJVNo.Size = New System.Drawing.Size(97, 19)
        Me.txtJVNo.TabIndex = 19
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
        Me.txtJVDate.Location = New System.Drawing.Point(566, 264)
        Me.txtJVDate.MaxLength = 0
        Me.txtJVDate.Name = "txtJVDate"
        Me.txtJVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJVDate.Size = New System.Drawing.Size(97, 19)
        Me.txtJVDate.TabIndex = 20
        '
        'chkFinalPost
        '
        Me.chkFinalPost.AutoSize = True
        Me.chkFinalPost.BackColor = System.Drawing.SystemColors.Control
        Me.chkFinalPost.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFinalPost.Enabled = False
        Me.chkFinalPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFinalPost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkFinalPost.Location = New System.Drawing.Point(583, 20)
        Me.chkFinalPost.Name = "chkFinalPost"
        Me.chkFinalPost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFinalPost.Size = New System.Drawing.Size(69, 18)
        Me.chkFinalPost.TabIndex = 21
        Me.chkFinalPost.Text = "FinalPost"
        Me.chkFinalPost.UseVisualStyleBackColor = False
        '
        'txtSGSTAmount
        '
        Me.txtSGSTAmount.AcceptsReturn = True
        Me.txtSGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtSGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtSGSTAmount.Location = New System.Drawing.Point(108, 109)
        Me.txtSGSTAmount.MaxLength = 0
        Me.txtSGSTAmount.Name = "txtSGSTAmount"
        Me.txtSGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSGSTAmount.Size = New System.Drawing.Size(113, 19)
        Me.txtSGSTAmount.TabIndex = 7
        '
        'txtIGSTAmount
        '
        Me.txtIGSTAmount.AcceptsReturn = True
        Me.txtIGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtIGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtIGSTAmount.Location = New System.Drawing.Point(108, 131)
        Me.txtIGSTAmount.MaxLength = 0
        Me.txtIGSTAmount.Name = "txtIGSTAmount"
        Me.txtIGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIGSTAmount.Size = New System.Drawing.Size(113, 19)
        Me.txtIGSTAmount.TabIndex = 9
        '
        'cboTransferType
        '
        Me.cboTransferType.BackColor = System.Drawing.SystemColors.Window
        Me.cboTransferType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTransferType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTransferType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTransferType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTransferType.Location = New System.Drawing.Point(108, 60)
        Me.cboTransferType.Name = "cboTransferType"
        Me.cboTransferType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTransferType.Size = New System.Drawing.Size(113, 22)
        Me.cboTransferType.TabIndex = 4
        '
        'txtCGSTAmount
        '
        Me.txtCGSTAmount.AcceptsReturn = True
        Me.txtCGSTAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCGSTAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCGSTAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCGSTAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCGSTAmount.ForeColor = System.Drawing.Color.Blue
        Me.txtCGSTAmount.Location = New System.Drawing.Point(108, 86)
        Me.txtCGSTAmount.MaxLength = 0
        Me.txtCGSTAmount.Name = "txtCGSTAmount"
        Me.txtCGSTAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCGSTAmount.Size = New System.Drawing.Size(113, 19)
        Me.txtCGSTAmount.TabIndex = 5
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(108, 247)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(389, 33)
        Me.txtRemarks.TabIndex = 18
        '
        'txtGSTNo
        '
        Me.txtGSTNo.AcceptsReturn = True
        Me.txtGSTNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSTNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtGSTNo.Location = New System.Drawing.Point(109, 14)
        Me.txtGSTNo.MaxLength = 0
        Me.txtGSTNo.Name = "txtGSTNo"
        Me.txtGSTNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTNo.Size = New System.Drawing.Size(113, 19)
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
        Me.txtGSTDate.Location = New System.Drawing.Point(463, 14)
        Me.txtGSTDate.MaxLength = 0
        Me.txtGSTDate.Name = "txtGSTDate"
        Me.txtGSTDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTDate.Size = New System.Drawing.Size(103, 19)
        Me.txtGSTDate.TabIndex = 2
        '
        'chkCancelled
        '
        Me.chkCancelled.AutoSize = True
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkCancelled.Location = New System.Drawing.Point(583, 39)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(73, 18)
        Me.chkCancelled.TabIndex = 22
        Me.chkCancelled.Text = "Cancelled"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplier.Location = New System.Drawing.Point(109, 36)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(457, 19)
        Me.txtSupplier.TabIndex = 3
        '
        'lblNetDC
        '
        Me.lblNetDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetDC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetDC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNetDC.Location = New System.Drawing.Point(221, 224)
        Me.lblNetDC.Name = "lblNetDC"
        Me.lblNetDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetDC.Size = New System.Drawing.Size(32, 19)
        Me.lblNetDC.TabIndex = 52
        Me.lblNetDC.Text = "DC"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(9, 227)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(93, 13)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Net Transfer :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(470, 268)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(93, 13)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "JV Date : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(9, 155)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(93, 13)
        Me.Label7.TabIndex = 49
        Me.Label7.Text = "CGST (RC) :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(9, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(93, 13)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "IGST (RC) :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(9, 204)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 47
        Me.Label4.Text = "SGST (RC) :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblVNOSeq
        '
        Me.lblVNOSeq.BackColor = System.Drawing.SystemColors.Control
        Me.lblVNOSeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVNOSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVNOSeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVNOSeq.Location = New System.Drawing.Point(592, 136)
        Me.lblVNOSeq.Name = "lblVNOSeq"
        Me.lblVNOSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVNOSeq.Size = New System.Drawing.Size(43, 11)
        Me.lblVNOSeq.TabIndex = 46
        Me.lblVNOSeq.Text = "lblVNOSeq"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(21, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(81, 14)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Transfer Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(468, 247)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "JV No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(9, 112)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(93, 13)
        Me.Label12.TabIndex = 43
        Me.Label12.Text = "SGST : "
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblJVMkey
        '
        Me.lblJVMkey.AutoSize = True
        Me.lblJVMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblJVMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblJVMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblJVMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblJVMkey.Location = New System.Drawing.Point(600, 164)
        Me.lblJVMkey.Name = "lblJVMkey"
        Me.lblJVMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblJVMkey.Size = New System.Drawing.Size(55, 14)
        Me.lblJVMkey.TabIndex = 42
        Me.lblJVMkey.Text = "lblJVMkey"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(9, 133)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(93, 13)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "IGST :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblMKey
        '
        Me.LblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMKey.Location = New System.Drawing.Point(592, 198)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(55, 11)
        Me.LblMKey.TabIndex = 40
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        'LblBookCode
        '
        Me.LblBookCode.BackColor = System.Drawing.SystemColors.Control
        Me.LblBookCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblBookCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBookCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblBookCode.Location = New System.Drawing.Point(600, 182)
        Me.LblBookCode.Name = "LblBookCode"
        Me.LblBookCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblBookCode.Size = New System.Drawing.Size(37, 13)
        Me.LblBookCode.TabIndex = 39
        Me.LblBookCode.Text = "LblBookCode"
        Me.LblBookCode.Visible = False
        '
        'lblModvatAmount
        '
        Me.lblModvatAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblModvatAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModvatAmount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModvatAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModvatAmount.Location = New System.Drawing.Point(9, 87)
        Me.lblModvatAmount.Name = "lblModvatAmount"
        Me.lblModvatAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModvatAmount.Size = New System.Drawing.Size(93, 13)
        Me.lblModvatAmount.TabIndex = 38
        Me.lblModvatAmount.Text = "CGST :"
        Me.lblModvatAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(47, 253)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(55, 14)
        Me.Label26.TabIndex = 37
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
        Me.lblVNo.Location = New System.Drawing.Point(56, 16)
        Me.lblVNo.Name = "lblVNo"
        Me.lblVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVNo.Size = New System.Drawing.Size(46, 14)
        Me.lblVNo.TabIndex = 36
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
        Me.Label6.Location = New System.Drawing.Point(403, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(35, 14)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(18, 40)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(84, 14)
        Me.lblCust.TabIndex = 34
        Me.lblCust.Text = "Account Name :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(669, 281)
        Me.SprdView.TabIndex = 32
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
        Me.Frame3.Location = New System.Drawing.Point(0, 276)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(669, 51)
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
        'FrmGSTEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(670, 327)
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
        Me.Name = "FrmGSTEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "GST Transfer Entry"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
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