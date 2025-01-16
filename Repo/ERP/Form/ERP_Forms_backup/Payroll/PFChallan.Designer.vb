Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmPFChallan
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
    Public WithEvents cboPaidBy As System.Windows.Forms.ComboBox
    Public WithEvents txtPaymentDate As System.Windows.Forms.TextBox
    Public WithEvents txtEmperDueDate As System.Windows.Forms.TextBox
    Public WithEvents txtEmpDueDate As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents txtAccountGroupCode As System.Windows.Forms.TextBox
    Public WithEvents txtEstableCode As System.Windows.Forms.TextBox
    Public WithEvents txtRefDate As System.Windows.Forms.TextBox
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents txtTotSubs_AC10C As System.Windows.Forms.TextBox
    Public WithEvents txtTotSubs_AC1C As System.Windows.Forms.TextBox
    Public WithEvents txtTotSubs_AC21C As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages_AC10C As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages_AC1C As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages_AC21C As System.Windows.Forms.TextBox
    Public WithEvents txtTotSubs_AC10B As System.Windows.Forms.TextBox
    Public WithEvents txtTotSubs_AC1B As System.Windows.Forms.TextBox
    Public WithEvents txtTotSubs_AC21B As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages_AC10B As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages_AC1B As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages_AC21B As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages_AC21 As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages_AC1 As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages_AC10 As System.Windows.Forms.TextBox
    Public WithEvents txtTotSubs_AC21 As System.Windows.Forms.TextBox
    Public WithEvents txtTotSubs_AC1 As System.Windows.Forms.TextBox
    Public WithEvents txtTotSubs_AC10 As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Public WithEvents txtDepositor As System.Windows.Forms.TextBox
    Public WithEvents txtChqNo As System.Windows.Forms.TextBox
    Public WithEvents txtBankName As System.Windows.Forms.TextBox
    Public WithEvents txtChqDate As System.Windows.Forms.TextBox
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents FraView As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents cmdPreview As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPFChallan))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.cmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraView = New System.Windows.Forms.GroupBox()
        Me.cboPaidBy = New System.Windows.Forms.ComboBox()
        Me.txtPaymentDate = New System.Windows.Forms.TextBox()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.txtEmperDueDate = New System.Windows.Forms.TextBox()
        Me.txtEmpDueDate = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtAccountGroupCode = New System.Windows.Forms.TextBox()
        Me.txtEstableCode = New System.Windows.Forms.TextBox()
        Me.txtRefDate = New System.Windows.Forms.TextBox()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtTotSubs_AC10C = New System.Windows.Forms.TextBox()
        Me.txtTotSubs_AC1C = New System.Windows.Forms.TextBox()
        Me.txtTotSubs_AC21C = New System.Windows.Forms.TextBox()
        Me.txtTotWages_AC10C = New System.Windows.Forms.TextBox()
        Me.txtTotWages_AC1C = New System.Windows.Forms.TextBox()
        Me.txtTotWages_AC21C = New System.Windows.Forms.TextBox()
        Me.txtTotSubs_AC10B = New System.Windows.Forms.TextBox()
        Me.txtTotSubs_AC1B = New System.Windows.Forms.TextBox()
        Me.txtTotSubs_AC21B = New System.Windows.Forms.TextBox()
        Me.txtTotWages_AC10B = New System.Windows.Forms.TextBox()
        Me.txtTotWages_AC1B = New System.Windows.Forms.TextBox()
        Me.txtTotWages_AC21B = New System.Windows.Forms.TextBox()
        Me.txtTotWages_AC21 = New System.Windows.Forms.TextBox()
        Me.txtTotWages_AC1 = New System.Windows.Forms.TextBox()
        Me.txtTotWages_AC10 = New System.Windows.Forms.TextBox()
        Me.txtTotSubs_AC21 = New System.Windows.Forms.TextBox()
        Me.txtTotSubs_AC1 = New System.Windows.Forms.TextBox()
        Me.txtTotSubs_AC10 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Frame5 = New System.Windows.Forms.GroupBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.txtDepositor = New System.Windows.Forms.TextBox()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtChqDate = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.FraView.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame5.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(598, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 35)
        Me.CmdClose.TabIndex = 41
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
        Me.CmdView.Location = New System.Drawing.Point(532, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 35)
        Me.CmdView.TabIndex = 40
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View List")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'cmdPreview
        '
        Me.cmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPreview.Image = CType(resources.GetObject("cmdPreview.Image"), System.Drawing.Image)
        Me.cmdPreview.Location = New System.Drawing.Point(466, 10)
        Me.cmdPreview.Name = "cmdPreview"
        Me.cmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPreview.Size = New System.Drawing.Size(67, 35)
        Me.cmdPreview.TabIndex = 39
        Me.cmdPreview.Text = "Preview"
        Me.cmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPreview, "Print Preview")
        Me.cmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(400, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 35)
        Me.cmdPrint.TabIndex = 38
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
        Me.CmdDelete.Location = New System.Drawing.Point(334, 10)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 35)
        Me.CmdDelete.TabIndex = 37
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(268, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 35)
        Me.cmdSavePrint.TabIndex = 36
        Me.cmdSavePrint.Text = "SavePrint"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(202, 10)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 35)
        Me.CmdSave.TabIndex = 35
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
        Me.CmdModify.Location = New System.Drawing.Point(136, 10)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 35)
        Me.CmdModify.TabIndex = 34
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
        Me.CmdAdd.Location = New System.Drawing.Point(70, 10)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 35)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraView
        '
        Me.FraView.BackColor = System.Drawing.SystemColors.Control
        Me.FraView.Controls.Add(Me.cboPaidBy)
        Me.FraView.Controls.Add(Me.txtPaymentDate)
        Me.FraView.Controls.Add(Me.Frame2)
        Me.FraView.Controls.Add(Me.txtAccountGroupCode)
        Me.FraView.Controls.Add(Me.txtEstableCode)
        Me.FraView.Controls.Add(Me.txtRefDate)
        Me.FraView.Controls.Add(Me.txtRefNo)
        Me.FraView.Controls.Add(Me.Frame4)
        Me.FraView.Controls.Add(Me.Frame5)
        Me.FraView.Controls.Add(Me.Frame3)
        Me.FraView.Controls.Add(Me.Label8)
        Me.FraView.Controls.Add(Me.Label5)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Controls.Add(Me.Label3)
        Me.FraView.Controls.Add(Me.Label2)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -4)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(749, 415)
        Me.FraView.TabIndex = 33
        Me.FraView.TabStop = False
        '
        'cboPaidBy
        '
        Me.cboPaidBy.BackColor = System.Drawing.SystemColors.Window
        Me.cboPaidBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPaidBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPaidBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaidBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPaidBy.Location = New System.Drawing.Point(650, 34)
        Me.cboPaidBy.Name = "cboPaidBy"
        Me.cboPaidBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPaidBy.Size = New System.Drawing.Size(93, 22)
        Me.cboPaidBy.TabIndex = 6
        '
        'txtPaymentDate
        '
        Me.txtPaymentDate.AcceptsReturn = True
        Me.txtPaymentDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtPaymentDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPaymentDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPaymentDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPaymentDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPaymentDate.Location = New System.Drawing.Point(650, 12)
        Me.txtPaymentDate.MaxLength = 0
        Me.txtPaymentDate.Name = "txtPaymentDate"
        Me.txtPaymentDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPaymentDate.Size = New System.Drawing.Size(92, 19)
        Me.txtPaymentDate.TabIndex = 3
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.txtEmperDueDate)
        Me.Frame2.Controls.Add(Me.txtEmpDueDate)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 54)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(749, 43)
        Me.Frame2.TabIndex = 50
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Dues For the Month of :"
        '
        'txtEmperDueDate
        '
        Me.txtEmperDueDate.AcceptsReturn = True
        Me.txtEmperDueDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmperDueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmperDueDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmperDueDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmperDueDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmperDueDate.Location = New System.Drawing.Point(628, 14)
        Me.txtEmperDueDate.MaxLength = 0
        Me.txtEmperDueDate.Name = "txtEmperDueDate"
        Me.txtEmperDueDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmperDueDate.Size = New System.Drawing.Size(104, 19)
        Me.txtEmperDueDate.TabIndex = 8
        '
        'txtEmpDueDate
        '
        Me.txtEmpDueDate.AcceptsReturn = True
        Me.txtEmpDueDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpDueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpDueDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpDueDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpDueDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpDueDate.Location = New System.Drawing.Point(242, 14)
        Me.txtEmpDueDate.MaxLength = 0
        Me.txtEmpDueDate.Name = "txtEmpDueDate"
        Me.txtEmpDueDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpDueDate.Size = New System.Drawing.Size(104, 19)
        Me.txtEmpDueDate.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(134, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(97, 14)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "Employees Share :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(528, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(89, 14)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Employer Share :"
        '
        'txtAccountGroupCode
        '
        Me.txtAccountGroupCode.AcceptsReturn = True
        Me.txtAccountGroupCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtAccountGroupCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAccountGroupCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAccountGroupCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAccountGroupCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAccountGroupCode.Location = New System.Drawing.Point(388, 34)
        Me.txtAccountGroupCode.MaxLength = 0
        Me.txtAccountGroupCode.Name = "txtAccountGroupCode"
        Me.txtAccountGroupCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAccountGroupCode.Size = New System.Drawing.Size(92, 19)
        Me.txtAccountGroupCode.TabIndex = 5
        '
        'txtEstableCode
        '
        Me.txtEstableCode.AcceptsReturn = True
        Me.txtEstableCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEstableCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEstableCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEstableCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEstableCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEstableCode.Location = New System.Drawing.Point(150, 34)
        Me.txtEstableCode.MaxLength = 0
        Me.txtEstableCode.Name = "txtEstableCode"
        Me.txtEstableCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEstableCode.Size = New System.Drawing.Size(92, 19)
        Me.txtEstableCode.TabIndex = 4
        '
        'txtRefDate
        '
        Me.txtRefDate.AcceptsReturn = True
        Me.txtRefDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefDate.Location = New System.Drawing.Point(388, 12)
        Me.txtRefDate.MaxLength = 0
        Me.txtRefDate.Name = "txtRefDate"
        Me.txtRefDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefDate.Size = New System.Drawing.Size(92, 19)
        Me.txtRefDate.TabIndex = 2
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefNo.Location = New System.Drawing.Point(150, 12)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(92, 19)
        Me.txtRefNo.TabIndex = 1
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtTotSubs_AC10C)
        Me.Frame4.Controls.Add(Me.txtTotSubs_AC1C)
        Me.Frame4.Controls.Add(Me.txtTotSubs_AC21C)
        Me.Frame4.Controls.Add(Me.txtTotWages_AC10C)
        Me.Frame4.Controls.Add(Me.txtTotWages_AC1C)
        Me.Frame4.Controls.Add(Me.txtTotWages_AC21C)
        Me.Frame4.Controls.Add(Me.txtTotSubs_AC10B)
        Me.Frame4.Controls.Add(Me.txtTotSubs_AC1B)
        Me.Frame4.Controls.Add(Me.txtTotSubs_AC21B)
        Me.Frame4.Controls.Add(Me.txtTotWages_AC10B)
        Me.Frame4.Controls.Add(Me.txtTotWages_AC1B)
        Me.Frame4.Controls.Add(Me.txtTotWages_AC21B)
        Me.Frame4.Controls.Add(Me.txtTotWages_AC21)
        Me.Frame4.Controls.Add(Me.txtTotWages_AC1)
        Me.Frame4.Controls.Add(Me.txtTotWages_AC10)
        Me.Frame4.Controls.Add(Me.txtTotSubs_AC21)
        Me.Frame4.Controls.Add(Me.txtTotSubs_AC1)
        Me.Frame4.Controls.Add(Me.txtTotSubs_AC10)
        Me.Frame4.Controls.Add(Me.Label13)
        Me.Frame4.Controls.Add(Me.Label12)
        Me.Frame4.Controls.Add(Me.Label11)
        Me.Frame4.Controls.Add(Me.Label10)
        Me.Frame4.Controls.Add(Me.Label9)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 92)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(749, 75)
        Me.Frame4.TabIndex = 53
        Me.Frame4.TabStop = False
        '
        'txtTotSubs_AC10C
        '
        Me.txtTotSubs_AC10C.AcceptsReturn = True
        Me.txtTotSubs_AC10C.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSubs_AC10C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSubs_AC10C.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSubs_AC10C.Enabled = False
        Me.txtTotSubs_AC10C.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSubs_AC10C.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSubs_AC10C.Location = New System.Drawing.Point(482, 28)
        Me.txtTotSubs_AC10C.MaxLength = 0
        Me.txtTotSubs_AC10C.Name = "txtTotSubs_AC10C"
        Me.txtTotSubs_AC10C.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSubs_AC10C.Size = New System.Drawing.Size(64, 19)
        Me.txtTotSubs_AC10C.TabIndex = 14
        '
        'txtTotSubs_AC1C
        '
        Me.txtTotSubs_AC1C.AcceptsReturn = True
        Me.txtTotSubs_AC1C.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSubs_AC1C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSubs_AC1C.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSubs_AC1C.Enabled = False
        Me.txtTotSubs_AC1C.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSubs_AC1C.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSubs_AC1C.Location = New System.Drawing.Point(284, 28)
        Me.txtTotSubs_AC1C.MaxLength = 0
        Me.txtTotSubs_AC1C.Name = "txtTotSubs_AC1C"
        Me.txtTotSubs_AC1C.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSubs_AC1C.Size = New System.Drawing.Size(64, 19)
        Me.txtTotSubs_AC1C.TabIndex = 11
        '
        'txtTotSubs_AC21C
        '
        Me.txtTotSubs_AC21C.AcceptsReturn = True
        Me.txtTotSubs_AC21C.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSubs_AC21C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSubs_AC21C.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSubs_AC21C.Enabled = False
        Me.txtTotSubs_AC21C.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSubs_AC21C.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSubs_AC21C.Location = New System.Drawing.Point(682, 28)
        Me.txtTotSubs_AC21C.MaxLength = 0
        Me.txtTotSubs_AC21C.Name = "txtTotSubs_AC21C"
        Me.txtTotSubs_AC21C.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSubs_AC21C.Size = New System.Drawing.Size(64, 19)
        Me.txtTotSubs_AC21C.TabIndex = 17
        '
        'txtTotWages_AC10C
        '
        Me.txtTotWages_AC10C.AcceptsReturn = True
        Me.txtTotWages_AC10C.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages_AC10C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages_AC10C.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages_AC10C.Enabled = False
        Me.txtTotWages_AC10C.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages_AC10C.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages_AC10C.Location = New System.Drawing.Point(482, 50)
        Me.txtTotWages_AC10C.MaxLength = 0
        Me.txtTotWages_AC10C.Name = "txtTotWages_AC10C"
        Me.txtTotWages_AC10C.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages_AC10C.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages_AC10C.TabIndex = 23
        '
        'txtTotWages_AC1C
        '
        Me.txtTotWages_AC1C.AcceptsReturn = True
        Me.txtTotWages_AC1C.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages_AC1C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages_AC1C.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages_AC1C.Enabled = False
        Me.txtTotWages_AC1C.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages_AC1C.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages_AC1C.Location = New System.Drawing.Point(284, 50)
        Me.txtTotWages_AC1C.MaxLength = 0
        Me.txtTotWages_AC1C.Name = "txtTotWages_AC1C"
        Me.txtTotWages_AC1C.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages_AC1C.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages_AC1C.TabIndex = 20
        '
        'txtTotWages_AC21C
        '
        Me.txtTotWages_AC21C.AcceptsReturn = True
        Me.txtTotWages_AC21C.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages_AC21C.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages_AC21C.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages_AC21C.Enabled = False
        Me.txtTotWages_AC21C.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages_AC21C.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages_AC21C.Location = New System.Drawing.Point(682, 50)
        Me.txtTotWages_AC21C.MaxLength = 0
        Me.txtTotWages_AC21C.Name = "txtTotWages_AC21C"
        Me.txtTotWages_AC21C.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages_AC21C.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages_AC21C.TabIndex = 26
        '
        'txtTotSubs_AC10B
        '
        Me.txtTotSubs_AC10B.AcceptsReturn = True
        Me.txtTotSubs_AC10B.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSubs_AC10B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSubs_AC10B.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSubs_AC10B.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSubs_AC10B.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSubs_AC10B.Location = New System.Drawing.Point(418, 28)
        Me.txtTotSubs_AC10B.MaxLength = 0
        Me.txtTotSubs_AC10B.Name = "txtTotSubs_AC10B"
        Me.txtTotSubs_AC10B.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSubs_AC10B.Size = New System.Drawing.Size(64, 19)
        Me.txtTotSubs_AC10B.TabIndex = 13
        '
        'txtTotSubs_AC1B
        '
        Me.txtTotSubs_AC1B.AcceptsReturn = True
        Me.txtTotSubs_AC1B.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSubs_AC1B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSubs_AC1B.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSubs_AC1B.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSubs_AC1B.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSubs_AC1B.Location = New System.Drawing.Point(220, 28)
        Me.txtTotSubs_AC1B.MaxLength = 0
        Me.txtTotSubs_AC1B.Name = "txtTotSubs_AC1B"
        Me.txtTotSubs_AC1B.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSubs_AC1B.Size = New System.Drawing.Size(64, 19)
        Me.txtTotSubs_AC1B.TabIndex = 10
        '
        'txtTotSubs_AC21B
        '
        Me.txtTotSubs_AC21B.AcceptsReturn = True
        Me.txtTotSubs_AC21B.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSubs_AC21B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSubs_AC21B.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSubs_AC21B.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSubs_AC21B.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSubs_AC21B.Location = New System.Drawing.Point(618, 28)
        Me.txtTotSubs_AC21B.MaxLength = 0
        Me.txtTotSubs_AC21B.Name = "txtTotSubs_AC21B"
        Me.txtTotSubs_AC21B.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSubs_AC21B.Size = New System.Drawing.Size(64, 19)
        Me.txtTotSubs_AC21B.TabIndex = 16
        '
        'txtTotWages_AC10B
        '
        Me.txtTotWages_AC10B.AcceptsReturn = True
        Me.txtTotWages_AC10B.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages_AC10B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages_AC10B.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages_AC10B.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages_AC10B.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages_AC10B.Location = New System.Drawing.Point(418, 50)
        Me.txtTotWages_AC10B.MaxLength = 0
        Me.txtTotWages_AC10B.Name = "txtTotWages_AC10B"
        Me.txtTotWages_AC10B.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages_AC10B.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages_AC10B.TabIndex = 22
        '
        'txtTotWages_AC1B
        '
        Me.txtTotWages_AC1B.AcceptsReturn = True
        Me.txtTotWages_AC1B.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages_AC1B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages_AC1B.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages_AC1B.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages_AC1B.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages_AC1B.Location = New System.Drawing.Point(220, 50)
        Me.txtTotWages_AC1B.MaxLength = 0
        Me.txtTotWages_AC1B.Name = "txtTotWages_AC1B"
        Me.txtTotWages_AC1B.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages_AC1B.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages_AC1B.TabIndex = 19
        '
        'txtTotWages_AC21B
        '
        Me.txtTotWages_AC21B.AcceptsReturn = True
        Me.txtTotWages_AC21B.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages_AC21B.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages_AC21B.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages_AC21B.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages_AC21B.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages_AC21B.Location = New System.Drawing.Point(618, 50)
        Me.txtTotWages_AC21B.MaxLength = 0
        Me.txtTotWages_AC21B.Name = "txtTotWages_AC21B"
        Me.txtTotWages_AC21B.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages_AC21B.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages_AC21B.TabIndex = 25
        '
        'txtTotWages_AC21
        '
        Me.txtTotWages_AC21.AcceptsReturn = True
        Me.txtTotWages_AC21.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages_AC21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages_AC21.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages_AC21.Enabled = False
        Me.txtTotWages_AC21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages_AC21.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages_AC21.Location = New System.Drawing.Point(554, 50)
        Me.txtTotWages_AC21.MaxLength = 0
        Me.txtTotWages_AC21.Name = "txtTotWages_AC21"
        Me.txtTotWages_AC21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages_AC21.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages_AC21.TabIndex = 24
        '
        'txtTotWages_AC1
        '
        Me.txtTotWages_AC1.AcceptsReturn = True
        Me.txtTotWages_AC1.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages_AC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages_AC1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages_AC1.Enabled = False
        Me.txtTotWages_AC1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages_AC1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages_AC1.Location = New System.Drawing.Point(156, 50)
        Me.txtTotWages_AC1.MaxLength = 0
        Me.txtTotWages_AC1.Name = "txtTotWages_AC1"
        Me.txtTotWages_AC1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages_AC1.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages_AC1.TabIndex = 18
        '
        'txtTotWages_AC10
        '
        Me.txtTotWages_AC10.AcceptsReturn = True
        Me.txtTotWages_AC10.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages_AC10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages_AC10.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages_AC10.Enabled = False
        Me.txtTotWages_AC10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages_AC10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages_AC10.Location = New System.Drawing.Point(354, 50)
        Me.txtTotWages_AC10.MaxLength = 0
        Me.txtTotWages_AC10.Name = "txtTotWages_AC10"
        Me.txtTotWages_AC10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages_AC10.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages_AC10.TabIndex = 21
        '
        'txtTotSubs_AC21
        '
        Me.txtTotSubs_AC21.AcceptsReturn = True
        Me.txtTotSubs_AC21.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSubs_AC21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSubs_AC21.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSubs_AC21.Enabled = False
        Me.txtTotSubs_AC21.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSubs_AC21.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSubs_AC21.Location = New System.Drawing.Point(554, 28)
        Me.txtTotSubs_AC21.MaxLength = 0
        Me.txtTotSubs_AC21.Name = "txtTotSubs_AC21"
        Me.txtTotSubs_AC21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSubs_AC21.Size = New System.Drawing.Size(64, 19)
        Me.txtTotSubs_AC21.TabIndex = 15
        '
        'txtTotSubs_AC1
        '
        Me.txtTotSubs_AC1.AcceptsReturn = True
        Me.txtTotSubs_AC1.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSubs_AC1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSubs_AC1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSubs_AC1.Enabled = False
        Me.txtTotSubs_AC1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSubs_AC1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSubs_AC1.Location = New System.Drawing.Point(156, 28)
        Me.txtTotSubs_AC1.MaxLength = 0
        Me.txtTotSubs_AC1.Name = "txtTotSubs_AC1"
        Me.txtTotSubs_AC1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSubs_AC1.Size = New System.Drawing.Size(64, 19)
        Me.txtTotSubs_AC1.TabIndex = 9
        '
        'txtTotSubs_AC10
        '
        Me.txtTotSubs_AC10.AcceptsReturn = True
        Me.txtTotSubs_AC10.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotSubs_AC10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotSubs_AC10.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotSubs_AC10.Enabled = False
        Me.txtTotSubs_AC10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotSubs_AC10.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotSubs_AC10.Location = New System.Drawing.Point(354, 28)
        Me.txtTotSubs_AC10.MaxLength = 0
        Me.txtTotSubs_AC10.Name = "txtTotSubs_AC10"
        Me.txtTotSubs_AC10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotSubs_AC10.Size = New System.Drawing.Size(64, 19)
        Me.txtTotSubs_AC10.TabIndex = 12
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(614, 10)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(59, 14)
        Me.Label13.TabIndex = 59
        Me.Label13.Text = "A/C No. 21"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(414, 12)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(59, 14)
        Me.Label12.TabIndex = 58
        Me.Label12.Text = "A/C No. 10"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(230, 12)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(53, 14)
        Me.Label11.TabIndex = 57
        Me.Label11.Text = "A/C No. 1"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(24, 52)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(94, 14)
        Me.Label10.TabIndex = 56
        Me.Label10.Text = "Total Wages Due :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(8, 30)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(129, 14)
        Me.Label9.TabIndex = 54
        Me.Label9.Text = "Total No. of Subscribers :"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.SprdMain)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(0, 162)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(747, 199)
        Me.Frame5.TabIndex = 44
        Me.Frame5.TabStop = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(743, 189)
        Me.SprdMain.TabIndex = 27
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtTotalAmount)
        Me.Frame3.Controls.Add(Me.txtDepositor)
        Me.Frame3.Controls.Add(Me.txtChqNo)
        Me.Frame3.Controls.Add(Me.txtBankName)
        Me.Frame3.Controls.Add(Me.txtChqDate)
        Me.Frame3.Controls.Add(Me.Label16)
        Me.Frame3.Controls.Add(Me.Label15)
        Me.Frame3.Controls.Add(Me.Label14)
        Me.Frame3.Controls.Add(Me.Label18)
        Me.Frame3.Controls.Add(Me.Label17)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 356)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(749, 59)
        Me.Frame3.TabIndex = 60
        Me.Frame3.TabStop = False
        '
        'txtTotalAmount
        '
        Me.txtTotalAmount.AcceptsReturn = True
        Me.txtTotalAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalAmount.Enabled = False
        Me.txtTotalAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalAmount.Location = New System.Drawing.Point(628, 12)
        Me.txtTotalAmount.MaxLength = 0
        Me.txtTotalAmount.Name = "txtTotalAmount"
        Me.txtTotalAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalAmount.Size = New System.Drawing.Size(104, 19)
        Me.txtTotalAmount.TabIndex = 29
        '
        'txtDepositor
        '
        Me.txtDepositor.AcceptsReturn = True
        Me.txtDepositor.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepositor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepositor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDepositor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepositor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDepositor.Location = New System.Drawing.Point(136, 12)
        Me.txtDepositor.MaxLength = 0
        Me.txtDepositor.Name = "txtDepositor"
        Me.txtDepositor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDepositor.Size = New System.Drawing.Size(200, 19)
        Me.txtDepositor.TabIndex = 28
        '
        'txtChqNo
        '
        Me.txtChqNo.AcceptsReturn = True
        Me.txtChqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChqNo.Location = New System.Drawing.Point(428, 34)
        Me.txtChqNo.MaxLength = 0
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqNo.Size = New System.Drawing.Size(104, 19)
        Me.txtChqNo.TabIndex = 31
        '
        'txtBankName
        '
        Me.txtBankName.AcceptsReturn = True
        Me.txtBankName.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBankName.Location = New System.Drawing.Point(136, 34)
        Me.txtBankName.MaxLength = 0
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankName.Size = New System.Drawing.Size(200, 19)
        Me.txtBankName.TabIndex = 30
        '
        'txtChqDate
        '
        Me.txtChqDate.AcceptsReturn = True
        Me.txtChqDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChqDate.Location = New System.Drawing.Point(628, 34)
        Me.txtChqDate.MaxLength = 0
        Me.txtChqDate.Name = "txtChqDate"
        Me.txtChqDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqDate.Size = New System.Drawing.Size(104, 19)
        Me.txtChqDate.TabIndex = 32
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(544, 14)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(74, 14)
        Me.Label16.TabIndex = 66
        Me.Label16.Text = "Total Amount :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(544, 36)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(75, 14)
        Me.Label15.TabIndex = 64
        Me.Label15.Text = "Cheque Date :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(350, 36)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(69, 14)
        Me.Label14.TabIndex = 63
        Me.Label14.Text = "Cheque No. :"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.BackColor = System.Drawing.SystemColors.Control
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(10, 14)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(102, 14)
        Me.Label18.TabIndex = 62
        Me.Label18.Text = "Name of Depositor :"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(12, 36)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(98, 14)
        Me.Label17.TabIndex = 61
        Me.Label17.Text = "Name of the Bank :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(540, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(92, 14)
        Me.Label8.TabIndex = 55
        Me.Label8.Text = "Date of Payment :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(504, 36)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(123, 14)
        Me.Label5.TabIndex = 49
        Me.Label5.Text = "Paid By Cheque / Cash :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(268, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(103, 14)
        Me.Label4.TabIndex = 48
        Me.Label4.Text = "Account Group No :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(6, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(123, 14)
        Me.Label3.TabIndex = 47
        Me.Label3.Text = "Establishment Code No :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(322, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(55, 14)
        Me.Label2.TabIndex = 46
        Me.Label2.Text = "Ref Date :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(96, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(46, 14)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Ref No :"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(749, 411)
        Me.SprdView.TabIndex = 43
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.cmdPreview)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.LblMKey)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 406)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 49)
        Me.FraMovement.TabIndex = 42
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(2, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 42
        '
        'LblMKey
        '
        Me.LblMKey.AutoSize = True
        Me.LblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMKey.Location = New System.Drawing.Point(680, 22)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(48, 14)
        Me.LblMKey.TabIndex = 65
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        'frmPFChallan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 456)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmPFChallan"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Combined Challan of A/c No 1, 2, 10, 21 & 22"
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        Me.FraMovement.PerformLayout()
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
#End Region
End Class