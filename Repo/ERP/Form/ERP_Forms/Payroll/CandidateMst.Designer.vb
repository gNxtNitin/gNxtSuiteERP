Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCandidateMst
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
    Public WithEvents cboCorporate As System.Windows.Forms.ComboBox
    Public WithEvents txtBloodGroup As System.Windows.Forms.TextBox
    Public WithEvents cboCatgeory As System.Windows.Forms.ComboBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtDOI As System.Windows.Forms.TextBox
    Public WithEvents chkIsJoined As System.Windows.Forms.CheckBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtDOJ As System.Windows.Forms.TextBox
    Public WithEvents cboMStatus As System.Windows.Forms.ComboBox
    Public WithEvents cboSex As System.Windows.Forms.ComboBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents cmdSelectPhoto As System.Windows.Forms.Button
    Public WithEvents ImagePhoto As System.Windows.Forms.PictureBox
    Public WithEvents PbPhoto As System.Windows.Forms.Panel
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents txtFName As System.Windows.Forms.TextBox
    Public WithEvents txtDOB As System.Windows.Forms.TextBox
    Public cdgPhotoOpen As System.Windows.Forms.OpenFileDialog
    Public WithEvents Label67 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label54 As System.Windows.Forms.Label
    Public WithEvents Label66 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblPhotoFileName As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents fraTop As System.Windows.Forms.GroupBox
    Public WithEvents txtCTC As System.Windows.Forms.TextBox
    Public WithEvents txtNetSalary As System.Windows.Forms.TextBox
    Public WithEvents txtDeduction As System.Windows.Forms.TextBox
    Public WithEvents txtGSalary As System.Windows.Forms.TextBox
    Public WithEvents txtBSalary As System.Windows.Forms.TextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents chkHRApp As System.Windows.Forms.CheckBox
    Public WithEvents chkCEOApp As System.Windows.Forms.CheckBox
    Public WithEvents chkCFOApp As System.Windows.Forms.CheckBox
    Public WithEvents chkMDApp As System.Windows.Forms.CheckBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents CboJoinDesignation As System.Windows.Forms.ComboBox
    Public WithEvents txtQualification As System.Windows.Forms.TextBox
    Public WithEvents txtCostCenter As System.Windows.Forms.TextBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents txtExperience As System.Windows.Forms.TextBox
    Public WithEvents txtLastCompany As System.Windows.Forms.TextBox
    Public WithEvents cbodesignation As System.Windows.Forms.ComboBox
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label57 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents fraModePayment As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents txtMobileOff As System.Windows.Forms.TextBox
    Public WithEvents txtOffeMail As System.Windows.Forms.TextBox
    Public WithEvents chkMetroCity As System.Windows.Forms.CheckBox
    Public WithEvents txtPhone As System.Windows.Forms.TextBox
    Public WithEvents txtEmail As System.Windows.Forms.TextBox
    Public WithEvents txtSpouse As System.Windows.Forms.TextBox
    Public WithEvents txtPinCode As System.Windows.Forms.TextBox
    Public WithEvents txtCity As System.Windows.Forms.TextBox
    Public WithEvents txtAddress As System.Windows.Forms.TextBox
    Public WithEvents txtState As System.Windows.Forms.TextBox
    Public WithEvents Label77 As System.Windows.Forms.Label
    Public WithEvents Label64 As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents fraPermAdd As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents txtDOM As System.Windows.Forms.TextBox
    Public WithEvents txtDOBActual As System.Windows.Forms.TextBox
    Public WithEvents Label79 As System.Windows.Forms.Label
    Public WithEvents Label80 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents chkLEApp As System.Windows.Forms.CheckBox
    Public WithEvents chkBonusApp As System.Windows.Forms.CheckBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents txtPFNo As System.Windows.Forms.TextBox
    Public WithEvents lblPFNo As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboESIApp As System.Windows.Forms.ComboBox
    Public WithEvents txtESINo As System.Windows.Forms.TextBox
    Public WithEvents txtDispensary As System.Windows.Forms.TextBox
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents lblEsiNo As System.Windows.Forms.Label
    Public WithEvents lblDispensary As System.Windows.Forms.Label
    Public WithEvents ESI As System.Windows.Forms.GroupBox
    Public WithEvents txtAdhaarNo As System.Windows.Forms.TextBox
    Public WithEvents txtPanNo As System.Windows.Forms.TextBox
    Public WithEvents Label76 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents txtLICID As System.Windows.Forms.TextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents sprdDeduct As AxFPSpreadADO.AxfpSpread
    Public WithEvents sprdEarn As AxFPSpreadADO.AxfpSpread
    Public WithEvents grdDeductions As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents sprdPerks As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label58 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents sprdSpouse As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents Label59 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents FraBottom As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdeMail As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label44 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCandidateMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdeMail = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraTop = New System.Windows.Forms.GroupBox()
        Me.cboCorporate = New System.Windows.Forms.ComboBox()
        Me.txtBloodGroup = New System.Windows.Forms.TextBox()
        Me.cboCatgeory = New System.Windows.Forms.ComboBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtDOI = New System.Windows.Forms.TextBox()
        Me.chkIsJoined = New System.Windows.Forms.CheckBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtDOJ = New System.Windows.Forms.TextBox()
        Me.cboMStatus = New System.Windows.Forms.ComboBox()
        Me.cboSex = New System.Windows.Forms.ComboBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.cmdSelectPhoto = New System.Windows.Forms.Button()
        Me.PbPhoto = New System.Windows.Forms.Panel()
        Me.ImagePhoto = New System.Windows.Forms.PictureBox()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.txtFName = New System.Windows.Forms.TextBox()
        Me.txtDOB = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblPhotoFileName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cdgPhotoOpen = New System.Windows.Forms.OpenFileDialog()
        Me.FraBottom = New System.Windows.Forms.GroupBox()
        Me.txtForm1CTC = New System.Windows.Forms.TextBox()
        Me.txtForm1NetSalary = New System.Windows.Forms.TextBox()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.txtForm1GSalary = New System.Windows.Forms.TextBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.txtForm1BSalary = New System.Windows.Forms.TextBox()
        Me.txtCTC = New System.Windows.Forms.TextBox()
        Me.txtNetSalary = New System.Windows.Forms.TextBox()
        Me.txtDeduction = New System.Windows.Forms.TextBox()
        Me.txtGSalary = New System.Windows.Forms.TextBox()
        Me.txtBSalary = New System.Windows.Forms.TextBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.fraModePayment = New System.Windows.Forms.GroupBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkHRApp = New System.Windows.Forms.CheckBox()
        Me.chkCEOApp = New System.Windows.Forms.CheckBox()
        Me.chkCFOApp = New System.Windows.Forms.CheckBox()
        Me.chkMDApp = New System.Windows.Forms.CheckBox()
        Me.CboJoinDesignation = New System.Windows.Forms.ComboBox()
        Me.txtQualification = New System.Windows.Forms.TextBox()
        Me.txtCostCenter = New System.Windows.Forms.TextBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.txtExperience = New System.Windows.Forms.TextBox()
        Me.txtLastCompany = New System.Windows.Forms.TextBox()
        Me.cbodesignation = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.fraPermAdd = New System.Windows.Forms.GroupBox()
        Me.txtMobileOff = New System.Windows.Forms.TextBox()
        Me.txtOffeMail = New System.Windows.Forms.TextBox()
        Me.chkMetroCity = New System.Windows.Forms.CheckBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtSpouse = New System.Windows.Forms.TextBox()
        Me.txtPinCode = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtDOM = New System.Windows.Forms.TextBox()
        Me.txtDOBActual = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.chkLEApp = New System.Windows.Forms.CheckBox()
        Me.chkBonusApp = New System.Windows.Forms.CheckBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtPFNo = New System.Windows.Forms.TextBox()
        Me.lblPFNo = New System.Windows.Forms.Label()
        Me.ESI = New System.Windows.Forms.GroupBox()
        Me.cboESIApp = New System.Windows.Forms.ComboBox()
        Me.txtESINo = New System.Windows.Forms.TextBox()
        Me.txtDispensary = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.lblEsiNo = New System.Windows.Forms.Label()
        Me.lblDispensary = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtAdhaarNo = New System.Windows.Forms.TextBox()
        Me.txtPanNo = New System.Windows.Forms.TextBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtLICID = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.sprdDeduct = New AxFPSpreadADO.AxfpSpread()
        Me.sprdEarn = New AxFPSpreadADO.AxfpSpread()
        Me.grdDeductions = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me.sprdPerks = New AxFPSpreadADO.AxfpSpread()
        Me.Label58 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage()
        Me.sprdSpouse = New AxFPSpreadADO.AxfpSpread()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.fraTop.SuspendLayout()
        Me.PbPhoto.SuspendLayout()
        CType(Me.ImagePhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraBottom.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.fraModePayment.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.fraPermAdd.SuspendLayout()
        Me._SSTab1_TabPage2.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.ESI.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me._SSTab1_TabPage3.SuspendLayout()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage4.SuspendLayout()
        CType(Me.sprdPerks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage5.SuspendLayout()
        CType(Me.sprdSpouse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(176, 12)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(711, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 49
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(645, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 48
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdeMail
        '
        Me.cmdeMail.BackColor = System.Drawing.SystemColors.Control
        Me.cmdeMail.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdeMail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdeMail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdeMail.Image = CType(resources.GetObject("cmdeMail.Image"), System.Drawing.Image)
        Me.cmdeMail.Location = New System.Drawing.Point(579, 10)
        Me.cmdeMail.Name = "cmdeMail"
        Me.cmdeMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdeMail.Size = New System.Drawing.Size(67, 37)
        Me.cmdeMail.TabIndex = 135
        Me.cmdeMail.Text = "eMail for Approval"
        Me.cmdeMail.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdeMail, "View List")
        Me.cmdeMail.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(445, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 46
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(241, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 43
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(377, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 45
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(175, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 42
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(109, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 77
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop
        '
        Me.fraTop.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop.Controls.Add(Me.cboCorporate)
        Me.fraTop.Controls.Add(Me.txtBloodGroup)
        Me.fraTop.Controls.Add(Me.cboCatgeory)
        Me.fraTop.Controls.Add(Me.cboDivision)
        Me.fraTop.Controls.Add(Me.txtDOI)
        Me.fraTop.Controls.Add(Me.chkIsJoined)
        Me.fraTop.Controls.Add(Me.txtEmpCode)
        Me.fraTop.Controls.Add(Me.txtDOJ)
        Me.fraTop.Controls.Add(Me.cboMStatus)
        Me.fraTop.Controls.Add(Me.cboSex)
        Me.fraTop.Controls.Add(Me.TxtName)
        Me.fraTop.Controls.Add(Me.cmdSearch)
        Me.fraTop.Controls.Add(Me.cmdSelectPhoto)
        Me.fraTop.Controls.Add(Me.PbPhoto)
        Me.fraTop.Controls.Add(Me.txtRefNo)
        Me.fraTop.Controls.Add(Me.txtFName)
        Me.fraTop.Controls.Add(Me.txtDOB)
        Me.fraTop.Controls.Add(Me.Label67)
        Me.fraTop.Controls.Add(Me.Label16)
        Me.fraTop.Controls.Add(Me.Label54)
        Me.fraTop.Controls.Add(Me.Label66)
        Me.fraTop.Controls.Add(Me.Label14)
        Me.fraTop.Controls.Add(Me.Label6)
        Me.fraTop.Controls.Add(Me.Label10)
        Me.fraTop.Controls.Add(Me.Label23)
        Me.fraTop.Controls.Add(Me.Label13)
        Me.fraTop.Controls.Add(Me.lblPhotoFileName)
        Me.fraTop.Controls.Add(Me.Label1)
        Me.fraTop.Controls.Add(Me.Label2)
        Me.fraTop.Controls.Add(Me.Label3)
        Me.fraTop.Controls.Add(Me.Label12)
        Me.fraTop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop.Location = New System.Drawing.Point(0, -6)
        Me.fraTop.Name = "fraTop"
        Me.fraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop.Size = New System.Drawing.Size(912, 176)
        Me.fraTop.TabIndex = 0
        Me.fraTop.TabStop = False
        '
        'cboCorporate
        '
        Me.cboCorporate.BackColor = System.Drawing.SystemColors.Window
        Me.cboCorporate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCorporate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCorporate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCorporate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCorporate.Location = New System.Drawing.Point(428, 112)
        Me.cboCorporate.Name = "cboCorporate"
        Me.cboCorporate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCorporate.Size = New System.Drawing.Size(65, 22)
        Me.cboCorporate.TabIndex = 8
        '
        'txtBloodGroup
        '
        Me.txtBloodGroup.AcceptsReturn = True
        Me.txtBloodGroup.BackColor = System.Drawing.SystemColors.Window
        Me.txtBloodGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBloodGroup.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBloodGroup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBloodGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtBloodGroup.Location = New System.Drawing.Point(580, 78)
        Me.txtBloodGroup.MaxLength = 10
        Me.txtBloodGroup.Name = "txtBloodGroup"
        Me.txtBloodGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBloodGroup.Size = New System.Drawing.Size(55, 20)
        Me.txtBloodGroup.TabIndex = 5
        '
        'cboCatgeory
        '
        Me.cboCatgeory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCatgeory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCatgeory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCatgeory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCatgeory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCatgeory.Location = New System.Drawing.Point(267, 112)
        Me.cboCatgeory.Name = "cboCatgeory"
        Me.cboCatgeory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCatgeory.Size = New System.Drawing.Size(89, 22)
        Me.cboCatgeory.TabIndex = 7
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(550, 145)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(87, 22)
        Me.cboDivision.TabIndex = 13
        '
        'txtDOI
        '
        Me.txtDOI.AcceptsReturn = True
        Me.txtDOI.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOI.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOI.Location = New System.Drawing.Point(282, 147)
        Me.txtDOI.MaxLength = 10
        Me.txtDOI.Name = "txtDOI"
        Me.txtDOI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOI.Size = New System.Drawing.Size(73, 20)
        Me.txtDOI.TabIndex = 11
        '
        'chkIsJoined
        '
        Me.chkIsJoined.AutoSize = True
        Me.chkIsJoined.BackColor = System.Drawing.SystemColors.Control
        Me.chkIsJoined.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkIsJoined.Enabled = False
        Me.chkIsJoined.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsJoined.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkIsJoined.Location = New System.Drawing.Point(496, 14)
        Me.chkIsJoined.Name = "chkIsJoined"
        Me.chkIsJoined.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkIsJoined.Size = New System.Drawing.Size(68, 18)
        Me.chkIsJoined.TabIndex = 118
        Me.chkIsJoined.Text = "Is Joined"
        Me.chkIsJoined.UseVisualStyleBackColor = False
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Enabled = False
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(361, 12)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(67, 20)
        Me.txtEmpCode.TabIndex = 116
        '
        'txtDOJ
        '
        Me.txtDOJ.AcceptsReturn = True
        Me.txtDOJ.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOJ.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOJ.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOJ.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOJ.Location = New System.Drawing.Point(106, 147)
        Me.txtDOJ.MaxLength = 10
        Me.txtDOJ.Name = "txtDOJ"
        Me.txtDOJ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOJ.Size = New System.Drawing.Size(75, 20)
        Me.txtDOJ.TabIndex = 10
        '
        'cboMStatus
        '
        Me.cboMStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboMStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMStatus.Location = New System.Drawing.Point(428, 147)
        Me.cboMStatus.Name = "cboMStatus"
        Me.cboMStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMStatus.Size = New System.Drawing.Size(65, 22)
        Me.cboMStatus.TabIndex = 12
        '
        'cboSex
        '
        Me.cboSex.BackColor = System.Drawing.SystemColors.Window
        Me.cboSex.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSex.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSex.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSex.Location = New System.Drawing.Point(108, 112)
        Me.cboSex.Name = "cboSex"
        Me.cboSex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSex.Size = New System.Drawing.Size(75, 22)
        Me.cboSex.TabIndex = 6
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(108, 45)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(381, 20)
        Me.TxtName.TabIndex = 3
        '
        'cmdSelectPhoto
        '
        Me.cmdSelectPhoto.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSelectPhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSelectPhoto.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSelectPhoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSelectPhoto.Location = New System.Drawing.Point(760, 142)
        Me.cmdSelectPhoto.Name = "cmdSelectPhoto"
        Me.cmdSelectPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSelectPhoto.Size = New System.Drawing.Size(91, 25)
        Me.cmdSelectPhoto.TabIndex = 58
        Me.cmdSelectPhoto.Text = "Select Photo"
        Me.cmdSelectPhoto.UseVisualStyleBackColor = False
        '
        'PbPhoto
        '
        Me.PbPhoto.BackColor = System.Drawing.SystemColors.Control
        Me.PbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PbPhoto.Controls.Add(Me.ImagePhoto)
        Me.PbPhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.PbPhoto.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PbPhoto.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PbPhoto.Location = New System.Drawing.Point(716, 14)
        Me.PbPhoto.Name = "PbPhoto"
        Me.PbPhoto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PbPhoto.Size = New System.Drawing.Size(176, 122)
        Me.PbPhoto.TabIndex = 54
        Me.PbPhoto.TabStop = True
        '
        'ImagePhoto
        '
        Me.ImagePhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ImagePhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.ImagePhoto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ImagePhoto.Location = New System.Drawing.Point(0, 0)
        Me.ImagePhoto.Name = "ImagePhoto"
        Me.ImagePhoto.Size = New System.Drawing.Size(172, 118)
        Me.ImagePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImagePhoto.TabIndex = 0
        Me.ImagePhoto.TabStop = False
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefNo.Location = New System.Drawing.Point(108, 12)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(67, 20)
        Me.txtRefNo.TabIndex = 1
        '
        'txtFName
        '
        Me.txtFName.AcceptsReturn = True
        Me.txtFName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtFName.Location = New System.Drawing.Point(108, 78)
        Me.txtFName.MaxLength = 25
        Me.txtFName.Name = "txtFName"
        Me.txtFName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFName.Size = New System.Drawing.Size(381, 20)
        Me.txtFName.TabIndex = 4
        '
        'txtDOB
        '
        Me.txtDOB.AcceptsReturn = True
        Me.txtDOB.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOB.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOB.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOB.Location = New System.Drawing.Point(550, 112)
        Me.txtDOB.MaxLength = 10
        Me.txtDOB.Name = "txtDOB"
        Me.txtDOB.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOB.Size = New System.Drawing.Size(87, 20)
        Me.txtDOB.TabIndex = 9
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.BackColor = System.Drawing.SystemColors.Control
        Me.Label67.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label67.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label67.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label67.Location = New System.Drawing.Point(362, 116)
        Me.Label67.Name = "Label67"
        Me.Label67.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label67.Size = New System.Drawing.Size(61, 14)
        Me.Label67.TabIndex = 124
        Me.Label67.Text = "Corporate :"
        Me.Label67.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label16.Location = New System.Drawing.Point(498, 80)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(73, 14)
        Me.Label16.TabIndex = 123
        Me.Label16.Text = "Blood Group :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.Color.Transparent
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label54.Location = New System.Drawing.Point(208, 114)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(57, 14)
        Me.Label54.TabIndex = 121
        Me.Label54.Text = "Category :"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.BackColor = System.Drawing.SystemColors.Control
        Me.Label66.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label66.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label66.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label66.Location = New System.Drawing.Point(494, 149)
        Me.Label66.Name = "Label66"
        Me.Label66.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label66.Size = New System.Drawing.Size(50, 14)
        Me.Label66.TabIndex = 120
        Me.Label66.Text = "Division :"
        Me.Label66.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(186, 149)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(95, 13)
        Me.Label14.TabIndex = 119
        Me.Label14.Text = "Interview Date :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label6.Location = New System.Drawing.Point(289, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(64, 14)
        Me.Label6.TabIndex = 117
        Me.Label6.Text = "Emp Code. :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(-25, 149)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(127, 13)
        Me.Label10.TabIndex = 111
        Me.Label10.Text = "Date of Joining :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label23.Location = New System.Drawing.Point(70, 114)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(32, 14)
        Me.Label23.TabIndex = 56
        Me.Label23.Text = "Sex :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label13.Location = New System.Drawing.Point(358, 149)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(58, 14)
        Me.Label13.TabIndex = 55
        Me.Label13.Text = "M. Status :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPhotoFileName
        '
        Me.lblPhotoFileName.AutoSize = True
        Me.lblPhotoFileName.BackColor = System.Drawing.SystemColors.Control
        Me.lblPhotoFileName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPhotoFileName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPhotoFileName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPhotoFileName.Location = New System.Drawing.Point(612, 14)
        Me.lblPhotoFileName.Name = "lblPhotoFileName"
        Me.lblPhotoFileName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPhotoFileName.Size = New System.Drawing.Size(77, 14)
        Me.lblPhotoFileName.TabIndex = 57
        Me.lblPhotoFileName.Text = "PhotoFileName"
        Me.lblPhotoFileName.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(62, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 14)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label2.Location = New System.Drawing.Point(20, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(82, 14)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Father's Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(509, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(35, 14)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "DOB :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label12.Location = New System.Drawing.Point(53, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(49, 14)
        Me.Label12.TabIndex = 51
        Me.Label12.Text = "Ref No. :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraBottom
        '
        Me.FraBottom.BackColor = System.Drawing.SystemColors.Control
        Me.FraBottom.Controls.Add(Me.txtForm1CTC)
        Me.FraBottom.Controls.Add(Me.txtForm1NetSalary)
        Me.FraBottom.Controls.Add(Me.Label83)
        Me.FraBottom.Controls.Add(Me.Label84)
        Me.FraBottom.Controls.Add(Me.txtForm1GSalary)
        Me.FraBottom.Controls.Add(Me.Label82)
        Me.FraBottom.Controls.Add(Me.Label81)
        Me.FraBottom.Controls.Add(Me.txtForm1BSalary)
        Me.FraBottom.Controls.Add(Me.txtCTC)
        Me.FraBottom.Controls.Add(Me.txtNetSalary)
        Me.FraBottom.Controls.Add(Me.txtDeduction)
        Me.FraBottom.Controls.Add(Me.txtGSalary)
        Me.FraBottom.Controls.Add(Me.txtBSalary)
        Me.FraBottom.Controls.Add(Me.SSTab1)
        Me.FraBottom.Controls.Add(Me.Label59)
        Me.FraBottom.Controls.Add(Me.Label43)
        Me.FraBottom.Controls.Add(Me.Label41)
        Me.FraBottom.Controls.Add(Me.Label15)
        Me.FraBottom.Controls.Add(Me.Label7)
        Me.FraBottom.Controls.Add(Me.Label30)
        Me.FraBottom.Controls.Add(Me.Label31)
        Me.FraBottom.Controls.Add(Me.Label32)
        Me.FraBottom.Controls.Add(Me.Label33)
        Me.FraBottom.Controls.Add(Me.Label34)
        Me.FraBottom.Controls.Add(Me.Label35)
        Me.FraBottom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraBottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBottom.Location = New System.Drawing.Point(0, 168)
        Me.FraBottom.Name = "FraBottom"
        Me.FraBottom.Padding = New System.Windows.Forms.Padding(0)
        Me.FraBottom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraBottom.Size = New System.Drawing.Size(910, 406)
        Me.FraBottom.TabIndex = 60
        Me.FraBottom.TabStop = False
        '
        'txtForm1CTC
        '
        Me.txtForm1CTC.AcceptsReturn = True
        Me.txtForm1CTC.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1CTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1CTC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1CTC.Enabled = False
        Me.txtForm1CTC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1CTC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1CTC.Location = New System.Drawing.Point(710, 378)
        Me.txtForm1CTC.MaxLength = 0
        Me.txtForm1CTC.Name = "txtForm1CTC"
        Me.txtForm1CTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1CTC.Size = New System.Drawing.Size(90, 20)
        Me.txtForm1CTC.TabIndex = 180
        Me.txtForm1CTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtForm1NetSalary
        '
        Me.txtForm1NetSalary.AcceptsReturn = True
        Me.txtForm1NetSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1NetSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1NetSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1NetSalary.Enabled = False
        Me.txtForm1NetSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1NetSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1NetSalary.Location = New System.Drawing.Point(532, 378)
        Me.txtForm1NetSalary.MaxLength = 0
        Me.txtForm1NetSalary.Name = "txtForm1NetSalary"
        Me.txtForm1NetSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1NetSalary.Size = New System.Drawing.Size(80, 20)
        Me.txtForm1NetSalary.TabIndex = 178
        Me.txtForm1NetSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.BackColor = System.Drawing.SystemColors.Control
        Me.Label83.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label83.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label83.Location = New System.Drawing.Point(644, 381)
        Me.Label83.Name = "Label83"
        Me.Label83.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label83.Size = New System.Drawing.Size(62, 14)
        Me.Label83.TabIndex = 181
        Me.Label83.Text = "Pay C.T.C. :"
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.BackColor = System.Drawing.SystemColors.Control
        Me.Label84.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label84.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label84.Location = New System.Drawing.Point(443, 381)
        Me.Label84.Name = "Label84"
        Me.Label84.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label84.Size = New System.Drawing.Size(84, 14)
        Me.Label84.TabIndex = 179
        Me.Label84.Text = "Pay Net Salary :"
        '
        'txtForm1GSalary
        '
        Me.txtForm1GSalary.AcceptsReturn = True
        Me.txtForm1GSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1GSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1GSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1GSalary.Enabled = False
        Me.txtForm1GSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1GSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1GSalary.Location = New System.Drawing.Point(123, 378)
        Me.txtForm1GSalary.MaxLength = 0
        Me.txtForm1GSalary.Name = "txtForm1GSalary"
        Me.txtForm1GSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1GSalary.Size = New System.Drawing.Size(90, 20)
        Me.txtForm1GSalary.TabIndex = 176
        Me.txtForm1GSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.BackColor = System.Drawing.SystemColors.Control
        Me.Label82.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label82.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label82.Location = New System.Drawing.Point(21, 382)
        Me.Label82.Name = "Label82"
        Me.Label82.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label82.Size = New System.Drawing.Size(98, 14)
        Me.Label82.TabIndex = 177
        Me.Label82.Text = "Pay Gross Salary :"
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.BackColor = System.Drawing.SystemColors.Control
        Me.Label81.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label81.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label81.Location = New System.Drawing.Point(329, 14)
        Me.Label81.Name = "Label81"
        Me.Label81.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label81.Size = New System.Drawing.Size(95, 14)
        Me.Label81.TabIndex = 171
        Me.Label81.Text = "Pay Basic Salary :"
        '
        'txtForm1BSalary
        '
        Me.txtForm1BSalary.AcceptsReturn = True
        Me.txtForm1BSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1BSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1BSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1BSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1BSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1BSalary.Location = New System.Drawing.Point(428, 10)
        Me.txtForm1BSalary.MaxLength = 0
        Me.txtForm1BSalary.Name = "txtForm1BSalary"
        Me.txtForm1BSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1BSalary.Size = New System.Drawing.Size(112, 20)
        Me.txtForm1BSalary.TabIndex = 170
        Me.txtForm1BSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCTC
        '
        Me.txtCTC.AcceptsReturn = True
        Me.txtCTC.BackColor = System.Drawing.SystemColors.Window
        Me.txtCTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCTC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCTC.Enabled = False
        Me.txtCTC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCTC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCTC.Location = New System.Drawing.Point(710, 350)
        Me.txtCTC.MaxLength = 0
        Me.txtCTC.Name = "txtCTC"
        Me.txtCTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCTC.Size = New System.Drawing.Size(90, 20)
        Me.txtCTC.TabIndex = 102
        Me.txtCTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNetSalary
        '
        Me.txtNetSalary.AcceptsReturn = True
        Me.txtNetSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetSalary.Enabled = False
        Me.txtNetSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNetSalary.Location = New System.Drawing.Point(532, 350)
        Me.txtNetSalary.MaxLength = 0
        Me.txtNetSalary.Name = "txtNetSalary"
        Me.txtNetSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetSalary.Size = New System.Drawing.Size(80, 20)
        Me.txtNetSalary.TabIndex = 41
        Me.txtNetSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDeduction
        '
        Me.txtDeduction.AcceptsReturn = True
        Me.txtDeduction.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeduction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeduction.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeduction.Enabled = False
        Me.txtDeduction.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeduction.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeduction.Location = New System.Drawing.Point(320, 350)
        Me.txtDeduction.MaxLength = 0
        Me.txtDeduction.Name = "txtDeduction"
        Me.txtDeduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeduction.Size = New System.Drawing.Size(90, 20)
        Me.txtDeduction.TabIndex = 40
        Me.txtDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtGSalary
        '
        Me.txtGSalary.AcceptsReturn = True
        Me.txtGSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSalary.Enabled = False
        Me.txtGSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGSalary.Location = New System.Drawing.Point(123, 350)
        Me.txtGSalary.MaxLength = 0
        Me.txtGSalary.Name = "txtGSalary"
        Me.txtGSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSalary.Size = New System.Drawing.Size(90, 20)
        Me.txtGSalary.TabIndex = 39
        Me.txtGSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBSalary
        '
        Me.txtBSalary.AcceptsReturn = True
        Me.txtBSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtBSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBSalary.Location = New System.Drawing.Point(148, 10)
        Me.txtBSalary.MaxLength = 0
        Me.txtBSalary.Name = "txtBSalary"
        Me.txtBSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBSalary.Size = New System.Drawing.Size(112, 20)
        Me.txtBSalary.TabIndex = 14
        Me.txtBSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage5)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 21)
        Me.SSTab1.Location = New System.Drawing.Point(2, 32)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 5
        Me.SSTab1.Size = New System.Drawing.Size(908, 316)
        Me.SSTab1.TabIndex = 62
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.fraModePayment)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(900, 287)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Company"
        '
        'fraModePayment
        '
        Me.fraModePayment.BackColor = System.Drawing.SystemColors.Control
        Me.fraModePayment.Controls.Add(Me.Frame5)
        Me.fraModePayment.Controls.Add(Me.CboJoinDesignation)
        Me.fraModePayment.Controls.Add(Me.txtQualification)
        Me.fraModePayment.Controls.Add(Me.txtCostCenter)
        Me.fraModePayment.Controls.Add(Me.cboDept)
        Me.fraModePayment.Controls.Add(Me.txtExperience)
        Me.fraModePayment.Controls.Add(Me.txtLastCompany)
        Me.fraModePayment.Controls.Add(Me.cbodesignation)
        Me.fraModePayment.Controls.Add(Me.Label20)
        Me.fraModePayment.Controls.Add(Me.Label29)
        Me.fraModePayment.Controls.Add(Me.Label57)
        Me.fraModePayment.Controls.Add(Me.Label4)
        Me.fraModePayment.Controls.Add(Me.Label40)
        Me.fraModePayment.Controls.Add(Me.Label37)
        Me.fraModePayment.Controls.Add(Me.Label5)
        Me.fraModePayment.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraModePayment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraModePayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.fraModePayment.Location = New System.Drawing.Point(0, 0)
        Me.fraModePayment.Name = "fraModePayment"
        Me.fraModePayment.Padding = New System.Windows.Forms.Padding(0)
        Me.fraModePayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraModePayment.Size = New System.Drawing.Size(900, 287)
        Me.fraModePayment.TabIndex = 68
        Me.fraModePayment.TabStop = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.chkHRApp)
        Me.Frame5.Controls.Add(Me.chkCEOApp)
        Me.Frame5.Controls.Add(Me.chkCFOApp)
        Me.Frame5.Controls.Add(Me.chkMDApp)
        Me.Frame5.Enabled = False
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(612, 6)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(121, 112)
        Me.Frame5.TabIndex = 130
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Approval"
        '
        'chkHRApp
        '
        Me.chkHRApp.AutoSize = True
        Me.chkHRApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkHRApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHRApp.Enabled = False
        Me.chkHRApp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHRApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHRApp.Location = New System.Drawing.Point(6, 87)
        Me.chkHRApp.Name = "chkHRApp"
        Me.chkHRApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHRApp.Size = New System.Drawing.Size(86, 18)
        Me.chkHRApp.TabIndex = 134
        Me.chkHRApp.Text = "HR Approval"
        Me.chkHRApp.UseVisualStyleBackColor = False
        '
        'chkCEOApp
        '
        Me.chkCEOApp.AutoSize = True
        Me.chkCEOApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkCEOApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCEOApp.Enabled = False
        Me.chkCEOApp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCEOApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCEOApp.Location = New System.Drawing.Point(6, 64)
        Me.chkCEOApp.Name = "chkCEOApp"
        Me.chkCEOApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCEOApp.Size = New System.Drawing.Size(93, 18)
        Me.chkCEOApp.TabIndex = 133
        Me.chkCEOApp.Text = "CEO Approval"
        Me.chkCEOApp.UseVisualStyleBackColor = False
        '
        'chkCFOApp
        '
        Me.chkCFOApp.AutoSize = True
        Me.chkCFOApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkCFOApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCFOApp.Enabled = False
        Me.chkCFOApp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCFOApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCFOApp.Location = New System.Drawing.Point(6, 41)
        Me.chkCFOApp.Name = "chkCFOApp"
        Me.chkCFOApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCFOApp.Size = New System.Drawing.Size(93, 18)
        Me.chkCFOApp.TabIndex = 132
        Me.chkCFOApp.Text = "CFO Approval"
        Me.chkCFOApp.UseVisualStyleBackColor = False
        '
        'chkMDApp
        '
        Me.chkMDApp.AutoSize = True
        Me.chkMDApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkMDApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMDApp.Enabled = False
        Me.chkMDApp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMDApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMDApp.Location = New System.Drawing.Point(6, 18)
        Me.chkMDApp.Name = "chkMDApp"
        Me.chkMDApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMDApp.Size = New System.Drawing.Size(87, 18)
        Me.chkMDApp.TabIndex = 131
        Me.chkMDApp.Text = "MD Approval"
        Me.chkMDApp.UseVisualStyleBackColor = False
        '
        'CboJoinDesignation
        '
        Me.CboJoinDesignation.BackColor = System.Drawing.SystemColors.Window
        Me.CboJoinDesignation.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboJoinDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboJoinDesignation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboJoinDesignation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.CboJoinDesignation.Location = New System.Drawing.Point(254, 162)
        Me.CboJoinDesignation.Name = "CboJoinDesignation"
        Me.CboJoinDesignation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboJoinDesignation.Size = New System.Drawing.Size(247, 22)
        Me.CboJoinDesignation.Sorted = True
        Me.CboJoinDesignation.TabIndex = 113
        '
        'txtQualification
        '
        Me.txtQualification.AcceptsReturn = True
        Me.txtQualification.BackColor = System.Drawing.SystemColors.Window
        Me.txtQualification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQualification.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQualification.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQualification.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtQualification.Location = New System.Drawing.Point(254, 138)
        Me.txtQualification.MaxLength = 0
        Me.txtQualification.Name = "txtQualification"
        Me.txtQualification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQualification.Size = New System.Drawing.Size(247, 20)
        Me.txtQualification.TabIndex = 112
        '
        'txtCostCenter
        '
        Me.txtCostCenter.AcceptsReturn = True
        Me.txtCostCenter.BackColor = System.Drawing.SystemColors.Window
        Me.txtCostCenter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCostCenter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCostCenter.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCostCenter.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCostCenter.Location = New System.Drawing.Point(254, 38)
        Me.txtCostCenter.MaxLength = 0
        Me.txtCostCenter.Name = "txtCostCenter"
        Me.txtCostCenter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCostCenter.Size = New System.Drawing.Size(247, 20)
        Me.txtCostCenter.TabIndex = 16
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboDept.Location = New System.Drawing.Point(254, 12)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(247, 22)
        Me.cboDept.TabIndex = 15
        '
        'txtExperience
        '
        Me.txtExperience.AcceptsReturn = True
        Me.txtExperience.BackColor = System.Drawing.SystemColors.Window
        Me.txtExperience.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExperience.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExperience.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExperience.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtExperience.Location = New System.Drawing.Point(254, 114)
        Me.txtExperience.MaxLength = 0
        Me.txtExperience.Name = "txtExperience"
        Me.txtExperience.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExperience.Size = New System.Drawing.Size(247, 20)
        Me.txtExperience.TabIndex = 19
        '
        'txtLastCompany
        '
        Me.txtLastCompany.AcceptsReturn = True
        Me.txtLastCompany.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLastCompany.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastCompany.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastCompany.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLastCompany.Location = New System.Drawing.Point(254, 90)
        Me.txtLastCompany.MaxLength = 0
        Me.txtLastCompany.Name = "txtLastCompany"
        Me.txtLastCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastCompany.Size = New System.Drawing.Size(247, 20)
        Me.txtLastCompany.TabIndex = 18
        '
        'cbodesignation
        '
        Me.cbodesignation.BackColor = System.Drawing.SystemColors.Window
        Me.cbodesignation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cbodesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodesignation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbodesignation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cbodesignation.Location = New System.Drawing.Point(254, 62)
        Me.cbodesignation.Name = "cbodesignation"
        Me.cbodesignation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbodesignation.Size = New System.Drawing.Size(247, 22)
        Me.cbodesignation.TabIndex = 17
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(11, 166)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(233, 13)
        Me.Label20.TabIndex = 115
        Me.Label20.Text = "Joining Designation :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.Black
        Me.Label29.Location = New System.Drawing.Point(18, 142)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(226, 16)
        Me.Label29.TabIndex = 114
        Me.Label29.Text = "Qualification :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.BackColor = System.Drawing.Color.Transparent
        Me.Label57.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label57.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label57.Location = New System.Drawing.Point(176, 14)
        Me.Label57.Name = "Label57"
        Me.Label57.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label57.Size = New System.Drawing.Size(68, 14)
        Me.Label57.TabIndex = 99
        Me.Label57.Text = "Department :"
        Me.Label57.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label4.Location = New System.Drawing.Point(174, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(70, 14)
        Me.Label4.TabIndex = 98
        Me.Label4.Text = "Cost Center :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.Transparent
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(2, 116)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(242, 16)
        Me.Label40.TabIndex = 94
        Me.Label40.Text = "Experience (In months) :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(2, 92)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(242, 16)
        Me.Label37.TabIndex = 93
        Me.Label37.Text = "Last Company Name :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label5.Location = New System.Drawing.Point(175, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(69, 14)
        Me.Label5.TabIndex = 78
        Me.Label5.Text = "Designation :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.fraPermAdd)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(900, 287)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Mail"
        '
        'fraPermAdd
        '
        Me.fraPermAdd.BackColor = System.Drawing.SystemColors.Control
        Me.fraPermAdd.Controls.Add(Me.txtMobileOff)
        Me.fraPermAdd.Controls.Add(Me.txtOffeMail)
        Me.fraPermAdd.Controls.Add(Me.chkMetroCity)
        Me.fraPermAdd.Controls.Add(Me.txtPhone)
        Me.fraPermAdd.Controls.Add(Me.txtEmail)
        Me.fraPermAdd.Controls.Add(Me.txtSpouse)
        Me.fraPermAdd.Controls.Add(Me.txtPinCode)
        Me.fraPermAdd.Controls.Add(Me.txtCity)
        Me.fraPermAdd.Controls.Add(Me.txtAddress)
        Me.fraPermAdd.Controls.Add(Me.txtState)
        Me.fraPermAdd.Controls.Add(Me.Label77)
        Me.fraPermAdd.Controls.Add(Me.Label64)
        Me.fraPermAdd.Controls.Add(Me.Label36)
        Me.fraPermAdd.Controls.Add(Me.Label28)
        Me.fraPermAdd.Controls.Add(Me.Label27)
        Me.fraPermAdd.Controls.Add(Me.Label26)
        Me.fraPermAdd.Controls.Add(Me.Label9)
        Me.fraPermAdd.Controls.Add(Me.Label8)
        Me.fraPermAdd.Controls.Add(Me.Label22)
        Me.fraPermAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.fraPermAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPermAdd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.fraPermAdd.Location = New System.Drawing.Point(0, 0)
        Me.fraPermAdd.Name = "fraPermAdd"
        Me.fraPermAdd.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPermAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPermAdd.Size = New System.Drawing.Size(900, 287)
        Me.fraPermAdd.TabIndex = 63
        Me.fraPermAdd.TabStop = False
        Me.fraPermAdd.Text = "Address-Permanent"
        '
        'txtMobileOff
        '
        Me.txtMobileOff.AcceptsReturn = True
        Me.txtMobileOff.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobileOff.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobileOff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMobileOff.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobileOff.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtMobileOff.Location = New System.Drawing.Point(254, 181)
        Me.txtMobileOff.MaxLength = 30
        Me.txtMobileOff.Name = "txtMobileOff"
        Me.txtMobileOff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMobileOff.Size = New System.Drawing.Size(257, 20)
        Me.txtMobileOff.TabIndex = 29
        '
        'txtOffeMail
        '
        Me.txtOffeMail.AcceptsReturn = True
        Me.txtOffeMail.BackColor = System.Drawing.SystemColors.Window
        Me.txtOffeMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOffeMail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOffeMail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOffeMail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtOffeMail.Location = New System.Drawing.Point(254, 160)
        Me.txtOffeMail.MaxLength = 30
        Me.txtOffeMail.Name = "txtOffeMail"
        Me.txtOffeMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOffeMail.Size = New System.Drawing.Size(473, 20)
        Me.txtOffeMail.TabIndex = 28
        '
        'chkMetroCity
        '
        Me.chkMetroCity.AutoSize = True
        Me.chkMetroCity.BackColor = System.Drawing.SystemColors.Control
        Me.chkMetroCity.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMetroCity.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMetroCity.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMetroCity.Location = New System.Drawing.Point(618, 38)
        Me.chkMetroCity.Name = "chkMetroCity"
        Me.chkMetroCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMetroCity.Size = New System.Drawing.Size(85, 18)
        Me.chkMetroCity.TabIndex = 22
        Me.chkMetroCity.Text = "Is Metro City"
        Me.chkMetroCity.UseVisualStyleBackColor = False
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPhone.Location = New System.Drawing.Point(254, 99)
        Me.txtPhone.MaxLength = 30
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(473, 20)
        Me.txtPhone.TabIndex = 25
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtEmail.Location = New System.Drawing.Point(254, 119)
        Me.txtEmail.MaxLength = 30
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail.Size = New System.Drawing.Size(473, 20)
        Me.txtEmail.TabIndex = 26
        '
        'txtSpouse
        '
        Me.txtSpouse.AcceptsReturn = True
        Me.txtSpouse.BackColor = System.Drawing.SystemColors.Window
        Me.txtSpouse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpouse.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSpouse.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSpouse.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtSpouse.Location = New System.Drawing.Point(254, 139)
        Me.txtSpouse.MaxLength = 30
        Me.txtSpouse.Name = "txtSpouse"
        Me.txtSpouse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSpouse.Size = New System.Drawing.Size(473, 20)
        Me.txtSpouse.TabIndex = 27
        '
        'txtPinCode
        '
        Me.txtPinCode.AcceptsReturn = True
        Me.txtPinCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPinCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPinCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPinCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPinCode.Location = New System.Drawing.Point(254, 57)
        Me.txtPinCode.MaxLength = 30
        Me.txtPinCode.Name = "txtPinCode"
        Me.txtPinCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinCode.Size = New System.Drawing.Size(473, 20)
        Me.txtPinCode.TabIndex = 23
        '
        'txtCity
        '
        Me.txtCity.AcceptsReturn = True
        Me.txtCity.BackColor = System.Drawing.SystemColors.Window
        Me.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCity.Location = New System.Drawing.Point(254, 35)
        Me.txtCity.MaxLength = 30
        Me.txtCity.Name = "txtCity"
        Me.txtCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCity.Size = New System.Drawing.Size(303, 20)
        Me.txtCity.TabIndex = 21
        '
        'txtAddress
        '
        Me.txtAddress.AcceptsReturn = True
        Me.txtAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAddress.Location = New System.Drawing.Point(254, 14)
        Me.txtAddress.MaxLength = 30
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress.Size = New System.Drawing.Size(473, 20)
        Me.txtAddress.TabIndex = 20
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.BackColor = System.Drawing.SystemColors.Window
        Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtState.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtState.Location = New System.Drawing.Point(254, 78)
        Me.txtState.MaxLength = 20
        Me.txtState.Name = "txtState"
        Me.txtState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtState.Size = New System.Drawing.Size(473, 20)
        Me.txtState.TabIndex = 24
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.BackColor = System.Drawing.SystemColors.Control
        Me.Label77.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label77.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label77.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label77.Location = New System.Drawing.Point(186, 183)
        Me.Label77.Name = "Label77"
        Me.Label77.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label77.Size = New System.Drawing.Size(62, 14)
        Me.Label77.TabIndex = 129
        Me.Label77.Text = "Mobile (O) :"
        Me.Label77.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label64
        '
        Me.Label64.BackColor = System.Drawing.SystemColors.Control
        Me.Label64.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label64.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label64.Location = New System.Drawing.Point(8, 162)
        Me.Label64.Name = "Label64"
        Me.Label64.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label64.Size = New System.Drawing.Size(236, 13)
        Me.Label64.TabIndex = 104
        Me.Label64.Text = "Official Email-id :"
        Me.Label64.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(8, 142)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(236, 13)
        Me.Label36.TabIndex = 92
        Me.Label36.Text = "Spouse :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(8, 121)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(236, 13)
        Me.Label28.TabIndex = 91
        Me.Label28.Text = "Personal Email-id :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(8, 59)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(236, 13)
        Me.Label27.TabIndex = 90
        Me.Label27.Text = "Pin Code :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(8, 101)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(236, 13)
        Me.Label26.TabIndex = 89
        Me.Label26.Text = "Phone(s) :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(8, 37)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(236, 13)
        Me.Label9.TabIndex = 88
        Me.Label9.Text = "City :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(8, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(236, 12)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Address :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(8, 81)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(236, 13)
        Me.Label22.TabIndex = 65
        Me.Label22.Text = "State :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame2)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame6)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame1)
        Me._SSTab1_TabPage2.Controls.Add(Me.ESI)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame3)
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame4)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(900, 287)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "PF/ESI Detail"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtDOM)
        Me.Frame2.Controls.Add(Me.txtDOBActual)
        Me.Frame2.Controls.Add(Me.Label79)
        Me.Frame2.Controls.Add(Me.Label80)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame2.Location = New System.Drawing.Point(6, 121)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(367, 39)
        Me.Frame2.TabIndex = 125
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Others Details"
        '
        'txtDOM
        '
        Me.txtDOM.AcceptsReturn = True
        Me.txtDOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOM.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOM.Location = New System.Drawing.Point(267, 12)
        Me.txtDOM.MaxLength = 5
        Me.txtDOM.Name = "txtDOM"
        Me.txtDOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOM.Size = New System.Drawing.Size(96, 20)
        Me.txtDOM.TabIndex = 35
        '
        'txtDOBActual
        '
        Me.txtDOBActual.AcceptsReturn = True
        Me.txtDOBActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOBActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOBActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOBActual.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOBActual.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDOBActual.Location = New System.Drawing.Point(85, 12)
        Me.txtDOBActual.MaxLength = 5
        Me.txtDOBActual.Name = "txtDOBActual"
        Me.txtDOBActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOBActual.Size = New System.Drawing.Size(79, 20)
        Me.txtDOBActual.TabIndex = 34
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.BackColor = System.Drawing.SystemColors.Control
        Me.Label79.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label79.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label79.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label79.Location = New System.Drawing.Point(174, 14)
        Me.Label79.Name = "Label79"
        Me.Label79.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label79.Size = New System.Drawing.Size(93, 14)
        Me.Label79.TabIndex = 127
        Me.Label79.Text = "Date of Marriage :"
        Me.Label79.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.BackColor = System.Drawing.SystemColors.Control
        Me.Label80.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label80.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label80.Location = New System.Drawing.Point(10, 14)
        Me.Label80.Name = "Label80"
        Me.Label80.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label80.Size = New System.Drawing.Size(77, 14)
        Me.Label80.TabIndex = 126
        Me.Label80.Text = "DOB (Actual) :"
        Me.Label80.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.chkLEApp)
        Me.Frame6.Controls.Add(Me.chkBonusApp)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame6.Location = New System.Drawing.Point(374, 121)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(358, 39)
        Me.Frame6.TabIndex = 96
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Others"
        '
        'chkLEApp
        '
        Me.chkLEApp.AutoSize = True
        Me.chkLEApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkLEApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLEApp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLEApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLEApp.Location = New System.Drawing.Point(204, 16)
        Me.chkLEApp.Name = "chkLEApp"
        Me.chkLEApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLEApp.Size = New System.Drawing.Size(107, 18)
        Me.chkLEApp.TabIndex = 101
        Me.chkLEApp.Text = "Is L.E. Applicable"
        Me.chkLEApp.UseVisualStyleBackColor = False
        '
        'chkBonusApp
        '
        Me.chkBonusApp.AutoSize = True
        Me.chkBonusApp.BackColor = System.Drawing.SystemColors.Control
        Me.chkBonusApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBonusApp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBonusApp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBonusApp.Location = New System.Drawing.Point(8, 16)
        Me.chkBonusApp.Name = "chkBonusApp"
        Me.chkBonusApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBonusApp.Size = New System.Drawing.Size(120, 18)
        Me.chkBonusApp.TabIndex = 100
        Me.chkBonusApp.Text = "Is Bonus Applicable"
        Me.chkBonusApp.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtPFNo)
        Me.Frame1.Controls.Add(Me.lblPFNo)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(4, 6)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(367, 37)
        Me.Frame1.TabIndex = 86
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "PF"
        '
        'txtPFNo
        '
        Me.txtPFNo.AcceptsReturn = True
        Me.txtPFNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPFNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPFNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPFNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPFNo.Location = New System.Drawing.Point(142, 12)
        Me.txtPFNo.MaxLength = 5
        Me.txtPFNo.Name = "txtPFNo"
        Me.txtPFNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPFNo.Size = New System.Drawing.Size(219, 20)
        Me.txtPFNo.TabIndex = 30
        '
        'lblPFNo
        '
        Me.lblPFNo.AutoSize = True
        Me.lblPFNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblPFNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPFNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPFNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPFNo.Location = New System.Drawing.Point(10, 12)
        Me.lblPFNo.Name = "lblPFNo"
        Me.lblPFNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPFNo.Size = New System.Drawing.Size(50, 14)
        Me.lblPFNo.TabIndex = 87
        Me.lblPFNo.Text = "Number :"
        Me.lblPFNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ESI
        '
        Me.ESI.BackColor = System.Drawing.SystemColors.Control
        Me.ESI.Controls.Add(Me.cboESIApp)
        Me.ESI.Controls.Add(Me.txtESINo)
        Me.ESI.Controls.Add(Me.txtDispensary)
        Me.ESI.Controls.Add(Me.Label42)
        Me.ESI.Controls.Add(Me.lblEsiNo)
        Me.ESI.Controls.Add(Me.lblDispensary)
        Me.ESI.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ESI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.ESI.Location = New System.Drawing.Point(4, 42)
        Me.ESI.Name = "ESI"
        Me.ESI.Padding = New System.Windows.Forms.Padding(0)
        Me.ESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ESI.Size = New System.Drawing.Size(367, 79)
        Me.ESI.TabIndex = 83
        Me.ESI.TabStop = False
        Me.ESI.Text = "ESI"
        '
        'cboESIApp
        '
        Me.cboESIApp.BackColor = System.Drawing.SystemColors.Window
        Me.cboESIApp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboESIApp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboESIApp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboESIApp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboESIApp.Location = New System.Drawing.Point(142, 10)
        Me.cboESIApp.Name = "cboESIApp"
        Me.cboESIApp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboESIApp.Size = New System.Drawing.Size(133, 22)
        Me.cboESIApp.TabIndex = 31
        '
        'txtESINo
        '
        Me.txtESINo.AcceptsReturn = True
        Me.txtESINo.BackColor = System.Drawing.SystemColors.Window
        Me.txtESINo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESINo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESINo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESINo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtESINo.Location = New System.Drawing.Point(142, 34)
        Me.txtESINo.MaxLength = 6
        Me.txtESINo.Name = "txtESINo"
        Me.txtESINo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESINo.Size = New System.Drawing.Size(219, 20)
        Me.txtESINo.TabIndex = 32
        '
        'txtDispensary
        '
        Me.txtDispensary.AcceptsReturn = True
        Me.txtDispensary.BackColor = System.Drawing.SystemColors.Window
        Me.txtDispensary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDispensary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDispensary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDispensary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDispensary.Location = New System.Drawing.Point(142, 56)
        Me.txtDispensary.MaxLength = 0
        Me.txtDispensary.Name = "txtDispensary"
        Me.txtDispensary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDispensary.Size = New System.Drawing.Size(219, 20)
        Me.txtDispensary.TabIndex = 33
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.Color.Transparent
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label42.Location = New System.Drawing.Point(33, 12)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(87, 14)
        Me.Label42.TabIndex = 95
        Me.Label42.Text = "ESI Applicability :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblEsiNo
        '
        Me.lblEsiNo.AutoSize = True
        Me.lblEsiNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblEsiNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEsiNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEsiNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEsiNo.Location = New System.Drawing.Point(10, 34)
        Me.lblEsiNo.Name = "lblEsiNo"
        Me.lblEsiNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEsiNo.Size = New System.Drawing.Size(50, 14)
        Me.lblEsiNo.TabIndex = 85
        Me.lblEsiNo.Text = "Number :"
        Me.lblEsiNo.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDispensary
        '
        Me.lblDispensary.AutoSize = True
        Me.lblDispensary.BackColor = System.Drawing.Color.Transparent
        Me.lblDispensary.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDispensary.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDispensary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDispensary.Location = New System.Drawing.Point(10, 56)
        Me.lblDispensary.Name = "lblDispensary"
        Me.lblDispensary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDispensary.Size = New System.Drawing.Size(68, 14)
        Me.lblDispensary.TabIndex = 84
        Me.lblDispensary.Text = "Dispensary :"
        Me.lblDispensary.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtAdhaarNo)
        Me.Frame3.Controls.Add(Me.txtPanNo)
        Me.Frame3.Controls.Add(Me.Label76)
        Me.Frame3.Controls.Add(Me.Label46)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame3.Location = New System.Drawing.Point(372, 6)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(367, 59)
        Me.Frame3.TabIndex = 81
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "PAN / GIR"
        '
        'txtAdhaarNo
        '
        Me.txtAdhaarNo.AcceptsReturn = True
        Me.txtAdhaarNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAdhaarNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdhaarNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdhaarNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdhaarNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtAdhaarNo.Location = New System.Drawing.Point(142, 33)
        Me.txtAdhaarNo.MaxLength = 5
        Me.txtAdhaarNo.Name = "txtAdhaarNo"
        Me.txtAdhaarNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdhaarNo.Size = New System.Drawing.Size(219, 20)
        Me.txtAdhaarNo.TabIndex = 37
        '
        'txtPanNo
        '
        Me.txtPanNo.AcceptsReturn = True
        Me.txtPanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPanNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPanNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPanNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtPanNo.Location = New System.Drawing.Point(142, 12)
        Me.txtPanNo.MaxLength = 5
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPanNo.Size = New System.Drawing.Size(219, 20)
        Me.txtPanNo.TabIndex = 36
        '
        'Label76
        '
        Me.Label76.BackColor = System.Drawing.SystemColors.Control
        Me.Label76.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label76.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label76.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label76.Location = New System.Drawing.Point(56, 35)
        Me.Label76.Name = "Label76"
        Me.Label76.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label76.Size = New System.Drawing.Size(87, 12)
        Me.Label76.TabIndex = 128
        Me.Label76.Text = "Adhaar Number :"
        Me.Label76.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(65, 13)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(73, 14)
        Me.Label46.TabIndex = 82
        Me.Label46.Text = "PAN Number :"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtLICID)
        Me.Frame4.Controls.Add(Me.Label25)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame4.Location = New System.Drawing.Point(372, 67)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(367, 43)
        Me.Frame4.TabIndex = 79
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "LIC ID"
        '
        'txtLICID
        '
        Me.txtLICID.AcceptsReturn = True
        Me.txtLICID.BackColor = System.Drawing.SystemColors.Window
        Me.txtLICID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLICID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLICID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLICID.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtLICID.Location = New System.Drawing.Point(142, 16)
        Me.txtLICID.MaxLength = 5
        Me.txtLICID.Name = "txtLICID"
        Me.txtLICID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLICID.Size = New System.Drawing.Size(219, 20)
        Me.txtLICID.TabIndex = 38
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(10, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(50, 14)
        Me.Label25.TabIndex = 80
        Me.Label25.Text = "Number :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.sprdDeduct)
        Me._SSTab1_TabPage3.Controls.Add(Me.sprdEarn)
        Me._SSTab1_TabPage3.Controls.Add(Me.grdDeductions)
        Me._SSTab1_TabPage3.Controls.Add(Me.Label11)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(900, 287)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "Salary"
        '
        'sprdDeduct
        '
        Me.sprdDeduct.DataSource = Nothing
        Me.sprdDeduct.Location = New System.Drawing.Point(449, 19)
        Me.sprdDeduct.Name = "sprdDeduct"
        Me.sprdDeduct.OcxState = CType(resources.GetObject("sprdDeduct.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdDeduct.Size = New System.Drawing.Size(444, 265)
        Me.sprdDeduct.TabIndex = 105
        '
        'sprdEarn
        '
        Me.sprdEarn.DataSource = Nothing
        Me.sprdEarn.Location = New System.Drawing.Point(2, 19)
        Me.sprdEarn.Name = "sprdEarn"
        Me.sprdEarn.OcxState = CType(resources.GetObject("sprdEarn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdEarn.Size = New System.Drawing.Size(444, 265)
        Me.sprdEarn.TabIndex = 106
        '
        'grdDeductions
        '
        Me.grdDeductions.BackColor = System.Drawing.SystemColors.Control
        Me.grdDeductions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdDeductions.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdDeductions.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDeductions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdDeductions.Location = New System.Drawing.Point(449, 0)
        Me.grdDeductions.Name = "grdDeductions"
        Me.grdDeductions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdDeductions.Size = New System.Drawing.Size(444, 19)
        Me.grdDeductions.TabIndex = 108
        Me.grdDeductions.Text = "Deductions"
        Me.grdDeductions.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(444, 19)
        Me.Label11.TabIndex = 107
        Me.Label11.Text = "Earnings"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me.sprdPerks)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label58)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(900, 287)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "Perks"
        '
        'sprdPerks
        '
        Me.sprdPerks.DataSource = Nothing
        Me.sprdPerks.Location = New System.Drawing.Point(4, 19)
        Me.sprdPerks.Name = "sprdPerks"
        Me.sprdPerks.OcxState = CType(resources.GetObject("sprdPerks.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdPerks.Size = New System.Drawing.Size(510, 263)
        Me.sprdPerks.TabIndex = 109
        '
        'Label58
        '
        Me.Label58.BackColor = System.Drawing.SystemColors.Control
        Me.Label58.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label58.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label58.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label58.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label58.Location = New System.Drawing.Point(4, 0)
        Me.Label58.Name = "Label58"
        Me.Label58.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label58.Size = New System.Drawing.Size(510, 19)
        Me.Label58.TabIndex = 110
        Me.Label58.Text = "Perks"
        Me.Label58.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_SSTab1_TabPage5
        '
        Me._SSTab1_TabPage5.Controls.Add(Me.sprdSpouse)
        Me._SSTab1_TabPage5.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage5.Name = "_SSTab1_TabPage5"
        Me._SSTab1_TabPage5.Size = New System.Drawing.Size(900, 287)
        Me._SSTab1_TabPage5.TabIndex = 5
        Me._SSTab1_TabPage5.Text = "Spouse Details"
        '
        'sprdSpouse
        '
        Me.sprdSpouse.DataSource = Nothing
        Me.sprdSpouse.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdSpouse.Location = New System.Drawing.Point(0, 0)
        Me.sprdSpouse.Name = "sprdSpouse"
        Me.sprdSpouse.OcxState = CType(resources.GetObject("sprdSpouse.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdSpouse.Size = New System.Drawing.Size(900, 287)
        Me.sprdSpouse.TabIndex = 122
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.BackColor = System.Drawing.SystemColors.Control
        Me.Label59.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label59.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label59.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label59.Location = New System.Drawing.Point(629, 352)
        Me.Label59.Name = "Label59"
        Me.Label59.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label59.Size = New System.Drawing.Size(77, 14)
        Me.Label59.TabIndex = 103
        Me.Label59.Text = "Form 1 C.T.C. :"
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(428, 352)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(99, 14)
        Me.Label43.TabIndex = 75
        Me.Label43.Text = "Form 1 Net Salary :"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label41.Location = New System.Drawing.Point(240, 352)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(61, 14)
        Me.Label41.TabIndex = 74
        Me.Label41.Text = "Deduction :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(6, 352)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(113, 14)
        Me.Label15.TabIndex = 73
        Me.Label15.Text = "Form 1 Gross Salary :"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(10, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(111, 19)
        Me.Label7.TabIndex = 61
        Me.Label7.Text = "Form 1 Basic Salary :"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(-4904, 144)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(37, 14)
        Me.Label30.TabIndex = 70
        Me.Label30.Text = "Grade"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(-4936, 96)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(70, 16)
        Me.Label31.TabIndex = 67
        Me.Label31.Text = "Department"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(-4936, 120)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(63, 14)
        Me.Label32.TabIndex = 69
        Me.Label32.Text = "Designation"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(-4968, 188)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(50, 16)
        Me.Label33.TabIndex = 72
        Me.Label33.Text = "Pincode"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(-4968, 164)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(22, 16)
        Me.Label34.TabIndex = 71
        Me.Label34.Text = "City"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(-4968, 92)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(51, 16)
        Me.Label35.TabIndex = 66
        Me.Label35.Text = "Address"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(910, 574)
        Me.SprdView.TabIndex = 97
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.cmdeMail)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 572)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(910, 51)
        Me.FraMovement.TabIndex = 76
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(8, 16)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 136
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(309, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 44
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(513, 10)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.cmdPreview.TabIndex = 47
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Menu
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label44.Location = New System.Drawing.Point(222, 42)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(32, 14)
        Me.Label44.TabIndex = 59
        Me.Label44.Text = "Sex :"
        '
        'frmCandidateMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.fraTop)
        Me.Controls.Add(Me.FraBottom)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Label44)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmCandidateMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Selected Candidate Master"
        Me.fraTop.ResumeLayout(False)
        Me.fraTop.PerformLayout()
        Me.PbPhoto.ResumeLayout(False)
        CType(Me.ImagePhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraBottom.ResumeLayout(False)
        Me.FraBottom.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.fraModePayment.ResumeLayout(False)
        Me.fraModePayment.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.fraPermAdd.ResumeLayout(False)
        Me.fraPermAdd.PerformLayout()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ESI.ResumeLayout(False)
        Me.ESI.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        CType(Me.sprdPerks, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage5.ResumeLayout(False)
        CType(Me.sprdSpouse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Public WithEvents Label81 As Label
    Public WithEvents txtForm1BSalary As TextBox
    Public WithEvents txtForm1CTC As TextBox
    Public WithEvents txtForm1NetSalary As TextBox
    Public WithEvents Label83 As Label
    Public WithEvents Label84 As Label
    Public WithEvents txtForm1GSalary As TextBox
    Public WithEvents Label82 As Label
#End Region
End Class