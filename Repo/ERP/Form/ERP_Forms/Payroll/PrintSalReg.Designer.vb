Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPrintSalReg
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
    Public WithEvents OptIncentive As System.Windows.Forms.RadioButton
    Public WithEvents optInaam As System.Windows.Forms.RadioButton
    Public WithEvents OptSalSheet As System.Windows.Forms.RadioButton
    Public WithEvents OptSalReg As System.Windows.Forms.RadioButton
    Public WithEvents optPaySlip As System.Windows.Forms.RadioButton
    Public WithEvents cmdsearch As System.Windows.Forms.Button
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents _optAll_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optAll_1 As System.Windows.Forms.RadioButton
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents FraSelection As System.Windows.Forms.GroupBox
    Public WithEvents optCashSheet As System.Windows.Forms.RadioButton
    Public WithEvents optBankTxt As System.Windows.Forms.RadioButton
    Public WithEvents OptDeductionList As System.Windows.Forms.RadioButton
    Public WithEvents txtDeductionName As System.Windows.Forms.TextBox
    Public WithEvents cmdDeductionSearch As System.Windows.Forms.Button
    Public WithEvents _optAllBank_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optAllBank_0 As System.Windows.Forms.RadioButton
    Public WithEvents cmdSearchBank As System.Windows.Forms.Button
    Public WithEvents txtBankName As System.Windows.Forms.TextBox
    Public WithEvents fraBankName As System.Windows.Forms.GroupBox
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdOk As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents FraOk As System.Windows.Forms.GroupBox
    Public WithEvents optAll As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optAllBank As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintSalReg))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdsearch = New System.Windows.Forms.Button()
        Me.cmdDeductionSearch = New System.Windows.Forms.Button()
        Me.cmdSearchBank = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.OptIncentive = New System.Windows.Forms.RadioButton()
        Me.optInaam = New System.Windows.Forms.RadioButton()
        Me.OptSalSheet = New System.Windows.Forms.RadioButton()
        Me.OptSalReg = New System.Windows.Forms.RadioButton()
        Me.optPaySlip = New System.Windows.Forms.RadioButton()
        Me.FraSelection = New System.Windows.Forms.GroupBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me._optAll_0 = New System.Windows.Forms.RadioButton()
        Me._optAll_1 = New System.Windows.Forms.RadioButton()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.optCashSheet = New System.Windows.Forms.RadioButton()
        Me.optBankTxt = New System.Windows.Forms.RadioButton()
        Me.OptDeductionList = New System.Windows.Forms.RadioButton()
        Me.txtDeductionName = New System.Windows.Forms.TextBox()
        Me.fraBankName = New System.Windows.Forms.GroupBox()
        Me._optAllBank_1 = New System.Windows.Forms.RadioButton()
        Me._optAllBank_0 = New System.Windows.Forms.RadioButton()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.FraOk = New System.Windows.Forms.GroupBox()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.optAll = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optAllBank = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.FraSelection.SuspendLayout()
        Me.fraBankName.SuspendLayout()
        Me.FraOk.SuspendLayout()
        CType(Me.optAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optAllBank, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdsearch
        '
        Me.cmdsearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdsearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdsearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdsearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdsearch.Image = CType(resources.GetObject("cmdsearch.Image"), System.Drawing.Image)
        Me.cmdsearch.Location = New System.Drawing.Point(106, 32)
        Me.cmdsearch.Name = "cmdsearch"
        Me.cmdsearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdsearch.Size = New System.Drawing.Size(27, 19)
        Me.cmdsearch.TabIndex = 14
        Me.cmdsearch.TabStop = False
        Me.cmdsearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdsearch, "Search")
        Me.cmdsearch.UseVisualStyleBackColor = False
        '
        'cmdDeductionSearch
        '
        Me.cmdDeductionSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDeductionSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDeductionSearch.Enabled = False
        Me.cmdDeductionSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDeductionSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDeductionSearch.Image = CType(resources.GetObject("cmdDeductionSearch.Image"), System.Drawing.Image)
        Me.cmdDeductionSearch.Location = New System.Drawing.Point(172, 285)
        Me.cmdDeductionSearch.Name = "cmdDeductionSearch"
        Me.cmdDeductionSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDeductionSearch.Size = New System.Drawing.Size(25, 19)
        Me.cmdDeductionSearch.TabIndex = 4
        Me.cmdDeductionSearch.TabStop = False
        Me.cmdDeductionSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDeductionSearch, "Search")
        Me.cmdDeductionSearch.UseVisualStyleBackColor = False
        '
        'cmdSearchBank
        '
        Me.cmdSearchBank.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchBank.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchBank.Enabled = False
        Me.cmdSearchBank.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchBank.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchBank.Image = CType(resources.GetObject("cmdSearchBank.Image"), System.Drawing.Image)
        Me.cmdSearchBank.Location = New System.Drawing.Point(170, 28)
        Me.cmdSearchBank.Name = "cmdSearchBank"
        Me.cmdSearchBank.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchBank.Size = New System.Drawing.Size(25, 19)
        Me.cmdSearchBank.TabIndex = 20
        Me.cmdSearchBank.TabStop = False
        Me.cmdSearchBank.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchBank, "Search")
        Me.cmdSearchBank.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.OptIncentive)
        Me.Frame1.Controls.Add(Me.optInaam)
        Me.Frame1.Controls.Add(Me.OptSalSheet)
        Me.Frame1.Controls.Add(Me.OptSalReg)
        Me.Frame1.Controls.Add(Me.optPaySlip)
        Me.Frame1.Controls.Add(Me.FraSelection)
        Me.Frame1.Controls.Add(Me.optCashSheet)
        Me.Frame1.Controls.Add(Me.optBankTxt)
        Me.Frame1.Controls.Add(Me.OptDeductionList)
        Me.Frame1.Controls.Add(Me.txtDeductionName)
        Me.Frame1.Controls.Add(Me.cmdDeductionSearch)
        Me.Frame1.Controls.Add(Me.fraBankName)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 0)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(201, 304)
        Me.Frame1.TabIndex = 3
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Printing Status"
        '
        'OptIncentive
        '
        Me.OptIncentive.AutoSize = True
        Me.OptIncentive.BackColor = System.Drawing.SystemColors.Control
        Me.OptIncentive.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptIncentive.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptIncentive.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptIncentive.Location = New System.Drawing.Point(42, 100)
        Me.OptIncentive.Name = "OptIncentive"
        Me.OptIncentive.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptIncentive.Size = New System.Drawing.Size(88, 18)
        Me.OptIncentive.TabIndex = 24
        Me.OptIncentive.TabStop = True
        Me.OptIncentive.Text = "Incentive Slip"
        Me.OptIncentive.UseVisualStyleBackColor = False
        '
        'optInaam
        '
        Me.optInaam.AutoSize = True
        Me.optInaam.BackColor = System.Drawing.SystemColors.Control
        Me.optInaam.Cursor = System.Windows.Forms.Cursors.Default
        Me.optInaam.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optInaam.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optInaam.Location = New System.Drawing.Point(42, 118)
        Me.optInaam.Name = "optInaam"
        Me.optInaam.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optInaam.Size = New System.Drawing.Size(73, 18)
        Me.optInaam.TabIndex = 23
        Me.optInaam.TabStop = True
        Me.optInaam.Text = "Inaam List"
        Me.optInaam.UseVisualStyleBackColor = False
        '
        'OptSalSheet
        '
        Me.OptSalSheet.AutoSize = True
        Me.OptSalSheet.BackColor = System.Drawing.SystemColors.Control
        Me.OptSalSheet.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptSalSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptSalSheet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSalSheet.Location = New System.Drawing.Point(42, 48)
        Me.OptSalSheet.Name = "OptSalSheet"
        Me.OptSalSheet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptSalSheet.Size = New System.Drawing.Size(80, 18)
        Me.OptSalSheet.TabIndex = 17
        Me.OptSalSheet.TabStop = True
        Me.OptSalSheet.Text = "Bank Letter"
        Me.OptSalSheet.UseVisualStyleBackColor = False
        '
        'OptSalReg
        '
        Me.OptSalReg.AutoSize = True
        Me.OptSalReg.BackColor = System.Drawing.SystemColors.Control
        Me.OptSalReg.Checked = True
        Me.OptSalReg.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptSalReg.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptSalReg.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptSalReg.Location = New System.Drawing.Point(42, 16)
        Me.OptSalReg.Name = "OptSalReg"
        Me.OptSalReg.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptSalReg.Size = New System.Drawing.Size(99, 18)
        Me.OptSalReg.TabIndex = 16
        Me.OptSalReg.TabStop = True
        Me.OptSalReg.Text = "Salary Register"
        Me.OptSalReg.UseVisualStyleBackColor = False
        '
        'optPaySlip
        '
        Me.optPaySlip.AutoSize = True
        Me.optPaySlip.BackColor = System.Drawing.SystemColors.Control
        Me.optPaySlip.Cursor = System.Windows.Forms.Cursors.Default
        Me.optPaySlip.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optPaySlip.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optPaySlip.Location = New System.Drawing.Point(42, 84)
        Me.optPaySlip.Name = "optPaySlip"
        Me.optPaySlip.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optPaySlip.Size = New System.Drawing.Size(63, 18)
        Me.optPaySlip.TabIndex = 15
        Me.optPaySlip.TabStop = True
        Me.optPaySlip.Text = "Pay Slip"
        Me.optPaySlip.UseVisualStyleBackColor = False
        '
        'FraSelection
        '
        Me.FraSelection.BackColor = System.Drawing.SystemColors.Control
        Me.FraSelection.Controls.Add(Me.cmdsearch)
        Me.FraSelection.Controls.Add(Me.txtEmpCode)
        Me.FraSelection.Controls.Add(Me._optAll_0)
        Me.FraSelection.Controls.Add(Me._optAll_1)
        Me.FraSelection.Controls.Add(Me.txtName)
        Me.FraSelection.Enabled = False
        Me.FraSelection.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraSelection.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraSelection.Location = New System.Drawing.Point(0, 131)
        Me.FraSelection.Name = "FraSelection"
        Me.FraSelection.Padding = New System.Windows.Forms.Padding(0)
        Me.FraSelection.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraSelection.Size = New System.Drawing.Size(201, 72)
        Me.FraSelection.TabIndex = 9
        Me.FraSelection.TabStop = False
        Me.FraSelection.Text = "Selection"
        Me.FraSelection.Visible = False
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(2, 32)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(103, 19)
        Me.txtEmpCode.TabIndex = 13
        '
        '_optAll_0
        '
        Me._optAll_0.AutoSize = True
        Me._optAll_0.BackColor = System.Drawing.SystemColors.Control
        Me._optAll_0.Checked = True
        Me._optAll_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAll_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAll_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAll.SetIndex(Me._optAll_0, CType(0, Short))
        Me._optAll_0.Location = New System.Drawing.Point(10, 13)
        Me._optAll_0.Name = "_optAll_0"
        Me._optAll_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAll_0.Size = New System.Drawing.Size(37, 18)
        Me._optAll_0.TabIndex = 12
        Me._optAll_0.TabStop = True
        Me._optAll_0.Text = "All"
        Me._optAll_0.UseVisualStyleBackColor = False
        '
        '_optAll_1
        '
        Me._optAll_1.AutoSize = True
        Me._optAll_1.BackColor = System.Drawing.SystemColors.Control
        Me._optAll_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAll_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAll_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAll.SetIndex(Me._optAll_1, CType(1, Short))
        Me._optAll_1.Location = New System.Drawing.Point(110, 13)
        Me._optAll_1.Name = "_optAll_1"
        Me._optAll_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAll_1.Size = New System.Drawing.Size(76, 18)
        Me._optAll_1.TabIndex = 11
        Me._optAll_1.TabStop = True
        Me._optAll_1.Text = "Particulars"
        Me._optAll_1.UseVisualStyleBackColor = False
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Enabled = False
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(2, 53)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(195, 19)
        Me.txtName.TabIndex = 10
        '
        'optCashSheet
        '
        Me.optCashSheet.AutoSize = True
        Me.optCashSheet.BackColor = System.Drawing.SystemColors.Control
        Me.optCashSheet.Cursor = System.Windows.Forms.Cursors.Default
        Me.optCashSheet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optCashSheet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optCashSheet.Location = New System.Drawing.Point(42, 32)
        Me.optCashSheet.Name = "optCashSheet"
        Me.optCashSheet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optCashSheet.Size = New System.Drawing.Size(115, 18)
        Me.optCashSheet.TabIndex = 8
        Me.optCashSheet.TabStop = True
        Me.optCashSheet.Text = "Salary Cash Sheet"
        Me.optCashSheet.UseVisualStyleBackColor = False
        '
        'optBankTxt
        '
        Me.optBankTxt.AutoSize = True
        Me.optBankTxt.BackColor = System.Drawing.SystemColors.Control
        Me.optBankTxt.Cursor = System.Windows.Forms.Cursors.Default
        Me.optBankTxt.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optBankTxt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optBankTxt.Location = New System.Drawing.Point(42, 66)
        Me.optBankTxt.Name = "optBankTxt"
        Me.optBankTxt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optBankTxt.Size = New System.Drawing.Size(64, 18)
        Me.optBankTxt.TabIndex = 7
        Me.optBankTxt.TabStop = True
        Me.optBankTxt.Text = "Text File"
        Me.optBankTxt.UseVisualStyleBackColor = False
        '
        'OptDeductionList
        '
        Me.OptDeductionList.AutoSize = True
        Me.OptDeductionList.BackColor = System.Drawing.SystemColors.Control
        Me.OptDeductionList.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptDeductionList.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptDeductionList.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptDeductionList.Location = New System.Drawing.Point(42, 263)
        Me.OptDeductionList.Name = "OptDeductionList"
        Me.OptDeductionList.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptDeductionList.Size = New System.Drawing.Size(104, 18)
        Me.OptDeductionList.TabIndex = 6
        Me.OptDeductionList.TabStop = True
        Me.OptDeductionList.Text = "Salary Head List"
        Me.OptDeductionList.UseVisualStyleBackColor = False
        '
        'txtDeductionName
        '
        Me.txtDeductionName.AcceptsReturn = True
        Me.txtDeductionName.BackColor = System.Drawing.SystemColors.Window
        Me.txtDeductionName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeductionName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDeductionName.Enabled = False
        Me.txtDeductionName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDeductionName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDeductionName.Location = New System.Drawing.Point(6, 285)
        Me.txtDeductionName.MaxLength = 0
        Me.txtDeductionName.Name = "txtDeductionName"
        Me.txtDeductionName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDeductionName.Size = New System.Drawing.Size(163, 19)
        Me.txtDeductionName.TabIndex = 5
        '
        'fraBankName
        '
        Me.fraBankName.BackColor = System.Drawing.SystemColors.Control
        Me.fraBankName.Controls.Add(Me._optAllBank_1)
        Me.fraBankName.Controls.Add(Me._optAllBank_0)
        Me.fraBankName.Controls.Add(Me.cmdSearchBank)
        Me.fraBankName.Controls.Add(Me.txtBankName)
        Me.fraBankName.Enabled = False
        Me.fraBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraBankName.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraBankName.Location = New System.Drawing.Point(0, 206)
        Me.fraBankName.Name = "fraBankName"
        Me.fraBankName.Padding = New System.Windows.Forms.Padding(0)
        Me.fraBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraBankName.Size = New System.Drawing.Size(201, 51)
        Me.fraBankName.TabIndex = 18
        Me.fraBankName.TabStop = False
        Me.fraBankName.Text = "Bank Name"
        Me.fraBankName.Visible = False
        '
        '_optAllBank_1
        '
        Me._optAllBank_1.AutoSize = True
        Me._optAllBank_1.BackColor = System.Drawing.SystemColors.Control
        Me._optAllBank_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAllBank_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAllBank_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAllBank.SetIndex(Me._optAllBank_1, CType(1, Short))
        Me._optAllBank_1.Location = New System.Drawing.Point(110, 14)
        Me._optAllBank_1.Name = "_optAllBank_1"
        Me._optAllBank_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAllBank_1.Size = New System.Drawing.Size(76, 18)
        Me._optAllBank_1.TabIndex = 22
        Me._optAllBank_1.TabStop = True
        Me._optAllBank_1.Text = "Particulars"
        Me._optAllBank_1.UseVisualStyleBackColor = False
        '
        '_optAllBank_0
        '
        Me._optAllBank_0.AutoSize = True
        Me._optAllBank_0.BackColor = System.Drawing.SystemColors.Control
        Me._optAllBank_0.Checked = True
        Me._optAllBank_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optAllBank_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optAllBank_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optAllBank.SetIndex(Me._optAllBank_0, CType(0, Short))
        Me._optAllBank_0.Location = New System.Drawing.Point(10, 14)
        Me._optAllBank_0.Name = "_optAllBank_0"
        Me._optAllBank_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optAllBank_0.Size = New System.Drawing.Size(37, 18)
        Me._optAllBank_0.TabIndex = 21
        Me._optAllBank_0.TabStop = True
        Me._optAllBank_0.Text = "All"
        Me._optAllBank_0.UseVisualStyleBackColor = False
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Enabled = False
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBankName.Location = New System.Drawing.Point(2, 32)
        Me.txtBankName.MaxLength = 0
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(165, 19)
        Me.txtBankName.TabIndex = 19
        '
        'FraOk
        '
        Me.FraOk.BackColor = System.Drawing.SystemColors.Control
        Me.FraOk.Controls.Add(Me.cmdOk)
        Me.FraOk.Controls.Add(Me.cmdCancel)
        Me.FraOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraOk.Location = New System.Drawing.Point(0, 301)
        Me.FraOk.Name = "FraOk"
        Me.FraOk.Padding = New System.Windows.Forms.Padding(0)
        Me.FraOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraOk.Size = New System.Drawing.Size(201, 43)
        Me.FraOk.TabIndex = 0
        Me.FraOk.TabStop = False
        '
        'cmdOk
        '
        Me.cmdOk.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOk.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdOk.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOk.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOk.Location = New System.Drawing.Point(8, 12)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdOk.Size = New System.Drawing.Size(73, 25)
        Me.cmdOk.TabIndex = 2
        Me.cmdOk.Text = "&Ok"
        Me.cmdOk.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Location = New System.Drawing.Point(116, 12)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(73, 25)
        Me.cmdCancel.TabIndex = 1
        Me.cmdCancel.Text = "&Cancel"
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'optAll
        '
        '
        'optAllBank
        '
        '
        'frmPrintSalReg
        '
        Me.AcceptButton = Me.cmdOk
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(201, 343)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraOk)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(144, 135)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintSalReg"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Print"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraSelection.ResumeLayout(False)
        Me.FraSelection.PerformLayout()
        Me.fraBankName.ResumeLayout(False)
        Me.fraBankName.PerformLayout()
        Me.FraOk.ResumeLayout(False)
        CType(Me.optAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optAllBank, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class