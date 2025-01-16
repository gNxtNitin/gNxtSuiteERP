Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTripRateMaster
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
        'Me.MDIParent = Admin.Master
        'Admin.Master.Show
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
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage0 As System.Windows.Forms.TabPage
    Public WithEvents SprdMain2 As AxFPSpreadADO.AxfpSpread
    Public WithEvents _SSTab1_TabPage1 As System.Windows.Forms.TabPage
    Public WithEvents SSTab1 As System.Windows.Forms.TabControl
    Public WithEvents txtPremiumRate As System.Windows.Forms.TextBox
    Public WithEvents txtOTRate As System.Windows.Forms.TextBox
    Public WithEvents txtPointRate As System.Windows.Forms.TextBox
    Public WithEvents txtBackRate As System.Windows.Forms.TextBox
    Public WithEvents txtTripRate As System.Windows.Forms.TextBox
    Public WithEvents chkStatus As System.Windows.Forms.CheckBox
    Public WithEvents txtAmendNo As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchWEF As System.Windows.Forms.Button
    Public WithEvents txtCustomerName As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchCustCode As System.Windows.Forms.Button
    Public WithEvents txtCustomerCode As System.Windows.Forms.TextBox
    Public WithEvents txtWEF As System.Windows.Forms.TextBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents lblWEF As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblMKey As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents fraBase As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdAmend As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTripRateMaster))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtPremiumRate = New System.Windows.Forms.TextBox()
        Me.txtOTRate = New System.Windows.Forms.TextBox()
        Me.txtPointRate = New System.Windows.Forms.TextBox()
        Me.txtBackRate = New System.Windows.Forms.TextBox()
        Me.txtTripRate = New System.Windows.Forms.TextBox()
        Me.txtAmendNo = New System.Windows.Forms.TextBox()
        Me.cmdSearchWEF = New System.Windows.Forms.Button()
        Me.txtCustomerName = New System.Windows.Forms.TextBox()
        Me.cmdSearchCustCode = New System.Windows.Forms.Button()
        Me.txtCustomerCode = New System.Windows.Forms.TextBox()
        Me.txtWEF = New System.Windows.Forms.TextBox()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdAmend = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.cmdBillToSearch = New System.Windows.Forms.Button()
        Me.txtDefaultRatePerKG = New System.Windows.Forms.TextBox()
        Me.txtDefaultPickupRate = New System.Windows.Forms.TextBox()
        Me.fraBase = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtBillTo = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.SSTab1 = New System.Windows.Forms.TabControl()
        Me._SSTab1_TabPage0 = New System.Windows.Forms.TabPage()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me._SSTab1_TabPage1 = New System.Windows.Forms.TabPage()
        Me.SprdMain2 = New AxFPSpreadADO.AxfpSpread()
        Me.chkStatus = New System.Windows.Forms.CheckBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblWEF = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblMKey = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.fraBase.SuspendLayout()
        Me.SSTab1.SuspendLayout()
        Me._SSTab1_TabPage0.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me._SSTab1_TabPage1.SuspendLayout()
        CType(Me.SprdMain2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPremiumRate
        '
        Me.txtPremiumRate.AcceptsReturn = True
        Me.txtPremiumRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPremiumRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPremiumRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPremiumRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPremiumRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPremiumRate.Location = New System.Drawing.Point(601, 498)
        Me.txtPremiumRate.MaxLength = 0
        Me.txtPremiumRate.Name = "txtPremiumRate"
        Me.txtPremiumRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPremiumRate.Size = New System.Drawing.Size(65, 20)
        Me.txtPremiumRate.TabIndex = 8
        Me.txtPremiumRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtPremiumRate, "Press F1 For Help")
        '
        'txtOTRate
        '
        Me.txtOTRate.AcceptsReturn = True
        Me.txtOTRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOTRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOTRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOTRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOTRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOTRate.Location = New System.Drawing.Point(374, 524)
        Me.txtOTRate.MaxLength = 0
        Me.txtOTRate.Name = "txtOTRate"
        Me.txtOTRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOTRate.Size = New System.Drawing.Size(65, 20)
        Me.txtOTRate.TabIndex = 10
        Me.txtOTRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtOTRate, "Press F1 For Help")
        '
        'txtPointRate
        '
        Me.txtPointRate.AcceptsReturn = True
        Me.txtPointRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPointRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPointRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPointRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPointRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPointRate.Location = New System.Drawing.Point(374, 498)
        Me.txtPointRate.MaxLength = 0
        Me.txtPointRate.Name = "txtPointRate"
        Me.txtPointRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPointRate.Size = New System.Drawing.Size(65, 20)
        Me.txtPointRate.TabIndex = 7
        Me.txtPointRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtPointRate, "Press F1 For Help")
        '
        'txtBackRate
        '
        Me.txtBackRate.AcceptsReturn = True
        Me.txtBackRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBackRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBackRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBackRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBackRate.Location = New System.Drawing.Point(164, 524)
        Me.txtBackRate.MaxLength = 0
        Me.txtBackRate.Name = "txtBackRate"
        Me.txtBackRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBackRate.Size = New System.Drawing.Size(65, 20)
        Me.txtBackRate.TabIndex = 9
        Me.txtBackRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtBackRate, "Press F1 For Help")
        '
        'txtTripRate
        '
        Me.txtTripRate.AcceptsReturn = True
        Me.txtTripRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtTripRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTripRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTripRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTripRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTripRate.Location = New System.Drawing.Point(164, 498)
        Me.txtTripRate.MaxLength = 0
        Me.txtTripRate.Name = "txtTripRate"
        Me.txtTripRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTripRate.Size = New System.Drawing.Size(65, 20)
        Me.txtTripRate.TabIndex = 6
        Me.txtTripRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtTripRate, "Press F1 For Help")
        '
        'txtAmendNo
        '
        Me.txtAmendNo.AcceptsReturn = True
        Me.txtAmendNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtAmendNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAmendNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAmendNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmendNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAmendNo.Location = New System.Drawing.Point(854, 13)
        Me.txtAmendNo.MaxLength = 0
        Me.txtAmendNo.Name = "txtAmendNo"
        Me.txtAmendNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAmendNo.Size = New System.Drawing.Size(46, 20)
        Me.txtAmendNo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtAmendNo, "Press F1 For Help")
        '
        'cmdSearchWEF
        '
        Me.cmdSearchWEF.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchWEF.Image = CType(resources.GetObject("cmdSearchWEF.Image"), System.Drawing.Image)
        Me.cmdSearchWEF.Location = New System.Drawing.Point(184, 38)
        Me.cmdSearchWEF.Name = "cmdSearchWEF"
        Me.cmdSearchWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchWEF.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchWEF.TabIndex = 5
        Me.cmdSearchWEF.TabStop = False
        Me.cmdSearchWEF.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchWEF, "Search")
        Me.cmdSearchWEF.UseVisualStyleBackColor = False
        '
        'txtCustomerName
        '
        Me.txtCustomerName.AcceptsReturn = True
        Me.txtCustomerName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerName.Location = New System.Drawing.Point(212, 13)
        Me.txtCustomerName.MaxLength = 0
        Me.txtCustomerName.Name = "txtCustomerName"
        Me.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerName.Size = New System.Drawing.Size(353, 20)
        Me.txtCustomerName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtCustomerName, "Press F1 For Help")
        '
        'cmdSearchCustCode
        '
        Me.cmdSearchCustCode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCustCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCustCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCustCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCustCode.Image = CType(resources.GetObject("cmdSearchCustCode.Image"), System.Drawing.Image)
        Me.cmdSearchCustCode.Location = New System.Drawing.Point(184, 13)
        Me.cmdSearchCustCode.Name = "cmdSearchCustCode"
        Me.cmdSearchCustCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCustCode.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchCustCode.TabIndex = 1
        Me.cmdSearchCustCode.TabStop = False
        Me.cmdSearchCustCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCustCode, "Search")
        Me.cmdSearchCustCode.UseVisualStyleBackColor = False
        '
        'txtCustomerCode
        '
        Me.txtCustomerCode.AcceptsReturn = True
        Me.txtCustomerCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCustomerCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCustomerCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomerCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCustomerCode.Location = New System.Drawing.Point(102, 13)
        Me.txtCustomerCode.MaxLength = 0
        Me.txtCustomerCode.Name = "txtCustomerCode"
        Me.txtCustomerCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCustomerCode.Size = New System.Drawing.Size(81, 20)
        Me.txtCustomerCode.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtCustomerCode, "Press F1 For Help")
        '
        'txtWEF
        '
        Me.txtWEF.AcceptsReturn = True
        Me.txtWEF.BackColor = System.Drawing.SystemColors.Window
        Me.txtWEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWEF.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWEF.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtWEF.Location = New System.Drawing.Point(102, 38)
        Me.txtWEF.MaxLength = 0
        Me.txtWEF.Name = "txtWEF"
        Me.txtWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtWEF.Size = New System.Drawing.Size(81, 20)
        Me.txtWEF.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtWEF, "Press F1 For Help")
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(636, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 34)
        Me.CmdClose.TabIndex = 21
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(570, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 34)
        Me.CmdView.TabIndex = 20
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(504, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 34)
        Me.CmdPreview.TabIndex = 19
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Print PO")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(438, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdPrint.TabIndex = 18
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(372, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 34)
        Me.CmdDelete.TabIndex = 17
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(306, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 34)
        Me.cmdSavePrint.TabIndex = 16
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print the Current Bill")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdAmend
        '
        Me.cmdAmend.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAmend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAmend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAmend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAmend.Image = CType(resources.GetObject("cmdAmend.Image"), System.Drawing.Image)
        Me.cmdAmend.Location = New System.Drawing.Point(240, 10)
        Me.cmdAmend.Name = "cmdAmend"
        Me.cmdAmend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAmend.Size = New System.Drawing.Size(67, 34)
        Me.cmdAmend.TabIndex = 28
        Me.cmdAmend.Text = "&Amendment"
        Me.cmdAmend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAmend, "Modify Record")
        Me.cmdAmend.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(174, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 34)
        Me.CmdSave.TabIndex = 15
        Me.CmdSave.Text = "&Save"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'CmdModify
        '
        Me.CmdModify.BackColor = System.Drawing.SystemColors.Control
        Me.CmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdModify.Image = CType(resources.GetObject("CmdModify.Image"), System.Drawing.Image)
        Me.CmdModify.Location = New System.Drawing.Point(108, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 34)
        Me.CmdModify.TabIndex = 14
        Me.CmdModify.Text = "&Modify"
        Me.CmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdModify, "Modify Record")
        Me.CmdModify.UseVisualStyleBackColor = False
        '
        'CmdAdd
        '
        Me.CmdAdd.BackColor = System.Drawing.SystemColors.Control
        Me.CmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAdd.Image = CType(resources.GetObject("CmdAdd.Image"), System.Drawing.Image)
        Me.CmdAdd.Location = New System.Drawing.Point(42, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 34)
        Me.CmdAdd.TabIndex = 13
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'cmdBillToSearch
        '
        Me.cmdBillToSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdBillToSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBillToSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdBillToSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBillToSearch.Image = CType(resources.GetObject("cmdBillToSearch.Image"), System.Drawing.Image)
        Me.cmdBillToSearch.Location = New System.Drawing.Point(753, 10)
        Me.cmdBillToSearch.Name = "cmdBillToSearch"
        Me.cmdBillToSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBillToSearch.Size = New System.Drawing.Size(28, 23)
        Me.cmdBillToSearch.TabIndex = 146
        Me.cmdBillToSearch.TabStop = False
        Me.cmdBillToSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdBillToSearch, "Search")
        Me.cmdBillToSearch.UseVisualStyleBackColor = False
        '
        'txtDefaultRatePerKG
        '
        Me.txtDefaultRatePerKG.AcceptsReturn = True
        Me.txtDefaultRatePerKG.BackColor = System.Drawing.SystemColors.Window
        Me.txtDefaultRatePerKG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDefaultRatePerKG.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDefaultRatePerKG.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultRatePerKG.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDefaultRatePerKG.Location = New System.Drawing.Point(601, 522)
        Me.txtDefaultRatePerKG.MaxLength = 0
        Me.txtDefaultRatePerKG.Name = "txtDefaultRatePerKG"
        Me.txtDefaultRatePerKG.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDefaultRatePerKG.Size = New System.Drawing.Size(65, 20)
        Me.txtDefaultRatePerKG.TabIndex = 147
        Me.txtDefaultRatePerKG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtDefaultRatePerKG, "Press F1 For Help")
        '
        'txtDefaultPickupRate
        '
        Me.txtDefaultPickupRate.AcceptsReturn = True
        Me.txtDefaultPickupRate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDefaultPickupRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDefaultPickupRate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDefaultPickupRate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultPickupRate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDefaultPickupRate.Location = New System.Drawing.Point(783, 496)
        Me.txtDefaultPickupRate.MaxLength = 0
        Me.txtDefaultPickupRate.Name = "txtDefaultPickupRate"
        Me.txtDefaultPickupRate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDefaultPickupRate.Size = New System.Drawing.Size(65, 20)
        Me.txtDefaultPickupRate.TabIndex = 149
        Me.txtDefaultPickupRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtDefaultPickupRate, "Press F1 For Help")
        '
        'fraBase
        '
        Me.fraBase.BackColor = System.Drawing.SystemColors.Control
        Me.fraBase.Controls.Add(Me.txtDefaultPickupRate)
        Me.fraBase.Controls.Add(Me.Label12)
        Me.fraBase.Controls.Add(Me.txtDefaultRatePerKG)
        Me.fraBase.Controls.Add(Me.Label10)
        Me.fraBase.Controls.Add(Me.cmdBillToSearch)
        Me.fraBase.Controls.Add(Me.txtBillTo)
        Me.fraBase.Controls.Add(Me.Label37)
        Me.fraBase.Controls.Add(Me.SSTab1)
        Me.fraBase.Controls.Add(Me.txtPremiumRate)
        Me.fraBase.Controls.Add(Me.txtOTRate)
        Me.fraBase.Controls.Add(Me.txtPointRate)
        Me.fraBase.Controls.Add(Me.txtBackRate)
        Me.fraBase.Controls.Add(Me.txtTripRate)
        Me.fraBase.Controls.Add(Me.chkStatus)
        Me.fraBase.Controls.Add(Me.txtAmendNo)
        Me.fraBase.Controls.Add(Me.txtRemarks)
        Me.fraBase.Controls.Add(Me.cmdSearchWEF)
        Me.fraBase.Controls.Add(Me.txtCustomerName)
        Me.fraBase.Controls.Add(Me.cmdSearchCustCode)
        Me.fraBase.Controls.Add(Me.txtCustomerCode)
        Me.fraBase.Controls.Add(Me.txtWEF)
        Me.fraBase.Controls.Add(Me.Label8)
        Me.fraBase.Controls.Add(Me.Label7)
        Me.fraBase.Controls.Add(Me.Label4)
        Me.fraBase.Controls.Add(Me.Label3)
        Me.fraBase.Controls.Add(Me.lblWEF)
        Me.fraBase.Controls.Add(Me.Label2)
        Me.fraBase.Controls.Add(Me.lblMKey)
        Me.fraBase.Controls.Add(Me.Label6)
        Me.fraBase.Controls.Add(Me.Label5)
        Me.fraBase.Controls.Add(Me.Label1)
        Me.fraBase.Controls.Add(Me.Label9)
        Me.fraBase.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBase.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBase.Location = New System.Drawing.Point(0, -4)
        Me.fraBase.Name = "fraBase"
        Me.fraBase.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBase.Size = New System.Drawing.Size(904, 579)
        Me.fraBase.TabIndex = 23
        Me.fraBase.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(673, 499)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(110, 14)
        Me.Label12.TabIndex = 151
        Me.Label12.Text = "Default Pick Up Rate :"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(503, 525)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(94, 14)
        Me.Label10.TabIndex = 148
        Me.Label10.Text = "Default Rate / Kg :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBillTo
        '
        Me.txtBillTo.AcceptsReturn = True
        Me.txtBillTo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBillTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBillTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBillTo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillTo.ForeColor = System.Drawing.Color.Blue
        Me.txtBillTo.Location = New System.Drawing.Point(631, 12)
        Me.txtBillTo.MaxLength = 0
        Me.txtBillTo.Name = "txtBillTo"
        Me.txtBillTo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBillTo.Size = New System.Drawing.Size(121, 22)
        Me.txtBillTo.TabIndex = 144
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.BackColor = System.Drawing.SystemColors.Control
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(571, 17)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(56, 13)
        Me.Label37.TabIndex = 145
        Me.Label37.Text = "Location :"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SSTab1
        '
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage0)
        Me.SSTab1.Controls.Add(Me._SSTab1_TabPage1)
        Me.SSTab1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SSTab1.ItemSize = New System.Drawing.Size(42, 18)
        Me.SSTab1.Location = New System.Drawing.Point(2, 62)
        Me.SSTab1.Name = "SSTab1"
        Me.SSTab1.SelectedIndex = 0
        Me.SSTab1.Size = New System.Drawing.Size(898, 429)
        Me.SSTab1.TabIndex = 37
        '
        '_SSTab1_TabPage0
        '
        Me._SSTab1_TabPage0.Controls.Add(Me.SprdMain)
        Me._SSTab1_TabPage0.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage0.Name = "_SSTab1_TabPage0"
        Me._SSTab1_TabPage0.Size = New System.Drawing.Size(890, 403)
        Me._SSTab1_TabPage0.TabIndex = 0
        Me._SSTab1_TabPage0.Text = "Vehicle No Wise"
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 0)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(890, 403)
        Me.SprdMain.TabIndex = 38
        '
        '_SSTab1_TabPage1
        '
        Me._SSTab1_TabPage1.Controls.Add(Me.SprdMain2)
        Me._SSTab1_TabPage1.Location = New System.Drawing.Point(4, 22)
        Me._SSTab1_TabPage1.Name = "_SSTab1_TabPage1"
        Me._SSTab1_TabPage1.Size = New System.Drawing.Size(890, 403)
        Me._SSTab1_TabPage1.TabIndex = 1
        Me._SSTab1_TabPage1.Text = "Transport && Capacity Wise"
        '
        'SprdMain2
        '
        Me.SprdMain2.DataSource = Nothing
        Me.SprdMain2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain2.Location = New System.Drawing.Point(0, 0)
        Me.SprdMain2.Name = "SprdMain2"
        Me.SprdMain2.OcxState = CType(resources.GetObject("SprdMain2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain2.Size = New System.Drawing.Size(890, 403)
        Me.SprdMain2.TabIndex = 39
        '
        'chkStatus
        '
        Me.chkStatus.AutoSize = True
        Me.chkStatus.BackColor = System.Drawing.SystemColors.Control
        Me.chkStatus.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkStatus.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkStatus.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkStatus.Location = New System.Drawing.Point(406, 40)
        Me.chkStatus.Name = "chkStatus"
        Me.chkStatus.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkStatus.Size = New System.Drawing.Size(136, 18)
        Me.chkStatus.TabIndex = 12
        Me.chkStatus.Text = "Status (Open / Closed)"
        Me.chkStatus.UseVisualStyleBackColor = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(164, 550)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(401, 19)
        Me.txtRemarks.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(492, 501)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(105, 14)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Premium Rate / Trip :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(271, 528)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(99, 14)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "Default Over Time :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(272, 501)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(98, 14)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Default Point Rate :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(3, 528)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(157, 14)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Default Drop Down Rate / Trip :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblWEF
        '
        Me.lblWEF.AutoSize = True
        Me.lblWEF.BackColor = System.Drawing.SystemColors.Control
        Me.lblWEF.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblWEF.Enabled = False
        Me.lblWEF.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWEF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWEF.Location = New System.Drawing.Point(344, 40)
        Me.lblWEF.Name = "lblWEF"
        Me.lblWEF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblWEF.Size = New System.Drawing.Size(39, 14)
        Me.lblWEF.TabIndex = 32
        Me.lblWEF.Text = "lblWEF"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(61, 501)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(99, 14)
        Me.Label2.TabIndex = 31
        Me.Label2.Text = "Default Rate / Trip :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMKey
        '
        Me.lblMKey.AutoSize = True
        Me.lblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMKey.Enabled = False
        Me.lblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMKey.Location = New System.Drawing.Point(232, 40)
        Me.lblMKey.Name = "lblMKey"
        Me.lblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMKey.Size = New System.Drawing.Size(44, 14)
        Me.lblMKey.TabIndex = 30
        Me.lblMKey.Text = "lblMKey"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(783, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(63, 14)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Amend No :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(105, 554)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(55, 14)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Remarks :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(2, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(87, 14)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Customer Code :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(50, 41)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(42, 14)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "W.E.F. :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(904, 575)
        Me.SprdView.TabIndex = 24
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.CmdDelete)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.cmdAmend)
        Me.Frame3.Controls.Add(Me.CmdSave)
        Me.Frame3.Controls.Add(Me.CmdModify)
        Me.Frame3.Controls.Add(Me.CmdAdd)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 573)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(908, 47)
        Me.Frame3.TabIndex = 22
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(592, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 22
        '
        'frmTripRateMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(910, 621)
        Me.Controls.Add(Me.fraBase)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(4, 23)
        Me.MaximizeBox = False
        Me.Name = "frmTripRateMaster"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Customer Wise Trip Rate Master"
        Me.fraBase.ResumeLayout(False)
        Me.fraBase.PerformLayout()
        Me.SSTab1.ResumeLayout(False)
        Me._SSTab1_TabPage0.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me._SSTab1_TabPage1.ResumeLayout(False)
        CType(Me.SprdMain2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub

    Public WithEvents txtBillTo As TextBox
    Public WithEvents Label37 As Label
    Public WithEvents txtDefaultPickupRate As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents txtDefaultRatePerKG As TextBox
    Public WithEvents Label10 As Label
    Public WithEvents cmdBillToSearch As Button
#End Region
End Class