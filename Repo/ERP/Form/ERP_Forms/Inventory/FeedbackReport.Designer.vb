Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmFeedbackReport
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

        'InventoryGST.Master.Show
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
    Public WithEvents cboShift As System.Windows.Forms.ComboBox
    Public WithEvents txtQtyInvolved As System.Windows.Forms.TextBox
    Public WithEvents txtDept2Code As System.Windows.Forms.TextBox
    Public WithEvents cmdDept2Search As System.Windows.Forms.Button
    Public WithEvents txtDept2Name As System.Windows.Forms.TextBox
    Public WithEvents txtRaisedBy As System.Windows.Forms.TextBox
    Public WithEvents cmdRaisedSearch As System.Windows.Forms.Button
    Public WithEvents txtRaisedName As System.Windows.Forms.TextBox
    Public WithEvents cboShift2 As System.Windows.Forms.ComboBox
    Public WithEvents txtStatusDate As System.Windows.Forms.TextBox
    Public WithEvents _OptStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents fraStatus As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdMainII As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents txtRecdName As System.Windows.Forms.TextBox
    Public WithEvents txtDeptName As System.Windows.Forms.TextBox
    Public WithEvents txtItemCode As System.Windows.Forms.TextBox
    Public WithEvents txtItemDesc As System.Windows.Forms.TextBox
    Public WithEvents cmdItemCode As System.Windows.Forms.Button
    Public WithEvents cmdRecdSearch As System.Windows.Forms.Button
    Public WithEvents txtRecdBy As System.Windows.Forms.TextBox
    Public WithEvents cmdDeptSearch As System.Windows.Forms.Button
    Public WithEvents txtDocNo As System.Windows.Forms.TextBox
    Public WithEvents txtDeptCode As System.Windows.Forms.TextBox
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
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
    Public WithEvents OptStatus As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFeedbackReport))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdDept2Search = New System.Windows.Forms.Button()
        Me.cmdRaisedSearch = New System.Windows.Forms.Button()
        Me.cmdItemCode = New System.Windows.Forms.Button()
        Me.cmdRecdSearch = New System.Windows.Forms.Button()
        Me.cmdDeptSearch = New System.Windows.Forms.Button()
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
        Me.cboShift = New System.Windows.Forms.ComboBox()
        Me.txtQtyInvolved = New System.Windows.Forms.TextBox()
        Me.txtDept2Code = New System.Windows.Forms.TextBox()
        Me.txtDept2Name = New System.Windows.Forms.TextBox()
        Me.txtRaisedBy = New System.Windows.Forms.TextBox()
        Me.txtRaisedName = New System.Windows.Forms.TextBox()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.cboShift2 = New System.Windows.Forms.ComboBox()
        Me.txtStatusDate = New System.Windows.Forms.TextBox()
        Me.fraStatus = New System.Windows.Forms.GroupBox()
        Me._OptStatus_1 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_0 = New System.Windows.Forms.RadioButton()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdMainII = New AxFPSpreadADO.AxfpSpread()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRecdName = New System.Windows.Forms.TextBox()
        Me.txtDeptName = New System.Windows.Forms.TextBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.txtItemDesc = New System.Windows.Forms.TextBox()
        Me.txtRecdBy = New System.Windows.Forms.TextBox()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.txtDeptCode = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.AdoDCMain = New VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.OptStatus = New VB6.RadioButtonArray(Me.components)
        Me.FraFront.SuspendLayout()
        Me.FraView.SuspendLayout()
        Me.fraStatus.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMainII, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdDept2Search
        '
        Me.cmdDept2Search.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDept2Search.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDept2Search.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDept2Search.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDept2Search.Image = CType(resources.GetObject("cmdDept2Search.Image"), System.Drawing.Image)
        Me.cmdDept2Search.Location = New System.Drawing.Point(203, 120)
        Me.cmdDept2Search.Name = "cmdDept2Search"
        Me.cmdDept2Search.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDept2Search.Size = New System.Drawing.Size(23, 19)
        Me.cmdDept2Search.TabIndex = 37
        Me.cmdDept2Search.TabStop = False
        Me.cmdDept2Search.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDept2Search, "Search")
        Me.cmdDept2Search.UseVisualStyleBackColor = False
        '
        'cmdRaisedSearch
        '
        Me.cmdRaisedSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdRaisedSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRaisedSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRaisedSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRaisedSearch.Image = CType(resources.GetObject("cmdRaisedSearch.Image"), System.Drawing.Image)
        Me.cmdRaisedSearch.Location = New System.Drawing.Point(203, 44)
        Me.cmdRaisedSearch.Name = "cmdRaisedSearch"
        Me.cmdRaisedSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRaisedSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdRaisedSearch.TabIndex = 10
        Me.cmdRaisedSearch.TabStop = False
        Me.cmdRaisedSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdRaisedSearch, "Search")
        Me.cmdRaisedSearch.UseVisualStyleBackColor = False
        '
        'cmdItemCode
        '
        Me.cmdItemCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdItemCode.Enabled = False
        Me.cmdItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdItemCode.Image = CType(resources.GetObject("cmdItemCode.Image"), System.Drawing.Image)
        Me.cmdItemCode.Location = New System.Drawing.Point(202, 146)
        Me.cmdItemCode.Name = "cmdItemCode"
        Me.cmdItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdItemCode.Size = New System.Drawing.Size(23, 19)
        Me.cmdItemCode.TabIndex = 4
        Me.cmdItemCode.TabStop = False
        Me.cmdItemCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdItemCode, "Search")
        Me.cmdItemCode.UseVisualStyleBackColor = False
        '
        'cmdRecdSearch
        '
        Me.cmdRecdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdRecdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRecdSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRecdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRecdSearch.Image = CType(resources.GetObject("cmdRecdSearch.Image"), System.Drawing.Image)
        Me.cmdRecdSearch.Location = New System.Drawing.Point(202, 94)
        Me.cmdRecdSearch.Name = "cmdRecdSearch"
        Me.cmdRecdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRecdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdRecdSearch.TabIndex = 13
        Me.cmdRecdSearch.TabStop = False
        Me.cmdRecdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdRecdSearch, "Search")
        Me.cmdRecdSearch.UseVisualStyleBackColor = False
        '
        'cmdDeptSearch
        '
        Me.cmdDeptSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDeptSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeptSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeptSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeptSearch.Image = CType(resources.GetObject("cmdDeptSearch.Image"), System.Drawing.Image)
        Me.cmdDeptSearch.Location = New System.Drawing.Point(202, 69)
        Me.cmdDeptSearch.Name = "cmdDeptSearch"
        Me.cmdDeptSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeptSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdDeptSearch.TabIndex = 7
        Me.cmdDeptSearch.TabStop = False
        Me.cmdDeptSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDeptSearch, "Search")
        Me.cmdDeptSearch.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(82, 12)
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
        Me.cmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(149, 12)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 16
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(216, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 17
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(283, 12)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 18
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(550, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 22
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(417, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 20
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(484, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 21
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(351, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 19
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(618, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 23
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.cboShift)
        Me.FraFront.Controls.Add(Me.txtQtyInvolved)
        Me.FraFront.Controls.Add(Me.txtDept2Code)
        Me.FraFront.Controls.Add(Me.cmdDept2Search)
        Me.FraFront.Controls.Add(Me.txtDept2Name)
        Me.FraFront.Controls.Add(Me.txtRaisedBy)
        Me.FraFront.Controls.Add(Me.cmdRaisedSearch)
        Me.FraFront.Controls.Add(Me.txtRaisedName)
        Me.FraFront.Controls.Add(Me.FraView)
        Me.FraFront.Controls.Add(Me.txtRecdName)
        Me.FraFront.Controls.Add(Me.txtDeptName)
        Me.FraFront.Controls.Add(Me.txtItemCode)
        Me.FraFront.Controls.Add(Me.txtItemDesc)
        Me.FraFront.Controls.Add(Me.cmdItemCode)
        Me.FraFront.Controls.Add(Me.cmdRecdSearch)
        Me.FraFront.Controls.Add(Me.txtRecdBy)
        Me.FraFront.Controls.Add(Me.cmdDeptSearch)
        Me.FraFront.Controls.Add(Me.txtDocNo)
        Me.FraFront.Controls.Add(Me.txtDeptCode)
        Me.FraFront.Controls.Add(Me.txtDate)
        Me.FraFront.Controls.Add(Me.Label45)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.Label19)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.Label14)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -2)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(751, 415)
        Me.FraFront.TabIndex = 15
        Me.FraFront.TabStop = False
        '
        'cboShift
        '
        Me.cboShift.BackColor = System.Drawing.SystemColors.Window
        Me.cboShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShift.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShift.Location = New System.Drawing.Point(618, 44)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShift.Size = New System.Drawing.Size(129, 22)
        Me.cboShift.TabIndex = 47
        '
        'txtQtyInvolved
        '
        Me.txtQtyInvolved.AcceptsReturn = True
        Me.txtQtyInvolved.BackColor = System.Drawing.SystemColors.Window
        Me.txtQtyInvolved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQtyInvolved.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQtyInvolved.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQtyInvolved.ForeColor = System.Drawing.Color.Blue
        Me.txtQtyInvolved.Location = New System.Drawing.Point(122, 172)
        Me.txtQtyInvolved.MaxLength = 0
        Me.txtQtyInvolved.Name = "txtQtyInvolved"
        Me.txtQtyInvolved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQtyInvolved.Size = New System.Drawing.Size(79, 19)
        Me.txtQtyInvolved.TabIndex = 40
        Me.txtQtyInvolved.Text = " "
        '
        'txtDept2Code
        '
        Me.txtDept2Code.AcceptsReturn = True
        Me.txtDept2Code.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept2Code.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept2Code.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept2Code.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept2Code.ForeColor = System.Drawing.Color.Blue
        Me.txtDept2Code.Location = New System.Drawing.Point(123, 120)
        Me.txtDept2Code.MaxLength = 0
        Me.txtDept2Code.Name = "txtDept2Code"
        Me.txtDept2Code.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept2Code.Size = New System.Drawing.Size(79, 19)
        Me.txtDept2Code.TabIndex = 38
        '
        'txtDept2Name
        '
        Me.txtDept2Name.AcceptsReturn = True
        Me.txtDept2Name.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept2Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept2Name.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept2Name.Enabled = False
        Me.txtDept2Name.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept2Name.ForeColor = System.Drawing.Color.Blue
        Me.txtDept2Name.Location = New System.Drawing.Point(231, 120)
        Me.txtDept2Name.MaxLength = 0
        Me.txtDept2Name.Name = "txtDept2Name"
        Me.txtDept2Name.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept2Name.Size = New System.Drawing.Size(315, 19)
        Me.txtDept2Name.TabIndex = 36
        Me.txtDept2Name.Text = " "
        '
        'txtRaisedBy
        '
        Me.txtRaisedBy.AcceptsReturn = True
        Me.txtRaisedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtRaisedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRaisedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRaisedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRaisedBy.ForeColor = System.Drawing.Color.Blue
        Me.txtRaisedBy.Location = New System.Drawing.Point(123, 44)
        Me.txtRaisedBy.MaxLength = 0
        Me.txtRaisedBy.Name = "txtRaisedBy"
        Me.txtRaisedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRaisedBy.Size = New System.Drawing.Size(79, 19)
        Me.txtRaisedBy.TabIndex = 9
        '
        'txtRaisedName
        '
        Me.txtRaisedName.AcceptsReturn = True
        Me.txtRaisedName.BackColor = System.Drawing.SystemColors.Window
        Me.txtRaisedName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRaisedName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRaisedName.Enabled = False
        Me.txtRaisedName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRaisedName.ForeColor = System.Drawing.Color.Blue
        Me.txtRaisedName.Location = New System.Drawing.Point(231, 44)
        Me.txtRaisedName.MaxLength = 0
        Me.txtRaisedName.Name = "txtRaisedName"
        Me.txtRaisedName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRaisedName.Size = New System.Drawing.Size(315, 19)
        Me.txtRaisedName.TabIndex = 11
        Me.txtRaisedName.Text = " "
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.cboShift2)
        Me.FraView.Controls.Add(Me.txtStatusDate)
        Me.FraView.Controls.Add(Me.fraStatus)
        Me.FraView.Controls.Add(Me.SprdMain)
        Me.FraView.Controls.Add(Me.SprdMainII)
        Me.FraView.Controls.Add(Me.Label5)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, 188)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(751, 227)
        Me.FraView.TabIndex = 32
        Me.FraView.TabStop = False
        '
        'cboShift2
        '
        Me.cboShift2.BackColor = System.Drawing.SystemColors.Window
        Me.cboShift2.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShift2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShift2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShift2.Location = New System.Drawing.Point(624, 200)
        Me.cboShift2.Name = "cboShift2"
        Me.cboShift2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShift2.Size = New System.Drawing.Size(89, 22)
        Me.cboShift2.TabIndex = 49
        '
        'txtStatusDate
        '
        Me.txtStatusDate.AcceptsReturn = True
        Me.txtStatusDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatusDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStatusDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStatusDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatusDate.ForeColor = System.Drawing.Color.Blue
        Me.txtStatusDate.Location = New System.Drawing.Point(278, 200)
        Me.txtStatusDate.MaxLength = 0
        Me.txtStatusDate.Name = "txtStatusDate"
        Me.txtStatusDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusDate.Size = New System.Drawing.Size(89, 19)
        Me.txtStatusDate.TabIndex = 45
        '
        'fraStatus
        '
        Me.fraStatus.BackColor = System.Drawing.SystemColors.Control
        Me.fraStatus.Controls.Add(Me._OptStatus_1)
        Me.fraStatus.Controls.Add(Me._OptStatus_0)
        Me.fraStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraStatus.Location = New System.Drawing.Point(0, 186)
        Me.fraStatus.Name = "fraStatus"
        Me.fraStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.fraStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraStatus.Size = New System.Drawing.Size(145, 41)
        Me.fraStatus.TabIndex = 42
        Me.fraStatus.TabStop = False
        Me.fraStatus.Text = "Status"
        '
        '_OptStatus_1
        '
        Me._OptStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_1, CType(1, Short))
        Me._OptStatus_1.Location = New System.Drawing.Point(74, 17)
        Me._OptStatus_1.Name = "_OptStatus_1"
        Me._OptStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_1.Size = New System.Drawing.Size(67, 18)
        Me._OptStatus_1.TabIndex = 44
        Me._OptStatus_1.TabStop = True
        Me._OptStatus_1.Text = "Close"
        Me._OptStatus_1.UseVisualStyleBackColor = False
        '
        '_OptStatus_0
        '
        Me._OptStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_0.Checked = True
        Me._OptStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_0, CType(0, Short))
        Me._OptStatus_0.Location = New System.Drawing.Point(6, 17)
        Me._OptStatus_0.Name = "_OptStatus_0"
        Me._OptStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_0.Size = New System.Drawing.Size(67, 18)
        Me._OptStatus_0.TabIndex = 43
        Me._OptStatus_0.TabStop = True
        Me._OptStatus_0.Text = "Open"
        Me._OptStatus_0.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(367, 165)
        Me.SprdMain.TabIndex = 34
        '
        'SprdMainII
        '
        Me.SprdMainII.DataSource = Nothing
        Me.SprdMainII.Location = New System.Drawing.Point(380, 8)
        Me.SprdMainII.Name = "SprdMainII"
        Me.SprdMainII.OcxState = CType(resources.GetObject("SprdMainII.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainII.Size = New System.Drawing.Size(367, 165)
        Me.SprdMainII.TabIndex = 35
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Menu
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label5.Location = New System.Drawing.Point(586, 202)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(35, 14)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "Shift :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(154, 202)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(35, 14)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtRecdName
        '
        Me.txtRecdName.AcceptsReturn = True
        Me.txtRecdName.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecdName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecdName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecdName.Enabled = False
        Me.txtRecdName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecdName.ForeColor = System.Drawing.Color.Blue
        Me.txtRecdName.Location = New System.Drawing.Point(230, 94)
        Me.txtRecdName.MaxLength = 0
        Me.txtRecdName.Name = "txtRecdName"
        Me.txtRecdName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecdName.Size = New System.Drawing.Size(315, 19)
        Me.txtRecdName.TabIndex = 14
        Me.txtRecdName.Text = " "
        '
        'txtDeptName
        '
        Me.txtDeptName.AcceptsReturn = True
        Me.txtDeptName.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeptName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeptName.Enabled = False
        Me.txtDeptName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeptName.ForeColor = System.Drawing.Color.Blue
        Me.txtDeptName.Location = New System.Drawing.Point(230, 69)
        Me.txtDeptName.MaxLength = 0
        Me.txtDeptName.Name = "txtDeptName"
        Me.txtDeptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeptName.Size = New System.Drawing.Size(315, 19)
        Me.txtDeptName.TabIndex = 8
        Me.txtDeptName.Text = " "
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Enabled = False
        Me.txtItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.Color.Blue
        Me.txtItemCode.Location = New System.Drawing.Point(122, 146)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(79, 19)
        Me.txtItemCode.TabIndex = 3
        Me.txtItemCode.Text = " "
        '
        'txtItemDesc
        '
        Me.txtItemDesc.AcceptsReturn = True
        Me.txtItemDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemDesc.Enabled = False
        Me.txtItemDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDesc.ForeColor = System.Drawing.Color.Blue
        Me.txtItemDesc.Location = New System.Drawing.Point(230, 146)
        Me.txtItemDesc.MaxLength = 0
        Me.txtItemDesc.Name = "txtItemDesc"
        Me.txtItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemDesc.Size = New System.Drawing.Size(315, 19)
        Me.txtItemDesc.TabIndex = 5
        Me.txtItemDesc.Text = " "
        '
        'txtRecdBy
        '
        Me.txtRecdBy.AcceptsReturn = True
        Me.txtRecdBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecdBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecdBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecdBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecdBy.ForeColor = System.Drawing.Color.Blue
        Me.txtRecdBy.Location = New System.Drawing.Point(122, 94)
        Me.txtRecdBy.MaxLength = 0
        Me.txtRecdBy.Name = "txtRecdBy"
        Me.txtRecdBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecdBy.Size = New System.Drawing.Size(79, 19)
        Me.txtRecdBy.TabIndex = 12
        '
        'txtDocNo
        '
        Me.txtDocNo.AcceptsReturn = True
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.ForeColor = System.Drawing.Color.Blue
        Me.txtDocNo.Location = New System.Drawing.Point(122, 18)
        Me.txtDocNo.MaxLength = 0
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocNo.Size = New System.Drawing.Size(79, 19)
        Me.txtDocNo.TabIndex = 1
        '
        'txtDeptCode
        '
        Me.txtDeptCode.AcceptsReturn = True
        Me.txtDeptCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeptCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeptCode.ForeColor = System.Drawing.Color.Blue
        Me.txtDeptCode.Location = New System.Drawing.Point(122, 69)
        Me.txtDeptCode.MaxLength = 0
        Me.txtDeptCode.Name = "txtDeptCode"
        Me.txtDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeptCode.Size = New System.Drawing.Size(79, 19)
        Me.txtDeptCode.TabIndex = 6
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.Blue
        Me.txtDate.Location = New System.Drawing.Point(456, 18)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(89, 19)
        Me.txtDate.TabIndex = 2
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Menu
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label45.Location = New System.Drawing.Point(580, 46)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(35, 14)
        Me.Label45.TabIndex = 48
        Me.Label45.Text = "Shift :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(39, 174)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(73, 14)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Qty Involved :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(52, 121)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(63, 14)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Dept Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(55, 46)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(62, 14)
        Me.Label19.TabIndex = 33
        Me.Label19.Text = "Raised By :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(63, 96)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(54, 14)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Recd By :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(54, 148)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(60, 14)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "Item Code :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(41, 18)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(72, 14)
        Me.lblCust.TabIndex = 29
        Me.lblCust.Text = "Doc Number :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(51, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(63, 14)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Dept Code :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(332, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(35, 14)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AdoDCMain
        '
        Me.AdoDCMain.BackColor = System.Drawing.SystemColors.Window
        Me.AdoDCMain.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AdoDCMain.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AdoDCMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdoDCMain.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AdoDCMain.Location = New System.Drawing.Point(56, 270)
        Me.AdoDCMain.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AdoDCMain.Name = "AdoDCMain"
        Me.AdoDCMain.Size = New System.Drawing.Size(313, 33)
        Me.AdoDCMain.TabIndex = 16
        Me.AdoDCMain.Text = "Adodc1"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(751, 409)
        Me.SprdView.TabIndex = 25
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
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 404)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(751, 53)
        Me.Frame3.TabIndex = 24
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 24
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(52, 14)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 27
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'OptStatus
        '
        '
        'FrmFeedbackReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(751, 458)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.AdoDCMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFeedbackReport"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Feedback Report"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.fraStatus.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMainII, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).EndInit()
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