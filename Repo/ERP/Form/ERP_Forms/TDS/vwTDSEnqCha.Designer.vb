Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewTDSEnqCha
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
	Public WithEvents _optOrderBy_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optOrderBy_1 As System.Windows.Forms.RadioButton
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents cmdsearch As System.Windows.Forms.Button
	Public WithEvents TxtAccount As System.Windows.Forms.TextBox
	Public WithEvents FraAccount As System.Windows.Forms.GroupBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateFrom1 As System.Windows.Forms.TextBox
	Public WithEvents txtDateTo2 As System.Windows.Forms.TextBox
	Public WithEvents _Lbl_1 As System.Windows.Forms.Label
	Public WithEvents _Lbl_0 As System.Windows.Forms.Label
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents _optChallanAmt_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optChallanAmt_0 As System.Windows.Forms.RadioButton
	Public WithEvents txtChallanAmt As System.Windows.Forms.TextBox
	Public WithEvents Frame7 As System.Windows.Forms.GroupBox
	Public WithEvents txtChallan As System.Windows.Forms.TextBox
	Public WithEvents _optChallan_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optChallan_1 As System.Windows.Forms.RadioButton
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents cmdSearchP As System.Windows.Forms.Button
	Public WithEvents _optPartyName_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optPartyName_0 As System.Windows.Forms.RadioButton
	Public WithEvents txtPartyName As System.Windows.Forms.TextBox
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdLedg As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents lblAcCode As System.Windows.Forms.Label
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents SprdPreview As AxFPSpreadADO.AxfpSpreadPreview
	Public WithEvents SprdCommand As AxFPSpreadADO.AxfpSpread
	Public WithEvents FraPreview As System.Windows.Forms.GroupBox
	Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
	Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
	Public CommonDialog1Font As System.Windows.Forms.FontDialog
	Public CommonDialog1Color As System.Windows.Forms.ColorDialog
	Public CommonDialog1Print As System.Windows.Forms.PrintDialog
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents optChallan As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optChallanAmt As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optPartyName As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewTDSEnqCha))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.txtChallanAmt = New System.Windows.Forms.TextBox()
        Me.txtChallan = New System.Windows.Forms.TextBox()
        Me.cmdSearchP = New System.Windows.Forms.Button()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateFrom1 = New System.Windows.Forms.TextBox()
        Me.txtDateTo2 = New System.Windows.Forms.TextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._optChallanAmt_1 = New System.Windows.Forms.RadioButton()
        Me._optChallanAmt_0 = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optChallan_0 = New System.Windows.Forms.RadioButton()
        Me._optChallan_1 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optPartyName_1 = New System.Windows.Forms.RadioButton()
        Me._optPartyName_0 = New System.Windows.Forms.RadioButton()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdLedg = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.FraPreview = New System.Windows.Forms.GroupBox()
        Me.SprdPreview = New AxFPSpreadADO.AxfpSpreadPreview()
        Me.SprdCommand = New AxFPSpreadADO.AxfpSpread()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optChallan = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optChallanAmt = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optPartyName = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame3.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdLedg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraPreview.SuspendLayout()
        CType(Me.SprdPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdCommand, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optChallan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optChallanAmt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPartyName, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(482, 16)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(23, 19)
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
        Me.TxtAccount.Location = New System.Drawing.Point(4, 16)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(477, 20)
        Me.TxtAccount.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'txtChallanAmt
        '
        Me.txtChallanAmt.AcceptsReturn = True
        Me.txtChallanAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanAmt.Enabled = False
        Me.txtChallanAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChallanAmt.Location = New System.Drawing.Point(4, 32)
        Me.txtChallanAmt.MaxLength = 0
        Me.txtChallanAmt.Name = "txtChallanAmt"
        Me.txtChallanAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanAmt.Size = New System.Drawing.Size(169, 20)
        Me.txtChallanAmt.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtChallanAmt, "Press F1 For Help")
        '
        'txtChallan
        '
        Me.txtChallan.AcceptsReturn = True
        Me.txtChallan.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallan.Enabled = False
        Me.txtChallan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallan.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChallan.Location = New System.Drawing.Point(4, 32)
        Me.txtChallan.MaxLength = 0
        Me.txtChallan.Name = "txtChallan"
        Me.txtChallan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallan.Size = New System.Drawing.Size(165, 20)
        Me.txtChallan.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtChallan, "Press F1 For Help")
        '
        'cmdSearchP
        '
        Me.cmdSearchP.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchP.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchP.Image = CType(resources.GetObject("cmdSearchP.Image"), System.Drawing.Image)
        Me.cmdSearchP.Location = New System.Drawing.Point(370, 32)
        Me.cmdSearchP.Name = "cmdSearchP"
        Me.cmdSearchP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchP.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchP.TabIndex = 7
        Me.cmdSearchP.TabStop = False
        Me.cmdSearchP.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchP, "Search")
        Me.cmdSearchP.UseVisualStyleBackColor = False
        '
        'txtPartyName
        '
        Me.txtPartyName.AcceptsReturn = True
        Me.txtPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyName.Enabled = False
        Me.txtPartyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartyName.Location = New System.Drawing.Point(6, 32)
        Me.txtPartyName.MaxLength = 0
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyName.Size = New System.Drawing.Size(363, 20)
        Me.txtPartyName.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtPartyName, "Press F1 For Help")
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(4, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 15
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(184, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 18
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(65, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 16
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(125, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 17
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optOrderBy_0)
        Me.Frame3.Controls.Add(Me._optOrderBy_1)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 410)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(281, 47)
        Me.Frame3.TabIndex = 34
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Order By"
        '
        '_optOrderBy_0
        '
        Me._optOrderBy_0.AutoSize = True
        Me._optOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_0.Checked = True
        Me._optOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_0, CType(0, Short))
        Me._optOrderBy_0.Location = New System.Drawing.Point(10, 18)
        Me._optOrderBy_0.Name = "_optOrderBy_0"
        Me._optOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_0.Size = New System.Drawing.Size(87, 18)
        Me._optOrderBy_0.TabIndex = 36
        Me._optOrderBy_0.TabStop = True
        Me._optOrderBy_0.Text = "Party Name"
        Me._optOrderBy_0.UseVisualStyleBackColor = False
        '
        '_optOrderBy_1
        '
        Me._optOrderBy_1.AutoSize = True
        Me._optOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_1, CType(1, Short))
        Me._optOrderBy_1.Location = New System.Drawing.Point(164, 18)
        Me._optOrderBy_1.Name = "_optOrderBy_1"
        Me._optOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_1.Size = New System.Drawing.Size(82, 18)
        Me._optOrderBy_1.TabIndex = 35
        Me._optOrderBy_1.TabStop = True
        Me._optOrderBy_1.Text = "Challan No"
        Me._optOrderBy_1.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(242, -2)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(509, 43)
        Me.FraAccount.TabIndex = 26
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Account"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me.txtDateFrom1)
        Me.Frame6.Controls.Add(Me.txtDateTo2)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, -2)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(241, 43)
        Me.Frame6.TabIndex = 21
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(44, 14)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(79, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(158, 14)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(79, 20)
        Me.txtDateTo.TabIndex = 1
        '
        'txtDateFrom1
        '
        Me.txtDateFrom1.AcceptsReturn = True
        Me.txtDateFrom1.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateFrom1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateFrom1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateFrom1.Enabled = False
        Me.txtDateFrom1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDateFrom1.Location = New System.Drawing.Point(44, 16)
        Me.txtDateFrom1.MaxLength = 0
        Me.txtDateFrom1.Name = "txtDateFrom1"
        Me.txtDateFrom1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateFrom1.Size = New System.Drawing.Size(79, 20)
        Me.txtDateFrom1.TabIndex = 23
        Me.txtDateFrom1.Visible = False
        '
        'txtDateTo2
        '
        Me.txtDateTo2.AcceptsReturn = True
        Me.txtDateTo2.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateTo2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateTo2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateTo2.Enabled = False
        Me.txtDateTo2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDateTo2.Location = New System.Drawing.Point(158, 16)
        Me.txtDateTo2.MaxLength = 0
        Me.txtDateTo2.Name = "txtDateTo2"
        Me.txtDateTo2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateTo2.Size = New System.Drawing.Size(79, 20)
        Me.txtDateTo2.TabIndex = 22
        Me.txtDateTo2.Visible = False
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(128, 17)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(26, 14)
        Me._Lbl_1.TabIndex = 25
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
        Me._Lbl_0.Location = New System.Drawing.Point(6, 17)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(42, 14)
        Me._Lbl_0.TabIndex = 24
        Me._Lbl_0.Text = "From :"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me._optChallanAmt_1)
        Me.Frame7.Controls.Add(Me._optChallanAmt_0)
        Me.Frame7.Controls.Add(Me.txtChallanAmt)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(574, 40)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(177, 55)
        Me.Frame7.TabIndex = 32
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Challan Amount"
        '
        '_optChallanAmt_1
        '
        Me._optChallanAmt_1.AutoSize = True
        Me._optChallanAmt_1.BackColor = System.Drawing.SystemColors.Control
        Me._optChallanAmt_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optChallanAmt_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optChallanAmt_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optChallanAmt.SetIndex(Me._optChallanAmt_1, CType(1, Short))
        Me._optChallanAmt_1.Location = New System.Drawing.Point(72, 12)
        Me._optChallanAmt_1.Name = "_optChallanAmt_1"
        Me._optChallanAmt_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optChallanAmt_1.Size = New System.Drawing.Size(84, 18)
        Me._optChallanAmt_1.TabIndex = 12
        Me._optChallanAmt_1.TabStop = True
        Me._optChallanAmt_1.Text = "Particulars"
        Me._optChallanAmt_1.UseVisualStyleBackColor = False
        '
        '_optChallanAmt_0
        '
        Me._optChallanAmt_0.AutoSize = True
        Me._optChallanAmt_0.BackColor = System.Drawing.SystemColors.Control
        Me._optChallanAmt_0.Checked = True
        Me._optChallanAmt_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optChallanAmt_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optChallanAmt_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optChallanAmt.SetIndex(Me._optChallanAmt_0, CType(0, Short))
        Me._optChallanAmt_0.Location = New System.Drawing.Point(12, 12)
        Me._optChallanAmt_0.Name = "_optChallanAmt_0"
        Me._optChallanAmt_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optChallanAmt_0.Size = New System.Drawing.Size(39, 18)
        Me._optChallanAmt_0.TabIndex = 11
        Me._optChallanAmt_0.TabStop = True
        Me._optChallanAmt_0.Text = "All"
        Me._optChallanAmt_0.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtChallan)
        Me.Frame2.Controls.Add(Me._optChallan_0)
        Me.Frame2.Controls.Add(Me._optChallan_1)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(400, 40)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(173, 55)
        Me.Frame2.TabIndex = 31
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Challan No"
        '
        '_optChallan_0
        '
        Me._optChallan_0.AutoSize = True
        Me._optChallan_0.BackColor = System.Drawing.SystemColors.Control
        Me._optChallan_0.Checked = True
        Me._optChallan_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optChallan_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optChallan_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optChallan.SetIndex(Me._optChallan_0, CType(0, Short))
        Me._optChallan_0.Location = New System.Drawing.Point(12, 12)
        Me._optChallan_0.Name = "_optChallan_0"
        Me._optChallan_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optChallan_0.Size = New System.Drawing.Size(39, 18)
        Me._optChallan_0.TabIndex = 8
        Me._optChallan_0.TabStop = True
        Me._optChallan_0.Text = "All"
        Me._optChallan_0.UseVisualStyleBackColor = False
        '
        '_optChallan_1
        '
        Me._optChallan_1.AutoSize = True
        Me._optChallan_1.BackColor = System.Drawing.SystemColors.Control
        Me._optChallan_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optChallan_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optChallan_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optChallan.SetIndex(Me._optChallan_1, CType(1, Short))
        Me._optChallan_1.Location = New System.Drawing.Point(72, 12)
        Me._optChallan_1.Name = "_optChallan_1"
        Me._optChallan_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optChallan_1.Size = New System.Drawing.Size(84, 18)
        Me._optChallan_1.TabIndex = 9
        Me._optChallan_1.TabStop = True
        Me._optChallan_1.Text = "Particulars"
        Me._optChallan_1.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdSearchP)
        Me.Frame1.Controls.Add(Me._optPartyName_1)
        Me.Frame1.Controls.Add(Me._optPartyName_0)
        Me.Frame1.Controls.Add(Me.txtPartyName)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 40)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(401, 55)
        Me.Frame1.TabIndex = 30
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Party Name"
        '
        '_optPartyName_1
        '
        Me._optPartyName_1.AutoSize = True
        Me._optPartyName_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPartyName_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPartyName_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPartyName_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPartyName.SetIndex(Me._optPartyName_1, CType(1, Short))
        Me._optPartyName_1.Location = New System.Drawing.Point(134, 10)
        Me._optPartyName_1.Name = "_optPartyName_1"
        Me._optPartyName_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPartyName_1.Size = New System.Drawing.Size(84, 18)
        Me._optPartyName_1.TabIndex = 5
        Me._optPartyName_1.TabStop = True
        Me._optPartyName_1.Text = "Particulars"
        Me._optPartyName_1.UseVisualStyleBackColor = False
        '
        '_optPartyName_0
        '
        Me._optPartyName_0.AutoSize = True
        Me._optPartyName_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPartyName_0.Checked = True
        Me._optPartyName_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPartyName_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPartyName_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPartyName.SetIndex(Me._optPartyName_0, CType(0, Short))
        Me._optPartyName_0.Location = New System.Drawing.Point(70, 10)
        Me._optPartyName_0.Name = "_optPartyName_0"
        Me._optPartyName_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPartyName_0.Size = New System.Drawing.Size(39, 18)
        Me._optPartyName_0.TabIndex = 4
        Me._optPartyName_0.TabStop = True
        Me._optPartyName_0.Text = "All"
        Me._optPartyName_0.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdLedg)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me.lblAcCode)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(-2, 90)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(751, 319)
        Me.Frame4.TabIndex = 19
        Me.Frame4.TabStop = False
        '
        'SprdLedg
        '
        Me.SprdLedg.DataSource = Nothing
        Me.SprdLedg.Location = New System.Drawing.Point(2, 8)
        Me.SprdLedg.Name = "SprdLedg"
        Me.SprdLedg.OcxState = CType(resources.GetObject("SprdLedg.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdLedg.Size = New System.Drawing.Size(748, 309)
        Me.SprdLedg.TabIndex = 14
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 15
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(30, 14)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(55, 11)
        Me.lblAcCode.TabIndex = 20
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'FraPreview
        '
        Me.FraPreview.BackColor = System.Drawing.SystemColors.Control
        Me.FraPreview.Controls.Add(Me.SprdPreview)
        Me.FraPreview.Controls.Add(Me.SprdCommand)
        Me.FraPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPreview.Location = New System.Drawing.Point(0, 0)
        Me.FraPreview.Name = "FraPreview"
        Me.FraPreview.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPreview.Size = New System.Drawing.Size(751, 409)
        Me.FraPreview.TabIndex = 27
        Me.FraPreview.TabStop = False
        Me.FraPreview.Visible = False
        '
        'SprdPreview
        '
        Me.SprdPreview.Location = New System.Drawing.Point(4, 46)
        Me.SprdPreview.Name = "SprdPreview"
        Me.SprdPreview.OcxState = CType(resources.GetObject("SprdPreview.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPreview.Size = New System.Drawing.Size(739, 359)
        Me.SprdPreview.TabIndex = 28
        '
        'SprdCommand
        '
        Me.SprdCommand.DataSource = Nothing
        Me.SprdCommand.Location = New System.Drawing.Point(4, 12)
        Me.SprdCommand.Name = "SprdCommand"
        Me.SprdCommand.OcxState = CType(resources.GetObject("SprdCommand.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdCommand.Size = New System.Drawing.Size(739, 29)
        Me.SprdCommand.TabIndex = 29
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(500, 404)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(251, 53)
        Me.FraMovement.TabIndex = 33
        Me.FraMovement.TabStop = False
        '
        'optChallan
        '
        '
        'optChallanAmt
        '
        '
        'optPartyName
        '
        '
        'frmViewTDSEnqCha
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(751, 458)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraPreview)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewTDSEnqCha"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "View TDS Enquiry - Challans"
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdLedg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraPreview.ResumeLayout(False)
        CType(Me.SprdPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdCommand, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optChallan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optChallanAmt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPartyName, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdLedg.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
	Public Sub VB6_RemoveADODataBinding()
		SprdLedg.DataSource = Nothing
	End Sub
#End Region 
End Class