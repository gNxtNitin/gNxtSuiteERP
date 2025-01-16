Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmVoltmeterMst
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
    Public WithEvents SprdPE As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraStd As System.Windows.Forms.GroupBox
    Public WithEvents txtShuntRatio As System.Windows.Forms.TextBox
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents _optStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDescription As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDocNo As System.Windows.Forms.Button
    Public WithEvents txtDocNo As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchDeptCode As System.Windows.Forms.Button
    Public WithEvents txtDeptCode As System.Windows.Forms.TextBox
    Public WithEvents txtMake As System.Windows.Forms.TextBox
    Public WithEvents txtFrequency As System.Windows.Forms.TextBox
    Public WithEvents txtRange As System.Windows.Forms.TextBox
    Public WithEvents cboCalibSource As System.Windows.Forms.ComboBox
    Public WithEvents txtMakersNo As System.Windows.Forms.TextBox
    Public WithEvents txtLC As System.Windows.Forms.TextBox
    Public WithEvents txtLastCalibDate As System.Windows.Forms.TextBox
    Public WithEvents txtCalibDueDate As System.Windows.Forms.TextBox
    Public WithEvents txtENo As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_10 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_8 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents lblDeptDesc As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_9 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents optStatus As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVoltmeterMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchDocNo = New System.Windows.Forms.Button()
        Me.CmdSearchDeptCode = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraStd = New System.Windows.Forms.GroupBox()
        Me.SprdPE = New AxFPSpreadADO.AxfpSpread()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtShuntRatio = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optStatus_1 = New System.Windows.Forms.RadioButton()
        Me._optStatus_0 = New System.Windows.Forms.RadioButton()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.txtDeptCode = New System.Windows.Forms.TextBox()
        Me.txtMake = New System.Windows.Forms.TextBox()
        Me.txtFrequency = New System.Windows.Forms.TextBox()
        Me.txtRange = New System.Windows.Forms.TextBox()
        Me.cboCalibSource = New System.Windows.Forms.ComboBox()
        Me.txtMakersNo = New System.Windows.Forms.TextBox()
        Me.txtLC = New System.Windows.Forms.TextBox()
        Me.txtLastCalibDate = New System.Windows.Forms.TextBox()
        Me.txtCalibDueDate = New System.Windows.Forms.TextBox()
        Me.txtENo = New System.Windows.Forms.TextBox()
        Me._lblLabels_10 = New System.Windows.Forms.Label()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.lblDeptDesc = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_9 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.fraStd.SuspendLayout()
        CType(Me.SprdPE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame4.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchDocNo
        '
        Me.cmdSearchDocNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDocNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDocNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDocNo.Image = CType(resources.GetObject("cmdSearchDocNo.Image"), System.Drawing.Image)
        Me.cmdSearchDocNo.Location = New System.Drawing.Point(216, 19)
        Me.cmdSearchDocNo.Name = "cmdSearchDocNo"
        Me.cmdSearchDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDocNo.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchDocNo.TabIndex = 45
        Me.cmdSearchDocNo.TabStop = False
        Me.cmdSearchDocNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDocNo, "Search")
        Me.cmdSearchDocNo.UseVisualStyleBackColor = False
        '
        'CmdSearchDeptCode
        '
        Me.CmdSearchDeptCode.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchDeptCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchDeptCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchDeptCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchDeptCode.Image = CType(resources.GetObject("CmdSearchDeptCode.Image"), System.Drawing.Image)
        Me.CmdSearchDeptCode.Location = New System.Drawing.Point(216, 163)
        Me.CmdSearchDeptCode.Name = "CmdSearchDeptCode"
        Me.CmdSearchDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchDeptCode.Size = New System.Drawing.Size(27, 21)
        Me.CmdSearchDeptCode.TabIndex = 42
        Me.CmdSearchDeptCode.TabStop = False
        Me.CmdSearchDeptCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchDeptCode, "Search")
        Me.CmdSearchDeptCode.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(500, 14)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 25
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(440, 14)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 37)
        Me.CmdView.TabIndex = 24
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(320, 14)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 22
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(260, 14)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 37)
        Me.CmdDelete.TabIndex = 21
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
        Me.CmdSave.Location = New System.Drawing.Point(140, 14)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
        Me.CmdSave.TabIndex = 19
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
        Me.CmdModify.Location = New System.Drawing.Point(80, 14)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 37)
        Me.CmdModify.TabIndex = 18
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
        Me.CmdAdd.Location = New System.Drawing.Point(20, 14)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 37)
        Me.CmdAdd.TabIndex = 17
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraStd
        '
        Me.fraStd.BackColor = System.Drawing.SystemColors.Control
        Me.fraStd.Controls.Add(Me.SprdPE)
        Me.fraStd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraStd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraStd.Location = New System.Drawing.Point(0, 240)
        Me.fraStd.Name = "fraStd"
        Me.fraStd.Padding = New System.Windows.Forms.Padding(0)
        Me.fraStd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraStd.Size = New System.Drawing.Size(581, 187)
        Me.fraStd.TabIndex = 38
        Me.fraStd.TabStop = False
        Me.fraStd.Text = "Calibration Permissible Errors"
        '
        'SprdPE
        '
        Me.SprdPE.DataSource = Nothing
        Me.SprdPE.Location = New System.Drawing.Point(8, 16)
        Me.SprdPE.Name = "SprdPE"
        Me.SprdPE.OcxState = CType(resources.GetObject("SprdPE.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPE.Size = New System.Drawing.Size(563, 165)
        Me.SprdPE.TabIndex = 14
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtShuntRatio)
        Me.Frame4.Controls.Add(Me.txtLocation)
        Me.Frame4.Controls.Add(Me.Frame1)
        Me.Frame4.Controls.Add(Me.txtDescription)
        Me.Frame4.Controls.Add(Me.cmdSearchDocNo)
        Me.Frame4.Controls.Add(Me.txtDocNo)
        Me.Frame4.Controls.Add(Me.CmdSearchDeptCode)
        Me.Frame4.Controls.Add(Me.txtDeptCode)
        Me.Frame4.Controls.Add(Me.txtMake)
        Me.Frame4.Controls.Add(Me.txtFrequency)
        Me.Frame4.Controls.Add(Me.txtRange)
        Me.Frame4.Controls.Add(Me.cboCalibSource)
        Me.Frame4.Controls.Add(Me.txtMakersNo)
        Me.Frame4.Controls.Add(Me.txtLC)
        Me.Frame4.Controls.Add(Me.txtLastCalibDate)
        Me.Frame4.Controls.Add(Me.txtCalibDueDate)
        Me.Frame4.Controls.Add(Me.txtENo)
        Me.Frame4.Controls.Add(Me._lblLabels_10)
        Me.Frame4.Controls.Add(Me._lblLabels_8)
        Me.Frame4.Controls.Add(Me._lblLabels_0)
        Me.Frame4.Controls.Add(Me.lblMkey)
        Me.Frame4.Controls.Add(Me.lblDeptDesc)
        Me.Frame4.Controls.Add(Me.Label3)
        Me.Frame4.Controls.Add(Me._lblLabels_11)
        Me.Frame4.Controls.Add(Me._lblLabels_9)
        Me.Frame4.Controls.Add(Me.Label27)
        Me.Frame4.Controls.Add(Me._lblLabels_7)
        Me.Frame4.Controls.Add(Me._lblLabels_6)
        Me.Frame4.Controls.Add(Me._lblLabels_5)
        Me.Frame4.Controls.Add(Me._lblLabels_4)
        Me.Frame4.Controls.Add(Me._lblLabels_3)
        Me.Frame4.Controls.Add(Me._lblLabels_1)
        Me.Frame4.Controls.Add(Me._lblLabels_2)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -6)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(580, 246)
        Me.Frame4.TabIndex = 28
        Me.Frame4.TabStop = False
        '
        'txtShuntRatio
        '
        Me.txtShuntRatio.AcceptsReturn = True
        Me.txtShuntRatio.BackColor = System.Drawing.SystemColors.Window
        Me.txtShuntRatio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtShuntRatio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtShuntRatio.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtShuntRatio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtShuntRatio.Location = New System.Drawing.Point(122, 140)
        Me.txtShuntRatio.MaxLength = 0
        Me.txtShuntRatio.Name = "txtShuntRatio"
        Me.txtShuntRatio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtShuntRatio.Size = New System.Drawing.Size(163, 19)
        Me.txtShuntRatio.TabIndex = 8
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocation.Location = New System.Drawing.Point(380, 116)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(163, 19)
        Me.txtLocation.TabIndex = 7
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optStatus_1)
        Me.Frame1.Controls.Add(Me._optStatus_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(312, 8)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(231, 34)
        Me.Frame1.TabIndex = 46
        Me.Frame1.TabStop = False
        '
        '_optStatus_1
        '
        Me._optStatus_1.AutoSize = True
        Me._optStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_1, CType(1, Short))
        Me._optStatus_1.Location = New System.Drawing.Point(134, 12)
        Me._optStatus_1.Name = "_optStatus_1"
        Me._optStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_1.Size = New System.Drawing.Size(64, 17)
        Me._optStatus_1.TabIndex = 15
        Me._optStatus_1.TabStop = True
        Me._optStatus_1.Text = "Inactive"
        Me._optStatus_1.UseVisualStyleBackColor = False
        '
        '_optStatus_0
        '
        Me._optStatus_0.AutoSize = True
        Me._optStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_0.Checked = True
        Me._optStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_0, CType(0, Short))
        Me._optStatus_0.Location = New System.Drawing.Point(24, 12)
        Me._optStatus_0.Name = "_optStatus_0"
        Me._optStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_0.Size = New System.Drawing.Size(56, 17)
        Me._optStatus_0.TabIndex = 16
        Me._optStatus_0.TabStop = True
        Me._optStatus_0.Text = "Active"
        Me._optStatus_0.UseVisualStyleBackColor = False
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = True
        Me.txtDescription.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescription.Location = New System.Drawing.Point(122, 44)
        Me.txtDescription.MaxLength = 0
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescription.Size = New System.Drawing.Size(421, 19)
        Me.txtDescription.TabIndex = 1
        '
        'txtDocNo
        '
        Me.txtDocNo.AcceptsReturn = True
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocNo.Location = New System.Drawing.Point(122, 20)
        Me.txtDocNo.MaxLength = 0
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocNo.Size = New System.Drawing.Size(91, 19)
        Me.txtDocNo.TabIndex = 0
        '
        'txtDeptCode
        '
        Me.txtDeptCode.AcceptsReturn = True
        Me.txtDeptCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeptCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeptCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeptCode.Location = New System.Drawing.Point(122, 164)
        Me.txtDeptCode.MaxLength = 0
        Me.txtDeptCode.Name = "txtDeptCode"
        Me.txtDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeptCode.Size = New System.Drawing.Size(91, 19)
        Me.txtDeptCode.TabIndex = 9
        '
        'txtMake
        '
        Me.txtMake.AcceptsReturn = True
        Me.txtMake.BackColor = System.Drawing.SystemColors.Window
        Me.txtMake.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMake.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMake.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMake.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMake.Location = New System.Drawing.Point(122, 92)
        Me.txtMake.MaxLength = 0
        Me.txtMake.Name = "txtMake"
        Me.txtMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMake.Size = New System.Drawing.Size(163, 19)
        Me.txtMake.TabIndex = 4
        '
        'txtFrequency
        '
        Me.txtFrequency.AcceptsReturn = True
        Me.txtFrequency.BackColor = System.Drawing.SystemColors.Window
        Me.txtFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFrequency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFrequency.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrequency.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFrequency.Location = New System.Drawing.Point(122, 188)
        Me.txtFrequency.MaxLength = 0
        Me.txtFrequency.Name = "txtFrequency"
        Me.txtFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFrequency.Size = New System.Drawing.Size(91, 19)
        Me.txtFrequency.TabIndex = 10
        '
        'txtRange
        '
        Me.txtRange.AcceptsReturn = True
        Me.txtRange.BackColor = System.Drawing.SystemColors.Window
        Me.txtRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRange.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRange.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRange.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRange.Location = New System.Drawing.Point(380, 92)
        Me.txtRange.MaxLength = 15
        Me.txtRange.Name = "txtRange"
        Me.txtRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRange.Size = New System.Drawing.Size(163, 19)
        Me.txtRange.TabIndex = 5
        '
        'cboCalibSource
        '
        Me.cboCalibSource.BackColor = System.Drawing.SystemColors.Window
        Me.cboCalibSource.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCalibSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCalibSource.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCalibSource.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCalibSource.Location = New System.Drawing.Point(452, 187)
        Me.cboCalibSource.Name = "cboCalibSource"
        Me.cboCalibSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCalibSource.Size = New System.Drawing.Size(91, 22)
        Me.cboCalibSource.TabIndex = 11
        '
        'txtMakersNo
        '
        Me.txtMakersNo.AcceptsReturn = True
        Me.txtMakersNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMakersNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMakersNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMakersNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMakersNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMakersNo.Location = New System.Drawing.Point(380, 68)
        Me.txtMakersNo.MaxLength = 0
        Me.txtMakersNo.Name = "txtMakersNo"
        Me.txtMakersNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMakersNo.Size = New System.Drawing.Size(163, 19)
        Me.txtMakersNo.TabIndex = 3
        '
        'txtLC
        '
        Me.txtLC.AcceptsReturn = True
        Me.txtLC.BackColor = System.Drawing.SystemColors.Window
        Me.txtLC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLC.Location = New System.Drawing.Point(122, 116)
        Me.txtLC.MaxLength = 0
        Me.txtLC.Name = "txtLC"
        Me.txtLC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLC.Size = New System.Drawing.Size(163, 19)
        Me.txtLC.TabIndex = 6
        '
        'txtLastCalibDate
        '
        Me.txtLastCalibDate.AcceptsReturn = True
        Me.txtLastCalibDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastCalibDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLastCalibDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastCalibDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastCalibDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLastCalibDate.Location = New System.Drawing.Point(122, 212)
        Me.txtLastCalibDate.MaxLength = 0
        Me.txtLastCalibDate.Name = "txtLastCalibDate"
        Me.txtLastCalibDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastCalibDate.Size = New System.Drawing.Size(91, 19)
        Me.txtLastCalibDate.TabIndex = 12
        '
        'txtCalibDueDate
        '
        Me.txtCalibDueDate.AcceptsReturn = True
        Me.txtCalibDueDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCalibDueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibDueDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCalibDueDate.Enabled = False
        Me.txtCalibDueDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalibDueDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCalibDueDate.Location = New System.Drawing.Point(452, 212)
        Me.txtCalibDueDate.MaxLength = 0
        Me.txtCalibDueDate.Name = "txtCalibDueDate"
        Me.txtCalibDueDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCalibDueDate.Size = New System.Drawing.Size(91, 19)
        Me.txtCalibDueDate.TabIndex = 13
        '
        'txtENo
        '
        Me.txtENo.AcceptsReturn = True
        Me.txtENo.BackColor = System.Drawing.SystemColors.Window
        Me.txtENo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtENo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtENo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtENo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtENo.Location = New System.Drawing.Point(122, 68)
        Me.txtENo.MaxLength = 0
        Me.txtENo.Name = "txtENo"
        Me.txtENo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtENo.Size = New System.Drawing.Size(163, 19)
        Me.txtENo.TabIndex = 2
        '
        '_lblLabels_10
        '
        Me._lblLabels_10.AutoSize = True
        Me._lblLabels_10.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_10, CType(10, Short))
        Me._lblLabels_10.Location = New System.Drawing.Point(37, 143)
        Me._lblLabels_10.Name = "_lblLabels_10"
        Me._lblLabels_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_10.Size = New System.Drawing.Size(71, 13)
        Me._lblLabels_10.TabIndex = 48
        Me._lblLabels_10.Text = "Shunt Ratio :"
        Me._lblLabels_10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.AutoSize = True
        Me._lblLabels_8.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_8, CType(8, Short))
        Me._lblLabels_8.Location = New System.Drawing.Point(311, 119)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(56, 13)
        Me._lblLabels_8.TabIndex = 47
        Me._lblLabels_8.Text = "Location :"
        Me._lblLabels_8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(7, 23)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(54, 13)
        Me._lblLabels_0.TabIndex = 44
        Me._lblLabels_0.Text = "Doc No. :"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(272, 21)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 43
        Me.lblMkey.Text = "lblMkey"
        '
        'lblDeptDesc
        '
        Me.lblDeptDesc.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeptDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeptDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeptDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeptDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptDesc.Location = New System.Drawing.Point(248, 164)
        Me.lblDeptDesc.Name = "lblDeptDesc"
        Me.lblDeptDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeptDesc.Size = New System.Drawing.Size(295, 19)
        Me.lblDeptDesc.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(39, 167)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Department :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.AutoSize = True
        Me._lblLabels_11.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_11, CType(11, Short))
        Me._lblLabels_11.Location = New System.Drawing.Point(73, 95)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(41, 13)
        Me._lblLabels_11.TabIndex = 39
        Me._lblLabels_11.Text = "Make :"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_9
        '
        Me._lblLabels_9.AutoSize = True
        Me._lblLabels_9.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_9, CType(9, Short))
        Me._lblLabels_9.Location = New System.Drawing.Point(45, 191)
        Me._lblLabels_9.Name = "_lblLabels_9"
        Me._lblLabels_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_9.Size = New System.Drawing.Size(65, 13)
        Me._lblLabels_9.TabIndex = 37
        Me._lblLabels_9.Text = "Frequency :"
        Me._lblLabels_9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(323, 95)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(45, 13)
        Me.Label27.TabIndex = 36
        Me.Label27.Text = "Range :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.AutoSize = True
        Me._lblLabels_7.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_7, CType(7, Short))
        Me._lblLabels_7.Location = New System.Drawing.Point(361, 191)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(76, 13)
        Me._lblLabels_7.TabIndex = 35
        Me._lblLabels_7.Text = "Calib Source :"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.AutoSize = True
        Me._lblLabels_6.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_6, CType(6, Short))
        Me._lblLabels_6.Location = New System.Drawing.Point(40, 47)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(71, 13)
        Me._lblLabels_6.TabIndex = 34
        Me._lblLabels_6.Text = "Description :"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.AutoSize = True
        Me._lblLabels_5.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_5, CType(5, Short))
        Me._lblLabels_5.Location = New System.Drawing.Point(299, 71)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(68, 13)
        Me._lblLabels_5.TabIndex = 33
        Me._lblLabels_5.Text = "Makers No :"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.AutoSize = True
        Me._lblLabels_4.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_4, CType(4, Short))
        Me._lblLabels_4.Location = New System.Drawing.Point(81, 119)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(31, 13)
        Me._lblLabels_4.TabIndex = 32
        Me._lblLabels_4.Text = "L.C. :"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = True
        Me._lblLabels_3.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_3, CType(3, Short))
        Me._lblLabels_3.Location = New System.Drawing.Point(17, 215)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(89, 13)
        Me._lblLabels_3.TabIndex = 31
        Me._lblLabels_3.Text = "Last Calib Date :"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(347, 215)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(89, 13)
        Me._lblLabels_1.TabIndex = 30
        Me._lblLabels_1.Text = "Calib Due Date :"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(25, 71)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(86, 13)
        Me._lblLabels_2.TabIndex = 29
        Me._lblLabels_2.Text = "Equipment No :"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(8, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(571, 423)
        Me.SprdView.TabIndex = 27
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 18)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 40
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 428)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(579, 55)
        Me.FraMovement.TabIndex = 26
        Me.FraMovement.TabStop = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(380, 14)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.cmdPreview.TabIndex = 23
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(200, 14)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdSavePrint.TabIndex = 20
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'optStatus
        '
        '
        'frmVoltmeterMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(582, 484)
        Me.Controls.Add(Me.fraStd)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVoltmeterMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Process Instruments Calibration Master"
        Me.fraStd.ResumeLayout(False)
        CType(Me.SprdPE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).EndInit()
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