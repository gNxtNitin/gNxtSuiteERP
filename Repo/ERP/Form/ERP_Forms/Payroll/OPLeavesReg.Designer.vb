Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmOPLeavesReg
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
        
        
        VB6_AddADODataBinding()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            VB6_RemoveADODataBinding()
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents optCardNo As System.Windows.Forms.RadioButton
    Public WithEvents OptName As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOPLeavesReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.cboCategory = New System.Windows.Forms.ComboBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.optCardNo = New System.Windows.Forms.RadioButton()
        Me.OptName = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdRefresh = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Location = New System.Drawing.Point(84, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(79, 34)
        Me.CmdPreview.TabIndex = 11
        Me.CmdPreview.Text = "Pre&view"
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Location = New System.Drawing.Point(664, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(80, 34)
        Me.CmdClose.TabIndex = 2
        Me.CmdClose.Text = "&Close"
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cboCategory)
        Me.Frame6.Controls.Add(Me.chkCategory)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(508, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(241, 39)
        Me.Frame6.TabIndex = 12
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Category"
        '
        'cboCategory
        '
        Me.cboCategory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCategory.Location = New System.Drawing.Point(4, 14)
        Me.cboCategory.Name = "cboCategory"
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Size = New System.Drawing.Size(183, 22)
        Me.cboCategory.TabIndex = 14
        '
        'chkCategory
        '
        Me.chkCategory.AutoSize = True
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(190, 18)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(46, 18)
        Me.chkCategory.TabIndex = 13
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkAll)
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(294, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(213, 39)
        Me.Frame4.TabIndex = 5
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Department"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(160, 18)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 9
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(2, 14)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(153, 22)
        Me.cboDept.TabIndex = 8
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.optCardNo)
        Me.Frame3.Controls.Add(Me.OptName)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(155, 39)
        Me.Frame3.TabIndex = 4
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Order By"
        '
        'optCardNo
        '
        Me.optCardNo.AutoSize = True
        Me.optCardNo.BackColor = System.Drawing.SystemColors.Control
        Me.optCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCardNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCardNo.Location = New System.Drawing.Point(70, 18)
        Me.optCardNo.Name = "optCardNo"
        Me.optCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCardNo.Size = New System.Drawing.Size(64, 18)
        Me.optCardNo.TabIndex = 7
        Me.optCardNo.TabStop = True
        Me.optCardNo.Text = "Card No"
        Me.optCardNo.UseVisualStyleBackColor = False
        '
        'OptName
        '
        Me.OptName.AutoSize = True
        Me.OptName.BackColor = System.Drawing.SystemColors.Control
        Me.OptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptName.Location = New System.Drawing.Point(6, 18)
        Me.OptName.Name = "OptName"
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Size = New System.Drawing.Size(52, 18)
        Me.OptName.TabIndex = 6
        Me.OptName.TabStop = True
        Me.OptName.Text = "Name"
        Me.OptName.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.SprdView)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 34)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(749, 375)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(743, 363)
        Me.SprdView.TabIndex = 15
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdRefresh)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 404)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.TabIndex = 1
        Me.FraMovement.TabStop = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Location = New System.Drawing.Point(4, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(80, 34)
        Me.cmdPrint.TabIndex = 10
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdRefresh
        '
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Location = New System.Drawing.Point(584, 12)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 34)
        Me.cmdRefresh.TabIndex = 3
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(190, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 12
        '
        'frmOPLeavesReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmOPLeavesReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Opening Leaves Register"
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class