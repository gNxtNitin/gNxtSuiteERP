Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmSalaryHead
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
    Public WithEvents txtDefaultAmount As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents _optStatus_0 As System.Windows.Forms.RadioButton
    Public WithEvents _optStatus_1 As System.Windows.Forms.RadioButton
    Public WithEvents txtClosedDate As System.Windows.Forms.MaskedTextBox
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents chkBasicSalPart As System.Windows.Forms.CheckBox
    Public WithEvents cboDC As System.Windows.Forms.ComboBox
    Public WithEvents txtDebit As System.Windows.Forms.TextBox
    Public WithEvents cmdDSearch As System.Windows.Forms.Button
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents chkLeaveEncash As System.Windows.Forms.CheckBox
    Public WithEvents ChkESI As System.Windows.Forms.CheckBox
    Public WithEvents ChkPF As System.Windows.Forms.CheckBox
    Public WithEvents frmPFESIIncluded As System.Windows.Forms.GroupBox
    Public WithEvents cboRound As System.Windows.Forms.ComboBox
    Public WithEvents txtPercentage As System.Windows.Forms.TextBox
    Public WithEvents txtSeq As System.Windows.Forms.TextBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents cboType As System.Windows.Forms.ComboBox
    Public WithEvents FraType As System.Windows.Forms.GroupBox
    Public WithEvents txtName As System.Windows.Forms.TextBox
    Public WithEvents _OptAdd_Ded_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptAdd_Ded_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptAdd_Ded_2 As System.Windows.Forms.RadioButton
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents _OptCalc_0 As System.Windows.Forms.RadioButton
    Public WithEvents _OptCalc_2 As System.Windows.Forms.RadioButton
    Public WithEvents _OptCalc_1 As System.Windows.Forms.RadioButton
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents _OptPaymentType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _OptPaymentType_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents fraMain As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents OptAdd_Ded As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptCalc As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents OptPaymentType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    Public WithEvents optStatus As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalaryHead))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdDSearch = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.fraMain = New System.Windows.Forms.GroupBox()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.txtDefaultAmount = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me._optStatus_0 = New System.Windows.Forms.RadioButton()
        Me._optStatus_1 = New System.Windows.Forms.RadioButton()
        Me.txtClosedDate = New System.Windows.Forms.MaskedTextBox()
        Me.chkBasicSalPart = New System.Windows.Forms.CheckBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.cboDC = New System.Windows.Forms.ComboBox()
        Me.txtDebit = New System.Windows.Forms.TextBox()
        Me.frmPFESIIncluded = New System.Windows.Forms.GroupBox()
        Me.chkLeaveEncash = New System.Windows.Forms.CheckBox()
        Me.ChkESI = New System.Windows.Forms.CheckBox()
        Me.ChkPF = New System.Windows.Forms.CheckBox()
        Me.cboRound = New System.Windows.Forms.ComboBox()
        Me.txtPercentage = New System.Windows.Forms.TextBox()
        Me.txtSeq = New System.Windows.Forms.TextBox()
        Me.FraType = New System.Windows.Forms.GroupBox()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me._OptAdd_Ded_0 = New System.Windows.Forms.RadioButton()
        Me._OptAdd_Ded_1 = New System.Windows.Forms.RadioButton()
        Me._OptAdd_Ded_2 = New System.Windows.Forms.RadioButton()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me._OptCalc_0 = New System.Windows.Forms.RadioButton()
        Me._OptCalc_2 = New System.Windows.Forms.RadioButton()
        Me._OptCalc_1 = New System.Windows.Forms.RadioButton()
        Me.Frame6 = New System.Windows.Forms.GroupBox()
        Me._OptPaymentType_1 = New System.Windows.Forms.RadioButton()
        Me._OptPaymentType_0 = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.OptAdd_Ded = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptCalc = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.OptPaymentType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.optStatus = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.fraMain.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.frmPFESIIncluded.SuspendLayout()
        Me.FraType.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame6.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.OptAdd_Ded, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptCalc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OptPaymentType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdDSearch
        '
        Me.cmdDSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdDSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDSearch.Image = CType(resources.GetObject("cmdDSearch.Image"), System.Drawing.Image)
        Me.cmdDSearch.Location = New System.Drawing.Point(282, 12)
        Me.cmdDSearch.Name = "cmdDSearch"
        Me.cmdDSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdDSearch.TabIndex = 13
        Me.cmdDSearch.TabStop = False
        Me.cmdDSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDSearch, "Search")
        Me.cmdDSearch.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(401, 14)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(29, 21)
        Me.cmdSearch.TabIndex = 2
        Me.cmdSearch.TabStop = False
        Me.cmdSearch.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearch, "Search")
        Me.cmdSearch.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(391, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(63, 34)
        Me.CmdClose.TabIndex = 23
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Control
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(329, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(63, 34)
        Me.CmdView.TabIndex = 22
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(267, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(63, 34)
        Me.CmdDelete.TabIndex = 20
        Me.CmdDelete.Text = "&Delete"
        Me.CmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdDelete, "Delete Record")
        Me.CmdDelete.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(205, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(63, 34)
        Me.CmdSave.TabIndex = 21
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
        Me.CmdModify.Location = New System.Drawing.Point(143, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(63, 34)
        Me.CmdModify.TabIndex = 19
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
        Me.CmdAdd.Location = New System.Drawing.Point(81, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(63, 34)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(114, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(50, 34)
        Me.cmdPrint.TabIndex = 26
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print List")
        Me.cmdPrint.UseVisualStyleBackColor = False
        Me.cmdPrint.Visible = False
        '
        'fraMain
        '
        Me.fraMain.BackColor = System.Drawing.SystemColors.Control
        Me.fraMain.Controls.Add(Me.Frame5)
        Me.fraMain.Controls.Add(Me.Frame4)
        Me.fraMain.Controls.Add(Me.chkBasicSalPart)
        Me.fraMain.Controls.Add(Me.Frame1)
        Me.fraMain.Controls.Add(Me.frmPFESIIncluded)
        Me.fraMain.Controls.Add(Me.cboRound)
        Me.fraMain.Controls.Add(Me.txtPercentage)
        Me.fraMain.Controls.Add(Me.txtSeq)
        Me.fraMain.Controls.Add(Me.cmdSearch)
        Me.fraMain.Controls.Add(Me.FraType)
        Me.fraMain.Controls.Add(Me.txtName)
        Me.fraMain.Controls.Add(Me.Frame2)
        Me.fraMain.Controls.Add(Me.Frame3)
        Me.fraMain.Controls.Add(Me.Frame6)
        Me.fraMain.Controls.Add(Me.Label4)
        Me.fraMain.Controls.Add(Me.Label3)
        Me.fraMain.Controls.Add(Me.Label2)
        Me.fraMain.Controls.Add(Me.Label1)
        Me.fraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMain.Location = New System.Drawing.Point(0, -4)
        Me.fraMain.Name = "fraMain"
        Me.fraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMain.Size = New System.Drawing.Size(550, 347)
        Me.fraMain.TabIndex = 4
        Me.fraMain.TabStop = False
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.txtDefaultAmount)
        Me.Frame5.Controls.Add(Me.Label5)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(69, 266)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(135, 43)
        Me.Frame5.TabIndex = 44
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Default Amount"
        '
        'txtDefaultAmount
        '
        Me.txtDefaultAmount.AcceptsReturn = True
        Me.txtDefaultAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtDefaultAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDefaultAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDefaultAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDefaultAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDefaultAmount.Location = New System.Drawing.Point(63, 16)
        Me.txtDefaultAmount.MaxLength = 0
        Me.txtDefaultAmount.Name = "txtDefaultAmount"
        Me.txtDefaultAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDefaultAmount.Size = New System.Drawing.Size(65, 20)
        Me.txtDefaultAmount.TabIndex = 45
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(6, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(50, 14)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Amount :"
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me._optStatus_0)
        Me.Frame4.Controls.Add(Me._optStatus_1)
        Me.Frame4.Controls.Add(Me.txtClosedDate)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(199, 266)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(243, 43)
        Me.Frame4.TabIndex = 40
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Status"
        '
        '_optStatus_0
        '
        Me._optStatus_0.AutoSize = True
        Me._optStatus_0.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_0.Checked = True
        Me._optStatus_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_0, CType(0, Short))
        Me._optStatus_0.Location = New System.Drawing.Point(60, 5)
        Me._optStatus_0.Name = "_optStatus_0"
        Me._optStatus_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_0.Size = New System.Drawing.Size(51, 18)
        Me._optStatus_0.TabIndex = 42
        Me._optStatus_0.TabStop = True
        Me._optStatus_0.Text = "Open"
        Me._optStatus_0.UseVisualStyleBackColor = False
        '
        '_optStatus_1
        '
        Me._optStatus_1.AutoSize = True
        Me._optStatus_1.BackColor = System.Drawing.SystemColors.Control
        Me._optStatus_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optStatus_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optStatus_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optStatus.SetIndex(Me._optStatus_1, CType(1, Short))
        Me._optStatus_1.Location = New System.Drawing.Point(60, 26)
        Me._optStatus_1.Name = "_optStatus_1"
        Me._optStatus_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optStatus_1.Size = New System.Drawing.Size(58, 18)
        Me._optStatus_1.TabIndex = 41
        Me._optStatus_1.TabStop = True
        Me._optStatus_1.Text = "Closed"
        Me._optStatus_1.UseVisualStyleBackColor = False
        '
        'txtClosedDate
        '
        Me.txtClosedDate.AllowPromptAsInput = False
        Me.txtClosedDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClosedDate.Location = New System.Drawing.Point(152, 16)
        Me.txtClosedDate.Mask = "##/##/####"
        Me.txtClosedDate.Name = "txtClosedDate"
        Me.txtClosedDate.Size = New System.Drawing.Size(81, 20)
        Me.txtClosedDate.TabIndex = 43
        '
        'chkBasicSalPart
        '
        Me.chkBasicSalPart.AutoSize = True
        Me.chkBasicSalPart.BackColor = System.Drawing.SystemColors.Control
        Me.chkBasicSalPart.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkBasicSalPart.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkBasicSalPart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkBasicSalPart.Location = New System.Drawing.Point(133, 42)
        Me.chkBasicSalPart.Name = "chkBasicSalPart"
        Me.chkBasicSalPart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkBasicSalPart.Size = New System.Drawing.Size(155, 18)
        Me.chkBasicSalPart.TabIndex = 3
        Me.chkBasicSalPart.Text = "Basic Salary Part (Yes/No)"
        Me.chkBasicSalPart.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.cboDC)
        Me.Frame1.Controls.Add(Me.txtDebit)
        Me.Frame1.Controls.Add(Me.cmdDSearch)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(69, 226)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(381, 39)
        Me.Frame1.TabIndex = 31
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Account Posting Head"
        '
        'cboDC
        '
        Me.cboDC.BackColor = System.Drawing.SystemColors.Window
        Me.cboDC.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDC.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDC.Location = New System.Drawing.Point(314, 12)
        Me.cboDC.Name = "cboDC"
        Me.cboDC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDC.Size = New System.Drawing.Size(57, 22)
        Me.cboDC.TabIndex = 32
        '
        'txtDebit
        '
        Me.txtDebit.AcceptsReturn = True
        Me.txtDebit.BackColor = System.Drawing.SystemColors.Window
        Me.txtDebit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDebit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDebit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDebit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDebit.Location = New System.Drawing.Point(12, 12)
        Me.txtDebit.MaxLength = 0
        Me.txtDebit.Name = "txtDebit"
        Me.txtDebit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDebit.Size = New System.Drawing.Size(269, 20)
        Me.txtDebit.TabIndex = 12
        '
        'frmPFESIIncluded
        '
        Me.frmPFESIIncluded.BackColor = System.Drawing.SystemColors.Control
        Me.frmPFESIIncluded.Controls.Add(Me.chkLeaveEncash)
        Me.frmPFESIIncluded.Controls.Add(Me.ChkESI)
        Me.frmPFESIIncluded.Controls.Add(Me.ChkPF)
        Me.frmPFESIIncluded.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.frmPFESIIncluded.ForeColor = System.Drawing.SystemColors.ControlText
        Me.frmPFESIIncluded.Location = New System.Drawing.Point(69, 164)
        Me.frmPFESIIncluded.Name = "frmPFESIIncluded"
        Me.frmPFESIIncluded.Padding = New System.Windows.Forms.Padding(0)
        Me.frmPFESIIncluded.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.frmPFESIIncluded.Size = New System.Drawing.Size(381, 35)
        Me.frmPFESIIncluded.TabIndex = 30
        Me.frmPFESIIncluded.TabStop = False
        Me.frmPFESIIncluded.Text = "Included In"
        '
        'chkLeaveEncash
        '
        Me.chkLeaveEncash.AutoSize = True
        Me.chkLeaveEncash.BackColor = System.Drawing.SystemColors.Control
        Me.chkLeaveEncash.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkLeaveEncash.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkLeaveEncash.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkLeaveEncash.Location = New System.Drawing.Point(224, 16)
        Me.chkLeaveEncash.Name = "chkLeaveEncash"
        Me.chkLeaveEncash.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkLeaveEncash.Size = New System.Drawing.Size(118, 18)
        Me.chkLeaveEncash.TabIndex = 8
        Me.chkLeaveEncash.Text = "Leave Encashment"
        Me.chkLeaveEncash.UseVisualStyleBackColor = False
        '
        'ChkESI
        '
        Me.ChkESI.AutoSize = True
        Me.ChkESI.BackColor = System.Drawing.SystemColors.Control
        Me.ChkESI.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkESI.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkESI.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkESI.Location = New System.Drawing.Point(126, 16)
        Me.ChkESI.Name = "ChkESI"
        Me.ChkESI.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkESI.Size = New System.Drawing.Size(41, 18)
        Me.ChkESI.TabIndex = 7
        Me.ChkESI.Text = "ESI"
        Me.ChkESI.UseVisualStyleBackColor = False
        '
        'ChkPF
        '
        Me.ChkPF.AutoSize = True
        Me.ChkPF.BackColor = System.Drawing.SystemColors.Control
        Me.ChkPF.Cursor = System.Windows.Forms.Cursors.Default
        Me.ChkPF.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkPF.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ChkPF.Location = New System.Drawing.Point(10, 16)
        Me.ChkPF.Name = "ChkPF"
        Me.ChkPF.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ChkPF.Size = New System.Drawing.Size(38, 18)
        Me.ChkPF.TabIndex = 6
        Me.ChkPF.Text = "PF"
        Me.ChkPF.UseVisualStyleBackColor = False
        '
        'cboRound
        '
        Me.cboRound.BackColor = System.Drawing.SystemColors.Window
        Me.cboRound.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboRound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRound.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRound.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboRound.Location = New System.Drawing.Point(373, 204)
        Me.cboRound.Name = "cboRound"
        Me.cboRound.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboRound.Size = New System.Drawing.Size(59, 22)
        Me.cboRound.TabIndex = 11
        '
        'txtPercentage
        '
        Me.txtPercentage.AcceptsReturn = True
        Me.txtPercentage.BackColor = System.Drawing.SystemColors.Window
        Me.txtPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPercentage.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPercentage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPercentage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPercentage.Location = New System.Drawing.Point(147, 202)
        Me.txtPercentage.MaxLength = 0
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPercentage.Size = New System.Drawing.Size(35, 20)
        Me.txtPercentage.TabIndex = 9
        '
        'txtSeq
        '
        Me.txtSeq.AcceptsReturn = True
        Me.txtSeq.BackColor = System.Drawing.SystemColors.Window
        Me.txtSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSeq.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSeq.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSeq.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSeq.Location = New System.Drawing.Point(261, 202)
        Me.txtSeq.MaxLength = 0
        Me.txtSeq.Name = "txtSeq"
        Me.txtSeq.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSeq.Size = New System.Drawing.Size(33, 20)
        Me.txtSeq.TabIndex = 10
        '
        'FraType
        '
        Me.FraType.BackColor = System.Drawing.SystemColors.Control
        Me.FraType.Controls.Add(Me.cboType)
        Me.FraType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraType.Location = New System.Drawing.Point(69, 124)
        Me.FraType.Name = "FraType"
        Me.FraType.Padding = New System.Windows.Forms.Padding(0)
        Me.FraType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraType.Size = New System.Drawing.Size(381, 39)
        Me.FraType.TabIndex = 17
        Me.FraType.TabStop = False
        Me.FraType.Text = "Type"
        '
        'cboType
        '
        Me.cboType.BackColor = System.Drawing.SystemColors.Window
        Me.cboType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboType.Location = New System.Drawing.Point(38, 10)
        Me.cboType.Name = "cboType"
        Me.cboType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboType.Size = New System.Drawing.Size(321, 24)
        Me.cboType.TabIndex = 5
        '
        'txtName
        '
        Me.txtName.AcceptsReturn = True
        Me.txtName.BackColor = System.Drawing.SystemColors.Window
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtName.Location = New System.Drawing.Point(133, 14)
        Me.txtName.MaxLength = 0
        Me.txtName.Name = "txtName"
        Me.txtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtName.Size = New System.Drawing.Size(267, 20)
        Me.txtName.TabIndex = 1
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me._OptAdd_Ded_0)
        Me.Frame2.Controls.Add(Me._OptAdd_Ded_1)
        Me.Frame2.Controls.Add(Me._OptAdd_Ded_2)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(69, 54)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(381, 35)
        Me.Frame2.TabIndex = 14
        Me.Frame2.TabStop = False
        '
        '_OptAdd_Ded_0
        '
        Me._OptAdd_Ded_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptAdd_Ded_0.Checked = True
        Me._OptAdd_Ded_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptAdd_Ded_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptAdd_Ded_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAdd_Ded.SetIndex(Me._OptAdd_Ded_0, CType(0, Short))
        Me._OptAdd_Ded_0.Location = New System.Drawing.Point(10, 12)
        Me._OptAdd_Ded_0.Name = "_OptAdd_Ded_0"
        Me._OptAdd_Ded_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptAdd_Ded_0.Size = New System.Drawing.Size(89, 17)
        Me._OptAdd_Ded_0.TabIndex = 35
        Me._OptAdd_Ded_0.TabStop = True
        Me._OptAdd_Ded_0.Text = "&Earning"
        Me._OptAdd_Ded_0.UseVisualStyleBackColor = False
        '
        '_OptAdd_Ded_1
        '
        Me._OptAdd_Ded_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptAdd_Ded_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptAdd_Ded_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptAdd_Ded_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAdd_Ded.SetIndex(Me._OptAdd_Ded_1, CType(1, Short))
        Me._OptAdd_Ded_1.Location = New System.Drawing.Point(128, 12)
        Me._OptAdd_Ded_1.Name = "_OptAdd_Ded_1"
        Me._OptAdd_Ded_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptAdd_Ded_1.Size = New System.Drawing.Size(77, 17)
        Me._OptAdd_Ded_1.TabIndex = 34
        Me._OptAdd_Ded_1.TabStop = True
        Me._OptAdd_Ded_1.Text = "D&edution"
        Me._OptAdd_Ded_1.UseVisualStyleBackColor = False
        '
        '_OptAdd_Ded_2
        '
        Me._OptAdd_Ded_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptAdd_Ded_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptAdd_Ded_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptAdd_Ded_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAdd_Ded.SetIndex(Me._OptAdd_Ded_2, CType(2, Short))
        Me._OptAdd_Ded_2.Location = New System.Drawing.Point(242, 12)
        Me._OptAdd_Ded_2.Name = "_OptAdd_Ded_2"
        Me._OptAdd_Ded_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptAdd_Ded_2.Size = New System.Drawing.Size(75, 15)
        Me._OptAdd_Ded_2.TabIndex = 33
        Me._OptAdd_Ded_2.TabStop = True
        Me._OptAdd_Ded_2.Text = "Pe&rks"
        Me._OptAdd_Ded_2.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me._OptCalc_0)
        Me.Frame3.Controls.Add(Me._OptCalc_2)
        Me.Frame3.Controls.Add(Me._OptCalc_1)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(69, 88)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(381, 35)
        Me.Frame3.TabIndex = 15
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Calculation"
        '
        '_OptCalc_0
        '
        Me._OptCalc_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptCalc_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptCalc_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptCalc_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptCalc.SetIndex(Me._OptCalc_0, CType(0, Short))
        Me._OptCalc_0.Location = New System.Drawing.Point(10, 14)
        Me._OptCalc_0.Name = "_OptCalc_0"
        Me._OptCalc_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptCalc_0.Size = New System.Drawing.Size(109, 17)
        Me._OptCalc_0.TabIndex = 38
        Me._OptCalc_0.TabStop = True
        Me._OptCalc_0.Text = "&Basic Salary"
        Me._OptCalc_0.UseVisualStyleBackColor = False
        '
        '_OptCalc_2
        '
        Me._OptCalc_2.BackColor = System.Drawing.SystemColors.Control
        Me._OptCalc_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptCalc_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptCalc_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptCalc.SetIndex(Me._OptCalc_2, CType(2, Short))
        Me._OptCalc_2.Location = New System.Drawing.Point(242, 14)
        Me._OptCalc_2.Name = "_OptCalc_2"
        Me._OptCalc_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptCalc_2.Size = New System.Drawing.Size(77, 17)
        Me._OptCalc_2.TabIndex = 37
        Me._OptCalc_2.TabStop = True
        Me._OptCalc_2.Text = "&Variable"
        Me._OptCalc_2.UseVisualStyleBackColor = False
        '
        '_OptCalc_1
        '
        Me._OptCalc_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptCalc_1.Checked = True
        Me._OptCalc_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptCalc_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptCalc_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptCalc.SetIndex(Me._OptCalc_1, CType(1, Short))
        Me._OptCalc_1.Location = New System.Drawing.Point(128, 14)
        Me._OptCalc_1.Name = "_OptCalc_1"
        Me._OptCalc_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptCalc_1.Size = New System.Drawing.Size(69, 17)
        Me._OptCalc_1.TabIndex = 36
        Me._OptCalc_1.TabStop = True
        Me._OptCalc_1.Text = "&Fixed"
        Me._OptCalc_1.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me._OptPaymentType_1)
        Me.Frame6.Controls.Add(Me._OptPaymentType_0)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(69, 308)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(381, 39)
        Me.Frame6.TabIndex = 47
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Payment Type"
        '
        '_OptPaymentType_1
        '
        Me._OptPaymentType_1.AutoSize = True
        Me._OptPaymentType_1.BackColor = System.Drawing.SystemColors.Control
        Me._OptPaymentType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPaymentType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPaymentType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPaymentType.SetIndex(Me._OptPaymentType_1, CType(1, Short))
        Me._OptPaymentType_1.Location = New System.Drawing.Point(202, 16)
        Me._OptPaymentType_1.Name = "_OptPaymentType_1"
        Me._OptPaymentType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPaymentType_1.Size = New System.Drawing.Size(56, 18)
        Me._OptPaymentType_1.TabIndex = 49
        Me._OptPaymentType_1.TabStop = True
        Me._OptPaymentType_1.Text = "&Yearly"
        Me._OptPaymentType_1.UseVisualStyleBackColor = False
        '
        '_OptPaymentType_0
        '
        Me._OptPaymentType_0.AutoSize = True
        Me._OptPaymentType_0.BackColor = System.Drawing.SystemColors.Control
        Me._OptPaymentType_0.Checked = True
        Me._OptPaymentType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._OptPaymentType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._OptPaymentType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptPaymentType.SetIndex(Me._OptPaymentType_0, CType(0, Short))
        Me._OptPaymentType_0.Location = New System.Drawing.Point(64, 16)
        Me._OptPaymentType_0.Name = "_OptPaymentType_0"
        Me._OptPaymentType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._OptPaymentType_0.Size = New System.Drawing.Size(62, 18)
        Me._OptPaymentType_0.TabIndex = 48
        Me._OptPaymentType_0.TabStop = True
        Me._OptPaymentType_0.Text = "&Monthly"
        Me._OptPaymentType_0.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(299, 206)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(58, 14)
        Me.Label4.TabIndex = 29
        Me.Label4.Text = "Rounding :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(70, 206)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(68, 14)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Percentage :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(190, 206)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(62, 14)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Sequence :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(71, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(40, 14)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Name :"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(550, 343)
        Me.SprdView.TabIndex = 39
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 338)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(549, 51)
        Me.FraMovement.TabIndex = 18
        Me.FraMovement.TabStop = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(147, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(57, 33)
        Me.cmdSavePrint.TabIndex = 25
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        Me.cmdSavePrint.Visible = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(164, 12)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(50, 33)
        Me.cmdPreview.TabIndex = 24
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdPreview.UseVisualStyleBackColor = False
        Me.cmdPreview.Visible = False
        '
        'OptAdd_Ded
        '
        '
        'OptCalc
        '
        '
        'OptPaymentType
        '
        '
        'optStatus
        '
        '
        'frmSalaryHead
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(551, 390)
        Me.Controls.Add(Me.fraMain)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(21, 91)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSalaryHead"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Salary Heads"
        Me.fraMain.ResumeLayout(False)
        Me.fraMain.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.frmPFESIIncluded.ResumeLayout(False)
        Me.frmPFESIIncluded.PerformLayout()
        Me.FraType.ResumeLayout(False)
        Me.Frame2.ResumeLayout(False)
        Me.Frame3.ResumeLayout(False)
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.OptAdd_Ded, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptCalc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OptPaymentType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.optStatus, System.ComponentModel.ISupportInitialize).EndInit()
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
#End Region
End Class