Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMRExpectation
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
        '
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
    Public WithEvents cmdPopulate As System.Windows.Forms.Button
    Public WithEvents txtRequisitionName As System.Windows.Forms.TextBox
    Public WithEvents txtSchdDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblmKey As System.Windows.Forms.Label
    Public WithEvents lblType As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents _Label3_1 As System.Windows.Forms.Label
    Public WithEvents lblSupplierName As System.Windows.Forms.Label
    Public WithEvents _Label3_0 As System.Windows.Forms.Label
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
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Label3 As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMRExpectation))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.FraFront = New System.Windows.Forms.GroupBox
        Me.cmdPopulate = New System.Windows.Forms.Button
        Me.txtRequisitionName = New System.Windows.Forms.TextBox
        Me.txtSchdDate = New System.Windows.Forms.TextBox
        Me.cmdSearch = New System.Windows.Forms.Button
        Me.txtSupplier = New System.Windows.Forms.TextBox
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread
        Me.lblmKey = New System.Windows.Forms.Label
        Me.lblType = New System.Windows.Forms.Label
        Me.lblBookType = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me._Label3_1 = New System.Windows.Forms.Label
        Me.lblSupplierName = New System.Windows.Forms.Label
        Me._Label3_0 = New System.Windows.Forms.Label
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
        Me.Label3 = New VB6.LabelArray(components)
        Me.FraFront.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "Material Receipt Expectation"
        Me.ClientSize = New System.Drawing.Size(751, 458)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.Icon = CType(resources.GetObject("frmMRExpectation.Icon"), System.Drawing.Icon)
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
        Me.Name = "frmMRExpectation"
        Me.FraFront.Size = New System.Drawing.Size(751, 415)
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.TabIndex = 17
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Enabled = True
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Visible = True
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.Name = "FraFront"
        Me.cmdPopulate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdPopulate.Text = "Populate From Schedule"
        Me.cmdPopulate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulate.Size = New System.Drawing.Size(159, 27)
        Me.cmdPopulate.Location = New System.Drawing.Point(586, 48)
        Me.cmdPopulate.TabIndex = 24
        Me.cmdPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPopulate.CausesValidation = True
        Me.cmdPopulate.Enabled = True
        Me.cmdPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulate.TabStop = True
        Me.cmdPopulate.Name = "cmdPopulate"
        Me.txtRequisitionName.AutoSize = False
        Me.txtRequisitionName.Enabled = False
        Me.txtRequisitionName.ForeColor = System.Drawing.Color.Blue
        Me.txtRequisitionName.Size = New System.Drawing.Size(315, 19)
        Me.txtRequisitionName.Location = New System.Drawing.Point(120, 58)
        Me.txtRequisitionName.TabIndex = 4
        Me.txtRequisitionName.Text = " "
        Me.txtRequisitionName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequisitionName.AcceptsReturn = True
        Me.txtRequisitionName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtRequisitionName.BackColor = System.Drawing.SystemColors.Window
        Me.txtRequisitionName.CausesValidation = True
        Me.txtRequisitionName.HideSelection = True
        Me.txtRequisitionName.ReadOnly = False
        Me.txtRequisitionName.MaxLength = 0
        Me.txtRequisitionName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRequisitionName.Multiline = False
        Me.txtRequisitionName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRequisitionName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRequisitionName.TabStop = True
        Me.txtRequisitionName.Visible = True
        Me.txtRequisitionName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRequisitionName.Name = "txtRequisitionName"
        Me.txtSchdDate.AutoSize = False
        Me.txtSchdDate.ForeColor = System.Drawing.Color.Blue
        Me.txtSchdDate.Size = New System.Drawing.Size(79, 19)
        Me.txtSchdDate.Location = New System.Drawing.Point(120, 36)
        Me.txtSchdDate.TabIndex = 3
        Me.txtSchdDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSchdDate.AcceptsReturn = True
        Me.txtSchdDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSchdDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSchdDate.CausesValidation = True
        Me.txtSchdDate.Enabled = True
        Me.txtSchdDate.HideSelection = True
        Me.txtSchdDate.ReadOnly = False
        Me.txtSchdDate.MaxLength = 0
        Me.txtSchdDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSchdDate.Multiline = False
        Me.txtSchdDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSchdDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSchdDate.TabStop = True
        Me.txtSchdDate.Visible = True
        Me.txtSchdDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSchdDate.Name = "txtSchdDate"
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearch.Location = New System.Drawing.Point(200, 14)
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.TabIndex = 1
        Me.cmdSearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.CausesValidation = True
        Me.cmdSearch.Enabled = True
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Name = "cmdSearch"
        Me.txtSupplier.AutoSize = False
        Me.txtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplier.Size = New System.Drawing.Size(79, 19)
        Me.txtSupplier.Location = New System.Drawing.Point(120, 14)
        Me.txtSupplier.TabIndex = 0
        Me.txtSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.CausesValidation = True
        Me.txtSupplier.Enabled = True
        Me.txtSupplier.HideSelection = True
        Me.txtSupplier.ReadOnly = False
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Multiline = False
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtSupplier.TabStop = True
        Me.txtSupplier.Visible = True
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Name = "txtSupplier"
        SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(747, 331)
        Me.SprdMain.Location = New System.Drawing.Point(2, 80)
        Me.SprdMain.TabIndex = 5
        Me.SprdMain.Name = "SprdMain"
        Me.lblmKey.Text = "lblmKey"
        Me.lblmKey.Size = New System.Drawing.Size(43, 21)
        Me.lblmKey.Location = New System.Drawing.Point(550, 52)
        Me.lblmKey.TabIndex = 23
        Me.lblmKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmKey.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblmKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblmKey.Enabled = True
        Me.lblmKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblmKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblmKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblmKey.UseMnemonic = True
        Me.lblmKey.Visible = True
        Me.lblmKey.AutoSize = False
        Me.lblmKey.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblmKey.Name = "lblmKey"
        Me.lblType.Text = "lblType"
        Me.lblType.Size = New System.Drawing.Size(49, 17)
        Me.lblType.Location = New System.Drawing.Point(670, 40)
        Me.lblType.TabIndex = 22
        Me.lblType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.Enabled = True
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.UseMnemonic = True
        Me.lblType.Visible = True
        Me.lblType.AutoSize = False
        Me.lblType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblType.Name = "lblType"
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Size = New System.Drawing.Size(119, 13)
        Me.lblBookType.Location = New System.Drawing.Point(624, 50)
        Me.lblBookType.TabIndex = 21
        Me.lblBookType.Visible = False
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Enabled = True
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.UseMnemonic = True
        Me.lblBookType.AutoSize = False
        Me.lblBookType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblBookType.Name = "lblBookType"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label8.Text = "Confirmation To :"
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Size = New System.Drawing.Size(98, 13)
        Me.Label8.Location = New System.Drawing.Point(20, 60)
        Me.Label8.TabIndex = 20
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Enabled = True
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.UseMnemonic = True
        Me.Label8.Visible = True
        Me.Label8.AutoSize = True
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label8.Name = "Label8"
        Me._Label3_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._Label3_1.Text = "Expectation Date :"
        Me._Label3_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label3_1.Size = New System.Drawing.Size(107, 13)
        Me._Label3_1.Location = New System.Drawing.Point(10, 39)
        Me._Label3_1.TabIndex = 19
        Me._Label3_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label3_1.Enabled = True
        Me._Label3_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label3_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label3_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label3_1.UseMnemonic = True
        Me._Label3_1.Visible = True
        Me._Label3_1.AutoSize = True
        Me._Label3_1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._Label3_1.Name = "_Label3_1"
        Me.lblSupplierName.Size = New System.Drawing.Size(485, 19)
        Me.lblSupplierName.Location = New System.Drawing.Point(224, 14)
        Me.lblSupplierName.TabIndex = 2
        Me.lblSupplierName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplierName.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblSupplierName.BackColor = System.Drawing.SystemColors.Control
        Me.lblSupplierName.Enabled = True
        Me.lblSupplierName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSupplierName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSupplierName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSupplierName.UseMnemonic = True
        Me.lblSupplierName.Visible = True
        Me.lblSupplierName.AutoSize = False
        Me.lblSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSupplierName.Name = "lblSupplierName"
        Me._Label3_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._Label3_0.Text = "Supplier Name :"
        Me._Label3_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label3_0.Size = New System.Drawing.Size(91, 13)
        Me._Label3_0.Location = New System.Drawing.Point(26, 17)
        Me._Label3_0.TabIndex = 18
        Me._Label3_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label3_0.Enabled = True
        Me._Label3_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Label3_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label3_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label3_0.UseMnemonic = True
        Me._Label3_0.Visible = True
        Me._Label3_0.AutoSize = True
        Me._Label3_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._Label3_0.Name = "_Label3_0"
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
        Me.SprdView.Size = New System.Drawing.Size(751, 409)
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.TabIndex = 16
        Me.SprdView.Name = "SprdView"
        Me.Frame3.Size = New System.Drawing.Size(751, 53)
        Me.Frame3.Location = New System.Drawing.Point(0, 404)
        Me.Frame3.TabIndex = 15
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
        Me.cmdAdd.Location = New System.Drawing.Point(82, 12)
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.TabIndex = 6
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
        Me.cmdModify.Location = New System.Drawing.Point(149, 12)
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.TabIndex = 7
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
        Me.cmdSave.Location = New System.Drawing.Point(216, 12)
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.TabIndex = 8
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
        Me.cmdDelete.Location = New System.Drawing.Point(283, 12)
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.TabIndex = 9
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
        Me.CmdView.Location = New System.Drawing.Point(550, 12)
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.TabIndex = 13
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
        Me.cmdPrint.Location = New System.Drawing.Point(417, 12)
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.TabIndex = 11
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
        Me.CmdPreview.Location = New System.Drawing.Point(484, 12)
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.TabIndex = 12
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(351, 12)
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.TabIndex = 10
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
        Me.cmdClose.Location = New System.Drawing.Point(618, 12)
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.TabIndex = 14
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
        Me.Label3.SetIndex(_Label3_1, CType(1, Short))
        Me.Label3.SetIndex(_Label3_0, CType(0, Short))
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(FraFront)
        Me.Controls.Add(AdoDCMain)
        Me.Controls.Add(SprdView)
        Me.Controls.Add(Frame3)
        Me.FraFront.Controls.Add(cmdPopulate)
        Me.FraFront.Controls.Add(txtRequisitionName)
        Me.FraFront.Controls.Add(txtSchdDate)
        Me.FraFront.Controls.Add(cmdSearch)
        Me.FraFront.Controls.Add(txtSupplier)
        Me.FraFront.Controls.Add(SprdMain)
        Me.FraFront.Controls.Add(lblmKey)
        Me.FraFront.Controls.Add(lblType)
        Me.FraFront.Controls.Add(lblBookType)
        Me.FraFront.Controls.Add(Label8)
        Me.FraFront.Controls.Add(_Label3_1)
        Me.FraFront.Controls.Add(lblSupplierName)
        Me.FraFront.Controls.Add(_Label3_0)
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
        Me.FraFront.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class