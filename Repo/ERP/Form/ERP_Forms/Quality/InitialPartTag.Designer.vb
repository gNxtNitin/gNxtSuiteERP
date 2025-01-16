Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmInitialPartTag
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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents optInspChng As System.Windows.Forms.RadioButton
    Public WithEvents optMacChng As System.Windows.Forms.RadioButton
    Public WithEvents optPakChng As System.Windows.Forms.RadioButton
    Public WithEvents optTransMthd As System.Windows.Forms.RadioButton
    Public WithEvents optDieChng As System.Windows.Forms.RadioButton
    Public WithEvents optJigChng As System.Windows.Forms.RadioButton
    Public WithEvents optOther As System.Windows.Forms.RadioButton
    Public WithEvents optSubChng As System.Windows.Forms.RadioButton
    Public WithEvents optProChng As System.Windows.Forms.RadioButton
    Public WithEvents optMtrlChng As System.Windows.Forms.RadioButton
    Public WithEvents optEnggChng As System.Windows.Forms.RadioButton
    Public WithEvents optDsgnChng As System.Windows.Forms.RadioButton
    Public WithEvents optNewSupp As System.Windows.Forms.RadioButton
    Public WithEvents optQI As System.Windows.Forms.RadioButton
    Public WithEvents optDisSupp As System.Windows.Forms.RadioButton
    Public WithEvents optManpower As System.Windows.Forms.RadioButton
    Public WithEvents optOffLoad As System.Windows.Forms.RadioButton
    Public WithEvents optShiftNewLoc As System.Windows.Forms.RadioButton
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents cmdSearchInitiated As System.Windows.Forms.Button
    Public WithEvents txtInitiatedBy As System.Windows.Forms.TextBox
    Public WithEvents txtPartNo As System.Windows.Forms.TextBox
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCust As System.Windows.Forms.Button
    Public WithEvents txtPartDesc As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSlipNo As System.Windows.Forms.Button
    Public WithEvents txtDate As System.Windows.Forms.TextBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents txtModel As System.Windows.Forms.TextBox
    Public WithEvents txtMRRNo As System.Windows.Forms.TextBox
    Public WithEvents txtMRRDate As System.Windows.Forms.TextBox
    Public WithEvents txtChallanDate As System.Windows.Forms.TextBox
    Public WithEvents txtChallanNo As System.Windows.Forms.TextBox
    Public WithEvents txtECNDate As System.Windows.Forms.TextBox
    Public WithEvents txtECNNo As System.Windows.Forms.TextBox
    Public WithEvents txtQuantity As System.Windows.Forms.TextBox
    Public WithEvents txtInitiatedDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCloseDept As System.Windows.Forms.Button
    Public WithEvents txtCloseDept As System.Windows.Forms.TextBox
    Public WithEvents txtClosedDate As System.Windows.Forms.TextBox
    Public WithEvents txtClosedBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchClosed As System.Windows.Forms.Button
    'Public WithEvents Line1 As Microsoft.VisualBasic.PowerPacks.LineShape
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblInitiatedBy As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblCustomer As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblCloseDept As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents lblClosedBy As System.Windows.Forms.Label
    Public WithEvents Label20 As System.Windows.Forms.Label
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
    'Public WithEvents ShapeContainer1 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInitialPartTag))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchInitiated = New System.Windows.Forms.Button()
        Me.cmdSearchCust = New System.Windows.Forms.Button()
        Me.cmdSearchSlipNo = New System.Windows.Forms.Button()
        Me.cmdSearchCloseDept = New System.Windows.Forms.Button()
        Me.cmdSearchClosed = New System.Windows.Forms.Button()
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
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.optInspChng = New System.Windows.Forms.RadioButton()
        Me.optMacChng = New System.Windows.Forms.RadioButton()
        Me.optPakChng = New System.Windows.Forms.RadioButton()
        Me.optTransMthd = New System.Windows.Forms.RadioButton()
        Me.optDieChng = New System.Windows.Forms.RadioButton()
        Me.optJigChng = New System.Windows.Forms.RadioButton()
        Me.optOther = New System.Windows.Forms.RadioButton()
        Me.optSubChng = New System.Windows.Forms.RadioButton()
        Me.optProChng = New System.Windows.Forms.RadioButton()
        Me.optMtrlChng = New System.Windows.Forms.RadioButton()
        Me.optEnggChng = New System.Windows.Forms.RadioButton()
        Me.optDsgnChng = New System.Windows.Forms.RadioButton()
        Me.optNewSupp = New System.Windows.Forms.RadioButton()
        Me.optQI = New System.Windows.Forms.RadioButton()
        Me.optDisSupp = New System.Windows.Forms.RadioButton()
        Me.optManpower = New System.Windows.Forms.RadioButton()
        Me.optOffLoad = New System.Windows.Forms.RadioButton()
        Me.optShiftNewLoc = New System.Windows.Forms.RadioButton()
        Me.txtInitiatedBy = New System.Windows.Forms.TextBox()
        Me.txtPartNo = New System.Windows.Forms.TextBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtPartDesc = New System.Windows.Forms.TextBox()
        Me.txtDate = New System.Windows.Forms.TextBox()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.txtMRRNo = New System.Windows.Forms.TextBox()
        Me.txtMRRDate = New System.Windows.Forms.TextBox()
        Me.txtChallanDate = New System.Windows.Forms.TextBox()
        Me.txtChallanNo = New System.Windows.Forms.TextBox()
        Me.txtECNDate = New System.Windows.Forms.TextBox()
        Me.txtECNNo = New System.Windows.Forms.TextBox()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.txtInitiatedDate = New System.Windows.Forms.TextBox()
        Me.txtCloseDept = New System.Windows.Forms.TextBox()
        Me.txtClosedDate = New System.Windows.Forms.TextBox()
        Me.txtClosedBy = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblInitiatedBy = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblCloseDept = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblClosedBy = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.fraTop1.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchInitiated
        '
        Me.cmdSearchInitiated.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchInitiated.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchInitiated.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchInitiated.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchInitiated.Image = CType(resources.GetObject("cmdSearchInitiated.Image"), System.Drawing.Image)
        Me.cmdSearchInitiated.Location = New System.Drawing.Point(201, 142)
        Me.cmdSearchInitiated.Name = "cmdSearchInitiated"
        Me.cmdSearchInitiated.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchInitiated.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchInitiated.TabIndex = 19
        Me.cmdSearchInitiated.TabStop = False
        Me.cmdSearchInitiated.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchInitiated, "Search")
        Me.cmdSearchInitiated.UseVisualStyleBackColor = False
        '
        'cmdSearchCust
        '
        Me.cmdSearchCust.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCust.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCust.Image = CType(resources.GetObject("cmdSearchCust.Image"), System.Drawing.Image)
        Me.cmdSearchCust.Location = New System.Drawing.Point(201, 54)
        Me.cmdSearchCust.Name = "cmdSearchCust"
        Me.cmdSearchCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCust.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchCust.TabIndex = 9
        Me.cmdSearchCust.TabStop = False
        Me.cmdSearchCust.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCust, "Search")
        Me.cmdSearchCust.UseVisualStyleBackColor = False
        '
        'cmdSearchSlipNo
        '
        Me.cmdSearchSlipNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipNo.Image = CType(resources.GetObject("cmdSearchSlipNo.Image"), System.Drawing.Image)
        Me.cmdSearchSlipNo.Location = New System.Drawing.Point(201, 10)
        Me.cmdSearchSlipNo.Name = "cmdSearchSlipNo"
        Me.cmdSearchSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSlipNo.TabIndex = 4
        Me.cmdSearchSlipNo.TabStop = False
        Me.cmdSearchSlipNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSlipNo, "Search")
        Me.cmdSearchSlipNo.UseVisualStyleBackColor = False
        '
        'cmdSearchCloseDept
        '
        Me.cmdSearchCloseDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCloseDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCloseDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCloseDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCloseDept.Image = CType(resources.GetObject("cmdSearchCloseDept.Image"), System.Drawing.Image)
        Me.cmdSearchCloseDept.Location = New System.Drawing.Point(201, 164)
        Me.cmdSearchCloseDept.Name = "cmdSearchCloseDept"
        Me.cmdSearchCloseDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCloseDept.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchCloseDept.TabIndex = 22
        Me.cmdSearchCloseDept.TabStop = False
        Me.cmdSearchCloseDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCloseDept, "Search")
        Me.cmdSearchCloseDept.UseVisualStyleBackColor = False
        '
        'cmdSearchClosed
        '
        Me.cmdSearchClosed.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchClosed.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchClosed.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchClosed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchClosed.Image = CType(resources.GetObject("cmdSearchClosed.Image"), System.Drawing.Image)
        Me.cmdSearchClosed.Location = New System.Drawing.Point(201, 186)
        Me.cmdSearchClosed.Name = "cmdSearchClosed"
        Me.cmdSearchClosed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchClosed.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchClosed.TabIndex = 24
        Me.cmdSearchClosed.TabStop = False
        Me.cmdSearchClosed.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchClosed, "Search")
        Me.cmdSearchClosed.UseVisualStyleBackColor = False
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
        Me.CmdPreview.TabIndex = 53
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
        Me.cmdSavePrint.TabIndex = 52
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
        Me.cmdPrint.TabIndex = 51
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
        Me.CmdClose.TabIndex = 50
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
        Me.CmdView.TabIndex = 49
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
        Me.CmdDelete.TabIndex = 48
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
        Me.CmdSave.TabIndex = 47
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
        Me.CmdModify.TabIndex = 46
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
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.SSTab1)
        Me.fraTop1.Controls.Add(Me.cmdSearchInitiated)
        Me.fraTop1.Controls.Add(Me.txtInitiatedBy)
        Me.fraTop1.Controls.Add(Me.txtPartNo)
        Me.fraTop1.Controls.Add(Me.txtCustomer)
        Me.fraTop1.Controls.Add(Me.cmdSearchCust)
        Me.fraTop1.Controls.Add(Me.txtPartDesc)
        Me.fraTop1.Controls.Add(Me.cmdSearchSlipNo)
        Me.fraTop1.Controls.Add(Me.txtDate)
        Me.fraTop1.Controls.Add(Me.txtSlipNo)
        Me.fraTop1.Controls.Add(Me.txtModel)
        Me.fraTop1.Controls.Add(Me.txtMRRNo)
        Me.fraTop1.Controls.Add(Me.txtMRRDate)
        Me.fraTop1.Controls.Add(Me.txtChallanDate)
        Me.fraTop1.Controls.Add(Me.txtChallanNo)
        Me.fraTop1.Controls.Add(Me.txtECNDate)
        Me.fraTop1.Controls.Add(Me.txtECNNo)
        Me.fraTop1.Controls.Add(Me.txtQuantity)
        Me.fraTop1.Controls.Add(Me.txtInitiatedDate)
        Me.fraTop1.Controls.Add(Me.cmdSearchCloseDept)
        Me.fraTop1.Controls.Add(Me.txtCloseDept)
        Me.fraTop1.Controls.Add(Me.txtClosedDate)
        Me.fraTop1.Controls.Add(Me.txtClosedBy)
        Me.fraTop1.Controls.Add(Me.cmdSearchClosed)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.lblInitiatedBy)
        Me.fraTop1.Controls.Add(Me.Label16)
        Me.fraTop1.Controls.Add(Me.lblCustomer)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label13)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label11)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.Label14)
        Me.fraTop1.Controls.Add(Me.Label15)
        Me.fraTop1.Controls.Add(Me.Label17)
        Me.fraTop1.Controls.Add(Me.lblCloseDept)
        Me.fraTop1.Controls.Add(Me.Label18)
        Me.fraTop1.Controls.Add(Me.lblClosedBy)
        Me.fraTop1.Controls.Add(Me.Label20)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(711, 419)
        Me.fraTop1.TabIndex = 55
        Me.fraTop1.TabStop = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(2, 212)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 1
        Me.SSTab1.Size = New System.Drawing.Size(705, 205)
        Me.SSTab1.TabIndex = 78
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.SprdMain)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(697, 179)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Routing"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(3, 3)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(697, 171)
        Me.SprdMain.TabIndex = 26
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.Label2)
        Me._SSTab1_TabPage1.Controls.Add(Me.txtRemarks)
        Me._SSTab1_TabPage1.Controls.Add(Me.optInspChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optMacChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optPakChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optTransMthd)
        Me._SSTab1_TabPage1.Controls.Add(Me.optDieChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optJigChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optOther)
        Me._SSTab1_TabPage1.Controls.Add(Me.optSubChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optProChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optMtrlChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optEnggChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optDsgnChng)
        Me._SSTab1_TabPage1.Controls.Add(Me.optNewSupp)
        Me._SSTab1_TabPage1.Controls.Add(Me.optQI)
        Me._SSTab1_TabPage1.Controls.Add(Me.optDisSupp)
        Me._SSTab1_TabPage1.Controls.Add(Me.optManpower)
        Me._SSTab1_TabPage1.Controls.Add(Me.optOffLoad)
        Me._SSTab1_TabPage1.Controls.Add(Me.optShiftNewLoc)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(697, 179)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Modification Detail"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(76, 35)
        Me.Label2.TabIndex = 79
        Me.Label2.Text = "Details Of Modification"
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRemarks.Location = New System.Drawing.Point(83, 133)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks.Size = New System.Drawing.Size(609, 41)
        Me.txtRemarks.TabIndex = 45
        '
        'optInspChng
        '
        Me.optInspChng.AutoSize = True
        Me.optInspChng.BackColor = System.Drawing.SystemColors.Control
        Me.optInspChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optInspChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInspChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optInspChng.Location = New System.Drawing.Point(490, 54)
        Me.optInspChng.Name = "optInspChng"
        Me.optInspChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optInspChng.Size = New System.Drawing.Size(177, 17)
        Me.optInspChng.TabIndex = 42
        Me.optInspChng.TabStop = True
        Me.optInspChng.Text = "Insp. Gauge / Method Change"
        Me.optInspChng.UseVisualStyleBackColor = False
        '
        'optMacChng
        '
        Me.optMacChng.AutoSize = True
        Me.optMacChng.BackColor = System.Drawing.SystemColors.Control
        Me.optMacChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optMacChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMacChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMacChng.Location = New System.Drawing.Point(490, 38)
        Me.optMacChng.Name = "optMacChng"
        Me.optMacChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optMacChng.Size = New System.Drawing.Size(184, 17)
        Me.optMacChng.TabIndex = 41
        Me.optMacChng.TabStop = True
        Me.optMacChng.Text = "Machine Change / Modification"
        Me.optMacChng.UseVisualStyleBackColor = False
        '
        'optPakChng
        '
        Me.optPakChng.AutoSize = True
        Me.optPakChng.BackColor = System.Drawing.SystemColors.Control
        Me.optPakChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPakChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPakChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPakChng.Location = New System.Drawing.Point(490, 22)
        Me.optPakChng.Name = "optPakChng"
        Me.optPakChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPakChng.Size = New System.Drawing.Size(113, 17)
        Me.optPakChng.TabIndex = 40
        Me.optPakChng.TabStop = True
        Me.optPakChng.Text = "Pakaging Change"
        Me.optPakChng.UseVisualStyleBackColor = False
        '
        'optTransMthd
        '
        Me.optTransMthd.AutoSize = True
        Me.optTransMthd.BackColor = System.Drawing.SystemColors.Control
        Me.optTransMthd.Cursor = System.Windows.Forms.Cursors.Default
        Me.optTransMthd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optTransMthd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTransMthd.Location = New System.Drawing.Point(490, 6)
        Me.optTransMthd.Name = "optTransMthd"
        Me.optTransMthd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optTransMthd.Size = New System.Drawing.Size(142, 17)
        Me.optTransMthd.TabIndex = 39
        Me.optTransMthd.TabStop = True
        Me.optTransMthd.Text = "Transportation Method"
        Me.optTransMthd.UseVisualStyleBackColor = False
        '
        'optDieChng
        '
        Me.optDieChng.AutoSize = True
        Me.optDieChng.BackColor = System.Drawing.SystemColors.Control
        Me.optDieChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDieChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDieChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDieChng.Location = New System.Drawing.Point(248, 54)
        Me.optDieChng.Name = "optDieChng"
        Me.optDieChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDieChng.Size = New System.Drawing.Size(127, 17)
        Me.optDieChng.TabIndex = 36
        Me.optDieChng.TabStop = True
        Me.optDieChng.Text = "Die / Mould Change"
        Me.optDieChng.UseVisualStyleBackColor = False
        '
        'optJigChng
        '
        Me.optJigChng.AutoSize = True
        Me.optJigChng.BackColor = System.Drawing.SystemColors.Control
        Me.optJigChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optJigChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optJigChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optJigChng.Location = New System.Drawing.Point(248, 38)
        Me.optJigChng.Name = "optJigChng"
        Me.optJigChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optJigChng.Size = New System.Drawing.Size(188, 17)
        Me.optJigChng.TabIndex = 35
        Me.optJigChng.TabStop = True
        Me.optJigChng.Text = "Jig /Fixture/Tool Design Change"
        Me.optJigChng.UseVisualStyleBackColor = False
        '
        'optOther
        '
        Me.optOther.AutoSize = True
        Me.optOther.BackColor = System.Drawing.SystemColors.Control
        Me.optOther.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOther.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optOther.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOther.Location = New System.Drawing.Point(10, 114)
        Me.optOther.Name = "optOther"
        Me.optOther.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOther.Size = New System.Drawing.Size(166, 17)
        Me.optOther.TabIndex = 44
        Me.optOther.TabStop = True
        Me.optOther.Text = "Other ( Specified in details )"
        Me.optOther.UseVisualStyleBackColor = False
        '
        'optSubChng
        '
        Me.optSubChng.AutoSize = True
        Me.optSubChng.BackColor = System.Drawing.SystemColors.Control
        Me.optSubChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optSubChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optSubChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSubChng.Location = New System.Drawing.Point(248, 22)
        Me.optSubChng.Name = "optSubChng"
        Me.optSubChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optSubChng.Size = New System.Drawing.Size(124, 17)
        Me.optSubChng.TabIndex = 34
        Me.optSubChng.TabStop = True
        Me.optSubChng.Text = "Sub Vendor Change"
        Me.optSubChng.UseVisualStyleBackColor = False
        '
        'optProChng
        '
        Me.optProChng.AutoSize = True
        Me.optProChng.BackColor = System.Drawing.SystemColors.Control
        Me.optProChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optProChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optProChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optProChng.Location = New System.Drawing.Point(248, 6)
        Me.optProChng.Name = "optProChng"
        Me.optProChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optProChng.Size = New System.Drawing.Size(189, 17)
        Me.optProChng.TabIndex = 33
        Me.optProChng.TabStop = True
        Me.optProChng.Text = "Manu. Process Sequence Change"
        Me.optProChng.UseVisualStyleBackColor = False
        '
        'optMtrlChng
        '
        Me.optMtrlChng.AutoSize = True
        Me.optMtrlChng.BackColor = System.Drawing.SystemColors.Control
        Me.optMtrlChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optMtrlChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMtrlChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optMtrlChng.Location = New System.Drawing.Point(10, 54)
        Me.optMtrlChng.Name = "optMtrlChng"
        Me.optMtrlChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optMtrlChng.Size = New System.Drawing.Size(108, 17)
        Me.optMtrlChng.TabIndex = 30
        Me.optMtrlChng.TabStop = True
        Me.optMtrlChng.Text = "Material Change"
        Me.optMtrlChng.UseVisualStyleBackColor = False
        '
        'optEnggChng
        '
        Me.optEnggChng.AutoSize = True
        Me.optEnggChng.BackColor = System.Drawing.SystemColors.Control
        Me.optEnggChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optEnggChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optEnggChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optEnggChng.Location = New System.Drawing.Point(10, 38)
        Me.optEnggChng.Name = "optEnggChng"
        Me.optEnggChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optEnggChng.Size = New System.Drawing.Size(196, 17)
        Me.optEnggChng.TabIndex = 29
        Me.optEnggChng.TabStop = True
        Me.optEnggChng.Text = "Engineering / Manu. Change Note"
        Me.optEnggChng.UseVisualStyleBackColor = False
        '
        'optDsgnChng
        '
        Me.optDsgnChng.AutoSize = True
        Me.optDsgnChng.BackColor = System.Drawing.SystemColors.Control
        Me.optDsgnChng.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDsgnChng.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDsgnChng.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDsgnChng.Location = New System.Drawing.Point(10, 22)
        Me.optDsgnChng.Name = "optDsgnChng"
        Me.optDsgnChng.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDsgnChng.Size = New System.Drawing.Size(184, 17)
        Me.optDsgnChng.TabIndex = 28
        Me.optDsgnChng.TabStop = True
        Me.optDsgnChng.Text = "Drawing / Specification Change"
        Me.optDsgnChng.UseVisualStyleBackColor = False
        '
        'optNewSupp
        '
        Me.optNewSupp.AutoSize = True
        Me.optNewSupp.BackColor = System.Drawing.SystemColors.Control
        Me.optNewSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.optNewSupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optNewSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optNewSupp.Location = New System.Drawing.Point(10, 6)
        Me.optNewSupp.Name = "optNewSupp"
        Me.optNewSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optNewSupp.Size = New System.Drawing.Size(171, 17)
        Me.optNewSupp.TabIndex = 27
        Me.optNewSupp.TabStop = True
        Me.optNewSupp.Text = "New Supplier / New Location"
        Me.optNewSupp.UseVisualStyleBackColor = False
        '
        'optQI
        '
        Me.optQI.AutoSize = True
        Me.optQI.BackColor = System.Drawing.SystemColors.Control
        Me.optQI.Cursor = System.Windows.Forms.Cursors.Default
        Me.optQI.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optQI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optQI.Location = New System.Drawing.Point(10, 70)
        Me.optQI.Name = "optQI"
        Me.optQI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optQI.Size = New System.Drawing.Size(133, 17)
        Me.optQI.TabIndex = 31
        Me.optQI.TabStop = True
        Me.optQI.Text = "Quality Improvement"
        Me.optQI.UseVisualStyleBackColor = False
        '
        'optDisSupp
        '
        Me.optDisSupp.AutoSize = True
        Me.optDisSupp.BackColor = System.Drawing.SystemColors.Control
        Me.optDisSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDisSupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDisSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDisSupp.Location = New System.Drawing.Point(248, 70)
        Me.optDisSupp.Name = "optDisSupp"
        Me.optDisSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDisSupp.Size = New System.Drawing.Size(359, 17)
        Me.optDisSupp.TabIndex = 37
        Me.optDisSupp.TabStop = True
        Me.optDisSupp.Text = "Restart of Supplier && Parts After Discontinuation for twelve Month"
        Me.optDisSupp.UseVisualStyleBackColor = False
        '
        'optManpower
        '
        Me.optManpower.AutoSize = True
        Me.optManpower.BackColor = System.Drawing.SystemColors.Control
        Me.optManpower.Cursor = System.Windows.Forms.Cursors.Default
        Me.optManpower.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optManpower.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optManpower.Location = New System.Drawing.Point(490, 70)
        Me.optManpower.Name = "optManpower"
        Me.optManpower.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optManpower.Size = New System.Drawing.Size(168, 17)
        Me.optManpower.TabIndex = 43
        Me.optManpower.TabStop = True
        Me.optManpower.Text = "Manpower At Critical Stages"
        Me.optManpower.UseVisualStyleBackColor = False
        '
        'optOffLoad
        '
        Me.optOffLoad.AutoSize = True
        Me.optOffLoad.BackColor = System.Drawing.SystemColors.Control
        Me.optOffLoad.Cursor = System.Windows.Forms.Cursors.Default
        Me.optOffLoad.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optOffLoad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOffLoad.Location = New System.Drawing.Point(10, 98)
        Me.optOffLoad.Name = "optOffLoad"
        Me.optOffLoad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optOffLoad.Size = New System.Drawing.Size(209, 17)
        Me.optOffLoad.TabIndex = 32
        Me.optOffLoad.TabStop = True
        Me.optOffLoad.Text = "Offloading by Tier 1 && Tier2 Supplier"
        Me.optOffLoad.UseVisualStyleBackColor = False
        '
        'optShiftNewLoc
        '
        Me.optShiftNewLoc.AutoSize = True
        Me.optShiftNewLoc.BackColor = System.Drawing.SystemColors.Control
        Me.optShiftNewLoc.Cursor = System.Windows.Forms.Cursors.Default
        Me.optShiftNewLoc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optShiftNewLoc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShiftNewLoc.Location = New System.Drawing.Point(248, 98)
        Me.optShiftNewLoc.Name = "optShiftNewLoc"
        Me.optShiftNewLoc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optShiftNewLoc.Size = New System.Drawing.Size(320, 17)
        Me.optShiftNewLoc.TabIndex = 38
        Me.optShiftNewLoc.TabStop = True
        Me.optShiftNewLoc.Text = "Same LIne / Jigs / Fixture / Dies / Shifting to New Location"
        Me.optShiftNewLoc.UseVisualStyleBackColor = False
        '
        'txtInitiatedBy
        '
        Me.txtInitiatedBy.AcceptsReturn = True
        Me.txtInitiatedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtInitiatedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInitiatedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInitiatedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInitiatedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInitiatedBy.Location = New System.Drawing.Point(108, 142)
        Me.txtInitiatedBy.MaxLength = 0
        Me.txtInitiatedBy.Name = "txtInitiatedBy"
        Me.txtInitiatedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInitiatedBy.Size = New System.Drawing.Size(93, 20)
        Me.txtInitiatedBy.TabIndex = 18
        '
        'txtPartNo
        '
        Me.txtPartNo.AcceptsReturn = True
        Me.txtPartNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartNo.Location = New System.Drawing.Point(108, 32)
        Me.txtPartNo.MaxLength = 0
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartNo.Size = New System.Drawing.Size(93, 20)
        Me.txtPartNo.TabIndex = 6
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCustomer.Location = New System.Drawing.Point(108, 54)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(93, 20)
        Me.txtCustomer.TabIndex = 8
        '
        'txtPartDesc
        '
        Me.txtPartDesc.AcceptsReturn = True
        Me.txtPartDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPartDesc.Location = New System.Drawing.Point(308, 32)
        Me.txtPartDesc.MaxLength = 0
        Me.txtPartDesc.Name = "txtPartDesc"
        Me.txtPartDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartDesc.Size = New System.Drawing.Size(387, 20)
        Me.txtPartDesc.TabIndex = 7
        '
        'txtDate
        '
        Me.txtDate.AcceptsReturn = True
        Me.txtDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDate.Location = New System.Drawing.Point(605, 10)
        Me.txtDate.MaxLength = 0
        Me.txtDate.Name = "txtDate"
        Me.txtDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDate.Size = New System.Drawing.Size(89, 20)
        Me.txtDate.TabIndex = 5
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(108, 10)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipNo.TabIndex = 1
        '
        'txtModel
        '
        Me.txtModel.AcceptsReturn = True
        Me.txtModel.BackColor = System.Drawing.SystemColors.Window
        Me.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModel.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtModel.Location = New System.Drawing.Point(108, 76)
        Me.txtModel.MaxLength = 0
        Me.txtModel.Name = "txtModel"
        Me.txtModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModel.Size = New System.Drawing.Size(93, 20)
        Me.txtModel.TabIndex = 10
        '
        'txtMRRNo
        '
        Me.txtMRRNo.AcceptsReturn = True
        Me.txtMRRNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMRRNo.Location = New System.Drawing.Point(108, 98)
        Me.txtMRRNo.MaxLength = 0
        Me.txtMRRNo.Name = "txtMRRNo"
        Me.txtMRRNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRNo.Size = New System.Drawing.Size(93, 20)
        Me.txtMRRNo.TabIndex = 12
        '
        'txtMRRDate
        '
        Me.txtMRRDate.AcceptsReturn = True
        Me.txtMRRDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtMRRDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMRRDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMRRDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMRRDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMRRDate.Location = New System.Drawing.Point(108, 120)
        Me.txtMRRDate.MaxLength = 0
        Me.txtMRRDate.Name = "txtMRRDate"
        Me.txtMRRDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMRRDate.Size = New System.Drawing.Size(93, 20)
        Me.txtMRRDate.TabIndex = 15
        '
        'txtChallanDate
        '
        Me.txtChallanDate.AcceptsReturn = True
        Me.txtChallanDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtChallanDate.Location = New System.Drawing.Point(308, 120)
        Me.txtChallanDate.MaxLength = 0
        Me.txtChallanDate.Name = "txtChallanDate"
        Me.txtChallanDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanDate.Size = New System.Drawing.Size(93, 20)
        Me.txtChallanDate.TabIndex = 16
        '
        'txtChallanNo
        '
        Me.txtChallanNo.AcceptsReturn = True
        Me.txtChallanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtChallanNo.Location = New System.Drawing.Point(308, 98)
        Me.txtChallanNo.MaxLength = 0
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanNo.Size = New System.Drawing.Size(93, 20)
        Me.txtChallanNo.TabIndex = 13
        '
        'txtECNDate
        '
        Me.txtECNDate.AcceptsReturn = True
        Me.txtECNDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtECNDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtECNDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtECNDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtECNDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtECNDate.Location = New System.Drawing.Point(605, 120)
        Me.txtECNDate.MaxLength = 0
        Me.txtECNDate.Name = "txtECNDate"
        Me.txtECNDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtECNDate.Size = New System.Drawing.Size(89, 20)
        Me.txtECNDate.TabIndex = 17
        '
        'txtECNNo
        '
        Me.txtECNNo.AcceptsReturn = True
        Me.txtECNNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtECNNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtECNNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtECNNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtECNNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtECNNo.Location = New System.Drawing.Point(605, 98)
        Me.txtECNNo.MaxLength = 0
        Me.txtECNNo.Name = "txtECNNo"
        Me.txtECNNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtECNNo.Size = New System.Drawing.Size(89, 20)
        Me.txtECNNo.TabIndex = 14
        '
        'txtQuantity
        '
        Me.txtQuantity.AcceptsReturn = True
        Me.txtQuantity.BackColor = System.Drawing.SystemColors.Window
        Me.txtQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQuantity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtQuantity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQuantity.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtQuantity.Location = New System.Drawing.Point(308, 76)
        Me.txtQuantity.MaxLength = 0
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtQuantity.Size = New System.Drawing.Size(93, 20)
        Me.txtQuantity.TabIndex = 11
        '
        'txtInitiatedDate
        '
        Me.txtInitiatedDate.AcceptsReturn = True
        Me.txtInitiatedDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtInitiatedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInitiatedDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInitiatedDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInitiatedDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInitiatedDate.Location = New System.Drawing.Point(605, 142)
        Me.txtInitiatedDate.MaxLength = 0
        Me.txtInitiatedDate.Name = "txtInitiatedDate"
        Me.txtInitiatedDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInitiatedDate.Size = New System.Drawing.Size(89, 20)
        Me.txtInitiatedDate.TabIndex = 20
        '
        'txtCloseDept
        '
        Me.txtCloseDept.AcceptsReturn = True
        Me.txtCloseDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtCloseDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCloseDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCloseDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCloseDept.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCloseDept.Location = New System.Drawing.Point(108, 164)
        Me.txtCloseDept.MaxLength = 0
        Me.txtCloseDept.Name = "txtCloseDept"
        Me.txtCloseDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCloseDept.Size = New System.Drawing.Size(93, 20)
        Me.txtCloseDept.TabIndex = 21
        '
        'txtClosedDate
        '
        Me.txtClosedDate.AcceptsReturn = True
        Me.txtClosedDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtClosedDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClosedDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtClosedDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClosedDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtClosedDate.Location = New System.Drawing.Point(605, 186)
        Me.txtClosedDate.MaxLength = 0
        Me.txtClosedDate.Name = "txtClosedDate"
        Me.txtClosedDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtClosedDate.Size = New System.Drawing.Size(89, 20)
        Me.txtClosedDate.TabIndex = 25
        '
        'txtClosedBy
        '
        Me.txtClosedBy.AcceptsReturn = True
        Me.txtClosedBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtClosedBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClosedBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtClosedBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClosedBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClosedBy.Location = New System.Drawing.Point(108, 186)
        Me.txtClosedBy.MaxLength = 0
        Me.txtClosedBy.Name = "txtClosedBy"
        Me.txtClosedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtClosedBy.Size = New System.Drawing.Size(93, 20)
        Me.txtClosedBy.TabIndex = 23
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(12, 146)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(65, 13)
        Me.Label10.TabIndex = 77
        Me.Label10.Text = "Initiated By"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblInitiatedBy
        '
        Me.lblInitiatedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblInitiatedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInitiatedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInitiatedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInitiatedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInitiatedBy.Location = New System.Drawing.Point(226, 142)
        Me.lblInitiatedBy.Name = "lblInitiatedBy"
        Me.lblInitiatedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInitiatedBy.Size = New System.Drawing.Size(275, 19)
        Me.lblInitiatedBy.TabIndex = 76
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(12, 36)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(45, 13)
        Me.Label16.TabIndex = 75
        Me.Label16.Text = "Part No"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCustomer
        '
        Me.lblCustomer.BackColor = System.Drawing.SystemColors.Control
        Me.lblCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCustomer.Location = New System.Drawing.Point(226, 54)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCustomer.Size = New System.Drawing.Size(275, 19)
        Me.lblCustomer.TabIndex = 74
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 58)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 73
        Me.Label6.Text = "Customer "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(219, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 72
        Me.Label5.Text = "Part Desc"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(12, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 71
        Me.Label7.Text = "Number"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(509, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 70
        Me.Label8.Text = "Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 69
        Me.Label1.Text = "Model"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(12, 102)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(49, 13)
        Me.Label13.TabIndex = 68
        Me.Label13.Text = "MRR No"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(12, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 67
        Me.Label4.Text = "MRR Date"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(219, 124)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(71, 13)
        Me.Label11.TabIndex = 66
        Me.Label11.Text = "Challan Date"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(219, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "Challan No"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(509, 124)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(55, 13)
        Me.Label9.TabIndex = 64
        Me.Label9.Text = "ECN Date"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(509, 104)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(46, 13)
        Me.Label12.TabIndex = 63
        Me.Label12.Text = "ECN No"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(219, 80)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(50, 13)
        Me.Label14.TabIndex = 62
        Me.Label14.Text = "Quantity"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(509, 146)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(76, 13)
        Me.Label15.TabIndex = 61
        Me.Label15.Text = "Initiated Date"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(12, 168)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(84, 13)
        Me.Label17.TabIndex = 60
        Me.Label17.Text = "Closed At Dept"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCloseDept
        '
        Me.lblCloseDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblCloseDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCloseDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCloseDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCloseDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCloseDept.Location = New System.Drawing.Point(226, 164)
        Me.lblCloseDept.Name = "lblCloseDept"
        Me.lblCloseDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCloseDept.Size = New System.Drawing.Size(275, 19)
        Me.lblCloseDept.TabIndex = 59
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(509, 190)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(69, 13)
        Me.Label18.TabIndex = 58
        Me.Label18.Text = "Closed Date"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblClosedBy
        '
        Me.lblClosedBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblClosedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClosedBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblClosedBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClosedBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblClosedBy.Location = New System.Drawing.Point(226, 186)
        Me.lblClosedBy.Name = "lblClosedBy"
        Me.lblClosedBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblClosedBy.Size = New System.Drawing.Size(275, 19)
        Me.lblClosedBy.TabIndex = 57
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.SystemColors.Control
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(12, 190)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(58, 13)
        Me.Label20.TabIndex = 56
        Me.Label20.Text = "Closed By"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 57
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(710, 413)
        Me.SprdView.TabIndex = 2
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 408)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(711, 51)
        Me.FraMovement.TabIndex = 3
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
        Me.lblMkey.TabIndex = 54
        Me.lblMkey.Text = "lblMkey"
        '
        'frmInitialPartTag
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(711, 458)
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
        Me.Name = "frmInitialPartTag"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Initial Part Tag"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        Me._SSTab1_TabPage1.PerformLayout()
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