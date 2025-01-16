Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmItemWarrantyEntry
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
    Public WithEvents txtEMailID As System.Windows.Forms.TextBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents cmdReOfferSearch As System.Windows.Forms.Button
    Public WithEvents txtRefDate As System.Windows.Forms.TextBox
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents cmdMRRSearch As System.Windows.Forms.Button
    Public WithEvents chkCancelled As System.Windows.Forms.CheckBox
    Public WithEvents TxtSupplier As System.Windows.Forms.TextBox
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents txtMRRDate As System.Windows.Forms.TextBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents txtBillDate As System.Windows.Forms.TextBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents LblMkey As System.Windows.Forms.Label
    Public WithEvents Frasupp As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frasprd As System.Windows.Forms.GroupBox
    Public WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Public WithEvents AdataItem As VB6.ADODC
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label18 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FrmItemWarrantyEntry))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.FraFront = New System.Windows.Forms.GroupBox
        Me.txtEMailID = New System.Windows.Forms.TextBox
        Me.Frasupp = New System.Windows.Forms.GroupBox
        Me.cboDivision = New System.Windows.Forms.ComboBox
        Me.cmdReOfferSearch = New System.Windows.Forms.Button
        Me.txtRefDate = New System.Windows.Forms.TextBox
        Me.txtRefNo = New System.Windows.Forms.TextBox
        Me.cmdMRRSearch = New System.Windows.Forms.Button
        Me.chkCancelled = New System.Windows.Forms.CheckBox
        Me.TxtSupplier = New System.Windows.Forms.TextBox
        Me.txtMRRNo = New System.Windows.Forms.TextBox
        Me.txtMRRDate = New System.Windows.Forms.TextBox
        Me.txtBillNo = New System.Windows.Forms.TextBox
        Me.txtBillDate = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.LblMkey = New System.Windows.Forms.Label
        Me.Frasprd = New System.Windows.Forms.GroupBox
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread
        Me.TxtRemarks = New System.Windows.Forms.TextBox
        Me.AdataItem = New VB6.ADODC
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.Report1 = New AxCrystal.AxCrystalReport
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.cmdClose = New System.Windows.Forms.Button
        Me.CmdView = New System.Windows.Forms.Button
        Me.CmdPreview = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.cmdSavePrint = New System.Windows.Forms.Button
        Me.cmdDelete = New System.Windows.Forms.Button
        Me.cmdSave = New System.Windows.Forms.Button
        Me.cmdModify = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.SprdView = New AxFPSpreadADO.AxfpSpread
        Me.Label18 = New System.Windows.Forms.Label
        Me.FraFront.SuspendLayout()
        Me.Frasupp.SuspendLayout()
        Me.Frasprd.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "Item Warranty Entry"
        Me.ClientSize = New System.Drawing.Size(751, 458)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.Icon = CType(resources.GetObject("FrmItemWarrantyEntry.Icon"), System.Drawing.Icon)
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
        Me.Name = "FrmItemWarrantyEntry"
        Me.FraFront.Size = New System.Drawing.Size(751, 415)
        Me.FraFront.Location = New System.Drawing.Point(0, -4)
        Me.FraFront.TabIndex = 21
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Enabled = True
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Visible = True
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.Name = "FraFront"
        Me.txtEMailID.AutoSize = False
        Me.txtEMailID.Size = New System.Drawing.Size(539, 21)
        Me.txtEMailID.Location = New System.Drawing.Point(154, 364)
        Me.txtEMailID.Multiline = True
        Me.txtEMailID.TabIndex = 38
        Me.txtEMailID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEMailID.AcceptsReturn = True
        Me.txtEMailID.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtEMailID.BackColor = System.Drawing.SystemColors.Window
        Me.txtEMailID.CausesValidation = True
        Me.txtEMailID.Enabled = True
        Me.txtEMailID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEMailID.HideSelection = True
        Me.txtEMailID.ReadOnly = False
        Me.txtEMailID.MaxLength = 0
        Me.txtEMailID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEMailID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEMailID.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEMailID.TabStop = True
        Me.txtEMailID.Visible = True
        Me.txtEMailID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEMailID.Name = "txtEMailID"
        Me.Frasupp.Size = New System.Drawing.Size(751, 111)
        Me.Frasupp.Location = New System.Drawing.Point(0, 4)
        Me.Frasupp.TabIndex = 24
        Me.Frasupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasupp.BackColor = System.Drawing.SystemColors.Control
        Me.Frasupp.Enabled = True
        Me.Frasupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasupp.Visible = True
        Me.Frasupp.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasupp.Name = "Frasupp"
        Me.cboDivision.Enabled = False
        Me.cboDivision.Size = New System.Drawing.Size(201, 21)
        Me.cboDivision.Location = New System.Drawing.Point(377, 12)
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.TabIndex = 36
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.CausesValidation = True
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.IntegralHeight = True
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Sorted = False
        Me.cboDivision.TabStop = True
        Me.cboDivision.Visible = True
        Me.cboDivision.Name = "cboDivision"
        Me.cmdReOfferSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdReOfferSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdReOfferSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdReOfferSearch.Location = New System.Drawing.Point(164, 12)
        Me.cmdReOfferSearch.Image = CType(resources.GetObject("cmdReOfferSearch.Image"), System.Drawing.Image)
        Me.cmdReOfferSearch.TabIndex = 35
        Me.cmdReOfferSearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdReOfferSearch, "Search")
        Me.cmdReOfferSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdReOfferSearch.CausesValidation = True
        Me.cmdReOfferSearch.Enabled = True
        Me.cmdReOfferSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdReOfferSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdReOfferSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdReOfferSearch.Name = "cmdReOfferSearch"
        Me.txtRefDate.AutoSize = False
        Me.txtRefDate.ForeColor = System.Drawing.Color.Blue
        Me.txtRefDate.Size = New System.Drawing.Size(81, 19)
        Me.txtRefDate.Location = New System.Drawing.Point(230, 12)
        Me.txtRefDate.TabIndex = 32
        Me.txtRefDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDate.AcceptsReturn = True
        Me.txtRefDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtRefDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefDate.CausesValidation = True
        Me.txtRefDate.Enabled = True
        Me.txtRefDate.HideSelection = True
        Me.txtRefDate.ReadOnly = False
        Me.txtRefDate.MaxLength = 0
        Me.txtRefDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefDate.Multiline = False
        Me.txtRefDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRefDate.TabStop = True
        Me.txtRefDate.Visible = True
        Me.txtRefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefDate.Name = "txtRefDate"
        Me.txtRefNo.AutoSize = False
        Me.txtRefNo.ForeColor = System.Drawing.Color.Blue
        Me.txtRefNo.Size = New System.Drawing.Size(85, 19)
        Me.txtRefNo.Location = New System.Drawing.Point(78, 12)
        Me.txtRefNo.TabIndex = 31
        Me.txtRefNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.CausesValidation = True
        Me.txtRefNo.Enabled = True
        Me.txtRefNo.HideSelection = True
        Me.txtRefNo.ReadOnly = False
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Multiline = False
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRefNo.TabStop = True
        Me.txtRefNo.Visible = True
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Name = "txtRefNo"
        Me.cmdMRRSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdMRRSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdMRRSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdMRRSearch.Location = New System.Drawing.Point(164, 36)
        Me.cmdMRRSearch.Image = CType(resources.GetObject("cmdMRRSearch.Image"), System.Drawing.Image)
        Me.cmdMRRSearch.TabIndex = 2
        Me.cmdMRRSearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdMRRSearch, "Search")
        Me.cmdMRRSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMRRSearch.CausesValidation = True
        Me.cmdMRRSearch.Enabled = True
        Me.cmdMRRSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMRRSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMRRSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMRRSearch.Name = "cmdMRRSearch"
        Me.chkCancelled.Text = "Is Cancelled "
        Me.chkCancelled.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCancelled.Size = New System.Drawing.Size(117, 13)
        Me.chkCancelled.Location = New System.Drawing.Point(594, 14)
        Me.chkCancelled.TabIndex = 4
        Me.chkCancelled.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkCancelled.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkCancelled.BackColor = System.Drawing.SystemColors.Control
        Me.chkCancelled.CausesValidation = True
        Me.chkCancelled.Enabled = True
        Me.chkCancelled.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCancelled.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkCancelled.TabStop = True
        Me.chkCancelled.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkCancelled.Visible = True
        Me.chkCancelled.Name = "chkCancelled"
        Me.TxtSupplier.AutoSize = False
        Me.TxtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.TxtSupplier.Size = New System.Drawing.Size(491, 19)
        Me.TxtSupplier.Location = New System.Drawing.Point(78, 60)
        Me.TxtSupplier.TabIndex = 5
        Me.TxtSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSupplier.AcceptsReturn = True
        Me.TxtSupplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.TxtSupplier.CausesValidation = True
        Me.TxtSupplier.Enabled = True
        Me.TxtSupplier.HideSelection = True
        Me.TxtSupplier.ReadOnly = False
        Me.TxtSupplier.MaxLength = 0
        Me.TxtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtSupplier.Multiline = False
        Me.TxtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtSupplier.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TxtSupplier.TabStop = True
        Me.TxtSupplier.Visible = True
        Me.TxtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSupplier.Name = "TxtSupplier"
        Me.txtMRRNo.AutoSize = False
        Me.txtMRRNo.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRNo.Size = New System.Drawing.Size(85, 19)
        Me.txtMRRNo.Location = New System.Drawing.Point(78, 36)
        Me.txtMRRNo.TabIndex = 1
        Me.txtMRRNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.CausesValidation = True
        Me.txtMRRNo.Enabled = True
        Me.txtMRRNo.HideSelection = True
        Me.txtMRRNo.ReadOnly = False
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Multiline = False
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtMRRNo.TabStop = True
        Me.txtMRRNo.Visible = True
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRDate.AutoSize = False
        Me.txtMRRDate.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRDate.Size = New System.Drawing.Size(81, 19)
        Me.txtMRRDate.Location = New System.Drawing.Point(230, 36)
        Me.txtMRRDate.TabIndex = 3
        Me.txtMRRDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRDate.AcceptsReturn = True
        Me.txtMRRDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtMRRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRDate.CausesValidation = True
        Me.txtMRRDate.Enabled = True
        Me.txtMRRDate.HideSelection = True
        Me.txtMRRDate.ReadOnly = False
        Me.txtMRRDate.MaxLength = 0
        Me.txtMRRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRDate.Multiline = False
        Me.txtMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtMRRDate.TabStop = True
        Me.txtMRRDate.Visible = True
        Me.txtMRRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRDate.Name = "txtMRRDate"
        Me.txtBillNo.AutoSize = False
        Me.txtBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillNo.Size = New System.Drawing.Size(91, 19)
        Me.txtBillNo.Location = New System.Drawing.Point(78, 84)
        Me.txtBillNo.TabIndex = 6
        Me.txtBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.CausesValidation = True
        Me.txtBillNo.Enabled = True
        Me.txtBillNo.HideSelection = True
        Me.txtBillNo.ReadOnly = False
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Multiline = False
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtBillNo.TabStop = True
        Me.txtBillNo.Visible = True
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillDate.AutoSize = False
        Me.txtBillDate.ForeColor = System.Drawing.Color.Blue
        Me.txtBillDate.Size = New System.Drawing.Size(81, 19)
        Me.txtBillDate.Location = New System.Drawing.Point(230, 84)
        Me.txtBillDate.TabIndex = 7
        Me.txtBillDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillDate.AcceptsReturn = True
        Me.txtBillDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtBillDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillDate.CausesValidation = True
        Me.txtBillDate.Enabled = True
        Me.txtBillDate.HideSelection = True
        Me.txtBillDate.ReadOnly = False
        Me.txtBillDate.MaxLength = 0
        Me.txtBillDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillDate.Multiline = False
        Me.txtBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillDate.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtBillDate.TabStop = True
        Me.txtBillDate.Visible = True
        Me.txtBillDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillDate.Name = "txtBillDate"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label12.Text = "Division :"
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.Location = New System.Drawing.Point(318, 16)
        Me.Label12.TabIndex = 37
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
        Me.Label2.Text = "Date :"
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.Location = New System.Drawing.Point(194, 14)
        Me.Label2.TabIndex = 34
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Enabled = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.UseMnemonic = True
        Me.Label2.Visible = True
        Me.Label2.AutoSize = True
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label2.Name = "Label2"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Text = "Ref No :"
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.Location = New System.Drawing.Point(26, 14)
        Me.Label1.TabIndex = 33
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
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label14.Text = "MRR No :"
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Size = New System.Drawing.Size(57, 13)
        Me.Label14.Location = New System.Drawing.Point(18, 38)
        Me.Label14.TabIndex = 30
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Enabled = True
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.UseMnemonic = True
        Me.Label14.Visible = True
        Me.Label14.AutoSize = True
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label14.Name = "Label14"
        Me.Label15.Text = "Date :"
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Size = New System.Drawing.Size(36, 13)
        Me.Label15.Location = New System.Drawing.Point(194, 38)
        Me.Label15.TabIndex = 29
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Enabled = True
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.UseMnemonic = True
        Me.Label15.Visible = True
        Me.Label15.AutoSize = True
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label15.Name = "Label15"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label5.Text = "Bill No :"
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Size = New System.Drawing.Size(73, 13)
        Me.Label5.Location = New System.Drawing.Point(2, 86)
        Me.Label5.TabIndex = 28
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
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label6.Text = "Date :"
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Size = New System.Drawing.Size(36, 13)
        Me.Label6.Location = New System.Drawing.Point(194, 86)
        Me.Label6.TabIndex = 27
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Enabled = True
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.UseMnemonic = True
        Me.Label6.Visible = True
        Me.Label6.AutoSize = True
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label6.Name = "Label6"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label4.Text = "Supplier :"
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.Location = New System.Drawing.Point(2, 62)
        Me.Label4.TabIndex = 26
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
        Me.LblMkey.Text = "MKEY"
        Me.LblMkey.Size = New System.Drawing.Size(31, 11)
        Me.LblMkey.Location = New System.Drawing.Point(594, 68)
        Me.LblMkey.TabIndex = 25
        Me.LblMkey.Visible = False
        Me.LblMkey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMkey.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.LblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMkey.Enabled = True
        Me.LblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMkey.UseMnemonic = True
        Me.LblMkey.AutoSize = False
        Me.LblMkey.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.LblMkey.Name = "LblMkey"
        Me.Frasprd.Size = New System.Drawing.Size(751, 251)
        Me.Frasprd.Location = New System.Drawing.Point(0, 110)
        Me.Frasprd.TabIndex = 18
        Me.Frasprd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasprd.BackColor = System.Drawing.SystemColors.Control
        Me.Frasprd.Enabled = True
        Me.Frasprd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasprd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasprd.Visible = True
        Me.Frasprd.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasprd.Name = "Frasprd"
        SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(745, 239)
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.TabIndex = 8
        Me.SprdMain.Name = "SprdMain"
        Me.TxtRemarks.AutoSize = False
        Me.TxtRemarks.Size = New System.Drawing.Size(539, 21)
        Me.TxtRemarks.Location = New System.Drawing.Point(154, 390)
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.TabIndex = 9
        Me.TxtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.AcceptsReturn = True
        Me.TxtRemarks.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.TxtRemarks.CausesValidation = True
        Me.TxtRemarks.Enabled = True
        Me.TxtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtRemarks.HideSelection = True
        Me.TxtRemarks.ReadOnly = False
        Me.TxtRemarks.MaxLength = 0
        Me.TxtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TxtRemarks.TabStop = True
        Me.TxtRemarks.Visible = True
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.AdataItem.Size = New System.Drawing.Size(80, 22)
        Me.AdataItem.Location = New System.Drawing.Point(0, 252)
        Me.AdataItem.Visible = 0
        Me.AdataItem.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Me.AdataItem.ConnectionTimeout = 15
        Me.AdataItem.CommandTimeout = 30
        Me.AdataItem.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AdataItem.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AdataItem.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AdataItem.CacheSize = 50
        Me.AdataItem.MaxRecords = 0
        Me.AdataItem.BOFAction = VB6.ADODC.BOFActionEnum.adDoMoveFirst
        Me.AdataItem.EOFAction = VB6.ADODC.EOFActionEnum.adDoMoveLast
        Me.AdataItem.BackColor = System.Drawing.SystemColors.Window
        Me.AdataItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AdataItem.Orientation = VB6.ADODC.OrientationEnum.adHorizontal
        Me.AdataItem.Enabled = True
        Me.AdataItem.UserName = ""
        Me.AdataItem.RecordSource = ""
        Me.AdataItem.Text = "Adodc1"
        Me.AdataItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdataItem.ConnectionString = ""
        Me.AdataItem.Name = "AdataItem"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label3.Text = "Reminder eMail Id :"
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Size = New System.Drawing.Size(111, 13)
        Me.Label3.Location = New System.Drawing.Point(37, 368)
        Me.Label3.TabIndex = 39
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
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label25.Text = "Remarks :"
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Size = New System.Drawing.Size(98, 13)
        Me.Label25.Location = New System.Drawing.Point(50, 394)
        Me.Label25.TabIndex = 22
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Enabled = True
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.UseMnemonic = True
        Me.Label25.Visible = True
        Me.Label25.AutoSize = True
        Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label25.Name = "Label25"
        Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Frame3.Size = New System.Drawing.Size(751, 51)
        Me.Frame3.Location = New System.Drawing.Point(0, 406)
        Me.Frame3.TabIndex = 19
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Enabled = True
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Visible = True
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.Name = "Frame3"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.Location = New System.Drawing.Point(594, 10)
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.CausesValidation = True
        Me.cmdClose.Enabled = True
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.TabStop = True
        Me.cmdClose.Name = "cmdClose"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Text = "List &View"
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.Location = New System.Drawing.Point(528, 10)
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.CausesValidation = True
        Me.CmdView.Enabled = True
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.TabStop = True
        Me.CmdView.Name = "CmdView"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.Location = New System.Drawing.Point(462, 10)
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.TabIndex = 15
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
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.Location = New System.Drawing.Point(395, 10)
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.TabIndex = 14
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
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.Location = New System.Drawing.Point(329, 10)
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.TabIndex = 13
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
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.Location = New System.Drawing.Point(263, 10)
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.TabIndex = 12
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
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.Location = New System.Drawing.Point(196, 10)
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.CausesValidation = True
        Me.cmdSave.Enabled = True
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.TabStop = True
        Me.cmdSave.Name = "cmdSave"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.Location = New System.Drawing.Point(129, 10)
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.CausesValidation = True
        Me.cmdModify.Enabled = True
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.TabStop = True
        Me.cmdModify.Name = "cmdModify"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.Location = New System.Drawing.Point(62, 10)
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
        SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(751, 407)
        Me.SprdView.Location = New System.Drawing.Point(0, -2)
        Me.SprdView.TabIndex = 23
        Me.SprdView.Name = "SprdView"
        Me.Label18.Text = "Supplier"
        Me.Label18.Size = New System.Drawing.Size(38, 13)
        Me.Label18.Location = New System.Drawing.Point(314, 50)
        Me.Label18.TabIndex = 20
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Enabled = True
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.UseMnemonic = True
        Me.Label18.Visible = True
        Me.Label18.AutoSize = True
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label18.Name = "Label18"
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(FraFront)
        Me.Controls.Add(Report1)
        Me.Controls.Add(Frame3)
        Me.Controls.Add(SprdView)
        Me.Controls.Add(Label18)
        Me.FraFront.Controls.Add(txtEMailID)
        Me.FraFront.Controls.Add(Frasupp)
        Me.FraFront.Controls.Add(Frasprd)
        Me.FraFront.Controls.Add(TxtRemarks)
        Me.FraFront.Controls.Add(AdataItem)
        Me.FraFront.Controls.Add(Label3)
        Me.FraFront.Controls.Add(Label25)
        Me.Frasupp.Controls.Add(cboDivision)
        Me.Frasupp.Controls.Add(cmdReOfferSearch)
        Me.Frasupp.Controls.Add(txtRefDate)
        Me.Frasupp.Controls.Add(txtRefNo)
        Me.Frasupp.Controls.Add(cmdMRRSearch)
        Me.Frasupp.Controls.Add(chkCancelled)
        Me.Frasupp.Controls.Add(TxtSupplier)
        Me.Frasupp.Controls.Add(txtMRRNo)
        Me.Frasupp.Controls.Add(txtMRRDate)
        Me.Frasupp.Controls.Add(txtBillNo)
        Me.Frasupp.Controls.Add(txtBillDate)
        Me.Frasupp.Controls.Add(Label12)
        Me.Frasupp.Controls.Add(Label2)
        Me.Frasupp.Controls.Add(Label1)
        Me.Frasupp.Controls.Add(Label14)
        Me.Frasupp.Controls.Add(Label15)
        Me.Frasupp.Controls.Add(Label5)
        Me.Frasupp.Controls.Add(Label6)
        Me.Frasupp.Controls.Add(Label4)
        Me.Frasupp.Controls.Add(LblMkey)
        Me.Frasprd.Controls.Add(SprdMain)
        Me.Frame3.Controls.Add(cmdClose)
        Me.Frame3.Controls.Add(CmdView)
        Me.Frame3.Controls.Add(CmdPreview)
        Me.Frame3.Controls.Add(cmdPrint)
        Me.Frame3.Controls.Add(cmdSavePrint)
        Me.Frame3.Controls.Add(cmdDelete)
        Me.Frame3.Controls.Add(cmdSave)
        Me.Frame3.Controls.Add(cmdModify)
        Me.Frame3.Controls.Add(cmdAdd)
        Me.FraFront.ResumeLayout(False)
        Me.Frasupp.ResumeLayout(False)
        Me.Frasprd.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(AdataItem, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class