Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamVoltmeterMst
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
    Public WithEvents cboLCDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate4 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate3 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate4 As System.Windows.Forms.Label
    Public WithEvents lblDate3 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents cboCalibSource As System.Windows.Forms.ComboBox
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptOrderBy_3 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboCDDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate2 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate1 As System.Windows.Forms.Label
    Public WithEvents lblDate2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtENo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchENo As System.Windows.Forms.Button
    Public WithEvents chkAllENo As System.Windows.Forms.CheckBox
    Public WithEvents txtDepartment As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDepartment As System.Windows.Forms.Button
    Public WithEvents chkAllDepartment As System.Windows.Forms.CheckBox
    Public WithEvents txtMake As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchMake As System.Windows.Forms.Button
    Public WithEvents chkAllMake As System.Windows.Forms.CheckBox
    Public WithEvents chkAllDescription As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchDescription As System.Windows.Forms.Button
    Public WithEvents txtDescription As System.Windows.Forms.TextBox
    Public WithEvents lblDeptCode As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamVoltmeterMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtENo = New System.Windows.Forms.TextBox()
        Me.cmdSearchENo = New System.Windows.Forms.Button()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.cmdSearchDepartment = New System.Windows.Forms.Button()
        Me.txtMake = New System.Windows.Forms.TextBox()
        Me.cmdSearchMake = New System.Windows.Forms.Button()
        Me.cmdSearchDescription = New System.Windows.Forms.Button()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboLCDate = New System.Windows.Forms.ComboBox()
        Me.txtDate4 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate3 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate4 = New System.Windows.Forms.Label()
        Me.lblDate3 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cboCalibSource = New System.Windows.Forms.ComboBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_3 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_2 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboCDDate = New System.Windows.Forms.ComboBox()
        Me.txtDate2 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate1 = New System.Windows.Forms.Label()
        Me.lblDate2 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAllENo = New System.Windows.Forms.CheckBox()
        Me.chkAllDepartment = New System.Windows.Forms.CheckBox()
        Me.chkAllMake = New System.Windows.Forms.CheckBox()
        Me.chkAllDescription = New System.Windows.Forms.CheckBox()
        Me.lblDeptCode = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtENo
        '
        Me.txtENo.AcceptsReturn = True
        Me.txtENo.BackColor = System.Drawing.SystemColors.Window
        Me.txtENo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtENo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtENo.Enabled = False
        Me.txtENo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtENo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtENo.Location = New System.Drawing.Point(82, 32)
        Me.txtENo.MaxLength = 0
        Me.txtENo.Name = "txtENo"
        Me.txtENo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtENo.Size = New System.Drawing.Size(229, 20)
        Me.txtENo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtENo, "Press F1 For Help")
        '
        'cmdSearchENo
        '
        Me.cmdSearchENo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchENo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchENo.Enabled = False
        Me.cmdSearchENo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchENo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchENo.Image = CType(resources.GetObject("cmdSearchENo.Image"), System.Drawing.Image)
        Me.cmdSearchENo.Location = New System.Drawing.Point(312, 32)
        Me.cmdSearchENo.Name = "cmdSearchENo"
        Me.cmdSearchENo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchENo.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchENo.TabIndex = 4
        Me.cmdSearchENo.TabStop = False
        Me.cmdSearchENo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchENo, "Search")
        Me.cmdSearchENo.UseVisualStyleBackColor = False
        '
        'txtDepartment
        '
        Me.txtDepartment.AcceptsReturn = True
        Me.txtDepartment.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepartment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDepartment.Enabled = False
        Me.txtDepartment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDepartment.Location = New System.Drawing.Point(82, 76)
        Me.txtDepartment.MaxLength = 0
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDepartment.Size = New System.Drawing.Size(229, 20)
        Me.txtDepartment.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtDepartment, "Press F1 For Help")
        '
        'cmdSearchDepartment
        '
        Me.cmdSearchDepartment.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDepartment.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDepartment.Enabled = False
        Me.cmdSearchDepartment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDepartment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDepartment.Image = CType(resources.GetObject("cmdSearchDepartment.Image"), System.Drawing.Image)
        Me.cmdSearchDepartment.Location = New System.Drawing.Point(312, 76)
        Me.cmdSearchDepartment.Name = "cmdSearchDepartment"
        Me.cmdSearchDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDepartment.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchDepartment.TabIndex = 10
        Me.cmdSearchDepartment.TabStop = False
        Me.cmdSearchDepartment.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDepartment, "Search")
        Me.cmdSearchDepartment.UseVisualStyleBackColor = False
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
        Me.txtMake.Location = New System.Drawing.Point(82, 54)
        Me.txtMake.MaxLength = 0
        Me.txtMake.Name = "txtMake"
        Me.txtMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMake.Size = New System.Drawing.Size(229, 20)
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
        'cmdSearchDescription
        '
        Me.cmdSearchDescription.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDescription.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDescription.Enabled = False
        Me.cmdSearchDescription.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDescription.Image = CType(resources.GetObject("cmdSearchDescription.Image"), System.Drawing.Image)
        Me.cmdSearchDescription.Location = New System.Drawing.Point(312, 10)
        Me.cmdSearchDescription.Name = "cmdSearchDescription"
        Me.cmdSearchDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDescription.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchDescription.TabIndex = 1
        Me.cmdSearchDescription.TabStop = False
        Me.cmdSearchDescription.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDescription, "Search")
        Me.cmdSearchDescription.UseVisualStyleBackColor = False
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = True
        Me.txtDescription.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescription.Enabled = False
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescription.Location = New System.Drawing.Point(82, 10)
        Me.txtDescription.MaxLength = 0
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescription.Size = New System.Drawing.Size(229, 20)
        Me.txtDescription.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtDescription, "Press F1 For Help")
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
        Me.cmdClose.TabIndex = 25
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
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboLCDate)
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
        Me.Frame4.TabIndex = 39
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Last Calib Date Condition"
        '
        'cboLCDate
        '
        Me.cboLCDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboLCDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLCDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLCDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLCDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLCDate.Location = New System.Drawing.Point(4, 14)
        Me.cboLCDate.Name = "cboLCDate"
        Me.cboLCDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLCDate.Size = New System.Drawing.Size(99, 22)
        Me.cboLCDate.TabIndex = 40
        '
        'txtDate4
        '
        Me.txtDate4.AllowPromptAsInput = False
        Me.txtDate4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate4.Location = New System.Drawing.Point(290, 12)
        Me.txtDate4.Mask = "##/##/####"
        Me.txtDate4.Name = "txtDate4"
        Me.txtDate4.Size = New System.Drawing.Size(76, 20)
        Me.txtDate4.TabIndex = 41
        '
        'txtDate3
        '
        Me.txtDate3.AllowPromptAsInput = False
        Me.txtDate3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate3.Location = New System.Drawing.Point(156, 12)
        Me.txtDate3.Mask = "##/##/####"
        Me.txtDate3.Name = "txtDate3"
        Me.txtDate3.Size = New System.Drawing.Size(76, 20)
        Me.txtDate3.TabIndex = 42
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
        Me.lblDate4.TabIndex = 44
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
        Me.lblDate3.TabIndex = 43
        Me.lblDate3.Text = "Date 1 : "
        Me.lblDate3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 140)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(762, 277)
        Me.SprdMain.TabIndex = 21
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cboCalibSource)
        Me.Frame3.Controls.Add(Me.cboStatus)
        Me.Frame3.Controls.Add(Me.Label5)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(394, 80)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(369, 59)
        Me.Frame3.TabIndex = 35
        Me.Frame3.TabStop = False
        '
        'cboCalibSource
        '
        Me.cboCalibSource.BackColor = System.Drawing.SystemColors.Window
        Me.cboCalibSource.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCalibSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCalibSource.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCalibSource.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCalibSource.Location = New System.Drawing.Point(138, 34)
        Me.cboCalibSource.Name = "cboCalibSource"
        Me.cboCalibSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCalibSource.Size = New System.Drawing.Size(113, 22)
        Me.cboCalibSource.TabIndex = 16
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(138, 11)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(113, 22)
        Me.cboStatus.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(84, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 37
        Me.Label5.Text = "Status : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(48, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 36
        Me.Label3.Text = "Calib Source : "
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
        Me.Frame2.Location = New System.Drawing.Point(0, 100)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(393, 41)
        Me.Frame2.TabIndex = 34
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
        Me._OptOrderBy_3.Location = New System.Drawing.Point(324, 20)
        Me._OptOrderBy_3.Name = "_OptOrderBy_3"
        Me._OptOrderBy_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_3.Size = New System.Drawing.Size(51, 17)
        Me._OptOrderBy_3.TabIndex = 20
        Me._OptOrderBy_3.TabStop = True
        Me._OptOrderBy_3.Text = "E_No"
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
        Me._OptOrderBy_2.Location = New System.Drawing.Point(216, 20)
        Me._OptOrderBy_2.Name = "_OptOrderBy_2"
        Me._OptOrderBy_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_2.Size = New System.Drawing.Size(63, 17)
        Me._OptOrderBy_2.TabIndex = 19
        Me._OptOrderBy_2.TabStop = True
        Me._OptOrderBy_2.Text = "Doc No"
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
        Me._OptOrderBy_1.Location = New System.Drawing.Point(116, 20)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(64, 17)
        Me._OptOrderBy_1.TabIndex = 18
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "LC Date"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_0
        '
        Me._OptOrderBy_0.AutoSize = True
        Me._OptOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_0.Checked = True
        Me._OptOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_0, CType(0, Short))
        Me._OptOrderBy_0.Location = New System.Drawing.Point(8, 20)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(67, 17)
        Me._OptOrderBy_0.TabIndex = 17
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "CD Date"
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboCDDate)
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
        Me.Frame1.TabIndex = 31
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Calib Due Date Condition"
        '
        'cboCDDate
        '
        Me.cboCDDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboCDDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCDDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCDDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCDDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCDDate.Location = New System.Drawing.Point(4, 14)
        Me.cboCDDate.Name = "cboCDDate"
        Me.cboCDDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCDDate.Size = New System.Drawing.Size(99, 22)
        Me.cboCDDate.TabIndex = 12
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
        Me.lblDate1.TabIndex = 33
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
        Me.lblDate2.TabIndex = 32
        Me.lblDate2.Text = "Date 2 : "
        Me.lblDate2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtENo)
        Me.FraAccount.Controls.Add(Me.cmdSearchENo)
        Me.FraAccount.Controls.Add(Me.chkAllENo)
        Me.FraAccount.Controls.Add(Me.txtDepartment)
        Me.FraAccount.Controls.Add(Me.cmdSearchDepartment)
        Me.FraAccount.Controls.Add(Me.chkAllDepartment)
        Me.FraAccount.Controls.Add(Me.txtMake)
        Me.FraAccount.Controls.Add(Me.cmdSearchMake)
        Me.FraAccount.Controls.Add(Me.chkAllMake)
        Me.FraAccount.Controls.Add(Me.chkAllDescription)
        Me.FraAccount.Controls.Add(Me.cmdSearchDescription)
        Me.FraAccount.Controls.Add(Me.txtDescription)
        Me.FraAccount.Controls.Add(Me.lblDeptCode)
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
        Me.FraAccount.TabIndex = 26
        Me.FraAccount.TabStop = False
        '
        'chkAllENo
        '
        Me.chkAllENo.AutoSize = True
        Me.chkAllENo.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllENo.Checked = True
        Me.chkAllENo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllENo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllENo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllENo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllENo.Location = New System.Drawing.Point(342, 36)
        Me.chkAllENo.Name = "chkAllENo"
        Me.chkAllENo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllENo.Size = New System.Drawing.Size(43, 17)
        Me.chkAllENo.TabIndex = 5
        Me.chkAllENo.Text = "ALL"
        Me.chkAllENo.UseVisualStyleBackColor = False
        '
        'chkAllDepartment
        '
        Me.chkAllDepartment.AutoSize = True
        Me.chkAllDepartment.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDepartment.Checked = True
        Me.chkAllDepartment.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllDepartment.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDepartment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDepartment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDepartment.Location = New System.Drawing.Point(342, 80)
        Me.chkAllDepartment.Name = "chkAllDepartment"
        Me.chkAllDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDepartment.Size = New System.Drawing.Size(43, 17)
        Me.chkAllDepartment.TabIndex = 11
        Me.chkAllDepartment.Text = "ALL"
        Me.chkAllDepartment.UseVisualStyleBackColor = False
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
        'chkAllDescription
        '
        Me.chkAllDescription.AutoSize = True
        Me.chkAllDescription.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDescription.Checked = True
        Me.chkAllDescription.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllDescription.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDescription.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDescription.Location = New System.Drawing.Point(342, 14)
        Me.chkAllDescription.Name = "chkAllDescription"
        Me.chkAllDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDescription.Size = New System.Drawing.Size(43, 17)
        Me.chkAllDescription.TabIndex = 2
        Me.chkAllDescription.Text = "ALL"
        Me.chkAllDescription.UseVisualStyleBackColor = False
        '
        'lblDeptCode
        '
        Me.lblDeptCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeptCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeptCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeptCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptCode.Location = New System.Drawing.Point(112, 80)
        Me.lblDeptCode.Name = "lblDeptCode"
        Me.lblDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeptCode.Size = New System.Drawing.Size(161, 17)
        Me.lblDeptCode.TabIndex = 45
        Me.lblDeptCode.Text = "Label7"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(38, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(42, 13)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "E_No : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(4, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(77, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Department : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(38, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 29
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
        Me.Label2.Location = New System.Drawing.Point(5, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Description : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(390, 410)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(373, 49)
        Me.FraMovement.TabIndex = 27
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 174)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 41
        '
        'frmParamVoltmeterMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(763, 459)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.SprdMain)
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
        Me.Name = "frmParamVoltmeterMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Process Instruments Master List"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
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