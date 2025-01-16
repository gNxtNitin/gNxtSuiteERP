Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMachineCPCopy
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
    Public WithEvents txtMachineSpecNew As System.Windows.Forms.TextBox
    Public WithEvents txtNumberNew As System.Windows.Forms.TextBox
    Public WithEvents txtMachineDescNew As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchMachineDesc As System.Windows.Forms.Button
    Public WithEvents cmdSearchMachineSpec As System.Windows.Forms.Button
    Public WithEvents cmdSearchCheckType As System.Windows.Forms.Button
    Public WithEvents txtCheckTypeNew As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents txtCheckType As System.Windows.Forms.TextBox
    Public WithEvents txtMachineDesc As System.Windows.Forms.TextBox
    Public WithEvents cmdSearchNumber As System.Windows.Forms.Button
    Public WithEvents txtNumber As System.Windows.Forms.TextBox
    Public WithEvents txtMachineSpec As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents lblLabels As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMachineCPCopy))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchMachineDesc = New System.Windows.Forms.Button()
        Me.cmdSearchMachineSpec = New System.Windows.Forms.Button()
        Me.cmdSearchCheckType = New System.Windows.Forms.Button()
        Me.cmdSearchNumber = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtMachineSpecNew = New System.Windows.Forms.TextBox()
        Me.txtNumberNew = New System.Windows.Forms.TextBox()
        Me.txtMachineDescNew = New System.Windows.Forms.TextBox()
        Me.txtCheckTypeNew = New System.Windows.Forms.TextBox()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.txtCheckType = New System.Windows.Forms.TextBox()
        Me.txtMachineDesc = New System.Windows.Forms.TextBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.txtMachineSpec = New System.Windows.Forms.TextBox()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.lblLabels = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Frame1.SuspendLayout()
        Me.Frame4.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchMachineDesc
        '
        Me.cmdSearchMachineDesc.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMachineDesc.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMachineDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMachineDesc.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMachineDesc.Image = CType(resources.GetObject("cmdSearchMachineDesc.Image"), System.Drawing.Image)
        Me.cmdSearchMachineDesc.Location = New System.Drawing.Point(504, 34)
        Me.cmdSearchMachineDesc.Name = "cmdSearchMachineDesc"
        Me.cmdSearchMachineDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMachineDesc.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchMachineDesc.TabIndex = 20
        Me.cmdSearchMachineDesc.TabStop = False
        Me.cmdSearchMachineDesc.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMachineDesc, "Search")
        Me.cmdSearchMachineDesc.UseVisualStyleBackColor = False
        '
        'cmdSearchMachineSpec
        '
        Me.cmdSearchMachineSpec.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchMachineSpec.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMachineSpec.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMachineSpec.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMachineSpec.Image = CType(resources.GetObject("cmdSearchMachineSpec.Image"), System.Drawing.Image)
        Me.cmdSearchMachineSpec.Location = New System.Drawing.Point(504, 56)
        Me.cmdSearchMachineSpec.Name = "cmdSearchMachineSpec"
        Me.cmdSearchMachineSpec.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMachineSpec.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchMachineSpec.TabIndex = 19
        Me.cmdSearchMachineSpec.TabStop = False
        Me.cmdSearchMachineSpec.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMachineSpec, "Search")
        Me.cmdSearchMachineSpec.UseVisualStyleBackColor = False
        '
        'cmdSearchCheckType
        '
        Me.cmdSearchCheckType.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCheckType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCheckType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCheckType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCheckType.Image = CType(resources.GetObject("cmdSearchCheckType.Image"), System.Drawing.Image)
        Me.cmdSearchCheckType.Location = New System.Drawing.Point(504, 78)
        Me.cmdSearchCheckType.Name = "cmdSearchCheckType"
        Me.cmdSearchCheckType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCheckType.Size = New System.Drawing.Size(29, 19)
        Me.cmdSearchCheckType.TabIndex = 18
        Me.cmdSearchCheckType.TabStop = False
        Me.cmdSearchCheckType.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCheckType, "Search")
        Me.cmdSearchCheckType.UseVisualStyleBackColor = False
        '
        'cmdSearchNumber
        '
        Me.cmdSearchNumber.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchNumber.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchNumber.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchNumber.Image = CType(resources.GetObject("cmdSearchNumber.Image"), System.Drawing.Image)
        Me.cmdSearchNumber.Location = New System.Drawing.Point(222, 10)
        Me.cmdSearchNumber.Name = "cmdSearchNumber"
        Me.cmdSearchNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchNumber.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchNumber.TabIndex = 12
        Me.cmdSearchNumber.TabStop = False
        Me.cmdSearchNumber.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchNumber, "Search")
        Me.cmdSearchNumber.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(420, 14)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 37)
        Me.CmdClose.TabIndex = 9
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(68, 14)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(60, 37)
        Me.CmdSave.TabIndex = 8
        Me.CmdSave.Text = "&Copy"
        Me.CmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdSave, "Save Record")
        Me.CmdSave.UseVisualStyleBackColor = False
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtMachineSpecNew)
        Me.Frame1.Controls.Add(Me.txtNumberNew)
        Me.Frame1.Controls.Add(Me.txtMachineDescNew)
        Me.Frame1.Controls.Add(Me.cmdSearchMachineDesc)
        Me.Frame1.Controls.Add(Me.cmdSearchMachineSpec)
        Me.Frame1.Controls.Add(Me.cmdSearchCheckType)
        Me.Frame1.Controls.Add(Me.txtCheckTypeNew)
        Me.Frame1.Controls.Add(Me._lblLabels_7)
        Me.Frame1.Controls.Add(Me._lblLabels_5)
        Me.Frame1.Controls.Add(Me._lblLabels_3)
        Me.Frame1.Controls.Add(Me._lblLabels_2)
        Me.Frame1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 112)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(556, 102)
        Me.Frame1.TabIndex = 17
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "To:"
        '
        'txtMachineSpecNew
        '
        Me.txtMachineSpecNew.AcceptsReturn = True
        Me.txtMachineSpecNew.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachineSpecNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachineSpecNew.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineSpecNew.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineSpecNew.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMachineSpecNew.Location = New System.Drawing.Point(106, 56)
        Me.txtMachineSpecNew.MaxLength = 0
        Me.txtMachineSpecNew.Name = "txtMachineSpecNew"
        Me.txtMachineSpecNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachineSpecNew.Size = New System.Drawing.Size(397, 19)
        Me.txtMachineSpecNew.TabIndex = 6
        '
        'txtNumberNew
        '
        Me.txtNumberNew.AcceptsReturn = True
        Me.txtNumberNew.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumberNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumberNew.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumberNew.Enabled = False
        Me.txtNumberNew.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumberNew.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumberNew.Location = New System.Drawing.Point(106, 12)
        Me.txtNumberNew.MaxLength = 0
        Me.txtNumberNew.Name = "txtNumberNew"
        Me.txtNumberNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumberNew.Size = New System.Drawing.Size(115, 19)
        Me.txtNumberNew.TabIndex = 4
        '
        'txtMachineDescNew
        '
        Me.txtMachineDescNew.AcceptsReturn = True
        Me.txtMachineDescNew.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachineDescNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachineDescNew.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineDescNew.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineDescNew.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMachineDescNew.Location = New System.Drawing.Point(106, 34)
        Me.txtMachineDescNew.MaxLength = 0
        Me.txtMachineDescNew.Name = "txtMachineDescNew"
        Me.txtMachineDescNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachineDescNew.Size = New System.Drawing.Size(397, 19)
        Me.txtMachineDescNew.TabIndex = 5
        '
        'txtCheckTypeNew
        '
        Me.txtCheckTypeNew.AcceptsReturn = True
        Me.txtCheckTypeNew.BackColor = System.Drawing.SystemColors.Window
        Me.txtCheckTypeNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCheckTypeNew.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCheckTypeNew.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCheckTypeNew.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCheckTypeNew.Location = New System.Drawing.Point(106, 78)
        Me.txtCheckTypeNew.MaxLength = 0
        Me.txtCheckTypeNew.Name = "txtCheckTypeNew"
        Me.txtCheckTypeNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCheckTypeNew.Size = New System.Drawing.Size(397, 19)
        Me.txtCheckTypeNew.TabIndex = 7
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.AutoSize = True
        Me._lblLabels_7.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_7, CType(7, Short))
        Me._lblLabels_7.Location = New System.Drawing.Point(14, 59)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(78, 13)
        Me._lblLabels_7.TabIndex = 24
        Me._lblLabels_7.Text = "Specification :"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.AutoSize = True
        Me._lblLabels_5.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_5, CType(5, Short))
        Me._lblLabels_5.Location = New System.Drawing.Point(45, 14)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(54, 13)
        Me._lblLabels_5.TabIndex = 23
        Me._lblLabels_5.Text = "Number :"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.AutoSize = True
        Me._lblLabels_3.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_3, CType(3, Short))
        Me._lblLabels_3.Location = New System.Drawing.Point(7, 37)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(82, 13)
        Me._lblLabels_3.TabIndex = 22
        Me._lblLabels_3.Text = "Machine Desc :"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.AutoSize = True
        Me._lblLabels_2.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_2, CType(2, Short))
        Me._lblLabels_2.Location = New System.Drawing.Point(20, 81)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(70, 13)
        Me._lblLabels_2.TabIndex = 21
        Me._lblLabels_2.Text = "Check Type :"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.txtCheckType)
        Me.Frame4.Controls.Add(Me.txtMachineDesc)
        Me.Frame4.Controls.Add(Me.cmdSearchNumber)
        Me.Frame4.Controls.Add(Me.txtNumber)
        Me.Frame4.Controls.Add(Me.txtMachineSpec)
        Me.Frame4.Controls.Add(Me._lblLabels_1)
        Me.Frame4.Controls.Add(Me._lblLabels_6)
        Me.Frame4.Controls.Add(Me._lblLabels_0)
        Me.Frame4.Controls.Add(Me._lblLabels_4)
        Me.Frame4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(0, 2)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(556, 102)
        Me.Frame4.TabIndex = 11
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "From :"
        '
        'txtCheckType
        '
        Me.txtCheckType.AcceptsReturn = True
        Me.txtCheckType.BackColor = System.Drawing.SystemColors.Window
        Me.txtCheckType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCheckType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCheckType.Enabled = False
        Me.txtCheckType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCheckType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCheckType.Location = New System.Drawing.Point(106, 78)
        Me.txtCheckType.MaxLength = 0
        Me.txtCheckType.Name = "txtCheckType"
        Me.txtCheckType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCheckType.Size = New System.Drawing.Size(397, 19)
        Me.txtCheckType.TabIndex = 3
        '
        'txtMachineDesc
        '
        Me.txtMachineDesc.AcceptsReturn = True
        Me.txtMachineDesc.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachineDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachineDesc.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineDesc.Enabled = False
        Me.txtMachineDesc.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineDesc.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMachineDesc.Location = New System.Drawing.Point(106, 34)
        Me.txtMachineDesc.MaxLength = 0
        Me.txtMachineDesc.Name = "txtMachineDesc"
        Me.txtMachineDesc.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachineDesc.Size = New System.Drawing.Size(397, 19)
        Me.txtMachineDesc.TabIndex = 1
        '
        'txtNumber
        '
        Me.txtNumber.AcceptsReturn = True
        Me.txtNumber.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumber.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumber.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumber.Location = New System.Drawing.Point(106, 12)
        Me.txtNumber.MaxLength = 0
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumber.Size = New System.Drawing.Size(115, 19)
        Me.txtNumber.TabIndex = 0
        '
        'txtMachineSpec
        '
        Me.txtMachineSpec.AcceptsReturn = True
        Me.txtMachineSpec.BackColor = System.Drawing.SystemColors.Window
        Me.txtMachineSpec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMachineSpec.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMachineSpec.Enabled = False
        Me.txtMachineSpec.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMachineSpec.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMachineSpec.Location = New System.Drawing.Point(106, 56)
        Me.txtMachineSpec.MaxLength = 0
        Me.txtMachineSpec.Name = "txtMachineSpec"
        Me.txtMachineSpec.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMachineSpec.Size = New System.Drawing.Size(397, 19)
        Me.txtMachineSpec.TabIndex = 2
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.AutoSize = True
        Me._lblLabels_1.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_1, CType(1, Short))
        Me._lblLabels_1.Location = New System.Drawing.Point(20, 81)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(70, 13)
        Me._lblLabels_1.TabIndex = 16
        Me._lblLabels_1.Text = "Check Type :"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.AutoSize = True
        Me._lblLabels_6.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_6, CType(6, Short))
        Me._lblLabels_6.Location = New System.Drawing.Point(7, 37)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(82, 13)
        Me._lblLabels_6.TabIndex = 15
        Me._lblLabels_6.Text = "Machine Desc :"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.AutoSize = True
        Me._lblLabels_0.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_0, CType(0, Short))
        Me._lblLabels_0.Location = New System.Drawing.Point(45, 14)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(54, 13)
        Me._lblLabels_0.TabIndex = 14
        Me._lblLabels_0.Text = "Number :"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.AutoSize = True
        Me._lblLabels_4.BackColor = System.Drawing.SystemColors.Control
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabels.SetIndex(Me._lblLabels_4, CType(4, Short))
        Me._lblLabels_4.Location = New System.Drawing.Point(14, 59)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(78, 13)
        Me._lblLabels_4.TabIndex = 13
        Me._lblLabels_4.Text = "Specification :"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(0, 18)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 19
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 228)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(555, 55)
        Me.FraMovement.TabIndex = 10
        Me.FraMovement.TabStop = False
        '
        'frmMachineCPCopy
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(558, 292)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Report1)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMachineCPCopy"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Copy Preventive Maintenance Check Points"
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.lblLabels, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class