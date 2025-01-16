Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmSuppAmendProcess
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
        '
        '
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
	Public WithEvents _OptItem_0 As System.Windows.Forms.RadioButton
	Public WithEvents _OptItem_1 As System.Windows.Forms.RadioButton
	Public WithEvents cmdSearchItem As System.Windows.Forms.Button
	Public WithEvents txtItem As System.Windows.Forms.TextBox
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents _optCustomer_0 As System.Windows.Forms.RadioButton
	Public WithEvents _optCustomer_1 As System.Windows.Forms.RadioButton
	Public WithEvents TxtAccount As System.Windows.Forms.TextBox
	Public WithEvents cmdsearch As System.Windows.Forms.Button
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	Public WithEvents TxtDtFrom As System.Windows.Forms.MaskedTextBox
	Public WithEvents LblDtfr As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents cmdProcess As System.Windows.Forms.Button
	Public WithEvents cmdClose As System.Windows.Forms.Button
	Public WithEvents FraButton As System.Windows.Forms.GroupBox
	Public WithEvents OptItem As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	Public WithEvents optCustomer As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSuppAmendProcess))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._OptItem_0 = New System.Windows.Forms.RadioButton()
        Me._OptItem_1 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optCustomer_0 = New System.Windows.Forms.RadioButton()
        Me._optCustomer_1 = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.TxtDtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.LblDtfr = New System.Windows.Forms.Label()
        Me.FraButton = New System.Windows.Forms.GroupBox()
        Me.cmdProcess = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.OptItem = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optCustomer = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame4.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraButton.SuspendLayout()
        CType(Me.OptItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(396, 46)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchItem.TabIndex = 9
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'txtItem
        '
        Me.txtItem.AcceptsReturn = True
        Me.txtItem.BackColor = System.Drawing.SystemColors.Window
        Me.txtItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItem.Location = New System.Drawing.Point(4, 46)
        Me.txtItem.MaxLength = 0
        Me.txtItem.Name = "txtItem"
        Me.txtItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItem.Size = New System.Drawing.Size(391, 20)
        Me.txtItem.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtItem, "Press F1 For Help")
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(4, 40)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(391, 20)
        Me.TxtAccount.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(396, 40)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 5
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._OptItem_0)
        Me.Frame4.Controls.Add(Me._OptItem_1)
        Me.Frame4.Controls.Add(Me.cmdSearchItem)
        Me.Frame4.Controls.Add(Me.txtItem)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 120)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(435, 85)
        Me.Frame4.TabIndex = 13
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Item"
        '
        '_OptItem_0
        '
        Me._OptItem_0.AutoSize = True
        Me._OptItem_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptItem_0.Checked = True
        Me._OptItem_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptItem_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptItem_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptItem.SetIndex(Me._OptItem_0, CType(0, Short))
        Me._OptItem_0.Location = New System.Drawing.Point(102, 24)
        Me._OptItem_0.Name = "_OptItem_0"
        Me._OptItem_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptItem_0.Size = New System.Drawing.Size(39, 18)
        Me._OptItem_0.TabIndex = 6
        Me._OptItem_0.TabStop = True
        Me._OptItem_0.Text = "All"
        Me._OptItem_0.UseVisualStyleBackColor = False
        '
        '_OptItem_1
        '
        Me._OptItem_1.AutoSize = True
        Me._OptItem_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptItem_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptItem_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptItem_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptItem.SetIndex(Me._OptItem_1, CType(1, Short))
        Me._OptItem_1.Location = New System.Drawing.Point(214, 24)
        Me._OptItem_1.Name = "_OptItem_1"
        Me._OptItem_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptItem_1.Size = New System.Drawing.Size(84, 18)
        Me._OptItem_1.TabIndex = 7
        Me._OptItem_1.TabStop = True
        Me._OptItem_1.Text = "Particulars"
        Me._OptItem_1.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optCustomer_0)
        Me.Frame1.Controls.Add(Me._optCustomer_1)
        Me.Frame1.Controls.Add(Me.TxtAccount)
        Me.Frame1.Controls.Add(Me.cmdsearch)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 42)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(435, 77)
        Me.Frame1.TabIndex = 12
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Customer"
        '
        '_optCustomer_0
        '
        Me._optCustomer_0.AutoSize = True
        Me._optCustomer_0.BackColor = System.Drawing.SystemColors.Control
        Me._optCustomer_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCustomer_0.Enabled = False
        Me._optCustomer_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCustomer_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCustomer.SetIndex(Me._optCustomer_0, CType(0, Short))
        Me._optCustomer_0.Location = New System.Drawing.Point(102, 18)
        Me._optCustomer_0.Name = "_optCustomer_0"
        Me._optCustomer_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCustomer_0.Size = New System.Drawing.Size(39, 18)
        Me._optCustomer_0.TabIndex = 2
        Me._optCustomer_0.TabStop = True
        Me._optCustomer_0.Text = "All"
        Me._optCustomer_0.UseVisualStyleBackColor = False
        '
        '_optCustomer_1
        '
        Me._optCustomer_1.AutoSize = True
        Me._optCustomer_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCustomer_1.Checked = True
        Me._optCustomer_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCustomer_1.Enabled = False
        Me._optCustomer_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCustomer_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCustomer.SetIndex(Me._optCustomer_1, CType(1, Short))
        Me._optCustomer_1.Location = New System.Drawing.Point(214, 18)
        Me._optCustomer_1.Name = "_optCustomer_1"
        Me._optCustomer_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCustomer_1.Size = New System.Drawing.Size(84, 18)
        Me._optCustomer_1.TabIndex = 3
        Me._optCustomer_1.TabStop = True
        Me._optCustomer_1.Text = "Particulars"
        Me._optCustomer_1.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.TxtDtFrom)
        Me.Frame2.Controls.Add(Me.LblDtfr)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(435, 41)
        Me.Frame2.TabIndex = 0
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Date Range"
        '
        'TxtDtFrom
        '
        Me.TxtDtFrom.AllowPromptAsInput = False
        Me.TxtDtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDtFrom.Location = New System.Drawing.Point(188, 14)
        Me.TxtDtFrom.Mask = "##/##/####"
        Me.TxtDtFrom.Name = "TxtDtFrom"
        Me.TxtDtFrom.Size = New System.Drawing.Size(81, 20)
        Me.TxtDtFrom.TabIndex = 1
        '
        'LblDtfr
        '
        Me.LblDtfr.AutoSize = True
        Me.LblDtfr.BackColor = System.Drawing.SystemColors.Control
        Me.LblDtfr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDtfr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDtfr.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDtfr.Location = New System.Drawing.Point(58, 18)
        Me.LblDtfr.Name = "LblDtfr"
        Me.LblDtfr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDtfr.Size = New System.Drawing.Size(62, 14)
        Me.LblDtfr.TabIndex = 10
        Me.LblDtfr.Text = "WEF Date :"
        '
        'FraButton
        '
        Me.FraButton.BackColor = System.Drawing.SystemColors.Control
        Me.FraButton.Controls.Add(Me.cmdProcess)
        Me.FraButton.Controls.Add(Me.cmdClose)
        Me.FraButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraButton.Location = New System.Drawing.Point(0, 202)
        Me.FraButton.Name = "FraButton"
        Me.FraButton.Padding = New System.Windows.Forms.Padding(0)
        Me.FraButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraButton.Size = New System.Drawing.Size(435, 53)
        Me.FraButton.TabIndex = 11
        Me.FraButton.TabStop = False
        '
        'cmdProcess
        '
        Me.cmdProcess.BackColor = System.Drawing.SystemColors.Control
        Me.cmdProcess.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdProcess.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProcess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdProcess.Location = New System.Drawing.Point(4, 10)
        Me.cmdProcess.Name = "cmdProcess"
        Me.cmdProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdProcess.Size = New System.Drawing.Size(95, 39)
        Me.cmdProcess.TabIndex = 15
        Me.cmdProcess.Text = "Process"
        Me.cmdProcess.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(332, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(95, 39)
        Me.cmdClose.TabIndex = 14
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'OptItem
        '
        '
        'optCustomer
        '
        '
        'FrmSuppAmendProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(435, 255)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraButton)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSuppAmendProcess"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Process - Supplier Costing Amendment"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraButton.ResumeLayout(False)
        CType(Me.OptItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class