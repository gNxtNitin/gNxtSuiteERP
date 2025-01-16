Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmEmpLeaveEntry
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
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents txtPlace As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents TxtEmpName As System.Windows.Forms.TextBox
    Public WithEvents txtAthCode As System.Windows.Forms.TextBox
    Public WithEvents cmdAthSearch As System.Windows.Forms.Button
    Public WithEvents txtRefDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblBalCPL As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lblBalSL As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblBalEL As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblBalCL As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblBalML As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents lblAvlCPL As System.Windows.Forms.Label
    Public WithEvents lblAvlCL As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblAvlEL As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents lblAvlSL As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblAvlML As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents lblCategory As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Fragridview As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpLeaveEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdAthSearch = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.txtPlace = New System.Windows.Forms.TextBox()
        Me.TxtEmpName = New System.Windows.Forms.TextBox()
        Me.txtAthCode = New System.Windows.Forms.TextBox()
        Me.txtRefDate = New System.Windows.Forms.MaskedTextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.lblBalCPL = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblBalSL = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblBalEL = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblBalCL = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBalML = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.lblAvlCPL = New System.Windows.Forms.Label()
        Me.lblAvlCL = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblAvlEL = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblAvlSL = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblAvlML = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Fragridview = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.FraView.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Fragridview.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(168, 40)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 22)
        Me.cmdSearch.TabIndex = 15
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdAthSearch
        '
        Me.cmdAthSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAthSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAthSearch.Enabled = False
        Me.cmdAthSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAthSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAthSearch.Image = CType(resources.GetObject("cmdAthSearch.Image"), System.Drawing.Image)
        Me.cmdAthSearch.Location = New System.Drawing.Point(636, 69)
        Me.cmdAthSearch.Name = "cmdAthSearch"
        Me.cmdAthSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAthSearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdAthSearch.TabIndex = 12
        Me.cmdAthSearch.TabStop = False
        Me.cmdAthSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAthSearch, "Search")
        Me.cmdAthSearch.UseVisualStyleBackColor = False
        Me.cmdAthSearch.Visible = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(131, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(90, 37)
        Me.CmdAdd.TabIndex = 9
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(220, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(90, 37)
        Me.CmdModify.TabIndex = 8
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Refresh Record(s)")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(309, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(90, 37)
        Me.CmdSave.TabIndex = 7
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
        Me.CmdDelete.Location = New System.Drawing.Point(487, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(90, 37)
        Me.CmdDelete.TabIndex = 6
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(754, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(90, 37)
        Me.CmdView.TabIndex = 5
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "Close the Form")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(576, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(90, 37)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Close the Form")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(843, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(90, 37)
        Me.CmdClose.TabIndex = 3
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(665, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(90, 37)
        Me.CmdPreview.TabIndex = 2
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(398, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(90, 37)
        Me.cmdSavePrint.TabIndex = 1
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.FraView.Controls.Add(Me.txtEmpCode)
        Me.FraView.Controls.Add(Me.txtDept)
        Me.FraView.Controls.Add(Me.txtPlace)
        Me.FraView.Controls.Add(Me.cmdSearch)
        Me.FraView.Controls.Add(Me.TxtEmpName)
        Me.FraView.Controls.Add(Me.txtAthCode)
        Me.FraView.Controls.Add(Me.cmdAthSearch)
        Me.FraView.Controls.Add(Me.txtRefDate)
        Me.FraView.Controls.Add(Me.SprdMain)
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.Frame2)
        Me.FraView.Controls.Add(Me.lblCategory)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Controls.Add(Me.Label3)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Controls.Add(Me.Label5)
        Me.FraView.Controls.Add(Me.Label9)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -2)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(1108, 572)
        Me.FraView.TabIndex = 10
        Me.FraView.TabStop = False
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(74, 41)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(92, 20)
        Me.txtEmpCode.TabIndex = 18
        '
        'txtDept
        '
        Me.txtDept.AcceptsReturn = True
        Me.txtDept.BackColor = System.Drawing.SystemColors.Window
        Me.txtDept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDept.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDept.Enabled = False
        Me.txtDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDept.Location = New System.Drawing.Point(74, 68)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(92, 20)
        Me.txtDept.TabIndex = 17
        '
        'txtPlace
        '
        Me.txtPlace.AcceptsReturn = True
        Me.txtPlace.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlace.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlace.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlace.Enabled = False
        Me.txtPlace.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPlace.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlace.Location = New System.Drawing.Point(224, 69)
        Me.txtPlace.MaxLength = 0
        Me.txtPlace.Name = "txtPlace"
        Me.txtPlace.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlace.Size = New System.Drawing.Size(266, 20)
        Me.txtPlace.TabIndex = 16
        Me.txtPlace.Visible = False
        '
        'TxtEmpName
        '
        Me.TxtEmpName.AcceptsReturn = True
        Me.TxtEmpName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtEmpName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtEmpName.Enabled = False
        Me.TxtEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtEmpName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtEmpName.Location = New System.Drawing.Point(198, 41)
        Me.TxtEmpName.MaxLength = 0
        Me.TxtEmpName.Name = "TxtEmpName"
        Me.TxtEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtEmpName.Size = New System.Drawing.Size(293, 20)
        Me.TxtEmpName.TabIndex = 14
        '
        'txtAthCode
        '
        Me.txtAthCode.AcceptsReturn = True
        Me.txtAthCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtAthCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAthCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAthCode.Enabled = False
        Me.txtAthCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAthCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAthCode.Location = New System.Drawing.Point(562, 69)
        Me.txtAthCode.MaxLength = 0
        Me.txtAthCode.Name = "txtAthCode"
        Me.txtAthCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAthCode.Size = New System.Drawing.Size(72, 20)
        Me.txtAthCode.TabIndex = 13
        Me.txtAthCode.Visible = False
        '
        'txtRefDate
        '
        Me.txtRefDate.AllowPromptAsInput = False
        Me.txtRefDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDate.Location = New System.Drawing.Point(74, 14)
        Me.txtRefDate.Mask = "##/##/####"
        Me.txtRefDate.Name = "txtRefDate"
        Me.txtRefDate.Size = New System.Drawing.Size(92, 20)
        Me.txtRefDate.TabIndex = 11
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 97)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(917, 475)
        Me.SprdMain.TabIndex = 26
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.lblBalCPL)
        Me.Frame1.Controls.Add(Me.Label8)
        Me.Frame1.Controls.Add(Me.lblBalSL)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.lblBalEL)
        Me.Frame1.Controls.Add(Me.Label6)
        Me.Frame1.Controls.Add(Me.lblBalCL)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.lblBalML)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(925, 97)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(180, 139)
        Me.Frame1.TabIndex = 27
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Balance Leave (Including This Month)"
        '
        'lblBalCPL
        '
        Me.lblBalCPL.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalCPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalCPL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalCPL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalCPL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalCPL.Location = New System.Drawing.Point(332, 16)
        Me.lblBalCPL.Name = "lblBalCPL"
        Me.lblBalCPL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalCPL.Size = New System.Drawing.Size(43, 15)
        Me.lblBalCPL.TabIndex = 46
        Me.lblBalCPL.Text = "0"
        Me.lblBalCPL.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblBalCPL.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(18, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(79, 14)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Casual Leave :"
        '
        'lblBalSL
        '
        Me.lblBalSL.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblBalSL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalSL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalSL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalSL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalSL.Location = New System.Drawing.Point(102, 54)
        Me.lblBalSL.Name = "lblBalSL"
        Me.lblBalSL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalSL.Size = New System.Drawing.Size(72, 21)
        Me.lblBalSL.TabIndex = 34
        Me.lblBalSL.Text = "0"
        Me.lblBalSL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(31, 58)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(66, 14)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Sick Leave :"
        '
        'lblBalEL
        '
        Me.lblBalEL.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblBalEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalEL.Location = New System.Drawing.Point(102, 79)
        Me.lblBalEL.Name = "lblBalEL"
        Me.lblBalEL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalEL.Size = New System.Drawing.Size(72, 21)
        Me.lblBalEL.TabIndex = 32
        Me.lblBalEL.Text = "0"
        Me.lblBalEL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(29, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(68, 14)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Earn Leave :"
        '
        'lblBalCL
        '
        Me.lblBalCL.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblBalCL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalCL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalCL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalCL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalCL.Location = New System.Drawing.Point(102, 29)
        Me.lblBalCL.Name = "lblBalCL"
        Me.lblBalCL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalCL.Size = New System.Drawing.Size(72, 21)
        Me.lblBalCL.TabIndex = 30
        Me.lblBalCL.Text = "0"
        Me.lblBalCL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(70, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(27, 14)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "ML :"
        '
        'lblBalML
        '
        Me.lblBalML.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblBalML.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBalML.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBalML.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalML.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBalML.Location = New System.Drawing.Point(102, 104)
        Me.lblBalML.Name = "lblBalML"
        Me.lblBalML.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBalML.Size = New System.Drawing.Size(72, 21)
        Me.lblBalML.TabIndex = 28
        Me.lblBalML.Text = "0"
        Me.lblBalML.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.lblAvlCPL)
        Me.Frame2.Controls.Add(Me.lblAvlCL)
        Me.Frame2.Controls.Add(Me.Label13)
        Me.Frame2.Controls.Add(Me.lblAvlEL)
        Me.Frame2.Controls.Add(Me.Label12)
        Me.Frame2.Controls.Add(Me.lblAvlSL)
        Me.Frame2.Controls.Add(Me.Label11)
        Me.Frame2.Controls.Add(Me.Label10)
        Me.Frame2.Controls.Add(Me.lblAvlML)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(925, 238)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(180, 153)
        Me.Frame2.TabIndex = 36
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Leave Availed (Till Month)"
        '
        'lblAvlCPL
        '
        Me.lblAvlCPL.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblAvlCPL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlCPL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlCPL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlCPL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlCPL.Location = New System.Drawing.Point(102, 120)
        Me.lblAvlCPL.Name = "lblAvlCPL"
        Me.lblAvlCPL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlCPL.Size = New System.Drawing.Size(72, 21)
        Me.lblAvlCPL.TabIndex = 47
        Me.lblAvlCPL.Text = "0"
        Me.lblAvlCPL.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblAvlCPL.Visible = False
        '
        'lblAvlCL
        '
        Me.lblAvlCL.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblAvlCL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlCL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlCL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlCL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlCL.Location = New System.Drawing.Point(102, 16)
        Me.lblAvlCL.Name = "lblAvlCL"
        Me.lblAvlCL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlCL.Size = New System.Drawing.Size(72, 21)
        Me.lblAvlCL.TabIndex = 44
        Me.lblAvlCL.Text = "0"
        Me.lblAvlCL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(29, 71)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(68, 14)
        Me.Label13.TabIndex = 43
        Me.Label13.Text = "Earn Leave :"
        '
        'lblAvlEL
        '
        Me.lblAvlEL.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblAvlEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlEL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlEL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlEL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlEL.Location = New System.Drawing.Point(102, 68)
        Me.lblAvlEL.Name = "lblAvlEL"
        Me.lblAvlEL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlEL.Size = New System.Drawing.Size(72, 21)
        Me.lblAvlEL.TabIndex = 42
        Me.lblAvlEL.Text = "0"
        Me.lblAvlEL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(31, 45)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(66, 14)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "Sick Leave :"
        '
        'lblAvlSL
        '
        Me.lblAvlSL.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblAvlSL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlSL.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlSL.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlSL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlSL.Location = New System.Drawing.Point(102, 42)
        Me.lblAvlSL.Name = "lblAvlSL"
        Me.lblAvlSL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlSL.Size = New System.Drawing.Size(72, 21)
        Me.lblAvlSL.TabIndex = 40
        Me.lblAvlSL.Text = "0"
        Me.lblAvlSL.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(18, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(79, 14)
        Me.Label11.TabIndex = 39
        Me.Label11.Text = "Casual Leave :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(70, 98)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(27, 14)
        Me.Label10.TabIndex = 38
        Me.Label10.Text = "ML :"
        '
        'lblAvlML
        '
        Me.lblAvlML.BackColor = System.Drawing.Color.LemonChiffon
        Me.lblAvlML.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAvlML.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAvlML.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAvlML.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAvlML.Location = New System.Drawing.Point(102, 94)
        Me.lblAvlML.Name = "lblAvlML"
        Me.lblAvlML.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAvlML.Size = New System.Drawing.Size(72, 21)
        Me.lblAvlML.TabIndex = 37
        Me.lblAvlML.Text = "0"
        Me.lblAvlML.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCategory
        '
        Me.lblCategory.BackColor = System.Drawing.SystemColors.Control
        Me.lblCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCategory.Location = New System.Drawing.Point(380, 14)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCategory.Size = New System.Drawing.Size(107, 17)
        Me.lblCategory.TabIndex = 45
        Me.lblCategory.Text = "lblCategory"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(9, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(61, 14)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Emp Code :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(15, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(55, 14)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Ref Date :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(35, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(35, 14)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "Dept :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Enabled = False
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(170, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(50, 14)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Reason :"
        Me.Label5.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Enabled = False
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(494, 71)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(61, 14)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Ath. Code :"
        Me.Label9.Visible = False
        '
        'Fragridview
        '
        Me.Fragridview.BackColor = System.Drawing.SystemColors.Control
        Me.Fragridview.Controls.Add(Me.SprdView)
        Me.Fragridview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Fragridview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Fragridview.Location = New System.Drawing.Point(-4, -4)
        Me.Fragridview.Name = "Fragridview"
        Me.Fragridview.Padding = New System.Windows.Forms.Padding(0)
        Me.Fragridview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Fragridview.Size = New System.Drawing.Size(1110, 574)
        Me.Fragridview.TabIndex = 24
        Me.Fragridview.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdView.Location = New System.Drawing.Point(0, 13)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(1110, 561)
        Me.SprdView.TabIndex = 25
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 26
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 568)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1108, 53)
        Me.FraMovement.TabIndex = 0
        Me.FraMovement.TabStop = False
        '
        'frmEmpLeaveEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.Fragridview)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(21, 91)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpLeaveEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Leave Entry"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Fragridview.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
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