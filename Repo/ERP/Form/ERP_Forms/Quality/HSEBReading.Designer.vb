Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmHSEBReading
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
        'Me.MdiParent = Quality.Master

        VB6_AddADODataBinding()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            VB6_RemoveADODataBinding()
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents txtMultiFactor As System.Windows.Forms.TextBox
    Public WithEvents txtDivCode As System.Windows.Forms.TextBox
    Public WithEvents cboMeterNo As System.Windows.Forms.ComboBox
    Public WithEvents txtKVA8 As System.Windows.Forms.TextBox
    Public WithEvents txtKVAMDI As System.Windows.Forms.TextBox
    Public WithEvents txtPF As System.Windows.Forms.TextBox
    Public WithEvents txtPFDay As System.Windows.Forms.TextBox
    Public WithEvents txtKWHDay As System.Windows.Forms.TextBox
    Public WithEvents txtKVAHDay As System.Windows.Forms.TextBox
    Public WithEvents txtKVAH As System.Windows.Forms.TextBox
    Public WithEvents txtKWH As System.Windows.Forms.TextBox
    Public WithEvents txtLastKWHReading As System.Windows.Forms.TextBox
    Public WithEvents txtKVRHD As System.Windows.Forms.TextBox
    Public WithEvents txtLastKVAHReading As System.Windows.Forms.TextBox
    Public WithEvents txtKVARHG As System.Windows.Forms.TextBox
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtNumber As System.Windows.Forms.TextBox
    Public WithEvents txtTime As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchNo As System.Windows.Forms.Button
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchEmpCode As System.Windows.Forms.Button
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents LblRemarks As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lbl As System.Windows.Forms.Label
    Public WithEvents lblEmpCode As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHSEBReading))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchNo = New System.Windows.Forms.Button()
        Me.cmdSearchEmpCode = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtMultiFactor = New System.Windows.Forms.TextBox()
        Me.txtDivCode = New System.Windows.Forms.TextBox()
        Me.cboMeterNo = New System.Windows.Forms.ComboBox()
        Me.txtKVA8 = New System.Windows.Forms.TextBox()
        Me.txtKVAMDI = New System.Windows.Forms.TextBox()
        Me.txtPF = New System.Windows.Forms.TextBox()
        Me.txtPFDay = New System.Windows.Forms.TextBox()
        Me.txtKWHDay = New System.Windows.Forms.TextBox()
        Me.txtKVAHDay = New System.Windows.Forms.TextBox()
        Me.txtKVAH = New System.Windows.Forms.TextBox()
        Me.txtKWH = New System.Windows.Forms.TextBox()
        Me.txtLastKWHReading = New System.Windows.Forms.TextBox()
        Me.txtKVRHD = New System.Windows.Forms.TextBox()
        Me.txtLastKVAHReading = New System.Windows.Forms.TextBox()
        Me.txtKVARHG = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.txtTime = New System.Windows.Forms.TextBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lbl = New System.Windows.Forms.Label()
        Me.lblEmpCode = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.fraTop1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchNo
        '
        Me.cmdSearchNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchNo.Image = CType(resources.GetObject("cmdSearchNo.Image"), System.Drawing.Image)
        Me.cmdSearchNo.Location = New System.Drawing.Point(239, 12)
        Me.cmdSearchNo.Name = "cmdSearchNo"
        Me.cmdSearchNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchNo.TabIndex = 1
        Me.cmdSearchNo.TabStop = False
        Me.cmdSearchNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchNo, "Search")
        Me.cmdSearchNo.UseVisualStyleBackColor = False
        '
        'cmdSearchEmpCode
        '
        Me.cmdSearchEmpCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEmpCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEmpCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEmpCode.Image = CType(resources.GetObject("cmdSearchEmpCode.Image"), System.Drawing.Image)
        Me.cmdSearchEmpCode.Location = New System.Drawing.Point(239, 258)
        Me.cmdSearchEmpCode.Name = "cmdSearchEmpCode"
        Me.cmdSearchEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchEmpCode.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchEmpCode.TabIndex = 30
        Me.cmdSearchEmpCode.TabStop = False
        Me.cmdSearchEmpCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchEmpCode, "Search")
        Me.cmdSearchEmpCode.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(408, 15)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 27
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(206, 15)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 24
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(340, 15)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 26
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(542, 15)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 29
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(474, 15)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 28
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(272, 15)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 25
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(138, 15)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 23
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(70, 15)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 22
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(4, 15)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 21
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtMultiFactor)
        Me.fraTop1.Controls.Add(Me.txtDivCode)
        Me.fraTop1.Controls.Add(Me.cboMeterNo)
        Me.fraTop1.Controls.Add(Me.txtKVA8)
        Me.fraTop1.Controls.Add(Me.txtKVAMDI)
        Me.fraTop1.Controls.Add(Me.txtPF)
        Me.fraTop1.Controls.Add(Me.txtPFDay)
        Me.fraTop1.Controls.Add(Me.txtKWHDay)
        Me.fraTop1.Controls.Add(Me.txtKVAHDay)
        Me.fraTop1.Controls.Add(Me.txtKVAH)
        Me.fraTop1.Controls.Add(Me.txtKWH)
        Me.fraTop1.Controls.Add(Me.txtLastKWHReading)
        Me.fraTop1.Controls.Add(Me.txtKVRHD)
        Me.fraTop1.Controls.Add(Me.txtLastKVAHReading)
        Me.fraTop1.Controls.Add(Me.txtKVARHG)
        Me.fraTop1.Controls.Add(Me.txtDate)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.txtNumber)
        Me.fraTop1.Controls.Add(Me.txtTime)
        Me.fraTop1.Controls.Add(Me.cmdSearchNo)
        Me.fraTop1.Controls.Add(Me.txtEmpCode)
        Me.fraTop1.Controls.Add(Me.cmdSearchEmpCode)
        Me.fraTop1.Controls.Add(Me.Label18)
        Me.fraTop1.Controls.Add(Me.Label17)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label15)
        Me.fraTop1.Controls.Add(Me.Label13)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.LblRemarks)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.lbl)
        Me.fraTop1.Controls.Add(Me.lblEmpCode)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, 0)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(613, 289)
        Me.fraTop1.TabIndex = 35
        Me.fraTop1.TabStop = False
        '
        'txtMultiFactor
        '
        Me.txtMultiFactor.AcceptsReturn = True
        Me.txtMultiFactor.BackColor = System.Drawing.SystemColors.Window
        Me.txtMultiFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMultiFactor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMultiFactor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMultiFactor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMultiFactor.Location = New System.Drawing.Point(506, 60)
        Me.txtMultiFactor.MaxLength = 0
        Me.txtMultiFactor.Name = "txtMultiFactor"
        Me.txtMultiFactor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMultiFactor.Size = New System.Drawing.Size(93, 20)
        Me.txtMultiFactor.TabIndex = 4
        '
        'txtDivCode
        '
        Me.txtDivCode.AcceptsReturn = True
        Me.txtDivCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDivCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivCode.Enabled = False
        Me.txtDivCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDivCode.Location = New System.Drawing.Point(141, 60)
        Me.txtDivCode.MaxLength = 0
        Me.txtDivCode.Name = "txtDivCode"
        Me.txtDivCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivCode.Size = New System.Drawing.Size(93, 20)
        Me.txtDivCode.TabIndex = 3
        '
        'cboMeterNo
        '
        Me.cboMeterNo.BackColor = System.Drawing.SystemColors.Window
        Me.cboMeterNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMeterNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMeterNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMeterNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMeterNo.Location = New System.Drawing.Point(140, 34)
        Me.cboMeterNo.Name = "cboMeterNo"
        Me.cboMeterNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMeterNo.Size = New System.Drawing.Size(189, 22)
        Me.cboMeterNo.TabIndex = 2
        '
        'txtKVA8
        '
        Me.txtKVA8.AcceptsReturn = True
        Me.txtKVA8.BackColor = System.Drawing.SystemColors.Window
        Me.txtKVA8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKVA8.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtKVA8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKVA8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKVA8.Location = New System.Drawing.Point(506, 192)
        Me.txtKVA8.MaxLength = 0
        Me.txtKVA8.Name = "txtKVA8"
        Me.txtKVA8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtKVA8.Size = New System.Drawing.Size(93, 20)
        Me.txtKVA8.TabIndex = 16
        '
        'txtKVAMDI
        '
        Me.txtKVAMDI.AcceptsReturn = True
        Me.txtKVAMDI.BackColor = System.Drawing.SystemColors.Window
        Me.txtKVAMDI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKVAMDI.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtKVAMDI.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKVAMDI.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKVAMDI.Location = New System.Drawing.Point(141, 192)
        Me.txtKVAMDI.MaxLength = 0
        Me.txtKVAMDI.Name = "txtKVAMDI"
        Me.txtKVAMDI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtKVAMDI.Size = New System.Drawing.Size(93, 20)
        Me.txtKVAMDI.TabIndex = 15
        '
        'txtPF
        '
        Me.txtPF.AcceptsReturn = True
        Me.txtPF.BackColor = System.Drawing.SystemColors.Window
        Me.txtPF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPF.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPF.Location = New System.Drawing.Point(141, 214)
        Me.txtPF.MaxLength = 0
        Me.txtPF.Name = "txtPF"
        Me.txtPF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPF.Size = New System.Drawing.Size(93, 20)
        Me.txtPF.TabIndex = 17
        '
        'txtPFDay
        '
        Me.txtPFDay.AcceptsReturn = True
        Me.txtPFDay.BackColor = System.Drawing.SystemColors.Window
        Me.txtPFDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPFDay.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPFDay.Enabled = False
        Me.txtPFDay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFDay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPFDay.Location = New System.Drawing.Point(506, 214)
        Me.txtPFDay.MaxLength = 0
        Me.txtPFDay.Name = "txtPFDay"
        Me.txtPFDay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPFDay.Size = New System.Drawing.Size(93, 20)
        Me.txtPFDay.TabIndex = 18
        '
        'txtKWHDay
        '
        Me.txtKWHDay.AcceptsReturn = True
        Me.txtKWHDay.BackColor = System.Drawing.SystemColors.Window
        Me.txtKWHDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKWHDay.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtKWHDay.Enabled = False
        Me.txtKWHDay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKWHDay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKWHDay.Location = New System.Drawing.Point(141, 170)
        Me.txtKWHDay.MaxLength = 0
        Me.txtKWHDay.Name = "txtKWHDay"
        Me.txtKWHDay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtKWHDay.Size = New System.Drawing.Size(93, 20)
        Me.txtKWHDay.TabIndex = 13
        '
        'txtKVAHDay
        '
        Me.txtKVAHDay.AcceptsReturn = True
        Me.txtKVAHDay.BackColor = System.Drawing.SystemColors.Window
        Me.txtKVAHDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKVAHDay.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtKVAHDay.Enabled = False
        Me.txtKVAHDay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKVAHDay.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKVAHDay.Location = New System.Drawing.Point(506, 170)
        Me.txtKVAHDay.MaxLength = 0
        Me.txtKVAHDay.Name = "txtKVAHDay"
        Me.txtKVAHDay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtKVAHDay.Size = New System.Drawing.Size(93, 20)
        Me.txtKVAHDay.TabIndex = 14
        '
        'txtKVAH
        '
        Me.txtKVAH.AcceptsReturn = True
        Me.txtKVAH.BackColor = System.Drawing.SystemColors.Window
        Me.txtKVAH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKVAH.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtKVAH.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKVAH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKVAH.Location = New System.Drawing.Point(506, 148)
        Me.txtKVAH.MaxLength = 0
        Me.txtKVAH.Name = "txtKVAH"
        Me.txtKVAH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtKVAH.Size = New System.Drawing.Size(93, 20)
        Me.txtKVAH.TabIndex = 12
        '
        'txtKWH
        '
        Me.txtKWH.AcceptsReturn = True
        Me.txtKWH.BackColor = System.Drawing.SystemColors.Window
        Me.txtKWH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKWH.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtKWH.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKWH.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKWH.Location = New System.Drawing.Point(506, 104)
        Me.txtKWH.MaxLength = 0
        Me.txtKWH.Name = "txtKWH"
        Me.txtKWH.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtKWH.Size = New System.Drawing.Size(93, 20)
        Me.txtKWH.TabIndex = 8
        '
        'txtLastKWHReading
        '
        Me.txtLastKWHReading.AcceptsReturn = True
        Me.txtLastKWHReading.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastKWHReading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLastKWHReading.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastKWHReading.Enabled = False
        Me.txtLastKWHReading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastKWHReading.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLastKWHReading.Location = New System.Drawing.Point(141, 104)
        Me.txtLastKWHReading.MaxLength = 0
        Me.txtLastKWHReading.Name = "txtLastKWHReading"
        Me.txtLastKWHReading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastKWHReading.Size = New System.Drawing.Size(93, 20)
        Me.txtLastKWHReading.TabIndex = 7
        '
        'txtKVRHD
        '
        Me.txtKVRHD.AcceptsReturn = True
        Me.txtKVRHD.BackColor = System.Drawing.SystemColors.Window
        Me.txtKVRHD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKVRHD.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtKVRHD.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKVRHD.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKVRHD.Location = New System.Drawing.Point(506, 126)
        Me.txtKVRHD.MaxLength = 0
        Me.txtKVRHD.Name = "txtKVRHD"
        Me.txtKVRHD.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtKVRHD.Size = New System.Drawing.Size(93, 20)
        Me.txtKVRHD.TabIndex = 10
        '
        'txtLastKVAHReading
        '
        Me.txtLastKVAHReading.AcceptsReturn = True
        Me.txtLastKVAHReading.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastKVAHReading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLastKVAHReading.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastKVAHReading.Enabled = False
        Me.txtLastKVAHReading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastKVAHReading.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtLastKVAHReading.Location = New System.Drawing.Point(141, 148)
        Me.txtLastKVAHReading.MaxLength = 0
        Me.txtLastKVAHReading.Name = "txtLastKVAHReading"
        Me.txtLastKVAHReading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastKVAHReading.Size = New System.Drawing.Size(93, 20)
        Me.txtLastKVAHReading.TabIndex = 11
        '
        'txtKVARHG
        '
        Me.txtKVARHG.AcceptsReturn = True
        Me.txtKVARHG.BackColor = System.Drawing.SystemColors.Window
        Me.txtKVARHG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtKVARHG.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtKVARHG.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKVARHG.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtKVARHG.Location = New System.Drawing.Point(141, 126)
        Me.txtKVARHG.MaxLength = 0
        Me.txtKVARHG.Name = "txtKVARHG"
        Me.txtKVARHG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtKVARHG.Size = New System.Drawing.Size(93, 20)
        Me.txtKVARHG.TabIndex = 9
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(141, 82)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(93, 20)
        Me.txtDate.TabIndex = 5
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtRemarks.Location = New System.Drawing.Point(141, 236)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(458, 20)
        Me.txtRemarks.TabIndex = 19
        '
        'txtNumber
        '
        Me.txtNumber.AcceptsReturn = True
        Me.txtNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNumber.Location = New System.Drawing.Point(141, 12)
        Me.txtNumber.MaxLength = 0
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumber.Size = New System.Drawing.Size(93, 20)
        Me.txtNumber.TabIndex = 0
        '
        'txtTime
        '
        Me.txtTime.AcceptsReturn = True
        Me.txtTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTime.Location = New System.Drawing.Point(506, 82)
        Me.txtTime.MaxLength = 0
        Me.txtTime.Name = "txtTime"
        Me.txtTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTime.Size = New System.Drawing.Size(93, 20)
        Me.txtTime.TabIndex = 6
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEmpCode.Location = New System.Drawing.Point(141, 258)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(93, 20)
        Me.txtEmpCode.TabIndex = 20
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(424, 63)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(77, 13)
        Me.Label18.TabIndex = 55
        Me.Label18.Text = "Multi Factor : "
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(51, 63)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(87, 13)
        Me.Label17.TabIndex = 54
        Me.Label17.Text = "Division Code : "
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(77, 38)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(61, 13)
        Me.Label16.TabIndex = 53
        Me.Label16.Text = "Meter No :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(70, 195)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(68, 13)
        Me.Label14.TabIndex = 52
        Me.Label14.Text = "KVA (MDI) : "
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(65, 173)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 51
        Me.Label10.Text = "KWH / Day : "
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(454, 195)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(47, 13)
        Me.Label15.TabIndex = 50
        Me.Label15.Text = "8KWA : "
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(107, 217)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(31, 13)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "P.F. : "
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(439, 217)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(62, 13)
        Me.Label12.TabIndex = 48
        Me.Label12.Text = "P.F. / Day : "
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(426, 173)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(75, 13)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "KVAH / Day : "
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(374, 107)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(127, 13)
        Me.Label9.TabIndex = 46
        Me.Label9.Text = "Current KWH Reading : "
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(28, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(110, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Last KWH Reading : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(448, 129)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "KVRHD : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(372, 151)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(129, 13)
        Me.Label3.TabIndex = 43
        Me.Label3.Text = "Current KVAH Reading : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(26, 151)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(112, 13)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Last KVAH Reading : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(79, 129)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "KVARHG : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(53, 85)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Reading Date : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblRemarks
        '
        Me.LblRemarks.AutoSize = True
        Me.LblRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.LblRemarks.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemarks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblRemarks.Location = New System.Drawing.Point(78, 239)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblRemarks.Size = New System.Drawing.Size(60, 13)
        Me.LblRemarks.TabIndex = 39
        Me.LblRemarks.Text = "Remarks : "
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(81, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "Number : "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(415, 85)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(86, 13)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "Reading Time : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.BackColor = System.Drawing.SystemColors.Control
        Me.lbl.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbl.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbl.Location = New System.Drawing.Point(74, 261)
        Me.lbl.Name = "lbl"
        Me.lbl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbl.Size = New System.Drawing.Size(64, 13)
        Me.lbl.TabIndex = 36
        Me.lbl.Text = "Signature : "
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblEmpCode
        '
        Me.lblEmpCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblEmpCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmpCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmpCode.Location = New System.Drawing.Point(266, 258)
        Me.lblEmpCode.Name = "lblEmpCode"
        Me.lblEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmpCode.Size = New System.Drawing.Size(333, 19)
        Me.lblEmpCode.TabIndex = 31
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(-178, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 37
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(614, 289)
        Me.SprdView.TabIndex = 32
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 284)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(613, 59)
        Me.FraMovement.TabIndex = 33
        Me.FraMovement.TabStop = False
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(10, 18)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 34
        Me.lblMkey.Text = "lblMkey"
        '
        'frmHSEBReading
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(613, 344)
        Me.Controls.Add(Me.fraTop1)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmHSEBReading"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "HSEB Reading Record"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class