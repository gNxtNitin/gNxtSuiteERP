<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBSSchedule
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
	Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
	Public WithEvents fraDate As System.Windows.Forms.GroupBox
	Public WithEvents CboScheduleNo As System.Windows.Forms.ComboBox
	Public WithEvents cboHead As System.Windows.Forms.ComboBox
	Public WithEvents FraSchedule As System.Windows.Forms.GroupBox
	Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
	Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
	Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
	Public CommonDialog1Font As System.Windows.Forms.FontDialog
	Public CommonDialog1Color As System.Windows.Forms.ColorDialog
	Public CommonDialog1Print As System.Windows.Forms.PrintDialog
	Public WithEvents cmdCancel As System.Windows.Forms.Button
	Public WithEvents CmdPreview As System.Windows.Forms.Button
	Public WithEvents cmdPrint As System.Windows.Forms.Button
	Public WithEvents cmdRefresh As System.Windows.Forms.Button
	Public WithEvents FraMovement As System.Windows.Forms.GroupBox
	Public WithEvents Report1 As AxCrystal.AxCrystalReport
	Public WithEvents fraGrid As System.Windows.Forms.GroupBox
	Public WithEvents SprdCommand As AxFPSpreadADO.AxfpSpread
	Public WithEvents SprdPreview As AxFPSpreadADO.AxfpSpreadPreview
	Public WithEvents FraPreview As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBSSchedule))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.fraGrid = New System.Windows.Forms.GroupBox()
        Me.fraDate = New System.Windows.Forms.GroupBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.FraSchedule = New System.Windows.Forms.GroupBox()
        Me.CboScheduleNo = New System.Windows.Forms.ComboBox()
        Me.cboHead = New System.Windows.Forms.ComboBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.FraPreview = New System.Windows.Forms.GroupBox()
        Me.SprdCommand = New AxFPSpreadADO.AxfpSpread()
        Me.SprdPreview = New AxFPSpreadADO.AxfpSpreadPreview()
        Me.fraGrid.SuspendLayout()
        Me.fraDate.SuspendLayout()
        Me.FraSchedule.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraPreview.SuspendLayout()
        CType(Me.SprdCommand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.Location = New System.Drawing.Point(206, 11)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(67, 37)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "Ca&ncel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCancel, "Close the Form")
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(139, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 3
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(73, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Image = CType(resources.GetObject("cmdRefresh.Image"), System.Drawing.Image)
        Me.cmdRefresh.Location = New System.Drawing.Point(6, 11)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(67, 37)
        Me.cmdRefresh.TabIndex = 9
        Me.cmdRefresh.Text = "&Show"
        Me.cmdRefresh.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdRefresh, "Refresh  Record")
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'fraGrid
        '
        Me.fraGrid.BackColor = System.Drawing.SystemColors.Control
        Me.fraGrid.Controls.Add(Me.fraDate)
        Me.fraGrid.Controls.Add(Me.FraSchedule)
        Me.fraGrid.Controls.Add(Me.SprdView)
        Me.fraGrid.Controls.Add(Me.FraMovement)
        Me.fraGrid.Controls.Add(Me.Report1)
        Me.fraGrid.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraGrid.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraGrid.Location = New System.Drawing.Point(0, 0)
        Me.fraGrid.Name = "fraGrid"
        Me.fraGrid.Padding = New System.Windows.Forms.Padding(0)
        Me.fraGrid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraGrid.Size = New System.Drawing.Size(900, 608)
        Me.fraGrid.TabIndex = 1
        Me.fraGrid.TabStop = False
        '
        'fraDate
        '
        Me.fraDate.BackColor = System.Drawing.SystemColors.Control
        Me.fraDate.Controls.Add(Me.txtDateTo)
        Me.fraDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDate.Location = New System.Drawing.Point(0, 0)
        Me.fraDate.Name = "fraDate"
        Me.fraDate.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDate.Size = New System.Drawing.Size(115, 39)
        Me.fraDate.TabIndex = 13
        Me.fraDate.TabStop = False
        Me.fraDate.Text = "As On"
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(12, 14)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(88, 20)
        Me.txtDateTo.TabIndex = 14
        '
        'FraSchedule
        '
        Me.FraSchedule.BackColor = System.Drawing.SystemColors.Control
        Me.FraSchedule.Controls.Add(Me.CboScheduleNo)
        Me.FraSchedule.Controls.Add(Me.cboHead)
        Me.FraSchedule.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraSchedule.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraSchedule.Location = New System.Drawing.Point(116, 0)
        Me.FraSchedule.Name = "FraSchedule"
        Me.FraSchedule.Padding = New System.Windows.Forms.Padding(0)
        Me.FraSchedule.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraSchedule.Size = New System.Drawing.Size(355, 39)
        Me.FraSchedule.TabIndex = 10
        Me.FraSchedule.TabStop = False
        Me.FraSchedule.Text = "Schedule No."
        '
        'CboScheduleNo
        '
        Me.CboScheduleNo.BackColor = System.Drawing.SystemColors.Window
        Me.CboScheduleNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboScheduleNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboScheduleNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboScheduleNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboScheduleNo.Location = New System.Drawing.Point(6, 14)
        Me.CboScheduleNo.Name = "CboScheduleNo"
        Me.CboScheduleNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboScheduleNo.Size = New System.Drawing.Size(75, 22)
        Me.CboScheduleNo.TabIndex = 12
        '
        'cboHead
        '
        Me.cboHead.BackColor = System.Drawing.SystemColors.Window
        Me.cboHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboHead.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboHead.Location = New System.Drawing.Point(86, 14)
        Me.cboHead.Name = "cboHead"
        Me.cboHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboHead.Size = New System.Drawing.Size(265, 22)
        Me.cboHead.Sorted = True
        Me.cboHead.TabIndex = 11
        Me.cboHead.Visible = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(3, 45)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(896, 509)
        Me.SprdView.TabIndex = 0
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdCancel)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(618, 552)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(279, 53)
        Me.FraMovement.TabIndex = 2
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(124, 154)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 14
        '
        'FraPreview
        '
        Me.FraPreview.BackColor = System.Drawing.SystemColors.Control
        Me.FraPreview.Controls.Add(Me.SprdCommand)
        Me.FraPreview.Controls.Add(Me.SprdPreview)
        Me.FraPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPreview.Location = New System.Drawing.Point(0, 0)
        Me.FraPreview.Name = "FraPreview"
        Me.FraPreview.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPreview.Size = New System.Drawing.Size(749, 413)
        Me.FraPreview.TabIndex = 6
        Me.FraPreview.TabStop = False
        Me.FraPreview.Visible = False
        '
        'SprdCommand
        '
        Me.SprdCommand.DataSource = Nothing
        Me.SprdCommand.Location = New System.Drawing.Point(4, 10)
        Me.SprdCommand.Name = "SprdCommand"
        Me.SprdCommand.OcxState = CType(resources.GetObject("SprdCommand.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdCommand.Size = New System.Drawing.Size(581, 29)
        Me.SprdCommand.TabIndex = 7
        '
        'SprdPreview
        '
        Me.SprdPreview.Location = New System.Drawing.Point(4, 38)
        Me.SprdPreview.Name = "SprdPreview"
        Me.SprdPreview.OcxState = CType(resources.GetObject("SprdPreview.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPreview.Size = New System.Drawing.Size(739, 369)
        Me.SprdPreview.TabIndex = 8
        '
        'frmBSSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.fraGrid)
        Me.Controls.Add(Me.FraPreview)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmBSSchedule"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Schedule Forming Part Of Balance Sheet"
        Me.fraGrid.ResumeLayout(False)
        Me.fraDate.ResumeLayout(False)
        Me.fraDate.PerformLayout()
        Me.FraSchedule.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraPreview.ResumeLayout(False)
        CType(Me.SprdCommand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region 
End Class