Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmployeeCategoryMst
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
    Public WithEvents _optCategoryType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optCategoryType_0 As System.Windows.Forms.RadioButton
    Public WithEvents FraCatType As System.Windows.Forms.GroupBox
    Public WithEvents cmdGRSearch As System.Windows.Forms.Button
    Public WithEvents txtGRPostingHead As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtELPostingHead As System.Windows.Forms.TextBox
    Public WithEvents cmdELSearch As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboCatgeory As System.Windows.Forms.ComboBox
    Public WithEvents txtSLTCDebit As System.Windows.Forms.TextBox
    Public WithEvents cmdSLTCDSearch As System.Windows.Forms.Button
    Public WithEvents txtSBonusDebit As System.Windows.Forms.TextBox
    Public WithEvents cmdSBonusDSearch As System.Windows.Forms.Button
    Public WithEvents txtSIncDebit As System.Windows.Forms.TextBox
    Public WithEvents cmdSIncDSearch As System.Windows.Forms.Button
    Public WithEvents cmdSBDSearch As System.Windows.Forms.Button
    Public WithEvents txtSBDebit As System.Windows.Forms.TextBox
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents txtSLTCCredit As System.Windows.Forms.TextBox
    Public WithEvents cmdSLTCCSearch As System.Windows.Forms.Button
    Public WithEvents txtSBonusCredit As System.Windows.Forms.TextBox
    Public WithEvents cmdSBonusCSearch As System.Windows.Forms.Button
    Public WithEvents txtSIncCredit As System.Windows.Forms.TextBox
    Public WithEvents cmdSIncCSearch As System.Windows.Forms.Button
    Public WithEvents cmdSBCSearch As System.Windows.Forms.Button
    Public WithEvents txtSBCredit As System.Windows.Forms.TextBox
    Public WithEvents Label53 As System.Windows.Forms.Label
    Public WithEvents Label54 As System.Windows.Forms.Label
    Public WithEvents Label55 As System.Windows.Forms.Label
    Public WithEvents Label56 As System.Windows.Forms.Label
    Public WithEvents Frame18 As System.Windows.Forms.GroupBox
    Public WithEvents txtPFCredit As System.Windows.Forms.TextBox
    Public WithEvents cmdPFCSearch As System.Windows.Forms.Button
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents txtWelfare_GS As System.Windows.Forms.TextBox
    Public WithEvents cmdWelfare_GS As System.Windows.Forms.Button
    Public WithEvents Label70 As System.Windows.Forms.Label
    Public WithEvents Frame21 As System.Windows.Forms.GroupBox
    Public WithEvents cmdESICSearch As System.Windows.Forms.Button
    Public WithEvents txtESICredit As System.Windows.Forms.TextBox
    Public WithEvents Label63 As System.Windows.Forms.Label
    Public WithEvents Label64 As System.Windows.Forms.Label
    Public WithEvents Frame12 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraGridView As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents optCategoryType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeCategoryMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdGRSearch = New System.Windows.Forms.Button()
        Me.cmdELSearch = New System.Windows.Forms.Button()
        Me.cmdSLTCDSearch = New System.Windows.Forms.Button()
        Me.cmdSBonusDSearch = New System.Windows.Forms.Button()
        Me.cmdSIncDSearch = New System.Windows.Forms.Button()
        Me.cmdSBDSearch = New System.Windows.Forms.Button()
        Me.cmdSLTCCSearch = New System.Windows.Forms.Button()
        Me.cmdSBonusCSearch = New System.Windows.Forms.Button()
        Me.cmdSIncCSearch = New System.Windows.Forms.Button()
        Me.cmdSBCSearch = New System.Windows.Forms.Button()
        Me.cmdPFCSearch = New System.Windows.Forms.Button()
        Me.cmdWelfare_GS = New System.Windows.Forms.Button()
        Me.cmdESICSearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.FraCatType = New System.Windows.Forms.GroupBox()
        Me._optCategoryType_1 = New System.Windows.Forms.RadioButton()
        Me._optCategoryType_0 = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtGRPostingHead = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtELPostingHead = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCatgeory = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtSLTCDebit = New System.Windows.Forms.TextBox()
        Me.txtSBonusDebit = New System.Windows.Forms.TextBox()
        Me.txtSIncDebit = New System.Windows.Forms.TextBox()
        Me.txtSBDebit = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Frame18 = New System.Windows.Forms.GroupBox()
        Me.txtSLTCCredit = New System.Windows.Forms.TextBox()
        Me.txtSBonusCredit = New System.Windows.Forms.TextBox()
        Me.txtSIncCredit = New System.Windows.Forms.TextBox()
        Me.txtSBCredit = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtPFCredit = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Frame21 = New System.Windows.Forms.GroupBox()
        Me.txtWelfare_GS = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.Frame12 = New System.Windows.Forms.GroupBox()
        Me.txtESICredit = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FraGridView = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.optCategoryType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraView.SuspendLayout()
        Me.FraCatType.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame18.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame21.SuspendLayout()
        Me.Frame12.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraGridView.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.optCategoryType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdGRSearch
        '
        Me.cmdGRSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdGRSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdGRSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdGRSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdGRSearch.Image = CType(resources.GetObject("cmdGRSearch.Image"), System.Drawing.Image)
        Me.cmdGRSearch.Location = New System.Drawing.Point(646, 16)
        Me.cmdGRSearch.Name = "cmdGRSearch"
        Me.cmdGRSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdGRSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdGRSearch.TabIndex = 60
        Me.cmdGRSearch.TabStop = False
        Me.cmdGRSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdGRSearch, "Search")
        Me.cmdGRSearch.UseVisualStyleBackColor = False
        '
        'cmdELSearch
        '
        Me.cmdELSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdELSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdELSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdELSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdELSearch.Image = CType(resources.GetObject("cmdELSearch.Image"), System.Drawing.Image)
        Me.cmdELSearch.Location = New System.Drawing.Point(646, 16)
        Me.cmdELSearch.Name = "cmdELSearch"
        Me.cmdELSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdELSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdELSearch.TabIndex = 55
        Me.cmdELSearch.TabStop = False
        Me.cmdELSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdELSearch, "Search")
        Me.cmdELSearch.UseVisualStyleBackColor = False
        '
        'cmdSLTCDSearch
        '
        Me.cmdSLTCDSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSLTCDSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSLTCDSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSLTCDSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSLTCDSearch.Image = CType(resources.GetObject("cmdSLTCDSearch.Image"), System.Drawing.Image)
        Me.cmdSLTCDSearch.Location = New System.Drawing.Point(646, 38)
        Me.cmdSLTCDSearch.Name = "cmdSLTCDSearch"
        Me.cmdSLTCDSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSLTCDSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdSLTCDSearch.TabIndex = 9
        Me.cmdSLTCDSearch.TabStop = False
        Me.cmdSLTCDSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSLTCDSearch, "Search")
        Me.cmdSLTCDSearch.UseVisualStyleBackColor = False
        '
        'cmdSBonusDSearch
        '
        Me.cmdSBonusDSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSBonusDSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSBonusDSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSBonusDSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSBonusDSearch.Image = CType(resources.GetObject("cmdSBonusDSearch.Image"), System.Drawing.Image)
        Me.cmdSBonusDSearch.Location = New System.Drawing.Point(646, 14)
        Me.cmdSBonusDSearch.Name = "cmdSBonusDSearch"
        Me.cmdSBonusDSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSBonusDSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdSBonusDSearch.TabIndex = 5
        Me.cmdSBonusDSearch.TabStop = False
        Me.cmdSBonusDSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSBonusDSearch, "Search")
        Me.cmdSBonusDSearch.UseVisualStyleBackColor = False
        '
        'cmdSIncDSearch
        '
        Me.cmdSIncDSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSIncDSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSIncDSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSIncDSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSIncDSearch.Image = CType(resources.GetObject("cmdSIncDSearch.Image"), System.Drawing.Image)
        Me.cmdSIncDSearch.Location = New System.Drawing.Point(320, 38)
        Me.cmdSIncDSearch.Name = "cmdSIncDSearch"
        Me.cmdSIncDSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSIncDSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdSIncDSearch.TabIndex = 7
        Me.cmdSIncDSearch.TabStop = False
        Me.cmdSIncDSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSIncDSearch, "Search")
        Me.cmdSIncDSearch.UseVisualStyleBackColor = False
        '
        'cmdSBDSearch
        '
        Me.cmdSBDSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSBDSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSBDSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSBDSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSBDSearch.Image = CType(resources.GetObject("cmdSBDSearch.Image"), System.Drawing.Image)
        Me.cmdSBDSearch.Location = New System.Drawing.Point(320, 14)
        Me.cmdSBDSearch.Name = "cmdSBDSearch"
        Me.cmdSBDSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSBDSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdSBDSearch.TabIndex = 3
        Me.cmdSBDSearch.TabStop = False
        Me.cmdSBDSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSBDSearch, "Search")
        Me.cmdSBDSearch.UseVisualStyleBackColor = False
        '
        'cmdSLTCCSearch
        '
        Me.cmdSLTCCSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSLTCCSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSLTCCSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSLTCCSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSLTCCSearch.Image = CType(resources.GetObject("cmdSLTCCSearch.Image"), System.Drawing.Image)
        Me.cmdSLTCCSearch.Location = New System.Drawing.Point(646, 38)
        Me.cmdSLTCCSearch.Name = "cmdSLTCCSearch"
        Me.cmdSLTCCSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSLTCCSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdSLTCCSearch.TabIndex = 17
        Me.cmdSLTCCSearch.TabStop = False
        Me.cmdSLTCCSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSLTCCSearch, "Search")
        Me.cmdSLTCCSearch.UseVisualStyleBackColor = False
        '
        'cmdSBonusCSearch
        '
        Me.cmdSBonusCSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSBonusCSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSBonusCSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSBonusCSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSBonusCSearch.Image = CType(resources.GetObject("cmdSBonusCSearch.Image"), System.Drawing.Image)
        Me.cmdSBonusCSearch.Location = New System.Drawing.Point(646, 14)
        Me.cmdSBonusCSearch.Name = "cmdSBonusCSearch"
        Me.cmdSBonusCSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSBonusCSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdSBonusCSearch.TabIndex = 13
        Me.cmdSBonusCSearch.TabStop = False
        Me.cmdSBonusCSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSBonusCSearch, "Search")
        Me.cmdSBonusCSearch.UseVisualStyleBackColor = False
        '
        'cmdSIncCSearch
        '
        Me.cmdSIncCSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSIncCSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSIncCSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSIncCSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSIncCSearch.Image = CType(resources.GetObject("cmdSIncCSearch.Image"), System.Drawing.Image)
        Me.cmdSIncCSearch.Location = New System.Drawing.Point(320, 38)
        Me.cmdSIncCSearch.Name = "cmdSIncCSearch"
        Me.cmdSIncCSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSIncCSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdSIncCSearch.TabIndex = 15
        Me.cmdSIncCSearch.TabStop = False
        Me.cmdSIncCSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSIncCSearch, "Search")
        Me.cmdSIncCSearch.UseVisualStyleBackColor = False
        '
        'cmdSBCSearch
        '
        Me.cmdSBCSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSBCSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSBCSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSBCSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSBCSearch.Image = CType(resources.GetObject("cmdSBCSearch.Image"), System.Drawing.Image)
        Me.cmdSBCSearch.Location = New System.Drawing.Point(320, 14)
        Me.cmdSBCSearch.Name = "cmdSBCSearch"
        Me.cmdSBCSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSBCSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdSBCSearch.TabIndex = 11
        Me.cmdSBCSearch.TabStop = False
        Me.cmdSBCSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSBCSearch, "Search")
        Me.cmdSBCSearch.UseVisualStyleBackColor = False
        '
        'cmdPFCSearch
        '
        Me.cmdPFCSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPFCSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPFCSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPFCSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPFCSearch.Image = CType(resources.GetObject("cmdPFCSearch.Image"), System.Drawing.Image)
        Me.cmdPFCSearch.Location = New System.Drawing.Point(646, 16)
        Me.cmdPFCSearch.Name = "cmdPFCSearch"
        Me.cmdPFCSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPFCSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdPFCSearch.TabIndex = 19
        Me.cmdPFCSearch.TabStop = False
        Me.cmdPFCSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPFCSearch, "Search")
        Me.cmdPFCSearch.UseVisualStyleBackColor = False
        '
        'cmdWelfare_GS
        '
        Me.cmdWelfare_GS.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdWelfare_GS.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWelfare_GS.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWelfare_GS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWelfare_GS.Image = CType(resources.GetObject("cmdWelfare_GS.Image"), System.Drawing.Image)
        Me.cmdWelfare_GS.Location = New System.Drawing.Point(646, 20)
        Me.cmdWelfare_GS.Name = "cmdWelfare_GS"
        Me.cmdWelfare_GS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWelfare_GS.Size = New System.Drawing.Size(29, 21)
        Me.cmdWelfare_GS.TabIndex = 24
        Me.cmdWelfare_GS.TabStop = False
        Me.cmdWelfare_GS.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdWelfare_GS, "Search")
        Me.cmdWelfare_GS.UseVisualStyleBackColor = False
        '
        'cmdESICSearch
        '
        Me.cmdESICSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdESICSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdESICSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdESICSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdESICSearch.Image = CType(resources.GetObject("cmdESICSearch.Image"), System.Drawing.Image)
        Me.cmdESICSearch.Location = New System.Drawing.Point(646, 16)
        Me.cmdESICSearch.Name = "cmdESICSearch"
        Me.cmdESICSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdESICSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdESICSearch.TabIndex = 22
        Me.cmdESICSearch.TabStop = False
        Me.cmdESICSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdESICSearch, "Search")
        Me.cmdESICSearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(562, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(62, 37)
        Me.CmdClose.TabIndex = 31
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(500, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(62, 37)
        Me.CmdView.TabIndex = 30
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(440, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(61, 37)
        Me.cmdPreview.TabIndex = 29
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPreview, "Print Preview")
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(378, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(62, 37)
        Me.cmdPrint.TabIndex = 28
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(316, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(62, 37)
        Me.CmdDelete.TabIndex = 27
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(248, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(69, 37)
        Me.cmdSavePrint.TabIndex = 26
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(186, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(62, 37)
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
        Me.CmdModify.Location = New System.Drawing.Point(124, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(62, 37)
        Me.CmdModify.TabIndex = 21
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
        Me.CmdAdd.Location = New System.Drawing.Point(62, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(62, 37)
        Me.CmdAdd.TabIndex = 1
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.FraCatType)
        Me.FraView.Controls.Add(Me.Frame2)
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.cboCatgeory)
        Me.FraView.Controls.Add(Me.Frame3)
        Me.FraView.Controls.Add(Me.Frame18)
        Me.FraView.Controls.Add(Me.Frame5)
        Me.FraView.Controls.Add(Me.Frame21)
        Me.FraView.Controls.Add(Me.Frame12)
        Me.FraView.Controls.Add(Me.Report1)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -4)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(683, 413)
        Me.FraView.TabIndex = 32
        Me.FraView.TabStop = False
        '
        'FraCatType
        '
        Me.FraCatType.BackColor = System.Drawing.SystemColors.Control
        Me.FraCatType.Controls.Add(Me._optCategoryType_1)
        Me.FraCatType.Controls.Add(Me._optCategoryType_0)
        Me.FraCatType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCatType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraCatType.Location = New System.Drawing.Point(422, 6)
        Me.FraCatType.Name = "FraCatType"
        Me.FraCatType.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCatType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCatType.Size = New System.Drawing.Size(261, 43)
        Me.FraCatType.TabIndex = 63
        Me.FraCatType.TabStop = False
        Me.FraCatType.Text = "Category Type"
        '
        '_optCategoryType_1
        '
        Me._optCategoryType_1.AutoSize = True
        Me._optCategoryType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCategoryType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCategoryType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCategoryType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCategoryType.SetIndex(Me._optCategoryType_1, CType(1, Short))
        Me._optCategoryType_1.Location = New System.Drawing.Point(136, 20)
        Me._optCategoryType_1.Name = "_optCategoryType_1"
        Me._optCategoryType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCategoryType_1.Size = New System.Drawing.Size(66, 18)
        Me._optCategoryType_1.TabIndex = 65
        Me._optCategoryType_1.TabStop = True
        Me._optCategoryType_1.Text = "Workers"
        Me._optCategoryType_1.UseVisualStyleBackColor = False
        '
        '_optCategoryType_0
        '
        Me._optCategoryType_0.AutoSize = True
        Me._optCategoryType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optCategoryType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCategoryType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCategoryType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCategoryType.SetIndex(Me._optCategoryType_0, CType(0, Short))
        Me._optCategoryType_0.Location = New System.Drawing.Point(44, 20)
        Me._optCategoryType_0.Name = "_optCategoryType_0"
        Me._optCategoryType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCategoryType_0.Size = New System.Drawing.Size(49, 18)
        Me._optCategoryType_0.TabIndex = 64
        Me._optCategoryType_0.TabStop = True
        Me._optCategoryType_0.Text = "Staff"
        Me._optCategoryType_0.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cmdGRSearch)
        Me.Frame2.Controls.Add(Me.txtGRPostingHead)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(2, 220)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(681, 45)
        Me.Frame2.TabIndex = 58
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Gratuity Posting Head"
        '
        'txtGRPostingHead
        '
        Me.txtGRPostingHead.AcceptsReturn = True
        Me.txtGRPostingHead.BackColor = System.Drawing.SystemColors.Window
        Me.txtGRPostingHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGRPostingHead.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGRPostingHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGRPostingHead.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGRPostingHead.Location = New System.Drawing.Point(112, 16)
        Me.txtGRPostingHead.MaxLength = 0
        Me.txtGRPostingHead.Name = "txtGRPostingHead"
        Me.txtGRPostingHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGRPostingHead.Size = New System.Drawing.Size(533, 21)
        Me.txtGRPostingHead.TabIndex = 59
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(6, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(60, 14)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "A/c Name :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Enabled = False
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(37, 14)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "Debit :"
        Me.Label4.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtELPostingHead)
        Me.Frame1.Controls.Add(Me.cmdELSearch)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(2, 174)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(681, 43)
        Me.Frame1.TabIndex = 54
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Leave Encash Posting Head"
        '
        'txtELPostingHead
        '
        Me.txtELPostingHead.AcceptsReturn = True
        Me.txtELPostingHead.BackColor = System.Drawing.SystemColors.Window
        Me.txtELPostingHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtELPostingHead.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtELPostingHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtELPostingHead.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtELPostingHead.Location = New System.Drawing.Point(112, 16)
        Me.txtELPostingHead.MaxLength = 0
        Me.txtELPostingHead.Name = "txtELPostingHead"
        Me.txtELPostingHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtELPostingHead.Size = New System.Drawing.Size(533, 21)
        Me.txtELPostingHead.TabIndex = 56
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(60, 14)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "A/c Name :"
        '
        'cboCatgeory
        '
        Me.cboCatgeory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCatgeory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCatgeory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCatgeory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCatgeory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCatgeory.Location = New System.Drawing.Point(67, 14)
        Me.cboCatgeory.Name = "cboCatgeory"
        Me.cboCatgeory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCatgeory.Size = New System.Drawing.Size(293, 22)
        Me.cboCatgeory.TabIndex = 0
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtSLTCDebit)
        Me.Frame3.Controls.Add(Me.cmdSLTCDSearch)
        Me.Frame3.Controls.Add(Me.txtSBonusDebit)
        Me.Frame3.Controls.Add(Me.cmdSBonusDSearch)
        Me.Frame3.Controls.Add(Me.txtSIncDebit)
        Me.Frame3.Controls.Add(Me.cmdSIncDSearch)
        Me.Frame3.Controls.Add(Me.cmdSBDSearch)
        Me.Frame3.Controls.Add(Me.txtSBDebit)
        Me.Frame3.Controls.Add(Me.Label24)
        Me.Frame3.Controls.Add(Me.Label23)
        Me.Frame3.Controls.Add(Me.Label22)
        Me.Frame3.Controls.Add(Me.Label5)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(2, 44)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(681, 65)
        Me.Frame3.TabIndex = 48
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "A/c Posting Debit Head"
        '
        'txtSLTCDebit
        '
        Me.txtSLTCDebit.AcceptsReturn = True
        Me.txtSLTCDebit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSLTCDebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSLTCDebit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSLTCDebit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSLTCDebit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSLTCDebit.Location = New System.Drawing.Point(404, 38)
        Me.txtSLTCDebit.MaxLength = 0
        Me.txtSLTCDebit.Name = "txtSLTCDebit"
        Me.txtSLTCDebit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSLTCDebit.Size = New System.Drawing.Size(241, 21)
        Me.txtSLTCDebit.TabIndex = 8
        '
        'txtSBonusDebit
        '
        Me.txtSBonusDebit.AcceptsReturn = True
        Me.txtSBonusDebit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSBonusDebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSBonusDebit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSBonusDebit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSBonusDebit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSBonusDebit.Location = New System.Drawing.Point(404, 14)
        Me.txtSBonusDebit.MaxLength = 0
        Me.txtSBonusDebit.Name = "txtSBonusDebit"
        Me.txtSBonusDebit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSBonusDebit.Size = New System.Drawing.Size(241, 21)
        Me.txtSBonusDebit.TabIndex = 4
        '
        'txtSIncDebit
        '
        Me.txtSIncDebit.AcceptsReturn = True
        Me.txtSIncDebit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSIncDebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSIncDebit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSIncDebit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSIncDebit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSIncDebit.Location = New System.Drawing.Point(70, 38)
        Me.txtSIncDebit.MaxLength = 0
        Me.txtSIncDebit.Name = "txtSIncDebit"
        Me.txtSIncDebit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSIncDebit.Size = New System.Drawing.Size(249, 21)
        Me.txtSIncDebit.TabIndex = 6
        '
        'txtSBDebit
        '
        Me.txtSBDebit.AcceptsReturn = True
        Me.txtSBDebit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSBDebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSBDebit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSBDebit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSBDebit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSBDebit.Location = New System.Drawing.Point(70, 14)
        Me.txtSBDebit.MaxLength = 0
        Me.txtSBDebit.Name = "txtSBDebit"
        Me.txtSBDebit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSBDebit.Size = New System.Drawing.Size(249, 21)
        Me.txtSBDebit.TabIndex = 2
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(354, 40)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(31, 14)
        Me.Label24.TabIndex = 52
        Me.Label24.Text = "LTC :"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(354, 16)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(44, 14)
        Me.Label23.TabIndex = 51
        Me.Label23.Text = "Bonus :"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(6, 40)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(56, 14)
        Me.Label22.TabIndex = 50
        Me.Label22.Text = "Incentive :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(44, 14)
        Me.Label5.TabIndex = 49
        Me.Label5.Text = "Salary :"
        '
        'Frame18
        '
        Me.Frame18.BackColor = System.Drawing.SystemColors.Control
        Me.Frame18.Controls.Add(Me.txtSLTCCredit)
        Me.Frame18.Controls.Add(Me.cmdSLTCCSearch)
        Me.Frame18.Controls.Add(Me.txtSBonusCredit)
        Me.Frame18.Controls.Add(Me.cmdSBonusCSearch)
        Me.Frame18.Controls.Add(Me.txtSIncCredit)
        Me.Frame18.Controls.Add(Me.cmdSIncCSearch)
        Me.Frame18.Controls.Add(Me.cmdSBCSearch)
        Me.Frame18.Controls.Add(Me.txtSBCredit)
        Me.Frame18.Controls.Add(Me.Label53)
        Me.Frame18.Controls.Add(Me.Label54)
        Me.Frame18.Controls.Add(Me.Label55)
        Me.Frame18.Controls.Add(Me.Label56)
        Me.Frame18.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame18.Location = New System.Drawing.Point(2, 108)
        Me.Frame18.Name = "Frame18"
        Me.Frame18.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame18.Size = New System.Drawing.Size(681, 65)
        Me.Frame18.TabIndex = 43
        Me.Frame18.TabStop = False
        Me.Frame18.Text = "A/c Posting Credit Head"
        '
        'txtSLTCCredit
        '
        Me.txtSLTCCredit.AcceptsReturn = True
        Me.txtSLTCCredit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSLTCCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSLTCCredit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSLTCCredit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSLTCCredit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSLTCCredit.Location = New System.Drawing.Point(404, 38)
        Me.txtSLTCCredit.MaxLength = 0
        Me.txtSLTCCredit.Name = "txtSLTCCredit"
        Me.txtSLTCCredit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSLTCCredit.Size = New System.Drawing.Size(241, 21)
        Me.txtSLTCCredit.TabIndex = 16
        '
        'txtSBonusCredit
        '
        Me.txtSBonusCredit.AcceptsReturn = True
        Me.txtSBonusCredit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSBonusCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSBonusCredit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSBonusCredit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSBonusCredit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSBonusCredit.Location = New System.Drawing.Point(404, 14)
        Me.txtSBonusCredit.MaxLength = 0
        Me.txtSBonusCredit.Name = "txtSBonusCredit"
        Me.txtSBonusCredit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSBonusCredit.Size = New System.Drawing.Size(241, 21)
        Me.txtSBonusCredit.TabIndex = 12
        '
        'txtSIncCredit
        '
        Me.txtSIncCredit.AcceptsReturn = True
        Me.txtSIncCredit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSIncCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSIncCredit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSIncCredit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSIncCredit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSIncCredit.Location = New System.Drawing.Point(70, 38)
        Me.txtSIncCredit.MaxLength = 0
        Me.txtSIncCredit.Name = "txtSIncCredit"
        Me.txtSIncCredit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSIncCredit.Size = New System.Drawing.Size(249, 21)
        Me.txtSIncCredit.TabIndex = 14
        '
        'txtSBCredit
        '
        Me.txtSBCredit.AcceptsReturn = True
        Me.txtSBCredit.BackColor = System.Drawing.SystemColors.Window
        Me.txtSBCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSBCredit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSBCredit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSBCredit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSBCredit.Location = New System.Drawing.Point(70, 14)
        Me.txtSBCredit.MaxLength = 0
        Me.txtSBCredit.Name = "txtSBCredit"
        Me.txtSBCredit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSBCredit.Size = New System.Drawing.Size(249, 21)
        Me.txtSBCredit.TabIndex = 10
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.BackColor = System.Drawing.SystemColors.Control
        Me.Label53.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label53.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label53.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label53.Location = New System.Drawing.Point(354, 40)
        Me.Label53.Name = "Label53"
        Me.Label53.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label53.Size = New System.Drawing.Size(31, 14)
        Me.Label53.TabIndex = 47
        Me.Label53.Text = "LTC :"
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.BackColor = System.Drawing.SystemColors.Control
        Me.Label54.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label54.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label54.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label54.Location = New System.Drawing.Point(354, 16)
        Me.Label54.Name = "Label54"
        Me.Label54.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label54.Size = New System.Drawing.Size(44, 14)
        Me.Label54.TabIndex = 46
        Me.Label54.Text = "Bonus :"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.BackColor = System.Drawing.SystemColors.Control
        Me.Label55.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label55.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label55.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label55.Location = New System.Drawing.Point(6, 40)
        Me.Label55.Name = "Label55"
        Me.Label55.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label55.Size = New System.Drawing.Size(56, 14)
        Me.Label55.TabIndex = 45
        Me.Label55.Text = "Incentive :"
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.BackColor = System.Drawing.SystemColors.Control
        Me.Label56.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label56.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label56.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label56.Location = New System.Drawing.Point(6, 16)
        Me.Label56.Name = "Label56"
        Me.Label56.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label56.Size = New System.Drawing.Size(44, 14)
        Me.Label56.TabIndex = 44
        Me.Label56.Text = "Salary :"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtPFCredit)
        Me.Frame5.Controls.Add(Me.cmdPFCSearch)
        Me.Frame5.Controls.Add(Me.Label8)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(2, 268)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(681, 43)
        Me.Frame5.TabIndex = 41
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "PF Contribution Posting Head"
        '
        'txtPFCredit
        '
        Me.txtPFCredit.AcceptsReturn = True
        Me.txtPFCredit.BackColor = System.Drawing.SystemColors.Window
        Me.txtPFCredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPFCredit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPFCredit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFCredit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPFCredit.Location = New System.Drawing.Point(112, 16)
        Me.txtPFCredit.MaxLength = 0
        Me.txtPFCredit.Name = "txtPFCredit"
        Me.txtPFCredit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPFCredit.Size = New System.Drawing.Size(533, 21)
        Me.txtPFCredit.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(6, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(60, 14)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "A/c Name :"
        '
        'Frame21
        '
        Me.Frame21.BackColor = System.Drawing.SystemColors.Control
        Me.Frame21.Controls.Add(Me.txtWelfare_GS)
        Me.Frame21.Controls.Add(Me.cmdWelfare_GS)
        Me.Frame21.Controls.Add(Me.Label70)
        Me.Frame21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame21.Location = New System.Drawing.Point(2, 356)
        Me.Frame21.Name = "Frame21"
        Me.Frame21.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame21.Size = New System.Drawing.Size(681, 49)
        Me.Frame21.TabIndex = 39
        Me.Frame21.TabStop = False
        Me.Frame21.Text = "Employer Welfare Contribution Posting Head"
        '
        'txtWelfare_GS
        '
        Me.txtWelfare_GS.AcceptsReturn = True
        Me.txtWelfare_GS.BackColor = System.Drawing.SystemColors.Window
        Me.txtWelfare_GS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWelfare_GS.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWelfare_GS.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWelfare_GS.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWelfare_GS.Location = New System.Drawing.Point(112, 20)
        Me.txtWelfare_GS.MaxLength = 0
        Me.txtWelfare_GS.Name = "txtWelfare_GS"
        Me.txtWelfare_GS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWelfare_GS.Size = New System.Drawing.Size(533, 21)
        Me.txtWelfare_GS.TabIndex = 23
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.BackColor = System.Drawing.SystemColors.Control
        Me.Label70.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label70.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label70.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label70.Location = New System.Drawing.Point(6, 22)
        Me.Label70.Name = "Label70"
        Me.Label70.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label70.Size = New System.Drawing.Size(60, 14)
        Me.Label70.TabIndex = 40
        Me.Label70.Text = "A/c Name :"
        '
        'Frame12
        '
        Me.Frame12.BackColor = System.Drawing.SystemColors.Control
        Me.Frame12.Controls.Add(Me.cmdESICSearch)
        Me.Frame12.Controls.Add(Me.txtESICredit)
        Me.Frame12.Controls.Add(Me.Label63)
        Me.Frame12.Controls.Add(Me.Label64)
        Me.Frame12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame12.Location = New System.Drawing.Point(2, 310)
        Me.Frame12.Name = "Frame12"
        Me.Frame12.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame12.Size = New System.Drawing.Size(681, 45)
        Me.Frame12.TabIndex = 36
        Me.Frame12.TabStop = False
        Me.Frame12.Text = "ESIC Contribution Posting Head"
        '
        'txtESICredit
        '
        Me.txtESICredit.AcceptsReturn = True
        Me.txtESICredit.BackColor = System.Drawing.SystemColors.Window
        Me.txtESICredit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESICredit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESICredit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESICredit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESICredit.Location = New System.Drawing.Point(112, 16)
        Me.txtESICredit.MaxLength = 0
        Me.txtESICredit.Name = "txtESICredit"
        Me.txtESICredit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESICredit.Size = New System.Drawing.Size(533, 21)
        Me.txtESICredit.TabIndex = 20
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.BackColor = System.Drawing.SystemColors.Control
        Me.Label63.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label63.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label63.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label63.Location = New System.Drawing.Point(6, 18)
        Me.Label63.Name = "Label63"
        Me.Label63.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label63.Size = New System.Drawing.Size(60, 14)
        Me.Label63.TabIndex = 38
        Me.Label63.Text = "A/c Name :"
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.BackColor = System.Drawing.SystemColors.Control
        Me.Label64.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label64.Enabled = False
        Me.Label64.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label64.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label64.Location = New System.Drawing.Point(6, 138)
        Me.Label64.Name = "Label64"
        Me.Label64.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label64.Size = New System.Drawing.Size(37, 14)
        Me.Label64.TabIndex = 37
        Me.Label64.Text = "Debit :"
        Me.Label64.Visible = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(2, 96)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 64
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(4, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(57, 14)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Category :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraGridView
        '
        Me.FraGridView.BackColor = System.Drawing.SystemColors.Control
        Me.FraGridView.Controls.Add(Me.SprdView)
        Me.FraGridView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGridView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGridView.Location = New System.Drawing.Point(0, -6)
        Me.FraGridView.Name = "FraGridView"
        Me.FraGridView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGridView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGridView.Size = New System.Drawing.Size(683, 415)
        Me.FraGridView.TabIndex = 33
        Me.FraGridView.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(4, 10)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(675, 403)
        Me.SprdView.TabIndex = 35
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 404)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(683, 57)
        Me.FraMovement.TabIndex = 34
        Me.FraMovement.TabStop = False
        '
        'optCategoryType
        '
        '
        'frmEmployeeCategoryMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(684, 461)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.FraGridView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmployeeCategoryMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Category Master"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.FraCatType.ResumeLayout(False)
        Me.FraCatType.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame18.ResumeLayout(False)
        Me.Frame18.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame21.ResumeLayout(False)
        Me.Frame21.PerformLayout()
        Me.Frame12.ResumeLayout(False)
        Me.Frame12.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraGridView.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.optCategoryType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class