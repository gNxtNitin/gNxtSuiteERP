Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmExpensesMst
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
    Public WithEvents _OptStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents txtDesc As System.Windows.Forms.TextBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents txtCode As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents lblCode As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
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
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents OptStatus As VB6.RadioButtonArray
    Public WithEvents lblLabels As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExpensesMst))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
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
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._OptStatus_0 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_1 = New System.Windows.Forms.RadioButton()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.lblCode = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.FraGridView = New System.Windows.Forms.GroupBox()
        Me.ADataGrid = New VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.OptStatus = New VB6.RadioButtonArray(Me.components)
        Me.lblLabels = New VB6.LabelArray(Me.components)
        Me.FraView.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraGridView.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(440, 102)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(33, 19)
        Me.cmdsearch.TabIndex = 2
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(182, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 33)
        Me.cmdSavePrint.TabIndex = 6
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
        Me.cmdPreview.Location = New System.Drawing.Point(362, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(60, 33)
        Me.cmdPreview.TabIndex = 9
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
        Me.cmdPrint.Location = New System.Drawing.Point(302, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 34)
        Me.cmdPrint.TabIndex = 8
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
        Me.CmdClose.Location = New System.Drawing.Point(482, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 11
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
        Me.CmdView.Location = New System.Drawing.Point(422, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 34)
        Me.CmdView.TabIndex = 10
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
        Me.CmdSave.Location = New System.Drawing.Point(122, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 34)
        Me.CmdSave.TabIndex = 5
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
        Me.CmdDelete.Location = New System.Drawing.Point(242, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 34)
        Me.CmdDelete.TabIndex = 7
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
        Me.CmdModify.Location = New System.Drawing.Point(62, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 34)
        Me.CmdModify.TabIndex = 4
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
        Me.CmdAdd.Location = New System.Drawing.Point(2, 12)
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
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Controls.Add(Me.Frame3)
        Me.FraView.Controls.Add(Me.txtDesc)
        Me.FraView.Controls.Add(Me.Report1)
        Me.FraView.Controls.Add(Me.cmdsearch)
        Me.FraView.Controls.Add(Me.txtCode)
        Me.FraView.Controls.Add(Me._lblLabels_3)
        Me.FraView.Controls.Add(Me.lblCode)
        Me.FraView.Controls.Add(Me._lblLabels_1)
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -6)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(545, 283)
        Me.FraView.TabIndex = 12
        Me.FraView.TabStop = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Frame3.Controls.Add(Me._OptStatus_0)
        Me.Frame3.Controls.Add(Me._OptStatus_1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(152, 160)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(175, 35)
        Me.Frame3.TabIndex = 20
        Me.Frame3.TabStop = False
        '
        '_OptStatus_0
        '
        Me._OptStatus_0.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptStatus_0.Checked = True
        Me._OptStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_0, CType(0, Short))
        Me._OptStatus_0.Location = New System.Drawing.Point(6, 12)
        Me._OptStatus_0.Name = "_OptStatus_0"
        Me._OptStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_0.Size = New System.Drawing.Size(67, 17)
        Me._OptStatus_0.TabIndex = 22
        Me._OptStatus_0.TabStop = True
        Me._OptStatus_0.Text = "Open"
        Me._OptStatus_0.UseVisualStyleBackColor = False
        '
        '_OptStatus_1
        '
        Me._OptStatus_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._OptStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_1, CType(1, Short))
        Me._OptStatus_1.Location = New System.Drawing.Point(90, 12)
        Me._OptStatus_1.Name = "_OptStatus_1"
        Me._OptStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_1.Size = New System.Drawing.Size(67, 17)
        Me._OptStatus_1.TabIndex = 21
        Me._OptStatus_1.TabStop = True
        Me._OptStatus_1.Text = "Close"
        Me._OptStatus_1.UseVisualStyleBackColor = False
        '
        'txtDesc
        '
        Me.txtDesc.AcceptsReturn = True
        Me.txtDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDesc.Location = New System.Drawing.Point(152, 134)
        Me.txtDesc.MaxLength = 0
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDesc.Size = New System.Drawing.Size(287, 20)
        Me.txtDesc.TabIndex = 3
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(486, 186)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 21
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCode.Location = New System.Drawing.Point(152, 102)
        Me.txtCode.MaxLength = 0
        Me.txtCode.Name = "txtCode"
        Me.txtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCode.Size = New System.Drawing.Size(287, 20)
        Me.txtCode.TabIndex = 1
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = True
        Me._lblLabels_3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_3, CType(3, Short))
        Me._lblLabels_3.Location = New System.Drawing.Point(98, 172)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(48, 14)
        Me._lblLabels_3.TabIndex = 23
        Me._lblLabels_3.Text = "Status :"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.lblCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCode.Location = New System.Drawing.Point(104, 103)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCode.Size = New System.Drawing.Size(42, 14)
        Me.lblCode.TabIndex = 18
        Me.lblCode.Text = "Code :"
        Me.lblCode.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(70, 136)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(76, 14)
        Me._lblLabels_1.TabIndex = 15
        Me._lblLabels_1.Text = "Description :"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraGridView
        '
        Me.FraGridView.BackColor = System.Drawing.SystemColors.Control
        Me.FraGridView.Controls.Add(Me.ADataGrid)
        Me.FraGridView.Controls.Add(Me.SprdView)
        Me.FraGridView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGridView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGridView.Location = New System.Drawing.Point(0, -6)
        Me.FraGridView.Name = "FraGridView"
        Me.FraGridView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGridView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGridView.Size = New System.Drawing.Size(545, 283)
        Me.FraGridView.TabIndex = 13
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
        Me.SprdView.Size = New System.Drawing.Size(539, 271)
        Me.SprdView.TabIndex = 19
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 274)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(545, 49)
        Me.FraMovement.TabIndex = 14
        Me.FraMovement.TabStop = False
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(0, 0)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(107, 14)
        Me._lblLabels_2.TabIndex = 17
        Me._lblLabels_2.Text = "Tariff Description :"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(0, 0)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(107, 14)
        Me._lblLabels_0.TabIndex = 16
        Me._lblLabels_0.Text = "Tariff Description :"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmExpensesMst
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(545, 323)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.FraGridView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExpensesMst"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Expenses Master"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraGridView.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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