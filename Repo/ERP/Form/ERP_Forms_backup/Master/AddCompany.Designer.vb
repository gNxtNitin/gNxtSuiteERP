Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAddCompany
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
    Public WithEvents txtGSTRegnNo As System.Windows.Forms.TextBox
    Public WithEvents txtCommissionerate As System.Windows.Forms.TextBox
    Public WithEvents chkExempted As System.Windows.Forms.CheckBox
    Public WithEvents txtCIN As System.Windows.Forms.TextBox
    Public WithEvents txtTANNo As System.Windows.Forms.TextBox
    Public WithEvents txtJurisdiction As System.Windows.Forms.TextBox
    Public WithEvents txtTINNo As System.Windows.Forms.TextBox
    Public WithEvents chkEOU As System.Windows.Forms.CheckBox
    Public WithEvents txtIECNo As System.Windows.Forms.TextBox
    Public WithEvents txtECCNo As System.Windows.Forms.TextBox
    Public WithEvents txtRegnNo As System.Windows.Forms.TextBox
    Public WithEvents txtPFEst As System.Windows.Forms.TextBox
    Public WithEvents txtESIEst As System.Windows.Forms.TextBox
    Public WithEvents txtPin As System.Windows.Forms.TextBox
    Public WithEvents TxtTDSNo As System.Windows.Forms.TextBox
    Public WithEvents txtFax As System.Windows.Forms.TextBox
    Public WithEvents txtPhone As System.Windows.Forms.TextBox
    Public WithEvents txtState As System.Windows.Forms.TextBox
    Public WithEvents txtCity As System.Windows.Forms.TextBox
    Public WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Public WithEvents txtEmail As System.Windows.Forms.TextBox
    Public WithEvents txtAdd As System.Windows.Forms.TextBox
    Public WithEvents txtPAN As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_27 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_26 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_25 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_24 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_23 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_22 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_21 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_20 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_19 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_17 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_16 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_15 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_13 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_10 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_9 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents FraCompany As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents ADataGrid As VB6.ADODC
    Public WithEvents lblLabels As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddCompany))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.FraCompany = New System.Windows.Forms.GroupBox()
        Me.txtGSTRegnNo = New System.Windows.Forms.TextBox()
        Me.txtCommissionerate = New System.Windows.Forms.TextBox()
        Me.chkExempted = New System.Windows.Forms.CheckBox()
        Me.txtCIN = New System.Windows.Forms.TextBox()
        Me.txtTANNo = New System.Windows.Forms.TextBox()
        Me.txtJurisdiction = New System.Windows.Forms.TextBox()
        Me.txtTINNo = New System.Windows.Forms.TextBox()
        Me.chkEOU = New System.Windows.Forms.CheckBox()
        Me.txtIECNo = New System.Windows.Forms.TextBox()
        Me.txtECCNo = New System.Windows.Forms.TextBox()
        Me.txtRegnNo = New System.Windows.Forms.TextBox()
        Me.txtPFEst = New System.Windows.Forms.TextBox()
        Me.txtESIEst = New System.Windows.Forms.TextBox()
        Me.txtPin = New System.Windows.Forms.TextBox()
        Me.TxtTDSNo = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtAdd = New System.Windows.Forms.TextBox()
        Me.txtPAN = New System.Windows.Forms.TextBox()
        Me._lblLabels_27 = New System.Windows.Forms.Label()
        Me._lblLabels_26 = New System.Windows.Forms.Label()
        Me._lblLabels_25 = New System.Windows.Forms.Label()
        Me._lblLabels_24 = New System.Windows.Forms.Label()
        Me._lblLabels_23 = New System.Windows.Forms.Label()
        Me._lblLabels_22 = New System.Windows.Forms.Label()
        Me._lblLabels_21 = New System.Windows.Forms.Label()
        Me._lblLabels_20 = New System.Windows.Forms.Label()
        Me._lblLabels_19 = New System.Windows.Forms.Label()
        Me._lblLabels_17 = New System.Windows.Forms.Label()
        Me._lblLabels_16 = New System.Windows.Forms.Label()
        Me._lblLabels_15 = New System.Windows.Forms.Label()
        Me._lblLabels_13 = New System.Windows.Forms.Label()
        Me._lblLabels_10 = New System.Windows.Forms.Label()
        Me._lblLabels_9 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtPrintCompanyName = New System.Windows.Forms.TextBox()
        Me.FraCompany.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.CmdClose.Location = New System.Drawing.Point(496, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 24
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
        Me.CmdSave.Location = New System.Drawing.Point(4, 13)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 0
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'FraCompany
        '
        Me.FraCompany.BackColor = System.Drawing.SystemColors.Control
        Me.FraCompany.Controls.Add(Me.txtPrintCompanyName)
        Me.FraCompany.Controls.Add(Me.Label1)
        Me.FraCompany.Controls.Add(Me.txtGSTRegnNo)
        Me.FraCompany.Controls.Add(Me.txtCommissionerate)
        Me.FraCompany.Controls.Add(Me.chkExempted)
        Me.FraCompany.Controls.Add(Me.txtCIN)
        Me.FraCompany.Controls.Add(Me.txtTANNo)
        Me.FraCompany.Controls.Add(Me.txtJurisdiction)
        Me.FraCompany.Controls.Add(Me.txtTINNo)
        Me.FraCompany.Controls.Add(Me.chkEOU)
        Me.FraCompany.Controls.Add(Me.txtIECNo)
        Me.FraCompany.Controls.Add(Me.txtECCNo)
        Me.FraCompany.Controls.Add(Me.txtRegnNo)
        Me.FraCompany.Controls.Add(Me.txtPFEst)
        Me.FraCompany.Controls.Add(Me.txtESIEst)
        Me.FraCompany.Controls.Add(Me.txtPin)
        Me.FraCompany.Controls.Add(Me.TxtTDSNo)
        Me.FraCompany.Controls.Add(Me.txtFax)
        Me.FraCompany.Controls.Add(Me.txtPhone)
        Me.FraCompany.Controls.Add(Me.txtState)
        Me.FraCompany.Controls.Add(Me.txtCity)
        Me.FraCompany.Controls.Add(Me.txtCompanyName)
        Me.FraCompany.Controls.Add(Me.txtEmail)
        Me.FraCompany.Controls.Add(Me.txtAdd)
        Me.FraCompany.Controls.Add(Me.txtPAN)
        Me.FraCompany.Controls.Add(Me._lblLabels_27)
        Me.FraCompany.Controls.Add(Me._lblLabels_26)
        Me.FraCompany.Controls.Add(Me._lblLabels_25)
        Me.FraCompany.Controls.Add(Me._lblLabels_24)
        Me.FraCompany.Controls.Add(Me._lblLabels_23)
        Me.FraCompany.Controls.Add(Me._lblLabels_22)
        Me.FraCompany.Controls.Add(Me._lblLabels_21)
        Me.FraCompany.Controls.Add(Me._lblLabels_20)
        Me.FraCompany.Controls.Add(Me._lblLabels_19)
        Me.FraCompany.Controls.Add(Me._lblLabels_17)
        Me.FraCompany.Controls.Add(Me._lblLabels_16)
        Me.FraCompany.Controls.Add(Me._lblLabels_15)
        Me.FraCompany.Controls.Add(Me._lblLabels_13)
        Me.FraCompany.Controls.Add(Me._lblLabels_10)
        Me.FraCompany.Controls.Add(Me._lblLabels_9)
        Me.FraCompany.Controls.Add(Me._lblLabels_2)
        Me.FraCompany.Controls.Add(Me._lblLabels_7)
        Me.FraCompany.Controls.Add(Me._lblLabels_6)
        Me.FraCompany.Controls.Add(Me._lblLabels_4)
        Me.FraCompany.Controls.Add(Me._lblLabels_1)
        Me.FraCompany.Controls.Add(Me._lblLabels_0)
        Me.FraCompany.Controls.Add(Me._lblLabels_5)
        Me.FraCompany.Controls.Add(Me._lblLabels_3)
        Me.FraCompany.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCompany.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FraCompany.Location = New System.Drawing.Point(0, 0)
        Me.FraCompany.Name = "FraCompany"
        Me.FraCompany.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCompany.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCompany.Size = New System.Drawing.Size(620, 471)
        Me.FraCompany.TabIndex = 26
        Me.FraCompany.TabStop = False
        '
        'txtGSTRegnNo
        '
        Me.txtGSTRegnNo.AcceptsReturn = True
        Me.txtGSTRegnNo.BackColor = System.Drawing.Color.White
        Me.txtGSTRegnNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTRegnNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTRegnNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTRegnNo.ForeColor = System.Drawing.Color.Blue
        Me.txtGSTRegnNo.Location = New System.Drawing.Point(155, 176)
        Me.txtGSTRegnNo.MaxLength = 50
        Me.txtGSTRegnNo.Name = "txtGSTRegnNo"
        Me.txtGSTRegnNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTRegnNo.Size = New System.Drawing.Size(439, 22)
        Me.txtGSTRegnNo.TabIndex = 9
        '
        'txtCommissionerate
        '
        Me.txtCommissionerate.AcceptsReturn = True
        Me.txtCommissionerate.BackColor = System.Drawing.Color.White
        Me.txtCommissionerate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCommissionerate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCommissionerate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCommissionerate.ForeColor = System.Drawing.Color.Blue
        Me.txtCommissionerate.Location = New System.Drawing.Point(155, 203)
        Me.txtCommissionerate.MaxLength = 50
        Me.txtCommissionerate.Name = "txtCommissionerate"
        Me.txtCommissionerate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCommissionerate.Size = New System.Drawing.Size(439, 22)
        Me.txtCommissionerate.TabIndex = 10
        '
        'chkExempted
        '
        Me.chkExempted.BackColor = System.Drawing.SystemColors.Control
        Me.chkExempted.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkExempted.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExempted.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkExempted.Location = New System.Drawing.Point(155, 434)
        Me.chkExempted.Name = "chkExempted"
        Me.chkExempted.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkExempted.Size = New System.Drawing.Size(89, 17)
        Me.chkExempted.TabIndex = 56
        Me.chkExempted.Text = "(Yes / No)"
        Me.chkExempted.UseVisualStyleBackColor = False
        '
        'txtCIN
        '
        Me.txtCIN.AcceptsReturn = True
        Me.txtCIN.BackColor = System.Drawing.Color.White
        Me.txtCIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCIN.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCIN.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCIN.ForeColor = System.Drawing.Color.Blue
        Me.txtCIN.Location = New System.Drawing.Point(155, 402)
        Me.txtCIN.MaxLength = 30
        Me.txtCIN.Name = "txtCIN"
        Me.txtCIN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCIN.Size = New System.Drawing.Size(165, 22)
        Me.txtCIN.TabIndex = 47
        '
        'txtTANNo
        '
        Me.txtTANNo.AcceptsReturn = True
        Me.txtTANNo.BackColor = System.Drawing.Color.White
        Me.txtTANNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTANNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTANNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTANNo.ForeColor = System.Drawing.Color.Blue
        Me.txtTANNo.Location = New System.Drawing.Point(418, 402)
        Me.txtTANNo.MaxLength = 30
        Me.txtTANNo.Name = "txtTANNo"
        Me.txtTANNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTANNo.Size = New System.Drawing.Size(175, 22)
        Me.txtTANNo.TabIndex = 48
        '
        'txtJurisdiction
        '
        Me.txtJurisdiction.AcceptsReturn = True
        Me.txtJurisdiction.BackColor = System.Drawing.Color.White
        Me.txtJurisdiction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtJurisdiction.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtJurisdiction.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJurisdiction.ForeColor = System.Drawing.Color.Blue
        Me.txtJurisdiction.Location = New System.Drawing.Point(419, 285)
        Me.txtJurisdiction.MaxLength = 50
        Me.txtJurisdiction.Name = "txtJurisdiction"
        Me.txtJurisdiction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtJurisdiction.Size = New System.Drawing.Size(175, 22)
        Me.txtJurisdiction.TabIndex = 18
        '
        'txtTINNo
        '
        Me.txtTINNo.AcceptsReturn = True
        Me.txtTINNo.BackColor = System.Drawing.Color.White
        Me.txtTINNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTINNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTINNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTINNo.ForeColor = System.Drawing.Color.Blue
        Me.txtTINNo.Location = New System.Drawing.Point(419, 231)
        Me.txtTINNo.MaxLength = 30
        Me.txtTINNo.Name = "txtTINNo"
        Me.txtTINNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTINNo.Size = New System.Drawing.Size(175, 22)
        Me.txtTINNo.TabIndex = 12
        '
        'chkEOU
        '
        Me.chkEOU.BackColor = System.Drawing.SystemColors.Control
        Me.chkEOU.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkEOU.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEOU.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEOU.Location = New System.Drawing.Point(494, 434)
        Me.chkEOU.Name = "chkEOU"
        Me.chkEOU.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkEOU.Size = New System.Drawing.Size(89, 16)
        Me.chkEOU.TabIndex = 50
        Me.chkEOU.Text = "(Yes / No)"
        Me.chkEOU.UseVisualStyleBackColor = False
        '
        'txtIECNo
        '
        Me.txtIECNo.AcceptsReturn = True
        Me.txtIECNo.BackColor = System.Drawing.Color.White
        Me.txtIECNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIECNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIECNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIECNo.ForeColor = System.Drawing.Color.Blue
        Me.txtIECNo.Location = New System.Drawing.Point(155, 375)
        Me.txtIECNo.MaxLength = 30
        Me.txtIECNo.Name = "txtIECNo"
        Me.txtIECNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIECNo.Size = New System.Drawing.Size(440, 22)
        Me.txtIECNo.TabIndex = 46
        '
        'txtECCNo
        '
        Me.txtECCNo.AcceptsReturn = True
        Me.txtECCNo.BackColor = System.Drawing.Color.White
        Me.txtECCNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtECCNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtECCNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtECCNo.ForeColor = System.Drawing.Color.Blue
        Me.txtECCNo.Location = New System.Drawing.Point(419, 313)
        Me.txtECCNo.MaxLength = 30
        Me.txtECCNo.Name = "txtECCNo"
        Me.txtECCNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtECCNo.Size = New System.Drawing.Size(175, 22)
        Me.txtECCNo.TabIndex = 22
        '
        'txtRegnNo
        '
        Me.txtRegnNo.AcceptsReturn = True
        Me.txtRegnNo.BackColor = System.Drawing.Color.White
        Me.txtRegnNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegnNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRegnNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRegnNo.ForeColor = System.Drawing.Color.Blue
        Me.txtRegnNo.Location = New System.Drawing.Point(155, 313)
        Me.txtRegnNo.MaxLength = 30
        Me.txtRegnNo.Name = "txtRegnNo"
        Me.txtRegnNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRegnNo.Size = New System.Drawing.Size(165, 22)
        Me.txtRegnNo.TabIndex = 21
        '
        'txtPFEst
        '
        Me.txtPFEst.AcceptsReturn = True
        Me.txtPFEst.BackColor = System.Drawing.Color.White
        Me.txtPFEst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPFEst.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPFEst.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFEst.ForeColor = System.Drawing.Color.Blue
        Me.txtPFEst.Location = New System.Drawing.Point(155, 258)
        Me.txtPFEst.MaxLength = 35
        Me.txtPFEst.Name = "txtPFEst"
        Me.txtPFEst.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPFEst.Size = New System.Drawing.Size(165, 22)
        Me.txtPFEst.TabIndex = 13
        '
        'txtESIEst
        '
        Me.txtESIEst.AcceptsReturn = True
        Me.txtESIEst.BackColor = System.Drawing.Color.White
        Me.txtESIEst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIEst.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIEst.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIEst.ForeColor = System.Drawing.Color.Blue
        Me.txtESIEst.Location = New System.Drawing.Point(419, 258)
        Me.txtESIEst.MaxLength = 15
        Me.txtESIEst.Name = "txtESIEst"
        Me.txtESIEst.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIEst.Size = New System.Drawing.Size(175, 22)
        Me.txtESIEst.TabIndex = 14
        '
        'txtPin
        '
        Me.txtPin.AcceptsReturn = True
        Me.txtPin.BackColor = System.Drawing.Color.White
        Me.txtPin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPin.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPin.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPin.ForeColor = System.Drawing.Color.Blue
        Me.txtPin.Location = New System.Drawing.Point(419, 97)
        Me.txtPin.MaxLength = 35
        Me.txtPin.Name = "txtPin"
        Me.txtPin.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPin.Size = New System.Drawing.Size(175, 22)
        Me.txtPin.TabIndex = 5
        '
        'TxtTDSNo
        '
        Me.TxtTDSNo.AcceptsReturn = True
        Me.TxtTDSNo.BackColor = System.Drawing.Color.White
        Me.TxtTDSNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtTDSNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtTDSNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTDSNo.ForeColor = System.Drawing.Color.Blue
        Me.TxtTDSNo.Location = New System.Drawing.Point(155, 231)
        Me.TxtTDSNo.MaxLength = 30
        Me.TxtTDSNo.Name = "TxtTDSNo"
        Me.TxtTDSNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtTDSNo.Size = New System.Drawing.Size(165, 22)
        Me.TxtTDSNo.TabIndex = 11
        '
        'txtFax
        '
        Me.txtFax.AcceptsReturn = True
        Me.txtFax.BackColor = System.Drawing.Color.White
        Me.txtFax.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFax.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFax.ForeColor = System.Drawing.Color.Blue
        Me.txtFax.Location = New System.Drawing.Point(419, 123)
        Me.txtFax.MaxLength = 15
        Me.txtFax.Name = "txtFax"
        Me.txtFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax.Size = New System.Drawing.Size(175, 22)
        Me.txtFax.TabIndex = 7
        '
        'txtPhone
        '
        Me.txtPhone.AcceptsReturn = True
        Me.txtPhone.BackColor = System.Drawing.Color.White
        Me.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPhone.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPhone.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPhone.ForeColor = System.Drawing.Color.Blue
        Me.txtPhone.Location = New System.Drawing.Point(155, 121)
        Me.txtPhone.MaxLength = 15
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPhone.Size = New System.Drawing.Size(165, 22)
        Me.txtPhone.TabIndex = 6
        '
        'txtState
        '
        Me.txtState.AcceptsReturn = True
        Me.txtState.BackColor = System.Drawing.Color.White
        Me.txtState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtState.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtState.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtState.ForeColor = System.Drawing.Color.Blue
        Me.txtState.Location = New System.Drawing.Point(155, 95)
        Me.txtState.MaxLength = 35
        Me.txtState.Name = "txtState"
        Me.txtState.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtState.Size = New System.Drawing.Size(165, 22)
        Me.txtState.TabIndex = 4
        '
        'txtCity
        '
        Me.txtCity.AcceptsReturn = True
        Me.txtCity.BackColor = System.Drawing.Color.White
        Me.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCity.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCity.ForeColor = System.Drawing.Color.Blue
        Me.txtCity.Location = New System.Drawing.Point(156, 67)
        Me.txtCity.MaxLength = 35
        Me.txtCity.Name = "txtCity"
        Me.txtCity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCity.Size = New System.Drawing.Size(439, 22)
        Me.txtCity.TabIndex = 3
        '
        'txtCompanyName
        '
        Me.txtCompanyName.AcceptsReturn = True
        Me.txtCompanyName.BackColor = System.Drawing.Color.White
        Me.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompanyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompanyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.ForeColor = System.Drawing.Color.Blue
        Me.txtCompanyName.Location = New System.Drawing.Point(156, 12)
        Me.txtCompanyName.MaxLength = 35
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompanyName.Size = New System.Drawing.Size(439, 22)
        Me.txtCompanyName.TabIndex = 1
        '
        'txtEmail
        '
        Me.txtEmail.AcceptsReturn = True
        Me.txtEmail.BackColor = System.Drawing.Color.White
        Me.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmail.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.ForeColor = System.Drawing.Color.Blue
        Me.txtEmail.Location = New System.Drawing.Point(155, 149)
        Me.txtEmail.MaxLength = 50
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmail.Size = New System.Drawing.Size(439, 22)
        Me.txtEmail.TabIndex = 8
        '
        'txtAdd
        '
        Me.txtAdd.AcceptsReturn = True
        Me.txtAdd.BackColor = System.Drawing.Color.White
        Me.txtAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAdd.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdd.ForeColor = System.Drawing.Color.Blue
        Me.txtAdd.Location = New System.Drawing.Point(156, 39)
        Me.txtAdd.MaxLength = 35
        Me.txtAdd.Name = "txtAdd"
        Me.txtAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAdd.Size = New System.Drawing.Size(439, 22)
        Me.txtAdd.TabIndex = 2
        '
        'txtPAN
        '
        Me.txtPAN.AcceptsReturn = True
        Me.txtPAN.BackColor = System.Drawing.Color.White
        Me.txtPAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPAN.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPAN.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPAN.ForeColor = System.Drawing.Color.Blue
        Me.txtPAN.Location = New System.Drawing.Point(155, 285)
        Me.txtPAN.MaxLength = 50
        Me.txtPAN.Name = "txtPAN"
        Me.txtPAN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPAN.Size = New System.Drawing.Size(165, 22)
        Me.txtPAN.TabIndex = 17
        '
        '_lblLabels_27
        '
        Me._lblLabels_27.AutoSize = True
        Me._lblLabels_27.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_27.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_27.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_27, CType(27, Short))
        Me._lblLabels_27.Location = New System.Drawing.Point(66, 179)
        Me._lblLabels_27.Name = "_lblLabels_27"
        Me._lblLabels_27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_27.Size = New System.Drawing.Size(80, 13)
        Me._lblLabels_27.TabIndex = 59
        Me._lblLabels_27.Text = "GST Regn No :"
        Me._lblLabels_27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_26
        '
        Me._lblLabels_26.AutoSize = True
        Me._lblLabels_26.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_26.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_26, CType(26, Short))
        Me._lblLabels_26.Location = New System.Drawing.Point(45, 207)
        Me._lblLabels_26.Name = "_lblLabels_26"
        Me._lblLabels_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_26.Size = New System.Drawing.Size(102, 13)
        Me._lblLabels_26.TabIndex = 58
        Me._lblLabels_26.Text = "Commissionerate :"
        Me._lblLabels_26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_25
        '
        Me._lblLabels_25.AutoSize = True
        Me._lblLabels_25.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_25.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_25, CType(25, Short))
        Me._lblLabels_25.Location = New System.Drawing.Point(42, 434)
        Me._lblLabels_25.Name = "_lblLabels_25"
        Me._lblLabels_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_25.Size = New System.Drawing.Size(99, 13)
        Me._lblLabels_25.TabIndex = 57
        Me._lblLabels_25.Text = "Excise Exempted :"
        Me._lblLabels_25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_24
        '
        Me._lblLabels_24.AutoSize = True
        Me._lblLabels_24.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_24.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_24, CType(24, Short))
        Me._lblLabels_24.Location = New System.Drawing.Point(97, 405)
        Me._lblLabels_24.Name = "_lblLabels_24"
        Me._lblLabels_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_24.Size = New System.Drawing.Size(49, 13)
        Me._lblLabels_24.TabIndex = 55
        Me._lblLabels_24.Text = "CIN No :"
        Me._lblLabels_24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_23
        '
        Me._lblLabels_23.AutoSize = True
        Me._lblLabels_23.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_23.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_23, CType(23, Short))
        Me._lblLabels_23.Location = New System.Drawing.Point(362, 404)
        Me._lblLabels_23.Name = "_lblLabels_23"
        Me._lblLabels_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_23.Size = New System.Drawing.Size(51, 13)
        Me._lblLabels_23.TabIndex = 54
        Me._lblLabels_23.Text = "TAN No :"
        Me._lblLabels_23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_22
        '
        Me._lblLabels_22.AutoSize = True
        Me._lblLabels_22.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_22.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_22, CType(22, Short))
        Me._lblLabels_22.Location = New System.Drawing.Point(346, 289)
        Me._lblLabels_22.Name = "_lblLabels_22"
        Me._lblLabels_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_22.Size = New System.Drawing.Size(70, 13)
        Me._lblLabels_22.TabIndex = 53
        Me._lblLabels_22.Text = "Jurisdiction :"
        Me._lblLabels_22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_21
        '
        Me._lblLabels_21.AutoSize = True
        Me._lblLabels_21.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_21.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_21, CType(21, Short))
        Me._lblLabels_21.Location = New System.Drawing.Point(368, 234)
        Me._lblLabels_21.Name = "_lblLabels_21"
        Me._lblLabels_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_21.Size = New System.Drawing.Size(48, 13)
        Me._lblLabels_21.TabIndex = 52
        Me._lblLabels_21.Text = "TIN No :"
        Me._lblLabels_21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_20
        '
        Me._lblLabels_20.AutoSize = True
        Me._lblLabels_20.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_20.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_20.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_20, CType(20, Short))
        Me._lblLabels_20.Location = New System.Drawing.Point(417, 434)
        Me._lblLabels_20.Name = "_lblLabels_20"
        Me._lblLabels_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_20.Size = New System.Drawing.Size(68, 13)
        Me._lblLabels_20.TabIndex = 51
        Me._lblLabels_20.Text = "E.O.U. Unit :"
        Me._lblLabels_20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_19
        '
        Me._lblLabels_19.AutoSize = True
        Me._lblLabels_19.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_19.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_19, CType(19, Short))
        Me._lblLabels_19.Location = New System.Drawing.Point(98, 378)
        Me._lblLabels_19.Name = "_lblLabels_19"
        Me._lblLabels_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_19.Size = New System.Drawing.Size(47, 13)
        Me._lblLabels_19.TabIndex = 49
        Me._lblLabels_19.Text = "IEC No :"
        Me._lblLabels_19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_17
        '
        Me._lblLabels_17.AutoSize = True
        Me._lblLabels_17.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_17.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_17, CType(17, Short))
        Me._lblLabels_17.Location = New System.Drawing.Point(365, 318)
        Me._lblLabels_17.Name = "_lblLabels_17"
        Me._lblLabels_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_17.Size = New System.Drawing.Size(51, 13)
        Me._lblLabels_17.TabIndex = 44
        Me._lblLabels_17.Text = "ECC No :"
        Me._lblLabels_17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_16
        '
        Me._lblLabels_16.AutoSize = True
        Me._lblLabels_16.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_16.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_16, CType(16, Short))
        Me._lblLabels_16.Location = New System.Drawing.Point(53, 318)
        Me._lblLabels_16.Name = "_lblLabels_16"
        Me._lblLabels_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_16.Size = New System.Drawing.Size(93, 13)
        Me._lblLabels_16.TabIndex = 43
        Me._lblLabels_16.Text = "Registration No :"
        Me._lblLabels_16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_15
        '
        Me._lblLabels_15.AutoSize = True
        Me._lblLabels_15.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_15.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_15, CType(15, Short))
        Me._lblLabels_15.Location = New System.Drawing.Point(98, 262)
        Me._lblLabels_15.Name = "_lblLabels_15"
        Me._lblLabels_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_15.Size = New System.Drawing.Size(46, 13)
        Me._lblLabels_15.TabIndex = 42
        Me._lblLabels_15.Text = "PF Est. :"
        Me._lblLabels_15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_13
        '
        Me._lblLabels_13.AutoSize = True
        Me._lblLabels_13.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_13.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_13, CType(13, Short))
        Me._lblLabels_13.Location = New System.Drawing.Point(368, 263)
        Me._lblLabels_13.Name = "_lblLabels_13"
        Me._lblLabels_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_13.Size = New System.Drawing.Size(49, 13)
        Me._lblLabels_13.TabIndex = 40
        Me._lblLabels_13.Text = "ESI Est. :"
        Me._lblLabels_13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_10
        '
        Me._lblLabels_10.AutoSize = True
        Me._lblLabels_10.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_10, CType(10, Short))
        Me._lblLabels_10.Location = New System.Drawing.Point(386, 98)
        Me._lblLabels_10.Name = "_lblLabels_10"
        Me._lblLabels_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_10.Size = New System.Drawing.Size(28, 13)
        Me._lblLabels_10.TabIndex = 37
        Me._lblLabels_10.Text = "Pin :"
        Me._lblLabels_10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_9
        '
        Me._lblLabels_9.AutoSize = True
        Me._lblLabels_9.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_9, CType(9, Short))
        Me._lblLabels_9.Location = New System.Drawing.Point(108, 97)
        Me._lblLabels_9.Name = "_lblLabels_9"
        Me._lblLabels_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_9.Size = New System.Drawing.Size(39, 13)
        Me._lblLabels_9.TabIndex = 36
        Me._lblLabels_9.Text = "State :"
        Me._lblLabels_9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(114, 69)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(33, 13)
        Me._lblLabels_2.TabIndex = 35
        Me._lblLabels_2.Text = "City :"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.AutoSize = True
        Me._lblLabels_7.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_7, CType(7, Short))
        Me._lblLabels_7.Location = New System.Drawing.Point(93, 235)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(51, 13)
        Me._lblLabels_7.TabIndex = 34
        Me._lblLabels_7.Text = "TDS No :"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.AutoSize = True
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_6, CType(6, Short))
        Me._lblLabels_6.Location = New System.Drawing.Point(385, 125)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(31, 13)
        Me._lblLabels_6.TabIndex = 33
        Me._lblLabels_6.Text = "Fax :"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.AutoSize = True
        Me._lblLabels_4.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_4, CType(4, Short))
        Me._lblLabels_4.Location = New System.Drawing.Point(102, 125)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(44, 13)
        Me._lblLabels_4.TabIndex = 32
        Me._lblLabels_4.Text = "Phone :"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(92, 42)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(54, 13)
        Me._lblLabels_1.TabIndex = 31
        Me._lblLabels_1.Text = "Address :"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me._lblLabels_0.Size = New System.Drawing.Size(95, 13)
        Me._lblLabels_0.TabIndex = 30
        Me._lblLabels_0.Text = "Company Name :"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.AutoSize = True
        Me._lblLabels_5.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_5, CType(5, Short))
        Me._lblLabels_5.Location = New System.Drawing.Point(104, 153)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(41, 13)
        Me._lblLabels_5.TabIndex = 29
        Me._lblLabels_5.Text = "Email :"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = True
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblLabels.SetIndex(Me._lblLabels_3, CType(3, Short))
        Me._lblLabels_3.Location = New System.Drawing.Point(73, 290)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(74, 13)
        Me._lblLabels_3.TabIndex = 28
        Me._lblLabels_3.Text = "PAN/GIR No :"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 469)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(620, 55)
        Me.FraMovement.TabIndex = 25
        Me.FraMovement.TabStop = False
        '
        'ADataGrid
        '
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.CommandTimeout = 0
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.ConnectionString = Nothing
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Location = New System.Drawing.Point(88, 80)
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.Name = "ADataGrid"
        Me.ADataGrid.Size = New System.Drawing.Size(99, 27)
        Me.ADataGrid.TabIndex = 27
        Me.ADataGrid.Text = "Adodc1"
        Me.ADataGrid.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(23, 347)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(121, 13)
        Me.Label1.TabIndex = 60
        Me.Label1.Text = "Print Company Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPrintCompanyName
        '
        Me.txtPrintCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrintCompanyName.Location = New System.Drawing.Point(156, 347)
        Me.txtPrintCompanyName.Name = "txtPrintCompanyName"
        Me.txtPrintCompanyName.Size = New System.Drawing.Size(437, 22)
        Me.txtPrintCompanyName.TabIndex = 61
        '
        'frmAddCompany
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(632, 528)
        Me.Controls.Add(Me.FraCompany)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.ADataGrid)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddCompany"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Company"
        Me.FraCompany.ResumeLayout(False)
        Me.FraCompany.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents txtPrintCompanyName As TextBox
    Public WithEvents Label1 As Label
#End Region
End Class