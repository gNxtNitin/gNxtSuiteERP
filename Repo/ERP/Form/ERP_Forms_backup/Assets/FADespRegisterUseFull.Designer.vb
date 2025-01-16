Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmFADespRegisterUseFull
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
        'Me.MDIParent = MIS.Master
        'MIS.Master.Show
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
	Public WithEvents _optDate_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optDate_0 As System.Windows.Forms.RadioButton
	Public WithEvents chkOpening As System.Windows.Forms.CheckBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents _Lbl_1 As System.Windows.Forms.Label
	Public WithEvents _Lbl_0 As System.Windows.Forms.Label
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents cboCompany As System.Windows.Forms.ComboBox
	Public WithEvents Frame7 As System.Windows.Forms.GroupBox
	Public WithEvents txtDeprMode As System.Windows.Forms.TextBox
	Public WithEvents cmdsearchDepr As System.Windows.Forms.Button
	Public WithEvents chkAllDepr As System.Windows.Forms.CheckBox
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents _optType_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optType_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optType_2 As System.Windows.Forms.RadioButton
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents txtDepreciationDate As System.Windows.Forms.MaskedTextBox
	Public WithEvents Frame8 As System.Windows.Forms.GroupBox
	Public WithEvents _optOption_2 As System.Windows.Forms.RadioButton
	Public WithEvents _optOption_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optOption_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents txtRefNo As System.Windows.Forms.TextBox
	Public WithEvents cmdRefNo As System.Windows.Forms.Button
	Public WithEvents chkRefNo As System.Windows.Forms.CheckBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Frame9 As System.Windows.Forms.GroupBox
	Public WithEvents chkAll As System.Windows.Forms.CheckBox
	Public WithEvents cmdsearch As System.Windows.Forms.Button
	Public WithEvents TxtAccount As System.Windows.Forms.TextBox
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents lblCount As System.Windows.Forms.Label
	Public WithEvents lblFYEndDate As System.Windows.Forms.Label
	Public WithEvents lblFYStartDate As System.Windows.Forms.Label
	Public WithEvents lblCurrentFyear As System.Windows.Forms.Label
	Public WithEvents lblAcCode As System.Windows.Forms.Label
	Public WithEvents lblTrnType As System.Windows.Forms.Label
	Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents optDate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optOption As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFADespRegisterUseFull))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtDeprMode = New System.Windows.Forms.TextBox()
        Me.cmdsearchDepr = New System.Windows.Forms.Button()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.cmdRefNo = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me._optDate_1 = New System.Windows.Forms.RadioButton()
        Me._optDate_0 = New System.Windows.Forms.RadioButton()
        Me.chkOpening = New System.Windows.Forms.CheckBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboCompany = New System.Windows.Forms.ComboBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkAllDepr = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optType_0 = New System.Windows.Forms.RadioButton()
        Me._optType_1 = New System.Windows.Forms.RadioButton()
        Me._optType_2 = New System.Windows.Forms.RadioButton()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.txtDepreciationDate = New System.Windows.Forms.MaskedTextBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optOption_2 = New System.Windows.Forms.RadioButton()
        Me._optOption_1 = New System.Windows.Forms.RadioButton()
        Me._optOption_0 = New System.Windows.Forms.RadioButton()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.chkRefNo = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me.lblCount = New System.Windows.Forms.Label()
        Me.lblFYEndDate = New System.Windows.Forms.Label()
        Me.lblFYStartDate = New System.Windows.Forms.Label()
        Me.lblCurrentFyear = New System.Windows.Forms.Label()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.lblTrnType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optDate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optOption = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame6.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDeprMode
        '
        Me.txtDeprMode.AcceptsReturn = True
        Me.txtDeprMode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeprMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeprMode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeprMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeprMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeprMode.Location = New System.Drawing.Point(136, 10)
        Me.txtDeprMode.MaxLength = 0
        Me.txtDeprMode.Name = "txtDeprMode"
        Me.txtDeprMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeprMode.Size = New System.Drawing.Size(99, 20)
        Me.txtDeprMode.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.txtDeprMode, "Press F1 For Help")
        '
        'cmdsearchDepr
        '
        Me.cmdsearchDepr.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchDepr.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchDepr.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchDepr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchDepr.Image = CType(resources.GetObject("cmdsearchDepr.Image"), System.Drawing.Image)
        Me.cmdsearchDepr.Location = New System.Drawing.Point(236, 10)
        Me.cmdsearchDepr.Name = "cmdsearchDepr"
        Me.cmdsearchDepr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchDepr.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearchDepr.TabIndex = 21
        Me.cmdsearchDepr.TabStop = False
        Me.cmdsearchDepr.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchDepr, "Search")
        Me.cmdsearchDepr.UseVisualStyleBackColor = False
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefNo.Location = New System.Drawing.Point(54, 12)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(63, 20)
        Me.txtRefNo.TabIndex = 45
        Me.ToolTip1.SetToolTip(Me.txtRefNo, "Press F1 For Help")
        '
        'cmdRefNo
        '
        Me.cmdRefNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdRefNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefNo.Image = CType(resources.GetObject("cmdRefNo.Image"), System.Drawing.Image)
        Me.cmdRefNo.Location = New System.Drawing.Point(118, 12)
        Me.cmdRefNo.Name = "cmdRefNo"
        Me.cmdRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefNo.Size = New System.Drawing.Size(29, 19)
        Me.cmdRefNo.TabIndex = 44
        Me.cmdRefNo.TabStop = False
        Me.cmdRefNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdRefNo, "Search")
        Me.cmdRefNo.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(320, 10)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(96, 10)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(223, 20)
        Me.TxtAccount.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(123, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 8
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
        Me.cmdPrint.Location = New System.Drawing.Point(63, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 7
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
        Me.cmdClose.Location = New System.Drawing.Point(184, 9)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 9
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
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 6
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me._optDate_1)
        Me.Frame6.Controls.Add(Me._optDate_0)
        Me.Frame6.Controls.Add(Me.chkOpening)
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(-2, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(443, 43)
        Me.Frame6.TabIndex = 10
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        '_optDate_1
        '
        Me._optDate_1.AutoSize = True
        Me._optDate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_1, CType(1, Short))
        Me._optDate_1.Location = New System.Drawing.Point(6, 26)
        Me._optDate_1.Name = "_optDate_1"
        Me._optDate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_1.Size = New System.Drawing.Size(60, 18)
        Me._optDate_1.TabIndex = 50
        Me._optDate_1.TabStop = True
        Me._optDate_1.Text = "V Date"
        Me._optDate_1.UseVisualStyleBackColor = False
        '
        '_optDate_0
        '
        Me._optDate_0.AutoSize = True
        Me._optDate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_0.Checked = True
        Me._optDate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_0, CType(0, Short))
        Me._optDate_0.Location = New System.Drawing.Point(6, 12)
        Me._optDate_0.Name = "_optDate_0"
        Me._optDate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_0.Size = New System.Drawing.Size(81, 18)
        Me._optDate_0.TabIndex = 49
        Me._optDate_0.TabStop = True
        Me._optDate_0.Text = "Put to Use"
        Me._optDate_0.UseVisualStyleBackColor = False
        '
        'chkOpening
        '
        Me.chkOpening.BackColor = System.Drawing.SystemColors.Control
        Me.chkOpening.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOpening.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOpening.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOpening.Location = New System.Drawing.Point(356, 10)
        Me.chkOpening.Name = "chkOpening"
        Me.chkOpening.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOpening.Size = New System.Drawing.Size(72, 32)
        Me.chkOpening.TabIndex = 47
        Me.chkOpening.Text = "Opening Include"
        Me.chkOpening.UseVisualStyleBackColor = False
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(156, 14)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(83, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(266, 14)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(83, 20)
        Me.txtDateTo.TabIndex = 1
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(242, 18)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(26, 14)
        Me._Lbl_1.TabIndex = 12
        Me._Lbl_1.Text = "To :"
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(118, 17)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(42, 14)
        Me._Lbl_0.TabIndex = 11
        Me._Lbl_0.Text = "From :"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboCompany)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(444, 0)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(314, 39)
        Me.Frame7.TabIndex = 31
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Company Name"
        '
        'cboCompany
        '
        Me.cboCompany.BackColor = System.Drawing.SystemColors.Window
        Me.cboCompany.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCompany.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCompany.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCompany.Location = New System.Drawing.Point(4, 12)
        Me.cboCompany.Name = "cboCompany"
        Me.cboCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCompany.Size = New System.Drawing.Size(297, 22)
        Me.cboCompany.TabIndex = 32
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtDeprMode)
        Me.Frame2.Controls.Add(Me.cmdsearchDepr)
        Me.Frame2.Controls.Add(Me.chkAllDepr)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(434, 42)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(322, 35)
        Me.Frame2.TabIndex = 19
        Me.Frame2.TabStop = False
        '
        'chkAllDepr
        '
        Me.chkAllDepr.AutoSize = True
        Me.chkAllDepr.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDepr.Checked = True
        Me.chkAllDepr.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllDepr.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDepr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDepr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDepr.Location = New System.Drawing.Point(266, 14)
        Me.chkAllDepr.Name = "chkAllDepr"
        Me.chkAllDepr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDepr.Size = New System.Drawing.Size(48, 18)
        Me.chkAllDepr.TabIndex = 20
        Me.chkAllDepr.Text = "ALL"
        Me.chkAllDepr.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(5, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(130, 14)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Mode of Depreciation :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optType_0)
        Me.Frame1.Controls.Add(Me._optType_1)
        Me.Frame1.Controls.Add(Me._optType_2)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(564, 36)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(185, 35)
        Me.Frame1.TabIndex = 24
        Me.Frame1.TabStop = False
        Me.Frame1.Visible = False
        '
        '_optType_0
        '
        Me._optType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optType_0.Checked = True
        Me._optType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_0, CType(0, Short))
        Me._optType_0.Location = New System.Drawing.Point(6, 14)
        Me._optType_0.Name = "_optType_0"
        Me._optType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_0.Size = New System.Drawing.Size(57, 13)
        Me._optType_0.TabIndex = 27
        Me._optType_0.TabStop = True
        Me._optType_0.Text = "Local"
        Me._optType_0.UseVisualStyleBackColor = False
        '
        '_optType_1
        '
        Me._optType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_1, CType(1, Short))
        Me._optType_1.Location = New System.Drawing.Point(64, 14)
        Me._optType_1.Name = "_optType_1"
        Me._optType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_1.Size = New System.Drawing.Size(65, 13)
        Me._optType_1.TabIndex = 26
        Me._optType_1.TabStop = True
        Me._optType_1.Text = "Central"
        Me._optType_1.UseVisualStyleBackColor = False
        '
        '_optType_2
        '
        Me._optType_2.BackColor = System.Drawing.SystemColors.Control
        Me._optType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_2, CType(2, Short))
        Me._optType_2.Location = New System.Drawing.Point(130, 14)
        Me._optType_2.Name = "_optType_2"
        Me._optType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_2.Size = New System.Drawing.Size(53, 13)
        Me._optType_2.TabIndex = 25
        Me._optType_2.TabStop = True
        Me._optType_2.Text = "Both"
        Me._optType_2.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.txtDepreciationDate)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 42)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(217, 37)
        Me.Frame8.TabIndex = 36
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Depreciation As on Date"
        '
        'txtDepreciationDate
        '
        Me.txtDepreciationDate.AllowPromptAsInput = False
        Me.txtDepreciationDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepreciationDate.Location = New System.Drawing.Point(130, 10)
        Me.txtDepreciationDate.Mask = "##/##/####"
        Me.txtDepreciationDate.Name = "txtDepreciationDate"
        Me.txtDepreciationDate.Size = New System.Drawing.Size(83, 20)
        Me.txtDepreciationDate.TabIndex = 37
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optOption_2)
        Me.Frame3.Controls.Add(Me._optOption_1)
        Me.Frame3.Controls.Add(Me._optOption_0)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(398, 74)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(187, 35)
        Me.Frame3.TabIndex = 33
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Show (Assets)"
        '
        '_optOption_2
        '
        Me._optOption_2.AutoSize = True
        Me._optOption_2.BackColor = System.Drawing.SystemColors.Control
        Me._optOption_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOption_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOption_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOption.SetIndex(Me._optOption_2, CType(2, Short))
        Me._optOption_2.Location = New System.Drawing.Point(128, 14)
        Me._optOption_2.Name = "_optOption_2"
        Me._optOption_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOption_2.Size = New System.Drawing.Size(55, 18)
        Me._optOption_2.TabIndex = 41
        Me._optOption_2.TabStop = True
        Me._optOption_2.Text = "Sales"
        Me._optOption_2.UseVisualStyleBackColor = False
        '
        '_optOption_1
        '
        Me._optOption_1.AutoSize = True
        Me._optOption_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOption_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOption_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOption_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOption.SetIndex(Me._optOption_1, CType(1, Short))
        Me._optOption_1.Location = New System.Drawing.Point(54, 14)
        Me._optOption_1.Name = "_optOption_1"
        Me._optOption_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOption_1.Size = New System.Drawing.Size(68, 18)
        Me._optOption_1.TabIndex = 35
        Me._optOption_1.TabStop = True
        Me._optOption_1.Text = "Current"
        Me._optOption_1.UseVisualStyleBackColor = False
        '
        '_optOption_0
        '
        Me._optOption_0.AutoSize = True
        Me._optOption_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOption_0.Checked = True
        Me._optOption_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOption_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOption_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOption.SetIndex(Me._optOption_0, CType(0, Short))
        Me._optOption_0.Location = New System.Drawing.Point(4, 14)
        Me._optOption_0.Name = "_optOption_0"
        Me._optOption_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOption_0.Size = New System.Drawing.Size(39, 18)
        Me._optOption_0.TabIndex = 34
        Me._optOption_0.TabStop = True
        Me._optOption_0.Text = "All"
        Me._optOption_0.UseVisualStyleBackColor = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.txtRefNo)
        Me.Frame9.Controls.Add(Me.cmdRefNo)
        Me.Frame9.Controls.Add(Me.chkRefNo)
        Me.Frame9.Controls.Add(Me.Label4)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(218, 42)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(215, 37)
        Me.Frame9.TabIndex = 42
        Me.Frame9.TabStop = False
        '
        'chkRefNo
        '
        Me.chkRefNo.AutoSize = True
        Me.chkRefNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkRefNo.Checked = True
        Me.chkRefNo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRefNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRefNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRefNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefNo.Location = New System.Drawing.Point(148, 16)
        Me.chkRefNo.Name = "chkRefNo"
        Me.chkRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRefNo.Size = New System.Drawing.Size(48, 18)
        Me.chkRefNo.TabIndex = 43
        Me.chkRefNo.Text = "ALL"
        Me.chkRefNo.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(5, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(48, 14)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "Ref No :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, 74)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(397, 35)
        Me.FraAccount.TabIndex = 13
        Me.FraAccount.TabStop = False
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(350, 14)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(48, 18)
        Me.chkAll.TabIndex = 4
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(2, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(94, 17)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Account Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 104)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(899, 460)
        Me.Frame4.TabIndex = 14
        Me.Frame4.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 1
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(899, 447)
        Me.SprdMain.TabIndex = 5
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(502, 561)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 49)
        Me.FraMovement.TabIndex = 15
        Me.FraMovement.TabStop = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._optShow_1)
        Me.Frame5.Controls.Add(Me._optShow_0)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 565)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(163, 45)
        Me.Frame5.TabIndex = 28
        Me.Frame5.TabStop = False
        '
        '_optShow_1
        '
        Me._optShow_1.AutoSize = True
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(82, 12)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(78, 18)
        Me._optShow_1.TabIndex = 30
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "Summary"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        '_optShow_0
        '
        Me._optShow_0.AutoSize = True
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Checked = True
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(14, 12)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(55, 18)
        Me._optShow_0.TabIndex = 29
        Me._optShow_0.TabStop = True
        Me._optShow_0.Text = "Detail"
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        'lblCount
        '
        Me.lblCount.BackColor = System.Drawing.SystemColors.Control
        Me.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCount.Location = New System.Drawing.Point(362, 573)
        Me.lblCount.Name = "lblCount"
        Me.lblCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCount.Size = New System.Drawing.Size(123, 31)
        Me.lblCount.TabIndex = 48
        Me.lblCount.Text = "Label3"
        '
        'lblFYEndDate
        '
        Me.lblFYEndDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblFYEndDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFYEndDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFYEndDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFYEndDate.Location = New System.Drawing.Point(384, 58)
        Me.lblFYEndDate.Name = "lblFYEndDate"
        Me.lblFYEndDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFYEndDate.Size = New System.Drawing.Size(103, 13)
        Me.lblFYEndDate.TabIndex = 40
        Me.lblFYEndDate.Text = "lblFYEndDate"
        Me.lblFYEndDate.Visible = False
        '
        'lblFYStartDate
        '
        Me.lblFYStartDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblFYStartDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFYStartDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFYStartDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFYStartDate.Location = New System.Drawing.Point(384, 40)
        Me.lblFYStartDate.Name = "lblFYStartDate"
        Me.lblFYStartDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFYStartDate.Size = New System.Drawing.Size(81, 15)
        Me.lblFYStartDate.TabIndex = 39
        Me.lblFYStartDate.Text = "lblFYStartDate"
        Me.lblFYStartDate.Visible = False
        '
        'lblCurrentFyear
        '
        Me.lblCurrentFyear.AutoSize = True
        Me.lblCurrentFyear.BackColor = System.Drawing.SystemColors.Control
        Me.lblCurrentFyear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCurrentFyear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentFyear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCurrentFyear.Location = New System.Drawing.Point(384, 48)
        Me.lblCurrentFyear.Name = "lblCurrentFyear"
        Me.lblCurrentFyear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCurrentFyear.Size = New System.Drawing.Size(81, 14)
        Me.lblCurrentFyear.TabIndex = 38
        Me.lblCurrentFyear.Text = "lblCurrentFyear"
        Me.lblCurrentFyear.Visible = False
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(250, 593)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(87, 13)
        Me.lblAcCode.TabIndex = 17
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'lblTrnType
        '
        Me.lblTrnType.AutoSize = True
        Me.lblTrnType.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrnType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTrnType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrnType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrnType.Location = New System.Drawing.Point(172, 595)
        Me.lblTrnType.Name = "lblTrnType"
        Me.lblTrnType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnType.Size = New System.Drawing.Size(56, 14)
        Me.lblTrnType.TabIndex = 16
        Me.lblTrnType.Text = "lblTrnType"
        Me.lblTrnType.Visible = False
        '
        'optType
        '
        '
        'frmFADespRegisterUseFull
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame9)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.lblCount)
        Me.Controls.Add(Me.lblFYEndDate)
        Me.Controls.Add(Me.lblFYStartDate)
        Me.Controls.Add(Me.lblCurrentFyear)
        Me.Controls.Add(Me.lblAcCode)
        Me.Controls.Add(Me.lblTrnType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFADespRegisterUseFull"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Fixed Assets Depreciation Register - Use Full"
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        SprdMain.DataSource = Nothing
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdMain.DataSource = Nothing
	End Sub
#End Region 
End Class