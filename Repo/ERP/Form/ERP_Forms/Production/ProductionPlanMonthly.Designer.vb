Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmProductionPlanMonthly
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
    Public WithEvents cmdTwiceMonth As System.Windows.Forms.Button
    Public WithEvents cmdTwiceWeek As System.Windows.Forms.Button
    Public WithEvents cmdOnceWeek As System.Windows.Forms.Button
    Public WithEvents cmdDaily As System.Windows.Forms.Button
    Public WithEvents cmdSched As System.Windows.Forms.Button
    Public WithEvents txtScheduleQty As System.Windows.Forms.TextBox
    Public WithEvents chkStatus As System.Windows.Forms.CheckBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtPlanningQty As System.Windows.Forms.TextBox
    Public WithEvents txtPlanDate As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchProduct As System.Windows.Forms.Button
    Public WithEvents txtProductCode As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchPlanNo As System.Windows.Forms.Button
    Public WithEvents txtPlanNo As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents lblProductCode As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents ADataGrid As VB6.ADODC
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductionPlanMonthly))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchProduct = New System.Windows.Forms.Button()
        Me.cmdSearchPlanNo = New System.Windows.Forms.Button()
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
        Me.txtCapacity = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdTwiceMonth = New System.Windows.Forms.Button()
        Me.cmdTwiceWeek = New System.Windows.Forms.Button()
        Me.cmdOnceWeek = New System.Windows.Forms.Button()
        Me.cmdDaily = New System.Windows.Forms.Button()
        Me.cmdSched = New System.Windows.Forms.Button()
        Me.txtScheduleQty = New System.Windows.Forms.TextBox()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtPlanningQty = New System.Windows.Forms.TextBox()
        Me.txtPlanDate = New System.Windows.Forms.TextBox()
        Me.txtProductCode = New System.Windows.Forms.TextBox()
        Me.txtPlanNo = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.lblProductCode = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ADataGrid = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.fraTop1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchProduct
        '
        Me.cmdSearchProduct.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchProduct.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchProduct.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchProduct.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchProduct.Image = CType(resources.GetObject("cmdSearchProduct.Image"), System.Drawing.Image)
        Me.cmdSearchProduct.Location = New System.Drawing.Point(210, 43)
        Me.cmdSearchProduct.Name = "cmdSearchProduct"
        Me.cmdSearchProduct.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchProduct.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchProduct.TabIndex = 6
        Me.cmdSearchProduct.TabStop = False
        Me.cmdSearchProduct.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchProduct, "Search")
        Me.cmdSearchProduct.UseVisualStyleBackColor = False
        '
        'cmdSearchPlanNo
        '
        Me.cmdSearchPlanNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchPlanNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchPlanNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchPlanNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchPlanNo.Image = CType(resources.GetObject("cmdSearchPlanNo.Image"), System.Drawing.Image)
        Me.cmdSearchPlanNo.Location = New System.Drawing.Point(210, 11)
        Me.cmdSearchPlanNo.Name = "cmdSearchPlanNo"
        Me.cmdSearchPlanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchPlanNo.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchPlanNo.TabIndex = 2
        Me.cmdSearchPlanNo.TabStop = False
        Me.cmdSearchPlanNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchPlanNo, "Search")
        Me.cmdSearchPlanNo.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(743, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 35)
        Me.CmdClose.TabIndex = 26
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
        Me.CmdView.Location = New System.Drawing.Point(677, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 35)
        Me.CmdView.TabIndex = 25
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
        Me.CmdPreview.Location = New System.Drawing.Point(611, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 35)
        Me.CmdPreview.TabIndex = 24
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
        Me.cmdPrint.Location = New System.Drawing.Point(545, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 35)
        Me.cmdPrint.TabIndex = 23
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
        Me.CmdDelete.Location = New System.Drawing.Point(479, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 35)
        Me.CmdDelete.TabIndex = 22
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(413, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 35)
        Me.cmdSavePrint.TabIndex = 21
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
        Me.CmdSave.Location = New System.Drawing.Point(347, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 35)
        Me.CmdSave.TabIndex = 20
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
        Me.CmdModify.Location = New System.Drawing.Point(281, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 35)
        Me.CmdModify.TabIndex = 19
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
        Me.CmdAdd.Location = New System.Drawing.Point(215, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 35)
        Me.CmdAdd.TabIndex = 18
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtCapacity)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.cmdTwiceMonth)
        Me.fraTop1.Controls.Add(Me.cmdTwiceWeek)
        Me.fraTop1.Controls.Add(Me.cmdOnceWeek)
        Me.fraTop1.Controls.Add(Me.cmdDaily)
        Me.fraTop1.Controls.Add(Me.cmdSched)
        Me.fraTop1.Controls.Add(Me.txtScheduleQty)
        Me.fraTop1.Controls.Add(Me.chkStatus)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.txtPlanningQty)
        Me.fraTop1.Controls.Add(Me.txtPlanDate)
        Me.fraTop1.Controls.Add(Me.cmdSearchProduct)
        Me.fraTop1.Controls.Add(Me.txtProductCode)
        Me.fraTop1.Controls.Add(Me.cmdSearchPlanNo)
        Me.fraTop1.Controls.Add(Me.txtPlanNo)
        Me.fraTop1.Controls.Add(Me.SprdMain)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.lblMkey)
        Me.fraTop1.Controls.Add(Me.lblProductCode)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me._lblLabels_0)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(1108, 578)
        Me.fraTop1.TabIndex = 28
        Me.fraTop1.TabStop = False
        '
        'txtCapacity
        '
        Me.txtCapacity.AcceptsReturn = True
        Me.txtCapacity.BackColor = System.Drawing.SystemColors.Window
        Me.txtCapacity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCapacity.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCapacity.Enabled = False
        Me.txtCapacity.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCapacity.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCapacity.Location = New System.Drawing.Point(535, 106)
        Me.txtCapacity.MaxLength = 0
        Me.txtCapacity.Name = "txtCapacity"
        Me.txtCapacity.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCapacity.Size = New System.Drawing.Size(77, 20)
        Me.txtCapacity.TabIndex = 39
        Me.txtCapacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(436, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(95, 14)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Monthly Capacity :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdTwiceMonth
        '
        Me.cmdTwiceMonth.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTwiceMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTwiceMonth.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTwiceMonth.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTwiceMonth.Location = New System.Drawing.Point(624, 104)
        Me.cmdTwiceMonth.Name = "cmdTwiceMonth"
        Me.cmdTwiceMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTwiceMonth.Size = New System.Drawing.Size(125, 23)
        Me.cmdTwiceMonth.TabIndex = 16
        Me.cmdTwiceMonth.Text = "Twice in a Month"
        Me.cmdTwiceMonth.UseVisualStyleBackColor = False
        '
        'cmdTwiceWeek
        '
        Me.cmdTwiceWeek.BackColor = System.Drawing.SystemColors.Control
        Me.cmdTwiceWeek.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdTwiceWeek.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTwiceWeek.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdTwiceWeek.Location = New System.Drawing.Point(624, 82)
        Me.cmdTwiceWeek.Name = "cmdTwiceWeek"
        Me.cmdTwiceWeek.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdTwiceWeek.Size = New System.Drawing.Size(125, 23)
        Me.cmdTwiceWeek.TabIndex = 15
        Me.cmdTwiceWeek.Text = "Twice in a Week"
        Me.cmdTwiceWeek.UseVisualStyleBackColor = False
        '
        'cmdOnceWeek
        '
        Me.cmdOnceWeek.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOnceWeek.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOnceWeek.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOnceWeek.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOnceWeek.Location = New System.Drawing.Point(624, 60)
        Me.cmdOnceWeek.Name = "cmdOnceWeek"
        Me.cmdOnceWeek.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOnceWeek.Size = New System.Drawing.Size(125, 23)
        Me.cmdOnceWeek.TabIndex = 14
        Me.cmdOnceWeek.Text = "Once in a Week"
        Me.cmdOnceWeek.UseVisualStyleBackColor = False
        '
        'cmdDaily
        '
        Me.cmdDaily.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDaily.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDaily.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDaily.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDaily.Location = New System.Drawing.Point(624, 38)
        Me.cmdDaily.Name = "cmdDaily"
        Me.cmdDaily.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDaily.Size = New System.Drawing.Size(125, 23)
        Me.cmdDaily.TabIndex = 13
        Me.cmdDaily.Text = "Daily"
        Me.cmdDaily.UseVisualStyleBackColor = False
        '
        'cmdSched
        '
        Me.cmdSched.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSched.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSched.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSched.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSched.Location = New System.Drawing.Point(624, 16)
        Me.cmdSched.Name = "cmdSched"
        Me.cmdSched.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSched.Size = New System.Drawing.Size(125, 23)
        Me.cmdSched.TabIndex = 12
        Me.cmdSched.Text = "As Per Schedule"
        Me.cmdSched.UseVisualStyleBackColor = False
        '
        'txtScheduleQty
        '
        Me.txtScheduleQty.AcceptsReturn = True
        Me.txtScheduleQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtScheduleQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtScheduleQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtScheduleQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtScheduleQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtScheduleQty.Location = New System.Drawing.Point(349, 76)
        Me.txtScheduleQty.MaxLength = 0
        Me.txtScheduleQty.Name = "txtScheduleQty"
        Me.txtScheduleQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtScheduleQty.Size = New System.Drawing.Size(77, 20)
        Me.txtScheduleQty.TabIndex = 8
        Me.txtScheduleQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(552, 19)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(57, 18)
        Me.chkStatus.TabIndex = 11
        Me.chkStatus.Text = "Status"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(110, 108)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(316, 20)
        Me.txtRemarks.TabIndex = 10
        '
        'txtPlanningQty
        '
        Me.txtPlanningQty.AcceptsReturn = True
        Me.txtPlanningQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlanningQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlanningQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlanningQty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlanningQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlanningQty.Location = New System.Drawing.Point(535, 76)
        Me.txtPlanningQty.MaxLength = 0
        Me.txtPlanningQty.Name = "txtPlanningQty"
        Me.txtPlanningQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlanningQty.Size = New System.Drawing.Size(77, 20)
        Me.txtPlanningQty.TabIndex = 9
        Me.txtPlanningQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPlanDate
        '
        Me.txtPlanDate.AcceptsReturn = True
        Me.txtPlanDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlanDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlanDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlanDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlanDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlanDate.Location = New System.Drawing.Point(110, 76)
        Me.txtPlanDate.MaxLength = 0
        Me.txtPlanDate.Name = "txtPlanDate"
        Me.txtPlanDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlanDate.Size = New System.Drawing.Size(99, 20)
        Me.txtPlanDate.TabIndex = 7
        '
        'txtProductCode
        '
        Me.txtProductCode.AcceptsReturn = True
        Me.txtProductCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtProductCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtProductCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProductCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProductCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProductCode.Location = New System.Drawing.Point(110, 44)
        Me.txtProductCode.MaxLength = 0
        Me.txtProductCode.Name = "txtProductCode"
        Me.txtProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProductCode.Size = New System.Drawing.Size(99, 20)
        Me.txtProductCode.TabIndex = 5
        '
        'txtPlanNo
        '
        Me.txtPlanNo.AcceptsReturn = True
        Me.txtPlanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlanNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlanNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlanNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPlanNo.Location = New System.Drawing.Point(110, 12)
        Me.txtPlanNo.MaxLength = 0
        Me.txtPlanNo.Name = "txtPlanNo"
        Me.txtPlanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlanNo.Size = New System.Drawing.Size(99, 20)
        Me.txtPlanNo.TabIndex = 1
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 142)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1106, 434)
        Me.SprdMain.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(223, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(109, 14)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Schedule Order Qty :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(55, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(49, 14)
        Me.Label2.TabIndex = 37
        Me.Label2.Text = "Remarks"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(458, 80)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(73, 14)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Planning Qty :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(258, 14)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 15)
        Me.lblMkey.TabIndex = 35
        Me.lblMkey.Text = "lblMkey"
        '
        'lblProductCode
        '
        Me.lblProductCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblProductCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblProductCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblProductCode.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblProductCode.Location = New System.Drawing.Point(238, 43)
        Me.lblProductCode.Name = "lblProductCode"
        Me.lblProductCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblProductCode.Size = New System.Drawing.Size(375, 21)
        Me.lblProductCode.TabIndex = 33
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(25, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(79, 14)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Plan For (Date)"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(32, 46)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(72, 14)
        Me._lblLabels_0.TabIndex = 31
        Me._lblLabels_0.Text = "Product Code"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(60, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(44, 14)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "Number"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ADataGrid
        '
        Me.ADataGrid.BackColor = System.Drawing.SystemColors.Window
        Me.ADataGrid.CommandTimeout = 0
        Me.ADataGrid.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataGrid.ConnectionString = Nothing
        Me.ADataGrid.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataGrid.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataGrid.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataGrid.Location = New System.Drawing.Point(0, 56)
        Me.ADataGrid.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataGrid.Name = "ADataGrid"
        Me.ADataGrid.Size = New System.Drawing.Size(113, 23)
        Me.ADataGrid.TabIndex = 29
        Me.ADataGrid.Text = "Adodc1"
        Me.ADataGrid.Visible = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 30
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
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 569)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1108, 51)
        Me.FraMovement.TabIndex = 0
        Me.FraMovement.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(1106, 566)
        Me.SprdView.TabIndex = 27
        '
        'frmProductionPlanMonthly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.fraTop1)
        Me.Controls.Add(Me.ADataGrid)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProductionPlanMonthly"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Production Plan (Monthly Schedule)"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents txtCapacity As TextBox
    Public WithEvents Label3 As Label
#End Region
End Class