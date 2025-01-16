Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamStockTillMonth
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
    Public WithEvents cboExportItem As System.Windows.Forms.ComboBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkZeroBal As System.Windows.Forms.CheckBox
    Public WithEvents chkShowMinMax As System.Windows.Forms.CheckBox
    Public WithEvents chkDivision As System.Windows.Forms.CheckBox
    Public WithEvents txtDivision As System.Windows.Forms.TextBox
    Public WithEvents cmdDivision As System.Windows.Forms.Button
    Public WithEvents chkViewAll As System.Windows.Forms.CheckBox
    Public WithEvents chkCS As System.Windows.Forms.CheckBox
    Public WithEvents cmdItemDesc As System.Windows.Forms.Button
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents chkItemAll As System.Windows.Forms.CheckBox
    Public WithEvents chkSubCategory As System.Windows.Forms.CheckBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents txtCatName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCategory As System.Windows.Forms.Button
    Public WithEvents txtSubCatName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSubCat As System.Windows.Forms.Button
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents chkOption As System.Windows.Forms.CheckBox
    Public WithEvents txtCondQty As System.Windows.Forms.TextBox
    Public WithEvents cboCond As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents FraOption As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamStockTillMonth))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdDivision = New System.Windows.Forms.Button()
        Me.cmdItemDesc = New System.Windows.Forms.Button()
        Me.cmdSearchCategory = New System.Windows.Forms.Button()
        Me.cmdSearchSubCat = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboExportItem = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkZeroBal = New System.Windows.Forms.CheckBox()
        Me.chkShowMinMax = New System.Windows.Forms.CheckBox()
        Me.chkDivision = New System.Windows.Forms.CheckBox()
        Me.txtDivision = New System.Windows.Forms.TextBox()
        Me.chkViewAll = New System.Windows.Forms.CheckBox()
        Me.chkCS = New System.Windows.Forms.CheckBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.chkItemAll = New System.Windows.Forms.CheckBox()
        Me.chkSubCategory = New System.Windows.Forms.CheckBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.txtCatName = New System.Windows.Forms.TextBox()
        Me.txtSubCatName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkOption = New System.Windows.Forms.CheckBox()
        Me.FraOption = New System.Windows.Forms.GroupBox()
        Me.txtCondQty = New System.Windows.Forms.TextBox()
        Me.cboCond = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame6.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.FraOption.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdDivision
        '
        Me.cmdDivision.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDivision.Enabled = False
        Me.cmdDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDivision.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDivision.Image = CType(resources.GetObject("cmdDivision.Image"), System.Drawing.Image)
        Me.cmdDivision.Location = New System.Drawing.Point(451, 103)
        Me.cmdDivision.Name = "cmdDivision"
        Me.cmdDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDivision.Size = New System.Drawing.Size(28, 23)
        Me.cmdDivision.TabIndex = 29
        Me.cmdDivision.TabStop = False
        Me.cmdDivision.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDivision, "Search")
        Me.cmdDivision.UseVisualStyleBackColor = False
        '
        'cmdItemDesc
        '
        Me.cmdItemDesc.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdItemDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdItemDesc.Enabled = False
        Me.cmdItemDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdItemDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdItemDesc.Image = CType(resources.GetObject("cmdItemDesc.Image"), System.Drawing.Image)
        Me.cmdItemDesc.Location = New System.Drawing.Point(451, 72)
        Me.cmdItemDesc.Name = "cmdItemDesc"
        Me.cmdItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdItemDesc.Size = New System.Drawing.Size(28, 23)
        Me.cmdItemDesc.TabIndex = 25
        Me.cmdItemDesc.TabStop = False
        Me.cmdItemDesc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdItemDesc, "Search")
        Me.cmdItemDesc.UseVisualStyleBackColor = False
        '
        'cmdSearchCategory
        '
        Me.cmdSearchCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCategory.Enabled = False
        Me.cmdSearchCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCategory.Image = CType(resources.GetObject("cmdSearchCategory.Image"), System.Drawing.Image)
        Me.cmdSearchCategory.Location = New System.Drawing.Point(451, 10)
        Me.cmdSearchCategory.Name = "cmdSearchCategory"
        Me.cmdSearchCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCategory.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchCategory.TabIndex = 17
        Me.cmdSearchCategory.TabStop = False
        Me.cmdSearchCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCategory, "Search")
        Me.cmdSearchCategory.UseVisualStyleBackColor = False
        '
        'cmdSearchSubCat
        '
        Me.cmdSearchSubCat.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchSubCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchSubCat.Enabled = False
        Me.cmdSearchSubCat.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchSubCat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchSubCat.Image = CType(resources.GetObject("cmdSearchSubCat.Image"), System.Drawing.Image)
        Me.cmdSearchSubCat.Location = New System.Drawing.Point(451, 41)
        Me.cmdSearchSubCat.Name = "cmdSearchSubCat"
        Me.cmdSearchSubCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchSubCat.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchSubCat.TabIndex = 15
        Me.cmdSearchSubCat.TabStop = False
        Me.cmdSearchSubCat.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchSubCat, "Search")
        Me.cmdSearchSubCat.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(126, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 4
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
        Me.cmdPrint.Location = New System.Drawing.Point(66, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 3
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(6, 10)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 2
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.Location = New System.Drawing.Point(186, 10)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(60, 37)
        Me.cmdExit.TabIndex = 5
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboExportItem)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 77)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(142, 54)
        Me.Frame1.TabIndex = 33
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Export Item"
        '
        'cboExportItem
        '
        Me.cboExportItem.BackColor = System.Drawing.SystemColors.Window
        Me.cboExportItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboExportItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExportItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboExportItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboExportItem.Location = New System.Drawing.Point(46, 20)
        Me.cboExportItem.Name = "cboExportItem"
        Me.cboExportItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboExportItem.Size = New System.Drawing.Size(90, 21)
        Me.cboExportItem.TabIndex = 34
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkZeroBal)
        Me.Frame3.Controls.Add(Me.chkShowMinMax)
        Me.Frame3.Controls.Add(Me.chkDivision)
        Me.Frame3.Controls.Add(Me.txtDivision)
        Me.Frame3.Controls.Add(Me.cmdDivision)
        Me.Frame3.Controls.Add(Me.chkViewAll)
        Me.Frame3.Controls.Add(Me.chkCS)
        Me.Frame3.Controls.Add(Me.cmdItemDesc)
        Me.Frame3.Controls.Add(Me.txtItemName)
        Me.Frame3.Controls.Add(Me.chkItemAll)
        Me.Frame3.Controls.Add(Me.chkSubCategory)
        Me.Frame3.Controls.Add(Me.chkCategory)
        Me.Frame3.Controls.Add(Me.txtCatName)
        Me.Frame3.Controls.Add(Me.cmdSearchCategory)
        Me.Frame3.Controls.Add(Me.txtSubCatName)
        Me.Frame3.Controls.Add(Me.cmdSearchSubCat)
        Me.Frame3.Controls.Add(Me.Label6)
        Me.Frame3.Controls.Add(Me.Label5)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Controls.Add(Me.Label1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(145, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(751, 132)
        Me.Frame3.TabIndex = 14
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Show"
        '
        'chkZeroBal
        '
        Me.chkZeroBal.BackColor = System.Drawing.SystemColors.Control
        Me.chkZeroBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkZeroBal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkZeroBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkZeroBal.Location = New System.Drawing.Point(574, 105)
        Me.chkZeroBal.Name = "chkZeroBal"
        Me.chkZeroBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkZeroBal.Size = New System.Drawing.Size(159, 15)
        Me.chkZeroBal.TabIndex = 36
        Me.chkZeroBal.Text = "Hide Zero Balance"
        Me.chkZeroBal.UseVisualStyleBackColor = False
        '
        'chkShowMinMax
        '
        Me.chkShowMinMax.BackColor = System.Drawing.SystemColors.Control
        Me.chkShowMinMax.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShowMinMax.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowMinMax.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShowMinMax.Location = New System.Drawing.Point(574, 74)
        Me.chkShowMinMax.Name = "chkShowMinMax"
        Me.chkShowMinMax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShowMinMax.Size = New System.Drawing.Size(159, 15)
        Me.chkShowMinMax.TabIndex = 35
        Me.chkShowMinMax.Text = "Show Min && Max "
        Me.chkShowMinMax.UseVisualStyleBackColor = False
        '
        'chkDivision
        '
        Me.chkDivision.BackColor = System.Drawing.SystemColors.Control
        Me.chkDivision.Checked = True
        Me.chkDivision.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDivision.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDivision.Location = New System.Drawing.Point(489, 105)
        Me.chkDivision.Name = "chkDivision"
        Me.chkDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDivision.Size = New System.Drawing.Size(50, 18)
        Me.chkDivision.TabIndex = 31
        Me.chkDivision.Text = "All"
        Me.chkDivision.UseVisualStyleBackColor = False
        '
        'txtDivision
        '
        Me.txtDivision.AcceptsReturn = True
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDivision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDivision.Enabled = False
        Me.txtDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDivision.ForeColor = System.Drawing.Color.Blue
        Me.txtDivision.Location = New System.Drawing.Point(112, 103)
        Me.txtDivision.MaxLength = 0
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDivision.Size = New System.Drawing.Size(334, 22)
        Me.txtDivision.TabIndex = 30
        '
        'chkViewAll
        '
        Me.chkViewAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkViewAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkViewAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkViewAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkViewAll.Location = New System.Drawing.Point(574, 43)
        Me.chkViewAll.Name = "chkViewAll"
        Me.chkViewAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkViewAll.Size = New System.Drawing.Size(159, 15)
        Me.chkViewAll.TabIndex = 28
        Me.chkViewAll.Text = "View All"
        Me.chkViewAll.UseVisualStyleBackColor = False
        '
        'chkCS
        '
        Me.chkCS.BackColor = System.Drawing.SystemColors.Control
        Me.chkCS.Checked = True
        Me.chkCS.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCS.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCS.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCS.Location = New System.Drawing.Point(574, 12)
        Me.chkCS.Name = "chkCS"
        Me.chkCS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCS.Size = New System.Drawing.Size(159, 15)
        Me.chkCS.TabIndex = 27
        Me.chkCS.Text = "Included Customer Stock"
        Me.chkCS.UseVisualStyleBackColor = False
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Enabled = False
        Me.txtItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.ForeColor = System.Drawing.Color.Blue
        Me.txtItemName.Location = New System.Drawing.Point(112, 72)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(334, 22)
        Me.txtItemName.TabIndex = 24
        '
        'chkItemAll
        '
        Me.chkItemAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkItemAll.Checked = True
        Me.chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkItemAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkItemAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkItemAll.Location = New System.Drawing.Point(489, 74)
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkItemAll.Size = New System.Drawing.Size(50, 18)
        Me.chkItemAll.TabIndex = 23
        Me.chkItemAll.Text = "All"
        Me.chkItemAll.UseVisualStyleBackColor = False
        '
        'chkSubCategory
        '
        Me.chkSubCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkSubCategory.Checked = True
        Me.chkSubCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSubCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSubCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSubCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSubCategory.Location = New System.Drawing.Point(489, 43)
        Me.chkSubCategory.Name = "chkSubCategory"
        Me.chkSubCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSubCategory.Size = New System.Drawing.Size(50, 18)
        Me.chkSubCategory.TabIndex = 22
        Me.chkSubCategory.Text = "All"
        Me.chkSubCategory.UseVisualStyleBackColor = False
        '
        'chkCategory
        '
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Checked = True
        Me.chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(489, 12)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(50, 18)
        Me.chkCategory.TabIndex = 21
        Me.chkCategory.Text = "All"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'txtCatName
        '
        Me.txtCatName.AcceptsReturn = True
        Me.txtCatName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCatName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCatName.Enabled = False
        Me.txtCatName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCatName.ForeColor = System.Drawing.Color.Blue
        Me.txtCatName.Location = New System.Drawing.Point(112, 10)
        Me.txtCatName.MaxLength = 0
        Me.txtCatName.Name = "txtCatName"
        Me.txtCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCatName.Size = New System.Drawing.Size(334, 22)
        Me.txtCatName.TabIndex = 18
        '
        'txtSubCatName
        '
        Me.txtSubCatName.AcceptsReturn = True
        Me.txtSubCatName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubCatName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubCatName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubCatName.Enabled = False
        Me.txtSubCatName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubCatName.ForeColor = System.Drawing.Color.Blue
        Me.txtSubCatName.Location = New System.Drawing.Point(112, 41)
        Me.txtSubCatName.MaxLength = 0
        Me.txtSubCatName.Name = "txtSubCatName"
        Me.txtSubCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubCatName.Size = New System.Drawing.Size(334, 22)
        Me.txtSubCatName.TabIndex = 16
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(54, 107)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Division :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(45, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Item Desc :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(26, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Sub Category :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(48, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Category :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 132)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(900, 432)
        Me.SprdMain.TabIndex = 1
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me.Label2)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(142, 64)
        Me.Frame6.TabIndex = 0
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "As On"
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(50, 24)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(75, 22)
        Me.txtDateTo.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(14, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(25, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "To :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(84, 138)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 34
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.chkOption)
        Me.Frame2.Controls.Add(Me.FraOption)
        Me.Frame2.Controls.Add(Me.CmdPreview)
        Me.Frame2.Controls.Add(Me.cmdPrint)
        Me.Frame2.Controls.Add(Me.cmdShow)
        Me.Frame2.Controls.Add(Me.cmdExit)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 562)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(898, 51)
        Me.Frame2.TabIndex = 10
        Me.Frame2.TabStop = False
        '
        'chkOption
        '
        Me.chkOption.BackColor = System.Drawing.SystemColors.Control
        Me.chkOption.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOption.Location = New System.Drawing.Point(448, 16)
        Me.chkOption.Name = "chkOption"
        Me.chkOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOption.Size = New System.Drawing.Size(89, 23)
        Me.chkOption.TabIndex = 6
        Me.chkOption.Text = "Conditional Check"
        Me.chkOption.UseVisualStyleBackColor = False
        '
        'FraOption
        '
        Me.FraOption.BackColor = System.Drawing.SystemColors.Control
        Me.FraOption.Controls.Add(Me.txtCondQty)
        Me.FraOption.Controls.Add(Me.cboCond)
        Me.FraOption.Controls.Add(Me.Label4)
        Me.FraOption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOption.Location = New System.Drawing.Point(554, 0)
        Me.FraOption.Name = "FraOption"
        Me.FraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOption.Size = New System.Drawing.Size(197, 45)
        Me.FraOption.TabIndex = 11
        Me.FraOption.TabStop = False
        Me.FraOption.Visible = False
        '
        'txtCondQty
        '
        Me.txtCondQty.AcceptsReturn = True
        Me.txtCondQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtCondQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCondQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCondQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCondQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCondQty.Location = New System.Drawing.Point(120, 16)
        Me.txtCondQty.MaxLength = 0
        Me.txtCondQty.Name = "txtCondQty"
        Me.txtCondQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCondQty.Size = New System.Drawing.Size(71, 22)
        Me.txtCondQty.TabIndex = 8
        '
        'cboCond
        '
        Me.cboCond.BackColor = System.Drawing.SystemColors.Window
        Me.cboCond.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCond.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCond.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCond.Location = New System.Drawing.Point(50, 16)
        Me.cboCond.Name = "cboCond"
        Me.cboCond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCond.Size = New System.Drawing.Size(65, 21)
        Me.cboCond.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(10, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Qty :"
        '
        'frmParamStockTillMonth
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Frame2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 16)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamStockTillMonth"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Inventory On Hand (Monthly)"
        Me.Frame1.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.FraOption.ResumeLayout(False)
        Me.FraOption.PerformLayout()
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