Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPDIProblemMst
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
        'Me.MDIParent = Production.Master

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
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents TxtCode As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
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
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPDIProblemMst))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.FraView = New System.Windows.Forms.GroupBox
        Me.Report1 = New AxCrystal.AxCrystalReport
        Me.cmdsearch = New System.Windows.Forms.Button
        Me.txtName = New System.Windows.Forms.TextBox
        Me.TxtCode = New System.Windows.Forms.TextBox
        Me._lblLabels_0 = New System.Windows.Forms.Label
        Me.FraGridView = New System.Windows.Forms.GroupBox
        Me.ADataGrid = New VB6.ADODC
        Me.SprdView = New AxFPSpreadADO.AxfpSpread
        Me.FraMovement = New System.Windows.Forms.GroupBox
        Me.cmdSavePrint = New System.Windows.Forms.Button
        Me.cmdPreview = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.CmdClose = New System.Windows.Forms.Button
        Me.CmdView = New System.Windows.Forms.Button
        Me.CmdSave = New System.Windows.Forms.Button
        Me.CmdDelete = New System.Windows.Forms.Button
        Me.CmdModify = New System.Windows.Forms.Button
        Me.CmdAdd = New System.Windows.Forms.Button
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(components)
        Me.FraView.SuspendLayout()
        Me.FraGridView.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Text = "PDI Problem Master"
        Me.ClientSize = New System.Drawing.Size(545, 323)
        Me.Location = New System.Drawing.Point(73, 22)
        Me.Icon = CType(resources.GetObject("frmPDIProblemMst.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.MinimizeBox = False
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ControlBox = True
        Me.Enabled = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "frmPDIProblemMst"
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Size = New System.Drawing.Size(545, 283)
        Me.FraView.Location = New System.Drawing.Point(0, -6)
        Me.FraView.TabIndex = 11
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.Enabled = True
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Visible = True
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.Name = "FraView"
        Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Location = New System.Drawing.Point(486, 232)
        Me.Report1.Name = "Report1"
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Size = New System.Drawing.Size(33, 23)
        Me.cmdsearch.Location = New System.Drawing.Point(440, 118)
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.TabIndex = 2
        Me.cmdsearch.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.CausesValidation = True
        Me.cmdsearch.Enabled = True
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Name = "cmdsearch"
        Me.txtName.AutoSize = False
        Me.txtName.ForeColor = System.Drawing.Color.FromARGB(0, 0, 192)
        Me.txtName.Size = New System.Drawing.Size(287, 23)
        Me.txtName.Location = New System.Drawing.Point(152, 118)
        Me.txtName.TabIndex = 1
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.AcceptsReturn = True
        Me.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.CausesValidation = True
        Me.txtName.Enabled = True
        Me.txtName.HideSelection = True
        Me.txtName.ReadOnly = False
        Me.txtName.Maxlength = 0
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.MultiLine = False
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtName.TabStop = True
        Me.txtName.Visible = True
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Name = "txtName"
        Me.TxtCode.AutoSize = False
        Me.TxtCode.Size = New System.Drawing.Size(43, 21)
        Me.TxtCode.Location = New System.Drawing.Point(275, 120)
        Me.TxtCode.TabIndex = 14
        Me.TxtCode.Text = "Text1"
        Me.TxtCode.Visible = False
        Me.TxtCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCode.AcceptsReturn = True
        Me.TxtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtCode.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCode.CausesValidation = True
        Me.TxtCode.Enabled = True
        Me.TxtCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCode.HideSelection = True
        Me.TxtCode.ReadOnly = False
        Me.TxtCode.Maxlength = 0
        Me.TxtCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCode.MultiLine = False
        Me.TxtCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCode.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.TxtCode.TabStop = True
        Me.TxtCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.TxtCode.Name = "TxtCode"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me._lblLabels_0.Text = "Fault Name :"
        Me._lblLabels_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.Size = New System.Drawing.Size(113, 13)
        Me._lblLabels_0.Location = New System.Drawing.Point(35, 122)
        Me._lblLabels_0.TabIndex = 15
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Enabled = True
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.UseMnemonic = True
        Me._lblLabels_0.Visible = True
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me.FraGridView.Size = New System.Drawing.Size(545, 283)
        Me.FraGridView.Location = New System.Drawing.Point(0, -6)
        Me.FraGridView.TabIndex = 12
        Me.FraGridView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraGridView.BackColor = System.Drawing.SystemColors.Control
        Me.FraGridView.Enabled = True
        Me.FraGridView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraGridView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraGridView.Visible = True
        Me.FraGridView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraGridView.Name = "FraGridView"
        Me.ADataGrid.Size = New System.Drawing.Size(99, 27)
        Me.ADataGrid.Location = New System.Drawing.Point(166, 38)
        Me.ADataGrid.Visible = 0
        Me.ADataGrid.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        Me.ADataGrid.ConnectionTimeout = 15
        Me.ADataGrid.CommandTimeout = 30
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.CacheSize = 50
        Me.ADataGrid.MaxRecords = 0
        Me.ADataGrid.BOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.BOFActionEnum.adDoMoveFirst
        Me.ADataGrid.EOFAction = Microsoft.VisualBasic.Compatibility.VB6.ADODC.EOFActionEnum.adDoMoveLast
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Orientation = Microsoft.VisualBasic.Compatibility.VB6.ADODC.OrientationEnum.adHorizontal
        Me.ADataGrid.Enabled = True
        Me.ADataGrid.UserName = ""
        Me.ADataGrid.RecordSource = ""
        Me.ADataGrid.Text = "Adodc1"
        Me.ADataGrid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataGrid.ConnectionString = ""
        Me.ADataGrid.Name = "ADataGrid"
        SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(539, 271)
        Me.SprdView.Location = New System.Drawing.Point(2, 8)
        Me.SprdView.TabIndex = 16
        Me.SprdView.Name = "SprdView"
        Me.FraMovement.Size = New System.Drawing.Size(545, 49)
        Me.FraMovement.Location = New System.Drawing.Point(0, 274)
        Me.FraMovement.TabIndex = 13
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Enabled = True
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Visible = True
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.Name = "FraMovement"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.Size = New System.Drawing.Size(60, 33)
        Me.cmdSavePrint.Location = New System.Drawing.Point(182, 12)
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print Record")
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.CausesValidation = True
        Me.cmdSavePrint.Enabled = True
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.TabStop = True
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.Size = New System.Drawing.Size(60, 33)
        Me.cmdPreview.Location = New System.Drawing.Point(362, 12)
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cmdPreview, "Print Preview")
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.CausesValidation = True
        Me.cmdPreview.Enabled = True
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.TabStop = True
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.Size = New System.Drawing.Size(60, 34)
        Me.cmdPrint.Location = New System.Drawing.Point(302, 12)
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print Record")
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.CausesValidation = True
        Me.cmdPrint.Enabled = True
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.TabStop = True
        Me.cmdPrint.Name = "cmdPrint"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.Location = New System.Drawing.Point(482, 12)
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.CausesValidation = True
        Me.CmdClose.Enabled = True
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.TabStop = True
        Me.CmdClose.Name = "CmdClose"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdView.Text = "List &View"
        Me.CmdView.Size = New System.Drawing.Size(60, 34)
        Me.CmdView.Location = New System.Drawing.Point(422, 12)
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.CausesValidation = True
        Me.CmdView.Enabled = True
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.TabStop = True
        Me.CmdView.Name = "CmdView"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.Size = New System.Drawing.Size(60, 34)
        Me.CmdSave.Location = New System.Drawing.Point(122, 12)
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.CausesValidation = True
        Me.CmdSave.Enabled = True
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.TabStop = True
        Me.CmdSave.Name = "CmdSave"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.Size = New System.Drawing.Size(60, 34)
        Me.CmdDelete.Location = New System.Drawing.Point(242, 12)
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.CausesValidation = True
        Me.CmdDelete.Enabled = True
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.TabStop = True
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.Size = New System.Drawing.Size(60, 34)
        Me.CmdModify.Location = New System.Drawing.Point(62, 12)
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.CausesValidation = True
        Me.CmdModify.Enabled = True
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.TabStop = True
        Me.CmdModify.Name = "CmdModify"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.Size = New System.Drawing.Size(60, 34)
        Me.CmdAdd.Location = New System.Drawing.Point(2, 12)
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.CausesValidation = True
        Me.CmdAdd.Enabled = True
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.TabStop = True
        Me.CmdAdd.Name = "CmdAdd"
        Me.lblLabels.SetIndex(_lblLabels_0, CType(0, Short))
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(FraView)
        Me.Controls.Add(FraGridView)
        Me.Controls.Add(FraMovement)
        Me.FraView.Controls.Add(Report1)
        Me.FraView.Controls.Add(cmdsearch)
        Me.FraView.Controls.Add(txtName)
        Me.FraView.Controls.Add(TxtCode)
        Me.FraView.Controls.Add(_lblLabels_0)
        Me.FraGridView.Controls.Add(ADataGrid)
        Me.FraGridView.Controls.Add(SprdView)
        Me.FraMovement.Controls.Add(cmdSavePrint)
        Me.FraMovement.Controls.Add(cmdPreview)
        Me.FraMovement.Controls.Add(cmdPrint)
        Me.FraMovement.Controls.Add(CmdClose)
        Me.FraMovement.Controls.Add(CmdView)
        Me.FraMovement.Controls.Add(CmdSave)
        Me.FraMovement.Controls.Add(CmdDelete)
        Me.FraMovement.Controls.Add(CmdModify)
        Me.FraMovement.Controls.Add(CmdAdd)
        Me.FraView.ResumeLayout(False)
        Me.FraGridView.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
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