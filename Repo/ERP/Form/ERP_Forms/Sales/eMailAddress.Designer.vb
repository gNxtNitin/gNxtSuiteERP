Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmeMailAddress
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
    Public WithEvents TxtSMTP As System.Windows.Forms.TextBox
    Public WithEvents TxtPOP3 As System.Windows.Forms.TextBox
    Public WithEvents _Label1_2 As System.Windows.Forms.Label
    Public WithEvents _Label1_1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents TxtPassword As System.Windows.Forms.TextBox
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtITBDId As System.Windows.Forms.TextBox
    Public WithEvents txtIndentAppID As System.Windows.Forms.TextBox
    Public WithEvents txtSecurity As System.Windows.Forms.TextBox
    Public WithEvents txtPaySlipeMail As System.Windows.Forms.TextBox
    Public WithEvents txtStockeMail As System.Windows.Forms.TextBox
    Public WithEvents txtHReMail As System.Windows.Forms.TextBox
    Public WithEvents txtMainteMail As System.Windows.Forms.TextBox
    Public WithEvents txtPureMail As System.Windows.Forms.TextBox
    Public WithEvents txtDespeMail As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents _Label1_0 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents _FraBorder_5 As System.Windows.Forms.GroupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmeMailAddress))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.TxtSMTP = New System.Windows.Forms.TextBox()
        Me.TxtPOP3 = New System.Windows.Forms.TextBox()
        Me._Label1_2 = New System.Windows.Forms.Label()
        Me._Label1_1 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.TxtPassword = New System.Windows.Forms.TextBox()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me._FraBorder_5 = New System.Windows.Forms.GroupBox()
        Me.txtITBDId = New System.Windows.Forms.TextBox()
        Me.txtIndentAppID = New System.Windows.Forms.TextBox()
        Me.txtSecurity = New System.Windows.Forms.TextBox()
        Me.txtPaySlipeMail = New System.Windows.Forms.TextBox()
        Me.txtStockeMail = New System.Windows.Forms.TextBox()
        Me.txtHReMail = New System.Windows.Forms.TextBox()
        Me.txtMainteMail = New System.Windows.Forms.TextBox()
        Me.txtPureMail = New System.Windows.Forms.TextBox()
        Me.txtDespeMail = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._Label1_0 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.FraBorder = New Microsoft.VisualBasic.Compatibility.VB6.GroupBoxArray(Me.components)
        Me.Label1 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboEnableSSL = New System.Windows.Forms.ComboBox()
        Me.LblCategory = New System.Windows.Forms.Label()
        Me.txtToolBrkDown = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtFFeMail = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtInsuranceID = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me._FraBorder_5.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.FraBorder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.cmdcancel.TabIndex = 14
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
        Me.cmdSavePrint.TabIndex = 16
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        Me.cmdSavePrint.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.TxtSMTP)
        Me.Frame1.Controls.Add(Me.TxtPOP3)
        Me.Frame1.Controls.Add(Me._Label1_2)
        Me.Frame1.Controls.Add(Me._Label1_1)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(326, 76)
        Me.Frame1.TabIndex = 26
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Server Information"
        '
        'TxtSMTP
        '
        Me.TxtSMTP.AcceptsReturn = True
        Me.TxtSMTP.BackColor = System.Drawing.SystemColors.Window
        Me.TxtSMTP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSMTP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtSMTP.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSMTP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtSMTP.Location = New System.Drawing.Point(127, 18)
        Me.TxtSMTP.MaxLength = 0
        Me.TxtSMTP.Name = "TxtSMTP"
        Me.TxtSMTP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtSMTP.Size = New System.Drawing.Size(193, 22)
        Me.TxtSMTP.TabIndex = 1
        '
        'TxtPOP3
        '
        Me.TxtPOP3.AcceptsReturn = True
        Me.TxtPOP3.BackColor = System.Drawing.SystemColors.Window
        Me.TxtPOP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPOP3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPOP3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPOP3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtPOP3.Location = New System.Drawing.Point(127, 45)
        Me.TxtPOP3.MaxLength = 0
        Me.TxtPOP3.Name = "TxtPOP3"
        Me.TxtPOP3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtPOP3.Size = New System.Drawing.Size(193, 22)
        Me.TxtPOP3.TabIndex = 2
        '
        '_Label1_2
        '
        Me._Label1_2.AutoSize = True
        Me._Label1_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me._Label1_2, CType(2, Short))
        Me._Label1_2.Location = New System.Drawing.Point(7, 19)
        Me._Label1_2.Name = "_Label1_2"
        Me._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_2.Size = New System.Drawing.Size(119, 13)
        Me._Label1_2.TabIndex = 28
        Me._Label1_2.Text = "Outgoing mail (SMTP)"
        '
        '_Label1_1
        '
        Me._Label1_1.AutoSize = True
        Me._Label1_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me._Label1_1, CType(1, Short))
        Me._Label1_1.Location = New System.Drawing.Point(8, 46)
        Me._Label1_1.Name = "_Label1_1"
        Me._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_1.Size = New System.Drawing.Size(116, 13)
        Me._Label1_1.TabIndex = 27
        Me._Label1_1.Text = "Incoming mail (POP3)"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.TxtPassword)
        Me.Frame2.Controls.Add(Me.TxtAccount)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(329, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(286, 76)
        Me.Frame2.TabIndex = 23
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Incoming Mail Server"
        '
        'TxtPassword
        '
        Me.TxtPassword.AcceptsReturn = True
        Me.TxtPassword.BackColor = System.Drawing.SystemColors.Window
        Me.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPassword.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtPassword.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtPassword.Location = New System.Drawing.Point(90, 47)
        Me.TxtPassword.MaxLength = 0
        Me.TxtPassword.Name = "TxtPassword"
        Me.TxtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtPassword.Size = New System.Drawing.Size(190, 22)
        Me.TxtPassword.TabIndex = 4
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(90, 18)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(190, 22)
        Me.TxtAccount.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(10, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(86, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Account Name :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(10, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(61, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Password :"
        '
        '_FraBorder_5
        '
        Me._FraBorder_5.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_5.Controls.Add(Me.txtInsuranceID)
        Me._FraBorder_5.Controls.Add(Me.Label15)
        Me._FraBorder_5.Controls.Add(Me.txtFFeMail)
        Me._FraBorder_5.Controls.Add(Me.Label14)
        Me._FraBorder_5.Controls.Add(Me.txtToolBrkDown)
        Me._FraBorder_5.Controls.Add(Me.Label13)
        Me._FraBorder_5.Controls.Add(Me.txtITBDId)
        Me._FraBorder_5.Controls.Add(Me.txtIndentAppID)
        Me._FraBorder_5.Controls.Add(Me.txtSecurity)
        Me._FraBorder_5.Controls.Add(Me.txtPaySlipeMail)
        Me._FraBorder_5.Controls.Add(Me.txtStockeMail)
        Me._FraBorder_5.Controls.Add(Me.txtHReMail)
        Me._FraBorder_5.Controls.Add(Me.txtMainteMail)
        Me._FraBorder_5.Controls.Add(Me.txtPureMail)
        Me._FraBorder_5.Controls.Add(Me.txtDespeMail)
        Me._FraBorder_5.Controls.Add(Me.Label11)
        Me._FraBorder_5.Controls.Add(Me.Label10)
        Me._FraBorder_5.Controls.Add(Me.Label9)
        Me._FraBorder_5.Controls.Add(Me.Label8)
        Me._FraBorder_5.Controls.Add(Me.Label4)
        Me._FraBorder_5.Controls.Add(Me.Label3)
        Me._FraBorder_5.Controls.Add(Me.Label2)
        Me._FraBorder_5.Controls.Add(Me._Label1_0)
        Me._FraBorder_5.Controls.Add(Me.Label6)
        Me._FraBorder_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_5, CType(5, Short))
        Me._FraBorder_5.Location = New System.Drawing.Point(0, 105)
        Me._FraBorder_5.Name = "_FraBorder_5"
        Me._FraBorder_5.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_5.Size = New System.Drawing.Size(617, 314)
        Me._FraBorder_5.TabIndex = 17
        Me._FraBorder_5.TabStop = False
        '
        'txtITBDId
        '
        Me.txtITBDId.AcceptsReturn = True
        Me.txtITBDId.BackColor = System.Drawing.SystemColors.Window
        Me.txtITBDId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtITBDId.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtITBDId.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtITBDId.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtITBDId.Location = New System.Drawing.Point(166, 216)
        Me.txtITBDId.MaxLength = 0
        Me.txtITBDId.Name = "txtITBDId"
        Me.txtITBDId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtITBDId.Size = New System.Drawing.Size(445, 22)
        Me.txtITBDId.TabIndex = 13
        '
        'txtIndentAppID
        '
        Me.txtIndentAppID.AcceptsReturn = True
        Me.txtIndentAppID.BackColor = System.Drawing.SystemColors.Window
        Me.txtIndentAppID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIndentAppID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIndentAppID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIndentAppID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIndentAppID.Location = New System.Drawing.Point(166, 191)
        Me.txtIndentAppID.MaxLength = 0
        Me.txtIndentAppID.Name = "txtIndentAppID"
        Me.txtIndentAppID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIndentAppID.Size = New System.Drawing.Size(445, 22)
        Me.txtIndentAppID.TabIndex = 12
        '
        'txtSecurity
        '
        Me.txtSecurity.AcceptsReturn = True
        Me.txtSecurity.BackColor = System.Drawing.SystemColors.Window
        Me.txtSecurity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSecurity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSecurity.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecurity.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSecurity.Location = New System.Drawing.Point(166, 167)
        Me.txtSecurity.MaxLength = 0
        Me.txtSecurity.Name = "txtSecurity"
        Me.txtSecurity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSecurity.Size = New System.Drawing.Size(445, 22)
        Me.txtSecurity.TabIndex = 11
        '
        'txtPaySlipeMail
        '
        Me.txtPaySlipeMail.AcceptsReturn = True
        Me.txtPaySlipeMail.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaySlipeMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaySlipeMail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaySlipeMail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaySlipeMail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPaySlipeMail.Location = New System.Drawing.Point(166, 142)
        Me.txtPaySlipeMail.MaxLength = 0
        Me.txtPaySlipeMail.Name = "txtPaySlipeMail"
        Me.txtPaySlipeMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaySlipeMail.Size = New System.Drawing.Size(445, 22)
        Me.txtPaySlipeMail.TabIndex = 10
        '
        'txtStockeMail
        '
        Me.txtStockeMail.AcceptsReturn = True
        Me.txtStockeMail.BackColor = System.Drawing.SystemColors.Window
        Me.txtStockeMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStockeMail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStockeMail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockeMail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtStockeMail.Location = New System.Drawing.Point(166, 118)
        Me.txtStockeMail.MaxLength = 0
        Me.txtStockeMail.Name = "txtStockeMail"
        Me.txtStockeMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStockeMail.Size = New System.Drawing.Size(445, 22)
        Me.txtStockeMail.TabIndex = 9
        '
        'txtHReMail
        '
        Me.txtHReMail.AcceptsReturn = True
        Me.txtHReMail.BackColor = System.Drawing.SystemColors.Window
        Me.txtHReMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHReMail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHReMail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHReMail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtHReMail.Location = New System.Drawing.Point(166, 93)
        Me.txtHReMail.MaxLength = 0
        Me.txtHReMail.Name = "txtHReMail"
        Me.txtHReMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHReMail.Size = New System.Drawing.Size(445, 22)
        Me.txtHReMail.TabIndex = 8
        '
        'txtMainteMail
        '
        Me.txtMainteMail.AcceptsReturn = True
        Me.txtMainteMail.BackColor = System.Drawing.SystemColors.Window
        Me.txtMainteMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMainteMail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMainteMail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMainteMail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMainteMail.Location = New System.Drawing.Point(166, 68)
        Me.txtMainteMail.MaxLength = 0
        Me.txtMainteMail.Name = "txtMainteMail"
        Me.txtMainteMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMainteMail.Size = New System.Drawing.Size(445, 22)
        Me.txtMainteMail.TabIndex = 7
        '
        'txtPureMail
        '
        Me.txtPureMail.AcceptsReturn = True
        Me.txtPureMail.BackColor = System.Drawing.SystemColors.Window
        Me.txtPureMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPureMail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPureMail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPureMail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPureMail.Location = New System.Drawing.Point(166, 44)
        Me.txtPureMail.MaxLength = 0
        Me.txtPureMail.Name = "txtPureMail"
        Me.txtPureMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPureMail.Size = New System.Drawing.Size(445, 22)
        Me.txtPureMail.TabIndex = 6
        '
        'txtDespeMail
        '
        Me.txtDespeMail.AcceptsReturn = True
        Me.txtDespeMail.BackColor = System.Drawing.SystemColors.Window
        Me.txtDespeMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDespeMail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDespeMail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDespeMail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDespeMail.Location = New System.Drawing.Point(166, 20)
        Me.txtDespeMail.MaxLength = 0
        Me.txtDespeMail.Name = "txtDespeMail"
        Me.txtDespeMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDespeMail.Size = New System.Drawing.Size(445, 22)
        Me.txtDespeMail.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(65, 219)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(100, 13)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "IT Break Down ID :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(55, 193)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(109, 13)
        Me.Label10.TabIndex = 31
        Me.Label10.Text = "Indent Approval ID :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(69, 169)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(92, 13)
        Me.Label9.TabIndex = 30
        Me.Label9.Text = "Security Mail ID :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(49, 144)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(112, 13)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "ePay Slip Report To :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(66, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Stock Report To :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(80, 95)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "HR Report To :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(31, 70)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(130, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Maintenance Report To :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label1_0
        '
        Me._Label1_0.AutoSize = True
        Me._Label1_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me._Label1_0, CType(0, Short))
        Me._Label1_0.Location = New System.Drawing.Point(51, 46)
        Me._Label1_0.Name = "_Label1_0"
        Me._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_0.Size = New System.Drawing.Size(110, 13)
        Me._Label1_0.TabIndex = 19
        Me._Label1_0.Text = "Purchase Report To :"
        Me._Label1_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(49, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(113, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Despatch Report To :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cmdcancel)
        Me.Frame8.Controls.Add(Me.cmdSave)
        Me.Frame8.Controls.Add(Me.cmdSavePrint)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 417)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(617, 47)
        Me.Frame8.TabIndex = 15
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
        'txtPort
        '
        Me.txtPort.AcceptsReturn = True
        Me.txtPort.BackColor = System.Drawing.SystemColors.Window
        Me.txtPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPort.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPort.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPort.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtPort.Location = New System.Drawing.Point(47, 81)
        Me.txtPort.MaxLength = 0
        Me.txtPort.Name = "txtPort"
        Me.txtPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPort.Size = New System.Drawing.Size(72, 22)
        Me.txtPort.TabIndex = 27
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(9, 85)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(34, 13)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "Port :"
        '
        'cboEnableSSL
        '
        Me.cboEnableSSL.BackColor = System.Drawing.SystemColors.Window
        Me.cboEnableSSL.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEnableSSL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEnableSSL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEnableSSL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboEnableSSL.Location = New System.Drawing.Point(241, 81)
        Me.cboEnableSSL.Name = "cboEnableSSL"
        Me.cboEnableSSL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEnableSSL.Size = New System.Drawing.Size(81, 21)
        Me.cboEnableSSL.TabIndex = 105
        '
        'LblCategory
        '
        Me.LblCategory.BackColor = System.Drawing.SystemColors.Control
        Me.LblCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblCategory.Location = New System.Drawing.Point(158, 85)
        Me.LblCategory.Name = "LblCategory"
        Me.LblCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCategory.Size = New System.Drawing.Size(82, 17)
        Me.LblCategory.TabIndex = 106
        Me.LblCategory.Text = "Enable SSL :"
        Me.LblCategory.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtToolBrkDown
        '
        Me.txtToolBrkDown.AcceptsReturn = True
        Me.txtToolBrkDown.BackColor = System.Drawing.SystemColors.Window
        Me.txtToolBrkDown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToolBrkDown.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToolBrkDown.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToolBrkDown.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtToolBrkDown.Location = New System.Drawing.Point(166, 240)
        Me.txtToolBrkDown.MaxLength = 0
        Me.txtToolBrkDown.Name = "txtToolBrkDown"
        Me.txtToolBrkDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToolBrkDown.Size = New System.Drawing.Size(445, 22)
        Me.txtToolBrkDown.TabIndex = 33
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(52, 243)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(113, 13)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "Tool Break Down ID :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFFeMail
        '
        Me.txtFFeMail.AcceptsReturn = True
        Me.txtFFeMail.BackColor = System.Drawing.SystemColors.Window
        Me.txtFFeMail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFFeMail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFFeMail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFFeMail.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFFeMail.Location = New System.Drawing.Point(166, 264)
        Me.txtFFeMail.MaxLength = 0
        Me.txtFFeMail.Name = "txtFFeMail"
        Me.txtFFeMail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFFeMail.Size = New System.Drawing.Size(445, 22)
        Me.txtFFeMail.TabIndex = 35
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(115, 267)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(50, 13)
        Me.Label14.TabIndex = 36
        Me.Label14.Text = "F&&F  ID :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtInsuranceID
        '
        Me.txtInsuranceID.AcceptsReturn = True
        Me.txtInsuranceID.BackColor = System.Drawing.SystemColors.Window
        Me.txtInsuranceID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInsuranceID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInsuranceID.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsuranceID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInsuranceID.Location = New System.Drawing.Point(165, 289)
        Me.txtInsuranceID.MaxLength = 0
        Me.txtInsuranceID.Name = "txtInsuranceID"
        Me.txtInsuranceID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInsuranceID.Size = New System.Drawing.Size(445, 22)
        Me.txtInsuranceID.TabIndex = 37
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(91, 292)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(74, 13)
        Me.Label15.TabIndex = 38
        Me.Label15.Text = "Insurance ID :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmeMailAddress
        '
        Me.AcceptButton = Me.cmdSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(618, 467)
        Me.Controls.Add(Me.cboEnableSSL)
        Me.Controls.Add(Me.LblCategory)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me._FraBorder_5)
        Me.Controls.Add(Me.Frame8)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(8, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmeMailAddress"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "eMail Address"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me._FraBorder_5.ResumeLayout(False)
        Me._FraBorder_5.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        CType(Me.FraBorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents txtPort As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents cboEnableSSL As ComboBox
    Public WithEvents LblCategory As Label
    Public WithEvents txtFFeMail As TextBox
    Public WithEvents Label14 As Label
    Public WithEvents txtToolBrkDown As TextBox
    Public WithEvents Label13 As Label
    Public WithEvents txtInsuranceID As TextBox
    Public WithEvents Label15 As Label
#End Region
End Class