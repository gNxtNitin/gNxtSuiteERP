Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPOExchangeRate
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
        '
        ''InventoryGST.Master.Show
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
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Public WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents txtPODate As System.Windows.Forms.TextBox
    Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtAmendDate As System.Windows.Forms.TextBox
    Public WithEvents chkStatus As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchPO As System.Windows.Forms.Button
    Public WithEvents ChkActivate As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchAmend As System.Windows.Forms.Button
    Public WithEvents TxtExchangeRate As System.Windows.Forms.TextBox
    Public WithEvents txtDivision As System.Windows.Forms.TextBox
    Public WithEvents chkModvatable As System.Windows.Forms.CheckBox
    Public WithEvents chkSTRefundable As System.Windows.Forms.CheckBox
    Public WithEvents chkCapital As System.Windows.Forms.CheckBox
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents cmdServProvided As System.Windows.Forms.Button
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblDivision As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents lblPOType As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPOExchangeRate))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchPO = New System.Windows.Forms.Button()
        Me.cmdSearchAmend = New System.Windows.Forms.Button()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.cmdServProvided = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtPODate = New System.Windows.Forms.TextBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.txtAmendDate = New System.Windows.Forms.TextBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.ChkActivate = New System.Windows.Forms.CheckBox()
        Me.TxtExchangeRate = New System.Windows.Forms.TextBox()
        Me.txtDivision = New System.Windows.Forms.TextBox()
        Me.chkModvatable = New System.Windows.Forms.CheckBox()
        Me.chkSTRefundable = New System.Windows.Forms.CheckBox()
        Me.chkCapital = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblDivision = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblPOType = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.fraTop1.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchPO
        '
        Me.cmdSearchPO.AutoSize = True
        Me.cmdSearchPO.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPO.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPO.Image = CType(resources.GetObject("cmdSearchPO.Image"), System.Drawing.Image)
        Me.cmdSearchPO.Location = New System.Drawing.Point(206, 7)
        Me.cmdSearchPO.Name = "cmdSearchPO"
        Me.cmdSearchPO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPO.Size = New System.Drawing.Size(32, 26)
        Me.cmdSearchPO.TabIndex = 15
        Me.cmdSearchPO.TabStop = False
        Me.cmdSearchPO.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPO, "Search")
        Me.cmdSearchPO.UseVisualStyleBackColor = False
        '
        'cmdSearchAmend
        '
        Me.cmdSearchAmend.AutoSize = True
        Me.cmdSearchAmend.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAmend.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAmend.Image = CType(resources.GetObject("cmdSearchAmend.Image"), System.Drawing.Image)
        Me.cmdSearchAmend.Location = New System.Drawing.Point(562, 7)
        Me.cmdSearchAmend.Name = "cmdSearchAmend"
        Me.cmdSearchAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAmend.Size = New System.Drawing.Size(32, 26)
        Me.cmdSearchAmend.TabIndex = 13
        Me.cmdSearchAmend.TabStop = False
        Me.cmdSearchAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAmend, "Search")
        Me.cmdSearchAmend.UseVisualStyleBackColor = False
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(112, 122)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(397, 22)
        Me.txtServProvided.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtServProvided, "Press F1 For Help")
        '
        'cmdServProvided
        '
        Me.cmdServProvided.AutoSize = True
        Me.cmdServProvided.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdServProvided.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdServProvided.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdServProvided.Image = CType(resources.GetObject("cmdServProvided.Image"), System.Drawing.Image)
        Me.cmdServProvided.Location = New System.Drawing.Point(510, 120)
        Me.cmdServProvided.Name = "cmdServProvided"
        Me.cmdServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdServProvided.Size = New System.Drawing.Size(32, 26)
        Me.cmdServProvided.TabIndex = 6
        Me.cmdServProvided.TabStop = False
        Me.cmdServProvided.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdServProvided, "Search")
        Me.cmdServProvided.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(78, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 33
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(582, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 0
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtCode)
        Me.fraTop1.Controls.Add(Me.txtSupplierName)
        Me.fraTop1.Controls.Add(Me.txtPONo)
        Me.fraTop1.Controls.Add(Me.txtPODate)
        Me.fraTop1.Controls.Add(Me.txtAmendNo)
        Me.fraTop1.Controls.Add(Me.txtAmendDate)
        Me.fraTop1.Controls.Add(Me.chkStatus)
        Me.fraTop1.Controls.Add(Me.cmdSearchPO)
        Me.fraTop1.Controls.Add(Me.ChkActivate)
        Me.fraTop1.Controls.Add(Me.cmdSearchAmend)
        Me.fraTop1.Controls.Add(Me.TxtExchangeRate)
        Me.fraTop1.Controls.Add(Me.txtDivision)
        Me.fraTop1.Controls.Add(Me.chkModvatable)
        Me.fraTop1.Controls.Add(Me.chkSTRefundable)
        Me.fraTop1.Controls.Add(Me.chkCapital)
        Me.fraTop1.Controls.Add(Me.txtServProvided)
        Me.fraTop1.Controls.Add(Me.cmdServProvided)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label26)
        Me.fraTop1.Controls.Add(Me.lblDivision)
        Me.fraTop1.Controls.Add(Me.Label29)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -2)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(751, 150)
        Me.fraTop1.TabIndex = 5
        Me.fraTop1.TabStop = False
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.Enabled = False
        Me.txtCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.Blue
        Me.txtCode.Location = New System.Drawing.Point(500, 38)
        Me.txtCode.MaxLength = 0
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.Size = New System.Drawing.Size(61, 22)
        Me.txtCode.TabIndex = 22
        '
        'txtSupplierName
        '
        Me.txtSupplierName.AcceptsReturn = True
        Me.txtSupplierName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplierName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplierName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplierName.Enabled = False
        Me.txtSupplierName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplierName.ForeColor = System.Drawing.Color.Blue
        Me.txtSupplierName.Location = New System.Drawing.Point(112, 38)
        Me.txtSupplierName.MaxLength = 0
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplierName.Size = New System.Drawing.Size(281, 22)
        Me.txtSupplierName.TabIndex = 21
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPONo.Location = New System.Drawing.Point(112, 10)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(93, 22)
        Me.txtPONo.TabIndex = 20
        '
        'txtPODate
        '
        Me.txtPODate.AcceptsReturn = True
        Me.txtPODate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPODate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPODate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPODate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPODate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPODate.Location = New System.Drawing.Point(317, 10)
        Me.txtPODate.MaxLength = 0
        Me.txtPODate.Name = "txtPODate"
        Me.txtPODate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPODate.Size = New System.Drawing.Size(75, 22)
        Me.txtPODate.TabIndex = 19
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmendNo.Location = New System.Drawing.Point(500, 10)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(61, 22)
        Me.txtAmendNo.TabIndex = 18
        '
        'txtAmendDate
        '
        Me.txtAmendDate.AcceptsReturn = True
        Me.txtAmendDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmendDate.Location = New System.Drawing.Point(662, 10)
        Me.txtAmendDate.MaxLength = 0
        Me.txtAmendDate.Name = "txtAmendDate"
        Me.txtAmendDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendDate.Size = New System.Drawing.Size(75, 22)
        Me.txtAmendDate.TabIndex = 17
        '
        'chkStatus
        '
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(545, 78)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(207, 16)
        Me.chkStatus.TabIndex = 16
        Me.chkStatus.Text = "Post Status (Posted/ Unposted)"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'ChkActivate
        '
        Me.ChkActivate.BackColor = System.Drawing.SystemColors.Control
        Me.ChkActivate.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkActivate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkActivate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkActivate.Location = New System.Drawing.Point(545, 62)
        Me.ChkActivate.Name = "ChkActivate"
        Me.ChkActivate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkActivate.Size = New System.Drawing.Size(191, 16)
        Me.ChkActivate.TabIndex = 14
        Me.ChkActivate.Text = "Closed Staus (Yes / No)"
        Me.ChkActivate.UseVisualStyleBackColor = False
        '
        'TxtExchangeRate
        '
        Me.TxtExchangeRate.AcceptsReturn = True
        Me.TxtExchangeRate.BackColor = System.Drawing.SystemColors.Window
        Me.TxtExchangeRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtExchangeRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtExchangeRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExchangeRate.ForeColor = System.Drawing.Color.Blue
        Me.TxtExchangeRate.Location = New System.Drawing.Point(112, 94)
        Me.TxtExchangeRate.MaxLength = 0
        Me.TxtExchangeRate.Name = "TxtExchangeRate"
        Me.TxtExchangeRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtExchangeRate.Size = New System.Drawing.Size(61, 22)
        Me.TxtExchangeRate.TabIndex = 12
        '
        'txtDivision
        '
        Me.txtDivision.AcceptsReturn = True
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDivision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivision.Enabled = False
        Me.txtDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivision.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDivision.Location = New System.Drawing.Point(112, 66)
        Me.txtDivision.MaxLength = 0
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivision.Size = New System.Drawing.Size(79, 22)
        Me.txtDivision.TabIndex = 11
        '
        'chkModvatable
        '
        Me.chkModvatable.BackColor = System.Drawing.SystemColors.Control
        Me.chkModvatable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkModvatable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkModvatable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkModvatable.Location = New System.Drawing.Point(545, 94)
        Me.chkModvatable.Name = "chkModvatable"
        Me.chkModvatable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkModvatable.Size = New System.Drawing.Size(191, 16)
        Me.chkModvatable.TabIndex = 10
        Me.chkModvatable.Text = "Modvatable (Yes / No)"
        Me.chkModvatable.UseVisualStyleBackColor = False
        Me.chkModvatable.Visible = False
        '
        'chkSTRefundable
        '
        Me.chkSTRefundable.BackColor = System.Drawing.SystemColors.Control
        Me.chkSTRefundable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSTRefundable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSTRefundable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSTRefundable.Location = New System.Drawing.Point(545, 110)
        Me.chkSTRefundable.Name = "chkSTRefundable"
        Me.chkSTRefundable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSTRefundable.Size = New System.Drawing.Size(191, 16)
        Me.chkSTRefundable.TabIndex = 9
        Me.chkSTRefundable.Text = "ST Refundable (Yes / No)"
        Me.chkSTRefundable.UseVisualStyleBackColor = False
        Me.chkSTRefundable.Visible = False
        '
        'chkCapital
        '
        Me.chkCapital.BackColor = System.Drawing.SystemColors.Control
        Me.chkCapital.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCapital.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCapital.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCapital.Location = New System.Drawing.Point(545, 126)
        Me.chkCapital.Name = "chkCapital"
        Me.chkCapital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCapital.Size = New System.Drawing.Size(191, 16)
        Me.chkCapital.TabIndex = 8
        Me.chkCapital.Text = "Capital (Yes / No)"
        Me.chkCapital.UseVisualStyleBackColor = False
        Me.chkCapital.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(455, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Code :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(51, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Supplier :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(52, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Number :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(275, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(425, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Amend No. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(621, 12)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(37, 13)
        Me.Label10.TabIndex = 27
        Me.Label10.Text = "Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(19, 98)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(87, 13)
        Me.Label26.TabIndex = 26
        Me.Label26.Text = "Exchange Rate :"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDivision
        '
        Me.lblDivision.BackColor = System.Drawing.Color.Transparent
        Me.lblDivision.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivision.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivision.Location = New System.Drawing.Point(192, 66)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDivision.Size = New System.Drawing.Size(203, 22)
        Me.lblDivision.TabIndex = 25
        Me.lblDivision.Text = "lblDivision"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(52, 70)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(54, 13)
        Me.Label29.TabIndex = 24
        Me.Label29.Text = "Division :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(11, 126)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(95, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Service Provider :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.lblPOType)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 144)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(751, 51)
        Me.FraMovement.TabIndex = 1
        Me.FraMovement.TabStop = False
        '
        'lblPOType
        '
        Me.lblPOType.BackColor = System.Drawing.SystemColors.Control
        Me.lblPOType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPOType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPOType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPOType.Location = New System.Drawing.Point(10, 28)
        Me.lblPOType.Name = "lblPOType"
        Me.lblPOType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPOType.Size = New System.Drawing.Size(71, 17)
        Me.lblPOType.TabIndex = 4
        Me.lblPOType.Text = "lblPOType"
        Me.lblPOType.Visible = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(684, 16)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(47, 21)
        Me.lblBookType.TabIndex = 3
        Me.lblBookType.Text = "lblBookType"
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(10, 18)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 2
        Me.lblMkey.Text = "lblMkey"
        Me.lblMkey.Visible = False
        '
        'frmPOExchangeRate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(752, 199)
        Me.Controls.Add(Me.fraTop1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPOExchangeRate"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Exchange Rate Change in PO"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class