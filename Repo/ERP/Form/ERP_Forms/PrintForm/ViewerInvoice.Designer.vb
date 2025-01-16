Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewerInvoice
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
	Public WithEvents mnuCustomToolbar As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuseperator As System.Windows.Forms.ToolStripSeparator
	Public WithEvents mnuCRToolbar As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuDisplayGroupTree As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuView As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
	Public WithEvents MainMenu1 As System.Windows.Forms.MenuStrip
	Public WithEvents cboZoom As System.Windows.Forms.ComboBox
	Public WithEvents txtCurPage As System.Windows.Forms.TextBox
	Public WithEvents cboSearch As System.Windows.Forms.ComboBox
	Public WithEvents Toolbar As AxComctlLib.AxToolbar
    Public WithEvents CRViewer1 As CRVIEWER9Lib.CRViewer9    ''AxCRVIEWERLib.AxCRViewer
    Public WithEvents StatusBar As AxComctlLib.AxStatusBar
	Public WithEvents ImgLst As AxComctlLib.AxImageList
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmViewerInvoice))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.MainMenu1 = New System.Windows.Forms.MenuStrip
		Me.mnuView = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuCustomToolbar = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuseperator = New System.Windows.Forms.ToolStripSeparator
		Me.mnuCRToolbar = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuDisplayGroupTree = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem
		Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem
		Me.Toolbar = New AxComctlLib.AxToolbar
		Me.cboZoom = New System.Windows.Forms.ComboBox
		Me.txtCurPage = New System.Windows.Forms.TextBox
		Me.cboSearch = New System.Windows.Forms.ComboBox
        Me.CRViewer1 = New CRVIEWER9Lib.CRViewer9   ''AxCRVIEWERLib.AxCRViewer
        Me.StatusBar = New AxComctlLib.AxStatusBar
		Me.ImgLst = New AxComctlLib.AxImageList
		Me.MainMenu1.SuspendLayout()
		Me.Toolbar.SuspendLayout()
		Me.SuspendLayout()
		Me.ToolTip1.Active = True
		CType(Me.Toolbar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CRViewer1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StatusBar, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.ImgLst, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
		Me.Text = "Viewer"
		Me.ClientSize = New System.Drawing.Size(690, 534)
		Me.Location = New System.Drawing.Point(146, 181)
		Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MaximizeBox = True
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.Name = "frmViewerInvoice"
		Me.mnuView.Name = "mnuView"
		Me.mnuView.Text = "View"
		Me.mnuView.Checked = False
		Me.mnuView.Enabled = True
		Me.mnuView.Visible = True
		Me.mnuCustomToolbar.Name = "mnuCustomToolbar"
		Me.mnuCustomToolbar.Text = "Display Custom Toolbar"
		Me.mnuCustomToolbar.Checked = True
		Me.mnuCustomToolbar.Enabled = True
		Me.mnuCustomToolbar.Visible = True
		Me.mnuseperator.Enabled = True
		Me.mnuseperator.Visible = True
		Me.mnuseperator.Name = "mnuseperator"
		Me.mnuCRToolbar.Name = "mnuCRToolbar"
		Me.mnuCRToolbar.Text = "Display Crystal Toolbar"
		Me.mnuCRToolbar.Checked = True
		Me.mnuCRToolbar.Enabled = True
		Me.mnuCRToolbar.Visible = True
		Me.mnuDisplayGroupTree.Name = "mnuDisplayGroupTree"
		Me.mnuDisplayGroupTree.Text = "Display Group Tree"
		Me.mnuDisplayGroupTree.Checked = True
		Me.mnuDisplayGroupTree.Enabled = True
		Me.mnuDisplayGroupTree.Visible = True
		Me.mnuHelp.Name = "mnuHelp"
		Me.mnuHelp.Text = "Help"
		Me.mnuHelp.Checked = False
		Me.mnuHelp.Enabled = True
		Me.mnuHelp.Visible = True
		Me.mnuAbout.Name = "mnuAbout"
		Me.mnuAbout.Text = "About"
		Me.mnuAbout.Checked = False
		Me.mnuAbout.Enabled = True
		Me.mnuAbout.Visible = True
		Toolbar.OcxState = CType(resources.GetObject("Toolbar.OcxState"), System.Windows.Forms.AxHost.State)
		Me.Toolbar.Dock = System.Windows.Forms.DockStyle.Top
		Me.Toolbar.Size = New System.Drawing.Size(690, 28)
		Me.Toolbar.Location = New System.Drawing.Point(0, 24)
		Me.Toolbar.TabIndex = 1
		Me.Toolbar.Name = "Toolbar"
		Me.cboZoom.Size = New System.Drawing.Size(56, 21)
		Me.cboZoom.Location = New System.Drawing.Point(310, 3)
		Me.cboZoom.TabIndex = 4
		Me.cboZoom.BackColor = System.Drawing.SystemColors.Window
		Me.cboZoom.CausesValidation = True
		Me.cboZoom.Enabled = True
		Me.cboZoom.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboZoom.IntegralHeight = True
		Me.cboZoom.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboZoom.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboZoom.Sorted = False
		Me.cboZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
		Me.cboZoom.TabStop = True
		Me.cboZoom.Visible = True
		Me.cboZoom.Name = "cboZoom"
		Me.txtCurPage.AutoSize = False
		Me.txtCurPage.Size = New System.Drawing.Size(25, 22)
		Me.txtCurPage.Location = New System.Drawing.Point(72, 3)
		Me.txtCurPage.TabIndex = 3
		Me.txtCurPage.Text = "0"
		Me.txtCurPage.AcceptsReturn = True
		Me.txtCurPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.txtCurPage.BackColor = System.Drawing.SystemColors.Window
		Me.txtCurPage.CausesValidation = True
		Me.txtCurPage.Enabled = True
		Me.txtCurPage.ForeColor = System.Drawing.SystemColors.WindowText
		Me.txtCurPage.HideSelection = True
		Me.txtCurPage.ReadOnly = False
		Me.txtCurPage.Maxlength = 0
		Me.txtCurPage.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.txtCurPage.MultiLine = False
		Me.txtCurPage.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.txtCurPage.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.txtCurPage.TabStop = True
		Me.txtCurPage.Visible = True
		Me.txtCurPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.txtCurPage.Name = "txtCurPage"
		Me.cboSearch.Size = New System.Drawing.Size(77, 21)
		Me.cboSearch.Location = New System.Drawing.Point(376, 3)
		Me.cboSearch.TabIndex = 2
		Me.cboSearch.BackColor = System.Drawing.SystemColors.Window
		Me.cboSearch.CausesValidation = True
		Me.cboSearch.Enabled = True
		Me.cboSearch.ForeColor = System.Drawing.SystemColors.WindowText
		Me.cboSearch.IntegralHeight = True
		Me.cboSearch.Cursor = System.Windows.Forms.Cursors.Default
		Me.cboSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.cboSearch.Sorted = False
		Me.cboSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
		Me.cboSearch.TabStop = True
		Me.cboSearch.Visible = True
		Me.cboSearch.Name = "cboSearch"
        'CRViewer1.OcxState = CType(resources.GetObject("CRViewer1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.CRViewer1.Size = New System.Drawing.Size(690, 465)
        Me.CRViewer1.Location = New System.Drawing.Point(-1, 51)
        Me.CRViewer1.TabIndex = 5
        Me.CRViewer1.Name = "CRViewer1"
        StatusBar.OcxState = CType(resources.GetObject("StatusBar.OcxState"), System.Windows.Forms.AxHost.State)
		Me.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.StatusBar.Size = New System.Drawing.Size(690, 19)
		Me.StatusBar.Location = New System.Drawing.Point(0, 515)
		Me.StatusBar.TabIndex = 0
		Me.StatusBar.Name = "StatusBar"
		ImgLst.OcxState = CType(resources.GetObject("ImgLst.OcxState"), System.Windows.Forms.AxHost.State)
		Me.ImgLst.Location = New System.Drawing.Point(524, 396)
		Me.ImgLst.Name = "ImgLst"
		CType(Me.ImgLst, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.StatusBar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CRViewer1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Toolbar, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Controls.Add(Toolbar)
		Me.Controls.Add(CRViewer1)
		Me.Controls.Add(StatusBar)
		Me.Controls.Add(ImgLst)
		Me.Toolbar.Controls.Add(cboZoom)
		Me.Toolbar.Controls.Add(txtCurPage)
		Me.Toolbar.Controls.Add(cboSearch)
		MainMenu1.Items.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuView, Me.mnuHelp})
		mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuCustomToolbar, Me.mnuseperator, Me.mnuCRToolbar, Me.mnuDisplayGroupTree})
		mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem(){Me.mnuAbout})
		Me.Controls.Add(MainMenu1)
		Me.MainMenu1.ResumeLayout(False)
		Me.Toolbar.ResumeLayout(False)
		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region 
End Class