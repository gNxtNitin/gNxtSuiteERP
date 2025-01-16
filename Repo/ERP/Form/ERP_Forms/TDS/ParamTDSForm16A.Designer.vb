<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamForm16A
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
		Me.MDIParent = TDS.Master
		TDS.Master.Show
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
	Public WithEvents LstSection As System.Windows.Forms.ListBox
	Public WithEvents FraSection As System.Windows.Forms.GroupBox
	Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents chkAllCerti As System.Windows.Forms.CheckBox
	Public WithEvents CmdSearchCNo As System.Windows.Forms.Button
	Public WithEvents txtCertificateNo As System.Windows.Forms.TextBox
	Public WithEvents FraOld As System.Windows.Forms.GroupBox
	Public WithEvents ChkAllParty As System.Windows.Forms.CheckBox
	Public WithEvents txtCustomer As System.Windows.Forms.TextBox
	Public WithEvents cmdSearch As System.Windows.Forms.Button
	Public WithEvents FraParty As System.Windows.Forms.GroupBox
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmParamForm16A))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.FraSection = New System.Windows.Forms.GroupBox
		Me.LstSection = New System.Windows.Forms.ListBox
		Me.Frame4 = New System.Windows.Forms.GroupBox
		Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox
		Me.txtDateTo = New System.Windows.Forms.MaskedTextBox
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Report1 = New AxCrystal.AxCrystalReport
		Me.FraOld = New System.Windows.Forms.GroupBox
		Me.chkAllCerti = New System.Windows.Forms.CheckBox
		Me.CmdSearchCNo = New System.Windows.Forms.Button
		Me.txtCertificateNo = New System.Windows.Forms.TextBox
		Me.FraParty = New System.Windows.Forms.GroupBox
		Me.ChkAllParty = New System.Windows.Forms.CheckBox
		Me.txtCustomer = New System.Windows.Forms.TextBox
		Me.cmdSearch = New System.Windows.Forms.Button
		Me.Frame5 = New System.Windows.Forms.GroupBox
		Me.cmdPrint = New System.Windows.Forms.Button
		Me.cmdPreview = New System.Windows.Forms.Button
		Me.cmdCancel = New System.Windows.Forms.Button
		Me.FraSection.SuspendLayout()
		Me.Frame4.SuspendLayout()
		Me.FraOld.SuspendLayout()
		Me.FraParty.SuspendLayout()
		Me.Frame5.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Text = "TDS Form 16A"
		Me.ClientSize = New System.Drawing.Size(393, 281)
		Me.Location = New System.Drawing.Point(4, 23)
		Me.Icon = CType(resources.GetObject("frmParamForm16A.Icon"), System.Drawing.Icon)
		Me.MaximizeBox = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "frmParamForm16A"
		Me.FraSection.Text = "Section"
		Me.FraSection.Size = New System.Drawing.Size(199, 105)
		Me.FraSection.Location = New System.Drawing.Point(0, 44)
		Me.FraSection.TabIndex = 12
		Me.FraSection.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraSection.BackColor = System.Drawing.SystemColors.Control
		Me.FraSection.Enabled = True
		Me.FraSection.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraSection.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraSection.Visible = True
		Me.FraSection.Padding = New System.Windows.Forms.Padding(0)
		Me.FraSection.Name = "FraSection"
		Me.LstSection.Size = New System.Drawing.Size(193, 90)
		Me.LstSection.IntegralHeight = False
		Me.LstSection.Location = New System.Drawing.Point(2, 14)
		Me.LstSection.TabIndex = 2
		Me.LstSection.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.LstSection.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.LstSection.BackColor = System.Drawing.SystemColors.Window
		Me.LstSection.CausesValidation = True
		Me.LstSection.Enabled = True
		Me.LstSection.ForeColor = System.Drawing.SystemColors.WindowText
		Me.LstSection.Cursor = System.Windows.Forms.Cursors.Default
		Me.LstSection.SelectionMode = System.Windows.Forms.SelectionMode.One
		Me.LstSection.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.LstSection.Sorted = False
		Me.LstSection.TabStop = True
		Me.LstSection.Visible = True
		Me.LstSection.MultiColumn = False
		Me.LstSection.Name = "LstSection"
		Me.Frame4.Text = "Payment Date"
		Me.Frame4.ForeColor = System.Drawing.Color.FromARGB(128, 0, 0)
		Me.Frame4.Size = New System.Drawing.Size(393, 43)
		Me.Frame4.Location = New System.Drawing.Point(0, 0)
		Me.Frame4.TabIndex = 9
		Me.Frame4.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame4.BackColor = System.Drawing.SystemColors.Control
		Me.Frame4.Enabled = True
		Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame4.Visible = True
		Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame4.Name = "Frame4"
		Me.txtDateFrom.AllowPromptAsInput = False
		Me.txtDateFrom.Size = New System.Drawing.Size(83, 21)
		Me.txtDateFrom.Location = New System.Drawing.Point(88, 14)
		Me.txtDateFrom.TabIndex = 0
		Me.txtDateFrom.MaxLength = 10
		Me.txtDateFrom.Mask = "##/##/####"
		Me.txtDateFrom.PromptChar = "_"
		Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateFrom.Name = "txtDateFrom"
		Me.txtDateTo.AllowPromptAsInput = False
		Me.txtDateTo.Size = New System.Drawing.Size(83, 21)
		Me.txtDateTo.Location = New System.Drawing.Point(300, 14)
		Me.txtDateTo.TabIndex = 1
		Me.txtDateTo.MaxLength = 10
		Me.txtDateTo.Mask = "##/##/####"
		Me.txtDateTo.PromptChar = "_"
		Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtDateTo.Name = "txtDateTo"
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label4.Text = "From : "
		Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.Size = New System.Drawing.Size(40, 13)
		Me.Label4.Location = New System.Drawing.Point(49, 18)
		Me.Label4.TabIndex = 11
		Me.Label4.BackColor = System.Drawing.SystemColors.Control
		Me.Label4.Enabled = True
		Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label4.UseMnemonic = True
		Me.Label4.Visible = True
		Me.Label4.AutoSize = True
		Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label4.Name = "Label4"
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
		Me.Label3.Text = "To : "
		Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Size = New System.Drawing.Size(28, 13)
		Me.Label3.Location = New System.Drawing.Point(273, 18)
		Me.Label3.TabIndex = 10
		Me.Label3.BackColor = System.Drawing.SystemColors.Control
		Me.Label3.Enabled = True
		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label3.UseMnemonic = True
		Me.Label3.Visible = True
		Me.Label3.AutoSize = True
		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label3.Name = "Label3"
		Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Report1.Location = New System.Drawing.Point(0, 0)
		Me.Report1.Name = "Report1"
		Me.FraOld.Text = "CertificateNo"
		Me.FraOld.Size = New System.Drawing.Size(393, 43)
		Me.FraOld.Location = New System.Drawing.Point(0, 150)
		Me.FraOld.TabIndex = 14
		Me.FraOld.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraOld.BackColor = System.Drawing.SystemColors.Control
		Me.FraOld.Enabled = True
		Me.FraOld.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraOld.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraOld.Visible = True
		Me.FraOld.Padding = New System.Windows.Forms.Padding(0)
		Me.FraOld.Name = "FraOld"
		Me.chkAllCerti.Text = "ALL"
		Me.chkAllCerti.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.chkAllCerti.Size = New System.Drawing.Size(45, 13)
		Me.chkAllCerti.Location = New System.Drawing.Point(338, 18)
		Me.chkAllCerti.TabIndex = 18
		Me.chkAllCerti.CheckState = System.Windows.Forms.CheckState.Checked
		Me.chkAllCerti.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.chkAllCerti.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.chkAllCerti.BackColor = System.Drawing.SystemColors.Control
		Me.chkAllCerti.CausesValidation = True
		Me.chkAllCerti.Enabled = True
		Me.chkAllCerti.ForeColor = System.Drawing.SystemColors.ControlText
		Me.chkAllCerti.Cursor = System.Windows.Forms.Cursors.Default
		Me.chkAllCerti.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.chkAllCerti.Appearance = System.Windows.Forms.Appearance.Normal
		Me.chkAllCerti.TabStop = True
		Me.chkAllCerti.Visible = True
		Me.chkAllCerti.Name = "chkAllCerti"
		Me.CmdSearchCNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.CmdSearchCNo.BackColor = System.Drawing.SystemColors.Menu
		Me.CmdSearchCNo.Enabled = False
		Me.CmdSearchCNo.Size = New System.Drawing.Size(21, 19)
		Me.CmdSearchCNo.Location = New System.Drawing.Point(310, 16)
		Me.CmdSearchCNo.Image = CType(resources.GetObject("CmdSearchCNo.Image"), System.Drawing.Image)
		Me.CmdSearchCNo.TabIndex = 16
		Me.CmdSearchCNo.TabStop = False
		Me.ToolTip1.SetToolTip(Me.CmdSearchCNo, "Search Supplier")
		Me.CmdSearchCNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CmdSearchCNo.CausesValidation = True
		Me.CmdSearchCNo.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CmdSearchCNo.Cursor = System.Windows.Forms.Cursors.Default
		Me.CmdSearchCNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CmdSearchCNo.Name = "CmdSearchCNo"
		Me.txtCertificateNo.AutoSize = False
		Me.txtCertificateNo.Enabled = False
		Me.txtCertificateNo.ForeColor = System.Drawing.Color.Blue
		Me.txtCertificateNo.Size = New System.Drawing.Size(295, 19)
		Me.txtCertificateNo.Location = New System.Drawing.Point(14, 16)
		Me.txtCertificateNo.TabIndex = 15
		Me.txtCertificateNo.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtCertificateNo.AcceptsReturn = True
		Me.txtCertificateNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtCertificateNo.BackColor = System.Drawing.SystemColors.Window
		Me.txtCertificateNo.CausesValidation = True
		Me.txtCertificateNo.HideSelection = True
		Me.txtCertificateNo.ReadOnly = False
		Me.txtCertificateNo.Maxlength = 0
		Me.txtCertificateNo.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCertificateNo.MultiLine = False
		Me.txtCertificateNo.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCertificateNo.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtCertificateNo.TabStop = True
		Me.txtCertificateNo.Visible = True
		Me.txtCertificateNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtCertificateNo.Name = "txtCertificateNo"
		Me.FraParty.Text = "Party"
		Me.FraParty.Size = New System.Drawing.Size(393, 43)
		Me.FraParty.Location = New System.Drawing.Point(0, 192)
		Me.FraParty.TabIndex = 13
		Me.FraParty.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.FraParty.BackColor = System.Drawing.SystemColors.Control
		Me.FraParty.Enabled = True
		Me.FraParty.ForeColor = System.Drawing.SystemColors.ControlText
		Me.FraParty.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.FraParty.Visible = True
		Me.FraParty.Padding = New System.Windows.Forms.Padding(0)
		Me.FraParty.Name = "FraParty"
		Me.ChkAllParty.Text = "ALL"
		Me.ChkAllParty.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ChkAllParty.Size = New System.Drawing.Size(45, 13)
		Me.ChkAllParty.Location = New System.Drawing.Point(338, 20)
		Me.ChkAllParty.TabIndex = 17
		Me.ChkAllParty.CheckState = System.Windows.Forms.CheckState.Checked
		Me.ChkAllParty.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.ChkAllParty.FlatStyle = System.Windows.Forms.FlatStyle.Standard
		Me.ChkAllParty.BackColor = System.Drawing.SystemColors.Control
		Me.ChkAllParty.CausesValidation = True
		Me.ChkAllParty.Enabled = True
		Me.ChkAllParty.ForeColor = System.Drawing.SystemColors.ControlText
		Me.ChkAllParty.Cursor = System.Windows.Forms.Cursors.Default
		Me.ChkAllParty.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ChkAllParty.Appearance = System.Windows.Forms.Appearance.Normal
		Me.ChkAllParty.TabStop = True
		Me.ChkAllParty.Visible = True
		Me.ChkAllParty.Name = "ChkAllParty"
		Me.txtCustomer.AutoSize = False
		Me.txtCustomer.ForeColor = System.Drawing.Color.Blue
		Me.txtCustomer.Size = New System.Drawing.Size(295, 19)
		Me.txtCustomer.Location = New System.Drawing.Point(14, 16)
		Me.txtCustomer.TabIndex = 3
		Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.txtCustomer.AcceptsReturn = True
		Me.txtCustomer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
		Me.txtCustomer.CausesValidation = True
		Me.txtCustomer.Enabled = True
		Me.txtCustomer.HideSelection = True
		Me.txtCustomer.ReadOnly = False
		Me.txtCustomer.Maxlength = 0
		Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCustomer.MultiLine = False
		Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCustomer.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtCustomer.TabStop = True
		Me.txtCustomer.Visible = True
		Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.txtCustomer.Name = "txtCustomer"
		Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
		Me.cmdSearch.Size = New System.Drawing.Size(25, 19)
		Me.cmdSearch.Location = New System.Drawing.Point(310, 16)
		Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
		Me.cmdSearch.TabIndex = 4
		Me.cmdSearch.TabStop = False
		Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search Supplier")
		Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdSearch.CausesValidation = True
		Me.cmdSearch.Enabled = True
		Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdSearch.Name = "cmdSearch"
		Me.Frame5.Size = New System.Drawing.Size(393, 51)
		Me.Frame5.Location = New System.Drawing.Point(0, 230)
		Me.Frame5.TabIndex = 8
		Me.Frame5.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame5.BackColor = System.Drawing.SystemColors.Control
		Me.Frame5.Enabled = True
		Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame5.Visible = True
		Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
		Me.Frame5.Name = "Frame5"
		Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdPrint.Text = "&Printer"
		Me.cmdPrint.Size = New System.Drawing.Size(80, 37)
		Me.cmdPrint.Location = New System.Drawing.Point(4, 10)
		Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
		Me.cmdPrint.TabIndex = 5
		Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print Report")
		Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPrint.CausesValidation = True
		Me.cmdPrint.Enabled = True
		Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPrint.TabStop = True
		Me.cmdPrint.Name = "cmdPrint"
		Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdPreview.Text = "Pre&view"
		Me.AcceptButton = Me.cmdPreview
		Me.cmdPreview.Size = New System.Drawing.Size(80, 37)
		Me.cmdPreview.Location = New System.Drawing.Point(150, 10)
		Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
		Me.cmdPreview.TabIndex = 6
		Me.ToolTip1.SetToolTip(Me.cmdPreview, "Preview Report")
		Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
		Me.cmdPreview.CausesValidation = True
		Me.cmdPreview.Enabled = True
		Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdPreview.TabStop = True
		Me.cmdPreview.Name = "cmdPreview"
		Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
		Me.cmdCancel.Text = "&Close"
		Me.cmdCancel.Size = New System.Drawing.Size(80, 37)
		Me.cmdCancel.Location = New System.Drawing.Point(310, 10)
		Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
		Me.cmdCancel.TabIndex = 7
		Me.ToolTip1.SetToolTip(Me.cmdCancel, "Close")
		Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
		Me.cmdCancel.CausesValidation = True
		Me.cmdCancel.Enabled = True
		Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
		Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
		Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cmdCancel.TabStop = True
		Me.cmdCancel.Name = "cmdCancel"
		CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(FraSection)
		Me.Controls.Add(Frame4)
		Me.Controls.Add(Report1)
		Me.Controls.Add(FraOld)
		Me.Controls.Add(FraParty)
		Me.Controls.Add(Frame5)
		Me.FraSection.Controls.Add(LstSection)
		Me.Frame4.Controls.Add(txtDateFrom)
		Me.Frame4.Controls.Add(txtDateTo)
		Me.Frame4.Controls.Add(Label4)
		Me.Frame4.Controls.Add(Label3)
		Me.FraOld.Controls.Add(chkAllCerti)
		Me.FraOld.Controls.Add(CmdSearchCNo)
		Me.FraOld.Controls.Add(txtCertificateNo)
		Me.FraParty.Controls.Add(ChkAllParty)
		Me.FraParty.Controls.Add(txtCustomer)
		Me.FraParty.Controls.Add(cmdSearch)
		Me.Frame5.Controls.Add(cmdPrint)
		Me.Frame5.Controls.Add(cmdPreview)
		Me.Frame5.Controls.Add(cmdCancel)
		Me.FraSection.ResumeLayout(False)
		Me.Frame4.ResumeLayout(False)
		Me.FraOld.ResumeLayout(False)
		Me.FraParty.ResumeLayout(False)
		Me.Frame5.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class