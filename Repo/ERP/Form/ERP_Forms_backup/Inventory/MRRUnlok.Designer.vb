Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMRRUnlock
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
    Public WithEvents txtMRRDate As System.Windows.Forms.TextBox
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents cmdMRRSearch As System.Windows.Forms.Button
    Public WithEvents cmdRequestSearch As System.Windows.Forms.Button
    Public WithEvents txtRequestCode As System.Windows.Forms.TextBox
    Public WithEvents txtReason As System.Windows.Forms.TextBox
    Public WithEvents txtAuthorityName As System.Windows.Forms.TextBox
    Public WithEvents txtRequestName As System.Windows.Forms.TextBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
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
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMRRUnlock))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.FraFront = New System.Windows.Forms.GroupBox
        Me.txtMRRDate = New System.Windows.Forms.TextBox
        Me.txtMRRNo = New System.Windows.Forms.TextBox
        Me.cmdMRRSearch = New System.Windows.Forms.Button
        Me.cmdRequestSearch = New System.Windows.Forms.Button
        Me.txtRequestCode = New System.Windows.Forms.TextBox
        Me.txtReason = New System.Windows.Forms.TextBox
        Me.txtAuthorityName = New System.Windows.Forms.TextBox
        Me.txtRequestName = New System.Windows.Forms.TextBox
        Me.txtSupplier = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
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
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "Pending MRR Approval"
        Me.ClientSize = New System.Drawing.Size(655, 196)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.Icon = CType(resources.GetObject("frmMRRUnlock.Icon"), System.Drawing.Icon)
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
        Me.Name = "frmMRRUnlock"
        Me.FraFront.Size = New System.Drawing.Size(655, 153)
        Me.FraFront.Location = New System.Drawing.Point(0, -6)
        Me.FraFront.TabIndex = 21
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Enabled = True
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Visible = True
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.Name = "FraFront"
        Me.txtMRRDate.AutoSize = False
        Me.txtMRRDate.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRDate.Size = New System.Drawing.Size(81, 19)
        Me.txtMRRDate.Location = New System.Drawing.Point(456, 16)
        Me.txtMRRDate.TabIndex = 2
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
        Me.txtMRRNo.AutoSize = False
        Me.txtMRRNo.ForeColor = System.Drawing.Color.Blue
        Me.txtMRRNo.Size = New System.Drawing.Size(85, 19)
        Me.txtMRRNo.Location = New System.Drawing.Point(120, 16)
        Me.txtMRRNo.TabIndex = 0
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
        Me.cmdMRRSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdMRRSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdMRRSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdMRRSearch.Location = New System.Drawing.Point(206, 16)
        Me.cmdMRRSearch.Image = CType(resources.GetObject("cmdMRRSearch.Image"), System.Drawing.Image)
        Me.cmdMRRSearch.TabIndex = 1
        Me.cmdMRRSearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdMRRSearch, "Search")
        Me.cmdMRRSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMRRSearch.CausesValidation = True
        Me.cmdMRRSearch.Enabled = True
        Me.cmdMRRSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdMRRSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdMRRSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdMRRSearch.Name = "cmdMRRSearch"
        Me.cmdRequestSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdRequestSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdRequestSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdRequestSearch.Location = New System.Drawing.Point(200, 62)
        Me.cmdRequestSearch.Image = CType(resources.GetObject("cmdRequestSearch.Image"), System.Drawing.Image)
        Me.cmdRequestSearch.TabIndex = 6
        Me.cmdRequestSearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdRequestSearch, "Search")
        Me.cmdRequestSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRequestSearch.CausesValidation = True
        Me.cmdRequestSearch.Enabled = True
        Me.cmdRequestSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRequestSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRequestSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRequestSearch.Name = "cmdRequestSearch"
        Me.txtRequestCode.AutoSize = False
        Me.txtRequestCode.ForeColor = System.Drawing.Color.Blue
        Me.txtRequestCode.Size = New System.Drawing.Size(79, 19)
        Me.txtRequestCode.Location = New System.Drawing.Point(120, 62)
        Me.txtRequestCode.TabIndex = 5
        Me.txtRequestCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestCode.AcceptsReturn = True
        Me.txtRequestCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtRequestCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtRequestCode.CausesValidation = True
        Me.txtRequestCode.Enabled = True
        Me.txtRequestCode.HideSelection = True
        Me.txtRequestCode.ReadOnly = False
        Me.txtRequestCode.MaxLength = 0
        Me.txtRequestCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRequestCode.Multiline = False
        Me.txtRequestCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRequestCode.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRequestCode.TabStop = True
        Me.txtRequestCode.Visible = True
        Me.txtRequestCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRequestCode.Name = "txtRequestCode"
        Me.txtReason.AutoSize = False
        Me.txtReason.Enabled = False
        Me.txtReason.ForeColor = System.Drawing.Color.Blue
        Me.txtReason.Size = New System.Drawing.Size(419, 41)
        Me.txtReason.Location = New System.Drawing.Point(120, 108)
        Me.txtReason.TabIndex = 9
        Me.txtReason.Text = " "
        Me.txtReason.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.CausesValidation = True
        Me.txtReason.HideSelection = True
        Me.txtReason.ReadOnly = False
        Me.txtReason.MaxLength = 0
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Multiline = False
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtReason.TabStop = True
        Me.txtReason.Visible = True
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Name = "txtReason"
        Me.txtAuthorityName.AutoSize = False
        Me.txtAuthorityName.Enabled = False
        Me.txtAuthorityName.ForeColor = System.Drawing.Color.Blue
        Me.txtAuthorityName.Size = New System.Drawing.Size(419, 19)
        Me.txtAuthorityName.Location = New System.Drawing.Point(120, 86)
        Me.txtAuthorityName.TabIndex = 8
        Me.txtAuthorityName.Text = " "
        Me.txtAuthorityName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthorityName.AcceptsReturn = True
        Me.txtAuthorityName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtAuthorityName.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuthorityName.CausesValidation = True
        Me.txtAuthorityName.HideSelection = True
        Me.txtAuthorityName.ReadOnly = False
        Me.txtAuthorityName.MaxLength = 0
        Me.txtAuthorityName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuthorityName.Multiline = False
        Me.txtAuthorityName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuthorityName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtAuthorityName.TabStop = True
        Me.txtAuthorityName.Visible = True
        Me.txtAuthorityName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthorityName.Name = "txtAuthorityName"
        Me.txtRequestName.AutoSize = False
        Me.txtRequestName.Enabled = False
        Me.txtRequestName.ForeColor = System.Drawing.Color.Blue
        Me.txtRequestName.Size = New System.Drawing.Size(315, 19)
        Me.txtRequestName.Location = New System.Drawing.Point(224, 62)
        Me.txtRequestName.TabIndex = 7
        Me.txtRequestName.Text = " "
        Me.txtRequestName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRequestName.AcceptsReturn = True
        Me.txtRequestName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtRequestName.BackColor = System.Drawing.SystemColors.Window
        Me.txtRequestName.CausesValidation = True
        Me.txtRequestName.HideSelection = True
        Me.txtRequestName.ReadOnly = False
        Me.txtRequestName.MaxLength = 0
        Me.txtRequestName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRequestName.Multiline = False
        Me.txtRequestName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRequestName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtRequestName.TabStop = True
        Me.txtRequestName.Visible = True
        Me.txtRequestName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRequestName.Name = "txtRequestName"
        Me.txtSupplier.AutoSize = False
        Me.txtSupplier.Enabled = False
        Me.txtSupplier.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplier.Size = New System.Drawing.Size(79, 19)
        Me.txtSupplier.Location = New System.Drawing.Point(120, 38)
        Me.txtSupplier.TabIndex = 3
        Me.txtSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.CausesValidation = True
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
        Me.Label15.Text = "Date :"
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Size = New System.Drawing.Size(36, 13)
        Me.Label15.Location = New System.Drawing.Point(418, 18)
        Me.Label15.TabIndex = 27
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
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label14.Text = "MRR No :"
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Size = New System.Drawing.Size(57, 13)
        Me.Label14.Location = New System.Drawing.Point(58, 18)
        Me.Label14.TabIndex = 26
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
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label2.Text = "Authority Given By :"
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Size = New System.Drawing.Size(114, 13)
        Me.Label2.Location = New System.Drawing.Point(4, 86)
        Me.Label2.TabIndex = 25
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
        Me.Label1.Text = "Reason :"
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.Location = New System.Drawing.Point(62, 110)
        Me.Label1.TabIndex = 24
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
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label8.Text = "Request By :"
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.Location = New System.Drawing.Point(44, 64)
        Me.Label8.TabIndex = 23
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
        Me.lblSupplierName.Size = New System.Drawing.Size(337, 19)
        Me.lblSupplierName.Location = New System.Drawing.Point(200, 38)
        Me.lblSupplierName.TabIndex = 4
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
        Me._Label3_0.Location = New System.Drawing.Point(26, 41)
        Me._Label3_0.TabIndex = 22
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
        Me.AdoDCMain.Location = New System.Drawing.Point(56, 106)
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
        Me.SprdView.Size = New System.Drawing.Size(655, 147)
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.TabIndex = 20
        Me.SprdView.Name = "SprdView"
        Me.Frame3.Size = New System.Drawing.Size(655, 53)
        Me.Frame3.Location = New System.Drawing.Point(0, 142)
        Me.Frame3.TabIndex = 19
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
        Me.cmdAdd.Location = New System.Drawing.Point(22, 12)
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.TabIndex = 10
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
        Me.cmdModify.Location = New System.Drawing.Point(89, 12)
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.TabIndex = 11
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
        Me.cmdSave.Location = New System.Drawing.Point(156, 12)
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.TabIndex = 12
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
        Me.cmdDelete.Location = New System.Drawing.Point(223, 12)
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.TabIndex = 13
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
        Me.CmdView.Location = New System.Drawing.Point(490, 12)
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.TabIndex = 17
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
        Me.cmdPrint.Location = New System.Drawing.Point(357, 12)
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.TabIndex = 15
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
        Me.CmdPreview.Location = New System.Drawing.Point(424, 12)
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.TabIndex = 16
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(291, 12)
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.TabIndex = 14
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
        Me.cmdClose.Location = New System.Drawing.Point(558, 12)
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.TabIndex = 18
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
        Me.Label3.SetIndex(_Label3_0, CType(0, Short))
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(FraFront)
        Me.Controls.Add(AdoDCMain)
        Me.Controls.Add(SprdView)
        Me.Controls.Add(Frame3)
        Me.FraFront.Controls.Add(txtMRRDate)
        Me.FraFront.Controls.Add(txtMRRNo)
        Me.FraFront.Controls.Add(cmdMRRSearch)
        Me.FraFront.Controls.Add(cmdRequestSearch)
        Me.FraFront.Controls.Add(txtRequestCode)
        Me.FraFront.Controls.Add(txtReason)
        Me.FraFront.Controls.Add(txtAuthorityName)
        Me.FraFront.Controls.Add(txtRequestName)
        Me.FraFront.Controls.Add(txtSupplier)
        Me.FraFront.Controls.Add(Label15)
        Me.FraFront.Controls.Add(Label14)
        Me.FraFront.Controls.Add(Label2)
        Me.FraFront.Controls.Add(Label1)
        Me.FraFront.Controls.Add(Label8)
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
        'SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class