Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintMultiEntry
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
    End Sub
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents _optSend_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optSend_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optSend_0 As System.Windows.Forms.RadioButton
    Public WithEvents FraSend As System.Windows.Forms.GroupBox
    Public WithEvents chkAllSupp As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearchSupp As System.Windows.Forms.Button
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboVoucher As System.Windows.Forms.ComboBox
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents _optPrintRange_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optPrintRange_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPrintRange_0 As System.Windows.Forms.RadioButton
    Public WithEvents _cmdsearchVNO_1 As System.Windows.Forms.Button
    Public WithEvents _cmdsearchVNO_0 As System.Windows.Forms.Button
    Public WithEvents txtVNoFrom As System.Windows.Forms.TextBox
    Public WithEvents txtVNoTo As System.Windows.Forms.TextBox
    Public WithEvents _Lbl_3 As System.Windows.Forms.Label
    Public WithEvents _Lbl_2 As System.Windows.Forms.Label
    Public WithEvents FraVNoRange As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents FraDateRange As System.Windows.Forms.GroupBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents chkPrintType As System.Windows.Forms.CheckBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As VB6.LabelArray
    Public WithEvents cmdsearchVNO As VB6.ButtonArray
    Public WithEvents optPrintRange As VB6.RadioButtonArray
    Public WithEvents optSend As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintMultiEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearchSupp = New System.Windows.Forms.Button()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me._cmdsearchVNO_1 = New System.Windows.Forms.Button()
        Me._cmdsearchVNO_0 = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.FraSend = New System.Windows.Forms.GroupBox()
        Me._optSend_2 = New System.Windows.Forms.RadioButton()
        Me._optSend_1 = New System.Windows.Forms.RadioButton()
        Me._optSend_0 = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkAllSupp = New System.Windows.Forms.CheckBox()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.cboVoucher = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optPrintRange_2 = New System.Windows.Forms.RadioButton()
        Me._optPrintRange_1 = New System.Windows.Forms.RadioButton()
        Me._optPrintRange_0 = New System.Windows.Forms.RadioButton()
        Me.FraVNoRange = New System.Windows.Forms.GroupBox()
        Me.txtVNoFrom = New System.Windows.Forms.TextBox()
        Me.txtVNoTo = New System.Windows.Forms.TextBox()
        Me._Lbl_3 = New System.Windows.Forms.Label()
        Me._Lbl_2 = New System.Windows.Forms.Label()
        Me.FraDateRange = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkPrintType = New System.Windows.Forms.CheckBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.cmdsearchVNO = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.optPrintRange = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optSend = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraSend.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.FraVNoRange.SuspendLayout()
        Me.FraDateRange.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdsearchVNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPrintRange, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optSend, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearchSupp
        '
        Me.cmdsearchSupp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchSupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchSupp.Image = CType(resources.GetObject("cmdsearchSupp.Image"), System.Drawing.Image)
        Me.cmdsearchSupp.Location = New System.Drawing.Point(291, 20)
        Me.cmdsearchSupp.Name = "cmdsearchSupp"
        Me.cmdsearchSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchSupp.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearchSupp.TabIndex = 25
        Me.cmdsearchSupp.TabStop = False
        Me.cmdsearchSupp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchSupp, "Search")
        Me.cmdsearchSupp.UseVisualStyleBackColor = False
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSupplier.Location = New System.Drawing.Point(4, 20)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(285, 22)
        Me.txtSupplier.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.txtSupplier, "Press F1 For Help")
        '
        '_cmdsearchVNO_1
        '
        Me._cmdsearchVNO_1.BackColor = System.Drawing.SystemColors.Menu
        Me._cmdsearchVNO_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdsearchVNO_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdsearchVNO_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cmdsearchVNO_1.Image = CType(resources.GetObject("_cmdsearchVNO_1.Image"), System.Drawing.Image)
        Me.cmdsearchVNO.SetIndex(Me._cmdsearchVNO_1, CType(1, Short))
        Me._cmdsearchVNO_1.Location = New System.Drawing.Point(138, 48)
        Me._cmdsearchVNO_1.Name = "_cmdsearchVNO_1"
        Me._cmdsearchVNO_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdsearchVNO_1.Size = New System.Drawing.Size(23, 19)
        Me._cmdsearchVNO_1.TabIndex = 19
        Me._cmdsearchVNO_1.TabStop = False
        Me._cmdsearchVNO_1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._cmdsearchVNO_1, "Search")
        Me._cmdsearchVNO_1.UseVisualStyleBackColor = False
        '
        '_cmdsearchVNO_0
        '
        Me._cmdsearchVNO_0.BackColor = System.Drawing.SystemColors.Menu
        Me._cmdsearchVNO_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdsearchVNO_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdsearchVNO_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cmdsearchVNO_0.Image = CType(resources.GetObject("_cmdsearchVNO_0.Image"), System.Drawing.Image)
        Me.cmdsearchVNO.SetIndex(Me._cmdsearchVNO_0, CType(0, Short))
        Me._cmdsearchVNO_0.Location = New System.Drawing.Point(138, 20)
        Me._cmdsearchVNO_0.Name = "_cmdsearchVNO_0"
        Me._cmdsearchVNO_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdsearchVNO_0.Size = New System.Drawing.Size(23, 19)
        Me._cmdsearchVNO_0.TabIndex = 18
        Me._cmdsearchVNO_0.TabStop = False
        Me._cmdsearchVNO_0.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._cmdsearchVNO_0, "Search")
        Me._cmdsearchVNO_0.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(130, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 5
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(64, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.Location = New System.Drawing.Point(198, 10)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(67, 37)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'FraSend
        '
        Me.FraSend.BackColor = System.Drawing.SystemColors.Control
        Me.FraSend.Controls.Add(Me._optSend_2)
        Me.FraSend.Controls.Add(Me._optSend_1)
        Me.FraSend.Controls.Add(Me._optSend_0)
        Me.FraSend.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraSend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraSend.Location = New System.Drawing.Point(0, 204)
        Me.FraSend.Name = "FraSend"
        Me.FraSend.Padding = New System.Windows.Forms.Padding(0)
        Me.FraSend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraSend.Size = New System.Drawing.Size(373, 47)
        Me.FraSend.TabIndex = 28
        Me.FraSend.TabStop = False
        Me.FraSend.Text = "MRR Send to A/c"
        '
        '_optSend_2
        '
        Me._optSend_2.BackColor = System.Drawing.SystemColors.Control
        Me._optSend_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optSend_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optSend_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSend.SetIndex(Me._optSend_2, CType(2, Short))
        Me._optSend_2.Location = New System.Drawing.Point(270, 22)
        Me._optSend_2.Name = "_optSend_2"
        Me._optSend_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optSend_2.Size = New System.Drawing.Size(125, 16)
        Me._optSend_2.TabIndex = 31
        Me._optSend_2.TabStop = True
        Me._optSend_2.Text = "Send"
        Me._optSend_2.UseVisualStyleBackColor = False
        '
        '_optSend_1
        '
        Me._optSend_1.BackColor = System.Drawing.SystemColors.Control
        Me._optSend_1.Checked = True
        Me._optSend_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optSend_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optSend_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSend.SetIndex(Me._optSend_1, CType(1, Short))
        Me._optSend_1.Location = New System.Drawing.Point(134, 22)
        Me._optSend_1.Name = "_optSend_1"
        Me._optSend_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optSend_1.Size = New System.Drawing.Size(125, 16)
        Me._optSend_1.TabIndex = 30
        Me._optSend_1.TabStop = True
        Me._optSend_1.Text = "Not Send"
        Me._optSend_1.UseVisualStyleBackColor = False
        '
        '_optSend_0
        '
        Me._optSend_0.BackColor = System.Drawing.SystemColors.Control
        Me._optSend_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optSend_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optSend_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optSend.SetIndex(Me._optSend_0, CType(0, Short))
        Me._optSend_0.Location = New System.Drawing.Point(4, 22)
        Me._optSend_0.Name = "_optSend_0"
        Me._optSend_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optSend_0.Size = New System.Drawing.Size(125, 16)
        Me._optSend_0.TabIndex = 29
        Me._optSend_0.TabStop = True
        Me._optSend_0.Text = "All"
        Me._optSend_0.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.chkAllSupp)
        Me.Frame2.Controls.Add(Me.cmdsearchSupp)
        Me.Frame2.Controls.Add(Me.txtSupplier)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 42)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(373, 51)
        Me.Frame2.TabIndex = 23
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Supplier"
        '
        'chkAllSupp
        '
        Me.chkAllSupp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllSupp.Checked = True
        Me.chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllSupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllSupp.Location = New System.Drawing.Point(321, 22)
        Me.chkAllSupp.Name = "chkAllSupp"
        Me.chkAllSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllSupp.Size = New System.Drawing.Size(48, 15)
        Me.chkAllSupp.TabIndex = 26
        Me.chkAllSupp.Text = "ALL"
        Me.chkAllSupp.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.cboVoucher)
        Me.FraAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, -2)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(373, 43)
        Me.FraAccount.TabIndex = 0
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Book"
        '
        'cboVoucher
        '
        Me.cboVoucher.BackColor = System.Drawing.SystemColors.Window
        Me.cboVoucher.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboVoucher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVoucher.Enabled = False
        Me.cboVoucher.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboVoucher.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cboVoucher.Location = New System.Drawing.Point(50, 14)
        Me.cboVoucher.Name = "cboVoucher"
        Me.cboVoucher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboVoucher.Size = New System.Drawing.Size(236, 21)
        Me.cboVoucher.Sorted = True
        Me.cboVoucher.TabIndex = 1
        Me.cboVoucher.TabStop = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optPrintRange_2)
        Me.Frame3.Controls.Add(Me._optPrintRange_1)
        Me.Frame3.Controls.Add(Me._optPrintRange_0)
        Me.Frame3.Controls.Add(Me.FraVNoRange)
        Me.Frame3.Controls.Add(Me.FraDateRange)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 88)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(373, 115)
        Me.Frame3.TabIndex = 7
        Me.Frame3.TabStop = False
        '
        '_optPrintRange_2
        '
        Me._optPrintRange_2.AutoSize = True
        Me._optPrintRange_2.BackColor = System.Drawing.SystemColors.Control
        Me._optPrintRange_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrintRange_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrintRange_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintRange.SetIndex(Me._optPrintRange_2, CType(2, Short))
        Me._optPrintRange_2.Location = New System.Drawing.Point(134, 16)
        Me._optPrintRange_2.Name = "_optPrintRange_2"
        Me._optPrintRange_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrintRange_2.Size = New System.Drawing.Size(111, 17)
        Me._optPrintRange_2.TabIndex = 27
        Me._optPrintRange_2.TabStop = True
        Me._optPrintRange_2.Text = "MRR Date Range"
        Me._optPrintRange_2.UseVisualStyleBackColor = False
        '
        '_optPrintRange_1
        '
        Me._optPrintRange_1.AutoSize = True
        Me._optPrintRange_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPrintRange_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrintRange_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrintRange_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintRange.SetIndex(Me._optPrintRange_1, CType(1, Short))
        Me._optPrintRange_1.Location = New System.Drawing.Point(270, 16)
        Me._optPrintRange_1.Name = "_optPrintRange_1"
        Me._optPrintRange_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrintRange_1.Size = New System.Drawing.Size(82, 17)
        Me._optPrintRange_1.TabIndex = 17
        Me._optPrintRange_1.TabStop = True
        Me._optPrintRange_1.Text = "VNo Range"
        Me._optPrintRange_1.UseVisualStyleBackColor = False
        '
        '_optPrintRange_0
        '
        Me._optPrintRange_0.AutoSize = True
        Me._optPrintRange_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPrintRange_0.Checked = True
        Me._optPrintRange_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrintRange_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrintRange_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintRange.SetIndex(Me._optPrintRange_0, CType(0, Short))
        Me._optPrintRange_0.Location = New System.Drawing.Point(4, 16)
        Me._optPrintRange_0.Name = "_optPrintRange_0"
        Me._optPrintRange_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrintRange_0.Size = New System.Drawing.Size(112, 17)
        Me._optPrintRange_0.TabIndex = 16
        Me._optPrintRange_0.TabStop = True
        Me._optPrintRange_0.Text = "Send Date Range"
        Me._optPrintRange_0.UseVisualStyleBackColor = False
        '
        'FraVNoRange
        '
        Me.FraVNoRange.BackColor = System.Drawing.SystemColors.Control
        Me.FraVNoRange.Controls.Add(Me._cmdsearchVNO_1)
        Me.FraVNoRange.Controls.Add(Me._cmdsearchVNO_0)
        Me.FraVNoRange.Controls.Add(Me.txtVNoFrom)
        Me.FraVNoRange.Controls.Add(Me.txtVNoTo)
        Me.FraVNoRange.Controls.Add(Me._Lbl_3)
        Me.FraVNoRange.Controls.Add(Me._Lbl_2)
        Me.FraVNoRange.Enabled = False
        Me.FraVNoRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraVNoRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraVNoRange.Location = New System.Drawing.Point(206, 40)
        Me.FraVNoRange.Name = "FraVNoRange"
        Me.FraVNoRange.Padding = New System.Windows.Forms.Padding(0)
        Me.FraVNoRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraVNoRange.Size = New System.Drawing.Size(165, 75)
        Me.FraVNoRange.TabIndex = 11
        Me.FraVNoRange.TabStop = False
        Me.FraVNoRange.Text = "Voucher No  Range Wise"
        '
        'txtVNoFrom
        '
        Me.txtVNoFrom.AcceptsReturn = True
        Me.txtVNoFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNoFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNoFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNoFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNoFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVNoFrom.Location = New System.Drawing.Point(44, 20)
        Me.txtVNoFrom.MaxLength = 0
        Me.txtVNoFrom.Name = "txtVNoFrom"
        Me.txtVNoFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoFrom.Size = New System.Drawing.Size(93, 22)
        Me.txtVNoFrom.TabIndex = 13
        '
        'txtVNoTo
        '
        Me.txtVNoTo.AcceptsReturn = True
        Me.txtVNoTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNoTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNoTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNoTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNoTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtVNoTo.Location = New System.Drawing.Point(44, 48)
        Me.txtVNoTo.MaxLength = 0
        Me.txtVNoTo.Name = "txtVNoTo"
        Me.txtVNoTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNoTo.Size = New System.Drawing.Size(93, 22)
        Me.txtVNoTo.TabIndex = 12
        '
        '_Lbl_3
        '
        Me._Lbl_3.AutoSize = True
        Me._Lbl_3.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_3, CType(3, Short))
        Me._Lbl_3.Location = New System.Drawing.Point(6, 51)
        Me._Lbl_3.Name = "_Lbl_3"
        Me._Lbl_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_3.Size = New System.Drawing.Size(25, 13)
        Me._Lbl_3.TabIndex = 15
        Me._Lbl_3.Text = "To :"
        '
        '_Lbl_2
        '
        Me._Lbl_2.AutoSize = True
        Me._Lbl_2.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_2, CType(2, Short))
        Me._Lbl_2.Location = New System.Drawing.Point(6, 23)
        Me._Lbl_2.Name = "_Lbl_2"
        Me._Lbl_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_2.Size = New System.Drawing.Size(40, 13)
        Me._Lbl_2.TabIndex = 14
        Me._Lbl_2.Text = "From :"
        '
        'FraDateRange
        '
        Me.FraDateRange.BackColor = System.Drawing.SystemColors.Control
        Me.FraDateRange.Controls.Add(Me.txtDateFrom)
        Me.FraDateRange.Controls.Add(Me.txtDateTo)
        Me.FraDateRange.Controls.Add(Me._Lbl_0)
        Me.FraDateRange.Controls.Add(Me._Lbl_1)
        Me.FraDateRange.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraDateRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraDateRange.Location = New System.Drawing.Point(2, 40)
        Me.FraDateRange.Name = "FraDateRange"
        Me.FraDateRange.Padding = New System.Windows.Forms.Padding(0)
        Me.FraDateRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraDateRange.Size = New System.Drawing.Size(153, 75)
        Me.FraDateRange.TabIndex = 8
        Me.FraDateRange.TabStop = False
        Me.FraDateRange.Text = "Date Range Wise"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(46, 20)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(83, 22)
        Me.txtDateFrom.TabIndex = 20
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(46, 47)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(83, 22)
        Me.txtDateTo.TabIndex = 21
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(6, 23)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(40, 13)
        Me._Lbl_0.TabIndex = 10
        Me._Lbl_0.Text = "From :"
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(6, 51)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(25, 13)
        Me._Lbl_1.TabIndex = 9
        Me._Lbl_1.Text = "To :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkPrintType)
        Me.Frame1.Controls.Add(Me.CmdPreview)
        Me.Frame1.Controls.Add(Me.cmdPrint)
        Me.Frame1.Controls.Add(Me.cmdExit)
        Me.Frame1.Controls.Add(Me.Report1)
        Me.Frame1.Controls.Add(Me.lblBookType)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 246)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(373, 51)
        Me.Frame1.TabIndex = 2
        Me.Frame1.TabStop = False
        '
        'chkPrintType
        '
        Me.chkPrintType.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintType.Checked = True
        Me.chkPrintType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrintType.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintType.Location = New System.Drawing.Point(296, 14)
        Me.chkPrintType.Name = "chkPrintType"
        Me.chkPrintType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintType.Size = New System.Drawing.Size(67, 29)
        Me.chkPrintType.TabIndex = 22
        Me.chkPrintType.Text = "Printed Format"
        Me.chkPrintType.UseVisualStyleBackColor = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(234, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 23
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(12, 18)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(37, 17)
        Me.lblBookType.TabIndex = 6
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'cmdsearchVNO
        '
        '
        'optPrintRange
        '
        '
        'frmPrintMultiEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(373, 298)
        Me.Controls.Add(Me.FraSend)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintMultiEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Multiple Voucher Printing"
        Me.FraSend.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.FraVNoRange.ResumeLayout(False)
        Me.FraVNoRange.PerformLayout()
        Me.FraDateRange.ResumeLayout(False)
        Me.FraDateRange.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdsearchVNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPrintRange, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optSend, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class