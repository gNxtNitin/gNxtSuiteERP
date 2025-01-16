Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamMonthlySchld
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
        'Me.MDIParent = Costing.Master
        'Costing.Master.Show
    End Sub
	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents chkCategory As System.Windows.Forms.CheckBox
	Public WithEvents txtCategoryDesc As System.Windows.Forms.TextBox
	Public WithEvents cmdCategory As System.Windows.Forms.Button
	Public WithEvents chkAllParty As System.Windows.Forms.CheckBox
	Public WithEvents txtPartyName As System.Windows.Forms.TextBox
	Public WithEvents cmdPartyName As System.Windows.Forms.Button
	Public WithEvents _optDetSummarised_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optDetSummarised_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optDetSummarised_2 As System.Windows.Forms.RadioButton
	Public WithEvents cmdSearchItem As System.Windows.Forms.Button
	Public WithEvents txtItemName As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchFG As System.Windows.Forms.Button
	Public WithEvents txtFGName As System.Windows.Forms.TextBox
	Public WithEvents chkFG As System.Windows.Forms.CheckBox
	Public WithEvents chkItem As System.Windows.Forms.CheckBox
	Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblRunDate As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cmdProcess As System.Windows.Forms.Button
    Public WithEvents chkRateRequired As System.Windows.Forms.CheckBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
    Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
    Public CommonDialog1Font As System.Windows.Forms.FontDialog
    Public CommonDialog1Color As System.Windows.Forms.ColorDialog
    Public CommonDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblBookSubType As System.Windows.Forms.Label
    Public WithEvents lblSubCatCode As System.Windows.Forms.Label
    Public WithEvents lblCatCode As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents optDetSummarised As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamMonthlySchld))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdCategory = New System.Windows.Forms.Button()
        Me.cmdPartyName = New System.Windows.Forms.Button()
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.cmdSearchFG = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.lstMaterialType = New System.Windows.Forms.CheckedListBox()
        Me.lblYear = New System.Windows.Forms.DateTimePicker()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.txtCategoryDesc = New System.Windows.Forms.TextBox()
        Me.chkAllParty = New System.Windows.Forms.CheckBox()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me._optDetSummarised_1 = New System.Windows.Forms.RadioButton()
        Me._optDetSummarised_0 = New System.Windows.Forms.RadioButton()
        Me._optDetSummarised_2 = New System.Windows.Forms.RadioButton()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.txtFGName = New System.Windows.Forms.TextBox()
        Me.chkFG = New System.Windows.Forms.CheckBox()
        Me.chkItem = New System.Windows.Forms.CheckBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblRunDate = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cmdProcess = New System.Windows.Forms.Button()
        Me.chkRateRequired = New System.Windows.Forms.CheckBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblBookSubType = New System.Windows.Forms.Label()
        Me.lblSubCatCode = New System.Windows.Forms.Label()
        Me.lblCatCode = New System.Windows.Forms.Label()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.optDetSummarised = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame4.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.optDetSummarised, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCategory
        '
        Me.cmdCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCategory.Image = CType(resources.GetObject("cmdCategory.Image"), System.Drawing.Image)
        Me.cmdCategory.Location = New System.Drawing.Point(371, 88)
        Me.cmdCategory.Name = "cmdCategory"
        Me.cmdCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCategory.Size = New System.Drawing.Size(26, 26)
        Me.cmdCategory.TabIndex = 31
        Me.cmdCategory.TabStop = False
        Me.cmdCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCategory, "Search")
        Me.cmdCategory.UseVisualStyleBackColor = False
        Me.cmdCategory.Visible = False
        '
        'cmdPartyName
        '
        Me.cmdPartyName.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPartyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPartyName.Enabled = False
        Me.cmdPartyName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPartyName.Image = CType(resources.GetObject("cmdPartyName.Image"), System.Drawing.Image)
        Me.cmdPartyName.Location = New System.Drawing.Point(371, 7)
        Me.cmdPartyName.Name = "cmdPartyName"
        Me.cmdPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPartyName.Size = New System.Drawing.Size(26, 26)
        Me.cmdPartyName.TabIndex = 22
        Me.cmdPartyName.TabStop = False
        Me.cmdPartyName.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPartyName, "Search")
        Me.cmdPartyName.UseVisualStyleBackColor = False
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(371, 62)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(26, 26)
        Me.cmdSearchItem.TabIndex = 12
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'cmdSearchFG
        '
        Me.cmdSearchFG.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchFG.Image = CType(resources.GetObject("cmdSearchFG.Image"), System.Drawing.Image)
        Me.cmdSearchFG.Location = New System.Drawing.Point(371, 36)
        Me.cmdSearchFG.Name = "cmdSearchFG"
        Me.cmdSearchFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchFG.Size = New System.Drawing.Size(26, 26)
        Me.cmdSearchFG.TabIndex = 10
        Me.cmdSearchFG.TabStop = False
        Me.cmdSearchFG.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchFG, "Search")
        Me.cmdSearchFG.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(825, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 35)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(759, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 35)
        Me.CmdPreview.TabIndex = 4
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
        Me.cmdPrint.Location = New System.Drawing.Point(693, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 35)
        Me.cmdPrint.TabIndex = 3
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(627, 10)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 35)
        Me.cmdShow.TabIndex = 1
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.AutoSize = True
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.Frame5)
        Me.Frame4.Controls.Add(Me.lblYear)
        Me.Frame4.Controls.Add(Me.chkCategory)
        Me.Frame4.Controls.Add(Me.txtCategoryDesc)
        Me.Frame4.Controls.Add(Me.cmdCategory)
        Me.Frame4.Controls.Add(Me.chkAllParty)
        Me.Frame4.Controls.Add(Me.txtPartyName)
        Me.Frame4.Controls.Add(Me.cmdPartyName)
        Me.Frame4.Controls.Add(Me._optDetSummarised_1)
        Me.Frame4.Controls.Add(Me._optDetSummarised_0)
        Me.Frame4.Controls.Add(Me._optDetSummarised_2)
        Me.Frame4.Controls.Add(Me.cmdSearchItem)
        Me.Frame4.Controls.Add(Me.txtItemName)
        Me.Frame4.Controls.Add(Me.cmdSearchFG)
        Me.Frame4.Controls.Add(Me.txtFGName)
        Me.Frame4.Controls.Add(Me.chkFG)
        Me.Frame4.Controls.Add(Me.chkItem)
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Controls.Add(Me.lblRunDate)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Controls.Add(Me.Label3)
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -4)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(898, 569)
        Me.Frame4.TabIndex = 5
        Me.Frame4.TabStop = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.lstMaterialType)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(443, 4)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(280, 109)
        Me.Frame5.TabIndex = 44
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Category"
        '
        'lstMaterialType
        '
        Me.lstMaterialType.BackColor = System.Drawing.SystemColors.Window
        Me.lstMaterialType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstMaterialType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMaterialType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstMaterialType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstMaterialType.IntegralHeight = False
        Me.lstMaterialType.Items.AddRange(New Object() {"lstMaterialType"})
        Me.lstMaterialType.Location = New System.Drawing.Point(0, 15)
        Me.lstMaterialType.Name = "lstMaterialType"
        Me.lstMaterialType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstMaterialType.Size = New System.Drawing.Size(280, 94)
        Me.lstMaterialType.TabIndex = 44
        '
        'lblYear
        '
        Me.lblYear.CustomFormat = "MMMM,yyyy"
        Me.lblYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblYear.Location = New System.Drawing.Point(731, 10)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(157, 20)
        Me.lblYear.TabIndex = 37
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Checked = True
        Me.chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(396, 89)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(37, 17)
        Me.chkCategory.TabIndex = 33
        Me.chkCategory.Text = "All"
        Me.chkCategory.UseVisualStyleBackColor = False
        Me.chkCategory.Visible = False
        '
        'txtCategoryDesc
        '
        Me.txtCategoryDesc.AcceptsReturn = True
        Me.txtCategoryDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategoryDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCategoryDesc.ForeColor = System.Drawing.Color.Blue
        Me.txtCategoryDesc.Location = New System.Drawing.Point(95, 88)
        Me.txtCategoryDesc.MaxLength = 0
        Me.txtCategoryDesc.Name = "txtCategoryDesc"
        Me.txtCategoryDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCategoryDesc.Size = New System.Drawing.Size(275, 20)
        Me.txtCategoryDesc.TabIndex = 32
        Me.txtCategoryDesc.Visible = False
        '
        'chkAllParty
        '
        Me.chkAllParty.AutoSize = True
        Me.chkAllParty.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllParty.Checked = True
        Me.chkAllParty.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllParty.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllParty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllParty.Location = New System.Drawing.Point(397, 14)
        Me.chkAllParty.Name = "chkAllParty"
        Me.chkAllParty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllParty.Size = New System.Drawing.Size(37, 17)
        Me.chkAllParty.TabIndex = 24
        Me.chkAllParty.Text = "All"
        Me.chkAllParty.UseVisualStyleBackColor = False
        '
        'txtPartyName
        '
        Me.txtPartyName.AcceptsReturn = True
        Me.txtPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyName.Enabled = False
        Me.txtPartyName.ForeColor = System.Drawing.Color.Blue
        Me.txtPartyName.Location = New System.Drawing.Point(96, 10)
        Me.txtPartyName.MaxLength = 0
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyName.Size = New System.Drawing.Size(275, 20)
        Me.txtPartyName.TabIndex = 23
        '
        '_optDetSummarised_1
        '
        Me._optDetSummarised_1.AutoSize = True
        Me._optDetSummarised_1.BackColor = System.Drawing.SystemColors.Control
        Me._optDetSummarised_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDetSummarised_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDetSummarised.SetIndex(Me._optDetSummarised_1, CType(1, Short))
        Me._optDetSummarised_1.Location = New System.Drawing.Point(734, 54)
        Me._optDetSummarised_1.Name = "_optDetSummarised_1"
        Me._optDetSummarised_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDetSummarised_1.Size = New System.Drawing.Size(76, 17)
        Me._optDetSummarised_1.TabIndex = 21
        Me._optDetSummarised_1.TabStop = True
        Me._optDetSummarised_1.Text = "Party Wise"
        Me._optDetSummarised_1.UseVisualStyleBackColor = False
        '
        '_optDetSummarised_0
        '
        Me._optDetSummarised_0.AutoSize = True
        Me._optDetSummarised_0.BackColor = System.Drawing.SystemColors.Control
        Me._optDetSummarised_0.Checked = True
        Me._optDetSummarised_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDetSummarised_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDetSummarised.SetIndex(Me._optDetSummarised_0, CType(0, Short))
        Me._optDetSummarised_0.Location = New System.Drawing.Point(734, 36)
        Me._optDetSummarised_0.Name = "_optDetSummarised_0"
        Me._optDetSummarised_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDetSummarised_0.Size = New System.Drawing.Size(82, 17)
        Me._optDetSummarised_0.TabIndex = 18
        Me._optDetSummarised_0.TabStop = True
        Me._optDetSummarised_0.Text = "Summarised"
        Me._optDetSummarised_0.UseVisualStyleBackColor = False
        '
        '_optDetSummarised_2
        '
        Me._optDetSummarised_2.AutoSize = True
        Me._optDetSummarised_2.BackColor = System.Drawing.SystemColors.Control
        Me._optDetSummarised_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDetSummarised_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDetSummarised.SetIndex(Me._optDetSummarised_2, CType(2, Short))
        Me._optDetSummarised_2.Location = New System.Drawing.Point(734, 74)
        Me._optDetSummarised_2.Name = "_optDetSummarised_2"
        Me._optDetSummarised_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDetSummarised_2.Size = New System.Drawing.Size(137, 17)
        Me._optDetSummarised_2.TabIndex = 17
        Me._optDetSummarised_2.TabStop = True
        Me._optDetSummarised_2.Text = "Detailed (Product Wise)"
        Me._optDetSummarised_2.UseVisualStyleBackColor = False
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.ForeColor = System.Drawing.Color.Blue
        Me.txtItemName.Location = New System.Drawing.Point(96, 62)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(275, 20)
        Me.txtItemName.TabIndex = 11
        '
        'txtFGName
        '
        Me.txtFGName.AcceptsReturn = True
        Me.txtFGName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFGName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFGName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFGName.ForeColor = System.Drawing.Color.Blue
        Me.txtFGName.Location = New System.Drawing.Point(96, 36)
        Me.txtFGName.MaxLength = 0
        Me.txtFGName.Name = "txtFGName"
        Me.txtFGName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFGName.Size = New System.Drawing.Size(275, 20)
        Me.txtFGName.TabIndex = 9
        '
        'chkFG
        '
        Me.chkFG.AutoSize = True
        Me.chkFG.BackColor = System.Drawing.SystemColors.Control
        Me.chkFG.Checked = True
        Me.chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFG.Location = New System.Drawing.Point(397, 39)
        Me.chkFG.Name = "chkFG"
        Me.chkFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFG.Size = New System.Drawing.Size(37, 17)
        Me.chkFG.TabIndex = 8
        Me.chkFG.Text = "All"
        Me.chkFG.UseVisualStyleBackColor = False
        '
        'chkItem
        '
        Me.chkItem.AutoSize = True
        Me.chkItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkItem.Checked = True
        Me.chkItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkItem.Location = New System.Drawing.Point(397, 64)
        Me.chkItem.Name = "chkItem"
        Me.chkItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkItem.Size = New System.Drawing.Size(37, 17)
        Me.chkItem.TabIndex = 7
        Me.chkItem.Text = "All"
        Me.chkItem.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 115)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(894, 438)
        Me.SprdMain.TabIndex = 6
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(860, 88)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 34
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(36, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Category :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label4.Visible = False
        '
        'lblRunDate
        '
        Me.lblRunDate.AutoSize = True
        Me.lblRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRunDate.Location = New System.Drawing.Point(838, 38)
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRunDate.Size = New System.Drawing.Size(50, 13)
        Me.lblRunDate.TabIndex = 29
        Me.lblRunDate.Text = "RunDate"
        Me.lblRunDate.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(23, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Party Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Finished Good :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(33, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "BOP Item :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdProcess)
        Me.Frame3.Controls.Add(Me.chkRateRequired)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdShow)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblBookSubType)
        Me.Frame3.Controls.Add(Me.lblSubCatCode)
        Me.Frame3.Controls.Add(Me.lblCatCode)
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 559)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(898, 49)
        Me.Frame3.TabIndex = 0
        Me.Frame3.TabStop = False
        '
        'cmdProcess
        '
        Me.cmdProcess.BackColor = System.Drawing.SystemColors.Control
        Me.cmdProcess.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdProcess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdProcess.Location = New System.Drawing.Point(300, 10)
        Me.cmdProcess.Name = "cmdProcess"
        Me.cmdProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdProcess.Size = New System.Drawing.Size(180, 35)
        Me.cmdProcess.TabIndex = 30
        Me.cmdProcess.Text = "Process For Schedule"
        Me.cmdProcess.UseVisualStyleBackColor = False
        '
        'chkRateRequired
        '
        Me.chkRateRequired.AutoSize = True
        Me.chkRateRequired.BackColor = System.Drawing.SystemColors.Control
        Me.chkRateRequired.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRateRequired.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRateRequired.Location = New System.Drawing.Point(8, 14)
        Me.chkRateRequired.Name = "chkRateRequired"
        Me.chkRateRequired.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRateRequired.Size = New System.Drawing.Size(95, 17)
        Me.chkRateRequired.TabIndex = 26
        Me.chkRateRequired.Text = "Rate Required"
        Me.chkRateRequired.UseVisualStyleBackColor = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(88, 10)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(107, 17)
        Me.lblBookType.TabIndex = 20
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBookType.Visible = False
        '
        'lblBookSubType
        '
        Me.lblBookSubType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookSubType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookSubType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookSubType.Location = New System.Drawing.Point(88, 30)
        Me.lblBookSubType.Name = "lblBookSubType"
        Me.lblBookSubType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookSubType.Size = New System.Drawing.Size(107, 17)
        Me.lblBookSubType.TabIndex = 19
        Me.lblBookSubType.Text = "lblBookSubType"
        Me.lblBookSubType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBookSubType.Visible = False
        '
        'lblSubCatCode
        '
        Me.lblSubCatCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblSubCatCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSubCatCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSubCatCode.Location = New System.Drawing.Point(164, 28)
        Me.lblSubCatCode.Name = "lblSubCatCode"
        Me.lblSubCatCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSubCatCode.Size = New System.Drawing.Size(87, 13)
        Me.lblSubCatCode.TabIndex = 16
        Me.lblSubCatCode.Text = "lblSubCatCode"
        Me.lblSubCatCode.Visible = False
        '
        'lblCatCode
        '
        Me.lblCatCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblCatCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCatCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCatCode.Location = New System.Drawing.Point(18, 12)
        Me.lblCatCode.Name = "lblCatCode"
        Me.lblCatCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCatCode.Size = New System.Drawing.Size(81, 13)
        Me.lblCatCode.TabIndex = 15
        Me.lblCatCode.Text = "lblCatCode"
        Me.lblCatCode.Visible = False
        '
        'optDetSummarised
        '
        '
        'frmParamMonthlySchld
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 16)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamMonthlySchld"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Monthly Schedule"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.optDetSummarised, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblYear As DateTimePicker
    Public WithEvents Frame5 As GroupBox
    Public WithEvents lstMaterialType As CheckedListBox
#End Region
End Class