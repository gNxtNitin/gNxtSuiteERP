Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCustWiseWeldCostMaster
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


        'VB6_AddADODataBinding()
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
	Public WithEvents txtNetWeldCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotMCCost As System.Windows.Forms.TextBox
	Public WithEvents txtSmokeCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotConsCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotLabourCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotPowerCost As System.Windows.Forms.TextBox
	Public WithEvents txtTotCO2Cost As System.Windows.Forms.TextBox
	Public WithEvents txtTotMIGCost As System.Windows.Forms.TextBox
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label52 As System.Windows.Forms.Label
	Public WithEvents Label51 As System.Windows.Forms.Label
	Public WithEvents Label37 As System.Windows.Forms.Label
	Public WithEvents Label28 As System.Windows.Forms.Label
	Public WithEvents Label36 As System.Windows.Forms.Label
	Public WithEvents Label32 As System.Windows.Forms.Label
	Public WithEvents Label31 As System.Windows.Forms.Label
	Public WithEvents Label30 As System.Windows.Forms.Label
	Public WithEvents Label29 As System.Windows.Forms.Label
	Public WithEvents Label27 As System.Windows.Forms.Label
	Public WithEvents Label22 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
	Public WithEvents txtSuppCustName As System.Windows.Forms.TextBox
	Public WithEvents txtSuppCustCode As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchCust As System.Windows.Forms.Button
	Public WithEvents cmdSearchWEF As System.Windows.Forms.Button
	Public WithEvents txtWEF As System.Windows.Forms.TextBox
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents lblMKey As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents chkStatus As System.Windows.Forms.CheckBox
	Public WithEvents txtRemarks As System.Windows.Forms.TextBox
	Public WithEvents txtAppBy As System.Windows.Forms.TextBox
	Public WithEvents txtPrepBy As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchPrepBy As System.Windows.Forms.Button
	Public WithEvents cmdSearchAppBy As System.Windows.Forms.Button
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents lblAppBy As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents lblPrepBy As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents fraCosting As System.Windows.Forms.GroupBox
	Public WithEvents SprdWeld As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents SprdCO2 As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents SprdMC As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents SprdPower As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents SprdLabour As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
	Public WithEvents SprdCons As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
	Public WithEvents SSTab1 As System.Windows.Forms.TabControl
	Public WithEvents fraBase As System.Windows.Forms.GroupBox
	Public WithEvents ADataGrid As VB6.ADODC
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents cmdAmend As System.Windows.Forms.Button
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCustWiseWeldCostMaster))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.fraBase = New System.Windows.Forms.GroupBox
		Me.Frame2 = New System.Windows.Forms.GroupBox
		Me.txtNetWeldCost = New System.Windows.Forms.TextBox
		Me.txtTotMCCost = New System.Windows.Forms.TextBox
		Me.txtSmokeCost = New System.Windows.Forms.TextBox
		Me.txtTotConsCost = New System.Windows.Forms.TextBox
		Me.txtTotLabourCost = New System.Windows.Forms.TextBox
		Me.txtTotPowerCost = New System.Windows.Forms.TextBox
		Me.txtTotCO2Cost = New System.Windows.Forms.TextBox
		Me.txtTotMIGCost = New System.Windows.Forms.TextBox
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label52 = New System.Windows.Forms.Label
		Me.Label51 = New System.Windows.Forms.Label
		Me.Label37 = New System.Windows.Forms.Label
		Me.Label28 = New System.Windows.Forms.Label
		Me.Label36 = New System.Windows.Forms.Label
		Me.Label32 = New System.Windows.Forms.Label
		Me.Label31 = New System.Windows.Forms.Label
		Me.Label30 = New System.Windows.Forms.Label
		Me.Label29 = New System.Windows.Forms.Label
		Me.Label27 = New System.Windows.Forms.Label
		Me.Label22 = New System.Windows.Forms.Label
		Me.Label21 = New System.Windows.Forms.Label
		Me.Label20 = New System.Windows.Forms.Label
		Me.Label19 = New System.Windows.Forms.Label
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.txtAmendNo = New System.Windows.Forms.TextBox
		Me.txtSuppCustName = New System.Windows.Forms.TextBox
		Me.txtSuppCustCode = New System.Windows.Forms.TextBox
		Me.cmdSearchCust = New System.Windows.Forms.Button
		Me.cmdSearchWEF = New System.Windows.Forms.Button
		Me.txtWEF = New System.Windows.Forms.TextBox
		Me.Label6 = New System.Windows.Forms.Label
		Me.lblMKey = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Label9 = New System.Windows.Forms.Label
		Me.fraCosting = New System.Windows.Forms.GroupBox
		Me.chkStatus = New System.Windows.Forms.CheckBox
		Me.txtRemarks = New System.Windows.Forms.TextBox
		Me.txtAppBy = New System.Windows.Forms.TextBox
		Me.txtPrepBy = New System.Windows.Forms.TextBox
		Me.cmdSearchPrepBy = New System.Windows.Forms.Button
		Me.cmdSearchAppBy = New System.Windows.Forms.Button
		Me.Label14 = New System.Windows.Forms.Label
		Me.lblAppBy = New System.Windows.Forms.Label
		Me.Label13 = New System.Windows.Forms.Label
		Me.lblPrepBy = New System.Windows.Forms.Label
		Me.Label5 = New System.Windows.Forms.Label
		Me.SSTab1 = New System.Windows.Forms.TabControl
		Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage
		Me.SprdWeld = New AxFPSpreadADO.AxfpSpread
		Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage
		Me.SprdCO2 = New AxFPSpreadADO.AxfpSpread
		Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage
		Me.SprdMC = New AxFPSpreadADO.AxfpSpread
		Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage
		Me.SprdPower = New AxFPSpreadADO.AxfpSpread
		Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage
		Me.SprdLabour = New AxFPSpreadADO.AxfpSpread
		Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage
		Me.SprdCons = New AxFPSpreadADO.AxfpSpread
		Me.ADataGrid = New VB6.ADODC
		Me.SprdView = New AxFPSpreadADO.AxfpSpread
		Me.Frame3 = New System.Windows.Forms.GroupBox
		Me.CmdClose = New System.Windows.Forms.Button
		Me.CmdView = New System.Windows.Forms.Button
		Me.CmdPreview = New System.Windows.Forms.Button
		Me.Report1 = New AxCrystal.AxCrystalReport
		Me.cmdPrint = New System.Windows.Forms.Button
		Me.CmdDelete = New System.Windows.Forms.Button
		Me.cmdSavePrint = New System.Windows.Forms.Button
		Me.CmdSave = New System.Windows.Forms.Button
		Me.cmdAmend = New System.Windows.Forms.Button
		Me.CmdModify = New System.Windows.Forms.Button
		Me.CmdAdd = New System.Windows.Forms.Button
		Me.fraBase.SuspendLayout()
		Me.Frame2.SuspendLayout()
		Me.Frame1.SuspendLayout()
		Me.fraCosting.SuspendLayout()
		Me.SSTab1.SuspendLayout()
		Me._SSTab1_TabPage0.SuspendLayout()
		Me._SSTab1_TabPage1.SuspendLayout()
		Me._SSTab1_TabPage2.SuspendLayout()
		Me._SSTab1_TabPage3.SuspendLayout()
		Me._SSTab1_TabPage4.SuspendLayout()
		Me._SSTab1_TabPage5.SuspendLayout()
		Me.Frame3.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.SprdWeld, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdCO2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdMC, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdPower, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdLabour, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdCons, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Text = "Customer Wise Welding Cost Master"
		Me.ClientSize = New System.Drawing.Size(751, 479)
		Me.Location = New System.Drawing.Point(4, 23)
		Me.ForeColor = System.Drawing.Color.Black
		Me.Icon = CType(resources.GetObject("frmCustWiseWeldCostMaster.Icon"), System.Drawing.Icon)
		Me.KeyPreview = True
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmCustWiseWeldCostMaster"
		Me.fraBase.Size = New System.Drawing.Size(751, 441)
		Me.fraBase.Location = New System.Drawing.Point(0, -4)
		Me.fraBase.TabIndex = 38
		Me.fraBase.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.fraBase.BackColor = System.Drawing.SystemColors.Control
		Me.fraBase.Enabled = True
		Me.fraBase.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraBase.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraBase.Visible = True
		Me.fraBase.Padding = New System.Windows.Forms.Padding(0)
		Me.fraBase.Name = "fraBase"
		Me.Frame2.Size = New System.Drawing.Size(745, 86)
		Me.Frame2.Location = New System.Drawing.Point(2, 304)
		Me.Frame2.TabIndex = 51
		Me.Frame2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame2.BackColor = System.Drawing.SystemColors.Control
		Me.Frame2.Enabled = True
		Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame2.Visible = True
		Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame2.Name = "Frame2"
		Me.txtNetWeldCost.AutoSize = False
		Me.txtNetWeldCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtNetWeldCost.Enabled = False
		Me.txtNetWeldCost.Size = New System.Drawing.Size(75, 19)
		Me.txtNetWeldCost.Location = New System.Drawing.Point(663, 60)
		Me.txtNetWeldCost.TabIndex = 18
		Me.txtNetWeldCost.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtNetWeldCost.AcceptsReturn = True
		Me.txtNetWeldCost.BackColor = System.Drawing.SystemColors.Window
		Me.txtNetWeldCost.CausesValidation = True
		Me.txtNetWeldCost.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtNetWeldCost.HideSelection = True
		Me.txtNetWeldCost.ReadOnly = False
		Me.txtNetWeldCost.Maxlength = 0
		Me.txtNetWeldCost.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtNetWeldCost.MultiLine = False
		Me.txtNetWeldCost.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtNetWeldCost.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtNetWeldCost.TabStop = True
		Me.txtNetWeldCost.Visible = True
		Me.txtNetWeldCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtNetWeldCost.Name = "txtNetWeldCost"
		Me.txtTotMCCost.AutoSize = False
		Me.txtTotMCCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtTotMCCost.Enabled = False
		Me.txtTotMCCost.Size = New System.Drawing.Size(75, 19)
		Me.txtTotMCCost.Location = New System.Drawing.Point(663, 11)
		Me.txtTotMCCost.TabIndex = 13
		Me.txtTotMCCost.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTotMCCost.AcceptsReturn = True
		Me.txtTotMCCost.BackColor = System.Drawing.SystemColors.Window
		Me.txtTotMCCost.CausesValidation = True
		Me.txtTotMCCost.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTotMCCost.HideSelection = True
		Me.txtTotMCCost.ReadOnly = False
		Me.txtTotMCCost.Maxlength = 0
		Me.txtTotMCCost.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTotMCCost.MultiLine = False
		Me.txtTotMCCost.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTotMCCost.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTotMCCost.TabStop = True
		Me.txtTotMCCost.Visible = True
		Me.txtTotMCCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTotMCCost.Name = "txtTotMCCost"
		Me.txtSmokeCost.AutoSize = False
		Me.txtSmokeCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtSmokeCost.Size = New System.Drawing.Size(75, 19)
		Me.txtSmokeCost.Location = New System.Drawing.Point(145, 59)
		Me.txtSmokeCost.TabIndex = 17
		Me.txtSmokeCost.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtSmokeCost.AcceptsReturn = True
		Me.txtSmokeCost.BackColor = System.Drawing.SystemColors.Window
		Me.txtSmokeCost.CausesValidation = True
		Me.txtSmokeCost.Enabled = True
		Me.txtSmokeCost.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtSmokeCost.HideSelection = True
		Me.txtSmokeCost.ReadOnly = False
		Me.txtSmokeCost.Maxlength = 0
		Me.txtSmokeCost.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtSmokeCost.MultiLine = False
		Me.txtSmokeCost.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtSmokeCost.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtSmokeCost.TabStop = True
		Me.txtSmokeCost.Visible = True
		Me.txtSmokeCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtSmokeCost.Name = "txtSmokeCost"
		Me.txtTotConsCost.AutoSize = False
		Me.txtTotConsCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtTotConsCost.Enabled = False
		Me.txtTotConsCost.Size = New System.Drawing.Size(75, 19)
		Me.txtTotConsCost.Location = New System.Drawing.Point(663, 35)
		Me.txtTotConsCost.TabIndex = 16
		Me.txtTotConsCost.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTotConsCost.AcceptsReturn = True
		Me.txtTotConsCost.BackColor = System.Drawing.SystemColors.Window
		Me.txtTotConsCost.CausesValidation = True
		Me.txtTotConsCost.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTotConsCost.HideSelection = True
		Me.txtTotConsCost.ReadOnly = False
		Me.txtTotConsCost.Maxlength = 0
		Me.txtTotConsCost.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTotConsCost.MultiLine = False
		Me.txtTotConsCost.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTotConsCost.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTotConsCost.TabStop = True
		Me.txtTotConsCost.Visible = True
		Me.txtTotConsCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTotConsCost.Name = "txtTotConsCost"
		Me.txtTotLabourCost.AutoSize = False
		Me.txtTotLabourCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtTotLabourCost.Enabled = False
		Me.txtTotLabourCost.Size = New System.Drawing.Size(75, 19)
		Me.txtTotLabourCost.Location = New System.Drawing.Point(393, 35)
		Me.txtTotLabourCost.TabIndex = 15
		Me.txtTotLabourCost.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTotLabourCost.AcceptsReturn = True
		Me.txtTotLabourCost.BackColor = System.Drawing.SystemColors.Window
		Me.txtTotLabourCost.CausesValidation = True
		Me.txtTotLabourCost.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTotLabourCost.HideSelection = True
		Me.txtTotLabourCost.ReadOnly = False
		Me.txtTotLabourCost.Maxlength = 0
		Me.txtTotLabourCost.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTotLabourCost.MultiLine = False
		Me.txtTotLabourCost.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTotLabourCost.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTotLabourCost.TabStop = True
		Me.txtTotLabourCost.Visible = True
		Me.txtTotLabourCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTotLabourCost.Name = "txtTotLabourCost"
		Me.txtTotPowerCost.AutoSize = False
		Me.txtTotPowerCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtTotPowerCost.Enabled = False
		Me.txtTotPowerCost.Size = New System.Drawing.Size(75, 19)
		Me.txtTotPowerCost.Location = New System.Drawing.Point(145, 35)
		Me.txtTotPowerCost.TabIndex = 14
		Me.txtTotPowerCost.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTotPowerCost.AcceptsReturn = True
		Me.txtTotPowerCost.BackColor = System.Drawing.SystemColors.Window
		Me.txtTotPowerCost.CausesValidation = True
		Me.txtTotPowerCost.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTotPowerCost.HideSelection = True
		Me.txtTotPowerCost.ReadOnly = False
		Me.txtTotPowerCost.Maxlength = 0
		Me.txtTotPowerCost.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTotPowerCost.MultiLine = False
		Me.txtTotPowerCost.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTotPowerCost.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTotPowerCost.TabStop = True
		Me.txtTotPowerCost.Visible = True
		Me.txtTotPowerCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTotPowerCost.Name = "txtTotPowerCost"
		Me.txtTotCO2Cost.AutoSize = False
		Me.txtTotCO2Cost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtTotCO2Cost.Enabled = False
		Me.txtTotCO2Cost.Size = New System.Drawing.Size(75, 19)
		Me.txtTotCO2Cost.Location = New System.Drawing.Point(393, 11)
		Me.txtTotCO2Cost.TabIndex = 12
		Me.txtTotCO2Cost.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTotCO2Cost.AcceptsReturn = True
		Me.txtTotCO2Cost.BackColor = System.Drawing.SystemColors.Window
		Me.txtTotCO2Cost.CausesValidation = True
		Me.txtTotCO2Cost.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTotCO2Cost.HideSelection = True
		Me.txtTotCO2Cost.ReadOnly = False
		Me.txtTotCO2Cost.Maxlength = 0
		Me.txtTotCO2Cost.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTotCO2Cost.MultiLine = False
		Me.txtTotCO2Cost.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTotCO2Cost.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTotCO2Cost.TabStop = True
		Me.txtTotCO2Cost.Visible = True
		Me.txtTotCO2Cost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTotCO2Cost.Name = "txtTotCO2Cost"
		Me.txtTotMIGCost.AutoSize = False
		Me.txtTotMIGCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
		Me.txtTotMIGCost.Enabled = False
		Me.txtTotMIGCost.Size = New System.Drawing.Size(75, 19)
		Me.txtTotMIGCost.Location = New System.Drawing.Point(145, 11)
		Me.txtTotMIGCost.TabIndex = 11
		Me.txtTotMIGCost.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtTotMIGCost.AcceptsReturn = True
		Me.txtTotMIGCost.BackColor = System.Drawing.SystemColors.Window
		Me.txtTotMIGCost.CausesValidation = True
		Me.txtTotMIGCost.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtTotMIGCost.HideSelection = True
		Me.txtTotMIGCost.ReadOnly = False
		Me.txtTotMIGCost.Maxlength = 0
		Me.txtTotMIGCost.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtTotMIGCost.MultiLine = False
		Me.txtTotMIGCost.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtTotMIGCost.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtTotMIGCost.TabStop = True
		Me.txtTotMIGCost.Visible = True
		Me.txtTotMIGCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtTotMIGCost.Name = "txtTotMIGCost"
		Me.Label2.Text = "Net Weld Cost :"
		Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.Size = New System.Drawing.Size(91, 13)
		Me.Label2.Location = New System.Drawing.Point(572, 63)
		Me.Label2.TabIndex = 66
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
		Me.Label52.Text = "M/c Cost :"
		Me.Label52.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label52.Size = New System.Drawing.Size(61, 13)
		Me.Label52.Location = New System.Drawing.Point(578, 14)
		Me.Label52.TabIndex = 65
		Me.Label52.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label52.BackColor = System.Drawing.SystemColors.Control
		Me.Label52.Enabled = True
		Me.Label52.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label52.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label52.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label52.UseMnemonic = True
		Me.Label52.Visible = True
		Me.Label52.AutoSize = True
		Me.Label52.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label52.Name = "Label52"
		Me.Label51.Text = "III."
		Me.Label51.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label51.Size = New System.Drawing.Size(17, 13)
		Me.Label51.Location = New System.Drawing.Point(560, 14)
		Me.Label51.TabIndex = 64
		Me.Label51.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label51.BackColor = System.Drawing.SystemColors.Control
		Me.Label51.Enabled = True
		Me.Label51.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label51.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label51.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label51.UseMnemonic = True
		Me.Label51.Visible = True
		Me.Label51.AutoSize = True
		Me.Label51.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label51.Name = "Label51"
		Me.Label37.Text = "VII."
		Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label37.Size = New System.Drawing.Size(21, 13)
		Me.Label37.Location = New System.Drawing.Point(8, 62)
		Me.Label37.TabIndex = 63
		Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label37.BackColor = System.Drawing.SystemColors.Control
		Me.Label37.Enabled = True
		Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label37.UseMnemonic = True
		Me.Label37.Visible = True
		Me.Label37.AutoSize = True
		Me.Label37.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label37.Name = "Label37"
		Me.Label28.Text = "Smoke Extraction:"
		Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label28.Size = New System.Drawing.Size(104, 13)
		Me.Label28.Location = New System.Drawing.Point(40, 62)
		Me.Label28.TabIndex = 62
		Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label28.BackColor = System.Drawing.SystemColors.Control
		Me.Label28.Enabled = True
		Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label28.UseMnemonic = True
		Me.Label28.Visible = True
		Me.Label28.AutoSize = True
		Me.Label28.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label28.Name = "Label28"
		Me.Label36.Text = "VI."
		Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label36.Size = New System.Drawing.Size(17, 13)
		Me.Label36.Location = New System.Drawing.Point(560, 38)
		Me.Label36.TabIndex = 61
		Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label36.BackColor = System.Drawing.SystemColors.Control
		Me.Label36.Enabled = True
		Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label36.UseMnemonic = True
		Me.Label36.Visible = True
		Me.Label36.AutoSize = True
		Me.Label36.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label36.Name = "Label36"
		Me.Label32.Text = "V."
		Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label32.Size = New System.Drawing.Size(13, 13)
		Me.Label32.Location = New System.Drawing.Point(296, 38)
		Me.Label32.TabIndex = 60
		Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label32.BackColor = System.Drawing.SystemColors.Control
		Me.Label32.Enabled = True
		Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label32.UseMnemonic = True
		Me.Label32.Visible = True
		Me.Label32.AutoSize = True
		Me.Label32.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label32.Name = "Label32"
		Me.Label31.Text = "IV."
		Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label31.Size = New System.Drawing.Size(17, 13)
		Me.Label31.Location = New System.Drawing.Point(8, 38)
		Me.Label31.TabIndex = 59
		Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label31.BackColor = System.Drawing.SystemColors.Control
		Me.Label31.Enabled = True
		Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label31.UseMnemonic = True
		Me.Label31.Visible = True
		Me.Label31.AutoSize = True
		Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label31.Name = "Label31"
		Me.Label30.Text = "II."
		Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label30.Size = New System.Drawing.Size(13, 13)
		Me.Label30.Location = New System.Drawing.Point(296, 14)
		Me.Label30.TabIndex = 58
		Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label30.BackColor = System.Drawing.SystemColors.Control
		Me.Label30.Enabled = True
		Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label30.UseMnemonic = True
		Me.Label30.Visible = True
		Me.Label30.AutoSize = True
		Me.Label30.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label30.Name = "Label30"
		Me.Label29.Text = "MIG Wire Cost :"
		Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label29.Size = New System.Drawing.Size(91, 13)
		Me.Label29.Location = New System.Drawing.Point(40, 14)
		Me.Label29.TabIndex = 57
		Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label29.BackColor = System.Drawing.SystemColors.Control
		Me.Label29.Enabled = True
		Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label29.UseMnemonic = True
		Me.Label29.Visible = True
		Me.Label29.AutoSize = True
		Me.Label29.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label29.Name = "Label29"
		Me.Label27.Text = "Consumables :"
		Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label27.Size = New System.Drawing.Size(83, 13)
		Me.Label27.Location = New System.Drawing.Point(578, 38)
		Me.Label27.TabIndex = 56
		Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label27.BackColor = System.Drawing.SystemColors.Control
		Me.Label27.Enabled = True
		Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label27.UseMnemonic = True
		Me.Label27.Visible = True
		Me.Label27.AutoSize = True
		Me.Label27.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label27.Name = "Label27"
		Me.Label22.Text = "Labour Cost :"
		Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label22.Size = New System.Drawing.Size(77, 13)
		Me.Label22.Location = New System.Drawing.Point(314, 38)
		Me.Label22.TabIndex = 55
		Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label22.BackColor = System.Drawing.SystemColors.Control
		Me.Label22.Enabled = True
		Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label22.UseMnemonic = True
		Me.Label22.Visible = True
		Me.Label22.AutoSize = True
		Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label22.Name = "Label22"
		Me.Label21.Text = "Power Cost :"
		Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label21.Size = New System.Drawing.Size(73, 13)
		Me.Label21.Location = New System.Drawing.Point(40, 38)
		Me.Label21.TabIndex = 54
		Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label21.BackColor = System.Drawing.SystemColors.Control
		Me.Label21.Enabled = True
		Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label21.UseMnemonic = True
		Me.Label21.Visible = True
		Me.Label21.AutoSize = True
		Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label21.Name = "Label21"
		Me.Label20.Text = "CO2 Cost :"
		Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label20.Size = New System.Drawing.Size(62, 13)
		Me.Label20.Location = New System.Drawing.Point(314, 14)
		Me.Label20.TabIndex = 53
		Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label20.BackColor = System.Drawing.SystemColors.Control
		Me.Label20.Enabled = True
		Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label20.UseMnemonic = True
		Me.Label20.Visible = True
		Me.Label20.AutoSize = True
		Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label20.Name = "Label20"
		Me.Label19.Text = "I."
		Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label19.Size = New System.Drawing.Size(9, 13)
		Me.Label19.Location = New System.Drawing.Point(8, 14)
		Me.Label19.TabIndex = 52
		Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label19.BackColor = System.Drawing.SystemColors.Control
		Me.Label19.Enabled = True
		Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label19.UseMnemonic = True
		Me.Label19.Visible = True
		Me.Label19.AutoSize = True
		Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label19.Name = "Label19"
		Me.Frame1.Size = New System.Drawing.Size(751, 55)
		Me.Frame1.Location = New System.Drawing.Point(0, 2)
		Me.Frame1.TabIndex = 44
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame1.Name = "Frame1"
		Me.txtAmendNo.AutoSize = False
		Me.txtAmendNo.Enabled = False
		Me.txtAmendNo.Size = New System.Drawing.Size(81, 19)
		Me.txtAmendNo.Location = New System.Drawing.Point(283, 30)
		Me.txtAmendNo.TabIndex = 5
		Me.ToolTip1.SetToolTip(Me.txtAmendNo, "Press F1 For Help")
		Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAmendNo.AcceptsReturn = True
		Me.txtAmendNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
		Me.txtAmendNo.CausesValidation = True
		Me.txtAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAmendNo.HideSelection = True
		Me.txtAmendNo.ReadOnly = False
		Me.txtAmendNo.Maxlength = 0
		Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAmendNo.MultiLine = False
		Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAmendNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAmendNo.TabStop = True
		Me.txtAmendNo.Visible = True
		Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtAmendNo.Name = "txtAmendNo"
		Me.txtSuppCustName.AutoSize = False
		Me.txtSuppCustName.Enabled = False
		Me.txtSuppCustName.Size = New System.Drawing.Size(353, 19)
		Me.txtSuppCustName.Location = New System.Drawing.Point(208, 10)
		Me.txtSuppCustName.TabIndex = 2
		Me.ToolTip1.SetToolTip(Me.txtSuppCustName, "Press F1 For Help")
		Me.txtSuppCustName.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtSuppCustName.AcceptsReturn = True
		Me.txtSuppCustName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtSuppCustName.BackColor = System.Drawing.SystemColors.Window
		Me.txtSuppCustName.CausesValidation = True
		Me.txtSuppCustName.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtSuppCustName.HideSelection = True
		Me.txtSuppCustName.ReadOnly = False
		Me.txtSuppCustName.Maxlength = 0
		Me.txtSuppCustName.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtSuppCustName.MultiLine = False
		Me.txtSuppCustName.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtSuppCustName.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtSuppCustName.TabStop = True
		Me.txtSuppCustName.Visible = True
		Me.txtSuppCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtSuppCustName.Name = "txtSuppCustName"
		Me.txtSuppCustCode.AutoSize = False
		Me.txtSuppCustCode.Size = New System.Drawing.Size(81, 19)
		Me.txtSuppCustCode.Location = New System.Drawing.Point(98, 10)
		Me.txtSuppCustCode.TabIndex = 0
		Me.ToolTip1.SetToolTip(Me.txtSuppCustCode, "Press F1 For Help")
		Me.txtSuppCustCode.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtSuppCustCode.AcceptsReturn = True
		Me.txtSuppCustCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtSuppCustCode.BackColor = System.Drawing.SystemColors.Window
		Me.txtSuppCustCode.CausesValidation = True
		Me.txtSuppCustCode.Enabled = True
		Me.txtSuppCustCode.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtSuppCustCode.HideSelection = True
		Me.txtSuppCustCode.ReadOnly = False
		Me.txtSuppCustCode.Maxlength = 0
		Me.txtSuppCustCode.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtSuppCustCode.MultiLine = False
		Me.txtSuppCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtSuppCustCode.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtSuppCustCode.TabStop = True
		Me.txtSuppCustCode.Visible = True
		Me.txtSuppCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtSuppCustCode.Name = "txtSuppCustCode"
		Me.cmdSearchCust.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearchCust.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearchCust.Size = New System.Drawing.Size(27, 19)
		Me.cmdSearchCust.Location = New System.Drawing.Point(180, 10)
		Me.cmdSearchCust.Image = CType(resources.GetObject("cmdSearchCust.Image"), System.Drawing.Image)
		Me.cmdSearchCust.TabIndex = 1
		Me.cmdSearchCust.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearchCust, "Search")
		Me.cmdSearchCust.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearchCust.CausesValidation = True
		Me.cmdSearchCust.Enabled = True
		Me.cmdSearchCust.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearchCust.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearchCust.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearchCust.Name = "cmdSearchCust"
		Me.cmdSearchWEF.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearchWEF.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearchWEF.Size = New System.Drawing.Size(27, 19)
		Me.cmdSearchWEF.Location = New System.Drawing.Point(180, 31)
		Me.cmdSearchWEF.Image = CType(resources.GetObject("cmdSearchWEF.Image"), System.Drawing.Image)
		Me.cmdSearchWEF.TabIndex = 4
		Me.cmdSearchWEF.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearchWEF, "Search")
		Me.cmdSearchWEF.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearchWEF.CausesValidation = True
		Me.cmdSearchWEF.Enabled = True
		Me.cmdSearchWEF.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearchWEF.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearchWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearchWEF.Name = "cmdSearchWEF"
		Me.txtWEF.AutoSize = False
		Me.txtWEF.Size = New System.Drawing.Size(81, 19)
		Me.txtWEF.Location = New System.Drawing.Point(98, 30)
		Me.txtWEF.TabIndex = 3
		Me.ToolTip1.SetToolTip(Me.txtWEF, "Press F1 For Help")
		Me.txtWEF.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtWEF.AcceptsReturn = True
		Me.txtWEF.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
		Me.txtWEF.CausesValidation = True
		Me.txtWEF.Enabled = True
		Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtWEF.HideSelection = True
		Me.txtWEF.ReadOnly = False
		Me.txtWEF.Maxlength = 0
		Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtWEF.MultiLine = False
		Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtWEF.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtWEF.TabStop = True
		Me.txtWEF.Visible = True
		Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtWEF.Name = "txtWEF"
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label6.Text = "Amend No :"
		Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.Size = New System.Drawing.Size(67, 13)
		Me.Label6.Location = New System.Drawing.Point(212, 32)
		Me.Label6.TabIndex = 48
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
		Me.lblMKey.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.lblMKey.Text = "lblMKey"
		Me.lblMKey.Enabled = False
		Me.lblMKey.Size = New System.Drawing.Size(37, 13)
		Me.lblMKey.Location = New System.Drawing.Point(700, 36)
		Me.lblMKey.TabIndex = 47
		Me.lblMKey.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
		Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblMKey.UseMnemonic = True
		Me.lblMKey.Visible = True
		Me.lblMKey.AutoSize = True
		Me.lblMKey.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lblMKey.Name = "lblMKey"
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label3.Text = "Customer :"
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(61, 13)
		Me.Label3.Location = New System.Drawing.Point(31, 12)
		Me.Label3.TabIndex = 46
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
		Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label9.Text = "W.E.F. :"
		Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label9.Size = New System.Drawing.Size(48, 13)
		Me.Label9.Location = New System.Drawing.Point(46, 32)
		Me.Label9.TabIndex = 45
		Me.Label9.BackColor = System.Drawing.SystemColors.Control
		Me.Label9.Enabled = True
		Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label9.UseMnemonic = True
		Me.Label9.Visible = True
		Me.Label9.AutoSize = True
		Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label9.Name = "Label9"
		Me.fraCosting.Size = New System.Drawing.Size(747, 55)
		Me.fraCosting.Location = New System.Drawing.Point(2, 384)
		Me.fraCosting.TabIndex = 39
		Me.fraCosting.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.fraCosting.BackColor = System.Drawing.SystemColors.Control
		Me.fraCosting.Enabled = True
		Me.fraCosting.ForeColor = System.Drawing.SystemColors.ControlText
		Me.fraCosting.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.fraCosting.Visible = True
		Me.fraCosting.Padding = New System.Windows.Forms.Padding(0)
		Me.fraCosting.Name = "fraCosting"
		Me.chkStatus.Text = "Status (Open / Closed)"
		Me.chkStatus.Enabled = False
		Me.chkStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkStatus.Size = New System.Drawing.Size(163, 13)
		Me.chkStatus.Location = New System.Drawing.Point(460, 13)
		Me.chkStatus.TabIndex = 20
		Me.chkStatus.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkStatus.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
		Me.chkStatus.CausesValidation = True
		Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkStatus.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkStatus.TabStop = True
		Me.chkStatus.CheckState = System.Windows.Forms.CheckState.Unchecked
		Me.chkStatus.Visible = True
		Me.chkStatus.Name = "chkStatus"
		Me.txtRemarks.AutoSize = False
		Me.txtRemarks.Size = New System.Drawing.Size(283, 19)
		Me.txtRemarks.Location = New System.Drawing.Point(88, 10)
		Me.txtRemarks.MultiLine = True
		Me.txtRemarks.TabIndex = 19
		Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtRemarks.AcceptsReturn = True
		Me.txtRemarks.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
		Me.txtRemarks.CausesValidation = True
		Me.txtRemarks.Enabled = True
		Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtRemarks.HideSelection = True
		Me.txtRemarks.ReadOnly = False
		Me.txtRemarks.Maxlength = 0
		Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtRemarks.TabStop = True
		Me.txtRemarks.Visible = True
		Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtRemarks.Name = "txtRemarks"
		Me.txtAppBy.AutoSize = False
		Me.txtAppBy.Size = New System.Drawing.Size(69, 19)
		Me.txtAppBy.Location = New System.Drawing.Point(460, 30)
		Me.txtAppBy.TabIndex = 23
		Me.txtAppBy.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtAppBy.AcceptsReturn = True
		Me.txtAppBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtAppBy.BackColor = System.Drawing.SystemColors.Window
		Me.txtAppBy.CausesValidation = True
		Me.txtAppBy.Enabled = True
		Me.txtAppBy.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtAppBy.HideSelection = True
		Me.txtAppBy.ReadOnly = False
		Me.txtAppBy.Maxlength = 0
		Me.txtAppBy.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtAppBy.MultiLine = False
		Me.txtAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtAppBy.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtAppBy.TabStop = True
		Me.txtAppBy.Visible = True
		Me.txtAppBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtAppBy.Name = "txtAppBy"
		Me.txtPrepBy.AutoSize = False
		Me.txtPrepBy.Size = New System.Drawing.Size(69, 19)
		Me.txtPrepBy.Location = New System.Drawing.Point(88, 30)
		Me.txtPrepBy.TabIndex = 21
		Me.txtPrepBy.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtPrepBy.AcceptsReturn = True
		Me.txtPrepBy.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtPrepBy.BackColor = System.Drawing.SystemColors.Window
		Me.txtPrepBy.CausesValidation = True
		Me.txtPrepBy.Enabled = True
		Me.txtPrepBy.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtPrepBy.HideSelection = True
		Me.txtPrepBy.ReadOnly = False
		Me.txtPrepBy.Maxlength = 0
		Me.txtPrepBy.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtPrepBy.MultiLine = False
		Me.txtPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtPrepBy.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtPrepBy.TabStop = True
		Me.txtPrepBy.Visible = True
		Me.txtPrepBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtPrepBy.Name = "txtPrepBy"
		Me.cmdSearchPrepBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearchPrepBy.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearchPrepBy.Size = New System.Drawing.Size(23, 17)
		Me.cmdSearchPrepBy.Location = New System.Drawing.Point(158, 31)
		Me.cmdSearchPrepBy.Image = CType(resources.GetObject("cmdSearchPrepBy.Image"), System.Drawing.Image)
		Me.cmdSearchPrepBy.TabIndex = 22
		Me.cmdSearchPrepBy.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearchPrepBy, "Search")
		Me.cmdSearchPrepBy.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearchPrepBy.CausesValidation = True
		Me.cmdSearchPrepBy.Enabled = True
		Me.cmdSearchPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearchPrepBy.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearchPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearchPrepBy.Name = "cmdSearchPrepBy"
		Me.cmdSearchAppBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearchAppBy.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearchAppBy.Size = New System.Drawing.Size(23, 17)
		Me.cmdSearchAppBy.Location = New System.Drawing.Point(530, 31)
		Me.cmdSearchAppBy.Image = CType(resources.GetObject("cmdSearchAppBy.Image"), System.Drawing.Image)
		Me.cmdSearchAppBy.TabIndex = 24
		Me.cmdSearchAppBy.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearchAppBy, "Search")
		Me.cmdSearchAppBy.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearchAppBy.CausesValidation = True
		Me.cmdSearchAppBy.Enabled = True
		Me.cmdSearchAppBy.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearchAppBy.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearchAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearchAppBy.Name = "cmdSearchAppBy"
		Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label14.Text = "Approved By :"
		Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label14.Size = New System.Drawing.Size(81, 13)
		Me.Label14.Location = New System.Drawing.Point(375, 33)
		Me.Label14.TabIndex = 43
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
		Me.lblAppBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblAppBy.Size = New System.Drawing.Size(185, 19)
		Me.lblAppBy.Location = New System.Drawing.Point(554, 30)
		Me.lblAppBy.TabIndex = 27
		Me.lblAppBy.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblAppBy.BackColor = System.Drawing.SystemColors.Control
		Me.lblAppBy.Enabled = True
		Me.lblAppBy.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblAppBy.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblAppBy.UseMnemonic = True
		Me.lblAppBy.Visible = True
		Me.lblAppBy.AutoSize = False
		Me.lblAppBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblAppBy.Name = "lblAppBy"
		Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label13.Text = "Prepared By :"
		Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label13.Size = New System.Drawing.Size(78, 13)
		Me.Label13.Location = New System.Drawing.Point(6, 33)
		Me.Label13.TabIndex = 42
		Me.Label13.BackColor = System.Drawing.SystemColors.Control
		Me.Label13.Enabled = True
		Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label13.UseMnemonic = True
		Me.Label13.Visible = True
		Me.Label13.AutoSize = True
		Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label13.Name = "Label13"
		Me.lblPrepBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lblPrepBy.Size = New System.Drawing.Size(185, 19)
		Me.lblPrepBy.Location = New System.Drawing.Point(182, 30)
		Me.lblPrepBy.TabIndex = 26
		Me.lblPrepBy.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lblPrepBy.BackColor = System.Drawing.SystemColors.Control
		Me.lblPrepBy.Enabled = True
		Me.lblPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblPrepBy.Cursor = System.Windows.Forms.Cursors.Default
		Me.lblPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lblPrepBy.UseMnemonic = True
		Me.lblPrepBy.Visible = True
		Me.lblPrepBy.AutoSize = False
		Me.lblPrepBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblPrepBy.Name = "lblPrepBy"
		Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label5.Text = "Remarks :"
		Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label5.Size = New System.Drawing.Size(58, 13)
		Me.Label5.Location = New System.Drawing.Point(26, 13)
		Me.Label5.TabIndex = 41
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
		Me.SSTab1.Size = New System.Drawing.Size(747, 245)
		Me.SSTab1.Location = New System.Drawing.Point(2, 58)
		Me.SSTab1.TabIndex = 50
		Me.SSTab1.SelectedIndex = 2
		Me.SSTab1.ItemSize = New System.Drawing.Size(42, 24)
		Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.SSTab1.Name = "SSTab1"
		Me._SSTab1_TabPage0.Text = "I - MIG Wire Cost"
		SprdWeld.OcxState = CType(resources.GetObject("SprdWeld.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdWeld.Size = New System.Drawing.Size(735, 206)
		Me.SprdWeld.Location = New System.Drawing.Point(6, 33)
		Me.SprdWeld.TabIndex = 25
		Me.SprdWeld.Name = "SprdWeld"
		Me._SSTab1_TabPage1.Text = "II - CO2 Cost"
		SprdCO2.OcxState = CType(resources.GetObject("SprdCO2.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdCO2.Size = New System.Drawing.Size(735, 206)
		Me.SprdCO2.Location = New System.Drawing.Point(6, 33)
		Me.SprdCO2.TabIndex = 6
		Me.SprdCO2.Name = "SprdCO2"
		Me._SSTab1_TabPage2.Text = "III - M/c Cost"
		SprdMC.OcxState = CType(resources.GetObject("SprdMC.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdMC.Size = New System.Drawing.Size(735, 207)
		Me.SprdMC.Location = New System.Drawing.Point(6, 33)
		Me.SprdMC.TabIndex = 7
		Me.SprdMC.Name = "SprdMC"
		Me._SSTab1_TabPage3.Text = "IV - Power Cost"
		SprdPower.OcxState = CType(resources.GetObject("SprdPower.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdPower.Size = New System.Drawing.Size(735, 207)
		Me.SprdPower.Location = New System.Drawing.Point(6, 33)
		Me.SprdPower.TabIndex = 8
		Me.SprdPower.Name = "SprdPower"
		Me._SSTab1_TabPage4.Text = "V - Labour Cost"
		SprdLabour.OcxState = CType(resources.GetObject("SprdLabour.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdLabour.Size = New System.Drawing.Size(735, 207)
		Me.SprdLabour.Location = New System.Drawing.Point(6, 33)
		Me.SprdLabour.TabIndex = 9
		Me.SprdLabour.Name = "SprdLabour"
		Me._SSTab1_TabPage5.Text = "VI - Consumables Cost"
		SprdCons.OcxState = CType(resources.GetObject("SprdCons.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdCons.Size = New System.Drawing.Size(735, 207)
		Me.SprdCons.Location = New System.Drawing.Point(6, 33)
		Me.SprdCons.TabIndex = 10
		Me.SprdCons.Name = "SprdCons"
		Me.ADataGrid.Size = New System.Drawing.Size(80, 22)
		Me.ADataGrid.Location = New System.Drawing.Point(252, 144)
		Me.ADataGrid.CursorLocation = ADODB.CursorLocationEnum.adUseClient
		Me.ADataGrid.ConnectionTimeout = 15
		Me.ADataGrid.CommandTimeout = 30
		Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
		Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
		Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
		Me.ADataGrid.CacheSize = 50
		Me.ADataGrid.MaxRecords = 0
		Me.ADataGrid.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
		Me.ADataGrid.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
		Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
		Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
		Me.ADataGrid.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
		Me.ADataGrid.Enabled = True
		Me.ADataGrid.UserName = ""
		Me.ADataGrid.RecordSource = ""
		Me.ADataGrid.Text = "Adodc1"
		Me.ADataGrid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ADataGrid.ConnectionString = ""
		Me.ADataGrid.Name = "ADataGrid"
		SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
		Me.SprdView.Size = New System.Drawing.Size(751, 435)
		Me.SprdView.Location = New System.Drawing.Point(0, 0)
		Me.SprdView.TabIndex = 40
		Me.SprdView.Name = "SprdView"
		Me.Frame3.Size = New System.Drawing.Size(751, 47)
		Me.Frame3.Location = New System.Drawing.Point(0, 432)
		Me.Frame3.TabIndex = 37
		Me.Frame3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame3.BackColor = System.Drawing.SystemColors.Control
		Me.Frame3.Enabled = True
		Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame3.Visible = True
		Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame3.Name = "Frame3"
		Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdClose.Text = "&Close"
		Me.CmdClose.Size = New System.Drawing.Size(67, 34)
		Me.CmdClose.Location = New System.Drawing.Point(636, 10)
		Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
		Me.CmdClose.TabIndex = 36
		Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
		Me.CmdClose.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
		Me.CmdClose.CausesValidation = True
		Me.CmdClose.Enabled = True
		Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdClose.TabStop = True
		Me.CmdClose.Name = "CmdClose"
		Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdView.Text = "List &View"
		Me.CmdView.Size = New System.Drawing.Size(67, 34)
		Me.CmdView.Location = New System.Drawing.Point(570, 10)
		Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
		Me.CmdView.TabIndex = 35
		Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
		Me.CmdView.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdView.BackColor = System.Drawing.SystemColors.Control
		Me.CmdView.CausesValidation = True
		Me.CmdView.Enabled = True
		Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdView.TabStop = True
		Me.CmdView.Name = "CmdView"
		Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdPreview.Text = "Pre&view"
		Me.CmdPreview.Enabled = False
		Me.CmdPreview.Size = New System.Drawing.Size(67, 34)
		Me.CmdPreview.Location = New System.Drawing.Point(504, 10)
		Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
		Me.CmdPreview.TabIndex = 34
		Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
		Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
		Me.CmdPreview.CausesValidation = True
		Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdPreview.TabStop = True
		Me.CmdPreview.Name = "CmdPreview"
		Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Report1.Location = New System.Drawing.Point(592, 12)
		Me.Report1.Name = "Report1"
		Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdPrint.Text = "&Print"
		Me.cmdPrint.Size = New System.Drawing.Size(67, 34)
		Me.cmdPrint.Location = New System.Drawing.Point(438, 10)
		Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
		Me.cmdPrint.TabIndex = 33
		Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
		Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPrint.CausesValidation = True
		Me.cmdPrint.Enabled = True
		Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPrint.TabStop = True
		Me.cmdPrint.Name = "cmdPrint"
		Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdDelete.Text = "&Delete"
		Me.CmdDelete.Size = New System.Drawing.Size(67, 34)
		Me.CmdDelete.Location = New System.Drawing.Point(372, 10)
		Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
		Me.CmdDelete.TabIndex = 32
		Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
		Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
		Me.CmdDelete.CausesValidation = True
		Me.CmdDelete.Enabled = True
		Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdDelete.TabStop = True
		Me.CmdDelete.Name = "CmdDelete"
		Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSavePrint.Text = "Save&&Print"
		Me.cmdSavePrint.Size = New System.Drawing.Size(67, 34)
		Me.cmdSavePrint.Location = New System.Drawing.Point(306, 10)
		Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
		Me.cmdSavePrint.TabIndex = 31
		Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
		Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdSavePrint.CausesValidation = True
		Me.cmdSavePrint.Enabled = True
		Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSavePrint.TabStop = True
		Me.cmdSavePrint.Name = "cmdSavePrint"
		Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSave.Text = "&Save"
		Me.CmdSave.Size = New System.Drawing.Size(67, 34)
		Me.CmdSave.Location = New System.Drawing.Point(240, 10)
		Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
		Me.CmdSave.TabIndex = 30
		Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
		Me.CmdSave.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
		Me.CmdSave.CausesValidation = True
		Me.CmdSave.Enabled = True
		Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSave.TabStop = True
		Me.CmdSave.Name = "CmdSave"
		Me.cmdAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdAmend.Text = "&Amendment"
		Me.cmdAmend.Size = New System.Drawing.Size(67, 34)
		Me.cmdAmend.Location = New System.Drawing.Point(174, 10)
		Me.cmdAmend.Image = CType(resources.GetObject("cmdAmend.Image"), System.Drawing.Image)
		Me.cmdAmend.TabIndex = 49
		Me.ToolTip1.SetToolTip(Me.cmdAmend, "Modify Record")
		Me.cmdAmend.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdAmend.BackColor = System.Drawing.SystemColors.Control
		Me.cmdAmend.CausesValidation = True
		Me.cmdAmend.Enabled = True
		Me.cmdAmend.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdAmend.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdAmend.TabStop = True
		Me.cmdAmend.Name = "cmdAmend"
		Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdModify.Text = "&Modify"
		Me.CmdModify.Size = New System.Drawing.Size(67, 34)
		Me.CmdModify.Location = New System.Drawing.Point(108, 10)
		Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
		Me.CmdModify.TabIndex = 29
		Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
		Me.CmdModify.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
		Me.CmdModify.CausesValidation = True
		Me.CmdModify.Enabled = True
		Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdModify.TabStop = True
		Me.CmdModify.Name = "CmdModify"
		Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdAdd.Text = "&Add"
		Me.CmdAdd.Size = New System.Drawing.Size(67, 34)
		Me.CmdAdd.Location = New System.Drawing.Point(42, 10)
		Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
		Me.CmdAdd.TabIndex = 28
		Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
		Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
		Me.CmdAdd.CausesValidation = True
		Me.CmdAdd.Enabled = True
		Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdAdd.TabStop = True
		Me.CmdAdd.Name = "CmdAdd"
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdCons, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdLabour, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdPower, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdMC, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdCO2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.SprdWeld, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(fraBase)
		Me.Controls.Add(ADataGrid)
		Me.Controls.Add(SprdView)
		Me.Controls.Add(Frame3)
		Me.fraBase.Controls.Add(Frame2)
		Me.fraBase.Controls.Add(Frame1)
		Me.fraBase.Controls.Add(fraCosting)
		Me.fraBase.Controls.Add(SSTab1)
		Me.Frame2.Controls.Add(txtNetWeldCost)
		Me.Frame2.Controls.Add(txtTotMCCost)
		Me.Frame2.Controls.Add(txtSmokeCost)
		Me.Frame2.Controls.Add(txtTotConsCost)
		Me.Frame2.Controls.Add(txtTotLabourCost)
		Me.Frame2.Controls.Add(txtTotPowerCost)
		Me.Frame2.Controls.Add(txtTotCO2Cost)
		Me.Frame2.Controls.Add(txtTotMIGCost)
		Me.Frame2.Controls.Add(Label2)
		Me.Frame2.Controls.Add(Label52)
		Me.Frame2.Controls.Add(Label51)
		Me.Frame2.Controls.Add(Label37)
		Me.Frame2.Controls.Add(Label28)
		Me.Frame2.Controls.Add(Label36)
		Me.Frame2.Controls.Add(Label32)
		Me.Frame2.Controls.Add(Label31)
		Me.Frame2.Controls.Add(Label30)
		Me.Frame2.Controls.Add(Label29)
		Me.Frame2.Controls.Add(Label27)
		Me.Frame2.Controls.Add(Label22)
		Me.Frame2.Controls.Add(Label21)
		Me.Frame2.Controls.Add(Label20)
		Me.Frame2.Controls.Add(Label19)
		Me.Frame1.Controls.Add(txtAmendNo)
		Me.Frame1.Controls.Add(txtSuppCustName)
		Me.Frame1.Controls.Add(txtSuppCustCode)
		Me.Frame1.Controls.Add(cmdSearchCust)
		Me.Frame1.Controls.Add(cmdSearchWEF)
		Me.Frame1.Controls.Add(txtWEF)
		Me.Frame1.Controls.Add(Label6)
		Me.Frame1.Controls.Add(lblMKey)
		Me.Frame1.Controls.Add(Label3)
		Me.Frame1.Controls.Add(Label9)
		Me.fraCosting.Controls.Add(chkStatus)
		Me.fraCosting.Controls.Add(txtRemarks)
		Me.fraCosting.Controls.Add(txtAppBy)
		Me.fraCosting.Controls.Add(txtPrepBy)
		Me.fraCosting.Controls.Add(cmdSearchPrepBy)
		Me.fraCosting.Controls.Add(cmdSearchAppBy)
		Me.fraCosting.Controls.Add(Label14)
		Me.fraCosting.Controls.Add(lblAppBy)
		Me.fraCosting.Controls.Add(Label13)
		Me.fraCosting.Controls.Add(lblPrepBy)
		Me.fraCosting.Controls.Add(Label5)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage0)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage1)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage2)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage3)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage4)
		Me.SSTab1.Controls.Add(_SSTab1_TabPage5)
		Me._SSTab1_TabPage0.Controls.Add(SprdWeld)
		Me._SSTab1_TabPage1.Controls.Add(SprdCO2)
		Me._SSTab1_TabPage2.Controls.Add(SprdMC)
		Me._SSTab1_TabPage3.Controls.Add(SprdPower)
		Me._SSTab1_TabPage4.Controls.Add(SprdLabour)
		Me._SSTab1_TabPage5.Controls.Add(SprdCons)
		Me.Frame3.Controls.Add(CmdClose)
		Me.Frame3.Controls.Add(CmdView)
		Me.Frame3.Controls.Add(CmdPreview)
		Me.Frame3.Controls.Add(Report1)
		Me.Frame3.Controls.Add(cmdPrint)
		Me.Frame3.Controls.Add(CmdDelete)
		Me.Frame3.Controls.Add(cmdSavePrint)
		Me.Frame3.Controls.Add(CmdSave)
		Me.Frame3.Controls.Add(cmdAmend)
		Me.Frame3.Controls.Add(CmdModify)
		Me.Frame3.Controls.Add(CmdAdd)
		Me.fraBase.ResumeLayout(False)
		Me.Frame2.ResumeLayout(False)
		Me.Frame1.ResumeLayout(False)
		Me.fraCosting.ResumeLayout(False)
		Me.SSTab1.ResumeLayout(False)
		Me._SSTab1_TabPage0.ResumeLayout(False)
		Me._SSTab1_TabPage1.ResumeLayout(False)
		Me._SSTab1_TabPage2.ResumeLayout(False)
		Me._SSTab1_TabPage3.ResumeLayout(False)
		Me._SSTab1_TabPage4.ResumeLayout(False)
		Me._SSTab1_TabPage5.ResumeLayout(False)
		Me.Frame3.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
#Region "Upgrade Support"
	Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdView.DataSource = Nothing
	End Sub
#End Region 
End Class