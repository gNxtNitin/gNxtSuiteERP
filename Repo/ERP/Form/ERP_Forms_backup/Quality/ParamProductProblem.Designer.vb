Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamProductProblem
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
    Public WithEvents chkAllItem As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchItem As System.Windows.Forms.Button
    Public WithEvents txtItem As System.Windows.Forms.TextBox
    Public WithEvents txtDept As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDept As System.Windows.Forms.Button
    Public WithEvents chkAllDept As System.Windows.Forms.CheckBox
    Public WithEvents txtCustomer As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCustomer As System.Windows.Forms.Button
    Public WithEvents chkAllCustomer As System.Windows.Forms.CheckBox
    Public WithEvents lblDeptName As System.Windows.Forms.Label
    Public WithEvents lblMachineName As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents cboProblemDate As System.Windows.Forms.ComboBox
    Public WithEvents txtDate2 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate1 As System.Windows.Forms.Label
    Public WithEvents lblDate2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamProductProblem))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.txtDept = New System.Windows.Forms.TextBox()
        Me.cmdSearchDept = New System.Windows.Forms.Button()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.cmdSearchCustomer = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAllItem = New System.Windows.Forms.CheckBox()
        Me.chkAllDept = New System.Windows.Forms.CheckBox()
        Me.chkAllCustomer = New System.Windows.Forms.CheckBox()
        Me.lblDeptName = New System.Windows.Forms.Label()
        Me.lblMachineName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboProblemDate = New System.Windows.Forms.ComboBox()
        Me.txtDate2 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate1 = New System.Windows.Forms.Label()
        Me.lblDate2 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraAccount.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Enabled = False
        Me.cmdSearchItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(488, 56)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchItem.TabIndex = 20
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'txtItem
        '
        Me.txtItem.AcceptsReturn = True
        Me.txtItem.BackColor = System.Drawing.SystemColors.Window
        Me.txtItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItem.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItem.Enabled = False
        Me.txtItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItem.Location = New System.Drawing.Point(90, 56)
        Me.txtItem.MaxLength = 0
        Me.txtItem.Name = "txtItem"
        Me.txtItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItem.Size = New System.Drawing.Size(397, 19)
        Me.txtItem.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.txtItem, "Press F1 For Help")
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
        Me.txtDept.Location = New System.Drawing.Point(90, 11)
        Me.txtDept.MaxLength = 0
        Me.txtDept.Name = "txtDept"
        Me.txtDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDept.Size = New System.Drawing.Size(397, 19)
        Me.txtDept.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.txtDept, "Press F1 For Help")
        '
        'cmdSearchDept
        '
        Me.cmdSearchDept.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDept.Enabled = False
        Me.cmdSearchDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDept.Image = CType(resources.GetObject("cmdSearchDept.Image"), System.Drawing.Image)
        Me.cmdSearchDept.Location = New System.Drawing.Point(488, 11)
        Me.cmdSearchDept.Name = "cmdSearchDept"
        Me.cmdSearchDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDept.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchDept.TabIndex = 17
        Me.cmdSearchDept.TabStop = False
        Me.cmdSearchDept.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDept, "Search")
        Me.cmdSearchDept.UseVisualStyleBackColor = False
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsReturn = True
        Me.txtCustomer.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomer.Enabled = False
        Me.txtCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomer.Location = New System.Drawing.Point(90, 33)
        Me.txtCustomer.MaxLength = 0
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomer.Size = New System.Drawing.Size(397, 19)
        Me.txtCustomer.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtCustomer, "Press F1 For Help")
        '
        'cmdSearchCustomer
        '
        Me.cmdSearchCustomer.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCustomer.Enabled = False
        Me.cmdSearchCustomer.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCustomer.Image = CType(resources.GetObject("cmdSearchCustomer.Image"), System.Drawing.Image)
        Me.cmdSearchCustomer.Location = New System.Drawing.Point(488, 33)
        Me.cmdSearchCustomer.Name = "cmdSearchCustomer"
        Me.cmdSearchCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCustomer.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchCustomer.TabIndex = 1
        Me.cmdSearchCustomer.TabStop = False
        Me.cmdSearchCustomer.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCustomer, "Search")
        Me.cmdSearchCustomer.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(278, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 10
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(123, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 9
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(63, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 8
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(4, 10)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 7
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 92)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(762, 317)
        Me.SprdMain.TabIndex = 6
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkAllItem)
        Me.FraAccount.Controls.Add(Me.cmdSearchItem)
        Me.FraAccount.Controls.Add(Me.txtItem)
        Me.FraAccount.Controls.Add(Me.txtDept)
        Me.FraAccount.Controls.Add(Me.cmdSearchDept)
        Me.FraAccount.Controls.Add(Me.chkAllDept)
        Me.FraAccount.Controls.Add(Me.txtCustomer)
        Me.FraAccount.Controls.Add(Me.cmdSearchCustomer)
        Me.FraAccount.Controls.Add(Me.chkAllCustomer)
        Me.FraAccount.Controls.Add(Me.lblDeptName)
        Me.FraAccount.Controls.Add(Me.lblMachineName)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(577, 91)
        Me.FraAccount.TabIndex = 11
        Me.FraAccount.TabStop = False
        '
        'chkAllItem
        '
        Me.chkAllItem.AutoSize = True
        Me.chkAllItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllItem.Checked = True
        Me.chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllItem.Location = New System.Drawing.Point(518, 59)
        Me.chkAllItem.Name = "chkAllItem"
        Me.chkAllItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllItem.Size = New System.Drawing.Size(48, 18)
        Me.chkAllItem.TabIndex = 21
        Me.chkAllItem.Text = "ALL"
        Me.chkAllItem.UseVisualStyleBackColor = False
        '
        'chkAllDept
        '
        Me.chkAllDept.AutoSize = True
        Me.chkAllDept.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllDept.Checked = True
        Me.chkAllDept.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllDept.Location = New System.Drawing.Point(518, 14)
        Me.chkAllDept.Name = "chkAllDept"
        Me.chkAllDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllDept.Size = New System.Drawing.Size(48, 18)
        Me.chkAllDept.TabIndex = 16
        Me.chkAllDept.Text = "ALL"
        Me.chkAllDept.UseVisualStyleBackColor = False
        '
        'chkAllCustomer
        '
        Me.chkAllCustomer.AutoSize = True
        Me.chkAllCustomer.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCustomer.Checked = True
        Me.chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCustomer.Location = New System.Drawing.Point(518, 36)
        Me.chkAllCustomer.Name = "chkAllCustomer"
        Me.chkAllCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCustomer.Size = New System.Drawing.Size(48, 18)
        Me.chkAllCustomer.TabIndex = 2
        Me.chkAllCustomer.Text = "ALL"
        Me.chkAllCustomer.UseVisualStyleBackColor = False
        '
        'lblDeptName
        '
        Me.lblDeptName.AutoSize = True
        Me.lblDeptName.BackColor = System.Drawing.SystemColors.Control
        Me.lblDeptName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeptName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDeptName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDeptName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptName.Location = New System.Drawing.Point(160, 11)
        Me.lblDeptName.Name = "lblDeptName"
        Me.lblDeptName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDeptName.Size = New System.Drawing.Size(2, 16)
        Me.lblDeptName.TabIndex = 26
        '
        'lblMachineName
        '
        Me.lblMachineName.AutoSize = True
        Me.lblMachineName.BackColor = System.Drawing.SystemColors.Control
        Me.lblMachineName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMachineName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMachineName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMachineName.Location = New System.Drawing.Point(160, 33)
        Me.lblMachineName.Name = "lblMachineName"
        Me.lblMachineName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMachineName.Size = New System.Drawing.Size(2, 16)
        Me.lblMachineName.TabIndex = 25
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 59)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(59, 14)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Product : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(12, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(81, 14)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "Department : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(12, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(72, 14)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Customer : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboProblemDate)
        Me.Frame1.Controls.Add(Me.txtDate2)
        Me.Frame1.Controls.Add(Me.txtDate1)
        Me.Frame1.Controls.Add(Me.lblDate1)
        Me.Frame1.Controls.Add(Me.lblDate2)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(578, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(185, 91)
        Me.Frame1.TabIndex = 13
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Date On Condition"
        '
        'cboProblemDate
        '
        Me.cboProblemDate.BackColor = System.Drawing.SystemColors.Window
        Me.cboProblemDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboProblemDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProblemDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboProblemDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboProblemDate.Location = New System.Drawing.Point(20, 14)
        Me.cboProblemDate.Name = "cboProblemDate"
        Me.cboProblemDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboProblemDate.Size = New System.Drawing.Size(147, 22)
        Me.cboProblemDate.TabIndex = 3
        '
        'txtDate2
        '
        Me.txtDate2.AllowPromptAsInput = False
        Me.txtDate2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate2.Location = New System.Drawing.Point(66, 60)
        Me.txtDate2.Mask = "##/##/####"
        Me.txtDate2.Name = "txtDate2"
        Me.txtDate2.Size = New System.Drawing.Size(100, 20)
        Me.txtDate2.TabIndex = 5
        '
        'txtDate1
        '
        Me.txtDate1.AllowPromptAsInput = False
        Me.txtDate1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate1.Location = New System.Drawing.Point(66, 36)
        Me.txtDate1.Mask = "##/##/####"
        Me.txtDate1.Name = "txtDate1"
        Me.txtDate1.Size = New System.Drawing.Size(100, 20)
        Me.txtDate1.TabIndex = 4
        '
        'lblDate1
        '
        Me.lblDate1.AutoSize = True
        Me.lblDate1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate1.Location = New System.Drawing.Point(14, 40)
        Me.lblDate1.Name = "lblDate1"
        Me.lblDate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate1.Size = New System.Drawing.Size(49, 14)
        Me.lblDate1.TabIndex = 15
        Me.lblDate1.Text = "Date 1 : "
        '
        'lblDate2
        '
        Me.lblDate2.AutoSize = True
        Me.lblDate2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate2.Location = New System.Drawing.Point(14, 64)
        Me.lblDate2.Name = "lblDate2"
        Me.lblDate2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate2.Size = New System.Drawing.Size(49, 14)
        Me.lblDate2.TabIndex = 14
        Me.lblDate2.Text = "Date 2 : "
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(422, 410)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(341, 49)
        Me.FraMovement.TabIndex = 12
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 174)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 15
        '
        'frmParamProductProblem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(763, 459)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Report1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamProductProblem"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Product Problem Log"
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain.DataSource = Nothing
    End Sub
#End Region
End Class