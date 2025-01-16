Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGenRecord
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
        'Me.MdiParent = Quality.Master

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
    Public WithEvents txtLastUnitReading As System.Windows.Forms.TextBox
    Public WithEvents fraUnitMtrReading As System.Windows.Forms.GroupBox
    Public WithEvents txtUnits As System.Windows.Forms.TextBox
    Public WithEvents txtLastReading As System.Windows.Forms.TextBox
    Public WithEvents fraHoursMtrReading As System.Windows.Forms.GroupBox
    Public WithEvents txtTotalTime As System.Windows.Forms.TextBox
    Public WithEvents txtOffTime As System.Windows.Forms.TextBox
    Public WithEvents txtOffDate As System.Windows.Forms.TextBox
    Public WithEvents txtOnTime As System.Windows.Forms.TextBox
    Public WithEvents txtOnDate As System.Windows.Forms.TextBox
    Public WithEvents txtNumber As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchNumber As System.Windows.Forms.Button
    Public WithEvents txtMachineNo As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchMachine As System.Windows.Forms.Button
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtDoneBy As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchDoneBy As System.Windows.Forms.Button
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents lblUnits As System.Windows.Forms.Label
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblMachineNo As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents lblDoneBy As System.Windows.Forms.Label
    Public WithEvents lblType As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGenRecord))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchNumber = New System.Windows.Forms.Button()
        Me.cmdSearchMachine = New System.Windows.Forms.Button()
        Me.cmdSearchDoneBy = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.fraTop1 = New System.Windows.Forms.GroupBox()
        Me.fraUnitMtrReading = New System.Windows.Forms.GroupBox()
        Me.txtLastUnitReading = New System.Windows.Forms.TextBox()
        Me.txtUnits = New System.Windows.Forms.TextBox()
        Me.fraHoursMtrReading = New System.Windows.Forms.GroupBox()
        Me.txtLastReading = New System.Windows.Forms.TextBox()
        Me.txtTotalTime = New System.Windows.Forms.TextBox()
        Me.txtOffTime = New System.Windows.Forms.TextBox()
        Me.txtOffDate = New System.Windows.Forms.TextBox()
        Me.txtOnTime = New System.Windows.Forms.TextBox()
        Me.txtOnDate = New System.Windows.Forms.TextBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.txtMachineNo = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtDoneBy = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.lblUnits = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblMachineNo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblDoneBy = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.fraTop1.SuspendLayout()
        Me.fraUnitMtrReading.SuspendLayout()
        Me.fraHoursMtrReading.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchNumber
        '
        Me.cmdSearchNumber.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchNumber.Image = CType(resources.GetObject("cmdSearchNumber.Image"), System.Drawing.Image)
        Me.cmdSearchNumber.Location = New System.Drawing.Point(206, 16)
        Me.cmdSearchNumber.Name = "cmdSearchNumber"
        Me.cmdSearchNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchNumber.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchNumber.TabIndex = 32
        Me.cmdSearchNumber.TabStop = False
        Me.cmdSearchNumber.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchNumber, "Search")
        Me.cmdSearchNumber.UseVisualStyleBackColor = False
        '
        'cmdSearchMachine
        '
        Me.cmdSearchMachine.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMachine.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMachine.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMachine.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMachine.Image = CType(resources.GetObject("cmdSearchMachine.Image"), System.Drawing.Image)
        Me.cmdSearchMachine.Location = New System.Drawing.Point(206, 40)
        Me.cmdSearchMachine.Name = "cmdSearchMachine"
        Me.cmdSearchMachine.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMachine.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchMachine.TabIndex = 29
        Me.cmdSearchMachine.TabStop = False
        Me.cmdSearchMachine.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMachine, "Search")
        Me.cmdSearchMachine.UseVisualStyleBackColor = False
        '
        'cmdSearchDoneBy
        '
        Me.cmdSearchDoneBy.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchDoneBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchDoneBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchDoneBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchDoneBy.Image = CType(resources.GetObject("cmdSearchDoneBy.Image"), System.Drawing.Image)
        Me.cmdSearchDoneBy.Location = New System.Drawing.Point(206, 112)
        Me.cmdSearchDoneBy.Name = "cmdSearchDoneBy"
        Me.cmdSearchDoneBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchDoneBy.Size = New System.Drawing.Size(27, 19)
        Me.cmdSearchDoneBy.TabIndex = 25
        Me.cmdSearchDoneBy.TabStop = False
        Me.cmdSearchDoneBy.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchDoneBy, "Search")
        Me.cmdSearchDoneBy.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(484, 12)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 35)
        Me.CmdPreview.TabIndex = 17
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(282, 12)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 35)
        Me.cmdSavePrint.TabIndex = 14
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(416, 12)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 35)
        Me.cmdPrint.TabIndex = 16
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(618, 12)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 35)
        Me.CmdClose.TabIndex = 19
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
        Me.CmdView.Location = New System.Drawing.Point(550, 12)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 35)
        Me.CmdView.TabIndex = 18
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "List View")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(348, 12)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 35)
        Me.CmdDelete.TabIndex = 15
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
        Me.CmdSave.Location = New System.Drawing.Point(214, 12)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 35)
        Me.CmdSave.TabIndex = 13
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
        Me.CmdModify.Location = New System.Drawing.Point(144, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 35)
        Me.CmdModify.TabIndex = 12
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
        Me.CmdAdd.Location = New System.Drawing.Point(78, 12)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 35)
        Me.CmdAdd.TabIndex = 11
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.fraUnitMtrReading)
        Me.fraTop1.Controls.Add(Me.txtUnits)
        Me.fraTop1.Controls.Add(Me.fraHoursMtrReading)
        Me.fraTop1.Controls.Add(Me.txtTotalTime)
        Me.fraTop1.Controls.Add(Me.txtOffTime)
        Me.fraTop1.Controls.Add(Me.txtOffDate)
        Me.fraTop1.Controls.Add(Me.txtOnTime)
        Me.fraTop1.Controls.Add(Me.txtOnDate)
        Me.fraTop1.Controls.Add(Me.txtNumber)
        Me.fraTop1.Controls.Add(Me.cmdSearchNumber)
        Me.fraTop1.Controls.Add(Me.txtMachineNo)
        Me.fraTop1.Controls.Add(Me.cmdSearchMachine)
        Me.fraTop1.Controls.Add(Me.txtRemarks)
        Me.fraTop1.Controls.Add(Me.txtDoneBy)
        Me.fraTop1.Controls.Add(Me.cmdSearchDoneBy)
        Me.fraTop1.Controls.Add(Me.SprdMain)
        Me.fraTop1.Controls.Add(Me.lblUnits)
        Me.fraTop1.Controls.Add(Me.Label10)
        Me.fraTop1.Controls.Add(Me.Label9)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.lblMachineNo)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.lblDoneBy)
        Me.fraTop1.Controls.Add(Me.lblType)
        Me.fraTop1.Controls.Add(Me.lblMkey)
        Me.fraTop1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -6)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(767, 469)
        Me.fraTop1.TabIndex = 22
        Me.fraTop1.TabStop = False
        '
        'fraUnitMtrReading
        '
        Me.fraUnitMtrReading.BackColor = System.Drawing.SystemColors.Control
        Me.fraUnitMtrReading.Controls.Add(Me.txtLastUnitReading)
        Me.fraUnitMtrReading.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraUnitMtrReading.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraUnitMtrReading.Location = New System.Drawing.Point(592, 104)
        Me.fraUnitMtrReading.Name = "fraUnitMtrReading"
        Me.fraUnitMtrReading.Padding = New System.Windows.Forms.Padding(0)
        Me.fraUnitMtrReading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraUnitMtrReading.Size = New System.Drawing.Size(169, 49)
        Me.fraUnitMtrReading.TabIndex = 42
        Me.fraUnitMtrReading.TabStop = False
        Me.fraUnitMtrReading.Text = "Last Unit Meter Reading"
        '
        'txtLastUnitReading
        '
        Me.txtLastUnitReading.AcceptsReturn = True
        Me.txtLastUnitReading.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastUnitReading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLastUnitReading.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastUnitReading.Enabled = False
        Me.txtLastUnitReading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastUnitReading.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLastUnitReading.Location = New System.Drawing.Point(18, 20)
        Me.txtLastUnitReading.MaxLength = 0
        Me.txtLastUnitReading.Name = "txtLastUnitReading"
        Me.txtLastUnitReading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastUnitReading.Size = New System.Drawing.Size(131, 20)
        Me.txtLastUnitReading.TabIndex = 43
        Me.txtLastUnitReading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtUnits
        '
        Me.txtUnits.AcceptsReturn = True
        Me.txtUnits.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUnits.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnits.Enabled = False
        Me.txtUnits.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUnits.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtUnits.Location = New System.Drawing.Point(486, 88)
        Me.txtUnits.MaxLength = 0
        Me.txtUnits.Name = "txtUnits"
        Me.txtUnits.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnits.Size = New System.Drawing.Size(99, 20)
        Me.txtUnits.TabIndex = 7
        '
        'fraHoursMtrReading
        '
        Me.fraHoursMtrReading.BackColor = System.Drawing.SystemColors.Control
        Me.fraHoursMtrReading.Controls.Add(Me.txtLastReading)
        Me.fraHoursMtrReading.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraHoursMtrReading.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraHoursMtrReading.Location = New System.Drawing.Point(592, 48)
        Me.fraHoursMtrReading.Name = "fraHoursMtrReading"
        Me.fraHoursMtrReading.Padding = New System.Windows.Forms.Padding(0)
        Me.fraHoursMtrReading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraHoursMtrReading.Size = New System.Drawing.Size(169, 49)
        Me.fraHoursMtrReading.TabIndex = 39
        Me.fraHoursMtrReading.TabStop = False
        Me.fraHoursMtrReading.Text = "Last Hours Meter Reading"
        '
        'txtLastReading
        '
        Me.txtLastReading.AcceptsReturn = True
        Me.txtLastReading.BackColor = System.Drawing.SystemColors.Window
        Me.txtLastReading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLastReading.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLastReading.Enabled = False
        Me.txtLastReading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLastReading.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLastReading.Location = New System.Drawing.Point(18, 20)
        Me.txtLastReading.MaxLength = 0
        Me.txtLastReading.Name = "txtLastReading"
        Me.txtLastReading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLastReading.Size = New System.Drawing.Size(131, 20)
        Me.txtLastReading.TabIndex = 40
        Me.txtLastReading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalTime
        '
        Me.txtTotalTime.AcceptsReturn = True
        Me.txtTotalTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalTime.Enabled = False
        Me.txtTotalTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalTime.Location = New System.Drawing.Point(486, 64)
        Me.txtTotalTime.MaxLength = 0
        Me.txtTotalTime.Name = "txtTotalTime"
        Me.txtTotalTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalTime.Size = New System.Drawing.Size(99, 20)
        Me.txtTotalTime.TabIndex = 6
        '
        'txtOffTime
        '
        Me.txtOffTime.AcceptsReturn = True
        Me.txtOffTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtOffTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOffTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOffTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOffTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOffTime.Location = New System.Drawing.Point(277, 88)
        Me.txtOffTime.MaxLength = 0
        Me.txtOffTime.Name = "txtOffTime"
        Me.txtOffTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOffTime.Size = New System.Drawing.Size(99, 20)
        Me.txtOffTime.TabIndex = 5
        '
        'txtOffDate
        '
        Me.txtOffDate.AcceptsReturn = True
        Me.txtOffDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOffDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOffDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOffDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOffDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOffDate.Location = New System.Drawing.Point(277, 64)
        Me.txtOffDate.MaxLength = 0
        Me.txtOffDate.Name = "txtOffDate"
        Me.txtOffDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOffDate.Size = New System.Drawing.Size(99, 20)
        Me.txtOffDate.TabIndex = 4
        '
        'txtOnTime
        '
        Me.txtOnTime.AcceptsReturn = True
        Me.txtOnTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtOnTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOnTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOnTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOnTime.Location = New System.Drawing.Point(102, 88)
        Me.txtOnTime.MaxLength = 0
        Me.txtOnTime.Name = "txtOnTime"
        Me.txtOnTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOnTime.Size = New System.Drawing.Size(99, 20)
        Me.txtOnTime.TabIndex = 3
        '
        'txtOnDate
        '
        Me.txtOnDate.AcceptsReturn = True
        Me.txtOnDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOnDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOnDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOnDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOnDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOnDate.Location = New System.Drawing.Point(102, 64)
        Me.txtOnDate.MaxLength = 0
        Me.txtOnDate.Name = "txtOnDate"
        Me.txtOnDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOnDate.Size = New System.Drawing.Size(99, 20)
        Me.txtOnDate.TabIndex = 2
        '
        'txtNumber
        '
        Me.txtNumber.AcceptsReturn = True
        Me.txtNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtNumber.Location = New System.Drawing.Point(102, 16)
        Me.txtNumber.MaxLength = 0
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumber.Size = New System.Drawing.Size(99, 20)
        Me.txtNumber.TabIndex = 0
        '
        'txtMachineNo
        '
        Me.txtMachineNo.AcceptsReturn = True
        Me.txtMachineNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachineNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachineNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMachineNo.Location = New System.Drawing.Point(102, 40)
        Me.txtMachineNo.MaxLength = 0
        Me.txtMachineNo.Name = "txtMachineNo"
        Me.txtMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachineNo.Size = New System.Drawing.Size(99, 20)
        Me.txtMachineNo.TabIndex = 1
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(102, 136)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(483, 20)
        Me.txtRemarks.TabIndex = 9
        '
        'txtDoneBy
        '
        Me.txtDoneBy.AcceptsReturn = True
        Me.txtDoneBy.BackColor = System.Drawing.SystemColors.Window
        Me.txtDoneBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDoneBy.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDoneBy.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDoneBy.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDoneBy.Location = New System.Drawing.Point(102, 112)
        Me.txtDoneBy.MaxLength = 0
        Me.txtDoneBy.Name = "txtDoneBy"
        Me.txtDoneBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDoneBy.Size = New System.Drawing.Size(99, 20)
        Me.txtDoneBy.TabIndex = 8
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(4, 160)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(759, 307)
        Me.SprdMain.TabIndex = 10
        '
        'lblUnits
        '
        Me.lblUnits.AutoSize = True
        Me.lblUnits.BackColor = System.Drawing.SystemColors.Control
        Me.lblUnits.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblUnits.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUnits.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblUnits.Location = New System.Drawing.Point(380, 91)
        Me.lblUnits.Name = "lblUnits"
        Me.lblUnits.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblUnits.Size = New System.Drawing.Size(98, 13)
        Me.lblUnits.TabIndex = 41
        Me.lblUnits.Text = "Units Generated : "
        Me.lblUnits.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(412, 67)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(69, 13)
        Me.Label10.TabIndex = 38
        Me.Label10.Text = "Total Time : "
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(216, 91)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(60, 13)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "Off Time : "
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(216, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 36
        Me.Label8.Text = "Off Date : "
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(42, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 35
        Me.Label4.Text = "On Time : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(42, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 34
        Me.Label3.Text = "On Date : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(10, 19)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Number : "
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(21, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(76, 13)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Machine No : "
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMachineNo
        '
        Me.lblMachineNo.BackColor = System.Drawing.SystemColors.Control
        Me.lblMachineNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMachineNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMachineNo.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMachineNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMachineNo.Location = New System.Drawing.Point(236, 40)
        Me.lblMachineNo.Name = "lblMachineNo"
        Me.lblMachineNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMachineNo.Size = New System.Drawing.Size(349, 19)
        Me.lblMachineNo.TabIndex = 30
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(40, 139)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Remarks : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(41, 115)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Done By : "
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblDoneBy
        '
        Me.lblDoneBy.BackColor = System.Drawing.SystemColors.Control
        Me.lblDoneBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDoneBy.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblDoneBy.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDoneBy.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDoneBy.Location = New System.Drawing.Point(236, 112)
        Me.lblDoneBy.Name = "lblDoneBy"
        Me.lblDoneBy.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblDoneBy.Size = New System.Drawing.Size(349, 19)
        Me.lblDoneBy.TabIndex = 26
        '
        'lblType
        '
        Me.lblType.BackColor = System.Drawing.SystemColors.Control
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblType.Location = New System.Drawing.Point(400, 16)
        Me.lblType.Name = "lblType"
        Me.lblType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblType.Size = New System.Drawing.Size(45, 15)
        Me.lblType.TabIndex = 24
        Me.lblType.Text = "lblType"
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(290, 14)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 15)
        Me.lblMkey.TabIndex = 23
        Me.lblMkey.Text = "lblMkey"
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 24
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 458)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(767, 51)
        Me.FraMovement.TabIndex = 20
        Me.FraMovement.TabStop = False
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(2, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(765, 457)
        Me.SprdView.TabIndex = 21
        '
        'frmGenRecord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(767, 509)
        Me.Controls.Add(Me.fraTop1)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGenRecord"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Generators Data Recording"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        Me.fraUnitMtrReading.ResumeLayout(False)
        Me.fraUnitMtrReading.PerformLayout()
        Me.fraHoursMtrReading.ResumeLayout(False)
        Me.fraHoursMtrReading.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        'SprdView.DataSource = CType(ADataGrid, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class