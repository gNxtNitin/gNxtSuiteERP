Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamStockDetailAll
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
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents txtAsOn As System.Windows.Forms.TextBox
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents chkRateRequired As System.Windows.Forms.CheckBox
    Public WithEvents chkViewAll As System.Windows.Forms.CheckBox
    Public WithEvents chkOthersStock As System.Windows.Forms.CheckBox
    Public WithEvents chkFGStock As System.Windows.Forms.CheckBox
    Public WithEvents chkWIPStock As System.Windows.Forms.CheckBox
    Public WithEvents chkProductionStock As System.Windows.Forms.CheckBox
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents _optShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtCategory As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchCategory As System.Windows.Forms.Button
    Public WithEvents chkAllCategory As System.Windows.Forms.CheckBox
    Public WithEvents chkAllSubCat As System.Windows.Forms.CheckBox
    Public WithEvents cmdSubCatsearch As System.Windows.Forms.Button
    Public WithEvents txtSubCategory As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents optShow As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamStockDetailAll))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.cmdsearchCategory = New System.Windows.Forms.Button()
        Me.cmdSubCatsearch = New System.Windows.Forms.Button()
        Me.txtSubCategory = New System.Windows.Forms.TextBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.txtAsOn = New System.Windows.Forms.TextBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkRateRequired = New System.Windows.Forms.CheckBox()
        Me.chkViewAll = New System.Windows.Forms.CheckBox()
        Me.chkOthersStock = New System.Windows.Forms.CheckBox()
        Me.chkFGStock = New System.Windows.Forms.CheckBox()
        Me.chkWIPStock = New System.Windows.Forms.CheckBox()
        Me.chkProductionStock = New System.Windows.Forms.CheckBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optShow_2 = New System.Windows.Forms.RadioButton()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkAllCategory = New System.Windows.Forms.CheckBox()
        Me.chkAllSubCat = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame3.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(816, 16)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearch.TabIndex = 20
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdExit
        '
        Me.cmdExit.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExit.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExit.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExit.Image = CType(resources.GetObject("cmdExit.Image"), System.Drawing.Image)
        Me.cmdExit.Location = New System.Drawing.Point(844, 16)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(60, 37)
        Me.cmdExit.TabIndex = 3
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(784, 16)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 37)
        Me.CmdPreview.TabIndex = 2
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
        Me.cmdPrint.Location = New System.Drawing.Point(724, 16)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 37)
        Me.cmdPrint.TabIndex = 1
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
        Me.cmdShow.Location = New System.Drawing.Point(664, 16)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 21
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'txtCategory
        '
        Me.txtCategory.AcceptsReturn = True
        Me.txtCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCategory.Location = New System.Drawing.Point(74, 12)
        Me.txtCategory.MaxLength = 0
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCategory.Size = New System.Drawing.Size(205, 22)
        Me.txtCategory.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.txtCategory, "Press F1 For Help")
        '
        'cmdsearchCategory
        '
        Me.cmdsearchCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchCategory.Image = CType(resources.GetObject("cmdsearchCategory.Image"), System.Drawing.Image)
        Me.cmdsearchCategory.Location = New System.Drawing.Point(279, 12)
        Me.cmdsearchCategory.Name = "cmdsearchCategory"
        Me.cmdsearchCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchCategory.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearchCategory.TabIndex = 15
        Me.cmdsearchCategory.TabStop = False
        Me.cmdsearchCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchCategory, "Search")
        Me.cmdsearchCategory.UseVisualStyleBackColor = False
        '
        'cmdSubCatsearch
        '
        Me.cmdSubCatsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSubCatsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSubCatsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSubCatsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSubCatsearch.Image = CType(resources.GetObject("cmdSubCatsearch.Image"), System.Drawing.Image)
        Me.cmdSubCatsearch.Location = New System.Drawing.Point(816, 12)
        Me.cmdSubCatsearch.Name = "cmdSubCatsearch"
        Me.cmdSubCatsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSubCatsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdSubCatsearch.TabIndex = 12
        Me.cmdSubCatsearch.TabStop = False
        Me.cmdSubCatsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSubCatsearch, "Search")
        Me.cmdSubCatsearch.UseVisualStyleBackColor = False
        '
        'txtSubCategory
        '
        Me.txtSubCategory.AcceptsReturn = True
        Me.txtSubCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubCategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubCategory.Location = New System.Drawing.Point(464, 12)
        Me.txtSubCategory.MaxLength = 0
        Me.txtSubCategory.Name = "txtSubCategory"
        Me.txtSubCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubCategory.Size = New System.Drawing.Size(348, 22)
        Me.txtSubCategory.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtSubCategory, "Press F1 For Help")
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdsearch)
        Me.Frame3.Controls.Add(Me.chkAll)
        Me.Frame3.Controls.Add(Me.txtAsOn)
        Me.Frame3.Controls.Add(Me.txtItemName)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Controls.Add(Me.Label1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, -4)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(908, 44)
        Me.Frame3.TabIndex = 5
        Me.Frame3.TabStop = False
        '
        'chkAll
        '
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(847, 19)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(51, 15)
        Me.chkAll.TabIndex = 19
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'txtAsOn
        '
        Me.txtAsOn.AcceptsReturn = True
        Me.txtAsOn.BackColor = System.Drawing.SystemColors.Window
        Me.txtAsOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAsOn.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAsOn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAsOn.ForeColor = System.Drawing.Color.Blue
        Me.txtAsOn.Location = New System.Drawing.Point(66, 16)
        Me.txtAsOn.MaxLength = 0
        Me.txtAsOn.Name = "txtAsOn"
        Me.txtAsOn.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAsOn.Size = New System.Drawing.Size(83, 22)
        Me.txtAsOn.TabIndex = 9
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.ForeColor = System.Drawing.Color.Blue
        Me.txtItemName.Location = New System.Drawing.Point(334, 16)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(476, 22)
        Me.txtItemName.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(6, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(55, 11)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "As On :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(224, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(107, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Item Description :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 78)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(910, 476)
        Me.SprdMain.TabIndex = 0
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(84, 138)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 6
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.chkRateRequired)
        Me.Frame2.Controls.Add(Me.chkViewAll)
        Me.Frame2.Controls.Add(Me.chkOthersStock)
        Me.Frame2.Controls.Add(Me.chkFGStock)
        Me.Frame2.Controls.Add(Me.chkWIPStock)
        Me.Frame2.Controls.Add(Me.chkProductionStock)
        Me.Frame2.Controls.Add(Me.cmdExit)
        Me.Frame2.Controls.Add(Me.CmdPreview)
        Me.Frame2.Controls.Add(Me.cmdPrint)
        Me.Frame2.Controls.Add(Me.cmdShow)
        Me.Frame2.Controls.Add(Me.Frame1)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 550)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(910, 69)
        Me.Frame2.TabIndex = 4
        Me.Frame2.TabStop = False
        '
        'chkRateRequired
        '
        Me.chkRateRequired.BackColor = System.Drawing.SystemColors.Control
        Me.chkRateRequired.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRateRequired.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRateRequired.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.chkRateRequired.Location = New System.Drawing.Point(340, 50)
        Me.chkRateRequired.Name = "chkRateRequired"
        Me.chkRateRequired.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRateRequired.Size = New System.Drawing.Size(153, 16)
        Me.chkRateRequired.TabIndex = 31
        Me.chkRateRequired.Text = "Rate Required"
        Me.chkRateRequired.UseVisualStyleBackColor = False
        '
        'chkViewAll
        '
        Me.chkViewAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkViewAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkViewAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkViewAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkViewAll.Location = New System.Drawing.Point(132, 52)
        Me.chkViewAll.Name = "chkViewAll"
        Me.chkViewAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkViewAll.Size = New System.Drawing.Size(73, 15)
        Me.chkViewAll.TabIndex = 30
        Me.chkViewAll.Text = "View All"
        Me.chkViewAll.UseVisualStyleBackColor = False
        '
        'chkOthersStock
        '
        Me.chkOthersStock.BackColor = System.Drawing.SystemColors.Control
        Me.chkOthersStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOthersStock.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOthersStock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOthersStock.Location = New System.Drawing.Point(340, 32)
        Me.chkOthersStock.Name = "chkOthersStock"
        Me.chkOthersStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOthersStock.Size = New System.Drawing.Size(153, 16)
        Me.chkOthersStock.TabIndex = 29
        Me.chkOthersStock.Text = "Others Stock (Include)"
        Me.chkOthersStock.UseVisualStyleBackColor = False
        '
        'chkFGStock
        '
        Me.chkFGStock.BackColor = System.Drawing.SystemColors.Control
        Me.chkFGStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFGStock.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFGStock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFGStock.Location = New System.Drawing.Point(340, 12)
        Me.chkFGStock.Name = "chkFGStock"
        Me.chkFGStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFGStock.Size = New System.Drawing.Size(135, 16)
        Me.chkFGStock.TabIndex = 28
        Me.chkFGStock.Text = "FG (Include)"
        Me.chkFGStock.UseVisualStyleBackColor = False
        '
        'chkWIPStock
        '
        Me.chkWIPStock.BackColor = System.Drawing.SystemColors.Control
        Me.chkWIPStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWIPStock.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWIPStock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWIPStock.Location = New System.Drawing.Point(132, 32)
        Me.chkWIPStock.Name = "chkWIPStock"
        Me.chkWIPStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWIPStock.Size = New System.Drawing.Size(181, 15)
        Me.chkWIPStock.TabIndex = 27
        Me.chkWIPStock.Text = "WIP Stock (Include)"
        Me.chkWIPStock.UseVisualStyleBackColor = False
        '
        'chkProductionStock
        '
        Me.chkProductionStock.BackColor = System.Drawing.SystemColors.Control
        Me.chkProductionStock.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkProductionStock.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkProductionStock.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkProductionStock.Location = New System.Drawing.Point(132, 12)
        Me.chkProductionStock.Name = "chkProductionStock"
        Me.chkProductionStock.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkProductionStock.Size = New System.Drawing.Size(197, 15)
        Me.chkProductionStock.TabIndex = 26
        Me.chkProductionStock.Text = "Production Stock (Include)"
        Me.chkProductionStock.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optShow_2)
        Me.Frame1.Controls.Add(Me._optShow_0)
        Me.Frame1.Controls.Add(Me._optShow_1)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(123, 69)
        Me.Frame1.TabIndex = 22
        Me.Frame1.TabStop = False
        '
        '_optShow_2
        '
        Me._optShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_2, CType(2, Short))
        Me._optShow_2.Location = New System.Drawing.Point(8, 50)
        Me._optShow_2.Name = "_optShow_2"
        Me._optShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_2.Size = New System.Drawing.Size(81, 16)
        Me._optShow_2.TabIndex = 25
        Me._optShow_2.TabStop = True
        Me._optShow_2.Text = "Sale Cost"
        Me._optShow_2.UseVisualStyleBackColor = False
        '
        '_optShow_0
        '
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Checked = True
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(8, 14)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(109, 16)
        Me._optShow_0.TabIndex = 24
        Me._optShow_0.TabStop = True
        Me._optShow_0.Text = "Purchase Cost"
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        '_optShow_1
        '
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(8, 32)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(95, 16)
        Me._optShow_1.TabIndex = 23
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "Landed Cost"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtCategory)
        Me.Frame5.Controls.Add(Me.cmdsearchCategory)
        Me.Frame5.Controls.Add(Me.chkAllCategory)
        Me.Frame5.Controls.Add(Me.chkAllSubCat)
        Me.Frame5.Controls.Add(Me.cmdSubCatsearch)
        Me.Frame5.Controls.Add(Me.txtSubCategory)
        Me.Frame5.Controls.Add(Me.Label4)
        Me.Frame5.Controls.Add(Me.Label6)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 35)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(908, 40)
        Me.Frame5.TabIndex = 10
        Me.Frame5.TabStop = False
        '
        'chkAllCategory
        '
        Me.chkAllCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCategory.Checked = True
        Me.chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCategory.Location = New System.Drawing.Point(309, 15)
        Me.chkAllCategory.Name = "chkAllCategory"
        Me.chkAllCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCategory.Size = New System.Drawing.Size(51, 15)
        Me.chkAllCategory.TabIndex = 14
        Me.chkAllCategory.Text = "ALL"
        Me.chkAllCategory.UseVisualStyleBackColor = False
        '
        'chkAllSubCat
        '
        Me.chkAllSubCat.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllSubCat.Checked = True
        Me.chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllSubCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllSubCat.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllSubCat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllSubCat.Location = New System.Drawing.Point(848, 14)
        Me.chkAllSubCat.Name = "chkAllSubCat"
        Me.chkAllSubCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllSubCat.Size = New System.Drawing.Size(51, 15)
        Me.chkAllSubCat.TabIndex = 13
        Me.chkAllSubCat.Text = "ALL"
        Me.chkAllSubCat.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(14, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Category :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(377, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Sub Category :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'frmParamStockDetailAll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame5)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamStockDetailAll"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Stock Detail"
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
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