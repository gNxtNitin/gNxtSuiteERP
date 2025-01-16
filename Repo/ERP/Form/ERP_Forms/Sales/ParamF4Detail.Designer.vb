Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamF4Detail
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
    Public WithEvents chkAgtPermission As System.Windows.Forms.CheckBox
    Public WithEvents _optStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optStatus_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents chkScrap As System.Windows.Forms.CheckBox
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents txtBillNo As System.Windows.Forms.TextBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents txtAsOn As System.Windows.Forms.MaskedTextBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents _optDate_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optDate_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents chkParty As System.Windows.Forms.CheckBox
    Public WithEvents cmdPartySearch As System.Windows.Forms.Button
    Public WithEvents txtPartyName As System.Windows.Forms.TextBox
    Public WithEvents txtPaint As System.Windows.Forms.TextBox
    Public WithEvents cmdPaintSearch As System.Windows.Forms.Button
    Public WithEvents chkPaintAll As System.Windows.Forms.CheckBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblMaterial As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtC4No As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents _optOrderBy_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblAcCode As System.Windows.Forms.Label
    Public WithEvents lblTrnType As System.Windows.Forms.Label
    Public WithEvents Lbl As VB6.LabelArray
    Public WithEvents optDate As VB6.RadioButtonArray
    Public WithEvents optOrderBy As VB6.RadioButtonArray
    Public WithEvents optShow As VB6.RadioButtonArray
    Public WithEvents optStatus As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamF4Detail))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtBillNo = New System.Windows.Forms.TextBox()
        Me.cmdPartySearch = New System.Windows.Forms.Button()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.txtPaint = New System.Windows.Forms.TextBox()
        Me.cmdPaintSearch = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtC4No = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.chkAgtPermission = New System.Windows.Forms.CheckBox()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._optStatus_0 = New System.Windows.Forms.RadioButton()
        Me._optStatus_2 = New System.Windows.Forms.RadioButton()
        Me._optStatus_1 = New System.Windows.Forms.RadioButton()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.chkScrap = New System.Windows.Forms.CheckBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtAsOn = New System.Windows.Forms.MaskedTextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._optDate_1 = New System.Windows.Forms.RadioButton()
        Me._optDate_0 = New System.Windows.Forms.RadioButton()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkParty = New System.Windows.Forms.CheckBox()
        Me.chkPaintAll = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMaterial = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optOrderBy_3 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_2 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._optOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.lblTrnType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optDate = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame8.SuspendLayout()
        Me.Frame9.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame1.SuspendLayout()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtBillNo
        '
        Me.txtBillNo.AcceptsReturn = True
        Me.txtBillNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBillNo.Location = New System.Drawing.Point(4, 14)
        Me.txtBillNo.MaxLength = 0
        Me.txtBillNo.Name = "txtBillNo"
        Me.txtBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillNo.Size = New System.Drawing.Size(71, 22)
        Me.txtBillNo.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.txtBillNo, "Press F1 For Help")
        '
        'cmdPartySearch
        '
        Me.cmdPartySearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPartySearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPartySearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPartySearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPartySearch.Image = CType(resources.GetObject("cmdPartySearch.Image"), System.Drawing.Image)
        Me.cmdPartySearch.Location = New System.Drawing.Point(398, 13)
        Me.cmdPartySearch.Name = "cmdPartySearch"
        Me.cmdPartySearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPartySearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdPartySearch.TabIndex = 19
        Me.cmdPartySearch.TabStop = False
        Me.cmdPartySearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPartySearch, "Search")
        Me.cmdPartySearch.UseVisualStyleBackColor = False
        '
        'txtPartyName
        '
        Me.txtPartyName.AcceptsReturn = True
        Me.txtPartyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartyName.Location = New System.Drawing.Point(78, 13)
        Me.txtPartyName.MaxLength = 0
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyName.Size = New System.Drawing.Size(318, 22)
        Me.txtPartyName.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.txtPartyName, "Press F1 For Help")
        '
        'txtPaint
        '
        Me.txtPaint.AcceptsReturn = True
        Me.txtPaint.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaint.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaint.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaint.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPaint.Location = New System.Drawing.Point(78, 43)
        Me.txtPaint.MaxLength = 0
        Me.txtPaint.Name = "txtPaint"
        Me.txtPaint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaint.Size = New System.Drawing.Size(318, 22)
        Me.txtPaint.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtPaint, "Press F1 For Help")
        '
        'cmdPaintSearch
        '
        Me.cmdPaintSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPaintSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPaintSearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPaintSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPaintSearch.Image = CType(resources.GetObject("cmdPaintSearch.Image"), System.Drawing.Image)
        Me.cmdPaintSearch.Location = New System.Drawing.Point(398, 43)
        Me.cmdPaintSearch.Name = "cmdPaintSearch"
        Me.cmdPaintSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPaintSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdPaintSearch.TabIndex = 14
        Me.cmdPaintSearch.TabStop = False
        Me.cmdPaintSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPaintSearch, "Search")
        Me.cmdPaintSearch.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(140, 12)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(29, 24)
        Me.cmdsearch.TabIndex = 34
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'TxtC4No
        '
        Me.TxtC4No.AcceptsReturn = True
        Me.TxtC4No.BackColor = System.Drawing.SystemColors.Window
        Me.TxtC4No.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtC4No.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtC4No.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtC4No.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtC4No.Location = New System.Drawing.Point(48, 13)
        Me.TxtC4No.MaxLength = 0
        Me.TxtC4No.Name = "TxtC4No"
        Me.TxtC4No.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtC4No.Size = New System.Drawing.Size(91, 22)
        Me.TxtC4No.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.TxtC4No, "Press F1 For Help")
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
        Me.CmdPreview.TabIndex = 3
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
        Me.cmdPrint.TabIndex = 2
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
        Me.cmdClose.TabIndex = 4
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
        Me.cmdShow.TabIndex = 1
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'chkAgtPermission
        '
        Me.chkAgtPermission.BackColor = System.Drawing.SystemColors.Control
        Me.chkAgtPermission.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAgtPermission.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAgtPermission.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAgtPermission.Location = New System.Drawing.Point(446, 566)
        Me.chkAgtPermission.Name = "chkAgtPermission"
        Me.chkAgtPermission.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAgtPermission.Size = New System.Drawing.Size(95, 41)
        Me.chkAgtPermission.TabIndex = 51
        Me.chkAgtPermission.Text = "Agt Permission"
        Me.chkAgtPermission.UseVisualStyleBackColor = False
        Me.chkAgtPermission.Visible = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me._optStatus_0)
        Me.Frame8.Controls.Add(Me._optStatus_2)
        Me.Frame8.Controls.Add(Me._optStatus_1)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(748, 0)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(150, 76)
        Me.Frame8.TabIndex = 44
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Status"
        '
        '_optStatus_0
        '
        Me._optStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_0.Checked = True
        Me._optStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_0, CType(0, Short))
        Me._optStatus_0.Location = New System.Drawing.Point(43, 14)
        Me._optStatus_0.Name = "_optStatus_0"
        Me._optStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_0.Size = New System.Drawing.Size(99, 16)
        Me._optStatus_0.TabIndex = 47
        Me._optStatus_0.TabStop = True
        Me._optStatus_0.Text = "All"
        Me._optStatus_0.UseVisualStyleBackColor = False
        '
        '_optStatus_2
        '
        Me._optStatus_2.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_2, CType(2, Short))
        Me._optStatus_2.Location = New System.Drawing.Point(43, 52)
        Me._optStatus_2.Name = "_optStatus_2"
        Me._optStatus_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_2.Size = New System.Drawing.Size(99, 16)
        Me._optStatus_2.TabIndex = 46
        Me._optStatus_2.TabStop = True
        Me._optStatus_2.Text = "Not Send"
        Me._optStatus_2.UseVisualStyleBackColor = False
        '
        '_optStatus_1
        '
        Me._optStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_1, CType(1, Short))
        Me._optStatus_1.Location = New System.Drawing.Point(43, 33)
        Me._optStatus_1.Name = "_optStatus_1"
        Me._optStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_1.Size = New System.Drawing.Size(99, 16)
        Me._optStatus_1.TabIndex = 45
        Me._optStatus_1.TabStop = True
        Me._optStatus_1.Text = "Send"
        Me._optStatus_1.UseVisualStyleBackColor = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.chkScrap)
        Me.Frame9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(748, 71)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(150, 41)
        Me.Frame9.TabIndex = 49
        Me.Frame9.TabStop = False
        '
        'chkScrap
        '
        Me.chkScrap.BackColor = System.Drawing.SystemColors.Control
        Me.chkScrap.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkScrap.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkScrap.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkScrap.Location = New System.Drawing.Point(44, 14)
        Me.chkScrap.Name = "chkScrap"
        Me.chkScrap.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkScrap.Size = New System.Drawing.Size(61, 17)
        Me.chkScrap.TabIndex = 50
        Me.chkScrap.Text = "Scrap"
        Me.chkScrap.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.txtBillNo)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(360, 566)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(83, 43)
        Me.Frame7.TabIndex = 42
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Bill No"
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtAsOn)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(141, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(104, 76)
        Me.Frame6.TabIndex = 40
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Pending As On "
        '
        'txtAsOn
        '
        Me.txtAsOn.AllowPromptAsInput = False
        Me.txtAsOn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAsOn.Location = New System.Drawing.Point(13, 30)
        Me.txtAsOn.Mask = "##/##/####"
        Me.txtAsOn.Name = "txtAsOn"
        Me.txtAsOn.Size = New System.Drawing.Size(85, 22)
        Me.txtAsOn.TabIndex = 41
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._optDate_1)
        Me.Frame2.Controls.Add(Me._optDate_0)
        Me.Frame2.Controls.Add(Me.txtDateFrom)
        Me.Frame2.Controls.Add(Me.txtDateTo)
        Me.Frame2.Controls.Add(Me._Lbl_0)
        Me.Frame2.Controls.Add(Me._Lbl_1)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(4, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(133, 112)
        Me.Frame2.TabIndex = 22
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Date"
        '
        '_optDate_1
        '
        Me._optDate_1.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_1.Checked = True
        Me._optDate_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_1, CType(1, Short))
        Me._optDate_1.Location = New System.Drawing.Point(2, 16)
        Me._optDate_1.Name = "_optDate_1"
        Me._optDate_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_1.Size = New System.Drawing.Size(69, 18)
        Me._optDate_1.TabIndex = 38
        Me._optDate_1.TabStop = True
        Me._optDate_1.Text = "F4 Date"
        Me._optDate_1.UseVisualStyleBackColor = False
        '
        '_optDate_0
        '
        Me._optDate_0.BackColor = System.Drawing.SystemColors.Control
        Me._optDate_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optDate_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optDate_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optDate.SetIndex(Me._optDate_0, CType(0, Short))
        Me._optDate_0.Location = New System.Drawing.Point(72, 16)
        Me._optDate_0.Name = "_optDate_0"
        Me._optDate_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optDate_0.Size = New System.Drawing.Size(59, 18)
        Me._optDate_0.TabIndex = 37
        Me._optDate_0.TabStop = True
        Me._optDate_0.Text = "VDate"
        Me._optDate_0.UseVisualStyleBackColor = False
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(46, 48)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(81, 22)
        Me.txtDateFrom.TabIndex = 23
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(46, 74)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(81, 22)
        Me.txtDateTo.TabIndex = 24
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(3, 50)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(40, 13)
        Me._Lbl_0.TabIndex = 26
        Me._Lbl_0.Text = "From :"
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(18, 76)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(25, 13)
        Me._Lbl_1.TabIndex = 25
        Me._Lbl_1.Text = "To :"
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkParty)
        Me.FraAccount.Controls.Add(Me.cmdPartySearch)
        Me.FraAccount.Controls.Add(Me.txtPartyName)
        Me.FraAccount.Controls.Add(Me.txtPaint)
        Me.FraAccount.Controls.Add(Me.cmdPaintSearch)
        Me.FraAccount.Controls.Add(Me.chkPaintAll)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.lblMaterial)
        Me.FraAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(249, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(496, 76)
        Me.FraAccount.TabIndex = 5
        Me.FraAccount.TabStop = False
        '
        'chkParty
        '
        Me.chkParty.BackColor = System.Drawing.SystemColors.Control
        Me.chkParty.Checked = True
        Me.chkParty.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkParty.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkParty.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkParty.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkParty.Location = New System.Drawing.Point(432, 17)
        Me.chkParty.Name = "chkParty"
        Me.chkParty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkParty.Size = New System.Drawing.Size(52, 16)
        Me.chkParty.TabIndex = 20
        Me.chkParty.Text = "ALL"
        Me.chkParty.UseVisualStyleBackColor = False
        '
        'chkPaintAll
        '
        Me.chkPaintAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkPaintAll.Checked = True
        Me.chkPaintAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPaintAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPaintAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPaintAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPaintAll.Location = New System.Drawing.Point(432, 47)
        Me.chkPaintAll.Name = "chkPaintAll"
        Me.chkPaintAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPaintAll.Size = New System.Drawing.Size(52, 16)
        Me.chkPaintAll.TabIndex = 13
        Me.chkPaintAll.Text = "ALL"
        Me.chkPaintAll.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Party Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMaterial
        '
        Me.lblMaterial.AutoSize = True
        Me.lblMaterial.BackColor = System.Drawing.SystemColors.Control
        Me.lblMaterial.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMaterial.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaterial.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaterial.Location = New System.Drawing.Point(40, 45)
        Me.lblMaterial.Name = "lblMaterial"
        Me.lblMaterial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMaterial.Size = New System.Drawing.Size(38, 13)
        Me.lblMaterial.TabIndex = 16
        Me.lblMaterial.Text = "Paint :"
        Me.lblMaterial.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optShow_1)
        Me.Frame3.Controls.Add(Me._optShow_0)
        Me.Frame3.Controls.Add(Me.cboShow)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(366, 71)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(380, 41)
        Me.Frame3.TabIndex = 28
        Me.Frame3.TabStop = False
        '
        '_optShow_1
        '
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(95, 14)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(81, 18)
        Me._optShow_1.TabIndex = 31
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "Summary"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        '_optShow_0
        '
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Checked = True
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(11, 14)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(65, 18)
        Me._optShow_0.TabIndex = 30
        Me._optShow_0.TabStop = True
        Me._optShow_0.Text = "Detail"
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(250, 13)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(123, 21)
        Me.cboShow.TabIndex = 29
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(205, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Show :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.chkAll)
        Me.Frame5.Controls.Add(Me.cmdsearch)
        Me.Frame5.Controls.Add(Me.TxtC4No)
        Me.Frame5.Controls.Add(Me.Label2)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(142, 71)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(221, 41)
        Me.Frame5.TabIndex = 32
        Me.Frame5.TabStop = False
        '
        'chkAll
        '
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(170, 16)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(48, 16)
        Me.chkAll.TabIndex = 35
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(2, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "C4 No :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(3, 106)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(896, 458)
        Me.Frame4.TabIndex = 6
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 9)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(892, 443)
        Me.SprdMain.TabIndex = 0
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 1
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optOrderBy_3)
        Me.Frame1.Controls.Add(Me._optOrderBy_2)
        Me.Frame1.Controls.Add(Me._optOrderBy_0)
        Me.Frame1.Controls.Add(Me._optOrderBy_1)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(-2, 566)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(358, 43)
        Me.Frame1.TabIndex = 10
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Order By"
        '
        '_optOrderBy_3
        '
        Me._optOrderBy_3.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_3, CType(3, Short))
        Me._optOrderBy_3.Location = New System.Drawing.Point(263, 18)
        Me._optOrderBy_3.Name = "_optOrderBy_3"
        Me._optOrderBy_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_3.Size = New System.Drawing.Size(87, 19)
        Me._optOrderBy_3.TabIndex = 48
        Me._optOrderBy_3.TabStop = True
        Me._optOrderBy_3.Text = "Item Name"
        Me._optOrderBy_3.UseVisualStyleBackColor = False
        '
        '_optOrderBy_2
        '
        Me._optOrderBy_2.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_2, CType(2, Short))
        Me._optOrderBy_2.Location = New System.Drawing.Point(156, 18)
        Me._optOrderBy_2.Name = "_optOrderBy_2"
        Me._optOrderBy_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_2.Size = New System.Drawing.Size(91, 19)
        Me._optOrderBy_2.TabIndex = 27
        Me._optOrderBy_2.TabStop = True
        Me._optOrderBy_2.Text = "Party Name"
        Me._optOrderBy_2.UseVisualStyleBackColor = False
        '
        '_optOrderBy_0
        '
        Me._optOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_0.Checked = True
        Me._optOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_0, CType(0, Short))
        Me._optOrderBy_0.Location = New System.Drawing.Point(6, 18)
        Me._optOrderBy_0.Name = "_optOrderBy_0"
        Me._optOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_0.Size = New System.Drawing.Size(45, 19)
        Me._optOrderBy_0.TabIndex = 12
        Me._optOrderBy_0.TabStop = True
        Me._optOrderBy_0.Text = "C4"
        Me._optOrderBy_0.UseVisualStyleBackColor = False
        '
        '_optOrderBy_1
        '
        Me._optOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrderBy_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrderBy.SetIndex(Me._optOrderBy_1, CType(1, Short))
        Me._optOrderBy_1.Location = New System.Drawing.Point(67, 18)
        Me._optOrderBy_1.Name = "_optOrderBy_1"
        Me._optOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrderBy_1.Size = New System.Drawing.Size(73, 19)
        Me._optOrderBy_1.TabIndex = 11
        Me._optOrderBy_1.TabStop = True
        Me._optOrderBy_1.Text = "Bill Wise"
        Me._optOrderBy_1.UseVisualStyleBackColor = False
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
        Me.FraMovement.Location = New System.Drawing.Point(652, 562)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 49)
        Me.FraMovement.TabIndex = 7
        Me.FraMovement.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(548, 596)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(75, 13)
        Me.lblBookType.TabIndex = 17
        Me.lblBookType.Text = "lblBookType"
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(538, 566)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(87, 13)
        Me.lblAcCode.TabIndex = 9
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
        Me.lblTrnType.Location = New System.Drawing.Point(536, 580)
        Me.lblTrnType.Name = "lblTrnType"
        Me.lblTrnType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnType.Size = New System.Drawing.Size(59, 13)
        Me.lblTrnType.TabIndex = 8
        Me.lblTrnType.Text = "lblTrnType"
        Me.lblTrnType.Visible = False
        '
        'optOrderBy
        '
        '
        'frmParamF4Detail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.chkAgtPermission)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame9)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.lblBookType)
        Me.Controls.Add(Me.lblAcCode)
        Me.Controls.Add(Me.lblTrnType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamF4Detail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "C4 Details Received"
        Me.Frame8.ResumeLayout(False)
        Me.Frame9.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame1.ResumeLayout(False)
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optDate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).EndInit()
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
#End Region
End Class