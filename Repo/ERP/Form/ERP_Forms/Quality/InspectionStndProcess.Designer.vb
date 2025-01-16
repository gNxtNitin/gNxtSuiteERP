Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmInspectionStndProcess
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
    Public WithEvents txtPartNo As System.Windows.Forms.TextBox
    Public WithEvents txtIDEN As System.Windows.Forms.TextBox
    Public WithEvents txtRevDate As System.Windows.Forms.TextBox
    Public WithEvents txtRevNo As System.Windows.Forms.TextBox
    Public WithEvents txtDocDate As System.Windows.Forms.TextBox
    Public WithEvents txtDocNo As System.Windows.Forms.TextBox
    Public WithEvents cboStage As System.Windows.Forms.ComboBox
    Public WithEvents cmdSearchStndNo As System.Windows.Forms.Button
    Public WithEvents cmdSearchICode As System.Windows.Forms.Button
    Public WithEvents txtItemCode As System.Windows.Forms.TextBox
    Public WithEvents txtStandardNo As System.Windows.Forms.TextBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents SprdRev As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblItemCode As System.Windows.Forms.Label
    Public WithEvents lblTdd As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents fraTop1 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInspectionStndProcess))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchStndNo = New System.Windows.Forms.Button()
        Me.cmdSearchICode = New System.Windows.Forms.Button()
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
        Me.txtPartNo = New System.Windows.Forms.TextBox()
        Me.txtIDEN = New System.Windows.Forms.TextBox()
        Me.txtRevDate = New System.Windows.Forms.TextBox()
        Me.txtRevNo = New System.Windows.Forms.TextBox()
        Me.txtDocDate = New System.Windows.Forms.TextBox()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.cboStage = New System.Windows.Forms.ComboBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.txtStandardNo = New System.Windows.Forms.TextBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.SprdRev = New AxFPSpreadADO.AxfpSpread()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblItemCode = New System.Windows.Forms.Label()
        Me.lblTdd = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.fraTop1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdRev, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmdSearchStndNo
        '
        Me.cmdSearchStndNo.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchStndNo.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchStndNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchStndNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchStndNo.Image = CType(resources.GetObject("cmdSearchStndNo.Image"), System.Drawing.Image)
        Me.cmdSearchStndNo.Location = New System.Drawing.Point(218, 12)
        Me.cmdSearchStndNo.Name = "cmdSearchStndNo"
        Me.cmdSearchStndNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchStndNo.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchStndNo.TabIndex = 16
        Me.cmdSearchStndNo.TabStop = False
        Me.cmdSearchStndNo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchStndNo, "Search")
        Me.cmdSearchStndNo.UseVisualStyleBackColor = False
        '
        'cmdSearchICode
        '
        Me.cmdSearchICode.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchICode.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchICode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchICode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchICode.Image = CType(resources.GetObject("cmdSearchICode.Image"), System.Drawing.Image)
        Me.cmdSearchICode.Location = New System.Drawing.Point(218, 36)
        Me.cmdSearchICode.Name = "cmdSearchICode"
        Me.cmdSearchICode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchICode.Size = New System.Drawing.Size(23, 19)
        Me.cmdSearchICode.TabIndex = 15
        Me.cmdSearchICode.TabStop = False
        Me.cmdSearchICode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchICode, "Search")
        Me.cmdSearchICode.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Enabled = False
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(466, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 10
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(264, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 9
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
        Me.cmdPrint.Location = New System.Drawing.Point(398, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 8
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
        Me.CmdClose.Location = New System.Drawing.Point(600, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 7
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
        Me.CmdView.Location = New System.Drawing.Point(532, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 6
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
        Me.CmdDelete.Location = New System.Drawing.Point(330, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.CmdDelete.TabIndex = 5
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
        Me.CmdSave.Location = New System.Drawing.Point(196, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(67, 37)
        Me.CmdSave.TabIndex = 4
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
        Me.CmdModify.Location = New System.Drawing.Point(128, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(67, 37)
        Me.CmdModify.TabIndex = 3
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
        Me.CmdAdd.Location = New System.Drawing.Point(62, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.CmdAdd.TabIndex = 2
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'fraTop1
        '
        Me.fraTop1.BackColor = System.Drawing.SystemColors.Control
        Me.fraTop1.Controls.Add(Me.txtPartNo)
        Me.fraTop1.Controls.Add(Me.txtIDEN)
        Me.fraTop1.Controls.Add(Me.txtRevDate)
        Me.fraTop1.Controls.Add(Me.txtRevNo)
        Me.fraTop1.Controls.Add(Me.txtDocDate)
        Me.fraTop1.Controls.Add(Me.txtDocNo)
        Me.fraTop1.Controls.Add(Me.cboStage)
        Me.fraTop1.Controls.Add(Me.cmdSearchStndNo)
        Me.fraTop1.Controls.Add(Me.cmdSearchICode)
        Me.fraTop1.Controls.Add(Me.txtItemCode)
        Me.fraTop1.Controls.Add(Me.txtStandardNo)
        Me.fraTop1.Controls.Add(Me.SprdMain)
        Me.fraTop1.Controls.Add(Me.SprdRev)
        Me.fraTop1.Controls.Add(Me.Label8)
        Me.fraTop1.Controls.Add(Me.Label6)
        Me.fraTop1.Controls.Add(Me.Label5)
        Me.fraTop1.Controls.Add(Me.Label4)
        Me.fraTop1.Controls.Add(Me.Label3)
        Me.fraTop1.Controls.Add(Me.Label2)
        Me.fraTop1.Controls.Add(Me.Label1)
        Me.fraTop1.Controls.Add(Me.lblItemCode)
        Me.fraTop1.Controls.Add(Me.lblTdd)
        Me.fraTop1.Controls.Add(Me.Label7)
        Me.fraTop1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraTop1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraTop1.Location = New System.Drawing.Point(0, -4)
        Me.fraTop1.Name = "fraTop1"
        Me.fraTop1.Padding = New System.Windows.Forms.Padding(0)
        Me.fraTop1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraTop1.Size = New System.Drawing.Size(751, 397)
        Me.fraTop1.TabIndex = 12
        Me.fraTop1.TabStop = False
        '
        'txtPartNo
        '
        Me.txtPartNo.AcceptsReturn = True
        Me.txtPartNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPartNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPartNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPartNo.Enabled = False
        Me.txtPartNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtPartNo.Location = New System.Drawing.Point(582, 84)
        Me.txtPartNo.MaxLength = 0
        Me.txtPartNo.Name = "txtPartNo"
        Me.txtPartNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPartNo.Size = New System.Drawing.Size(157, 20)
        Me.txtPartNo.TabIndex = 34
        '
        'txtIDEN
        '
        Me.txtIDEN.AcceptsReturn = True
        Me.txtIDEN.BackColor = System.Drawing.SystemColors.Window
        Me.txtIDEN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIDEN.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIDEN.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIDEN.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtIDEN.Location = New System.Drawing.Point(582, 60)
        Me.txtIDEN.MaxLength = 0
        Me.txtIDEN.Name = "txtIDEN"
        Me.txtIDEN.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIDEN.Size = New System.Drawing.Size(157, 20)
        Me.txtIDEN.TabIndex = 31
        '
        'txtRevDate
        '
        Me.txtRevDate.AcceptsReturn = True
        Me.txtRevDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRevDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRevDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRevDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRevDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRevDate.Location = New System.Drawing.Point(348, 84)
        Me.txtRevDate.MaxLength = 0
        Me.txtRevDate.Name = "txtRevDate"
        Me.txtRevDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRevDate.Size = New System.Drawing.Size(93, 20)
        Me.txtRevDate.TabIndex = 29
        '
        'txtRevNo
        '
        Me.txtRevNo.AcceptsReturn = True
        Me.txtRevNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRevNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRevNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRevNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRevNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtRevNo.Location = New System.Drawing.Point(122, 84)
        Me.txtRevNo.MaxLength = 0
        Me.txtRevNo.Name = "txtRevNo"
        Me.txtRevNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRevNo.Size = New System.Drawing.Size(93, 20)
        Me.txtRevNo.TabIndex = 27
        '
        'txtDocDate
        '
        Me.txtDocDate.AcceptsReturn = True
        Me.txtDocDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDocDate.Location = New System.Drawing.Point(348, 60)
        Me.txtDocDate.MaxLength = 0
        Me.txtDocDate.Name = "txtDocDate"
        Me.txtDocDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocDate.Size = New System.Drawing.Size(93, 20)
        Me.txtDocDate.TabIndex = 25
        '
        'txtDocNo
        '
        Me.txtDocNo.AcceptsReturn = True
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDocNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtDocNo.Location = New System.Drawing.Point(122, 60)
        Me.txtDocNo.MaxLength = 0
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocNo.Size = New System.Drawing.Size(93, 20)
        Me.txtDocNo.TabIndex = 23
        '
        'cboStage
        '
        Me.cboStage.BackColor = System.Drawing.SystemColors.Window
        Me.cboStage.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStage.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStage.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboStage.Location = New System.Drawing.Point(478, 12)
        Me.cboStage.Name = "cboStage"
        Me.cboStage.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboStage.Size = New System.Drawing.Size(261, 22)
        Me.cboStage.TabIndex = 21
        '
        'txtItemCode
        '
        Me.txtItemCode.AcceptsReturn = True
        Me.txtItemCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtItemCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtItemCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtItemCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtItemCode.Location = New System.Drawing.Point(122, 36)
        Me.txtItemCode.MaxLength = 0
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtItemCode.Size = New System.Drawing.Size(93, 20)
        Me.txtItemCode.TabIndex = 14
        '
        'txtStandardNo
        '
        Me.txtStandardNo.AcceptsReturn = True
        Me.txtStandardNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtStandardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStandardNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtStandardNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStandardNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtStandardNo.Location = New System.Drawing.Point(122, 12)
        Me.txtStandardNo.MaxLength = 0
        Me.txtStandardNo.Name = "txtStandardNo"
        Me.txtStandardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtStandardNo.Size = New System.Drawing.Size(93, 20)
        Me.txtStandardNo.TabIndex = 13
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 104)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(747, 197)
        Me.SprdMain.TabIndex = 20
        '
        'SprdRev
        '
        Me.SprdRev.DataSource = Nothing
        Me.SprdRev.Location = New System.Drawing.Point(2, 302)
        Me.SprdRev.Name = "SprdRev"
        Me.SprdRev.OcxState = CType(resources.GetObject("SprdRev.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdRev.Size = New System.Drawing.Size(747, 93)
        Me.SprdRev.TabIndex = 33
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(532, 87)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(45, 13)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Part No"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(463, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(105, 13)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "IDEN. (Code If Any)"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(285, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "Rev Date"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(66, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(47, 13)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Rev No "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(285, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(54, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Doc Date"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(66, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Doc No "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(404, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Stage"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblItemCode
        '
        Me.lblItemCode.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblItemCode.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblItemCode.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemCode.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblItemCode.Location = New System.Drawing.Point(242, 36)
        Me.lblItemCode.Name = "lblItemCode"
        Me.lblItemCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblItemCode.Size = New System.Drawing.Size(497, 19)
        Me.lblItemCode.TabIndex = 19
        '
        'lblTdd
        '
        Me.lblTdd.AutoSize = True
        Me.lblTdd.BackColor = System.Drawing.SystemColors.Control
        Me.lblTdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTdd.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTdd.Location = New System.Drawing.Point(36, 40)
        Me.lblTdd.Name = "lblTdd"
        Me.lblTdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTdd.Size = New System.Drawing.Size(60, 13)
        Me.lblTdd.TabIndex = 18
        Me.lblTdd.Text = "Item Code"
        Me.lblTdd.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(36, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(71, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Standard No"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 0)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 14
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(750, 393)
        Me.SprdView.TabIndex = 0
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
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 394)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(751, 51)
        Me.FraMovement.TabIndex = 1
        Me.FraMovement.TabStop = False
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(10, 18)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(45, 17)
        Me.lblMkey.TabIndex = 11
        Me.lblMkey.Text = "lblMkey"
        '
        'frmInspectionStndProcess
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(752, 446)
        Me.Controls.Add(Me.fraTop1)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(-242, 29)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmInspectionStndProcess"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Inspection Standard"
        Me.fraTop1.ResumeLayout(False)
        Me.fraTop1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdRev, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
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