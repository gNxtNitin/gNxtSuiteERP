Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAgeingAnalysis
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
    Public WithEvents chkLetter As System.Windows.Forms.CheckBox
    Public WithEvents chkHideZero As System.Windows.Forms.CheckBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptDays_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDays_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _OptShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents FraShow As System.Windows.Forms.GroupBox
    Public WithEvents lblPaidDays3 As System.Windows.Forms.Label
    Public WithEvents lblPaidDays4 As System.Windows.Forms.Label
    Public WithEvents lblPaidDays1 As System.Windows.Forms.Label
    Public WithEvents lblPaidDays2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDays1 As System.Windows.Forms.TextBox
    Public WithEvents txtDays2 As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraDayCategory As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents _OptSumDet_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents chkAllGroup As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents TxtGroup As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents _Lbl_7 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents _OptDueDate_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDueDate_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDueDate_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdAgeing As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptDays As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptDueDate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptSumDet As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAgeingAnalysis))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkLetter = New System.Windows.Forms.CheckBox()
        Me.chkHideZero = New System.Windows.Forms.CheckBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptDays_1 = New System.Windows.Forms.RadioButton()
        Me._OptDays_0 = New System.Windows.Forms.RadioButton()
        Me.FraShow = New System.Windows.Forms.GroupBox()
        Me._OptShow_2 = New System.Windows.Forms.RadioButton()
        Me._OptShow_0 = New System.Windows.Forms.RadioButton()
        Me._OptShow_1 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.lblPaidDays3 = New System.Windows.Forms.Label()
        Me.lblPaidDays4 = New System.Windows.Forms.Label()
        Me.lblPaidDays1 = New System.Windows.Forms.Label()
        Me.lblPaidDays2 = New System.Windows.Forms.Label()
        Me.FraDayCategory = New System.Windows.Forms.GroupBox()
        Me.txtDays1 = New System.Windows.Forms.TextBox()
        Me.txtDays2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._OptSumDet_1 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_0 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.chkAllGroup = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.TxtGroup = New System.Windows.Forms.TextBox()
        Me._Lbl_7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me._OptDueDate_2 = New System.Windows.Forms.RadioButton()
        Me._OptDueDate_1 = New System.Windows.Forms.RadioButton()
        Me._OptDueDate_0 = New System.Windows.Forms.RadioButton()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdAgeing = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptDays = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptDueDate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptSumDet = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraShow.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraDayCategory.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdAgeing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptDays, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(131, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(63, 37)
        Me.CmdPreview.TabIndex = 12
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
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
        Me.cmdPrint.Location = New System.Drawing.Point(67, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(63, 37)
        Me.cmdPrint.TabIndex = 11
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print ")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(194, 9)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(63, 37)
        Me.cmdClose.TabIndex = 13
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(63, 37)
        Me.cmdShow.TabIndex = 10
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(472, 10)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdsearch.TabIndex = 4
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(81, 10)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(389, 19)
        Me.TxtAccount.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkLetter)
        Me.Frame3.Controls.Add(Me.chkHideZero)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(504, 83)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(245, 40)
        Me.Frame3.TabIndex = 38
        Me.Frame3.TabStop = False
        '
        'chkLetter
        '
        Me.chkLetter.AutoSize = True
        Me.chkLetter.BackColor = System.Drawing.SystemColors.Control
        Me.chkLetter.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLetter.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLetter.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLetter.Location = New System.Drawing.Point(130, 14)
        Me.chkLetter.Name = "chkLetter"
        Me.chkLetter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLetter.Size = New System.Drawing.Size(91, 18)
        Me.chkLetter.TabIndex = 40
        Me.chkLetter.Text = "Debit Letter"
        Me.chkLetter.UseVisualStyleBackColor = False
        '
        'chkHideZero
        '
        Me.chkHideZero.AutoSize = True
        Me.chkHideZero.BackColor = System.Drawing.SystemColors.Control
        Me.chkHideZero.Checked = True
        Me.chkHideZero.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHideZero.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHideZero.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHideZero.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHideZero.Location = New System.Drawing.Point(6, 14)
        Me.chkHideZero.Name = "chkHideZero"
        Me.chkHideZero.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHideZero.Size = New System.Drawing.Size(101, 18)
        Me.chkHideZero.TabIndex = 39
        Me.chkHideZero.Text = "Hide Zero Bal."
        Me.chkHideZero.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptDays_1)
        Me.Frame2.Controls.Add(Me._OptDays_0)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(782, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(115, 84)
        Me.Frame2.TabIndex = 37
        Me.Frame2.TabStop = False
        '
        '_OptDays_1
        '
        Me._OptDays_1.AutoSize = True
        Me._OptDays_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptDays_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDays_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDays_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDays.SetIndex(Me._OptDays_1, CType(1, Short))
        Me._OptDays_1.Location = New System.Drawing.Point(8, 41)
        Me._OptDays_1.Name = "_OptDays_1"
        Me._OptDays_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDays_1.Size = New System.Drawing.Size(39, 18)
        Me._OptDays_1.TabIndex = 42
        Me._OptDays_1.TabStop = True
        Me._OptDays_1.Text = "All"
        Me._OptDays_1.UseVisualStyleBackColor = False
        '
        '_OptDays_0
        '
        Me._OptDays_0.AutoSize = True
        Me._OptDays_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptDays_0.Checked = True
        Me._OptDays_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDays_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDays_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDays.SetIndex(Me._OptDays_0, CType(0, Short))
        Me._OptDays_0.Location = New System.Drawing.Point(8, 18)
        Me._OptDays_0.Name = "_OptDays_0"
        Me._OptDays_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDays_0.Size = New System.Drawing.Size(73, 18)
        Me._OptDays_0.TabIndex = 41
        Me._OptDays_0.TabStop = True
        Me._OptDays_0.Text = "Due Date"
        Me._OptDays_0.UseVisualStyleBackColor = False
        '
        'FraShow
        '
        Me.FraShow.BackColor = System.Drawing.SystemColors.Control
        Me.FraShow.Controls.Add(Me._OptShow_2)
        Me.FraShow.Controls.Add(Me._OptShow_0)
        Me.FraShow.Controls.Add(Me._OptShow_1)
        Me.FraShow.Enabled = False
        Me.FraShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraShow.Location = New System.Drawing.Point(212, 83)
        Me.FraShow.Name = "FraShow"
        Me.FraShow.Padding = New System.Windows.Forms.Padding(0)
        Me.FraShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraShow.Size = New System.Drawing.Size(291, 40)
        Me.FraShow.TabIndex = 32
        Me.FraShow.TabStop = False
        Me.FraShow.Text = "Show"
        '
        '_OptShow_2
        '
        Me._OptShow_2.AutoSize = True
        Me._OptShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_2, CType(2, Short))
        Me._OptShow_2.Location = New System.Drawing.Point(210, 14)
        Me._OptShow_2.Name = "_OptShow_2"
        Me._OptShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_2.Size = New System.Drawing.Size(59, 18)
        Me._OptShow_2.TabIndex = 35
        Me._OptShow_2.TabStop = True
        Me._OptShow_2.Text = "Credit"
        Me._OptShow_2.UseVisualStyleBackColor = False
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
        Me._OptShow_0.Location = New System.Drawing.Point(16, 14)
        Me._OptShow_0.Name = "_OptShow_0"
        Me._OptShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_0.Size = New System.Drawing.Size(39, 18)
        Me._OptShow_0.TabIndex = 34
        Me._OptShow_0.TabStop = True
        Me._OptShow_0.Text = "All"
        Me._OptShow_0.UseVisualStyleBackColor = False
        '
        '_OptShow_1
        '
        Me._OptShow_1.AutoSize = True
        Me._OptShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_1, CType(1, Short))
        Me._OptShow_1.Location = New System.Drawing.Point(104, 14)
        Me._OptShow_1.Name = "_OptShow_1"
        Me._OptShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_1.Size = New System.Drawing.Size(53, 18)
        Me._OptShow_1.TabIndex = 33
        Me._OptShow_1.TabStop = True
        Me._OptShow_1.Text = "Debit"
        Me._OptShow_1.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.lblPaidDays3)
        Me.Frame1.Controls.Add(Me.lblPaidDays4)
        Me.Frame1.Controls.Add(Me.lblPaidDays1)
        Me.Frame1.Controls.Add(Me.lblPaidDays2)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(340, 564)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(249, 49)
        Me.Frame1.TabIndex = 22
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Paid Days"
        '
        'lblPaidDays3
        '
        Me.lblPaidDays3.AutoSize = True
        Me.lblPaidDays3.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaidDays3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaidDays3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaidDays3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaidDays3.Location = New System.Drawing.Point(118, 20)
        Me.lblPaidDays3.Name = "lblPaidDays3"
        Me.lblPaidDays3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaidDays3.Size = New System.Drawing.Size(75, 14)
        Me.lblPaidDays3.TabIndex = 26
        Me.lblPaidDays3.Text = "lblPaidDays1"
        '
        'lblPaidDays4
        '
        Me.lblPaidDays4.AutoSize = True
        Me.lblPaidDays4.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaidDays4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaidDays4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaidDays4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaidDays4.Location = New System.Drawing.Point(172, 20)
        Me.lblPaidDays4.Name = "lblPaidDays4"
        Me.lblPaidDays4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaidDays4.Size = New System.Drawing.Size(75, 14)
        Me.lblPaidDays4.TabIndex = 25
        Me.lblPaidDays4.Text = "lblPaidDays1"
        '
        'lblPaidDays1
        '
        Me.lblPaidDays1.AutoSize = True
        Me.lblPaidDays1.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaidDays1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaidDays1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaidDays1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaidDays1.Location = New System.Drawing.Point(12, 20)
        Me.lblPaidDays1.Name = "lblPaidDays1"
        Me.lblPaidDays1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaidDays1.Size = New System.Drawing.Size(75, 14)
        Me.lblPaidDays1.TabIndex = 24
        Me.lblPaidDays1.Text = "lblPaidDays1"
        '
        'lblPaidDays2
        '
        Me.lblPaidDays2.AutoSize = True
        Me.lblPaidDays2.BackColor = System.Drawing.SystemColors.Control
        Me.lblPaidDays2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaidDays2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaidDays2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaidDays2.Location = New System.Drawing.Point(64, 20)
        Me.lblPaidDays2.Name = "lblPaidDays2"
        Me.lblPaidDays2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaidDays2.Size = New System.Drawing.Size(75, 14)
        Me.lblPaidDays2.TabIndex = 23
        Me.lblPaidDays2.Text = "lblPaidDays1"
        '
        'FraDayCategory
        '
        Me.FraDayCategory.BackColor = System.Drawing.SystemColors.Control
        Me.FraDayCategory.Controls.Add(Me.txtDays1)
        Me.FraDayCategory.Controls.Add(Me.txtDays2)
        Me.FraDayCategory.Controls.Add(Me.Label2)
        Me.FraDayCategory.Controls.Add(Me.Label1)
        Me.FraDayCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraDayCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraDayCategory.Location = New System.Drawing.Point(2, 562)
        Me.FraDayCategory.Name = "FraDayCategory"
        Me.FraDayCategory.Padding = New System.Windows.Forms.Padding(0)
        Me.FraDayCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraDayCategory.Size = New System.Drawing.Size(237, 49)
        Me.FraDayCategory.TabIndex = 19
        Me.FraDayCategory.TabStop = False
        Me.FraDayCategory.Text = "Days Category"
        '
        'txtDays1
        '
        Me.txtDays1.AcceptsReturn = True
        Me.txtDays1.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays1.Location = New System.Drawing.Point(22, 28)
        Me.txtDays1.MaxLength = 4
        Me.txtDays1.Name = "txtDays1"
        Me.txtDays1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays1.Size = New System.Drawing.Size(40, 19)
        Me.txtDays1.TabIndex = 8
        '
        'txtDays2
        '
        Me.txtDays2.AcceptsReturn = True
        Me.txtDays2.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays2.Location = New System.Drawing.Point(134, 28)
        Me.txtDays2.MaxLength = 4
        Me.txtDays2.Name = "txtDays2"
        Me.txtDays2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays2.Size = New System.Drawing.Size(40, 19)
        Me.txtDays2.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(136, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(35, 14)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Day 2"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(24, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(35, 14)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Day 1"
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(640, 562)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(261, 49)
        Me.FraMovement.TabIndex = 18
        Me.FraMovement.TabStop = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me._OptSumDet_1)
        Me.Frame7.Controls.Add(Me._OptSumDet_0)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(0, 83)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(211, 40)
        Me.Frame7.TabIndex = 17
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Format"
        '
        '_OptSumDet_1
        '
        Me._OptSumDet_1.AutoSize = True
        Me._OptSumDet_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_1, CType(1, Short))
        Me._OptSumDet_1.Location = New System.Drawing.Point(110, 14)
        Me._OptSumDet_1.Name = "_OptSumDet_1"
        Me._OptSumDet_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_1.Size = New System.Drawing.Size(96, 18)
        Me._OptSumDet_1.TabIndex = 6
        Me._OptSumDet_1.TabStop = True
        Me._OptSumDet_1.Text = "Summarised"
        Me._OptSumDet_1.UseVisualStyleBackColor = False
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
        Me._OptSumDet_0.Location = New System.Drawing.Point(4, 14)
        Me._OptSumDet_0.Name = "_OptSumDet_0"
        Me._OptSumDet_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_0.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_0.TabIndex = 5
        Me._OptSumDet_0.TabStop = True
        Me._OptSumDet_0.Text = "Detailed"
        Me._OptSumDet_0.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.cboDivision)
        Me.FraAccount.Controls.Add(Me.chkAllGroup)
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.TxtGroup)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Controls.Add(Me._Lbl_7)
        Me.FraAccount.Controls.Add(Me.Label4)
        Me.FraAccount.Controls.Add(Me.Label3)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(184, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(594, 86)
        Me.FraAccount.TabIndex = 16
        Me.FraAccount.TabStop = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(81, 33)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(389, 22)
        Me.cboDivision.TabIndex = 43
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
        Me.chkAllGroup.Location = New System.Drawing.Point(508, 48)
        Me.chkAllGroup.Name = "chkAllGroup"
        Me.chkAllGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllGroup.Size = New System.Drawing.Size(48, 18)
        Me.chkAllGroup.TabIndex = 31
        Me.chkAllGroup.Text = "ALL"
        Me.chkAllGroup.UseVisualStyleBackColor = False
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
        Me.chkAll.Location = New System.Drawing.Point(507, 12)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(48, 18)
        Me.chkAll.TabIndex = 30
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'TxtGroup
        '
        Me.TxtGroup.AcceptsReturn = True
        Me.TxtGroup.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGroup.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGroup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGroup.ForeColor = System.Drawing.Color.Blue
        Me.TxtGroup.Location = New System.Drawing.Point(81, 59)
        Me.TxtGroup.MaxLength = 0
        Me.TxtGroup.Name = "TxtGroup"
        Me.TxtGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGroup.Size = New System.Drawing.Size(389, 19)
        Me.TxtGroup.TabIndex = 28
        '
        '_Lbl_7
        '
        Me._Lbl_7.AutoSize = True
        Me._Lbl_7.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_7, CType(7, Short))
        Me._Lbl_7.Location = New System.Drawing.Point(21, 35)
        Me._Lbl_7.Name = "_Lbl_7"
        Me._Lbl_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_7.Size = New System.Drawing.Size(56, 14)
        Me._Lbl_7.TabIndex = 44
        Me._Lbl_7.Text = "Division :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(30, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(47, 14)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Group :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "Customer :"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me._OptDueDate_2)
        Me.Frame6.Controls.Add(Me._OptDueDate_1)
        Me.Frame6.Controls.Add(Me._OptDueDate_0)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(183, 86)
        Me.Frame6.TabIndex = 15
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "As on Date"
        '
        '_OptDueDate_2
        '
        Me._OptDueDate_2.AutoSize = True
        Me._OptDueDate_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptDueDate_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDueDate_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDueDate_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDueDate.SetIndex(Me._OptDueDate_2, CType(2, Short))
        Me._OptDueDate_2.Location = New System.Drawing.Point(124, 21)
        Me._OptDueDate_2.Name = "_OptDueDate_2"
        Me._OptDueDate_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDueDate_2.Size = New System.Drawing.Size(46, 18)
        Me._OptDueDate_2.TabIndex = 36
        Me._OptDueDate_2.TabStop = True
        Me._OptDueDate_2.Text = "Due"
        Me._OptDueDate_2.UseVisualStyleBackColor = False
        '
        '_OptDueDate_1
        '
        Me._OptDueDate_1.AutoSize = True
        Me._OptDueDate_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptDueDate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDueDate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDueDate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDueDate.SetIndex(Me._OptDueDate_1, CType(1, Short))
        Me._OptDueDate_1.Location = New System.Drawing.Point(70, 21)
        Me._OptDueDate_1.Name = "_OptDueDate_1"
        Me._OptDueDate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDueDate_1.Size = New System.Drawing.Size(41, 18)
        Me._OptDueDate_1.TabIndex = 1
        Me._OptDueDate_1.TabStop = True
        Me._OptDueDate_1.Text = "Bill"
        Me._OptDueDate_1.UseVisualStyleBackColor = False
        '
        '_OptDueDate_0
        '
        Me._OptDueDate_0.AutoSize = True
        Me._OptDueDate_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptDueDate_0.Checked = True
        Me._OptDueDate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDueDate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDueDate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDueDate.SetIndex(Me._OptDueDate_0, CType(0, Short))
        Me._OptDueDate_0.Location = New System.Drawing.Point(8, 21)
        Me._OptDueDate_0.Name = "_OptDueDate_0"
        Me._OptDueDate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDueDate_0.Size = New System.Drawing.Size(49, 18)
        Me._OptDueDate_0.TabIndex = 0
        Me._OptDueDate_0.TabStop = True
        Me._OptDueDate_0.Text = "MRR"
        Me._OptDueDate_0.UseVisualStyleBackColor = False
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(22, 47)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(83, 20)
        Me.txtDateTo.TabIndex = 2
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdAgeing)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 118)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(896, 442)
        Me.Frame4.TabIndex = 14
        Me.Frame4.TabStop = False
        '
        'SprdAgeing
        '
        Me.SprdAgeing.DataSource = Nothing
        Me.SprdAgeing.Location = New System.Drawing.Point(2, 8)
        Me.SprdAgeing.Name = "SprdAgeing"
        Me.SprdAgeing.OcxState = CType(resources.GetObject("SprdAgeing.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdAgeing.Size = New System.Drawing.Size(892, 430)
        Me.SprdAgeing.TabIndex = 7
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 78)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 9
        '
        'OptDueDate
        '
        '
        'OptSumDet
        '
        '
        'frmAgeingAnalysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.FraShow)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraDayCategory)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAgeingAnalysis"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Outstanding (Age Wise)"
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraShow.ResumeLayout(False)
        Me.FraShow.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraDayCategory.ResumeLayout(False)
        Me.FraDayCategory.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdAgeing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptDays, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdAgeing.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdAgeing.DataSource = Nothing
    End Sub
#End Region
End Class