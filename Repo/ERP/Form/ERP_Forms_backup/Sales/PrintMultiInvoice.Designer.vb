Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintMultiInvoice
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
    Public WithEvents txtPartyName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchParty As System.Windows.Forms.Button
    Public WithEvents _optPartyName_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optPartyName_1 As System.Windows.Forms.RadioButton
    Public WithEvents lblInvoiceSeq As System.Windows.Forms.Label
    Public WithEvents _Lbl_4 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _optOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents chkPrintAll As System.Windows.Forms.CheckBox
    Public WithEvents cboInvType As System.Windows.Forms.ComboBox
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents _optPrintRange_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPrintRange_0 As System.Windows.Forms.RadioButton
    Public WithEvents _cmdsearchInvNO_1 As System.Windows.Forms.Button
    Public WithEvents _cmdsearchInvNO_0 As System.Windows.Forms.Button
    Public WithEvents txtInvNoFrom As System.Windows.Forms.TextBox
    Public WithEvents txtInvNoTo As System.Windows.Forms.TextBox
    Public WithEvents _Lbl_3 As System.Windows.Forms.Label
    Public WithEvents _Lbl_2 As System.Windows.Forms.Label
    Public WithEvents FraVNoRange As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents FraDateRange As System.Windows.Forms.GroupBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents cmdPDF As System.Windows.Forms.Button
    Public WithEvents ChkPaintPrint As System.Windows.Forms.CheckBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents cmdsearchInvNO As Microsoft.VisualBasic.Compatibility.VB6.ButtonArray
    Public WithEvents optOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optPartyName As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optPrintRange As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintMultiInvoice))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchParty = New System.Windows.Forms.Button()
        Me._cmdsearchInvNO_1 = New System.Windows.Forms.Button()
        Me._cmdsearchInvNO_0 = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmdPDF = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me._optPartyName_0 = New System.Windows.Forms.RadioButton()
        Me._optPartyName_1 = New System.Windows.Forms.RadioButton()
        Me.lblInvoiceSeq = New System.Windows.Forms.Label()
        Me._Lbl_4 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkPrintAll = New System.Windows.Forms.CheckBox()
        Me.cboInvType = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optPrintRange_1 = New System.Windows.Forms.RadioButton()
        Me._optPrintRange_0 = New System.Windows.Forms.RadioButton()
        Me.FraVNoRange = New System.Windows.Forms.GroupBox()
        Me.txtInvNoFrom = New System.Windows.Forms.TextBox()
        Me.txtInvNoTo = New System.Windows.Forms.TextBox()
        Me._Lbl_3 = New System.Windows.Forms.Label()
        Me._Lbl_2 = New System.Windows.Forms.Label()
        Me.FraDateRange = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.ChkPaintPrint = New System.Windows.Forms.CheckBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.cmdsearchInvNO = New Microsoft.VisualBasic.Compatibility.VB6.ButtonArray(Me.components)
        Me.optOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optPartyName = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optPrintRange = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.FraVNoRange.SuspendLayout()
        Me.FraDateRange.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdsearchInvNO, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPartyName, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPrintRange, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchParty
        '
        Me.cmdSearchParty.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchParty.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchParty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchParty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchParty.Image = CType(resources.GetObject("cmdSearchParty.Image"), System.Drawing.Image)
        Me.cmdSearchParty.Location = New System.Drawing.Point(290, 40)
        Me.cmdSearchParty.Name = "cmdSearchParty"
        Me.cmdSearchParty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchParty.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchParty.TabIndex = 28
        Me.cmdSearchParty.TabStop = False
        Me.cmdSearchParty.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchParty, "Search")
        Me.cmdSearchParty.UseVisualStyleBackColor = False
        '
        '_cmdsearchInvNO_1
        '
        Me._cmdsearchInvNO_1.BackColor = System.Drawing.SystemColors.Menu
        Me._cmdsearchInvNO_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdsearchInvNO_1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdsearchInvNO_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cmdsearchInvNO_1.Image = CType(resources.GetObject("_cmdsearchInvNO_1.Image"), System.Drawing.Image)
        Me.cmdsearchInvNO.SetIndex(Me._cmdsearchInvNO_1, CType(1, Short))
        Me._cmdsearchInvNO_1.Location = New System.Drawing.Point(138, 48)
        Me._cmdsearchInvNO_1.Name = "_cmdsearchInvNO_1"
        Me._cmdsearchInvNO_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdsearchInvNO_1.Size = New System.Drawing.Size(23, 19)
        Me._cmdsearchInvNO_1.TabIndex = 19
        Me._cmdsearchInvNO_1.TabStop = False
        Me._cmdsearchInvNO_1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._cmdsearchInvNO_1, "Search")
        Me._cmdsearchInvNO_1.UseVisualStyleBackColor = False
        '
        '_cmdsearchInvNO_0
        '
        Me._cmdsearchInvNO_0.BackColor = System.Drawing.SystemColors.Menu
        Me._cmdsearchInvNO_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._cmdsearchInvNO_0.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._cmdsearchInvNO_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._cmdsearchInvNO_0.Image = CType(resources.GetObject("_cmdsearchInvNO_0.Image"), System.Drawing.Image)
        Me.cmdsearchInvNO.SetIndex(Me._cmdsearchInvNO_0, CType(0, Short))
        Me._cmdsearchInvNO_0.Location = New System.Drawing.Point(138, 20)
        Me._cmdsearchInvNO_0.Name = "_cmdsearchInvNO_0"
        Me._cmdsearchInvNO_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._cmdsearchInvNO_0.Size = New System.Drawing.Size(23, 19)
        Me._cmdsearchInvNO_0.TabIndex = 18
        Me._cmdsearchInvNO_0.TabStop = False
        Me._cmdsearchInvNO_0.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me._cmdsearchInvNO_0, "Search")
        Me._cmdsearchInvNO_0.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.Location = New System.Drawing.Point(206, 10)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(63, 37)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'cmdPDF
        '
        Me.cmdPDF.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPDF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPDF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPDF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPDF.Image = CType(resources.GetObject("cmdPDF.Image"), System.Drawing.Image)
        Me.cmdPDF.Location = New System.Drawing.Point(144, 10)
        Me.cmdPDF.Name = "cmdPDF"
        Me.cmdPDF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPDF.Size = New System.Drawing.Size(63, 37)
        Me.cmdPDF.TabIndex = 34
        Me.cmdPDF.Text = "&PDF"
        Me.cmdPDF.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPDF, "Print")
        Me.cmdPDF.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(82, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(63, 37)
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
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(20, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(63, 37)
        Me.cmdPrint.TabIndex = 4
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtPartyName)
        Me.Frame4.Controls.Add(Me.cmdSearchParty)
        Me.Frame4.Controls.Add(Me._optPartyName_0)
        Me.Frame4.Controls.Add(Me._optPartyName_1)
        Me.Frame4.Controls.Add(Me.lblInvoiceSeq)
        Me.Frame4.Controls.Add(Me._Lbl_4)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 84)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(319, 69)
        Me.Frame4.TabIndex = 25
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Party Name"
        '
        'txtPartyName
        '
        Me.txtPartyName.AcceptsReturn = True
        Me.txtPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPartyName.Location = New System.Drawing.Point(76, 40)
        Me.txtPartyName.MaxLength = 0
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyName.Size = New System.Drawing.Size(213, 20)
        Me.txtPartyName.TabIndex = 29
        '
        '_optPartyName_0
        '
        Me._optPartyName_0.AutoSize = True
        Me._optPartyName_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPartyName_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPartyName_0.Enabled = False
        Me._optPartyName_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPartyName_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPartyName.SetIndex(Me._optPartyName_0, CType(0, Short))
        Me._optPartyName_0.Location = New System.Drawing.Point(6, 18)
        Me._optPartyName_0.Name = "_optPartyName_0"
        Me._optPartyName_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPartyName_0.Size = New System.Drawing.Size(45, 18)
        Me._optPartyName_0.TabIndex = 27
        Me._optPartyName_0.TabStop = True
        Me._optPartyName_0.Text = "ALL"
        Me._optPartyName_0.UseVisualStyleBackColor = False
        '
        '_optPartyName_1
        '
        Me._optPartyName_1.AutoSize = True
        Me._optPartyName_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPartyName_1.Checked = True
        Me._optPartyName_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPartyName_1.Enabled = False
        Me._optPartyName_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPartyName_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPartyName.SetIndex(Me._optPartyName_1, CType(1, Short))
        Me._optPartyName_1.Location = New System.Drawing.Point(162, 18)
        Me._optPartyName_1.Name = "_optPartyName_1"
        Me._optPartyName_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPartyName_1.Size = New System.Drawing.Size(70, 18)
        Me._optPartyName_1.TabIndex = 26
        Me._optPartyName_1.TabStop = True
        Me._optPartyName_1.Text = "Particular"
        Me._optPartyName_1.UseVisualStyleBackColor = False
        '
        'lblInvoiceSeq
        '
        Me.lblInvoiceSeq.BackColor = System.Drawing.SystemColors.Control
        Me.lblInvoiceSeq.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblInvoiceSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceSeq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblInvoiceSeq.Location = New System.Drawing.Point(262, 12)
        Me.lblInvoiceSeq.Name = "lblInvoiceSeq"
        Me.lblInvoiceSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblInvoiceSeq.Size = New System.Drawing.Size(37, 17)
        Me.lblInvoiceSeq.TabIndex = 33
        Me.lblInvoiceSeq.Text = "9"
        '
        '_Lbl_4
        '
        Me._Lbl_4.AutoSize = True
        Me._Lbl_4.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_4, CType(4, Short))
        Me._Lbl_4.Location = New System.Drawing.Point(6, 43)
        Me._Lbl_4.Name = "_Lbl_4"
        Me._Lbl_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_4.Size = New System.Drawing.Size(65, 14)
        Me._Lbl_4.TabIndex = 30
        Me._Lbl_4.Text = "Party Name:"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._optOrderBy_1)
        Me.Frame2.Controls.Add(Me._optOrderBy_0)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 42)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(319, 41)
        Me.Frame2.TabIndex = 22
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order BY"
        '
        '_optOrderBy_1
        '
        Me._optOrderBy_1.AutoSize = True
        Me._optOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_1, CType(1, Short))
        Me._optOrderBy_1.Location = New System.Drawing.Point(162, 18)
        Me._optOrderBy_1.Name = "_optOrderBy_1"
        Me._optOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_1.Size = New System.Drawing.Size(80, 18)
        Me._optOrderBy_1.TabIndex = 24
        Me._optOrderBy_1.TabStop = True
        Me._optOrderBy_1.Text = "Party Name"
        Me._optOrderBy_1.UseVisualStyleBackColor = False
        '
        '_optOrderBy_0
        '
        Me._optOrderBy_0.AutoSize = True
        Me._optOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_0.Checked = True
        Me._optOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_0, CType(0, Short))
        Me._optOrderBy_0.Location = New System.Drawing.Point(6, 18)
        Me._optOrderBy_0.Name = "_optOrderBy_0"
        Me._optOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_0.Size = New System.Drawing.Size(75, 18)
        Me._optOrderBy_0.TabIndex = 23
        Me._optOrderBy_0.TabStop = True
        Me._optOrderBy_0.Text = "Invoice No"
        Me._optOrderBy_0.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkPrintAll)
        Me.FraAccount.Controls.Add(Me.cboInvType)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(0, -2)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(319, 43)
        Me.FraAccount.TabIndex = 0
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Book"
        '
        'chkPrintAll
        '
        Me.chkPrintAll.AutoSize = True
        Me.chkPrintAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrintAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrintAll.Enabled = False
        Me.chkPrintAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrintAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrintAll.Location = New System.Drawing.Point(272, 16)
        Me.chkPrintAll.Name = "chkPrintAll"
        Me.chkPrintAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrintAll.Size = New System.Drawing.Size(38, 18)
        Me.chkPrintAll.TabIndex = 31
        Me.chkPrintAll.Text = "All"
        Me.chkPrintAll.UseVisualStyleBackColor = False
        '
        'cboInvType
        '
        Me.cboInvType.BackColor = System.Drawing.SystemColors.Window
        Me.cboInvType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInvType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInvType.Enabled = False
        Me.cboInvType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboInvType.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.cboInvType.Location = New System.Drawing.Point(6, 14)
        Me.cboInvType.Name = "cboInvType"
        Me.cboInvType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInvType.Size = New System.Drawing.Size(264, 22)
        Me.cboInvType.Sorted = True
        Me.cboInvType.TabIndex = 1
        Me.cboInvType.TabStop = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optPrintRange_1)
        Me.Frame3.Controls.Add(Me._optPrintRange_0)
        Me.Frame3.Controls.Add(Me.FraVNoRange)
        Me.Frame3.Controls.Add(Me.FraDateRange)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 148)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(319, 115)
        Me.Frame3.TabIndex = 7
        Me.Frame3.TabStop = False
        '
        '_optPrintRange_1
        '
        Me._optPrintRange_1.AutoSize = True
        Me._optPrintRange_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPrintRange_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrintRange_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrintRange_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintRange.SetIndex(Me._optPrintRange_1, CType(1, Short))
        Me._optPrintRange_1.Location = New System.Drawing.Point(162, 14)
        Me._optPrintRange_1.Name = "_optPrintRange_1"
        Me._optPrintRange_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrintRange_1.Size = New System.Drawing.Size(93, 18)
        Me._optPrintRange_1.TabIndex = 17
        Me._optPrintRange_1.TabStop = True
        Me._optPrintRange_1.Text = "Invoice Range"
        Me._optPrintRange_1.UseVisualStyleBackColor = False
        '
        '_optPrintRange_0
        '
        Me._optPrintRange_0.AutoSize = True
        Me._optPrintRange_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPrintRange_0.Checked = True
        Me._optPrintRange_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPrintRange_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPrintRange_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPrintRange.SetIndex(Me._optPrintRange_0, CType(0, Short))
        Me._optPrintRange_0.Location = New System.Drawing.Point(4, 16)
        Me._optPrintRange_0.Name = "_optPrintRange_0"
        Me._optPrintRange_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPrintRange_0.Size = New System.Drawing.Size(81, 18)
        Me._optPrintRange_0.TabIndex = 16
        Me._optPrintRange_0.TabStop = True
        Me._optPrintRange_0.Text = "Date Range"
        Me._optPrintRange_0.UseVisualStyleBackColor = False
        '
        'FraVNoRange
        '
        Me.FraVNoRange.BackColor = System.Drawing.SystemColors.Control
        Me.FraVNoRange.Controls.Add(Me._cmdsearchInvNO_1)
        Me.FraVNoRange.Controls.Add(Me._cmdsearchInvNO_0)
        Me.FraVNoRange.Controls.Add(Me.txtInvNoFrom)
        Me.FraVNoRange.Controls.Add(Me.txtInvNoTo)
        Me.FraVNoRange.Controls.Add(Me._Lbl_3)
        Me.FraVNoRange.Controls.Add(Me._Lbl_2)
        Me.FraVNoRange.Enabled = False
        Me.FraVNoRange.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraVNoRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraVNoRange.Location = New System.Drawing.Point(154, 40)
        Me.FraVNoRange.Name = "FraVNoRange"
        Me.FraVNoRange.Padding = New System.Windows.Forms.Padding(0)
        Me.FraVNoRange.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraVNoRange.Size = New System.Drawing.Size(165, 75)
        Me.FraVNoRange.TabIndex = 11
        Me.FraVNoRange.TabStop = False
        Me.FraVNoRange.Text = "Invoice No  Range Wise"
        '
        'txtInvNoFrom
        '
        Me.txtInvNoFrom.AcceptsReturn = True
        Me.txtInvNoFrom.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvNoFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvNoFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvNoFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvNoFrom.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInvNoFrom.Location = New System.Drawing.Point(44, 20)
        Me.txtInvNoFrom.MaxLength = 0
        Me.txtInvNoFrom.Name = "txtInvNoFrom"
        Me.txtInvNoFrom.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvNoFrom.Size = New System.Drawing.Size(93, 20)
        Me.txtInvNoFrom.TabIndex = 13
        '
        'txtInvNoTo
        '
        Me.txtInvNoTo.AcceptsReturn = True
        Me.txtInvNoTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtInvNoTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInvNoTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInvNoTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInvNoTo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInvNoTo.Location = New System.Drawing.Point(44, 48)
        Me.txtInvNoTo.MaxLength = 0
        Me.txtInvNoTo.Name = "txtInvNoTo"
        Me.txtInvNoTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInvNoTo.Size = New System.Drawing.Size(93, 20)
        Me.txtInvNoTo.TabIndex = 12
        '
        '_Lbl_3
        '
        Me._Lbl_3.AutoSize = True
        Me._Lbl_3.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_3, CType(3, Short))
        Me._Lbl_3.Location = New System.Drawing.Point(6, 51)
        Me._Lbl_3.Name = "_Lbl_3"
        Me._Lbl_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_3.Size = New System.Drawing.Size(24, 14)
        Me._Lbl_3.TabIndex = 15
        Me._Lbl_3.Text = "To :"
        '
        '_Lbl_2
        '
        Me._Lbl_2.AutoSize = True
        Me._Lbl_2.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_2, CType(2, Short))
        Me._Lbl_2.Location = New System.Drawing.Point(6, 23)
        Me._Lbl_2.Name = "_Lbl_2"
        Me._Lbl_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_2.Size = New System.Drawing.Size(37, 14)
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
        Me.FraDateRange.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraDateRange.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraDateRange.Location = New System.Drawing.Point(0, 40)
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
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(46, 20)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(83, 20)
        Me.txtDateFrom.TabIndex = 20
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(46, 47)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(83, 20)
        Me.txtDateTo.TabIndex = 21
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(6, 23)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(37, 14)
        Me._Lbl_0.TabIndex = 10
        Me._Lbl_0.Text = "From :"
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(6, 51)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(24, 14)
        Me._Lbl_1.TabIndex = 9
        Me._Lbl_1.Text = "To :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.Report1)
        Me.Frame1.Controls.Add(Me.cmdExit)
        Me.Frame1.Controls.Add(Me.cmdPDF)
        Me.Frame1.Controls.Add(Me.ChkPaintPrint)
        Me.Frame1.Controls.Add(Me.CmdPreview)
        Me.Frame1.Controls.Add(Me.cmdPrint)
        Me.Frame1.Controls.Add(Me.lblBookType)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 258)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(319, 51)
        Me.Frame1.TabIndex = 2
        Me.Frame1.TabStop = False
        '
        'ChkPaintPrint
        '
        Me.ChkPaintPrint.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPaintPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPaintPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPaintPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPaintPrint.Location = New System.Drawing.Point(268, 16)
        Me.ChkPaintPrint.Name = "ChkPaintPrint"
        Me.ChkPaintPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPaintPrint.Size = New System.Drawing.Size(51, 25)
        Me.ChkPaintPrint.TabIndex = 32
        Me.ChkPaintPrint.Text = "Paint"
        Me.ChkPaintPrint.UseVisualStyleBackColor = False
        Me.ChkPaintPrint.Visible = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(278, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 35
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(12, 18)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(37, 17)
        Me.lblBookType.TabIndex = 6
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'cmdsearchInvNO
        '
        '
        'optPartyName
        '
        '
        'optPrintRange
        '
        '
        'frmPrintMultiInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(320, 309)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintMultiInvoice"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Multiple Export Invoice Printing"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.FraVNoRange.ResumeLayout(False)
        Me.FraVNoRange.PerformLayout()
        Me.FraDateRange.ResumeLayout(False)
        Me.FraDateRange.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdsearchInvNO, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPartyName, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPrintRange, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class