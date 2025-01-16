Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewTrailBalCal
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
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _txtDate_1 As System.Windows.Forms.MaskedTextBox
    Public WithEvents _txtDate_0 As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _optPrint_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPrint_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents ChkHideZeroBal As System.Windows.Forms.CheckBox
    Public WithEvents ChkHideZeroTrans As System.Windows.Forms.CheckBox
    Public WithEvents FraHideRow As System.Windows.Forms.GroupBox
    Public WithEvents _chkGroup_9 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_0 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_3 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_6 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_4 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_7 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_5 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_8 As System.Windows.Forms.CheckBox
    Public WithEvents FraOption As System.Windows.Forms.GroupBox
    Public WithEvents chkPnLFlag As System.Windows.Forms.CheckBox
    Public WithEvents _OptGroup_6 As System.Windows.Forms.RadioButton
    Public WithEvents _OptGroup_5 As System.Windows.Forms.RadioButton
    Public WithEvents _OptGroup_4 As System.Windows.Forms.RadioButton
    Public WithEvents _OptGroup_3 As System.Windows.Forms.RadioButton
    Public WithEvents ChkAllGroup As System.Windows.Forms.CheckBox
    Public WithEvents TxtGroup As System.Windows.Forms.TextBox
    Public WithEvents _OptGroup_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptGroup_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptGroup_0 As System.Windows.Forms.RadioButton
    Public WithEvents ViewReport As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public CMDialog1Open As System.Windows.Forms.OpenFileDialog
    Public CMDialog1Save As System.Windows.Forms.SaveFileDialog
    Public CMDialog1Font As System.Windows.Forms.FontDialog
    Public CMDialog1Color As System.Windows.Forms.ColorDialog
    Public CMDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CboCC As System.Windows.Forms.ComboBox
    Public WithEvents CboDept As System.Windows.Forms.ComboBox
    Public WithEvents lblCC As System.Windows.Forms.Label
    Public WithEvents lblDept As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptGroup As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents chkGroup As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    Public WithEvents optPrint As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents txtDate As Microsoft.VisualBasic.Compatibility.VB6.MaskedTextBoxArray
    Public WithEvents txtDate1 As Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewTrailBalCal))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._txtDate_1 = New System.Windows.Forms.MaskedTextBox()
        Me._txtDate_0 = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optPrint_1 = New System.Windows.Forms.RadioButton()
        Me._optPrint_0 = New System.Windows.Forms.RadioButton()
        Me.FraHideRow = New System.Windows.Forms.GroupBox()
        Me.ChkHideZeroBal = New System.Windows.Forms.CheckBox()
        Me.ChkHideZeroTrans = New System.Windows.Forms.CheckBox()
        Me.FraOption = New System.Windows.Forms.GroupBox()
        Me._chkGroup_9 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_0 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_3 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_6 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_1 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_4 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_7 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_2 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_5 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_8 = New System.Windows.Forms.CheckBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkPnLFlag = New System.Windows.Forms.CheckBox()
        Me._OptGroup_6 = New System.Windows.Forms.RadioButton()
        Me._OptGroup_5 = New System.Windows.Forms.RadioButton()
        Me._OptGroup_4 = New System.Windows.Forms.RadioButton()
        Me._OptGroup_3 = New System.Windows.Forms.RadioButton()
        Me.ChkAllGroup = New System.Windows.Forms.CheckBox()
        Me.TxtGroup = New System.Windows.Forms.TextBox()
        Me._OptGroup_2 = New System.Windows.Forms.RadioButton()
        Me._OptGroup_1 = New System.Windows.Forms.RadioButton()
        Me._OptGroup_0 = New System.Windows.Forms.RadioButton()
        Me.ViewReport = New System.Windows.Forms.Label()
        Me.CMDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CMDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CMDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CMDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CMDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.CboCC = New System.Windows.Forms.ComboBox()
        Me.CboDept = New System.Windows.Forms.ComboBox()
        Me.lblCC = New System.Windows.Forms.Label()
        Me.lblDept = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptGroup = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.chkGroup = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.optPrint = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.txtDate = New Microsoft.VisualBasic.Compatibility.VB6.MaskedTextBoxArray(Me.components)
        Me.txtDate1 = New Microsoft.VisualBasic.Compatibility.VB6.TextBoxArray(Me.components)
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.Frame7.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraHideRow.SuspendLayout()
        Me.FraOption.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame6.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPrint, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtDate1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(138, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 27
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(71, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 26
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdExit.Location = New System.Drawing.Point(206, 9)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(67, 37)
        Me.cmdExit.TabIndex = 28
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 25
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboDivision)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(269, -2)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(299, 48)
        Me.Frame7.TabIndex = 49
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(4, 18)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(239, 22)
        Me.cboDivision.TabIndex = 50
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.lstCompanyName)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(569, -2)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(328, 106)
        Me.Frame3.TabIndex = 47
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Company Name"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._txtDate_1)
        Me.Frame4.Controls.Add(Me._txtDate_0)
        Me.Frame4.Controls.Add(Me._Lbl_1)
        Me.Frame4.Controls.Add(Me._Lbl_0)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -2)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(266, 48)
        Me.Frame4.TabIndex = 29
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Date"
        '
        '_txtDate_1
        '
        Me._txtDate_1.AllowPromptAsInput = False
        Me._txtDate_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.SetIndex(Me._txtDate_1, CType(1, Short))
        Me._txtDate_1.Location = New System.Drawing.Point(170, 18)
        Me._txtDate_1.Mask = "##/##/####"
        Me._txtDate_1.Name = "_txtDate_1"
        Me._txtDate_1.Size = New System.Drawing.Size(81, 20)
        Me._txtDate_1.TabIndex = 10
        '
        '_txtDate_0
        '
        Me._txtDate_0.AllowPromptAsInput = False
        Me._txtDate_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.SetIndex(Me._txtDate_0, CType(0, Short))
        Me._txtDate_0.Location = New System.Drawing.Point(46, 18)
        Me._txtDate_0.Mask = "##/##/####"
        Me._txtDate_0.Name = "_txtDate_0"
        Me._txtDate_0.Size = New System.Drawing.Size(81, 20)
        Me._txtDate_0.TabIndex = 9
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(144, 21)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(20, 14)
        Me._Lbl_1.TabIndex = 31
        Me._Lbl_1.Text = "To"
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 21)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(36, 14)
        Me._Lbl_0.TabIndex = 30
        Me._Lbl_0.Text = "From"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._optPrint_1)
        Me.Frame2.Controls.Add(Me._optPrint_0)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(-3, 562)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(267, 48)
        Me.Frame2.TabIndex = 42
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Print Option"
        '
        '_optPrint_1
        '
        Me._optPrint_1.AutoSize = True
        Me._optPrint_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPrint_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrint_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrint_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrint.SetIndex(Me._optPrint_1, CType(1, Short))
        Me._optPrint_1.Location = New System.Drawing.Point(146, 20)
        Me._optPrint_1.Name = "_optPrint_1"
        Me._optPrint_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrint_1.Size = New System.Drawing.Size(106, 18)
        Me._optPrint_1.TabIndex = 44
        Me._optPrint_1.TabStop = True
        Me._optPrint_1.Text = "All Transaction"
        Me._optPrint_1.UseVisualStyleBackColor = False
        '
        '_optPrint_0
        '
        Me._optPrint_0.AutoSize = True
        Me._optPrint_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPrint_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrint_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrint_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrint.SetIndex(Me._optPrint_0, CType(0, Short))
        Me._optPrint_0.Location = New System.Drawing.Point(10, 20)
        Me._optPrint_0.Name = "_optPrint_0"
        Me._optPrint_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrint_0.Size = New System.Drawing.Size(101, 18)
        Me._optPrint_0.TabIndex = 43
        Me._optPrint_0.TabStop = True
        Me._optPrint_0.Text = "Only Balances"
        Me._optPrint_0.UseVisualStyleBackColor = False
        '
        'FraHideRow
        '
        Me.FraHideRow.BackColor = System.Drawing.SystemColors.Control
        Me.FraHideRow.Controls.Add(Me.ChkHideZeroBal)
        Me.FraHideRow.Controls.Add(Me.ChkHideZeroTrans)
        Me.FraHideRow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraHideRow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraHideRow.Location = New System.Drawing.Point(268, 562)
        Me.FraHideRow.Name = "FraHideRow"
        Me.FraHideRow.Padding = New System.Windows.Forms.Padding(0)
        Me.FraHideRow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraHideRow.Size = New System.Drawing.Size(354, 48)
        Me.FraHideRow.TabIndex = 32
        Me.FraHideRow.TabStop = False
        Me.FraHideRow.Text = "Hide"
        Me.FraHideRow.Visible = False
        '
        'ChkHideZeroBal
        '
        Me.ChkHideZeroBal.AutoSize = True
        Me.ChkHideZeroBal.BackColor = System.Drawing.SystemColors.Control
        Me.ChkHideZeroBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkHideZeroBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkHideZeroBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkHideZeroBal.Location = New System.Drawing.Point(40, 19)
        Me.ChkHideZeroBal.Name = "ChkHideZeroBal"
        Me.ChkHideZeroBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkHideZeroBal.Size = New System.Drawing.Size(97, 18)
        Me.ChkHideZeroBal.TabIndex = 33
        Me.ChkHideZeroBal.Text = "Zero Balance"
        Me.ChkHideZeroBal.UseVisualStyleBackColor = False
        '
        'ChkHideZeroTrans
        '
        Me.ChkHideZeroTrans.AutoSize = True
        Me.ChkHideZeroTrans.BackColor = System.Drawing.SystemColors.Control
        Me.ChkHideZeroTrans.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkHideZeroTrans.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkHideZeroTrans.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkHideZeroTrans.Location = New System.Drawing.Point(204, 19)
        Me.ChkHideZeroTrans.Name = "ChkHideZeroTrans"
        Me.ChkHideZeroTrans.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkHideZeroTrans.Size = New System.Drawing.Size(142, 18)
        Me.ChkHideZeroTrans.TabIndex = 34
        Me.ChkHideZeroTrans.Text = "Without Transactions"
        Me.ChkHideZeroTrans.UseVisualStyleBackColor = False
        '
        'FraOption
        '
        Me.FraOption.BackColor = System.Drawing.SystemColors.Control
        Me.FraOption.Controls.Add(Me._chkGroup_9)
        Me.FraOption.Controls.Add(Me._chkGroup_0)
        Me.FraOption.Controls.Add(Me._chkGroup_3)
        Me.FraOption.Controls.Add(Me._chkGroup_6)
        Me.FraOption.Controls.Add(Me._chkGroup_1)
        Me.FraOption.Controls.Add(Me._chkGroup_4)
        Me.FraOption.Controls.Add(Me._chkGroup_7)
        Me.FraOption.Controls.Add(Me._chkGroup_2)
        Me.FraOption.Controls.Add(Me._chkGroup_5)
        Me.FraOption.Controls.Add(Me._chkGroup_8)
        Me.FraOption.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOption.Location = New System.Drawing.Point(0, 40)
        Me.FraOption.Name = "FraOption"
        Me.FraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOption.Size = New System.Drawing.Size(568, 64)
        Me.FraOption.TabIndex = 38
        Me.FraOption.TabStop = False
        '
        '_chkGroup_9
        '
        Me._chkGroup_9.AutoSize = True
        Me._chkGroup_9.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_9.Checked = True
        Me._chkGroup_9.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_9, CType(9, Short))
        Me._chkGroup_9.Location = New System.Drawing.Point(467, 39)
        Me._chkGroup_9.Name = "_chkGroup_9"
        Me._chkGroup_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_9.Size = New System.Drawing.Size(72, 18)
        Me._chkGroup_9.TabIndex = 22
        Me._chkGroup_9.Text = "Opening"
        Me._chkGroup_9.UseVisualStyleBackColor = False
        '
        '_chkGroup_0
        '
        Me._chkGroup_0.AutoSize = True
        Me._chkGroup_0.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_0.Checked = True
        Me._chkGroup_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_0, CType(0, Short))
        Me._chkGroup_0.Location = New System.Drawing.Point(6, 14)
        Me._chkGroup_0.Name = "_chkGroup_0"
        Me._chkGroup_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_0.Size = New System.Drawing.Size(53, 18)
        Me._chkGroup_0.TabIndex = 13
        Me._chkGroup_0.Text = "Bank"
        Me._chkGroup_0.UseVisualStyleBackColor = False
        '
        '_chkGroup_3
        '
        Me._chkGroup_3.AutoSize = True
        Me._chkGroup_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_3.Checked = True
        Me._chkGroup_3.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_3, CType(3, Short))
        Me._chkGroup_3.Location = New System.Drawing.Point(120, 39)
        Me._chkGroup_3.Name = "_chkGroup_3"
        Me._chkGroup_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_3.Size = New System.Drawing.Size(78, 18)
        Me._chkGroup_3.TabIndex = 19
        Me._chkGroup_3.Text = "Purchase"
        Me._chkGroup_3.UseVisualStyleBackColor = False
        '
        '_chkGroup_6
        '
        Me._chkGroup_6.AutoSize = True
        Me._chkGroup_6.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_6.Checked = True
        Me._chkGroup_6.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_6, CType(6, Short))
        Me._chkGroup_6.Location = New System.Drawing.Point(230, 14)
        Me._chkGroup_6.Name = "_chkGroup_6"
        Me._chkGroup_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_6.Size = New System.Drawing.Size(67, 18)
        Me._chkGroup_6.TabIndex = 21
        Me._chkGroup_6.Text = "Journal"
        Me._chkGroup_6.UseVisualStyleBackColor = False
        '
        '_chkGroup_1
        '
        Me._chkGroup_1.AutoSize = True
        Me._chkGroup_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_1.Checked = True
        Me._chkGroup_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_1, CType(1, Short))
        Me._chkGroup_1.Location = New System.Drawing.Point(6, 39)
        Me._chkGroup_1.Name = "_chkGroup_1"
        Me._chkGroup_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_1.Size = New System.Drawing.Size(54, 18)
        Me._chkGroup_1.TabIndex = 15
        Me._chkGroup_1.Text = "Cash"
        Me._chkGroup_1.UseVisualStyleBackColor = False
        '
        '_chkGroup_4
        '
        Me._chkGroup_4.AutoSize = True
        Me._chkGroup_4.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_4.Checked = True
        Me._chkGroup_4.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_4, CType(4, Short))
        Me._chkGroup_4.Location = New System.Drawing.Point(230, 39)
        Me._chkGroup_4.Name = "_chkGroup_4"
        Me._chkGroup_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_4.Size = New System.Drawing.Size(82, 18)
        Me._chkGroup_4.TabIndex = 14
        Me._chkGroup_4.Text = "Debit Note"
        Me._chkGroup_4.UseVisualStyleBackColor = False
        '
        '_chkGroup_7
        '
        Me._chkGroup_7.AutoSize = True
        Me._chkGroup_7.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_7.Checked = True
        Me._chkGroup_7.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_7, CType(7, Short))
        Me._chkGroup_7.Location = New System.Drawing.Point(467, 14)
        Me._chkGroup_7.Name = "_chkGroup_7"
        Me._chkGroup_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_7.Size = New System.Drawing.Size(63, 18)
        Me._chkGroup_7.TabIndex = 16
        Me._chkGroup_7.Text = "Contra"
        Me._chkGroup_7.UseVisualStyleBackColor = False
        '
        '_chkGroup_2
        '
        Me._chkGroup_2.AutoSize = True
        Me._chkGroup_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_2.Checked = True
        Me._chkGroup_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_2, CType(2, Short))
        Me._chkGroup_2.Location = New System.Drawing.Point(120, 14)
        Me._chkGroup_2.Name = "_chkGroup_2"
        Me._chkGroup_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_2.Size = New System.Drawing.Size(49, 18)
        Me._chkGroup_2.TabIndex = 17
        Me._chkGroup_2.Text = "Sale"
        Me._chkGroup_2.UseVisualStyleBackColor = False
        '
        '_chkGroup_5
        '
        Me._chkGroup_5.AutoSize = True
        Me._chkGroup_5.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_5.Checked = True
        Me._chkGroup_5.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_5, CType(5, Short))
        Me._chkGroup_5.Location = New System.Drawing.Point(358, 39)
        Me._chkGroup_5.Name = "_chkGroup_5"
        Me._chkGroup_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_5.Size = New System.Drawing.Size(88, 18)
        Me._chkGroup_5.TabIndex = 18
        Me._chkGroup_5.Text = "Credit Note"
        Me._chkGroup_5.UseVisualStyleBackColor = False
        '
        '_chkGroup_8
        '
        Me._chkGroup_8.AutoSize = True
        Me._chkGroup_8.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_8.Checked = True
        Me._chkGroup_8.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_8, CType(8, Short))
        Me._chkGroup_8.Location = New System.Drawing.Point(358, 14)
        Me._chkGroup_8.Name = "_chkGroup_8"
        Me._chkGroup_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_8.Size = New System.Drawing.Size(48, 18)
        Me._chkGroup_8.TabIndex = 20
        Me._chkGroup_8.Text = "PDC"
        Me._chkGroup_8.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkPnLFlag)
        Me.Frame1.Controls.Add(Me._OptGroup_6)
        Me.Frame1.Controls.Add(Me._OptGroup_5)
        Me.Frame1.Controls.Add(Me._OptGroup_4)
        Me.Frame1.Controls.Add(Me._OptGroup_3)
        Me.Frame1.Controls.Add(Me.ChkAllGroup)
        Me.Frame1.Controls.Add(Me.TxtGroup)
        Me.Frame1.Controls.Add(Me._OptGroup_2)
        Me.Frame1.Controls.Add(Me._OptGroup_1)
        Me.Frame1.Controls.Add(Me._OptGroup_0)
        Me.Frame1.Controls.Add(Me.ViewReport)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 104)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(898, 63)
        Me.Frame1.TabIndex = 39
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Details"
        '
        'chkPnLFlag
        '
        Me.chkPnLFlag.AutoSize = True
        Me.chkPnLFlag.BackColor = System.Drawing.SystemColors.Control
        Me.chkPnLFlag.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPnLFlag.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPnLFlag.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPnLFlag.Location = New System.Drawing.Point(723, 37)
        Me.chkPnLFlag.Name = "chkPnLFlag"
        Me.chkPnLFlag.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPnLFlag.Size = New System.Drawing.Size(169, 18)
        Me.chkPnLFlag.TabIndex = 46
        Me.chkPnLFlag.Text = "Exclude Final P && L Record"
        Me.chkPnLFlag.UseVisualStyleBackColor = False
        '
        '_OptGroup_6
        '
        Me._OptGroup_6.AutoSize = True
        Me._OptGroup_6.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_6, CType(6, Short))
        Me._OptGroup_6.Location = New System.Drawing.Point(725, 14)
        Me._OptGroup_6.Name = "_OptGroup_6"
        Me._OptGroup_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_6.Size = New System.Drawing.Size(78, 18)
        Me._OptGroup_6.TabIndex = 6
        Me._OptGroup_6.TabStop = True
        Me._OptGroup_6.Text = "Creditors"
        Me._OptGroup_6.UseVisualStyleBackColor = False
        '
        '_OptGroup_5
        '
        Me._OptGroup_5.AutoSize = True
        Me._OptGroup_5.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_5, CType(5, Short))
        Me._OptGroup_5.Location = New System.Drawing.Point(615, 14)
        Me._OptGroup_5.Name = "_OptGroup_5"
        Me._OptGroup_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_5.Size = New System.Drawing.Size(69, 18)
        Me._OptGroup_5.TabIndex = 4
        Me._OptGroup_5.TabStop = True
        Me._OptGroup_5.Text = "Debtors"
        Me._OptGroup_5.UseVisualStyleBackColor = False
        '
        '_OptGroup_4
        '
        Me._OptGroup_4.AutoSize = True
        Me._OptGroup_4.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_4.Checked = True
        Me._OptGroup_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_4, CType(4, Short))
        Me._OptGroup_4.Location = New System.Drawing.Point(506, 14)
        Me._OptGroup_4.Name = "_OptGroup_4"
        Me._OptGroup_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_4.Size = New System.Drawing.Size(68, 18)
        Me._OptGroup_4.TabIndex = 3
        Me._OptGroup_4.TabStop = True
        Me._OptGroup_4.Text = "General"
        Me._OptGroup_4.UseVisualStyleBackColor = False
        '
        '_OptGroup_3
        '
        Me._OptGroup_3.AutoSize = True
        Me._OptGroup_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_3, CType(3, Short))
        Me._OptGroup_3.Location = New System.Drawing.Point(386, 14)
        Me._OptGroup_3.Name = "_OptGroup_3"
        Me._OptGroup_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_3.Size = New System.Drawing.Size(79, 18)
        Me._OptGroup_3.TabIndex = 2
        Me._OptGroup_3.TabStop = True
        Me._OptGroup_3.Text = "Expenses"
        Me._OptGroup_3.UseVisualStyleBackColor = False
        '
        'ChkAllGroup
        '
        Me.ChkAllGroup.BackColor = System.Drawing.SystemColors.Control
        Me.ChkAllGroup.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkAllGroup.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkAllGroup.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkAllGroup.Location = New System.Drawing.Point(388, 37)
        Me.ChkAllGroup.Name = "ChkAllGroup"
        Me.ChkAllGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkAllGroup.Size = New System.Drawing.Size(52, 17)
        Me.ChkAllGroup.TabIndex = 8
        Me.ChkAllGroup.Text = "ALL"
        Me.ChkAllGroup.UseVisualStyleBackColor = False
        Me.ChkAllGroup.Visible = False
        '
        'TxtGroup
        '
        Me.TxtGroup.AcceptsReturn = True
        Me.TxtGroup.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGroup.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGroup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGroup.ForeColor = System.Drawing.Color.Blue
        Me.TxtGroup.Location = New System.Drawing.Point(6, 35)
        Me.TxtGroup.MaxLength = 0
        Me.TxtGroup.Name = "TxtGroup"
        Me.TxtGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGroup.Size = New System.Drawing.Size(379, 20)
        Me.TxtGroup.TabIndex = 7
        Me.TxtGroup.Visible = False
        '
        '_OptGroup_2
        '
        Me._OptGroup_2.AutoSize = True
        Me._OptGroup_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_2, CType(2, Short))
        Me._OptGroup_2.Location = New System.Drawing.Point(844, 14)
        Me._OptGroup_2.Name = "_OptGroup_2"
        Me._OptGroup_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_2.Size = New System.Drawing.Size(39, 18)
        Me._OptGroup_2.TabIndex = 5
        Me._OptGroup_2.TabStop = True
        Me._OptGroup_2.Text = "All"
        Me._OptGroup_2.UseVisualStyleBackColor = False
        '
        '_OptGroup_1
        '
        Me._OptGroup_1.AutoSize = True
        Me._OptGroup_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_1, CType(1, Short))
        Me._OptGroup_1.Location = New System.Drawing.Point(209, 14)
        Me._OptGroup_1.Name = "_OptGroup_1"
        Me._OptGroup_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_1.Size = New System.Drawing.Size(136, 18)
        Me._OptGroup_1.TabIndex = 1
        Me._OptGroup_1.TabStop = True
        Me._OptGroup_1.Text = "Detailed Group Wise"
        Me._OptGroup_1.UseVisualStyleBackColor = False
        '
        '_OptGroup_0
        '
        Me._OptGroup_0.AutoSize = True
        Me._OptGroup_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptGroup_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptGroup_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptGroup_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptGroup.SetIndex(Me._OptGroup_0, CType(0, Short))
        Me._OptGroup_0.Location = New System.Drawing.Point(4, 14)
        Me._OptGroup_0.Name = "_OptGroup_0"
        Me._OptGroup_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptGroup_0.Size = New System.Drawing.Size(164, 18)
        Me._OptGroup_0.TabIndex = 0
        Me._OptGroup_0.TabStop = True
        Me._OptGroup_0.Text = "Summerised Group Wise"
        Me._OptGroup_0.UseVisualStyleBackColor = False
        '
        'ViewReport
        '
        Me.ViewReport.AutoSize = True
        Me.ViewReport.BackColor = System.Drawing.SystemColors.Control
        Me.ViewReport.Cursor = System.Windows.Forms.Cursors.Default
        Me.ViewReport.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ViewReport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ViewReport.Location = New System.Drawing.Point(404, 10)
        Me.ViewReport.Name = "ViewReport"
        Me.ViewReport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ViewReport.Size = New System.Drawing.Size(0, 14)
        Me.ViewReport.TabIndex = 40
        Me.ViewReport.Visible = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(84, 138)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 50
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.CboCC)
        Me.Frame6.Controls.Add(Me.CboDept)
        Me.Frame6.Controls.Add(Me.lblCC)
        Me.Frame6.Controls.Add(Me.lblDept)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(536, 52)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(213, 57)
        Me.Frame6.TabIndex = 35
        Me.Frame6.TabStop = False
        Me.Frame6.Visible = False
        '
        'CboCC
        '
        Me.CboCC.BackColor = System.Drawing.SystemColors.Window
        Me.CboCC.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboCC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboCC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboCC.Location = New System.Drawing.Point(38, 12)
        Me.CboCC.Name = "CboCC"
        Me.CboCC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboCC.Size = New System.Drawing.Size(172, 22)
        Me.CboCC.TabIndex = 23
        '
        'CboDept
        '
        Me.CboDept.BackColor = System.Drawing.SystemColors.Window
        Me.CboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboDept.Location = New System.Drawing.Point(38, 34)
        Me.CboDept.Name = "CboDept"
        Me.CboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboDept.Size = New System.Drawing.Size(173, 22)
        Me.CboDept.TabIndex = 24
        '
        'lblCC
        '
        Me.lblCC.AutoSize = True
        Me.lblCC.BackColor = System.Drawing.SystemColors.Control
        Me.lblCC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCC.Location = New System.Drawing.Point(6, 16)
        Me.lblCC.Name = "lblCC"
        Me.lblCC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCC.Size = New System.Drawing.Size(29, 14)
        Me.lblCC.TabIndex = 36
        Me.lblCC.Text = "C.C."
        '
        'lblDept
        '
        Me.lblDept.AutoSize = True
        Me.lblDept.BackColor = System.Drawing.SystemColors.Control
        Me.lblDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDept.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDept.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDept.Location = New System.Drawing.Point(4, 38)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDept.Size = New System.Drawing.Size(35, 14)
        Me.lblDept.TabIndex = 37
        Me.lblDept.Text = "Dept."
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 171)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(898, 388)
        Me.SprdMain.TabIndex = 45
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdExit)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(624, 562)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(277, 49)
        Me.FraMovement.TabIndex = 41
        Me.FraMovement.TabStop = False
        '
        'OptGroup
        '
        '
        'txtDate
        '
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
        Me.lstCompanyName.Size = New System.Drawing.Size(328, 93)
        Me.lstCompanyName.TabIndex = 4
        '
        'frmViewTrailBalCal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraHideRow)
        Me.Controls.Add(Me.FraOption)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(5, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewTrailBalCal"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Trail Balance (Calander Wise)"
        Me.Frame7.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraHideRow.ResumeLayout(False)
        Me.FraHideRow.PerformLayout()
        Me.FraOption.ResumeLayout(False)
        Me.FraOption.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPrint, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtDate1, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents lstCompanyName As CheckedListBox
#End Region
End Class