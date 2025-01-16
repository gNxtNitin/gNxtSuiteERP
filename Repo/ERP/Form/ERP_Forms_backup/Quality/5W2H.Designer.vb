Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frm5W2H
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
    Public WithEvents txtPresentHEIL2 As System.Windows.Forms.TextBox
    Public WithEvents txtPresentHEIL1 As System.Windows.Forms.TextBox
    Public WithEvents txtHeldAt As System.Windows.Forms.TextBox
    Public WithEvents txtSubject As System.Windows.Forms.TextBox
    Public WithEvents txtWith As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchPrepBy As System.Windows.Forms.Button
    Public WithEvents txtPreparedBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSlipNo As System.Windows.Forms.Button
    Public WithEvents txtHeldOn As System.Windows.Forms.TextBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblMac As System.Windows.Forms.Label
    Public WithEvents lblPreparedBy As System.Windows.Forms.Label
    Public WithEvents lblCompl As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm5W2H))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchPrepBy = New System.Windows.Forms.Button()
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
        Me.txtPresentHEIL2 = New System.Windows.Forms.TextBox()
        Me.txtPresentHEIL1 = New System.Windows.Forms.TextBox()
        Me.txtHeldAt = New System.Windows.Forms.TextBox()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.txtWith = New System.Windows.Forms.TextBox()
        Me.txtPreparedBy = New System.Windows.Forms.TextBox()
        Me.txtHeldOn = New System.Windows.Forms.TextBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblMac = New System.Windows.Forms.Label()
        Me.lblPreparedBy = New System.Windows.Forms.Label()
        Me.lblCompl = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
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
        'cmdSearchPrepBy
        '
        Me.cmdSearchPrepBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPrepBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPrepBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPrepBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPrepBy.Image = CType(resources.GetObject("cmdSearchPrepBy.Image"), System.Drawing.Image)
        Me.cmdSearchPrepBy.Location = New System.Drawing.Point(198, 86)
        Me.cmdSearchPrepBy.Name = "cmdSearchPrepBy"
        Me.cmdSearchPrepBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPrepBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchPrepBy.TabIndex = 17
        Me.cmdSearchPrepBy.TabStop = False
        Me.cmdSearchPrepBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPrepBy, "Search")
        Me.cmdSearchPrepBy.UseVisualStyleBackColor = False
        '
        'cmdSearchSlipNo
        '
        Me.cmdSearchSlipNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipNo.Image = CType(resources.GetObject("cmdSearchSlipNo.Image"), System.Drawing.Image)
        Me.cmdSearchSlipNo.Location = New System.Drawing.Point(198, 14)
        Me.cmdSearchSlipNo.Name = "cmdSearchSlipNo"
        Me.cmdSearchSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSlipNo.TabIndex = 15
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
        Me.CmdPreview.Location = New System.Drawing.Point(478, 11)
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(276, 11)
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
        Me.cmdPrint.Location = New System.Drawing.Point(410, 11)
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
        Me.CmdClose.Location = New System.Drawing.Point(612, 11)
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
        Me.CmdView.Location = New System.Drawing.Point(544, 11)
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
        Me.CmdDelete.Location = New System.Drawing.Point(342, 11)
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
        Me.CmdSave.Location = New System.Drawing.Point(208, 11)
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
        Me.CmdModify.Location = New System.Drawing.Point(140, 11)
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
        Me.CmdAdd.Location = New System.Drawing.Point(74, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 2
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtPresentHEIL2)
        Me.fraTop1.Controls.Add(Me.txtPresentHEIL1)
        Me.fraTop1.Controls.Add(Me.txtHeldAt)
        Me.fraTop1.Controls.Add(Me.txtSubject)
        Me.fraTop1.Controls.Add(Me.txtWith)
        Me.fraTop1.Controls.Add(Me.cmdSearchPrepBy)
        Me.fraTop1.Controls.Add(Me.txtPreparedBy)
        Me.fraTop1.Controls.Add(Me.cmdSearchSlipNo)
        Me.fraTop1.Controls.Add(Me.txtHeldOn)
        Me.fraTop1.Controls.Add(Me.txtSlipNo)
        Me.fraTop1.Controls.Add(Me.SprdMain)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.lblMac)
        Me.fraTop1.Controls.Add(Me.lblPreparedBy)
        Me.fraTop1.Controls.Add(Me.lblCompl)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(751, 465)
        Me.fraTop1.TabIndex = 12
        Me.fraTop1.TabStop = False
        '
        'txtPresentHEIL2
        '
        Me.txtPresentHEIL2.AcceptsReturn = True
        Me.txtPresentHEIL2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPresentHEIL2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPresentHEIL2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPresentHEIL2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentHEIL2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPresentHEIL2.Location = New System.Drawing.Point(100, 134)
        Me.txtPresentHEIL2.MaxLength = 0
        Me.txtPresentHEIL2.Name = "txtPresentHEIL2"
        Me.txtPresentHEIL2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPresentHEIL2.Size = New System.Drawing.Size(645, 20)
        Me.txtPresentHEIL2.TabIndex = 30
        '
        'txtPresentHEIL1
        '
        Me.txtPresentHEIL1.AcceptsReturn = True
        Me.txtPresentHEIL1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPresentHEIL1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPresentHEIL1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPresentHEIL1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPresentHEIL1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPresentHEIL1.Location = New System.Drawing.Point(100, 110)
        Me.txtPresentHEIL1.MaxLength = 0
        Me.txtPresentHEIL1.Name = "txtPresentHEIL1"
        Me.txtPresentHEIL1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPresentHEIL1.Size = New System.Drawing.Size(645, 20)
        Me.txtPresentHEIL1.TabIndex = 28
        '
        'txtHeldAt
        '
        Me.txtHeldAt.AcceptsReturn = True
        Me.txtHeldAt.BackColor = System.Drawing.SystemColors.Window
        Me.txtHeldAt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHeldAt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHeldAt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeldAt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtHeldAt.Location = New System.Drawing.Point(100, 62)
        Me.txtHeldAt.MaxLength = 0
        Me.txtHeldAt.Name = "txtHeldAt"
        Me.txtHeldAt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHeldAt.Size = New System.Drawing.Size(479, 20)
        Me.txtHeldAt.TabIndex = 26
        '
        'txtSubject
        '
        Me.txtSubject.AcceptsReturn = True
        Me.txtSubject.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubject.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubject.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubject.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSubject.Location = New System.Drawing.Point(100, 38)
        Me.txtSubject.MaxLength = 0
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubject.Size = New System.Drawing.Size(645, 20)
        Me.txtSubject.TabIndex = 24
        '
        'txtWith
        '
        Me.txtWith.AcceptsReturn = True
        Me.txtWith.BackColor = System.Drawing.SystemColors.Window
        Me.txtWith.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWith.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWith.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWith.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtWith.Location = New System.Drawing.Point(398, 14)
        Me.txtWith.MaxLength = 0
        Me.txtWith.Name = "txtWith"
        Me.txtWith.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWith.Size = New System.Drawing.Size(347, 20)
        Me.txtWith.TabIndex = 22
        '
        'txtPreparedBy
        '
        Me.txtPreparedBy.AcceptsReturn = True
        Me.txtPreparedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreparedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPreparedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreparedBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPreparedBy.Location = New System.Drawing.Point(100, 86)
        Me.txtPreparedBy.MaxLength = 0
        Me.txtPreparedBy.Name = "txtPreparedBy"
        Me.txtPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPreparedBy.Size = New System.Drawing.Size(93, 20)
        Me.txtPreparedBy.TabIndex = 16
        '
        'txtHeldOn
        '
        Me.txtHeldOn.AcceptsReturn = True
        Me.txtHeldOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtHeldOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHeldOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtHeldOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtHeldOn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtHeldOn.Location = New System.Drawing.Point(676, 62)
        Me.txtHeldOn.MaxLength = 0
        Me.txtHeldOn.Name = "txtHeldOn"
        Me.txtHeldOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHeldOn.Size = New System.Drawing.Size(69, 20)
        Me.txtHeldOn.TabIndex = 14
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(100, 14)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipNo.TabIndex = 13
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 160)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(747, 303)
        Me.SprdMain.TabIndex = 32
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(7, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Present HEIL2"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(7, 114)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 29
        Me.Label1.Text = "Present HEIL1"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(7, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Held At"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(7, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Subject"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMac
        '
        Me.lblMac.AutoSize = True
        Me.lblMac.BackColor = System.Drawing.SystemColors.Control
        Me.lblMac.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMac.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMac.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMac.Location = New System.Drawing.Point(293, 18)
        Me.lblMac.Name = "lblMac"
        Me.lblMac.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMac.Size = New System.Drawing.Size(31, 13)
        Me.lblMac.TabIndex = 23
        Me.lblMac.Text = "With"
        Me.lblMac.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPreparedBy
        '
        Me.lblPreparedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblPreparedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPreparedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPreparedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreparedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPreparedBy.Location = New System.Drawing.Point(230, 86)
        Me.lblPreparedBy.Name = "lblPreparedBy"
        Me.lblPreparedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPreparedBy.Size = New System.Drawing.Size(515, 19)
        Me.lblPreparedBy.TabIndex = 21
        '
        'lblCompl
        '
        Me.lblCompl.AutoSize = True
        Me.lblCompl.BackColor = System.Drawing.SystemColors.Control
        Me.lblCompl.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompl.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompl.Location = New System.Drawing.Point(7, 90)
        Me.lblCompl.Name = "lblCompl"
        Me.lblCompl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompl.Size = New System.Drawing.Size(72, 13)
        Me.lblCompl.TabIndex = 20
        Me.lblCompl.Text = "Recorded By"
        Me.lblCompl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(623, 66)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(48, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Held On"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(7, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 18
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
        Me.Report1.TabIndex = 14
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(750, 457)
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 458)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(751, 51)
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
        'frm5W2H
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(752, 509)
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
        Me.Name = "frm5W2H"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "5W2H"
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