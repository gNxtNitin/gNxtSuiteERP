Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmUpdatePFESINo
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
    Public WithEvents _optESIShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optESIShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optESIShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents FraESI As System.Windows.Forms.GroupBox
    Public WithEvents _optPFShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optPFShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPFShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents FraPF As System.Windows.Forms.GroupBox
    Public WithEvents _optOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents TxtCardNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents txtESINo As System.Windows.Forms.TextBox
    Public WithEvents txtPFNo As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblESI As System.Windows.Forms.Label
    Public WithEvents lblPF As System.Windows.Forms.Label
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblCategory As System.Windows.Forms.Label
    Public WithEvents optESIShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optPFShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdatePFESINo))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.txtESINo = New System.Windows.Forms.TextBox()
        Me.txtPFNo = New System.Windows.Forms.TextBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.FraESI = New System.Windows.Forms.GroupBox()
        Me._optESIShow_2 = New System.Windows.Forms.RadioButton()
        Me._optESIShow_1 = New System.Windows.Forms.RadioButton()
        Me._optESIShow_0 = New System.Windows.Forms.RadioButton()
        Me.FraPF = New System.Windows.Forms.GroupBox()
        Me._optPFShow_0 = New System.Windows.Forms.RadioButton()
        Me._optPFShow_1 = New System.Windows.Forms.RadioButton()
        Me._optPFShow_2 = New System.Windows.Forms.RadioButton()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._optOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.TxtCardNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblESI = New System.Windows.Forms.Label()
        Me.lblPF = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.optESIShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optPFShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraESI.SuspendLayout()
        Me.FraPF.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.FraMain.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optESIShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPFShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(476, 416)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 5
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(208, 14)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearch.TabIndex = 25
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'txtESINo
        '
        Me.txtESINo.AcceptsReturn = True
        Me.txtESINo.BackColor = System.Drawing.SystemColors.Window
        Me.txtESINo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESINo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESINo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESINo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESINo.Location = New System.Drawing.Point(297, 38)
        Me.txtESINo.MaxLength = 0
        Me.txtESINo.Name = "txtESINo"
        Me.txtESINo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESINo.Size = New System.Drawing.Size(191, 19)
        Me.txtESINo.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtESINo, "Press F1 For Help")
        '
        'txtPFNo
        '
        Me.txtPFNo.AcceptsReturn = True
        Me.txtPFNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPFNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPFNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPFNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPFNo.Location = New System.Drawing.Point(100, 38)
        Me.txtPFNo.MaxLength = 0
        Me.txtPFNo.Name = "txtPFNo"
        Me.txtPFNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPFNo.Size = New System.Drawing.Size(107, 19)
        Me.txtPFNo.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtPFNo, "Press F1 For Help")
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(138, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 7
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
        Me.CmdPreview.Location = New System.Drawing.Point(206, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 6
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(4, 10)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 4
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'FraESI
        '
        Me.FraESI.BackColor = System.Drawing.SystemColors.Control
        Me.FraESI.Controls.Add(Me._optESIShow_2)
        Me.FraESI.Controls.Add(Me._optESIShow_1)
        Me.FraESI.Controls.Add(Me._optESIShow_0)
        Me.FraESI.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraESI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraESI.Location = New System.Drawing.Point(658, 0)
        Me.FraESI.Name = "FraESI"
        Me.FraESI.Padding = New System.Windows.Forms.Padding(0)
        Me.FraESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraESI.Size = New System.Drawing.Size(93, 69)
        Me.FraESI.TabIndex = 20
        Me.FraESI.TabStop = False
        Me.FraESI.Text = "ESI No"
        '
        '_optESIShow_2
        '
        Me._optESIShow_2.AutoSize = True
        Me._optESIShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._optESIShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optESIShow_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optESIShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optESIShow.SetIndex(Me._optESIShow_2, CType(2, Short))
        Me._optESIShow_2.Location = New System.Drawing.Point(4, 48)
        Me._optESIShow_2.Name = "_optESIShow_2"
        Me._optESIShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optESIShow_2.Size = New System.Drawing.Size(70, 18)
        Me._optESIShow_2.TabIndex = 23
        Me._optESIShow_2.TabStop = True
        Me._optESIShow_2.Text = "Not Blank"
        Me._optESIShow_2.UseVisualStyleBackColor = False
        '
        '_optESIShow_1
        '
        Me._optESIShow_1.AutoSize = True
        Me._optESIShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optESIShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optESIShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optESIShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optESIShow.SetIndex(Me._optESIShow_1, CType(1, Short))
        Me._optESIShow_1.Location = New System.Drawing.Point(4, 32)
        Me._optESIShow_1.Name = "_optESIShow_1"
        Me._optESIShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optESIShow_1.Size = New System.Drawing.Size(51, 18)
        Me._optESIShow_1.TabIndex = 22
        Me._optESIShow_1.TabStop = True
        Me._optESIShow_1.Text = "Blank"
        Me._optESIShow_1.UseVisualStyleBackColor = False
        '
        '_optESIShow_0
        '
        Me._optESIShow_0.AutoSize = True
        Me._optESIShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optESIShow_0.Checked = True
        Me._optESIShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optESIShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optESIShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optESIShow.SetIndex(Me._optESIShow_0, CType(0, Short))
        Me._optESIShow_0.Location = New System.Drawing.Point(4, 16)
        Me._optESIShow_0.Name = "_optESIShow_0"
        Me._optESIShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optESIShow_0.Size = New System.Drawing.Size(37, 18)
        Me._optESIShow_0.TabIndex = 21
        Me._optESIShow_0.TabStop = True
        Me._optESIShow_0.Text = "All"
        Me._optESIShow_0.UseVisualStyleBackColor = False
        '
        'FraPF
        '
        Me.FraPF.BackColor = System.Drawing.SystemColors.Control
        Me.FraPF.Controls.Add(Me._optPFShow_0)
        Me.FraPF.Controls.Add(Me._optPFShow_1)
        Me.FraPF.Controls.Add(Me._optPFShow_2)
        Me.FraPF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPF.Location = New System.Drawing.Point(564, 0)
        Me.FraPF.Name = "FraPF"
        Me.FraPF.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPF.Size = New System.Drawing.Size(93, 69)
        Me.FraPF.TabIndex = 15
        Me.FraPF.TabStop = False
        Me.FraPF.Text = "PF No"
        '
        '_optPFShow_0
        '
        Me._optPFShow_0.AutoSize = True
        Me._optPFShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPFShow_0.Checked = True
        Me._optPFShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPFShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPFShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPFShow.SetIndex(Me._optPFShow_0, CType(0, Short))
        Me._optPFShow_0.Location = New System.Drawing.Point(4, 16)
        Me._optPFShow_0.Name = "_optPFShow_0"
        Me._optPFShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPFShow_0.Size = New System.Drawing.Size(37, 18)
        Me._optPFShow_0.TabIndex = 18
        Me._optPFShow_0.TabStop = True
        Me._optPFShow_0.Text = "All"
        Me._optPFShow_0.UseVisualStyleBackColor = False
        '
        '_optPFShow_1
        '
        Me._optPFShow_1.AutoSize = True
        Me._optPFShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPFShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPFShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPFShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPFShow.SetIndex(Me._optPFShow_1, CType(1, Short))
        Me._optPFShow_1.Location = New System.Drawing.Point(4, 32)
        Me._optPFShow_1.Name = "_optPFShow_1"
        Me._optPFShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPFShow_1.Size = New System.Drawing.Size(51, 18)
        Me._optPFShow_1.TabIndex = 17
        Me._optPFShow_1.TabStop = True
        Me._optPFShow_1.Text = "Blank"
        Me._optPFShow_1.UseVisualStyleBackColor = False
        '
        '_optPFShow_2
        '
        Me._optPFShow_2.AutoSize = True
        Me._optPFShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._optPFShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPFShow_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPFShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPFShow.SetIndex(Me._optPFShow_2, CType(2, Short))
        Me._optPFShow_2.Location = New System.Drawing.Point(4, 48)
        Me._optPFShow_2.Name = "_optPFShow_2"
        Me._optPFShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPFShow_2.Size = New System.Drawing.Size(70, 18)
        Me._optPFShow_2.TabIndex = 16
        Me._optPFShow_2.TabStop = True
        Me._optPFShow_2.Text = "Not Blank"
        Me._optPFShow_2.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._optOrderBy_0)
        Me.Frame4.Controls.Add(Me._optOrderBy_1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(67, 69)
        Me.Frame4.TabIndex = 12
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Order By"
        '
        '_optOrderBy_0
        '
        Me._optOrderBy_0.AutoSize = True
        Me._optOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_0.Checked = True
        Me._optOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_0, CType(0, Short))
        Me._optOrderBy_0.Location = New System.Drawing.Point(4, 22)
        Me._optOrderBy_0.Name = "_optOrderBy_0"
        Me._optOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_0.Size = New System.Drawing.Size(52, 18)
        Me._optOrderBy_0.TabIndex = 14
        Me._optOrderBy_0.TabStop = True
        Me._optOrderBy_0.Text = "Name"
        Me._optOrderBy_0.UseVisualStyleBackColor = False
        '
        '_optOrderBy_1
        '
        Me._optOrderBy_1.AutoSize = True
        Me._optOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_1, CType(1, Short))
        Me._optOrderBy_1.Location = New System.Drawing.Point(4, 42)
        Me._optOrderBy_1.Name = "_optOrderBy_1"
        Me._optOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_1.Size = New System.Drawing.Size(50, 18)
        Me._optOrderBy_1.TabIndex = 13
        Me._optOrderBy_1.TabStop = True
        Me._optOrderBy_1.Text = "Code"
        Me._optOrderBy_1.UseVisualStyleBackColor = False
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.TxtName)
        Me.FraMain.Controls.Add(Me.TxtCardNo)
        Me.FraMain.Controls.Add(Me.cmdSearch)
        Me.FraMain.Controls.Add(Me.txtESINo)
        Me.FraMain.Controls.Add(Me.txtPFNo)
        Me.FraMain.Controls.Add(Me.Label1)
        Me.FraMain.Controls.Add(Me.lblESI)
        Me.FraMain.Controls.Add(Me.lblPF)
        Me.FraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(68, 0)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(495, 69)
        Me.FraMain.TabIndex = 0
        Me.FraMain.TabStop = False
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Enabled = False
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(244, 14)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(245, 19)
        Me.TxtName.TabIndex = 27
        '
        'TxtCardNo
        '
        Me.TxtCardNo.AcceptsReturn = True
        Me.TxtCardNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCardNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCardNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCardNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCardNo.Location = New System.Drawing.Point(100, 14)
        Me.TxtCardNo.MaxLength = 0
        Me.TxtCardNo.Name = "TxtCardNo"
        Me.TxtCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCardNo.Size = New System.Drawing.Size(107, 19)
        Me.TxtCardNo.TabIndex = 26
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(30, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Emp Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblESI
        '
        Me.lblESI.AutoSize = True
        Me.lblESI.BackColor = System.Drawing.SystemColors.Control
        Me.lblESI.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblESI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblESI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblESI.Location = New System.Drawing.Point(246, 40)
        Me.lblESI.Name = "lblESI"
        Me.lblESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblESI.Size = New System.Drawing.Size(44, 14)
        Me.lblESI.TabIndex = 11
        Me.lblESI.Text = "ESI No :"
        Me.lblESI.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPF
        '
        Me.lblPF.AutoSize = True
        Me.lblPF.BackColor = System.Drawing.SystemColors.Control
        Me.lblPF.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPF.Location = New System.Drawing.Point(53, 40)
        Me.lblPF.Name = "lblPF"
        Me.lblPF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPF.Size = New System.Drawing.Size(41, 14)
        Me.lblPF.TabIndex = 9
        Me.lblPF.Text = "PF No :"
        Me.lblPF.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdMain)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 64)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(751, 347)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(745, 335)
        Me.SprdMain.TabIndex = 19
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdPrint)
        Me.Frame1.Controls.Add(Me.CmdPreview)
        Me.Frame1.Controls.Add(Me.cmdShow)
        Me.Frame1.Controls.Add(Me.cmdClose)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(406, 406)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(345, 51)
        Me.Frame1.TabIndex = 2
        Me.Frame1.TabStop = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(274, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 3
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 21
        '
        'lblCategory
        '
        Me.lblCategory.BackColor = System.Drawing.SystemColors.Control
        Me.lblCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCategory.Location = New System.Drawing.Point(30, 424)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCategory.Size = New System.Drawing.Size(85, 15)
        Me.lblCategory.TabIndex = 24
        Me.lblCategory.Text = "lblCategory"
        '
        'frmUpdatePFESINo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(751, 458)
        Me.Controls.Add(Me.FraESI)
        Me.Controls.Add(Me.FraPF)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.CmdSave)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.lblCategory)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUpdatePFESINo"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Update -PF & ESI No"
        Me.FraESI.ResumeLayout(False)
        Me.FraESI.PerformLayout()
        Me.FraPF.ResumeLayout(False)
        Me.FraPF.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.FraMain.ResumeLayout(False)
        Me.FraMain.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optESIShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPFShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdMain.DataSource = CType(Adata, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain.DataSource = Nothing
    End Sub
#End Region
End Class