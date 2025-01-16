Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmLoanMaster
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents txtAmtPerPeriod As System.Windows.Forms.TextBox
    Public WithEvents cboLoanType As System.Windows.Forms.ComboBox
    Public WithEvents txtInstYear As System.Windows.Forms.TextBox
    Public WithEvents txtInstMonth As System.Windows.Forms.TextBox
    Public WithEvents txtLoanDate As System.Windows.Forms.TextBox
    Public WithEvents txtLoanAmt As System.Windows.Forms.TextBox
    Public WithEvents Label42 As System.Windows.Forms.Label
    Public WithEvents lblLoanType As System.Windows.Forms.Label
    Public WithEvents Label40 As System.Windows.Forms.Label
    Public WithEvents Label39 As System.Windows.Forms.Label
    Public WithEvents Label36 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents fraLoanDetail As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblModifyMode As System.Windows.Forms.Label
    Public WithEvents lblADDMode As System.Windows.Forms.Label
    Public WithEvents lblPostAC As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents txtEmpNo As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents fraTop As System.Windows.Forms.GroupBox
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents optInterest As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoanMaster))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.fraLoanDetail = New System.Windows.Forms.GroupBox()
        Me.txtAmtPerPeriod = New System.Windows.Forms.TextBox()
        Me.cboLoanType = New System.Windows.Forms.ComboBox()
        Me.txtInstYear = New System.Windows.Forms.TextBox()
        Me.txtInstMonth = New System.Windows.Forms.TextBox()
        Me.txtLoanDate = New System.Windows.Forms.TextBox()
        Me.txtLoanAmt = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.lblLoanType = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblModifyMode = New System.Windows.Forms.Label()
        Me.lblADDMode = New System.Windows.Forms.Label()
        Me.lblPostAC = New System.Windows.Forms.Label()
        Me.fraTop = New System.Windows.Forms.GroupBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.txtEmpNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.optInterest = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optInterest_1 = New System.Windows.Forms.RadioButton()
        Me._optInterest_0 = New System.Windows.Forms.RadioButton()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.fraLoanDetail.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraTop.SuspendLayout()
        CType(Me.optInterest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdAdd.Location = New System.Drawing.Point(121, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(70, 37)
        Me.CmdAdd.TabIndex = 0
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
        Me.CmdModify.Location = New System.Drawing.Point(192, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(70, 37)
        Me.CmdModify.TabIndex = 91
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
        Me.CmdSave.Location = New System.Drawing.Point(263, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(70, 37)
        Me.CmdSave.TabIndex = 92
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
        Me.CmdDelete.Location = New System.Drawing.Point(405, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(70, 37)
        Me.CmdDelete.TabIndex = 94
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
        Me.CmdView.Location = New System.Drawing.Point(618, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(70, 37)
        Me.CmdView.TabIndex = 97
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
        Me.CmdClose.Location = New System.Drawing.Point(689, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(70, 37)
        Me.CmdClose.TabIndex = 98
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(476, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(70, 37)
        Me.cmdPrint.TabIndex = 95
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdSavePrint.Location = New System.Drawing.Point(334, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(70, 37)
        Me.cmdSavePrint.TabIndex = 93
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
        Me.CmdPreview.Location = New System.Drawing.Point(547, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(70, 37)
        Me.CmdPreview.TabIndex = 96
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'fraLoanDetail
        '
        Me.fraLoanDetail.BackColor = System.Drawing.SystemColors.Control
        Me.fraLoanDetail.Controls.Add(Me.txtAmtPerPeriod)
        Me.fraLoanDetail.Controls.Add(Me.cboLoanType)
        Me.fraLoanDetail.Controls.Add(Me.txtInstYear)
        Me.fraLoanDetail.Controls.Add(Me.txtInstMonth)
        Me.fraLoanDetail.Controls.Add(Me.txtLoanDate)
        Me.fraLoanDetail.Controls.Add(Me.txtLoanAmt)
        Me.fraLoanDetail.Controls.Add(Me.Label42)
        Me.fraLoanDetail.Controls.Add(Me.lblLoanType)
        Me.fraLoanDetail.Controls.Add(Me.Label40)
        Me.fraLoanDetail.Controls.Add(Me.Label39)
        Me.fraLoanDetail.Controls.Add(Me.Label36)
        Me.fraLoanDetail.Controls.Add(Me.Label16)
        Me.fraLoanDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraLoanDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraLoanDetail.Location = New System.Drawing.Point(0, 38)
        Me.fraLoanDetail.Name = "fraLoanDetail"
        Me.fraLoanDetail.Padding = New System.Windows.Forms.Padding(0)
        Me.fraLoanDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraLoanDetail.Size = New System.Drawing.Size(905, 65)
        Me.fraLoanDetail.TabIndex = 17
        Me.fraLoanDetail.TabStop = False
        Me.fraLoanDetail.Text = "Loan Details"
        '
        'txtAmtPerPeriod
        '
        Me.txtAmtPerPeriod.AcceptsReturn = True
        Me.txtAmtPerPeriod.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmtPerPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmtPerPeriod.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmtPerPeriod.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmtPerPeriod.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmtPerPeriod.Location = New System.Drawing.Point(652, 40)
        Me.txtAmtPerPeriod.MaxLength = 0
        Me.txtAmtPerPeriod.Name = "txtAmtPerPeriod"
        Me.txtAmtPerPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmtPerPeriod.Size = New System.Drawing.Size(87, 20)
        Me.txtAmtPerPeriod.TabIndex = 9
        '
        'cboLoanType
        '
        Me.cboLoanType.BackColor = System.Drawing.SystemColors.Window
        Me.cboLoanType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLoanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLoanType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLoanType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLoanType.Location = New System.Drawing.Point(106, 14)
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLoanType.Size = New System.Drawing.Size(163, 22)
        Me.cboLoanType.TabIndex = 2
        '
        'txtInstYear
        '
        Me.txtInstYear.AcceptsReturn = True
        Me.txtInstYear.BackColor = System.Drawing.SystemColors.Window
        Me.txtInstYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstYear.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInstYear.Enabled = False
        Me.txtInstYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInstYear.Location = New System.Drawing.Point(418, 40)
        Me.txtInstYear.MaxLength = 0
        Me.txtInstYear.Name = "txtInstYear"
        Me.txtInstYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInstYear.Size = New System.Drawing.Size(88, 20)
        Me.txtInstYear.TabIndex = 7
        '
        'txtInstMonth
        '
        Me.txtInstMonth.AcceptsReturn = True
        Me.txtInstMonth.BackColor = System.Drawing.SystemColors.Window
        Me.txtInstMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInstMonth.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInstMonth.Enabled = False
        Me.txtInstMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInstMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInstMonth.Location = New System.Drawing.Point(106, 40)
        Me.txtInstMonth.MaxLength = 0
        Me.txtInstMonth.Name = "txtInstMonth"
        Me.txtInstMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInstMonth.Size = New System.Drawing.Size(163, 20)
        Me.txtInstMonth.TabIndex = 5
        '
        'txtLoanDate
        '
        Me.txtLoanDate.AcceptsReturn = True
        Me.txtLoanDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtLoanDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLoanDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLoanDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLoanDate.Location = New System.Drawing.Point(652, 16)
        Me.txtLoanDate.MaxLength = 0
        Me.txtLoanDate.Name = "txtLoanDate"
        Me.txtLoanDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLoanDate.Size = New System.Drawing.Size(87, 20)
        Me.txtLoanDate.TabIndex = 4
        '
        'txtLoanAmt
        '
        Me.txtLoanAmt.AcceptsReturn = True
        Me.txtLoanAmt.BackColor = System.Drawing.SystemColors.Window
        Me.txtLoanAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLoanAmt.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLoanAmt.Enabled = False
        Me.txtLoanAmt.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanAmt.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLoanAmt.Location = New System.Drawing.Point(418, 16)
        Me.txtLoanAmt.MaxLength = 0
        Me.txtLoanAmt.Name = "txtLoanAmt"
        Me.txtLoanAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLoanAmt.Size = New System.Drawing.Size(87, 20)
        Me.txtLoanAmt.TabIndex = 3
        Me.txtLoanAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.BackColor = System.Drawing.SystemColors.Control
        Me.Label42.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label42.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label42.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label42.Location = New System.Drawing.Point(546, 42)
        Me.Label42.Name = "Label42"
        Me.Label42.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label42.Size = New System.Drawing.Size(100, 14)
        Me.Label42.TabIndex = 23
        Me.Label42.Text = "Instalment Amount :"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLoanType
        '
        Me.lblLoanType.BackColor = System.Drawing.SystemColors.Control
        Me.lblLoanType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLoanType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoanType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLoanType.Location = New System.Drawing.Point(2, 18)
        Me.lblLoanType.Name = "lblLoanType"
        Me.lblLoanType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLoanType.Size = New System.Drawing.Size(100, 15)
        Me.lblLoanType.TabIndex = 22
        Me.lblLoanType.Text = "Type :"
        Me.lblLoanType.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.BackColor = System.Drawing.SystemColors.Control
        Me.Label40.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label40.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label40.Location = New System.Drawing.Point(337, 43)
        Me.Label40.Name = "Label40"
        Me.Label40.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label40.Size = New System.Drawing.Size(76, 14)
        Me.Label40.TabIndex = 21
        Me.Label40.Text = "Starting Year :"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.SystemColors.Control
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label39.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label39.Location = New System.Drawing.Point(20, 42)
        Me.Label39.Name = "Label39"
        Me.Label39.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label39.Size = New System.Drawing.Size(82, 14)
        Me.Label39.TabIndex = 20
        Me.Label39.Text = "Starting Month :"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.BackColor = System.Drawing.SystemColors.Control
        Me.Label36.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label36.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label36.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label36.Location = New System.Drawing.Point(611, 18)
        Me.Label36.Name = "Label36"
        Me.Label36.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label36.Size = New System.Drawing.Size(35, 14)
        Me.Label36.TabIndex = 19
        Me.Label36.Text = "Date :"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(363, 19)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(50, 14)
        Me.Label16.TabIndex = 18
        Me.Label16.Text = "Amount :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.SprdMain)
        Me.Frame5.Controls.Add(Me.lblModifyMode)
        Me.Frame5.Controls.Add(Me.lblADDMode)
        Me.Frame5.Controls.Add(Me.lblPostAC)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 104)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(905, 395)
        Me.Frame5.TabIndex = 16
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Loan Scheduling"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(905, 382)
        Me.SprdMain.TabIndex = 10
        '
        'lblModifyMode
        '
        Me.lblModifyMode.AutoSize = True
        Me.lblModifyMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblModifyMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModifyMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModifyMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModifyMode.Location = New System.Drawing.Point(820, 398)
        Me.lblModifyMode.Name = "lblModifyMode"
        Me.lblModifyMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModifyMode.Size = New System.Drawing.Size(75, 14)
        Me.lblModifyMode.TabIndex = 29
        Me.lblModifyMode.Text = "lblModifyMode"
        '
        'lblADDMode
        '
        Me.lblADDMode.AutoSize = True
        Me.lblADDMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblADDMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblADDMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblADDMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblADDMode.Location = New System.Drawing.Point(820, 374)
        Me.lblADDMode.Name = "lblADDMode"
        Me.lblADDMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblADDMode.Size = New System.Drawing.Size(65, 14)
        Me.lblADDMode.TabIndex = 28
        Me.lblADDMode.Text = "lblADDMode"
        '
        'lblPostAC
        '
        Me.lblPostAC.AutoSize = True
        Me.lblPostAC.BackColor = System.Drawing.SystemColors.Control
        Me.lblPostAC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPostAC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPostAC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPostAC.Location = New System.Drawing.Point(824, 426)
        Me.lblPostAC.Name = "lblPostAC"
        Me.lblPostAC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPostAC.Size = New System.Drawing.Size(53, 14)
        Me.lblPostAC.TabIndex = 24
        Me.lblPostAC.Text = "lblPostAC"
        Me.lblPostAC.Visible = False
        '
        'fraTop
        '
        Me.fraTop.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop.Controls.Add(Me.cmdSearch)
        Me.fraTop.Controls.Add(Me.TxtName)
        Me.fraTop.Controls.Add(Me.txtEmpNo)
        Me.fraTop.Controls.Add(Me.Label1)
        Me.fraTop.Controls.Add(Me.Label12)
        Me.fraTop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop.Location = New System.Drawing.Point(0, -4)
        Me.fraTop.Name = "fraTop"
        Me.fraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop.Size = New System.Drawing.Size(907, 41)
        Me.fraTop.TabIndex = 12
        Me.fraTop.TabStop = False
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Enabled = False
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(242, 14)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(365, 20)
        Me.TxtName.TabIndex = 0
        '
        'txtEmpNo
        '
        Me.txtEmpNo.AcceptsReturn = True
        Me.txtEmpNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpNo.Enabled = False
        Me.txtEmpNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpNo.Location = New System.Drawing.Point(108, 14)
        Me.txtEmpNo.MaxLength = 0
        Me.txtEmpNo.Name = "txtEmpNo"
        Me.txtEmpNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpNo.Size = New System.Drawing.Size(73, 20)
        Me.txtEmpNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Menu
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(140, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 14)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Menu
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label12.Location = New System.Drawing.Point(6, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(55, 14)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Card No. :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Menu
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label44.Location = New System.Drawing.Point(222, 42)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(32, 14)
        Me.Label44.TabIndex = 15
        Me.Label44.Text = "Sex :"
        '
        'optInterest
        '
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._optInterest_1)
        Me.Frame2.Controls.Add(Me._optInterest_0)
        Me.Frame2.Controls.Add(Me.txtRate)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 507)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(484, 58)
        Me.Frame2.TabIndex = 31
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Interest Calculation "
        '
        '_optInterest_1
        '
        Me._optInterest_1.AutoSize = True
        Me._optInterest_1.BackColor = System.Drawing.SystemColors.Control
        Me._optInterest_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optInterest_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optInterest_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optInterest_1.Location = New System.Drawing.Point(4, 36)
        Me._optInterest_1.Name = "_optInterest_1"
        Me._optInterest_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optInterest_1.Size = New System.Drawing.Size(85, 18)
        Me._optInterest_1.TabIndex = 33
        Me._optInterest_1.TabStop = True
        Me._optInterest_1.Text = "With Interest"
        Me._optInterest_1.UseVisualStyleBackColor = False
        '
        '_optInterest_0
        '
        Me._optInterest_0.AutoSize = True
        Me._optInterest_0.BackColor = System.Drawing.SystemColors.Control
        Me._optInterest_0.Checked = True
        Me._optInterest_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optInterest_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optInterest_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optInterest_0.Location = New System.Drawing.Point(4, 18)
        Me._optInterest_0.Name = "_optInterest_0"
        Me._optInterest_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optInterest_0.Size = New System.Drawing.Size(103, 18)
        Me._optInterest_0.TabIndex = 32
        Me._optInterest_0.TabStop = True
        Me._optInterest_0.Text = "Without  Interest"
        Me._optInterest_0.UseVisualStyleBackColor = False
        '
        'txtRate
        '
        Me.txtRate.AcceptsReturn = True
        Me.txtRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRate.Enabled = False
        Me.txtRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRate.Location = New System.Drawing.Point(214, 15)
        Me.txtRate.MaxLength = 0
        Me.txtRate.Name = "txtRate"
        Me.txtRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRate.Size = New System.Drawing.Size(53, 20)
        Me.txtRate.TabIndex = 31
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(136, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(77, 14)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Rate / Annual :"
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(4, 568)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(902, 52)
        Me.FraMovement.TabIndex = 115
        Me.FraMovement.TabStop = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(184, 15)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearch.TabIndex = 15
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'frmLoanMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.fraLoanDetail)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.fraTop)
        Me.Controls.Add(Me.Label44)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoanMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Employee Advance & Loan Master"
        Me.fraLoanDetail.ResumeLayout(False)
        Me.fraLoanDetail.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraTop.ResumeLayout(False)
        Me.fraTop.PerformLayout()
        CType(Me.optInterest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents Frame2 As GroupBox
    Public WithEvents _optInterest_1 As RadioButton
    Public WithEvents _optInterest_0 As RadioButton
    Public WithEvents txtRate As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents FraMovement As GroupBox
    Public WithEvents CmdAdd As Button
    Public WithEvents CmdModify As Button
    Public WithEvents CmdSave As Button
    Public WithEvents CmdDelete As Button
    Public WithEvents CmdView As Button
    Public WithEvents CmdClose As Button
    Public WithEvents cmdPrint As Button
    Public WithEvents cmdSavePrint As Button
    Public WithEvents CmdPreview As Button
    Public WithEvents cmdSearch As Button
#End Region
End Class