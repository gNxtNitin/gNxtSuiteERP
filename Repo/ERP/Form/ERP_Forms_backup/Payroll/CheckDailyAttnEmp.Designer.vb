Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCheckDailyAttnEmp
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
    Public WithEvents chkAllEmp As System.Windows.Forms.CheckBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents FraSelection As System.Windows.Forms.GroupBox
    Public WithEvents chkSHowBlankLeave As System.Windows.Forms.CheckBox
    Public WithEvents cboMinorShift As System.Windows.Forms.ComboBox
    Public WithEvents chkMinorShift As System.Windows.Forms.CheckBox
    Public WithEvents Frame11 As System.Windows.Forms.GroupBox
    Public WithEvents chkShift As System.Windows.Forms.CheckBox
    Public WithEvents cboShift As System.Windows.Forms.ComboBox
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents chkShow As System.Windows.Forms.CheckBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents optCoctC As System.Windows.Forms.RadioButton
    Public WithEvents optBookNo As System.Windows.Forms.RadioButton
    Public WithEvents optCode As System.Windows.Forms.RadioButton
    Public WithEvents OptName As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents txtTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents sprdAttn As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboEmpCatType As System.Windows.Forms.ComboBox
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents chkDivision As System.Windows.Forms.CheckBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents txtPageNo As System.Windows.Forms.TextBox
    Public WithEvents chkPageNo As System.Windows.Forms.CheckBox
    Public WithEvents txtBookNo As System.Windows.Forms.TextBox
    Public WithEvents chkBookNo As System.Windows.Forms.CheckBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCheckDailyAttnEmp))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.FraSelection = New System.Windows.Forms.GroupBox()
        Me.chkAllEmp = New System.Windows.Forms.CheckBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.chkSHowBlankLeave = New System.Windows.Forms.CheckBox()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.cboMinorShift = New System.Windows.Forms.ComboBox()
        Me.chkMinorShift = New System.Windows.Forms.CheckBox()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.chkShift = New System.Windows.Forms.CheckBox()
        Me.cboShift = New System.Windows.Forms.ComboBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.chkShow = New System.Windows.Forms.CheckBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboDept = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.optCoctC = New System.Windows.Forms.RadioButton()
        Me.optBookNo = New System.Windows.Forms.RadioButton()
        Me.optCode = New System.Windows.Forms.RadioButton()
        Me.OptName = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdAttn = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.cboEmpCatType = New System.Windows.Forms.ComboBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.chkDivision = New System.Windows.Forms.CheckBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtPageNo = New System.Windows.Forms.TextBox()
        Me.chkPageNo = New System.Windows.Forms.CheckBox()
        Me.txtBookNo = New System.Windows.Forms.TextBox()
        Me.chkBookNo = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FraSelection.SuspendLayout()
        Me.Frame11.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.cboDept, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.sprdAttn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame10.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame5.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(74, 50)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdsearch.TabIndex = 47
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Location = New System.Drawing.Point(84, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(79, 34)
        Me.CmdPreview.TabIndex = 12
        Me.CmdPreview.Text = "Pre&view"
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Location = New System.Drawing.Point(822, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(80, 34)
        Me.CmdClose.TabIndex = 4
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'FraSelection
        '
        Me.FraSelection.BackColor = System.Drawing.SystemColors.Control
        Me.FraSelection.Controls.Add(Me.chkAllEmp)
        Me.FraSelection.Controls.Add(Me.txtEmpCode)
        Me.FraSelection.Controls.Add(Me.cmdsearch)
        Me.FraSelection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraSelection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraSelection.Location = New System.Drawing.Point(122, 0)
        Me.FraSelection.Name = "FraSelection"
        Me.FraSelection.Padding = New System.Windows.Forms.Padding(0)
        Me.FraSelection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraSelection.Size = New System.Drawing.Size(107, 79)
        Me.FraSelection.TabIndex = 46
        Me.FraSelection.TabStop = False
        Me.FraSelection.Text = "Code"
        '
        'chkAllEmp
        '
        Me.chkAllEmp.AutoSize = True
        Me.chkAllEmp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllEmp.Location = New System.Drawing.Point(4, 20)
        Me.chkAllEmp.Name = "chkAllEmp"
        Me.chkAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllEmp.Size = New System.Drawing.Size(46, 18)
        Me.chkAllEmp.TabIndex = 49
        Me.chkAllEmp.Text = "ALL"
        Me.chkAllEmp.UseVisualStyleBackColor = False
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(2, 50)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(71, 20)
        Me.txtEmpCode.TabIndex = 48
        '
        'chkSHowBlankLeave
        '
        Me.chkSHowBlankLeave.BackColor = System.Drawing.SystemColors.Control
        Me.chkSHowBlankLeave.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSHowBlankLeave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSHowBlankLeave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSHowBlankLeave.Location = New System.Drawing.Point(776, 68)
        Me.chkSHowBlankLeave.Name = "chkSHowBlankLeave"
        Me.chkSHowBlankLeave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSHowBlankLeave.Size = New System.Drawing.Size(131, 15)
        Me.chkSHowBlankLeave.TabIndex = 41
        Me.chkSHowBlankLeave.Text = "Show Blank Leave"
        Me.chkSHowBlankLeave.UseVisualStyleBackColor = False
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.cboMinorShift)
        Me.Frame11.Controls.Add(Me.chkMinorShift)
        Me.Frame11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame11.Location = New System.Drawing.Point(579, 40)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(161, 39)
        Me.Frame11.TabIndex = 38
        Me.Frame11.TabStop = False
        Me.Frame11.Text = "Minor Shift"
        '
        'cboMinorShift
        '
        Me.cboMinorShift.BackColor = System.Drawing.SystemColors.Window
        Me.cboMinorShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMinorShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMinorShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMinorShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMinorShift.Location = New System.Drawing.Point(2, 14)
        Me.cboMinorShift.Name = "cboMinorShift"
        Me.cboMinorShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMinorShift.Size = New System.Drawing.Size(100, 22)
        Me.cboMinorShift.TabIndex = 40
        '
        'chkMinorShift
        '
        Me.chkMinorShift.AutoSize = True
        Me.chkMinorShift.BackColor = System.Drawing.SystemColors.Control
        Me.chkMinorShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMinorShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMinorShift.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMinorShift.Location = New System.Drawing.Point(106, 15)
        Me.chkMinorShift.Name = "chkMinorShift"
        Me.chkMinorShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMinorShift.Size = New System.Drawing.Size(46, 18)
        Me.chkMinorShift.TabIndex = 39
        Me.chkMinorShift.Text = "ALL"
        Me.chkMinorShift.UseVisualStyleBackColor = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.chkShift)
        Me.Frame9.Controls.Add(Me.cboShift)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(444, 40)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(132, 39)
        Me.Frame9.TabIndex = 33
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Shift"
        '
        'chkShift
        '
        Me.chkShift.AutoSize = True
        Me.chkShift.BackColor = System.Drawing.SystemColors.Control
        Me.chkShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShift.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShift.Location = New System.Drawing.Point(78, 15)
        Me.chkShift.Name = "chkShift"
        Me.chkShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShift.Size = New System.Drawing.Size(46, 18)
        Me.chkShift.TabIndex = 35
        Me.chkShift.Text = "ALL"
        Me.chkShift.UseVisualStyleBackColor = False
        '
        'cboShift
        '
        Me.cboShift.BackColor = System.Drawing.SystemColors.Window
        Me.cboShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShift.Location = New System.Drawing.Point(4, 14)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShift.Size = New System.Drawing.Size(70, 22)
        Me.cboShift.TabIndex = 34
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cboShow)
        Me.Frame6.Controls.Add(Me.chkShow)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(579, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(161, 39)
        Me.Frame6.TabIndex = 26
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Show"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(2, 14)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(100, 22)
        Me.cboShow.TabIndex = 28
        '
        'chkShow
        '
        Me.chkShow.AutoSize = True
        Me.chkShow.BackColor = System.Drawing.SystemColors.Control
        Me.chkShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShow.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShow.Location = New System.Drawing.Point(106, 15)
        Me.chkShow.Name = "chkShow"
        Me.chkShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShow.Size = New System.Drawing.Size(46, 18)
        Me.chkShow.TabIndex = 27
        Me.chkShow.Text = "ALL"
        Me.chkShow.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.chkCategory)
        Me.Frame7.Controls.Add(Me.cboCategory)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(230, 40)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(212, 39)
        Me.Frame7.TabIndex = 23
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Category"
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(161, 15)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 25
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'cboCategory
        '
        Me.cboCategory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCategory.Location = New System.Drawing.Point(2, 14)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(154, 22)
        Me.cboCategory.TabIndex = 24
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(230, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(346, 39)
        Me.Frame4.TabIndex = 7
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Department"
        '
        'cboDept
        '
        Me.cboDept.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cboDept.AutoSize = False
        Me.cboDept.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cboDept.DisplayLayout.Appearance = Appearance1
        Me.cboDept.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cboDept.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.cboDept.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cboDept.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.cboDept.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cboDept.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.cboDept.DisplayLayout.MaxColScrollRegions = 1
        Me.cboDept.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboDept.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cboDept.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.cboDept.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cboDept.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cboDept.DisplayLayout.Override.CellAppearance = Appearance8
        Me.cboDept.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cboDept.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.cboDept.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.cboDept.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.cboDept.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cboDept.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.cboDept.DisplayLayout.Override.RowAppearance = Appearance11
        Me.cboDept.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cboDept.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.cboDept.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cboDept.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cboDept.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cboDept.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboDept.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.Location = New System.Drawing.Point(0, 13)
        Me.cboDept.MaxLength = 50
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(346, 26)
        Me.cboDept.TabIndex = 108
        Me.cboDept.Tag = "9158"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.optCoctC)
        Me.Frame3.Controls.Add(Me.optBookNo)
        Me.Frame3.Controls.Add(Me.optCode)
        Me.Frame3.Controls.Add(Me.OptName)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(743, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(164, 64)
        Me.Frame3.TabIndex = 6
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Order By"
        '
        'optCoctC
        '
        Me.optCoctC.AutoSize = True
        Me.optCoctC.BackColor = System.Drawing.SystemColors.Control
        Me.optCoctC.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCoctC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCoctC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCoctC.Location = New System.Drawing.Point(74, 32)
        Me.optCoctC.Name = "optCoctC"
        Me.optCoctC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCoctC.Size = New System.Drawing.Size(47, 18)
        Me.optCoctC.TabIndex = 29
        Me.optCoctC.TabStop = True
        Me.optCoctC.Text = "Dept"
        Me.optCoctC.UseVisualStyleBackColor = False
        '
        'optBookNo
        '
        Me.optBookNo.AutoSize = True
        Me.optBookNo.BackColor = System.Drawing.SystemColors.Control
        Me.optBookNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBookNo.Enabled = False
        Me.optBookNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBookNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBookNo.Location = New System.Drawing.Point(2, 32)
        Me.optBookNo.Name = "optBookNo"
        Me.optBookNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBookNo.Size = New System.Drawing.Size(65, 18)
        Me.optBookNo.TabIndex = 22
        Me.optBookNo.TabStop = True
        Me.optBookNo.Text = "Book No"
        Me.optBookNo.UseVisualStyleBackColor = False
        '
        'optCode
        '
        Me.optCode.AutoSize = True
        Me.optCode.BackColor = System.Drawing.SystemColors.Control
        Me.optCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCode.Location = New System.Drawing.Point(74, 16)
        Me.optCode.Name = "optCode"
        Me.optCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCode.Size = New System.Drawing.Size(50, 18)
        Me.optCode.TabIndex = 9
        Me.optCode.TabStop = True
        Me.optCode.Text = "Code"
        Me.optCode.UseVisualStyleBackColor = False
        '
        'OptName
        '
        Me.OptName.AutoSize = True
        Me.OptName.BackColor = System.Drawing.SystemColors.Control
        Me.OptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptName.Location = New System.Drawing.Point(2, 16)
        Me.OptName.Name = "OptName"
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Size = New System.Drawing.Size(52, 18)
        Me.OptName.TabIndex = 8
        Me.OptName.TabStop = True
        Me.OptName.Text = "Name"
        Me.OptName.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtTo)
        Me.Frame2.Controls.Add(Me.txtFrom)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(121, 81)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Date"
        '
        'txtTo
        '
        Me.txtTo.AllowPromptAsInput = False
        Me.txtTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.Location = New System.Drawing.Point(38, 48)
        Me.txtTo.Mask = "##/##/####"
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(79, 20)
        Me.txtTo.TabIndex = 42
        '
        'txtFrom
        '
        Me.txtFrom.AllowPromptAsInput = False
        Me.txtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.Location = New System.Drawing.Point(38, 20)
        Me.txtFrom.Mask = "##/##/####"
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(79, 20)
        Me.txtFrom.TabIndex = 43
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(2, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(37, 14)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "From :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(2, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(24, 14)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "To :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.sprdAttn)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 77)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(908, 492)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'sprdAttn
        '
        Me.sprdAttn.DataSource = Nothing
        Me.sprdAttn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdAttn.Location = New System.Drawing.Point(0, 13)
        Me.sprdAttn.Name = "sprdAttn"
        Me.sprdAttn.OcxState = CType(resources.GetObject("sprdAttn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdAttn.Size = New System.Drawing.Size(908, 479)
        Me.sprdAttn.TabIndex = 2
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Frame10)
        Me.FraMovement.Controls.Add(Me.Frame8)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(908, 51)
        Me.FraMovement.TabIndex = 3
        Me.FraMovement.TabStop = False
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.cboEmpCatType)
        Me.Frame10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(182, 8)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(179, 43)
        Me.Frame10.TabIndex = 36
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Emp. Category Type:"
        '
        'cboEmpCatType
        '
        Me.cboEmpCatType.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmpCatType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpCatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmpCatType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpCatType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboEmpCatType.Location = New System.Drawing.Point(4, 14)
        Me.cboEmpCatType.Name = "cboEmpCatType"
        Me.cboEmpCatType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmpCatType.Size = New System.Drawing.Size(173, 22)
        Me.cboEmpCatType.TabIndex = 37
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.chkDivision)
        Me.Frame8.Controls.Add(Me.cboDivision)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(362, 8)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(209, 43)
        Me.Frame8.TabIndex = 30
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Division"
        '
        'chkDivision
        '
        Me.chkDivision.AutoSize = True
        Me.chkDivision.BackColor = System.Drawing.SystemColors.Control
        Me.chkDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDivision.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDivision.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDivision.Location = New System.Drawing.Point(160, 18)
        Me.chkDivision.Name = "chkDivision"
        Me.chkDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDivision.Size = New System.Drawing.Size(46, 18)
        Me.chkDivision.TabIndex = 32
        Me.chkDivision.Text = "ALL"
        Me.chkDivision.UseVisualStyleBackColor = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(2, 14)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(157, 22)
        Me.cboDivision.TabIndex = 31
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(4, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(80, 34)
        Me.cmdPrint.TabIndex = 13
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(742, 12)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 34)
        Me.cmdRefresh.TabIndex = 5
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(134, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 37
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(114, 22)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(64, 14)
        Me.lblBookType.TabIndex = 14
        Me.lblBookType.Text = "lblBookType"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtPageNo)
        Me.Frame5.Controls.Add(Me.chkPageNo)
        Me.Frame5.Controls.Add(Me.txtBookNo)
        Me.Frame5.Controls.Add(Me.chkBookNo)
        Me.Frame5.Controls.Add(Me.Label2)
        Me.Frame5.Controls.Add(Me.Label1)
        Me.Frame5.Enabled = False
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(-2, 52)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(151, 63)
        Me.Frame5.TabIndex = 15
        Me.Frame5.TabStop = False
        '
        'txtPageNo
        '
        Me.txtPageNo.AcceptsReturn = True
        Me.txtPageNo.BackColor = System.Drawing.Color.White
        Me.txtPageNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPageNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPageNo.Enabled = False
        Me.txtPageNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPageNo.ForeColor = System.Drawing.Color.Blue
        Me.txtPageNo.Location = New System.Drawing.Point(66, 34)
        Me.txtPageNo.MaxLength = 35
        Me.txtPageNo.Name = "txtPageNo"
        Me.txtPageNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPageNo.Size = New System.Drawing.Size(37, 20)
        Me.txtPageNo.TabIndex = 19
        '
        'chkPageNo
        '
        Me.chkPageNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkPageNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPageNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPageNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPageNo.Location = New System.Drawing.Point(104, 38)
        Me.chkPageNo.Name = "chkPageNo"
        Me.chkPageNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPageNo.Size = New System.Drawing.Size(45, 13)
        Me.chkPageNo.TabIndex = 18
        Me.chkPageNo.Text = "ALL"
        Me.chkPageNo.UseVisualStyleBackColor = False
        '
        'txtBookNo
        '
        Me.txtBookNo.AcceptsReturn = True
        Me.txtBookNo.BackColor = System.Drawing.Color.White
        Me.txtBookNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBookNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBookNo.Enabled = False
        Me.txtBookNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBookNo.ForeColor = System.Drawing.Color.Blue
        Me.txtBookNo.Location = New System.Drawing.Point(66, 12)
        Me.txtBookNo.MaxLength = 35
        Me.txtBookNo.Name = "txtBookNo"
        Me.txtBookNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBookNo.Size = New System.Drawing.Size(37, 20)
        Me.txtBookNo.TabIndex = 17
        '
        'chkBookNo
        '
        Me.chkBookNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkBookNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBookNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBookNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBookNo.Location = New System.Drawing.Point(104, 14)
        Me.chkBookNo.Name = "chkBookNo"
        Me.chkBookNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBookNo.Size = New System.Drawing.Size(45, 13)
        Me.chkBookNo.TabIndex = 16
        Me.chkBookNo.Text = "ALL"
        Me.chkBookNo.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(61, 11)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Page No :"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 11)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Book No :"
        '
        'frmCheckDailyAttnEmp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraSelection)
        Me.Controls.Add(Me.chkSHowBlankLeave)
        Me.Controls.Add(Me.Frame11)
        Me.Controls.Add(Me.Frame9)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame5)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 15)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCheckDailyAttnEmp"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Daily Attendance Report"
        Me.FraSelection.ResumeLayout(False)
        Me.FraSelection.PerformLayout()
        Me.Frame11.ResumeLayout(False)
        Me.Frame11.PerformLayout()
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.cboDept, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.sprdAttn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        Me.Frame10.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cboDept As Infragistics.Win.UltraWinGrid.UltraCombo
#End Region
End Class