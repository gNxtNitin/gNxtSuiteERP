Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmPMemoDiaCuttingPlan
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
        'Me.MDIParent = Production.Master

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
    Public WithEvents txtNetScrapWt As System.Windows.Forms.TextBox
    Public WithEvents txtStockQty As System.Windows.Forms.TextBox
    Public WithEvents txtCTLWt As System.Windows.Forms.TextBox
    Public WithEvents txtNetRMWt As System.Windows.Forms.TextBox
    Public WithEvents txtGrossRMWt As System.Windows.Forms.TextBox
    Public WithEvents txtThickness As System.Windows.Forms.TextBox
    Public WithEvents txtRMCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchRMCode As System.Windows.Forms.Button
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdMainScrap As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblDensity As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents _lblUnder_42 As System.Windows.Forms.Label
    Public WithEvents lblRMUOM As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblRMCode As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtProdDate As System.Windows.Forms.TextBox
    Public WithEvents txtEntryDate As System.Windows.Forms.TextBox
    Public WithEvents txtRefTM As System.Windows.Forms.TextBox
    Public WithEvents cboType As System.Windows.Forms.ComboBox
    Public WithEvents cmdSearchEmp As System.Windows.Forms.Button
    Public WithEvents CmdSearchDept As System.Windows.Forms.Button
    Public WithEvents txtPMemoNo As System.Windows.Forms.TextBox
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtEmp As System.Windows.Forms.TextBox
    Public WithEvents cboShiftcd As System.Windows.Forms.ComboBox
    Public WithEvents txtPMemoDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblEmp As System.Windows.Forms.Label
    Public WithEvents lblDept As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents AdoDCMain As VB6.ADODC
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents lblUnder As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPMemoDiaCuttingPlan))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchRMCode = New System.Windows.Forms.Button()
        Me.cmdSearchEmp = New System.Windows.Forms.Button()
        Me.CmdSearchDept = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdSearchHeatNo = New System.Windows.Forms.Button()
        Me.cmdPopulateBOM = New System.Windows.Forms.Button()
        Me.cmdSearchBOM = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.cboProductionFrom = New System.Windows.Forms.ComboBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtSearchBOM = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtHeatNo = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtNetScrapWt = New System.Windows.Forms.TextBox()
        Me.txtStockQty = New System.Windows.Forms.TextBox()
        Me.txtCTLWt = New System.Windows.Forms.TextBox()
        Me.txtNetRMWt = New System.Windows.Forms.TextBox()
        Me.txtGrossRMWt = New System.Windows.Forms.TextBox()
        Me.txtThickness = New System.Windows.Forms.TextBox()
        Me.txtRMCode = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdMainScrap = New AxFPSpreadADO.AxfpSpread()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblDensity = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me._lblUnder_42 = New System.Windows.Forms.Label()
        Me.lblRMUOM = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblRMCode = New System.Windows.Forms.Label()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtProdDate = New System.Windows.Forms.TextBox()
        Me.txtEntryDate = New System.Windows.Forms.TextBox()
        Me.txtRefTM = New System.Windows.Forms.TextBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.txtPMemoNo = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.cboShiftcd = New System.Windows.Forms.ComboBox()
        Me.txtPMemoDate = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblEmp = New System.Windows.Forms.Label()
        Me.lblDept = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.AdoDCMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.lblUnder = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.txtMachineNo = New System.Windows.Forms.TextBox()
        Me.cmdSearchMacNo = New System.Windows.Forms.Button()
        Me.lblMac = New System.Windows.Forms.Label()
        Me.lblMachineNo = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMainScrap, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblUnder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchRMCode
        '
        Me.cmdSearchRMCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchRMCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchRMCode.Enabled = False
        Me.cmdSearchRMCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchRMCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchRMCode.Image = CType(resources.GetObject("cmdSearchRMCode.Image"), System.Drawing.Image)
        Me.cmdSearchRMCode.Location = New System.Drawing.Point(203, 37)
        Me.cmdSearchRMCode.Name = "cmdSearchRMCode"
        Me.cmdSearchRMCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchRMCode.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchRMCode.TabIndex = 18
        Me.cmdSearchRMCode.TabStop = False
        Me.cmdSearchRMCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchRMCode, "Search")
        Me.cmdSearchRMCode.UseVisualStyleBackColor = False
        '
        'cmdSearchEmp
        '
        Me.cmdSearchEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEmp.Image = CType(resources.GetObject("cmdSearchEmp.Image"), System.Drawing.Image)
        Me.cmdSearchEmp.Location = New System.Drawing.Point(210, 89)
        Me.cmdSearchEmp.Name = "cmdSearchEmp"
        Me.cmdSearchEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchEmp.TabIndex = 13
        Me.cmdSearchEmp.TabStop = False
        Me.cmdSearchEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchEmp, "Search")
        Me.cmdSearchEmp.UseVisualStyleBackColor = False
        '
        'CmdSearchDept
        '
        Me.CmdSearchDept.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchDept.Image = CType(resources.GetObject("CmdSearchDept.Image"), System.Drawing.Image)
        Me.CmdSearchDept.Location = New System.Drawing.Point(210, 63)
        Me.CmdSearchDept.Name = "CmdSearchDept"
        Me.CmdSearchDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchDept.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchDept.TabIndex = 9
        Me.CmdSearchDept.TabStop = False
        Me.CmdSearchDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchDept, "Search")
        Me.CmdSearchDept.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(210, 13)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(709, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 38
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(442, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 34
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(575, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 36
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
        Me.cmdPrint.Location = New System.Drawing.Point(508, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 35
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(641, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 37
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(374, 12)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 33
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
        Me.cmdSave.Location = New System.Drawing.Point(307, 12)
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
        Me.cmdModify.Location = New System.Drawing.Point(240, 12)
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
        Me.cmdAdd.Location = New System.Drawing.Point(173, 12)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdSearchHeatNo
        '
        Me.cmdSearchHeatNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchHeatNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchHeatNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchHeatNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchHeatNo.Image = CType(resources.GetObject("cmdSearchHeatNo.Image"), System.Drawing.Image)
        Me.cmdSearchHeatNo.Location = New System.Drawing.Point(202, 61)
        Me.cmdSearchHeatNo.Name = "cmdSearchHeatNo"
        Me.cmdSearchHeatNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchHeatNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchHeatNo.TabIndex = 20
        Me.cmdSearchHeatNo.TabStop = False
        Me.cmdSearchHeatNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchHeatNo, "Search")
        Me.cmdSearchHeatNo.UseVisualStyleBackColor = False
        '
        'cmdPopulateBOM
        '
        Me.cmdPopulateBOM.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPopulateBOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulateBOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulateBOM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulateBOM.Location = New System.Drawing.Point(231, 12)
        Me.cmdPopulateBOM.Name = "cmdPopulateBOM"
        Me.cmdPopulateBOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulateBOM.Size = New System.Drawing.Size(145, 20)
        Me.cmdPopulateBOM.TabIndex = 73
        Me.cmdPopulateBOM.TabStop = False
        Me.cmdPopulateBOM.Text = "Get Data From BOM"
        Me.cmdPopulateBOM.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPopulateBOM, "Search")
        Me.cmdPopulateBOM.UseVisualStyleBackColor = False
        '
        'cmdSearchBOM
        '
        Me.cmdSearchBOM.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchBOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchBOM.Enabled = False
        Me.cmdSearchBOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchBOM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchBOM.Image = CType(resources.GetObject("cmdSearchBOM.Image"), System.Drawing.Image)
        Me.cmdSearchBOM.Location = New System.Drawing.Point(203, 14)
        Me.cmdSearchBOM.Name = "cmdSearchBOM"
        Me.cmdSearchBOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchBOM.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchBOM.TabIndex = 75
        Me.cmdSearchBOM.TabStop = False
        Me.cmdSearchBOM.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchBOM, "Search")
        Me.cmdSearchBOM.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.txtMachineNo)
        Me.FraFront.Controls.Add(Me.cmdSearchMacNo)
        Me.FraFront.Controls.Add(Me.lblMac)
        Me.FraFront.Controls.Add(Me.lblMachineNo)
        Me.FraFront.Controls.Add(Me.cboProductionFrom)
        Me.FraFront.Controls.Add(Me.Label18)
        Me.FraFront.Controls.Add(Me.Frame1)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.txtProdDate)
        Me.FraFront.Controls.Add(Me.txtEntryDate)
        Me.FraFront.Controls.Add(Me.txtRefTM)
        Me.FraFront.Controls.Add(Me.cboType)
        Me.FraFront.Controls.Add(Me.cmdSearchEmp)
        Me.FraFront.Controls.Add(Me.CmdSearchDept)
        Me.FraFront.Controls.Add(Me.txtPMemoNo)
        Me.FraFront.Controls.Add(Me.txtDept)
        Me.FraFront.Controls.Add(Me.txtRemarks)
        Me.FraFront.Controls.Add(Me.txtEmp)
        Me.FraFront.Controls.Add(Me.cboShiftcd)
        Me.FraFront.Controls.Add(Me.txtPMemoDate)
        Me.FraFront.Controls.Add(Me.cmdSearch)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.lblEmp)
        Me.FraFront.Controls.Add(Me.lblDept)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(910, 574)
        Me.FraFront.TabIndex = 41
        Me.FraFront.TabStop = False
        '
        'cboProductionFrom
        '
        Me.cboProductionFrom.BackColor = System.Drawing.SystemColors.Window
        Me.cboProductionFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboProductionFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProductionFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboProductionFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboProductionFrom.Location = New System.Drawing.Point(764, 89)
        Me.cboProductionFrom.Name = "cboProductionFrom"
        Me.cboProductionFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboProductionFrom.Size = New System.Drawing.Size(114, 22)
        Me.cboProductionFrom.TabIndex = 56
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(668, 93)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(91, 14)
        Me.Label18.TabIndex = 57
        Me.Label18.Text = "Production From :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdSearchBOM)
        Me.Frame1.Controls.Add(Me.txtSearchBOM)
        Me.Frame1.Controls.Add(Me.cmdPopulateBOM)
        Me.Frame1.Controls.Add(Me.Label20)
        Me.Frame1.Controls.Add(Me.cmdSearchHeatNo)
        Me.Frame1.Controls.Add(Me.txtHeatNo)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Controls.Add(Me.Label19)
        Me.Frame1.Controls.Add(Me.Label16)
        Me.Frame1.Controls.Add(Me.txtNetScrapWt)
        Me.Frame1.Controls.Add(Me.txtStockQty)
        Me.Frame1.Controls.Add(Me.txtCTLWt)
        Me.Frame1.Controls.Add(Me.txtNetRMWt)
        Me.Frame1.Controls.Add(Me.txtGrossRMWt)
        Me.Frame1.Controls.Add(Me.txtThickness)
        Me.Frame1.Controls.Add(Me.txtRMCode)
        Me.Frame1.Controls.Add(Me.cmdSearchRMCode)
        Me.Frame1.Controls.Add(Me.SprdMain)
        Me.Frame1.Controls.Add(Me.SprdMainScrap)
        Me.Frame1.Controls.Add(Me.Label17)
        Me.Frame1.Controls.Add(Me.lblDensity)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Controls.Add(Me.Label15)
        Me.Frame1.Controls.Add(Me.Label14)
        Me.Frame1.Controls.Add(Me.Label13)
        Me.Frame1.Controls.Add(Me._lblUnder_42)
        Me.Frame1.Controls.Add(Me.lblRMUOM)
        Me.Frame1.Controls.Add(Me.Label10)
        Me.Frame1.Controls.Add(Me.lblRMCode)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 159)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(908, 413)
        Me.Frame1.TabIndex = 53
        Me.Frame1.TabStop = False
        '
        'txtSearchBOM
        '
        Me.txtSearchBOM.AcceptsReturn = True
        Me.txtSearchBOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearchBOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchBOM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearchBOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchBOM.ForeColor = System.Drawing.Color.Blue
        Me.txtSearchBOM.Location = New System.Drawing.Point(97, 13)
        Me.txtSearchBOM.MaxLength = 0
        Me.txtSearchBOM.Name = "txtSearchBOM"
        Me.txtSearchBOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearchBOM.Size = New System.Drawing.Size(105, 20)
        Me.txtSearchBOM.TabIndex = 72
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(-2, 16)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(91, 14)
        Me.Label20.TabIndex = 74
        Me.Label20.Text = "Search FG BOM :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtHeatNo
        '
        Me.txtHeatNo.AcceptsReturn = True
        Me.txtHeatNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtHeatNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHeatNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHeatNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeatNo.ForeColor = System.Drawing.Color.Blue
        Me.txtHeatNo.Location = New System.Drawing.Point(97, 61)
        Me.txtHeatNo.MaxLength = 0
        Me.txtHeatNo.Name = "txtHeatNo"
        Me.txtHeatNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHeatNo.Size = New System.Drawing.Size(103, 20)
        Me.txtHeatNo.TabIndex = 19
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(0, 63)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(89, 14)
        Me.Label11.TabIndex = 71
        Me.Label11.Text = "Internal Heat No :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(365, 65)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(49, 14)
        Me.Label19.TabIndex = 69
        Me.Label19.Text = "Density :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(334, 65)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(23, 14)
        Me.Label16.TabIndex = 68
        Me.Label16.Text = "MM"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtNetScrapWt
        '
        Me.txtNetScrapWt.AcceptsReturn = True
        Me.txtNetScrapWt.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetScrapWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetScrapWt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetScrapWt.Enabled = False
        Me.txtNetScrapWt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetScrapWt.ForeColor = System.Drawing.Color.Blue
        Me.txtNetScrapWt.Location = New System.Drawing.Point(522, 388)
        Me.txtNetScrapWt.MaxLength = 0
        Me.txtNetScrapWt.Name = "txtNetScrapWt"
        Me.txtNetScrapWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetScrapWt.Size = New System.Drawing.Size(73, 20)
        Me.txtNetScrapWt.TabIndex = 65
        '
        'txtStockQty
        '
        Me.txtStockQty.AcceptsReturn = True
        Me.txtStockQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtStockQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStockQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStockQty.Enabled = False
        Me.txtStockQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockQty.ForeColor = System.Drawing.Color.Blue
        Me.txtStockQty.Location = New System.Drawing.Point(764, 37)
        Me.txtStockQty.MaxLength = 0
        Me.txtStockQty.Name = "txtStockQty"
        Me.txtStockQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStockQty.Size = New System.Drawing.Size(112, 20)
        Me.txtStockQty.TabIndex = 21
        '
        'txtCTLWt
        '
        Me.txtCTLWt.AcceptsReturn = True
        Me.txtCTLWt.BackColor = System.Drawing.SystemColors.Window
        Me.txtCTLWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCTLWt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCTLWt.Enabled = False
        Me.txtCTLWt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCTLWt.ForeColor = System.Drawing.Color.Blue
        Me.txtCTLWt.Location = New System.Drawing.Point(324, 388)
        Me.txtCTLWt.MaxLength = 0
        Me.txtCTLWt.Name = "txtCTLWt"
        Me.txtCTLWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCTLWt.Size = New System.Drawing.Size(69, 20)
        Me.txtCTLWt.TabIndex = 29
        '
        'txtNetRMWt
        '
        Me.txtNetRMWt.AcceptsReturn = True
        Me.txtNetRMWt.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetRMWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetRMWt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetRMWt.Enabled = False
        Me.txtNetRMWt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetRMWt.ForeColor = System.Drawing.Color.Blue
        Me.txtNetRMWt.Location = New System.Drawing.Point(97, 388)
        Me.txtNetRMWt.MaxLength = 0
        Me.txtNetRMWt.Name = "txtNetRMWt"
        Me.txtNetRMWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetRMWt.Size = New System.Drawing.Size(73, 20)
        Me.txtNetRMWt.TabIndex = 28
        '
        'txtGrossRMWt
        '
        Me.txtGrossRMWt.AcceptsReturn = True
        Me.txtGrossRMWt.BackColor = System.Drawing.SystemColors.Window
        Me.txtGrossRMWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrossRMWt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGrossRMWt.Enabled = False
        Me.txtGrossRMWt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrossRMWt.ForeColor = System.Drawing.Color.Blue
        Me.txtGrossRMWt.Location = New System.Drawing.Point(764, 61)
        Me.txtGrossRMWt.MaxLength = 0
        Me.txtGrossRMWt.Name = "txtGrossRMWt"
        Me.txtGrossRMWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGrossRMWt.Size = New System.Drawing.Size(112, 20)
        Me.txtGrossRMWt.TabIndex = 26
        '
        'txtThickness
        '
        Me.txtThickness.AcceptsReturn = True
        Me.txtThickness.BackColor = System.Drawing.SystemColors.Window
        Me.txtThickness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtThickness.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtThickness.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtThickness.ForeColor = System.Drawing.Color.Blue
        Me.txtThickness.Location = New System.Drawing.Point(287, 61)
        Me.txtThickness.MaxLength = 0
        Me.txtThickness.Name = "txtThickness"
        Me.txtThickness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtThickness.Size = New System.Drawing.Size(45, 20)
        Me.txtThickness.TabIndex = 22
        '
        'txtRMCode
        '
        Me.txtRMCode.AcceptsReturn = True
        Me.txtRMCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtRMCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRMCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRMCode.Enabled = False
        Me.txtRMCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRMCode.ForeColor = System.Drawing.Color.Blue
        Me.txtRMCode.Location = New System.Drawing.Point(97, 37)
        Me.txtRMCode.MaxLength = 0
        Me.txtRMCode.Name = "txtRMCode"
        Me.txtRMCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRMCode.Size = New System.Drawing.Size(105, 20)
        Me.txtRMCode.TabIndex = 17
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(4, 86)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(902, 171)
        Me.SprdMain.TabIndex = 27
        '
        'SprdMainScrap
        '
        Me.SprdMainScrap.DataSource = Nothing
        Me.SprdMainScrap.Location = New System.Drawing.Point(4, 261)
        Me.SprdMainScrap.Name = "SprdMainScrap"
        Me.SprdMainScrap.OcxState = CType(resources.GetObject("SprdMainScrap.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainScrap.Size = New System.Drawing.Size(902, 124)
        Me.SprdMainScrap.TabIndex = 27
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(457, 391)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(61, 14)
        Me.Label17.TabIndex = 67
        Me.Label17.Text = "Scrap Wt. :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDensity
        '
        Me.lblDensity.BackColor = System.Drawing.SystemColors.Control
        Me.lblDensity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDensity.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDensity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDensity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDensity.Location = New System.Drawing.Point(419, 61)
        Me.lblDensity.Name = "lblDensity"
        Me.lblDensity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDensity.Size = New System.Drawing.Size(60, 19)
        Me.lblDensity.TabIndex = 63
        Me.lblDensity.Text = "lblDensity"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(699, 39)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(60, 14)
        Me.Label9.TabIndex = 62
        Me.Label9.Text = "Stock Qty :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(236, 391)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(84, 14)
        Me.Label15.TabIndex = 61
        Me.Label15.Text = "CTL Wt. (Kgs) : "
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(5, 391)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(78, 14)
        Me.Label14.TabIndex = 60
        Me.Label14.Text = "Net Wt. (Kgs) :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(667, 67)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(92, 14)
        Me.Label13.TabIndex = 59
        Me.Label13.Text = "Gross Wt. (Kgs) :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblUnder_42
        '
        Me._lblUnder_42.AutoSize = True
        Me._lblUnder_42.BackColor = System.Drawing.SystemColors.Control
        Me._lblUnder_42.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblUnder_42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblUnder_42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnder.SetIndex(Me._lblUnder_42, CType(42, Short))
        Me._lblUnder_42.Location = New System.Drawing.Point(233, 65)
        Me._lblUnder_42.Name = "_lblUnder_42"
        Me._lblUnder_42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblUnder_42.Size = New System.Drawing.Size(46, 14)
        Me._lblUnder_42.TabIndex = 58
        Me._lblUnder_42.Text = "RM Dia :"
        Me._lblUnder_42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRMUOM
        '
        Me.lblRMUOM.BackColor = System.Drawing.SystemColors.Control
        Me.lblRMUOM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRMUOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRMUOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRMUOM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRMUOM.Location = New System.Drawing.Point(577, 37)
        Me.lblRMUOM.Name = "lblRMUOM"
        Me.lblRMUOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRMUOM.Size = New System.Drawing.Size(67, 19)
        Me.lblRMUOM.TabIndex = 20
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(33, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(56, 14)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "RM Code :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRMCode
        '
        Me.lblRMCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblRMCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRMCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRMCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRMCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRMCode.Location = New System.Drawing.Point(229, 37)
        Me.lblRMCode.Name = "lblRMCode"
        Me.lblRMCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRMCode.Size = New System.Drawing.Size(345, 19)
        Me.lblRMCode.TabIndex = 19
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(104, 36)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(231, 22)
        Me.cboDivision.TabIndex = 6
        '
        'txtProdDate
        '
        Me.txtProdDate.AcceptsReturn = True
        Me.txtProdDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtProdDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProdDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProdDate.Enabled = False
        Me.txtProdDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProdDate.ForeColor = System.Drawing.Color.Blue
        Me.txtProdDate.Location = New System.Drawing.Point(764, 13)
        Me.txtProdDate.MaxLength = 0
        Me.txtProdDate.Name = "txtProdDate"
        Me.txtProdDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProdDate.Size = New System.Drawing.Size(114, 20)
        Me.txtProdDate.TabIndex = 5
        '
        'txtEntryDate
        '
        Me.txtEntryDate.AcceptsReturn = True
        Me.txtEntryDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEntryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEntryDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEntryDate.Enabled = False
        Me.txtEntryDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntryDate.ForeColor = System.Drawing.Color.Blue
        Me.txtEntryDate.Location = New System.Drawing.Point(764, 120)
        Me.txtEntryDate.MaxLength = 0
        Me.txtEntryDate.Multiline = True
        Me.txtEntryDate.Name = "txtEntryDate"
        Me.txtEntryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEntryDate.Size = New System.Drawing.Size(114, 29)
        Me.txtEntryDate.TabIndex = 15
        '
        'txtRefTM
        '
        Me.txtRefTM.AcceptsReturn = True
        Me.txtRefTM.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefTM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefTM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefTM.Enabled = False
        Me.txtRefTM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefTM.ForeColor = System.Drawing.Color.Blue
        Me.txtRefTM.Location = New System.Drawing.Point(438, 13)
        Me.txtRefTM.MaxLength = 0
        Me.txtRefTM.Name = "txtRefTM"
        Me.txtRefTM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefTM.Size = New System.Drawing.Size(43, 20)
        Me.txtRefTM.TabIndex = 4
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboType.Location = New System.Drawing.Point(764, 36)
        Me.cboType.Name = "cboType"
        Me.cboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboType.Size = New System.Drawing.Size(114, 22)
        Me.cboType.TabIndex = 7
        '
        'txtPMemoNo
        '
        Me.txtPMemoNo.AcceptsReturn = True
        Me.txtPMemoNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMemoNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMemoNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPMemoNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMemoNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPMemoNo.Location = New System.Drawing.Point(104, 13)
        Me.txtPMemoNo.MaxLength = 0
        Me.txtPMemoNo.Name = "txtPMemoNo"
        Me.txtPMemoNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPMemoNo.Size = New System.Drawing.Size(105, 20)
        Me.txtPMemoNo.TabIndex = 1
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.ForeColor = System.Drawing.Color.Blue
        Me.txtDept.Location = New System.Drawing.Point(104, 63)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(105, 20)
        Me.txtDept.TabIndex = 8
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtRemarks.Location = New System.Drawing.Point(104, 114)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(555, 20)
        Me.txtRemarks.TabIndex = 16
        '
        'txtEmp
        '
        Me.txtEmp.AcceptsReturn = True
        Me.txtEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.ForeColor = System.Drawing.Color.Blue
        Me.txtEmp.Location = New System.Drawing.Point(104, 89)
        Me.txtEmp.MaxLength = 0
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmp.Size = New System.Drawing.Size(105, 20)
        Me.txtEmp.TabIndex = 12
        '
        'cboShiftcd
        '
        Me.cboShiftcd.BackColor = System.Drawing.SystemColors.Window
        Me.cboShiftcd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShiftcd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShiftcd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShiftcd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShiftcd.Location = New System.Drawing.Point(764, 62)
        Me.cboShiftcd.Name = "cboShiftcd"
        Me.cboShiftcd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShiftcd.Size = New System.Drawing.Size(114, 22)
        Me.cboShiftcd.TabIndex = 11
        '
        'txtPMemoDate
        '
        Me.txtPMemoDate.AcceptsReturn = True
        Me.txtPMemoDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMemoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMemoDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPMemoDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMemoDate.ForeColor = System.Drawing.Color.Blue
        Me.txtPMemoDate.Location = New System.Drawing.Point(348, 13)
        Me.txtPMemoDate.MaxLength = 0
        Me.txtPMemoDate.Name = "txtPMemoDate"
        Me.txtPMemoDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPMemoDate.Size = New System.Drawing.Size(89, 20)
        Me.txtPMemoDate.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(50, 36)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(50, 14)
        Me.Label12.TabIndex = 52
        Me.Label12.Text = "Division :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(670, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(89, 14)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "Production Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(696, 120)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(63, 14)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "Entry Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(723, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(36, 14)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblEmp
        '
        Me.lblEmp.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmp.Location = New System.Drawing.Point(236, 89)
        Me.lblEmp.Name = "lblEmp"
        Me.lblEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmp.Size = New System.Drawing.Size(421, 19)
        Me.lblEmp.TabIndex = 14
        '
        'lblDept
        '
        Me.lblDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDept.Location = New System.Drawing.Point(236, 63)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDept.Size = New System.Drawing.Size(421, 19)
        Me.lblDept.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(284, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(55, 14)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Ref Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(696, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 14)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Shift Code :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(45, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(55, 14)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Remarks :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(41, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(59, 14)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Employee :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(60, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(35, 14)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "Dept :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(35, 16)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(65, 14)
        Me.lblCust.TabIndex = 42
        Me.lblCust.Text = "P.Memo No :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(910, 568)
        Me.SprdView.TabIndex = 40
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
        Me.AdoDCMain.TabIndex = 42
        Me.AdoDCMain.Text = "Adodc1"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblMKey)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(2, 567)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(908, 53)
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
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(682, 18)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(29, 17)
        Me.lblBookType.TabIndex = 50
        Me.lblBookType.Text = "lblBookType"
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(52, 14)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 48
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'txtMachineNo
        '
        Me.txtMachineNo.AcceptsReturn = True
        Me.txtMachineNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachineNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachineNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMachineNo.Location = New System.Drawing.Point(104, 139)
        Me.txtMachineNo.MaxLength = 0
        Me.txtMachineNo.Name = "txtMachineNo"
        Me.txtMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachineNo.Size = New System.Drawing.Size(105, 20)
        Me.txtMachineNo.TabIndex = 58
        '
        'cmdSearchMacNo
        '
        Me.cmdSearchMacNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMacNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMacNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMacNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMacNo.Image = CType(resources.GetObject("cmdSearchMacNo.Image"), System.Drawing.Image)
        Me.cmdSearchMacNo.Location = New System.Drawing.Point(210, 139)
        Me.cmdSearchMacNo.Name = "cmdSearchMacNo"
        Me.cmdSearchMacNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMacNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchMacNo.TabIndex = 59
        Me.cmdSearchMacNo.TabStop = False
        Me.cmdSearchMacNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMacNo, "Search")
        Me.cmdSearchMacNo.UseVisualStyleBackColor = False
        '
        'lblMac
        '
        Me.lblMac.AutoSize = True
        Me.lblMac.BackColor = System.Drawing.SystemColors.Control
        Me.lblMac.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMac.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMac.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMac.Location = New System.Drawing.Point(27, 143)
        Me.lblMac.Name = "lblMac"
        Me.lblMac.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMac.Size = New System.Drawing.Size(73, 13)
        Me.lblMac.TabIndex = 61
        Me.lblMac.Text = "Machine No :"
        Me.lblMac.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMachineNo
        '
        Me.lblMachineNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblMachineNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMachineNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMachineNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMachineNo.Location = New System.Drawing.Point(236, 139)
        Me.lblMachineNo.Name = "lblMachineNo"
        Me.lblMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMachineNo.Size = New System.Drawing.Size(421, 19)
        Me.lblMachineNo.TabIndex = 60
        '
        'FrmPMemoDiaCuttingPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.AdoDCMain)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmPMemoDiaCuttingPlan"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Production - Cutting Plan"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMainScrap, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblUnder, System.ComponentModel.ISupportInitialize).EndInit()
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
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cboProductionFrom As ComboBox
    Public WithEvents Label18 As Label
    Public WithEvents Label16 As Label
    Public WithEvents Label19 As Label
    Public WithEvents txtHeatNo As TextBox
    Public WithEvents Label11 As Label
    Public WithEvents cmdSearchHeatNo As Button
    Public WithEvents txtSearchBOM As TextBox
    Public WithEvents cmdPopulateBOM As Button
    Public WithEvents Label20 As Label
    Public WithEvents cmdSearchBOM As Button
    Public WithEvents txtMachineNo As TextBox
    Public WithEvents cmdSearchMacNo As Button
    Public WithEvents lblMac As Label
    Public WithEvents lblMachineNo As Label
#End Region
End Class