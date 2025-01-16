Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGatePassGST
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
    Public WithEvents cboGSTStatus As System.Windows.Forms.ComboBox
    Public WithEvents cmdServProvided As System.Windows.Forms.Button
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents cboPurpose As System.Windows.Forms.ComboBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtEmp As System.Windows.Forms.TextBox
    Public WithEvents txtAddress As System.Windows.Forms.TextBox
    Public WithEvents txtReturnDate As System.Windows.Forms.TextBox
    Public WithEvents txtST38No As System.Windows.Forms.TextBox
    Public WithEvents txtSuppName As System.Windows.Forms.TextBox
    Public WithEvents txtAuthorityName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchRgp As System.Windows.Forms.Button
    Public WithEvents cboGatePasstype As System.Windows.Forms.ComboBox
    Public WithEvents txtRgpreqno As System.Windows.Forms.TextBox
    Public WithEvents txtPrepare As System.Windows.Forms.TextBox
    Public WithEvents txtSuppcode As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtGatepassno As System.Windows.Forms.TextBox
    Public WithEvents txtAuthority As System.Windows.Forms.TextBox
    Public WithEvents cboMaterial As System.Windows.Forms.ComboBox
    Public WithEvents txtF4no As System.Windows.Forms.TextBox
    Public WithEvents chkF4status As System.Windows.Forms.CheckBox
    Public WithEvents txtRgpreqdate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchauth As System.Windows.Forms.Button
    Public WithEvents txtVehicle As System.Windows.Forms.TextBox
    Public WithEvents txtChallanno As System.Windows.Forms.TextBox
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents txtGatePassDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents txtRemoval As System.Windows.Forms.TextBox
    'Public WithEvents MSComm1 As AxMSCommLib.AxMSComm
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblDept As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents cmdBarCode As System.Windows.Forms.Button
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
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGatePassGST))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdServProvided = New System.Windows.Forms.Button()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.cmdSearchRgp = New System.Windows.Forms.Button()
        Me.cmdSearchauth = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdBarCode = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.cmdeWayBill = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.txtDistance = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txteWayBillNo = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.TxtGRDate = New System.Windows.Forms.TextBox()
        Me.TxtGRNo = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtTransportCode = New System.Windows.Forms.TextBox()
        Me.txtCarriers = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.cboVehicleType = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cboTransmode = New System.Windows.Forms.ComboBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.txtPrefix = New System.Windows.Forms.TextBox()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.cboGSTStatus = New System.Windows.Forms.ComboBox()
        Me.cboPurpose = New System.Windows.Forms.ComboBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtEmp = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtReturnDate = New System.Windows.Forms.TextBox()
        Me.txtST38No = New System.Windows.Forms.TextBox()
        Me.txtSuppName = New System.Windows.Forms.TextBox()
        Me.txtAuthorityName = New System.Windows.Forms.TextBox()
        Me.cboGatePasstype = New System.Windows.Forms.ComboBox()
        Me.txtRgpreqno = New System.Windows.Forms.TextBox()
        Me.txtPrepare = New System.Windows.Forms.TextBox()
        Me.txtSuppcode = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtGatepassno = New System.Windows.Forms.TextBox()
        Me.txtAuthority = New System.Windows.Forms.TextBox()
        Me.cboMaterial = New System.Windows.Forms.ComboBox()
        Me.txtF4no = New System.Windows.Forms.TextBox()
        Me.chkF4status = New System.Windows.Forms.CheckBox()
        Me.txtRgpreqdate = New System.Windows.Forms.TextBox()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.txtChallanno = New System.Windows.Forms.TextBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.txtGatePassDate = New System.Windows.Forms.TextBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.txtRemoval = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblDept = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.FraFront.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdServProvided
        '
        Me.cmdServProvided.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdServProvided.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdServProvided.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdServProvided.Image = CType(resources.GetObject("cmdServProvided.Image"), System.Drawing.Image)
        Me.cmdServProvided.Location = New System.Drawing.Point(494, 478)
        Me.cmdServProvided.Name = "cmdServProvided"
        Me.cmdServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdServProvided.Size = New System.Drawing.Size(27, 19)
        Me.cmdServProvided.TabIndex = 30
        Me.cmdServProvided.TabStop = False
        Me.cmdServProvided.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdServProvided, "Search")
        Me.cmdServProvided.UseVisualStyleBackColor = False
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(128, 478)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(365, 22)
        Me.txtServProvided.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.txtServProvided, "Press F1 For Help")
        '
        'cmdSearchRgp
        '
        Me.cmdSearchRgp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchRgp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchRgp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchRgp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchRgp.Image = CType(resources.GetObject("cmdSearchRgp.Image"), System.Drawing.Image)
        Me.cmdSearchRgp.Location = New System.Drawing.Point(214, 68)
        Me.cmdSearchRgp.Name = "cmdSearchRgp"
        Me.cmdSearchRgp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchRgp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchRgp.TabIndex = 8
        Me.cmdSearchRgp.TabStop = False
        Me.cmdSearchRgp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchRgp, "Search")
        Me.cmdSearchRgp.UseVisualStyleBackColor = False
        '
        'cmdSearchauth
        '
        Me.cmdSearchauth.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchauth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchauth.Enabled = False
        Me.cmdSearchauth.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchauth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchauth.Image = CType(resources.GetObject("cmdSearchauth.Image"), System.Drawing.Image)
        Me.cmdSearchauth.Location = New System.Drawing.Point(214, 95)
        Me.cmdSearchauth.Name = "cmdSearchauth"
        Me.cmdSearchauth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchauth.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchauth.TabIndex = 12
        Me.cmdSearchauth.TabStop = False
        Me.cmdSearchauth.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchauth, "Search")
        Me.cmdSearchauth.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(214, 13)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdBarCode
        '
        Me.cmdBarCode.BackColor = System.Drawing.SystemColors.Control
        Me.cmdBarCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBarCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBarCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBarCode.Image = CType(resources.GetObject("cmdBarCode.Image"), System.Drawing.Image)
        Me.cmdBarCode.Location = New System.Drawing.Point(406, 12)
        Me.cmdBarCode.Name = "cmdBarCode"
        Me.cmdBarCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBarCode.Size = New System.Drawing.Size(56, 37)
        Me.cmdBarCode.TabIndex = 68
        Me.cmdBarCode.Text = "&Barcode"
        Me.cmdBarCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBarCode, "Delete")
        Me.cmdBarCode.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(728, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 40
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(662, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 39
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(596, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 38
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(529, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 37
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(463, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 36
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(339, 12)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 35
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(272, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 34
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(205, 12)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 33
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(138, 12)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(419, 236)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(44, 14)
        Me.Label22.TabIndex = 236
        Me.Label22.Text = "GR No :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label22, "AWB/RRP No.")
        '
        'cmdeWayBill
        '
        Me.cmdeWayBill.BackColor = System.Drawing.Color.Bisque
        Me.cmdeWayBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdeWayBill.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdeWayBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdeWayBill.Location = New System.Drawing.Point(364, 551)
        Me.cmdeWayBill.Name = "cmdeWayBill"
        Me.cmdeWayBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdeWayBill.Size = New System.Drawing.Size(160, 22)
        Me.cmdeWayBill.TabIndex = 251
        Me.cmdeWayBill.Text = "Print e-Way Bill"
        Me.cmdeWayBill.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdeWayBill, "Delete")
        Me.cmdeWayBill.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.txtDistance)
        Me.FraFront.Controls.Add(Me.Label46)
        Me.FraFront.Controls.Add(Me.cmdeWayBill)
        Me.FraFront.Controls.Add(Me.txteWayBillNo)
        Me.FraFront.Controls.Add(Me.Label23)
        Me.FraFront.Controls.Add(Me.lblModDate)
        Me.FraFront.Controls.Add(Me.Label54)
        Me.FraFront.Controls.Add(Me.lblAddDate)
        Me.FraFront.Controls.Add(Me.Label55)
        Me.FraFront.Controls.Add(Me.lblModUser)
        Me.FraFront.Controls.Add(Me.Label56)
        Me.FraFront.Controls.Add(Me.lblAddUser)
        Me.FraFront.Controls.Add(Me.Label57)
        Me.FraFront.Controls.Add(Me.TxtGRDate)
        Me.FraFront.Controls.Add(Me.TxtGRNo)
        Me.FraFront.Controls.Add(Me.Label15)
        Me.FraFront.Controls.Add(Me.Label22)
        Me.FraFront.Controls.Add(Me.txtTransportCode)
        Me.FraFront.Controls.Add(Me.txtCarriers)
        Me.FraFront.Controls.Add(Me.Label42)
        Me.FraFront.Controls.Add(Me.Label30)
        Me.FraFront.Controls.Add(Me.cboVehicleType)
        Me.FraFront.Controls.Add(Me.Label14)
        Me.FraFront.Controls.Add(Me.cboTransmode)
        Me.FraFront.Controls.Add(Me.Label49)
        Me.FraFront.Controls.Add(Me.txtPrefix)
        Me.FraFront.Controls.Add(Me.txtBillTo)
        Me.FraFront.Controls.Add(Me.cboGSTStatus)
        Me.FraFront.Controls.Add(Me.cmdServProvided)
        Me.FraFront.Controls.Add(Me.txtServProvided)
        Me.FraFront.Controls.Add(Me.cboPurpose)
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.txtEmp)
        Me.FraFront.Controls.Add(Me.txtAddress)
        Me.FraFront.Controls.Add(Me.txtReturnDate)
        Me.FraFront.Controls.Add(Me.txtST38No)
        Me.FraFront.Controls.Add(Me.txtSuppName)
        Me.FraFront.Controls.Add(Me.txtAuthorityName)
        Me.FraFront.Controls.Add(Me.cmdSearchRgp)
        Me.FraFront.Controls.Add(Me.cboGatePasstype)
        Me.FraFront.Controls.Add(Me.txtRgpreqno)
        Me.FraFront.Controls.Add(Me.txtPrepare)
        Me.FraFront.Controls.Add(Me.txtSuppcode)
        Me.FraFront.Controls.Add(Me.txtRemarks)
        Me.FraFront.Controls.Add(Me.txtGatepassno)
        Me.FraFront.Controls.Add(Me.txtAuthority)
        Me.FraFront.Controls.Add(Me.cboMaterial)
        Me.FraFront.Controls.Add(Me.txtF4no)
        Me.FraFront.Controls.Add(Me.chkF4status)
        Me.FraFront.Controls.Add(Me.txtRgpreqdate)
        Me.FraFront.Controls.Add(Me.cmdSearchauth)
        Me.FraFront.Controls.Add(Me.txtVehicle)
        Me.FraFront.Controls.Add(Me.txtChallanno)
        Me.FraFront.Controls.Add(Me.cboStatus)
        Me.FraFront.Controls.Add(Me.txtGatePassDate)
        Me.FraFront.Controls.Add(Me.cmdSearch)
        Me.FraFront.Controls.Add(Me.Frame6)
        Me.FraFront.Controls.Add(Me.txtRemoval)
        Me.FraFront.Controls.Add(Me.Label32)
        Me.FraFront.Controls.Add(Me.Label17)
        Me.FraFront.Controls.Add(Me.Label21)
        Me.FraFront.Controls.Add(Me.Label20)
        Me.FraFront.Controls.Add(Me.Label19)
        Me.FraFront.Controls.Add(Me.Label18)
        Me.FraFront.Controls.Add(Me.Label16)
        Me.FraFront.Controls.Add(Me.lblDept)
        Me.FraFront.Controls.Add(Me.Label13)
        Me.FraFront.Controls.Add(Me.Label12)
        Me.FraFront.Controls.Add(Me.Label11)
        Me.FraFront.Controls.Add(Me.Label10)
        Me.FraFront.Controls.Add(Me.Label9)
        Me.FraFront.Controls.Add(Me.Label8)
        Me.FraFront.Controls.Add(Me.Label6)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label3)
        Me.FraFront.Controls.Add(Me.Label1)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Label4)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Controls.Add(Me.Label5)
        Me.FraFront.Controls.Add(Me.Label38)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -2)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(904, 576)
        Me.FraFront.TabIndex = 44
        Me.FraFront.TabStop = False
        '
        'txtDistance
        '
        Me.txtDistance.AcceptsReturn = True
        Me.txtDistance.BackColor = System.Drawing.SystemColors.Window
        Me.txtDistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDistance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDistance.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDistance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDistance.Location = New System.Drawing.Point(812, 232)
        Me.txtDistance.MaxLength = 0
        Me.txtDistance.Name = "txtDistance"
        Me.txtDistance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDistance.Size = New System.Drawing.Size(83, 20)
        Me.txtDistance.TabIndex = 257
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(754, 234)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(55, 14)
        Me.Label46.TabIndex = 258
        Me.Label46.Text = "Distance :"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txteWayBillNo
        '
        Me.txteWayBillNo.AcceptsReturn = True
        Me.txteWayBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txteWayBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteWayBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteWayBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteWayBillNo.ForeColor = System.Drawing.Color.Blue
        Me.txteWayBillNo.Location = New System.Drawing.Point(367, 525)
        Me.txteWayBillNo.MaxLength = 0
        Me.txteWayBillNo.Name = "txteWayBillNo"
        Me.txteWayBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteWayBillNo.Size = New System.Drawing.Size(153, 22)
        Me.txteWayBillNo.TabIndex = 255
        Me.txteWayBillNo.Text = " "
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(293, 527)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(76, 13)
        Me.Label23.TabIndex = 256
        Me.Label23.Text = "eWay Bill No:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(830, 553)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 254
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.SystemColors.Control
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label54.Location = New System.Drawing.Point(764, 555)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(62, 15)
        Me.Label54.TabIndex = 253
        Me.Label54.Text = "Mod Date:"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(830, 535)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 252
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.BackColor = System.Drawing.SystemColors.Control
        Me.Label55.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label55.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label55.Location = New System.Drawing.Point(763, 537)
        Me.Label55.Name = "Label55"
        Me.Label55.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label55.Size = New System.Drawing.Size(63, 15)
        Me.Label55.TabIndex = 251
        Me.Label55.Text = "Add Date :"
        Me.Label55.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(634, 553)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 250
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.SystemColors.Control
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(567, 555)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(63, 15)
        Me.Label56.TabIndex = 249
        Me.Label56.Text = "Mod User:"
        Me.Label56.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(634, 535)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 248
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.BackColor = System.Drawing.SystemColors.Control
        Me.Label57.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label57.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label57.Location = New System.Drawing.Point(568, 537)
        Me.Label57.Name = "Label57"
        Me.Label57.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label57.Size = New System.Drawing.Size(64, 15)
        Me.Label57.TabIndex = 247
        Me.Label57.Text = "Add User :"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TxtGRDate
        '
        Me.TxtGRDate.AcceptsReturn = True
        Me.TxtGRDate.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGRDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRDate.ForeColor = System.Drawing.Color.Blue
        Me.TxtGRDate.Location = New System.Drawing.Point(646, 232)
        Me.TxtGRDate.MaxLength = 0
        Me.TxtGRDate.Name = "TxtGRDate"
        Me.TxtGRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGRDate.Size = New System.Drawing.Size(103, 20)
        Me.TxtGRDate.TabIndex = 235
        '
        'TxtGRNo
        '
        Me.TxtGRNo.AcceptsReturn = True
        Me.TxtGRNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRNo.ForeColor = System.Drawing.Color.Blue
        Me.TxtGRNo.Location = New System.Drawing.Point(467, 232)
        Me.TxtGRNo.MaxLength = 0
        Me.TxtGRNo.Name = "TxtGRNo"
        Me.TxtGRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGRNo.Size = New System.Drawing.Size(113, 20)
        Me.TxtGRNo.TabIndex = 234
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(589, 236)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(53, 14)
        Me.Label15.TabIndex = 237
        Me.Label15.Text = "GR Date :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTransportCode
        '
        Me.txtTransportCode.AcceptsReturn = True
        Me.txtTransportCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransportCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransportCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransportCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransportCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTransportCode.Location = New System.Drawing.Point(634, 499)
        Me.txtTransportCode.MaxLength = 0
        Me.txtTransportCode.Name = "txtTransportCode"
        Me.txtTransportCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransportCode.Size = New System.Drawing.Size(267, 20)
        Me.txtTransportCode.TabIndex = 231
        '
        'txtCarriers
        '
        Me.txtCarriers.AcceptsReturn = True
        Me.txtCarriers.BackColor = System.Drawing.SystemColors.Window
        Me.txtCarriers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCarriers.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCarriers.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarriers.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCarriers.Location = New System.Drawing.Point(634, 477)
        Me.txtCarriers.MaxLength = 0
        Me.txtCarriers.Name = "txtCarriers"
        Me.txtCarriers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCarriers.Size = New System.Drawing.Size(267, 20)
        Me.txtCarriers.TabIndex = 230
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(559, 501)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(72, 14)
        Me.Label42.TabIndex = 233
        Me.Label42.Text = "Transport ID :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(531, 481)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(100, 14)
        Me.Label30.TabIndex = 232
        Me.Label30.Text = "Transporter Name :"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboVehicleType
        '
        Me.cboVehicleType.BackColor = System.Drawing.SystemColors.Window
        Me.cboVehicleType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVehicleType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboVehicleType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboVehicleType.Location = New System.Drawing.Point(320, 232)
        Me.cboVehicleType.Name = "cboVehicleType"
        Me.cboVehicleType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboVehicleType.Size = New System.Drawing.Size(91, 22)
        Me.cboVehicleType.TabIndex = 24
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(243, 234)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(74, 14)
        Me.Label14.TabIndex = 229
        Me.Label14.Text = "Vehicle Type :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboTransmode
        '
        Me.cboTransmode.BackColor = System.Drawing.SystemColors.Window
        Me.cboTransmode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTransmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTransmode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTransmode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTransmode.Location = New System.Drawing.Point(120, 232)
        Me.cboTransmode.Name = "cboTransmode"
        Me.cboTransmode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTransmode.Size = New System.Drawing.Size(112, 22)
        Me.cboTransmode.TabIndex = 23
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.BackColor = System.Drawing.SystemColors.Control
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(47, 236)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(70, 14)
        Me.Label49.TabIndex = 227
        Me.Label49.Text = "Trans Mode :"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPrefix
        '
        Me.txtPrefix.AcceptsReturn = True
        Me.txtPrefix.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrefix.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrefix.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrefix.Enabled = False
        Me.txtPrefix.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!)
        Me.txtPrefix.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPrefix.Location = New System.Drawing.Point(712, 95)
        Me.txtPrefix.MaxLength = 0
        Me.txtPrefix.Name = "txtPrefix"
        Me.txtPrefix.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrefix.Size = New System.Drawing.Size(60, 22)
        Me.txtPrefix.TabIndex = 146
        '
        'txtBillTo
        '
        Me.txtBillTo.AcceptsReturn = True
        Me.txtBillTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillTo.Enabled = False
        Me.txtBillTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillTo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillTo.Location = New System.Drawing.Point(532, 122)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(102, 22)
        Me.txtBillTo.TabIndex = 145
        '
        'cboGSTStatus
        '
        Me.cboGSTStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboGSTStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGSTStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGSTStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGSTStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGSTStatus.Location = New System.Drawing.Point(120, 204)
        Me.cboGSTStatus.Name = "cboGSTStatus"
        Me.cboGSTStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGSTStatus.Size = New System.Drawing.Size(113, 21)
        Me.cboGSTStatus.TabIndex = 73
        '
        'cboPurpose
        '
        Me.cboPurpose.BackColor = System.Drawing.SystemColors.Window
        Me.cboPurpose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPurpose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurpose.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPurpose.Location = New System.Drawing.Point(713, 41)
        Me.cboPurpose.Name = "cboPurpose"
        Me.cboPurpose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPurpose.Size = New System.Drawing.Size(182, 21)
        Me.cboPurpose.TabIndex = 6
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(120, 41)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(231, 21)
        Me.cboDivision.TabIndex = 5
        '
        'txtEmp
        '
        Me.txtEmp.AcceptsReturn = True
        Me.txtEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmp.Enabled = False
        Me.txtEmp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmp.ForeColor = System.Drawing.Color.Blue
        Me.txtEmp.Location = New System.Drawing.Point(713, 204)
        Me.txtEmp.MaxLength = 0
        Me.txtEmp.Name = "txtEmp"
        Me.txtEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmp.Size = New System.Drawing.Size(182, 22)
        Me.txtEmp.TabIndex = 22
        Me.txtEmp.Text = " "
        '
        'txtAddress
        '
        Me.txtAddress.AcceptsReturn = True
        Me.txtAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddress.Enabled = False
        Me.txtAddress.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.Color.Blue
        Me.txtAddress.Location = New System.Drawing.Point(238, 149)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress.Size = New System.Drawing.Size(293, 43)
        Me.txtAddress.TabIndex = 67
        '
        'txtReturnDate
        '
        Me.txtReturnDate.AcceptsReturn = True
        Me.txtReturnDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReturnDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReturnDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReturnDate.Enabled = False
        Me.txtReturnDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReturnDate.ForeColor = System.Drawing.Color.Blue
        Me.txtReturnDate.Location = New System.Drawing.Point(120, 176)
        Me.txtReturnDate.MaxLength = 0
        Me.txtReturnDate.Name = "txtReturnDate"
        Me.txtReturnDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReturnDate.Size = New System.Drawing.Size(93, 22)
        Me.txtReturnDate.TabIndex = 23
        '
        'txtST38No
        '
        Me.txtST38No.AcceptsReturn = True
        Me.txtST38No.BackColor = System.Drawing.SystemColors.Window
        Me.txtST38No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtST38No.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtST38No.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtST38No.ForeColor = System.Drawing.Color.Blue
        Me.txtST38No.Location = New System.Drawing.Point(128, 525)
        Me.txtST38No.MaxLength = 0
        Me.txtST38No.Name = "txtST38No"
        Me.txtST38No.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtST38No.Size = New System.Drawing.Size(157, 22)
        Me.txtST38No.TabIndex = 32
        Me.txtST38No.Text = " "
        '
        'txtSuppName
        '
        Me.txtSuppName.AcceptsReturn = True
        Me.txtSuppName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppName.Enabled = False
        Me.txtSuppName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppName.ForeColor = System.Drawing.Color.Blue
        Me.txtSuppName.Location = New System.Drawing.Point(238, 122)
        Me.txtSuppName.MaxLength = 0
        Me.txtSuppName.Name = "txtSuppName"
        Me.txtSuppName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppName.Size = New System.Drawing.Size(293, 22)
        Me.txtSuppName.TabIndex = 16
        Me.txtSuppName.Text = " "
        '
        'txtAuthorityName
        '
        Me.txtAuthorityName.AcceptsReturn = True
        Me.txtAuthorityName.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuthorityName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthorityName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuthorityName.Enabled = False
        Me.txtAuthorityName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthorityName.ForeColor = System.Drawing.Color.Blue
        Me.txtAuthorityName.Location = New System.Drawing.Point(238, 95)
        Me.txtAuthorityName.MaxLength = 0
        Me.txtAuthorityName.Name = "txtAuthorityName"
        Me.txtAuthorityName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuthorityName.Size = New System.Drawing.Size(293, 22)
        Me.txtAuthorityName.TabIndex = 13
        Me.txtAuthorityName.Text = " "
        '
        'cboGatePasstype
        '
        Me.cboGatePasstype.BackColor = System.Drawing.SystemColors.Window
        Me.cboGatePasstype.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGatePasstype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGatePasstype.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGatePasstype.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGatePasstype.Location = New System.Drawing.Point(120, 149)
        Me.cboGatePasstype.Name = "cboGatePasstype"
        Me.cboGatePasstype.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGatePasstype.Size = New System.Drawing.Size(95, 21)
        Me.cboGatePasstype.TabIndex = 18
        '
        'txtRgpreqno
        '
        Me.txtRgpreqno.AcceptsReturn = True
        Me.txtRgpreqno.BackColor = System.Drawing.SystemColors.Window
        Me.txtRgpreqno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRgpreqno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRgpreqno.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRgpreqno.ForeColor = System.Drawing.Color.Blue
        Me.txtRgpreqno.Location = New System.Drawing.Point(120, 68)
        Me.txtRgpreqno.MaxLength = 0
        Me.txtRgpreqno.Name = "txtRgpreqno"
        Me.txtRgpreqno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRgpreqno.Size = New System.Drawing.Size(93, 22)
        Me.txtRgpreqno.TabIndex = 7
        Me.txtRgpreqno.Text = " "
        '
        'txtPrepare
        '
        Me.txtPrepare.AcceptsReturn = True
        Me.txtPrepare.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrepare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrepare.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrepare.Enabled = False
        Me.txtPrepare.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrepare.ForeColor = System.Drawing.Color.Blue
        Me.txtPrepare.Location = New System.Drawing.Point(713, 176)
        Me.txtPrepare.MaxLength = 0
        Me.txtPrepare.Name = "txtPrepare"
        Me.txtPrepare.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrepare.Size = New System.Drawing.Size(182, 22)
        Me.txtPrepare.TabIndex = 21
        Me.txtPrepare.Text = " "
        '
        'txtSuppcode
        '
        Me.txtSuppcode.AcceptsReturn = True
        Me.txtSuppcode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppcode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppcode.Enabled = False
        Me.txtSuppcode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppcode.ForeColor = System.Drawing.Color.Blue
        Me.txtSuppcode.Location = New System.Drawing.Point(120, 122)
        Me.txtSuppcode.MaxLength = 0
        Me.txtSuppcode.Name = "txtSuppcode"
        Me.txtSuppcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppcode.Size = New System.Drawing.Size(93, 22)
        Me.txtSuppcode.TabIndex = 15
        Me.txtSuppcode.Text = " "
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtRemarks.Location = New System.Drawing.Point(128, 501)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(391, 22)
        Me.txtRemarks.TabIndex = 31
        Me.txtRemarks.Text = " "
        '
        'txtGatepassno
        '
        Me.txtGatepassno.AcceptsReturn = True
        Me.txtGatepassno.BackColor = System.Drawing.SystemColors.Window
        Me.txtGatepassno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGatepassno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGatepassno.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGatepassno.ForeColor = System.Drawing.Color.Blue
        Me.txtGatepassno.Location = New System.Drawing.Point(120, 13)
        Me.txtGatepassno.MaxLength = 0
        Me.txtGatepassno.Name = "txtGatepassno"
        Me.txtGatepassno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGatepassno.Size = New System.Drawing.Size(93, 22)
        Me.txtGatepassno.TabIndex = 1
        '
        'txtAuthority
        '
        Me.txtAuthority.AcceptsReturn = True
        Me.txtAuthority.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuthority.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthority.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuthority.Enabled = False
        Me.txtAuthority.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthority.ForeColor = System.Drawing.Color.Blue
        Me.txtAuthority.Location = New System.Drawing.Point(120, 95)
        Me.txtAuthority.MaxLength = 0
        Me.txtAuthority.Name = "txtAuthority"
        Me.txtAuthority.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuthority.Size = New System.Drawing.Size(93, 22)
        Me.txtAuthority.TabIndex = 11
        Me.txtAuthority.Text = " "
        '
        'cboMaterial
        '
        Me.cboMaterial.BackColor = System.Drawing.SystemColors.Window
        Me.cboMaterial.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaterial.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMaterial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMaterial.Location = New System.Drawing.Point(713, 68)
        Me.cboMaterial.Name = "cboMaterial"
        Me.cboMaterial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMaterial.Size = New System.Drawing.Size(182, 21)
        Me.cboMaterial.TabIndex = 10
        '
        'txtF4no
        '
        Me.txtF4no.AcceptsReturn = True
        Me.txtF4no.BackColor = System.Drawing.SystemColors.Window
        Me.txtF4no.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtF4no.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtF4no.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtF4no.ForeColor = System.Drawing.Color.Blue
        Me.txtF4no.Location = New System.Drawing.Point(352, 204)
        Me.txtF4no.MaxLength = 0
        Me.txtF4no.Name = "txtF4no"
        Me.txtF4no.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtF4no.Size = New System.Drawing.Size(93, 22)
        Me.txtF4no.TabIndex = 20
        Me.txtF4no.Text = " "
        '
        'chkF4status
        '
        Me.chkF4status.BackColor = System.Drawing.SystemColors.Control
        Me.chkF4status.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkF4status.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkF4status.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkF4status.Location = New System.Drawing.Point(448, 206)
        Me.chkF4status.Name = "chkF4status"
        Me.chkF4status.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkF4status.Size = New System.Drawing.Size(81, 17)
        Me.chkF4status.TabIndex = 41
        Me.chkF4status.Text = "(Yes/No)"
        Me.chkF4status.UseVisualStyleBackColor = False
        '
        'txtRgpreqdate
        '
        Me.txtRgpreqdate.AcceptsReturn = True
        Me.txtRgpreqdate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRgpreqdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRgpreqdate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRgpreqdate.Enabled = False
        Me.txtRgpreqdate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRgpreqdate.ForeColor = System.Drawing.Color.Blue
        Me.txtRgpreqdate.Location = New System.Drawing.Point(446, 68)
        Me.txtRgpreqdate.MaxLength = 0
        Me.txtRgpreqdate.Name = "txtRgpreqdate"
        Me.txtRgpreqdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRgpreqdate.Size = New System.Drawing.Size(83, 22)
        Me.txtRgpreqdate.TabIndex = 9
        Me.txtRgpreqdate.Text = " "
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.ForeColor = System.Drawing.Color.Blue
        Me.txtVehicle.Location = New System.Drawing.Point(713, 122)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(182, 22)
        Me.txtVehicle.TabIndex = 17
        Me.txtVehicle.Text = " "
        '
        'txtChallanno
        '
        Me.txtChallanno.AcceptsReturn = True
        Me.txtChallanno.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanno.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanno.ForeColor = System.Drawing.Color.Blue
        Me.txtChallanno.Location = New System.Drawing.Point(773, 95)
        Me.txtChallanno.MaxLength = 0
        Me.txtChallanno.Name = "txtChallanno"
        Me.txtChallanno.ReadOnly = True
        Me.txtChallanno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanno.Size = New System.Drawing.Size(121, 22)
        Me.txtChallanno.TabIndex = 14
        Me.txtChallanno.Text = " "
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(713, 15)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(182, 21)
        Me.cboStatus.TabIndex = 4
        '
        'txtGatePassDate
        '
        Me.txtGatePassDate.AcceptsReturn = True
        Me.txtGatePassDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtGatePassDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGatePassDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGatePassDate.Enabled = False
        Me.txtGatePassDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGatePassDate.ForeColor = System.Drawing.Color.Blue
        Me.txtGatePassDate.Location = New System.Drawing.Point(446, 13)
        Me.txtGatePassDate.MaxLength = 0
        Me.txtGatePassDate.Name = "txtGatePassDate"
        Me.txtGatePassDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGatePassDate.Size = New System.Drawing.Size(83, 22)
        Me.txtGatePassDate.TabIndex = 3
        Me.txtGatePassDate.Text = " "
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 250)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(904, 227)
        Me.Frame6.TabIndex = 46
        Me.Frame6.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(3, 10)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(898, 210)
        Me.SprdMain.TabIndex = 28
        '
        'txtRemoval
        '
        Me.txtRemoval.AcceptsReturn = True
        Me.txtRemoval.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemoval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemoval.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemoval.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemoval.ForeColor = System.Drawing.Color.Blue
        Me.txtRemoval.Location = New System.Drawing.Point(713, 149)
        Me.txtRemoval.MaxLength = 0
        Me.txtRemoval.Name = "txtRemoval"
        Me.txtRemoval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemoval.Size = New System.Drawing.Size(182, 22)
        Me.txtRemoval.TabIndex = 19
        Me.txtRemoval.Text = " "
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(48, 208)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(67, 13)
        Me.Label32.TabIndex = 74
        Me.Label32.Text = "GST Status :"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(20, 480)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(95, 13)
        Me.Label17.TabIndex = 72
        Me.Label17.Text = "Service Provider :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(636, 45)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(72, 13)
        Me.Label21.TabIndex = 71
        Me.Label21.Text = "Purpose for :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(61, 45)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(54, 13)
        Me.Label20.TabIndex = 70
        Me.Label20.Text = "Division :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(607, 205)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(101, 13)
        Me.Label19.TabIndex = 69
        Me.Label19.Text = "Responsible Emp :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(39, 179)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(76, 13)
        Me.Label18.TabIndex = 66
        Me.Label18.Text = " Return Date :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(57, 527)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(52, 13)
        Me.Label16.TabIndex = 65
        Me.Label16.Text = "Packing :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDept
        '
        Me.lblDept.AutoSize = True
        Me.lblDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDept.Location = New System.Drawing.Point(310, 74)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDept.Size = New System.Drawing.Size(45, 13)
        Me.lblDept.TabIndex = 62
        Me.lblDept.Text = "lblDept"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(26, 153)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(89, 13)
        Me.Label13.TabIndex = 61
        Me.Label13.Text = "Gate Pass Type :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(623, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(85, 13)
        Me.Label12.TabIndex = 60
        Me.Label12.Text = "Material From :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(238, 205)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(104, 13)
        Me.Label11.TabIndex = 59
        Me.Label11.Text = "Outward 57F4 No. :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(384, 70)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(37, 13)
        Me.Label10.TabIndex = 58
        Me.Label10.Text = "Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(34, 72)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 57
        Me.Label9.Text = "RGP Req. No. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(20, 98)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(95, 13)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "Auth. Emp Code :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(639, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Vehicle No. :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(32, 13)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(83, 13)
        Me.lblCust.TabIndex = 54
        Me.lblCust.Text = "Gate Pass No. :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(637, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 53
        Me.Label3.Text = "Challan No. :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(633, 179)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Prepared By :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(30, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Supplier Code :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(48, 502)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(57, 13)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Remarks :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(384, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(640, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 47
        Me.Label5.Text = "RGP Status :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(619, 151)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(89, 13)
        Me.Label38.TabIndex = 45
        Me.Label38.Text = "Removal Time : "
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.cmdBarCode)
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
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(1, 568)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(905, 53)
        Me.Frame3.TabIndex = 42
        Me.Frame3.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(52, 34)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(71, 13)
        Me.lblBookType.TabIndex = 70
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 69
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
        Me.lblMKey.TabIndex = 49
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'UltraGrid1
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.Color.White
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance1
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.White
        Appearance2.BackColor2 = System.Drawing.Color.White
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.UltraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.GroupByBox.Hidden = True
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UltraGrid1.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UltraGrid1.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance8
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.UltraGrid1.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UltraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance11
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.UltraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(904, 570)
        Me.UltraGrid1.TabIndex = 79
        '
        'frmGatePassGST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(906, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.UltraGrid1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGatePassGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "RGP / NRGP Gate Pass (Under GST)"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents txtBillTo As TextBox
    Public WithEvents txtPrefix As TextBox
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Public WithEvents cboTransmode As ComboBox
    Public WithEvents Label49 As Label
    Public WithEvents cboVehicleType As ComboBox
    Public WithEvents Label14 As Label
    Public WithEvents txtTransportCode As TextBox
    Public WithEvents txtCarriers As TextBox
    Public WithEvents Label42 As Label
    Public WithEvents Label30 As Label
    Public WithEvents TxtGRDate As TextBox
    Public WithEvents TxtGRNo As TextBox
    Public WithEvents Label15 As Label
    Public WithEvents Label22 As Label
    Public WithEvents lblModDate As Label
    Public WithEvents Label54 As Label
    Public WithEvents lblAddDate As Label
    Public WithEvents Label55 As Label
    Public WithEvents lblModUser As Label
    Public WithEvents Label56 As Label
    Public WithEvents lblAddUser As Label
    Public WithEvents Label57 As Label
    Public WithEvents txteWayBillNo As TextBox
    Public WithEvents Label23 As Label
    Public WithEvents cmdeWayBill As Button
    Public WithEvents txtDistance As TextBox
    Public WithEvents Label46 As Label
#End Region
End Class