Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamStockValuationFIFO
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
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkScrap As System.Windows.Forms.CheckBox
    Public WithEvents chkFG As System.Windows.Forms.CheckBox
    Public WithEvents chkWIP As System.Windows.Forms.CheckBox
    Public WithEvents chkCS As System.Windows.Forms.CheckBox
    Public WithEvents chkZeroBal As System.Windows.Forms.CheckBox
    Public WithEvents cmdItemDesc As System.Windows.Forms.Button
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents chkItemAll As System.Windows.Forms.CheckBox
    Public WithEvents chkSubCategory As System.Windows.Forms.CheckBox
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents txtCatName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCategory As System.Windows.Forms.Button
    Public WithEvents txtSubCatName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchSubCat As System.Windows.Forms.Button
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents chkViewAll As System.Windows.Forms.CheckBox
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
    Public WithEvents AData1 As VB6.ADODC
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_3 As System.Windows.Forms.RadioButton
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents optShow As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamStockValuationFIFO))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdItemDesc = New System.Windows.Forms.Button()
        Me.cmdSearchCategory = New System.Windows.Forms.Button()
        Me.cmdSearchSubCat = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkScrap = New System.Windows.Forms.CheckBox()
        Me.chkFG = New System.Windows.Forms.CheckBox()
        Me.chkWIP = New System.Windows.Forms.CheckBox()
        Me.chkCS = New System.Windows.Forms.CheckBox()
        Me.chkZeroBal = New System.Windows.Forms.CheckBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.chkItemAll = New System.Windows.Forms.CheckBox()
        Me.chkSubCategory = New System.Windows.Forms.CheckBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.txtCatName = New System.Windows.Forms.TextBox()
        Me.txtSubCatName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkViewAll = New System.Windows.Forms.CheckBox()
        Me.chkOption = New System.Windows.Forms.CheckBox()
        Me.FraOption = New System.Windows.Forms.GroupBox()
        Me.txtCondQty = New System.Windows.Forms.TextBox()
        Me.cboCond = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.AData1 = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me._optShow_2 = New System.Windows.Forms.RadioButton()
        Me._optShow_3 = New System.Windows.Forms.RadioButton()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame6.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.FraOption.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame8.SuspendLayout()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdItemDesc
        '
        Me.cmdItemDesc.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdItemDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdItemDesc.Enabled = False
        Me.cmdItemDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdItemDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdItemDesc.Image = CType(resources.GetObject("cmdItemDesc.Image"), System.Drawing.Image)
        Me.cmdItemDesc.Location = New System.Drawing.Point(416, 73)
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
        Me.cmdSearchCategory.Location = New System.Drawing.Point(416, 13)
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
        Me.cmdSearchSubCat.Location = New System.Drawing.Point(416, 43)
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
        Me.Frame1.Controls.Add(Me.cboShow)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 56)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(158, 48)
        Me.Frame1.TabIndex = 27
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Show"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(5, 17)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(141, 21)
        Me.cboShow.TabIndex = 28
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.chkScrap)
        Me.Frame3.Controls.Add(Me.chkFG)
        Me.Frame3.Controls.Add(Me.chkWIP)
        Me.Frame3.Controls.Add(Me.chkCS)
        Me.Frame3.Controls.Add(Me.chkZeroBal)
        Me.Frame3.Controls.Add(Me.cmdItemDesc)
        Me.Frame3.Controls.Add(Me.txtItemName)
        Me.Frame3.Controls.Add(Me.chkItemAll)
        Me.Frame3.Controls.Add(Me.chkSubCategory)
        Me.Frame3.Controls.Add(Me.chkCategory)
        Me.Frame3.Controls.Add(Me.txtCatName)
        Me.Frame3.Controls.Add(Me.cmdSearchCategory)
        Me.Frame3.Controls.Add(Me.txtSubCatName)
        Me.Frame3.Controls.Add(Me.cmdSearchSubCat)
        Me.Frame3.Controls.Add(Me.Label5)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Controls.Add(Me.Label1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(161, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(737, 104)
        Me.Frame3.TabIndex = 14
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Show"
        '
        'chkScrap
        '
        Me.chkScrap.BackColor = System.Drawing.SystemColors.Control
        Me.chkScrap.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkScrap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkScrap.Location = New System.Drawing.Point(537, 77)
        Me.chkScrap.Name = "chkScrap"
        Me.chkScrap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkScrap.Size = New System.Drawing.Size(185, 17)
        Me.chkScrap.TabIndex = 41
        Me.chkScrap.Text = "Included Scrap Stock"
        Me.chkScrap.UseVisualStyleBackColor = False
        '
        'chkFG
        '
        Me.chkFG.BackColor = System.Drawing.SystemColors.Control
        Me.chkFG.Checked = True
        Me.chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFG.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFG.Location = New System.Drawing.Point(537, 61)
        Me.chkFG.Name = "chkFG"
        Me.chkFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFG.Size = New System.Drawing.Size(185, 17)
        Me.chkFG.TabIndex = 40
        Me.chkFG.Text = "Included FG/CR Stock"
        Me.chkFG.UseVisualStyleBackColor = False
        '
        'chkWIP
        '
        Me.chkWIP.BackColor = System.Drawing.SystemColors.Control
        Me.chkWIP.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWIP.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWIP.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWIP.Location = New System.Drawing.Point(537, 45)
        Me.chkWIP.Name = "chkWIP"
        Me.chkWIP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWIP.Size = New System.Drawing.Size(185, 17)
        Me.chkWIP.TabIndex = 36
        Me.chkWIP.Text = "Included WIP Stock"
        Me.chkWIP.UseVisualStyleBackColor = False
        '
        'chkCS
        '
        Me.chkCS.BackColor = System.Drawing.SystemColors.Control
        Me.chkCS.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCS.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCS.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCS.Location = New System.Drawing.Point(537, 29)
        Me.chkCS.Name = "chkCS"
        Me.chkCS.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCS.Size = New System.Drawing.Size(185, 17)
        Me.chkCS.TabIndex = 35
        Me.chkCS.Text = "Included Customer Stock"
        Me.chkCS.UseVisualStyleBackColor = False
        '
        'chkZeroBal
        '
        Me.chkZeroBal.BackColor = System.Drawing.SystemColors.Control
        Me.chkZeroBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkZeroBal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkZeroBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkZeroBal.Location = New System.Drawing.Point(537, 13)
        Me.chkZeroBal.Name = "chkZeroBal"
        Me.chkZeroBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkZeroBal.Size = New System.Drawing.Size(185, 17)
        Me.chkZeroBal.TabIndex = 34
        Me.chkZeroBal.Text = "Hide Zero Balance"
        Me.chkZeroBal.UseVisualStyleBackColor = False
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
        Me.txtItemName.Location = New System.Drawing.Point(101, 73)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(313, 22)
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
        Me.chkItemAll.Location = New System.Drawing.Point(452, 75)
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkItemAll.Size = New System.Drawing.Size(54, 16)
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
        Me.chkSubCategory.Location = New System.Drawing.Point(452, 45)
        Me.chkSubCategory.Name = "chkSubCategory"
        Me.chkSubCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSubCategory.Size = New System.Drawing.Size(54, 16)
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
        Me.chkCategory.Location = New System.Drawing.Point(452, 15)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(54, 16)
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
        Me.txtCatName.Location = New System.Drawing.Point(101, 13)
        Me.txtCatName.MaxLength = 0
        Me.txtCatName.Name = "txtCatName"
        Me.txtCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCatName.Size = New System.Drawing.Size(313, 22)
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
        Me.txtSubCatName.Location = New System.Drawing.Point(101, 43)
        Me.txtSubCatName.MaxLength = 0
        Me.txtSubCatName.Name = "txtSubCatName"
        Me.txtSubCatName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubCatName.Size = New System.Drawing.Size(313, 22)
        Me.txtSubCatName.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(33, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Item Desc :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(14, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(82, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Sub Category :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(36, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Category :"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 148)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(898, 412)
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
        Me.Frame6.Size = New System.Drawing.Size(158, 52)
        Me.Frame6.TabIndex = 0
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "As On"
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(48, 16)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(102, 22)
        Me.txtDateTo.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(18, 20)
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
        Me.Report1.TabIndex = 28
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.chkViewAll)
        Me.Frame2.Controls.Add(Me.chkOption)
        Me.Frame2.Controls.Add(Me.FraOption)
        Me.Frame2.Controls.Add(Me.CmdPreview)
        Me.Frame2.Controls.Add(Me.cmdPrint)
        Me.Frame2.Controls.Add(Me.cmdShow)
        Me.Frame2.Controls.Add(Me.cmdExit)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 560)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(898, 51)
        Me.Frame2.TabIndex = 10
        Me.Frame2.TabStop = False
        '
        'chkViewAll
        '
        Me.chkViewAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkViewAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkViewAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkViewAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkViewAll.Location = New System.Drawing.Point(355, 16)
        Me.chkViewAll.Name = "chkViewAll"
        Me.chkViewAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkViewAll.Size = New System.Drawing.Size(73, 20)
        Me.chkViewAll.TabIndex = 37
        Me.chkViewAll.Text = "View All"
        Me.chkViewAll.UseVisualStyleBackColor = False
        '
        'chkOption
        '
        Me.chkOption.BackColor = System.Drawing.SystemColors.Control
        Me.chkOption.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOption.Location = New System.Drawing.Point(452, 15)
        Me.chkOption.Name = "chkOption"
        Me.chkOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOption.Size = New System.Drawing.Size(138, 22)
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
        Me.FraOption.Location = New System.Drawing.Point(594, 0)
        Me.FraOption.Name = "FraOption"
        Me.FraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOption.Size = New System.Drawing.Size(155, 45)
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
        Me.txtCondQty.Location = New System.Drawing.Point(88, 16)
        Me.txtCondQty.MaxLength = 0
        Me.txtCondQty.Name = "txtCondQty"
        Me.txtCondQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCondQty.Size = New System.Drawing.Size(61, 22)
        Me.txtCondQty.TabIndex = 8
        '
        'cboCond
        '
        Me.cboCond.BackColor = System.Drawing.SystemColors.Window
        Me.cboCond.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCond.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCond.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCond.Location = New System.Drawing.Point(36, 16)
        Me.cboCond.Name = "cboCond"
        Me.cboCond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCond.Size = New System.Drawing.Size(51, 21)
        Me.cboCond.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(6, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Qty :"
        '
        'AData1
        '
        Me.AData1.BackColor = System.Drawing.SystemColors.Window
        Me.AData1.CommandTimeout = 0
        Me.AData1.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.AData1.ConnectionString = Nothing
        Me.AData1.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.AData1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AData1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.AData1.Location = New System.Drawing.Point(134, 202)
        Me.AData1.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.AData1.Name = "AData1"
        Me.AData1.Size = New System.Drawing.Size(113, 28)
        Me.AData1.TabIndex = 29
        Me.AData1.Text = "Adodc1"
        Me.AData1.Visible = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._optShow_0)
        Me.Frame4.Controls.Add(Me._optShow_1)
        Me.Frame4.Controls.Add(Me._optShow_2)
        Me.Frame4.Controls.Add(Me._optShow_3)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(1, 104)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(541, 42)
        Me.Frame4.TabIndex = 29
        Me.Frame4.TabStop = False
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
        Me._optShow_0.Size = New System.Drawing.Size(95, 18)
        Me._optShow_0.TabIndex = 33
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
        Me._optShow_1.Location = New System.Drawing.Point(144, 14)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(95, 18)
        Me._optShow_1.TabIndex = 32
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "Landed Cost"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        '_optShow_2
        '
        Me._optShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_2, CType(2, Short))
        Me._optShow_2.Location = New System.Drawing.Point(280, 14)
        Me._optShow_2.Name = "_optShow_2"
        Me._optShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_2.Size = New System.Drawing.Size(95, 18)
        Me._optShow_2.TabIndex = 31
        Me._optShow_2.TabStop = True
        Me._optShow_2.Text = "Sale Cost"
        Me._optShow_2.UseVisualStyleBackColor = False
        '
        '_optShow_3
        '
        Me._optShow_3.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_3, CType(3, Short))
        Me._optShow_3.Location = New System.Drawing.Point(416, 14)
        Me._optShow_3.Name = "_optShow_3"
        Me._optShow_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_3.Size = New System.Drawing.Size(95, 18)
        Me._optShow_3.TabIndex = 30
        Me._optShow_3.TabStop = True
        Me._optShow_3.Text = "Current Cost"
        Me._optShow_3.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cboDivision)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(546, 104)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(350, 42)
        Me.Frame8.TabIndex = 38
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(73, 14)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(267, 21)
        Me.cboDivision.TabIndex = 39
        '
        'optShow
        '
        '
        'frmParamStockValuationFIFO
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
        Me.Controls.Add(Me.AData1)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame8)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 16)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamStockValuationFIFO"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Bank Stock Statement"
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
        Me.Frame4.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
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