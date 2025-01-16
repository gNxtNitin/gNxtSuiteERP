<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBalanceSheet
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
	Public WithEvents txtDiff As System.Windows.Forms.TextBox
	Public WithEvents chkOpening As System.Windows.Forms.CheckBox
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents lblFrom As System.Windows.Forms.Label
	Public WithEvents lblTo As System.Windows.Forms.Label
	Public WithEvents fraDate As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public WithEvents PicUp As System.Windows.Forms.PictureBox
	Public WithEvents PicDown As System.Windows.Forms.PictureBox
	Public WithEvents PicDash As System.Windows.Forms.PictureBox
	Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
	Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
	Public CommonDialog1Font As System.Windows.Forms.FontDialog
	Public CommonDialog1Color As System.Windows.Forms.ColorDialog
	Public CommonDialog1Print As System.Windows.Forms.PrintDialog
	Public WithEvents cmdRefresh As System.Windows.Forms.Button
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents lblDiff As System.Windows.Forms.Label
	Public WithEvents lblType As System.Windows.Forms.Label
	Public WithEvents fraGrid As System.Windows.Forms.GroupBox
	Public WithEvents SprdCommand As AxFPSpreadADO.AxfpSpread
	Public WithEvents SprdPreview As AxFPSpreadADO.AxfpSpreadPreview
	Public WithEvents FraPreview As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBalanceSheet))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.chkExpand = New System.Windows.Forms.Button()
        Me.fraGrid = New System.Windows.Forms.GroupBox()
        Me.chkClosingStock = New System.Windows.Forms.CheckBox()
        Me.chkOpeningStock = New System.Windows.Forms.CheckBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.txtDiff = New System.Windows.Forms.TextBox()
        Me.chkOpening = New System.Windows.Forms.CheckBox()
        Me.fraDate = New System.Windows.Forms.GroupBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.PicUp = New System.Windows.Forms.PictureBox()
        Me.PicDown = New System.Windows.Forms.PictureBox()
        Me.PicDash = New System.Windows.Forms.PictureBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblDiff = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.FraPreview = New System.Windows.Forms.GroupBox()
        Me.SprdCommand = New AxFPSpreadADO.AxfpSpread()
        Me.SprdPreview = New AxFPSpreadADO.AxfpSpreadPreview()
        Me.fraGrid.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.fraDate.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicDash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.FraPreview.SuspendLayout()
        CType(Me.SprdCommand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Image = CType(resources.GetObject("cmdRefresh.Image"), System.Drawing.Image)
        Me.cmdRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdRefresh.Location = New System.Drawing.Point(4, 11)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(67, 37)
        Me.cmdRefresh.TabIndex = 10
        Me.cmdRefresh.Text = "&Show"
        Me.cmdRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdRefresh, "Refresh  Record")
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.Location = New System.Drawing.Point(206, 11)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(67, 37)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Ca&ncel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCancel, "Close the Form")
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(71, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
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
        Me.CmdPreview.Location = New System.Drawing.Point(139, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 3
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print  Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'chkExpand
        '
        Me.chkExpand.BackColor = System.Drawing.SystemColors.Control
        Me.chkExpand.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExpand.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExpand.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExpand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkExpand.Location = New System.Drawing.Point(631, 10)
        Me.chkExpand.Name = "chkExpand"
        Me.chkExpand.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExpand.Size = New System.Drawing.Size(115, 29)
        Me.chkExpand.TabIndex = 49
        Me.chkExpand.Text = "&Expand All"
        Me.ToolTip1.SetToolTip(Me.chkExpand, "Refresh  Record")
        Me.chkExpand.UseVisualStyleBackColor = False
        '
        'fraGrid
        '
        Me.fraGrid.BackColor = System.Drawing.SystemColors.Control
        Me.fraGrid.Controls.Add(Me.chkClosingStock)
        Me.fraGrid.Controls.Add(Me.chkOpeningStock)
        Me.fraGrid.Controls.Add(Me.chkExpand)
        Me.fraGrid.Controls.Add(Me.Frame3)
        Me.fraGrid.Controls.Add(Me.txtDiff)
        Me.fraGrid.Controls.Add(Me.chkOpening)
        Me.fraGrid.Controls.Add(Me.fraDate)
        Me.fraGrid.Controls.Add(Me.SprdView)
        Me.fraGrid.Controls.Add(Me.PicUp)
        Me.fraGrid.Controls.Add(Me.PicDown)
        Me.fraGrid.Controls.Add(Me.PicDash)
        Me.fraGrid.Controls.Add(Me.Report1)
        Me.fraGrid.Controls.Add(Me.lblDiff)
        Me.fraGrid.Controls.Add(Me.lblType)
        Me.fraGrid.Controls.Add(Me.FraMovement)
        Me.fraGrid.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraGrid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraGrid.Location = New System.Drawing.Point(0, 0)
        Me.fraGrid.Name = "fraGrid"
        Me.fraGrid.Padding = New System.Windows.Forms.Padding(0)
        Me.fraGrid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraGrid.Size = New System.Drawing.Size(908, 620)
        Me.fraGrid.TabIndex = 1
        Me.fraGrid.TabStop = False
        '
        'chkClosingStock
        '
        Me.chkClosingStock.AutoSize = True
        Me.chkClosingStock.BackColor = System.Drawing.SystemColors.Control
        Me.chkClosingStock.Checked = True
        Me.chkClosingStock.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkClosingStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkClosingStock.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkClosingStock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkClosingStock.Location = New System.Drawing.Point(752, 57)
        Me.chkClosingStock.Name = "chkClosingStock"
        Me.chkClosingStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkClosingStock.Size = New System.Drawing.Size(145, 18)
        Me.chkClosingStock.TabIndex = 51
        Me.chkClosingStock.Text = "Include Closing Stock"
        Me.chkClosingStock.UseVisualStyleBackColor = False
        '
        'chkOpeningStock
        '
        Me.chkOpeningStock.AutoSize = True
        Me.chkOpeningStock.BackColor = System.Drawing.SystemColors.Control
        Me.chkOpeningStock.Checked = True
        Me.chkOpeningStock.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOpeningStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOpeningStock.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOpeningStock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOpeningStock.Location = New System.Drawing.Point(752, 36)
        Me.chkOpeningStock.Name = "chkOpeningStock"
        Me.chkOpeningStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOpeningStock.Size = New System.Drawing.Size(149, 18)
        Me.chkOpeningStock.TabIndex = 50
        Me.chkOpeningStock.Text = "Include Opening Stock"
        Me.chkOpeningStock.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.lstCompanyName)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(141, -1)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(444, 76)
        Me.Frame3.TabIndex = 48
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Company Name"
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
        Me.lstCompanyName.Size = New System.Drawing.Size(444, 63)
        Me.lstCompanyName.TabIndex = 3
        '
        'txtDiff
        '
        Me.txtDiff.AcceptsReturn = True
        Me.txtDiff.BackColor = System.Drawing.SystemColors.Window
        Me.txtDiff.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDiff.Enabled = False
        Me.txtDiff.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiff.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDiff.Location = New System.Drawing.Point(344, 578)
        Me.txtDiff.MaxLength = 0
        Me.txtDiff.Name = "txtDiff"
        Me.txtDiff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDiff.Size = New System.Drawing.Size(105, 20)
        Me.txtDiff.TabIndex = 20
        '
        'chkOpening
        '
        Me.chkOpening.AutoSize = True
        Me.chkOpening.BackColor = System.Drawing.SystemColors.Control
        Me.chkOpening.Checked = True
        Me.chkOpening.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkOpening.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOpening.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOpening.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOpening.Location = New System.Drawing.Point(752, 12)
        Me.chkOpening.Name = "chkOpening"
        Me.chkOpening.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOpening.Size = New System.Drawing.Size(115, 18)
        Me.chkOpening.TabIndex = 19
        Me.chkOpening.Text = "Include Opening"
        Me.chkOpening.UseVisualStyleBackColor = False
        '
        'fraDate
        '
        Me.fraDate.BackColor = System.Drawing.SystemColors.Control
        Me.fraDate.Controls.Add(Me.txtDateTo)
        Me.fraDate.Controls.Add(Me.txtDateFrom)
        Me.fraDate.Controls.Add(Me.lblFrom)
        Me.fraDate.Controls.Add(Me.lblTo)
        Me.fraDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDate.Location = New System.Drawing.Point(0, 0)
        Me.fraDate.Name = "fraDate"
        Me.fraDate.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDate.Size = New System.Drawing.Size(138, 76)
        Me.fraDate.TabIndex = 14
        Me.fraDate.TabStop = False
        Me.fraDate.Text = "Date"
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(48, 45)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(80, 20)
        Me.txtDateTo.TabIndex = 18
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(48, 10)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(80, 20)
        Me.txtDateFrom.TabIndex = 17
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFrom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFrom.Location = New System.Drawing.Point(10, 14)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFrom.Size = New System.Drawing.Size(42, 14)
        Me.lblFrom.TabIndex = 16
        Me.lblFrom.Text = "From :"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTo.Location = New System.Drawing.Point(10, 47)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTo.Size = New System.Drawing.Size(26, 14)
        Me.lblTo.TabIndex = 15
        Me.lblTo.Text = "To :"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 82)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(904, 486)
        Me.SprdView.TabIndex = 0
        '
        'PicUp
        '
        Me.PicUp.BackColor = System.Drawing.SystemColors.Control
        Me.PicUp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PicUp.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicUp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PicUp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PicUp.Image = CType(resources.GetObject("PicUp.Image"), System.Drawing.Image)
        Me.PicUp.Location = New System.Drawing.Point(28, 594)
        Me.PicUp.Name = "PicUp"
        Me.PicUp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PicUp.Size = New System.Drawing.Size(29, 17)
        Me.PicUp.TabIndex = 13
        Me.PicUp.TabStop = False
        Me.PicUp.Visible = False
        '
        'PicDown
        '
        Me.PicDown.BackColor = System.Drawing.SystemColors.Control
        Me.PicDown.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PicDown.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicDown.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PicDown.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PicDown.Image = CType(resources.GetObject("PicDown.Image"), System.Drawing.Image)
        Me.PicDown.Location = New System.Drawing.Point(60, 578)
        Me.PicDown.Name = "PicDown"
        Me.PicDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PicDown.Size = New System.Drawing.Size(29, 17)
        Me.PicDown.TabIndex = 12
        Me.PicDown.TabStop = False
        Me.PicDown.Visible = False
        '
        'PicDash
        '
        Me.PicDash.BackColor = System.Drawing.SystemColors.Control
        Me.PicDash.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PicDash.Cursor = System.Windows.Forms.Cursors.Default
        Me.PicDash.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PicDash.ForeColor = System.Drawing.SystemColors.ControlText
        Me.PicDash.Location = New System.Drawing.Point(28, 578)
        Me.PicDash.Name = "PicDash"
        Me.PicDash.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.PicDash.Size = New System.Drawing.Size(29, 17)
        Me.PicDash.TabIndex = 11
        Me.PicDash.TabStop = False
        Me.PicDash.Visible = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(124, 154)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 21
        '
        'lblDiff
        '
        Me.lblDiff.AutoSize = True
        Me.lblDiff.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiff.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiff.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDiff.Location = New System.Drawing.Point(310, 580)
        Me.lblDiff.Name = "lblDiff"
        Me.lblDiff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiff.Size = New System.Drawing.Size(34, 14)
        Me.lblDiff.TabIndex = 21
        Me.lblDiff.Text = "Diff : "
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Location = New System.Drawing.Point(18, 270)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.Size = New System.Drawing.Size(0, 14)
        Me.lblType.TabIndex = 9
        Me.lblType.Visible = False
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.cmdCancel)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(631, 566)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(277, 51)
        Me.FraMovement.TabIndex = 2
        Me.FraMovement.TabStop = False
        '
        'FraPreview
        '
        Me.FraPreview.BackColor = System.Drawing.SystemColors.Control
        Me.FraPreview.Controls.Add(Me.SprdCommand)
        Me.FraPreview.Controls.Add(Me.SprdPreview)
        Me.FraPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPreview.Location = New System.Drawing.Point(0, 0)
        Me.FraPreview.Name = "FraPreview"
        Me.FraPreview.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPreview.Size = New System.Drawing.Size(908, 568)
        Me.FraPreview.TabIndex = 6
        Me.FraPreview.TabStop = False
        Me.FraPreview.Visible = False
        '
        'SprdCommand
        '
        Me.SprdCommand.DataSource = Nothing
        Me.SprdCommand.Location = New System.Drawing.Point(2, 10)
        Me.SprdCommand.Name = "SprdCommand"
        Me.SprdCommand.OcxState = CType(resources.GetObject("SprdCommand.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdCommand.Size = New System.Drawing.Size(885, 29)
        Me.SprdCommand.TabIndex = 7
        '
        'SprdPreview
        '
        Me.SprdPreview.Location = New System.Drawing.Point(2, 40)
        Me.SprdPreview.Name = "SprdPreview"
        Me.SprdPreview.OcxState = CType(resources.GetObject("SprdPreview.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPreview.Size = New System.Drawing.Size(904, 528)
        Me.SprdPreview.TabIndex = 8
        '
        'frmBalanceSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.fraGrid)
        Me.Controls.Add(Me.FraPreview)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBalanceSheet"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Balance Sheet"
        Me.fraGrid.ResumeLayout(False)
        Me.fraGrid.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.fraDate.ResumeLayout(False)
        Me.fraDate.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicDash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraPreview.ResumeLayout(False)
        CType(Me.SprdCommand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents Frame3 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
    Public WithEvents chkClosingStock As CheckBox
    Public WithEvents chkOpeningStock As CheckBox
    Public WithEvents chkExpand As Button
#End Region
End Class