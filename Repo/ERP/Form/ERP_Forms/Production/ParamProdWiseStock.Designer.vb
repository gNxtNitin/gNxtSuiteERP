Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamProdWiseStock
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
    Public WithEvents chkCategory As System.Windows.Forms.CheckBox
    Public WithEvents txtCategoryDesc As System.Windows.Forms.TextBox
    Public WithEvents cmdCategory As System.Windows.Forms.Button
    Public WithEvents chkAllParty As System.Windows.Forms.CheckBox
    Public WithEvents txtPartyName As System.Windows.Forms.TextBox
    Public WithEvents cmdPartyName As System.Windows.Forms.Button
    Public WithEvents cmdSearchItem As System.Windows.Forms.Button
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchFG As System.Windows.Forms.Button
    Public WithEvents txtFGName As System.Windows.Forms.TextBox
    Public WithEvents chkFG As System.Windows.Forms.CheckBox
    Public WithEvents chkItem As System.Windows.Forms.CheckBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents UpDYear As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblYear As System.Windows.Forms.Label
    Public WithEvents lblRunDate As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public CommonDialog1Open As System.Windows.Forms.OpenFileDialog
    Public CommonDialog1Save As System.Windows.Forms.SaveFileDialog
    Public CommonDialog1Font As System.Windows.Forms.FontDialog
    Public CommonDialog1Color As System.Windows.Forms.ColorDialog
    Public CommonDialog1Print As System.Windows.Forms.PrintDialog
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblBookSubType As System.Windows.Forms.Label
    Public WithEvents lblSubCatCode As System.Windows.Forms.Label
    Public WithEvents lblCatCode As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamProdWiseStock))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdCategory = New System.Windows.Forms.Button()
        Me.cmdPartyName = New System.Windows.Forms.Button()
        Me.cmdSearchItem = New System.Windows.Forms.Button()
        Me.cmdSearchFG = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkCategory = New System.Windows.Forms.CheckBox()
        Me.txtCategoryDesc = New System.Windows.Forms.TextBox()
        Me.chkAllParty = New System.Windows.Forms.CheckBox()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.txtFGName = New System.Windows.Forms.TextBox()
        Me.chkFG = New System.Windows.Forms.CheckBox()
        Me.chkItem = New System.Windows.Forms.CheckBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.UpDYear = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.lblRunDate = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblBookSubType = New System.Windows.Forms.Label()
        Me.lblSubCatCode = New System.Windows.Forms.Label()
        Me.lblCatCode = New System.Windows.Forms.Label()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdCategory
        '
        Me.cmdCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCategory.Image = CType(resources.GetObject("cmdCategory.Image"), System.Drawing.Image)
        Me.cmdCategory.Location = New System.Drawing.Point(371, 76)
        Me.cmdCategory.Name = "cmdCategory"
        Me.cmdCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCategory.Size = New System.Drawing.Size(23, 19)
        Me.cmdCategory.TabIndex = 26
        Me.cmdCategory.TabStop = False
        Me.cmdCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCategory, "Search")
        Me.cmdCategory.UseVisualStyleBackColor = False
        '
        'cmdPartyName
        '
        Me.cmdPartyName.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPartyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPartyName.Enabled = False
        Me.cmdPartyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPartyName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPartyName.Image = CType(resources.GetObject("cmdPartyName.Image"), System.Drawing.Image)
        Me.cmdPartyName.Location = New System.Drawing.Point(372, 10)
        Me.cmdPartyName.Name = "cmdPartyName"
        Me.cmdPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPartyName.Size = New System.Drawing.Size(23, 19)
        Me.cmdPartyName.TabIndex = 19
        Me.cmdPartyName.TabStop = False
        Me.cmdPartyName.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPartyName, "Search")
        Me.cmdPartyName.UseVisualStyleBackColor = False
        '
        'cmdSearchItem
        '
        Me.cmdSearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchItem.Image = CType(resources.GetObject("cmdSearchItem.Image"), System.Drawing.Image)
        Me.cmdSearchItem.Location = New System.Drawing.Point(372, 54)
        Me.cmdSearchItem.Name = "cmdSearchItem"
        Me.cmdSearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchItem.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchItem.TabIndex = 12
        Me.cmdSearchItem.TabStop = False
        Me.cmdSearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchItem, "Search")
        Me.cmdSearchItem.UseVisualStyleBackColor = False
        '
        'cmdSearchFG
        '
        Me.cmdSearchFG.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchFG.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchFG.Image = CType(resources.GetObject("cmdSearchFG.Image"), System.Drawing.Image)
        Me.cmdSearchFG.Location = New System.Drawing.Point(372, 32)
        Me.cmdSearchFG.Name = "cmdSearchFG"
        Me.cmdSearchFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchFG.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchFG.TabIndex = 10
        Me.cmdSearchFG.TabStop = False
        Me.cmdSearchFG.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchFG, "Search")
        Me.cmdSearchFG.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(698, 10)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(67, 35)
        Me.cmdClose.TabIndex = 2
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(632, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 35)
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
        Me.cmdPrint.Location = New System.Drawing.Point(566, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 35)
        Me.cmdPrint.TabIndex = 3
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdShow
        '
        Me.cmdShow.BackColor = System.Drawing.SystemColors.Control
        Me.cmdShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(500, 10)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(67, 35)
        Me.cmdShow.TabIndex = 1
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.chkCategory)
        Me.Frame4.Controls.Add(Me.txtCategoryDesc)
        Me.Frame4.Controls.Add(Me.cmdCategory)
        Me.Frame4.Controls.Add(Me.chkAllParty)
        Me.Frame4.Controls.Add(Me.txtPartyName)
        Me.Frame4.Controls.Add(Me.cmdPartyName)
        Me.Frame4.Controls.Add(Me.cmdSearchItem)
        Me.Frame4.Controls.Add(Me.txtItemName)
        Me.Frame4.Controls.Add(Me.cmdSearchFG)
        Me.Frame4.Controls.Add(Me.txtFGName)
        Me.Frame4.Controls.Add(Me.chkFG)
        Me.Frame4.Controls.Add(Me.chkItem)
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me.UpDYear)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Controls.Add(Me.lblYear)
        Me.Frame4.Controls.Add(Me.lblRunDate)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Controls.Add(Me.Label3)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -6)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(769, 433)
        Me.Frame4.TabIndex = 5
        Me.Frame4.TabStop = False
        '
        'chkCategory
        '
        Me.chkCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkCategory.Checked = True
        Me.chkCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCategory.Location = New System.Drawing.Point(395, 78)
        Me.chkCategory.Name = "chkCategory"
        Me.chkCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCategory.Size = New System.Drawing.Size(45, 16)
        Me.chkCategory.TabIndex = 28
        Me.chkCategory.Text = "All"
        Me.chkCategory.UseVisualStyleBackColor = False
        '
        'txtCategoryDesc
        '
        Me.txtCategoryDesc.AcceptsReturn = True
        Me.txtCategoryDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategoryDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategoryDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCategoryDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategoryDesc.ForeColor = System.Drawing.Color.Blue
        Me.txtCategoryDesc.Location = New System.Drawing.Point(95, 76)
        Me.txtCategoryDesc.MaxLength = 0
        Me.txtCategoryDesc.Name = "txtCategoryDesc"
        Me.txtCategoryDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCategoryDesc.Size = New System.Drawing.Size(275, 19)
        Me.txtCategoryDesc.TabIndex = 27
        '
        'chkAllParty
        '
        Me.chkAllParty.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllParty.Checked = True
        Me.chkAllParty.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllParty.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllParty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllParty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllParty.Location = New System.Drawing.Point(396, 14)
        Me.chkAllParty.Name = "chkAllParty"
        Me.chkAllParty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllParty.Size = New System.Drawing.Size(45, 16)
        Me.chkAllParty.TabIndex = 21
        Me.chkAllParty.Text = "All"
        Me.chkAllParty.UseVisualStyleBackColor = False
        '
        'txtPartyName
        '
        Me.txtPartyName.AcceptsReturn = True
        Me.txtPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyName.Enabled = False
        Me.txtPartyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyName.ForeColor = System.Drawing.Color.Blue
        Me.txtPartyName.Location = New System.Drawing.Point(96, 10)
        Me.txtPartyName.MaxLength = 0
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyName.Size = New System.Drawing.Size(275, 19)
        Me.txtPartyName.TabIndex = 20
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.ForeColor = System.Drawing.Color.Blue
        Me.txtItemName.Location = New System.Drawing.Point(96, 54)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(275, 19)
        Me.txtItemName.TabIndex = 11
        '
        'txtFGName
        '
        Me.txtFGName.AcceptsReturn = True
        Me.txtFGName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFGName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFGName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFGName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFGName.ForeColor = System.Drawing.Color.Blue
        Me.txtFGName.Location = New System.Drawing.Point(96, 32)
        Me.txtFGName.MaxLength = 0
        Me.txtFGName.Name = "txtFGName"
        Me.txtFGName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFGName.Size = New System.Drawing.Size(275, 19)
        Me.txtFGName.TabIndex = 9
        '
        'chkFG
        '
        Me.chkFG.BackColor = System.Drawing.SystemColors.Control
        Me.chkFG.Checked = True
        Me.chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFG.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFG.Location = New System.Drawing.Point(396, 34)
        Me.chkFG.Name = "chkFG"
        Me.chkFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFG.Size = New System.Drawing.Size(45, 16)
        Me.chkFG.TabIndex = 8
        Me.chkFG.Text = "All"
        Me.chkFG.UseVisualStyleBackColor = False
        '
        'chkItem
        '
        Me.chkItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkItem.Checked = True
        Me.chkItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkItem.Location = New System.Drawing.Point(396, 56)
        Me.chkItem.Name = "chkItem"
        Me.chkItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkItem.Size = New System.Drawing.Size(45, 16)
        Me.chkItem.TabIndex = 7
        Me.chkItem.Text = "All"
        Me.chkItem.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 98)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(766, 329)
        Me.SprdMain.TabIndex = 6
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(736, 38)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 29
        '
        'UpDYear
        '
        Me.UpDYear.BackColor = System.Drawing.Color.Red
        Me.UpDYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UpDYear.Location = New System.Drawing.Point(748, 8)
        Me.UpDYear.Name = "UpDYear"
        Me.UpDYear.Size = New System.Drawing.Size(16, 30)
        Me.UpDYear.TabIndex = 24
        Me.UpDYear.Text = "UpDYear"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(31, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(57, 14)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Category :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.BackColor = System.Drawing.SystemColors.Control
        Me.lblYear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblYear.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblYear.Location = New System.Drawing.Point(739, 10)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblYear.Size = New System.Drawing.Size(2, 18)
        Me.lblYear.TabIndex = 23
        Me.lblYear.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblRunDate
        '
        Me.lblRunDate.AutoSize = True
        Me.lblRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRunDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRunDate.Location = New System.Drawing.Point(640, 16)
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRunDate.Size = New System.Drawing.Size(48, 14)
        Me.lblRunDate.TabIndex = 25
        Me.lblRunDate.Text = "RunDate"
        Me.lblRunDate.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(17, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(68, 14)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Party Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(1, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Finished Good :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(29, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(56, 14)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "BOP Item :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdShow)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblBookSubType)
        Me.Frame3.Controls.Add(Me.lblSubCatCode)
        Me.Frame3.Controls.Add(Me.lblCatCode)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 422)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(769, 49)
        Me.Frame3.TabIndex = 0
        Me.Frame3.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(88, 10)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(107, 17)
        Me.lblBookType.TabIndex = 18
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBookType.Visible = False
        '
        'lblBookSubType
        '
        Me.lblBookSubType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookSubType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookSubType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookSubType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookSubType.Location = New System.Drawing.Point(88, 30)
        Me.lblBookSubType.Name = "lblBookSubType"
        Me.lblBookSubType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookSubType.Size = New System.Drawing.Size(107, 17)
        Me.lblBookSubType.TabIndex = 17
        Me.lblBookSubType.Text = "lblBookSubType"
        Me.lblBookSubType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBookSubType.Visible = False
        '
        'lblSubCatCode
        '
        Me.lblSubCatCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblSubCatCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblSubCatCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSubCatCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSubCatCode.Location = New System.Drawing.Point(16, 28)
        Me.lblSubCatCode.Name = "lblSubCatCode"
        Me.lblSubCatCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSubCatCode.Size = New System.Drawing.Size(87, 13)
        Me.lblSubCatCode.TabIndex = 16
        Me.lblSubCatCode.Text = "lblSubCatCode"
        Me.lblSubCatCode.Visible = False
        '
        'lblCatCode
        '
        Me.lblCatCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblCatCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCatCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCatCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCatCode.Location = New System.Drawing.Point(18, 12)
        Me.lblCatCode.Name = "lblCatCode"
        Me.lblCatCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCatCode.Size = New System.Drawing.Size(81, 13)
        Me.lblCatCode.TabIndex = 15
        Me.lblCatCode.Text = "lblCatCode"
        Me.lblCatCode.Visible = False
        '
        'frmParamProdWiseStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(752, 471)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 16)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamProdWiseStock"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Monthly Schedule"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class