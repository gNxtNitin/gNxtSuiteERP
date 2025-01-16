Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamItemConsMaint
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
    Public WithEvents cboMaintType As System.Windows.Forms.ComboBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_3 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents chkAllItem As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchItem As System.Windows.Forms.Button
    Public WithEvents txtItem As System.Windows.Forms.TextBox
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDept As System.Windows.Forms.Button
    Public WithEvents chkAllDept As System.Windows.Forms.CheckBox
    Public WithEvents txtMachine As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchMachine As System.Windows.Forms.Button
    Public WithEvents chkAllMachine As System.Windows.Forms.CheckBox
    Public WithEvents lblDeptName As System.Windows.Forms.Label
    Public WithEvents lblMachineName As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents cboRprDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate2 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate1 As System.Windows.Forms.Label
    Public WithEvents lblDate2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents AData1 As VB6.ADODC
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents OptOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamItemConsMaint))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.cmdSearchDept = New System.Windows.Forms.Button()
        Me.txtMachine = New System.Windows.Forms.TextBox()
        Me.cmdSearchMachine = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cboMaintType = New System.Windows.Forms.ComboBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_2 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_3 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAllItem = New System.Windows.Forms.CheckBox()
        Me.chkAllDept = New System.Windows.Forms.CheckBox()
        Me.chkAllMachine = New System.Windows.Forms.CheckBox()
        Me.lblDeptName = New System.Windows.Forms.Label()
        Me.lblMachineName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboRprDate = New System.Windows.Forms.ComboBox()
        Me.txtDate2 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate1 = New System.Windows.Forms.Label()
        Me.lblDate2 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.AData1 = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame3.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Enabled = False
        Me.cmdSearchItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(480, 56)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchItem.TabIndex = 26
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'txtItem
        '
        Me.txtItem.AcceptsReturn = True
        Me.txtItem.BackColor = System.Drawing.SystemColors.Window
        Me.txtItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItem.Enabled = False
        Me.txtItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItem.Location = New System.Drawing.Point(74, 56)
        Me.txtItem.MaxLength = 0
        Me.txtItem.Name = "txtItem"
        Me.txtItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItem.Size = New System.Drawing.Size(405, 19)
        Me.txtItem.TabIndex = 25
        Me.ToolTip1.SetToolTip(Me.txtItem, "Press F1 For Help")
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
        Me.txtDept.Location = New System.Drawing.Point(74, 11)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(405, 19)
        Me.txtDept.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.txtDept, "Press F1 For Help")
        '
        'cmdSearchDept
        '
        Me.cmdSearchDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDept.Enabled = False
        Me.cmdSearchDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDept.Image = CType(resources.GetObject("cmdSearchDept.Image"), System.Drawing.Image)
        Me.cmdSearchDept.Location = New System.Drawing.Point(480, 11)
        Me.cmdSearchDept.Name = "cmdSearchDept"
        Me.cmdSearchDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDept.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchDept.TabIndex = 23
        Me.cmdSearchDept.TabStop = False
        Me.cmdSearchDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDept, "Search")
        Me.cmdSearchDept.UseVisualStyleBackColor = False
        '
        'txtMachine
        '
        Me.txtMachine.AcceptsReturn = True
        Me.txtMachine.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachine.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachine.Enabled = False
        Me.txtMachine.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachine.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMachine.Location = New System.Drawing.Point(74, 33)
        Me.txtMachine.MaxLength = 0
        Me.txtMachine.Name = "txtMachine"
        Me.txtMachine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachine.Size = New System.Drawing.Size(405, 19)
        Me.txtMachine.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtMachine, "Press F1 For Help")
        '
        'cmdSearchMachine
        '
        Me.cmdSearchMachine.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMachine.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMachine.Enabled = False
        Me.cmdSearchMachine.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMachine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMachine.Image = CType(resources.GetObject("cmdSearchMachine.Image"), System.Drawing.Image)
        Me.cmdSearchMachine.Location = New System.Drawing.Point(480, 33)
        Me.cmdSearchMachine.Name = "cmdSearchMachine"
        Me.cmdSearchMachine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMachine.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchMachine.TabIndex = 1
        Me.cmdSearchMachine.TabStop = False
        Me.cmdSearchMachine.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMachine, "Search")
        Me.cmdSearchMachine.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(210, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 14
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
        Me.cmdClose.Location = New System.Drawing.Point(278, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 15
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
        Me.CmdPreview.TabIndex = 13
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
        Me.cmdPrint.TabIndex = 12
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
        Me.cmdShow.TabIndex = 11
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cboMaintType)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(288, 408)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(129, 51)
        Me.Frame3.TabIndex = 33
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Maint. Type"
        '
        'cboMaintType
        '
        Me.cboMaintType.BackColor = System.Drawing.SystemColors.Window
        Me.cboMaintType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMaintType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaintType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMaintType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMaintType.Location = New System.Drawing.Point(8, 16)
        Me.cboMaintType.Name = "cboMaintType"
        Me.cboMaintType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMaintType.Size = New System.Drawing.Size(115, 22)
        Me.cboMaintType.TabIndex = 34
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 92)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(762, 317)
        Me.SprdMain.TabIndex = 10
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Controls.Add(Me._OptOrderBy_2)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Controls.Add(Me._OptOrderBy_3)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 408)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(289, 51)
        Me.Frame2.TabIndex = 21
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order By"
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
        Me._OptOrderBy_0.Location = New System.Drawing.Point(8, 22)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(49, 18)
        Me._OptOrderBy_0.TabIndex = 6
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "Date"
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_2
        '
        Me._OptOrderBy_2.AutoSize = True
        Me._OptOrderBy_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_2, CType(2, Short))
        Me._OptOrderBy_2.Location = New System.Drawing.Point(144, 22)
        Me._OptOrderBy_2.Name = "_OptOrderBy_2"
        Me._OptOrderBy_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_2.Size = New System.Drawing.Size(71, 18)
        Me._OptOrderBy_2.TabIndex = 8
        Me._OptOrderBy_2.TabStop = True
        Me._OptOrderBy_2.Text = "Machine"
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
        Me._OptOrderBy_1.Location = New System.Drawing.Point(76, 22)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(50, 18)
        Me._OptOrderBy_1.TabIndex = 7
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Dept"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_3
        '
        Me._OptOrderBy_3.AutoSize = True
        Me._OptOrderBy_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_3, CType(3, Short))
        Me._OptOrderBy_3.Location = New System.Drawing.Point(228, 22)
        Me._OptOrderBy_3.Name = "_OptOrderBy_3"
        Me._OptOrderBy_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_3.Size = New System.Drawing.Size(50, 18)
        Me._OptOrderBy_3.TabIndex = 9
        Me._OptOrderBy_3.TabStop = True
        Me._OptOrderBy_3.Text = "Item"
        Me._OptOrderBy_3.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkAllItem)
        Me.FraAccount.Controls.Add(Me.cmdSearchItem)
        Me.FraAccount.Controls.Add(Me.txtItem)
        Me.FraAccount.Controls.Add(Me.txtDept)
        Me.FraAccount.Controls.Add(Me.cmdSearchDept)
        Me.FraAccount.Controls.Add(Me.chkAllDept)
        Me.FraAccount.Controls.Add(Me.txtMachine)
        Me.FraAccount.Controls.Add(Me.cmdSearchMachine)
        Me.FraAccount.Controls.Add(Me.chkAllMachine)
        Me.FraAccount.Controls.Add(Me.lblDeptName)
        Me.FraAccount.Controls.Add(Me.lblMachineName)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(577, 91)
        Me.FraAccount.TabIndex = 16
        Me.FraAccount.TabStop = False
        '
        'chkAllItem
        '
        Me.chkAllItem.AutoSize = True
        Me.chkAllItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllItem.Checked = True
        Me.chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllItem.Location = New System.Drawing.Point(510, 59)
        Me.chkAllItem.Name = "chkAllItem"
        Me.chkAllItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllItem.Size = New System.Drawing.Size(48, 18)
        Me.chkAllItem.TabIndex = 27
        Me.chkAllItem.Text = "ALL"
        Me.chkAllItem.UseVisualStyleBackColor = False
        '
        'chkAllDept
        '
        Me.chkAllDept.AutoSize = True
        Me.chkAllDept.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDept.Checked = True
        Me.chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDept.Location = New System.Drawing.Point(510, 14)
        Me.chkAllDept.Name = "chkAllDept"
        Me.chkAllDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDept.Size = New System.Drawing.Size(48, 18)
        Me.chkAllDept.TabIndex = 22
        Me.chkAllDept.Text = "ALL"
        Me.chkAllDept.UseVisualStyleBackColor = False
        '
        'chkAllMachine
        '
        Me.chkAllMachine.AutoSize = True
        Me.chkAllMachine.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllMachine.Checked = True
        Me.chkAllMachine.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllMachine.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllMachine.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllMachine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllMachine.Location = New System.Drawing.Point(510, 36)
        Me.chkAllMachine.Name = "chkAllMachine"
        Me.chkAllMachine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllMachine.Size = New System.Drawing.Size(48, 18)
        Me.chkAllMachine.TabIndex = 2
        Me.chkAllMachine.Text = "ALL"
        Me.chkAllMachine.UseVisualStyleBackColor = False
        '
        'lblDeptName
        '
        Me.lblDeptName.AutoSize = True
        Me.lblDeptName.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeptName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeptName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptName.Location = New System.Drawing.Point(160, 11)
        Me.lblDeptName.Name = "lblDeptName"
        Me.lblDeptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeptName.Size = New System.Drawing.Size(2, 16)
        Me.lblDeptName.TabIndex = 32
        '
        'lblMachineName
        '
        Me.lblMachineName.AutoSize = True
        Me.lblMachineName.BackColor = System.Drawing.SystemColors.Control
        Me.lblMachineName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMachineName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMachineName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMachineName.Location = New System.Drawing.Point(160, 33)
        Me.lblMachineName.Name = "lblMachineName"
        Me.lblMachineName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMachineName.Size = New System.Drawing.Size(2, 16)
        Me.lblMachineName.TabIndex = 31
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(37, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 14)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Item : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(34, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(41, 14)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Dept : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(13, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(62, 14)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Machine : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboRprDate)
        Me.Frame1.Controls.Add(Me.txtDate2)
        Me.Frame1.Controls.Add(Me.txtDate1)
        Me.Frame1.Controls.Add(Me.lblDate1)
        Me.Frame1.Controls.Add(Me.lblDate2)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(578, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(185, 91)
        Me.Frame1.TabIndex = 18
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Date On Condition"
        '
        'cboRprDate
        '
        Me.cboRprDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboRprDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRprDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRprDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRprDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRprDate.Location = New System.Drawing.Point(20, 14)
        Me.cboRprDate.Name = "cboRprDate"
        Me.cboRprDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRprDate.Size = New System.Drawing.Size(147, 22)
        Me.cboRprDate.TabIndex = 3
        '
        'txtDate2
        '
        Me.txtDate2.AllowPromptAsInput = False
        Me.txtDate2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate2.Location = New System.Drawing.Point(66, 60)
        Me.txtDate2.Mask = "##/##/####"
        Me.txtDate2.Name = "txtDate2"
        Me.txtDate2.Size = New System.Drawing.Size(100, 20)
        Me.txtDate2.TabIndex = 5
        '
        'txtDate1
        '
        Me.txtDate1.AllowPromptAsInput = False
        Me.txtDate1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate1.Location = New System.Drawing.Point(66, 36)
        Me.txtDate1.Mask = "##/##/####"
        Me.txtDate1.Name = "txtDate1"
        Me.txtDate1.Size = New System.Drawing.Size(100, 20)
        Me.txtDate1.TabIndex = 4
        '
        'lblDate1
        '
        Me.lblDate1.AutoSize = True
        Me.lblDate1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate1.Location = New System.Drawing.Point(14, 40)
        Me.lblDate1.Name = "lblDate1"
        Me.lblDate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate1.Size = New System.Drawing.Size(49, 14)
        Me.lblDate1.TabIndex = 20
        Me.lblDate1.Text = "Date 1 : "
        '
        'lblDate2
        '
        Me.lblDate2.AutoSize = True
        Me.lblDate2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate2.Location = New System.Drawing.Point(14, 64)
        Me.lblDate2.Name = "lblDate2"
        Me.lblDate2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate2.Size = New System.Drawing.Size(49, 14)
        Me.lblDate2.TabIndex = 19
        Me.lblDate2.Text = "Date 2 : "
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
        Me.FraMovement.Location = New System.Drawing.Point(422, 410)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(341, 49)
        Me.FraMovement.TabIndex = 17
        Me.FraMovement.TabStop = False
        '
        'AData1
        '
        Me.AData1.BackColor = System.Drawing.SystemColors.Window
        Me.AData1.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AData1.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AData1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AData1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AData1.Location = New System.Drawing.Point(128, 416)
        Me.AData1.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AData1.Name = "AData1"
        Me.AData1.Size = New System.Drawing.Size(113, 23)
        Me.AData1.TabIndex = 34
        Me.AData1.Text = "Adodc1"
        Me.AData1.Visible = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 174)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 35
        '
        'frmParamItemConsMaint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(763, 459)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.AData1)
        Me.Controls.Add(Me.Report1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamItemConsMaint"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Item Consumption List during Maintenance"
        Me.Frame3.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
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