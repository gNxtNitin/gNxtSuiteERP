Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPendingMRR
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

        'InventoryGST.Master.Show
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
    Public WithEvents ChkALL As System.Windows.Forms.CheckBox
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents _Lbl_7 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents _optPending_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optPending_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optPending_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optPending_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _chkRefType_8 As System.Windows.Forms.CheckBox
    Public WithEvents _chkRefType_7 As System.Windows.Forms.CheckBox
    Public WithEvents _chkRefType_6 As System.Windows.Forms.CheckBox
    Public WithEvents _chkRefType_5 As System.Windows.Forms.CheckBox
    Public WithEvents _chkRefType_4 As System.Windows.Forms.CheckBox
    Public WithEvents _chkRefType_3 As System.Windows.Forms.CheckBox
    Public WithEvents _chkRefType_2 As System.Windows.Forms.CheckBox
    Public WithEvents _chkRefType_1 As System.Windows.Forms.CheckBox
    Public WithEvents _chkRefType_0 As System.Windows.Forms.CheckBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents LblTotalAmt As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As VB6.LabelArray
    Public WithEvents chkRefType As VB6.CheckBoxArray
    Public WithEvents optPending As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPendingMRR))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.ChkALL = New System.Windows.Forms.CheckBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me._Lbl_7 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optPending_3 = New System.Windows.Forms.RadioButton()
        Me._optPending_2 = New System.Windows.Forms.RadioButton()
        Me._optPending_1 = New System.Windows.Forms.RadioButton()
        Me._optPending_0 = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._chkRefType_8 = New System.Windows.Forms.CheckBox()
        Me._chkRefType_7 = New System.Windows.Forms.CheckBox()
        Me._chkRefType_6 = New System.Windows.Forms.CheckBox()
        Me._chkRefType_5 = New System.Windows.Forms.CheckBox()
        Me._chkRefType_4 = New System.Windows.Forms.CheckBox()
        Me._chkRefType_3 = New System.Windows.Forms.CheckBox()
        Me._chkRefType_2 = New System.Windows.Forms.CheckBox()
        Me._chkRefType_1 = New System.Windows.Forms.CheckBox()
        Me._chkRefType_0 = New System.Windows.Forms.CheckBox()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblTotalAmt = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.chkRefType = New Microsoft.VisualBasic.Compatibility.VB6.CheckBoxArray(Me.components)
        Me.optPending = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraAccount.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.chkRefType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optPending, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(4, 14)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(401, 22)
        Me.TxtAccount.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
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
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 16
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(206, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 19
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(71, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 17
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(139, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 18
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.ChkALL)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(162, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(550, 48)
        Me.FraAccount.TabIndex = 23
        Me.FraAccount.TabStop = False
        Me.FraAccount.Text = "Account"
        '
        'ChkALL
        '
        Me.ChkALL.BackColor = System.Drawing.SystemColors.Control
        Me.ChkALL.Checked = True
        Me.ChkALL.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkALL.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkALL.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkALL.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkALL.Location = New System.Drawing.Point(410, 14)
        Me.ChkALL.Name = "ChkALL"
        Me.ChkALL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkALL.Size = New System.Drawing.Size(45, 19)
        Me.ChkALL.TabIndex = 3
        Me.ChkALL.Text = "ALL"
        Me.ChkALL.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboDivision)
        Me.Frame7.Controls.Add(Me._Lbl_7)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(162, 43)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(550, 38)
        Me.Frame7.TabIndex = 31
        Me.Frame7.TabStop = False
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(66, 10)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(341, 21)
        Me.cboDivision.TabIndex = 32
        '
        '_Lbl_7
        '
        Me._Lbl_7.AutoSize = True
        Me._Lbl_7.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_7, CType(7, Short))
        Me._Lbl_7.Location = New System.Drawing.Point(8, 12)
        Me._Lbl_7.Name = "_Lbl_7"
        Me._Lbl_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_7.Size = New System.Drawing.Size(54, 13)
        Me._Lbl_7.TabIndex = 33
        Me._Lbl_7.Text = "Division :"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optPending_3)
        Me.Frame1.Controls.Add(Me._optPending_2)
        Me.Frame1.Controls.Add(Me._optPending_1)
        Me.Frame1.Controls.Add(Me._optPending_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(714, -1)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(198, 83)
        Me.Frame1.TabIndex = 29
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Pending"
        '
        '_optPending_3
        '
        Me._optPending_3.BackColor = System.Drawing.SystemColors.Control
        Me._optPending_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPending_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPending_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPending.SetIndex(Me._optPending_3, CType(3, Short))
        Me._optPending_3.Location = New System.Drawing.Point(105, 47)
        Me._optPending_3.Name = "_optPending_3"
        Me._optPending_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPending_3.Size = New System.Drawing.Size(67, 20)
        Me._optPending_3.TabIndex = 7
        Me._optPending_3.TabStop = True
        Me._optPending_3.Text = "QC"
        Me._optPending_3.UseVisualStyleBackColor = False
        '
        '_optPending_2
        '
        Me._optPending_2.BackColor = System.Drawing.SystemColors.Control
        Me._optPending_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPending_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPending_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPending.SetIndex(Me._optPending_2, CType(2, Short))
        Me._optPending_2.Location = New System.Drawing.Point(105, 22)
        Me._optPending_2.Name = "_optPending_2"
        Me._optPending_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPending_2.Size = New System.Drawing.Size(65, 20)
        Me._optPending_2.TabIndex = 6
        Me._optPending_2.TabStop = True
        Me._optPending_2.Text = "Store"
        Me._optPending_2.UseVisualStyleBackColor = False
        '
        '_optPending_1
        '
        Me._optPending_1.BackColor = System.Drawing.SystemColors.Control
        Me._optPending_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPending_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPending_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPending.SetIndex(Me._optPending_1, CType(1, Short))
        Me._optPending_1.Location = New System.Drawing.Point(19, 47)
        Me._optPending_1.Name = "_optPending_1"
        Me._optPending_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPending_1.Size = New System.Drawing.Size(77, 20)
        Me._optPending_1.TabIndex = 5
        Me._optPending_1.TabStop = True
        Me._optPending_1.Text = "Accounts"
        Me._optPending_1.UseVisualStyleBackColor = False
        '
        '_optPending_0
        '
        Me._optPending_0.BackColor = System.Drawing.SystemColors.Control
        Me._optPending_0.Checked = True
        Me._optPending_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optPending_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optPending_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPending.SetIndex(Me._optPending_0, CType(0, Short))
        Me._optPending_0.Location = New System.Drawing.Point(19, 22)
        Me._optPending_0.Name = "_optPending_0"
        Me._optPending_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optPending_0.Size = New System.Drawing.Size(77, 20)
        Me._optPending_0.TabIndex = 4
        Me._optPending_0.TabStop = True
        Me._optPending_0.Text = "All"
        Me._optPending_0.UseVisualStyleBackColor = False
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
        Me.Frame6.Size = New System.Drawing.Size(160, 80)
        Me.Frame6.TabIndex = 20
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(63, 14)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(89, 22)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(63, 44)
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
        Me._Lbl_1.Location = New System.Drawing.Point(34, 48)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(25, 13)
        Me._Lbl_1.TabIndex = 22
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
        Me._Lbl_0.Location = New System.Drawing.Point(19, 17)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(40, 13)
        Me._Lbl_0.TabIndex = 21
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
        Me.Frame4.Location = New System.Drawing.Point(0, 77)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(908, 447)
        Me.Frame4.TabIndex = 24
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(906, 436)
        Me.SprdMain.TabIndex = 8
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 9
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.Frame2)
        Me.Frame3.Controls.Add(Me.FraMovement)
        Me.Frame3.Controls.Add(Me.Label1)
        Me.Frame3.Controls.Add(Me.LblTotalAmt)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(1, 526)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(907, 91)
        Me.Frame3.TabIndex = 25
        Me.Frame3.TabStop = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._chkRefType_8)
        Me.Frame2.Controls.Add(Me._chkRefType_7)
        Me.Frame2.Controls.Add(Me._chkRefType_6)
        Me.Frame2.Controls.Add(Me._chkRefType_5)
        Me.Frame2.Controls.Add(Me._chkRefType_4)
        Me.Frame2.Controls.Add(Me._chkRefType_3)
        Me.Frame2.Controls.Add(Me._chkRefType_2)
        Me.Frame2.Controls.Add(Me._chkRefType_1)
        Me.Frame2.Controls.Add(Me._chkRefType_0)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(548, 90)
        Me.Frame2.TabIndex = 30
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Against Ref Type"
        '
        '_chkRefType_8
        '
        Me._chkRefType_8.BackColor = System.Drawing.SystemColors.Control
        Me._chkRefType_8.Checked = True
        Me._chkRefType_8.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkRefType_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkRefType_8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkRefType_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefType.SetIndex(Me._chkRefType_8, CType(8, Short))
        Me._chkRefType_8.Location = New System.Drawing.Point(360, 63)
        Me._chkRefType_8.Name = "_chkRefType_8"
        Me._chkRefType_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkRefType_8.Size = New System.Drawing.Size(159, 20)
        Me._chkRefType_8.TabIndex = 35
        Me._chkRefType_8.Text = "Job Work Return"
        Me._chkRefType_8.UseVisualStyleBackColor = False
        '
        '_chkRefType_7
        '
        Me._chkRefType_7.BackColor = System.Drawing.SystemColors.Control
        Me._chkRefType_7.Checked = True
        Me._chkRefType_7.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkRefType_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkRefType_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkRefType_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefType.SetIndex(Me._chkRefType_7, CType(7, Short))
        Me._chkRefType_7.Location = New System.Drawing.Point(360, 40)
        Me._chkRefType_7.Name = "_chkRefType_7"
        Me._chkRefType_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkRefType_7.Size = New System.Drawing.Size(159, 20)
        Me._chkRefType_7.TabIndex = 34
        Me._chkRefType_7.Text = "Sale Return (Warranty)"
        Me._chkRefType_7.UseVisualStyleBackColor = False
        '
        '_chkRefType_6
        '
        Me._chkRefType_6.BackColor = System.Drawing.SystemColors.Control
        Me._chkRefType_6.Checked = True
        Me._chkRefType_6.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkRefType_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkRefType_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkRefType_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefType.SetIndex(Me._chkRefType_6, CType(6, Short))
        Me._chkRefType_6.Location = New System.Drawing.Point(360, 18)
        Me._chkRefType_6.Name = "_chkRefType_6"
        Me._chkRefType_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkRefType_6.Size = New System.Drawing.Size(159, 20)
        Me._chkRefType_6.TabIndex = 15
        Me._chkRefType_6.Text = "Sale Return"
        Me._chkRefType_6.UseVisualStyleBackColor = False
        '
        '_chkRefType_5
        '
        Me._chkRefType_5.BackColor = System.Drawing.SystemColors.Control
        Me._chkRefType_5.Checked = True
        Me._chkRefType_5.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkRefType_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkRefType_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkRefType_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefType.SetIndex(Me._chkRefType_5, CType(5, Short))
        Me._chkRefType_5.Location = New System.Drawing.Point(176, 63)
        Me._chkRefType_5.Name = "_chkRefType_5"
        Me._chkRefType_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkRefType_5.Size = New System.Drawing.Size(135, 20)
        Me._chkRefType_5.TabIndex = 14
        Me._chkRefType_5.Text = "RGP"
        Me._chkRefType_5.UseVisualStyleBackColor = False
        '
        '_chkRefType_4
        '
        Me._chkRefType_4.BackColor = System.Drawing.SystemColors.Control
        Me._chkRefType_4.Checked = True
        Me._chkRefType_4.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkRefType_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkRefType_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkRefType_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefType.SetIndex(Me._chkRefType_4, CType(4, Short))
        Me._chkRefType_4.Location = New System.Drawing.Point(176, 40)
        Me._chkRefType_4.Name = "_chkRefType_4"
        Me._chkRefType_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkRefType_4.Size = New System.Drawing.Size(135, 20)
        Me._chkRefType_4.TabIndex = 13
        Me._chkRefType_4.Text = "Purchase Order"
        Me._chkRefType_4.UseVisualStyleBackColor = False
        '
        '_chkRefType_3
        '
        Me._chkRefType_3.BackColor = System.Drawing.SystemColors.Control
        Me._chkRefType_3.Checked = True
        Me._chkRefType_3.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkRefType_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkRefType_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkRefType_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefType.SetIndex(Me._chkRefType_3, CType(3, Short))
        Me._chkRefType_3.Location = New System.Drawing.Point(176, 18)
        Me._chkRefType_3.Name = "_chkRefType_3"
        Me._chkRefType_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkRefType_3.Size = New System.Drawing.Size(135, 20)
        Me._chkRefType_3.TabIndex = 12
        Me._chkRefType_3.Text = "Jobwork"
        Me._chkRefType_3.UseVisualStyleBackColor = False
        '
        '_chkRefType_2
        '
        Me._chkRefType_2.BackColor = System.Drawing.SystemColors.Control
        Me._chkRefType_2.Checked = True
        Me._chkRefType_2.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkRefType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkRefType_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkRefType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefType.SetIndex(Me._chkRefType_2, CType(2, Short))
        Me._chkRefType_2.Location = New System.Drawing.Point(6, 63)
        Me._chkRefType_2.Name = "_chkRefType_2"
        Me._chkRefType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkRefType_2.Size = New System.Drawing.Size(135, 20)
        Me._chkRefType_2.TabIndex = 11
        Me._chkRefType_2.Text = "FOC"
        Me._chkRefType_2.UseVisualStyleBackColor = False
        '
        '_chkRefType_1
        '
        Me._chkRefType_1.BackColor = System.Drawing.SystemColors.Control
        Me._chkRefType_1.Checked = True
        Me._chkRefType_1.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkRefType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkRefType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkRefType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefType.SetIndex(Me._chkRefType_1, CType(1, Short))
        Me._chkRefType_1.Location = New System.Drawing.Point(6, 40)
        Me._chkRefType_1.Name = "_chkRefType_1"
        Me._chkRefType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkRefType_1.Size = New System.Drawing.Size(135, 20)
        Me._chkRefType_1.TabIndex = 10
        Me._chkRefType_1.Text = "Delivery Schedule"
        Me._chkRefType_1.UseVisualStyleBackColor = False
        '
        '_chkRefType_0
        '
        Me._chkRefType_0.BackColor = System.Drawing.SystemColors.Control
        Me._chkRefType_0.Checked = True
        Me._chkRefType_0.CheckState = System.Windows.Forms.CheckState.Checked
        Me._chkRefType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._chkRefType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._chkRefType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRefType.SetIndex(Me._chkRefType_0, CType(0, Short))
        Me._chkRefType_0.Location = New System.Drawing.Point(6, 18)
        Me._chkRefType_0.Name = "_chkRefType_0"
        Me._chkRefType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._chkRefType_0.Size = New System.Drawing.Size(135, 20)
        Me._chkRefType_0.TabIndex = 9
        Me._chkRefType_0.Text = "Cash"
        Me._chkRefType_0.UseVisualStyleBackColor = False
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(624, 38)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(277, 53)
        Me.FraMovement.TabIndex = 26
        Me.FraMovement.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(644, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Total Amount :"
        '
        'LblTotalAmt
        '
        Me.LblTotalAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblTotalAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTotalAmt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblTotalAmt.Location = New System.Drawing.Point(730, 16)
        Me.LblTotalAmt.Name = "LblTotalAmt"
        Me.LblTotalAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotalAmt.Size = New System.Drawing.Size(171, 21)
        Me.LblTotalAmt.TabIndex = 27
        Me.LblTotalAmt.Text = "LblTotalAmt"
        Me.LblTotalAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkRefType
        '
        '
        'optPending
        '
        '
        'frmPendingMRR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPendingMRR"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Pending MRR"
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.chkRefType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optPending, System.ComponentModel.ISupportInitialize).EndInit()
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
#End Region
End Class