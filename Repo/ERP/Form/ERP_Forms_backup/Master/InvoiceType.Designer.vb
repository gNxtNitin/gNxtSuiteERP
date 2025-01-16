Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmInvType
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
    Public WithEvents chkGSTReq As System.Windows.Forms.CheckBox
    Public WithEvents chkSalesTaxReq As System.Windows.Forms.CheckBox
    Public WithEvents chkSaleReturn As System.Windows.Forms.CheckBox
    Public WithEvents chkFixAssets As System.Windows.Forms.CheckBox
    Public WithEvents chkInstitutional As System.Windows.Forms.CheckBox
    Public WithEvents chkOEM As System.Windows.Forms.CheckBox
    Public WithEvents chkAfterMarket As System.Windows.Forms.CheckBox
    Public WithEvents chkExportInvoice As System.Windows.Forms.CheckBox
    Public WithEvents chkScrapSale As System.Windows.Forms.CheckBox
    Public WithEvents chkJw As System.Windows.Forms.CheckBox
    Public WithEvents chkSale57 As System.Windows.Forms.CheckBox
    Public WithEvents chkSaleComp As System.Windows.Forms.CheckBox
    Public WithEvents chkSPD As System.Windows.Forms.CheckBox
    Public WithEvents chkSuppBill As System.Windows.Forms.CheckBox
    Public WithEvents chkStockTrf As System.Windows.Forms.CheckBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtAlias As System.Windows.Forms.TextBox
    Public WithEvents txtInvHeading As System.Windows.Forms.TextBox
    Public WithEvents _OptItemType_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptItemType_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents txtAccount As System.Windows.Forms.TextBox
    Public WithEvents _OptType_11 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_10 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_9 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_8 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_7 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_6 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_3 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_5 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_4 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _OptStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents TxtStartingNo As System.Windows.Forms.TextBox
    Public WithEvents TxtCode As System.Windows.Forms.TextBox
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblCategory As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LblSTaxType As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents ADataGrid As VB6.ADODC
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
    Public WithEvents OptItemType As VB6.RadioButtonArray
    Public WithEvents OptStatus As VB6.RadioButtonArray
    Public WithEvents OptType As VB6.RadioButtonArray
    Public WithEvents lblLabels As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvType))
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
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.CmdAcctSearch = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.chkGSTReq = New System.Windows.Forms.CheckBox()
        Me.chkSalesTaxReq = New System.Windows.Forms.CheckBox()
        Me.chkSaleReturn = New System.Windows.Forms.CheckBox()
        Me.chkFixAssets = New System.Windows.Forms.CheckBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkInstitutional = New System.Windows.Forms.CheckBox()
        Me.chkOEM = New System.Windows.Forms.CheckBox()
        Me.chkAfterMarket = New System.Windows.Forms.CheckBox()
        Me.chkExportInvoice = New System.Windows.Forms.CheckBox()
        Me.chkScrapSale = New System.Windows.Forms.CheckBox()
        Me.chkJw = New System.Windows.Forms.CheckBox()
        Me.chkSale57 = New System.Windows.Forms.CheckBox()
        Me.chkSaleComp = New System.Windows.Forms.CheckBox()
        Me.chkSPD = New System.Windows.Forms.CheckBox()
        Me.chkSuppBill = New System.Windows.Forms.CheckBox()
        Me.chkStockTrf = New System.Windows.Forms.CheckBox()
        Me.txtAlias = New System.Windows.Forms.TextBox()
        Me.txtInvHeading = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptItemType_0 = New System.Windows.Forms.RadioButton()
        Me._OptItemType_1 = New System.Windows.Forms.RadioButton()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.txtAccount = New System.Windows.Forms.TextBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._OptType_11 = New System.Windows.Forms.RadioButton()
        Me._OptType_10 = New System.Windows.Forms.RadioButton()
        Me._OptType_9 = New System.Windows.Forms.RadioButton()
        Me._OptType_8 = New System.Windows.Forms.RadioButton()
        Me._OptType_7 = New System.Windows.Forms.RadioButton()
        Me._OptType_6 = New System.Windows.Forms.RadioButton()
        Me._OptType_3 = New System.Windows.Forms.RadioButton()
        Me._OptType_5 = New System.Windows.Forms.RadioButton()
        Me._OptType_4 = New System.Windows.Forms.RadioButton()
        Me._OptType_2 = New System.Windows.Forms.RadioButton()
        Me._OptType_1 = New System.Windows.Forms.RadioButton()
        Me._OptType_0 = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me._OptStatus_1 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_0 = New System.Windows.Forms.RadioButton()
        Me.TxtStartingNo = New System.Windows.Forms.TextBox()
        Me.TxtCode = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LblSTaxType = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraGridView = New System.Windows.Forms.GroupBox()
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.OptItemType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.chkSameGSTN = New System.Windows.Forms.CheckBox()
        Me.FraView.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame4.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.FraGridView.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.OptItemType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(182, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 33)
        Me.cmdSavePrint.TabIndex = 29
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
        Me.cmdPreview.Location = New System.Drawing.Point(362, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(60, 33)
        Me.cmdPreview.TabIndex = 32
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
        Me.cmdPrint.Location = New System.Drawing.Point(302, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 34)
        Me.cmdPrint.TabIndex = 31
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
        Me.CmdClose.Location = New System.Drawing.Point(482, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 34
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
        Me.CmdView.Location = New System.Drawing.Point(422, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 34)
        Me.CmdView.TabIndex = 33
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
        Me.CmdSave.Location = New System.Drawing.Point(122, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 34)
        Me.CmdSave.TabIndex = 28
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
        Me.CmdDelete.Location = New System.Drawing.Point(242, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 34)
        Me.CmdDelete.TabIndex = 30
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
        Me.CmdModify.Location = New System.Drawing.Point(62, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 34)
        Me.CmdModify.TabIndex = 27
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
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(432, 16)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 22)
        Me.cmdsearch.TabIndex = 230
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'CmdAcctSearch
        '
        Me.CmdAcctSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdAcctSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAcctSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAcctSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAcctSearch.Image = CType(resources.GetObject("CmdAcctSearch.Image"), System.Drawing.Image)
        Me.CmdAcctSearch.Location = New System.Drawing.Point(432, 212)
        Me.CmdAcctSearch.Name = "CmdAcctSearch"
        Me.CmdAcctSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAcctSearch.Size = New System.Drawing.Size(29, 22)
        Me.CmdAcctSearch.TabIndex = 231
        Me.CmdAcctSearch.TabStop = False
        Me.CmdAcctSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAcctSearch, "Search")
        Me.CmdAcctSearch.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Controls.Add(Me.CmdAcctSearch)
        Me.FraView.Controls.Add(Me.cmdsearch)
        Me.FraView.Controls.Add(Me.chkGSTReq)
        Me.FraView.Controls.Add(Me.chkSalesTaxReq)
        Me.FraView.Controls.Add(Me.chkSaleReturn)
        Me.FraView.Controls.Add(Me.chkFixAssets)
        Me.FraView.Controls.Add(Me.Frame2)
        Me.FraView.Controls.Add(Me.txtAlias)
        Me.FraView.Controls.Add(Me.txtInvHeading)
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.Report1)
        Me.FraView.Controls.Add(Me.txtAccount)
        Me.FraView.Controls.Add(Me.Frame4)
        Me.FraView.Controls.Add(Me.Frame6)
        Me.FraView.Controls.Add(Me.TxtStartingNo)
        Me.FraView.Controls.Add(Me.TxtCode)
        Me.FraView.Controls.Add(Me.txtName)
        Me.FraView.Controls.Add(Me._lblLabels_2)
        Me.FraView.Controls.Add(Me._lblLabels_1)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Controls.Add(Me.lblCategory)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Controls.Add(Me.Label3)
        Me.FraView.Controls.Add(Me.LblSTaxType)
        Me.FraView.Controls.Add(Me._lblLabels_0)
        Me.FraView.Controls.Add(Me.Label2)
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -6)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(545, 371)
        Me.FraView.TabIndex = 35
        Me.FraView.TabStop = False
        '
        'chkGSTReq
        '
        Me.chkGSTReq.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkGSTReq.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGSTReq.Enabled = False
        Me.chkGSTReq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGSTReq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGSTReq.Location = New System.Drawing.Point(298, 338)
        Me.chkGSTReq.Name = "chkGSTReq"
        Me.chkGSTReq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGSTReq.Size = New System.Drawing.Size(183, 15)
        Me.chkGSTReq.TabIndex = 64
        Me.chkGSTReq.Text = "GST Refund Required"
        Me.chkGSTReq.UseVisualStyleBackColor = False
        '
        'chkSalesTaxReq
        '
        Me.chkSalesTaxReq.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkSalesTaxReq.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSalesTaxReq.Enabled = False
        Me.chkSalesTaxReq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSalesTaxReq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSalesTaxReq.Location = New System.Drawing.Point(298, 318)
        Me.chkSalesTaxReq.Name = "chkSalesTaxReq"
        Me.chkSalesTaxReq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSalesTaxReq.Size = New System.Drawing.Size(183, 15)
        Me.chkSalesTaxReq.TabIndex = 56
        Me.chkSalesTaxReq.Text = "Sales Tax Refund Required"
        Me.chkSalesTaxReq.UseVisualStyleBackColor = False
        Me.chkSalesTaxReq.Visible = False
        '
        'chkSaleReturn
        '
        Me.chkSaleReturn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkSaleReturn.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSaleReturn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSaleReturn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSaleReturn.Location = New System.Drawing.Point(330, 193)
        Me.chkSaleReturn.Name = "chkSaleReturn"
        Me.chkSaleReturn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSaleReturn.Size = New System.Drawing.Size(97, 15)
        Me.chkSaleReturn.TabIndex = 13
        Me.chkSaleReturn.Text = "Return"
        Me.chkSaleReturn.UseVisualStyleBackColor = False
        '
        'chkFixAssets
        '
        Me.chkFixAssets.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkFixAssets.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFixAssets.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFixAssets.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFixAssets.Location = New System.Drawing.Point(224, 193)
        Me.chkFixAssets.Name = "chkFixAssets"
        Me.chkFixAssets.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFixAssets.Size = New System.Drawing.Size(97, 15)
        Me.chkFixAssets.TabIndex = 12
        Me.chkFixAssets.Text = "Fixed Assets "
        Me.chkFixAssets.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame2.Controls.Add(Me.chkSameGSTN)
        Me.Frame2.Controls.Add(Me.chkInstitutional)
        Me.Frame2.Controls.Add(Me.chkOEM)
        Me.Frame2.Controls.Add(Me.chkAfterMarket)
        Me.Frame2.Controls.Add(Me.chkExportInvoice)
        Me.Frame2.Controls.Add(Me.chkScrapSale)
        Me.Frame2.Controls.Add(Me.chkJw)
        Me.Frame2.Controls.Add(Me.chkSale57)
        Me.Frame2.Controls.Add(Me.chkSaleComp)
        Me.Frame2.Controls.Add(Me.chkSPD)
        Me.Frame2.Controls.Add(Me.chkSuppBill)
        Me.Frame2.Controls.Add(Me.chkStockTrf)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(152, 236)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(377, 77)
        Me.Frame2.TabIndex = 51
        Me.Frame2.TabStop = False
        '
        'chkInstitutional
        '
        Me.chkInstitutional.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkInstitutional.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInstitutional.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInstitutional.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInstitutional.Location = New System.Drawing.Point(240, 44)
        Me.chkInstitutional.Name = "chkInstitutional"
        Me.chkInstitutional.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInstitutional.Size = New System.Drawing.Size(131, 17)
        Me.chkInstitutional.TabIndex = 60
        Me.chkInstitutional.Text = "Institutional"
        Me.chkInstitutional.UseVisualStyleBackColor = False
        '
        'chkOEM
        '
        Me.chkOEM.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkOEM.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOEM.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOEM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOEM.Location = New System.Drawing.Point(240, 28)
        Me.chkOEM.Name = "chkOEM"
        Me.chkOEM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOEM.Size = New System.Drawing.Size(131, 17)
        Me.chkOEM.TabIndex = 55
        Me.chkOEM.Text = "OEM"
        Me.chkOEM.UseVisualStyleBackColor = False
        '
        'chkAfterMarket
        '
        Me.chkAfterMarket.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkAfterMarket.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAfterMarket.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAfterMarket.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAfterMarket.Location = New System.Drawing.Point(240, 14)
        Me.chkAfterMarket.Name = "chkAfterMarket"
        Me.chkAfterMarket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAfterMarket.Size = New System.Drawing.Size(131, 17)
        Me.chkAfterMarket.TabIndex = 54
        Me.chkAfterMarket.Text = "After Market"
        Me.chkAfterMarket.UseVisualStyleBackColor = False
        '
        'chkExportInvoice
        '
        Me.chkExportInvoice.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkExportInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExportInvoice.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExportInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExportInvoice.Location = New System.Drawing.Point(116, 58)
        Me.chkExportInvoice.Name = "chkExportInvoice"
        Me.chkExportInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExportInvoice.Size = New System.Drawing.Size(119, 17)
        Me.chkExportInvoice.TabIndex = 53
        Me.chkExportInvoice.Text = "Export Invoice"
        Me.chkExportInvoice.UseVisualStyleBackColor = False
        '
        'chkScrapSale
        '
        Me.chkScrapSale.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkScrapSale.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkScrapSale.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrapSale.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkScrapSale.Location = New System.Drawing.Point(8, 58)
        Me.chkScrapSale.Name = "chkScrapSale"
        Me.chkScrapSale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkScrapSale.Size = New System.Drawing.Size(115, 17)
        Me.chkScrapSale.TabIndex = 19
        Me.chkScrapSale.Text = "Scrap Sale"
        Me.chkScrapSale.UseVisualStyleBackColor = False
        '
        'chkJw
        '
        Me.chkJw.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkJw.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkJw.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkJw.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkJw.Location = New System.Drawing.Point(8, 44)
        Me.chkJw.Name = "chkJw"
        Me.chkJw.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkJw.Size = New System.Drawing.Size(103, 17)
        Me.chkJw.TabIndex = 18
        Me.chkJw.Text = "Jobwork"
        Me.chkJw.UseVisualStyleBackColor = False
        '
        'chkSale57
        '
        Me.chkSale57.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkSale57.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSale57.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSale57.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSale57.Location = New System.Drawing.Point(8, 28)
        Me.chkSale57.Name = "chkSale57"
        Me.chkSale57.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSale57.Size = New System.Drawing.Size(103, 17)
        Me.chkSale57.TabIndex = 17
        Me.chkSale57.Text = "Sale 57 AB"
        Me.chkSale57.UseVisualStyleBackColor = False
        '
        'chkSaleComp
        '
        Me.chkSaleComp.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkSaleComp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSaleComp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSaleComp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSaleComp.Location = New System.Drawing.Point(8, 12)
        Me.chkSaleComp.Name = "chkSaleComp"
        Me.chkSaleComp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSaleComp.Size = New System.Drawing.Size(103, 17)
        Me.chkSaleComp.TabIndex = 16
        Me.chkSaleComp.Text = "Sale Comp."
        Me.chkSaleComp.UseVisualStyleBackColor = False
        '
        'chkSPD
        '
        Me.chkSPD.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkSPD.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSPD.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSPD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSPD.Location = New System.Drawing.Point(116, 44)
        Me.chkSPD.Name = "chkSPD"
        Me.chkSPD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSPD.Size = New System.Drawing.Size(119, 17)
        Me.chkSPD.TabIndex = 22
        Me.chkSPD.Text = "SPD Sale"
        Me.chkSPD.UseVisualStyleBackColor = False
        '
        'chkSuppBill
        '
        Me.chkSuppBill.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkSuppBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSuppBill.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSuppBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSuppBill.Location = New System.Drawing.Point(116, 28)
        Me.chkSuppBill.Name = "chkSuppBill"
        Me.chkSuppBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSuppBill.Size = New System.Drawing.Size(119, 17)
        Me.chkSuppBill.TabIndex = 21
        Me.chkSuppBill.Text = "Supp. Invoice"
        Me.chkSuppBill.UseVisualStyleBackColor = False
        '
        'chkStockTrf
        '
        Me.chkStockTrf.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkStockTrf.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStockTrf.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStockTrf.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStockTrf.Location = New System.Drawing.Point(116, 12)
        Me.chkStockTrf.Name = "chkStockTrf"
        Me.chkStockTrf.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStockTrf.Size = New System.Drawing.Size(119, 17)
        Me.chkStockTrf.TabIndex = 20
        Me.chkStockTrf.Text = "Stock Transfer"
        Me.chkStockTrf.UseVisualStyleBackColor = False
        '
        'txtAlias
        '
        Me.txtAlias.AcceptsReturn = True
        Me.txtAlias.BackColor = System.Drawing.SystemColors.Window
        Me.txtAlias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAlias.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAlias.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAlias.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAlias.Location = New System.Drawing.Point(152, 38)
        Me.txtAlias.MaxLength = 0
        Me.txtAlias.Name = "txtAlias"
        Me.txtAlias.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAlias.Size = New System.Drawing.Size(281, 22)
        Me.txtAlias.TabIndex = 2
        '
        'txtInvHeading
        '
        Me.txtInvHeading.AcceptsReturn = True
        Me.txtInvHeading.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvHeading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvHeading.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvHeading.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvHeading.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInvHeading.Location = New System.Drawing.Point(152, 60)
        Me.txtInvHeading.MaxLength = 0
        Me.txtInvHeading.Name = "txtInvHeading"
        Me.txtInvHeading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvHeading.Size = New System.Drawing.Size(281, 22)
        Me.txtInvHeading.TabIndex = 4
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame1.Controls.Add(Me._OptItemType_0)
        Me.Frame1.Controls.Add(Me._OptItemType_1)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(152, 308)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(137, 31)
        Me.Frame1.TabIndex = 47
        Me.Frame1.TabStop = False
        '
        '_OptItemType_0
        '
        Me._OptItemType_0.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptItemType_0.Checked = True
        Me._OptItemType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptItemType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptItemType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptItemType.SetIndex(Me._OptItemType_0, CType(0, Short))
        Me._OptItemType_0.Location = New System.Drawing.Point(8, 12)
        Me._OptItemType_0.Name = "_OptItemType_0"
        Me._OptItemType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptItemType_0.Size = New System.Drawing.Size(59, 16)
        Me._OptItemType_0.TabIndex = 23
        Me._OptItemType_0.TabStop = True
        Me._OptItemType_0.Text = "Raw"
        Me._OptItemType_0.UseVisualStyleBackColor = False
        '
        '_OptItemType_1
        '
        Me._OptItemType_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptItemType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptItemType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptItemType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptItemType.SetIndex(Me._OptItemType_1, CType(1, Short))
        Me._OptItemType_1.Location = New System.Drawing.Point(70, 12)
        Me._OptItemType_1.Name = "_OptItemType_1"
        Me._OptItemType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptItemType_1.Size = New System.Drawing.Size(61, 16)
        Me._OptItemType_1.TabIndex = 24
        Me._OptItemType_1.TabStop = True
        Me._OptItemType_1.Text = "Other"
        Me._OptItemType_1.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(486, 100)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 65
        '
        'txtAccount
        '
        Me.txtAccount.AcceptsReturn = True
        Me.txtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAccount.Location = New System.Drawing.Point(152, 212)
        Me.txtAccount.MaxLength = 0
        Me.txtAccount.Name = "txtAccount"
        Me.txtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAccount.Size = New System.Drawing.Size(281, 22)
        Me.txtAccount.TabIndex = 14
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame4.Controls.Add(Me._OptType_11)
        Me.Frame4.Controls.Add(Me._OptType_10)
        Me.Frame4.Controls.Add(Me._OptType_9)
        Me.Frame4.Controls.Add(Me._OptType_8)
        Me.Frame4.Controls.Add(Me._OptType_7)
        Me.Frame4.Controls.Add(Me._OptType_6)
        Me.Frame4.Controls.Add(Me._OptType_3)
        Me.Frame4.Controls.Add(Me._OptType_5)
        Me.Frame4.Controls.Add(Me._OptType_4)
        Me.Frame4.Controls.Add(Me._OptType_2)
        Me.Frame4.Controls.Add(Me._OptType_1)
        Me.Frame4.Controls.Add(Me._OptType_0)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(152, 82)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(303, 101)
        Me.Frame4.TabIndex = 43
        Me.Frame4.TabStop = False
        '
        '_OptType_11
        '
        Me._OptType_11.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_11, CType(11, Short))
        Me._OptType_11.Location = New System.Drawing.Point(154, 82)
        Me._OptType_11.Name = "_OptType_11"
        Me._OptType_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_11.Size = New System.Drawing.Size(137, 15)
        Me._OptType_11.TabIndex = 63
        Me._OptType_11.TabStop = True
        Me._OptType_11.Text = "Reverse Charge (S)"
        Me._OptType_11.UseVisualStyleBackColor = False
        '
        '_OptType_10
        '
        Me._OptType_10.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_10, CType(10, Short))
        Me._OptType_10.Location = New System.Drawing.Point(8, 82)
        Me._OptType_10.Name = "_OptType_10"
        Me._OptType_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_10.Size = New System.Drawing.Size(137, 15)
        Me._OptType_10.TabIndex = 62
        Me._OptType_10.TabStop = True
        Me._OptType_10.Text = "Reverse Charge (G)"
        Me._OptType_10.UseVisualStyleBackColor = False
        '
        '_OptType_9
        '
        Me._OptType_9.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_9, CType(9, Short))
        Me._OptType_9.Location = New System.Drawing.Point(154, 68)
        Me._OptType_9.Name = "_OptType_9"
        Me._OptType_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_9.Size = New System.Drawing.Size(125, 15)
        Me._OptType_9.TabIndex = 61
        Me._OptType_9.TabStop = True
        Me._OptType_9.Text = "Work Order"
        Me._OptType_9.UseVisualStyleBackColor = False
        '
        '_OptType_8
        '
        Me._OptType_8.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_8, CType(8, Short))
        Me._OptType_8.Location = New System.Drawing.Point(8, 68)
        Me._OptType_8.Name = "_OptType_8"
        Me._OptType_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_8.Size = New System.Drawing.Size(145, 15)
        Me._OptType_8.TabIndex = 59
        Me._OptType_8.TabStop = True
        Me._OptType_8.Text = "Export Miscellaneous"
        Me._OptType_8.UseVisualStyleBackColor = False
        '
        '_OptType_7
        '
        Me._OptType_7.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_7, CType(7, Short))
        Me._OptType_7.Location = New System.Drawing.Point(8, 54)
        Me._OptType_7.Name = "_OptType_7"
        Me._OptType_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_7.Size = New System.Drawing.Size(105, 15)
        Me._OptType_7.TabIndex = 58
        Me._OptType_7.TabStop = True
        Me._OptType_7.Text = "Export"
        Me._OptType_7.UseVisualStyleBackColor = False
        '
        '_OptType_6
        '
        Me._OptType_6.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_6, CType(6, Short))
        Me._OptType_6.Location = New System.Drawing.Point(154, 54)
        Me._OptType_6.Name = "_OptType_6"
        Me._OptType_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_6.Size = New System.Drawing.Size(125, 15)
        Me._OptType_6.TabIndex = 57
        Me._OptType_6.TabStop = True
        Me._OptType_6.Text = "Trading Invoice"
        Me._OptType_6.UseVisualStyleBackColor = False
        '
        '_OptType_3
        '
        Me._OptType_3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_3, CType(3, Short))
        Me._OptType_3.Location = New System.Drawing.Point(154, 12)
        Me._OptType_3.Name = "_OptType_3"
        Me._OptType_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_3.Size = New System.Drawing.Size(105, 15)
        Me._OptType_3.TabIndex = 8
        Me._OptType_3.TabStop = True
        Me._OptType_3.Text = "Rejection"
        Me._OptType_3.UseVisualStyleBackColor = False
        '
        '_OptType_5
        '
        Me._OptType_5.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_5, CType(5, Short))
        Me._OptType_5.Location = New System.Drawing.Point(154, 40)
        Me._OptType_5.Name = "_OptType_5"
        Me._OptType_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_5.Size = New System.Drawing.Size(125, 15)
        Me._OptType_5.TabIndex = 10
        Me._OptType_5.TabStop = True
        Me._OptType_5.Text = "Proforma Invoice"
        Me._OptType_5.UseVisualStyleBackColor = False
        '
        '_OptType_4
        '
        Me._OptType_4.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_4, CType(4, Short))
        Me._OptType_4.Location = New System.Drawing.Point(154, 26)
        Me._OptType_4.Name = "_OptType_4"
        Me._OptType_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_4.Size = New System.Drawing.Size(105, 15)
        Me._OptType_4.TabIndex = 9
        Me._OptType_4.TabStop = True
        Me._OptType_4.Text = "Cash Memo"
        Me._OptType_4.UseVisualStyleBackColor = False
        '
        '_OptType_2
        '
        Me._OptType_2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_2, CType(2, Short))
        Me._OptType_2.Location = New System.Drawing.Point(8, 40)
        Me._OptType_2.Name = "_OptType_2"
        Me._OptType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_2.Size = New System.Drawing.Size(105, 15)
        Me._OptType_2.TabIndex = 7
        Me._OptType_2.TabStop = True
        Me._OptType_2.Text = "Job Work"
        Me._OptType_2.UseVisualStyleBackColor = False
        '
        '_OptType_1
        '
        Me._OptType_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_1, CType(1, Short))
        Me._OptType_1.Location = New System.Drawing.Point(8, 26)
        Me._OptType_1.Name = "_OptType_1"
        Me._OptType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_1.Size = New System.Drawing.Size(105, 15)
        Me._OptType_1.TabIndex = 6
        Me._OptType_1.TabStop = True
        Me._OptType_1.Text = "Miscellaneous"
        Me._OptType_1.UseVisualStyleBackColor = False
        '
        '_OptType_0
        '
        Me._OptType_0.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptType_0.Checked = True
        Me._OptType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_0, CType(0, Short))
        Me._OptType_0.Location = New System.Drawing.Point(8, 12)
        Me._OptType_0.Name = "_OptType_0"
        Me._OptType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_0.Size = New System.Drawing.Size(105, 15)
        Me._OptType_0.TabIndex = 5
        Me._OptType_0.TabStop = True
        Me._OptType_0.Text = "Exciseable"
        Me._OptType_0.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame6.Controls.Add(Me._OptStatus_1)
        Me.Frame6.Controls.Add(Me._OptStatus_0)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(152, 334)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(137, 31)
        Me.Frame6.TabIndex = 42
        Me.Frame6.TabStop = False
        '
        '_OptStatus_1
        '
        Me._OptStatus_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_1, CType(1, Short))
        Me._OptStatus_1.Location = New System.Drawing.Point(70, 12)
        Me._OptStatus_1.Name = "_OptStatus_1"
        Me._OptStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_1.Size = New System.Drawing.Size(61, 16)
        Me._OptStatus_1.TabIndex = 26
        Me._OptStatus_1.TabStop = True
        Me._OptStatus_1.Text = "Close"
        Me._OptStatus_1.UseVisualStyleBackColor = False
        '
        '_OptStatus_0
        '
        Me._OptStatus_0.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptStatus_0.Checked = True
        Me._OptStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_0, CType(0, Short))
        Me._OptStatus_0.Location = New System.Drawing.Point(8, 12)
        Me._OptStatus_0.Name = "_OptStatus_0"
        Me._OptStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_0.Size = New System.Drawing.Size(59, 16)
        Me._OptStatus_0.TabIndex = 25
        Me._OptStatus_0.TabStop = True
        Me._OptStatus_0.Text = "Open"
        Me._OptStatus_0.UseVisualStyleBackColor = False
        '
        'TxtStartingNo
        '
        Me.TxtStartingNo.AcceptsReturn = True
        Me.TxtStartingNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtStartingNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtStartingNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtStartingNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStartingNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtStartingNo.Location = New System.Drawing.Point(152, 190)
        Me.TxtStartingNo.MaxLength = 0
        Me.TxtStartingNo.Name = "TxtStartingNo"
        Me.TxtStartingNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtStartingNo.Size = New System.Drawing.Size(69, 22)
        Me.TxtStartingNo.TabIndex = 11
        '
        'TxtCode
        '
        Me.TxtCode.AcceptsReturn = True
        Me.TxtCode.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCode.Location = New System.Drawing.Point(353, 42)
        Me.TxtCode.MaxLength = 0
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCode.Size = New System.Drawing.Size(43, 22)
        Me.TxtCode.TabIndex = 38
        Me.TxtCode.Text = "Text1"
        Me.TxtCode.Visible = False
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(152, 16)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(281, 22)
        Me.txtName.TabIndex = 1
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(64, 42)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(76, 13)
        Me._lblLabels_2.TabIndex = 50
        Me._lblLabels_2.Text = "Invoice Alias :"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(44, 64)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(95, 13)
        Me._lblLabels_1.TabIndex = 49
        Me._lblLabels_1.Text = "Invoice Heading :"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(81, 320)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 48
        Me.Label1.Text = "Item Type :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.lblCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCategory.Location = New System.Drawing.Point(478, 198)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCategory.Size = New System.Drawing.Size(67, 13)
        Me.lblCategory.TabIndex = 46
        Me.lblCategory.Text = "lblCategory"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 346)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Status :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(127, 13)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "Account Posting Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblSTaxType
        '
        Me.LblSTaxType.AutoSize = True
        Me.LblSTaxType.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.LblSTaxType.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblSTaxType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSTaxType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblSTaxType.Location = New System.Drawing.Point(8, 192)
        Me.LblSTaxType.Name = "LblSTaxType"
        Me.LblSTaxType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblSTaxType.Size = New System.Drawing.Size(113, 13)
        Me.LblSTaxType.TabIndex = 41
        Me.LblSTaxType.Text = "Invoice Starting No. :"
        Me.LblSTaxType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(8, 20)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(76, 13)
        Me._lblLabels_0.TabIndex = 40
        Me._lblLabels_0.Text = "Invoice Type :"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Identification :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraGridView
        '
        Me.FraGridView.BackColor = System.Drawing.SystemColors.Control
        Me.FraGridView.Controls.Add(Me.ADataGrid)
        Me.FraGridView.Controls.Add(Me.SprdView)
        Me.FraGridView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGridView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGridView.Location = New System.Drawing.Point(0, -6)
        Me.FraGridView.Name = "FraGridView"
        Me.FraGridView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGridView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGridView.Size = New System.Drawing.Size(545, 371)
        Me.FraGridView.TabIndex = 36
        Me.FraGridView.TabStop = False
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
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(4, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(537, 359)
        Me.SprdView.TabIndex = 52
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 360)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(545, 49)
        Me.FraMovement.TabIndex = 37
        Me.FraMovement.TabStop = False
        '
        'OptItemType
        '
        '
        'OptStatus
        '
        '
        'OptType
        '
        '
        'chkSameGSTN
        '
        Me.chkSameGSTN.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.chkSameGSTN.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSameGSTN.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSameGSTN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSameGSTN.Location = New System.Drawing.Point(240, 58)
        Me.chkSameGSTN.Name = "chkSameGSTN"
        Me.chkSameGSTN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSameGSTN.Size = New System.Drawing.Size(119, 17)
        Me.chkSameGSTN.TabIndex = 61
        Me.chkSameGSTN.Text = "Same GSTN"
        Me.chkSameGSTN.UseVisualStyleBackColor = False
        '
        'frmInvType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(545, 409)
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
        Me.Name = "frmInvType"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Invoice Type"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame4.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.FraGridView.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.OptItemType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Public WithEvents CmdAcctSearch As Button
    Public WithEvents cmdsearch As Button
    Public WithEvents chkSameGSTN As CheckBox
#End Region
End Class