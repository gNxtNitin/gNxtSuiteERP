Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBillExpGST
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
        'Me.MDIParent = Admin.Master
        'Admin.Master.Show
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
    Public WithEvents txtGSTRecoverable As System.Windows.Forms.TextBox
    Public WithEvents chkGSTRecoverable As System.Windows.Forms.CheckBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents txtScrap As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents chkIncludingSales As System.Windows.Forms.CheckBox
    Public WithEvents txtSalesExpHead As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents chkTaxable As System.Windows.Forms.CheckBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _OptType_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptType_2 As System.Windows.Forms.RadioButton
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents chkRoundOff As System.Windows.Forms.CheckBox
    Public WithEvents txtPrintSequence As System.Windows.Forms.TextBox
    Public WithEvents TxtDefaultPer As System.Windows.Forms.TextBox
    Public WithEvents _OptAdd_Ded_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptAdd_Ded_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents CboIdentification As System.Windows.Forms.ComboBox
    Public WithEvents TxtCode As System.Windows.Forms.TextBox
    Public WithEvents txtPurchase As System.Windows.Forms.TextBox
    Public WithEvents txtSales As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents ADataGrid As VB6.ADODC
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraGridView As System.Windows.Forms.GroupBox
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents OptAdd_Ded As VB6.RadioButtonArray
    Public WithEvents OptStatus As VB6.RadioButtonArray
    Public WithEvents OptType As VB6.RadioButtonArray
    Public WithEvents lblLabels As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBillExpGST))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.txtGSTRecoverable = New System.Windows.Forms.TextBox()
        Me.chkGSTRecoverable = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.txtScrap = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkIncludingSales = New System.Windows.Forms.CheckBox()
        Me.txtSalesExpHead = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkTaxable = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._OptType_0 = New System.Windows.Forms.RadioButton()
        Me._OptType_1 = New System.Windows.Forms.RadioButton()
        Me._OptType_2 = New System.Windows.Forms.RadioButton()
        Me.chkRoundOff = New System.Windows.Forms.CheckBox()
        Me.txtPrintSequence = New System.Windows.Forms.TextBox()
        Me.TxtDefaultPer = New System.Windows.Forms.TextBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._OptAdd_Ded_0 = New System.Windows.Forms.RadioButton()
        Me._OptAdd_Ded_1 = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me._OptStatus_1 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_0 = New System.Windows.Forms.RadioButton()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.CboIdentification = New System.Windows.Forms.ComboBox()
        Me.TxtCode = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtPurchase = New System.Windows.Forms.TextBox()
        Me.txtSales = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraGridView = New System.Windows.Forms.GroupBox()
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.OptAdd_Ded = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.FraView.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.FraGridView.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.OptAdd_Ded, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(188, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(57, 33)
        Me.cmdSavePrint.TabIndex = 17
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPreview.Location = New System.Drawing.Point(364, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(51, 33)
        Me.cmdPreview.TabIndex = 20
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPreview, "Print Preview")
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(304, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 34)
        Me.cmdPrint.TabIndex = 19
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print Record")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdClose.Location = New System.Drawing.Point(474, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 22
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(414, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 34)
        Me.CmdView.TabIndex = 21
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdSave.Location = New System.Drawing.Point(128, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 34)
        Me.CmdSave.TabIndex = 16
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdDelete.Location = New System.Drawing.Point(244, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 34)
        Me.CmdDelete.TabIndex = 18
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdModify.Location = New System.Drawing.Point(68, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 34)
        Me.CmdModify.TabIndex = 15
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdAdd.Location = New System.Drawing.Point(8, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 34)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.Frame8)
        Me.FraView.Controls.Add(Me.Frame7)
        Me.FraView.Controls.Add(Me.Frame5)
        Me.FraView.Controls.Add(Me.Frame2)
        Me.FraView.Controls.Add(Me.Frame4)
        Me.FraView.Controls.Add(Me.chkRoundOff)
        Me.FraView.Controls.Add(Me.txtPrintSequence)
        Me.FraView.Controls.Add(Me.TxtDefaultPer)
        Me.FraView.Controls.Add(Me.Frame3)
        Me.FraView.Controls.Add(Me.Frame6)
        Me.FraView.Controls.Add(Me.Report1)
        Me.FraView.Controls.Add(Me.txtName)
        Me.FraView.Controls.Add(Me.CboIdentification)
        Me.FraView.Controls.Add(Me.TxtCode)
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.Label5)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Controls.Add(Me._lblLabels_0)
        Me.FraView.Controls.Add(Me.Label2)
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -4)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(539, 351)
        Me.FraView.TabIndex = 23
        Me.FraView.TabStop = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.txtGSTRecoverable)
        Me.Frame8.Controls.Add(Me.chkGSTRecoverable)
        Me.Frame8.Controls.Add(Me.Label8)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame8.Location = New System.Drawing.Point(0, 288)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(539, 63)
        Me.Frame8.TabIndex = 52
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "GST Recoverable (Export)"
        '
        'txtGSTRecoverable
        '
        Me.txtGSTRecoverable.AcceptsReturn = True
        Me.txtGSTRecoverable.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSTRecoverable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSTRecoverable.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSTRecoverable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSTRecoverable.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtGSTRecoverable.Location = New System.Drawing.Point(128, 34)
        Me.txtGSTRecoverable.MaxLength = 0
        Me.txtGSTRecoverable.Name = "txtGSTRecoverable"
        Me.txtGSTRecoverable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSTRecoverable.Size = New System.Drawing.Size(355, 22)
        Me.txtGSTRecoverable.TabIndex = 54
        '
        'chkGSTRecoverable
        '
        Me.chkGSTRecoverable.BackColor = System.Drawing.SystemColors.Control
        Me.chkGSTRecoverable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkGSTRecoverable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkGSTRecoverable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGSTRecoverable.Location = New System.Drawing.Point(128, 16)
        Me.chkGSTRecoverable.Name = "chkGSTRecoverable"
        Me.chkGSTRecoverable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkGSTRecoverable.Size = New System.Drawing.Size(191, 17)
        Me.chkGSTRecoverable.TabIndex = 53
        Me.chkGSTRecoverable.Text = "GST Recoverable (Export)"
        Me.chkGSTRecoverable.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(4, 38)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(106, 13)
        Me.Label8.TabIndex = 56
        Me.Label8.Text = "Recoverable Head :"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.txtScrap)
        Me.Frame7.Controls.Add(Me.Label6)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame7.Location = New System.Drawing.Point(0, 208)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(539, 41)
        Me.Frame7.TabIndex = 48
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "TCS Posting (For Scrap)"
        '
        'txtScrap
        '
        Me.txtScrap.AcceptsReturn = True
        Me.txtScrap.BackColor = System.Drawing.SystemColors.Window
        Me.txtScrap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScrap.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScrap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScrap.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtScrap.Location = New System.Drawing.Point(128, 12)
        Me.txtScrap.MaxLength = 0
        Me.txtScrap.Name = "txtScrap"
        Me.txtScrap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScrap.Size = New System.Drawing.Size(355, 22)
        Me.txtScrap.TabIndex = 49
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(8, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(98, 13)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Sales (For Scrap) :"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.chkIncludingSales)
        Me.Frame5.Controls.Add(Me.txtSalesExpHead)
        Me.Frame5.Controls.Add(Me.Label7)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame5.Location = New System.Drawing.Point(0, 248)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(539, 39)
        Me.Frame5.TabIndex = 43
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Posting"
        '
        'chkIncludingSales
        '
        Me.chkIncludingSales.BackColor = System.Drawing.SystemColors.Control
        Me.chkIncludingSales.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkIncludingSales.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludingSales.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkIncludingSales.Location = New System.Drawing.Point(414, 16)
        Me.chkIncludingSales.Name = "chkIncludingSales"
        Me.chkIncludingSales.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkIncludingSales.Size = New System.Drawing.Size(115, 16)
        Me.chkIncludingSales.TabIndex = 46
        Me.chkIncludingSales.Text = "Include in Sales"
        Me.chkIncludingSales.UseVisualStyleBackColor = False
        '
        'txtSalesExpHead
        '
        Me.txtSalesExpHead.AcceptsReturn = True
        Me.txtSalesExpHead.BackColor = System.Drawing.SystemColors.Window
        Me.txtSalesExpHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesExpHead.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSalesExpHead.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesExpHead.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSalesExpHead.Location = New System.Drawing.Point(128, 12)
        Me.txtSalesExpHead.MaxLength = 0
        Me.txtSalesExpHead.Name = "txtSalesExpHead"
        Me.txtSalesExpHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSalesExpHead.Size = New System.Drawing.Size(251, 22)
        Me.txtSalesExpHead.TabIndex = 45
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(114, 13)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "Sales Expense Head :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.chkTaxable)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(390, 84)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(149, 65)
        Me.Frame2.TabIndex = 41
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "GST Applicable"
        '
        'chkTaxable
        '
        Me.chkTaxable.BackColor = System.Drawing.SystemColors.Control
        Me.chkTaxable.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTaxable.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTaxable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTaxable.Location = New System.Drawing.Point(16, 28)
        Me.chkTaxable.Name = "chkTaxable"
        Me.chkTaxable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTaxable.Size = New System.Drawing.Size(85, 18)
        Me.chkTaxable.TabIndex = 42
        Me.chkTaxable.Text = "(Yes / No)"
        Me.chkTaxable.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._OptType_0)
        Me.Frame4.Controls.Add(Me._OptType_1)
        Me.Frame4.Controls.Add(Me._OptType_2)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(240, 84)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(149, 65)
        Me.Frame4.TabIndex = 37
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Expenses Applicable"
        '
        '_OptType_0
        '
        Me._OptType_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_0, CType(0, Short))
        Me._OptType_0.Location = New System.Drawing.Point(32, 14)
        Me._OptType_0.Name = "_OptType_0"
        Me._OptType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_0.Size = New System.Drawing.Size(110, 18)
        Me._OptType_0.TabIndex = 40
        Me._OptType_0.TabStop = True
        Me._OptType_0.Text = "For Purchase"
        Me._OptType_0.UseVisualStyleBackColor = False
        '
        '_OptType_1
        '
        Me._OptType_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_1, CType(1, Short))
        Me._OptType_1.Location = New System.Drawing.Point(32, 30)
        Me._OptType_1.Name = "_OptType_1"
        Me._OptType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_1.Size = New System.Drawing.Size(110, 18)
        Me._OptType_1.TabIndex = 39
        Me._OptType_1.TabStop = True
        Me._OptType_1.Text = "For Sales"
        Me._OptType_1.UseVisualStyleBackColor = False
        '
        '_OptType_2
        '
        Me._OptType_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptType_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptType.SetIndex(Me._OptType_2, CType(2, Short))
        Me._OptType_2.Location = New System.Drawing.Point(32, 46)
        Me._OptType_2.Name = "_OptType_2"
        Me._OptType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptType_2.Size = New System.Drawing.Size(110, 18)
        Me._OptType_2.TabIndex = 38
        Me._OptType_2.TabStop = True
        Me._OptType_2.Text = "Both"
        Me._OptType_2.UseVisualStyleBackColor = False
        '
        'chkRoundOff
        '
        Me.chkRoundOff.BackColor = System.Drawing.SystemColors.Control
        Me.chkRoundOff.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRoundOff.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRoundOff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRoundOff.Location = New System.Drawing.Point(348, 66)
        Me.chkRoundOff.Name = "chkRoundOff"
        Me.chkRoundOff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRoundOff.Size = New System.Drawing.Size(85, 16)
        Me.chkRoundOff.TabIndex = 6
        Me.chkRoundOff.Text = "Round Off"
        Me.chkRoundOff.UseVisualStyleBackColor = False
        '
        'txtPrintSequence
        '
        Me.txtPrintSequence.AcceptsReturn = True
        Me.txtPrintSequence.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrintSequence.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrintSequence.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrintSequence.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrintSequence.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPrintSequence.Location = New System.Drawing.Point(290, 62)
        Me.txtPrintSequence.MaxLength = 0
        Me.txtPrintSequence.Name = "txtPrintSequence"
        Me.txtPrintSequence.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrintSequence.Size = New System.Drawing.Size(37, 22)
        Me.txtPrintSequence.TabIndex = 5
        '
        'TxtDefaultPer
        '
        Me.TxtDefaultPer.AcceptsReturn = True
        Me.TxtDefaultPer.BackColor = System.Drawing.SystemColors.Window
        Me.TxtDefaultPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtDefaultPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtDefaultPer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDefaultPer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.TxtDefaultPer.Location = New System.Drawing.Point(146, 62)
        Me.TxtDefaultPer.MaxLength = 0
        Me.TxtDefaultPer.Name = "TxtDefaultPer"
        Me.TxtDefaultPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtDefaultPer.Size = New System.Drawing.Size(37, 22)
        Me.TxtDefaultPer.TabIndex = 4
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._OptAdd_Ded_0)
        Me.Frame3.Controls.Add(Me._OptAdd_Ded_1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(2, 84)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(113, 65)
        Me.Frame3.TabIndex = 33
        Me.Frame3.TabStop = False
        '
        '_OptAdd_Ded_0
        '
        Me._OptAdd_Ded_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptAdd_Ded_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptAdd_Ded_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptAdd_Ded_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAdd_Ded.SetIndex(Me._OptAdd_Ded_0, CType(0, Short))
        Me._OptAdd_Ded_0.Location = New System.Drawing.Point(28, 17)
        Me._OptAdd_Ded_0.Name = "_OptAdd_Ded_0"
        Me._OptAdd_Ded_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptAdd_Ded_0.Size = New System.Drawing.Size(69, 16)
        Me._OptAdd_Ded_0.TabIndex = 7
        Me._OptAdd_Ded_0.TabStop = True
        Me._OptAdd_Ded_0.Text = "Add"
        Me._OptAdd_Ded_0.UseVisualStyleBackColor = False
        '
        '_OptAdd_Ded_1
        '
        Me._OptAdd_Ded_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptAdd_Ded_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptAdd_Ded_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptAdd_Ded_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAdd_Ded.SetIndex(Me._OptAdd_Ded_1, CType(1, Short))
        Me._OptAdd_Ded_1.Location = New System.Drawing.Point(28, 39)
        Me._OptAdd_Ded_1.Name = "_OptAdd_Ded_1"
        Me._OptAdd_Ded_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptAdd_Ded_1.Size = New System.Drawing.Size(65, 16)
        Me._OptAdd_Ded_1.TabIndex = 8
        Me._OptAdd_Ded_1.TabStop = True
        Me._OptAdd_Ded_1.Text = "Deduct"
        Me._OptAdd_Ded_1.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me._OptStatus_1)
        Me.Frame6.Controls.Add(Me._OptStatus_0)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(116, 84)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(123, 65)
        Me.Frame6.TabIndex = 32
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Status"
        '
        '_OptStatus_1
        '
        Me._OptStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_1, CType(1, Short))
        Me._OptStatus_1.Location = New System.Drawing.Point(50, 42)
        Me._OptStatus_1.Name = "_OptStatus_1"
        Me._OptStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_1.Size = New System.Drawing.Size(63, 16)
        Me._OptStatus_1.TabIndex = 10
        Me._OptStatus_1.TabStop = True
        Me._OptStatus_1.Text = "Close"
        Me._OptStatus_1.UseVisualStyleBackColor = False
        '
        '_OptStatus_0
        '
        Me._OptStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_0.Checked = True
        Me._OptStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_0, CType(0, Short))
        Me._OptStatus_0.Location = New System.Drawing.Point(50, 20)
        Me._OptStatus_0.Name = "_OptStatus_0"
        Me._OptStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_0.Size = New System.Drawing.Size(61, 16)
        Me._OptStatus_0.TabIndex = 9
        Me._OptStatus_0.TabStop = True
        Me._OptStatus_0.Text = "Open"
        Me._OptStatus_0.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(10, 36)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 53
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtName.Location = New System.Drawing.Point(146, 16)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(285, 22)
        Me.txtName.TabIndex = 1
        '
        'CboIdentification
        '
        Me.CboIdentification.BackColor = System.Drawing.SystemColors.Window
        Me.CboIdentification.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboIdentification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboIdentification.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboIdentification.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.CboIdentification.Location = New System.Drawing.Point(146, 38)
        Me.CboIdentification.Name = "CboIdentification"
        Me.CboIdentification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboIdentification.Size = New System.Drawing.Size(285, 21)
        Me.CboIdentification.TabIndex = 3
        '
        'TxtCode
        '
        Me.TxtCode.AcceptsReturn = True
        Me.TxtCode.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCode.Location = New System.Drawing.Point(275, 16)
        Me.TxtCode.MaxLength = 0
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCode.Size = New System.Drawing.Size(19, 22)
        Me.TxtCode.TabIndex = 26
        Me.TxtCode.Text = "Text1"
        Me.TxtCode.Visible = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtPurchase)
        Me.Frame1.Controls.Add(Me.txtSales)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(0, 148)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(539, 59)
        Me.Frame1.TabIndex = 27
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Posting"
        '
        'txtPurchase
        '
        Me.txtPurchase.AcceptsReturn = True
        Me.txtPurchase.BackColor = System.Drawing.SystemColors.Window
        Me.txtPurchase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPurchase.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPurchase.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPurchase.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPurchase.Location = New System.Drawing.Point(128, 34)
        Me.txtPurchase.MaxLength = 0
        Me.txtPurchase.Name = "txtPurchase"
        Me.txtPurchase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPurchase.Size = New System.Drawing.Size(355, 22)
        Me.txtPurchase.TabIndex = 13
        '
        'txtSales
        '
        Me.txtSales.AcceptsReturn = True
        Me.txtSales.BackColor = System.Drawing.SystemColors.Window
        Me.txtSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSales.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSales.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSales.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSales.Location = New System.Drawing.Point(128, 10)
        Me.txtSales.MaxLength = 0
        Me.txtSales.Name = "txtSales"
        Me.txtSales.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSales.Size = New System.Drawing.Size(355, 22)
        Me.txtSales.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(52, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Purchase :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(52, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Sales :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(192, 64)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(87, 13)
        Me.Label5.TabIndex = 36
        Me.Label5.Text = "Print Sequence :"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(17, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(120, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "Default % :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(17, 20)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(120, 13)
        Me._lblLabels_0.TabIndex = 31
        Me._lblLabels_0.Text = "Name :"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(17, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(120, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Identification :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraGridView
        '
        Me.FraGridView.BackColor = System.Drawing.SystemColors.Control
        Me.FraGridView.Controls.Add(Me.ADataGrid)
        Me.FraGridView.Controls.Add(Me.SprdView)
        Me.FraGridView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGridView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGridView.Location = New System.Drawing.Point(0, -4)
        Me.FraGridView.Name = "FraGridView"
        Me.FraGridView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGridView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGridView.Size = New System.Drawing.Size(539, 351)
        Me.FraGridView.TabIndex = 24
        Me.FraGridView.TabStop = False
        '
        'ADataGrid
        '
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.CommandTimeout = 0
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.ConnectionString = Nothing
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Location = New System.Drawing.Point(166, 38)
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.Name = "ADataGrid"
        Me.ADataGrid.Size = New System.Drawing.Size(99, 27)
        Me.ADataGrid.TabIndex = 0
        Me.ADataGrid.Text = "Adodc1"
        Me.ADataGrid.Visible = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(533, 339)
        Me.SprdView.TabIndex = 34
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 342)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(539, 49)
        Me.FraMovement.TabIndex = 25
        Me.FraMovement.TabStop = False
        '
        'OptAdd_Ded
        '
        '
        'OptStatus
        '
        '
        'OptType
        '
        '
        'frmBillExpGST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(540, 392)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.FraGridView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBillExpGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Bill Expenses"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraGridView.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.OptAdd_Ded, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class