Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmDivisionRights
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
    Public WithEvents txtUserId As System.Windows.Forms.TextBox
    Public WithEvents LblUserName As System.Windows.Forms.Label
    Public WithEvents LblUserID As System.Windows.Forms.Label
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents _OptRights_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptRights_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraDetail As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblCompanyName As System.Windows.Forms.Label
    Public WithEvents OptRights As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDivisionRights))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.LblUserName = New System.Windows.Forms.Label()
        Me.LblUserID = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptRights_1 = New System.Windows.Forms.RadioButton()
        Me._OptRights_0 = New System.Windows.Forms.RadioButton()
        Me.FraDetail = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblCompanyName = New System.Windows.Forms.Label()
        Me.OptRights = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraMain.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraDetail.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.OptRights, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdClose.Location = New System.Drawing.Point(459, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(70, 38)
        Me.CmdClose.TabIndex = 1
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdSave.Location = New System.Drawing.Point(4, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(70, 38)
        Me.CmdSave.TabIndex = 0
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.txtUserId)
        Me.FraMain.Controls.Add(Me.LblUserName)
        Me.FraMain.Controls.Add(Me.LblUserID)
        Me.FraMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, 38)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(426, 64)
        Me.FraMain.TabIndex = 2
        Me.FraMain.TabStop = False
        '
        'txtUserId
        '
        Me.txtUserId.AcceptsReturn = True
        Me.txtUserId.BackColor = System.Drawing.SystemColors.Window
        Me.txtUserId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserId.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUserId.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserId.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserId.Location = New System.Drawing.Point(62, 10)
        Me.txtUserId.MaxLength = 0
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUserId.Size = New System.Drawing.Size(360, 22)
        Me.txtUserId.TabIndex = 11
        '
        'LblUserName
        '
        Me.LblUserName.BackColor = System.Drawing.SystemColors.Control
        Me.LblUserName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblUserName.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblUserName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUserName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblUserName.Location = New System.Drawing.Point(62, 37)
        Me.LblUserName.Name = "LblUserName"
        Me.LblUserName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblUserName.Size = New System.Drawing.Size(360, 17)
        Me.LblUserName.TabIndex = 6
        '
        'LblUserID
        '
        Me.LblUserID.AutoSize = True
        Me.LblUserID.BackColor = System.Drawing.SystemColors.Control
        Me.LblUserID.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblUserID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUserID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblUserID.Location = New System.Drawing.Point(8, 12)
        Me.LblUserID.Name = "LblUserID"
        Me.LblUserID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblUserID.Size = New System.Drawing.Size(47, 13)
        Me.LblUserID.TabIndex = 5
        Me.LblUserID.Text = "UserID :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._OptRights_1)
        Me.Frame1.Controls.Add(Me._OptRights_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(428, 38)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(100, 64)
        Me.Frame1.TabIndex = 7
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Assign Rights"
        '
        '_OptRights_1
        '
        Me._OptRights_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptRights_1.Checked = True
        Me._OptRights_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRights_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptRights_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptRights.SetIndex(Me._OptRights_1, CType(1, Short))
        Me._OptRights_1.Location = New System.Drawing.Point(14, 38)
        Me._OptRights_1.Name = "_OptRights_1"
        Me._OptRights_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRights_1.Size = New System.Drawing.Size(85, 17)
        Me._OptRights_1.TabIndex = 9
        Me._OptRights_1.TabStop = True
        Me._OptRights_1.Text = "None"
        Me._OptRights_1.UseVisualStyleBackColor = False
        '
        '_OptRights_0
        '
        Me._OptRights_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptRights_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptRights_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptRights_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptRights.SetIndex(Me._OptRights_0, CType(0, Short))
        Me._OptRights_0.Location = New System.Drawing.Point(14, 18)
        Me._OptRights_0.Name = "_OptRights_0"
        Me._OptRights_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptRights_0.Size = New System.Drawing.Size(75, 17)
        Me._OptRights_0.TabIndex = 8
        Me._OptRights_0.TabStop = True
        Me._OptRights_0.Text = "All"
        Me._OptRights_0.UseVisualStyleBackColor = False
        '
        'FraDetail
        '
        Me.FraDetail.BackColor = System.Drawing.SystemColors.Control
        Me.FraDetail.Controls.Add(Me.SprdMain)
        Me.FraDetail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraDetail.Location = New System.Drawing.Point(0, 98)
        Me.FraDetail.Name = "FraDetail"
        Me.FraDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.FraDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraDetail.Size = New System.Drawing.Size(530, 324)
        Me.FraDetail.TabIndex = 3
        Me.FraDetail.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 10)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(528, 310)
        Me.SprdMain.TabIndex = 13
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 417)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(532, 53)
        Me.FraMovement.TabIndex = 4
        Me.FraMovement.TabStop = False
        '
        'lblCompanyName
        '
        Me.lblCompanyName.BackColor = System.Drawing.SystemColors.Control
        Me.lblCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCompanyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompanyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompanyName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompanyName.Location = New System.Drawing.Point(0, 0)
        Me.lblCompanyName.Name = "lblCompanyName"
        Me.lblCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompanyName.Size = New System.Drawing.Size(530, 38)
        Me.lblCompanyName.TabIndex = 12
        Me.lblCompanyName.Text = "lblCompanyName"
        Me.lblCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'OptRights
        '
        '
        'frmDivisionRights
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(532, 471)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraDetail)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.lblCompanyName)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDivisionRights"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Security -Department Control"
        Me.FraMain.ResumeLayout(False)
        Me.FraMain.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.FraDetail.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.OptRights, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class