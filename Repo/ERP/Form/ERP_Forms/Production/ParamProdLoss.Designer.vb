Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamProdLoss
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
        'Me.MDIParent = Production.Master

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
    Public WithEvents _OptShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboReason As System.Windows.Forms.ComboBox
    Public WithEvents txtEmpName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchEmp As System.Windows.Forms.Button
    Public WithEvents chkAllEmp As System.Windows.Forms.CheckBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
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
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents OptOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamProdLoss))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtEmpName = New System.Windows.Forms.TextBox()
        Me.cmdSearchEmp = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._OptShow_1 = New System.Windows.Forms.RadioButton()
        Me._OptShow_0 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.cboReason = New System.Windows.Forms.ComboBox()
        Me.chkAllEmp = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtEmpName
        '
        Me.txtEmpName.AcceptsReturn = True
        Me.txtEmpName.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpName.Location = New System.Drawing.Point(98, 16)
        Me.txtEmpName.MaxLength = 0
        Me.txtEmpName.Name = "txtEmpName"
        Me.txtEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpName.Size = New System.Drawing.Size(287, 19)
        Me.txtEmpName.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.txtEmpName, "Press F1 For Help")
        '
        'cmdSearchEmp
        '
        Me.cmdSearchEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchEmp.Image = CType(resources.GetObject("cmdSearchEmp.Image"), System.Drawing.Image)
        Me.cmdSearchEmp.Location = New System.Drawing.Point(388, 16)
        Me.cmdSearchEmp.Name = "cmdSearchEmp"
        Me.cmdSearchEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchEmp.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchEmp.TabIndex = 19
        Me.cmdSearchEmp.TabStop = False
        Me.cmdSearchEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchEmp, "Search")
        Me.cmdSearchEmp.UseVisualStyleBackColor = False
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
        Me.CmdPreview.TabIndex = 5
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
        Me.cmdPrint.TabIndex = 4
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
        Me.cmdClose.TabIndex = 6
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
        Me.cmdShow.TabIndex = 3
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._OptShow_1)
        Me.Frame3.Controls.Add(Me._OptShow_0)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(164, 416)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(187, 43)
        Me.Frame3.TabIndex = 24
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Show"
        '
        '_OptShow_1
        '
        Me._OptShow_1.AutoSize = True
        Me._OptShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_1, CType(1, Short))
        Me._OptShow_1.Location = New System.Drawing.Point(100, 20)
        Me._OptShow_1.Name = "_OptShow_1"
        Me._OptShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_1.Size = New System.Drawing.Size(70, 18)
        Me._OptShow_1.TabIndex = 26
        Me._OptShow_1.TabStop = True
        Me._OptShow_1.Text = "Summary"
        Me._OptShow_1.UseVisualStyleBackColor = False
        '
        '_OptShow_0
        '
        Me._OptShow_0.AutoSize = True
        Me._OptShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_0.Checked = True
        Me._OptShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_0, CType(0, Short))
        Me._OptShow_0.Location = New System.Drawing.Point(10, 20)
        Me._OptShow_0.Name = "_OptShow_0"
        Me._OptShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_0.Size = New System.Drawing.Size(51, 18)
        Me._OptShow_0.TabIndex = 25
        Me._OptShow_0.TabStop = True
        Me._OptShow_0.Text = "Detail"
        Me._OptShow_0.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboDept)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(592, -2)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(157, 73)
        Me.Frame1.TabIndex = 16
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Department"
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(8, 24)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(143, 22)
        Me.cboDept.TabIndex = 17
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 416)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(163, 43)
        Me.Frame2.TabIndex = 13
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order By"
        '
        '_OptOrderBy_0
        '
        Me._OptOrderBy_0.AutoSize = True
        Me._OptOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_0.Checked = True
        Me._OptOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_0, CType(0, Short))
        Me._OptOrderBy_0.Location = New System.Drawing.Point(10, 20)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(47, 18)
        Me._OptOrderBy_0.TabIndex = 15
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "Date"
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_1
        '
        Me._OptOrderBy_1.AutoSize = True
        Me._OptOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_1, CType(1, Short))
        Me._OptOrderBy_1.Location = New System.Drawing.Point(72, 20)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(73, 18)
        Me._OptOrderBy_1.TabIndex = 14
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Emp Code"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.cboReason)
        Me.FraAccount.Controls.Add(Me.txtEmpName)
        Me.FraAccount.Controls.Add(Me.cmdSearchEmp)
        Me.FraAccount.Controls.Add(Me.chkAllEmp)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(122, -2)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(469, 73)
        Me.FraAccount.TabIndex = 10
        Me.FraAccount.TabStop = False
        '
        'cboReason
        '
        Me.cboReason.BackColor = System.Drawing.SystemColors.Window
        Me.cboReason.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboReason.Location = New System.Drawing.Point(98, 40)
        Me.cboReason.Name = "cboReason"
        Me.cboReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboReason.Size = New System.Drawing.Size(287, 22)
        Me.cboReason.TabIndex = 23
        '
        'chkAllEmp
        '
        Me.chkAllEmp.AutoSize = True
        Me.chkAllEmp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllEmp.Checked = True
        Me.chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllEmp.Location = New System.Drawing.Point(420, 18)
        Me.chkAllEmp.Name = "chkAllEmp"
        Me.chkAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllEmp.Size = New System.Drawing.Size(46, 18)
        Me.chkAllEmp.TabIndex = 18
        Me.chkAllEmp.Text = "ALL"
        Me.chkAllEmp.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(14, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(77, 14)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Loss Reason :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(14, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(59, 14)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Employee :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, -2)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(121, 73)
        Me.Frame6.TabIndex = 7
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(42, 16)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(42, 40)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(75, 20)
        Me.txtDateTo.TabIndex = 1
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(4, 42)
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
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 20)
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
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 66)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(749, 349)
        Me.Frame4.TabIndex = 11
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(744, 337)
        Me.SprdMain.TabIndex = 2
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 3
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
        Me.FraMovement.Location = New System.Drawing.Point(502, 410)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 49)
        Me.FraMovement.TabIndex = 12
        Me.FraMovement.TabStop = False
        '
        'frmParamProdLoss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 459)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame2)
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
        Me.Name = "frmParamProdLoss"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Worker's Production Loss Register"
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).EndInit()
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