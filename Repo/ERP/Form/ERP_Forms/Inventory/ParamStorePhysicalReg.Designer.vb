Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamStorePhysicalReg
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
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents cboDept As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents _optType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_0 As System.Windows.Forms.RadioButton
    Public WithEvents fraDetSum As System.Windows.Forms.GroupBox
    Public WithEvents lstMaterialType As System.Windows.Forms.CheckedListBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents _optVal_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optVal_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optVal_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optVal_3 As System.Windows.Forms.RadioButton
    Public WithEvents chkZeroRate As System.Windows.Forms.CheckBox
    Public WithEvents FraVal As System.Windows.Forms.GroupBox
    Public WithEvents chkAfterUpdate As System.Windows.Forms.CheckBox
    Public WithEvents chkRate As System.Windows.Forms.CheckBox
    Public WithEvents CboSType As System.Windows.Forms.ComboBox
    Public WithEvents txtLocation As System.Windows.Forms.TextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents CboWareHouse As System.Windows.Forms.ComboBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSearchModel As System.Windows.Forms.Button
    Public WithEvents txtModel As System.Windows.Forms.TextBox
    Public WithEvents chkModel As System.Windows.Forms.CheckBox
    Public WithEvents cmdItemDesc As System.Windows.Forms.Button
    Public WithEvents txtItemName As System.Windows.Forms.TextBox
    Public WithEvents chkItemAll As System.Windows.Forms.CheckBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents chkPhyItem As System.Windows.Forms.CheckBox
    Public WithEvents chkZeroBal As System.Windows.Forms.CheckBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cboCond As System.Windows.Forms.ComboBox
    Public WithEvents txtCondQty As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents FraOption As System.Windows.Forms.GroupBox
    Public WithEvents chkOption As System.Windows.Forms.CheckBox
    Public WithEvents FraConditional As System.Windows.Forms.GroupBox
    Public WithEvents _optNonMoving_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optNonMoving_0 As System.Windows.Forms.RadioButton
    Public WithEvents FraNonMoving As System.Windows.Forms.GroupBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents optNonMoving As VB6.RadioButtonArray
    Public WithEvents optType As VB6.RadioButtonArray
    Public WithEvents optVal As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamStorePhysicalReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchModel = New System.Windows.Forms.Button()
        Me.cmdItemDesc = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboDept = New System.Windows.Forms.ComboBox()
        Me.fraDetSum = New System.Windows.Forms.GroupBox()
        Me._optType_1 = New System.Windows.Forms.RadioButton()
        Me._optType_0 = New System.Windows.Forms.RadioButton()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.lstMaterialType = New System.Windows.Forms.CheckedListBox()
        Me.FraVal = New System.Windows.Forms.GroupBox()
        Me._optVal_1 = New System.Windows.Forms.RadioButton()
        Me._optVal_0 = New System.Windows.Forms.RadioButton()
        Me._optVal_2 = New System.Windows.Forms.RadioButton()
        Me._optVal_3 = New System.Windows.Forms.RadioButton()
        Me.chkZeroRate = New System.Windows.Forms.CheckBox()
        Me.chkAfterUpdate = New System.Windows.Forms.CheckBox()
        Me.chkRate = New System.Windows.Forms.CheckBox()
        Me.CboSType = New System.Windows.Forms.ComboBox()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.CboWareHouse = New System.Windows.Forms.ComboBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtModel = New System.Windows.Forms.TextBox()
        Me.chkModel = New System.Windows.Forms.CheckBox()
        Me.txtItemName = New System.Windows.Forms.TextBox()
        Me.chkItemAll = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.chkPhyItem = New System.Windows.Forms.CheckBox()
        Me.chkZeroBal = New System.Windows.Forms.CheckBox()
        Me.FraConditional = New System.Windows.Forms.GroupBox()
        Me.FraOption = New System.Windows.Forms.GroupBox()
        Me.cboCond = New System.Windows.Forms.ComboBox()
        Me.txtCondQty = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkOption = New System.Windows.Forms.CheckBox()
        Me.FraNonMoving = New System.Windows.Forms.GroupBox()
        Me._optNonMoving_1 = New System.Windows.Forms.RadioButton()
        Me._optNonMoving_0 = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.optNonMoving = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optVal = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me._optType_2 = New System.Windows.Forms.RadioButton()
        Me.Frame10.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.fraDetSum.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.FraVal.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraConditional.SuspendLayout()
        Me.FraOption.SuspendLayout()
        Me.FraNonMoving.SuspendLayout()
        CType(Me.optNonMoving, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optVal, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchModel
        '
        Me.cmdSearchModel.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchModel.Enabled = False
        Me.cmdSearchModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchModel.Image = CType(resources.GetObject("cmdSearchModel.Image"), System.Drawing.Image)
        Me.cmdSearchModel.Location = New System.Drawing.Point(313, 43)
        Me.cmdSearchModel.Name = "cmdSearchModel"
        Me.cmdSearchModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchModel.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchModel.TabIndex = 30
        Me.cmdSearchModel.TabStop = False
        Me.cmdSearchModel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchModel, "Search")
        Me.cmdSearchModel.UseVisualStyleBackColor = False
        '
        'cmdItemDesc
        '
        Me.cmdItemDesc.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdItemDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdItemDesc.Enabled = False
        Me.cmdItemDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdItemDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdItemDesc.Image = CType(resources.GetObject("cmdItemDesc.Image"), System.Drawing.Image)
        Me.cmdItemDesc.Location = New System.Drawing.Point(313, 13)
        Me.cmdItemDesc.Name = "cmdItemDesc"
        Me.cmdItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdItemDesc.Size = New System.Drawing.Size(28, 23)
        Me.cmdItemDesc.TabIndex = 12
        Me.cmdItemDesc.TabStop = False
        Me.cmdItemDesc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdItemDesc, "Search")
        Me.cmdItemDesc.UseVisualStyleBackColor = False
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
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.cboDivision)
        Me.Frame10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(399, 0)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(375, 44)
        Me.Frame10.TabIndex = 55
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(4, 14)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(239, 21)
        Me.cboDivision.TabIndex = 56
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cboShow)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(399, 42)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(93, 44)
        Me.Frame8.TabIndex = 53
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Show"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(4, 16)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(87, 21)
        Me.cboShow.TabIndex = 54
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboDept)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(2, 42)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(394, 38)
        Me.Frame7.TabIndex = 51
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Department"
        '
        'cboDept
        '
        Me.cboDept.BackColor = System.Drawing.SystemColors.Window
        Me.cboDept.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDept.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDept.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDept.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDept.Location = New System.Drawing.Point(79, 12)
        Me.cboDept.Name = "cboDept"
        Me.cboDept.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDept.Size = New System.Drawing.Size(309, 21)
        Me.cboDept.TabIndex = 52
        '
        'fraDetSum
        '
        Me.fraDetSum.BackColor = System.Drawing.SystemColors.Control
        Me.fraDetSum.Controls.Add(Me._optType_2)
        Me.fraDetSum.Controls.Add(Me._optType_1)
        Me.fraDetSum.Controls.Add(Me._optType_0)
        Me.fraDetSum.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraDetSum.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraDetSum.Location = New System.Drawing.Point(494, 42)
        Me.fraDetSum.Name = "fraDetSum"
        Me.fraDetSum.Padding = New System.Windows.Forms.Padding(0)
        Me.fraDetSum.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraDetSum.Size = New System.Drawing.Size(280, 44)
        Me.fraDetSum.TabIndex = 48
        Me.fraDetSum.TabStop = False
        Me.fraDetSum.Text = "Type"
        '
        '_optType_1
        '
        Me._optType_1.AutoSize = True
        Me._optType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optType_1.Checked = True
        Me._optType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_1, CType(1, Short))
        Me._optType_1.Location = New System.Drawing.Point(66, 15)
        Me._optType_1.Name = "_optType_1"
        Me._optType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_1.Size = New System.Drawing.Size(116, 17)
        Me._optType_1.TabIndex = 50
        Me._optType_1.TabStop = True
        Me._optType_1.Text = "Summarised Dept"
        Me._optType_1.UseVisualStyleBackColor = False
        '
        '_optType_0
        '
        Me._optType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_0, CType(0, Short))
        Me._optType_0.Location = New System.Drawing.Point(4, 15)
        Me._optType_0.Name = "_optType_0"
        Me._optType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_0.Size = New System.Drawing.Size(56, 20)
        Me._optType_0.TabIndex = 49
        Me._optType_0.TabStop = True
        Me._optType_0.Text = "Detail"
        Me._optType_0.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.lstMaterialType)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(777, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(240, 118)
        Me.Frame5.TabIndex = 46
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Category"
        '
        'lstMaterialType
        '
        Me.lstMaterialType.BackColor = System.Drawing.SystemColors.Window
        Me.lstMaterialType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstMaterialType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstMaterialType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstMaterialType.IntegralHeight = False
        Me.lstMaterialType.Items.AddRange(New Object() {"lstMaterialType"})
        Me.lstMaterialType.Location = New System.Drawing.Point(2, 14)
        Me.lstMaterialType.Name = "lstMaterialType"
        Me.lstMaterialType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstMaterialType.Size = New System.Drawing.Size(234, 100)
        Me.lstMaterialType.TabIndex = 47
        '
        'FraVal
        '
        Me.FraVal.BackColor = System.Drawing.SystemColors.Control
        Me.FraVal.Controls.Add(Me._optVal_1)
        Me.FraVal.Controls.Add(Me._optVal_0)
        Me.FraVal.Controls.Add(Me._optVal_2)
        Me.FraVal.Controls.Add(Me._optVal_3)
        Me.FraVal.Controls.Add(Me.chkZeroRate)
        Me.FraVal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraVal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraVal.Location = New System.Drawing.Point(397, 113)
        Me.FraVal.Name = "FraVal"
        Me.FraVal.Padding = New System.Windows.Forms.Padding(0)
        Me.FraVal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraVal.Size = New System.Drawing.Size(499, 41)
        Me.FraVal.TabIndex = 40
        Me.FraVal.TabStop = False
        Me.FraVal.Text = "Valuation Price at"
        '
        '_optVal_1
        '
        Me._optVal_1.AutoSize = True
        Me._optVal_1.BackColor = System.Drawing.SystemColors.Control
        Me._optVal_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optVal_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optVal_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVal.SetIndex(Me._optVal_1, CType(1, Short))
        Me._optVal_1.Location = New System.Drawing.Point(90, 15)
        Me._optVal_1.Name = "_optVal_1"
        Me._optVal_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optVal_1.Size = New System.Drawing.Size(69, 17)
        Me._optVal_1.TabIndex = 45
        Me._optVal_1.TabStop = True
        Me._optVal_1.Text = "Purchase"
        Me._optVal_1.UseVisualStyleBackColor = False
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
        Me._optVal_0.Location = New System.Drawing.Point(2, 15)
        Me._optVal_0.Name = "_optVal_0"
        Me._optVal_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optVal_0.Size = New System.Drawing.Size(62, 17)
        Me._optVal_0.TabIndex = 44
        Me._optVal_0.TabStop = True
        Me._optVal_0.Text = "Landed"
        Me._optVal_0.UseVisualStyleBackColor = False
        '
        '_optVal_2
        '
        Me._optVal_2.AutoSize = True
        Me._optVal_2.BackColor = System.Drawing.SystemColors.Control
        Me._optVal_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optVal_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optVal_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVal.SetIndex(Me._optVal_2, CType(2, Short))
        Me._optVal_2.Location = New System.Drawing.Point(273, 15)
        Me._optVal_2.Name = "_optVal_2"
        Me._optVal_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optVal_2.Size = New System.Drawing.Size(46, 17)
        Me._optVal_2.TabIndex = 43
        Me._optVal_2.TabStop = True
        Me._optVal_2.Text = "Sale"
        Me._optVal_2.UseVisualStyleBackColor = False
        '
        '_optVal_3
        '
        Me._optVal_3.AutoSize = True
        Me._optVal_3.BackColor = System.Drawing.SystemColors.Control
        Me._optVal_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optVal_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optVal_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optVal.SetIndex(Me._optVal_3, CType(3, Short))
        Me._optVal_3.Location = New System.Drawing.Point(185, 15)
        Me._optVal_3.Name = "_optVal_3"
        Me._optVal_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optVal_3.Size = New System.Drawing.Size(62, 17)
        Me._optVal_3.TabIndex = 42
        Me._optVal_3.TabStop = True
        Me._optVal_3.Text = "Current"
        Me._optVal_3.UseVisualStyleBackColor = False
        '
        'chkZeroRate
        '
        Me.chkZeroRate.AutoSize = True
        Me.chkZeroRate.BackColor = System.Drawing.SystemColors.Control
        Me.chkZeroRate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkZeroRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkZeroRate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkZeroRate.Location = New System.Drawing.Point(345, 15)
        Me.chkZeroRate.Name = "chkZeroRate"
        Me.chkZeroRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkZeroRate.Size = New System.Drawing.Size(145, 17)
        Me.chkZeroRate.TabIndex = 41
        Me.chkZeroRate.Text = "Zero Qty Rate Required"
        Me.chkZeroRate.UseVisualStyleBackColor = False
        Me.chkZeroRate.Visible = False
        '
        'chkAfterUpdate
        '
        Me.chkAfterUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.chkAfterUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAfterUpdate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAfterUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAfterUpdate.Location = New System.Drawing.Point(400, 93)
        Me.chkAfterUpdate.Name = "chkAfterUpdate"
        Me.chkAfterUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAfterUpdate.Size = New System.Drawing.Size(144, 20)
        Me.chkAfterUpdate.TabIndex = 39
        Me.chkAfterUpdate.Text = "After Inventory Update"
        Me.chkAfterUpdate.UseVisualStyleBackColor = False
        '
        'chkRate
        '
        Me.chkRate.BackColor = System.Drawing.SystemColors.Control
        Me.chkRate.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkRate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkRate.Location = New System.Drawing.Point(554, 93)
        Me.chkRate.Name = "chkRate"
        Me.chkRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkRate.Size = New System.Drawing.Size(98, 16)
        Me.chkRate.TabIndex = 38
        Me.chkRate.Text = "Rate Required"
        Me.chkRate.UseVisualStyleBackColor = False
        '
        'CboSType
        '
        Me.CboSType.BackColor = System.Drawing.SystemColors.Window
        Me.CboSType.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboSType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboSType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboSType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboSType.Location = New System.Drawing.Point(440, 579)
        Me.CboSType.Name = "CboSType"
        Me.CboSType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboSType.Size = New System.Drawing.Size(71, 21)
        Me.CboSType.TabIndex = 36
        '
        'txtLocation
        '
        Me.txtLocation.AcceptsReturn = True
        Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
        Me.txtLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLocation.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLocation.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLocation.ForeColor = System.Drawing.Color.Blue
        Me.txtLocation.Location = New System.Drawing.Point(590, 579)
        Me.txtLocation.MaxLength = 0
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLocation.Size = New System.Drawing.Size(53, 22)
        Me.txtLocation.TabIndex = 32
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.Label1)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me.Label2)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(234, 42)
        Me.Frame6.TabIndex = 0
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "As On"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Enabled = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(44, 12)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 22)
        Me.txtDateFrom.TabIndex = 25
        Me.txtDateFrom.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Enabled = False
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(2, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "From :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.Label1.Visible = False
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(155, 14)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(75, 22)
        Me.txtDateTo.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(127, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(25, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "To :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.CboWareHouse)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(238, 0)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(158, 42)
        Me.Frame4.TabIndex = 27
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Ware House"
        '
        'CboWareHouse
        '
        Me.CboWareHouse.BackColor = System.Drawing.SystemColors.Window
        Me.CboWareHouse.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboWareHouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboWareHouse.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboWareHouse.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboWareHouse.Location = New System.Drawing.Point(2, 16)
        Me.CboWareHouse.Name = "CboWareHouse"
        Me.CboWareHouse.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboWareHouse.Size = New System.Drawing.Size(154, 21)
        Me.CboWareHouse.TabIndex = 34
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.cmdSearchModel)
        Me.Frame3.Controls.Add(Me.txtModel)
        Me.Frame3.Controls.Add(Me.chkModel)
        Me.Frame3.Controls.Add(Me.cmdItemDesc)
        Me.Frame3.Controls.Add(Me.txtItemName)
        Me.Frame3.Controls.Add(Me.chkItemAll)
        Me.Frame3.Controls.Add(Me.Label8)
        Me.Frame3.Controls.Add(Me.Label5)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(2, 80)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(394, 73)
        Me.Frame3.TabIndex = 9
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Show"
        '
        'txtModel
        '
        Me.txtModel.AcceptsReturn = True
        Me.txtModel.BackColor = System.Drawing.SystemColors.Window
        Me.txtModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModel.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtModel.Enabled = False
        Me.txtModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModel.ForeColor = System.Drawing.Color.Blue
        Me.txtModel.Location = New System.Drawing.Point(70, 43)
        Me.txtModel.MaxLength = 0
        Me.txtModel.Name = "txtModel"
        Me.txtModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtModel.Size = New System.Drawing.Size(240, 22)
        Me.txtModel.TabIndex = 29
        '
        'chkModel
        '
        Me.chkModel.BackColor = System.Drawing.SystemColors.Control
        Me.chkModel.Checked = True
        Me.chkModel.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkModel.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkModel.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkModel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkModel.Location = New System.Drawing.Point(346, 43)
        Me.chkModel.Name = "chkModel"
        Me.chkModel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkModel.Size = New System.Drawing.Size(41, 19)
        Me.chkModel.TabIndex = 28
        Me.chkModel.Text = "All"
        Me.chkModel.UseVisualStyleBackColor = False
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
        Me.txtItemName.Location = New System.Drawing.Point(70, 13)
        Me.txtItemName.MaxLength = 0
        Me.txtItemName.Name = "txtItemName"
        Me.txtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemName.Size = New System.Drawing.Size(240, 22)
        Me.txtItemName.TabIndex = 11
        '
        'chkItemAll
        '
        Me.chkItemAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkItemAll.Checked = True
        Me.chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkItemAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkItemAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkItemAll.Location = New System.Drawing.Point(346, 15)
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkItemAll.Size = New System.Drawing.Size(41, 19)
        Me.chkItemAll.TabIndex = 10
        Me.chkItemAll.Text = "All"
        Me.chkItemAll.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(20, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Model :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(3, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Item Desc :"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(0, 158)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1019, 406)
        Me.SprdMain.TabIndex = 1
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(84, 138)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 56
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.CmdPreview)
        Me.Frame2.Controls.Add(Me.cmdPrint)
        Me.Frame2.Controls.Add(Me.cmdShow)
        Me.Frame2.Controls.Add(Me.cmdExit)
        Me.Frame2.Controls.Add(Me.lblBookType)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(767, 560)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(251, 51)
        Me.Frame2.TabIndex = 7
        Me.Frame2.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(190, 18)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(71, 13)
        Me.lblBookType.TabIndex = 14
        Me.lblBookType.Text = "lblBookType"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.chkPhyItem)
        Me.Frame1.Controls.Add(Me.chkZeroBal)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 560)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(145, 51)
        Me.Frame1.TabIndex = 21
        Me.Frame1.TabStop = False
        '
        'chkPhyItem
        '
        Me.chkPhyItem.BackColor = System.Drawing.SystemColors.Control
        Me.chkPhyItem.Checked = True
        Me.chkPhyItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPhyItem.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPhyItem.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPhyItem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPhyItem.Location = New System.Drawing.Point(2, 32)
        Me.chkPhyItem.Name = "chkPhyItem"
        Me.chkPhyItem.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPhyItem.Size = New System.Drawing.Size(141, 16)
        Me.chkPhyItem.TabIndex = 35
        Me.chkPhyItem.Text = "Show Only Physical"
        Me.chkPhyItem.UseVisualStyleBackColor = False
        '
        'chkZeroBal
        '
        Me.chkZeroBal.BackColor = System.Drawing.SystemColors.Control
        Me.chkZeroBal.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkZeroBal.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkZeroBal.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkZeroBal.Location = New System.Drawing.Point(2, 14)
        Me.chkZeroBal.Name = "chkZeroBal"
        Me.chkZeroBal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkZeroBal.Size = New System.Drawing.Size(141, 16)
        Me.chkZeroBal.TabIndex = 22
        Me.chkZeroBal.Text = "Hide Zero Balance"
        Me.chkZeroBal.UseVisualStyleBackColor = False
        '
        'FraConditional
        '
        Me.FraConditional.BackColor = System.Drawing.SystemColors.Control
        Me.FraConditional.Controls.Add(Me.FraOption)
        Me.FraConditional.Controls.Add(Me.chkOption)
        Me.FraConditional.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraConditional.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraConditional.Location = New System.Drawing.Point(146, 560)
        Me.FraConditional.Name = "FraConditional"
        Me.FraConditional.Padding = New System.Windows.Forms.Padding(0)
        Me.FraConditional.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraConditional.Size = New System.Drawing.Size(241, 51)
        Me.FraConditional.TabIndex = 15
        Me.FraConditional.TabStop = False
        '
        'FraOption
        '
        Me.FraOption.BackColor = System.Drawing.SystemColors.Control
        Me.FraOption.Controls.Add(Me.cboCond)
        Me.FraOption.Controls.Add(Me.txtCondQty)
        Me.FraOption.Controls.Add(Me.Label4)
        Me.FraOption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOption.Location = New System.Drawing.Point(96, 0)
        Me.FraOption.Name = "FraOption"
        Me.FraOption.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOption.Size = New System.Drawing.Size(143, 51)
        Me.FraOption.TabIndex = 17
        Me.FraOption.TabStop = False
        Me.FraOption.Visible = False
        '
        'cboCond
        '
        Me.cboCond.BackColor = System.Drawing.SystemColors.Window
        Me.cboCond.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCond.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCond.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCond.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCond.Location = New System.Drawing.Point(38, 18)
        Me.cboCond.Name = "cboCond"
        Me.cboCond.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCond.Size = New System.Drawing.Size(59, 21)
        Me.cboCond.TabIndex = 19
        '
        'txtCondQty
        '
        Me.txtCondQty.AcceptsReturn = True
        Me.txtCondQty.BackColor = System.Drawing.SystemColors.Window
        Me.txtCondQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCondQty.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCondQty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCondQty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCondQty.Location = New System.Drawing.Point(98, 18)
        Me.txtCondQty.MaxLength = 0
        Me.txtCondQty.Name = "txtCondQty"
        Me.txtCondQty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCondQty.Size = New System.Drawing.Size(41, 22)
        Me.txtCondQty.TabIndex = 18
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(10, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Qty :"
        '
        'chkOption
        '
        Me.chkOption.BackColor = System.Drawing.SystemColors.Control
        Me.chkOption.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOption.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOption.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOption.Location = New System.Drawing.Point(4, 16)
        Me.chkOption.Name = "chkOption"
        Me.chkOption.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOption.Size = New System.Drawing.Size(89, 23)
        Me.chkOption.TabIndex = 16
        Me.chkOption.Text = "Conditional Check"
        Me.chkOption.UseVisualStyleBackColor = False
        '
        'FraNonMoving
        '
        Me.FraNonMoving.BackColor = System.Drawing.SystemColors.Control
        Me.FraNonMoving.Controls.Add(Me._optNonMoving_1)
        Me.FraNonMoving.Controls.Add(Me._optNonMoving_0)
        Me.FraNonMoving.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraNonMoving.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraNonMoving.Location = New System.Drawing.Point(148, 561)
        Me.FraNonMoving.Name = "FraNonMoving"
        Me.FraNonMoving.Padding = New System.Windows.Forms.Padding(0)
        Me.FraNonMoving.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraNonMoving.Size = New System.Drawing.Size(239, 51)
        Me.FraNonMoving.TabIndex = 23
        Me.FraNonMoving.TabStop = False
        '
        '_optNonMoving_1
        '
        Me._optNonMoving_1.BackColor = System.Drawing.SystemColors.Control
        Me._optNonMoving_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optNonMoving_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optNonMoving_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optNonMoving.SetIndex(Me._optNonMoving_1, CType(1, Short))
        Me._optNonMoving_1.Location = New System.Drawing.Point(8, 30)
        Me._optNonMoving_1.Name = "_optNonMoving_1"
        Me._optNonMoving_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optNonMoving_1.Size = New System.Drawing.Size(127, 16)
        Me._optNonMoving_1.TabIndex = 26
        Me._optNonMoving_1.TabStop = True
        Me._optNonMoving_1.Text = "Non-Issue Item"
        Me._optNonMoving_1.UseVisualStyleBackColor = False
        '
        '_optNonMoving_0
        '
        Me._optNonMoving_0.BackColor = System.Drawing.SystemColors.Control
        Me._optNonMoving_0.Checked = True
        Me._optNonMoving_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optNonMoving_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optNonMoving_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optNonMoving.SetIndex(Me._optNonMoving_0, CType(0, Short))
        Me._optNonMoving_0.Location = New System.Drawing.Point(8, 12)
        Me._optNonMoving_0.Name = "_optNonMoving_0"
        Me._optNonMoving_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optNonMoving_0.Size = New System.Drawing.Size(123, 16)
        Me._optNonMoving_0.TabIndex = 25
        Me._optNonMoving_0.TabStop = True
        Me._optNonMoving_0.Text = "Non-Moving Item"
        Me._optNonMoving_0.UseVisualStyleBackColor = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(400, 581)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(37, 13)
        Me.Label10.TabIndex = 37
        Me.Label10.Text = "Type :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(532, 583)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Location :"
        '
        'optNonMoving
        '
        '
        '_optType_2
        '
        Me._optType_2.BackColor = System.Drawing.SystemColors.Control
        Me._optType_2.Checked = True
        Me._optType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._optType_2.Location = New System.Drawing.Point(183, 15)
        Me._optType_2.Name = "_optType_2"
        Me._optType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_2.Size = New System.Drawing.Size(90, 20)
        Me._optType_2.TabIndex = 51
        Me._optType_2.TabStop = True
        Me._optType_2.Text = "Summarised"
        Me._optType_2.UseVisualStyleBackColor = False
        '
        'frmParamStorePhysicalReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1019, 611)
        Me.Controls.Add(Me.Frame10)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.fraDetSum)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.FraVal)
        Me.Controls.Add(Me.chkAfterUpdate)
        Me.Controls.Add(Me.chkRate)
        Me.Controls.Add(Me.CboSType)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.SprdMain)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraConditional)
        Me.Controls.Add(Me.FraNonMoving)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label6)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 16)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamStorePhysicalReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Physical Stock Register"
        Me.Frame10.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.fraDetSum.ResumeLayout(False)
        Me.fraDetSum.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.FraVal.ResumeLayout(False)
        Me.FraVal.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.FraConditional.ResumeLayout(False)
        Me.FraOption.ResumeLayout(False)
        Me.FraOption.PerformLayout()
        Me.FraNonMoving.ResumeLayout(False)
        CType(Me.optNonMoving, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optVal, System.ComponentModel.ISupportInitialize).EndInit()
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

    Public WithEvents txtDateFrom As MaskedTextBox
    Public WithEvents Label1 As Label
    Public WithEvents _optType_2 As RadioButton
#End Region
End Class