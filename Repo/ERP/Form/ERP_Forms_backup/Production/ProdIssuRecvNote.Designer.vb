Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmProdIssuRecvNote
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
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtEntryDate As System.Windows.Forms.TextBox
    Public WithEvents txtRefTM As System.Windows.Forms.TextBox
    Public WithEvents txtPSNo As System.Windows.Forms.TextBox
    Public WithEvents cmdPopulate As System.Windows.Forms.Button
    Public WithEvents txtIssueEmp As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchIssueEmp As System.Windows.Forms.Button
    Public WithEvents txtFromDept As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchFromDept As System.Windows.Forms.Button
    Public WithEvents cmdSearchRecvEmp As System.Windows.Forms.Button
    Public WithEvents cmdSearchCC As System.Windows.Forms.Button
    Public WithEvents cmdSearchToDept As System.Windows.Forms.Button
    Public WithEvents txtIssueNo As System.Windows.Forms.TextBox
    Public WithEvents txtToDept As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtCost As System.Windows.Forms.TextBox
    Public WithEvents txtRecvEmp As System.Windows.Forms.TextBox
    Public WithEvents cboShiftcd As System.Windows.Forms.ComboBox
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents chkReceive As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblIssueEmp As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblFromDept As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblRecvEmp As System.Windows.Forms.Label
    Public WithEvents lblCostctr As System.Windows.Forms.Label
    Public WithEvents lblToDept As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
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
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProdIssuRecvNote))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchIssueEmp = New System.Windows.Forms.Button()
        Me.cmdSearchFromDept = New System.Windows.Forms.Button()
        Me.cmdSearchRecvEmp = New System.Windows.Forms.Button()
        Me.cmdSearchCC = New System.Windows.Forms.Button()
        Me.cmdSearchToDept = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtEntryDate = New System.Windows.Forms.TextBox()
        Me.txtRefTM = New System.Windows.Forms.TextBox()
        Me.txtPSNo = New System.Windows.Forms.TextBox()
        Me.cmdPopulate = New System.Windows.Forms.Button()
        Me.txtIssueEmp = New System.Windows.Forms.TextBox()
        Me.txtFromDept = New System.Windows.Forms.TextBox()
        Me.txtIssueNo = New System.Windows.Forms.TextBox()
        Me.txtToDept = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtCost = New System.Windows.Forms.TextBox()
        Me.txtRecvEmp = New System.Windows.Forms.TextBox()
        Me.cboShiftcd = New System.Windows.Forms.ComboBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.chkReceive = New System.Windows.Forms.CheckBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblIssueEmp = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblFromDept = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblRecvEmp = New System.Windows.Forms.Label()
        Me.lblCostctr = New System.Windows.Forms.Label()
        Me.lblToDept = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.AdoDCMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.txtCPNo = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchIssueEmp
        '
        Me.cmdSearchIssueEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchIssueEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchIssueEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchIssueEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchIssueEmp.Image = CType(resources.GetObject("cmdSearchIssueEmp.Image"), System.Drawing.Image)
        Me.cmdSearchIssueEmp.Location = New System.Drawing.Point(210, 104)
        Me.cmdSearchIssueEmp.Name = "cmdSearchIssueEmp"
        Me.cmdSearchIssueEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchIssueEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchIssueEmp.TabIndex = 12
        Me.cmdSearchIssueEmp.TabStop = False
        Me.cmdSearchIssueEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchIssueEmp, "Search")
        Me.cmdSearchIssueEmp.UseVisualStyleBackColor = False
        '
        'cmdSearchFromDept
        '
        Me.cmdSearchFromDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchFromDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchFromDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchFromDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchFromDept.Image = CType(resources.GetObject("cmdSearchFromDept.Image"), System.Drawing.Image)
        Me.cmdSearchFromDept.Location = New System.Drawing.Point(210, 56)
        Me.cmdSearchFromDept.Name = "cmdSearchFromDept"
        Me.cmdSearchFromDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchFromDept.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchFromDept.TabIndex = 7
        Me.cmdSearchFromDept.TabStop = False
        Me.cmdSearchFromDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchFromDept, "Search")
        Me.cmdSearchFromDept.UseVisualStyleBackColor = False
        '
        'cmdSearchRecvEmp
        '
        Me.cmdSearchRecvEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchRecvEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchRecvEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchRecvEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchRecvEmp.Image = CType(resources.GetObject("cmdSearchRecvEmp.Image"), System.Drawing.Image)
        Me.cmdSearchRecvEmp.Location = New System.Drawing.Point(210, 127)
        Me.cmdSearchRecvEmp.Name = "cmdSearchRecvEmp"
        Me.cmdSearchRecvEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchRecvEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchRecvEmp.TabIndex = 14
        Me.cmdSearchRecvEmp.TabStop = False
        Me.cmdSearchRecvEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchRecvEmp, "Search")
        Me.cmdSearchRecvEmp.UseVisualStyleBackColor = False
        '
        'cmdSearchCC
        '
        Me.cmdSearchCC.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCC.Image = CType(resources.GetObject("cmdSearchCC.Image"), System.Drawing.Image)
        Me.cmdSearchCC.Location = New System.Drawing.Point(360, 151)
        Me.cmdSearchCC.Name = "cmdSearchCC"
        Me.cmdSearchCC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCC.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchCC.TabIndex = 17
        Me.cmdSearchCC.TabStop = False
        Me.cmdSearchCC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCC, "Search")
        Me.cmdSearchCC.UseVisualStyleBackColor = False
        '
        'cmdSearchToDept
        '
        Me.cmdSearchToDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchToDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchToDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchToDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchToDept.Image = CType(resources.GetObject("cmdSearchToDept.Image"), System.Drawing.Image)
        Me.cmdSearchToDept.Location = New System.Drawing.Point(210, 80)
        Me.cmdSearchToDept.Name = "cmdSearchToDept"
        Me.cmdSearchToDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchToDept.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchToDept.TabIndex = 10
        Me.cmdSearchToDept.TabStop = False
        Me.cmdSearchToDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchToDept, "Search")
        Me.cmdSearchToDept.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(210, 10)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(82, 12)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(77, 37)
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
        Me.cmdModify.Location = New System.Drawing.Point(157, 12)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(77, 37)
        Me.cmdModify.TabIndex = 24
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
        Me.cmdSave.Location = New System.Drawing.Point(232, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(77, 37)
        Me.cmdSave.TabIndex = 25
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
        Me.cmdDelete.Location = New System.Drawing.Point(307, 12)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(77, 37)
        Me.cmdDelete.TabIndex = 26
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
        Me.CmdView.Location = New System.Drawing.Point(607, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(77, 37)
        Me.CmdView.TabIndex = 30
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
        Me.cmdPrint.Location = New System.Drawing.Point(457, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(77, 37)
        Me.cmdPrint.TabIndex = 28
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
        Me.CmdPreview.Location = New System.Drawing.Point(532, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(77, 37)
        Me.CmdPreview.TabIndex = 29
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(382, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(77, 37)
        Me.cmdSavePrint.TabIndex = 27
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
        Me.cmdClose.Location = New System.Drawing.Point(682, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(77, 37)
        Me.cmdClose.TabIndex = 31
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.txtCPNo)
        Me.FraFront.Controls.Add(Me.Label13)
        Me.FraFront.Controls.Add(Me.Button1)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.txtEntryDate)
        Me.FraFront.Controls.Add(Me.txtRefTM)
        Me.FraFront.Controls.Add(Me.txtPSNo)
        Me.FraFront.Controls.Add(Me.cmdPopulate)
        Me.FraFront.Controls.Add(Me.txtIssueEmp)
        Me.FraFront.Controls.Add(Me.cmdSearchIssueEmp)
        Me.FraFront.Controls.Add(Me.txtFromDept)
        Me.FraFront.Controls.Add(Me.cmdSearchFromDept)
        Me.FraFront.Controls.Add(Me.cmdSearchRecvEmp)
        Me.FraFront.Controls.Add(Me.cmdSearchCC)
        Me.FraFront.Controls.Add(Me.cmdSearchToDept)
        Me.FraFront.Controls.Add(Me.txtIssueNo)
        Me.FraFront.Controls.Add(Me.txtToDept)
        Me.FraFront.Controls.Add(Me.txtRemarks)
        Me.FraFront.Controls.Add(Me.txtCost)
        Me.FraFront.Controls.Add(Me.txtRecvEmp)
        Me.FraFront.Controls.Add(Me.cboShiftcd)
        Me.FraFront.Controls.Add(Me.txtDate)
        Me.FraFront.Controls.Add(Me.chkReceive)
        Me.FraFront.Controls.Add(Me.cmdSearch)
        Me.FraFront.Controls.Add(Me.SprdMain)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.Label11)
        Me.FraFront.Controls.Add(Me.Label10)
        Me.FraFront.Controls.Add(Me.lblIssueEmp)
        Me.FraFront.Controls.Add(Me.Label9)
        Me.FraFront.Controls.Add(Me.lblFromDept)
        Me.FraFront.Controls.Add(Me.lblBookType)
        Me.FraFront.Controls.Add(Me.lblRecvEmp)
        Me.FraFront.Controls.Add(Me.lblCostctr)
        Me.FraFront.Controls.Add(Me.lblToDept)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(907, 573)
        Me.FraFront.TabIndex = 34
        Me.FraFront.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.Location = New System.Drawing.Point(552, 148)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(180, 26)
        Me.Button1.TabIndex = 54
        Me.Button1.Text = "From BOM"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(104, 32)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(231, 22)
        Me.cboDivision.TabIndex = 5
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
        Me.txtEntryDate.Location = New System.Drawing.Point(577, 56)
        Me.txtEntryDate.MaxLength = 0
        Me.txtEntryDate.Multiline = True
        Me.txtEntryDate.Name = "txtEntryDate"
        Me.txtEntryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEntryDate.Size = New System.Drawing.Size(167, 37)
        Me.txtEntryDate.TabIndex = 51
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
        Me.txtRefTM.Location = New System.Drawing.Point(702, 10)
        Me.txtRefTM.MaxLength = 0
        Me.txtRefTM.Name = "txtRefTM"
        Me.txtRefTM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefTM.Size = New System.Drawing.Size(43, 20)
        Me.txtRefTM.TabIndex = 50
        '
        'txtPSNo
        '
        Me.txtPSNo.AcceptsReturn = True
        Me.txtPSNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPSNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPSNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPSNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPSNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPSNo.Location = New System.Drawing.Point(611, 96)
        Me.txtPSNo.MaxLength = 0
        Me.txtPSNo.Name = "txtPSNo"
        Me.txtPSNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPSNo.Size = New System.Drawing.Size(92, 20)
        Me.txtPSNo.TabIndex = 8
        '
        'cmdPopulate
        '
        Me.cmdPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulate.Location = New System.Drawing.Point(552, 122)
        Me.cmdPopulate.Name = "cmdPopulate"
        Me.cmdPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulate.Size = New System.Drawing.Size(180, 26)
        Me.cmdPopulate.TabIndex = 20
        Me.cmdPopulate.Text = "Populate From Production Plan"
        Me.cmdPopulate.UseVisualStyleBackColor = False
        '
        'txtIssueEmp
        '
        Me.txtIssueEmp.AcceptsReturn = True
        Me.txtIssueEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtIssueEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssueEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIssueEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssueEmp.ForeColor = System.Drawing.Color.Blue
        Me.txtIssueEmp.Location = New System.Drawing.Point(104, 104)
        Me.txtIssueEmp.MaxLength = 0
        Me.txtIssueEmp.Name = "txtIssueEmp"
        Me.txtIssueEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIssueEmp.Size = New System.Drawing.Size(105, 20)
        Me.txtIssueEmp.TabIndex = 11
        '
        'txtFromDept
        '
        Me.txtFromDept.AcceptsReturn = True
        Me.txtFromDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtFromDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFromDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDept.ForeColor = System.Drawing.Color.Blue
        Me.txtFromDept.Location = New System.Drawing.Point(104, 56)
        Me.txtFromDept.MaxLength = 0
        Me.txtFromDept.Name = "txtFromDept"
        Me.txtFromDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFromDept.Size = New System.Drawing.Size(105, 20)
        Me.txtFromDept.TabIndex = 6
        '
        'txtIssueNo
        '
        Me.txtIssueNo.AcceptsReturn = True
        Me.txtIssueNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtIssueNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssueNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIssueNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssueNo.ForeColor = System.Drawing.Color.Blue
        Me.txtIssueNo.Location = New System.Drawing.Point(104, 10)
        Me.txtIssueNo.MaxLength = 0
        Me.txtIssueNo.Name = "txtIssueNo"
        Me.txtIssueNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIssueNo.Size = New System.Drawing.Size(105, 20)
        Me.txtIssueNo.TabIndex = 1
        '
        'txtToDept
        '
        Me.txtToDept.AcceptsReturn = True
        Me.txtToDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtToDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDept.ForeColor = System.Drawing.Color.Blue
        Me.txtToDept.Location = New System.Drawing.Point(104, 80)
        Me.txtToDept.MaxLength = 0
        Me.txtToDept.Name = "txtToDept"
        Me.txtToDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToDept.Size = New System.Drawing.Size(105, 20)
        Me.txtToDept.TabIndex = 9
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtRemarks.Location = New System.Drawing.Point(104, 177)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(483, 20)
        Me.txtRemarks.TabIndex = 18
        '
        'txtCost
        '
        Me.txtCost.AcceptsReturn = True
        Me.txtCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost.ForeColor = System.Drawing.Color.Blue
        Me.txtCost.Location = New System.Drawing.Point(308, 151)
        Me.txtCost.MaxLength = 0
        Me.txtCost.Name = "txtCost"
        Me.txtCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCost.Size = New System.Drawing.Size(50, 20)
        Me.txtCost.TabIndex = 16
        '
        'txtRecvEmp
        '
        Me.txtRecvEmp.AcceptsReturn = True
        Me.txtRecvEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecvEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecvEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecvEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecvEmp.ForeColor = System.Drawing.Color.Blue
        Me.txtRecvEmp.Location = New System.Drawing.Point(104, 127)
        Me.txtRecvEmp.MaxLength = 0
        Me.txtRecvEmp.Name = "txtRecvEmp"
        Me.txtRecvEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecvEmp.Size = New System.Drawing.Size(105, 20)
        Me.txtRecvEmp.TabIndex = 13
        '
        'cboShiftcd
        '
        Me.cboShiftcd.BackColor = System.Drawing.SystemColors.Window
        Me.cboShiftcd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShiftcd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShiftcd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShiftcd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShiftcd.Location = New System.Drawing.Point(104, 151)
        Me.cboShiftcd.Name = "cboShiftcd"
        Me.cboShiftcd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShiftcd.Size = New System.Drawing.Size(105, 22)
        Me.cboShiftcd.TabIndex = 15
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.Blue
        Me.txtDate.Location = New System.Drawing.Point(626, 10)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(75, 20)
        Me.txtDate.TabIndex = 4
        '
        'chkReceive
        '
        Me.chkReceive.BackColor = System.Drawing.SystemColors.Control
        Me.chkReceive.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkReceive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReceive.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReceive.Location = New System.Drawing.Point(426, 10)
        Me.chkReceive.Name = "chkReceive"
        Me.chkReceive.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkReceive.Size = New System.Drawing.Size(73, 19)
        Me.chkReceive.TabIndex = 3
        Me.chkReceive.Text = "(Yes/No)"
        Me.chkReceive.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 198)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(902, 367)
        Me.SprdMain.TabIndex = 19
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(42, 36)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(50, 14)
        Me.Label12.TabIndex = 53
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
        Me.Label8.Location = New System.Drawing.Point(505, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(63, 14)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Entry Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(508, 99)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(100, 14)
        Me.Label11.TabIndex = 49
        Me.Label11.Text = "Production Slip No :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(24, 109)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(68, 14)
        Me.Label10.TabIndex = 48
        Me.Label10.Text = "Issued Emp :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblIssueEmp
        '
        Me.lblIssueEmp.BackColor = System.Drawing.SystemColors.Control
        Me.lblIssueEmp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIssueEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIssueEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIssueEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIssueEmp.Location = New System.Drawing.Point(236, 104)
        Me.lblIssueEmp.Name = "lblIssueEmp"
        Me.lblIssueEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIssueEmp.Size = New System.Drawing.Size(265, 19)
        Me.lblIssueEmp.TabIndex = 47
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(30, 61)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(62, 14)
        Me.Label9.TabIndex = 46
        Me.Label9.Text = "From Dept :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFromDept
        '
        Me.lblFromDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblFromDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFromDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFromDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFromDept.Location = New System.Drawing.Point(236, 56)
        Me.lblFromDept.Name = "lblFromDept"
        Me.lblFromDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFromDept.Size = New System.Drawing.Size(265, 19)
        Me.lblFromDept.TabIndex = 45
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(508, 104)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(64, 14)
        Me.lblBookType.TabIndex = 44
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblRecvEmp
        '
        Me.lblRecvEmp.BackColor = System.Drawing.SystemColors.Control
        Me.lblRecvEmp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRecvEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRecvEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecvEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRecvEmp.Location = New System.Drawing.Point(236, 127)
        Me.lblRecvEmp.Name = "lblRecvEmp"
        Me.lblRecvEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRecvEmp.Size = New System.Drawing.Size(265, 19)
        Me.lblRecvEmp.TabIndex = 22
        '
        'lblCostctr
        '
        Me.lblCostctr.BackColor = System.Drawing.SystemColors.Control
        Me.lblCostctr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCostctr.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCostctr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostctr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCostctr.Location = New System.Drawing.Point(384, 151)
        Me.lblCostctr.Name = "lblCostctr"
        Me.lblCostctr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCostctr.Size = New System.Drawing.Size(116, 19)
        Me.lblCostctr.TabIndex = 23
        '
        'lblToDept
        '
        Me.lblToDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblToDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblToDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblToDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblToDept.Location = New System.Drawing.Point(236, 80)
        Me.lblToDept.Name = "lblToDept"
        Me.lblToDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblToDept.Size = New System.Drawing.Size(265, 19)
        Me.lblToDept.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(585, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(35, 14)
        Me.Label7.TabIndex = 42
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
        Me.Label6.Location = New System.Drawing.Point(290, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(105, 14)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "Received Completed"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(29, 156)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 14)
        Me.Label5.TabIndex = 40
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
        Me.Label4.Location = New System.Drawing.Point(37, 180)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(55, 14)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Remarks :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(236, 153)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(70, 14)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Cost Center :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(11, 132)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(81, 14)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Received Emp :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(43, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(49, 14)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "To Dept :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(13, 14)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(79, 14)
        Me.lblCust.TabIndex = 35
        Me.lblCust.Text = "Issue Number :"
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
        Me.AdoDCMain.TabIndex = 35
        Me.AdoDCMain.Text = "Adodc1"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(907, 567)
        Me.SprdView.TabIndex = 33
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
        Me.Frame3.Controls.Add(Me.lblMKey)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 565)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(907, 53)
        Me.Frame3.TabIndex = 32
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 32
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
        Me.lblMKey.TabIndex = 43
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'txtCPNo
        '
        Me.txtCPNo.AcceptsReturn = True
        Me.txtCPNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCPNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCPNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCPNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCPNo.ForeColor = System.Drawing.Color.Blue
        Me.txtCPNo.Location = New System.Drawing.Point(812, 96)
        Me.txtCPNo.MaxLength = 0
        Me.txtCPNo.Name = "txtCPNo"
        Me.txtCPNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCPNo.Size = New System.Drawing.Size(92, 20)
        Me.txtCPNo.TabIndex = 55
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(709, 99)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(85, 14)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "Cutting Plan No :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FrmProdIssuRecvNote
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
        Me.Name = "FrmProdIssuRecvNote"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Material Movement Issue Note"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
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

    Public WithEvents Button1 As Button
    Public WithEvents txtCPNo As TextBox
    Public WithEvents Label13 As Label
#End Region
End Class