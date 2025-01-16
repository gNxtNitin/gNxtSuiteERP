Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamStock
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
    Public WithEvents chkQCStockType As System.Windows.Forms.CheckBox
    Public WithEvents cboIsShowItem As System.Windows.Forms.ComboBox
    Public WithEvents cboExport As System.Windows.Forms.GroupBox
    Public WithEvents CboSType As System.Windows.Forms.ComboBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents _optType_4 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_0 As System.Windows.Forms.RadioButton
    Public WithEvents fraDetSum As System.Windows.Forms.GroupBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents chkRate As System.Windows.Forms.CheckBox
    Public WithEvents _optVal_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optVal_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optVal_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optVal_1 As System.Windows.Forms.RadioButton
    Public WithEvents FraVal As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdOption As AxFPSpreadADO.AxfpSpread
    Public WithEvents chkDespatchShow As System.Windows.Forms.CheckBox
    Public WithEvents chkViewAll As System.Windows.Forms.CheckBox
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents cboRef As System.Windows.Forms.CheckedListBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkOption As System.Windows.Forms.CheckBox
    Public WithEvents txtCondQty As System.Windows.Forms.TextBox
    Public WithEvents cboCond As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents FraOption As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents chkIncludOp As System.Windows.Forms.CheckBox
    Public WithEvents lblStockFlag As System.Windows.Forms.Label
    Public WithEvents lblLabelType As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents CboItemType As System.Windows.Forms.ComboBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents txtDays As System.Windows.Forms.TextBox
    Public WithEvents lblDays As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents FraAge As System.Windows.Forms.GroupBox
    Public WithEvents optType As VB6.RadioButtonArray
    Public WithEvents optVal As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamStock))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.chkQCStockType = New System.Windows.Forms.CheckBox()
        Me.cboExport = New System.Windows.Forms.GroupBox()
        Me.cboIsShowItem = New System.Windows.Forms.ComboBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.CboSType = New System.Windows.Forms.ComboBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.fraDetSum = New System.Windows.Forms.GroupBox()
        Me._optType_4 = New System.Windows.Forms.RadioButton()
        Me._optType_3 = New System.Windows.Forms.RadioButton()
        Me._optType_2 = New System.Windows.Forms.RadioButton()
        Me._optType_1 = New System.Windows.Forms.RadioButton()
        Me._optType_0 = New System.Windows.Forms.RadioButton()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.FraVal = New System.Windows.Forms.GroupBox()
        Me.chkRate = New System.Windows.Forms.CheckBox()
        Me._optVal_3 = New System.Windows.Forms.RadioButton()
        Me._optVal_2 = New System.Windows.Forms.RadioButton()
        Me._optVal_0 = New System.Windows.Forms.RadioButton()
        Me._optVal_1 = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdOption = New AxFPSpreadADO.AxfpSpread()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.chkDespatchShow = New System.Windows.Forms.CheckBox()
        Me.chkViewAll = New System.Windows.Forms.CheckBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboRef = New System.Windows.Forms.CheckedListBox()
        Me.chkOption = New System.Windows.Forms.CheckBox()
        Me.FraOption = New System.Windows.Forms.GroupBox()
        Me.txtCondQty = New System.Windows.Forms.TextBox()
        Me.cboCond = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkIncludOp = New System.Windows.Forms.CheckBox()
        Me.lblStockFlag = New System.Windows.Forms.Label()
        Me.lblLabelType = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.CboItemType = New System.Windows.Forms.ComboBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.FraAge = New System.Windows.Forms.GroupBox()
        Me.txtDays = New System.Windows.Forms.TextBox()
        Me.lblDays = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.optType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optVal = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.chkRunningBal = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.cboCapital = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblYear = New System.Windows.Forms.DateTimePicker()
        Me.lblRunDate = New System.Windows.Forms.Label()
        Me.cboExport.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.fraDetSum.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.FraVal.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdOption, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraOption.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.FraAge.SuspendLayout()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optVal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(162, 18)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(82, 37)
        Me.CmdPreview.TabIndex = 12
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
        Me.cmdPrint.Location = New System.Drawing.Point(82, 18)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(82, 37)
        Me.cmdPrint.TabIndex = 11
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
        Me.cmdShow.Location = New System.Drawing.Point(2, 18)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(82, 37)
        Me.cmdShow.TabIndex = 10
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
        Me.cmdExit.Location = New System.Drawing.Point(242, 18)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdExit.Size = New System.Drawing.Size(82, 37)
        Me.cmdExit.TabIndex = 13
        Me.cmdExit.Text = "&Close"
        Me.cmdExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdExit, "Close the Form")
        Me.cmdExit.UseVisualStyleBackColor = False
        '
        'chkQCStockType
        '
        Me.chkQCStockType.BackColor = System.Drawing.SystemColors.Control
        Me.chkQCStockType.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkQCStockType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkQCStockType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkQCStockType.Location = New System.Drawing.Point(615, 77)
        Me.chkQCStockType.Name = "chkQCStockType"
        Me.chkQCStockType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkQCStockType.Size = New System.Drawing.Size(167, 19)
        Me.chkQCStockType.TabIndex = 49
        Me.chkQCStockType.Text = "Include QC Stock Type"
        Me.chkQCStockType.UseVisualStyleBackColor = False
        '
        'cboExport
        '
        Me.cboExport.BackColor = System.Drawing.SystemColors.Control
        Me.cboExport.Controls.Add(Me.cboIsShowItem)
        Me.cboExport.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboExport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cboExport.Location = New System.Drawing.Point(325, 0)
        Me.cboExport.Name = "cboExport"
        Me.cboExport.Padding = New System.Windows.Forms.Padding(0)
        Me.cboExport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboExport.Size = New System.Drawing.Size(111, 48)
        Me.cboExport.TabIndex = 39
        Me.cboExport.TabStop = False
        Me.cboExport.Text = "Show Item"
        '
        'cboIsShowItem
        '
        Me.cboIsShowItem.BackColor = System.Drawing.SystemColors.Window
        Me.cboIsShowItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboIsShowItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIsShowItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboIsShowItem.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboIsShowItem.Location = New System.Drawing.Point(6, 18)
        Me.cboIsShowItem.Name = "cboIsShowItem"
        Me.cboIsShowItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboIsShowItem.Size = New System.Drawing.Size(102, 21)
        Me.cboIsShowItem.TabIndex = 40
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.CboSType)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(228, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(93, 46)
        Me.Frame5.TabIndex = 35
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Stock Type"
        '
        'CboSType
        '
        Me.CboSType.BackColor = System.Drawing.SystemColors.Window
        Me.CboSType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboSType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboSType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboSType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboSType.Location = New System.Drawing.Point(2, 18)
        Me.CboSType.Name = "CboSType"
        Me.CboSType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboSType.Size = New System.Drawing.Size(79, 21)
        Me.CboSType.TabIndex = 36
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 197)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1083, 343)
        Me.SprdMain.TabIndex = 9
        '
        'fraDetSum
        '
        Me.fraDetSum.BackColor = System.Drawing.SystemColors.Control
        Me.fraDetSum.Controls.Add(Me._optType_4)
        Me.fraDetSum.Controls.Add(Me._optType_3)
        Me.fraDetSum.Controls.Add(Me._optType_2)
        Me.fraDetSum.Controls.Add(Me._optType_1)
        Me.fraDetSum.Controls.Add(Me._optType_0)
        Me.fraDetSum.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDetSum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDetSum.Location = New System.Drawing.Point(440, 0)
        Me.fraDetSum.Name = "fraDetSum"
        Me.fraDetSum.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDetSum.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDetSum.Size = New System.Drawing.Size(165, 96)
        Me.fraDetSum.TabIndex = 19
        Me.fraDetSum.TabStop = False
        Me.fraDetSum.Text = "Type"
        '
        '_optType_4
        '
        Me._optType_4.AutoSize = True
        Me._optType_4.BackColor = System.Drawing.SystemColors.Control
        Me._optType_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_4, CType(4, Short))
        Me._optType_4.Location = New System.Drawing.Point(8, 71)
        Me._optType_4.Name = "_optType_4"
        Me._optType_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_4.Size = New System.Drawing.Size(160, 17)
        Me._optType_4.TabIndex = 51
        Me._optType_4.TabStop = True
        Me._optType_4.Text = "Summarised (Month Wise)"
        Me._optType_4.UseVisualStyleBackColor = False
        '
        '_optType_3
        '
        Me._optType_3.AutoSize = True
        Me._optType_3.BackColor = System.Drawing.SystemColors.Control
        Me._optType_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_3, CType(3, Short))
        Me._optType_3.Location = New System.Drawing.Point(8, 52)
        Me._optType_3.Name = "_optType_3"
        Me._optType_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_3.Size = New System.Drawing.Size(151, 17)
        Me._optType_3.TabIndex = 50
        Me._optType_3.TabStop = True
        Me._optType_3.Text = "Summarised (Date Wise)"
        Me._optType_3.UseVisualStyleBackColor = False
        '
        '_optType_2
        '
        Me._optType_2.AutoSize = True
        Me._optType_2.BackColor = System.Drawing.SystemColors.Control
        Me._optType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_2, CType(2, Short))
        Me._optType_2.Location = New System.Drawing.Point(8, 33)
        Me._optType_2.Name = "_optType_2"
        Me._optType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_2.Size = New System.Drawing.Size(131, 17)
        Me._optType_2.TabIndex = 46
        Me._optType_2.TabStop = True
        Me._optType_2.Text = "Summarised (Group)"
        Me._optType_2.UseVisualStyleBackColor = False
        '
        '_optType_1
        '
        Me._optType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_1, CType(1, Short))
        Me._optType_1.Location = New System.Drawing.Point(67, 14)
        Me._optType_1.Name = "_optType_1"
        Me._optType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_1.Size = New System.Drawing.Size(95, 16)
        Me._optType_1.TabIndex = 6
        Me._optType_1.TabStop = True
        Me._optType_1.Text = "Summarised"
        Me._optType_1.UseVisualStyleBackColor = False
        '
        '_optType_0
        '
        Me._optType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optType_0.Checked = True
        Me._optType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_0, CType(0, Short))
        Me._optType_0.Location = New System.Drawing.Point(8, 14)
        Me._optType_0.Name = "_optType_0"
        Me._optType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_0.Size = New System.Drawing.Size(57, 16)
        Me._optType_0.TabIndex = 5
        Me._optType_0.TabStop = True
        Me._optType_0.Text = "Detail"
        Me._optType_0.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.cboDept)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(129, 49)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(194, 48)
        Me.Frame4.TabIndex = 31
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Department"
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(7, 18)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(177, 21)
        Me.cboDept.TabIndex = 4
        '
        'FraVal
        '
        Me.FraVal.BackColor = System.Drawing.SystemColors.Control
        Me.FraVal.Controls.Add(Me.chkRate)
        Me.FraVal.Controls.Add(Me._optVal_3)
        Me.FraVal.Controls.Add(Me._optVal_2)
        Me.FraVal.Controls.Add(Me._optVal_0)
        Me.FraVal.Controls.Add(Me._optVal_1)
        Me.FraVal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraVal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraVal.Location = New System.Drawing.Point(611, 0)
        Me.FraVal.Name = "FraVal"
        Me.FraVal.Padding = New System.Windows.Forms.Padding(0)
        Me.FraVal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraVal.Size = New System.Drawing.Size(220, 54)
        Me.FraVal.TabIndex = 23
        Me.FraVal.TabStop = False
        Me.FraVal.Text = "Valuation at Price)"
        Me.FraVal.Visible = False
        '
        'chkRate
        '
        Me.chkRate.AutoSize = True
        Me.chkRate.BackColor = System.Drawing.SystemColors.Control
        Me.chkRate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRate.Location = New System.Drawing.Point(73, 35)
        Me.chkRate.Name = "chkRate"
        Me.chkRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRate.Size = New System.Drawing.Size(145, 17)
        Me.chkRate.TabIndex = 42
        Me.chkRate.Text = "Zero Qty Rate Required"
        Me.chkRate.UseVisualStyleBackColor = False
        '
        '_optVal_3
        '
        Me._optVal_3.AutoSize = True
        Me._optVal_3.BackColor = System.Drawing.SystemColors.Control
        Me._optVal_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optVal_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optVal_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVal.SetIndex(Me._optVal_3, CType(3, Short))
        Me._optVal_3.Location = New System.Drawing.Point(74, 14)
        Me._optVal_3.Name = "_optVal_3"
        Me._optVal_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optVal_3.Size = New System.Drawing.Size(62, 17)
        Me._optVal_3.TabIndex = 41
        Me._optVal_3.TabStop = True
        Me._optVal_3.Text = "Current"
        Me._optVal_3.UseVisualStyleBackColor = False
        '
        '_optVal_2
        '
        Me._optVal_2.AutoSize = True
        Me._optVal_2.BackColor = System.Drawing.SystemColors.Control
        Me._optVal_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optVal_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optVal_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVal.SetIndex(Me._optVal_2, CType(2, Short))
        Me._optVal_2.Location = New System.Drawing.Point(154, 14)
        Me._optVal_2.Name = "_optVal_2"
        Me._optVal_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optVal_2.Size = New System.Drawing.Size(46, 17)
        Me._optVal_2.TabIndex = 26
        Me._optVal_2.TabStop = True
        Me._optVal_2.Text = "Sale"
        Me._optVal_2.UseVisualStyleBackColor = False
        '
        '_optVal_0
        '
        Me._optVal_0.AutoSize = True
        Me._optVal_0.BackColor = System.Drawing.SystemColors.Control
        Me._optVal_0.Checked = True
        Me._optVal_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optVal_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optVal_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVal.SetIndex(Me._optVal_0, CType(0, Short))
        Me._optVal_0.Location = New System.Drawing.Point(4, 14)
        Me._optVal_0.Name = "_optVal_0"
        Me._optVal_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optVal_0.Size = New System.Drawing.Size(62, 17)
        Me._optVal_0.TabIndex = 24
        Me._optVal_0.TabStop = True
        Me._optVal_0.Text = "Landed"
        Me._optVal_0.UseVisualStyleBackColor = False
        '
        '_optVal_1
        '
        Me._optVal_1.AutoSize = True
        Me._optVal_1.BackColor = System.Drawing.SystemColors.Control
        Me._optVal_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optVal_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optVal_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVal.SetIndex(Me._optVal_1, CType(1, Short))
        Me._optVal_1.Location = New System.Drawing.Point(4, 35)
        Me._optVal_1.Name = "_optVal_1"
        Me._optVal_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optVal_1.Size = New System.Drawing.Size(69, 17)
        Me._optVal_1.TabIndex = 25
        Me._optVal_1.TabStop = True
        Me._optVal_1.Text = "Purchase"
        Me._optVal_1.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me.Label2)
        Me.Frame6.Controls.Add(Me.Label1)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(126, 69)
        Me.Frame6.TabIndex = 2
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Period"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(48, 14)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 22)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(48, 40)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(75, 22)
        Me.txtDateTo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(19, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(25, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "To :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "From :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(84, 138)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 50
        '
        'SprdOption
        '
        Me.SprdOption.DataSource = Nothing
        Me.SprdOption.Location = New System.Drawing.Point(0, 101)
        Me.SprdOption.Name = "SprdOption"
        Me.SprdOption.OcxState = CType(resources.GetObject("SprdOption.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdOption.Size = New System.Drawing.Size(1084, 93)
        Me.SprdOption.TabIndex = 8
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.chkDespatchShow)
        Me.Frame2.Controls.Add(Me.chkViewAll)
        Me.Frame2.Controls.Add(Me.Frame7)
        Me.Frame2.Controls.Add(Me.Frame1)
        Me.Frame2.Controls.Add(Me.chkOption)
        Me.Frame2.Controls.Add(Me.FraOption)
        Me.Frame2.Controls.Add(Me.CmdPreview)
        Me.Frame2.Controls.Add(Me.cmdPrint)
        Me.Frame2.Controls.Add(Me.cmdShow)
        Me.Frame2.Controls.Add(Me.cmdExit)
        Me.Frame2.Controls.Add(Me.chkIncludOp)
        Me.Frame2.Controls.Add(Me.lblStockFlag)
        Me.Frame2.Controls.Add(Me.lblLabelType)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 543)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(1067, 67)
        Me.Frame2.TabIndex = 28
        Me.Frame2.TabStop = False
        '
        'chkDespatchShow
        '
        Me.chkDespatchShow.BackColor = System.Drawing.SystemColors.Control
        Me.chkDespatchShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkDespatchShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDespatchShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkDespatchShow.Location = New System.Drawing.Point(396, 37)
        Me.chkDespatchShow.Name = "chkDespatchShow"
        Me.chkDespatchShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkDespatchShow.Size = New System.Drawing.Size(111, 15)
        Me.chkDespatchShow.TabIndex = 52
        Me.chkDespatchShow.Text = "Show Despatch"
        Me.chkDespatchShow.UseVisualStyleBackColor = False
        Me.chkDespatchShow.Visible = False
        '
        'chkViewAll
        '
        Me.chkViewAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkViewAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkViewAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkViewAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkViewAll.Location = New System.Drawing.Point(396, 16)
        Me.chkViewAll.Name = "chkViewAll"
        Me.chkViewAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkViewAll.Size = New System.Drawing.Size(89, 15)
        Me.chkViewAll.TabIndex = 43
        Me.chkViewAll.Text = "View All"
        Me.chkViewAll.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboShow)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(515, -1)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(133, 41)
        Me.Frame7.TabIndex = 37
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Show"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(18, 14)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(110, 21)
        Me.cboShow.TabIndex = 38
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboRef)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(654, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(272, 64)
        Me.Frame1.TabIndex = 34
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Ref Type"
        '
        'cboRef
        '
        Me.cboRef.BackColor = System.Drawing.SystemColors.Window
        Me.cboRef.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRef.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboRef.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRef.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRef.IntegralHeight = False
        Me.cboRef.Items.AddRange(New Object() {"cboRef"})
        Me.cboRef.Location = New System.Drawing.Point(0, 15)
        Me.cboRef.Name = "cboRef"
        Me.cboRef.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRef.Size = New System.Drawing.Size(272, 49)
        Me.cboRef.TabIndex = 48
        '
        'chkOption
        '
        Me.chkOption.BackColor = System.Drawing.SystemColors.Control
        Me.chkOption.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOption.Location = New System.Drawing.Point(934, 16)
        Me.chkOption.Name = "chkOption"
        Me.chkOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOption.Size = New System.Drawing.Size(129, 17)
        Me.chkOption.TabIndex = 14
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
        Me.FraOption.Location = New System.Drawing.Point(933, 30)
        Me.FraOption.Name = "FraOption"
        Me.FraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOption.Size = New System.Drawing.Size(131, 35)
        Me.FraOption.TabIndex = 32
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
        Me.txtCondQty.Location = New System.Drawing.Point(84, 10)
        Me.txtCondQty.MaxLength = 0
        Me.txtCondQty.Name = "txtCondQty"
        Me.txtCondQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCondQty.Size = New System.Drawing.Size(43, 22)
        Me.txtCondQty.TabIndex = 16
        '
        'cboCond
        '
        Me.cboCond.BackColor = System.Drawing.SystemColors.Window
        Me.cboCond.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCond.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCond.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCond.Location = New System.Drawing.Point(36, 10)
        Me.cboCond.Name = "cboCond"
        Me.cboCond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCond.Size = New System.Drawing.Size(49, 21)
        Me.cboCond.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(4, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Qty :"
        '
        'chkIncludOp
        '
        Me.chkIncludOp.BackColor = System.Drawing.SystemColors.Control
        Me.chkIncludOp.Checked = True
        Me.chkIncludOp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludOp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkIncludOp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncludOp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkIncludOp.Location = New System.Drawing.Point(516, 45)
        Me.chkIncludOp.Name = "chkIncludOp"
        Me.chkIncludOp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkIncludOp.Size = New System.Drawing.Size(129, 17)
        Me.chkIncludOp.TabIndex = 47
        Me.chkIncludOp.Text = "Opening Including"
        Me.chkIncludOp.UseVisualStyleBackColor = False
        '
        'lblStockFlag
        '
        Me.lblStockFlag.AutoSize = True
        Me.lblStockFlag.BackColor = System.Drawing.SystemColors.Control
        Me.lblStockFlag.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblStockFlag.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStockFlag.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblStockFlag.Location = New System.Drawing.Point(236, 12)
        Me.lblStockFlag.Name = "lblStockFlag"
        Me.lblStockFlag.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblStockFlag.Size = New System.Drawing.Size(70, 13)
        Me.lblStockFlag.TabIndex = 29
        Me.lblStockFlag.Text = "lblStockFlag"
        Me.lblStockFlag.Visible = False
        '
        'lblLabelType
        '
        Me.lblLabelType.AutoSize = True
        Me.lblLabelType.BackColor = System.Drawing.SystemColors.Control
        Me.lblLabelType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabelType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabelType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabelType.Location = New System.Drawing.Point(236, 46)
        Me.lblLabelType.Name = "lblLabelType"
        Me.lblLabelType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabelType.Size = New System.Drawing.Size(71, 13)
        Me.lblLabelType.TabIndex = 30
        Me.lblLabelType.Text = "lblLabelType"
        Me.lblLabelType.Visible = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.CboItemType)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(129, 0)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(97, 46)
        Me.Frame3.TabIndex = 27
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Ware House"
        '
        'CboItemType
        '
        Me.CboItemType.BackColor = System.Drawing.SystemColors.Window
        Me.CboItemType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboItemType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboItemType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboItemType.Location = New System.Drawing.Point(2, 18)
        Me.CboItemType.Name = "CboItemType"
        Me.CboItemType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboItemType.Size = New System.Drawing.Size(91, 21)
        Me.CboItemType.TabIndex = 3
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cboDivision)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(325, 49)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(111, 48)
        Me.Frame8.TabIndex = 44
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
        Me.cboDivision.Location = New System.Drawing.Point(7, 18)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(99, 21)
        Me.cboDivision.TabIndex = 45
        '
        'FraAge
        '
        Me.FraAge.BackColor = System.Drawing.SystemColors.Control
        Me.FraAge.Controls.Add(Me.txtDays)
        Me.FraAge.Controls.Add(Me.lblDays)
        Me.FraAge.Controls.Add(Me.Label3)
        Me.FraAge.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAge.Location = New System.Drawing.Point(676, 0)
        Me.FraAge.Name = "FraAge"
        Me.FraAge.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAge.Size = New System.Drawing.Size(75, 53)
        Me.FraAge.TabIndex = 20
        Me.FraAge.TabStop = False
        Me.FraAge.Text = "Age Days"
        Me.FraAge.Visible = False
        '
        'txtDays
        '
        Me.txtDays.AcceptsReturn = True
        Me.txtDays.BackColor = System.Drawing.SystemColors.Window
        Me.txtDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDays.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDays.Location = New System.Drawing.Point(4, 30)
        Me.txtDays.MaxLength = 3
        Me.txtDays.Name = "txtDays"
        Me.txtDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDays.Size = New System.Drawing.Size(35, 22)
        Me.txtDays.TabIndex = 7
        '
        'lblDays
        '
        Me.lblDays.AutoSize = True
        Me.lblDays.BackColor = System.Drawing.Color.Transparent
        Me.lblDays.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDays.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDays.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lblDays.Location = New System.Drawing.Point(4, 16)
        Me.lblDays.Name = "lblDays"
        Me.lblDays.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDays.Size = New System.Drawing.Size(59, 13)
        Me.lblDays.TabIndex = 21
        Me.lblDays.Text = "More than"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(40, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "Days"
        '
        'optType
        '
        '
        'optVal
        '
        '
        'chkRunningBal
        '
        Me.chkRunningBal.BackColor = System.Drawing.SystemColors.Control
        Me.chkRunningBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRunningBal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRunningBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRunningBal.Location = New System.Drawing.Point(615, 58)
        Me.chkRunningBal.Name = "chkRunningBal"
        Me.chkRunningBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRunningBal.Size = New System.Drawing.Size(167, 19)
        Me.chkRunningBal.TabIndex = 51
        Me.chkRunningBal.Text = "Running Total Required"
        Me.chkRunningBal.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(834, -1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(166, 100)
        Me.GroupBox1.TabIndex = 77
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
        Me.lstCompanyName.Size = New System.Drawing.Size(166, 87)
        Me.lstCompanyName.TabIndex = 2
        '
        'cboCapital
        '
        Me.cboCapital.BackColor = System.Drawing.SystemColors.Window
        Me.cboCapital.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCapital.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCapital.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCapital.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCapital.Location = New System.Drawing.Point(62, 73)
        Me.cboCapital.Name = "cboCapital"
        Me.cboCapital.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCapital.Size = New System.Drawing.Size(57, 21)
        Me.cboCapital.TabIndex = 78
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 77)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(49, 13)
        Me.Label5.TabIndex = 79
        Me.Label5.Text = "Capital :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox2.Controls.Add(Me.lblYear)
        Me.GroupBox2.Controls.Add(Me.lblRunDate)
        Me.GroupBox2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox2.Location = New System.Drawing.Point(1003, -2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox2.Size = New System.Drawing.Size(78, 57)
        Me.GroupBox2.TabIndex = 80
        Me.GroupBox2.TabStop = False
        '
        'lblYear
        '
        Me.lblYear.CustomFormat = "yyyy"
        Me.lblYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.lblYear.Location = New System.Drawing.Point(5, 17)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.Size = New System.Drawing.Size(68, 22)
        Me.lblYear.TabIndex = 36
        '
        'lblRunDate
        '
        Me.lblRunDate.AutoSize = True
        Me.lblRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRunDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRunDate.Location = New System.Drawing.Point(10, 20)
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRunDate.Size = New System.Drawing.Size(48, 14)
        Me.lblRunDate.TabIndex = 7
        Me.lblRunDate.Text = "RunDate"
        Me.lblRunDate.Visible = False
        '
        'frmParamStock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1085, 611)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboCapital)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkRunningBal)
        Me.Controls.Add(Me.chkQCStockType)
        Me.Controls.Add(Me.cboExport)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.fraDetSum)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraVal)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.SprdOption)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.FraAge)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 16)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamStock"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Stock Statement"
        Me.cboExport.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.fraDetSum.ResumeLayout(False)
        Me.fraDetSum.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.FraVal.ResumeLayout(False)
        Me.FraVal.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdOption, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.FraOption.ResumeLayout(False)
        Me.FraOption.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.FraAge.ResumeLayout(False)
        Me.FraAge.PerformLayout()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optVal, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
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

    Public WithEvents chkRunningBal As CheckBox
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
    Public WithEvents cboCapital As ComboBox
    Public WithEvents Label5 As Label
    Public WithEvents GroupBox2 As GroupBox
    Friend WithEvents lblYear As DateTimePicker
    Public WithEvents lblRunDate As Label
#End Region
End Class