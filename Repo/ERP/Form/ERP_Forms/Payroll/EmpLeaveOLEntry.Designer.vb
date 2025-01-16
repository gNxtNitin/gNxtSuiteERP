Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmpLeaveOLEntry
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
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
    Public WithEvents lblAvlCPL As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblAvlSL As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblAvlEL As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblAvlCL As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents lblBalCPL As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblBalCL As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblBalEL As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblBalSL As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents TxtEmpName As System.Windows.Forms.TextBox
    Public WithEvents txtReason As System.Windows.Forms.TextBox
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents txtDesg As System.Windows.Forms.TextBox
    Public WithEvents txtRecEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtRecEmpName As System.Windows.Forms.TextBox
    Public WithEvents txtAppEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtAppEmpName As System.Windows.Forms.TextBox
    Public WithEvents txtLeaveFrom As System.Windows.Forms.TextBox
    Public WithEvents txtLeaveTo As System.Windows.Forms.TextBox
    Public WithEvents txtLDays As System.Windows.Forms.TextBox
    Public WithEvents txtRefDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpLeaveOLEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.lblAvlCPL = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblAvlSL = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblAvlEL = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblAvlCL = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.lblBalCPL = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBalCL = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblBalEL = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblBalSL = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtEmpName = New System.Windows.Forms.TextBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.txtDesg = New System.Windows.Forms.TextBox()
        Me.txtRecEmpCode = New System.Windows.Forms.TextBox()
        Me.txtRecEmpName = New System.Windows.Forms.TextBox()
        Me.txtAppEmpCode = New System.Windows.Forms.TextBox()
        Me.txtAppEmpName = New System.Windows.Forms.TextBox()
        Me.txtLeaveFrom = New System.Windows.Forms.TextBox()
        Me.txtLeaveTo = New System.Windows.Forms.TextBox()
        Me.txtLDays = New System.Windows.Forms.TextBox()
        Me.txtRefDate = New System.Windows.Forms.MaskedTextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.FraView.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(4, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
        Me.CmdSave.TabIndex = 19
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(214, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 16
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Close the Form")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(484, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 18
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(274, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 17
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Controls.Add(Me.Frame2)
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.TxtEmpName)
        Me.FraView.Controls.Add(Me.txtReason)
        Me.FraView.Controls.Add(Me.txtDept)
        Me.FraView.Controls.Add(Me.txtEmpCode)
        Me.FraView.Controls.Add(Me.txtRefNo)
        Me.FraView.Controls.Add(Me.txtDesg)
        Me.FraView.Controls.Add(Me.txtRecEmpCode)
        Me.FraView.Controls.Add(Me.txtRecEmpName)
        Me.FraView.Controls.Add(Me.txtAppEmpCode)
        Me.FraView.Controls.Add(Me.txtAppEmpName)
        Me.FraView.Controls.Add(Me.txtLeaveFrom)
        Me.FraView.Controls.Add(Me.txtLeaveTo)
        Me.FraView.Controls.Add(Me.txtLDays)
        Me.FraView.Controls.Add(Me.txtRefDate)
        Me.FraView.Controls.Add(Me.SprdMain)
        Me.FraView.Controls.Add(Me.Label5)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Controls.Add(Me.Label3)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Controls.Add(Me.Label14)
        Me.FraView.Controls.Add(Me.Label15)
        Me.FraView.Controls.Add(Me.Label16)
        Me.FraView.Controls.Add(Me.Label17)
        Me.FraView.Controls.Add(Me.Label9)
        Me.FraView.Controls.Add(Me.Label18)
        Me.FraView.Controls.Add(Me.Label19)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -4)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(549, 423)
        Me.FraView.TabIndex = 20
        Me.FraView.TabStop = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lblAvlCPL)
        Me.Frame2.Controls.Add(Me.Label10)
        Me.Frame2.Controls.Add(Me.Label11)
        Me.Frame2.Controls.Add(Me.lblAvlSL)
        Me.Frame2.Controls.Add(Me.Label12)
        Me.Frame2.Controls.Add(Me.lblAvlEL)
        Me.Frame2.Controls.Add(Me.Label13)
        Me.Frame2.Controls.Add(Me.lblAvlCL)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(274, 380)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(273, 41)
        Me.Frame2.TabIndex = 30
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Leave Availed (Till Month)"
        '
        'lblAvlCPL
        '
        Me.lblAvlCPL.BackColor = System.Drawing.SystemColors.Control
        Me.lblAvlCPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlCPL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlCPL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlCPL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlCPL.Location = New System.Drawing.Point(230, 16)
        Me.lblAvlCPL.Name = "lblAvlCPL"
        Me.lblAvlCPL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlCPL.Size = New System.Drawing.Size(29, 15)
        Me.lblAvlCPL.TabIndex = 38
        Me.lblAvlCPL.Text = "0"
        Me.lblAvlCPL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(195, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(27, 13)
        Me.Label10.TabIndex = 37
        Me.Label10.Text = "CPL :"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(3, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(27, 13)
        Me.Label11.TabIndex = 36
        Me.Label11.Text = "CL :"
        '
        'lblAvlSL
        '
        Me.lblAvlSL.BackColor = System.Drawing.SystemColors.Control
        Me.lblAvlSL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlSL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlSL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlSL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlSL.Location = New System.Drawing.Point(92, 16)
        Me.lblAvlSL.Name = "lblAvlSL"
        Me.lblAvlSL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlSL.Size = New System.Drawing.Size(29, 15)
        Me.lblAvlSL.TabIndex = 35
        Me.lblAvlSL.Text = "0"
        Me.lblAvlSL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(65, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(27, 13)
        Me.Label12.TabIndex = 34
        Me.Label12.Text = "SL :"
        '
        'lblAvlEL
        '
        Me.lblAvlEL.BackColor = System.Drawing.SystemColors.Control
        Me.lblAvlEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlEL.Location = New System.Drawing.Point(154, 16)
        Me.lblAvlEL.Name = "lblAvlEL"
        Me.lblAvlEL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlEL.Size = New System.Drawing.Size(37, 15)
        Me.lblAvlEL.TabIndex = 33
        Me.lblAvlEL.Text = "0"
        Me.lblAvlEL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(127, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(27, 13)
        Me.Label13.TabIndex = 32
        Me.Label13.Text = "EL :"
        '
        'lblAvlCL
        '
        Me.lblAvlCL.BackColor = System.Drawing.SystemColors.Control
        Me.lblAvlCL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlCL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlCL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlCL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlCL.Location = New System.Drawing.Point(30, 16)
        Me.lblAvlCL.Name = "lblAvlCL"
        Me.lblAvlCL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlCL.Size = New System.Drawing.Size(29, 15)
        Me.lblAvlCL.TabIndex = 31
        Me.lblAvlCL.Text = "0"
        Me.lblAvlCL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.lblBalCPL)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.lblBalCL)
        Me.Frame1.Controls.Add(Me.Label6)
        Me.Frame1.Controls.Add(Me.lblBalEL)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.lblBalSL)
        Me.Frame1.Controls.Add(Me.Label8)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 380)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(273, 41)
        Me.Frame1.TabIndex = 21
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Balance Leave (Including This Month)"
        '
        'lblBalCPL
        '
        Me.lblBalCPL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalCPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalCPL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalCPL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalCPL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalCPL.Location = New System.Drawing.Point(236, 16)
        Me.lblBalCPL.Name = "lblBalCPL"
        Me.lblBalCPL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalCPL.Size = New System.Drawing.Size(29, 15)
        Me.lblBalCPL.TabIndex = 29
        Me.lblBalCPL.Text = "0"
        Me.lblBalCPL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(201, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(32, 14)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "CPL :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBalCL
        '
        Me.lblBalCL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalCL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalCL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalCL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalCL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalCL.Location = New System.Drawing.Point(30, 16)
        Me.lblBalCL.Name = "lblBalCL"
        Me.lblBalCL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalCL.Size = New System.Drawing.Size(29, 15)
        Me.lblBalCL.TabIndex = 27
        Me.lblBalCL.Text = "0"
        Me.lblBalCL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(127, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(27, 13)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "EL :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBalEL
        '
        Me.lblBalEL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalEL.Location = New System.Drawing.Point(154, 16)
        Me.lblBalEL.Name = "lblBalEL"
        Me.lblBalEL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalEL.Size = New System.Drawing.Size(39, 15)
        Me.lblBalEL.TabIndex = 25
        Me.lblBalEL.Text = "0"
        Me.lblBalEL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(65, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(27, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "SL :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBalSL
        '
        Me.lblBalSL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalSL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalSL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalSL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalSL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalSL.Location = New System.Drawing.Point(92, 16)
        Me.lblBalSL.Name = "lblBalSL"
        Me.lblBalSL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalSL.Size = New System.Drawing.Size(29, 15)
        Me.lblBalSL.TabIndex = 23
        Me.lblBalSL.Text = "0"
        Me.lblBalSL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(3, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(27, 13)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "CL :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TxtEmpName
        '
        Me.TxtEmpName.AcceptsReturn = True
        Me.TxtEmpName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEmpName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtEmpName.Enabled = False
        Me.TxtEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmpName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtEmpName.Location = New System.Drawing.Point(184, 32)
        Me.TxtEmpName.MaxLength = 0
        Me.TxtEmpName.Name = "TxtEmpName"
        Me.TxtEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtEmpName.Size = New System.Drawing.Size(359, 20)
        Me.TxtEmpName.TabIndex = 4
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Enabled = False
        Me.txtReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReason.Location = New System.Drawing.Point(96, 132)
        Me.txtReason.MaxLength = 0
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(450, 20)
        Me.txtReason.TabIndex = 14
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Enabled = False
        Me.txtDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDept.Location = New System.Drawing.Point(96, 52)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(184, 20)
        Me.txtDept.TabIndex = 5
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Enabled = False
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(96, 32)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(86, 20)
        Me.txtEmpCode.TabIndex = 3
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Enabled = False
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefNo.Location = New System.Drawing.Point(96, 12)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(86, 20)
        Me.txtRefNo.TabIndex = 1
        '
        'txtDesg
        '
        Me.txtDesg.AcceptsReturn = True
        Me.txtDesg.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesg.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDesg.Enabled = False
        Me.txtDesg.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesg.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDesg.Location = New System.Drawing.Point(354, 52)
        Me.txtDesg.MaxLength = 0
        Me.txtDesg.Name = "txtDesg"
        Me.txtDesg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDesg.Size = New System.Drawing.Size(190, 20)
        Me.txtDesg.TabIndex = 6
        '
        'txtRecEmpCode
        '
        Me.txtRecEmpCode.AcceptsReturn = True
        Me.txtRecEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecEmpCode.Enabled = False
        Me.txtRecEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRecEmpCode.Location = New System.Drawing.Point(96, 92)
        Me.txtRecEmpCode.MaxLength = 0
        Me.txtRecEmpCode.Name = "txtRecEmpCode"
        Me.txtRecEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecEmpCode.Size = New System.Drawing.Size(86, 20)
        Me.txtRecEmpCode.TabIndex = 10
        '
        'txtRecEmpName
        '
        Me.txtRecEmpName.AcceptsReturn = True
        Me.txtRecEmpName.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRecEmpName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecEmpName.Enabled = False
        Me.txtRecEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRecEmpName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRecEmpName.Location = New System.Drawing.Point(184, 92)
        Me.txtRecEmpName.MaxLength = 0
        Me.txtRecEmpName.Name = "txtRecEmpName"
        Me.txtRecEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecEmpName.Size = New System.Drawing.Size(361, 20)
        Me.txtRecEmpName.TabIndex = 11
        '
        'txtAppEmpCode
        '
        Me.txtAppEmpCode.AcceptsReturn = True
        Me.txtAppEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtAppEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAppEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAppEmpCode.Enabled = False
        Me.txtAppEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAppEmpCode.Location = New System.Drawing.Point(96, 112)
        Me.txtAppEmpCode.MaxLength = 0
        Me.txtAppEmpCode.Name = "txtAppEmpCode"
        Me.txtAppEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppEmpCode.Size = New System.Drawing.Size(86, 20)
        Me.txtAppEmpCode.TabIndex = 12
        '
        'txtAppEmpName
        '
        Me.txtAppEmpName.AcceptsReturn = True
        Me.txtAppEmpName.BackColor = System.Drawing.SystemColors.Window
        Me.txtAppEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAppEmpName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAppEmpName.Enabled = False
        Me.txtAppEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppEmpName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAppEmpName.Location = New System.Drawing.Point(184, 112)
        Me.txtAppEmpName.MaxLength = 0
        Me.txtAppEmpName.Name = "txtAppEmpName"
        Me.txtAppEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppEmpName.Size = New System.Drawing.Size(361, 20)
        Me.txtAppEmpName.TabIndex = 13
        '
        'txtLeaveFrom
        '
        Me.txtLeaveFrom.AcceptsReturn = True
        Me.txtLeaveFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtLeaveFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLeaveFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLeaveFrom.Enabled = False
        Me.txtLeaveFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLeaveFrom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLeaveFrom.Location = New System.Drawing.Point(96, 72)
        Me.txtLeaveFrom.MaxLength = 0
        Me.txtLeaveFrom.Name = "txtLeaveFrom"
        Me.txtLeaveFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLeaveFrom.Size = New System.Drawing.Size(86, 20)
        Me.txtLeaveFrom.TabIndex = 7
        '
        'txtLeaveTo
        '
        Me.txtLeaveTo.AcceptsReturn = True
        Me.txtLeaveTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtLeaveTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLeaveTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLeaveTo.Enabled = False
        Me.txtLeaveTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLeaveTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLeaveTo.Location = New System.Drawing.Point(256, 72)
        Me.txtLeaveTo.MaxLength = 0
        Me.txtLeaveTo.Name = "txtLeaveTo"
        Me.txtLeaveTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLeaveTo.Size = New System.Drawing.Size(92, 20)
        Me.txtLeaveTo.TabIndex = 8
        '
        'txtLDays
        '
        Me.txtLDays.AcceptsReturn = True
        Me.txtLDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtLDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLDays.Enabled = False
        Me.txtLDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLDays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLDays.Location = New System.Drawing.Point(468, 72)
        Me.txtLDays.MaxLength = 0
        Me.txtLDays.Name = "txtLDays"
        Me.txtLDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLDays.Size = New System.Drawing.Size(76, 20)
        Me.txtLDays.TabIndex = 9
        '
        'txtRefDate
        '
        Me.txtRefDate.AllowPromptAsInput = False
        Me.txtRefDate.Enabled = False
        Me.txtRefDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDate.Location = New System.Drawing.Point(452, 12)
        Me.txtRefDate.Mask = "##/##/####"
        Me.txtRefDate.Name = "txtRefDate"
        Me.txtRefDate.Size = New System.Drawing.Size(90, 20)
        Me.txtRefDate.TabIndex = 2
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 152)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(544, 228)
        Me.SprdMain.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Enabled = False
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 134)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(50, 14)
        Me.Label5.TabIndex = 49
        Me.Label5.Text = "Reason :"
        Me.Label5.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(35, 14)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Dept :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(390, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(55, 14)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Ref Date :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Emp Code :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(6, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(46, 14)
        Me.Label14.TabIndex = 45
        Me.Label14.Text = "Ref No :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(282, 54)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(66, 14)
        Me.Label15.TabIndex = 44
        Me.Label15.Text = "Designation:"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(6, 94)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(81, 14)
        Me.Label16.TabIndex = 43
        Me.Label16.Text = "Recommended:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(6, 114)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(77, 14)
        Me.Label17.TabIndex = 42
        Me.Label17.Text = "Approved By :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(6, 74)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(70, 14)
        Me.Label9.TabIndex = 41
        Me.Label9.Text = "Leave From :"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(184, 74)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(57, 14)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Leave To :"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(362, 74)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(93, 14)
        Me.Label19.TabIndex = 39
        Me.Label19.Text = "Total Leave Days:"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 21
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 414)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(550, 53)
        Me.FraMovement.TabIndex = 0
        Me.FraMovement.TabStop = False
        '
        'frmEmpLeaveOLEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(550, 467)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(21, 91)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpLeaveOLEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Leave Entry"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class