Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmRMCosting
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
	Public WithEvents chkV2V As System.Windows.Forms.CheckBox
	Public WithEvents cmdSearchCopy As System.Windows.Forms.Button
	Public WithEvents txtSupplierCode As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchSupplier As System.Windows.Forms.Button
	Public WithEvents txtSupplierName As System.Windows.Forms.TextBox
	Public WithEvents chkStatus As System.Windows.Forms.CheckBox
	Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
	Public WithEvents txtUnit As System.Windows.Forms.TextBox
	Public WithEvents txtCustPartNo As System.Windows.Forms.TextBox
	Public WithEvents txtCopyFrom As System.Windows.Forms.TextBox
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
	Public WithEvents SprdMainOperation As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
	Public WithEvents SprdCostingExp As AxFPSpreadADO.AxfpSpread
	Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
	Public WithEvents SSTab1 As System.Windows.Forms.TabControl
	Public WithEvents TxtRemarks As System.Windows.Forms.TextBox
	Public WithEvents txtApprovedBy As System.Windows.Forms.TextBox
	Public WithEvents txtPreparedBy As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchPrepBy As System.Windows.Forms.Button
	Public WithEvents cmdSearchAppBy As System.Windows.Forms.Button
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents lblApprovedBy As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents lblPreparedBy As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents fraCosting As System.Windows.Forms.GroupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRMCosting))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchCopy = New System.Windows.Forms.Button()
        Me.txtSupplierCode = New System.Windows.Forms.TextBox()
        Me.cmdSearchSupplier = New System.Windows.Forms.Button()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.txtCustPartNo = New System.Windows.Forms.TextBox()
        Me.txtCopyFrom = New System.Windows.Forms.TextBox()
        Me.txtItemDesc = New System.Windows.Forms.TextBox()
        Me.cmdSearchItemCode = New System.Windows.Forms.Button()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.cmdSearchWEF = New System.Windows.Forms.Button()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.cmdSearchPrepBy = New System.Windows.Forms.Button()
        Me.cmdSearchAppBy = New System.Windows.Forms.Button()
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
        Me.FraShow = New System.Windows.Forms.GroupBox()
        Me.optRoundingDown = New System.Windows.Forms.RadioButton()
        Me.optNone = New System.Windows.Forms.RadioButton()
        Me.optRoundingUP = New System.Windows.Forms.RadioButton()
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
        Me.chkV2V = New System.Windows.Forms.CheckBox()
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
        Me.SprdMainOperation = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage()
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
        Me.txtDigit = New System.Windows.Forms.TextBox()
        Me.fraBase.SuspendLayout()
        Me.FraShow.SuspendLayout()
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
        CType(Me.SprdMainOperation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage5.SuspendLayout()
        CType(Me.SprdCostingExp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraCosting.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchCopy
        '
        Me.cmdSearchCopy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCopy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCopy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCopy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCopy.Image = CType(resources.GetObject("cmdSearchCopy.Image"), System.Drawing.Image)
        Me.cmdSearchCopy.Location = New System.Drawing.Point(706, 8)
        Me.cmdSearchCopy.Name = "cmdSearchCopy"
        Me.cmdSearchCopy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCopy.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchCopy.TabIndex = 83
        Me.cmdSearchCopy.TabStop = False
        Me.cmdSearchCopy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCopy, "Search")
        Me.cmdSearchCopy.UseVisualStyleBackColor = False
        '
        'txtSupplierCode
        '
        Me.txtSupplierCode.AcceptsReturn = True
        Me.txtSupplierCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplierCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSupplierCode.Location = New System.Drawing.Point(98, 10)
        Me.txtSupplierCode.MaxLength = 0
        Me.txtSupplierCode.Name = "txtSupplierCode"
        Me.txtSupplierCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierCode.Size = New System.Drawing.Size(81, 20)
        Me.txtSupplierCode.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtSupplierCode, "Press F1 For Help")
        '
        'cmdSearchSupplier
        '
        Me.cmdSearchSupplier.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSupplier.Image = CType(resources.GetObject("cmdSearchSupplier.Image"), System.Drawing.Image)
        Me.cmdSearchSupplier.Location = New System.Drawing.Point(180, 10)
        Me.cmdSearchSupplier.Name = "cmdSearchSupplier"
        Me.cmdSearchSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSupplier.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchSupplier.TabIndex = 2
        Me.cmdSearchSupplier.TabStop = False
        Me.cmdSearchSupplier.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSupplier, "Search")
        Me.cmdSearchSupplier.UseVisualStyleBackColor = False
        '
        'txtSupplierName
        '
        Me.txtSupplierName.AcceptsReturn = True
        Me.txtSupplierName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSupplierName.Location = New System.Drawing.Point(208, 10)
        Me.txtSupplierName.MaxLength = 0
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierName.Size = New System.Drawing.Size(353, 20)
        Me.txtSupplierName.TabIndex = 81
        Me.ToolTip1.SetToolTip(Me.txtSupplierName, "Press F1 For Help")
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmendNo.Location = New System.Drawing.Point(280, 61)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(41, 20)
        Me.txtAmendNo.TabIndex = 43
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
        Me.txtUnit.Location = New System.Drawing.Point(616, 35)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(89, 20)
        Me.txtUnit.TabIndex = 36
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
        Me.txtCustPartNo.Location = New System.Drawing.Point(378, 61)
        Me.txtCustPartNo.MaxLength = 0
        Me.txtCustPartNo.Name = "txtCustPartNo"
        Me.txtCustPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPartNo.Size = New System.Drawing.Size(115, 20)
        Me.txtCustPartNo.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.txtCustPartNo, "Press F1 For Help")
        '
        'txtCopyFrom
        '
        Me.txtCopyFrom.AcceptsReturn = True
        Me.txtCopyFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtCopyFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopyFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCopyFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyFrom.Location = New System.Drawing.Point(616, 9)
        Me.txtCopyFrom.MaxLength = 0
        Me.txtCopyFrom.Name = "txtCopyFrom"
        Me.txtCopyFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyFrom.Size = New System.Drawing.Size(89, 20)
        Me.txtCopyFrom.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.txtCopyFrom, "Press F1 For Help")
        '
        'txtItemDesc
        '
        Me.txtItemDesc.AcceptsReturn = True
        Me.txtItemDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemDesc.Location = New System.Drawing.Point(208, 35)
        Me.txtItemDesc.MaxLength = 0
        Me.txtItemDesc.Name = "txtItemDesc"
        Me.txtItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemDesc.Size = New System.Drawing.Size(353, 20)
        Me.txtItemDesc.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.txtItemDesc, "Press F1 For Help")
        '
        'cmdSearchItemCode
        '
        Me.cmdSearchItemCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItemCode.Image = CType(resources.GetObject("cmdSearchItemCode.Image"), System.Drawing.Image)
        Me.cmdSearchItemCode.Location = New System.Drawing.Point(180, 35)
        Me.cmdSearchItemCode.Name = "cmdSearchItemCode"
        Me.cmdSearchItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItemCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchItemCode.TabIndex = 4
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
        Me.txtItemCode.Location = New System.Drawing.Point(98, 35)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(81, 20)
        Me.txtItemCode.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtItemCode, "Press F1 For Help")
        '
        'cmdSearchWEF
        '
        Me.cmdSearchWEF.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchWEF.Image = CType(resources.GetObject("cmdSearchWEF.Image"), System.Drawing.Image)
        Me.cmdSearchWEF.Location = New System.Drawing.Point(180, 61)
        Me.cmdSearchWEF.Name = "cmdSearchWEF"
        Me.cmdSearchWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchWEF.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchWEF.TabIndex = 6
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
        Me.txtWEF.Location = New System.Drawing.Point(98, 61)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(81, 20)
        Me.txtWEF.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtWEF, "Press F1 For Help")
        '
        'cmdSearchPrepBy
        '
        Me.cmdSearchPrepBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPrepBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPrepBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPrepBy.Image = CType(resources.GetObject("cmdSearchPrepBy.Image"), System.Drawing.Image)
        Me.cmdSearchPrepBy.Location = New System.Drawing.Point(367, 14)
        Me.cmdSearchPrepBy.Name = "cmdSearchPrepBy"
        Me.cmdSearchPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPrepBy.Size = New System.Drawing.Size(23, 20)
        Me.cmdSearchPrepBy.TabIndex = 23
        Me.cmdSearchPrepBy.TabStop = False
        Me.cmdSearchPrepBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPrepBy, "Search")
        Me.cmdSearchPrepBy.UseVisualStyleBackColor = False
        '
        'cmdSearchAppBy
        '
        Me.cmdSearchAppBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAppBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAppBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAppBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAppBy.Image = CType(resources.GetObject("cmdSearchAppBy.Image"), System.Drawing.Image)
        Me.cmdSearchAppBy.Location = New System.Drawing.Point(633, 14)
        Me.cmdSearchAppBy.Name = "cmdSearchAppBy"
        Me.cmdSearchAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAppBy.Size = New System.Drawing.Size(23, 20)
        Me.cmdSearchAppBy.TabIndex = 22
        Me.cmdSearchAppBy.TabStop = False
        Me.cmdSearchAppBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAppBy, "Search")
        Me.cmdSearchAppBy.UseVisualStyleBackColor = False
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
        Me.CmdClose.TabIndex = 16
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
        Me.CmdView.TabIndex = 15
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
        Me.CmdPreview.TabIndex = 14
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
        Me.cmdPrint.TabIndex = 13
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
        Me.CmdDelete.TabIndex = 12
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
        Me.cmdSavePrint.TabIndex = 11
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
        Me.CmdSave.TabIndex = 10
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
        Me.cmdAmend.TabIndex = 70
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
        Me.CmdModify.TabIndex = 9
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
        Me.CmdAdd.TabIndex = 8
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraBase
        '
        Me.fraBase.BackColor = System.Drawing.SystemColors.Control
        Me.fraBase.Controls.Add(Me.FraShow)
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
        Me.fraBase.Size = New System.Drawing.Size(910, 576)
        Me.fraBase.TabIndex = 18
        Me.fraBase.TabStop = False
        '
        'FraShow
        '
        Me.FraShow.BackColor = System.Drawing.SystemColors.Control
        Me.FraShow.Controls.Add(Me.txtDigit)
        Me.FraShow.Controls.Add(Me.optRoundingDown)
        Me.FraShow.Controls.Add(Me.optNone)
        Me.FraShow.Controls.Add(Me.optRoundingUP)
        Me.FraShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraShow.Location = New System.Drawing.Point(208, 507)
        Me.FraShow.Name = "FraShow"
        Me.FraShow.Padding = New System.Windows.Forms.Padding(0)
        Me.FraShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraShow.Size = New System.Drawing.Size(699, 32)
        Me.FraShow.TabIndex = 82
        Me.FraShow.TabStop = False
        Me.FraShow.Text = "Calc On"
        '
        'optRoundingDown
        '
        Me.optRoundingDown.AutoSize = True
        Me.optRoundingDown.BackColor = System.Drawing.SystemColors.Control
        Me.optRoundingDown.Cursor = System.Windows.Forms.Cursors.Default
        Me.optRoundingDown.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optRoundingDown.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRoundingDown.Location = New System.Drawing.Point(232, 11)
        Me.optRoundingDown.Name = "optRoundingDown"
        Me.optRoundingDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optRoundingDown.Size = New System.Drawing.Size(111, 18)
        Me.optRoundingDown.TabIndex = 22
        Me.optRoundingDown.Text = "Rounding Down"
        Me.optRoundingDown.UseVisualStyleBackColor = False
        '
        'optNone
        '
        Me.optNone.AutoSize = True
        Me.optNone.BackColor = System.Drawing.SystemColors.Control
        Me.optNone.Checked = True
        Me.optNone.Cursor = System.Windows.Forms.Cursors.Default
        Me.optNone.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optNone.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optNone.Location = New System.Drawing.Point(58, 11)
        Me.optNone.Name = "optNone"
        Me.optNone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optNone.Size = New System.Drawing.Size(53, 18)
        Me.optNone.TabIndex = 21
        Me.optNone.TabStop = True
        Me.optNone.Text = "None"
        Me.optNone.UseVisualStyleBackColor = False
        '
        'optRoundingUP
        '
        Me.optRoundingUP.AutoSize = True
        Me.optRoundingUP.BackColor = System.Drawing.SystemColors.Control
        Me.optRoundingUP.Cursor = System.Windows.Forms.Cursors.Default
        Me.optRoundingUP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optRoundingUP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRoundingUP.Location = New System.Drawing.Point(127, 11)
        Me.optRoundingUP.Name = "optRoundingUP"
        Me.optRoundingUP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optRoundingUP.Size = New System.Drawing.Size(94, 18)
        Me.optRoundingUP.TabIndex = 20
        Me.optRoundingUP.Text = "Rounding Up"
        Me.optRoundingUP.UseVisualStyleBackColor = False
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
        Me.txtToolCostPerPc.Location = New System.Drawing.Point(498, 487)
        Me.txtToolCostPerPc.MaxLength = 0
        Me.txtToolCostPerPc.Name = "txtToolCostPerPc"
        Me.txtToolCostPerPc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolCostPerPc.Size = New System.Drawing.Size(75, 20)
        Me.txtToolCostPerPc.TabIndex = 56
        '
        'txtToolQty
        '
        Me.txtToolQty.AcceptsReturn = True
        Me.txtToolQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToolQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolQty.Location = New System.Drawing.Point(316, 487)
        Me.txtToolQty.MaxLength = 0
        Me.txtToolQty.Name = "txtToolQty"
        Me.txtToolQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolQty.Size = New System.Drawing.Size(75, 20)
        Me.txtToolQty.TabIndex = 55
        '
        'txtToolCost
        '
        Me.txtToolCost.AcceptsReturn = True
        Me.txtToolCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToolCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolCost.Location = New System.Drawing.Point(114, 487)
        Me.txtToolCost.MaxLength = 0
        Me.txtToolCost.Name = "txtToolCost"
        Me.txtToolCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolCost.Size = New System.Drawing.Size(75, 20)
        Me.txtToolCost.TabIndex = 54
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
        Me.txtNetWt.Location = New System.Drawing.Point(832, 487)
        Me.txtNetWt.MaxLength = 0
        Me.txtNetWt.Name = "txtNetWt"
        Me.txtNetWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetWt.Size = New System.Drawing.Size(75, 20)
        Me.txtNetWt.TabIndex = 60
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
        Me.txtScrapWt.Location = New System.Drawing.Point(832, 461)
        Me.txtScrapWt.MaxLength = 0
        Me.txtScrapWt.Name = "txtScrapWt"
        Me.txtScrapWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScrapWt.Size = New System.Drawing.Size(75, 20)
        Me.txtScrapWt.TabIndex = 59
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
        Me.txtGrossWt.Location = New System.Drawing.Point(832, 435)
        Me.txtGrossWt.MaxLength = 0
        Me.txtGrossWt.Name = "txtGrossWt"
        Me.txtGrossWt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGrossWt.Size = New System.Drawing.Size(75, 20)
        Me.txtGrossWt.TabIndex = 58
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
        Me.txtOpeartionCost.Location = New System.Drawing.Point(498, 461)
        Me.txtOpeartionCost.MaxLength = 0
        Me.txtOpeartionCost.Name = "txtOpeartionCost"
        Me.txtOpeartionCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOpeartionCost.Size = New System.Drawing.Size(75, 20)
        Me.txtOpeartionCost.TabIndex = 52
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
        Me.txtOtherCost.Location = New System.Drawing.Point(666, 461)
        Me.txtOtherCost.MaxLength = 0
        Me.txtOtherCost.Name = "txtOtherCost"
        Me.txtOtherCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOtherCost.Size = New System.Drawing.Size(75, 20)
        Me.txtOtherCost.TabIndex = 53
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
        Me.txtNetBOPCost.Location = New System.Drawing.Point(114, 513)
        Me.txtNetBOPCost.MaxLength = 0
        Me.txtNetBOPCost.Name = "txtNetBOPCost"
        Me.txtNetBOPCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetBOPCost.Size = New System.Drawing.Size(75, 20)
        Me.txtNetBOPCost.TabIndex = 57
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
        Me.txtProcessCost_B.Location = New System.Drawing.Point(316, 461)
        Me.txtProcessCost_B.MaxLength = 0
        Me.txtProcessCost_B.Name = "txtProcessCost_B"
        Me.txtProcessCost_B.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProcessCost_B.Size = New System.Drawing.Size(75, 20)
        Me.txtProcessCost_B.TabIndex = 51
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
        Me.txtProcessCost_A.Location = New System.Drawing.Point(114, 461)
        Me.txtProcessCost_A.MaxLength = 0
        Me.txtProcessCost_A.Name = "txtProcessCost_A"
        Me.txtProcessCost_A.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProcessCost_A.Size = New System.Drawing.Size(75, 20)
        Me.txtProcessCost_A.TabIndex = 50
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
        Me.txtStdPartCost.Location = New System.Drawing.Point(666, 435)
        Me.txtStdPartCost.MaxLength = 0
        Me.txtStdPartCost.Name = "txtStdPartCost"
        Me.txtStdPartCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStdPartCost.Size = New System.Drawing.Size(75, 20)
        Me.txtStdPartCost.TabIndex = 49
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
        Me.txtNetCost.Location = New System.Drawing.Point(498, 435)
        Me.txtNetCost.MaxLength = 0
        Me.txtNetCost.Name = "txtNetCost"
        Me.txtNetCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetCost.Size = New System.Drawing.Size(75, 20)
        Me.txtNetCost.TabIndex = 48
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
        Me.txtScrapCost.Location = New System.Drawing.Point(316, 435)
        Me.txtScrapCost.MaxLength = 0
        Me.txtScrapCost.Name = "txtScrapCost"
        Me.txtScrapCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScrapCost.Size = New System.Drawing.Size(75, 20)
        Me.txtScrapCost.TabIndex = 47
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
        Me.txtGrossCost.Location = New System.Drawing.Point(114, 435)
        Me.txtGrossCost.MaxLength = 0
        Me.txtGrossCost.Name = "txtGrossCost"
        Me.txtGrossCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGrossCost.Size = New System.Drawing.Size(75, 20)
        Me.txtGrossCost.TabIndex = 46
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkV2V)
        Me.Frame1.Controls.Add(Me.cmdSearchCopy)
        Me.Frame1.Controls.Add(Me.txtSupplierCode)
        Me.Frame1.Controls.Add(Me.cmdSearchSupplier)
        Me.Frame1.Controls.Add(Me.txtSupplierName)
        Me.Frame1.Controls.Add(Me.chkStatus)
        Me.Frame1.Controls.Add(Me.txtAmendNo)
        Me.Frame1.Controls.Add(Me.txtUnit)
        Me.Frame1.Controls.Add(Me.txtCustPartNo)
        Me.Frame1.Controls.Add(Me.txtCopyFrom)
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
        Me.Frame1.Size = New System.Drawing.Size(751, 94)
        Me.Frame1.TabIndex = 32
        Me.Frame1.TabStop = False
        '
        'chkV2V
        '
        Me.chkV2V.AutoSize = True
        Me.chkV2V.BackColor = System.Drawing.SystemColors.Control
        Me.chkV2V.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkV2V.Enabled = False
        Me.chkV2V.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkV2V.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkV2V.Location = New System.Drawing.Point(654, 64)
        Me.chkV2V.Name = "chkV2V"
        Me.chkV2V.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkV2V.Size = New System.Drawing.Size(97, 18)
        Me.chkV2V.TabIndex = 84
        Me.chkV2V.Text = "V2V Supplier"
        Me.chkV2V.UseVisualStyleBackColor = False
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Enabled = False
        Me.chkStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(498, 64)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(149, 18)
        Me.chkStatus.TabIndex = 45
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
        Me.Label25.Location = New System.Drawing.Point(10, 13)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(85, 14)
        Me.Label25.TabIndex = 82
        Me.Label25.Text = "Vendor Code :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(211, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(70, 14)
        Me.Label3.TabIndex = 44
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
        Me.lblMKey.TabIndex = 42
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
        Me.Label7.Location = New System.Drawing.Point(578, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(34, 14)
        Me.Label7.TabIndex = 41
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
        Me.Label4.Location = New System.Drawing.Point(326, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(52, 14)
        Me.Label4.TabIndex = 40
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
        Me.Label2.Location = New System.Drawing.Point(577, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(41, 14)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Copy :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(26, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(70, 14)
        Me.Label1.TabIndex = 38
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
        Me.Label9.Location = New System.Drawing.Point(46, 64)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(42, 14)
        Me.Label9.TabIndex = 37
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
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 24)
        Me.SSTab1.Location = New System.Drawing.Point(2, 94)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(910, 336)
        Me.SSTab1.TabIndex = 19
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.SprdMain)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(902, 304)
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
        Me.SprdMain.Size = New System.Drawing.Size(902, 304)
        Me.SprdMain.TabIndex = 0
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.SprdPart)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(902, 304)
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
        Me.SprdPart.Size = New System.Drawing.Size(902, 304)
        Me.SprdPart.TabIndex = 69
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.SprdProcess1)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(902, 304)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "Press Process Cost"
        '
        'SprdProcess1
        '
        Me.SprdProcess1.DataSource = Nothing
        Me.SprdProcess1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdProcess1.Location = New System.Drawing.Point(0, 0)
        Me.SprdProcess1.Name = "SprdProcess1"
        Me.SprdProcess1.OcxState = CType(resources.GetObject("SprdProcess1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdProcess1.Size = New System.Drawing.Size(902, 304)
        Me.SprdProcess1.TabIndex = 7
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.SprdProcess2)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(902, 304)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "Surface Process Cost"
        '
        'SprdProcess2
        '
        Me.SprdProcess2.DataSource = Nothing
        Me.SprdProcess2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdProcess2.Location = New System.Drawing.Point(0, 0)
        Me.SprdProcess2.Name = "SprdProcess2"
        Me.SprdProcess2.OcxState = CType(resources.GetObject("SprdProcess2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdProcess2.Size = New System.Drawing.Size(902, 304)
        Me.SprdProcess2.TabIndex = 71
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me.SprdMainOperation)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(902, 304)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "Weld Operation"
        '
        'SprdMainOperation
        '
        Me.SprdMainOperation.DataSource = Nothing
        Me.SprdMainOperation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMainOperation.Location = New System.Drawing.Point(0, 0)
        Me.SprdMainOperation.Name = "SprdMainOperation"
        Me.SprdMainOperation.OcxState = CType(resources.GetObject("SprdMainOperation.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainOperation.Size = New System.Drawing.Size(902, 304)
        Me.SprdMainOperation.TabIndex = 68
        '
        '_SSTab1_TabPage5
        '
        Me._SSTab1_TabPage5.Controls.Add(Me.SprdCostingExp)
        Me._SSTab1_TabPage5.Location = New System.Drawing.Point(4, 28)
        Me._SSTab1_TabPage5.Name = "_SSTab1_TabPage5"
        Me._SSTab1_TabPage5.Size = New System.Drawing.Size(902, 304)
        Me._SSTab1_TabPage5.TabIndex = 5
        Me._SSTab1_TabPage5.Text = "Costing Expenses"
        '
        'SprdCostingExp
        '
        Me.SprdCostingExp.DataSource = Nothing
        Me.SprdCostingExp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdCostingExp.Location = New System.Drawing.Point(0, 0)
        Me.SprdCostingExp.Name = "SprdCostingExp"
        Me.SprdCostingExp.OcxState = CType(resources.GetObject("SprdCostingExp.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdCostingExp.Size = New System.Drawing.Size(902, 304)
        Me.SprdCostingExp.TabIndex = 77
        '
        'fraCosting
        '
        Me.fraCosting.BackColor = System.Drawing.SystemColors.Control
        Me.fraCosting.Controls.Add(Me.TxtRemarks)
        Me.fraCosting.Controls.Add(Me.txtApprovedBy)
        Me.fraCosting.Controls.Add(Me.txtPreparedBy)
        Me.fraCosting.Controls.Add(Me.cmdSearchPrepBy)
        Me.fraCosting.Controls.Add(Me.cmdSearchAppBy)
        Me.fraCosting.Controls.Add(Me.Label14)
        Me.fraCosting.Controls.Add(Me.lblApprovedBy)
        Me.fraCosting.Controls.Add(Me.Label13)
        Me.fraCosting.Controls.Add(Me.lblPreparedBy)
        Me.fraCosting.Controls.Add(Me.Label5)
        Me.fraCosting.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCosting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCosting.Location = New System.Drawing.Point(2, 534)
        Me.fraCosting.Name = "fraCosting"
        Me.fraCosting.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCosting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCosting.Size = New System.Drawing.Size(910, 42)
        Me.fraCosting.TabIndex = 20
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
        Me.TxtRemarks.Location = New System.Drawing.Point(73, 14)
        Me.TxtRemarks.MaxLength = 0
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtRemarks.Size = New System.Drawing.Size(139, 19)
        Me.TxtRemarks.TabIndex = 26
        '
        'txtApprovedBy
        '
        Me.txtApprovedBy.AcceptsReturn = True
        Me.txtApprovedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtApprovedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtApprovedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtApprovedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApprovedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtApprovedBy.Location = New System.Drawing.Point(563, 14)
        Me.txtApprovedBy.MaxLength = 0
        Me.txtApprovedBy.Name = "txtApprovedBy"
        Me.txtApprovedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtApprovedBy.Size = New System.Drawing.Size(69, 20)
        Me.txtApprovedBy.TabIndex = 25
        '
        'txtPreparedBy
        '
        Me.txtPreparedBy.AcceptsReturn = True
        Me.txtPreparedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreparedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPreparedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreparedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPreparedBy.Location = New System.Drawing.Point(297, 14)
        Me.txtPreparedBy.MaxLength = 0
        Me.txtPreparedBy.Name = "txtPreparedBy"
        Me.txtPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPreparedBy.Size = New System.Drawing.Size(69, 20)
        Me.txtPreparedBy.TabIndex = 24
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(478, 18)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(83, 14)
        Me.Label14.TabIndex = 31
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
        Me.lblApprovedBy.Location = New System.Drawing.Point(657, 14)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblApprovedBy.Size = New System.Drawing.Size(95, 19)
        Me.lblApprovedBy.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(215, 18)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(80, 14)
        Me.Label13.TabIndex = 29
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
        Me.lblPreparedBy.Location = New System.Drawing.Point(391, 14)
        Me.lblPreparedBy.Name = "lblPreparedBy"
        Me.lblPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPreparedBy.Size = New System.Drawing.Size(85, 19)
        Me.lblPreparedBy.TabIndex = 28
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(11, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 14)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Remarks :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(409, 490)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(87, 14)
        Me.Label24.TabIndex = 80
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
        Me.Label23.Location = New System.Drawing.Point(240, 490)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(74, 14)
        Me.Label23.TabIndex = 79
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
        Me.Label22.Location = New System.Drawing.Point(17, 490)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(99, 14)
        Me.Label22.TabIndex = 78
        Me.Label22.Text = "Tooling Cost Rs. "
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(781, 490)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(48, 14)
        Me.Label21.TabIndex = 76
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
        Me.Label20.Location = New System.Drawing.Point(765, 466)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(65, 14)
        Me.Label20.TabIndex = 75
        Me.Label20.Text = "Scrap Wtt :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(766, 437)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(64, 14)
        Me.Label19.TabIndex = 74
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
        Me.Label18.Location = New System.Drawing.Point(400, 466)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(96, 14)
        Me.Label18.TabIndex = 73
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
        Me.Label17.Location = New System.Drawing.Point(590, 466)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(73, 14)
        Me.Label17.TabIndex = 72
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
        Me.Label16.Location = New System.Drawing.Point(31, 516)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(85, 14)
        Me.Label16.TabIndex = 67
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
        Me.Label15.Location = New System.Drawing.Point(208, 466)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(106, 14)
        Me.Label15.TabIndex = 66
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
        Me.Label12.Location = New System.Drawing.Point(9, 466)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(107, 14)
        Me.Label12.TabIndex = 65
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
        Me.Label11.Location = New System.Drawing.Point(578, 437)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(85, 14)
        Me.Label11.TabIndex = 64
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
        Me.Label10.Location = New System.Drawing.Point(436, 437)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(60, 14)
        Me.Label10.TabIndex = 63
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
        Me.Label8.Location = New System.Drawing.Point(241, 437)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(73, 14)
        Me.Label8.TabIndex = 62
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
        Me.Label6.Location = New System.Drawing.Point(40, 437)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(76, 14)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Gross Cost :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(912, 572)
        Me.SprdView.TabIndex = 21
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
        Me.Frame3.Location = New System.Drawing.Point(2, 570)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(906, 48)
        Me.Frame3.TabIndex = 17
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(716, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 17
        '
        'txtDigit
        '
        Me.txtDigit.AcceptsReturn = True
        Me.txtDigit.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigit.Location = New System.Drawing.Point(357, 9)
        Me.txtDigit.MaxLength = 0
        Me.txtDigit.Name = "txtDigit"
        Me.txtDigit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigit.Size = New System.Drawing.Size(75, 20)
        Me.txtDigit.TabIndex = 56
        '
        'FrmRMCosting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(911, 621)
        Me.Controls.Add(Me.fraBase)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "FrmRMCosting"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Costing (BOP Items)"
        Me.fraBase.ResumeLayout(False)
        Me.fraBase.PerformLayout()
        Me.FraShow.ResumeLayout(False)
        Me.FraShow.PerformLayout()
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
        CType(Me.SprdMainOperation, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage5.ResumeLayout(False)
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

    Public WithEvents FraShow As GroupBox
    Public WithEvents optRoundingDown As RadioButton
    Public WithEvents optNone As RadioButton
    Public WithEvents optRoundingUP As RadioButton
    Public WithEvents txtDigit As TextBox
#End Region
End Class