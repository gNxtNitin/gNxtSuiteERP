Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmProductProblem
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
    Public WithEvents cmdSearchNumber As System.Windows.Forms.Button
    Public WithEvents txtNumber As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblFormType As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSearchProblemEmp As System.Windows.Forms.Button
    Public WithEvents txtProblemEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtProblemDesc As System.Windows.Forms.TextBox
    Public WithEvents txtItemCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchItem As System.Windows.Forms.Button
    Public WithEvents cmdSearchProblemDept As System.Windows.Forms.Button
    Public WithEvents txtProblemDeptCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSuppCust As System.Windows.Forms.Button
    Public WithEvents txtSuppCustCode As System.Windows.Forms.TextBox
    Public WithEvents txtProblemDate As System.Windows.Forms.TextBox
    Public WithEvents lblItemModel As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents lblProblemEmpName As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblFreq As System.Windows.Forms.Label
    Public WithEvents lblItemName As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents lblProblemDeptName As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents lblSuppCustName As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents fraProblem As System.Windows.Forms.GroupBox
    Public WithEvents txtRootCause As System.Windows.Forms.TextBox
    Public WithEvents txtActionDate As System.Windows.Forms.TextBox
    Public WithEvents txtActionDeptCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchActionDept As System.Windows.Forms.Button
    Public WithEvents txtActionTaken As System.Windows.Forms.TextBox
    Public WithEvents txtEffectiveness As System.Windows.Forms.TextBox
    Public WithEvents txtActionEmpCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchActionEmp As System.Windows.Forms.Button
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label28 As System.Windows.Forms.Label
    Public WithEvents lblActionDeptName As System.Windows.Forms.Label
    Public WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label23 As System.Windows.Forms.Label
    Public WithEvents lblActionEmpName As System.Windows.Forms.Label
    Public WithEvents fraAction As System.Windows.Forms.GroupBox
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
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductProblem))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchNumber = New System.Windows.Forms.Button()
        Me.cmdSearchProblemEmp = New System.Windows.Forms.Button()
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.cmdSearchProblemDept = New System.Windows.Forms.Button()
        Me.cmdSearchSuppCust = New System.Windows.Forms.Button()
        Me.cmdSearchActionDept = New System.Windows.Forms.Button()
        Me.cmdSearchActionEmp = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblFormType = New System.Windows.Forms.Label()
        Me.fraProblem = New System.Windows.Forms.GroupBox()
        Me.txtProblemEmpCode = New System.Windows.Forms.TextBox()
        Me.txtProblemDesc = New System.Windows.Forms.TextBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.txtProblemDeptCode = New System.Windows.Forms.TextBox()
        Me.txtSuppCustCode = New System.Windows.Forms.TextBox()
        Me.txtProblemDate = New System.Windows.Forms.TextBox()
        Me.lblItemModel = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.lblProblemEmpName = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblFreq = New System.Windows.Forms.Label()
        Me.lblItemName = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblProblemDeptName = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblSuppCustName = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.fraAction = New System.Windows.Forms.GroupBox()
        Me.txtRootCause = New System.Windows.Forms.TextBox()
        Me.txtActionDate = New System.Windows.Forms.TextBox()
        Me.txtActionDeptCode = New System.Windows.Forms.TextBox()
        Me.txtActionTaken = New System.Windows.Forms.TextBox()
        Me.txtEffectiveness = New System.Windows.Forms.TextBox()
        Me.txtActionEmpCode = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblActionDeptName = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblActionEmpName = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.fraProblem.SuspendLayout()
        Me.fraAction.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchNumber
        '
        Me.cmdSearchNumber.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchNumber.Image = CType(resources.GetObject("cmdSearchNumber.Image"), System.Drawing.Image)
        Me.cmdSearchNumber.Location = New System.Drawing.Point(253, 14)
        Me.cmdSearchNumber.Name = "cmdSearchNumber"
        Me.cmdSearchNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchNumber.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchNumber.TabIndex = 26
        Me.cmdSearchNumber.TabStop = False
        Me.cmdSearchNumber.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchNumber, "Search")
        Me.cmdSearchNumber.UseVisualStyleBackColor = False
        '
        'cmdSearchProblemEmp
        '
        Me.cmdSearchProblemEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProblemEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProblemEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProblemEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProblemEmp.Image = CType(resources.GetObject("cmdSearchProblemEmp.Image"), System.Drawing.Image)
        Me.cmdSearchProblemEmp.Location = New System.Drawing.Point(253, 160)
        Me.cmdSearchProblemEmp.Name = "cmdSearchProblemEmp"
        Me.cmdSearchProblemEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProblemEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchProblemEmp.TabIndex = 41
        Me.cmdSearchProblemEmp.TabStop = False
        Me.cmdSearchProblemEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProblemEmp, "Search")
        Me.cmdSearchProblemEmp.UseVisualStyleBackColor = False
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(253, 88)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchItem.TabIndex = 37
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'cmdSearchProblemDept
        '
        Me.cmdSearchProblemDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProblemDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProblemDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProblemDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProblemDept.Image = CType(resources.GetObject("cmdSearchProblemDept.Image"), System.Drawing.Image)
        Me.cmdSearchProblemDept.Location = New System.Drawing.Point(253, 40)
        Me.cmdSearchProblemDept.Name = "cmdSearchProblemDept"
        Me.cmdSearchProblemDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProblemDept.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchProblemDept.TabIndex = 34
        Me.cmdSearchProblemDept.TabStop = False
        Me.cmdSearchProblemDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProblemDept, "Search")
        Me.cmdSearchProblemDept.UseVisualStyleBackColor = False
        '
        'cmdSearchSuppCust
        '
        Me.cmdSearchSuppCust.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSuppCust.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSuppCust.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSuppCust.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSuppCust.Image = CType(resources.GetObject("cmdSearchSuppCust.Image"), System.Drawing.Image)
        Me.cmdSearchSuppCust.Location = New System.Drawing.Point(253, 64)
        Me.cmdSearchSuppCust.Name = "cmdSearchSuppCust"
        Me.cmdSearchSuppCust.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSuppCust.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchSuppCust.TabIndex = 31
        Me.cmdSearchSuppCust.TabStop = False
        Me.cmdSearchSuppCust.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSuppCust, "Search")
        Me.cmdSearchSuppCust.UseVisualStyleBackColor = False
        '
        'cmdSearchActionDept
        '
        Me.cmdSearchActionDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchActionDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchActionDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchActionDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchActionDept.Image = CType(resources.GetObject("cmdSearchActionDept.Image"), System.Drawing.Image)
        Me.cmdSearchActionDept.Location = New System.Drawing.Point(253, 40)
        Me.cmdSearchActionDept.Name = "cmdSearchActionDept"
        Me.cmdSearchActionDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchActionDept.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchActionDept.TabIndex = 48
        Me.cmdSearchActionDept.TabStop = False
        Me.cmdSearchActionDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchActionDept, "Search")
        Me.cmdSearchActionDept.UseVisualStyleBackColor = False
        '
        'cmdSearchActionEmp
        '
        Me.cmdSearchActionEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchActionEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchActionEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchActionEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchActionEmp.Image = CType(resources.GetObject("cmdSearchActionEmp.Image"), System.Drawing.Image)
        Me.cmdSearchActionEmp.Location = New System.Drawing.Point(253, 136)
        Me.cmdSearchActionEmp.Name = "cmdSearchActionEmp"
        Me.cmdSearchActionEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchActionEmp.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchActionEmp.TabIndex = 47
        Me.cmdSearchActionEmp.TabStop = False
        Me.cmdSearchActionEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchActionEmp, "Search")
        Me.cmdSearchActionEmp.UseVisualStyleBackColor = False
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
        Me.CmdPreview.TabIndex = 19
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(206, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 16
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
        Me.cmdPrint.Location = New System.Drawing.Point(340, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 18
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
        Me.CmdClose.Location = New System.Drawing.Point(542, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
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
        Me.CmdView.Location = New System.Drawing.Point(474, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 20
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
        Me.CmdDelete.Location = New System.Drawing.Point(272, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 17
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
        Me.CmdSave.Location = New System.Drawing.Point(138, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
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
        Me.CmdModify.Location = New System.Drawing.Point(70, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
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
        Me.CmdAdd.Location = New System.Drawing.Point(4, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 13
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdSearchNumber)
        Me.Frame1.Controls.Add(Me.txtNumber)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.lblFormType)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(613, 41)
        Me.Frame1.TabIndex = 25
        Me.Frame1.TabStop = False
        '
        'txtNumber
        '
        Me.txtNumber.AcceptsReturn = True
        Me.txtNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNumber.Location = New System.Drawing.Point(156, 14)
        Me.txtNumber.MaxLength = 0
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumber.Size = New System.Drawing.Size(93, 19)
        Me.txtNumber.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(5, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(70, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "Slip Number"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFormType
        '
        Me.lblFormType.AutoSize = True
        Me.lblFormType.BackColor = System.Drawing.SystemColors.Control
        Me.lblFormType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFormType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFormType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFormType.Location = New System.Drawing.Point(552, 16)
        Me.lblFormType.Name = "lblFormType"
        Me.lblFormType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFormType.Size = New System.Drawing.Size(13, 14)
        Me.lblFormType.TabIndex = 27
        Me.lblFormType.Text = "P"
        Me.lblFormType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraProblem
        '
        Me.fraProblem.BackColor = System.Drawing.SystemColors.Control
        Me.fraProblem.Controls.Add(Me.cmdSearchProblemEmp)
        Me.fraProblem.Controls.Add(Me.txtProblemEmpCode)
        Me.fraProblem.Controls.Add(Me.txtProblemDesc)
        Me.fraProblem.Controls.Add(Me.txtItemCode)
        Me.fraProblem.Controls.Add(Me.cmdSearchItem)
        Me.fraProblem.Controls.Add(Me.cmdSearchProblemDept)
        Me.fraProblem.Controls.Add(Me.txtProblemDeptCode)
        Me.fraProblem.Controls.Add(Me.cmdSearchSuppCust)
        Me.fraProblem.Controls.Add(Me.txtSuppCustCode)
        Me.fraProblem.Controls.Add(Me.txtProblemDate)
        Me.fraProblem.Controls.Add(Me.lblItemModel)
        Me.fraProblem.Controls.Add(Me._lblLabels_0)
        Me.fraProblem.Controls.Add(Me.lblProblemEmpName)
        Me.fraProblem.Controls.Add(Me.Label17)
        Me.fraProblem.Controls.Add(Me.lblFreq)
        Me.fraProblem.Controls.Add(Me.lblItemName)
        Me.fraProblem.Controls.Add(Me._lblLabels_2)
        Me.fraProblem.Controls.Add(Me.Label19)
        Me.fraProblem.Controls.Add(Me.lblProblemDeptName)
        Me.fraProblem.Controls.Add(Me.Label29)
        Me.fraProblem.Controls.Add(Me.lblSuppCustName)
        Me.fraProblem.Controls.Add(Me.Label8)
        Me.fraProblem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraProblem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraProblem.Location = New System.Drawing.Point(0, 40)
        Me.fraProblem.Name = "fraProblem"
        Me.fraProblem.Padding = New System.Windows.Forms.Padding(0)
        Me.fraProblem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraProblem.Size = New System.Drawing.Size(613, 193)
        Me.fraProblem.TabIndex = 29
        Me.fraProblem.TabStop = False
        Me.fraProblem.Text = "Problem"
        '
        'txtProblemEmpCode
        '
        Me.txtProblemEmpCode.AcceptsReturn = True
        Me.txtProblemEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProblemEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProblemEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProblemEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProblemEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProblemEmpCode.Location = New System.Drawing.Point(155, 160)
        Me.txtProblemEmpCode.MaxLength = 0
        Me.txtProblemEmpCode.Name = "txtProblemEmpCode"
        Me.txtProblemEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProblemEmpCode.Size = New System.Drawing.Size(93, 19)
        Me.txtProblemEmpCode.TabIndex = 6
        '
        'txtProblemDesc
        '
        Me.txtProblemDesc.AcceptsReturn = True
        Me.txtProblemDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtProblemDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProblemDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProblemDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProblemDesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtProblemDesc.Location = New System.Drawing.Point(155, 136)
        Me.txtProblemDesc.MaxLength = 0
        Me.txtProblemDesc.Name = "txtProblemDesc"
        Me.txtProblemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProblemDesc.Size = New System.Drawing.Size(439, 19)
        Me.txtProblemDesc.TabIndex = 5
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemCode.Location = New System.Drawing.Point(155, 88)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(93, 19)
        Me.txtItemCode.TabIndex = 4
        '
        'txtProblemDeptCode
        '
        Me.txtProblemDeptCode.AcceptsReturn = True
        Me.txtProblemDeptCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProblemDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProblemDeptCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProblemDeptCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProblemDeptCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProblemDeptCode.Location = New System.Drawing.Point(155, 40)
        Me.txtProblemDeptCode.MaxLength = 0
        Me.txtProblemDeptCode.Name = "txtProblemDeptCode"
        Me.txtProblemDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProblemDeptCode.Size = New System.Drawing.Size(93, 19)
        Me.txtProblemDeptCode.TabIndex = 2
        '
        'txtSuppCustCode
        '
        Me.txtSuppCustCode.AcceptsReturn = True
        Me.txtSuppCustCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuppCustCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuppCustCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuppCustCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuppCustCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuppCustCode.Location = New System.Drawing.Point(155, 64)
        Me.txtSuppCustCode.MaxLength = 0
        Me.txtSuppCustCode.Name = "txtSuppCustCode"
        Me.txtSuppCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuppCustCode.Size = New System.Drawing.Size(93, 19)
        Me.txtSuppCustCode.TabIndex = 3
        '
        'txtProblemDate
        '
        Me.txtProblemDate.AcceptsReturn = True
        Me.txtProblemDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtProblemDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProblemDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProblemDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProblemDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtProblemDate.Location = New System.Drawing.Point(156, 16)
        Me.txtProblemDate.MaxLength = 0
        Me.txtProblemDate.Name = "txtProblemDate"
        Me.txtProblemDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProblemDate.Size = New System.Drawing.Size(95, 19)
        Me.txtProblemDate.TabIndex = 1
        '
        'lblItemModel
        '
        Me.lblItemModel.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblItemModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemModel.Location = New System.Drawing.Point(155, 112)
        Me.lblItemModel.Name = "lblItemModel"
        Me.lblItemModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemModel.Size = New System.Drawing.Size(439, 19)
        Me.lblItemModel.TabIndex = 45
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(5, 116)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(40, 13)
        Me._lblLabels_0.TabIndex = 44
        Me._lblLabels_0.Text = "Model"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProblemEmpName
        '
        Me.lblProblemEmpName.BackColor = System.Drawing.SystemColors.Control
        Me.lblProblemEmpName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProblemEmpName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProblemEmpName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProblemEmpName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProblemEmpName.Location = New System.Drawing.Point(281, 160)
        Me.lblProblemEmpName.Name = "lblProblemEmpName"
        Me.lblProblemEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProblemEmpName.Size = New System.Drawing.Size(313, 19)
        Me.lblProblemEmpName.TabIndex = 43
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(5, 164)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(62, 13)
        Me.Label17.TabIndex = 42
        Me.Label17.Text = "Entered By"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFreq
        '
        Me.lblFreq.AutoSize = True
        Me.lblFreq.BackColor = System.Drawing.SystemColors.Control
        Me.lblFreq.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFreq.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFreq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFreq.Location = New System.Drawing.Point(5, 140)
        Me.lblFreq.Name = "lblFreq"
        Me.lblFreq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFreq.Size = New System.Drawing.Size(102, 13)
        Me.lblFreq.TabIndex = 40
        Me.lblFreq.Text = "Problem Observed"
        Me.lblFreq.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblItemName
        '
        Me.lblItemName.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblItemName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemName.Location = New System.Drawing.Point(281, 88)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemName.Size = New System.Drawing.Size(313, 19)
        Me.lblItemName.TabIndex = 39
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(5, 92)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(46, 13)
        Me._lblLabels_2.TabIndex = 38
        Me._lblLabels_2.Text = "Product"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(5, 44)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(68, 13)
        Me.Label19.TabIndex = 36
        Me.Label19.Text = "Department"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProblemDeptName
        '
        Me.lblProblemDeptName.BackColor = System.Drawing.SystemColors.Control
        Me.lblProblemDeptName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProblemDeptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProblemDeptName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProblemDeptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProblemDeptName.Location = New System.Drawing.Point(281, 40)
        Me.lblProblemDeptName.Name = "lblProblemDeptName"
        Me.lblProblemDeptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProblemDeptName.Size = New System.Drawing.Size(313, 19)
        Me.lblProblemDeptName.TabIndex = 35
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(5, 68)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(56, 13)
        Me.Label29.TabIndex = 33
        Me.Label29.Text = "Customer"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSuppCustName
        '
        Me.lblSuppCustName.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuppCustName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSuppCustName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSuppCustName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuppCustName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSuppCustName.Location = New System.Drawing.Point(281, 64)
        Me.lblSuppCustName.Name = "lblSuppCustName"
        Me.lblSuppCustName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSuppCustName.Size = New System.Drawing.Size(313, 19)
        Me.lblSuppCustName.TabIndex = 32
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(5, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "Date"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraAction
        '
        Me.fraAction.BackColor = System.Drawing.SystemColors.Control
        Me.fraAction.Controls.Add(Me.txtRootCause)
        Me.fraAction.Controls.Add(Me.txtActionDate)
        Me.fraAction.Controls.Add(Me.txtActionDeptCode)
        Me.fraAction.Controls.Add(Me.cmdSearchActionDept)
        Me.fraAction.Controls.Add(Me.txtActionTaken)
        Me.fraAction.Controls.Add(Me.txtEffectiveness)
        Me.fraAction.Controls.Add(Me.txtActionEmpCode)
        Me.fraAction.Controls.Add(Me.cmdSearchActionEmp)
        Me.fraAction.Controls.Add(Me.Label31)
        Me.fraAction.Controls.Add(Me.Label28)
        Me.fraAction.Controls.Add(Me.lblActionDeptName)
        Me.fraAction.Controls.Add(Me.Label26)
        Me.fraAction.Controls.Add(Me._lblLabels_3)
        Me.fraAction.Controls.Add(Me.Label24)
        Me.fraAction.Controls.Add(Me.Label23)
        Me.fraAction.Controls.Add(Me.lblActionEmpName)
        Me.fraAction.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAction.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraAction.Location = New System.Drawing.Point(0, 232)
        Me.fraAction.Name = "fraAction"
        Me.fraAction.Padding = New System.Windows.Forms.Padding(0)
        Me.fraAction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraAction.Size = New System.Drawing.Size(613, 164)
        Me.fraAction.TabIndex = 46
        Me.fraAction.TabStop = False
        Me.fraAction.Text = "Action Taken"
        '
        'txtRootCause
        '
        Me.txtRootCause.AcceptsReturn = True
        Me.txtRootCause.BackColor = System.Drawing.SystemColors.Window
        Me.txtRootCause.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRootCause.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRootCause.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRootCause.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRootCause.Location = New System.Drawing.Point(155, 64)
        Me.txtRootCause.MaxLength = 0
        Me.txtRootCause.Name = "txtRootCause"
        Me.txtRootCause.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRootCause.Size = New System.Drawing.Size(439, 19)
        Me.txtRootCause.TabIndex = 9
        '
        'txtActionDate
        '
        Me.txtActionDate.AcceptsReturn = True
        Me.txtActionDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtActionDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActionDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActionDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActionDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtActionDate.Location = New System.Drawing.Point(156, 16)
        Me.txtActionDate.MaxLength = 0
        Me.txtActionDate.Name = "txtActionDate"
        Me.txtActionDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActionDate.Size = New System.Drawing.Size(95, 19)
        Me.txtActionDate.TabIndex = 7
        '
        'txtActionDeptCode
        '
        Me.txtActionDeptCode.AcceptsReturn = True
        Me.txtActionDeptCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtActionDeptCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActionDeptCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActionDeptCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActionDeptCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtActionDeptCode.Location = New System.Drawing.Point(155, 40)
        Me.txtActionDeptCode.MaxLength = 0
        Me.txtActionDeptCode.Name = "txtActionDeptCode"
        Me.txtActionDeptCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActionDeptCode.Size = New System.Drawing.Size(93, 19)
        Me.txtActionDeptCode.TabIndex = 8
        '
        'txtActionTaken
        '
        Me.txtActionTaken.AcceptsReturn = True
        Me.txtActionTaken.BackColor = System.Drawing.SystemColors.Window
        Me.txtActionTaken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActionTaken.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActionTaken.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActionTaken.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtActionTaken.Location = New System.Drawing.Point(155, 88)
        Me.txtActionTaken.MaxLength = 0
        Me.txtActionTaken.Name = "txtActionTaken"
        Me.txtActionTaken.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActionTaken.Size = New System.Drawing.Size(439, 19)
        Me.txtActionTaken.TabIndex = 10
        '
        'txtEffectiveness
        '
        Me.txtEffectiveness.AcceptsReturn = True
        Me.txtEffectiveness.BackColor = System.Drawing.SystemColors.Window
        Me.txtEffectiveness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEffectiveness.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEffectiveness.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEffectiveness.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtEffectiveness.Location = New System.Drawing.Point(155, 112)
        Me.txtEffectiveness.MaxLength = 0
        Me.txtEffectiveness.Name = "txtEffectiveness"
        Me.txtEffectiveness.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEffectiveness.Size = New System.Drawing.Size(439, 19)
        Me.txtEffectiveness.TabIndex = 11
        '
        'txtActionEmpCode
        '
        Me.txtActionEmpCode.AcceptsReturn = True
        Me.txtActionEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtActionEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtActionEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtActionEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtActionEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtActionEmpCode.Location = New System.Drawing.Point(155, 136)
        Me.txtActionEmpCode.MaxLength = 0
        Me.txtActionEmpCode.Name = "txtActionEmpCode"
        Me.txtActionEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtActionEmpCode.Size = New System.Drawing.Size(93, 19)
        Me.txtActionEmpCode.TabIndex = 12
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.BackColor = System.Drawing.SystemColors.Control
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(5, 20)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(31, 13)
        Me.Label31.TabIndex = 56
        Me.Label31.Text = "Date"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.SystemColors.Control
        Me.Label28.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label28.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label28.Location = New System.Drawing.Point(5, 68)
        Me.Label28.Name = "Label28"
        Me.Label28.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label28.Size = New System.Drawing.Size(65, 13)
        Me.Label28.TabIndex = 55
        Me.Label28.Text = "Root Cause"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblActionDeptName
        '
        Me.lblActionDeptName.BackColor = System.Drawing.SystemColors.Control
        Me.lblActionDeptName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblActionDeptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblActionDeptName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActionDeptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblActionDeptName.Location = New System.Drawing.Point(281, 40)
        Me.lblActionDeptName.Name = "lblActionDeptName"
        Me.lblActionDeptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblActionDeptName.Size = New System.Drawing.Size(313, 19)
        Me.lblActionDeptName.TabIndex = 54
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.BackColor = System.Drawing.SystemColors.Control
        Me.Label26.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label26.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label26.Location = New System.Drawing.Point(5, 44)
        Me.Label26.Name = "Label26"
        Me.Label26.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label26.Size = New System.Drawing.Size(68, 13)
        Me.Label26.TabIndex = 53
        Me.Label26.Text = "Department"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = True
        Me._lblLabels_3.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_3, CType(3, Short))
        Me._lblLabels_3.Location = New System.Drawing.Point(5, 92)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(126, 13)
        Me._lblLabels_3.TabIndex = 52
        Me._lblLabels_3.Text = "Corrective Action Taken"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(5, 116)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(73, 13)
        Me.Label24.TabIndex = 51
        Me.Label24.Text = "Effectiveness"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.SystemColors.Control
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(5, 140)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(87, 13)
        Me.Label23.TabIndex = 50
        Me.Label23.Text = "Action Taken By"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblActionEmpName
        '
        Me.lblActionEmpName.BackColor = System.Drawing.SystemColors.Control
        Me.lblActionEmpName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblActionEmpName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblActionEmpName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActionEmpName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblActionEmpName.Location = New System.Drawing.Point(281, 136)
        Me.lblActionEmpName.Name = "lblActionEmpName"
        Me.lblActionEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblActionEmpName.Size = New System.Drawing.Size(313, 19)
        Me.lblActionEmpName.TabIndex = 49
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(-178, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 48
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(614, 396)
        Me.SprdView.TabIndex = 22
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 395)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(613, 51)
        Me.FraMovement.TabIndex = 23
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
        Me.lblMkey.TabIndex = 24
        Me.lblMkey.Text = "lblMkey"
        '
        'frmProductProblem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(613, 447)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.fraProblem)
        Me.Controls.Add(Me.fraAction)
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
        Me.Name = "frmProductProblem"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Product Problem Entry"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.fraProblem.ResumeLayout(False)
        Me.fraProblem.PerformLayout()
        Me.fraAction.ResumeLayout(False)
        Me.fraAction.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
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