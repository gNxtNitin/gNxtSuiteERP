Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmProcessProductWiseInventory
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

        'InventoryGST.Master.Show
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
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    'Public WithEvents PBar As AxComctlLib.AxProgressBar
    Public WithEvents chkInhouseItem As System.Windows.Forms.CheckBox
    Public WithEvents txtFromDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtStockDate As System.Windows.Forms.MaskedTextBox
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
        Me.fraDate = New System.Windows.Forms.GroupBox()
        Me.chkInhouseItem = New System.Windows.Forms.CheckBox()
        Me.txtFromDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtStockDate = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Lbl = New VB6.LabelArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.fraDate.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdProcess)
        Me.Frame1.Controls.Add(Me.cmdClose)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 144)
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
        'fraDate
        '
        Me.fraDate.BackColor = System.Drawing.SystemColors.Control
        Me.fraDate.Controls.Add(Me.chkInhouseItem)
        Me.fraDate.Controls.Add(Me.txtFromDate)
        Me.fraDate.Controls.Add(Me.txtStockDate)
        Me.fraDate.Controls.Add(Me._Lbl_1)
        Me.fraDate.Controls.Add(Me._Lbl_0)
        Me.fraDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDate.Location = New System.Drawing.Point(0, 0)
        Me.fraDate.Name = "fraDate"
        Me.fraDate.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDate.Size = New System.Drawing.Size(367, 127)
        Me.fraDate.TabIndex = 0
        Me.fraDate.TabStop = False
        Me.fraDate.Text = "Date"
        '
        'chkInhouseItem
        '
        Me.chkInhouseItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkInhouseItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInhouseItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInhouseItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInhouseItem.Location = New System.Drawing.Point(68, 100)
        Me.chkInhouseItem.Name = "chkInhouseItem"
        Me.chkInhouseItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInhouseItem.Size = New System.Drawing.Size(241, 17)
        Me.chkInhouseItem.TabIndex = 9
        Me.chkInhouseItem.Text = "Including BOP which are Inhouse also"
        Me.chkInhouseItem.UseVisualStyleBackColor = False
        '
        'txtFromDate
        '
        Me.txtFromDate.AllowPromptAsInput = False
        Me.txtFromDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDate.Location = New System.Drawing.Point(158, 16)
        Me.txtFromDate.Mask = "##/##/####"
        Me.txtFromDate.Name = "txtFromDate"
        Me.txtFromDate.Size = New System.Drawing.Size(80, 20)
        Me.txtFromDate.TabIndex = 1
        '
        'txtStockDate
        '
        Me.txtStockDate.AllowPromptAsInput = False
        Me.txtStockDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockDate.Location = New System.Drawing.Point(158, 42)
        Me.txtStockDate.Mask = "##/##/####"
        Me.txtStockDate.Name = "txtStockDate"
        Me.txtStockDate.Size = New System.Drawing.Size(80, 20)
        Me.txtStockDate.TabIndex = 7
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
        Me._Lbl_1.Size = New System.Drawing.Size(52, 14)
        Me._Lbl_1.TabIndex = 8
        Me._Lbl_1.Text = "To Date : "
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
        Me._Lbl_0.Size = New System.Drawing.Size(65, 14)
        Me._Lbl_0.TabIndex = 2
        Me._Lbl_0.Text = "From Date : "
        '
        'frmProcessProductWiseInventory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(368, 197)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.fraDate)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmProcessProductWiseInventory"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Process Productwise Inventory"
        Me.Frame1.ResumeLayout(False)
        Me.fraDate.ResumeLayout(False)
        Me.fraDate.PerformLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class