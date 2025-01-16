Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmDivTransReq
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
    Public WithEvents cboDivisionTo As System.Windows.Forms.ComboBox
    Public WithEvents cboDivisionFrom As System.Windows.Forms.ComboBox
    Public WithEvents txtEntryDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchEmp As System.Windows.Forms.Button
    Public WithEvents cmdSearchCC As System.Windows.Forms.Button
    Public WithEvents txtReqNo As System.Windows.Forms.TextBox
    Public WithEvents txtsubdept As System.Windows.Forms.TextBox
    Public WithEvents txtCost As System.Windows.Forms.TextBox
    Public WithEvents txtEmp As System.Windows.Forms.TextBox
    Public WithEvents cboShiftcd As System.Windows.Forms.ComboBox
    Public WithEvents txtReqDate As System.Windows.Forms.TextBox
    Public WithEvents chkIssue As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblEmpname As System.Windows.Forms.Label
    Public WithEvents lblCostctr As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDivTransReq))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchEmp = New System.Windows.Forms.Button()
        Me.cmdSearchCC = New System.Windows.Forms.Button()
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
        Me.cboDivisionTo = New System.Windows.Forms.ComboBox()
        Me.cboDivisionFrom = New System.Windows.Forms.ComboBox()
        Me.txtEntryDate = New System.Windows.Forms.TextBox()
        Me.txtReqNo = New System.Windows.Forms.TextBox()
        Me.txtsubdept = New System.Windows.Forms.TextBox()
        Me.txtCost = New System.Windows.Forms.TextBox()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.cboShiftcd = New System.Windows.Forms.ComboBox()
        Me.txtReqDate = New System.Windows.Forms.TextBox()
        Me.chkIssue = New System.Windows.Forms.CheckBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblEmpname = New System.Windows.Forms.Label()
        Me.lblCostctr = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.FraFront.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchEmp
        '
        Me.cmdSearchEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEmp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEmp.Image = CType(resources.GetObject("cmdSearchEmp.Image"), System.Drawing.Image)
        Me.cmdSearchEmp.Location = New System.Drawing.Point(208, 102)
        Me.cmdSearchEmp.Name = "cmdSearchEmp"
        Me.cmdSearchEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchEmp.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchEmp.TabIndex = 8
        Me.cmdSearchEmp.TabStop = False
        Me.cmdSearchEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchEmp, "Search")
        Me.cmdSearchEmp.UseVisualStyleBackColor = False
        '
        'cmdSearchCC
        '
        Me.cmdSearchCC.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCC.Image = CType(resources.GetObject("cmdSearchCC.Image"), System.Drawing.Image)
        Me.cmdSearchCC.Location = New System.Drawing.Point(208, 133)
        Me.cmdSearchCC.Name = "cmdSearchCC"
        Me.cmdSearchCC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCC.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchCC.TabIndex = 10
        Me.cmdSearchCC.TabStop = False
        Me.cmdSearchCC.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCC, "Search")
        Me.cmdSearchCC.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(210, 12)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(28, 23)
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
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(135, 14)
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
        Me.cmdModify.Location = New System.Drawing.Point(202, 14)
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
        Me.cmdSave.Location = New System.Drawing.Point(269, 14)
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
        Me.cmdDelete.Location = New System.Drawing.Point(336, 14)
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
        Me.CmdView.Location = New System.Drawing.Point(603, 14)
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
        Me.cmdPrint.Location = New System.Drawing.Point(470, 14)
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
        Me.CmdPreview.Location = New System.Drawing.Point(537, 14)
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(404, 14)
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
        Me.cmdClose.Location = New System.Drawing.Point(671, 14)
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
        Me.FraFront.Controls.Add(Me.cboDivisionTo)
        Me.FraFront.Controls.Add(Me.cboDivisionFrom)
        Me.FraFront.Controls.Add(Me.txtEntryDate)
        Me.FraFront.Controls.Add(Me.cmdSearchEmp)
        Me.FraFront.Controls.Add(Me.cmdSearchCC)
        Me.FraFront.Controls.Add(Me.txtReqNo)
        Me.FraFront.Controls.Add(Me.txtsubdept)
        Me.FraFront.Controls.Add(Me.txtCost)
        Me.FraFront.Controls.Add(Me.txtEmp)
        Me.FraFront.Controls.Add(Me.cboShiftcd)
        Me.FraFront.Controls.Add(Me.txtReqDate)
        Me.FraFront.Controls.Add(Me.chkIssue)
        Me.FraFront.Controls.Add(Me.cmdSearch)
        Me.FraFront.Controls.Add(Me.Frame6)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label9)
        Me.FraFront.Controls.Add(Me.lblBookType)
        Me.FraFront.Controls.Add(Me.lblEmpname)
        Me.FraFront.Controls.Add(Me.lblCostctr)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(910, 574)
        Me.FraFront.TabIndex = 26
        Me.FraFront.TabStop = False
        '
        'cboDivisionTo
        '
        Me.cboDivisionTo.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivisionTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivisionTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivisionTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivisionTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivisionTo.Location = New System.Drawing.Point(98, 73)
        Me.cboDivisionTo.Name = "cboDivisionTo"
        Me.cboDivisionTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivisionTo.Size = New System.Drawing.Size(231, 21)
        Me.cboDivisionTo.TabIndex = 6
        '
        'cboDivisionFrom
        '
        Me.cboDivisionFrom.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivisionFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivisionFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivisionFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivisionFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivisionFrom.Location = New System.Drawing.Point(98, 43)
        Me.cboDivisionFrom.Name = "cboDivisionFrom"
        Me.cboDivisionFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivisionFrom.Size = New System.Drawing.Size(231, 21)
        Me.cboDivisionFrom.TabIndex = 5
        '
        'txtEntryDate
        '
        Me.txtEntryDate.AcceptsReturn = True
        Me.txtEntryDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEntryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEntryDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEntryDate.Enabled = False
        Me.txtEntryDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEntryDate.ForeColor = System.Drawing.Color.Blue
        Me.txtEntryDate.Location = New System.Drawing.Point(606, 13)
        Me.txtEntryDate.MaxLength = 0
        Me.txtEntryDate.Name = "txtEntryDate"
        Me.txtEntryDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEntryDate.Size = New System.Drawing.Size(101, 22)
        Me.txtEntryDate.TabIndex = 37
        '
        'txtReqNo
        '
        Me.txtReqNo.AcceptsReturn = True
        Me.txtReqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReqNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.ForeColor = System.Drawing.Color.Blue
        Me.txtReqNo.Location = New System.Drawing.Point(98, 13)
        Me.txtReqNo.MaxLength = 0
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReqNo.Size = New System.Drawing.Size(105, 22)
        Me.txtReqNo.TabIndex = 1
        '
        'txtsubdept
        '
        Me.txtsubdept.AcceptsReturn = True
        Me.txtsubdept.BackColor = System.Drawing.SystemColors.Window
        Me.txtsubdept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtsubdept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtsubdept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtsubdept.ForeColor = System.Drawing.Color.Blue
        Me.txtsubdept.Location = New System.Drawing.Point(98, 163)
        Me.txtsubdept.MaxLength = 0
        Me.txtsubdept.Name = "txtsubdept"
        Me.txtsubdept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtsubdept.Size = New System.Drawing.Size(483, 22)
        Me.txtsubdept.TabIndex = 11
        '
        'txtCost
        '
        Me.txtCost.AcceptsReturn = True
        Me.txtCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCost.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCost.ForeColor = System.Drawing.Color.Blue
        Me.txtCost.Location = New System.Drawing.Point(98, 133)
        Me.txtCost.MaxLength = 0
        Me.txtCost.Name = "txtCost"
        Me.txtCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCost.Size = New System.Drawing.Size(105, 22)
        Me.txtCost.TabIndex = 9
        '
        'txtEmp
        '
        Me.txtEmp.AcceptsReturn = True
        Me.txtEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.ForeColor = System.Drawing.Color.Blue
        Me.txtEmp.Location = New System.Drawing.Point(98, 103)
        Me.txtEmp.MaxLength = 0
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmp.Size = New System.Drawing.Size(105, 22)
        Me.txtEmp.TabIndex = 7
        '
        'cboShiftcd
        '
        Me.cboShiftcd.BackColor = System.Drawing.SystemColors.Window
        Me.cboShiftcd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShiftcd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShiftcd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShiftcd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShiftcd.Location = New System.Drawing.Point(98, 193)
        Me.cboShiftcd.Name = "cboShiftcd"
        Me.cboShiftcd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShiftcd.Size = New System.Drawing.Size(105, 21)
        Me.cboShiftcd.TabIndex = 12
        '
        'txtReqDate
        '
        Me.txtReqDate.AcceptsReturn = True
        Me.txtReqDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReqDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReqDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqDate.ForeColor = System.Drawing.Color.Blue
        Me.txtReqDate.Location = New System.Drawing.Point(354, 13)
        Me.txtReqDate.MaxLength = 0
        Me.txtReqDate.Name = "txtReqDate"
        Me.txtReqDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReqDate.Size = New System.Drawing.Size(101, 22)
        Me.txtReqDate.TabIndex = 4
        '
        'chkIssue
        '
        Me.chkIssue.BackColor = System.Drawing.SystemColors.Control
        Me.chkIssue.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkIssue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIssue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkIssue.Location = New System.Drawing.Point(818, 16)
        Me.chkIssue.Name = "chkIssue"
        Me.chkIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkIssue.Size = New System.Drawing.Size(73, 20)
        Me.chkIssue.TabIndex = 3
        Me.chkIssue.Text = "(Yes/No)"
        Me.chkIssue.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(-2, 222)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(910, 348)
        Me.Frame6.TabIndex = 28
        Me.Frame6.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 9)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(908, 337)
        Me.SprdMain.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(25, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Division To :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(10, 46)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(84, 13)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "Division From :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(536, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 38
        Me.Label9.Text = "Entry Date :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(802, 72)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(71, 13)
        Me.lblBookType.TabIndex = 36
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblEmpname
        '
        Me.lblEmpname.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmpname.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmpname.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpname.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmpname.Location = New System.Drawing.Point(234, 103)
        Me.lblEmpname.Name = "lblEmpname"
        Me.lblEmpname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmpname.Size = New System.Drawing.Size(315, 21)
        Me.lblEmpname.TabIndex = 14
        '
        'lblCostctr
        '
        Me.lblCostctr.BackColor = System.Drawing.SystemColors.Control
        Me.lblCostctr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCostctr.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCostctr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostctr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCostctr.Location = New System.Drawing.Point(234, 133)
        Me.lblCostctr.Name = "lblCostctr"
        Me.lblCostctr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCostctr.Size = New System.Drawing.Size(315, 19)
        Me.lblCostctr.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(313, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 34
        Me.Label7.Text = "Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(716, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(98, 13)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Issue Completed :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(28, 197)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Shift Code :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(37, 167)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Remarks :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(22, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Cost Center :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(30, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(64, 13)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Employee :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(40, 17)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(54, 13)
        Me.lblCust.TabIndex = 27
        Me.lblCust.Text = "Number :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(908, 566)
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
        Me.Frame3.Location = New System.Drawing.Point(0, 562)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(910, 59)
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
        Me.lblMKey.Size = New System.Drawing.Size(49, 13)
        Me.lblMKey.TabIndex = 35
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'FrmDivTransReq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraFront)
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
        Me.Name = "FrmDivTransReq"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Division Transfer Note (Requisition)"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame6.ResumeLayout(False)
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