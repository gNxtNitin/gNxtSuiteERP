Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamF4DetailOutward
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
    Public WithEvents lstPurpose As System.Windows.Forms.CheckedListBox
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents txtAsOn As System.Windows.Forms.MaskedTextBox
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents chkList As System.Windows.Forms.CheckBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents chkEmp As System.Windows.Forms.CheckBox
    Public WithEvents cmdEmp As System.Windows.Forms.Button
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents chkPrepareBy As System.Windows.Forms.CheckBox
    Public WithEvents cmdPrepareBy As System.Windows.Forms.Button
    Public WithEvents txtPrepareBy As System.Windows.Forms.TextBox
    Public WithEvents ChkPartyAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdPartySearch As System.Windows.Forms.Button
    Public WithEvents txtPartyName As System.Windows.Forms.TextBox
    Public WithEvents txtItemDesc As System.Windows.Forms.TextBox
    Public WithEvents cmdItemDesc As System.Windows.Forms.Button
    Public WithEvents chkItemAll As System.Windows.Forms.CheckBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblMaterial As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents _optShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents chkValue As System.Windows.Forms.CheckBox
    Public WithEvents cboShow As System.Windows.Forms.ComboBox
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _OptShowNo_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShowNo_0 As System.Windows.Forms.RadioButton
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtC4No As System.Windows.Forms.TextBox
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents lstCategory As System.Windows.Forms.CheckedListBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents _optOrder_3 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrder_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrder_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optOrder_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptWise_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptWise_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptWise_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame9 As System.Windows.Forms.GroupBox
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Lbl As VB6.LabelArray
    Public WithEvents OptShowNo As VB6.RadioButtonArray
    Public WithEvents OptWise As VB6.RadioButtonArray
    Public WithEvents optOrder As VB6.RadioButtonArray
    Public WithEvents optShow As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamF4DetailOutward))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdEmp = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.cmdPrepareBy = New System.Windows.Forms.Button()
        Me.txtPrepareBy = New System.Windows.Forms.TextBox()
        Me.cmdPartySearch = New System.Windows.Forms.Button()
        Me.txtPartyName = New System.Windows.Forms.TextBox()
        Me.txtItemDesc = New System.Windows.Forms.TextBox()
        Me.cmdItemDesc = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtC4No = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me.lstPurpose = New System.Windows.Forms.CheckedListBox()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtAsOn = New System.Windows.Forms.MaskedTextBox()
        Me.chkList = New System.Windows.Forms.CheckBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkEmp = New System.Windows.Forms.CheckBox()
        Me.chkPrepareBy = New System.Windows.Forms.CheckBox()
        Me.ChkPartyAll = New System.Windows.Forms.CheckBox()
        Me.chkItemAll = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMaterial = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optShow_2 = New System.Windows.Forms.RadioButton()
        Me.chkValue = New System.Windows.Forms.CheckBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._OptShowNo_1 = New System.Windows.Forms.RadioButton()
        Me._OptShowNo_0 = New System.Windows.Forms.RadioButton()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.lstCategory = New System.Windows.Forms.CheckedListBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._optOrder_3 = New System.Windows.Forms.RadioButton()
        Me._optOrder_2 = New System.Windows.Forms.RadioButton()
        Me._optOrder_1 = New System.Windows.Forms.RadioButton()
        Me._optOrder_0 = New System.Windows.Forms.RadioButton()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me._OptWise_2 = New System.Windows.Forms.RadioButton()
        Me._OptWise_0 = New System.Windows.Forms.RadioButton()
        Me._OptWise_1 = New System.Windows.Forms.RadioButton()
        Me.Frame9 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptShowNo = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptWise = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optOrder = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame10.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame8.SuspendLayout()
        Me.Frame9.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShowNo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptWise, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optOrder, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdEmp
        '
        Me.cmdEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEmp.Enabled = False
        Me.cmdEmp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEmp.Image = CType(resources.GetObject("cmdEmp.Image"), System.Drawing.Image)
        Me.cmdEmp.Location = New System.Drawing.Point(426, 103)
        Me.cmdEmp.Name = "cmdEmp"
        Me.cmdEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEmp.Size = New System.Drawing.Size(29, 22)
        Me.cmdEmp.TabIndex = 55
        Me.cmdEmp.TabStop = False
        Me.cmdEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdEmp, "Search")
        Me.cmdEmp.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Enabled = False
        Me.txtName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(126, 103)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(298, 22)
        Me.txtName.TabIndex = 54
        Me.ToolTip1.SetToolTip(Me.txtName, "Press F1 For Help")
        '
        'cmdPrepareBy
        '
        Me.cmdPrepareBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPrepareBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrepareBy.Enabled = False
        Me.cmdPrepareBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrepareBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrepareBy.Image = CType(resources.GetObject("cmdPrepareBy.Image"), System.Drawing.Image)
        Me.cmdPrepareBy.Location = New System.Drawing.Point(426, 73)
        Me.cmdPrepareBy.Name = "cmdPrepareBy"
        Me.cmdPrepareBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrepareBy.Size = New System.Drawing.Size(29, 22)
        Me.cmdPrepareBy.TabIndex = 51
        Me.cmdPrepareBy.TabStop = False
        Me.cmdPrepareBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrepareBy, "Search")
        Me.cmdPrepareBy.UseVisualStyleBackColor = False
        '
        'txtPrepareBy
        '
        Me.txtPrepareBy.AcceptsReturn = True
        Me.txtPrepareBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrepareBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrepareBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrepareBy.Enabled = False
        Me.txtPrepareBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrepareBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrepareBy.Location = New System.Drawing.Point(126, 73)
        Me.txtPrepareBy.MaxLength = 0
        Me.txtPrepareBy.Name = "txtPrepareBy"
        Me.txtPrepareBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrepareBy.Size = New System.Drawing.Size(298, 22)
        Me.txtPrepareBy.TabIndex = 50
        Me.ToolTip1.SetToolTip(Me.txtPrepareBy, "Press F1 For Help")
        '
        'cmdPartySearch
        '
        Me.cmdPartySearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdPartySearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPartySearch.Enabled = False
        Me.cmdPartySearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPartySearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPartySearch.Image = CType(resources.GetObject("cmdPartySearch.Image"), System.Drawing.Image)
        Me.cmdPartySearch.Location = New System.Drawing.Point(426, 10)
        Me.cmdPartySearch.Name = "cmdPartySearch"
        Me.cmdPartySearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPartySearch.Size = New System.Drawing.Size(29, 22)
        Me.cmdPartySearch.TabIndex = 13
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
        Me.txtPartyName.Enabled = False
        Me.txtPartyName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPartyName.Location = New System.Drawing.Point(126, 10)
        Me.txtPartyName.MaxLength = 0
        Me.txtPartyName.Name = "txtPartyName"
        Me.txtPartyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartyName.Size = New System.Drawing.Size(298, 22)
        Me.txtPartyName.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.txtPartyName, "Press F1 For Help")
        '
        'txtItemDesc
        '
        Me.txtItemDesc.AcceptsReturn = True
        Me.txtItemDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemDesc.Enabled = False
        Me.txtItemDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtItemDesc.Location = New System.Drawing.Point(126, 43)
        Me.txtItemDesc.MaxLength = 0
        Me.txtItemDesc.Name = "txtItemDesc"
        Me.txtItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemDesc.Size = New System.Drawing.Size(298, 22)
        Me.txtItemDesc.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtItemDesc, "Press F1 For Help")
        '
        'cmdItemDesc
        '
        Me.cmdItemDesc.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdItemDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdItemDesc.Enabled = False
        Me.cmdItemDesc.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdItemDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdItemDesc.Image = CType(resources.GetObject("cmdItemDesc.Image"), System.Drawing.Image)
        Me.cmdItemDesc.Location = New System.Drawing.Point(426, 43)
        Me.cmdItemDesc.Name = "cmdItemDesc"
        Me.cmdItemDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdItemDesc.Size = New System.Drawing.Size(29, 22)
        Me.cmdItemDesc.TabIndex = 9
        Me.cmdItemDesc.TabStop = False
        Me.cmdItemDesc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdItemDesc, "Search")
        Me.cmdItemDesc.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(255, 21)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(34, 24)
        Me.cmdsearch.TabIndex = 35
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
        Me.TxtC4No.Location = New System.Drawing.Point(123, 22)
        Me.TxtC4No.MaxLength = 0
        Me.TxtC4No.Name = "TxtC4No"
        Me.TxtC4No.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtC4No.Size = New System.Drawing.Size(129, 22)
        Me.TxtC4No.TabIndex = 34
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
        Me.CmdPreview.Location = New System.Drawing.Point(123, 12)
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
        Me.cmdPrint.Location = New System.Drawing.Point(63, 12)
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
        Me.cmdClose.Location = New System.Drawing.Point(184, 12)
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
        Me.cmdShow.Location = New System.Drawing.Point(4, 12)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 37)
        Me.cmdShow.TabIndex = 1
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me.lstPurpose)
        Me.Frame10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(200, 58)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(179, 82)
        Me.Frame10.TabIndex = 59
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Purpose"
        '
        'lstPurpose
        '
        Me.lstPurpose.BackColor = System.Drawing.SystemColors.Window
        Me.lstPurpose.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstPurpose.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstPurpose.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstPurpose.IntegralHeight = False
        Me.lstPurpose.Items.AddRange(New Object() {"lstPurpose"})
        Me.lstPurpose.Location = New System.Drawing.Point(2, 14)
        Me.lstPurpose.Name = "lstPurpose"
        Me.lstPurpose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstPurpose.Size = New System.Drawing.Size(172, 62)
        Me.lstPurpose.TabIndex = 60
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtAsOn)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(257, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(122, 58)
        Me.Frame6.TabIndex = 37
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Pending As On"
        '
        'txtAsOn
        '
        Me.txtAsOn.AllowPromptAsInput = False
        Me.txtAsOn.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAsOn.Location = New System.Drawing.Point(13, 23)
        Me.txtAsOn.Mask = "##/##/####"
        Me.txtAsOn.Name = "txtAsOn"
        Me.txtAsOn.Size = New System.Drawing.Size(79, 22)
        Me.txtAsOn.TabIndex = 38
        '
        'chkList
        '
        Me.chkList.BackColor = System.Drawing.SystemColors.Control
        Me.chkList.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkList.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkList.Location = New System.Drawing.Point(792, 146)
        Me.chkList.Name = "chkList"
        Me.chkList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkList.Size = New System.Drawing.Size(104, 46)
        Me.chkList.TabIndex = 30
        Me.chkList.Text = "Report in List Format"
        Me.chkList.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtDateFrom)
        Me.Frame2.Controls.Add(Me.txtDateTo)
        Me.Frame2.Controls.Add(Me._Lbl_1)
        Me.Frame2.Controls.Add(Me._Lbl_0)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 0)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(254, 58)
        Me.Frame2.TabIndex = 16
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(49, 23)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(79, 22)
        Me.txtDateFrom.TabIndex = 17
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(168, 23)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(79, 22)
        Me.txtDateTo.TabIndex = 22
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(138, 29)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(25, 13)
        Me._Lbl_1.TabIndex = 23
        Me._Lbl_1.Text = "To :"
        Me._Lbl_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(6, 27)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(40, 13)
        Me._Lbl_0.TabIndex = 18
        Me._Lbl_0.Text = "From :"
        Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.chkEmp)
        Me.FraAccount.Controls.Add(Me.cmdEmp)
        Me.FraAccount.Controls.Add(Me.txtName)
        Me.FraAccount.Controls.Add(Me.chkPrepareBy)
        Me.FraAccount.Controls.Add(Me.cmdPrepareBy)
        Me.FraAccount.Controls.Add(Me.txtPrepareBy)
        Me.FraAccount.Controls.Add(Me.ChkPartyAll)
        Me.FraAccount.Controls.Add(Me.cmdPartySearch)
        Me.FraAccount.Controls.Add(Me.txtPartyName)
        Me.FraAccount.Controls.Add(Me.txtItemDesc)
        Me.FraAccount.Controls.Add(Me.cmdItemDesc)
        Me.FraAccount.Controls.Add(Me.chkItemAll)
        Me.FraAccount.Controls.Add(Me.Label4)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.lblMaterial)
        Me.FraAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(382, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(516, 142)
        Me.FraAccount.TabIndex = 5
        Me.FraAccount.TabStop = False
        '
        'chkEmp
        '
        Me.chkEmp.BackColor = System.Drawing.SystemColors.Control
        Me.chkEmp.Checked = True
        Me.chkEmp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkEmp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkEmp.Location = New System.Drawing.Point(458, 105)
        Me.chkEmp.Name = "chkEmp"
        Me.chkEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkEmp.Size = New System.Drawing.Size(48, 18)
        Me.chkEmp.TabIndex = 56
        Me.chkEmp.Text = "ALL"
        Me.chkEmp.UseVisualStyleBackColor = False
        '
        'chkPrepareBy
        '
        Me.chkPrepareBy.BackColor = System.Drawing.SystemColors.Control
        Me.chkPrepareBy.Checked = True
        Me.chkPrepareBy.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPrepareBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPrepareBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPrepareBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPrepareBy.Location = New System.Drawing.Point(458, 75)
        Me.chkPrepareBy.Name = "chkPrepareBy"
        Me.chkPrepareBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPrepareBy.Size = New System.Drawing.Size(48, 18)
        Me.chkPrepareBy.TabIndex = 52
        Me.chkPrepareBy.Text = "ALL"
        Me.chkPrepareBy.UseVisualStyleBackColor = False
        '
        'ChkPartyAll
        '
        Me.ChkPartyAll.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPartyAll.Checked = True
        Me.ChkPartyAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkPartyAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPartyAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPartyAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPartyAll.Location = New System.Drawing.Point(458, 12)
        Me.ChkPartyAll.Name = "ChkPartyAll"
        Me.ChkPartyAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPartyAll.Size = New System.Drawing.Size(48, 18)
        Me.ChkPartyAll.TabIndex = 14
        Me.ChkPartyAll.Text = "ALL"
        Me.ChkPartyAll.UseVisualStyleBackColor = False
        '
        'chkItemAll
        '
        Me.chkItemAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkItemAll.Checked = True
        Me.chkItemAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkItemAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkItemAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkItemAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkItemAll.Location = New System.Drawing.Point(458, 45)
        Me.chkItemAll.Name = "chkItemAll"
        Me.chkItemAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkItemAll.Size = New System.Drawing.Size(48, 18)
        Me.chkItemAll.TabIndex = 8
        Me.chkItemAll.Text = "ALL"
        Me.chkItemAll.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(22, 106)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(104, 13)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Responsibile Emp :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(13, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Prepare By (User-ID):"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(18, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(108, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Party Name :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMaterial
        '
        Me.lblMaterial.BackColor = System.Drawing.SystemColors.Control
        Me.lblMaterial.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMaterial.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMaterial.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMaterial.Location = New System.Drawing.Point(26, 46)
        Me.lblMaterial.Name = "lblMaterial"
        Me.lblMaterial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMaterial.Size = New System.Drawing.Size(100, 13)
        Me.lblMaterial.TabIndex = 11
        Me.lblMaterial.Text = "Item Desc :"
        Me.lblMaterial.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optShow_2)
        Me.Frame1.Controls.Add(Me.chkValue)
        Me.Frame1.Controls.Add(Me.cboShow)
        Me.Frame1.Controls.Add(Me._optShow_0)
        Me.Frame1.Controls.Add(Me._optShow_1)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(351, 138)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(439, 58)
        Me.Frame1.TabIndex = 19
        Me.Frame1.TabStop = False
        '
        '_optShow_2
        '
        Me._optShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_2, CType(2, Short))
        Me._optShow_2.Location = New System.Drawing.Point(245, 8)
        Me._optShow_2.Name = "_optShow_2"
        Me._optShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_2.Size = New System.Drawing.Size(113, 20)
        Me._optShow_2.TabIndex = 42
        Me._optShow_2.TabStop = True
        Me._optShow_2.Text = "Summary (Item)"
        Me._optShow_2.UseVisualStyleBackColor = False
        '
        'chkValue
        '
        Me.chkValue.BackColor = System.Drawing.SystemColors.Control
        Me.chkValue.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkValue.Enabled = False
        Me.chkValue.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkValue.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkValue.Location = New System.Drawing.Point(308, 28)
        Me.chkValue.Name = "chkValue"
        Me.chkValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkValue.Size = New System.Drawing.Size(120, 24)
        Me.chkValue.TabIndex = 32
        Me.chkValue.Text = "Value Required"
        Me.chkValue.UseVisualStyleBackColor = False
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(53, 31)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(247, 21)
        Me.cboShow.TabIndex = 27
        '
        '_optShow_0
        '
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Checked = True
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(57, 8)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(63, 20)
        Me._optShow_0.TabIndex = 21
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
        Me._optShow_1.Location = New System.Drawing.Point(131, 8)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(107, 20)
        Me._optShow_1.TabIndex = 20
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "Summary (F4)"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(7, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(40, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Show :"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._OptShowNo_1)
        Me.Frame5.Controls.Add(Me._OptShowNo_0)
        Me.Frame5.Controls.Add(Me.chkAll)
        Me.Frame5.Controls.Add(Me.cmdsearch)
        Me.Frame5.Controls.Add(Me.TxtC4No)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(2, 138)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(346, 58)
        Me.Frame5.TabIndex = 33
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Show"
        '
        '_OptShowNo_1
        '
        Me._OptShowNo_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptShowNo_1.Checked = True
        Me._OptShowNo_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShowNo_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShowNo_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShowNo.SetIndex(Me._OptShowNo_1, CType(1, Short))
        Me._OptShowNo_1.Location = New System.Drawing.Point(66, 23)
        Me._OptShowNo_1.Name = "_OptShowNo_1"
        Me._OptShowNo_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShowNo_1.Size = New System.Drawing.Size(54, 18)
        Me._OptShowNo_1.TabIndex = 47
        Me._OptShowNo_1.TabStop = True
        Me._OptShowNo_1.Text = "RGP"
        Me._OptShowNo_1.UseVisualStyleBackColor = False
        '
        '_OptShowNo_0
        '
        Me._OptShowNo_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptShowNo_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShowNo_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShowNo_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShowNo.SetIndex(Me._OptShowNo_0, CType(0, Short))
        Me._OptShowNo_0.Location = New System.Drawing.Point(4, 23)
        Me._OptShowNo_0.Name = "_OptShowNo_0"
        Me._OptShowNo_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShowNo_0.Size = New System.Drawing.Size(64, 18)
        Me._OptShowNo_0.TabIndex = 46
        Me._OptShowNo_0.Text = "F4No"
        Me._OptShowNo_0.UseVisualStyleBackColor = False
        '
        'chkAll
        '
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(292, 22)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(48, 20)
        Me.chkAll.TabIndex = 36
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.lstCategory)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(2, 58)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(196, 80)
        Me.Frame7.TabIndex = 39
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Category"
        '
        'lstCategory
        '
        Me.lstCategory.BackColor = System.Drawing.SystemColors.Window
        Me.lstCategory.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstCategory.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCategory.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstCategory.IntegralHeight = False
        Me.lstCategory.Items.AddRange(New Object() {"lstCategory"})
        Me.lstCategory.Location = New System.Drawing.Point(2, 12)
        Me.lstCategory.Name = "lstCategory"
        Me.lstCategory.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstCategory.Size = New System.Drawing.Size(190, 62)
        Me.lstCategory.TabIndex = 40
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 192)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(898, 358)
        Me.Frame4.TabIndex = 6
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(894, 348)
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
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(652, 552)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 56)
        Me.FraMovement.TabIndex = 7
        Me.FraMovement.TabStop = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._optOrder_3)
        Me.Frame3.Controls.Add(Me._optOrder_2)
        Me.Frame3.Controls.Add(Me._optOrder_1)
        Me.Frame3.Controls.Add(Me._optOrder_0)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(2, 553)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(207, 57)
        Me.Frame3.TabIndex = 24
        Me.Frame3.TabStop = False
        '
        '_optOrder_3
        '
        Me._optOrder_3.BackColor = System.Drawing.SystemColors.Control
        Me._optOrder_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrder_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrder_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrder.SetIndex(Me._optOrder_3, CType(3, Short))
        Me._optOrder_3.Location = New System.Drawing.Point(98, 32)
        Me._optOrder_3.Name = "_optOrder_3"
        Me._optOrder_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrder_3.Size = New System.Drawing.Size(97, 18)
        Me._optOrder_3.TabIndex = 41
        Me._optOrder_3.TabStop = True
        Me._optOrder_3.Text = "Dept Code"
        Me._optOrder_3.UseVisualStyleBackColor = False
        '
        '_optOrder_2
        '
        Me._optOrder_2.BackColor = System.Drawing.SystemColors.Control
        Me._optOrder_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrder_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrder_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrder.SetIndex(Me._optOrder_2, CType(2, Short))
        Me._optOrder_2.Location = New System.Drawing.Point(98, 12)
        Me._optOrder_2.Name = "_optOrder_2"
        Me._optOrder_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrder_2.Size = New System.Drawing.Size(75, 18)
        Me._optOrder_2.TabIndex = 31
        Me._optOrder_2.TabStop = True
        Me._optOrder_2.Text = "Material Code"
        Me._optOrder_2.UseVisualStyleBackColor = False
        '
        '_optOrder_1
        '
        Me._optOrder_1.BackColor = System.Drawing.SystemColors.Control
        Me._optOrder_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrder_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrder_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrder.SetIndex(Me._optOrder_1, CType(1, Short))
        Me._optOrder_1.Location = New System.Drawing.Point(4, 32)
        Me._optOrder_1.Name = "_optOrder_1"
        Me._optOrder_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrder_1.Size = New System.Drawing.Size(97, 18)
        Me._optOrder_1.TabIndex = 26
        Me._optOrder_1.TabStop = True
        Me._optOrder_1.Text = "Party Name"
        Me._optOrder_1.UseVisualStyleBackColor = False
        '
        '_optOrder_0
        '
        Me._optOrder_0.BackColor = System.Drawing.SystemColors.Control
        Me._optOrder_0.Checked = True
        Me._optOrder_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optOrder_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optOrder_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optOrder.SetIndex(Me._optOrder_0, CType(0, Short))
        Me._optOrder_0.Location = New System.Drawing.Point(4, 12)
        Me._optOrder_0.Name = "_optOrder_0"
        Me._optOrder_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optOrder_0.Size = New System.Drawing.Size(75, 18)
        Me._optOrder_0.TabIndex = 25
        Me._optOrder_0.TabStop = True
        Me._optOrder_0.Text = "Ref No"
        Me._optOrder_0.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me._OptWise_2)
        Me.Frame8.Controls.Add(Me._OptWise_0)
        Me.Frame8.Controls.Add(Me._OptWise_1)
        Me.Frame8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(216, 552)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(236, 56)
        Me.Frame8.TabIndex = 43
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Wise"
        '
        '_OptWise_2
        '
        Me._OptWise_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptWise_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptWise_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptWise_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptWise.SetIndex(Me._OptWise_2, CType(2, Short))
        Me._OptWise_2.Location = New System.Drawing.Point(2, 32)
        Me._OptWise_2.Name = "_OptWise_2"
        Me._OptWise_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptWise_2.Size = New System.Drawing.Size(112, 18)
        Me._OptWise_2.TabIndex = 58
        Me._OptWise_2.TabStop = True
        Me._OptWise_2.Text = "Emp-Pre Wise"
        Me._OptWise_2.UseVisualStyleBackColor = False
        '
        '_OptWise_0
        '
        Me._OptWise_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptWise_0.Checked = True
        Me._OptWise_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptWise_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptWise_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptWise.SetIndex(Me._OptWise_0, CType(0, Short))
        Me._OptWise_0.Location = New System.Drawing.Point(2, 12)
        Me._OptWise_0.Name = "_OptWise_0"
        Me._OptWise_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptWise_0.Size = New System.Drawing.Size(90, 18)
        Me._OptWise_0.TabIndex = 45
        Me._OptWise_0.TabStop = True
        Me._OptWise_0.Text = "Dept Wise"
        Me._OptWise_0.UseVisualStyleBackColor = False
        '
        '_OptWise_1
        '
        Me._OptWise_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptWise_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptWise_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptWise_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptWise.SetIndex(Me._OptWise_1, CType(1, Short))
        Me._OptWise_1.Location = New System.Drawing.Point(124, 12)
        Me._OptWise_1.Name = "_OptWise_1"
        Me._OptWise_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptWise_1.Size = New System.Drawing.Size(90, 18)
        Me._OptWise_1.TabIndex = 44
        Me._OptWise_1.TabStop = True
        Me._OptWise_1.Text = "Emp Wise"
        Me._OptWise_1.UseVisualStyleBackColor = False
        '
        'Frame9
        '
        Me.Frame9.BackColor = System.Drawing.SystemColors.Control
        Me.Frame9.Controls.Add(Me.cboDivision)
        Me.Frame9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame9.Location = New System.Drawing.Point(456, 552)
        Me.Frame9.Name = "Frame9"
        Me.Frame9.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame9.Size = New System.Drawing.Size(192, 56)
        Me.Frame9.TabIndex = 48
        Me.Frame9.TabStop = False
        Me.Frame9.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(18, 20)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(170, 21)
        Me.cboDivision.TabIndex = 49
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(276, 428)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(71, 13)
        Me.lblBookType.TabIndex = 29
        Me.lblBookType.Text = "lblBookType"
        '
        'optOrder
        '
        '
        'optShow
        '
        '
        'frmParamF4DetailOutward
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame10)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.chkList)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame9)
        Me.Controls.Add(Me.lblBookType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamF4DetailOutward"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "C4 Details Received"
        Me.Frame10.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame7.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame8.ResumeLayout(False)
        Me.Frame9.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShowNo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptWise, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optOrder, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
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