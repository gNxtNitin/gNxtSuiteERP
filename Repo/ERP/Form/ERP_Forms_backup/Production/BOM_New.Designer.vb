Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmBOMNew
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
        'Me.MDIParent = Production.Master

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
    Public WithEvents CmdPopFromFile As System.Windows.Forms.Button
    Public WithEvents chkApproved As System.Windows.Forms.CheckBox
    Public WithEvents cboProcessType As System.Windows.Forms.ComboBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdTool As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdOthers As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraBOMDetail As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents SprdMainMWS As AxFPSpreadADO.AxfpSpread
    Public WithEvents txtWL As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents txtSA As System.Windows.Forms.TextBox
    Public WithEvents SprdMainPLT As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents txtSAPS_I As System.Windows.Forms.TextBox
    Public WithEvents txtSAPS_E As System.Windows.Forms.TextBox
    Public WithEvents SprdMainPPS As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents txtSAPC As System.Windows.Forms.TextBox
    Public WithEvents SprdMainPC As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents txtOutPutQty As System.Windows.Forms.TextBox
    Public WithEvents chkBOP As System.Windows.Forms.CheckBox
    Public WithEvents txtProcessCost As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCopyProdCode As System.Windows.Forms.Button
    Public WithEvents txtCopyProductDesc As System.Windows.Forms.TextBox
    Public WithEvents txtCopyAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtCopyProductCode As System.Windows.Forms.TextBox
    Public WithEvents chkStatus As System.Windows.Forms.CheckBox
    Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
    Public WithEvents chkScrap As System.Windows.Forms.CheckBox
    Public WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtApprovedBy As System.Windows.Forms.TextBox
    Public WithEvents txtPreparedBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchPrepBy As System.Windows.Forms.Button
    Public WithEvents cmdSearchAppBy As System.Windows.Forms.Button
    Public WithEvents cmdSearchWEF As System.Windows.Forms.Button
    Public WithEvents txtUnit As System.Windows.Forms.TextBox
    Public WithEvents txtCustPartNo As System.Windows.Forms.TextBox
    Public WithEvents txtModelNo As System.Windows.Forms.TextBox
    Public WithEvents txtProductDesc As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchProdCode As System.Windows.Forms.Button
    Public WithEvents txtProductCode As System.Windows.Forms.TextBox
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents SprdSeq As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMainRel As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents lblApproval As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblOldWEF As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents lblCopyMKey As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents lblApprovedBy As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblPreparedBy As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
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
    Public WithEvents cmdAmend As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents lblType As System.Windows.Forms.Label
    Public WithEvents lblDetail As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public CommonDialogOpen As System.Windows.Forms.OpenFileDialog
    Public CommonDialogSave As System.Windows.Forms.SaveFileDialog
    Public CommonDialogFont As System.Windows.Forms.FontDialog
    Public CommonDialogColor As System.Windows.Forms.ColorDialog
    Public CommonDialogPrint As System.Windows.Forms.PrintDialog
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBOMNew))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtOutPutQty = New System.Windows.Forms.TextBox()
        Me.cmdSearchCopyProdCode = New System.Windows.Forms.Button()
        Me.txtCopyProductDesc = New System.Windows.Forms.TextBox()
        Me.txtCopyAmendNo = New System.Windows.Forms.TextBox()
        Me.txtCopyProductCode = New System.Windows.Forms.TextBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.cmdSearchPrepBy = New System.Windows.Forms.Button()
        Me.cmdSearchAppBy = New System.Windows.Forms.Button()
        Me.cmdSearchWEF = New System.Windows.Forms.Button()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.txtCustPartNo = New System.Windows.Forms.TextBox()
        Me.txtModelNo = New System.Windows.Forms.TextBox()
        Me.txtProductDesc = New System.Windows.Forms.TextBox()
        Me.cmdSearchProdCode = New System.Windows.Forms.Button()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdAmend = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraBase = New System.Windows.Forms.GroupBox()
        Me.CmdPopFromFile = New System.Windows.Forms.Button()
        Me.chkApproved = New System.Windows.Forms.CheckBox()
        Me.cboProcessType = New System.Windows.Forms.ComboBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.fraBOMDetail = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage()
        Me.SprdTool = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage6 = New System.Windows.Forms.TabPage()
        Me.SprdOthers = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.SprdMainMWS = New AxFPSpreadADO.AxfpSpread()
        Me.txtWL = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.txtSA = New System.Windows.Forms.TextBox()
        Me.SprdMainPLT = New AxFPSpreadADO.AxfpSpread()
        Me.Label10 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.txtSAPS_I = New System.Windows.Forms.TextBox()
        Me.txtSAPS_E = New System.Windows.Forms.TextBox()
        Me.SprdMainPPS = New AxFPSpreadADO.AxfpSpread()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me.txtSAPC = New System.Windows.Forms.TextBox()
        Me.SprdMainPC = New AxFPSpreadADO.AxfpSpread()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.chkBOP = New System.Windows.Forms.CheckBox()
        Me.txtProcessCost = New System.Windows.Forms.TextBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.chkScrap = New System.Windows.Forms.CheckBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.txtApprovedBy = New System.Windows.Forms.TextBox()
        Me.txtPreparedBy = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.SprdSeq = New AxFPSpreadADO.AxfpSpread()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdMainRel = New AxFPSpreadADO.AxfpSpread()
        Me.lblApproval = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblOldWEF = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblCopyMKey = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblApprovedBy = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblPreparedBy = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblDetail = New System.Windows.Forms.Label()
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog()
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog()
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.fraBase.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.fraBOMDetail.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage5.SuspendLayout()
        CType(Me.SprdTool, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage6.SuspendLayout()
        CType(Me.SprdOthers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        CType(Me.SprdMainMWS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage2.SuspendLayout()
        CType(Me.SprdMainPLT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage3.SuspendLayout()
        CType(Me.SprdMainPPS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage4.SuspendLayout()
        CType(Me.SprdMainPC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdSeq, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdMainRel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtOutPutQty
        '
        Me.txtOutPutQty.AcceptsReturn = True
        Me.txtOutPutQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtOutPutQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutPutQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOutPutQty.Enabled = False
        Me.txtOutPutQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutPutQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOutPutQty.Location = New System.Drawing.Point(828, 37)
        Me.txtOutPutQty.MaxLength = 0
        Me.txtOutPutQty.Name = "txtOutPutQty"
        Me.txtOutPutQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOutPutQty.Size = New System.Drawing.Size(53, 20)
        Me.txtOutPutQty.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtOutPutQty, "Press F1 For Help")
        '
        'cmdSearchCopyProdCode
        '
        Me.cmdSearchCopyProdCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCopyProdCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCopyProdCode.Enabled = False
        Me.cmdSearchCopyProdCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCopyProdCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCopyProdCode.Image = CType(resources.GetObject("cmdSearchCopyProdCode.Image"), System.Drawing.Image)
        Me.cmdSearchCopyProdCode.Location = New System.Drawing.Point(184, 65)
        Me.cmdSearchCopyProdCode.Name = "cmdSearchCopyProdCode"
        Me.cmdSearchCopyProdCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCopyProdCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchCopyProdCode.TabIndex = 12
        Me.cmdSearchCopyProdCode.TabStop = False
        Me.cmdSearchCopyProdCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCopyProdCode, "Search")
        Me.cmdSearchCopyProdCode.UseVisualStyleBackColor = False
        '
        'txtCopyProductDesc
        '
        Me.txtCopyProductDesc.AcceptsReturn = True
        Me.txtCopyProductDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtCopyProductDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopyProductDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCopyProductDesc.Enabled = False
        Me.txtCopyProductDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyProductDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyProductDesc.Location = New System.Drawing.Point(212, 65)
        Me.txtCopyProductDesc.MaxLength = 0
        Me.txtCopyProductDesc.Name = "txtCopyProductDesc"
        Me.txtCopyProductDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyProductDesc.Size = New System.Drawing.Size(181, 20)
        Me.txtCopyProductDesc.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtCopyProductDesc, "Press F1 For Help")
        '
        'txtCopyAmendNo
        '
        Me.txtCopyAmendNo.AcceptsReturn = True
        Me.txtCopyAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCopyAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopyAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCopyAmendNo.Enabled = False
        Me.txtCopyAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyAmendNo.Location = New System.Drawing.Point(461, 65)
        Me.txtCopyAmendNo.MaxLength = 0
        Me.txtCopyAmendNo.Name = "txtCopyAmendNo"
        Me.txtCopyAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyAmendNo.Size = New System.Drawing.Size(83, 20)
        Me.txtCopyAmendNo.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.txtCopyAmendNo, "Press F1 For Help")
        '
        'txtCopyProductCode
        '
        Me.txtCopyProductCode.AcceptsReturn = True
        Me.txtCopyProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCopyProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopyProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCopyProductCode.Enabled = False
        Me.txtCopyProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyProductCode.Location = New System.Drawing.Point(102, 65)
        Me.txtCopyProductCode.MaxLength = 0
        Me.txtCopyProductCode.Name = "txtCopyProductCode"
        Me.txtCopyProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyProductCode.Size = New System.Drawing.Size(81, 20)
        Me.txtCopyProductCode.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtCopyProductCode, "Press F1 For Help")
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmendNo.Location = New System.Drawing.Point(828, 9)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(53, 20)
        Me.txtAmendNo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtAmendNo, "Press F1 For Help")
        '
        'cmdSearchPrepBy
        '
        Me.cmdSearchPrepBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPrepBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPrepBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPrepBy.Image = CType(resources.GetObject("cmdSearchPrepBy.Image"), System.Drawing.Image)
        Me.cmdSearchPrepBy.Location = New System.Drawing.Point(491, 520)
        Me.cmdSearchPrepBy.Name = "cmdSearchPrepBy"
        Me.cmdSearchPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPrepBy.Size = New System.Drawing.Size(23, 19)
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
        Me.cmdSearchAppBy.Location = New System.Drawing.Point(491, 542)
        Me.cmdSearchAppBy.Name = "cmdSearchAppBy"
        Me.cmdSearchAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAppBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchAppBy.TabIndex = 25
        Me.cmdSearchAppBy.TabStop = False
        Me.cmdSearchAppBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAppBy, "Search")
        Me.cmdSearchAppBy.UseVisualStyleBackColor = False
        '
        'cmdSearchWEF
        '
        Me.cmdSearchWEF.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchWEF.Image = CType(resources.GetObject("cmdSearchWEF.Image"), System.Drawing.Image)
        Me.cmdSearchWEF.Location = New System.Drawing.Point(184, 37)
        Me.cmdSearchWEF.Name = "cmdSearchWEF"
        Me.cmdSearchWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchWEF.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchWEF.TabIndex = 7
        Me.cmdSearchWEF.TabStop = False
        Me.cmdSearchWEF.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchWEF, "Search")
        Me.cmdSearchWEF.UseVisualStyleBackColor = False
        '
        'txtUnit
        '
        Me.txtUnit.AcceptsReturn = True
        Me.txtUnit.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnit.Location = New System.Drawing.Point(718, 10)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(39, 20)
        Me.txtUnit.TabIndex = 4
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
        Me.txtCustPartNo.Location = New System.Drawing.Point(461, 37)
        Me.txtCustPartNo.MaxLength = 0
        Me.txtCustPartNo.Name = "txtCustPartNo"
        Me.txtCustPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPartNo.Size = New System.Drawing.Size(83, 20)
        Me.txtCustPartNo.TabIndex = 9
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
        Me.txtModelNo.Location = New System.Drawing.Point(278, 37)
        Me.txtModelNo.MaxLength = 0
        Me.txtModelNo.Name = "txtModelNo"
        Me.txtModelNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModelNo.Size = New System.Drawing.Size(115, 20)
        Me.txtModelNo.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtModelNo, "Press F1 For Help")
        '
        'txtProductDesc
        '
        Me.txtProductDesc.AcceptsReturn = True
        Me.txtProductDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductDesc.Location = New System.Drawing.Point(212, 9)
        Me.txtProductDesc.MaxLength = 0
        Me.txtProductDesc.Name = "txtProductDesc"
        Me.txtProductDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductDesc.Size = New System.Drawing.Size(331, 20)
        Me.txtProductDesc.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtProductDesc, "Press F1 For Help")
        '
        'cmdSearchProdCode
        '
        Me.cmdSearchProdCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProdCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProdCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProdCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProdCode.Image = CType(resources.GetObject("cmdSearchProdCode.Image"), System.Drawing.Image)
        Me.cmdSearchProdCode.Location = New System.Drawing.Point(184, 9)
        Me.cmdSearchProdCode.Name = "cmdSearchProdCode"
        Me.cmdSearchProdCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProdCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchProdCode.TabIndex = 2
        Me.cmdSearchProdCode.TabStop = False
        Me.cmdSearchProdCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProdCode, "Search")
        Me.cmdSearchProdCode.UseVisualStyleBackColor = False
        '
        'txtProductCode
        '
        Me.txtProductCode.AcceptsReturn = True
        Me.txtProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductCode.Location = New System.Drawing.Point(102, 9)
        Me.txtProductCode.MaxLength = 0
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.Size = New System.Drawing.Size(81, 20)
        Me.txtProductCode.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtProductCode, "Press F1 For Help")
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(102, 37)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(81, 20)
        Me.txtWEF.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtWEF, "Press F1 For Help")
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(690, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 34)
        Me.CmdClose.TabIndex = 33
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
        Me.CmdView.Location = New System.Drawing.Point(624, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 34)
        Me.CmdView.TabIndex = 32
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
        Me.CmdPreview.Location = New System.Drawing.Point(558, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 34)
        Me.CmdPreview.TabIndex = 31
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
        Me.cmdPrint.Location = New System.Drawing.Point(492, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdPrint.TabIndex = 30
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
        Me.CmdDelete.Location = New System.Drawing.Point(426, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 34)
        Me.CmdDelete.TabIndex = 29
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(360, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdSavePrint.TabIndex = 28
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdAmend
        '
        Me.cmdAmend.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmend.Image = CType(resources.GetObject("cmdAmend.Image"), System.Drawing.Image)
        Me.cmdAmend.Location = New System.Drawing.Point(294, 10)
        Me.cmdAmend.Name = "cmdAmend"
        Me.cmdAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmend.Size = New System.Drawing.Size(67, 34)
        Me.cmdAmend.TabIndex = 50
        Me.cmdAmend.Text = "A&mendment"
        Me.cmdAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAmend, "Modify Record")
        Me.cmdAmend.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(228, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 34)
        Me.CmdSave.TabIndex = 27
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
        Me.CmdModify.Location = New System.Drawing.Point(162, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 34)
        Me.CmdModify.TabIndex = 26
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
        Me.CmdAdd.Location = New System.Drawing.Point(96, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 34)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraBase
        '
        Me.fraBase.BackColor = System.Drawing.SystemColors.Control
        Me.fraBase.Controls.Add(Me.CmdPopFromFile)
        Me.fraBase.Controls.Add(Me.chkApproved)
        Me.fraBase.Controls.Add(Me.cboProcessType)
        Me.fraBase.Controls.Add(Me.SSTab1)
        Me.fraBase.Controls.Add(Me.txtOutPutQty)
        Me.fraBase.Controls.Add(Me.chkBOP)
        Me.fraBase.Controls.Add(Me.txtProcessCost)
        Me.fraBase.Controls.Add(Me.cmdSearchCopyProdCode)
        Me.fraBase.Controls.Add(Me.txtCopyProductDesc)
        Me.fraBase.Controls.Add(Me.txtCopyAmendNo)
        Me.fraBase.Controls.Add(Me.txtCopyProductCode)
        Me.fraBase.Controls.Add(Me.chkStatus)
        Me.fraBase.Controls.Add(Me.txtAmendNo)
        Me.fraBase.Controls.Add(Me.chkScrap)
        Me.fraBase.Controls.Add(Me.TxtRemarks)
        Me.fraBase.Controls.Add(Me.txtApprovedBy)
        Me.fraBase.Controls.Add(Me.txtPreparedBy)
        Me.fraBase.Controls.Add(Me.cmdSearchPrepBy)
        Me.fraBase.Controls.Add(Me.cmdSearchAppBy)
        Me.fraBase.Controls.Add(Me.cmdSearchWEF)
        Me.fraBase.Controls.Add(Me.txtUnit)
        Me.fraBase.Controls.Add(Me.txtCustPartNo)
        Me.fraBase.Controls.Add(Me.txtModelNo)
        Me.fraBase.Controls.Add(Me.txtProductDesc)
        Me.fraBase.Controls.Add(Me.cmdSearchProdCode)
        Me.fraBase.Controls.Add(Me.txtProductCode)
        Me.fraBase.Controls.Add(Me.txtWEF)
        Me.fraBase.Controls.Add(Me.Frame1)
        Me.fraBase.Controls.Add(Me.Frame2)
        Me.fraBase.Controls.Add(Me.lblApproval)
        Me.fraBase.Controls.Add(Me.Label12)
        Me.fraBase.Controls.Add(Me.lblOldWEF)
        Me.fraBase.Controls.Add(Me.Label16)
        Me.fraBase.Controls.Add(Me.Label15)
        Me.fraBase.Controls.Add(Me.lblCopyMKey)
        Me.fraBase.Controls.Add(Me.Label8)
        Me.fraBase.Controls.Add(Me.Label3)
        Me.fraBase.Controls.Add(Me.Label6)
        Me.fraBase.Controls.Add(Me.Label14)
        Me.fraBase.Controls.Add(Me.lblApprovedBy)
        Me.fraBase.Controls.Add(Me.Label13)
        Me.fraBase.Controls.Add(Me.lblPreparedBy)
        Me.fraBase.Controls.Add(Me.Label5)
        Me.fraBase.Controls.Add(Me.Label7)
        Me.fraBase.Controls.Add(Me.Label4)
        Me.fraBase.Controls.Add(Me.Label2)
        Me.fraBase.Controls.Add(Me.lblMKey)
        Me.fraBase.Controls.Add(Me.Label1)
        Me.fraBase.Controls.Add(Me.Label9)
        Me.fraBase.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBase.Location = New System.Drawing.Point(0, -4)
        Me.fraBase.Name = "fraBase"
        Me.fraBase.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBase.Size = New System.Drawing.Size(898, 570)
        Me.fraBase.TabIndex = 35
        Me.fraBase.TabStop = False
        '
        'CmdPopFromFile
        '
        Me.CmdPopFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPopFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopFromFile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopFromFile.Location = New System.Drawing.Point(548, 65)
        Me.CmdPopFromFile.Name = "CmdPopFromFile"
        Me.CmdPopFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopFromFile.Size = New System.Drawing.Size(97, 23)
        Me.CmdPopFromFile.TabIndex = 81
        Me.CmdPopFromFile.Text = "Populate From File"
        Me.CmdPopFromFile.UseVisualStyleBackColor = False
        '
        'chkApproved
        '
        Me.chkApproved.AutoSize = True
        Me.chkApproved.BackColor = System.Drawing.SystemColors.Control
        Me.chkApproved.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkApproved.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApproved.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkApproved.Location = New System.Drawing.Point(553, 10)
        Me.chkApproved.Name = "chkApproved"
        Me.chkApproved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkApproved.Size = New System.Drawing.Size(80, 20)
        Me.chkApproved.TabIndex = 79
        Me.chkApproved.Text = "Approved"
        Me.chkApproved.UseVisualStyleBackColor = False
        '
        'cboProcessType
        '
        Me.cboProcessType.BackColor = System.Drawing.SystemColors.Window
        Me.cboProcessType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboProcessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProcessType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboProcessType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboProcessType.Location = New System.Drawing.Point(86, 542)
        Me.cboProcessType.Name = "cboProcessType"
        Me.cboProcessType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboProcessType.Size = New System.Drawing.Size(99, 22)
        Me.cboProcessType.TabIndex = 63
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage5)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage6)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(-1, 222)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(897, 288)
        Me.SSTab1.TabIndex = 61
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.fraBOMDetail)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(889, 262)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "BOM Detail"
        '
        'fraBOMDetail
        '
        Me.fraBOMDetail.BackColor = System.Drawing.SystemColors.Control
        Me.fraBOMDetail.Controls.Add(Me.SprdMain)
        Me.fraBOMDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBOMDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBOMDetail.Location = New System.Drawing.Point(2, -5)
        Me.fraBOMDetail.Name = "fraBOMDetail"
        Me.fraBOMDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBOMDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBOMDetail.Size = New System.Drawing.Size(886, 267)
        Me.fraBOMDetail.TabIndex = 62
        Me.fraBOMDetail.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 12)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(882, 250)
        Me.SprdMain.TabIndex = 17
        '
        '_SSTab1_TabPage5
        '
        Me._SSTab1_TabPage5.Controls.Add(Me.SprdTool)
        Me._SSTab1_TabPage5.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage5.Name = "_SSTab1_TabPage5"
        Me._SSTab1_TabPage5.Size = New System.Drawing.Size(889, 262)
        Me._SSTab1_TabPage5.TabIndex = 5
        Me._SSTab1_TabPage5.Text = "Tools Details"
        '
        'SprdTool
        '
        Me.SprdTool.DataSource = Nothing
        Me.SprdTool.Location = New System.Drawing.Point(2, 2)
        Me.SprdTool.Name = "SprdTool"
        Me.SprdTool.OcxState = CType(resources.GetObject("SprdTool.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdTool.Size = New System.Drawing.Size(884, 258)
        Me.SprdTool.TabIndex = 17
        '
        '_SSTab1_TabPage6
        '
        Me._SSTab1_TabPage6.Controls.Add(Me.SprdOthers)
        Me._SSTab1_TabPage6.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage6.Name = "_SSTab1_TabPage6"
        Me._SSTab1_TabPage6.Size = New System.Drawing.Size(889, 262)
        Me._SSTab1_TabPage6.TabIndex = 6
        Me._SSTab1_TabPage6.Text = "Others Consumption"
        Me._SSTab1_TabPage6.UseVisualStyleBackColor = True
        '
        'SprdOthers
        '
        Me.SprdOthers.DataSource = Nothing
        Me.SprdOthers.Location = New System.Drawing.Point(2, 2)
        Me.SprdOthers.Name = "SprdOthers"
        Me.SprdOthers.OcxState = CType(resources.GetObject("SprdOthers.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdOthers.Size = New System.Drawing.Size(884, 258)
        Me.SprdOthers.TabIndex = 17
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.SprdMainMWS)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtWL)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label11)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(889, 262)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Welding Detail"
        '
        'SprdMainMWS
        '
        Me.SprdMainMWS.DataSource = Nothing
        Me.SprdMainMWS.Location = New System.Drawing.Point(4, 31)
        Me.SprdMainMWS.Name = "SprdMainMWS"
        Me.SprdMainMWS.OcxState = CType(resources.GetObject("SprdMainMWS.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainMWS.Size = New System.Drawing.Size(884, 229)
        Me.SprdMainMWS.TabIndex = 73
        '
        'txtWL
        '
        Me.txtWL.AcceptsReturn = True
        Me.txtWL.BackColor = System.Drawing.SystemColors.Window
        Me.txtWL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWL.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWL.Location = New System.Drawing.Point(150, 6)
        Me.txtWL.MaxLength = 0
        Me.txtWL.Name = "txtWL"
        Me.txtWL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWL.Size = New System.Drawing.Size(53, 20)
        Me.txtWL.TabIndex = 74
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(26, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(108, 14)
        Me.Label11.TabIndex = 78
        Me.Label11.Text = "Welding Area (Inch) :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.txtSA)
        Me._SSTab1_TabPage2.Controls.Add(Me.SprdMainPLT)
        Me._SSTab1_TabPage2.Controls.Add(Me.Label10)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(889, 262)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "Plating Detail"
        '
        'txtSA
        '
        Me.txtSA.AcceptsReturn = True
        Me.txtSA.BackColor = System.Drawing.SystemColors.Window
        Me.txtSA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSA.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSA.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSA.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSA.Location = New System.Drawing.Point(200, 5)
        Me.txtSA.MaxLength = 0
        Me.txtSA.Multiline = True
        Me.txtSA.Name = "txtSA"
        Me.txtSA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSA.Size = New System.Drawing.Size(73, 19)
        Me.txtSA.TabIndex = 65
        '
        'SprdMainPLT
        '
        Me.SprdMainPLT.DataSource = Nothing
        Me.SprdMainPLT.Location = New System.Drawing.Point(4, 31)
        Me.SprdMainPLT.Name = "SprdMainPLT"
        Me.SprdMainPLT.OcxState = CType(resources.GetObject("SprdMainPLT.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainPLT.Size = New System.Drawing.Size(884, 229)
        Me.SprdMainPLT.TabIndex = 75
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(6, 9)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(163, 14)
        Me.Label10.TabIndex = 66
        Me.Label10.Text = "Plating Surface Area (per DM2) :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.txtSAPS_I)
        Me._SSTab1_TabPage3.Controls.Add(Me.txtSAPS_E)
        Me._SSTab1_TabPage3.Controls.Add(Me.SprdMainPPS)
        Me._SSTab1_TabPage3.Controls.Add(Me.Label18)
        Me._SSTab1_TabPage3.Controls.Add(Me.Label17)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(889, 262)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "Paint Detail"
        '
        'txtSAPS_I
        '
        Me.txtSAPS_I.AcceptsReturn = True
        Me.txtSAPS_I.BackColor = System.Drawing.SystemColors.Window
        Me.txtSAPS_I.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAPS_I.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSAPS_I.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAPS_I.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSAPS_I.Location = New System.Drawing.Point(668, 4)
        Me.txtSAPS_I.MaxLength = 0
        Me.txtSAPS_I.Multiline = True
        Me.txtSAPS_I.Name = "txtSAPS_I"
        Me.txtSAPS_I.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSAPS_I.Size = New System.Drawing.Size(73, 19)
        Me.txtSAPS_I.TabIndex = 69
        '
        'txtSAPS_E
        '
        Me.txtSAPS_E.AcceptsReturn = True
        Me.txtSAPS_E.BackColor = System.Drawing.SystemColors.Window
        Me.txtSAPS_E.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAPS_E.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSAPS_E.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAPS_E.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSAPS_E.Location = New System.Drawing.Point(238, 4)
        Me.txtSAPS_E.MaxLength = 0
        Me.txtSAPS_E.Multiline = True
        Me.txtSAPS_E.Name = "txtSAPS_E"
        Me.txtSAPS_E.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSAPS_E.Size = New System.Drawing.Size(73, 19)
        Me.txtSAPS_E.TabIndex = 67
        '
        'SprdMainPPS
        '
        Me.SprdMainPPS.DataSource = Nothing
        Me.SprdMainPPS.Location = New System.Drawing.Point(4, 31)
        Me.SprdMainPPS.Name = "SprdMainPPS"
        Me.SprdMainPPS.OcxState = CType(resources.GetObject("SprdMainPPS.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainPPS.Size = New System.Drawing.Size(884, 229)
        Me.SprdMainPPS.TabIndex = 76
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(437, 6)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(193, 14)
        Me.Label18.TabIndex = 70
        Me.Label18.Text = "Internal Paint Surface Area (per DM2) :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(4, 6)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(197, 14)
        Me.Label17.TabIndex = 68
        Me.Label17.Text = "External Paint Surface Area (per DM2) :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me.txtSAPC)
        Me._SSTab1_TabPage4.Controls.Add(Me.SprdMainPC)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label26)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(889, 262)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "Powder Coating Detail"
        '
        'txtSAPC
        '
        Me.txtSAPC.AcceptsReturn = True
        Me.txtSAPC.BackColor = System.Drawing.SystemColors.Window
        Me.txtSAPC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSAPC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSAPC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSAPC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSAPC.Location = New System.Drawing.Point(200, 5)
        Me.txtSAPC.MaxLength = 0
        Me.txtSAPC.Multiline = True
        Me.txtSAPC.Name = "txtSAPC"
        Me.txtSAPC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSAPC.Size = New System.Drawing.Size(73, 19)
        Me.txtSAPC.TabIndex = 71
        '
        'SprdMainPC
        '
        Me.SprdMainPC.DataSource = Nothing
        Me.SprdMainPC.Location = New System.Drawing.Point(4, 31)
        Me.SprdMainPC.Name = "SprdMainPC"
        Me.SprdMainPC.OcxState = CType(resources.GetObject("SprdMainPC.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainPC.Size = New System.Drawing.Size(884, 229)
        Me.SprdMainPC.TabIndex = 77
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(5, 9)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(166, 14)
        Me.Label26.TabIndex = 72
        Me.Label26.Text = "Coated Surface Area (per DM2) :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkBOP
        '
        Me.chkBOP.AutoSize = True
        Me.chkBOP.BackColor = System.Drawing.SystemColors.Control
        Me.chkBOP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBOP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBOP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBOP.Location = New System.Drawing.Point(656, 523)
        Me.chkBOP.Name = "chkBOP"
        Me.chkBOP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBOP.Size = New System.Drawing.Size(47, 18)
        Me.chkBOP.TabIndex = 58
        Me.chkBOP.Text = "BOP"
        Me.chkBOP.UseVisualStyleBackColor = False
        '
        'txtProcessCost
        '
        Me.txtProcessCost.AcceptsReturn = True
        Me.txtProcessCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtProcessCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProcessCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProcessCost.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProcessCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProcessCost.Location = New System.Drawing.Point(280, 542)
        Me.txtProcessCost.MaxLength = 0
        Me.txtProcessCost.Multiline = True
        Me.txtProcessCost.Name = "txtProcessCost"
        Me.txtProcessCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProcessCost.Size = New System.Drawing.Size(49, 19)
        Me.txtProcessCost.TabIndex = 18
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(730, 547)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(136, 18)
        Me.chkStatus.TabIndex = 21
        Me.chkStatus.Text = "Status (Open / Closed)"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'chkScrap
        '
        Me.chkScrap.AutoSize = True
        Me.chkScrap.BackColor = System.Drawing.SystemColors.Control
        Me.chkScrap.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkScrap.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkScrap.Location = New System.Drawing.Point(730, 523)
        Me.chkScrap.Name = "chkScrap"
        Me.chkScrap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkScrap.Size = New System.Drawing.Size(105, 18)
        Me.chkScrap.TabIndex = 20
        Me.chkScrap.Text = "From Cut Length"
        Me.chkScrap.UseVisualStyleBackColor = False
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AcceptsReturn = True
        Me.TxtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtRemarks.Location = New System.Drawing.Point(86, 520)
        Me.TxtRemarks.MaxLength = 0
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtRemarks.Size = New System.Drawing.Size(244, 19)
        Me.TxtRemarks.TabIndex = 19
        '
        'txtApprovedBy
        '
        Me.txtApprovedBy.AcceptsReturn = True
        Me.txtApprovedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtApprovedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtApprovedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtApprovedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApprovedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtApprovedBy.Location = New System.Drawing.Point(437, 542)
        Me.txtApprovedBy.MaxLength = 0
        Me.txtApprovedBy.Name = "txtApprovedBy"
        Me.txtApprovedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtApprovedBy.Size = New System.Drawing.Size(53, 20)
        Me.txtApprovedBy.TabIndex = 24
        '
        'txtPreparedBy
        '
        Me.txtPreparedBy.AcceptsReturn = True
        Me.txtPreparedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreparedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPreparedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreparedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPreparedBy.Location = New System.Drawing.Point(437, 520)
        Me.txtPreparedBy.MaxLength = 0
        Me.txtPreparedBy.Name = "txtPreparedBy"
        Me.txtPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPreparedBy.Size = New System.Drawing.Size(53, 20)
        Me.txtPreparedBy.TabIndex = 22
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.SprdSeq)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 96)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(444, 123)
        Me.Frame1.TabIndex = 55
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Process Sequence"
        '
        'SprdSeq
        '
        Me.SprdSeq.DataSource = Nothing
        Me.SprdSeq.Location = New System.Drawing.Point(2, 12)
        Me.SprdSeq.Name = "SprdSeq"
        Me.SprdSeq.OcxState = CType(resources.GetObject("SprdSeq.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdSeq.Size = New System.Drawing.Size(438, 106)
        Me.SprdSeq.TabIndex = 15
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdMainRel)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(451, 96)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(444, 123)
        Me.Frame2.TabIndex = 56
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Item Relation"
        '
        'SprdMainRel
        '
        Me.SprdMainRel.DataSource = Nothing
        Me.SprdMainRel.Location = New System.Drawing.Point(4, 12)
        Me.SprdMainRel.Name = "SprdMainRel"
        Me.SprdMainRel.OcxState = CType(resources.GetObject("SprdMainRel.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMainRel.Size = New System.Drawing.Size(438, 106)
        Me.SprdMainRel.TabIndex = 16
        '
        'lblApproval
        '
        Me.lblApproval.BackColor = System.Drawing.SystemColors.Control
        Me.lblApproval.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblApproval.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApproval.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblApproval.Location = New System.Drawing.Point(564, 56)
        Me.lblApproval.Name = "lblApproval"
        Me.lblApproval.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblApproval.Size = New System.Drawing.Size(47, 11)
        Me.lblApproval.TabIndex = 80
        Me.lblApproval.Text = "N"
        Me.lblApproval.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(5, 546)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(79, 14)
        Me.Label12.TabIndex = 64
        Me.Label12.Text = "Process Type :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblOldWEF
        '
        Me.lblOldWEF.AutoSize = True
        Me.lblOldWEF.BackColor = System.Drawing.SystemColors.Control
        Me.lblOldWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOldWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOldWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOldWEF.Location = New System.Drawing.Point(674, 392)
        Me.lblOldWEF.Name = "lblOldWEF"
        Me.lblOldWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOldWEF.Size = New System.Drawing.Size(55, 14)
        Me.lblOldWEF.TabIndex = 60
        Me.lblOldWEF.Text = "lblOldWEF"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(757, 40)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(65, 14)
        Me.Label16.TabIndex = 59
        Me.Label16.Text = "Output Qty :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(202, 546)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(78, 14)
        Me.Label15.TabIndex = 57
        Me.Label15.Text = "Process Cost :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCopyMKey
        '
        Me.lblCopyMKey.AutoSize = True
        Me.lblCopyMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblCopyMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCopyMKey.Enabled = False
        Me.lblCopyMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCopyMKey.Location = New System.Drawing.Point(100, 62)
        Me.lblCopyMKey.Name = "lblCopyMKey"
        Me.lblCopyMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCopyMKey.Size = New System.Drawing.Size(69, 14)
        Me.lblCopyMKey.TabIndex = 54
        Me.lblCopyMKey.Text = "lblCopyMKey"
        Me.lblCopyMKey.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(394, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(63, 14)
        Me.Label8.TabIndex = 53
        Me.Label8.Text = "Amend No :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(30, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(65, 14)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "Copy From :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(761, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(63, 14)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Amend No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(354, 545)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(77, 14)
        Me.Label14.TabIndex = 47
        Me.Label14.Text = "Approved By :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblApprovedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblApprovedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblApprovedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApprovedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblApprovedBy.Location = New System.Drawing.Point(515, 542)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblApprovedBy.Size = New System.Drawing.Size(121, 19)
        Me.lblApprovedBy.TabIndex = 46
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(354, 523)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(73, 14)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "Prepared By :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPreparedBy
        '
        Me.lblPreparedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblPreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPreparedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPreparedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreparedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPreparedBy.Location = New System.Drawing.Point(515, 520)
        Me.lblPreparedBy.Name = "lblPreparedBy"
        Me.lblPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPreparedBy.Size = New System.Drawing.Size(121, 19)
        Me.lblPreparedBy.TabIndex = 44
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(29, 524)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(55, 14)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Remarks :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(681, 13)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(31, 14)
        Me.Label7.TabIndex = 42
        Me.Label7.Text = "Unit :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(396, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(48, 14)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Part No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(213, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(57, 14)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Model No :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Enabled = False
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(12, 28)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 39
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblMKey.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(78, 14)
        Me.Label1.TabIndex = 38
        Me.Label1.Text = "Product Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(50, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(42, 14)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "W.E.F. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ADataGrid
        '
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.CommandTimeout = 0
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.ConnectionString = Nothing
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Location = New System.Drawing.Point(252, 144)
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.Name = "ADataGrid"
        Me.ADataGrid.Size = New System.Drawing.Size(80, 22)
        Me.ADataGrid.TabIndex = 36
        Me.ADataGrid.Text = "Adodc1"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(898, 562)
        Me.SprdView.TabIndex = 36
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
        Me.Frame3.Controls.Add(Me.cmdAmend)
        Me.Frame3.Controls.Add(Me.CmdSave)
        Me.Frame3.Controls.Add(Me.CmdModify)
        Me.Frame3.Controls.Add(Me.CmdAdd)
        Me.Frame3.Controls.Add(Me.lblType)
        Me.Frame3.Controls.Add(Me.lblDetail)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 563)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(898, 47)
        Me.Frame3.TabIndex = 34
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(592, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 34
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Location = New System.Drawing.Point(716, 18)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.Size = New System.Drawing.Size(31, 15)
        Me.lblType.TabIndex = 49
        Me.lblType.Text = "lblType"
        '
        'lblDetail
        '
        Me.lblDetail.AutoSize = True
        Me.lblDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDetail.Location = New System.Drawing.Point(20, 14)
        Me.lblDetail.Name = "lblDetail"
        Me.lblDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDetail.Size = New System.Drawing.Size(43, 14)
        Me.lblDetail.TabIndex = 48
        Me.lblDetail.Text = "lblDetail"
        Me.lblDetail.Visible = False
        '
        'FrmBOMNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(899, 611)
        Me.Controls.Add(Me.fraBase)
        Me.Controls.Add(Me.ADataGrid)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "FrmBOMNew"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Bill Of Material"
        Me.fraBase.ResumeLayout(False)
        Me.fraBase.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.fraBOMDetail.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage5.ResumeLayout(False)
        CType(Me.SprdTool, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage6.ResumeLayout(False)
        CType(Me.SprdOthers, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me._SSTab1_TabPage1.PerformLayout()
        CType(Me.SprdMainMWS, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        Me._SSTab1_TabPage2.PerformLayout()
        CType(Me.SprdMainPLT, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        Me._SSTab1_TabPage3.PerformLayout()
        CType(Me.SprdMainPPS, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        Me._SSTab1_TabPage4.PerformLayout()
        CType(Me.SprdMainPC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdSeq, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        CType(Me.SprdMainRel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
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

    Public WithEvents _SSTab1_TabPage5 As TabPage
    Public WithEvents _SSTab1_TabPage6 As TabPage
#End Region
End Class