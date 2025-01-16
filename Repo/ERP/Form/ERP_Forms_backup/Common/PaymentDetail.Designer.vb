Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPaymentDetail
#Region "Windows Form Designer generated code "
    <System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
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
    Public WithEvents lblDC As System.Windows.Forms.Label
    Public WithEvents lblAmount As System.Windows.Forms.Label
    Public WithEvents Amount As System.Windows.Forms.Label
    Public WithEvents lblAccountName As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents _optShow_2 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optShow_0 As System.Windows.Forms.RadioButton
    Public WithEvents txtDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox

    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents ADataMain As VB6.ADODC
    Public WithEvents cmdToken As System.Windows.Forms.Button
    Public WithEvents CmdPopFromFile As System.Windows.Forms.Button
    Public WithEvents cmdAppendDetail As System.Windows.Forms.Button
    Public WithEvents cmdPopulate As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents lblTempProcessKey As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents lblDivisionCode As System.Windows.Forms.Label
    Public WithEvents lblDiffDC As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LblCr As System.Windows.Forms.Label
    Public WithEvents LblNetAmt As System.Windows.Forms.Label
    Public WithEvents LblNet As System.Windows.Forms.Label
    Public WithEvents LblCrAmt As System.Windows.Forms.Label
    Public WithEvents LblDrAmt As System.Windows.Forms.Label
    Public WithEvents LblDr As System.Windows.Forms.Label
    Public WithEvents LblTotal As System.Windows.Forms.Label
    Public WithEvents lblNetDC As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents lblDiffAmt As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public CommonDialogOpen As System.Windows.Forms.OpenFileDialog
    Public CommonDialogSave As System.Windows.Forms.SaveFileDialog
    Public CommonDialogFont As System.Windows.Forms.FontDialog
    Public CommonDialogColor As System.Windows.Forms.ColorDialog
    Public CommonDialogPrint As System.Windows.Forms.PrintDialog
    Public WithEvents lblTrnRowNo As System.Windows.Forms.Label
    Public WithEvents lblCostCName As System.Windows.Forms.Label
    Public WithEvents lblCostCCode As System.Windows.Forms.Label
    Public WithEvents lblAccountCode As System.Windows.Forms.Label
    Public WithEvents lblADDMode As System.Windows.Forms.Label
    Public WithEvents lblModifyMode As System.Windows.Forms.Label
    Public WithEvents lblBillNo As System.Windows.Forms.Label
    Public WithEvents lblBillYear As System.Windows.Forms.Label
    Public WithEvents lblVDate As System.Windows.Forms.Label
    Public WithEvents lblNarration As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents optShow As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPaymentDetail))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.lblDC = New System.Windows.Forms.Label()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.Amount = New System.Windows.Forms.Label()
        Me.lblAccountName = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDefaultCompanyName = New System.Windows.Forms.TextBox()
        Me._optShow_2 = New System.Windows.Forms.RadioButton()
        Me._optShow_1 = New System.Windows.Forms.RadioButton()
        Me._optShow_0 = New System.Windows.Forms.RadioButton()
        Me.txtDate = New System.Windows.Forms.MaskedTextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.ADataMain = New Microsoft.VisualBasic.Compatibility.VB6.ADODC()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.optAsPerMRR = New System.Windows.Forms.RadioButton()
        Me.optAsPerBill = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtBillSearchFrom = New System.Windows.Forms.MaskedTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdToken = New System.Windows.Forms.Button()
        Me.CmdPopFromFile = New System.Windows.Forms.Button()
        Me.cmdAppendDetail = New System.Windows.Forms.Button()
        Me.cmdPopulate = New System.Windows.Forms.Button()
        Me.lblDiffDC = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LblCr = New System.Windows.Forms.Label()
        Me.LblNetAmt = New System.Windows.Forms.Label()
        Me.LblNet = New System.Windows.Forms.Label()
        Me.LblCrAmt = New System.Windows.Forms.Label()
        Me.LblDrAmt = New System.Windows.Forms.Label()
        Me.LblDr = New System.Windows.Forms.Label()
        Me.LblTotal = New System.Windows.Forms.Label()
        Me.lblNetDC = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblDiffAmt = New System.Windows.Forms.Label()
        Me.lblTempProcessKey = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.lblDivisionCode = New System.Windows.Forms.Label()
        Me.CommonDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialogFont = New System.Windows.Forms.FontDialog()
        Me.CommonDialogColor = New System.Windows.Forms.ColorDialog()
        Me.CommonDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.lblTrnRowNo = New System.Windows.Forms.Label()
        Me.lblCostCName = New System.Windows.Forms.Label()
        Me.lblCostCCode = New System.Windows.Forms.Label()
        Me.lblAccountCode = New System.Windows.Forms.Label()
        Me.lblADDMode = New System.Windows.Forms.Label()
        Me.lblModifyMode = New System.Windows.Forms.Label()
        Me.lblBillNo = New System.Windows.Forms.Label()
        Me.lblBillYear = New System.Windows.Forms.Label()
        Me.lblVDate = New System.Windows.Forms.Label()
        Me.lblNarration = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.optShow = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.lblVoucherAmount = New System.Windows.Forms.Label()
        Me.lblVoucherDC = New System.Windows.Forms.Label()
        Me.Frame1.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdCancel.Location = New System.Drawing.Point(64, 48)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(60, 34)
        Me.cmdCancel.TabIndex = 32
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdCancel, "Close the form")
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Image = CType(resources.GetObject("cmdOk.Image"), System.Drawing.Image)
        Me.cmdOk.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.cmdOk.Location = New System.Drawing.Point(4, 48)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(60, 34)
        Me.cmdOk.TabIndex = 31
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdOk, "Save Voucher")
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.lblDC)
        Me.Frame1.Controls.Add(Me.lblAmount)
        Me.Frame1.Controls.Add(Me.Amount)
        Me.Frame1.Controls.Add(Me.lblAccountName)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, -6)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(1033, 36)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        '
        'lblDC
        '
        Me.lblDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDC.Location = New System.Drawing.Point(704, 11)
        Me.lblDC.Name = "lblDC"
        Me.lblDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDC.Size = New System.Drawing.Size(25, 20)
        Me.lblDC.TabIndex = 6
        Me.lblDC.Text = "lblDC"
        '
        'lblAmount
        '
        Me.lblAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAmount.Location = New System.Drawing.Point(586, 11)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAmount.Size = New System.Drawing.Size(118, 20)
        Me.lblAmount.TabIndex = 5
        Me.lblAmount.Text = "lblAmount"
        Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Amount
        '
        Me.Amount.AutoSize = True
        Me.Amount.BackColor = System.Drawing.SystemColors.Control
        Me.Amount.Cursor = System.Windows.Forms.Cursors.Default
        Me.Amount.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Amount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Amount.Location = New System.Drawing.Point(533, 12)
        Me.Amount.Name = "Amount"
        Me.Amount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Amount.Size = New System.Drawing.Size(50, 14)
        Me.Amount.TabIndex = 4
        Me.Amount.Text = "Amount :"
        '
        'lblAccountName
        '
        Me.lblAccountName.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccountName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAccountName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAccountName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAccountName.Location = New System.Drawing.Point(101, 11)
        Me.lblAccountName.Name = "lblAccountName"
        Me.lblAccountName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccountName.Size = New System.Drawing.Size(328, 20)
        Me.lblAccountName.TabIndex = 3
        Me.lblAccountName.Text = "lblAccountName"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(6, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(84, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Account Name :"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.Controls.Add(Me.txtDefaultCompanyName)
        Me.Frame4.Controls.Add(Me._optShow_2)
        Me.Frame4.Controls.Add(Me._optShow_1)
        Me.Frame4.Controls.Add(Me._optShow_0)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 24)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(1033, 33)
        Me.Frame4.TabIndex = 36
        Me.Frame4.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(370, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(127, 14)
        Me.Label4.TabIndex = 51
        Me.Label4.Text = "Default Compamy Name :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtDefaultCompanyName
        '
        Me.txtDefaultCompanyName.AcceptsReturn = True
        Me.txtDefaultCompanyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtDefaultCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDefaultCompanyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDefaultCompanyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDefaultCompanyName.Location = New System.Drawing.Point(502, 12)
        Me.txtDefaultCompanyName.MaxLength = 0
        Me.txtDefaultCompanyName.Name = "txtDefaultCompanyName"
        Me.txtDefaultCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDefaultCompanyName.Size = New System.Drawing.Size(366, 20)
        Me.txtDefaultCompanyName.TabIndex = 48
        '
        '_optShow_2
        '
        Me._optShow_2.AutoSize = True
        Me._optShow_2.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_2.Checked = True
        Me._optShow_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_2, CType(2, Short))
        Me._optShow_2.Location = New System.Drawing.Point(140, 12)
        Me._optShow_2.Name = "_optShow_2"
        Me._optShow_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_2.Size = New System.Drawing.Size(130, 18)
        Me._optShow_2.TabIndex = 40
        Me._optShow_2.TabStop = True
        Me._optShow_2.Text = "Check All MRR As On "
        Me._optShow_2.UseVisualStyleBackColor = False
        '
        '_optShow_1
        '
        Me._optShow_1.AutoSize = True
        Me._optShow_1.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_1, CType(1, Short))
        Me._optShow_1.Location = New System.Drawing.Point(280, 12)
        Me._optShow_1.Name = "_optShow_1"
        Me._optShow_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_1.Size = New System.Drawing.Size(50, 18)
        Me._optShow_1.TabIndex = 38
        Me._optShow_1.TabStop = True
        Me._optShow_1.Text = "None"
        Me._optShow_1.UseVisualStyleBackColor = False
        '
        '_optShow_0
        '
        Me._optShow_0.AutoSize = True
        Me._optShow_0.BackColor = System.Drawing.SystemColors.Control
        Me._optShow_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optShow_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optShow_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optShow.SetIndex(Me._optShow_0, CType(0, Short))
        Me._optShow_0.Location = New System.Drawing.Point(9, 12)
        Me._optShow_0.Name = "_optShow_0"
        Me._optShow_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optShow_0.Size = New System.Drawing.Size(121, 18)
        Me._optShow_0.TabIndex = 37
        Me._optShow_0.TabStop = True
        Me._optShow_0.Text = "Check All Bill As On "
        Me._optShow_0.UseVisualStyleBackColor = False
        '
        'txtDate
        '
        Me.txtDate.AllowPromptAsInput = False
        Me.txtDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDate.Location = New System.Drawing.Point(453, 14)
        Me.txtDate.Mask = "##/##/####"
        Me.txtDate.Name = "txtDate"
        Me.txtDate.Size = New System.Drawing.Size(81, 20)
        Me.txtDate.TabIndex = 39
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.SprdMain)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 52)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(1033, 351)
        Me.Frame2.TabIndex = 1
        Me.Frame2.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SprdMain.Location = New System.Drawing.Point(0, 13)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(1033, 338)
        Me.SprdMain.TabIndex = 34
        '
        'ADataMain
        '
        Me.ADataMain.BackColor = System.Drawing.SystemColors.Window
        Me.ADataMain.CommandTimeout = 0
        Me.ADataMain.CommandType = ADODB.CommandTypeEnum.adCmdUnknown
        Me.ADataMain.ConnectionString = Nothing
        Me.ADataMain.CursorType = ADODB.CursorTypeEnum.adOpenStatic
        Me.ADataMain.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ADataMain.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ADataMain.Location = New System.Drawing.Point(0, 52)
        Me.ADataMain.LockType = ADODB.LockTypeEnum.adLockOptimistic
        Me.ADataMain.Name = "ADataMain"
        Me.ADataMain.Size = New System.Drawing.Size(231, 39)
        Me.ADataMain.TabIndex = 42
        Me.ADataMain.Text = "Adodc1"
        Me.ADataMain.Visible = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.optAsPerMRR)
        Me.Frame3.Controls.Add(Me.optAsPerBill)
        Me.Frame3.Controls.Add(Me.Label7)
        Me.Frame3.Controls.Add(Me.Label6)
        Me.Frame3.Controls.Add(Me.txtBillSearchFrom)
        Me.Frame3.Controls.Add(Me.Label5)
        Me.Frame3.Controls.Add(Me.cmdToken)
        Me.Frame3.Controls.Add(Me.CmdPopFromFile)
        Me.Frame3.Controls.Add(Me.txtDate)
        Me.Frame3.Controls.Add(Me.cmdAppendDetail)
        Me.Frame3.Controls.Add(Me.cmdPopulate)
        Me.Frame3.Controls.Add(Me.cmdCancel)
        Me.Frame3.Controls.Add(Me.cmdOk)
        Me.Frame3.Controls.Add(Me.lblDiffDC)
        Me.Frame3.Controls.Add(Me.Label3)
        Me.Frame3.Controls.Add(Me.LblCr)
        Me.Frame3.Controls.Add(Me.LblNetAmt)
        Me.Frame3.Controls.Add(Me.LblNet)
        Me.Frame3.Controls.Add(Me.LblCrAmt)
        Me.Frame3.Controls.Add(Me.LblDrAmt)
        Me.Frame3.Controls.Add(Me.LblDr)
        Me.Frame3.Controls.Add(Me.LblTotal)
        Me.Frame3.Controls.Add(Me.lblNetDC)
        Me.Frame3.Controls.Add(Me.Label2)
        Me.Frame3.Controls.Add(Me.lblDiffAmt)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 399)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(733, 84)
        Me.Frame3.TabIndex = 17
        Me.Frame3.TabStop = False
        '
        'optAsPerMRR
        '
        Me.optAsPerMRR.AutoSize = True
        Me.optAsPerMRR.BackColor = System.Drawing.SystemColors.Control
        Me.optAsPerMRR.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAsPerMRR.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAsPerMRR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAsPerMRR.Location = New System.Drawing.Point(210, 16)
        Me.optAsPerMRR.Name = "optAsPerMRR"
        Me.optAsPerMRR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAsPerMRR.Size = New System.Drawing.Size(72, 18)
        Me.optAsPerMRR.TabIndex = 72
        Me.optAsPerMRR.Text = "MRR Date"
        Me.optAsPerMRR.UseVisualStyleBackColor = False
        '
        'optAsPerBill
        '
        Me.optAsPerBill.AutoSize = True
        Me.optAsPerBill.BackColor = System.Drawing.SystemColors.Control
        Me.optAsPerBill.Checked = True
        Me.optAsPerBill.Cursor = System.Windows.Forms.Cursors.Default
        Me.optAsPerBill.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optAsPerBill.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAsPerBill.Location = New System.Drawing.Point(128, 16)
        Me.optAsPerBill.Name = "optAsPerBill"
        Me.optAsPerBill.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optAsPerBill.Size = New System.Drawing.Size(63, 18)
        Me.optAsPerBill.TabIndex = 71
        Me.optAsPerBill.TabStop = True
        Me.optAsPerBill.Text = "Bill Date"
        Me.optAsPerBill.UseVisualStyleBackColor = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(105, 14)
        Me.Label7.TabIndex = 70
        Me.Label7.Text = "Bill Populate As Per :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(426, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(27, 14)
        Me.Label6.TabIndex = 69
        Me.Label6.Text = "TO :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtBillSearchFrom
        '
        Me.txtBillSearchFrom.AllowPromptAsInput = False
        Me.txtBillSearchFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBillSearchFrom.Location = New System.Drawing.Point(342, 14)
        Me.txtBillSearchFrom.Mask = "##/##/####"
        Me.txtBillSearchFrom.Name = "txtBillSearchFrom"
        Me.txtBillSearchFrom.Size = New System.Drawing.Size(81, 20)
        Me.txtBillSearchFrom.TabIndex = 68
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(301, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(37, 14)
        Me.Label5.TabIndex = 66
        Me.Label5.Text = "From :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdToken
        '
        Me.cmdToken.BackColor = System.Drawing.SystemColors.Control
        Me.cmdToken.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdToken.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdToken.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdToken.Location = New System.Drawing.Point(462, 41)
        Me.cmdToken.Name = "cmdToken"
        Me.cmdToken.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdToken.Size = New System.Drawing.Size(76, 41)
        Me.cmdToken.TabIndex = 65
        Me.cmdToken.Text = "Populate From Token"
        Me.cmdToken.UseVisualStyleBackColor = False
        '
        'CmdPopFromFile
        '
        Me.CmdPopFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPopFromFile.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPopFromFile.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPopFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPopFromFile.Location = New System.Drawing.Point(390, 41)
        Me.CmdPopFromFile.Name = "CmdPopFromFile"
        Me.CmdPopFromFile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPopFromFile.Size = New System.Drawing.Size(72, 41)
        Me.CmdPopFromFile.TabIndex = 61
        Me.CmdPopFromFile.Text = "Populate From File"
        Me.CmdPopFromFile.UseVisualStyleBackColor = False
        '
        'cmdAppendDetail
        '
        Me.cmdAppendDetail.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAppendDetail.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAppendDetail.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAppendDetail.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAppendDetail.Location = New System.Drawing.Point(253, 41)
        Me.cmdAppendDetail.Name = "cmdAppendDetail"
        Me.cmdAppendDetail.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAppendDetail.Size = New System.Drawing.Size(70, 41)
        Me.cmdAppendDetail.TabIndex = 35
        Me.cmdAppendDetail.Text = "Append  Bill Detail"
        Me.cmdAppendDetail.UseVisualStyleBackColor = False
        '
        'cmdPopulate
        '
        Me.cmdPopulate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPopulate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPopulate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPopulate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPopulate.Location = New System.Drawing.Point(181, 41)
        Me.cmdPopulate.Name = "cmdPopulate"
        Me.cmdPopulate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPopulate.Size = New System.Drawing.Size(72, 41)
        Me.cmdPopulate.TabIndex = 33
        Me.cmdPopulate.Text = "Populate Bill Detail"
        Me.cmdPopulate.UseVisualStyleBackColor = False
        '
        'lblDiffDC
        '
        Me.lblDiffDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiffDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiffDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiffDC.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiffDC.ForeColor = System.Drawing.Color.Black
        Me.lblDiffDC.Location = New System.Drawing.Point(708, 63)
        Me.lblDiffDC.Name = "lblDiffDC"
        Me.lblDiffDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiffDC.Size = New System.Drawing.Size(22, 17)
        Me.lblDiffDC.TabIndex = 30
        Me.lblDiffDC.Text = "Cr"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(572, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(41, 14)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Credit :"
        '
        'LblCr
        '
        Me.LblCr.BackColor = System.Drawing.SystemColors.Control
        Me.LblCr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCr.ForeColor = System.Drawing.Color.Black
        Me.LblCr.Location = New System.Drawing.Point(708, 9)
        Me.LblCr.Name = "LblCr"
        Me.LblCr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCr.Size = New System.Drawing.Size(22, 17)
        Me.LblCr.TabIndex = 27
        Me.LblCr.Text = "Cr"
        '
        'LblNetAmt
        '
        Me.LblNetAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblNetAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblNetAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNetAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblNetAmt.Location = New System.Drawing.Point(615, 45)
        Me.LblNetAmt.Name = "LblNetAmt"
        Me.LblNetAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNetAmt.Size = New System.Drawing.Size(92, 17)
        Me.LblNetAmt.TabIndex = 26
        Me.LblNetAmt.Text = "LblNetAmt"
        Me.LblNetAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblNet
        '
        Me.LblNet.AutoSize = True
        Me.LblNet.BackColor = System.Drawing.SystemColors.Control
        Me.LblNet.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblNet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNet.ForeColor = System.Drawing.Color.Black
        Me.LblNet.Location = New System.Drawing.Point(571, 47)
        Me.LblNet.Name = "LblNet"
        Me.LblNet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblNet.Size = New System.Drawing.Size(29, 14)
        Me.LblNet.TabIndex = 25
        Me.LblNet.Text = "Net :"
        Me.LblNet.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblCrAmt
        '
        Me.LblCrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblCrAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblCrAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblCrAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCrAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblCrAmt.Location = New System.Drawing.Point(615, 9)
        Me.LblCrAmt.Name = "LblCrAmt"
        Me.LblCrAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblCrAmt.Size = New System.Drawing.Size(92, 17)
        Me.LblCrAmt.TabIndex = 24
        Me.LblCrAmt.Text = "LblCrAmt"
        Me.LblCrAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblDrAmt
        '
        Me.LblDrAmt.BackColor = System.Drawing.SystemColors.Control
        Me.LblDrAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblDrAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDrAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDrAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDrAmt.Location = New System.Drawing.Point(615, 27)
        Me.LblDrAmt.Name = "LblDrAmt"
        Me.LblDrAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDrAmt.Size = New System.Drawing.Size(92, 17)
        Me.LblDrAmt.TabIndex = 23
        Me.LblDrAmt.Text = "LblDrAmt"
        Me.LblDrAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblDr
        '
        Me.LblDr.BackColor = System.Drawing.SystemColors.Control
        Me.LblDr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblDr.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblDr.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDr.ForeColor = System.Drawing.Color.Black
        Me.LblDr.Location = New System.Drawing.Point(708, 27)
        Me.LblDr.Name = "LblDr"
        Me.LblDr.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblDr.Size = New System.Drawing.Size(22, 17)
        Me.LblDr.TabIndex = 22
        Me.LblDr.Text = "Dr"
        '
        'LblTotal
        '
        Me.LblTotal.AutoSize = True
        Me.LblTotal.BackColor = System.Drawing.SystemColors.Control
        Me.LblTotal.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTotal.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotal.ForeColor = System.Drawing.Color.Black
        Me.LblTotal.Location = New System.Drawing.Point(573, 27)
        Me.LblTotal.Name = "LblTotal"
        Me.LblTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTotal.Size = New System.Drawing.Size(37, 14)
        Me.LblTotal.TabIndex = 21
        Me.LblTotal.Text = "Debit :"
        '
        'lblNetDC
        '
        Me.lblNetDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblNetDC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNetDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNetDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetDC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblNetDC.Location = New System.Drawing.Point(708, 45)
        Me.lblNetDC.Name = "lblNetDC"
        Me.lblNetDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNetDC.Size = New System.Drawing.Size(22, 17)
        Me.lblNetDC.TabIndex = 20
        Me.lblNetDC.Text = "lblNetDC"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(572, 65)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(30, 14)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "Diff :"
        '
        'lblDiffAmt
        '
        Me.lblDiffAmt.BackColor = System.Drawing.SystemColors.Control
        Me.lblDiffAmt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiffAmt.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDiffAmt.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiffAmt.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDiffAmt.Location = New System.Drawing.Point(615, 63)
        Me.lblDiffAmt.Name = "lblDiffAmt"
        Me.lblDiffAmt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDiffAmt.Size = New System.Drawing.Size(92, 17)
        Me.lblDiffAmt.TabIndex = 18
        Me.lblDiffAmt.Text = "lblDiffAmt"
        Me.lblDiffAmt.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTempProcessKey
        '
        Me.lblTempProcessKey.AutoSize = True
        Me.lblTempProcessKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblTempProcessKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTempProcessKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTempProcessKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTempProcessKey.Location = New System.Drawing.Point(758, 416)
        Me.lblTempProcessKey.Name = "lblTempProcessKey"
        Me.lblTempProcessKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTempProcessKey.Size = New System.Drawing.Size(101, 14)
        Me.lblTempProcessKey.TabIndex = 64
        Me.lblTempProcessKey.Text = "lblTempProcessKey"
        Me.lblTempProcessKey.Visible = False
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(760, 457)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(79, 19)
        Me.lblMkey.TabIndex = 63
        '
        'lblDivisionCode
        '
        Me.lblDivisionCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblDivisionCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDivisionCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDivisionCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDivisionCode.Location = New System.Drawing.Point(762, 437)
        Me.lblDivisionCode.Name = "lblDivisionCode"
        Me.lblDivisionCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDivisionCode.Size = New System.Drawing.Size(73, 15)
        Me.lblDivisionCode.TabIndex = 62
        Me.lblDivisionCode.Text = "lblDivisionCode"
        Me.lblDivisionCode.Visible = False
        '
        'lblTrnRowNo
        '
        Me.lblTrnRowNo.AutoSize = True
        Me.lblTrnRowNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTrnRowNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTrnRowNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTrnRowNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTrnRowNo.Location = New System.Drawing.Point(436, 0)
        Me.lblTrnRowNo.Name = "lblTrnRowNo"
        Me.lblTrnRowNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTrnRowNo.Size = New System.Drawing.Size(69, 14)
        Me.lblTrnRowNo.TabIndex = 28
        Me.lblTrnRowNo.Text = "lblTrnRowNo"
        '
        'lblCostCName
        '
        Me.lblCostCName.BackColor = System.Drawing.SystemColors.Control
        Me.lblCostCName.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCostCName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCostCName.Location = New System.Drawing.Point(146, 18)
        Me.lblCostCName.Name = "lblCostCName"
        Me.lblCostCName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCostCName.Size = New System.Drawing.Size(73, 15)
        Me.lblCostCName.TabIndex = 16
        Me.lblCostCName.Text = "lblCostCName"
        Me.lblCostCName.Visible = False
        '
        'lblCostCCode
        '
        Me.lblCostCCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblCostCCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCostCCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCostCCode.Location = New System.Drawing.Point(68, 18)
        Me.lblCostCCode.Name = "lblCostCCode"
        Me.lblCostCCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCostCCode.Size = New System.Drawing.Size(73, 15)
        Me.lblCostCCode.TabIndex = 15
        Me.lblCostCCode.Text = "lblCostCCode"
        Me.lblCostCCode.Visible = False
        '
        'lblAccountCode
        '
        Me.lblAccountCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblAccountCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAccountCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccountCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAccountCode.Location = New System.Drawing.Point(0, 18)
        Me.lblAccountCode.Name = "lblAccountCode"
        Me.lblAccountCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAccountCode.Size = New System.Drawing.Size(64, 16)
        Me.lblAccountCode.TabIndex = 14
        Me.lblAccountCode.Text = "lblAccountCode"
        Me.lblAccountCode.Visible = False
        '
        'lblADDMode
        '
        Me.lblADDMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblADDMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblADDMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblADDMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblADDMode.Location = New System.Drawing.Point(285, 0)
        Me.lblADDMode.Name = "lblADDMode"
        Me.lblADDMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblADDMode.Size = New System.Drawing.Size(67, 13)
        Me.lblADDMode.TabIndex = 13
        Me.lblADDMode.Text = "lblADDMode"
        Me.lblADDMode.Visible = False
        '
        'lblModifyMode
        '
        Me.lblModifyMode.BackColor = System.Drawing.SystemColors.Control
        Me.lblModifyMode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblModifyMode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModifyMode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblModifyMode.Location = New System.Drawing.Point(355, 0)
        Me.lblModifyMode.Name = "lblModifyMode"
        Me.lblModifyMode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblModifyMode.Size = New System.Drawing.Size(73, 13)
        Me.lblModifyMode.TabIndex = 12
        Me.lblModifyMode.Text = "lblModifyMode"
        Me.lblModifyMode.Visible = False
        '
        'lblBillNo
        '
        Me.lblBillNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillNo.Location = New System.Drawing.Point(52, -1)
        Me.lblBillNo.Name = "lblBillNo"
        Me.lblBillNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillNo.Size = New System.Drawing.Size(47, 17)
        Me.lblBillNo.TabIndex = 11
        Me.lblBillNo.Text = "lblBilNo"
        Me.lblBillNo.Visible = False
        '
        'lblBillYear
        '
        Me.lblBillYear.BackColor = System.Drawing.SystemColors.Control
        Me.lblBillYear.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBillYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBillYear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBillYear.Location = New System.Drawing.Point(104, -1)
        Me.lblBillYear.Name = "lblBillYear"
        Me.lblBillYear.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBillYear.Size = New System.Drawing.Size(51, 17)
        Me.lblBillYear.TabIndex = 10
        Me.lblBillYear.Text = "lblBillYear"
        Me.lblBillYear.Visible = False
        '
        'lblVDate
        '
        Me.lblVDate.BackColor = System.Drawing.SystemColors.Control
        Me.lblVDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVDate.Location = New System.Drawing.Point(226, -1)
        Me.lblVDate.Name = "lblVDate"
        Me.lblVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVDate.Size = New System.Drawing.Size(55, 17)
        Me.lblVDate.TabIndex = 9
        Me.lblVDate.Text = "lblVDate"
        Me.lblVDate.Visible = False
        '
        'lblNarration
        '
        Me.lblNarration.BackColor = System.Drawing.SystemColors.Control
        Me.lblNarration.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblNarration.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNarration.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNarration.Location = New System.Drawing.Point(162, -1)
        Me.lblNarration.Name = "lblNarration"
        Me.lblNarration.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblNarration.Size = New System.Drawing.Size(59, 17)
        Me.lblNarration.TabIndex = 8
        Me.lblNarration.Text = "lblNarration"
        Me.lblNarration.Visible = False
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(0, 0)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(45, 15)
        Me.lblBookType.TabIndex = 7
        Me.lblBookType.Text = "lblBType"
        Me.lblBookType.Visible = False
        '
        'optShow
        '
        '
        'lblVoucherAmount
        '
        Me.lblVoucherAmount.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoucherAmount.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVoucherAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVoucherAmount.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoucherAmount.Location = New System.Drawing.Point(784, 454)
        Me.lblVoucherAmount.Name = "lblVoucherAmount"
        Me.lblVoucherAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVoucherAmount.Size = New System.Drawing.Size(73, 15)
        Me.lblVoucherAmount.TabIndex = 65
        Me.lblVoucherAmount.Text = "lblVoucherAmount"
        Me.lblVoucherAmount.Visible = False
        '
        'lblVoucherDC
        '
        Me.lblVoucherDC.BackColor = System.Drawing.SystemColors.Control
        Me.lblVoucherDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVoucherDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVoucherDC.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVoucherDC.Location = New System.Drawing.Point(776, 468)
        Me.lblVoucherDC.Name = "lblVoucherDC"
        Me.lblVoucherDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVoucherDC.Size = New System.Drawing.Size(73, 15)
        Me.lblVoucherDC.TabIndex = 66
        Me.lblVoucherDC.Text = "lblVoucherDC"
        Me.lblVoucherDC.Visible = False
        '
        'frmPaymentDetail
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1039, 484)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblVoucherDC)
        Me.Controls.Add(Me.lblVoucherAmount)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.ADataMain)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.lblTrnRowNo)
        Me.Controls.Add(Me.lblTempProcessKey)
        Me.Controls.Add(Me.lblDivisionCode)
        Me.Controls.Add(Me.lblMkey)
        Me.Controls.Add(Me.lblCostCName)
        Me.Controls.Add(Me.lblCostCCode)
        Me.Controls.Add(Me.lblAccountCode)
        Me.Controls.Add(Me.lblADDMode)
        Me.Controls.Add(Me.lblModifyMode)
        Me.Controls.Add(Me.lblBillNo)
        Me.Controls.Add(Me.lblBillYear)
        Me.Controls.Add(Me.lblVDate)
        Me.Controls.Add(Me.lblNarration)
        Me.Controls.Add(Me.lblBookType)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(10, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPaymentDetail"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Payment Detail"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.optShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents Label4 As Label
    Public WithEvents txtDefaultCompanyName As TextBox
    Public WithEvents Label5 As Label
    Public WithEvents Label6 As Label
    Public WithEvents txtBillSearchFrom As MaskedTextBox
    Public WithEvents Label7 As Label
    Public WithEvents optAsPerBill As RadioButton
    Public WithEvents optAsPerMRR As RadioButton
    Public WithEvents lblVoucherAmount As Label
    Public WithEvents lblVoucherDC As Label
#End Region
End Class