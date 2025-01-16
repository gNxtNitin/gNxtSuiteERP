Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFGSubCostingCustomerWise
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
	Public WithEvents txtProcessCost_C As System.Windows.Forms.TextBox
	Public WithEvents txtToolCostPerPc As System.Windows.Forms.TextBox
	Public WithEvents txtToolQty As System.Windows.Forms.TextBox
	Public WithEvents txtToolCost As System.Windows.Forms.TextBox
	Public WithEvents txtNetWt As System.Windows.Forms.TextBox
	Public WithEvents txtScrapWt As System.Windows.Forms.TextBox
	Public WithEvents txtGrossWt As System.Windows.Forms.TextBox
	Public WithEvents txtOpeartionCost As System.Windows.Forms.TextBox
	Public WithEvents txtOtherCost As System.Windows.Forms.TextBox
	Public WithEvents txtNetBOPCost As System.Windows.Forms.TextBox
	Public WithEvents txtProcessCost_B As System.Windows.Forms.TextBox
	Public WithEvents txtProcessCost_A As System.Windows.Forms.TextBox
	Public WithEvents txtStdPartCost As System.Windows.Forms.TextBox
	Public WithEvents txtNetCost As System.Windows.Forms.TextBox
	Public WithEvents txtScrapCost As System.Windows.Forms.TextBox
	Public WithEvents txtGrossCost As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchCust As System.Windows.Forms.Button
	Public WithEvents txtSuppCustCode As System.Windows.Forms.TextBox
	Public WithEvents txtSuppCustName As System.Windows.Forms.TextBox
	Public WithEvents chkStatus As System.Windows.Forms.CheckBox
	Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
	Public WithEvents txtUnit As System.Windows.Forms.TextBox
	Public WithEvents txtCustPartNo As System.Windows.Forms.TextBox
	Public WithEvents txtModelNo As System.Windows.Forms.TextBox
	Public WithEvents txtItemDesc As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchItemCode As System.Windows.Forms.Button
	Public WithEvents txtItemCode As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchWEF As System.Windows.Forms.Button
	Public WithEvents txtWEF As System.Windows.Forms.TextBox
	Public WithEvents Label25 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents lblMKey As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents SprdPart As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents SprdProcess1 As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents SprdProcess2 As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents SprdProcess3 As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
	Public WithEvents SprdMainOperation As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
	Public WithEvents SprdCostingExp As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage6 As System.Windows.Forms.TabPage
	Public WithEvents SSTab1 As System.Windows.Forms.TabControl
	Public WithEvents TxtRemarks As System.Windows.Forms.TextBox
	Public WithEvents txtApprovedBy As System.Windows.Forms.TextBox
	Public WithEvents txtPreparedBy As System.Windows.Forms.TextBox
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents lblApprovedBy As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents lblPreparedBy As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents fraCosting As System.Windows.Forms.GroupBox
	Public WithEvents Label26 As System.Windows.Forms.Label
	Public WithEvents Label24 As System.Windows.Forms.Label
	Public WithEvents Label23 As System.Windows.Forms.Label
	Public WithEvents Label22 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents fraBase As System.Windows.Forms.GroupBox
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFGSubCostingCustomerWise))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchCust = New System.Windows.Forms.Button()
        Me.txtSuppCustCode = New System.Windows.Forms.TextBox()
        Me.txtSuppCustName = New System.Windows.Forms.TextBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.txtCustPartNo = New System.Windows.Forms.TextBox()
        Me.txtModelNo = New System.Windows.Forms.TextBox()
        Me.txtItemDesc = New System.Windows.Forms.TextBox()
        Me.cmdSearchItemCode = New System.Windows.Forms.Button()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.cmdSearchWEF = New System.Windows.Forms.Button()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdAmend = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraBase = New System.Windows.Forms.GroupBox()
        Me.txtProcessCost_C = New System.Windows.Forms.TextBox()
        Me.txtToolCostPerPc = New System.Windows.Forms.TextBox()
        Me.txtToolQty = New System.Windows.Forms.TextBox()
        Me.txtToolCost = New System.Windows.Forms.TextBox()
        Me.txtNetWt = New System.Windows.Forms.TextBox()
        Me.txtScrapWt = New System.Windows.Forms.TextBox()
        Me.txtGrossWt = New System.Windows.Forms.TextBox()
        Me.txtOpeartionCost = New System.Windows.Forms.TextBox()
        Me.txtOtherCost = New System.Windows.Forms.TextBox()
        Me.txtNetBOPCost = New System.Windows.Forms.TextBox()
        Me.txtProcessCost_B = New System.Windows.Forms.TextBox()
        Me.txtProcessCost_A = New System.Windows.Forms.TextBox()
        Me.txtStdPartCost = New System.Windows.Forms.TextBox()
        Me.txtNetCost = New System.Windows.Forms.TextBox()
        Me.txtScrapCost = New System.Windows.Forms.TextBox()
        Me.txtGrossCost = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.SprdPart = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.SprdProcess1 = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.SprdProcess2 = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me.SprdProcess3 = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage()
        Me.SprdMainOperation = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage6 = New System.Windows.Forms.TabPage()
        Me.SprdCostingExp = New AxFPSpreadADO.AxfpSpread()
        Me.fraCosting = New System.Windows.Forms.GroupBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.txtApprovedBy = New System.Windows.Forms.TextBox()
        Me.txtPreparedBy = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblApprovedBy = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblPreparedBy = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.fraBase.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        CType(Me.SprdPart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage2.SuspendLayout()
        CType(Me.SprdProcess1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage3.SuspendLayout()
        CType(Me.SprdProcess2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage4.SuspendLayout()
        CType(Me.SprdProcess3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage5.SuspendLayout()
        CType(Me.SprdMainOperation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage6.SuspendLayout()
        CType(Me.SprdCostingExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCosting.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchCust
        '
        Me.cmdSearchCust.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCust.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCust.Image = CType(resources.GetObject("cmdSearchCust.Image"), System.Drawing.Image)
        Me.cmdSearchCust.Location = New System.Drawing.Point(180, 10)
        Me.cmdSearchCust.Name = "cmdSearchCust"
        Me.cmdSearchCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCust.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchCust.TabIndex = 1
        Me.cmdSearchCust.TabStop = False
        Me.cmdSearchCust.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCust, "Search")
        Me.cmdSearchCust.UseVisualStyleBackColor = False
        '
        'txtSuppCustCode
        '
        Me.txtSuppCustCode.AcceptsReturn = True
        Me.txtSuppCustCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppCustCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppCustCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppCustCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuppCustCode.Location = New System.Drawing.Point(98, 10)
        Me.txtSuppCustCode.MaxLength = 0
        Me.txtSuppCustCode.Name = "txtSuppCustCode"
        Me.txtSuppCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppCustCode.Size = New System.Drawing.Size(81, 20)
        Me.txtSuppCustCode.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtSuppCustCode, "Press F1 For Help")
        '
        'txtSuppCustName
        '
        Me.txtSuppCustName.AcceptsReturn = True
        Me.txtSuppCustName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppCustName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppCustName.Enabled = False
        Me.txtSuppCustName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppCustName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuppCustName.Location = New System.Drawing.Point(208, 10)
        Me.txtSuppCustName.MaxLength = 0
        Me.txtSuppCustName.Name = "txtSuppCustName"
        Me.txtSuppCustName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppCustName.Size = New System.Drawing.Size(353, 20)
        Me.txtSuppCustName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtSuppCustName, "Press F1 For Help")
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmendNo.Location = New System.Drawing.Point(292, 51)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(81, 20)
        Me.txtAmendNo.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtAmendNo, "Press F1 For Help")
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnit.Location = New System.Drawing.Point(616, 28)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(89, 20)
        Me.txtUnit.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtUnit, "Press F1 For Help")
        '
        'txtCustPartNo
        '
        Me.txtCustPartNo.AcceptsReturn = True
        Me.txtCustPartNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustPartNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPartNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustPartNo.Location = New System.Drawing.Point(446, 51)
        Me.txtCustPartNo.MaxLength = 0
        Me.txtCustPartNo.Name = "txtCustPartNo"
        Me.txtCustPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPartNo.Size = New System.Drawing.Size(115, 20)
        Me.txtCustPartNo.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtCustPartNo, "Press F1 For Help")
        '
        'txtModelNo
        '
        Me.txtModelNo.AcceptsReturn = True
        Me.txtModelNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtModelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModelNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModelNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModelNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModelNo.Location = New System.Drawing.Point(616, 8)
        Me.txtModelNo.MaxLength = 0
        Me.txtModelNo.Name = "txtModelNo"
        Me.txtModelNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModelNo.Size = New System.Drawing.Size(115, 20)
        Me.txtModelNo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtModelNo, "Press F1 For Help")
        Me.txtModelNo.Visible = False
        '
        'txtItemDesc
        '
        Me.txtItemDesc.AcceptsReturn = True
        Me.txtItemDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemDesc.Location = New System.Drawing.Point(208, 30)
        Me.txtItemDesc.MaxLength = 0
        Me.txtItemDesc.Name = "txtItemDesc"
        Me.txtItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemDesc.Size = New System.Drawing.Size(353, 20)
        Me.txtItemDesc.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtItemDesc, "Press F1 For Help")
        '
        'cmdSearchItemCode
        '
        Me.cmdSearchItemCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItemCode.Image = CType(resources.GetObject("cmdSearchItemCode.Image"), System.Drawing.Image)
        Me.cmdSearchItemCode.Location = New System.Drawing.Point(180, 30)
        Me.cmdSearchItemCode.Name = "cmdSearchItemCode"
        Me.cmdSearchItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItemCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchItemCode.TabIndex = 5
        Me.cmdSearchItemCode.TabStop = False
        Me.cmdSearchItemCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItemCode, "Search")
        Me.cmdSearchItemCode.UseVisualStyleBackColor = False
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemCode.Location = New System.Drawing.Point(98, 30)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(81, 20)
        Me.txtItemCode.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtItemCode, "Press F1 For Help")
        '
        'cmdSearchWEF
        '
        Me.cmdSearchWEF.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchWEF.Image = CType(resources.GetObject("cmdSearchWEF.Image"), System.Drawing.Image)
        Me.cmdSearchWEF.Location = New System.Drawing.Point(180, 51)
        Me.cmdSearchWEF.Name = "cmdSearchWEF"
        Me.cmdSearchWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchWEF.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchWEF.TabIndex = 9
        Me.cmdSearchWEF.TabStop = False
        Me.cmdSearchWEF.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchWEF, "Search")
        Me.cmdSearchWEF.UseVisualStyleBackColor = False
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(98, 51)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(81, 20)
        Me.txtWEF.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtWEF, "Press F1 For Help")
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(632, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 34)
        Me.CmdClose.TabIndex = 47
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(566, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 34)
        Me.CmdView.TabIndex = 46
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
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(500, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 34)
        Me.CmdPreview.TabIndex = 45
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(434, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdPrint.TabIndex = 44
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
        Me.CmdDelete.Location = New System.Drawing.Point(368, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 34)
        Me.CmdDelete.TabIndex = 43
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(302, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdSavePrint.TabIndex = 42
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(236, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 34)
        Me.CmdSave.TabIndex = 41
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'cmdAmend
        '
        Me.cmdAmend.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmend.Image = CType(resources.GetObject("cmdAmend.Image"), System.Drawing.Image)
        Me.cmdAmend.Location = New System.Drawing.Point(170, 10)
        Me.cmdAmend.Name = "cmdAmend"
        Me.cmdAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmend.Size = New System.Drawing.Size(67, 34)
        Me.cmdAmend.TabIndex = 73
        Me.cmdAmend.Text = "&Amendment"
        Me.cmdAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAmend, "Modify Record")
        Me.cmdAmend.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(104, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 34)
        Me.CmdModify.TabIndex = 40
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
        Me.CmdAdd.Location = New System.Drawing.Point(38, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 34)
        Me.CmdAdd.TabIndex = 15
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraBase
        '
        Me.fraBase.BackColor = System.Drawing.SystemColors.Control
        Me.fraBase.Controls.Add(Me.txtProcessCost_C)
        Me.fraBase.Controls.Add(Me.txtToolCostPerPc)
        Me.fraBase.Controls.Add(Me.txtToolQty)
        Me.fraBase.Controls.Add(Me.txtToolCost)
        Me.fraBase.Controls.Add(Me.txtNetWt)
        Me.fraBase.Controls.Add(Me.txtScrapWt)
        Me.fraBase.Controls.Add(Me.txtGrossWt)
        Me.fraBase.Controls.Add(Me.txtOpeartionCost)
        Me.fraBase.Controls.Add(Me.txtOtherCost)
        Me.fraBase.Controls.Add(Me.txtNetBOPCost)
        Me.fraBase.Controls.Add(Me.txtProcessCost_B)
        Me.fraBase.Controls.Add(Me.txtProcessCost_A)
        Me.fraBase.Controls.Add(Me.txtStdPartCost)
        Me.fraBase.Controls.Add(Me.txtNetCost)
        Me.fraBase.Controls.Add(Me.txtScrapCost)
        Me.fraBase.Controls.Add(Me.txtGrossCost)
        Me.fraBase.Controls.Add(Me.Frame1)
        Me.fraBase.Controls.Add(Me.SSTab1)
        Me.fraBase.Controls.Add(Me.fraCosting)
        Me.fraBase.Controls.Add(Me.Label26)
        Me.fraBase.Controls.Add(Me.Label24)
        Me.fraBase.Controls.Add(Me.Label23)
        Me.fraBase.Controls.Add(Me.Label22)
        Me.fraBase.Controls.Add(Me.Label21)
        Me.fraBase.Controls.Add(Me.Label20)
        Me.fraBase.Controls.Add(Me.Label19)
        Me.fraBase.Controls.Add(Me.Label18)
        Me.fraBase.Controls.Add(Me.Label17)
        Me.fraBase.Controls.Add(Me.Label16)
        Me.fraBase.Controls.Add(Me.Label15)
        Me.fraBase.Controls.Add(Me.Label12)
        Me.fraBase.Controls.Add(Me.Label11)
        Me.fraBase.Controls.Add(Me.Label10)
        Me.fraBase.Controls.Add(Me.Label8)
        Me.fraBase.Controls.Add(Me.Label6)
        Me.fraBase.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBase.Location = New System.Drawing.Point(0, -4)
        Me.fraBase.Name = "fraBase"
        Me.fraBase.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBase.Size = New System.Drawing.Size(751, 441)
        Me.fraBase.TabIndex = 49
        Me.fraBase.TabStop = False
        '
        'txtProcessCost_C
        '
        Me.txtProcessCost_C.AcceptsReturn = True
        Me.txtProcessCost_C.BackColor = System.Drawing.SystemColors.Window
        Me.txtProcessCost_C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProcessCost_C.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProcessCost_C.Enabled = False
        Me.txtProcessCost_C.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProcessCost_C.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProcessCost_C.Location = New System.Drawing.Point(486, 354)
        Me.txtProcessCost_C.MaxLength = 0
        Me.txtProcessCost_C.Name = "txtProcessCost_C"
        Me.txtProcessCost_C.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProcessCost_C.Size = New System.Drawing.Size(75, 20)
        Me.txtProcessCost_C.TabIndex = 27
        '
        'txtToolCostPerPc
        '
        Me.txtToolCostPerPc.AcceptsReturn = True
        Me.txtToolCostPerPc.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolCostPerPc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolCostPerPc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolCostPerPc.Enabled = False
        Me.txtToolCostPerPc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToolCostPerPc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolCostPerPc.Location = New System.Drawing.Point(666, 374)
        Me.txtToolCostPerPc.MaxLength = 0
        Me.txtToolCostPerPc.Name = "txtToolCostPerPc"
        Me.txtToolCostPerPc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolCostPerPc.Size = New System.Drawing.Size(75, 20)
        Me.txtToolCostPerPc.TabIndex = 32
        '
        'txtToolQty
        '
        Me.txtToolQty.AcceptsReturn = True
        Me.txtToolQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToolQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolQty.Location = New System.Drawing.Point(486, 374)
        Me.txtToolQty.MaxLength = 0
        Me.txtToolQty.Name = "txtToolQty"
        Me.txtToolQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolQty.Size = New System.Drawing.Size(75, 20)
        Me.txtToolQty.TabIndex = 31
        '
        'txtToolCost
        '
        Me.txtToolCost.AcceptsReturn = True
        Me.txtToolCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToolCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolCost.Location = New System.Drawing.Point(304, 374)
        Me.txtToolCost.MaxLength = 0
        Me.txtToolCost.Name = "txtToolCost"
        Me.txtToolCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolCost.Size = New System.Drawing.Size(75, 20)
        Me.txtToolCost.TabIndex = 30
        '
        'txtNetWt
        '
        Me.txtNetWt.AcceptsReturn = True
        Me.txtNetWt.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetWt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetWt.Enabled = False
        Me.txtNetWt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetWt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNetWt.Location = New System.Drawing.Point(666, 394)
        Me.txtNetWt.MaxLength = 0
        Me.txtNetWt.Name = "txtNetWt"
        Me.txtNetWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetWt.Size = New System.Drawing.Size(75, 20)
        Me.txtNetWt.TabIndex = 36
        '
        'txtScrapWt
        '
        Me.txtScrapWt.AcceptsReturn = True
        Me.txtScrapWt.BackColor = System.Drawing.SystemColors.Window
        Me.txtScrapWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScrapWt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScrapWt.Enabled = False
        Me.txtScrapWt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScrapWt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtScrapWt.Location = New System.Drawing.Point(486, 394)
        Me.txtScrapWt.MaxLength = 0
        Me.txtScrapWt.Name = "txtScrapWt"
        Me.txtScrapWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScrapWt.Size = New System.Drawing.Size(75, 20)
        Me.txtScrapWt.TabIndex = 35
        '
        'txtGrossWt
        '
        Me.txtGrossWt.AcceptsReturn = True
        Me.txtGrossWt.BackColor = System.Drawing.SystemColors.Window
        Me.txtGrossWt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrossWt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGrossWt.Enabled = False
        Me.txtGrossWt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrossWt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGrossWt.Location = New System.Drawing.Point(304, 394)
        Me.txtGrossWt.MaxLength = 0
        Me.txtGrossWt.Name = "txtGrossWt"
        Me.txtGrossWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGrossWt.Size = New System.Drawing.Size(75, 20)
        Me.txtGrossWt.TabIndex = 34
        '
        'txtOpeartionCost
        '
        Me.txtOpeartionCost.AcceptsReturn = True
        Me.txtOpeartionCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtOpeartionCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOpeartionCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOpeartionCost.Enabled = False
        Me.txtOpeartionCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOpeartionCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOpeartionCost.Location = New System.Drawing.Point(666, 354)
        Me.txtOpeartionCost.MaxLength = 0
        Me.txtOpeartionCost.Name = "txtOpeartionCost"
        Me.txtOpeartionCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOpeartionCost.Size = New System.Drawing.Size(75, 20)
        Me.txtOpeartionCost.TabIndex = 28
        '
        'txtOtherCost
        '
        Me.txtOtherCost.AcceptsReturn = True
        Me.txtOtherCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtOtherCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOtherCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOtherCost.Enabled = False
        Me.txtOtherCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtherCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOtherCost.Location = New System.Drawing.Point(114, 374)
        Me.txtOtherCost.MaxLength = 0
        Me.txtOtherCost.Name = "txtOtherCost"
        Me.txtOtherCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOtherCost.Size = New System.Drawing.Size(75, 20)
        Me.txtOtherCost.TabIndex = 29
        '
        'txtNetBOPCost
        '
        Me.txtNetBOPCost.AcceptsReturn = True
        Me.txtNetBOPCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetBOPCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetBOPCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetBOPCost.Enabled = False
        Me.txtNetBOPCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetBOPCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNetBOPCost.Location = New System.Drawing.Point(114, 394)
        Me.txtNetBOPCost.MaxLength = 0
        Me.txtNetBOPCost.Name = "txtNetBOPCost"
        Me.txtNetBOPCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetBOPCost.Size = New System.Drawing.Size(75, 20)
        Me.txtNetBOPCost.TabIndex = 33
        '
        'txtProcessCost_B
        '
        Me.txtProcessCost_B.AcceptsReturn = True
        Me.txtProcessCost_B.BackColor = System.Drawing.SystemColors.Window
        Me.txtProcessCost_B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProcessCost_B.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProcessCost_B.Enabled = False
        Me.txtProcessCost_B.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProcessCost_B.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProcessCost_B.Location = New System.Drawing.Point(304, 354)
        Me.txtProcessCost_B.MaxLength = 0
        Me.txtProcessCost_B.Name = "txtProcessCost_B"
        Me.txtProcessCost_B.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProcessCost_B.Size = New System.Drawing.Size(75, 20)
        Me.txtProcessCost_B.TabIndex = 26
        '
        'txtProcessCost_A
        '
        Me.txtProcessCost_A.AcceptsReturn = True
        Me.txtProcessCost_A.BackColor = System.Drawing.SystemColors.Window
        Me.txtProcessCost_A.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProcessCost_A.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProcessCost_A.Enabled = False
        Me.txtProcessCost_A.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProcessCost_A.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProcessCost_A.Location = New System.Drawing.Point(114, 354)
        Me.txtProcessCost_A.MaxLength = 0
        Me.txtProcessCost_A.Name = "txtProcessCost_A"
        Me.txtProcessCost_A.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProcessCost_A.Size = New System.Drawing.Size(75, 20)
        Me.txtProcessCost_A.TabIndex = 25
        '
        'txtStdPartCost
        '
        Me.txtStdPartCost.AcceptsReturn = True
        Me.txtStdPartCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtStdPartCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStdPartCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStdPartCost.Enabled = False
        Me.txtStdPartCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStdPartCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStdPartCost.Location = New System.Drawing.Point(666, 334)
        Me.txtStdPartCost.MaxLength = 0
        Me.txtStdPartCost.Name = "txtStdPartCost"
        Me.txtStdPartCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStdPartCost.Size = New System.Drawing.Size(75, 20)
        Me.txtStdPartCost.TabIndex = 24
        '
        'txtNetCost
        '
        Me.txtNetCost.AcceptsReturn = True
        Me.txtNetCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetCost.Enabled = False
        Me.txtNetCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNetCost.Location = New System.Drawing.Point(486, 334)
        Me.txtNetCost.MaxLength = 0
        Me.txtNetCost.Name = "txtNetCost"
        Me.txtNetCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetCost.Size = New System.Drawing.Size(75, 20)
        Me.txtNetCost.TabIndex = 23
        '
        'txtScrapCost
        '
        Me.txtScrapCost.AcceptsReturn = True
        Me.txtScrapCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtScrapCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScrapCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScrapCost.Enabled = False
        Me.txtScrapCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScrapCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtScrapCost.Location = New System.Drawing.Point(304, 334)
        Me.txtScrapCost.MaxLength = 0
        Me.txtScrapCost.Name = "txtScrapCost"
        Me.txtScrapCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScrapCost.Size = New System.Drawing.Size(75, 20)
        Me.txtScrapCost.TabIndex = 22
        '
        'txtGrossCost
        '
        Me.txtGrossCost.AcceptsReturn = True
        Me.txtGrossCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtGrossCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGrossCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGrossCost.Enabled = False
        Me.txtGrossCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGrossCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGrossCost.Location = New System.Drawing.Point(114, 334)
        Me.txtGrossCost.MaxLength = 0
        Me.txtGrossCost.Name = "txtGrossCost"
        Me.txtGrossCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGrossCost.Size = New System.Drawing.Size(75, 20)
        Me.txtGrossCost.TabIndex = 21
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdSearchCust)
        Me.Frame1.Controls.Add(Me.txtSuppCustCode)
        Me.Frame1.Controls.Add(Me.txtSuppCustName)
        Me.Frame1.Controls.Add(Me.chkStatus)
        Me.Frame1.Controls.Add(Me.txtAmendNo)
        Me.Frame1.Controls.Add(Me.txtUnit)
        Me.Frame1.Controls.Add(Me.txtCustPartNo)
        Me.Frame1.Controls.Add(Me.txtModelNo)
        Me.Frame1.Controls.Add(Me.txtItemDesc)
        Me.Frame1.Controls.Add(Me.cmdSearchItemCode)
        Me.Frame1.Controls.Add(Me.txtItemCode)
        Me.Frame1.Controls.Add(Me.cmdSearchWEF)
        Me.Frame1.Controls.Add(Me.txtWEF)
        Me.Frame1.Controls.Add(Me.Label25)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.lblMKey)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(751, 75)
        Me.Frame1.TabIndex = 58
        Me.Frame1.TabStop = False
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Enabled = False
        Me.chkStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(580, 54)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(149, 18)
        Me.chkStatus.TabIndex = 12
        Me.chkStatus.Text = "Status (Open / Closed)"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(30, 12)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(69, 14)
        Me.Label25.TabIndex = 82
        Me.Label25.Text = "Customer :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(221, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(70, 14)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "Amend No :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Enabled = False
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(704, 38)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 64
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(578, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(34, 14)
        Me.Label7.TabIndex = 63
        Me.Label7.Text = "Unit :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(392, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(52, 14)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Part No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(552, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(64, 14)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Model No :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(26, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(70, 14)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Item Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(46, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(42, 14)
        Me.Label9.TabIndex = 59
        Me.Label9.Text = "W.E.F. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage5)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage6)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 24)
        Me.SSTab1.Location = New System.Drawing.Point(2, 76)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(747, 257)
        Me.SSTab1.TabIndex = 50
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.SprdMain)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(739, 225)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Raw Material Details"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 0)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(739, 225)
        Me.SprdMain.TabIndex = 13
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.SprdPart)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(739, 225)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Other Part Detail"
        '
        'SprdPart
        '
        Me.SprdPart.DataSource = Nothing
        Me.SprdPart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdPart.Location = New System.Drawing.Point(0, 0)
        Me.SprdPart.Name = "SprdPart"
        Me.SprdPart.OcxState = CType(resources.GetObject("SprdPart.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPart.Size = New System.Drawing.Size(739, 225)
        Me.SprdPart.TabIndex = 14
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.SprdProcess1)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(739, 225)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "Process Cost A"
        '
        'SprdProcess1
        '
        Me.SprdProcess1.DataSource = Nothing
        Me.SprdProcess1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdProcess1.Location = New System.Drawing.Point(0, 0)
        Me.SprdProcess1.Name = "SprdProcess1"
        Me.SprdProcess1.OcxState = CType(resources.GetObject("SprdProcess1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdProcess1.Size = New System.Drawing.Size(739, 225)
        Me.SprdProcess1.TabIndex = 16
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.SprdProcess2)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(739, 225)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "Process Cost B"
        '
        'SprdProcess2
        '
        Me.SprdProcess2.DataSource = Nothing
        Me.SprdProcess2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdProcess2.Location = New System.Drawing.Point(0, 0)
        Me.SprdProcess2.Name = "SprdProcess2"
        Me.SprdProcess2.OcxState = CType(resources.GetObject("SprdProcess2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdProcess2.Size = New System.Drawing.Size(739, 225)
        Me.SprdProcess2.TabIndex = 17
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me.SprdProcess3)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(739, 225)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "Process Cost C"
        '
        'SprdProcess3
        '
        Me.SprdProcess3.DataSource = Nothing
        Me.SprdProcess3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdProcess3.Location = New System.Drawing.Point(0, 0)
        Me.SprdProcess3.Name = "SprdProcess3"
        Me.SprdProcess3.OcxState = CType(resources.GetObject("SprdProcess3.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdProcess3.Size = New System.Drawing.Size(739, 225)
        Me.SprdProcess3.TabIndex = 18
        '
        '_SSTab1_TabPage5
        '
        Me._SSTab1_TabPage5.Controls.Add(Me.SprdMainOperation)
        Me._SSTab1_TabPage5.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage5.Name = "_SSTab1_TabPage5"
        Me._SSTab1_TabPage5.Size = New System.Drawing.Size(739, 225)
        Me._SSTab1_TabPage5.TabIndex = 5
        Me._SSTab1_TabPage5.Text = "Operation"
        '
        'SprdMainOperation
        '
        Me.SprdMainOperation.DataSource = Nothing
        Me.SprdMainOperation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMainOperation.Location = New System.Drawing.Point(0, 0)
        Me.SprdMainOperation.Name = "SprdMainOperation"
        Me.SprdMainOperation.OcxState = CType(resources.GetObject("SprdMainOperation.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainOperation.Size = New System.Drawing.Size(739, 225)
        Me.SprdMainOperation.TabIndex = 19
        '
        '_SSTab1_TabPage6
        '
        Me._SSTab1_TabPage6.Controls.Add(Me.SprdCostingExp)
        Me._SSTab1_TabPage6.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage6.Name = "_SSTab1_TabPage6"
        Me._SSTab1_TabPage6.Size = New System.Drawing.Size(739, 225)
        Me._SSTab1_TabPage6.TabIndex = 6
        Me._SSTab1_TabPage6.Text = "Costing Expenses"
        '
        'SprdCostingExp
        '
        Me.SprdCostingExp.DataSource = Nothing
        Me.SprdCostingExp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdCostingExp.Location = New System.Drawing.Point(0, 0)
        Me.SprdCostingExp.Name = "SprdCostingExp"
        Me.SprdCostingExp.OcxState = CType(resources.GetObject("SprdCostingExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdCostingExp.Size = New System.Drawing.Size(739, 225)
        Me.SprdCostingExp.TabIndex = 20
        '
        'fraCosting
        '
        Me.fraCosting.BackColor = System.Drawing.SystemColors.Control
        Me.fraCosting.Controls.Add(Me.TxtRemarks)
        Me.fraCosting.Controls.Add(Me.txtApprovedBy)
        Me.fraCosting.Controls.Add(Me.txtPreparedBy)
        Me.fraCosting.Controls.Add(Me.Label14)
        Me.fraCosting.Controls.Add(Me.lblApprovedBy)
        Me.fraCosting.Controls.Add(Me.Label13)
        Me.fraCosting.Controls.Add(Me.lblPreparedBy)
        Me.fraCosting.Controls.Add(Me.Label5)
        Me.fraCosting.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCosting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCosting.Location = New System.Drawing.Point(2, 408)
        Me.fraCosting.Name = "fraCosting"
        Me.fraCosting.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCosting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCosting.Size = New System.Drawing.Size(747, 33)
        Me.fraCosting.TabIndex = 51
        Me.fraCosting.TabStop = False
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AcceptsReturn = True
        Me.TxtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtRemarks.Location = New System.Drawing.Point(88, 10)
        Me.TxtRemarks.MaxLength = 0
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtRemarks.Size = New System.Drawing.Size(141, 19)
        Me.TxtRemarks.TabIndex = 37
        '
        'txtApprovedBy
        '
        Me.txtApprovedBy.AcceptsReturn = True
        Me.txtApprovedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtApprovedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtApprovedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtApprovedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApprovedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtApprovedBy.Location = New System.Drawing.Point(572, 10)
        Me.txtApprovedBy.MaxLength = 0
        Me.txtApprovedBy.Name = "txtApprovedBy"
        Me.txtApprovedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtApprovedBy.Size = New System.Drawing.Size(49, 20)
        Me.txtApprovedBy.TabIndex = 39
        '
        'txtPreparedBy
        '
        Me.txtPreparedBy.AcceptsReturn = True
        Me.txtPreparedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreparedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPreparedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreparedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPreparedBy.Location = New System.Drawing.Point(314, 10)
        Me.txtPreparedBy.MaxLength = 0
        Me.txtPreparedBy.Name = "txtPreparedBy"
        Me.txtPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPreparedBy.Size = New System.Drawing.Size(49, 20)
        Me.txtPreparedBy.TabIndex = 38
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(487, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(83, 14)
        Me.Label14.TabIndex = 57
        Me.Label14.Text = "Approved By :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblApprovedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblApprovedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblApprovedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApprovedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblApprovedBy.Location = New System.Drawing.Point(622, 10)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblApprovedBy.Size = New System.Drawing.Size(121, 19)
        Me.lblApprovedBy.TabIndex = 56
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(232, 14)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(80, 14)
        Me.Label13.TabIndex = 55
        Me.Label13.Text = "Prepared By :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPreparedBy
        '
        Me.lblPreparedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblPreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPreparedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPreparedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreparedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPreparedBy.Location = New System.Drawing.Point(364, 10)
        Me.lblPreparedBy.Name = "lblPreparedBy"
        Me.lblPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPreparedBy.Size = New System.Drawing.Size(121, 19)
        Me.lblPreparedBy.TabIndex = 54
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(26, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 14)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "Remarks :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(380, 356)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(107, 14)
        Me.Label26.TabIndex = 83
        Me.Label26.Text = "Process (C) Cost :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(570, 378)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(87, 14)
        Me.Label24.TabIndex = 81
        Me.Label24.Text = "Tool Cost / Pc.:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(405, 378)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(74, 14)
        Me.Label23.TabIndex = 80
        Me.Label23.Text = "@ Total Qty :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(197, 378)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(102, 14)
        Me.Label22.TabIndex = 79
        Me.Label22.Text = "Tooling Cost Rs. :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(614, 396)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(48, 14)
        Me.Label21.TabIndex = 78
        Me.Label21.Text = "Net Wt :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(421, 396)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(61, 14)
        Me.Label20.TabIndex = 77
        Me.Label20.Text = "Scrap Wt :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(240, 396)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(64, 14)
        Me.Label19.TabIndex = 76
        Me.Label19.Text = "Gross Wt :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(569, 356)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(96, 14)
        Me.Label18.TabIndex = 75
        Me.Label18.Text = "Operation Cost :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(39, 378)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(73, 14)
        Me.Label17.TabIndex = 74
        Me.Label17.Text = "Other Cost :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(23, 396)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(85, 14)
        Me.Label16.TabIndex = 72
        Me.Label16.Text = "Net BOP Cost :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(197, 356)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(106, 14)
        Me.Label15.TabIndex = 71
        Me.Label15.Text = "Process (B) Cost :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(7, 356)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(107, 14)
        Me.Label12.TabIndex = 70
        Me.Label12.Text = "Process (A) Cost :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(578, 336)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(85, 14)
        Me.Label11.TabIndex = 69
        Me.Label11.Text = "Std Part Cost :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(424, 336)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(60, 14)
        Me.Label10.TabIndex = 68
        Me.Label10.Text = "Net Cost :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(229, 336)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(73, 14)
        Me.Label8.TabIndex = 67
        Me.Label8.Text = "Scrap Cost :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(40, 336)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(76, 14)
        Me.Label6.TabIndex = 66
        Me.Label6.Text = "Gross Cost :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(751, 435)
        Me.SprdView.TabIndex = 52
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.CmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.CmdSave)
        Me.Frame3.Controls.Add(Me.cmdAmend)
        Me.Frame3.Controls.Add(Me.CmdModify)
        Me.Frame3.Controls.Add(Me.CmdAdd)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 432)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(751, 47)
        Me.Frame3.TabIndex = 48
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(716, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 48
        '
        'frmFGSubCostingCustomerWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(751, 479)
        Me.Controls.Add(Me.fraBase)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmFGSubCostingCustomerWise"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Customer Wise Sub Costing Entry"
        Me.fraBase.ResumeLayout(False)
        Me.fraBase.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        CType(Me.SprdPart, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        CType(Me.SprdProcess1, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        CType(Me.SprdProcess2, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        CType(Me.SprdProcess3, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage5.ResumeLayout(False)
        CType(Me.SprdMainOperation, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage6.ResumeLayout(False)
        CType(Me.SprdCostingExp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraCosting.ResumeLayout(False)
        Me.fraCosting.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

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