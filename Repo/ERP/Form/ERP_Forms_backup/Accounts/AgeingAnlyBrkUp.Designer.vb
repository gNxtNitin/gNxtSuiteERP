Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmAgeingAnlyBreakup
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
        'Me.MdiParent = AccountGST.Master

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
    Public WithEvents chkClearChq As System.Windows.Forms.CheckBox
    Public WithEvents txtPDCDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptDueDate_5 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDueDate_4 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDueDate_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDueDate_3 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDueDate_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDueDate_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents TxtGroup As System.Windows.Forms.TextBox
    Public WithEvents chkAllGroup As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents _Lbl_7 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents _OptSuppType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSuppType_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSuppType_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _OptSumDet_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSumDet_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents _OptShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents FraShow As System.Windows.Forms.GroupBox
    Public WithEvents chkHideZero As System.Windows.Forms.CheckBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents txtDays6 As System.Windows.Forms.TextBox
    Public WithEvents txtDays7 As System.Windows.Forms.TextBox
    Public WithEvents txtDays8 As System.Windows.Forms.TextBox
    Public WithEvents txtDays9 As System.Windows.Forms.TextBox
    Public WithEvents txtDays10 As System.Windows.Forms.TextBox
    Public WithEvents txtDays5 As System.Windows.Forms.TextBox
    Public WithEvents txtDays4 As System.Windows.Forms.TextBox
    Public WithEvents txtDays3 As System.Windows.Forms.TextBox
    Public WithEvents txtDays2 As System.Windows.Forms.TextBox
    Public WithEvents txtDays1 As System.Windows.Forms.TextBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents SprdAgeing As AxFPSpreadADO.AxfpSpread
    Public WithEvents AData1 As VB6.ADODC
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptDueDate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptSumDet As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptSuppType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAgeingAnlyBreakup))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.chkClearChq = New System.Windows.Forms.CheckBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtPDCDate = New System.Windows.Forms.MaskedTextBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me._OptDueDate_5 = New System.Windows.Forms.RadioButton()
        Me._OptDueDate_4 = New System.Windows.Forms.RadioButton()
        Me._OptDueDate_2 = New System.Windows.Forms.RadioButton()
        Me._OptDueDate_3 = New System.Windows.Forms.RadioButton()
        Me._OptDueDate_1 = New System.Windows.Forms.RadioButton()
        Me._OptDueDate_0 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.optGroupWise = New System.Windows.Forms.RadioButton()
        Me.optPartyWise = New System.Windows.Forms.RadioButton()
        Me.chkAllPerson = New System.Windows.Forms.CheckBox()
        Me.txtSalePerson = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.TxtGroup = New System.Windows.Forms.TextBox()
        Me.chkAllGroup = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.chkHideZero = New System.Windows.Forms.CheckBox()
        Me._Lbl_7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptSuppType_3 = New System.Windows.Forms.RadioButton()
        Me._OptSuppType_2 = New System.Windows.Forms.RadioButton()
        Me._OptSuppType_0 = New System.Windows.Forms.RadioButton()
        Me._OptSuppType_1 = New System.Windows.Forms.RadioButton()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me._OptSumDet_2 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_1 = New System.Windows.Forms.RadioButton()
        Me._OptSumDet_0 = New System.Windows.Forms.RadioButton()
        Me.FraShow = New System.Windows.Forms.GroupBox()
        Me._OptShow_2 = New System.Windows.Forms.RadioButton()
        Me._OptShow_0 = New System.Windows.Forms.RadioButton()
        Me._OptShow_1 = New System.Windows.Forms.RadioButton()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.txtDays6 = New System.Windows.Forms.TextBox()
        Me.txtDays7 = New System.Windows.Forms.TextBox()
        Me.txtDays8 = New System.Windows.Forms.TextBox()
        Me.txtDays9 = New System.Windows.Forms.TextBox()
        Me.txtDays10 = New System.Windows.Forms.TextBox()
        Me.txtDays5 = New System.Windows.Forms.TextBox()
        Me.txtDays4 = New System.Windows.Forms.TextBox()
        Me.txtDays3 = New System.Windows.Forms.TextBox()
        Me.txtDays2 = New System.Windows.Forms.TextBox()
        Me.txtDays1 = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdAgeing = New AxFPSpreadADO.AxfpSpread()
        Me.AData1 = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptDueDate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptSumDet = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptSuppType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.Frame5.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.FraShow.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdAgeing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptDueDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSuppType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(361, 10)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 22)
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
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(74, 12)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(286, 20)
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
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(131, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(63, 37)
        Me.CmdPreview.TabIndex = 10
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(67, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(63, 37)
        Me.cmdPrint.TabIndex = 9
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
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(194, 9)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(63, 37)
        Me.cmdClose.TabIndex = 11
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
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(63, 37)
        Me.cmdShow.TabIndex = 8
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'chkClearChq
        '
        Me.chkClearChq.BackColor = System.Drawing.SystemColors.Control
        Me.chkClearChq.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkClearChq.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkClearChq.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkClearChq.Location = New System.Drawing.Point(74, 136)
        Me.chkClearChq.Name = "chkClearChq"
        Me.chkClearChq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkClearChq.Size = New System.Drawing.Size(136, 19)
        Me.chkClearChq.TabIndex = 64
        Me.chkClearChq.Text = "After Clear Chqeue"
        Me.chkClearChq.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtPDCDate)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(135, 62)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(87, 62)
        Me.Frame5.TabIndex = 60
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "PDC As On Date"
        '
        'txtPDCDate
        '
        Me.txtPDCDate.AllowPromptAsInput = False
        Me.txtPDCDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPDCDate.Location = New System.Drawing.Point(5, 16)
        Me.txtPDCDate.Mask = "##/##/####"
        Me.txtPDCDate.Name = "txtPDCDate"
        Me.txtPDCDate.Size = New System.Drawing.Size(77, 20)
        Me.txtPDCDate.TabIndex = 61
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtDateTo)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(136, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(86, 159)
        Me.Frame3.TabIndex = 58
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "As On Date"
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(5, 21)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(77, 20)
        Me.txtDateTo.TabIndex = 59
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me._OptDueDate_5)
        Me.Frame6.Controls.Add(Me._OptDueDate_4)
        Me.Frame6.Controls.Add(Me._OptDueDate_2)
        Me.Frame6.Controls.Add(Me._OptDueDate_3)
        Me.Frame6.Controls.Add(Me._OptDueDate_1)
        Me.Frame6.Controls.Add(Me._OptDueDate_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(132, 159)
        Me.Frame6.TabIndex = 13
        Me.Frame6.TabStop = False
        '
        '_OptDueDate_5
        '
        Me._OptDueDate_5.AutoSize = True
        Me._OptDueDate_5.BackColor = System.Drawing.SystemColors.Control
        Me._OptDueDate_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDueDate_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDueDate_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDueDate.SetIndex(Me._OptDueDate_5, CType(5, Short))
        Me._OptDueDate_5.Location = New System.Drawing.Point(6, 130)
        Me._OptDueDate_5.Name = "_OptDueDate_5"
        Me._OptDueDate_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDueDate_5.Size = New System.Drawing.Size(99, 18)
        Me._OptDueDate_5.TabIndex = 57
        Me._OptDueDate_5.Text = "Commutative"
        Me._OptDueDate_5.UseVisualStyleBackColor = False
        Me._OptDueDate_5.Visible = False
        '
        '_OptDueDate_4
        '
        Me._OptDueDate_4.AutoSize = True
        Me._OptDueDate_4.BackColor = System.Drawing.SystemColors.Control
        Me._OptDueDate_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDueDate_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDueDate_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDueDate.SetIndex(Me._OptDueDate_4, CType(4, Short))
        Me._OptDueDate_4.Location = New System.Drawing.Point(6, 58)
        Me._OptDueDate_4.Name = "_OptDueDate_4"
        Me._OptDueDate_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDueDate_4.Size = New System.Drawing.Size(69, 18)
        Me._OptDueDate_4.TabIndex = 50
        Me._OptDueDate_4.Text = "Due(PO)"
        Me._OptDueDate_4.UseVisualStyleBackColor = False
        '
        '_OptDueDate_2
        '
        Me._OptDueDate_2.AutoSize = True
        Me._OptDueDate_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptDueDate_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDueDate_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDueDate_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDueDate.SetIndex(Me._OptDueDate_2, CType(2, Short))
        Me._OptDueDate_2.Location = New System.Drawing.Point(6, 82)
        Me._OptDueDate_2.Name = "_OptDueDate_2"
        Me._OptDueDate_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDueDate_2.Size = New System.Drawing.Size(98, 18)
        Me._OptDueDate_2.TabIndex = 45
        Me._OptDueDate_2.Text = "Over Due(PO)"
        Me._OptDueDate_2.UseVisualStyleBackColor = False
        Me._OptDueDate_2.Visible = False
        '
        '_OptDueDate_3
        '
        Me._OptDueDate_3.AutoSize = True
        Me._OptDueDate_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptDueDate_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDueDate_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDueDate_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDueDate.SetIndex(Me._OptDueDate_3, CType(3, Short))
        Me._OptDueDate_3.Location = New System.Drawing.Point(6, 106)
        Me._OptDueDate_3.Name = "_OptDueDate_3"
        Me._OptDueDate_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDueDate_3.Size = New System.Drawing.Size(125, 18)
        Me._OptDueDate_3.TabIndex = 44
        Me._OptDueDate_3.Text = "Over Due (Master)"
        Me._OptDueDate_3.UseVisualStyleBackColor = False
        Me._OptDueDate_3.Visible = False
        '
        '_OptDueDate_1
        '
        Me._OptDueDate_1.AutoSize = True
        Me._OptDueDate_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptDueDate_1.Checked = True
        Me._OptDueDate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDueDate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDueDate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDueDate.SetIndex(Me._OptDueDate_1, CType(1, Short))
        Me._OptDueDate_1.Location = New System.Drawing.Point(6, 34)
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
        Me._OptDueDate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDueDate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDueDate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDueDate.SetIndex(Me._OptDueDate_0, CType(0, Short))
        Me._OptDueDate_0.Location = New System.Drawing.Point(6, 10)
        Me._OptDueDate_0.Name = "_OptDueDate_0"
        Me._OptDueDate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDueDate_0.Size = New System.Drawing.Size(49, 18)
        Me._OptDueDate_0.TabIndex = 0
        Me._OptDueDate_0.Text = "MRR"
        Me._OptDueDate_0.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.optGroupWise)
        Me.FraAccount.Controls.Add(Me.optPartyWise)
        Me.FraAccount.Controls.Add(Me.chkAllPerson)
        Me.FraAccount.Controls.Add(Me.chkClearChq)
        Me.FraAccount.Controls.Add(Me.txtSalePerson)
        Me.FraAccount.Controls.Add(Me.Label13)
        Me.FraAccount.Controls.Add(Me.cboDivision)
        Me.FraAccount.Controls.Add(Me.TxtGroup)
        Me.FraAccount.Controls.Add(Me.chkAllGroup)
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.chkHideZero)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Controls.Add(Me._Lbl_7)
        Me.FraAccount.Controls.Add(Me.Label4)
        Me.FraAccount.Controls.Add(Me.Label3)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(225, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(437, 159)
        Me.FraAccount.TabIndex = 14
        Me.FraAccount.TabStop = False
        '
        'optGroupWise
        '
        Me.optGroupWise.AutoSize = True
        Me.optGroupWise.BackColor = System.Drawing.SystemColors.Control
        Me.optGroupWise.Cursor = System.Windows.Forms.Cursors.Default
        Me.optGroupWise.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optGroupWise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optGroupWise.Location = New System.Drawing.Point(224, 108)
        Me.optGroupWise.Name = "optGroupWise"
        Me.optGroupWise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optGroupWise.Size = New System.Drawing.Size(89, 18)
        Me.optGroupWise.TabIndex = 68
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
        Me.optPartyWise.Location = New System.Drawing.Point(74, 108)
        Me.optPartyWise.Name = "optPartyWise"
        Me.optPartyWise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPartyWise.Size = New System.Drawing.Size(83, 18)
        Me.optPartyWise.TabIndex = 67
        Me.optPartyWise.TabStop = True
        Me.optPartyWise.Text = "Party Wise"
        Me.optPartyWise.UseVisualStyleBackColor = False
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
        Me.chkAllPerson.Location = New System.Drawing.Point(388, 82)
        Me.chkAllPerson.Name = "chkAllPerson"
        Me.chkAllPerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllPerson.Size = New System.Drawing.Size(48, 18)
        Me.chkAllPerson.TabIndex = 65
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
        Me.txtSalePerson.Location = New System.Drawing.Point(74, 82)
        Me.txtSalePerson.MaxLength = 0
        Me.txtSalePerson.Name = "txtSalePerson"
        Me.txtSalePerson.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSalePerson.Size = New System.Drawing.Size(286, 20)
        Me.txtSalePerson.TabIndex = 64
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(20, 85)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(53, 14)
        Me.Label13.TabIndex = 66
        Me.Label13.Text = "Person :"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(74, 35)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(286, 22)
        Me.cboDivision.TabIndex = 62
        '
        'TxtGroup
        '
        Me.TxtGroup.AcceptsReturn = True
        Me.TxtGroup.BackColor = System.Drawing.SystemColors.Window
        Me.TxtGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtGroup.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtGroup.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGroup.ForeColor = System.Drawing.Color.Blue
        Me.TxtGroup.Location = New System.Drawing.Point(74, 60)
        Me.TxtGroup.MaxLength = 0
        Me.TxtGroup.Name = "TxtGroup"
        Me.TxtGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtGroup.Size = New System.Drawing.Size(286, 20)
        Me.TxtGroup.TabIndex = 52
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
        Me.chkAllGroup.Location = New System.Drawing.Point(388, 60)
        Me.chkAllGroup.Name = "chkAllGroup"
        Me.chkAllGroup.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllGroup.Size = New System.Drawing.Size(48, 18)
        Me.chkAllGroup.TabIndex = 51
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
        Me.chkAll.Location = New System.Drawing.Point(388, 13)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(48, 18)
        Me.chkAll.TabIndex = 18
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'chkHideZero
        '
        Me.chkHideZero.BackColor = System.Drawing.SystemColors.Control
        Me.chkHideZero.Checked = True
        Me.chkHideZero.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHideZero.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkHideZero.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkHideZero.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkHideZero.Location = New System.Drawing.Point(224, 134)
        Me.chkHideZero.Name = "chkHideZero"
        Me.chkHideZero.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHideZero.Size = New System.Drawing.Size(136, 21)
        Me.chkHideZero.TabIndex = 6
        Me.chkHideZero.Text = "Hide Zero Bal."
        Me.chkHideZero.UseVisualStyleBackColor = False
        '
        '_Lbl_7
        '
        Me._Lbl_7.AutoSize = True
        Me._Lbl_7.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_7, CType(7, Short))
        Me._Lbl_7.Location = New System.Drawing.Point(17, 38)
        Me._Lbl_7.Name = "_Lbl_7"
        Me._Lbl_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_7.Size = New System.Drawing.Size(56, 14)
        Me._Lbl_7.TabIndex = 63
        Me._Lbl_7.Text = "Division :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(26, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(47, 14)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Group :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Customer :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._OptSuppType_3)
        Me.Frame1.Controls.Add(Me._OptSuppType_2)
        Me.Frame1.Controls.Add(Me._OptSuppType_0)
        Me.Frame1.Controls.Add(Me._OptSuppType_1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(785, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(123, 96)
        Me.Frame1.TabIndex = 46
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Type"
        '
        '_OptSuppType_3
        '
        Me._OptSuppType_3.AutoSize = True
        Me._OptSuppType_3.BackColor = System.Drawing.SystemColors.Control
        Me._OptSuppType_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSuppType_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSuppType_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSuppType.SetIndex(Me._OptSuppType_3, CType(3, Short))
        Me._OptSuppType_3.Location = New System.Drawing.Point(4, 70)
        Me._OptSuppType_3.Name = "_OptSuppType_3"
        Me._OptSuppType_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSuppType_3.Size = New System.Drawing.Size(75, 18)
        Me._OptSuppType_3.TabIndex = 50
        Me._OptSuppType_3.TabStop = True
        Me._OptSuppType_3.Text = "Inter Unit"
        Me._OptSuppType_3.UseVisualStyleBackColor = False
        '
        '_OptSuppType_2
        '
        Me._OptSuppType_2.AutoSize = True
        Me._OptSuppType_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptSuppType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSuppType_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSuppType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSuppType.SetIndex(Me._OptSuppType_2, CType(2, Short))
        Me._OptSuppType_2.Location = New System.Drawing.Point(4, 51)
        Me._OptSuppType_2.Name = "_OptSuppType_2"
        Me._OptSuppType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSuppType_2.Size = New System.Drawing.Size(81, 18)
        Me._OptSuppType_2.TabIndex = 49
        Me._OptSuppType_2.TabStop = True
        Me._OptSuppType_2.Text = "Customer"
        Me._OptSuppType_2.UseVisualStyleBackColor = False
        '
        '_OptSuppType_0
        '
        Me._OptSuppType_0.AutoSize = True
        Me._OptSuppType_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptSuppType_0.Checked = True
        Me._OptSuppType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSuppType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSuppType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSuppType.SetIndex(Me._OptSuppType_0, CType(0, Short))
        Me._OptSuppType_0.Location = New System.Drawing.Point(4, 13)
        Me._OptSuppType_0.Name = "_OptSuppType_0"
        Me._OptSuppType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSuppType_0.Size = New System.Drawing.Size(39, 18)
        Me._OptSuppType_0.TabIndex = 48
        Me._OptSuppType_0.TabStop = True
        Me._OptSuppType_0.Text = "All"
        Me._OptSuppType_0.UseVisualStyleBackColor = False
        '
        '_OptSuppType_1
        '
        Me._OptSuppType_1.AutoSize = True
        Me._OptSuppType_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSuppType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSuppType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSuppType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSuppType.SetIndex(Me._OptSuppType_1, CType(1, Short))
        Me._OptSuppType_1.Location = New System.Drawing.Point(4, 32)
        Me._OptSuppType_1.Name = "_OptSuppType_1"
        Me._OptSuppType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSuppType_1.Size = New System.Drawing.Size(71, 18)
        Me._OptSuppType_1.TabIndex = 47
        Me._OptSuppType_1.TabStop = True
        Me._OptSuppType_1.Text = "Supplier"
        Me._OptSuppType_1.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me._OptSumDet_2)
        Me.Frame7.Controls.Add(Me._OptSumDet_1)
        Me.Frame7.Controls.Add(Me._OptSumDet_0)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(666, 95)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(243, 63)
        Me.Frame7.TabIndex = 15
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Format"
        '
        '_OptSumDet_2
        '
        Me._OptSumDet_2.AutoSize = True
        Me._OptSumDet_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_2, CType(2, Short))
        Me._OptSumDet_2.Location = New System.Drawing.Point(83, 16)
        Me._OptSumDet_2.Name = "_OptSumDet_2"
        Me._OptSumDet_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_2.Size = New System.Drawing.Size(162, 18)
        Me._OptSumDet_2.TabIndex = 6
        Me._OptSumDet_2.TabStop = True
        Me._OptSumDet_2.Text = "Summarised - Customer"
        Me._OptSumDet_2.UseVisualStyleBackColor = False
        '
        '_OptSumDet_1
        '
        Me._OptSumDet_1.AutoSize = True
        Me._OptSumDet_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSumDet_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSumDet_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSumDet_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSumDet.SetIndex(Me._OptSumDet_1, CType(1, Short))
        Me._OptSumDet_1.Location = New System.Drawing.Point(9, 34)
        Me._OptSumDet_1.Name = "_OptSumDet_1"
        Me._OptSumDet_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_1.Size = New System.Drawing.Size(127, 18)
        Me._OptSumDet_1.TabIndex = 5
        Me._OptSumDet_1.TabStop = True
        Me._OptSumDet_1.Text = "Summarised - Unit"
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
        Me._OptSumDet_0.Location = New System.Drawing.Point(9, 16)
        Me._OptSumDet_0.Name = "_OptSumDet_0"
        Me._OptSumDet_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSumDet_0.Size = New System.Drawing.Size(69, 18)
        Me._OptSumDet_0.TabIndex = 4
        Me._OptSumDet_0.TabStop = True
        Me._OptSumDet_0.Text = "Detailed"
        Me._OptSumDet_0.UseVisualStyleBackColor = False
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
        Me.FraShow.Location = New System.Drawing.Point(665, 0)
        Me.FraShow.Name = "FraShow"
        Me.FraShow.Padding = New System.Windows.Forms.Padding(0)
        Me.FraShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraShow.Size = New System.Drawing.Size(117, 96)
        Me.FraShow.TabIndex = 19
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
        Me._OptShow_2.Location = New System.Drawing.Point(28, 70)
        Me._OptShow_2.Name = "_OptShow_2"
        Me._OptShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_2.Size = New System.Drawing.Size(59, 18)
        Me._OptShow_2.TabIndex = 22
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
        Me._OptShow_0.Location = New System.Drawing.Point(28, 14)
        Me._OptShow_0.Name = "_OptShow_0"
        Me._OptShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_0.Size = New System.Drawing.Size(39, 18)
        Me._OptShow_0.TabIndex = 21
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
        Me._OptShow_1.Location = New System.Drawing.Point(28, 42)
        Me._OptShow_1.Name = "_OptShow_1"
        Me._OptShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_1.Size = New System.Drawing.Size(53, 18)
        Me._OptShow_1.TabIndex = 20
        Me._OptShow_1.TabStop = True
        Me._OptShow_1.Text = "Debit"
        Me._OptShow_1.UseVisualStyleBackColor = False
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
        Me.FraMovement.Location = New System.Drawing.Point(793, 561)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(261, 49)
        Me.FraMovement.TabIndex = 16
        Me.FraMovement.TabStop = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.txtDays6)
        Me.Frame8.Controls.Add(Me.txtDays7)
        Me.Frame8.Controls.Add(Me.txtDays8)
        Me.Frame8.Controls.Add(Me.txtDays9)
        Me.Frame8.Controls.Add(Me.txtDays10)
        Me.Frame8.Controls.Add(Me.txtDays5)
        Me.Frame8.Controls.Add(Me.txtDays4)
        Me.Frame8.Controls.Add(Me.txtDays3)
        Me.Frame8.Controls.Add(Me.txtDays2)
        Me.Frame8.Controls.Add(Me.txtDays1)
        Me.Frame8.Controls.Add(Me.Label12)
        Me.Frame8.Controls.Add(Me.Label11)
        Me.Frame8.Controls.Add(Me.Label10)
        Me.Frame8.Controls.Add(Me.Label9)
        Me.Frame8.Controls.Add(Me.Label8)
        Me.Frame8.Controls.Add(Me.Label7)
        Me.Frame8.Controls.Add(Me.Label6)
        Me.Frame8.Controls.Add(Me.Label5)
        Me.Frame8.Controls.Add(Me.Label1)
        Me.Frame8.Controls.Add(Me.Label2)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 560)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(526, 51)
        Me.Frame8.TabIndex = 23
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Days Category"
        '
        'txtDays6
        '
        Me.txtDays6.AcceptsReturn = True
        Me.txtDays6.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays6.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays6.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays6.Location = New System.Drawing.Point(262, 28)
        Me.txtDays6.MaxLength = 4
        Me.txtDays6.Name = "txtDays6"
        Me.txtDays6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays6.Size = New System.Drawing.Size(40, 20)
        Me.txtDays6.TabIndex = 38
        '
        'txtDays7
        '
        Me.txtDays7.AcceptsReturn = True
        Me.txtDays7.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays7.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays7.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays7.Location = New System.Drawing.Point(314, 28)
        Me.txtDays7.MaxLength = 4
        Me.txtDays7.Name = "txtDays7"
        Me.txtDays7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays7.Size = New System.Drawing.Size(40, 20)
        Me.txtDays7.TabIndex = 37
        '
        'txtDays8
        '
        Me.txtDays8.AcceptsReturn = True
        Me.txtDays8.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays8.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays8.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays8.Location = New System.Drawing.Point(366, 28)
        Me.txtDays8.MaxLength = 4
        Me.txtDays8.Name = "txtDays8"
        Me.txtDays8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays8.Size = New System.Drawing.Size(40, 20)
        Me.txtDays8.TabIndex = 36
        '
        'txtDays9
        '
        Me.txtDays9.AcceptsReturn = True
        Me.txtDays9.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays9.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays9.Location = New System.Drawing.Point(418, 28)
        Me.txtDays9.MaxLength = 4
        Me.txtDays9.Name = "txtDays9"
        Me.txtDays9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays9.Size = New System.Drawing.Size(40, 20)
        Me.txtDays9.TabIndex = 35
        '
        'txtDays10
        '
        Me.txtDays10.AcceptsReturn = True
        Me.txtDays10.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays10.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays10.Location = New System.Drawing.Point(470, 28)
        Me.txtDays10.MaxLength = 4
        Me.txtDays10.Name = "txtDays10"
        Me.txtDays10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays10.Size = New System.Drawing.Size(40, 20)
        Me.txtDays10.TabIndex = 34
        '
        'txtDays5
        '
        Me.txtDays5.AcceptsReturn = True
        Me.txtDays5.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays5.Location = New System.Drawing.Point(210, 28)
        Me.txtDays5.MaxLength = 4
        Me.txtDays5.Name = "txtDays5"
        Me.txtDays5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays5.Size = New System.Drawing.Size(40, 20)
        Me.txtDays5.TabIndex = 32
        '
        'txtDays4
        '
        Me.txtDays4.AcceptsReturn = True
        Me.txtDays4.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays4.Location = New System.Drawing.Point(158, 28)
        Me.txtDays4.MaxLength = 4
        Me.txtDays4.Name = "txtDays4"
        Me.txtDays4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays4.Size = New System.Drawing.Size(40, 20)
        Me.txtDays4.TabIndex = 29
        '
        'txtDays3
        '
        Me.txtDays3.AcceptsReturn = True
        Me.txtDays3.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays3.Location = New System.Drawing.Point(106, 28)
        Me.txtDays3.MaxLength = 4
        Me.txtDays3.Name = "txtDays3"
        Me.txtDays3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays3.Size = New System.Drawing.Size(40, 20)
        Me.txtDays3.TabIndex = 28
        '
        'txtDays2
        '
        Me.txtDays2.AcceptsReturn = True
        Me.txtDays2.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays2.Location = New System.Drawing.Point(54, 28)
        Me.txtDays2.MaxLength = 4
        Me.txtDays2.Name = "txtDays2"
        Me.txtDays2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays2.Size = New System.Drawing.Size(40, 20)
        Me.txtDays2.TabIndex = 25
        '
        'txtDays1
        '
        Me.txtDays1.AcceptsReturn = True
        Me.txtDays1.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays1.Location = New System.Drawing.Point(2, 28)
        Me.txtDays1.MaxLength = 4
        Me.txtDays1.Name = "txtDays1"
        Me.txtDays1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays1.Size = New System.Drawing.Size(40, 20)
        Me.txtDays1.TabIndex = 24
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(317, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(35, 14)
        Me.Label12.TabIndex = 43
        Me.Label12.Text = "Day 7"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(264, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(35, 14)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Day 6"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(420, 14)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(35, 14)
        Me.Label10.TabIndex = 41
        Me.Label10.Text = "Day 9"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(368, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(35, 14)
        Me.Label9.TabIndex = 40
        Me.Label9.Text = "Day 8"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(471, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(41, 14)
        Me.Label8.TabIndex = 39
        Me.Label8.Text = "Day 10"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(213, 14)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(35, 14)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Day 5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(109, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(35, 14)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Day 3"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(160, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(35, 14)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "Day 4"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(35, 14)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Day 1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(56, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(35, 14)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Day 2"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdAgeing)
        Me.Frame4.Controls.Add(Me.AData1)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 155)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1055, 401)
        Me.Frame4.TabIndex = 12
        Me.Frame4.TabStop = False
        '
        'SprdAgeing
        '
        Me.SprdAgeing.DataSource = Nothing
        Me.SprdAgeing.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdAgeing.Location = New System.Drawing.Point(0, 13)
        Me.SprdAgeing.Name = "SprdAgeing"
        Me.SprdAgeing.OcxState = CType(resources.GetObject("SprdAgeing.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdAgeing.Size = New System.Drawing.Size(1055, 388)
        Me.SprdAgeing.TabIndex = 7
        '
        'AData1
        '
        Me.AData1.BackColor = System.Drawing.SystemColors.Window
        Me.AData1.CommandTimeout = 0
        Me.AData1.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AData1.ConnectionString = Nothing
        Me.AData1.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AData1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AData1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AData1.Location = New System.Drawing.Point(136, -24)
        Me.AData1.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AData1.Name = "AData1"
        Me.AData1.Size = New System.Drawing.Size(113, 23)
        Me.AData1.TabIndex = 8
        Me.AData1.Text = "Adodc1"
        Me.AData1.Visible = False
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
        'OptSumDet
        '
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(913, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(142, 159)
        Me.GroupBox1.TabIndex = 65
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
        Me.lstCompanyName.Size = New System.Drawing.Size(142, 146)
        Me.lstCompanyName.TabIndex = 2
        '
        'frmAgeingAnlyBreakup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1057, 611)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.FraShow)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame4)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAgeingAnlyBreakup"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Age Wise (Break-up Wise)"
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.FraShow.ResumeLayout(False)
        Me.FraShow.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdAgeing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptDueDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSumDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSuppType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdAgeing.DataSource = CType(AData1, msdatasrc.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdAgeing.DataSource = Nothing
    End Sub

    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
    Public WithEvents chkAllPerson As CheckBox
    Public WithEvents txtSalePerson As TextBox
    Public WithEvents Label13 As Label
    Public WithEvents _OptSuppType_3 As RadioButton
    Public WithEvents _OptSumDet_2 As RadioButton
    Public WithEvents optGroupWise As RadioButton
    Public WithEvents optPartyWise As RadioButton
#End Region
End Class