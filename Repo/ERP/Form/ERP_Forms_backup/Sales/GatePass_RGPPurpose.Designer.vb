Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGatePassPurpose
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

        'InventoryGST.Master.Show
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
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtSuppcode As System.Windows.Forms.TextBox
    Public WithEvents txtSuppName As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents cboPurpose As System.Windows.Forms.ComboBox
    Public WithEvents txtRgpreqno As System.Windows.Forms.TextBox
    Public WithEvents txtGatepassno As System.Windows.Forms.TextBox
    Public WithEvents txtRgpreqdate As System.Windows.Forms.TextBox
    Public WithEvents txtGatePassDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblCust As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGatePassPurpose))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.txtSuppcode = New System.Windows.Forms.TextBox()
        Me.txtSuppName = New System.Windows.Forms.TextBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.cboPurpose = New System.Windows.Forms.ComboBox()
        Me.txtRgpreqno = New System.Windows.Forms.TextBox()
        Me.txtGatepassno = New System.Windows.Forms.TextBox()
        Me.txtRgpreqdate = New System.Windows.Forms.TextBox()
        Me.txtGatePassDate = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblCust = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.FraFront.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(214, 13)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearch.TabIndex = 4
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(826, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(6, 12)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 0
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.cboDivision)
        Me.FraFront.Controls.Add(Me.txtSuppcode)
        Me.FraFront.Controls.Add(Me.txtSuppName)
        Me.FraFront.Controls.Add(Me.Frame6)
        Me.FraFront.Controls.Add(Me.cboPurpose)
        Me.FraFront.Controls.Add(Me.txtRgpreqno)
        Me.FraFront.Controls.Add(Me.txtGatepassno)
        Me.FraFront.Controls.Add(Me.txtRgpreqdate)
        Me.FraFront.Controls.Add(Me.txtGatePassDate)
        Me.FraFront.Controls.Add(Me.cmdSearch)
        Me.FraFront.Controls.Add(Me.Label20)
        Me.FraFront.Controls.Add(Me.Label2)
        Me.FraFront.Controls.Add(Me.Label21)
        Me.FraFront.Controls.Add(Me.Label10)
        Me.FraFront.Controls.Add(Me.Label9)
        Me.FraFront.Controls.Add(Me.lblCust)
        Me.FraFront.Controls.Add(Me.Label7)
        Me.FraFront.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -2)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(906, 572)
        Me.FraFront.TabIndex = 3
        Me.FraFront.TabStop = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(120, 103)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(231, 21)
        Me.cboDivision.TabIndex = 20
        '
        'txtSuppcode
        '
        Me.txtSuppcode.AcceptsReturn = True
        Me.txtSuppcode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppcode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppcode.Enabled = False
        Me.txtSuppcode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppcode.ForeColor = System.Drawing.Color.Blue
        Me.txtSuppcode.Location = New System.Drawing.Point(120, 73)
        Me.txtSuppcode.MaxLength = 0
        Me.txtSuppcode.Name = "txtSuppcode"
        Me.txtSuppcode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppcode.Size = New System.Drawing.Size(93, 22)
        Me.txtSuppcode.TabIndex = 18
        Me.txtSuppcode.Text = " "
        '
        'txtSuppName
        '
        Me.txtSuppName.AcceptsReturn = True
        Me.txtSuppName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppName.Enabled = False
        Me.txtSuppName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppName.ForeColor = System.Drawing.Color.Blue
        Me.txtSuppName.Location = New System.Drawing.Point(236, 73)
        Me.txtSuppName.MaxLength = 0
        Me.txtSuppName.Name = "txtSuppName"
        Me.txtSuppName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppName.Size = New System.Drawing.Size(293, 22)
        Me.txtSuppName.TabIndex = 17
        Me.txtSuppName.Text = " "
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.SprdMain)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 154)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(906, 418)
        Me.Frame6.TabIndex = 15
        Me.Frame6.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(904, 406)
        Me.SprdMain.TabIndex = 16
        '
        'cboPurpose
        '
        Me.cboPurpose.BackColor = System.Drawing.SystemColors.Window
        Me.cboPurpose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPurpose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurpose.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPurpose.Location = New System.Drawing.Point(120, 133)
        Me.cboPurpose.Name = "cboPurpose"
        Me.cboPurpose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPurpose.Size = New System.Drawing.Size(231, 21)
        Me.cboPurpose.TabIndex = 9
        '
        'txtRgpreqno
        '
        Me.txtRgpreqno.AcceptsReturn = True
        Me.txtRgpreqno.BackColor = System.Drawing.SystemColors.Window
        Me.txtRgpreqno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRgpreqno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRgpreqno.Enabled = False
        Me.txtRgpreqno.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRgpreqno.ForeColor = System.Drawing.Color.Blue
        Me.txtRgpreqno.Location = New System.Drawing.Point(120, 43)
        Me.txtRgpreqno.MaxLength = 0
        Me.txtRgpreqno.Name = "txtRgpreqno"
        Me.txtRgpreqno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRgpreqno.Size = New System.Drawing.Size(93, 22)
        Me.txtRgpreqno.TabIndex = 8
        Me.txtRgpreqno.Text = " "
        '
        'txtGatepassno
        '
        Me.txtGatepassno.AcceptsReturn = True
        Me.txtGatepassno.BackColor = System.Drawing.SystemColors.Window
        Me.txtGatepassno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGatepassno.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGatepassno.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGatepassno.ForeColor = System.Drawing.Color.Blue
        Me.txtGatepassno.Location = New System.Drawing.Point(120, 13)
        Me.txtGatepassno.MaxLength = 0
        Me.txtGatepassno.Name = "txtGatepassno"
        Me.txtGatepassno.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGatepassno.Size = New System.Drawing.Size(93, 22)
        Me.txtGatepassno.TabIndex = 7
        '
        'txtRgpreqdate
        '
        Me.txtRgpreqdate.AcceptsReturn = True
        Me.txtRgpreqdate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRgpreqdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRgpreqdate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRgpreqdate.Enabled = False
        Me.txtRgpreqdate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRgpreqdate.ForeColor = System.Drawing.Color.Blue
        Me.txtRgpreqdate.Location = New System.Drawing.Point(446, 43)
        Me.txtRgpreqdate.MaxLength = 0
        Me.txtRgpreqdate.Name = "txtRgpreqdate"
        Me.txtRgpreqdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRgpreqdate.Size = New System.Drawing.Size(83, 22)
        Me.txtRgpreqdate.TabIndex = 6
        Me.txtRgpreqdate.Text = " "
        '
        'txtGatePassDate
        '
        Me.txtGatePassDate.AcceptsReturn = True
        Me.txtGatePassDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtGatePassDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGatePassDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGatePassDate.Enabled = False
        Me.txtGatePassDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGatePassDate.ForeColor = System.Drawing.Color.Blue
        Me.txtGatePassDate.Location = New System.Drawing.Point(446, 13)
        Me.txtGatePassDate.MaxLength = 0
        Me.txtGatePassDate.Name = "txtGatePassDate"
        Me.txtGatePassDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGatePassDate.Size = New System.Drawing.Size(83, 22)
        Me.txtGatePassDate.TabIndex = 5
        Me.txtGatePassDate.Text = " "
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(61, 107)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(54, 13)
        Me.Label20.TabIndex = 21
        Me.Label20.Text = "Division :"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(30, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(85, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Supplier Code :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(43, 137)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(72, 13)
        Me.Label21.TabIndex = 14
        Me.Label21.Text = "Purpose for :"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(405, 45)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(37, 13)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(34, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(81, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "RGP Req. No. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCust
        '
        Me.lblCust.AutoSize = True
        Me.lblCust.BackColor = System.Drawing.SystemColors.Control
        Me.lblCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCust.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCust.Location = New System.Drawing.Point(32, 16)
        Me.lblCust.Name = "lblCust"
        Me.lblCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCust.Size = New System.Drawing.Size(83, 13)
        Me.lblCust.TabIndex = 11
        Me.lblCust.Text = "Gate Pass No. :"
        Me.lblCust.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(405, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Date :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 566)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(906, 53)
        Me.Frame3.TabIndex = 2
        Me.Frame3.TabStop = False
        '
        'frmGatePassPurpose
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(906, 621)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGatePassPurpose"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Gate Pass - Change Purpose After Quotation"
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class