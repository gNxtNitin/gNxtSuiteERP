Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmeInvoiceBillSetup
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
    Public WithEvents txtEINVPassword As System.Windows.Forms.TextBox
    Public WithEvents txtCDKey As System.Windows.Forms.TextBox
    Public WithEvents txtEFUserName As System.Windows.Forms.TextBox
    Public WithEvents txtEFPassword As System.Windows.Forms.TextBox
    Public WithEvents txtEINVUserName As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents _Label1_2 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents _FraBorder_1 As System.Windows.Forms.GroupBox
    Public WithEvents txteWayBillGenerate As System.Windows.Forms.TextBox
    Public WithEvents txteInvoicePrint As System.Windows.Forms.TextBox
    Public WithEvents txtCancelURL As System.Windows.Forms.TextBox
    Public WithEvents txtGenerateURL As System.Windows.Forms.TextBox
    Public WithEvents txtGetByIRN As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmeInvoiceBillSetup))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdcancel = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me._FraBorder_1 = New System.Windows.Forms.GroupBox()
        Me.txtEINVPassword = New System.Windows.Forms.TextBox()
        Me.txtCDKey = New System.Windows.Forms.TextBox()
        Me.txtEFUserName = New System.Windows.Forms.TextBox()
        Me.txtEFPassword = New System.Windows.Forms.TextBox()
        Me.txtEINVUserName = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me._Label1_2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me._FraBorder_2 = New System.Windows.Forms.GroupBox()
        Me.txteWayBillGenerate = New System.Windows.Forms.TextBox()
        Me.txteInvoicePrint = New System.Windows.Forms.TextBox()
        Me.txtCancelURL = New System.Windows.Forms.TextBox()
        Me.txtGenerateURL = New System.Windows.Forms.TextBox()
        Me.txtGetByIRN = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.FraBorder = New Microsoft.VisualBasic.Compatibility.VB6.GroupBoxArray(Me.components)
        Me.Label1 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me._FraBorder_1.SuspendLayout()
        Me._FraBorder_2.SuspendLayout()
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
        Me.cmdcancel.TabIndex = 1
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
        Me.cmdSavePrint.TabIndex = 3
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        Me.cmdSavePrint.Visible = False
        '
        '_FraBorder_1
        '
        Me._FraBorder_1.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_1.Controls.Add(Me.txtEINVPassword)
        Me._FraBorder_1.Controls.Add(Me.txtCDKey)
        Me._FraBorder_1.Controls.Add(Me.txtEFUserName)
        Me._FraBorder_1.Controls.Add(Me.txtEFPassword)
        Me._FraBorder_1.Controls.Add(Me.txtEINVUserName)
        Me._FraBorder_1.Controls.Add(Me.Label13)
        Me._FraBorder_1.Controls.Add(Me.Label12)
        Me._FraBorder_1.Controls.Add(Me._Label1_2)
        Me._FraBorder_1.Controls.Add(Me.Label11)
        Me._FraBorder_1.Controls.Add(Me.Label10)
        Me._FraBorder_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_1, CType(1, Short))
        Me._FraBorder_1.Location = New System.Drawing.Point(0, 2)
        Me._FraBorder_1.Name = "_FraBorder_1"
        Me._FraBorder_1.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_1.Size = New System.Drawing.Size(617, 125)
        Me._FraBorder_1.TabIndex = 4
        Me._FraBorder_1.TabStop = False
        Me._FraBorder_1.Text = "e-Invoice Setup"
        '
        'txtEINVPassword
        '
        Me.txtEINVPassword.AcceptsReturn = True
        Me.txtEINVPassword.BackColor = System.Drawing.SystemColors.Window
        Me.txtEINVPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEINVPassword.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEINVPassword.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEINVPassword.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEINVPassword.Location = New System.Drawing.Point(166, 100)
        Me.txtEINVPassword.MaxLength = 0
        Me.txtEINVPassword.Name = "txtEINVPassword"
        Me.txtEINVPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtEINVPassword.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEINVPassword.Size = New System.Drawing.Size(445, 22)
        Me.txtEINVPassword.TabIndex = 13
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
        Me.txtCDKey.TabIndex = 8
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
        Me.txtEFUserName.TabIndex = 7
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
        Me.txtEFPassword.TabIndex = 6
        '
        'txtEINVUserName
        '
        Me.txtEINVUserName.AcceptsReturn = True
        Me.txtEINVUserName.BackColor = System.Drawing.SystemColors.Window
        Me.txtEINVUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEINVUserName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEINVUserName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEINVUserName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEINVUserName.Location = New System.Drawing.Point(166, 78)
        Me.txtEINVUserName.MaxLength = 0
        Me.txtEINVUserName.Name = "txtEINVUserName"
        Me.txtEINVUserName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEINVUserName.Size = New System.Drawing.Size(445, 22)
        Me.txtEINVUserName.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(37, 102)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(109, 13)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "e Invoice Password :"
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
        Me.Label12.TabIndex = 12
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
        Me._Label1_2.TabIndex = 11
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
        Me.Label11.TabIndex = 10
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
        Me.Label10.Location = New System.Drawing.Point(29, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(117, 13)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "e Invoice User Name :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_FraBorder_2
        '
        Me._FraBorder_2.BackColor = System.Drawing.SystemColors.Control
        Me._FraBorder_2.Controls.Add(Me.txteWayBillGenerate)
        Me._FraBorder_2.Controls.Add(Me.txteInvoicePrint)
        Me._FraBorder_2.Controls.Add(Me.txtCancelURL)
        Me._FraBorder_2.Controls.Add(Me.txtGenerateURL)
        Me._FraBorder_2.Controls.Add(Me.txtGetByIRN)
        Me._FraBorder_2.Controls.Add(Me.Label3)
        Me._FraBorder_2.Controls.Add(Me.Label2)
        Me._FraBorder_2.Controls.Add(Me.Label18)
        Me._FraBorder_2.Controls.Add(Me.Label17)
        Me._FraBorder_2.Controls.Add(Me.Label14)
        Me._FraBorder_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._FraBorder_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraBorder.SetIndex(Me._FraBorder_2, CType(2, Short))
        Me._FraBorder_2.Location = New System.Drawing.Point(0, 128)
        Me._FraBorder_2.Name = "_FraBorder_2"
        Me._FraBorder_2.Padding = New System.Windows.Forms.Padding(0)
        Me._FraBorder_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._FraBorder_2.Size = New System.Drawing.Size(617, 143)
        Me._FraBorder_2.TabIndex = 15
        Me._FraBorder_2.TabStop = False
        Me._FraBorder_2.Text = "URL"
        '
        'txteWayBillGenerate
        '
        Me.txteWayBillGenerate.AcceptsReturn = True
        Me.txteWayBillGenerate.BackColor = System.Drawing.SystemColors.Window
        Me.txteWayBillGenerate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteWayBillGenerate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteWayBillGenerate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteWayBillGenerate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txteWayBillGenerate.Location = New System.Drawing.Point(166, 102)
        Me.txteWayBillGenerate.MaxLength = 0
        Me.txteWayBillGenerate.Name = "txteWayBillGenerate"
        Me.txteWayBillGenerate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteWayBillGenerate.Size = New System.Drawing.Size(445, 22)
        Me.txteWayBillGenerate.TabIndex = 21
        '
        'txteInvoicePrint
        '
        Me.txteInvoicePrint.AcceptsReturn = True
        Me.txteInvoicePrint.BackColor = System.Drawing.SystemColors.Window
        Me.txteInvoicePrint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txteInvoicePrint.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txteInvoicePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txteInvoicePrint.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txteInvoicePrint.Location = New System.Drawing.Point(166, 80)
        Me.txteInvoicePrint.MaxLength = 0
        Me.txteInvoicePrint.Name = "txteInvoicePrint"
        Me.txteInvoicePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txteInvoicePrint.Size = New System.Drawing.Size(445, 22)
        Me.txteInvoicePrint.TabIndex = 20
        '
        'txtCancelURL
        '
        Me.txtCancelURL.AcceptsReturn = True
        Me.txtCancelURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtCancelURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCancelURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCancelURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCancelURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCancelURL.Location = New System.Drawing.Point(166, 36)
        Me.txtCancelURL.MaxLength = 0
        Me.txtCancelURL.Name = "txtCancelURL"
        Me.txtCancelURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCancelURL.Size = New System.Drawing.Size(445, 22)
        Me.txtCancelURL.TabIndex = 17
        '
        'txtGenerateURL
        '
        Me.txtGenerateURL.AcceptsReturn = True
        Me.txtGenerateURL.BackColor = System.Drawing.SystemColors.Window
        Me.txtGenerateURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGenerateURL.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGenerateURL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGenerateURL.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGenerateURL.Location = New System.Drawing.Point(166, 14)
        Me.txtGenerateURL.MaxLength = 0
        Me.txtGenerateURL.Name = "txtGenerateURL"
        Me.txtGenerateURL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGenerateURL.Size = New System.Drawing.Size(445, 22)
        Me.txtGenerateURL.TabIndex = 16
        '
        'txtGetByIRN
        '
        Me.txtGetByIRN.AcceptsReturn = True
        Me.txtGetByIRN.BackColor = System.Drawing.SystemColors.Window
        Me.txtGetByIRN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGetByIRN.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGetByIRN.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGetByIRN.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGetByIRN.Location = New System.Drawing.Point(166, 58)
        Me.txtGetByIRN.MaxLength = 0
        Me.txtGetByIRN.Name = "txtGetByIRN"
        Me.txtGetByIRN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGetByIRN.Size = New System.Drawing.Size(445, 22)
        Me.txtGetByIRN.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(40, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(110, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "eWay Bill Generate :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(70, 82)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "eInvoice Print :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(109, 38)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(46, 13)
        Me.Label18.TabIndex = 23
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
        Me.Label17.Location = New System.Drawing.Point(96, 16)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(59, 13)
        Me.Label17.TabIndex = 22
        Me.Label17.Text = "Generate :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(33, 60)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(113, 13)
        Me.Label14.TabIndex = 18
        Me.Label14.Text = "Get eInvoice by IRN :"
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
        Me.Frame8.Location = New System.Drawing.Point(0, 266)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(617, 47)
        Me.Frame8.TabIndex = 2
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
        'frmeInvoiceBillSetup
        '
        Me.AcceptButton = Me.cmdSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(618, 314)
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
        Me.Name = "frmeInvoiceBillSetup"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "eInvoice Bill Setup"
        Me._FraBorder_1.ResumeLayout(False)
        Me._FraBorder_1.PerformLayout()
        Me._FraBorder_2.ResumeLayout(False)
        Me._FraBorder_2.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        CType(Me.FraBorder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class