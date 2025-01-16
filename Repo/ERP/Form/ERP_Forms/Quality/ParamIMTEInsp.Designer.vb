Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamIMTEInsp
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
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents cboCalOnCondition As System.Windows.Forms.ComboBox
    Public WithEvents txtDate2 As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDate1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents lblDate1 As System.Windows.Forms.Label
    Public WithEvents lblDate2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDocNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDocNo As System.Windows.Forms.Button
    Public WithEvents lblUnitRange As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents lblMaxRange As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents lblMake As System.Windows.Forms.Label
    Public WithEvents lblMakersNo As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents lblLC As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents lblFrequency As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblMinRange As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblRange As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents lblLocation As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents lblENo As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblDescription As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamIMTEInsp))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.cmdSearchDocNo = New System.Windows.Forms.Button()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboCalOnCondition = New System.Windows.Forms.ComboBox()
        Me.txtDate2 = New System.Windows.Forms.MaskedTextBox()
        Me.txtDate1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblDate1 = New System.Windows.Forms.Label()
        Me.lblDate2 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.lblUnitRange = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblMaxRange = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblMake = New System.Windows.Forms.Label()
        Me.lblMakersNo = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblLC = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblFrequency = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMinRange = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblRange = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblENo = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(332, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 9
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
        Me.CmdPreview.TabIndex = 8
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
        Me.cmdPrint.TabIndex = 7
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
        Me.cmdShow.TabIndex = 6
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'txtDocNo
        '
        Me.txtDocNo.AcceptsReturn = True
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocNo.Location = New System.Drawing.Point(91, 14)
        Me.txtDocNo.MaxLength = 0
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocNo.Size = New System.Drawing.Size(109, 19)
        Me.txtDocNo.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtDocNo, "Press F1 For Help")
        '
        'cmdSearchDocNo
        '
        Me.cmdSearchDocNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDocNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDocNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDocNo.Image = CType(resources.GetObject("cmdSearchDocNo.Image"), System.Drawing.Image)
        Me.cmdSearchDocNo.Location = New System.Drawing.Point(200, 14)
        Me.cmdSearchDocNo.Name = "cmdSearchDocNo"
        Me.cmdSearchDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDocNo.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchDocNo.TabIndex = 1
        Me.cmdSearchDocNo.TabStop = False
        Me.cmdSearchDocNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDocNo, "Search")
        Me.cmdSearchDocNo.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 144)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(762, 317)
        Me.SprdMain.TabIndex = 5
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
        Me.FraMovement.Location = New System.Drawing.Point(366, 458)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(397, 49)
        Me.FraMovement.TabIndex = 11
        Me.FraMovement.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboCalOnCondition)
        Me.Frame1.Controls.Add(Me.txtDate2)
        Me.Frame1.Controls.Add(Me.txtDate1)
        Me.Frame1.Controls.Add(Me.lblDate1)
        Me.Frame1.Controls.Add(Me.lblDate2)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(610, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(153, 139)
        Me.Frame1.TabIndex = 14
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Calibrated On Condition"
        '
        'cboCalOnCondition
        '
        Me.cboCalOnCondition.BackColor = System.Drawing.SystemColors.Window
        Me.cboCalOnCondition.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCalOnCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCalOnCondition.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCalOnCondition.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCalOnCondition.Location = New System.Drawing.Point(14, 28)
        Me.cboCalOnCondition.Name = "cboCalOnCondition"
        Me.cboCalOnCondition.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCalOnCondition.Size = New System.Drawing.Size(123, 22)
        Me.cboCalOnCondition.TabIndex = 2
        '
        'txtDate2
        '
        Me.txtDate2.AllowPromptAsInput = False
        Me.txtDate2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate2.Location = New System.Drawing.Point(64, 98)
        Me.txtDate2.Mask = "##/##/####"
        Me.txtDate2.Name = "txtDate2"
        Me.txtDate2.Size = New System.Drawing.Size(76, 20)
        Me.txtDate2.TabIndex = 4
        '
        'txtDate1
        '
        Me.txtDate1.AllowPromptAsInput = False
        Me.txtDate1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate1.Location = New System.Drawing.Point(66, 66)
        Me.txtDate1.Mask = "##/##/####"
        Me.txtDate1.Name = "txtDate1"
        Me.txtDate1.Size = New System.Drawing.Size(76, 20)
        Me.txtDate1.TabIndex = 3
        '
        'lblDate1
        '
        Me.lblDate1.AutoSize = True
        Me.lblDate1.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate1.Location = New System.Drawing.Point(6, 70)
        Me.lblDate1.Name = "lblDate1"
        Me.lblDate1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate1.Size = New System.Drawing.Size(49, 14)
        Me.lblDate1.TabIndex = 16
        Me.lblDate1.Text = "Date 1 : "
        '
        'lblDate2
        '
        Me.lblDate2.AutoSize = True
        Me.lblDate2.BackColor = System.Drawing.SystemColors.Control
        Me.lblDate2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDate2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDate2.Location = New System.Drawing.Point(6, 102)
        Me.lblDate2.Name = "lblDate2"
        Me.lblDate2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDate2.Size = New System.Drawing.Size(49, 14)
        Me.lblDate2.TabIndex = 15
        Me.lblDate2.Text = "Date 2 : "
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtDocNo)
        Me.FraAccount.Controls.Add(Me.cmdSearchDocNo)
        Me.FraAccount.Controls.Add(Me.lblUnitRange)
        Me.FraAccount.Controls.Add(Me.Label17)
        Me.FraAccount.Controls.Add(Me.lblMaxRange)
        Me.FraAccount.Controls.Add(Me.Label15)
        Me.FraAccount.Controls.Add(Me.lblMake)
        Me.FraAccount.Controls.Add(Me.lblMakersNo)
        Me.FraAccount.Controls.Add(Me.Label13)
        Me.FraAccount.Controls.Add(Me.Label11)
        Me.FraAccount.Controls.Add(Me.lblLC)
        Me.FraAccount.Controls.Add(Me.Label7)
        Me.FraAccount.Controls.Add(Me.lblFrequency)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.lblMinRange)
        Me.FraAccount.Controls.Add(Me.Label5)
        Me.FraAccount.Controls.Add(Me.lblRange)
        Me.FraAccount.Controls.Add(Me.Label10)
        Me.FraAccount.Controls.Add(Me.lblLocation)
        Me.FraAccount.Controls.Add(Me.Label8)
        Me.FraAccount.Controls.Add(Me.lblENo)
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Controls.Add(Me.lblDescription)
        Me.FraAccount.Controls.Add(Me.Label4)
        Me.FraAccount.Controls.Add(Me.Label3)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(609, 139)
        Me.FraAccount.TabIndex = 10
        Me.FraAccount.TabStop = False
        '
        'lblUnitRange
        '
        Me.lblUnitRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnitRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUnitRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUnitRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnitRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnitRange.Location = New System.Drawing.Point(495, 97)
        Me.lblUnitRange.Name = "lblUnitRange"
        Me.lblUnitRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUnitRange.Size = New System.Drawing.Size(109, 17)
        Me.lblUnitRange.TabIndex = 36
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(421, 97)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(74, 14)
        Me.Label17.TabIndex = 37
        Me.Label17.Text = "Range Unit : "
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMaxRange
        '
        Me.lblMaxRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblMaxRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMaxRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMaxRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaxRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaxRange.Location = New System.Drawing.Point(495, 76)
        Me.lblMaxRange.Name = "lblMaxRange"
        Me.lblMaxRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMaxRange.Size = New System.Drawing.Size(109, 17)
        Me.lblMaxRange.TabIndex = 34
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(421, 76)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(75, 14)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "Max Range : "
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMake
        '
        Me.lblMake.BackColor = System.Drawing.SystemColors.Control
        Me.lblMake.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMake.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMake.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMake.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMake.Location = New System.Drawing.Point(292, 76)
        Me.lblMake.Name = "lblMake"
        Me.lblMake.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMake.Size = New System.Drawing.Size(117, 17)
        Me.lblMake.TabIndex = 32
        '
        'lblMakersNo
        '
        Me.lblMakersNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblMakersNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMakersNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMakersNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMakersNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMakersNo.Location = New System.Drawing.Point(292, 56)
        Me.lblMakersNo.Name = "lblMakersNo"
        Me.lblMakersNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMakersNo.Size = New System.Drawing.Size(117, 17)
        Me.lblMakersNo.TabIndex = 30
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(248, 76)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(46, 14)
        Me.Label13.TabIndex = 33
        Me.Label13.Text = "Make : "
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(219, 56)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(75, 14)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "Makers No : "
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLC
        '
        Me.lblLC.BackColor = System.Drawing.SystemColors.Control
        Me.lblLC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLC.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLC.Location = New System.Drawing.Point(91, 77)
        Me.lblLC.Name = "lblLC"
        Me.lblLC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLC.Size = New System.Drawing.Size(117, 17)
        Me.lblLC.TabIndex = 28
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(53, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(40, 14)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "L. C. : "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFrequency
        '
        Me.lblFrequency.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrequency.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFrequency.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFrequency.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrequency.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFrequency.Location = New System.Drawing.Point(495, 37)
        Me.lblFrequency.Name = "lblFrequency"
        Me.lblFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFrequency.Size = New System.Drawing.Size(109, 17)
        Me.lblFrequency.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(426, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(74, 14)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Frequency : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMinRange
        '
        Me.lblMinRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblMinRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMinRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMinRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMinRange.Location = New System.Drawing.Point(495, 56)
        Me.lblMinRange.Name = "lblMinRange"
        Me.lblMinRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMinRange.Size = New System.Drawing.Size(109, 17)
        Me.lblMinRange.TabIndex = 25
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(424, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(73, 14)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "Min Range : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRange
        '
        Me.lblRange.BackColor = System.Drawing.SystemColors.Control
        Me.lblRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRange.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRange.Location = New System.Drawing.Point(91, 97)
        Me.lblRange.Name = "lblRange"
        Me.lblRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRange.Size = New System.Drawing.Size(317, 17)
        Me.lblRange.TabIndex = 23
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(44, 97)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(50, 14)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Range : "
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLocation
        '
        Me.lblLocation.BackColor = System.Drawing.SystemColors.Control
        Me.lblLocation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLocation.Location = New System.Drawing.Point(91, 117)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLocation.Size = New System.Drawing.Size(317, 17)
        Me.lblLocation.TabIndex = 21
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(32, 117)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(63, 14)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Location : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblENo
        '
        Me.lblENo.BackColor = System.Drawing.SystemColors.Control
        Me.lblENo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblENo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblENo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblENo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblENo.Location = New System.Drawing.Point(91, 57)
        Me.lblENo.Name = "lblENo"
        Me.lblENo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblENo.Size = New System.Drawing.Size(117, 17)
        Me.lblENo.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(49, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(42, 14)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "E. No : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDescription
        '
        Me.lblDescription.BackColor = System.Drawing.SystemColors.Control
        Me.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDescription.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDescription.Location = New System.Drawing.Point(91, 37)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDescription.Size = New System.Drawing.Size(317, 17)
        Me.lblDescription.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(17, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(79, 14)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Description : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(38, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(53, 14)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "Doc No : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 174)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 16
        '
        'frmParamIMTEInsp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(763, 510)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Report1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamIMTEInsp"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "IMTE Calibration History Card (Variables)"
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
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