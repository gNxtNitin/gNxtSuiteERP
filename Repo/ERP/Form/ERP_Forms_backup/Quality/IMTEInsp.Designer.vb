Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmIMTEInsp
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
    Public WithEvents txtActualNoGoSize As System.Windows.Forms.TextBox
    Public WithEvents txtActualGoSize As System.Windows.Forms.TextBox
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents fraActualSize As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraCalibResult As System.Windows.Forms.GroupBox
    Public WithEvents SprdInst As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtCretificateIssueDate As System.Windows.Forms.TextBox
    Public WithEvents txtCretificateNo As System.Windows.Forms.TextBox
    Public WithEvents chkNABLLogo As System.Windows.Forms.CheckBox
    Public WithEvents txtReceiptDate As System.Windows.Forms.TextBox
    Public WithEvents txtAppName As System.Windows.Forms.TextBox
    Public WithEvents txtInspName As System.Windows.Forms.TextBox
    Public WithEvents lblNoGoSize As System.Windows.Forms.Label
    Public WithEvents lblGoSize As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents lblWearSize As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents lblBasicSize As System.Windows.Forms.Label
    Public WithEvents fraGoNoGo As System.Windows.Forms.Panel
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblMinRange As System.Windows.Forms.Label
    Public WithEvents lblMaxRange As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents lblUnitRange As System.Windows.Forms.Label
    Public WithEvents lblRange As System.Windows.Forms.Label
    Public WithEvents fraRange As System.Windows.Forms.Panel
    Public WithEvents txtCalibProc As System.Windows.Forms.TextBox
    Public WithEvents txtVisualInsp As System.Windows.Forms.TextBox
    Public WithEvents txtUncertainty As System.Windows.Forms.TextBox
    Public WithEvents txtZeroError As System.Windows.Forms.TextBox
    Public WithEvents chkCalibOK As System.Windows.Forms.CheckBox
    Public WithEvents txtSoakingTime As System.Windows.Forms.TextBox
    Public WithEvents txtHumidity As System.Windows.Forms.TextBox
    Public WithEvents txtAmbTemp As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchAppBy As System.Windows.Forms.Button
    Public WithEvents txtAppBy As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDocNo As System.Windows.Forms.Button
    Public WithEvents txtDocNo As System.Windows.Forms.TextBox
    Public WithEvents txtInspBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchInspBy As System.Windows.Forms.Button
    Public WithEvents cmdSearchSlipNo As System.Windows.Forms.Button
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents lblMenu As System.Windows.Forms.Label
    Public WithEvents lblCalibOK As System.Windows.Forms.Label
    Public WithEvents lblType As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblCaliFacil As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_21 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblMake As System.Windows.Forms.Label
    Public WithEvents lblMakersNo As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblFrequency As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents lblLocation As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblENo As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents lblLC As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblDescription As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
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
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmIMTEInsp))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchAppBy = New System.Windows.Forms.Button()
        Me.cmdSearchDocNo = New System.Windows.Forms.Button()
        Me.cmdSearchInspBy = New System.Windows.Forms.Button()
        Me.cmdSearchSlipNo = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraActualSize = New System.Windows.Forms.GroupBox()
        Me.txtActualNoGoSize = New System.Windows.Forms.TextBox()
        Me.txtActualGoSize = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.fraCalibResult = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.SprdInst = New AxFPSpreadADO.AxfpSpread()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtCretificateIssueDate = New System.Windows.Forms.TextBox()
        Me.txtCretificateNo = New System.Windows.Forms.TextBox()
        Me.chkNABLLogo = New System.Windows.Forms.CheckBox()
        Me.txtReceiptDate = New System.Windows.Forms.TextBox()
        Me.txtAppName = New System.Windows.Forms.TextBox()
        Me.txtInspName = New System.Windows.Forms.TextBox()
        Me.fraGoNoGo = New System.Windows.Forms.Panel()
        Me.lblNoGoSize = New System.Windows.Forms.Label()
        Me.lblGoSize = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblWearSize = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblBasicSize = New System.Windows.Forms.Label()
        Me.fraRange = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblMinRange = New System.Windows.Forms.Label()
        Me.lblMaxRange = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblUnitRange = New System.Windows.Forms.Label()
        Me.lblRange = New System.Windows.Forms.Label()
        Me.txtCalibProc = New System.Windows.Forms.TextBox()
        Me.txtVisualInsp = New System.Windows.Forms.TextBox()
        Me.txtUncertainty = New System.Windows.Forms.TextBox()
        Me.txtZeroError = New System.Windows.Forms.TextBox()
        Me.chkCalibOK = New System.Windows.Forms.CheckBox()
        Me.txtSoakingTime = New System.Windows.Forms.TextBox()
        Me.txtHumidity = New System.Windows.Forms.TextBox()
        Me.txtAmbTemp = New System.Windows.Forms.TextBox()
        Me.txtAppBy = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.txtInspBy = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.lblMenu = New System.Windows.Forms.Label()
        Me.lblCalibOK = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblCaliFacil = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me._lblLabels_21 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblMake = New System.Windows.Forms.Label()
        Me.lblMakersNo = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblFrequency = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblENo = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblLC = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.fraActualSize.SuspendLayout()
        Me.fraCalibResult.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdInst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTop1.SuspendLayout()
        Me.fraGoNoGo.SuspendLayout()
        Me.fraRange.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchAppBy
        '
        Me.cmdSearchAppBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAppBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAppBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAppBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAppBy.Image = CType(resources.GetObject("cmdSearchAppBy.Image"), System.Drawing.Image)
        Me.cmdSearchAppBy.Location = New System.Drawing.Point(180, 216)
        Me.cmdSearchAppBy.Name = "cmdSearchAppBy"
        Me.cmdSearchAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAppBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchAppBy.TabIndex = 17
        Me.cmdSearchAppBy.TabStop = False
        Me.cmdSearchAppBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAppBy, "Search")
        Me.cmdSearchAppBy.UseVisualStyleBackColor = False
        '
        'cmdSearchDocNo
        '
        Me.cmdSearchDocNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDocNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDocNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDocNo.Image = CType(resources.GetObject("cmdSearchDocNo.Image"), System.Drawing.Image)
        Me.cmdSearchDocNo.Location = New System.Drawing.Point(180, 34)
        Me.cmdSearchDocNo.Name = "cmdSearchDocNo"
        Me.cmdSearchDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDocNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchDocNo.TabIndex = 45
        Me.cmdSearchDocNo.TabStop = False
        Me.cmdSearchDocNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDocNo, "Search")
        Me.cmdSearchDocNo.UseVisualStyleBackColor = False
        '
        'cmdSearchInspBy
        '
        Me.cmdSearchInspBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchInspBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchInspBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchInspBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchInspBy.Image = CType(resources.GetObject("cmdSearchInspBy.Image"), System.Drawing.Image)
        Me.cmdSearchInspBy.Location = New System.Drawing.Point(180, 196)
        Me.cmdSearchInspBy.Name = "cmdSearchInspBy"
        Me.cmdSearchInspBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchInspBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchInspBy.TabIndex = 13
        Me.cmdSearchInspBy.TabStop = False
        Me.cmdSearchInspBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchInspBy, "Search")
        Me.cmdSearchInspBy.UseVisualStyleBackColor = False
        '
        'cmdSearchSlipNo
        '
        Me.cmdSearchSlipNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipNo.Image = CType(resources.GetObject("cmdSearchSlipNo.Image"), System.Drawing.Image)
        Me.cmdSearchSlipNo.Location = New System.Drawing.Point(180, 13)
        Me.cmdSearchSlipNo.Name = "cmdSearchSlipNo"
        Me.cmdSearchSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSlipNo.TabIndex = 40
        Me.cmdSearchSlipNo.TabStop = False
        Me.cmdSearchSlipNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSlipNo, "Search")
        Me.cmdSearchSlipNo.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(488, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 35
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(286, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 32
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
        Me.cmdPrint.Location = New System.Drawing.Point(420, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 34
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
        Me.CmdClose.Location = New System.Drawing.Point(622, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 37
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
        Me.CmdView.Location = New System.Drawing.Point(554, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 36
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
        Me.CmdDelete.Location = New System.Drawing.Point(352, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 33
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
        Me.CmdSave.Location = New System.Drawing.Point(218, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 31
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
        Me.CmdModify.Location = New System.Drawing.Point(150, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 30
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
        Me.CmdAdd.Location = New System.Drawing.Point(84, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 29
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraActualSize
        '
        Me.fraActualSize.BackColor = System.Drawing.SystemColors.Control
        Me.fraActualSize.Controls.Add(Me.txtActualNoGoSize)
        Me.fraActualSize.Controls.Add(Me.txtActualGoSize)
        Me.fraActualSize.Controls.Add(Me.Label29)
        Me.fraActualSize.Controls.Add(Me.Label28)
        Me.fraActualSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraActualSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraActualSize.Location = New System.Drawing.Point(0, 252)
        Me.fraActualSize.Name = "fraActualSize"
        Me.fraActualSize.Padding = New System.Windows.Forms.Padding(0)
        Me.fraActualSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraActualSize.Size = New System.Drawing.Size(439, 209)
        Me.fraActualSize.TabIndex = 92
        Me.fraActualSize.TabStop = False
        Me.fraActualSize.Text = "Actual Size"
        '
        'txtActualNoGoSize
        '
        Me.txtActualNoGoSize.AcceptsReturn = True
        Me.txtActualNoGoSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtActualNoGoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualNoGoSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActualNoGoSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualNoGoSize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtActualNoGoSize.Location = New System.Drawing.Point(176, 117)
        Me.txtActualNoGoSize.MaxLength = 0
        Me.txtActualNoGoSize.Name = "txtActualNoGoSize"
        Me.txtActualNoGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActualNoGoSize.Size = New System.Drawing.Size(165, 20)
        Me.txtActualNoGoSize.TabIndex = 26
        '
        'txtActualGoSize
        '
        Me.txtActualGoSize.AcceptsReturn = True
        Me.txtActualGoSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtActualGoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualGoSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActualGoSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualGoSize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtActualGoSize.Location = New System.Drawing.Point(176, 64)
        Me.txtActualGoSize.MaxLength = 0
        Me.txtActualGoSize.Name = "txtActualGoSize"
        Me.txtActualGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActualGoSize.Size = New System.Drawing.Size(165, 20)
        Me.txtActualGoSize.TabIndex = 25
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(69, 120)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(94, 13)
        Me.Label29.TabIndex = 94
        Me.Label29.Text = "Actual NoGo Size"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(85, 67)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(79, 13)
        Me.Label28.TabIndex = 93
        Me.Label28.Text = "Actual Go Size"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraCalibResult
        '
        Me.fraCalibResult.BackColor = System.Drawing.SystemColors.Control
        Me.fraCalibResult.Controls.Add(Me.SprdMain)
        Me.fraCalibResult.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCalibResult.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCalibResult.Location = New System.Drawing.Point(0, 252)
        Me.fraCalibResult.Name = "fraCalibResult"
        Me.fraCalibResult.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCalibResult.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCalibResult.Size = New System.Drawing.Size(439, 209)
        Me.fraCalibResult.TabIndex = 71
        Me.fraCalibResult.TabStop = False
        Me.fraCalibResult.Text = "Calibration Results"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(3, 16)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(427, 187)
        Me.SprdMain.TabIndex = 27
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.SprdInst)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(437, 252)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(329, 209)
        Me.Frame1.TabIndex = 70
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Master Instruments used in Calibration"
        '
        'SprdInst
        '
        Me.SprdInst.DataSource = Nothing
        Me.SprdInst.Location = New System.Drawing.Point(8, 16)
        Me.SprdInst.Name = "SprdInst"
        Me.SprdInst.OcxState = CType(resources.GetObject("SprdInst.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdInst.Size = New System.Drawing.Size(315, 187)
        Me.SprdInst.TabIndex = 28
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtCretificateIssueDate)
        Me.fraTop1.Controls.Add(Me.txtCretificateNo)
        Me.fraTop1.Controls.Add(Me.chkNABLLogo)
        Me.fraTop1.Controls.Add(Me.txtReceiptDate)
        Me.fraTop1.Controls.Add(Me.txtAppName)
        Me.fraTop1.Controls.Add(Me.txtInspName)
        Me.fraTop1.Controls.Add(Me.fraGoNoGo)
        Me.fraTop1.Controls.Add(Me.fraRange)
        Me.fraTop1.Controls.Add(Me.txtCalibProc)
        Me.fraTop1.Controls.Add(Me.txtVisualInsp)
        Me.fraTop1.Controls.Add(Me.txtUncertainty)
        Me.fraTop1.Controls.Add(Me.txtZeroError)
        Me.fraTop1.Controls.Add(Me.chkCalibOK)
        Me.fraTop1.Controls.Add(Me.txtSoakingTime)
        Me.fraTop1.Controls.Add(Me.txtHumidity)
        Me.fraTop1.Controls.Add(Me.txtAmbTemp)
        Me.fraTop1.Controls.Add(Me.cmdSearchAppBy)
        Me.fraTop1.Controls.Add(Me.txtAppBy)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.cmdSearchDocNo)
        Me.fraTop1.Controls.Add(Me.txtDocNo)
        Me.fraTop1.Controls.Add(Me.txtInspBy)
        Me.fraTop1.Controls.Add(Me.cmdSearchInspBy)
        Me.fraTop1.Controls.Add(Me.cmdSearchSlipNo)
        Me.fraTop1.Controls.Add(Me.txtDate)
        Me.fraTop1.Controls.Add(Me.txtSlipNo)
        Me.fraTop1.Controls.Add(Me.Label35)
        Me.fraTop1.Controls.Add(Me.Label34)
        Me.fraTop1.Controls.Add(Me._lblLabels_0)
        Me.fraTop1.Controls.Add(Me.Label33)
        Me.fraTop1.Controls.Add(Me.lblMenu)
        Me.fraTop1.Controls.Add(Me.lblCalibOK)
        Me.fraTop1.Controls.Add(Me.lblType)
        Me.fraTop1.Controls.Add(Me.Label32)
        Me.fraTop1.Controls.Add(Me.Label26)
        Me.fraTop1.Controls.Add(Me.lblCaliFacil)
        Me.fraTop1.Controls.Add(Me.Label13)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me._lblLabels_21)
        Me.fraTop1.Controls.Add(Me.Label25)
        Me.fraTop1.Controls.Add(Me.Label24)
        Me.fraTop1.Controls.Add(Me.Label22)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.lblMake)
        Me.fraTop1.Controls.Add(Me.lblMakersNo)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.lblFrequency)
        Me.fraTop1.Controls.Add(Me.Label23)
        Me.fraTop1.Controls.Add(Me.Label19)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.lblLocation)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.lblENo)
        Me.fraTop1.Controls.Add(Me.Label18)
        Me.fraTop1.Controls.Add(Me.lblLC)
        Me.fraTop1.Controls.Add(Me.Label17)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Controls.Add(Me.lblDescription)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(766, 258)
        Me.fraTop1.TabIndex = 24
        Me.fraTop1.TabStop = False
        '
        'txtCretificateIssueDate
        '
        Me.txtCretificateIssueDate.AcceptsReturn = True
        Me.txtCretificateIssueDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtCretificateIssueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCretificateIssueDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCretificateIssueDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCretificateIssueDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCretificateIssueDate.Location = New System.Drawing.Point(638, 216)
        Me.txtCretificateIssueDate.MaxLength = 0
        Me.txtCretificateIssueDate.Name = "txtCretificateIssueDate"
        Me.txtCretificateIssueDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCretificateIssueDate.Size = New System.Drawing.Size(121, 20)
        Me.txtCretificateIssueDate.TabIndex = 19
        '
        'txtCretificateNo
        '
        Me.txtCretificateNo.AcceptsReturn = True
        Me.txtCretificateNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCretificateNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCretificateNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCretificateNo.Enabled = False
        Me.txtCretificateNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCretificateNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCretificateNo.Location = New System.Drawing.Point(638, 196)
        Me.txtCretificateNo.MaxLength = 0
        Me.txtCretificateNo.Name = "txtCretificateNo"
        Me.txtCretificateNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCretificateNo.Size = New System.Drawing.Size(121, 20)
        Me.txtCretificateNo.TabIndex = 15
        '
        'chkNABLLogo
        '
        Me.chkNABLLogo.BackColor = System.Drawing.SystemColors.Control
        Me.chkNABLLogo.Checked = True
        Me.chkNABLLogo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNABLLogo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkNABLLogo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkNABLLogo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkNABLLogo.Location = New System.Drawing.Point(342, 237)
        Me.chkNABLLogo.Name = "chkNABLLogo"
        Me.chkNABLLogo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkNABLLogo.Size = New System.Drawing.Size(17, 17)
        Me.chkNABLLogo.TabIndex = 21
        Me.chkNABLLogo.UseVisualStyleBackColor = False
        '
        'txtReceiptDate
        '
        Me.txtReceiptDate.AcceptsReturn = True
        Me.txtReceiptDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReceiptDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReceiptDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReceiptDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceiptDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReceiptDate.Location = New System.Drawing.Point(650, 34)
        Me.txtReceiptDate.MaxLength = 0
        Me.txtReceiptDate.Name = "txtReceiptDate"
        Me.txtReceiptDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReceiptDate.Size = New System.Drawing.Size(109, 20)
        Me.txtReceiptDate.TabIndex = 3
        '
        'txtAppName
        '
        Me.txtAppName.AcceptsReturn = True
        Me.txtAppName.BackColor = System.Drawing.SystemColors.Window
        Me.txtAppName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAppName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAppName.Enabled = False
        Me.txtAppName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAppName.Location = New System.Drawing.Point(205, 216)
        Me.txtAppName.MaxLength = 0
        Me.txtAppName.Name = "txtAppName"
        Me.txtAppName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppName.Size = New System.Drawing.Size(263, 20)
        Me.txtAppName.TabIndex = 18
        '
        'txtInspName
        '
        Me.txtInspName.AcceptsReturn = True
        Me.txtInspName.BackColor = System.Drawing.SystemColors.Window
        Me.txtInspName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInspName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInspName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInspName.Location = New System.Drawing.Point(205, 196)
        Me.txtInspName.MaxLength = 0
        Me.txtInspName.Name = "txtInspName"
        Me.txtInspName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInspName.Size = New System.Drawing.Size(263, 20)
        Me.txtInspName.TabIndex = 14
        '
        'fraGoNoGo
        '
        Me.fraGoNoGo.BackColor = System.Drawing.SystemColors.Control
        Me.fraGoNoGo.Controls.Add(Me.lblNoGoSize)
        Me.fraGoNoGo.Controls.Add(Me.lblGoSize)
        Me.fraGoNoGo.Controls.Add(Me.Label27)
        Me.fraGoNoGo.Controls.Add(Me.Label21)
        Me.fraGoNoGo.Controls.Add(Me.lblWearSize)
        Me.fraGoNoGo.Controls.Add(Me.Label31)
        Me.fraGoNoGo.Controls.Add(Me.Label30)
        Me.fraGoNoGo.Controls.Add(Me.lblBasicSize)
        Me.fraGoNoGo.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraGoNoGo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraGoNoGo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraGoNoGo.Location = New System.Drawing.Point(15, 134)
        Me.fraGoNoGo.Name = "fraGoNoGo"
        Me.fraGoNoGo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraGoNoGo.Size = New System.Drawing.Size(460, 41)
        Me.fraGoNoGo.TabIndex = 83
        '
        'lblNoGoSize
        '
        Me.lblNoGoSize.BackColor = System.Drawing.SystemColors.Control
        Me.lblNoGoSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNoGoSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNoGoSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoGoSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNoGoSize.Location = New System.Drawing.Point(302, 2)
        Me.lblNoGoSize.Name = "lblNoGoSize"
        Me.lblNoGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNoGoSize.Size = New System.Drawing.Size(151, 19)
        Me.lblNoGoSize.TabIndex = 91
        '
        'lblGoSize
        '
        Me.lblGoSize.BackColor = System.Drawing.SystemColors.Control
        Me.lblGoSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGoSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGoSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGoSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGoSize.Location = New System.Drawing.Point(70, 2)
        Me.lblGoSize.Name = "lblGoSize"
        Me.lblGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGoSize.Size = New System.Drawing.Size(159, 19)
        Me.lblGoSize.TabIndex = 86
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(237, 7)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(60, 13)
        Me.Label27.TabIndex = 85
        Me.Label27.Text = "NoGo Size"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(16, 7)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(45, 13)
        Me.Label21.TabIndex = 84
        Me.Label21.Text = "Go Size"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWearSize
        '
        Me.lblWearSize.BackColor = System.Drawing.SystemColors.Control
        Me.lblWearSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWearSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWearSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWearSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWearSize.Location = New System.Drawing.Point(302, 22)
        Me.lblWearSize.Name = "lblWearSize"
        Me.lblWearSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWearSize.Size = New System.Drawing.Size(151, 19)
        Me.lblWearSize.TabIndex = 90
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(239, 25)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(57, 13)
        Me.Label31.TabIndex = 89
        Me.Label31.Text = "Wear Size"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.SystemColors.Control
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(1, 25)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(56, 13)
        Me.Label30.TabIndex = 88
        Me.Label30.Text = "Basic Size"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBasicSize
        '
        Me.lblBasicSize.BackColor = System.Drawing.SystemColors.Control
        Me.lblBasicSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBasicSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBasicSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBasicSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBasicSize.Location = New System.Drawing.Point(70, 22)
        Me.lblBasicSize.Name = "lblBasicSize"
        Me.lblBasicSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBasicSize.Size = New System.Drawing.Size(159, 19)
        Me.lblBasicSize.TabIndex = 87
        '
        'fraRange
        '
        Me.fraRange.BackColor = System.Drawing.SystemColors.Control
        Me.fraRange.Controls.Add(Me.Label9)
        Me.fraRange.Controls.Add(Me.Label11)
        Me.fraRange.Controls.Add(Me.lblMinRange)
        Me.fraRange.Controls.Add(Me.lblMaxRange)
        Me.fraRange.Controls.Add(Me.Label15)
        Me.fraRange.Controls.Add(Me.Label20)
        Me.fraRange.Controls.Add(Me.lblUnitRange)
        Me.fraRange.Controls.Add(Me.lblRange)
        Me.fraRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.fraRange.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraRange.Location = New System.Drawing.Point(16, 134)
        Me.fraRange.Name = "fraRange"
        Me.fraRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraRange.Size = New System.Drawing.Size(460, 41)
        Me.fraRange.TabIndex = 74
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(28, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(39, 13)
        Me.Label9.TabIndex = 82
        Me.Label9.Text = "Range"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(0, 25)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(64, 13)
        Me.Label11.TabIndex = 81
        Me.Label11.Text = "Min. Range"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMinRange
        '
        Me.lblMinRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblMinRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMinRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMinRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMinRange.Location = New System.Drawing.Point(70, 22)
        Me.lblMinRange.Name = "lblMinRange"
        Me.lblMinRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMinRange.Size = New System.Drawing.Size(79, 19)
        Me.lblMinRange.TabIndex = 80
        '
        'lblMaxRange
        '
        Me.lblMaxRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblMaxRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMaxRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMaxRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaxRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxRange.Location = New System.Drawing.Point(227, 22)
        Me.lblMaxRange.Name = "lblMaxRange"
        Me.lblMaxRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMaxRange.Size = New System.Drawing.Size(79, 19)
        Me.lblMaxRange.TabIndex = 79
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(152, 25)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(67, 13)
        Me.Label15.TabIndex = 78
        Me.Label15.Text = "Max. Range"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(308, 25)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(63, 13)
        Me.Label20.TabIndex = 77
        Me.Label20.Text = "Range Unit"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblUnitRange
        '
        Me.lblUnitRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnitRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnitRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUnitRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnitRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnitRange.Location = New System.Drawing.Point(374, 22)
        Me.lblUnitRange.Name = "lblUnitRange"
        Me.lblUnitRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUnitRange.Size = New System.Drawing.Size(79, 19)
        Me.lblUnitRange.TabIndex = 76
        '
        'lblRange
        '
        Me.lblRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRange.Location = New System.Drawing.Point(70, 0)
        Me.lblRange.Name = "lblRange"
        Me.lblRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRange.Size = New System.Drawing.Size(383, 19)
        Me.lblRange.TabIndex = 75
        '
        'txtCalibProc
        '
        Me.txtCalibProc.AcceptsReturn = True
        Me.txtCalibProc.BackColor = System.Drawing.SystemColors.Window
        Me.txtCalibProc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCalibProc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCalibProc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCalibProc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCalibProc.Location = New System.Drawing.Point(650, 115)
        Me.txtCalibProc.MaxLength = 0
        Me.txtCalibProc.Name = "txtCalibProc"
        Me.txtCalibProc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCalibProc.Size = New System.Drawing.Size(109, 20)
        Me.txtCalibProc.TabIndex = 7
        '
        'txtVisualInsp
        '
        Me.txtVisualInsp.AcceptsReturn = True
        Me.txtVisualInsp.BackColor = System.Drawing.SystemColors.Window
        Me.txtVisualInsp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVisualInsp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVisualInsp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVisualInsp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVisualInsp.Location = New System.Drawing.Point(650, 136)
        Me.txtVisualInsp.MaxLength = 0
        Me.txtVisualInsp.Name = "txtVisualInsp"
        Me.txtVisualInsp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVisualInsp.Size = New System.Drawing.Size(109, 20)
        Me.txtVisualInsp.TabIndex = 8
        '
        'txtUncertainty
        '
        Me.txtUncertainty.AcceptsReturn = True
        Me.txtUncertainty.BackColor = System.Drawing.SystemColors.Window
        Me.txtUncertainty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUncertainty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUncertainty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUncertainty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUncertainty.Location = New System.Drawing.Point(650, 176)
        Me.txtUncertainty.MaxLength = 0
        Me.txtUncertainty.Name = "txtUncertainty"
        Me.txtUncertainty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUncertainty.Size = New System.Drawing.Size(109, 20)
        Me.txtUncertainty.TabIndex = 11
        '
        'txtZeroError
        '
        Me.txtZeroError.AcceptsReturn = True
        Me.txtZeroError.BackColor = System.Drawing.SystemColors.Window
        Me.txtZeroError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtZeroError.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtZeroError.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtZeroError.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtZeroError.Location = New System.Drawing.Point(650, 156)
        Me.txtZeroError.MaxLength = 0
        Me.txtZeroError.Name = "txtZeroError"
        Me.txtZeroError.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtZeroError.Size = New System.Drawing.Size(109, 20)
        Me.txtZeroError.TabIndex = 9
        '
        'chkCalibOK
        '
        Me.chkCalibOK.BackColor = System.Drawing.SystemColors.Control
        Me.chkCalibOK.Checked = True
        Me.chkCalibOK.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCalibOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCalibOK.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCalibOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCalibOK.Location = New System.Drawing.Point(454, 237)
        Me.chkCalibOK.Name = "chkCalibOK"
        Me.chkCalibOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCalibOK.Size = New System.Drawing.Size(17, 17)
        Me.chkCalibOK.TabIndex = 23
        Me.chkCalibOK.UseVisualStyleBackColor = False
        '
        'txtSoakingTime
        '
        Me.txtSoakingTime.AcceptsReturn = True
        Me.txtSoakingTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtSoakingTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSoakingTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSoakingTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSoakingTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSoakingTime.Location = New System.Drawing.Point(650, 94)
        Me.txtSoakingTime.MaxLength = 0
        Me.txtSoakingTime.Name = "txtSoakingTime"
        Me.txtSoakingTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSoakingTime.Size = New System.Drawing.Size(109, 20)
        Me.txtSoakingTime.TabIndex = 6
        '
        'txtHumidity
        '
        Me.txtHumidity.AcceptsReturn = True
        Me.txtHumidity.BackColor = System.Drawing.SystemColors.Window
        Me.txtHumidity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHumidity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHumidity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHumidity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtHumidity.Location = New System.Drawing.Point(650, 74)
        Me.txtHumidity.MaxLength = 0
        Me.txtHumidity.Name = "txtHumidity"
        Me.txtHumidity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHumidity.Size = New System.Drawing.Size(109, 20)
        Me.txtHumidity.TabIndex = 5
        '
        'txtAmbTemp
        '
        Me.txtAmbTemp.AcceptsReturn = True
        Me.txtAmbTemp.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmbTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmbTemp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmbTemp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmbTemp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAmbTemp.Location = New System.Drawing.Point(650, 54)
        Me.txtAmbTemp.MaxLength = 0
        Me.txtAmbTemp.Name = "txtAmbTemp"
        Me.txtAmbTemp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmbTemp.Size = New System.Drawing.Size(109, 20)
        Me.txtAmbTemp.TabIndex = 4
        '
        'txtAppBy
        '
        Me.txtAppBy.AcceptsReturn = True
        Me.txtAppBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtAppBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAppBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAppBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAppBy.Location = New System.Drawing.Point(85, 216)
        Me.txtAppBy.MaxLength = 0
        Me.txtAppBy.Name = "txtAppBy"
        Me.txtAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppBy.Size = New System.Drawing.Size(93, 20)
        Me.txtAppBy.TabIndex = 16
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(85, 176)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(383, 20)
        Me.txtRemarks.TabIndex = 10
        '
        'txtDocNo
        '
        Me.txtDocNo.AcceptsReturn = True
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocNo.Location = New System.Drawing.Point(85, 34)
        Me.txtDocNo.MaxLength = 0
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocNo.Size = New System.Drawing.Size(93, 20)
        Me.txtDocNo.TabIndex = 2
        '
        'txtInspBy
        '
        Me.txtInspBy.AcceptsReturn = True
        Me.txtInspBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtInspBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInspBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInspBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInspBy.Location = New System.Drawing.Point(85, 196)
        Me.txtInspBy.MaxLength = 0
        Me.txtInspBy.Name = "txtInspBy"
        Me.txtInspBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInspBy.Size = New System.Drawing.Size(93, 20)
        Me.txtInspBy.TabIndex = 12
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(650, 13)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(109, 20)
        Me.txtDate.TabIndex = 1
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(85, 13)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipNo.TabIndex = 0
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.BackColor = System.Drawing.SystemColors.Control
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label35.Location = New System.Drawing.Point(510, 217)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(114, 13)
        Me.Label35.TabIndex = 101
        Me.Label35.Text = "Cretificate Issue Date"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.BackColor = System.Drawing.SystemColors.Control
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label34.Location = New System.Drawing.Point(491, 199)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(136, 13)
        Me.Label34.TabIndex = 100
        Me.Label34.Text = "Calibration Cretificate No"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(241, 239)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(89, 13)
        Me._lblLabels_0.TabIndex = 20
        Me._lblLabels_0.Text = "Print NABL Logo"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.BackColor = System.Drawing.SystemColors.Control
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label33.Location = New System.Drawing.Point(568, 37)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(72, 13)
        Me.Label33.TabIndex = 99
        Me.Label33.Text = "Receipt Date"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMenu
        '
        Me.lblMenu.AutoSize = True
        Me.lblMenu.BackColor = System.Drawing.SystemColors.Control
        Me.lblMenu.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMenu.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMenu.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMenu.Location = New System.Drawing.Point(477, 32)
        Me.lblMenu.Name = "lblMenu"
        Me.lblMenu.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMenu.Size = New System.Drawing.Size(69, 14)
        Me.lblMenu.TabIndex = 98
        Me.lblMenu.Text = "mnuIMTEInsp"
        Me.lblMenu.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCalibOK
        '
        Me.lblCalibOK.AutoSize = True
        Me.lblCalibOK.BackColor = System.Drawing.SystemColors.Control
        Me.lblCalibOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCalibOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCalibOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCalibOK.Location = New System.Drawing.Point(488, 16)
        Me.lblCalibOK.Name = "lblCalibOK"
        Me.lblCalibOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCalibOK.Size = New System.Drawing.Size(15, 14)
        Me.lblCalibOK.TabIndex = 97
        Me.lblCalibOK.Text = "Y"
        Me.lblCalibOK.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Location = New System.Drawing.Point(317, 13)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.Size = New System.Drawing.Size(151, 19)
        Me.lblType.TabIndex = 96
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.SystemColors.Control
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(284, 16)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(31, 13)
        Me.Label32.TabIndex = 95
        Me.Label32.Text = "Type"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(252, 37)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(58, 13)
        Me.Label26.TabIndex = 73
        Me.Label26.Text = "Cali. Facil."
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCaliFacil
        '
        Me.lblCaliFacil.BackColor = System.Drawing.SystemColors.Control
        Me.lblCaliFacil.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCaliFacil.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCaliFacil.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaliFacil.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCaliFacil.Location = New System.Drawing.Point(317, 34)
        Me.lblCaliFacil.Name = "lblCaliFacil"
        Me.lblCaliFacil.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCaliFacil.Size = New System.Drawing.Size(151, 19)
        Me.lblCaliFacil.TabIndex = 72
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(553, 118)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(87, 13)
        Me.Label13.TabIndex = 69
        Me.Label13.Text = "Calib Procedure"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(546, 139)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(92, 13)
        Me.Label12.TabIndex = 68
        Me.Label12.Text = "Visual Inspection"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(578, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Uncertainty"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(586, 159)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 66
        Me.Label4.Text = "Zero Error"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_21
        '
        Me._lblLabels_21.AutoSize = True
        Me._lblLabels_21.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_21.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_21, CType(21, Short))
        Me._lblLabels_21.Location = New System.Drawing.Point(368, 239)
        Me._lblLabels_21.Name = "_lblLabels_21"
        Me._lblLabels_21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_21.Size = New System.Drawing.Size(81, 13)
        Me._lblLabels_21.TabIndex = 22
        Me._lblLabels_21.Text = "Calibration OK"
        Me._lblLabels_21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(535, 97)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(104, 13)
        Me.Label25.TabIndex = 65
        Me.Label25.Text = "Soaking Time (Hrs)"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(574, 77)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(74, 13)
        Me.Label24.TabIndex = 64
        Me.Label24.Text = "Humidity (%)"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(543, 57)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(99, 13)
        Me.Label22.TabIndex = 63
        Me.Label22.Text = "Ambient Temp (C)"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(281, 97)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(35, 13)
        Me.Label6.TabIndex = 62
        Me.Label6.Text = "Make"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMake
        '
        Me.lblMake.BackColor = System.Drawing.SystemColors.Control
        Me.lblMake.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMake.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMake.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMake.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMake.Location = New System.Drawing.Point(317, 94)
        Me.lblMake.Name = "lblMake"
        Me.lblMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMake.Size = New System.Drawing.Size(151, 19)
        Me.lblMake.TabIndex = 61
        '
        'lblMakersNo
        '
        Me.lblMakersNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblMakersNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMakersNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMakersNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMakersNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMakersNo.Location = New System.Drawing.Point(317, 74)
        Me.lblMakersNo.Name = "lblMakersNo"
        Me.lblMakersNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMakersNo.Size = New System.Drawing.Size(151, 19)
        Me.lblMakersNo.TabIndex = 60
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(251, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 59
        Me.Label3.Text = "Makers No"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFrequency
        '
        Me.lblFrequency.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrequency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFrequency.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFrequency.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrequency.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFrequency.Location = New System.Drawing.Point(317, 115)
        Me.lblFrequency.Name = "lblFrequency"
        Me.lblFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFrequency.Size = New System.Drawing.Size(151, 19)
        Me.lblFrequency.TabIndex = 58
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(253, 118)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(59, 13)
        Me.Label23.TabIndex = 57
        Me.Label23.Text = "Frequency"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(17, 219)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(47, 13)
        Me.Label19.TabIndex = 56
        Me.Label19.Text = "App. By"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(31, 118)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(50, 13)
        Me.Label14.TabIndex = 55
        Me.Label14.Text = "Location"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLocation
        '
        Me.lblLocation.BackColor = System.Drawing.SystemColors.Control
        Me.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLocation.Location = New System.Drawing.Point(85, 115)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLocation.Size = New System.Drawing.Size(159, 19)
        Me.lblLocation.TabIndex = 54
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(16, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 53
        Me.Label1.Text = "Description"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(48, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(34, 13)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "E. No"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblENo
        '
        Me.lblENo.BackColor = System.Drawing.SystemColors.Control
        Me.lblENo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblENo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblENo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblENo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblENo.Location = New System.Drawing.Point(85, 74)
        Me.lblENo.Name = "lblENo"
        Me.lblENo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblENo.Size = New System.Drawing.Size(159, 19)
        Me.lblENo.TabIndex = 51
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(17, 179)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(51, 13)
        Me.Label18.TabIndex = 50
        Me.Label18.Text = "Remarks"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLC
        '
        Me.lblLC.BackColor = System.Drawing.SystemColors.Control
        Me.lblLC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLC.Location = New System.Drawing.Point(85, 94)
        Me.lblLC.Name = "lblLC"
        Me.lblLC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLC.Size = New System.Drawing.Size(159, 19)
        Me.lblLC.TabIndex = 49
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(53, 97)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(28, 13)
        Me.Label17.TabIndex = 48
        Me.Label17.Text = "L. C."
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(37, 37)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(45, 13)
        Me.Label16.TabIndex = 47
        Me.Label16.Text = "Doc No"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDescription
        '
        Me.lblDescription.BackColor = System.Drawing.SystemColors.Control
        Me.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDescription.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDescription.Location = New System.Drawing.Point(85, 54)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDescription.Size = New System.Drawing.Size(383, 19)
        Me.lblDescription.TabIndex = 46
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(17, 199)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(47, 13)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "Insp. By"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(552, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(90, 13)
        Me.Label8.TabIndex = 42
        Me.Label8.Text = "Calibration Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(9, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "Number"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 94
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 458)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(767, 51)
        Me.FraMovement.TabIndex = 38
        Me.FraMovement.TabStop = False
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(10, 10)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 39
        Me.lblMkey.Text = "lblMkey"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(766, 457)
        Me.SprdView.TabIndex = 44
        '
        'frmIMTEInsp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(767, 509)
        Me.Controls.Add(Me.fraActualSize)
        Me.Controls.Add(Me.fraCalibResult)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.fraTop1)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmIMTEInsp"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IMTE Inspection (Calibration)"
        Me.fraActualSize.ResumeLayout(False)
        Me.fraActualSize.PerformLayout()
        Me.fraCalibResult.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdInst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.fraGoNoGo.ResumeLayout(False)
        Me.fraGoNoGo.PerformLayout()
        Me.fraRange.ResumeLayout(False)
        Me.fraRange.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
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