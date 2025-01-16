Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmMarkAbsentFromMc
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
    Public WithEvents _optMark_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optMark_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents PBar As System.Windows.Forms.ProgressBar
    Public WithEvents FraMark As System.Windows.Forms.GroupBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents OptParti As System.Windows.Forms.RadioButton
    Public WithEvents OptAll As System.Windows.Forms.RadioButton
    Public WithEvents TxtCardNo As System.Windows.Forms.TextBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents FraEmp As System.Windows.Forms.GroupBox
    Public WithEvents UpDMonth As System.Windows.Forms.NumericUpDown     ''AxComCtl2.AxUpDown
    Public WithEvents txtMonth As System.Windows.Forms.TextBox
    Public WithEvents lblNewDate As System.Windows.Forms.Label
    Public WithEvents lblMonth As System.Windows.Forms.Label
    Public WithEvents FraPeriod As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdOK As System.Windows.Forms.Button
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents optMark As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMarkAbsentFromMc))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optMark_0 = New System.Windows.Forms.RadioButton()
        Me._optMark_1 = New System.Windows.Forms.RadioButton()
        Me.FraMark = New System.Windows.Forms.GroupBox()
        Me.PBar = New System.Windows.Forms.ProgressBar()
        Me.FraEmp = New System.Windows.Forms.GroupBox()
        Me.OptParti = New System.Windows.Forms.RadioButton()
        Me.OptAll = New System.Windows.Forms.RadioButton()
        Me.TxtCardNo = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.FraPeriod = New System.Windows.Forms.GroupBox()
        Me.UpDMonth = New System.Windows.Forms.NumericUpDown()
        Me.txtMonth = New System.Windows.Forms.TextBox()
        Me.lblNewDate = New System.Windows.Forms.Label()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.CmdOK = New System.Windows.Forms.Button()
        Me.optMark = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.FraMark.SuspendLayout()
        Me.FraEmp.SuspendLayout()
        Me.FraPeriod.SuspendLayout()
        CType(Me.UpDMonth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        CType(Me.optMark, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(212, 12)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearch.TabIndex = 4
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(294, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 9
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optMark_0)
        Me.Frame1.Controls.Add(Me._optMark_1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 38)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(357, 33)
        Me.Frame1.TabIndex = 16
        Me.Frame1.TabStop = False
        '
        '_optMark_0
        '
        Me._optMark_0.AutoSize = True
        Me._optMark_0.BackColor = System.Drawing.SystemColors.Control
        Me._optMark_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMark_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMark_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMark.SetIndex(Me._optMark_0, CType(0, Short))
        Me._optMark_0.Location = New System.Drawing.Point(54, 14)
        Me._optMark_0.Name = "_optMark_0"
        Me._optMark_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMark_0.Size = New System.Drawing.Size(60, 18)
        Me._optMark_0.TabIndex = 18
        Me._optMark_0.TabStop = True
        Me._optMark_0.Text = "Absent"
        Me._optMark_0.UseVisualStyleBackColor = False
        '
        '_optMark_1
        '
        Me._optMark_1.AutoSize = True
        Me._optMark_1.BackColor = System.Drawing.SystemColors.Control
        Me._optMark_1.Checked = True
        Me._optMark_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optMark_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optMark_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMark.SetIndex(Me._optMark_1, CType(1, Short))
        Me._optMark_1.Location = New System.Drawing.Point(158, 14)
        Me._optMark_1.Name = "_optMark_1"
        Me._optMark_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optMark_1.Size = New System.Drawing.Size(82, 18)
        Me._optMark_1.TabIndex = 17
        Me._optMark_1.TabStop = True
        Me._optMark_1.Text = "Without Pay"
        Me._optMark_1.UseVisualStyleBackColor = False
        '
        'FraMark
        '
        Me.FraMark.BackColor = System.Drawing.SystemColors.Control
        Me.FraMark.Controls.Add(Me.PBar)
        Me.FraMark.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMark.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMark.Location = New System.Drawing.Point(0, 146)
        Me.FraMark.Name = "FraMark"
        Me.FraMark.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMark.Size = New System.Drawing.Size(357, 41)
        Me.FraMark.TabIndex = 14
        Me.FraMark.TabStop = False
        '
        'PBar
        '
        Me.PBar.Location = New System.Drawing.Point(4, 18)
        Me.PBar.Name = "PBar"
        Me.PBar.Size = New System.Drawing.Size(347, 13)
        Me.PBar.TabIndex = 15
        Me.PBar.Visible = False
        '
        'FraEmp
        '
        Me.FraEmp.BackColor = System.Drawing.SystemColors.Control
        Me.FraEmp.Controls.Add(Me.cmdSearch)
        Me.FraEmp.Controls.Add(Me.OptParti)
        Me.FraEmp.Controls.Add(Me.OptAll)
        Me.FraEmp.Controls.Add(Me.TxtCardNo)
        Me.FraEmp.Controls.Add(Me.TxtName)
        Me.FraEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraEmp.Location = New System.Drawing.Point(0, 72)
        Me.FraEmp.Name = "FraEmp"
        Me.FraEmp.Padding = New System.Windows.Forms.Padding(0)
        Me.FraEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraEmp.Size = New System.Drawing.Size(357, 73)
        Me.FraEmp.TabIndex = 8
        Me.FraEmp.TabStop = False
        Me.FraEmp.Text = "Employee"
        '
        'OptParti
        '
        Me.OptParti.AutoSize = True
        Me.OptParti.BackColor = System.Drawing.SystemColors.Control
        Me.OptParti.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptParti.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptParti.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptParti.Location = New System.Drawing.Point(10, 42)
        Me.OptParti.Name = "OptParti"
        Me.OptParti.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptParti.Size = New System.Drawing.Size(73, 18)
        Me.OptParti.TabIndex = 1
        Me.OptParti.TabStop = True
        Me.OptParti.Text = "Particular "
        Me.OptParti.UseVisualStyleBackColor = False
        '
        'OptAll
        '
        Me.OptAll.AutoSize = True
        Me.OptAll.BackColor = System.Drawing.SystemColors.Control
        Me.OptAll.Checked = True
        Me.OptAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAll.Location = New System.Drawing.Point(10, 16)
        Me.OptAll.Name = "OptAll"
        Me.OptAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptAll.Size = New System.Drawing.Size(40, 18)
        Me.OptAll.TabIndex = 0
        Me.OptAll.TabStop = True
        Me.OptAll.Text = "All "
        Me.OptAll.UseVisualStyleBackColor = False
        '
        'TxtCardNo
        '
        Me.TxtCardNo.AcceptsReturn = True
        Me.TxtCardNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCardNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCardNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCardNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCardNo.Location = New System.Drawing.Point(104, 12)
        Me.TxtCardNo.MaxLength = 0
        Me.TxtCardNo.Name = "TxtCardNo"
        Me.TxtCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCardNo.Size = New System.Drawing.Size(107, 21)
        Me.TxtCardNo.TabIndex = 3
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
        Me.TxtName.Location = New System.Drawing.Point(104, 40)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(249, 21)
        Me.TxtName.TabIndex = 5
        '
        'FraPeriod
        '
        Me.FraPeriod.BackColor = System.Drawing.SystemColors.Control
        Me.FraPeriod.Controls.Add(Me.UpDMonth)
        Me.FraPeriod.Controls.Add(Me.txtMonth)
        Me.FraPeriod.Controls.Add(Me.lblNewDate)
        Me.FraPeriod.Controls.Add(Me.lblMonth)
        Me.FraPeriod.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPeriod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPeriod.Location = New System.Drawing.Point(0, -2)
        Me.FraPeriod.Name = "FraPeriod"
        Me.FraPeriod.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPeriod.Size = New System.Drawing.Size(356, 39)
        Me.FraPeriod.TabIndex = 7
        Me.FraPeriod.TabStop = False
        Me.FraPeriod.Text = "Period"
        '
        'UpDMonth
        '
        Me.UpDMonth.Location = New System.Drawing.Point(218, 12)
        Me.UpDMonth.Name = "UpDMonth"
        Me.UpDMonth.Size = New System.Drawing.Size(16, 20)
        Me.UpDMonth.TabIndex = 12
        '
        'txtMonth
        '
        Me.txtMonth.AcceptsReturn = True
        Me.txtMonth.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMonth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonth.Location = New System.Drawing.Point(112, 12)
        Me.txtMonth.MaxLength = 0
        Me.txtMonth.Name = "txtMonth"
        Me.txtMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonth.Size = New System.Drawing.Size(123, 21)
        Me.txtMonth.TabIndex = 11
        '
        'lblNewDate
        '
        Me.lblNewDate.AutoSize = True
        Me.lblNewDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblNewDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNewDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNewDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNewDate.Location = New System.Drawing.Point(264, 14)
        Me.lblNewDate.Name = "lblNewDate"
        Me.lblNewDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNewDate.Size = New System.Drawing.Size(52, 14)
        Me.lblNewDate.TabIndex = 13
        Me.lblNewDate.Text = "NewDate"
        Me.lblNewDate.Visible = False
        '
        'lblMonth
        '
        Me.lblMonth.AutoSize = True
        Me.lblMonth.BackColor = System.Drawing.SystemColors.Control
        Me.lblMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMonth.Location = New System.Drawing.Point(54, 16)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMonth.Size = New System.Drawing.Size(42, 14)
        Me.lblMonth.TabIndex = 10
        Me.lblMonth.Text = "Month :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.CmdClose)
        Me.Frame2.Controls.Add(Me.CmdOK)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 182)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(357, 47)
        Me.Frame2.TabIndex = 2
        Me.Frame2.TabStop = False
        '
        'CmdOK
        '
        Me.CmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.CmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdOK.Location = New System.Drawing.Point(4, 10)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdOK.Size = New System.Drawing.Size(60, 33)
        Me.CmdOK.TabIndex = 6
        Me.CmdOK.Text = "Ok"
        Me.CmdOK.UseVisualStyleBackColor = False
        '
        'FrmMarkAbsentFromMc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(357, 230)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMark)
        Me.Controls.Add(Me.FraEmp)
        Me.Controls.Add(Me.FraPeriod)
        Me.Controls.Add(Me.Frame2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "FrmMarkAbsentFromMc"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Mark Absent/Without Pay From M/c Data"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraMark.ResumeLayout(False)
        Me.FraEmp.ResumeLayout(False)
        Me.FraEmp.PerformLayout()
        Me.FraPeriod.ResumeLayout(False)
        Me.FraPeriod.PerformLayout()
        CType(Me.UpDMonth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        CType(Me.optMark, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class