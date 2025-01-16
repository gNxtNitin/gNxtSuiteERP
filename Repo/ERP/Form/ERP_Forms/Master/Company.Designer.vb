Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmCompany
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
    Public WithEvents txtRegdAdd2 As System.Windows.Forms.TextBox
    Public WithEvents txtRegdFax As System.Windows.Forms.TextBox
    Public WithEvents txtRegdPhone As System.Windows.Forms.TextBox
    Public WithEvents txtRegdState As System.Windows.Forms.TextBox
    Public WithEvents txtRegdCity As System.Windows.Forms.TextBox
    Public WithEvents txtRegdEmail As System.Windows.Forms.TextBox
    Public WithEvents txtRegdAdd1 As System.Windows.Forms.TextBox
    Public WithEvents txtRegdPin As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_13 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_16 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_15 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_14 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_9 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_8 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents FraRegdInfo As System.Windows.Forms.GroupBox
    Public WithEvents txtPin As System.Windows.Forms.TextBox
    Public WithEvents txtCompanyShortName As System.Windows.Forms.TextBox
    Public WithEvents txtaddress As System.Windows.Forms.TextBox
    Public WithEvents txtEmail As System.Windows.Forms.TextBox
    Public WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Public WithEvents txtCity As System.Windows.Forms.TextBox
    Public WithEvents txtState As System.Windows.Forms.TextBox
    Public WithEvents txtPhone As System.Windows.Forms.TextBox
    Public WithEvents txtFax As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_12 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_10 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents FraCompany As System.Windows.Forms.GroupBox
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdcancel As System.Windows.Forms.Button
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents lblLabels As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCompany))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.FraRegdInfo = New System.Windows.Forms.GroupBox()
        Me.txtRegdAdd2 = New System.Windows.Forms.TextBox()
        Me.txtRegdFax = New System.Windows.Forms.TextBox()
        Me.txtRegdPhone = New System.Windows.Forms.TextBox()
        Me.txtRegdState = New System.Windows.Forms.TextBox()
        Me.txtRegdCity = New System.Windows.Forms.TextBox()
        Me.txtRegdEmail = New System.Windows.Forms.TextBox()
        Me.txtRegdAdd1 = New System.Windows.Forms.TextBox()
        Me.txtRegdPin = New System.Windows.Forms.TextBox()
        Me._lblLabels_13 = New System.Windows.Forms.Label()
        Me._lblLabels_16 = New System.Windows.Forms.Label()
        Me._lblLabels_15 = New System.Windows.Forms.Label()
        Me._lblLabels_14 = New System.Windows.Forms.Label()
        Me._lblLabels_9 = New System.Windows.Forms.Label()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.FraCompany = New System.Windows.Forms.GroupBox()
        Me.txtPin = New System.Windows.Forms.TextBox()
        Me.txtCompanyShortName = New System.Windows.Forms.TextBox()
        Me.txtaddress = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me._lblLabels_12 = New System.Windows.Forms.Label()
        Me._lblLabels_10 = New System.Windows.Forms.Label()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtBranchName = New System.Windows.Forms.TextBox()
        Me.txtAccountNo = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtIFSCCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.FraRegdInfo.SuspendLayout()
        Me.FraCompany.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(4, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(80, 40)
        Me.cmdSave.TabIndex = 18
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdcancel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdcancel.Image = CType(resources.GetObject("cmdcancel.Image"), System.Drawing.Image)
        Me.cmdcancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdcancel.Location = New System.Drawing.Point(479, 10)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdcancel.Size = New System.Drawing.Size(80, 40)
        Me.cmdcancel.TabIndex = 20
        Me.cmdcancel.Text = "&Close"
        Me.cmdcancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdcancel, "Cancel & Close Form")
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'FraRegdInfo
        '
        Me.FraRegdInfo.BackColor = System.Drawing.SystemColors.Control
        Me.FraRegdInfo.Controls.Add(Me.txtRegdAdd2)
        Me.FraRegdInfo.Controls.Add(Me.txtRegdFax)
        Me.FraRegdInfo.Controls.Add(Me.txtRegdPhone)
        Me.FraRegdInfo.Controls.Add(Me.txtRegdState)
        Me.FraRegdInfo.Controls.Add(Me.txtRegdCity)
        Me.FraRegdInfo.Controls.Add(Me.txtRegdEmail)
        Me.FraRegdInfo.Controls.Add(Me.txtRegdAdd1)
        Me.FraRegdInfo.Controls.Add(Me.txtRegdPin)
        Me.FraRegdInfo.Controls.Add(Me._lblLabels_13)
        Me.FraRegdInfo.Controls.Add(Me._lblLabels_16)
        Me.FraRegdInfo.Controls.Add(Me._lblLabels_15)
        Me.FraRegdInfo.Controls.Add(Me._lblLabels_14)
        Me.FraRegdInfo.Controls.Add(Me._lblLabels_9)
        Me.FraRegdInfo.Controls.Add(Me._lblLabels_8)
        Me.FraRegdInfo.Controls.Add(Me._lblLabels_7)
        Me.FraRegdInfo.Controls.Add(Me._lblLabels_3)
        Me.FraRegdInfo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraRegdInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FraRegdInfo.Location = New System.Drawing.Point(0, 216)
        Me.FraRegdInfo.Name = "FraRegdInfo"
        Me.FraRegdInfo.Padding = New System.Windows.Forms.Padding(0)
        Me.FraRegdInfo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraRegdInfo.Size = New System.Drawing.Size(564, 188)
        Me.FraRegdInfo.TabIndex = 32
        Me.FraRegdInfo.TabStop = False
        Me.FraRegdInfo.Text = "Registered Address"
        '
        'txtRegdAdd2
        '
        Me.txtRegdAdd2.AcceptsReturn = True
        Me.txtRegdAdd2.BackColor = System.Drawing.Color.White
        Me.txtRegdAdd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegdAdd2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegdAdd2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegdAdd2.ForeColor = System.Drawing.Color.Blue
        Me.txtRegdAdd2.Location = New System.Drawing.Point(100, 44)
        Me.txtRegdAdd2.MaxLength = 35
        Me.txtRegdAdd2.Name = "txtRegdAdd2"
        Me.txtRegdAdd2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegdAdd2.Size = New System.Drawing.Size(458, 22)
        Me.txtRegdAdd2.TabIndex = 11
        '
        'txtRegdFax
        '
        Me.txtRegdFax.AcceptsReturn = True
        Me.txtRegdFax.BackColor = System.Drawing.Color.White
        Me.txtRegdFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegdFax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegdFax.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegdFax.ForeColor = System.Drawing.Color.Blue
        Me.txtRegdFax.Location = New System.Drawing.Point(410, 128)
        Me.txtRegdFax.MaxLength = 15
        Me.txtRegdFax.Name = "txtRegdFax"
        Me.txtRegdFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegdFax.Size = New System.Drawing.Size(148, 22)
        Me.txtRegdFax.TabIndex = 16
        '
        'txtRegdPhone
        '
        Me.txtRegdPhone.AcceptsReturn = True
        Me.txtRegdPhone.BackColor = System.Drawing.Color.White
        Me.txtRegdPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegdPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegdPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegdPhone.ForeColor = System.Drawing.Color.Blue
        Me.txtRegdPhone.Location = New System.Drawing.Point(100, 128)
        Me.txtRegdPhone.MaxLength = 15
        Me.txtRegdPhone.Name = "txtRegdPhone"
        Me.txtRegdPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegdPhone.Size = New System.Drawing.Size(146, 22)
        Me.txtRegdPhone.TabIndex = 15
        '
        'txtRegdState
        '
        Me.txtRegdState.AcceptsReturn = True
        Me.txtRegdState.BackColor = System.Drawing.Color.White
        Me.txtRegdState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegdState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegdState.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegdState.ForeColor = System.Drawing.Color.Blue
        Me.txtRegdState.Location = New System.Drawing.Point(100, 100)
        Me.txtRegdState.MaxLength = 35
        Me.txtRegdState.Name = "txtRegdState"
        Me.txtRegdState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegdState.Size = New System.Drawing.Size(146, 22)
        Me.txtRegdState.TabIndex = 13
        '
        'txtRegdCity
        '
        Me.txtRegdCity.AcceptsReturn = True
        Me.txtRegdCity.BackColor = System.Drawing.Color.White
        Me.txtRegdCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegdCity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegdCity.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegdCity.ForeColor = System.Drawing.Color.Blue
        Me.txtRegdCity.Location = New System.Drawing.Point(100, 72)
        Me.txtRegdCity.MaxLength = 35
        Me.txtRegdCity.Name = "txtRegdCity"
        Me.txtRegdCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegdCity.Size = New System.Drawing.Size(458, 22)
        Me.txtRegdCity.TabIndex = 12
        '
        'txtRegdEmail
        '
        Me.txtRegdEmail.AcceptsReturn = True
        Me.txtRegdEmail.BackColor = System.Drawing.Color.White
        Me.txtRegdEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegdEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegdEmail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegdEmail.ForeColor = System.Drawing.Color.Blue
        Me.txtRegdEmail.Location = New System.Drawing.Point(100, 156)
        Me.txtRegdEmail.MaxLength = 50
        Me.txtRegdEmail.Name = "txtRegdEmail"
        Me.txtRegdEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegdEmail.Size = New System.Drawing.Size(458, 22)
        Me.txtRegdEmail.TabIndex = 17
        '
        'txtRegdAdd1
        '
        Me.txtRegdAdd1.AcceptsReturn = True
        Me.txtRegdAdd1.BackColor = System.Drawing.Color.White
        Me.txtRegdAdd1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegdAdd1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegdAdd1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegdAdd1.ForeColor = System.Drawing.Color.Blue
        Me.txtRegdAdd1.Location = New System.Drawing.Point(100, 16)
        Me.txtRegdAdd1.MaxLength = 35
        Me.txtRegdAdd1.Name = "txtRegdAdd1"
        Me.txtRegdAdd1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegdAdd1.Size = New System.Drawing.Size(458, 22)
        Me.txtRegdAdd1.TabIndex = 10
        '
        'txtRegdPin
        '
        Me.txtRegdPin.AcceptsReturn = True
        Me.txtRegdPin.BackColor = System.Drawing.Color.White
        Me.txtRegdPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegdPin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegdPin.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegdPin.ForeColor = System.Drawing.Color.Blue
        Me.txtRegdPin.Location = New System.Drawing.Point(410, 100)
        Me.txtRegdPin.MaxLength = 35
        Me.txtRegdPin.Name = "txtRegdPin"
        Me.txtRegdPin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegdPin.Size = New System.Drawing.Size(148, 22)
        Me.txtRegdPin.TabIndex = 14
        '
        '_lblLabels_13
        '
        Me._lblLabels_13.AutoSize = True
        Me._lblLabels_13.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_13.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_13, CType(13, Short))
        Me._lblLabels_13.Location = New System.Drawing.Point(35, 46)
        Me._lblLabels_13.Name = "_lblLabels_13"
        Me._lblLabels_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_13.Size = New System.Drawing.Size(57, 13)
        Me._lblLabels_13.TabIndex = 40
        Me._lblLabels_13.Text = "Address  :"
        '
        '_lblLabels_16
        '
        Me._lblLabels_16.AutoSize = True
        Me._lblLabels_16.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_16.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_16, CType(16, Short))
        Me._lblLabels_16.Location = New System.Drawing.Point(373, 132)
        Me._lblLabels_16.Name = "_lblLabels_16"
        Me._lblLabels_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_16.Size = New System.Drawing.Size(31, 13)
        Me._lblLabels_16.TabIndex = 39
        Me._lblLabels_16.Text = "Fax :"
        '
        '_lblLabels_15
        '
        Me._lblLabels_15.AutoSize = True
        Me._lblLabels_15.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_15.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_15, CType(15, Short))
        Me._lblLabels_15.Location = New System.Drawing.Point(48, 130)
        Me._lblLabels_15.Name = "_lblLabels_15"
        Me._lblLabels_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_15.Size = New System.Drawing.Size(44, 13)
        Me._lblLabels_15.TabIndex = 38
        Me._lblLabels_15.Text = "Phone :"
        '
        '_lblLabels_14
        '
        Me._lblLabels_14.AutoSize = True
        Me._lblLabels_14.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_14.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_14, CType(14, Short))
        Me._lblLabels_14.Location = New System.Drawing.Point(35, 18)
        Me._lblLabels_14.Name = "_lblLabels_14"
        Me._lblLabels_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_14.Size = New System.Drawing.Size(57, 13)
        Me._lblLabels_14.TabIndex = 37
        Me._lblLabels_14.Text = "Address  :"
        '
        '_lblLabels_9
        '
        Me._lblLabels_9.AutoSize = True
        Me._lblLabels_9.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_9, CType(9, Short))
        Me._lblLabels_9.Location = New System.Drawing.Point(50, 158)
        Me._lblLabels_9.Name = "_lblLabels_9"
        Me._lblLabels_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_9.Size = New System.Drawing.Size(41, 13)
        Me._lblLabels_9.TabIndex = 36
        Me._lblLabels_9.Text = "Email :"
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.AutoSize = True
        Me._lblLabels_8.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_8, CType(8, Short))
        Me._lblLabels_8.Location = New System.Drawing.Point(56, 74)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(36, 13)
        Me._lblLabels_8.TabIndex = 35
        Me._lblLabels_8.Text = "City  :"
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.AutoSize = True
        Me._lblLabels_7.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_7, CType(7, Short))
        Me._lblLabels_7.Location = New System.Drawing.Point(53, 102)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(39, 13)
        Me._lblLabels_7.TabIndex = 34
        Me._lblLabels_7.Text = "State :"
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = True
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_3, CType(3, Short))
        Me._lblLabels_3.Location = New System.Drawing.Point(373, 105)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(28, 13)
        Me._lblLabels_3.TabIndex = 33
        Me._lblLabels_3.Text = "Pin :"
        '
        'FraCompany
        '
        Me.FraCompany.BackColor = System.Drawing.SystemColors.Control
        Me.FraCompany.Controls.Add(Me.txtPin)
        Me.FraCompany.Controls.Add(Me.txtCompanyShortName)
        Me.FraCompany.Controls.Add(Me.txtaddress)
        Me.FraCompany.Controls.Add(Me.txtEmail)
        Me.FraCompany.Controls.Add(Me.txtCompanyName)
        Me.FraCompany.Controls.Add(Me.txtCity)
        Me.FraCompany.Controls.Add(Me.txtState)
        Me.FraCompany.Controls.Add(Me.txtPhone)
        Me.FraCompany.Controls.Add(Me.txtFax)
        Me.FraCompany.Controls.Add(Me._lblLabels_12)
        Me.FraCompany.Controls.Add(Me._lblLabels_10)
        Me.FraCompany.Controls.Add(Me._lblLabels_11)
        Me.FraCompany.Controls.Add(Me.Label1)
        Me.FraCompany.Controls.Add(Me._lblLabels_5)
        Me.FraCompany.Controls.Add(Me._lblLabels_0)
        Me.FraCompany.Controls.Add(Me._lblLabels_1)
        Me.FraCompany.Controls.Add(Me._lblLabels_4)
        Me.FraCompany.Controls.Add(Me._lblLabels_6)
        Me.FraCompany.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCompany.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FraCompany.Location = New System.Drawing.Point(0, -2)
        Me.FraCompany.Name = "FraCompany"
        Me.FraCompany.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCompany.Size = New System.Drawing.Size(565, 212)
        Me.FraCompany.TabIndex = 0
        Me.FraCompany.TabStop = False
        '
        'txtPin
        '
        Me.txtPin.AcceptsReturn = True
        Me.txtPin.BackColor = System.Drawing.Color.White
        Me.txtPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPin.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPin.ForeColor = System.Drawing.Color.Blue
        Me.txtPin.Location = New System.Drawing.Point(410, 124)
        Me.txtPin.MaxLength = 35
        Me.txtPin.Name = "txtPin"
        Me.txtPin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPin.Size = New System.Drawing.Size(148, 22)
        Me.txtPin.TabIndex = 6
        '
        'txtCompanyShortName
        '
        Me.txtCompanyShortName.AcceptsReturn = True
        Me.txtCompanyShortName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompanyShortName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompanyShortName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompanyShortName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyShortName.ForeColor = System.Drawing.Color.Blue
        Me.txtCompanyShortName.Location = New System.Drawing.Point(100, 40)
        Me.txtCompanyShortName.MaxLength = 0
        Me.txtCompanyShortName.Name = "txtCompanyShortName"
        Me.txtCompanyShortName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompanyShortName.Size = New System.Drawing.Size(458, 22)
        Me.txtCompanyShortName.TabIndex = 2
        '
        'txtaddress
        '
        Me.txtaddress.AcceptsReturn = True
        Me.txtaddress.BackColor = System.Drawing.Color.White
        Me.txtaddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtaddress.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtaddress.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtaddress.ForeColor = System.Drawing.Color.Blue
        Me.txtaddress.Location = New System.Drawing.Point(100, 68)
        Me.txtaddress.MaxLength = 35
        Me.txtaddress.Name = "txtaddress"
        Me.txtaddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtaddress.Size = New System.Drawing.Size(458, 22)
        Me.txtaddress.TabIndex = 3
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.BackColor = System.Drawing.Color.White
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.Color.Blue
        Me.txtEmail.Location = New System.Drawing.Point(100, 180)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail.Size = New System.Drawing.Size(458, 22)
        Me.txtEmail.TabIndex = 9
        '
        'txtCompanyName
        '
        Me.txtCompanyName.AcceptsReturn = True
        Me.txtCompanyName.BackColor = System.Drawing.Color.White
        Me.txtCompanyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompanyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.ForeColor = System.Drawing.Color.Blue
        Me.txtCompanyName.Location = New System.Drawing.Point(100, 12)
        Me.txtCompanyName.MaxLength = 0
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompanyName.Size = New System.Drawing.Size(458, 22)
        Me.txtCompanyName.TabIndex = 1
        '
        'txtCity
        '
        Me.txtCity.AcceptsReturn = True
        Me.txtCity.BackColor = System.Drawing.Color.White
        Me.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCity.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.ForeColor = System.Drawing.Color.Blue
        Me.txtCity.Location = New System.Drawing.Point(100, 96)
        Me.txtCity.MaxLength = 35
        Me.txtCity.Name = "txtCity"
        Me.txtCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCity.Size = New System.Drawing.Size(458, 22)
        Me.txtCity.TabIndex = 4
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.BackColor = System.Drawing.Color.White
        Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtState.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.ForeColor = System.Drawing.Color.Blue
        Me.txtState.Location = New System.Drawing.Point(100, 124)
        Me.txtState.MaxLength = 35
        Me.txtState.Name = "txtState"
        Me.txtState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtState.Size = New System.Drawing.Size(146, 22)
        Me.txtState.TabIndex = 5
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.Color.White
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.ForeColor = System.Drawing.Color.Blue
        Me.txtPhone.Location = New System.Drawing.Point(100, 152)
        Me.txtPhone.MaxLength = 15
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(146, 22)
        Me.txtPhone.TabIndex = 7
        '
        'txtFax
        '
        Me.txtFax.AcceptsReturn = True
        Me.txtFax.BackColor = System.Drawing.Color.White
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFax.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.ForeColor = System.Drawing.Color.Blue
        Me.txtFax.Location = New System.Drawing.Point(410, 152)
        Me.txtFax.MaxLength = 15
        Me.txtFax.Name = "txtFax"
        Me.txtFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax.Size = New System.Drawing.Size(148, 22)
        Me.txtFax.TabIndex = 8
        '
        '_lblLabels_12
        '
        Me._lblLabels_12.AutoSize = True
        Me._lblLabels_12.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_12, CType(12, Short))
        Me._lblLabels_12.Location = New System.Drawing.Point(376, 130)
        Me._lblLabels_12.Name = "_lblLabels_12"
        Me._lblLabels_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_12.Size = New System.Drawing.Size(28, 13)
        Me._lblLabels_12.TabIndex = 31
        Me._lblLabels_12.Text = "Pin :"
        '
        '_lblLabels_10
        '
        Me._lblLabels_10.AutoSize = True
        Me._lblLabels_10.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_10, CType(10, Short))
        Me._lblLabels_10.Location = New System.Drawing.Point(55, 129)
        Me._lblLabels_10.Name = "_lblLabels_10"
        Me._lblLabels_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_10.Size = New System.Drawing.Size(39, 13)
        Me._lblLabels_10.TabIndex = 30
        Me._lblLabels_10.Text = "State :"
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.AutoSize = True
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_11, CType(11, Short))
        Me._lblLabels_11.Location = New System.Drawing.Point(58, 98)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(36, 13)
        Me._lblLabels_11.TabIndex = 24
        Me._lblLabels_11.Text = "City  :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(21, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Short Name :"
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.AutoSize = True
        Me._lblLabels_5.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_5, CType(5, Short))
        Me._lblLabels_5.Location = New System.Drawing.Point(51, 183)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(41, 13)
        Me._lblLabels_5.TabIndex = 22
        Me._lblLabels_5.Text = "Email :"
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(51, 16)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(43, 13)
        Me._lblLabels_0.TabIndex = 18
        Me._lblLabels_0.Text = "Name :"
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(37, 70)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(57, 13)
        Me._lblLabels_1.TabIndex = 20
        Me._lblLabels_1.Text = "Address  :"
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.AutoSize = True
        Me._lblLabels_4.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_4, CType(4, Short))
        Me._lblLabels_4.Location = New System.Drawing.Point(50, 155)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(44, 13)
        Me._lblLabels_4.TabIndex = 21
        Me._lblLabels_4.Text = "Phone :"
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.AutoSize = True
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_6, CType(6, Short))
        Me._lblLabels_6.Location = New System.Drawing.Point(373, 155)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(31, 13)
        Me._lblLabels_6.TabIndex = 23
        Me._lblLabels_6.Text = "Fax :"
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cmdSave)
        Me.Frame8.Controls.Add(Me.cmdcancel)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 503)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(563, 52)
        Me.Frame8.TabIndex = 26
        Me.Frame8.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.txtBranchName)
        Me.GroupBox1.Controls.Add(Me.txtAccountNo)
        Me.GroupBox1.Controls.Add(Me.txtBankName)
        Me.GroupBox1.Controls.Add(Me.txtIFSCCode)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(2, 406)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(564, 104)
        Me.GroupBox1.TabIndex = 33
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bank Detail"
        '
        'txtBranchName
        '
        Me.txtBranchName.AcceptsReturn = True
        Me.txtBranchName.BackColor = System.Drawing.Color.White
        Me.txtBranchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBranchName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBranchName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBranchName.ForeColor = System.Drawing.Color.Blue
        Me.txtBranchName.Location = New System.Drawing.Point(100, 44)
        Me.txtBranchName.MaxLength = 35
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBranchName.Size = New System.Drawing.Size(458, 22)
        Me.txtBranchName.TabIndex = 11
        '
        'txtAccountNo
        '
        Me.txtAccountNo.AcceptsReturn = True
        Me.txtAccountNo.BackColor = System.Drawing.Color.White
        Me.txtAccountNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAccountNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAccountNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountNo.ForeColor = System.Drawing.Color.Blue
        Me.txtAccountNo.Location = New System.Drawing.Point(100, 73)
        Me.txtAccountNo.MaxLength = 35
        Me.txtAccountNo.Name = "txtAccountNo"
        Me.txtAccountNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAccountNo.Size = New System.Drawing.Size(146, 22)
        Me.txtAccountNo.TabIndex = 13
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.Color.White
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.Color.Blue
        Me.txtBankName.Location = New System.Drawing.Point(100, 16)
        Me.txtBankName.MaxLength = 35
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(458, 22)
        Me.txtBankName.TabIndex = 10
        '
        'txtIFSCCode
        '
        Me.txtIFSCCode.AcceptsReturn = True
        Me.txtIFSCCode.BackColor = System.Drawing.Color.White
        Me.txtIFSCCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIFSCCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIFSCCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIFSCCode.ForeColor = System.Drawing.Color.Blue
        Me.txtIFSCCode.Location = New System.Drawing.Point(410, 73)
        Me.txtIFSCCode.MaxLength = 35
        Me.txtIFSCCode.Name = "txtIFSCCode"
        Me.txtIFSCCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIFSCCode.Size = New System.Drawing.Size(148, 22)
        Me.txtIFSCCode.TabIndex = 14
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label2.Location = New System.Drawing.Point(8, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(83, 13)
        Me.Label2.TabIndex = 40
        Me.Label2.Text = "Branch Name  :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label5.Location = New System.Drawing.Point(20, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "Bank Name :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label8.Location = New System.Drawing.Point(20, 75)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(71, 13)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "Account No :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label9.Location = New System.Drawing.Point(339, 78)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(65, 13)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "IFSC Code :"
        '
        'frmCompany
        '
        Me.AcceptButton = Me.cmdSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(568, 557)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.FraRegdInfo)
        Me.Controls.Add(Me.FraCompany)
        Me.Controls.Add(Me.Frame8)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(21, 67)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCompany"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Active Company Info"
        Me.FraRegdInfo.ResumeLayout(False)
        Me.FraRegdInfo.PerformLayout()
        Me.FraCompany.ResumeLayout(False)
        Me.FraCompany.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents txtBranchName As TextBox
    Public WithEvents txtAccountNo As TextBox
    Public WithEvents txtBankName As TextBox
    Public WithEvents txtIFSCCode As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents Label5 As Label
    Public WithEvents Label8 As Label
    Public WithEvents Label9 As Label
#End Region
End Class