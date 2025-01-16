Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmItemPurchasesYearly
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
        'Me.MdiParent = AccountGST.Master

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
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents chkDebit As System.Windows.Forms.CheckBox
    Public WithEvents chkSupp As System.Windows.Forms.CheckBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _optRate_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optRate_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame11 As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents FraDate As System.Windows.Forms.GroupBox
    Public WithEvents lstInvoiceType As System.Windows.Forms.CheckedListBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchItem As System.Windows.Forms.Button
    Public WithEvents chkAllItem As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents txtCategory As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchCategory As System.Windows.Forms.Button
    Public WithEvents chkAllCategory As System.Windows.Forms.CheckBox
    Public WithEvents txtSubCategory As System.Windows.Forms.TextBox
    Public WithEvents cmdSubCatsearch As System.Windows.Forms.Button
    Public WithEvents chkAllSubCat As System.Windows.Forms.CheckBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _optOrderBy_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _optDate_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optDate_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents optDate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optOrderBy As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optRate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmItemPurchasesYearly))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.cmdsearchItem = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.txtCategory = New System.Windows.Forms.TextBox()
        Me.cmdsearchCategory = New System.Windows.Forms.Button()
        Me.txtSubCategory = New System.Windows.Forms.TextBox()
        Me.cmdSubCatsearch = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdExport = New System.Windows.Forms.Button()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.chkDebit = New System.Windows.Forms.CheckBox()
        Me.chkSupp = New System.Windows.Forms.CheckBox()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me._optRate_0 = New System.Windows.Forms.RadioButton()
        Me._optRate_1 = New System.Windows.Forms.RadioButton()
        Me.FraDate = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lstInvoiceType = New System.Windows.Forms.CheckedListBox()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkAllItem = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkAllCategory = New System.Windows.Forms.CheckBox()
        Me.chkAllSubCat = New System.Windows.Forms.CheckBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optOrderBy_2 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._optDate_1 = New System.Windows.Forms.RadioButton()
        Me._optDate_0 = New System.Windows.Forms.RadioButton()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optDate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optRate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.UltraGridColumnChooser1 = New Infragistics.Win.UltraWinGrid.UltraGridColumnChooser()
        Me.UltraDataSource1 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.UltraGridExcelExporter1 = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.Frame9.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame11.SuspendLayout()
        Me.FraDate.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optRate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraGridColumnChooser1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtItemName
        '
        Me.txtItemName.AcceptsReturn = True
        Me.txtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemName.Location = New System.Drawing.Point(100, 39)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(158, 20)
        Me.txtItemName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtItemName, "Press F1 For Help")
        '
        'cmdsearchItem
        '
        Me.cmdsearchItem.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchItem.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchItem.Image = CType(resources.GetObject("cmdsearchItem.Image"), System.Drawing.Image)
        Me.cmdsearchItem.Location = New System.Drawing.Point(260, 38)
        Me.cmdsearchItem.Name = "cmdsearchItem"
        Me.cmdsearchItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchItem.Size = New System.Drawing.Size(29, 22)
        Me.cmdsearchItem.TabIndex = 4
        Me.cmdsearchItem.TabStop = False
        Me.cmdsearchItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchItem, "Search")
        Me.cmdsearchItem.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(260, 12)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 20)
        Me.cmdsearch.TabIndex = 1
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(100, 12)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(158, 20)
        Me.TxtAccount.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'txtCategory
        '
        Me.txtCategory.AcceptsReturn = True
        Me.txtCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCategory.Location = New System.Drawing.Point(100, 66)
        Me.txtCategory.MaxLength = 0
        Me.txtCategory.Name = "txtCategory"
        Me.txtCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCategory.Size = New System.Drawing.Size(158, 20)
        Me.txtCategory.TabIndex = 38
        Me.ToolTip1.SetToolTip(Me.txtCategory, "Press F1 For Help")
        '
        'cmdsearchCategory
        '
        Me.cmdsearchCategory.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchCategory.Image = CType(resources.GetObject("cmdsearchCategory.Image"), System.Drawing.Image)
        Me.cmdsearchCategory.Location = New System.Drawing.Point(260, 64)
        Me.cmdsearchCategory.Name = "cmdsearchCategory"
        Me.cmdsearchCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchCategory.Size = New System.Drawing.Size(29, 20)
        Me.cmdsearchCategory.TabIndex = 37
        Me.cmdsearchCategory.TabStop = False
        Me.cmdsearchCategory.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchCategory, "Search")
        Me.cmdsearchCategory.UseVisualStyleBackColor = False
        '
        'txtSubCategory
        '
        Me.txtSubCategory.AcceptsReturn = True
        Me.txtSubCategory.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSubCategory.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubCategory.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubCategory.Location = New System.Drawing.Point(100, 93)
        Me.txtSubCategory.MaxLength = 0
        Me.txtSubCategory.Name = "txtSubCategory"
        Me.txtSubCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubCategory.Size = New System.Drawing.Size(158, 20)
        Me.txtSubCategory.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.txtSubCategory, "Press F1 For Help")
        '
        'cmdSubCatsearch
        '
        Me.cmdSubCatsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSubCatsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSubCatsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSubCatsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSubCatsearch.Image = CType(resources.GetObject("cmdSubCatsearch.Image"), System.Drawing.Image)
        Me.cmdSubCatsearch.Location = New System.Drawing.Point(260, 92)
        Me.cmdSubCatsearch.Name = "cmdSubCatsearch"
        Me.cmdSubCatsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSubCatsearch.Size = New System.Drawing.Size(29, 20)
        Me.cmdSubCatsearch.TabIndex = 47
        Me.cmdSubCatsearch.TabStop = False
        Me.cmdSubCatsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSubCatsearch, "Search")
        Me.cmdSubCatsearch.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CmdPreview.Location = New System.Drawing.Point(168, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(78, 37)
        Me.CmdPreview.TabIndex = 9
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
        Me.cmdPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdPrint.Location = New System.Drawing.Point(86, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(78, 37)
        Me.cmdPrint.TabIndex = 8
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
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(332, 9)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(78, 37)
        Me.cmdClose.TabIndex = 10
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
        Me.cmdShow.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdShow.Location = New System.Drawing.Point(4, 9)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(78, 37)
        Me.cmdShow.TabIndex = 7
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'cmdExport
        '
        Me.cmdExport.BackColor = System.Drawing.SystemColors.Control
        Me.cmdExport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdExport.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdExport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdExport.Location = New System.Drawing.Point(250, 9)
        Me.cmdExport.Name = "cmdExport"
        Me.cmdExport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExport.Size = New System.Drawing.Size(78, 37)
        Me.cmdExport.TabIndex = 14
        Me.cmdExport.Text = "&Export"
        Me.cmdExport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExport, "Excel Export")
        Me.cmdExport.UseVisualStyleBackColor = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.cboDivision)
        Me.Frame9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(467, 559)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(182, 49)
        Me.Frame9.TabIndex = 55
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(4, 18)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(172, 22)
        Me.cboDivision.TabIndex = 56
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.chkDebit)
        Me.Frame6.Controls.Add(Me.chkSupp)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(164, 44)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(159, 75)
        Me.Frame6.TabIndex = 52
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Including"
        '
        'chkDebit
        '
        Me.chkDebit.AutoSize = True
        Me.chkDebit.BackColor = System.Drawing.SystemColors.Control
        Me.chkDebit.Checked = True
        Me.chkDebit.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDebit.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDebit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDebit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDebit.Location = New System.Drawing.Point(4, 17)
        Me.chkDebit.Name = "chkDebit"
        Me.chkDebit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDebit.Size = New System.Drawing.Size(125, 18)
        Me.chkDebit.TabIndex = 54
        Me.chkDebit.Text = "Debit / Credit Note"
        Me.chkDebit.UseVisualStyleBackColor = False
        '
        'chkSupp
        '
        Me.chkSupp.AutoSize = True
        Me.chkSupp.BackColor = System.Drawing.SystemColors.Control
        Me.chkSupp.Checked = True
        Me.chkSupp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSupp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSupp.Location = New System.Drawing.Point(4, 41)
        Me.chkSupp.Name = "chkSupp"
        Me.chkSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSupp.Size = New System.Drawing.Size(110, 18)
        Me.chkSupp.TabIndex = 53
        Me.chkSupp.Text = "Supplementary"
        Me.chkSupp.UseVisualStyleBackColor = False
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me._optRate_0)
        Me.Frame11.Controls.Add(Me._optRate_1)
        Me.Frame11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame11.Location = New System.Drawing.Point(325, 48)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(87, 67)
        Me.Frame11.TabIndex = 49
        Me.Frame11.TabStop = False
        Me.Frame11.Text = "Rate"
        '
        '_optRate_0
        '
        Me._optRate_0.AutoSize = True
        Me._optRate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optRate_0.Checked = True
        Me._optRate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRate.SetIndex(Me._optRate_0, CType(0, Short))
        Me._optRate_0.Location = New System.Drawing.Point(4, 12)
        Me._optRate_0.Name = "_optRate_0"
        Me._optRate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRate_0.Size = New System.Drawing.Size(77, 18)
        Me._optRate_0.TabIndex = 51
        Me._optRate_0.TabStop = True
        Me._optRate_0.Text = "Purchase"
        Me._optRate_0.UseVisualStyleBackColor = False
        '
        '_optRate_1
        '
        Me._optRate_1.AutoSize = True
        Me._optRate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optRate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRate.SetIndex(Me._optRate_1, CType(1, Short))
        Me._optRate_1.Location = New System.Drawing.Point(4, 35)
        Me._optRate_1.Name = "_optRate_1"
        Me._optRate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRate_1.Size = New System.Drawing.Size(66, 18)
        Me._optRate_1.TabIndex = 50
        Me._optRate_1.TabStop = True
        Me._optRate_1.Text = "Landed"
        Me._optRate_1.UseVisualStyleBackColor = False
        '
        'FraDate
        '
        Me.FraDate.BackColor = System.Drawing.SystemColors.Control
        Me.FraDate.Controls.Add(Me.txtDateFrom)
        Me.FraDate.Controls.Add(Me.txtDateTo)
        Me.FraDate.Controls.Add(Me._Lbl_0)
        Me.FraDate.Controls.Add(Me._Lbl_1)
        Me.FraDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraDate.Location = New System.Drawing.Point(164, 0)
        Me.FraDate.Name = "FraDate"
        Me.FraDate.Padding = New System.Windows.Forms.Padding(0)
        Me.FraDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraDate.Size = New System.Drawing.Size(248, 45)
        Me.FraDate.TabIndex = 30
        Me.FraDate.TabStop = False
        Me.FraDate.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(46, 16)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 20)
        Me.txtDateFrom.TabIndex = 31
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(163, 16)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(75, 20)
        Me.txtDateTo.TabIndex = 32
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(4, 19)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(42, 14)
        Me._Lbl_0.TabIndex = 34
        Me._Lbl_0.Text = "From :"
        Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(133, 19)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(26, 14)
        Me._Lbl_1.TabIndex = 33
        Me._Lbl_1.Text = "To :"
        Me._Lbl_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.lstInvoiceType)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(163, 120)
        Me.Frame3.TabIndex = 26
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Invoice Type"
        '
        'lstInvoiceType
        '
        Me.lstInvoiceType.BackColor = System.Drawing.SystemColors.Window
        Me.lstInvoiceType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstInvoiceType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstInvoiceType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstInvoiceType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstInvoiceType.Location = New System.Drawing.Point(0, 13)
        Me.lstInvoiceType.Name = "lstInvoiceType"
        Me.lstInvoiceType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstInvoiceType.Size = New System.Drawing.Size(163, 107)
        Me.lstInvoiceType.TabIndex = 27
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.Label6)
        Me.FraAccount.Controls.Add(Me.Label3)
        Me.FraAccount.Controls.Add(Me.txtItemName)
        Me.FraAccount.Controls.Add(Me.cmdsearchItem)
        Me.FraAccount.Controls.Add(Me.chkAllItem)
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Controls.Add(Me.txtCategory)
        Me.FraAccount.Controls.Add(Me.cmdsearchCategory)
        Me.FraAccount.Controls.Add(Me.chkAllCategory)
        Me.FraAccount.Controls.Add(Me.txtSubCategory)
        Me.FraAccount.Controls.Add(Me.cmdSubCatsearch)
        Me.FraAccount.Controls.Add(Me.chkAllSubCat)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(414, -1)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(344, 121)
        Me.FraAccount.TabIndex = 11
        Me.FraAccount.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(13, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(84, 14)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "SubCategory :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(34, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(63, 14)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Category :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkAllItem
        '
        Me.chkAllItem.AutoSize = True
        Me.chkAllItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllItem.Checked = True
        Me.chkAllItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllItem.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllItem.Location = New System.Drawing.Point(290, 40)
        Me.chkAllItem.Name = "chkAllItem"
        Me.chkAllItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllItem.Size = New System.Drawing.Size(48, 18)
        Me.chkAllItem.TabIndex = 5
        Me.chkAllItem.Text = "ALL"
        Me.chkAllItem.UseVisualStyleBackColor = False
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(290, 14)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(48, 18)
        Me.chkAll.TabIndex = 2
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(25, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(72, 14)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Item Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(5, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(92, 14)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Account Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkAllCategory
        '
        Me.chkAllCategory.AutoSize = True
        Me.chkAllCategory.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCategory.Checked = True
        Me.chkAllCategory.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCategory.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCategory.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCategory.Location = New System.Drawing.Point(290, 66)
        Me.chkAllCategory.Name = "chkAllCategory"
        Me.chkAllCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCategory.Size = New System.Drawing.Size(48, 18)
        Me.chkAllCategory.TabIndex = 36
        Me.chkAllCategory.Text = "ALL"
        Me.chkAllCategory.UseVisualStyleBackColor = False
        '
        'chkAllSubCat
        '
        Me.chkAllSubCat.AutoSize = True
        Me.chkAllSubCat.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllSubCat.Checked = True
        Me.chkAllSubCat.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllSubCat.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllSubCat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllSubCat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllSubCat.Location = New System.Drawing.Point(290, 94)
        Me.chkAllSubCat.Name = "chkAllSubCat"
        Me.chkAllSubCat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllSubCat.Size = New System.Drawing.Size(48, 18)
        Me.chkAllSubCat.TabIndex = 46
        Me.chkAllSubCat.Text = "ALL"
        Me.chkAllSubCat.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.UltraGrid1)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 117)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1098, 439)
        Me.Frame4.TabIndex = 12
        Me.Frame4.TabStop = False
        '
        'UltraGrid1
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.Color.White
        Appearance1.ForeColor = System.Drawing.Color.Black
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance1
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.White
        Appearance2.BackColor2 = System.Drawing.Color.White
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.UltraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UltraGrid1.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UltraGrid1.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance8
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.UltraGrid1.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UltraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance11
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.UltraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.UltraGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGrid1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 13)
        Me.UltraGrid1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(1098, 426)
        Me.UltraGrid1.TabIndex = 8
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 7
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optOrderBy_2)
        Me.Frame1.Controls.Add(Me._optOrderBy_1)
        Me.Frame1.Controls.Add(Me._optOrderBy_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 560)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(304, 49)
        Me.Frame1.TabIndex = 16
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Group By"
        '
        '_optOrderBy_2
        '
        Me._optOrderBy_2.AutoSize = True
        Me._optOrderBy_2.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_2, CType(2, Short))
        Me._optOrderBy_2.Location = New System.Drawing.Point(88, 18)
        Me._optOrderBy_2.Name = "_optOrderBy_2"
        Me._optOrderBy_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_2.Size = New System.Drawing.Size(124, 18)
        Me._optOrderBy_2.TabIndex = 44
        Me._optOrderBy_2.TabStop = True
        Me._optOrderBy_2.Text = "Item + Party Name"
        Me._optOrderBy_2.UseVisualStyleBackColor = False
        '
        '_optOrderBy_1
        '
        Me._optOrderBy_1.AutoSize = True
        Me._optOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_1, CType(1, Short))
        Me._optOrderBy_1.Location = New System.Drawing.Point(213, 18)
        Me._optOrderBy_1.Name = "_optOrderBy_1"
        Me._optOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_1.Size = New System.Drawing.Size(87, 18)
        Me._optOrderBy_1.TabIndex = 18
        Me._optOrderBy_1.TabStop = True
        Me._optOrderBy_1.Text = "Party Name"
        Me._optOrderBy_1.UseVisualStyleBackColor = False
        '
        '_optOrderBy_0
        '
        Me._optOrderBy_0.AutoSize = True
        Me._optOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_0.Checked = True
        Me._optOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_0, CType(0, Short))
        Me._optOrderBy_0.Location = New System.Drawing.Point(4, 18)
        Me._optOrderBy_0.Name = "_optOrderBy_0"
        Me._optOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_0.Size = New System.Drawing.Size(84, 18)
        Me._optOrderBy_0.TabIndex = 17
        Me._optOrderBy_0.TabStop = True
        Me._optOrderBy_0.Text = "Item Name"
        Me._optOrderBy_0.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me._optDate_1)
        Me.Frame8.Controls.Add(Me._optDate_0)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(305, 559)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(106, 49)
        Me.Frame8.TabIndex = 41
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Base On"
        '
        '_optDate_1
        '
        Me._optDate_1.AutoSize = True
        Me._optDate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_1, CType(1, Short))
        Me._optDate_1.Location = New System.Drawing.Point(16, 29)
        Me._optDate_1.Name = "_optDate_1"
        Me._optDate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_1.Size = New System.Drawing.Size(49, 18)
        Me._optDate_1.TabIndex = 43
        Me._optDate_1.TabStop = True
        Me._optDate_1.Text = "MRR"
        Me._optDate_1.UseVisualStyleBackColor = False
        '
        '_optDate_0
        '
        Me._optDate_0.AutoSize = True
        Me._optDate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_0.Checked = True
        Me._optDate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_0, CType(0, Short))
        Me._optDate_0.Location = New System.Drawing.Point(16, 13)
        Me._optDate_0.Name = "_optDate_0"
        Me._optDate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_0.Size = New System.Drawing.Size(57, 18)
        Me._optDate_0.TabIndex = 42
        Me._optDate_0.TabStop = True
        Me._optDate_0.Text = "VDate"
        Me._optDate_0.UseVisualStyleBackColor = False
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdExport)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(652, 559)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(412, 49)
        Me.FraMovement.TabIndex = 13
        Me.FraMovement.TabStop = False
        '
        'UltraGridColumnChooser1
        '
        Me.UltraGridColumnChooser1.ColumnDisplayOrder = Infragistics.Win.UltraWinGrid.ColumnDisplayOrder.SameAsGrid
        Me.UltraGridColumnChooser1.DisplayLayout.AutoFitStyle = Infragistics.Win.UltraWinGrid.AutoFitStyle.ResizeAllColumns
        Me.UltraGridColumnChooser1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraGridColumnChooser1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGridColumnChooser1.DisplayLayout.MaxRowScrollRegions = 1
        Me.UltraGridColumnChooser1.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed
        Me.UltraGridColumnChooser1.DisplayLayout.Override.AllowColSizing = Infragistics.Win.UltraWinGrid.AllowColSizing.None
        Me.UltraGridColumnChooser1.DisplayLayout.Override.AllowRowLayoutCellSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.None
        Me.UltraGridColumnChooser1.DisplayLayout.Override.AllowRowLayoutLabelSizing = Infragistics.Win.UltraWinGrid.RowLayoutSizing.None
        Me.UltraGridColumnChooser1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.UltraGridColumnChooser1.DisplayLayout.Override.CellPadding = 2
        Me.UltraGridColumnChooser1.DisplayLayout.Override.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never
        Me.UltraGridColumnChooser1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.[Select]
        Me.UltraGridColumnChooser1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraGridColumnChooser1.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.AutoFixed
        Me.UltraGridColumnChooser1.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.UltraGridColumnChooser1.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.UltraGridColumnChooser1.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.UltraGridColumnChooser1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.UltraGridColumnChooser1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGridColumnChooser1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGridColumnChooser1.Location = New System.Drawing.Point(762, 3)
        Me.UltraGridColumnChooser1.Name = "UltraGridColumnChooser1"
        Me.UltraGridColumnChooser1.Size = New System.Drawing.Size(182, 116)
        Me.UltraGridColumnChooser1.StyleLibraryName = ""
        Me.UltraGridColumnChooser1.StyleSetName = ""
        Me.UltraGridColumnChooser1.TabIndex = 131
        Me.UltraGridColumnChooser1.Text = "UltraGridColumnChooser1"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(947, -4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(151, 123)
        Me.GroupBox1.TabIndex = 132
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Company Name"
        '
        'lstCompanyName
        '
        Me.lstCompanyName.BackColor = System.Drawing.SystemColors.Window
        Me.lstCompanyName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstCompanyName.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstCompanyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstCompanyName.IntegralHeight = False
        Me.lstCompanyName.Location = New System.Drawing.Point(0, 13)
        Me.lstCompanyName.Name = "lstCompanyName"
        Me.lstCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstCompanyName.Size = New System.Drawing.Size(151, 110)
        Me.lstCompanyName.TabIndex = 2
        '
        'frmItemPurchasesYearly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1100, 612)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.UltraGridColumnChooser1)
        Me.Controls.Add(Me.Frame9)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame11)
        Me.Controls.Add(Me.FraDate)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmItemPurchasesYearly"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Item Purchases (Tabular Format Month Wise)"
        Me.Frame9.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame11.ResumeLayout(False)
        Me.Frame11.PerformLayout()
        Me.FraDate.ResumeLayout(False)
        Me.FraDate.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optRate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraGridColumnChooser1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdMain.DataSource = CType(AData1, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        'SprdMain.DataSource = Nothing
    End Sub
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents UltraGridColumnChooser1 As Infragistics.Win.UltraWinGrid.UltraGridColumnChooser
    Public WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraDataSource1 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Public WithEvents cmdExport As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents UltraGridExcelExporter1 As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
#End Region
End Class