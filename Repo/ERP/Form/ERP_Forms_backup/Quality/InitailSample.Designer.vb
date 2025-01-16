Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmInitailSample
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
    Public WithEvents cmdFile As System.Windows.Forms.Button
    Public WithEvents txtFile As System.Windows.Forms.TextBox
    Public WithEvents cmdPopulate As System.Windows.Forms.Button
    Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDrawingNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDept As System.Windows.Forms.Button
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents chkInHouse As System.Windows.Forms.CheckBox
    Public WithEvents txtPartName As System.Windows.Forms.TextBox
    Public WithEvents cboDisposition As System.Windows.Forms.ComboBox
    Public WithEvents chkLabTest As System.Windows.Forms.CheckBox
    Public WithEvents chkFitment As System.Windows.Forms.CheckBox
    Public WithEvents chkDim As System.Windows.Forms.CheckBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents cboSample As System.Windows.Forms.ComboBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchSupplier As System.Windows.Forms.Button
    Public WithEvents txtPartNo As System.Windows.Forms.TextBox
    Public WithEvents txtInspectedBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchInspected As System.Windows.Forms.Button
    Public WithEvents txtFrequency As System.Windows.Forms.TextBox
    Public WithEvents txtProject As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchAuthorised As System.Windows.Forms.Button
    Public WithEvents txtAuthorisedBy As System.Windows.Forms.TextBox
    Public WithEvents txtRefIRDate As System.Windows.Forms.TextBox
    Public WithEvents txtRefIRNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSlipNo As System.Windows.Forms.Button
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents txtNoOfSamples As System.Windows.Forms.TextBox
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblDept As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblSupplier As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblInspectedBy As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents lblAuthorisedBy As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
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
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInitailSample))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchDept = New System.Windows.Forms.Button()
        Me.cmdsearchSupplier = New System.Windows.Forms.Button()
        Me.cmdSearchInspected = New System.Windows.Forms.Button()
        Me.cmdSearchAuthorised = New System.Windows.Forms.Button()
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
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cmdFile = New System.Windows.Forms.Button()
        Me.txtFile = New System.Windows.Forms.TextBox()
        Me.cmdPopulate = New System.Windows.Forms.Button()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtDrawingNo = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.chkInHouse = New System.Windows.Forms.CheckBox()
        Me.txtPartName = New System.Windows.Forms.TextBox()
        Me.cboDisposition = New System.Windows.Forms.ComboBox()
        Me.chkLabTest = New System.Windows.Forms.CheckBox()
        Me.chkFitment = New System.Windows.Forms.CheckBox()
        Me.chkDim = New System.Windows.Forms.CheckBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.cboSample = New System.Windows.Forms.ComboBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.txtPartNo = New System.Windows.Forms.TextBox()
        Me.txtInspectedBy = New System.Windows.Forms.TextBox()
        Me.txtFrequency = New System.Windows.Forms.TextBox()
        Me.txtProject = New System.Windows.Forms.TextBox()
        Me.txtAuthorisedBy = New System.Windows.Forms.TextBox()
        Me.txtRefIRDate = New System.Windows.Forms.TextBox()
        Me.txtRefIRNo = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.txtNoOfSamples = New System.Windows.Forms.TextBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblDept = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblSupplier = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblInspectedBy = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblAuthorisedBy = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.fraTop1.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchDept
        '
        Me.cmdSearchDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDept.Image = CType(resources.GetObject("cmdSearchDept.Image"), System.Drawing.Image)
        Me.cmdSearchDept.Location = New System.Drawing.Point(273, 100)
        Me.cmdSearchDept.Name = "cmdSearchDept"
        Me.cmdSearchDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDept.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchDept.TabIndex = 57
        Me.cmdSearchDept.TabStop = False
        Me.cmdSearchDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDept, "Search")
        Me.cmdSearchDept.UseVisualStyleBackColor = False
        '
        'cmdsearchSupplier
        '
        Me.cmdsearchSupplier.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchSupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchSupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchSupplier.Image = CType(resources.GetObject("cmdsearchSupplier.Image"), System.Drawing.Image)
        Me.cmdsearchSupplier.Location = New System.Drawing.Point(273, 122)
        Me.cmdsearchSupplier.Name = "cmdsearchSupplier"
        Me.cmdsearchSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchSupplier.Size = New System.Drawing.Size(23, 19)
        Me.cmdsearchSupplier.TabIndex = 49
        Me.cmdsearchSupplier.TabStop = False
        Me.cmdsearchSupplier.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchSupplier, "Search")
        Me.cmdsearchSupplier.UseVisualStyleBackColor = False
        '
        'cmdSearchInspected
        '
        Me.cmdSearchInspected.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchInspected.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchInspected.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchInspected.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchInspected.Image = CType(resources.GetObject("cmdSearchInspected.Image"), System.Drawing.Image)
        Me.cmdSearchInspected.Location = New System.Drawing.Point(273, 260)
        Me.cmdSearchInspected.Name = "cmdSearchInspected"
        Me.cmdSearchInspected.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchInspected.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchInspected.TabIndex = 45
        Me.cmdSearchInspected.TabStop = False
        Me.cmdSearchInspected.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchInspected, "Search")
        Me.cmdSearchInspected.UseVisualStyleBackColor = False
        '
        'cmdSearchAuthorised
        '
        Me.cmdSearchAuthorised.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchAuthorised.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchAuthorised.Enabled = False
        Me.cmdSearchAuthorised.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchAuthorised.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchAuthorised.Image = CType(resources.GetObject("cmdSearchAuthorised.Image"), System.Drawing.Image)
        Me.cmdSearchAuthorised.Location = New System.Drawing.Point(273, 282)
        Me.cmdSearchAuthorised.Name = "cmdSearchAuthorised"
        Me.cmdSearchAuthorised.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchAuthorised.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchAuthorised.TabIndex = 40
        Me.cmdSearchAuthorised.TabStop = False
        Me.cmdSearchAuthorised.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchAuthorised, "Search")
        Me.cmdSearchAuthorised.UseVisualStyleBackColor = False
        '
        'cmdSearchSlipNo
        '
        Me.cmdSearchSlipNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipNo.Image = CType(resources.GetObject("cmdSearchSlipNo.Image"), System.Drawing.Image)
        Me.cmdSearchSlipNo.Location = New System.Drawing.Point(273, 12)
        Me.cmdSearchSlipNo.Name = "cmdSearchSlipNo"
        Me.cmdSearchSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSlipNo.TabIndex = 34
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
        Me.CmdPreview.TabIndex = 28
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
        Me.cmdSavePrint.TabIndex = 25
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
        Me.cmdPrint.TabIndex = 27
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
        Me.CmdClose.TabIndex = 30
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
        Me.CmdView.TabIndex = 29
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
        Me.CmdDelete.TabIndex = 26
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
        Me.CmdSave.TabIndex = 24
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
        Me.CmdModify.TabIndex = 23
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
        Me.CmdAdd.TabIndex = 22
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.Frame1)
        Me.fraTop1.Controls.Add(Me.txtDrawingNo)
        Me.fraTop1.Controls.Add(Me.cmdSearchDept)
        Me.fraTop1.Controls.Add(Me.txtDept)
        Me.fraTop1.Controls.Add(Me.chkInHouse)
        Me.fraTop1.Controls.Add(Me.txtPartName)
        Me.fraTop1.Controls.Add(Me.cboDisposition)
        Me.fraTop1.Controls.Add(Me.chkLabTest)
        Me.fraTop1.Controls.Add(Me.chkFitment)
        Me.fraTop1.Controls.Add(Me.chkDim)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.cboSample)
        Me.fraTop1.Controls.Add(Me.txtSupplier)
        Me.fraTop1.Controls.Add(Me.cmdsearchSupplier)
        Me.fraTop1.Controls.Add(Me.txtPartNo)
        Me.fraTop1.Controls.Add(Me.txtInspectedBy)
        Me.fraTop1.Controls.Add(Me.cmdSearchInspected)
        Me.fraTop1.Controls.Add(Me.txtFrequency)
        Me.fraTop1.Controls.Add(Me.txtProject)
        Me.fraTop1.Controls.Add(Me.cmdSearchAuthorised)
        Me.fraTop1.Controls.Add(Me.txtAuthorisedBy)
        Me.fraTop1.Controls.Add(Me.txtRefIRDate)
        Me.fraTop1.Controls.Add(Me.txtRefIRNo)
        Me.fraTop1.Controls.Add(Me.cmdSearchSlipNo)
        Me.fraTop1.Controls.Add(Me.txtDate)
        Me.fraTop1.Controls.Add(Me.txtSlipNo)
        Me.fraTop1.Controls.Add(Me.SprdMain)
        Me.fraTop1.Controls.Add(Me.txtNoOfSamples)
        Me.fraTop1.Controls.Add(Me.lblBookType)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label13)
        Me.fraTop1.Controls.Add(Me.lblDept)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label18)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.lblSupplier)
        Me.fraTop1.Controls.Add(Me.Label17)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Controls.Add(Me.lblInspectedBy)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.lblAuthorisedBy)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(710, 466)
        Me.fraTop1.TabIndex = 33
        Me.fraTop1.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdFile)
        Me.Frame1.Controls.Add(Me.txtFile)
        Me.Frame1.Controls.Add(Me.cmdPopulate)
        Me.Frame1.Controls.Add(Me.Label24)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(335, 168)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(369, 65)
        Me.Frame1.TabIndex = 62
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Populate Data from File"
        '
        'cmdFile
        '
        Me.cmdFile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFile.Location = New System.Drawing.Point(347, 16)
        Me.cmdFile.Name = "cmdFile"
        Me.cmdFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFile.Size = New System.Drawing.Size(17, 21)
        Me.cmdFile.TabIndex = 65
        Me.cmdFile.Text = "..."
        Me.cmdFile.UseVisualStyleBackColor = False
        '
        'txtFile
        '
        Me.txtFile.AcceptsReturn = True
        Me.txtFile.BackColor = System.Drawing.SystemColors.Window
        Me.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFile.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFile.Enabled = False
        Me.txtFile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFile.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFile.Location = New System.Drawing.Point(41, 16)
        Me.txtFile.MaxLength = 5000
        Me.txtFile.Name = "txtFile"
        Me.txtFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFile.Size = New System.Drawing.Size(304, 20)
        Me.txtFile.TabIndex = 64
        '
        'cmdPopulate
        '
        Me.cmdPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulate.Location = New System.Drawing.Point(40, 40)
        Me.cmdPopulate.Name = "cmdPopulate"
        Me.cmdPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulate.Size = New System.Drawing.Size(323, 21)
        Me.cmdPopulate.TabIndex = 63
        Me.cmdPopulate.Text = "Populate"
        Me.cmdPopulate.UseVisualStyleBackColor = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(5, 20)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(31, 13)
        Me.Label24.TabIndex = 66
        Me.Label24.Text = "File :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDrawingNo
        '
        Me.txtDrawingNo.AcceptsReturn = True
        Me.txtDrawingNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDrawingNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDrawingNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDrawingNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDrawingNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDrawingNo.Location = New System.Drawing.Point(109, 56)
        Me.txtDrawingNo.MaxLength = 0
        Me.txtDrawingNo.Name = "txtDrawingNo"
        Me.txtDrawingNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDrawingNo.Size = New System.Drawing.Size(163, 20)
        Me.txtDrawingNo.TabIndex = 4
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDept.Location = New System.Drawing.Point(109, 100)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(163, 20)
        Me.txtDept.TabIndex = 9
        '
        'chkInHouse
        '
        Me.chkInHouse.BackColor = System.Drawing.SystemColors.Control
        Me.chkInHouse.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInHouse.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInHouse.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInHouse.Location = New System.Drawing.Point(297, 81)
        Me.chkInHouse.Name = "chkInHouse"
        Me.chkInHouse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInHouse.Size = New System.Drawing.Size(109, 13)
        Me.chkInHouse.TabIndex = 7
        Me.chkInHouse.Text = "In House"
        Me.chkInHouse.UseVisualStyleBackColor = False
        '
        'txtPartName
        '
        Me.txtPartName.AcceptsReturn = True
        Me.txtPartName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPartName.Location = New System.Drawing.Point(521, 34)
        Me.txtPartName.MaxLength = 0
        Me.txtPartName.Name = "txtPartName"
        Me.txtPartName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartName.Size = New System.Drawing.Size(181, 20)
        Me.txtPartName.TabIndex = 3
        '
        'cboDisposition
        '
        Me.cboDisposition.BackColor = System.Drawing.SystemColors.Window
        Me.cboDisposition.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDisposition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDisposition.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDisposition.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDisposition.Location = New System.Drawing.Point(109, 214)
        Me.cboDisposition.Name = "cboDisposition"
        Me.cboDisposition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDisposition.Size = New System.Drawing.Size(163, 22)
        Me.cboDisposition.TabIndex = 17
        '
        'chkLabTest
        '
        Me.chkLabTest.BackColor = System.Drawing.SystemColors.Control
        Me.chkLabTest.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLabTest.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLabTest.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLabTest.Location = New System.Drawing.Point(617, 148)
        Me.chkLabTest.Name = "chkLabTest"
        Me.chkLabTest.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLabTest.Size = New System.Drawing.Size(79, 13)
        Me.chkLabTest.TabIndex = 14
        Me.chkLabTest.Text = "Lab Test"
        Me.chkLabTest.UseVisualStyleBackColor = False
        '
        'chkFitment
        '
        Me.chkFitment.BackColor = System.Drawing.SystemColors.Control
        Me.chkFitment.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFitment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFitment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFitment.Location = New System.Drawing.Point(449, 148)
        Me.chkFitment.Name = "chkFitment"
        Me.chkFitment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFitment.Size = New System.Drawing.Size(109, 13)
        Me.chkFitment.TabIndex = 13
        Me.chkFitment.Text = "Fitment Check"
        Me.chkFitment.UseVisualStyleBackColor = False
        '
        'chkDim
        '
        Me.chkDim.BackColor = System.Drawing.SystemColors.Control
        Me.chkDim.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDim.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDim.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDim.Location = New System.Drawing.Point(297, 148)
        Me.chkDim.Name = "chkDim"
        Me.chkDim.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDim.Size = New System.Drawing.Size(109, 13)
        Me.chkDim.TabIndex = 12
        Me.chkDim.Text = "Dim. Check"
        Me.chkDim.UseVisualStyleBackColor = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(109, 238)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(593, 20)
        Me.txtRemarks.TabIndex = 18
        '
        'cboSample
        '
        Me.cboSample.BackColor = System.Drawing.SystemColors.Window
        Me.cboSample.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSample.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSample.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSample.Location = New System.Drawing.Point(109, 144)
        Me.cboSample.Name = "cboSample"
        Me.cboSample.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSample.Size = New System.Drawing.Size(163, 22)
        Me.cboSample.TabIndex = 11
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSupplier.Location = New System.Drawing.Point(109, 122)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(163, 20)
        Me.txtSupplier.TabIndex = 10
        '
        'txtPartNo
        '
        Me.txtPartNo.AcceptsReturn = True
        Me.txtPartNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartNo.Location = New System.Drawing.Point(109, 34)
        Me.txtPartNo.MaxLength = 0
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartNo.Size = New System.Drawing.Size(163, 20)
        Me.txtPartNo.TabIndex = 2
        '
        'txtInspectedBy
        '
        Me.txtInspectedBy.AcceptsReturn = True
        Me.txtInspectedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtInspectedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInspectedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInspectedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspectedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInspectedBy.Location = New System.Drawing.Point(109, 260)
        Me.txtInspectedBy.MaxLength = 0
        Me.txtInspectedBy.Name = "txtInspectedBy"
        Me.txtInspectedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInspectedBy.Size = New System.Drawing.Size(163, 20)
        Me.txtInspectedBy.TabIndex = 19
        '
        'txtFrequency
        '
        Me.txtFrequency.AcceptsReturn = True
        Me.txtFrequency.BackColor = System.Drawing.SystemColors.Window
        Me.txtFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFrequency.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFrequency.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrequency.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFrequency.Location = New System.Drawing.Point(109, 192)
        Me.txtFrequency.MaxLength = 0
        Me.txtFrequency.Name = "txtFrequency"
        Me.txtFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFrequency.Size = New System.Drawing.Size(163, 20)
        Me.txtFrequency.TabIndex = 16
        '
        'txtProject
        '
        Me.txtProject.AcceptsReturn = True
        Me.txtProject.BackColor = System.Drawing.SystemColors.Window
        Me.txtProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProject.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProject.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProject.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtProject.Location = New System.Drawing.Point(521, 56)
        Me.txtProject.MaxLength = 0
        Me.txtProject.Name = "txtProject"
        Me.txtProject.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProject.Size = New System.Drawing.Size(181, 20)
        Me.txtProject.TabIndex = 5
        '
        'txtAuthorisedBy
        '
        Me.txtAuthorisedBy.AcceptsReturn = True
        Me.txtAuthorisedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtAuthorisedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAuthorisedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAuthorisedBy.Enabled = False
        Me.txtAuthorisedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAuthorisedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAuthorisedBy.Location = New System.Drawing.Point(109, 282)
        Me.txtAuthorisedBy.MaxLength = 0
        Me.txtAuthorisedBy.Name = "txtAuthorisedBy"
        Me.txtAuthorisedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAuthorisedBy.Size = New System.Drawing.Size(163, 20)
        Me.txtAuthorisedBy.TabIndex = 20
        '
        'txtRefIRDate
        '
        Me.txtRefIRDate.AcceptsReturn = True
        Me.txtRefIRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefIRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefIRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefIRDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefIRDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRefIRDate.Location = New System.Drawing.Point(520, 78)
        Me.txtRefIRDate.MaxLength = 0
        Me.txtRefIRDate.Name = "txtRefIRDate"
        Me.txtRefIRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefIRDate.Size = New System.Drawing.Size(181, 20)
        Me.txtRefIRDate.TabIndex = 8
        '
        'txtRefIRNo
        '
        Me.txtRefIRNo.AcceptsReturn = True
        Me.txtRefIRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefIRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefIRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefIRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefIRNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRefIRNo.Location = New System.Drawing.Point(109, 78)
        Me.txtRefIRNo.MaxLength = 0
        Me.txtRefIRNo.Name = "txtRefIRNo"
        Me.txtRefIRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefIRNo.Size = New System.Drawing.Size(163, 20)
        Me.txtRefIRNo.TabIndex = 6
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(521, 12)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(181, 20)
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
        Me.txtSlipNo.Location = New System.Drawing.Point(109, 12)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(163, 20)
        Me.txtSlipNo.TabIndex = 0
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 308)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(707, 155)
        Me.SprdMain.TabIndex = 21
        '
        'txtNoOfSamples
        '
        Me.txtNoOfSamples.AcceptsReturn = True
        Me.txtNoOfSamples.BackColor = System.Drawing.SystemColors.Window
        Me.txtNoOfSamples.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNoOfSamples.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNoOfSamples.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNoOfSamples.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNoOfSamples.Location = New System.Drawing.Point(109, 170)
        Me.txtNoOfSamples.MaxLength = 0
        Me.txtNoOfSamples.Name = "txtNoOfSamples"
        Me.txtNoOfSamples.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNoOfSamples.Size = New System.Drawing.Size(163, 20)
        Me.txtNoOfSamples.TabIndex = 15
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(14, 108)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(31, 19)
        Me.lblBookType.TabIndex = 61
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(9, 59)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(64, 13)
        Me.Label9.TabIndex = 60
        Me.Label9.Text = "DrawingNo"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(9, 103)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(32, 13)
        Me.Label13.TabIndex = 59
        Me.Label13.Text = "Dept"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDept
        '
        Me.lblDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDept.Location = New System.Drawing.Point(297, 100)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDept.Size = New System.Drawing.Size(405, 19)
        Me.lblDept.TabIndex = 58
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(416, 37)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Part Name"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(9, 218)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Disposition"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(9, 241)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(51, 13)
        Me.Label18.TabIndex = 54
        Me.Label18.Text = "Remarks"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(9, 148)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 53
        Me.Label5.Text = "Sample"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSupplier
        '
        Me.lblSupplier.BackColor = System.Drawing.SystemColors.Control
        Me.lblSupplier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSupplier.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSupplier.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSupplier.Location = New System.Drawing.Point(297, 122)
        Me.lblSupplier.Name = "lblSupplier"
        Me.lblSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSupplier.Size = New System.Drawing.Size(405, 19)
        Me.lblSupplier.TabIndex = 52
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(9, 125)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(49, 13)
        Me.Label17.TabIndex = 51
        Me.Label17.Text = "Supplier"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(9, 37)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(45, 13)
        Me.Label16.TabIndex = 50
        Me.Label16.Text = "Part No"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblInspectedBy
        '
        Me.lblInspectedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblInspectedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInspectedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInspectedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInspectedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInspectedBy.Location = New System.Drawing.Point(297, 260)
        Me.lblInspectedBy.Name = "lblInspectedBy"
        Me.lblInspectedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInspectedBy.Size = New System.Drawing.Size(405, 19)
        Me.lblInspectedBy.TabIndex = 47
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(9, 263)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(72, 13)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Inspected By"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(9, 195)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Frequency"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(416, 59)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Project"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(9, 285)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(78, 13)
        Me.Label14.TabIndex = 42
        Me.Label14.Text = "Authorised By"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAuthorisedBy
        '
        Me.lblAuthorisedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblAuthorisedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAuthorisedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAuthorisedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuthorisedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAuthorisedBy.Location = New System.Drawing.Point(297, 282)
        Me.lblAuthorisedBy.Name = "lblAuthorisedBy"
        Me.lblAuthorisedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAuthorisedBy.Size = New System.Drawing.Size(405, 19)
        Me.lblAuthorisedBy.TabIndex = 41
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(9, 173)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(86, 13)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "No. Of Samples"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(416, 81)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(69, 13)
        Me.Label11.TabIndex = 38
        Me.Label11.Text = "Ref. I.R Date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(9, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Ref. I.R No"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(416, 15)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 36
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
        Me.Label7.Location = New System.Drawing.Point(9, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 35
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
        Me.Report1.TabIndex = 35
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
        Me.FraMovement.TabIndex = 31
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
        Me.lblMkey.TabIndex = 32
        Me.lblMkey.Text = "lblMkey"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(710, 457)
        Me.SprdView.TabIndex = 48
        '
        'frmInitailSample
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(710, 509)
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
        Me.Name = "frmInitailSample"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Initial Sample Parts Inspection"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
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