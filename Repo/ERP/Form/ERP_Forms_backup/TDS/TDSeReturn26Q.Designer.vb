Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTDSeReturn26Q
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
        'Me.MDIParent = TDS.Master
        'TDS.Master.Show
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
	Public WithEvents txtTokenNo As System.Windows.Forms.TextBox
	Public WithEvents lstSection As System.Windows.Forms.CheckedListBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label16 As System.Windows.Forms.Label
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents Frame7 As System.Windows.Forms.GroupBox
	Public WithEvents txtPanNo As System.Windows.Forms.TextBox
	Public WithEvents txtTDSAcNo As System.Windows.Forms.TextBox
	Public WithEvents txtReturnFiled As System.Windows.Forms.TextBox
	Public WithEvents txtProvReceiptNo As System.Windows.Forms.TextBox
	Public WithEvents txtAYear As System.Windows.Forms.TextBox
	Public WithEvents txtFYear As System.Windows.Forms.TextBox
	Public WithEvents TxtAccount As System.Windows.Forms.TextBox
	Public WithEvents CmdSearch As System.Windows.Forms.Button
	Public WithEvents Label28 As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label19 As System.Windows.Forms.Label
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
	Public WithEvents txtUACK As System.Windows.Forms.TextBox
	Public WithEvents txtMobileNo As System.Windows.Forms.TextBox
	Public WithEvents txtPhone As System.Windows.Forms.TextBox
	Public WithEvents txtEmail As System.Windows.Forms.TextBox
	Public WithEvents txtBranch As System.Windows.Forms.TextBox
	Public WithEvents txtDeductorType As System.Windows.Forms.TextBox
	Public WithEvents txtFlat As System.Windows.Forms.TextBox
	Public WithEvents txtBuilding As System.Windows.Forms.TextBox
	Public WithEvents txtRoad As System.Windows.Forms.TextBox
	Public WithEvents txtArea As System.Windows.Forms.TextBox
	Public WithEvents txtTown As System.Windows.Forms.TextBox
	Public WithEvents txtState As System.Windows.Forms.TextBox
	Public WithEvents txtPinCode As System.Windows.Forms.TextBox
	Public WithEvents txtPersonName As System.Windows.Forms.TextBox
	Public WithEvents Label41 As System.Windows.Forms.Label
	Public WithEvents Label39 As System.Windows.Forms.Label
	Public WithEvents Label25 As System.Windows.Forms.Label
	Public WithEvents Label23 As System.Windows.Forms.Label
	Public WithEvents Label22 As System.Windows.Forms.Label
	Public WithEvents Label21 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
	Public WithEvents txtResponPANNo As System.Windows.Forms.TextBox
	Public WithEvents chkPersonAddChange As System.Windows.Forms.CheckBox
	Public WithEvents txtDesg As System.Windows.Forms.TextBox
	Public WithEvents txtPersonName_p As System.Windows.Forms.TextBox
	Public WithEvents txtPinCode_p As System.Windows.Forms.TextBox
	Public WithEvents txtState_p As System.Windows.Forms.TextBox
	Public WithEvents txtTown_p As System.Windows.Forms.TextBox
	Public WithEvents txtArea_p As System.Windows.Forms.TextBox
	Public WithEvents txtRoad_p As System.Windows.Forms.TextBox
	Public WithEvents txtBuilding_p As System.Windows.Forms.TextBox
	Public WithEvents txtFlat_p As System.Windows.Forms.TextBox
	Public WithEvents txtEmail_p As System.Windows.Forms.TextBox
	Public WithEvents txtPhone_p As System.Windows.Forms.TextBox
	Public WithEvents Label40 As System.Windows.Forms.Label
	Public WithEvents Label27 As System.Windows.Forms.Label
	Public WithEvents Label38 As System.Windows.Forms.Label
	Public WithEvents Label37 As System.Windows.Forms.Label
	Public WithEvents Label36 As System.Windows.Forms.Label
	Public WithEvents Label35 As System.Windows.Forms.Label
	Public WithEvents Label34 As System.Windows.Forms.Label
	Public WithEvents Label33 As System.Windows.Forms.Label
	Public WithEvents Label32 As System.Windows.Forms.Label
	Public WithEvents Label31 As System.Windows.Forms.Label
	Public WithEvents Label30 As System.Windows.Forms.Label
	Public WithEvents Label29 As System.Windows.Forms.Label
	Public WithEvents Label26 As System.Windows.Forms.Label
	Public WithEvents Label24 As System.Windows.Forms.Label
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
	Public WithEvents SprdView26 As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
	Public WithEvents SprdViewAnnex As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
	Public WithEvents SSTab1 As System.Windows.Forms.TabControl
	Public WithEvents cmdValidate As System.Windows.Forms.Button
	Public WithEvents chkConsolidated As System.Windows.Forms.CheckBox
	Public WithEvents cmdCD As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents lblFormType As System.Windows.Forms.Label
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTDSeReturn26Q))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtAYear = New System.Windows.Forms.TextBox()
        Me.txtFYear = New System.Windows.Forms.TextBox()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.CmdSearch = New System.Windows.Forms.Button()
        Me.txtBranch = New System.Windows.Forms.TextBox()
        Me.txtDeductorType = New System.Windows.Forms.TextBox()
        Me.txtPersonName = New System.Windows.Forms.TextBox()
        Me.txtDesg = New System.Windows.Forms.TextBox()
        Me.txtPersonName_p = New System.Windows.Forms.TextBox()
        Me.cmdValidate = New System.Windows.Forms.Button()
        Me.cmdCD = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtTokenNo = New System.Windows.Forms.TextBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.lstSection = New System.Windows.Forms.CheckedListBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtPanNo = New System.Windows.Forms.TextBox()
        Me.txtTDSAcNo = New System.Windows.Forms.TextBox()
        Me.txtReturnFiled = New System.Windows.Forms.TextBox()
        Me.txtProvReceiptNo = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtUACK = New System.Windows.Forms.TextBox()
        Me.txtMobileNo = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtFlat = New System.Windows.Forms.TextBox()
        Me.txtBuilding = New System.Windows.Forms.TextBox()
        Me.txtRoad = New System.Windows.Forms.TextBox()
        Me.txtArea = New System.Windows.Forms.TextBox()
        Me.txtTown = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.txtPinCode = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtResponPANNo = New System.Windows.Forms.TextBox()
        Me.chkPersonAddChange = New System.Windows.Forms.CheckBox()
        Me.txtPinCode_p = New System.Windows.Forms.TextBox()
        Me.txtState_p = New System.Windows.Forms.TextBox()
        Me.txtTown_p = New System.Windows.Forms.TextBox()
        Me.txtArea_p = New System.Windows.Forms.TextBox()
        Me.txtRoad_p = New System.Windows.Forms.TextBox()
        Me.txtBuilding_p = New System.Windows.Forms.TextBox()
        Me.txtFlat_p = New System.Windows.Forms.TextBox()
        Me.txtEmail_p = New System.Windows.Forms.TextBox()
        Me.txtPhone_p = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.SprdView26 = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdViewAnnex = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.chkConsolidated = New System.Windows.Forms.CheckBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblFormType = New System.Windows.Forms.Label()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me._SSTab1_TabPage2.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me._SSTab1_TabPage3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView26, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage4.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdViewAnnex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtAYear
        '
        Me.txtAYear.AcceptsReturn = True
        Me.txtAYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtAYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAYear.Location = New System.Drawing.Point(509, 176)
        Me.txtAYear.MaxLength = 0
        Me.txtAYear.Name = "txtAYear"
        Me.txtAYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAYear.Size = New System.Drawing.Size(123, 19)
        Me.txtAYear.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtAYear, "Press F1 For Help")
        '
        'txtFYear
        '
        Me.txtFYear.AcceptsReturn = True
        Me.txtFYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtFYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFYear.Location = New System.Drawing.Point(509, 150)
        Me.txtFYear.MaxLength = 0
        Me.txtFYear.Name = "txtFYear"
        Me.txtFYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFYear.Size = New System.Drawing.Size(123, 19)
        Me.txtFYear.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtFYear, "Press F1 For Help")
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(220, 24)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(357, 19)
        Me.TxtAccount.TabIndex = 91
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        Me.TxtAccount.Visible = False
        '
        'CmdSearch
        '
        Me.CmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearch.Image = CType(resources.GetObject("CmdSearch.Image"), System.Drawing.Image)
        Me.CmdSearch.Location = New System.Drawing.Point(18, 22)
        Me.CmdSearch.Name = "CmdSearch"
        Me.CmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearch.Size = New System.Drawing.Size(27, 19)
        Me.CmdSearch.TabIndex = 93
        Me.CmdSearch.TabStop = False
        Me.CmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearch, "Search")
        Me.CmdSearch.UseVisualStyleBackColor = False
        Me.CmdSearch.Visible = False
        '
        'txtBranch
        '
        Me.txtBranch.AcceptsReturn = True
        Me.txtBranch.BackColor = System.Drawing.SystemColors.Window
        Me.txtBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBranch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBranch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBranch.Location = New System.Drawing.Point(241, 84)
        Me.txtBranch.MaxLength = 0
        Me.txtBranch.Name = "txtBranch"
        Me.txtBranch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBranch.Size = New System.Drawing.Size(415, 19)
        Me.txtBranch.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtBranch, "Press F1 For Help")
        '
        'txtDeductorType
        '
        Me.txtDeductorType.AcceptsReturn = True
        Me.txtDeductorType.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeductorType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeductorType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeductorType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeductorType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeductorType.Location = New System.Drawing.Point(241, 60)
        Me.txtDeductorType.MaxLength = 0
        Me.txtDeductorType.Name = "txtDeductorType"
        Me.txtDeductorType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeductorType.Size = New System.Drawing.Size(415, 19)
        Me.txtDeductorType.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtDeductorType, "Press F1 For Help")
        '
        'txtPersonName
        '
        Me.txtPersonName.AcceptsReturn = True
        Me.txtPersonName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPersonName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPersonName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPersonName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPersonName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPersonName.Location = New System.Drawing.Point(241, 36)
        Me.txtPersonName.MaxLength = 0
        Me.txtPersonName.Name = "txtPersonName"
        Me.txtPersonName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPersonName.Size = New System.Drawing.Size(415, 19)
        Me.txtPersonName.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtPersonName, "Press F1 For Help")
        '
        'txtDesg
        '
        Me.txtDesg.AcceptsReturn = True
        Me.txtDesg.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesg.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDesg.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDesg.Location = New System.Drawing.Point(241, 60)
        Me.txtDesg.MaxLength = 0
        Me.txtDesg.Name = "txtDesg"
        Me.txtDesg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDesg.Size = New System.Drawing.Size(415, 19)
        Me.txtDesg.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.txtDesg, "Press F1 For Help")
        '
        'txtPersonName_p
        '
        Me.txtPersonName_p.AcceptsReturn = True
        Me.txtPersonName_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtPersonName_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPersonName_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPersonName_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPersonName_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPersonName_p.Location = New System.Drawing.Point(241, 36)
        Me.txtPersonName_p.MaxLength = 0
        Me.txtPersonName_p.Name = "txtPersonName_p"
        Me.txtPersonName_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPersonName_p.Size = New System.Drawing.Size(415, 19)
        Me.txtPersonName_p.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.txtPersonName_p, "Press F1 For Help")
        '
        'cmdValidate
        '
        Me.cmdValidate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdValidate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdValidate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdValidate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdValidate.Image = CType(resources.GetObject("cmdValidate.Image"), System.Drawing.Image)
        Me.cmdValidate.Location = New System.Drawing.Point(332, 9)
        Me.cmdValidate.Name = "cmdValidate"
        Me.cmdValidate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdValidate.Size = New System.Drawing.Size(67, 37)
        Me.cmdValidate.TabIndex = 87
        Me.cmdValidate.Text = "&Validate"
        Me.cmdValidate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdValidate, "Show Record")
        Me.cmdValidate.UseVisualStyleBackColor = False
        '
        'cmdCD
        '
        Me.cmdCD.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCD.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCD.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCD.Image = CType(resources.GetObject("cmdCD.Image"), System.Drawing.Image)
        Me.cmdCD.Location = New System.Drawing.Point(400, 9)
        Me.cmdCD.Name = "cmdCD"
        Me.cmdCD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCD.Size = New System.Drawing.Size(67, 37)
        Me.cmdCD.TabIndex = 37
        Me.cmdCD.Text = "Create CD"
        Me.cmdCD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCD, "Show Record")
        Me.cmdCD.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(533, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 39
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(467, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 38
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(600, 9)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 40
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(266, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 36
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 22)
        Me.SSTab1.Location = New System.Drawing.Point(0, 0)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 1
        Me.SSTab1.Size = New System.Drawing.Size(908, 576)
        Me.SSTab1.TabIndex = 41
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.Frame3)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 26)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(900, 546)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Company Details 1"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtTokenNo)
        Me.Frame3.Controls.Add(Me.Frame7)
        Me.Frame3.Controls.Add(Me.txtPanNo)
        Me.Frame3.Controls.Add(Me.txtTDSAcNo)
        Me.Frame3.Controls.Add(Me.txtReturnFiled)
        Me.Frame3.Controls.Add(Me.txtProvReceiptNo)
        Me.Frame3.Controls.Add(Me.txtAYear)
        Me.Frame3.Controls.Add(Me.txtFYear)
        Me.Frame3.Controls.Add(Me.TxtAccount)
        Me.Frame3.Controls.Add(Me.CmdSearch)
        Me.Frame3.Controls.Add(Me.Label28)
        Me.Frame3.Controls.Add(Me.Label20)
        Me.Frame3.Controls.Add(Me.Label19)
        Me.Frame3.Controls.Add(Me.Label18)
        Me.Frame3.Controls.Add(Me.Label14)
        Me.Frame3.Controls.Add(Me.Label13)
        Me.Frame3.Controls.Add(Me.Label8)
        Me.Frame3.Controls.Add(Me.Label4)
        Me.Frame3.Controls.Add(Me.Label15)
        Me.Frame3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(900, 546)
        Me.Frame3.TabIndex = 43
        Me.Frame3.TabStop = False
        '
        'txtTokenNo
        '
        Me.txtTokenNo.AcceptsReturn = True
        Me.txtTokenNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTokenNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTokenNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTokenNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTokenNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTokenNo.Location = New System.Drawing.Point(509, 254)
        Me.txtTokenNo.MaxLength = 0
        Me.txtTokenNo.Name = "txtTokenNo"
        Me.txtTokenNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTokenNo.Size = New System.Drawing.Size(123, 19)
        Me.txtTokenNo.TabIndex = 8
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.lstSection)
        Me.Frame7.Controls.Add(Me.txtDateFrom)
        Me.Frame7.Controls.Add(Me.txtDateTo)
        Me.Frame7.Controls.Add(Me.Label16)
        Me.Frame7.Controls.Add(Me.Label17)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(2, 0)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(898, 85)
        Me.Frame7.TabIndex = 82
        Me.Frame7.TabStop = False
        '
        'lstSection
        '
        Me.lstSection.BackColor = System.Drawing.SystemColors.Window
        Me.lstSection.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstSection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstSection.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstSection.IntegralHeight = False
        Me.lstSection.Items.AddRange(New Object() {"lstSection"})
        Me.lstSection.Location = New System.Drawing.Point(396, 10)
        Me.lstSection.Name = "lstSection"
        Me.lstSection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstSection.Size = New System.Drawing.Size(219, 70)
        Me.lstSection.TabIndex = 90
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(49, 14)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(83, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(49, 42)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(83, 20)
        Me.txtDateTo.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(10, 18)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(45, 14)
        Me.Label16.TabIndex = 84
        Me.Label16.Text = "From : "
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(22, 46)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(29, 14)
        Me.Label17.TabIndex = 83
        Me.Label17.Text = "To : "
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPanNo
        '
        Me.txtPanNo.AcceptsReturn = True
        Me.txtPanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPanNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPanNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPanNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPanNo.Location = New System.Drawing.Point(509, 124)
        Me.txtPanNo.MaxLength = 0
        Me.txtPanNo.Name = "txtPanNo"
        Me.txtPanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPanNo.Size = New System.Drawing.Size(123, 19)
        Me.txtPanNo.TabIndex = 3
        '
        'txtTDSAcNo
        '
        Me.txtTDSAcNo.AcceptsReturn = True
        Me.txtTDSAcNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSAcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSAcNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSAcNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSAcNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSAcNo.Location = New System.Drawing.Point(509, 98)
        Me.txtTDSAcNo.MaxLength = 0
        Me.txtTDSAcNo.Name = "txtTDSAcNo"
        Me.txtTDSAcNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSAcNo.Size = New System.Drawing.Size(123, 19)
        Me.txtTDSAcNo.TabIndex = 2
        '
        'txtReturnFiled
        '
        Me.txtReturnFiled.AcceptsReturn = True
        Me.txtReturnFiled.BackColor = System.Drawing.SystemColors.Window
        Me.txtReturnFiled.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReturnFiled.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReturnFiled.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReturnFiled.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReturnFiled.Location = New System.Drawing.Point(509, 202)
        Me.txtReturnFiled.MaxLength = 0
        Me.txtReturnFiled.Name = "txtReturnFiled"
        Me.txtReturnFiled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReturnFiled.Size = New System.Drawing.Size(123, 19)
        Me.txtReturnFiled.TabIndex = 6
        '
        'txtProvReceiptNo
        '
        Me.txtProvReceiptNo.AcceptsReturn = True
        Me.txtProvReceiptNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtProvReceiptNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProvReceiptNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProvReceiptNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProvReceiptNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProvReceiptNo.Location = New System.Drawing.Point(509, 228)
        Me.txtProvReceiptNo.MaxLength = 0
        Me.txtProvReceiptNo.Name = "txtProvReceiptNo"
        Me.txtProvReceiptNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProvReceiptNo.Size = New System.Drawing.Size(123, 19)
        Me.txtProvReceiptNo.TabIndex = 7
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(24, 256)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(337, 14)
        Me.Label28.TabIndex = 94
        Me.Label28.Text = "(g) Token no. of previous regular statement (Form no. 26Q) :"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(24, 126)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(159, 14)
        Me.Label20.TabIndex = 53
        Me.Label20.Text = "(b) Permanent A/c Number :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(24, 100)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(173, 14)
        Me.Label19.TabIndex = 52
        Me.Label19.Text = "(a) Tax Deduction A/c Number :"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(12, 100)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(19, 14)
        Me.Label18.TabIndex = 51
        Me.Label18.Text = "1. "
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(24, 178)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(130, 14)
        Me.Label14.TabIndex = 47
        Me.Label14.Text = "(d) Assessment Year :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(24, 204)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(365, 14)
        Me.Label13.TabIndex = 46
        Me.Label13.Text = "(e) Has any statement been filed earlier for this quarter (Yes/No) :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(24, 230)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(431, 14)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "(f) If answer of (e) is 'Yes', then Provisional Receipt No. of original statement" &
    " :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(24, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(104, 14)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "(c) Financial Year :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(0, 24)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(116, 14)
        Me.Label15.TabIndex = 92
        Me.Label15.Text = "TDS Account Name :"
        Me.Label15.Visible = False
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame2)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 26)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(900, 546)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Company Details 2"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtUACK)
        Me.Frame2.Controls.Add(Me.txtMobileNo)
        Me.Frame2.Controls.Add(Me.txtPhone)
        Me.Frame2.Controls.Add(Me.txtEmail)
        Me.Frame2.Controls.Add(Me.txtBranch)
        Me.Frame2.Controls.Add(Me.txtDeductorType)
        Me.Frame2.Controls.Add(Me.txtFlat)
        Me.Frame2.Controls.Add(Me.txtBuilding)
        Me.Frame2.Controls.Add(Me.txtRoad)
        Me.Frame2.Controls.Add(Me.txtArea)
        Me.Frame2.Controls.Add(Me.txtTown)
        Me.Frame2.Controls.Add(Me.txtState)
        Me.Frame2.Controls.Add(Me.txtPinCode)
        Me.Frame2.Controls.Add(Me.txtPersonName)
        Me.Frame2.Controls.Add(Me.Label41)
        Me.Frame2.Controls.Add(Me.Label39)
        Me.Frame2.Controls.Add(Me.Label25)
        Me.Frame2.Controls.Add(Me.Label23)
        Me.Frame2.Controls.Add(Me.Label22)
        Me.Frame2.Controls.Add(Me.Label21)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.Label9)
        Me.Frame2.Controls.Add(Me.Label10)
        Me.Frame2.Controls.Add(Me.Label11)
        Me.Frame2.Controls.Add(Me.Label12)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(900, 546)
        Me.Frame2.TabIndex = 54
        Me.Frame2.TabStop = False
        '
        'txtUACK
        '
        Me.txtUACK.AcceptsReturn = True
        Me.txtUACK.BackColor = System.Drawing.SystemColors.Window
        Me.txtUACK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUACK.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUACK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUACK.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUACK.Location = New System.Drawing.Point(241, 286)
        Me.txtUACK.MaxLength = 0
        Me.txtUACK.Name = "txtUACK"
        Me.txtUACK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUACK.Size = New System.Drawing.Size(179, 19)
        Me.txtUACK.TabIndex = 96
        '
        'txtMobileNo
        '
        Me.txtMobileNo.AcceptsReturn = True
        Me.txtMobileNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobileNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMobileNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobileNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMobileNo.Location = New System.Drawing.Point(555, 246)
        Me.txtMobileNo.MaxLength = 0
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMobileNo.Size = New System.Drawing.Size(101, 19)
        Me.txtMobileNo.TabIndex = 20
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPhone.Location = New System.Drawing.Point(241, 246)
        Me.txtPhone.MaxLength = 0
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(179, 19)
        Me.txtPhone.TabIndex = 19
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmail.Location = New System.Drawing.Point(241, 266)
        Me.txtEmail.MaxLength = 0
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail.Size = New System.Drawing.Size(415, 19)
        Me.txtEmail.TabIndex = 21
        '
        'txtFlat
        '
        Me.txtFlat.AcceptsReturn = True
        Me.txtFlat.BackColor = System.Drawing.SystemColors.Window
        Me.txtFlat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFlat.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFlat.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFlat.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFlat.Location = New System.Drawing.Point(241, 126)
        Me.txtFlat.MaxLength = 0
        Me.txtFlat.Name = "txtFlat"
        Me.txtFlat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFlat.Size = New System.Drawing.Size(415, 19)
        Me.txtFlat.TabIndex = 12
        '
        'txtBuilding
        '
        Me.txtBuilding.AcceptsReturn = True
        Me.txtBuilding.BackColor = System.Drawing.SystemColors.Window
        Me.txtBuilding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuilding.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBuilding.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuilding.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBuilding.Location = New System.Drawing.Point(241, 146)
        Me.txtBuilding.MaxLength = 0
        Me.txtBuilding.Name = "txtBuilding"
        Me.txtBuilding.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBuilding.Size = New System.Drawing.Size(415, 19)
        Me.txtBuilding.TabIndex = 13
        '
        'txtRoad
        '
        Me.txtRoad.AcceptsReturn = True
        Me.txtRoad.BackColor = System.Drawing.SystemColors.Window
        Me.txtRoad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRoad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRoad.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRoad.Location = New System.Drawing.Point(241, 166)
        Me.txtRoad.MaxLength = 0
        Me.txtRoad.Name = "txtRoad"
        Me.txtRoad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRoad.Size = New System.Drawing.Size(415, 19)
        Me.txtRoad.TabIndex = 14
        '
        'txtArea
        '
        Me.txtArea.AcceptsReturn = True
        Me.txtArea.BackColor = System.Drawing.SystemColors.Window
        Me.txtArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtArea.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtArea.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArea.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtArea.Location = New System.Drawing.Point(241, 186)
        Me.txtArea.MaxLength = 0
        Me.txtArea.Name = "txtArea"
        Me.txtArea.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtArea.Size = New System.Drawing.Size(415, 19)
        Me.txtArea.TabIndex = 15
        '
        'txtTown
        '
        Me.txtTown.AcceptsReturn = True
        Me.txtTown.BackColor = System.Drawing.SystemColors.Window
        Me.txtTown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTown.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTown.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTown.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTown.Location = New System.Drawing.Point(241, 206)
        Me.txtTown.MaxLength = 0
        Me.txtTown.Name = "txtTown"
        Me.txtTown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTown.Size = New System.Drawing.Size(415, 19)
        Me.txtTown.TabIndex = 16
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.BackColor = System.Drawing.SystemColors.Window
        Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtState.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtState.Location = New System.Drawing.Point(241, 226)
        Me.txtState.MaxLength = 0
        Me.txtState.Name = "txtState"
        Me.txtState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtState.Size = New System.Drawing.Size(179, 19)
        Me.txtState.TabIndex = 17
        '
        'txtPinCode
        '
        Me.txtPinCode.AcceptsReturn = True
        Me.txtPinCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtPinCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPinCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPinCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPinCode.Location = New System.Drawing.Point(555, 226)
        Me.txtPinCode.MaxLength = 0
        Me.txtPinCode.Name = "txtPinCode"
        Me.txtPinCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinCode.Size = New System.Drawing.Size(101, 19)
        Me.txtPinCode.TabIndex = 18
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label41.Location = New System.Drawing.Point(38, 284)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(423, 14)
        Me.Label41.TabIndex = 97
        Me.Label41.Text = "Unique acknowledgement of the corrosponding form no 15CA (if available) :"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(476, 248)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(70, 14)
        Me.Label39.TabIndex = 89
        Me.Label39.Text = "Mobile No. :"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(40, 248)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(91, 14)
        Me.Label25.TabIndex = 68
        Me.Label25.Text = "Telephone No. :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(40, 268)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(46, 14)
        Me.Label23.TabIndex = 67
        Me.Label23.Text = "E-mail :"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(24, 86)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(154, 14)
        Me.Label22.TabIndex = 66
        Me.Label22.Text = "(c) Branch/Division (if any) :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(24, 62)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(124, 14)
        Me.Label21.TabIndex = 65
        Me.Label21.Text = "(b) Type of deductor :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(12, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(172, 14)
        Me.Label7.TabIndex = 64
        Me.Label7.Text = "2. Particulars of the deductor :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(42, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(52, 14)
        Me.Label6.TabIndex = 63
        Me.Label6.Text = "Flat No. :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(42, 148)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(189, 14)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "Name of the Premises / Building :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(42, 168)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(119, 14)
        Me.Label2.TabIndex = 61
        Me.Label2.Text = "Road / Street / Lane :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(42, 188)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(90, 14)
        Me.Label9.TabIndex = 60
        Me.Label9.Text = "Area / Locality :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(42, 208)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(121, 14)
        Me.Label10.TabIndex = 59
        Me.Label10.Text = "Town / City / District :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(42, 228)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(41, 14)
        Me.Label11.TabIndex = 58
        Me.Label11.Text = "State :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(486, 228)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(62, 14)
        Me.Label12.TabIndex = 57
        Me.Label12.Text = "Pin Code :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(24, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(79, 14)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "(d) Address :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(24, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 55
        Me.Label1.Text = "(a) Name :"
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame6)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 26)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(900, 546)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "Company Details 3"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtResponPANNo)
        Me.Frame6.Controls.Add(Me.chkPersonAddChange)
        Me.Frame6.Controls.Add(Me.txtDesg)
        Me.Frame6.Controls.Add(Me.txtPersonName_p)
        Me.Frame6.Controls.Add(Me.txtPinCode_p)
        Me.Frame6.Controls.Add(Me.txtState_p)
        Me.Frame6.Controls.Add(Me.txtTown_p)
        Me.Frame6.Controls.Add(Me.txtArea_p)
        Me.Frame6.Controls.Add(Me.txtRoad_p)
        Me.Frame6.Controls.Add(Me.txtBuilding_p)
        Me.Frame6.Controls.Add(Me.txtFlat_p)
        Me.Frame6.Controls.Add(Me.txtEmail_p)
        Me.Frame6.Controls.Add(Me.txtPhone_p)
        Me.Frame6.Controls.Add(Me.Label40)
        Me.Frame6.Controls.Add(Me.Label27)
        Me.Frame6.Controls.Add(Me.Label38)
        Me.Frame6.Controls.Add(Me.Label37)
        Me.Frame6.Controls.Add(Me.Label36)
        Me.Frame6.Controls.Add(Me.Label35)
        Me.Frame6.Controls.Add(Me.Label34)
        Me.Frame6.Controls.Add(Me.Label33)
        Me.Frame6.Controls.Add(Me.Label32)
        Me.Frame6.Controls.Add(Me.Label31)
        Me.Frame6.Controls.Add(Me.Label30)
        Me.Frame6.Controls.Add(Me.Label29)
        Me.Frame6.Controls.Add(Me.Label26)
        Me.Frame6.Controls.Add(Me.Label24)
        Me.Frame6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(900, 546)
        Me.Frame6.TabIndex = 69
        Me.Frame6.TabStop = False
        '
        'txtResponPANNo
        '
        Me.txtResponPANNo.AcceptsReturn = True
        Me.txtResponPANNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtResponPANNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResponPANNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtResponPANNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResponPANNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtResponPANNo.Location = New System.Drawing.Point(555, 266)
        Me.txtResponPANNo.MaxLength = 0
        Me.txtResponPANNo.Name = "txtResponPANNo"
        Me.txtResponPANNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtResponPANNo.Size = New System.Drawing.Size(101, 19)
        Me.txtResponPANNo.TabIndex = 33
        '
        'chkPersonAddChange
        '
        Me.chkPersonAddChange.AutoSize = True
        Me.chkPersonAddChange.BackColor = System.Drawing.SystemColors.Control
        Me.chkPersonAddChange.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPersonAddChange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPersonAddChange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPersonAddChange.Location = New System.Drawing.Point(240, 294)
        Me.chkPersonAddChange.Name = "chkPersonAddChange"
        Me.chkPersonAddChange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPersonAddChange.Size = New System.Drawing.Size(358, 18)
        Me.chkPersonAddChange.TabIndex = 86
        Me.chkPersonAddChange.Text = "Change of Address of Responsible Person since last Return"
        Me.chkPersonAddChange.UseVisualStyleBackColor = False
        '
        'txtPinCode_p
        '
        Me.txtPinCode_p.AcceptsReturn = True
        Me.txtPinCode_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtPinCode_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPinCode_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPinCode_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCode_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPinCode_p.Location = New System.Drawing.Point(555, 218)
        Me.txtPinCode_p.MaxLength = 0
        Me.txtPinCode_p.Name = "txtPinCode_p"
        Me.txtPinCode_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinCode_p.Size = New System.Drawing.Size(101, 19)
        Me.txtPinCode_p.TabIndex = 30
        '
        'txtState_p
        '
        Me.txtState_p.AcceptsReturn = True
        Me.txtState_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtState_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtState_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtState_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtState_p.Location = New System.Drawing.Point(241, 218)
        Me.txtState_p.MaxLength = 0
        Me.txtState_p.Name = "txtState_p"
        Me.txtState_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtState_p.Size = New System.Drawing.Size(179, 19)
        Me.txtState_p.TabIndex = 29
        '
        'txtTown_p
        '
        Me.txtTown_p.AcceptsReturn = True
        Me.txtTown_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtTown_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTown_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTown_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTown_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTown_p.Location = New System.Drawing.Point(241, 194)
        Me.txtTown_p.MaxLength = 0
        Me.txtTown_p.Name = "txtTown_p"
        Me.txtTown_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTown_p.Size = New System.Drawing.Size(415, 19)
        Me.txtTown_p.TabIndex = 28
        '
        'txtArea_p
        '
        Me.txtArea_p.AcceptsReturn = True
        Me.txtArea_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtArea_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtArea_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtArea_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArea_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtArea_p.Location = New System.Drawing.Point(241, 170)
        Me.txtArea_p.MaxLength = 0
        Me.txtArea_p.Name = "txtArea_p"
        Me.txtArea_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtArea_p.Size = New System.Drawing.Size(415, 19)
        Me.txtArea_p.TabIndex = 27
        '
        'txtRoad_p
        '
        Me.txtRoad_p.AcceptsReturn = True
        Me.txtRoad_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtRoad_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRoad_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRoad_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRoad_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRoad_p.Location = New System.Drawing.Point(241, 146)
        Me.txtRoad_p.MaxLength = 0
        Me.txtRoad_p.Name = "txtRoad_p"
        Me.txtRoad_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRoad_p.Size = New System.Drawing.Size(415, 19)
        Me.txtRoad_p.TabIndex = 26
        '
        'txtBuilding_p
        '
        Me.txtBuilding_p.AcceptsReturn = True
        Me.txtBuilding_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtBuilding_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBuilding_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBuilding_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBuilding_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBuilding_p.Location = New System.Drawing.Point(241, 122)
        Me.txtBuilding_p.MaxLength = 0
        Me.txtBuilding_p.Name = "txtBuilding_p"
        Me.txtBuilding_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBuilding_p.Size = New System.Drawing.Size(415, 19)
        Me.txtBuilding_p.TabIndex = 25
        '
        'txtFlat_p
        '
        Me.txtFlat_p.AcceptsReturn = True
        Me.txtFlat_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtFlat_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFlat_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFlat_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFlat_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFlat_p.Location = New System.Drawing.Point(241, 98)
        Me.txtFlat_p.MaxLength = 0
        Me.txtFlat_p.Name = "txtFlat_p"
        Me.txtFlat_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFlat_p.Size = New System.Drawing.Size(415, 19)
        Me.txtFlat_p.TabIndex = 24
        '
        'txtEmail_p
        '
        Me.txtEmail_p.AcceptsReturn = True
        Me.txtEmail_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmail_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmail_p.Location = New System.Drawing.Point(241, 266)
        Me.txtEmail_p.MaxLength = 0
        Me.txtEmail_p.Name = "txtEmail_p"
        Me.txtEmail_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail_p.Size = New System.Drawing.Size(181, 19)
        Me.txtEmail_p.TabIndex = 32
        '
        'txtPhone_p
        '
        Me.txtPhone_p.AcceptsReturn = True
        Me.txtPhone_p.BackColor = System.Drawing.SystemColors.Window
        Me.txtPhone_p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone_p.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone_p.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone_p.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPhone_p.Location = New System.Drawing.Point(241, 242)
        Me.txtPhone_p.MaxLength = 0
        Me.txtPhone_p.Name = "txtPhone_p"
        Me.txtPhone_p.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone_p.Size = New System.Drawing.Size(415, 19)
        Me.txtPhone_p.TabIndex = 31
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(486, 268)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(51, 14)
        Me.Label40.TabIndex = 95
        Me.Label40.Text = "PAN No :"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(42, 62)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(78, 14)
        Me.Label27.TabIndex = 85
        Me.Label27.Text = "Designation :"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.SystemColors.Control
        Me.Label38.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label38.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label38.Location = New System.Drawing.Point(24, 38)
        Me.Label38.Name = "Label38"
        Me.Label38.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label38.Size = New System.Drawing.Size(61, 14)
        Me.Label38.TabIndex = 81
        Me.Label38.Text = "(a) Name :"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(24, 82)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(79, 14)
        Me.Label37.TabIndex = 80
        Me.Label37.Text = "(b) Address :"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(486, 220)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(62, 14)
        Me.Label36.TabIndex = 79
        Me.Label36.Text = "Pin Code :"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(42, 220)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(41, 14)
        Me.Label35.TabIndex = 78
        Me.Label35.Text = "State :"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(42, 196)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(121, 14)
        Me.Label34.TabIndex = 77
        Me.Label34.Text = "Town / City / District :"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(42, 172)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(90, 14)
        Me.Label33.TabIndex = 76
        Me.Label33.Text = "Area / Locality :"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(42, 148)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(119, 14)
        Me.Label32.TabIndex = 75
        Me.Label32.Text = "Road / Street / Lane :"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(42, 124)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(189, 14)
        Me.Label31.TabIndex = 74
        Me.Label31.Text = "Name of the Premises / Building :"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(42, 100)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(52, 14)
        Me.Label30.TabIndex = 73
        Me.Label30.Text = "Flat No. :"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(12, 16)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(342, 14)
        Me.Label29.TabIndex = 72
        Me.Label29.Text = "2. Particulars of the person responsible for deduction of tax :"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(40, 268)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(46, 14)
        Me.Label26.TabIndex = 71
        Me.Label26.Text = "E-mail :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(40, 244)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(91, 14)
        Me.Label24.TabIndex = 70
        Me.Label24.Text = "Telephone No. :"
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.Frame1)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 26)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(900, 546)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "Challan Detail"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.SprdView26)
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(900, 546)
        Me.Frame1.TabIndex = 50
        Me.Frame1.TabStop = False
        '
        'SprdView26
        '
        Me.SprdView26.DataSource = Nothing
        Me.SprdView26.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdView26.Location = New System.Drawing.Point(0, 13)
        Me.SprdView26.Name = "SprdView26"
        Me.SprdView26.OcxState = CType(resources.GetObject("SprdView26.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView26.Size = New System.Drawing.Size(900, 533)
        Me.SprdView26.TabIndex = 34
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me.Frame4)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 26)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(900, 546)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "Annexure Detail"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdViewAnnex)
        Me.Frame4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(900, 546)
        Me.Frame4.TabIndex = 49
        Me.Frame4.TabStop = False
        '
        'SprdViewAnnex
        '
        Me.SprdViewAnnex.DataSource = Nothing
        Me.SprdViewAnnex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdViewAnnex.Location = New System.Drawing.Point(0, 13)
        Me.SprdViewAnnex.Name = "SprdViewAnnex"
        Me.SprdViewAnnex.OcxState = CType(resources.GetObject("SprdViewAnnex.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdViewAnnex.Size = New System.Drawing.Size(900, 533)
        Me.SprdViewAnnex.TabIndex = 35
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdValidate)
        Me.FraMovement.Controls.Add(Me.chkConsolidated)
        Me.FraMovement.Controls.Add(Me.cmdCD)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.lblFormType)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(2, 572)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(906, 49)
        Me.FraMovement.TabIndex = 42
        Me.FraMovement.TabStop = False
        '
        'chkConsolidated
        '
        Me.chkConsolidated.BackColor = System.Drawing.SystemColors.Control
        Me.chkConsolidated.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkConsolidated.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkConsolidated.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkConsolidated.Location = New System.Drawing.Point(12, 20)
        Me.chkConsolidated.Name = "chkConsolidated"
        Me.chkConsolidated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkConsolidated.Size = New System.Drawing.Size(177, 19)
        Me.chkConsolidated.TabIndex = 48
        Me.chkConsolidated.Text = "Consolidated"
        Me.chkConsolidated.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(280, 10)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 88
        '
        'lblFormType
        '
        Me.lblFormType.AutoSize = True
        Me.lblFormType.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFormType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFormType.Location = New System.Drawing.Point(202, 16)
        Me.lblFormType.Name = "lblFormType"
        Me.lblFormType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFormType.Size = New System.Drawing.Size(64, 14)
        Me.lblFormType.TabIndex = 88
        Me.lblFormType.Text = "lblFormType"
        '
        'frmTDSeReturn26Q
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.SSTab1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTDSeReturn26Q"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "TDS e-Return (Form 26Q)"
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdView26, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdViewAnnex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdViewAnnex.DataSource = CType(AData1, MSDATASRC.DataSource)
        'SprdView26.DataSource = CType(AData26, MSDATASRC.DataSource)
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdViewAnnex.DataSource = Nothing
		SprdView26.DataSource = Nothing
	End Sub
#End Region 
End Class