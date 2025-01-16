Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSalVoucher
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
    Public WithEvents txtSuspendPer As System.Windows.Forms.TextBox
    Public WithEvents txtPaidDays As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtAtcBasic As System.Windows.Forms.TextBox
    Public WithEvents txtPerks As System.Windows.Forms.TextBox
    Public WithEvents chkApproved As System.Windows.Forms.CheckBox
    Public WithEvents cboSalType As System.Windows.Forms.ComboBox
    Public WithEvents cmdSearchSalary As System.Windows.Forms.Button
    Public WithEvents cbodesignation As System.Windows.Forms.ComboBox
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents txtEmpNo As System.Windows.Forms.TextBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblDesg As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblWEF As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents fraTop As System.Windows.Forms.GroupBox
    Public WithEvents txtBSalary As System.Windows.Forms.TextBox
    Public WithEvents txtNetSalary As System.Windows.Forms.TextBox
    Public WithEvents txtDeduction As System.Windows.Forms.TextBox
    Public WithEvents txtGSalary As System.Windows.Forms.TextBox
    Public WithEvents sprdDeduct As AxFPSpreadADO.AxfpSpread
    Public WithEvents sprdPerks As AxFPSpreadADO.AxfpSpread
    Public WithEvents sprdEarn As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents grdDeductions As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdAccountPosting As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label44 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalVoucher))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchSalary = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdAccountPosting = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.txtSuspendPer = New System.Windows.Forms.TextBox()
        Me.txtPaidDays = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtAtcBasic = New System.Windows.Forms.TextBox()
        Me.txtPerks = New System.Windows.Forms.TextBox()
        Me.fraTop = New System.Windows.Forms.GroupBox()
        Me.chkApproved = New System.Windows.Forms.CheckBox()
        Me.cboSalType = New System.Windows.Forms.ComboBox()
        Me.cbodesignation = New System.Windows.Forms.ComboBox()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.txtEmpNo = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblDesg = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblWEF = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtBSalary = New System.Windows.Forms.TextBox()
        Me.txtNetSalary = New System.Windows.Forms.TextBox()
        Me.txtDeduction = New System.Windows.Forms.TextBox()
        Me.txtGSalary = New System.Windows.Forms.TextBox()
        Me.sprdDeduct = New AxFPSpreadADO.AxfpSpread()
        Me.sprdPerks = New AxFPSpreadADO.AxfpSpread()
        Me.sprdEarn = New AxFPSpreadADO.AxfpSpread()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grdDeductions = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.FraMain.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.fraTop.SuspendLayout()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprdPerks, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchSalary
        '
        Me.cmdSearchSalary.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSalary.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSalary.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSalary.Image = CType(resources.GetObject("cmdSearchSalary.Image"), System.Drawing.Image)
        Me.cmdSearchSalary.Location = New System.Drawing.Point(694, 12)
        Me.cmdSearchSalary.Name = "cmdSearchSalary"
        Me.cmdSearchSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSalary.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchSalary.TabIndex = 44
        Me.cmdSearchSalary.TabStop = False
        Me.cmdSearchSalary.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSalary, "Search Salary Define month/year for the employee")
        Me.cmdSearchSalary.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(216, 12)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(666, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 27
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(600, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 26
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(468, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 24
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
        Me.CmdDelete.Location = New System.Drawing.Point(402, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 23
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdAccountPosting
        '
        Me.cmdAccountPosting.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAccountPosting.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAccountPosting.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAccountPosting.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAccountPosting.Image = CType(resources.GetObject("cmdAccountPosting.Image"), System.Drawing.Image)
        Me.cmdAccountPosting.Location = New System.Drawing.Point(336, 10)
        Me.cmdAccountPosting.Name = "cmdAccountPosting"
        Me.cmdAccountPosting.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAccountPosting.Size = New System.Drawing.Size(67, 37)
        Me.cmdAccountPosting.TabIndex = 57
        Me.cmdAccountPosting.Text = "A/c Posting"
        Me.cmdAccountPosting.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAccountPosting, "Add New Record")
        Me.cmdAccountPosting.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(204, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 21
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
        Me.CmdModify.Location = New System.Drawing.Point(138, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 20
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
        Me.CmdAdd.Location = New System.Drawing.Point(72, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraMain
        '
        Me.FraMain.BackColor = System.Drawing.SystemColors.Control
        Me.FraMain.Controls.Add(Me.txtSuspendPer)
        Me.FraMain.Controls.Add(Me.txtPaidDays)
        Me.FraMain.Controls.Add(Me.Frame1)
        Me.FraMain.Controls.Add(Me.txtAtcBasic)
        Me.FraMain.Controls.Add(Me.txtPerks)
        Me.FraMain.Controls.Add(Me.fraTop)
        Me.FraMain.Controls.Add(Me.txtBSalary)
        Me.FraMain.Controls.Add(Me.txtNetSalary)
        Me.FraMain.Controls.Add(Me.txtDeduction)
        Me.FraMain.Controls.Add(Me.txtGSalary)
        Me.FraMain.Controls.Add(Me.sprdDeduct)
        Me.FraMain.Controls.Add(Me.sprdPerks)
        Me.FraMain.Controls.Add(Me.sprdEarn)
        Me.FraMain.Controls.Add(Me.Label16)
        Me.FraMain.Controls.Add(Me.Label14)
        Me.FraMain.Controls.Add(Me.Label6)
        Me.FraMain.Controls.Add(Me.Label5)
        Me.FraMain.Controls.Add(Me.Label4)
        Me.FraMain.Controls.Add(Me.Label2)
        Me.FraMain.Controls.Add(Me.Label11)
        Me.FraMain.Controls.Add(Me.grdDeductions)
        Me.FraMain.Controls.Add(Me.Label43)
        Me.FraMain.Controls.Add(Me.Label41)
        Me.FraMain.Controls.Add(Me.Label15)
        Me.FraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, -6)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(749, 415)
        Me.FraMain.TabIndex = 28
        Me.FraMain.TabStop = False
        '
        'txtSuspendPer
        '
        Me.txtSuspendPer.AcceptsReturn = True
        Me.txtSuspendPer.BackColor = System.Drawing.SystemColors.Window
        Me.txtSuspendPer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuspendPer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSuspendPer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSuspendPer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSuspendPer.Location = New System.Drawing.Point(440, 92)
        Me.txtSuspendPer.MaxLength = 0
        Me.txtSuspendPer.Name = "txtSuspendPer"
        Me.txtSuspendPer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSuspendPer.Size = New System.Drawing.Size(49, 20)
        Me.txtSuspendPer.TabIndex = 9
        Me.txtSuspendPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPaidDays
        '
        Me.txtPaidDays.AcceptsReturn = True
        Me.txtPaidDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaidDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaidDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaidDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaidDays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPaidDays.Location = New System.Drawing.Point(350, 92)
        Me.txtPaidDays.MaxLength = 0
        Me.txtPaidDays.Name = "txtPaidDays"
        Me.txtPaidDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaidDays.Size = New System.Drawing.Size(39, 20)
        Me.txtPaidDays.TabIndex = 8
        Me.txtPaidDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtRemarks)
        Me.Frame1.Controls.Add(Me.txtVDate)
        Me.Frame1.Controls.Add(Me.txtVNo)
        Me.Frame1.Controls.Add(Me.Label13)
        Me.Frame1.Controls.Add(Me.Label10)
        Me.Frame1.Controls.Add(Me.Label9)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(2, 362)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(747, 53)
        Me.Frame1.TabIndex = 51
        Me.Frame1.TabStop = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(375, 10)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(361, 39)
        Me.txtRemarks.TabIndex = 16
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVDate.Location = New System.Drawing.Point(209, 20)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(87, 19)
        Me.txtVDate.TabIndex = 15
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVNo.Location = New System.Drawing.Point(53, 20)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(87, 19)
        Me.txtVNo.TabIndex = 14
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(313, 22)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(55, 14)
        Me.Label13.TabIndex = 54
        Me.Label13.Text = "Remarks :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(157, 22)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(46, 14)
        Me.Label10.TabIndex = 53
        Me.Label10.Text = "V Date :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(8, 22)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(40, 14)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "V No  :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtAtcBasic
        '
        Me.txtAtcBasic.AcceptsReturn = True
        Me.txtAtcBasic.BackColor = System.Drawing.SystemColors.Window
        Me.txtAtcBasic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAtcBasic.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAtcBasic.Enabled = False
        Me.txtAtcBasic.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAtcBasic.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAtcBasic.Location = New System.Drawing.Point(638, 92)
        Me.txtAtcBasic.MaxLength = 0
        Me.txtAtcBasic.Name = "txtAtcBasic"
        Me.txtAtcBasic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAtcBasic.Size = New System.Drawing.Size(101, 20)
        Me.txtAtcBasic.TabIndex = 10
        Me.txtAtcBasic.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPerks
        '
        Me.txtPerks.AcceptsReturn = True
        Me.txtPerks.BackColor = System.Drawing.SystemColors.Window
        Me.txtPerks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPerks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPerks.Enabled = False
        Me.txtPerks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPerks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPerks.Location = New System.Drawing.Point(476, 342)
        Me.txtPerks.MaxLength = 0
        Me.txtPerks.Name = "txtPerks"
        Me.txtPerks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPerks.Size = New System.Drawing.Size(80, 20)
        Me.txtPerks.TabIndex = 46
        '
        'fraTop
        '
        Me.fraTop.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop.Controls.Add(Me.chkApproved)
        Me.fraTop.Controls.Add(Me.cboSalType)
        Me.fraTop.Controls.Add(Me.cmdSearchSalary)
        Me.fraTop.Controls.Add(Me.cbodesignation)
        Me.fraTop.Controls.Add(Me.txtWEF)
        Me.fraTop.Controls.Add(Me.txtEmpNo)
        Me.fraTop.Controls.Add(Me.TxtName)
        Me.fraTop.Controls.Add(Me.cmdSearch)
        Me.fraTop.Controls.Add(Me.Label8)
        Me.fraTop.Controls.Add(Me.Label7)
        Me.fraTop.Controls.Add(Me.lblDesg)
        Me.fraTop.Controls.Add(Me.Label3)
        Me.fraTop.Controls.Add(Me.lblWEF)
        Me.fraTop.Controls.Add(Me.Label1)
        Me.fraTop.Controls.Add(Me.Label12)
        Me.fraTop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop.Location = New System.Drawing.Point(0, 2)
        Me.fraTop.Name = "fraTop"
        Me.fraTop.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop.Size = New System.Drawing.Size(749, 87)
        Me.fraTop.TabIndex = 37
        Me.fraTop.TabStop = False
        '
        'chkApproved
        '
        Me.chkApproved.AutoSize = True
        Me.chkApproved.BackColor = System.Drawing.SystemColors.Control
        Me.chkApproved.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkApproved.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkApproved.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkApproved.Location = New System.Drawing.Point(368, 64)
        Me.chkApproved.Name = "chkApproved"
        Me.chkApproved.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkApproved.Size = New System.Drawing.Size(74, 18)
        Me.chkApproved.TabIndex = 55
        Me.chkApproved.Text = "Approved"
        Me.chkApproved.UseVisualStyleBackColor = False
        '
        'cboSalType
        '
        Me.cboSalType.BackColor = System.Drawing.SystemColors.Window
        Me.cboSalType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSalType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSalType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSalType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cboSalType.Location = New System.Drawing.Point(606, 60)
        Me.cboSalType.Name = "cboSalType"
        Me.cboSalType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSalType.Size = New System.Drawing.Size(89, 22)
        Me.cboSalType.TabIndex = 49
        '
        'cbodesignation
        '
        Me.cbodesignation.BackColor = System.Drawing.SystemColors.Window
        Me.cbodesignation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cbodesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbodesignation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbodesignation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cbodesignation.Location = New System.Drawing.Point(92, 60)
        Me.cbodesignation.Name = "cbodesignation"
        Me.cbodesignation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cbodesignation.Size = New System.Drawing.Size(217, 22)
        Me.cbodesignation.TabIndex = 5
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(605, 12)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(87, 19)
        Me.txtWEF.TabIndex = 3
        '
        'txtEmpNo
        '
        Me.txtEmpNo.AcceptsReturn = True
        Me.txtEmpNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpNo.Location = New System.Drawing.Point(92, 12)
        Me.txtEmpNo.MaxLength = 0
        Me.txtEmpNo.Name = "txtEmpNo"
        Me.txtEmpNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpNo.Size = New System.Drawing.Size(123, 19)
        Me.txtEmpNo.TabIndex = 1
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
        Me.TxtName.Location = New System.Drawing.Point(92, 36)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(635, 19)
        Me.TxtName.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label8.Location = New System.Drawing.Point(526, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(70, 14)
        Me.Label8.TabIndex = 50
        Me.Label8.Text = "Salary Type :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label7.Location = New System.Drawing.Point(-48, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(69, 14)
        Me.Label7.TabIndex = 43
        Me.Label7.Text = "Designation :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDesg
        '
        Me.lblDesg.BackColor = System.Drawing.SystemColors.Control
        Me.lblDesg.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDesg.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDesg.Location = New System.Drawing.Point(258, 14)
        Me.lblDesg.Name = "lblDesg"
        Me.lblDesg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDesg.Size = New System.Drawing.Size(181, 13)
        Me.lblDesg.TabIndex = 42
        Me.lblDesg.Text = "lblDesg"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(526, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Salary Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWEF
        '
        Me.lblWEF.BackColor = System.Drawing.SystemColors.Control
        Me.lblWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWEF.Location = New System.Drawing.Point(464, 8)
        Me.lblWEF.Name = "lblWEF"
        Me.lblWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWEF.Size = New System.Drawing.Size(61, 15)
        Me.lblWEF.TabIndex = 40
        Me.lblWEF.Text = "lblWEF"
        Me.lblWEF.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Menu
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label1.Location = New System.Drawing.Point(22, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 14)
        Me.Label1.TabIndex = 39
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
        Me.Label12.Location = New System.Drawing.Point(22, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(61, 14)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "Emp Code :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBSalary
        '
        Me.txtBSalary.AcceptsReturn = True
        Me.txtBSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtBSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBSalary.Location = New System.Drawing.Point(144, 92)
        Me.txtBSalary.MaxLength = 0
        Me.txtBSalary.Name = "txtBSalary"
        Me.txtBSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBSalary.Size = New System.Drawing.Size(101, 20)
        Me.txtBSalary.TabIndex = 7
        Me.txtBSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNetSalary
        '
        Me.txtNetSalary.AcceptsReturn = True
        Me.txtNetSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtNetSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNetSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNetSalary.Enabled = False
        Me.txtNetSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNetSalary.Location = New System.Drawing.Point(658, 342)
        Me.txtNetSalary.MaxLength = 0
        Me.txtNetSalary.Name = "txtNetSalary"
        Me.txtNetSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetSalary.Size = New System.Drawing.Size(80, 20)
        Me.txtNetSalary.TabIndex = 19
        '
        'txtDeduction
        '
        Me.txtDeduction.AcceptsReturn = True
        Me.txtDeduction.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeduction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeduction.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeduction.Enabled = False
        Me.txtDeduction.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeduction.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeduction.Location = New System.Drawing.Point(300, 342)
        Me.txtDeduction.MaxLength = 0
        Me.txtDeduction.Name = "txtDeduction"
        Me.txtDeduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeduction.Size = New System.Drawing.Size(80, 20)
        Me.txtDeduction.TabIndex = 18
        '
        'txtGSalary
        '
        Me.txtGSalary.AcceptsReturn = True
        Me.txtGSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtGSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtGSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtGSalary.Enabled = False
        Me.txtGSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtGSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtGSalary.Location = New System.Drawing.Point(116, 342)
        Me.txtGSalary.MaxLength = 0
        Me.txtGSalary.Name = "txtGSalary"
        Me.txtGSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSalary.Size = New System.Drawing.Size(76, 20)
        Me.txtGSalary.TabIndex = 17
        '
        'sprdDeduct
        '
        Me.sprdDeduct.DataSource = Nothing
        Me.sprdDeduct.Location = New System.Drawing.Point(250, 137)
        Me.sprdDeduct.Name = "sprdDeduct"
        Me.sprdDeduct.OcxState = CType(resources.GetObject("sprdDeduct.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdDeduct.Size = New System.Drawing.Size(247, 201)
        Me.sprdDeduct.TabIndex = 12
        '
        'sprdPerks
        '
        Me.sprdPerks.DataSource = Nothing
        Me.sprdPerks.Location = New System.Drawing.Point(498, 136)
        Me.sprdPerks.Name = "sprdPerks"
        Me.sprdPerks.OcxState = CType(resources.GetObject("sprdPerks.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdPerks.Size = New System.Drawing.Size(247, 201)
        Me.sprdPerks.TabIndex = 13
        '
        'sprdEarn
        '
        Me.sprdEarn.DataSource = Nothing
        Me.sprdEarn.Location = New System.Drawing.Point(2, 138)
        Me.sprdEarn.Name = "sprdEarn"
        Me.sprdEarn.OcxState = CType(resources.GetObject("sprdEarn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdEarn.Size = New System.Drawing.Size(247, 201)
        Me.sprdEarn.TabIndex = 11
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(414, 94)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(28, 16)
        Me.Label16.TabIndex = 58
        Me.Label16.Text = "% :"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(266, 94)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(87, 17)
        Me.Label14.TabIndex = 56
        Me.Label14.Text = "Paid Days :"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(534, 94)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(111, 19)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "Basic Salary :"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(390, 344)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(85, 19)
        Me.Label5.TabIndex = 47
        Me.Label5.Text = "Perks :"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(498, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(245, 19)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Perks"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(6, 94)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(135, 19)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Paid Basic Salary :"
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(2, 116)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(245, 19)
        Me.Label11.TabIndex = 30
        Me.Label11.Text = "Earnings"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'grdDeductions
        '
        Me.grdDeductions.BackColor = System.Drawing.SystemColors.Control
        Me.grdDeductions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdDeductions.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdDeductions.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDeductions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdDeductions.Location = New System.Drawing.Point(250, 116)
        Me.grdDeductions.Name = "grdDeductions"
        Me.grdDeductions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdDeductions.Size = New System.Drawing.Size(245, 19)
        Me.grdDeductions.TabIndex = 31
        Me.grdDeductions.Text = "Deductions"
        Me.grdDeductions.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(566, 344)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(91, 19)
        Me.Label43.TabIndex = 34
        Me.Label43.Text = "Net Salary :"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label41.Location = New System.Drawing.Point(214, 344)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(85, 19)
        Me.Label41.TabIndex = 33
        Me.Label41.Text = "Deduction :"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(8, 344)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(107, 19)
        Me.Label15.TabIndex = 32
        Me.Label15.Text = "Gross Salary :"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(749, 409)
        Me.SprdView.TabIndex = 36
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdAccountPosting)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 404)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.TabIndex = 35
        Me.FraMovement.TabStop = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(534, 10)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.cmdPreview.TabIndex = 25
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 58
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(270, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 22
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
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
        Me.Label44.TabIndex = 6
        Me.Label44.Text = "Sex :"
        '
        'frmSalVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Label44)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 15)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSalVoucher"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = " Salary Voucher Payment"
        Me.FraMain.ResumeLayout(False)
        Me.FraMain.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.fraTop.ResumeLayout(False)
        Me.fraTop.PerformLayout()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdPerks, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class