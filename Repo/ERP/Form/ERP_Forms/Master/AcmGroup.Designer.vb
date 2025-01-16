Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAcmGroup
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
        'Me.MDIParent = Admin.Master
        'Admin.Master.Show
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
    Public WithEvents txtScheduleNo As System.Windows.Forms.TextBox
    Public WithEvents txtSeqNo As System.Windows.Forms.TextBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents TxtParentName As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents CmdSearchGroup As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents TxtBSGroupCr As System.Windows.Forms.TextBox
    Public WithEvents TxtBSGroupDr As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchBSDr As System.Windows.Forms.Button
    Public WithEvents CmdSearchBSCr As System.Windows.Forms.Button
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblGroup As System.Windows.Forms.Label
    Public WithEvents fraBSGroup As System.Windows.Forms.GroupBox
    Public WithEvents _optType_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboGroupType As System.Windows.Forms.ComboBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _OptStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents fraMain As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents ADataMain As VB6.ADODC
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents OptStatus As VB6.RadioButtonArray
    Public WithEvents lblLabels As VB6.LabelArray
    Public WithEvents optType As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAcmGroup))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.CmdSearchGroup = New System.Windows.Forms.Button()
        Me.CmdSearchBSDr = New System.Windows.Forms.Button()
        Me.CmdSearchBSCr = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.fraMain = New System.Windows.Forms.GroupBox()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.txtScheduleNo = New System.Windows.Forms.TextBox()
        Me.txtSeqNo = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.TxtParentName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.fraBSGroup = New System.Windows.Forms.GroupBox()
        Me.TxtBSGroupCr = New System.Windows.Forms.TextBox()
        Me.TxtBSGroupDr = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblGroup = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optType_3 = New System.Windows.Forms.RadioButton()
        Me._optType_2 = New System.Windows.Forms.RadioButton()
        Me._optType_1 = New System.Windows.Forms.RadioButton()
        Me._optType_0 = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkStockGroup = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtMISSeqNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboGroupType = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._OptStatus_0 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_1 = New System.Windows.Forms.RadioButton()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.ADataMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.OptStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optOpeningBalHead = New System.Windows.Forms.RadioButton()
        Me.optClosingBalHead = New System.Windows.Forms.RadioButton()
        Me.fraMain.SuspendLayout()
        Me.FraView.SuspendLayout()
        Me.fraBSGroup.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(601, 13)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 22)
        Me.cmdsearch.TabIndex = 2
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search Group Name")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'CmdSearchGroup
        '
        Me.CmdSearchGroup.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchGroup.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchGroup.Image = CType(resources.GetObject("CmdSearchGroup.Image"), System.Drawing.Image)
        Me.CmdSearchGroup.Location = New System.Drawing.Point(601, 38)
        Me.CmdSearchGroup.Name = "CmdSearchGroup"
        Me.CmdSearchGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchGroup.Size = New System.Drawing.Size(29, 22)
        Me.CmdSearchGroup.TabIndex = 4
        Me.CmdSearchGroup.TabStop = False
        Me.CmdSearchGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchGroup, "Search Under Group Name")
        Me.CmdSearchGroup.UseVisualStyleBackColor = False
        '
        'CmdSearchBSDr
        '
        Me.CmdSearchBSDr.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchBSDr.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchBSDr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchBSDr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchBSDr.Image = CType(resources.GetObject("CmdSearchBSDr.Image"), System.Drawing.Image)
        Me.CmdSearchBSDr.Location = New System.Drawing.Point(601, 14)
        Me.CmdSearchBSDr.Name = "CmdSearchBSDr"
        Me.CmdSearchBSDr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchBSDr.Size = New System.Drawing.Size(29, 22)
        Me.CmdSearchBSDr.TabIndex = 8
        Me.CmdSearchBSDr.TabStop = False
        Me.CmdSearchBSDr.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchBSDr, "Search Under Group Name")
        Me.CmdSearchBSDr.UseVisualStyleBackColor = False
        '
        'CmdSearchBSCr
        '
        Me.CmdSearchBSCr.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchBSCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchBSCr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchBSCr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchBSCr.Image = CType(resources.GetObject("CmdSearchBSCr.Image"), System.Drawing.Image)
        Me.CmdSearchBSCr.Location = New System.Drawing.Point(601, 42)
        Me.CmdSearchBSCr.Name = "CmdSearchBSCr"
        Me.CmdSearchBSCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchBSCr.Size = New System.Drawing.Size(29, 22)
        Me.CmdSearchBSCr.TabIndex = 10
        Me.CmdSearchBSCr.TabStop = False
        Me.CmdSearchBSCr.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchBSCr, "Search Under Group Name")
        Me.CmdSearchBSCr.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSavePrint.Location = New System.Drawing.Point(275, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(75, 38)
        Me.cmdSavePrint.TabIndex = 18
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdPreview.Location = New System.Drawing.Point(500, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(75, 38)
        Me.CmdPreview.TabIndex = 21
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdPrint.Location = New System.Drawing.Point(425, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(75, 38)
        Me.cmdPrint.TabIndex = 20
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print ")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdAdd.Location = New System.Drawing.Point(52, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(75, 38)
        Me.CmdAdd.TabIndex = 15
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdModify.Location = New System.Drawing.Point(125, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(75, 38)
        Me.CmdModify.TabIndex = 16
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdSave.Location = New System.Drawing.Point(200, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(75, 38)
        Me.CmdSave.TabIndex = 17
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdDelete.Location = New System.Drawing.Point(350, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(75, 38)
        Me.CmdDelete.TabIndex = 19
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdView.Location = New System.Drawing.Point(575, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(75, 38)
        Me.CmdView.TabIndex = 22
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdClose.Location = New System.Drawing.Point(650, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(75, 38)
        Me.CmdClose.TabIndex = 23
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'fraMain
        '
        Me.fraMain.BackColor = System.Drawing.SystemColors.Control
        Me.fraMain.Controls.Add(Me.GroupBox1)
        Me.fraMain.Controls.Add(Me.FraView)
        Me.fraMain.Controls.Add(Me.fraBSGroup)
        Me.fraMain.Controls.Add(Me.Frame1)
        Me.fraMain.Controls.Add(Me.Frame2)
        Me.fraMain.Controls.Add(Me.Frame3)
        Me.fraMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMain.Location = New System.Drawing.Point(0, -4)
        Me.fraMain.Name = "fraMain"
        Me.fraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMain.Size = New System.Drawing.Size(773, 260)
        Me.fraMain.TabIndex = 26
        Me.fraMain.TabStop = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.txtScheduleNo)
        Me.FraView.Controls.Add(Me.txtSeqNo)
        Me.FraView.Controls.Add(Me.TxtName)
        Me.FraView.Controls.Add(Me.TxtParentName)
        Me.FraView.Controls.Add(Me.cmdsearch)
        Me.FraView.Controls.Add(Me.CmdSearchGroup)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Controls.Add(Me.Label2)
        Me.FraView.Controls.Add(Me._lblLabels_0)
        Me.FraView.Controls.Add(Me._lblLabels_1)
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, 0)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(639, 89)
        Me.FraView.TabIndex = 27
        Me.FraView.TabStop = False
        '
        'txtScheduleNo
        '
        Me.txtScheduleNo.AcceptsReturn = True
        Me.txtScheduleNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtScheduleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScheduleNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScheduleNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScheduleNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtScheduleNo.Location = New System.Drawing.Point(174, 62)
        Me.txtScheduleNo.MaxLength = 0
        Me.txtScheduleNo.Name = "txtScheduleNo"
        Me.txtScheduleNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScheduleNo.Size = New System.Drawing.Size(91, 22)
        Me.txtScheduleNo.TabIndex = 5
        '
        'txtSeqNo
        '
        Me.txtSeqNo.AcceptsReturn = True
        Me.txtSeqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeqNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeqNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSeqNo.Location = New System.Drawing.Point(378, 62)
        Me.txtSeqNo.MaxLength = 0
        Me.txtSeqNo.Name = "txtSeqNo"
        Me.txtSeqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeqNo.Size = New System.Drawing.Size(53, 22)
        Me.txtSeqNo.TabIndex = 6
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(174, 14)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(421, 22)
        Me.TxtName.TabIndex = 1
        '
        'TxtParentName
        '
        Me.TxtParentName.AcceptsReturn = True
        Me.TxtParentName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtParentName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtParentName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtParentName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParentName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtParentName.Location = New System.Drawing.Point(174, 38)
        Me.TxtParentName.MaxLength = 0
        Me.TxtParentName.Name = "TxtParentName"
        Me.TxtParentName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtParentName.Size = New System.Drawing.Size(421, 22)
        Me.TxtParentName.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(91, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Schedule No :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(318, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Seq No. :"
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(122, 18)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(45, 15)
        Me._lblLabels_0.TabIndex = 29
        Me._lblLabels_0.Text = "Name :"
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(54, 42)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(113, 13)
        Me._lblLabels_1.TabIndex = 28
        Me._lblLabels_1.Text = "Parent Group Name :"
        '
        'fraBSGroup
        '
        Me.fraBSGroup.BackColor = System.Drawing.SystemColors.Control
        Me.fraBSGroup.Controls.Add(Me.TxtBSGroupCr)
        Me.fraBSGroup.Controls.Add(Me.TxtBSGroupDr)
        Me.fraBSGroup.Controls.Add(Me.CmdSearchBSDr)
        Me.fraBSGroup.Controls.Add(Me.CmdSearchBSCr)
        Me.fraBSGroup.Controls.Add(Me.Label6)
        Me.fraBSGroup.Controls.Add(Me.lblGroup)
        Me.fraBSGroup.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBSGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBSGroup.Location = New System.Drawing.Point(0, 88)
        Me.fraBSGroup.Name = "fraBSGroup"
        Me.fraBSGroup.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBSGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBSGroup.Size = New System.Drawing.Size(639, 69)
        Me.fraBSGroup.TabIndex = 30
        Me.fraBSGroup.TabStop = False
        Me.fraBSGroup.Text = "Balance Sheet Group"
        '
        'TxtBSGroupCr
        '
        Me.TxtBSGroupCr.AcceptsReturn = True
        Me.TxtBSGroupCr.BackColor = System.Drawing.SystemColors.Window
        Me.TxtBSGroupCr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBSGroupCr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtBSGroupCr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBSGroupCr.ForeColor = System.Drawing.Color.Blue
        Me.TxtBSGroupCr.Location = New System.Drawing.Point(174, 42)
        Me.TxtBSGroupCr.MaxLength = 0
        Me.TxtBSGroupCr.Name = "TxtBSGroupCr"
        Me.TxtBSGroupCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBSGroupCr.Size = New System.Drawing.Size(421, 22)
        Me.TxtBSGroupCr.TabIndex = 9
        '
        'TxtBSGroupDr
        '
        Me.TxtBSGroupDr.AcceptsReturn = True
        Me.TxtBSGroupDr.BackColor = System.Drawing.SystemColors.Window
        Me.TxtBSGroupDr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtBSGroupDr.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtBSGroupDr.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBSGroupDr.ForeColor = System.Drawing.Color.Blue
        Me.TxtBSGroupDr.Location = New System.Drawing.Point(174, 14)
        Me.TxtBSGroupDr.MaxLength = 0
        Me.TxtBSGroupDr.Name = "TxtBSGroupDr"
        Me.TxtBSGroupDr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBSGroupDr.Size = New System.Drawing.Size(421, 22)
        Me.TxtBSGroupDr.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(103, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Credit A/c :"
        '
        'lblGroup
        '
        Me.lblGroup.AutoSize = True
        Me.lblGroup.BackColor = System.Drawing.Color.Transparent
        Me.lblGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGroup.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGroup.ForeColor = System.Drawing.Color.Black
        Me.lblGroup.Location = New System.Drawing.Point(106, 18)
        Me.lblGroup.Name = "lblGroup"
        Me.lblGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGroup.Size = New System.Drawing.Size(61, 13)
        Me.lblGroup.TabIndex = 31
        Me.lblGroup.Text = "Debit A/c :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optType_3)
        Me.Frame1.Controls.Add(Me._optType_2)
        Me.Frame1.Controls.Add(Me._optType_1)
        Me.Frame1.Controls.Add(Me._optType_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 159)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(639, 53)
        Me.Frame1.TabIndex = 33
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Type"
        '
        '_optType_3
        '
        Me._optType_3.BackColor = System.Drawing.SystemColors.Control
        Me._optType_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_3, CType(3, Short))
        Me._optType_3.Location = New System.Drawing.Point(456, 18)
        Me._optType_3.Name = "_optType_3"
        Me._optType_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_3.Size = New System.Drawing.Size(77, 17)
        Me._optType_3.TabIndex = 14
        Me._optType_3.TabStop = True
        Me._optType_3.Text = "Creditors"
        Me._optType_3.UseVisualStyleBackColor = False
        '
        '_optType_2
        '
        Me._optType_2.BackColor = System.Drawing.SystemColors.Control
        Me._optType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_2, CType(2, Short))
        Me._optType_2.Location = New System.Drawing.Point(358, 18)
        Me._optType_2.Name = "_optType_2"
        Me._optType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_2.Size = New System.Drawing.Size(88, 19)
        Me._optType_2.TabIndex = 13
        Me._optType_2.TabStop = True
        Me._optType_2.Text = "Debtors"
        Me._optType_2.UseVisualStyleBackColor = False
        '
        '_optType_1
        '
        Me._optType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_1, CType(1, Short))
        Me._optType_1.Location = New System.Drawing.Point(265, 18)
        Me._optType_1.Name = "_optType_1"
        Me._optType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_1.Size = New System.Drawing.Size(87, 17)
        Me._optType_1.TabIndex = 12
        Me._optType_1.TabStop = True
        Me._optType_1.Text = "General"
        Me._optType_1.UseVisualStyleBackColor = False
        '
        '_optType_0
        '
        Me._optType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optType_0.Checked = True
        Me._optType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_0, CType(0, Short))
        Me._optType_0.Location = New System.Drawing.Point(173, 18)
        Me._optType_0.Name = "_optType_0"
        Me._optType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_0.Size = New System.Drawing.Size(86, 21)
        Me._optType_0.TabIndex = 11
        Me._optType_0.TabStop = True
        Me._optType_0.Text = "Expenses"
        Me._optType_0.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.txtMISSeqNo)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.cboGroupType)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 211)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(639, 49)
        Me.Frame2.TabIndex = 36
        Me.Frame2.TabStop = False
        '
        'chkStockGroup
        '
        Me.chkStockGroup.AutoSize = True
        Me.chkStockGroup.BackColor = System.Drawing.SystemColors.Control
        Me.chkStockGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStockGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStockGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStockGroup.Location = New System.Drawing.Point(6, 20)
        Me.chkStockGroup.Name = "chkStockGroup"
        Me.chkStockGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStockGroup.Size = New System.Drawing.Size(94, 18)
        Me.chkStockGroup.TabIndex = 41
        Me.chkStockGroup.Text = "Stock Group"
        Me.chkStockGroup.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(98, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "MIS Head :"
        '
        'txtMISSeqNo
        '
        Me.txtMISSeqNo.AcceptsReturn = True
        Me.txtMISSeqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMISSeqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMISSeqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMISSeqNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMISSeqNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMISSeqNo.Location = New System.Drawing.Point(561, 17)
        Me.txtMISSeqNo.MaxLength = 0
        Me.txtMISSeqNo.Name = "txtMISSeqNo"
        Me.txtMISSeqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMISSeqNo.Size = New System.Drawing.Size(53, 22)
        Me.txtMISSeqNo.TabIndex = 38
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(482, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "MIS Seq No. :"
        '
        'cboGroupType
        '
        Me.cboGroupType.BackColor = System.Drawing.SystemColors.Window
        Me.cboGroupType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGroupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGroupType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGroupType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGroupType.Location = New System.Drawing.Point(168, 17)
        Me.cboGroupType.Name = "cboGroupType"
        Me.cboGroupType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGroupType.Size = New System.Drawing.Size(293, 21)
        Me.cboGroupType.TabIndex = 37
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._OptStatus_0)
        Me.Frame3.Controls.Add(Me._OptStatus_1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(645, 89)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(125, 52)
        Me.Frame3.TabIndex = 38
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Status"
        '
        '_OptStatus_0
        '
        Me._OptStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_0.Checked = True
        Me._OptStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_0, CType(0, Short))
        Me._OptStatus_0.Location = New System.Drawing.Point(49, 8)
        Me._OptStatus_0.Name = "_OptStatus_0"
        Me._OptStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_0.Size = New System.Drawing.Size(68, 20)
        Me._OptStatus_0.TabIndex = 40
        Me._OptStatus_0.TabStop = True
        Me._OptStatus_0.Text = "Open"
        Me._OptStatus_0.UseVisualStyleBackColor = False
        '
        '_OptStatus_1
        '
        Me._OptStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_1, CType(1, Short))
        Me._OptStatus_1.Location = New System.Drawing.Point(49, 30)
        Me._OptStatus_1.Name = "_OptStatus_1"
        Me._OptStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_1.Size = New System.Drawing.Size(68, 20)
        Me._OptStatus_1.TabIndex = 39
        Me._OptStatus_1.TabStop = True
        Me._OptStatus_1.Text = "Close"
        Me._OptStatus_1.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 27
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(774, 253)
        Me.SprdView.TabIndex = 0
        '
        'ADataMain
        '
        Me.ADataMain.BackColor = System.Drawing.SystemColors.Window
        Me.ADataMain.CommandTimeout = 0
        Me.ADataMain.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataMain.ConnectionString = Nothing
        Me.ADataMain.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataMain.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataMain.Location = New System.Drawing.Point(0, 0)
        Me.ADataMain.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataMain.Name = "ADataMain"
        Me.ADataMain.Size = New System.Drawing.Size(231, 39)
        Me.ADataMain.TabIndex = 28
        Me.ADataMain.Text = "ADataMain"
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 251)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(774, 53)
        Me.FraMovement.TabIndex = 25
        Me.FraMovement.TabStop = False
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(0, 24)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(105, 13)
        Me._lblLabels_2.TabIndex = 24
        Me._lblLabels_2.Text = "Group Name"
        '
        'OptStatus
        '
        '
        'optType
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.optOpeningBalHead)
        Me.GroupBox1.Controls.Add(Me.optClosingBalHead)
        Me.GroupBox1.Controls.Add(Me.chkStockGroup)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(644, 160)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(125, 97)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Stock Group"
        '
        'optOpeningBalHead
        '
        Me.optOpeningBalHead.AutoSize = True
        Me.optOpeningBalHead.BackColor = System.Drawing.SystemColors.Control
        Me.optOpeningBalHead.Checked = True
        Me.optOpeningBalHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOpeningBalHead.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optOpeningBalHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOpeningBalHead.Location = New System.Drawing.Point(8, 43)
        Me.optOpeningBalHead.Name = "optOpeningBalHead"
        Me.optOpeningBalHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOpeningBalHead.Size = New System.Drawing.Size(98, 17)
        Me.optOpeningBalHead.TabIndex = 43
        Me.optOpeningBalHead.TabStop = True
        Me.optOpeningBalHead.Text = "Opening Head"
        Me.optOpeningBalHead.UseVisualStyleBackColor = False
        '
        'optClosingBalHead
        '
        Me.optClosingBalHead.AutoSize = True
        Me.optClosingBalHead.BackColor = System.Drawing.SystemColors.Control
        Me.optClosingBalHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.optClosingBalHead.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optClosingBalHead.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optClosingBalHead.Location = New System.Drawing.Point(8, 65)
        Me.optClosingBalHead.Name = "optClosingBalHead"
        Me.optClosingBalHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optClosingBalHead.Size = New System.Drawing.Size(93, 17)
        Me.optClosingBalHead.TabIndex = 42
        Me.optClosingBalHead.TabStop = True
        Me.optClosingBalHead.Text = "Closing Head"
        Me.optClosingBalHead.UseVisualStyleBackColor = False
        '
        'frmAcmGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(778, 316)
        Me.Controls.Add(Me.fraMain)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.ADataMain)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAcmGroup"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Account Group"
        Me.fraMain.ResumeLayout(False)
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.fraBSGroup.ResumeLayout(False)
        Me.fraBSGroup.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(ADataMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Public WithEvents Label4 As Label
    Public WithEvents txtMISSeqNo As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents chkStockGroup As CheckBox
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents optOpeningBalHead As RadioButton
    Public WithEvents optClosingBalHead As RadioButton
#End Region
End Class