Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmLeaveRequisitionEntry
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
        'Me.MDIParent = Payroll.Master
        'Payroll.Master.Show
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
	Public WithEvents chkHalfDay As System.Windows.Forms.CheckBox
	Public WithEvents _optHRStatus_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optHRStatus_1 As System.Windows.Forms.RadioButton
	Public WithEvents FraHRStatus As System.Windows.Forms.GroupBox
	Public WithEvents _optStatus_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optStatus_0 As System.Windows.Forms.RadioButton
	Public WithEvents FraAppStatus As System.Windows.Forms.GroupBox
	Public WithEvents txtAppEmpCode As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchAppEmp As System.Windows.Forms.Button
	Public WithEvents txtRecEmpCode As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchRecEmp As System.Windows.Forms.Button
	Public WithEvents txtDays As System.Windows.Forms.TextBox
	Public WithEvents txtDateTo As System.Windows.Forms.TextBox
	Public WithEvents txtDateFrom As System.Windows.Forms.TextBox
	Public WithEvents cmdSearch As System.Windows.Forms.Button
	Public WithEvents txtReqDate As System.Windows.Forms.TextBox
	Public WithEvents txtEmp As System.Windows.Forms.TextBox
	Public WithEvents txtReason As System.Windows.Forms.TextBox
	Public WithEvents txtReqNo As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchEmp As System.Windows.Forms.Button
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents lblToeMailID As System.Windows.Forms.Label
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents lblAppEmpName As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents lblRecEmpName As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents lblDesgName As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents lblFromeMailID As System.Windows.Forms.Label
	Public WithEvents lblCust As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents lblDeptname As System.Windows.Forms.Label
	Public WithEvents lblEmpname As System.Windows.Forms.Label
	Public WithEvents lblBookType As System.Windows.Forms.Label
	Public WithEvents FraFront As System.Windows.Forms.GroupBox
	Public WithEvents AdoDCMain As VB6.ADODC
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents cmdDelete As System.Windows.Forms.Button
	Public WithEvents cmdSave As System.Windows.Forms.Button
	Public WithEvents cmdModify As System.Windows.Forms.Button
	Public WithEvents cmdAdd As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents lblMKey As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents optHRStatus As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optStatus As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLeaveRequisitionEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchAppEmp = New System.Windows.Forms.Button()
        Me.cmdSearchRecEmp = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdSearchEmp = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.lblAvlCPL = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblAvlSL = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblAvlEL = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblAvlCL = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.lblBalCPL = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblBalCL = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblBalEL = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblBalSL = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.chkHalfDay = New System.Windows.Forms.CheckBox()
        Me.FraHRStatus = New System.Windows.Forms.GroupBox()
        Me._optHRStatus_0 = New System.Windows.Forms.RadioButton()
        Me._optHRStatus_1 = New System.Windows.Forms.RadioButton()
        Me.FraAppStatus = New System.Windows.Forms.GroupBox()
        Me._optStatus_1 = New System.Windows.Forms.RadioButton()
        Me._optStatus_0 = New System.Windows.Forms.RadioButton()
        Me.txtAppEmpCode = New System.Windows.Forms.TextBox()
        Me.txtRecEmpCode = New System.Windows.Forms.TextBox()
        Me.txtDays = New System.Windows.Forms.TextBox()
        Me.txtDateTo = New System.Windows.Forms.TextBox()
        Me.txtDateFrom = New System.Windows.Forms.TextBox()
        Me.txtReqDate = New System.Windows.Forms.TextBox()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.txtReqNo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblToeMailID = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblAppEmpName = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblRecEmpName = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblDesgName = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblFromeMailID = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblDeptname = New System.Windows.Forms.Label()
        Me.lblEmpname = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.AdoDCMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.optHRStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraFront.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraHRStatus.SuspendLayout()
        Me.FraAppStatus.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optHRStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchAppEmp
        '
        Me.cmdSearchAppEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAppEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAppEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAppEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAppEmp.Image = CType(resources.GetObject("cmdSearchAppEmp.Image"), System.Drawing.Image)
        Me.cmdSearchAppEmp.Location = New System.Drawing.Point(224, 347)
        Me.cmdSearchAppEmp.Name = "cmdSearchAppEmp"
        Me.cmdSearchAppEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAppEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchAppEmp.TabIndex = 13
        Me.cmdSearchAppEmp.TabStop = False
        Me.cmdSearchAppEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAppEmp, "Search")
        Me.cmdSearchAppEmp.UseVisualStyleBackColor = False
        '
        'cmdSearchRecEmp
        '
        Me.cmdSearchRecEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchRecEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchRecEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchRecEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchRecEmp.Image = CType(resources.GetObject("cmdSearchRecEmp.Image"), System.Drawing.Image)
        Me.cmdSearchRecEmp.Location = New System.Drawing.Point(224, 325)
        Me.cmdSearchRecEmp.Name = "cmdSearchRecEmp"
        Me.cmdSearchRecEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchRecEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchRecEmp.TabIndex = 11
        Me.cmdSearchRecEmp.TabStop = False
        Me.cmdSearchRecEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchRecEmp, "Search")
        Me.cmdSearchRecEmp.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(222, 18)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdSearchEmp
        '
        Me.cmdSearchEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEmp.Image = CType(resources.GetObject("cmdSearchEmp.Image"), System.Drawing.Image)
        Me.cmdSearchEmp.Location = New System.Drawing.Point(222, 41)
        Me.cmdSearchEmp.Name = "cmdSearchEmp"
        Me.cmdSearchEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchEmp.TabIndex = 5
        Me.cmdSearchEmp.TabStop = False
        Me.cmdSearchEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchEmp, "Search")
        Me.cmdSearchEmp.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(472, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 28
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
        Me.CmdView.Location = New System.Drawing.Point(406, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 27
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
        Me.CmdPreview.Location = New System.Drawing.Point(340, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 26
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
        Me.cmdPrint.Location = New System.Drawing.Point(273, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 25
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(273, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 24
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(207, 12)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 23
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
        Me.cmdSave.Location = New System.Drawing.Point(140, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 22
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
        Me.cmdModify.Location = New System.Drawing.Point(73, 12)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 21
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
        Me.cmdAdd.Location = New System.Drawing.Point(6, 12)
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
        Me.FraFront.Controls.Add(Me.Frame2)
        Me.FraFront.Controls.Add(Me.Frame1)
        Me.FraFront.Controls.Add(Me.chkHalfDay)
        Me.FraFront.Controls.Add(Me.FraHRStatus)
        Me.FraFront.Controls.Add(Me.FraAppStatus)
        Me.FraFront.Controls.Add(Me.txtAppEmpCode)
        Me.FraFront.Controls.Add(Me.cmdSearchAppEmp)
        Me.FraFront.Controls.Add(Me.txtRecEmpCode)
        Me.FraFront.Controls.Add(Me.cmdSearchRecEmp)
        Me.FraFront.Controls.Add(Me.txtDays)
        Me.FraFront.Controls.Add(Me.txtDateTo)
        Me.FraFront.Controls.Add(Me.txtDateFrom)
        Me.FraFront.Controls.Add(Me.cmdSearch)
        Me.FraFront.Controls.Add(Me.txtReqDate)
        Me.FraFront.Controls.Add(Me.txtEmp)
        Me.FraFront.Controls.Add(Me.txtReason)
        Me.FraFront.Controls.Add(Me.txtReqNo)
        Me.FraFront.Controls.Add(Me.cmdSearchEmp)
        Me.FraFront.Controls.Add(Me.Label19)
        Me.FraFront.Controls.Add(Me.lblToeMailID)
        Me.FraFront.Controls.Add(Me.Label17)
        Me.FraFront.Controls.Add(Me.lblAppEmpName)
        Me.FraFront.Controls.Add(Me.Label15)
        Me.FraFront.Controls.Add(Me.lblRecEmpName)
        Me.FraFront.Controls.Add(Me.Label13)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label11)
        Me.FraFront.Controls.Add(Me.Label10)
        Me.FraFront.Controls.Add(Me.lblDesgName)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.lblFromeMailID)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.lblDeptname)
        Me.FraFront.Controls.Add(Me.lblEmpname)
        Me.FraFront.Controls.Add(Me.lblBookType)
        Me.FraFront.Controls.Add(Me.SprdMain)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(-3, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(545, 511)
        Me.FraFront.TabIndex = 32
        Me.FraFront.TabStop = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lblAvlCPL)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Controls.Add(Me.lblAvlSL)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.lblAvlEL)
        Me.Frame2.Controls.Add(Me.Label9)
        Me.Frame2.Controls.Add(Me.lblAvlCL)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(275, 469)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(268, 41)
        Me.Frame2.TabIndex = 55
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Leave Availed (Till Month)"
        '
        'lblAvlCPL
        '
        Me.lblAvlCPL.BackColor = System.Drawing.SystemColors.Control
        Me.lblAvlCPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlCPL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlCPL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlCPL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlCPL.Location = New System.Drawing.Point(230, 16)
        Me.lblAvlCPL.Name = "lblAvlCPL"
        Me.lblAvlCPL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlCPL.Size = New System.Drawing.Size(29, 15)
        Me.lblAvlCPL.TabIndex = 38
        Me.lblAvlCPL.Text = "0"
        Me.lblAvlCPL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(195, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "CPL :"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(3, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(27, 13)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "CL :"
        '
        'lblAvlSL
        '
        Me.lblAvlSL.BackColor = System.Drawing.SystemColors.Control
        Me.lblAvlSL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlSL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlSL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlSL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlSL.Location = New System.Drawing.Point(92, 16)
        Me.lblAvlSL.Name = "lblAvlSL"
        Me.lblAvlSL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlSL.Size = New System.Drawing.Size(29, 15)
        Me.lblAvlSL.TabIndex = 35
        Me.lblAvlSL.Text = "0"
        Me.lblAvlSL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(65, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(27, 13)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "SL :"
        '
        'lblAvlEL
        '
        Me.lblAvlEL.BackColor = System.Drawing.SystemColors.Control
        Me.lblAvlEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlEL.Location = New System.Drawing.Point(154, 16)
        Me.lblAvlEL.Name = "lblAvlEL"
        Me.lblAvlEL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlEL.Size = New System.Drawing.Size(37, 15)
        Me.lblAvlEL.TabIndex = 33
        Me.lblAvlEL.Text = "0"
        Me.lblAvlEL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(127, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(27, 13)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "EL :"
        '
        'lblAvlCL
        '
        Me.lblAvlCL.BackColor = System.Drawing.SystemColors.Control
        Me.lblAvlCL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlCL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlCL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlCL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlCL.Location = New System.Drawing.Point(30, 16)
        Me.lblAvlCL.Name = "lblAvlCL"
        Me.lblAvlCL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlCL.Size = New System.Drawing.Size(29, 15)
        Me.lblAvlCL.TabIndex = 31
        Me.lblAvlCL.Text = "0"
        Me.lblAvlCL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.lblBalCPL)
        Me.Frame1.Controls.Add(Me.Label14)
        Me.Frame1.Controls.Add(Me.lblBalCL)
        Me.Frame1.Controls.Add(Me.Label16)
        Me.Frame1.Controls.Add(Me.lblBalEL)
        Me.Frame1.Controls.Add(Me.Label18)
        Me.Frame1.Controls.Add(Me.lblBalSL)
        Me.Frame1.Controls.Add(Me.Label20)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(1, 469)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(273, 41)
        Me.Frame1.TabIndex = 54
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Balance Leave (Including This Month)"
        '
        'lblBalCPL
        '
        Me.lblBalCPL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalCPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalCPL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalCPL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalCPL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalCPL.Location = New System.Drawing.Point(236, 16)
        Me.lblBalCPL.Name = "lblBalCPL"
        Me.lblBalCPL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalCPL.Size = New System.Drawing.Size(29, 15)
        Me.lblBalCPL.TabIndex = 29
        Me.lblBalCPL.Text = "0"
        Me.lblBalCPL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(201, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(32, 14)
        Me.Label14.TabIndex = 28
        Me.Label14.Text = "CPL :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBalCL
        '
        Me.lblBalCL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalCL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalCL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalCL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalCL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalCL.Location = New System.Drawing.Point(30, 16)
        Me.lblBalCL.Name = "lblBalCL"
        Me.lblBalCL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalCL.Size = New System.Drawing.Size(29, 15)
        Me.lblBalCL.TabIndex = 27
        Me.lblBalCL.Text = "0"
        Me.lblBalCL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(127, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(27, 13)
        Me.Label16.TabIndex = 26
        Me.Label16.Text = "EL :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBalEL
        '
        Me.lblBalEL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalEL.Location = New System.Drawing.Point(154, 16)
        Me.lblBalEL.Name = "lblBalEL"
        Me.lblBalEL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalEL.Size = New System.Drawing.Size(39, 15)
        Me.lblBalEL.TabIndex = 25
        Me.lblBalEL.Text = "0"
        Me.lblBalEL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(65, 16)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(27, 13)
        Me.Label18.TabIndex = 24
        Me.Label18.Text = "SL :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBalSL
        '
        Me.lblBalSL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalSL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalSL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalSL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalSL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalSL.Location = New System.Drawing.Point(92, 16)
        Me.lblBalSL.Name = "lblBalSL"
        Me.lblBalSL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalSL.Size = New System.Drawing.Size(29, 15)
        Me.lblBalSL.TabIndex = 23
        Me.lblBalSL.Text = "0"
        Me.lblBalSL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(3, 16)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(27, 13)
        Me.Label20.TabIndex = 22
        Me.Label20.Text = "CL :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkHalfDay
        '
        Me.chkHalfDay.AutoSize = True
        Me.chkHalfDay.BackColor = System.Drawing.SystemColors.Control
        Me.chkHalfDay.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHalfDay.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHalfDay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHalfDay.Location = New System.Drawing.Point(392, 128)
        Me.chkHalfDay.Name = "chkHalfDay"
        Me.chkHalfDay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHalfDay.Size = New System.Drawing.Size(68, 18)
        Me.chkHalfDay.TabIndex = 8
        Me.chkHalfDay.Text = "Half Day"
        Me.chkHalfDay.UseVisualStyleBackColor = False
        '
        'FraHRStatus
        '
        Me.FraHRStatus.BackColor = System.Drawing.SystemColors.Control
        Me.FraHRStatus.Controls.Add(Me._optHRStatus_0)
        Me.FraHRStatus.Controls.Add(Me._optHRStatus_1)
        Me.FraHRStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraHRStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraHRStatus.Location = New System.Drawing.Point(276, 420)
        Me.FraHRStatus.Name = "FraHRStatus"
        Me.FraHRStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.FraHRStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraHRStatus.Size = New System.Drawing.Size(262, 43)
        Me.FraHRStatus.TabIndex = 18
        Me.FraHRStatus.TabStop = False
        Me.FraHRStatus.Text = "HR Status"
        '
        '_optHRStatus_0
        '
        Me._optHRStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._optHRStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optHRStatus_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optHRStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optHRStatus.SetIndex(Me._optHRStatus_0, CType(0, Short))
        Me._optHRStatus_0.Location = New System.Drawing.Point(60, 16)
        Me._optHRStatus_0.Name = "_optHRStatus_0"
        Me._optHRStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optHRStatus_0.Size = New System.Drawing.Size(79, 17)
        Me._optHRStatus_0.TabIndex = 19
        Me._optHRStatus_0.TabStop = True
        Me._optHRStatus_0.Text = "Open"
        Me._optHRStatus_0.UseVisualStyleBackColor = False
        '
        '_optHRStatus_1
        '
        Me._optHRStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._optHRStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optHRStatus_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optHRStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optHRStatus.SetIndex(Me._optHRStatus_1, CType(1, Short))
        Me._optHRStatus_1.Location = New System.Drawing.Point(170, 16)
        Me._optHRStatus_1.Name = "_optHRStatus_1"
        Me._optHRStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optHRStatus_1.Size = New System.Drawing.Size(79, 17)
        Me._optHRStatus_1.TabIndex = 20
        Me._optHRStatus_1.TabStop = True
        Me._optHRStatus_1.Text = "Closed"
        Me._optHRStatus_1.UseVisualStyleBackColor = False
        '
        'FraAppStatus
        '
        Me.FraAppStatus.BackColor = System.Drawing.SystemColors.Control
        Me.FraAppStatus.Controls.Add(Me._optStatus_1)
        Me.FraAppStatus.Controls.Add(Me._optStatus_0)
        Me.FraAppStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAppStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAppStatus.Location = New System.Drawing.Point(1, 420)
        Me.FraAppStatus.Name = "FraAppStatus"
        Me.FraAppStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAppStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAppStatus.Size = New System.Drawing.Size(262, 43)
        Me.FraAppStatus.TabIndex = 15
        Me.FraAppStatus.TabStop = False
        Me.FraAppStatus.Text = "Approval Status"
        '
        '_optStatus_1
        '
        Me._optStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_1, CType(1, Short))
        Me._optStatus_1.Location = New System.Drawing.Point(170, 16)
        Me._optStatus_1.Name = "_optStatus_1"
        Me._optStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_1.Size = New System.Drawing.Size(79, 17)
        Me._optStatus_1.TabIndex = 17
        Me._optStatus_1.TabStop = True
        Me._optStatus_1.Text = "Rejected"
        Me._optStatus_1.UseVisualStyleBackColor = False
        '
        '_optStatus_0
        '
        Me._optStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_0, CType(0, Short))
        Me._optStatus_0.Location = New System.Drawing.Point(60, 16)
        Me._optStatus_0.Name = "_optStatus_0"
        Me._optStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_0.Size = New System.Drawing.Size(79, 17)
        Me._optStatus_0.TabIndex = 16
        Me._optStatus_0.TabStop = True
        Me._optStatus_0.Text = "Approved"
        Me._optStatus_0.UseVisualStyleBackColor = False
        '
        'txtAppEmpCode
        '
        Me.txtAppEmpCode.AcceptsReturn = True
        Me.txtAppEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtAppEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAppEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAppEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppEmpCode.ForeColor = System.Drawing.Color.Blue
        Me.txtAppEmpCode.Location = New System.Drawing.Point(148, 347)
        Me.txtAppEmpCode.MaxLength = 0
        Me.txtAppEmpCode.Name = "txtAppEmpCode"
        Me.txtAppEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppEmpCode.Size = New System.Drawing.Size(75, 20)
        Me.txtAppEmpCode.TabIndex = 12
        '
        'txtRecEmpCode
        '
        Me.txtRecEmpCode.AcceptsReturn = True
        Me.txtRecEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecEmpCode.ForeColor = System.Drawing.Color.Blue
        Me.txtRecEmpCode.Location = New System.Drawing.Point(148, 325)
        Me.txtRecEmpCode.MaxLength = 0
        Me.txtRecEmpCode.Name = "txtRecEmpCode"
        Me.txtRecEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecEmpCode.Size = New System.Drawing.Size(75, 20)
        Me.txtRecEmpCode.TabIndex = 10
        '
        'txtDays
        '
        Me.txtDays.AcceptsReturn = True
        Me.txtDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays.ForeColor = System.Drawing.Color.Blue
        Me.txtDays.Location = New System.Drawing.Point(148, 150)
        Me.txtDays.MaxLength = 0
        Me.txtDays.Name = "txtDays"
        Me.txtDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays.Size = New System.Drawing.Size(75, 20)
        Me.txtDays.TabIndex = 9
        '
        'txtDateTo
        '
        Me.txtDateTo.AcceptsReturn = True
        Me.txtDateTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.ForeColor = System.Drawing.Color.Blue
        Me.txtDateTo.Location = New System.Drawing.Point(293, 128)
        Me.txtDateTo.MaxLength = 0
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateTo.Size = New System.Drawing.Size(75, 20)
        Me.txtDateTo.TabIndex = 7
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AcceptsReturn = True
        Me.txtDateFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.ForeColor = System.Drawing.Color.Blue
        Me.txtDateFrom.Location = New System.Drawing.Point(148, 128)
        Me.txtDateFrom.MaxLength = 0
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 20)
        Me.txtDateFrom.TabIndex = 6
        '
        'txtReqDate
        '
        Me.txtReqDate.AcceptsReturn = True
        Me.txtReqDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReqDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReqDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqDate.ForeColor = System.Drawing.Color.Blue
        Me.txtReqDate.Location = New System.Drawing.Point(430, 18)
        Me.txtReqDate.MaxLength = 0
        Me.txtReqDate.Name = "txtReqDate"
        Me.txtReqDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReqDate.Size = New System.Drawing.Size(75, 20)
        Me.txtReqDate.TabIndex = 3
        '
        'txtEmp
        '
        Me.txtEmp.AcceptsReturn = True
        Me.txtEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.ForeColor = System.Drawing.Color.Blue
        Me.txtEmp.Location = New System.Drawing.Point(148, 41)
        Me.txtEmp.MaxLength = 0
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmp.Size = New System.Drawing.Size(75, 20)
        Me.txtEmp.TabIndex = 4
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.Color.Blue
        Me.txtReason.Location = New System.Drawing.Point(148, 394)
        Me.txtReason.MaxLength = 0
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(359, 20)
        Me.txtReason.TabIndex = 14
        '
        'txtReqNo
        '
        Me.txtReqNo.AcceptsReturn = True
        Me.txtReqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.ForeColor = System.Drawing.Color.Blue
        Me.txtReqNo.Location = New System.Drawing.Point(148, 18)
        Me.txtReqNo.MaxLength = 0
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReqNo.Size = New System.Drawing.Size(73, 20)
        Me.txtReqNo.TabIndex = 1
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(14, 374)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(133, 13)
        Me.Label19.TabIndex = 53
        Me.Label19.Text = "To eMail ID:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblToeMailID
        '
        Me.lblToeMailID.BackColor = System.Drawing.SystemColors.Control
        Me.lblToeMailID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblToeMailID.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblToeMailID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToeMailID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblToeMailID.Location = New System.Drawing.Point(148, 371)
        Me.lblToeMailID.Name = "lblToeMailID"
        Me.lblToeMailID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblToeMailID.Size = New System.Drawing.Size(359, 19)
        Me.lblToeMailID.TabIndex = 52
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(14, 350)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(133, 13)
        Me.Label17.TabIndex = 51
        Me.Label17.Text = "*Approved by:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAppEmpName
        '
        Me.lblAppEmpName.BackColor = System.Drawing.SystemColors.Control
        Me.lblAppEmpName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAppEmpName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAppEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppEmpName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAppEmpName.Location = New System.Drawing.Point(248, 347)
        Me.lblAppEmpName.Name = "lblAppEmpName"
        Me.lblAppEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAppEmpName.Size = New System.Drawing.Size(259, 19)
        Me.lblAppEmpName.TabIndex = 50
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(14, 328)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(133, 13)
        Me.Label15.TabIndex = 49
        Me.Label15.Text = "Recommended By :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRecEmpName
        '
        Me.lblRecEmpName.BackColor = System.Drawing.SystemColors.Control
        Me.lblRecEmpName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRecEmpName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRecEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRecEmpName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRecEmpName.Location = New System.Drawing.Point(248, 325)
        Me.lblRecEmpName.Name = "lblRecEmpName"
        Me.lblRecEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRecEmpName.Size = New System.Drawing.Size(259, 19)
        Me.lblRecEmpName.TabIndex = 48
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(8, 152)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(139, 13)
        Me.Label13.TabIndex = 47
        Me.Label13.Text = "Working Days Applied :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(236, 130)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(53, 14)
        Me.Label12.TabIndex = 46
        Me.Label12.Text = "To Date :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(14, 130)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(133, 13)
        Me.Label11.TabIndex = 45
        Me.Label11.Text = "From Date :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(14, 109)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(133, 13)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "Designation:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDesgName
        '
        Me.lblDesgName.BackColor = System.Drawing.SystemColors.Control
        Me.lblDesgName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDesgName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDesgName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesgName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDesgName.Location = New System.Drawing.Point(148, 106)
        Me.lblDesgName.Name = "lblDesgName"
        Me.lblDesgName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDesgName.Size = New System.Drawing.Size(359, 19)
        Me.lblDesgName.TabIndex = 43
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(14, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(133, 13)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "From eMail ID:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFromeMailID
        '
        Me.lblFromeMailID.BackColor = System.Drawing.SystemColors.Control
        Me.lblFromeMailID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFromeMailID.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFromeMailID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromeMailID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFromeMailID.Location = New System.Drawing.Point(148, 64)
        Me.lblFromeMailID.Name = "lblFromeMailID"
        Me.lblFromeMailID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFromeMailID.Size = New System.Drawing.Size(359, 19)
        Me.lblFromeMailID.TabIndex = 41
        '
        'lblCust
        '
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(14, 20)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(133, 13)
        Me.lblCust.TabIndex = 40
        Me.lblCust.Text = "Ref No:"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(14, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(133, 13)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Department :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(14, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(133, 13)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Employee :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(14, 395)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(133, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Reason :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(305, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(37, 14)
        Me.Label7.TabIndex = 36
        Me.Label7.Text = "Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDeptname
        '
        Me.lblDeptname.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeptname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeptname.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeptname.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeptname.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptname.Location = New System.Drawing.Point(148, 86)
        Me.lblDeptname.Name = "lblDeptname"
        Me.lblDeptname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeptname.Size = New System.Drawing.Size(359, 19)
        Me.lblDeptname.TabIndex = 35
        '
        'lblEmpname
        '
        Me.lblEmpname.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmpname.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmpname.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpname.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmpname.Location = New System.Drawing.Point(246, 41)
        Me.lblEmpname.Name = "lblEmpname"
        Me.lblEmpname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmpname.Size = New System.Drawing.Size(259, 19)
        Me.lblEmpname.TabIndex = 34
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(461, 132)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(64, 14)
        Me.lblBookType.TabIndex = 33
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 176)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(540, 144)
        Me.SprdMain.TabIndex = 15
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
        Me.AdoDCMain.TabIndex = 33
        Me.AdoDCMain.Text = "Adodc1"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(545, 501)
        Me.SprdView.TabIndex = 30
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.lblMKey)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 495)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(545, 53)
        Me.Frame3.TabIndex = 29
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 29
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
        Me.lblMKey.TabIndex = 31
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'optHRStatus
        '
        '
        'optStatus
        '
        '
        'FrmLeaveRequisitionEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(546, 550)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.AdoDCMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmLeaveRequisitionEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Leave Requisition Entry"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraHRStatus.ResumeLayout(False)
        Me.FraAppStatus.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optHRStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents Frame2 As GroupBox
    Public WithEvents lblAvlCPL As Label
    Public WithEvents Label2 As Label
    Public WithEvents Label5 As Label
    Public WithEvents lblAvlSL As Label
    Public WithEvents Label6 As Label
    Public WithEvents lblAvlEL As Label
    Public WithEvents Label9 As Label
    Public WithEvents lblAvlCL As Label
    Public WithEvents Frame1 As GroupBox
    Public WithEvents lblBalCPL As Label
    Public WithEvents Label14 As Label
    Public WithEvents lblBalCL As Label
    Public WithEvents Label16 As Label
    Public WithEvents lblBalEL As Label
    Public WithEvents Label18 As Label
    Public WithEvents lblBalSL As Label
    Public WithEvents Label20 As Label
#End Region
End Class