Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmProcessMonthlySchld
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
        ''InventoryGST.Master.Show
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
    Public WithEvents cmdProcess As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblBookSubType As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    'Public WithEvents PBar As AxComctlLib.AxProgressBar
    Public WithEvents chkInhouseItem As System.Windows.Forms.CheckBox
    Public WithEvents txtBufferPer As System.Windows.Forms.TextBox
    Public WithEvents txtDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtStockDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_2 As System.Windows.Forms.Label
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents fraDate As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cmdProcess = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblBookSubType = New System.Windows.Forms.Label()
        Me.fraDate = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optCustomerSchedule = New System.Windows.Forms.RadioButton()
        Me.optPlanning = New System.Windows.Forms.RadioButton()
        Me.chkInhouseItem = New System.Windows.Forms.CheckBox()
        Me.txtBufferPer = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtStockDate = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_2 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.fraDate.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdProcess)
        Me.Frame1.Controls.Add(Me.cmdClose)
        Me.Frame1.Controls.Add(Me.lblBookType)
        Me.Frame1.Controls.Add(Me.lblBookSubType)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 182)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(367, 53)
        Me.Frame1.TabIndex = 4
        Me.Frame1.TabStop = False
        '
        'cmdProcess
        '
        Me.cmdProcess.BackColor = System.Drawing.SystemColors.Control
        Me.cmdProcess.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdProcess.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdProcess.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdProcess.Location = New System.Drawing.Point(6, 12)
        Me.cmdProcess.Name = "cmdProcess"
        Me.cmdProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdProcess.Size = New System.Drawing.Size(80, 37)
        Me.cmdProcess.TabIndex = 6
        Me.cmdProcess.Text = "Process"
        Me.cmdProcess.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Location = New System.Drawing.Point(282, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(80, 37)
        Me.cmdClose.TabIndex = 5
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(141, 12)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(71, 13)
        Me.lblBookType.TabIndex = 8
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblBookSubType
        '
        Me.lblBookSubType.AutoSize = True
        Me.lblBookSubType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookSubType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookSubType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookSubType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookSubType.Location = New System.Drawing.Point(132, 32)
        Me.lblBookSubType.Name = "lblBookSubType"
        Me.lblBookSubType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookSubType.Size = New System.Drawing.Size(90, 13)
        Me.lblBookSubType.TabIndex = 7
        Me.lblBookSubType.Text = "lblBookSubType"
        Me.lblBookSubType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'fraDate
        '
        Me.fraDate.BackColor = System.Drawing.SystemColors.Control
        Me.fraDate.Controls.Add(Me.GroupBox1)
        Me.fraDate.Controls.Add(Me.chkInhouseItem)
        Me.fraDate.Controls.Add(Me.txtBufferPer)
        Me.fraDate.Controls.Add(Me.txtDate)
        Me.fraDate.Controls.Add(Me.txtStockDate)
        Me.fraDate.Controls.Add(Me._Lbl_2)
        Me.fraDate.Controls.Add(Me._Lbl_1)
        Me.fraDate.Controls.Add(Me._Lbl_0)
        Me.fraDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDate.Location = New System.Drawing.Point(0, 0)
        Me.fraDate.Name = "fraDate"
        Me.fraDate.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDate.Size = New System.Drawing.Size(367, 187)
        Me.fraDate.TabIndex = 0
        Me.fraDate.TabStop = False
        Me.fraDate.Text = "Date"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.optCustomerSchedule)
        Me.GroupBox1.Controls.Add(Me.optPlanning)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(15, 95)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(341, 62)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        '
        'optCustomerSchedule
        '
        Me.optCustomerSchedule.AutoSize = True
        Me.optCustomerSchedule.BackColor = System.Drawing.SystemColors.Control
        Me.optCustomerSchedule.Checked = True
        Me.optCustomerSchedule.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCustomerSchedule.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCustomerSchedule.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCustomerSchedule.Location = New System.Drawing.Point(13, 14)
        Me.optCustomerSchedule.Name = "optCustomerSchedule"
        Me.optCustomerSchedule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCustomerSchedule.Size = New System.Drawing.Size(152, 17)
        Me.optCustomerSchedule.TabIndex = 32
        Me.optCustomerSchedule.TabStop = True
        Me.optCustomerSchedule.Text = "From Customer Schedule"
        Me.optCustomerSchedule.UseVisualStyleBackColor = False
        '
        'optPlanning
        '
        Me.optPlanning.AutoSize = True
        Me.optPlanning.BackColor = System.Drawing.SystemColors.Control
        Me.optPlanning.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPlanning.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPlanning.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPlanning.Location = New System.Drawing.Point(13, 37)
        Me.optPlanning.Name = "optPlanning"
        Me.optPlanning.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPlanning.Size = New System.Drawing.Size(156, 17)
        Me.optPlanning.TabIndex = 31
        Me.optPlanning.TabStop = True
        Me.optPlanning.Text = "From Production Planning"
        Me.optPlanning.UseVisualStyleBackColor = False
        '
        'chkInhouseItem
        '
        Me.chkInhouseItem.AutoSize = True
        Me.chkInhouseItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkInhouseItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInhouseItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInhouseItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInhouseItem.Location = New System.Drawing.Point(12, 166)
        Me.chkInhouseItem.Name = "chkInhouseItem"
        Me.chkInhouseItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInhouseItem.Size = New System.Drawing.Size(212, 17)
        Me.chkInhouseItem.TabIndex = 13
        Me.chkInhouseItem.Text = "Including BOP which are Inhouse also"
        Me.chkInhouseItem.UseVisualStyleBackColor = False
        '
        'txtBufferPer
        '
        Me.txtBufferPer.AcceptsReturn = True
        Me.txtBufferPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtBufferPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBufferPer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBufferPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBufferPer.Location = New System.Drawing.Point(156, 68)
        Me.txtBufferPer.MaxLength = 0
        Me.txtBufferPer.Name = "txtBufferPer"
        Me.txtBufferPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBufferPer.Size = New System.Drawing.Size(83, 22)
        Me.txtBufferPer.TabIndex = 12
        Me.txtBufferPer.Text = "0"
        '
        'txtDate
        '
        Me.txtDate.AllowPromptAsInput = False
        Me.txtDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Location = New System.Drawing.Point(158, 16)
        Me.txtDate.Mask = "##/##/####"
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(80, 22)
        Me.txtDate.TabIndex = 1
        '
        'txtStockDate
        '
        Me.txtStockDate.AllowPromptAsInput = False
        Me.txtStockDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockDate.Location = New System.Drawing.Point(158, 42)
        Me.txtStockDate.Mask = "##/##/####"
        Me.txtStockDate.Name = "txtStockDate"
        Me.txtStockDate.Size = New System.Drawing.Size(80, 22)
        Me.txtStockDate.TabIndex = 9
        '
        '_Lbl_2
        '
        Me._Lbl_2.AutoSize = True
        Me._Lbl_2.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_2, CType(2, Short))
        Me._Lbl_2.Location = New System.Drawing.Point(66, 71)
        Me._Lbl_2.Name = "_Lbl_2"
        Me._Lbl_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_2.Size = New System.Drawing.Size(59, 13)
        Me._Lbl_2.TabIndex = 11
        Me._Lbl_2.Text = "Buffer % : "
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(66, 45)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(71, 13)
        Me._Lbl_1.TabIndex = 10
        Me._Lbl_1.Text = "Stock Date : "
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(66, 19)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(86, 13)
        Me._Lbl_0.TabIndex = 2
        Me._Lbl_0.Text = "Planning Date : "
        '
        'frmProcessMonthlySchld
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(368, 237)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.fraDate)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmProcessMonthlySchld"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Process Monthly Schedule"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.fraDate.ResumeLayout(False)
        Me.fraDate.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents optCustomerSchedule As RadioButton
    Public WithEvents optPlanning As RadioButton
#End Region
End Class