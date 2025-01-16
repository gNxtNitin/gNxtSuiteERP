Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmpRegShiftChange
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
    Public WithEvents cmdShiftChange As System.Windows.Forms.Button
    Public WithEvents cmdUpdateSD As System.Windows.Forms.Button
    Public WithEvents cboShowShift As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents _optAll_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optAll_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents FraSelection As System.Windows.Forms.GroupBox
    Public WithEvents cboShift As System.Windows.Forms.ComboBox
    Public WithEvents cmdSet_G As System.Windows.Forms.Button
    Public WithEvents txtShiftG_IN As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtShiftG_OUT As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtShiftG_BS As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtShiftG_BE As System.Windows.Forms.MaskedTextBox
    Public WithEvents Shift As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents cmpPopulate As System.Windows.Forms.Button
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents optBook As System.Windows.Forms.RadioButton
    Public WithEvents optCardNo As System.Windows.Forms.RadioButton
    Public WithEvents OptName As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents sprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkBlankShift As System.Windows.Forms.CheckBox
    Public WithEvents cmdSaveMonthly As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents txtUpdateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtUpdateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents optAll As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpRegShiftChange))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdShiftChange = New System.Windows.Forms.Button()
        Me.cmdUpdateSD = New System.Windows.Forms.Button()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboShowShift = New System.Windows.Forms.ComboBox()
        Me.FraSelection = New System.Windows.Forms.GroupBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me._optAll_1 = New System.Windows.Forms.RadioButton()
        Me._optAll_0 = New System.Windows.Forms.RadioButton()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboShift = New System.Windows.Forms.ComboBox()
        Me.cmdSet_G = New System.Windows.Forms.Button()
        Me.txtShiftG_IN = New System.Windows.Forms.MaskedTextBox()
        Me.txtShiftG_OUT = New System.Windows.Forms.MaskedTextBox()
        Me.txtShiftG_BS = New System.Windows.Forms.MaskedTextBox()
        Me.txtShiftG_BE = New System.Windows.Forms.MaskedTextBox()
        Me.Shift = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmpPopulate = New System.Windows.Forms.Button()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.optDepartment = New System.Windows.Forms.RadioButton()
        Me.optBook = New System.Windows.Forms.RadioButton()
        Me.optCardNo = New System.Windows.Forms.RadioButton()
        Me.OptName = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.lblRunDate = New System.Windows.Forms.DateTimePicker()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.chkBlankShift = New System.Windows.Forms.CheckBox()
        Me.cmdSaveMonthly = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.txtUpdateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtUpdateTo = New System.Windows.Forms.MaskedTextBox()
        Me.optAll = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame7.SuspendLayout()
        Me.FraSelection.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.sprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optAll, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(106, 14)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 22)
        Me.cmdsearch.TabIndex = 34
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Location = New System.Drawing.Point(164, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(79, 34)
        Me.CmdPreview.TabIndex = 15
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
        Me.CmdClose.Location = New System.Drawing.Point(1020, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(80, 34)
        Me.CmdClose.TabIndex = 6
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdShiftChange
        '
        Me.cmdShiftChange.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShiftChange.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShiftChange.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShiftChange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShiftChange.Location = New System.Drawing.Point(880, 66)
        Me.cmdShiftChange.Name = "cmdShiftChange"
        Me.cmdShiftChange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShiftChange.Size = New System.Drawing.Size(221, 22)
        Me.cmdShiftChange.TabIndex = 46
        Me.cmdShiftChange.Text = "Shift Change as per IN Time"
        Me.cmdShiftChange.UseVisualStyleBackColor = False
        '
        'cmdUpdateSD
        '
        Me.cmdUpdateSD.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateSD.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdateSD.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdateSD.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateSD.Location = New System.Drawing.Point(880, 46)
        Me.cmdUpdateSD.Name = "cmdUpdateSD"
        Me.cmdUpdateSD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdateSD.Size = New System.Drawing.Size(221, 22)
        Me.cmdUpdateSD.TabIndex = 43
        Me.cmdUpdateSD.Text = "Update All Weekly Off"
        Me.cmdUpdateSD.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboShowShift)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(95, 0)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(139, 40)
        Me.Frame7.TabIndex = 44
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Show"
        '
        'cboShowShift
        '
        Me.cboShowShift.BackColor = System.Drawing.SystemColors.Window
        Me.cboShowShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShowShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShowShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShowShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShowShift.Location = New System.Drawing.Point(3, 14)
        Me.cboShowShift.Name = "cboShowShift"
        Me.cboShowShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShowShift.Size = New System.Drawing.Size(133, 22)
        Me.cboShowShift.TabIndex = 45
        '
        'FraSelection
        '
        Me.FraSelection.BackColor = System.Drawing.SystemColors.Control
        Me.FraSelection.Controls.Add(Me.txtName)
        Me.FraSelection.Controls.Add(Me._optAll_1)
        Me.FraSelection.Controls.Add(Me._optAll_0)
        Me.FraSelection.Controls.Add(Me.txtEmpCode)
        Me.FraSelection.Controls.Add(Me.cmdsearch)
        Me.FraSelection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraSelection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraSelection.Location = New System.Drawing.Point(235, 42)
        Me.FraSelection.Name = "FraSelection"
        Me.FraSelection.Padding = New System.Windows.Forms.Padding(0)
        Me.FraSelection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraSelection.Size = New System.Drawing.Size(359, 68)
        Me.FraSelection.TabIndex = 33
        Me.FraSelection.TabStop = False
        Me.FraSelection.Text = "Employee"
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
        Me.txtName.Location = New System.Drawing.Point(2, 40)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(352, 20)
        Me.txtName.TabIndex = 38
        '
        '_optAll_1
        '
        Me._optAll_1.AutoSize = True
        Me._optAll_1.BackColor = System.Drawing.SystemColors.Control
        Me._optAll_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAll_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAll_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAll.SetIndex(Me._optAll_1, CType(1, Short))
        Me._optAll_1.Location = New System.Drawing.Point(182, 17)
        Me._optAll_1.Name = "_optAll_1"
        Me._optAll_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAll_1.Size = New System.Drawing.Size(76, 18)
        Me._optAll_1.TabIndex = 37
        Me._optAll_1.TabStop = True
        Me._optAll_1.Text = "Particulars"
        Me._optAll_1.UseVisualStyleBackColor = False
        '
        '_optAll_0
        '
        Me._optAll_0.AutoSize = True
        Me._optAll_0.BackColor = System.Drawing.SystemColors.Control
        Me._optAll_0.Checked = True
        Me._optAll_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAll_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAll_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAll.SetIndex(Me._optAll_0, CType(0, Short))
        Me._optAll_0.Location = New System.Drawing.Point(138, 17)
        Me._optAll_0.Name = "_optAll_0"
        Me._optAll_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAll_0.Size = New System.Drawing.Size(37, 18)
        Me._optAll_0.TabIndex = 36
        Me._optAll_0.TabStop = True
        Me._optAll_0.Text = "All"
        Me._optAll_0.UseVisualStyleBackColor = False
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(2, 14)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(103, 20)
        Me.txtEmpCode.TabIndex = 35
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cboShift)
        Me.Frame5.Controls.Add(Me.cmdSet_G)
        Me.Frame5.Controls.Add(Me.txtShiftG_IN)
        Me.Frame5.Controls.Add(Me.txtShiftG_OUT)
        Me.Frame5.Controls.Add(Me.txtShiftG_BS)
        Me.Frame5.Controls.Add(Me.txtShiftG_BE)
        Me.Frame5.Controls.Add(Me.Shift)
        Me.Frame5.Controls.Add(Me.Label4)
        Me.Frame5.Controls.Add(Me.Label3)
        Me.Frame5.Controls.Add(Me.Label1)
        Me.Frame5.Controls.Add(Me.Label2)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(598, 42)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(280, 68)
        Me.Frame5.TabIndex = 21
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Shift Default Time"
        '
        'cboShift
        '
        Me.cboShift.BackColor = System.Drawing.SystemColors.Window
        Me.cboShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShift.Location = New System.Drawing.Point(49, 40)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShift.Size = New System.Drawing.Size(73, 22)
        Me.cboShift.TabIndex = 31
        '
        'cmdSet_G
        '
        Me.cmdSet_G.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSet_G.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSet_G.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSet_G.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSet_G.Location = New System.Drawing.Point(48, 14)
        Me.cmdSet_G.Name = "cmdSet_G"
        Me.cmdSet_G.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSet_G.Size = New System.Drawing.Size(74, 22)
        Me.cmdSet_G.TabIndex = 30
        Me.cmdSet_G.Text = "Set"
        Me.cmdSet_G.UseVisualStyleBackColor = False
        '
        'txtShiftG_IN
        '
        Me.txtShiftG_IN.AllowPromptAsInput = False
        Me.txtShiftG_IN.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftG_IN.Location = New System.Drawing.Point(150, 14)
        Me.txtShiftG_IN.Mask = "##:##"
        Me.txtShiftG_IN.Name = "txtShiftG_IN"
        Me.txtShiftG_IN.Size = New System.Drawing.Size(43, 20)
        Me.txtShiftG_IN.TabIndex = 24
        '
        'txtShiftG_OUT
        '
        Me.txtShiftG_OUT.AllowPromptAsInput = False
        Me.txtShiftG_OUT.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftG_OUT.Location = New System.Drawing.Point(232, 14)
        Me.txtShiftG_OUT.Mask = "##:##"
        Me.txtShiftG_OUT.Name = "txtShiftG_OUT"
        Me.txtShiftG_OUT.Size = New System.Drawing.Size(43, 20)
        Me.txtShiftG_OUT.TabIndex = 25
        '
        'txtShiftG_BS
        '
        Me.txtShiftG_BS.AllowPromptAsInput = False
        Me.txtShiftG_BS.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftG_BS.Location = New System.Drawing.Point(151, 40)
        Me.txtShiftG_BS.Mask = "##:##"
        Me.txtShiftG_BS.Name = "txtShiftG_BS"
        Me.txtShiftG_BS.Size = New System.Drawing.Size(43, 20)
        Me.txtShiftG_BS.TabIndex = 26
        '
        'txtShiftG_BE
        '
        Me.txtShiftG_BE.AllowPromptAsInput = False
        Me.txtShiftG_BE.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShiftG_BE.Location = New System.Drawing.Point(233, 40)
        Me.txtShiftG_BE.Mask = "##:##"
        Me.txtShiftG_BE.Name = "txtShiftG_BE"
        Me.txtShiftG_BE.Size = New System.Drawing.Size(43, 20)
        Me.txtShiftG_BE.TabIndex = 27
        '
        'Shift
        '
        Me.Shift.BackColor = System.Drawing.SystemColors.Control
        Me.Shift.Cursor = System.Windows.Forms.Cursors.Default
        Me.Shift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Shift.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Shift.Location = New System.Drawing.Point(10, 42)
        Me.Shift.Name = "Shift"
        Me.Shift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Shift.Size = New System.Drawing.Size(37, 11)
        Me.Shift.TabIndex = 32
        Me.Shift.Text = "Shift :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(126, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(33, 14)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "B.S. :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(198, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(32, 14)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "B.E. :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(126, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(22, 14)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "IN :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(198, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(34, 14)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "OUT :"
        '
        'cmpPopulate
        '
        Me.cmpPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmpPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmpPopulate.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmpPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmpPopulate.Location = New System.Drawing.Point(880, 87)
        Me.cmpPopulate.Name = "cmpPopulate"
        Me.cmpPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmpPopulate.Size = New System.Drawing.Size(221, 22)
        Me.cmpPopulate.TabIndex = 20
        Me.cmpPopulate.Text = "Populate Last Day"
        Me.cmpPopulate.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.chkCategory)
        Me.Frame6.Controls.Add(Me.cboCategory)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(600, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(286, 41)
        Me.Frame6.TabIndex = 17
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Category"
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(234, 16)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 19
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
        Me.cboCategory.Location = New System.Drawing.Point(6, 16)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(224, 22)
        Me.cboCategory.TabIndex = 18
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkAll)
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(236, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(362, 41)
        Me.Frame4.TabIndex = 9
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Department"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(312, 16)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 13
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(4, 16)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(304, 22)
        Me.cboDept.TabIndex = 12
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.optDepartment)
        Me.Frame3.Controls.Add(Me.optBook)
        Me.Frame3.Controls.Add(Me.optCardNo)
        Me.Frame3.Controls.Add(Me.OptName)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(94, 110)
        Me.Frame3.TabIndex = 8
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Order By"
        '
        'optDepartment
        '
        Me.optDepartment.AutoSize = True
        Me.optDepartment.BackColor = System.Drawing.SystemColors.Control
        Me.optDepartment.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDepartment.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDepartment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDepartment.Location = New System.Drawing.Point(6, 62)
        Me.optDepartment.Name = "optDepartment"
        Me.optDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDepartment.Size = New System.Drawing.Size(80, 18)
        Me.optDepartment.TabIndex = 40
        Me.optDepartment.TabStop = True
        Me.optDepartment.Text = "Department"
        Me.optDepartment.UseVisualStyleBackColor = False
        '
        'optBook
        '
        Me.optBook.AutoSize = True
        Me.optBook.BackColor = System.Drawing.SystemColors.Control
        Me.optBook.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBook.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBook.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBook.Location = New System.Drawing.Point(6, 86)
        Me.optBook.Name = "optBook"
        Me.optBook.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBook.Size = New System.Drawing.Size(65, 18)
        Me.optBook.TabIndex = 39
        Me.optBook.TabStop = True
        Me.optBook.Text = "Book No"
        Me.optBook.UseVisualStyleBackColor = False
        Me.optBook.Visible = False
        '
        'optCardNo
        '
        Me.optCardNo.AutoSize = True
        Me.optCardNo.BackColor = System.Drawing.SystemColors.Control
        Me.optCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCardNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCardNo.Location = New System.Drawing.Point(6, 38)
        Me.optCardNo.Name = "optCardNo"
        Me.optCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCardNo.Size = New System.Drawing.Size(64, 18)
        Me.optCardNo.TabIndex = 11
        Me.optCardNo.TabStop = True
        Me.optCardNo.Text = "Card No"
        Me.optCardNo.UseVisualStyleBackColor = False
        '
        'OptName
        '
        Me.OptName.AutoSize = True
        Me.OptName.BackColor = System.Drawing.SystemColors.Control
        Me.OptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptName.Location = New System.Drawing.Point(6, 14)
        Me.OptName.Name = "OptName"
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Size = New System.Drawing.Size(52, 18)
        Me.OptName.TabIndex = 10
        Me.OptName.TabStop = True
        Me.OptName.Text = "Name"
        Me.OptName.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lblRunDate)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(887, -1)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(217, 43)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        '
        'lblRunDate
        '
        Me.lblRunDate.CustomFormat = "dd MMMM,yyyy"
        Me.lblRunDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblRunDate.Location = New System.Drawing.Point(5, 13)
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblRunDate.Size = New System.Drawing.Size(207, 22)
        Me.lblRunDate.TabIndex = 36
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.sprdMain)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 104)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1108, 470)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'sprdMain
        '
        Me.sprdMain.DataSource = Nothing
        Me.sprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdMain.Location = New System.Drawing.Point(0, 13)
        Me.sprdMain.Name = "sprdMain"
        Me.sprdMain.OcxState = CType(resources.GetObject("sprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdMain.Size = New System.Drawing.Size(1108, 457)
        Me.sprdMain.TabIndex = 4
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Label5)
        Me.FraMovement.Controls.Add(Me.Label39)
        Me.FraMovement.Controls.Add(Me.chkBlankShift)
        Me.FraMovement.Controls.Add(Me.cmdSaveMonthly)
        Me.FraMovement.Controls.Add(Me.cmdSave)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.txtUpdateFrom)
        Me.FraMovement.Controls.Add(Me.txtUpdateTo)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(1, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1103, 51)
        Me.FraMovement.TabIndex = 5
        Me.FraMovement.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(573, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(37, 14)
        Me.Label5.TabIndex = 128
        Me.Label5.Text = "From :"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(466, 23)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(24, 14)
        Me.Label39.TabIndex = 127
        Me.Label39.Text = "To :"
        '
        'chkBlankShift
        '
        Me.chkBlankShift.AutoSize = True
        Me.chkBlankShift.BackColor = System.Drawing.SystemColors.Control
        Me.chkBlankShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBlankShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBlankShift.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBlankShift.Location = New System.Drawing.Point(694, 21)
        Me.chkBlankShift.Name = "chkBlankShift"
        Me.chkBlankShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBlankShift.Size = New System.Drawing.Size(109, 18)
        Me.chkBlankShift.TabIndex = 47
        Me.chkBlankShift.Text = "Show Blank Shift"
        Me.chkBlankShift.UseVisualStyleBackColor = False
        '
        'cmdSaveMonthly
        '
        Me.cmdSaveMonthly.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSaveMonthly.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSaveMonthly.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveMonthly.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaveMonthly.Location = New System.Drawing.Point(322, 12)
        Me.cmdSaveMonthly.Name = "cmdSaveMonthly"
        Me.cmdSaveMonthly.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSaveMonthly.Size = New System.Drawing.Size(126, 34)
        Me.cmdSaveMonthly.TabIndex = 40
        Me.cmdSaveMonthly.Text = "Save For the Period"
        Me.cmdSaveMonthly.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(242, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(80, 34)
        Me.cmdSave.TabIndex = 16
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(84, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(80, 34)
        Me.cmdPrint.TabIndex = 14
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(4, 12)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 34)
        Me.cmdRefresh.TabIndex = 7
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
        Me.Report1.TabIndex = 48
        '
        'txtUpdateFrom
        '
        Me.txtUpdateFrom.AllowPromptAsInput = False
        Me.txtUpdateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUpdateFrom.Location = New System.Drawing.Point(494, 20)
        Me.txtUpdateFrom.Mask = "##/##/####"
        Me.txtUpdateFrom.Name = "txtUpdateFrom"
        Me.txtUpdateFrom.Size = New System.Drawing.Size(74, 20)
        Me.txtUpdateFrom.TabIndex = 41
        '
        'txtUpdateTo
        '
        Me.txtUpdateTo.AllowPromptAsInput = False
        Me.txtUpdateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUpdateTo.Location = New System.Drawing.Point(614, 20)
        Me.txtUpdateTo.Mask = "##/##/####"
        Me.txtUpdateTo.Name = "txtUpdateTo"
        Me.txtUpdateTo.Size = New System.Drawing.Size(76, 20)
        Me.txtUpdateTo.TabIndex = 42
        '
        'optAll
        '
        '
        'frmEmpRegShiftChange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.cmdShiftChange)
        Me.Controls.Add(Me.cmdUpdateSD)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.FraSelection)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.cmpPopulate)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 15)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpRegShiftChange"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Shift Change Master"
        Me.Frame7.ResumeLayout(False)
        Me.FraSelection.ResumeLayout(False)
        Me.FraSelection.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        CType(Me.sprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optAll, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblRunDate As DateTimePicker
    Public WithEvents Label5 As Label
    Public WithEvents Label39 As Label
    Public WithEvents optDepartment As RadioButton
#End Region
End Class