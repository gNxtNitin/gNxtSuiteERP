Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmProductPackingMaster
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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents txtUOM As System.Windows.Forms.TextBox
    Public WithEvents txtProductName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchProductCode As System.Windows.Forms.Button
    Public WithEvents txtProductCode As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents fraBase As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdAmend As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductPackingMaster))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtUOM = New System.Windows.Forms.TextBox()
        Me.txtProductName = New System.Windows.Forms.TextBox()
        Me.cmdSearchProductCode = New System.Windows.Forms.Button()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdAmend = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.txtInnerUOM = New System.Windows.Forms.TextBox()
        Me.txtInnerName = New System.Windows.Forms.TextBox()
        Me.cmdSearchInnerCode = New System.Windows.Forms.Button()
        Me.txtInnerBoxCode = New System.Windows.Forms.TextBox()
        Me.txtOuterUOM = New System.Windows.Forms.TextBox()
        Me.txtOuterName = New System.Windows.Forms.TextBox()
        Me.cmdSearchOuterCode = New System.Windows.Forms.Button()
        Me.txtOuterBoxCode = New System.Windows.Forms.TextBox()
        Me.fraBase = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtOuter_UOM_StdQty = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtOuter_IB_StdQty = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.frmRegdDealer = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInnerStdQty = New System.Windows.Forms.TextBox()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.fraBase.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.frmRegdDealer.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtUOM
        '
        Me.txtUOM.AcceptsReturn = True
        Me.txtUOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUOM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUOM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUOM.Location = New System.Drawing.Point(566, 13)
        Me.txtUOM.MaxLength = 0
        Me.txtUOM.Name = "txtUOM"
        Me.txtUOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUOM.Size = New System.Drawing.Size(65, 20)
        Me.txtUOM.TabIndex = 8
        Me.txtUOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtUOM, "Press F1 For Help")
        '
        'txtProductName
        '
        Me.txtProductName.AcceptsReturn = True
        Me.txtProductName.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductName.Location = New System.Drawing.Point(212, 13)
        Me.txtProductName.MaxLength = 0
        Me.txtProductName.Name = "txtProductName"
        Me.txtProductName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductName.Size = New System.Drawing.Size(353, 20)
        Me.txtProductName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtProductName, "Press F1 For Help")
        '
        'cmdSearchProductCode
        '
        Me.cmdSearchProductCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProductCode.Image = CType(resources.GetObject("cmdSearchProductCode.Image"), System.Drawing.Image)
        Me.cmdSearchProductCode.Location = New System.Drawing.Point(184, 13)
        Me.cmdSearchProductCode.Name = "cmdSearchProductCode"
        Me.cmdSearchProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProductCode.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchProductCode.TabIndex = 1
        Me.cmdSearchProductCode.TabStop = False
        Me.cmdSearchProductCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProductCode, "Search")
        Me.cmdSearchProductCode.UseVisualStyleBackColor = False
        '
        'txtProductCode
        '
        Me.txtProductCode.AcceptsReturn = True
        Me.txtProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductCode.Location = New System.Drawing.Point(102, 13)
        Me.txtProductCode.MaxLength = 0
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.Size = New System.Drawing.Size(81, 20)
        Me.txtProductCode.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtProductCode, "Press F1 For Help")
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(636, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 34)
        Me.CmdClose.TabIndex = 21
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
        Me.CmdView.Location = New System.Drawing.Point(570, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 34)
        Me.CmdView.TabIndex = 20
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(504, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 34)
        Me.CmdPreview.TabIndex = 19
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(438, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdPrint.TabIndex = 18
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(372, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 34)
        Me.CmdDelete.TabIndex = 17
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(306, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdSavePrint.TabIndex = 16
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdAmend
        '
        Me.cmdAmend.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmend.Image = CType(resources.GetObject("cmdAmend.Image"), System.Drawing.Image)
        Me.cmdAmend.Location = New System.Drawing.Point(240, 10)
        Me.cmdAmend.Name = "cmdAmend"
        Me.cmdAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmend.Size = New System.Drawing.Size(67, 34)
        Me.cmdAmend.TabIndex = 28
        Me.cmdAmend.Text = "&Amendment"
        Me.cmdAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAmend, "Modify Record")
        Me.cmdAmend.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(174, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 34)
        Me.CmdSave.TabIndex = 15
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
        Me.CmdModify.Location = New System.Drawing.Point(108, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 34)
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
        Me.CmdAdd.Location = New System.Drawing.Point(42, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 34)
        Me.CmdAdd.TabIndex = 13
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'txtInnerUOM
        '
        Me.txtInnerUOM.AcceptsReturn = True
        Me.txtInnerUOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtInnerUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInnerUOM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInnerUOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInnerUOM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInnerUOM.Location = New System.Drawing.Point(656, 48)
        Me.txtInnerUOM.MaxLength = 0
        Me.txtInnerUOM.Name = "txtInnerUOM"
        Me.txtInnerUOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInnerUOM.Size = New System.Drawing.Size(65, 20)
        Me.txtInnerUOM.TabIndex = 37
        Me.txtInnerUOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtInnerUOM, "Press F1 For Help")
        '
        'txtInnerName
        '
        Me.txtInnerName.AcceptsReturn = True
        Me.txtInnerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtInnerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInnerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInnerName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInnerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInnerName.Location = New System.Drawing.Point(302, 48)
        Me.txtInnerName.MaxLength = 0
        Me.txtInnerName.Name = "txtInnerName"
        Me.txtInnerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInnerName.Size = New System.Drawing.Size(353, 20)
        Me.txtInnerName.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.txtInnerName, "Press F1 For Help")
        '
        'cmdSearchInnerCode
        '
        Me.cmdSearchInnerCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchInnerCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchInnerCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchInnerCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchInnerCode.Image = CType(resources.GetObject("cmdSearchInnerCode.Image"), System.Drawing.Image)
        Me.cmdSearchInnerCode.Location = New System.Drawing.Point(274, 48)
        Me.cmdSearchInnerCode.Name = "cmdSearchInnerCode"
        Me.cmdSearchInnerCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchInnerCode.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchInnerCode.TabIndex = 35
        Me.cmdSearchInnerCode.TabStop = False
        Me.cmdSearchInnerCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchInnerCode, "Search")
        Me.cmdSearchInnerCode.UseVisualStyleBackColor = False
        '
        'txtInnerBoxCode
        '
        Me.txtInnerBoxCode.AcceptsReturn = True
        Me.txtInnerBoxCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtInnerBoxCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInnerBoxCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInnerBoxCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInnerBoxCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInnerBoxCode.Location = New System.Drawing.Point(190, 48)
        Me.txtInnerBoxCode.MaxLength = 0
        Me.txtInnerBoxCode.Name = "txtInnerBoxCode"
        Me.txtInnerBoxCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInnerBoxCode.Size = New System.Drawing.Size(81, 20)
        Me.txtInnerBoxCode.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.txtInnerBoxCode, "Press F1 For Help")
        '
        'txtOuterUOM
        '
        Me.txtOuterUOM.AcceptsReturn = True
        Me.txtOuterUOM.BackColor = System.Drawing.SystemColors.Window
        Me.txtOuterUOM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOuterUOM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOuterUOM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOuterUOM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOuterUOM.Location = New System.Drawing.Point(656, 48)
        Me.txtOuterUOM.MaxLength = 0
        Me.txtOuterUOM.Name = "txtOuterUOM"
        Me.txtOuterUOM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOuterUOM.Size = New System.Drawing.Size(65, 20)
        Me.txtOuterUOM.TabIndex = 37
        Me.txtOuterUOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtOuterUOM, "Press F1 For Help")
        '
        'txtOuterName
        '
        Me.txtOuterName.AcceptsReturn = True
        Me.txtOuterName.BackColor = System.Drawing.SystemColors.Window
        Me.txtOuterName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOuterName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOuterName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOuterName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOuterName.Location = New System.Drawing.Point(302, 48)
        Me.txtOuterName.MaxLength = 0
        Me.txtOuterName.Name = "txtOuterName"
        Me.txtOuterName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOuterName.Size = New System.Drawing.Size(353, 20)
        Me.txtOuterName.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.txtOuterName, "Press F1 For Help")
        '
        'cmdSearchOuterCode
        '
        Me.cmdSearchOuterCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchOuterCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchOuterCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchOuterCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchOuterCode.Image = CType(resources.GetObject("cmdSearchOuterCode.Image"), System.Drawing.Image)
        Me.cmdSearchOuterCode.Location = New System.Drawing.Point(274, 48)
        Me.cmdSearchOuterCode.Name = "cmdSearchOuterCode"
        Me.cmdSearchOuterCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchOuterCode.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchOuterCode.TabIndex = 35
        Me.cmdSearchOuterCode.TabStop = False
        Me.cmdSearchOuterCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchOuterCode, "Search")
        Me.cmdSearchOuterCode.UseVisualStyleBackColor = False
        '
        'txtOuterBoxCode
        '
        Me.txtOuterBoxCode.AcceptsReturn = True
        Me.txtOuterBoxCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtOuterBoxCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOuterBoxCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOuterBoxCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOuterBoxCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOuterBoxCode.Location = New System.Drawing.Point(190, 48)
        Me.txtOuterBoxCode.MaxLength = 0
        Me.txtOuterBoxCode.Name = "txtOuterBoxCode"
        Me.txtOuterBoxCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOuterBoxCode.Size = New System.Drawing.Size(81, 20)
        Me.txtOuterBoxCode.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.txtOuterBoxCode, "Press F1 For Help")
        '
        'fraBase
        '
        Me.fraBase.BackColor = System.Drawing.SystemColors.Control
        Me.fraBase.Controls.Add(Me.GroupBox1)
        Me.fraBase.Controls.Add(Me.frmRegdDealer)
        Me.fraBase.Controls.Add(Me.txtUOM)
        Me.fraBase.Controls.Add(Me.txtProductName)
        Me.fraBase.Controls.Add(Me.cmdSearchProductCode)
        Me.fraBase.Controls.Add(Me.txtProductCode)
        Me.fraBase.Controls.Add(Me.Label1)
        Me.fraBase.Controls.Add(Me.SprdMain)
        Me.fraBase.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBase.Location = New System.Drawing.Point(0, -4)
        Me.fraBase.Name = "fraBase"
        Me.fraBase.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBase.Size = New System.Drawing.Size(751, 441)
        Me.fraBase.TabIndex = 23
        Me.fraBase.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.txtOuter_UOM_StdQty)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtOuterUOM)
        Me.GroupBox1.Controls.Add(Me.txtOuterName)
        Me.GroupBox1.Controls.Add(Me.cmdSearchOuterCode)
        Me.GroupBox1.Controls.Add(Me.txtOuterBoxCode)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtOuter_IB_StdQty)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(1, 119)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(748, 76)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Outer Boxes Details"
        '
        'txtOuter_UOM_StdQty
        '
        Me.txtOuter_UOM_StdQty.AcceptsReturn = True
        Me.txtOuter_UOM_StdQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtOuter_UOM_StdQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOuter_UOM_StdQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOuter_UOM_StdQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOuter_UOM_StdQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOuter_UOM_StdQty.Location = New System.Drawing.Point(656, 18)
        Me.txtOuter_UOM_StdQty.MaxLength = 0
        Me.txtOuter_UOM_StdQty.Name = "txtOuter_UOM_StdQty"
        Me.txtOuter_UOM_StdQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOuter_UOM_StdQty.Size = New System.Drawing.Size(61, 22)
        Me.txtOuter_UOM_StdQty.TabIndex = 39
        Me.txtOuter_UOM_StdQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(513, 21)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(142, 14)
        Me.Label13.TabIndex = 40
        Me.Label13.Text = "Nos of UOM per Outer Box :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(64, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(122, 14)
        Me.Label6.TabIndex = 38
        Me.Label6.Text = "Packing Box Item Code :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtOuter_IB_StdQty
        '
        Me.txtOuter_IB_StdQty.AcceptsReturn = True
        Me.txtOuter_IB_StdQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtOuter_IB_StdQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOuter_IB_StdQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOuter_IB_StdQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOuter_IB_StdQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOuter_IB_StdQty.Location = New System.Drawing.Point(190, 18)
        Me.txtOuter_IB_StdQty.MaxLength = 0
        Me.txtOuter_IB_StdQty.Name = "txtOuter_IB_StdQty"
        Me.txtOuter_IB_StdQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOuter_IB_StdQty.Size = New System.Drawing.Size(61, 22)
        Me.txtOuter_IB_StdQty.TabIndex = 3
        Me.txtOuter_IB_StdQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(-58, 18)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(52, 13)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "CGST % :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(-58, 42)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(51, 13)
        Me.Label10.TabIndex = 30
        Me.Label10.Text = "SGST % :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(-62, 66)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(48, 13)
        Me.Label11.TabIndex = 29
        Me.Label11.Text = "IGST % :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(9, 21)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(177, 14)
        Me.Label12.TabIndex = 33
        Me.Label12.Text = "Nos of Inner Boxes per Outer Box :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmRegdDealer
        '
        Me.frmRegdDealer.BackColor = System.Drawing.SystemColors.Control
        Me.frmRegdDealer.Controls.Add(Me.txtInnerUOM)
        Me.frmRegdDealer.Controls.Add(Me.txtInnerName)
        Me.frmRegdDealer.Controls.Add(Me.cmdSearchInnerCode)
        Me.frmRegdDealer.Controls.Add(Me.txtInnerBoxCode)
        Me.frmRegdDealer.Controls.Add(Me.Label2)
        Me.frmRegdDealer.Controls.Add(Me.txtInnerStdQty)
        Me.frmRegdDealer.Controls.Add(Me._lblLabels_2)
        Me.frmRegdDealer.Controls.Add(Me._lblLabels_3)
        Me.frmRegdDealer.Controls.Add(Me._lblLabels_4)
        Me.frmRegdDealer.Controls.Add(Me.Label3)
        Me.frmRegdDealer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmRegdDealer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frmRegdDealer.Location = New System.Drawing.Point(2, 43)
        Me.frmRegdDealer.Name = "frmRegdDealer"
        Me.frmRegdDealer.Padding = New System.Windows.Forms.Padding(0)
        Me.frmRegdDealer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frmRegdDealer.Size = New System.Drawing.Size(748, 75)
        Me.frmRegdDealer.TabIndex = 39
        Me.frmRegdDealer.TabStop = False
        Me.frmRegdDealer.Text = "Inner Boxes Details"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(64, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(122, 14)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Packing Box Item Code :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtInnerStdQty
        '
        Me.txtInnerStdQty.AcceptsReturn = True
        Me.txtInnerStdQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtInnerStdQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInnerStdQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInnerStdQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInnerStdQty.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInnerStdQty.Location = New System.Drawing.Point(190, 18)
        Me.txtInnerStdQty.MaxLength = 0
        Me.txtInnerStdQty.Name = "txtInnerStdQty"
        Me.txtInnerStdQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInnerStdQty.Size = New System.Drawing.Size(61, 22)
        Me.txtInnerStdQty.TabIndex = 3
        Me.txtInnerStdQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(-58, 18)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(52, 13)
        Me._lblLabels_2.TabIndex = 31
        Me._lblLabels_2.Text = "CGST % :"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = True
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(-58, 42)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(51, 13)
        Me._lblLabels_3.TabIndex = 30
        Me._lblLabels_3.Text = "SGST % :"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.AutoSize = True
        Me._lblLabels_4.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_4.Location = New System.Drawing.Point(-62, 66)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(48, 13)
        Me._lblLabels_4.TabIndex = 29
        Me._lblLabels_4.Text = "IGST % :"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(47, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(139, 14)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Nos of UOM per Inner Box :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(2, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(78, 14)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Product Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(1, 198)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(747, 238)
        Me.SprdMain.TabIndex = 38
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(751, 435)
        Me.SprdView.TabIndex = 24
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.CmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdAmend)
        Me.Frame3.Controls.Add(Me.CmdSave)
        Me.Frame3.Controls.Add(Me.CmdModify)
        Me.Frame3.Controls.Add(Me.CmdAdd)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 432)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(751, 47)
        Me.Frame3.TabIndex = 22
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(592, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 22
        '
        'frmProductPackingMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(751, 479)
        Me.Controls.Add(Me.fraBase)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmProductPackingMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Product Packing Standard Master"
        Me.fraBase.ResumeLayout(False)
        Me.fraBase.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.frmRegdDealer.ResumeLayout(False)
        Me.frmRegdDealer.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents frmRegdDealer As GroupBox
    Public WithEvents txtInnerStdQty As TextBox
    Public WithEvents _lblLabels_2 As Label
    Public WithEvents _lblLabels_3 As Label
    Public WithEvents _lblLabels_4 As Label
    Public WithEvents txtInnerUOM As TextBox
    Public WithEvents txtInnerName As TextBox
    Public WithEvents cmdSearchInnerCode As Button
    Public WithEvents txtInnerBoxCode As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents txtOuterUOM As TextBox
    Public WithEvents txtOuterName As TextBox
    Public WithEvents cmdSearchOuterCode As Button
    Public WithEvents txtOuterBoxCode As TextBox
    Public WithEvents Label6 As Label
    Public WithEvents txtOuter_IB_StdQty As TextBox
    Public WithEvents Label9 As Label
    Public WithEvents Label10 As Label
    Public WithEvents Label11 As Label
    Public WithEvents Label12 As Label
    Public WithEvents txtOuter_UOM_StdQty As TextBox
    Public WithEvents Label13 As Label
#End Region
End Class