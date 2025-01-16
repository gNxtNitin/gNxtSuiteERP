<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamSalesComp
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
        'Me.MDIParent = SnapSoftERP.Master
        'SnapSoftERP.Master.Show
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
	Public WithEvents _optBase_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optBase_1 As System.Windows.Forms.RadioButton
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents _optWise_3 As System.Windows.Forms.RadioButton
	Public WithEvents _optWise_2 As System.Windows.Forms.RadioButton
	Public WithEvents _optWise_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optWise_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents lstInvoiceType As System.Windows.Forms.CheckedListBox
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents cboTo As System.Windows.Forms.ComboBox
	Public WithEvents cboFrom As System.Windows.Forms.ComboBox
	Public WithEvents _Lbl_1 As System.Windows.Forms.Label
	Public WithEvents _Lbl_0 As System.Windows.Forms.Label
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents cboMonth As System.Windows.Forms.ComboBox
	Public WithEvents _Lbl_3 As System.Windows.Forms.Label
	Public WithEvents FraMonth As System.Windows.Forms.GroupBox
    Public WithEvents _optAmountType_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optAmountType_1 As System.Windows.Forms.RadioButton
	Public WithEvents fraYear As System.Windows.Forms.GroupBox
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents chkAll As System.Windows.Forms.CheckBox
	Public WithEvents cmdsearch As System.Windows.Forms.Button
	Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    'Public WithEvents AData1 As VB6.ADODC
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents cboRejection As System.Windows.Forms.ComboBox
	Public WithEvents cboExport As System.Windows.Forms.ComboBox
	Public WithEvents cboCancelled As System.Windows.Forms.ComboBox
	Public WithEvents cboFOC As System.Windows.Forms.ComboBox
	Public WithEvents cboAgtD3 As System.Windows.Forms.ComboBox
	Public WithEvents cboCT3 As System.Windows.Forms.ComboBox
	Public WithEvents cboLocation As System.Windows.Forms.ComboBox
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents lblReportType As System.Windows.Forms.Label
	Public WithEvents lblAcCode As System.Windows.Forms.Label
	Public WithEvents lblTrnType As System.Windows.Forms.Label
	Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents optAmountType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optBase As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optWise As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamSalesComp))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._optBase_0 = New System.Windows.Forms.RadioButton()
        Me._optBase_1 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optWise_3 = New System.Windows.Forms.RadioButton()
        Me._optWise_2 = New System.Windows.Forms.RadioButton()
        Me._optWise_1 = New System.Windows.Forms.RadioButton()
        Me._optWise_0 = New System.Windows.Forms.RadioButton()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lstInvoiceType = New System.Windows.Forms.CheckedListBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboTo = New System.Windows.Forms.ComboBox()
        Me.cboFrom = New System.Windows.Forms.ComboBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.FraMonth = New System.Windows.Forms.GroupBox()
        Me.cboMonth = New System.Windows.Forms.ComboBox()
        Me._Lbl_3 = New System.Windows.Forms.Label()
        Me.fraYear = New System.Windows.Forms.GroupBox()
        Me._optAmountType_0 = New System.Windows.Forms.RadioButton()
        Me._optAmountType_1 = New System.Windows.Forms.RadioButton()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cboRejection = New System.Windows.Forms.ComboBox()
        Me.cboExport = New System.Windows.Forms.ComboBox()
        Me.cboCancelled = New System.Windows.Forms.ComboBox()
        Me.cboFOC = New System.Windows.Forms.ComboBox()
        Me.cboAgtD3 = New System.Windows.Forms.ComboBox()
        Me.cboCT3 = New System.Windows.Forms.ComboBox()
        Me.cboLocation = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblReportType = New System.Windows.Forms.Label()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.lblTrnType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optAmountType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optBase = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optWise = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.Frame5.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.FraMonth.SuspendLayout()
        Me.fraYear.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraAccount.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optAmountType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(341, 21)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 1
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
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(47, 21)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(289, 20)
        Me.TxtAccount.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(123, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 6
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
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(63, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 5
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(184, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 7
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(4, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 4
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._optBase_0)
        Me.Frame5.Controls.Add(Me._optBase_1)
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(893, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(94, 51)
        Me.Frame5.TabIndex = 48
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Base"
        '
        '_optBase_0
        '
        Me._optBase_0.AutoSize = True
        Me._optBase_0.BackColor = System.Drawing.SystemColors.Control
        Me._optBase_0.Checked = True
        Me._optBase_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBase_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBase.SetIndex(Me._optBase_0, CType(0, Short))
        Me._optBase_0.Location = New System.Drawing.Point(11, 14)
        Me._optBase_0.Name = "_optBase_0"
        Me._optBase_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBase_0.Size = New System.Drawing.Size(61, 17)
        Me._optBase_0.TabIndex = 50
        Me._optBase_0.TabStop = True
        Me._optBase_0.Text = "Amount"
        Me._optBase_0.UseVisualStyleBackColor = False
        '
        '_optBase_1
        '
        Me._optBase_1.AutoSize = True
        Me._optBase_1.BackColor = System.Drawing.SystemColors.Control
        Me._optBase_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBase_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBase.SetIndex(Me._optBase_1, CType(1, Short))
        Me._optBase_1.Location = New System.Drawing.Point(11, 32)
        Me._optBase_1.Name = "_optBase_1"
        Me._optBase_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBase_1.Size = New System.Drawing.Size(41, 17)
        Me._optBase_1.TabIndex = 49
        Me._optBase_1.TabStop = True
        Me._optBase_1.Text = "Qty"
        Me._optBase_1.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optWise_3)
        Me.Frame1.Controls.Add(Me._optWise_2)
        Me.Frame1.Controls.Add(Me._optWise_1)
        Me.Frame1.Controls.Add(Me._optWise_0)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(989, -1)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(105, 102)
        Me.Frame1.TabIndex = 19
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Wise"
        '
        '_optWise_3
        '
        Me._optWise_3.AutoSize = True
        Me._optWise_3.BackColor = System.Drawing.SystemColors.Control
        Me._optWise_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optWise_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optWise.SetIndex(Me._optWise_3, CType(3, Short))
        Me._optWise_3.Location = New System.Drawing.Point(14, 77)
        Me._optWise_3.Name = "_optWise_3"
        Me._optWise_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optWise_3.Size = New System.Drawing.Size(67, 17)
        Me._optWise_3.TabIndex = 52
        Me._optWise_3.TabStop = True
        Me._optWise_3.Text = "Inv Type"
        Me._optWise_3.UseVisualStyleBackColor = False
        '
        '_optWise_2
        '
        Me._optWise_2.AutoSize = True
        Me._optWise_2.BackColor = System.Drawing.SystemColors.Control
        Me._optWise_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optWise_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optWise.SetIndex(Me._optWise_2, CType(2, Short))
        Me._optWise_2.Location = New System.Drawing.Point(14, 56)
        Me._optWise_2.Name = "_optWise_2"
        Me._optWise_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optWise_2.Size = New System.Drawing.Size(81, 17)
        Me._optWise_2.TabIndex = 51
        Me._optWise_2.TabStop = True
        Me._optWise_2.Text = "Party + Item"
        Me._optWise_2.UseVisualStyleBackColor = False
        '
        '_optWise_1
        '
        Me._optWise_1.AutoSize = True
        Me._optWise_1.BackColor = System.Drawing.SystemColors.Control
        Me._optWise_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optWise_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optWise.SetIndex(Me._optWise_1, CType(1, Short))
        Me._optWise_1.Location = New System.Drawing.Point(14, 35)
        Me._optWise_1.Name = "_optWise_1"
        Me._optWise_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optWise_1.Size = New System.Drawing.Size(45, 17)
        Me._optWise_1.TabIndex = 21
        Me._optWise_1.TabStop = True
        Me._optWise_1.Text = "Item"
        Me._optWise_1.UseVisualStyleBackColor = False
        '
        '_optWise_0
        '
        Me._optWise_0.AutoSize = True
        Me._optWise_0.BackColor = System.Drawing.SystemColors.Control
        Me._optWise_0.Checked = True
        Me._optWise_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optWise_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optWise.SetIndex(Me._optWise_0, CType(0, Short))
        Me._optWise_0.Location = New System.Drawing.Point(14, 14)
        Me._optWise_0.Name = "_optWise_0"
        Me._optWise_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optWise_0.Size = New System.Drawing.Size(49, 17)
        Me._optWise_0.TabIndex = 20
        Me._optWise_0.TabStop = True
        Me._optWise_0.Text = "Party"
        Me._optWise_0.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.lstInvoiceType)
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(263, 101)
        Me.Frame3.TabIndex = 30
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Invoice Type"
        '
        'lstInvoiceType
        '
        Me.lstInvoiceType.BackColor = System.Drawing.SystemColors.Window
        Me.lstInvoiceType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstInvoiceType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstInvoiceType.IntegralHeight = False
        Me.lstInvoiceType.Location = New System.Drawing.Point(2, 14)
        Me.lstInvoiceType.Name = "lstInvoiceType"
        Me.lstInvoiceType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstInvoiceType.Size = New System.Drawing.Size(257, 83)
        Me.lstInvoiceType.TabIndex = 31
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cboTo)
        Me.Frame6.Controls.Add(Me.cboFrom)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(452, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(231, 51)
        Me.Frame6.TabIndex = 8
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Year"
        '
        'cboTo
        '
        Me.cboTo.BackColor = System.Drawing.SystemColors.Window
        Me.cboTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTo.Location = New System.Drawing.Point(168, 16)
        Me.cboTo.Name = "cboTo"
        Me.cboTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTo.Size = New System.Drawing.Size(57, 21)
        Me.cboTo.TabIndex = 18
        '
        'cboFrom
        '
        Me.cboFrom.BackColor = System.Drawing.SystemColors.Window
        Me.cboFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFrom.Location = New System.Drawing.Point(42, 16)
        Me.cboFrom.Name = "cboFrom"
        Me.cboFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFrom.Size = New System.Drawing.Size(57, 21)
        Me.cboFrom.TabIndex = 17
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(142, 20)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(26, 13)
        Me._Lbl_1.TabIndex = 10
        Me._Lbl_1.Text = "To :"
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(6, 19)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(36, 13)
        Me._Lbl_0.TabIndex = 9
        Me._Lbl_0.Text = "From :"
        '
        'FraMonth
        '
        Me.FraMonth.BackColor = System.Drawing.SystemColors.Control
        Me.FraMonth.Controls.Add(Me.cboMonth)
        Me.FraMonth.Controls.Add(Me._Lbl_3)
        Me.FraMonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMonth.Location = New System.Drawing.Point(734, 0)
        Me.FraMonth.Name = "FraMonth"
        Me.FraMonth.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMonth.Size = New System.Drawing.Size(157, 51)
        Me.FraMonth.TabIndex = 27
        Me.FraMonth.TabStop = False
        Me.FraMonth.Text = "Month"
        '
        'cboMonth
        '
        Me.cboMonth.BackColor = System.Drawing.SystemColors.Window
        Me.cboMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMonth.Location = New System.Drawing.Point(50, 18)
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMonth.Size = New System.Drawing.Size(105, 21)
        Me.cboMonth.TabIndex = 28
        '
        '_Lbl_3
        '
        Me._Lbl_3.AutoSize = True
        Me._Lbl_3.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_3, CType(3, Short))
        Me._Lbl_3.Location = New System.Drawing.Point(6, 21)
        Me._Lbl_3.Name = "_Lbl_3"
        Me._Lbl_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_3.Size = New System.Drawing.Size(43, 13)
        Me._Lbl_3.TabIndex = 29
        Me._Lbl_3.Text = "Month :"
        '
        'fraYear
        '
        Me.fraYear.BackColor = System.Drawing.SystemColors.Control
        Me.fraYear.Controls.Add(Me._optAmountType_0)
        Me.fraYear.Controls.Add(Me._optAmountType_1)
        Me.fraYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraYear.Location = New System.Drawing.Point(893, 50)
        Me.fraYear.Name = "fraYear"
        Me.fraYear.Padding = New System.Windows.Forms.Padding(0)
        Me.fraYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraYear.Size = New System.Drawing.Size(94, 51)
        Me.fraYear.TabIndex = 22
        Me.fraYear.TabStop = False
        Me.fraYear.Text = "Amount"
        '
        '_optAmountType_0
        '
        Me._optAmountType_0.AutoSize = True
        Me._optAmountType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optAmountType_0.Checked = True
        Me._optAmountType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAmountType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAmountType.SetIndex(Me._optAmountType_0, CType(0, Short))
        Me._optAmountType_0.Location = New System.Drawing.Point(10, 14)
        Me._optAmountType_0.Name = "_optAmountType_0"
        Me._optAmountType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAmountType_0.Size = New System.Drawing.Size(80, 17)
        Me._optAmountType_0.TabIndex = 24
        Me._optAmountType_0.TabStop = True
        Me._optAmountType_0.Text = "Accessable"
        Me._optAmountType_0.UseVisualStyleBackColor = False
        '
        '_optAmountType_1
        '
        Me._optAmountType_1.AutoSize = True
        Me._optAmountType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optAmountType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAmountType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAmountType.SetIndex(Me._optAmountType_1, CType(1, Short))
        Me._optAmountType_1.Location = New System.Drawing.Point(10, 31)
        Me._optAmountType_1.Name = "_optAmountType_1"
        Me._optAmountType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAmountType_1.Size = New System.Drawing.Size(47, 17)
        Me._optAmountType_1.TabIndex = 23
        Me._optAmountType_1.TabStop = True
        Me._optAmountType_1.Text = "GST"
        Me._optAmountType_1.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(414, 428)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 49
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(450, 50)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(441, 51)
        Me.FraAccount.TabIndex = 11
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Account Name"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(378, 22)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(45, 17)
        Me.chkAll.TabIndex = 2
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 96)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1094, 470)
        Me.Frame4.TabIndex = 12
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1094, 457)
        Me.SprdMain.TabIndex = 3
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(797, 564)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(294, 53)
        Me.FraMovement.TabIndex = 13
        Me.FraMovement.TabStop = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cboRejection)
        Me.Frame2.Controls.Add(Me.cboExport)
        Me.Frame2.Controls.Add(Me.cboCancelled)
        Me.Frame2.Controls.Add(Me.cboFOC)
        Me.Frame2.Controls.Add(Me.cboAgtD3)
        Me.Frame2.Controls.Add(Me.cboCT3)
        Me.Frame2.Controls.Add(Me.cboLocation)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.Label8)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Controls.Add(Me.Label11)
        Me.Frame2.Controls.Add(Me.Label12)
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 564)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(495, 55)
        Me.Frame2.TabIndex = 33
        Me.Frame2.TabStop = False
        '
        'cboRejection
        '
        Me.cboRejection.BackColor = System.Drawing.SystemColors.Window
        Me.cboRejection.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRejection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRejection.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRejection.Location = New System.Drawing.Point(304, 30)
        Me.cboRejection.Name = "cboRejection"
        Me.cboRejection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRejection.Size = New System.Drawing.Size(65, 21)
        Me.cboRejection.TabIndex = 34
        '
        'cboExport
        '
        Me.cboExport.BackColor = System.Drawing.SystemColors.Window
        Me.cboExport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboExport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExport.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboExport.Location = New System.Drawing.Point(182, 30)
        Me.cboExport.Name = "cboExport"
        Me.cboExport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboExport.Size = New System.Drawing.Size(57, 21)
        Me.cboExport.TabIndex = 36
        '
        'cboCancelled
        '
        Me.cboCancelled.BackColor = System.Drawing.SystemColors.Window
        Me.cboCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCancelled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCancelled.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCancelled.Location = New System.Drawing.Point(182, 10)
        Me.cboCancelled.Name = "cboCancelled"
        Me.cboCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCancelled.Size = New System.Drawing.Size(57, 21)
        Me.cboCancelled.TabIndex = 40
        '
        'cboFOC
        '
        Me.cboFOC.BackColor = System.Drawing.SystemColors.Window
        Me.cboFOC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFOC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFOC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFOC.Location = New System.Drawing.Point(54, 30)
        Me.cboFOC.Name = "cboFOC"
        Me.cboFOC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFOC.Size = New System.Drawing.Size(57, 21)
        Me.cboFOC.TabIndex = 39
        '
        'cboAgtD3
        '
        Me.cboAgtD3.BackColor = System.Drawing.SystemColors.Window
        Me.cboAgtD3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAgtD3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAgtD3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAgtD3.Location = New System.Drawing.Point(54, 8)
        Me.cboAgtD3.Name = "cboAgtD3"
        Me.cboAgtD3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAgtD3.Size = New System.Drawing.Size(57, 21)
        Me.cboAgtD3.TabIndex = 38
        '
        'cboCT3
        '
        Me.cboCT3.BackColor = System.Drawing.SystemColors.Window
        Me.cboCT3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCT3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCT3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCT3.Location = New System.Drawing.Point(430, 10)
        Me.cboCT3.Name = "cboCT3"
        Me.cboCT3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCT3.Size = New System.Drawing.Size(59, 21)
        Me.cboCT3.TabIndex = 37
        '
        'cboLocation
        '
        Me.cboLocation.BackColor = System.Drawing.SystemColors.Window
        Me.cboLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLocation.Location = New System.Drawing.Point(304, 10)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLocation.Size = New System.Drawing.Size(65, 21)
        Me.cboLocation.TabIndex = 35
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(117, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Cancelled :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(241, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(58, 13)
        Me.Label6.TabIndex = 46
        Me.Label6.Text = "Rejection :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(21, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "FOC :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(2, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "Agt. D3 :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(374, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Agt CT3 :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(114, 32)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(61, 13)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Export Inv :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(246, 12)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "Location :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblReportType
        '
        Me.lblReportType.BackColor = System.Drawing.SystemColors.Control
        Me.lblReportType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblReportType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblReportType.Location = New System.Drawing.Point(703, 589)
        Me.lblReportType.Name = "lblReportType"
        Me.lblReportType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblReportType.Size = New System.Drawing.Size(37, 23)
        Me.lblReportType.TabIndex = 26
        Me.lblReportType.Text = "lblReportType"
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(599, 587)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(87, 13)
        Me.lblAcCode.TabIndex = 15
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'lblTrnType
        '
        Me.lblTrnType.AutoSize = True
        Me.lblTrnType.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrnType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTrnType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrnType.Location = New System.Drawing.Point(521, 591)
        Me.lblTrnType.Name = "lblTrnType"
        Me.lblTrnType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnType.Size = New System.Drawing.Size(57, 13)
        Me.lblTrnType.TabIndex = 14
        Me.lblTrnType.Text = "lblTrnType"
        Me.lblTrnType.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(267, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(180, 100)
        Me.GroupBox1.TabIndex = 77
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Company Name"
        '
        'lstCompanyName
        '
        Me.lstCompanyName.BackColor = System.Drawing.SystemColors.Window
        Me.lstCompanyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCompanyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstCompanyName.IntegralHeight = False
        Me.lstCompanyName.Location = New System.Drawing.Point(0, 13)
        Me.lstCompanyName.Name = "lstCompanyName"
        Me.lstCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstCompanyName.Size = New System.Drawing.Size(180, 87)
        Me.lstCompanyName.TabIndex = 2
        '
        'frmParamSalesComp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1096, 621)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.FraMonth)
        Me.Controls.Add(Me.fraYear)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.lblReportType)
        Me.Controls.Add(Me.lblAcCode)
        Me.Controls.Add(Me.lblTrnType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamSalesComp"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Sales Comparison Chat"
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.FraMonth.ResumeLayout(False)
        Me.FraMonth.PerformLayout()
        Me.fraYear.ResumeLayout(False)
        Me.fraYear.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optAmountType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        SprdMain.DataSource = Nothing ' CType(AData1, MSDATASRC.DataSource)
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdMain.DataSource = Nothing
	End Sub

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
#End Region
End Class