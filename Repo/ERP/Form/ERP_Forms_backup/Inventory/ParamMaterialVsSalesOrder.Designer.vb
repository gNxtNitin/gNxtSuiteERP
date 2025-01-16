Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamMaterialVsSalesOrder
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
    Public WithEvents chkSOAll As System.Windows.Forms.CheckBox
    Public WithEvents txtSalesOrder As System.Windows.Forms.TextBox
    Public WithEvents cmdSaleOrder As System.Windows.Forms.Button
    Public WithEvents UpDYear As System.Windows.Forms.Label
    Public WithEvents lblRunDate As System.Windows.Forms.Label
    Public WithEvents lblYear As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _optBaseOn_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optBaseOn_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _optCalcOn_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optCalcOn_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchCustName As System.Windows.Forms.Button
    Public WithEvents chkAllCustomer As System.Windows.Forms.CheckBox
    Public WithEvents cmdSearchFG As System.Windows.Forms.Button
    Public WithEvents txtFGName As System.Windows.Forms.TextBox
    Public WithEvents chkFG As System.Windows.Forms.CheckBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents _Label4_1 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents chkQCStockType As System.Windows.Forms.CheckBox
    Public WithEvents chkCRStockType As System.Windows.Forms.CheckBox
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
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
    Public WithEvents Label4 As VB6.LabelArray
    Public WithEvents optBaseOn As VB6.RadioButtonArray
    Public WithEvents optCalcOn As VB6.RadioButtonArray
    Public WithEvents optShow As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamMaterialVsSalesOrder))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSaleOrder = New System.Windows.Forms.Button()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.cmdsearchCustName = New System.Windows.Forms.Button()
        Me.cmdSearchFG = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.chkSOAll = New System.Windows.Forms.CheckBox()
        Me.txtSalesOrder = New System.Windows.Forms.TextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.UpDYear = New System.Windows.Forms.Label()
        Me.lblRunDate = New System.Windows.Forms.Label()
        Me.lblYear = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optBaseOn_1 = New System.Windows.Forms.RadioButton()
        Me._optBaseOn_0 = New System.Windows.Forms.RadioButton()
        Me._optCalcOn_1 = New System.Windows.Forms.RadioButton()
        Me._optCalcOn_0 = New System.Windows.Forms.RadioButton()
        Me.chkAllCustomer = New System.Windows.Forms.CheckBox()
        Me.txtFGName = New System.Windows.Forms.TextBox()
        Me.chkFG = New System.Windows.Forms.CheckBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Label3 = New System.Windows.Forms.Label()
        Me._Label4_1 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.chkQCStockType = New System.Windows.Forms.CheckBox()
        Me.chkCRStockType = New System.Windows.Forms.CheckBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblBookSubType = New System.Windows.Forms.Label()
        Me.CommonDialog1Open = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog1Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog1Font = New System.Windows.Forms.FontDialog()
        Me.CommonDialog1Color = New System.Windows.Forms.ColorDialog()
        Me.CommonDialog1Print = New System.Windows.Forms.PrintDialog()
        Me.Label4 = New VB6.LabelArray(Me.components)
        Me.optBaseOn = New VB6.RadioButtonArray(Me.components)
        Me.optCalcOn = New VB6.RadioButtonArray(Me.components)
        Me.optShow = New VB6.RadioButtonArray(Me.components)
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optBaseOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optCalcOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSaleOrder
        '
        Me.cmdSaleOrder.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSaleOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSaleOrder.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaleOrder.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSaleOrder.Image = CType(resources.GetObject("cmdSaleOrder.Image"), System.Drawing.Image)
        Me.cmdSaleOrder.Location = New System.Drawing.Point(446, 58)
        Me.cmdSaleOrder.Name = "cmdSaleOrder"
        Me.cmdSaleOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSaleOrder.Size = New System.Drawing.Size(29, 19)
        Me.cmdSaleOrder.TabIndex = 26
        Me.cmdSaleOrder.TabStop = False
        Me.cmdSaleOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSaleOrder, "Search")
        Me.cmdSaleOrder.UseVisualStyleBackColor = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AcceptsReturn = True
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerName.Location = New System.Drawing.Point(340, 10)
        Me.txtCustomerName.MaxLength = 0
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerName.Size = New System.Drawing.Size(345, 19)
        Me.txtCustomerName.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtCustomerName, "Press F1 For Help")
        '
        'cmdsearchCustName
        '
        Me.cmdsearchCustName.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchCustName.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchCustName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchCustName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchCustName.Image = CType(resources.GetObject("cmdsearchCustName.Image"), System.Drawing.Image)
        Me.cmdsearchCustName.Location = New System.Drawing.Point(685, 10)
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
        Me.cmdSearchFG.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchFG.Image = CType(resources.GetObject("cmdSearchFG.Image"), System.Drawing.Image)
        Me.cmdSearchFG.Location = New System.Drawing.Point(686, 34)
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
        Me.Frame4.Controls.Add(Me.chkSOAll)
        Me.Frame4.Controls.Add(Me.txtSalesOrder)
        Me.Frame4.Controls.Add(Me.cmdSaleOrder)
        Me.Frame4.Controls.Add(Me.Frame2)
        Me.Frame4.Controls.Add(Me.Frame1)
        Me.Frame4.Controls.Add(Me._optCalcOn_1)
        Me.Frame4.Controls.Add(Me._optCalcOn_0)
        Me.Frame4.Controls.Add(Me.txtCustomerName)
        Me.Frame4.Controls.Add(Me.cmdsearchCustName)
        Me.Frame4.Controls.Add(Me.chkAllCustomer)
        Me.Frame4.Controls.Add(Me.cmdSearchFG)
        Me.Frame4.Controls.Add(Me.txtFGName)
        Me.Frame4.Controls.Add(Me.chkFG)
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Controls.Add(Me.Label3)
        Me.Frame4.Controls.Add(Me._Label4_1)
        Me.Frame4.Controls.Add(Me.Label1)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, -2)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(769, 429)
        Me.Frame4.TabIndex = 5
        Me.Frame4.TabStop = False
        '
        'chkSOAll
        '
        Me.chkSOAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkSOAll.Checked = True
        Me.chkSOAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSOAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkSOAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkSOAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkSOAll.Location = New System.Drawing.Point(476, 60)
        Me.chkSOAll.Name = "chkSOAll"
        Me.chkSOAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkSOAll.Size = New System.Drawing.Size(41, 18)
        Me.chkSOAll.TabIndex = 28
        Me.chkSOAll.Text = "All"
        Me.chkSOAll.UseVisualStyleBackColor = False
        '
        'txtSalesOrder
        '
        Me.txtSalesOrder.AcceptsReturn = True
        Me.txtSalesOrder.BackColor = System.Drawing.SystemColors.Window
        Me.txtSalesOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSalesOrder.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSalesOrder.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSalesOrder.ForeColor = System.Drawing.Color.Blue
        Me.txtSalesOrder.Location = New System.Drawing.Point(340, 58)
        Me.txtSalesOrder.MaxLength = 0
        Me.txtSalesOrder.Name = "txtSalesOrder"
        Me.txtSalesOrder.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSalesOrder.Size = New System.Drawing.Size(105, 19)
        Me.txtSalesOrder.TabIndex = 27
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.UpDYear)
        Me.Frame2.Controls.Add(Me.lblRunDate)
        Me.Frame2.Controls.Add(Me.lblYear)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, -2)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(215, 47)
        Me.Frame2.TabIndex = 22
        Me.Frame2.TabStop = False
        '
        'UpDYear
        '
        Me.UpDYear.BackColor = System.Drawing.Color.Red
        Me.UpDYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.UpDYear.Location = New System.Drawing.Point(194, 14)
        Me.UpDYear.Name = "UpDYear"
        Me.UpDYear.Size = New System.Drawing.Size(16, 28)
        Me.UpDYear.TabIndex = 23
        Me.UpDYear.Text = "UpDYear"
        '
        'lblRunDate
        '
        Me.lblRunDate.AutoSize = True
        Me.lblRunDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblRunDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblRunDate.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRunDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRunDate.Location = New System.Drawing.Point(10, 14)
        Me.lblRunDate.Name = "lblRunDate"
        Me.lblRunDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblRunDate.Size = New System.Drawing.Size(48, 14)
        Me.lblRunDate.TabIndex = 25
        Me.lblRunDate.Text = "RunDate"
        Me.lblRunDate.Visible = False
        '
        'lblYear
        '
        Me.lblYear.AutoSize = True
        Me.lblYear.BackColor = System.Drawing.SystemColors.Control
        Me.lblYear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblYear.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblYear.Location = New System.Drawing.Point(184, 16)
        Me.lblYear.Name = "lblYear"
        Me.lblYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblYear.Size = New System.Drawing.Size(2, 20)
        Me.lblYear.TabIndex = 24
        Me.lblYear.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optBaseOn_1)
        Me.Frame1.Controls.Add(Me._optBaseOn_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 44)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(215, 39)
        Me.Frame1.TabIndex = 19
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "PO Type"
        '
        '_optBaseOn_1
        '
        Me._optBaseOn_1.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_1, CType(1, Short))
        Me._optBaseOn_1.Location = New System.Drawing.Point(122, 16)
        Me._optBaseOn_1.Name = "_optBaseOn_1"
        Me._optBaseOn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_1.Size = New System.Drawing.Size(89, 17)
        Me._optBaseOn_1.TabIndex = 21
        Me._optBaseOn_1.TabStop = True
        Me._optBaseOn_1.Text = "Closed"
        Me._optBaseOn_1.UseVisualStyleBackColor = False
        '
        '_optBaseOn_0
        '
        Me._optBaseOn_0.BackColor = System.Drawing.SystemColors.Control
        Me._optBaseOn_0.Checked = True
        Me._optBaseOn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optBaseOn_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optBaseOn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBaseOn.SetIndex(Me._optBaseOn_0, CType(0, Short))
        Me._optBaseOn_0.Location = New System.Drawing.Point(20, 16)
        Me._optBaseOn_0.Name = "_optBaseOn_0"
        Me._optBaseOn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optBaseOn_0.Size = New System.Drawing.Size(73, 17)
        Me._optBaseOn_0.TabIndex = 20
        Me._optBaseOn_0.TabStop = True
        Me._optBaseOn_0.Text = "Open"
        Me._optBaseOn_0.UseVisualStyleBackColor = False
        '
        '_optCalcOn_1
        '
        Me._optCalcOn_1.BackColor = System.Drawing.SystemColors.Control
        Me._optCalcOn_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCalcOn_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCalcOn_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCalcOn.SetIndex(Me._optCalcOn_1, CType(1, Short))
        Me._optCalcOn_1.Location = New System.Drawing.Point(650, 60)
        Me._optCalcOn_1.Name = "_optCalcOn_1"
        Me._optCalcOn_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCalcOn_1.Size = New System.Drawing.Size(103, 17)
        Me._optCalcOn_1.TabIndex = 18
        Me._optCalcOn_1.TabStop = True
        Me._optCalcOn_1.Text = "Gross Weight"
        Me._optCalcOn_1.UseVisualStyleBackColor = False
        '
        '_optCalcOn_0
        '
        Me._optCalcOn_0.BackColor = System.Drawing.SystemColors.Control
        Me._optCalcOn_0.Checked = True
        Me._optCalcOn_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optCalcOn_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optCalcOn_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCalcOn.SetIndex(Me._optCalcOn_0, CType(0, Short))
        Me._optCalcOn_0.Location = New System.Drawing.Point(552, 60)
        Me._optCalcOn_0.Name = "_optCalcOn_0"
        Me._optCalcOn_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optCalcOn_0.Size = New System.Drawing.Size(87, 17)
        Me._optCalcOn_0.TabIndex = 17
        Me._optCalcOn_0.TabStop = True
        Me._optCalcOn_0.Text = "Net Weight"
        Me._optCalcOn_0.UseVisualStyleBackColor = False
        '
        'chkAllCustomer
        '
        Me.chkAllCustomer.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllCustomer.Checked = True
        Me.chkAllCustomer.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllCustomer.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllCustomer.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllCustomer.Location = New System.Drawing.Point(715, 12)
        Me.chkAllCustomer.Name = "chkAllCustomer"
        Me.chkAllCustomer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllCustomer.Size = New System.Drawing.Size(49, 18)
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
        Me.txtFGName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFGName.ForeColor = System.Drawing.Color.Blue
        Me.txtFGName.Location = New System.Drawing.Point(340, 34)
        Me.txtFGName.MaxLength = 0
        Me.txtFGName.Name = "txtFGName"
        Me.txtFGName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFGName.Size = New System.Drawing.Size(345, 19)
        Me.txtFGName.TabIndex = 8
        '
        'chkFG
        '
        Me.chkFG.BackColor = System.Drawing.SystemColors.Control
        Me.chkFG.Checked = True
        Me.chkFG.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFG.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkFG.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFG.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkFG.Location = New System.Drawing.Point(716, 36)
        Me.chkFG.Name = "chkFG"
        Me.chkFG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkFG.Size = New System.Drawing.Size(43, 18)
        Me.chkFG.TabIndex = 7
        Me.chkFG.Text = "All"
        Me.chkFG.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 84)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(764, 341)
        Me.SprdMain.TabIndex = 6
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 102)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(253, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(71, 14)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Sales Order :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label4_1
        '
        Me._Label4_1.AutoSize = True
        Me._Label4_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label4_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label4_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label4_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.SetIndex(Me._Label4_1, CType(1, Short))
        Me._Label4_1.Location = New System.Drawing.Point(238, 12)
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
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(238, 36)
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
        Me.Frame3.Controls.Add(Me.chkQCStockType)
        Me.Frame3.Controls.Add(Me.chkCRStockType)
        Me.Frame3.Controls.Add(Me.Frame5)
        Me.Frame3.Controls.Add(Me.cmdClose)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdShow)
        Me.Frame3.Controls.Add(Me.lblBookType)
        Me.Frame3.Controls.Add(Me.lblBookSubType)
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
        'chkQCStockType
        '
        Me.chkQCStockType.BackColor = System.Drawing.SystemColors.Control
        Me.chkQCStockType.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkQCStockType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkQCStockType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkQCStockType.Location = New System.Drawing.Point(220, 14)
        Me.chkQCStockType.Name = "chkQCStockType"
        Me.chkQCStockType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkQCStockType.Size = New System.Drawing.Size(179, 18)
        Me.chkQCStockType.TabIndex = 34
        Me.chkQCStockType.Text = "Include QC Stock Type"
        Me.chkQCStockType.UseVisualStyleBackColor = False
        Me.chkQCStockType.Visible = False
        '
        'chkCRStockType
        '
        Me.chkCRStockType.BackColor = System.Drawing.SystemColors.Control
        Me.chkCRStockType.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkCRStockType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCRStockType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkCRStockType.Location = New System.Drawing.Point(220, 32)
        Me.chkCRStockType.Name = "chkCRStockType"
        Me.chkCRStockType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkCRStockType.Size = New System.Drawing.Size(179, 18)
        Me.chkCRStockType.TabIndex = 33
        Me.chkCRStockType.Text = "Include CR Stock Type"
        Me.chkCRStockType.UseVisualStyleBackColor = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._optShow_0)
        Me.Frame5.Controls.Add(Me._optShow_1)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(2, 10)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(215, 39)
        Me.Frame5.TabIndex = 30
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Show"
        '
        '_optShow_0
        '
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Checked = True
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(20, 16)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(73, 18)
        Me._optShow_0.TabIndex = 32
        Me._optShow_0.TabStop = True
        Me._optShow_0.Text = "Detail"
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        '_optShow_1
        '
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(122, 16)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(89, 18)
        Me._optShow_1.TabIndex = 31
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "Summary"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(370, 10)
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
        Me.lblBookSubType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookSubType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookSubType.Location = New System.Drawing.Point(378, 30)
        Me.lblBookSubType.Name = "lblBookSubType"
        Me.lblBookSubType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookSubType.Size = New System.Drawing.Size(107, 17)
        Me.lblBookSubType.TabIndex = 11
        Me.lblBookSubType.Text = "lblBookSubType"
        Me.lblBookSubType.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblBookSubType.Visible = False
        '
        'frmParamMaterialVsSalesOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(769, 471)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 16)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamMaterialVsSalesOrder"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Sales Order Vs Material Stock"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        CType(Me.Label4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optBaseOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optCalcOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class