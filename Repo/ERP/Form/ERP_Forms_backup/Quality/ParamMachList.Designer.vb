Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamMachList
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
        'Me.MdiParent = Quality.Master

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
    Public WithEvents _optIPT_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optIPT_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optIPT_0 As System.Windows.Forms.RadioButton
    Public WithEvents fraIPT As System.Windows.Forms.GroupBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtMachNo As System.Windows.Forms.TextBox
    Public WithEvents lblMachNo As System.Windows.Forms.Label
    Public WithEvents FraMac As System.Windows.Forms.GroupBox
    Public WithEvents _optKey_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optKey_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optKey_2 As System.Windows.Forms.RadioButton
    Public WithEvents FraKey As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents lblRepType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents optIPT As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optKey As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamMachList))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtMachNo = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.fraIPT = New System.Windows.Forms.GroupBox()
        Me._optIPT_2 = New System.Windows.Forms.RadioButton()
        Me._optIPT_1 = New System.Windows.Forms.RadioButton()
        Me._optIPT_0 = New System.Windows.Forms.RadioButton()
        Me.FraMac = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.lblMachNo = New System.Windows.Forms.Label()
        Me.FraKey = New System.Windows.Forms.GroupBox()
        Me._optKey_0 = New System.Windows.Forms.RadioButton()
        Me._optKey_1 = New System.Windows.Forms.RadioButton()
        Me._optKey_2 = New System.Windows.Forms.RadioButton()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblRepType = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.optIPT = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optKey = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.fraIPT.SuspendLayout()
        Me.FraMac.SuspendLayout()
        Me.FraKey.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optIPT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optKey, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Enabled = False
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(92, 16)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 1
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'TxtMachNo
        '
        Me.TxtMachNo.AcceptsReturn = True
        Me.TxtMachNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtMachNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtMachNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtMachNo.Enabled = False
        Me.TxtMachNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMachNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtMachNo.Location = New System.Drawing.Point(6, 16)
        Me.TxtMachNo.MaxLength = 0
        Me.TxtMachNo.Name = "TxtMachNo"
        Me.TxtMachNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtMachNo.Size = New System.Drawing.Size(85, 19)
        Me.TxtMachNo.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.TxtMachNo, "Press F1 For Help")
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(53, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
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
        Me.cmdPrint.Location = New System.Drawing.Point(147, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(236, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 5
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'fraIPT
        '
        Me.fraIPT.BackColor = System.Drawing.SystemColors.Control
        Me.fraIPT.Controls.Add(Me._optIPT_2)
        Me.fraIPT.Controls.Add(Me._optIPT_1)
        Me.fraIPT.Controls.Add(Me._optIPT_0)
        Me.fraIPT.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraIPT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraIPT.Location = New System.Drawing.Point(0, 2)
        Me.fraIPT.Name = "fraIPT"
        Me.fraIPT.Padding = New System.Windows.Forms.Padding(0)
        Me.fraIPT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraIPT.Size = New System.Drawing.Size(361, 59)
        Me.fraIPT.TabIndex = 14
        Me.fraIPT.TabStop = False
        Me.fraIPT.Text = "Initial Part Tag"
        '
        '_optIPT_2
        '
        Me._optIPT_2.AutoSize = True
        Me._optIPT_2.BackColor = System.Drawing.SystemColors.Control
        Me._optIPT_2.Checked = True
        Me._optIPT_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optIPT_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optIPT_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optIPT.SetIndex(Me._optIPT_2, CType(2, Short))
        Me._optIPT_2.Location = New System.Drawing.Point(256, 26)
        Me._optIPT_2.Name = "_optIPT_2"
        Me._optIPT_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optIPT_2.Size = New System.Drawing.Size(70, 18)
        Me._optIPT_2.TabIndex = 17
        Me._optIPT_2.TabStop = True
        Me._optIPT_2.Text = "Pending"
        Me._optIPT_2.UseVisualStyleBackColor = False
        '
        '_optIPT_1
        '
        Me._optIPT_1.AutoSize = True
        Me._optIPT_1.BackColor = System.Drawing.SystemColors.Control
        Me._optIPT_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optIPT_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optIPT_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optIPT.SetIndex(Me._optIPT_1, CType(1, Short))
        Me._optIPT_1.Location = New System.Drawing.Point(132, 26)
        Me._optIPT_1.Name = "_optIPT_1"
        Me._optIPT_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optIPT_1.Size = New System.Drawing.Size(86, 18)
        Me._optIPT_1.TabIndex = 16
        Me._optIPT_1.TabStop = True
        Me._optIPT_1.Text = "Completed"
        Me._optIPT_1.UseVisualStyleBackColor = False
        '
        '_optIPT_0
        '
        Me._optIPT_0.AutoSize = True
        Me._optIPT_0.BackColor = System.Drawing.SystemColors.Control
        Me._optIPT_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optIPT_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optIPT_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optIPT.SetIndex(Me._optIPT_0, CType(0, Short))
        Me._optIPT_0.Location = New System.Drawing.Point(26, 26)
        Me._optIPT_0.Name = "_optIPT_0"
        Me._optIPT_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optIPT_0.Size = New System.Drawing.Size(50, 18)
        Me._optIPT_0.TabIndex = 15
        Me._optIPT_0.TabStop = True
        Me._optIPT_0.Text = "Both"
        Me._optIPT_0.UseVisualStyleBackColor = False
        '
        'FraMac
        '
        Me.FraMac.BackColor = System.Drawing.SystemColors.Control
        Me.FraMac.Controls.Add(Me.chkAll)
        Me.FraMac.Controls.Add(Me.cmdsearch)
        Me.FraMac.Controls.Add(Me.TxtMachNo)
        Me.FraMac.Controls.Add(Me.lblMachNo)
        Me.FraMac.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMac.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMac.Location = New System.Drawing.Point(0, 2)
        Me.FraMac.Name = "FraMac"
        Me.FraMac.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMac.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMac.Size = New System.Drawing.Size(361, 59)
        Me.FraMac.TabIndex = 12
        Me.FraMac.TabStop = False
        Me.FraMac.Text = "Machine "
        '
        'chkAll
        '
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(122, 20)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(49, 13)
        Me.chkAll.TabIndex = 2
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'lblMachNo
        '
        Me.lblMachNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblMachNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMachNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMachNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMachNo.Location = New System.Drawing.Point(6, 36)
        Me.lblMachNo.Name = "lblMachNo"
        Me.lblMachNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMachNo.Size = New System.Drawing.Size(351, 19)
        Me.lblMachNo.TabIndex = 13
        '
        'FraKey
        '
        Me.FraKey.BackColor = System.Drawing.SystemColors.Control
        Me.FraKey.Controls.Add(Me._optKey_0)
        Me.FraKey.Controls.Add(Me._optKey_1)
        Me.FraKey.Controls.Add(Me._optKey_2)
        Me.FraKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraKey.Location = New System.Drawing.Point(0, 2)
        Me.FraKey.Name = "FraKey"
        Me.FraKey.Padding = New System.Windows.Forms.Padding(0)
        Me.FraKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraKey.Size = New System.Drawing.Size(361, 59)
        Me.FraKey.TabIndex = 6
        Me.FraKey.TabStop = False
        Me.FraKey.Text = "Key Machine"
        '
        '_optKey_0
        '
        Me._optKey_0.BackColor = System.Drawing.SystemColors.Control
        Me._optKey_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optKey_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optKey_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optKey.SetIndex(Me._optKey_0, CType(0, Short))
        Me._optKey_0.Location = New System.Drawing.Point(88, 26)
        Me._optKey_0.Name = "_optKey_0"
        Me._optKey_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optKey_0.Size = New System.Drawing.Size(75, 13)
        Me._optKey_0.TabIndex = 10
        Me._optKey_0.TabStop = True
        Me._optKey_0.Text = "Yes"
        Me._optKey_0.UseVisualStyleBackColor = False
        '
        '_optKey_1
        '
        Me._optKey_1.BackColor = System.Drawing.SystemColors.Control
        Me._optKey_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optKey_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optKey_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optKey.SetIndex(Me._optKey_1, CType(1, Short))
        Me._optKey_1.Location = New System.Drawing.Point(184, 26)
        Me._optKey_1.Name = "_optKey_1"
        Me._optKey_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optKey_1.Size = New System.Drawing.Size(75, 13)
        Me._optKey_1.TabIndex = 9
        Me._optKey_1.TabStop = True
        Me._optKey_1.Text = "No"
        Me._optKey_1.UseVisualStyleBackColor = False
        '
        '_optKey_2
        '
        Me._optKey_2.BackColor = System.Drawing.SystemColors.Control
        Me._optKey_2.Checked = True
        Me._optKey_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optKey_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optKey_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optKey.SetIndex(Me._optKey_2, CType(2, Short))
        Me._optKey_2.Location = New System.Drawing.Point(274, 26)
        Me._optKey_2.Name = "_optKey_2"
        Me._optKey_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optKey_2.Size = New System.Drawing.Size(75, 13)
        Me._optKey_2.TabIndex = 8
        Me._optKey_2.TabStop = True
        Me._optKey_2.Text = "Both"
        Me._optKey_2.UseVisualStyleBackColor = False
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.lblRepType)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 56)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(361, 53)
        Me.FraMovement.TabIndex = 7
        Me.FraMovement.TabStop = False
        '
        'lblRepType
        '
        Me.lblRepType.BackColor = System.Drawing.SystemColors.Control
        Me.lblRepType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRepType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRepType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRepType.Location = New System.Drawing.Point(12, 18)
        Me.lblRepType.Name = "lblRepType"
        Me.lblRepType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRepType.Size = New System.Drawing.Size(31, 23)
        Me.lblRepType.TabIndex = 11
        Me.lblRepType.Text = "lblRepType"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 54)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 15
        '
        'frmParamMachList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(361, 110)
        Me.Controls.Add(Me.fraIPT)
        Me.Controls.Add(Me.FraMac)
        Me.Controls.Add(Me.FraKey)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Report1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamMachList"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "List of Machines"
        Me.fraIPT.ResumeLayout(False)
        Me.fraIPT.PerformLayout()
        Me.FraMac.ResumeLayout(False)
        Me.FraKey.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optIPT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optKey, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class