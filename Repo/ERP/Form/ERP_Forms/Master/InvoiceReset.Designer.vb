Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmInvoiceReset
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
        'Me.MDIParent = Admin.Master
        'Admin.Master.Show
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
    Public WithEvents txtReason As System.Windows.Forms.TextBox
    Public WithEvents txtInvoiceDate As System.Windows.Forms.TextBox
    Public WithEvents chkReset As System.Windows.Forms.CheckBox
    Public WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Public WithEvents Adata As VB6.ADODC
    Public WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblQty As System.Windows.Forms.Label
    Public WithEvents lblLock As System.Windows.Forms.Label
    Public WithEvents lblInvoice As System.Windows.Forms.Label
    Public WithEvents lblName As System.Windows.Forms.Label
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoiceReset))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.txtInvoiceDate = New System.Windows.Forms.TextBox()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.chkReset = New System.Windows.Forms.CheckBox()
        Me.Adata = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.lblLock = New System.Windows.Forms.Label()
        Me.lblInvoice = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.chkPackingReset = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraMain.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReason.Location = New System.Drawing.Point(118, 78)
        Me.txtReason.MaxLength = 0
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(445, 22)
        Me.txtReason.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtReason, "Press F1 For Help")
        '
        'txtInvoiceDate
        '
        Me.txtInvoiceDate.AcceptsReturn = True
        Me.txtInvoiceDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvoiceDate.Enabled = False
        Me.txtInvoiceDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInvoiceDate.Location = New System.Drawing.Point(317, 12)
        Me.txtInvoiceDate.MaxLength = 0
        Me.txtInvoiceDate.Name = "txtInvoiceDate"
        Me.txtInvoiceDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvoiceDate.Size = New System.Drawing.Size(79, 22)
        Me.txtInvoiceDate.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtInvoiceDate, "Press F1 For Help")
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.AcceptsReturn = True
        Me.txtInvoiceNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvoiceNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvoiceNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvoiceNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInvoiceNo.Location = New System.Drawing.Point(119, 12)
        Me.txtInvoiceNo.MaxLength = 0
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvoiceNo.Size = New System.Drawing.Size(79, 22)
        Me.txtInvoiceNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtInvoiceNo, "Press F1 For Help")
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AcceptsReturn = True
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerName.Enabled = False
        Me.txtCustomerName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerName.Location = New System.Drawing.Point(119, 34)
        Me.txtCustomerName.MaxLength = 0
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerName.Size = New System.Drawing.Size(445, 22)
        Me.txtCustomerName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtCustomerName, "Press F1 For Help")
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(4, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 0
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.Label2)
        Me.FraMain.Controls.Add(Me.chkPackingReset)
        Me.FraMain.Controls.Add(Me.txtReason)
        Me.FraMain.Controls.Add(Me.txtInvoiceDate)
        Me.FraMain.Controls.Add(Me.chkReset)
        Me.FraMain.Controls.Add(Me.txtInvoiceNo)
        Me.FraMain.Controls.Add(Me.Adata)
        Me.FraMain.Controls.Add(Me.txtCustomerName)
        Me.FraMain.Controls.Add(Me.Label1)
        Me.FraMain.Controls.Add(Me.lblQty)
        Me.FraMain.Controls.Add(Me.lblLock)
        Me.FraMain.Controls.Add(Me.lblInvoice)
        Me.FraMain.Controls.Add(Me.lblName)
        Me.FraMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, -6)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(569, 115)
        Me.FraMain.TabIndex = 6
        Me.FraMain.TabStop = False
        '
        'chkReset
        '
        Me.chkReset.BackColor = System.Drawing.SystemColors.Control
        Me.chkReset.Checked = True
        Me.chkReset.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkReset.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkReset.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReset.Location = New System.Drawing.Point(119, 59)
        Me.chkReset.Name = "chkReset"
        Me.chkReset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkReset.Size = New System.Drawing.Size(76, 16)
        Me.chkReset.TabIndex = 4
        Me.chkReset.Text = "ALL"
        Me.chkReset.UseVisualStyleBackColor = False
        '
        'Adata
        '
        Me.Adata.BackColor = System.Drawing.SystemColors.Window
        Me.Adata.CommandTimeout = 0
        Me.Adata.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.Adata.ConnectionString = Nothing
        Me.Adata.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.Adata.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Adata.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Adata.Location = New System.Drawing.Point(160, 288)
        Me.Adata.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.Adata.Name = "Adata"
        Me.Adata.Size = New System.Drawing.Size(80, 22)
        Me.Adata.TabIndex = 6
        Me.Adata.Text = "Adodc1"
        Me.Adata.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(63, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Reason :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblQty.Enabled = False
        Me.lblQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblQty.Location = New System.Drawing.Point(279, 14)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblQty.Size = New System.Drawing.Size(37, 13)
        Me.lblQty.TabIndex = 13
        Me.lblQty.Text = "Date :"
        Me.lblQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLock
        '
        Me.lblLock.AutoSize = True
        Me.lblLock.BackColor = System.Drawing.SystemColors.Control
        Me.lblLock.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLock.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLock.Location = New System.Drawing.Point(18, 58)
        Me.lblLock.Name = "lblLock"
        Me.lblLock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLock.Size = New System.Drawing.Size(92, 13)
        Me.lblLock.TabIndex = 11
        Me.lblLock.Text = "Original Printing:"
        Me.lblLock.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblInvoice
        '
        Me.lblInvoice.AutoSize = True
        Me.lblInvoice.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvoice.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInvoice.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvoice.Location = New System.Drawing.Point(46, 14)
        Me.lblInvoice.Name = "lblInvoice"
        Me.lblInvoice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvoice.Size = New System.Drawing.Size(64, 13)
        Me.lblInvoice.TabIndex = 10
        Me.lblInvoice.Text = "Invoice No:"
        Me.lblInvoice.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.SystemColors.Control
        Me.lblName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblName.Location = New System.Drawing.Point(19, 36)
        Me.lblName.Name = "lblName"
        Me.lblName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblName.Size = New System.Drawing.Size(95, 13)
        Me.lblName.TabIndex = 8
        Me.lblName.Text = "Customer Name :"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.CmdSave)
        Me.Frame1.Controls.Add(Me.cmdClose)
        Me.Frame1.Controls.Add(Me.lblBookType)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 104)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(569, 51)
        Me.Frame1.TabIndex = 9
        Me.Frame1.TabStop = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(498, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 7
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(218, 16)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(71, 13)
        Me.lblBookType.TabIndex = 12
        Me.lblBookType.Text = "lblBookType"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 10
        '
        'chkPackingReset
        '
        Me.chkPackingReset.BackColor = System.Drawing.SystemColors.Control
        Me.chkPackingReset.Checked = True
        Me.chkPackingReset.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPackingReset.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPackingReset.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPackingReset.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPackingReset.Location = New System.Drawing.Point(482, 59)
        Me.chkPackingReset.Name = "chkPackingReset"
        Me.chkPackingReset.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPackingReset.Size = New System.Drawing.Size(76, 16)
        Me.chkPackingReset.TabIndex = 15
        Me.chkPackingReset.Text = "ALL"
        Me.chkPackingReset.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(384, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(94, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Packing Printing :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmInvoiceReset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(570, 157)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Report1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInvoiceReset"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Invoice Printing Reset"
        Me.FraMain.ResumeLayout(False)
        Me.FraMain.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents Label2 As Label
    Public WithEvents chkPackingReset As CheckBox
#End Region
End Class