Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmBreakDownMan
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
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtCTotalTime As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCCompldBy As System.Windows.Forms.Button
    Public WithEvents txtCCompldBy As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtCComptDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtCComptTime As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblCCompldBy As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents LblRemarks As System.Windows.Forms.Label
    Public WithEvents fraComplainerEnd As System.Windows.Forms.GroupBox
    Public WithEvents chkItemConsumed As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchProblem As System.Windows.Forms.Button
    Public WithEvents txtProblem As System.Windows.Forms.TextBox
    Public WithEvents txtTotalTime As System.Windows.Forms.TextBox
    Public WithEvents txtDeputPerson As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDeputPerson As System.Windows.Forms.Button
    Public WithEvents txtDeputRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtSlipRecvdBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSlipRecvdBy As System.Windows.Forms.Button
    Public WithEvents txtDeputDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtComptDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDeputTime As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtComptTime As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblCompTime As System.Windows.Forms.Label
    Public WithEvents LblDptTime As System.Windows.Forms.Label
    Public WithEvents lblProblem As System.Windows.Forms.Label
    Public WithEvents LblProb As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblDeputedPerson As System.Windows.Forms.Label
    Public WithEvents LblDeptDate As System.Windows.Forms.Label
    Public WithEvents lbldeptremarks As System.Windows.Forms.Label
    Public WithEvents lblCompDate As System.Windows.Forms.Label
    Public WithEvents lblSlipRecd As System.Windows.Forms.Label
    Public WithEvents lblSlipRecvdBy As System.Windows.Forms.Label
    Public WithEvents lblDeputPerson As System.Windows.Forms.Label
    Public WithEvents fraComplainee As System.Windows.Forms.GroupBox
    Public WithEvents txtSlipNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSlipNo As System.Windows.Forms.Button
    Public WithEvents cmdSearchFromDept As System.Windows.Forms.Button
    Public WithEvents txtToDept As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchToDept As System.Windows.Forms.Button
    Public WithEvents txtFromDept As System.Windows.Forms.TextBox
    Public WithEvents txtMachineNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchMacNo As System.Windows.Forms.Button
    Public WithEvents txtCompldBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCompldBy As System.Windows.Forms.Button
    Public WithEvents txtReason As System.Windows.Forms.TextBox
    Public WithEvents lblTdd As System.Windows.Forms.Label
    Public WithEvents Lbl12 As System.Windows.Forms.Label
    Public WithEvents lblMac As System.Windows.Forms.Label
    Public WithEvents lblCompl As System.Windows.Forms.Label
    Public WithEvents LblReason As System.Windows.Forms.Label
    Public WithEvents lblFromDept As System.Windows.Forms.Label
    Public WithEvents lblCompldBy As System.Windows.Forms.Label
    Public WithEvents lblMachineNo As System.Windows.Forms.Label
    Public WithEvents lblToDept As System.Windows.Forms.Label
    Public WithEvents fraComplainer As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents fraItem As System.Windows.Forms.GroupBox
    Public WithEvents txtSlipDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtSlipTime As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblFormType As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBreakDownMan))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchCCompldBy = New System.Windows.Forms.Button()
        Me.cmdSearchProblem = New System.Windows.Forms.Button()
        Me.cmdSearchDeputPerson = New System.Windows.Forms.Button()
        Me.cmdSearchSlipRecvdBy = New System.Windows.Forms.Button()
        Me.cmdSearchSlipNo = New System.Windows.Forms.Button()
        Me.cmdSearchFromDept = New System.Windows.Forms.Button()
        Me.cmdSearchToDept = New System.Windows.Forms.Button()
        Me.cmdSearchMacNo = New System.Windows.Forms.Button()
        Me.cmdSearchCompldBy = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.fraComplainerEnd = New System.Windows.Forms.GroupBox()
        Me.txtCTotalTime = New System.Windows.Forms.TextBox()
        Me.txtCCompldBy = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtCComptDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtCComptTime = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblCCompldBy = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblRemarks = New System.Windows.Forms.Label()
        Me.fraComplainee = New System.Windows.Forms.GroupBox()
        Me.chkItemConsumed = New System.Windows.Forms.CheckBox()
        Me.txtProblem = New System.Windows.Forms.TextBox()
        Me.txtTotalTime = New System.Windows.Forms.TextBox()
        Me.txtDeputPerson = New System.Windows.Forms.TextBox()
        Me.txtDeputRemarks = New System.Windows.Forms.TextBox()
        Me.txtSlipRecvdBy = New System.Windows.Forms.TextBox()
        Me.txtDeputDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtComptDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtDeputTime = New System.Windows.Forms.MaskedTextBox()
        Me.txtComptTime = New System.Windows.Forms.MaskedTextBox()
        Me.lblCompTime = New System.Windows.Forms.Label()
        Me.LblDptTime = New System.Windows.Forms.Label()
        Me.lblProblem = New System.Windows.Forms.Label()
        Me.LblProb = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDeputedPerson = New System.Windows.Forms.Label()
        Me.LblDeptDate = New System.Windows.Forms.Label()
        Me.lbldeptremarks = New System.Windows.Forms.Label()
        Me.lblCompDate = New System.Windows.Forms.Label()
        Me.lblSlipRecd = New System.Windows.Forms.Label()
        Me.lblSlipRecvdBy = New System.Windows.Forms.Label()
        Me.lblDeputPerson = New System.Windows.Forms.Label()
        Me.txtSlipNo = New System.Windows.Forms.TextBox()
        Me.fraComplainer = New System.Windows.Forms.GroupBox()
        Me.txtToDept = New System.Windows.Forms.TextBox()
        Me.txtFromDept = New System.Windows.Forms.TextBox()
        Me.txtMachineNo = New System.Windows.Forms.TextBox()
        Me.txtCompldBy = New System.Windows.Forms.TextBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.lblTdd = New System.Windows.Forms.Label()
        Me.Lbl12 = New System.Windows.Forms.Label()
        Me.lblMac = New System.Windows.Forms.Label()
        Me.lblCompl = New System.Windows.Forms.Label()
        Me.LblReason = New System.Windows.Forms.Label()
        Me.lblFromDept = New System.Windows.Forms.Label()
        Me.lblCompldBy = New System.Windows.Forms.Label()
        Me.lblMachineNo = New System.Windows.Forms.Label()
        Me.lblToDept = New System.Windows.Forms.Label()
        Me.fraItem = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.txtSlipDate = New System.Windows.Forms.MaskedTextBox()
        Me.txtSlipTime = New System.Windows.Forms.MaskedTextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblFormType = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.fraTop1.SuspendLayout()
        Me.fraComplainerEnd.SuspendLayout()
        Me.fraComplainee.SuspendLayout()
        Me.fraComplainer.SuspendLayout()
        Me.fraItem.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchCCompldBy
        '
        Me.cmdSearchCCompldBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCCompldBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCCompldBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCCompldBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCCompldBy.Image = CType(resources.GetObject("cmdSearchCCompldBy.Image"), System.Drawing.Image)
        Me.cmdSearchCCompldBy.Location = New System.Drawing.Point(214, 34)
        Me.cmdSearchCCompldBy.Name = "cmdSearchCCompldBy"
        Me.cmdSearchCCompldBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCCompldBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchCCompldBy.TabIndex = 42
        Me.cmdSearchCCompldBy.TabStop = False
        Me.cmdSearchCCompldBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCCompldBy, "Search")
        Me.cmdSearchCCompldBy.UseVisualStyleBackColor = False
        '
        'cmdSearchProblem
        '
        Me.cmdSearchProblem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProblem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProblem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProblem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProblem.Image = CType(resources.GetObject("cmdSearchProblem.Image"), System.Drawing.Image)
        Me.cmdSearchProblem.Location = New System.Drawing.Point(214, 54)
        Me.cmdSearchProblem.Name = "cmdSearchProblem"
        Me.cmdSearchProblem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProblem.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchProblem.TabIndex = 40
        Me.cmdSearchProblem.TabStop = False
        Me.cmdSearchProblem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProblem, "Search")
        Me.cmdSearchProblem.UseVisualStyleBackColor = False
        '
        'cmdSearchDeputPerson
        '
        Me.cmdSearchDeputPerson.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDeputPerson.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDeputPerson.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDeputPerson.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDeputPerson.Image = CType(resources.GetObject("cmdSearchDeputPerson.Image"), System.Drawing.Image)
        Me.cmdSearchDeputPerson.Location = New System.Drawing.Point(214, 10)
        Me.cmdSearchDeputPerson.Name = "cmdSearchDeputPerson"
        Me.cmdSearchDeputPerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDeputPerson.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchDeputPerson.TabIndex = 39
        Me.cmdSearchDeputPerson.TabStop = False
        Me.cmdSearchDeputPerson.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDeputPerson, "Search")
        Me.cmdSearchDeputPerson.UseVisualStyleBackColor = False
        '
        'cmdSearchSlipRecvdBy
        '
        Me.cmdSearchSlipRecvdBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipRecvdBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipRecvdBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipRecvdBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipRecvdBy.Image = CType(resources.GetObject("cmdSearchSlipRecvdBy.Image"), System.Drawing.Image)
        Me.cmdSearchSlipRecvdBy.Location = New System.Drawing.Point(214, 98)
        Me.cmdSearchSlipRecvdBy.Name = "cmdSearchSlipRecvdBy"
        Me.cmdSearchSlipRecvdBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipRecvdBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSlipRecvdBy.TabIndex = 41
        Me.cmdSearchSlipRecvdBy.TabStop = False
        Me.cmdSearchSlipRecvdBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSlipRecvdBy, "Search")
        Me.cmdSearchSlipRecvdBy.UseVisualStyleBackColor = False
        '
        'cmdSearchSlipNo
        '
        Me.cmdSearchSlipNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSlipNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSlipNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSlipNo.Image = CType(resources.GetObject("cmdSearchSlipNo.Image"), System.Drawing.Image)
        Me.cmdSearchSlipNo.Location = New System.Drawing.Point(0, 0)
        Me.cmdSearchSlipNo.Name = "cmdSearchSlipNo"
        Me.cmdSearchSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSlipNo.Size = New System.Drawing.Size(0, 0)
        Me.cmdSearchSlipNo.TabIndex = 57
        Me.cmdSearchSlipNo.TabStop = False
        Me.cmdSearchSlipNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSlipNo, "Search")
        Me.cmdSearchSlipNo.UseVisualStyleBackColor = False
        '
        'cmdSearchFromDept
        '
        Me.cmdSearchFromDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchFromDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchFromDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchFromDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchFromDept.Image = CType(resources.GetObject("cmdSearchFromDept.Image"), System.Drawing.Image)
        Me.cmdSearchFromDept.Location = New System.Drawing.Point(214, 10)
        Me.cmdSearchFromDept.Name = "cmdSearchFromDept"
        Me.cmdSearchFromDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchFromDept.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchFromDept.TabIndex = 35
        Me.cmdSearchFromDept.TabStop = False
        Me.cmdSearchFromDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchFromDept, "Search")
        Me.cmdSearchFromDept.UseVisualStyleBackColor = False
        '
        'cmdSearchToDept
        '
        Me.cmdSearchToDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchToDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchToDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchToDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchToDept.Image = CType(resources.GetObject("cmdSearchToDept.Image"), System.Drawing.Image)
        Me.cmdSearchToDept.Location = New System.Drawing.Point(214, 32)
        Me.cmdSearchToDept.Name = "cmdSearchToDept"
        Me.cmdSearchToDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchToDept.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchToDept.TabIndex = 36
        Me.cmdSearchToDept.TabStop = False
        Me.cmdSearchToDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchToDept, "Search")
        Me.cmdSearchToDept.UseVisualStyleBackColor = False
        '
        'cmdSearchMacNo
        '
        Me.cmdSearchMacNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMacNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMacNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMacNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMacNo.Image = CType(resources.GetObject("cmdSearchMacNo.Image"), System.Drawing.Image)
        Me.cmdSearchMacNo.Location = New System.Drawing.Point(214, 54)
        Me.cmdSearchMacNo.Name = "cmdSearchMacNo"
        Me.cmdSearchMacNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMacNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchMacNo.TabIndex = 37
        Me.cmdSearchMacNo.TabStop = False
        Me.cmdSearchMacNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMacNo, "Search")
        Me.cmdSearchMacNo.UseVisualStyleBackColor = False
        '
        'cmdSearchCompldBy
        '
        Me.cmdSearchCompldBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCompldBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCompldBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCompldBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCompldBy.Image = CType(resources.GetObject("cmdSearchCompldBy.Image"), System.Drawing.Image)
        Me.cmdSearchCompldBy.Location = New System.Drawing.Point(214, 76)
        Me.cmdSearchCompldBy.Name = "cmdSearchCompldBy"
        Me.cmdSearchCompldBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCompldBy.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchCompldBy.TabIndex = 38
        Me.cmdSearchCompldBy.TabStop = False
        Me.cmdSearchCompldBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCompldBy, "Search")
        Me.cmdSearchCompldBy.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(678, 9)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(78, 33)
        Me.CmdClose.TabIndex = 33
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
        Me.CmdView.Location = New System.Drawing.Point(601, 9)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(78, 33)
        Me.CmdView.TabIndex = 32
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
        Me.CmdPreview.Location = New System.Drawing.Point(524, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(78, 33)
        Me.CmdPreview.TabIndex = 31
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(447, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(78, 33)
        Me.cmdPrint.TabIndex = 30
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(370, 9)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(78, 33)
        Me.CmdDelete.TabIndex = 29
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(293, 9)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(78, 33)
        Me.cmdSavePrint.TabIndex = 28
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(216, 9)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(78, 33)
        Me.CmdSave.TabIndex = 27
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
        Me.CmdModify.Location = New System.Drawing.Point(139, 9)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(78, 33)
        Me.CmdModify.TabIndex = 26
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
        Me.CmdAdd.Location = New System.Drawing.Point(62, 9)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(78, 33)
        Me.CmdAdd.TabIndex = 25
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.cboDivision)
        Me.fraTop1.Controls.Add(Me.fraComplainerEnd)
        Me.fraTop1.Controls.Add(Me.fraComplainee)
        Me.fraTop1.Controls.Add(Me.txtSlipNo)
        Me.fraTop1.Controls.Add(Me.cmdSearchSlipNo)
        Me.fraTop1.Controls.Add(Me.fraComplainer)
        Me.fraTop1.Controls.Add(Me.fraItem)
        Me.fraTop1.Controls.Add(Me.txtSlipDate)
        Me.fraTop1.Controls.Add(Me.txtSlipTime)
        Me.fraTop1.Controls.Add(Me.Label12)
        Me.fraTop1.Controls.Add(Me.lblFormType)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(911, 583)
        Me.fraTop1.TabIndex = 45
        Me.fraTop1.TabStop = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(552, 8)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(189, 22)
        Me.cboDivision.TabIndex = 3
        '
        'fraComplainerEnd
        '
        Me.fraComplainerEnd.BackColor = System.Drawing.SystemColors.Control
        Me.fraComplainerEnd.Controls.Add(Me.txtCTotalTime)
        Me.fraComplainerEnd.Controls.Add(Me.cmdSearchCCompldBy)
        Me.fraComplainerEnd.Controls.Add(Me.txtCCompldBy)
        Me.fraComplainerEnd.Controls.Add(Me.txtRemarks)
        Me.fraComplainerEnd.Controls.Add(Me.txtCComptDate)
        Me.fraComplainerEnd.Controls.Add(Me.txtCComptTime)
        Me.fraComplainerEnd.Controls.Add(Me.Label5)
        Me.fraComplainerEnd.Controls.Add(Me.lblCCompldBy)
        Me.fraComplainerEnd.Controls.Add(Me.Label3)
        Me.fraComplainerEnd.Controls.Add(Me.Label2)
        Me.fraComplainerEnd.Controls.Add(Me.Label1)
        Me.fraComplainerEnd.Controls.Add(Me.LblRemarks)
        Me.fraComplainerEnd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraComplainerEnd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraComplainerEnd.Location = New System.Drawing.Point(0, 303)
        Me.fraComplainerEnd.Name = "fraComplainerEnd"
        Me.fraComplainerEnd.Padding = New System.Windows.Forms.Padding(0)
        Me.fraComplainerEnd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraComplainerEnd.Size = New System.Drawing.Size(908, 78)
        Me.fraComplainerEnd.TabIndex = 69
        Me.fraComplainerEnd.TabStop = False
        Me.fraComplainerEnd.Text = "Completed At Complainer Site"
        '
        'txtCTotalTime
        '
        Me.txtCTotalTime.AcceptsReturn = True
        Me.txtCTotalTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtCTotalTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCTotalTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCTotalTime.Enabled = False
        Me.txtCTotalTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCTotalTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCTotalTime.Location = New System.Drawing.Point(648, 14)
        Me.txtCTotalTime.MaxLength = 0
        Me.txtCTotalTime.Name = "txtCTotalTime"
        Me.txtCTotalTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCTotalTime.Size = New System.Drawing.Size(93, 20)
        Me.txtCTotalTime.TabIndex = 21
        '
        'txtCCompldBy
        '
        Me.txtCCompldBy.AcceptsReturn = True
        Me.txtCCompldBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtCCompldBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCCompldBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCCompldBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCCompldBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCCompldBy.Location = New System.Drawing.Point(118, 34)
        Me.txtCCompldBy.MaxLength = 0
        Me.txtCCompldBy.Name = "txtCCompldBy"
        Me.txtCCompldBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCCompldBy.Size = New System.Drawing.Size(93, 20)
        Me.txtCCompldBy.TabIndex = 22
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtRemarks.Location = New System.Drawing.Point(118, 54)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(623, 20)
        Me.txtRemarks.TabIndex = 23
        '
        'txtCComptDate
        '
        Me.txtCComptDate.AllowPromptAsInput = False
        Me.txtCComptDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCComptDate.Location = New System.Drawing.Point(118, 13)
        Me.txtCComptDate.Mask = "##/##/####"
        Me.txtCComptDate.Name = "txtCComptDate"
        Me.txtCComptDate.Size = New System.Drawing.Size(93, 20)
        Me.txtCComptDate.TabIndex = 19
        '
        'txtCComptTime
        '
        Me.txtCComptTime.AllowPromptAsInput = False
        Me.txtCComptTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCComptTime.Location = New System.Drawing.Point(398, 13)
        Me.txtCComptTime.Mask = "##:##"
        Me.txtCComptTime.Name = "txtCComptTime"
        Me.txtCComptTime.Size = New System.Drawing.Size(93, 20)
        Me.txtCComptTime.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(577, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 77
        Me.Label5.Text = "Total Time :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCCompldBy
        '
        Me.lblCCompldBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblCCompldBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCCompldBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCCompldBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCCompldBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCCompldBy.Location = New System.Drawing.Point(238, 34)
        Me.lblCCompldBy.Name = "lblCCompldBy"
        Me.lblCCompldBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCCompldBy.Size = New System.Drawing.Size(503, 19)
        Me.lblCCompldBy.TabIndex = 75
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(24, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(91, 13)
        Me.Label3.TabIndex = 74
        Me.Label3.Text = "Complained By :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(100, 13)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "Completion Date :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(294, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(101, 13)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "Completion Time :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblRemarks
        '
        Me.LblRemarks.AutoSize = True
        Me.LblRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.LblRemarks.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblRemarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemarks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblRemarks.Location = New System.Drawing.Point(8, 56)
        Me.LblRemarks.Name = "LblRemarks"
        Me.LblRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblRemarks.Size = New System.Drawing.Size(57, 13)
        Me.LblRemarks.TabIndex = 70
        Me.LblRemarks.Text = "Remarks :"
        Me.LblRemarks.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraComplainee
        '
        Me.fraComplainee.BackColor = System.Drawing.SystemColors.Control
        Me.fraComplainee.Controls.Add(Me.chkItemConsumed)
        Me.fraComplainee.Controls.Add(Me.cmdSearchProblem)
        Me.fraComplainee.Controls.Add(Me.txtProblem)
        Me.fraComplainee.Controls.Add(Me.txtTotalTime)
        Me.fraComplainee.Controls.Add(Me.txtDeputPerson)
        Me.fraComplainee.Controls.Add(Me.cmdSearchDeputPerson)
        Me.fraComplainee.Controls.Add(Me.txtDeputRemarks)
        Me.fraComplainee.Controls.Add(Me.txtSlipRecvdBy)
        Me.fraComplainee.Controls.Add(Me.cmdSearchSlipRecvdBy)
        Me.fraComplainee.Controls.Add(Me.txtDeputDate)
        Me.fraComplainee.Controls.Add(Me.txtComptDate)
        Me.fraComplainee.Controls.Add(Me.txtDeputTime)
        Me.fraComplainee.Controls.Add(Me.txtComptTime)
        Me.fraComplainee.Controls.Add(Me.lblCompTime)
        Me.fraComplainee.Controls.Add(Me.LblDptTime)
        Me.fraComplainee.Controls.Add(Me.lblProblem)
        Me.fraComplainee.Controls.Add(Me.LblProb)
        Me.fraComplainee.Controls.Add(Me.Label4)
        Me.fraComplainee.Controls.Add(Me.lblDeputedPerson)
        Me.fraComplainee.Controls.Add(Me.LblDeptDate)
        Me.fraComplainee.Controls.Add(Me.lbldeptremarks)
        Me.fraComplainee.Controls.Add(Me.lblCompDate)
        Me.fraComplainee.Controls.Add(Me.lblSlipRecd)
        Me.fraComplainee.Controls.Add(Me.lblSlipRecvdBy)
        Me.fraComplainee.Controls.Add(Me.lblDeputPerson)
        Me.fraComplainee.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraComplainee.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraComplainee.Location = New System.Drawing.Point(0, 154)
        Me.fraComplainee.Name = "fraComplainee"
        Me.fraComplainee.Padding = New System.Windows.Forms.Padding(0)
        Me.fraComplainee.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraComplainee.Size = New System.Drawing.Size(908, 146)
        Me.fraComplainee.TabIndex = 61
        Me.fraComplainee.TabStop = False
        Me.fraComplainee.Text = "Complainee Site"
        '
        'chkItemConsumed
        '
        Me.chkItemConsumed.AutoSize = True
        Me.chkItemConsumed.BackColor = System.Drawing.SystemColors.Control
        Me.chkItemConsumed.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkItemConsumed.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemConsumed.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkItemConsumed.Location = New System.Drawing.Point(600, 35)
        Me.chkItemConsumed.Name = "chkItemConsumed"
        Me.chkItemConsumed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkItemConsumed.Size = New System.Drawing.Size(136, 17)
        Me.chkItemConsumed.TabIndex = 12
        Me.chkItemConsumed.Text = "Item Consumed (Y/N)"
        Me.chkItemConsumed.UseVisualStyleBackColor = False
        '
        'txtProblem
        '
        Me.txtProblem.AcceptsReturn = True
        Me.txtProblem.BackColor = System.Drawing.SystemColors.Window
        Me.txtProblem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProblem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProblem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProblem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtProblem.Location = New System.Drawing.Point(118, 54)
        Me.txtProblem.MaxLength = 0
        Me.txtProblem.Name = "txtProblem"
        Me.txtProblem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProblem.Size = New System.Drawing.Size(93, 20)
        Me.txtProblem.TabIndex = 13
        '
        'txtTotalTime
        '
        Me.txtTotalTime.AcceptsReturn = True
        Me.txtTotalTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalTime.Enabled = False
        Me.txtTotalTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtTotalTime.Location = New System.Drawing.Point(648, 120)
        Me.txtTotalTime.MaxLength = 0
        Me.txtTotalTime.Name = "txtTotalTime"
        Me.txtTotalTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalTime.Size = New System.Drawing.Size(93, 20)
        Me.txtTotalTime.TabIndex = 18
        '
        'txtDeputPerson
        '
        Me.txtDeputPerson.AcceptsReturn = True
        Me.txtDeputPerson.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeputPerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeputPerson.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeputPerson.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeputPerson.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDeputPerson.Location = New System.Drawing.Point(118, 10)
        Me.txtDeputPerson.MaxLength = 0
        Me.txtDeputPerson.Name = "txtDeputPerson"
        Me.txtDeputPerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeputPerson.Size = New System.Drawing.Size(93, 20)
        Me.txtDeputPerson.TabIndex = 9
        '
        'txtDeputRemarks
        '
        Me.txtDeputRemarks.AcceptsReturn = True
        Me.txtDeputRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeputRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeputRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeputRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeputRemarks.ForeColor = System.Drawing.Color.Blue
        Me.txtDeputRemarks.Location = New System.Drawing.Point(118, 76)
        Me.txtDeputRemarks.MaxLength = 0
        Me.txtDeputRemarks.Name = "txtDeputRemarks"
        Me.txtDeputRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeputRemarks.Size = New System.Drawing.Size(623, 20)
        Me.txtDeputRemarks.TabIndex = 14
        '
        'txtSlipRecvdBy
        '
        Me.txtSlipRecvdBy.AcceptsReturn = True
        Me.txtSlipRecvdBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipRecvdBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipRecvdBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipRecvdBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipRecvdBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipRecvdBy.Location = New System.Drawing.Point(118, 98)
        Me.txtSlipRecvdBy.MaxLength = 0
        Me.txtSlipRecvdBy.Name = "txtSlipRecvdBy"
        Me.txtSlipRecvdBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipRecvdBy.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipRecvdBy.TabIndex = 15
        '
        'txtDeputDate
        '
        Me.txtDeputDate.AllowPromptAsInput = False
        Me.txtDeputDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeputDate.Location = New System.Drawing.Point(118, 31)
        Me.txtDeputDate.Mask = "##/##/####"
        Me.txtDeputDate.Name = "txtDeputDate"
        Me.txtDeputDate.Size = New System.Drawing.Size(93, 20)
        Me.txtDeputDate.TabIndex = 10
        '
        'txtComptDate
        '
        Me.txtComptDate.AllowPromptAsInput = False
        Me.txtComptDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComptDate.Location = New System.Drawing.Point(118, 119)
        Me.txtComptDate.Mask = "##/##/####"
        Me.txtComptDate.Name = "txtComptDate"
        Me.txtComptDate.Size = New System.Drawing.Size(93, 20)
        Me.txtComptDate.TabIndex = 16
        '
        'txtDeputTime
        '
        Me.txtDeputTime.AllowPromptAsInput = False
        Me.txtDeputTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeputTime.Location = New System.Drawing.Point(398, 31)
        Me.txtDeputTime.Mask = "##:##"
        Me.txtDeputTime.Name = "txtDeputTime"
        Me.txtDeputTime.Size = New System.Drawing.Size(93, 20)
        Me.txtDeputTime.TabIndex = 11
        '
        'txtComptTime
        '
        Me.txtComptTime.AllowPromptAsInput = False
        Me.txtComptTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtComptTime.Location = New System.Drawing.Point(398, 119)
        Me.txtComptTime.Mask = "##:##"
        Me.txtComptTime.Name = "txtComptTime"
        Me.txtComptTime.Size = New System.Drawing.Size(93, 20)
        Me.txtComptTime.TabIndex = 17
        '
        'lblCompTime
        '
        Me.lblCompTime.AutoSize = True
        Me.lblCompTime.BackColor = System.Drawing.SystemColors.Control
        Me.lblCompTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompTime.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompTime.Location = New System.Drawing.Point(294, 126)
        Me.lblCompTime.Name = "lblCompTime"
        Me.lblCompTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompTime.Size = New System.Drawing.Size(101, 13)
        Me.lblCompTime.TabIndex = 81
        Me.lblCompTime.Text = "Completion Time :"
        Me.lblCompTime.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblDptTime
        '
        Me.LblDptTime.AutoSize = True
        Me.LblDptTime.BackColor = System.Drawing.SystemColors.Control
        Me.LblDptTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDptTime.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDptTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDptTime.Location = New System.Drawing.Point(294, 36)
        Me.LblDptTime.Name = "LblDptTime"
        Me.LblDptTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDptTime.Size = New System.Drawing.Size(85, 13)
        Me.LblDptTime.TabIndex = 80
        Me.LblDptTime.Text = "Deputed Time :"
        Me.LblDptTime.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProblem
        '
        Me.lblProblem.BackColor = System.Drawing.SystemColors.Control
        Me.lblProblem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProblem.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProblem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProblem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProblem.Location = New System.Drawing.Point(238, 54)
        Me.lblProblem.Name = "lblProblem"
        Me.lblProblem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProblem.Size = New System.Drawing.Size(503, 19)
        Me.lblProblem.TabIndex = 79
        '
        'LblProb
        '
        Me.LblProb.AutoSize = True
        Me.LblProb.BackColor = System.Drawing.SystemColors.Control
        Me.LblProb.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblProb.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProb.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblProb.Location = New System.Drawing.Point(64, 58)
        Me.LblProb.Name = "LblProb"
        Me.LblProb.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblProb.Size = New System.Drawing.Size(50, 13)
        Me.LblProb.TabIndex = 78
        Me.LblProb.Text = "Reason :"
        Me.LblProb.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(544, 126)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "Total Time :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDeputedPerson
        '
        Me.lblDeputedPerson.AutoSize = True
        Me.lblDeputedPerson.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeputedPerson.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeputedPerson.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeputedPerson.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeputedPerson.Location = New System.Drawing.Point(4, 14)
        Me.lblDeputedPerson.Name = "lblDeputedPerson"
        Me.lblDeputedPerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeputedPerson.Size = New System.Drawing.Size(94, 13)
        Me.lblDeputedPerson.TabIndex = 68
        Me.lblDeputedPerson.Text = "Deputed Person :"
        Me.lblDeputedPerson.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblDeptDate
        '
        Me.LblDeptDate.AutoSize = True
        Me.LblDeptDate.BackColor = System.Drawing.SystemColors.Control
        Me.LblDeptDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDeptDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDeptDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblDeptDate.Location = New System.Drawing.Point(4, 36)
        Me.LblDeptDate.Name = "LblDeptDate"
        Me.LblDeptDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDeptDate.Size = New System.Drawing.Size(84, 13)
        Me.LblDeptDate.TabIndex = 67
        Me.LblDeptDate.Text = "Deputed Date :"
        Me.LblDeptDate.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbldeptremarks
        '
        Me.lbldeptremarks.AutoSize = True
        Me.lbldeptremarks.BackColor = System.Drawing.SystemColors.Control
        Me.lbldeptremarks.Cursor = System.Windows.Forms.Cursors.Default
        Me.lbldeptremarks.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldeptremarks.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lbldeptremarks.Location = New System.Drawing.Point(31, 80)
        Me.lbldeptremarks.Name = "lbldeptremarks"
        Me.lbldeptremarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lbldeptremarks.Size = New System.Drawing.Size(77, 13)
        Me.lbldeptremarks.TabIndex = 66
        Me.lbldeptremarks.Text = "Action Taken :"
        Me.lbldeptremarks.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCompDate
        '
        Me.lblCompDate.AutoSize = True
        Me.lblCompDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblCompDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompDate.Location = New System.Drawing.Point(4, 126)
        Me.lblCompDate.Name = "lblCompDate"
        Me.lblCompDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompDate.Size = New System.Drawing.Size(100, 13)
        Me.lblCompDate.TabIndex = 65
        Me.lblCompDate.Text = "Completion Date :"
        Me.lblCompDate.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSlipRecd
        '
        Me.lblSlipRecd.AutoSize = True
        Me.lblSlipRecd.BackColor = System.Drawing.SystemColors.Control
        Me.lblSlipRecd.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSlipRecd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSlipRecd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSlipRecd.Location = New System.Drawing.Point(10, 102)
        Me.lblSlipRecd.Name = "lblSlipRecd"
        Me.lblSlipRecd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSlipRecd.Size = New System.Drawing.Size(97, 13)
        Me.lblSlipRecd.TabIndex = 64
        Me.lblSlipRecd.Text = "Slip Received By :"
        Me.lblSlipRecd.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSlipRecvdBy
        '
        Me.lblSlipRecvdBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblSlipRecvdBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSlipRecvdBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSlipRecvdBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSlipRecvdBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSlipRecvdBy.Location = New System.Drawing.Point(238, 98)
        Me.lblSlipRecvdBy.Name = "lblSlipRecvdBy"
        Me.lblSlipRecvdBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSlipRecvdBy.Size = New System.Drawing.Size(503, 19)
        Me.lblSlipRecvdBy.TabIndex = 63
        '
        'lblDeputPerson
        '
        Me.lblDeputPerson.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeputPerson.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeputPerson.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeputPerson.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeputPerson.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeputPerson.Location = New System.Drawing.Point(238, 10)
        Me.lblDeputPerson.Name = "lblDeputPerson"
        Me.lblDeputPerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeputPerson.Size = New System.Drawing.Size(503, 19)
        Me.lblDeputPerson.TabIndex = 62
        '
        'txtSlipNo
        '
        Me.txtSlipNo.AcceptsReturn = True
        Me.txtSlipNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSlipNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSlipNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtSlipNo.Location = New System.Drawing.Point(118, 10)
        Me.txtSlipNo.MaxLength = 0
        Me.txtSlipNo.Name = "txtSlipNo"
        Me.txtSlipNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSlipNo.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipNo.TabIndex = 0
        '
        'fraComplainer
        '
        Me.fraComplainer.BackColor = System.Drawing.SystemColors.Control
        Me.fraComplainer.Controls.Add(Me.cmdSearchFromDept)
        Me.fraComplainer.Controls.Add(Me.txtToDept)
        Me.fraComplainer.Controls.Add(Me.cmdSearchToDept)
        Me.fraComplainer.Controls.Add(Me.txtFromDept)
        Me.fraComplainer.Controls.Add(Me.txtMachineNo)
        Me.fraComplainer.Controls.Add(Me.cmdSearchMacNo)
        Me.fraComplainer.Controls.Add(Me.txtCompldBy)
        Me.fraComplainer.Controls.Add(Me.cmdSearchCompldBy)
        Me.fraComplainer.Controls.Add(Me.txtReason)
        Me.fraComplainer.Controls.Add(Me.lblTdd)
        Me.fraComplainer.Controls.Add(Me.Lbl12)
        Me.fraComplainer.Controls.Add(Me.lblMac)
        Me.fraComplainer.Controls.Add(Me.lblCompl)
        Me.fraComplainer.Controls.Add(Me.LblReason)
        Me.fraComplainer.Controls.Add(Me.lblFromDept)
        Me.fraComplainer.Controls.Add(Me.lblCompldBy)
        Me.fraComplainer.Controls.Add(Me.lblMachineNo)
        Me.fraComplainer.Controls.Add(Me.lblToDept)
        Me.fraComplainer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraComplainer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraComplainer.Location = New System.Drawing.Point(0, 28)
        Me.fraComplainer.Name = "fraComplainer"
        Me.fraComplainer.Padding = New System.Windows.Forms.Padding(0)
        Me.fraComplainer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraComplainer.Size = New System.Drawing.Size(908, 123)
        Me.fraComplainer.TabIndex = 47
        Me.fraComplainer.TabStop = False
        Me.fraComplainer.Text = "Complainer Site"
        '
        'txtToDept
        '
        Me.txtToDept.AcceptsReturn = True
        Me.txtToDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtToDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtToDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtToDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtToDept.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtToDept.Location = New System.Drawing.Point(118, 32)
        Me.txtToDept.MaxLength = 0
        Me.txtToDept.Name = "txtToDept"
        Me.txtToDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtToDept.Size = New System.Drawing.Size(93, 20)
        Me.txtToDept.TabIndex = 5
        '
        'txtFromDept
        '
        Me.txtFromDept.AcceptsReturn = True
        Me.txtFromDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtFromDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFromDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFromDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFromDept.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtFromDept.Location = New System.Drawing.Point(118, 10)
        Me.txtFromDept.MaxLength = 0
        Me.txtFromDept.Name = "txtFromDept"
        Me.txtFromDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFromDept.Size = New System.Drawing.Size(93, 20)
        Me.txtFromDept.TabIndex = 4
        '
        'txtMachineNo
        '
        Me.txtMachineNo.AcceptsReturn = True
        Me.txtMachineNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachineNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachineNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtMachineNo.Location = New System.Drawing.Point(118, 54)
        Me.txtMachineNo.MaxLength = 0
        Me.txtMachineNo.Name = "txtMachineNo"
        Me.txtMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachineNo.Size = New System.Drawing.Size(93, 20)
        Me.txtMachineNo.TabIndex = 6
        '
        'txtCompldBy
        '
        Me.txtCompldBy.AcceptsReturn = True
        Me.txtCompldBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompldBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompldBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompldBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompldBy.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtCompldBy.Location = New System.Drawing.Point(118, 76)
        Me.txtCompldBy.MaxLength = 0
        Me.txtCompldBy.Name = "txtCompldBy"
        Me.txtCompldBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompldBy.Size = New System.Drawing.Size(93, 20)
        Me.txtCompldBy.TabIndex = 7
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.Color.Blue
        Me.txtReason.Location = New System.Drawing.Point(118, 98)
        Me.txtReason.MaxLength = 0
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(623, 20)
        Me.txtReason.TabIndex = 8
        '
        'lblTdd
        '
        Me.lblTdd.AutoSize = True
        Me.lblTdd.BackColor = System.Drawing.SystemColors.Control
        Me.lblTdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTdd.Location = New System.Drawing.Point(4, 36)
        Me.lblTdd.Name = "lblTdd"
        Me.lblTdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTdd.Size = New System.Drawing.Size(53, 13)
        Me.lblTdd.TabIndex = 56
        Me.lblTdd.Text = "To Dept :"
        Me.lblTdd.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Lbl12
        '
        Me.Lbl12.AutoSize = True
        Me.Lbl12.BackColor = System.Drawing.SystemColors.Control
        Me.Lbl12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Lbl12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lbl12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl12.Location = New System.Drawing.Point(4, 14)
        Me.Lbl12.Name = "Lbl12"
        Me.Lbl12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Lbl12.Size = New System.Drawing.Size(68, 13)
        Me.Lbl12.TabIndex = 55
        Me.Lbl12.Text = "From Dept :"
        Me.Lbl12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMac
        '
        Me.lblMac.AutoSize = True
        Me.lblMac.BackColor = System.Drawing.SystemColors.Control
        Me.lblMac.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMac.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMac.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMac.Location = New System.Drawing.Point(4, 58)
        Me.lblMac.Name = "lblMac"
        Me.lblMac.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMac.Size = New System.Drawing.Size(73, 13)
        Me.lblMac.TabIndex = 54
        Me.lblMac.Text = "Machine No :"
        Me.lblMac.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCompl
        '
        Me.lblCompl.AutoSize = True
        Me.lblCompl.BackColor = System.Drawing.SystemColors.Control
        Me.lblCompl.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompl.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompl.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompl.Location = New System.Drawing.Point(4, 80)
        Me.lblCompl.Name = "lblCompl"
        Me.lblCompl.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompl.Size = New System.Drawing.Size(91, 13)
        Me.lblCompl.TabIndex = 53
        Me.lblCompl.Text = "Complained By :"
        Me.lblCompl.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblReason
        '
        Me.LblReason.AutoSize = True
        Me.LblReason.BackColor = System.Drawing.SystemColors.Control
        Me.LblReason.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblReason.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReason.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblReason.Location = New System.Drawing.Point(62, 102)
        Me.LblReason.Name = "LblReason"
        Me.LblReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblReason.Size = New System.Drawing.Size(56, 13)
        Me.LblReason.TabIndex = 52
        Me.LblReason.Text = "Problem :"
        Me.LblReason.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFromDept
        '
        Me.lblFromDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblFromDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFromDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFromDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFromDept.Location = New System.Drawing.Point(238, 10)
        Me.lblFromDept.Name = "lblFromDept"
        Me.lblFromDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFromDept.Size = New System.Drawing.Size(503, 19)
        Me.lblFromDept.TabIndex = 51
        '
        'lblCompldBy
        '
        Me.lblCompldBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblCompldBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCompldBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCompldBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCompldBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCompldBy.Location = New System.Drawing.Point(238, 76)
        Me.lblCompldBy.Name = "lblCompldBy"
        Me.lblCompldBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCompldBy.Size = New System.Drawing.Size(503, 19)
        Me.lblCompldBy.TabIndex = 50
        '
        'lblMachineNo
        '
        Me.lblMachineNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblMachineNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMachineNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMachineNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMachineNo.Location = New System.Drawing.Point(238, 54)
        Me.lblMachineNo.Name = "lblMachineNo"
        Me.lblMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMachineNo.Size = New System.Drawing.Size(503, 19)
        Me.lblMachineNo.TabIndex = 49
        '
        'lblToDept
        '
        Me.lblToDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblToDept.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblToDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblToDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblToDept.Location = New System.Drawing.Point(238, 32)
        Me.lblToDept.Name = "lblToDept"
        Me.lblToDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblToDept.Size = New System.Drawing.Size(503, 19)
        Me.lblToDept.TabIndex = 48
        '
        'fraItem
        '
        Me.fraItem.BackColor = System.Drawing.SystemColors.Control
        Me.fraItem.Controls.Add(Me.SprdMain)
        Me.fraItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraItem.Location = New System.Drawing.Point(0, 383)
        Me.fraItem.Name = "fraItem"
        Me.fraItem.Padding = New System.Windows.Forms.Padding(0)
        Me.fraItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraItem.Size = New System.Drawing.Size(911, 196)
        Me.fraItem.TabIndex = 46
        Me.fraItem.TabStop = False
        Me.fraItem.Text = "Item Consumed Details"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 15)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(911, 181)
        Me.SprdMain.TabIndex = 24
        '
        'txtSlipDate
        '
        Me.txtSlipDate.AllowPromptAsInput = False
        Me.txtSlipDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipDate.Location = New System.Drawing.Point(282, 9)
        Me.txtSlipDate.Mask = "##/##/####"
        Me.txtSlipDate.Name = "txtSlipDate"
        Me.txtSlipDate.Size = New System.Drawing.Size(93, 20)
        Me.txtSlipDate.TabIndex = 1
        '
        'txtSlipTime
        '
        Me.txtSlipTime.AllowPromptAsInput = False
        Me.txtSlipTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSlipTime.Location = New System.Drawing.Point(446, 9)
        Me.txtSlipTime.Mask = "##:##"
        Me.txtSlipTime.Name = "txtSlipTime"
        Me.txtSlipTime.Size = New System.Drawing.Size(43, 20)
        Me.txtSlipTime.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(496, 12)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(54, 13)
        Me.Label12.TabIndex = 82
        Me.Label12.Text = "Division :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFormType
        '
        Me.lblFormType.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFormType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFormType.Location = New System.Drawing.Point(506, 14)
        Me.lblFormType.Name = "lblFormType"
        Me.lblFormType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFormType.Size = New System.Drawing.Size(53, 13)
        Me.lblFormType.TabIndex = 73
        Me.lblFormType.Text = "FormType"
        Me.lblFormType.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(76, 13)
        Me.Label7.TabIndex = 60
        Me.Label7.Text = "Slip Number :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(214, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Slip Date :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(378, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 58
        Me.Label9.Text = "Slip Time :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 47
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(911, 577)
        Me.SprdView.TabIndex = 34
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 574)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(911, 45)
        Me.FraMovement.TabIndex = 43
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
        Me.lblMkey.TabIndex = 44
        Me.lblMkey.Text = "lblMkey"
        '
        'frmBreakDownMan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
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
        Me.Name = "frmBreakDownMan"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Break Down Maintenance"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.fraComplainerEnd.ResumeLayout(False)
        Me.fraComplainerEnd.PerformLayout()
        Me.fraComplainee.ResumeLayout(False)
        Me.fraComplainee.PerformLayout()
        Me.fraComplainer.ResumeLayout(False)
        Me.fraComplainer.PerformLayout()
        Me.fraItem.ResumeLayout(False)
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