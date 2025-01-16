Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintInvCopy
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
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _chkPrintOption_5 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrintOption_4 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrintOption_3 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrintOption_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrintOption_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkPrintOption_0 As System.Windows.Forms.CheckBox
    Public WithEvents fraPrintOption As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public WithEvents chkPrintOption As VB6.CheckBoxArray
    Public WithEvents optShow As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintInvCopy))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optShow_4 = New System.Windows.Forms.RadioButton()
        Me._optShow_3 = New System.Windows.Forms.RadioButton()
        Me._optShow_2 = New System.Windows.Forms.RadioButton()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me.fraPrintOption = New System.Windows.Forms.GroupBox()
        Me._chkPrintOption_5 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_4 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_3 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_2 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_1 = New System.Windows.Forms.CheckBox()
        Me._chkPrintOption_0 = New System.Windows.Forms.CheckBox()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.chkPrintOption = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.chkPrePrint = New System.Windows.Forms.CheckBox()
        Me.optA4 = New System.Windows.Forms.RadioButton()
        Me.optA3 = New System.Windows.Forms.RadioButton()
        Me.optPrintLandScape = New System.Windows.Forms.RadioButton()
        Me.optPrintPortrait = New System.Windows.Forms.RadioButton()
        Me._optShow_5 = New System.Windows.Forms.RadioButton()
        Me._optShow_6 = New System.Windows.Forms.RadioButton()
        Me.Frame1.SuspendLayout()
        Me.fraPrintOption.SuspendLayout()
        Me.FraOk.SuspendLayout()
        CType(Me.chkPrintOption, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optShow_6)
        Me.Frame1.Controls.Add(Me._optShow_5)
        Me.Frame1.Controls.Add(Me._optShow_4)
        Me.Frame1.Controls.Add(Me._optShow_3)
        Me.Frame1.Controls.Add(Me._optShow_2)
        Me.Frame1.Controls.Add(Me._optShow_1)
        Me.Frame1.Controls.Add(Me._optShow_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(360, 113)
        Me.Frame1.TabIndex = 10
        Me.Frame1.TabStop = False
        '
        '_optShow_4
        '
        Me._optShow_4.AutoSize = True
        Me._optShow_4.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_4, CType(4, Short))
        Me._optShow_4.Location = New System.Drawing.Point(203, 58)
        Me._optShow_4.Name = "_optShow_4"
        Me._optShow_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_4.Size = New System.Drawing.Size(115, 17)
        Me._optShow_4.TabIndex = 15
        Me._optShow_4.TabStop = True
        Me._optShow_4.Text = "Packing List Outer"
        Me._optShow_4.UseVisualStyleBackColor = False
        '
        '_optShow_3
        '
        Me._optShow_3.AutoSize = True
        Me._optShow_3.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_3, CType(3, Short))
        Me._optShow_3.Location = New System.Drawing.Point(203, 35)
        Me._optShow_3.Name = "_optShow_3"
        Me._optShow_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_3.Size = New System.Drawing.Size(112, 17)
        Me._optShow_3.TabIndex = 14
        Me._optShow_3.TabStop = True
        Me._optShow_3.Text = "Packing List Inner"
        Me._optShow_3.UseVisualStyleBackColor = False
        '
        '_optShow_2
        '
        Me._optShow_2.AutoSize = True
        Me._optShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_2, CType(2, Short))
        Me._optShow_2.Location = New System.Drawing.Point(16, 35)
        Me._optShow_2.Name = "_optShow_2"
        Me._optShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_2.Size = New System.Drawing.Size(170, 17)
        Me._optShow_2.TabIndex = 13
        Me._optShow_2.TabStop = True
        Me._optShow_2.Text = "Tax Invoice With Digital Sign"
        Me._optShow_2.UseVisualStyleBackColor = False
        '
        '_optShow_1
        '
        Me._optShow_1.AutoSize = True
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(203, 12)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(112, 17)
        Me._optShow_1.TabIndex = 12
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "Tax Invoice (PDF)"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        '_optShow_0
        '
        Me._optShow_0.AutoSize = True
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Checked = True
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(16, 12)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(142, 17)
        Me._optShow_0.TabIndex = 11
        Me._optShow_0.TabStop = True
        Me._optShow_0.Text = "Tax Invoice (On Screen)"
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        'fraPrintOption
        '
        Me.fraPrintOption.BackColor = System.Drawing.SystemColors.Control
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_5)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_4)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_3)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_2)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_1)
        Me.fraPrintOption.Controls.Add(Me._chkPrintOption_0)
        Me.fraPrintOption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPrintOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPrintOption.Location = New System.Drawing.Point(0, 114)
        Me.fraPrintOption.Name = "fraPrintOption"
        Me.fraPrintOption.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPrintOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPrintOption.Size = New System.Drawing.Size(358, 68)
        Me.fraPrintOption.TabIndex = 3
        Me.fraPrintOption.TabStop = False
        '
        '_chkPrintOption_5
        '
        Me._chkPrintOption_5.AutoSize = True
        Me._chkPrintOption_5.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_5.Checked = True
        Me._chkPrintOption_5.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_5, CType(5, Short))
        Me._chkPrintOption_5.Location = New System.Drawing.Point(10, 47)
        Me._chkPrintOption_5.Name = "_chkPrintOption_5"
        Me._chkPrintOption_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_5.Size = New System.Drawing.Size(135, 17)
        Me._chkPrintOption_5.TabIndex = 9
        Me._chkPrintOption_5.Text = "Triplicate for Supplier"
        Me._chkPrintOption_5.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_4
        '
        Me._chkPrintOption_4.AutoSize = True
        Me._chkPrintOption_4.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_4.Checked = True
        Me._chkPrintOption_4.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_4, CType(4, Short))
        Me._chkPrintOption_4.Location = New System.Drawing.Point(228, 47)
        Me._chkPrintOption_4.Name = "_chkPrintOption_4"
        Me._chkPrintOption_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_4.Size = New System.Drawing.Size(101, 17)
        Me._chkPrintOption_4.TabIndex = 8
        Me._chkPrintOption_4.Text = "Accounts Copy"
        Me._chkPrintOption_4.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_3
        '
        Me._chkPrintOption_3.AutoSize = True
        Me._chkPrintOption_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_3.Checked = True
        Me._chkPrintOption_3.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_3, CType(3, Short))
        Me._chkPrintOption_3.Location = New System.Drawing.Point(228, 31)
        Me._chkPrintOption_3.Name = "_chkPrintOption_3"
        Me._chkPrintOption_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_3.Size = New System.Drawing.Size(82, 17)
        Me._chkPrintOption_3.TabIndex = 7
        Me._chkPrintOption_3.Text = "Extra Copy"
        Me._chkPrintOption_3.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_2
        '
        Me._chkPrintOption_2.AutoSize = True
        Me._chkPrintOption_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_2.Checked = True
        Me._chkPrintOption_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_2, CType(2, Short))
        Me._chkPrintOption_2.Location = New System.Drawing.Point(228, 15)
        Me._chkPrintOption_2.Name = "_chkPrintOption_2"
        Me._chkPrintOption_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_2.Size = New System.Drawing.Size(123, 17)
        Me._chkPrintOption_2.TabIndex = 6
        Me._chkPrintOption_2.Text = "Center Excise Copy"
        Me._chkPrintOption_2.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_1
        '
        Me._chkPrintOption_1.AutoSize = True
        Me._chkPrintOption_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_1.Checked = True
        Me._chkPrintOption_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_1, CType(1, Short))
        Me._chkPrintOption_1.Location = New System.Drawing.Point(10, 31)
        Me._chkPrintOption_1.Name = "_chkPrintOption_1"
        Me._chkPrintOption_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_1.Size = New System.Drawing.Size(153, 17)
        Me._chkPrintOption_1.TabIndex = 5
        Me._chkPrintOption_1.Text = "Duplicate for Transporter"
        Me._chkPrintOption_1.UseVisualStyleBackColor = False
        '
        '_chkPrintOption_0
        '
        Me._chkPrintOption_0.AutoSize = True
        Me._chkPrintOption_0.BackColor = System.Drawing.SystemColors.Control
        Me._chkPrintOption_0.Checked = True
        Me._chkPrintOption_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkPrintOption_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkPrintOption_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkPrintOption_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintOption.SetIndex(Me._chkPrintOption_0, CType(0, Short))
        Me._chkPrintOption_0.Location = New System.Drawing.Point(10, 15)
        Me._chkPrintOption_0.Name = "_chkPrintOption_0"
        Me._chkPrintOption_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkPrintOption_0.Size = New System.Drawing.Size(134, 17)
        Me._chkPrintOption_0.TabIndex = 4
        Me._chkPrintOption_0.Text = "Original for Recipient"
        Me._chkPrintOption_0.UseVisualStyleBackColor = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 232)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(361, 47)
        Me.FraOk.TabIndex = 0
        Me.FraOk.TabStop = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(8, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(73, 27)
        Me.cmdOk.TabIndex = 2
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(279, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 27)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.GroupBox4)
        Me.GroupBox3.Controls.Add(Me.optPrintLandScape)
        Me.GroupBox3.Controls.Add(Me.optPrintPortrait)
        Me.GroupBox3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(3, 183)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(355, 48)
        Me.GroupBox3.TabIndex = 271
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Invoice Print"
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox4.Controls.Add(Me.chkPrePrint)
        Me.GroupBox4.Controls.Add(Me.optA4)
        Me.GroupBox4.Controls.Add(Me.optA3)
        Me.GroupBox4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox4.Location = New System.Drawing.Point(170, 11)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox4.Size = New System.Drawing.Size(183, 36)
        Me.GroupBox4.TabIndex = 84
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Paper"
        '
        'chkPrePrint
        '
        Me.chkPrePrint.AutoSize = True
        Me.chkPrePrint.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrePrint.Checked = True
        Me.chkPrePrint.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrePrint.Location = New System.Drawing.Point(113, 16)
        Me.chkPrePrint.Name = "chkPrePrint"
        Me.chkPrePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrePrint.Size = New System.Drawing.Size(65, 17)
        Me.chkPrePrint.TabIndex = 81
        Me.chkPrePrint.Text = "PrePrint"
        Me.chkPrePrint.UseVisualStyleBackColor = False
        Me.chkPrePrint.Visible = False
        '
        'optA4
        '
        Me.optA4.AutoSize = True
        Me.optA4.BackColor = System.Drawing.SystemColors.Control
        Me.optA4.Checked = True
        Me.optA4.Cursor = System.Windows.Forms.Cursors.Default
        Me.optA4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optA4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optA4.Location = New System.Drawing.Point(20, 16)
        Me.optA4.Name = "optA4"
        Me.optA4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optA4.Size = New System.Drawing.Size(38, 17)
        Me.optA4.TabIndex = 80
        Me.optA4.TabStop = True
        Me.optA4.Text = "A4"
        Me.optA4.UseVisualStyleBackColor = False
        '
        'optA3
        '
        Me.optA3.AutoSize = True
        Me.optA3.BackColor = System.Drawing.SystemColors.Control
        Me.optA3.Cursor = System.Windows.Forms.Cursors.Default
        Me.optA3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optA3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optA3.Location = New System.Drawing.Point(67, 16)
        Me.optA3.Name = "optA3"
        Me.optA3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optA3.Size = New System.Drawing.Size(38, 17)
        Me.optA3.TabIndex = 79
        Me.optA3.TabStop = True
        Me.optA3.Text = "A3"
        Me.optA3.UseVisualStyleBackColor = False
        '
        'optPrintLandScape
        '
        Me.optPrintLandScape.AutoSize = True
        Me.optPrintLandScape.BackColor = System.Drawing.SystemColors.Control
        Me.optPrintLandScape.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPrintLandScape.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPrintLandScape.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintLandScape.Location = New System.Drawing.Point(83, 25)
        Me.optPrintLandScape.Name = "optPrintLandScape"
        Me.optPrintLandScape.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPrintLandScape.Size = New System.Drawing.Size(79, 17)
        Me.optPrintLandScape.TabIndex = 83
        Me.optPrintLandScape.Text = "LandScape"
        Me.optPrintLandScape.UseVisualStyleBackColor = False
        '
        'optPrintPortrait
        '
        Me.optPrintPortrait.AutoSize = True
        Me.optPrintPortrait.BackColor = System.Drawing.SystemColors.Control
        Me.optPrintPortrait.Checked = True
        Me.optPrintPortrait.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPrintPortrait.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPrintPortrait.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintPortrait.Location = New System.Drawing.Point(12, 25)
        Me.optPrintPortrait.Name = "optPrintPortrait"
        Me.optPrintPortrait.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPrintPortrait.Size = New System.Drawing.Size(63, 17)
        Me.optPrintPortrait.TabIndex = 82
        Me.optPrintPortrait.TabStop = True
        Me.optPrintPortrait.Text = "Portrait"
        Me.optPrintPortrait.UseVisualStyleBackColor = False
        '
        '_optShow_5
        '
        Me._optShow_5.AutoSize = True
        Me._optShow_5.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optShow_5.Location = New System.Drawing.Point(16, 58)
        Me._optShow_5.Name = "_optShow_5"
        Me._optShow_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_5.Size = New System.Drawing.Size(107, 17)
        Me._optShow_5.TabIndex = 16
        Me._optShow_5.TabStop = True
        Me._optShow_5.Text = "Delivery Challan"
        Me._optShow_5.UseVisualStyleBackColor = False
        '
        '_optShow_6
        '
        Me._optShow_6.AutoSize = True
        Me._optShow_6.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optShow_6.Location = New System.Drawing.Point(16, 81)
        Me._optShow_6.Name = "_optShow_6"
        Me._optShow_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_6.Size = New System.Drawing.Size(125, 17)
        Me._optShow_6.TabIndex = 17
        Me._optShow_6.TabStop = True
        Me._optShow_6.Text = "Commercial Invoice"
        Me._optShow_6.UseVisualStyleBackColor = False
        '
        'frmPrintInvCopy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(363, 280)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.fraPrintOption)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintInvCopy"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.fraPrintOption.ResumeLayout(False)
        Me.fraPrintOption.PerformLayout()
        Me.FraOk.ResumeLayout(False)
        CType(Me.chkPrintOption, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents _optShow_2 As RadioButton
    Public WithEvents _optShow_3 As RadioButton
    Public WithEvents _optShow_4 As RadioButton
    Public WithEvents GroupBox3 As GroupBox
    Public WithEvents GroupBox4 As GroupBox
    Public WithEvents optA4 As RadioButton
    Public WithEvents optA3 As RadioButton
    Public WithEvents optPrintLandScape As RadioButton
    Public WithEvents optPrintPortrait As RadioButton
    Public WithEvents chkPrePrint As CheckBox
    Public WithEvents _optShow_6 As RadioButton
    Public WithEvents _optShow_5 As RadioButton
#End Region
End Class