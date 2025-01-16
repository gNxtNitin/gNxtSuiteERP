Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBSGroup
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
    Public WithEvents _OptStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtSeqNo As System.Windows.Forms.TextBox
    Public WithEvents txtScheduleNo As System.Windows.Forms.TextBox
    Public WithEvents CmdSearchGroup As System.Windows.Forms.Button
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtSubGroupName As System.Windows.Forms.TextBox
    Public WithEvents TxtGroupName As System.Windows.Forms.TextBox
    Public WithEvents CboAcctType As System.Windows.Forms.ComboBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents _ChkPrintIn_3 As System.Windows.Forms.CheckBox
    Public WithEvents _ChkPrintIn_2 As System.Windows.Forms.CheckBox
    Public WithEvents _ChkPrintIn_1 As System.Windows.Forms.CheckBox
    Public WithEvents _ChkPrintIn_0 As System.Windows.Forms.CheckBox
    Public WithEvents FraPrintIn As System.Windows.Forms.GroupBox
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents ADataMain As VB6.ADODC
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents ChkPrintIn As VB6.CheckBoxArray
    Public WithEvents OptStatus As VB6.RadioButtonArray
    Public WithEvents lblLabels As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBSGroup))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdSearchGroup = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptStatus_1 = New System.Windows.Forms.RadioButton()
        Me._OptStatus_0 = New System.Windows.Forms.RadioButton()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.txtSeqNo = New System.Windows.Forms.TextBox()
        Me.txtScheduleNo = New System.Windows.Forms.TextBox()
        Me.TxtSubGroupName = New System.Windows.Forms.TextBox()
        Me.TxtGroupName = New System.Windows.Forms.TextBox()
        Me.CboAcctType = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.FraPrintIn = New System.Windows.Forms.GroupBox()
        Me._ChkPrintIn_3 = New System.Windows.Forms.CheckBox()
        Me._ChkPrintIn_2 = New System.Windows.Forms.CheckBox()
        Me._ChkPrintIn_1 = New System.Windows.Forms.CheckBox()
        Me._ChkPrintIn_0 = New System.Windows.Forms.CheckBox()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.ADataMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.ChkPrintIn = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.OptStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.FraView.SuspendLayout()
        Me.FraPrintIn.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChkPrintIn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdSearchGroup
        '
        Me.CmdSearchGroup.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdSearchGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSearchGroup.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSearchGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSearchGroup.Image = CType(resources.GetObject("CmdSearchGroup.Image"), System.Drawing.Image)
        Me.CmdSearchGroup.Location = New System.Drawing.Point(432, 50)
        Me.CmdSearchGroup.Name = "CmdSearchGroup"
        Me.CmdSearchGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSearchGroup.Size = New System.Drawing.Size(29, 19)
        Me.CmdSearchGroup.TabIndex = 5
        Me.CmdSearchGroup.TabStop = False
        Me.CmdSearchGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSearchGroup, "Search Under Group Name")
        Me.CmdSearchGroup.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(432, 16)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search Group Name")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(186, 16)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 34)
        Me.cmdSavePrint.TabIndex = 23
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
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(366, 16)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 34)
        Me.CmdPreview.TabIndex = 26
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(306, 16)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 34)
        Me.cmdPrint.TabIndex = 25
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print ")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdAdd.Location = New System.Drawing.Point(6, 16)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(60, 34)
        Me.CmdAdd.TabIndex = 20
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdModify.Location = New System.Drawing.Point(66, 16)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(60, 34)
        Me.CmdModify.TabIndex = 21
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdSave.Location = New System.Drawing.Point(126, 16)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 34)
        Me.CmdSave.TabIndex = 22
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
        Me.CmdDelete.Location = New System.Drawing.Point(246, 16)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(60, 34)
        Me.CmdDelete.TabIndex = 24
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdView.Location = New System.Drawing.Point(426, 16)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(60, 34)
        Me.CmdView.TabIndex = 27
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdClose.Location = New System.Drawing.Point(486, 16)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 28
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._OptStatus_1)
        Me.Frame1.Controls.Add(Me._OptStatus_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(436, 148)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(115, 45)
        Me.Frame1.TabIndex = 30
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Status"
        '
        '_OptStatus_1
        '
        Me._OptStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptStatus.SetIndex(Me._OptStatus_1, CType(1, Short))
        Me._OptStatus_1.Location = New System.Drawing.Point(12, 30)
        Me._OptStatus_1.Name = "_OptStatus_1"
        Me._OptStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_1.Size = New System.Drawing.Size(64, 15)
        Me._OptStatus_1.TabIndex = 32
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
        Me._OptStatus_0.Location = New System.Drawing.Point(12, 16)
        Me._OptStatus_0.Name = "_OptStatus_0"
        Me._OptStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptStatus_0.Size = New System.Drawing.Size(64, 15)
        Me._OptStatus_0.TabIndex = 31
        Me._OptStatus_0.TabStop = True
        Me._OptStatus_0.Text = "Open"
        Me._OptStatus_0.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.txtSeqNo)
        Me.FraView.Controls.Add(Me.txtScheduleNo)
        Me.FraView.Controls.Add(Me.CmdSearchGroup)
        Me.FraView.Controls.Add(Me.cmdsearch)
        Me.FraView.Controls.Add(Me.TxtSubGroupName)
        Me.FraView.Controls.Add(Me.TxtGroupName)
        Me.FraView.Controls.Add(Me.CboAcctType)
        Me.FraView.Controls.Add(Me.Label2)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Controls.Add(Me._lblLabels_3)
        Me.FraView.Controls.Add(Me._lblLabels_1)
        Me.FraView.Controls.Add(Me._lblLabels_0)
        Me.FraView.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -2)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(551, 151)
        Me.FraView.TabIndex = 0
        Me.FraView.TabStop = False
        '
        'txtSeqNo
        '
        Me.txtSeqNo.AcceptsReturn = True
        Me.txtSeqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeqNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeqNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSeqNo.Location = New System.Drawing.Point(378, 120)
        Me.txtSeqNo.MaxLength = 0
        Me.txtSeqNo.Name = "txtSeqNo"
        Me.txtSeqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeqNo.Size = New System.Drawing.Size(53, 22)
        Me.txtSeqNo.TabIndex = 11
        '
        'txtScheduleNo
        '
        Me.txtScheduleNo.AcceptsReturn = True
        Me.txtScheduleNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtScheduleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScheduleNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScheduleNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScheduleNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtScheduleNo.Location = New System.Drawing.Point(174, 120)
        Me.txtScheduleNo.MaxLength = 0
        Me.txtScheduleNo.Name = "txtScheduleNo"
        Me.txtScheduleNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScheduleNo.Size = New System.Drawing.Size(91, 22)
        Me.txtScheduleNo.TabIndex = 10
        '
        'TxtSubGroupName
        '
        Me.TxtSubGroupName.AcceptsReturn = True
        Me.TxtSubGroupName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtSubGroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtSubGroupName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtSubGroupName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSubGroupName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtSubGroupName.Location = New System.Drawing.Point(174, 50)
        Me.TxtSubGroupName.MaxLength = 0
        Me.TxtSubGroupName.Name = "TxtSubGroupName"
        Me.TxtSubGroupName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtSubGroupName.Size = New System.Drawing.Size(257, 22)
        Me.TxtSubGroupName.TabIndex = 4
        '
        'TxtGroupName
        '
        Me.TxtGroupName.AcceptsReturn = True
        Me.TxtGroupName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGroupName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGroupName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGroupName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGroupName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtGroupName.Location = New System.Drawing.Point(174, 16)
        Me.TxtGroupName.MaxLength = 0
        Me.TxtGroupName.Name = "TxtGroupName"
        Me.TxtGroupName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGroupName.Size = New System.Drawing.Size(257, 22)
        Me.TxtGroupName.TabIndex = 2
        '
        'CboAcctType
        '
        Me.CboAcctType.BackColor = System.Drawing.SystemColors.Window
        Me.CboAcctType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboAcctType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboAcctType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboAcctType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.CboAcctType.Location = New System.Drawing.Point(174, 84)
        Me.CboAcctType.Name = "CboAcctType"
        Me.CboAcctType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboAcctType.Size = New System.Drawing.Size(257, 21)
        Me.CboAcctType.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(318, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Seq No. :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(32, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Schedule No :"
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = True
        Me._lblLabels_3.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_3, CType(3, Short))
        Me._lblLabels_3.Location = New System.Drawing.Point(32, 90)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(80, 13)
        Me._lblLabels_3.TabIndex = 7
        Me._lblLabels_3.Text = "Account Type :"
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(32, 54)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(113, 13)
        Me._lblLabels_1.TabIndex = 6
        Me._lblLabels_1.Text = "Parent Group Name :"
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(32, 20)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(43, 13)
        Me._lblLabels_0.TabIndex = 1
        Me._lblLabels_0.Text = "Name :"
        '
        'FraPrintIn
        '
        Me.FraPrintIn.BackColor = System.Drawing.SystemColors.Control
        Me.FraPrintIn.Controls.Add(Me._ChkPrintIn_3)
        Me.FraPrintIn.Controls.Add(Me._ChkPrintIn_2)
        Me.FraPrintIn.Controls.Add(Me._ChkPrintIn_1)
        Me.FraPrintIn.Controls.Add(Me._ChkPrintIn_0)
        Me.FraPrintIn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPrintIn.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPrintIn.Location = New System.Drawing.Point(0, 148)
        Me.FraPrintIn.Name = "FraPrintIn"
        Me.FraPrintIn.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPrintIn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPrintIn.Size = New System.Drawing.Size(437, 45)
        Me.FraPrintIn.TabIndex = 14
        Me.FraPrintIn.TabStop = False
        Me.FraPrintIn.Text = "Print In"
        '
        '_ChkPrintIn_3
        '
        Me._ChkPrintIn_3.BackColor = System.Drawing.SystemColors.Control
        Me._ChkPrintIn_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._ChkPrintIn_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ChkPrintIn_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintIn.SetIndex(Me._ChkPrintIn_3, CType(3, Short))
        Me._ChkPrintIn_3.Location = New System.Drawing.Point(342, 20)
        Me._ChkPrintIn_3.Name = "_ChkPrintIn_3"
        Me._ChkPrintIn_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ChkPrintIn_3.Size = New System.Drawing.Size(91, 15)
        Me._ChkPrintIn_3.TabIndex = 18
        Me._ChkPrintIn_3.Text = "Fund Flow"
        Me._ChkPrintIn_3.UseVisualStyleBackColor = False
        '
        '_ChkPrintIn_2
        '
        Me._ChkPrintIn_2.BackColor = System.Drawing.SystemColors.Control
        Me._ChkPrintIn_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._ChkPrintIn_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ChkPrintIn_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintIn.SetIndex(Me._ChkPrintIn_2, CType(2, Short))
        Me._ChkPrintIn_2.Location = New System.Drawing.Point(254, 20)
        Me._ChkPrintIn_2.Name = "_ChkPrintIn_2"
        Me._ChkPrintIn_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ChkPrintIn_2.Size = New System.Drawing.Size(81, 15)
        Me._ChkPrintIn_2.TabIndex = 17
        Me._ChkPrintIn_2.Text = "Schedule"
        Me._ChkPrintIn_2.UseVisualStyleBackColor = False
        '
        '_ChkPrintIn_1
        '
        Me._ChkPrintIn_1.BackColor = System.Drawing.SystemColors.Control
        Me._ChkPrintIn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._ChkPrintIn_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ChkPrintIn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintIn.SetIndex(Me._ChkPrintIn_1, CType(1, Short))
        Me._ChkPrintIn_1.Location = New System.Drawing.Point(122, 20)
        Me._ChkPrintIn_1.Name = "_ChkPrintIn_1"
        Me._ChkPrintIn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ChkPrintIn_1.Size = New System.Drawing.Size(119, 15)
        Me._ChkPrintIn_1.TabIndex = 16
        Me._ChkPrintIn_1.Text = "Profit && Loss A/c"
        Me._ChkPrintIn_1.UseVisualStyleBackColor = False
        '
        '_ChkPrintIn_0
        '
        Me._ChkPrintIn_0.BackColor = System.Drawing.SystemColors.Control
        Me._ChkPrintIn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._ChkPrintIn_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._ChkPrintIn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPrintIn.SetIndex(Me._ChkPrintIn_0, CType(0, Short))
        Me._ChkPrintIn_0.Location = New System.Drawing.Point(4, 20)
        Me._ChkPrintIn_0.Name = "_ChkPrintIn_0"
        Me._ChkPrintIn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._ChkPrintIn_0.Size = New System.Drawing.Size(133, 17)
        Me._ChkPrintIn_0.TabIndex = 15
        Me._ChkPrintIn_0.Text = "Balance Sheet"
        Me._ChkPrintIn_0.UseVisualStyleBackColor = False
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 188)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(551, 57)
        Me.FraMovement.TabIndex = 19
        Me.FraMovement.TabStop = False
        '
        'ADataMain
        '
        Me.ADataMain.BackColor = System.Drawing.SystemColors.Window
        Me.ADataMain.CommandTimeout = 0
        Me.ADataMain.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataMain.ConnectionString = Nothing
        Me.ADataMain.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataMain.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataMain.Location = New System.Drawing.Point(0, 0)
        Me.ADataMain.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataMain.Name = "ADataMain"
        Me.ADataMain.Size = New System.Drawing.Size(231, 39)
        Me.ADataMain.TabIndex = 31
        Me.ADataMain.Text = "ADataMain"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(551, 193)
        Me.SprdView.TabIndex = 12
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(0, 24)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(105, 13)
        Me._lblLabels_2.TabIndex = 13
        Me._lblLabels_2.Text = "Group Name"
        '
        'ChkPrintIn
        '
        '
        'OptStatus
        '
        '
        'frmBSGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(551, 246)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.FraPrintIn)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.ADataMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBSGroup"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Balance Sheet Group"
        Me.Frame1.ResumeLayout(False)
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.FraPrintIn.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChkPrintIn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class