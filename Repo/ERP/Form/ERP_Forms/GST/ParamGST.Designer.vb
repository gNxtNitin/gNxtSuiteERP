Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamGST
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
        'Me.MDIParent = SalesGST.Master
        'SalesGST.Master.Show()
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
    Public WithEvents cboShowType As System.Windows.Forms.ComboBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents chkTransfer As System.Windows.Forms.CheckBox
    Public WithEvents chkChallan As System.Windows.Forms.CheckBox
    Public WithEvents chkSale As System.Windows.Forms.CheckBox
    Public WithEvents chkSaleDN As System.Windows.Forms.CheckBox
    Public WithEvents chkPurchase As System.Windows.Forms.CheckBox
    Public WithEvents chkPurSupp As System.Windows.Forms.CheckBox
    Public WithEvents chkDNCN As System.Windows.Forms.CheckBox
    Public WithEvents chkLCOpening As System.Windows.Forms.CheckBox
    Public WithEvents chkLCDiscount As System.Windows.Forms.CheckBox
    Public WithEvents FraShow As System.Windows.Forms.GroupBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents optShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamGST))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboShowType = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me.FraShow = New System.Windows.Forms.GroupBox()
        Me.chkTransfer = New System.Windows.Forms.CheckBox()
        Me.chkChallan = New System.Windows.Forms.CheckBox()
        Me.chkSale = New System.Windows.Forms.CheckBox()
        Me.chkSaleDN = New System.Windows.Forms.CheckBox()
        Me.chkPurchase = New System.Windows.Forms.CheckBox()
        Me.chkPurSupp = New System.Windows.Forms.CheckBox()
        Me.chkDNCN = New System.Windows.Forms.CheckBox()
        Me.chkLCOpening = New System.Windows.Forms.CheckBox()
        Me.chkLCDiscount = New System.Windows.Forms.CheckBox()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.FraShow.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Enabled = False
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(444, 12)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearch.TabIndex = 3
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
        Me.TxtAccount.Enabled = False
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(106, 12)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(335, 20)
        Me.TxtAccount.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(123, 9)
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
        Me.cmdPrint.Location = New System.Drawing.Point(63, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 7
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
        Me.cmdClose.Location = New System.Drawing.Point(184, 9)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 37)
        Me.cmdClose.TabIndex = 9
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
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 6
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboShowType)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(124, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(121, 88)
        Me.Frame1.TabIndex = 30
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Show Type"
        '
        'cboShowType
        '
        Me.cboShowType.BackColor = System.Drawing.SystemColors.Window
        Me.cboShowType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShowType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShowType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShowType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShowType.Location = New System.Drawing.Point(4, 24)
        Me.cboShowType.Name = "cboShowType"
        Me.cboShowType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShowType.Size = New System.Drawing.Size(113, 22)
        Me.cboShowType.TabIndex = 31
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optShow_0)
        Me.Frame3.Controls.Add(Me._optShow_1)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 574)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(179, 45)
        Me.Frame3.TabIndex = 27
        Me.Frame3.TabStop = False
        '
        '_optShow_0
        '
        Me._optShow_0.AutoSize = True
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Checked = True
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(2, 16)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(51, 18)
        Me._optShow_0.TabIndex = 29
        Me._optShow_0.TabStop = True
        Me._optShow_0.Text = "Detail"
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        '_optShow_1
        '
        Me._optShow_1.AutoSize = True
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(90, 16)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(70, 18)
        Me._optShow_1.TabIndex = 28
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "Summary"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        'FraShow
        '
        Me.FraShow.BackColor = System.Drawing.SystemColors.Control
        Me.FraShow.Controls.Add(Me.chkTransfer)
        Me.FraShow.Controls.Add(Me.chkChallan)
        Me.FraShow.Controls.Add(Me.chkSale)
        Me.FraShow.Controls.Add(Me.chkSaleDN)
        Me.FraShow.Controls.Add(Me.chkPurchase)
        Me.FraShow.Controls.Add(Me.chkPurSupp)
        Me.FraShow.Controls.Add(Me.chkDNCN)
        Me.FraShow.Controls.Add(Me.chkLCOpening)
        Me.FraShow.Controls.Add(Me.chkLCDiscount)
        Me.FraShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraShow.Location = New System.Drawing.Point(244, 0)
        Me.FraShow.Name = "FraShow"
        Me.FraShow.Padding = New System.Windows.Forms.Padding(0)
        Me.FraShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraShow.Size = New System.Drawing.Size(383, 87)
        Me.FraShow.TabIndex = 17
        Me.FraShow.TabStop = False
        Me.FraShow.Text = "Show"
        '
        'chkTransfer
        '
        Me.chkTransfer.AutoSize = True
        Me.chkTransfer.BackColor = System.Drawing.SystemColors.Control
        Me.chkTransfer.Checked = True
        Me.chkTransfer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTransfer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTransfer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransfer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTransfer.Location = New System.Drawing.Point(270, 61)
        Me.chkTransfer.Name = "chkTransfer"
        Me.chkTransfer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTransfer.Size = New System.Drawing.Size(68, 18)
        Me.chkTransfer.TabIndex = 26
        Me.chkTransfer.Text = "Transfer"
        Me.chkTransfer.UseVisualStyleBackColor = False
        '
        'chkChallan
        '
        Me.chkChallan.AutoSize = True
        Me.chkChallan.BackColor = System.Drawing.SystemColors.Control
        Me.chkChallan.Checked = True
        Me.chkChallan.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkChallan.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkChallan.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkChallan.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkChallan.Location = New System.Drawing.Point(138, 61)
        Me.chkChallan.Name = "chkChallan"
        Me.chkChallan.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkChallan.Size = New System.Drawing.Size(61, 18)
        Me.chkChallan.TabIndex = 25
        Me.chkChallan.Text = "Challan"
        Me.chkChallan.UseVisualStyleBackColor = False
        '
        'chkSale
        '
        Me.chkSale.AutoSize = True
        Me.chkSale.BackColor = System.Drawing.SystemColors.Control
        Me.chkSale.Checked = True
        Me.chkSale.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSale.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSale.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSale.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSale.Location = New System.Drawing.Point(6, 14)
        Me.chkSale.Name = "chkSale"
        Me.chkSale.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSale.Size = New System.Drawing.Size(47, 18)
        Me.chkSale.TabIndex = 24
        Me.chkSale.Text = "Sale"
        Me.chkSale.UseVisualStyleBackColor = False
        '
        'chkSaleDN
        '
        Me.chkSaleDN.AutoSize = True
        Me.chkSaleDN.BackColor = System.Drawing.SystemColors.Control
        Me.chkSaleDN.Checked = True
        Me.chkSaleDN.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSaleDN.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSaleDN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSaleDN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSaleDN.Location = New System.Drawing.Point(6, 37)
        Me.chkSaleDN.Name = "chkSaleDN"
        Me.chkSaleDN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSaleDN.Size = New System.Drawing.Size(99, 18)
        Me.chkSaleDN.TabIndex = 23
        Me.chkSaleDN.Text = "Sale Debit Note"
        Me.chkSaleDN.UseVisualStyleBackColor = False
        '
        'chkPurchase
        '
        Me.chkPurchase.AutoSize = True
        Me.chkPurchase.BackColor = System.Drawing.SystemColors.Control
        Me.chkPurchase.Checked = True
        Me.chkPurchase.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPurchase.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPurchase.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPurchase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPurchase.Location = New System.Drawing.Point(138, 14)
        Me.chkPurchase.Name = "chkPurchase"
        Me.chkPurchase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPurchase.Size = New System.Drawing.Size(72, 18)
        Me.chkPurchase.TabIndex = 22
        Me.chkPurchase.Text = "Purchase"
        Me.chkPurchase.UseVisualStyleBackColor = False
        '
        'chkPurSupp
        '
        Me.chkPurSupp.AutoSize = True
        Me.chkPurSupp.BackColor = System.Drawing.SystemColors.Control
        Me.chkPurSupp.Checked = True
        Me.chkPurSupp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPurSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPurSupp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPurSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPurSupp.Location = New System.Drawing.Point(138, 37)
        Me.chkPurSupp.Name = "chkPurSupp"
        Me.chkPurSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPurSupp.Size = New System.Drawing.Size(100, 18)
        Me.chkPurSupp.TabIndex = 21
        Me.chkPurSupp.Text = "Purchase Supp"
        Me.chkPurSupp.UseVisualStyleBackColor = False
        '
        'chkDNCN
        '
        Me.chkDNCN.AutoSize = True
        Me.chkDNCN.BackColor = System.Drawing.SystemColors.Control
        Me.chkDNCN.Checked = True
        Me.chkDNCN.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDNCN.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDNCN.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDNCN.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDNCN.Location = New System.Drawing.Point(6, 61)
        Me.chkDNCN.Name = "chkDNCN"
        Me.chkDNCN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDNCN.Size = New System.Drawing.Size(99, 18)
        Me.chkDNCN.TabIndex = 20
        Me.chkDNCN.Text = "Supplier DN/CN"
        Me.chkDNCN.UseVisualStyleBackColor = False
        '
        'chkLCOpening
        '
        Me.chkLCOpening.AutoSize = True
        Me.chkLCOpening.BackColor = System.Drawing.SystemColors.Control
        Me.chkLCOpening.Checked = True
        Me.chkLCOpening.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLCOpening.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLCOpening.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLCOpening.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLCOpening.Location = New System.Drawing.Point(270, 14)
        Me.chkLCOpening.Name = "chkLCOpening"
        Me.chkLCOpening.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLCOpening.Size = New System.Drawing.Size(82, 18)
        Me.chkLCOpening.TabIndex = 19
        Me.chkLCOpening.Text = "LC Opening"
        Me.chkLCOpening.UseVisualStyleBackColor = False
        '
        'chkLCDiscount
        '
        Me.chkLCDiscount.AutoSize = True
        Me.chkLCDiscount.BackColor = System.Drawing.SystemColors.Control
        Me.chkLCDiscount.Checked = True
        Me.chkLCDiscount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLCDiscount.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLCDiscount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLCDiscount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLCDiscount.Location = New System.Drawing.Point(270, 37)
        Me.chkLCDiscount.Name = "chkLCDiscount"
        Me.chkLCDiscount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLCDiscount.Size = New System.Drawing.Size(84, 18)
        Me.chkLCDiscount.TabIndex = 18
        Me.chkLCDiscount.Text = "LC Discount"
        Me.chkLCDiscount.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(228, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(521, 57)
        Me.FraAccount.TabIndex = 13
        Me.FraAccount.TabStop = False
        '
        'chkAll
        '
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Enabled = False
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(474, 16)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(45, 13)
        Me.chkAll.TabIndex = 4
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(2, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(104, 17)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Account Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me.Report1)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(123, 88)
        Me.Frame6.TabIndex = 10
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(40, 10)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(79, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(40, 38)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(79, 20)
        Me.txtDateTo.TabIndex = 1
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 2
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(14, 42)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(24, 14)
        Me._Lbl_1.TabIndex = 12
        Me._Lbl_1.Text = "To :"
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 13)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(37, 14)
        Me._Lbl_0.TabIndex = 11
        Me._Lbl_0.Text = "From :"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 84)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1193, 487)
        Me.Frame4.TabIndex = 14
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1193, 474)
        Me.SprdMain.TabIndex = 5
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
        Me.FraMovement.Location = New System.Drawing.Point(770, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(249, 49)
        Me.FraMovement.TabIndex = 15
        Me.FraMovement.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox3.Controls.Add(Me.lstCompanyName)
        Me.GroupBox3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox3.Location = New System.Drawing.Point(628, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox3.Size = New System.Drawing.Size(391, 87)
        Me.GroupBox3.TabIndex = 46
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Company Name"
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
        Me.lstCompanyName.Size = New System.Drawing.Size(391, 74)
        Me.lstCompanyName.TabIndex = 2
        '
        'frmParamGST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1194, 621)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.FraShow)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamGST"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "FORM GST Register"
        Me.Frame1.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.FraShow.ResumeLayout(False)
        Me.FraShow.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain.DataSource = Nothing
    End Sub

    Public WithEvents GroupBox3 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
#End Region
End Class