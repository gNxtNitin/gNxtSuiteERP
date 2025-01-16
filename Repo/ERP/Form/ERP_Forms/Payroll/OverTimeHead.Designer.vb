Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOverTimeHead
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
    Public WithEvents txtPrevOTHour As System.Windows.Forms.TextBox
    Public WithEvents txtPrevOTMin As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents chkClear As System.Windows.Forms.CheckBox
    Public WithEvents txtOTMin As System.Windows.Forms.TextBox
    Public WithEvents txtOTHour As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents fraFH As System.Windows.Forms.GroupBox
    Public WithEvents lblDate As System.Windows.Forms.Label
    Public WithEvents lblEmpName As System.Windows.Forms.Label
    Public WithEvents fraMain As System.Windows.Forms.GroupBox
    Public WithEvents _optOTType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optOTType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optOTType_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOverTimeHead))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.fraMain = New System.Windows.Forms.GroupBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtPrevOTHour = New System.Windows.Forms.TextBox()
        Me.txtPrevOTMin = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkClear = New System.Windows.Forms.CheckBox()
        Me.fraFH = New System.Windows.Forms.GroupBox()
        Me.txtOTMin = New System.Windows.Forms.TextBox()
        Me.txtOTHour = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblEmpName = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optOTType_2 = New System.Windows.Forms.RadioButton()
        Me._optOTType_1 = New System.Windows.Forms.RadioButton()
        Me._optOTType_0 = New System.Windows.Forms.RadioButton()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.optOTType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.fraMain.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.fraFH.SuspendLayout()
        Me.Frame1.SuspendLayout()
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
        Me.fraMain.Controls.Add(Me.Frame2)
        Me.fraMain.Controls.Add(Me.chkClear)
        Me.fraMain.Controls.Add(Me.fraFH)
        Me.fraMain.Controls.Add(Me.lblDate)
        Me.fraMain.Controls.Add(Me.lblEmpName)
        Me.fraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMain.Location = New System.Drawing.Point(0, -4)
        Me.fraMain.Name = "fraMain"
        Me.fraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMain.Size = New System.Drawing.Size(291, 152)
        Me.fraMain.TabIndex = 9
        Me.fraMain.TabStop = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtPrevOTHour)
        Me.Frame2.Controls.Add(Me.txtPrevOTMin)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(2, 117)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(287, 37)
        Me.Frame2.TabIndex = 20
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Previous Month"
        '
        'txtPrevOTHour
        '
        Me.txtPrevOTHour.AcceptsReturn = True
        Me.txtPrevOTHour.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrevOTHour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrevOTHour.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrevOTHour.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrevOTHour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrevOTHour.Location = New System.Drawing.Point(54, 14)
        Me.txtPrevOTHour.MaxLength = 2
        Me.txtPrevOTHour.Name = "txtPrevOTHour"
        Me.txtPrevOTHour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrevOTHour.Size = New System.Drawing.Size(33, 20)
        Me.txtPrevOTHour.TabIndex = 2
        '
        'txtPrevOTMin
        '
        Me.txtPrevOTMin.AcceptsReturn = True
        Me.txtPrevOTMin.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrevOTMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrevOTMin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrevOTMin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrevOTMin.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrevOTMin.Location = New System.Drawing.Point(224, 14)
        Me.txtPrevOTMin.MaxLength = 2
        Me.txtPrevOTMin.Name = "txtPrevOTMin"
        Me.txtPrevOTMin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrevOTMin.Size = New System.Drawing.Size(31, 20)
        Me.txtPrevOTMin.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(36, 14)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Hour :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(162, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(44, 14)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Minute :"
        '
        'chkClear
        '
        Me.chkClear.AutoSize = True
        Me.chkClear.BackColor = System.Drawing.SystemColors.Control
        Me.chkClear.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkClear.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkClear.Location = New System.Drawing.Point(232, 62)
        Me.chkClear.Name = "chkClear"
        Me.chkClear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkClear.Size = New System.Drawing.Size(51, 18)
        Me.chkClear.TabIndex = 10
        Me.chkClear.Text = "Clear"
        Me.chkClear.UseVisualStyleBackColor = False
        '
        'fraFH
        '
        Me.fraFH.BackColor = System.Drawing.SystemColors.Control
        Me.fraFH.Controls.Add(Me.txtOTMin)
        Me.fraFH.Controls.Add(Me.txtOTHour)
        Me.fraFH.Controls.Add(Me.Label2)
        Me.fraFH.Controls.Add(Me.Label1)
        Me.fraFH.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraFH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraFH.Location = New System.Drawing.Point(2, 77)
        Me.fraFH.Name = "fraFH"
        Me.fraFH.Padding = New System.Windows.Forms.Padding(0)
        Me.fraFH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraFH.Size = New System.Drawing.Size(287, 39)
        Me.fraFH.TabIndex = 12
        Me.fraFH.TabStop = False
        '
        'txtOTMin
        '
        Me.txtOTMin.AcceptsReturn = True
        Me.txtOTMin.BackColor = System.Drawing.SystemColors.Window
        Me.txtOTMin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOTMin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOTMin.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOTMin.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOTMin.Location = New System.Drawing.Point(224, 14)
        Me.txtOTMin.MaxLength = 2
        Me.txtOTMin.Name = "txtOTMin"
        Me.txtOTMin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOTMin.Size = New System.Drawing.Size(31, 20)
        Me.txtOTMin.TabIndex = 1
        '
        'txtOTHour
        '
        Me.txtOTHour.AcceptsReturn = True
        Me.txtOTHour.BackColor = System.Drawing.SystemColors.Window
        Me.txtOTHour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOTHour.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOTHour.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOTHour.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOTHour.Location = New System.Drawing.Point(54, 14)
        Me.txtOTHour.MaxLength = 2
        Me.txtOTHour.Name = "txtOTHour"
        Me.txtOTHour.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOTHour.Size = New System.Drawing.Size(33, 20)
        Me.txtOTHour.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(162, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(44, 14)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Minute :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(36, 14)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Hour :"
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate.Location = New System.Drawing.Point(3, 34)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate.Size = New System.Drawing.Size(285, 16)
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
        Me.lblEmpName.Location = New System.Drawing.Point(3, 14)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmpName.Size = New System.Drawing.Size(285, 16)
        Me.lblEmpName.TabIndex = 13
        Me.lblEmpName.Text = "Name"
        Me.lblEmpName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optOTType_2)
        Me.Frame1.Controls.Add(Me._optOTType_1)
        Me.Frame1.Controls.Add(Me._optOTType_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 144)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(291, 38)
        Me.Frame1.TabIndex = 19
        Me.Frame1.TabStop = False
        '
        '_optOTType_2
        '
        Me._optOTType_2.BackColor = System.Drawing.SystemColors.Control
        Me._optOTType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOTType_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOTType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOTType.SetIndex(Me._optOTType_2, CType(2, Short))
        Me._optOTType_2.Location = New System.Drawing.Point(192, 14)
        Me._optOTType_2.Name = "_optOTType_2"
        Me._optOTType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOTType_2.Size = New System.Drawing.Size(91, 20)
        Me._optOTType_2.TabIndex = 6
        Me._optOTType_2.TabStop = True
        Me._optOTType_2.Text = "Complulsory Duty"
        Me._optOTType_2.UseVisualStyleBackColor = False
        '
        '_optOTType_1
        '
        Me._optOTType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOTType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOTType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOTType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOTType.SetIndex(Me._optOTType_1, CType(1, Short))
        Me._optOTType_1.Location = New System.Drawing.Point(74, 14)
        Me._optOTType_1.Name = "_optOTType_1"
        Me._optOTType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOTType_1.Size = New System.Drawing.Size(103, 20)
        Me._optOTType_1.TabIndex = 5
        Me._optOTType_1.TabStop = True
        Me._optOTType_1.Text = "Absentiesm"
        Me._optOTType_1.UseVisualStyleBackColor = False
        '
        '_optOTType_0
        '
        Me._optOTType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOTType_0.Checked = True
        Me._optOTType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOTType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOTType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOTType.SetIndex(Me._optOTType_0, CType(0, Short))
        Me._optOTType_0.Location = New System.Drawing.Point(4, 14)
        Me._optOTType_0.Name = "_optOTType_0"
        Me._optOTType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOTType_0.Size = New System.Drawing.Size(61, 20)
        Me._optOTType_0.TabIndex = 4
        Me._optOTType_0.TabStop = True
        Me._optOTType_0.Text = "Over Time"
        Me._optOTType_0.UseVisualStyleBackColor = False
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 176)
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
        'frmOverTimeHead
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(291, 223)
        Me.Controls.Add(Me.fraMain)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(21, 91)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOverTimeHead"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Over Time Heads"
        Me.fraMain.ResumeLayout(False)
        Me.fraMain.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.fraFH.ResumeLayout(False)
        Me.fraFH.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
        CType(Me.optOTType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class