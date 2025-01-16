Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmIMTEMst
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
    Public WithEvents txtCalibBy As System.Windows.Forms.TextBox
    Public WithEvents txtCertNo As System.Windows.Forms.TextBox
    Public WithEvents txtModel As System.Windows.Forms.TextBox
    Public WithEvents txtCalibValid As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_25 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_24 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_23 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_22 As System.Windows.Forms.Label
    Public WithEvents fraMaster As System.Windows.Forms.GroupBox
    Public WithEvents txtWearSize As System.Windows.Forms.TextBox
    Public WithEvents txtGoSize As System.Windows.Forms.TextBox
    Public WithEvents txtBasicSize As System.Windows.Forms.TextBox
    Public WithEvents txtNoGoSize As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_20 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_19 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_18 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_17 As System.Windows.Forms.Label
    Public WithEvents fraSize As System.Windows.Forms.GroupBox
    Public WithEvents txtUnitRange As System.Windows.Forms.TextBox
    Public WithEvents txtMaxRange As System.Windows.Forms.TextBox
    Public WithEvents txtMinRange As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_16 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_15 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_14 As System.Windows.Forms.Label
    Public WithEvents fraRange As System.Windows.Forms.GroupBox
    Public WithEvents txtSuppCustCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSuppCustCode As System.Windows.Forms.Button
    Public WithEvents txtSuppCustName As System.Windows.Forms.TextBox
    Public WithEvents chkMasterInst As System.Windows.Forms.CheckBox
    Public WithEvents txtLC As System.Windows.Forms.TextBox
    Public WithEvents txtENO As System.Windows.Forms.TextBox
    Public WithEvents txtMarkersNo As System.Windows.Forms.TextBox
    Public WithEvents txtMake As System.Windows.Forms.TextBox
    Public WithEvents txtRange As System.Windows.Forms.TextBox
    Public WithEvents cboType As System.Windows.Forms.ComboBox
    Public WithEvents txtCalibOK As System.Windows.Forms.TextBox
    Public WithEvents txtLCDate As System.Windows.Forms.TextBox
    Public WithEvents cboCaliFacil As System.Windows.Forms.ComboBox
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents txtIssueTo As System.Windows.Forms.TextBox
    Public WithEvents cmdItemCode As System.Windows.Forms.Button
    Public WithEvents _optStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtCDate As System.Windows.Forms.TextBox
    Public WithEvents txtValFrequency As System.Windows.Forms.TextBox
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents txtDescription As System.Windows.Forms.TextBox
    Public WithEvents txtIssueDate As System.Windows.Forms.TextBox
    Public WithEvents txtNumber As System.Windows.Forms.TextBox
    Public WithEvents txtItemCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchNumber As System.Windows.Forms.Button
    Public WithEvents _lblLabels_28 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_26 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_21 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_10 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_13 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_12 As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_9 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_8 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
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
    Public WithEvents _lblLabels_27 As System.Windows.Forms.Label
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents optStatus As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIMTEMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchSuppCustCode = New System.Windows.Forms.Button()
        Me.cmdItemCode = New System.Windows.Forms.Button()
        Me.cmdSearchNumber = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraMaster = New System.Windows.Forms.GroupBox()
        Me.txtCalibBy = New System.Windows.Forms.TextBox()
        Me.txtCertNo = New System.Windows.Forms.TextBox()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.txtCalibValid = New System.Windows.Forms.TextBox()
        Me._lblLabels_25 = New System.Windows.Forms.Label()
        Me._lblLabels_24 = New System.Windows.Forms.Label()
        Me._lblLabels_23 = New System.Windows.Forms.Label()
        Me._lblLabels_22 = New System.Windows.Forms.Label()
        Me.fraSize = New System.Windows.Forms.GroupBox()
        Me.txtWearSize = New System.Windows.Forms.TextBox()
        Me.txtGoSize = New System.Windows.Forms.TextBox()
        Me.txtBasicSize = New System.Windows.Forms.TextBox()
        Me.txtNoGoSize = New System.Windows.Forms.TextBox()
        Me._lblLabels_20 = New System.Windows.Forms.Label()
        Me._lblLabels_19 = New System.Windows.Forms.Label()
        Me._lblLabels_18 = New System.Windows.Forms.Label()
        Me._lblLabels_17 = New System.Windows.Forms.Label()
        Me.fraRange = New System.Windows.Forms.GroupBox()
        Me.txtUnitRange = New System.Windows.Forms.TextBox()
        Me.txtMaxRange = New System.Windows.Forms.TextBox()
        Me.txtMinRange = New System.Windows.Forms.TextBox()
        Me._lblLabels_16 = New System.Windows.Forms.Label()
        Me._lblLabels_15 = New System.Windows.Forms.Label()
        Me._lblLabels_14 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtSuppCustCode = New System.Windows.Forms.TextBox()
        Me.txtSuppCustName = New System.Windows.Forms.TextBox()
        Me.chkMasterInst = New System.Windows.Forms.CheckBox()
        Me.txtLC = New System.Windows.Forms.TextBox()
        Me.txtENO = New System.Windows.Forms.TextBox()
        Me.txtMarkersNo = New System.Windows.Forms.TextBox()
        Me.txtMake = New System.Windows.Forms.TextBox()
        Me.txtRange = New System.Windows.Forms.TextBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.txtCalibOK = New System.Windows.Forms.TextBox()
        Me.txtLCDate = New System.Windows.Forms.TextBox()
        Me.cboCaliFacil = New System.Windows.Forms.ComboBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.txtIssueTo = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optStatus_1 = New System.Windows.Forms.RadioButton()
        Me._optStatus_0 = New System.Windows.Forms.RadioButton()
        Me.txtCDate = New System.Windows.Forms.TextBox()
        Me.txtValFrequency = New System.Windows.Forms.TextBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.txtIssueDate = New System.Windows.Forms.TextBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me._lblLabels_28 = New System.Windows.Forms.Label()
        Me._lblLabels_26 = New System.Windows.Forms.Label()
        Me._lblLabels_21 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_10 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._lblLabels_13 = New System.Windows.Forms.Label()
        Me._lblLabels_12 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me._lblLabels_9 = New System.Windows.Forms.Label()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me._lblLabels_27 = New System.Windows.Forms.Label()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.fraMaster.SuspendLayout()
        Me.fraSize.SuspendLayout()
        Me.fraRange.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchSuppCustCode
        '
        Me.cmdSearchSuppCustCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSuppCustCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSuppCustCode.Enabled = False
        Me.cmdSearchSuppCustCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSuppCustCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSuppCustCode.Image = CType(resources.GetObject("cmdSearchSuppCustCode.Image"), System.Drawing.Image)
        Me.cmdSearchSuppCustCode.Location = New System.Drawing.Point(238, 218)
        Me.cmdSearchSuppCustCode.Name = "cmdSearchSuppCustCode"
        Me.cmdSearchSuppCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSuppCustCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchSuppCustCode.TabIndex = 15
        Me.cmdSearchSuppCustCode.TabStop = False
        Me.cmdSearchSuppCustCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSuppCustCode, "Search")
        Me.cmdSearchSuppCustCode.UseVisualStyleBackColor = False
        '
        'cmdItemCode
        '
        Me.cmdItemCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdItemCode.Image = CType(resources.GetObject("cmdItemCode.Image"), System.Drawing.Image)
        Me.cmdItemCode.Location = New System.Drawing.Point(238, 42)
        Me.cmdItemCode.Name = "cmdItemCode"
        Me.cmdItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdItemCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdItemCode.TabIndex = 42
        Me.cmdItemCode.TabStop = False
        Me.cmdItemCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdItemCode, "Search")
        Me.cmdItemCode.UseVisualStyleBackColor = False
        '
        'cmdSearchNumber
        '
        Me.cmdSearchNumber.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchNumber.Image = CType(resources.GetObject("cmdSearchNumber.Image"), System.Drawing.Image)
        Me.cmdSearchNumber.Location = New System.Drawing.Point(238, 16)
        Me.cmdSearchNumber.Name = "cmdSearchNumber"
        Me.cmdSearchNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchNumber.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchNumber.TabIndex = 41
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
        Me.CmdClose.Location = New System.Drawing.Point(492, 14)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 40
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
        Me.CmdView.Location = New System.Drawing.Point(432, 14)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 37)
        Me.CmdView.TabIndex = 39
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
        Me.cmdPrint.Location = New System.Drawing.Point(312, 14)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 37
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
        Me.CmdDelete.Location = New System.Drawing.Point(252, 14)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 37)
        Me.CmdDelete.TabIndex = 36
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
        Me.CmdSave.Location = New System.Drawing.Point(132, 14)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
        Me.CmdSave.TabIndex = 34
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
        Me.CmdModify.Location = New System.Drawing.Point(72, 14)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 37)
        Me.CmdModify.TabIndex = 33
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
        Me.CmdAdd.Location = New System.Drawing.Point(12, 14)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 37)
        Me.CmdAdd.TabIndex = 32
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraMaster
        '
        Me.fraMaster.BackColor = System.Drawing.SystemColors.Control
        Me.fraMaster.Controls.Add(Me.txtCalibBy)
        Me.fraMaster.Controls.Add(Me.txtCertNo)
        Me.fraMaster.Controls.Add(Me.txtModel)
        Me.fraMaster.Controls.Add(Me.txtCalibValid)
        Me.fraMaster.Controls.Add(Me._lblLabels_25)
        Me.fraMaster.Controls.Add(Me._lblLabels_24)
        Me.fraMaster.Controls.Add(Me._lblLabels_23)
        Me.fraMaster.Controls.Add(Me._lblLabels_22)
        Me.fraMaster.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMaster.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMaster.Location = New System.Drawing.Point(0, 317)
        Me.fraMaster.Name = "fraMaster"
        Me.fraMaster.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMaster.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMaster.Size = New System.Drawing.Size(563, 57)
        Me.fraMaster.TabIndex = 77
        Me.fraMaster.TabStop = False
        Me.fraMaster.Text = "Mater Instrument"
        '
        'txtCalibBy
        '
        Me.txtCalibBy.AcceptsReturn = True
        Me.txtCalibBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtCalibBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCalibBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalibBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCalibBy.Location = New System.Drawing.Point(383, 12)
        Me.txtCalibBy.MaxLength = 0
        Me.txtCalibBy.Name = "txtCalibBy"
        Me.txtCalibBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCalibBy.Size = New System.Drawing.Size(161, 20)
        Me.txtCalibBy.TabIndex = 29
        '
        'txtCertNo
        '
        Me.txtCertNo.AcceptsReturn = True
        Me.txtCertNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCertNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCertNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCertNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCertNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCertNo.Location = New System.Drawing.Point(120, 34)
        Me.txtCertNo.MaxLength = 0
        Me.txtCertNo.Name = "txtCertNo"
        Me.txtCertNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCertNo.Size = New System.Drawing.Size(161, 20)
        Me.txtCertNo.TabIndex = 30
        '
        'txtModel
        '
        Me.txtModel.AcceptsReturn = True
        Me.txtModel.BackColor = System.Drawing.SystemColors.Window
        Me.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModel.Location = New System.Drawing.Point(120, 12)
        Me.txtModel.MaxLength = 0
        Me.txtModel.Name = "txtModel"
        Me.txtModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModel.Size = New System.Drawing.Size(161, 20)
        Me.txtModel.TabIndex = 28
        '
        'txtCalibValid
        '
        Me.txtCalibValid.AcceptsReturn = True
        Me.txtCalibValid.BackColor = System.Drawing.SystemColors.Window
        Me.txtCalibValid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibValid.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCalibValid.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalibValid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCalibValid.Location = New System.Drawing.Point(383, 34)
        Me.txtCalibValid.MaxLength = 0
        Me.txtCalibValid.Name = "txtCalibValid"
        Me.txtCalibValid.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCalibValid.Size = New System.Drawing.Size(161, 20)
        Me.txtCalibValid.TabIndex = 31
        '
        '_lblLabels_25
        '
        Me._lblLabels_25.AutoSize = True
        Me._lblLabels_25.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_25.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_25, CType(25, Short))
        Me._lblLabels_25.Location = New System.Drawing.Point(319, 14)
        Me._lblLabels_25.Name = "_lblLabels_25"
        Me._lblLabels_25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_25.Size = New System.Drawing.Size(55, 13)
        Me._lblLabels_25.TabIndex = 81
        Me._lblLabels_25.Text = "Calib By :"
        Me._lblLabels_25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_24
        '
        Me._lblLabels_24.AutoSize = True
        Me._lblLabels_24.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_24.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_24, CType(24, Short))
        Me._lblLabels_24.Location = New System.Drawing.Point(19, 37)
        Me._lblLabels_24.Name = "_lblLabels_24"
        Me._lblLabels_24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_24.Size = New System.Drawing.Size(86, 13)
        Me._lblLabels_24.TabIndex = 80
        Me._lblLabels_24.Text = "Certificate No. :"
        Me._lblLabels_24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_23
        '
        Me._lblLabels_23.AutoSize = True
        Me._lblLabels_23.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_23.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_23, CType(23, Short))
        Me._lblLabels_23.Location = New System.Drawing.Point(67, 14)
        Me._lblLabels_23.Name = "_lblLabels_23"
        Me._lblLabels_23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_23.Size = New System.Drawing.Size(46, 13)
        Me._lblLabels_23.TabIndex = 79
        Me._lblLabels_23.Text = "Model :"
        Me._lblLabels_23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_22
        '
        Me._lblLabels_22.AutoSize = True
        Me._lblLabels_22.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_22.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_22, CType(22, Short))
        Me._lblLabels_22.Location = New System.Drawing.Point(294, 37)
        Me._lblLabels_22.Name = "_lblLabels_22"
        Me._lblLabels_22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_22.Size = New System.Drawing.Size(77, 13)
        Me._lblLabels_22.TabIndex = 78
        Me._lblLabels_22.Text = "Calib Vaidity :"
        Me._lblLabels_22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraSize
        '
        Me.fraSize.BackColor = System.Drawing.SystemColors.Control
        Me.fraSize.Controls.Add(Me.txtWearSize)
        Me.fraSize.Controls.Add(Me.txtGoSize)
        Me.fraSize.Controls.Add(Me.txtBasicSize)
        Me.fraSize.Controls.Add(Me.txtNoGoSize)
        Me.fraSize.Controls.Add(Me._lblLabels_20)
        Me.fraSize.Controls.Add(Me._lblLabels_19)
        Me.fraSize.Controls.Add(Me._lblLabels_18)
        Me.fraSize.Controls.Add(Me._lblLabels_17)
        Me.fraSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSize.Location = New System.Drawing.Point(0, 258)
        Me.fraSize.Name = "fraSize"
        Me.fraSize.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSize.Size = New System.Drawing.Size(563, 59)
        Me.fraSize.TabIndex = 66
        Me.fraSize.TabStop = False
        Me.fraSize.Text = "Required Sizes"
        '
        'txtWearSize
        '
        Me.txtWearSize.AcceptsReturn = True
        Me.txtWearSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtWearSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWearSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWearSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWearSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWearSize.Location = New System.Drawing.Point(383, 34)
        Me.txtWearSize.MaxLength = 0
        Me.txtWearSize.Name = "txtWearSize"
        Me.txtWearSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWearSize.Size = New System.Drawing.Size(161, 20)
        Me.txtWearSize.TabIndex = 24
        '
        'txtGoSize
        '
        Me.txtGoSize.AcceptsReturn = True
        Me.txtGoSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtGoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGoSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGoSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGoSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGoSize.Location = New System.Drawing.Point(120, 12)
        Me.txtGoSize.MaxLength = 0
        Me.txtGoSize.Name = "txtGoSize"
        Me.txtGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGoSize.Size = New System.Drawing.Size(161, 20)
        Me.txtGoSize.TabIndex = 21
        '
        'txtBasicSize
        '
        Me.txtBasicSize.AcceptsReturn = True
        Me.txtBasicSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtBasicSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBasicSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBasicSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBasicSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBasicSize.Location = New System.Drawing.Point(120, 34)
        Me.txtBasicSize.MaxLength = 0
        Me.txtBasicSize.Name = "txtBasicSize"
        Me.txtBasicSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBasicSize.Size = New System.Drawing.Size(161, 20)
        Me.txtBasicSize.TabIndex = 23
        '
        'txtNoGoSize
        '
        Me.txtNoGoSize.AcceptsReturn = True
        Me.txtNoGoSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtNoGoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoGoSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNoGoSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoGoSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNoGoSize.Location = New System.Drawing.Point(383, 12)
        Me.txtNoGoSize.MaxLength = 0
        Me.txtNoGoSize.Name = "txtNoGoSize"
        Me.txtNoGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNoGoSize.Size = New System.Drawing.Size(161, 20)
        Me.txtNoGoSize.TabIndex = 22
        '
        '_lblLabels_20
        '
        Me._lblLabels_20.AutoSize = True
        Me._lblLabels_20.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_20.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_20, CType(20, Short))
        Me._lblLabels_20.Location = New System.Drawing.Point(306, 37)
        Me._lblLabels_20.Name = "_lblLabels_20"
        Me._lblLabels_20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_20.Size = New System.Drawing.Size(63, 13)
        Me._lblLabels_20.TabIndex = 70
        Me._lblLabels_20.Text = "Wear Size :"
        Me._lblLabels_20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_19
        '
        Me._lblLabels_19.AutoSize = True
        Me._lblLabels_19.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_19.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_19, CType(19, Short))
        Me._lblLabels_19.Location = New System.Drawing.Point(57, 14)
        Me._lblLabels_19.Name = "_lblLabels_19"
        Me._lblLabels_19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_19.Size = New System.Drawing.Size(51, 13)
        Me._lblLabels_19.TabIndex = 69
        Me._lblLabels_19.Text = "Go Size :"
        Me._lblLabels_19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_18
        '
        Me._lblLabels_18.AutoSize = True
        Me._lblLabels_18.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_18.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_18, CType(18, Short))
        Me._lblLabels_18.Location = New System.Drawing.Point(42, 37)
        Me._lblLabels_18.Name = "_lblLabels_18"
        Me._lblLabels_18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_18.Size = New System.Drawing.Size(62, 13)
        Me._lblLabels_18.TabIndex = 68
        Me._lblLabels_18.Text = "Basic Size :"
        Me._lblLabels_18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_17
        '
        Me._lblLabels_17.AutoSize = True
        Me._lblLabels_17.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_17.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_17, CType(17, Short))
        Me._lblLabels_17.Location = New System.Drawing.Point(305, 14)
        Me._lblLabels_17.Name = "_lblLabels_17"
        Me._lblLabels_17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_17.Size = New System.Drawing.Size(66, 13)
        Me._lblLabels_17.TabIndex = 67
        Me._lblLabels_17.Text = "NoGo Size :"
        Me._lblLabels_17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraRange
        '
        Me.fraRange.BackColor = System.Drawing.SystemColors.Control
        Me.fraRange.Controls.Add(Me.txtUnitRange)
        Me.fraRange.Controls.Add(Me.txtMaxRange)
        Me.fraRange.Controls.Add(Me.txtMinRange)
        Me.fraRange.Controls.Add(Me._lblLabels_16)
        Me.fraRange.Controls.Add(Me._lblLabels_15)
        Me.fraRange.Controls.Add(Me._lblLabels_14)
        Me.fraRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraRange.Location = New System.Drawing.Point(0, 258)
        Me.fraRange.Name = "fraRange"
        Me.fraRange.Padding = New System.Windows.Forms.Padding(0)
        Me.fraRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraRange.Size = New System.Drawing.Size(563, 59)
        Me.fraRange.TabIndex = 62
        Me.fraRange.TabStop = False
        Me.fraRange.Text = "Range"
        '
        'txtUnitRange
        '
        Me.txtUnitRange.AcceptsReturn = True
        Me.txtUnitRange.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnitRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnitRange.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnitRange.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnitRange.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnitRange.Location = New System.Drawing.Point(432, 24)
        Me.txtUnitRange.MaxLength = 0
        Me.txtUnitRange.Name = "txtUnitRange"
        Me.txtUnitRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnitRange.Size = New System.Drawing.Size(123, 20)
        Me.txtUnitRange.TabIndex = 27
        '
        'txtMaxRange
        '
        Me.txtMaxRange.AcceptsReturn = True
        Me.txtMaxRange.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxRange.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaxRange.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaxRange.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMaxRange.Location = New System.Drawing.Point(256, 24)
        Me.txtMaxRange.MaxLength = 0
        Me.txtMaxRange.Name = "txtMaxRange"
        Me.txtMaxRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaxRange.Size = New System.Drawing.Size(91, 20)
        Me.txtMaxRange.TabIndex = 26
        '
        'txtMinRange
        '
        Me.txtMinRange.AcceptsReturn = True
        Me.txtMinRange.BackColor = System.Drawing.SystemColors.Window
        Me.txtMinRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMinRange.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMinRange.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMinRange.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMinRange.Location = New System.Drawing.Point(80, 24)
        Me.txtMinRange.MaxLength = 0
        Me.txtMinRange.Name = "txtMinRange"
        Me.txtMinRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMinRange.Size = New System.Drawing.Size(91, 20)
        Me.txtMinRange.TabIndex = 25
        '
        '_lblLabels_16
        '
        Me._lblLabels_16.AutoSize = True
        Me._lblLabels_16.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_16.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_16, CType(16, Short))
        Me._lblLabels_16.Location = New System.Drawing.Point(357, 26)
        Me._lblLabels_16.Name = "_lblLabels_16"
        Me._lblLabels_16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_16.Size = New System.Drawing.Size(69, 13)
        Me._lblLabels_16.TabIndex = 65
        Me._lblLabels_16.Text = "Range Unit :"
        Me._lblLabels_16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_15
        '
        Me._lblLabels_15.AutoSize = True
        Me._lblLabels_15.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_15.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_15, CType(15, Short))
        Me._lblLabels_15.Location = New System.Drawing.Point(181, 26)
        Me._lblLabels_15.Name = "_lblLabels_15"
        Me._lblLabels_15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_15.Size = New System.Drawing.Size(70, 13)
        Me._lblLabels_15.TabIndex = 64
        Me._lblLabels_15.Text = "Max Range :"
        Me._lblLabels_15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_14
        '
        Me._lblLabels_14.AutoSize = True
        Me._lblLabels_14.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_14.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_14, CType(14, Short))
        Me._lblLabels_14.Location = New System.Drawing.Point(8, 26)
        Me._lblLabels_14.Name = "_lblLabels_14"
        Me._lblLabels_14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_14.Size = New System.Drawing.Size(67, 13)
        Me._lblLabels_14.TabIndex = 63
        Me._lblLabels_14.Text = "Min Range :"
        Me._lblLabels_14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtSuppCustCode)
        Me.Frame4.Controls.Add(Me.cmdSearchSuppCustCode)
        Me.Frame4.Controls.Add(Me.txtSuppCustName)
        Me.Frame4.Controls.Add(Me.chkMasterInst)
        Me.Frame4.Controls.Add(Me.txtLC)
        Me.Frame4.Controls.Add(Me.txtENO)
        Me.Frame4.Controls.Add(Me.txtMarkersNo)
        Me.Frame4.Controls.Add(Me.txtMake)
        Me.Frame4.Controls.Add(Me.txtRange)
        Me.Frame4.Controls.Add(Me.cboType)
        Me.Frame4.Controls.Add(Me.txtCalibOK)
        Me.Frame4.Controls.Add(Me.txtLCDate)
        Me.Frame4.Controls.Add(Me.cboCaliFacil)
        Me.Frame4.Controls.Add(Me.txtItemName)
        Me.Frame4.Controls.Add(Me.txtIssueTo)
        Me.Frame4.Controls.Add(Me.cmdItemCode)
        Me.Frame4.Controls.Add(Me.Frame1)
        Me.Frame4.Controls.Add(Me.txtCDate)
        Me.Frame4.Controls.Add(Me.txtValFrequency)
        Me.Frame4.Controls.Add(Me.txtLocation)
        Me.Frame4.Controls.Add(Me.txtDescription)
        Me.Frame4.Controls.Add(Me.txtIssueDate)
        Me.Frame4.Controls.Add(Me.txtNumber)
        Me.Frame4.Controls.Add(Me.txtItemCode)
        Me.Frame4.Controls.Add(Me.cmdSearchNumber)
        Me.Frame4.Controls.Add(Me._lblLabels_28)
        Me.Frame4.Controls.Add(Me._lblLabels_26)
        Me.Frame4.Controls.Add(Me._lblLabels_21)
        Me.Frame4.Controls.Add(Me._lblLabels_5)
        Me.Frame4.Controls.Add(Me._lblLabels_10)
        Me.Frame4.Controls.Add(Me._lblLabels_7)
        Me.Frame4.Controls.Add(Me._lblLabels_11)
        Me.Frame4.Controls.Add(Me._lblLabels_3)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Controls.Add(Me._lblLabels_13)
        Me.Frame4.Controls.Add(Me._lblLabels_12)
        Me.Frame4.Controls.Add(Me.Label27)
        Me.Frame4.Controls.Add(Me._lblLabels_9)
        Me.Frame4.Controls.Add(Me._lblLabels_8)
        Me.Frame4.Controls.Add(Me._lblLabels_6)
        Me.Frame4.Controls.Add(Me._lblLabels_4)
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
        Me.Frame4.Size = New System.Drawing.Size(563, 264)
        Me.Frame4.TabIndex = 46
        Me.Frame4.TabStop = False
        '
        'txtSuppCustCode
        '
        Me.txtSuppCustCode.AcceptsReturn = True
        Me.txtSuppCustCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppCustCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppCustCode.Enabled = False
        Me.txtSuppCustCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppCustCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuppCustCode.Location = New System.Drawing.Point(122, 218)
        Me.txtSuppCustCode.MaxLength = 0
        Me.txtSuppCustCode.Name = "txtSuppCustCode"
        Me.txtSuppCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppCustCode.Size = New System.Drawing.Size(115, 20)
        Me.txtSuppCustCode.TabIndex = 14
        '
        'txtSuppCustName
        '
        Me.txtSuppCustName.AcceptsReturn = True
        Me.txtSuppCustName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppCustName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppCustName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppCustName.Enabled = False
        Me.txtSuppCustName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppCustName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuppCustName.Location = New System.Drawing.Point(266, 218)
        Me.txtSuppCustName.MaxLength = 0
        Me.txtSuppCustName.Name = "txtSuppCustName"
        Me.txtSuppCustName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppCustName.Size = New System.Drawing.Size(275, 20)
        Me.txtSuppCustName.TabIndex = 16
        '
        'chkMasterInst
        '
        Me.chkMasterInst.BackColor = System.Drawing.SystemColors.Control
        Me.chkMasterInst.Checked = True
        Me.chkMasterInst.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMasterInst.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkMasterInst.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkMasterInst.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkMasterInst.Location = New System.Drawing.Point(122, 240)
        Me.chkMasterInst.Name = "chkMasterInst"
        Me.chkMasterInst.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkMasterInst.Size = New System.Drawing.Size(17, 17)
        Me.chkMasterInst.TabIndex = 17
        Me.chkMasterInst.UseVisualStyleBackColor = False
        '
        'txtLC
        '
        Me.txtLC.AcceptsReturn = True
        Me.txtLC.BackColor = System.Drawing.SystemColors.Window
        Me.txtLC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLC.Location = New System.Drawing.Point(122, 108)
        Me.txtLC.MaxLength = 0
        Me.txtLC.Name = "txtLC"
        Me.txtLC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLC.Size = New System.Drawing.Size(161, 20)
        Me.txtLC.TabIndex = 5
        '
        'txtENO
        '
        Me.txtENO.AcceptsReturn = True
        Me.txtENO.BackColor = System.Drawing.SystemColors.Window
        Me.txtENO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtENO.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtENO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtENO.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtENO.Location = New System.Drawing.Point(380, 86)
        Me.txtENO.MaxLength = 0
        Me.txtENO.Name = "txtENO"
        Me.txtENO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtENO.Size = New System.Drawing.Size(161, 20)
        Me.txtENO.TabIndex = 4
        '
        'txtMarkersNo
        '
        Me.txtMarkersNo.AcceptsReturn = True
        Me.txtMarkersNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMarkersNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMarkersNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMarkersNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMarkersNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMarkersNo.Location = New System.Drawing.Point(380, 108)
        Me.txtMarkersNo.MaxLength = 0
        Me.txtMarkersNo.Name = "txtMarkersNo"
        Me.txtMarkersNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMarkersNo.Size = New System.Drawing.Size(161, 20)
        Me.txtMarkersNo.TabIndex = 6
        '
        'txtMake
        '
        Me.txtMake.AcceptsReturn = True
        Me.txtMake.BackColor = System.Drawing.SystemColors.Window
        Me.txtMake.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMake.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMake.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMake.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMake.Location = New System.Drawing.Point(380, 130)
        Me.txtMake.MaxLength = 0
        Me.txtMake.Name = "txtMake"
        Me.txtMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMake.Size = New System.Drawing.Size(161, 20)
        Me.txtMake.TabIndex = 8
        '
        'txtRange
        '
        Me.txtRange.AcceptsReturn = True
        Me.txtRange.BackColor = System.Drawing.SystemColors.Window
        Me.txtRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRange.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRange.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRange.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRange.Location = New System.Drawing.Point(122, 130)
        Me.txtRange.MaxLength = 0
        Me.txtRange.Name = "txtRange"
        Me.txtRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRange.Size = New System.Drawing.Size(161, 20)
        Me.txtRange.TabIndex = 7
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboType.Location = New System.Drawing.Point(122, 85)
        Me.cboType.Name = "cboType"
        Me.cboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboType.Size = New System.Drawing.Size(161, 22)
        Me.cboType.TabIndex = 3
        '
        'txtCalibOK
        '
        Me.txtCalibOK.AcceptsReturn = True
        Me.txtCalibOK.BackColor = System.Drawing.SystemColors.Window
        Me.txtCalibOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibOK.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCalibOK.Enabled = False
        Me.txtCalibOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalibOK.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCalibOK.Location = New System.Drawing.Point(380, 174)
        Me.txtCalibOK.MaxLength = 0
        Me.txtCalibOK.Name = "txtCalibOK"
        Me.txtCalibOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCalibOK.Size = New System.Drawing.Size(161, 20)
        Me.txtCalibOK.TabIndex = 11
        '
        'txtLCDate
        '
        Me.txtLCDate.AcceptsReturn = True
        Me.txtLCDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtLCDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLCDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLCDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLCDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLCDate.Location = New System.Drawing.Point(122, 174)
        Me.txtLCDate.MaxLength = 0
        Me.txtLCDate.Name = "txtLCDate"
        Me.txtLCDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLCDate.Size = New System.Drawing.Size(91, 20)
        Me.txtLCDate.TabIndex = 10
        '
        'cboCaliFacil
        '
        Me.cboCaliFacil.BackColor = System.Drawing.SystemColors.Window
        Me.cboCaliFacil.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCaliFacil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCaliFacil.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCaliFacil.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCaliFacil.Location = New System.Drawing.Point(380, 239)
        Me.cboCaliFacil.Name = "cboCaliFacil"
        Me.cboCaliFacil.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCaliFacil.Size = New System.Drawing.Size(161, 22)
        Me.cboCaliFacil.TabIndex = 18
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Enabled = False
        Me.txtItemName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemName.Location = New System.Drawing.Point(266, 42)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(275, 20)
        Me.txtItemName.TabIndex = 43
        '
        'txtIssueTo
        '
        Me.txtIssueTo.AcceptsReturn = True
        Me.txtIssueTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtIssueTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssueTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIssueTo.Enabled = False
        Me.txtIssueTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssueTo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIssueTo.Location = New System.Drawing.Point(212, 254)
        Me.txtIssueTo.MaxLength = 0
        Me.txtIssueTo.Name = "txtIssueTo"
        Me.txtIssueTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIssueTo.Size = New System.Drawing.Size(91, 20)
        Me.txtIssueTo.TabIndex = 20
        Me.txtIssueTo.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optStatus_1)
        Me.Frame1.Controls.Add(Me._optStatus_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(310, 8)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(229, 31)
        Me.Frame1.TabIndex = 56
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
        Me._optStatus_1.Location = New System.Drawing.Point(118, 12)
        Me._optStatus_1.Name = "_optStatus_1"
        Me._optStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_1.Size = New System.Drawing.Size(64, 17)
        Me._optStatus_1.TabIndex = 58
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
        Me._optStatus_0.Location = New System.Drawing.Point(8, 12)
        Me._optStatus_0.Name = "_optStatus_0"
        Me._optStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_0.Size = New System.Drawing.Size(56, 17)
        Me._optStatus_0.TabIndex = 57
        Me._optStatus_0.TabStop = True
        Me._optStatus_0.Text = "Active"
        Me._optStatus_0.UseVisualStyleBackColor = False
        '
        'txtCDate
        '
        Me.txtCDate.AcceptsReturn = True
        Me.txtCDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCDate.Enabled = False
        Me.txtCDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCDate.Location = New System.Drawing.Point(380, 196)
        Me.txtCDate.MaxLength = 0
        Me.txtCDate.Name = "txtCDate"
        Me.txtCDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCDate.Size = New System.Drawing.Size(91, 20)
        Me.txtCDate.TabIndex = 13
        '
        'txtValFrequency
        '
        Me.txtValFrequency.AcceptsReturn = True
        Me.txtValFrequency.BackColor = System.Drawing.SystemColors.Window
        Me.txtValFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtValFrequency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtValFrequency.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValFrequency.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtValFrequency.Location = New System.Drawing.Point(122, 196)
        Me.txtValFrequency.MaxLength = 0
        Me.txtValFrequency.Name = "txtValFrequency"
        Me.txtValFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtValFrequency.Size = New System.Drawing.Size(91, 20)
        Me.txtValFrequency.TabIndex = 12
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLocation.Location = New System.Drawing.Point(122, 152)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(419, 20)
        Me.txtLocation.TabIndex = 9
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = True
        Me.txtDescription.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescription.Location = New System.Drawing.Point(122, 64)
        Me.txtDescription.MaxLength = 0
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescription.Size = New System.Drawing.Size(419, 20)
        Me.txtDescription.TabIndex = 2
        '
        'txtIssueDate
        '
        Me.txtIssueDate.AcceptsReturn = True
        Me.txtIssueDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtIssueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIssueDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIssueDate.Enabled = False
        Me.txtIssueDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIssueDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIssueDate.Location = New System.Drawing.Point(156, 248)
        Me.txtIssueDate.MaxLength = 0
        Me.txtIssueDate.Name = "txtIssueDate"
        Me.txtIssueDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIssueDate.Size = New System.Drawing.Size(63, 20)
        Me.txtIssueDate.TabIndex = 19
        Me.txtIssueDate.Visible = False
        '
        'txtNumber
        '
        Me.txtNumber.AcceptsReturn = True
        Me.txtNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumber.Location = New System.Drawing.Point(122, 16)
        Me.txtNumber.MaxLength = 0
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumber.Size = New System.Drawing.Size(115, 20)
        Me.txtNumber.TabIndex = 0
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemCode.Location = New System.Drawing.Point(122, 42)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(115, 20)
        Me.txtItemCode.TabIndex = 1
        '
        '_lblLabels_28
        '
        Me._lblLabels_28.AutoSize = True
        Me._lblLabels_28.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_28.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_28, CType(28, Short))
        Me._lblLabels_28.Location = New System.Drawing.Point(51, 221)
        Me._lblLabels_28.Name = "_lblLabels_28"
        Me._lblLabels_28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_28.Size = New System.Drawing.Size(62, 13)
        Me._lblLabels_28.TabIndex = 84
        Me._lblLabels_28.Text = "Customer :"
        Me._lblLabels_28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_26
        '
        Me._lblLabels_26.AutoSize = True
        Me._lblLabels_26.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_26.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_26, CType(26, Short))
        Me._lblLabels_26.Location = New System.Drawing.Point(36, 242)
        Me._lblLabels_26.Name = "_lblLabels_26"
        Me._lblLabels_26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_26.Size = New System.Drawing.Size(72, 13)
        Me._lblLabels_26.TabIndex = 82
        Me._lblLabels_26.Text = "Master Inst. :"
        Me._lblLabels_26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_21
        '
        Me._lblLabels_21.AutoSize = True
        Me._lblLabels_21.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_21, CType(21, Short))
        Me._lblLabels_21.Location = New System.Drawing.Point(330, 177)
        Me._lblLabels_21.Name = "_lblLabels_21"
        Me._lblLabels_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_21.Size = New System.Drawing.Size(44, 13)
        Me._lblLabels_21.TabIndex = 76
        Me._lblLabels_21.Text = "Status :"
        Me._lblLabels_21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.AutoSize = True
        Me._lblLabels_5.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_5, CType(5, Short))
        Me._lblLabels_5.Location = New System.Drawing.Point(334, 89)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(40, 13)
        Me._lblLabels_5.TabIndex = 75
        Me._lblLabels_5.Text = "E. No :"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_10
        '
        Me._lblLabels_10.AutoSize = True
        Me._lblLabels_10.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_10, CType(10, Short))
        Me._lblLabels_10.Location = New System.Drawing.Point(318, 199)
        Me._lblLabels_10.Name = "_lblLabels_10"
        Me._lblLabels_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_10.Size = New System.Drawing.Size(55, 13)
        Me._lblLabels_10.TabIndex = 74
        Me._lblLabels_10.Text = "CD Date :"
        Me._lblLabels_10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.AutoSize = True
        Me._lblLabels_7.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_7, CType(7, Short))
        Me._lblLabels_7.Location = New System.Drawing.Point(305, 111)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(68, 13)
        Me._lblLabels_7.TabIndex = 73
        Me._lblLabels_7.Text = "Makers No :"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.AutoSize = True
        Me._lblLabels_11.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_11, CType(11, Short))
        Me._lblLabels_11.Location = New System.Drawing.Point(335, 133)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(41, 13)
        Me._lblLabels_11.TabIndex = 72
        Me._lblLabels_11.Text = "Make :"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = True
        Me._lblLabels_3.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_3, CType(3, Short))
        Me._lblLabels_3.Location = New System.Drawing.Point(49, 177)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(58, 13)
        Me._lblLabels_3.TabIndex = 71
        Me._lblLabels_3.Text = "L.C. Date :"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(75, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 61
        Me.Label1.Text = "Type :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_13
        '
        Me._lblLabels_13.AutoSize = True
        Me._lblLabels_13.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_13.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_13.Enabled = False
        Me._lblLabels_13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_13, CType(13, Short))
        Me._lblLabels_13.Location = New System.Drawing.Point(163, 239)
        Me._lblLabels_13.Name = "_lblLabels_13"
        Me._lblLabels_13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_13.Size = New System.Drawing.Size(60, 13)
        Me._lblLabels_13.TabIndex = 60
        Me._lblLabels_13.Text = "Issued To :"
        Me._lblLabels_13.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._lblLabels_13.Visible = False
        '
        '_lblLabels_12
        '
        Me._lblLabels_12.AutoSize = True
        Me._lblLabels_12.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_12, CType(12, Short))
        Me._lblLabels_12.Location = New System.Drawing.Point(66, 133)
        Me._lblLabels_12.Name = "_lblLabels_12"
        Me._lblLabels_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_12.Size = New System.Drawing.Size(45, 13)
        Me._lblLabels_12.TabIndex = 59
        Me._lblLabels_12.Text = "Range :"
        Me._lblLabels_12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(306, 243)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(64, 13)
        Me.Label27.TabIndex = 55
        Me.Label27.Text = "Cali. Facil. :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_9
        '
        Me._lblLabels_9.AutoSize = True
        Me._lblLabels_9.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_9, CType(9, Short))
        Me._lblLabels_9.Location = New System.Drawing.Point(16, 199)
        Me._lblLabels_9.Name = "_lblLabels_9"
        Me._lblLabels_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_9.Size = New System.Drawing.Size(86, 13)
        Me._lblLabels_9.TabIndex = 54
        Me._lblLabels_9.Text = "Val. Frequency :"
        Me._lblLabels_9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.AutoSize = True
        Me._lblLabels_8.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_8, CType(8, Short))
        Me._lblLabels_8.Location = New System.Drawing.Point(54, 155)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(56, 13)
        Me._lblLabels_8.TabIndex = 53
        Me._lblLabels_8.Text = "Location :"
        Me._lblLabels_8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.AutoSize = True
        Me._lblLabels_6.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_6, CType(6, Short))
        Me._lblLabels_6.Location = New System.Drawing.Point(39, 67)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(71, 13)
        Me._lblLabels_6.TabIndex = 52
        Me._lblLabels_6.Text = "Description :"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.AutoSize = True
        Me._lblLabels_4.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_4, CType(4, Short))
        Me._lblLabels_4.Location = New System.Drawing.Point(76, 111)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(34, 13)
        Me._lblLabels_4.TabIndex = 51
        Me._lblLabels_4.Text = "L. C. :"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Enabled = False
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(150, 237)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(65, 13)
        Me._lblLabels_1.TabIndex = 50
        Me._lblLabels_1.Text = "Issue Date :"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._lblLabels_1.Visible = False
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(272, 18)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 49
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
        Me._lblLabels_0.Location = New System.Drawing.Point(6, 18)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(54, 13)
        Me._lblLabels_0.TabIndex = 48
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
        Me._lblLabels_2.Location = New System.Drawing.Point(46, 45)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(66, 13)
        Me._lblLabels_2.TabIndex = 47
        Me._lblLabels_2.Text = "Item Code :"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 58)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 79
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdMain)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 370)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(563, 111)
        Me.Frame2.TabIndex = 85
        Me.Frame2.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(557, 99)
        Me.SprdMain.TabIndex = 86
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(563, 481)
        Me.SprdView.TabIndex = 45
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 476)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(563, 55)
        Me.FraMovement.TabIndex = 44
        Me.FraMovement.TabStop = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(372, 14)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.cmdPreview.TabIndex = 38
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(192, 14)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdSavePrint.TabIndex = 35
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        '_lblLabels_27
        '
        Me._lblLabels_27.AutoSize = True
        Me._lblLabels_27.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_27.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_27, CType(27, Short))
        Me._lblLabels_27.Location = New System.Drawing.Point(0, 3)
        Me._lblLabels_27.Name = "_lblLabels_27"
        Me._lblLabels_27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_27.Size = New System.Drawing.Size(60, 13)
        Me._lblLabels_27.TabIndex = 83
        Me._lblLabels_27.Text = "Issued To :"
        Me._lblLabels_27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'optStatus
        '
        '
        'frmIMTEMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(565, 532)
        Me.Controls.Add(Me.fraMaster)
        Me.Controls.Add(Me.fraSize)
        Me.Controls.Add(Me.fraRange)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me._lblLabels_27)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIMTEMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IMTE Master"
        Me.fraMaster.ResumeLayout(False)
        Me.fraMaster.PerformLayout()
        Me.fraSize.ResumeLayout(False)
        Me.fraSize.PerformLayout()
        Me.fraRange.ResumeLayout(False)
        Me.fraRange.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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