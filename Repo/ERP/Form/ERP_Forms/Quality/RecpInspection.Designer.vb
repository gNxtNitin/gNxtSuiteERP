Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmRecpInspection
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
    Public WithEvents cboDisposition As System.Windows.Forms.ComboBox
    Public WithEvents chkPDIR As System.Windows.Forms.CheckBox
    Public WithEvents txtRework As System.Windows.Forms.TextBox
    Public WithEvents txtSegregated As System.Windows.Forms.TextBox
    Public WithEvents txtUnderDev As System.Windows.Forms.TextBox
    Public WithEvents txtAuthorisedBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchAuthorised As System.Windows.Forms.Button
    Public WithEvents cmdSearchInspected As System.Windows.Forms.Button
    Public WithEvents txtInspectedBy As System.Windows.Forms.TextBox
    Public WithEvents txtPartNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchPartNo As System.Windows.Forms.Button
    Public WithEvents txtSource As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSource As System.Windows.Forms.Button
    Public WithEvents txtProject As System.Windows.Forms.TextBox
    Public WithEvents txtReceivedQty As System.Windows.Forms.TextBox
    Public WithEvents txtAcceptedQty As System.Windows.Forms.TextBox
    Public WithEvents txtRejectedQty As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchMRRNo As System.Windows.Forms.Button
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSlipNo As System.Windows.Forms.Button
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblQCStatus As System.Windows.Forms.Label
    Public WithEvents lblApprovedQty As System.Windows.Forms.Label
    Public WithEvents lblAuto_Key_Std As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblAuthorisedBy As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblInspectedBy As System.Windows.Forms.Label
    Public WithEvents lblPartNo As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblSource As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label22 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents lblBillDate As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblBillNo As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRecpInspection))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchAuthorised = New System.Windows.Forms.Button()
        Me.cmdSearchInspected = New System.Windows.Forms.Button()
        Me.cmdSearchPartNo = New System.Windows.Forms.Button()
        Me.cmdSearchSource = New System.Windows.Forms.Button()
        Me.CmdSearchMRRNo = New System.Windows.Forms.Button()
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
        Me.cboDisposition = New System.Windows.Forms.ComboBox()
        Me.chkPDIR = New System.Windows.Forms.CheckBox()
        Me.txtRework = New System.Windows.Forms.TextBox()
        Me.txtSegregated = New System.Windows.Forms.TextBox()
        Me.txtUnderDev = New System.Windows.Forms.TextBox()
        Me.txtAuthorisedBy = New System.Windows.Forms.TextBox()
        Me.txtInspectedBy = New System.Windows.Forms.TextBox()
        Me.txtPartNo = New System.Windows.Forms.TextBox()
        Me.txtSource = New System.Windows.Forms.TextBox()
        Me.txtProject = New System.Windows.Forms.TextBox()
        Me.txtReceivedQty = New System.Windows.Forms.TextBox()
        Me.txtAcceptedQty = New System.Windows.Forms.TextBox()
        Me.txtRejectedQty = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtMRRNo = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblQCStatus = New System.Windows.Forms.Label()
        Me.lblApprovedQty = New System.Windows.Forms.Label()
        Me.lblAuto_Key_Std = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblAuthorisedBy = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblInspectedBy = New System.Windows.Forms.Label()
        Me.lblPartNo = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblSource = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblBillDate = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblBillNo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
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
        'cmdSearchAuthorised
        '
        Me.cmdSearchAuthorised.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAuthorised.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAuthorised.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAuthorised.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAuthorised.Image = CType(resources.GetObject("cmdSearchAuthorised.Image"), System.Drawing.Image)
        Me.cmdSearchAuthorised.Location = New System.Drawing.Point(180, 172)
        Me.cmdSearchAuthorised.Name = "cmdSearchAuthorised"
        Me.cmdSearchAuthorised.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAuthorised.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchAuthorised.TabIndex = 52
        Me.cmdSearchAuthorised.TabStop = False
        Me.cmdSearchAuthorised.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAuthorised, "Search")
        Me.cmdSearchAuthorised.UseVisualStyleBackColor = False
        '
        'cmdSearchInspected
        '
        Me.cmdSearchInspected.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchInspected.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchInspected.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchInspected.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchInspected.Image = CType(resources.GetObject("cmdSearchInspected.Image"), System.Drawing.Image)
        Me.cmdSearchInspected.Location = New System.Drawing.Point(180, 150)
        Me.cmdSearchInspected.Name = "cmdSearchInspected"
        Me.cmdSearchInspected.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchInspected.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchInspected.TabIndex = 51
        Me.cmdSearchInspected.TabStop = False
        Me.cmdSearchInspected.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchInspected, "Search")
        Me.cmdSearchInspected.UseVisualStyleBackColor = False
        '
        'cmdSearchPartNo
        '
        Me.cmdSearchPartNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPartNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPartNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPartNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPartNo.Image = CType(resources.GetObject("cmdSearchPartNo.Image"), System.Drawing.Image)
        Me.cmdSearchPartNo.Location = New System.Drawing.Point(180, 106)
        Me.cmdSearchPartNo.Name = "cmdSearchPartNo"
        Me.cmdSearchPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPartNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchPartNo.TabIndex = 48
        Me.cmdSearchPartNo.TabStop = False
        Me.cmdSearchPartNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPartNo, "Search")
        Me.cmdSearchPartNo.UseVisualStyleBackColor = False
        '
        'cmdSearchSource
        '
        Me.cmdSearchSource.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSource.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSource.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSource.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSource.Image = CType(resources.GetObject("cmdSearchSource.Image"), System.Drawing.Image)
        Me.cmdSearchSource.Location = New System.Drawing.Point(180, 62)
        Me.cmdSearchSource.Name = "cmdSearchSource"
        Me.cmdSearchSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSource.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSource.TabIndex = 45
        Me.cmdSearchSource.TabStop = False
        Me.cmdSearchSource.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSource, "Search")
        Me.cmdSearchSource.UseVisualStyleBackColor = False
        '
        'CmdSearchMRRNo
        '
        Me.CmdSearchMRRNo.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchMRRNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchMRRNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchMRRNo.Image = CType(resources.GetObject("CmdSearchMRRNo.Image"), System.Drawing.Image)
        Me.CmdSearchMRRNo.Location = New System.Drawing.Point(180, 84)
        Me.CmdSearchMRRNo.Name = "CmdSearchMRRNo"
        Me.CmdSearchMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchMRRNo.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchMRRNo.TabIndex = 33
        Me.CmdSearchMRRNo.TabStop = False
        Me.CmdSearchMRRNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchMRRNo, "Search")
        Me.CmdSearchMRRNo.UseVisualStyleBackColor = False
        '
        'cmdSearchSlipNo
        '
        Me.cmdSearchSlipNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipNo.Image = CType(resources.GetObject("cmdSearchSlipNo.Image"), System.Drawing.Image)
        Me.cmdSearchSlipNo.Location = New System.Drawing.Point(180, 18)
        Me.cmdSearchSlipNo.Name = "cmdSearchSlipNo"
        Me.cmdSearchSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSlipNo.TabIndex = 31
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
        Me.CmdPreview.TabIndex = 24
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
        Me.cmdSavePrint.TabIndex = 21
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
        Me.cmdPrint.TabIndex = 23
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
        Me.CmdClose.TabIndex = 26
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
        Me.CmdView.TabIndex = 25
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
        Me.CmdDelete.TabIndex = 22
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
        Me.CmdSave.TabIndex = 20
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
        Me.CmdModify.TabIndex = 19
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
        Me.CmdAdd.TabIndex = 18
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.cboDisposition)
        Me.fraTop1.Controls.Add(Me.chkPDIR)
        Me.fraTop1.Controls.Add(Me.txtRework)
        Me.fraTop1.Controls.Add(Me.txtSegregated)
        Me.fraTop1.Controls.Add(Me.txtUnderDev)
        Me.fraTop1.Controls.Add(Me.txtAuthorisedBy)
        Me.fraTop1.Controls.Add(Me.cmdSearchAuthorised)
        Me.fraTop1.Controls.Add(Me.cmdSearchInspected)
        Me.fraTop1.Controls.Add(Me.txtInspectedBy)
        Me.fraTop1.Controls.Add(Me.txtPartNo)
        Me.fraTop1.Controls.Add(Me.cmdSearchPartNo)
        Me.fraTop1.Controls.Add(Me.txtSource)
        Me.fraTop1.Controls.Add(Me.cmdSearchSource)
        Me.fraTop1.Controls.Add(Me.txtProject)
        Me.fraTop1.Controls.Add(Me.txtReceivedQty)
        Me.fraTop1.Controls.Add(Me.txtAcceptedQty)
        Me.fraTop1.Controls.Add(Me.txtRejectedQty)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.CmdSearchMRRNo)
        Me.fraTop1.Controls.Add(Me.txtMRRNo)
        Me.fraTop1.Controls.Add(Me.cmdSearchSlipNo)
        Me.fraTop1.Controls.Add(Me.txtDate)
        Me.fraTop1.Controls.Add(Me.txtSlipNo)
        Me.fraTop1.Controls.Add(Me.SprdMain)
        Me.fraTop1.Controls.Add(Me.lblQCStatus)
        Me.fraTop1.Controls.Add(Me.lblApprovedQty)
        Me.fraTop1.Controls.Add(Me.lblAuto_Key_Std)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.lblAuthorisedBy)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.lblInspectedBy)
        Me.fraTop1.Controls.Add(Me.lblPartNo)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Controls.Add(Me.lblSource)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label22)
        Me.fraTop1.Controls.Add(Me.Label23)
        Me.fraTop1.Controls.Add(Me.Label24)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label29)
        Me.fraTop1.Controls.Add(Me.lblBillDate)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.lblBillNo)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(711, 419)
        Me.fraTop1.TabIndex = 30
        Me.fraTop1.TabStop = False
        '
        'cboDisposition
        '
        Me.cboDisposition.BackColor = System.Drawing.SystemColors.Window
        Me.cboDisposition.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDisposition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDisposition.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDisposition.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDisposition.Location = New System.Drawing.Point(583, 168)
        Me.cboDisposition.Name = "cboDisposition"
        Me.cboDisposition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDisposition.Size = New System.Drawing.Size(125, 22)
        Me.cboDisposition.TabIndex = 16
        '
        'chkPDIR
        '
        Me.chkPDIR.AutoSize = True
        Me.chkPDIR.BackColor = System.Drawing.SystemColors.Control
        Me.chkPDIR.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPDIR.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPDIR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPDIR.Location = New System.Drawing.Point(582, 150)
        Me.chkPDIR.Name = "chkPDIR"
        Me.chkPDIR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPDIR.Size = New System.Drawing.Size(99, 17)
        Me.chkPDIR.TabIndex = 15
        Me.chkPDIR.Text = "PDIR Received"
        Me.chkPDIR.UseVisualStyleBackColor = False
        '
        'txtRework
        '
        Me.txtRework.AcceptsReturn = True
        Me.txtRework.BackColor = System.Drawing.SystemColors.Window
        Me.txtRework.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRework.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRework.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRework.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRework.Location = New System.Drawing.Point(583, 106)
        Me.txtRework.MaxLength = 0
        Me.txtRework.Name = "txtRework"
        Me.txtRework.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRework.Size = New System.Drawing.Size(83, 20)
        Me.txtRework.TabIndex = 13
        '
        'txtSegregated
        '
        Me.txtSegregated.AcceptsReturn = True
        Me.txtSegregated.BackColor = System.Drawing.SystemColors.Window
        Me.txtSegregated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSegregated.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSegregated.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSegregated.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSegregated.Location = New System.Drawing.Point(583, 84)
        Me.txtSegregated.MaxLength = 0
        Me.txtSegregated.Name = "txtSegregated"
        Me.txtSegregated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSegregated.Size = New System.Drawing.Size(83, 20)
        Me.txtSegregated.TabIndex = 12
        '
        'txtUnderDev
        '
        Me.txtUnderDev.AcceptsReturn = True
        Me.txtUnderDev.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnderDev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnderDev.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnderDev.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnderDev.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUnderDev.Location = New System.Drawing.Point(583, 62)
        Me.txtUnderDev.MaxLength = 0
        Me.txtUnderDev.Name = "txtUnderDev"
        Me.txtUnderDev.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnderDev.Size = New System.Drawing.Size(83, 20)
        Me.txtUnderDev.TabIndex = 11
        '
        'txtAuthorisedBy
        '
        Me.txtAuthorisedBy.AcceptsReturn = True
        Me.txtAuthorisedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuthorisedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthorisedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuthorisedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthorisedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAuthorisedBy.Location = New System.Drawing.Point(87, 172)
        Me.txtAuthorisedBy.MaxLength = 0
        Me.txtAuthorisedBy.Name = "txtAuthorisedBy"
        Me.txtAuthorisedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuthorisedBy.Size = New System.Drawing.Size(93, 20)
        Me.txtAuthorisedBy.TabIndex = 8
        '
        'txtInspectedBy
        '
        Me.txtInspectedBy.AcceptsReturn = True
        Me.txtInspectedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtInspectedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInspectedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInspectedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspectedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInspectedBy.Location = New System.Drawing.Point(87, 150)
        Me.txtInspectedBy.MaxLength = 0
        Me.txtInspectedBy.Name = "txtInspectedBy"
        Me.txtInspectedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInspectedBy.Size = New System.Drawing.Size(93, 20)
        Me.txtInspectedBy.TabIndex = 7
        '
        'txtPartNo
        '
        Me.txtPartNo.AcceptsReturn = True
        Me.txtPartNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartNo.Location = New System.Drawing.Point(87, 106)
        Me.txtPartNo.MaxLength = 0
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartNo.Size = New System.Drawing.Size(93, 20)
        Me.txtPartNo.TabIndex = 5
        '
        'txtSource
        '
        Me.txtSource.AcceptsReturn = True
        Me.txtSource.BackColor = System.Drawing.SystemColors.Window
        Me.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSource.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSource.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSource.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSource.Location = New System.Drawing.Point(87, 62)
        Me.txtSource.MaxLength = 0
        Me.txtSource.Name = "txtSource"
        Me.txtSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSource.Size = New System.Drawing.Size(93, 20)
        Me.txtSource.TabIndex = 3
        '
        'txtProject
        '
        Me.txtProject.AcceptsReturn = True
        Me.txtProject.BackColor = System.Drawing.SystemColors.Window
        Me.txtProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProject.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProject.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProject.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtProject.Location = New System.Drawing.Point(87, 40)
        Me.txtProject.MaxLength = 0
        Me.txtProject.Name = "txtProject"
        Me.txtProject.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProject.Size = New System.Drawing.Size(393, 20)
        Me.txtProject.TabIndex = 2
        '
        'txtReceivedQty
        '
        Me.txtReceivedQty.AcceptsReturn = True
        Me.txtReceivedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtReceivedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReceivedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReceivedQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReceivedQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtReceivedQty.Location = New System.Drawing.Point(583, 18)
        Me.txtReceivedQty.MaxLength = 0
        Me.txtReceivedQty.Name = "txtReceivedQty"
        Me.txtReceivedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReceivedQty.Size = New System.Drawing.Size(83, 20)
        Me.txtReceivedQty.TabIndex = 9
        '
        'txtAcceptedQty
        '
        Me.txtAcceptedQty.AcceptsReturn = True
        Me.txtAcceptedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtAcceptedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAcceptedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAcceptedQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAcceptedQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAcceptedQty.Location = New System.Drawing.Point(583, 40)
        Me.txtAcceptedQty.MaxLength = 0
        Me.txtAcceptedQty.Name = "txtAcceptedQty"
        Me.txtAcceptedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAcceptedQty.Size = New System.Drawing.Size(83, 20)
        Me.txtAcceptedQty.TabIndex = 10
        '
        'txtRejectedQty
        '
        Me.txtRejectedQty.AcceptsReturn = True
        Me.txtRejectedQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtRejectedQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRejectedQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRejectedQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRejectedQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRejectedQty.Location = New System.Drawing.Point(583, 128)
        Me.txtRejectedQty.MaxLength = 0
        Me.txtRejectedQty.Name = "txtRejectedQty"
        Me.txtRejectedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRejectedQty.Size = New System.Drawing.Size(83, 20)
        Me.txtRejectedQty.TabIndex = 14
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(87, 128)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(393, 20)
        Me.txtRemarks.TabIndex = 6
        '
        'txtMRRNo
        '
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMRRNo.Location = New System.Drawing.Point(87, 84)
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.Size = New System.Drawing.Size(93, 20)
        Me.txtMRRNo.TabIndex = 4
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(388, 18)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(91, 20)
        Me.txtDate.TabIndex = 0
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(87, 18)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipNo.TabIndex = 1
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 196)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(707, 221)
        Me.SprdMain.TabIndex = 17
        '
        'lblQCStatus
        '
        Me.lblQCStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblQCStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblQCStatus.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQCStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblQCStatus.Location = New System.Drawing.Point(266, 18)
        Me.lblQCStatus.Name = "lblQCStatus"
        Me.lblQCStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblQCStatus.Size = New System.Drawing.Size(69, 17)
        Me.lblQCStatus.TabIndex = 63
        Me.lblQCStatus.Text = "lblQCStatus"
        '
        'lblApprovedQty
        '
        Me.lblApprovedQty.BackColor = System.Drawing.SystemColors.Control
        Me.lblApprovedQty.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblApprovedQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblApprovedQty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblApprovedQty.Location = New System.Drawing.Point(670, 76)
        Me.lblApprovedQty.Name = "lblApprovedQty"
        Me.lblApprovedQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblApprovedQty.Size = New System.Drawing.Size(27, 17)
        Me.lblApprovedQty.TabIndex = 62
        Me.lblApprovedQty.Text = "lblAuto_Key_Std"
        '
        'lblAuto_Key_Std
        '
        Me.lblAuto_Key_Std.BackColor = System.Drawing.SystemColors.Control
        Me.lblAuto_Key_Std.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAuto_Key_Std.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuto_Key_Std.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAuto_Key_Std.Location = New System.Drawing.Point(676, 46)
        Me.lblAuto_Key_Std.Name = "lblAuto_Key_Std"
        Me.lblAuto_Key_Std.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAuto_Key_Std.Size = New System.Drawing.Size(27, 17)
        Me.lblAuto_Key_Std.TabIndex = 61
        Me.lblAuto_Key_Std.Text = "lblAuto_Key_Std"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(484, 174)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(65, 13)
        Me.Label12.TabIndex = 60
        Me.Label12.Text = "Disposition"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(484, 110)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(45, 13)
        Me.Label11.TabIndex = 59
        Me.Label11.Text = "Rework"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(484, 88)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(66, 13)
        Me.Label9.TabIndex = 58
        Me.Label9.Text = "Segregated"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(484, 66)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Under Dev"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAuthorisedBy
        '
        Me.lblAuthorisedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblAuthorisedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAuthorisedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAuthorisedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuthorisedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAuthorisedBy.Location = New System.Drawing.Point(205, 172)
        Me.lblAuthorisedBy.Name = "lblAuthorisedBy"
        Me.lblAuthorisedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAuthorisedBy.Size = New System.Drawing.Size(275, 19)
        Me.lblAuthorisedBy.TabIndex = 56
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(4, 176)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(78, 13)
        Me.Label14.TabIndex = 55
        Me.Label14.Text = "Authorised By"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(4, 154)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(72, 13)
        Me.Label10.TabIndex = 54
        Me.Label10.Text = "Inspected By"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblInspectedBy
        '
        Me.lblInspectedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblInspectedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInspectedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInspectedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInspectedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInspectedBy.Location = New System.Drawing.Point(205, 150)
        Me.lblInspectedBy.Name = "lblInspectedBy"
        Me.lblInspectedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInspectedBy.Size = New System.Drawing.Size(275, 19)
        Me.lblInspectedBy.TabIndex = 53
        '
        'lblPartNo
        '
        Me.lblPartNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblPartNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPartNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPartNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPartNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPartNo.Location = New System.Drawing.Point(205, 106)
        Me.lblPartNo.Name = "lblPartNo"
        Me.lblPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPartNo.Size = New System.Drawing.Size(275, 19)
        Me.lblPartNo.TabIndex = 50
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(4, 110)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(45, 13)
        Me.Label16.TabIndex = 49
        Me.Label16.Text = "Part No"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSource
        '
        Me.lblSource.BackColor = System.Drawing.SystemColors.Control
        Me.lblSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSource.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSource.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSource.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSource.Location = New System.Drawing.Point(205, 62)
        Me.lblSource.Name = "lblSource"
        Me.lblSource.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSource.Size = New System.Drawing.Size(275, 19)
        Me.lblSource.TabIndex = 47
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(4, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 46
        Me.Label6.Text = "Source"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 44
        Me.Label5.Text = "Project"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.SystemColors.Control
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(484, 22)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(74, 13)
        Me.Label22.TabIndex = 43
        Me.Label22.Text = "Received Qty"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(484, 44)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(87, 13)
        Me.Label23.TabIndex = 42
        Me.Label23.Text = "Direct Accepted"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(484, 132)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(72, 13)
        Me.Label24.TabIndex = 41
        Me.Label24.Text = "Rejected Qty"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Number"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(4, 88)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(49, 13)
        Me.Label29.TabIndex = 39
        Me.Label29.Text = "MRR No"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBillDate
        '
        Me.lblBillDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBillDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillDate.Location = New System.Drawing.Point(400, 84)
        Me.lblBillDate.Name = "lblBillDate"
        Me.lblBillDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillDate.Size = New System.Drawing.Size(81, 19)
        Me.lblBillDate.TabIndex = 38
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(345, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Bill Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBillNo
        '
        Me.lblBillNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBillNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillNo.Location = New System.Drawing.Point(247, 84)
        Me.lblBillNo.Name = "lblBillNo"
        Me.lblBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillNo.Size = New System.Drawing.Size(93, 19)
        Me.lblBillNo.TabIndex = 36
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(204, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Bill No"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Remarks"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(336, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 32
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(710, 413)
        Me.SprdView.TabIndex = 27
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 408)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(711, 51)
        Me.FraMovement.TabIndex = 28
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
        Me.lblMkey.TabIndex = 29
        Me.lblMkey.Text = "lblMkey"
        '
        'frmRecpInspection
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(711, 458)
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
        Me.Name = "frmRecpInspection"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Receipt Inspection"
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