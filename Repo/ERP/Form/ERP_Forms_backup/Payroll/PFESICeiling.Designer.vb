Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPFESICeiling
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
    Public WithEvents optContCeiling As System.Windows.Forms.RadioButton
    Public WithEvents optContBasic As System.Windows.Forms.RadioButton
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents txtESICeiling As System.Windows.Forms.TextBox
    Public WithEvents txtESIRate As System.Windows.Forms.TextBox
    Public WithEvents cboESIRound As System.Windows.Forms.ComboBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents FraESI As System.Windows.Forms.GroupBox
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents cboPFRound As System.Windows.Forms.ComboBox
    Public WithEvents txtPensionRate As System.Windows.Forms.TextBox
    Public WithEvents txtEPFRate As System.Windows.Forms.TextBox
    Public WithEvents txtPFRate As System.Windows.Forms.TextBox
    Public WithEvents txtPFCeiling As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblPFCeiling As System.Windows.Forms.Label
    Public WithEvents fraPF As System.Windows.Forms.GroupBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblWEF As System.Windows.Forms.Label
    Public WithEvents fraView As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents FraGridView As System.Windows.Forms.GroupBox
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPFESICeiling))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.fraView = New System.Windows.Forms.GroupBox()
        Me.optContCeiling = New System.Windows.Forms.RadioButton()
        Me.optContBasic = New System.Windows.Forms.RadioButton()
        Me.FraESI = New System.Windows.Forms.GroupBox()
        Me.txtESICeiling = New System.Windows.Forms.TextBox()
        Me.txtESIRate = New System.Windows.Forms.TextBox()
        Me.cboESIRound = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.fraPF = New System.Windows.Forms.GroupBox()
        Me.cboPFRound = New System.Windows.Forms.ComboBox()
        Me.txtPensionRate = New System.Windows.Forms.TextBox()
        Me.txtEPFRate = New System.Windows.Forms.TextBox()
        Me.txtPFRate = New System.Windows.Forms.TextBox()
        Me.txtPFCeiling = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPFCeiling = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblWEF = New System.Windows.Forms.Label()
        Me.FraGridView = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.fraView.SuspendLayout()
        Me.FraESI.SuspendLayout()
        Me.fraPF.SuspendLayout()
        Me.FraGridView.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(306, 10)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(260, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(50, 34)
        Me.cmdPrint.TabIndex = 29
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(410, 12)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(50, 34)
        Me.cmdClose.TabIndex = 32
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(360, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(50, 34)
        Me.CmdView.TabIndex = 31
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(102, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(50, 34)
        Me.CmdSave.TabIndex = 26
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
        Me.CmdDelete.Location = New System.Drawing.Point(210, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(50, 34)
        Me.CmdDelete.TabIndex = 28
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(52, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(50, 34)
        Me.CmdModify.TabIndex = 25
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
        Me.CmdAdd.Location = New System.Drawing.Point(2, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(50, 34)
        Me.CmdAdd.TabIndex = 24
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(152, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(57, 33)
        Me.cmdSavePrint.TabIndex = 27
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(310, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(50, 33)
        Me.cmdPreview.TabIndex = 30
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPreview, "Print Preview")
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'fraView
        '
        Me.fraView.BackColor = System.Drawing.SystemColors.Control
        Me.fraView.Controls.Add(Me.optContCeiling)
        Me.fraView.Controls.Add(Me.optContBasic)
        Me.fraView.Controls.Add(Me.cmdsearch)
        Me.fraView.Controls.Add(Me.FraESI)
        Me.fraView.Controls.Add(Me.txtWEF)
        Me.fraView.Controls.Add(Me.fraPF)
        Me.fraView.Controls.Add(Me.Label6)
        Me.fraView.Controls.Add(Me.lblWEF)
        Me.fraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraView.Location = New System.Drawing.Point(0, -4)
        Me.fraView.Name = "fraView"
        Me.fraView.Padding = New System.Windows.Forms.Padding(0)
        Me.fraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraView.Size = New System.Drawing.Size(463, 238)
        Me.fraView.TabIndex = 0
        Me.fraView.TabStop = False
        '
        'optContCeiling
        '
        Me.optContCeiling.AutoSize = True
        Me.optContCeiling.BackColor = System.Drawing.SystemColors.Control
        Me.optContCeiling.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContCeiling.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContCeiling.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContCeiling.Location = New System.Drawing.Point(300, 211)
        Me.optContCeiling.Name = "optContCeiling"
        Me.optContCeiling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContCeiling.Size = New System.Drawing.Size(56, 18)
        Me.optContCeiling.TabIndex = 36
        Me.optContCeiling.TabStop = True
        Me.optContCeiling.Text = "Ceiling"
        Me.optContCeiling.UseVisualStyleBackColor = False
        '
        'optContBasic
        '
        Me.optContBasic.AutoSize = True
        Me.optContBasic.BackColor = System.Drawing.SystemColors.Control
        Me.optContBasic.Checked = True
        Me.optContBasic.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContBasic.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContBasic.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContBasic.Location = New System.Drawing.Point(188, 211)
        Me.optContBasic.Name = "optContBasic"
        Me.optContBasic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContBasic.Size = New System.Drawing.Size(86, 18)
        Me.optContBasic.TabIndex = 35
        Me.optContBasic.TabStop = True
        Me.optContBasic.Text = "Basic Salary"
        Me.optContBasic.UseVisualStyleBackColor = False
        '
        'FraESI
        '
        Me.FraESI.BackColor = System.Drawing.SystemColors.Control
        Me.FraESI.Controls.Add(Me.txtESICeiling)
        Me.FraESI.Controls.Add(Me.txtESIRate)
        Me.FraESI.Controls.Add(Me.cboESIRound)
        Me.FraESI.Controls.Add(Me.Label9)
        Me.FraESI.Controls.Add(Me.Label8)
        Me.FraESI.Controls.Add(Me.Label5)
        Me.FraESI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraESI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraESI.Location = New System.Drawing.Point(236, 32)
        Me.FraESI.Name = "FraESI"
        Me.FraESI.Padding = New System.Windows.Forms.Padding(0)
        Me.FraESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraESI.Size = New System.Drawing.Size(219, 172)
        Me.FraESI.TabIndex = 15
        Me.FraESI.TabStop = False
        Me.FraESI.Text = "ESI"
        '
        'txtESICeiling
        '
        Me.txtESICeiling.AcceptsReturn = True
        Me.txtESICeiling.BackColor = System.Drawing.SystemColors.Window
        Me.txtESICeiling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESICeiling.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESICeiling.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESICeiling.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESICeiling.Location = New System.Drawing.Point(103, 20)
        Me.txtESICeiling.MaxLength = 0
        Me.txtESICeiling.Name = "txtESICeiling"
        Me.txtESICeiling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESICeiling.Size = New System.Drawing.Size(60, 20)
        Me.txtESICeiling.TabIndex = 17
        Me.txtESICeiling.Text = "6500.00"
        '
        'txtESIRate
        '
        Me.txtESIRate.AcceptsReturn = True
        Me.txtESIRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtESIRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtESIRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtESIRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtESIRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtESIRate.Location = New System.Drawing.Point(103, 44)
        Me.txtESIRate.MaxLength = 0
        Me.txtESIRate.Name = "txtESIRate"
        Me.txtESIRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtESIRate.Size = New System.Drawing.Size(60, 20)
        Me.txtESIRate.TabIndex = 19
        '
        'cboESIRound
        '
        Me.cboESIRound.BackColor = System.Drawing.SystemColors.Window
        Me.cboESIRound.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboESIRound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboESIRound.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboESIRound.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboESIRound.Location = New System.Drawing.Point(103, 70)
        Me.cboESIRound.Name = "cboESIRound"
        Me.cboESIRound.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboESIRound.Size = New System.Drawing.Size(77, 22)
        Me.cboESIRound.TabIndex = 21
        Me.cboESIRound.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(26, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(44, 14)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Ceiling :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(26, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(35, 14)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Rate :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(26, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(44, 14)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Round :"
        Me.Label5.Visible = False
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(180, 10)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(125, 20)
        Me.txtWEF.TabIndex = 2
        '
        'fraPF
        '
        Me.fraPF.BackColor = System.Drawing.SystemColors.Control
        Me.fraPF.Controls.Add(Me.cboPFRound)
        Me.fraPF.Controls.Add(Me.txtPensionRate)
        Me.fraPF.Controls.Add(Me.txtEPFRate)
        Me.fraPF.Controls.Add(Me.txtPFRate)
        Me.fraPF.Controls.Add(Me.txtPFCeiling)
        Me.fraPF.Controls.Add(Me.Label4)
        Me.fraPF.Controls.Add(Me.Label3)
        Me.fraPF.Controls.Add(Me.Label2)
        Me.fraPF.Controls.Add(Me.Label1)
        Me.fraPF.Controls.Add(Me.lblPFCeiling)
        Me.fraPF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPF.Location = New System.Drawing.Point(8, 32)
        Me.fraPF.Name = "fraPF"
        Me.fraPF.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPF.Size = New System.Drawing.Size(219, 172)
        Me.fraPF.TabIndex = 4
        Me.fraPF.TabStop = False
        Me.fraPF.Text = "PF"
        '
        'cboPFRound
        '
        Me.cboPFRound.BackColor = System.Drawing.SystemColors.Window
        Me.cboPFRound.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPFRound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPFRound.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPFRound.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPFRound.Location = New System.Drawing.Point(92, 118)
        Me.cboPFRound.Name = "cboPFRound"
        Me.cboPFRound.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPFRound.Size = New System.Drawing.Size(77, 22)
        Me.cboPFRound.TabIndex = 14
        Me.cboPFRound.Visible = False
        '
        'txtPensionRate
        '
        Me.txtPensionRate.AcceptsReturn = True
        Me.txtPensionRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPensionRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPensionRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPensionRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPensionRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPensionRate.Location = New System.Drawing.Point(92, 92)
        Me.txtPensionRate.MaxLength = 0
        Me.txtPensionRate.Name = "txtPensionRate"
        Me.txtPensionRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPensionRate.Size = New System.Drawing.Size(60, 20)
        Me.txtPensionRate.TabIndex = 12
        '
        'txtEPFRate
        '
        Me.txtEPFRate.AcceptsReturn = True
        Me.txtEPFRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEPFRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEPFRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEPFRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEPFRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEPFRate.Location = New System.Drawing.Point(92, 68)
        Me.txtEPFRate.MaxLength = 0
        Me.txtEPFRate.Name = "txtEPFRate"
        Me.txtEPFRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEPFRate.Size = New System.Drawing.Size(60, 20)
        Me.txtEPFRate.TabIndex = 10
        '
        'txtPFRate
        '
        Me.txtPFRate.AcceptsReturn = True
        Me.txtPFRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPFRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPFRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPFRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPFRate.Location = New System.Drawing.Point(92, 44)
        Me.txtPFRate.MaxLength = 0
        Me.txtPFRate.Name = "txtPFRate"
        Me.txtPFRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPFRate.Size = New System.Drawing.Size(60, 20)
        Me.txtPFRate.TabIndex = 8
        '
        'txtPFCeiling
        '
        Me.txtPFCeiling.AcceptsReturn = True
        Me.txtPFCeiling.BackColor = System.Drawing.SystemColors.Window
        Me.txtPFCeiling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPFCeiling.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPFCeiling.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPFCeiling.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPFCeiling.Location = New System.Drawing.Point(92, 20)
        Me.txtPFCeiling.MaxLength = 0
        Me.txtPFCeiling.Name = "txtPFCeiling"
        Me.txtPFCeiling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPFCeiling.Size = New System.Drawing.Size(60, 20)
        Me.txtPFCeiling.TabIndex = 6
        Me.txtPFCeiling.Text = "6500.00"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(34, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(44, 14)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Round :"
        Me.Label4.Visible = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(34, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(51, 25)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Pension Fund :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(34, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(31, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "EPF :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(34, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(35, 14)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Rate :"
        '
        'lblPFCeiling
        '
        Me.lblPFCeiling.AutoSize = True
        Me.lblPFCeiling.BackColor = System.Drawing.SystemColors.Control
        Me.lblPFCeiling.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPFCeiling.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPFCeiling.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPFCeiling.Location = New System.Drawing.Point(34, 20)
        Me.lblPFCeiling.Name = "lblPFCeiling"
        Me.lblPFCeiling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPFCeiling.Size = New System.Drawing.Size(44, 14)
        Me.lblPFCeiling.TabIndex = 5
        Me.lblPFCeiling.Text = "Ceiling :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(10, 211)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(147, 14)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Employer PF Contribution on :"
        '
        'lblWEF
        '
        Me.lblWEF.AutoSize = True
        Me.lblWEF.BackColor = System.Drawing.SystemColors.Control
        Me.lblWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWEF.Location = New System.Drawing.Point(68, 14)
        Me.lblWEF.Name = "lblWEF"
        Me.lblWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWEF.Size = New System.Drawing.Size(107, 14)
        Me.lblWEF.TabIndex = 1
        Me.lblWEF.Text = "With Effective From :"
        '
        'FraGridView
        '
        Me.FraGridView.BackColor = System.Drawing.SystemColors.Control
        Me.FraGridView.Controls.Add(Me.SprdView)
        Me.FraGridView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGridView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGridView.Location = New System.Drawing.Point(0, -4)
        Me.FraGridView.Name = "FraGridView"
        Me.FraGridView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGridView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGridView.Size = New System.Drawing.Size(463, 207)
        Me.FraGridView.TabIndex = 22
        Me.FraGridView.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(457, 197)
        Me.SprdView.TabIndex = 33
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdPrint)
        Me.Frame1.Controls.Add(Me.cmdClose)
        Me.Frame1.Controls.Add(Me.CmdView)
        Me.Frame1.Controls.Add(Me.CmdSave)
        Me.Frame1.Controls.Add(Me.CmdDelete)
        Me.Frame1.Controls.Add(Me.CmdModify)
        Me.Frame1.Controls.Add(Me.CmdAdd)
        Me.Frame1.Controls.Add(Me.cmdSavePrint)
        Me.Frame1.Controls.Add(Me.cmdPreview)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 229)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(463, 51)
        Me.Frame1.TabIndex = 23
        Me.Frame1.TabStop = False
        '
        'frmPFESICeiling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(464, 281)
        Me.Controls.Add(Me.fraView)
        Me.Controls.Add(Me.FraGridView)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(22, 92)
        Me.MaximizeBox = False
        Me.Name = "frmPFESICeiling"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "PF & ESI Ceiling"
        Me.fraView.ResumeLayout(False)
        Me.fraView.PerformLayout()
        Me.FraESI.ResumeLayout(False)
        Me.FraESI.PerformLayout()
        Me.fraPF.ResumeLayout(False)
        Me.fraPF.PerformLayout()
        Me.FraGridView.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class