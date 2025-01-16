Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPerksVarProcess
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
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents cboCategory As System.Windows.Forms.ComboBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents optCardNo As System.Windows.Forms.RadioButton
    Public WithEvents OptName As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents UpDYear As System.Windows.Forms.Label
    Public WithEvents lblRunDate As System.Windows.Forms.Label
    Public WithEvents lblYear As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents sprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdRefresh As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmPerksVarProcess))
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
        Me.Frame6 = New System.Windows.Forms.GroupBox
        Me.chkCategory = New System.Windows.Forms.CheckBox
        Me.cboCategory = New System.Windows.Forms.ComboBox
        Me.Frame4 = New System.Windows.Forms.GroupBox
        Me.chkAll = New System.Windows.Forms.CheckBox
        Me.cboDept = New System.Windows.Forms.ComboBox
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.optCardNo = New System.Windows.Forms.RadioButton
        Me.OptName = New System.Windows.Forms.RadioButton
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.UpDYear = New System.Windows.Forms.Label
        Me.lblRunDate = New System.Windows.Forms.Label
        Me.lblYear = New System.Windows.Forms.Label
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.sprdMain = New AxFPSpreadADO.AxfpSpread
        Me.FraMovement = New System.Windows.Forms.GroupBox
        Me.cmdSave = New System.Windows.Forms.Button
        Me.CmdPreview = New System.Windows.Forms.Button
        Me.cmdPrint = New System.Windows.Forms.Button
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.CmdClose = New System.Windows.Forms.Button
        Me.Report1 = New AxCrystal.AxCrystalReport
        Me.lblBookType = New System.Windows.Forms.Label
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        Me.ToolTip1.Active = True
        CType(Me.sprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Text = "Perks Process Register"
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Icon = CType(resources.GetObject("frmPerksVarProcess.Icon"), System.Drawing.Icon)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
        Me.ControlBox = True
        Me.Enabled = True
        Me.KeyPreview = False
        Me.MaximizeBox = True
        Me.MinimizeBox = True
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = True
        Me.HelpButton = False
        Me.WindowState = System.Windows.Forms.FormWindowState.Normal
        Me.Name = "frmPerksVarProcess"
        Me.Frame6.Text = "Category"
        Me.Frame6.Size = New System.Drawing.Size(215, 39)
        Me.Frame6.Location = New System.Drawing.Point(326, 0)
        Me.Frame6.TabIndex = 19
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Enabled = True
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Visible = True
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.Name = "Frame6"
        Me.chkCategory.Text = "ALL"
        Me.chkCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.Size = New System.Drawing.Size(45, 19)
        Me.chkCategory.Location = New System.Drawing.Point(166, 16)
        Me.chkCategory.TabIndex = 21
        Me.chkCategory.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkCategory.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.CausesValidation = True
        Me.chkCategory.Enabled = True
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkCategory.TabStop = True
        Me.chkCategory.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkCategory.Visible = True
        Me.chkCategory.Name = "chkCategory"
        Me.cboCategory.Size = New System.Drawing.Size(155, 21)
        Me.cboCategory.Location = New System.Drawing.Point(6, 16)
        Me.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCategory.TabIndex = 20
        Me.cboCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCategory.BackColor = System.Drawing.SystemColors.Window
        Me.cboCategory.CausesValidation = True
        Me.cboCategory.Enabled = True
        Me.cboCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCategory.IntegralHeight = True
        Me.cboCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCategory.Sorted = False
        Me.cboCategory.TabStop = True
        Me.cboCategory.Visible = True
        Me.cboCategory.Name = "cboCategory"
        Me.Frame4.Text = "Department"
        Me.Frame4.Size = New System.Drawing.Size(183, 39)
        Me.Frame4.Location = New System.Drawing.Point(142, 0)
        Me.Frame4.TabIndex = 10
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Enabled = True
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Visible = True
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.Name = "Frame4"
        Me.chkAll.Text = "ALL"
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.Size = New System.Drawing.Size(45, 19)
        Me.chkAll.Location = New System.Drawing.Point(136, 14)
        Me.chkAll.TabIndex = 14
        Me.chkAll.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.chkAll.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.CausesValidation = True
        Me.chkAll.Enabled = True
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Appearance = System.Windows.Forms.Appearance.Normal
        Me.chkAll.TabStop = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Unchecked
        Me.chkAll.Visible = True
        Me.chkAll.Name = "chkAll"
        Me.cboDept.Size = New System.Drawing.Size(127, 21)
        Me.cboDept.Location = New System.Drawing.Point(6, 14)
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.TabIndex = 13
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.CausesValidation = True
        Me.cboDept.Enabled = True
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.IntegralHeight = True
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Sorted = False
        Me.cboDept.TabStop = True
        Me.cboDept.Visible = True
        Me.cboDept.Name = "cboDept"
        Me.Frame3.Text = "Order By"
        Me.Frame3.Size = New System.Drawing.Size(141, 39)
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.TabIndex = 9
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Enabled = True
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Visible = True
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.Name = "Frame3"
        Me.optCardNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.optCardNo.Text = "Card No"
        Me.optCardNo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCardNo.Size = New System.Drawing.Size(69, 13)
        Me.optCardNo.Location = New System.Drawing.Point(62, 16)
        Me.optCardNo.TabIndex = 12
        Me.optCardNo.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.optCardNo.BackColor = System.Drawing.SystemColors.Control
        Me.optCardNo.CausesValidation = True
        Me.optCardNo.Enabled = True
        Me.optCardNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCardNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCardNo.Appearance = System.Windows.Forms.Appearance.Normal
        Me.optCardNo.TabStop = True
        Me.optCardNo.Checked = False
        Me.optCardNo.Visible = True
        Me.optCardNo.Name = "optCardNo"
        Me.OptName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OptName.Text = "Name"
        Me.OptName.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptName.Size = New System.Drawing.Size(73, 13)
        Me.OptName.Location = New System.Drawing.Point(6, 16)
        Me.OptName.TabIndex = 11
        Me.OptName.CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OptName.BackColor = System.Drawing.SystemColors.Control
        Me.OptName.CausesValidation = True
        Me.OptName.Enabled = True
        Me.OptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptName.Appearance = System.Windows.Forms.Appearance.Normal
        Me.OptName.TabStop = True
        Me.OptName.Checked = False
        Me.OptName.Visible = True
        Me.OptName.Name = "OptName"
        Me.Frame2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.Size = New System.Drawing.Size(207, 41)
        Me.Frame2.Location = New System.Drawing.Point(542, -2)
        Me.Frame2.TabIndex = 1
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Enabled = True
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Visible = True
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.Name = "Frame2"
        Me.UpDYear.Size = New System.Drawing.Size(16, 30)
        Me.UpDYear.Location = New System.Drawing.Point(188, 10)
        Me.UpDYear.TabIndex = 2
        Me.UpDYear.Text = "UpDYear"
        Me.UpDYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UpDYear.BackColor = System.Drawing.Color.Red
        Me.UpDYear.Name = "UpDYear"
        Me.lblRunDate.Text = "RunDate"
        Me.lblRunDate.Size = New System.Drawing.Size(43, 13)
        Me.lblRunDate.Location = New System.Drawing.Point(10, 20)
        Me.lblRunDate.TabIndex = 8
        Me.lblRunDate.Visible = False
        Me.lblRunDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunDate.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRunDate.Enabled = True
        Me.lblRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRunDate.UseMnemonic = True
        Me.lblRunDate.AutoSize = True
        Me.lblRunDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblYear.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lblYear.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.Size = New System.Drawing.Size(10, 26)
        Me.lblYear.Location = New System.Drawing.Point(178, 12)
        Me.lblYear.TabIndex = 3
        Me.lblYear.BackColor = System.Drawing.SystemColors.Control
        Me.lblYear.Enabled = True
        Me.lblYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblYear.UseMnemonic = True
        Me.lblYear.Visible = True
        Me.lblYear.AutoSize = True
        Me.lblYear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblYear.Name = "lblYear"
        Me.Frame1.Size = New System.Drawing.Size(749, 375)
        Me.Frame1.Location = New System.Drawing.Point(0, 34)
        Me.Frame1.TabIndex = 0
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Enabled = True
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Visible = True
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.Name = "Frame1"
        sprdMain.OcxState = CType(resources.GetObject("sprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdMain.Size = New System.Drawing.Size(743, 363)
        Me.sprdMain.Location = New System.Drawing.Point(2, 8)
        Me.sprdMain.TabIndex = 4
        Me.sprdMain.Name = "sprdMain"
        Me.FraMovement.Size = New System.Drawing.Size(749, 51)
        Me.FraMovement.Location = New System.Drawing.Point(0, 404)
        Me.FraMovement.TabIndex = 5
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Enabled = True
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Visible = True
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.Name = "FraMovement"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.Size = New System.Drawing.Size(80, 34)
        Me.cmdSave.Location = New System.Drawing.Point(242, 12)
        Me.cmdSave.TabIndex = 17
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.CausesValidation = True
        Me.cmdSave.Enabled = True
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.TabStop = True
        Me.cmdSave.Name = "cmdSave"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Size = New System.Drawing.Size(79, 34)
        Me.CmdPreview.Location = New System.Drawing.Point(164, 12)
        Me.CmdPreview.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.CausesValidation = True
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.TabStop = True
        Me.CmdPreview.Name = "CmdPreview"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.Size = New System.Drawing.Size(80, 34)
        Me.cmdPrint.Location = New System.Drawing.Point(84, 12)
        Me.cmdPrint.TabIndex = 15
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.CausesValidation = True
        Me.cmdPrint.Enabled = True
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.TabStop = True
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdRefresh.Text = "&Refresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(80, 34)
        Me.cmdRefresh.Location = New System.Drawing.Point(4, 12)
        Me.cmdRefresh.TabIndex = 7
        Me.cmdRefresh.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdRefresh.BackColor = System.Drawing.SystemColors.Control
        Me.cmdRefresh.CausesValidation = True
        Me.cmdRefresh.Enabled = True
        Me.cmdRefresh.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdRefresh.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdRefresh.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdRefresh.TabStop = True
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.Size = New System.Drawing.Size(80, 34)
        Me.CmdClose.Location = New System.Drawing.Point(666, 12)
        Me.CmdClose.TabIndex = 6
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
        Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Location = New System.Drawing.Point(270, 14)
        Me.Report1.Name = "Report1"
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Size = New System.Drawing.Size(59, 13)
        Me.lblBookType.Location = New System.Drawing.Point(336, 18)
        Me.lblBookType.TabIndex = 18
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopLeft
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Enabled = True
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.UseMnemonic = True
        Me.lblBookType.Visible = True
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lblBookType.Name = "lblBookType"
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Controls.Add(Frame6)
        Me.Controls.Add(Frame4)
        Me.Controls.Add(Frame3)
        Me.Controls.Add(Frame2)
        Me.Controls.Add(Frame1)
        Me.Controls.Add(FraMovement)
        Me.Frame6.Controls.Add(chkCategory)
        Me.Frame6.Controls.Add(cboCategory)
        Me.Frame4.Controls.Add(chkAll)
        Me.Frame4.Controls.Add(cboDept)
        Me.Frame3.Controls.Add(optCardNo)
        Me.Frame3.Controls.Add(OptName)
        Me.Frame2.Controls.Add(UpDYear)
        Me.Frame2.Controls.Add(lblRunDate)
        Me.Frame2.Controls.Add(lblYear)
        Me.Frame1.Controls.Add(sprdMain)
        Me.FraMovement.Controls.Add(cmdSave)
        Me.FraMovement.Controls.Add(CmdPreview)
        Me.FraMovement.Controls.Add(cmdPrint)
        Me.FraMovement.Controls.Add(cmdRefresh)
        Me.FraMovement.Controls.Add(CmdClose)
        Me.FraMovement.Controls.Add(Report1)
        Me.FraMovement.Controls.Add(lblBookType)
        Me.Frame6.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
#End Region
End Class