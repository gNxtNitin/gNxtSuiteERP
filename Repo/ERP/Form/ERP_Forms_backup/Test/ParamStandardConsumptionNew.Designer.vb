Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamStandardConsumptionNew
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
	Public WithEvents lstMaterialType As System.Windows.Forms.CheckedListBox
	Public WithEvents Frame6 As System.Windows.Forms.GroupBox
	Public WithEvents _txtDate_1 As System.Windows.Forms.MaskedTextBox
	Public WithEvents _txtDate_0 As System.Windows.Forms.MaskedTextBox
	Public WithEvents _Lbl_0 As System.Windows.Forms.Label
	Public WithEvents _Lbl_1 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents txtCategory As System.Windows.Forms.TextBox
	Public WithEvents cmdsearchCategory As System.Windows.Forms.Button
	Public WithEvents chkAllCategory As System.Windows.Forms.CheckBox
	Public WithEvents chkAllSubCat As System.Windows.Forms.CheckBox
	Public WithEvents cmdSubCatsearch As System.Windows.Forms.Button
	Public WithEvents txtSubCategory As System.Windows.Forms.TextBox
	Public WithEvents txtBOPName As System.Windows.Forms.TextBox
	Public WithEvents cmdsearchBOPName As System.Windows.Forms.Button
	Public WithEvents chkAllBOP As System.Windows.Forms.CheckBox
	Public WithEvents cmdSearchFG As System.Windows.Forms.Button
	Public WithEvents txtFGName As System.Windows.Forms.TextBox
	Public WithEvents chkFG As System.Windows.Forms.CheckBox
	Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents _Label4_0 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents _Label4_1 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents chkRate As System.Windows.Forms.CheckBox
	Public WithEvents cboClass As System.Windows.Forms.ComboBox
	Public WithEvents _optBase_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optBase_1 As System.Windows.Forms.RadioButton
	Public WithEvents Frame7 As System.Windows.Forms.GroupBox
	Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
	Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdShow As System.Windows.Forms.Button
	Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
	Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
	Public CommonDialog1Font As System.Windows.Forms.FontDialog
	Public CommonDialog1Color As System.Windows.Forms.ColorDialog
	Public CommonDialog1Print As System.Windows.Forms.PrintDialog
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents lblBookType As System.Windows.Forms.Label
	Public WithEvents lblBookSubType As System.Windows.Forms.Label
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents Label4 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
	Public WithEvents optBase As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents txtDate As Microsoft.VisualBasic.Compatibility.VB6.MaskedTextBoxArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamStandardConsumptionNew))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.cmdsearchCategory = New System.Windows.Forms.Button()
        Me.cmdSubCatsearch = New System.Windows.Forms.Button()
        Me.txtSubCategory = New System.Windows.Forms.TextBox()
        Me.txtBOPName = New System.Windows.Forms.TextBox()
        Me.cmdsearchBOPName = New System.Windows.Forms.Button()
        Me.cmdSearchFG = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.lstMaterialType = New System.Windows.Forms.CheckedListBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optDespatch = New System.Windows.Forms.RadioButton()
        Me.optProduction = New System.Windows.Forms.RadioButton()
        Me._txtDate_1 = New System.Windows.Forms.MaskedTextBox()
        Me._txtDate_0 = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me.chkAllCategory = New System.Windows.Forms.CheckBox()
        Me.chkAllSubCat = New System.Windows.Forms.CheckBox()
        Me.chkAllBOP = New System.Windows.Forms.CheckBox()
        Me.txtFGName = New System.Windows.Forms.TextBox()
        Me.chkFG = New System.Windows.Forms.CheckBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me._Label4_0 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me._Label4_1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkRate = New System.Windows.Forms.CheckBox()
        Me.cboClass = New System.Windows.Forms.ComboBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._optBase_0 = New System.Windows.Forms.RadioButton()
        Me._optBase_1 = New System.Windows.Forms.RadioButton()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblBookSubType = New System.Windows.Forms.Label()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Label4 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optBase = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtDate = New Microsoft.VisualBasic.Compatibility.VB6.MaskedTextBoxArray(Me.components)
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.Frame4.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optBase, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtCategory
        '
        Me.txtCategory.AcceptsReturn = True
        Me.txtCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCategory.Location = New System.Drawing.Point(422, 50)
        Me.txtCategory.MaxLength = 0
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCategory.Size = New System.Drawing.Size(269, 20)
        Me.txtCategory.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.txtCategory, "Press F1 For Help")
        Me.txtCategory.Visible = False
        '
        'cmdsearchCategory
        '
        Me.cmdsearchCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchCategory.Image = CType(resources.GetObject("cmdsearchCategory.Image"), System.Drawing.Image)
        Me.cmdsearchCategory.Location = New System.Drawing.Point(691, 50)
        Me.cmdsearchCategory.Name = "cmdsearchCategory"
        Me.cmdsearchCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchCategory.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearchCategory.TabIndex = 21
        Me.cmdsearchCategory.TabStop = False
        Me.cmdsearchCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchCategory, "Search")
        Me.cmdsearchCategory.UseVisualStyleBackColor = False
        Me.cmdsearchCategory.Visible = False
        '
        'cmdSubCatsearch
        '
        Me.cmdSubCatsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSubCatsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSubCatsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSubCatsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSubCatsearch.Image = CType(resources.GetObject("cmdSubCatsearch.Image"), System.Drawing.Image)
        Me.cmdSubCatsearch.Location = New System.Drawing.Point(691, 70)
        Me.cmdSubCatsearch.Name = "cmdSubCatsearch"
        Me.cmdSubCatsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSubCatsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdSubCatsearch.TabIndex = 18
        Me.cmdSubCatsearch.TabStop = False
        Me.cmdSubCatsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSubCatsearch, "Search")
        Me.cmdSubCatsearch.UseVisualStyleBackColor = False
        Me.cmdSubCatsearch.Visible = False
        '
        'txtSubCategory
        '
        Me.txtSubCategory.AcceptsReturn = True
        Me.txtSubCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubCategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubCategory.Location = New System.Drawing.Point(422, 70)
        Me.txtSubCategory.MaxLength = 0
        Me.txtSubCategory.Name = "txtSubCategory"
        Me.txtSubCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubCategory.Size = New System.Drawing.Size(269, 20)
        Me.txtSubCategory.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.txtSubCategory, "Press F1 For Help")
        Me.txtSubCategory.Visible = False
        '
        'txtBOPName
        '
        Me.txtBOPName.AcceptsReturn = True
        Me.txtBOPName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBOPName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBOPName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBOPName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBOPName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBOPName.Location = New System.Drawing.Point(422, 10)
        Me.txtBOPName.MaxLength = 0
        Me.txtBOPName.Name = "txtBOPName"
        Me.txtBOPName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBOPName.Size = New System.Drawing.Size(269, 20)
        Me.txtBOPName.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtBOPName, "Press F1 For Help")
        '
        'cmdsearchBOPName
        '
        Me.cmdsearchBOPName.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchBOPName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchBOPName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchBOPName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchBOPName.Image = CType(resources.GetObject("cmdsearchBOPName.Image"), System.Drawing.Image)
        Me.cmdsearchBOPName.Location = New System.Drawing.Point(691, 10)
        Me.cmdsearchBOPName.Name = "cmdsearchBOPName"
        Me.cmdsearchBOPName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchBOPName.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearchBOPName.TabIndex = 14
        Me.cmdsearchBOPName.TabStop = False
        Me.cmdsearchBOPName.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchBOPName, "Search")
        Me.cmdsearchBOPName.UseVisualStyleBackColor = False
        '
        'cmdSearchFG
        '
        Me.cmdSearchFG.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchFG.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchFG.Image = CType(resources.GetObject("cmdSearchFG.Image"), System.Drawing.Image)
        Me.cmdSearchFG.Location = New System.Drawing.Point(691, 30)
        Me.cmdSearchFG.Name = "cmdSearchFG"
        Me.cmdSearchFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchFG.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchFG.TabIndex = 9
        Me.cmdSearchFG.TabStop = False
        Me.cmdSearchFG.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchFG, "Search")
        Me.cmdSearchFG.UseVisualStyleBackColor = False
        Me.cmdSearchFG.Visible = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(698, 10)
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
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(632, 10)
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
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(566, 10)
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
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(500, 10)
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
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.GroupBox2)
        Me.Frame4.Controls.Add(Me.Frame6)
        Me.Frame4.Controls.Add(Me.Frame2)
        Me.Frame4.Controls.Add(Me.txtCategory)
        Me.Frame4.Controls.Add(Me.cmdsearchCategory)
        Me.Frame4.Controls.Add(Me.chkAllCategory)
        Me.Frame4.Controls.Add(Me.chkAllSubCat)
        Me.Frame4.Controls.Add(Me.cmdSubCatsearch)
        Me.Frame4.Controls.Add(Me.txtSubCategory)
        Me.Frame4.Controls.Add(Me.txtBOPName)
        Me.Frame4.Controls.Add(Me.cmdsearchBOPName)
        Me.Frame4.Controls.Add(Me.chkAllBOP)
        Me.Frame4.Controls.Add(Me.cmdSearchFG)
        Me.Frame4.Controls.Add(Me.txtFGName)
        Me.Frame4.Controls.Add(Me.chkFG)
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me._Label4_0)
        Me.Frame4.Controls.Add(Me.Label6)
        Me.Frame4.Controls.Add(Me._Label4_1)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Controls.Add(Me.GroupBox3)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(1, -2)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(995, 562)
        Me.Frame4.TabIndex = 5
        Me.Frame4.TabStop = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.lstMaterialType)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(122, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(193, 114)
        Me.Frame6.TabIndex = 33
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Material Type"
        '
        'lstMaterialType
        '
        Me.lstMaterialType.BackColor = System.Drawing.SystemColors.Window
        Me.lstMaterialType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstMaterialType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstMaterialType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstMaterialType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstMaterialType.IntegralHeight = False
        Me.lstMaterialType.Items.AddRange(New Object() {"lstMaterialType"})
        Me.lstMaterialType.Location = New System.Drawing.Point(0, 13)
        Me.lstMaterialType.Name = "lstMaterialType"
        Me.lstMaterialType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstMaterialType.Size = New System.Drawing.Size(193, 101)
        Me.lstMaterialType.TabIndex = 34
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.GroupBox1)
        Me.Frame2.Controls.Add(Me._txtDate_1)
        Me.Frame2.Controls.Add(Me._txtDate_0)
        Me.Frame2.Controls.Add(Me._Lbl_0)
        Me.Frame2.Controls.Add(Me._Lbl_1)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(121, 61)
        Me.Frame2.TabIndex = 25
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Date"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(3, 61)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(93, 56)
        Me.GroupBox1.TabIndex = 35
        Me.GroupBox1.TabStop = False
        '
        'optDespatch
        '
        Me.optDespatch.AutoSize = True
        Me.optDespatch.BackColor = System.Drawing.SystemColors.Control
        Me.optDespatch.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDespatch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDespatch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDespatch.Location = New System.Drawing.Point(3, 36)
        Me.optDespatch.Name = "optDespatch"
        Me.optDespatch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDespatch.Size = New System.Drawing.Size(72, 17)
        Me.optDespatch.TabIndex = 10
        Me.optDespatch.TabStop = True
        Me.optDespatch.Text = "Despatch"
        Me.optDespatch.UseVisualStyleBackColor = False
        '
        'optProduction
        '
        Me.optProduction.AutoSize = True
        Me.optProduction.BackColor = System.Drawing.SystemColors.Control
        Me.optProduction.Checked = True
        Me.optProduction.Cursor = System.Windows.Forms.Cursors.Default
        Me.optProduction.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optProduction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optProduction.Location = New System.Drawing.Point(3, 13)
        Me.optProduction.Name = "optProduction"
        Me.optProduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optProduction.Size = New System.Drawing.Size(80, 17)
        Me.optProduction.TabIndex = 9
        Me.optProduction.TabStop = True
        Me.optProduction.Text = "Production"
        Me.optProduction.UseVisualStyleBackColor = False
        '
        '_txtDate_1
        '
        Me._txtDate_1.AllowPromptAsInput = False
        Me._txtDate_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.SetIndex(Me._txtDate_1, CType(1, Short))
        Me._txtDate_1.Location = New System.Drawing.Point(36, 35)
        Me._txtDate_1.Mask = "##/##/####"
        Me._txtDate_1.Name = "_txtDate_1"
        Me._txtDate_1.Size = New System.Drawing.Size(81, 20)
        Me._txtDate_1.TabIndex = 26
        '
        '_txtDate_0
        '
        Me._txtDate_0.AllowPromptAsInput = False
        Me._txtDate_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.SetIndex(Me._txtDate_0, CType(0, Short))
        Me._txtDate_0.Location = New System.Drawing.Point(36, 12)
        Me._txtDate_0.Mask = "##/##/####"
        Me._txtDate_0.Name = "_txtDate_0"
        Me._txtDate_0.Size = New System.Drawing.Size(81, 20)
        Me._txtDate_0.TabIndex = 27
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 13)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(36, 14)
        Me._Lbl_0.TabIndex = 29
        Me._Lbl_0.Text = "From"
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(10, 39)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(20, 14)
        Me._Lbl_1.TabIndex = 28
        Me._Lbl_1.Text = "To"
        '
        'chkAllCategory
        '
        Me.chkAllCategory.AutoSize = True
        Me.chkAllCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCategory.Checked = True
        Me.chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCategory.Location = New System.Drawing.Point(721, 52)
        Me.chkAllCategory.Name = "chkAllCategory"
        Me.chkAllCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCategory.Size = New System.Drawing.Size(48, 18)
        Me.chkAllCategory.TabIndex = 20
        Me.chkAllCategory.Text = "ALL"
        Me.chkAllCategory.UseVisualStyleBackColor = False
        Me.chkAllCategory.Visible = False
        '
        'chkAllSubCat
        '
        Me.chkAllSubCat.AutoSize = True
        Me.chkAllSubCat.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllSubCat.Checked = True
        Me.chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllSubCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllSubCat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllSubCat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllSubCat.Location = New System.Drawing.Point(721, 72)
        Me.chkAllSubCat.Name = "chkAllSubCat"
        Me.chkAllSubCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllSubCat.Size = New System.Drawing.Size(48, 18)
        Me.chkAllSubCat.TabIndex = 19
        Me.chkAllSubCat.Text = "ALL"
        Me.chkAllSubCat.UseVisualStyleBackColor = False
        Me.chkAllSubCat.Visible = False
        '
        'chkAllBOP
        '
        Me.chkAllBOP.AutoSize = True
        Me.chkAllBOP.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllBOP.Checked = True
        Me.chkAllBOP.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllBOP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllBOP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllBOP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllBOP.Location = New System.Drawing.Point(721, 12)
        Me.chkAllBOP.Name = "chkAllBOP"
        Me.chkAllBOP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllBOP.Size = New System.Drawing.Size(48, 18)
        Me.chkAllBOP.TabIndex = 13
        Me.chkAllBOP.Text = "ALL"
        Me.chkAllBOP.UseVisualStyleBackColor = False
        '
        'txtFGName
        '
        Me.txtFGName.AcceptsReturn = True
        Me.txtFGName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFGName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFGName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFGName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFGName.ForeColor = System.Drawing.Color.Blue
        Me.txtFGName.Location = New System.Drawing.Point(422, 30)
        Me.txtFGName.MaxLength = 0
        Me.txtFGName.Name = "txtFGName"
        Me.txtFGName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFGName.Size = New System.Drawing.Size(269, 20)
        Me.txtFGName.TabIndex = 8
        Me.txtFGName.Visible = False
        '
        'chkFG
        '
        Me.chkFG.AutoSize = True
        Me.chkFG.BackColor = System.Drawing.SystemColors.Control
        Me.chkFG.Checked = True
        Me.chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFG.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFG.Location = New System.Drawing.Point(721, 32)
        Me.chkFG.Name = "chkFG"
        Me.chkFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFG.Size = New System.Drawing.Size(40, 18)
        Me.chkFG.TabIndex = 7
        Me.chkFG.Text = "All"
        Me.chkFG.UseVisualStyleBackColor = False
        Me.chkFG.Visible = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 117)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(990, 439)
        Me.SprdMain.TabIndex = 6
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 102)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 34
        '
        '_Label4_0
        '
        Me._Label4_0.AutoSize = True
        Me._Label4_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label4_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label4_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label4_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.SetIndex(Me._Label4_0, CType(0, Short))
        Me._Label4_0.Location = New System.Drawing.Point(324, 52)
        Me._Label4_0.Name = "_Label4_0"
        Me._Label4_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label4_0.Size = New System.Drawing.Size(63, 14)
        Me._Label4_0.TabIndex = 24
        Me._Label4_0.Text = "Category :"
        Me._Label4_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._Label4_0.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(324, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(87, 14)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Sub Category :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label6.Visible = False
        '
        '_Label4_1
        '
        Me._Label4_1.AutoSize = True
        Me._Label4_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label4_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label4_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label4_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.SetIndex(Me._Label4_1, CType(1, Short))
        Me._Label4_1.Location = New System.Drawing.Point(325, 12)
        Me._Label4_1.Name = "_Label4_1"
        Me._Label4_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label4_1.Size = New System.Drawing.Size(88, 14)
        Me._Label4_1.TabIndex = 16
        Me._Label4_1.Text = "RM/ BOP Desc :"
        Me._Label4_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(324, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(92, 14)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Finished Good :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Visible = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkRate)
        Me.Frame3.Controls.Add(Me.cboClass)
        Me.Frame3.Controls.Add(Me.Frame7)
        Me.Frame3.Controls.Add(Me.Frame5)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdShow)
        Me.Frame3.Controls.Add(Me.Label2)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblBookSubType)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 559)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(767, 49)
        Me.Frame3.TabIndex = 0
        Me.Frame3.TabStop = False
        '
        'chkRate
        '
        Me.chkRate.AutoSize = True
        Me.chkRate.BackColor = System.Drawing.SystemColors.Control
        Me.chkRate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRate.Location = New System.Drawing.Point(408, 20)
        Me.chkRate.Name = "chkRate"
        Me.chkRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRate.Size = New System.Drawing.Size(77, 18)
        Me.chkRate.TabIndex = 40
        Me.chkRate.Text = "With Rate"
        Me.chkRate.UseVisualStyleBackColor = False
        '
        'cboClass
        '
        Me.cboClass.BackColor = System.Drawing.SystemColors.Window
        Me.cboClass.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboClass.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboClass.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboClass.Location = New System.Drawing.Point(48, 16)
        Me.cboClass.Name = "cboClass"
        Me.cboClass.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboClass.Size = New System.Drawing.Size(81, 22)
        Me.cboClass.TabIndex = 38
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me._optBase_0)
        Me.Frame7.Controls.Add(Me._optBase_1)
        Me.Frame7.Enabled = False
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(258, 4)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(143, 45)
        Me.Frame7.TabIndex = 35
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Base on"
        Me.Frame7.Visible = False
        '
        '_optBase_0
        '
        Me._optBase_0.AutoSize = True
        Me._optBase_0.BackColor = System.Drawing.SystemColors.Control
        Me._optBase_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBase_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBase_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBase.SetIndex(Me._optBase_0, CType(0, Short))
        Me._optBase_0.Location = New System.Drawing.Point(38, 12)
        Me._optBase_0.Name = "_optBase_0"
        Me._optBase_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBase_0.Size = New System.Drawing.Size(85, 18)
        Me._optBase_0.TabIndex = 37
        Me._optBase_0.TabStop = True
        Me._optBase_0.Text = "Production"
        Me._optBase_0.UseVisualStyleBackColor = False
        '
        '_optBase_1
        '
        Me._optBase_1.AutoSize = True
        Me._optBase_1.BackColor = System.Drawing.SystemColors.Control
        Me._optBase_1.Checked = True
        Me._optBase_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBase_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBase_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBase.SetIndex(Me._optBase_1, CType(1, Short))
        Me._optBase_1.Location = New System.Drawing.Point(38, 28)
        Me._optBase_1.Name = "_optBase_1"
        Me._optBase_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBase_1.Size = New System.Drawing.Size(76, 18)
        Me._optBase_1.TabIndex = 36
        Me._optBase_1.TabStop = True
        Me._optBase_1.Text = "Despatch"
        Me._optBase_1.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._optShow_1)
        Me.Frame5.Controls.Add(Me._optShow_0)
        Me.Frame5.Enabled = False
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(130, 4)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(127, 45)
        Me.Frame5.TabIndex = 30
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Show"
        Me.Frame5.Visible = False
        '
        '_optShow_1
        '
        Me._optShow_1.AutoSize = True
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Checked = True
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(8, 28)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(39, 18)
        Me._optShow_1.TabIndex = 32
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "All"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        '_optShow_0
        '
        Me._optShow_0.AutoSize = True
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Enabled = False
        Me._optShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(8, 12)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(107, 18)
        Me._optShow_0.TabIndex = 31
        Me._optShow_0.TabStop = True
        Me._optShow_0.Text = "Only Base Item"
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(47, 14)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Class : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(88, 10)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(107, 17)
        Me.lblBookType.TabIndex = 12
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBookType.Visible = False
        '
        'lblBookSubType
        '
        Me.lblBookSubType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookSubType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookSubType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookSubType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookSubType.Location = New System.Drawing.Point(88, 30)
        Me.lblBookSubType.Name = "lblBookSubType"
        Me.lblBookSubType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookSubType.Size = New System.Drawing.Size(107, 17)
        Me.lblBookSubType.TabIndex = 11
        Me.lblBookSubType.Text = "lblBookSubType"
        Me.lblBookSubType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBookSubType.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.optProduction)
        Me.GroupBox3.Controls.Add(Me.optDespatch)
        Me.GroupBox3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(1, 58)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(118, 56)
        Me.GroupBox3.TabIndex = 35
        Me.GroupBox3.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.lstCompanyName)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox2.Location = New System.Drawing.Point(772, -3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox2.Size = New System.Drawing.Size(221, 115)
        Me.GroupBox2.TabIndex = 77
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Company Name"
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
        Me.lstCompanyName.Size = New System.Drawing.Size(221, 102)
        Me.lstCompanyName.TabIndex = 2
        '
        'frmParamStandardConsumptionNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(994, 611)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamStandardConsumptionNew"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
        Me.Text = "Standard Consumption Vs Physical"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optBase, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents optDespatch As RadioButton
    Public WithEvents optProduction As RadioButton
    Public WithEvents GroupBox3 As GroupBox
    Public WithEvents GroupBox2 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
#End Region
End Class