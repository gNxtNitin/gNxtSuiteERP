Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintVoucher
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
    Public WithEvents optRTGSLetter As System.Windows.Forms.RadioButton
    Public WithEvents optHundiAdvise As System.Windows.Forms.RadioButton
    Public WithEvents OptReceiptExcel As System.Windows.Forms.RadioButton
    Public WithEvents OptReceiptWithDue As System.Windows.Forms.RadioButton
    Public WithEvents optLoanPrint As System.Windows.Forms.RadioButton
    Public WithEvents OptBankAdvise As System.Windows.Forms.RadioButton
    Public WithEvents optDNVoucher As System.Windows.Forms.RadioButton
    Public WithEvents chkPrintType As System.Windows.Forms.CheckBox
    Public WithEvents OptDnCn As System.Windows.Forms.RadioButton
    Public WithEvents OptItemRecevied As System.Windows.Forms.RadioButton
    Public WithEvents OptVoucher As System.Windows.Forms.RadioButton
    Public WithEvents OptReceipt As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintVoucher))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.optRTGSLetter = New System.Windows.Forms.RadioButton()
        Me.optHundiAdvise = New System.Windows.Forms.RadioButton()
        Me.OptReceiptExcel = New System.Windows.Forms.RadioButton()
        Me.OptReceiptWithDue = New System.Windows.Forms.RadioButton()
        Me.optLoanPrint = New System.Windows.Forms.RadioButton()
        Me.OptBankAdvise = New System.Windows.Forms.RadioButton()
        Me.optDNVoucher = New System.Windows.Forms.RadioButton()
        Me.chkPrintType = New System.Windows.Forms.CheckBox()
        Me.OptDnCn = New System.Windows.Forms.RadioButton()
        Me.OptItemRecevied = New System.Windows.Forms.RadioButton()
        Me.OptVoucher = New System.Windows.Forms.RadioButton()
        Me.OptReceipt = New System.Windows.Forms.RadioButton()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.Frame1.SuspendLayout()
        Me.FraOk.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.optRTGSLetter)
        Me.Frame1.Controls.Add(Me.optHundiAdvise)
        Me.Frame1.Controls.Add(Me.OptReceiptExcel)
        Me.Frame1.Controls.Add(Me.OptReceiptWithDue)
        Me.Frame1.Controls.Add(Me.optLoanPrint)
        Me.Frame1.Controls.Add(Me.OptBankAdvise)
        Me.Frame1.Controls.Add(Me.optDNVoucher)
        Me.Frame1.Controls.Add(Me.chkPrintType)
        Me.Frame1.Controls.Add(Me.OptDnCn)
        Me.Frame1.Controls.Add(Me.OptItemRecevied)
        Me.Frame1.Controls.Add(Me.OptVoucher)
        Me.Frame1.Controls.Add(Me.OptReceipt)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(201, 219)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Printing Option"
        '
        'optRTGSLetter
        '
        Me.optRTGSLetter.AutoSize = True
        Me.optRTGSLetter.BackColor = System.Drawing.SystemColors.Control
        Me.optRTGSLetter.Cursor = System.Windows.Forms.Cursors.Default
        Me.optRTGSLetter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optRTGSLetter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRTGSLetter.Location = New System.Drawing.Point(10, 196)
        Me.optRTGSLetter.Name = "optRTGSLetter"
        Me.optRTGSLetter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optRTGSLetter.Size = New System.Drawing.Size(84, 18)
        Me.optRTGSLetter.TabIndex = 15
        Me.optRTGSLetter.TabStop = True
        Me.optRTGSLetter.Text = "RTGS Letter"
        Me.optRTGSLetter.UseVisualStyleBackColor = False
        Me.optRTGSLetter.Visible = False
        '
        'optHundiAdvise
        '
        Me.optHundiAdvise.AutoSize = True
        Me.optHundiAdvise.BackColor = System.Drawing.SystemColors.Control
        Me.optHundiAdvise.Cursor = System.Windows.Forms.Cursors.Default
        Me.optHundiAdvise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optHundiAdvise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optHundiAdvise.Location = New System.Drawing.Point(10, 70)
        Me.optHundiAdvise.Name = "optHundiAdvise"
        Me.optHundiAdvise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optHundiAdvise.Size = New System.Drawing.Size(88, 18)
        Me.optHundiAdvise.TabIndex = 14
        Me.optHundiAdvise.TabStop = True
        Me.optHundiAdvise.Text = "Hundi Advice"
        Me.optHundiAdvise.UseVisualStyleBackColor = False
        '
        'OptReceiptExcel
        '
        Me.OptReceiptExcel.AutoSize = True
        Me.OptReceiptExcel.BackColor = System.Drawing.SystemColors.Control
        Me.OptReceiptExcel.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptReceiptExcel.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptReceiptExcel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptReceiptExcel.Location = New System.Drawing.Point(10, 178)
        Me.OptReceiptExcel.Name = "OptReceiptExcel"
        Me.OptReceiptExcel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptReceiptExcel.Size = New System.Drawing.Size(126, 18)
        Me.OptReceiptExcel.TabIndex = 13
        Me.OptReceiptExcel.TabStop = True
        Me.OptReceiptExcel.Text = "Advice (In Excel File)"
        Me.OptReceiptExcel.UseVisualStyleBackColor = False
        '
        'OptReceiptWithDue
        '
        Me.OptReceiptWithDue.AutoSize = True
        Me.OptReceiptWithDue.BackColor = System.Drawing.SystemColors.Control
        Me.OptReceiptWithDue.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptReceiptWithDue.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptReceiptWithDue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptReceiptWithDue.Location = New System.Drawing.Point(10, 88)
        Me.OptReceiptWithDue.Name = "OptReceiptWithDue"
        Me.OptReceiptWithDue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptReceiptWithDue.Size = New System.Drawing.Size(138, 18)
        Me.OptReceiptWithDue.TabIndex = 12
        Me.OptReceiptWithDue.TabStop = True
        Me.OptReceiptWithDue.Text = "Advice (With Due Date)"
        Me.OptReceiptWithDue.UseVisualStyleBackColor = False
        '
        'optLoanPrint
        '
        Me.optLoanPrint.AutoSize = True
        Me.optLoanPrint.BackColor = System.Drawing.SystemColors.Control
        Me.optLoanPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.optLoanPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optLoanPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optLoanPrint.Location = New System.Drawing.Point(10, 160)
        Me.optLoanPrint.Name = "optLoanPrint"
        Me.optLoanPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optLoanPrint.Size = New System.Drawing.Size(141, 18)
        Me.optLoanPrint.TabIndex = 11
        Me.optLoanPrint.TabStop = True
        Me.optLoanPrint.Text = "Employee Loan Register"
        Me.optLoanPrint.UseVisualStyleBackColor = False
        '
        'OptBankAdvise
        '
        Me.OptBankAdvise.AutoSize = True
        Me.OptBankAdvise.BackColor = System.Drawing.SystemColors.Control
        Me.OptBankAdvise.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptBankAdvise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptBankAdvise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptBankAdvise.Location = New System.Drawing.Point(10, 124)
        Me.OptBankAdvise.Name = "OptBankAdvise"
        Me.OptBankAdvise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptBankAdvise.Size = New System.Drawing.Size(144, 18)
        Me.OptBankAdvise.TabIndex = 10
        Me.OptBankAdvise.TabStop = True
        Me.OptBankAdvise.Text = "Bank Discounting Advice"
        Me.OptBankAdvise.UseVisualStyleBackColor = False
        '
        'optDNVoucher
        '
        Me.optDNVoucher.AutoSize = True
        Me.optDNVoucher.BackColor = System.Drawing.SystemColors.Control
        Me.optDNVoucher.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDNVoucher.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDNVoucher.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDNVoucher.Location = New System.Drawing.Point(10, 34)
        Me.optDNVoucher.Name = "optDNVoucher"
        Me.optDNVoucher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDNVoucher.Size = New System.Drawing.Size(162, 18)
        Me.optDNVoucher.TabIndex = 2
        Me.optDNVoucher.TabStop = True
        Me.optDNVoucher.Text = "Voucher (Debit Note Format)"
        Me.optDNVoucher.UseVisualStyleBackColor = False
        '
        'chkPrintType
        '
        Me.chkPrintType.AutoSize = True
        Me.chkPrintType.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintType.Checked = True
        Me.chkPrintType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrintType.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintType.Location = New System.Drawing.Point(94, 143)
        Me.chkPrintType.Name = "chkPrintType"
        Me.chkPrintType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintType.Size = New System.Drawing.Size(95, 18)
        Me.chkPrintType.TabIndex = 6
        Me.chkPrintType.Text = "Printed Format"
        Me.chkPrintType.UseVisualStyleBackColor = False
        '
        'OptDnCn
        '
        Me.OptDnCn.AutoSize = True
        Me.OptDnCn.BackColor = System.Drawing.SystemColors.Control
        Me.OptDnCn.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptDnCn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptDnCn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDnCn.Location = New System.Drawing.Point(10, 142)
        Me.OptDnCn.Name = "OptDnCn"
        Me.OptDnCn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptDnCn.Size = New System.Drawing.Size(74, 18)
        Me.OptDnCn.TabIndex = 5
        Me.OptDnCn.TabStop = True
        Me.OptDnCn.Text = "Debit Note"
        Me.OptDnCn.UseVisualStyleBackColor = False
        '
        'OptItemRecevied
        '
        Me.OptItemRecevied.AutoSize = True
        Me.OptItemRecevied.BackColor = System.Drawing.SystemColors.Control
        Me.OptItemRecevied.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptItemRecevied.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptItemRecevied.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptItemRecevied.Location = New System.Drawing.Point(10, 106)
        Me.OptItemRecevied.Name = "OptItemRecevied"
        Me.OptItemRecevied.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptItemRecevied.Size = New System.Drawing.Size(92, 18)
        Me.OptItemRecevied.TabIndex = 4
        Me.OptItemRecevied.TabStop = True
        Me.OptItemRecevied.Text = "Item Recevied"
        Me.OptItemRecevied.UseVisualStyleBackColor = False
        '
        'OptVoucher
        '
        Me.OptVoucher.AutoSize = True
        Me.OptVoucher.BackColor = System.Drawing.SystemColors.Control
        Me.OptVoucher.Checked = True
        Me.OptVoucher.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptVoucher.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptVoucher.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptVoucher.Location = New System.Drawing.Point(10, 16)
        Me.OptVoucher.Name = "OptVoucher"
        Me.OptVoucher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptVoucher.Size = New System.Drawing.Size(66, 18)
        Me.OptVoucher.TabIndex = 1
        Me.OptVoucher.TabStop = True
        Me.OptVoucher.Text = "Voucher"
        Me.OptVoucher.UseVisualStyleBackColor = False
        '
        'OptReceipt
        '
        Me.OptReceipt.AutoSize = True
        Me.OptReceipt.BackColor = System.Drawing.SystemColors.Control
        Me.OptReceipt.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptReceipt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptReceipt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptReceipt.Location = New System.Drawing.Point(10, 52)
        Me.OptReceipt.Name = "OptReceipt"
        Me.OptReceipt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptReceipt.Size = New System.Drawing.Size(59, 18)
        Me.OptReceipt.TabIndex = 3
        Me.OptReceipt.TabStop = True
        Me.OptReceipt.Text = "Advice"
        Me.OptReceipt.UseVisualStyleBackColor = False
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 214)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(201, 43)
        Me.FraOk.TabIndex = 9
        Me.FraOk.TabStop = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(8, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(73, 25)
        Me.cmdOk.TabIndex = 7
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
        Me.cmdCancel.Location = New System.Drawing.Point(116, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 25)
        Me.cmdCancel.TabIndex = 8
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'frmPrintVoucher
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(201, 258)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintVoucher"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraOk.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class