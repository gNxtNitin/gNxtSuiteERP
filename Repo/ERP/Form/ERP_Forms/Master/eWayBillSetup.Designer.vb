Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmeWayBillSetup
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
    Public WithEvents txtEWBPassword As System.Windows.Forms.TextBox
    Public WithEvents txtCDKey As System.Windows.Forms.TextBox
    Public WithEvents txtEFUserName As System.Windows.Forms.TextBox
    Public WithEvents txtEFPassword As System.Windows.Forms.TextBox
    Public WithEvents txtEWBUserName As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents _Label1_2 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents _FraBorder_1 As System.Windows.Forms.GroupBox
    Public WithEvents txtGetPrintURL As System.Windows.Forms.TextBox
    Public WithEvents txtGetByDistanceURL As System.Windows.Forms.TextBox
    Public WithEvents txtCancelURL As System.Windows.Forms.TextBox
    Public WithEvents txtGenerateURL As System.Windows.Forms.TextBox
    Public WithEvents txtUpdateURL As System.Windows.Forms.TextBox
    Public WithEvents txtCreateURLWebtel As System.Windows.Forms.TextBox
    Public WithEvents txtFatchURL As System.Windows.Forms.TextBox
    Public WithEvents txtGetByIDURL As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents _Label1_3 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents _FraBorder_2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdcancel As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents FraBorder As VB6.GroupBoxArray
    Public WithEvents Label1 As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmeWayBillSetup))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me._FraBorder_1 = New System.Windows.Forms.GroupBox()
        Me.txtEWBPassword = New System.Windows.Forms.TextBox()
        Me.txtCDKey = New System.Windows.Forms.TextBox()
        Me.txtEFUserName = New System.Windows.Forms.TextBox()
        Me.txtEFPassword = New System.Windows.Forms.TextBox()
        Me.txtEWBUserName = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me._Label1_2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me._FraBorder_2 = New System.Windows.Forms.GroupBox()
        Me.txtConsilidationURLWebtel = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtGetPrintURL = New System.Windows.Forms.TextBox()
        Me.txtGetByDistanceURL = New System.Windows.Forms.TextBox()
        Me.txtCancelURL = New System.Windows.Forms.TextBox()
        Me.txtGenerateURL = New System.Windows.Forms.TextBox()
        Me.txtUpdateURL = New System.Windows.Forms.TextBox()
        Me.txtCreateURLWebtel = New System.Windows.Forms.TextBox()
        Me.txtFatchURL = New System.Windows.Forms.TextBox()
        Me.txtGetByIDURL = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me._Label1_3 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.FraBorder = New Microsoft.VisualBasic.Compatibility.VB6.GroupBoxArray(Me.components)
        Me.Label1 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtFontSize = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtDSCertificateNo = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me._optType_1 = New System.Windows.Forms.RadioButton()
        Me._optType_0 = New System.Windows.Forms.RadioButton()
        Me.txtDSAuthSign = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtDigitalSignBottomRight = New System.Windows.Forms.TextBox()
        Me.txtDigitalSignTopRight = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtDigitalSignBottomLeft = New System.Windows.Forms.TextBox()
        Me.txtDigitalSignURL = New System.Windows.Forms.TextBox()
        Me.txtDigitalSignUID = New System.Windows.Forms.TextBox()
        Me.txtDigitalSignPassword = New System.Windows.Forms.TextBox()
        Me.txtDigitalSignTopLeft = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.optType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtFindAuth = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.optTop = New System.Windows.Forms.RadioButton()
        Me.optBottom = New System.Windows.Forms.RadioButton()
        Me.Label26 = New System.Windows.Forms.Label()
        Me._FraBorder_1.SuspendLayout()
        Me._FraBorder_2.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.FraBorder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdcancel
        '
        Me.cmdcancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdcancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdcancel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdcancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdcancel.Image = CType(resources.GetObject("cmdcancel.Image"), System.Drawing.Image)
        Me.cmdcancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdcancel.Location = New System.Drawing.Point(544, 10)
        Me.cmdcancel.Name = "cmdcancel"
        Me.cmdcancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdcancel.Size = New System.Drawing.Size(69, 34)
        Me.cmdcancel.TabIndex = 6
        Me.cmdcancel.Text = "&Close"
        Me.cmdcancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.cmdcancel, "Cancel & Close Setup")
        Me.cmdcancel.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Location = New System.Drawing.Point(4, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(38, 25)
        Me.cmdSavePrint.TabIndex = 8
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        Me.cmdSavePrint.Visible = False
        '
        '_FraBorder_1
        '
        Me._FraBorder_1.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_1.Controls.Add(Me.txtEWBPassword)
        Me._FraBorder_1.Controls.Add(Me.txtCDKey)
        Me._FraBorder_1.Controls.Add(Me.txtEFUserName)
        Me._FraBorder_1.Controls.Add(Me.txtEFPassword)
        Me._FraBorder_1.Controls.Add(Me.txtEWBUserName)
        Me._FraBorder_1.Controls.Add(Me.Label13)
        Me._FraBorder_1.Controls.Add(Me.Label12)
        Me._FraBorder_1.Controls.Add(Me._Label1_2)
        Me._FraBorder_1.Controls.Add(Me.Label11)
        Me._FraBorder_1.Controls.Add(Me.Label10)
        Me._FraBorder_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_1, CType(1, Short))
        Me._FraBorder_1.Location = New System.Drawing.Point(0, 5)
        Me._FraBorder_1.Name = "_FraBorder_1"
        Me._FraBorder_1.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_1.Size = New System.Drawing.Size(617, 125)
        Me._FraBorder_1.TabIndex = 16
        Me._FraBorder_1.TabStop = False
        Me._FraBorder_1.Text = "e-Way Bill Setup"
        '
        'txtEWBPassword
        '
        Me.txtEWBPassword.AcceptsReturn = True
        Me.txtEWBPassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtEWBPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEWBPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEWBPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWBPassword.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEWBPassword.Location = New System.Drawing.Point(166, 100)
        Me.txtEWBPassword.MaxLength = 0
        Me.txtEWBPassword.Name = "txtEWBPassword"
        Me.txtEWBPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtEWBPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEWBPassword.Size = New System.Drawing.Size(445, 22)
        Me.txtEWBPassword.TabIndex = 4
        '
        'txtCDKey
        '
        Me.txtCDKey.AcceptsReturn = True
        Me.txtCDKey.BackColor = System.Drawing.SystemColors.Window
        Me.txtCDKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCDKey.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCDKey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCDKey.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCDKey.Location = New System.Drawing.Point(166, 12)
        Me.txtCDKey.MaxLength = 0
        Me.txtCDKey.Name = "txtCDKey"
        Me.txtCDKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCDKey.Size = New System.Drawing.Size(445, 22)
        Me.txtCDKey.TabIndex = 0
        '
        'txtEFUserName
        '
        Me.txtEFUserName.AcceptsReturn = True
        Me.txtEFUserName.BackColor = System.Drawing.SystemColors.Window
        Me.txtEFUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEFUserName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEFUserName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEFUserName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEFUserName.Location = New System.Drawing.Point(166, 34)
        Me.txtEFUserName.MaxLength = 0
        Me.txtEFUserName.Name = "txtEFUserName"
        Me.txtEFUserName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEFUserName.Size = New System.Drawing.Size(445, 22)
        Me.txtEFUserName.TabIndex = 1
        '
        'txtEFPassword
        '
        Me.txtEFPassword.AcceptsReturn = True
        Me.txtEFPassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtEFPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEFPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEFPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEFPassword.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEFPassword.Location = New System.Drawing.Point(166, 56)
        Me.txtEFPassword.MaxLength = 0
        Me.txtEFPassword.Name = "txtEFPassword"
        Me.txtEFPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtEFPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEFPassword.Size = New System.Drawing.Size(445, 22)
        Me.txtEFPassword.TabIndex = 2
        '
        'txtEWBUserName
        '
        Me.txtEWBUserName.AcceptsReturn = True
        Me.txtEWBUserName.BackColor = System.Drawing.SystemColors.Window
        Me.txtEWBUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEWBUserName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEWBUserName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEWBUserName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEWBUserName.Location = New System.Drawing.Point(166, 78)
        Me.txtEWBUserName.MaxLength = 0
        Me.txtEWBUserName.Name = "txtEWBUserName"
        Me.txtEWBUserName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEWBUserName.Size = New System.Drawing.Size(445, 22)
        Me.txtEWBUserName.TabIndex = 3
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(62, 102)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(88, 13)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "EWB Password :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(106, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(50, 13)
        Me.Label12.TabIndex = 24
        Me.Label12.Text = "CD Key :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label1_2
        '
        Me._Label1_2.AutoSize = True
        Me._Label1_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me._Label1_2, CType(2, Short))
        Me._Label1_2.Location = New System.Drawing.Point(71, 36)
        Me._Label1_2.Name = "_Label1_2"
        Me._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_2.Size = New System.Drawing.Size(81, 13)
        Me._Label1_2.TabIndex = 23
        Me._Label1_2.Text = "EFUser Name :"
        Me._Label1_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(79, 58)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(73, 13)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "EFPassword :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(54, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(96, 13)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "EWB User Name :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_FraBorder_2
        '
        Me._FraBorder_2.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_2.Controls.Add(Me.txtConsilidationURLWebtel)
        Me._FraBorder_2.Controls.Add(Me.Label23)
        Me._FraBorder_2.Controls.Add(Me.txtGetPrintURL)
        Me._FraBorder_2.Controls.Add(Me.txtGetByDistanceURL)
        Me._FraBorder_2.Controls.Add(Me.txtCancelURL)
        Me._FraBorder_2.Controls.Add(Me.txtGenerateURL)
        Me._FraBorder_2.Controls.Add(Me.txtUpdateURL)
        Me._FraBorder_2.Controls.Add(Me.txtCreateURLWebtel)
        Me._FraBorder_2.Controls.Add(Me.txtFatchURL)
        Me._FraBorder_2.Controls.Add(Me.txtGetByIDURL)
        Me._FraBorder_2.Controls.Add(Me.Label5)
        Me._FraBorder_2.Controls.Add(Me.Label4)
        Me._FraBorder_2.Controls.Add(Me.Label18)
        Me._FraBorder_2.Controls.Add(Me.Label17)
        Me._FraBorder_2.Controls.Add(Me._Label1_3)
        Me._FraBorder_2.Controls.Add(Me.Label16)
        Me._FraBorder_2.Controls.Add(Me.Label15)
        Me._FraBorder_2.Controls.Add(Me.Label14)
        Me._FraBorder_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_2, CType(2, Short))
        Me._FraBorder_2.Location = New System.Drawing.Point(0, 128)
        Me._FraBorder_2.Name = "_FraBorder_2"
        Me._FraBorder_2.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_2.Size = New System.Drawing.Size(617, 219)
        Me._FraBorder_2.TabIndex = 27
        Me._FraBorder_2.TabStop = False
        Me._FraBorder_2.Text = "e-Way Bill URL"
        '
        'txtConsilidationURLWebtel
        '
        Me.txtConsilidationURLWebtel.AcceptsReturn = True
        Me.txtConsilidationURLWebtel.BackColor = System.Drawing.SystemColors.Window
        Me.txtConsilidationURLWebtel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConsilidationURLWebtel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConsilidationURLWebtel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsilidationURLWebtel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtConsilidationURLWebtel.Location = New System.Drawing.Point(166, 190)
        Me.txtConsilidationURLWebtel.MaxLength = 0
        Me.txtConsilidationURLWebtel.Name = "txtConsilidationURLWebtel"
        Me.txtConsilidationURLWebtel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtConsilidationURLWebtel.Size = New System.Drawing.Size(445, 22)
        Me.txtConsilidationURLWebtel.TabIndex = 44
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(42, 192)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(113, 13)
        Me.Label23.TabIndex = 45
        Me.Label23.Text = "Consolidation Eway :"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtGetPrintURL
        '
        Me.txtGetPrintURL.AcceptsReturn = True
        Me.txtGetPrintURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtGetPrintURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGetPrintURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGetPrintURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGetPrintURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGetPrintURL.Location = New System.Drawing.Point(166, 168)
        Me.txtGetPrintURL.MaxLength = 0
        Me.txtGetPrintURL.Name = "txtGetPrintURL"
        Me.txtGetPrintURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGetPrintURL.Size = New System.Drawing.Size(445, 22)
        Me.txtGetPrintURL.TabIndex = 7
        '
        'txtGetByDistanceURL
        '
        Me.txtGetByDistanceURL.AcceptsReturn = True
        Me.txtGetByDistanceURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtGetByDistanceURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGetByDistanceURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGetByDistanceURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGetByDistanceURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGetByDistanceURL.Location = New System.Drawing.Point(166, 146)
        Me.txtGetByDistanceURL.MaxLength = 0
        Me.txtGetByDistanceURL.Name = "txtGetByDistanceURL"
        Me.txtGetByDistanceURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGetByDistanceURL.Size = New System.Drawing.Size(445, 22)
        Me.txtGetByDistanceURL.TabIndex = 6
        '
        'txtCancelURL
        '
        Me.txtCancelURL.AcceptsReturn = True
        Me.txtCancelURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtCancelURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCancelURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCancelURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCancelURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCancelURL.Location = New System.Drawing.Point(166, 80)
        Me.txtCancelURL.MaxLength = 0
        Me.txtCancelURL.Name = "txtCancelURL"
        Me.txtCancelURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCancelURL.Size = New System.Drawing.Size(445, 22)
        Me.txtCancelURL.TabIndex = 3
        '
        'txtGenerateURL
        '
        Me.txtGenerateURL.AcceptsReturn = True
        Me.txtGenerateURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtGenerateURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGenerateURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGenerateURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGenerateURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGenerateURL.Location = New System.Drawing.Point(166, 58)
        Me.txtGenerateURL.MaxLength = 0
        Me.txtGenerateURL.Name = "txtGenerateURL"
        Me.txtGenerateURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGenerateURL.Size = New System.Drawing.Size(445, 22)
        Me.txtGenerateURL.TabIndex = 2
        '
        'txtUpdateURL
        '
        Me.txtUpdateURL.AcceptsReturn = True
        Me.txtUpdateURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtUpdateURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUpdateURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUpdateURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUpdateURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUpdateURL.Location = New System.Drawing.Point(166, 36)
        Me.txtUpdateURL.MaxLength = 0
        Me.txtUpdateURL.Name = "txtUpdateURL"
        Me.txtUpdateURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUpdateURL.Size = New System.Drawing.Size(445, 22)
        Me.txtUpdateURL.TabIndex = 1
        '
        'txtCreateURLWebtel
        '
        Me.txtCreateURLWebtel.AcceptsReturn = True
        Me.txtCreateURLWebtel.BackColor = System.Drawing.SystemColors.Window
        Me.txtCreateURLWebtel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCreateURLWebtel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCreateURLWebtel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreateURLWebtel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCreateURLWebtel.Location = New System.Drawing.Point(166, 14)
        Me.txtCreateURLWebtel.MaxLength = 0
        Me.txtCreateURLWebtel.Name = "txtCreateURLWebtel"
        Me.txtCreateURLWebtel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCreateURLWebtel.Size = New System.Drawing.Size(445, 22)
        Me.txtCreateURLWebtel.TabIndex = 0
        '
        'txtFatchURL
        '
        Me.txtFatchURL.AcceptsReturn = True
        Me.txtFatchURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtFatchURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFatchURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFatchURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFatchURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFatchURL.Location = New System.Drawing.Point(166, 102)
        Me.txtFatchURL.MaxLength = 0
        Me.txtFatchURL.Name = "txtFatchURL"
        Me.txtFatchURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFatchURL.Size = New System.Drawing.Size(445, 22)
        Me.txtFatchURL.TabIndex = 4
        '
        'txtGetByIDURL
        '
        Me.txtGetByIDURL.AcceptsReturn = True
        Me.txtGetByIDURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtGetByIDURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGetByIDURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGetByIDURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGetByIDURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGetByIDURL.Location = New System.Drawing.Point(166, 124)
        Me.txtGetByIDURL.MaxLength = 0
        Me.txtGetByIDURL.Name = "txtGetByIDURL"
        Me.txtGetByIDURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGetByIDURL.Size = New System.Drawing.Size(445, 22)
        Me.txtGetByIDURL.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(98, 170)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Get Print :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(78, 148)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Get Distance :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(109, 82)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(46, 13)
        Me.Label18.TabIndex = 39
        Me.Label18.Text = "Cancel :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(96, 60)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(59, 13)
        Me.Label17.TabIndex = 38
        Me.Label17.Text = "Generate :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label1_3
        '
        Me._Label1_3.AutoSize = True
        Me._Label1_3.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me._Label1_3, CType(3, Short))
        Me._Label1_3.Location = New System.Drawing.Point(107, 38)
        Me._Label1_3.Name = "_Label1_3"
        Me._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_3.Size = New System.Drawing.Size(51, 13)
        Me._Label1_3.TabIndex = 37
        Me._Label1_3.Text = "Update :"
        Me._Label1_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(111, 16)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(46, 13)
        Me.Label16.TabIndex = 36
        Me.Label16.Text = "Create :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(117, 104)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(40, 13)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "Fatch :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(94, 126)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(61, 13)
        Me.Label14.TabIndex = 34
        Me.Label14.Text = "Get By ID :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cmdcancel)
        Me.Frame8.Controls.Add(Me.cmdSave)
        Me.Frame8.Controls.Add(Me.cmdSavePrint)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 561)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(617, 47)
        Me.Frame8.TabIndex = 7
        Me.Frame8.TabStop = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdSave.Location = New System.Drawing.Point(4, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(69, 34)
        Me.cmdSave.TabIndex = 0
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.Label26)
        Me.GroupBox1.Controls.Add(Me.Frame1)
        Me.GroupBox1.Controls.Add(Me.txtFindAuth)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtFontSize)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.txtDSCertificateNo)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me._optType_1)
        Me.GroupBox1.Controls.Add(Me._optType_0)
        Me.GroupBox1.Controls.Add(Me.txtDSAuthSign)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.txtDigitalSignBottomRight)
        Me.GroupBox1.Controls.Add(Me.txtDigitalSignTopRight)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.txtDigitalSignBottomLeft)
        Me.GroupBox1.Controls.Add(Me.txtDigitalSignURL)
        Me.GroupBox1.Controls.Add(Me.txtDigitalSignUID)
        Me.GroupBox1.Controls.Add(Me.txtDigitalSignPassword)
        Me.GroupBox1.Controls.Add(Me.txtDigitalSignTopLeft)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(1, 353)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(617, 213)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Digital Signature Setup"
        '
        'txtFontSize
        '
        Me.txtFontSize.AcceptsReturn = True
        Me.txtFontSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtFontSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFontSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFontSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFontSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFontSize.Location = New System.Drawing.Point(166, 156)
        Me.txtFontSize.MaxLength = 0
        Me.txtFontSize.Name = "txtFontSize"
        Me.txtFontSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFontSize.Size = New System.Drawing.Size(112, 22)
        Me.txtFontSize.TabIndex = 44
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(96, 159)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(59, 13)
        Me.Label24.TabIndex = 45
        Me.Label24.Text = "Font Size :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDSCertificateNo
        '
        Me.txtDSCertificateNo.AcceptsReturn = True
        Me.txtDSCertificateNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDSCertificateNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDSCertificateNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDSCertificateNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDSCertificateNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDSCertificateNo.Location = New System.Drawing.Point(500, 67)
        Me.txtDSCertificateNo.MaxLength = 0
        Me.txtDSCertificateNo.Name = "txtDSCertificateNo"
        Me.txtDSCertificateNo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtDSCertificateNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDSCertificateNo.Size = New System.Drawing.Size(112, 22)
        Me.txtDSCertificateNo.TabIndex = 42
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(406, 69)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(89, 13)
        Me.Label22.TabIndex = 43
        Me.Label22.Text = "Certificate SNo :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(33, 20)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(125, 13)
        Me.Label21.TabIndex = 41
        Me.Label21.Text = "Digital Signature Type :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_optType_1
        '
        Me._optType_1.AutoSize = True
        Me._optType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_1, CType(1, Short))
        Me._optType_1.Location = New System.Drawing.Point(252, 18)
        Me._optType_1.Name = "_optType_1"
        Me._optType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_1.Size = New System.Drawing.Size(59, 18)
        Me._optType_1.TabIndex = 40
        Me._optType_1.TabStop = True
        Me._optType_1.Text = "Token"
        Me._optType_1.UseVisualStyleBackColor = False
        '
        '_optType_0
        '
        Me._optType_0.AutoSize = True
        Me._optType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optType_0.Checked = True
        Me._optType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_0, CType(0, Short))
        Me._optType_0.Location = New System.Drawing.Point(170, 18)
        Me._optType_0.Name = "_optType_0"
        Me._optType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_0.Size = New System.Drawing.Size(43, 18)
        Me._optType_0.TabIndex = 39
        Me._optType_0.TabStop = True
        Me._optType_0.Text = "API"
        Me._optType_0.UseVisualStyleBackColor = False
        '
        'txtDSAuthSign
        '
        Me.txtDSAuthSign.AcceptsReturn = True
        Me.txtDSAuthSign.BackColor = System.Drawing.SystemColors.Window
        Me.txtDSAuthSign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDSAuthSign.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDSAuthSign.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDSAuthSign.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDSAuthSign.Location = New System.Drawing.Point(166, 67)
        Me.txtDSAuthSign.MaxLength = 0
        Me.txtDSAuthSign.Name = "txtDSAuthSign"
        Me.txtDSAuthSign.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDSAuthSign.Size = New System.Drawing.Size(236, 22)
        Me.txtDSAuthSign.TabIndex = 1
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(34, 70)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(120, 13)
        Me.Label20.TabIndex = 32
        Me.Label20.Text = "Authorized Signatory :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDigitalSignBottomRight
        '
        Me.txtDigitalSignBottomRight.AcceptsReturn = True
        Me.txtDigitalSignBottomRight.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigitalSignBottomRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigitalSignBottomRight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigitalSignBottomRight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigitalSignBottomRight.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigitalSignBottomRight.Location = New System.Drawing.Point(500, 133)
        Me.txtDigitalSignBottomRight.MaxLength = 0
        Me.txtDigitalSignBottomRight.Name = "txtDigitalSignBottomRight"
        Me.txtDigitalSignBottomRight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigitalSignBottomRight.Size = New System.Drawing.Size(112, 22)
        Me.txtDigitalSignBottomRight.TabIndex = 7
        '
        'txtDigitalSignTopRight
        '
        Me.txtDigitalSignTopRight.AcceptsReturn = True
        Me.txtDigitalSignTopRight.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigitalSignTopRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigitalSignTopRight.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigitalSignTopRight.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigitalSignTopRight.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigitalSignTopRight.Location = New System.Drawing.Point(500, 111)
        Me.txtDigitalSignTopRight.MaxLength = 0
        Me.txtDigitalSignTopRight.Name = "txtDigitalSignTopRight"
        Me.txtDigitalSignTopRight.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigitalSignTopRight.Size = New System.Drawing.Size(112, 22)
        Me.txtDigitalSignTopRight.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(417, 135)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(79, 13)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "Bottom Right:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(434, 113)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(62, 13)
        Me.Label19.TabIndex = 28
        Me.Label19.Text = "Top Right :"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDigitalSignBottomLeft
        '
        Me.txtDigitalSignBottomLeft.AcceptsReturn = True
        Me.txtDigitalSignBottomLeft.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigitalSignBottomLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigitalSignBottomLeft.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigitalSignBottomLeft.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigitalSignBottomLeft.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigitalSignBottomLeft.Location = New System.Drawing.Point(166, 133)
        Me.txtDigitalSignBottomLeft.MaxLength = 0
        Me.txtDigitalSignBottomLeft.Name = "txtDigitalSignBottomLeft"
        Me.txtDigitalSignBottomLeft.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigitalSignBottomLeft.Size = New System.Drawing.Size(112, 22)
        Me.txtDigitalSignBottomLeft.TabIndex = 6
        '
        'txtDigitalSignURL
        '
        Me.txtDigitalSignURL.AcceptsReturn = True
        Me.txtDigitalSignURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigitalSignURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigitalSignURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigitalSignURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigitalSignURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigitalSignURL.Location = New System.Drawing.Point(166, 45)
        Me.txtDigitalSignURL.MaxLength = 0
        Me.txtDigitalSignURL.Name = "txtDigitalSignURL"
        Me.txtDigitalSignURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigitalSignURL.Size = New System.Drawing.Size(445, 22)
        Me.txtDigitalSignURL.TabIndex = 0
        '
        'txtDigitalSignUID
        '
        Me.txtDigitalSignUID.AcceptsReturn = True
        Me.txtDigitalSignUID.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigitalSignUID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigitalSignUID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigitalSignUID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigitalSignUID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigitalSignUID.Location = New System.Drawing.Point(166, 89)
        Me.txtDigitalSignUID.MaxLength = 0
        Me.txtDigitalSignUID.Name = "txtDigitalSignUID"
        Me.txtDigitalSignUID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigitalSignUID.Size = New System.Drawing.Size(236, 22)
        Me.txtDigitalSignUID.TabIndex = 2
        '
        'txtDigitalSignPassword
        '
        Me.txtDigitalSignPassword.AcceptsReturn = True
        Me.txtDigitalSignPassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigitalSignPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigitalSignPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigitalSignPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigitalSignPassword.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigitalSignPassword.Location = New System.Drawing.Point(500, 89)
        Me.txtDigitalSignPassword.MaxLength = 0
        Me.txtDigitalSignPassword.Name = "txtDigitalSignPassword"
        Me.txtDigitalSignPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtDigitalSignPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigitalSignPassword.Size = New System.Drawing.Size(112, 22)
        Me.txtDigitalSignPassword.TabIndex = 3
        '
        'txtDigitalSignTopLeft
        '
        Me.txtDigitalSignTopLeft.AcceptsReturn = True
        Me.txtDigitalSignTopLeft.BackColor = System.Drawing.SystemColors.Window
        Me.txtDigitalSignTopLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDigitalSignTopLeft.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDigitalSignTopLeft.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDigitalSignTopLeft.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDigitalSignTopLeft.Location = New System.Drawing.Point(166, 111)
        Me.txtDigitalSignTopLeft.MaxLength = 0
        Me.txtDigitalSignTopLeft.Name = "txtDigitalSignTopLeft"
        Me.txtDigitalSignTopLeft.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDigitalSignTopLeft.Size = New System.Drawing.Size(112, 22)
        Me.txtDigitalSignTopLeft.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(84, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Bottom Left:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(34, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(121, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "Digital Signature URL :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(86, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(69, 13)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "User Name :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(435, 91)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(61, 13)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Password :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(101, 113)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(54, 13)
        Me.Label8.TabIndex = 21
        Me.Label8.Text = "Top Left :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'optType
        '
        '
        'txtFindAuth
        '
        Me.txtFindAuth.AcceptsReturn = True
        Me.txtFindAuth.BackColor = System.Drawing.SystemColors.Window
        Me.txtFindAuth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFindAuth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFindAuth.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFindAuth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFindAuth.Location = New System.Drawing.Point(166, 180)
        Me.txtFindAuth.MaxLength = 0
        Me.txtFindAuth.Name = "txtFindAuth"
        Me.txtFindAuth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFindAuth.Size = New System.Drawing.Size(331, 22)
        Me.txtFindAuth.TabIndex = 46
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(70, 182)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(85, 13)
        Me.Label25.TabIndex = 47
        Me.Label25.Text = "Find Auth Text :"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.optTop)
        Me.Frame1.Controls.Add(Me.optBottom)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(503, 155)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(107, 55)
        Me.Frame1.TabIndex = 48
        Me.Frame1.TabStop = False
        '
        'optTop
        '
        Me.optTop.AutoSize = True
        Me.optTop.BackColor = System.Drawing.SystemColors.Control
        Me.optTop.Checked = True
        Me.optTop.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTop.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTop.Location = New System.Drawing.Point(13, 11)
        Me.optTop.Name = "optTop"
        Me.optTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTop.Size = New System.Drawing.Size(45, 18)
        Me.optTop.TabIndex = 3
        Me.optTop.TabStop = True
        Me.optTop.Text = "Top"
        Me.optTop.UseVisualStyleBackColor = False
        '
        'optBottom
        '
        Me.optBottom.AutoSize = True
        Me.optBottom.BackColor = System.Drawing.SystemColors.Control
        Me.optBottom.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBottom.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBottom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBottom.Location = New System.Drawing.Point(13, 30)
        Me.optBottom.Name = "optBottom"
        Me.optBottom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBottom.Size = New System.Drawing.Size(65, 18)
        Me.optBottom.TabIndex = 4
        Me.optBottom.Text = "Bottom"
        Me.optBottom.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(392, 159)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(104, 13)
        Me.Label26.TabIndex = 49
        Me.Label26.Text = "Find Auth Location:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmeWayBillSetup
        '
        Me.AcceptButton = Me.cmdSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(618, 610)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me._FraBorder_1)
        Me.Controls.Add(Me._FraBorder_2)
        Me.Controls.Add(Me.Frame8)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(8, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmeWayBillSetup"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "eWay Bill Setup"
        Me._FraBorder_1.ResumeLayout(False)
        Me._FraBorder_1.PerformLayout()
        Me._FraBorder_2.ResumeLayout(False)
        Me._FraBorder_2.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        CType(Me.FraBorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents txtDigitalSignBottomLeft As TextBox
    Public WithEvents txtDigitalSignURL As TextBox
    Public WithEvents txtDigitalSignUID As TextBox
    Public WithEvents txtDigitalSignPassword As TextBox
    Public WithEvents txtDigitalSignTopLeft As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents Label3 As Label
    Public WithEvents Label6 As Label
    Public WithEvents Label7 As Label
    Public WithEvents Label8 As Label
    Public WithEvents txtDigitalSignBottomRight As TextBox
    Public WithEvents txtDigitalSignTopRight As TextBox
    Public WithEvents Label9 As Label
    Public WithEvents Label19 As Label
    Public WithEvents txtDSAuthSign As TextBox
    Public WithEvents Label20 As Label
    Public WithEvents Label21 As Label
    Public WithEvents _optType_1 As RadioButton
    Public WithEvents _optType_0 As RadioButton
    Public WithEvents optType As VB6.RadioButtonArray
    Public WithEvents txtDSCertificateNo As TextBox
    Public WithEvents Label22 As Label
    Public WithEvents txtConsilidationURLWebtel As TextBox
    Public WithEvents Label23 As Label
    Public WithEvents txtFontSize As TextBox
    Public WithEvents Label24 As Label
    Public WithEvents txtFindAuth As TextBox
    Public WithEvents Label25 As Label
    Public WithEvents Label26 As Label
    Public WithEvents Frame1 As GroupBox
    Public WithEvents optTop As RadioButton
    Public WithEvents optBottom As RadioButton
#End Region
End Class