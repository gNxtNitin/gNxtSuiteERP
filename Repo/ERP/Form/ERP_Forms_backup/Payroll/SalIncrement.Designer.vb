Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSalIncrement
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
    Public WithEvents txtCTC As System.Windows.Forms.TextBox
    Public WithEvents txtNextIncDate As System.Windows.Forms.TextBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents optContBasic As System.Windows.Forms.RadioButton
    Public WithEvents optContCeiling As System.Windows.Forms.RadioButton
    Public WithEvents cbodesignation As System.Windows.Forms.ComboBox
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents txtEmpNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSalary As System.Windows.Forms.Button
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents lblEmpType As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblDesg As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblWEF As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents fraTop As System.Windows.Forms.GroupBox
    Public WithEvents txtAddDays As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboAppMon As System.Windows.Forms.ComboBox
    Public WithEvents cboAppYear As System.Windows.Forms.ComboBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboArrearMonth As System.Windows.Forms.ComboBox
    Public WithEvents cboArrearYear As System.Windows.Forms.ComboBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cboYear As System.Windows.Forms.ComboBox
    Public WithEvents cboMonth As System.Windows.Forms.ComboBox
    Public WithEvents Label24 As System.Windows.Forms.Label
    Public WithEvents Label29 As System.Windows.Forms.Label
    Public WithEvents fraSalMY As System.Windows.Forms.GroupBox
    Public WithEvents txtPreBSalary As System.Windows.Forms.TextBox
    Public WithEvents txtBSalary As System.Windows.Forms.TextBox
    Public WithEvents txtNetSalary As System.Windows.Forms.TextBox
    Public WithEvents txtDeduction As System.Windows.Forms.TextBox
    Public WithEvents txtGSalary As System.Windows.Forms.TextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents Label31 As System.Windows.Forms.Label
    Public WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents Label33 As System.Windows.Forms.Label
    Public WithEvents Label34 As System.Windows.Forms.Label
    Public WithEvents Label35 As System.Windows.Forms.Label
    Public WithEvents grdDeductions As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents sprdEarn As AxFPSpreadADO.AxfpSpread
    Public WithEvents sprdDeduct As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents sprdPerks As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents lblAppDate As System.Windows.Forms.Label
    Public WithEvents lblPBasicSal As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label43 As System.Windows.Forms.Label
    Public WithEvents Label41 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents FraMain As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalIncrement))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchSalary = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraMain = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtPrevForm1BSalary = New System.Windows.Forms.TextBox()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.txtForm1BSalary = New System.Windows.Forms.TextBox()
        Me.txtForm1CTC = New System.Windows.Forms.TextBox()
        Me.txtForm1NetSalary = New System.Windows.Forms.TextBox()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.txtForm1GSalary = New System.Windows.Forms.TextBox()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.txtCTC = New System.Windows.Forms.TextBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtNextIncDate = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.fraTop = New System.Windows.Forms.GroupBox()
        Me.optContCeilingGross = New System.Windows.Forms.RadioButton()
        Me.optContGross = New System.Windows.Forms.RadioButton()
        Me.optContBasic = New System.Windows.Forms.RadioButton()
        Me.optContCeiling = New System.Windows.Forms.RadioButton()
        Me.cbodesignation = New System.Windows.Forms.ComboBox()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.txtEmpNo = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.lblEmpType = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblDesg = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblWEF = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtAddDays = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cboAppMon = New System.Windows.Forms.ComboBox()
        Me.cboAppYear = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.cboArrearMonth = New System.Windows.Forms.ComboBox()
        Me.cboArrearYear = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.fraSalMY = New System.Windows.Forms.GroupBox()
        Me.cboYear = New System.Windows.Forms.ComboBox()
        Me.cboMonth = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtPreBSalary = New System.Windows.Forms.TextBox()
        Me.txtBSalary = New System.Windows.Forms.TextBox()
        Me.txtNetSalary = New System.Windows.Forms.TextBox()
        Me.txtDeduction = New System.Windows.Forms.TextBox()
        Me.txtGSalary = New System.Windows.Forms.TextBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.grdDeductions = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.sprdEarn = New AxFPSpreadADO.AxfpSpread()
        Me.sprdDeduct = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.sprdPerks = New AxFPSpreadADO.AxfpSpread()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblAppDate = New System.Windows.Forms.Label()
        Me.lblPBasicSal = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.FraMain.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.fraTop.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.fraSalMY.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        CType(Me.sprdPerks, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.cmdSearchSalary.Location = New System.Drawing.Point(573, 12)
        Me.cmdSearchSalary.Name = "cmdSearchSalary"
        Me.cmdSearchSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSalary.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchSalary.TabIndex = 4
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
        Me.CmdClose.Location = New System.Drawing.Point(756, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 30
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
        Me.CmdView.Location = New System.Drawing.Point(690, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 29
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
        Me.cmdPrint.Location = New System.Drawing.Point(558, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 27
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
        Me.CmdDelete.Location = New System.Drawing.Point(492, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 26
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
        Me.CmdSave.Location = New System.Drawing.Point(360, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 24
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
        Me.CmdModify.Location = New System.Drawing.Point(294, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 23
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
        Me.CmdAdd.Location = New System.Drawing.Point(228, 12)
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
        Me.FraMain.Controls.Add(Me.Label17)
        Me.FraMain.Controls.Add(Me.txtPrevForm1BSalary)
        Me.FraMain.Controls.Add(Me.Label81)
        Me.FraMain.Controls.Add(Me.txtForm1BSalary)
        Me.FraMain.Controls.Add(Me.txtForm1CTC)
        Me.FraMain.Controls.Add(Me.txtForm1NetSalary)
        Me.FraMain.Controls.Add(Me.Label83)
        Me.FraMain.Controls.Add(Me.Label84)
        Me.FraMain.Controls.Add(Me.txtForm1GSalary)
        Me.FraMain.Controls.Add(Me.Label82)
        Me.FraMain.Controls.Add(Me.txtCTC)
        Me.FraMain.Controls.Add(Me.Frame4)
        Me.FraMain.Controls.Add(Me.fraTop)
        Me.FraMain.Controls.Add(Me.Frame1)
        Me.FraMain.Controls.Add(Me.Frame2)
        Me.FraMain.Controls.Add(Me.Frame3)
        Me.FraMain.Controls.Add(Me.fraSalMY)
        Me.FraMain.Controls.Add(Me.txtPreBSalary)
        Me.FraMain.Controls.Add(Me.txtBSalary)
        Me.FraMain.Controls.Add(Me.txtNetSalary)
        Me.FraMain.Controls.Add(Me.txtDeduction)
        Me.FraMain.Controls.Add(Me.txtGSalary)
        Me.FraMain.Controls.Add(Me.SSTab1)
        Me.FraMain.Controls.Add(Me.Label16)
        Me.FraMain.Controls.Add(Me.lblAppDate)
        Me.FraMain.Controls.Add(Me.lblPBasicSal)
        Me.FraMain.Controls.Add(Me.Label2)
        Me.FraMain.Controls.Add(Me.Label43)
        Me.FraMain.Controls.Add(Me.Label41)
        Me.FraMain.Controls.Add(Me.Label15)
        Me.FraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMain.Location = New System.Drawing.Point(0, -6)
        Me.FraMain.Name = "FraMain"
        Me.FraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMain.Size = New System.Drawing.Size(1106, 578)
        Me.FraMain.TabIndex = 31
        Me.FraMain.TabStop = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(737, 140)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(140, 14)
        Me.Label17.TabIndex = 185
        Me.Label17.Text = "Previous Pay Basic Salary :"
        '
        'txtPrevForm1BSalary
        '
        Me.txtPrevForm1BSalary.AcceptsReturn = True
        Me.txtPrevForm1BSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrevForm1BSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrevForm1BSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrevForm1BSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrevForm1BSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrevForm1BSalary.Location = New System.Drawing.Point(898, 138)
        Me.txtPrevForm1BSalary.MaxLength = 0
        Me.txtPrevForm1BSalary.Name = "txtPrevForm1BSalary"
        Me.txtPrevForm1BSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrevForm1BSalary.Size = New System.Drawing.Size(112, 20)
        Me.txtPrevForm1BSalary.TabIndex = 184
        Me.txtPrevForm1BSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.BackColor = System.Drawing.SystemColors.Control
        Me.Label81.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label81.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label81.Location = New System.Drawing.Point(233, 140)
        Me.Label81.Name = "Label81"
        Me.Label81.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label81.Size = New System.Drawing.Size(95, 14)
        Me.Label81.TabIndex = 183
        Me.Label81.Text = "Pay Basic Salary :"
        '
        'txtForm1BSalary
        '
        Me.txtForm1BSalary.AcceptsReturn = True
        Me.txtForm1BSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1BSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1BSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1BSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1BSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1BSalary.Location = New System.Drawing.Point(346, 138)
        Me.txtForm1BSalary.MaxLength = 0
        Me.txtForm1BSalary.Name = "txtForm1BSalary"
        Me.txtForm1BSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1BSalary.Size = New System.Drawing.Size(112, 20)
        Me.txtForm1BSalary.TabIndex = 182
        Me.txtForm1BSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtForm1CTC
        '
        Me.txtForm1CTC.AcceptsReturn = True
        Me.txtForm1CTC.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1CTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1CTC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1CTC.Enabled = False
        Me.txtForm1CTC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1CTC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1CTC.Location = New System.Drawing.Point(809, 552)
        Me.txtForm1CTC.MaxLength = 0
        Me.txtForm1CTC.Name = "txtForm1CTC"
        Me.txtForm1CTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1CTC.Size = New System.Drawing.Size(109, 20)
        Me.txtForm1CTC.TabIndex = 180
        Me.txtForm1CTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtForm1NetSalary
        '
        Me.txtForm1NetSalary.AcceptsReturn = True
        Me.txtForm1NetSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1NetSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1NetSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1NetSalary.Enabled = False
        Me.txtForm1NetSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1NetSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1NetSalary.Location = New System.Drawing.Point(597, 552)
        Me.txtForm1NetSalary.MaxLength = 0
        Me.txtForm1NetSalary.Name = "txtForm1NetSalary"
        Me.txtForm1NetSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1NetSalary.Size = New System.Drawing.Size(109, 20)
        Me.txtForm1NetSalary.TabIndex = 178
        Me.txtForm1NetSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.BackColor = System.Drawing.SystemColors.Control
        Me.Label83.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label83.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label83.Location = New System.Drawing.Point(728, 555)
        Me.Label83.Name = "Label83"
        Me.Label83.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label83.Size = New System.Drawing.Size(62, 14)
        Me.Label83.TabIndex = 181
        Me.Label83.Text = "Pay C.T.C. :"
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.BackColor = System.Drawing.SystemColors.Control
        Me.Label84.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label84.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label84.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label84.Location = New System.Drawing.Point(495, 555)
        Me.Label84.Name = "Label84"
        Me.Label84.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label84.Size = New System.Drawing.Size(84, 14)
        Me.Label84.TabIndex = 179
        Me.Label84.Text = "Pay Net Salary :"
        '
        'txtForm1GSalary
        '
        Me.txtForm1GSalary.AcceptsReturn = True
        Me.txtForm1GSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtForm1GSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtForm1GSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtForm1GSalary.Enabled = False
        Me.txtForm1GSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtForm1GSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtForm1GSalary.Location = New System.Drawing.Point(116, 552)
        Me.txtForm1GSalary.MaxLength = 0
        Me.txtForm1GSalary.Name = "txtForm1GSalary"
        Me.txtForm1GSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtForm1GSalary.Size = New System.Drawing.Size(109, 20)
        Me.txtForm1GSalary.TabIndex = 176
        Me.txtForm1GSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.BackColor = System.Drawing.SystemColors.Control
        Me.Label82.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label82.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label82.Location = New System.Drawing.Point(-4, 556)
        Me.Label82.Name = "Label82"
        Me.Label82.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label82.Size = New System.Drawing.Size(98, 14)
        Me.Label82.TabIndex = 177
        Me.Label82.Text = "Pay Gross Salary :"
        '
        'txtCTC
        '
        Me.txtCTC.AcceptsReturn = True
        Me.txtCTC.BackColor = System.Drawing.SystemColors.Window
        Me.txtCTC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCTC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCTC.Enabled = False
        Me.txtCTC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCTC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCTC.Location = New System.Drawing.Point(809, 527)
        Me.txtCTC.MaxLength = 0
        Me.txtCTC.Name = "txtCTC"
        Me.txtCTC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCTC.Size = New System.Drawing.Size(109, 20)
        Me.txtCTC.TabIndex = 74
        Me.txtCTC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtNextIncDate)
        Me.Frame4.Controls.Add(Me.Label14)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(760, 6)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(136, 45)
        Me.Frame4.TabIndex = 72
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Next Increment Due"
        '
        'txtNextIncDate
        '
        Me.txtNextIncDate.AcceptsReturn = True
        Me.txtNextIncDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtNextIncDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNextIncDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNextIncDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNextIncDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNextIncDate.Location = New System.Drawing.Point(44, 16)
        Me.txtNextIncDate.MaxLength = 0
        Me.txtNextIncDate.Name = "txtNextIncDate"
        Me.txtNextIncDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNextIncDate.Size = New System.Drawing.Size(87, 20)
        Me.txtNextIncDate.TabIndex = 9
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(4, 18)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(35, 14)
        Me.Label14.TabIndex = 73
        Me.Label14.Text = "Date :"
        '
        'fraTop
        '
        Me.fraTop.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop.Controls.Add(Me.optContCeilingGross)
        Me.fraTop.Controls.Add(Me.optContGross)
        Me.fraTop.Controls.Add(Me.optContBasic)
        Me.fraTop.Controls.Add(Me.optContCeiling)
        Me.fraTop.Controls.Add(Me.cbodesignation)
        Me.fraTop.Controls.Add(Me.txtWEF)
        Me.fraTop.Controls.Add(Me.txtEmpNo)
        Me.fraTop.Controls.Add(Me.cmdSearchSalary)
        Me.fraTop.Controls.Add(Me.TxtName)
        Me.fraTop.Controls.Add(Me.cmdSearch)
        Me.fraTop.Controls.Add(Me.lblEmpType)
        Me.fraTop.Controls.Add(Me.Label13)
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
        Me.fraTop.Size = New System.Drawing.Size(753, 87)
        Me.fraTop.TabIndex = 45
        Me.fraTop.TabStop = False
        '
        'optContCeilingGross
        '
        Me.optContCeilingGross.AutoSize = True
        Me.optContCeilingGross.BackColor = System.Drawing.SystemColors.Control
        Me.optContCeilingGross.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContCeilingGross.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContCeilingGross.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContCeilingGross.Location = New System.Drawing.Point(644, 64)
        Me.optContCeilingGross.Name = "optContCeilingGross"
        Me.optContCeilingGross.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContCeilingGross.Size = New System.Drawing.Size(104, 18)
        Me.optContCeilingGross.TabIndex = 78
        Me.optContCeilingGross.TabStop = True
        Me.optContCeilingGross.Text = "Ceiling on Gross"
        Me.optContCeilingGross.UseVisualStyleBackColor = False
        '
        'optContGross
        '
        Me.optContGross.AutoSize = True
        Me.optContGross.BackColor = System.Drawing.SystemColors.Control
        Me.optContGross.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContGross.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContGross.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContGross.Location = New System.Drawing.Point(480, 64)
        Me.optContGross.Name = "optContGross"
        Me.optContGross.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContGross.Size = New System.Drawing.Size(55, 18)
        Me.optContGross.TabIndex = 77
        Me.optContGross.TabStop = True
        Me.optContGross.Text = "Gross"
        Me.optContGross.UseVisualStyleBackColor = False
        '
        'optContBasic
        '
        Me.optContBasic.AutoSize = True
        Me.optContBasic.BackColor = System.Drawing.SystemColors.Control
        Me.optContBasic.Checked = True
        Me.optContBasic.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContBasic.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContBasic.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContBasic.Location = New System.Drawing.Point(393, 64)
        Me.optContBasic.Name = "optContBasic"
        Me.optContBasic.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContBasic.Size = New System.Drawing.Size(86, 18)
        Me.optContBasic.TabIndex = 7
        Me.optContBasic.TabStop = True
        Me.optContBasic.Text = "Basic Salary"
        Me.optContBasic.UseVisualStyleBackColor = False
        '
        'optContCeiling
        '
        Me.optContCeiling.AutoSize = True
        Me.optContCeiling.BackColor = System.Drawing.SystemColors.Control
        Me.optContCeiling.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContCeiling.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContCeiling.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContCeiling.Location = New System.Drawing.Point(537, 64)
        Me.optContCeiling.Name = "optContCeiling"
        Me.optContCeiling.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContCeiling.Size = New System.Drawing.Size(101, 18)
        Me.optContCeiling.TabIndex = 8
        Me.optContCeiling.TabStop = True
        Me.optContCeiling.Text = "Ceiling on Basic"
        Me.optContCeiling.UseVisualStyleBackColor = False
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
        Me.cbodesignation.Size = New System.Drawing.Size(197, 22)
        Me.cbodesignation.TabIndex = 6
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(485, 12)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(87, 20)
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
        Me.txtEmpNo.Size = New System.Drawing.Size(123, 20)
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
        Me.TxtName.Size = New System.Drawing.Size(511, 20)
        Me.TxtName.TabIndex = 5
        '
        'lblEmpType
        '
        Me.lblEmpType.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblEmpType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmpType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblEmpType.Location = New System.Drawing.Point(318, 16)
        Me.lblEmpType.Name = "lblEmpType"
        Me.lblEmpType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblEmpType.Size = New System.Drawing.Size(31, 17)
        Me.lblEmpType.TabIndex = 76
        Me.lblEmpType.Text = "lblEmpType"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(292, 64)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(100, 14)
        Me.Label13.TabIndex = 71
        Me.Label13.Text = "PF Contribution on :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label7.Location = New System.Drawing.Point(14, 64)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(69, 14)
        Me.Label7.TabIndex = 55
        Me.Label7.Text = "Designation :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDesg
        '
        Me.lblDesg.AutoSize = True
        Me.lblDesg.BackColor = System.Drawing.SystemColors.Control
        Me.lblDesg.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDesg.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDesg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDesg.Location = New System.Drawing.Point(258, 14)
        Me.lblDesg.Name = "lblDesg"
        Me.lblDesg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDesg.Size = New System.Drawing.Size(42, 14)
        Me.lblDesg.TabIndex = 54
        Me.lblDesg.Text = "lblDesg"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(414, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(60, 14)
        Me.Label3.TabIndex = 52
        Me.Label3.Text = "WEF Date :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWEF
        '
        Me.lblWEF.BackColor = System.Drawing.SystemColors.Control
        Me.lblWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWEF.Location = New System.Drawing.Point(366, 12)
        Me.lblWEF.Name = "lblWEF"
        Me.lblWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWEF.Size = New System.Drawing.Size(61, 15)
        Me.lblWEF.TabIndex = 48
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
        Me.Label1.Location = New System.Drawing.Point(43, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 14)
        Me.Label1.TabIndex = 47
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
        Me.Label12.TabIndex = 46
        Me.Label12.Text = "Emp Code :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtAddDays)
        Me.Frame1.Controls.Add(Me.Label10)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(760, 46)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(136, 43)
        Me.Frame1.TabIndex = 56
        Me.Frame1.TabStop = False
        '
        'txtAddDays
        '
        Me.txtAddDays.AcceptsReturn = True
        Me.txtAddDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtAddDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAddDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAddDays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAddDays.Location = New System.Drawing.Point(74, 12)
        Me.txtAddDays.MaxLength = 0
        Me.txtAddDays.Name = "txtAddDays"
        Me.txtAddDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAddDays.Size = New System.Drawing.Size(57, 20)
        Me.txtAddDays.TabIndex = 10
        Me.txtAddDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(6, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(64, 14)
        Me.Label10.TabIndex = 70
        Me.Label10.Text = "Add. Days :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cboAppMon)
        Me.Frame2.Controls.Add(Me.cboAppYear)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(248, 88)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(248, 49)
        Me.Frame2.TabIndex = 49
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "With Applicable From"
        '
        'cboAppMon
        '
        Me.cboAppMon.BackColor = System.Drawing.SystemColors.Window
        Me.cboAppMon.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAppMon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAppMon.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAppMon.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAppMon.Location = New System.Drawing.Point(54, 20)
        Me.cboAppMon.Name = "cboAppMon"
        Me.cboAppMon.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAppMon.Size = New System.Drawing.Size(91, 22)
        Me.cboAppMon.TabIndex = 14
        '
        'cboAppYear
        '
        Me.cboAppYear.BackColor = System.Drawing.SystemColors.Window
        Me.cboAppYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAppYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAppYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAppYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAppYear.Location = New System.Drawing.Point(186, 20)
        Me.cboAppYear.Name = "cboAppYear"
        Me.cboAppYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAppYear.Size = New System.Drawing.Size(59, 22)
        Me.cboAppYear.TabIndex = 15
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(42, 14)
        Me.Label5.TabIndex = 51
        Me.Label5.Text = "Month :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(149, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(36, 14)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "Year :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cboArrearMonth)
        Me.Frame3.Controls.Add(Me.cboArrearYear)
        Me.Frame3.Controls.Add(Me.Label8)
        Me.Frame3.Controls.Add(Me.Label6)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(760, 88)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(248, 49)
        Me.Frame3.TabIndex = 32
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Arrear To Paid with the salary of :"
        '
        'cboArrearMonth
        '
        Me.cboArrearMonth.BackColor = System.Drawing.SystemColors.Window
        Me.cboArrearMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboArrearMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboArrearMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboArrearMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboArrearMonth.Location = New System.Drawing.Point(54, 20)
        Me.cboArrearMonth.Name = "cboArrearMonth"
        Me.cboArrearMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboArrearMonth.Size = New System.Drawing.Size(91, 22)
        Me.cboArrearMonth.TabIndex = 16
        '
        'cboArrearYear
        '
        Me.cboArrearYear.BackColor = System.Drawing.SystemColors.Window
        Me.cboArrearYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboArrearYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboArrearYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboArrearYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboArrearYear.Location = New System.Drawing.Point(186, 20)
        Me.cboArrearYear.Name = "cboArrearYear"
        Me.cboArrearYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboArrearYear.Size = New System.Drawing.Size(59, 22)
        Me.cboArrearYear.TabIndex = 17
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(8, 24)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(42, 14)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "Month :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(149, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(36, 14)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Year :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraSalMY
        '
        Me.fraSalMY.BackColor = System.Drawing.SystemColors.Control
        Me.fraSalMY.Controls.Add(Me.cboYear)
        Me.fraSalMY.Controls.Add(Me.cboMonth)
        Me.fraSalMY.Controls.Add(Me.Label24)
        Me.fraSalMY.Controls.Add(Me.Label29)
        Me.fraSalMY.Controls.Add(Me.Label30)
        Me.fraSalMY.Controls.Add(Me.Label31)
        Me.fraSalMY.Controls.Add(Me.Label32)
        Me.fraSalMY.Controls.Add(Me.Label33)
        Me.fraSalMY.Controls.Add(Me.Label34)
        Me.fraSalMY.Controls.Add(Me.Label35)
        Me.fraSalMY.Enabled = False
        Me.fraSalMY.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraSalMY.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraSalMY.Location = New System.Drawing.Point(0, 88)
        Me.fraSalMY.Name = "fraSalMY"
        Me.fraSalMY.Padding = New System.Windows.Forms.Padding(0)
        Me.fraSalMY.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraSalMY.Size = New System.Drawing.Size(248, 49)
        Me.fraSalMY.TabIndex = 41
        Me.fraSalMY.TabStop = False
        Me.fraSalMY.Text = "Increment Due From"
        '
        'cboYear
        '
        Me.cboYear.BackColor = System.Drawing.SystemColors.Window
        Me.cboYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboYear.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboYear.Location = New System.Drawing.Point(186, 20)
        Me.cboYear.Name = "cboYear"
        Me.cboYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboYear.Size = New System.Drawing.Size(59, 22)
        Me.cboYear.TabIndex = 13
        '
        'cboMonth
        '
        Me.cboMonth.BackColor = System.Drawing.SystemColors.Window
        Me.cboMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMonth.Location = New System.Drawing.Point(54, 20)
        Me.cboMonth.Name = "cboMonth"
        Me.cboMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMonth.Size = New System.Drawing.Size(91, 22)
        Me.cboMonth.TabIndex = 12
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.SystemColors.Control
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(149, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(36, 14)
        Me.Label24.TabIndex = 43
        Me.Label24.Text = "Year :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.SystemColors.Control
        Me.Label29.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label29.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label29.Location = New System.Drawing.Point(8, 24)
        Me.Label29.Name = "Label29"
        Me.Label29.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label29.Size = New System.Drawing.Size(42, 14)
        Me.Label29.TabIndex = 42
        Me.Label29.Text = "Month :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.BackColor = System.Drawing.Color.Transparent
        Me.Label30.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label30.Location = New System.Drawing.Point(-4904, 144)
        Me.Label30.Name = "Label30"
        Me.Label30.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label30.Size = New System.Drawing.Size(43, 16)
        Me.Label30.TabIndex = 67
        Me.Label30.Text = "Grade"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.BackColor = System.Drawing.Color.Transparent
        Me.Label31.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label31.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label31.Location = New System.Drawing.Point(-4936, 96)
        Me.Label31.Name = "Label31"
        Me.Label31.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label31.Size = New System.Drawing.Size(70, 16)
        Me.Label31.TabIndex = 66
        Me.Label31.Text = "Department"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.BackColor = System.Drawing.Color.Transparent
        Me.Label32.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label32.Location = New System.Drawing.Point(-4936, 120)
        Me.Label32.Name = "Label32"
        Me.Label32.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label32.Size = New System.Drawing.Size(76, 16)
        Me.Label32.TabIndex = 65
        Me.Label32.Text = "Designation"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label33.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Black
        Me.Label33.Location = New System.Drawing.Point(-4968, 188)
        Me.Label33.Name = "Label33"
        Me.Label33.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label33.Size = New System.Drawing.Size(50, 16)
        Me.Label33.TabIndex = 64
        Me.Label33.Text = "Pincode"
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label34.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label34.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(-4968, 164)
        Me.Label34.Name = "Label34"
        Me.Label34.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label34.Size = New System.Drawing.Size(22, 16)
        Me.Label34.TabIndex = 63
        Me.Label34.Text = "City"
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label35.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label35.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.Black
        Me.Label35.Location = New System.Drawing.Point(-4968, 92)
        Me.Label35.Name = "Label35"
        Me.Label35.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label35.Size = New System.Drawing.Size(51, 16)
        Me.Label35.TabIndex = 62
        Me.Label35.Text = "Address"
        '
        'txtPreBSalary
        '
        Me.txtPreBSalary.AcceptsReturn = True
        Me.txtPreBSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtPreBSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPreBSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPreBSalary.Enabled = False
        Me.txtPreBSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPreBSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPreBSalary.Location = New System.Drawing.Point(630, 138)
        Me.txtPreBSalary.MaxLength = 0
        Me.txtPreBSalary.Name = "txtPreBSalary"
        Me.txtPreBSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPreBSalary.Size = New System.Drawing.Size(101, 20)
        Me.txtPreBSalary.TabIndex = 19
        Me.txtPreBSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBSalary
        '
        Me.txtBSalary.AcceptsReturn = True
        Me.txtBSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtBSalary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBSalary.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBSalary.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBSalary.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBSalary.Location = New System.Drawing.Point(110, 138)
        Me.txtBSalary.MaxLength = 0
        Me.txtBSalary.Name = "txtBSalary"
        Me.txtBSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBSalary.Size = New System.Drawing.Size(101, 20)
        Me.txtBSalary.TabIndex = 18
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
        Me.txtNetSalary.Location = New System.Drawing.Point(597, 527)
        Me.txtNetSalary.MaxLength = 0
        Me.txtNetSalary.Name = "txtNetSalary"
        Me.txtNetSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNetSalary.Size = New System.Drawing.Size(109, 20)
        Me.txtNetSalary.TabIndex = 22
        Me.txtNetSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtDeduction.Location = New System.Drawing.Point(366, 527)
        Me.txtDeduction.MaxLength = 0
        Me.txtDeduction.Name = "txtDeduction"
        Me.txtDeduction.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeduction.Size = New System.Drawing.Size(109, 20)
        Me.txtDeduction.TabIndex = 21
        Me.txtDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.txtGSalary.Location = New System.Drawing.Point(116, 527)
        Me.txtGSalary.MaxLength = 0
        Me.txtGSalary.Name = "txtGSalary"
        Me.txtGSalary.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtGSalary.Size = New System.Drawing.Size(109, 20)
        Me.txtGSalary.TabIndex = 20
        Me.txtGSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 21)
        Me.SSTab1.Location = New System.Drawing.Point(2, 160)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(1106, 356)
        Me.SSTab1.TabIndex = 57
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.grdDeductions)
        Me._SSTab1_TabPage0.Controls.Add(Me.Label11)
        Me._SSTab1_TabPage0.Controls.Add(Me.sprdEarn)
        Me._SSTab1_TabPage0.Controls.Add(Me.sprdDeduct)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(1098, 327)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Salary"
        '
        'grdDeductions
        '
        Me.grdDeductions.BackColor = System.Drawing.SystemColors.Control
        Me.grdDeductions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdDeductions.Cursor = System.Windows.Forms.Cursors.Default
        Me.grdDeductions.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdDeductions.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdDeductions.Location = New System.Drawing.Point(542, 0)
        Me.grdDeductions.Name = "grdDeductions"
        Me.grdDeductions.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.grdDeductions.Size = New System.Drawing.Size(539, 19)
        Me.grdDeductions.TabIndex = 60
        Me.grdDeductions.Text = "Deductions"
        Me.grdDeductions.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(2, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(539, 19)
        Me.Label11.TabIndex = 61
        Me.Label11.Text = "Earnings"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'sprdEarn
        '
        Me.sprdEarn.DataSource = Nothing
        Me.sprdEarn.Location = New System.Drawing.Point(2, 19)
        Me.sprdEarn.Name = "sprdEarn"
        Me.sprdEarn.OcxState = CType(resources.GetObject("sprdEarn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdEarn.Size = New System.Drawing.Size(539, 305)
        Me.sprdEarn.TabIndex = 59
        '
        'sprdDeduct
        '
        Me.sprdDeduct.DataSource = Nothing
        Me.sprdDeduct.Location = New System.Drawing.Point(542, 19)
        Me.sprdDeduct.Name = "sprdDeduct"
        Me.sprdDeduct.OcxState = CType(resources.GetObject("sprdDeduct.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdDeduct.Size = New System.Drawing.Size(539, 305)
        Me.sprdDeduct.TabIndex = 58
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.sprdPerks)
        Me._SSTab1_TabPage1.Controls.Add(Me.Label9)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 25)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(1098, 327)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Perks"
        '
        'sprdPerks
        '
        Me.sprdPerks.DataSource = Nothing
        Me.sprdPerks.Location = New System.Drawing.Point(0, 24)
        Me.sprdPerks.Name = "sprdPerks"
        Me.sprdPerks.OcxState = CType(resources.GetObject("sprdPerks.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdPerks.Size = New System.Drawing.Size(743, 302)
        Me.sprdPerks.TabIndex = 68
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(2, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(740, 19)
        Me.Label9.TabIndex = 69
        Me.Label9.Text = "Perks"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(753, 529)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(52, 16)
        Me.Label16.TabIndex = 75
        Me.Label16.Text = "C.T.C. :"
        '
        'lblAppDate
        '
        Me.lblAppDate.AutoSize = True
        Me.lblAppDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAppDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAppDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAppDate.Location = New System.Drawing.Point(1016, 140)
        Me.lblAppDate.Name = "lblAppDate"
        Me.lblAppDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAppDate.Size = New System.Drawing.Size(59, 14)
        Me.lblAppDate.TabIndex = 53
        Me.lblAppDate.Text = "lblAppDate"
        '
        'lblPBasicSal
        '
        Me.lblPBasicSal.BackColor = System.Drawing.SystemColors.Control
        Me.lblPBasicSal.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPBasicSal.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPBasicSal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPBasicSal.Location = New System.Drawing.Point(464, 140)
        Me.lblPBasicSal.Name = "lblPBasicSal"
        Me.lblPBasicSal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPBasicSal.Size = New System.Drawing.Size(165, 19)
        Me.lblPBasicSal.TabIndex = 36
        Me.lblPBasicSal.Text = "Previous Basic Salary :"
        Me.lblPBasicSal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(6, 140)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(111, 19)
        Me.Label2.TabIndex = 35
        Me.Label2.Text = "Basic Salary :"
        '
        'Label43
        '
        Me.Label43.BackColor = System.Drawing.SystemColors.Control
        Me.Label43.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label43.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label43.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label43.Location = New System.Drawing.Point(503, 529)
        Me.Label43.Name = "Label43"
        Me.Label43.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label43.Size = New System.Drawing.Size(91, 19)
        Me.Label43.TabIndex = 39
        Me.Label43.Text = "Net Salary :"
        '
        'Label41
        '
        Me.Label41.BackColor = System.Drawing.SystemColors.Control
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label41.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label41.Location = New System.Drawing.Point(280, 529)
        Me.Label41.Name = "Label41"
        Me.Label41.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label41.Size = New System.Drawing.Size(85, 19)
        Me.Label41.TabIndex = 38
        Me.Label41.Text = "Deduction :"
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(2, 529)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(107, 19)
        Me.Label15.TabIndex = 37
        Me.Label15.Text = "Gross Salary :"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(1106, 572)
        Me.SprdView.TabIndex = 44
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(1106, 51)
        Me.FraMovement.TabIndex = 40
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 0
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(624, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.cmdPreview.TabIndex = 28
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(426, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 25
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
        Me.Label44.TabIndex = 11
        Me.Label44.Text = "Sex :"
        '
        'frmSalIncrement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.FraMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Label44)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSalIncrement"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = " Salary Increment"
        Me.FraMain.ResumeLayout(False)
        Me.FraMain.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.fraTop.ResumeLayout(False)
        Me.fraTop.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.fraSalMY.ResumeLayout(False)
        Me.fraSalMY.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        CType(Me.sprdEarn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdDeduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        CType(Me.sprdPerks, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents txtForm1CTC As TextBox
    Public WithEvents txtForm1NetSalary As TextBox
    Public WithEvents Label83 As Label
    Public WithEvents Label84 As Label
    Public WithEvents txtForm1GSalary As TextBox
    Public WithEvents Label82 As Label
    Public WithEvents Label81 As Label
    Public WithEvents txtForm1BSalary As TextBox
    Public WithEvents Label17 As Label
    Public WithEvents txtPrevForm1BSalary As TextBox
    Public WithEvents optContGross As RadioButton
    Public WithEvents optContCeilingGross As RadioButton
#End Region
End Class