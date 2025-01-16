Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLoadingTripSlip
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
    Public WithEvents txtAmount As System.Windows.Forms.TextBox
    Public WithEvents cmdLoaction As System.Windows.Forms.Button
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents chkStatus As System.Windows.Forms.CheckBox
    Public WithEvents txtTripNo As System.Windows.Forms.TextBox
    Public WithEvents txtTripDate As System.Windows.Forms.TextBox
    Public WithEvents txtSlipDate As System.Windows.Forms.TextBox
    Public WithEvents txtTransporterName As System.Windows.Forms.TextBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents txtVehicleNo As System.Windows.Forms.TextBox
    Public WithEvents txtVehicleType As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents _optFreightType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optFreightType_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents FraTop As System.Windows.Forms.GroupBox
    Public WithEvents Frabot As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraCmd As System.Windows.Forms.GroupBox
    Public WithEvents optFreightType As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoadingTripSlip))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdLoaction = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.Frabot = New System.Windows.Forms.GroupBox()
        Me.FraTop = New System.Windows.Forms.GroupBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.txtTripNo = New System.Windows.Forms.TextBox()
        Me.txtTripDate = New System.Windows.Forms.TextBox()
        Me.txtSlipDate = New System.Windows.Forms.TextBox()
        Me.txtTransporterName = New System.Windows.Forms.TextBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.txtVehicleNo = New System.Windows.Forms.TextBox()
        Me.txtVehicleType = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optFreightType_1 = New System.Windows.Forms.RadioButton()
        Me._optFreightType_0 = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraCmd = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.optFreightType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frabot.SuspendLayout()
        Me.FraTop.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraCmd.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optFreightType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdLoaction
        '
        Me.cmdLoaction.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdLoaction.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLoaction.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoaction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLoaction.Image = CType(resources.GetObject("cmdLoaction.Image"), System.Drawing.Image)
        Me.cmdLoaction.Location = New System.Drawing.Point(544, 104)
        Me.cmdLoaction.Name = "cmdLoaction"
        Me.cmdLoaction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLoaction.Size = New System.Drawing.Size(28, 23)
        Me.cmdLoaction.TabIndex = 13
        Me.cmdLoaction.TabStop = False
        Me.cmdLoaction.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdLoaction, "Search")
        Me.cmdLoaction.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(300, 72)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearch.TabIndex = 6
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(666, 14)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 23
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(600, 14)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 22
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(534, 14)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 21
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(468, 14)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 20
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(402, 14)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 19
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(336, 14)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 18
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(270, 14)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 17
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(204, 14)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 16
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(138, 14)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'Frabot
        '
        Me.Frabot.BackColor = System.Drawing.SystemColors.Control
        Me.Frabot.Controls.Add(Me.FraTop)
        Me.Frabot.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frabot.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frabot.Location = New System.Drawing.Point(0, -4)
        Me.Frabot.Name = "Frabot"
        Me.Frabot.Padding = New System.Windows.Forms.Padding(0)
        Me.Frabot.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frabot.Size = New System.Drawing.Size(910, 566)
        Me.Frabot.TabIndex = 26
        Me.Frabot.TabStop = False
        '
        'FraTop
        '
        Me.FraTop.BackColor = System.Drawing.SystemColors.Control
        Me.FraTop.Controls.Add(Me.txtAmount)
        Me.FraTop.Controls.Add(Me.cmdLoaction)
        Me.FraTop.Controls.Add(Me.txtLocation)
        Me.FraTop.Controls.Add(Me.chkStatus)
        Me.FraTop.Controls.Add(Me.txtTripNo)
        Me.FraTop.Controls.Add(Me.txtTripDate)
        Me.FraTop.Controls.Add(Me.txtSlipDate)
        Me.FraTop.Controls.Add(Me.txtTransporterName)
        Me.FraTop.Controls.Add(Me.txtSlipNo)
        Me.FraTop.Controls.Add(Me.txtVehicleNo)
        Me.FraTop.Controls.Add(Me.txtVehicleType)
        Me.FraTop.Controls.Add(Me.txtRemarks)
        Me.FraTop.Controls.Add(Me.cmdsearch)
        Me.FraTop.Controls.Add(Me.Frame1)
        Me.FraTop.Controls.Add(Me.Label10)
        Me.FraTop.Controls.Add(Me.Label7)
        Me.FraTop.Controls.Add(Me.Label23)
        Me.FraTop.Controls.Add(Me.lblModDate)
        Me.FraTop.Controls.Add(Me.Label48)
        Me.FraTop.Controls.Add(Me.lblAddDate)
        Me.FraTop.Controls.Add(Me.Label45)
        Me.FraTop.Controls.Add(Me.lblModUser)
        Me.FraTop.Controls.Add(Me.Label46)
        Me.FraTop.Controls.Add(Me.lblAddUser)
        Me.FraTop.Controls.Add(Me.Label44)
        Me.FraTop.Controls.Add(Me.Label5)
        Me.FraTop.Controls.Add(Me.Label4)
        Me.FraTop.Controls.Add(Me.Label2)
        Me.FraTop.Controls.Add(Me.Label1)
        Me.FraTop.Controls.Add(Me.Label3)
        Me.FraTop.Controls.Add(Me.lblMKey)
        Me.FraTop.Controls.Add(Me.Label6)
        Me.FraTop.Controls.Add(Me.Label8)
        Me.FraTop.Controls.Add(Me.Label9)
        Me.FraTop.Controls.Add(Me.lblBookType)
        Me.FraTop.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraTop.Location = New System.Drawing.Point(0, 1)
        Me.FraTop.Name = "FraTop"
        Me.FraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.FraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraTop.Size = New System.Drawing.Size(908, 565)
        Me.FraTop.TabIndex = 27
        Me.FraTop.TabStop = False
        '
        'txtAmount
        '
        Me.txtAmount.AcceptsReturn = True
        Me.txtAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmount.Location = New System.Drawing.Point(126, 233)
        Me.txtAmount.MaxLength = 0
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmount.Size = New System.Drawing.Size(173, 22)
        Me.txtAmount.TabIndex = 14
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLocation.Location = New System.Drawing.Point(126, 203)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(415, 22)
        Me.txtLocation.TabIndex = 12
        '
        'chkStatus
        '
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(710, 16)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(167, 20)
        Me.chkStatus.TabIndex = 7
        Me.chkStatus.Text = "Status (Open / Closed)"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'txtTripNo
        '
        Me.txtTripNo.AcceptsReturn = True
        Me.txtTripNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTripNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTripNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTripNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTripNo.Location = New System.Drawing.Point(126, 43)
        Me.txtTripNo.MaxLength = 0
        Me.txtTripNo.Name = "txtTripNo"
        Me.txtTripNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTripNo.Size = New System.Drawing.Size(101, 22)
        Me.txtTripNo.TabIndex = 3
        '
        'txtTripDate
        '
        Me.txtTripDate.AcceptsReturn = True
        Me.txtTripDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtTripDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTripDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTripDate.Enabled = False
        Me.txtTripDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTripDate.Location = New System.Drawing.Point(406, 43)
        Me.txtTripDate.MaxLength = 0
        Me.txtTripDate.Name = "txtTripDate"
        Me.txtTripDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTripDate.Size = New System.Drawing.Size(188, 22)
        Me.txtTripDate.TabIndex = 4
        '
        'txtSlipDate
        '
        Me.txtSlipDate.AcceptsReturn = True
        Me.txtSlipDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipDate.Enabled = False
        Me.txtSlipDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipDate.Location = New System.Drawing.Point(406, 13)
        Me.txtSlipDate.MaxLength = 0
        Me.txtSlipDate.Name = "txtSlipDate"
        Me.txtSlipDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipDate.Size = New System.Drawing.Size(188, 22)
        Me.txtSlipDate.TabIndex = 2
        '
        'txtTransporterName
        '
        Me.txtTransporterName.AcceptsReturn = True
        Me.txtTransporterName.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransporterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransporterName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransporterName.Enabled = False
        Me.txtTransporterName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporterName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTransporterName.Location = New System.Drawing.Point(126, 103)
        Me.txtTransporterName.MaxLength = 0
        Me.txtTransporterName.Name = "txtTransporterName"
        Me.txtTransporterName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransporterName.Size = New System.Drawing.Size(417, 22)
        Me.txtTransporterName.TabIndex = 8
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(126, 13)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(101, 22)
        Me.txtSlipNo.TabIndex = 1
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.AcceptsReturn = True
        Me.txtVehicleNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicleNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicleNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVehicleNo.Location = New System.Drawing.Point(126, 73)
        Me.txtVehicleNo.MaxLength = 0
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicleNo.Size = New System.Drawing.Size(173, 22)
        Me.txtVehicleNo.TabIndex = 5
        '
        'txtVehicleType
        '
        Me.txtVehicleType.AcceptsReturn = True
        Me.txtVehicleType.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicleType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicleType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicleType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVehicleType.Location = New System.Drawing.Point(126, 133)
        Me.txtVehicleType.MaxLength = 0
        Me.txtVehicleType.Name = "txtVehicleType"
        Me.txtVehicleType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicleType.Size = New System.Drawing.Size(191, 22)
        Me.txtVehicleType.TabIndex = 9
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(126, 263)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(415, 37)
        Me.txtRemarks.TabIndex = 15
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optFreightType_1)
        Me.Frame1.Controls.Add(Me._optFreightType_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(126, 161)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(343, 33)
        Me.Frame1.TabIndex = 46
        Me.Frame1.TabStop = False
        '
        '_optFreightType_1
        '
        Me._optFreightType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optFreightType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFreightType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFreightType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFreightType.SetIndex(Me._optFreightType_1, CType(1, Short))
        Me._optFreightType_1.Location = New System.Drawing.Point(170, 11)
        Me._optFreightType_1.Name = "_optFreightType_1"
        Me._optFreightType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFreightType_1.Size = New System.Drawing.Size(88, 17)
        Me._optFreightType_1.TabIndex = 11
        Me._optFreightType_1.TabStop = True
        Me._optFreightType_1.Text = "Premium"
        Me._optFreightType_1.UseVisualStyleBackColor = False
        '
        '_optFreightType_0
        '
        Me._optFreightType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optFreightType_0.Checked = True
        Me._optFreightType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFreightType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFreightType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFreightType.SetIndex(Me._optFreightType_0, CType(0, Short))
        Me._optFreightType_0.Location = New System.Drawing.Point(54, 11)
        Me._optFreightType_0.Name = "_optFreightType_0"
        Me._optFreightType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFreightType_0.Size = New System.Drawing.Size(70, 17)
        Me._optFreightType_0.TabIndex = 10
        Me._optFreightType_0.TabStop = True
        Me._optFreightType_0.Text = "Regular"
        Me._optFreightType_0.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(70, 237)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(53, 13)
        Me.Label10.TabIndex = 49
        Me.Label10.Text = "Amount :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(61, 207)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "Customer :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(47, 172)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(76, 13)
        Me.Label23.TabIndex = 47
        Me.Label23.Text = "Freight Type :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(686, 511)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 45
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(627, 511)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 44
        Me.Label48.Text = "Mod Date:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(319, 511)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(85, 19)
        Me.lblAddDate.TabIndex = 43
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(257, 513)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 42
        Me.Label45.Text = "Add Date :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(529, 511)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 41
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(469, 511)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 40
        Me.Label46.Text = "Mod User:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(69, 511)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(85, 19)
        Me.lblAddUser.TabIndex = 39
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(10, 511)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 38
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(42, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "Trip Sheet No :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(365, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Date :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(326, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Date && Time :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(19, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(104, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Collection Slip No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(19, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(104, 13)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Transporter Name :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(760, 62)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(49, 13)
        Me.lblMKey.TabIndex = 32
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(57, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Vehicle No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(48, 136)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Vehicle Type :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(66, 272)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Remarks :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(750, 104)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(103, 15)
        Me.lblBookType.TabIndex = 28
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(910, 560)
        Me.SprdView.TabIndex = 25
        '
        'FraCmd
        '
        Me.FraCmd.BackColor = System.Drawing.SystemColors.Control
        Me.FraCmd.Controls.Add(Me.CmdClose)
        Me.FraCmd.Controls.Add(Me.CmdView)
        Me.FraCmd.Controls.Add(Me.CmdPreview)
        Me.FraCmd.Controls.Add(Me.cmdPrint)
        Me.FraCmd.Controls.Add(Me.CmdDelete)
        Me.FraCmd.Controls.Add(Me.cmdSavePrint)
        Me.FraCmd.Controls.Add(Me.CmdSave)
        Me.FraCmd.Controls.Add(Me.CmdModify)
        Me.FraCmd.Controls.Add(Me.CmdAdd)
        Me.FraCmd.Controls.Add(Me.Report1)
        Me.FraCmd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCmd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraCmd.Location = New System.Drawing.Point(0, 559)
        Me.FraCmd.Name = "FraCmd"
        Me.FraCmd.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCmd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCmd.Size = New System.Drawing.Size(908, 60)
        Me.FraCmd.TabIndex = 24
        Me.FraCmd.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(22, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 24
        '
        'optFreightType
        '
        '
        'frmLoadingTripSlip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.Frabot)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraCmd)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoadingTripSlip"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Collection Vehicle Pass"
        Me.Frabot.ResumeLayout(False)
        Me.FraTop.ResumeLayout(False)
        Me.FraTop.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraCmd.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optFreightType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataPPOMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class