Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmPFMergeing
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
    Public WithEvents cmdSearchCont As System.Windows.Forms.Button
    Public WithEvents optContParti As System.Windows.Forms.RadioButton
    Public WithEvents optContAll As System.Windows.Forms.RadioButton
    Public WithEvents txtContractorName As System.Windows.Forms.TextBox
    Public WithEvents FraContractor As System.Windows.Forms.GroupBox
    Public WithEvents _optEmpType_1 As System.Windows.Forms.RadioButton
    Public WithEvents _optEmpType_0 As System.Windows.Forms.RadioButton
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents cmdSearch As System.Windows.Forms.Button
    Public WithEvents OptParti As System.Windows.Forms.RadioButton
    Public WithEvents OptAll As System.Windows.Forms.RadioButton
    Public WithEvents TxtCardNo As System.Windows.Forms.TextBox
    Public WithEvents TxtName As System.Windows.Forms.TextBox
    Public WithEvents FraEmp As System.Windows.Forms.GroupBox
    Public WithEvents txtTo As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtFrom As System.Windows.Forms.MaskedTextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents FraPeriod As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdOK As System.Windows.Forms.Button
    Public WithEvents PBar As System.Windows.Forms.ProgressBar
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents optEmpType As Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPFMergeing))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchCont = New System.Windows.Forms.Button()
        Me.cmdSearch = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.FraContractor = New System.Windows.Forms.GroupBox()
        Me.optContParti = New System.Windows.Forms.RadioButton()
        Me.optContAll = New System.Windows.Forms.RadioButton()
        Me.txtContractorName = New System.Windows.Forms.TextBox()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me._optEmpType_1 = New System.Windows.Forms.RadioButton()
        Me._optEmpType_0 = New System.Windows.Forms.RadioButton()
        Me.FraEmp = New System.Windows.Forms.GroupBox()
        Me.OptParti = New System.Windows.Forms.RadioButton()
        Me.OptAll = New System.Windows.Forms.RadioButton()
        Me.TxtCardNo = New System.Windows.Forms.TextBox()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.FraPeriod = New System.Windows.Forms.GroupBox()
        Me.txtTo = New System.Windows.Forms.MaskedTextBox()
        Me.txtFrom = New System.Windows.Forms.MaskedTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.CmdOK = New System.Windows.Forms.Button()
        Me.PBar = New System.Windows.Forms.ProgressBar()
        Me.optEmpType = New Microsoft.VisualBasic.Compatibility.VB6.RadioButtonArray(Me.components)
        Me.FraContractor.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.FraEmp.SuspendLayout()
        Me.FraPeriod.SuspendLayout()
        Me.Frame2.SuspendLayout()
        CType(Me.optEmpType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchCont
        '
        Me.cmdSearchCont.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearchCont.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchCont.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchCont.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchCont.Image = CType(resources.GetObject("cmdSearchCont.Image"), System.Drawing.Image)
        Me.cmdSearchCont.Location = New System.Drawing.Point(320, 28)
        Me.cmdSearchCont.Name = "cmdSearchCont"
        Me.cmdSearchCont.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchCont.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearchCont.TabIndex = 22
        Me.cmdSearchCont.TabStop = False
        Me.cmdSearchCont.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchCont, "Search")
        Me.cmdSearchCont.UseVisualStyleBackColor = False
        '
        'cmdSearch
        '
        Me.cmdSearch.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSearch.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearch.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearch.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearch.Image = CType(resources.GetObject("cmdSearch.Image"), System.Drawing.Image)
        Me.cmdSearch.Location = New System.Drawing.Point(212, 16)
        Me.cmdSearch.Name = "cmdSearch"
        Me.cmdSearch.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearch.Size = New System.Drawing.Size(27, 21)
        Me.cmdSearch.TabIndex = 4
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
        Me.CmdClose.Location = New System.Drawing.Point(294, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(60, 34)
        Me.CmdClose.TabIndex = 9
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the Form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'FraContractor
        '
        Me.FraContractor.BackColor = System.Drawing.SystemColors.Control
        Me.FraContractor.Controls.Add(Me.cmdSearchCont)
        Me.FraContractor.Controls.Add(Me.optContParti)
        Me.FraContractor.Controls.Add(Me.optContAll)
        Me.FraContractor.Controls.Add(Me.txtContractorName)
        Me.FraContractor.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraContractor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraContractor.Location = New System.Drawing.Point(0, 176)
        Me.FraContractor.Name = "FraContractor"
        Me.FraContractor.Padding = New System.Windows.Forms.Padding(0)
        Me.FraContractor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraContractor.Size = New System.Drawing.Size(357, 60)
        Me.FraContractor.TabIndex = 18
        Me.FraContractor.TabStop = False
        Me.FraContractor.Text = "Contractor"
        '
        'optContParti
        '
        Me.optContParti.AutoSize = True
        Me.optContParti.BackColor = System.Drawing.SystemColors.Control
        Me.optContParti.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContParti.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContParti.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContParti.Location = New System.Drawing.Point(194, 10)
        Me.optContParti.Name = "optContParti"
        Me.optContParti.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContParti.Size = New System.Drawing.Size(73, 18)
        Me.optContParti.TabIndex = 21
        Me.optContParti.TabStop = True
        Me.optContParti.Text = "Particular "
        Me.optContParti.UseVisualStyleBackColor = False
        '
        'optContAll
        '
        Me.optContAll.AutoSize = True
        Me.optContAll.BackColor = System.Drawing.SystemColors.Control
        Me.optContAll.Checked = True
        Me.optContAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.optContAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optContAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optContAll.Location = New System.Drawing.Point(94, 12)
        Me.optContAll.Name = "optContAll"
        Me.optContAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.optContAll.Size = New System.Drawing.Size(40, 18)
        Me.optContAll.TabIndex = 20
        Me.optContAll.TabStop = True
        Me.optContAll.Text = "All "
        Me.optContAll.UseVisualStyleBackColor = False
        '
        'txtContractorName
        '
        Me.txtContractorName.AcceptsReturn = True
        Me.txtContractorName.BackColor = System.Drawing.SystemColors.Window
        Me.txtContractorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtContractorName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtContractorName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContractorName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtContractorName.Location = New System.Drawing.Point(4, 33)
        Me.txtContractorName.MaxLength = 0
        Me.txtContractorName.Name = "txtContractorName"
        Me.txtContractorName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtContractorName.Size = New System.Drawing.Size(315, 21)
        Me.txtContractorName.TabIndex = 19
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me._optEmpType_1)
        Me.Frame1.Controls.Add(Me._optEmpType_0)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 54)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(358, 51)
        Me.Frame1.TabIndex = 15
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Employee"
        '
        '_optEmpType_1
        '
        Me._optEmpType_1.AutoSize = True
        Me._optEmpType_1.BackColor = System.Drawing.SystemColors.Control
        Me._optEmpType_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._optEmpType_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optEmpType_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optEmpType.SetIndex(Me._optEmpType_1, CType(1, Short))
        Me._optEmpType_1.Location = New System.Drawing.Point(198, 22)
        Me._optEmpType_1.Name = "_optEmpType_1"
        Me._optEmpType_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optEmpType_1.Size = New System.Drawing.Size(76, 18)
        Me._optEmpType_1.TabIndex = 17
        Me._optEmpType_1.TabStop = True
        Me._optEmpType_1.Text = "Contractor"
        Me._optEmpType_1.UseVisualStyleBackColor = False
        '
        '_optEmpType_0
        '
        Me._optEmpType_0.AutoSize = True
        Me._optEmpType_0.BackColor = System.Drawing.SystemColors.Control
        Me._optEmpType_0.Checked = True
        Me._optEmpType_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._optEmpType_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._optEmpType_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optEmpType.SetIndex(Me._optEmpType_0, CType(0, Short))
        Me._optEmpType_0.Location = New System.Drawing.Point(74, 22)
        Me._optEmpType_0.Name = "_optEmpType_0"
        Me._optEmpType_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._optEmpType_0.Size = New System.Drawing.Size(59, 18)
        Me._optEmpType_0.TabIndex = 16
        Me._optEmpType_0.TabStop = True
        Me._optEmpType_0.Text = "On Roll"
        Me._optEmpType_0.UseVisualStyleBackColor = False
        '
        'FraEmp
        '
        Me.FraEmp.BackColor = System.Drawing.SystemColors.Control
        Me.FraEmp.Controls.Add(Me.cmdSearch)
        Me.FraEmp.Controls.Add(Me.OptParti)
        Me.FraEmp.Controls.Add(Me.OptAll)
        Me.FraEmp.Controls.Add(Me.TxtCardNo)
        Me.FraEmp.Controls.Add(Me.TxtName)
        Me.FraEmp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraEmp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraEmp.Location = New System.Drawing.Point(0, 106)
        Me.FraEmp.Name = "FraEmp"
        Me.FraEmp.Padding = New System.Windows.Forms.Padding(0)
        Me.FraEmp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraEmp.Size = New System.Drawing.Size(357, 74)
        Me.FraEmp.TabIndex = 8
        Me.FraEmp.TabStop = False
        Me.FraEmp.Text = "Employee"
        '
        'OptParti
        '
        Me.OptParti.AutoSize = True
        Me.OptParti.BackColor = System.Drawing.SystemColors.Control
        Me.OptParti.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptParti.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptParti.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptParti.Location = New System.Drawing.Point(10, 46)
        Me.OptParti.Name = "OptParti"
        Me.OptParti.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptParti.Size = New System.Drawing.Size(73, 18)
        Me.OptParti.TabIndex = 1
        Me.OptParti.TabStop = True
        Me.OptParti.Text = "Particular "
        Me.OptParti.UseVisualStyleBackColor = False
        '
        'OptAll
        '
        Me.OptAll.AutoSize = True
        Me.OptAll.BackColor = System.Drawing.SystemColors.Control
        Me.OptAll.Checked = True
        Me.OptAll.Cursor = System.Windows.Forms.Cursors.Default
        Me.OptAll.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OptAll.ForeColor = System.Drawing.SystemColors.ControlText
        Me.OptAll.Location = New System.Drawing.Point(10, 24)
        Me.OptAll.Name = "OptAll"
        Me.OptAll.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.OptAll.Size = New System.Drawing.Size(40, 18)
        Me.OptAll.TabIndex = 0
        Me.OptAll.TabStop = True
        Me.OptAll.Text = "All "
        Me.OptAll.UseVisualStyleBackColor = False
        '
        'TxtCardNo
        '
        Me.TxtCardNo.AcceptsReturn = True
        Me.TxtCardNo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtCardNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtCardNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCardNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtCardNo.Location = New System.Drawing.Point(104, 16)
        Me.TxtCardNo.MaxLength = 0
        Me.TxtCardNo.Name = "TxtCardNo"
        Me.TxtCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtCardNo.Size = New System.Drawing.Size(107, 21)
        Me.TxtCardNo.TabIndex = 3
        '
        'TxtName
        '
        Me.TxtName.AcceptsReturn = True
        Me.TxtName.BackColor = System.Drawing.SystemColors.Window
        Me.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtName.Enabled = False
        Me.TxtName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtName.Location = New System.Drawing.Point(104, 44)
        Me.TxtName.MaxLength = 0
        Me.TxtName.Name = "TxtName"
        Me.TxtName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtName.Size = New System.Drawing.Size(249, 21)
        Me.TxtName.TabIndex = 5
        '
        'FraPeriod
        '
        Me.FraPeriod.BackColor = System.Drawing.SystemColors.Control
        Me.FraPeriod.Controls.Add(Me.txtTo)
        Me.FraPeriod.Controls.Add(Me.txtFrom)
        Me.FraPeriod.Controls.Add(Me.Label1)
        Me.FraPeriod.Controls.Add(Me.Label2)
        Me.FraPeriod.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraPeriod.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraPeriod.Location = New System.Drawing.Point(0, -2)
        Me.FraPeriod.Name = "FraPeriod"
        Me.FraPeriod.Padding = New System.Windows.Forms.Padding(0)
        Me.FraPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraPeriod.Size = New System.Drawing.Size(358, 55)
        Me.FraPeriod.TabIndex = 7
        Me.FraPeriod.TabStop = False
        Me.FraPeriod.Text = "Period"
        '
        'txtTo
        '
        Me.txtTo.AllowPromptAsInput = False
        Me.txtTo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTo.Location = New System.Drawing.Point(216, 20)
        Me.txtTo.Mask = "##/##/####"
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(80, 20)
        Me.txtTo.TabIndex = 11
        '
        'txtFrom
        '
        Me.txtFrom.AllowPromptAsInput = False
        Me.txtFrom.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFrom.Location = New System.Drawing.Point(92, 20)
        Me.txtFrom.Mask = "##/##/####"
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(80, 20)
        Me.txtFrom.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(54, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(37, 14)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "From :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(194, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(24, 14)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "To :"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.CmdClose)
        Me.Frame2.Controls.Add(Me.CmdOK)
        Me.Frame2.Controls.Add(Me.PBar)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(0, 236)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(357, 47)
        Me.Frame2.TabIndex = 2
        Me.Frame2.TabStop = False
        '
        'CmdOK
        '
        Me.CmdOK.BackColor = System.Drawing.SystemColors.Control
        Me.CmdOK.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdOK.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdOK.Location = New System.Drawing.Point(4, 10)
        Me.CmdOK.Name = "CmdOK"
        Me.CmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdOK.Size = New System.Drawing.Size(60, 33)
        Me.CmdOK.TabIndex = 6
        Me.CmdOK.Text = "Ok"
        Me.CmdOK.UseVisualStyleBackColor = False
        '
        'PBar
        '
        Me.PBar.Location = New System.Drawing.Point(68, 28)
        Me.PBar.Name = "PBar"
        Me.PBar.Size = New System.Drawing.Size(223, 13)
        Me.PBar.TabIndex = 10
        Me.PBar.Visible = False
        '
        'optEmpType
        '
        '
        'FrmPFMergeing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(357, 284)
        Me.Controls.Add(Me.FraContractor)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.FraEmp)
        Me.Controls.Add(Me.FraPeriod)
        Me.Controls.Add(Me.Frame2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "FrmPFMergeing"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Staff PF Merging"
        Me.FraContractor.ResumeLayout(False)
        Me.FraContractor.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.FraEmp.ResumeLayout(False)
        Me.FraEmp.PerformLayout()
        Me.FraPeriod.ResumeLayout(False)
        Me.FraPeriod.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        CType(Me.optEmpType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class