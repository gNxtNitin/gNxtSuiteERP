Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAddFYNo
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
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents txtNewFYDateTo As System.Windows.Forms.TextBox
    Public WithEvents txtNewFYNo As System.Windows.Forms.TextBox
    Public WithEvents txtNewFYDateFrom As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents FraNew As System.Windows.Forms.GroupBox
    Public WithEvents TxtCompany As System.Windows.Forms.TextBox
    Public WithEvents txtCurrFYDateTo As System.Windows.Forms.TextBox
    Public WithEvents txtCurrFYNo As System.Windows.Forms.TextBox
    Public WithEvents txtCurrFYDateFrom As System.Windows.Forms.TextBox
    Public WithEvents lblCCode As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents FraCurrent As System.Windows.Forms.GroupBox
    Public WithEvents lblLabels As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddFYNo))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.FraNew = New System.Windows.Forms.GroupBox()
        Me.txtNewFYDateTo = New System.Windows.Forms.TextBox()
        Me.txtNewFYNo = New System.Windows.Forms.TextBox()
        Me.txtNewFYDateFrom = New System.Windows.Forms.TextBox()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me.FraCurrent = New System.Windows.Forms.GroupBox()
        Me.TxtCompany = New System.Windows.Forms.TextBox()
        Me.txtCurrFYDateTo = New System.Windows.Forms.TextBox()
        Me.txtCurrFYNo = New System.Windows.Forms.TextBox()
        Me.txtCurrFYDateFrom = New System.Windows.Forms.TextBox()
        Me.lblCCode = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.FraMovement.SuspendLayout
        Me.FraNew.SuspendLayout
        Me.FraCurrent.SuspendLayout
        CType(Me.lblLabels,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"),System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdClose.Location = New System.Drawing.Point(484, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 10
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
        Me.CmdSave.Location = New System.Drawing.Point(247, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 9
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdAdd.Location = New System.Drawing.Point(10, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = false
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 195)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(562, 53)
        Me.FraMovement.TabIndex = 21
        Me.FraMovement.TabStop = false
        '
        'FraNew
        '
        Me.FraNew.BackColor = System.Drawing.SystemColors.Control
        Me.FraNew.Controls.Add(Me.txtNewFYDateTo)
        Me.FraNew.Controls.Add(Me.txtNewFYNo)
        Me.FraNew.Controls.Add(Me.txtNewFYDateFrom)
        Me.FraNew.Controls.Add(Me._lblLabels_7)
        Me.FraNew.Controls.Add(Me._lblLabels_6)
        Me.FraNew.Controls.Add(Me._lblLabels_5)
        Me.FraNew.Enabled = false
        Me.FraNew.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FraNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraNew.Location = New System.Drawing.Point(0, 114)
        Me.FraNew.Name = "FraNew"
        Me.FraNew.Padding = New System.Windows.Forms.Padding(0)
        Me.FraNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraNew.Size = New System.Drawing.Size(564, 82)
        Me.FraNew.TabIndex = 16
        Me.FraNew.TabStop = false
        Me.FraNew.Text = "New"
        '
        'txtNewFYDateTo
        '
        Me.txtNewFYDateTo.AcceptsReturn = true
        Me.txtNewFYDateTo.BackColor = System.Drawing.Color.White
        Me.txtNewFYDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewFYDateTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNewFYDateTo.Enabled = false
        Me.txtNewFYDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtNewFYDateTo.ForeColor = System.Drawing.Color.Blue
        Me.txtNewFYDateTo.Location = New System.Drawing.Point(292, 48)
        Me.txtNewFYDateTo.MaxLength = 35
        Me.txtNewFYDateTo.Name = "txtNewFYDateTo"
        Me.txtNewFYDateTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNewFYDateTo.Size = New System.Drawing.Size(83, 22)
        Me.txtNewFYDateTo.TabIndex = 11
        '
        'txtNewFYNo
        '
        Me.txtNewFYNo.AcceptsReturn = true
        Me.txtNewFYNo.BackColor = System.Drawing.Color.White
        Me.txtNewFYNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewFYNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNewFYNo.Enabled = false
        Me.txtNewFYNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtNewFYNo.ForeColor = System.Drawing.Color.Blue
        Me.txtNewFYNo.Location = New System.Drawing.Point(102, 15)
        Me.txtNewFYNo.MaxLength = 35
        Me.txtNewFYNo.Name = "txtNewFYNo"
        Me.txtNewFYNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNewFYNo.Size = New System.Drawing.Size(82, 22)
        Me.txtNewFYNo.TabIndex = 6
        '
        'txtNewFYDateFrom
        '
        Me.txtNewFYDateFrom.AcceptsReturn = true
        Me.txtNewFYDateFrom.BackColor = System.Drawing.Color.White
        Me.txtNewFYDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNewFYDateFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNewFYDateFrom.Enabled = false
        Me.txtNewFYDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtNewFYDateFrom.ForeColor = System.Drawing.Color.Blue
        Me.txtNewFYDateFrom.Location = New System.Drawing.Point(102, 48)
        Me.txtNewFYDateFrom.MaxLength = 35
        Me.txtNewFYDateFrom.Name = "txtNewFYDateFrom"
        Me.txtNewFYDateFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNewFYDateFrom.Size = New System.Drawing.Size(83, 22)
        Me.txtNewFYDateFrom.TabIndex = 7
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.AutoSize = true
        Me._lblLabels_7.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_7, CType(7,Short))
        Me._lblLabels_7.Location = New System.Drawing.Point(235, 52)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(52, 13)
        Me._lblLabels_7.TabIndex = 19
        Me._lblLabels_7.Text = "Date To :"
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.AutoSize = true
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_6, CType(6,Short))
        Me._lblLabels_6.Location = New System.Drawing.Point(30, 53)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(67, 13)
        Me._lblLabels_6.TabIndex = 18
        Me._lblLabels_6.Text = "Date From :"
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.AutoSize = true
        Me._lblLabels_5.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_5, CType(5,Short))
        Me._lblLabels_5.Location = New System.Drawing.Point(17, 19)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(81, 13)
        Me._lblLabels_5.TabIndex = 17
        Me._lblLabels_5.Text = "Financial Year :"
        '
        'FraCurrent
        '
        Me.FraCurrent.BackColor = System.Drawing.SystemColors.Control
        Me.FraCurrent.Controls.Add(Me.TxtCompany)
        Me.FraCurrent.Controls.Add(Me.txtCurrFYDateTo)
        Me.FraCurrent.Controls.Add(Me.txtCurrFYNo)
        Me.FraCurrent.Controls.Add(Me.txtCurrFYDateFrom)
        Me.FraCurrent.Controls.Add(Me.lblCCode)
        Me.FraCurrent.Controls.Add(Me._lblLabels_4)
        Me.FraCurrent.Controls.Add(Me._lblLabels_3)
        Me.FraCurrent.Controls.Add(Me._lblLabels_1)
        Me.FraCurrent.Controls.Add(Me._lblLabels_2)
        Me.FraCurrent.Enabled = false
        Me.FraCurrent.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FraCurrent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128,Byte),Integer), CType(CType(0,Byte),Integer), CType(CType(0,Byte),Integer))
        Me.FraCurrent.Location = New System.Drawing.Point(-1, 0)
        Me.FraCurrent.Name = "FraCurrent"
        Me.FraCurrent.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCurrent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCurrent.Size = New System.Drawing.Size(565, 112)
        Me.FraCurrent.TabIndex = 1
        Me.FraCurrent.TabStop = false
        Me.FraCurrent.Text = "Current"
        '
        'TxtCompany
        '
        Me.TxtCompany.AcceptsReturn = true
        Me.TxtCompany.BackColor = System.Drawing.Color.White
        Me.TxtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCompany.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCompany.Enabled = false
        Me.TxtCompany.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TxtCompany.ForeColor = System.Drawing.Color.Blue
        Me.TxtCompany.Location = New System.Drawing.Point(102, 14)
        Me.TxtCompany.MaxLength = 35
        Me.TxtCompany.Name = "TxtCompany"
        Me.TxtCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCompany.Size = New System.Drawing.Size(456, 22)
        Me.TxtCompany.TabIndex = 2
        '
        'txtCurrFYDateTo
        '
        Me.txtCurrFYDateTo.AcceptsReturn = true
        Me.txtCurrFYDateTo.BackColor = System.Drawing.Color.White
        Me.txtCurrFYDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrFYDateTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurrFYDateTo.Enabled = false
        Me.txtCurrFYDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCurrFYDateTo.ForeColor = System.Drawing.Color.Blue
        Me.txtCurrFYDateTo.Location = New System.Drawing.Point(292, 77)
        Me.txtCurrFYDateTo.MaxLength = 35
        Me.txtCurrFYDateTo.Name = "txtCurrFYDateTo"
        Me.txtCurrFYDateTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurrFYDateTo.Size = New System.Drawing.Size(83, 22)
        Me.txtCurrFYDateTo.TabIndex = 5
        '
        'txtCurrFYNo
        '
        Me.txtCurrFYNo.AcceptsReturn = true
        Me.txtCurrFYNo.BackColor = System.Drawing.Color.White
        Me.txtCurrFYNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrFYNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurrFYNo.Enabled = false
        Me.txtCurrFYNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCurrFYNo.ForeColor = System.Drawing.Color.Blue
        Me.txtCurrFYNo.Location = New System.Drawing.Point(102, 45)
        Me.txtCurrFYNo.MaxLength = 35
        Me.txtCurrFYNo.Name = "txtCurrFYNo"
        Me.txtCurrFYNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurrFYNo.Size = New System.Drawing.Size(84, 22)
        Me.txtCurrFYNo.TabIndex = 3
        '
        'txtCurrFYDateFrom
        '
        Me.txtCurrFYDateFrom.AcceptsReturn = true
        Me.txtCurrFYDateFrom.BackColor = System.Drawing.Color.White
        Me.txtCurrFYDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCurrFYDateFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCurrFYDateFrom.Enabled = false
        Me.txtCurrFYDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.txtCurrFYDateFrom.ForeColor = System.Drawing.Color.Blue
        Me.txtCurrFYDateFrom.Location = New System.Drawing.Point(102, 77)
        Me.txtCurrFYDateFrom.MaxLength = 35
        Me.txtCurrFYDateFrom.Name = "txtCurrFYDateFrom"
        Me.txtCurrFYDateFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCurrFYDateFrom.Size = New System.Drawing.Size(83, 22)
        Me.txtCurrFYDateFrom.TabIndex = 4
        '
        'lblCCode
        '
        Me.lblCCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblCCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.lblCCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCCode.Location = New System.Drawing.Point(166, 40)
        Me.lblCCode.Name = "lblCCode"
        Me.lblCCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCCode.Size = New System.Drawing.Size(43, 13)
        Me.lblCCode.TabIndex = 20
        Me.lblCCode.Text = "lblCCode"
        Me.lblCCode.Visible = false
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.AutoSize = true
        Me._lblLabels_4.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_4, CType(4,Short))
        Me._lblLabels_4.Location = New System.Drawing.Point(237, 81)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(52, 13)
        Me._lblLabels_4.TabIndex = 15
        Me._lblLabels_4.Text = "Date To :"
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = true
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_3, CType(3,Short))
        Me._lblLabels_3.Location = New System.Drawing.Point(32, 81)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(67, 13)
        Me._lblLabels_3.TabIndex = 14
        Me._lblLabels_3.Text = "Date From :"
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = true
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1,Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(18, 49)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(81, 13)
        Me._lblLabels_1.TabIndex = 13
        Me._lblLabels_1.Text = "Financial Year :"
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = true
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2,Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(37, 17)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(62, 13)
        Me._lblLabels_2.TabIndex = 12
        Me._lblLabels_2.Text = "Company :"
        '
        'frmAddFYNo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(566, 250)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.FraNew)
        Me.Controls.Add(Me.FraCurrent)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.KeyPreview = true
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmAddFYNo"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Financial Year"
        Me.FraMovement.ResumeLayout(false)
        Me.FraNew.ResumeLayout(false)
        Me.FraNew.PerformLayout
        Me.FraCurrent.ResumeLayout(false)
        Me.FraCurrent.PerformLayout
        CType(Me.lblLabels,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
#End Region
End Class