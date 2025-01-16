Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamPOReg
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
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents Frame14 As System.Windows.Forms.GroupBox
    Public WithEvents cboItemLock As System.Windows.Forms.ComboBox
    Public WithEvents Frame13 As System.Windows.Forms.GroupBox
    Public WithEvents txtSubCategory As System.Windows.Forms.TextBox
    Public WithEvents cmdSubCatsearch As System.Windows.Forms.Button
    Public WithEvents chkAllSubCat As System.Windows.Forms.CheckBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents cboStatus As System.Windows.Forms.ComboBox
    Public WithEvents cboOrderType As System.Windows.Forms.ComboBox
    Public WithEvents cboPurType As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkCategoryWise As System.Windows.Forms.CheckBox
    Public WithEvents txtPONo As System.Windows.Forms.TextBox
    Public WithEvents chkRecdPo As System.Windows.Forms.CheckBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchSupp As System.Windows.Forms.Button
    Public WithEvents chkAllSupp As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtItemName As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents _OptDate_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDate_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptDate_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _OptShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cboSuppType As System.Windows.Forms.ComboBox
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents cboItemType As System.Windows.Forms.ComboBox
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents cboExportItem As System.Windows.Forms.ComboBox
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents cboClassification As System.Windows.Forms.ComboBox
    Public WithEvents Frame11 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame12 As System.Windows.Forms.GroupBox
    Public WithEvents txtCategory As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchCategory As System.Windows.Forms.Button
    Public WithEvents chkAllCategory As System.Windows.Forms.CheckBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
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
    Public WithEvents OptDate As VB6.RadioButtonArray
    Public WithEvents OptOrderBy As VB6.RadioButtonArray
    Public WithEvents OptShow As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamPOReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtSubCategory = New System.Windows.Forms.TextBox()
        Me.cmdSubCatsearch = New System.Windows.Forms.Button()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.cmdsearchSupp = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.cmdsearchCategory = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame14 = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Frame13 = New System.Windows.Forms.GroupBox()
        Me.cboItemLock = New System.Windows.Forms.ComboBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.chkAllSubCat = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.cboOrderType = New System.Windows.Forms.ComboBox()
        Me.cboPurType = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkCapital = New System.Windows.Forms.CheckBox()
        Me.chkCategoryWise = New System.Windows.Forms.CheckBox()
        Me.chkRecdPo = New System.Windows.Forms.CheckBox()
        Me.chkAllSupp = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me._OptDate_2 = New System.Windows.Forms.RadioButton()
        Me._OptDate_1 = New System.Windows.Forms.RadioButton()
        Me._OptDate_0 = New System.Windows.Forms.RadioButton()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._OptShow_2 = New System.Windows.Forms.RadioButton()
        Me._OptShow_1 = New System.Windows.Forms.RadioButton()
        Me._OptShow_0 = New System.Windows.Forms.RadioButton()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cboSuppType = New System.Windows.Forms.ComboBox()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.cboItemType = New System.Windows.Forms.ComboBox()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.cboExportItem = New System.Windows.Forms.ComboBox()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.cboClassification = New System.Windows.Forms.ComboBox()
        Me.Frame12 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkAllCategory = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.lblTrnType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptDate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame14.SuspendLayout()
        Me.Frame13.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.Frame10.SuspendLayout()
        Me.Frame11.SuspendLayout()
        Me.Frame12.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtSubCategory
        '
        Me.txtSubCategory.AcceptsReturn = True
        Me.txtSubCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubCategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubCategory.Location = New System.Drawing.Point(5, 14)
        Me.txtSubCategory.MaxLength = 0
        Me.txtSubCategory.Name = "txtSubCategory"
        Me.txtSubCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubCategory.Size = New System.Drawing.Size(229, 22)
        Me.txtSubCategory.TabIndex = 61
        Me.ToolTip1.SetToolTip(Me.txtSubCategory, "Press F1 For Help")
        '
        'cmdSubCatsearch
        '
        Me.cmdSubCatsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSubCatsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSubCatsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSubCatsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSubCatsearch.Image = CType(resources.GetObject("cmdSubCatsearch.Image"), System.Drawing.Image)
        Me.cmdSubCatsearch.Location = New System.Drawing.Point(239, 14)
        Me.cmdSubCatsearch.Name = "cmdSubCatsearch"
        Me.cmdSubCatsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSubCatsearch.Size = New System.Drawing.Size(29, 22)
        Me.cmdSubCatsearch.TabIndex = 60
        Me.cmdSubCatsearch.TabStop = False
        Me.cmdSubCatsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSubCatsearch, "Search")
        Me.cmdSubCatsearch.UseVisualStyleBackColor = False
        '
        'txtPONo
        '
        Me.txtPONo.AcceptsReturn = True
        Me.txtPONo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPONo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPONo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPONo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPONo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPONo.Location = New System.Drawing.Point(73, 73)
        Me.txtPONo.MaxLength = 0
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPONo.Size = New System.Drawing.Size(87, 22)
        Me.txtPONo.TabIndex = 38
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
        Me.txtSupplier.Location = New System.Drawing.Point(72, 13)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(316, 22)
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
        Me.cmdsearchSupp.Location = New System.Drawing.Point(390, 11)
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
        Me.cmdsearch.Location = New System.Drawing.Point(390, 43)
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
        Me.TxtItemName.Location = New System.Drawing.Point(72, 43)
        Me.TxtItemName.MaxLength = 0
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtItemName.Size = New System.Drawing.Size(316, 22)
        Me.TxtItemName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtItemName, "Press F1 For Help")
        '
        'txtCategory
        '
        Me.txtCategory.AcceptsReturn = True
        Me.txtCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCategory.Location = New System.Drawing.Point(2, 14)
        Me.txtCategory.MaxLength = 0
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCategory.Size = New System.Drawing.Size(280, 22)
        Me.txtCategory.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.txtCategory, "Press F1 For Help")
        '
        'cmdsearchCategory
        '
        Me.cmdsearchCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchCategory.Image = CType(resources.GetObject("cmdsearchCategory.Image"), System.Drawing.Image)
        Me.cmdsearchCategory.Location = New System.Drawing.Point(285, 12)
        Me.cmdsearchCategory.Name = "cmdsearchCategory"
        Me.cmdsearchCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchCategory.Size = New System.Drawing.Size(29, 24)
        Me.cmdsearchCategory.TabIndex = 42
        Me.cmdsearchCategory.TabStop = False
        Me.cmdsearchCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchCategory, "Search")
        Me.cmdsearchCategory.UseVisualStyleBackColor = False
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
        Me.cmdPrint.Location = New System.Drawing.Point(63, 9)
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
        Me.cmdClose.Location = New System.Drawing.Point(184, 9)
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
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 6
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame14
        '
        Me.Frame14.BackColor = System.Drawing.SystemColors.Control
        Me.Frame14.Controls.Add(Me.cboShow)
        Me.Frame14.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame14.Location = New System.Drawing.Point(720, 102)
        Me.Frame14.Name = "Frame14"
        Me.Frame14.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame14.Size = New System.Drawing.Size(178, 44)
        Me.Frame14.TabIndex = 67
        Me.Frame14.TabStop = False
        Me.Frame14.Text = "Show"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(5, 15)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(169, 21)
        Me.cboShow.TabIndex = 68
        '
        'Frame13
        '
        Me.Frame13.BackColor = System.Drawing.SystemColors.Control
        Me.Frame13.Controls.Add(Me.cboItemLock)
        Me.Frame13.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame13.Location = New System.Drawing.Point(483, 559)
        Me.Frame13.Name = "Frame13"
        Me.Frame13.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame13.Size = New System.Drawing.Size(167, 51)
        Me.Frame13.TabIndex = 65
        Me.Frame13.TabStop = False
        Me.Frame13.Text = "Lock / Unlock Item"
        '
        'cboItemLock
        '
        Me.cboItemLock.BackColor = System.Drawing.SystemColors.Window
        Me.cboItemLock.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboItemLock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemLock.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboItemLock.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboItemLock.Location = New System.Drawing.Point(23, 20)
        Me.cboItemLock.Name = "cboItemLock"
        Me.cboItemLock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboItemLock.Size = New System.Drawing.Size(131, 21)
        Me.cboItemLock.TabIndex = 66
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.txtSubCategory)
        Me.Frame7.Controls.Add(Me.cmdSubCatsearch)
        Me.Frame7.Controls.Add(Me.chkAllSubCat)
        Me.Frame7.Controls.Add(Me.Label9)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(574, 143)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(324, 43)
        Me.Frame7.TabIndex = 58
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Sub Category"
        '
        'chkAllSubCat
        '
        Me.chkAllSubCat.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllSubCat.Checked = True
        Me.chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllSubCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllSubCat.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllSubCat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllSubCat.Location = New System.Drawing.Point(274, 16)
        Me.chkAllSubCat.Name = "chkAllSubCat"
        Me.chkAllSubCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllSubCat.Size = New System.Drawing.Size(48, 16)
        Me.chkAllSubCat.TabIndex = 59
        Me.chkAllSubCat.Text = "ALL"
        Me.chkAllSubCat.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(377, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(82, 13)
        Me.Label9.TabIndex = 62
        Me.Label9.Text = "Sub Category :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Frame1.Location = New System.Drawing.Point(687, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(211, 102)
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
        Me.cboStatus.Size = New System.Drawing.Size(124, 21)
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
        Me.cboOrderType.Size = New System.Drawing.Size(124, 21)
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
        Me.cboPurType.Size = New System.Drawing.Size(124, 21)
        Me.cboPurType.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(35, 76)
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
        Me.Label3.Location = New System.Drawing.Point(10, 47)
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
        Me.Label1.Location = New System.Drawing.Point(4, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Pur Type :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkCapital)
        Me.FraAccount.Controls.Add(Me.chkCategoryWise)
        Me.FraAccount.Controls.Add(Me.txtPONo)
        Me.FraAccount.Controls.Add(Me.chkRecdPo)
        Me.FraAccount.Controls.Add(Me.txtSupplier)
        Me.FraAccount.Controls.Add(Me.cmdsearchSupp)
        Me.FraAccount.Controls.Add(Me.chkAllSupp)
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtItemName)
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Controls.Add(Me.Label5)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(197, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(487, 102)
        Me.FraAccount.TabIndex = 13
        Me.FraAccount.TabStop = False
        '
        'chkCapital
        '
        Me.chkCapital.AutoSize = True
        Me.chkCapital.BackColor = System.Drawing.SystemColors.Control
        Me.chkCapital.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCapital.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCapital.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCapital.Location = New System.Drawing.Point(377, 77)
        Me.chkCapital.Name = "chkCapital"
        Me.chkCapital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCapital.Size = New System.Drawing.Size(62, 17)
        Me.chkCapital.TabIndex = 45
        Me.chkCapital.Text = "Capital"
        Me.chkCapital.UseVisualStyleBackColor = False
        '
        'chkCategoryWise
        '
        Me.chkCategoryWise.AutoSize = True
        Me.chkCategoryWise.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategoryWise.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategoryWise.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategoryWise.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategoryWise.Location = New System.Drawing.Point(271, 76)
        Me.chkCategoryWise.Name = "chkCategoryWise"
        Me.chkCategoryWise.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategoryWise.Size = New System.Drawing.Size(101, 17)
        Me.chkCategoryWise.TabIndex = 44
        Me.chkCategoryWise.Text = "Category Wise"
        Me.chkCategoryWise.UseVisualStyleBackColor = False
        '
        'chkRecdPo
        '
        Me.chkRecdPo.AutoSize = True
        Me.chkRecdPo.BackColor = System.Drawing.SystemColors.Control
        Me.chkRecdPo.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRecdPo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRecdPo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRecdPo.Location = New System.Drawing.Point(168, 76)
        Me.chkRecdPo.Name = "chkRecdPo"
        Me.chkRecdPo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRecdPo.Size = New System.Drawing.Size(98, 17)
        Me.chkRecdPo.TabIndex = 37
        Me.chkRecdPo.Text = "Show Recd PO"
        Me.chkRecdPo.UseVisualStyleBackColor = False
        '
        'chkAllSupp
        '
        Me.chkAllSupp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllSupp.Checked = True
        Me.chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllSupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllSupp.Location = New System.Drawing.Point(424, 15)
        Me.chkAllSupp.Name = "chkAllSupp"
        Me.chkAllSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllSupp.Size = New System.Drawing.Size(60, 17)
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
        Me.chkAll.Location = New System.Drawing.Point(423, 47)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(60, 17)
        Me.chkAll.TabIndex = 4
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(27, 72)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(45, 13)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "PO No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(16, 12)
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
        Me.Label2.Location = New System.Drawing.Point(3, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Item Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me._OptDate_2)
        Me.Frame6.Controls.Add(Me._OptDate_1)
        Me.Frame6.Controls.Add(Me._OptDate_0)
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
        Me.Frame6.Size = New System.Drawing.Size(194, 102)
        Me.Frame6.TabIndex = 10
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        '_OptDate_2
        '
        Me._OptDate_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptDate_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDate_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDate_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDate.SetIndex(Me._OptDate_2, CType(2, Short))
        Me._OptDate_2.Location = New System.Drawing.Point(121, 14)
        Me._OptDate_2.Name = "_OptDate_2"
        Me._OptDate_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDate_2.Size = New System.Drawing.Size(65, 18)
        Me._OptDate_2.TabIndex = 47
        Me._OptDate_2.TabStop = True
        Me._OptDate_2.Text = "Amend"
        Me._OptDate_2.UseVisualStyleBackColor = False
        '
        '_OptDate_1
        '
        Me._OptDate_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptDate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDate_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDate.SetIndex(Me._OptDate_1, CType(1, Short))
        Me._OptDate_1.Location = New System.Drawing.Point(64, 14)
        Me._OptDate_1.Name = "_OptDate_1"
        Me._OptDate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDate_1.Size = New System.Drawing.Size(39, 18)
        Me._OptDate_1.TabIndex = 46
        Me._OptDate_1.TabStop = True
        Me._OptDate_1.Text = "PO"
        Me._OptDate_1.UseVisualStyleBackColor = False
        '
        '_OptDate_0
        '
        Me._OptDate_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptDate_0.Checked = True
        Me._OptDate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptDate_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptDate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDate.SetIndex(Me._OptDate_0, CType(0, Short))
        Me._OptDate_0.Location = New System.Drawing.Point(4, 14)
        Me._OptDate_0.Name = "_OptDate_0"
        Me._OptDate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptDate_0.Size = New System.Drawing.Size(51, 18)
        Me._OptDate_0.TabIndex = 45
        Me._OptDate_0.TabStop = True
        Me._OptDate_0.Text = "WEF"
        Me._OptDate_0.UseVisualStyleBackColor = False
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(64, 38)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 22)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(64, 68)
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
        Me._Lbl_1.Location = New System.Drawing.Point(35, 72)
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
        Me._Lbl_0.Location = New System.Drawing.Point(26, 42)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(40, 13)
        Me._Lbl_0.TabIndex = 11
        Me._Lbl_0.Text = "From :"
        Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 559)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(262, 49)
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
        Me._OptOrderBy_0.Location = New System.Drawing.Point(9, 21)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(80, 21)
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
        Me._OptOrderBy_1.Location = New System.Drawing.Point(127, 21)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(130, 21)
        Me._OptOrderBy_1.TabIndex = 27
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Item Description"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._OptShow_2)
        Me.Frame3.Controls.Add(Me._OptShow_1)
        Me.Frame3.Controls.Add(Me._OptShow_0)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(264, 559)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(216, 49)
        Me.Frame3.TabIndex = 33
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Show Only"
        '
        '_OptShow_2
        '
        Me._OptShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_2, CType(2, Short))
        Me._OptShow_2.Location = New System.Drawing.Point(158, 21)
        Me._OptShow_2.Name = "_OptShow_2"
        Me._OptShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_2.Size = New System.Drawing.Size(54, 21)
        Me._OptShow_2.TabIndex = 36
        Me._OptShow_2.TabStop = True
        Me._OptShow_2.Text = "Both"
        Me._OptShow_2.UseVisualStyleBackColor = False
        '
        '_OptShow_1
        '
        Me._OptShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_1, CType(1, Short))
        Me._OptShow_1.Location = New System.Drawing.Point(82, 21)
        Me._OptShow_1.Name = "_OptShow_1"
        Me._OptShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_1.Size = New System.Drawing.Size(70, 21)
        Me._OptShow_1.TabIndex = 35
        Me._OptShow_1.TabStop = True
        Me._OptShow_1.Text = "Closed"
        Me._OptShow_1.UseVisualStyleBackColor = False
        '
        '_OptShow_0
        '
        Me._OptShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_0.Checked = True
        Me._OptShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_0, CType(0, Short))
        Me._OptShow_0.Location = New System.Drawing.Point(2, 21)
        Me._OptShow_0.Name = "_OptShow_0"
        Me._OptShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_0.Size = New System.Drawing.Size(66, 21)
        Me._OptShow_0.TabIndex = 34
        Me._OptShow_0.TabStop = True
        Me._OptShow_0.Text = "Active"
        Me._OptShow_0.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cboSuppType)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(0, 101)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(180, 44)
        Me.Frame8.TabIndex = 48
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Supplier Type"
        '
        'cboSuppType
        '
        Me.cboSuppType.BackColor = System.Drawing.SystemColors.Window
        Me.cboSuppType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboSuppType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSuppType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboSuppType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboSuppType.Location = New System.Drawing.Point(5, 15)
        Me.cboSuppType.Name = "cboSuppType"
        Me.cboSuppType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboSuppType.Size = New System.Drawing.Size(169, 21)
        Me.cboSuppType.TabIndex = 53
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.cboItemType)
        Me.Frame9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(0, 142)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(180, 44)
        Me.Frame9.TabIndex = 49
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Item Type "
        '
        'cboItemType
        '
        Me.cboItemType.BackColor = System.Drawing.SystemColors.Window
        Me.cboItemType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboItemType.Location = New System.Drawing.Point(5, 15)
        Me.cboItemType.Name = "cboItemType"
        Me.cboItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboItemType.Size = New System.Drawing.Size(169, 21)
        Me.cboItemType.TabIndex = 54
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.cboExportItem)
        Me.Frame10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(181, 101)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(171, 44)
        Me.Frame10.TabIndex = 50
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Export Item"
        '
        'cboExportItem
        '
        Me.cboExportItem.BackColor = System.Drawing.SystemColors.Window
        Me.cboExportItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboExportItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExportItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboExportItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboExportItem.Location = New System.Drawing.Point(5, 15)
        Me.cboExportItem.Name = "cboExportItem"
        Me.cboExportItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboExportItem.Size = New System.Drawing.Size(159, 21)
        Me.cboExportItem.TabIndex = 55
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.cboClassification)
        Me.Frame11.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame11.Location = New System.Drawing.Point(354, 101)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(180, 44)
        Me.Frame11.TabIndex = 51
        Me.Frame11.TabStop = False
        Me.Frame11.Text = "Classification"
        '
        'cboClassification
        '
        Me.cboClassification.BackColor = System.Drawing.SystemColors.Window
        Me.cboClassification.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboClassification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboClassification.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboClassification.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboClassification.Location = New System.Drawing.Point(5, 15)
        Me.cboClassification.Name = "cboClassification"
        Me.cboClassification.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboClassification.Size = New System.Drawing.Size(169, 21)
        Me.cboClassification.TabIndex = 56
        '
        'Frame12
        '
        Me.Frame12.BackColor = System.Drawing.SystemColors.Control
        Me.Frame12.Controls.Add(Me.cboDivision)
        Me.Frame12.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame12.Location = New System.Drawing.Point(538, 102)
        Me.Frame12.Name = "Frame12"
        Me.Frame12.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame12.Size = New System.Drawing.Size(180, 44)
        Me.Frame12.TabIndex = 52
        Me.Frame12.TabStop = False
        Me.Frame12.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(5, 15)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(169, 21)
        Me.cboDivision.TabIndex = 57
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtCategory)
        Me.Frame5.Controls.Add(Me.cmdsearchCategory)
        Me.Frame5.Controls.Add(Me.chkAllCategory)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(183, 143)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(389, 43)
        Me.Frame5.TabIndex = 40
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Category"
        '
        'chkAllCategory
        '
        Me.chkAllCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCategory.Checked = True
        Me.chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCategory.Location = New System.Drawing.Point(328, 14)
        Me.chkAllCategory.Name = "chkAllCategory"
        Me.chkAllCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCategory.Size = New System.Drawing.Size(58, 19)
        Me.chkAllCategory.TabIndex = 41
        Me.chkAllCategory.Text = "ALL"
        Me.chkAllCategory.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 180)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(898, 372)
        Me.Frame4.TabIndex = 14
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 11)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(892, 357)
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
        Me.FraMovement.Location = New System.Drawing.Point(654, 560)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 49)
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
        'frmParamPOReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame14)
        Me.Controls.Add(Me.Frame13)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame9)
        Me.Controls.Add(Me.Frame10)
        Me.Controls.Add(Me.Frame11)
        Me.Controls.Add(Me.Frame12)
        Me.Controls.Add(Me.Frame5)
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
        Me.Name = "frmParamPOReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Purchase Register"
        Me.Frame14.ResumeLayout(False)
        Me.Frame13.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.Frame9.ResumeLayout(False)
        Me.Frame10.ResumeLayout(False)
        Me.Frame11.ResumeLayout(False)
        Me.Frame12.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents chkCapital As CheckBox
#End Region
End Class