Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmReworkProdDeptWise
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
    Public WithEvents cboShiftcd As System.Windows.Forms.ComboBox
    Public WithEvents chkApproved As System.Windows.Forms.CheckBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtProdDate As System.Windows.Forms.TextBox
    Public WithEvents txtRefTM As System.Windows.Forms.TextBox
    Public WithEvents txtPMemoNo As System.Windows.Forms.TextBox
    Public WithEvents txtPMemoDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents txtStockQty As System.Windows.Forms.TextBox
    Public WithEvents txtRecdQty As System.Windows.Forms.TextBox
    Public WithEvents txtRecdDate As System.Windows.Forms.TextBox
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents fraRecdDetail As System.Windows.Forms.GroupBox
    Public WithEvents txtBatchNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchBatchNo As System.Windows.Forms.Button
    Public WithEvents txtSBSlipDate As System.Windows.Forms.TextBox
    Public WithEvents txtSBSlipNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSBSlipSearch As System.Windows.Forms.Button
    Public WithEvents txtReWorkManDays As System.Windows.Forms.TextBox
    Public WithEvents txtEmp As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchDept As System.Windows.Forms.Button
    Public WithEvents cmdSearchEmp As System.Windows.Forms.Button
    Public WithEvents cboType As System.Windows.Forms.ComboBox
    Public WithEvents txtEntryDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchProductCode As System.Windows.Forms.Button
    Public WithEvents txtProductCode As System.Windows.Forms.TextBox
    Public WithEvents txtAvailableQty As System.Windows.Forms.TextBox
    Public WithEvents txtReWorkQty As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchSendDept As System.Windows.Forms.Button
    Public WithEvents txtSendDept As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents txtReworkCost As System.Windows.Forms.TextBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents lblApproval As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblDept As System.Windows.Forms.Label
    Public WithEvents lblEmp As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblProductCode As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblSendDept As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblProductionUOM As System.Windows.Forms.Label
    Public WithEvents lblShow As System.Windows.Forms.Label
    Public WithEvents FraRework As System.Windows.Forms.GroupBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents AdoDCMain As VB6.ADODC
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmReworkProdDeptWise))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdSearchBatchNo = New System.Windows.Forms.Button()
        Me.cmdSBSlipSearch = New System.Windows.Forms.Button()
        Me.CmdSearchDept = New System.Windows.Forms.Button()
        Me.cmdSearchEmp = New System.Windows.Forms.Button()
        Me.cmdSearchProductCode = New System.Windows.Forms.Button()
        Me.CmdSearchSendDept = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.cboShiftcd = New System.Windows.Forms.ComboBox()
        Me.chkApproved = New System.Windows.Forms.CheckBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtProdDate = New System.Windows.Forms.TextBox()
        Me.txtRefTM = New System.Windows.Forms.TextBox()
        Me.txtPMemoNo = New System.Windows.Forms.TextBox()
        Me.txtPMemoDate = New System.Windows.Forms.TextBox()
        Me.fraRecdDetail = New System.Windows.Forms.GroupBox()
        Me.txtStockQty = New System.Windows.Forms.TextBox()
        Me.txtRecdQty = New System.Windows.Forms.TextBox()
        Me.txtRecdDate = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.FraRework = New System.Windows.Forms.GroupBox()
        Me.txtBatchNo = New System.Windows.Forms.TextBox()
        Me.txtSBSlipDate = New System.Windows.Forms.TextBox()
        Me.txtSBSlipNo = New System.Windows.Forms.TextBox()
        Me.txtReWorkManDays = New System.Windows.Forms.TextBox()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.txtEntryDate = New System.Windows.Forms.TextBox()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.txtAvailableQty = New System.Windows.Forms.TextBox()
        Me.txtReWorkQty = New System.Windows.Forms.TextBox()
        Me.txtSendDept = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.txtReworkCost = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblApproval = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDept = New System.Windows.Forms.Label()
        Me.lblEmp = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblProductCode = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblSendDept = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblProductionUOM = New System.Windows.Forms.Label()
        Me.lblShow = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.AdoDCMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        Me.fraRecdDetail.SuspendLayout()
        Me.FraRework.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(210, 14)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdSearchBatchNo
        '
        Me.cmdSearchBatchNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchBatchNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchBatchNo.Enabled = False
        Me.cmdSearchBatchNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchBatchNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchBatchNo.Image = CType(resources.GetObject("cmdSearchBatchNo.Image"), System.Drawing.Image)
        Me.cmdSearchBatchNo.Location = New System.Drawing.Point(209, 76)
        Me.cmdSearchBatchNo.Name = "cmdSearchBatchNo"
        Me.cmdSearchBatchNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchBatchNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchBatchNo.TabIndex = 16
        Me.cmdSearchBatchNo.TabStop = False
        Me.cmdSearchBatchNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchBatchNo, "Search")
        Me.cmdSearchBatchNo.UseVisualStyleBackColor = False
        '
        'cmdSBSlipSearch
        '
        Me.cmdSBSlipSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSBSlipSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSBSlipSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSBSlipSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSBSlipSearch.Image = CType(resources.GetObject("cmdSBSlipSearch.Image"), System.Drawing.Image)
        Me.cmdSBSlipSearch.Location = New System.Drawing.Point(210, 32)
        Me.cmdSBSlipSearch.Name = "cmdSBSlipSearch"
        Me.cmdSBSlipSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSBSlipSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSBSlipSearch.TabIndex = 11
        Me.cmdSBSlipSearch.TabStop = False
        Me.cmdSBSlipSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSBSlipSearch, "Search")
        Me.cmdSBSlipSearch.UseVisualStyleBackColor = False
        '
        'CmdSearchDept
        '
        Me.CmdSearchDept.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchDept.Image = CType(resources.GetObject("CmdSearchDept.Image"), System.Drawing.Image)
        Me.CmdSearchDept.Location = New System.Drawing.Point(210, 10)
        Me.CmdSearchDept.Name = "CmdSearchDept"
        Me.CmdSearchDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchDept.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchDept.TabIndex = 9
        Me.CmdSearchDept.TabStop = False
        Me.CmdSearchDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchDept, "Search")
        Me.CmdSearchDept.UseVisualStyleBackColor = False
        '
        'cmdSearchEmp
        '
        Me.cmdSearchEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEmp.Image = CType(resources.GetObject("cmdSearchEmp.Image"), System.Drawing.Image)
        Me.cmdSearchEmp.Location = New System.Drawing.Point(210, 121)
        Me.cmdSearchEmp.Name = "cmdSearchEmp"
        Me.cmdSearchEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchEmp.TabIndex = 20
        Me.cmdSearchEmp.TabStop = False
        Me.cmdSearchEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchEmp, "Search")
        Me.cmdSearchEmp.UseVisualStyleBackColor = False
        '
        'cmdSearchProductCode
        '
        Me.cmdSearchProductCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProductCode.Enabled = False
        Me.cmdSearchProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProductCode.Image = CType(resources.GetObject("cmdSearchProductCode.Image"), System.Drawing.Image)
        Me.cmdSearchProductCode.Location = New System.Drawing.Point(210, 54)
        Me.cmdSearchProductCode.Name = "cmdSearchProductCode"
        Me.cmdSearchProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProductCode.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchProductCode.TabIndex = 14
        Me.cmdSearchProductCode.TabStop = False
        Me.cmdSearchProductCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProductCode, "Search")
        Me.cmdSearchProductCode.UseVisualStyleBackColor = False
        '
        'CmdSearchSendDept
        '
        Me.CmdSearchSendDept.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchSendDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchSendDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchSendDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchSendDept.Image = CType(resources.GetObject("CmdSearchSendDept.Image"), System.Drawing.Image)
        Me.CmdSearchSendDept.Location = New System.Drawing.Point(210, 144)
        Me.CmdSearchSendDept.Name = "CmdSearchSendDept"
        Me.CmdSearchSendDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchSendDept.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchSendDept.TabIndex = 22
        Me.CmdSearchSendDept.TabStop = False
        Me.CmdSearchSendDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchSendDept, "Search")
        Me.CmdSearchSendDept.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(166, 12)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(233, 12)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 37
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(300, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 38
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(367, 12)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 39
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(634, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 43
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(501, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 41
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(568, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 42
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(435, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 40
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(702, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 44
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.cboShiftcd)
        Me.FraFront.Controls.Add(Me.chkApproved)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.txtProdDate)
        Me.FraFront.Controls.Add(Me.txtRefTM)
        Me.FraFront.Controls.Add(Me.txtPMemoNo)
        Me.FraFront.Controls.Add(Me.txtPMemoDate)
        Me.FraFront.Controls.Add(Me.cmdSearch)
        Me.FraFront.Controls.Add(Me.fraRecdDetail)
        Me.FraFront.Controls.Add(Me.FraRework)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label16)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -4)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(908, 572)
        Me.FraFront.TabIndex = 49
        Me.FraFront.TabStop = False
        '
        'cboShiftcd
        '
        Me.cboShiftcd.BackColor = System.Drawing.SystemColors.Window
        Me.cboShiftcd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShiftcd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShiftcd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShiftcd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShiftcd.Location = New System.Drawing.Point(105, 36)
        Me.cboShiftcd.Name = "cboShiftcd"
        Me.cboShiftcd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShiftcd.Size = New System.Drawing.Size(107, 22)
        Me.cboShiftcd.TabIndex = 6
        '
        'chkApproved
        '
        Me.chkApproved.AutoSize = True
        Me.chkApproved.BackColor = System.Drawing.SystemColors.Control
        Me.chkApproved.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkApproved.Enabled = False
        Me.chkApproved.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApproved.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkApproved.Location = New System.Drawing.Point(596, 40)
        Me.chkApproved.Name = "chkApproved"
        Me.chkApproved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkApproved.Size = New System.Drawing.Size(74, 18)
        Me.chkApproved.TabIndex = 71
        Me.chkApproved.Text = "Approved"
        Me.chkApproved.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(342, 36)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(215, 22)
        Me.cboDivision.TabIndex = 7
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
        Me.txtProdDate.Location = New System.Drawing.Point(596, 14)
        Me.txtProdDate.MaxLength = 0
        Me.txtProdDate.Name = "txtProdDate"
        Me.txtProdDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProdDate.Size = New System.Drawing.Size(109, 20)
        Me.txtProdDate.TabIndex = 5
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
        Me.txtRefTM.Location = New System.Drawing.Point(432, 14)
        Me.txtRefTM.MaxLength = 0
        Me.txtRefTM.Name = "txtRefTM"
        Me.txtRefTM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefTM.Size = New System.Drawing.Size(43, 20)
        Me.txtRefTM.TabIndex = 4
        '
        'txtPMemoNo
        '
        Me.txtPMemoNo.AcceptsReturn = True
        Me.txtPMemoNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMemoNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMemoNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPMemoNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMemoNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPMemoNo.Location = New System.Drawing.Point(104, 14)
        Me.txtPMemoNo.MaxLength = 0
        Me.txtPMemoNo.Name = "txtPMemoNo"
        Me.txtPMemoNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPMemoNo.Size = New System.Drawing.Size(105, 20)
        Me.txtPMemoNo.TabIndex = 1
        '
        'txtPMemoDate
        '
        Me.txtPMemoDate.AcceptsReturn = True
        Me.txtPMemoDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMemoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMemoDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPMemoDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMemoDate.ForeColor = System.Drawing.Color.Blue
        Me.txtPMemoDate.Location = New System.Drawing.Point(342, 14)
        Me.txtPMemoDate.MaxLength = 0
        Me.txtPMemoDate.Name = "txtPMemoDate"
        Me.txtPMemoDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPMemoDate.Size = New System.Drawing.Size(89, 20)
        Me.txtPMemoDate.TabIndex = 3
        '
        'fraRecdDetail
        '
        Me.fraRecdDetail.BackColor = System.Drawing.SystemColors.Control
        Me.fraRecdDetail.Controls.Add(Me.txtStockQty)
        Me.fraRecdDetail.Controls.Add(Me.txtRecdQty)
        Me.fraRecdDetail.Controls.Add(Me.txtRecdDate)
        Me.fraRecdDetail.Controls.Add(Me.Label15)
        Me.fraRecdDetail.Controls.Add(Me.Label14)
        Me.fraRecdDetail.Controls.Add(Me.Label12)
        Me.fraRecdDetail.Enabled = False
        Me.fraRecdDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRecdDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraRecdDetail.Location = New System.Drawing.Point(0, 535)
        Me.fraRecdDetail.Name = "fraRecdDetail"
        Me.fraRecdDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.fraRecdDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraRecdDetail.Size = New System.Drawing.Size(723, 35)
        Me.fraRecdDetail.TabIndex = 61
        Me.fraRecdDetail.TabStop = False
        Me.fraRecdDetail.Text = "Received Department Detail"
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
        Me.txtStockQty.Location = New System.Drawing.Point(602, 12)
        Me.txtStockQty.MaxLength = 0
        Me.txtStockQty.Name = "txtStockQty"
        Me.txtStockQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStockQty.Size = New System.Drawing.Size(105, 20)
        Me.txtStockQty.TabIndex = 36
        '
        'txtRecdQty
        '
        Me.txtRecdQty.AcceptsReturn = True
        Me.txtRecdQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecdQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecdQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecdQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecdQty.ForeColor = System.Drawing.Color.Blue
        Me.txtRecdQty.Location = New System.Drawing.Point(210, 12)
        Me.txtRecdQty.MaxLength = 0
        Me.txtRecdQty.Name = "txtRecdQty"
        Me.txtRecdQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecdQty.Size = New System.Drawing.Size(105, 20)
        Me.txtRecdQty.TabIndex = 34
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
        Me.txtRecdDate.Location = New System.Drawing.Point(404, 12)
        Me.txtRecdDate.MaxLength = 0
        Me.txtRecdDate.Name = "txtRecdDate"
        Me.txtRecdDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecdDate.Size = New System.Drawing.Size(105, 20)
        Me.txtRecdDate.TabIndex = 35
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(533, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(60, 14)
        Me.Label15.TabIndex = 64
        Me.Label15.Text = "Stock Qty :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(144, 15)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(58, 14)
        Me.Label14.TabIndex = 63
        Me.Label14.Text = "Recd Qty :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(328, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(63, 14)
        Me.Label12.TabIndex = 62
        Me.Label12.Text = "Recd Date :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraRework
        '
        Me.FraRework.BackColor = System.Drawing.SystemColors.Control
        Me.FraRework.Controls.Add(Me.txtBatchNo)
        Me.FraRework.Controls.Add(Me.cmdSearchBatchNo)
        Me.FraRework.Controls.Add(Me.txtSBSlipDate)
        Me.FraRework.Controls.Add(Me.txtSBSlipNo)
        Me.FraRework.Controls.Add(Me.cmdSBSlipSearch)
        Me.FraRework.Controls.Add(Me.txtReWorkManDays)
        Me.FraRework.Controls.Add(Me.txtEmp)
        Me.FraRework.Controls.Add(Me.txtRemarks)
        Me.FraRework.Controls.Add(Me.txtDept)
        Me.FraRework.Controls.Add(Me.CmdSearchDept)
        Me.FraRework.Controls.Add(Me.cmdSearchEmp)
        Me.FraRework.Controls.Add(Me.cboType)
        Me.FraRework.Controls.Add(Me.txtEntryDate)
        Me.FraRework.Controls.Add(Me.cmdSearchProductCode)
        Me.FraRework.Controls.Add(Me.txtProductCode)
        Me.FraRework.Controls.Add(Me.txtAvailableQty)
        Me.FraRework.Controls.Add(Me.txtReWorkQty)
        Me.FraRework.Controls.Add(Me.CmdSearchSendDept)
        Me.FraRework.Controls.Add(Me.txtSendDept)
        Me.FraRework.Controls.Add(Me.SprdMain)
        Me.FraRework.Controls.Add(Me.txtReworkCost)
        Me.FraRework.Controls.Add(Me.Label21)
        Me.FraRework.Controls.Add(Me.Label20)
        Me.FraRework.Controls.Add(Me.Label19)
        Me.FraRework.Controls.Add(Me.lblApproval)
        Me.FraRework.Controls.Add(Me.Label18)
        Me.FraRework.Controls.Add(Me.Label17)
        Me.FraRework.Controls.Add(Me.Label3)
        Me.FraRework.Controls.Add(Me.Label1)
        Me.FraRework.Controls.Add(Me.Label4)
        Me.FraRework.Controls.Add(Me.lblDept)
        Me.FraRework.Controls.Add(Me.lblEmp)
        Me.FraRework.Controls.Add(Me.Label2)
        Me.FraRework.Controls.Add(Me.Label6)
        Me.FraRework.Controls.Add(Me.lblProductCode)
        Me.FraRework.Controls.Add(Me.Label10)
        Me.FraRework.Controls.Add(Me.Label9)
        Me.FraRework.Controls.Add(Me.Label11)
        Me.FraRework.Controls.Add(Me.lblSendDept)
        Me.FraRework.Controls.Add(Me.Label13)
        Me.FraRework.Controls.Add(Me.lblProductionUOM)
        Me.FraRework.Controls.Add(Me.lblShow)
        Me.FraRework.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraRework.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraRework.Location = New System.Drawing.Point(0, 56)
        Me.FraRework.Name = "FraRework"
        Me.FraRework.Padding = New System.Windows.Forms.Padding(0)
        Me.FraRework.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraRework.Size = New System.Drawing.Size(908, 473)
        Me.FraRework.TabIndex = 50
        Me.FraRework.TabStop = False
        '
        'txtBatchNo
        '
        Me.txtBatchNo.AcceptsReturn = True
        Me.txtBatchNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBatchNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBatchNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBatchNo.Enabled = False
        Me.txtBatchNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBatchNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBatchNo.Location = New System.Drawing.Point(103, 76)
        Me.txtBatchNo.MaxLength = 0
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBatchNo.Size = New System.Drawing.Size(105, 20)
        Me.txtBatchNo.TabIndex = 15
        '
        'txtSBSlipDate
        '
        Me.txtSBSlipDate.AcceptsReturn = True
        Me.txtSBSlipDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSBSlipDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSBSlipDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSBSlipDate.Enabled = False
        Me.txtSBSlipDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSBSlipDate.ForeColor = System.Drawing.Color.Blue
        Me.txtSBSlipDate.Location = New System.Drawing.Point(272, 32)
        Me.txtSBSlipDate.MaxLength = 0
        Me.txtSBSlipDate.Name = "txtSBSlipDate"
        Me.txtSBSlipDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSBSlipDate.Size = New System.Drawing.Size(81, 20)
        Me.txtSBSlipDate.TabIndex = 12
        '
        'txtSBSlipNo
        '
        Me.txtSBSlipNo.AcceptsReturn = True
        Me.txtSBSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSBSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSBSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSBSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSBSlipNo.ForeColor = System.Drawing.Color.Blue
        Me.txtSBSlipNo.Location = New System.Drawing.Point(104, 32)
        Me.txtSBSlipNo.MaxLength = 0
        Me.txtSBSlipNo.Name = "txtSBSlipNo"
        Me.txtSBSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSBSlipNo.Size = New System.Drawing.Size(105, 20)
        Me.txtSBSlipNo.TabIndex = 10
        '
        'txtReWorkManDays
        '
        Me.txtReWorkManDays.AcceptsReturn = True
        Me.txtReWorkManDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtReWorkManDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReWorkManDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReWorkManDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReWorkManDays.ForeColor = System.Drawing.Color.Blue
        Me.txtReWorkManDays.Location = New System.Drawing.Point(666, 166)
        Me.txtReWorkManDays.MaxLength = 0
        Me.txtReWorkManDays.Name = "txtReWorkManDays"
        Me.txtReWorkManDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReWorkManDays.Size = New System.Drawing.Size(41, 20)
        Me.txtReWorkManDays.TabIndex = 25
        '
        'txtEmp
        '
        Me.txtEmp.AcceptsReturn = True
        Me.txtEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.ForeColor = System.Drawing.Color.Blue
        Me.txtEmp.Location = New System.Drawing.Point(104, 121)
        Me.txtEmp.MaxLength = 0
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmp.Size = New System.Drawing.Size(105, 20)
        Me.txtEmp.TabIndex = 19
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtRemarks.Location = New System.Drawing.Point(104, 166)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(305, 20)
        Me.txtRemarks.TabIndex = 23
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.ForeColor = System.Drawing.Color.Blue
        Me.txtDept.Location = New System.Drawing.Point(104, 10)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(105, 20)
        Me.txtDept.TabIndex = 8
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboType.Location = New System.Drawing.Point(598, 10)
        Me.cboType.Name = "cboType"
        Me.cboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboType.Size = New System.Drawing.Size(111, 22)
        Me.cboType.TabIndex = 27
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
        Me.txtEntryDate.Location = New System.Drawing.Point(562, 35)
        Me.txtEntryDate.MaxLength = 0
        Me.txtEntryDate.Multiline = True
        Me.txtEntryDate.Name = "txtEntryDate"
        Me.txtEntryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEntryDate.Size = New System.Drawing.Size(147, 37)
        Me.txtEntryDate.TabIndex = 28
        '
        'txtProductCode
        '
        Me.txtProductCode.AcceptsReturn = True
        Me.txtProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductCode.Enabled = False
        Me.txtProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.ForeColor = System.Drawing.Color.Blue
        Me.txtProductCode.Location = New System.Drawing.Point(104, 54)
        Me.txtProductCode.MaxLength = 0
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.Size = New System.Drawing.Size(105, 20)
        Me.txtProductCode.TabIndex = 13
        '
        'txtAvailableQty
        '
        Me.txtAvailableQty.AcceptsReturn = True
        Me.txtAvailableQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtAvailableQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAvailableQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAvailableQty.Enabled = False
        Me.txtAvailableQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAvailableQty.ForeColor = System.Drawing.Color.Blue
        Me.txtAvailableQty.Location = New System.Drawing.Point(104, 98)
        Me.txtAvailableQty.MaxLength = 0
        Me.txtAvailableQty.Name = "txtAvailableQty"
        Me.txtAvailableQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAvailableQty.Size = New System.Drawing.Size(105, 20)
        Me.txtAvailableQty.TabIndex = 17
        '
        'txtReWorkQty
        '
        Me.txtReWorkQty.AcceptsReturn = True
        Me.txtReWorkQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtReWorkQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReWorkQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReWorkQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReWorkQty.ForeColor = System.Drawing.Color.Blue
        Me.txtReWorkQty.Location = New System.Drawing.Point(378, 98)
        Me.txtReWorkQty.MaxLength = 0
        Me.txtReWorkQty.Name = "txtReWorkQty"
        Me.txtReWorkQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReWorkQty.Size = New System.Drawing.Size(105, 20)
        Me.txtReWorkQty.TabIndex = 18
        '
        'txtSendDept
        '
        Me.txtSendDept.AcceptsReturn = True
        Me.txtSendDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtSendDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSendDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSendDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSendDept.ForeColor = System.Drawing.Color.Blue
        Me.txtSendDept.Location = New System.Drawing.Point(104, 144)
        Me.txtSendDept.MaxLength = 0
        Me.txtSendDept.Name = "txtSendDept"
        Me.txtSendDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSendDept.Size = New System.Drawing.Size(105, 20)
        Me.txtSendDept.TabIndex = 21
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 196)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(904, 272)
        Me.SprdMain.TabIndex = 33
        '
        'txtReworkCost
        '
        Me.txtReworkCost.AcceptsReturn = True
        Me.txtReworkCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtReworkCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReworkCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReworkCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReworkCost.ForeColor = System.Drawing.Color.Blue
        Me.txtReworkCost.Location = New System.Drawing.Point(500, 166)
        Me.txtReworkCost.MaxLength = 0
        Me.txtReworkCost.Name = "txtReworkCost"
        Me.txtReworkCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReworkCost.Size = New System.Drawing.Size(47, 20)
        Me.txtReworkCost.TabIndex = 24
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(40, 79)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(57, 14)
        Me.Label21.TabIndex = 76
        Me.Label21.Text = "Batch No :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(236, 34)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(35, 14)
        Me.Label20.TabIndex = 75
        Me.Label20.Text = "Date :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(16, 34)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(81, 14)
        Me.Label19.TabIndex = 74
        Me.Label19.Text = "Return Slip No :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblApproval
        '
        Me.lblApproval.BackColor = System.Drawing.SystemColors.Control
        Me.lblApproval.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblApproval.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApproval.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblApproval.Location = New System.Drawing.Point(600, 88)
        Me.lblApproval.Name = "lblApproval"
        Me.lblApproval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblApproval.Size = New System.Drawing.Size(85, 15)
        Me.lblApproval.TabIndex = 72
        Me.lblApproval.Text = "lblApproval"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(557, 167)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(99, 14)
        Me.Label18.TabIndex = 70
        Me.Label18.Text = "Rework Man Days:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(416, 167)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(76, 14)
        Me.Label17.TabIndex = 69
        Me.Label17.Text = "Rework Cost :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(59, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(38, 14)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Deptt :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(38, 124)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(59, 14)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "Employee :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(53, 169)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(44, 14)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Action :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDept
        '
        Me.lblDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDept.Location = New System.Drawing.Point(236, 10)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDept.Size = New System.Drawing.Size(247, 19)
        Me.lblDept.TabIndex = 26
        '
        'lblEmp
        '
        Me.lblEmp.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmp.Location = New System.Drawing.Point(236, 121)
        Me.lblEmp.Name = "lblEmp"
        Me.lblEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmp.Size = New System.Drawing.Size(247, 19)
        Me.lblEmp.TabIndex = 29
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(556, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(36, 14)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Type :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(491, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(63, 14)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Entry Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProductCode
        '
        Me.lblProductCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductCode.Location = New System.Drawing.Point(236, 54)
        Me.lblProductCode.Name = "lblProductCode"
        Me.lblProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductCode.Size = New System.Drawing.Size(247, 19)
        Me.lblProductCode.TabIndex = 30
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(19, 57)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(78, 14)
        Me.Label10.TabIndex = 55
        Me.Label10.Text = "Product Code :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(20, 103)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(77, 14)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Available Qty :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(301, 99)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(71, 14)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Rework Qty :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSendDept
        '
        Me.lblSendDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblSendDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSendDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSendDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSendDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSendDept.Location = New System.Drawing.Point(237, 144)
        Me.lblSendDept.Name = "lblSendDept"
        Me.lblSendDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSendDept.Size = New System.Drawing.Size(247, 19)
        Me.lblSendDept.TabIndex = 32
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(31, 147)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(66, 14)
        Me.Label13.TabIndex = 52
        Me.Label13.Text = "Send Deptt :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProductionUOM
        '
        Me.lblProductionUOM.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductionUOM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProductionUOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductionUOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductionUOM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductionUOM.Location = New System.Drawing.Point(212, 98)
        Me.lblProductionUOM.Name = "lblProductionUOM"
        Me.lblProductionUOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductionUOM.Size = New System.Drawing.Size(61, 19)
        Me.lblProductionUOM.TabIndex = 31
        '
        'lblShow
        '
        Me.lblShow.AutoSize = True
        Me.lblShow.BackColor = System.Drawing.SystemColors.Control
        Me.lblShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblShow.Location = New System.Drawing.Point(576, 140)
        Me.lblShow.Name = "lblShow"
        Me.lblShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblShow.Size = New System.Drawing.Size(46, 14)
        Me.lblShow.TabIndex = 51
        Me.lblShow.Text = "lblShow"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(33, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 14)
        Me.Label5.TabIndex = 73
        Me.Label5.Text = "Shift Code :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(284, 40)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(50, 14)
        Me.Label16.TabIndex = 68
        Me.Label16.Text = "Division :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(489, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(89, 14)
        Me.Label8.TabIndex = 67
        Me.Label8.Text = "Production Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(278, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(55, 14)
        Me.Label7.TabIndex = 66
        Me.Label7.Text = "Ref Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(50, 18)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(46, 14)
        Me.lblCust.TabIndex = 65
        Me.lblCust.Text = "Ref No :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.AdoDCMain.TabIndex = 50
        Me.AdoDCMain.Text = "Adodc1"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(908, 568)
        Me.SprdView.TabIndex = 46
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
        Me.Frame3.Location = New System.Drawing.Point(0, 566)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(908, 53)
        Me.Frame3.TabIndex = 45
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 45
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
        Me.lblBookType.TabIndex = 48
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
        Me.lblMKey.TabIndex = 47
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'FrmReworkProdDeptWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.AdoDCMain)
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
        Me.Name = "FrmReworkProdDeptWise"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Rework Production"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.fraRecdDetail.ResumeLayout(False)
        Me.fraRecdDetail.PerformLayout()
        Me.FraRework.ResumeLayout(False)
        Me.FraRework.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
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