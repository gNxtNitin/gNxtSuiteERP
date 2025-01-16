Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBonusCeiling
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
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents cboBonusRound As System.Windows.Forms.ComboBox
    Public WithEvents txtBonusRate As System.Windows.Forms.TextBox
    Public WithEvents txtBonusCeiling As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblPFCeiling As System.Windows.Forms.Label
    Public WithEvents fraPF As System.Windows.Forms.GroupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBonusCeiling))
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
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.fraPF = New System.Windows.Forms.GroupBox()
        Me.cboBonusRound = New System.Windows.Forms.ComboBox()
        Me.txtBonusRate = New System.Windows.Forms.TextBox()
        Me.txtBonusCeiling = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblPFCeiling = New System.Windows.Forms.Label()
        Me.lblWEF = New System.Windows.Forms.Label()
        Me.FraGridView = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.fraView.SuspendLayout()
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
        Me.cmdPrint.TabIndex = 18
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
        Me.cmdClose.TabIndex = 21
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
        Me.CmdView.TabIndex = 20
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
        Me.CmdSave.TabIndex = 15
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
        Me.CmdDelete.TabIndex = 17
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
        Me.CmdModify.TabIndex = 14
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
        Me.CmdAdd.TabIndex = 13
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
        Me.cmdSavePrint.TabIndex = 16
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
        Me.cmdPreview.TabIndex = 19
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPreview, "Print Preview")
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'fraView
        '
        Me.fraView.BackColor = System.Drawing.SystemColors.Control
        Me.fraView.Controls.Add(Me.cmdsearch)
        Me.fraView.Controls.Add(Me.txtWEF)
        Me.fraView.Controls.Add(Me.fraPF)
        Me.fraView.Controls.Add(Me.lblWEF)
        Me.fraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraView.Location = New System.Drawing.Point(0, -4)
        Me.fraView.Name = "fraView"
        Me.fraView.Padding = New System.Windows.Forms.Padding(0)
        Me.fraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraView.Size = New System.Drawing.Size(463, 207)
        Me.fraView.TabIndex = 0
        Me.fraView.TabStop = False
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
        Me.txtWEF.Size = New System.Drawing.Size(125, 19)
        Me.txtWEF.TabIndex = 2
        '
        'fraPF
        '
        Me.fraPF.BackColor = System.Drawing.SystemColors.Control
        Me.fraPF.Controls.Add(Me.cboBonusRound)
        Me.fraPF.Controls.Add(Me.txtBonusRate)
        Me.fraPF.Controls.Add(Me.txtBonusCeiling)
        Me.fraPF.Controls.Add(Me.Label4)
        Me.fraPF.Controls.Add(Me.Label1)
        Me.fraPF.Controls.Add(Me.lblPFCeiling)
        Me.fraPF.Font = New System.Drawing.Font("Arial", 8.83!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraPF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraPF.Location = New System.Drawing.Point(67, 41)
        Me.fraPF.Name = "fraPF"
        Me.fraPF.Padding = New System.Windows.Forms.Padding(0)
        Me.fraPF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraPF.Size = New System.Drawing.Size(277, 145)
        Me.fraPF.TabIndex = 4
        Me.fraPF.TabStop = False
        Me.fraPF.Text = "Bonus"
        '
        'cboBonusRound
        '
        Me.cboBonusRound.BackColor = System.Drawing.SystemColors.Window
        Me.cboBonusRound.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboBonusRound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBonusRound.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBonusRound.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboBonusRound.Location = New System.Drawing.Point(92, 70)
        Me.cboBonusRound.Name = "cboBonusRound"
        Me.cboBonusRound.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboBonusRound.Size = New System.Drawing.Size(77, 22)
        Me.cboBonusRound.TabIndex = 10
        Me.cboBonusRound.Visible = False
        '
        'txtBonusRate
        '
        Me.txtBonusRate.AcceptsReturn = True
        Me.txtBonusRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBonusRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBonusRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBonusRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonusRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBonusRate.Location = New System.Drawing.Point(92, 44)
        Me.txtBonusRate.MaxLength = 0
        Me.txtBonusRate.Name = "txtBonusRate"
        Me.txtBonusRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBonusRate.Size = New System.Drawing.Size(60, 19)
        Me.txtBonusRate.TabIndex = 8
        '
        'txtBonusCeiling
        '
        Me.txtBonusCeiling.AcceptsReturn = True
        Me.txtBonusCeiling.BackColor = System.Drawing.SystemColors.Window
        Me.txtBonusCeiling.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBonusCeiling.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBonusCeiling.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBonusCeiling.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBonusCeiling.Location = New System.Drawing.Point(92, 20)
        Me.txtBonusCeiling.MaxLength = 0
        Me.txtBonusCeiling.Name = "txtBonusCeiling"
        Me.txtBonusCeiling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBonusCeiling.Size = New System.Drawing.Size(60, 19)
        Me.txtBonusCeiling.TabIndex = 6
        Me.txtBonusCeiling.Text = "6500.00"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(34, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(44, 14)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Round :"
        Me.Label4.Visible = False
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
        Me.FraGridView.TabIndex = 11
        Me.FraGridView.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(457, 197)
        Me.SprdView.TabIndex = 22
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
        Me.Frame1.Location = New System.Drawing.Point(0, 198)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(463, 51)
        Me.Frame1.TabIndex = 12
        Me.Frame1.TabStop = False
        '
        'frmBonusCeiling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(465, 252)
        Me.Controls.Add(Me.fraView)
        Me.Controls.Add(Me.FraGridView)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(22, 92)
        Me.MaximizeBox = False
        Me.Name = "frmBonusCeiling"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Bonus Ceiling"
        Me.fraView.ResumeLayout(False)
        Me.fraView.PerformLayout()
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