Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewOutsSummary
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
    Public WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents txtPaymentDays As System.Windows.Forms.TextBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _OptDaysShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDaysShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _OptSumDet_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents txtDays4 As System.Windows.Forms.TextBox
    Public WithEvents txtDays3 As System.Windows.Forms.TextBox
    Public WithEvents txtDays2 As System.Windows.Forms.TextBox
    Public WithEvents txtDays1 As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents txtPaymentDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents _optAsOn_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optAsOn_0 As System.Windows.Forms.RadioButton
    Public WithEvents fraDate As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents _optParty_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optParty_0 As System.Windows.Forms.RadioButton
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents _Lbl_7 As System.Windows.Forms.Label
    Public WithEvents fraParty As System.Windows.Forms.GroupBox
    Public WithEvents lblRunDate As System.Windows.Forms.Label
    Public WithEvents fraCostC As System.Windows.Forms.GroupBox
    Public WithEvents cboChqsInMonth As System.Windows.Forms.ComboBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public CMDialog1Open As System.Windows.Forms.OpenFileDialog
    Public CMDialog1Save As System.Windows.Forms.SaveFileDialog
    Public CMDialog1Font As System.Windows.Forms.FontDialog
    Public CMDialog1Color As System.Windows.Forms.ColorDialog
    Public CMDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblOutsType As System.Windows.Forms.Label
    Public WithEvents lblAddress As System.Windows.Forms.Label
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptDaysShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptSumDet As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optAsOn As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optParty As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewOutsSummary))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtPaymentDays = New System.Windows.Forms.TextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptDaysShow_0 = New System.Windows.Forms.RadioButton()
        Me._OptDaysShow_1 = New System.Windows.Forms.RadioButton()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._OptSumDet_1 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_0 = New System.Windows.Forms.RadioButton()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.txtDays4 = New System.Windows.Forms.TextBox()
        Me.txtDays3 = New System.Windows.Forms.TextBox()
        Me.txtDays2 = New System.Windows.Forms.TextBox()
        Me.txtDays1 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtPaymentDate = New System.Windows.Forms.MaskedTextBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.fraDate = New System.Windows.Forms.GroupBox()
        Me._optAsOn_1 = New System.Windows.Forms.RadioButton()
        Me._optAsOn_0 = New System.Windows.Forms.RadioButton()
        Me.fraParty = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me._optParty_1 = New System.Windows.Forms.RadioButton()
        Me._optParty_0 = New System.Windows.Forms.RadioButton()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me._Lbl_7 = New System.Windows.Forms.Label()
        Me.fraCostC = New System.Windows.Forms.GroupBox()
        Me.lblYear = New System.Windows.Forms.DateTimePicker()
        Me.lblRunDate = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.cboChqsInMonth = New System.Windows.Forms.ComboBox()
        Me.CMDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CMDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CMDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CMDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CMDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblOutsType = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptDaysShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptSumDet = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optAsOn = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optParty = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.Frame9.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.fraDate.SuspendLayout()
        Me.fraParty.SuspendLayout()
        Me.fraCostC.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptDaysShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optAsOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optParty, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(4, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 34)
        Me.cmdShow.TabIndex = 8
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(184, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 34)
        Me.cmdClose.TabIndex = 11
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(65, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 34)
        Me.cmdPrint.TabIndex = 9
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(125, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 34)
        Me.CmdPreview.TabIndex = 10
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(366, 34)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 20)
        Me.cmdsearch.TabIndex = 6
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.cboPaymentMode)
        Me.Frame9.Controls.Add(Me._Lbl_1)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(2, 84)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(245, 39)
        Me.Frame9.TabIndex = 52
        Me.Frame9.TabStop = False
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.BackColor = System.Drawing.SystemColors.Window
        Me.cboPaymentMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaymentMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymentMode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPaymentMode.Location = New System.Drawing.Point(104, 12)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPaymentMode.Size = New System.Drawing.Size(137, 22)
        Me.cboPaymentMode.TabIndex = 53
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(12, 14)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(95, 14)
        Me._Lbl_1.TabIndex = 54
        Me._Lbl_1.Text = "Payment Mode :"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtPaymentDays)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(196, 43)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(138, 45)
        Me.Frame6.TabIndex = 49
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Payment Days"
        '
        'txtPaymentDays
        '
        Me.txtPaymentDays.AcceptsReturn = True
        Me.txtPaymentDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaymentDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaymentDays.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentDays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPaymentDays.Location = New System.Drawing.Point(42, 14)
        Me.txtPaymentDays.MaxLength = 4
        Me.txtPaymentDays.Name = "txtPaymentDays"
        Me.txtPaymentDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaymentDays.Size = New System.Drawing.Size(36, 20)
        Me.txtPaymentDays.TabIndex = 50
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptDaysShow_0)
        Me.Frame2.Controls.Add(Me._OptDaysShow_1)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(242, 564)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(228, 44)
        Me.Frame2.TabIndex = 38
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Format"
        Me.Frame2.Visible = False
        '
        '_OptDaysShow_0
        '
        Me._OptDaysShow_0.AutoSize = True
        Me._OptDaysShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptDaysShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDaysShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDaysShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDaysShow.SetIndex(Me._OptDaysShow_0, CType(0, Short))
        Me._OptDaysShow_0.Location = New System.Drawing.Point(9, 18)
        Me._OptDaysShow_0.Name = "_OptDaysShow_0"
        Me._OptDaysShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDaysShow_0.Size = New System.Drawing.Size(64, 18)
        Me._OptDaysShow_0.TabIndex = 40
        Me._OptDaysShow_0.TabStop = True
        Me._OptDaysShow_0.Text = "Master"
        Me._OptDaysShow_0.UseVisualStyleBackColor = False
        '
        '_OptDaysShow_1
        '
        Me._OptDaysShow_1.AutoSize = True
        Me._OptDaysShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptDaysShow_1.Checked = True
        Me._OptDaysShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDaysShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDaysShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDaysShow.SetIndex(Me._OptDaysShow_1, CType(1, Short))
        Me._OptDaysShow_1.Location = New System.Drawing.Point(115, 18)
        Me._OptDaysShow_1.Name = "_OptDaysShow_1"
        Me._OptDaysShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDaysShow_1.Size = New System.Drawing.Size(77, 18)
        Me._OptDaysShow_1.TabIndex = 39
        Me._OptDaysShow_1.TabStop = True
        Me._OptDaysShow_1.Text = "Auto Calc"
        Me._OptDaysShow_1.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me._OptSumDet_1)
        Me.Frame7.Controls.Add(Me._OptSumDet_0)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(0, 564)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(190, 44)
        Me.Frame7.TabIndex = 35
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Format"
        '
        '_OptSumDet_1
        '
        Me._OptSumDet_1.AutoSize = True
        Me._OptSumDet_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_1.Checked = True
        Me._OptSumDet_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_1, CType(1, Short))
        Me._OptSumDet_1.Location = New System.Drawing.Point(87, 18)
        Me._OptSumDet_1.Name = "_OptSumDet_1"
        Me._OptSumDet_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_1.Size = New System.Drawing.Size(96, 18)
        Me._OptSumDet_1.TabIndex = 37
        Me._OptSumDet_1.TabStop = True
        Me._OptSumDet_1.Text = "Summarised"
        Me._OptSumDet_1.UseVisualStyleBackColor = False
        '
        '_OptSumDet_0
        '
        Me._OptSumDet_0.AutoSize = True
        Me._OptSumDet_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_0, CType(0, Short))
        Me._OptSumDet_0.Location = New System.Drawing.Point(9, 18)
        Me._OptSumDet_0.Name = "_OptSumDet_0"
        Me._OptSumDet_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_0.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_0.TabIndex = 36
        Me._OptSumDet_0.TabStop = True
        Me._OptSumDet_0.Text = "Detailed"
        Me._OptSumDet_0.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.txtDays4)
        Me.Frame8.Controls.Add(Me.txtDays3)
        Me.Frame8.Controls.Add(Me.txtDays2)
        Me.Frame8.Controls.Add(Me.txtDays1)
        Me.Frame8.Controls.Add(Me.Label6)
        Me.Frame8.Controls.Add(Me.Label5)
        Me.Frame8.Controls.Add(Me.Label1)
        Me.Frame8.Controls.Add(Me.Label2)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(250, 85)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(340, 38)
        Me.Frame8.TabIndex = 26
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Days Category"
        '
        'txtDays4
        '
        Me.txtDays4.AcceptsReturn = True
        Me.txtDays4.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays4.Location = New System.Drawing.Point(255, 14)
        Me.txtDays4.MaxLength = 4
        Me.txtDays4.Name = "txtDays4"
        Me.txtDays4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays4.Size = New System.Drawing.Size(28, 20)
        Me.txtDays4.TabIndex = 30
        '
        'txtDays3
        '
        Me.txtDays3.AcceptsReturn = True
        Me.txtDays3.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays3.Location = New System.Drawing.Point(184, 14)
        Me.txtDays3.MaxLength = 4
        Me.txtDays3.Name = "txtDays3"
        Me.txtDays3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays3.Size = New System.Drawing.Size(28, 20)
        Me.txtDays3.TabIndex = 29
        '
        'txtDays2
        '
        Me.txtDays2.AcceptsReturn = True
        Me.txtDays2.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays2.Location = New System.Drawing.Point(113, 14)
        Me.txtDays2.MaxLength = 4
        Me.txtDays2.Name = "txtDays2"
        Me.txtDays2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays2.Size = New System.Drawing.Size(28, 20)
        Me.txtDays2.TabIndex = 28
        '
        'txtDays1
        '
        Me.txtDays1.AcceptsReturn = True
        Me.txtDays1.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays1.Location = New System.Drawing.Point(42, 14)
        Me.txtDays1.MaxLength = 4
        Me.txtDays1.Name = "txtDays1"
        Me.txtDays1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays1.Size = New System.Drawing.Size(28, 20)
        Me.txtDays1.TabIndex = 27
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(145, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(35, 14)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Day 3"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(216, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(35, 14)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Day 4"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(35, 14)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Day 1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(74, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(35, 14)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Day 2"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtPaymentDate)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(232, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(102, 46)
        Me.Frame1.TabIndex = 18
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Payment As On"
        Me.Frame1.Visible = False
        '
        'txtPaymentDate
        '
        Me.txtPaymentDate.AllowPromptAsInput = False
        Me.txtPaymentDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentDate.Location = New System.Drawing.Point(9, 19)
        Me.txtPaymentDate.Mask = "##/##/####"
        Me.txtPaymentDate.Name = "txtPaymentDate"
        Me.txtPaymentDate.Size = New System.Drawing.Size(87, 20)
        Me.txtPaymentDate.TabIndex = 2
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 127)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(896, 437)
        Me.SprdView.TabIndex = 7
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(652, 564)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(249, 49)
        Me.FraMovement.TabIndex = 16
        Me.FraMovement.TabStop = False
        '
        'fraDate
        '
        Me.fraDate.BackColor = System.Drawing.SystemColors.Control
        Me.fraDate.Controls.Add(Me._optAsOn_1)
        Me.fraDate.Controls.Add(Me._optAsOn_0)
        Me.fraDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDate.Location = New System.Drawing.Point(0, 0)
        Me.fraDate.Name = "fraDate"
        Me.fraDate.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDate.Size = New System.Drawing.Size(72, 88)
        Me.fraDate.TabIndex = 13
        Me.fraDate.TabStop = False
        Me.fraDate.Text = "Base On"
        '
        '_optAsOn_1
        '
        Me._optAsOn_1.AutoSize = True
        Me._optAsOn_1.BackColor = System.Drawing.SystemColors.Control
        Me._optAsOn_1.Checked = True
        Me._optAsOn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAsOn_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAsOn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAsOn.SetIndex(Me._optAsOn_1, CType(1, Short))
        Me._optAsOn_1.Location = New System.Drawing.Point(8, 26)
        Me._optAsOn_1.Name = "_optAsOn_1"
        Me._optAsOn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAsOn_1.Size = New System.Drawing.Size(49, 18)
        Me._optAsOn_1.TabIndex = 0
        Me._optAsOn_1.TabStop = True
        Me._optAsOn_1.Text = "MRR"
        Me._optAsOn_1.UseVisualStyleBackColor = False
        '
        '_optAsOn_0
        '
        Me._optAsOn_0.AutoSize = True
        Me._optAsOn_0.BackColor = System.Drawing.SystemColors.Control
        Me._optAsOn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAsOn_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAsOn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAsOn.SetIndex(Me._optAsOn_0, CType(0, Short))
        Me._optAsOn_0.Location = New System.Drawing.Point(10, 48)
        Me._optAsOn_0.Name = "_optAsOn_0"
        Me._optAsOn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAsOn_0.Size = New System.Drawing.Size(41, 18)
        Me._optAsOn_0.TabIndex = 1
        Me._optAsOn_0.TabStop = True
        Me._optAsOn_0.Text = "Bill"
        Me._optAsOn_0.UseVisualStyleBackColor = False
        '
        'fraParty
        '
        Me.fraParty.BackColor = System.Drawing.SystemColors.Control
        Me.fraParty.Controls.Add(Me.cboDivision)
        Me.fraParty.Controls.Add(Me._optParty_1)
        Me.fraParty.Controls.Add(Me._optParty_0)
        Me.fraParty.Controls.Add(Me.cmdsearch)
        Me.fraParty.Controls.Add(Me.TxtName)
        Me.fraParty.Controls.Add(Me._Lbl_7)
        Me.fraParty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraParty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraParty.Location = New System.Drawing.Point(336, 0)
        Me.fraParty.Name = "fraParty"
        Me.fraParty.Padding = New System.Windows.Forms.Padding(0)
        Me.fraParty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraParty.Size = New System.Drawing.Size(402, 88)
        Me.fraParty.TabIndex = 12
        Me.fraParty.TabStop = False
        Me.fraParty.Text = "Party"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(61, 58)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(301, 22)
        Me.cboDivision.TabIndex = 41
        '
        '_optParty_1
        '
        Me._optParty_1.AutoSize = True
        Me._optParty_1.BackColor = System.Drawing.SystemColors.Control
        Me._optParty_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optParty_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optParty_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optParty.SetIndex(Me._optParty_1, CType(1, Short))
        Me._optParty_1.Location = New System.Drawing.Point(220, 10)
        Me._optParty_1.Name = "_optParty_1"
        Me._optParty_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optParty_1.Size = New System.Drawing.Size(39, 18)
        Me._optParty_1.TabIndex = 4
        Me._optParty_1.TabStop = True
        Me._optParty_1.Text = "All"
        Me._optParty_1.UseVisualStyleBackColor = False
        '
        '_optParty_0
        '
        Me._optParty_0.AutoSize = True
        Me._optParty_0.BackColor = System.Drawing.SystemColors.Control
        Me._optParty_0.Checked = True
        Me._optParty_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optParty_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optParty_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optParty.SetIndex(Me._optParty_0, CType(0, Short))
        Me._optParty_0.Location = New System.Drawing.Point(64, 10)
        Me._optParty_0.Name = "_optParty_0"
        Me._optParty_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optParty_0.Size = New System.Drawing.Size(84, 18)
        Me._optParty_0.TabIndex = 3
        Me._optParty_0.TabStop = True
        Me._optParty_0.Text = "Particulars"
        Me._optParty_0.UseVisualStyleBackColor = False
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(61, 35)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(301, 20)
        Me.TxtName.TabIndex = 5
        '
        '_Lbl_7
        '
        Me._Lbl_7.AutoSize = True
        Me._Lbl_7.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_7, CType(7, Short))
        Me._Lbl_7.Location = New System.Drawing.Point(4, 60)
        Me._Lbl_7.Name = "_Lbl_7"
        Me._Lbl_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_7.Size = New System.Drawing.Size(56, 14)
        Me._Lbl_7.TabIndex = 42
        Me._Lbl_7.Text = "Division :"
        '
        'fraCostC
        '
        Me.fraCostC.BackColor = System.Drawing.SystemColors.Control
        Me.fraCostC.Controls.Add(Me.lblYear)
        Me.fraCostC.Controls.Add(Me.lblRunDate)
        Me.fraCostC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCostC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCostC.Location = New System.Drawing.Point(75, 0)
        Me.fraCostC.Name = "fraCostC"
        Me.fraCostC.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCostC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCostC.Size = New System.Drawing.Size(155, 46)
        Me.fraCostC.TabIndex = 14
        Me.fraCostC.TabStop = False
        Me.fraCostC.Text = "Month"
        '
        'lblYear
        '
        Me.lblYear.CustomFormat = "MMMM,yyyy"
        Me.lblYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblYear.Location = New System.Drawing.Point(4, 14)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(148, 20)
        Me.lblYear.TabIndex = 36
        '
        'lblRunDate
        '
        Me.lblRunDate.AutoSize = True
        Me.lblRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRunDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRunDate.Location = New System.Drawing.Point(6, 28)
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRunDate.Size = New System.Drawing.Size(48, 14)
        Me.lblRunDate.TabIndex = 23
        Me.lblRunDate.Text = "RunDate"
        Me.lblRunDate.Visible = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.cboChqsInMonth)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(75, 43)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(119, 45)
        Me.Frame5.TabIndex = 46
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Chqs in Month"
        '
        'cboChqsInMonth
        '
        Me.cboChqsInMonth.BackColor = System.Drawing.SystemColors.Window
        Me.cboChqsInMonth.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboChqsInMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboChqsInMonth.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboChqsInMonth.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboChqsInMonth.Location = New System.Drawing.Point(6, 15)
        Me.cboChqsInMonth.Name = "cboChqsInMonth"
        Me.cboChqsInMonth.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboChqsInMonth.Size = New System.Drawing.Size(108, 22)
        Me.cboChqsInMonth.TabIndex = 47
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(92, 106)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 54
        '
        'lblOutsType
        '
        Me.lblOutsType.AutoSize = True
        Me.lblOutsType.BackColor = System.Drawing.SystemColors.Control
        Me.lblOutsType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOutsType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOutsType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOutsType.Location = New System.Drawing.Point(422, 430)
        Me.lblOutsType.Name = "lblOutsType"
        Me.lblOutsType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOutsType.Size = New System.Drawing.Size(63, 14)
        Me.lblOutsType.TabIndex = 17
        Me.lblOutsType.Text = "lblOutsType"
        Me.lblOutsType.Visible = False
        '
        'lblAddress
        '
        Me.lblAddress.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddress.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddress.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddress.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddress.Location = New System.Drawing.Point(0, 0)
        Me.lblAddress.Name = "lblAddress"
        Me.lblAddress.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddress.Size = New System.Drawing.Size(51, 11)
        Me.lblAddress.TabIndex = 15
        Me.lblAddress.Text = "Address"
        Me.lblAddress.Visible = False
        '
        'optParty
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(740, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(156, 125)
        Me.GroupBox1.TabIndex = 66
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Company Name"
        '
        'lstCompanyName
        '
        Me.lstCompanyName.BackColor = System.Drawing.SystemColors.Window
        Me.lstCompanyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCompanyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstCompanyName.IntegralHeight = False
        Me.lstCompanyName.Location = New System.Drawing.Point(0, 13)
        Me.lstCompanyName.Name = "lstCompanyName"
        Me.lstCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstCompanyName.Size = New System.Drawing.Size(156, 112)
        Me.lstCompanyName.TabIndex = 2
        '
        'frmViewOutsSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.fraCostC)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.fraDate)
        Me.Controls.Add(Me.fraParty)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.lblOutsType)
        Me.Controls.Add(Me.lblAddress)
        Me.Controls.Add(Me.Frame9)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmViewOutsSummary"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "View Outstanding (Summary Month Wise)"
        Me.Frame9.ResumeLayout(False)
        Me.Frame9.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.fraDate.ResumeLayout(False)
        Me.fraDate.PerformLayout()
        Me.fraParty.ResumeLayout(False)
        Me.fraParty.PerformLayout()
        Me.fraCostC.ResumeLayout(False)
        Me.fraCostC.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptDaysShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optAsOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optParty, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Friend WithEvents lblYear As DateTimePicker
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
#End Region
End Class