Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGaugeFixMst
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
    Public WithEvents SprdStd As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraStd As System.Windows.Forms.GroupBox
    Public WithEvents txtSize As System.Windows.Forms.TextBox
    Public WithEvents txtWearSize As System.Windows.Forms.TextBox
    Public WithEvents txtNogoSize As System.Windows.Forms.TextBox
    Public WithEvents txtGoSize As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents fraSize As System.Windows.Forms.GroupBox
    Public WithEvents txtPartName As System.Windows.Forms.TextBox
    Public WithEvents txtComponent As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtQty As System.Windows.Forms.TextBox
    Public WithEvents txtValFrequency As System.Windows.Forms.TextBox
    Public WithEvents txtIssueDate As System.Windows.Forms.TextBox
    Public WithEvents _optStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDRGNo As System.Windows.Forms.TextBox
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents cboType As System.Windows.Forms.ComboBox
    Public WithEvents txtDescription As System.Windows.Forms.TextBox
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents txtTypeNo As System.Windows.Forms.TextBox
    Public WithEvents txtVDoneOn As System.Windows.Forms.TextBox
    Public WithEvents txtVDueOn As System.Windows.Forms.TextBox
    Public WithEvents txtNumber As System.Windows.Forms.TextBox
    Public WithEvents txtModel As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchNumber As System.Windows.Forms.Button
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_9 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_10 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_8 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGaugeFixMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchNumber = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraStd = New System.Windows.Forms.GroupBox()
        Me.SprdStd = New AxFPSpreadADO.AxfpSpread()
        Me.fraSize = New System.Windows.Forms.GroupBox()
        Me.txtSize = New System.Windows.Forms.TextBox()
        Me.txtWearSize = New System.Windows.Forms.TextBox()
        Me.txtNogoSize = New System.Windows.Forms.TextBox()
        Me.txtGoSize = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtPartName = New System.Windows.Forms.TextBox()
        Me.txtComponent = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtValFrequency = New System.Windows.Forms.TextBox()
        Me.txtIssueDate = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optStatus_1 = New System.Windows.Forms.RadioButton()
        Me._optStatus_0 = New System.Windows.Forms.RadioButton()
        Me.txtDRGNo = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtTypeNo = New System.Windows.Forms.TextBox()
        Me.txtVDoneOn = New System.Windows.Forms.TextBox()
        Me.txtVDueOn = New System.Windows.Forms.TextBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_9 = New System.Windows.Forms.Label()
        Me._lblLabels_10 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.fraStd.SuspendLayout()
        CType(Me.SprdStd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraSize.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchNumber
        '
        Me.cmdSearchNumber.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchNumber.Image = CType(resources.GetObject("cmdSearchNumber.Image"), System.Drawing.Image)
        Me.cmdSearchNumber.Location = New System.Drawing.Point(238, 10)
        Me.cmdSearchNumber.Name = "cmdSearchNumber"
        Me.cmdSearchNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchNumber.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchNumber.TabIndex = 32
        Me.cmdSearchNumber.TabStop = False
        Me.cmdSearchNumber.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchNumber, "Search")
        Me.cmdSearchNumber.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(524, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 29
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
        Me.CmdView.Location = New System.Drawing.Point(464, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 37)
        Me.CmdView.TabIndex = 28
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
        Me.cmdPrint.Location = New System.Drawing.Point(344, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 26
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
        Me.CmdDelete.Location = New System.Drawing.Point(284, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 37)
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
        Me.CmdSave.Location = New System.Drawing.Point(164, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
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
        Me.CmdModify.Location = New System.Drawing.Point(104, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 37)
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
        Me.CmdAdd.Location = New System.Drawing.Point(44, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 37)
        Me.CmdAdd.TabIndex = 21
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraStd
        '
        Me.fraStd.BackColor = System.Drawing.SystemColors.Control
        Me.fraStd.Controls.Add(Me.SprdStd)
        Me.fraStd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraStd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraStd.Location = New System.Drawing.Point(0, 234)
        Me.fraStd.Name = "fraStd"
        Me.fraStd.Padding = New System.Windows.Forms.Padding(0)
        Me.fraStd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraStd.Size = New System.Drawing.Size(629, 177)
        Me.fraStd.TabIndex = 55
        Me.fraStd.TabStop = False
        Me.fraStd.Text = "Calibration Standards"
        '
        'SprdStd
        '
        Me.SprdStd.DataSource = Nothing
        Me.SprdStd.Location = New System.Drawing.Point(2, 16)
        Me.SprdStd.Name = "SprdStd"
        Me.SprdStd.OcxState = CType(resources.GetObject("SprdStd.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdStd.Size = New System.Drawing.Size(623, 157)
        Me.SprdStd.TabIndex = 16
        '
        'fraSize
        '
        Me.fraSize.BackColor = System.Drawing.SystemColors.Control
        Me.fraSize.Controls.Add(Me.txtSize)
        Me.fraSize.Controls.Add(Me.txtWearSize)
        Me.fraSize.Controls.Add(Me.txtNogoSize)
        Me.fraSize.Controls.Add(Me.txtGoSize)
        Me.fraSize.Controls.Add(Me.Label11)
        Me.fraSize.Controls.Add(Me.Label10)
        Me.fraSize.Controls.Add(Me.Label7)
        Me.fraSize.Controls.Add(Me.Label8)
        Me.fraSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSize.Location = New System.Drawing.Point(0, 232)
        Me.fraSize.Name = "fraSize"
        Me.fraSize.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSize.Size = New System.Drawing.Size(629, 179)
        Me.fraSize.TabIndex = 48
        Me.fraSize.TabStop = False
        Me.fraSize.Text = "Required Sizes"
        '
        'txtSize
        '
        Me.txtSize.AcceptsReturn = True
        Me.txtSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSize.Location = New System.Drawing.Point(120, 82)
        Me.txtSize.MaxLength = 0
        Me.txtSize.Name = "txtSize"
        Me.txtSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSize.Size = New System.Drawing.Size(163, 20)
        Me.txtSize.TabIndex = 19
        '
        'txtWearSize
        '
        Me.txtWearSize.AcceptsReturn = True
        Me.txtWearSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtWearSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWearSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWearSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWearSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWearSize.Location = New System.Drawing.Point(374, 82)
        Me.txtWearSize.MaxLength = 0
        Me.txtWearSize.Name = "txtWearSize"
        Me.txtWearSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWearSize.Size = New System.Drawing.Size(163, 20)
        Me.txtWearSize.TabIndex = 20
        '
        'txtNogoSize
        '
        Me.txtNogoSize.AcceptsReturn = True
        Me.txtNogoSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtNogoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNogoSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNogoSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNogoSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNogoSize.Location = New System.Drawing.Point(374, 58)
        Me.txtNogoSize.MaxLength = 0
        Me.txtNogoSize.Name = "txtNogoSize"
        Me.txtNogoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNogoSize.Size = New System.Drawing.Size(163, 20)
        Me.txtNogoSize.TabIndex = 18
        '
        'txtGoSize
        '
        Me.txtGoSize.AcceptsReturn = True
        Me.txtGoSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtGoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGoSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGoSize.Location = New System.Drawing.Point(120, 58)
        Me.txtGoSize.MaxLength = 0
        Me.txtGoSize.Name = "txtGoSize"
        Me.txtGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGoSize.Size = New System.Drawing.Size(163, 20)
        Me.txtGoSize.TabIndex = 17
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(78, 84)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(33, 13)
        Me.Label11.TabIndex = 52
        Me.Label11.Text = "Size :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(301, 84)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(63, 13)
        Me.Label10.TabIndex = 51
        Me.Label10.Text = "Wear Size :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(301, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(65, 13)
        Me.Label7.TabIndex = 50
        Me.Label7.Text = "Nogo Size :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(56, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(51, 13)
        Me.Label8.TabIndex = 49
        Me.Label8.Text = "Go Size :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtPartName)
        Me.Frame4.Controls.Add(Me.txtComponent)
        Me.Frame4.Controls.Add(Me.txtRemarks)
        Me.Frame4.Controls.Add(Me.txtQty)
        Me.Frame4.Controls.Add(Me.txtValFrequency)
        Me.Frame4.Controls.Add(Me.txtIssueDate)
        Me.Frame4.Controls.Add(Me.Frame1)
        Me.Frame4.Controls.Add(Me.txtDRGNo)
        Me.Frame4.Controls.Add(Me.txtLocation)
        Me.Frame4.Controls.Add(Me.cboType)
        Me.Frame4.Controls.Add(Me.txtDescription)
        Me.Frame4.Controls.Add(Me.txtCustomer)
        Me.Frame4.Controls.Add(Me.txtTypeNo)
        Me.Frame4.Controls.Add(Me.txtVDoneOn)
        Me.Frame4.Controls.Add(Me.txtVDueOn)
        Me.Frame4.Controls.Add(Me.txtNumber)
        Me.Frame4.Controls.Add(Me.txtModel)
        Me.Frame4.Controls.Add(Me.cmdSearchNumber)
        Me.Frame4.Controls.Add(Me.Label3)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Controls.Add(Me._lblLabels_11)
        Me.Frame4.Controls.Add(Me._lblLabels_9)
        Me.Frame4.Controls.Add(Me._lblLabels_10)
        Me.Frame4.Controls.Add(Me.Label27)
        Me.Frame4.Controls.Add(Me._lblLabels_8)
        Me.Frame4.Controls.Add(Me._lblLabels_7)
        Me.Frame4.Controls.Add(Me._lblLabels_6)
        Me.Frame4.Controls.Add(Me._lblLabels_5)
        Me.Frame4.Controls.Add(Me._lblLabels_4)
        Me.Frame4.Controls.Add(Me._lblLabels_3)
        Me.Frame4.Controls.Add(Me._lblLabels_1)
        Me.Frame4.Controls.Add(Me.lblMkey)
        Me.Frame4.Controls.Add(Me._lblLabels_0)
        Me.Frame4.Controls.Add(Me._lblLabels_2)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -6)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(630, 240)
        Me.Frame4.TabIndex = 35
        Me.Frame4.TabStop = False
        '
        'txtPartName
        '
        Me.txtPartName.AcceptsReturn = True
        Me.txtPartName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPartName.Location = New System.Drawing.Point(122, 214)
        Me.txtPartName.MaxLength = 0
        Me.txtPartName.Name = "txtPartName"
        Me.txtPartName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartName.Size = New System.Drawing.Size(165, 20)
        Me.txtPartName.TabIndex = 14
        '
        'txtComponent
        '
        Me.txtComponent.AcceptsReturn = True
        Me.txtComponent.BackColor = System.Drawing.SystemColors.Window
        Me.txtComponent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComponent.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComponent.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComponent.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtComponent.Location = New System.Drawing.Point(374, 114)
        Me.txtComponent.MaxLength = 15
        Me.txtComponent.Name = "txtComponent"
        Me.txtComponent.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComponent.Size = New System.Drawing.Size(239, 20)
        Me.txtComponent.TabIndex = 6
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(374, 194)
        Me.txtRemarks.MaxLength = 15
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(241, 20)
        Me.txtRemarks.TabIndex = 15
        '
        'txtQty
        '
        Me.txtQty.AcceptsReturn = True
        Me.txtQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtQty.Location = New System.Drawing.Point(122, 194)
        Me.txtQty.MaxLength = 0
        Me.txtQty.Name = "txtQty"
        Me.txtQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQty.Size = New System.Drawing.Size(91, 20)
        Me.txtQty.TabIndex = 13
        '
        'txtValFrequency
        '
        Me.txtValFrequency.AcceptsReturn = True
        Me.txtValFrequency.BackColor = System.Drawing.SystemColors.Window
        Me.txtValFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValFrequency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtValFrequency.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValFrequency.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtValFrequency.Location = New System.Drawing.Point(122, 174)
        Me.txtValFrequency.MaxLength = 0
        Me.txtValFrequency.Name = "txtValFrequency"
        Me.txtValFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValFrequency.Size = New System.Drawing.Size(91, 20)
        Me.txtValFrequency.TabIndex = 11
        '
        'txtIssueDate
        '
        Me.txtIssueDate.AcceptsReturn = True
        Me.txtIssueDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtIssueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssueDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIssueDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssueDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIssueDate.Location = New System.Drawing.Point(374, 174)
        Me.txtIssueDate.MaxLength = 0
        Me.txtIssueDate.Name = "txtIssueDate"
        Me.txtIssueDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIssueDate.Size = New System.Drawing.Size(95, 20)
        Me.txtIssueDate.TabIndex = 12
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optStatus_1)
        Me.Frame1.Controls.Add(Me._optStatus_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(382, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(231, 31)
        Me.Frame1.TabIndex = 47
        Me.Frame1.TabStop = False
        '
        '_optStatus_1
        '
        Me._optStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_1, CType(1, Short))
        Me._optStatus_1.Location = New System.Drawing.Point(118, 12)
        Me._optStatus_1.Name = "_optStatus_1"
        Me._optStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_1.Size = New System.Drawing.Size(75, 13)
        Me._optStatus_1.TabIndex = 31
        Me._optStatus_1.TabStop = True
        Me._optStatus_1.Text = "Inactive"
        Me._optStatus_1.UseVisualStyleBackColor = False
        '
        '_optStatus_0
        '
        Me._optStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_0.Checked = True
        Me._optStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_0, CType(0, Short))
        Me._optStatus_0.Location = New System.Drawing.Point(8, 12)
        Me._optStatus_0.Name = "_optStatus_0"
        Me._optStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_0.Size = New System.Drawing.Size(69, 13)
        Me._optStatus_0.TabIndex = 30
        Me._optStatus_0.TabStop = True
        Me._optStatus_0.Text = "Active"
        Me._optStatus_0.UseVisualStyleBackColor = False
        '
        'txtDRGNo
        '
        Me.txtDRGNo.AcceptsReturn = True
        Me.txtDRGNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDRGNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDRGNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDRGNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDRGNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDRGNo.Location = New System.Drawing.Point(374, 134)
        Me.txtDRGNo.MaxLength = 15
        Me.txtDRGNo.Name = "txtDRGNo"
        Me.txtDRGNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDRGNo.Size = New System.Drawing.Size(239, 20)
        Me.txtDRGNo.TabIndex = 8
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocation.Location = New System.Drawing.Point(122, 92)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(491, 20)
        Me.txtLocation.TabIndex = 4
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboType.Location = New System.Drawing.Point(122, 112)
        Me.cboType.Name = "cboType"
        Me.cboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboType.Size = New System.Drawing.Size(163, 22)
        Me.cboType.TabIndex = 5
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = True
        Me.txtDescription.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescription.Location = New System.Drawing.Point(122, 52)
        Me.txtDescription.MaxLength = 0
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescription.Size = New System.Drawing.Size(491, 20)
        Me.txtDescription.TabIndex = 2
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomer.Location = New System.Drawing.Point(122, 72)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(491, 20)
        Me.txtCustomer.TabIndex = 3
        '
        'txtTypeNo
        '
        Me.txtTypeNo.AcceptsReturn = True
        Me.txtTypeNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTypeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTypeNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTypeNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTypeNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTypeNo.Location = New System.Drawing.Point(122, 134)
        Me.txtTypeNo.MaxLength = 0
        Me.txtTypeNo.Name = "txtTypeNo"
        Me.txtTypeNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTypeNo.Size = New System.Drawing.Size(163, 20)
        Me.txtTypeNo.TabIndex = 7
        '
        'txtVDoneOn
        '
        Me.txtVDoneOn.AcceptsReturn = True
        Me.txtVDoneOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDoneOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDoneOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDoneOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDoneOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVDoneOn.Location = New System.Drawing.Point(122, 154)
        Me.txtVDoneOn.MaxLength = 0
        Me.txtVDoneOn.Name = "txtVDoneOn"
        Me.txtVDoneOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDoneOn.Size = New System.Drawing.Size(91, 20)
        Me.txtVDoneOn.TabIndex = 9
        '
        'txtVDueOn
        '
        Me.txtVDueOn.AcceptsReturn = True
        Me.txtVDueOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDueOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDueOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDueOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDueOn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVDueOn.Location = New System.Drawing.Point(374, 154)
        Me.txtVDueOn.MaxLength = 0
        Me.txtVDueOn.Name = "txtVDueOn"
        Me.txtVDueOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDueOn.Size = New System.Drawing.Size(95, 20)
        Me.txtVDueOn.TabIndex = 10
        '
        'txtNumber
        '
        Me.txtNumber.AcceptsReturn = True
        Me.txtNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumber.Location = New System.Drawing.Point(122, 12)
        Me.txtNumber.MaxLength = 0
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumber.Size = New System.Drawing.Size(115, 20)
        Me.txtNumber.TabIndex = 0
        '
        'txtModel
        '
        Me.txtModel.AcceptsReturn = True
        Me.txtModel.BackColor = System.Drawing.SystemColors.Window
        Me.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModel.Location = New System.Drawing.Point(122, 32)
        Me.txtModel.MaxLength = 0
        Me.txtModel.Name = "txtModel"
        Me.txtModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModel.Size = New System.Drawing.Size(491, 20)
        Me.txtModel.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(14, 218)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(96, 13)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "Instrument Used :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(298, 116)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(73, 13)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Component :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(305, 196)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Remarks :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.AutoSize = True
        Me._lblLabels_11.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_11, CType(11, Short))
        Me._lblLabels_11.Location = New System.Drawing.Point(5, 197)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(103, 13)
        Me._lblLabels_11.TabIndex = 56
        Me._lblLabels_11.Text = "Qty Manuf./Check :"
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
        Me._lblLabels_9.Location = New System.Drawing.Point(25, 177)
        Me._lblLabels_9.Name = "_lblLabels_9"
        Me._lblLabels_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_9.Size = New System.Drawing.Size(86, 13)
        Me._lblLabels_9.TabIndex = 54
        Me._lblLabels_9.Text = "Val. Frequency :"
        Me._lblLabels_9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_10
        '
        Me._lblLabels_10.AutoSize = True
        Me._lblLabels_10.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_10, CType(10, Short))
        Me._lblLabels_10.Location = New System.Drawing.Point(305, 177)
        Me._lblLabels_10.Name = "_lblLabels_10"
        Me._lblLabels_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_10.Size = New System.Drawing.Size(65, 13)
        Me._lblLabels_10.TabIndex = 53
        Me._lblLabels_10.Text = "Issue Date :"
        Me._lblLabels_10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(305, 136)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(60, 13)
        Me.Label27.TabIndex = 46
        Me.Label27.Text = "DRG. No. :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.AutoSize = True
        Me._lblLabels_8.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_8, CType(8, Short))
        Me._lblLabels_8.Location = New System.Drawing.Point(61, 94)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(56, 13)
        Me._lblLabels_8.TabIndex = 45
        Me._lblLabels_8.Text = "Location :"
        Me._lblLabels_8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.AutoSize = True
        Me._lblLabels_7.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_7, CType(7, Short))
        Me._lblLabels_7.Location = New System.Drawing.Point(82, 114)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(37, 13)
        Me._lblLabels_7.TabIndex = 44
        Me._lblLabels_7.Text = "Type :"
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
        Me._lblLabels_6.Location = New System.Drawing.Point(46, 54)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(71, 13)
        Me._lblLabels_6.TabIndex = 43
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
        Me._lblLabels_5.Location = New System.Drawing.Point(58, 74)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(62, 13)
        Me._lblLabels_5.TabIndex = 42
        Me._lblLabels_5.Text = "Customer :"
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
        Me._lblLabels_4.Location = New System.Drawing.Point(62, 136)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(55, 13)
        Me._lblLabels_4.TabIndex = 41
        Me._lblLabels_4.Text = "Type No :"
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
        Me._lblLabels_3.Location = New System.Drawing.Point(44, 157)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(69, 13)
        Me._lblLabels_3.TabIndex = 40
        Me._lblLabels_3.Text = "V. Done On :"
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
        Me._lblLabels_1.Location = New System.Drawing.Point(305, 157)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(59, 13)
        Me._lblLabels_1.TabIndex = 39
        Me._lblLabels_1.Text = "V. DueOn :"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(574, 136)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 38
        Me.lblMkey.Text = "lblMkey"
        Me.lblMkey.Visible = False
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(13, 14)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(54, 13)
        Me._lblLabels_0.TabIndex = 37
        Me._lblLabels_0.Text = "Doc No. :"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(13, 36)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(46, 13)
        Me._lblLabels_2.TabIndex = 36
        Me._lblLabels_2.Text = "Model :"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(627, 411)
        Me.SprdView.TabIndex = 34
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 18)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 57
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 408)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(627, 51)
        Me.FraMovement.TabIndex = 33
        Me.FraMovement.TabStop = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(404, 10)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.cmdPreview.TabIndex = 27
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(224, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdSavePrint.TabIndex = 24
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'optStatus
        '
        '
        'frmGaugeFixMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(630, 460)
        Me.Controls.Add(Me.fraStd)
        Me.Controls.Add(Me.fraSize)
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
        Me.Name = "frmGaugeFixMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Gauge Fixture Master"
        Me.fraStd.ResumeLayout(False)
        CType(Me.SprdStd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraSize.ResumeLayout(False)
        Me.fraSize.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame1.ResumeLayout(False)
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