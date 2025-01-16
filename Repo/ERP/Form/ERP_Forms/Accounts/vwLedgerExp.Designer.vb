Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewLedgerExp
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
    Public WithEvents _chkGroup_8 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_5 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_4 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_6 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_3 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_0 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_7 As System.Windows.Forms.CheckBox
    Public WithEvents FraOption As System.Windows.Forms.GroupBox
    Public WithEvents _OptSumDet_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_2 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents _Lbl_7 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents chkAllAccount As System.Windows.Forms.CheckBox
    Public WithEvents cmdAgtsearch As System.Windows.Forms.Button
    Public WithEvents TxtAgtAccount As System.Windows.Forms.TextBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOptional As System.Windows.Forms.Button
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_5 As System.Windows.Forms.Label
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdLedg As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents ChkWithRunBal As System.Windows.Forms.CheckBox
    Public WithEvents cboCond As System.Windows.Forms.ComboBox
    Public WithEvents txtCondAmount As System.Windows.Forms.TextBox
    Public WithEvents FraAmountCond As System.Windows.Forms.GroupBox
    Public WithEvents chkOption As System.Windows.Forms.CheckBox
    Public WithEvents FraConditional As System.Windows.Forms.GroupBox
    Public WithEvents cboExpHead As System.Windows.Forms.ComboBox
    Public WithEvents CboDept As System.Windows.Forms.ComboBox
    Public WithEvents CboCC As System.Windows.Forms.ComboBox
    Public WithEvents cboEmp As System.Windows.Forms.ComboBox
    Public WithEvents _Lbl_6 As System.Windows.Forms.Label
    Public WithEvents _Lbl_3 As System.Windows.Forms.Label
    Public WithEvents _Lbl_2 As System.Windows.Forms.Label
    Public WithEvents _Lbl_4 As System.Windows.Forms.Label
    Public WithEvents FraOthers As System.Windows.Forms.GroupBox
    Public WithEvents lblPrintCount As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblAcCode As System.Windows.Forms.Label
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptSumDet As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents chkGroup As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewLedgerExp))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdAgtsearch = New System.Windows.Forms.Button()
        Me.TxtAgtAccount = New System.Windows.Forms.TextBox()
        Me.cmdOptional = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.ChkWithRunBal = New System.Windows.Forms.CheckBox()
        Me.FraOption = New System.Windows.Forms.GroupBox()
        Me._chkGroup_8 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_5 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_2 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_4 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_1 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_6 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_3 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_0 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_7 = New System.Windows.Forms.CheckBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptSumDet_0 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_1 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_2 = New System.Windows.Forms.RadioButton()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me._Lbl_7 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkAllAccount = New System.Windows.Forms.CheckBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_5 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdLedg = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.FraOthers = New System.Windows.Forms.GroupBox()
        Me.FraConditional = New System.Windows.Forms.GroupBox()
        Me.FraAmountCond = New System.Windows.Forms.GroupBox()
        Me.cboCond = New System.Windows.Forms.ComboBox()
        Me.txtCondAmount = New System.Windows.Forms.TextBox()
        Me.chkOption = New System.Windows.Forms.CheckBox()
        Me.cboExpHead = New System.Windows.Forms.ComboBox()
        Me.CboDept = New System.Windows.Forms.ComboBox()
        Me.CboCC = New System.Windows.Forms.ComboBox()
        Me.cboEmp = New System.Windows.Forms.ComboBox()
        Me._Lbl_6 = New System.Windows.Forms.Label()
        Me._Lbl_3 = New System.Windows.Forms.Label()
        Me._Lbl_2 = New System.Windows.Forms.Label()
        Me._Lbl_4 = New System.Windows.Forms.Label()
        Me.lblPrintCount = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptSumDet = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.chkGroup = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.FraOption.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdLedg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.FraOthers.SuspendLayout()
        Me.FraConditional.SuspendLayout()
        Me.FraAmountCond.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(4, 24)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(343, 20)
        Me.TxtAccount.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(348, 23)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 22)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdAgtsearch
        '
        Me.cmdAgtsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAgtsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAgtsearch.Enabled = False
        Me.cmdAgtsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAgtsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAgtsearch.Image = CType(resources.GetObject("cmdAgtsearch.Image"), System.Drawing.Image)
        Me.cmdAgtsearch.Location = New System.Drawing.Point(293, 20)
        Me.cmdAgtsearch.Name = "cmdAgtsearch"
        Me.cmdAgtsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAgtsearch.Size = New System.Drawing.Size(29, 22)
        Me.cmdAgtsearch.TabIndex = 13
        Me.cmdAgtsearch.TabStop = False
        Me.cmdAgtsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAgtsearch, "Search")
        Me.cmdAgtsearch.UseVisualStyleBackColor = False
        '
        'TxtAgtAccount
        '
        Me.TxtAgtAccount.AcceptsReturn = True
        Me.TxtAgtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAgtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAgtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAgtAccount.Enabled = False
        Me.TxtAgtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAgtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAgtAccount.Location = New System.Drawing.Point(4, 20)
        Me.TxtAgtAccount.MaxLength = 0
        Me.TxtAgtAccount.Name = "TxtAgtAccount"
        Me.TxtAgtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAgtAccount.Size = New System.Drawing.Size(288, 20)
        Me.TxtAgtAccount.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.TxtAgtAccount, "Press F1 For Help")
        '
        'cmdOptional
        '
        Me.cmdOptional.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOptional.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOptional.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOptional.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOptional.Image = CType(resources.GetObject("cmdOptional.Image"), System.Drawing.Image)
        Me.cmdOptional.Location = New System.Drawing.Point(66, 81)
        Me.cmdOptional.Name = "cmdOptional"
        Me.cmdOptional.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOptional.Size = New System.Drawing.Size(53, 21)
        Me.cmdOptional.TabIndex = 44
        Me.cmdOptional.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdOptional, "Show Record")
        Me.cmdOptional.UseVisualStyleBackColor = False
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
        Me.CmdPreview.Location = New System.Drawing.Point(137, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 25
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
        Me.cmdPrint.Location = New System.Drawing.Point(71, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 24
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(204, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 26
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
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(4, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 23
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'ChkWithRunBal
        '
        Me.ChkWithRunBal.BackColor = System.Drawing.SystemColors.Control
        Me.ChkWithRunBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkWithRunBal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkWithRunBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkWithRunBal.Location = New System.Drawing.Point(0, 64)
        Me.ChkWithRunBal.Name = "ChkWithRunBal"
        Me.ChkWithRunBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkWithRunBal.Size = New System.Drawing.Size(119, 13)
        Me.ChkWithRunBal.TabIndex = 42
        Me.ChkWithRunBal.Text = "Running Balance"
        Me.ToolTip1.SetToolTip(Me.ChkWithRunBal, "Selecting this option may cause slow reporting")
        Me.ChkWithRunBal.UseVisualStyleBackColor = False
        Me.ChkWithRunBal.Visible = False
        '
        'FraOption
        '
        Me.FraOption.BackColor = System.Drawing.SystemColors.Control
        Me.FraOption.Controls.Add(Me._chkGroup_8)
        Me.FraOption.Controls.Add(Me._chkGroup_5)
        Me.FraOption.Controls.Add(Me._chkGroup_2)
        Me.FraOption.Controls.Add(Me._chkGroup_4)
        Me.FraOption.Controls.Add(Me._chkGroup_1)
        Me.FraOption.Controls.Add(Me._chkGroup_6)
        Me.FraOption.Controls.Add(Me._chkGroup_3)
        Me.FraOption.Controls.Add(Me._chkGroup_0)
        Me.FraOption.Controls.Add(Me._chkGroup_7)
        Me.FraOption.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOption.Location = New System.Drawing.Point(508, 0)
        Me.FraOption.Name = "FraOption"
        Me.FraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOption.Size = New System.Drawing.Size(268, 78)
        Me.FraOption.TabIndex = 30
        Me.FraOption.TabStop = False
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
        Me._chkGroup_8.Location = New System.Drawing.Point(185, 13)
        Me._chkGroup_8.Name = "_chkGroup_8"
        Me._chkGroup_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_8.Size = New System.Drawing.Size(48, 18)
        Me._chkGroup_8.TabIndex = 7
        Me._chkGroup_8.Text = "PDC"
        Me._chkGroup_8.UseVisualStyleBackColor = False
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
        Me._chkGroup_5.Location = New System.Drawing.Point(76, 53)
        Me._chkGroup_5.Name = "_chkGroup_5"
        Me._chkGroup_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_5.Size = New System.Drawing.Size(88, 18)
        Me._chkGroup_5.TabIndex = 10
        Me._chkGroup_5.Text = "Credit Note"
        Me._chkGroup_5.UseVisualStyleBackColor = False
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
        Me._chkGroup_2.Location = New System.Drawing.Point(6, 53)
        Me._chkGroup_2.Name = "_chkGroup_2"
        Me._chkGroup_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_2.Size = New System.Drawing.Size(49, 18)
        Me._chkGroup_2.TabIndex = 6
        Me._chkGroup_2.Text = "Sale"
        Me._chkGroup_2.UseVisualStyleBackColor = False
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
        Me._chkGroup_4.Location = New System.Drawing.Point(76, 33)
        Me._chkGroup_4.Name = "_chkGroup_4"
        Me._chkGroup_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_4.Size = New System.Drawing.Size(82, 18)
        Me._chkGroup_4.TabIndex = 9
        Me._chkGroup_4.Text = "Debit Note"
        Me._chkGroup_4.UseVisualStyleBackColor = False
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
        Me._chkGroup_1.Location = New System.Drawing.Point(6, 33)
        Me._chkGroup_1.Name = "_chkGroup_1"
        Me._chkGroup_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_1.Size = New System.Drawing.Size(54, 18)
        Me._chkGroup_1.TabIndex = 5
        Me._chkGroup_1.Text = "Cash"
        Me._chkGroup_1.UseVisualStyleBackColor = False
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
        Me._chkGroup_6.Location = New System.Drawing.Point(185, 33)
        Me._chkGroup_6.Name = "_chkGroup_6"
        Me._chkGroup_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_6.Size = New System.Drawing.Size(67, 18)
        Me._chkGroup_6.TabIndex = 11
        Me._chkGroup_6.Text = "Journal"
        Me._chkGroup_6.UseVisualStyleBackColor = False
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
        Me._chkGroup_3.Location = New System.Drawing.Point(76, 13)
        Me._chkGroup_3.Name = "_chkGroup_3"
        Me._chkGroup_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_3.Size = New System.Drawing.Size(78, 18)
        Me._chkGroup_3.TabIndex = 8
        Me._chkGroup_3.Text = "Purchase"
        Me._chkGroup_3.UseVisualStyleBackColor = False
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
        Me._chkGroup_0.Location = New System.Drawing.Point(6, 13)
        Me._chkGroup_0.Name = "_chkGroup_0"
        Me._chkGroup_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_0.Size = New System.Drawing.Size(53, 18)
        Me._chkGroup_0.TabIndex = 4
        Me._chkGroup_0.Text = "Bank"
        Me._chkGroup_0.UseVisualStyleBackColor = False
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
        Me._chkGroup_7.Location = New System.Drawing.Point(185, 53)
        Me._chkGroup_7.Name = "_chkGroup_7"
        Me._chkGroup_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_7.Size = New System.Drawing.Size(63, 18)
        Me._chkGroup_7.TabIndex = 15
        Me._chkGroup_7.Text = "Contra"
        Me._chkGroup_7.UseVisualStyleBackColor = False
        Me._chkGroup_7.Visible = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptSumDet_0)
        Me.Frame2.Controls.Add(Me._OptSumDet_1)
        Me.Frame2.Controls.Add(Me._OptSumDet_2)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(779, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(119, 78)
        Me.Frame2.TabIndex = 34
        Me.Frame2.TabStop = False
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
        Me._OptSumDet_0.Location = New System.Drawing.Point(6, 13)
        Me._OptSumDet_0.Name = "_OptSumDet_0"
        Me._OptSumDet_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_0.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_0.TabIndex = 19
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
        Me._OptSumDet_1.Location = New System.Drawing.Point(6, 33)
        Me._OptSumDet_1.Name = "_OptSumDet_1"
        Me._OptSumDet_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_1.Size = New System.Drawing.Size(50, 18)
        Me._OptSumDet_1.TabIndex = 20
        Me._OptSumDet_1.TabStop = True
        Me._OptSumDet_1.Text = "Daily"
        Me._OptSumDet_1.UseVisualStyleBackColor = False
        '
        '_OptSumDet_2
        '
        Me._OptSumDet_2.AutoSize = True
        Me._OptSumDet_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_2, CType(2, Short))
        Me._OptSumDet_2.Location = New System.Drawing.Point(6, 53)
        Me._OptSumDet_2.Name = "_OptSumDet_2"
        Me._OptSumDet_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_2.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_2.TabIndex = 21
        Me._OptSumDet_2.TabStop = True
        Me._OptSumDet_2.Text = "Monthly"
        Me._OptSumDet_2.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboDivision)
        Me.Frame7.Controls.Add(Me._Lbl_7)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(508, 72)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(390, 42)
        Me.Frame7.TabIndex = 53
        Me.Frame7.TabStop = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(66, 12)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(320, 22)
        Me.cboDivision.TabIndex = 54
        '
        '_Lbl_7
        '
        Me._Lbl_7.AutoSize = True
        Me._Lbl_7.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_7, CType(7, Short))
        Me._Lbl_7.Location = New System.Drawing.Point(8, 14)
        Me._Lbl_7.Name = "_Lbl_7"
        Me._Lbl_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_7.Size = New System.Drawing.Size(56, 14)
        Me._Lbl_7.TabIndex = 55
        Me._Lbl_7.Text = "Division :"
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(124, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(383, 58)
        Me.FraAccount.TabIndex = 39
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Account"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkAllAccount)
        Me.Frame3.Controls.Add(Me.cmdAgtsearch)
        Me.Frame3.Controls.Add(Me.TxtAgtAccount)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(124, 57)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(383, 57)
        Me.Frame3.TabIndex = 43
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Against Account Name"
        '
        'chkAllAccount
        '
        Me.chkAllAccount.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllAccount.Checked = True
        Me.chkAllAccount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllAccount.Location = New System.Drawing.Point(325, 22)
        Me.chkAllAccount.Name = "chkAllAccount"
        Me.chkAllAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllAccount.Size = New System.Drawing.Size(51, 18)
        Me.chkAllAccount.TabIndex = 14
        Me.chkAllAccount.Text = "ALL"
        Me.chkAllAccount.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cmdOptional)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me._Lbl_5)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(123, 114)
        Me.Frame6.TabIndex = 27
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(42, 52)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(78, 20)
        Me.txtDateTo.TabIndex = 1
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(42, 21)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(78, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        '_Lbl_5
        '
        Me._Lbl_5.AutoSize = True
        Me._Lbl_5.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_5, CType(5, Short))
        Me._Lbl_5.Location = New System.Drawing.Point(6, 83)
        Me._Lbl_5.Name = "_Lbl_5"
        Me._Lbl_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_5.Size = New System.Drawing.Size(58, 14)
        Me._Lbl_5.TabIndex = 45
        Me._Lbl_5.Text = "Optional :"
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(16, 54)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(26, 14)
        Me._Lbl_1.TabIndex = 29
        Me._Lbl_1.Text = "To :"
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 22)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(42, 14)
        Me._Lbl_0.TabIndex = 28
        Me._Lbl_0.Text = "From :"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdLedg)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 109)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(898, 453)
        Me.Frame4.TabIndex = 31
        Me.Frame4.TabStop = False
        '
        'SprdLedg
        '
        Me.SprdLedg.DataSource = Nothing
        Me.SprdLedg.Location = New System.Drawing.Point(0, 8)
        Me.SprdLedg.Name = "SprdLedg"
        Me.SprdLedg.OcxState = CType(resources.GetObject("SprdLedg.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdLedg.Size = New System.Drawing.Size(894, 440)
        Me.SprdLedg.TabIndex = 22
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 23
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
        Me.FraMovement.Location = New System.Drawing.Point(626, 562)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(275, 49)
        Me.FraMovement.TabIndex = 32
        Me.FraMovement.TabStop = False
        '
        'FraOthers
        '
        Me.FraOthers.BackColor = System.Drawing.SystemColors.Control
        Me.FraOthers.Controls.Add(Me.FraConditional)
        Me.FraOthers.Controls.Add(Me.cboExpHead)
        Me.FraOthers.Controls.Add(Me.CboDept)
        Me.FraOthers.Controls.Add(Me.CboCC)
        Me.FraOthers.Controls.Add(Me.cboEmp)
        Me.FraOthers.Controls.Add(Me._Lbl_6)
        Me.FraOthers.Controls.Add(Me._Lbl_3)
        Me.FraOthers.Controls.Add(Me._Lbl_2)
        Me.FraOthers.Controls.Add(Me._Lbl_4)
        Me.FraOthers.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOthers.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOthers.Location = New System.Drawing.Point(124, 0)
        Me.FraOthers.Name = "FraOthers"
        Me.FraOthers.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOthers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOthers.Size = New System.Drawing.Size(541, 79)
        Me.FraOthers.TabIndex = 35
        Me.FraOthers.TabStop = False
        Me.FraOthers.Visible = False
        '
        'FraConditional
        '
        Me.FraConditional.BackColor = System.Drawing.SystemColors.Control
        Me.FraConditional.Controls.Add(Me.FraAmountCond)
        Me.FraConditional.Controls.Add(Me.chkOption)
        Me.FraConditional.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraConditional.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraConditional.Location = New System.Drawing.Point(382, 0)
        Me.FraConditional.Name = "FraConditional"
        Me.FraConditional.Padding = New System.Windows.Forms.Padding(0)
        Me.FraConditional.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraConditional.Size = New System.Drawing.Size(159, 79)
        Me.FraConditional.TabIndex = 48
        Me.FraConditional.TabStop = False
        '
        'FraAmountCond
        '
        Me.FraAmountCond.BackColor = System.Drawing.SystemColors.Control
        Me.FraAmountCond.Controls.Add(Me.cboCond)
        Me.FraAmountCond.Controls.Add(Me.txtCondAmount)
        Me.FraAmountCond.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAmountCond.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAmountCond.Location = New System.Drawing.Point(0, 30)
        Me.FraAmountCond.Name = "FraAmountCond"
        Me.FraAmountCond.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAmountCond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAmountCond.Size = New System.Drawing.Size(159, 49)
        Me.FraAmountCond.TabIndex = 50
        Me.FraAmountCond.TabStop = False
        Me.FraAmountCond.Text = "Amount is"
        Me.FraAmountCond.Visible = False
        '
        'cboCond
        '
        Me.cboCond.BackColor = System.Drawing.SystemColors.Window
        Me.cboCond.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCond.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCond.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCond.Location = New System.Drawing.Point(8, 18)
        Me.cboCond.Name = "cboCond"
        Me.cboCond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCond.Size = New System.Drawing.Size(59, 22)
        Me.cboCond.TabIndex = 52
        '
        'txtCondAmount
        '
        Me.txtCondAmount.AcceptsReturn = True
        Me.txtCondAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtCondAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCondAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCondAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCondAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCondAmount.Location = New System.Drawing.Point(68, 18)
        Me.txtCondAmount.MaxLength = 0
        Me.txtCondAmount.Name = "txtCondAmount"
        Me.txtCondAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCondAmount.Size = New System.Drawing.Size(87, 20)
        Me.txtCondAmount.TabIndex = 51
        '
        'chkOption
        '
        Me.chkOption.BackColor = System.Drawing.SystemColors.Control
        Me.chkOption.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOption.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOption.Location = New System.Drawing.Point(6, 12)
        Me.chkOption.Name = "chkOption"
        Me.chkOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOption.Size = New System.Drawing.Size(127, 13)
        Me.chkOption.TabIndex = 49
        Me.chkOption.Text = "Conditional Check"
        Me.chkOption.UseVisualStyleBackColor = False
        '
        'cboExpHead
        '
        Me.cboExpHead.BackColor = System.Drawing.SystemColors.Window
        Me.cboExpHead.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboExpHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExpHead.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboExpHead.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboExpHead.Location = New System.Drawing.Point(228, 46)
        Me.cboExpHead.Name = "cboExpHead"
        Me.cboExpHead.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboExpHead.Size = New System.Drawing.Size(150, 22)
        Me.cboExpHead.TabIndex = 46
        '
        'CboDept
        '
        Me.CboDept.BackColor = System.Drawing.SystemColors.Window
        Me.CboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboDept.Location = New System.Drawing.Point(38, 46)
        Me.CboDept.Name = "CboDept"
        Me.CboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboDept.Size = New System.Drawing.Size(150, 22)
        Me.CboDept.TabIndex = 17
        '
        'CboCC
        '
        Me.CboCC.BackColor = System.Drawing.SystemColors.Window
        Me.CboCC.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboCC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboCC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboCC.Location = New System.Drawing.Point(38, 16)
        Me.CboCC.Name = "CboCC"
        Me.CboCC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboCC.Size = New System.Drawing.Size(150, 22)
        Me.CboCC.TabIndex = 16
        '
        'cboEmp
        '
        Me.cboEmp.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboEmp.Location = New System.Drawing.Point(228, 16)
        Me.cboEmp.Name = "cboEmp"
        Me.cboEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmp.Size = New System.Drawing.Size(150, 22)
        Me.cboEmp.TabIndex = 18
        '
        '_Lbl_6
        '
        Me._Lbl_6.AutoSize = True
        Me._Lbl_6.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_6, CType(6, Short))
        Me._Lbl_6.Location = New System.Drawing.Point(194, 49)
        Me._Lbl_6.Name = "_Lbl_6"
        Me._Lbl_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_6.Size = New System.Drawing.Size(29, 14)
        Me._Lbl_6.TabIndex = 47
        Me._Lbl_6.Text = "Exp."
        '
        '_Lbl_3
        '
        Me._Lbl_3.AutoSize = True
        Me._Lbl_3.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_3, CType(3, Short))
        Me._Lbl_3.Location = New System.Drawing.Point(4, 49)
        Me._Lbl_3.Name = "_Lbl_3"
        Me._Lbl_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_3.Size = New System.Drawing.Size(32, 14)
        Me._Lbl_3.TabIndex = 38
        Me._Lbl_3.Text = "Dept"
        '
        '_Lbl_2
        '
        Me._Lbl_2.AutoSize = True
        Me._Lbl_2.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_2, CType(2, Short))
        Me._Lbl_2.Location = New System.Drawing.Point(4, 20)
        Me._Lbl_2.Name = "_Lbl_2"
        Me._Lbl_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_2.Size = New System.Drawing.Size(29, 14)
        Me._Lbl_2.TabIndex = 37
        Me._Lbl_2.Text = "C.C."
        '
        '_Lbl_4
        '
        Me._Lbl_4.AutoSize = True
        Me._Lbl_4.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_4, CType(4, Short))
        Me._Lbl_4.Location = New System.Drawing.Point(194, 20)
        Me._Lbl_4.Name = "_Lbl_4"
        Me._Lbl_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_4.Size = New System.Drawing.Size(31, 14)
        Me._Lbl_4.TabIndex = 36
        Me._Lbl_4.Text = "Emp"
        '
        'lblPrintCount
        '
        Me.lblPrintCount.BackColor = System.Drawing.SystemColors.Control
        Me.lblPrintCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPrintCount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPrintCount.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrintCount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrintCount.Location = New System.Drawing.Point(0, 568)
        Me.lblPrintCount.Name = "lblPrintCount"
        Me.lblPrintCount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrintCount.Size = New System.Drawing.Size(403, 43)
        Me.lblPrintCount.TabIndex = 41
        Me.lblPrintCount.Text = "lblPrintCount"
        Me.lblPrintCount.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblPrintCount.Visible = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(452, 578)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(67, 17)
        Me.lblBookType.TabIndex = 40
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(4, 360)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(55, 11)
        Me.lblAcCode.TabIndex = 33
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'OptSumDet
        '
        '
        'chkGroup
        '
        '
        'frmViewLedgerExp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.FraOption)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.ChkWithRunBal)
        Me.Controls.Add(Me.FraOthers)
        Me.Controls.Add(Me.lblPrintCount)
        Me.Controls.Add(Me.lblBookType)
        Me.Controls.Add(Me.lblAcCode)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewLedgerExp"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "View Ledger (Expense)"
        Me.FraOption.ResumeLayout(False)
        Me.FraOption.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdLedg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraOthers.ResumeLayout(False)
        Me.FraOthers.PerformLayout()
        Me.FraConditional.ResumeLayout(False)
        Me.FraAmountCond.ResumeLayout(False)
        Me.FraAmountCond.PerformLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdLedg.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdLedg.DataSource = Nothing
    End Sub
#End Region
End Class