Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamScrapRecStm
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
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents txtPaymentDays As System.Windows.Forms.TextBox
    Public WithEvents cmdPaySearch As System.Windows.Forms.Button
    Public WithEvents txtPayment As System.Windows.Forms.TextBox
    Public WithEvents txtOthCond1 As System.Windows.Forms.TextBox
    Public WithEvents txtOthCond2 As System.Windows.Forms.TextBox
    Public WithEvents txtPacking As System.Windows.Forms.TextBox
    Public WithEvents txtExcise As System.Windows.Forms.TextBox
    Public WithEvents txtDelivery As System.Windows.Forms.TextBox
    Public WithEvents txtDespMode As System.Windows.Forms.TextBox
    Public WithEvents txtInspection As System.Windows.Forms.TextBox
    Public WithEvents txtInsurance As System.Windows.Forms.TextBox
    Public WithEvents cmdServProvided As System.Windows.Forms.Button
    Public WithEvents txtServProvided As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblPaymentTerms As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblDueDays As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label44 As System.Windows.Forms.Label
    Public WithEvents lblAddUser As System.Windows.Forms.Label
    Public WithEvents Label46 As System.Windows.Forms.Label
    Public WithEvents lblModUser As System.Windows.Forms.Label
    Public WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblAddDate As System.Windows.Forms.Label
    Public WithEvents Label48 As System.Windows.Forms.Label
    Public WithEvents lblModDate As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents txtAnnexTitle As System.Windows.Forms.TextBox
    Public WithEvents SprdAnnex As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label27 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents sprdOpening As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents sprdProcess As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents sprdComponent As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents sprdOthScrap As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage3 As System.Windows.Forms.TabPage
    Public WithEvents SprdSale As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage4 As System.Windows.Forms.TabPage
    Public WithEvents sprdClosing As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage5 As System.Windows.Forms.TabPage
    Public WithEvents sprdSummary As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame11 As System.Windows.Forms.GroupBox
    Public WithEvents _TabMain_TabPage6 As System.Windows.Forms.TabPage
    Public WithEvents TabMain As System.Windows.Forms.TabControl
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Lbl As VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamScrapRecStm))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdPaySearch = New System.Windows.Forms.Button()
        Me.cmdServProvided = New System.Windows.Forms.Button()
        Me.txtServProvided = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.TabMain = New System.Windows.Forms.TabControl()
        Me._TabMain_TabPage0 = New System.Windows.Forms.TabPage()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.sprdOpening = New AxFPSpreadADO.AxfpSpread()
        Me._TabMain_TabPage1 = New System.Windows.Forms.TabPage()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.sprdProcess = New AxFPSpreadADO.AxfpSpread()
        Me._TabMain_TabPage2 = New System.Windows.Forms.TabPage()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.sprdComponent = New AxFPSpreadADO.AxfpSpread()
        Me._TabMain_TabPage3 = New System.Windows.Forms.TabPage()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.sprdOthScrap = New AxFPSpreadADO.AxfpSpread()
        Me._TabMain_TabPage4 = New System.Windows.Forms.TabPage()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.SprdSale = New AxFPSpreadADO.AxfpSpread()
        Me._TabMain_TabPage5 = New System.Windows.Forms.TabPage()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.sprdClosing = New AxFPSpreadADO.AxfpSpread()
        Me._TabMain_TabPage6 = New System.Windows.Forms.TabPage()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.sprdSummary = New AxFPSpreadADO.AxfpSpread()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.txtPaymentDays = New System.Windows.Forms.TextBox()
        Me.txtPayment = New System.Windows.Forms.TextBox()
        Me.txtOthCond1 = New System.Windows.Forms.TextBox()
        Me.txtOthCond2 = New System.Windows.Forms.TextBox()
        Me.txtPacking = New System.Windows.Forms.TextBox()
        Me.txtExcise = New System.Windows.Forms.TextBox()
        Me.txtDelivery = New System.Windows.Forms.TextBox()
        Me.txtDespMode = New System.Windows.Forms.TextBox()
        Me.txtInspection = New System.Windows.Forms.TextBox()
        Me.txtInsurance = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblPaymentTerms = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblDueDays = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.lblAddUser = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.lblModUser = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblAddDate = New System.Windows.Forms.Label()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.lblModDate = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtAnnexTitle = New System.Windows.Forms.TextBox()
        Me.SprdAnnex = New AxFPSpreadADO.AxfpSpread()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabMain.SuspendLayout()
        Me._TabMain_TabPage0.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.sprdOpening, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage1.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.sprdProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage2.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.sprdComponent, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage3.SuspendLayout()
        Me.Frame10.SuspendLayout()
        CType(Me.sprdOthScrap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.SprdSale, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage5.SuspendLayout()
        Me.Frame9.SuspendLayout()
        CType(Me.sprdClosing, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._TabMain_TabPage6.SuspendLayout()
        Me.Frame11.SuspendLayout()
        CType(Me.sprdSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame7.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.SprdAnnex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdPaySearch
        '
        Me.cmdPaySearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPaySearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPaySearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPaySearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPaySearch.Image = CType(resources.GetObject("cmdPaySearch.Image"), System.Drawing.Image)
        Me.cmdPaySearch.Location = New System.Drawing.Point(210, 172)
        Me.cmdPaySearch.Name = "cmdPaySearch"
        Me.cmdPaySearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPaySearch.Size = New System.Drawing.Size(23, 19)
        Me.cmdPaySearch.TabIndex = 28
        Me.cmdPaySearch.TabStop = False
        Me.cmdPaySearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPaySearch, "Search")
        Me.cmdPaySearch.UseVisualStyleBackColor = False
        '
        'cmdServProvided
        '
        Me.cmdServProvided.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdServProvided.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdServProvided.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdServProvided.Image = CType(resources.GetObject("cmdServProvided.Image"), System.Drawing.Image)
        Me.cmdServProvided.Location = New System.Drawing.Point(594, 192)
        Me.cmdServProvided.Name = "cmdServProvided"
        Me.cmdServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdServProvided.Size = New System.Drawing.Size(27, 19)
        Me.cmdServProvided.TabIndex = 18
        Me.cmdServProvided.TabStop = False
        Me.cmdServProvided.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdServProvided, "Search")
        Me.cmdServProvided.UseVisualStyleBackColor = False
        '
        'txtServProvided
        '
        Me.txtServProvided.AcceptsReturn = True
        Me.txtServProvided.BackColor = System.Drawing.SystemColors.Window
        Me.txtServProvided.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtServProvided.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServProvided.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServProvided.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServProvided.Location = New System.Drawing.Point(162, 192)
        Me.txtServProvided.MaxLength = 0
        Me.txtServProvided.Name = "txtServProvided"
        Me.txtServProvided.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServProvided.Size = New System.Drawing.Size(431, 22)
        Me.txtServProvided.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.txtServProvided, "Press F1 For Help")
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.cmdPrint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        Me.cmdShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
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
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(516, 6)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(231, 21)
        Me.cboDivision.TabIndex = 56
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
        Me.Frame6.Location = New System.Drawing.Point(0, -2)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(121, 59)
        Me.Frame6.TabIndex = 6
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(42, 12)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 22)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(42, 36)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(75, 22)
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
        Me._Lbl_1.Location = New System.Drawing.Point(4, 38)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(25, 13)
        Me._Lbl_1.TabIndex = 8
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
        Me._Lbl_0.Location = New System.Drawing.Point(4, 16)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(40, 13)
        Me._Lbl_0.TabIndex = 7
        Me._Lbl_0.Text = "From :"
        Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me.TabMain)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 52)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(910, 528)
        Me.Frame4.TabIndex = 9
        Me.Frame4.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 94)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 0
        '
        'TabMain
        '
        Me.TabMain.Controls.Add(Me._TabMain_TabPage0)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage1)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage2)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage3)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage4)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage5)
        Me.TabMain.Controls.Add(Me._TabMain_TabPage6)
        Me.TabMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabMain.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabMain.ItemSize = New System.Drawing.Size(42, 18)
        Me.TabMain.Location = New System.Drawing.Point(0, 15)
        Me.TabMain.Name = "TabMain"
        Me.TabMain.SelectedIndex = 1
        Me.TabMain.Size = New System.Drawing.Size(910, 513)
        Me.TabMain.TabIndex = 11
        '
        '_TabMain_TabPage0
        '
        Me._TabMain_TabPage0.Controls.Add(Me.Frame8)
        Me._TabMain_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage0.Name = "_TabMain_TabPage0"
        Me._TabMain_TabPage0.Size = New System.Drawing.Size(902, 487)
        Me._TabMain_TabPage0.TabIndex = 0
        Me._TabMain_TabPage0.Text = "Opening Scrap"
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.sprdOpening)
        Me.Frame8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 0)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(902, 487)
        Me.Frame8.TabIndex = 54
        Me.Frame8.TabStop = False
        '
        'sprdOpening
        '
        Me.sprdOpening.DataSource = Nothing
        Me.sprdOpening.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdOpening.Location = New System.Drawing.Point(0, 15)
        Me.sprdOpening.Name = "sprdOpening"
        Me.sprdOpening.OcxState = CType(resources.GetObject("sprdOpening.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdOpening.Size = New System.Drawing.Size(902, 472)
        Me.sprdOpening.TabIndex = 55
        '
        '_TabMain_TabPage1
        '
        Me._TabMain_TabPage1.Controls.Add(Me.Frame1)
        Me._TabMain_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage1.Name = "_TabMain_TabPage1"
        Me._TabMain_TabPage1.Size = New System.Drawing.Size(902, 487)
        Me._TabMain_TabPage1.TabIndex = 1
        Me._TabMain_TabPage1.Text = "Process Scrap"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.sprdProcess)
        Me.Frame1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(902, 487)
        Me.Frame1.TabIndex = 50
        Me.Frame1.TabStop = False
        '
        'sprdProcess
        '
        Me.sprdProcess.DataSource = Nothing
        Me.sprdProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdProcess.Location = New System.Drawing.Point(0, 15)
        Me.sprdProcess.Name = "sprdProcess"
        Me.sprdProcess.OcxState = CType(resources.GetObject("sprdProcess.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdProcess.Size = New System.Drawing.Size(902, 472)
        Me.sprdProcess.TabIndex = 51
        '
        '_TabMain_TabPage2
        '
        Me._TabMain_TabPage2.Controls.Add(Me.Frame2)
        Me._TabMain_TabPage2.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage2.Name = "_TabMain_TabPage2"
        Me._TabMain_TabPage2.Size = New System.Drawing.Size(902, 487)
        Me._TabMain_TabPage2.TabIndex = 2
        Me._TabMain_TabPage2.Text = "Component Scrap"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.sprdComponent)
        Me.Frame2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(902, 487)
        Me.Frame2.TabIndex = 52
        Me.Frame2.TabStop = False
        '
        'sprdComponent
        '
        Me.sprdComponent.DataSource = Nothing
        Me.sprdComponent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdComponent.Location = New System.Drawing.Point(0, 15)
        Me.sprdComponent.Name = "sprdComponent"
        Me.sprdComponent.OcxState = CType(resources.GetObject("sprdComponent.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdComponent.Size = New System.Drawing.Size(902, 472)
        Me.sprdComponent.TabIndex = 53
        '
        '_TabMain_TabPage3
        '
        Me._TabMain_TabPage3.Controls.Add(Me.Frame10)
        Me._TabMain_TabPage3.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage3.Name = "_TabMain_TabPage3"
        Me._TabMain_TabPage3.Size = New System.Drawing.Size(902, 487)
        Me._TabMain_TabPage3.TabIndex = 3
        Me._TabMain_TabPage3.Text = "Other Scrap"
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.sprdOthScrap)
        Me.Frame10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(0, 0)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(902, 487)
        Me.Frame10.TabIndex = 61
        Me.Frame10.TabStop = False
        '
        'sprdOthScrap
        '
        Me.sprdOthScrap.DataSource = Nothing
        Me.sprdOthScrap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdOthScrap.Location = New System.Drawing.Point(0, 15)
        Me.sprdOthScrap.Name = "sprdOthScrap"
        Me.sprdOthScrap.OcxState = CType(resources.GetObject("sprdOthScrap.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdOthScrap.Size = New System.Drawing.Size(902, 472)
        Me.sprdOthScrap.TabIndex = 62
        '
        '_TabMain_TabPage4
        '
        Me._TabMain_TabPage4.Controls.Add(Me.Frame3)
        Me._TabMain_TabPage4.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage4.Name = "_TabMain_TabPage4"
        Me._TabMain_TabPage4.Size = New System.Drawing.Size(902, 487)
        Me._TabMain_TabPage4.TabIndex = 4
        Me._TabMain_TabPage4.Text = "Scrap Sale"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.SprdSale)
        Me.Frame3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(902, 487)
        Me.Frame3.TabIndex = 59
        Me.Frame3.TabStop = False
        '
        'SprdSale
        '
        Me.SprdSale.DataSource = Nothing
        Me.SprdSale.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdSale.Location = New System.Drawing.Point(0, 15)
        Me.SprdSale.Name = "SprdSale"
        Me.SprdSale.OcxState = CType(resources.GetObject("SprdSale.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdSale.Size = New System.Drawing.Size(902, 472)
        Me.SprdSale.TabIndex = 60
        '
        '_TabMain_TabPage5
        '
        Me._TabMain_TabPage5.Controls.Add(Me.Frame9)
        Me._TabMain_TabPage5.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage5.Name = "_TabMain_TabPage5"
        Me._TabMain_TabPage5.Size = New System.Drawing.Size(902, 487)
        Me._TabMain_TabPage5.TabIndex = 5
        Me._TabMain_TabPage5.Text = "Closing Scrap"
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.sprdClosing)
        Me.Frame9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(0, 0)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(902, 487)
        Me.Frame9.TabIndex = 58
        Me.Frame9.TabStop = False
        '
        'sprdClosing
        '
        Me.sprdClosing.DataSource = Nothing
        Me.sprdClosing.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdClosing.Location = New System.Drawing.Point(0, 15)
        Me.sprdClosing.Name = "sprdClosing"
        Me.sprdClosing.OcxState = CType(resources.GetObject("sprdClosing.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdClosing.Size = New System.Drawing.Size(902, 472)
        Me.sprdClosing.TabIndex = 65
        '
        '_TabMain_TabPage6
        '
        Me._TabMain_TabPage6.Controls.Add(Me.Frame11)
        Me._TabMain_TabPage6.Location = New System.Drawing.Point(4, 22)
        Me._TabMain_TabPage6.Name = "_TabMain_TabPage6"
        Me._TabMain_TabPage6.Size = New System.Drawing.Size(902, 487)
        Me._TabMain_TabPage6.TabIndex = 6
        Me._TabMain_TabPage6.Text = "Scrap Summary"
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.sprdSummary)
        Me.Frame11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Frame11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame11.Location = New System.Drawing.Point(0, 0)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(902, 487)
        Me.Frame11.TabIndex = 63
        Me.Frame11.TabStop = False
        '
        'sprdSummary
        '
        Me.sprdSummary.DataSource = Nothing
        Me.sprdSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.sprdSummary.Location = New System.Drawing.Point(0, 15)
        Me.sprdSummary.Name = "sprdSummary"
        Me.sprdSummary.OcxState = CType(resources.GetObject("sprdSummary.OcxState"), System.Windows.Forms.AxHost.State)
        Me.sprdSummary.Size = New System.Drawing.Size(902, 472)
        Me.sprdSummary.TabIndex = 64
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.txtPaymentDays)
        Me.Frame7.Controls.Add(Me.cmdPaySearch)
        Me.Frame7.Controls.Add(Me.txtPayment)
        Me.Frame7.Controls.Add(Me.txtOthCond1)
        Me.Frame7.Controls.Add(Me.txtOthCond2)
        Me.Frame7.Controls.Add(Me.txtPacking)
        Me.Frame7.Controls.Add(Me.txtExcise)
        Me.Frame7.Controls.Add(Me.txtDelivery)
        Me.Frame7.Controls.Add(Me.txtDespMode)
        Me.Frame7.Controls.Add(Me.txtInspection)
        Me.Frame7.Controls.Add(Me.txtInsurance)
        Me.Frame7.Controls.Add(Me.cmdServProvided)
        Me.Frame7.Controls.Add(Me.txtServProvided)
        Me.Frame7.Controls.Add(Me.Label13)
        Me.Frame7.Controls.Add(Me.lblPaymentTerms)
        Me.Frame7.Controls.Add(Me.Label7)
        Me.Frame7.Controls.Add(Me.Label5)
        Me.Frame7.Controls.Add(Me.Label3)
        Me.Frame7.Controls.Add(Me.lblDueDays)
        Me.Frame7.Controls.Add(Me.Label16)
        Me.Frame7.Controls.Add(Me.Label15)
        Me.Frame7.Controls.Add(Me.Label14)
        Me.Frame7.Controls.Add(Me.Label17)
        Me.Frame7.Controls.Add(Me.Label18)
        Me.Frame7.Controls.Add(Me.Label44)
        Me.Frame7.Controls.Add(Me.lblAddUser)
        Me.Frame7.Controls.Add(Me.Label46)
        Me.Frame7.Controls.Add(Me.lblModUser)
        Me.Frame7.Controls.Add(Me.Label45)
        Me.Frame7.Controls.Add(Me.lblAddDate)
        Me.Frame7.Controls.Add(Me.Label48)
        Me.Frame7.Controls.Add(Me.lblModDate)
        Me.Frame7.Controls.Add(Me.Label12)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(-4996, 22)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(739, 269)
        Me.Frame7.TabIndex = 16
        Me.Frame7.TabStop = False
        '
        'txtPaymentDays
        '
        Me.txtPaymentDays.AcceptsReturn = True
        Me.txtPaymentDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaymentDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaymentDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentDays.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPaymentDays.Location = New System.Drawing.Point(550, 172)
        Me.txtPaymentDays.MaxLength = 15
        Me.txtPaymentDays.Name = "txtPaymentDays"
        Me.txtPaymentDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaymentDays.Size = New System.Drawing.Size(71, 22)
        Me.txtPaymentDays.TabIndex = 29
        '
        'txtPayment
        '
        Me.txtPayment.AcceptsReturn = True
        Me.txtPayment.BackColor = System.Drawing.SystemColors.Window
        Me.txtPayment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPayment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPayment.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPayment.Location = New System.Drawing.Point(162, 172)
        Me.txtPayment.MaxLength = 15
        Me.txtPayment.Name = "txtPayment"
        Me.txtPayment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPayment.Size = New System.Drawing.Size(47, 22)
        Me.txtPayment.TabIndex = 27
        '
        'txtOthCond1
        '
        Me.txtOthCond1.AcceptsReturn = True
        Me.txtOthCond1.BackColor = System.Drawing.SystemColors.Window
        Me.txtOthCond1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOthCond1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOthCond1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthCond1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOthCond1.Location = New System.Drawing.Point(162, 132)
        Me.txtOthCond1.MaxLength = 15
        Me.txtOthCond1.Name = "txtOthCond1"
        Me.txtOthCond1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOthCond1.Size = New System.Drawing.Size(459, 22)
        Me.txtOthCond1.TabIndex = 26
        '
        'txtOthCond2
        '
        Me.txtOthCond2.AcceptsReturn = True
        Me.txtOthCond2.BackColor = System.Drawing.SystemColors.Window
        Me.txtOthCond2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOthCond2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOthCond2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthCond2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtOthCond2.Location = New System.Drawing.Point(162, 152)
        Me.txtOthCond2.MaxLength = 15
        Me.txtOthCond2.Name = "txtOthCond2"
        Me.txtOthCond2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOthCond2.Size = New System.Drawing.Size(459, 22)
        Me.txtOthCond2.TabIndex = 25
        '
        'txtPacking
        '
        Me.txtPacking.AcceptsReturn = True
        Me.txtPacking.BackColor = System.Drawing.SystemColors.Window
        Me.txtPacking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPacking.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPacking.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPacking.ForeColor = System.Drawing.Color.Blue
        Me.txtPacking.Location = New System.Drawing.Point(162, 92)
        Me.txtPacking.MaxLength = 0
        Me.txtPacking.Name = "txtPacking"
        Me.txtPacking.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPacking.Size = New System.Drawing.Size(459, 22)
        Me.txtPacking.TabIndex = 24
        '
        'txtExcise
        '
        Me.txtExcise.AcceptsReturn = True
        Me.txtExcise.BackColor = System.Drawing.SystemColors.Window
        Me.txtExcise.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExcise.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExcise.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExcise.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtExcise.Location = New System.Drawing.Point(162, 12)
        Me.txtExcise.MaxLength = 15
        Me.txtExcise.Name = "txtExcise"
        Me.txtExcise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExcise.Size = New System.Drawing.Size(459, 22)
        Me.txtExcise.TabIndex = 23
        '
        'txtDelivery
        '
        Me.txtDelivery.AcceptsReturn = True
        Me.txtDelivery.BackColor = System.Drawing.SystemColors.Window
        Me.txtDelivery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDelivery.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDelivery.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDelivery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDelivery.Location = New System.Drawing.Point(162, 32)
        Me.txtDelivery.MaxLength = 15
        Me.txtDelivery.Name = "txtDelivery"
        Me.txtDelivery.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDelivery.Size = New System.Drawing.Size(459, 22)
        Me.txtDelivery.TabIndex = 22
        '
        'txtDespMode
        '
        Me.txtDespMode.AcceptsReturn = True
        Me.txtDespMode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDespMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDespMode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDespMode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDespMode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDespMode.Location = New System.Drawing.Point(162, 52)
        Me.txtDespMode.MaxLength = 15
        Me.txtDespMode.Name = "txtDespMode"
        Me.txtDespMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDespMode.Size = New System.Drawing.Size(459, 22)
        Me.txtDespMode.TabIndex = 21
        '
        'txtInspection
        '
        Me.txtInspection.AcceptsReturn = True
        Me.txtInspection.BackColor = System.Drawing.SystemColors.Window
        Me.txtInspection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInspection.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInspection.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInspection.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtInspection.Location = New System.Drawing.Point(162, 72)
        Me.txtInspection.MaxLength = 15
        Me.txtInspection.Name = "txtInspection"
        Me.txtInspection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInspection.Size = New System.Drawing.Size(459, 22)
        Me.txtInspection.TabIndex = 20
        '
        'txtInsurance
        '
        Me.txtInsurance.AcceptsReturn = True
        Me.txtInsurance.BackColor = System.Drawing.SystemColors.Window
        Me.txtInsurance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInsurance.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInsurance.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInsurance.ForeColor = System.Drawing.Color.Blue
        Me.txtInsurance.Location = New System.Drawing.Point(162, 112)
        Me.txtInsurance.MaxLength = 0
        Me.txtInsurance.Name = "txtInsurance"
        Me.txtInsurance.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInsurance.Size = New System.Drawing.Size(459, 22)
        Me.txtInsurance.TabIndex = 19
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(390, 174)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(85, 13)
        Me.Label13.TabIndex = 49
        Me.Label13.Text = "Payment Days :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblPaymentTerms
        '
        Me.lblPaymentTerms.BackColor = System.Drawing.Color.Transparent
        Me.lblPaymentTerms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPaymentTerms.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPaymentTerms.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPaymentTerms.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPaymentTerms.Location = New System.Drawing.Point(234, 172)
        Me.lblPaymentTerms.Name = "lblPaymentTerms"
        Me.lblPaymentTerms.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPaymentTerms.Size = New System.Drawing.Size(179, 19)
        Me.lblPaymentTerms.TabIndex = 48
        Me.lblPaymentTerms.Text = "lblPaymentTerms"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(2, 174)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(87, 13)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "PaymentTerms :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(2, 154)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(80, 13)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Other Cond 2 :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(54, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(88, 13)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Sales / VAT Tax :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDueDays
        '
        Me.lblDueDays.AutoSize = True
        Me.lblDueDays.BackColor = System.Drawing.SystemColors.Control
        Me.lblDueDays.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDueDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDueDays.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDueDays.Location = New System.Drawing.Point(2, 96)
        Me.lblDueDays.Name = "lblDueDays"
        Me.lblDueDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDueDays.Size = New System.Drawing.Size(113, 13)
        Me.lblDueDays.TabIndex = 44
        Me.lblDueDays.Text = "Packing Forwarding :"
        Me.lblDueDays.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(2, 54)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(107, 13)
        Me.Label16.TabIndex = 43
        Me.Label16.Text = "Mode of Despatch :"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(2, 34)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(55, 13)
        Me.Label15.TabIndex = 42
        Me.Label15.Text = "Delivery :"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(2, 14)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(44, 13)
        Me.Label14.TabIndex = 41
        Me.Label14.Text = "Excise :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(2, 74)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(65, 13)
        Me.Label17.TabIndex = 40
        Me.Label17.Text = "Inspection :"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(2, 116)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(60, 13)
        Me.Label18.TabIndex = 39
        Me.Label18.Text = "Insurance :"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.BackColor = System.Drawing.SystemColors.Control
        Me.Label44.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label44.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label44.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label44.Location = New System.Drawing.Point(118, 246)
        Me.Label44.Name = "Label44"
        Me.Label44.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label44.Size = New System.Drawing.Size(61, 15)
        Me.Label44.TabIndex = 38
        Me.Label44.Text = "Add User :"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddUser
        '
        Me.lblAddUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddUser.Location = New System.Drawing.Point(177, 244)
        Me.lblAddUser.Name = "lblAddUser"
        Me.lblAddUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddUser.Size = New System.Drawing.Size(69, 19)
        Me.lblAddUser.TabIndex = 37
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.BackColor = System.Drawing.SystemColors.Control
        Me.Label46.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label46.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label46.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label46.Location = New System.Drawing.Point(379, 246)
        Me.Label46.Name = "Label46"
        Me.Label46.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label46.Size = New System.Drawing.Size(61, 15)
        Me.Label46.TabIndex = 36
        Me.Label46.Text = "Mod User:"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModUser
        '
        Me.lblModUser.BackColor = System.Drawing.SystemColors.Control
        Me.lblModUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModUser.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModUser.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModUser.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModUser.Location = New System.Drawing.Point(439, 244)
        Me.lblModUser.Name = "lblModUser"
        Me.lblModUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModUser.Size = New System.Drawing.Size(69, 19)
        Me.lblModUser.TabIndex = 35
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.BackColor = System.Drawing.SystemColors.Control
        Me.Label45.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label45.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label45.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label45.Location = New System.Drawing.Point(249, 246)
        Me.Label45.Name = "Label45"
        Me.Label45.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label45.Size = New System.Drawing.Size(63, 15)
        Me.Label45.TabIndex = 34
        Me.Label45.Text = "Add Date :"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAddDate
        '
        Me.lblAddDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblAddDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAddDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAddDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAddDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAddDate.Location = New System.Drawing.Point(309, 244)
        Me.lblAddDate.Name = "lblAddDate"
        Me.lblAddDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAddDate.Size = New System.Drawing.Size(69, 19)
        Me.lblAddDate.TabIndex = 33
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.BackColor = System.Drawing.SystemColors.Control
        Me.Label48.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label48.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label48.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label48.Location = New System.Drawing.Point(512, 246)
        Me.Label48.Name = "Label48"
        Me.Label48.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label48.Size = New System.Drawing.Size(63, 15)
        Me.Label48.TabIndex = 32
        Me.Label48.Text = "Mod Date:"
        Me.Label48.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblModDate
        '
        Me.lblModDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblModDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblModDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModDate.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModDate.Location = New System.Drawing.Point(571, 244)
        Me.lblModDate.Name = "lblModDate"
        Me.lblModDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModDate.Size = New System.Drawing.Size(69, 19)
        Me.lblModDate.TabIndex = 31
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(54, 194)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(95, 13)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "Service Provider :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtAnnexTitle)
        Me.Frame5.Controls.Add(Me.SprdAnnex)
        Me.Frame5.Controls.Add(Me.Label27)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(-4996, 22)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(739, 269)
        Me.Frame5.TabIndex = 12
        Me.Frame5.TabStop = False
        '
        'txtAnnexTitle
        '
        Me.txtAnnexTitle.AcceptsReturn = True
        Me.txtAnnexTitle.BackColor = System.Drawing.SystemColors.Window
        Me.txtAnnexTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAnnexTitle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAnnexTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnnexTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtAnnexTitle.Location = New System.Drawing.Point(84, 12)
        Me.txtAnnexTitle.MaxLength = 0
        Me.txtAnnexTitle.Name = "txtAnnexTitle"
        Me.txtAnnexTitle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAnnexTitle.Size = New System.Drawing.Size(649, 22)
        Me.txtAnnexTitle.TabIndex = 13
        '
        'SprdAnnex
        '
        Me.SprdAnnex.DataSource = Nothing
        Me.SprdAnnex.Location = New System.Drawing.Point(4, 34)
        Me.SprdAnnex.Name = "SprdAnnex"
        Me.SprdAnnex.OcxState = CType(resources.GetObject("SprdAnnex.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdAnnex.Size = New System.Drawing.Size(731, 233)
        Me.SprdAnnex.TabIndex = 14
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.SystemColors.Control
        Me.Label27.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label27.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label27.Location = New System.Drawing.Point(26, 14)
        Me.Label27.Name = "Label27"
        Me.Label27.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label27.Size = New System.Drawing.Size(56, 13)
        Me.Label27.TabIndex = 15
        Me.Label27.Text = "Heading :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.FraMovement.Location = New System.Drawing.Point(664, 572)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 49)
        Me.FraMovement.TabIndex = 10
        Me.FraMovement.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(458, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(54, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Division :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmParamScrapRecStm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.cboDivision)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamScrapRecStm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Material Scrap Reconciliation Report"
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabMain.ResumeLayout(False)
        Me._TabMain_TabPage0.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        CType(Me.sprdOpening, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage1.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        CType(Me.sprdProcess, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage2.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        CType(Me.sprdComponent, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage3.ResumeLayout(False)
        Me.Frame10.ResumeLayout(False)
        CType(Me.sprdOthScrap, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage4.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        CType(Me.SprdSale, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage5.ResumeLayout(False)
        Me.Frame9.ResumeLayout(False)
        CType(Me.sprdClosing, System.ComponentModel.ISupportInitialize).EndInit()
        Me._TabMain_TabPage6.ResumeLayout(False)
        Me.Frame11.ResumeLayout(False)
        CType(Me.sprdSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        CType(Me.SprdAnnex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
End Class