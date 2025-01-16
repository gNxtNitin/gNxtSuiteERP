Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMnuRightsNew
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
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents _OptShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cmpPopulate As System.Windows.Forms.Button
    Public WithEvents chkAllUserID As System.Windows.Forms.CheckBox
    Public WithEvents txtMenuName As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchMenu As System.Windows.Forms.Button
    Public WithEvents chkAllMenu As System.Windows.Forms.CheckBox
    Public WithEvents chkAllModule As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearchModule As System.Windows.Forms.Button
    Public WithEvents txtModuleName As System.Windows.Forms.TextBox
    Public WithEvents txtUserId As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents LblUserName As System.Windows.Forms.Label
    Public WithEvents LblUserID As System.Windows.Forms.Label
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents _OptRights_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptRights_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkAllView As System.Windows.Forms.CheckBox
    Public WithEvents chkAllDelete As System.Windows.Forms.CheckBox
    Public WithEvents chkAllModify As System.Windows.Forms.CheckBox
    Public WithEvents ChkAllAdd As System.Windows.Forms.CheckBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraDetail As System.Windows.Forms.GroupBox
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents OptRights As VB6.RadioButtonArray
    Public WithEvents OptShow As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMnuRightsNew))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearchMenu = New System.Windows.Forms.Button()
        Me.cmdsearchModule = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._OptShow_0 = New System.Windows.Forms.RadioButton()
        Me._OptShow_1 = New System.Windows.Forms.RadioButton()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.cmpPopulate = New System.Windows.Forms.Button()
        Me.chkAllUserID = New System.Windows.Forms.CheckBox()
        Me.txtMenuName = New System.Windows.Forms.TextBox()
        Me.chkAllMenu = New System.Windows.Forms.CheckBox()
        Me.chkAllModule = New System.Windows.Forms.CheckBox()
        Me.txtModuleName = New System.Windows.Forms.TextBox()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblUserName = New System.Windows.Forms.Label()
        Me.LblUserID = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptRights_1 = New System.Windows.Forms.RadioButton()
        Me._OptRights_0 = New System.Windows.Forms.RadioButton()
        Me.FraDetail = New System.Windows.Forms.GroupBox()
        Me.chkAllView = New System.Windows.Forms.CheckBox()
        Me.chkAllDelete = New System.Windows.Forms.CheckBox()
        Me.chkAllModify = New System.Windows.Forms.CheckBox()
        Me.ChkAllAdd = New System.Windows.Forms.CheckBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptRights = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame3.SuspendLayout()
        Me.FraMain.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraDetail.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptRights, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearchMenu
        '
        Me.cmdsearchMenu.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchMenu.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchMenu.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchMenu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchMenu.Image = CType(resources.GetObject("cmdsearchMenu.Image"), System.Drawing.Image)
        Me.cmdsearchMenu.Location = New System.Drawing.Point(338, 59)
        Me.cmdsearchMenu.Name = "cmdsearchMenu"
        Me.cmdsearchMenu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchMenu.Size = New System.Drawing.Size(29, 25)
        Me.cmdsearchMenu.TabIndex = 9
        Me.cmdsearchMenu.TabStop = False
        Me.cmdsearchMenu.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchMenu, "Search")
        Me.cmdsearchMenu.UseVisualStyleBackColor = False
        '
        'cmdsearchModule
        '
        Me.cmdsearchModule.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchModule.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchModule.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchModule.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchModule.Image = CType(resources.GetObject("cmdsearchModule.Image"), System.Drawing.Image)
        Me.cmdsearchModule.Location = New System.Drawing.Point(338, 33)
        Me.cmdsearchModule.Name = "cmdsearchModule"
        Me.cmdsearchModule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchModule.Size = New System.Drawing.Size(29, 23)
        Me.cmdsearchModule.TabIndex = 6
        Me.cmdsearchModule.TabStop = False
        Me.cmdsearchModule.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchModule, "Search")
        Me.cmdsearchModule.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdSave.Location = New System.Drawing.Point(2, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(100, 38)
        Me.CmdSave.TabIndex = 21
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdClose.Location = New System.Drawing.Point(1001, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(100, 38)
        Me.CmdClose.TabIndex = 22
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._OptShow_0)
        Me.Frame3.Controls.Add(Me._OptShow_1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame3.Location = New System.Drawing.Point(552, 47)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(177, 43)
        Me.Frame3.TabIndex = 26
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Show"
        '
        '_OptShow_0
        '
        Me._OptShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_0.Checked = True
        Me._OptShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_0, CType(0, Short))
        Me._OptShow_0.Location = New System.Drawing.Point(4, 14)
        Me._OptShow_0.Name = "_OptShow_0"
        Me._OptShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_0.Size = New System.Drawing.Size(98, 21)
        Me._OptShow_0.TabIndex = 14
        Me._OptShow_0.TabStop = True
        Me._OptShow_0.Text = "Only Active"
        Me._OptShow_0.UseVisualStyleBackColor = False
        '
        '_OptShow_1
        '
        Me._OptShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_1, CType(1, Short))
        Me._OptShow_1.Location = New System.Drawing.Point(112, 14)
        Me._OptShow_1.Name = "_OptShow_1"
        Me._OptShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_1.Size = New System.Drawing.Size(62, 21)
        Me._OptShow_1.TabIndex = 15
        Me._OptShow_1.TabStop = True
        Me._OptShow_1.Text = "All"
        Me._OptShow_1.UseVisualStyleBackColor = False
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.cmpPopulate)
        Me.FraMain.Controls.Add(Me.chkAllUserID)
        Me.FraMain.Controls.Add(Me.txtMenuName)
        Me.FraMain.Controls.Add(Me.cmdsearchMenu)
        Me.FraMain.Controls.Add(Me.chkAllMenu)
        Me.FraMain.Controls.Add(Me.chkAllModule)
        Me.FraMain.Controls.Add(Me.cmdsearchModule)
        Me.FraMain.Controls.Add(Me.txtModuleName)
        Me.FraMain.Controls.Add(Me.txtUserId)
        Me.FraMain.Controls.Add(Me.Label2)
        Me.FraMain.Controls.Add(Me.Label1)
        Me.FraMain.Controls.Add(Me.LblUserName)
        Me.FraMain.Controls.Add(Me.LblUserID)
        Me.FraMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, 0)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(551, 90)
        Me.FraMain.TabIndex = 0
        Me.FraMain.TabStop = False
        '
        'cmpPopulate
        '
        Me.cmpPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmpPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmpPopulate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmpPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmpPopulate.Location = New System.Drawing.Point(450, 44)
        Me.cmpPopulate.Name = "cmpPopulate"
        Me.cmpPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmpPopulate.Size = New System.Drawing.Size(91, 31)
        Me.cmpPopulate.TabIndex = 11
        Me.cmpPopulate.Text = "Populate"
        Me.cmpPopulate.UseVisualStyleBackColor = False
        '
        'chkAllUserID
        '
        Me.chkAllUserID.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllUserID.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllUserID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllUserID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllUserID.Location = New System.Drawing.Point(500, 10)
        Me.chkAllUserID.Name = "chkAllUserID"
        Me.chkAllUserID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllUserID.Size = New System.Drawing.Size(48, 19)
        Me.chkAllUserID.TabIndex = 4
        Me.chkAllUserID.Text = "All"
        Me.chkAllUserID.UseVisualStyleBackColor = False
        '
        'txtMenuName
        '
        Me.txtMenuName.AcceptsReturn = True
        Me.txtMenuName.BackColor = System.Drawing.SystemColors.Window
        Me.txtMenuName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMenuName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMenuName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMenuName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMenuName.Location = New System.Drawing.Point(86, 61)
        Me.txtMenuName.MaxLength = 0
        Me.txtMenuName.Name = "txtMenuName"
        Me.txtMenuName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMenuName.Size = New System.Drawing.Size(251, 22)
        Me.txtMenuName.TabIndex = 8
        '
        'chkAllMenu
        '
        Me.chkAllMenu.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllMenu.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllMenu.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllMenu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllMenu.Location = New System.Drawing.Point(368, 64)
        Me.chkAllMenu.Name = "chkAllMenu"
        Me.chkAllMenu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllMenu.Size = New System.Drawing.Size(67, 18)
        Me.chkAllMenu.TabIndex = 10
        Me.chkAllMenu.Text = "All"
        Me.chkAllMenu.UseVisualStyleBackColor = False
        '
        'chkAllModule
        '
        Me.chkAllModule.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllModule.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllModule.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllModule.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllModule.Location = New System.Drawing.Point(368, 38)
        Me.chkAllModule.Name = "chkAllModule"
        Me.chkAllModule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllModule.Size = New System.Drawing.Size(67, 18)
        Me.chkAllModule.TabIndex = 7
        Me.chkAllModule.Text = "All"
        Me.chkAllModule.UseVisualStyleBackColor = False
        '
        'txtModuleName
        '
        Me.txtModuleName.AcceptsReturn = True
        Me.txtModuleName.BackColor = System.Drawing.SystemColors.Window
        Me.txtModuleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModuleName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModuleName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModuleName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModuleName.Location = New System.Drawing.Point(86, 35)
        Me.txtModuleName.MaxLength = 0
        Me.txtModuleName.Name = "txtModuleName"
        Me.txtModuleName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModuleName.Size = New System.Drawing.Size(251, 22)
        Me.txtModuleName.TabIndex = 5
        '
        'txtUserId
        '
        Me.txtUserId.AcceptsReturn = True
        Me.txtUserId.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserId.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserId.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserId.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserId.Location = New System.Drawing.Point(86, 10)
        Me.txtUserId.MaxLength = 0
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserId.Size = New System.Drawing.Size(81, 22)
        Me.txtUserId.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Menu Name :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Module:"
        '
        'LblUserName
        '
        Me.LblUserName.BackColor = System.Drawing.SystemColors.Control
        Me.LblUserName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblUserName.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblUserName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUserName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblUserName.Location = New System.Drawing.Point(168, 10)
        Me.LblUserName.Name = "LblUserName"
        Me.LblUserName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblUserName.Size = New System.Drawing.Size(329, 20)
        Me.LblUserName.TabIndex = 3
        '
        'LblUserID
        '
        Me.LblUserID.AutoSize = True
        Me.LblUserID.BackColor = System.Drawing.SystemColors.Control
        Me.LblUserID.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblUserID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUserID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblUserID.Location = New System.Drawing.Point(8, 12)
        Me.LblUserID.Name = "LblUserID"
        Me.LblUserID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblUserID.Size = New System.Drawing.Size(47, 13)
        Me.LblUserID.TabIndex = 1
        Me.LblUserID.Text = "UserID :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._OptRights_1)
        Me.Frame1.Controls.Add(Me._OptRights_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(552, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(177, 46)
        Me.Frame1.TabIndex = 4
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Assign Rights"
        '
        '_OptRights_1
        '
        Me._OptRights_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptRights_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRights_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptRights_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptRights.SetIndex(Me._OptRights_1, CType(1, Short))
        Me._OptRights_1.Location = New System.Drawing.Point(90, 16)
        Me._OptRights_1.Name = "_OptRights_1"
        Me._OptRights_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRights_1.Size = New System.Drawing.Size(83, 17)
        Me._OptRights_1.TabIndex = 13
        Me._OptRights_1.TabStop = True
        Me._OptRights_1.Text = "None"
        Me._OptRights_1.UseVisualStyleBackColor = False
        '
        '_OptRights_0
        '
        Me._OptRights_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptRights_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRights_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptRights_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptRights.SetIndex(Me._OptRights_0, CType(0, Short))
        Me._OptRights_0.Location = New System.Drawing.Point(4, 16)
        Me._OptRights_0.Name = "_OptRights_0"
        Me._OptRights_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRights_0.Size = New System.Drawing.Size(61, 17)
        Me._OptRights_0.TabIndex = 12
        Me._OptRights_0.TabStop = True
        Me._OptRights_0.Text = "All"
        Me._OptRights_0.UseVisualStyleBackColor = False
        '
        'FraDetail
        '
        Me.FraDetail.BackColor = System.Drawing.SystemColors.Control
        Me.FraDetail.Controls.Add(Me.chkAllView)
        Me.FraDetail.Controls.Add(Me.chkAllDelete)
        Me.FraDetail.Controls.Add(Me.chkAllModify)
        Me.FraDetail.Controls.Add(Me.ChkAllAdd)
        Me.FraDetail.Controls.Add(Me.SprdMain)
        Me.FraDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraDetail.Location = New System.Drawing.Point(0, 86)
        Me.FraDetail.Name = "FraDetail"
        Me.FraDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.FraDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraDetail.Size = New System.Drawing.Size(1105, 487)
        Me.FraDetail.TabIndex = 7
        Me.FraDetail.TabStop = False
        '
        'chkAllView
        '
        Me.chkAllView.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllView.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllView.Location = New System.Drawing.Point(460, 11)
        Me.chkAllView.Name = "chkAllView"
        Me.chkAllView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllView.Size = New System.Drawing.Size(132, 20)
        Me.chkAllView.TabIndex = 19
        Me.chkAllView.Text = "All View"
        Me.chkAllView.UseVisualStyleBackColor = False
        '
        'chkAllDelete
        '
        Me.chkAllDelete.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDelete.Location = New System.Drawing.Point(314, 11)
        Me.chkAllDelete.Name = "chkAllDelete"
        Me.chkAllDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDelete.Size = New System.Drawing.Size(132, 20)
        Me.chkAllDelete.TabIndex = 18
        Me.chkAllDelete.Text = "All Delete"
        Me.chkAllDelete.UseVisualStyleBackColor = False
        '
        'chkAllModify
        '
        Me.chkAllModify.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllModify.Location = New System.Drawing.Point(168, 11)
        Me.chkAllModify.Name = "chkAllModify"
        Me.chkAllModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllModify.Size = New System.Drawing.Size(132, 20)
        Me.chkAllModify.TabIndex = 17
        Me.chkAllModify.Text = "All Modify"
        Me.chkAllModify.UseVisualStyleBackColor = False
        '
        'ChkAllAdd
        '
        Me.ChkAllAdd.BackColor = System.Drawing.SystemColors.Control
        Me.ChkAllAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkAllAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAllAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkAllAdd.Location = New System.Drawing.Point(38, 11)
        Me.ChkAllAdd.Name = "ChkAllAdd"
        Me.ChkAllAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkAllAdd.Size = New System.Drawing.Size(114, 20)
        Me.ChkAllAdd.TabIndex = 16
        Me.ChkAllAdd.Text = "All ADD"
        Me.ChkAllAdd.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 34)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1100, 450)
        Me.SprdMain.TabIndex = 20
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.CmdSave)
        Me.Frame2.Controls.Add(Me.CmdClose)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 569)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(1107, 53)
        Me.Frame2.TabIndex = 9
        Me.Frame2.TabStop = False
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
        'OptRights
        '
        '
        'OptShow
        '
        '
        'frmMnuRightsNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraDetail)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Report1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMnuRightsNew"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Security - Menu Control"
        Me.Frame3.ResumeLayout(False)
        Me.FraMain.ResumeLayout(False)
        Me.FraMain.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.FraDetail.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptRights, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class