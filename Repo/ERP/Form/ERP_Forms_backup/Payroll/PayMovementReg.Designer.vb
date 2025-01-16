Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPayMovementReg
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
    Public WithEvents _optHRApp_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optHRApp_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optHRApp_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _optMoveType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optMoveType_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optMoveType_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents chkAllEmp As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents FraSelection As System.Windows.Forms.GroupBox
    Public WithEvents txtTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents sprdAttn As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents optHRApp As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optMoveType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPayMovementReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.FraSelection = New System.Windows.Forms.GroupBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._optHRApp_2 = New System.Windows.Forms.RadioButton()
        Me._optHRApp_1 = New System.Windows.Forms.RadioButton()
        Me._optHRApp_0 = New System.Windows.Forms.RadioButton()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optMoveType_2 = New System.Windows.Forms.RadioButton()
        Me._optMoveType_0 = New System.Windows.Forms.RadioButton()
        Me._optMoveType_1 = New System.Windows.Forms.RadioButton()
        Me.chkAllEmp = New System.Windows.Forms.CheckBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdAttn = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.optHRApp = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optMoveType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me._optMoveType_3 = New System.Windows.Forms.RadioButton()
        Me.FraSelection.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.sprdAttn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optHRApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optMoveType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Enabled = False
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(433, 12)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdsearch.TabIndex = 15
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Location = New System.Drawing.Point(1016, 14)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(80, 34)
        Me.CmdClose.TabIndex = 3
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Location = New System.Drawing.Point(938, 14)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(79, 34)
        Me.CmdPreview.TabIndex = 6
        Me.CmdPreview.Text = "Pre&view"
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'FraSelection
        '
        Me.FraSelection.BackColor = System.Drawing.SystemColors.Control
        Me.FraSelection.Controls.Add(Me.Frame4)
        Me.FraSelection.Controls.Add(Me.Frame3)
        Me.FraSelection.Controls.Add(Me.chkAllEmp)
        Me.FraSelection.Controls.Add(Me.cmdsearch)
        Me.FraSelection.Controls.Add(Me.txtEmpCode)
        Me.FraSelection.Controls.Add(Me.txtName)
        Me.FraSelection.Controls.Add(Me.Label4)
        Me.FraSelection.Controls.Add(Me.Label3)
        Me.FraSelection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraSelection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraSelection.Location = New System.Drawing.Point(130, 0)
        Me.FraSelection.Name = "FraSelection"
        Me.FraSelection.Padding = New System.Windows.Forms.Padding(0)
        Me.FraSelection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraSelection.Size = New System.Drawing.Size(976, 83)
        Me.FraSelection.TabIndex = 12
        Me.FraSelection.TabStop = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._optHRApp_2)
        Me.Frame4.Controls.Add(Me._optHRApp_1)
        Me.Frame4.Controls.Add(Me._optHRApp_0)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(777, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(197, 35)
        Me.Frame4.TabIndex = 22
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "HR Approval"
        '
        '_optHRApp_2
        '
        Me._optHRApp_2.AutoSize = True
        Me._optHRApp_2.BackColor = System.Drawing.SystemColors.Control
        Me._optHRApp_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optHRApp_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optHRApp_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optHRApp.SetIndex(Me._optHRApp_2, CType(2, Short))
        Me._optHRApp_2.Location = New System.Drawing.Point(134, 16)
        Me._optHRApp_2.Name = "_optHRApp_2"
        Me._optHRApp_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optHRApp_2.Size = New System.Drawing.Size(38, 18)
        Me._optHRApp_2.TabIndex = 25
        Me._optHRApp_2.TabStop = True
        Me._optHRApp_2.Text = "No"
        Me._optHRApp_2.UseVisualStyleBackColor = False
        '
        '_optHRApp_1
        '
        Me._optHRApp_1.AutoSize = True
        Me._optHRApp_1.BackColor = System.Drawing.SystemColors.Control
        Me._optHRApp_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optHRApp_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optHRApp_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optHRApp.SetIndex(Me._optHRApp_1, CType(1, Short))
        Me._optHRApp_1.Location = New System.Drawing.Point(70, 16)
        Me._optHRApp_1.Name = "_optHRApp_1"
        Me._optHRApp_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optHRApp_1.Size = New System.Drawing.Size(44, 18)
        Me._optHRApp_1.TabIndex = 24
        Me._optHRApp_1.TabStop = True
        Me._optHRApp_1.Text = "Yes"
        Me._optHRApp_1.UseVisualStyleBackColor = False
        '
        '_optHRApp_0
        '
        Me._optHRApp_0.AutoSize = True
        Me._optHRApp_0.BackColor = System.Drawing.SystemColors.Control
        Me._optHRApp_0.Checked = True
        Me._optHRApp_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optHRApp_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optHRApp_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optHRApp.SetIndex(Me._optHRApp_0, CType(0, Short))
        Me._optHRApp_0.Location = New System.Drawing.Point(4, 16)
        Me._optHRApp_0.Name = "_optHRApp_0"
        Me._optHRApp_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optHRApp_0.Size = New System.Drawing.Size(47, 18)
        Me._optHRApp_0.TabIndex = 23
        Me._optHRApp_0.TabStop = True
        Me._optHRApp_0.Text = "Both"
        Me._optHRApp_0.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optMoveType_3)
        Me.Frame3.Controls.Add(Me._optMoveType_2)
        Me.Frame3.Controls.Add(Me._optMoveType_0)
        Me.Frame3.Controls.Add(Me._optMoveType_1)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(242, 85)
        Me.Frame3.TabIndex = 19
        Me.Frame3.TabStop = False
        '
        '_optMoveType_2
        '
        Me._optMoveType_2.AutoSize = True
        Me._optMoveType_2.BackColor = System.Drawing.SystemColors.Control
        Me._optMoveType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMoveType_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMoveType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMoveType.SetIndex(Me._optMoveType_2, CType(2, Short))
        Me._optMoveType_2.Location = New System.Drawing.Point(84, 19)
        Me._optMoveType_2.Name = "_optMoveType_2"
        Me._optMoveType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMoveType_2.Size = New System.Drawing.Size(83, 18)
        Me._optMoveType_2.TabIndex = 26
        Me._optMoveType_2.TabStop = True
        Me._optMoveType_2.Text = "Manual (I/O)"
        Me._optMoveType_2.UseVisualStyleBackColor = False
        '
        '_optMoveType_0
        '
        Me._optMoveType_0.AutoSize = True
        Me._optMoveType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optMoveType_0.Checked = True
        Me._optMoveType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMoveType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMoveType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMoveType.SetIndex(Me._optMoveType_0, CType(0, Short))
        Me._optMoveType_0.Location = New System.Drawing.Point(8, 19)
        Me._optMoveType_0.Name = "_optMoveType_0"
        Me._optMoveType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMoveType_0.Size = New System.Drawing.Size(59, 18)
        Me._optMoveType_0.TabIndex = 21
        Me._optMoveType_0.TabStop = True
        Me._optMoveType_0.Text = "Official"
        Me._optMoveType_0.UseVisualStyleBackColor = False
        '
        '_optMoveType_1
        '
        Me._optMoveType_1.AutoSize = True
        Me._optMoveType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optMoveType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMoveType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMoveType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMoveType.SetIndex(Me._optMoveType_1, CType(1, Short))
        Me._optMoveType_1.Location = New System.Drawing.Point(8, 54)
        Me._optMoveType_1.Name = "_optMoveType_1"
        Me._optMoveType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMoveType_1.Size = New System.Drawing.Size(67, 18)
        Me._optMoveType_1.TabIndex = 20
        Me._optMoveType_1.TabStop = True
        Me._optMoveType_1.Text = "Personal"
        Me._optMoveType_1.UseVisualStyleBackColor = False
        '
        'chkAllEmp
        '
        Me.chkAllEmp.AutoSize = True
        Me.chkAllEmp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllEmp.Checked = True
        Me.chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllEmp.Location = New System.Drawing.Point(463, 14)
        Me.chkAllEmp.Name = "chkAllEmp"
        Me.chkAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllEmp.Size = New System.Drawing.Size(38, 18)
        Me.chkAllEmp.TabIndex = 18
        Me.chkAllEmp.Text = "All"
        Me.chkAllEmp.UseVisualStyleBackColor = False
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Enabled = False
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(361, 12)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(71, 20)
        Me.txtEmpCode.TabIndex = 14
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
        Me.txtName.Location = New System.Drawing.Point(361, 36)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(365, 20)
        Me.txtName.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(257, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(89, 14)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Employee Name :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(255, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(87, 14)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Employee Code :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtTo)
        Me.Frame2.Controls.Add(Me.txtFrom)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(129, 87)
        Me.Frame2.TabIndex = 7
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Period"
        '
        'txtTo
        '
        Me.txtTo.AllowPromptAsInput = False
        Me.txtTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.Location = New System.Drawing.Point(42, 57)
        Me.txtTo.Mask = "##/##/####"
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(81, 20)
        Me.txtTo.TabIndex = 8
        '
        'txtFrom
        '
        Me.txtFrom.AllowPromptAsInput = False
        Me.txtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.Location = New System.Drawing.Point(42, 23)
        Me.txtFrom.Mask = "##/##/####"
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtFrom.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 63)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(24, 14)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "To :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(37, 14)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "From :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.sprdAttn)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 82)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1106, 493)
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
        Me.sprdAttn.Size = New System.Drawing.Size(1106, 480)
        Me.sprdAttn.TabIndex = 1
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 568)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1106, 51)
        Me.FraMovement.TabIndex = 2
        Me.FraMovement.TabStop = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(858, 14)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(80, 34)
        Me.cmdPrint.TabIndex = 5
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(778, 14)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 34)
        Me.cmdRefresh.TabIndex = 4
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(270, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 7
        '
        '_optMoveType_3
        '
        Me._optMoveType_3.AutoSize = True
        Me._optMoveType_3.BackColor = System.Drawing.SystemColors.Control
        Me._optMoveType_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMoveType_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMoveType_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMoveType.SetIndex(Me._optMoveType_3, CType(3, Short))
        Me._optMoveType_3.Location = New System.Drawing.Point(84, 54)
        Me._optMoveType_3.Name = "_optMoveType_3"
        Me._optMoveType_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMoveType_3.Size = New System.Drawing.Size(130, 18)
        Me._optMoveType_3.TabIndex = 27
        Me._optMoveType_3.TabStop = True
        Me._optMoveType_3.Text = "Personal (W/o Adjust)"
        Me._optMoveType_3.UseVisualStyleBackColor = False
        '
        'frmPayMovementReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.FraSelection)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmPayMovementReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Movement Register"
        Me.FraSelection.ResumeLayout(False)
        Me.FraSelection.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.sprdAttn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optHRApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optMoveType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'sprdAttn.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        sprdAttn.DataSource = Nothing
    End Sub

    Public WithEvents _optMoveType_3 As RadioButton
#End Region
End Class