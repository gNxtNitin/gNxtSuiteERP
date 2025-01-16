Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCategoryMst
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
        'Me.MDIParent = Admin.Master
        'Admin.Master.Show
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
    Public WithEvents _optClassification_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optClassification_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optClassification_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optClassification_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents chkMaxLevel As System.Windows.Forms.CheckBox
    Public WithEvents chkAutoMovement As System.Windows.Forms.CheckBox
    Public WithEvents chkER1 As System.Windows.Forms.CheckBox
    Public WithEvents chkQFRRequired As System.Windows.Forms.CheckBox
    Public WithEvents chkTPReport As System.Windows.Forms.CheckBox
    Public WithEvents chkTCRequired As System.Windows.Forms.CheckBox
    Public WithEvents chkCostingRequired As System.Windows.Forms.CheckBox
    Public WithEvents chkBOMItem As System.Windows.Forms.CheckBox
    Public WithEvents chkIndentItem As System.Windows.Forms.CheckBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtSales As System.Windows.Forms.TextBox
    Public WithEvents txtPurchase As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtAcctConsumption As System.Windows.Forms.TextBox
    Public WithEvents CboType As System.Windows.Forms.ComboBox
    Public WithEvents txtStockType As System.Windows.Forms.TextBox
    Public WithEvents txtDesc As System.Windows.Forms.TextBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Public WithEvents lblAcctConsumption As System.Windows.Forms.Label
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents lblDesc As System.Windows.Forms.Label
    Public WithEvents lblStockType As System.Windows.Forms.Label
    Public WithEvents lblCategory As System.Windows.Forms.Label
    Public WithEvents lblCode As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents ADataGrid As VB6.ADODC
    'Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraGridView As System.Windows.Forms.GroupBox
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblLabels As VB6.LabelArray
    Public WithEvents optClassification As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCategoryMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.txtItemPrefix = New System.Windows.Forms.TextBox()
        Me.lblItemCodePrefix = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkQuotation = New System.Windows.Forms.CheckBox()
        Me.chkAutoIssueSubStore = New System.Windows.Forms.CheckBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optClassification_0 = New System.Windows.Forms.RadioButton()
        Me._optClassification_3 = New System.Windows.Forms.RadioButton()
        Me._optClassification_2 = New System.Windows.Forms.RadioButton()
        Me._optClassification_1 = New System.Windows.Forms.RadioButton()
        Me.chkMaxLevel = New System.Windows.Forms.CheckBox()
        Me.chkAutoMovement = New System.Windows.Forms.CheckBox()
        Me.chkER1 = New System.Windows.Forms.CheckBox()
        Me.chkQFRRequired = New System.Windows.Forms.CheckBox()
        Me.chkTPReport = New System.Windows.Forms.CheckBox()
        Me.chkTCRequired = New System.Windows.Forms.CheckBox()
        Me.chkCostingRequired = New System.Windows.Forms.CheckBox()
        Me.chkBOMItem = New System.Windows.Forms.CheckBox()
        Me.chkIndentItem = New System.Windows.Forms.CheckBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtSales = New System.Windows.Forms.TextBox()
        Me.txtPurchase = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAcctConsumption = New System.Windows.Forms.TextBox()
        Me.CboType = New System.Windows.Forms.ComboBox()
        Me.txtStockType = New System.Windows.Forms.TextBox()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.lblAcctConsumption = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.lblStockType = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.FraGridView = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optClassification = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraView.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraGridView.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optClassification, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(182, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 33)
        Me.cmdSavePrint.TabIndex = 9
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPreview.Location = New System.Drawing.Point(362, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(60, 33)
        Me.cmdPreview.TabIndex = 12
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPreview, "Print Preview")
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(302, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 34)
        Me.cmdPrint.TabIndex = 11
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print Record")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdClose.Location = New System.Drawing.Point(482, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 14
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(422, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 34)
        Me.CmdView.TabIndex = 13
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdSave.Location = New System.Drawing.Point(122, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 34)
        Me.CmdSave.TabIndex = 8
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdDelete.Location = New System.Drawing.Point(242, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 34)
        Me.CmdDelete.TabIndex = 10
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdModify.Location = New System.Drawing.Point(62, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 34)
        Me.CmdModify.TabIndex = 7
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
        Me.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdAdd.Location = New System.Drawing.Point(2, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 34)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Controls.Add(Me.txtItemPrefix)
        Me.FraView.Controls.Add(Me.lblItemCodePrefix)
        Me.FraView.Controls.Add(Me.Frame2)
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.txtAcctConsumption)
        Me.FraView.Controls.Add(Me.CboType)
        Me.FraView.Controls.Add(Me.txtStockType)
        Me.FraView.Controls.Add(Me.txtDesc)
        Me.FraView.Controls.Add(Me.Report1)
        Me.FraView.Controls.Add(Me.txtCode)
        Me.FraView.Controls.Add(Me.lblAcctConsumption)
        Me.FraView.Controls.Add(Me.lblModDate)
        Me.FraView.Controls.Add(Me.Label48)
        Me.FraView.Controls.Add(Me.lblAddDate)
        Me.FraView.Controls.Add(Me.Label45)
        Me.FraView.Controls.Add(Me.lblModUser)
        Me.FraView.Controls.Add(Me.Label46)
        Me.FraView.Controls.Add(Me.lblAddUser)
        Me.FraView.Controls.Add(Me.Label44)
        Me.FraView.Controls.Add(Me.lblDesc)
        Me.FraView.Controls.Add(Me.lblStockType)
        Me.FraView.Controls.Add(Me.lblCategory)
        Me.FraView.Controls.Add(Me.lblCode)
        Me.FraView.Controls.Add(Me._lblLabels_1)
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(2, 2)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(542, 348)
        Me.FraView.TabIndex = 15
        Me.FraView.TabStop = False
        '
        'txtItemPrefix
        '
        Me.txtItemPrefix.AcceptsReturn = True
        Me.txtItemPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemPrefix.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemPrefix.ForeColor = System.Drawing.Color.Blue
        Me.txtItemPrefix.Location = New System.Drawing.Point(388, 107)
        Me.txtItemPrefix.MaxLength = 0
        Me.txtItemPrefix.Name = "txtItemPrefix"
        Me.txtItemPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemPrefix.Size = New System.Drawing.Size(50, 22)
        Me.txtItemPrefix.TabIndex = 46
        '
        'lblItemCodePrefix
        '
        Me.lblItemCodePrefix.AutoSize = True
        Me.lblItemCodePrefix.BackColor = System.Drawing.Color.Transparent
        Me.lblItemCodePrefix.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemCodePrefix.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCodePrefix.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemCodePrefix.Location = New System.Drawing.Point(287, 113)
        Me.lblItemCodePrefix.Name = "lblItemCodePrefix"
        Me.lblItemCodePrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemCodePrefix.Size = New System.Drawing.Size(98, 13)
        Me.lblItemCodePrefix.TabIndex = 47
        Me.lblItemCodePrefix.Text = "Item Code Prefix :"
        Me.lblItemCodePrefix.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame2.Controls.Add(Me.chkQuotation)
        Me.Frame2.Controls.Add(Me.chkAutoIssueSubStore)
        Me.Frame2.Controls.Add(Me.Frame3)
        Me.Frame2.Controls.Add(Me.chkMaxLevel)
        Me.Frame2.Controls.Add(Me.chkAutoMovement)
        Me.Frame2.Controls.Add(Me.chkER1)
        Me.Frame2.Controls.Add(Me.chkQFRRequired)
        Me.Frame2.Controls.Add(Me.chkTPReport)
        Me.Frame2.Controls.Add(Me.chkTCRequired)
        Me.Frame2.Controls.Add(Me.chkCostingRequired)
        Me.Frame2.Controls.Add(Me.chkBOMItem)
        Me.Frame2.Controls.Add(Me.chkIndentItem)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(4, 222)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(539, 97)
        Me.Frame2.TabIndex = 44
        Me.Frame2.TabStop = False
        '
        'chkQuotation
        '
        Me.chkQuotation.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkQuotation.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkQuotation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkQuotation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkQuotation.Location = New System.Drawing.Point(260, 75)
        Me.chkQuotation.Name = "chkQuotation"
        Me.chkQuotation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkQuotation.Size = New System.Drawing.Size(129, 16)
        Me.chkQuotation.TabIndex = 56
        Me.chkQuotation.Text = "Quotation Required"
        Me.chkQuotation.UseVisualStyleBackColor = False
        '
        'chkAutoIssueSubStore
        '
        Me.chkAutoIssueSubStore.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkAutoIssueSubStore.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoIssueSubStore.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoIssueSubStore.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoIssueSubStore.Location = New System.Drawing.Point(4, 75)
        Me.chkAutoIssueSubStore.Name = "chkAutoIssueSubStore"
        Me.chkAutoIssueSubStore.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoIssueSubStore.Size = New System.Drawing.Size(148, 16)
        Me.chkAutoIssueSubStore.TabIndex = 55
        Me.chkAutoIssueSubStore.Text = "Auto Issue Sub Store"
        Me.chkAutoIssueSubStore.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame3.Controls.Add(Me._optClassification_0)
        Me.Frame3.Controls.Add(Me._optClassification_3)
        Me.Frame3.Controls.Add(Me._optClassification_2)
        Me.Frame3.Controls.Add(Me._optClassification_1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(410, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(129, 96)
        Me.Frame3.TabIndex = 54
        Me.Frame3.TabStop = False
        '
        '_optClassification_0
        '
        Me._optClassification_0.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optClassification_0.Checked = True
        Me._optClassification_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optClassification_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optClassification_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optClassification.SetIndex(Me._optClassification_0, CType(0, Short))
        Me._optClassification_0.Location = New System.Drawing.Point(4, 12)
        Me._optClassification_0.Name = "_optClassification_0"
        Me._optClassification_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optClassification_0.Size = New System.Drawing.Size(97, 16)
        Me._optClassification_0.TabIndex = 58
        Me._optClassification_0.TabStop = True
        Me._optClassification_0.Text = "None"
        Me._optClassification_0.UseVisualStyleBackColor = False
        '
        '_optClassification_3
        '
        Me._optClassification_3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optClassification_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optClassification_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optClassification_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optClassification.SetIndex(Me._optClassification_3, CType(3, Short))
        Me._optClassification_3.Location = New System.Drawing.Point(4, 60)
        Me._optClassification_3.Name = "_optClassification_3"
        Me._optClassification_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optClassification_3.Size = New System.Drawing.Size(117, 16)
        Me._optClassification_3.TabIndex = 57
        Me._optClassification_3.TabStop = True
        Me._optClassification_3.Text = "Paint && Chemical"
        Me._optClassification_3.UseVisualStyleBackColor = False
        '
        '_optClassification_2
        '
        Me._optClassification_2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optClassification_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optClassification_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optClassification_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optClassification.SetIndex(Me._optClassification_2, CType(2, Short))
        Me._optClassification_2.Location = New System.Drawing.Point(4, 44)
        Me._optClassification_2.Name = "_optClassification_2"
        Me._optClassification_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optClassification_2.Size = New System.Drawing.Size(117, 16)
        Me._optClassification_2.TabIndex = 56
        Me._optClassification_2.TabStop = True
        Me._optClassification_2.Text = "Packing Material"
        Me._optClassification_2.UseVisualStyleBackColor = False
        '
        '_optClassification_1
        '
        Me._optClassification_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optClassification_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optClassification_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optClassification_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optClassification.SetIndex(Me._optClassification_1, CType(1, Short))
        Me._optClassification_1.Location = New System.Drawing.Point(4, 28)
        Me._optClassification_1.Name = "_optClassification_1"
        Me._optClassification_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optClassification_1.Size = New System.Drawing.Size(97, 16)
        Me._optClassification_1.TabIndex = 55
        Me._optClassification_1.TabStop = True
        Me._optClassification_1.Text = "Locks"
        Me._optClassification_1.UseVisualStyleBackColor = False
        '
        'chkMaxLevel
        '
        Me.chkMaxLevel.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkMaxLevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMaxLevel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMaxLevel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMaxLevel.Location = New System.Drawing.Point(260, 52)
        Me.chkMaxLevel.Name = "chkMaxLevel"
        Me.chkMaxLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMaxLevel.Size = New System.Drawing.Size(121, 16)
        Me.chkMaxLevel.TabIndex = 53
        Me.chkMaxLevel.Text = "Max Level Check"
        Me.chkMaxLevel.UseVisualStyleBackColor = False
        '
        'chkAutoMovement
        '
        Me.chkAutoMovement.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkAutoMovement.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoMovement.Location = New System.Drawing.Point(115, 52)
        Me.chkAutoMovement.Name = "chkAutoMovement"
        Me.chkAutoMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoMovement.Size = New System.Drawing.Size(129, 16)
        Me.chkAutoMovement.TabIndex = 52
        Me.chkAutoMovement.Text = "Auto Movement"
        Me.chkAutoMovement.UseVisualStyleBackColor = False
        '
        'chkER1
        '
        Me.chkER1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkER1.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkER1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkER1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkER1.Location = New System.Drawing.Point(4, 52)
        Me.chkER1.Name = "chkER1"
        Me.chkER1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkER1.Size = New System.Drawing.Size(97, 16)
        Me.chkER1.TabIndex = 51
        Me.chkER1.Text = "ER1 Item"
        Me.chkER1.UseVisualStyleBackColor = False
        '
        'chkQFRRequired
        '
        Me.chkQFRRequired.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkQFRRequired.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkQFRRequired.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkQFRRequired.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkQFRRequired.Location = New System.Drawing.Point(260, 32)
        Me.chkQFRRequired.Name = "chkQFRRequired"
        Me.chkQFRRequired.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkQFRRequired.Size = New System.Drawing.Size(131, 16)
        Me.chkQFRRequired.TabIndex = 50
        Me.chkQFRRequired.Text = "QFR Required"
        Me.chkQFRRequired.UseVisualStyleBackColor = False
        '
        'chkTPReport
        '
        Me.chkTPReport.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkTPReport.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTPReport.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTPReport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTPReport.Location = New System.Drawing.Point(260, 12)
        Me.chkTPReport.Name = "chkTPReport"
        Me.chkTPReport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTPReport.Size = New System.Drawing.Size(143, 16)
        Me.chkTPReport.TabIndex = 49
        Me.chkTPReport.Text = "TPI Report Required"
        Me.chkTPReport.UseVisualStyleBackColor = False
        '
        'chkTCRequired
        '
        Me.chkTCRequired.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkTCRequired.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTCRequired.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTCRequired.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTCRequired.Location = New System.Drawing.Point(115, 32)
        Me.chkTCRequired.Name = "chkTCRequired"
        Me.chkTCRequired.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTCRequired.Size = New System.Drawing.Size(97, 16)
        Me.chkTCRequired.TabIndex = 48
        Me.chkTCRequired.Text = "TC Required"
        Me.chkTCRequired.UseVisualStyleBackColor = False
        '
        'chkCostingRequired
        '
        Me.chkCostingRequired.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkCostingRequired.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCostingRequired.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCostingRequired.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCostingRequired.Location = New System.Drawing.Point(115, 12)
        Me.chkCostingRequired.Name = "chkCostingRequired"
        Me.chkCostingRequired.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCostingRequired.Size = New System.Drawing.Size(123, 16)
        Me.chkCostingRequired.TabIndex = 47
        Me.chkCostingRequired.Text = "Costing Required"
        Me.chkCostingRequired.UseVisualStyleBackColor = False
        '
        'chkBOMItem
        '
        Me.chkBOMItem.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkBOMItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBOMItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBOMItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBOMItem.Location = New System.Drawing.Point(4, 32)
        Me.chkBOMItem.Name = "chkBOMItem"
        Me.chkBOMItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBOMItem.Size = New System.Drawing.Size(97, 16)
        Me.chkBOMItem.TabIndex = 46
        Me.chkBOMItem.Text = "BOM Item"
        Me.chkBOMItem.UseVisualStyleBackColor = False
        '
        'chkIndentItem
        '
        Me.chkIndentItem.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkIndentItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkIndentItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIndentItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkIndentItem.Location = New System.Drawing.Point(4, 12)
        Me.chkIndentItem.Name = "chkIndentItem"
        Me.chkIndentItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkIndentItem.Size = New System.Drawing.Size(97, 16)
        Me.chkIndentItem.TabIndex = 45
        Me.chkIndentItem.Text = "Indent Item"
        Me.chkIndentItem.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame1.Controls.Add(Me.txtSales)
        Me.Frame1.Controls.Add(Me.txtPurchase)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(4, 164)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(539, 59)
        Me.Frame1.TabIndex = 37
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Posting"
        '
        'txtSales
        '
        Me.txtSales.AcceptsReturn = True
        Me.txtSales.BackColor = System.Drawing.SystemColors.Window
        Me.txtSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSales.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSales.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSales.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSales.Location = New System.Drawing.Point(128, 10)
        Me.txtSales.MaxLength = 0
        Me.txtSales.Name = "txtSales"
        Me.txtSales.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSales.Size = New System.Drawing.Size(355, 22)
        Me.txtSales.TabIndex = 41
        '
        'txtPurchase
        '
        Me.txtPurchase.AcceptsReturn = True
        Me.txtPurchase.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchase.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurchase.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchase.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPurchase.Location = New System.Drawing.Point(128, 34)
        Me.txtPurchase.MaxLength = 0
        Me.txtPurchase.Name = "txtPurchase"
        Me.txtPurchase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurchase.Size = New System.Drawing.Size(355, 22)
        Me.txtPurchase.TabIndex = 40
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(52, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "Sales :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(52, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Purchase :"
        '
        'txtAcctConsumption
        '
        Me.txtAcctConsumption.AcceptsReturn = True
        Me.txtAcctConsumption.BackColor = System.Drawing.SystemColors.Window
        Me.txtAcctConsumption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcctConsumption.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAcctConsumption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcctConsumption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAcctConsumption.Location = New System.Drawing.Point(152, 137)
        Me.txtAcctConsumption.MaxLength = 0
        Me.txtAcctConsumption.Name = "txtAcctConsumption"
        Me.txtAcctConsumption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAcctConsumption.Size = New System.Drawing.Size(287, 22)
        Me.txtAcctConsumption.TabIndex = 34
        '
        'CboType
        '
        Me.CboType.BackColor = System.Drawing.SystemColors.Window
        Me.CboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboType.Location = New System.Drawing.Point(152, 78)
        Me.CboType.Name = "CboType"
        Me.CboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboType.Size = New System.Drawing.Size(289, 21)
        Me.CboType.TabIndex = 4
        '
        'txtStockType
        '
        Me.txtStockType.AcceptsReturn = True
        Me.txtStockType.BackColor = System.Drawing.SystemColors.Window
        Me.txtStockType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStockType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStockType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockType.ForeColor = System.Drawing.Color.Blue
        Me.txtStockType.Location = New System.Drawing.Point(152, 107)
        Me.txtStockType.MaxLength = 0
        Me.txtStockType.Name = "txtStockType"
        Me.txtStockType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStockType.Size = New System.Drawing.Size(50, 22)
        Me.txtStockType.TabIndex = 5
        '
        'txtDesc
        '
        Me.txtDesc.AcceptsReturn = True
        Me.txtDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDesc.Location = New System.Drawing.Point(152, 48)
        Me.txtDesc.MaxLength = 0
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDesc.Size = New System.Drawing.Size(287, 22)
        Me.txtDesc.TabIndex = 3
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(486, 232)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 45
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCode.Location = New System.Drawing.Point(152, 18)
        Me.txtCode.MaxLength = 0
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.Size = New System.Drawing.Size(287, 22)
        Me.txtCode.TabIndex = 1
        '
        'lblAcctConsumption
        '
        Me.lblAcctConsumption.AutoSize = True
        Me.lblAcctConsumption.BackColor = System.Drawing.Color.Transparent
        Me.lblAcctConsumption.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcctConsumption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcctConsumption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcctConsumption.Location = New System.Drawing.Point(58, 142)
        Me.lblAcctConsumption.Name = "lblAcctConsumption"
        Me.lblAcctConsumption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcctConsumption.Size = New System.Drawing.Size(83, 13)
        Me.lblAcctConsumption.TabIndex = 36
        Me.lblAcctConsumption.Text = "Account Head :"
        Me.lblAcctConsumption.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.Color.Transparent
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(463, 323)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 33
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.Color.Transparent
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(404, 325)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 32
        Me.Label48.Text = "Mod Date:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.Color.Transparent
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(201, 323)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 31
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.Color.Transparent
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(141, 325)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 30
        Me.Label45.Text = "Add Date :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.Color.Transparent
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(331, 323)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 29
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.Color.Transparent
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(271, 325)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 28
        Me.Label46.Text = "Mod User:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.Color.Transparent
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(69, 323)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 27
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.Color.Transparent
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(10, 325)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 26
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDesc
        '
        Me.lblDesc.AutoSize = True
        Me.lblDesc.BackColor = System.Drawing.Color.Transparent
        Me.lblDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDesc.Location = New System.Drawing.Point(78, 84)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDesc.Size = New System.Drawing.Size(63, 13)
        Me.lblDesc.TabIndex = 25
        Me.lblDesc.Text = "Item Desc :"
        Me.lblDesc.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblStockType
        '
        Me.lblStockType.AutoSize = True
        Me.lblStockType.BackColor = System.Drawing.Color.Transparent
        Me.lblStockType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStockType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStockType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStockType.Location = New System.Drawing.Point(73, 113)
        Me.lblStockType.Name = "lblStockType"
        Me.lblStockType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStockType.Size = New System.Drawing.Size(68, 13)
        Me.lblStockType.TabIndex = 23
        Me.lblStockType.Text = "Stock Type :"
        Me.lblStockType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.BackColor = System.Drawing.Color.Transparent
        Me.lblCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCategory.Location = New System.Drawing.Point(12, 18)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCategory.Size = New System.Drawing.Size(67, 13)
        Me.lblCategory.TabIndex = 22
        Me.lblCategory.Text = "lblCategory"
        Me.lblCategory.Visible = False
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.Color.Transparent
        Me.lblCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCode.Location = New System.Drawing.Point(104, 23)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCode.Size = New System.Drawing.Size(40, 13)
        Me.lblCode.TabIndex = 21
        Me.lblCode.Text = "Code :"
        Me.lblCode.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(70, 54)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(71, 13)
        Me._lblLabels_1.TabIndex = 18
        Me._lblLabels_1.Text = "Description :"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraGridView
        '
        Me.FraGridView.BackColor = System.Drawing.SystemColors.Control
        Me.FraGridView.Controls.Add(Me.SprdView)
        Me.FraGridView.Controls.Add(Me.ADataGrid)
        Me.FraGridView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGridView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGridView.Location = New System.Drawing.Point(2, -6)
        Me.FraGridView.Name = "FraGridView"
        Me.FraGridView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGridView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGridView.Size = New System.Drawing.Size(545, 358)
        Me.FraGridView.TabIndex = 16
        Me.FraGridView.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(4, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(537, 344)
        Me.SprdView.TabIndex = 22
        '
        'ADataGrid
        '
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.CommandTimeout = 0
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.ConnectionString = Nothing
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Location = New System.Drawing.Point(166, 38)
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.Name = "ADataGrid"
        Me.ADataGrid.Size = New System.Drawing.Size(99, 27)
        Me.ADataGrid.TabIndex = 0
        Me.ADataGrid.Text = "Adodc1"
        Me.ADataGrid.Visible = False
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 347)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(545, 49)
        Me.FraMovement.TabIndex = 17
        Me.FraMovement.TabStop = False
        '
        'optClassification
        '
        '
        'frmCategoryMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(545, 397)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.FraGridView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCategoryMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Category Master"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraGridView.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optClassification, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub

    Public WithEvents chkAutoIssueSubStore As CheckBox
    Public WithEvents txtItemPrefix As TextBox
    Public WithEvents lblItemCodePrefix As Label
    Public WithEvents chkQuotation As CheckBox
#End Region
End Class