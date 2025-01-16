Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmProcQualRecord
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
    Public WithEvents txtApprovedBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchApprBy As System.Windows.Forms.Button
    Public WithEvents txtNote As System.Windows.Forms.TextBox
    Public WithEvents txtApprovedDate As System.Windows.Forms.TextBox
    Public WithEvents txtNameProcess As System.Windows.Forms.TextBox
    Public WithEvents cboFinalRemarks As System.Windows.Forms.ComboBox
    Public WithEvents cmdSearchMan As System.Windows.Forms.Button
    Public WithEvents txtManager As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchProdSuper As System.Windows.Forms.Button
    Public WithEvents txtProdSupervisor As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchQCInsp As System.Windows.Forms.Button
    Public WithEvents txtQCInspector As System.Windows.Forms.TextBox
    Public WithEvents txtMaxTrialOp As System.Windows.Forms.TextBox
    Public WithEvents txtMaxTrialDate As System.Windows.Forms.TextBox
    Public WithEvents txtMinTrialOp As System.Windows.Forms.TextBox
    Public WithEvents txtNameEquip As System.Windows.Forms.TextBox
    Public WithEvents txtNameTypeProd As System.Windows.Forms.TextBox
    Public WithEvents txtMinTrialDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSlipNo As System.Windows.Forms.Button
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblApprovedBy As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents lblManager As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblProdSupervisor As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents lblQCInspector As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProcQualRecord))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchApprBy = New System.Windows.Forms.Button()
        Me.cmdSearchMan = New System.Windows.Forms.Button()
        Me.cmdSearchProdSuper = New System.Windows.Forms.Button()
        Me.cmdSearchQCInsp = New System.Windows.Forms.Button()
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
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtApprovedBy = New System.Windows.Forms.TextBox()
        Me.txtNote = New System.Windows.Forms.TextBox()
        Me.txtApprovedDate = New System.Windows.Forms.TextBox()
        Me.txtNameProcess = New System.Windows.Forms.TextBox()
        Me.cboFinalRemarks = New System.Windows.Forms.ComboBox()
        Me.txtManager = New System.Windows.Forms.TextBox()
        Me.txtProdSupervisor = New System.Windows.Forms.TextBox()
        Me.txtQCInspector = New System.Windows.Forms.TextBox()
        Me.txtMaxTrialOp = New System.Windows.Forms.TextBox()
        Me.txtMaxTrialDate = New System.Windows.Forms.TextBox()
        Me.txtMinTrialOp = New System.Windows.Forms.TextBox()
        Me.txtNameEquip = New System.Windows.Forms.TextBox()
        Me.txtNameTypeProd = New System.Windows.Forms.TextBox()
        Me.txtMinTrialDate = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblApprovedBy = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblManager = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblProdSupervisor = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblQCInspector = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.fraTop1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchApprBy
        '
        Me.cmdSearchApprBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchApprBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchApprBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchApprBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchApprBy.Image = CType(resources.GetObject("cmdSearchApprBy.Image"), System.Drawing.Image)
        Me.cmdSearchApprBy.Location = New System.Drawing.Point(254, 228)
        Me.cmdSearchApprBy.Name = "cmdSearchApprBy"
        Me.cmdSearchApprBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchApprBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchApprBy.TabIndex = 51
        Me.cmdSearchApprBy.TabStop = False
        Me.cmdSearchApprBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchApprBy, "Search")
        Me.cmdSearchApprBy.UseVisualStyleBackColor = False
        '
        'cmdSearchMan
        '
        Me.cmdSearchMan.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMan.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMan.Image = CType(resources.GetObject("cmdSearchMan.Image"), System.Drawing.Image)
        Me.cmdSearchMan.Location = New System.Drawing.Point(254, 300)
        Me.cmdSearchMan.Name = "cmdSearchMan"
        Me.cmdSearchMan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMan.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchMan.TabIndex = 40
        Me.cmdSearchMan.TabStop = False
        Me.cmdSearchMan.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMan, "Search")
        Me.cmdSearchMan.UseVisualStyleBackColor = False
        '
        'cmdSearchProdSuper
        '
        Me.cmdSearchProdSuper.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProdSuper.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProdSuper.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProdSuper.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProdSuper.Image = CType(resources.GetObject("cmdSearchProdSuper.Image"), System.Drawing.Image)
        Me.cmdSearchProdSuper.Location = New System.Drawing.Point(254, 276)
        Me.cmdSearchProdSuper.Name = "cmdSearchProdSuper"
        Me.cmdSearchProdSuper.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProdSuper.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchProdSuper.TabIndex = 36
        Me.cmdSearchProdSuper.TabStop = False
        Me.cmdSearchProdSuper.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProdSuper, "Search")
        Me.cmdSearchProdSuper.UseVisualStyleBackColor = False
        '
        'cmdSearchQCInsp
        '
        Me.cmdSearchQCInsp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchQCInsp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchQCInsp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchQCInsp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchQCInsp.Image = CType(resources.GetObject("cmdSearchQCInsp.Image"), System.Drawing.Image)
        Me.cmdSearchQCInsp.Location = New System.Drawing.Point(254, 252)
        Me.cmdSearchQCInsp.Name = "cmdSearchQCInsp"
        Me.cmdSearchQCInsp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchQCInsp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchQCInsp.TabIndex = 32
        Me.cmdSearchQCInsp.TabStop = False
        Me.cmdSearchQCInsp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchQCInsp, "Search")
        Me.cmdSearchQCInsp.UseVisualStyleBackColor = False
        '
        'cmdSearchSlipNo
        '
        Me.cmdSearchSlipNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipNo.Image = CType(resources.GetObject("cmdSearchSlipNo.Image"), System.Drawing.Image)
        Me.cmdSearchSlipNo.Location = New System.Drawing.Point(254, 12)
        Me.cmdSearchSlipNo.Name = "cmdSearchSlipNo"
        Me.cmdSearchSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSlipNo.TabIndex = 15
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
        Me.CmdPreview.Location = New System.Drawing.Point(456, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 10
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(254, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 9
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
        Me.cmdPrint.Location = New System.Drawing.Point(388, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 8
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
        Me.CmdClose.Location = New System.Drawing.Point(590, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 7
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
        Me.CmdView.Location = New System.Drawing.Point(522, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 6
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
        Me.CmdDelete.Location = New System.Drawing.Point(320, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 5
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
        Me.CmdSave.Location = New System.Drawing.Point(186, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 4
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
        Me.CmdModify.Location = New System.Drawing.Point(118, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 3
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
        Me.CmdAdd.Location = New System.Drawing.Point(52, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 2
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtApprovedBy)
        Me.fraTop1.Controls.Add(Me.cmdSearchApprBy)
        Me.fraTop1.Controls.Add(Me.txtNote)
        Me.fraTop1.Controls.Add(Me.txtApprovedDate)
        Me.fraTop1.Controls.Add(Me.txtNameProcess)
        Me.fraTop1.Controls.Add(Me.cboFinalRemarks)
        Me.fraTop1.Controls.Add(Me.cmdSearchMan)
        Me.fraTop1.Controls.Add(Me.txtManager)
        Me.fraTop1.Controls.Add(Me.cmdSearchProdSuper)
        Me.fraTop1.Controls.Add(Me.txtProdSupervisor)
        Me.fraTop1.Controls.Add(Me.cmdSearchQCInsp)
        Me.fraTop1.Controls.Add(Me.txtQCInspector)
        Me.fraTop1.Controls.Add(Me.txtMaxTrialOp)
        Me.fraTop1.Controls.Add(Me.txtMaxTrialDate)
        Me.fraTop1.Controls.Add(Me.txtMinTrialOp)
        Me.fraTop1.Controls.Add(Me.txtNameEquip)
        Me.fraTop1.Controls.Add(Me.txtNameTypeProd)
        Me.fraTop1.Controls.Add(Me.txtMinTrialDate)
        Me.fraTop1.Controls.Add(Me.cmdSearchSlipNo)
        Me.fraTop1.Controls.Add(Me.txtDate)
        Me.fraTop1.Controls.Add(Me.txtSlipNo)
        Me.fraTop1.Controls.Add(Me.SprdMain)
        Me.fraTop1.Controls.Add(Me.lblApprovedBy)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label13)
        Me.fraTop1.Controls.Add(Me.Label18)
        Me.fraTop1.Controls.Add(Me.lblManager)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Controls.Add(Me.lblProdSupervisor)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.lblQCInspector)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(711, 467)
        Me.fraTop1.TabIndex = 12
        Me.fraTop1.TabStop = False
        '
        'txtApprovedBy
        '
        Me.txtApprovedBy.AcceptsReturn = True
        Me.txtApprovedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtApprovedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtApprovedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtApprovedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApprovedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtApprovedBy.Location = New System.Drawing.Point(159, 228)
        Me.txtApprovedBy.MaxLength = 0
        Me.txtApprovedBy.Name = "txtApprovedBy"
        Me.txtApprovedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtApprovedBy.Size = New System.Drawing.Size(93, 20)
        Me.txtApprovedBy.TabIndex = 52
        '
        'txtNote
        '
        Me.txtNote.AcceptsReturn = True
        Me.txtNote.BackColor = System.Drawing.SystemColors.Window
        Me.txtNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNote.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNote.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNote.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNote.Location = New System.Drawing.Point(159, 204)
        Me.txtNote.MaxLength = 0
        Me.txtNote.Name = "txtNote"
        Me.txtNote.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNote.Size = New System.Drawing.Size(543, 20)
        Me.txtNote.TabIndex = 49
        '
        'txtApprovedDate
        '
        Me.txtApprovedDate.AcceptsReturn = True
        Me.txtApprovedDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtApprovedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtApprovedDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtApprovedDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtApprovedDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtApprovedDate.Location = New System.Drawing.Point(608, 180)
        Me.txtApprovedDate.MaxLength = 0
        Me.txtApprovedDate.Name = "txtApprovedDate"
        Me.txtApprovedDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtApprovedDate.Size = New System.Drawing.Size(93, 20)
        Me.txtApprovedDate.TabIndex = 47
        '
        'txtNameProcess
        '
        Me.txtNameProcess.AcceptsReturn = True
        Me.txtNameProcess.BackColor = System.Drawing.SystemColors.Window
        Me.txtNameProcess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNameProcess.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNameProcess.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameProcess.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNameProcess.Location = New System.Drawing.Point(159, 84)
        Me.txtNameProcess.MaxLength = 0
        Me.txtNameProcess.Name = "txtNameProcess"
        Me.txtNameProcess.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNameProcess.Size = New System.Drawing.Size(543, 20)
        Me.txtNameProcess.TabIndex = 45
        '
        'cboFinalRemarks
        '
        Me.cboFinalRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.cboFinalRemarks.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFinalRemarks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFinalRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFinalRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFinalRemarks.Location = New System.Drawing.Point(158, 180)
        Me.cboFinalRemarks.Name = "cboFinalRemarks"
        Me.cboFinalRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFinalRemarks.Size = New System.Drawing.Size(111, 22)
        Me.cboFinalRemarks.TabIndex = 43
        '
        'txtManager
        '
        Me.txtManager.AcceptsReturn = True
        Me.txtManager.BackColor = System.Drawing.SystemColors.Window
        Me.txtManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManager.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtManager.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtManager.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtManager.Location = New System.Drawing.Point(159, 300)
        Me.txtManager.MaxLength = 0
        Me.txtManager.Name = "txtManager"
        Me.txtManager.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtManager.Size = New System.Drawing.Size(93, 20)
        Me.txtManager.TabIndex = 39
        '
        'txtProdSupervisor
        '
        Me.txtProdSupervisor.AcceptsReturn = True
        Me.txtProdSupervisor.BackColor = System.Drawing.SystemColors.Window
        Me.txtProdSupervisor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProdSupervisor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProdSupervisor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProdSupervisor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtProdSupervisor.Location = New System.Drawing.Point(159, 276)
        Me.txtProdSupervisor.MaxLength = 0
        Me.txtProdSupervisor.Name = "txtProdSupervisor"
        Me.txtProdSupervisor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProdSupervisor.Size = New System.Drawing.Size(93, 20)
        Me.txtProdSupervisor.TabIndex = 35
        '
        'txtQCInspector
        '
        Me.txtQCInspector.AcceptsReturn = True
        Me.txtQCInspector.BackColor = System.Drawing.SystemColors.Window
        Me.txtQCInspector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQCInspector.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQCInspector.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQCInspector.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtQCInspector.Location = New System.Drawing.Point(159, 252)
        Me.txtQCInspector.MaxLength = 0
        Me.txtQCInspector.Name = "txtQCInspector"
        Me.txtQCInspector.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQCInspector.Size = New System.Drawing.Size(93, 20)
        Me.txtQCInspector.TabIndex = 31
        '
        'txtMaxTrialOp
        '
        Me.txtMaxTrialOp.AcceptsReturn = True
        Me.txtMaxTrialOp.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxTrialOp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxTrialOp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaxTrialOp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaxTrialOp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMaxTrialOp.Location = New System.Drawing.Point(159, 156)
        Me.txtMaxTrialOp.MaxLength = 0
        Me.txtMaxTrialOp.Name = "txtMaxTrialOp"
        Me.txtMaxTrialOp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaxTrialOp.Size = New System.Drawing.Size(543, 20)
        Me.txtMaxTrialOp.TabIndex = 29
        '
        'txtMaxTrialDate
        '
        Me.txtMaxTrialDate.AcceptsReturn = True
        Me.txtMaxTrialDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaxTrialDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaxTrialDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaxTrialDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaxTrialDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMaxTrialDate.Location = New System.Drawing.Point(608, 108)
        Me.txtMaxTrialDate.MaxLength = 0
        Me.txtMaxTrialDate.Name = "txtMaxTrialDate"
        Me.txtMaxTrialDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaxTrialDate.Size = New System.Drawing.Size(93, 20)
        Me.txtMaxTrialDate.TabIndex = 27
        '
        'txtMinTrialOp
        '
        Me.txtMinTrialOp.AcceptsReturn = True
        Me.txtMinTrialOp.BackColor = System.Drawing.SystemColors.Window
        Me.txtMinTrialOp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMinTrialOp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMinTrialOp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMinTrialOp.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMinTrialOp.Location = New System.Drawing.Point(159, 132)
        Me.txtMinTrialOp.MaxLength = 0
        Me.txtMinTrialOp.Name = "txtMinTrialOp"
        Me.txtMinTrialOp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMinTrialOp.Size = New System.Drawing.Size(543, 20)
        Me.txtMinTrialOp.TabIndex = 25
        '
        'txtNameEquip
        '
        Me.txtNameEquip.AcceptsReturn = True
        Me.txtNameEquip.BackColor = System.Drawing.SystemColors.Window
        Me.txtNameEquip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNameEquip.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNameEquip.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameEquip.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNameEquip.Location = New System.Drawing.Point(159, 60)
        Me.txtNameEquip.MaxLength = 0
        Me.txtNameEquip.Name = "txtNameEquip"
        Me.txtNameEquip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNameEquip.Size = New System.Drawing.Size(543, 20)
        Me.txtNameEquip.TabIndex = 23
        '
        'txtNameTypeProd
        '
        Me.txtNameTypeProd.AcceptsReturn = True
        Me.txtNameTypeProd.BackColor = System.Drawing.SystemColors.Window
        Me.txtNameTypeProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNameTypeProd.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNameTypeProd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNameTypeProd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNameTypeProd.Location = New System.Drawing.Point(159, 36)
        Me.txtNameTypeProd.MaxLength = 0
        Me.txtNameTypeProd.Name = "txtNameTypeProd"
        Me.txtNameTypeProd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNameTypeProd.Size = New System.Drawing.Size(543, 20)
        Me.txtNameTypeProd.TabIndex = 21
        '
        'txtMinTrialDate
        '
        Me.txtMinTrialDate.AcceptsReturn = True
        Me.txtMinTrialDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMinTrialDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMinTrialDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMinTrialDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMinTrialDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMinTrialDate.Location = New System.Drawing.Point(159, 108)
        Me.txtMinTrialDate.MaxLength = 0
        Me.txtMinTrialDate.Name = "txtMinTrialDate"
        Me.txtMinTrialDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMinTrialDate.Size = New System.Drawing.Size(93, 20)
        Me.txtMinTrialDate.TabIndex = 19
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(608, 12)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(93, 20)
        Me.txtDate.TabIndex = 14
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(159, 12)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipNo.TabIndex = 13
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 322)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(707, 143)
        Me.SprdMain.TabIndex = 18
        '
        'lblApprovedBy
        '
        Me.lblApprovedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblApprovedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblApprovedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblApprovedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApprovedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblApprovedBy.Location = New System.Drawing.Point(279, 228)
        Me.lblApprovedBy.Name = "lblApprovedBy"
        Me.lblApprovedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblApprovedBy.Size = New System.Drawing.Size(423, 19)
        Me.lblApprovedBy.TabIndex = 54
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(12, 232)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(74, 13)
        Me.Label10.TabIndex = 53
        Me.Label10.Text = "Approved By"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 208)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Note"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(472, 184)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Approved Date"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(12, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(93, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Name Of Process"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(22, 186)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(78, 13)
        Me.Label13.TabIndex = 44
        Me.Label13.Text = "Final Remarks"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(12, 304)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(52, 13)
        Me.Label18.TabIndex = 42
        Me.Label18.Text = "Manager"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblManager
        '
        Me.lblManager.BackColor = System.Drawing.SystemColors.Control
        Me.lblManager.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblManager.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblManager.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblManager.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblManager.Location = New System.Drawing.Point(279, 300)
        Me.lblManager.Name = "lblManager"
        Me.lblManager.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblManager.Size = New System.Drawing.Size(423, 19)
        Me.lblManager.TabIndex = 41
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(12, 280)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(119, 13)
        Me.Label16.TabIndex = 38
        Me.Label16.Text = "Production Supervisor"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProdSupervisor
        '
        Me.lblProdSupervisor.BackColor = System.Drawing.SystemColors.Control
        Me.lblProdSupervisor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProdSupervisor.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProdSupervisor.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProdSupervisor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProdSupervisor.Location = New System.Drawing.Point(279, 276)
        Me.lblProdSupervisor.Name = "lblProdSupervisor"
        Me.lblProdSupervisor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProdSupervisor.Size = New System.Drawing.Size(423, 19)
        Me.lblProdSupervisor.TabIndex = 37
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(12, 256)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(77, 13)
        Me.Label14.TabIndex = 34
        Me.Label14.Text = "Q.C. Inspector"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblQCInspector
        '
        Me.lblQCInspector.BackColor = System.Drawing.SystemColors.Control
        Me.lblQCInspector.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblQCInspector.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblQCInspector.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCInspector.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblQCInspector.Location = New System.Drawing.Point(279, 252)
        Me.lblQCInspector.Name = "lblQCInspector"
        Me.lblQCInspector.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblQCInspector.Size = New System.Drawing.Size(423, 19)
        Me.lblQCInspector.TabIndex = 33
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(12, 160)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(131, 13)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "Maximum Trial Operator"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(472, 112)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(109, 13)
        Me.Label11.TabIndex = 28
        Me.Label11.Text = "Maximum Trial Date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(12, 136)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(128, 13)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Minimum Trial Operator"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(110, 13)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "Name Of Equipment"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(12, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(132, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Name && Type Of Product"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(12, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(106, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Minimum Trial Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(472, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(12, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 16
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
        Me.Report1.TabIndex = 14
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(710, 461)
        Me.SprdView.TabIndex = 0
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
        Me.FraMovement.Size = New System.Drawing.Size(711, 51)
        Me.FraMovement.TabIndex = 1
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
        Me.lblMkey.TabIndex = 11
        Me.lblMkey.Text = "lblMkey"
        '
        'frmProcQualRecord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(711, 509)
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
        Me.Name = "frmProcQualRecord"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Process Qualification Record"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
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