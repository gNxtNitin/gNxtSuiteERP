Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamGSTR1
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
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents SprdMain5A As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents SprdMain6 As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents SprdMain6A As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents SprdMain7 As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents SprdMain7A As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents SprdMain8 As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents SprdMain8A As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage7 As System.Windows.Forms.TabPage
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents SprdMain9 As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage8 As System.Windows.Forms.TabPage
    Public WithEvents SprdMain10 As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents _SSTab1_TabPage9 As System.Windows.Forms.TabPage
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents SprdMain11 As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage10 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblAcCode As System.Windows.Forms.Label
    Public WithEvents lblTrnType As System.Windows.Forms.Label
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamGSTR1))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SprdMain5A = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SprdMain6 = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage3 = New System.Windows.Forms.TabPage()
        Me.SprdMain6A = New AxFPSpreadADO.AxfpSpread()
        Me.Label4 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage4 = New System.Windows.Forms.TabPage()
        Me.SprdMain7 = New AxFPSpreadADO.AxfpSpread()
        Me.Label5 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage5 = New System.Windows.Forms.TabPage()
        Me.SprdMain7A = New AxFPSpreadADO.AxfpSpread()
        Me.Label6 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage6 = New System.Windows.Forms.TabPage()
        Me.SprdMain8 = New AxFPSpreadADO.AxfpSpread()
        Me.Label7 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage7 = New System.Windows.Forms.TabPage()
        Me.SprdMain8A = New AxFPSpreadADO.AxfpSpread()
        Me.Label8 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage8 = New System.Windows.Forms.TabPage()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.SprdMain9 = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage9 = New System.Windows.Forms.TabPage()
        Me.SprdMain10 = New AxFPSpreadADO.AxfpSpread()
        Me.Label11 = New System.Windows.Forms.Label()
        Me._SSTab1_TabPage10 = New System.Windows.Forms.TabPage()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.SprdMain11 = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.lblTrnType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.cboGSTNO = New System.Windows.Forms.ComboBox()
        Me._Lbl_7 = New System.Windows.Forms.Label()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        CType(Me.SprdMain5A, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage2.SuspendLayout()
        CType(Me.SprdMain6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage3.SuspendLayout()
        CType(Me.SprdMain6A, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage4.SuspendLayout()
        CType(Me.SprdMain7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage5.SuspendLayout()
        CType(Me.SprdMain7A, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage6.SuspendLayout()
        CType(Me.SprdMain8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage7.SuspendLayout()
        CType(Me.SprdMain8A, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage8.SuspendLayout()
        CType(Me.SprdMain9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage9.SuspendLayout()
        CType(Me.SprdMain10, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage10.SuspendLayout()
        CType(Me.SprdMain11, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.CmdPreview.TabIndex = 4
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
        Me.cmdPrint.TabIndex = 3
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
        Me.cmdClose.TabIndex = 5
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
        Me.cmdShow.TabIndex = 2
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(223, 44)
        Me.Frame6.TabIndex = 6
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(44, 14)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 20)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(144, 14)
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
        Me._Lbl_1.Location = New System.Drawing.Point(120, 18)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(24, 14)
        Me._Lbl_1.TabIndex = 8
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
        Me._Lbl_0.Location = New System.Drawing.Point(4, 18)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(37, 14)
        Me._Lbl_0.TabIndex = 7
        Me._Lbl_0.Text = "From :"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SSTab1)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 47)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1104, 506)
        Me.Frame4.TabIndex = 9
        Me.Frame4.TabStop = False
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage2)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage3)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage4)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage5)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage6)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage7)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage8)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage9)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage10)
        Me.SSTab1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 20)
        Me.SSTab1.Location = New System.Drawing.Point(0, 13)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 8
        Me.SSTab1.Size = New System.Drawing.Size(1104, 493)
        Me.SSTab1.TabIndex = 13
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.Label1)
        Me._SSTab1_TabPage0.Controls.Add(Me.SprdMain)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "B2B"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(4, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(724, 17)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "GSTR1 B2B Invoices: B2 invoices are raised for outward supply of goods or service" &
    "s to a registered customer. "
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 24)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1090, 438)
        Me.SprdMain.TabIndex = 14
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.Label2)
        Me._SSTab1_TabPage1.Controls.Add(Me.SprdMain5A)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "B2CL"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(4, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(724, 35)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "GSTR1 B2CL Invoices: B2C large invoices are raised for outward supply of goods or" &
    " services to a non registered customer. "
        '
        'SprdMain5A
        '
        Me.SprdMain5A.DataSource = Nothing
        Me.SprdMain5A.Location = New System.Drawing.Point(0, 42)
        Me.SprdMain5A.Name = "SprdMain5A"
        Me.SprdMain5A.OcxState = CType(resources.GetObject("SprdMain5A.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain5A.Size = New System.Drawing.Size(1094, 420)
        Me.SprdMain5A.TabIndex = 16
        '
        '_SSTab1_TabPage2
        '
        Me._SSTab1_TabPage2.Controls.Add(Me.Label3)
        Me._SSTab1_TabPage2.Controls.Add(Me.SprdMain6)
        Me._SSTab1_TabPage2.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage2.Name = "_SSTab1_TabPage2"
        Me._SSTab1_TabPage2.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage2.TabIndex = 2
        Me._SSTab1_TabPage2.Text = "B2CS"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(2, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(1090, 32)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = resources.GetString("Label3.Text")
        '
        'SprdMain6
        '
        Me.SprdMain6.DataSource = Nothing
        Me.SprdMain6.Location = New System.Drawing.Point(2, 39)
        Me.SprdMain6.Name = "SprdMain6"
        Me.SprdMain6.OcxState = CType(resources.GetObject("SprdMain6.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain6.Size = New System.Drawing.Size(1094, 425)
        Me.SprdMain6.TabIndex = 18
        '
        '_SSTab1_TabPage3
        '
        Me._SSTab1_TabPage3.Controls.Add(Me.SprdMain6A)
        Me._SSTab1_TabPage3.Controls.Add(Me.Label4)
        Me._SSTab1_TabPage3.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage3.Name = "_SSTab1_TabPage3"
        Me._SSTab1_TabPage3.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage3.TabIndex = 3
        Me._SSTab1_TabPage3.Text = "CN / DN Reg"
        '
        'SprdMain6A
        '
        Me.SprdMain6A.DataSource = Nothing
        Me.SprdMain6A.Location = New System.Drawing.Point(1, 34)
        Me.SprdMain6A.Name = "SprdMain6A"
        Me.SprdMain6A.OcxState = CType(resources.GetObject("SprdMain6A.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain6A.Size = New System.Drawing.Size(1093, 428)
        Me.SprdMain6A.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(1, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(734, 31)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "GSTR1 Credit/Debit Note to Registered customer: Credit/Debit notes are raised aga" &
    "inst already created invoices for outward supply of goods or services. "
        '
        '_SSTab1_TabPage4
        '
        Me._SSTab1_TabPage4.Controls.Add(Me.SprdMain7)
        Me._SSTab1_TabPage4.Controls.Add(Me.Label5)
        Me._SSTab1_TabPage4.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage4.Name = "_SSTab1_TabPage4"
        Me._SSTab1_TabPage4.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage4.TabIndex = 4
        Me._SSTab1_TabPage4.Text = "CN / DN Un Reg"
        '
        'SprdMain7
        '
        Me.SprdMain7.DataSource = Nothing
        Me.SprdMain7.Location = New System.Drawing.Point(3, 34)
        Me.SprdMain7.Name = "SprdMain7"
        Me.SprdMain7.OcxState = CType(resources.GetObject("SprdMain7.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain7.Size = New System.Drawing.Size(1093, 428)
        Me.SprdMain7.TabIndex = 22
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(3, 2)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(734, 31)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "GSTR1 Credit/Debit Note to Un-Registered customer: Credit/Debit notes are raised " &
    "against already created invoices for outward supply of goods or services. "
        '
        '_SSTab1_TabPage5
        '
        Me._SSTab1_TabPage5.Controls.Add(Me.SprdMain7A)
        Me._SSTab1_TabPage5.Controls.Add(Me.Label6)
        Me._SSTab1_TabPage5.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage5.Name = "_SSTab1_TabPage5"
        Me._SSTab1_TabPage5.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage5.TabIndex = 5
        Me._SSTab1_TabPage5.Text = "Export"
        '
        'SprdMain7A
        '
        Me.SprdMain7A.DataSource = Nothing
        Me.SprdMain7A.Location = New System.Drawing.Point(2, 34)
        Me.SprdMain7A.Name = "SprdMain7A"
        Me.SprdMain7A.OcxState = CType(resources.GetObject("SprdMain7A.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain7A.Size = New System.Drawing.Size(1092, 430)
        Me.SprdMain7A.TabIndex = 24
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(2, 2)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(732, 31)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "GSTR1 Export: :"
        '
        '_SSTab1_TabPage6
        '
        Me._SSTab1_TabPage6.Controls.Add(Me.SprdMain8)
        Me._SSTab1_TabPage6.Controls.Add(Me.Label7)
        Me._SSTab1_TabPage6.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage6.Name = "_SSTab1_TabPage6"
        Me._SSTab1_TabPage6.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage6.TabIndex = 6
        Me._SSTab1_TabPage6.Text = "Adv Tax"
        '
        'SprdMain8
        '
        Me.SprdMain8.DataSource = Nothing
        Me.SprdMain8.Location = New System.Drawing.Point(2, 35)
        Me.SprdMain8.Name = "SprdMain8"
        Me.SprdMain8.OcxState = CType(resources.GetObject("SprdMain8.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain8.Size = New System.Drawing.Size(1094, 427)
        Me.SprdMain8.TabIndex = 26
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(2, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(734, 31)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "GSTR1 Tax Paid: Tax paid is adjusted against tax liability of current financial p" &
    "eriod. "
        '
        '_SSTab1_TabPage7
        '
        Me._SSTab1_TabPage7.Controls.Add(Me.SprdMain8A)
        Me._SSTab1_TabPage7.Controls.Add(Me.Label8)
        Me._SSTab1_TabPage7.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage7.Name = "_SSTab1_TabPage7"
        Me._SSTab1_TabPage7.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage7.TabIndex = 7
        Me._SSTab1_TabPage7.Text = "Adv Summ"
        '
        'SprdMain8A
        '
        Me.SprdMain8A.DataSource = Nothing
        Me.SprdMain8A.Location = New System.Drawing.Point(1, 34)
        Me.SprdMain8A.Name = "SprdMain8A"
        Me.SprdMain8A.OcxState = CType(resources.GetObject("SprdMain8A.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain8A.Size = New System.Drawing.Size(1095, 428)
        Me.SprdMain8A.TabIndex = 28
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(1, 2)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(734, 31)
        Me.Label8.TabIndex = 29
        Me.Label8.Text = "GSTR1 Tax Paid: Tax paid is adjusted against tax liability of current financial p" &
    "eriod. "
        '
        '_SSTab1_TabPage8
        '
        Me._SSTab1_TabPage8.Controls.Add(Me.Label9)
        Me._SSTab1_TabPage8.Controls.Add(Me.SprdMain9)
        Me._SSTab1_TabPage8.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage8.Name = "_SSTab1_TabPage8"
        Me._SSTab1_TabPage8.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage8.TabIndex = 8
        Me._SSTab1_TabPage8.Text = "HSN"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(1, 2)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(734, 31)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "GSTR1 HSN Summary: Summary of outward supply HSN wise with their quantity, supply" &
    " value."
        '
        'SprdMain9
        '
        Me.SprdMain9.DataSource = Nothing
        Me.SprdMain9.Location = New System.Drawing.Point(1, 34)
        Me.SprdMain9.Name = "SprdMain9"
        Me.SprdMain9.OcxState = CType(resources.GetObject("SprdMain9.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain9.Size = New System.Drawing.Size(1093, 428)
        Me.SprdMain9.TabIndex = 30
        '
        '_SSTab1_TabPage9
        '
        Me._SSTab1_TabPage9.Controls.Add(Me.SprdMain10)
        Me._SSTab1_TabPage9.Controls.Add(Me.Label11)
        Me._SSTab1_TabPage9.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage9.Name = "_SSTab1_TabPage9"
        Me._SSTab1_TabPage9.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage9.TabIndex = 9
        Me._SSTab1_TabPage9.Text = "Doc"
        '
        'SprdMain10
        '
        Me.SprdMain10.DataSource = Nothing
        Me.SprdMain10.Location = New System.Drawing.Point(2, 35)
        Me.SprdMain10.Name = "SprdMain10"
        Me.SprdMain10.OcxState = CType(resources.GetObject("SprdMain10.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain10.Size = New System.Drawing.Size(1092, 429)
        Me.SprdMain10.TabIndex = 32
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(2, 3)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(734, 31)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "GSTR1 Doc Issued: Summary of supply document type."
        '
        '_SSTab1_TabPage10
        '
        Me._SSTab1_TabPage10.Controls.Add(Me.Label10)
        Me._SSTab1_TabPage10.Controls.Add(Me.SprdMain11)
        Me._SSTab1_TabPage10.Location = New System.Drawing.Point(4, 24)
        Me._SSTab1_TabPage10.Name = "_SSTab1_TabPage10"
        Me._SSTab1_TabPage10.Size = New System.Drawing.Size(1096, 465)
        Me._SSTab1_TabPage10.TabIndex = 10
        Me._SSTab1_TabPage10.Text = "Nit Rated"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(2, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(734, 31)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "Summary For Nil rated, exempted and non GST outward supplies"
        '
        'SprdMain11
        '
        Me.SprdMain11.DataSource = Nothing
        Me.SprdMain11.Location = New System.Drawing.Point(2, 35)
        Me.SprdMain11.Name = "SprdMain11"
        Me.SprdMain11.OcxState = CType(resources.GetObject("SprdMain11.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain11.Size = New System.Drawing.Size(1092, 429)
        Me.SprdMain11.TabIndex = 34
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 14
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
        Me.FraMovement.Location = New System.Drawing.Point(860, 570)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 49)
        Me.FraMovement.TabIndex = 10
        Me.FraMovement.TabStop = False
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(250, 428)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(87, 13)
        Me.lblAcCode.TabIndex = 12
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'lblTrnType
        '
        Me.lblTrnType.AutoSize = True
        Me.lblTrnType.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrnType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTrnType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrnType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrnType.Location = New System.Drawing.Point(172, 432)
        Me.lblTrnType.Name = "lblTrnType"
        Me.lblTrnType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnType.Size = New System.Drawing.Size(56, 14)
        Me.lblTrnType.TabIndex = 11
        Me.lblTrnType.Text = "lblTrnType"
        Me.lblTrnType.Visible = False
        '
        'cboGSTNO
        '
        Me.cboGSTNO.BackColor = System.Drawing.SystemColors.Window
        Me.cboGSTNO.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboGSTNO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGSTNO.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboGSTNO.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboGSTNO.Location = New System.Drawing.Point(324, 8)
        Me.cboGSTNO.Name = "cboGSTNO"
        Me.cboGSTNO.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboGSTNO.Size = New System.Drawing.Size(295, 22)
        Me.cboGSTNO.TabIndex = 36
        '
        '_Lbl_7
        '
        Me._Lbl_7.AutoSize = True
        Me._Lbl_7.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._Lbl_7.Location = New System.Drawing.Point(264, 11)
        Me._Lbl_7.Name = "_Lbl_7"
        Me._Lbl_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_7.Size = New System.Drawing.Size(52, 14)
        Me._Lbl_7.TabIndex = 37
        Me._Lbl_7.Text = "GST No :"
        '
        'frmParamGSTR1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.cboGSTNO)
        Me.Controls.Add(Me._Lbl_7)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.lblAcCode)
        Me.Controls.Add(Me.lblTrnType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamGSTR1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "GSTR - 1 (Details of outward Supplies)"
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        CType(Me.SprdMain5A, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage2.ResumeLayout(False)
        CType(Me.SprdMain6, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage3.ResumeLayout(False)
        CType(Me.SprdMain6A, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage4.ResumeLayout(False)
        CType(Me.SprdMain7, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage5.ResumeLayout(False)
        CType(Me.SprdMain7A, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage6.ResumeLayout(False)
        CType(Me.SprdMain8, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage7.ResumeLayout(False)
        CType(Me.SprdMain8A, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage8.ResumeLayout(False)
        CType(Me.SprdMain9, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage9.ResumeLayout(False)
        CType(Me.SprdMain10, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage10.ResumeLayout(False)
        CType(Me.SprdMain11, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdMain11.DataSource = CType(AData17, MSDATASRC.DataSource)
        'SprdMain10.DataSource = CType(AData14, MSDATASRC.DataSource)
        'SprdMain9.DataSource = CType(AData15, MSDATASRC.DataSource)
        'SprdMain8A.DataSource = CType(AData13, MSDATASRC.DataSource)
        'SprdMain8.DataSource = CType(AData11, MSDATASRC.DataSource)
        'SprdMain7A.DataSource = CType(AData10, MSDATASRC.DataSource)
        'SprdMain7.DataSource = CType(AData9, MSDATASRC.DataSource)
        'SprdMain6A.DataSource = CType(AData8, MSDATASRC.DataSource)
        'SprdMain6.DataSource = CType(AData7, MSDATASRC.DataSource)
        'SprdMain5A.DataSource = CType(AData6, MSDATASRC.DataSource)
        ''SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdMain11.DataSource = Nothing
        SprdMain10.DataSource = Nothing
        SprdMain9.DataSource = Nothing
        SprdMain8A.DataSource = Nothing
        SprdMain8.DataSource = Nothing
        SprdMain7A.DataSource = Nothing
        SprdMain7.DataSource = Nothing
        SprdMain6A.DataSource = Nothing
        SprdMain6.DataSource = Nothing
        SprdMain5A.DataSource = Nothing
        SprdMain.DataSource = Nothing
    End Sub

    Public WithEvents cboGSTNO As ComboBox
    Public WithEvents _Lbl_7 As Label
#End Region
End Class