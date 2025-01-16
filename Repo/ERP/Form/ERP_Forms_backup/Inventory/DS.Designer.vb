Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmDS
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
        '
        ''InventoryGST.Master.Show
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
    Public WithEvents cmdDivSearch As System.Windows.Forms.Button
    Public WithEvents txtDivision As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchItem As System.Windows.Forms.Button
    Public WithEvents txtSearchItem As System.Windows.Forms.TextBox
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents cmdPoSearch As System.Windows.Forms.Button
    Public WithEvents cmdPopulate As System.Windows.Forms.Button
    Public WithEvents cmdAmendSchd As System.Windows.Forms.Button
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents txtPOAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtPOAmendDate As System.Windows.Forms.TextBox
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents txtScheduleDate As System.Windows.Forms.TextBox
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents chkStatus As System.Windows.Forms.CheckBox
    Public WithEvents txtDSAmendDate As System.Windows.Forms.TextBox
    Public WithEvents txtDSAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtDSDate As System.Windows.Forms.TextBox
    Public WithEvents txtDSNo As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraAccounts As System.Windows.Forms.GroupBox
    Public WithEvents FraTrn As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents lblDetail As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDS))
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
        Me.cmdDivSearch = New System.Windows.Forms.Button()
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.cmdPoSearch = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.FraTrn = New System.Windows.Forms.GroupBox()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtDivision = New System.Windows.Forms.TextBox()
        Me.txtSearchItem = New System.Windows.Forms.TextBox()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdPopulate = New System.Windows.Forms.Button()
        Me.cmdAmendSchd = New System.Windows.Forms.Button()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.txtPOAmendNo = New System.Windows.Forms.TextBox()
        Me.txtPOAmendDate = New System.Windows.Forms.TextBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.txtScheduleDate = New System.Windows.Forms.TextBox()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.txtDSAmendDate = New System.Windows.Forms.TextBox()
        Me.txtDSAmendNo = New System.Windows.Forms.TextBox()
        Me.txtDSDate = New System.Windows.Forms.TextBox()
        Me.txtDSNo = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.fraAccounts = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblDetail = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.FraTrn.SuspendLayout()
        Me.fraTop1.SuspendLayout()
        Me.fraAccounts.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdDivSearch
        '
        Me.cmdDivSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDivSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDivSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDivSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDivSearch.Image = CType(resources.GetObject("cmdDivSearch.Image"), System.Drawing.Image)
        Me.cmdDivSearch.Location = New System.Drawing.Point(198, 103)
        Me.cmdDivSearch.Name = "cmdDivSearch"
        Me.cmdDivSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDivSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdDivSearch.TabIndex = 10
        Me.cmdDivSearch.TabStop = False
        Me.cmdDivSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDivSearch, "Search")
        Me.cmdDivSearch.UseVisualStyleBackColor = False
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(203, 193)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchItem.TabIndex = 14
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'cmdPoSearch
        '
        Me.cmdPoSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPoSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPoSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPoSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPoSearch.Image = CType(resources.GetObject("cmdPoSearch.Image"), System.Drawing.Image)
        Me.cmdPoSearch.Location = New System.Drawing.Point(198, 73)
        Me.cmdPoSearch.Name = "cmdPoSearch"
        Me.cmdPoSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPoSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdPoSearch.TabIndex = 8
        Me.cmdPoSearch.TabStop = False
        Me.cmdPoSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPoSearch, "Search")
        Me.cmdPoSearch.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(680, 42)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearch.TabIndex = 6
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(136, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(204, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 18
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(272, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 19
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
        Me.CmdDelete.Location = New System.Drawing.Point(406, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 21
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(608, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 24
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(676, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 25
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(474, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 22
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(340, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 20
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(542, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 23
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'FraTrn
        '
        Me.FraTrn.BackColor = System.Drawing.SystemColors.Control
        Me.FraTrn.Controls.Add(Me.fraTop1)
        Me.FraTrn.Controls.Add(Me.fraAccounts)
        Me.FraTrn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTrn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTrn.Location = New System.Drawing.Point(-1, -6)
        Me.FraTrn.Name = "FraTrn"
        Me.FraTrn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTrn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTrn.Size = New System.Drawing.Size(909, 578)
        Me.FraTrn.TabIndex = 26
        Me.FraTrn.TabStop = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.cmdDivSearch)
        Me.fraTop1.Controls.Add(Me.txtDivision)
        Me.fraTop1.Controls.Add(Me.cmdSearchItem)
        Me.fraTop1.Controls.Add(Me.txtSearchItem)
        Me.fraTop1.Controls.Add(Me.cmdRefresh)
        Me.fraTop1.Controls.Add(Me.cmdPoSearch)
        Me.fraTop1.Controls.Add(Me.cmdPopulate)
        Me.fraTop1.Controls.Add(Me.cmdAmendSchd)
        Me.fraTop1.Controls.Add(Me.txtPONo)
        Me.fraTop1.Controls.Add(Me.txtPODate)
        Me.fraTop1.Controls.Add(Me.txtPOAmendNo)
        Me.fraTop1.Controls.Add(Me.txtPOAmendDate)
        Me.fraTop1.Controls.Add(Me.cboStatus)
        Me.fraTop1.Controls.Add(Me.txtScheduleDate)
        Me.fraTop1.Controls.Add(Me.txtWEF)
        Me.fraTop1.Controls.Add(Me.chkStatus)
        Me.fraTop1.Controls.Add(Me.txtDSAmendDate)
        Me.fraTop1.Controls.Add(Me.txtDSAmendNo)
        Me.fraTop1.Controls.Add(Me.txtDSDate)
        Me.fraTop1.Controls.Add(Me.txtDSNo)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.txtSupplierName)
        Me.fraTop1.Controls.Add(Me.txtCode)
        Me.fraTop1.Controls.Add(Me.cmdsearch)
        Me.fraTop1.Controls.Add(Me.Label29)
        Me.fraTop1.Controls.Add(Me.Label44)
        Me.fraTop1.Controls.Add(Me.lblAddUser)
        Me.fraTop1.Controls.Add(Me.Label46)
        Me.fraTop1.Controls.Add(Me.lblModUser)
        Me.fraTop1.Controls.Add(Me.Label45)
        Me.fraTop1.Controls.Add(Me.lblAddDate)
        Me.fraTop1.Controls.Add(Me.Label48)
        Me.fraTop1.Controls.Add(Me.lblModDate)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Controls.Add(Me.Label15)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.Label13)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(2, 1)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(908, 225)
        Me.fraTop1.TabIndex = 27
        Me.fraTop1.TabStop = False
        '
        'txtDivision
        '
        Me.txtDivision.AcceptsReturn = True
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDivision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivision.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDivision.Location = New System.Drawing.Point(108, 103)
        Me.txtDivision.MaxLength = 0
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivision.Size = New System.Drawing.Size(89, 22)
        Me.txtDivision.TabIndex = 9
        '
        'txtSearchItem
        '
        Me.txtSearchItem.AcceptsReturn = True
        Me.txtSearchItem.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearchItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearchItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSearchItem.Location = New System.Drawing.Point(108, 193)
        Me.txtSearchItem.MaxLength = 0
        Me.txtSearchItem.Name = "txtSearchItem"
        Me.txtSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearchItem.Size = New System.Drawing.Size(91, 22)
        Me.txtSearchItem.TabIndex = 13
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(768, 102)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(121, 21)
        Me.cmdRefresh.TabIndex = 56
        Me.cmdRefresh.Text = "Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'cmdPopulate
        '
        Me.cmdPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulate.Location = New System.Drawing.Point(768, 147)
        Me.cmdPopulate.Name = "cmdPopulate"
        Me.cmdPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulate.Size = New System.Drawing.Size(121, 21)
        Me.cmdPopulate.TabIndex = 55
        Me.cmdPopulate.Text = "Populate"
        Me.cmdPopulate.UseVisualStyleBackColor = False
        '
        'cmdAmendSchd
        '
        Me.cmdAmendSchd.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmendSchd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmendSchd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmendSchd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmendSchd.Location = New System.Drawing.Point(768, 125)
        Me.cmdAmendSchd.Name = "cmdAmendSchd"
        Me.cmdAmendSchd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmendSchd.Size = New System.Drawing.Size(121, 21)
        Me.cmdAmendSchd.TabIndex = 54
        Me.cmdAmendSchd.Text = "Amend Schedule"
        Me.cmdAmendSchd.UseVisualStyleBackColor = False
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPONo.Location = New System.Drawing.Point(108, 73)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(89, 22)
        Me.txtPONo.TabIndex = 7
        '
        'txtPODate
        '
        Me.txtPODate.AcceptsReturn = True
        Me.txtPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPODate.Enabled = False
        Me.txtPODate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPODate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPODate.Location = New System.Drawing.Point(364, 73)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(75, 22)
        Me.txtPODate.TabIndex = 48
        '
        'txtPOAmendNo
        '
        Me.txtPOAmendNo.AcceptsReturn = True
        Me.txtPOAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPOAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPOAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPOAmendNo.Enabled = False
        Me.txtPOAmendNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPOAmendNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPOAmendNo.Location = New System.Drawing.Point(634, 73)
        Me.txtPOAmendNo.MaxLength = 0
        Me.txtPOAmendNo.Name = "txtPOAmendNo"
        Me.txtPOAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPOAmendNo.Size = New System.Drawing.Size(75, 22)
        Me.txtPOAmendNo.TabIndex = 47
        '
        'txtPOAmendDate
        '
        Me.txtPOAmendDate.AcceptsReturn = True
        Me.txtPOAmendDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPOAmendDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPOAmendDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPOAmendDate.Enabled = False
        Me.txtPOAmendDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPOAmendDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPOAmendDate.Location = New System.Drawing.Point(810, 73)
        Me.txtPOAmendDate.MaxLength = 0
        Me.txtPOAmendDate.Name = "txtPOAmendDate"
        Me.txtPOAmendDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPOAmendDate.Size = New System.Drawing.Size(75, 22)
        Me.txtPOAmendDate.TabIndex = 46
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(634, 133)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(75, 21)
        Me.cboStatus.TabIndex = 44
        '
        'txtScheduleDate
        '
        Me.txtScheduleDate.AcceptsReturn = True
        Me.txtScheduleDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtScheduleDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScheduleDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScheduleDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScheduleDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtScheduleDate.Location = New System.Drawing.Point(108, 133)
        Me.txtScheduleDate.MaxLength = 0
        Me.txtScheduleDate.Name = "txtScheduleDate"
        Me.txtScheduleDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScheduleDate.Size = New System.Drawing.Size(75, 22)
        Me.txtScheduleDate.TabIndex = 42
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Enabled = False
        Me.txtWEF.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtWEF.Location = New System.Drawing.Point(634, 103)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(75, 22)
        Me.txtWEF.TabIndex = 16
        '
        'chkStatus
        '
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(364, 108)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(135, 19)
        Me.chkStatus.TabIndex = 17
        Me.chkStatus.Text = "Posted/ Unposted"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'txtDSAmendDate
        '
        Me.txtDSAmendDate.AcceptsReturn = True
        Me.txtDSAmendDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDSAmendDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDSAmendDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDSAmendDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDSAmendDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDSAmendDate.Location = New System.Drawing.Point(810, 13)
        Me.txtDSAmendDate.MaxLength = 0
        Me.txtDSAmendDate.Name = "txtDSAmendDate"
        Me.txtDSAmendDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDSAmendDate.Size = New System.Drawing.Size(75, 22)
        Me.txtDSAmendDate.TabIndex = 4
        '
        'txtDSAmendNo
        '
        Me.txtDSAmendNo.AcceptsReturn = True
        Me.txtDSAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDSAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDSAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDSAmendNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDSAmendNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDSAmendNo.Location = New System.Drawing.Point(634, 13)
        Me.txtDSAmendNo.MaxLength = 0
        Me.txtDSAmendNo.Name = "txtDSAmendNo"
        Me.txtDSAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDSAmendNo.Size = New System.Drawing.Size(75, 22)
        Me.txtDSAmendNo.TabIndex = 3
        '
        'txtDSDate
        '
        Me.txtDSDate.AcceptsReturn = True
        Me.txtDSDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDSDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDSDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDSDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDSDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDSDate.Location = New System.Drawing.Point(364, 13)
        Me.txtDSDate.MaxLength = 0
        Me.txtDSDate.Name = "txtDSDate"
        Me.txtDSDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDSDate.Size = New System.Drawing.Size(75, 22)
        Me.txtDSDate.TabIndex = 2
        '
        'txtDSNo
        '
        Me.txtDSNo.AcceptsReturn = True
        Me.txtDSNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDSNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDSNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDSNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDSNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDSNo.Location = New System.Drawing.Point(108, 13)
        Me.txtDSNo.MaxLength = 0
        Me.txtDSNo.Name = "txtDSNo"
        Me.txtDSNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDSNo.Size = New System.Drawing.Size(93, 22)
        Me.txtDSNo.TabIndex = 1
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(108, 163)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(479, 22)
        Me.txtRemarks.TabIndex = 12
        '
        'txtSupplierName
        '
        Me.txtSupplierName.AcceptsReturn = True
        Me.txtSupplierName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplierName.Location = New System.Drawing.Point(108, 43)
        Me.txtSupplierName.MaxLength = 0
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierName.Size = New System.Drawing.Size(572, 22)
        Me.txtSupplierName.TabIndex = 5
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.Blue
        Me.txtCode.Location = New System.Drawing.Point(810, 43)
        Me.txtCode.MaxLength = 0
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.Size = New System.Drawing.Size(75, 22)
        Me.txtCode.TabIndex = 11
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(15, 107)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(90, 13)
        Me.Label29.TabIndex = 66
        Me.Label29.Text = "Search Division :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(258, 196)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 65
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(324, 194)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 64
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(584, 194)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 63
        Me.Label46.Text = "Mod User:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(650, 192)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 62
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(422, 196)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 61
        Me.Label45.Text = "Add Date :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(486, 194)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 60
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(748, 194)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 59
        Me.Label48.Text = "Mod Date:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(814, 192)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 58
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(33, 197)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(72, 13)
        Me.Label16.TabIndex = 57
        Me.Label16.Text = "Search Item :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(57, 77)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(48, 13)
        Me.Label15.TabIndex = 52
        Me.Label15.Text = "PO No. :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(303, 77)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(54, 13)
        Me.Label14.TabIndex = 51
        Me.Label14.Text = "PO Date :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(540, 76)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(90, 13)
        Me.Label13.TabIndex = 50
        Me.Label13.Text = "PO Amend. No. :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(769, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 49
        Me.Label5.Text = "Date :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(538, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Schedule Status :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(20, 136)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Schedule Date :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(313, 108)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(44, 13)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "Status :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(537, 108)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(93, 13)
        Me.Label11.TabIndex = 38
        Me.Label11.Text = "PO Amend w.e.f :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(769, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(37, 13)
        Me.Label10.TabIndex = 37
        Me.Label10.Text = "Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(534, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(96, 13)
        Me.Label9.TabIndex = 36
        Me.Label9.Text = "Amendment No. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(320, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(51, 17)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "Number :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(48, 167)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Remarks :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(50, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Supplier :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(766, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Code :"
        '
        'fraAccounts
        '
        Me.fraAccounts.BackColor = System.Drawing.SystemColors.Control
        Me.fraAccounts.Controls.Add(Me.SprdMain)
        Me.fraAccounts.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAccounts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAccounts.Location = New System.Drawing.Point(-2, 220)
        Me.fraAccounts.Name = "fraAccounts"
        Me.fraAccounts.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAccounts.Size = New System.Drawing.Size(910, 356)
        Me.fraAccounts.TabIndex = 30
        Me.fraAccounts.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(4, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(904, 344)
        Me.SprdMain.TabIndex = 15
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 28
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.lblDetail)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 568)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(910, 54)
        Me.FraMovement.TabIndex = 31
        Me.FraMovement.TabStop = False
        '
        'lblDetail
        '
        Me.lblDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDetail.Location = New System.Drawing.Point(14, 32)
        Me.lblDetail.Name = "lblDetail"
        Me.lblDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDetail.Size = New System.Drawing.Size(35, 13)
        Me.lblDetail.TabIndex = 53
        Me.lblDetail.Text = "False"
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(684, 16)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(47, 21)
        Me.lblBookType.TabIndex = 41
        Me.lblBookType.Text = "lblBookType"
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(10, 18)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 40
        Me.lblMkey.Text = "lblMkey"
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
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(910, 568)
        Me.UltraGrid1.TabIndex = 80
        '
        'frmDS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraTrn)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDS"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase Delivery Schedule"
        Me.FraTrn.ResumeLayout(False)
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.fraAccounts.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(ADataGrid, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub

    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
#End Region
End Class