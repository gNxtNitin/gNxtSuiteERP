Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmESICChallan
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
    Public WithEvents txtTotalContribution As System.Windows.Forms.TextBox
    Public WithEvents txtOnRollEmpCont As System.Windows.Forms.TextBox
    Public WithEvents txtOnRollEmperCont As System.Windows.Forms.TextBox
    Public WithEvents txtOtherEmperCont As System.Windows.Forms.TextBox
    Public WithEvents txtOtherEmpCont As System.Windows.Forms.TextBox
    Public WithEvents txtTotalEmperCont As System.Windows.Forms.TextBox
    Public WithEvents txtTotalEmpCont As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtChallanNo As System.Windows.Forms.TextBox
    Public WithEvents cboPaidBy As System.Windows.Forms.ComboBox
    Public WithEvents txtPaymentDate As System.Windows.Forms.TextBox
    Public WithEvents txtEstableCode As System.Windows.Forms.TextBox
    Public WithEvents txtRefDate As System.Windows.Forms.TextBox
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents txtTotEmp As System.Windows.Forms.TextBox
    Public WithEvents txtTotWages As System.Windows.Forms.TextBox
    Public WithEvents txtOtherEmp As System.Windows.Forms.TextBox
    Public WithEvents txtOtherWages As System.Windows.Forms.TextBox
    Public WithEvents txtOnRollWages As System.Windows.Forms.TextBox
    Public WithEvents txtOnRollEmp As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents txtBankDate As System.Windows.Forms.TextBox
    Public WithEvents txtBankSLNo As System.Windows.Forms.TextBox
    Public WithEvents txtDepositor As System.Windows.Forms.TextBox
    Public WithEvents txtTotalAmount As System.Windows.Forms.TextBox
    Public WithEvents txtDepositorCode As System.Windows.Forms.TextBox
    Public WithEvents txtChqNo As System.Windows.Forms.TextBox
    Public WithEvents txtBankName As System.Windows.Forms.TextBox
    Public WithEvents txtChqDate As System.Windows.Forms.TextBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label16 As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label18 As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmESICChallan))
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
        Me.txtTotalContribution = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtOnRollEmpCont = New System.Windows.Forms.TextBox()
        Me.txtOnRollEmperCont = New System.Windows.Forms.TextBox()
        Me.txtOtherEmperCont = New System.Windows.Forms.TextBox()
        Me.txtOtherEmpCont = New System.Windows.Forms.TextBox()
        Me.txtTotalEmperCont = New System.Windows.Forms.TextBox()
        Me.txtTotalEmpCont = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtChallanNo = New System.Windows.Forms.TextBox()
        Me.cboPaidBy = New System.Windows.Forms.ComboBox()
        Me.txtPaymentDate = New System.Windows.Forms.TextBox()
        Me.txtEstableCode = New System.Windows.Forms.TextBox()
        Me.txtRefDate = New System.Windows.Forms.TextBox()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtTotEmp = New System.Windows.Forms.TextBox()
        Me.txtTotWages = New System.Windows.Forms.TextBox()
        Me.txtOtherEmp = New System.Windows.Forms.TextBox()
        Me.txtOtherWages = New System.Windows.Forms.TextBox()
        Me.txtOnRollWages = New System.Windows.Forms.TextBox()
        Me.txtOnRollEmp = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.txtBankDate = New System.Windows.Forms.TextBox()
        Me.txtBankSLNo = New System.Windows.Forms.TextBox()
        Me.txtDepositor = New System.Windows.Forms.TextBox()
        Me.txtTotalAmount = New System.Windows.Forms.TextBox()
        Me.txtDepositorCode = New System.Windows.Forms.TextBox()
        Me.txtChqNo = New System.Windows.Forms.TextBox()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.txtChqDate = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.FraView.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.Frame4.SuspendLayout()
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
        Me.CmdClose.TabIndex = 36
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
        Me.CmdView.TabIndex = 35
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
        Me.cmdPreview.TabIndex = 34
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
        Me.cmdPrint.TabIndex = 33
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
        Me.CmdDelete.TabIndex = 32
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
        Me.cmdSavePrint.TabIndex = 31
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
        Me.CmdSave.TabIndex = 30
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
        Me.CmdModify.TabIndex = 29
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
        Me.FraView.Controls.Add(Me.txtTotalContribution)
        Me.FraView.Controls.Add(Me.Frame1)
        Me.FraView.Controls.Add(Me.txtChallanNo)
        Me.FraView.Controls.Add(Me.cboPaidBy)
        Me.FraView.Controls.Add(Me.txtPaymentDate)
        Me.FraView.Controls.Add(Me.txtEstableCode)
        Me.FraView.Controls.Add(Me.txtRefDate)
        Me.FraView.Controls.Add(Me.txtRefNo)
        Me.FraView.Controls.Add(Me.Frame4)
        Me.FraView.Controls.Add(Me.Frame3)
        Me.FraView.Controls.Add(Me.Label12)
        Me.FraView.Controls.Add(Me.Label4)
        Me.FraView.Controls.Add(Me.Label8)
        Me.FraView.Controls.Add(Me.Label5)
        Me.FraView.Controls.Add(Me.Label3)
        Me.FraView.Controls.Add(Me.Label2)
        Me.FraView.Controls.Add(Me.Label1)
        Me.FraView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraView.Location = New System.Drawing.Point(0, -4)
        Me.FraView.Name = "FraView"
        Me.FraView.Padding = New System.Windows.Forms.Padding(0)
        Me.FraView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraView.Size = New System.Drawing.Size(749, 271)
        Me.FraView.TabIndex = 28
        Me.FraView.TabStop = False
        '
        'txtTotalContribution
        '
        Me.txtTotalContribution.AcceptsReturn = True
        Me.txtTotalContribution.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalContribution.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalContribution.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalContribution.Enabled = False
        Me.txtTotalContribution.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalContribution.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalContribution.Location = New System.Drawing.Point(622, 122)
        Me.txtTotalContribution.MaxLength = 0
        Me.txtTotalContribution.Name = "txtTotalContribution"
        Me.txtTotalContribution.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalContribution.Size = New System.Drawing.Size(94, 19)
        Me.txtTotalContribution.TabIndex = 19
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtOnRollEmpCont)
        Me.Frame1.Controls.Add(Me.txtOnRollEmperCont)
        Me.Frame1.Controls.Add(Me.txtOtherEmperCont)
        Me.Frame1.Controls.Add(Me.txtOtherEmpCont)
        Me.Frame1.Controls.Add(Me.txtTotalEmperCont)
        Me.Frame1.Controls.Add(Me.txtTotalEmpCont)
        Me.Frame1.Controls.Add(Me.Label11)
        Me.Frame1.Controls.Add(Me.Label6)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(386, 58)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(359, 59)
        Me.Frame1.TabIndex = 55
        Me.Frame1.TabStop = False
        '
        'txtOnRollEmpCont
        '
        Me.txtOnRollEmpCont.AcceptsReturn = True
        Me.txtOnRollEmpCont.BackColor = System.Drawing.SystemColors.Window
        Me.txtOnRollEmpCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOnRollEmpCont.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOnRollEmpCont.Enabled = False
        Me.txtOnRollEmpCont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnRollEmpCont.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOnRollEmpCont.Location = New System.Drawing.Point(156, 12)
        Me.txtOnRollEmpCont.MaxLength = 0
        Me.txtOnRollEmpCont.Name = "txtOnRollEmpCont"
        Me.txtOnRollEmpCont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOnRollEmpCont.Size = New System.Drawing.Size(64, 19)
        Me.txtOnRollEmpCont.TabIndex = 13
        '
        'txtOnRollEmperCont
        '
        Me.txtOnRollEmperCont.AcceptsReturn = True
        Me.txtOnRollEmperCont.BackColor = System.Drawing.SystemColors.Window
        Me.txtOnRollEmperCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOnRollEmperCont.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOnRollEmperCont.Enabled = False
        Me.txtOnRollEmperCont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnRollEmperCont.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOnRollEmperCont.Location = New System.Drawing.Point(156, 34)
        Me.txtOnRollEmperCont.MaxLength = 0
        Me.txtOnRollEmperCont.Name = "txtOnRollEmperCont"
        Me.txtOnRollEmperCont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOnRollEmperCont.Size = New System.Drawing.Size(64, 19)
        Me.txtOnRollEmperCont.TabIndex = 16
        '
        'txtOtherEmperCont
        '
        Me.txtOtherEmperCont.AcceptsReturn = True
        Me.txtOtherEmperCont.BackColor = System.Drawing.SystemColors.Window
        Me.txtOtherEmperCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOtherEmperCont.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOtherEmperCont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtherEmperCont.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOtherEmperCont.Location = New System.Drawing.Point(222, 34)
        Me.txtOtherEmperCont.MaxLength = 0
        Me.txtOtherEmperCont.Name = "txtOtherEmperCont"
        Me.txtOtherEmperCont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOtherEmperCont.Size = New System.Drawing.Size(64, 19)
        Me.txtOtherEmperCont.TabIndex = 17
        '
        'txtOtherEmpCont
        '
        Me.txtOtherEmpCont.AcceptsReturn = True
        Me.txtOtherEmpCont.BackColor = System.Drawing.SystemColors.Window
        Me.txtOtherEmpCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOtherEmpCont.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOtherEmpCont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtherEmpCont.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOtherEmpCont.Location = New System.Drawing.Point(222, 12)
        Me.txtOtherEmpCont.MaxLength = 0
        Me.txtOtherEmpCont.Name = "txtOtherEmpCont"
        Me.txtOtherEmpCont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOtherEmpCont.Size = New System.Drawing.Size(64, 19)
        Me.txtOtherEmpCont.TabIndex = 14
        '
        'txtTotalEmperCont
        '
        Me.txtTotalEmperCont.AcceptsReturn = True
        Me.txtTotalEmperCont.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalEmperCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalEmperCont.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalEmperCont.Enabled = False
        Me.txtTotalEmperCont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalEmperCont.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalEmperCont.Location = New System.Drawing.Point(288, 34)
        Me.txtTotalEmperCont.MaxLength = 0
        Me.txtTotalEmperCont.Name = "txtTotalEmperCont"
        Me.txtTotalEmperCont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalEmperCont.Size = New System.Drawing.Size(64, 19)
        Me.txtTotalEmperCont.TabIndex = 18
        '
        'txtTotalEmpCont
        '
        Me.txtTotalEmpCont.AcceptsReturn = True
        Me.txtTotalEmpCont.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalEmpCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalEmpCont.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalEmpCont.Enabled = False
        Me.txtTotalEmpCont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalEmpCont.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalEmpCont.Location = New System.Drawing.Point(288, 12)
        Me.txtTotalEmpCont.MaxLength = 0
        Me.txtTotalEmpCont.Name = "txtTotalEmpCont"
        Me.txtTotalEmpCont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalEmpCont.Size = New System.Drawing.Size(64, 19)
        Me.txtTotalEmpCont.TabIndex = 15
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(8, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(127, 14)
        Me.Label11.TabIndex = 57
        Me.Label11.Text = "Employee's Contribution :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(10, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(125, 14)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Employer's Contribution :"
        '
        'txtChallanNo
        '
        Me.txtChallanNo.AcceptsReturn = True
        Me.txtChallanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChallanNo.Location = New System.Drawing.Point(388, 34)
        Me.txtChallanNo.MaxLength = 0
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanNo.Size = New System.Drawing.Size(92, 19)
        Me.txtChallanNo.TabIndex = 5
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
        Me.Frame4.Controls.Add(Me.txtTotEmp)
        Me.Frame4.Controls.Add(Me.txtTotWages)
        Me.Frame4.Controls.Add(Me.txtOtherEmp)
        Me.Frame4.Controls.Add(Me.txtOtherWages)
        Me.Frame4.Controls.Add(Me.txtOnRollWages)
        Me.Frame4.Controls.Add(Me.txtOnRollEmp)
        Me.Frame4.Controls.Add(Me.Label10)
        Me.Frame4.Controls.Add(Me.Label9)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 58)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(359, 59)
        Me.Frame4.TabIndex = 43
        Me.Frame4.TabStop = False
        '
        'txtTotEmp
        '
        Me.txtTotEmp.AcceptsReturn = True
        Me.txtTotEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotEmp.Enabled = False
        Me.txtTotEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotEmp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotEmp.Location = New System.Drawing.Point(288, 12)
        Me.txtTotEmp.MaxLength = 0
        Me.txtTotEmp.Name = "txtTotEmp"
        Me.txtTotEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotEmp.Size = New System.Drawing.Size(64, 19)
        Me.txtTotEmp.TabIndex = 9
        '
        'txtTotWages
        '
        Me.txtTotWages.AcceptsReturn = True
        Me.txtTotWages.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotWages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotWages.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotWages.Enabled = False
        Me.txtTotWages.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotWages.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotWages.Location = New System.Drawing.Point(288, 34)
        Me.txtTotWages.MaxLength = 0
        Me.txtTotWages.Name = "txtTotWages"
        Me.txtTotWages.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotWages.Size = New System.Drawing.Size(64, 19)
        Me.txtTotWages.TabIndex = 12
        '
        'txtOtherEmp
        '
        Me.txtOtherEmp.AcceptsReturn = True
        Me.txtOtherEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtOtherEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOtherEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOtherEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtherEmp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOtherEmp.Location = New System.Drawing.Point(222, 12)
        Me.txtOtherEmp.MaxLength = 0
        Me.txtOtherEmp.Name = "txtOtherEmp"
        Me.txtOtherEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOtherEmp.Size = New System.Drawing.Size(64, 19)
        Me.txtOtherEmp.TabIndex = 8
        '
        'txtOtherWages
        '
        Me.txtOtherWages.AcceptsReturn = True
        Me.txtOtherWages.BackColor = System.Drawing.SystemColors.Window
        Me.txtOtherWages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOtherWages.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOtherWages.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtherWages.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOtherWages.Location = New System.Drawing.Point(222, 34)
        Me.txtOtherWages.MaxLength = 0
        Me.txtOtherWages.Name = "txtOtherWages"
        Me.txtOtherWages.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOtherWages.Size = New System.Drawing.Size(64, 19)
        Me.txtOtherWages.TabIndex = 11
        '
        'txtOnRollWages
        '
        Me.txtOnRollWages.AcceptsReturn = True
        Me.txtOnRollWages.BackColor = System.Drawing.SystemColors.Window
        Me.txtOnRollWages.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOnRollWages.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOnRollWages.Enabled = False
        Me.txtOnRollWages.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnRollWages.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOnRollWages.Location = New System.Drawing.Point(156, 34)
        Me.txtOnRollWages.MaxLength = 0
        Me.txtOnRollWages.Name = "txtOnRollWages"
        Me.txtOnRollWages.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOnRollWages.Size = New System.Drawing.Size(64, 19)
        Me.txtOnRollWages.TabIndex = 10
        '
        'txtOnRollEmp
        '
        Me.txtOnRollEmp.AcceptsReturn = True
        Me.txtOnRollEmp.BackColor = System.Drawing.SystemColors.Window
        Me.txtOnRollEmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOnRollEmp.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOnRollEmp.Enabled = False
        Me.txtOnRollEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnRollEmp.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOnRollEmp.Location = New System.Drawing.Point(156, 12)
        Me.txtOnRollEmp.MaxLength = 0
        Me.txtOnRollEmp.Name = "txtOnRollEmp"
        Me.txtOnRollEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOnRollEmp.Size = New System.Drawing.Size(64, 19)
        Me.txtOnRollEmp.TabIndex = 7
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(24, 36)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(72, 14)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Total Wages :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(8, 14)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(97, 14)
        Me.Label9.TabIndex = 44
        Me.Label9.Text = "No. of Employees :"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.txtBankDate)
        Me.Frame3.Controls.Add(Me.txtBankSLNo)
        Me.Frame3.Controls.Add(Me.txtDepositor)
        Me.Frame3.Controls.Add(Me.txtTotalAmount)
        Me.Frame3.Controls.Add(Me.txtDepositorCode)
        Me.Frame3.Controls.Add(Me.txtChqNo)
        Me.Frame3.Controls.Add(Me.txtBankName)
        Me.Frame3.Controls.Add(Me.txtChqDate)
        Me.Frame3.Controls.Add(Me.Label13)
        Me.Frame3.Controls.Add(Me.Label7)
        Me.Frame3.Controls.Add(Me.Label16)
        Me.Frame3.Controls.Add(Me.Label15)
        Me.Frame3.Controls.Add(Me.Label14)
        Me.Frame3.Controls.Add(Me.Label18)
        Me.Frame3.Controls.Add(Me.Label17)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 142)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(749, 129)
        Me.Frame3.TabIndex = 47
        Me.Frame3.TabStop = False
        '
        'txtBankDate
        '
        Me.txtBankDate.AcceptsReturn = True
        Me.txtBankDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBankDate.Location = New System.Drawing.Point(394, 78)
        Me.txtBankDate.MaxLength = 0
        Me.txtBankDate.Name = "txtBankDate"
        Me.txtBankDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankDate.Size = New System.Drawing.Size(104, 19)
        Me.txtBankDate.TabIndex = 27
        '
        'txtBankSLNo
        '
        Me.txtBankSLNo.AcceptsReturn = True
        Me.txtBankSLNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtBankSLNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBankSLNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBankSLNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBankSLNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBankSLNo.Location = New System.Drawing.Point(136, 78)
        Me.txtBankSLNo.MaxLength = 0
        Me.txtBankSLNo.Name = "txtBankSLNo"
        Me.txtBankSLNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBankSLNo.Size = New System.Drawing.Size(104, 19)
        Me.txtBankSLNo.TabIndex = 26
        '
        'txtDepositor
        '
        Me.txtDepositor.AcceptsReturn = True
        Me.txtDepositor.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepositor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepositor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDepositor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepositor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDepositor.Location = New System.Drawing.Point(208, 12)
        Me.txtDepositor.MaxLength = 0
        Me.txtDepositor.Name = "txtDepositor"
        Me.txtDepositor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDepositor.Size = New System.Drawing.Size(288, 19)
        Me.txtDepositor.TabIndex = 21
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
        Me.txtTotalAmount.TabIndex = 22
        '
        'txtDepositorCode
        '
        Me.txtDepositorCode.AcceptsReturn = True
        Me.txtDepositorCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepositorCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepositorCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDepositorCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepositorCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDepositorCode.Location = New System.Drawing.Point(136, 12)
        Me.txtDepositorCode.MaxLength = 0
        Me.txtDepositorCode.Name = "txtDepositorCode"
        Me.txtDepositorCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDepositorCode.Size = New System.Drawing.Size(70, 19)
        Me.txtDepositorCode.TabIndex = 20
        '
        'txtChqNo
        '
        Me.txtChqNo.AcceptsReturn = True
        Me.txtChqNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChqNo.Location = New System.Drawing.Point(136, 56)
        Me.txtChqNo.MaxLength = 0
        Me.txtChqNo.Name = "txtChqNo"
        Me.txtChqNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqNo.Size = New System.Drawing.Size(104, 19)
        Me.txtChqNo.TabIndex = 24
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
        Me.txtBankName.Size = New System.Drawing.Size(362, 19)
        Me.txtBankName.TabIndex = 23
        '
        'txtChqDate
        '
        Me.txtChqDate.AcceptsReturn = True
        Me.txtChqDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtChqDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChqDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChqDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChqDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChqDate.Location = New System.Drawing.Point(394, 56)
        Me.txtChqDate.MaxLength = 0
        Me.txtChqDate.Name = "txtChqDate"
        Me.txtChqDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChqDate.Size = New System.Drawing.Size(104, 19)
        Me.txtChqDate.TabIndex = 25
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(56, 80)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(71, 14)
        Me.Label13.TabIndex = 60
        Me.Label13.Text = "Bank Sl. No. :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(310, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(35, 14)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "Date :"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.SystemColors.Control
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(528, 14)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(87, 14)
        Me.Label16.TabIndex = 53
        Me.Label16.Text = "Challan Amount :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(310, 58)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(75, 14)
        Me.Label15.TabIndex = 51
        Me.Label15.Text = "Cheque Date :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(56, 58)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(69, 14)
        Me.Label14.TabIndex = 50
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
        Me.Label18.TabIndex = 49
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
        Me.Label17.TabIndex = 48
        Me.Label17.Text = "Name of the Bank :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(506, 124)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(95, 14)
        Me.Label12.TabIndex = 58
        Me.Label12.Text = "Total Contribution :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(310, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(64, 14)
        Me.Label4.TabIndex = 54
        Me.Label4.Text = "Challan No :"
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
        Me.Label8.TabIndex = 45
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
        Me.Label5.TabIndex = 42
        Me.Label5.Text = "Paid By Cheque / Cash :"
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
        Me.Label3.TabIndex = 41
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
        Me.Label2.TabIndex = 40
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
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Ref No :"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(749, 267)
        Me.SprdView.TabIndex = 38
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
        Me.FraMovement.Location = New System.Drawing.Point(0, 262)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(749, 49)
        Me.FraMovement.TabIndex = 37
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(2, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 37
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
        Me.LblMKey.TabIndex = 52
        Me.LblMKey.Text = "LblMKey"
        Me.LblMKey.Visible = False
        '
        'frmESICChallan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(749, 311)
        Me.Controls.Add(Me.FraView)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "frmESICChallan"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "E.S.I.C. Challan "
        Me.FraView.ResumeLayout(False)
        Me.FraView.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
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