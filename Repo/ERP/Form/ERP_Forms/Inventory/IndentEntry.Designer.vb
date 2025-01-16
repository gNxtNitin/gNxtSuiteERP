Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmIndentEntry
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
    Public WithEvents cmdCCSearch As System.Windows.Forms.Button
    Public WithEvents txtCCentre As System.Windows.Forms.TextBox
    Public WithEvents TxtBillTm As System.Windows.Forms.TextBox
    Public WithEvents cmdDeptSearch As System.Windows.Forms.Button
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents txtIndentBy As System.Windows.Forms.TextBox
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents txtIndentNo As System.Windows.Forms.TextBox
    Public WithEvents txtIndentDate As System.Windows.Forms.TextBox
    Public WithEvents lblDivision As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblCCentre As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblIndentBy As System.Windows.Forms.Label
    Public WithEvents lblDept As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraTop As System.Windows.Forms.GroupBox
    Public WithEvents chkAutoIssueToSS As System.Windows.Forms.CheckBox
    Public WithEvents chkAutoIssue As System.Windows.Forms.CheckBox
    Public WithEvents txtHOD As System.Windows.Forms.TextBox
    Public WithEvents txtHODAppDate As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lblHOD As System.Windows.Forms.Label
    Public WithEvents FraHODApp As System.Windows.Forms.GroupBox
    Public WithEvents txtStatus As System.Windows.Forms.TextBox
    Public WithEvents txtFinalAppDate As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents chkSendBack As System.Windows.Forms.CheckBox
    Public WithEvents chkApproval As System.Windows.Forms.CheckBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblAppBy As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents FraApproved As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents Frabot As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents FraCmd As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmIndentEntry))
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
        Me.cmdCCSearch = New System.Windows.Forms.Button()
        Me.cmdDeptSearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.cmdSearchProduct = New System.Windows.Forms.Button()
        Me.cmdSearchSONo = New System.Windows.Forms.Button()
        Me.Frabot = New System.Windows.Forms.GroupBox()
        Me.FraTop = New System.Windows.Forms.GroupBox()
        Me.cmdReOrderLevel = New System.Windows.Forms.Button()
        Me.fraOrder = New System.Windows.Forms.GroupBox()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.txtSONo = New System.Windows.Forms.TextBox()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtPlanQty = New System.Windows.Forms.TextBox()
        Me.cmdGetData = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtRequestBy = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtDivision = New System.Windows.Forms.TextBox()
        Me.txtCCentre = New System.Windows.Forms.TextBox()
        Me.TxtBillTm = New System.Windows.Forms.TextBox()
        Me.chkCancelled = New System.Windows.Forms.CheckBox()
        Me.txtIndentBy = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.txtIndentNo = New System.Windows.Forms.TextBox()
        Me.txtIndentDate = New System.Windows.Forms.TextBox()
        Me.lblDivision = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblCCentre = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblIndentBy = New System.Windows.Forms.Label()
        Me.lblDept = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraHODApp = New System.Windows.Forms.GroupBox()
        Me.chkHODApproval = New System.Windows.Forms.CheckBox()
        Me.chkAutoIssueToSS = New System.Windows.Forms.CheckBox()
        Me.chkAutoIssue = New System.Windows.Forms.CheckBox()
        Me.txtHOD = New System.Windows.Forms.TextBox()
        Me.txtHODAppDate = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblHOD = New System.Windows.Forms.Label()
        Me.FraApproved = New System.Windows.Forms.GroupBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtFinalAppDate = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.chkSendBack = New System.Windows.Forms.CheckBox()
        Me.chkApproval = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblAppBy = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraCmd = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Frabot.SuspendLayout()
        Me.FraTop.SuspendLayout()
        Me.fraOrder.SuspendLayout()
        Me.FraHODApp.SuspendLayout()
        Me.FraApproved.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraCmd.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.cmdDivSearch.Location = New System.Drawing.Point(168, 63)
        Me.cmdDivSearch.Name = "cmdDivSearch"
        Me.cmdDivSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDivSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdDivSearch.TabIndex = 7
        Me.cmdDivSearch.TabStop = False
        Me.cmdDivSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDivSearch, "Search")
        Me.cmdDivSearch.UseVisualStyleBackColor = False
        '
        'cmdCCSearch
        '
        Me.cmdCCSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdCCSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCCSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCCSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCCSearch.Image = CType(resources.GetObject("cmdCCSearch.Image"), System.Drawing.Image)
        Me.cmdCCSearch.Location = New System.Drawing.Point(671, 63)
        Me.cmdCCSearch.Name = "cmdCCSearch"
        Me.cmdCCSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCCSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdCCSearch.TabIndex = 10
        Me.cmdCCSearch.TabStop = False
        Me.cmdCCSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCCSearch, "Search")
        Me.cmdCCSearch.UseVisualStyleBackColor = False
        '
        'cmdDeptSearch
        '
        Me.cmdDeptSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDeptSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeptSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeptSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeptSearch.Image = CType(resources.GetObject("cmdDeptSearch.Image"), System.Drawing.Image)
        Me.cmdDeptSearch.Location = New System.Drawing.Point(168, 38)
        Me.cmdDeptSearch.Name = "cmdDeptSearch"
        Me.cmdDeptSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeptSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdDeptSearch.TabIndex = 4
        Me.cmdDeptSearch.TabStop = False
        Me.cmdDeptSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDeptSearch, "Search")
        Me.cmdDeptSearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(667, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 31
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(601, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 30
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(535, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 29
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(469, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 28
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(403, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 27
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(337, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 26
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(271, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 25
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(205, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 24
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
        Me.CmdAdd.Location = New System.Drawing.Point(139, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'cmdSearchProduct
        '
        Me.cmdSearchProduct.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProduct.Enabled = False
        Me.cmdSearchProduct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProduct.Image = CType(resources.GetObject("cmdSearchProduct.Image"), System.Drawing.Image)
        Me.cmdSearchProduct.Location = New System.Drawing.Point(183, 34)
        Me.cmdSearchProduct.Name = "cmdSearchProduct"
        Me.cmdSearchProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProduct.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchProduct.TabIndex = 61
        Me.cmdSearchProduct.TabStop = False
        Me.cmdSearchProduct.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProduct, "Search")
        Me.cmdSearchProduct.UseVisualStyleBackColor = False
        '
        'cmdSearchSONo
        '
        Me.cmdSearchSONo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSONo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSONo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSONo.Image = CType(resources.GetObject("cmdSearchSONo.Image"), System.Drawing.Image)
        Me.cmdSearchSONo.Location = New System.Drawing.Point(183, 10)
        Me.cmdSearchSONo.Name = "cmdSearchSONo"
        Me.cmdSearchSONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSONo.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchSONo.TabIndex = 68
        Me.cmdSearchSONo.TabStop = False
        Me.cmdSearchSONo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSONo, "Search")
        Me.cmdSearchSONo.UseVisualStyleBackColor = False
        '
        'Frabot
        '
        Me.Frabot.BackColor = System.Drawing.SystemColors.Control
        Me.Frabot.Controls.Add(Me.FraTop)
        Me.Frabot.Controls.Add(Me.FraHODApp)
        Me.Frabot.Controls.Add(Me.FraApproved)
        Me.Frabot.Controls.Add(Me.Frame2)
        Me.Frabot.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frabot.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frabot.Location = New System.Drawing.Point(0, -6)
        Me.Frabot.Name = "Frabot"
        Me.Frabot.Padding = New System.Windows.Forms.Padding(0)
        Me.Frabot.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frabot.Size = New System.Drawing.Size(905, 572)
        Me.Frabot.TabIndex = 34
        Me.Frabot.TabStop = False
        '
        'FraTop
        '
        Me.FraTop.BackColor = System.Drawing.SystemColors.Control
        Me.FraTop.Controls.Add(Me.cmdReOrderLevel)
        Me.FraTop.Controls.Add(Me.fraOrder)
        Me.FraTop.Controls.Add(Me.txtRequestBy)
        Me.FraTop.Controls.Add(Me.Label12)
        Me.FraTop.Controls.Add(Me.cmdDivSearch)
        Me.FraTop.Controls.Add(Me.txtDivision)
        Me.FraTop.Controls.Add(Me.cmdCCSearch)
        Me.FraTop.Controls.Add(Me.txtCCentre)
        Me.FraTop.Controls.Add(Me.TxtBillTm)
        Me.FraTop.Controls.Add(Me.cmdDeptSearch)
        Me.FraTop.Controls.Add(Me.chkCancelled)
        Me.FraTop.Controls.Add(Me.txtIndentBy)
        Me.FraTop.Controls.Add(Me.txtDept)
        Me.FraTop.Controls.Add(Me.txtIndentNo)
        Me.FraTop.Controls.Add(Me.txtIndentDate)
        Me.FraTop.Controls.Add(Me.lblDivision)
        Me.FraTop.Controls.Add(Me.Label13)
        Me.FraTop.Controls.Add(Me.lblCCentre)
        Me.FraTop.Controls.Add(Me.Label15)
        Me.FraTop.Controls.Add(Me.Label10)
        Me.FraTop.Controls.Add(Me.lblIndentBy)
        Me.FraTop.Controls.Add(Me.lblDept)
        Me.FraTop.Controls.Add(Me.lblBookType)
        Me.FraTop.Controls.Add(Me.Label4)
        Me.FraTop.Controls.Add(Me.Label3)
        Me.FraTop.Controls.Add(Me.Label1)
        Me.FraTop.Controls.Add(Me.Label2)
        Me.FraTop.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTop.Location = New System.Drawing.Point(0, 2)
        Me.FraTop.Name = "FraTop"
        Me.FraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTop.Size = New System.Drawing.Size(910, 169)
        Me.FraTop.TabIndex = 36
        Me.FraTop.TabStop = False
        '
        'cmdReOrderLevel
        '
        Me.cmdReOrderLevel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdReOrderLevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReOrderLevel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReOrderLevel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReOrderLevel.Location = New System.Drawing.Point(809, 116)
        Me.cmdReOrderLevel.Name = "cmdReOrderLevel"
        Me.cmdReOrderLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReOrderLevel.Size = New System.Drawing.Size(86, 46)
        Me.cmdReOrderLevel.TabIndex = 72
        Me.cmdReOrderLevel.Text = "Populate Data - Reorder Level"
        Me.cmdReOrderLevel.UseVisualStyleBackColor = False
        '
        'fraOrder
        '
        Me.fraOrder.Controls.Add(Me.lblCustomerName)
        Me.fraOrder.Controls.Add(Me.cmdSearchSONo)
        Me.fraOrder.Controls.Add(Me._lblLabels_0)
        Me.fraOrder.Controls.Add(Me.txtSONo)
        Me.fraOrder.Controls.Add(Me.lblProductName)
        Me.fraOrder.Controls.Add(Me.txtProductCode)
        Me.fraOrder.Controls.Add(Me.Label17)
        Me.fraOrder.Controls.Add(Me.cmdSearchProduct)
        Me.fraOrder.Controls.Add(Me.txtPlanQty)
        Me.fraOrder.Controls.Add(Me.cmdGetData)
        Me.fraOrder.Controls.Add(Me.Label14)
        Me.fraOrder.Location = New System.Drawing.Point(3, 110)
        Me.fraOrder.Name = "fraOrder"
        Me.fraOrder.Size = New System.Drawing.Size(800, 59)
        Me.fraOrder.TabIndex = 71
        Me.fraOrder.TabStop = False
        '
        'lblCustomerName
        '
        Me.lblCustomerName.BackColor = System.Drawing.SystemColors.Control
        Me.lblCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCustomerName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCustomerName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCustomerName.Location = New System.Drawing.Point(211, 10)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustomerName.Size = New System.Drawing.Size(327, 21)
        Me.lblCustomerName.TabIndex = 70
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(4, 37)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(78, 14)
        Me._lblLabels_0.TabIndex = 62
        Me._lblLabels_0.Text = "Product Code :"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtSONo
        '
        Me.txtSONo.AcceptsReturn = True
        Me.txtSONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSONo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSONo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSONo.Location = New System.Drawing.Point(83, 10)
        Me.txtSONo.MaxLength = 0
        Me.txtSONo.Name = "txtSONo"
        Me.txtSONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSONo.Size = New System.Drawing.Size(99, 20)
        Me.txtSONo.TabIndex = 67
        '
        'lblProductName
        '
        Me.lblProductName.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProductName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductName.Location = New System.Drawing.Point(211, 34)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductName.Size = New System.Drawing.Size(327, 21)
        Me.lblProductName.TabIndex = 63
        '
        'txtProductCode
        '
        Me.txtProductCode.AcceptsReturn = True
        Me.txtProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductCode.Enabled = False
        Me.txtProductCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!)
        Me.txtProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductCode.Location = New System.Drawing.Point(83, 34)
        Me.txtProductCode.MaxLength = 0
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.Size = New System.Drawing.Size(99, 22)
        Me.txtProductCode.TabIndex = 60
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(25, 13)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(57, 14)
        Me.Label17.TabIndex = 69
        Me.Label17.Text = "Order No :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPlanQty
        '
        Me.txtPlanQty.AcceptsReturn = True
        Me.txtPlanQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlanQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlanQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlanQty.Enabled = False
        Me.txtPlanQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlanQty.ForeColor = System.Drawing.Color.Blue
        Me.txtPlanQty.Location = New System.Drawing.Point(613, 32)
        Me.txtPlanQty.MaxLength = 0
        Me.txtPlanQty.Name = "txtPlanQty"
        Me.txtPlanQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlanQty.Size = New System.Drawing.Size(91, 22)
        Me.txtPlanQty.TabIndex = 66
        '
        'cmdGetData
        '
        Me.cmdGetData.BackColor = System.Drawing.SystemColors.Control
        Me.cmdGetData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetData.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetData.Location = New System.Drawing.Point(708, 31)
        Me.cmdGetData.Name = "cmdGetData"
        Me.cmdGetData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetData.Size = New System.Drawing.Size(86, 23)
        Me.cmdGetData.TabIndex = 64
        Me.cmdGetData.Text = "Get Data"
        Me.cmdGetData.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(543, 35)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(63, 13)
        Me.Label14.TabIndex = 65
        Me.Label14.Text = "Order Qty :"
        '
        'txtRequestBy
        '
        Me.txtRequestBy.AcceptsReturn = True
        Me.txtRequestBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtRequestBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRequestBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRequestBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRequestBy.Location = New System.Drawing.Point(589, 88)
        Me.txtRequestBy.MaxLength = 0
        Me.txtRequestBy.Name = "txtRequestBy"
        Me.txtRequestBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRequestBy.Size = New System.Drawing.Size(307, 22)
        Me.txtRequestBy.TabIndex = 14
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(518, 91)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(70, 13)
        Me.Label12.TabIndex = 59
        Me.Label12.Text = "Request By :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDivision
        '
        Me.txtDivision.AcceptsReturn = True
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDivision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivision.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDivision.Location = New System.Drawing.Point(86, 63)
        Me.txtDivision.MaxLength = 0
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivision.Size = New System.Drawing.Size(79, 22)
        Me.txtDivision.TabIndex = 6
        '
        'txtCCentre
        '
        Me.txtCCentre.AcceptsReturn = True
        Me.txtCCentre.BackColor = System.Drawing.SystemColors.Window
        Me.txtCCentre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCCentre.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCCentre.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCCentre.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCCentre.Location = New System.Drawing.Point(589, 63)
        Me.txtCCentre.MaxLength = 0
        Me.txtCCentre.Name = "txtCCentre"
        Me.txtCCentre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCCentre.Size = New System.Drawing.Size(79, 22)
        Me.txtCCentre.TabIndex = 9
        '
        'TxtBillTm
        '
        Me.TxtBillTm.AcceptsReturn = True
        Me.TxtBillTm.BackColor = System.Drawing.SystemColors.Window
        Me.TxtBillTm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBillTm.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtBillTm.Enabled = False
        Me.TxtBillTm.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillTm.ForeColor = System.Drawing.Color.Blue
        Me.TxtBillTm.Location = New System.Drawing.Point(610, 13)
        Me.TxtBillTm.MaxLength = 0
        Me.TxtBillTm.Name = "TxtBillTm"
        Me.TxtBillTm.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBillTm.Size = New System.Drawing.Size(31, 22)
        Me.TxtBillTm.TabIndex = 44
        '
        'chkCancelled
        '
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.Enabled = False
        Me.chkCancelled.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCancelled.Location = New System.Drawing.Point(745, 17)
        Me.chkCancelled.Name = "chkCancelled"
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Size = New System.Drawing.Size(158, 16)
        Me.chkCancelled.TabIndex = 41
        Me.chkCancelled.Text = "Indent Cancelled (Yes/No)"
        Me.chkCancelled.UseVisualStyleBackColor = False
        '
        'txtIndentBy
        '
        Me.txtIndentBy.AcceptsReturn = True
        Me.txtIndentBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtIndentBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIndentBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIndentBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndentBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIndentBy.Location = New System.Drawing.Point(86, 88)
        Me.txtIndentBy.MaxLength = 0
        Me.txtIndentBy.Name = "txtIndentBy"
        Me.txtIndentBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIndentBy.Size = New System.Drawing.Size(108, 22)
        Me.txtIndentBy.TabIndex = 12
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDept.Location = New System.Drawing.Point(86, 38)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(79, 22)
        Me.txtDept.TabIndex = 3
        '
        'txtIndentNo
        '
        Me.txtIndentNo.AcceptsReturn = True
        Me.txtIndentNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtIndentNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIndentNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIndentNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndentNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIndentNo.Location = New System.Drawing.Point(86, 13)
        Me.txtIndentNo.MaxLength = 0
        Me.txtIndentNo.Name = "txtIndentNo"
        Me.txtIndentNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIndentNo.Size = New System.Drawing.Size(79, 22)
        Me.txtIndentNo.TabIndex = 1
        '
        'txtIndentDate
        '
        Me.txtIndentDate.AcceptsReturn = True
        Me.txtIndentDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtIndentDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIndentDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIndentDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndentDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIndentDate.Location = New System.Drawing.Point(262, 13)
        Me.txtIndentDate.MaxLength = 0
        Me.txtIndentDate.Name = "txtIndentDate"
        Me.txtIndentDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIndentDate.Size = New System.Drawing.Size(75, 22)
        Me.txtIndentDate.TabIndex = 2
        '
        'lblDivision
        '
        Me.lblDivision.BackColor = System.Drawing.Color.Transparent
        Me.lblDivision.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivision.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivision.Location = New System.Drawing.Point(198, 63)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDivision.Size = New System.Drawing.Size(310, 22)
        Me.lblDivision.TabIndex = 8
        Me.lblDivision.Text = "lblDivision"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(31, 66)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(54, 13)
        Me.Label13.TabIndex = 57
        Me.Label13.Text = "Division :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCCentre
        '
        Me.lblCCentre.BackColor = System.Drawing.Color.Transparent
        Me.lblCCentre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCCentre.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCCentre.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCCentre.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCCentre.Location = New System.Drawing.Point(701, 63)
        Me.lblCCentre.Name = "lblCCentre"
        Me.lblCCentre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCCentre.Size = New System.Drawing.Size(194, 22)
        Me.lblCCentre.TabIndex = 11
        Me.lblCCentre.Text = "lblCCentre"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(516, 67)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(72, 13)
        Me.Label15.TabIndex = 56
        Me.Label15.Text = "Cost Centre :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(528, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(80, 13)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "Prepare Time :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblIndentBy
        '
        Me.lblIndentBy.BackColor = System.Drawing.Color.Transparent
        Me.lblIndentBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIndentBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIndentBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIndentBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIndentBy.Location = New System.Drawing.Point(198, 88)
        Me.lblIndentBy.Name = "lblIndentBy"
        Me.lblIndentBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIndentBy.Size = New System.Drawing.Size(310, 22)
        Me.lblIndentBy.TabIndex = 13
        Me.lblIndentBy.Text = "lblIndentBy"
        '
        'lblDept
        '
        Me.lblDept.BackColor = System.Drawing.Color.Transparent
        Me.lblDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDept.Location = New System.Drawing.Point(198, 38)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDept.Size = New System.Drawing.Size(578, 22)
        Me.lblDept.TabIndex = 5
        Me.lblDept.Text = "lblDept"
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(664, 13)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(57, 15)
        Me.lblBookType.TabIndex = 43
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(11, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Indented By :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(11, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Department :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(19, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Indent No. :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(221, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraHODApp
        '
        Me.FraHODApp.BackColor = System.Drawing.SystemColors.Control
        Me.FraHODApp.Controls.Add(Me.chkHODApproval)
        Me.FraHODApp.Controls.Add(Me.chkAutoIssueToSS)
        Me.FraHODApp.Controls.Add(Me.chkAutoIssue)
        Me.FraHODApp.Controls.Add(Me.txtHOD)
        Me.FraHODApp.Controls.Add(Me.txtHODAppDate)
        Me.FraHODApp.Controls.Add(Me.Label5)
        Me.FraHODApp.Controls.Add(Me.Label8)
        Me.FraHODApp.Controls.Add(Me.lblHOD)
        Me.FraHODApp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraHODApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraHODApp.Location = New System.Drawing.Point(0, 172)
        Me.FraHODApp.Name = "FraHODApp"
        Me.FraHODApp.Padding = New System.Windows.Forms.Padding(0)
        Me.FraHODApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraHODApp.Size = New System.Drawing.Size(908, 47)
        Me.FraHODApp.TabIndex = 52
        Me.FraHODApp.TabStop = False
        Me.FraHODApp.Text = "H.O.D. Approved"
        '
        'chkHODApproval
        '
        Me.chkHODApproval.BackColor = System.Drawing.SystemColors.Control
        Me.chkHODApproval.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHODApproval.Enabled = False
        Me.chkHODApproval.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHODApproval.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHODApproval.Location = New System.Drawing.Point(744, 20)
        Me.chkHODApproval.Name = "chkHODApproval"
        Me.chkHODApproval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHODApproval.Size = New System.Drawing.Size(156, 17)
        Me.chkHODApproval.TabIndex = 56
        Me.chkHODApproval.Text = "HOD Approved (Yes/No)"
        Me.chkHODApproval.UseVisualStyleBackColor = False
        '
        'chkAutoIssueToSS
        '
        Me.chkAutoIssueToSS.BackColor = System.Drawing.SystemColors.Control
        Me.chkAutoIssueToSS.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoIssueToSS.Enabled = False
        Me.chkAutoIssueToSS.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoIssueToSS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoIssueToSS.Location = New System.Drawing.Point(524, 9)
        Me.chkAutoIssueToSS.Name = "chkAutoIssueToSS"
        Me.chkAutoIssueToSS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoIssueToSS.Size = New System.Drawing.Size(219, 20)
        Me.chkAutoIssueToSS.TabIndex = 17
        Me.chkAutoIssueToSS.Text = "Auto Issue to Sub Store (Yes/No)"
        Me.chkAutoIssueToSS.UseVisualStyleBackColor = False
        Me.chkAutoIssueToSS.Visible = False
        '
        'chkAutoIssue
        '
        Me.chkAutoIssue.BackColor = System.Drawing.SystemColors.Control
        Me.chkAutoIssue.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoIssue.Enabled = False
        Me.chkAutoIssue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoIssue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoIssue.Location = New System.Drawing.Point(524, 26)
        Me.chkAutoIssue.Name = "chkAutoIssue"
        Me.chkAutoIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoIssue.Size = New System.Drawing.Size(156, 20)
        Me.chkAutoIssue.TabIndex = 16
        Me.chkAutoIssue.Text = "Auto Issue (Yes/No)"
        Me.chkAutoIssue.UseVisualStyleBackColor = False
        Me.chkAutoIssue.Visible = False
        '
        'txtHOD
        '
        Me.txtHOD.AcceptsReturn = True
        Me.txtHOD.BackColor = System.Drawing.SystemColors.Window
        Me.txtHOD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHOD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHOD.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHOD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtHOD.Location = New System.Drawing.Point(86, 16)
        Me.txtHOD.MaxLength = 0
        Me.txtHOD.Name = "txtHOD"
        Me.txtHOD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHOD.Size = New System.Drawing.Size(103, 22)
        Me.txtHOD.TabIndex = 14
        '
        'txtHODAppDate
        '
        Me.txtHODAppDate.AcceptsReturn = True
        Me.txtHODAppDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtHODAppDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHODAppDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHODAppDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHODAppDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtHODAppDate.Location = New System.Drawing.Point(454, 16)
        Me.txtHODAppDate.MaxLength = 0
        Me.txtHODAppDate.Name = "txtHODAppDate"
        Me.txtHODAppDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHODAppDate.Size = New System.Drawing.Size(63, 22)
        Me.txtHODAppDate.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(40, 19)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "H.O.D. :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(375, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(64, 13)
        Me.Label8.TabIndex = 54
        Me.Label8.Text = "App. Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblHOD
        '
        Me.lblHOD.BackColor = System.Drawing.Color.Transparent
        Me.lblHOD.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHOD.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHOD.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHOD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHOD.Location = New System.Drawing.Point(190, 16)
        Me.lblHOD.Name = "lblHOD"
        Me.lblHOD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHOD.Size = New System.Drawing.Size(179, 22)
        Me.lblHOD.TabIndex = 53
        Me.lblHOD.Text = "lblHOD"
        '
        'FraApproved
        '
        Me.FraApproved.BackColor = System.Drawing.SystemColors.Control
        Me.FraApproved.Controls.Add(Me.txtStatus)
        Me.FraApproved.Controls.Add(Me.txtFinalAppDate)
        Me.FraApproved.Controls.Add(Me.txtRemarks)
        Me.FraApproved.Controls.Add(Me.chkSendBack)
        Me.FraApproved.Controls.Add(Me.chkApproval)
        Me.FraApproved.Controls.Add(Me.Label6)
        Me.FraApproved.Controls.Add(Me.Label7)
        Me.FraApproved.Controls.Add(Me.Label9)
        Me.FraApproved.Controls.Add(Me.lblAppBy)
        Me.FraApproved.Controls.Add(Me.Label11)
        Me.FraApproved.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraApproved.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraApproved.Location = New System.Drawing.Point(0, 221)
        Me.FraApproved.Name = "FraApproved"
        Me.FraApproved.Padding = New System.Windows.Forms.Padding(0)
        Me.FraApproved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraApproved.Size = New System.Drawing.Size(908, 62)
        Me.FraApproved.TabIndex = 46
        Me.FraApproved.TabStop = False
        Me.FraApproved.Text = "Indent Approval"
        '
        'txtStatus
        '
        Me.txtStatus.AcceptsReturn = True
        Me.txtStatus.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStatus.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtStatus.Location = New System.Drawing.Point(401, 13)
        Me.txtStatus.MaxLength = 0
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatus.Size = New System.Drawing.Size(173, 22)
        Me.txtStatus.TabIndex = 18
        '
        'txtFinalAppDate
        '
        Me.txtFinalAppDate.AcceptsReturn = True
        Me.txtFinalAppDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtFinalAppDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFinalAppDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFinalAppDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFinalAppDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFinalAppDate.Location = New System.Drawing.Point(261, 13)
        Me.txtFinalAppDate.MaxLength = 0
        Me.txtFinalAppDate.Name = "txtFinalAppDate"
        Me.txtFinalAppDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFinalAppDate.Size = New System.Drawing.Size(75, 22)
        Me.txtFinalAppDate.TabIndex = 19
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(84, 39)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(491, 21)
        Me.txtRemarks.TabIndex = 20
        '
        'chkSendBack
        '
        Me.chkSendBack.BackColor = System.Drawing.SystemColors.Control
        Me.chkSendBack.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSendBack.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSendBack.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSendBack.Location = New System.Drawing.Point(744, 34)
        Me.chkSendBack.Name = "chkSendBack"
        Me.chkSendBack.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSendBack.Size = New System.Drawing.Size(156, 17)
        Me.chkSendBack.TabIndex = 22
        Me.chkSendBack.Text = "Send Back (Yes/No)"
        Me.chkSendBack.UseVisualStyleBackColor = False
        '
        'chkApproval
        '
        Me.chkApproval.BackColor = System.Drawing.SystemColors.Control
        Me.chkApproval.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkApproval.Enabled = False
        Me.chkApproval.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApproval.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkApproval.Location = New System.Drawing.Point(744, 13)
        Me.chkApproval.Name = "chkApproval"
        Me.chkApproval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkApproval.Size = New System.Drawing.Size(156, 17)
        Me.chkApproval.TabIndex = 21
        Me.chkApproval.Text = "Approved (Yes/No)"
        Me.chkApproval.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(354, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(44, 13)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Status :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(192, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(64, 13)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "App. Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(4, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(80, 13)
        Me.Label9.TabIndex = 49
        Me.Label9.Text = "Approved By :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAppBy
        '
        Me.lblAppBy.BackColor = System.Drawing.Color.Transparent
        Me.lblAppBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAppBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAppBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAppBy.Location = New System.Drawing.Point(84, 13)
        Me.lblAppBy.Name = "lblAppBy"
        Me.lblAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAppBy.Size = New System.Drawing.Size(81, 22)
        Me.lblAppBy.TabIndex = 48
        Me.lblAppBy.Text = "lblAppBy"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(27, 41)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(57, 13)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "Remarks :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdMain)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 276)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(908, 292)
        Me.Frame2.TabIndex = 35
        Me.Frame2.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 15)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(908, 277)
        Me.SprdMain.TabIndex = 23
        '
        'FraCmd
        '
        Me.FraCmd.BackColor = System.Drawing.SystemColors.Control
        Me.FraCmd.Controls.Add(Me.CmdClose)
        Me.FraCmd.Controls.Add(Me.CmdView)
        Me.FraCmd.Controls.Add(Me.CmdPreview)
        Me.FraCmd.Controls.Add(Me.cmdPrint)
        Me.FraCmd.Controls.Add(Me.CmdDelete)
        Me.FraCmd.Controls.Add(Me.cmdSavePrint)
        Me.FraCmd.Controls.Add(Me.CmdSave)
        Me.FraCmd.Controls.Add(Me.CmdModify)
        Me.FraCmd.Controls.Add(Me.CmdAdd)
        Me.FraCmd.Controls.Add(Me.Report1)
        Me.FraCmd.Controls.Add(Me.LblMKey)
        Me.FraCmd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCmd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraCmd.Location = New System.Drawing.Point(0, 565)
        Me.FraCmd.Name = "FraCmd"
        Me.FraCmd.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCmd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCmd.Size = New System.Drawing.Size(908, 55)
        Me.FraCmd.TabIndex = 32
        Me.FraCmd.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(22, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 32
        '
        'LblMKey
        '
        Me.LblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMKey.Location = New System.Drawing.Point(56, 20)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(25, 17)
        Me.LblMKey.TabIndex = 42
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
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
        Me.UltraGrid1.Size = New System.Drawing.Size(906, 566)
        Me.UltraGrid1.TabIndex = 78
        '
        'FrmIndentEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.Frabot)
        Me.Controls.Add(Me.FraCmd)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmIndentEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Indent Entry"
        Me.Frabot.ResumeLayout(False)
        Me.FraTop.ResumeLayout(False)
        Me.FraTop.PerformLayout()
        Me.fraOrder.ResumeLayout(False)
        Me.fraOrder.PerformLayout()
        Me.FraHODApp.ResumeLayout(False)
        Me.FraHODApp.PerformLayout()
        Me.FraApproved.ResumeLayout(False)
        Me.FraApproved.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraCmd.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(ADataPPOMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub
    Public WithEvents chkHODApproval As System.Windows.Forms.CheckBox
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Public WithEvents txtRequestBy As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents cmdSearchProduct As Button
    Public WithEvents txtProductCode As TextBox
    Public WithEvents lblProductName As Label
    Public WithEvents _lblLabels_0 As Label
    Public WithEvents cmdGetData As Button
    Public WithEvents txtPlanQty As TextBox
    Public WithEvents Label14 As Label
    Public WithEvents cmdSearchSONo As Button
    Public WithEvents txtSONo As TextBox
    Public WithEvents Label17 As Label
    Friend WithEvents fraOrder As GroupBox
    Public WithEvents lblCustomerName As Label
    Public WithEvents cmdReOrderLevel As Button
#End Region
End Class