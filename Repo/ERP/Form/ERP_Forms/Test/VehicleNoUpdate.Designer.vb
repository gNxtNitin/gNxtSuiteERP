Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmVehicleNoUpdate
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
        'Me.MDIParent = AccountGST.Master
        'AccountGST.Master.Show
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
    Public WithEvents Frame11 As System.Windows.Forms.GroupBox
    Public WithEvents _OptSelection_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptSelection_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents OptSelection As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVehicleNoUpdate))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.TxtAccount = New System.Windows.Forms.TextBox()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdUpdateVehicleNo = New System.Windows.Forms.Button()
        Me.txtVehicle = New System.Windows.Forms.TextBox()
        Me.cmdSearchVehicle = New System.Windows.Forms.Button()
        Me.txtVehicleNew = New System.Windows.Forms.TextBox()
        Me.cmdSearchVehicleNew = New System.Windows.Forms.Button()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.txtConsolidateEWay = New System.Windows.Forms.TextBox()
        Me.cmdConsolidatedEWayBill = New System.Windows.Forms.Button()
        Me.Frame11 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboShow = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptSelection_0 = New System.Windows.Forms.RadioButton()
        Me._OptSelection_1 = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.OptSelection = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame8 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.chkAllVehicleNew = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.chkAllVehicle = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboReasonCode = New System.Windows.Forms.ComboBox()
        Me.Frame11.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.OptSelection, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame8.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClose.Image = CType(resources.GetObject("cmdClose.Image"), System.Drawing.Image)
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdClose.Location = New System.Drawing.Point(746, 11)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(105, 37)
        Me.cmdClose.TabIndex = 8
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
        Me.cmdShow.Location = New System.Drawing.Point(4, 11)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(105, 37)
        Me.cmdShow.TabIndex = 6
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'TxtAccount
        '
        Me.TxtAccount.AcceptsReturn = True
        Me.TxtAccount.BackColor = System.Drawing.SystemColors.Window
        Me.TxtAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtAccount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtAccount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAccount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtAccount.Location = New System.Drawing.Point(117, 18)
        Me.TxtAccount.MaxLength = 0
        Me.TxtAccount.Name = "TxtAccount"
        Me.TxtAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtAccount.Size = New System.Drawing.Size(278, 20)
        Me.TxtAccount.TabIndex = 39
        Me.ToolTip1.SetToolTip(Me.TxtAccount, "Press F1 For Help")
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(395, 17)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 21)
        Me.cmdsearch.TabIndex = 38
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdUpdateVehicleNo
        '
        Me.cmdUpdateVehicleNo.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdateVehicleNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdateVehicleNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdUpdateVehicleNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdateVehicleNo.Image = CType(resources.GetObject("cmdUpdateVehicleNo.Image"), System.Drawing.Image)
        Me.cmdUpdateVehicleNo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdUpdateVehicleNo.Location = New System.Drawing.Point(179, 11)
        Me.cmdUpdateVehicleNo.Name = "cmdUpdateVehicleNo"
        Me.cmdUpdateVehicleNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdateVehicleNo.Size = New System.Drawing.Size(105, 37)
        Me.cmdUpdateVehicleNo.TabIndex = 62
        Me.cmdUpdateVehicleNo.Text = "&Update Vehicle No"
        Me.cmdUpdateVehicleNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdUpdateVehicleNo, "Save Record")
        Me.cmdUpdateVehicleNo.UseVisualStyleBackColor = False
        '
        'txtVehicle
        '
        Me.txtVehicle.AcceptsReturn = True
        Me.txtVehicle.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVehicle.Location = New System.Drawing.Point(117, 41)
        Me.txtVehicle.MaxLength = 0
        Me.txtVehicle.Name = "txtVehicle"
        Me.txtVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicle.Size = New System.Drawing.Size(278, 20)
        Me.txtVehicle.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.txtVehicle, "Press F1 For Help")
        '
        'cmdSearchVehicle
        '
        Me.cmdSearchVehicle.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchVehicle.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchVehicle.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchVehicle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchVehicle.Image = CType(resources.GetObject("cmdSearchVehicle.Image"), System.Drawing.Image)
        Me.cmdSearchVehicle.Location = New System.Drawing.Point(395, 41)
        Me.cmdSearchVehicle.Name = "cmdSearchVehicle"
        Me.cmdSearchVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchVehicle.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchVehicle.TabIndex = 42
        Me.cmdSearchVehicle.TabStop = False
        Me.cmdSearchVehicle.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchVehicle, "Search")
        Me.cmdSearchVehicle.UseVisualStyleBackColor = False
        '
        'txtVehicleNew
        '
        Me.txtVehicleNew.AcceptsReturn = True
        Me.txtVehicleNew.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicleNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicleNew.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicleNew.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNew.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVehicleNew.Location = New System.Drawing.Point(117, 65)
        Me.txtVehicleNew.MaxLength = 0
        Me.txtVehicleNew.Name = "txtVehicleNew"
        Me.txtVehicleNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicleNew.Size = New System.Drawing.Size(278, 20)
        Me.txtVehicleNew.TabIndex = 47
        Me.ToolTip1.SetToolTip(Me.txtVehicleNew, "Press F1 For Help")
        '
        'cmdSearchVehicleNew
        '
        Me.cmdSearchVehicleNew.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchVehicleNew.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchVehicleNew.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchVehicleNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchVehicleNew.Image = CType(resources.GetObject("cmdSearchVehicleNew.Image"), System.Drawing.Image)
        Me.cmdSearchVehicleNew.Location = New System.Drawing.Point(395, 66)
        Me.cmdSearchVehicleNew.Name = "cmdSearchVehicleNew"
        Me.cmdSearchVehicleNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchVehicleNew.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchVehicleNew.TabIndex = 46
        Me.cmdSearchVehicleNew.TabStop = False
        Me.cmdSearchVehicleNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchVehicleNew, "Search")
        Me.cmdSearchVehicleNew.UseVisualStyleBackColor = False
        '
        'txtReason
        '
        Me.txtReason.AcceptsReturn = True
        Me.txtReason.BackColor = System.Drawing.SystemColors.Window
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtReason.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReason.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtReason.Location = New System.Drawing.Point(97, 46)
        Me.txtReason.MaxLength = 0
        Me.txtReason.Name = "txtReason"
        Me.txtReason.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtReason.Size = New System.Drawing.Size(154, 20)
        Me.txtReason.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.txtReason, "Press F1 For Help")
        '
        'txtConsolidateEWay
        '
        Me.txtConsolidateEWay.AcceptsReturn = True
        Me.txtConsolidateEWay.BackColor = System.Drawing.SystemColors.Window
        Me.txtConsolidateEWay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtConsolidateEWay.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtConsolidateEWay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtConsolidateEWay.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtConsolidateEWay.Location = New System.Drawing.Point(117, 89)
        Me.txtConsolidateEWay.MaxLength = 0
        Me.txtConsolidateEWay.Name = "txtConsolidateEWay"
        Me.txtConsolidateEWay.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtConsolidateEWay.Size = New System.Drawing.Size(278, 20)
        Me.txtConsolidateEWay.TabIndex = 49
        Me.ToolTip1.SetToolTip(Me.txtConsolidateEWay, "Press F1 For Help")
        '
        'cmdConsolidatedEWayBill
        '
        Me.cmdConsolidatedEWayBill.BackColor = System.Drawing.SystemColors.Control
        Me.cmdConsolidatedEWayBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdConsolidatedEWayBill.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdConsolidatedEWayBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdConsolidatedEWayBill.Image = CType(resources.GetObject("cmdConsolidatedEWayBill.Image"), System.Drawing.Image)
        Me.cmdConsolidatedEWayBill.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdConsolidatedEWayBill.Location = New System.Drawing.Point(375, 11)
        Me.cmdConsolidatedEWayBill.Name = "cmdConsolidatedEWayBill"
        Me.cmdConsolidatedEWayBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdConsolidatedEWayBill.Size = New System.Drawing.Size(105, 37)
        Me.cmdConsolidatedEWayBill.TabIndex = 64
        Me.cmdConsolidatedEWayBill.Text = "Re-&Consolidated EWay"
        Me.cmdConsolidatedEWayBill.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdConsolidatedEWayBill, "Save Record")
        Me.cmdConsolidatedEWayBill.UseVisualStyleBackColor = False
        '
        'Frame11
        '
        Me.Frame11.BackColor = System.Drawing.SystemColors.Control
        Me.Frame11.Controls.Add(Me.Label6)
        Me.Frame11.Controls.Add(Me.cboShow)
        Me.Frame11.Controls.Add(Me.Label5)
        Me.Frame11.Controls.Add(Me.cboDivision)
        Me.Frame11.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame11.Location = New System.Drawing.Point(904, 0)
        Me.Frame11.Name = "Frame11"
        Me.Frame11.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame11.Size = New System.Drawing.Size(199, 71)
        Me.Frame11.TabIndex = 15
        Me.Frame11.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(20, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(44, 14)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Show :"
        '
        'cboShow
        '
        Me.cboShow.BackColor = System.Drawing.SystemColors.Window
        Me.cboShow.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboShow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboShow.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboShow.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboShow.Location = New System.Drawing.Point(69, 46)
        Me.cboShow.Name = "cboShow"
        Me.cboShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboShow.Size = New System.Drawing.Size(125, 22)
        Me.cboShow.TabIndex = 19
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(56, 14)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Division :"
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(69, 15)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(125, 22)
        Me.cboDivision.TabIndex = 16
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._OptSelection_0)
        Me.Frame1.Controls.Add(Me._OptSelection_1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Frame1.Location = New System.Drawing.Point(904, 69)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(119, 48)
        Me.Frame1.TabIndex = 12
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Selection"
        '
        '_OptSelection_0
        '
        Me._OptSelection_0.AutoSize = True
        Me._OptSelection_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelection_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelection_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelection_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelection.SetIndex(Me._OptSelection_0, CType(0, Short))
        Me._OptSelection_0.Location = New System.Drawing.Point(10, 20)
        Me._OptSelection_0.Name = "_OptSelection_0"
        Me._OptSelection_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelection_0.Size = New System.Drawing.Size(39, 18)
        Me._OptSelection_0.TabIndex = 3
        Me._OptSelection_0.TabStop = True
        Me._OptSelection_0.Text = "All"
        Me._OptSelection_0.UseVisualStyleBackColor = False
        '
        '_OptSelection_1
        '
        Me._OptSelection_1.AutoSize = True
        Me._OptSelection_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptSelection_1.Checked = True
        Me._OptSelection_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptSelection_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptSelection_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSelection.SetIndex(Me._OptSelection_1, CType(1, Short))
        Me._OptSelection_1.Location = New System.Drawing.Point(59, 20)
        Me._OptSelection_1.Name = "_OptSelection_1"
        Me._OptSelection_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptSelection_1.Size = New System.Drawing.Size(53, 18)
        Me._OptSelection_1.TabIndex = 4
        Me._OptSelection_1.TabStop = True
        Me._OptSelection_1.Text = "None"
        Me._OptSelection_1.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me.Label2)
        Me.Frame6.Controls.Add(Me.Label1)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, 0)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(157, 112)
        Me.Frame6.TabIndex = 0
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(77, 16)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(74, 20)
        Me.txtDateFrom.TabIndex = 1
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(77, 46)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(74, 20)
        Me.txtDateTo.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(47, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(26, 14)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "To :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(69, 14)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Date From :"
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
        Me.Frame4.Size = New System.Drawing.Size(1109, 458)
        Me.Frame4.TabIndex = 10
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1109, 445)
        Me.SprdMain.TabIndex = 5
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(24, 70)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 6
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.cmdConsolidatedEWayBill)
        Me.FraMovement.Controls.Add(Me.cmdUpdateVehicleNo)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(249, 566)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(854, 53)
        Me.FraMovement.TabIndex = 11
        Me.FraMovement.TabStop = False
        '
        'OptSelection
        '
        '
        'Frame8
        '
        Me.Frame8.BackColor = System.Drawing.SystemColors.Control
        Me.Frame8.Controls.Add(Me.Label10)
        Me.Frame8.Controls.Add(Me.txtConsolidateEWay)
        Me.Frame8.Controls.Add(Me.Label9)
        Me.Frame8.Controls.Add(Me.txtVehicleNew)
        Me.Frame8.Controls.Add(Me.cmdSearchVehicleNew)
        Me.Frame8.Controls.Add(Me.chkAllVehicleNew)
        Me.Frame8.Controls.Add(Me.Label8)
        Me.Frame8.Controls.Add(Me.txtVehicle)
        Me.Frame8.Controls.Add(Me.cmdSearchVehicle)
        Me.Frame8.Controls.Add(Me.chkAllVehicle)
        Me.Frame8.Controls.Add(Me.Label7)
        Me.Frame8.Controls.Add(Me.TxtAccount)
        Me.Frame8.Controls.Add(Me.cmdsearch)
        Me.Frame8.Controls.Add(Me.chkAll)
        Me.Frame8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame8.Location = New System.Drawing.Point(161, 0)
        Me.Frame8.Name = "Frame8"
        Me.Frame8.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame8.Size = New System.Drawing.Size(479, 114)
        Me.Frame8.TabIndex = 37
        Me.Frame8.TabStop = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(0, 92)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(117, 14)
        Me.Label10.TabIndex = 50
        Me.Label10.Text = "Consolidated EWay :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(20, 68)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(97, 14)
        Me.Label9.TabIndex = 48
        Me.Label9.Text = "New Vehicle No :"
        '
        'chkAllVehicleNew
        '
        Me.chkAllVehicleNew.AutoSize = True
        Me.chkAllVehicleNew.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllVehicleNew.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllVehicleNew.Enabled = False
        Me.chkAllVehicleNew.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllVehicleNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllVehicleNew.Location = New System.Drawing.Point(425, 68)
        Me.chkAllVehicleNew.Name = "chkAllVehicleNew"
        Me.chkAllVehicleNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllVehicleNew.Size = New System.Drawing.Size(48, 18)
        Me.chkAllVehicleNew.TabIndex = 45
        Me.chkAllVehicleNew.Text = "ALL"
        Me.chkAllVehicleNew.UseVisualStyleBackColor = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(26, 44)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(91, 14)
        Me.Label8.TabIndex = 44
        Me.Label8.Text = "Old Vehicle No :"
        '
        'chkAllVehicle
        '
        Me.chkAllVehicle.AutoSize = True
        Me.chkAllVehicle.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllVehicle.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllVehicle.Enabled = False
        Me.chkAllVehicle.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllVehicle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllVehicle.Location = New System.Drawing.Point(425, 43)
        Me.chkAllVehicle.Name = "chkAllVehicle"
        Me.chkAllVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllVehicle.Size = New System.Drawing.Size(48, 18)
        Me.chkAllVehicle.TabIndex = 41
        Me.chkAllVehicle.Text = "ALL"
        Me.chkAllVehicle.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(48, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(69, 14)
        Me.Label7.TabIndex = 40
        Me.Label7.Text = "Customer :"
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
        Me.chkAll.Location = New System.Drawing.Point(425, 19)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(48, 18)
        Me.chkAll.TabIndex = 37
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(143, 601)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(74, 14)
        Me.lblBookType.TabIndex = 38
        Me.lblBookType.Text = "lblBookType"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.txtReason)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cboReasonCode)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(643, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(257, 73)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(40, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(54, 14)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Reason :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(8, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(86, 14)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Reason Code :"
        '
        'cboReasonCode
        '
        Me.cboReasonCode.BackColor = System.Drawing.SystemColors.Window
        Me.cboReasonCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboReasonCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboReasonCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboReasonCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboReasonCode.Location = New System.Drawing.Point(97, 15)
        Me.cboReasonCode.Name = "cboReasonCode"
        Me.cboReasonCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboReasonCode.Size = New System.Drawing.Size(154, 22)
        Me.cboReasonCode.TabIndex = 16
        '
        'frmVehicleNoUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1108, 621)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblBookType)
        Me.Controls.Add(Me.Frame8)
        Me.Controls.Add(Me.Frame11)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(3, 23)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVehicleNoUpdate"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Vehicle No Update"
        Me.Frame11.ResumeLayout(False)
        Me.Frame11.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.OptSelection, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame8.ResumeLayout(False)
        Me.Frame8.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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

    Public WithEvents Frame8 As GroupBox
    Public WithEvents TxtAccount As TextBox
    Public WithEvents cmdsearch As Button
    Public WithEvents chkAll As CheckBox
    Public WithEvents lblBookType As Label
    Public WithEvents cmdUpdateVehicleNo As Button
    Public WithEvents Label6 As Label
    Public WithEvents cboShow As ComboBox
    Public WithEvents Label5 As Label
    Public WithEvents Label7 As Label
    Public WithEvents Label8 As Label
    Public WithEvents txtVehicle As TextBox
    Public WithEvents cmdSearchVehicle As Button
    Public WithEvents chkAllVehicle As CheckBox
    Public WithEvents Label9 As Label
    Public WithEvents txtVehicleNew As TextBox
    Public WithEvents cmdSearchVehicleNew As Button
    Public WithEvents chkAllVehicleNew As CheckBox
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents Label3 As Label
    Public WithEvents Label4 As Label
    Public WithEvents cboReasonCode As ComboBox
    Public WithEvents txtReason As TextBox
    Public WithEvents Label10 As Label
    Public WithEvents txtConsolidateEWay As TextBox
    Public WithEvents cmdConsolidatedEWayBill As Button
#End Region
End Class