Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmRejectionReg
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
    Public WithEvents _optDNStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optDNStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optDNStatus_2 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _optStatus_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents ChkALL As System.Windows.Forms.CheckBox
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents LblTotalAmt As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As VB6.LabelArray
    Public WithEvents optDNStatus As VB6.RadioButtonArray
    Public WithEvents optStatus As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRejectionReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optDNStatus_0 = New System.Windows.Forms.RadioButton()
        Me._optDNStatus_1 = New System.Windows.Forms.RadioButton()
        Me._optDNStatus_2 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optStatus_2 = New System.Windows.Forms.RadioButton()
        Me._optStatus_1 = New System.Windows.Forms.RadioButton()
        Me._optStatus_0 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.ChkALL = New System.Windows.Forms.CheckBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LblTotalAmt = New System.Windows.Forms.Label()
        Me.Lbl = New VB6.LabelArray(Me.components)
        Me.optDNStatus = New VB6.RadioButtonArray(Me.components)
        Me.optStatus = New VB6.RadioButtonArray(Me.components)
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optDNStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.TxtAccount.Location = New System.Drawing.Point(4, 28)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(341, 19)
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
        Me.cmdShow.TabIndex = 15
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
        Me.cmdClose.TabIndex = 18
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
        Me.cmdPrint.TabIndex = 16
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
        Me.CmdPreview.TabIndex = 17
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._optDNStatus_0)
        Me.Frame2.Controls.Add(Me._optDNStatus_1)
        Me.Frame2.Controls.Add(Me._optDNStatus_2)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(648, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(103, 65)
        Me.Frame2.TabIndex = 22
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Invoice Status"
        '
        '_optDNStatus_0
        '
        Me._optDNStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._optDNStatus_0.Checked = True
        Me._optDNStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDNStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDNStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDNStatus.SetIndex(Me._optDNStatus_0, CType(0, Short))
        Me._optDNStatus_0.Location = New System.Drawing.Point(8, 16)
        Me._optDNStatus_0.Name = "_optDNStatus_0"
        Me._optDNStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDNStatus_0.Size = New System.Drawing.Size(91, 16)
        Me._optDNStatus_0.TabIndex = 25
        Me._optDNStatus_0.TabStop = True
        Me._optDNStatus_0.Text = "All"
        Me._optDNStatus_0.UseVisualStyleBackColor = False
        '
        '_optDNStatus_1
        '
        Me._optDNStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._optDNStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDNStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDNStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDNStatus.SetIndex(Me._optDNStatus_1, CType(1, Short))
        Me._optDNStatus_1.Location = New System.Drawing.Point(8, 32)
        Me._optDNStatus_1.Name = "_optDNStatus_1"
        Me._optDNStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDNStatus_1.Size = New System.Drawing.Size(91, 16)
        Me._optDNStatus_1.TabIndex = 24
        Me._optDNStatus_1.TabStop = True
        Me._optDNStatus_1.Text = "Not Created"
        Me._optDNStatus_1.UseVisualStyleBackColor = False
        '
        '_optDNStatus_2
        '
        Me._optDNStatus_2.BackColor = System.Drawing.SystemColors.Control
        Me._optDNStatus_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDNStatus_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDNStatus_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDNStatus.SetIndex(Me._optDNStatus_2, CType(2, Short))
        Me._optDNStatus_2.Location = New System.Drawing.Point(8, 48)
        Me._optDNStatus_2.Name = "_optDNStatus_2"
        Me._optDNStatus_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDNStatus_2.Size = New System.Drawing.Size(91, 16)
        Me._optDNStatus_2.TabIndex = 23
        Me._optDNStatus_2.TabStop = True
        Me._optDNStatus_2.Text = "Created"
        Me._optDNStatus_2.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optStatus_2)
        Me.Frame1.Controls.Add(Me._optStatus_1)
        Me.Frame1.Controls.Add(Me._optStatus_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(544, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(103, 65)
        Me.Frame1.TabIndex = 21
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Material Status"
        '
        '_optStatus_2
        '
        Me._optStatus_2.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_2, CType(2, Short))
        Me._optStatus_2.Location = New System.Drawing.Point(8, 48)
        Me._optStatus_2.Name = "_optStatus_2"
        Me._optStatus_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_2.Size = New System.Drawing.Size(91, 16)
        Me._optStatus_2.TabIndex = 6
        Me._optStatus_2.TabStop = True
        Me._optStatus_2.Text = "Send"
        Me._optStatus_2.UseVisualStyleBackColor = False
        '
        '_optStatus_1
        '
        Me._optStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_1, CType(1, Short))
        Me._optStatus_1.Location = New System.Drawing.Point(8, 32)
        Me._optStatus_1.Name = "_optStatus_1"
        Me._optStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_1.Size = New System.Drawing.Size(91, 16)
        Me._optStatus_1.TabIndex = 5
        Me._optStatus_1.TabStop = True
        Me._optStatus_1.Text = "Not Send"
        Me._optStatus_1.UseVisualStyleBackColor = False
        '
        '_optStatus_0
        '
        Me._optStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_0.Checked = True
        Me._optStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_0, CType(0, Short))
        Me._optStatus_0.Location = New System.Drawing.Point(8, 16)
        Me._optStatus_0.Name = "_optStatus_0"
        Me._optStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_0.Size = New System.Drawing.Size(91, 16)
        Me._optStatus_0.TabIndex = 4
        Me._optStatus_0.TabStop = True
        Me._optStatus_0.Text = "All"
        Me._optStatus_0.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.ChkALL)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(138, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(405, 65)
        Me.FraAccount.TabIndex = 10
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
        Me.ChkALL.Location = New System.Drawing.Point(348, 28)
        Me.ChkALL.Name = "ChkALL"
        Me.ChkALL.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkALL.Size = New System.Drawing.Size(45, 19)
        Me.ChkALL.TabIndex = 3
        Me.ChkALL.Text = "ALL"
        Me.ChkALL.UseVisualStyleBackColor = False
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
        Me.Frame6.Size = New System.Drawing.Size(137, 65)
        Me.Frame6.TabIndex = 7
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(48, 16)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(83, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(48, 39)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(83, 20)
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
        Me._Lbl_1.Location = New System.Drawing.Point(6, 41)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(24, 14)
        Me._Lbl_1.TabIndex = 9
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
        Me._Lbl_0.Location = New System.Drawing.Point(6, 17)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(37, 14)
        Me._Lbl_0.TabIndex = 8
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
        Me.Frame4.Location = New System.Drawing.Point(0, 60)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(751, 311)
        Me.Frame4.TabIndex = 11
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(746, 301)
        Me.SprdMain.TabIndex = 12
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 13
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.FraMovement)
        Me.Frame3.Controls.Add(Me.Label1)
        Me.Frame3.Controls.Add(Me.LblTotalAmt)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 366)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(751, 91)
        Me.Frame3.TabIndex = 13
        Me.Frame3.TabStop = False
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
        Me.FraMovement.Location = New System.Drawing.Point(474, 38)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(277, 53)
        Me.FraMovement.TabIndex = 14
        Me.FraMovement.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(474, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(74, 14)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Total Amount :"
        '
        'LblTotalAmt
        '
        Me.LblTotalAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblTotalAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTotalAmt.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblTotalAmt.Location = New System.Drawing.Point(576, 16)
        Me.LblTotalAmt.Name = "LblTotalAmt"
        Me.LblTotalAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotalAmt.Size = New System.Drawing.Size(171, 21)
        Me.LblTotalAmt.TabIndex = 19
        Me.LblTotalAmt.Text = "LblTotalAmt"
        Me.LblTotalAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'optDNStatus
        '
        '
        'optStatus
        '
        '
        'frmRejectionReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(751, 458)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraAccount)
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
        Me.Name = "frmRejectionReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "QC Rejection Register"
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.FraAccount.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optDNStatus, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class