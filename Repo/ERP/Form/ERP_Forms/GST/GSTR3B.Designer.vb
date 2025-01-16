Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGSTR3B
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
        'Me.MDIParent = SalesGST.Master
        'SalesGST.Master.Show()
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
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents txtRegnNo As System.Windows.Forms.TextBox
    Public WithEvents txtAddress As System.Windows.Forms.TextBox
    Public WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Public WithEvents lblTile As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents SprdView3 As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SprdView4 As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents SprdView5 As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents SprdView6 As AxFPSpreadADO.AxfpSpread
    Public WithEvents lbl4 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents SprdView7 As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents SprdView8 As AxFPSpreadADO.AxfpSpread
    Public WithEvents lbl5A As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents _SSTab1_TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents _SSTab1_TabPage7 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
    Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
    Public CommonDialog1Font As System.Windows.Forms.FontDialog
    Public CommonDialog1Color As System.Windows.Forms.ColorDialog
    Public CommonDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdCreateCD As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblFormType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGSTR3B))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdCreateCD = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me.txtRegnNo = New System.Windows.Forms.TextBox()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.lblTile = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdView3 = New AxFPSpreadADO.AxfpSpread()
        Me.Label9 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.SprdView4 = New AxFPSpreadADO.AxfpSpread()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.SprdView5 = New AxFPSpreadADO.AxfpSpread()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdView6 = New AxFPSpreadADO.AxfpSpread()
        Me.lbl4 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.SprdView7 = New AxFPSpreadADO.AxfpSpread()
        Me.Label5 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage6 = New System.Windows.Forms.TabPage()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.SprdView8 = New AxFPSpreadADO.AxfpSpread()
        Me.lbl5A = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage7 = New System.Windows.Forms.TabPage()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblFormType = New System.Windows.Forms.Label()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.cboGSTNO = New System.Windows.Forms.ComboBox()
        Me._Lbl_7 = New System.Windows.Forms.Label()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me._SSTab1_TabPage1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage2.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.SprdView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage3.SuspendLayout()
        Me.Frame7.SuspendLayout()
        CType(Me.SprdView5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage4.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage5.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.SprdView7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage6.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.SprdView8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(686, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 9
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
        Me.CmdPreview.Location = New System.Drawing.Point(627, 11)
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
        Me.cmdPrint.Location = New System.Drawing.Point(567, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 7
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdCreateCD
        '
        Me.cmdCreateCD.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCreateCD.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCreateCD.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCreateCD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCreateCD.Image = CType(resources.GetObject("cmdCreateCD.Image"), System.Drawing.Image)
        Me.cmdCreateCD.Location = New System.Drawing.Point(508, 11)
        Me.cmdCreateCD.Name = "cmdCreateCD"
        Me.cmdCreateCD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCreateCD.Size = New System.Drawing.Size(60, 37)
        Me.cmdCreateCD.TabIndex = 6
        Me.cmdCreateCD.Text = "Create CD"
        Me.cmdCreateCD.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCreateCD, "Show Record")
        Me.cmdCreateCD.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(448, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 22
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage5)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage6)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage7)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(0, 0)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 1
        Me.SSTab1.Size = New System.Drawing.Size(751, 411)
        Me.SSTab1.TabIndex = 11
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.Frame1)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(743, 385)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Tab 0"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboGSTNO)
        Me.Frame1.Controls.Add(Me._Lbl_7)
        Me.Frame1.Controls.Add(Me.Frame9)
        Me.Frame1.Controls.Add(Me.txtRegnNo)
        Me.Frame1.Controls.Add(Me.txtAddress)
        Me.Frame1.Controls.Add(Me.txtCompanyName)
        Me.Frame1.Controls.Add(Me.lblTile)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(743, 385)
        Me.Frame1.TabIndex = 12
        Me.Frame1.TabStop = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.txtDateFrom)
        Me.Frame9.Controls.Add(Me.txtDateTo)
        Me.Frame9.Controls.Add(Me._Lbl_0)
        Me.Frame9.Controls.Add(Me._Lbl_1)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(132, 72)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(435, 61)
        Me.Frame9.TabIndex = 13
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Date Range"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(94, 24)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(89, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(284, 24)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(89, 20)
        Me.txtDateTo.TabIndex = 1
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(54, 28)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(37, 14)
        Me._Lbl_0.TabIndex = 15
        Me._Lbl_0.Text = "From :"
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(256, 28)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(24, 14)
        Me._Lbl_1.TabIndex = 14
        Me._Lbl_1.Text = "To :"
        '
        'txtRegnNo
        '
        Me.txtRegnNo.AcceptsReturn = True
        Me.txtRegnNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRegnNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegnNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegnNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegnNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRegnNo.Location = New System.Drawing.Point(229, 201)
        Me.txtRegnNo.MaxLength = 0
        Me.txtRegnNo.Name = "txtRegnNo"
        Me.txtRegnNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegnNo.Size = New System.Drawing.Size(441, 20)
        Me.txtRegnNo.TabIndex = 4
        '
        'txtAddress
        '
        Me.txtAddress.AcceptsReturn = True
        Me.txtAddress.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddress.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddress.Location = New System.Drawing.Point(229, 241)
        Me.txtAddress.MaxLength = 0
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddress.Size = New System.Drawing.Size(441, 57)
        Me.txtAddress.TabIndex = 3
        '
        'txtCompanyName
        '
        Me.txtCompanyName.AcceptsReturn = True
        Me.txtCompanyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompanyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompanyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCompanyName.Location = New System.Drawing.Point(229, 221)
        Me.txtCompanyName.MaxLength = 0
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompanyName.Size = New System.Drawing.Size(441, 20)
        Me.txtCompanyName.TabIndex = 2
        '
        'lblTile
        '
        Me.lblTile.BackColor = System.Drawing.SystemColors.Control
        Me.lblTile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTile.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTile.Location = New System.Drawing.Point(2, 8)
        Me.lblTile.Name = "lblTile"
        Me.lblTile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTile.Size = New System.Drawing.Size(737, 51)
        Me.lblTile.TabIndex = 18
        Me.lblTile.Text = "Title"
        Me.lblTile.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(136, 203)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(58, 14)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "1.  GSTIN :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(136, 225)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(81, 14)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "2. Legal Name :"
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.Frame2)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(743, 385)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Tab 1"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdView3)
        Me.Frame2.Controls.Add(Me.Label9)
        Me.Frame2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(743, 385)
        Me.Frame2.TabIndex = 19
        Me.Frame2.TabStop = False
        '
        'SprdView3
        '
        Me.SprdView3.DataSource = Nothing
        Me.SprdView3.Location = New System.Drawing.Point(2, 32)
        Me.SprdView3.Name = "SprdView3"
        Me.SprdView3.OcxState = CType(resources.GetObject("SprdView3.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView3.Size = New System.Drawing.Size(737, 347)
        Me.SprdView3.TabIndex = 20
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(4, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(382, 14)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "3.1 Details of outward Supplies and inward supplies liable to reverse charge :"
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.Frame6)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(743, 385)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "Tab 2"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.SprdView4)
        Me.Frame6.Controls.Add(Me.Label1)
        Me.Frame6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(743, 385)
        Me.Frame6.TabIndex = 29
        Me.Frame6.TabStop = False
        '
        'SprdView4
        '
        Me.SprdView4.DataSource = Nothing
        Me.SprdView4.Location = New System.Drawing.Point(2, 43)
        Me.SprdView4.Name = "SprdView4"
        Me.SprdView4.OcxState = CType(resources.GetObject("SprdView4.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView4.Size = New System.Drawing.Size(737, 339)
        Me.SprdView4.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(734, 14)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "3.2 Of the supplies shown in 3.1 (a) above, details of inter-State supplies made " &
    "to unregistered persons, composition taxablepersons and UIN holders :"
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.Frame7)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(743, 385)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "Tab 3"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.SprdView5)
        Me.Frame7.Controls.Add(Me.Label2)
        Me.Frame7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(0, 0)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(743, 385)
        Me.Frame7.TabIndex = 32
        Me.Frame7.TabStop = False
        '
        'SprdView5
        '
        Me.SprdView5.DataSource = Nothing
        Me.SprdView5.Location = New System.Drawing.Point(2, 32)
        Me.SprdView5.Name = "SprdView5"
        Me.SprdView5.OcxState = CType(resources.GetObject("SprdView5.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView5.Size = New System.Drawing.Size(737, 347)
        Me.SprdView5.TabIndex = 33
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(75, 14)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "4. Eligible ITC :"
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me.Frame4)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(743, 385)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "Tab 4"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdView6)
        Me.Frame4.Controls.Add(Me.lbl4)
        Me.Frame4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(743, 385)
        Me.Frame4.TabIndex = 26
        Me.Frame4.TabStop = False
        '
        'SprdView6
        '
        Me.SprdView6.DataSource = Nothing
        Me.SprdView6.Location = New System.Drawing.Point(2, 32)
        Me.SprdView6.Name = "SprdView6"
        Me.SprdView6.OcxState = CType(resources.GetObject("SprdView6.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView6.Size = New System.Drawing.Size(737, 347)
        Me.SprdView6.TabIndex = 27
        '
        'lbl4
        '
        Me.lbl4.AutoSize = True
        Me.lbl4.BackColor = System.Drawing.SystemColors.Control
        Me.lbl4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbl4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl4.Location = New System.Drawing.Point(8, 12)
        Me.lbl4.Name = "lbl4"
        Me.lbl4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbl4.Size = New System.Drawing.Size(301, 14)
        Me.lbl4.TabIndex = 28
        Me.lbl4.Text = "5. Values of exempt, nil-rated and non-GST inward supplies :"
        '
        '_SSTab1_TabPage5
        '
        Me._SSTab1_TabPage5.Controls.Add(Me.Frame8)
        Me._SSTab1_TabPage5.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage5.Name = "_SSTab1_TabPage5"
        Me._SSTab1_TabPage5.Size = New System.Drawing.Size(743, 385)
        Me._SSTab1_TabPage5.TabIndex = 5
        Me._SSTab1_TabPage5.Text = "Tab 5"
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.SprdView7)
        Me.Frame8.Controls.Add(Me.Label5)
        Me.Frame8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 0)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(743, 385)
        Me.Frame8.TabIndex = 35
        Me.Frame8.TabStop = False
        '
        'SprdView7
        '
        Me.SprdView7.DataSource = Nothing
        Me.SprdView7.Location = New System.Drawing.Point(2, 32)
        Me.SprdView7.Name = "SprdView7"
        Me.SprdView7.OcxState = CType(resources.GetObject("SprdView7.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView7.Size = New System.Drawing.Size(737, 347)
        Me.SprdView7.TabIndex = 36
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(103, 14)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "6.1 Payment of tax :"
        '
        '_SSTab1_TabPage6
        '
        Me._SSTab1_TabPage6.Controls.Add(Me.Frame5)
        Me._SSTab1_TabPage6.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage6.Name = "_SSTab1_TabPage6"
        Me._SSTab1_TabPage6.Size = New System.Drawing.Size(743, 385)
        Me._SSTab1_TabPage6.TabIndex = 6
        Me._SSTab1_TabPage6.Text = "Tab 6"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.SprdView8)
        Me.Frame5.Controls.Add(Me.lbl5A)
        Me.Frame5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(743, 385)
        Me.Frame5.TabIndex = 23
        Me.Frame5.TabStop = False
        '
        'SprdView8
        '
        Me.SprdView8.DataSource = Nothing
        Me.SprdView8.Location = New System.Drawing.Point(2, 32)
        Me.SprdView8.Name = "SprdView8"
        Me.SprdView8.OcxState = CType(resources.GetObject("SprdView8.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView8.Size = New System.Drawing.Size(737, 347)
        Me.SprdView8.TabIndex = 24
        '
        'lbl5A
        '
        Me.lbl5A.AutoSize = True
        Me.lbl5A.BackColor = System.Drawing.SystemColors.Control
        Me.lbl5A.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbl5A.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl5A.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl5A.Location = New System.Drawing.Point(8, 16)
        Me.lbl5A.Name = "lbl5A"
        Me.lbl5A.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbl5A.Size = New System.Drawing.Size(108, 14)
        Me.lbl5A.TabIndex = 25
        Me.lbl5A.Text = "6.2 TDS/ TCS Credit :"
        '
        '_SSTab1_TabPage7
        '
        Me._SSTab1_TabPage7.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage7.Name = "_SSTab1_TabPage7"
        Me._SSTab1_TabPage7.Size = New System.Drawing.Size(743, 385)
        Me._SSTab1_TabPage7.TabIndex = 7
        Me._SSTab1_TabPage7.Text = "Tab 7"
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdCreateCD)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.lblFormType)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 406)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(751, 51)
        Me.FraMovement.TabIndex = 5
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(280, 10)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 23
        '
        'lblFormType
        '
        Me.lblFormType.AutoSize = True
        Me.lblFormType.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFormType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFormType.Location = New System.Drawing.Point(368, 14)
        Me.lblFormType.Name = "lblFormType"
        Me.lblFormType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFormType.Size = New System.Drawing.Size(64, 14)
        Me.lblFormType.TabIndex = 10
        Me.lblFormType.Text = "lblFormType"
        Me.lblFormType.Visible = False
        '
        'cboGSTNO
        '
        Me.cboGSTNO.BackColor = System.Drawing.SystemColors.Window
        Me.cboGSTNO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGSTNO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGSTNO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGSTNO.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGSTNO.Location = New System.Drawing.Point(229, 140)
        Me.cboGSTNO.Name = "cboGSTNO"
        Me.cboGSTNO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGSTNO.Size = New System.Drawing.Size(295, 22)
        Me.cboGSTNO.TabIndex = 38
        '
        '_Lbl_7
        '
        Me._Lbl_7.AutoSize = True
        Me._Lbl_7.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Lbl_7.Location = New System.Drawing.Point(136, 143)
        Me._Lbl_7.Name = "_Lbl_7"
        Me._Lbl_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_7.Size = New System.Drawing.Size(52, 14)
        Me._Lbl_7.TabIndex = 39
        Me._Lbl_7.Text = "GST No :"
        '
        'frmGSTR3B
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(751, 458)
        Me.Controls.Add(Me.SSTab1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGSTR3B"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FORM GSTR-3B"
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.SprdView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        CType(Me.SprdView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        CType(Me.SprdView5, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.SprdView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage5.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        CType(Me.SprdView7, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage6.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        CType(Me.SprdView8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView8.DataSource = CType(AData5, MSDATASRC.DataSource)
        'SprdView6.DataSource = CType(AData4, MSDATASRC.DataSource)
        'SprdView7.DataSource = CType(AData4, MSDATASRC.DataSource)
        'SprdView3.DataSource = CType(AData3, MSDATASRC.DataSource)
        'SprdView4.DataSource = CType(AData3, MSDATASRC.DataSource)
        'SprdView5.DataSource = CType(AData3, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView8.DataSource = Nothing
        SprdView6.DataSource = Nothing
        SprdView7.DataSource = Nothing
        SprdView3.DataSource = Nothing
        SprdView4.DataSource = Nothing
        SprdView5.DataSource = Nothing
    End Sub

    Public WithEvents cboGSTNO As ComboBox
    Public WithEvents _Lbl_7 As Label
#End Region
End Class