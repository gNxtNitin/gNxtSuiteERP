Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTrfLeaveBalance
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
    Public WithEvents _TxtDisplayTransfer_1 As System.Windows.Forms.TextBox
    Public WithEvents _TxtDisplayTransfer_0 As System.Windows.Forms.TextBox
    Public WithEvents OptParticularAccount As System.Windows.Forms.RadioButton
    Public WithEvents OptAllAccount As System.Windows.Forms.RadioButton
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents lblName As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents CboFYearTo As System.Windows.Forms.ComboBox
    Public WithEvents CboFYearFrom As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraFYear As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdStart As System.Windows.Forms.Button
    Public WithEvents FraButton As System.Windows.Forms.GroupBox
    Public WithEvents TxtDisplayTransfer As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTrfLeaveBalance))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me._TxtDisplayTransfer_1 = New System.Windows.Forms.TextBox()
        Me._TxtDisplayTransfer_0 = New System.Windows.Forms.TextBox()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.OptParticularAccount = New System.Windows.Forms.RadioButton()
        Me.OptAllAccount = New System.Windows.Forms.RadioButton()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.lblName = New System.Windows.Forms.Label()
        Me.FraFYear = New System.Windows.Forms.GroupBox()
        Me.CboFYearTo = New System.Windows.Forms.ComboBox()
        Me.CboFYearFrom = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FraButton = New System.Windows.Forms.GroupBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.TxtDisplayTransfer = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.FraAccount.SuspendLayout()
        Me.FraFYear.SuspendLayout()
        Me.FraButton.SuspendLayout()
        CType(Me.TxtDisplayTransfer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Enabled = False
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(176, 12)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearch.TabIndex = 9
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        '_TxtDisplayTransfer_1
        '
        Me._TxtDisplayTransfer_1.AcceptsReturn = True
        Me._TxtDisplayTransfer_1.BackColor = System.Drawing.Color.Black
        Me._TxtDisplayTransfer_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me._TxtDisplayTransfer_1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._TxtDisplayTransfer_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._TxtDisplayTransfer_1.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtDisplayTransfer.SetIndex(Me._TxtDisplayTransfer_1, CType(1, Short))
        Me._TxtDisplayTransfer_1.Location = New System.Drawing.Point(0, 190)
        Me._TxtDisplayTransfer_1.MaxLength = 0
        Me._TxtDisplayTransfer_1.Multiline = True
        Me._TxtDisplayTransfer_1.Name = "_TxtDisplayTransfer_1"
        Me._TxtDisplayTransfer_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._TxtDisplayTransfer_1.Size = New System.Drawing.Size(335, 115)
        Me._TxtDisplayTransfer_1.TabIndex = 13
        '
        '_TxtDisplayTransfer_0
        '
        Me._TxtDisplayTransfer_0.AcceptsReturn = True
        Me._TxtDisplayTransfer_0.BackColor = System.Drawing.Color.Black
        Me._TxtDisplayTransfer_0.Cursor = System.Windows.Forms.Cursors.IBeam
        Me._TxtDisplayTransfer_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._TxtDisplayTransfer_0.ForeColor = System.Drawing.SystemColors.Window
        Me.TxtDisplayTransfer.SetIndex(Me._TxtDisplayTransfer_0, CType(0, Short))
        Me._TxtDisplayTransfer_0.Location = New System.Drawing.Point(0, 116)
        Me._TxtDisplayTransfer_0.MaxLength = 0
        Me._TxtDisplayTransfer_0.Multiline = True
        Me._TxtDisplayTransfer_0.Name = "_TxtDisplayTransfer_0"
        Me._TxtDisplayTransfer_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._TxtDisplayTransfer_0.Size = New System.Drawing.Size(339, 189)
        Me._TxtDisplayTransfer_0.TabIndex = 14
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.OptParticularAccount)
        Me.FraAccount.Controls.Add(Me.OptAllAccount)
        Me.FraAccount.Controls.Add(Me.txtName)
        Me.FraAccount.Controls.Add(Me.cmdSearch)
        Me.FraAccount.Controls.Add(Me.lblName)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FraAccount.Location = New System.Drawing.Point(0, 62)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(339, 53)
        Me.FraAccount.TabIndex = 8
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Employee"
        '
        'OptParticularAccount
        '
        Me.OptParticularAccount.AutoSize = True
        Me.OptParticularAccount.BackColor = System.Drawing.SystemColors.Control
        Me.OptParticularAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptParticularAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptParticularAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptParticularAccount.Location = New System.Drawing.Point(6, 16)
        Me.OptParticularAccount.Name = "OptParticularAccount"
        Me.OptParticularAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptParticularAccount.Size = New System.Drawing.Size(70, 18)
        Me.OptParticularAccount.TabIndex = 12
        Me.OptParticularAccount.TabStop = True
        Me.OptParticularAccount.Text = "Particular"
        Me.OptParticularAccount.UseVisualStyleBackColor = False
        '
        'OptAllAccount
        '
        Me.OptAllAccount.AutoSize = True
        Me.OptAllAccount.BackColor = System.Drawing.SystemColors.Control
        Me.OptAllAccount.Checked = True
        Me.OptAllAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptAllAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptAllAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAllAccount.Location = New System.Drawing.Point(6, 36)
        Me.OptAllAccount.Name = "OptAllAccount"
        Me.OptAllAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptAllAccount.Size = New System.Drawing.Size(37, 18)
        Me.OptAllAccount.TabIndex = 11
        Me.OptAllAccount.TabStop = True
        Me.OptAllAccount.Text = "All"
        Me.OptAllAccount.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Enabled = False
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.Blue
        Me.txtName.Location = New System.Drawing.Point(98, 12)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(77, 20)
        Me.txtName.TabIndex = 10
        '
        'lblName
        '
        Me.lblName.BackColor = System.Drawing.SystemColors.Control
        Me.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblName.Location = New System.Drawing.Point(98, 32)
        Me.lblName.Name = "lblName"
        Me.lblName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblName.Size = New System.Drawing.Size(237, 17)
        Me.lblName.TabIndex = 15
        '
        'FraFYear
        '
        Me.FraFYear.BackColor = System.Drawing.SystemColors.Control
        Me.FraFYear.Controls.Add(Me.CboFYearTo)
        Me.FraFYear.Controls.Add(Me.CboFYearFrom)
        Me.FraFYear.Controls.Add(Me.Label2)
        Me.FraFYear.Controls.Add(Me.Label1)
        Me.FraFYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFYear.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FraFYear.Location = New System.Drawing.Point(0, 0)
        Me.FraFYear.Name = "FraFYear"
        Me.FraFYear.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFYear.Size = New System.Drawing.Size(339, 61)
        Me.FraFYear.TabIndex = 3
        Me.FraFYear.TabStop = False
        Me.FraFYear.Text = "Transfer Opening Balance"
        '
        'CboFYearTo
        '
        Me.CboFYearTo.BackColor = System.Drawing.SystemColors.Window
        Me.CboFYearTo.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboFYearTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboFYearTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboFYearTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboFYearTo.Location = New System.Drawing.Point(138, 34)
        Me.CboFYearTo.Name = "CboFYearTo"
        Me.CboFYearTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboFYearTo.Size = New System.Drawing.Size(199, 22)
        Me.CboFYearTo.TabIndex = 5
        '
        'CboFYearFrom
        '
        Me.CboFYearFrom.BackColor = System.Drawing.SystemColors.Window
        Me.CboFYearFrom.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboFYearFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboFYearFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboFYearFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboFYearFrom.Location = New System.Drawing.Point(138, 10)
        Me.CboFYearFrom.Name = "CboFYearFrom"
        Me.CboFYearFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboFYearFrom.Size = New System.Drawing.Size(199, 22)
        Me.CboFYearFrom.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(42, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(93, 17)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Year To :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(42, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(93, 17)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Year From :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraButton
        '
        Me.FraButton.BackColor = System.Drawing.SystemColors.Control
        Me.FraButton.Controls.Add(Me.cmdClose)
        Me.FraButton.Controls.Add(Me.cmdStart)
        Me.FraButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraButton.Location = New System.Drawing.Point(0, 302)
        Me.FraButton.Name = "FraButton"
        Me.FraButton.Padding = New System.Windows.Forms.Padding(0)
        Me.FraButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraButton.Size = New System.Drawing.Size(339, 43)
        Me.FraButton.TabIndex = 0
        Me.FraButton.TabStop = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(208, 14)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(87, 23)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdStart
        '
        Me.cmdStart.BackColor = System.Drawing.SystemColors.Control
        Me.cmdStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdStart.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdStart.Location = New System.Drawing.Point(52, 14)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdStart.Size = New System.Drawing.Size(87, 23)
        Me.cmdStart.TabIndex = 1
        Me.cmdStart.Text = "Start"
        Me.cmdStart.UseVisualStyleBackColor = False
        '
        'frmTrfLeaveBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(340, 346)
        Me.Controls.Add(Me._TxtDisplayTransfer_1)
        Me.Controls.Add(Me._TxtDisplayTransfer_0)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.FraFYear)
        Me.Controls.Add(Me.FraButton)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmTrfLeaveBalance"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Transfer Leave Balance"
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.FraFYear.ResumeLayout(False)
        Me.FraButton.ResumeLayout(False)
        CType(Me.TxtDisplayTransfer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class