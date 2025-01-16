Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmDeliveryInstruction
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
    Public WithEvents chkShipTo As System.Windows.Forms.CheckBox
    Public WithEvents TxtShipTo As System.Windows.Forms.TextBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtAddress As System.Windows.Forms.TextBox
    Public WithEvents TxtGRNo As System.Windows.Forms.TextBox
    Public WithEvents TxtGRDate As System.Windows.Forms.TextBox
    Public WithEvents txtPrepared As System.Windows.Forms.TextBox
    Public WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Public WithEvents txtCustPODate As System.Windows.Forms.TextBox
    Public WithEvents txtCustPoNo As System.Windows.Forms.TextBox
    Public WithEvents txtSODate As System.Windows.Forms.TextBox
    Public WithEvents txtSONo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSo As System.Windows.Forms.Button
    Public WithEvents txtVehicleNo As System.Windows.Forms.TextBox
    Public WithEvents txtLoadingTime As System.Windows.Forms.TextBox
    Public WithEvents cboRefType As System.Windows.Forms.ComboBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtTransporter As System.Windows.Forms.TextBox
    Public WithEvents TxtCustomerName As System.Windows.Forms.TextBox
    Public WithEvents txtDNNo As System.Windows.Forms.TextBox
    Public WithEvents txtDNDate As System.Windows.Forms.TextBox
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents lblDespType As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents LblMkey As System.Windows.Forms.Label
    Public WithEvents Frasupp As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frasprd As System.Windows.Forms.GroupBox
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
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDeliveryInstruction))
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
        Me.cmdSearchSo = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdGetData = New System.Windows.Forms.Button()
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.cmdSearchStoreLoc = New System.Windows.Forms.Button()
        Me.CmdPopFromFile = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.Frasupp = New System.Windows.Forms.GroupBox()
        Me.txtDIDate = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtSearchItem = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtStoreLoc = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtShipCustomer = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.chkShipTo = New System.Windows.Forms.CheckBox()
        Me.TxtShipTo = New System.Windows.Forms.TextBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.TxtGRNo = New System.Windows.Forms.TextBox()
        Me.TxtGRDate = New System.Windows.Forms.TextBox()
        Me.txtPrepared = New System.Windows.Forms.TextBox()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.txtCustPODate = New System.Windows.Forms.TextBox()
        Me.txtCustPoNo = New System.Windows.Forms.TextBox()
        Me.txtSODate = New System.Windows.Forms.TextBox()
        Me.txtSONo = New System.Windows.Forms.TextBox()
        Me.txtVehicleNo = New System.Windows.Forms.TextBox()
        Me.txtLoadingTime = New System.Windows.Forms.TextBox()
        Me.cboRefType = New System.Windows.Forms.ComboBox()
        Me.TxtTransporter = New System.Windows.Forms.TextBox()
        Me.TxtCustomerName = New System.Windows.Forms.TextBox()
        Me.txtDNNo = New System.Windows.Forms.TextBox()
        Me.txtDNDate = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblDespType = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LblMkey = New System.Windows.Forms.Label()
        Me.Frasprd = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.FraFront.SuspendLayout()
        Me.Frasupp.SuspendLayout()
        Me.Frasprd.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchSo
        '
        Me.cmdSearchSo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSo.Image = CType(resources.GetObject("cmdSearchSo.Image"), System.Drawing.Image)
        Me.cmdSearchSo.Location = New System.Drawing.Point(188, 86)
        Me.cmdSearchSo.Name = "cmdSearchSo"
        Me.cmdSearchSo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSo.Size = New System.Drawing.Size(23, 23)
        Me.cmdSearchSo.TabIndex = 11
        Me.cmdSearchSo.TabStop = False
        Me.cmdSearchSo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSo, "Search")
        Me.cmdSearchSo.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(454, 63)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(23, 22)
        Me.cmdsearch.TabIndex = 8
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(550, 138)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(46, 13)
        Me.Label19.TabIndex = 61
        Me.Label19.Text = "GR No :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.ToolTip1.SetToolTip(Me.Label19, "AWB/RRP No.")
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(705, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 8
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
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(639, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 7
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(573, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 6
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(506, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 5
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(440, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 4
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdDelete.Location = New System.Drawing.Point(372, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 3
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
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSave.Location = New System.Drawing.Point(305, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 2
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
        Me.cmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdModify.Location = New System.Drawing.Point(238, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 1
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
        Me.cmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdAdd.Location = New System.Drawing.Point(171, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 0
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdGetData
        '
        Me.cmdGetData.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdGetData.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGetData.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGetData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGetData.Location = New System.Drawing.Point(770, 239)
        Me.cmdGetData.Name = "cmdGetData"
        Me.cmdGetData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGetData.Size = New System.Drawing.Size(97, 24)
        Me.cmdGetData.TabIndex = 24
        Me.cmdGetData.TabStop = False
        Me.cmdGetData.Text = "Get Data"
        Me.cmdGetData.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdGetData, "Search")
        Me.cmdGetData.UseVisualStyleBackColor = False
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(455, 242)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchItem.TabIndex = 123
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'cmdSearchStoreLoc
        '
        Me.cmdSearchStoreLoc.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchStoreLoc.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchStoreLoc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchStoreLoc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchStoreLoc.Image = CType(resources.GetObject("cmdSearchStoreLoc.Image"), System.Drawing.Image)
        Me.cmdSearchStoreLoc.Location = New System.Drawing.Point(710, 184)
        Me.cmdSearchStoreLoc.Name = "cmdSearchStoreLoc"
        Me.cmdSearchStoreLoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchStoreLoc.Size = New System.Drawing.Size(23, 22)
        Me.cmdSearchStoreLoc.TabIndex = 127
        Me.cmdSearchStoreLoc.TabStop = False
        Me.cmdSearchStoreLoc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchStoreLoc, "Search")
        Me.cmdSearchStoreLoc.UseVisualStyleBackColor = False
        '
        'CmdPopFromFile
        '
        Me.CmdPopFromFile.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdPopFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopFromFile.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopFromFile.Location = New System.Drawing.Point(873, 237)
        Me.CmdPopFromFile.Name = "CmdPopFromFile"
        Me.CmdPopFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopFromFile.Size = New System.Drawing.Size(150, 26)
        Me.CmdPopFromFile.TabIndex = 245
        Me.CmdPopFromFile.TabStop = False
        Me.CmdPopFromFile.Text = "DI Qty Update From Excel"
        Me.ToolTip1.SetToolTip(Me.CmdPopFromFile, "Search")
        Me.CmdPopFromFile.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.Frasupp)
        Me.FraFront.Controls.Add(Me.Frasprd)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(-2, 0)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(1108, 574)
        Me.FraFront.TabIndex = 40
        Me.FraFront.TabStop = False
        '
        'Frasupp
        '
        Me.Frasupp.BackColor = System.Drawing.SystemColors.Control
        Me.Frasupp.Controls.Add(Me.CmdPopFromFile)
        Me.Frasupp.Controls.Add(Me.cmdSearchStoreLoc)
        Me.Frasupp.Controls.Add(Me.txtDIDate)
        Me.Frasupp.Controls.Add(Me.Label7)
        Me.Frasupp.Controls.Add(Me.cmdSearchItem)
        Me.Frasupp.Controls.Add(Me.txtSearchItem)
        Me.Frasupp.Controls.Add(Me.Label26)
        Me.Frasupp.Controls.Add(Me.cmdGetData)
        Me.Frasupp.Controls.Add(Me.txtStoreLoc)
        Me.Frasupp.Controls.Add(Me.Label25)
        Me.Frasupp.Controls.Add(Me.txtShipCustomer)
        Me.Frasupp.Controls.Add(Me.Label24)
        Me.Frasupp.Controls.Add(Me.txtBillTo)
        Me.Frasupp.Controls.Add(Me.Label23)
        Me.Frasupp.Controls.Add(Me.chkShipTo)
        Me.Frasupp.Controls.Add(Me.TxtShipTo)
        Me.Frasupp.Controls.Add(Me.cboDivision)
        Me.Frasupp.Controls.Add(Me.txtAmendNo)
        Me.Frasupp.Controls.Add(Me.txtAddress)
        Me.Frasupp.Controls.Add(Me.TxtGRNo)
        Me.Frasupp.Controls.Add(Me.TxtGRDate)
        Me.Frasupp.Controls.Add(Me.txtPrepared)
        Me.Frasupp.Controls.Add(Me.txtCustomerCode)
        Me.Frasupp.Controls.Add(Me.txtCustPODate)
        Me.Frasupp.Controls.Add(Me.txtCustPoNo)
        Me.Frasupp.Controls.Add(Me.txtSODate)
        Me.Frasupp.Controls.Add(Me.txtSONo)
        Me.Frasupp.Controls.Add(Me.cmdSearchSo)
        Me.Frasupp.Controls.Add(Me.txtVehicleNo)
        Me.Frasupp.Controls.Add(Me.txtLoadingTime)
        Me.Frasupp.Controls.Add(Me.cboRefType)
        Me.Frasupp.Controls.Add(Me.cmdsearch)
        Me.Frasupp.Controls.Add(Me.TxtTransporter)
        Me.Frasupp.Controls.Add(Me.TxtCustomerName)
        Me.Frasupp.Controls.Add(Me.txtDNNo)
        Me.Frasupp.Controls.Add(Me.txtDNDate)
        Me.Frasupp.Controls.Add(Me.Label22)
        Me.Frasupp.Controls.Add(Me.lblDespType)
        Me.Frasupp.Controls.Add(Me.Label21)
        Me.Frasupp.Controls.Add(Me.Label17)
        Me.Frasupp.Controls.Add(Me.Label19)
        Me.Frasupp.Controls.Add(Me.Label20)
        Me.Frasupp.Controls.Add(Me.Label12)
        Me.Frasupp.Controls.Add(Me.Label8)
        Me.Frasupp.Controls.Add(Me.Label6)
        Me.Frasupp.Controls.Add(Me.Label5)
        Me.Frasupp.Controls.Add(Me.Label1)
        Me.Frasupp.Controls.Add(Me.Label11)
        Me.Frasupp.Controls.Add(Me.Label10)
        Me.Frasupp.Controls.Add(Me.Label9)
        Me.Frasupp.Controls.Add(Me.Label2)
        Me.Frasupp.Controls.Add(Me.Label3)
        Me.Frasupp.Controls.Add(Me.Label14)
        Me.Frasupp.Controls.Add(Me.Label15)
        Me.Frasupp.Controls.Add(Me.Label4)
        Me.Frasupp.Controls.Add(Me.LblMkey)
        Me.Frasupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasupp.Location = New System.Drawing.Point(5, 2)
        Me.Frasupp.Name = "Frasupp"
        Me.Frasupp.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasupp.Size = New System.Drawing.Size(1103, 272)
        Me.Frasupp.TabIndex = 0
        Me.Frasupp.TabStop = False
        '
        'txtDIDate
        '
        Me.txtDIDate.AcceptsReturn = True
        Me.txtDIDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDIDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDIDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDIDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDIDate.ForeColor = System.Drawing.Color.Blue
        Me.txtDIDate.Location = New System.Drawing.Point(600, 246)
        Me.txtDIDate.MaxLength = 0
        Me.txtDIDate.Name = "txtDIDate"
        Me.txtDIDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDIDate.Size = New System.Drawing.Size(92, 22)
        Me.txtDIDate.TabIndex = 23
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(545, 248)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(51, 13)
        Me.Label7.TabIndex = 126
        Me.Label7.Text = "DI Date :"
        '
        'txtSearchItem
        '
        Me.txtSearchItem.AcceptsReturn = True
        Me.txtSearchItem.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearchItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearchItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSearchItem.Location = New System.Drawing.Point(102, 243)
        Me.txtSearchItem.MaxLength = 0
        Me.txtSearchItem.Name = "txtSearchItem"
        Me.txtSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearchItem.Size = New System.Drawing.Size(352, 22)
        Me.txtSearchItem.TabIndex = 22
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(23, 247)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(72, 13)
        Me.Label26.TabIndex = 124
        Me.Label26.Text = "Search Item :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtStoreLoc
        '
        Me.txtStoreLoc.AcceptsReturn = True
        Me.txtStoreLoc.BackColor = System.Drawing.SystemColors.Window
        Me.txtStoreLoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStoreLoc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStoreLoc.Enabled = False
        Me.txtStoreLoc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStoreLoc.ForeColor = System.Drawing.Color.Blue
        Me.txtStoreLoc.Location = New System.Drawing.Point(600, 185)
        Me.txtStoreLoc.MaxLength = 0
        Me.txtStoreLoc.Name = "txtStoreLoc"
        Me.txtStoreLoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStoreLoc.Size = New System.Drawing.Size(110, 22)
        Me.txtStoreLoc.TabIndex = 19
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Enabled = False
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(510, 189)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(86, 13)
        Me.Label25.TabIndex = 77
        Me.Label25.Text = "Store Location :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtShipCustomer
        '
        Me.txtShipCustomer.AcceptsReturn = True
        Me.txtShipCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtShipCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShipCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShipCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShipCustomer.ForeColor = System.Drawing.Color.Blue
        Me.txtShipCustomer.Location = New System.Drawing.Point(102, 159)
        Me.txtShipCustomer.MaxLength = 0
        Me.txtShipCustomer.Name = "txtShipCustomer"
        Me.txtShipCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShipCustomer.Size = New System.Drawing.Size(378, 22)
        Me.txtShipCustomer.TabIndex = 14
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(47, 165)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(50, 13)
        Me.Label24.TabIndex = 75
        Me.Label24.Text = "Ship To :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.txtBillTo.Location = New System.Drawing.Point(600, 61)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(110, 22)
        Me.txtBillTo.TabIndex = 6
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(506, 66)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(90, 13)
        Me.Label23.TabIndex = 73
        Me.Label23.Text = "Bill To Location :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkShipTo
        '
        Me.chkShipTo.AutoSize = True
        Me.chkShipTo.BackColor = System.Drawing.SystemColors.Control
        Me.chkShipTo.Checked = True
        Me.chkShipTo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShipTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShipTo.Enabled = False
        Me.chkShipTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShipTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShipTo.Location = New System.Drawing.Point(102, 139)
        Me.chkShipTo.Name = "chkShipTo"
        Me.chkShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShipTo.Size = New System.Drawing.Size(215, 17)
        Me.chkShipTo.TabIndex = 21
        Me.chkShipTo.Text = "'Shipped To' Same as 'Billed To' (Y/N)"
        Me.chkShipTo.UseVisualStyleBackColor = False
        '
        'TxtShipTo
        '
        Me.TxtShipTo.AcceptsReturn = True
        Me.TxtShipTo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtShipTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtShipTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtShipTo.Enabled = False
        Me.TxtShipTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipTo.ForeColor = System.Drawing.Color.Blue
        Me.TxtShipTo.Location = New System.Drawing.Point(600, 159)
        Me.TxtShipTo.MaxLength = 0
        Me.TxtShipTo.Name = "TxtShipTo"
        Me.TxtShipTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtShipTo.Size = New System.Drawing.Size(110, 22)
        Me.TxtShipTo.TabIndex = 17
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(102, 38)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(231, 21)
        Me.cboDivision.TabIndex = 4
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.Color.Blue
        Me.txtAmendNo.Location = New System.Drawing.Point(434, 86)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(47, 22)
        Me.txtAmendNo.TabIndex = 11
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
        Me.txtAddress.Location = New System.Drawing.Point(102, 184)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress.Size = New System.Drawing.Size(379, 33)
        Me.txtAddress.TabIndex = 18
        '
        'TxtGRNo
        '
        Me.TxtGRNo.AcceptsReturn = True
        Me.TxtGRNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGRNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRNo.ForeColor = System.Drawing.Color.Blue
        Me.TxtGRNo.Location = New System.Drawing.Point(600, 134)
        Me.TxtGRNo.MaxLength = 0
        Me.TxtGRNo.Name = "TxtGRNo"
        Me.TxtGRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGRNo.Size = New System.Drawing.Size(110, 22)
        Me.TxtGRNo.TabIndex = 15
        '
        'TxtGRDate
        '
        Me.TxtGRDate.AcceptsReturn = True
        Me.TxtGRDate.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGRDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGRDate.ForeColor = System.Drawing.Color.Blue
        Me.TxtGRDate.Location = New System.Drawing.Point(771, 134)
        Me.TxtGRDate.MaxLength = 0
        Me.TxtGRDate.Name = "TxtGRDate"
        Me.TxtGRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGRDate.Size = New System.Drawing.Size(129, 22)
        Me.TxtGRDate.TabIndex = 16
        '
        'txtPrepared
        '
        Me.txtPrepared.AcceptsReturn = True
        Me.txtPrepared.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrepared.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrepared.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrepared.Enabled = False
        Me.txtPrepared.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrepared.ForeColor = System.Drawing.Color.Blue
        Me.txtPrepared.Location = New System.Drawing.Point(600, 219)
        Me.txtPrepared.MaxLength = 0
        Me.txtPrepared.Name = "txtPrepared"
        Me.txtPrepared.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrepared.Size = New System.Drawing.Size(91, 22)
        Me.txtPrepared.TabIndex = 21
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.AcceptsReturn = True
        Me.txtCustomerCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerCode.Enabled = False
        Me.txtCustomerCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.Color.Blue
        Me.txtCustomerCode.Location = New System.Drawing.Point(771, 63)
        Me.txtCustomerCode.MaxLength = 0
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerCode.Size = New System.Drawing.Size(129, 22)
        Me.txtCustomerCode.TabIndex = 8
        '
        'txtCustPODate
        '
        Me.txtCustPODate.AcceptsReturn = True
        Me.txtCustPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustPODate.Enabled = False
        Me.txtCustPODate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPODate.ForeColor = System.Drawing.Color.Blue
        Me.txtCustPODate.Location = New System.Drawing.Point(258, 110)
        Me.txtCustPODate.MaxLength = 0
        Me.txtCustPODate.Name = "txtCustPODate"
        Me.txtCustPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPODate.Size = New System.Drawing.Size(81, 22)
        Me.txtCustPODate.TabIndex = 13
        '
        'txtCustPoNo
        '
        Me.txtCustPoNo.AcceptsReturn = True
        Me.txtCustPoNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustPoNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustPoNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustPoNo.Enabled = False
        Me.txtCustPoNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPoNo.ForeColor = System.Drawing.Color.Blue
        Me.txtCustPoNo.Location = New System.Drawing.Point(102, 110)
        Me.txtCustPoNo.MaxLength = 0
        Me.txtCustPoNo.Name = "txtCustPoNo"
        Me.txtCustPoNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPoNo.Size = New System.Drawing.Size(109, 22)
        Me.txtCustPoNo.TabIndex = 12
        '
        'txtSODate
        '
        Me.txtSODate.AcceptsReturn = True
        Me.txtSODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSODate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSODate.ForeColor = System.Drawing.Color.Blue
        Me.txtSODate.Location = New System.Drawing.Point(258, 86)
        Me.txtSODate.MaxLength = 0
        Me.txtSODate.Name = "txtSODate"
        Me.txtSODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSODate.Size = New System.Drawing.Size(81, 22)
        Me.txtSODate.TabIndex = 10
        '
        'txtSONo
        '
        Me.txtSONo.AcceptsReturn = True
        Me.txtSONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSONo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSONo.ForeColor = System.Drawing.Color.Blue
        Me.txtSONo.Location = New System.Drawing.Point(102, 86)
        Me.txtSONo.MaxLength = 0
        Me.txtSONo.Name = "txtSONo"
        Me.txtSONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSONo.Size = New System.Drawing.Size(85, 22)
        Me.txtSONo.TabIndex = 9
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.AcceptsReturn = True
        Me.txtVehicleNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicleNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicleNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.ForeColor = System.Drawing.Color.Blue
        Me.txtVehicleNo.Location = New System.Drawing.Point(771, 38)
        Me.txtVehicleNo.MaxLength = 0
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicleNo.Size = New System.Drawing.Size(129, 22)
        Me.txtVehicleNo.TabIndex = 7
        '
        'txtLoadingTime
        '
        Me.txtLoadingTime.AcceptsReturn = True
        Me.txtLoadingTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtLoadingTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLoadingTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLoadingTime.Enabled = False
        Me.txtLoadingTime.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoadingTime.ForeColor = System.Drawing.Color.Blue
        Me.txtLoadingTime.Location = New System.Drawing.Point(436, 13)
        Me.txtLoadingTime.MaxLength = 0
        Me.txtLoadingTime.Name = "txtLoadingTime"
        Me.txtLoadingTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLoadingTime.Size = New System.Drawing.Size(85, 22)
        Me.txtLoadingTime.TabIndex = 2
        '
        'cboRefType
        '
        Me.cboRefType.BackColor = System.Drawing.SystemColors.Window
        Me.cboRefType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRefType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRefType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRefType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRefType.Location = New System.Drawing.Point(600, 13)
        Me.cboRefType.Name = "cboRefType"
        Me.cboRefType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRefType.Size = New System.Drawing.Size(300, 21)
        Me.cboRefType.TabIndex = 3
        '
        'TxtTransporter
        '
        Me.TxtTransporter.AcceptsReturn = True
        Me.TxtTransporter.BackColor = System.Drawing.SystemColors.Window
        Me.TxtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTransporter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransporter.ForeColor = System.Drawing.Color.Blue
        Me.TxtTransporter.Location = New System.Drawing.Point(102, 219)
        Me.TxtTransporter.MaxLength = 0
        Me.TxtTransporter.Name = "TxtTransporter"
        Me.TxtTransporter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtTransporter.Size = New System.Drawing.Size(297, 22)
        Me.TxtTransporter.TabIndex = 20
        '
        'TxtCustomerName
        '
        Me.TxtCustomerName.AcceptsReturn = True
        Me.TxtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCustomerName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustomerName.ForeColor = System.Drawing.Color.Blue
        Me.TxtCustomerName.Location = New System.Drawing.Point(102, 63)
        Me.TxtCustomerName.MaxLength = 0
        Me.TxtCustomerName.Name = "TxtCustomerName"
        Me.TxtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCustomerName.Size = New System.Drawing.Size(350, 22)
        Me.TxtCustomerName.TabIndex = 5
        '
        'txtDNNo
        '
        Me.txtDNNo.AcceptsReturn = True
        Me.txtDNNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDNNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDNNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDNNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDNNo.ForeColor = System.Drawing.Color.Blue
        Me.txtDNNo.Location = New System.Drawing.Point(102, 13)
        Me.txtDNNo.MaxLength = 0
        Me.txtDNNo.Name = "txtDNNo"
        Me.txtDNNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDNNo.Size = New System.Drawing.Size(85, 22)
        Me.txtDNNo.TabIndex = 0
        '
        'txtDNDate
        '
        Me.txtDNDate.AcceptsReturn = True
        Me.txtDNDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDNDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDNDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDNDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDNDate.ForeColor = System.Drawing.Color.Blue
        Me.txtDNDate.Location = New System.Drawing.Point(264, 13)
        Me.txtDNDate.MaxLength = 0
        Me.txtDNDate.Name = "txtDNDate"
        Me.txtDNDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDNDate.Size = New System.Drawing.Size(81, 22)
        Me.txtDNDate.TabIndex = 1
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(480, 163)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(116, 13)
        Me.Label22.TabIndex = 71
        Me.Label22.Text = "Shipped To Location :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDespType
        '
        Me.lblDespType.AutoSize = True
        Me.lblDespType.BackColor = System.Drawing.SystemColors.Control
        Me.lblDespType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDespType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDespType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDespType.Location = New System.Drawing.Point(826, 104)
        Me.lblDespType.Name = "lblDespType"
        Me.lblDespType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDespType.Size = New System.Drawing.Size(70, 13)
        Me.lblDespType.TabIndex = 70
        Me.lblDespType.Text = "lblDespType"
        Me.lblDespType.Visible = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(43, 42)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(54, 13)
        Me.Label21.TabIndex = 69
        Me.Label21.Text = "Division :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(342, 90)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(88, 13)
        Me.Label17.TabIndex = 64
        Me.Label17.Text = "Our Amend No :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(712, 138)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(55, 13)
        Me.Label20.TabIndex = 60
        Me.Label20.Text = "GR Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(521, 223)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(75, 13)
        Me.Label12.TabIndex = 59
        Me.Label12.Text = "Prepared By :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(727, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(40, 13)
        Me.Label8.TabIndex = 58
        Me.Label8.Text = "Code :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(559, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Type :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(220, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Date :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(19, 115)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "Customer Po :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(217, 89)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(37, 13)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Date :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(8, 90)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(89, 13)
        Me.Label10.TabIndex = 52
        Me.Label10.Text = "Sales Order No :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(701, 44)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Vehicle No :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(354, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "Loading Time :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(26, 221)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Transporter :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(50, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(47, 13)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "DN No :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(223, 15)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(37, 13)
        Me.Label15.TabIndex = 45
        Me.Label15.Text = "Date :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(2, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(95, 13)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Customer Name :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblMkey
        '
        Me.LblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMkey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMkey.Location = New System.Drawing.Point(850, 48)
        Me.LblMkey.Name = "LblMkey"
        Me.LblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMkey.Size = New System.Drawing.Size(31, 10)
        Me.LblMkey.TabIndex = 43
        Me.LblMkey.Text = "MKEY"
        '
        'Frasprd
        '
        Me.Frasprd.BackColor = System.Drawing.SystemColors.Control
        Me.Frasprd.Controls.Add(Me.SprdMain)
        Me.Frasprd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frasprd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frasprd.Location = New System.Drawing.Point(2, 268)
        Me.Frasprd.Name = "Frasprd"
        Me.Frasprd.Padding = New System.Windows.Forms.Padding(0)
        Me.Frasprd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frasprd.Size = New System.Drawing.Size(1104, 306)
        Me.Frasprd.TabIndex = 49
        Me.Frasprd.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1098, 292)
        Me.SprdMain.TabIndex = 0
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 41
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
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 570)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(1106, 51)
        Me.Frame3.TabIndex = 38
        Me.Frame3.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(4, 18)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(33, 17)
        Me.lblBookType.TabIndex = 48
        Me.lblBookType.Text = "lblBookType"
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
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 2)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(1104, 566)
        Me.UltraGrid1.TabIndex = 81
        '
        'FrmDeliveryInstruction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Report1)
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
        Me.Name = "FrmDeliveryInstruction"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Delivery Instruction"
        Me.FraFront.ResumeLayout(False)
        Me.Frasupp.ResumeLayout(False)
        Me.Frasupp.PerformLayout()
        Me.Frasprd.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(AdataItem, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdView.DataSource = Nothing
    End Sub

    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label23 As Label
    Public WithEvents txtShipCustomer As TextBox
    Public WithEvents Label24 As Label
    Public WithEvents txtStoreLoc As TextBox
    Public WithEvents Label25 As Label
    Public WithEvents cmdGetData As Button
    Public WithEvents cmdSearchItem As Button
    Public WithEvents txtSearchItem As TextBox
    Public WithEvents Label26 As Label
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Public WithEvents txtDIDate As TextBox
    Public WithEvents Label7 As Label
    Public WithEvents cmdSearchStoreLoc As Button
    Public WithEvents CmdPopFromFile As Button
    Public WithEvents CommonDialogOpen As OpenFileDialog
#End Region
End Class