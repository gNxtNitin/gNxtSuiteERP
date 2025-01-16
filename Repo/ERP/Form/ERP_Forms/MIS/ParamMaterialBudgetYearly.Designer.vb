Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamMaterialBudgetYearly
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
        'Me.MDIParent = MIS.Master
        'MIS.Master.Show()
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
    Public WithEvents _optBaseOn_4 As System.Windows.Forms.RadioButton
    Public WithEvents _optBaseOn_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optBaseOn_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optBaseOn_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optBaseOn_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _optCalcOn_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optCalcOn_0 As System.Windows.Forms.RadioButton
    Public WithEvents cboType As System.Windows.Forms.ComboBox
    Public WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchCustName As System.Windows.Forms.Button
    Public WithEvents chkAllCustomer As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchFG As System.Windows.Forms.Button
    Public WithEvents txtFGName As System.Windows.Forms.TextBox
    Public WithEvents chkFG As System.Windows.Forms.CheckBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents _Label4_1 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _optRate_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optRate_1 As System.Windows.Forms.RadioButton
    Public WithEvents txtRateAsOn As System.Windows.Forms.MaskedTextBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
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
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Label4 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents optBaseOn As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optCalcOn As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optRate As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamMaterialBudgetYearly))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.cmdsearchCustName = New System.Windows.Forms.Button()
        Me.cmdSearchFG = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optBaseOn_4 = New System.Windows.Forms.RadioButton()
        Me._optBaseOn_3 = New System.Windows.Forms.RadioButton()
        Me._optBaseOn_2 = New System.Windows.Forms.RadioButton()
        Me._optBaseOn_1 = New System.Windows.Forms.RadioButton()
        Me._optBaseOn_0 = New System.Windows.Forms.RadioButton()
        Me._optCalcOn_1 = New System.Windows.Forms.RadioButton()
        Me._optCalcOn_0 = New System.Windows.Forms.RadioButton()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.chkAllCustomer = New System.Windows.Forms.CheckBox()
        Me.txtFGName = New System.Windows.Forms.TextBox()
        Me.chkFG = New System.Windows.Forms.CheckBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label2 = New System.Windows.Forms.Label()
        Me._Label4_1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._optRate_0 = New System.Windows.Forms.RadioButton()
        Me._optRate_1 = New System.Windows.Forms.RadioButton()
        Me.txtRateAsOn = New System.Windows.Forms.MaskedTextBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblBookSubType = New System.Windows.Forms.Label()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Label4 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optBaseOn = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optCalcOn = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optRate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame4.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optBaseOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCalcOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AcceptsReturn = True
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerName.Location = New System.Drawing.Point(120, 10)
        Me.txtCustomerName.MaxLength = 0
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerName.Size = New System.Drawing.Size(347, 19)
        Me.txtCustomerName.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtCustomerName, "Press F1 For Help")
        '
        'cmdsearchCustName
        '
        Me.cmdsearchCustName.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchCustName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchCustName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchCustName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchCustName.Image = CType(resources.GetObject("cmdsearchCustName.Image"), System.Drawing.Image)
        Me.cmdsearchCustName.Location = New System.Drawing.Point(467, 10)
        Me.cmdsearchCustName.Name = "cmdsearchCustName"
        Me.cmdsearchCustName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchCustName.Size = New System.Drawing.Size(29, 19)
        Me.cmdsearchCustName.TabIndex = 14
        Me.cmdsearchCustName.TabStop = False
        Me.cmdsearchCustName.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchCustName, "Search")
        Me.cmdsearchCustName.UseVisualStyleBackColor = False
        '
        'cmdSearchFG
        '
        Me.cmdSearchFG.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchFG.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchFG.Image = CType(resources.GetObject("cmdSearchFG.Image"), System.Drawing.Image)
        Me.cmdSearchFG.Location = New System.Drawing.Point(468, 34)
        Me.cmdSearchFG.Name = "cmdSearchFG"
        Me.cmdSearchFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchFG.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchFG.TabIndex = 9
        Me.cmdSearchFG.TabStop = False
        Me.cmdSearchFG.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchFG, "Search")
        Me.cmdSearchFG.UseVisualStyleBackColor = False
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(1033, 10)
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
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(967, 10)
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
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(901, 10)
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
        Me.cmdShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdShow.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdShow.Image = CType(resources.GetObject("cmdShow.Image"), System.Drawing.Image)
        Me.cmdShow.Location = New System.Drawing.Point(835, 10)
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
        Me.Frame4.Controls.Add(Me.Frame1)
        Me.Frame4.Controls.Add(Me._optCalcOn_1)
        Me.Frame4.Controls.Add(Me._optCalcOn_0)
        Me.Frame4.Controls.Add(Me.cboType)
        Me.Frame4.Controls.Add(Me.txtCustomerName)
        Me.Frame4.Controls.Add(Me.cmdsearchCustName)
        Me.Frame4.Controls.Add(Me.chkAllCustomer)
        Me.Frame4.Controls.Add(Me.cmdSearchFG)
        Me.Frame4.Controls.Add(Me.txtFGName)
        Me.Frame4.Controls.Add(Me.chkFG)
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me.Label2)
        Me.Frame4.Controls.Add(Me._Label4_1)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -2)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1104, 578)
        Me.Frame4.TabIndex = 5
        Me.Frame4.TabStop = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optBaseOn_4)
        Me.Frame1.Controls.Add(Me._optBaseOn_3)
        Me.Frame1.Controls.Add(Me._optBaseOn_2)
        Me.Frame1.Controls.Add(Me._optBaseOn_1)
        Me.Frame1.Controls.Add(Me._optBaseOn_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(120, 54)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(601, 37)
        Me.Frame1.TabIndex = 21
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Base On"
        '
        '_optBaseOn_4
        '
        Me._optBaseOn_4.AutoSize = True
        Me._optBaseOn_4.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_4.Checked = True
        Me._optBaseOn_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_4, CType(4, Short))
        Me._optBaseOn_4.Location = New System.Drawing.Point(422, 16)
        Me._optBaseOn_4.Name = "_optBaseOn_4"
        Me._optBaseOn_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_4.Size = New System.Drawing.Size(126, 18)
        Me._optBaseOn_4.TabIndex = 30
        Me._optBaseOn_4.TabStop = True
        Me._optBaseOn_4.Text = "Net Sale Including D3"
        Me._optBaseOn_4.UseVisualStyleBackColor = False
        '
        '_optBaseOn_3
        '
        Me._optBaseOn_3.AutoSize = True
        Me._optBaseOn_3.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_3, CType(3, Short))
        Me._optBaseOn_3.Location = New System.Drawing.Point(322, 16)
        Me._optBaseOn_3.Name = "_optBaseOn_3"
        Me._optBaseOn_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_3.Size = New System.Drawing.Size(62, 18)
        Me._optBaseOn_3.TabIndex = 29
        Me._optBaseOn_3.TabStop = True
        Me._optBaseOn_3.Text = "Sale D3"
        Me._optBaseOn_3.UseVisualStyleBackColor = False
        '
        '_optBaseOn_2
        '
        Me._optBaseOn_2.AutoSize = True
        Me._optBaseOn_2.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_2, CType(2, Short))
        Me._optBaseOn_2.Location = New System.Drawing.Point(224, 16)
        Me._optBaseOn_2.Name = "_optBaseOn_2"
        Me._optBaseOn_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_2.Size = New System.Drawing.Size(65, 18)
        Me._optBaseOn_2.TabIndex = 28
        Me._optBaseOn_2.TabStop = True
        Me._optBaseOn_2.Text = "Net Sale"
        Me._optBaseOn_2.UseVisualStyleBackColor = False
        '
        '_optBaseOn_1
        '
        Me._optBaseOn_1.AutoSize = True
        Me._optBaseOn_1.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_1, CType(1, Short))
        Me._optBaseOn_1.Location = New System.Drawing.Point(136, 16)
        Me._optBaseOn_1.Name = "_optBaseOn_1"
        Me._optBaseOn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_1.Size = New System.Drawing.Size(71, 18)
        Me._optBaseOn_1.TabIndex = 23
        Me._optBaseOn_1.TabStop = True
        Me._optBaseOn_1.Text = "Despatch"
        Me._optBaseOn_1.UseVisualStyleBackColor = False
        '
        '_optBaseOn_0
        '
        Me._optBaseOn_0.AutoSize = True
        Me._optBaseOn_0.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_0, CType(0, Short))
        Me._optBaseOn_0.Location = New System.Drawing.Point(58, 16)
        Me._optBaseOn_0.Name = "_optBaseOn_0"
        Me._optBaseOn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_0.Size = New System.Drawing.Size(59, 18)
        Me._optBaseOn_0.TabIndex = 22
        Me._optBaseOn_0.TabStop = True
        Me._optBaseOn_0.Text = "Budget"
        Me._optBaseOn_0.UseVisualStyleBackColor = False
        '
        '_optCalcOn_1
        '
        Me._optCalcOn_1.AutoSize = True
        Me._optCalcOn_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCalcOn_1.Checked = True
        Me._optCalcOn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCalcOn_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCalcOn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCalcOn.SetIndex(Me._optCalcOn_1, CType(1, Short))
        Me._optCalcOn_1.Location = New System.Drawing.Point(662, 38)
        Me._optCalcOn_1.Name = "_optCalcOn_1"
        Me._optCalcOn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCalcOn_1.Size = New System.Drawing.Size(91, 18)
        Me._optCalcOn_1.TabIndex = 20
        Me._optCalcOn_1.TabStop = True
        Me._optCalcOn_1.Text = "Gross Weight"
        Me._optCalcOn_1.UseVisualStyleBackColor = False
        '
        '_optCalcOn_0
        '
        Me._optCalcOn_0.AutoSize = True
        Me._optCalcOn_0.BackColor = System.Drawing.SystemColors.Control
        Me._optCalcOn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCalcOn_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCalcOn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCalcOn.SetIndex(Me._optCalcOn_0, CType(0, Short))
        Me._optCalcOn_0.Location = New System.Drawing.Point(564, 38)
        Me._optCalcOn_0.Name = "_optCalcOn_0"
        Me._optCalcOn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCalcOn_0.Size = New System.Drawing.Size(77, 18)
        Me._optCalcOn_0.TabIndex = 19
        Me._optCalcOn_0.TabStop = True
        Me._optCalcOn_0.Text = "Net Weight"
        Me._optCalcOn_0.UseVisualStyleBackColor = False
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboType.Location = New System.Drawing.Point(606, 10)
        Me.cboType.Name = "cboType"
        Me.cboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboType.Size = New System.Drawing.Size(161, 22)
        Me.cboType.TabIndex = 17
        '
        'chkAllCustomer
        '
        Me.chkAllCustomer.AutoSize = True
        Me.chkAllCustomer.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCustomer.Checked = True
        Me.chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCustomer.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCustomer.Location = New System.Drawing.Point(497, 12)
        Me.chkAllCustomer.Name = "chkAllCustomer"
        Me.chkAllCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCustomer.Size = New System.Drawing.Size(46, 18)
        Me.chkAllCustomer.TabIndex = 13
        Me.chkAllCustomer.Text = "ALL"
        Me.chkAllCustomer.UseVisualStyleBackColor = False
        '
        'txtFGName
        '
        Me.txtFGName.AcceptsReturn = True
        Me.txtFGName.BackColor = System.Drawing.SystemColors.Window
        Me.txtFGName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFGName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFGName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFGName.ForeColor = System.Drawing.Color.Blue
        Me.txtFGName.Location = New System.Drawing.Point(120, 34)
        Me.txtFGName.MaxLength = 0
        Me.txtFGName.Name = "txtFGName"
        Me.txtFGName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFGName.Size = New System.Drawing.Size(347, 19)
        Me.txtFGName.TabIndex = 8
        '
        'chkFG
        '
        Me.chkFG.AutoSize = True
        Me.chkFG.BackColor = System.Drawing.SystemColors.Control
        Me.chkFG.Checked = True
        Me.chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFG.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFG.Location = New System.Drawing.Point(498, 36)
        Me.chkFG.Name = "chkFG"
        Me.chkFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFG.Size = New System.Drawing.Size(38, 18)
        Me.chkFG.TabIndex = 7
        Me.chkFG.Text = "All"
        Me.chkFG.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 94)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1098, 480)
        Me.SprdMain.TabIndex = 6
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 102)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 22
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(544, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Type : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label4_1
        '
        Me._Label4_1.AutoSize = True
        Me._Label4_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label4_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label4_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label4_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.SetIndex(Me._Label4_1, CType(1, Short))
        Me._Label4_1.Location = New System.Drawing.Point(16, 12)
        Me._Label4_1.Name = "_Label4_1"
        Me._Label4_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label4_1.Size = New System.Drawing.Size(89, 14)
        Me._Label4_1.TabIndex = 16
        Me._Label4_1.Text = "Customer Name :"
        Me._Label4_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(25, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Finished Good :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.Frame5)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdShow)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblBookSubType)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 571)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(1106, 49)
        Me.Frame3.TabIndex = 0
        Me.Frame3.TabStop = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._optRate_0)
        Me.Frame5.Controls.Add(Me._optRate_1)
        Me.Frame5.Controls.Add(Me.txtRateAsOn)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 0)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(359, 49)
        Me.Frame5.TabIndex = 24
        Me.Frame5.TabStop = False
        '
        '_optRate_0
        '
        Me._optRate_0.AutoSize = True
        Me._optRate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optRate_0.Checked = True
        Me._optRate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRate_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRate.SetIndex(Me._optRate_0, CType(0, Short))
        Me._optRate_0.Location = New System.Drawing.Point(14, 18)
        Me._optRate_0.Name = "_optRate_0"
        Me._optRate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRate_0.Size = New System.Drawing.Size(81, 18)
        Me._optRate_0.TabIndex = 26
        Me._optRate_0.TabStop = True
        Me._optRate_0.Text = "Actual Rate"
        Me._optRate_0.UseVisualStyleBackColor = False
        '
        '_optRate_1
        '
        Me._optRate_1.AutoSize = True
        Me._optRate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optRate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optRate_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optRate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optRate.SetIndex(Me._optRate_1, CType(1, Short))
        Me._optRate_1.Location = New System.Drawing.Point(126, 18)
        Me._optRate_1.Name = "_optRate_1"
        Me._optRate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optRate_1.Size = New System.Drawing.Size(80, 18)
        Me._optRate_1.TabIndex = 25
        Me._optRate_1.TabStop = True
        Me._optRate_1.Text = "Rate As On"
        Me._optRate_1.UseVisualStyleBackColor = False
        '
        'txtRateAsOn
        '
        Me.txtRateAsOn.AllowPromptAsInput = False
        Me.txtRateAsOn.Enabled = False
        Me.txtRateAsOn.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRateAsOn.Location = New System.Drawing.Point(248, 18)
        Me.txtRateAsOn.Mask = "##/##/####"
        Me.txtRateAsOn.Name = "txtRateAsOn"
        Me.txtRateAsOn.Size = New System.Drawing.Size(83, 20)
        Me.txtRateAsOn.TabIndex = 27
        Me.txtRateAsOn.Visible = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(2, 10)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(107, 17)
        Me.lblBookType.TabIndex = 12
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBookType.Visible = False
        '
        'lblBookSubType
        '
        Me.lblBookSubType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookSubType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookSubType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookSubType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookSubType.Location = New System.Drawing.Point(2, 30)
        Me.lblBookSubType.Name = "lblBookSubType"
        Me.lblBookSubType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookSubType.Size = New System.Drawing.Size(107, 17)
        Me.lblBookSubType.TabIndex = 11
        Me.lblBookSubType.Text = "lblBookSubType"
        Me.lblBookSubType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBookSubType.Visible = False
        '
        'optRate
        '
        '
        'frmParamMaterialBudgetYearly
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamMaterialBudgetYearly"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Material Budget (Yearly)"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optBaseOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCalcOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class