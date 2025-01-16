Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamSaleReg
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
        'Me.MDIParent = SalesGST.Master

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
    Public WithEvents Frame8 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents txtTariffHeading As System.Windows.Forms.TextBox
    Public WithEvents chkShowTariff As System.Windows.Forms.CheckBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents _optType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optType_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents lstInvoiceType As System.Windows.Forms.CheckedListBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtEmpName As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchEmp As System.Windows.Forms.Button
    Public WithEvents chkAllEmp As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtAccount As System.Windows.Forms.TextBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents cboCT1 As System.Windows.Forms.ComboBox
    Public WithEvents cboDuty As System.Windows.Forms.ComboBox
    Public WithEvents cboMRP As System.Windows.Forms.ComboBox
    Public WithEvents cboLocation As System.Windows.Forms.ComboBox
    Public WithEvents cboCT3 As System.Windows.Forms.ComboBox
    Public WithEvents cboExport As System.Windows.Forms.ComboBox
    Public WithEvents cboCancelled As System.Windows.Forms.ComboBox
    Public WithEvents cboRejection As System.Windows.Forms.ComboBox
    Public WithEvents cboFOC As System.Windows.Forms.ComboBox
    Public WithEvents cboAgtD3 As System.Windows.Forms.ComboBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents lblAcCode As System.Windows.Forms.Label
    Public WithEvents lblTrnType As System.Windows.Forms.Label
    Public WithEvents Lbl As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents optType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamSaleReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtTariffHeading = New System.Windows.Forms.TextBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtEmpName = New System.Windows.Forms.TextBox()
        Me.cmdsearchEmp = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.chkShowTariff = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optType_2 = New System.Windows.Forms.RadioButton()
        Me._optType_1 = New System.Windows.Forms.RadioButton()
        Me._optType_0 = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.lstInvoiceType = New System.Windows.Forms.CheckedListBox()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkAllEmp = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.cboCT1 = New System.Windows.Forms.ComboBox()
        Me.cboDuty = New System.Windows.Forms.ComboBox()
        Me.cboMRP = New System.Windows.Forms.ComboBox()
        Me.cboLocation = New System.Windows.Forms.ComboBox()
        Me.cboCT3 = New System.Windows.Forms.ComboBox()
        Me.cboExport = New System.Windows.Forms.ComboBox()
        Me.cboCancelled = New System.Windows.Forms.ComboBox()
        Me.cboRejection = New System.Windows.Forms.ComboBox()
        Me.cboFOC = New System.Windows.Forms.ComboBox()
        Me.cboAgtD3 = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.lblTrnType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.optType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame8.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTariffHeading
        '
        Me.txtTariffHeading.AcceptsReturn = True
        Me.txtTariffHeading.BackColor = System.Drawing.SystemColors.Window
        Me.txtTariffHeading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTariffHeading.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTariffHeading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTariffHeading.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTariffHeading.Location = New System.Drawing.Point(3, 14)
        Me.txtTariffHeading.MaxLength = 0
        Me.txtTariffHeading.Name = "txtTariffHeading"
        Me.txtTariffHeading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTariffHeading.Size = New System.Drawing.Size(81, 19)
        Me.txtTariffHeading.TabIndex = 49
        Me.ToolTip1.SetToolTip(Me.txtTariffHeading, "Press F1 For Help")
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(100, 43)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(73, 19)
        Me.txtEmpCode.TabIndex = 60
        Me.ToolTip1.SetToolTip(Me.txtEmpCode, "Press F1 For Help")
        '
        'txtEmpName
        '
        Me.txtEmpName.AcceptsReturn = True
        Me.txtEmpName.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpName.Location = New System.Drawing.Point(174, 43)
        Me.txtEmpName.MaxLength = 0
        Me.txtEmpName.Name = "txtEmpName"
        Me.txtEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpName.Size = New System.Drawing.Size(224, 19)
        Me.txtEmpName.TabIndex = 58
        Me.ToolTip1.SetToolTip(Me.txtEmpName, "Press F1 For Help")
        '
        'cmdsearchEmp
        '
        Me.cmdsearchEmp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchEmp.Image = CType(resources.GetObject("cmdsearchEmp.Image"), System.Drawing.Image)
        Me.cmdsearchEmp.Location = New System.Drawing.Point(400, 42)
        Me.cmdsearchEmp.Name = "cmdsearchEmp"
        Me.cmdsearchEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchEmp.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearchEmp.TabIndex = 57
        Me.cmdsearchEmp.TabStop = False
        Me.cmdsearchEmp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchEmp, "Search")
        Me.cmdsearchEmp.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(400, 12)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearch.TabIndex = 7
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
        Me.TxtAccount.Location = New System.Drawing.Point(100, 13)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(298, 19)
        Me.TxtAccount.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.Location = New System.Drawing.Point(168, 13)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(56, 37)
        Me.cmdClose.TabIndex = 17
        Me.cmdClose.Text = "&Close"
        Me.cmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdClose, "Close the Form")
        Me.cmdClose.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Enabled = False
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(57, 13)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(56, 37)
        Me.cmdPrint.TabIndex = 15
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
        Me.cmdShow.Location = New System.Drawing.Point(2, 13)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(56, 37)
        Me.cmdShow.TabIndex = 14
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(113, 13)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(56, 37)
        Me.CmdPreview.TabIndex = 16
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.cboShow)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(385, 72)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(125, 41)
        Me.Frame8.TabIndex = 53
        Me.Frame8.TabStop = False
        Me.Frame8.Text = "Show"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(2, 14)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(121, 22)
        Me.cboShow.TabIndex = 54
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.cboDivision)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(256, 72)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(127, 41)
        Me.Frame7.TabIndex = 51
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Division"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(4, 14)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(119, 22)
        Me.cboDivision.TabIndex = 52
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtTariffHeading)
        Me.Frame5.Controls.Add(Me.chkShowTariff)
        Me.Frame5.Controls.Add(Me.Label3)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(511, 72)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(173, 41)
        Me.Frame5.TabIndex = 47
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Tariff"
        '
        'chkShowTariff
        '
        Me.chkShowTariff.AutoSize = True
        Me.chkShowTariff.BackColor = System.Drawing.SystemColors.Control
        Me.chkShowTariff.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkShowTariff.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkShowTariff.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkShowTariff.Location = New System.Drawing.Point(87, 14)
        Me.chkShowTariff.Name = "chkShowTariff"
        Me.chkShowTariff.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkShowTariff.Size = New System.Drawing.Size(71, 18)
        Me.chkShowTariff.TabIndex = 48
        Me.chkShowTariff.Text = "Null Tariff"
        Me.chkShowTariff.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(40, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(0, 14)
        Me.Label3.TabIndex = 50
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optType_2)
        Me.Frame1.Controls.Add(Me._optType_1)
        Me.Frame1.Controls.Add(Me._optType_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(685, 72)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(211, 41)
        Me.Frame1.TabIndex = 27
        Me.Frame1.TabStop = False
        '
        '_optType_2
        '
        Me._optType_2.AutoSize = True
        Me._optType_2.BackColor = System.Drawing.SystemColors.Control
        Me._optType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_2, CType(2, Short))
        Me._optType_2.Location = New System.Drawing.Point(152, 14)
        Me._optType_2.Name = "_optType_2"
        Me._optType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_2.Size = New System.Drawing.Size(47, 18)
        Me._optType_2.TabIndex = 5
        Me._optType_2.TabStop = True
        Me._optType_2.Text = "Both"
        Me._optType_2.UseVisualStyleBackColor = False
        '
        '_optType_1
        '
        Me._optType_1.AutoSize = True
        Me._optType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_1, CType(1, Short))
        Me._optType_1.Location = New System.Drawing.Point(73, 14)
        Me._optType_1.Name = "_optType_1"
        Me._optType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_1.Size = New System.Drawing.Size(59, 18)
        Me._optType_1.TabIndex = 4
        Me._optType_1.TabStop = True
        Me._optType_1.Text = "Central"
        Me._optType_1.UseVisualStyleBackColor = False
        '
        '_optType_0
        '
        Me._optType_0.AutoSize = True
        Me._optType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optType_0.Checked = True
        Me._optType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optType.SetIndex(Me._optType_0, CType(0, Short))
        Me._optType_0.Location = New System.Drawing.Point(2, 14)
        Me._optType_0.Name = "_optType_0"
        Me._optType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optType_0.Size = New System.Drawing.Size(51, 18)
        Me._optType_0.TabIndex = 3
        Me._optType_0.TabStop = True
        Me._optType_0.Text = "Local"
        Me._optType_0.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(256, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(146, 72)
        Me.Frame6.TabIndex = 18
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(60, 13)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(75, 20)
        Me.txtDateFrom.TabIndex = 1
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(60, 43)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(75, 20)
        Me.txtDateTo.TabIndex = 2
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(30, 47)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(24, 14)
        Me._Lbl_1.TabIndex = 20
        Me._Lbl_1.Text = "To :"
        Me._Lbl_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Lbl_0
        '
        Me._Lbl_0.AutoSize = True
        Me._Lbl_0.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_0, CType(0, Short))
        Me._Lbl_0.Location = New System.Drawing.Point(18, 16)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(37, 14)
        Me._Lbl_0.TabIndex = 19
        Me._Lbl_0.Text = "From :"
        Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.Frame3.Size = New System.Drawing.Size(254, 114)
        Me.Frame3.TabIndex = 33
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Invoice Type"
        '
        'lstInvoiceType
        '
        Me.lstInvoiceType.BackColor = System.Drawing.SystemColors.Window
        Me.lstInvoiceType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lstInvoiceType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstInvoiceType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.lstInvoiceType.IntegralHeight = False
        Me.lstInvoiceType.Location = New System.Drawing.Point(2, 14)
        Me.lstInvoiceType.Name = "lstInvoiceType"
        Me.lstInvoiceType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lstInvoiceType.Size = New System.Drawing.Size(248, 96)
        Me.lstInvoiceType.TabIndex = 0
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtEmpCode)
        Me.FraAccount.Controls.Add(Me.txtEmpName)
        Me.FraAccount.Controls.Add(Me.cmdsearchEmp)
        Me.FraAccount.Controls.Add(Me.chkAllEmp)
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtAccount)
        Me.FraAccount.Controls.Add(Me.Label12)
        Me.FraAccount.Controls.Add(Me.Label9)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(405, 0)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(492, 72)
        Me.FraAccount.TabIndex = 21
        Me.FraAccount.TabStop = False
        '
        'chkAllEmp
        '
        Me.chkAllEmp.AutoSize = True
        Me.chkAllEmp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllEmp.Checked = True
        Me.chkAllEmp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllEmp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllEmp.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllEmp.Location = New System.Drawing.Point(436, 45)
        Me.chkAllEmp.Name = "chkAllEmp"
        Me.chkAllEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllEmp.Size = New System.Drawing.Size(46, 18)
        Me.chkAllEmp.TabIndex = 56
        Me.chkAllEmp.Text = "ALL"
        Me.chkAllEmp.UseVisualStyleBackColor = False
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(436, 15)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(46, 18)
        Me.chkAll.TabIndex = 8
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(20, 45)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(77, 14)
        Me.Label12.TabIndex = 59
        Me.Label12.Text = "Sales Person :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(13, 15)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(84, 14)
        Me.Label9.TabIndex = 55
        Me.Label9.Text = "Account Name :"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(2, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(98, 17)
        Me.Label2.TabIndex = 26
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 110)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(900, 448)
        Me.Frame4.TabIndex = 22
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(896, 436)
        Me.SprdMain.TabIndex = 13
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 14
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(672, 556)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(229, 55)
        Me.FraMovement.TabIndex = 23
        Me.FraMovement.TabStop = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.cboCT1)
        Me.Frame2.Controls.Add(Me.cboDuty)
        Me.Frame2.Controls.Add(Me.cboMRP)
        Me.Frame2.Controls.Add(Me.cboLocation)
        Me.Frame2.Controls.Add(Me.cboCT3)
        Me.Frame2.Controls.Add(Me.cboExport)
        Me.Frame2.Controls.Add(Me.cboCancelled)
        Me.Frame2.Controls.Add(Me.cboRejection)
        Me.Frame2.Controls.Add(Me.cboFOC)
        Me.Frame2.Controls.Add(Me.cboAgtD3)
        Me.Frame2.Controls.Add(Me.Label14)
        Me.Frame2.Controls.Add(Me.Label13)
        Me.Frame2.Controls.Add(Me.lblBookType)
        Me.Frame2.Controls.Add(Me.Label11)
        Me.Frame2.Controls.Add(Me.Label10)
        Me.Frame2.Controls.Add(Me.Label8)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 556)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(519, 55)
        Me.Frame2.TabIndex = 28
        Me.Frame2.TabStop = False
        '
        'cboCT1
        '
        Me.cboCT1.BackColor = System.Drawing.SystemColors.Window
        Me.cboCT1.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCT1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCT1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCT1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCT1.Location = New System.Drawing.Point(464, 10)
        Me.cboCT1.Name = "cboCT1"
        Me.cboCT1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCT1.Size = New System.Drawing.Size(51, 22)
        Me.cboCT1.TabIndex = 44
        '
        'cboDuty
        '
        Me.cboDuty.BackColor = System.Drawing.SystemColors.Window
        Me.cboDuty.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDuty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDuty.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDuty.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDuty.Location = New System.Drawing.Point(464, 32)
        Me.cboDuty.Name = "cboDuty"
        Me.cboDuty.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDuty.Size = New System.Drawing.Size(51, 22)
        Me.cboDuty.TabIndex = 43
        '
        'cboMRP
        '
        Me.cboMRP.BackColor = System.Drawing.SystemColors.Window
        Me.cboMRP.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboMRP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMRP.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMRP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboMRP.Location = New System.Drawing.Point(364, 32)
        Me.cboMRP.Name = "cboMRP"
        Me.cboMRP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboMRP.Size = New System.Drawing.Size(51, 22)
        Me.cboMRP.TabIndex = 40
        '
        'cboLocation
        '
        Me.cboLocation.BackColor = System.Drawing.SystemColors.Window
        Me.cboLocation.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLocation.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLocation.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLocation.Location = New System.Drawing.Point(277, 32)
        Me.cboLocation.Name = "cboLocation"
        Me.cboLocation.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLocation.Size = New System.Drawing.Size(51, 22)
        Me.cboLocation.TabIndex = 38
        '
        'cboCT3
        '
        Me.cboCT3.BackColor = System.Drawing.SystemColors.Window
        Me.cboCT3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCT3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCT3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCT3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCT3.Location = New System.Drawing.Point(364, 10)
        Me.cboCT3.Name = "cboCT3"
        Me.cboCT3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCT3.Size = New System.Drawing.Size(51, 22)
        Me.cboCT3.TabIndex = 36
        '
        'cboExport
        '
        Me.cboExport.BackColor = System.Drawing.SystemColors.Window
        Me.cboExport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboExport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExport.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboExport.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboExport.Location = New System.Drawing.Point(277, 10)
        Me.cboExport.Name = "cboExport"
        Me.cboExport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboExport.Size = New System.Drawing.Size(51, 22)
        Me.cboExport.TabIndex = 34
        '
        'cboCancelled
        '
        Me.cboCancelled.BackColor = System.Drawing.SystemColors.Window
        Me.cboCancelled.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCancelled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCancelled.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCancelled.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCancelled.Location = New System.Drawing.Point(170, 32)
        Me.cboCancelled.Name = "cboCancelled"
        Me.cboCancelled.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCancelled.Size = New System.Drawing.Size(51, 22)
        Me.cboCancelled.TabIndex = 12
        '
        'cboRejection
        '
        Me.cboRejection.BackColor = System.Drawing.SystemColors.Window
        Me.cboRejection.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRejection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRejection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRejection.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRejection.Location = New System.Drawing.Point(170, 10)
        Me.cboRejection.Name = "cboRejection"
        Me.cboRejection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRejection.Size = New System.Drawing.Size(51, 22)
        Me.cboRejection.TabIndex = 11
        '
        'cboFOC
        '
        Me.cboFOC.BackColor = System.Drawing.SystemColors.Window
        Me.cboFOC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFOC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFOC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFOC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFOC.Location = New System.Drawing.Point(54, 32)
        Me.cboFOC.Name = "cboFOC"
        Me.cboFOC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFOC.Size = New System.Drawing.Size(51, 22)
        Me.cboFOC.TabIndex = 10
        '
        'cboAgtD3
        '
        Me.cboAgtD3.BackColor = System.Drawing.SystemColors.Window
        Me.cboAgtD3.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboAgtD3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAgtD3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboAgtD3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboAgtD3.Location = New System.Drawing.Point(54, 10)
        Me.cboAgtD3.Name = "cboAgtD3"
        Me.cboAgtD3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboAgtD3.Size = New System.Drawing.Size(51, 22)
        Me.cboAgtD3.TabIndex = 9
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(431, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(32, 14)
        Me.Label14.TabIndex = 46
        Me.Label14.Text = "CT1 :"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(429, 34)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(35, 14)
        Me.Label13.TabIndex = 45
        Me.Label13.Text = "Duty :"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(64, 34)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(25, 19)
        Me.lblBookType.TabIndex = 42
        Me.lblBookType.Text = "lblBookType"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(328, 34)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(34, 14)
        Me.Label11.TabIndex = 41
        Me.Label11.Text = "MRP :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(224, 34)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(54, 14)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "Location :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(331, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(32, 14)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "CT3 :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(234, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(44, 14)
        Me.Label1.TabIndex = 35
        Me.Label1.Text = "Export :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(106, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(60, 14)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "Cancelled :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(109, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(57, 14)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Rejection :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(16, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(34, 14)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "FOC :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(1, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(49, 14)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Agt. D3 :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(412, 428)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(87, 13)
        Me.lblAcCode.TabIndex = 25
        Me.lblAcCode.Text = "lblAcCode"
        Me.lblAcCode.Visible = False
        '
        'lblTrnType
        '
        Me.lblTrnType.AutoSize = True
        Me.lblTrnType.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrnType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTrnType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrnType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrnType.Location = New System.Drawing.Point(446, 438)
        Me.lblTrnType.Name = "lblTrnType"
        Me.lblTrnType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnType.Size = New System.Drawing.Size(56, 14)
        Me.lblTrnType.TabIndex = 24
        Me.lblTrnType.Text = "lblTrnType"
        Me.lblTrnType.Visible = False
        '
        'optType
        '
        '
        'frmParamSaleReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(900, 611)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.lblAcCode)
        Me.Controls.Add(Me.lblTrnType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 24)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmParamSaleReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Sale Register"
        Me.Frame8.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optType, System.ComponentModel.ISupportInitialize).EndInit()
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