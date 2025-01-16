Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewOuts
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
    Public WithEvents _OptShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents FraShow As System.Windows.Forms.GroupBox
    Public WithEvents _OptSumDet_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
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
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents fraDate As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents chkAllGroup As System.Windows.Forms.CheckBox
    Public WithEvents TxtGroup As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents _Lbl_7 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents fraParty As System.Windows.Forms.GroupBox
    Public WithEvents chkLegelNotice As System.Windows.Forms.CheckBox
    Public WithEvents chkPrintListFormat As System.Windows.Forms.CheckBox
    Public WithEvents chkReminderLetter As System.Windows.Forms.CheckBox
    Public WithEvents CboShowFor As System.Windows.Forms.ComboBox
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents fraCostC As System.Windows.Forms.GroupBox
    Public CMDialog1Open As System.Windows.Forms.OpenFileDialog
    Public CMDialog1Save As System.Windows.Forms.SaveFileDialog
    Public CMDialog1Font As System.Windows.Forms.FontDialog
    Public CMDialog1Color As System.Windows.Forms.ColorDialog
    Public CMDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblOutsType As System.Windows.Forms.Label
    Public WithEvents lblAddress As System.Windows.Forms.Label
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptSumDet As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optAsOn As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewOuts))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.FraShow = New System.Windows.Forms.GroupBox()
        Me._OptShow_1 = New System.Windows.Forms.RadioButton()
        Me._OptShow_0 = New System.Windows.Forms.RadioButton()
        Me._OptShow_2 = New System.Windows.Forms.RadioButton()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._OptSumDet_0 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_1 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtPaymentDate = New System.Windows.Forms.MaskedTextBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.fraDate = New System.Windows.Forms.GroupBox()
        Me._optAsOn_1 = New System.Windows.Forms.RadioButton()
        Me._optAsOn_0 = New System.Windows.Forms.RadioButton()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.fraParty = New System.Windows.Forms.GroupBox()
        Me.chkAllPerson = New System.Windows.Forms.CheckBox()
        Me.txtSalePerson = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.chkAllGroup = New System.Windows.Forms.CheckBox()
        Me.TxtGroup = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me._Lbl_7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.fraCostC = New System.Windows.Forms.GroupBox()
        Me.chkLegelNotice = New System.Windows.Forms.CheckBox()
        Me.chkPrintListFormat = New System.Windows.Forms.CheckBox()
        Me.chkReminderLetter = New System.Windows.Forms.CheckBox()
        Me.CboShowFor = New System.Windows.Forms.ComboBox()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.CMDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CMDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CMDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CMDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CMDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.lblOutsType = New System.Windows.Forms.Label()
        Me.lblAddress = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptSumDet = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optAsOn = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.optDues_0 = New System.Windows.Forms.RadioButton()
        Me.optDues_1 = New System.Windows.Forms.RadioButton()
        Me.optDueShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optGroupWise = New System.Windows.Forms.RadioButton()
        Me.optPartyWise = New System.Windows.Forms.RadioButton()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.FraShow.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.fraDate.SuspendLayout()
        Me.fraParty.SuspendLayout()
        Me.fraCostC.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optAsOn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.optDueShow, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.cmdShow.TabIndex = 9
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
        Me.cmdClose.TabIndex = 12
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
        Me.cmdPrint.TabIndex = 10
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
        Me.CmdPreview.TabIndex = 11
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
        Me.cmdsearch.Location = New System.Drawing.Point(365, 14)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 5
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'FraShow
        '
        Me.FraShow.BackColor = System.Drawing.SystemColors.Control
        Me.FraShow.Controls.Add(Me._OptShow_1)
        Me.FraShow.Controls.Add(Me._OptShow_0)
        Me.FraShow.Controls.Add(Me._OptShow_2)
        Me.FraShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraShow.Location = New System.Drawing.Point(260, 575)
        Me.FraShow.Name = "FraShow"
        Me.FraShow.Padding = New System.Windows.Forms.Padding(0)
        Me.FraShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraShow.Size = New System.Drawing.Size(390, 44)
        Me.FraShow.TabIndex = 29
        Me.FraShow.TabStop = False
        Me.FraShow.Text = "Show Only"
        '
        '_OptShow_1
        '
        Me._OptShow_1.AutoSize = True
        Me._OptShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_1, CType(1, Short))
        Me._OptShow_1.Location = New System.Drawing.Point(155, 18)
        Me._OptShow_1.Name = "_OptShow_1"
        Me._OptShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_1.Size = New System.Drawing.Size(53, 18)
        Me._OptShow_1.TabIndex = 32
        Me._OptShow_1.TabStop = True
        Me._OptShow_1.Text = "Debit"
        Me._OptShow_1.UseVisualStyleBackColor = False
        '
        '_OptShow_0
        '
        Me._OptShow_0.AutoSize = True
        Me._OptShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_0.Checked = True
        Me._OptShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_0, CType(0, Short))
        Me._OptShow_0.Location = New System.Drawing.Point(6, 18)
        Me._OptShow_0.Name = "_OptShow_0"
        Me._OptShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_0.Size = New System.Drawing.Size(39, 18)
        Me._OptShow_0.TabIndex = 31
        Me._OptShow_0.TabStop = True
        Me._OptShow_0.Text = "All"
        Me._OptShow_0.UseVisualStyleBackColor = False
        '
        '_OptShow_2
        '
        Me._OptShow_2.AutoSize = True
        Me._OptShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_2, CType(2, Short))
        Me._OptShow_2.Location = New System.Drawing.Point(320, 18)
        Me._OptShow_2.Name = "_OptShow_2"
        Me._OptShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_2.Size = New System.Drawing.Size(59, 18)
        Me._OptShow_2.TabIndex = 30
        Me._OptShow_2.TabStop = True
        Me._OptShow_2.Text = "Credit"
        Me._OptShow_2.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me._OptSumDet_0)
        Me.Frame7.Controls.Add(Me._OptSumDet_1)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(0, 574)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(256, 45)
        Me.Frame7.TabIndex = 21
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Format"
        '
        '_OptSumDet_0
        '
        Me._OptSumDet_0.AutoSize = True
        Me._OptSumDet_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_0.Checked = True
        Me._OptSumDet_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_0, CType(0, Short))
        Me._OptSumDet_0.Location = New System.Drawing.Point(7, 17)
        Me._OptSumDet_0.Name = "_OptSumDet_0"
        Me._OptSumDet_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_0.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_0.TabIndex = 23
        Me._OptSumDet_0.TabStop = True
        Me._OptSumDet_0.Text = "Detailed"
        Me._OptSumDet_0.UseVisualStyleBackColor = False
        '
        '_OptSumDet_1
        '
        Me._OptSumDet_1.AutoSize = True
        Me._OptSumDet_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_1, CType(1, Short))
        Me._OptSumDet_1.Location = New System.Drawing.Point(113, 17)
        Me._OptSumDet_1.Name = "_OptSumDet_1"
        Me._OptSumDet_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_1.Size = New System.Drawing.Size(96, 18)
        Me._OptSumDet_1.TabIndex = 22
        Me._OptSumDet_1.TabStop = True
        Me._OptSumDet_1.Text = "Summarised"
        Me._OptSumDet_1.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtPaymentDate)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(189, -1)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(96, 57)
        Me.Frame1.TabIndex = 20
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Payment As On"
        '
        'txtPaymentDate
        '
        Me.txtPaymentDate.AllowPromptAsInput = False
        Me.txtPaymentDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentDate.Location = New System.Drawing.Point(6, 35)
        Me.txtPaymentDate.Mask = "##/##/####"
        Me.txtPaymentDate.Name = "txtPaymentDate"
        Me.txtPaymentDate.Size = New System.Drawing.Size(87, 20)
        Me.txtPaymentDate.TabIndex = 3
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 122)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(1108, 450)
        Me.SprdView.TabIndex = 8
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
        Me.FraMovement.Location = New System.Drawing.Point(857, 575)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(249, 49)
        Me.FraMovement.TabIndex = 18
        Me.FraMovement.TabStop = False
        '
        'fraDate
        '
        Me.fraDate.BackColor = System.Drawing.SystemColors.Control
        Me.fraDate.Controls.Add(Me.Label5)
        Me.fraDate.Controls.Add(Me.Label3)
        Me.fraDate.Controls.Add(Me.txtDateFrom)
        Me.fraDate.Controls.Add(Me._optAsOn_1)
        Me.fraDate.Controls.Add(Me._optAsOn_0)
        Me.fraDate.Controls.Add(Me.txtDateTo)
        Me.fraDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDate.Location = New System.Drawing.Point(0, -1)
        Me.fraDate.Name = "fraDate"
        Me.fraDate.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDate.Size = New System.Drawing.Size(108, 118)
        Me.fraDate.TabIndex = 14
        Me.fraDate.TabStop = False
        Me.fraDate.Text = "As On Date"
        '
        '_optAsOn_1
        '
        Me._optAsOn_1.AutoSize = True
        Me._optAsOn_1.BackColor = System.Drawing.SystemColors.Control
        Me._optAsOn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAsOn_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAsOn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAsOn.SetIndex(Me._optAsOn_1, CType(1, Short))
        Me._optAsOn_1.Location = New System.Drawing.Point(5, 16)
        Me._optAsOn_1.Name = "_optAsOn_1"
        Me._optAsOn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAsOn_1.Size = New System.Drawing.Size(49, 18)
        Me._optAsOn_1.TabIndex = 0
        Me._optAsOn_1.Text = "MRR"
        Me._optAsOn_1.UseVisualStyleBackColor = False
        '
        '_optAsOn_0
        '
        Me._optAsOn_0.AutoSize = True
        Me._optAsOn_0.BackColor = System.Drawing.SystemColors.Control
        Me._optAsOn_0.Checked = True
        Me._optAsOn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAsOn_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAsOn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAsOn.SetIndex(Me._optAsOn_0, CType(0, Short))
        Me._optAsOn_0.Location = New System.Drawing.Point(60, 16)
        Me._optAsOn_0.Name = "_optAsOn_0"
        Me._optAsOn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAsOn_0.Size = New System.Drawing.Size(41, 18)
        Me._optAsOn_0.TabIndex = 1
        Me._optAsOn_0.TabStop = True
        Me._optAsOn_0.Text = "Bill"
        Me._optAsOn_0.UseVisualStyleBackColor = False
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(6, 91)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(87, 20)
        Me.txtDateTo.TabIndex = 2
        '
        'fraParty
        '
        Me.fraParty.BackColor = System.Drawing.SystemColors.Control
        Me.fraParty.Controls.Add(Me.chkAllPerson)
        Me.fraParty.Controls.Add(Me.txtSalePerson)
        Me.fraParty.Controls.Add(Me.Label2)
        Me.fraParty.Controls.Add(Me.Label1)
        Me.fraParty.Controls.Add(Me.cboDivision)
        Me.fraParty.Controls.Add(Me.chkAll)
        Me.fraParty.Controls.Add(Me.chkAllGroup)
        Me.fraParty.Controls.Add(Me.TxtGroup)
        Me.fraParty.Controls.Add(Me.cmdsearch)
        Me.fraParty.Controls.Add(Me.TxtName)
        Me.fraParty.Controls.Add(Me._Lbl_7)
        Me.fraParty.Controls.Add(Me.Label4)
        Me.fraParty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraParty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraParty.Location = New System.Drawing.Point(288, 0)
        Me.fraParty.Name = "fraParty"
        Me.fraParty.Padding = New System.Windows.Forms.Padding(0)
        Me.fraParty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraParty.Size = New System.Drawing.Size(446, 118)
        Me.fraParty.TabIndex = 13
        Me.fraParty.TabStop = False
        '
        'chkAllPerson
        '
        Me.chkAllPerson.AutoSize = True
        Me.chkAllPerson.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllPerson.Checked = True
        Me.chkAllPerson.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllPerson.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllPerson.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllPerson.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllPerson.Location = New System.Drawing.Point(399, 91)
        Me.chkAllPerson.Name = "chkAllPerson"
        Me.chkAllPerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllPerson.Size = New System.Drawing.Size(48, 18)
        Me.chkAllPerson.TabIndex = 38
        Me.chkAllPerson.Text = "ALL"
        Me.chkAllPerson.UseVisualStyleBackColor = False
        '
        'txtSalePerson
        '
        Me.txtSalePerson.AcceptsReturn = True
        Me.txtSalePerson.BackColor = System.Drawing.SystemColors.Window
        Me.txtSalePerson.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalePerson.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSalePerson.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalePerson.ForeColor = System.Drawing.Color.Blue
        Me.txtSalePerson.Location = New System.Drawing.Point(67, 89)
        Me.txtSalePerson.MaxLength = 0
        Me.txtSalePerson.Name = "txtSalePerson"
        Me.txtSalePerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSalePerson.Size = New System.Drawing.Size(295, 20)
        Me.txtSalePerson.TabIndex = 37
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(10, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(53, 14)
        Me.Label2.TabIndex = 39
        Me.Label2.Text = "Person :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(22, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 14)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Party :"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(67, 38)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(295, 22)
        Me.cboDivision.TabIndex = 34
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(399, 16)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(48, 18)
        Me.chkAll.TabIndex = 28
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'chkAllGroup
        '
        Me.chkAllGroup.AutoSize = True
        Me.chkAllGroup.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllGroup.Checked = True
        Me.chkAllGroup.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllGroup.Location = New System.Drawing.Point(399, 66)
        Me.chkAllGroup.Name = "chkAllGroup"
        Me.chkAllGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllGroup.Size = New System.Drawing.Size(48, 18)
        Me.chkAllGroup.TabIndex = 26
        Me.chkAllGroup.Text = "ALL"
        Me.chkAllGroup.UseVisualStyleBackColor = False
        '
        'TxtGroup
        '
        Me.TxtGroup.AcceptsReturn = True
        Me.TxtGroup.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGroup.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGroup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGroup.ForeColor = System.Drawing.Color.Blue
        Me.TxtGroup.Location = New System.Drawing.Point(67, 64)
        Me.TxtGroup.MaxLength = 0
        Me.TxtGroup.Name = "TxtGroup"
        Me.TxtGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGroup.Size = New System.Drawing.Size(295, 20)
        Me.TxtGroup.TabIndex = 25
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(67, 14)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(295, 20)
        Me.TxtName.TabIndex = 4
        '
        '_Lbl_7
        '
        Me._Lbl_7.AutoSize = True
        Me._Lbl_7.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_7, CType(7, Short))
        Me._Lbl_7.Location = New System.Drawing.Point(7, 41)
        Me._Lbl_7.Name = "_Lbl_7"
        Me._Lbl_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_7.Size = New System.Drawing.Size(56, 14)
        Me._Lbl_7.TabIndex = 35
        Me._Lbl_7.Text = "Division :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(16, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(47, 14)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "Group :"
        '
        'fraCostC
        '
        Me.fraCostC.BackColor = System.Drawing.SystemColors.Control
        Me.fraCostC.Controls.Add(Me.chkLegelNotice)
        Me.fraCostC.Controls.Add(Me.chkPrintListFormat)
        Me.fraCostC.Controls.Add(Me.chkReminderLetter)
        Me.fraCostC.Controls.Add(Me.CboShowFor)
        Me.fraCostC.Controls.Add(Me._Lbl_0)
        Me.fraCostC.Controls.Add(Me.Report1)
        Me.fraCostC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraCostC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraCostC.Location = New System.Drawing.Point(935, 0)
        Me.fraCostC.Name = "fraCostC"
        Me.fraCostC.Padding = New System.Windows.Forms.Padding(0)
        Me.fraCostC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraCostC.Size = New System.Drawing.Size(170, 114)
        Me.fraCostC.TabIndex = 15
        Me.fraCostC.TabStop = False
        '
        'chkLegelNotice
        '
        Me.chkLegelNotice.AutoSize = True
        Me.chkLegelNotice.BackColor = System.Drawing.SystemColors.Control
        Me.chkLegelNotice.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkLegelNotice.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLegelNotice.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLegelNotice.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLegelNotice.Location = New System.Drawing.Point(72, 70)
        Me.chkLegelNotice.Name = "chkLegelNotice"
        Me.chkLegelNotice.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLegelNotice.Size = New System.Drawing.Size(94, 18)
        Me.chkLegelNotice.TabIndex = 33
        Me.chkLegelNotice.Text = "Legel Notice"
        Me.chkLegelNotice.UseVisualStyleBackColor = False
        '
        'chkPrintListFormat
        '
        Me.chkPrintListFormat.AutoSize = True
        Me.chkPrintListFormat.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintListFormat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkPrintListFormat.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintListFormat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintListFormat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintListFormat.Location = New System.Drawing.Point(48, 52)
        Me.chkPrintListFormat.Name = "chkPrintListFormat"
        Me.chkPrintListFormat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintListFormat.Size = New System.Drawing.Size(118, 18)
        Me.chkPrintListFormat.TabIndex = 24
        Me.chkPrintListFormat.Text = "Print List Format"
        Me.chkPrintListFormat.UseVisualStyleBackColor = False
        '
        'chkReminderLetter
        '
        Me.chkReminderLetter.AutoSize = True
        Me.chkReminderLetter.BackColor = System.Drawing.SystemColors.Control
        Me.chkReminderLetter.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.chkReminderLetter.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkReminderLetter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkReminderLetter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkReminderLetter.Location = New System.Drawing.Point(49, 34)
        Me.chkReminderLetter.Name = "chkReminderLetter"
        Me.chkReminderLetter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkReminderLetter.Size = New System.Drawing.Size(117, 18)
        Me.chkReminderLetter.TabIndex = 7
        Me.chkReminderLetter.Text = "Reminder Letter"
        Me.chkReminderLetter.UseVisualStyleBackColor = False
        '
        'CboShowFor
        '
        Me.CboShowFor.BackColor = System.Drawing.SystemColors.Window
        Me.CboShowFor.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboShowFor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboShowFor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboShowFor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboShowFor.Location = New System.Drawing.Point(60, 10)
        Me.CboShowFor.Name = "CboShowFor"
        Me.CboShowFor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboShowFor.Size = New System.Drawing.Size(107, 22)
        Me.CboShowFor.TabIndex = 6
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 14)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(44, 14)
        Me._Lbl_0.TabIndex = 16
        Me._Lbl_0.Text = "Show :"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(62, 75)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 30
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
        Me.lblOutsType.TabIndex = 19
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
        Me.lblAddress.TabIndex = 17
        Me.lblAddress.Text = "Address"
        Me.lblAddress.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(738, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(192, 116)
        Me.GroupBox1.TabIndex = 67
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
        Me.lstCompanyName.Size = New System.Drawing.Size(192, 103)
        Me.lstCompanyName.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.optDues_0)
        Me.GroupBox2.Controls.Add(Me.optDues_1)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox2.Location = New System.Drawing.Point(111, 57)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox2.Size = New System.Drawing.Size(175, 33)
        Me.GroupBox2.TabIndex = 68
        Me.GroupBox2.TabStop = False
        '
        'optDues_0
        '
        Me.optDues_0.AutoSize = True
        Me.optDues_0.BackColor = System.Drawing.SystemColors.Control
        Me.optDues_0.Checked = True
        Me.optDues_0.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDues_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDues_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDueShow.SetIndex(Me.optDues_0, CType(0, Short))
        Me.optDues_0.Location = New System.Drawing.Point(3, 10)
        Me.optDues_0.Name = "optDues_0"
        Me.optDues_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDues_0.Size = New System.Drawing.Size(65, 18)
        Me.optDues_0.TabIndex = 0
        Me.optDues_0.TabStop = True
        Me.optDues_0.Text = "All Bills"
        Me.optDues_0.UseVisualStyleBackColor = False
        '
        'optDues_1
        '
        Me.optDues_1.AutoSize = True
        Me.optDues_1.BackColor = System.Drawing.SystemColors.Control
        Me.optDues_1.Cursor = System.Windows.Forms.Cursors.Default
        Me.optDues_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDues_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDueShow.SetIndex(Me.optDues_1, CType(1, Short))
        Me.optDues_1.Location = New System.Drawing.Point(89, 10)
        Me.optDues_1.Name = "optDues_1"
        Me.optDues_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optDues_1.Size = New System.Drawing.Size(80, 18)
        Me.optDues_1.TabIndex = 1
        Me.optDues_1.TabStop = True
        Me.optDues_1.Text = "Only Dues"
        Me.optDues_1.UseVisualStyleBackColor = False
        '
        'optGroupWise
        '
        Me.optGroupWise.AutoSize = True
        Me.optGroupWise.BackColor = System.Drawing.SystemColors.Control
        Me.optGroupWise.Cursor = System.Windows.Forms.Cursors.Default
        Me.optGroupWise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optGroupWise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGroupWise.Location = New System.Drawing.Point(197, 94)
        Me.optGroupWise.Name = "optGroupWise"
        Me.optGroupWise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optGroupWise.Size = New System.Drawing.Size(89, 18)
        Me.optGroupWise.TabIndex = 70
        Me.optGroupWise.Text = "Group Wise"
        Me.optGroupWise.UseVisualStyleBackColor = False
        '
        'optPartyWise
        '
        Me.optPartyWise.AutoSize = True
        Me.optPartyWise.BackColor = System.Drawing.SystemColors.Control
        Me.optPartyWise.Checked = True
        Me.optPartyWise.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPartyWise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPartyWise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPartyWise.Location = New System.Drawing.Point(111, 94)
        Me.optPartyWise.Name = "optPartyWise"
        Me.optPartyWise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPartyWise.Size = New System.Drawing.Size(83, 18)
        Me.optPartyWise.TabIndex = 69
        Me.optPartyWise.TabStop = True
        Me.optPartyWise.Text = "Party Wise"
        Me.optPartyWise.UseVisualStyleBackColor = False
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(10, 53)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(87, 20)
        Me.txtDateFrom.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(12, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(42, 14)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "From :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(9, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(26, 14)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "To :"
        '
        'frmViewOuts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.optGroupWise)
        Me.Controls.Add(Me.optPartyWise)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.FraShow)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.fraDate)
        Me.Controls.Add(Me.fraParty)
        Me.Controls.Add(Me.fraCostC)
        Me.Controls.Add(Me.lblOutsType)
        Me.Controls.Add(Me.lblAddress)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 15)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewOuts"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "View Outstanding"
        Me.FraShow.ResumeLayout(False)
        Me.FraShow.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
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
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optAsOn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.optDueShow, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
    Public WithEvents Label1 As Label
    Public WithEvents GroupBox2 As GroupBox
    Public WithEvents optDues_0 As RadioButton
    Public WithEvents optDues_1 As RadioButton
    Public WithEvents optDueShow As VB6.RadioButtonArray
    Public WithEvents chkAllPerson As CheckBox
    Public WithEvents txtSalePerson As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents optGroupWise As RadioButton
    Public WithEvents optPartyWise As RadioButton
    Public WithEvents Label5 As Label
    Public WithEvents Label3 As Label
    Public WithEvents txtDateFrom As MaskedTextBox
#End Region
End Class