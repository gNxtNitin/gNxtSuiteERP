Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamGSTReversal
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
    Public WithEvents _optOrderBy_4 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_2 As System.Windows.Forms.RadioButton
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _OptSearch_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSearch_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSearch_0 As System.Windows.Forms.RadioButton
    Public WithEvents cmdFind As System.Windows.Forms.Button
    Public WithEvents txtSearch As System.Windows.Forms.TextBox
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents FraFront As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblView As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents OptSearch As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamGSTReversal))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdFind = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._optOrderBy_4 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_3 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_2 = New System.Windows.Forms.RadioButton()
        Me.FraFront = New System.Windows.Forms.GroupBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._OptSearch_2 = New System.Windows.Forms.RadioButton()
        Me._OptSearch_1 = New System.Windows.Forms.RadioButton()
        Me._OptSearch_0 = New System.Windows.Forms.RadioButton()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblView = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.OptSearch = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame4.SuspendLayout()
        Me.FraFront.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame6.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdFind
        '
        Me.cmdFind.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdFind.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdFind.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdFind.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdFind.Image = CType(resources.GetObject("cmdFind.Image"), System.Drawing.Image)
        Me.cmdFind.Location = New System.Drawing.Point(286, 14)
        Me.cmdFind.Name = "cmdFind"
        Me.cmdFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdFind.Size = New System.Drawing.Size(27, 19)
        Me.cmdFind.TabIndex = 27
        Me.cmdFind.TabStop = False
        Me.cmdFind.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdFind, "Search")
        Me.cmdFind.UseVisualStyleBackColor = False
        '
        'txtSearch
        '
        Me.txtSearch.AcceptsReturn = True
        Me.txtSearch.BackColor = System.Drawing.SystemColors.Window
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSearch.Location = New System.Drawing.Point(6, 14)
        Me.txtSearch.MaxLength = 0
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSearch.Size = New System.Drawing.Size(279, 19)
        Me.txtSearch.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.txtSearch, "Press F1 For Help")
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(6, 14)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(279, 19)
        Me.TxtAccount.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(286, 14)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearch.TabIndex = 16
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(4, 10)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 37)
        Me.cmdShow.TabIndex = 12
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(140, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 37)
        Me.cmdClose.TabIndex = 11
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._optOrderBy_4)
        Me.Frame4.Controls.Add(Me._optOrderBy_3)
        Me.Frame4.Controls.Add(Me._optOrderBy_0)
        Me.Frame4.Controls.Add(Me._optOrderBy_1)
        Me.Frame4.Controls.Add(Me._optOrderBy_2)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 412)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(325, 45)
        Me.Frame4.TabIndex = 18
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Order By"
        '
        '_optOrderBy_4
        '
        Me._optOrderBy_4.AutoSize = True
        Me._optOrderBy_4.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_4, CType(4, Short))
        Me._optOrderBy_4.Location = New System.Drawing.Point(266, 20)
        Me._optOrderBy_4.Name = "_optOrderBy_4"
        Me._optOrderBy_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_4.Size = New System.Drawing.Size(47, 18)
        Me._optOrderBy_4.TabIndex = 24
        Me._optOrderBy_4.TabStop = True
        Me._optOrderBy_4.Text = "MRR"
        Me._optOrderBy_4.UseVisualStyleBackColor = False
        '
        '_optOrderBy_3
        '
        Me._optOrderBy_3.AutoSize = True
        Me._optOrderBy_3.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_3, CType(3, Short))
        Me._optOrderBy_3.Location = New System.Drawing.Point(188, 20)
        Me._optOrderBy_3.Name = "_optOrderBy_3"
        Me._optOrderBy_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_3.Size = New System.Drawing.Size(66, 18)
        Me._optOrderBy_3.TabIndex = 23
        Me._optOrderBy_3.TabStop = True
        Me._optOrderBy_3.Text = "Claim No"
        Me._optOrderBy_3.UseVisualStyleBackColor = False
        '
        '_optOrderBy_0
        '
        Me._optOrderBy_0.AutoSize = True
        Me._optOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_0, CType(0, Short))
        Me._optOrderBy_0.Location = New System.Drawing.Point(2, 20)
        Me._optOrderBy_0.Name = "_optOrderBy_0"
        Me._optOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_0.Size = New System.Drawing.Size(64, 18)
        Me._optOrderBy_0.TabIndex = 21
        Me._optOrderBy_0.TabStop = True
        Me._optOrderBy_0.Text = "Supplier"
        Me._optOrderBy_0.UseVisualStyleBackColor = False
        '
        '_optOrderBy_1
        '
        Me._optOrderBy_1.AutoSize = True
        Me._optOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_1.Checked = True
        Me._optOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_1, CType(1, Short))
        Me._optOrderBy_1.Location = New System.Drawing.Point(76, 20)
        Me._optOrderBy_1.Name = "_optOrderBy_1"
        Me._optOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_1.Size = New System.Drawing.Size(54, 18)
        Me._optOrderBy_1.TabIndex = 20
        Me._optOrderBy_1.TabStop = True
        Me._optOrderBy_1.Text = "Bill No"
        Me._optOrderBy_1.UseVisualStyleBackColor = False
        '
        '_optOrderBy_2
        '
        Me._optOrderBy_2.AutoSize = True
        Me._optOrderBy_2.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_2, CType(2, Short))
        Me._optOrderBy_2.Location = New System.Drawing.Point(138, 20)
        Me._optOrderBy_2.Name = "_optOrderBy_2"
        Me._optOrderBy_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_2.Size = New System.Drawing.Size(46, 18)
        Me._optOrderBy_2.TabIndex = 19
        Me._optOrderBy_2.TabStop = True
        Me._optOrderBy_2.Text = "VNo"
        Me._optOrderBy_2.UseVisualStyleBackColor = False
        '
        'FraFront
        '
        Me.FraFront.BackColor = System.Drawing.SystemColors.Control
        Me.FraFront.Controls.Add(Me.Frame8)
        Me.FraFront.Controls.Add(Me.Frame3)
        Me.FraFront.Controls.Add(Me.Frame1)
        Me.FraFront.Controls.Add(Me.Frame2)
        Me.FraFront.Controls.Add(Me.lblBookType)
        Me.FraFront.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraFront.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraFront.Location = New System.Drawing.Point(0, -2)
        Me.FraFront.Name = "FraFront"
        Me.FraFront.Padding = New System.Windows.Forms.Padding(0)
        Me.FraFront.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraFront.Size = New System.Drawing.Size(751, 413)
        Me.FraFront.TabIndex = 1
        Me.FraFront.TabStop = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me._OptSearch_2)
        Me.Frame8.Controls.Add(Me._OptSearch_1)
        Me.Frame8.Controls.Add(Me._OptSearch_0)
        Me.Frame8.Controls.Add(Me.cmdFind)
        Me.Frame8.Controls.Add(Me.txtSearch)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(128, 40)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(557, 39)
        Me.Frame8.TabIndex = 25
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Search"
        '
        '_OptSearch_2
        '
        Me._OptSearch_2.AutoSize = True
        Me._OptSearch_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptSearch_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSearch_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSearch_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSearch.SetIndex(Me._OptSearch_2, CType(2, Short))
        Me._OptSearch_2.Location = New System.Drawing.Point(492, 16)
        Me._OptSearch_2.Name = "_OptSearch_2"
        Me._OptSearch_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSearch_2.Size = New System.Drawing.Size(46, 18)
        Me._OptSearch_2.TabIndex = 30
        Me._OptSearch_2.TabStop = True
        Me._OptSearch_2.Text = "VNo"
        Me._OptSearch_2.UseVisualStyleBackColor = False
        '
        '_OptSearch_1
        '
        Me._OptSearch_1.AutoSize = True
        Me._OptSearch_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSearch_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSearch_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSearch_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSearch.SetIndex(Me._OptSearch_1, CType(1, Short))
        Me._OptSearch_1.Location = New System.Drawing.Point(416, 16)
        Me._OptSearch_1.Name = "_OptSearch_1"
        Me._OptSearch_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSearch_1.Size = New System.Drawing.Size(54, 18)
        Me._OptSearch_1.TabIndex = 29
        Me._OptSearch_1.TabStop = True
        Me._OptSearch_1.Text = "Bill No"
        Me._OptSearch_1.UseVisualStyleBackColor = False
        '
        '_OptSearch_0
        '
        Me._OptSearch_0.AutoSize = True
        Me._OptSearch_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptSearch_0.Checked = True
        Me._OptSearch_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSearch_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSearch_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSearch.SetIndex(Me._OptSearch_0, CType(0, Short))
        Me._OptSearch_0.Location = New System.Drawing.Point(330, 16)
        Me._OptSearch_0.Name = "_OptSearch_0"
        Me._OptSearch_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSearch_0.Size = New System.Drawing.Size(63, 18)
        Me._OptSearch_0.TabIndex = 28
        Me._OptSearch_0.TabStop = True
        Me._OptSearch_0.Text = "MRR No"
        Me._OptSearch_0.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.TxtAccount)
        Me.Frame3.Controls.Add(Me.cmdSearch)
        Me.Frame3.Controls.Add(Me.chkAll)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(128, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(373, 39)
        Me.Frame3.TabIndex = 14
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Customer Name"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(318, 16)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 15
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtDateTo)
        Me.Frame1.Controls.Add(Me.txtDateFrom)
        Me.Frame1.Controls.Add(Me.Label8)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(127, 77)
        Me.Frame1.TabIndex = 3
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "As On Date"
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(42, 44)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(81, 20)
        Me.txtDateTo.TabIndex = 4
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(42, 16)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtDateFrom.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(5, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(37, 14)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "From :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(17, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(24, 14)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "To :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdMain)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 72)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(751, 341)
        Me.Frame2.TabIndex = 8
        Me.Frame2.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(4, 10)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(743, 327)
        Me.SprdMain.TabIndex = 9
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(440, 386)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(64, 14)
        Me.lblBookType.TabIndex = 2
        Me.lblBookType.Text = "lblBookType"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(751, 409)
        Me.SprdView.TabIndex = 0
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.cmdShow)
        Me.Frame6.Controls.Add(Me.cmdClose)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(538, 406)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(213, 51)
        Me.Frame6.TabIndex = 10
        Me.Frame6.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(2, 420)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 20
        '
        'lblView
        '
        Me.lblView.BackColor = System.Drawing.SystemColors.Control
        Me.lblView.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblView.Location = New System.Drawing.Point(462, 428)
        Me.lblView.Name = "lblView"
        Me.lblView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblView.Size = New System.Drawing.Size(51, 17)
        Me.lblView.TabIndex = 22
        Me.lblView.Text = "lblView"
        Me.lblView.Visible = False
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(2, 418)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 13
        Me.lblMKey.Text = "lblMKey"
        Me.lblMKey.Visible = False
        '
        'frmParamGSTReversal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(750, 458)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraFront)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.lblView)
        Me.Controls.Add(Me.lblMKey)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 15)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamGSTReversal"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "GST Claim Reversal Register"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.FraFront.ResumeLayout(False)
        Me.FraFront.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame6.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptSearch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdMain.DataSource = CType(Adata, MSDATASRC.DataSource)
        ''SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain.DataSource = Nothing
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class