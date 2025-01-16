Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmParamLoadingReg
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
    Public WithEvents _optFreightType_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optFreightType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optFreightType_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame10 As System.Windows.Forms.GroupBox
    Public WithEvents chkPendingInTime As System.Windows.Forms.CheckBox
    Public WithEvents ChkStandardTrans As System.Windows.Forms.CheckBox
    Public WithEvents chkInComplete As System.Windows.Forms.CheckBox
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents chkWOCollection As System.Windows.Forms.CheckBox
    Public WithEvents _OptShowAck_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShowAck_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShowAck_2 As System.Windows.Forms.RadioButton
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents _OptVT_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptVT_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptVT_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _OptOrderBy_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptOrderBy_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents cboDivision As System.Windows.Forms.ComboBox
    Public WithEvents txtVehicleNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchVehicle As System.Windows.Forms.Button
    Public WithEvents chkVehicleAll As System.Windows.Forms.CheckBox
    Public WithEvents txtSupplier As System.Windows.Forms.TextBox
    Public WithEvents cmdsearchSupp As System.Windows.Forms.Button
    Public WithEvents chkAllSupp As System.Windows.Forms.CheckBox
    Public WithEvents chkAll As System.Windows.Forms.CheckBox
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents TxtItemName As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraAccount As System.Windows.Forms.GroupBox
    Public WithEvents chkAck As System.Windows.Forms.CheckBox
    Public WithEvents txtDateFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtDateTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents _Lbl_1 As System.Windows.Forms.Label
    Public WithEvents _Lbl_0 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdClose As System.Windows.Forms.Button
    Public WithEvents cmdShow As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblAcCode As System.Windows.Forms.Label
    Public WithEvents lblTrnType As System.Windows.Forms.Label
    Public WithEvents Lbl As VB6.LabelArray
    Public WithEvents OptOrderBy As VB6.RadioButtonArray
    Public WithEvents OptShow As VB6.RadioButtonArray
    Public WithEvents OptShowAck As VB6.RadioButtonArray
    Public WithEvents OptVT As VB6.RadioButtonArray
    Public WithEvents optFreightType As VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParamLoadingReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtVehicleNo = New System.Windows.Forms.TextBox()
        Me.cmdSearchVehicle = New System.Windows.Forms.Button()
        Me.txtSupplier = New System.Windows.Forms.TextBox()
        Me.cmdsearchSupp = New System.Windows.Forms.Button()
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.TxtItemName = New System.Windows.Forms.TextBox()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdShow = New System.Windows.Forms.Button()
        Me.txtTransporter = New System.Windows.Forms.TextBox()
        Me.cmdSearchTransport = New System.Windows.Forms.Button()
        Me.Frame10 = New System.Windows.Forms.GroupBox()
        Me._optFreightType_2 = New System.Windows.Forms.RadioButton()
        Me._optFreightType_1 = New System.Windows.Forms.RadioButton()
        Me._optFreightType_0 = New System.Windows.Forms.RadioButton()
        Me.chkPendingInTime = New System.Windows.Forms.CheckBox()
        Me.ChkStandardTrans = New System.Windows.Forms.CheckBox()
        Me.Frame7 = New System.Windows.Forms.GroupBox()
        Me.chkInComplete = New System.Windows.Forms.CheckBox()
        Me.chkWOCollection = New System.Windows.Forms.CheckBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me._OptShowAck_1 = New System.Windows.Forms.RadioButton()
        Me._OptShowAck_0 = New System.Windows.Forms.RadioButton()
        Me._OptShowAck_2 = New System.Windows.Forms.RadioButton()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._OptVT_2 = New System.Windows.Forms.RadioButton()
        Me._OptVT_0 = New System.Windows.Forms.RadioButton()
        Me._OptVT_1 = New System.Windows.Forms.RadioButton()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._OptShow_2 = New System.Windows.Forms.RadioButton()
        Me._OptShow_1 = New System.Windows.Forms.RadioButton()
        Me._OptShow_0 = New System.Windows.Forms.RadioButton()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptOrderBy_0 = New System.Windows.Forms.RadioButton()
        Me._OptOrderBy_1 = New System.Windows.Forms.RadioButton()
        Me.FraAccount = New System.Windows.Forms.GroupBox()
        Me.chkTransportAll = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboDivision = New System.Windows.Forms.ComboBox()
        Me.chkVehicleAll = New System.Windows.Forms.CheckBox()
        Me.chkAllSupp = New System.Windows.Forms.CheckBox()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me.chkAck = New System.Windows.Forms.CheckBox()
        Me.txtDateFrom = New System.Windows.Forms.MaskedTextBox()
        Me.txtDateTo = New System.Windows.Forms.MaskedTextBox()
        Me._Lbl_1 = New System.Windows.Forms.Label()
        Me._Lbl_0 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblAcCode = New System.Windows.Forms.Label()
        Me.lblTrnType = New System.Windows.Forms.Label()
        Me.Lbl = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.OptOrderBy = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptShowAck = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptVT = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optFreightType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstCompanyName = New System.Windows.Forms.CheckedListBox()
        Me.Frame10.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.FraAccount.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptShowAck, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptVT, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optFreightType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.AcceptsReturn = True
        Me.txtVehicleNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVehicleNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVehicleNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVehicleNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVehicleNo.Location = New System.Drawing.Point(82, 59)
        Me.txtVehicleNo.MaxLength = 0
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVehicleNo.Size = New System.Drawing.Size(358, 22)
        Me.txtVehicleNo.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.txtVehicleNo, "Press F1 For Help")
        '
        'cmdSearchVehicle
        '
        Me.cmdSearchVehicle.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchVehicle.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchVehicle.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchVehicle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchVehicle.Image = CType(resources.GetObject("cmdSearchVehicle.Image"), System.Drawing.Image)
        Me.cmdSearchVehicle.Location = New System.Drawing.Point(440, 59)
        Me.cmdSearchVehicle.Name = "cmdSearchVehicle"
        Me.cmdSearchVehicle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchVehicle.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchVehicle.TabIndex = 28
        Me.cmdSearchVehicle.TabStop = False
        Me.cmdSearchVehicle.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchVehicle, "Search")
        Me.cmdSearchVehicle.UseVisualStyleBackColor = False
        '
        'txtSupplier
        '
        Me.txtSupplier.AcceptsReturn = True
        Me.txtSupplier.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSupplier.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSupplier.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSupplier.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSupplier.Location = New System.Drawing.Point(82, 11)
        Me.txtSupplier.MaxLength = 0
        Me.txtSupplier.Name = "txtSupplier"
        Me.txtSupplier.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSupplier.Size = New System.Drawing.Size(358, 22)
        Me.txtSupplier.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.txtSupplier, "Press F1 For Help")
        '
        'cmdsearchSupp
        '
        Me.cmdsearchSupp.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearchSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearchSupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearchSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearchSupp.Image = CType(resources.GetObject("cmdsearchSupp.Image"), System.Drawing.Image)
        Me.cmdsearchSupp.Location = New System.Drawing.Point(440, 10)
        Me.cmdsearchSupp.Name = "cmdsearchSupp"
        Me.cmdsearchSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearchSupp.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearchSupp.TabIndex = 23
        Me.cmdsearchSupp.TabStop = False
        Me.cmdsearchSupp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearchSupp, "Search")
        Me.cmdsearchSupp.UseVisualStyleBackColor = False
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(440, 35)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdsearch.TabIndex = 3
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'TxtItemName
        '
        Me.TxtItemName.AcceptsReturn = True
        Me.TxtItemName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtItemName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtItemName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtItemName.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtItemName.Location = New System.Drawing.Point(82, 35)
        Me.TxtItemName.MaxLength = 0
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtItemName.Size = New System.Drawing.Size(358, 22)
        Me.TxtItemName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.TxtItemName, "Press F1 For Help")
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(123, 17)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(60, 41)
        Me.CmdPreview.TabIndex = 8
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
        Me.cmdPrint.Location = New System.Drawing.Point(63, 17)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(60, 41)
        Me.cmdPrint.TabIndex = 7
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
        Me.cmdClose.Location = New System.Drawing.Point(184, 17)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdClose.Size = New System.Drawing.Size(60, 41)
        Me.cmdClose.TabIndex = 9
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
        Me.cmdShow.Location = New System.Drawing.Point(4, 17)
        Me.cmdShow.Name = "cmdShow"
        Me.cmdShow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdShow.Size = New System.Drawing.Size(60, 41)
        Me.cmdShow.TabIndex = 6
        Me.cmdShow.Text = "Sho&w"
        Me.cmdShow.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdShow, "Show Record")
        Me.cmdShow.UseVisualStyleBackColor = False
        '
        'txtTransporter
        '
        Me.txtTransporter.AcceptsReturn = True
        Me.txtTransporter.BackColor = System.Drawing.SystemColors.Window
        Me.txtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTransporter.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTransporter.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransporter.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTransporter.Location = New System.Drawing.Point(82, 83)
        Me.txtTransporter.MaxLength = 0
        Me.txtTransporter.Name = "txtTransporter"
        Me.txtTransporter.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTransporter.Size = New System.Drawing.Size(358, 22)
        Me.txtTransporter.TabIndex = 52
        Me.ToolTip1.SetToolTip(Me.txtTransporter, "Press F1 For Help")
        '
        'cmdSearchTransport
        '
        Me.cmdSearchTransport.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchTransport.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchTransport.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchTransport.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchTransport.Image = CType(resources.GetObject("cmdSearchTransport.Image"), System.Drawing.Image)
        Me.cmdSearchTransport.Location = New System.Drawing.Point(440, 83)
        Me.cmdSearchTransport.Name = "cmdSearchTransport"
        Me.cmdSearchTransport.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchTransport.Size = New System.Drawing.Size(28, 23)
        Me.cmdSearchTransport.TabIndex = 51
        Me.cmdSearchTransport.TabStop = False
        Me.cmdSearchTransport.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchTransport, "Search")
        Me.cmdSearchTransport.UseVisualStyleBackColor = False
        '
        'Frame10
        '
        Me.Frame10.BackColor = System.Drawing.SystemColors.Control
        Me.Frame10.Controls.Add(Me._optFreightType_2)
        Me.Frame10.Controls.Add(Me._optFreightType_1)
        Me.Frame10.Controls.Add(Me._optFreightType_0)
        Me.Frame10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame10.Location = New System.Drawing.Point(790, -2)
        Me.Frame10.Name = "Frame10"
        Me.Frame10.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame10.Size = New System.Drawing.Size(110, 80)
        Me.Frame10.TabIndex = 50
        Me.Frame10.TabStop = False
        Me.Frame10.Text = "Freight Type"
        '
        '_optFreightType_2
        '
        Me._optFreightType_2.BackColor = System.Drawing.SystemColors.Control
        Me._optFreightType_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFreightType_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFreightType_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFreightType.SetIndex(Me._optFreightType_2, CType(2, Short))
        Me._optFreightType_2.Location = New System.Drawing.Point(9, 58)
        Me._optFreightType_2.Name = "_optFreightType_2"
        Me._optFreightType_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFreightType_2.Size = New System.Drawing.Size(91, 17)
        Me._optFreightType_2.TabIndex = 53
        Me._optFreightType_2.TabStop = True
        Me._optFreightType_2.Text = "Premium"
        Me._optFreightType_2.UseVisualStyleBackColor = False
        '
        '_optFreightType_1
        '
        Me._optFreightType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optFreightType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFreightType_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFreightType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFreightType.SetIndex(Me._optFreightType_1, CType(1, Short))
        Me._optFreightType_1.Location = New System.Drawing.Point(9, 38)
        Me._optFreightType_1.Name = "_optFreightType_1"
        Me._optFreightType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFreightType_1.Size = New System.Drawing.Size(91, 17)
        Me._optFreightType_1.TabIndex = 52
        Me._optFreightType_1.TabStop = True
        Me._optFreightType_1.Text = "Regular"
        Me._optFreightType_1.UseVisualStyleBackColor = False
        '
        '_optFreightType_0
        '
        Me._optFreightType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optFreightType_0.Checked = True
        Me._optFreightType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optFreightType_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optFreightType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optFreightType.SetIndex(Me._optFreightType_0, CType(0, Short))
        Me._optFreightType_0.Location = New System.Drawing.Point(9, 18)
        Me._optFreightType_0.Name = "_optFreightType_0"
        Me._optFreightType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optFreightType_0.Size = New System.Drawing.Size(91, 17)
        Me._optFreightType_0.TabIndex = 51
        Me._optFreightType_0.TabStop = True
        Me._optFreightType_0.Text = "All"
        Me._optFreightType_0.UseVisualStyleBackColor = False
        '
        'chkPendingInTime
        '
        Me.chkPendingInTime.BackColor = System.Drawing.SystemColors.Control
        Me.chkPendingInTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkPendingInTime.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPendingInTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkPendingInTime.Location = New System.Drawing.Point(673, 81)
        Me.chkPendingInTime.Name = "chkPendingInTime"
        Me.chkPendingInTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkPendingInTime.Size = New System.Drawing.Size(185, 17)
        Me.chkPendingInTime.TabIndex = 47
        Me.chkPendingInTime.Text = "Pending IN Time"
        Me.chkPendingInTime.UseVisualStyleBackColor = False
        '
        'ChkStandardTrans
        '
        Me.ChkStandardTrans.BackColor = System.Drawing.SystemColors.Control
        Me.ChkStandardTrans.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkStandardTrans.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkStandardTrans.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkStandardTrans.Location = New System.Drawing.Point(454, 588)
        Me.ChkStandardTrans.Name = "ChkStandardTrans"
        Me.ChkStandardTrans.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkStandardTrans.Size = New System.Drawing.Size(193, 17)
        Me.ChkStandardTrans.TabIndex = 46
        Me.ChkStandardTrans.Text = "Show Standard Transportation"
        Me.ChkStandardTrans.UseVisualStyleBackColor = False
        Me.ChkStandardTrans.Visible = False
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.chkInComplete)
        Me.Frame7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(449, 544)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(199, 33)
        Me.Frame7.TabIndex = 43
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "Incomplete "
        '
        'chkInComplete
        '
        Me.chkInComplete.BackColor = System.Drawing.SystemColors.Control
        Me.chkInComplete.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkInComplete.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkInComplete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkInComplete.Location = New System.Drawing.Point(4, 13)
        Me.chkInComplete.Name = "chkInComplete"
        Me.chkInComplete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkInComplete.Size = New System.Drawing.Size(157, 17)
        Me.chkInComplete.TabIndex = 44
        Me.chkInComplete.Text = "Only Incompelete Entry"
        Me.chkInComplete.UseVisualStyleBackColor = False
        '
        'chkWOCollection
        '
        Me.chkWOCollection.BackColor = System.Drawing.SystemColors.Control
        Me.chkWOCollection.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkWOCollection.Enabled = False
        Me.chkWOCollection.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkWOCollection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkWOCollection.Location = New System.Drawing.Point(673, 104)
        Me.chkWOCollection.Name = "chkWOCollection"
        Me.chkWOCollection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkWOCollection.Size = New System.Drawing.Size(145, 17)
        Me.chkWOCollection.TabIndex = 42
        Me.chkWOCollection.Text = "Only W/o Collection"
        Me.chkWOCollection.UseVisualStyleBackColor = False
        Me.chkWOCollection.Visible = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me._OptShowAck_1)
        Me.Frame5.Controls.Add(Me._OptShowAck_0)
        Me.Frame5.Controls.Add(Me._OptShowAck_2)
        Me.Frame5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(301, 544)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(145, 65)
        Me.Frame5.TabIndex = 38
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Show"
        '
        '_OptShowAck_1
        '
        Me._OptShowAck_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptShowAck_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShowAck_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShowAck_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShowAck.SetIndex(Me._OptShowAck_1, CType(1, Short))
        Me._OptShowAck_1.Location = New System.Drawing.Point(50, 26)
        Me._OptShowAck_1.Name = "_OptShowAck_1"
        Me._OptShowAck_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShowAck_1.Size = New System.Drawing.Size(90, 18)
        Me._OptShowAck_1.TabIndex = 41
        Me._OptShowAck_1.TabStop = True
        Me._OptShowAck_1.Text = "Completed"
        Me._OptShowAck_1.UseVisualStyleBackColor = False
        '
        '_OptShowAck_0
        '
        Me._OptShowAck_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptShowAck_0.Checked = True
        Me._OptShowAck_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShowAck_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShowAck_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShowAck.SetIndex(Me._OptShowAck_0, CType(0, Short))
        Me._OptShowAck_0.Location = New System.Drawing.Point(50, 10)
        Me._OptShowAck_0.Name = "_OptShowAck_0"
        Me._OptShowAck_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShowAck_0.Size = New System.Drawing.Size(90, 18)
        Me._OptShowAck_0.TabIndex = 40
        Me._OptShowAck_0.TabStop = True
        Me._OptShowAck_0.Text = "All"
        Me._OptShowAck_0.UseVisualStyleBackColor = False
        '
        '_OptShowAck_2
        '
        Me._OptShowAck_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptShowAck_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShowAck_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShowAck_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShowAck.SetIndex(Me._OptShowAck_2, CType(2, Short))
        Me._OptShowAck_2.Location = New System.Drawing.Point(50, 42)
        Me._OptShowAck_2.Name = "_OptShowAck_2"
        Me._OptShowAck_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShowAck_2.Size = New System.Drawing.Size(90, 18)
        Me._OptShowAck_2.TabIndex = 39
        Me._OptShowAck_2.TabStop = True
        Me._OptShowAck_2.Text = "Pending"
        Me._OptShowAck_2.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._OptVT_2)
        Me.Frame3.Controls.Add(Me._OptVT_0)
        Me.Frame3.Controls.Add(Me._OptVT_1)
        Me.Frame3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(144, 544)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(156, 65)
        Me.Frame3.TabIndex = 34
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Vehicle Type"
        '
        '_OptVT_2
        '
        Me._OptVT_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptVT_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptVT_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptVT_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptVT.SetIndex(Me._OptVT_2, CType(2, Short))
        Me._OptVT_2.Location = New System.Drawing.Point(48, 44)
        Me._OptVT_2.Name = "_OptVT_2"
        Me._OptVT_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptVT_2.Size = New System.Drawing.Size(98, 17)
        Me._OptVT_2.TabIndex = 37
        Me._OptVT_2.TabStop = True
        Me._OptVT_2.Text = "Third Party"
        Me._OptVT_2.UseVisualStyleBackColor = False
        '
        '_OptVT_0
        '
        Me._OptVT_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptVT_0.Checked = True
        Me._OptVT_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptVT_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptVT_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptVT.SetIndex(Me._OptVT_0, CType(0, Short))
        Me._OptVT_0.Location = New System.Drawing.Point(48, 12)
        Me._OptVT_0.Name = "_OptVT_0"
        Me._OptVT_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptVT_0.Size = New System.Drawing.Size(98, 17)
        Me._OptVT_0.TabIndex = 36
        Me._OptVT_0.TabStop = True
        Me._OptVT_0.Text = "All"
        Me._OptVT_0.UseVisualStyleBackColor = False
        '
        '_OptVT_1
        '
        Me._OptVT_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptVT_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptVT_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptVT_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptVT.SetIndex(Me._OptVT_1, CType(1, Short))
        Me._OptVT_1.Location = New System.Drawing.Point(48, 28)
        Me._OptVT_1.Name = "_OptVT_1"
        Me._OptVT_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptVT_1.Size = New System.Drawing.Size(98, 17)
        Me._OptVT_1.TabIndex = 35
        Me._OptVT_1.TabStop = True
        Me._OptVT_1.Text = "Company"
        Me._OptVT_1.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._OptShow_2)
        Me.Frame1.Controls.Add(Me._OptShow_1)
        Me.Frame1.Controls.Add(Me._OptShow_0)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(673, -2)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(115, 80)
        Me.Frame1.TabIndex = 31
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Show"
        '
        '_OptShow_2
        '
        Me._OptShow_2.AutoSize = True
        Me._OptShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_2, CType(2, Short))
        Me._OptShow_2.Location = New System.Drawing.Point(5, 60)
        Me._OptShow_2.Name = "_OptShow_2"
        Me._OptShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_2.Size = New System.Drawing.Size(101, 17)
        Me._OptShow_2.TabIndex = 34
        Me._OptShow_2.TabStop = True
        Me._OptShow_2.Text = "Transport Wise"
        Me._OptShow_2.UseVisualStyleBackColor = False
        '
        '_OptShow_1
        '
        Me._OptShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_1, CType(1, Short))
        Me._OptShow_1.Location = New System.Drawing.Point(5, 38)
        Me._OptShow_1.Name = "_OptShow_1"
        Me._OptShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_1.Size = New System.Drawing.Size(77, 17)
        Me._OptShow_1.TabIndex = 33
        Me._OptShow_1.TabStop = True
        Me._OptShow_1.Text = "Summary"
        Me._OptShow_1.UseVisualStyleBackColor = False
        '
        '_OptShow_0
        '
        Me._OptShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptShow_0.Checked = True
        Me._OptShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptShow_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptShow.SetIndex(Me._OptShow_0, CType(0, Short))
        Me._OptShow_0.Location = New System.Drawing.Point(5, 18)
        Me._OptShow_0.Name = "_OptShow_0"
        Me._OptShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptShow_0.Size = New System.Drawing.Size(67, 17)
        Me._OptShow_0.TabIndex = 32
        Me._OptShow_0.TabStop = True
        Me._OptShow_0.Text = "Detail"
        Me._OptShow_0.UseVisualStyleBackColor = False
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptOrderBy_0)
        Me.Frame2.Controls.Add(Me._OptOrderBy_1)
        Me.Frame2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(2, 544)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(140, 65)
        Me.Frame2.TabIndex = 19
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Order By"
        '
        '_OptOrderBy_0
        '
        Me._OptOrderBy_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_0.Checked = True
        Me._OptOrderBy_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_0, CType(0, Short))
        Me._OptOrderBy_0.Location = New System.Drawing.Point(7, 17)
        Me._OptOrderBy_0.Name = "_OptOrderBy_0"
        Me._OptOrderBy_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_0.Size = New System.Drawing.Size(85, 17)
        Me._OptOrderBy_0.TabIndex = 21
        Me._OptOrderBy_0.TabStop = True
        Me._OptOrderBy_0.Text = "Slip No."
        Me._OptOrderBy_0.UseVisualStyleBackColor = False
        '
        '_OptOrderBy_1
        '
        Me._OptOrderBy_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptOrderBy_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptOrderBy_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptOrderBy_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptOrderBy.SetIndex(Me._OptOrderBy_1, CType(1, Short))
        Me._OptOrderBy_1.Location = New System.Drawing.Point(7, 37)
        Me._OptOrderBy_1.Name = "_OptOrderBy_1"
        Me._OptOrderBy_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptOrderBy_1.Size = New System.Drawing.Size(119, 17)
        Me._OptOrderBy_1.TabIndex = 20
        Me._OptOrderBy_1.TabStop = True
        Me._OptOrderBy_1.Text = "Item Description"
        Me._OptOrderBy_1.UseVisualStyleBackColor = False
        '
        'FraAccount
        '
        Me.FraAccount.BackColor = System.Drawing.SystemColors.Control
        Me.FraAccount.Controls.Add(Me.txtTransporter)
        Me.FraAccount.Controls.Add(Me.cmdSearchTransport)
        Me.FraAccount.Controls.Add(Me.chkTransportAll)
        Me.FraAccount.Controls.Add(Me.Label4)
        Me.FraAccount.Controls.Add(Me.cboDivision)
        Me.FraAccount.Controls.Add(Me.txtVehicleNo)
        Me.FraAccount.Controls.Add(Me.cmdSearchVehicle)
        Me.FraAccount.Controls.Add(Me.chkVehicleAll)
        Me.FraAccount.Controls.Add(Me.txtSupplier)
        Me.FraAccount.Controls.Add(Me.cmdsearchSupp)
        Me.FraAccount.Controls.Add(Me.chkAllSupp)
        Me.FraAccount.Controls.Add(Me.chkAll)
        Me.FraAccount.Controls.Add(Me.cmdsearch)
        Me.FraAccount.Controls.Add(Me.TxtItemName)
        Me.FraAccount.Controls.Add(Me.Label3)
        Me.FraAccount.Controls.Add(Me.Label1)
        Me.FraAccount.Controls.Add(Me.Label5)
        Me.FraAccount.Controls.Add(Me.Label2)
        Me.FraAccount.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraAccount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraAccount.Location = New System.Drawing.Point(151, -2)
        Me.FraAccount.Name = "FraAccount"
        Me.FraAccount.Padding = New System.Windows.Forms.Padding(0)
        Me.FraAccount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraAccount.Size = New System.Drawing.Size(518, 132)
        Me.FraAccount.TabIndex = 13
        Me.FraAccount.TabStop = False
        '
        'chkTransportAll
        '
        Me.chkTransportAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkTransportAll.Checked = True
        Me.chkTransportAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTransportAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkTransportAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkTransportAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkTransportAll.Location = New System.Drawing.Point(471, 85)
        Me.chkTransportAll.Name = "chkTransportAll"
        Me.chkTransportAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkTransportAll.Size = New System.Drawing.Size(45, 19)
        Me.chkTransportAll.TabIndex = 50
        Me.chkTransportAll.Text = "ALL"
        Me.chkTransportAll.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(12, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Transporter :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboDivision
        '
        Me.cboDivision.BackColor = System.Drawing.SystemColors.Window
        Me.cboDivision.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDivision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDivision.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDivision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDivision.Location = New System.Drawing.Point(82, 106)
        Me.cboDivision.Name = "cboDivision"
        Me.cboDivision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDivision.Size = New System.Drawing.Size(358, 21)
        Me.cboDivision.TabIndex = 48
        '
        'chkVehicleAll
        '
        Me.chkVehicleAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkVehicleAll.Checked = True
        Me.chkVehicleAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVehicleAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkVehicleAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkVehicleAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkVehicleAll.Location = New System.Drawing.Point(471, 61)
        Me.chkVehicleAll.Name = "chkVehicleAll"
        Me.chkVehicleAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkVehicleAll.Size = New System.Drawing.Size(45, 19)
        Me.chkVehicleAll.TabIndex = 27
        Me.chkVehicleAll.Text = "ALL"
        Me.chkVehicleAll.UseVisualStyleBackColor = False
        '
        'chkAllSupp
        '
        Me.chkAllSupp.BackColor = System.Drawing.SystemColors.Control
        Me.chkAllSupp.Checked = True
        Me.chkAllSupp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllSupp.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAllSupp.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAllSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAllSupp.Location = New System.Drawing.Point(471, 12)
        Me.chkAllSupp.Name = "chkAllSupp"
        Me.chkAllSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAllSupp.Size = New System.Drawing.Size(45, 19)
        Me.chkAllSupp.TabIndex = 22
        Me.chkAllSupp.Text = "ALL"
        Me.chkAllSupp.UseVisualStyleBackColor = False
        '
        'chkAll
        '
        Me.chkAll.BackColor = System.Drawing.SystemColors.Control
        Me.chkAll.Checked = True
        Me.chkAll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAll.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAll.Location = New System.Drawing.Point(471, 38)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAll.Size = New System.Drawing.Size(45, 19)
        Me.chkAll.TabIndex = 4
        Me.chkAll.Text = "ALL"
        Me.chkAll.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(24, 110)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Division :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(12, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Vehicle No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(23, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Supplier :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(9, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Item Name :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.chkAck)
        Me.Frame6.Controls.Add(Me.txtDateFrom)
        Me.Frame6.Controls.Add(Me.txtDateTo)
        Me.Frame6.Controls.Add(Me._Lbl_1)
        Me.Frame6.Controls.Add(Me._Lbl_0)
        Me.Frame6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(0, -2)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(148, 132)
        Me.Frame6.TabIndex = 10
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Date"
        '
        'chkAck
        '
        Me.chkAck.BackColor = System.Drawing.SystemColors.Control
        Me.chkAck.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAck.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAck.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAck.Location = New System.Drawing.Point(46, 101)
        Me.chkAck.Name = "chkAck"
        Me.chkAck.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAck.Size = New System.Drawing.Size(89, 17)
        Me.chkAck.TabIndex = 45
        Me.chkAck.Text = "Ack. Date"
        Me.chkAck.UseVisualStyleBackColor = False
        '
        'txtDateFrom
        '
        Me.txtDateFrom.AllowPromptAsInput = False
        Me.txtDateFrom.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateFrom.Location = New System.Drawing.Point(55, 17)
        Me.txtDateFrom.Mask = "##/##/####"
        Me.txtDateFrom.Name = "txtDateFrom"
        Me.txtDateFrom.Size = New System.Drawing.Size(85, 22)
        Me.txtDateFrom.TabIndex = 0
        '
        'txtDateTo
        '
        Me.txtDateTo.AllowPromptAsInput = False
        Me.txtDateTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateTo.Location = New System.Drawing.Point(55, 50)
        Me.txtDateTo.Mask = "##/##/####"
        Me.txtDateTo.Name = "txtDateTo"
        Me.txtDateTo.Size = New System.Drawing.Size(85, 22)
        Me.txtDateTo.TabIndex = 1
        '
        '_Lbl_1
        '
        Me._Lbl_1.AutoSize = True
        Me._Lbl_1.BackColor = System.Drawing.SystemColors.Control
        Me._Lbl_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Lbl_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Lbl_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Lbl.SetIndex(Me._Lbl_1, CType(1, Short))
        Me._Lbl_1.Location = New System.Drawing.Point(23, 55)
        Me._Lbl_1.Name = "_Lbl_1"
        Me._Lbl_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_1.Size = New System.Drawing.Size(25, 13)
        Me._Lbl_1.TabIndex = 12
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
        Me._Lbl_0.Location = New System.Drawing.Point(8, 21)
        Me._Lbl_0.Name = "_Lbl_0"
        Me._Lbl_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Lbl_0.Size = New System.Drawing.Size(40, 13)
        Me._Lbl_0.TabIndex = 11
        Me._Lbl_0.Text = "From :"
        Me._Lbl_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.SprdMain)
        Me.Frame4.Controls.Add(Me.Report1)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 127)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1040, 413)
        Me.Frame4.TabIndex = 14
        Me.Frame4.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1035, 402)
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
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdClose)
        Me.FraMovement.Controls.Add(Me.cmdShow)
        Me.FraMovement.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(790, 544)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(247, 69)
        Me.FraMovement.TabIndex = 15
        Me.FraMovement.TabStop = False
        '
        'lblBookType
        '
        Me.lblBookType.AutoSize = True
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(826, 109)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(71, 13)
        Me.lblBookType.TabIndex = 26
        Me.lblBookType.Text = "lblBookType"
        Me.lblBookType.Visible = False
        '
        'lblAcCode
        '
        Me.lblAcCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAcCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAcCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAcCode.Location = New System.Drawing.Point(808, 530)
        Me.lblAcCode.Name = "lblAcCode"
        Me.lblAcCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAcCode.Size = New System.Drawing.Size(87, 13)
        Me.lblAcCode.TabIndex = 17
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
        Me.lblTrnType.Location = New System.Drawing.Point(742, 526)
        Me.lblTrnType.Name = "lblTrnType"
        Me.lblTrnType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnType.Size = New System.Drawing.Size(59, 13)
        Me.lblTrnType.TabIndex = 16
        Me.lblTrnType.Text = "lblTrnType"
        Me.lblTrnType.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.Controls.Add(Me.lstCompanyName)
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GroupBox1.Location = New System.Drawing.Point(904, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GroupBox1.Size = New System.Drawing.Size(131, 132)
        Me.GroupBox1.TabIndex = 51
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
        Me.lstCompanyName.Size = New System.Drawing.Size(131, 119)
        Me.lstCompanyName.TabIndex = 3
        '
        'frmParamLoadingReg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1039, 611)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Frame10)
        Me.Controls.Add(Me.chkPendingInTime)
        Me.Controls.Add(Me.ChkStandardTrans)
        Me.Controls.Add(Me.Frame7)
        Me.Controls.Add(Me.chkWOCollection)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.FraAccount)
        Me.Controls.Add(Me.Frame6)
        Me.Controls.Add(Me.Frame4)
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
        Me.Name = "frmParamLoadingReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "UnLoading Register"
        Me.Frame10.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame5.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.FraAccount.ResumeLayout(False)
        Me.FraAccount.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Lbl, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptOrderBy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptShowAck, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptVT, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optFreightType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
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

    Public WithEvents _OptShow_2 As RadioButton
    Public WithEvents txtTransporter As TextBox
    Public WithEvents cmdSearchTransport As Button
    Public WithEvents chkTransportAll As CheckBox
    Public WithEvents Label4 As Label
    Public WithEvents GroupBox1 As GroupBox
    Public WithEvents lstCompanyName As CheckedListBox
#End Region
End Class