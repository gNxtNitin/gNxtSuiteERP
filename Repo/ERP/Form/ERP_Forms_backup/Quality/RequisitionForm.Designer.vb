Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmRequisitionForm
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
    Public WithEvents cboPreviousFailure As System.Windows.Forms.ComboBox
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents cboUrgency As System.Windows.Forms.ComboBox
    Public WithEvents txtUrgencyReason As System.Windows.Forms.TextBox
    Public WithEvents SprdSample As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents fraSample As System.Windows.Forms.GroupBox
    Public WithEvents SprdRepair As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraRepair As System.Windows.Forms.GroupBox
    Public WithEvents SprdNew As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraNew As System.Windows.Forms.GroupBox
    Public WithEvents cmdSearchDeptCode As System.Windows.Forms.Button
    Public WithEvents txtDeptCode As System.Windows.Forms.TextBox
    Public WithEvents txtDeptName As System.Windows.Forms.TextBox
    Public WithEvents cboReqType As System.Windows.Forms.ComboBox
    Public WithEvents txtReqDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchReqBy As System.Windows.Forms.Button
    Public WithEvents txtReqBy As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtAppBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchAppBy As System.Windows.Forms.Button
    Public WithEvents txtReqName As System.Windows.Forms.TextBox
    Public WithEvents txtAppName As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents fraRequest As System.Windows.Forms.GroupBox
    Public WithEvents SprdAction As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraAction1 As System.Windows.Forms.GroupBox
    Public WithEvents txtActionDate As System.Windows.Forms.TextBox
    Public WithEvents cboReqStatus As System.Windows.Forms.ComboBox
    Public WithEvents txtStatusReason As System.Windows.Forms.TextBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents fraAction2 As System.Windows.Forms.GroupBox
    Public WithEvents txtReqNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchReqNo As System.Windows.Forms.Button
    Public WithEvents Frame4 As System.Windows.Forms.Panel
    Public WithEvents Frame3 As System.Windows.Forms.Panel
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
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
    Public WithEvents lblFormType As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRequisitionForm))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchDeptCode = New System.Windows.Forms.Button()
        Me.cmdSearchReqBy = New System.Windows.Forms.Button()
        Me.cmdSearchAppBy = New System.Windows.Forms.Button()
        Me.cmdSearchReqNo = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraRequest = New System.Windows.Forms.GroupBox()
        Me.fraSample = New System.Windows.Forms.GroupBox()
        Me.cboPreviousFailure = New System.Windows.Forms.ComboBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.cboUrgency = New System.Windows.Forms.ComboBox()
        Me.txtUrgencyReason = New System.Windows.Forms.TextBox()
        Me.SprdSample = New AxFPSpreadADO.AxfpSpread()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.fraRepair = New System.Windows.Forms.GroupBox()
        Me.SprdRepair = New AxFPSpreadADO.AxfpSpread()
        Me.fraNew = New System.Windows.Forms.GroupBox()
        Me.SprdNew = New AxFPSpreadADO.AxfpSpread()
        Me.txtDeptCode = New System.Windows.Forms.TextBox()
        Me.txtDeptName = New System.Windows.Forms.TextBox()
        Me.cboReqType = New System.Windows.Forms.ComboBox()
        Me.txtReqDate = New System.Windows.Forms.TextBox()
        Me.txtReqBy = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAppBy = New System.Windows.Forms.TextBox()
        Me.txtReqName = New System.Windows.Forms.TextBox()
        Me.txtAppName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.fraAction1 = New System.Windows.Forms.GroupBox()
        Me.SprdAction = New AxFPSpreadADO.AxfpSpread()
        Me.fraAction2 = New System.Windows.Forms.GroupBox()
        Me.txtActionDate = New System.Windows.Forms.TextBox()
        Me.cboReqStatus = New System.Windows.Forms.ComboBox()
        Me.txtStatusReason = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtReqNo = New System.Windows.Forms.TextBox()
        Me.Frame4 = New System.Windows.Forms.Panel()
        Me.Frame3 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblFormType = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.fraRequest.SuspendLayout()
        Me.fraSample.SuspendLayout()
        CType(Me.SprdSample, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraRepair.SuspendLayout()
        CType(Me.SprdRepair, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraNew.SuspendLayout()
        CType(Me.SprdNew, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAction1.SuspendLayout()
        CType(Me.SprdAction, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraAction2.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchDeptCode
        '
        Me.cmdSearchDeptCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDeptCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDeptCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDeptCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDeptCode.Image = CType(resources.GetObject("cmdSearchDeptCode.Image"), System.Drawing.Image)
        Me.cmdSearchDeptCode.Location = New System.Drawing.Point(252, 36)
        Me.cmdSearchDeptCode.Name = "cmdSearchDeptCode"
        Me.cmdSearchDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDeptCode.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchDeptCode.TabIndex = 37
        Me.cmdSearchDeptCode.TabStop = False
        Me.cmdSearchDeptCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDeptCode, "Search")
        Me.cmdSearchDeptCode.UseVisualStyleBackColor = False
        '
        'cmdSearchReqBy
        '
        Me.cmdSearchReqBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchReqBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchReqBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchReqBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchReqBy.Image = CType(resources.GetObject("cmdSearchReqBy.Image"), System.Drawing.Image)
        Me.cmdSearchReqBy.Location = New System.Drawing.Point(252, 80)
        Me.cmdSearchReqBy.Name = "cmdSearchReqBy"
        Me.cmdSearchReqBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchReqBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchReqBy.TabIndex = 26
        Me.cmdSearchReqBy.TabStop = False
        Me.cmdSearchReqBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchReqBy, "Search")
        Me.cmdSearchReqBy.UseVisualStyleBackColor = False
        '
        'cmdSearchAppBy
        '
        Me.cmdSearchAppBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAppBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAppBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAppBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAppBy.Image = CType(resources.GetObject("cmdSearchAppBy.Image"), System.Drawing.Image)
        Me.cmdSearchAppBy.Location = New System.Drawing.Point(252, 102)
        Me.cmdSearchAppBy.Name = "cmdSearchAppBy"
        Me.cmdSearchAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAppBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchAppBy.TabIndex = 25
        Me.cmdSearchAppBy.TabStop = False
        Me.cmdSearchAppBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAppBy, "Search")
        Me.cmdSearchAppBy.UseVisualStyleBackColor = False
        '
        'cmdSearchReqNo
        '
        Me.cmdSearchReqNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchReqNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchReqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchReqNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchReqNo.Image = CType(resources.GetObject("cmdSearchReqNo.Image"), System.Drawing.Image)
        Me.cmdSearchReqNo.Location = New System.Drawing.Point(252, 13)
        Me.cmdSearchReqNo.Name = "cmdSearchReqNo"
        Me.cmdSearchReqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchReqNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchReqNo.TabIndex = 34
        Me.cmdSearchReqNo.TabStop = False
        Me.cmdSearchReqNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchReqNo, "Search")
        Me.cmdSearchReqNo.UseVisualStyleBackColor = False
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
        Me.CmdPreview.TabIndex = 17
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
        Me.cmdSavePrint.TabIndex = 14
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
        Me.cmdPrint.TabIndex = 16
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
        Me.CmdClose.TabIndex = 19
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
        Me.CmdView.TabIndex = 18
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
        Me.CmdDelete.TabIndex = 15
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
        Me.CmdSave.TabIndex = 13
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
        Me.CmdModify.TabIndex = 12
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
        Me.CmdAdd.TabIndex = 11
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraRequest
        '
        Me.fraRequest.BackColor = System.Drawing.SystemColors.Control
        Me.fraRequest.Controls.Add(Me.fraSample)
        Me.fraRequest.Controls.Add(Me.fraRepair)
        Me.fraRequest.Controls.Add(Me.fraNew)
        Me.fraRequest.Controls.Add(Me.cmdSearchDeptCode)
        Me.fraRequest.Controls.Add(Me.txtDeptCode)
        Me.fraRequest.Controls.Add(Me.txtDeptName)
        Me.fraRequest.Controls.Add(Me.cboReqType)
        Me.fraRequest.Controls.Add(Me.txtReqDate)
        Me.fraRequest.Controls.Add(Me.cmdSearchReqBy)
        Me.fraRequest.Controls.Add(Me.txtReqBy)
        Me.fraRequest.Controls.Add(Me.txtRemarks)
        Me.fraRequest.Controls.Add(Me.txtAppBy)
        Me.fraRequest.Controls.Add(Me.cmdSearchAppBy)
        Me.fraRequest.Controls.Add(Me.txtReqName)
        Me.fraRequest.Controls.Add(Me.txtAppName)
        Me.fraRequest.Controls.Add(Me.Label2)
        Me.fraRequest.Controls.Add(Me.Label1)
        Me.fraRequest.Controls.Add(Me.Label8)
        Me.fraRequest.Controls.Add(Me.Label10)
        Me.fraRequest.Controls.Add(Me.Label18)
        Me.fraRequest.Controls.Add(Me.Label19)
        Me.fraRequest.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRequest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraRequest.Location = New System.Drawing.Point(0, 40)
        Me.fraRequest.Name = "fraRequest"
        Me.fraRequest.Padding = New System.Windows.Forms.Padding(0)
        Me.fraRequest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraRequest.Size = New System.Drawing.Size(766, 264)
        Me.fraRequest.TabIndex = 24
        Me.fraRequest.TabStop = False
        Me.fraRequest.Text = "Request Entry"
        '
        'fraSample
        '
        Me.fraSample.BackColor = System.Drawing.SystemColors.Control
        Me.fraSample.Controls.Add(Me.cboPreviousFailure)
        Me.fraSample.Controls.Add(Me.txtCustomer)
        Me.fraSample.Controls.Add(Me.cboUrgency)
        Me.fraSample.Controls.Add(Me.txtUrgencyReason)
        Me.fraSample.Controls.Add(Me.SprdSample)
        Me.fraSample.Controls.Add(Me.Label3)
        Me.fraSample.Controls.Add(Me.Label6)
        Me.fraSample.Controls.Add(Me.Label9)
        Me.fraSample.Controls.Add(Me.Label11)
        Me.fraSample.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSample.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSample.Location = New System.Drawing.Point(0, 128)
        Me.fraSample.Name = "fraSample"
        Me.fraSample.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSample.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSample.Size = New System.Drawing.Size(766, 137)
        Me.fraSample.TabIndex = 40
        Me.fraSample.TabStop = False
        Me.fraSample.Text = "Request for Samples"
        '
        'cboPreviousFailure
        '
        Me.cboPreviousFailure.BackColor = System.Drawing.SystemColors.Window
        Me.cboPreviousFailure.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPreviousFailure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPreviousFailure.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPreviousFailure.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPreviousFailure.Location = New System.Drawing.Point(608, 36)
        Me.cboPreviousFailure.Name = "cboPreviousFailure"
        Me.cboPreviousFailure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPreviousFailure.Size = New System.Drawing.Size(117, 22)
        Me.cboPreviousFailure.TabIndex = 42
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCustomer.Location = New System.Drawing.Point(157, 14)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(327, 20)
        Me.txtCustomer.TabIndex = 44
        '
        'cboUrgency
        '
        Me.cboUrgency.BackColor = System.Drawing.SystemColors.Window
        Me.cboUrgency.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboUrgency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUrgency.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUrgency.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboUrgency.Location = New System.Drawing.Point(608, 13)
        Me.cboUrgency.Name = "cboUrgency"
        Me.cboUrgency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboUrgency.Size = New System.Drawing.Size(117, 22)
        Me.cboUrgency.TabIndex = 43
        '
        'txtUrgencyReason
        '
        Me.txtUrgencyReason.AcceptsReturn = True
        Me.txtUrgencyReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtUrgencyReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUrgencyReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUrgencyReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUrgencyReason.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUrgencyReason.Location = New System.Drawing.Point(157, 37)
        Me.txtUrgencyReason.MaxLength = 0
        Me.txtUrgencyReason.Name = "txtUrgencyReason"
        Me.txtUrgencyReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUrgencyReason.Size = New System.Drawing.Size(327, 20)
        Me.txtUrgencyReason.TabIndex = 41
        '
        'SprdSample
        '
        Me.SprdSample.DataSource = Nothing
        Me.SprdSample.Location = New System.Drawing.Point(3, 64)
        Me.SprdSample.Name = "SprdSample"
        Me.SprdSample.OcxState = CType(resources.GetObject("SprdSample.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdSample.Size = New System.Drawing.Size(755, 67)
        Me.SprdSample.TabIndex = 45
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(504, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Previous Failure"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(95, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Customer"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(548, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 47
        Me.Label9.Text = "Urgency"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(38, 40)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(103, 13)
        Me.Label11.TabIndex = 46
        Me.Label11.Text = "Reason of Urgency"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraRepair
        '
        Me.fraRepair.BackColor = System.Drawing.SystemColors.Control
        Me.fraRepair.Controls.Add(Me.SprdRepair)
        Me.fraRepair.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraRepair.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraRepair.Location = New System.Drawing.Point(0, 128)
        Me.fraRepair.Name = "fraRepair"
        Me.fraRepair.Padding = New System.Windows.Forms.Padding(0)
        Me.fraRepair.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraRepair.Size = New System.Drawing.Size(766, 137)
        Me.fraRepair.TabIndex = 49
        Me.fraRepair.TabStop = False
        Me.fraRepair.Text = "Request for Reapir of Instruments"
        '
        'SprdRepair
        '
        Me.SprdRepair.DataSource = Nothing
        Me.SprdRepair.Location = New System.Drawing.Point(3, 16)
        Me.SprdRepair.Name = "SprdRepair"
        Me.SprdRepair.OcxState = CType(resources.GetObject("SprdRepair.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdRepair.Size = New System.Drawing.Size(755, 115)
        Me.SprdRepair.TabIndex = 50
        '
        'fraNew
        '
        Me.fraNew.BackColor = System.Drawing.SystemColors.Control
        Me.fraNew.Controls.Add(Me.SprdNew)
        Me.fraNew.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraNew.Location = New System.Drawing.Point(0, 128)
        Me.fraNew.Name = "fraNew"
        Me.fraNew.Padding = New System.Windows.Forms.Padding(0)
        Me.fraNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraNew.Size = New System.Drawing.Size(766, 137)
        Me.fraNew.TabIndex = 51
        Me.fraNew.TabStop = False
        Me.fraNew.Text = "Request for New Instrument"
        '
        'SprdNew
        '
        Me.SprdNew.DataSource = Nothing
        Me.SprdNew.Location = New System.Drawing.Point(3, 16)
        Me.SprdNew.Name = "SprdNew"
        Me.SprdNew.OcxState = CType(resources.GetObject("SprdNew.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdNew.Size = New System.Drawing.Size(755, 115)
        Me.SprdNew.TabIndex = 52
        '
        'txtDeptCode
        '
        Me.txtDeptCode.AcceptsReturn = True
        Me.txtDeptCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeptCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeptCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeptCode.Location = New System.Drawing.Point(157, 36)
        Me.txtDeptCode.MaxLength = 0
        Me.txtDeptCode.Name = "txtDeptCode"
        Me.txtDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeptCode.Size = New System.Drawing.Size(93, 20)
        Me.txtDeptCode.TabIndex = 3
        '
        'txtDeptName
        '
        Me.txtDeptName.AcceptsReturn = True
        Me.txtDeptName.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeptName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeptName.Enabled = False
        Me.txtDeptName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeptName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeptName.Location = New System.Drawing.Point(277, 36)
        Me.txtDeptName.MaxLength = 0
        Me.txtDeptName.Name = "txtDeptName"
        Me.txtDeptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeptName.Size = New System.Drawing.Size(447, 20)
        Me.txtDeptName.TabIndex = 4
        '
        'cboReqType
        '
        Me.cboReqType.BackColor = System.Drawing.SystemColors.Window
        Me.cboReqType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboReqType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReqType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReqType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboReqType.Location = New System.Drawing.Point(607, 13)
        Me.cboReqType.Name = "cboReqType"
        Me.cboReqType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboReqType.Size = New System.Drawing.Size(117, 22)
        Me.cboReqType.TabIndex = 2
        '
        'txtReqDate
        '
        Me.txtReqDate.AcceptsReturn = True
        Me.txtReqDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReqDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReqDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReqDate.Location = New System.Drawing.Point(157, 14)
        Me.txtReqDate.MaxLength = 0
        Me.txtReqDate.Name = "txtReqDate"
        Me.txtReqDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReqDate.Size = New System.Drawing.Size(93, 20)
        Me.txtReqDate.TabIndex = 1
        '
        'txtReqBy
        '
        Me.txtReqBy.AcceptsReturn = True
        Me.txtReqBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReqBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReqBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReqBy.Location = New System.Drawing.Point(157, 80)
        Me.txtReqBy.MaxLength = 0
        Me.txtReqBy.Name = "txtReqBy"
        Me.txtReqBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReqBy.Size = New System.Drawing.Size(93, 20)
        Me.txtReqBy.TabIndex = 6
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(157, 58)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(567, 20)
        Me.txtRemarks.TabIndex = 5
        '
        'txtAppBy
        '
        Me.txtAppBy.AcceptsReturn = True
        Me.txtAppBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtAppBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAppBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAppBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAppBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAppBy.Location = New System.Drawing.Point(157, 102)
        Me.txtAppBy.MaxLength = 0
        Me.txtAppBy.Name = "txtAppBy"
        Me.txtAppBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppBy.Size = New System.Drawing.Size(93, 20)
        Me.txtAppBy.TabIndex = 8
        '
        'txtReqName
        '
        Me.txtReqName.AcceptsReturn = True
        Me.txtReqName.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReqName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReqName.Enabled = False
        Me.txtReqName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReqName.Location = New System.Drawing.Point(277, 80)
        Me.txtReqName.MaxLength = 0
        Me.txtReqName.Name = "txtReqName"
        Me.txtReqName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReqName.Size = New System.Drawing.Size(447, 20)
        Me.txtReqName.TabIndex = 7
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
        Me.txtAppName.Location = New System.Drawing.Point(277, 102)
        Me.txtAppName.MaxLength = 0
        Me.txtAppName.Name = "txtAppName"
        Me.txtAppName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAppName.Size = New System.Drawing.Size(447, 20)
        Me.txtAppName.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(55, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(89, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Requested Dept"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(508, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Requisition Type"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(53, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(91, 13)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Requisition Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(68, 82)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(77, 13)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "Requested By"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(84, 60)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(51, 13)
        Me.Label18.TabIndex = 28
        Me.Label18.Text = "Remarks"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(36, 104)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(109, 13)
        Me.Label19.TabIndex = 27
        Me.Label19.Text = "Approved By (HOD)"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraAction1
        '
        Me.fraAction1.BackColor = System.Drawing.SystemColors.Control
        Me.fraAction1.Controls.Add(Me.SprdAction)
        Me.fraAction1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAction1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAction1.Location = New System.Drawing.Point(0, 303)
        Me.fraAction1.Name = "fraAction1"
        Me.fraAction1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAction1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAction1.Size = New System.Drawing.Size(766, 103)
        Me.fraAction1.TabIndex = 39
        Me.fraAction1.TabStop = False
        Me.fraAction1.Text = "Action Entry"
        '
        'SprdAction
        '
        Me.SprdAction.DataSource = Nothing
        Me.SprdAction.Location = New System.Drawing.Point(3, 14)
        Me.SprdAction.Name = "SprdAction"
        Me.SprdAction.OcxState = CType(resources.GetObject("SprdAction.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdAction.Size = New System.Drawing.Size(755, 83)
        Me.SprdAction.TabIndex = 10
        '
        'fraAction2
        '
        Me.fraAction2.BackColor = System.Drawing.SystemColors.Control
        Me.fraAction2.Controls.Add(Me.txtActionDate)
        Me.fraAction2.Controls.Add(Me.cboReqStatus)
        Me.fraAction2.Controls.Add(Me.txtStatusReason)
        Me.fraAction2.Controls.Add(Me.Label14)
        Me.fraAction2.Controls.Add(Me.Label13)
        Me.fraAction2.Controls.Add(Me.Label12)
        Me.fraAction2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAction2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAction2.Location = New System.Drawing.Point(0, 396)
        Me.fraAction2.Name = "fraAction2"
        Me.fraAction2.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAction2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAction2.Size = New System.Drawing.Size(766, 63)
        Me.fraAction2.TabIndex = 53
        Me.fraAction2.TabStop = False
        '
        'txtActionDate
        '
        Me.txtActionDate.AcceptsReturn = True
        Me.txtActionDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtActionDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActionDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActionDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActionDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtActionDate.Location = New System.Drawing.Point(157, 14)
        Me.txtActionDate.MaxLength = 0
        Me.txtActionDate.Name = "txtActionDate"
        Me.txtActionDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActionDate.Size = New System.Drawing.Size(93, 20)
        Me.txtActionDate.TabIndex = 56
        '
        'cboReqStatus
        '
        Me.cboReqStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboReqStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboReqStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReqStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReqStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboReqStatus.Location = New System.Drawing.Point(631, 13)
        Me.cboReqStatus.Name = "cboReqStatus"
        Me.cboReqStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboReqStatus.Size = New System.Drawing.Size(93, 22)
        Me.cboReqStatus.TabIndex = 55
        '
        'txtStatusReason
        '
        Me.txtStatusReason.AcceptsReturn = True
        Me.txtStatusReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtStatusReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStatusReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStatusReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatusReason.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtStatusReason.Location = New System.Drawing.Point(157, 36)
        Me.txtStatusReason.MaxLength = 0
        Me.txtStatusReason.Name = "txtStatusReason"
        Me.txtStatusReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStatusReason.Size = New System.Drawing.Size(567, 20)
        Me.txtStatusReason.TabIndex = 54
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(80, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(66, 13)
        Me.Label14.TabIndex = 59
        Me.Label14.Text = "Action Date"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(516, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(98, 13)
        Me.Label13.TabIndex = 58
        Me.Label13.Text = "Requisition Status"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(64, 38)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(78, 13)
        Me.Label12.TabIndex = 57
        Me.Label12.Text = "Status Reason"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtReqNo)
        Me.Frame2.Controls.Add(Me.cmdSearchReqNo)
        Me.Frame2.Controls.Add(Me.Frame4)
        Me.Frame2.Controls.Add(Me.Frame3)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(766, 40)
        Me.Frame2.TabIndex = 31
        Me.Frame2.TabStop = False
        '
        'txtReqNo
        '
        Me.txtReqNo.AcceptsReturn = True
        Me.txtReqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtReqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReqNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReqNo.Location = New System.Drawing.Point(157, 14)
        Me.txtReqNo.MaxLength = 0
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReqNo.Size = New System.Drawing.Size(93, 20)
        Me.txtReqNo.TabIndex = 0
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(16, 144)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(460, 41)
        Me.Frame4.TabIndex = 33
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(16, 144)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(460, 41)
        Me.Frame3.TabIndex = 32
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(53, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "Requisition No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 55
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
        Me.FraMovement.Controls.Add(Me.lblFormType)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 458)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(767, 51)
        Me.FraMovement.TabIndex = 20
        Me.FraMovement.TabStop = False
        '
        'lblFormType
        '
        Me.lblFormType.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFormType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFormType.Location = New System.Drawing.Point(10, 24)
        Me.lblFormType.Name = "lblFormType"
        Me.lblFormType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFormType.Size = New System.Drawing.Size(69, 17)
        Me.lblFormType.TabIndex = 23
        Me.lblFormType.Text = "lblFormType"
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
        Me.lblMkey.Size = New System.Drawing.Size(69, 17)
        Me.lblMkey.TabIndex = 21
        Me.lblMkey.Text = "lblMkey"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(766, 457)
        Me.SprdView.TabIndex = 22
        '
        'frmRequisitionForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(767, 509)
        Me.Controls.Add(Me.fraRequest)
        Me.Controls.Add(Me.fraAction1)
        Me.Controls.Add(Me.fraAction2)
        Me.Controls.Add(Me.Frame2)
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
        Me.Name = "frmRequisitionForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Requisition Form for Laboratory (Standard Room)"
        Me.fraRequest.ResumeLayout(False)
        Me.fraRequest.PerformLayout()
        Me.fraSample.ResumeLayout(False)
        Me.fraSample.PerformLayout()
        CType(Me.SprdSample, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraRepair.ResumeLayout(False)
        CType(Me.SprdRepair, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraNew.ResumeLayout(False)
        CType(Me.SprdNew, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAction1.ResumeLayout(False)
        CType(Me.SprdAction, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraAction2.ResumeLayout(False)
        Me.fraAction2.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
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