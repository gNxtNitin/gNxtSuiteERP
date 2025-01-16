Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamMachineMst
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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents cboMaintType As System.Windows.Forms.ComboBox
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents cboDuePMDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate4 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate3 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate4 As System.Windows.Forms.Label
    Public WithEvents lblDate3 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cboBreakDown As System.Windows.Forms.ComboBox
    Public WithEvents cboKey As System.Windows.Forms.ComboBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptOrderBy_3 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboLastPMDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate2 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate1 As System.Windows.Forms.Label
    Public WithEvents lblDate2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtMachineDesc As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchMachineDesc As System.Windows.Forms.Button
    Public WithEvents chkAllMachineDesc As System.Windows.Forms.CheckBox
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchLocation As System.Windows.Forms.Button
    Public WithEvents chkAllLocation As System.Windows.Forms.CheckBox
    Public WithEvents txtMake As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchMake As System.Windows.Forms.Button
    Public WithEvents chkAllMake As System.Windows.Forms.CheckBox
    Public WithEvents chkAllMachineNo As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchMachineNo As System.Windows.Forms.Button
    Public WithEvents txtMachineNo As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents OptOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamMachineMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtMachineDesc = New System.Windows.Forms.TextBox()
        Me.cmdSearchMachineDesc = New System.Windows.Forms.Button()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.cmdSearchLocation = New System.Windows.Forms.Button()
        Me.txtMake = New System.Windows.Forms.TextBox()
        Me.cmdSearchMake = New System.Windows.Forms.Button()
        Me.cmdSearchMachineNo = New System.Windows.Forms.Button()
        Me.txtMachineNo = New System.Windows.Forms.TextBox()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboMaintType = New System.Windows.Forms.ComboBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboDuePMDate = New System.Windows.Forms.ComboBox()
        Me.txtDate4 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate3 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate4 = New System.Windows.Forms.Label()
        Me.lblDate3 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cboBreakDown = New System.Windows.Forms.ComboBox()
        Me.cboKey = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_3 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_2 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboLastPMDate = New System.Windows.Forms.ComboBox()
        Me.txtDate2 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate1 = New System.Windows.Forms.Label()
        Me.lblDate2 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAllMachineDesc = New System.Windows.Forms.CheckBox()
        Me.chkAllLocation = New System.Windows.Forms.CheckBox()
        Me.chkAllMake = New System.Windows.Forms.CheckBox()
        Me.chkAllMachineNo = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtMachineDesc
        '
        Me.txtMachineDesc.AcceptsReturn = True
        Me.txtMachineDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachineDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachineDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineDesc.Enabled = False
        Me.txtMachineDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMachineDesc.Location = New System.Drawing.Point(98, 32)
        Me.txtMachineDesc.MaxLength = 0
        Me.txtMachineDesc.Name = "txtMachineDesc"
        Me.txtMachineDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachineDesc.Size = New System.Drawing.Size(213, 20)
        Me.txtMachineDesc.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtMachineDesc, "Press F1 For Help")
        '
        'cmdSearchMachineDesc
        '
        Me.cmdSearchMachineDesc.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMachineDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMachineDesc.Enabled = False
        Me.cmdSearchMachineDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMachineDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMachineDesc.Image = CType(resources.GetObject("cmdSearchMachineDesc.Image"), System.Drawing.Image)
        Me.cmdSearchMachineDesc.Location = New System.Drawing.Point(312, 32)
        Me.cmdSearchMachineDesc.Name = "cmdSearchMachineDesc"
        Me.cmdSearchMachineDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMachineDesc.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchMachineDesc.TabIndex = 4
        Me.cmdSearchMachineDesc.TabStop = False
        Me.cmdSearchMachineDesc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMachineDesc, "Search")
        Me.cmdSearchMachineDesc.UseVisualStyleBackColor = False
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Enabled = False
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocation.Location = New System.Drawing.Point(98, 76)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(213, 20)
        Me.txtLocation.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtLocation, "Press F1 For Help")
        '
        'cmdSearchLocation
        '
        Me.cmdSearchLocation.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchLocation.Enabled = False
        Me.cmdSearchLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchLocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchLocation.Image = CType(resources.GetObject("cmdSearchLocation.Image"), System.Drawing.Image)
        Me.cmdSearchLocation.Location = New System.Drawing.Point(312, 76)
        Me.cmdSearchLocation.Name = "cmdSearchLocation"
        Me.cmdSearchLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchLocation.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchLocation.TabIndex = 10
        Me.cmdSearchLocation.TabStop = False
        Me.cmdSearchLocation.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchLocation, "Search")
        Me.cmdSearchLocation.UseVisualStyleBackColor = False
        '
        'txtMake
        '
        Me.txtMake.AcceptsReturn = True
        Me.txtMake.BackColor = System.Drawing.SystemColors.Window
        Me.txtMake.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMake.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMake.Enabled = False
        Me.txtMake.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMake.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMake.Location = New System.Drawing.Point(98, 54)
        Me.txtMake.MaxLength = 0
        Me.txtMake.Name = "txtMake"
        Me.txtMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMake.Size = New System.Drawing.Size(213, 20)
        Me.txtMake.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtMake, "Press F1 For Help")
        '
        'cmdSearchMake
        '
        Me.cmdSearchMake.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMake.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMake.Enabled = False
        Me.cmdSearchMake.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMake.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMake.Image = CType(resources.GetObject("cmdSearchMake.Image"), System.Drawing.Image)
        Me.cmdSearchMake.Location = New System.Drawing.Point(312, 54)
        Me.cmdSearchMake.Name = "cmdSearchMake"
        Me.cmdSearchMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMake.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchMake.TabIndex = 7
        Me.cmdSearchMake.TabStop = False
        Me.cmdSearchMake.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMake, "Search")
        Me.cmdSearchMake.UseVisualStyleBackColor = False
        '
        'cmdSearchMachineNo
        '
        Me.cmdSearchMachineNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMachineNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMachineNo.Enabled = False
        Me.cmdSearchMachineNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMachineNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMachineNo.Image = CType(resources.GetObject("cmdSearchMachineNo.Image"), System.Drawing.Image)
        Me.cmdSearchMachineNo.Location = New System.Drawing.Point(312, 10)
        Me.cmdSearchMachineNo.Name = "cmdSearchMachineNo"
        Me.cmdSearchMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMachineNo.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchMachineNo.TabIndex = 1
        Me.cmdSearchMachineNo.TabStop = False
        Me.cmdSearchMachineNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMachineNo, "Search")
        Me.cmdSearchMachineNo.UseVisualStyleBackColor = False
        '
        'txtMachineNo
        '
        Me.txtMachineNo.AcceptsReturn = True
        Me.txtMachineNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachineNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachineNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineNo.Enabled = False
        Me.txtMachineNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMachineNo.Location = New System.Drawing.Point(98, 10)
        Me.txtMachineNo.MaxLength = 0
        Me.txtMachineNo.Name = "txtMachineNo"
        Me.txtMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachineNo.Size = New System.Drawing.Size(213, 20)
        Me.txtMachineNo.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtMachineNo, "Press F1 For Help")
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(242, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 25
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        Me.CmdSave.Visible = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(310, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 26
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(123, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 24
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(63, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 23
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(4, 10)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 22
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 140)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(908, 431)
        Me.SprdMain.TabIndex = 21
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cboMaintType)
        Me.Frame5.Controls.Add(Me.cboStatus)
        Me.Frame5.Controls.Add(Me.Label8)
        Me.Frame5.Controls.Add(Me.Label7)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 570)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(385, 49)
        Me.Frame5.TabIndex = 46
        Me.Frame5.TabStop = False
        '
        'cboMaintType
        '
        Me.cboMaintType.BackColor = System.Drawing.SystemColors.Window
        Me.cboMaintType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMaintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaintType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMaintType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMaintType.Location = New System.Drawing.Point(278, 18)
        Me.cboMaintType.Name = "cboMaintType"
        Me.cboMaintType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMaintType.Size = New System.Drawing.Size(97, 22)
        Me.cboMaintType.TabIndex = 49
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(66, 18)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(121, 22)
        Me.cboStatus.TabIndex = 47
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(195, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(78, 29)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Maintenance Type : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(12, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(47, 13)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "Status : "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboDuePMDate)
        Me.Frame4.Controls.Add(Me.txtDate4)
        Me.Frame4.Controls.Add(Me.txtDate3)
        Me.Frame4.Controls.Add(Me.lblDate4)
        Me.Frame4.Controls.Add(Me.lblDate3)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(394, 40)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(369, 39)
        Me.Frame4.TabIndex = 40
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "PM Due Date Condition"
        '
        'cboDuePMDate
        '
        Me.cboDuePMDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboDuePMDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDuePMDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDuePMDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDuePMDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDuePMDate.Location = New System.Drawing.Point(4, 14)
        Me.cboDuePMDate.Name = "cboDuePMDate"
        Me.cboDuePMDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDuePMDate.Size = New System.Drawing.Size(99, 22)
        Me.cboDuePMDate.TabIndex = 41
        '
        'txtDate4
        '
        Me.txtDate4.AllowPromptAsInput = False
        Me.txtDate4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate4.Location = New System.Drawing.Point(290, 12)
        Me.txtDate4.Mask = "##/##/####"
        Me.txtDate4.Name = "txtDate4"
        Me.txtDate4.Size = New System.Drawing.Size(76, 20)
        Me.txtDate4.TabIndex = 42
        '
        'txtDate3
        '
        Me.txtDate3.AllowPromptAsInput = False
        Me.txtDate3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate3.Location = New System.Drawing.Point(156, 12)
        Me.txtDate3.Mask = "##/##/####"
        Me.txtDate3.Name = "txtDate3"
        Me.txtDate3.Size = New System.Drawing.Size(76, 20)
        Me.txtDate3.TabIndex = 43
        '
        'lblDate4
        '
        Me.lblDate4.AutoSize = True
        Me.lblDate4.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate4.Location = New System.Drawing.Point(230, 16)
        Me.lblDate4.Name = "lblDate4"
        Me.lblDate4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate4.Size = New System.Drawing.Size(49, 13)
        Me.lblDate4.TabIndex = 45
        Me.lblDate4.Text = "Date 2 : "
        Me.lblDate4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDate3
        '
        Me.lblDate3.AutoSize = True
        Me.lblDate3.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate3.Location = New System.Drawing.Point(106, 16)
        Me.lblDate3.Name = "lblDate3"
        Me.lblDate3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate3.Size = New System.Drawing.Size(47, 13)
        Me.lblDate3.TabIndex = 44
        Me.lblDate3.Text = "Date 1 : "
        Me.lblDate3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cboBreakDown)
        Me.Frame3.Controls.Add(Me.cboKey)
        Me.Frame3.Controls.Add(Me.Label5)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 99)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(393, 40)
        Me.Frame3.TabIndex = 36
        Me.Frame3.TabStop = False
        '
        'cboBreakDown
        '
        Me.cboBreakDown.BackColor = System.Drawing.SystemColors.Window
        Me.cboBreakDown.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboBreakDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBreakDown.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBreakDown.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboBreakDown.Location = New System.Drawing.Point(314, 14)
        Me.cboBreakDown.Name = "cboBreakDown"
        Me.cboBreakDown.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboBreakDown.Size = New System.Drawing.Size(73, 22)
        Me.cboBreakDown.TabIndex = 16
        '
        'cboKey
        '
        Me.cboKey.BackColor = System.Drawing.SystemColors.Window
        Me.cboKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboKey.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboKey.Location = New System.Drawing.Point(98, 14)
        Me.cboKey.Name = "cboKey"
        Me.cboKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboKey.Size = New System.Drawing.Size(73, 22)
        Me.cboKey.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(7, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(80, 13)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Key Machine : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(185, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(113, 13)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Machine under B/D : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_3)
        Me.Frame2.Controls.Add(Me._OptOrderBy_2)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(394, 80)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(369, 59)
        Me.Frame2.TabIndex = 35
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order By"
        '
        '_OptOrderBy_3
        '
        Me._OptOrderBy_3.AutoSize = True
        Me._OptOrderBy_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_3, CType(3, Short))
        Me._OptOrderBy_3.Location = New System.Drawing.Point(220, 40)
        Me._OptOrderBy_3.Name = "_OptOrderBy_3"
        Me._OptOrderBy_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_3.Size = New System.Drawing.Size(91, 17)
        Me._OptOrderBy_3.TabIndex = 20
        Me._OptOrderBy_3.TabStop = True
        Me._OptOrderBy_3.Text = "Due PM Date"
        Me._OptOrderBy_3.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_2
        '
        Me._OptOrderBy_2.AutoSize = True
        Me._OptOrderBy_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_2, CType(2, Short))
        Me._OptOrderBy_2.Location = New System.Drawing.Point(220, 20)
        Me._OptOrderBy_2.Name = "_OptOrderBy_2"
        Me._OptOrderBy_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_2.Size = New System.Drawing.Size(91, 17)
        Me._OptOrderBy_2.TabIndex = 19
        Me._OptOrderBy_2.TabStop = True
        Me._OptOrderBy_2.Text = "Last PM Date"
        Me._OptOrderBy_2.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_1
        '
        Me._OptOrderBy_1.AutoSize = True
        Me._OptOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_1, CType(1, Short))
        Me._OptOrderBy_1.Location = New System.Drawing.Point(44, 40)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(94, 17)
        Me._OptOrderBy_1.TabIndex = 18
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Machine Desc"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_0
        '
        Me._OptOrderBy_0.AutoSize = True
        Me._OptOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_0, CType(0, Short))
        Me._OptOrderBy_0.Location = New System.Drawing.Point(44, 20)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(85, 17)
        Me._OptOrderBy_0.TabIndex = 17
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "Machine No"
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboLastPMDate)
        Me.Frame1.Controls.Add(Me.txtDate2)
        Me.Frame1.Controls.Add(Me.txtDate1)
        Me.Frame1.Controls.Add(Me.lblDate1)
        Me.Frame1.Controls.Add(Me.lblDate2)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(394, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(369, 39)
        Me.Frame1.TabIndex = 32
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Last PM Date Condition"
        '
        'cboLastPMDate
        '
        Me.cboLastPMDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboLastPMDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLastPMDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLastPMDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLastPMDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLastPMDate.Location = New System.Drawing.Point(4, 14)
        Me.cboLastPMDate.Name = "cboLastPMDate"
        Me.cboLastPMDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLastPMDate.Size = New System.Drawing.Size(99, 22)
        Me.cboLastPMDate.TabIndex = 12
        '
        'txtDate2
        '
        Me.txtDate2.AllowPromptAsInput = False
        Me.txtDate2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate2.Location = New System.Drawing.Point(290, 12)
        Me.txtDate2.Mask = "##/##/####"
        Me.txtDate2.Name = "txtDate2"
        Me.txtDate2.Size = New System.Drawing.Size(76, 20)
        Me.txtDate2.TabIndex = 14
        '
        'txtDate1
        '
        Me.txtDate1.AllowPromptAsInput = False
        Me.txtDate1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate1.Location = New System.Drawing.Point(156, 12)
        Me.txtDate1.Mask = "##/##/####"
        Me.txtDate1.Name = "txtDate1"
        Me.txtDate1.Size = New System.Drawing.Size(76, 20)
        Me.txtDate1.TabIndex = 13
        '
        'lblDate1
        '
        Me.lblDate1.AutoSize = True
        Me.lblDate1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate1.Location = New System.Drawing.Point(106, 16)
        Me.lblDate1.Name = "lblDate1"
        Me.lblDate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate1.Size = New System.Drawing.Size(47, 13)
        Me.lblDate1.TabIndex = 34
        Me.lblDate1.Text = "Date 1 : "
        Me.lblDate1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDate2
        '
        Me.lblDate2.AutoSize = True
        Me.lblDate2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate2.Location = New System.Drawing.Point(230, 16)
        Me.lblDate2.Name = "lblDate2"
        Me.lblDate2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate2.Size = New System.Drawing.Size(49, 13)
        Me.lblDate2.TabIndex = 33
        Me.lblDate2.Text = "Date 2 : "
        Me.lblDate2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtMachineDesc)
        Me.FraAccount.Controls.Add(Me.cmdSearchMachineDesc)
        Me.FraAccount.Controls.Add(Me.chkAllMachineDesc)
        Me.FraAccount.Controls.Add(Me.txtLocation)
        Me.FraAccount.Controls.Add(Me.cmdSearchLocation)
        Me.FraAccount.Controls.Add(Me.chkAllLocation)
        Me.FraAccount.Controls.Add(Me.txtMake)
        Me.FraAccount.Controls.Add(Me.cmdSearchMake)
        Me.FraAccount.Controls.Add(Me.chkAllMake)
        Me.FraAccount.Controls.Add(Me.chkAllMachineNo)
        Me.FraAccount.Controls.Add(Me.cmdSearchMachineNo)
        Me.FraAccount.Controls.Add(Me.txtMachineNo)
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Controls.Add(Me.Label4)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(393, 99)
        Me.FraAccount.TabIndex = 27
        Me.FraAccount.TabStop = False
        '
        'chkAllMachineDesc
        '
        Me.chkAllMachineDesc.AutoSize = True
        Me.chkAllMachineDesc.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllMachineDesc.Checked = True
        Me.chkAllMachineDesc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllMachineDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllMachineDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllMachineDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllMachineDesc.Location = New System.Drawing.Point(342, 36)
        Me.chkAllMachineDesc.Name = "chkAllMachineDesc"
        Me.chkAllMachineDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllMachineDesc.Size = New System.Drawing.Size(43, 17)
        Me.chkAllMachineDesc.TabIndex = 5
        Me.chkAllMachineDesc.Text = "ALL"
        Me.chkAllMachineDesc.UseVisualStyleBackColor = False
        '
        'chkAllLocation
        '
        Me.chkAllLocation.AutoSize = True
        Me.chkAllLocation.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllLocation.Checked = True
        Me.chkAllLocation.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllLocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllLocation.Location = New System.Drawing.Point(342, 80)
        Me.chkAllLocation.Name = "chkAllLocation"
        Me.chkAllLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllLocation.Size = New System.Drawing.Size(43, 17)
        Me.chkAllLocation.TabIndex = 11
        Me.chkAllLocation.Text = "ALL"
        Me.chkAllLocation.UseVisualStyleBackColor = False
        '
        'chkAllMake
        '
        Me.chkAllMake.AutoSize = True
        Me.chkAllMake.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllMake.Checked = True
        Me.chkAllMake.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllMake.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllMake.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllMake.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllMake.Location = New System.Drawing.Point(342, 58)
        Me.chkAllMake.Name = "chkAllMake"
        Me.chkAllMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllMake.Size = New System.Drawing.Size(43, 17)
        Me.chkAllMake.TabIndex = 8
        Me.chkAllMake.Text = "ALL"
        Me.chkAllMake.UseVisualStyleBackColor = False
        '
        'chkAllMachineNo
        '
        Me.chkAllMachineNo.AutoSize = True
        Me.chkAllMachineNo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllMachineNo.Checked = True
        Me.chkAllMachineNo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllMachineNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllMachineNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllMachineNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllMachineNo.Location = New System.Drawing.Point(342, 14)
        Me.chkAllMachineNo.Name = "chkAllMachineNo"
        Me.chkAllMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllMachineNo.Size = New System.Drawing.Size(43, 17)
        Me.chkAllMachineNo.TabIndex = 2
        Me.chkAllMachineNo.Text = "ALL"
        Me.chkAllMachineNo.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(4, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "Machine Desc : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(27, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Location : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(54, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Make : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(17, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Machine No : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(390, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(373, 49)
        Me.FraMovement.TabIndex = 28
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 174)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 48
        '
        'frmParamMachineMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Report1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamMachineMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Machine Master List"
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain.DataSource = Nothing
    End Sub
#End Region
End Class