Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmQualityFlash
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
    Public WithEvents txtReplyDate As System.Windows.Forms.TextBox
    Public WithEvents cboReworkBy As System.Windows.Forms.ComboBox
    Public WithEvents chkReplyRecv As System.Windows.Forms.CheckBox
    Public WithEvents chkSatisfy As System.Windows.Forms.CheckBox
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents chkRework As System.Windows.Forms.CheckBox
    Public WithEvents chkCall As System.Windows.Forms.CheckBox
    Public WithEvents chkRejected As System.Windows.Forms.CheckBox
    Public WithEvents chkSegregation As System.Windows.Forms.CheckBox
    Public WithEvents chkDeviation As System.Windows.Forms.CheckBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSlipNo As System.Windows.Forms.Button
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchMRRNo As System.Windows.Forms.Button
    Public WithEvents txtItemCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchItemCode As System.Windows.Forms.Button
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents txtRejectedQty As System.Windows.Forms.TextBox
    Public WithEvents txtAcceptedQty As System.Windows.Forms.TextBox
    Public WithEvents txtReceivedQty As System.Windows.Forms.TextBox
    Public WithEvents lblHeatNo As System.Windows.Forms.Label
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblMRRDate As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lblItemCode As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblSupplier As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblBillNo As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblBillDate As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
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
    Public WithEvents cboCause As System.Windows.Forms.ComboBox
    Public WithEvents cboUnderstood As System.Windows.Forms.ComboBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtCorrMeasure As System.Windows.Forms.TextBox
    Public WithEvents cboInspection As System.Windows.Forms.ComboBox
    Public WithEvents cboAdded As System.Windows.Forms.ComboBox
    Public WithEvents txtCorrAction As System.Windows.Forms.TextBox
    Public WithEvents txtMeasure As System.Windows.Forms.TextBox
    Public WithEvents txtMachine As System.Windows.Forms.TextBox
    Public WithEvents txtMaterial As System.Windows.Forms.TextBox
    Public WithEvents txtMethod As System.Windows.Forms.TextBox
    Public WithEvents txtMan As System.Windows.Forms.TextBox
    Public WithEvents txtDescription As System.Windows.Forms.TextBox
    Public WithEvents Label21 As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents fraReplySlip As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmQualityFlash))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchSlipNo = New System.Windows.Forms.Button()
        Me.CmdSearchMRRNo = New System.Windows.Forms.Button()
        Me.cmdSearchItemCode = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtReplyDate = New System.Windows.Forms.TextBox()
        Me.cboReworkBy = New System.Windows.Forms.ComboBox()
        Me.chkReplyRecv = New System.Windows.Forms.CheckBox()
        Me.chkSatisfy = New System.Windows.Forms.CheckBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkRework = New System.Windows.Forms.CheckBox()
        Me.chkCall = New System.Windows.Forms.CheckBox()
        Me.chkRejected = New System.Windows.Forms.CheckBox()
        Me.chkSegregation = New System.Windows.Forms.CheckBox()
        Me.chkDeviation = New System.Windows.Forms.CheckBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtMRRNo = New System.Windows.Forms.TextBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.txtRejectedQty = New System.Windows.Forms.TextBox()
        Me.txtAcceptedQty = New System.Windows.Forms.TextBox()
        Me.txtReceivedQty = New System.Windows.Forms.TextBox()
        Me.lblHeatNo = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblMRRDate = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblItemCode = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblSupplier = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblBillNo = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblBillDate = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.fraReplySlip = New System.Windows.Forms.GroupBox()
        Me.cboCause = New System.Windows.Forms.ComboBox()
        Me.cboUnderstood = New System.Windows.Forms.ComboBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtCorrMeasure = New System.Windows.Forms.TextBox()
        Me.cboInspection = New System.Windows.Forms.ComboBox()
        Me.cboAdded = New System.Windows.Forms.ComboBox()
        Me.txtCorrAction = New System.Windows.Forms.TextBox()
        Me.txtMeasure = New System.Windows.Forms.TextBox()
        Me.txtMachine = New System.Windows.Forms.TextBox()
        Me.txtMaterial = New System.Windows.Forms.TextBox()
        Me.txtMethod = New System.Windows.Forms.TextBox()
        Me.txtMan = New System.Windows.Forms.TextBox()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.fraReplySlip.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchSlipNo
        '
        Me.cmdSearchSlipNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipNo.Image = CType(resources.GetObject("cmdSearchSlipNo.Image"), System.Drawing.Image)
        Me.cmdSearchSlipNo.Location = New System.Drawing.Point(169, 10)
        Me.cmdSearchSlipNo.Name = "cmdSearchSlipNo"
        Me.cmdSearchSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSlipNo.TabIndex = 21
        Me.cmdSearchSlipNo.TabStop = False
        Me.cmdSearchSlipNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSlipNo, "Search")
        Me.cmdSearchSlipNo.UseVisualStyleBackColor = False
        '
        'CmdSearchMRRNo
        '
        Me.CmdSearchMRRNo.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchMRRNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchMRRNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchMRRNo.Image = CType(resources.GetObject("CmdSearchMRRNo.Image"), System.Drawing.Image)
        Me.CmdSearchMRRNo.Location = New System.Drawing.Point(169, 32)
        Me.CmdSearchMRRNo.Name = "CmdSearchMRRNo"
        Me.CmdSearchMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchMRRNo.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchMRRNo.TabIndex = 19
        Me.CmdSearchMRRNo.TabStop = False
        Me.CmdSearchMRRNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchMRRNo, "Search")
        Me.CmdSearchMRRNo.UseVisualStyleBackColor = False
        '
        'cmdSearchItemCode
        '
        Me.cmdSearchItemCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItemCode.Image = CType(resources.GetObject("cmdSearchItemCode.Image"), System.Drawing.Image)
        Me.cmdSearchItemCode.Location = New System.Drawing.Point(169, 54)
        Me.cmdSearchItemCode.Name = "cmdSearchItemCode"
        Me.cmdSearchItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItemCode.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchItemCode.TabIndex = 17
        Me.cmdSearchItemCode.TabStop = False
        Me.cmdSearchItemCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItemCode, "Search")
        Me.cmdSearchItemCode.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.CmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.CmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        Me.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
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
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.Frame3)
        Me.Frame2.Controls.Add(Me.SprdMain)
        Me.Frame2.Controls.Add(Me.Frame1)
        Me.Frame2.Controls.Add(Me.txtSlipNo)
        Me.Frame2.Controls.Add(Me.txtDate)
        Me.Frame2.Controls.Add(Me.cmdSearchSlipNo)
        Me.Frame2.Controls.Add(Me.txtMRRNo)
        Me.Frame2.Controls.Add(Me.CmdSearchMRRNo)
        Me.Frame2.Controls.Add(Me.txtItemCode)
        Me.Frame2.Controls.Add(Me.cmdSearchItemCode)
        Me.Frame2.Controls.Add(Me.cboStatus)
        Me.Frame2.Controls.Add(Me.txtRejectedQty)
        Me.Frame2.Controls.Add(Me.txtAcceptedQty)
        Me.Frame2.Controls.Add(Me.txtReceivedQty)
        Me.Frame2.Controls.Add(Me.lblHeatNo)
        Me.Frame2.Controls.Add(Me.Label27)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.lblMRRDate)
        Me.Frame2.Controls.Add(Me.Label8)
        Me.Frame2.Controls.Add(Me.lblItemCode)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.lblSupplier)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Controls.Add(Me.lblBillNo)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.lblBillDate)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.Label15)
        Me.Frame2.Controls.Add(Me.Label29)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Controls.Add(Me.Label24)
        Me.Frame2.Controls.Add(Me.Label23)
        Me.Frame2.Controls.Add(Me.Label22)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(711, 411)
        Me.Frame2.TabIndex = 12
        Me.Frame2.TabStop = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtReplyDate)
        Me.Frame3.Controls.Add(Me.cboReworkBy)
        Me.Frame3.Controls.Add(Me.chkReplyRecv)
        Me.Frame3.Controls.Add(Me.chkSatisfy)
        Me.Frame3.Controls.Add(Me.Label26)
        Me.Frame3.Controls.Add(Me.Label25)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(2, 348)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(707, 61)
        Me.Frame3.TabIndex = 75
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Reply Status"
        '
        'txtReplyDate
        '
        Me.txtReplyDate.AcceptsReturn = True
        Me.txtReplyDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtReplyDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReplyDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReplyDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReplyDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReplyDate.Location = New System.Drawing.Point(301, 14)
        Me.txtReplyDate.MaxLength = 0
        Me.txtReplyDate.Name = "txtReplyDate"
        Me.txtReplyDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReplyDate.Size = New System.Drawing.Size(93, 20)
        Me.txtReplyDate.TabIndex = 80
        '
        'cboReworkBy
        '
        Me.cboReworkBy.BackColor = System.Drawing.SystemColors.Window
        Me.cboReworkBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboReworkBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReworkBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReworkBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboReworkBy.Location = New System.Drawing.Point(544, 14)
        Me.cboReworkBy.Name = "cboReworkBy"
        Me.cboReworkBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboReworkBy.Size = New System.Drawing.Size(157, 22)
        Me.cboReworkBy.TabIndex = 78
        '
        'chkReplyRecv
        '
        Me.chkReplyRecv.AutoSize = True
        Me.chkReplyRecv.BackColor = System.Drawing.SystemColors.Control
        Me.chkReplyRecv.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkReplyRecv.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReplyRecv.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReplyRecv.Location = New System.Drawing.Point(38, 20)
        Me.chkReplyRecv.Name = "chkReplyRecv"
        Me.chkReplyRecv.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkReplyRecv.Size = New System.Drawing.Size(104, 17)
        Me.chkReplyRecv.TabIndex = 77
        Me.chkReplyRecv.Text = "Reply Received"
        Me.chkReplyRecv.UseVisualStyleBackColor = False
        '
        'chkSatisfy
        '
        Me.chkSatisfy.AutoSize = True
        Me.chkSatisfy.BackColor = System.Drawing.SystemColors.Control
        Me.chkSatisfy.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSatisfy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSatisfy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSatisfy.Location = New System.Drawing.Point(38, 42)
        Me.chkSatisfy.Name = "chkSatisfy"
        Me.chkSatisfy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSatisfy.Size = New System.Drawing.Size(123, 17)
        Me.chkSatisfy.TabIndex = 76
        Me.chkSatisfy.Text = "Satisfied with reply"
        Me.chkSatisfy.UseVisualStyleBackColor = False
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(232, 18)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(63, 13)
        Me.Label26.TabIndex = 81
        Me.Label26.Text = "Reply Date"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.SystemColors.Control
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(476, 20)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(61, 13)
        Me.Label25.TabIndex = 79
        Me.Label25.Text = "Rework By"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 150)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(707, 195)
        Me.SprdMain.TabIndex = 74
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkRework)
        Me.Frame1.Controls.Add(Me.chkCall)
        Me.Frame1.Controls.Add(Me.chkRejected)
        Me.Frame1.Controls.Add(Me.chkSegregation)
        Me.Frame1.Controls.Add(Me.chkDeviation)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(464, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(247, 97)
        Me.Frame1.TabIndex = 41
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Action On Defective Lot"
        '
        'chkRework
        '
        Me.chkRework.AutoSize = True
        Me.chkRework.BackColor = System.Drawing.SystemColors.Control
        Me.chkRework.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRework.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRework.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRework.Location = New System.Drawing.Point(6, 64)
        Me.chkRework.Name = "chkRework"
        Me.chkRework.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRework.Size = New System.Drawing.Size(64, 17)
        Me.chkRework.TabIndex = 45
        Me.chkRework.Text = "Rework"
        Me.chkRework.UseVisualStyleBackColor = False
        '
        'chkCall
        '
        Me.chkCall.AutoSize = True
        Me.chkCall.BackColor = System.Drawing.SystemColors.Control
        Me.chkCall.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCall.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCall.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCall.Location = New System.Drawing.Point(6, 80)
        Me.chkCall.Name = "chkCall"
        Me.chkCall.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCall.Size = New System.Drawing.Size(108, 17)
        Me.chkCall.TabIndex = 46
        Me.chkCall.Text = "Please call on us"
        Me.chkCall.UseVisualStyleBackColor = False
        '
        'chkRejected
        '
        Me.chkRejected.AutoSize = True
        Me.chkRejected.BackColor = System.Drawing.SystemColors.Control
        Me.chkRejected.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRejected.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRejected.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRejected.Location = New System.Drawing.Point(6, 48)
        Me.chkRejected.Name = "chkRejected"
        Me.chkRejected.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRejected.Size = New System.Drawing.Size(197, 17)
        Me.chkRejected.TabIndex = 44
        Me.chkRejected.Text = "Rejected awaiting supplier's reply"
        Me.chkRejected.UseVisualStyleBackColor = False
        '
        'chkSegregation
        '
        Me.chkSegregation.AutoSize = True
        Me.chkSegregation.BackColor = System.Drawing.SystemColors.Control
        Me.chkSegregation.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSegregation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSegregation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSegregation.Location = New System.Drawing.Point(6, 32)
        Me.chkSegregation.Name = "chkSegregation"
        Me.chkSegregation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSegregation.Size = New System.Drawing.Size(88, 17)
        Me.chkSegregation.TabIndex = 43
        Me.chkSegregation.Text = "Segregation"
        Me.chkSegregation.UseVisualStyleBackColor = False
        '
        'chkDeviation
        '
        Me.chkDeviation.AutoSize = True
        Me.chkDeviation.BackColor = System.Drawing.SystemColors.Control
        Me.chkDeviation.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDeviation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDeviation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDeviation.Location = New System.Drawing.Point(6, 16)
        Me.chkDeviation.Name = "chkDeviation"
        Me.chkDeviation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDeviation.Size = New System.Drawing.Size(156, 17)
        Me.chkDeviation.TabIndex = 42
        Me.chkDeviation.Text = "Accepted under deviation"
        Me.chkDeviation.UseVisualStyleBackColor = False
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(74, 10)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipNo.TabIndex = 23
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(368, 10)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(93, 20)
        Me.txtDate.TabIndex = 22
        '
        'txtMRRNo
        '
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMRRNo.Location = New System.Drawing.Point(74, 32)
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.Size = New System.Drawing.Size(93, 20)
        Me.txtMRRNo.TabIndex = 20
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtItemCode.Location = New System.Drawing.Point(74, 54)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(93, 20)
        Me.txtItemCode.TabIndex = 18
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(74, 126)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(147, 22)
        Me.cboStatus.TabIndex = 16
        '
        'txtRejectedQty
        '
        Me.txtRejectedQty.AcceptsReturn = True
        Me.txtRejectedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtRejectedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRejectedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRejectedQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRejectedQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRejectedQty.Location = New System.Drawing.Point(641, 128)
        Me.txtRejectedQty.MaxLength = 0
        Me.txtRejectedQty.Name = "txtRejectedQty"
        Me.txtRejectedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRejectedQty.Size = New System.Drawing.Size(67, 20)
        Me.txtRejectedQty.TabIndex = 15
        '
        'txtAcceptedQty
        '
        Me.txtAcceptedQty.AcceptsReturn = True
        Me.txtAcceptedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtAcceptedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcceptedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAcceptedQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcceptedQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAcceptedQty.Location = New System.Drawing.Point(481, 128)
        Me.txtAcceptedQty.MaxLength = 0
        Me.txtAcceptedQty.Name = "txtAcceptedQty"
        Me.txtAcceptedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAcceptedQty.Size = New System.Drawing.Size(67, 20)
        Me.txtAcceptedQty.TabIndex = 14
        '
        'txtReceivedQty
        '
        Me.txtReceivedQty.AcceptsReturn = True
        Me.txtReceivedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtReceivedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReceivedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReceivedQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceivedQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReceivedQty.Location = New System.Drawing.Point(314, 128)
        Me.txtReceivedQty.MaxLength = 0
        Me.txtReceivedQty.Name = "txtReceivedQty"
        Me.txtReceivedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReceivedQty.Size = New System.Drawing.Size(67, 20)
        Me.txtReceivedQty.TabIndex = 13
        '
        'lblHeatNo
        '
        Me.lblHeatNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblHeatNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHeatNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblHeatNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeatNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblHeatNo.Location = New System.Drawing.Point(613, 102)
        Me.lblHeatNo.Name = "lblHeatNo"
        Me.lblHeatNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblHeatNo.Size = New System.Drawing.Size(93, 19)
        Me.lblHeatNo.TabIndex = 83
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(560, 106)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(49, 13)
        Me.Label27.TabIndex = 82
        Me.Label27.Text = "Heat No"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(279, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(58, 13)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "MRR Date"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMRRDate
        '
        Me.lblMRRDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblMRRDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMRRDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMRRDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMRRDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMRRDate.Location = New System.Drawing.Point(368, 32)
        Me.lblMRRDate.Name = "lblMRRDate"
        Me.lblMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMRRDate.Size = New System.Drawing.Size(93, 19)
        Me.lblMRRDate.TabIndex = 39
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(279, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 38
        Me.Label8.Text = "Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblItemCode
        '
        Me.lblItemCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemCode.Location = New System.Drawing.Point(194, 54)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemCode.Size = New System.Drawing.Size(267, 19)
        Me.lblItemCode.TabIndex = 37
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(6, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Item Code"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSupplier
        '
        Me.lblSupplier.BackColor = System.Drawing.SystemColors.Control
        Me.lblSupplier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSupplier.Location = New System.Drawing.Point(74, 78)
        Me.lblSupplier.Name = "lblSupplier"
        Me.lblSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSupplier.Size = New System.Drawing.Size(387, 19)
        Me.lblSupplier.TabIndex = 35
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 106)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Bill No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBillNo
        '
        Me.lblBillNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBillNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillNo.Location = New System.Drawing.Point(74, 102)
        Me.lblBillNo.Name = "lblBillNo"
        Me.lblBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillNo.Size = New System.Drawing.Size(93, 19)
        Me.lblBillNo.TabIndex = 33
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(279, 106)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Bill Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBillDate
        '
        Me.lblBillDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBillDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillDate.Location = New System.Drawing.Point(368, 102)
        Me.lblBillDate.Name = "lblBillDate"
        Me.lblBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillDate.Size = New System.Drawing.Size(93, 19)
        Me.lblBillDate.TabIndex = 31
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "Status"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(6, 82)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(49, 13)
        Me.Label15.TabIndex = 29
        Me.Label15.Text = "Supplier"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(6, 36)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(49, 13)
        Me.Label29.TabIndex = 28
        Me.Label29.Text = "MRR No"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(6, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Number"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(553, 132)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(72, 13)
        Me.Label24.TabIndex = 26
        Me.Label24.Text = "Rejected Qty"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(393, 132)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(75, 13)
        Me.Label23.TabIndex = 25
        Me.Label23.Text = "Accepted Qty"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(227, 132)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(74, 13)
        Me.Label22.TabIndex = 24
        Me.Label22.Text = "Received Qty"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.SprdView.Size = New System.Drawing.Size(710, 372)
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 406)
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
        'fraReplySlip
        '
        Me.fraReplySlip.BackColor = System.Drawing.SystemColors.Control
        Me.fraReplySlip.Controls.Add(Me.cboCause)
        Me.fraReplySlip.Controls.Add(Me.cboUnderstood)
        Me.fraReplySlip.Controls.Add(Me.txtRemarks)
        Me.fraReplySlip.Controls.Add(Me.txtCorrMeasure)
        Me.fraReplySlip.Controls.Add(Me.cboInspection)
        Me.fraReplySlip.Controls.Add(Me.cboAdded)
        Me.fraReplySlip.Controls.Add(Me.txtCorrAction)
        Me.fraReplySlip.Controls.Add(Me.txtMeasure)
        Me.fraReplySlip.Controls.Add(Me.txtMachine)
        Me.fraReplySlip.Controls.Add(Me.txtMaterial)
        Me.fraReplySlip.Controls.Add(Me.txtMethod)
        Me.fraReplySlip.Controls.Add(Me.txtMan)
        Me.fraReplySlip.Controls.Add(Me.txtDescription)
        Me.fraReplySlip.Controls.Add(Me.Label21)
        Me.fraReplySlip.Controls.Add(Me.Label20)
        Me.fraReplySlip.Controls.Add(Me.Label19)
        Me.fraReplySlip.Controls.Add(Me.Label18)
        Me.fraReplySlip.Controls.Add(Me.Label17)
        Me.fraReplySlip.Controls.Add(Me.Label16)
        Me.fraReplySlip.Controls.Add(Me.Label14)
        Me.fraReplySlip.Controls.Add(Me.Label12)
        Me.fraReplySlip.Controls.Add(Me.Label11)
        Me.fraReplySlip.Controls.Add(Me.Label10)
        Me.fraReplySlip.Controls.Add(Me.Label9)
        Me.fraReplySlip.Controls.Add(Me.Label5)
        Me.fraReplySlip.Controls.Add(Me.Label13)
        Me.fraReplySlip.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraReplySlip.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraReplySlip.Location = New System.Drawing.Point(2, 150)
        Me.fraReplySlip.Name = "fraReplySlip"
        Me.fraReplySlip.Padding = New System.Windows.Forms.Padding(0)
        Me.fraReplySlip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraReplySlip.Size = New System.Drawing.Size(707, 193)
        Me.fraReplySlip.TabIndex = 47
        Me.fraReplySlip.TabStop = False
        Me.fraReplySlip.Text = "Reply Slip"
        Me.fraReplySlip.Visible = False
        '
        'cboCause
        '
        Me.cboCause.BackColor = System.Drawing.SystemColors.Window
        Me.cboCause.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCause.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCause.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCause.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCause.Location = New System.Drawing.Point(484, 44)
        Me.cboCause.Name = "cboCause"
        Me.cboCause.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCause.Size = New System.Drawing.Size(209, 22)
        Me.cboCause.TabIndex = 60
        '
        'cboUnderstood
        '
        Me.cboUnderstood.BackColor = System.Drawing.SystemColors.Window
        Me.cboUnderstood.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboUnderstood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnderstood.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboUnderstood.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboUnderstood.Location = New System.Drawing.Point(127, 44)
        Me.cboUnderstood.Name = "cboUnderstood"
        Me.cboUnderstood.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboUnderstood.Size = New System.Drawing.Size(209, 22)
        Me.cboUnderstood.TabIndex = 59
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(484, 168)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(209, 20)
        Me.txtRemarks.TabIndex = 58
        '
        'txtCorrMeasure
        '
        Me.txtCorrMeasure.AcceptsReturn = True
        Me.txtCorrMeasure.BackColor = System.Drawing.SystemColors.Window
        Me.txtCorrMeasure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCorrMeasure.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCorrMeasure.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCorrMeasure.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCorrMeasure.Location = New System.Drawing.Point(127, 168)
        Me.txtCorrMeasure.MaxLength = 0
        Me.txtCorrMeasure.Name = "txtCorrMeasure"
        Me.txtCorrMeasure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCorrMeasure.Size = New System.Drawing.Size(209, 20)
        Me.txtCorrMeasure.TabIndex = 57
        '
        'cboInspection
        '
        Me.cboInspection.BackColor = System.Drawing.SystemColors.Window
        Me.cboInspection.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInspection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInspection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInspection.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboInspection.Location = New System.Drawing.Point(484, 142)
        Me.cboInspection.Name = "cboInspection"
        Me.cboInspection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInspection.Size = New System.Drawing.Size(209, 22)
        Me.cboInspection.TabIndex = 56
        '
        'cboAdded
        '
        Me.cboAdded.BackColor = System.Drawing.SystemColors.Window
        Me.cboAdded.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAdded.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAdded.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAdded.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAdded.Location = New System.Drawing.Point(127, 142)
        Me.cboAdded.Name = "cboAdded"
        Me.cboAdded.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAdded.Size = New System.Drawing.Size(209, 22)
        Me.cboAdded.TabIndex = 55
        '
        'txtCorrAction
        '
        Me.txtCorrAction.AcceptsReturn = True
        Me.txtCorrAction.BackColor = System.Drawing.SystemColors.Window
        Me.txtCorrAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCorrAction.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCorrAction.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCorrAction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCorrAction.Location = New System.Drawing.Point(484, 118)
        Me.txtCorrAction.MaxLength = 0
        Me.txtCorrAction.Name = "txtCorrAction"
        Me.txtCorrAction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCorrAction.Size = New System.Drawing.Size(209, 20)
        Me.txtCorrAction.TabIndex = 54
        '
        'txtMeasure
        '
        Me.txtMeasure.AcceptsReturn = True
        Me.txtMeasure.BackColor = System.Drawing.SystemColors.Window
        Me.txtMeasure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMeasure.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMeasure.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMeasure.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMeasure.Location = New System.Drawing.Point(127, 118)
        Me.txtMeasure.MaxLength = 0
        Me.txtMeasure.Name = "txtMeasure"
        Me.txtMeasure.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMeasure.Size = New System.Drawing.Size(209, 20)
        Me.txtMeasure.TabIndex = 53
        '
        'txtMachine
        '
        Me.txtMachine.AcceptsReturn = True
        Me.txtMachine.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachine.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachine.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachine.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMachine.Location = New System.Drawing.Point(484, 94)
        Me.txtMachine.MaxLength = 0
        Me.txtMachine.Name = "txtMachine"
        Me.txtMachine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachine.Size = New System.Drawing.Size(209, 20)
        Me.txtMachine.TabIndex = 52
        '
        'txtMaterial
        '
        Me.txtMaterial.AcceptsReturn = True
        Me.txtMaterial.BackColor = System.Drawing.SystemColors.Window
        Me.txtMaterial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMaterial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMaterial.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMaterial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMaterial.Location = New System.Drawing.Point(127, 94)
        Me.txtMaterial.MaxLength = 0
        Me.txtMaterial.Name = "txtMaterial"
        Me.txtMaterial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMaterial.Size = New System.Drawing.Size(209, 20)
        Me.txtMaterial.TabIndex = 51
        '
        'txtMethod
        '
        Me.txtMethod.AcceptsReturn = True
        Me.txtMethod.BackColor = System.Drawing.SystemColors.Window
        Me.txtMethod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMethod.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMethod.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMethod.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMethod.Location = New System.Drawing.Point(484, 70)
        Me.txtMethod.MaxLength = 0
        Me.txtMethod.Name = "txtMethod"
        Me.txtMethod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMethod.Size = New System.Drawing.Size(209, 20)
        Me.txtMethod.TabIndex = 50
        '
        'txtMan
        '
        Me.txtMan.AcceptsReturn = True
        Me.txtMan.BackColor = System.Drawing.SystemColors.Window
        Me.txtMan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMan.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMan.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMan.Location = New System.Drawing.Point(127, 70)
        Me.txtMan.MaxLength = 0
        Me.txtMan.Name = "txtMan"
        Me.txtMan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMan.Size = New System.Drawing.Size(209, 20)
        Me.txtMan.TabIndex = 49
        '
        'txtDescription
        '
        Me.txtDescription.AcceptsReturn = True
        Me.txtDescription.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDescription.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescription.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDescription.Location = New System.Drawing.Point(127, 20)
        Me.txtDescription.MaxLength = 0
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescription.Size = New System.Drawing.Size(567, 20)
        Me.txtDescription.TabIndex = 48
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.SystemColors.Control
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(336, 174)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(51, 13)
        Me.Label21.TabIndex = 73
        Me.Label21.Text = "Remarks"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(8, 174)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(105, 13)
        Me.Label20.TabIndex = 72
        Me.Label20.Text = "Corrective Measure"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(336, 148)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(97, 13)
        Me.Label19.TabIndex = 71
        Me.Label19.Text = "Inspection Report"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(8, 148)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(41, 13)
        Me.Label18.TabIndex = 70
        Me.Label18.Text = "Added"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(336, 124)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(94, 13)
        Me.Label17.TabIndex = 69
        Me.Label17.Text = "Corrective Action"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(8, 124)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(50, 13)
        Me.Label16.TabIndex = 68
        Me.Label16.Text = "Measure"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(336, 100)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(49, 13)
        Me.Label14.TabIndex = 67
        Me.Label14.Text = "Machine"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(8, 100)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(49, 13)
        Me.Label12.TabIndex = 66
        Me.Label12.Text = "Material"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(336, 76)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(47, 13)
        Me.Label11.TabIndex = 65
        Me.Label11.Text = "Method"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(8, 76)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(29, 13)
        Me.Label10.TabIndex = 64
        Me.Label10.Text = "Man"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(336, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(95, 13)
        Me.Label9.TabIndex = 63
        Me.Label9.Text = "Cause Indentified"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 62
        Me.Label5.Text = "Understood"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(8, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(65, 13)
        Me.Label13.TabIndex = 61
        Me.Label13.Text = "Description"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmQualityFlash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(711, 458)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.fraReplySlip)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmQualityFlash"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Quality Flash Report"
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.fraReplySlip.ResumeLayout(False)
        Me.fraReplySlip.PerformLayout()
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