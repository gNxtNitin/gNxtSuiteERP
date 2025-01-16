Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintOTReg
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
    Public WithEvents txtBankName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchBank As System.Windows.Forms.Button
    Public WithEvents _optAllBank_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optAllBank_1 As System.Windows.Forms.RadioButton
    Public WithEvents fraBankName As System.Windows.Forms.GroupBox
    Public WithEvents optBankTxt As System.Windows.Forms.RadioButton
    Public WithEvents optCash As System.Windows.Forms.RadioButton
    Public WithEvents optBank As System.Windows.Forms.RadioButton
    Public WithEvents optCheckList As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public WithEvents optAllBank As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintOTReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchBank = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.fraBankName = New System.Windows.Forms.GroupBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me._optAllBank_0 = New System.Windows.Forms.RadioButton()
        Me._optAllBank_1 = New System.Windows.Forms.RadioButton()
        Me.optBankTxt = New System.Windows.Forms.RadioButton()
        Me.optCash = New System.Windows.Forms.RadioButton()
        Me.optBank = New System.Windows.Forms.RadioButton()
        Me.optCheckList = New System.Windows.Forms.RadioButton()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.optAllBank = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.fraBankName.SuspendLayout()
        Me.FraOk.SuspendLayout()
        CType(Me.optAllBank, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchBank
        '
        Me.cmdSearchBank.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchBank.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchBank.Enabled = False
        Me.cmdSearchBank.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchBank.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchBank.Image = CType(resources.GetObject("cmdSearchBank.Image"), System.Drawing.Image)
        Me.cmdSearchBank.Location = New System.Drawing.Point(174, 34)
        Me.cmdSearchBank.Name = "cmdSearchBank"
        Me.cmdSearchBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchBank.Size = New System.Drawing.Size(25, 19)
        Me.cmdSearchBank.TabIndex = 11
        Me.cmdSearchBank.TabStop = False
        Me.cmdSearchBank.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchBank, "Search")
        Me.cmdSearchBank.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.fraBankName)
        Me.Frame1.Controls.Add(Me.optBankTxt)
        Me.Frame1.Controls.Add(Me.optCash)
        Me.Frame1.Controls.Add(Me.optBank)
        Me.Frame1.Controls.Add(Me.optCheckList)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(205, 121)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Printing Status"
        '
        'fraBankName
        '
        Me.fraBankName.BackColor = System.Drawing.SystemColors.Control
        Me.fraBankName.Controls.Add(Me.txtBankName)
        Me.fraBankName.Controls.Add(Me.cmdSearchBank)
        Me.fraBankName.Controls.Add(Me._optAllBank_0)
        Me.fraBankName.Controls.Add(Me._optAllBank_1)
        Me.fraBankName.Enabled = False
        Me.fraBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBankName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBankName.Location = New System.Drawing.Point(0, 62)
        Me.fraBankName.Name = "fraBankName"
        Me.fraBankName.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBankName.Size = New System.Drawing.Size(205, 59)
        Me.fraBankName.TabIndex = 8
        Me.fraBankName.TabStop = False
        Me.fraBankName.Text = "Bank Name"
        Me.fraBankName.Visible = False
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Enabled = False
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBankName.Location = New System.Drawing.Point(2, 34)
        Me.txtBankName.MaxLength = 0
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(171, 19)
        Me.txtBankName.TabIndex = 12
        '
        '_optAllBank_0
        '
        Me._optAllBank_0.AutoSize = True
        Me._optAllBank_0.BackColor = System.Drawing.SystemColors.Control
        Me._optAllBank_0.Checked = True
        Me._optAllBank_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAllBank_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAllBank_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAllBank.SetIndex(Me._optAllBank_0, CType(0, Short))
        Me._optAllBank_0.Location = New System.Drawing.Point(10, 16)
        Me._optAllBank_0.Name = "_optAllBank_0"
        Me._optAllBank_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAllBank_0.Size = New System.Drawing.Size(37, 18)
        Me._optAllBank_0.TabIndex = 10
        Me._optAllBank_0.TabStop = True
        Me._optAllBank_0.Text = "All"
        Me._optAllBank_0.UseVisualStyleBackColor = False
        '
        '_optAllBank_1
        '
        Me._optAllBank_1.AutoSize = True
        Me._optAllBank_1.BackColor = System.Drawing.SystemColors.Control
        Me._optAllBank_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAllBank_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAllBank_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAllBank.SetIndex(Me._optAllBank_1, CType(1, Short))
        Me._optAllBank_1.Location = New System.Drawing.Point(110, 16)
        Me._optAllBank_1.Name = "_optAllBank_1"
        Me._optAllBank_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAllBank_1.Size = New System.Drawing.Size(76, 18)
        Me._optAllBank_1.TabIndex = 9
        Me._optAllBank_1.TabStop = True
        Me._optAllBank_1.Text = "Particulars"
        Me._optAllBank_1.UseVisualStyleBackColor = False
        '
        'optBankTxt
        '
        Me.optBankTxt.AutoSize = True
        Me.optBankTxt.BackColor = System.Drawing.SystemColors.Control
        Me.optBankTxt.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBankTxt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBankTxt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBankTxt.Location = New System.Drawing.Point(106, 42)
        Me.optBankTxt.Name = "optBankTxt"
        Me.optBankTxt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBankTxt.Size = New System.Drawing.Size(64, 18)
        Me.optBankTxt.TabIndex = 7
        Me.optBankTxt.TabStop = True
        Me.optBankTxt.Text = "Text File"
        Me.optBankTxt.UseVisualStyleBackColor = False
        '
        'optCash
        '
        Me.optCash.AutoSize = True
        Me.optCash.BackColor = System.Drawing.SystemColors.Control
        Me.optCash.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCash.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCash.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCash.Location = New System.Drawing.Point(106, 20)
        Me.optCash.Name = "optCash"
        Me.optCash.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCash.Size = New System.Drawing.Size(70, 18)
        Me.optCash.TabIndex = 6
        Me.optCash.TabStop = True
        Me.optCash.Text = "Cash List"
        Me.optCash.UseVisualStyleBackColor = False
        '
        'optBank
        '
        Me.optBank.AutoSize = True
        Me.optBank.BackColor = System.Drawing.SystemColors.Control
        Me.optBank.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBank.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBank.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBank.Location = New System.Drawing.Point(6, 42)
        Me.optBank.Name = "optBank"
        Me.optBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBank.Size = New System.Drawing.Size(80, 18)
        Me.optBank.TabIndex = 5
        Me.optBank.TabStop = True
        Me.optBank.Text = "Bank Letter"
        Me.optBank.UseVisualStyleBackColor = False
        '
        'optCheckList
        '
        Me.optCheckList.AutoSize = True
        Me.optCheckList.BackColor = System.Drawing.SystemColors.Control
        Me.optCheckList.Checked = True
        Me.optCheckList.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCheckList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCheckList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCheckList.Location = New System.Drawing.Point(6, 20)
        Me.optCheckList.Name = "optCheckList"
        Me.optCheckList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCheckList.Size = New System.Drawing.Size(75, 18)
        Me.optCheckList.TabIndex = 1
        Me.optCheckList.TabStop = True
        Me.optCheckList.Text = "Check List"
        Me.optCheckList.UseVisualStyleBackColor = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 116)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(205, 43)
        Me.FraOk.TabIndex = 2
        Me.FraOk.TabStop = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(4, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(57, 25)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(142, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(57, 25)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'optAllBank
        '
        '
        'frmPrintOTReg
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(205, 160)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintOTReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.fraBankName.ResumeLayout(False)
        Me.fraBankName.PerformLayout()
        Me.FraOk.ResumeLayout(False)
        CType(Me.optAllBank, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class