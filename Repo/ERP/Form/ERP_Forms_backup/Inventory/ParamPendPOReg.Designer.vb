Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamPendPOReg
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
        '
        ''InventoryGST.Master.Show
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
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchSupp As System.Windows.Forms.Button
    Public WithEvents chkAllSupp As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtItemName As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents cboOrderType As System.Windows.Forms.ComboBox
    Public WithEvents cboPurType As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblAcCode As System.Windows.Forms.Label
    Public WithEvents lblTrnType As System.Windows.Forms.Label
    Public WithEvents Lbl As VB6.LabelArray
    Public WithEvents OptOrderBy As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamPendPOReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.cmdsearchSupp = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.chkAllSupp = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.cboOrderType = New System.Windows.Forms.ComboBox()
        Me.cboPurType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.lblTrnType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame2.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPONo.Location = New System.Drawing.Point(326, 73)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(95, 22)
        Me.txtPONo.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.txtPONo, "Press F1 For Help")
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSupplier.Location = New System.Drawing.Point(78, 13)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(344, 22)
        Me.txtSupplier.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.txtSupplier, "Press F1 For Help")
        '
        'cmdsearchSupp
        '
        Me.cmdsearchSupp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchSupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchSupp.Image = CType(resources.GetObject("cmdsearchSupp.Image"), System.Drawing.Image)
        Me.cmdsearchSupp.Location = New System.Drawing.Point(424, 11)
        Me.cmdsearchSupp.Name = "cmdsearchSupp"
        Me.cmdsearchSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchSupp.Size = New System.Drawing.Size(29, 23)
        Me.cmdsearchSupp.TabIndex = 30
        Me.cmdsearchSupp.TabStop = False
        Me.cmdsearchSupp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchSupp, "Search")
        Me.cmdsearchSupp.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(423, 41)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 23)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'TxtItemName
        '
        Me.TxtItemName.AcceptsReturn = True
        Me.TxtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtItemName.Location = New System.Drawing.Point(78, 43)
        Me.TxtItemName.MaxLength = 0
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtItemName.Size = New System.Drawing.Size(344, 22)
        Me.TxtItemName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtItemName, "Press F1 For Help")
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(123, 11)
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
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(63, 11)
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
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(184, 11)
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
        Me.cmdShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(4, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 6
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 557)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(425, 52)
        Me.Frame2.TabIndex = 26
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order By"
        '
        '_OptOrderBy_0
        '
        Me._OptOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_0.Checked = True
        Me._OptOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_0, CType(0, Short))
        Me._OptOrderBy_0.Location = New System.Drawing.Point(86, 22)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(89, 17)
        Me._OptOrderBy_0.TabIndex = 28
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "PO No."
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_1
        '
        Me._OptOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_1, CType(1, Short))
        Me._OptOrderBy_1.Location = New System.Drawing.Point(262, 22)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(129, 17)
        Me._OptOrderBy_1.TabIndex = 27
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Item Description"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtPONo)
        Me.FraAccount.Controls.Add(Me.cboShow)
        Me.FraAccount.Controls.Add(Me.txtSupplier)
        Me.FraAccount.Controls.Add(Me.cmdsearchSupp)
        Me.FraAccount.Controls.Add(Me.chkAllSupp)
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtItemName)
        Me.FraAccount.Controls.Add(Me.Label7)
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Controls.Add(Me.Label5)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(159, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(523, 100)
        Me.FraAccount.TabIndex = 13
        Me.FraAccount.TabStop = False
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(78, 73)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(107, 21)
        Me.cboShow.TabIndex = 33
        '
        'chkAllSupp
        '
        Me.chkAllSupp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllSupp.Checked = True
        Me.chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllSupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllSupp.Location = New System.Drawing.Point(456, 14)
        Me.chkAllSupp.Name = "chkAllSupp"
        Me.chkAllSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllSupp.Size = New System.Drawing.Size(52, 18)
        Me.chkAllSupp.TabIndex = 29
        Me.chkAllSupp.Text = "ALL"
        Me.chkAllSupp.UseVisualStyleBackColor = False
        '
        'chkAll
        '
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(458, 43)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(52, 18)
        Me.chkAll.TabIndex = 4
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(274, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 36
        Me.Label7.Text = "PO No. :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(28, 76)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(49, 13)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "Show :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(22, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Supplier :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(9, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Item Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboStatus)
        Me.Frame1.Controls.Add(Me.cboOrderType)
        Me.Frame1.Controls.Add(Me.cboPurType)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(683, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(214, 100)
        Me.Frame1.TabIndex = 19
        Me.Frame1.TabStop = False
        '
        'cboStatus
        '
        Me.cboStatus.BackColor = System.Drawing.SystemColors.Window
        Me.cboStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStatus.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStatus.Location = New System.Drawing.Point(82, 73)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStatus.Size = New System.Drawing.Size(126, 21)
        Me.cboStatus.TabIndex = 24
        '
        'cboOrderType
        '
        Me.cboOrderType.BackColor = System.Drawing.SystemColors.Window
        Me.cboOrderType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrderType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboOrderType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboOrderType.Location = New System.Drawing.Point(82, 43)
        Me.cboOrderType.Name = "cboOrderType"
        Me.cboOrderType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboOrderType.Size = New System.Drawing.Size(126, 21)
        Me.cboOrderType.TabIndex = 23
        '
        'cboPurType
        '
        Me.cboPurType.BackColor = System.Drawing.SystemColors.Window
        Me.cboPurType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPurType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPurType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPurType.Location = New System.Drawing.Point(82, 13)
        Me.cboPurType.Name = "cboPurType"
        Me.cboPurType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPurType.Size = New System.Drawing.Size(126, 21)
        Me.cboPurType.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(37, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Status :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(14, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Order Type :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(2, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Pur Type :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(156, 100)
        Me.Frame6.TabIndex = 10
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(59, 21)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(89, 22)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(59, 58)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(89, 22)
        Me.txtDateTo.TabIndex = 1
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(30, 62)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(25, 13)
        Me._Lbl_1.TabIndex = 12
        Me._Lbl_1.Text = "To :"
        Me._Lbl_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(15, 25)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(40, 13)
        Me._Lbl_0.TabIndex = 11
        Me._Lbl_0.Text = "From :"
        Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(-2, 98)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(900, 462)
        Me.Frame4.TabIndex = 14
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(5, 10)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(893, 446)
        Me.SprdMain.TabIndex = 5
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 6
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(652, 556)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 54)
        Me.FraMovement.TabIndex = 15
        Me.FraMovement.TabStop = False
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(250, 428)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(87, 13)
        Me.lblAcCode.TabIndex = 17
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'lblTrnType
        '
        Me.lblTrnType.AutoSize = True
        Me.lblTrnType.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrnType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTrnType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrnType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrnType.Location = New System.Drawing.Point(172, 432)
        Me.lblTrnType.Name = "lblTrnType"
        Me.lblTrnType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnType.Size = New System.Drawing.Size(59, 13)
        Me.lblTrnType.TabIndex = 16
        Me.lblTrnType.Text = "lblTrnType"
        Me.lblTrnType.Visible = False
        '
        'frmParamPendPOReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.lblAcCode)
        Me.Controls.Add(Me.lblTrnType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamPendPOReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Pending Purchase Order Register"
        Me.Frame2.ResumeLayout(False)
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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