Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmItemLock
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
    Public WithEvents txtQty As System.Windows.Forms.TextBox
    Public WithEvents chkUpdate As System.Windows.Forms.CheckBox
    Public WithEvents txtItemCode As System.Windows.Forms.TextBox
    Public WithEvents Adata As VB6.ADODC
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents lblQty As System.Windows.Forms.Label
    Public WithEvents lblLock As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItemLock))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.chkUpdate = New System.Windows.Forms.CheckBox()
        Me.Adata = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.lblLock = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMain.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtQty
        '
        Me.txtQty.AcceptsReturn = True
        Me.txtQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtQty.Location = New System.Drawing.Point(119, 56)
        Me.txtQty.MaxLength = 0
        Me.txtQty.Name = "txtQty"
        Me.txtQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQty.Size = New System.Drawing.Size(79, 22)
        Me.txtQty.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtQty, "Press F1 For Help")
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemCode.Location = New System.Drawing.Point(119, 12)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(79, 22)
        Me.txtItemCode.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtItemCode, "Press F1 For Help")
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemName.Location = New System.Drawing.Point(119, 34)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(445, 22)
        Me.txtItemName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtItemName, "Press F1 For Help")
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
        Me.FraMain.Controls.Add(Me.txtQty)
        Me.FraMain.Controls.Add(Me.chkUpdate)
        Me.FraMain.Controls.Add(Me.txtItemCode)
        Me.FraMain.Controls.Add(Me.Adata)
        Me.FraMain.Controls.Add(Me.txtItemName)
        Me.FraMain.Controls.Add(Me.lblQty)
        Me.FraMain.Controls.Add(Me.lblLock)
        Me.FraMain.Controls.Add(Me.Label3)
        Me.FraMain.Controls.Add(Me.lblName)
        Me.FraMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, -6)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(569, 87)
        Me.FraMain.TabIndex = 2
        Me.FraMain.TabStop = False
        '
        'chkUpdate
        '
        Me.chkUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.chkUpdate.Checked = True
        Me.chkUpdate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkUpdate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkUpdate.Location = New System.Drawing.Point(517, 16)
        Me.chkUpdate.Name = "chkUpdate"
        Me.chkUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkUpdate.Size = New System.Drawing.Size(45, 16)
        Me.chkUpdate.TabIndex = 4
        Me.chkUpdate.Text = "ALL"
        Me.chkUpdate.UseVisualStyleBackColor = False
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
        Me.Adata.TabIndex = 12
        Me.Adata.Text = "Adodc1"
        Me.Adata.Visible = False
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblQty.Location = New System.Drawing.Point(20, 58)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblQty.Size = New System.Drawing.Size(88, 13)
        Me.lblQty.TabIndex = 12
        Me.lblQty.Text = "Stock Lock Qty :"
        Me.lblQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLock
        '
        Me.lblLock.AutoSize = True
        Me.lblLock.BackColor = System.Drawing.SystemColors.Control
        Me.lblLock.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLock.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLock.Location = New System.Drawing.Point(424, 14)
        Me.lblLock.Name = "lblLock"
        Me.lblLock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLock.Size = New System.Drawing.Size(79, 13)
        Me.lblLock.TabIndex = 9
        Me.lblLock.Text = "Lock / Unlock:"
        Me.lblLock.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(47, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Item Code :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.BackColor = System.Drawing.SystemColors.Control
        Me.lblName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblName.Location = New System.Drawing.Point(47, 36)
        Me.lblName.Name = "lblName"
        Me.lblName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblName.Size = New System.Drawing.Size(69, 13)
        Me.lblName.TabIndex = 6
        Me.lblName.Text = "Item Name :"
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
        Me.Frame1.Location = New System.Drawing.Point(0, 76)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(569, 51)
        Me.Frame1.TabIndex = 7
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
        Me.cmdClose.TabIndex = 5
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
        Me.lblBookType.TabIndex = 10
        Me.lblBookType.Text = "lblBookType"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 8
        '
        'frmItemLock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(570, 128)
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
        Me.Name = "frmItemLock"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Item Lock"
        Me.FraMain.ResumeLayout(False)
        Me.FraMain.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class