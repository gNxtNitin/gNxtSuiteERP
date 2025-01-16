Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUsers
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUsers))
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.fraShow = New System.Windows.Forms.GroupBox()
        Me.txtDLLFileName = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtDLLPathName = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtDSCertificateNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtDigitalSignUID = New System.Windows.Forms.TextBox()
        Me.txtDigitalSignPassword = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkDigitalSign = New System.Windows.Forms.CheckBox()
        Me.SSTab = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chkAllow_ExcessIssue = New System.Windows.Forms.CheckBox()
        Me.chkInv_Level_AppUser = New System.Windows.Forms.CheckBox()
        Me.chkInv_LevelUser = New System.Windows.Forms.CheckBox()
        Me.chkPay_CorpUser = New System.Windows.Forms.CheckBox()
        Me.chkAllow_StockAdj = New System.Windows.Forms.CheckBox()
        Me.chkAllow_PoprintApp = New System.Windows.Forms.CheckBox()
        Me.chkAllow_RmPo = New System.Windows.Forms.CheckBox()
        Me.chkAllow_BopPo = New System.Windows.Forms.CheckBox()
        Me.chkAllow_AccountMaster = New System.Windows.Forms.CheckBox()
        Me.chkBookLocking = New System.Windows.Forms.CheckBox()
        Me.chkInvoiceAdmin = New System.Windows.Forms.CheckBox()
        Me.chkDS = New System.Windows.Forms.CheckBox()
        Me.chkRunDate = New System.Windows.Forms.CheckBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.OptStatOpen = New System.Windows.Forms.RadioButton()
        Me.OptStatClosed = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.optDeptRights = New System.Windows.Forms.RadioButton()
        Me.optBranchRights = New System.Windows.Forms.RadioButton()
        Me.optMenuRights = New System.Windows.Forms.RadioButton()
        Me.optModuleRights = New System.Windows.Forms.RadioButton()
        Me.fraView = New System.Windows.Forms.GroupBox()
        Me.txtEquivalentName = New System.Windows.Forms.TextBox()
        Me.cmdsearchEquivalent = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.txteMailId = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtWindowUser = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtIPAddress = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboUserType = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtAdminPassword = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboLevel = New System.Windows.Forms.ComboBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtEquivalent = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtpassword = New System.Windows.Forms.TextBox()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.LblLevel = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me._Label2_1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.sprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.FraMain.SuspendLayout()
        Me.fraShow.SuspendLayout()
        Me.SSTab.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.fraView.SuspendLayout()
        CType(Me.sprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.fraShow)
        Me.FraMain.Controls.Add(Me.sprdView)
        Me.FraMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(-1, -5)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(629, 521)
        Me.FraMain.TabIndex = 25
        Me.FraMain.TabStop = False
        '
        'fraShow
        '
        Me.fraShow.Controls.Add(Me.txtDLLFileName)
        Me.fraShow.Controls.Add(Me.Label13)
        Me.fraShow.Controls.Add(Me.txtDLLPathName)
        Me.fraShow.Controls.Add(Me.Label12)
        Me.fraShow.Controls.Add(Me.Label11)
        Me.fraShow.Controls.Add(Me.txtDSCertificateNo)
        Me.fraShow.Controls.Add(Me.Label22)
        Me.fraShow.Controls.Add(Me.txtDigitalSignUID)
        Me.fraShow.Controls.Add(Me.txtDigitalSignPassword)
        Me.fraShow.Controls.Add(Me.Label9)
        Me.fraShow.Controls.Add(Me.chkDigitalSign)
        Me.fraShow.Controls.Add(Me.SSTab)
        Me.fraShow.Controls.Add(Me.chkRunDate)
        Me.fraShow.Controls.Add(Me.Frame2)
        Me.fraShow.Controls.Add(Me.Frame1)
        Me.fraShow.Controls.Add(Me.fraView)
        Me.fraShow.Location = New System.Drawing.Point(1, 1)
        Me.fraShow.Name = "fraShow"
        Me.fraShow.Size = New System.Drawing.Size(627, 519)
        Me.fraShow.TabIndex = 41
        Me.fraShow.TabStop = False
        '
        'txtDLLFileName
        '
        Me.txtDLLFileName.AcceptsReturn = True
        Me.txtDLLFileName.BackColor = System.Drawing.SystemColors.Window
        Me.txtDLLFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDLLFileName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDLLFileName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDLLFileName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDLLFileName.Location = New System.Drawing.Point(131, 319)
        Me.txtDLLFileName.MaxLength = 0
        Me.txtDLLFileName.Name = "txtDLLFileName"
        Me.txtDLLFileName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDLLFileName.Size = New System.Drawing.Size(236, 22)
        Me.txtDLLFileName.TabIndex = 54
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(11, 321)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(85, 13)
        Me.Label13.TabIndex = 55
        Me.Label13.Text = "DLL File Name :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDLLPathName
        '
        Me.txtDLLPathName.AcceptsReturn = True
        Me.txtDLLPathName.BackColor = System.Drawing.SystemColors.Window
        Me.txtDLLPathName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDLLPathName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDLLPathName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDLLPathName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDLLPathName.Location = New System.Drawing.Point(131, 293)
        Me.txtDLLPathName.MaxLength = 0
        Me.txtDLLPathName.Name = "txtDLLPathName"
        Me.txtDLLPathName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDLLPathName.Size = New System.Drawing.Size(236, 22)
        Me.txtDLLPathName.TabIndex = 52
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(11, 295)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(89, 13)
        Me.Label12.TabIndex = 53
        Me.Label12.Text = "DLL Path Name :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(393, 298)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(61, 13)
        Me.Label11.TabIndex = 51
        Me.Label11.Text = "Password :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDSCertificateNo
        '
        Me.txtDSCertificateNo.AcceptsReturn = True
        Me.txtDSCertificateNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDSCertificateNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDSCertificateNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDSCertificateNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDSCertificateNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDSCertificateNo.Location = New System.Drawing.Point(464, 267)
        Me.txtDSCertificateNo.MaxLength = 0
        Me.txtDSCertificateNo.Name = "txtDSCertificateNo"
        Me.txtDSCertificateNo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtDSCertificateNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDSCertificateNo.Size = New System.Drawing.Size(112, 22)
        Me.txtDSCertificateNo.TabIndex = 49
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(370, 269)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(89, 13)
        Me.Label22.TabIndex = 50
        Me.Label22.Text = "Certificate SNo :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDigitalSignUID
        '
        Me.txtDigitalSignUID.AcceptsReturn = True
        Me.txtDigitalSignUID.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigitalSignUID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigitalSignUID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigitalSignUID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigitalSignUID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigitalSignUID.Location = New System.Drawing.Point(131, 267)
        Me.txtDigitalSignUID.MaxLength = 0
        Me.txtDigitalSignUID.Name = "txtDigitalSignUID"
        Me.txtDigitalSignUID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigitalSignUID.Size = New System.Drawing.Size(236, 22)
        Me.txtDigitalSignUID.TabIndex = 46
        '
        'txtDigitalSignPassword
        '
        Me.txtDigitalSignPassword.AcceptsReturn = True
        Me.txtDigitalSignPassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigitalSignPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigitalSignPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigitalSignPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigitalSignPassword.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigitalSignPassword.Location = New System.Drawing.Point(464, 292)
        Me.txtDigitalSignPassword.MaxLength = 0
        Me.txtDigitalSignPassword.Name = "txtDigitalSignPassword"
        Me.txtDigitalSignPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtDigitalSignPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigitalSignPassword.Size = New System.Drawing.Size(112, 22)
        Me.txtDigitalSignPassword.TabIndex = 47
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(11, 269)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(69, 13)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "User Name :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkDigitalSign
        '
        Me.chkDigitalSign.BackColor = System.Drawing.SystemColors.Control
        Me.chkDigitalSign.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDigitalSign.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDigitalSign.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDigitalSign.Location = New System.Drawing.Point(464, 243)
        Me.chkDigitalSign.Name = "chkDigitalSign"
        Me.chkDigitalSign.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDigitalSign.Size = New System.Drawing.Size(169, 17)
        Me.chkDigitalSign.TabIndex = 45
        Me.chkDigitalSign.Text = "Digital Signature (Yes / No)"
        Me.chkDigitalSign.UseVisualStyleBackColor = False
        '
        'SSTab
        '
        Me.SSTab.Controls.Add(Me.TabPage1)
        Me.SSTab.Controls.Add(Me.TabPage2)
        Me.SSTab.Location = New System.Drawing.Point(3, 348)
        Me.SSTab.Name = "SSTab"
        Me.SSTab.SelectedIndex = 0
        Me.SSTab.Size = New System.Drawing.Size(621, 165)
        Me.SSTab.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.SprdMain)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(613, 139)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Company Rights"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 4)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(606, 130)
        Me.SprdMain.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.chkAllow_ExcessIssue)
        Me.TabPage2.Controls.Add(Me.chkInv_Level_AppUser)
        Me.TabPage2.Controls.Add(Me.chkInv_LevelUser)
        Me.TabPage2.Controls.Add(Me.chkPay_CorpUser)
        Me.TabPage2.Controls.Add(Me.chkAllow_StockAdj)
        Me.TabPage2.Controls.Add(Me.chkAllow_PoprintApp)
        Me.TabPage2.Controls.Add(Me.chkAllow_RmPo)
        Me.TabPage2.Controls.Add(Me.chkAllow_BopPo)
        Me.TabPage2.Controls.Add(Me.chkAllow_AccountMaster)
        Me.TabPage2.Controls.Add(Me.chkBookLocking)
        Me.TabPage2.Controls.Add(Me.chkInvoiceAdmin)
        Me.TabPage2.Controls.Add(Me.chkDS)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(613, 139)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Other Rights"
        '
        'chkAllow_ExcessIssue
        '
        Me.chkAllow_ExcessIssue.AutoSize = True
        Me.chkAllow_ExcessIssue.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllow_ExcessIssue.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllow_ExcessIssue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllow_ExcessIssue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllow_ExcessIssue.Location = New System.Drawing.Point(432, 114)
        Me.chkAllow_ExcessIssue.Name = "chkAllow_ExcessIssue"
        Me.chkAllow_ExcessIssue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllow_ExcessIssue.Size = New System.Drawing.Size(171, 17)
        Me.chkAllow_ExcessIssue.TabIndex = 33
        Me.chkAllow_ExcessIssue.Text = "Allow Excess Issue (Yes / No)"
        Me.chkAllow_ExcessIssue.UseVisualStyleBackColor = False
        '
        'chkInv_Level_AppUser
        '
        Me.chkInv_Level_AppUser.AutoSize = True
        Me.chkInv_Level_AppUser.BackColor = System.Drawing.SystemColors.Control
        Me.chkInv_Level_AppUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInv_Level_AppUser.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInv_Level_AppUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInv_Level_AppUser.Location = New System.Drawing.Point(432, 78)
        Me.chkInv_Level_AppUser.Name = "chkInv_Level_AppUser"
        Me.chkInv_Level_AppUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInv_Level_AppUser.Size = New System.Drawing.Size(158, 17)
        Me.chkInv_Level_AppUser.TabIndex = 32
        Me.chkInv_Level_AppUser.Text = "Allow Max Level Approval"
        Me.chkInv_Level_AppUser.UseVisualStyleBackColor = False
        '
        'chkInv_LevelUser
        '
        Me.chkInv_LevelUser.AutoSize = True
        Me.chkInv_LevelUser.BackColor = System.Drawing.SystemColors.Control
        Me.chkInv_LevelUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInv_LevelUser.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInv_LevelUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInv_LevelUser.Location = New System.Drawing.Point(432, 42)
        Me.chkInv_LevelUser.Name = "chkInv_LevelUser"
        Me.chkInv_LevelUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInv_LevelUser.Size = New System.Drawing.Size(149, 17)
        Me.chkInv_LevelUser.TabIndex = 31
        Me.chkInv_LevelUser.Text = "Allow Max Level Change"
        Me.chkInv_LevelUser.UseVisualStyleBackColor = False
        '
        'chkPay_CorpUser
        '
        Me.chkPay_CorpUser.AutoSize = True
        Me.chkPay_CorpUser.BackColor = System.Drawing.SystemColors.Control
        Me.chkPay_CorpUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPay_CorpUser.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPay_CorpUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPay_CorpUser.Location = New System.Drawing.Point(432, 7)
        Me.chkPay_CorpUser.Name = "chkPay_CorpUser"
        Me.chkPay_CorpUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPay_CorpUser.Size = New System.Drawing.Size(175, 17)
        Me.chkPay_CorpUser.TabIndex = 30
        Me.chkPay_CorpUser.Text = "HR Corporate User (Yes / No)"
        Me.chkPay_CorpUser.UseVisualStyleBackColor = False
        '
        'chkAllow_StockAdj
        '
        Me.chkAllow_StockAdj.AutoSize = True
        Me.chkAllow_StockAdj.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllow_StockAdj.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllow_StockAdj.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllow_StockAdj.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllow_StockAdj.Location = New System.Drawing.Point(231, 114)
        Me.chkAllow_StockAdj.Name = "chkAllow_StockAdj"
        Me.chkAllow_StockAdj.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllow_StockAdj.Size = New System.Drawing.Size(199, 17)
        Me.chkAllow_StockAdj.TabIndex = 29
        Me.chkAllow_StockAdj.Text = "Allow Stock Adjustment (Yes / No)"
        Me.chkAllow_StockAdj.UseVisualStyleBackColor = False
        '
        'chkAllow_PoprintApp
        '
        Me.chkAllow_PoprintApp.AutoSize = True
        Me.chkAllow_PoprintApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllow_PoprintApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllow_PoprintApp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllow_PoprintApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllow_PoprintApp.Location = New System.Drawing.Point(231, 78)
        Me.chkAllow_PoprintApp.Name = "chkAllow_PoprintApp"
        Me.chkAllow_PoprintApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllow_PoprintApp.Size = New System.Drawing.Size(200, 17)
        Me.chkAllow_PoprintApp.TabIndex = 28
        Me.chkAllow_PoprintApp.Text = "Allow PO Print Approval (Yes / No)"
        Me.chkAllow_PoprintApp.UseVisualStyleBackColor = False
        '
        'chkAllow_RmPo
        '
        Me.chkAllow_RmPo.AutoSize = True
        Me.chkAllow_RmPo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllow_RmPo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllow_RmPo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllow_RmPo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllow_RmPo.Location = New System.Drawing.Point(231, 42)
        Me.chkAllow_RmPo.Name = "chkAllow_RmPo"
        Me.chkAllow_RmPo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllow_RmPo.Size = New System.Drawing.Size(180, 17)
        Me.chkAllow_RmPo.TabIndex = 27
        Me.chkAllow_RmPo.Text = "Allow Create RM PO (Yes / No)"
        Me.chkAllow_RmPo.UseVisualStyleBackColor = False
        '
        'chkAllow_BopPo
        '
        Me.chkAllow_BopPo.AutoSize = True
        Me.chkAllow_BopPo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllow_BopPo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllow_BopPo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllow_BopPo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllow_BopPo.Location = New System.Drawing.Point(231, 7)
        Me.chkAllow_BopPo.Name = "chkAllow_BopPo"
        Me.chkAllow_BopPo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllow_BopPo.Size = New System.Drawing.Size(184, 17)
        Me.chkAllow_BopPo.TabIndex = 26
        Me.chkAllow_BopPo.Text = "Allow Create BOP PO (Yes / No)"
        Me.chkAllow_BopPo.UseVisualStyleBackColor = False
        '
        'chkAllow_AccountMaster
        '
        Me.chkAllow_AccountMaster.AutoSize = True
        Me.chkAllow_AccountMaster.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllow_AccountMaster.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllow_AccountMaster.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllow_AccountMaster.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllow_AccountMaster.Location = New System.Drawing.Point(7, 114)
        Me.chkAllow_AccountMaster.Name = "chkAllow_AccountMaster"
        Me.chkAllow_AccountMaster.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllow_AccountMaster.Size = New System.Drawing.Size(221, 17)
        Me.chkAllow_AccountMaster.TabIndex = 25
        Me.chkAllow_AccountMaster.Text = "Allow Open  Account Master (Yes / No)"
        Me.chkAllow_AccountMaster.UseVisualStyleBackColor = False
        '
        'chkBookLocking
        '
        Me.chkBookLocking.AutoSize = True
        Me.chkBookLocking.BackColor = System.Drawing.SystemColors.Control
        Me.chkBookLocking.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBookLocking.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBookLocking.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBookLocking.Location = New System.Drawing.Point(7, 78)
        Me.chkBookLocking.Name = "chkBookLocking"
        Me.chkBookLocking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBookLocking.Size = New System.Drawing.Size(163, 17)
        Me.chkBookLocking.TabIndex = 24
        Me.chkBookLocking.Text = "Allow Book Lock (Yes / No)"
        Me.chkBookLocking.UseVisualStyleBackColor = False
        '
        'chkInvoiceAdmin
        '
        Me.chkInvoiceAdmin.AutoSize = True
        Me.chkInvoiceAdmin.BackColor = System.Drawing.SystemColors.Control
        Me.chkInvoiceAdmin.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInvoiceAdmin.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInvoiceAdmin.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInvoiceAdmin.Location = New System.Drawing.Point(7, 42)
        Me.chkInvoiceAdmin.Name = "chkInvoiceAdmin"
        Me.chkInvoiceAdmin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInvoiceAdmin.Size = New System.Drawing.Size(151, 17)
        Me.chkInvoiceAdmin.TabIndex = 23
        Me.chkInvoiceAdmin.Text = "Invoice Admin (Yes / No)"
        Me.chkInvoiceAdmin.UseVisualStyleBackColor = False
        '
        'chkDS
        '
        Me.chkDS.AutoSize = True
        Me.chkDS.BackColor = System.Drawing.SystemColors.Control
        Me.chkDS.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDS.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDS.Location = New System.Drawing.Point(7, 7)
        Me.chkDS.Name = "chkDS"
        Me.chkDS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDS.Size = New System.Drawing.Size(176, 17)
        Me.chkDS.TabIndex = 22
        Me.chkDS.Text = "Authorised DS Post (Yes / No)"
        Me.chkDS.UseVisualStyleBackColor = False
        '
        'chkRunDate
        '
        Me.chkRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.chkRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRunDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRunDate.Location = New System.Drawing.Point(464, 224)
        Me.chkRunDate.Name = "chkRunDate"
        Me.chkRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRunDate.Size = New System.Drawing.Size(169, 17)
        Me.chkRunDate.TabIndex = 17
        Me.chkRunDate.Text = "Run Date Change (Yes / No)"
        Me.chkRunDate.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.OptStatOpen)
        Me.Frame2.Controls.Add(Me.OptStatClosed)
        Me.Frame2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(466, 162)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(150, 56)
        Me.Frame2.TabIndex = 41
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Status"
        '
        'OptStatOpen
        '
        Me.OptStatOpen.BackColor = System.Drawing.SystemColors.Control
        Me.OptStatOpen.Checked = True
        Me.OptStatOpen.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptStatOpen.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptStatOpen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatOpen.Location = New System.Drawing.Point(63, 12)
        Me.OptStatOpen.Name = "OptStatOpen"
        Me.OptStatOpen.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptStatOpen.Size = New System.Drawing.Size(69, 20)
        Me.OptStatOpen.TabIndex = 16
        Me.OptStatOpen.TabStop = True
        Me.OptStatOpen.Text = "Open"
        Me.OptStatOpen.UseVisualStyleBackColor = False
        '
        'OptStatClosed
        '
        Me.OptStatClosed.BackColor = System.Drawing.SystemColors.Control
        Me.OptStatClosed.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptStatClosed.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptStatClosed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatClosed.Location = New System.Drawing.Point(63, 34)
        Me.OptStatClosed.Name = "OptStatClosed"
        Me.OptStatClosed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptStatClosed.Size = New System.Drawing.Size(69, 20)
        Me.OptStatClosed.TabIndex = 16
        Me.OptStatClosed.Text = "Close"
        Me.OptStatClosed.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.optDeptRights)
        Me.Frame1.Controls.Add(Me.optBranchRights)
        Me.Frame1.Controls.Add(Me.optMenuRights)
        Me.Frame1.Controls.Add(Me.optModuleRights)
        Me.Frame1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(465, 7)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(152, 151)
        Me.Frame1.TabIndex = 39
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Rights"
        '
        'optDeptRights
        '
        Me.optDeptRights.Appearance = System.Windows.Forms.Appearance.Button
        Me.optDeptRights.BackColor = System.Drawing.SystemColors.Control
        Me.optDeptRights.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDeptRights.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDeptRights.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDeptRights.Location = New System.Drawing.Point(12, 119)
        Me.optDeptRights.Name = "optDeptRights"
        Me.optDeptRights.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDeptRights.Size = New System.Drawing.Size(133, 29)
        Me.optDeptRights.TabIndex = 15
        Me.optDeptRights.TabStop = True
        Me.optDeptRights.Text = "Department Rights"
        Me.optDeptRights.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optDeptRights.UseVisualStyleBackColor = False
        '
        'optBranchRights
        '
        Me.optBranchRights.Appearance = System.Windows.Forms.Appearance.Button
        Me.optBranchRights.BackColor = System.Drawing.SystemColors.Control
        Me.optBranchRights.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBranchRights.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBranchRights.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBranchRights.Location = New System.Drawing.Point(12, 20)
        Me.optBranchRights.Name = "optBranchRights"
        Me.optBranchRights.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBranchRights.Size = New System.Drawing.Size(134, 29)
        Me.optBranchRights.TabIndex = 12
        Me.optBranchRights.TabStop = True
        Me.optBranchRights.Text = "Division Rights"
        Me.optBranchRights.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optBranchRights.UseVisualStyleBackColor = False
        '
        'optMenuRights
        '
        Me.optMenuRights.Appearance = System.Windows.Forms.Appearance.Button
        Me.optMenuRights.BackColor = System.Drawing.SystemColors.Control
        Me.optMenuRights.Cursor = System.Windows.Forms.Cursors.Default
        Me.optMenuRights.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMenuRights.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMenuRights.Location = New System.Drawing.Point(12, 86)
        Me.optMenuRights.Name = "optMenuRights"
        Me.optMenuRights.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optMenuRights.Size = New System.Drawing.Size(133, 29)
        Me.optMenuRights.TabIndex = 14
        Me.optMenuRights.TabStop = True
        Me.optMenuRights.Text = "Menu Rights"
        Me.optMenuRights.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optMenuRights.UseVisualStyleBackColor = False
        '
        'optModuleRights
        '
        Me.optModuleRights.Appearance = System.Windows.Forms.Appearance.Button
        Me.optModuleRights.BackColor = System.Drawing.SystemColors.Control
        Me.optModuleRights.Cursor = System.Windows.Forms.Cursors.Default
        Me.optModuleRights.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optModuleRights.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optModuleRights.Location = New System.Drawing.Point(12, 53)
        Me.optModuleRights.Name = "optModuleRights"
        Me.optModuleRights.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optModuleRights.Size = New System.Drawing.Size(134, 29)
        Me.optModuleRights.TabIndex = 13
        Me.optModuleRights.TabStop = True
        Me.optModuleRights.Text = "Module Rights"
        Me.optModuleRights.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.optModuleRights.UseVisualStyleBackColor = False
        '
        'fraView
        '
        Me.fraView.BackColor = System.Drawing.SystemColors.Control
        Me.fraView.Controls.Add(Me.txtEquivalentName)
        Me.fraView.Controls.Add(Me.cmdsearchEquivalent)
        Me.fraView.Controls.Add(Me.cmdsearch)
        Me.fraView.Controls.Add(Me.txteMailId)
        Me.fraView.Controls.Add(Me.Label10)
        Me.fraView.Controls.Add(Me.txtWindowUser)
        Me.fraView.Controls.Add(Me.Label8)
        Me.fraView.Controls.Add(Me.txtIPAddress)
        Me.fraView.Controls.Add(Me.Label5)
        Me.fraView.Controls.Add(Me.cboUserType)
        Me.fraView.Controls.Add(Me.Label3)
        Me.fraView.Controls.Add(Me.txtAdminPassword)
        Me.fraView.Controls.Add(Me.Label2)
        Me.fraView.Controls.Add(Me.cboLevel)
        Me.fraView.Controls.Add(Me.txtEmpCode)
        Me.fraView.Controls.Add(Me.txtEquivalent)
        Me.fraView.Controls.Add(Me.txtName)
        Me.fraView.Controls.Add(Me.txtpassword)
        Me.fraView.Controls.Add(Me.txtUserID)
        Me.fraView.Controls.Add(Me.LblLevel)
        Me.fraView.Controls.Add(Me.Label4)
        Me.fraView.Controls.Add(Me.Label7)
        Me.fraView.Controls.Add(Me.Label6)
        Me.fraView.Controls.Add(Me._Label2_1)
        Me.fraView.Controls.Add(Me.Label1)
        Me.fraView.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.fraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraView.Location = New System.Drawing.Point(0, 1)
        Me.fraView.Name = "fraView"
        Me.fraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraView.Size = New System.Drawing.Size(460, 262)
        Me.fraView.TabIndex = 38
        Me.fraView.TabStop = False
        '
        'txtEquivalentName
        '
        Me.txtEquivalentName.AcceptsReturn = True
        Me.txtEquivalentName.BackColor = System.Drawing.SystemColors.Window
        Me.txtEquivalentName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEquivalentName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEquivalentName.Enabled = False
        Me.txtEquivalentName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEquivalentName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtEquivalentName.Location = New System.Drawing.Point(266, 184)
        Me.txtEquivalentName.MaxLength = 0
        Me.txtEquivalentName.Name = "txtEquivalentName"
        Me.txtEquivalentName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEquivalentName.Size = New System.Drawing.Size(188, 22)
        Me.txtEquivalentName.TabIndex = 9
        '
        'cmdsearchEquivalent
        '
        Me.cmdsearchEquivalent.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchEquivalent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchEquivalent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchEquivalent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchEquivalent.Image = CType(resources.GetObject("cmdsearchEquivalent.Image"), System.Drawing.Image)
        Me.cmdsearchEquivalent.Location = New System.Drawing.Point(237, 184)
        Me.cmdsearchEquivalent.Name = "cmdsearchEquivalent"
        Me.cmdsearchEquivalent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchEquivalent.Size = New System.Drawing.Size(25, 22)
        Me.cmdsearchEquivalent.TabIndex = 50
        Me.cmdsearchEquivalent.TabStop = False
        Me.cmdsearchEquivalent.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdsearchEquivalent.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(411, 8)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(25, 22)
        Me.cmdsearch.TabIndex = 49
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'txteMailId
        '
        Me.txteMailId.AcceptsReturn = True
        Me.txteMailId.BackColor = System.Drawing.SystemColors.Window
        Me.txteMailId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteMailId.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteMailId.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteMailId.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txteMailId.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txteMailId.Location = New System.Drawing.Point(131, 159)
        Me.txteMailId.MaxLength = 0
        Me.txteMailId.Name = "txteMailId"
        Me.txteMailId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteMailId.Size = New System.Drawing.Size(323, 22)
        Me.txteMailId.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(11, 161)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(54, 13)
        Me.Label10.TabIndex = 48
        Me.Label10.Text = "eMail Id :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtWindowUser
        '
        Me.txtWindowUser.AcceptsReturn = True
        Me.txtWindowUser.BackColor = System.Drawing.SystemColors.Window
        Me.txtWindowUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWindowUser.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWindowUser.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWindowUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtWindowUser.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWindowUser.Location = New System.Drawing.Point(131, 236)
        Me.txtWindowUser.MaxLength = 0
        Me.txtWindowUser.Name = "txtWindowUser"
        Me.txtWindowUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWindowUser.Size = New System.Drawing.Size(323, 22)
        Me.txtWindowUser.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(11, 238)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(114, 13)
        Me.Label8.TabIndex = 46
        Me.Label8.Text = "Window User Name :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIPAddress
        '
        Me.txtIPAddress.AcceptsReturn = True
        Me.txtIPAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIPAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIPAddress.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIPAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtIPAddress.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtIPAddress.Location = New System.Drawing.Point(131, 210)
        Me.txtIPAddress.MaxLength = 0
        Me.txtIPAddress.Name = "txtIPAddress"
        Me.txtIPAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIPAddress.Size = New System.Drawing.Size(323, 22)
        Me.txtIPAddress.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(11, 212)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "IP Address :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboUserType
        '
        Me.cboUserType.BackColor = System.Drawing.SystemColors.Window
        Me.cboUserType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboUserType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUserType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUserType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboUserType.Location = New System.Drawing.Point(131, 134)
        Me.cboUserType.Name = "cboUserType"
        Me.cboUserType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboUserType.Size = New System.Drawing.Size(129, 21)
        Me.cboUserType.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(11, 138)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(99, 14)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "User Type :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAdminPassword
        '
        Me.txtAdminPassword.AcceptsReturn = True
        Me.txtAdminPassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdminPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdminPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdminPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdminPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtAdminPassword.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtAdminPassword.Location = New System.Drawing.Point(131, 109)
        Me.txtAdminPassword.MaxLength = 0
        Me.txtAdminPassword.Name = "txtAdminPassword"
        Me.txtAdminPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtAdminPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdminPassword.Size = New System.Drawing.Size(323, 22)
        Me.txtAdminPassword.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Menu
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(11, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(97, 13)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Admin Password :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboLevel
        '
        Me.cboLevel.BackColor = System.Drawing.SystemColors.Window
        Me.cboLevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLevel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLevel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboLevel.Location = New System.Drawing.Point(324, 134)
        Me.cboLevel.Name = "cboLevel"
        Me.cboLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLevel.Size = New System.Drawing.Size(129, 21)
        Me.cboLevel.TabIndex = 6
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtEmpCode.Location = New System.Drawing.Point(131, 34)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(323, 22)
        Me.txtEmpCode.TabIndex = 1
        '
        'txtEquivalent
        '
        Me.txtEquivalent.AcceptsReturn = True
        Me.txtEquivalent.BackColor = System.Drawing.SystemColors.Window
        Me.txtEquivalent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEquivalent.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEquivalent.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEquivalent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtEquivalent.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtEquivalent.Location = New System.Drawing.Point(131, 184)
        Me.txtEquivalent.MaxLength = 0
        Me.txtEquivalent.Name = "txtEquivalent"
        Me.txtEquivalent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEquivalent.Size = New System.Drawing.Size(105, 22)
        Me.txtEquivalent.TabIndex = 8
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(131, 59)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(323, 22)
        Me.txtName.TabIndex = 2
        '
        'txtpassword
        '
        Me.txtpassword.AcceptsReturn = True
        Me.txtpassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtpassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtpassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtpassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtpassword.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtpassword.Location = New System.Drawing.Point(131, 84)
        Me.txtpassword.MaxLength = 0
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtpassword.Size = New System.Drawing.Size(323, 22)
        Me.txtpassword.TabIndex = 3
        '
        'txtUserID
        '
        Me.txtUserID.AcceptsReturn = True
        Me.txtUserID.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtUserID.Location = New System.Drawing.Point(131, 10)
        Me.txtUserID.MaxLength = 0
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserID.Size = New System.Drawing.Size(279, 22)
        Me.txtUserID.TabIndex = 0
        '
        'LblLevel
        '
        Me.LblLevel.BackColor = System.Drawing.SystemColors.Control
        Me.LblLevel.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblLevel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblLevel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblLevel.Location = New System.Drawing.Point(279, 138)
        Me.LblLevel.Name = "LblLevel"
        Me.LblLevel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblLevel.Size = New System.Drawing.Size(41, 14)
        Me.LblLevel.TabIndex = 38
        Me.LblLevel.Text = "Level :"
        Me.LblLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Menu
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(11, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Employee Code :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(11, 186)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(81, 13)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "Equivalent To :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Menu
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(11, 61)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(43, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Name :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        '_Label2_1
        '
        Me._Label2_1.AutoSize = True
        Me._Label2_1.BackColor = System.Drawing.SystemColors.Menu
        Me._Label2_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label2_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label2_1.ForeColor = System.Drawing.Color.Black
        Me._Label2_1.Location = New System.Drawing.Point(11, 88)
        Me._Label2_1.Name = "_Label2_1"
        Me._Label2_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label2_1.Size = New System.Drawing.Size(61, 13)
        Me._Label2_1.TabIndex = 27
        Me._Label2_1.Text = "Password :"
        Me._Label2_1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Menu
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(11, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "User ID :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'sprdView
        '
        Me.sprdView.DataSource = Nothing
        Me.sprdView.Location = New System.Drawing.Point(3, 10)
        Me.sprdView.Name = "sprdView"
        Me.sprdView.OcxState = CType(resources.GetObject("sprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdView.Size = New System.Drawing.Size(623, 505)
        Me.sprdView.TabIndex = 0
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 508)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(630, 53)
        Me.FraMovement.TabIndex = 26
        Me.FraMovement.TabStop = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSavePrint.Location = New System.Drawing.Point(211, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(70, 38)
        Me.cmdSavePrint.TabIndex = 2
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.CmdPreview.Location = New System.Drawing.Point(418, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(70, 38)
        Me.CmdPreview.TabIndex = 5
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.cmdPrint.Location = New System.Drawing.Point(349, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(70, 38)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.CmdAdd.Location = New System.Drawing.Point(3, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(70, 38)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.CmdModify.Location = New System.Drawing.Point(72, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(70, 38)
        Me.CmdModify.TabIndex = 0
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.CmdSave.Location = New System.Drawing.Point(142, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(70, 38)
        Me.CmdSave.TabIndex = 1
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.CmdDelete.Location = New System.Drawing.Point(280, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(70, 38)
        Me.CmdDelete.TabIndex = 3
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.CmdView.Location = New System.Drawing.Point(487, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(70, 38)
        Me.CmdView.TabIndex = 6
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.CmdClose.Location = New System.Drawing.Point(556, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(70, 38)
        Me.CmdClose.TabIndex = 7
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'frmUsers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 562)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.FraMain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUsers"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "User Master"
        Me.FraMain.ResumeLayout(False)
        Me.fraShow.ResumeLayout(False)
        Me.fraShow.PerformLayout()
        Me.SSTab.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.fraView.ResumeLayout(False)
        Me.fraView.PerformLayout()
        CType(Me.sprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Friend WithEvents sprdView As AxFPSpreadADO.AxfpSpread
    Friend WithEvents fraShow As System.Windows.Forms.GroupBox
    Public WithEvents fraView As System.Windows.Forms.GroupBox
    Public WithEvents cboLevel As System.Windows.Forms.ComboBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtEquivalent As System.Windows.Forms.TextBox
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents txtpassword As System.Windows.Forms.TextBox
    Public WithEvents txtUserID As System.Windows.Forms.TextBox
    Public WithEvents LblLevel As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents _Label2_1 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents optMenuRights As System.Windows.Forms.RadioButton
    Public WithEvents txtAdminPassword As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents cboUserType As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents optBranchRights As System.Windows.Forms.RadioButton
    Public WithEvents optModuleRights As System.Windows.Forms.RadioButton
    Public WithEvents optDeptRights As System.Windows.Forms.RadioButton
    Public WithEvents txtIPAddress As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents OptStatOpen As System.Windows.Forms.RadioButton
    Public WithEvents OptStatClosed As System.Windows.Forms.RadioButton
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents chkRunDate As System.Windows.Forms.CheckBox
    Public WithEvents txtWindowUser As System.Windows.Forms.TextBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txteMailId As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents SSTab As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Public WithEvents chkAllow_ExcessIssue As CheckBox
    Public WithEvents chkInv_Level_AppUser As CheckBox
    Public WithEvents chkInv_LevelUser As CheckBox
    Public WithEvents chkPay_CorpUser As CheckBox
    Public WithEvents chkAllow_StockAdj As CheckBox
    Public WithEvents chkAllow_PoprintApp As CheckBox
    Public WithEvents chkAllow_RmPo As CheckBox
    Public WithEvents chkAllow_BopPo As CheckBox
    Public WithEvents chkAllow_AccountMaster As CheckBox
    Public WithEvents chkBookLocking As CheckBox
    Public WithEvents chkInvoiceAdmin As CheckBox
    Public WithEvents chkDS As CheckBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents chkDigitalSign As CheckBox
    Public WithEvents cmdsearch As Button
    Public WithEvents txtEquivalentName As TextBox
    Public WithEvents cmdsearchEquivalent As Button
    Public WithEvents txtDSCertificateNo As TextBox
    Public WithEvents Label22 As Label
    Public WithEvents txtDigitalSignUID As TextBox
    Public WithEvents txtDigitalSignPassword As TextBox
    Public WithEvents Label9 As Label
    Public WithEvents Label11 As Label
    Public WithEvents txtDLLFileName As TextBox
    Public WithEvents Label13 As Label
    Public WithEvents txtDLLPathName As TextBox
    Public WithEvents Label12 As Label
End Class
