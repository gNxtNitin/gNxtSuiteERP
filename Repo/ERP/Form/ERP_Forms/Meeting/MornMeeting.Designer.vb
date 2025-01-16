Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMornMeeting
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
        'Production.Master.Show
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
	Public WithEvents txtNarration As System.Windows.Forms.TextBox
	Public WithEvents txtPointType As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchPT As System.Windows.Forms.Button
	Public WithEvents chkAllDept As System.Windows.Forms.CheckBox
	Public WithEvents cmdSearchExpectedDept As System.Windows.Forms.Button
	Public WithEvents txtExpectedDept As System.Windows.Forms.TextBox
	Public WithEvents chkIsStatus As System.Windows.Forms.CheckBox
	Public WithEvents txtNumber As System.Windows.Forms.TextBox
	Public WithEvents TxtRemarks As System.Windows.Forms.TextBox
	Public WithEvents txtExpectedBy As System.Windows.Forms.TextBox
	Public WithEvents cmdSearchExpectedBy As System.Windows.Forms.Button
	Public WithEvents cmdSearchRaisedBy As System.Windows.Forms.Button
	Public WithEvents txtRaisedBy As System.Windows.Forms.TextBox
	Public WithEvents txtRaisedDate As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtExpectedDate1 As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtExpectedDate2 As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtExpectedDate3 As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtExpectedDate4 As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtExpectedDate5 As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label18 As System.Windows.Forms.Label
	Public WithEvents Label17 As System.Windows.Forms.Label
	Public WithEvents lblPointType As System.Windows.Forms.Label
	Public WithEvents lblBookType As System.Windows.Forms.Label
	Public WithEvents lblExpectedDept As System.Windows.Forms.Label
	Public WithEvents Label15 As System.Windows.Forms.Label
	Public WithEvents Label12 As System.Windows.Forms.Label
	Public WithEvents Label11 As System.Windows.Forms.Label
	Public WithEvents Label10 As System.Windows.Forms.Label
	Public WithEvents Label9 As System.Windows.Forms.Label
	Public WithEvents Label8 As System.Windows.Forms.Label
	Public WithEvents Label7 As System.Windows.Forms.Label
	Public WithEvents Label6 As System.Windows.Forms.Label
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents lblNumber As System.Windows.Forms.Label
	Public WithEvents Label20 As System.Windows.Forms.Label
	Public WithEvents Label30 As System.Windows.Forms.Label
	Public WithEvents lblMkey As System.Windows.Forms.Label
	Public WithEvents Label14 As System.Windows.Forms.Label
	Public WithEvents lblExpectedBy As System.Windows.Forms.Label
	Public WithEvents Label13 As System.Windows.Forms.Label
	Public WithEvents lblRaisedBy As System.Windows.Forms.Label
	Public WithEvents Label5 As System.Windows.Forms.Label
	Public WithEvents fraBase As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
	Public WithEvents CmdView As System.Windows.Forms.Button
	Public WithEvents CmdDelete As System.Windows.Forms.Button
	Public WithEvents CmdSave As System.Windows.Forms.Button
	Public WithEvents CmdModify As System.Windows.Forms.Button
	Public WithEvents CmdAdd As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdSavePrint As System.Windows.Forms.Button
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMornMeeting))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchPT = New System.Windows.Forms.Button()
        Me.cmdSearchExpectedDept = New System.Windows.Forms.Button()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.cmdSearchExpectedBy = New System.Windows.Forms.Button()
        Me.cmdSearchRaisedBy = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.fraBase = New System.Windows.Forms.GroupBox()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.txtPointType = New System.Windows.Forms.TextBox()
        Me.chkAllDept = New System.Windows.Forms.CheckBox()
        Me.txtExpectedDept = New System.Windows.Forms.TextBox()
        Me.chkIsStatus = New System.Windows.Forms.CheckBox()
        Me.TxtRemarks = New System.Windows.Forms.TextBox()
        Me.txtExpectedBy = New System.Windows.Forms.TextBox()
        Me.txtRaisedBy = New System.Windows.Forms.TextBox()
        Me.txtRaisedDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtExpectedDate1 = New System.Windows.Forms.MaskedTextBox()
        Me.txtExpectedDate2 = New System.Windows.Forms.MaskedTextBox()
        Me.txtExpectedDate3 = New System.Windows.Forms.MaskedTextBox()
        Me.txtExpectedDate4 = New System.Windows.Forms.MaskedTextBox()
        Me.txtExpectedDate5 = New System.Windows.Forms.MaskedTextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblPointType = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblExpectedDept = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNumber = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblExpectedBy = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblRaisedBy = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.fraBase.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchPT
        '
        Me.cmdSearchPT.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPT.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPT.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPT.Image = CType(resources.GetObject("cmdSearchPT.Image"), System.Drawing.Image)
        Me.cmdSearchPT.Location = New System.Drawing.Point(210, 114)
        Me.cmdSearchPT.Name = "cmdSearchPT"
        Me.cmdSearchPT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPT.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchPT.TabIndex = 13
        Me.cmdSearchPT.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPT, "Search")
        Me.cmdSearchPT.UseVisualStyleBackColor = False
        '
        'cmdSearchExpectedDept
        '
        Me.cmdSearchExpectedDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchExpectedDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchExpectedDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchExpectedDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchExpectedDept.Image = CType(resources.GetObject("cmdSearchExpectedDept.Image"), System.Drawing.Image)
        Me.cmdSearchExpectedDept.Location = New System.Drawing.Point(210, 92)
        Me.cmdSearchExpectedDept.Name = "cmdSearchExpectedDept"
        Me.cmdSearchExpectedDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchExpectedDept.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchExpectedDept.TabIndex = 10
        Me.cmdSearchExpectedDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchExpectedDept, "Search")
        Me.cmdSearchExpectedDept.UseVisualStyleBackColor = False
        '
        'txtNumber
        '
        Me.txtNumber.AcceptsReturn = True
        Me.txtNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumber.Location = New System.Drawing.Point(120, 22)
        Me.txtNumber.MaxLength = 0
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumber.Size = New System.Drawing.Size(89, 20)
        Me.txtNumber.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtNumber, "Press F1 For Help")
        '
        'cmdSearchExpectedBy
        '
        Me.cmdSearchExpectedBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchExpectedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchExpectedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchExpectedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchExpectedBy.Image = CType(resources.GetObject("cmdSearchExpectedBy.Image"), System.Drawing.Image)
        Me.cmdSearchExpectedBy.Location = New System.Drawing.Point(210, 70)
        Me.cmdSearchExpectedBy.Name = "cmdSearchExpectedBy"
        Me.cmdSearchExpectedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchExpectedBy.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchExpectedBy.TabIndex = 7
        Me.cmdSearchExpectedBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchExpectedBy, "Search")
        Me.cmdSearchExpectedBy.UseVisualStyleBackColor = False
        '
        'cmdSearchRaisedBy
        '
        Me.cmdSearchRaisedBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchRaisedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchRaisedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchRaisedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchRaisedBy.Image = CType(resources.GetObject("cmdSearchRaisedBy.Image"), System.Drawing.Image)
        Me.cmdSearchRaisedBy.Location = New System.Drawing.Point(210, 48)
        Me.cmdSearchRaisedBy.Name = "cmdSearchRaisedBy"
        Me.cmdSearchRaisedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchRaisedBy.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchRaisedBy.TabIndex = 4
        Me.cmdSearchRaisedBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchRaisedBy, "Search")
        Me.cmdSearchRaisedBy.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(707, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(79, 37)
        Me.CmdClose.TabIndex = 31
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
        Me.CmdView.Location = New System.Drawing.Point(630, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(79, 37)
        Me.CmdView.TabIndex = 30
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(395, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(79, 37)
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
        Me.CmdSave.Location = New System.Drawing.Point(238, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(79, 37)
        Me.CmdSave.TabIndex = 25
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
        Me.CmdModify.Location = New System.Drawing.Point(160, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(79, 37)
        Me.CmdModify.TabIndex = 24
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
        Me.CmdAdd.Location = New System.Drawing.Point(81, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(79, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(473, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(79, 37)
        Me.cmdPrint.TabIndex = 28
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
        Me.CmdPreview.Location = New System.Drawing.Point(552, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(79, 37)
        Me.CmdPreview.TabIndex = 29
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(316, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(79, 37)
        Me.cmdSavePrint.TabIndex = 26
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'fraBase
        '
        Me.fraBase.BackColor = System.Drawing.SystemColors.Control
        Me.fraBase.Controls.Add(Me.txtNarration)
        Me.fraBase.Controls.Add(Me.txtPointType)
        Me.fraBase.Controls.Add(Me.cmdSearchPT)
        Me.fraBase.Controls.Add(Me.chkAllDept)
        Me.fraBase.Controls.Add(Me.cmdSearchExpectedDept)
        Me.fraBase.Controls.Add(Me.txtExpectedDept)
        Me.fraBase.Controls.Add(Me.chkIsStatus)
        Me.fraBase.Controls.Add(Me.txtNumber)
        Me.fraBase.Controls.Add(Me.TxtRemarks)
        Me.fraBase.Controls.Add(Me.txtExpectedBy)
        Me.fraBase.Controls.Add(Me.cmdSearchExpectedBy)
        Me.fraBase.Controls.Add(Me.cmdSearchRaisedBy)
        Me.fraBase.Controls.Add(Me.txtRaisedBy)
        Me.fraBase.Controls.Add(Me.txtRaisedDate)
        Me.fraBase.Controls.Add(Me.txtExpectedDate1)
        Me.fraBase.Controls.Add(Me.txtExpectedDate2)
        Me.fraBase.Controls.Add(Me.txtExpectedDate3)
        Me.fraBase.Controls.Add(Me.txtExpectedDate4)
        Me.fraBase.Controls.Add(Me.txtExpectedDate5)
        Me.fraBase.Controls.Add(Me.Label18)
        Me.fraBase.Controls.Add(Me.Label17)
        Me.fraBase.Controls.Add(Me.lblPointType)
        Me.fraBase.Controls.Add(Me.lblBookType)
        Me.fraBase.Controls.Add(Me.lblExpectedDept)
        Me.fraBase.Controls.Add(Me.Label15)
        Me.fraBase.Controls.Add(Me.Label12)
        Me.fraBase.Controls.Add(Me.Label11)
        Me.fraBase.Controls.Add(Me.Label10)
        Me.fraBase.Controls.Add(Me.Label9)
        Me.fraBase.Controls.Add(Me.Label8)
        Me.fraBase.Controls.Add(Me.Label7)
        Me.fraBase.Controls.Add(Me.Label6)
        Me.fraBase.Controls.Add(Me.Label4)
        Me.fraBase.Controls.Add(Me.Label2)
        Me.fraBase.Controls.Add(Me.Label3)
        Me.fraBase.Controls.Add(Me.Label1)
        Me.fraBase.Controls.Add(Me.lblNumber)
        Me.fraBase.Controls.Add(Me.Label20)
        Me.fraBase.Controls.Add(Me.Label30)
        Me.fraBase.Controls.Add(Me.lblMkey)
        Me.fraBase.Controls.Add(Me.Label14)
        Me.fraBase.Controls.Add(Me.lblExpectedBy)
        Me.fraBase.Controls.Add(Me.Label13)
        Me.fraBase.Controls.Add(Me.lblRaisedBy)
        Me.fraBase.Controls.Add(Me.Label5)
        Me.fraBase.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBase.Location = New System.Drawing.Point(0, -5)
        Me.fraBase.Name = "fraBase"
        Me.fraBase.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBase.Size = New System.Drawing.Size(909, 457)
        Me.fraBase.TabIndex = 33
        Me.fraBase.TabStop = False
        '
        'txtNarration
        '
        Me.txtNarration.AcceptsReturn = True
        Me.txtNarration.BackColor = System.Drawing.SystemColors.Window
        Me.txtNarration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNarration.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNarration.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNarration.Location = New System.Drawing.Point(120, 371)
        Me.txtNarration.MaxLength = 0
        Me.txtNarration.Multiline = True
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNarration.Size = New System.Drawing.Size(459, 58)
        Me.txtNarration.TabIndex = 21
        '
        'txtPointType
        '
        Me.txtPointType.AcceptsReturn = True
        Me.txtPointType.BackColor = System.Drawing.SystemColors.Window
        Me.txtPointType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPointType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPointType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPointType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPointType.Location = New System.Drawing.Point(120, 114)
        Me.txtPointType.MaxLength = 0
        Me.txtPointType.Name = "txtPointType"
        Me.txtPointType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPointType.Size = New System.Drawing.Size(89, 20)
        Me.txtPointType.TabIndex = 12
        '
        'chkAllDept
        '
        Me.chkAllDept.AutoSize = True
        Me.chkAllDept.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDept.Location = New System.Drawing.Point(428, 439)
        Me.chkAllDept.Name = "chkAllDept"
        Me.chkAllDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDept.Size = New System.Drawing.Size(96, 18)
        Me.chkAllDept.TabIndex = 23
        Me.chkAllDept.Text = "For All HODs "
        Me.chkAllDept.UseVisualStyleBackColor = False
        '
        'txtExpectedDept
        '
        Me.txtExpectedDept.AcceptsReturn = True
        Me.txtExpectedDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpectedDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpectedDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExpectedDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpectedDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExpectedDept.Location = New System.Drawing.Point(120, 92)
        Me.txtExpectedDept.MaxLength = 0
        Me.txtExpectedDept.Name = "txtExpectedDept"
        Me.txtExpectedDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpectedDept.Size = New System.Drawing.Size(89, 20)
        Me.txtExpectedDept.TabIndex = 9
        '
        'chkIsStatus
        '
        Me.chkIsStatus.AutoSize = True
        Me.chkIsStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkIsStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkIsStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIsStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkIsStatus.Location = New System.Drawing.Point(120, 439)
        Me.chkIsStatus.Name = "chkIsStatus"
        Me.chkIsStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkIsStatus.Size = New System.Drawing.Size(103, 18)
        Me.chkIsStatus.TabIndex = 22
        Me.chkIsStatus.Text = "Open / Closed"
        Me.chkIsStatus.UseVisualStyleBackColor = False
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AcceptsReturn = True
        Me.TxtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtRemarks.Location = New System.Drawing.Point(120, 138)
        Me.TxtRemarks.MaxLength = 0
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtRemarks.Size = New System.Drawing.Size(459, 143)
        Me.TxtRemarks.TabIndex = 15
        '
        'txtExpectedBy
        '
        Me.txtExpectedBy.AcceptsReturn = True
        Me.txtExpectedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtExpectedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExpectedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExpectedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpectedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExpectedBy.Location = New System.Drawing.Point(120, 70)
        Me.txtExpectedBy.MaxLength = 0
        Me.txtExpectedBy.Name = "txtExpectedBy"
        Me.txtExpectedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExpectedBy.Size = New System.Drawing.Size(89, 20)
        Me.txtExpectedBy.TabIndex = 6
        '
        'txtRaisedBy
        '
        Me.txtRaisedBy.AcceptsReturn = True
        Me.txtRaisedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtRaisedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRaisedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRaisedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRaisedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRaisedBy.Location = New System.Drawing.Point(120, 48)
        Me.txtRaisedBy.MaxLength = 0
        Me.txtRaisedBy.Name = "txtRaisedBy"
        Me.txtRaisedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRaisedBy.Size = New System.Drawing.Size(89, 20)
        Me.txtRaisedBy.TabIndex = 3
        '
        'txtRaisedDate
        '
        Me.txtRaisedDate.AllowPromptAsInput = False
        Me.txtRaisedDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRaisedDate.Location = New System.Drawing.Point(416, 22)
        Me.txtRaisedDate.Mask = "##/##/####"
        Me.txtRaisedDate.Name = "txtRaisedDate"
        Me.txtRaisedDate.Size = New System.Drawing.Size(77, 20)
        Me.txtRaisedDate.TabIndex = 2
        '
        'txtExpectedDate1
        '
        Me.txtExpectedDate1.AllowPromptAsInput = False
        Me.txtExpectedDate1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpectedDate1.Location = New System.Drawing.Point(120, 287)
        Me.txtExpectedDate1.Mask = "##/##/####"
        Me.txtExpectedDate1.Name = "txtExpectedDate1"
        Me.txtExpectedDate1.Size = New System.Drawing.Size(77, 20)
        Me.txtExpectedDate1.TabIndex = 16
        '
        'txtExpectedDate2
        '
        Me.txtExpectedDate2.AllowPromptAsInput = False
        Me.txtExpectedDate2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpectedDate2.Location = New System.Drawing.Point(414, 287)
        Me.txtExpectedDate2.Mask = "##/##/####"
        Me.txtExpectedDate2.Name = "txtExpectedDate2"
        Me.txtExpectedDate2.Size = New System.Drawing.Size(77, 20)
        Me.txtExpectedDate2.TabIndex = 17
        '
        'txtExpectedDate3
        '
        Me.txtExpectedDate3.AllowPromptAsInput = False
        Me.txtExpectedDate3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpectedDate3.Location = New System.Drawing.Point(120, 315)
        Me.txtExpectedDate3.Mask = "##/##/####"
        Me.txtExpectedDate3.Name = "txtExpectedDate3"
        Me.txtExpectedDate3.Size = New System.Drawing.Size(77, 20)
        Me.txtExpectedDate3.TabIndex = 18
        '
        'txtExpectedDate4
        '
        Me.txtExpectedDate4.AllowPromptAsInput = False
        Me.txtExpectedDate4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpectedDate4.Location = New System.Drawing.Point(414, 315)
        Me.txtExpectedDate4.Mask = "##/##/####"
        Me.txtExpectedDate4.Name = "txtExpectedDate4"
        Me.txtExpectedDate4.Size = New System.Drawing.Size(77, 20)
        Me.txtExpectedDate4.TabIndex = 19
        '
        'txtExpectedDate5
        '
        Me.txtExpectedDate5.AllowPromptAsInput = False
        Me.txtExpectedDate5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpectedDate5.Location = New System.Drawing.Point(120, 341)
        Me.txtExpectedDate5.Mask = "##/##/####"
        Me.txtExpectedDate5.Name = "txtExpectedDate5"
        Me.txtExpectedDate5.Size = New System.Drawing.Size(77, 20)
        Me.txtExpectedDate5.TabIndex = 20
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(20, 375)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(63, 14)
        Me.Label18.TabIndex = 56
        Me.Label18.Text = "Remarks :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(46, 116)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(70, 14)
        Me.Label17.TabIndex = 55
        Me.Label17.Text = "Point Type :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPointType
        '
        Me.lblPointType.BackColor = System.Drawing.SystemColors.Control
        Me.lblPointType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPointType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPointType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPointType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPointType.Location = New System.Drawing.Point(242, 114)
        Me.lblPointType.Name = "lblPointType"
        Me.lblPointType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPointType.Size = New System.Drawing.Size(337, 19)
        Me.lblPointType.TabIndex = 14
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(336, 437)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(64, 14)
        Me.lblBookType.TabIndex = 54
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblExpectedDept
        '
        Me.lblExpectedDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblExpectedDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExpectedDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblExpectedDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpectedDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblExpectedDept.Location = New System.Drawing.Point(242, 92)
        Me.lblExpectedDept.Name = "lblExpectedDept"
        Me.lblExpectedDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblExpectedDept.Size = New System.Drawing.Size(337, 19)
        Me.lblExpectedDept.TabIndex = 11
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(25, 94)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(91, 14)
        Me.Label15.TabIndex = 52
        Me.Label15.Text = "Expected Dept :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(69, 439)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(48, 14)
        Me.Label12.TabIndex = 51
        Me.Label12.Text = "Status :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(201, 343)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(81, 13)
        Me.Label11.TabIndex = 50
        Me.Label11.Text = "DD/MM/YYYY"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(11, 345)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(99, 14)
        Me.Label10.TabIndex = 49
        Me.Label10.Text = "Expected Date 5 :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(495, 317)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "DD/MM/YYYY"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(305, 319)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(99, 14)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "Expected Date 4 :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(201, 319)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(81, 13)
        Me.Label7.TabIndex = 46
        Me.Label7.Text = "DD/MM/YYYY"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(11, 319)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(99, 14)
        Me.Label6.TabIndex = 45
        Me.Label6.Text = "Expected Date 3 :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(495, 289)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(81, 13)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "DD/MM/YYYY"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(305, 291)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(99, 14)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Expected Date 2 :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(201, 291)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "DD/MM/YYYY"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(11, 291)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(99, 14)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Expected Date 1 :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNumber
        '
        Me.lblNumber.AutoSize = True
        Me.lblNumber.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNumber.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNumber.Location = New System.Drawing.Point(65, 24)
        Me.lblNumber.Name = "lblNumber"
        Me.lblNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNumber.Size = New System.Drawing.Size(51, 14)
        Me.lblNumber.TabIndex = 40
        Me.lblNumber.Text = "Ref No. :"
        Me.lblNumber.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(332, 24)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(77, 14)
        Me.Label20.TabIndex = 39
        Me.Label20.Text = "Raised Date :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(497, 24)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(81, 13)
        Me.Label30.TabIndex = 38
        Me.Label30.Text = "DD/MM/YYYY"
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(264, 16)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(75, 15)
        Me.lblMkey.TabIndex = 37
        Me.lblMkey.Text = "lblMkey"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(21, 72)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(95, 14)
        Me.Label14.TabIndex = 36
        Me.Label14.Text = "Expected From :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblExpectedBy
        '
        Me.lblExpectedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblExpectedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblExpectedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblExpectedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpectedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblExpectedBy.Location = New System.Drawing.Point(242, 70)
        Me.lblExpectedBy.Name = "lblExpectedBy"
        Me.lblExpectedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblExpectedBy.Size = New System.Drawing.Size(337, 19)
        Me.lblExpectedBy.TabIndex = 8
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(50, 54)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(66, 14)
        Me.Label13.TabIndex = 35
        Me.Label13.Text = "Raised By :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRaisedBy
        '
        Me.lblRaisedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblRaisedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRaisedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRaisedBy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRaisedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRaisedBy.Location = New System.Drawing.Point(242, 48)
        Me.lblRaisedBy.Name = "lblRaisedBy"
        Me.lblRaisedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRaisedBy.Size = New System.Drawing.Size(337, 19)
        Me.lblRaisedBy.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(35, 142)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(81, 14)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Point / Issue :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(909, 451)
        Me.SprdView.TabIndex = 53
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdDelete)
        Me.Frame3.Controls.Add(Me.CmdSave)
        Me.Frame3.Controls.Add(Me.CmdModify)
        Me.Frame3.Controls.Add(Me.CmdAdd)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 450)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(909, 53)
        Me.Frame3.TabIndex = 32
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(516, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 32
        '
        'frmMornMeeting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 504)
        Me.Controls.Add(Me.fraBase)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmMornMeeting"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Morning Meeting Minutes"
        Me.fraBase.ResumeLayout(False)
        Me.fraBase.PerformLayout()
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
#End Region 
End Class