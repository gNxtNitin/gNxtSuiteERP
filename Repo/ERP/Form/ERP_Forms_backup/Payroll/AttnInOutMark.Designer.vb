Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAttnInOutMark
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
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
    Public WithEvents chkClear As System.Windows.Forms.CheckBox
    Public WithEvents txtOUTTime As System.Windows.Forms.TextBox
    Public WithEvents txtINTime As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents fraFH As System.Windows.Forms.GroupBox
    Public WithEvents lblDate As System.Windows.Forms.Label
    Public WithEvents lblEmpName As System.Windows.Forms.Label
    Public WithEvents fraMain As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents lblType As System.Windows.Forms.Label
    Public WithEvents lblCode As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents optOTType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAttnInOutMark))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.fraMain = New System.Windows.Forms.GroupBox()
        Me.fraShift = New System.Windows.Forms.GroupBox()
        Me.chkRoundClock = New System.Windows.Forms.CheckBox()
        Me.cmdShiftChange = New System.Windows.Forms.Button()
        Me.txtBreakTo = New System.Windows.Forms.TextBox()
        Me.txtBreakFrom = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboShift = New System.Windows.Forms.ComboBox()
        Me.Shift = New System.Windows.Forms.Label()
        Me.chkClearShift = New System.Windows.Forms.CheckBox()
        Me.txtOutShift = New System.Windows.Forms.TextBox()
        Me.txtINShift = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.fraFH = New System.Windows.Forms.GroupBox()
        Me.chkClear = New System.Windows.Forms.CheckBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtOUTTime = New System.Windows.Forms.TextBox()
        Me.txtINTime = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblEmpName = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.optOTType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.fraMain.SuspendLayout()
        Me.fraShift.SuspendLayout()
        Me.fraFH.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.optOTType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(212, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(63, 34)
        Me.CmdClose.TabIndex = 8
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'fraMain
        '
        Me.fraMain.BackColor = System.Drawing.SystemColors.Control
        Me.fraMain.Controls.Add(Me.fraShift)
        Me.fraMain.Controls.Add(Me.fraFH)
        Me.fraMain.Controls.Add(Me.lblDate)
        Me.fraMain.Controls.Add(Me.lblEmpName)
        Me.fraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMain.Location = New System.Drawing.Point(0, -4)
        Me.fraMain.Name = "fraMain"
        Me.fraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMain.Size = New System.Drawing.Size(291, 280)
        Me.fraMain.TabIndex = 9
        Me.fraMain.TabStop = False
        '
        'fraShift
        '
        Me.fraShift.BackColor = System.Drawing.SystemColors.Control
        Me.fraShift.Controls.Add(Me.chkRoundClock)
        Me.fraShift.Controls.Add(Me.cmdShiftChange)
        Me.fraShift.Controls.Add(Me.txtBreakTo)
        Me.fraShift.Controls.Add(Me.txtBreakFrom)
        Me.fraShift.Controls.Add(Me.Label4)
        Me.fraShift.Controls.Add(Me.Label7)
        Me.fraShift.Controls.Add(Me.cboShift)
        Me.fraShift.Controls.Add(Me.Shift)
        Me.fraShift.Controls.Add(Me.chkClearShift)
        Me.fraShift.Controls.Add(Me.txtOutShift)
        Me.fraShift.Controls.Add(Me.txtINShift)
        Me.fraShift.Controls.Add(Me.Label5)
        Me.fraShift.Controls.Add(Me.Label6)
        Me.fraShift.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraShift.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraShift.Location = New System.Drawing.Point(2, 50)
        Me.fraShift.Name = "fraShift"
        Me.fraShift.Padding = New System.Windows.Forms.Padding(0)
        Me.fraShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraShift.Size = New System.Drawing.Size(287, 132)
        Me.fraShift.TabIndex = 15
        Me.fraShift.TabStop = False
        Me.fraShift.Text = "Shift"
        '
        'chkRoundClock
        '
        Me.chkRoundClock.AutoSize = True
        Me.chkRoundClock.BackColor = System.Drawing.SystemColors.Control
        Me.chkRoundClock.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRoundClock.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRoundClock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRoundClock.Location = New System.Drawing.Point(193, 32)
        Me.chkRoundClock.Name = "chkRoundClock"
        Me.chkRoundClock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRoundClock.Size = New System.Drawing.Size(86, 18)
        Me.chkRoundClock.TabIndex = 40
        Me.chkRoundClock.Text = "Round Clock"
        Me.chkRoundClock.UseVisualStyleBackColor = False
        '
        'cmdShiftChange
        '
        Me.cmdShiftChange.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShiftChange.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShiftChange.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShiftChange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShiftChange.Location = New System.Drawing.Point(77, 100)
        Me.cmdShiftChange.Name = "cmdShiftChange"
        Me.cmdShiftChange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShiftChange.Size = New System.Drawing.Size(124, 25)
        Me.cmdShiftChange.TabIndex = 39
        Me.cmdShiftChange.Text = "&Shift Update"
        Me.cmdShiftChange.UseVisualStyleBackColor = False
        '
        'txtBreakTo
        '
        Me.txtBreakTo.AcceptsReturn = True
        Me.txtBreakTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBreakTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBreakTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBreakTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBreakTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBreakTo.Location = New System.Drawing.Point(232, 78)
        Me.txtBreakTo.MaxLength = 2
        Me.txtBreakTo.Name = "txtBreakTo"
        Me.txtBreakTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBreakTo.Size = New System.Drawing.Size(52, 20)
        Me.txtBreakTo.TabIndex = 36
        '
        'txtBreakFrom
        '
        Me.txtBreakFrom.AcceptsReturn = True
        Me.txtBreakFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtBreakFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBreakFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBreakFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBreakFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBreakFrom.Location = New System.Drawing.Point(78, 78)
        Me.txtBreakFrom.MaxLength = 2
        Me.txtBreakFrom.Name = "txtBreakFrom"
        Me.txtBreakFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBreakFrom.Size = New System.Drawing.Size(54, 20)
        Me.txtBreakFrom.TabIndex = 35
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(173, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(55, 14)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Break To :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(5, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(68, 14)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Break From :"
        '
        'cboShift
        '
        Me.cboShift.BackColor = System.Drawing.SystemColors.Window
        Me.cboShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShift.Location = New System.Drawing.Point(78, 13)
        Me.cboShift.Name = "cboShift"
        Me.cboShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShift.Size = New System.Drawing.Size(73, 22)
        Me.cboShift.TabIndex = 33
        '
        'Shift
        '
        Me.Shift.BackColor = System.Drawing.SystemColors.Control
        Me.Shift.Cursor = System.Windows.Forms.Cursors.Default
        Me.Shift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Shift.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Shift.Location = New System.Drawing.Point(36, 15)
        Me.Shift.Name = "Shift"
        Me.Shift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Shift.Size = New System.Drawing.Size(37, 11)
        Me.Shift.TabIndex = 34
        Me.Shift.Text = "Shift :"
        '
        'chkClearShift
        '
        Me.chkClearShift.AutoSize = True
        Me.chkClearShift.BackColor = System.Drawing.SystemColors.Control
        Me.chkClearShift.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkClearShift.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkClearShift.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkClearShift.Location = New System.Drawing.Point(193, 12)
        Me.chkClearShift.Name = "chkClearShift"
        Me.chkClearShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkClearShift.Size = New System.Drawing.Size(51, 18)
        Me.chkClearShift.TabIndex = 10
        Me.chkClearShift.Text = "Clear"
        Me.chkClearShift.UseVisualStyleBackColor = False
        Me.chkClearShift.Visible = False
        '
        'txtOutShift
        '
        Me.txtOutShift.AcceptsReturn = True
        Me.txtOutShift.BackColor = System.Drawing.SystemColors.Window
        Me.txtOutShift.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutShift.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOutShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOutShift.Location = New System.Drawing.Point(232, 54)
        Me.txtOutShift.MaxLength = 2
        Me.txtOutShift.Name = "txtOutShift"
        Me.txtOutShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOutShift.Size = New System.Drawing.Size(52, 20)
        Me.txtOutShift.TabIndex = 1
        '
        'txtINShift
        '
        Me.txtINShift.AcceptsReturn = True
        Me.txtINShift.BackColor = System.Drawing.SystemColors.Window
        Me.txtINShift.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtINShift.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtINShift.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtINShift.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtINShift.Location = New System.Drawing.Point(78, 54)
        Me.txtINShift.MaxLength = 2
        Me.txtINShift.Name = "txtINShift"
        Me.txtINShift.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtINShift.Size = New System.Drawing.Size(54, 20)
        Me.txtINShift.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(173, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(55, 14)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Out Time :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(27, 56)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(46, 14)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "In Time :"
        '
        'fraFH
        '
        Me.fraFH.BackColor = System.Drawing.SystemColors.Control
        Me.fraFH.Controls.Add(Me.chkClear)
        Me.fraFH.Controls.Add(Me.txtRemarks)
        Me.fraFH.Controls.Add(Me.Label3)
        Me.fraFH.Controls.Add(Me.txtOUTTime)
        Me.fraFH.Controls.Add(Me.txtINTime)
        Me.fraFH.Controls.Add(Me.Label2)
        Me.fraFH.Controls.Add(Me.Label1)
        Me.fraFH.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraFH.Location = New System.Drawing.Point(2, 184)
        Me.fraFH.Name = "fraFH"
        Me.fraFH.Padding = New System.Windows.Forms.Padding(0)
        Me.fraFH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraFH.Size = New System.Drawing.Size(287, 96)
        Me.fraFH.TabIndex = 12
        Me.fraFH.TabStop = False
        Me.fraFH.Text = "Time"
        '
        'chkClear
        '
        Me.chkClear.AutoSize = True
        Me.chkClear.BackColor = System.Drawing.SystemColors.Control
        Me.chkClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkClear.Location = New System.Drawing.Point(182, 15)
        Me.chkClear.Name = "chkClear"
        Me.chkClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkClear.Size = New System.Drawing.Size(51, 18)
        Me.chkClear.TabIndex = 10
        Me.chkClear.Text = "Clear"
        Me.chkClear.UseVisualStyleBackColor = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(67, 68)
        Me.txtRemarks.MaxLength = 2
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(211, 20)
        Me.txtRemarks.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 70)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(55, 14)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Remarks :"
        '
        'txtOUTTime
        '
        Me.txtOUTTime.AcceptsReturn = True
        Me.txtOUTTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtOUTTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOUTTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOUTTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOUTTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOUTTime.Location = New System.Drawing.Point(67, 41)
        Me.txtOUTTime.MaxLength = 2
        Me.txtOUTTime.Name = "txtOUTTime"
        Me.txtOUTTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOUTTime.Size = New System.Drawing.Size(52, 20)
        Me.txtOUTTime.TabIndex = 1
        '
        'txtINTime
        '
        Me.txtINTime.AcceptsReturn = True
        Me.txtINTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtINTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtINTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtINTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtINTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtINTime.Location = New System.Drawing.Point(67, 14)
        Me.txtINTime.MaxLength = 2
        Me.txtINTime.Name = "txtINTime"
        Me.txtINTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtINTime.Size = New System.Drawing.Size(54, 20)
        Me.txtINTime.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(55, 14)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Out Time :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(46, 14)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "In Time :"
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate.Location = New System.Drawing.Point(0, 34)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate.Size = New System.Drawing.Size(286, 16)
        Me.lblDate.TabIndex = 14
        Me.lblDate.Text = "Date"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblEmpName
        '
        Me.lblEmpName.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmpName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmpName.Location = New System.Drawing.Point(0, 14)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmpName.Size = New System.Drawing.Size(286, 38)
        Me.lblEmpName.TabIndex = 13
        Me.lblEmpName.Text = "Name"
        Me.lblEmpName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdOk)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.lblType)
        Me.FraMovement.Controls.Add(Me.lblCode)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 272)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(291, 47)
        Me.FraMovement.TabIndex = 11
        Me.FraMovement.TabStop = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Image = CType(resources.GetObject("cmdOk.Image"), System.Drawing.Image)
        Me.cmdOk.Location = New System.Drawing.Point(4, 10)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(63, 34)
        Me.cmdOk.TabIndex = 7
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Location = New System.Drawing.Point(90, 32)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.Size = New System.Drawing.Size(27, 13)
        Me.lblType.TabIndex = 16
        Me.lblType.Text = "Type"
        Me.lblType.Visible = False
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCode.Location = New System.Drawing.Point(88, 16)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCode.Size = New System.Drawing.Size(32, 14)
        Me.lblCode.TabIndex = 15
        Me.lblCode.Text = "Code"
        Me.lblCode.Visible = False
        '
        'frmAttnInOutMark
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(291, 321)
        Me.Controls.Add(Me.fraMain)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(21, 91)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAttnInOutMark"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "In / Out Attendance"
        Me.fraMain.ResumeLayout(False)
        Me.fraShift.ResumeLayout(False)
        Me.fraShift.PerformLayout()
        Me.fraFH.ResumeLayout(False)
        Me.fraFH.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        CType(Me.optOTType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents txtRemarks As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents fraShift As GroupBox
    Public WithEvents chkClearShift As CheckBox
    Public WithEvents txtOutShift As TextBox
    Public WithEvents txtINShift As TextBox
    Public WithEvents Label5 As Label
    Public WithEvents Label6 As Label
    Public WithEvents cboShift As ComboBox
    Public WithEvents Shift As Label
    Public WithEvents txtBreakTo As TextBox
    Public WithEvents txtBreakFrom As TextBox
    Public WithEvents Label4 As Label
    Public WithEvents Label7 As Label
    Public WithEvents cmdShiftChange As Button
    Public WithEvents chkRoundClock As CheckBox
#End Region
End Class