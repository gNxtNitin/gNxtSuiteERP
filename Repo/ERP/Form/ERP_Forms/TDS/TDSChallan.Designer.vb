<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTDSChallan
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
	Public WithEvents txtTDSAmount As System.Windows.Forms.TextBox
	Public WithEvents txtSurcharge As System.Windows.Forms.TextBox
	Public WithEvents txtCess As System.Windows.Forms.TextBox
	Public WithEvents txtInterest As System.Windows.Forms.TextBox
	Public WithEvents txtOthers As System.Windows.Forms.TextBox
	Public WithEvents txtNetAmount As System.Windows.Forms.TextBox
	Public WithEvents txtAmountPaid As System.Windows.Forms.TextBox
	Public WithEvents txtTdsPer As System.Windows.Forms.TextBox
	Public WithEvents _OptSelection_2 As System.Windows.Forms.RadioButton
	Public WithEvents _OptSelection_0 As System.Windows.Forms.RadioButton
	Public WithEvents _OptSelection_1 As System.Windows.Forms.RadioButton
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents txtChqDate As System.Windows.Forms.TextBox
	Public WithEvents txtChqNo As System.Windows.Forms.TextBox
	Public WithEvents cmdSection As System.Windows.Forms.Button
	Public WithEvents txtSectionName As System.Windows.Forms.TextBox
	Public WithEvents txtBankCode As System.Windows.Forms.TextBox
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents txtRefNo As System.Windows.Forms.TextBox
	Public WithEvents txtChallanNo As System.Windows.Forms.TextBox
	Public WithEvents txtChallanDate As System.Windows.Forms.TextBox
	Public WithEvents txtBankName As System.Windows.Forms.TextBox
	Public WithEvents TxtAccount As System.Windows.Forms.TextBox
	Public WithEvents txtDateFrom As System.Windows.Forms.TextBox
	Public WithEvents txtDateTo As System.Windows.Forms.TextBox
	Public WithEvents CmdSearch As System.Windows.Forms.Button
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents _Lbl_4 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents lblMKey As System.Windows.Forms.Label
	Public WithEvents _Lbl_3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Lable11 As System.Windows.Forms.Label
	Public WithEvents _Lbl_1 As System.Windows.Forms.Label
	Public WithEvents _Lbl_0 As System.Windows.Forms.Label
	Public WithEvents _Lbl_2 As System.Windows.Forms.Label
	Public WithEvents FraChallan As System.Windows.Forms.GroupBox
    'Public WithEvents AData1 As VB6.ADODC
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents FraView As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents cmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'Public WithEvents ADataGrid As VB6.ADODC
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents OptSelection As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTDSChallan))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtTdsPer = New System.Windows.Forms.TextBox()
        Me.cmdSection = New System.Windows.Forms.Button()
        Me.txtBankCode = New System.Windows.Forms.TextBox()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.CmdSearch = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.txtTDSAmount = New System.Windows.Forms.TextBox()
        Me.txtSurcharge = New System.Windows.Forms.TextBox()
        Me.txtCess = New System.Windows.Forms.TextBox()
        Me.txtInterest = New System.Windows.Forms.TextBox()
        Me.txtOthers = New System.Windows.Forms.TextBox()
        Me.txtNetAmount = New System.Windows.Forms.TextBox()
        Me.txtAmountPaid = New System.Windows.Forms.TextBox()
        Me.FraChallan = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptSelection_2 = New System.Windows.Forms.RadioButton()
        Me._OptSelection_0 = New System.Windows.Forms.RadioButton()
        Me._OptSelection_1 = New System.Windows.Forms.RadioButton()
        Me.txtChqDate = New System.Windows.Forms.TextBox()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.txtSectionName = New System.Windows.Forms.TextBox()
        Me.txtChallanNo = New System.Windows.Forms.TextBox()
        Me.txtChallanDate = New System.Windows.Forms.TextBox()
        Me.txtDateFrom = New System.Windows.Forms.TextBox()
        Me.txtDateTo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me._Lbl_4 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me._Lbl_3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Lable11 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me._Lbl_2 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptSelection = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraView.SuspendLayout()
        Me.FraChallan.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTdsPer
        '
        Me.txtTdsPer.AcceptsReturn = True
        Me.txtTdsPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtTdsPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTdsPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTdsPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTdsPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTdsPer.Location = New System.Drawing.Point(4, 32)
        Me.txtTdsPer.MaxLength = 0
        Me.txtTdsPer.Name = "txtTdsPer"
        Me.txtTdsPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTdsPer.Size = New System.Drawing.Size(41, 20)
        Me.txtTdsPer.TabIndex = 58
        Me.ToolTip1.SetToolTip(Me.txtTdsPer, "Press F1 For Help")
        '
        'cmdSection
        '
        Me.cmdSection.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSection.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSection.Image = CType(resources.GetObject("cmdSection.Image"), System.Drawing.Image)
        Me.cmdSection.Location = New System.Drawing.Point(410, 54)
        Me.cmdSection.Name = "cmdSection"
        Me.cmdSection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSection.Size = New System.Drawing.Size(27, 19)
        Me.cmdSection.TabIndex = 7
        Me.cmdSection.TabStop = False
        Me.cmdSection.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSection, "Search")
        Me.cmdSection.UseVisualStyleBackColor = False
        '
        'txtBankCode
        '
        Me.txtBankCode.AcceptsReturn = True
        Me.txtBankCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBankCode.Location = New System.Drawing.Point(590, 76)
        Me.txtBankCode.MaxLength = 0
        Me.txtBankCode.Name = "txtBankCode"
        Me.txtBankCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankCode.Size = New System.Drawing.Size(61, 20)
        Me.txtBankCode.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtBankCode, "Press F1 For Help")
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(584, 102)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(69, 35)
        Me.cmdShow.TabIndex = 14
        Me.cmdShow.Text = "Populate"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefNo.Location = New System.Drawing.Point(106, 10)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(79, 20)
        Me.txtRefNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtRefNo, "Press F1 For Help")
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBankName.Location = New System.Drawing.Point(106, 76)
        Me.txtBankName.MaxLength = 0
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(303, 20)
        Me.txtBankName.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtBankName, "Press F1 For Help")
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(106, 32)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(303, 20)
        Me.TxtAccount.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'CmdSearch
        '
        Me.CmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearch.Image = CType(resources.GetObject("CmdSearch.Image"), System.Drawing.Image)
        Me.CmdSearch.Location = New System.Drawing.Point(410, 32)
        Me.CmdSearch.Name = "CmdSearch"
        Me.CmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearch.Size = New System.Drawing.Size(27, 19)
        Me.CmdSearch.TabIndex = 5
        Me.CmdSearch.TabStop = False
        Me.CmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearch, "Search")
        Me.CmdSearch.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(180, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(240, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 37)
        Me.CmdModify.TabIndex = 24
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(420, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 37)
        Me.CmdDelete.TabIndex = 27
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(300, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
        Me.CmdSave.TabIndex = 25
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(600, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 37)
        Me.CmdView.TabIndex = 30
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(660, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 31
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(480, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 28
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.txtTDSAmount)
        Me.FraView.Controls.Add(Me.txtSurcharge)
        Me.FraView.Controls.Add(Me.txtCess)
        Me.FraView.Controls.Add(Me.txtInterest)
        Me.FraView.Controls.Add(Me.txtOthers)
        Me.FraView.Controls.Add(Me.txtNetAmount)
        Me.FraView.Controls.Add(Me.txtAmountPaid)
        Me.FraView.Controls.Add(Me.FraChallan)
        Me.FraView.Controls.Add(Me.SprdMain)
        Me.FraView.Controls.Add(Me.Label7)
        Me.FraView.Controls.Add(Me.Label8)
        Me.FraView.Controls.Add(Me.Label9)
        Me.FraView.Controls.Add(Me.Label10)
        Me.FraView.Controls.Add(Me.Label11)
        Me.FraView.Controls.Add(Me.Label12)
        Me.FraView.Controls.Add(Me.Label3)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -4)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(910, 576)
        Me.FraView.TabIndex = 23
        Me.FraView.TabStop = False
        '
        'txtTDSAmount
        '
        Me.txtTDSAmount.AcceptsReturn = True
        Me.txtTDSAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTDSAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTDSAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTDSAmount.Enabled = False
        Me.txtTDSAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTDSAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTDSAmount.Location = New System.Drawing.Point(377, 526)
        Me.txtTDSAmount.MaxLength = 0
        Me.txtTDSAmount.Name = "txtTDSAmount"
        Me.txtTDSAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTDSAmount.Size = New System.Drawing.Size(79, 20)
        Me.txtTDSAmount.TabIndex = 17
        Me.txtTDSAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSurcharge
        '
        Me.txtSurcharge.AcceptsReturn = True
        Me.txtSurcharge.BackColor = System.Drawing.SystemColors.Window
        Me.txtSurcharge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSurcharge.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSurcharge.Enabled = False
        Me.txtSurcharge.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSurcharge.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSurcharge.Location = New System.Drawing.Point(601, 526)
        Me.txtSurcharge.MaxLength = 0
        Me.txtSurcharge.Name = "txtSurcharge"
        Me.txtSurcharge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSurcharge.Size = New System.Drawing.Size(79, 20)
        Me.txtSurcharge.TabIndex = 18
        Me.txtSurcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCess
        '
        Me.txtCess.AcceptsReturn = True
        Me.txtCess.BackColor = System.Drawing.SystemColors.Window
        Me.txtCess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCess.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCess.Enabled = False
        Me.txtCess.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCess.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCess.Location = New System.Drawing.Point(826, 526)
        Me.txtCess.MaxLength = 0
        Me.txtCess.Name = "txtCess"
        Me.txtCess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCess.Size = New System.Drawing.Size(79, 20)
        Me.txtCess.TabIndex = 19
        Me.txtCess.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtInterest
        '
        Me.txtInterest.AcceptsReturn = True
        Me.txtInterest.BackColor = System.Drawing.SystemColors.Window
        Me.txtInterest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInterest.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInterest.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInterest.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInterest.Location = New System.Drawing.Point(377, 548)
        Me.txtInterest.MaxLength = 0
        Me.txtInterest.Name = "txtInterest"
        Me.txtInterest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInterest.Size = New System.Drawing.Size(79, 20)
        Me.txtInterest.TabIndex = 20
        Me.txtInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtOthers
        '
        Me.txtOthers.AcceptsReturn = True
        Me.txtOthers.BackColor = System.Drawing.SystemColors.Window
        Me.txtOthers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOthers.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOthers.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthers.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOthers.Location = New System.Drawing.Point(601, 548)
        Me.txtOthers.MaxLength = 0
        Me.txtOthers.Name = "txtOthers"
        Me.txtOthers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOthers.Size = New System.Drawing.Size(79, 20)
        Me.txtOthers.TabIndex = 21
        Me.txtOthers.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNetAmount
        '
        Me.txtNetAmount.AcceptsReturn = True
        Me.txtNetAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetAmount.Enabled = False
        Me.txtNetAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNetAmount.Location = New System.Drawing.Point(826, 548)
        Me.txtNetAmount.MaxLength = 0
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetAmount.Size = New System.Drawing.Size(79, 20)
        Me.txtNetAmount.TabIndex = 22
        Me.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAmountPaid
        '
        Me.txtAmountPaid.AcceptsReturn = True
        Me.txtAmountPaid.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmountPaid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmountPaid.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmountPaid.Enabled = False
        Me.txtAmountPaid.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmountPaid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmountPaid.Location = New System.Drawing.Point(826, 504)
        Me.txtAmountPaid.MaxLength = 0
        Me.txtAmountPaid.Name = "txtAmountPaid"
        Me.txtAmountPaid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmountPaid.Size = New System.Drawing.Size(79, 20)
        Me.txtAmountPaid.TabIndex = 16
        Me.txtAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'FraChallan
        '
        Me.FraChallan.BackColor = System.Drawing.SystemColors.Control
        Me.FraChallan.Controls.Add(Me.Frame1)
        Me.FraChallan.Controls.Add(Me.txtChqDate)
        Me.FraChallan.Controls.Add(Me.txtChqNo)
        Me.FraChallan.Controls.Add(Me.cmdSection)
        Me.FraChallan.Controls.Add(Me.txtSectionName)
        Me.FraChallan.Controls.Add(Me.txtBankCode)
        Me.FraChallan.Controls.Add(Me.cmdShow)
        Me.FraChallan.Controls.Add(Me.txtRefNo)
        Me.FraChallan.Controls.Add(Me.txtChallanNo)
        Me.FraChallan.Controls.Add(Me.txtChallanDate)
        Me.FraChallan.Controls.Add(Me.txtBankName)
        Me.FraChallan.Controls.Add(Me.TxtAccount)
        Me.FraChallan.Controls.Add(Me.txtDateFrom)
        Me.FraChallan.Controls.Add(Me.txtDateTo)
        Me.FraChallan.Controls.Add(Me.CmdSearch)
        Me.FraChallan.Controls.Add(Me.Label6)
        Me.FraChallan.Controls.Add(Me.Label5)
        Me.FraChallan.Controls.Add(Me._Lbl_4)
        Me.FraChallan.Controls.Add(Me.Label4)
        Me.FraChallan.Controls.Add(Me.lblMKey)
        Me.FraChallan.Controls.Add(Me._Lbl_3)
        Me.FraChallan.Controls.Add(Me.Label1)
        Me.FraChallan.Controls.Add(Me.Label2)
        Me.FraChallan.Controls.Add(Me.Lable11)
        Me.FraChallan.Controls.Add(Me._Lbl_1)
        Me.FraChallan.Controls.Add(Me._Lbl_0)
        Me.FraChallan.Controls.Add(Me._Lbl_2)
        Me.FraChallan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraChallan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FraChallan.Location = New System.Drawing.Point(0, 0)
        Me.FraChallan.Name = "FraChallan"
        Me.FraChallan.Padding = New System.Windows.Forms.Padding(0)
        Me.FraChallan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraChallan.Size = New System.Drawing.Size(912, 147)
        Me.FraChallan.TabIndex = 32
        Me.FraChallan.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtTdsPer)
        Me.Frame1.Controls.Add(Me._OptSelection_2)
        Me.Frame1.Controls.Add(Me._OptSelection_0)
        Me.Frame1.Controls.Add(Me._OptSelection_1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(416, 92)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(167, 55)
        Me.Frame1.TabIndex = 54
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Selection"
        '
        '_OptSelection_2
        '
        Me._OptSelection_2.AutoSize = True
        Me._OptSelection_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelection_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelection_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelection_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelection.SetIndex(Me._OptSelection_2, CType(2, Short))
        Me._OptSelection_2.Location = New System.Drawing.Point(54, 14)
        Me._OptSelection_2.Name = "_OptSelection_2"
        Me._OptSelection_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelection_2.Size = New System.Drawing.Size(44, 18)
        Me._OptSelection_2.TabIndex = 57
        Me._OptSelection_2.TabStop = True
        Me._OptSelection_2.Text = "Per"
        Me._OptSelection_2.UseVisualStyleBackColor = False
        '
        '_OptSelection_0
        '
        Me._OptSelection_0.AutoSize = True
        Me._OptSelection_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelection_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelection_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelection_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelection.SetIndex(Me._OptSelection_0, CType(0, Short))
        Me._OptSelection_0.Location = New System.Drawing.Point(2, 14)
        Me._OptSelection_0.Name = "_OptSelection_0"
        Me._OptSelection_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelection_0.Size = New System.Drawing.Size(39, 18)
        Me._OptSelection_0.TabIndex = 56
        Me._OptSelection_0.TabStop = True
        Me._OptSelection_0.Text = "All"
        Me._OptSelection_0.UseVisualStyleBackColor = False
        '
        '_OptSelection_1
        '
        Me._OptSelection_1.AutoSize = True
        Me._OptSelection_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelection_1.Checked = True
        Me._OptSelection_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelection_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelection_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelection.SetIndex(Me._OptSelection_1, CType(1, Short))
        Me._OptSelection_1.Location = New System.Drawing.Point(110, 14)
        Me._OptSelection_1.Name = "_OptSelection_1"
        Me._OptSelection_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelection_1.Size = New System.Drawing.Size(53, 18)
        Me._OptSelection_1.TabIndex = 55
        Me._OptSelection_1.TabStop = True
        Me._OptSelection_1.Text = "None"
        Me._OptSelection_1.UseVisualStyleBackColor = False
        '
        'txtChqDate
        '
        Me.txtChqDate.AcceptsReturn = True
        Me.txtChqDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChqDate.Location = New System.Drawing.Point(332, 120)
        Me.txtChqDate.MaxLength = 0
        Me.txtChqDate.Name = "txtChqDate"
        Me.txtChqDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqDate.Size = New System.Drawing.Size(79, 20)
        Me.txtChqDate.TabIndex = 13
        '
        'txtChqNo
        '
        Me.txtChqNo.AcceptsReturn = True
        Me.txtChqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChqNo.Location = New System.Drawing.Point(106, 120)
        Me.txtChqNo.MaxLength = 0
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqNo.Size = New System.Drawing.Size(79, 20)
        Me.txtChqNo.TabIndex = 12
        '
        'txtSectionName
        '
        Me.txtSectionName.AcceptsReturn = True
        Me.txtSectionName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSectionName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSectionName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSectionName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSectionName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSectionName.Location = New System.Drawing.Point(106, 54)
        Me.txtSectionName.MaxLength = 0
        Me.txtSectionName.Name = "txtSectionName"
        Me.txtSectionName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSectionName.Size = New System.Drawing.Size(303, 20)
        Me.txtSectionName.TabIndex = 6
        '
        'txtChallanNo
        '
        Me.txtChallanNo.AcceptsReturn = True
        Me.txtChallanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChallanNo.Location = New System.Drawing.Point(106, 98)
        Me.txtChallanNo.MaxLength = 0
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanNo.Size = New System.Drawing.Size(79, 20)
        Me.txtChallanNo.TabIndex = 10
        '
        'txtChallanDate
        '
        Me.txtChallanDate.AcceptsReturn = True
        Me.txtChallanDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChallanDate.Location = New System.Drawing.Point(332, 98)
        Me.txtChallanDate.MaxLength = 0
        Me.txtChallanDate.Name = "txtChallanDate"
        Me.txtChallanDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanDate.Size = New System.Drawing.Size(79, 20)
        Me.txtChallanDate.TabIndex = 11
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AcceptsReturn = True
        Me.txtDateFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDateFrom.Location = New System.Drawing.Point(436, 10)
        Me.txtDateFrom.MaxLength = 0
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateFrom.Size = New System.Drawing.Size(79, 20)
        Me.txtDateFrom.TabIndex = 2
        '
        'txtDateTo
        '
        Me.txtDateTo.AcceptsReturn = True
        Me.txtDateTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDateTo.Location = New System.Drawing.Point(572, 10)
        Me.txtDateTo.MaxLength = 0
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateTo.Size = New System.Drawing.Size(79, 20)
        Me.txtDateTo.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(232, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(85, 14)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Chq / DD Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(27, 123)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(75, 14)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Chq / DD No :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_4
        '
        Me._Lbl_4.AutoSize = True
        Me._Lbl_4.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_4, CType(4, Short))
        Me._Lbl_4.Location = New System.Drawing.Point(14, 56)
        Me._Lbl_4.Name = "_Lbl_4"
        Me._Lbl_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_4.Size = New System.Drawing.Size(88, 14)
        Me._Lbl_4.TabIndex = 44
        Me._Lbl_4.Text = "Section Name :"
        Me._Lbl_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(522, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(69, 14)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "BankCode :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(222, 12)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 35
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        '_Lbl_3
        '
        Me._Lbl_3.AutoSize = True
        Me._Lbl_3.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_3, CType(3, Short))
        Me._Lbl_3.Location = New System.Drawing.Point(54, 13)
        Me._Lbl_3.Name = "_Lbl_3"
        Me._Lbl_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_3.Size = New System.Drawing.Size(48, 14)
        Me._Lbl_3.TabIndex = 34
        Me._Lbl_3.Text = "Ref No :"
        Me._Lbl_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(32, 101)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(70, 14)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Challan No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(230, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(80, 14)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Challan Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Lable11
        '
        Me.Lable11.AutoSize = True
        Me.Lable11.BackColor = System.Drawing.SystemColors.Control
        Me.Lable11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Lable11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lable11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lable11.Location = New System.Drawing.Point(28, 77)
        Me.Lable11.Name = "Lable11"
        Me.Lable11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Lable11.Size = New System.Drawing.Size(74, 14)
        Me.Lable11.TabIndex = 38
        Me.Lable11.Text = "Bank Name :"
        Me.Lable11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(472, 11)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(26, 14)
        Me._Lbl_1.TabIndex = 37
        Me._Lbl_1.Text = "To :"
        Me._Lbl_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(348, 11)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(42, 14)
        Me._Lbl_0.TabIndex = 36
        Me._Lbl_0.Text = "From :"
        Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_2
        '
        Me._Lbl_2.AutoSize = True
        Me._Lbl_2.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_2, CType(2, Short))
        Me._Lbl_2.Location = New System.Drawing.Point(20, 35)
        Me._Lbl_2.Name = "_Lbl_2"
        Me._Lbl_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_2.Size = New System.Drawing.Size(82, 14)
        Me._Lbl_2.TabIndex = 33
        Me._Lbl_2.Text = "TDS Account :"
        Me._Lbl_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 150)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(906, 350)
        Me.SprdMain.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(294, 528)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(81, 14)
        Me.Label7.TabIndex = 53
        Me.Label7.Text = "TDS Amount :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(756, 530)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(68, 14)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Edu. Cess :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(319, 550)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(57, 14)
        Me.Label9.TabIndex = 51
        Me.Label9.Text = "Interest :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(552, 550)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(51, 14)
        Me.Label10.TabIndex = 50
        Me.Label10.Text = "Others :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(530, 528)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(70, 14)
        Me.Label11.TabIndex = 49
        Me.Label11.Text = "Surcharge :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(746, 550)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(78, 14)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "Net Amount :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(738, 506)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(83, 14)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Amount Paid :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(910, 572)
        Me.SprdView.TabIndex = 41
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(910, 51)
        Me.FraMovement.TabIndex = 42
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(18, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 0
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(540, 10)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.cmdPreview.TabIndex = 29
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(360, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdSavePrint.TabIndex = 26
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'OptSelection
        '
        '
        'frmTDSChallan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTDSChallan"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "TDS Challan"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.FraChallan.ResumeLayout(False)
        Me.FraChallan.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSelection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
        'SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdMain.DataSource = Nothing
		SprdView.DataSource = Nothing
	End Sub
#End Region 
End Class