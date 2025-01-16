Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFFSettlement
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
        'Me.MDIParent = Payroll.Master
        'Payroll.Master.Show()
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
    Public WithEvents Reset_Renamed As System.Windows.Forms.Button
    Public WithEvents txtBSalary As System.Windows.Forms.TextBox
    Public WithEvents txtAtcBasic As System.Windows.Forms.TextBox
    Public WithEvents txtTotOthers As System.Windows.Forms.TextBox
    Public WithEvents txtReason As System.Windows.Forms.TextBox
    Public WithEvents chkAccountPosting As System.Windows.Forms.CheckBox
    Public WithEvents txtDOL As System.Windows.Forms.TextBox
    Public WithEvents txtFName As System.Windows.Forms.TextBox
    Public WithEvents cbodesignation As System.Windows.Forms.ComboBox
    Public WithEvents txtDOJ As System.Windows.Forms.TextBox
    Public WithEvents txtEmpNo As System.Windows.Forms.TextBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblDesg As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents fraTop As System.Windows.Forms.GroupBox
    Public WithEvents txtPaidDays As System.Windows.Forms.TextBox
    Public WithEvents txtNetSalary As System.Windows.Forms.TextBox
    Public WithEvents txtDeduction As System.Windows.Forms.TextBox
    Public WithEvents txtGSalary As System.Windows.Forms.TextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents grdDeductions As System.Windows.Forms.Label
    Public WithEvents sprdEarn As AxFPSpreadADO.AxfpSpread
    Public WithEvents sprdDeduct As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents chkCPL As System.Windows.Forms.CheckBox
    Public WithEvents chkCalcPFonEL As System.Windows.Forms.CheckBox
    Public WithEvents cmdLeave As System.Windows.Forms.Button
    Public WithEvents txtELAmount As System.Windows.Forms.TextBox
    Public WithEvents txtELDays As System.Windows.Forms.TextBox
    Public WithEvents lblBasicEL As System.Windows.Forms.Label
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents txtLTCFrom As System.Windows.Forms.TextBox
    Public WithEvents txtLTCMonth As System.Windows.Forms.TextBox
    Public WithEvents txtLTCAmt As System.Windows.Forms.TextBox
    Public WithEvents Label51 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents txtSalArrear As System.Windows.Forms.TextBox
    Public WithEvents txtIncArrear As System.Windows.Forms.TextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtIncAmtPreMon As System.Windows.Forms.TextBox
    Public WithEvents txtIncAmtForMon As System.Windows.Forms.TextBox
    Public WithEvents txtIncHoursPreMon As System.Windows.Forms.TextBox
    Public WithEvents txtIncHoursForMon As System.Windows.Forms.TextBox
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkBonusPaid As System.Windows.Forms.CheckBox
    Public WithEvents chkMannualPerBonus As System.Windows.Forms.CheckBox
    Public WithEvents txtBonusPerForYear As System.Windows.Forms.TextBox
    Public WithEvents txtBonusPerCurrYear As System.Windows.Forms.TextBox
    Public WithEvents txtBonusCurrYear As System.Windows.Forms.TextBox
    Public WithEvents txtBonusForYear As System.Windows.Forms.TextBox
    Public WithEvents lblMannual As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents txtCompAmount As System.Windows.Forms.TextBox
    Public WithEvents txtCompMonth As System.Windows.Forms.TextBox
    Public WithEvents txtExGratiaAmount As System.Windows.Forms.TextBox
    Public WithEvents txtExGratiaMonth As System.Windows.Forms.TextBox
    Public WithEvents txtGratuityMon As System.Windows.Forms.TextBox
    Public WithEvents txtNoticeMon As System.Windows.Forms.TextBox
    Public WithEvents txtGratuityAmt As System.Windows.Forms.TextBox
    Public WithEvents txtNoticeamt As System.Windows.Forms.TextBox
    Public WithEvents Label49 As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents txtOthers As System.Windows.Forms.TextBox
    Public WithEvents Label38 As System.Windows.Forms.Label
    Public WithEvents lblActGross As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents chkSuspension As System.Windows.Forms.CheckBox
    Public WithEvents txtSuspension As System.Windows.Forms.TextBox
    Public WithEvents Label50 As System.Windows.Forms.Label
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents chkTransfer As System.Windows.Forms.CheckBox
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents txtChqNo As System.Windows.Forms.TextBox
    Public WithEvents txtBankName As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents Label37 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdPolicyPreview As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdAccountPosting As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label44 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFFSettlement))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Reset_Renamed = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdLeave = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdAccountPosting = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grdDeductions = New System.Windows.Forms.Label()
        Me.sprdEarn = New AxFPSpreadADO.AxfpSpread()
        Me.sprdDeduct = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.chkCPL = New System.Windows.Forms.CheckBox()
        Me.chkCalcPFonEL = New System.Windows.Forms.CheckBox()
        Me.txtELAmount = New System.Windows.Forms.TextBox()
        Me.txtELDays = New System.Windows.Forms.TextBox()
        Me.lblBasicEL = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtLTCFrom = New System.Windows.Forms.TextBox()
        Me.txtLTCMonth = New System.Windows.Forms.TextBox()
        Me.txtLTCAmt = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtSalArrear = New System.Windows.Forms.TextBox()
        Me.txtIncArrear = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtIncAmtPreMon = New System.Windows.Forms.TextBox()
        Me.txtIncAmtForMon = New System.Windows.Forms.TextBox()
        Me.txtIncHoursPreMon = New System.Windows.Forms.TextBox()
        Me.txtIncHoursForMon = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkBonusPaid = New System.Windows.Forms.CheckBox()
        Me.chkMannualPerBonus = New System.Windows.Forms.CheckBox()
        Me.txtBonusPerForYear = New System.Windows.Forms.TextBox()
        Me.txtBonusPerCurrYear = New System.Windows.Forms.TextBox()
        Me.txtBonusCurrYear = New System.Windows.Forms.TextBox()
        Me.txtBonusForYear = New System.Windows.Forms.TextBox()
        Me.lblMannual = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtCompAmount = New System.Windows.Forms.TextBox()
        Me.txtCompMonth = New System.Windows.Forms.TextBox()
        Me.txtExGratiaAmount = New System.Windows.Forms.TextBox()
        Me.txtExGratiaMonth = New System.Windows.Forms.TextBox()
        Me.txtGratuityMon = New System.Windows.Forms.TextBox()
        Me.txtNoticeMon = New System.Windows.Forms.TextBox()
        Me.txtGratuityAmt = New System.Windows.Forms.TextBox()
        Me.txtNoticeamt = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtOthers = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblActGross = New System.Windows.Forms.Label()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.chkSuspension = New System.Windows.Forms.CheckBox()
        Me.txtSuspension = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.chkTransfer = New System.Windows.Forms.CheckBox()
        Me.txtBSalary = New System.Windows.Forms.TextBox()
        Me.txtAtcBasic = New System.Windows.Forms.TextBox()
        Me.txtTotOthers = New System.Windows.Forms.TextBox()
        Me.fraTop = New System.Windows.Forms.GroupBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.chkAccountPosting = New System.Windows.Forms.CheckBox()
        Me.txtDOL = New System.Windows.Forms.TextBox()
        Me.txtFName = New System.Windows.Forms.TextBox()
        Me.cbodesignation = New System.Windows.Forms.ComboBox()
        Me.txtDOJ = New System.Windows.Forms.TextBox()
        Me.txtEmpNo = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblDesg = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtPaidDays = New System.Windows.Forms.TextBox()
        Me.txtNetSalary = New System.Windows.Forms.TextBox()
        Me.txtDeduction = New System.Windows.Forms.TextBox()
        Me.txtGSalary = New System.Windows.Forms.TextBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdEmailExternal = New System.Windows.Forms.Button()
        Me.cmdEMailAccounts = New System.Windows.Forms.Button()
        Me.cmdPolicyPreview = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.FraMain.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.Frame10.SuspendLayout()
        Me.fraTop.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Reset_Renamed
        '
        Me.Reset_Renamed.BackColor = System.Drawing.SystemColors.Control
        Me.Reset_Renamed.Cursor = System.Windows.Forms.Cursors.Default
        Me.Reset_Renamed.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Reset_Renamed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Reset_Renamed.Location = New System.Drawing.Point(610, 86)
        Me.Reset_Renamed.Name = "Reset_Renamed"
        Me.Reset_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Reset_Renamed.Size = New System.Drawing.Size(127, 19)
        Me.Reset_Renamed.TabIndex = 122
        Me.Reset_Renamed.Text = "&Calculate"
        Me.ToolTip1.SetToolTip(Me.Reset_Renamed, "Add New Record")
        Me.Reset_Renamed.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(216, 12)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdLeave
        '
        Me.cmdLeave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLeave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdLeave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLeave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLeave.Image = CType(resources.GetObject("cmdLeave.Image"), System.Drawing.Image)
        Me.cmdLeave.Location = New System.Drawing.Point(40, 50)
        Me.cmdLeave.Name = "cmdLeave"
        Me.cmdLeave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdLeave.Size = New System.Drawing.Size(33, 17)
        Me.cmdLeave.TabIndex = 34
        Me.cmdLeave.Text = "-->>"
        Me.cmdLeave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdLeave, "Add New Record")
        Me.cmdLeave.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(961, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(73, 37)
        Me.CmdClose.TabIndex = 59
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
        Me.CmdView.Location = New System.Drawing.Point(889, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(73, 37)
        Me.CmdView.TabIndex = 58
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(529, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(73, 37)
        Me.cmdPrint.TabIndex = 56
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(457, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(73, 37)
        Me.CmdDelete.TabIndex = 55
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdAccountPosting
        '
        Me.cmdAccountPosting.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAccountPosting.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAccountPosting.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAccountPosting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAccountPosting.Image = CType(resources.GetObject("cmdAccountPosting.Image"), System.Drawing.Image)
        Me.cmdAccountPosting.Location = New System.Drawing.Point(385, 10)
        Me.cmdAccountPosting.Name = "cmdAccountPosting"
        Me.cmdAccountPosting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAccountPosting.Size = New System.Drawing.Size(73, 37)
        Me.cmdAccountPosting.TabIndex = 124
        Me.cmdAccountPosting.Text = "A/c Posting"
        Me.cmdAccountPosting.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAccountPosting, "Add New Record")
        Me.cmdAccountPosting.UseVisualStyleBackColor = False
        Me.cmdAccountPosting.Visible = False
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
        Me.CmdSave.Size = New System.Drawing.Size(73, 37)
        Me.CmdSave.TabIndex = 53
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(169, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(73, 37)
        Me.CmdModify.TabIndex = 52
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
        Me.CmdAdd.Location = New System.Drawing.Point(97, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(73, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.SSTab1)
        Me.FraMain.Controls.Add(Me.Reset_Renamed)
        Me.FraMain.Controls.Add(Me.txtBSalary)
        Me.FraMain.Controls.Add(Me.txtAtcBasic)
        Me.FraMain.Controls.Add(Me.txtTotOthers)
        Me.FraMain.Controls.Add(Me.fraTop)
        Me.FraMain.Controls.Add(Me.txtPaidDays)
        Me.FraMain.Controls.Add(Me.txtNetSalary)
        Me.FraMain.Controls.Add(Me.txtDeduction)
        Me.FraMain.Controls.Add(Me.txtGSalary)
        Me.FraMain.Controls.Add(Me.Frame8)
        Me.FraMain.Controls.Add(Me.Label4)
        Me.FraMain.Controls.Add(Me.Label6)
        Me.FraMain.Controls.Add(Me.Label5)
        Me.FraMain.Controls.Add(Me.Label2)
        Me.FraMain.Controls.Add(Me.Label43)
        Me.FraMain.Controls.Add(Me.Label41)
        Me.FraMain.Controls.Add(Me.Label15)
        Me.FraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, -6)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(1107, 583)
        Me.FraMain.TabIndex = 60
        Me.FraMain.TabStop = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 21)
        Me.SSTab1.Location = New System.Drawing.Point(0, 108)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 1
        Me.SSTab1.Size = New System.Drawing.Size(1107, 394)
        Me.SSTab1.TabIndex = 78
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.Label11)
        Me._SSTab1_TabPage0.Controls.Add(Me.grdDeductions)
        Me._SSTab1_TabPage0.Controls.Add(Me.sprdEarn)
        Me._SSTab1_TabPage0.Controls.Add(Me.sprdDeduct)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(1099, 365)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Salary"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(8, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(539, 19)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Earnings"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'grdDeductions
        '
        Me.grdDeductions.BackColor = System.Drawing.SystemColors.Control
        Me.grdDeductions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdDeductions.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdDeductions.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDeductions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdDeductions.Location = New System.Drawing.Point(553, 0)
        Me.grdDeductions.Name = "grdDeductions"
        Me.grdDeductions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdDeductions.Size = New System.Drawing.Size(538, 19)
        Me.grdDeductions.TabIndex = 1
        Me.grdDeductions.Text = "Deductions"
        Me.grdDeductions.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'sprdEarn
        '
        Me.sprdEarn.DataSource = Nothing
        Me.sprdEarn.Location = New System.Drawing.Point(8, 21)
        Me.sprdEarn.Name = "sprdEarn"
        Me.sprdEarn.OcxState = CType(resources.GetObject("sprdEarn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdEarn.Size = New System.Drawing.Size(541, 341)
        Me.sprdEarn.TabIndex = 13
        '
        'sprdDeduct
        '
        Me.sprdDeduct.DataSource = Nothing
        Me.sprdDeduct.Location = New System.Drawing.Point(553, 21)
        Me.sprdDeduct.Name = "sprdDeduct"
        Me.sprdDeduct.OcxState = CType(resources.GetObject("sprdDeduct.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdDeduct.Size = New System.Drawing.Size(540, 341)
        Me.sprdDeduct.TabIndex = 14
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame7)
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame3)
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame2)
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame1)
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame4)
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame6)
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame5)
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame9)
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame10)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(1099, 365)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Other Details"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.chkCPL)
        Me.Frame7.Controls.Add(Me.chkCalcPFonEL)
        Me.Frame7.Controls.Add(Me.cmdLeave)
        Me.Frame7.Controls.Add(Me.txtELAmount)
        Me.Frame7.Controls.Add(Me.txtELDays)
        Me.Frame7.Controls.Add(Me.lblBasicEL)
        Me.Frame7.Controls.Add(Me.Label42)
        Me.Frame7.Controls.Add(Me.Label40)
        Me.Frame7.Controls.Add(Me.Label39)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(295, 117)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(229, 93)
        Me.Frame7.TabIndex = 112
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Earned Leave Encashment"
        '
        'chkCPL
        '
        Me.chkCPL.AutoSize = True
        Me.chkCPL.BackColor = System.Drawing.SystemColors.Control
        Me.chkCPL.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCPL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCPL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCPL.Location = New System.Drawing.Point(106, 30)
        Me.chkCPL.Name = "chkCPL"
        Me.chkCPL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCPL.Size = New System.Drawing.Size(68, 18)
        Me.chkCPL.TabIndex = 131
        Me.chkCPL.Text = "Add CPL"
        Me.chkCPL.UseVisualStyleBackColor = False
        '
        'chkCalcPFonEL
        '
        Me.chkCalcPFonEL.AutoSize = True
        Me.chkCalcPFonEL.BackColor = System.Drawing.SystemColors.Control
        Me.chkCalcPFonEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCalcPFonEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCalcPFonEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCalcPFonEL.Location = New System.Drawing.Point(106, 14)
        Me.chkCalcPFonEL.Name = "chkCalcPFonEL"
        Me.chkCalcPFonEL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCalcPFonEL.Size = New System.Drawing.Size(94, 18)
        Me.chkCalcPFonEL.TabIndex = 33
        Me.chkCalcPFonEL.Text = "Calc PF On EL"
        Me.chkCalcPFonEL.UseVisualStyleBackColor = False
        '
        'txtELAmount
        '
        Me.txtELAmount.AcceptsReturn = True
        Me.txtELAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtELAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtELAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtELAmount.Enabled = False
        Me.txtELAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtELAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtELAmount.Location = New System.Drawing.Point(136, 68)
        Me.txtELAmount.MaxLength = 0
        Me.txtELAmount.Name = "txtELAmount"
        Me.txtELAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtELAmount.Size = New System.Drawing.Size(85, 20)
        Me.txtELAmount.TabIndex = 37
        '
        'txtELDays
        '
        Me.txtELDays.AcceptsReturn = True
        Me.txtELDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtELDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtELDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtELDays.Enabled = False
        Me.txtELDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtELDays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtELDays.Location = New System.Drawing.Point(88, 68)
        Me.txtELDays.MaxLength = 0
        Me.txtELDays.Name = "txtELDays"
        Me.txtELDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtELDays.Size = New System.Drawing.Size(47, 20)
        Me.txtELDays.TabIndex = 36
        '
        'lblBasicEL
        '
        Me.lblBasicEL.AutoSize = True
        Me.lblBasicEL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBasicEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBasicEL.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBasicEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBasicEL.Location = New System.Drawing.Point(6, 30)
        Me.lblBasicEL.Name = "lblBasicEL"
        Me.lblBasicEL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBasicEL.Size = New System.Drawing.Size(56, 14)
        Me.lblBasicEL.TabIndex = 123
        Me.lblBasicEL.Text = "lblBasicEL"
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(38, 70)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(46, 14)
        Me.Label42.TabIndex = 115
        Me.Label42.Text = "Leave  :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(136, 50)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(85, 17)
        Me.Label40.TabIndex = 114
        Me.Label40.Text = "Amount (Rs.)"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(88, 50)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(47, 17)
        Me.Label39.TabIndex = 113
        Me.Label39.Text = "Days"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtLTCFrom)
        Me.Frame3.Controls.Add(Me.txtLTCMonth)
        Me.Frame3.Controls.Add(Me.txtLTCAmt)
        Me.Frame3.Controls.Add(Me.Label51)
        Me.Frame3.Controls.Add(Me.Label27)
        Me.Frame3.Controls.Add(Me.Label26)
        Me.Frame3.Controls.Add(Me.Label18)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(525, -3)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(209, 119)
        Me.Frame3.TabIndex = 93
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "L.T.C."
        '
        'txtLTCFrom
        '
        Me.txtLTCFrom.AcceptsReturn = True
        Me.txtLTCFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtLTCFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLTCFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLTCFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLTCFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLTCFrom.Location = New System.Drawing.Point(116, 20)
        Me.txtLTCFrom.MaxLength = 0
        Me.txtLTCFrom.Name = "txtLTCFrom"
        Me.txtLTCFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLTCFrom.Size = New System.Drawing.Size(87, 20)
        Me.txtLTCFrom.TabIndex = 35
        '
        'txtLTCMonth
        '
        Me.txtLTCMonth.AcceptsReturn = True
        Me.txtLTCMonth.BackColor = System.Drawing.SystemColors.Window
        Me.txtLTCMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLTCMonth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLTCMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLTCMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLTCMonth.Location = New System.Drawing.Point(60, 60)
        Me.txtLTCMonth.MaxLength = 0
        Me.txtLTCMonth.Name = "txtLTCMonth"
        Me.txtLTCMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLTCMonth.Size = New System.Drawing.Size(57, 20)
        Me.txtLTCMonth.TabIndex = 38
        '
        'txtLTCAmt
        '
        Me.txtLTCAmt.AcceptsReturn = True
        Me.txtLTCAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtLTCAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLTCAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLTCAmt.Enabled = False
        Me.txtLTCAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLTCAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLTCAmt.Location = New System.Drawing.Point(118, 60)
        Me.txtLTCAmt.MaxLength = 0
        Me.txtLTCAmt.Name = "txtLTCAmt"
        Me.txtLTCAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLTCAmt.Size = New System.Drawing.Size(87, 20)
        Me.txtLTCAmt.TabIndex = 39
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.BackColor = System.Drawing.SystemColors.Control
        Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label51.Location = New System.Drawing.Point(49, 22)
        Me.Label51.Name = "Label51"
        Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label51.Size = New System.Drawing.Size(58, 14)
        Me.Label51.TabIndex = 132
        Me.Label51.Text = "LTC From :"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(60, 42)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(57, 17)
        Me.Label27.TabIndex = 105
        Me.Label27.Text = "Months"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(118, 42)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(87, 17)
        Me.Label26.TabIndex = 104
        Me.Label26.Text = "Amount (Rs.)"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(4, 62)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(43, 14)
        Me.Label18.TabIndex = 94
        Me.Label18.Text = "L.T.C.  :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtSalArrear)
        Me.Frame2.Controls.Add(Me.txtIncArrear)
        Me.Frame2.Controls.Add(Me.Label25)
        Me.Frame2.Controls.Add(Me.Label16)
        Me.Frame2.Controls.Add(Me.Label14)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(3, 67)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(289, 71)
        Me.Frame2.TabIndex = 90
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Arrear"
        '
        'txtSalArrear
        '
        Me.txtSalArrear.AcceptsReturn = True
        Me.txtSalArrear.BackColor = System.Drawing.SystemColors.Window
        Me.txtSalArrear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalArrear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSalArrear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalArrear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSalArrear.Location = New System.Drawing.Point(186, 28)
        Me.txtSalArrear.MaxLength = 0
        Me.txtSalArrear.Name = "txtSalArrear"
        Me.txtSalArrear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSalArrear.Size = New System.Drawing.Size(93, 20)
        Me.txtSalArrear.TabIndex = 19
        '
        'txtIncArrear
        '
        Me.txtIncArrear.AcceptsReturn = True
        Me.txtIncArrear.BackColor = System.Drawing.SystemColors.Window
        Me.txtIncArrear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncArrear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIncArrear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncArrear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIncArrear.Location = New System.Drawing.Point(186, 48)
        Me.txtIncArrear.MaxLength = 0
        Me.txtIncArrear.Name = "txtIncArrear"
        Me.txtIncArrear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIncArrear.Size = New System.Drawing.Size(93, 20)
        Me.txtIncArrear.TabIndex = 20
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(186, 10)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(93, 17)
        Me.Label25.TabIndex = 103
        Me.Label25.Text = "Amount (Rs.)"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(85, 30)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(87, 14)
        Me.Label16.TabIndex = 92
        Me.Label16.Text = "Salary / Wages :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(120, 50)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(56, 14)
        Me.Label14.TabIndex = 91
        Me.Label14.Text = "Incentive :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtIncAmtPreMon)
        Me.Frame1.Controls.Add(Me.txtIncAmtForMon)
        Me.Frame1.Controls.Add(Me.txtIncHoursPreMon)
        Me.Frame1.Controls.Add(Me.txtIncHoursForMon)
        Me.Frame1.Controls.Add(Me.Label24)
        Me.Frame1.Controls.Add(Me.Label23)
        Me.Frame1.Controls.Add(Me.Label13)
        Me.Frame1.Controls.Add(Me.Label10)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(3, -3)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(289, 71)
        Me.Frame1.TabIndex = 87
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Incentive"
        '
        'txtIncAmtPreMon
        '
        Me.txtIncAmtPreMon.AcceptsReturn = True
        Me.txtIncAmtPreMon.BackColor = System.Drawing.SystemColors.Window
        Me.txtIncAmtPreMon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncAmtPreMon.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIncAmtPreMon.Enabled = False
        Me.txtIncAmtPreMon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncAmtPreMon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIncAmtPreMon.Location = New System.Drawing.Point(186, 48)
        Me.txtIncAmtPreMon.MaxLength = 0
        Me.txtIncAmtPreMon.Name = "txtIncAmtPreMon"
        Me.txtIncAmtPreMon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIncAmtPreMon.Size = New System.Drawing.Size(93, 20)
        Me.txtIncAmtPreMon.TabIndex = 18
        '
        'txtIncAmtForMon
        '
        Me.txtIncAmtForMon.AcceptsReturn = True
        Me.txtIncAmtForMon.BackColor = System.Drawing.SystemColors.Window
        Me.txtIncAmtForMon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncAmtForMon.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIncAmtForMon.Enabled = False
        Me.txtIncAmtForMon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncAmtForMon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIncAmtForMon.Location = New System.Drawing.Point(186, 28)
        Me.txtIncAmtForMon.MaxLength = 0
        Me.txtIncAmtForMon.Name = "txtIncAmtForMon"
        Me.txtIncAmtForMon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIncAmtForMon.Size = New System.Drawing.Size(93, 20)
        Me.txtIncAmtForMon.TabIndex = 16
        '
        'txtIncHoursPreMon
        '
        Me.txtIncHoursPreMon.AcceptsReturn = True
        Me.txtIncHoursPreMon.BackColor = System.Drawing.SystemColors.Window
        Me.txtIncHoursPreMon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncHoursPreMon.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIncHoursPreMon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncHoursPreMon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIncHoursPreMon.Location = New System.Drawing.Point(108, 48)
        Me.txtIncHoursPreMon.MaxLength = 0
        Me.txtIncHoursPreMon.Name = "txtIncHoursPreMon"
        Me.txtIncHoursPreMon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIncHoursPreMon.Size = New System.Drawing.Size(75, 20)
        Me.txtIncHoursPreMon.TabIndex = 17
        '
        'txtIncHoursForMon
        '
        Me.txtIncHoursForMon.AcceptsReturn = True
        Me.txtIncHoursForMon.BackColor = System.Drawing.SystemColors.Window
        Me.txtIncHoursForMon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncHoursForMon.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIncHoursForMon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIncHoursForMon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIncHoursForMon.Location = New System.Drawing.Point(108, 28)
        Me.txtIncHoursForMon.MaxLength = 0
        Me.txtIncHoursForMon.Name = "txtIncHoursForMon"
        Me.txtIncHoursForMon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIncHoursForMon.Size = New System.Drawing.Size(75, 20)
        Me.txtIncHoursForMon.TabIndex = 15
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(108, 10)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(75, 17)
        Me.Label24.TabIndex = 102
        Me.Label24.Text = "Hours"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(186, 10)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(93, 17)
        Me.Label23.TabIndex = 101
        Me.Label23.Text = "Amount (Rs.)"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(5, 50)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(87, 14)
        Me.Label13.TabIndex = 89
        Me.Label13.Text = "Previous Month :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(14, 30)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(79, 14)
        Me.Label10.TabIndex = 88
        Me.Label10.Text = "For the Month :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkBonusPaid)
        Me.Frame4.Controls.Add(Me.chkMannualPerBonus)
        Me.Frame4.Controls.Add(Me.txtBonusPerForYear)
        Me.Frame4.Controls.Add(Me.txtBonusPerCurrYear)
        Me.Frame4.Controls.Add(Me.txtBonusCurrYear)
        Me.Frame4.Controls.Add(Me.txtBonusForYear)
        Me.Frame4.Controls.Add(Me.lblMannual)
        Me.Frame4.Controls.Add(Me.Label17)
        Me.Frame4.Controls.Add(Me.Label28)
        Me.Frame4.Controls.Add(Me.Label20)
        Me.Frame4.Controls.Add(Me.Label19)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(3, 139)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(289, 71)
        Me.Frame4.TabIndex = 95
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Bonus"
        '
        'chkBonusPaid
        '
        Me.chkBonusPaid.AutoSize = True
        Me.chkBonusPaid.BackColor = System.Drawing.SystemColors.Control
        Me.chkBonusPaid.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBonusPaid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBonusPaid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBonusPaid.Location = New System.Drawing.Point(236, 30)
        Me.chkBonusPaid.Name = "chkBonusPaid"
        Me.chkBonusPaid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBonusPaid.Size = New System.Drawing.Size(46, 18)
        Me.chkBonusPaid.TabIndex = 135
        Me.chkBonusPaid.Text = "Paid"
        Me.chkBonusPaid.UseVisualStyleBackColor = False
        '
        'chkMannualPerBonus
        '
        Me.chkMannualPerBonus.AutoSize = True
        Me.chkMannualPerBonus.BackColor = System.Drawing.SystemColors.Control
        Me.chkMannualPerBonus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMannualPerBonus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMannualPerBonus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMannualPerBonus.Location = New System.Drawing.Point(236, 52)
        Me.chkMannualPerBonus.Name = "chkMannualPerBonus"
        Me.chkMannualPerBonus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMannualPerBonus.Size = New System.Drawing.Size(15, 14)
        Me.chkMannualPerBonus.TabIndex = 133
        Me.chkMannualPerBonus.UseVisualStyleBackColor = False
        Me.chkMannualPerBonus.Visible = False
        '
        'txtBonusPerForYear
        '
        Me.txtBonusPerForYear.AcceptsReturn = True
        Me.txtBonusPerForYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtBonusPerForYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBonusPerForYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBonusPerForYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonusPerForYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBonusPerForYear.Location = New System.Drawing.Point(106, 28)
        Me.txtBonusPerForYear.MaxLength = 0
        Me.txtBonusPerForYear.Name = "txtBonusPerForYear"
        Me.txtBonusPerForYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBonusPerForYear.Size = New System.Drawing.Size(45, 20)
        Me.txtBonusPerForYear.TabIndex = 21
        '
        'txtBonusPerCurrYear
        '
        Me.txtBonusPerCurrYear.AcceptsReturn = True
        Me.txtBonusPerCurrYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtBonusPerCurrYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBonusPerCurrYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBonusPerCurrYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonusPerCurrYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBonusPerCurrYear.Location = New System.Drawing.Point(106, 48)
        Me.txtBonusPerCurrYear.MaxLength = 0
        Me.txtBonusPerCurrYear.Name = "txtBonusPerCurrYear"
        Me.txtBonusPerCurrYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBonusPerCurrYear.Size = New System.Drawing.Size(45, 20)
        Me.txtBonusPerCurrYear.TabIndex = 23
        '
        'txtBonusCurrYear
        '
        Me.txtBonusCurrYear.AcceptsReturn = True
        Me.txtBonusCurrYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtBonusCurrYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBonusCurrYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBonusCurrYear.Enabled = False
        Me.txtBonusCurrYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonusCurrYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBonusCurrYear.Location = New System.Drawing.Point(152, 48)
        Me.txtBonusCurrYear.MaxLength = 0
        Me.txtBonusCurrYear.Name = "txtBonusCurrYear"
        Me.txtBonusCurrYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBonusCurrYear.Size = New System.Drawing.Size(83, 20)
        Me.txtBonusCurrYear.TabIndex = 24
        '
        'txtBonusForYear
        '
        Me.txtBonusForYear.AcceptsReturn = True
        Me.txtBonusForYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtBonusForYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBonusForYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBonusForYear.Enabled = False
        Me.txtBonusForYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonusForYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBonusForYear.Location = New System.Drawing.Point(152, 28)
        Me.txtBonusForYear.MaxLength = 0
        Me.txtBonusForYear.Name = "txtBonusForYear"
        Me.txtBonusForYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBonusForYear.Size = New System.Drawing.Size(83, 20)
        Me.txtBonusForYear.TabIndex = 22
        '
        'lblMannual
        '
        Me.lblMannual.BackColor = System.Drawing.SystemColors.Control
        Me.lblMannual.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMannual.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMannual.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMannual.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMannual.Location = New System.Drawing.Point(236, 10)
        Me.lblMannual.Name = "lblMannual"
        Me.lblMannual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMannual.Size = New System.Drawing.Size(51, 17)
        Me.lblMannual.TabIndex = 134
        Me.lblMannual.Text = "Mannual"
        Me.lblMannual.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblMannual.Visible = False
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(106, 10)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(45, 17)
        Me.Label17.TabIndex = 111
        Me.Label17.Text = "%"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(152, 10)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(83, 17)
        Me.Label28.TabIndex = 106
        Me.Label28.Text = "Amount (Rs.)"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(6, 48)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(94, 14)
        Me.Label20.TabIndex = 97
        Me.Label20.Text = "For Current Year :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(25, 28)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(76, 14)
        Me.Label19.TabIndex = 96
        Me.Label19.Text = "For The Year :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtCompAmount)
        Me.Frame6.Controls.Add(Me.txtCompMonth)
        Me.Frame6.Controls.Add(Me.txtExGratiaAmount)
        Me.Frame6.Controls.Add(Me.txtExGratiaMonth)
        Me.Frame6.Controls.Add(Me.txtGratuityMon)
        Me.Frame6.Controls.Add(Me.txtNoticeMon)
        Me.Frame6.Controls.Add(Me.txtGratuityAmt)
        Me.Frame6.Controls.Add(Me.txtNoticeamt)
        Me.Frame6.Controls.Add(Me.Label49)
        Me.Frame6.Controls.Add(Me.Label48)
        Me.Frame6.Controls.Add(Me.Label36)
        Me.Frame6.Controls.Add(Me.Label29)
        Me.Frame6.Controls.Add(Me.Label22)
        Me.Frame6.Controls.Add(Me.Label21)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(295, -3)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(229, 119)
        Me.Frame6.TabIndex = 98
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Gratuity / Ex-Gratia"
        '
        'txtCompAmount
        '
        Me.txtCompAmount.AcceptsReturn = True
        Me.txtCompAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCompAmount.Location = New System.Drawing.Point(140, 92)
        Me.txtCompAmount.MaxLength = 0
        Me.txtCompAmount.Name = "txtCompAmount"
        Me.txtCompAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompAmount.Size = New System.Drawing.Size(85, 20)
        Me.txtCompAmount.TabIndex = 32
        '
        'txtCompMonth
        '
        Me.txtCompMonth.AcceptsReturn = True
        Me.txtCompMonth.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompMonth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompMonth.Enabled = False
        Me.txtCompMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCompMonth.Location = New System.Drawing.Point(94, 92)
        Me.txtCompMonth.MaxLength = 0
        Me.txtCompMonth.Name = "txtCompMonth"
        Me.txtCompMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompMonth.Size = New System.Drawing.Size(45, 20)
        Me.txtCompMonth.TabIndex = 31
        Me.txtCompMonth.Visible = False
        '
        'txtExGratiaAmount
        '
        Me.txtExGratiaAmount.AcceptsReturn = True
        Me.txtExGratiaAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtExGratiaAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExGratiaAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExGratiaAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExGratiaAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExGratiaAmount.Location = New System.Drawing.Point(140, 72)
        Me.txtExGratiaAmount.MaxLength = 0
        Me.txtExGratiaAmount.Name = "txtExGratiaAmount"
        Me.txtExGratiaAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExGratiaAmount.Size = New System.Drawing.Size(85, 20)
        Me.txtExGratiaAmount.TabIndex = 30
        '
        'txtExGratiaMonth
        '
        Me.txtExGratiaMonth.AcceptsReturn = True
        Me.txtExGratiaMonth.BackColor = System.Drawing.SystemColors.Window
        Me.txtExGratiaMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExGratiaMonth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExGratiaMonth.Enabled = False
        Me.txtExGratiaMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExGratiaMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExGratiaMonth.Location = New System.Drawing.Point(94, 72)
        Me.txtExGratiaMonth.MaxLength = 0
        Me.txtExGratiaMonth.Name = "txtExGratiaMonth"
        Me.txtExGratiaMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExGratiaMonth.Size = New System.Drawing.Size(45, 20)
        Me.txtExGratiaMonth.TabIndex = 29
        Me.txtExGratiaMonth.Visible = False
        '
        'txtGratuityMon
        '
        Me.txtGratuityMon.AcceptsReturn = True
        Me.txtGratuityMon.BackColor = System.Drawing.SystemColors.Window
        Me.txtGratuityMon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGratuityMon.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGratuityMon.Enabled = False
        Me.txtGratuityMon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGratuityMon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGratuityMon.Location = New System.Drawing.Point(94, 32)
        Me.txtGratuityMon.MaxLength = 0
        Me.txtGratuityMon.Name = "txtGratuityMon"
        Me.txtGratuityMon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGratuityMon.Size = New System.Drawing.Size(45, 20)
        Me.txtGratuityMon.TabIndex = 25
        '
        'txtNoticeMon
        '
        Me.txtNoticeMon.AcceptsReturn = True
        Me.txtNoticeMon.BackColor = System.Drawing.SystemColors.Window
        Me.txtNoticeMon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoticeMon.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNoticeMon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoticeMon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNoticeMon.Location = New System.Drawing.Point(94, 52)
        Me.txtNoticeMon.MaxLength = 0
        Me.txtNoticeMon.Name = "txtNoticeMon"
        Me.txtNoticeMon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNoticeMon.Size = New System.Drawing.Size(45, 20)
        Me.txtNoticeMon.TabIndex = 27
        '
        'txtGratuityAmt
        '
        Me.txtGratuityAmt.AcceptsReturn = True
        Me.txtGratuityAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtGratuityAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGratuityAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGratuityAmt.Enabled = False
        Me.txtGratuityAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGratuityAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGratuityAmt.Location = New System.Drawing.Point(140, 32)
        Me.txtGratuityAmt.MaxLength = 0
        Me.txtGratuityAmt.Name = "txtGratuityAmt"
        Me.txtGratuityAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGratuityAmt.Size = New System.Drawing.Size(85, 20)
        Me.txtGratuityAmt.TabIndex = 26
        '
        'txtNoticeamt
        '
        Me.txtNoticeamt.AcceptsReturn = True
        Me.txtNoticeamt.BackColor = System.Drawing.SystemColors.Window
        Me.txtNoticeamt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoticeamt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNoticeamt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoticeamt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNoticeamt.Location = New System.Drawing.Point(140, 52)
        Me.txtNoticeamt.MaxLength = 0
        Me.txtNoticeamt.Name = "txtNoticeamt"
        Me.txtNoticeamt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNoticeamt.Size = New System.Drawing.Size(85, 20)
        Me.txtNoticeamt.TabIndex = 28
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.BackColor = System.Drawing.SystemColors.Control
        Me.Label49.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label49.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label49.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label49.Location = New System.Drawing.Point(7, 94)
        Me.Label49.Name = "Label49"
        Me.Label49.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label49.Size = New System.Drawing.Size(81, 14)
        Me.Label49.TabIndex = 127
        Me.Label49.Text = "Compensation :"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(26, 74)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(64, 14)
        Me.Label48.TabIndex = 126
        Me.Label48.Text = "Ex - Gratia :"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label36
        '
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(94, 14)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(45, 17)
        Me.Label36.TabIndex = 108
        Me.Label36.Text = "Days"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label29
        '
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(140, 14)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(85, 17)
        Me.Label29.TabIndex = 107
        Me.Label29.Text = "Amount (Rs.)"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(41, 34)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(51, 14)
        Me.Label22.TabIndex = 100
        Me.Label22.Text = "Gratuity :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(23, 54)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(64, 14)
        Me.Label21.TabIndex = 99
        Me.Label21.Text = "Notice Pay :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtOthers)
        Me.Frame5.Controls.Add(Me.Label38)
        Me.Frame5.Controls.Add(Me.lblActGross)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(738, -2)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(209, 34)
        Me.Frame5.TabIndex = 109
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Others, If Any"
        '
        'txtOthers
        '
        Me.txtOthers.AcceptsReturn = True
        Me.txtOthers.BackColor = System.Drawing.SystemColors.Window
        Me.txtOthers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOthers.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOthers.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthers.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOthers.Location = New System.Drawing.Point(100, 10)
        Me.txtOthers.MaxLength = 0
        Me.txtOthers.Name = "txtOthers"
        Me.txtOthers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOthers.Size = New System.Drawing.Size(105, 20)
        Me.txtOthers.TabIndex = 40
        Me.txtOthers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(7, 12)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(73, 14)
        Me.Label38.TabIndex = 110
        Me.Label38.Text = "Others (Rs.) :"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblActGross
        '
        Me.lblActGross.BackColor = System.Drawing.SystemColors.Control
        Me.lblActGross.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblActGross.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActGross.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblActGross.Location = New System.Drawing.Point(16, 12)
        Me.lblActGross.Name = "lblActGross"
        Me.lblActGross.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblActGross.Size = New System.Drawing.Size(67, 13)
        Me.lblActGross.TabIndex = 116
        Me.lblActGross.Text = "lblActGross"
        Me.lblActGross.Visible = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.chkSuspension)
        Me.Frame9.Controls.Add(Me.txtSuspension)
        Me.Frame9.Controls.Add(Me.Label50)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(739, 37)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(209, 77)
        Me.Frame9.TabIndex = 128
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Suspension"
        '
        'chkSuspension
        '
        Me.chkSuspension.AutoSize = True
        Me.chkSuspension.BackColor = System.Drawing.SystemColors.Control
        Me.chkSuspension.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSuspension.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSuspension.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSuspension.Location = New System.Drawing.Point(52, 14)
        Me.chkSuspension.Name = "chkSuspension"
        Me.chkSuspension.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSuspension.Size = New System.Drawing.Size(135, 18)
        Me.chkSuspension.TabIndex = 41
        Me.chkSuspension.Text = "Suspension (Yes / No)"
        Me.chkSuspension.UseVisualStyleBackColor = False
        '
        'txtSuspension
        '
        Me.txtSuspension.AcceptsReturn = True
        Me.txtSuspension.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuspension.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuspension.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuspension.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuspension.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuspension.Location = New System.Drawing.Point(76, 38)
        Me.txtSuspension.MaxLength = 0
        Me.txtSuspension.Name = "txtSuspension"
        Me.txtSuspension.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuspension.Size = New System.Drawing.Size(49, 20)
        Me.txtSuspension.TabIndex = 42
        Me.txtSuspension.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.BackColor = System.Drawing.SystemColors.Control
        Me.Label50.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label50.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label50.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label50.Location = New System.Drawing.Point(54, 40)
        Me.Label50.Name = "Label50"
        Me.Label50.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label50.Size = New System.Drawing.Size(23, 14)
        Me.Label50.TabIndex = 129
        Me.Label50.Text = "% :"
        Me.Label50.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.chkTransfer)
        Me.Frame10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(952, -2)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(148, 52)
        Me.Frame10.TabIndex = 130
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Transfer to Other Unit"
        '
        'chkTransfer
        '
        Me.chkTransfer.AutoSize = True
        Me.chkTransfer.BackColor = System.Drawing.SystemColors.Control
        Me.chkTransfer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTransfer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransfer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTransfer.Location = New System.Drawing.Point(28, 25)
        Me.chkTransfer.Name = "chkTransfer"
        Me.chkTransfer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTransfer.Size = New System.Drawing.Size(68, 18)
        Me.chkTransfer.TabIndex = 43
        Me.chkTransfer.Text = "Transfer"
        Me.chkTransfer.UseVisualStyleBackColor = False
        '
        'txtBSalary
        '
        Me.txtBSalary.AcceptsReturn = True
        Me.txtBSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtBSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBSalary.Enabled = False
        Me.txtBSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBSalary.Location = New System.Drawing.Point(486, 86)
        Me.txtBSalary.MaxLength = 0
        Me.txtBSalary.Name = "txtBSalary"
        Me.txtBSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBSalary.Size = New System.Drawing.Size(101, 20)
        Me.txtBSalary.TabIndex = 2
        Me.txtBSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAtcBasic
        '
        Me.txtAtcBasic.AcceptsReturn = True
        Me.txtAtcBasic.BackColor = System.Drawing.SystemColors.Window
        Me.txtAtcBasic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAtcBasic.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAtcBasic.Enabled = False
        Me.txtAtcBasic.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAtcBasic.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAtcBasic.Location = New System.Drawing.Point(90, 86)
        Me.txtAtcBasic.MaxLength = 0
        Me.txtAtcBasic.Name = "txtAtcBasic"
        Me.txtAtcBasic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAtcBasic.Size = New System.Drawing.Size(101, 20)
        Me.txtAtcBasic.TabIndex = 0
        Me.txtAtcBasic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotOthers
        '
        Me.txtTotOthers.AcceptsReturn = True
        Me.txtTotOthers.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotOthers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotOthers.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotOthers.Enabled = False
        Me.txtTotOthers.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotOthers.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotOthers.Location = New System.Drawing.Point(472, 508)
        Me.txtTotOthers.MaxLength = 0
        Me.txtTotOthers.Name = "txtTotOthers"
        Me.txtTotOthers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotOthers.Size = New System.Drawing.Size(80, 20)
        Me.txtTotOthers.TabIndex = 46
        '
        'fraTop
        '
        Me.fraTop.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop.Controls.Add(Me.txtReason)
        Me.fraTop.Controls.Add(Me.chkAccountPosting)
        Me.fraTop.Controls.Add(Me.txtDOL)
        Me.fraTop.Controls.Add(Me.txtFName)
        Me.fraTop.Controls.Add(Me.cbodesignation)
        Me.fraTop.Controls.Add(Me.txtDOJ)
        Me.fraTop.Controls.Add(Me.txtEmpNo)
        Me.fraTop.Controls.Add(Me.TxtName)
        Me.fraTop.Controls.Add(Me.cmdSearch)
        Me.fraTop.Controls.Add(Me.Label45)
        Me.fraTop.Controls.Add(Me.Label8)
        Me.fraTop.Controls.Add(Me.Label9)
        Me.fraTop.Controls.Add(Me.Label7)
        Me.fraTop.Controls.Add(Me.lblDesg)
        Me.fraTop.Controls.Add(Me.Label3)
        Me.fraTop.Controls.Add(Me.Label1)
        Me.fraTop.Controls.Add(Me.Label12)
        Me.fraTop.Controls.Add(Me.Label30)
        Me.fraTop.Controls.Add(Me.Label31)
        Me.fraTop.Controls.Add(Me.Label32)
        Me.fraTop.Controls.Add(Me.Label33)
        Me.fraTop.Controls.Add(Me.Label34)
        Me.fraTop.Controls.Add(Me.Label35)
        Me.fraTop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop.Location = New System.Drawing.Point(0, 2)
        Me.fraTop.Name = "fraTop"
        Me.fraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop.Size = New System.Drawing.Size(1107, 81)
        Me.fraTop.TabIndex = 67
        Me.fraTop.TabStop = False
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReason.Location = New System.Drawing.Point(454, 56)
        Me.txtReason.MaxLength = 0
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(195, 20)
        Me.txtReason.TabIndex = 5
        '
        'chkAccountPosting
        '
        Me.chkAccountPosting.AutoSize = True
        Me.chkAccountPosting.BackColor = System.Drawing.SystemColors.Control
        Me.chkAccountPosting.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAccountPosting.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAccountPosting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAccountPosting.Location = New System.Drawing.Point(654, 58)
        Me.chkAccountPosting.Name = "chkAccountPosting"
        Me.chkAccountPosting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAccountPosting.Size = New System.Drawing.Size(81, 18)
        Me.chkAccountPosting.TabIndex = 6
        Me.chkAccountPosting.Text = "A/c Posting"
        Me.chkAccountPosting.UseVisualStyleBackColor = False
        '
        'txtDOL
        '
        Me.txtDOL.AcceptsReturn = True
        Me.txtDOL.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOL.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDOL.Location = New System.Drawing.Point(654, 34)
        Me.txtDOL.MaxLength = 0
        Me.txtDOL.Name = "txtDOL"
        Me.txtDOL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOL.Size = New System.Drawing.Size(89, 20)
        Me.txtDOL.TabIndex = 3
        '
        'txtFName
        '
        Me.txtFName.AcceptsReturn = True
        Me.txtFName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFName.Enabled = False
        Me.txtFName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFName.Location = New System.Drawing.Point(454, 12)
        Me.txtFName.MaxLength = 0
        Me.txtFName.Name = "txtFName"
        Me.txtFName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFName.Size = New System.Drawing.Size(289, 20)
        Me.txtFName.TabIndex = 1
        '
        'cbodesignation
        '
        Me.cbodesignation.BackColor = System.Drawing.SystemColors.Window
        Me.cbodesignation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cbodesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodesignation.Enabled = False
        Me.cbodesignation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbodesignation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cbodesignation.Location = New System.Drawing.Point(96, 56)
        Me.cbodesignation.Name = "cbodesignation"
        Me.cbodesignation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbodesignation.Size = New System.Drawing.Size(269, 22)
        Me.cbodesignation.TabIndex = 4
        '
        'txtDOJ
        '
        Me.txtDOJ.AcceptsReturn = True
        Me.txtDOJ.BackColor = System.Drawing.SystemColors.Window
        Me.txtDOJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDOJ.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDOJ.Enabled = False
        Me.txtDOJ.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDOJ.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDOJ.Location = New System.Drawing.Point(454, 34)
        Me.txtDOJ.MaxLength = 0
        Me.txtDOJ.Name = "txtDOJ"
        Me.txtDOJ.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDOJ.Size = New System.Drawing.Size(87, 20)
        Me.txtDOJ.TabIndex = 2
        '
        'txtEmpNo
        '
        Me.txtEmpNo.AcceptsReturn = True
        Me.txtEmpNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpNo.Location = New System.Drawing.Point(96, 12)
        Me.txtEmpNo.MaxLength = 0
        Me.txtEmpNo.Name = "txtEmpNo"
        Me.txtEmpNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpNo.Size = New System.Drawing.Size(117, 20)
        Me.txtEmpNo.TabIndex = 0
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Enabled = False
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(96, 34)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(269, 20)
        Me.TxtName.TabIndex = 3
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label45.Location = New System.Drawing.Point(398, 58)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(50, 14)
        Me.Label45.TabIndex = 117
        Me.Label45.Text = "Reason :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(566, 36)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(76, 14)
        Me.Label8.TabIndex = 76
        Me.Label8.Text = "Leaving Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label9.Location = New System.Drawing.Point(360, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(82, 14)
        Me.Label9.TabIndex = 75
        Me.Label9.Text = "Father's Name :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label7.Location = New System.Drawing.Point(13, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(69, 14)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Designation :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDesg
        '
        Me.lblDesg.BackColor = System.Drawing.SystemColors.Control
        Me.lblDesg.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDesg.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDesg.Location = New System.Drawing.Point(258, 64)
        Me.lblDesg.Name = "lblDesg"
        Me.lblDesg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDesg.Size = New System.Drawing.Size(181, 13)
        Me.lblDesg.TabIndex = 4
        Me.lblDesg.Text = "lblDesg"
        Me.lblDesg.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(370, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(71, 14)
        Me.Label3.TabIndex = 70
        Me.Label3.Text = "Joining Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(42, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 14)
        Me.Label1.TabIndex = 69
        Me.Label1.Text = "Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label12.Location = New System.Drawing.Point(21, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(61, 14)
        Me.Label12.TabIndex = 68
        Me.Label12.Text = "Emp Code :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Label30.TabIndex = 84
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
        Me.Label31.TabIndex = 83
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
        Me.Label32.TabIndex = 82
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
        Me.Label33.TabIndex = 81
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
        Me.Label34.TabIndex = 80
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
        Me.Label35.TabIndex = 79
        Me.Label35.Text = "Address"
        '
        'txtPaidDays
        '
        Me.txtPaidDays.AcceptsReturn = True
        Me.txtPaidDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaidDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaidDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidDays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPaidDays.Location = New System.Drawing.Point(288, 86)
        Me.txtPaidDays.MaxLength = 0
        Me.txtPaidDays.Name = "txtPaidDays"
        Me.txtPaidDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDays.Size = New System.Drawing.Size(49, 20)
        Me.txtPaidDays.TabIndex = 1
        Me.txtPaidDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtNetSalary.Location = New System.Drawing.Point(654, 508)
        Me.txtNetSalary.MaxLength = 0
        Me.txtNetSalary.Name = "txtNetSalary"
        Me.txtNetSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetSalary.Size = New System.Drawing.Size(80, 20)
        Me.txtNetSalary.TabIndex = 47
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
        Me.txtDeduction.Location = New System.Drawing.Point(296, 508)
        Me.txtDeduction.MaxLength = 0
        Me.txtDeduction.Name = "txtDeduction"
        Me.txtDeduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeduction.Size = New System.Drawing.Size(80, 20)
        Me.txtDeduction.TabIndex = 45
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
        Me.txtGSalary.Location = New System.Drawing.Point(112, 508)
        Me.txtGSalary.MaxLength = 0
        Me.txtGSalary.Name = "txtGSalary"
        Me.txtGSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSalary.Size = New System.Drawing.Size(76, 20)
        Me.txtGSalary.TabIndex = 44
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.txtChqNo)
        Me.Frame8.Controls.Add(Me.txtBankName)
        Me.Frame8.Controls.Add(Me.txtRemarks)
        Me.Frame8.Controls.Add(Me.Label47)
        Me.Frame8.Controls.Add(Me.Label46)
        Me.Frame8.Controls.Add(Me.Label37)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(1, 534)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(749, 45)
        Me.Frame8.TabIndex = 118
        Me.Frame8.TabStop = False
        '
        'txtChqNo
        '
        Me.txtChqNo.AcceptsReturn = True
        Me.txtChqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChqNo.Location = New System.Drawing.Point(120, 10)
        Me.txtChqNo.MaxLength = 0
        Me.txtChqNo.Multiline = True
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqNo.Size = New System.Drawing.Size(127, 31)
        Me.txtChqNo.TabIndex = 48
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBankName.Location = New System.Drawing.Point(330, 10)
        Me.txtBankName.MaxLength = 0
        Me.txtBankName.Multiline = True
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(143, 31)
        Me.txtBankName.TabIndex = 49
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(544, 10)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(195, 31)
        Me.txtRemarks.TabIndex = 50
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.BackColor = System.Drawing.SystemColors.Control
        Me.Label47.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label47.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label47.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label47.Location = New System.Drawing.Point(4, 18)
        Me.Label47.Name = "Label47"
        Me.Label47.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label47.Size = New System.Drawing.Size(101, 14)
        Me.Label47.TabIndex = 121
        Me.Label47.Text = "Cheque No && Date :"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(252, 18)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(67, 14)
        Me.Label46.TabIndex = 120
        Me.Label46.Text = "Bank Name :"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Menu
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label37.Location = New System.Drawing.Point(482, 18)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(55, 14)
        Me.Label37.TabIndex = 119
        Me.Label37.Text = "Remarks :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(374, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(117, 19)
        Me.Label4.TabIndex = 77
        Me.Label4.Text = "Paid Basic Salary :"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(4, 88)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(111, 19)
        Me.Label6.TabIndex = 74
        Me.Label6.Text = "Basic Salary :"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(396, 510)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(85, 19)
        Me.Label5.TabIndex = 73
        Me.Label5.Text = "Others :"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(216, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(73, 17)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Paid Days :"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(562, 510)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(91, 19)
        Me.Label43.TabIndex = 64
        Me.Label43.Text = "Net Salary :"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label41.Location = New System.Drawing.Point(210, 510)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(85, 19)
        Me.Label41.TabIndex = 63
        Me.Label41.Text = "Deduction :"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(4, 510)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(107, 19)
        Me.Label15.TabIndex = 62
        Me.Label15.Text = "Gross Salary :"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(1107, 573)
        Me.SprdView.TabIndex = 66
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdEmailExternal)
        Me.FraMovement.Controls.Add(Me.cmdEMailAccounts)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.cmdPolicyPreview)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdAccountPosting)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 569)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1107, 51)
        Me.FraMovement.TabIndex = 65
        Me.FraMovement.TabStop = False
        '
        'cmdEmailExternal
        '
        Me.cmdEmailExternal.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEmailExternal.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEmailExternal.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEmailExternal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEmailExternal.Location = New System.Drawing.Point(817, 10)
        Me.cmdEmailExternal.Name = "cmdEmailExternal"
        Me.cmdEmailExternal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEmailExternal.Size = New System.Drawing.Size(73, 37)
        Me.cmdEmailExternal.TabIndex = 128
        Me.cmdEmailExternal.Text = "eMail External"
        Me.cmdEmailExternal.UseVisualStyleBackColor = False
        '
        'cmdEMailAccounts
        '
        Me.cmdEMailAccounts.BackColor = System.Drawing.SystemColors.Control
        Me.cmdEMailAccounts.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEMailAccounts.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEMailAccounts.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEMailAccounts.Location = New System.Drawing.Point(745, 10)
        Me.cmdEMailAccounts.Name = "cmdEMailAccounts"
        Me.cmdEMailAccounts.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEMailAccounts.Size = New System.Drawing.Size(73, 37)
        Me.cmdEMailAccounts.TabIndex = 127
        Me.cmdEMailAccounts.Text = "eMail Accounts"
        Me.cmdEMailAccounts.UseVisualStyleBackColor = False
        '
        'cmdPolicyPreview
        '
        Me.cmdPolicyPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPolicyPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPolicyPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPolicyPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPolicyPreview.Image = CType(resources.GetObject("cmdPolicyPreview.Image"), System.Drawing.Image)
        Me.cmdPolicyPreview.Location = New System.Drawing.Point(673, 10)
        Me.cmdPolicyPreview.Name = "cmdPolicyPreview"
        Me.cmdPolicyPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPolicyPreview.Size = New System.Drawing.Size(73, 37)
        Me.cmdPolicyPreview.TabIndex = 125
        Me.cmdPolicyPreview.Text = "Policy Letter Preview"
        Me.cmdPolicyPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPolicyPreview.UseVisualStyleBackColor = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(601, 10)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(73, 37)
        Me.cmdPreview.TabIndex = 57
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(313, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(73, 37)
        Me.cmdSavePrint.TabIndex = 54
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 126
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
        Me.Label44.TabIndex = 0
        Me.Label44.Text = "Sex :"
        '
        'frmFFSettlement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Label44)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFFSettlement"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Full & Final Settlement"
        Me.FraMain.ResumeLayout(False)
        Me.FraMain.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me.Frame10.ResumeLayout(False)
        Me.Frame10.PerformLayout()
        Me.fraTop.ResumeLayout(False)
        Me.fraTop.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents cmdEmailExternal As Button
    Public WithEvents cmdEMailAccounts As Button
#End Region
End Class