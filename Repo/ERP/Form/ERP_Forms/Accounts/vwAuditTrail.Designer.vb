Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmVoucherAuditTrail
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
    Public WithEvents _optDate_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optDate_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents chkAllAccount As System.Windows.Forms.CheckBox
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents cmdOptional As System.Windows.Forms.Button
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
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
    Public WithEvents SprdLedg As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
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
    Public WithEvents chkGroup As Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray
    Public WithEvents optDate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVoucherAuditTrail))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdOptional = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me._optDate_1 = New System.Windows.Forms.RadioButton()
        Me._optDate_0 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.chkAllAccount = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
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
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdLedg = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.FraOthers = New System.Windows.Forms.GroupBox()
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
        Me.chkGroup = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.optDate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me._chkGroup_10 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_9 = New System.Windows.Forms.CheckBox()
        Me.Frame6.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraOption.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdLedg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.FraOthers.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Enabled = False
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(10, 14)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(260, 20)
        Me.TxtAccount.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Enabled = False
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(272, 14)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdOptional
        '
        Me.cmdOptional.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOptional.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOptional.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOptional.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOptional.Image = CType(resources.GetObject("cmdOptional.Image"), System.Drawing.Image)
        Me.cmdOptional.Location = New System.Drawing.Point(6, 20)
        Me.cmdOptional.Name = "cmdOptional"
        Me.cmdOptional.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOptional.Size = New System.Drawing.Size(53, 41)
        Me.cmdOptional.TabIndex = 39
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
        Me.CmdPreview.Location = New System.Drawing.Point(137, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 19
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
        Me.cmdPrint.Location = New System.Drawing.Point(71, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 18
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
        Me.cmdClose.Location = New System.Drawing.Point(204, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 20
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
        Me.cmdShow.Location = New System.Drawing.Point(4, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 17
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Controls.Add(Me._optDate_1)
        Me.Frame6.Controls.Add(Me._optDate_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(141, 79)
        Me.Frame6.TabIndex = 21
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(42, 30)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(78, 20)
        Me.txtDateFrom.TabIndex = 1
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(42, 52)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(78, 20)
        Me.txtDateTo.TabIndex = 0
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(4, 34)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(42, 14)
        Me._Lbl_1.TabIndex = 23
        Me._Lbl_1.Text = "From :"
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(14, 56)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(26, 14)
        Me._Lbl_0.TabIndex = 22
        Me._Lbl_0.Text = "To :"
        '
        '_optDate_1
        '
        Me._optDate_1.AutoSize = True
        Me._optDate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_1, CType(1, Short))
        Me._optDate_1.Location = New System.Drawing.Point(70, 12)
        Me._optDate_1.Name = "_optDate_1"
        Me._optDate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_1.Size = New System.Drawing.Size(59, 18)
        Me._optDate_1.TabIndex = 41
        Me._optDate_1.TabStop = True
        Me._optDate_1.Text = "Trans."
        Me._optDate_1.UseVisualStyleBackColor = False
        '
        '_optDate_0
        '
        Me._optDate_0.AutoSize = True
        Me._optDate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_0.Checked = True
        Me._optDate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_0, CType(0, Short))
        Me._optDate_0.Location = New System.Drawing.Point(2, 12)
        Me._optDate_0.Name = "_optDate_0"
        Me._optDate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_0.Size = New System.Drawing.Size(57, 18)
        Me._optDate_0.TabIndex = 40
        Me._optDate_0.TabStop = True
        Me._optDate_0.Text = "VDate"
        Me._optDate_0.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtVNo)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Controls.Add(Me.cboShow)
        Me.FraAccount.Controls.Add(Me.chkAllAccount)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(208, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(381, 79)
        Me.FraAccount.TabIndex = 32
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Account"
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.txtVNo.Location = New System.Drawing.Point(46, 39)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(101, 20)
        Me.txtVNo.TabIndex = 78
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(7, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(37, 14)
        Me.Label2.TabIndex = 79
        Me.Label2.Text = "VNo. :"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(198, 38)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(149, 22)
        Me.cboShow.TabIndex = 43
        '
        'chkAllAccount
        '
        Me.chkAllAccount.AutoSize = True
        Me.chkAllAccount.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllAccount.Checked = True
        Me.chkAllAccount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllAccount.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllAccount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllAccount.Location = New System.Drawing.Point(306, 18)
        Me.chkAllAccount.Name = "chkAllAccount"
        Me.chkAllAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllAccount.Size = New System.Drawing.Size(48, 18)
        Me.chkAllAccount.TabIndex = 37
        Me.chkAllAccount.Text = "ALL"
        Me.chkAllAccount.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(152, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Show :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cmdOptional)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(142, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(63, 79)
        Me.Frame1.TabIndex = 38
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Optional "
        '
        'FraOption
        '
        Me.FraOption.BackColor = System.Drawing.SystemColors.Control
        Me.FraOption.Controls.Add(Me._chkGroup_10)
        Me.FraOption.Controls.Add(Me._chkGroup_9)
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
        Me.FraOption.Location = New System.Drawing.Point(592, 0)
        Me.FraOption.Name = "FraOption"
        Me.FraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOption.Size = New System.Drawing.Size(306, 79)
        Me.FraOption.TabIndex = 24
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
        Me._chkGroup_8.Location = New System.Drawing.Point(158, 12)
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
        Me._chkGroup_5.Location = New System.Drawing.Point(66, 56)
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
        Me._chkGroup_2.Location = New System.Drawing.Point(6, 56)
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
        Me._chkGroup_4.Location = New System.Drawing.Point(66, 34)
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
        Me._chkGroup_1.Location = New System.Drawing.Point(6, 34)
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
        Me._chkGroup_6.Location = New System.Drawing.Point(158, 34)
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
        Me._chkGroup_3.Location = New System.Drawing.Point(66, 12)
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
        Me._chkGroup_0.Location = New System.Drawing.Point(6, 12)
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
        Me._chkGroup_7.Location = New System.Drawing.Point(158, 56)
        Me._chkGroup_7.Name = "_chkGroup_7"
        Me._chkGroup_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_7.Size = New System.Drawing.Size(63, 18)
        Me._chkGroup_7.TabIndex = 12
        Me._chkGroup_7.Text = "Contra"
        Me._chkGroup_7.UseVisualStyleBackColor = False
        Me._chkGroup_7.Visible = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdLedg)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 74)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(898, 490)
        Me.Frame4.TabIndex = 25
        Me.Frame4.TabStop = False
        '
        'SprdLedg
        '
        Me.SprdLedg.DataSource = Nothing
        Me.SprdLedg.Location = New System.Drawing.Point(2, 8)
        Me.SprdLedg.Name = "SprdLedg"
        Me.SprdLedg.OcxState = CType(resources.GetObject("SprdLedg.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdLedg.Size = New System.Drawing.Size(894, 476)
        Me.SprdLedg.TabIndex = 16
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 17
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
        Me.FraMovement.Location = New System.Drawing.Point(624, 561)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(275, 49)
        Me.FraMovement.TabIndex = 26
        Me.FraMovement.TabStop = False
        '
        'FraOthers
        '
        Me.FraOthers.BackColor = System.Drawing.SystemColors.Control
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
        Me.FraOthers.Location = New System.Drawing.Point(206, 0)
        Me.FraOthers.Name = "FraOthers"
        Me.FraOthers.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOthers.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOthers.Size = New System.Drawing.Size(383, 79)
        Me.FraOthers.TabIndex = 28
        Me.FraOthers.TabStop = False
        Me.FraOthers.Visible = False
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
        Me.cboExpHead.TabIndex = 35
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
        Me.CboDept.TabIndex = 14
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
        Me.CboCC.TabIndex = 13
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
        Me.cboEmp.TabIndex = 15
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
        Me._Lbl_6.TabIndex = 36
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
        Me._Lbl_3.TabIndex = 31
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
        Me._Lbl_2.TabIndex = 30
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
        Me._Lbl_4.TabIndex = 29
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
        Me.lblPrintCount.TabIndex = 34
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
        Me.lblBookType.Location = New System.Drawing.Point(406, 440)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(67, 17)
        Me.lblBookType.TabIndex = 33
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
        Me.lblAcCode.TabIndex = 27
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'chkGroup
        '
        '
        '_chkGroup_10
        '
        Me._chkGroup_10.AutoSize = True
        Me._chkGroup_10.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_10.Checked = True
        Me._chkGroup_10.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me._chkGroup_10.Location = New System.Drawing.Point(224, 34)
        Me.chkGroup.SetIndex(Me._chkGroup_10, CType(10, Short))
        Me._chkGroup_10.Name = "_chkGroup_10"
        Me._chkGroup_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_10.Size = New System.Drawing.Size(86, 18)
        Me._chkGroup_10.TabIndex = 16
        Me._chkGroup_10.Text = "Sale Credit"
        Me._chkGroup_10.UseVisualStyleBackColor = False
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
        Me._chkGroup_9.Location = New System.Drawing.Point(225, 12)
        Me.chkGroup.SetIndex(Me._chkGroup_9, CType(9, Short))
        Me._chkGroup_9.Name = "_chkGroup_9"
        Me._chkGroup_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_9.Size = New System.Drawing.Size(80, 18)
        Me._chkGroup_9.TabIndex = 15
        Me._chkGroup_9.Text = "Sale Debit"
        Me._chkGroup_9.UseVisualStyleBackColor = False
        '
        'frmVoucherAuditTrail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraOption)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
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
        Me.Name = "frmVoucherAuditTrail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Audit Trail"
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.FraOption.ResumeLayout(False)
        Me.FraOption.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdLedg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraOthers.ResumeLayout(False)
        Me.FraOthers.PerformLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents txtVNo As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents _chkGroup_10 As CheckBox
    Public WithEvents _chkGroup_9 As CheckBox
#End Region
End Class