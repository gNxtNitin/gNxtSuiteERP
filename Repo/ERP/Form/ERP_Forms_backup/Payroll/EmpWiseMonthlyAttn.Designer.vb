Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmpWiseMonthlyAttn
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
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents chkAllEmp As System.Windows.Forms.CheckBox
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents FraSelection As System.Windows.Forms.GroupBox
    Public WithEvents optHODWise As System.Windows.Forms.RadioButton
    Public WithEvents optDept As System.Windows.Forms.RadioButton
    Public WithEvents OptName As System.Windows.Forms.RadioButton
    Public WithEvents optCard As System.Windows.Forms.RadioButton
    Public WithEvents OptBook As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents chkWithWorkingHours As System.Windows.Forms.CheckBox
    Public WithEvents chkPunchData As System.Windows.Forms.CheckBox
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdWOPay As System.Windows.Forms.Button
    Public WithEvents cmdAbsent As System.Windows.Forms.Button
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents cboEmpCatType As System.Windows.Forms.ComboBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents cboCatgeory As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents sprdAttn As AxFPSpreadADO.AxfpSpread
    Public WithEvents sprdRemarks As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdExport As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public CommonDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents _lblColor_9 As System.Windows.Forms.Label
    Public WithEvents _lblColor_8 As System.Windows.Forms.Label
    Public WithEvents _lblColor_6 As System.Windows.Forms.Label
    Public WithEvents _lblColor_7 As System.Windows.Forms.Label
    Public WithEvents _lblColor_5 As System.Windows.Forms.Label
    Public WithEvents _lblColor_4 As System.Windows.Forms.Label
    Public WithEvents _lblColor_3 As System.Windows.Forms.Label
    Public WithEvents _lblColor_2 As System.Windows.Forms.Label
    Public WithEvents _lblColor_1 As System.Windows.Forms.Label
    Public WithEvents _lblColor_0 As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents txtPageNo As System.Windows.Forms.TextBox
    Public WithEvents chkPageNo As System.Windows.Forms.CheckBox
    Public WithEvents txtBookNo As System.Windows.Forms.TextBox
    Public WithEvents chkBookNo As System.Windows.Forms.CheckBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents SprdCommand As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdPreview As AxFPSpreadADO.AxfpSpreadPreview
    Public WithEvents FraPreview As System.Windows.Forms.GroupBox
    Public WithEvents lblColor As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpWiseMonthlyAttn))
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
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdExport = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.FraSelection = New System.Windows.Forms.GroupBox()
        Me.chkAllEmp = New System.Windows.Forms.CheckBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.optHODWise = New System.Windows.Forms.RadioButton()
        Me.optDept = New System.Windows.Forms.RadioButton()
        Me.OptName = New System.Windows.Forms.RadioButton()
        Me.optCard = New System.Windows.Forms.RadioButton()
        Me.OptBook = New System.Windows.Forms.RadioButton()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.chkWithWorkingHours = New System.Windows.Forms.CheckBox()
        Me.chkPunchData = New System.Windows.Forms.CheckBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.lblRunDate = New System.Windows.Forms.DateTimePicker()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cmdWOPay = New System.Windows.Forms.Button()
        Me.cmdAbsent = New System.Windows.Forms.Button()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboEmpCatType = New System.Windows.Forms.ComboBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.cboCatgeory = New System.Windows.Forms.ComboBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboDept = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdAttn = New AxFPSpreadADO.AxfpSpread()
        Me.sprdRemarks = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me._lblColor_10 = New System.Windows.Forms.Label()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me._lblColor_9 = New System.Windows.Forms.Label()
        Me._lblColor_8 = New System.Windows.Forms.Label()
        Me._lblColor_6 = New System.Windows.Forms.Label()
        Me._lblColor_7 = New System.Windows.Forms.Label()
        Me._lblColor_5 = New System.Windows.Forms.Label()
        Me._lblColor_4 = New System.Windows.Forms.Label()
        Me._lblColor_3 = New System.Windows.Forms.Label()
        Me._lblColor_2 = New System.Windows.Forms.Label()
        Me._lblColor_1 = New System.Windows.Forms.Label()
        Me._lblColor_0 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtPageNo = New System.Windows.Forms.TextBox()
        Me.chkPageNo = New System.Windows.Forms.CheckBox()
        Me.txtBookNo = New System.Windows.Forms.TextBox()
        Me.chkBookNo = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FraPreview = New System.Windows.Forms.GroupBox()
        Me.SprdCommand = New AxFPSpreadADO.AxfpSpread()
        Me.SprdPreview = New AxFPSpreadADO.AxfpSpreadPreview()
        Me.lblColor = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.FraSelection.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.cboDept, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        CType(Me.sprdAttn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprdRemarks, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame5.SuspendLayout()
        Me.FraPreview.SuspendLayout()
        CType(Me.SprdCommand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(122, 12)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearch.TabIndex = 58
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdExport
        '
        Me.cmdExport.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExport.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExport.Location = New System.Drawing.Point(150, 12)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExport.Size = New System.Drawing.Size(73, 34)
        Me.cmdExport.TabIndex = 44
        Me.cmdExport.Text = "&Export"
        Me.ToolTip1.SetToolTip(Me.cmdExport, "Print PO")
        Me.cmdExport.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Location = New System.Drawing.Point(78, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(73, 34)
        Me.CmdPreview.TabIndex = 8
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
        Me.CmdClose.Location = New System.Drawing.Point(670, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(74, 34)
        Me.CmdClose.TabIndex = 3
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'FraSelection
        '
        Me.FraSelection.BackColor = System.Drawing.SystemColors.Control
        Me.FraSelection.Controls.Add(Me.cmdSearch)
        Me.FraSelection.Controls.Add(Me.chkAllEmp)
        Me.FraSelection.Controls.Add(Me.txtName)
        Me.FraSelection.Controls.Add(Me.txtEmpCode)
        Me.FraSelection.Controls.Add(Me.Label3)
        Me.FraSelection.Controls.Add(Me.Label4)
        Me.FraSelection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraSelection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraSelection.Location = New System.Drawing.Point(154, 0)
        Me.FraSelection.Name = "FraSelection"
        Me.FraSelection.Padding = New System.Windows.Forms.Padding(0)
        Me.FraSelection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraSelection.Size = New System.Drawing.Size(299, 73)
        Me.FraSelection.TabIndex = 21
        Me.FraSelection.TabStop = False
        '
        'chkAllEmp
        '
        Me.chkAllEmp.AutoSize = True
        Me.chkAllEmp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllEmp.Location = New System.Drawing.Point(152, 14)
        Me.chkAllEmp.Name = "chkAllEmp"
        Me.chkAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllEmp.Size = New System.Drawing.Size(46, 18)
        Me.chkAllEmp.TabIndex = 27
        Me.chkAllEmp.Text = "ALL"
        Me.chkAllEmp.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Enabled = False
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(50, 34)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(245, 20)
        Me.txtName.TabIndex = 23
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(50, 12)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(71, 20)
        Me.txtEmpCode.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(6, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(38, 14)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Code :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(40, 14)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Name :"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.optHODWise)
        Me.Frame3.Controls.Add(Me.optDept)
        Me.Frame3.Controls.Add(Me.OptName)
        Me.Frame3.Controls.Add(Me.optCard)
        Me.Frame3.Controls.Add(Me.OptBook)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(155, 73)
        Me.Frame3.TabIndex = 30
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Order By"
        '
        'optHODWise
        '
        Me.optHODWise.AutoSize = True
        Me.optHODWise.BackColor = System.Drawing.SystemColors.Control
        Me.optHODWise.Cursor = System.Windows.Forms.Cursors.Default
        Me.optHODWise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optHODWise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optHODWise.Location = New System.Drawing.Point(78, 32)
        Me.optHODWise.Name = "optHODWise"
        Me.optHODWise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optHODWise.Size = New System.Drawing.Size(47, 18)
        Me.optHODWise.TabIndex = 54
        Me.optHODWise.TabStop = True
        Me.optHODWise.Text = "HOD"
        Me.optHODWise.UseVisualStyleBackColor = False
        '
        'optDept
        '
        Me.optDept.AutoSize = True
        Me.optDept.BackColor = System.Drawing.SystemColors.Control
        Me.optDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDept.Location = New System.Drawing.Point(4, 51)
        Me.optDept.Name = "optDept"
        Me.optDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDept.Size = New System.Drawing.Size(47, 18)
        Me.optDept.TabIndex = 34
        Me.optDept.TabStop = True
        Me.optDept.Text = "Dept"
        Me.optDept.UseVisualStyleBackColor = False
        '
        'OptName
        '
        Me.OptName.AutoSize = True
        Me.OptName.BackColor = System.Drawing.SystemColors.Control
        Me.OptName.Checked = True
        Me.OptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptName.Location = New System.Drawing.Point(4, 14)
        Me.OptName.Name = "OptName"
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Size = New System.Drawing.Size(52, 18)
        Me.OptName.TabIndex = 33
        Me.OptName.TabStop = True
        Me.OptName.Text = "Name"
        Me.OptName.UseVisualStyleBackColor = False
        '
        'optCard
        '
        Me.optCard.AutoSize = True
        Me.optCard.BackColor = System.Drawing.SystemColors.Control
        Me.optCard.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCard.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCard.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCard.Location = New System.Drawing.Point(4, 32)
        Me.optCard.Name = "optCard"
        Me.optCard.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCard.Size = New System.Drawing.Size(64, 18)
        Me.optCard.TabIndex = 32
        Me.optCard.TabStop = True
        Me.optCard.Text = "Card No"
        Me.optCard.UseVisualStyleBackColor = False
        '
        'OptBook
        '
        Me.OptBook.AutoSize = True
        Me.OptBook.BackColor = System.Drawing.SystemColors.Control
        Me.OptBook.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptBook.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptBook.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptBook.Location = New System.Drawing.Point(78, 14)
        Me.OptBook.Name = "OptBook"
        Me.OptBook.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptBook.Size = New System.Drawing.Size(65, 18)
        Me.OptBook.TabIndex = 31
        Me.OptBook.TabStop = True
        Me.OptBook.Text = "Book No"
        Me.OptBook.UseVisualStyleBackColor = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.chkWithWorkingHours)
        Me.Frame9.Controls.Add(Me.chkPunchData)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(0, 71)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(146, 65)
        Me.Frame9.TabIndex = 55
        Me.Frame9.TabStop = False
        '
        'chkWithWorkingHours
        '
        Me.chkWithWorkingHours.AutoSize = True
        Me.chkWithWorkingHours.BackColor = System.Drawing.SystemColors.Control
        Me.chkWithWorkingHours.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWithWorkingHours.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWithWorkingHours.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWithWorkingHours.Location = New System.Drawing.Point(2, 16)
        Me.chkWithWorkingHours.Name = "chkWithWorkingHours"
        Me.chkWithWorkingHours.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWithWorkingHours.Size = New System.Drawing.Size(121, 18)
        Me.chkWithWorkingHours.TabIndex = 57
        Me.chkWithWorkingHours.Text = "With Working Hours"
        Me.chkWithWorkingHours.UseVisualStyleBackColor = False
        Me.chkWithWorkingHours.Visible = False
        '
        'chkPunchData
        '
        Me.chkPunchData.AutoSize = True
        Me.chkPunchData.BackColor = System.Drawing.SystemColors.Control
        Me.chkPunchData.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPunchData.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPunchData.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPunchData.Location = New System.Drawing.Point(2, 32)
        Me.chkPunchData.Name = "chkPunchData"
        Me.chkPunchData.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPunchData.Size = New System.Drawing.Size(113, 18)
        Me.chkPunchData.TabIndex = 56
        Me.chkPunchData.Text = "Show Punch Data"
        Me.chkPunchData.UseVisualStyleBackColor = False
        Me.chkPunchData.Visible = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lblRunDate)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(598, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(151, 75)
        Me.Frame2.TabIndex = 26
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Period"
        '
        'lblRunDate
        '
        Me.lblRunDate.CustomFormat = "MMMM,yyyy"
        Me.lblRunDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblRunDate.Location = New System.Drawing.Point(6, 26)
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblRunDate.Size = New System.Drawing.Size(138, 22)
        Me.lblRunDate.TabIndex = 36
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cmdWOPay)
        Me.Frame8.Controls.Add(Me.cmdAbsent)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(588, 71)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(161, 64)
        Me.Frame8.TabIndex = 51
        Me.Frame8.TabStop = False
        '
        'cmdWOPay
        '
        Me.cmdWOPay.AutoSize = True
        Me.cmdWOPay.BackColor = System.Drawing.SystemColors.Control
        Me.cmdWOPay.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdWOPay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdWOPay.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdWOPay.Location = New System.Drawing.Point(2, 37)
        Me.cmdWOPay.Name = "cmdWOPay"
        Me.cmdWOPay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdWOPay.Size = New System.Drawing.Size(155, 24)
        Me.cmdWOPay.TabIndex = 53
        Me.cmdWOPay.Text = "Mark W/o Pay for Not Punch"
        Me.cmdWOPay.UseVisualStyleBackColor = False
        '
        'cmdAbsent
        '
        Me.cmdAbsent.AutoSize = True
        Me.cmdAbsent.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAbsent.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAbsent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAbsent.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAbsent.Location = New System.Drawing.Point(2, 10)
        Me.cmdAbsent.Name = "cmdAbsent"
        Me.cmdAbsent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAbsent.Size = New System.Drawing.Size(155, 24)
        Me.cmdAbsent.TabIndex = 52
        Me.cmdAbsent.Text = "Mark Absent for Not Punch"
        Me.cmdAbsent.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cboEmpCatType)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(476, 71)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(113, 65)
        Me.Frame6.TabIndex = 47
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Emp. Category Type"
        '
        'cboEmpCatType
        '
        Me.cboEmpCatType.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmpCatType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmpCatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmpCatType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmpCatType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboEmpCatType.Location = New System.Drawing.Point(4, 28)
        Me.cboEmpCatType.Name = "cboEmpCatType"
        Me.cboEmpCatType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmpCatType.Size = New System.Drawing.Size(105, 22)
        Me.cboEmpCatType.TabIndex = 48
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.chkCategory)
        Me.Frame7.Controls.Add(Me.cboCatgeory)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(318, 71)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(159, 65)
        Me.Frame7.TabIndex = 18
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
        Me.chkCategory.Location = New System.Drawing.Point(112, 28)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 20
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'cboCatgeory
        '
        Me.cboCatgeory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCatgeory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCatgeory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCatgeory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCatgeory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCatgeory.Location = New System.Drawing.Point(2, 28)
        Me.cboCatgeory.Name = "cboCatgeory"
        Me.cboCatgeory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCatgeory.Size = New System.Drawing.Size(109, 22)
        Me.cboCatgeory.TabIndex = 19
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(2, 71)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(317, 67)
        Me.Frame4.TabIndex = 5
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
        Me.cboDept.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.Location = New System.Drawing.Point(2, 27)
        Me.cboDept.MaxLength = 50
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(310, 24)
        Me.cboDept.TabIndex = 109
        Me.cboDept.Tag = "9158"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.sprdAttn)
        Me.Frame1.Controls.Add(Me.sprdRemarks)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(2, 134)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(749, 440)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'sprdAttn
        '
        Me.sprdAttn.DataSource = Nothing
        Me.sprdAttn.Location = New System.Drawing.Point(2, 10)
        Me.sprdAttn.Name = "sprdAttn"
        Me.sprdAttn.OcxState = CType(resources.GetObject("sprdAttn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdAttn.Size = New System.Drawing.Size(743, 426)
        Me.sprdAttn.TabIndex = 1
        '
        'sprdRemarks
        '
        Me.sprdRemarks.DataSource = Nothing
        Me.sprdRemarks.Location = New System.Drawing.Point(2, 223)
        Me.sprdRemarks.Name = "sprdRemarks"
        Me.sprdRemarks.OcxState = CType(resources.GetObject("sprdRemarks.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdRemarks.Size = New System.Drawing.Size(743, 37)
        Me.sprdRemarks.TabIndex = 59
        Me.sprdRemarks.Visible = False
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me._lblColor_10)
        Me.FraMovement.Controls.Add(Me.cmdExport)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me._lblColor_9)
        Me.FraMovement.Controls.Add(Me._lblColor_8)
        Me.FraMovement.Controls.Add(Me._lblColor_6)
        Me.FraMovement.Controls.Add(Me._lblColor_7)
        Me.FraMovement.Controls.Add(Me._lblColor_5)
        Me.FraMovement.Controls.Add(Me._lblColor_4)
        Me.FraMovement.Controls.Add(Me._lblColor_3)
        Me.FraMovement.Controls.Add(Me._lblColor_2)
        Me.FraMovement.Controls.Add(Me._lblColor_1)
        Me.FraMovement.Controls.Add(Me._lblColor_0)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.TabIndex = 2
        Me.FraMovement.TabStop = False
        '
        '_lblColor_10
        '
        Me._lblColor_10.BackColor = System.Drawing.Color.White
        Me._lblColor_10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_10, CType(10, Short))
        Me._lblColor_10.Location = New System.Drawing.Point(78, 7)
        Me._lblColor_10.Name = "_lblColor_10"
        Me._lblColor_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_10.Size = New System.Drawing.Size(66, 19)
        Me._lblColor_10.TabIndex = 51
        Me._lblColor_10.Text = "Ok"
        Me._lblColor_10.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me._lblColor_10.Visible = False
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
        Me.cmdPrint.Size = New System.Drawing.Size(74, 34)
        Me.cmdPrint.TabIndex = 9
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(596, 12)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(74, 34)
        Me.cmdRefresh.TabIndex = 4
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(136, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 45
        '
        '_lblColor_9
        '
        Me._lblColor_9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._lblColor_9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_9, CType(9, Short))
        Me._lblColor_9.Location = New System.Drawing.Point(530, 28)
        Me._lblColor_9.Name = "_lblColor_9"
        Me._lblColor_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_9.Size = New System.Drawing.Size(66, 19)
        Me._lblColor_9.TabIndex = 50
        Me._lblColor_9.Text = "Manual"
        Me._lblColor_9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblColor_8
        '
        Me._lblColor_8.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me._lblColor_8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_8, CType(8, Short))
        Me._lblColor_8.Location = New System.Drawing.Point(530, 10)
        Me._lblColor_8.Name = "_lblColor_8"
        Me._lblColor_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_8.Size = New System.Drawing.Size(66, 19)
        Me._lblColor_8.TabIndex = 49
        Me._lblColor_8.Text = "Absent"
        Me._lblColor_8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblColor_6
        '
        Me._lblColor_6.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._lblColor_6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_6, CType(6, Short))
        Me._lblColor_6.Location = New System.Drawing.Point(454, 10)
        Me._lblColor_6.Name = "_lblColor_6"
        Me._lblColor_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_6.Size = New System.Drawing.Size(76, 19)
        Me._lblColor_6.TabIndex = 46
        Me._lblColor_6.Text = "CPL Earn"
        Me._lblColor_6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblColor_7
        '
        Me._lblColor_7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._lblColor_7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_7, CType(7, Short))
        Me._lblColor_7.Location = New System.Drawing.Point(454, 28)
        Me._lblColor_7.Name = "_lblColor_7"
        Me._lblColor_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_7.Size = New System.Drawing.Size(76, 19)
        Me._lblColor_7.TabIndex = 45
        Me._lblColor_7.Text = "CPL Avail"
        Me._lblColor_7.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblColor_5
        '
        Me._lblColor_5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me._lblColor_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_5, CType(5, Short))
        Me._lblColor_5.Location = New System.Drawing.Point(376, 28)
        Me._lblColor_5.Name = "_lblColor_5"
        Me._lblColor_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_5.Size = New System.Drawing.Size(76, 19)
        Me._lblColor_5.TabIndex = 40
        Me._lblColor_5.Text = "Short Leave"
        Me._lblColor_5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblColor_4
        '
        Me._lblColor_4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me._lblColor_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_4, CType(4, Short))
        Me._lblColor_4.Location = New System.Drawing.Point(376, 10)
        Me._lblColor_4.Name = "_lblColor_4"
        Me._lblColor_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_4.Size = New System.Drawing.Size(76, 19)
        Me._lblColor_4.TabIndex = 39
        Me._lblColor_4.Text = "Leave"
        Me._lblColor_4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblColor_3
        '
        Me._lblColor_3.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._lblColor_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_3, CType(3, Short))
        Me._lblColor_3.Location = New System.Drawing.Point(300, 28)
        Me._lblColor_3.Name = "_lblColor_3"
        Me._lblColor_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_3.Size = New System.Drawing.Size(76, 19)
        Me._lblColor_3.TabIndex = 38
        Me._lblColor_3.Text = "Holiday"
        Me._lblColor_3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblColor_2
        '
        Me._lblColor_2.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me._lblColor_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_2, CType(2, Short))
        Me._lblColor_2.Location = New System.Drawing.Point(300, 10)
        Me._lblColor_2.Name = "_lblColor_2"
        Me._lblColor_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_2.Size = New System.Drawing.Size(76, 19)
        Me._lblColor_2.TabIndex = 37
        Me._lblColor_2.Text = "Late Comers"
        Me._lblColor_2.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblColor_1
        '
        Me._lblColor_1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me._lblColor_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_1, CType(1, Short))
        Me._lblColor_1.Location = New System.Drawing.Point(224, 28)
        Me._lblColor_1.Name = "_lblColor_1"
        Me._lblColor_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_1.Size = New System.Drawing.Size(76, 19)
        Me._lblColor_1.TabIndex = 36
        Me._lblColor_1.Text = "O.D."
        Me._lblColor_1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        '_lblColor_0
        '
        Me._lblColor_0.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me._lblColor_0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me._lblColor_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblColor_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblColor_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblColor.SetIndex(Me._lblColor_0, CType(0, Short))
        Me._lblColor_0.Location = New System.Drawing.Point(224, 10)
        Me._lblColor_0.Name = "_lblColor_0"
        Me._lblColor_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblColor_0.Size = New System.Drawing.Size(76, 19)
        Me._lblColor_0.TabIndex = 35
        Me._lblColor_0.Text = "Not Punch"
        Me._lblColor_0.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(584, 22)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(64, 14)
        Me.lblBookType.TabIndex = 10
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
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(452, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(147, 73)
        Me.Frame5.TabIndex = 11
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
        Me.txtPageNo.Size = New System.Drawing.Size(33, 20)
        Me.txtPageNo.TabIndex = 15
        '
        'chkPageNo
        '
        Me.chkPageNo.AutoSize = True
        Me.chkPageNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkPageNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPageNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPageNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPageNo.Location = New System.Drawing.Point(100, 38)
        Me.chkPageNo.Name = "chkPageNo"
        Me.chkPageNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPageNo.Size = New System.Drawing.Size(46, 18)
        Me.chkPageNo.TabIndex = 14
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
        Me.txtBookNo.Size = New System.Drawing.Size(33, 20)
        Me.txtBookNo.TabIndex = 13
        '
        'chkBookNo
        '
        Me.chkBookNo.AutoSize = True
        Me.chkBookNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkBookNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBookNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBookNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBookNo.Location = New System.Drawing.Point(100, 14)
        Me.chkBookNo.Name = "chkBookNo"
        Me.chkBookNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBookNo.Size = New System.Drawing.Size(46, 18)
        Me.chkBookNo.TabIndex = 12
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
        Me.Label2.TabIndex = 17
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
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Book No :"
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
        Me.FraPreview.Size = New System.Drawing.Size(751, 618)
        Me.FraPreview.TabIndex = 41
        Me.FraPreview.TabStop = False
        Me.FraPreview.Visible = False
        '
        'SprdCommand
        '
        Me.SprdCommand.DataSource = Nothing
        Me.SprdCommand.Location = New System.Drawing.Point(2, 10)
        Me.SprdCommand.Name = "SprdCommand"
        Me.SprdCommand.OcxState = CType(resources.GetObject("SprdCommand.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdCommand.Size = New System.Drawing.Size(743, 29)
        Me.SprdCommand.TabIndex = 42
        '
        'SprdPreview
        '
        Me.SprdPreview.Location = New System.Drawing.Point(2, 40)
        Me.SprdPreview.Name = "SprdPreview"
        Me.SprdPreview.OcxState = CType(resources.GetObject("SprdPreview.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPreview.Size = New System.Drawing.Size(743, 578)
        Me.SprdPreview.TabIndex = 43
        '
        'frmEmpWiseMonthlyAttn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 621)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraSelection)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame9)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.FraPreview)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmEmpWiseMonthlyAttn"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Monthly Attendance Report"
        Me.FraSelection.ResumeLayout(False)
        Me.FraSelection.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.cboDept, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        CType(Me.sprdAttn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdRemarks, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.FraPreview.ResumeLayout(False)
        CType(Me.SprdCommand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdPreview, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblRunDate As DateTimePicker
    Friend WithEvents cboDept As Infragistics.Win.UltraWinGrid.UltraCombo
    Public WithEvents _lblColor_10 As Label
#End Region
End Class