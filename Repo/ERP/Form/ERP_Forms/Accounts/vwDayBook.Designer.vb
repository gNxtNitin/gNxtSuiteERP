Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmViewDayBook
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
        'VB6_AddADODataBinding()
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            ''VB6_RemoveADODataBinding()
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents chkBankDetail As System.Windows.Forms.CheckBox
    Public WithEvents _OptSumDet_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_0 As System.Windows.Forms.RadioButton
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdReceipt As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdPayment As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblClosing As System.Windows.Forms.Label
    Public WithEvents lblOpening As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _chkGroup_8 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_5 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_7 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_4 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_6 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_3 As System.Windows.Forms.CheckBox
    Public WithEvents _chkGroup_0 As System.Windows.Forms.CheckBox
    Public WithEvents FraOption As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdExport As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents CboDept As System.Windows.Forms.ComboBox
    Public WithEvents CboCC As System.Windows.Forms.ComboBox
    Public WithEvents cboEmp As System.Windows.Forms.ComboBox
    Public WithEvents _Lbl_3 As System.Windows.Forms.Label
    Public WithEvents _Lbl_2 As System.Windows.Forms.Label
    Public WithEvents _Lbl_4 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmViewDayBook))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdExport = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkBankDetail = New System.Windows.Forms.CheckBox()
        Me._OptSumDet_2 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_1 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_0 = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdReceipt = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdPayment = New AxFPSpreadADO.AxfpSpread()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblClosing = New System.Windows.Forms.Label()
        Me.lblOpening = New System.Windows.Forms.Label()
        Me.FraOption = New System.Windows.Forms.GroupBox()
        Me._chkGroup_8 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_5 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_2 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_7 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_4 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_1 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_6 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_3 = New System.Windows.Forms.CheckBox()
        Me._chkGroup_0 = New System.Windows.Forms.CheckBox()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.CboDept = New System.Windows.Forms.ComboBox()
        Me.CboCC = New System.Windows.Forms.ComboBox()
        Me.cboEmp = New System.Windows.Forms.ComboBox()
        Me._Lbl_3 = New System.Windows.Forms.Label()
        Me._Lbl_2 = New System.Windows.Forms.Label()
        Me._Lbl_4 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptSumDet = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.chkGroup = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.FraAccount.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraOption.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
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
        Me.TxtAccount.Location = New System.Drawing.Point(54, 12)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(355, 20)
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
        Me.cmdsearch.Location = New System.Drawing.Point(410, 12)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(268, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 20
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdExport
        '
        Me.cmdExport.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExport.Enabled = False
        Me.cmdExport.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExport.Image = CType(resources.GetObject("cmdExport.Image"), System.Drawing.Image)
        Me.cmdExport.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdExport.Location = New System.Drawing.Point(202, 11)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExport.Size = New System.Drawing.Size(67, 37)
        Me.cmdExport.TabIndex = 45
        Me.cmdExport.Text = "Export Excel"
        Me.cmdExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExport, "Export Excel")
        Me.cmdExport.UseVisualStyleBackColor = False
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
        Me.CmdPreview.Location = New System.Drawing.Point(135, 11)
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(69, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 18
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
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(2, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 17
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkBankDetail)
        Me.FraAccount.Controls.Add(Me._OptSumDet_2)
        Me.FraAccount.Controls.Add(Me._OptSumDet_1)
        Me.FraAccount.Controls.Add(Me._OptSumDet_0)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(139, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(447, 68)
        Me.FraAccount.TabIndex = 32
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Account"
        '
        'chkBankDetail
        '
        Me.chkBankDetail.AutoSize = True
        Me.chkBankDetail.BackColor = System.Drawing.SystemColors.Control
        Me.chkBankDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBankDetail.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBankDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBankDetail.Location = New System.Drawing.Point(312, 40)
        Me.chkBankDetail.Name = "chkBankDetail"
        Me.chkBankDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBankDetail.Size = New System.Drawing.Size(120, 18)
        Me.chkBankDetail.TabIndex = 44
        Me.chkBankDetail.Text = "Show Bank Detail"
        Me.chkBankDetail.UseVisualStyleBackColor = False
        '
        '_OptSumDet_2
        '
        Me._OptSumDet_2.AutoSize = True
        Me._OptSumDet_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_2.Enabled = False
        Me._OptSumDet_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_2, CType(2, Short))
        Me._OptSumDet_2.Location = New System.Drawing.Point(224, 40)
        Me._OptSumDet_2.Name = "_OptSumDet_2"
        Me._OptSumDet_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_2.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_2.TabIndex = 38
        Me._OptSumDet_2.TabStop = True
        Me._OptSumDet_2.Text = "Monthly"
        Me._OptSumDet_2.UseVisualStyleBackColor = False
        '
        '_OptSumDet_1
        '
        Me._OptSumDet_1.AutoSize = True
        Me._OptSumDet_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_1.Enabled = False
        Me._OptSumDet_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_1, CType(1, Short))
        Me._OptSumDet_1.Location = New System.Drawing.Point(144, 40)
        Me._OptSumDet_1.Name = "_OptSumDet_1"
        Me._OptSumDet_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_1.Size = New System.Drawing.Size(50, 18)
        Me._OptSumDet_1.TabIndex = 37
        Me._OptSumDet_1.TabStop = True
        Me._OptSumDet_1.Text = "Daily"
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
        Me._OptSumDet_0.Location = New System.Drawing.Point(52, 40)
        Me._OptSumDet_0.Name = "_OptSumDet_0"
        Me._OptSumDet_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_0.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_0.TabIndex = 36
        Me._OptSumDet_0.TabStop = True
        Me._OptSumDet_0.Text = "Detailed"
        Me._OptSumDet_0.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(135, 68)
        Me.Frame6.TabIndex = 21
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(50, 41)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(77, 20)
        Me.txtDateTo.TabIndex = 1
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(50, 16)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(77, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(22, 43)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(26, 14)
        Me._Lbl_1.TabIndex = 23
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
        Me._Lbl_0.Location = New System.Drawing.Point(6, 17)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(42, 14)
        Me._Lbl_0.TabIndex = 22
        Me._Lbl_0.Text = "From :"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdReceipt)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me.SprdPayment)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Controls.Add(Me.Label3)
        Me.Frame4.Controls.Add(Me.lblClosing)
        Me.Frame4.Controls.Add(Me.lblOpening)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 63)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1106, 509)
        Me.Frame4.TabIndex = 25
        Me.Frame4.TabStop = False
        '
        'SprdReceipt
        '
        Me.SprdReceipt.DataSource = Nothing
        Me.SprdReceipt.Location = New System.Drawing.Point(4, 34)
        Me.SprdReceipt.Name = "SprdReceipt"
        Me.SprdReceipt.OcxState = CType(resources.GetObject("SprdReceipt.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdReceipt.Size = New System.Drawing.Size(548, 447)
        Me.SprdReceipt.TabIndex = 16
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
        'SprdPayment
        '
        Me.SprdPayment.DataSource = Nothing
        Me.SprdPayment.Location = New System.Drawing.Point(553, 34)
        Me.SprdPayment.Name = "SprdPayment"
        Me.SprdPayment.OcxState = CType(resources.GetObject("SprdPayment.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdPayment.Size = New System.Drawing.Size(549, 447)
        Me.SprdPayment.TabIndex = 39
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label2.Location = New System.Drawing.Point(899, 486)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(63, 16)
        Me.Label2.TabIndex = 43
        Me.Label2.Text = "Closing :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Location = New System.Drawing.Point(892, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(70, 16)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "Opening :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(556, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(64, 16)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Payment"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(6, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(56, 16)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Receipt"
        '
        'lblClosing
        '
        Me.lblClosing.BackColor = System.Drawing.Color.Transparent
        Me.lblClosing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClosing.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblClosing.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClosing.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblClosing.Location = New System.Drawing.Point(968, 486)
        Me.lblClosing.Name = "lblClosing"
        Me.lblClosing.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblClosing.Size = New System.Drawing.Size(131, 17)
        Me.lblClosing.TabIndex = 35
        Me.lblClosing.Text = "0.00"
        Me.lblClosing.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblOpening
        '
        Me.lblOpening.BackColor = System.Drawing.Color.Transparent
        Me.lblOpening.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOpening.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblOpening.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpening.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOpening.Location = New System.Drawing.Point(968, 10)
        Me.lblOpening.Name = "lblOpening"
        Me.lblOpening.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblOpening.Size = New System.Drawing.Size(131, 17)
        Me.lblOpening.TabIndex = 34
        Me.lblOpening.Text = "0.00"
        Me.lblOpening.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraOption
        '
        Me.FraOption.BackColor = System.Drawing.SystemColors.Control
        Me.FraOption.Controls.Add(Me._chkGroup_8)
        Me.FraOption.Controls.Add(Me._chkGroup_5)
        Me.FraOption.Controls.Add(Me._chkGroup_2)
        Me.FraOption.Controls.Add(Me._chkGroup_7)
        Me.FraOption.Controls.Add(Me._chkGroup_4)
        Me.FraOption.Controls.Add(Me._chkGroup_1)
        Me.FraOption.Controls.Add(Me._chkGroup_6)
        Me.FraOption.Controls.Add(Me._chkGroup_3)
        Me.FraOption.Controls.Add(Me._chkGroup_0)
        Me.FraOption.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOption.Location = New System.Drawing.Point(516, 0)
        Me.FraOption.Name = "FraOption"
        Me.FraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOption.Size = New System.Drawing.Size(231, 69)
        Me.FraOption.TabIndex = 24
        Me.FraOption.TabStop = False
        Me.FraOption.Visible = False
        '
        '_chkGroup_8
        '
        Me._chkGroup_8.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_8.Checked = True
        Me._chkGroup_8.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_8, CType(8, Short))
        Me._chkGroup_8.Location = New System.Drawing.Point(156, 50)
        Me._chkGroup_8.Name = "_chkGroup_8"
        Me._chkGroup_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_8.Size = New System.Drawing.Size(71, 13)
        Me._chkGroup_8.TabIndex = 12
        Me._chkGroup_8.Text = "PDC"
        Me._chkGroup_8.UseVisualStyleBackColor = False
        '
        '_chkGroup_5
        '
        Me._chkGroup_5.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_5.Checked = True
        Me._chkGroup_5.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_5, CType(5, Short))
        Me._chkGroup_5.Location = New System.Drawing.Point(64, 50)
        Me._chkGroup_5.Name = "_chkGroup_5"
        Me._chkGroup_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_5.Size = New System.Drawing.Size(95, 13)
        Me._chkGroup_5.TabIndex = 9
        Me._chkGroup_5.Text = "Credit Note"
        Me._chkGroup_5.UseVisualStyleBackColor = False
        '
        '_chkGroup_2
        '
        Me._chkGroup_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_2.Checked = True
        Me._chkGroup_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_2, CType(2, Short))
        Me._chkGroup_2.Location = New System.Drawing.Point(6, 50)
        Me._chkGroup_2.Name = "_chkGroup_2"
        Me._chkGroup_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_2.Size = New System.Drawing.Size(61, 13)
        Me._chkGroup_2.TabIndex = 6
        Me._chkGroup_2.Text = "Sale"
        Me._chkGroup_2.UseVisualStyleBackColor = False
        '
        '_chkGroup_7
        '
        Me._chkGroup_7.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_7.Checked = True
        Me._chkGroup_7.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_7, CType(7, Short))
        Me._chkGroup_7.Location = New System.Drawing.Point(156, 32)
        Me._chkGroup_7.Name = "_chkGroup_7"
        Me._chkGroup_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_7.Size = New System.Drawing.Size(71, 13)
        Me._chkGroup_7.TabIndex = 11
        Me._chkGroup_7.Text = "Contra"
        Me._chkGroup_7.UseVisualStyleBackColor = False
        '
        '_chkGroup_4
        '
        Me._chkGroup_4.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_4.Checked = True
        Me._chkGroup_4.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_4, CType(4, Short))
        Me._chkGroup_4.Location = New System.Drawing.Point(64, 32)
        Me._chkGroup_4.Name = "_chkGroup_4"
        Me._chkGroup_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_4.Size = New System.Drawing.Size(95, 13)
        Me._chkGroup_4.TabIndex = 8
        Me._chkGroup_4.Text = "Debit Note"
        Me._chkGroup_4.UseVisualStyleBackColor = False
        '
        '_chkGroup_1
        '
        Me._chkGroup_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_1.Checked = True
        Me._chkGroup_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_1, CType(1, Short))
        Me._chkGroup_1.Location = New System.Drawing.Point(6, 32)
        Me._chkGroup_1.Name = "_chkGroup_1"
        Me._chkGroup_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_1.Size = New System.Drawing.Size(61, 13)
        Me._chkGroup_1.TabIndex = 5
        Me._chkGroup_1.Text = "Cash"
        Me._chkGroup_1.UseVisualStyleBackColor = False
        '
        '_chkGroup_6
        '
        Me._chkGroup_6.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_6.Checked = True
        Me._chkGroup_6.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_6, CType(6, Short))
        Me._chkGroup_6.Location = New System.Drawing.Point(156, 14)
        Me._chkGroup_6.Name = "_chkGroup_6"
        Me._chkGroup_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_6.Size = New System.Drawing.Size(71, 13)
        Me._chkGroup_6.TabIndex = 10
        Me._chkGroup_6.Text = "Journal"
        Me._chkGroup_6.UseVisualStyleBackColor = False
        '
        '_chkGroup_3
        '
        Me._chkGroup_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkGroup_3.Checked = True
        Me._chkGroup_3.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkGroup_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkGroup_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkGroup_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkGroup.SetIndex(Me._chkGroup_3, CType(3, Short))
        Me._chkGroup_3.Location = New System.Drawing.Point(64, 14)
        Me._chkGroup_3.Name = "_chkGroup_3"
        Me._chkGroup_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkGroup_3.Size = New System.Drawing.Size(95, 13)
        Me._chkGroup_3.TabIndex = 7
        Me._chkGroup_3.Text = "Purchase"
        Me._chkGroup_3.UseVisualStyleBackColor = False
        '
        '_chkGroup_0
        '
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
        Me._chkGroup_0.Size = New System.Drawing.Size(61, 13)
        Me._chkGroup_0.TabIndex = 4
        Me._chkGroup_0.Text = "Bank"
        Me._chkGroup_0.UseVisualStyleBackColor = False
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdExport)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(760, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(345, 49)
        Me.FraMovement.TabIndex = 26
        Me.FraMovement.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.CboDept)
        Me.Frame1.Controls.Add(Me.CboCC)
        Me.Frame1.Controls.Add(Me.cboEmp)
        Me.Frame1.Controls.Add(Me._Lbl_3)
        Me.Frame1.Controls.Add(Me._Lbl_2)
        Me.Frame1.Controls.Add(Me._Lbl_4)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(478, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(271, 83)
        Me.Frame1.TabIndex = 28
        Me.Frame1.TabStop = False
        '
        'CboDept
        '
        Me.CboDept.BackColor = System.Drawing.SystemColors.Window
        Me.CboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboDept.Location = New System.Drawing.Point(62, 34)
        Me.CboDept.Name = "CboDept"
        Me.CboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboDept.Size = New System.Drawing.Size(206, 22)
        Me.CboDept.TabIndex = 14
        '
        'CboCC
        '
        Me.CboCC.BackColor = System.Drawing.SystemColors.Window
        Me.CboCC.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboCC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboCC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboCC.Location = New System.Drawing.Point(62, 10)
        Me.CboCC.Name = "CboCC"
        Me.CboCC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboCC.Size = New System.Drawing.Size(206, 22)
        Me.CboCC.TabIndex = 13
        '
        'cboEmp
        '
        Me.cboEmp.BackColor = System.Drawing.SystemColors.Window
        Me.cboEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboEmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboEmp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboEmp.Location = New System.Drawing.Point(62, 58)
        Me.cboEmp.Name = "cboEmp"
        Me.cboEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboEmp.Size = New System.Drawing.Size(206, 22)
        Me.cboEmp.TabIndex = 15
        '
        '_Lbl_3
        '
        Me._Lbl_3.AutoSize = True
        Me._Lbl_3.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_3, CType(3, Short))
        Me._Lbl_3.Location = New System.Drawing.Point(4, 38)
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
        Me._Lbl_2.Location = New System.Drawing.Point(4, 13)
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
        Me._Lbl_4.Location = New System.Drawing.Point(4, 62)
        Me._Lbl_4.Name = "_Lbl_4"
        Me._Lbl_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_4.Size = New System.Drawing.Size(31, 14)
        Me._Lbl_4.TabIndex = 29
        Me._Lbl_4.Text = "Emp"
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(14, 588)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(145, 17)
        Me.lblBookType.TabIndex = 33
        Me.lblBookType.Text = "lblBookType"
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
        'OptSumDet
        '
        '
        'chkGroup
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(585, -7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(518, 74)
        Me.GroupBox1.TabIndex = 65
        Me.GroupBox1.TabStop = False
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
        Me.lstCompanyName.Size = New System.Drawing.Size(518, 61)
        Me.lstCompanyName.TabIndex = 4
        '
        'frmViewDayBook
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraOption)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.lblBookType)
        Me.Controls.Add(Me.lblAcCode)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmViewDayBook"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "View Day Book"
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.SprdReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdPayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraOption.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkGroup, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
#End Region
#Region "Upgrade Support"
    'Public Sub VB6_AddADODataBinding()
    '    'SprdPayment.DataSource = CType(AData2, MSDATASRC.DataSource)
    '    'SprdReceipt.DataSource = CType(AData1, MSDATASRC.DataSource)
    'End Sub
    'Public Sub VB6_RemoveADODataBinding()
    '    SprdPayment.DataSource = Nothing
    '    SprdReceipt.DataSource = Nothing
    'End Sub
#End Region
End Class