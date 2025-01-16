Imports Microsoft.VisualBasic.Compatibility

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBOMOut
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
    Public WithEvents chkBOP As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchDept As System.Windows.Forms.Button
    Public WithEvents txtDeptCode As System.Windows.Forms.TextBox
    Public WithEvents txtProcessCost As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCopyProdCode As System.Windows.Forms.Button
    Public WithEvents txtCopyProductDesc As System.Windows.Forms.TextBox
    Public WithEvents txtCopyAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtCopyProductCode As System.Windows.Forms.TextBox
    Public WithEvents chkStatus As System.Windows.Forms.CheckBox
    Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
    Public WithEvents TxtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtAppBy As System.Windows.Forms.TextBox
    Public WithEvents txtPrepBy As System.Windows.Forms.TextBox
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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents chkInhouse As System.Windows.Forms.CheckBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents lblCopyMKey As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents lblAppBy As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblPrepBy As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents fraBase As System.Windows.Forms.GroupBox
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
    Public WithEvents lblDetail As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBOMOut))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchDept = New System.Windows.Forms.Button()
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
        Me.chkBOP = New System.Windows.Forms.CheckBox()
        Me.txtDeptCode = New System.Windows.Forms.TextBox()
        Me.txtProcessCost = New System.Windows.Forms.TextBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAppBy = New System.Windows.Forms.TextBox()
        Me.txtPrepBy = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.chkInhouse = New System.Windows.Forms.CheckBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblCopyMKey = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblAppBy = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblPrepBy = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblDetail = New System.Windows.Forms.Label()
        Me.fraBase.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchDept
        '
        Me.cmdSearchDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDept.Image = CType(resources.GetObject("cmdSearchDept.Image"), System.Drawing.Image)
        Me.cmdSearchDept.Location = New System.Drawing.Point(346, 473)
        Me.cmdSearchDept.Name = "cmdSearchDept"
        Me.cmdSearchDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDept.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchDept.TabIndex = 33
        Me.cmdSearchDept.TabStop = False
        Me.cmdSearchDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDept, "Search")
        Me.cmdSearchDept.UseVisualStyleBackColor = False
        '
        'cmdSearchCopyProdCode
        '
        Me.cmdSearchCopyProdCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCopyProdCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCopyProdCode.Enabled = False
        Me.cmdSearchCopyProdCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCopyProdCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCopyProdCode.Image = CType(resources.GetObject("cmdSearchCopyProdCode.Image"), System.Drawing.Image)
        Me.cmdSearchCopyProdCode.Location = New System.Drawing.Point(184, 73)
        Me.cmdSearchCopyProdCode.Name = "cmdSearchCopyProdCode"
        Me.cmdSearchCopyProdCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCopyProdCode.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchCopyProdCode.TabIndex = 49
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
        Me.txtCopyProductDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyProductDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyProductDesc.Location = New System.Drawing.Point(216, 73)
        Me.txtCopyProductDesc.MaxLength = 0
        Me.txtCopyProductDesc.Name = "txtCopyProductDesc"
        Me.txtCopyProductDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyProductDesc.Size = New System.Drawing.Size(440, 22)
        Me.txtCopyProductDesc.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.txtCopyProductDesc, "Press F1 For Help")
        '
        'txtCopyAmendNo
        '
        Me.txtCopyAmendNo.AcceptsReturn = True
        Me.txtCopyAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCopyAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopyAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCopyAmendNo.Enabled = False
        Me.txtCopyAmendNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyAmendNo.Location = New System.Drawing.Point(779, 73)
        Me.txtCopyAmendNo.MaxLength = 0
        Me.txtCopyAmendNo.Name = "txtCopyAmendNo"
        Me.txtCopyAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyAmendNo.Size = New System.Drawing.Size(105, 22)
        Me.txtCopyAmendNo.TabIndex = 47
        Me.ToolTip1.SetToolTip(Me.txtCopyAmendNo, "Press F1 For Help")
        '
        'txtCopyProductCode
        '
        Me.txtCopyProductCode.AcceptsReturn = True
        Me.txtCopyProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCopyProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCopyProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCopyProductCode.Enabled = False
        Me.txtCopyProductCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCopyProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCopyProductCode.Location = New System.Drawing.Point(102, 73)
        Me.txtCopyProductCode.MaxLength = 0
        Me.txtCopyProductCode.Name = "txtCopyProductCode"
        Me.txtCopyProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCopyProductCode.Size = New System.Drawing.Size(81, 22)
        Me.txtCopyProductCode.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.txtCopyProductCode, "Press F1 For Help")
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmendNo.Location = New System.Drawing.Point(779, 13)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(105, 22)
        Me.txtAmendNo.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.txtAmendNo, "Press F1 For Help")
        '
        'cmdSearchPrepBy
        '
        Me.cmdSearchPrepBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPrepBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPrepBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPrepBy.Image = CType(resources.GetObject("cmdSearchPrepBy.Image"), System.Drawing.Image)
        Me.cmdSearchPrepBy.Location = New System.Drawing.Point(161, 534)
        Me.cmdSearchPrepBy.Name = "cmdSearchPrepBy"
        Me.cmdSearchPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPrepBy.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchPrepBy.TabIndex = 29
        Me.cmdSearchPrepBy.TabStop = False
        Me.cmdSearchPrepBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPrepBy, "Search")
        Me.cmdSearchPrepBy.UseVisualStyleBackColor = False
        '
        'cmdSearchAppBy
        '
        Me.cmdSearchAppBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAppBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAppBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAppBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAppBy.Image = CType(resources.GetObject("cmdSearchAppBy.Image"), System.Drawing.Image)
        Me.cmdSearchAppBy.Location = New System.Drawing.Point(671, 535)
        Me.cmdSearchAppBy.Name = "cmdSearchAppBy"
        Me.cmdSearchAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAppBy.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchAppBy.TabIndex = 28
        Me.cmdSearchAppBy.TabStop = False
        Me.cmdSearchAppBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAppBy, "Search")
        Me.cmdSearchAppBy.UseVisualStyleBackColor = False
        '
        'cmdSearchWEF
        '
        Me.cmdSearchWEF.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchWEF.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchWEF.Image = CType(resources.GetObject("cmdSearchWEF.Image"), System.Drawing.Image)
        Me.cmdSearchWEF.Location = New System.Drawing.Point(184, 43)
        Me.cmdSearchWEF.Name = "cmdSearchWEF"
        Me.cmdSearchWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchWEF.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchWEF.TabIndex = 27
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
        Me.txtUnit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnit.Location = New System.Drawing.Point(779, 43)
        Me.txtUnit.MaxLength = 0
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnit.Size = New System.Drawing.Size(105, 22)
        Me.txtUnit.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.txtUnit, "Press F1 For Help")
        '
        'txtCustPartNo
        '
        Me.txtCustPartNo.AcceptsReturn = True
        Me.txtCustPartNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustPartNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustPartNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustPartNo.Location = New System.Drawing.Point(544, 43)
        Me.txtCustPartNo.MaxLength = 0
        Me.txtCustPartNo.Name = "txtCustPartNo"
        Me.txtCustPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustPartNo.Size = New System.Drawing.Size(115, 22)
        Me.txtCustPartNo.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.txtCustPartNo, "Press F1 For Help")
        '
        'txtModelNo
        '
        Me.txtModelNo.AcceptsReturn = True
        Me.txtModelNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtModelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModelNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModelNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModelNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModelNo.Location = New System.Drawing.Point(318, 43)
        Me.txtModelNo.MaxLength = 0
        Me.txtModelNo.Name = "txtModelNo"
        Me.txtModelNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModelNo.Size = New System.Drawing.Size(115, 22)
        Me.txtModelNo.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.txtModelNo, "Press F1 For Help")
        '
        'txtProductDesc
        '
        Me.txtProductDesc.AcceptsReturn = True
        Me.txtProductDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductDesc.Location = New System.Drawing.Point(216, 13)
        Me.txtProductDesc.MaxLength = 0
        Me.txtProductDesc.Name = "txtProductDesc"
        Me.txtProductDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductDesc.Size = New System.Drawing.Size(440, 22)
        Me.txtProductDesc.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.txtProductDesc, "Press F1 For Help")
        '
        'cmdSearchProdCode
        '
        Me.cmdSearchProdCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProdCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProdCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProdCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProdCode.Image = CType(resources.GetObject("cmdSearchProdCode.Image"), System.Drawing.Image)
        Me.cmdSearchProdCode.Location = New System.Drawing.Point(184, 13)
        Me.cmdSearchProdCode.Name = "cmdSearchProdCode"
        Me.cmdSearchProdCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProdCode.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchProdCode.TabIndex = 15
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
        Me.txtProductCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductCode.Location = New System.Drawing.Point(102, 13)
        Me.txtProductCode.MaxLength = 0
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.Size = New System.Drawing.Size(81, 22)
        Me.txtProductCode.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtProductCode, "Press F1 For Help")
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(102, 43)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(81, 22)
        Me.txtWEF.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.txtWEF, "Press F1 For Help")
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(689, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 34)
        Me.CmdClose.TabIndex = 8
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
        Me.CmdView.Location = New System.Drawing.Point(623, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 34)
        Me.CmdView.TabIndex = 7
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
        Me.CmdPreview.Location = New System.Drawing.Point(557, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 34)
        Me.CmdPreview.TabIndex = 6
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(491, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdPrint.TabIndex = 5
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
        Me.CmdDelete.Location = New System.Drawing.Point(425, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 34)
        Me.CmdDelete.TabIndex = 4
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(359, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdSavePrint.TabIndex = 3
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdAmend
        '
        Me.cmdAmend.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmend.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmend.Image = CType(resources.GetObject("cmdAmend.Image"), System.Drawing.Image)
        Me.cmdAmend.Location = New System.Drawing.Point(293, 12)
        Me.cmdAmend.Name = "cmdAmend"
        Me.cmdAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmend.Size = New System.Drawing.Size(67, 34)
        Me.cmdAmend.TabIndex = 42
        Me.cmdAmend.Text = "A&mendment"
        Me.cmdAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAmend, "Modify Record")
        Me.cmdAmend.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(227, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 34)
        Me.CmdSave.TabIndex = 2
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
        Me.CmdModify.Location = New System.Drawing.Point(161, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 34)
        Me.CmdModify.TabIndex = 1
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
        Me.CmdAdd.Location = New System.Drawing.Point(95, 12)
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
        Me.fraBase.Controls.Add(Me.chkBOP)
        Me.fraBase.Controls.Add(Me.cmdSearchDept)
        Me.fraBase.Controls.Add(Me.txtDeptCode)
        Me.fraBase.Controls.Add(Me.txtProcessCost)
        Me.fraBase.Controls.Add(Me.cmdSearchCopyProdCode)
        Me.fraBase.Controls.Add(Me.txtCopyProductDesc)
        Me.fraBase.Controls.Add(Me.txtCopyAmendNo)
        Me.fraBase.Controls.Add(Me.txtCopyProductCode)
        Me.fraBase.Controls.Add(Me.chkStatus)
        Me.fraBase.Controls.Add(Me.txtAmendNo)
        Me.fraBase.Controls.Add(Me.TxtRemarks)
        Me.fraBase.Controls.Add(Me.txtAppBy)
        Me.fraBase.Controls.Add(Me.txtPrepBy)
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
        Me.fraBase.Controls.Add(Me.SprdMain)
        Me.fraBase.Controls.Add(Me.chkInhouse)
        Me.fraBase.Controls.Add(Me.Label10)
        Me.fraBase.Controls.Add(Me.Label15)
        Me.fraBase.Controls.Add(Me.lblCopyMKey)
        Me.fraBase.Controls.Add(Me.Label8)
        Me.fraBase.Controls.Add(Me.Label3)
        Me.fraBase.Controls.Add(Me.Label6)
        Me.fraBase.Controls.Add(Me.Label14)
        Me.fraBase.Controls.Add(Me.lblAppBy)
        Me.fraBase.Controls.Add(Me.Label13)
        Me.fraBase.Controls.Add(Me.lblPrepBy)
        Me.fraBase.Controls.Add(Me.Label5)
        Me.fraBase.Controls.Add(Me.Label7)
        Me.fraBase.Controls.Add(Me.Label4)
        Me.fraBase.Controls.Add(Me.Label2)
        Me.fraBase.Controls.Add(Me.lblMKey)
        Me.fraBase.Controls.Add(Me.Label1)
        Me.fraBase.Controls.Add(Me.Label9)
        Me.fraBase.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBase.Location = New System.Drawing.Point(0, -4)
        Me.fraBase.Name = "fraBase"
        Me.fraBase.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBase.Size = New System.Drawing.Size(896, 566)
        Me.fraBase.TabIndex = 10
        Me.fraBase.TabStop = False
        '
        'chkBOP
        '
        Me.chkBOP.BackColor = System.Drawing.SystemColors.Control
        Me.chkBOP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBOP.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBOP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBOP.Location = New System.Drawing.Point(698, 475)
        Me.chkBOP.Name = "chkBOP"
        Me.chkBOP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBOP.Size = New System.Drawing.Size(68, 17)
        Me.chkBOP.TabIndex = 55
        Me.chkBOP.Text = "BOP"
        Me.chkBOP.UseVisualStyleBackColor = False
        '
        'txtDeptCode
        '
        Me.txtDeptCode.AcceptsReturn = True
        Me.txtDeptCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeptCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeptCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeptCode.Location = New System.Drawing.Point(281, 475)
        Me.txtDeptCode.MaxLength = 0
        Me.txtDeptCode.Multiline = True
        Me.txtDeptCode.Name = "txtDeptCode"
        Me.txtDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeptCode.Size = New System.Drawing.Size(63, 19)
        Me.txtDeptCode.TabIndex = 32
        '
        'txtProcessCost
        '
        Me.txtProcessCost.AcceptsReturn = True
        Me.txtProcessCost.BackColor = System.Drawing.SystemColors.Window
        Me.txtProcessCost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProcessCost.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProcessCost.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProcessCost.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProcessCost.Location = New System.Drawing.Point(91, 475)
        Me.txtProcessCost.MaxLength = 0
        Me.txtProcessCost.Multiline = True
        Me.txtProcessCost.Name = "txtProcessCost"
        Me.txtProcessCost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProcessCost.Size = New System.Drawing.Size(63, 19)
        Me.txtProcessCost.TabIndex = 31
        '
        'chkStatus
        '
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(700, 505)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(155, 18)
        Me.chkStatus.TabIndex = 45
        Me.chkStatus.Text = "Status (Open / Closed)"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AcceptsReturn = True
        Me.TxtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtRemarks.Location = New System.Drawing.Point(91, 505)
        Me.TxtRemarks.MaxLength = 0
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtRemarks.Size = New System.Drawing.Size(279, 19)
        Me.TxtRemarks.TabIndex = 35
        '
        'txtAppBy
        '
        Me.txtAppBy.AcceptsReturn = True
        Me.txtAppBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtAppBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAppBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAppBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAppBy.Location = New System.Drawing.Point(600, 535)
        Me.txtAppBy.MaxLength = 0
        Me.txtAppBy.Name = "txtAppBy"
        Me.txtAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppBy.Size = New System.Drawing.Size(69, 22)
        Me.txtAppBy.TabIndex = 34
        '
        'txtPrepBy
        '
        Me.txtPrepBy.AcceptsReturn = True
        Me.txtPrepBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrepBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrepBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrepBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrepBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrepBy.Location = New System.Drawing.Point(91, 535)
        Me.txtPrepBy.MaxLength = 0
        Me.txtPrepBy.Name = "txtPrepBy"
        Me.txtPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrepBy.Size = New System.Drawing.Size(69, 22)
        Me.txtPrepBy.TabIndex = 30
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(4, 106)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(890, 356)
        Me.SprdMain.TabIndex = 20
        '
        'chkInhouse
        '
        Me.chkInhouse.BackColor = System.Drawing.SystemColors.Control
        Me.chkInhouse.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInhouse.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInhouse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInhouse.Location = New System.Drawing.Point(778, 473)
        Me.chkInhouse.Name = "chkInhouse"
        Me.chkInhouse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInhouse.Size = New System.Drawing.Size(110, 17)
        Me.chkInhouse.TabIndex = 52
        Me.chkInhouse.Text = "InHouse"
        Me.chkInhouse.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(208, 477)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "Dept Code :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(10, 477)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(77, 13)
        Me.Label15.TabIndex = 53
        Me.Label15.Text = "Process Cost :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCopyMKey
        '
        Me.lblCopyMKey.AutoSize = True
        Me.lblCopyMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblCopyMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCopyMKey.Enabled = False
        Me.lblCopyMKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCopyMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCopyMKey.Location = New System.Drawing.Point(238, 64)
        Me.lblCopyMKey.Name = "lblCopyMKey"
        Me.lblCopyMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCopyMKey.Size = New System.Drawing.Size(76, 13)
        Me.lblCopyMKey.TabIndex = 51
        Me.lblCopyMKey.Text = "lblCopyMKey"
        Me.lblCopyMKey.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblCopyMKey.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(707, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(67, 13)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Amend No :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(28, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Copy From :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(707, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(67, 13)
        Me.Label6.TabIndex = 44
        Me.Label6.Text = "Amend No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(516, 539)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(80, 13)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "Approved By :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAppBy
        '
        Me.lblAppBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblAppBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAppBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAppBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAppBy.Location = New System.Drawing.Point(703, 535)
        Me.lblAppBy.Name = "lblAppBy"
        Me.lblAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAppBy.Size = New System.Drawing.Size(183, 19)
        Me.lblAppBy.TabIndex = 39
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(12, 538)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(75, 13)
        Me.Label13.TabIndex = 38
        Me.Label13.Text = "Prepared By :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPrepBy
        '
        Me.lblPrepBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblPrepBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPrepBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPrepBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrepBy.Location = New System.Drawing.Point(189, 535)
        Me.lblPrepBy.Name = "lblPrepBy"
        Me.lblPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrepBy.Size = New System.Drawing.Size(183, 19)
        Me.lblPrepBy.TabIndex = 37
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(30, 509)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Remarks :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(740, 47)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(34, 13)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "Unit :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(493, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Part No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(255, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Model No :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Enabled = False
        Me.lblMKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(-2, 28)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(49, 13)
        Me.lblMKey.TabIndex = 23
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblMKey.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(16, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Product Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(55, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(43, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "W.E.F. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(896, 562)
        Me.SprdView.TabIndex = 11
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
        Me.Frame3.Controls.Add(Me.lblDetail)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 560)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(898, 51)
        Me.Frame3.TabIndex = 9
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(592, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 9
        '
        'lblDetail
        '
        Me.lblDetail.AutoSize = True
        Me.lblDetail.BackColor = System.Drawing.SystemColors.Control
        Me.lblDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDetail.Location = New System.Drawing.Point(20, 14)
        Me.lblDetail.Name = "lblDetail"
        Me.lblDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDetail.Size = New System.Drawing.Size(50, 13)
        Me.lblDetail.TabIndex = 41
        Me.lblDetail.Text = "lblDetail"
        Me.lblDetail.Visible = False
        '
        'frmBOMOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.fraBase)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmBOMOut"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Bill Of Material (Outward Jobwork)"
        Me.fraBase.ResumeLayout(False)
        Me.fraBase.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class