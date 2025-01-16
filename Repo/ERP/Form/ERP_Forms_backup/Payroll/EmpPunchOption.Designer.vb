Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmpPunchOption
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
    Public WithEvents TxtEmpName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents _optPunch_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optPunch_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPunch_2 As System.Windows.Forms.RadioButton
    Public WithEvents txtStopDate As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _optShift_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optShift_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShift_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents optPunch As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optShift As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpPunchOption))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.TxtEmpName = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optPunch_3 = New System.Windows.Forms.RadioButton()
        Me._optPunch_0 = New System.Windows.Forms.RadioButton()
        Me._optPunch_1 = New System.Windows.Forms.RadioButton()
        Me._optPunch_2 = New System.Windows.Forms.RadioButton()
        Me.txtStopDate = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optShift_2 = New System.Windows.Forms.RadioButton()
        Me._optShift_1 = New System.Windows.Forms.RadioButton()
        Me._optShift_0 = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.optPunch = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optShift = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraView.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.optPunch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShift, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(184, 14)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearch.TabIndex = 16
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(2, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
        Me.CmdSave.TabIndex = 2
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(484, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 1
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Controls.Add(Me.TxtEmpName)
        Me.FraView.Controls.Add(Me.cmdSearch)
        Me.FraView.Controls.Add(Me.txtDept)
        Me.FraView.Controls.Add(Me.txtEmpCode)
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.Frame2)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, 0)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(549, 171)
        Me.FraView.TabIndex = 3
        Me.FraView.TabStop = False
        '
        'TxtEmpName
        '
        Me.TxtEmpName.AcceptsReturn = True
        Me.TxtEmpName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEmpName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtEmpName.Enabled = False
        Me.TxtEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmpName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtEmpName.Location = New System.Drawing.Point(90, 34)
        Me.TxtEmpName.MaxLength = 0
        Me.TxtEmpName.Name = "TxtEmpName"
        Me.TxtEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtEmpName.Size = New System.Drawing.Size(319, 20)
        Me.TxtEmpName.TabIndex = 17
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Enabled = False
        Me.txtDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDept.Location = New System.Drawing.Point(90, 54)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(92, 20)
        Me.txtDept.TabIndex = 15
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(90, 14)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(92, 20)
        Me.txtEmpCode.TabIndex = 14
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame1.Controls.Add(Me._optPunch_3)
        Me.Frame1.Controls.Add(Me._optPunch_0)
        Me.Frame1.Controls.Add(Me._optPunch_1)
        Me.Frame1.Controls.Add(Me._optPunch_2)
        Me.Frame1.Controls.Add(Me.txtStopDate)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(2, 76)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(547, 45)
        Me.Frame1.TabIndex = 8
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Punch Option"
        '
        '_optPunch_3
        '
        Me._optPunch_3.AutoSize = True
        Me._optPunch_3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optPunch_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPunch_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPunch_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPunch.SetIndex(Me._optPunch_3, CType(3, Short))
        Me._optPunch_3.Location = New System.Drawing.Point(221, 18)
        Me._optPunch_3.Name = "_optPunch_3"
        Me._optPunch_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPunch_3.Size = New System.Drawing.Size(59, 18)
        Me._optPunch_3.TabIndex = 14
        Me._optPunch_3.TabStop = True
        Me._optPunch_3.Text = "OnLine"
        Me._optPunch_3.UseVisualStyleBackColor = False
        '
        '_optPunch_0
        '
        Me._optPunch_0.AutoSize = True
        Me._optPunch_0.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optPunch_0.Checked = True
        Me._optPunch_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPunch_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPunch_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPunch.SetIndex(Me._optPunch_0, CType(0, Short))
        Me._optPunch_0.Location = New System.Drawing.Point(8, 18)
        Me._optPunch_0.Name = "_optPunch_0"
        Me._optPunch_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPunch_0.Size = New System.Drawing.Size(69, 18)
        Me._optPunch_0.TabIndex = 12
        Me._optPunch_0.TabStop = True
        Me._optPunch_0.Text = "Punching"
        Me._optPunch_0.UseVisualStyleBackColor = False
        '
        '_optPunch_1
        '
        Me._optPunch_1.AutoSize = True
        Me._optPunch_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optPunch_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPunch_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPunch_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPunch.SetIndex(Me._optPunch_1, CType(1, Short))
        Me._optPunch_1.Location = New System.Drawing.Point(116, 18)
        Me._optPunch_1.Name = "_optPunch_1"
        Me._optPunch_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPunch_1.Size = New System.Drawing.Size(65, 18)
        Me._optPunch_1.TabIndex = 11
        Me._optPunch_1.TabStop = True
        Me._optPunch_1.Text = "Mannual"
        Me._optPunch_1.UseVisualStyleBackColor = False
        '
        '_optPunch_2
        '
        Me._optPunch_2.AutoSize = True
        Me._optPunch_2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optPunch_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPunch_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPunch_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPunch.SetIndex(Me._optPunch_2, CType(2, Short))
        Me._optPunch_2.Location = New System.Drawing.Point(324, 18)
        Me._optPunch_2.Name = "_optPunch_2"
        Me._optPunch_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPunch_2.Size = New System.Drawing.Size(47, 18)
        Me._optPunch_2.TabIndex = 10
        Me._optPunch_2.TabStop = True
        Me._optPunch_2.Text = "Stop"
        Me._optPunch_2.UseVisualStyleBackColor = False
        '
        'txtStopDate
        '
        Me.txtStopDate.AcceptsReturn = True
        Me.txtStopDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtStopDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStopDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStopDate.Enabled = False
        Me.txtStopDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStopDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtStopDate.Location = New System.Drawing.Point(454, 16)
        Me.txtStopDate.MaxLength = 10
        Me.txtStopDate.Name = "txtStopDate"
        Me.txtStopDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStopDate.Size = New System.Drawing.Size(75, 20)
        Me.txtStopDate.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(415, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(35, 14)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame2.Controls.Add(Me._optShift_2)
        Me.Frame2.Controls.Add(Me._optShift_1)
        Me.Frame2.Controls.Add(Me._optShift_0)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(2, 124)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(547, 45)
        Me.Frame2.TabIndex = 4
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Shift Option"
        '
        '_optShift_2
        '
        Me._optShift_2.AutoSize = True
        Me._optShift_2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optShift_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShift_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShift_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShift.SetIndex(Me._optShift_2, CType(2, Short))
        Me._optShift_2.Location = New System.Drawing.Point(324, 18)
        Me._optShift_2.Name = "_optShift_2"
        Me._optShift_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShift_2.Size = New System.Drawing.Size(51, 18)
        Me._optShift_2.TabIndex = 7
        Me._optShift_2.TabStop = True
        Me._optShift_2.Text = "Open"
        Me._optShift_2.UseVisualStyleBackColor = False
        '
        '_optShift_1
        '
        Me._optShift_1.AutoSize = True
        Me._optShift_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optShift_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShift_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShift_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShift.SetIndex(Me._optShift_1, CType(1, Short))
        Me._optShift_1.Location = New System.Drawing.Point(190, 18)
        Me._optShift_1.Name = "_optShift_1"
        Me._optShift_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShift_1.Size = New System.Drawing.Size(102, 18)
        Me._optShift_1.TabIndex = 6
        Me._optShift_1.TabStop = True
        Me._optShift_1.Text = "Regular Change"
        Me._optShift_1.UseVisualStyleBackColor = False
        '
        '_optShift_0
        '
        Me._optShift_0.AutoSize = True
        Me._optShift_0.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._optShift_0.Checked = True
        Me._optShift_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShift_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShift_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShift.SetIndex(Me._optShift_0, CType(0, Short))
        Me._optShift_0.Location = New System.Drawing.Point(86, 18)
        Me._optShift_0.Name = "_optShift_0"
        Me._optShift_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShift_0.Size = New System.Drawing.Size(63, 18)
        Me._optShift_0.TabIndex = 5
        Me._optShift_0.TabStop = True
        Me._optShift_0.Text = "General"
        Me._optShift_0.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(35, 14)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Dept :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "Emp Code :"
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 160)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(550, 53)
        Me.FraMovement.TabIndex = 0
        Me.FraMovement.TabStop = False
        '
        'optPunch
        '
        '
        'optShift
        '
        '
        'frmEmpPunchOption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(550, 213)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(21, 91)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpPunchOption"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Punch Option"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.optPunch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShift, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents _optPunch_3 As RadioButton
#End Region
End Class