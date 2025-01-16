Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGaugeFixCal
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
    Public WithEvents cmdSearchNo As System.Windows.Forms.Button
    Public WithEvents CmdSearchGauge As System.Windows.Forms.Button
    Public WithEvents txtGaugeNo As System.Windows.Forms.TextBox
    Public WithEvents txtNumber As System.Windows.Forms.TextBox
    Public WithEvents txtPartName As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents txtActualNoGoSize As System.Windows.Forms.TextBox
    Public WithEvents txtActualGoSize As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblRemark As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents lblDepartment As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblGoSize As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblNoGoSize As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblWearSize As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents lblCompSize As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblFrequency As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents lblGaugeDesc As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblModel As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblCustomer As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblNew As System.Windows.Forms.Label
    Public WithEvents lblDueDate As System.Windows.Forms.Label
    Public WithEvents lblDocNo As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGaugeFixCal))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchNo = New System.Windows.Forms.Button()
        Me.CmdSearchGauge = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.txtGaugeNo = New System.Windows.Forms.TextBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.txtPartName = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtActualNoGoSize = New System.Windows.Forms.TextBox()
        Me.txtActualGoSize = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblRemark = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.lblDepartment = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblGoSize = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblNoGoSize = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblWearSize = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblCompSize = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblFrequency = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblGaugeDesc = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblModel = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblNew = New System.Windows.Forms.Label()
        Me.lblDueDate = New System.Windows.Forms.Label()
        Me.lblDocNo = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.fraTop1.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchNo
        '
        Me.cmdSearchNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchNo.Image = CType(resources.GetObject("cmdSearchNo.Image"), System.Drawing.Image)
        Me.cmdSearchNo.Location = New System.Drawing.Point(344, 16)
        Me.cmdSearchNo.Name = "cmdSearchNo"
        Me.cmdSearchNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchNo.TabIndex = 51
        Me.cmdSearchNo.TabStop = False
        Me.cmdSearchNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchNo, "Search")
        Me.cmdSearchNo.UseVisualStyleBackColor = False
        '
        'CmdSearchGauge
        '
        Me.CmdSearchGauge.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchGauge.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchGauge.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchGauge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchGauge.Image = CType(resources.GetObject("CmdSearchGauge.Image"), System.Drawing.Image)
        Me.CmdSearchGauge.Location = New System.Drawing.Point(344, 38)
        Me.CmdSearchGauge.Name = "CmdSearchGauge"
        Me.CmdSearchGauge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchGauge.Size = New System.Drawing.Size(23, 19)
        Me.CmdSearchGauge.TabIndex = 50
        Me.CmdSearchGauge.TabStop = False
        Me.CmdSearchGauge.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchGauge, "Search")
        Me.CmdSearchGauge.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(4, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 7
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(70, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 8
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(138, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 9
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(272, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 11
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(474, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 14
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(542, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 15
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(340, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 12
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(206, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 10
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(408, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 13
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.cmdSearchNo)
        Me.fraTop1.Controls.Add(Me.CmdSearchGauge)
        Me.fraTop1.Controls.Add(Me.txtGaugeNo)
        Me.fraTop1.Controls.Add(Me.txtNumber)
        Me.fraTop1.Controls.Add(Me.txtPartName)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.txtDate)
        Me.fraTop1.Controls.Add(Me.txtActualNoGoSize)
        Me.fraTop1.Controls.Add(Me.txtActualGoSize)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.lblRemark)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me._lblLabels_2)
        Me.fraTop1.Controls.Add(Me.lblDepartment)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.lblGoSize)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.lblNoGoSize)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.lblWearSize)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.lblCompSize)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.lblFrequency)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.lblGaugeDesc)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.lblModel)
        Me.fraTop1.Controls.Add(Me.Label15)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me._lblLabels_1)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.lblCustomer)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.lblNew)
        Me.fraTop1.Controls.Add(Me.lblDueDate)
        Me.fraTop1.Controls.Add(Me.lblDocNo)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, 0)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(613, 339)
        Me.fraTop1.TabIndex = 16
        Me.fraTop1.TabStop = False
        '
        'txtGaugeNo
        '
        Me.txtGaugeNo.AcceptsReturn = True
        Me.txtGaugeNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtGaugeNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGaugeNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGaugeNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGaugeNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGaugeNo.Location = New System.Drawing.Point(157, 40)
        Me.txtGaugeNo.MaxLength = 0
        Me.txtGaugeNo.Name = "txtGaugeNo"
        Me.txtGaugeNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGaugeNo.Size = New System.Drawing.Size(181, 19)
        Me.txtGaugeNo.TabIndex = 2
        '
        'txtNumber
        '
        Me.txtNumber.AcceptsReturn = True
        Me.txtNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumber.Location = New System.Drawing.Point(157, 16)
        Me.txtNumber.MaxLength = 0
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumber.Size = New System.Drawing.Size(181, 19)
        Me.txtNumber.TabIndex = 1
        '
        'txtPartName
        '
        Me.txtPartName.AcceptsReturn = True
        Me.txtPartName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPartName.Location = New System.Drawing.Point(156, 284)
        Me.txtPartName.MaxLength = 0
        Me.txtPartName.Name = "txtPartName"
        Me.txtPartName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartName.Size = New System.Drawing.Size(447, 19)
        Me.txtPartName.TabIndex = 5
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(156, 310)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(447, 19)
        Me.txtRemarks.TabIndex = 6
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(508, 12)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(95, 19)
        Me.txtDate.TabIndex = 0
        '
        'txtActualNoGoSize
        '
        Me.txtActualNoGoSize.AcceptsReturn = True
        Me.txtActualNoGoSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtActualNoGoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualNoGoSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActualNoGoSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualNoGoSize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtActualNoGoSize.Location = New System.Drawing.Point(508, 230)
        Me.txtActualNoGoSize.MaxLength = 0
        Me.txtActualNoGoSize.Name = "txtActualNoGoSize"
        Me.txtActualNoGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActualNoGoSize.Size = New System.Drawing.Size(95, 19)
        Me.txtActualNoGoSize.TabIndex = 4
        '
        'txtActualGoSize
        '
        Me.txtActualGoSize.AcceptsReturn = True
        Me.txtActualGoSize.BackColor = System.Drawing.SystemColors.Window
        Me.txtActualGoSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActualGoSize.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActualGoSize.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActualGoSize.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtActualGoSize.Location = New System.Drawing.Point(157, 230)
        Me.txtActualGoSize.MaxLength = 0
        Me.txtActualGoSize.Name = "txtActualGoSize"
        Me.txtActualGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActualGoSize.Size = New System.Drawing.Size(93, 19)
        Me.txtActualGoSize.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(59, 288)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Instrument Used"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRemark
        '
        Me.lblRemark.AutoSize = True
        Me.lblRemark.BackColor = System.Drawing.SystemColors.Control
        Me.lblRemark.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRemark.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemark.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRemark.Location = New System.Drawing.Point(16, 314)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRemark.Size = New System.Drawing.Size(107, 13)
        Me.lblRemark.TabIndex = 45
        Me.lblRemark.Text = "Calib. By / Remarks"
        Me.lblRemark.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(431, 16)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 44
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
        Me.Label7.Location = New System.Drawing.Point(8, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "Number"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(9, 40)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(58, 13)
        Me._lblLabels_2.TabIndex = 42
        Me._lblLabels_2.Text = "Gauge No"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDepartment
        '
        Me.lblDepartment.BackColor = System.Drawing.SystemColors.Control
        Me.lblDepartment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDepartment.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDepartment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDepartment.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDepartment.Location = New System.Drawing.Point(157, 136)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDepartment.Size = New System.Drawing.Size(447, 19)
        Me.lblDepartment.TabIndex = 41
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(9, 140)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(68, 13)
        Me.Label4.TabIndex = 40
        Me.Label4.Text = "Department"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblGoSize
        '
        Me.lblGoSize.BackColor = System.Drawing.SystemColors.Control
        Me.lblGoSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGoSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGoSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGoSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGoSize.Location = New System.Drawing.Point(157, 160)
        Me.lblGoSize.Name = "lblGoSize"
        Me.lblGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGoSize.Size = New System.Drawing.Size(93, 19)
        Me.lblGoSize.TabIndex = 39
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(9, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(45, 13)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "Go Size"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNoGoSize
        '
        Me.lblNoGoSize.BackColor = System.Drawing.SystemColors.Control
        Me.lblNoGoSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNoGoSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNoGoSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoGoSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNoGoSize.Location = New System.Drawing.Point(508, 160)
        Me.lblNoGoSize.Name = "lblNoGoSize"
        Me.lblNoGoSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNoGoSize.Size = New System.Drawing.Size(95, 19)
        Me.lblNoGoSize.TabIndex = 37
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(383, 164)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 36
        Me.Label6.Text = "NoGo Size"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWearSize
        '
        Me.lblWearSize.BackColor = System.Drawing.SystemColors.Control
        Me.lblWearSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWearSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWearSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWearSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWearSize.Location = New System.Drawing.Point(157, 182)
        Me.lblWearSize.Name = "lblWearSize"
        Me.lblWearSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWearSize.Size = New System.Drawing.Size(93, 19)
        Me.lblWearSize.TabIndex = 35
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(9, 184)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 34
        Me.Label9.Text = "Wear Size"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCompSize
        '
        Me.lblCompSize.BackColor = System.Drawing.SystemColors.Control
        Me.lblCompSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCompSize.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompSize.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompSize.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompSize.Location = New System.Drawing.Point(508, 182)
        Me.lblCompSize.Name = "lblCompSize"
        Me.lblCompSize.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompSize.Size = New System.Drawing.Size(95, 19)
        Me.lblCompSize.TabIndex = 33
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(383, 186)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(61, 13)
        Me.Label11.TabIndex = 32
        Me.Label11.Text = "Comp Size"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFrequency
        '
        Me.lblFrequency.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrequency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFrequency.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFrequency.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrequency.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFrequency.Location = New System.Drawing.Point(157, 206)
        Me.lblFrequency.Name = "lblFrequency"
        Me.lblFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFrequency.Size = New System.Drawing.Size(93, 19)
        Me.lblFrequency.TabIndex = 31
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(9, 208)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(59, 13)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "Frequency"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblGaugeDesc
        '
        Me.lblGaugeDesc.BackColor = System.Drawing.SystemColors.Control
        Me.lblGaugeDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblGaugeDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblGaugeDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGaugeDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblGaugeDesc.Location = New System.Drawing.Point(157, 64)
        Me.lblGaugeDesc.Name = "lblGaugeDesc"
        Me.lblGaugeDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblGaugeDesc.Size = New System.Drawing.Size(447, 19)
        Me.lblGaugeDesc.TabIndex = 29
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(9, 64)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(67, 13)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "Gauge Desc"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModel
        '
        Me.lblModel.BackColor = System.Drawing.SystemColors.Control
        Me.lblModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModel.Location = New System.Drawing.Point(157, 112)
        Me.lblModel.Name = "lblModel"
        Me.lblModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModel.Size = New System.Drawing.Size(447, 19)
        Me.lblModel.TabIndex = 27
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(9, 116)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(40, 13)
        Me.Label15.TabIndex = 26
        Me.Label15.Text = "Model"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(383, 234)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(94, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Actual NoGo Size"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(9, 234)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(79, 13)
        Me._lblLabels_1.TabIndex = 24
        Me._lblLabels_1.Text = "Actual Go Size"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(9, 92)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(59, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Customer "
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCustomer
        '
        Me.lblCustomer.BackColor = System.Drawing.SystemColors.Control
        Me.lblCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCustomer.Location = New System.Drawing.Point(157, 88)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustomer.Size = New System.Drawing.Size(447, 19)
        Me.lblCustomer.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(7, 256)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(116, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Calibration Due Date "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNew
        '
        Me.lblNew.BackColor = System.Drawing.SystemColors.Control
        Me.lblNew.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNew.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNew.Location = New System.Drawing.Point(392, 8)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNew.Size = New System.Drawing.Size(57, 17)
        Me.lblNew.TabIndex = 20
        Me.lblNew.Text = "0"
        '
        'lblDueDate
        '
        Me.lblDueDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblDueDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDueDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDueDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDueDate.Location = New System.Drawing.Point(157, 256)
        Me.lblDueDate.Name = "lblDueDate"
        Me.lblDueDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDueDate.Size = New System.Drawing.Size(93, 19)
        Me.lblDueDate.TabIndex = 19
        '
        'lblDocNo
        '
        Me.lblDocNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblDocNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDocNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDocNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDocNo.Location = New System.Drawing.Point(508, 40)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDocNo.Size = New System.Drawing.Size(93, 19)
        Me.lblDocNo.TabIndex = 18
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(460, 40)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(45, 13)
        Me.Label16.TabIndex = 17
        Me.Label16.Text = "Doc No"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 336)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(613, 51)
        Me.FraMovement.TabIndex = 47
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
        Me.lblMkey.TabIndex = 48
        Me.lblMkey.Text = "lblMkey"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(-178, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 49
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(614, 335)
        Me.SprdView.TabIndex = 49
        '
        'frmGaugeFixCal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(613, 387)
        Me.Controls.Add(Me.fraTop1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGaugeFixCal"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Gauge Fixture Calibration"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
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