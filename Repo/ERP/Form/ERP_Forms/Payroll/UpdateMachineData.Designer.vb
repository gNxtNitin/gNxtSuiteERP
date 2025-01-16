Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmUpdateMachineData
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
    Public WithEvents txtDateTo As System.Windows.Forms.TextBox
    Public WithEvents txtDateFrom As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents chkLeave As System.Windows.Forms.CheckBox
    Public WithEvents chkOT As System.Windows.Forms.CheckBox
    Public WithEvents chkAttend As System.Windows.Forms.CheckBox
    Public WithEvents chkHoliday As System.Windows.Forms.CheckBox
    Public WithEvents chkDept As System.Windows.Forms.CheckBox
    Public WithEvents chkEmployee As System.Windows.Forms.CheckBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
    Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
    Public CommonDialog1Font As System.Windows.Forms.FontDialog
    Public CommonDialog1Color As System.Windows.Forms.ColorDialog
    Public CommonDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdOK As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents PBar As System.Windows.Forms.ProgressBar
    Public WithEvents lblStatus As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmUpdateMachineData))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.Frame6 = New System.Windows.Forms.GroupBox
        Me.txtDateTo = New System.Windows.Forms.TextBox
        Me.txtDateFrom = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.chkLeave = New System.Windows.Forms.CheckBox
        Me.chkOT = New System.Windows.Forms.CheckBox
        Me.chkAttend = New System.Windows.Forms.CheckBox
        Me.chkHoliday = New System.Windows.Forms.CheckBox
        Me.chkDept = New System.Windows.Forms.CheckBox
        Me.chkEmployee = New System.Windows.Forms.CheckBox
        Me.FraMovement = New System.Windows.Forms.GroupBox
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdOK = New System.Windows.Forms.Button
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.PBar = New System.Windows.Forms.ProgressBar
        Me.lblStatus = New System.Windows.Forms.Label
        Me.Frame6.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.PBar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Text = "Data Update From Savior machine"
        Me.ClientSize = New System.Drawing.Size(377, 241)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.Icon = CType(resources.GetObject("frmUpdateMachineData.Icon"), System.Drawing.Icon)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.ControlBox = True
        Me.Enabled = True
        Me.KeyPreview = False
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "frmUpdateMachineData"
        Me.Frame6.Text = "Preiod"
        Me.Frame6.Size = New System.Drawing.Size(377, 49)
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.TabIndex = 0
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Enabled = True
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Visible = True
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.Name = "Frame6"
        Me.txtDateTo.AutoSize = False
        Me.txtDateTo.Enabled = False
        Me.txtDateTo.ForeColor = System.Drawing.Color.FromARGB(0, 0, 192)
        Me.txtDateTo.Size = New System.Drawing.Size(89, 19)
        Me.txtDateTo.Location = New System.Drawing.Point(276, 20)
        Me.txtDateTo.TabIndex = 2
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.AcceptsReturn = True
        Me.txtDateTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtDateTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateTo.CausesValidation = True
        Me.txtDateTo.HideSelection = True
        Me.txtDateTo.ReadOnly = False
        Me.txtDateTo.Maxlength = 0
        Me.txtDateTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateTo.MultiLine = False
        Me.txtDateTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateTo.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDateTo.TabStop = True
        Me.txtDateTo.Visible = True
        Me.txtDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateFrom.AutoSize = False
        Me.txtDateFrom.Enabled = False
        Me.txtDateFrom.ForeColor = System.Drawing.Color.FromARGB(0, 0, 192)
        Me.txtDateFrom.Size = New System.Drawing.Size(89, 19)
        Me.txtDateFrom.Location = New System.Drawing.Point(50, 19)
        Me.txtDateFrom.TabIndex = 1
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.AcceptsReturn = True
        Me.txtDateFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtDateFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtDateFrom.CausesValidation = True
        Me.txtDateFrom.HideSelection = True
        Me.txtDateFrom.ReadOnly = False
        Me.txtDateFrom.Maxlength = 0
        Me.txtDateFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDateFrom.MultiLine = False
        Me.txtDateFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDateFrom.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtDateFrom.TabStop = True
        Me.txtDateFrom.Visible = True
        Me.txtDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.Label2.Text = "To :"
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Size = New System.Drawing.Size(24, 13)
        Me.Label2.Location = New System.Drawing.Point(238, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Enabled = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.UseMnemonic = True
        Me.Label2.Visible = True
        Me.Label2.AutoSize = True
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label2.Name = "Label2"
        Me.Label1.Text = "From :"
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.Location = New System.Drawing.Point(8, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Enabled = True
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.UseMnemonic = True
        Me.Label1.Visible = True
        Me.Label1.AutoSize = True
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Label1.Name = "Label1"
        Me.Frame1.Size = New System.Drawing.Size(377, 85)
        Me.Frame1.Location = New System.Drawing.Point(0, 44)
        Me.Frame1.TabIndex = 11
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Enabled = True
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Visible = True
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.Name = "Frame1"
        Me.chkLeave.Text = "Leave"
        Me.chkLeave.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLeave.Size = New System.Drawing.Size(123, 13)
        Me.chkLeave.Location = New System.Drawing.Point(200, 62)
        Me.chkLeave.TabIndex = 17
        Me.chkLeave.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkLeave.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkLeave.BackColor = System.Drawing.SystemColors.Control
        Me.chkLeave.CausesValidation = True
        Me.chkLeave.Enabled = True
        Me.chkLeave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLeave.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLeave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLeave.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkLeave.TabStop = True
        Me.chkLeave.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkLeave.Visible = True
        Me.chkLeave.Name = "chkLeave"
        Me.chkOT.Text = "Over Time"
        Me.chkOT.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOT.Size = New System.Drawing.Size(123, 13)
        Me.chkOT.Location = New System.Drawing.Point(200, 38)
        Me.chkOT.TabIndex = 16
        Me.chkOT.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkOT.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkOT.BackColor = System.Drawing.SystemColors.Control
        Me.chkOT.CausesValidation = True
        Me.chkOT.Enabled = True
        Me.chkOT.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOT.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOT.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOT.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkOT.TabStop = True
        Me.chkOT.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkOT.Visible = True
        Me.chkOT.Name = "chkOT"
        Me.chkAttend.Text = "Attendance"
        Me.chkAttend.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAttend.Size = New System.Drawing.Size(123, 13)
        Me.chkAttend.Location = New System.Drawing.Point(200, 16)
        Me.chkAttend.TabIndex = 15
        Me.chkAttend.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkAttend.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkAttend.BackColor = System.Drawing.SystemColors.Control
        Me.chkAttend.CausesValidation = True
        Me.chkAttend.Enabled = True
        Me.chkAttend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAttend.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAttend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAttend.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkAttend.TabStop = True
        Me.chkAttend.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkAttend.Visible = True
        Me.chkAttend.Name = "chkAttend"
        Me.chkHoliday.Text = "Holiday Master"
        Me.chkHoliday.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHoliday.Size = New System.Drawing.Size(155, 13)
        Me.chkHoliday.Location = New System.Drawing.Point(12, 62)
        Me.chkHoliday.TabIndex = 14
        Me.chkHoliday.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkHoliday.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkHoliday.BackColor = System.Drawing.SystemColors.Control
        Me.chkHoliday.CausesValidation = True
        Me.chkHoliday.Enabled = True
        Me.chkHoliday.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHoliday.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHoliday.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHoliday.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkHoliday.TabStop = True
        Me.chkHoliday.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkHoliday.Visible = True
        Me.chkHoliday.Name = "chkHoliday"
        Me.chkDept.Text = "Department Master"
        Me.chkDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDept.Size = New System.Drawing.Size(155, 13)
        Me.chkDept.Location = New System.Drawing.Point(12, 38)
        Me.chkDept.TabIndex = 13
        Me.chkDept.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkDept.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkDept.BackColor = System.Drawing.SystemColors.Control
        Me.chkDept.CausesValidation = True
        Me.chkDept.Enabled = True
        Me.chkDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDept.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkDept.TabStop = True
        Me.chkDept.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkDept.Visible = True
        Me.chkDept.Name = "chkDept"
        Me.chkEmployee.Text = "Employee Master"
        Me.chkEmployee.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmployee.Size = New System.Drawing.Size(155, 13)
        Me.chkEmployee.Location = New System.Drawing.Point(12, 16)
        Me.chkEmployee.TabIndex = 12
        Me.chkEmployee.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkEmployee.BackColor = System.Drawing.SystemColors.Control
        Me.chkEmployee.CausesValidation = True
        Me.chkEmployee.Enabled = True
        Me.chkEmployee.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEmployee.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkEmployee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkEmployee.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkEmployee.TabStop = True
        Me.chkEmployee.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkEmployee.Visible = True
        Me.chkEmployee.Name = "chkEmployee"
        Me.FraMovement.Size = New System.Drawing.Size(377, 49)
        Me.FraMovement.Location = New System.Drawing.Point(0, 124)
        Me.FraMovement.TabIndex = 8
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Enabled = True
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Visible = True
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.Name = "FraMovement"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.Size = New System.Drawing.Size(79, 34)
        Me.cmdClose.Location = New System.Drawing.Point(292, 11)
        Me.cmdClose.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.CausesValidation = True
        Me.cmdClose.Enabled = True
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.TabStop = True
        Me.cmdClose.Name = "cmdClose"
        Me.cmdOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdOK.Text = "&Received"
        Me.cmdOK.Size = New System.Drawing.Size(79, 34)
        Me.cmdOK.Location = New System.Drawing.Point(4, 11)
        Me.cmdOK.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmdOK, "Show Record")
        Me.cmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOK.CausesValidation = True
        Me.cmdOK.Enabled = True
        Me.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOK.TabStop = True
        Me.cmdOK.Name = "cmdOK"
        Me.Frame2.Size = New System.Drawing.Size(377, 73)
        Me.Frame2.Location = New System.Drawing.Point(0, 168)
        Me.Frame2.TabIndex = 5
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Enabled = True
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Visible = True
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.Name = "Frame2"
        'PBar.OcxState = CType(resources.GetObject("PBar.OcxState"), System.Windows.Forms.AxHost.State)
        Me.PBar.Size = New System.Drawing.Size(371, 15)
        Me.PBar.Location = New System.Drawing.Point(2, 12)
        Me.PBar.TabIndex = 6
        Me.PBar.Visible = False
        Me.PBar.Name = "PBar"
        Me.lblStatus.Size = New System.Drawing.Size(371, 43)
        Me.lblStatus.Location = New System.Drawing.Point(2, 28)
        Me.lblStatus.TabIndex = 7
        Me.lblStatus.Visible = False
        Me.lblStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblStatus.Enabled = True
        Me.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStatus.UseMnemonic = True
        Me.lblStatus.AutoSize = False
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatus.Name = "lblStatus"
        CType(Me.PBar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(Frame6)
        Me.Controls.Add(Frame1)
        Me.Controls.Add(FraMovement)
        Me.Controls.Add(Frame2)
        Me.Frame6.Controls.Add(txtDateTo)
        Me.Frame6.Controls.Add(txtDateFrom)
        Me.Frame6.Controls.Add(Label2)
        Me.Frame6.Controls.Add(Label1)
        Me.Frame1.Controls.Add(chkLeave)
        Me.Frame1.Controls.Add(chkOT)
        Me.Frame1.Controls.Add(chkAttend)
        Me.Frame1.Controls.Add(chkHoliday)
        Me.Frame1.Controls.Add(chkDept)
        Me.Frame1.Controls.Add(chkEmployee)
        Me.FraMovement.Controls.Add(cmdClose)
        Me.FraMovement.Controls.Add(cmdOK)
        Me.Frame2.Controls.Add(PBar)
        Me.Frame2.Controls.Add(lblStatus)
        Me.Frame6.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class