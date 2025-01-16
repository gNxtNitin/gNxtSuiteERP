Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmFGBreakup
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
    Public WithEvents txtAvailableQty As System.Windows.Forms.TextBox
    Public WithEvents txtProductCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchProductCode As System.Windows.Forms.Button
    Public WithEvents cmdPopulate As System.Windows.Forms.Button
    Public WithEvents txtDismantleQty As System.Windows.Forms.TextBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents cmdSearchEmp As System.Windows.Forms.Button
    Public WithEvents txtPMemoNo As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtEmp As System.Windows.Forms.TextBox
    Public WithEvents txtPMemoDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblProductionUOM As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblProductCode As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblEmp As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmFGBreakup))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.FraFront = New System.Windows.Forms.GroupBox
        Me.txtAvailableQty = New System.Windows.Forms.TextBox
        Me.txtProductCode = New System.Windows.Forms.TextBox
        Me.cmdSearchProductCode = New System.Windows.Forms.Button
        Me.cmdPopulate = New System.Windows.Forms.Button
        Me.txtDismantleQty = New System.Windows.Forms.TextBox
        Me.cboDivision = New System.Windows.Forms.ComboBox
        Me.cmdSearchEmp = New System.Windows.Forms.Button
        Me.txtPMemoNo = New System.Windows.Forms.TextBox
        Me.txtRemarks = New System.Windows.Forms.TextBox
        Me.txtEmp = New System.Windows.Forms.TextBox
        Me.txtPMemoDate = New System.Windows.Forms.TextBox
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread
        Me.lblProductionUOM = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblProductCode = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.lblEmp = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblCust = New System.Windows.Forms.Label
        Me.AdoDCMain = New VB6.ADODC
        Me.SprdView = New AxFPSpreadADO.AxfpSpread
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdModify = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.CmdView = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.CmdPreview = New System.Windows.Forms.Button
        Me.cmdSavePrint = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.Report1 = New AxCrystal.AxCrystalReport
        Me.lblBookType = New System.Windows.Forms.Label
        Me.lblMKey = New System.Windows.Forms.Label
        Me.FraFront.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "Finished Goods Dismantle"
        Me.ClientSize = New System.Drawing.Size(724, 458)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.Icon = CType(resources.GetObject("FrmFGBreakup.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.MinimizeBox = False
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ControlBox = True
        Me.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "FrmFGBreakup"
        Me.FraFront.Size = New System.Drawing.Size(723, 415)
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.TabIndex = 27
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Enabled = True
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Visible = True
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.Name = "FraFront"
        Me.txtAvailableQty.AutoSize = False
        Me.txtAvailableQty.Enabled = False
        Me.txtAvailableQty.ForeColor = System.Drawing.Color.Blue
        Me.txtAvailableQty.Size = New System.Drawing.Size(105, 19)
        Me.txtAvailableQty.Location = New System.Drawing.Point(104, 126)
        Me.txtAvailableQty.TabIndex = 12
        Me.txtAvailableQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAvailableQty.AcceptsReturn = True
        Me.txtAvailableQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtAvailableQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtAvailableQty.CausesValidation = True
        Me.txtAvailableQty.HideSelection = True
        Me.txtAvailableQty.ReadOnly = False
        Me.txtAvailableQty.MaxLength = 0
        Me.txtAvailableQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAvailableQty.Multiline = False
        Me.txtAvailableQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAvailableQty.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAvailableQty.TabStop = True
        Me.txtAvailableQty.Visible = True
        Me.txtAvailableQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAvailableQty.Name = "txtAvailableQty"
        Me.txtProductCode.AutoSize = False
        Me.txtProductCode.ForeColor = System.Drawing.Color.Blue
        Me.txtProductCode.Size = New System.Drawing.Size(105, 19)
        Me.txtProductCode.Location = New System.Drawing.Point(104, 104)
        Me.txtProductCode.TabIndex = 9
        Me.txtProductCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.AcceptsReturn = True
        Me.txtProductCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductCode.CausesValidation = True
        Me.txtProductCode.Enabled = True
        Me.txtProductCode.HideSelection = True
        Me.txtProductCode.ReadOnly = False
        Me.txtProductCode.MaxLength = 0
        Me.txtProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductCode.Multiline = False
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtProductCode.TabStop = True
        Me.txtProductCode.Visible = True
        Me.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductCode.Name = "txtProductCode"
        Me.cmdSearchProductCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSearchProductCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProductCode.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchProductCode.Location = New System.Drawing.Point(210, 104)
        Me.cmdSearchProductCode.Image = CType(resources.GetObject("cmdSearchProductCode.Image"), System.Drawing.Image)
        Me.cmdSearchProductCode.TabIndex = 10
        Me.cmdSearchProductCode.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdSearchProductCode, "Search")
        Me.cmdSearchProductCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProductCode.CausesValidation = True
        Me.cmdSearchProductCode.Enabled = True
        Me.cmdSearchProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProductCode.Name = "cmdSearchProductCode"
        Me.cmdPopulate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdPopulate.Text = "Populate"
        Me.cmdPopulate.Enabled = False
        Me.cmdPopulate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulate.Size = New System.Drawing.Size(107, 21)
        Me.cmdPopulate.Location = New System.Drawing.Point(508, 126)
        Me.cmdPopulate.TabIndex = 15
        Me.cmdPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPopulate.CausesValidation = True
        Me.cmdPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulate.TabStop = True
        Me.cmdPopulate.Name = "cmdPopulate"
        Me.txtDismantleQty.AutoSize = False
        Me.txtDismantleQty.ForeColor = System.Drawing.Color.Blue
        Me.txtDismantleQty.Size = New System.Drawing.Size(105, 19)
        Me.txtDismantleQty.Location = New System.Drawing.Point(380, 126)
        Me.txtDismantleQty.TabIndex = 14
        Me.txtDismantleQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDismantleQty.AcceptsReturn = True
        Me.txtDismantleQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtDismantleQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtDismantleQty.CausesValidation = True
        Me.txtDismantleQty.Enabled = True
        Me.txtDismantleQty.HideSelection = True
        Me.txtDismantleQty.ReadOnly = False
        Me.txtDismantleQty.MaxLength = 0
        Me.txtDismantleQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDismantleQty.Multiline = False
        Me.txtDismantleQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDismantleQty.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDismantleQty.TabStop = True
        Me.txtDismantleQty.Visible = True
        Me.txtDismantleQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDismantleQty.Name = "txtDismantleQty"
        Me.cboDivision.Size = New System.Drawing.Size(231, 21)
        Me.cboDivision.Location = New System.Drawing.Point(104, 34)
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.TabIndex = 4
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.CausesValidation = True
        Me.cboDivision.Enabled = True
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.IntegralHeight = True
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Sorted = False
        Me.cboDivision.TabStop = True
        Me.cboDivision.Visible = True
        Me.cboDivision.Name = "cboDivision"
        Me.cmdSearchEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSearchEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchEmp.Location = New System.Drawing.Point(210, 59)
        Me.cmdSearchEmp.Image = CType(resources.GetObject("cmdSearchEmp.Image"), System.Drawing.Image)
        Me.cmdSearchEmp.TabIndex = 6
        Me.cmdSearchEmp.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdSearchEmp, "Search")
        Me.cmdSearchEmp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEmp.CausesValidation = True
        Me.cmdSearchEmp.Enabled = True
        Me.cmdSearchEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchEmp.Name = "cmdSearchEmp"
        Me.txtPMemoNo.AutoSize = False
        Me.txtPMemoNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPMemoNo.Size = New System.Drawing.Size(105, 19)
        Me.txtPMemoNo.Location = New System.Drawing.Point(104, 12)
        Me.txtPMemoNo.TabIndex = 1
        Me.txtPMemoNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMemoNo.AcceptsReturn = True
        Me.txtPMemoNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtPMemoNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMemoNo.CausesValidation = True
        Me.txtPMemoNo.Enabled = True
        Me.txtPMemoNo.HideSelection = True
        Me.txtPMemoNo.ReadOnly = False
        Me.txtPMemoNo.MaxLength = 0
        Me.txtPMemoNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPMemoNo.Multiline = False
        Me.txtPMemoNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPMemoNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtPMemoNo.TabStop = True
        Me.txtPMemoNo.Visible = True
        Me.txtPMemoNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMemoNo.Name = "txtPMemoNo"
        Me.txtRemarks.AutoSize = False
        Me.txtRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtRemarks.Size = New System.Drawing.Size(381, 19)
        Me.txtRemarks.Location = New System.Drawing.Point(104, 81)
        Me.txtRemarks.TabIndex = 8
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.CausesValidation = True
        Me.txtRemarks.Enabled = True
        Me.txtRemarks.HideSelection = True
        Me.txtRemarks.ReadOnly = False
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Multiline = False
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRemarks.TabStop = True
        Me.txtRemarks.Visible = True
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtEmp.AutoSize = False
        Me.txtEmp.ForeColor = System.Drawing.Color.Blue
        Me.txtEmp.Size = New System.Drawing.Size(105, 19)
        Me.txtEmp.Location = New System.Drawing.Point(104, 59)
        Me.txtEmp.TabIndex = 5
        Me.txtEmp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.AcceptsReturn = True
        Me.txtEmp.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmp.CausesValidation = True
        Me.txtEmp.Enabled = True
        Me.txtEmp.HideSelection = True
        Me.txtEmp.ReadOnly = False
        Me.txtEmp.MaxLength = 0
        Me.txtEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmp.Multiline = False
        Me.txtEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmp.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEmp.TabStop = True
        Me.txtEmp.Visible = True
        Me.txtEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmp.Name = "txtEmp"
        Me.txtPMemoDate.AutoSize = False
        Me.txtPMemoDate.ForeColor = System.Drawing.Color.Blue
        Me.txtPMemoDate.Size = New System.Drawing.Size(89, 19)
        Me.txtPMemoDate.Location = New System.Drawing.Point(348, 12)
        Me.txtPMemoDate.TabIndex = 3
        Me.txtPMemoDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPMemoDate.AcceptsReturn = True
        Me.txtPMemoDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtPMemoDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPMemoDate.CausesValidation = True
        Me.txtPMemoDate.Enabled = True
        Me.txtPMemoDate.HideSelection = True
        Me.txtPMemoDate.ReadOnly = False
        Me.txtPMemoDate.MaxLength = 0
        Me.txtPMemoDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPMemoDate.Multiline = False
        Me.txtPMemoDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPMemoDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtPMemoDate.TabStop = True
        Me.txtPMemoDate.Visible = True
        Me.txtPMemoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPMemoDate.Name = "txtPMemoDate"
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearch.Location = New System.Drawing.Point(210, 12)
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.CausesValidation = True
        Me.cmdSearch.Enabled = True
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Name = "cmdSearch"
        SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(717, 265)
        Me.SprdMain.Location = New System.Drawing.Point(2, 148)
        Me.SprdMain.TabIndex = 16
        Me.SprdMain.Name = "SprdMain"
        Me.lblProductionUOM.Size = New System.Drawing.Size(61, 19)
        Me.lblProductionUOM.Location = New System.Drawing.Point(212, 126)
        Me.lblProductionUOM.TabIndex = 13
        Me.lblProductionUOM.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductionUOM.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblProductionUOM.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductionUOM.Enabled = True
        Me.lblProductionUOM.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductionUOM.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductionUOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductionUOM.UseMnemonic = True
        Me.lblProductionUOM.Visible = True
        Me.lblProductionUOM.AutoSize = False
        Me.lblProductionUOM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProductionUOM.Name = "lblProductionUOM"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label9.Text = "Available Qty :"
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Size = New System.Drawing.Size(84, 13)
        Me.Label9.Location = New System.Drawing.Point(16, 129)
        Me.Label9.TabIndex = 37
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Enabled = True
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.UseMnemonic = True
        Me.Label9.Visible = True
        Me.Label9.AutoSize = True
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label9.Name = "Label9"
        Me.lblProductCode.Size = New System.Drawing.Size(247, 19)
        Me.lblProductCode.Location = New System.Drawing.Point(236, 104)
        Me.lblProductCode.TabIndex = 11
        Me.lblProductCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductCode.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblProductCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductCode.Enabled = True
        Me.lblProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductCode.UseMnemonic = True
        Me.lblProductCode.Visible = True
        Me.lblProductCode.AutoSize = False
        Me.lblProductCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProductCode.Name = "lblProductCode"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label5.Text = "Dismantle Qty :"
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.Location = New System.Drawing.Point(288, 129)
        Me.Label5.TabIndex = 36
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Enabled = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.UseMnemonic = True
        Me.Label5.Visible = True
        Me.Label5.AutoSize = True
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label5.Name = "Label5"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label3.Text = "Product Code :"
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.Location = New System.Drawing.Point(13, 107)
        Me.Label3.TabIndex = 35
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Enabled = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.UseMnemonic = True
        Me.Label3.Visible = True
        Me.Label3.AutoSize = True
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label3.Name = "Label3"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label12.Text = "Division :"
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.Location = New System.Drawing.Point(44, 38)
        Me.Label12.TabIndex = 34
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Enabled = True
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.UseMnemonic = True
        Me.Label12.Visible = True
        Me.Label12.AutoSize = True
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label12.Name = "Label12"
        Me.lblEmp.Size = New System.Drawing.Size(247, 19)
        Me.lblEmp.Location = New System.Drawing.Point(236, 59)
        Me.lblEmp.TabIndex = 7
        Me.lblEmp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmp.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblEmp.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmp.Enabled = True
        Me.lblEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmp.UseMnemonic = True
        Me.lblEmp.Visible = True
        Me.lblEmp.AutoSize = False
        Me.lblEmp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmp.Name = "lblEmp"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label7.Text = "Ref Date :"
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.Location = New System.Drawing.Point(284, 14)
        Me.Label7.TabIndex = 31
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Enabled = True
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.UseMnemonic = True
        Me.Label7.Visible = True
        Me.Label7.AutoSize = True
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label7.Name = "Label7"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label4.Text = "Remarks :"
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.Location = New System.Drawing.Point(41, 84)
        Me.Label4.TabIndex = 30
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Enabled = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.UseMnemonic = True
        Me.Label4.Visible = True
        Me.Label4.AutoSize = True
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label4.Name = "Label4"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Text = "Employee :"
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.Location = New System.Drawing.Point(36, 62)
        Me.Label1.TabIndex = 29
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Enabled = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.UseMnemonic = True
        Me.Label1.Visible = True
        Me.Label1.AutoSize = True
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label1.Name = "Label1"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblCust.Text = "Ref No :"
        Me.lblCust.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.Size = New System.Drawing.Size(49, 13)
        Me.lblCust.Location = New System.Drawing.Point(50, 16)
        Me.lblCust.TabIndex = 28
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Enabled = True
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.UseMnemonic = True
        Me.lblCust.Visible = True
        Me.lblCust.AutoSize = True
        Me.lblCust.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblCust.Name = "lblCust"
        Me.AdoDCMain.Size = New System.Drawing.Size(313, 33)
        Me.AdoDCMain.Location = New System.Drawing.Point(56, 270)
        Me.AdoDCMain.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Me.AdoDCMain.ConnectionTimeout = 15
        Me.AdoDCMain.CommandTimeout = 30
        Me.AdoDCMain.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AdoDCMain.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AdoDCMain.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AdoDCMain.CacheSize = 50
        Me.AdoDCMain.MaxRecords = 0
        Me.AdoDCMain.BOFAction = VB6.ADODC.BOFActionEnum.adDoMoveFirst
        Me.AdoDCMain.EOFAction = VB6.ADODC.EOFActionEnum.adDoMoveLast
        Me.AdoDCMain.BackColor = System.Drawing.SystemColors.Window
        Me.AdoDCMain.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AdoDCMain.Orientation = VB6.ADODC.OrientationEnum.adHorizontal
        Me.AdoDCMain.Enabled = True
        Me.AdoDCMain.UserName = ""
        Me.AdoDCMain.RecordSource = ""
        Me.AdoDCMain.Text = "Adodc1"
        Me.AdoDCMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdoDCMain.ConnectionString = ""
        Me.AdoDCMain.Name = "AdoDCMain"
        SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(723, 409)
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.TabIndex = 26
        Me.SprdView.Name = "SprdView"
        Me.Frame3.Size = New System.Drawing.Size(723, 53)
        Me.Frame3.Location = New System.Drawing.Point(0, 404)
        Me.Frame3.TabIndex = 25
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Enabled = True
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Visible = True
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.Name = "Frame3"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.Location = New System.Drawing.Point(66, 12)
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.CausesValidation = True
        Me.cmdAdd.Enabled = True
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.TabStop = True
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.Location = New System.Drawing.Point(133, 12)
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.CausesValidation = True
        Me.cmdModify.Enabled = True
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.TabStop = True
        Me.cmdModify.Name = "cmdModify"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.Location = New System.Drawing.Point(200, 12)
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.CausesValidation = True
        Me.cmdSave.Enabled = True
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.TabStop = True
        Me.cmdSave.Name = "cmdSave"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.Location = New System.Drawing.Point(267, 12)
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.CausesValidation = True
        Me.cmdDelete.Enabled = True
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.TabStop = True
        Me.cmdDelete.Name = "cmdDelete"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Text = "List &View"
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.Location = New System.Drawing.Point(534, 12)
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.CausesValidation = True
        Me.CmdView.Enabled = True
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.TabStop = True
        Me.CmdView.Name = "CmdView"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.Location = New System.Drawing.Point(401, 12)
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.CausesValidation = True
        Me.cmdPrint.Enabled = True
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.TabStop = True
        Me.cmdPrint.Name = "cmdPrint"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.Location = New System.Drawing.Point(468, 12)
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.CausesValidation = True
        Me.CmdPreview.Enabled = True
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.TabStop = True
        Me.CmdPreview.Name = "CmdPreview"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.Location = New System.Drawing.Point(335, 12)
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.CausesValidation = True
        Me.cmdSavePrint.Enabled = True
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.TabStop = True
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.Location = New System.Drawing.Point(602, 12)
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.CausesValidation = True
        Me.cmdClose.Enabled = True
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.TabStop = True
        Me.cmdClose.Name = "cmdClose"
        Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Size = New System.Drawing.Size(29, 17)
        Me.lblBookType.Location = New System.Drawing.Point(682, 18)
        Me.lblBookType.TabIndex = 33
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Enabled = True
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.UseMnemonic = True
        Me.lblBookType.Visible = True
        Me.lblBookType.AutoSize = False
        Me.lblBookType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblBookType.Name = "lblBookType"
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Size = New System.Drawing.Size(37, 13)
        Me.lblMKey.Location = New System.Drawing.Point(52, 14)
        Me.lblMKey.TabIndex = 32
        Me.lblMKey.Visible = False
        Me.lblMKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Enabled = True
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.UseMnemonic = True
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblMKey.Name = "lblMKey"
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(FraFront)
        Me.Controls.Add(AdoDCMain)
        Me.Controls.Add(SprdView)
        Me.Controls.Add(Frame3)
        Me.FraFront.Controls.Add(txtAvailableQty)
        Me.FraFront.Controls.Add(txtProductCode)
        Me.FraFront.Controls.Add(cmdSearchProductCode)
        Me.FraFront.Controls.Add(cmdPopulate)
        Me.FraFront.Controls.Add(txtDismantleQty)
        Me.FraFront.Controls.Add(cboDivision)
        Me.FraFront.Controls.Add(cmdSearchEmp)
        Me.FraFront.Controls.Add(txtPMemoNo)
        Me.FraFront.Controls.Add(txtRemarks)
        Me.FraFront.Controls.Add(txtEmp)
        Me.FraFront.Controls.Add(txtPMemoDate)
        Me.FraFront.Controls.Add(cmdSearch)
        Me.FraFront.Controls.Add(SprdMain)
        Me.FraFront.Controls.Add(lblProductionUOM)
        Me.FraFront.Controls.Add(Label9)
        Me.FraFront.Controls.Add(lblProductCode)
        Me.FraFront.Controls.Add(Label5)
        Me.FraFront.Controls.Add(Label3)
        Me.FraFront.Controls.Add(Label12)
        Me.FraFront.Controls.Add(lblEmp)
        Me.FraFront.Controls.Add(Label7)
        Me.FraFront.Controls.Add(Label4)
        Me.FraFront.Controls.Add(Label1)
        Me.FraFront.Controls.Add(lblCust)
        Me.Frame3.Controls.Add(cmdAdd)
        Me.Frame3.Controls.Add(cmdModify)
        Me.Frame3.Controls.Add(cmdSave)
        Me.Frame3.Controls.Add(cmdDelete)
        Me.Frame3.Controls.Add(CmdView)
        Me.Frame3.Controls.Add(cmdPrint)
        Me.Frame3.Controls.Add(CmdPreview)
        Me.Frame3.Controls.Add(cmdSavePrint)
        Me.Frame3.Controls.Add(cmdClose)
        Me.Frame3.Controls.Add(Report1)
        Me.Frame3.Controls.Add(lblBookType)
        Me.Frame3.Controls.Add(lblMKey)
        Me.FraFront.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
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