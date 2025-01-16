Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmMedicineIssue
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
    Public WithEvents txtSecurityName As System.Windows.Forms.TextBox
    Public WithEvents txtDisease As System.Windows.Forms.TextBox
    Public WithEvents txtEmpName As System.Windows.Forms.TextBox
    Public WithEvents txtDepartment As System.Windows.Forms.TextBox
    Public WithEvents txtMedicineName As System.Windows.Forms.TextBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtEmpCode As System.Windows.Forms.TextBox
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents cboPurpose As System.Windows.Forms.ComboBox
    Public WithEvents _Label8_9 As System.Windows.Forms.Label
    Public WithEvents _Label7_6 As System.Windows.Forms.Label
    Public WithEvents lblFilePath As System.Windows.Forms.Label
    Public WithEvents _Label9_4 As System.Windows.Forms.Label
    Public WithEvents _Label1_3 As System.Windows.Forms.Label
    Public WithEvents _Label5_1 As System.Windows.Forms.Label
    Public WithEvents _Label6_2 As System.Windows.Forms.Label
    Public WithEvents _Label7_5 As System.Windows.Forms.Label
    Public WithEvents _Label8_8 As System.Windows.Forms.Label
    Public WithEvents _Label3_7 As System.Windows.Forms.Label
    Public WithEvents FraCustSupp As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label1 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents Label3 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents Label5 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents Label6 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents Label7 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents Label8 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents Label9 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMedicineIssue))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.FraCustSupp = New System.Windows.Forms.GroupBox()
        Me.txtSecurityName = New System.Windows.Forms.TextBox()
        Me.txtDisease = New System.Windows.Forms.TextBox()
        Me.txtEmpName = New System.Windows.Forms.TextBox()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.txtMedicineName = New System.Windows.Forms.TextBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.txtEmpCode = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.cboPurpose = New System.Windows.Forms.ComboBox()
        Me._Label8_9 = New System.Windows.Forms.Label()
        Me._Label7_6 = New System.Windows.Forms.Label()
        Me.lblFilePath = New System.Windows.Forms.Label()
        Me._Label9_4 = New System.Windows.Forms.Label()
        Me._Label1_3 = New System.Windows.Forms.Label()
        Me._Label5_1 = New System.Windows.Forms.Label()
        Me._Label6_2 = New System.Windows.Forms.Label()
        Me._Label7_5 = New System.Windows.Forms.Label()
        Me._Label8_8 = New System.Windows.Forms.Label()
        Me._Label3_7 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.Label1 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label3 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label5 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label6 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label7 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label8 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label9 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.FraCustSupp.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(502, 9)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(61, 37)
        Me.CmdClose.TabIndex = 18
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
        Me.CmdView.Location = New System.Drawing.Point(442, 9)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(61, 37)
        Me.CmdView.TabIndex = 17
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
        Me.CmdPreview.Location = New System.Drawing.Point(382, 9)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(61, 37)
        Me.CmdPreview.TabIndex = 16
        Me.CmdPreview.Text = "Pre&view"
        Me.CmdPreview.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdPreview, "Preview")
        Me.CmdPreview.UseVisualStyleBackColor = False
        '
        'cmdPrint
        '
        Me.cmdPrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdPrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPrint.Image = CType(resources.GetObject("cmdPrint.Image"), System.Drawing.Image)
        Me.cmdPrint.Location = New System.Drawing.Point(322, 9)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(61, 37)
        Me.cmdPrint.TabIndex = 15
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(262, 9)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(61, 37)
        Me.CmdDelete.TabIndex = 14
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(202, 9)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(61, 37)
        Me.cmdSavePrint.TabIndex = 13
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save and Print Record")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'CmdSave
        '
        Me.CmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.CmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdSave.Image = CType(resources.GetObject("CmdSave.Image"), System.Drawing.Image)
        Me.CmdSave.Location = New System.Drawing.Point(142, 9)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(61, 37)
        Me.CmdSave.TabIndex = 12
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
        Me.CmdModify.Location = New System.Drawing.Point(82, 9)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(61, 37)
        Me.CmdModify.TabIndex = 11
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
        Me.CmdAdd.Location = New System.Drawing.Point(22, 9)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(61, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraCustSupp
        '
        Me.FraCustSupp.BackColor = System.Drawing.SystemColors.Control
        Me.FraCustSupp.Controls.Add(Me.txtSecurityName)
        Me.FraCustSupp.Controls.Add(Me.txtDisease)
        Me.FraCustSupp.Controls.Add(Me.txtEmpName)
        Me.FraCustSupp.Controls.Add(Me.txtDepartment)
        Me.FraCustSupp.Controls.Add(Me.txtMedicineName)
        Me.FraCustSupp.Controls.Add(Me.txtVNo)
        Me.FraCustSupp.Controls.Add(Me.txtVDate)
        Me.FraCustSupp.Controls.Add(Me.txtEmpCode)
        Me.FraCustSupp.Controls.Add(Me.txtRemarks)
        Me.FraCustSupp.Controls.Add(Me.cboPurpose)
        Me.FraCustSupp.Controls.Add(Me._Label8_9)
        Me.FraCustSupp.Controls.Add(Me._Label7_6)
        Me.FraCustSupp.Controls.Add(Me.lblFilePath)
        Me.FraCustSupp.Controls.Add(Me._Label9_4)
        Me.FraCustSupp.Controls.Add(Me._Label1_3)
        Me.FraCustSupp.Controls.Add(Me._Label5_1)
        Me.FraCustSupp.Controls.Add(Me._Label6_2)
        Me.FraCustSupp.Controls.Add(Me._Label7_5)
        Me.FraCustSupp.Controls.Add(Me._Label8_8)
        Me.FraCustSupp.Controls.Add(Me._Label3_7)
        Me.FraCustSupp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCustSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraCustSupp.Location = New System.Drawing.Point(0, 0)
        Me.FraCustSupp.Name = "FraCustSupp"
        Me.FraCustSupp.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCustSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCustSupp.Size = New System.Drawing.Size(601, 229)
        Me.FraCustSupp.TabIndex = 23
        Me.FraCustSupp.TabStop = False
        '
        'txtSecurityName
        '
        Me.txtSecurityName.AcceptsReturn = True
        Me.txtSecurityName.BackColor = System.Drawing.SystemColors.Window
        Me.txtSecurityName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSecurityName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSecurityName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSecurityName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSecurityName.Location = New System.Drawing.Point(140, 194)
        Me.txtSecurityName.MaxLength = 0
        Me.txtSecurityName.Name = "txtSecurityName"
        Me.txtSecurityName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSecurityName.Size = New System.Drawing.Size(449, 19)
        Me.txtSecurityName.TabIndex = 10
        '
        'txtDisease
        '
        Me.txtDisease.AcceptsReturn = True
        Me.txtDisease.BackColor = System.Drawing.SystemColors.Window
        Me.txtDisease.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDisease.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDisease.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisease.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDisease.Location = New System.Drawing.Point(140, 117)
        Me.txtDisease.MaxLength = 0
        Me.txtDisease.Name = "txtDisease"
        Me.txtDisease.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDisease.Size = New System.Drawing.Size(449, 19)
        Me.txtDisease.TabIndex = 7
        '
        'txtEmpName
        '
        Me.txtEmpName.AcceptsReturn = True
        Me.txtEmpName.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpName.Location = New System.Drawing.Point(232, 67)
        Me.txtEmpName.MaxLength = 0
        Me.txtEmpName.Name = "txtEmpName"
        Me.txtEmpName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpName.Size = New System.Drawing.Size(355, 19)
        Me.txtEmpName.TabIndex = 5
        '
        'txtDepartment
        '
        Me.txtDepartment.AcceptsReturn = True
        Me.txtDepartment.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepartment.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDepartment.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDepartment.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDepartment.Location = New System.Drawing.Point(140, 92)
        Me.txtDepartment.MaxLength = 0
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDepartment.Size = New System.Drawing.Size(449, 19)
        Me.txtDepartment.TabIndex = 6
        '
        'txtMedicineName
        '
        Me.txtMedicineName.AcceptsReturn = True
        Me.txtMedicineName.BackColor = System.Drawing.SystemColors.Window
        Me.txtMedicineName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMedicineName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMedicineName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMedicineName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMedicineName.Location = New System.Drawing.Point(140, 142)
        Me.txtMedicineName.MaxLength = 0
        Me.txtMedicineName.Name = "txtMedicineName"
        Me.txtMedicineName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMedicineName.Size = New System.Drawing.Size(449, 19)
        Me.txtMedicineName.TabIndex = 8
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVNo.Location = New System.Drawing.Point(140, 14)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(83, 19)
        Me.txtVNo.TabIndex = 1
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Enabled = False
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVDate.Location = New System.Drawing.Point(466, 14)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(121, 19)
        Me.txtVDate.TabIndex = 2
        '
        'txtEmpCode
        '
        Me.txtEmpCode.AcceptsReturn = True
        Me.txtEmpCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmpCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmpCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmpCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmpCode.Location = New System.Drawing.Point(140, 67)
        Me.txtEmpCode.MaxLength = 0
        Me.txtEmpCode.Name = "txtEmpCode"
        Me.txtEmpCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmpCode.Size = New System.Drawing.Size(87, 19)
        Me.txtEmpCode.TabIndex = 4
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(140, 168)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(449, 19)
        Me.txtRemarks.TabIndex = 9
        '
        'cboPurpose
        '
        Me.cboPurpose.BackColor = System.Drawing.SystemColors.Window
        Me.cboPurpose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPurpose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurpose.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPurpose.Location = New System.Drawing.Point(140, 40)
        Me.cboPurpose.Name = "cboPurpose"
        Me.cboPurpose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPurpose.Size = New System.Drawing.Size(173, 22)
        Me.cboPurpose.TabIndex = 3
        '
        '_Label8_9
        '
        Me._Label8_9.BackColor = System.Drawing.SystemColors.Control
        Me._Label8_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label8_9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label8_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.SetIndex(Me._Label8_9, CType(9, Short))
        Me._Label8_9.Location = New System.Drawing.Point(4, 194)
        Me._Label8_9.Name = "_Label8_9"
        Me._Label8_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label8_9.Size = New System.Drawing.Size(131, 21)
        Me._Label8_9.TabIndex = 33
        Me._Label8_9.Text = "Security Person :"
        Me._Label8_9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label7_6
        '
        Me._Label7_6.BackColor = System.Drawing.SystemColors.Control
        Me._Label7_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label7_6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label7_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.SetIndex(Me._Label7_6, CType(6, Short))
        Me._Label7_6.Location = New System.Drawing.Point(4, 117)
        Me._Label7_6.Name = "_Label7_6"
        Me._Label7_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label7_6.Size = New System.Drawing.Size(131, 13)
        Me._Label7_6.TabIndex = 32
        Me._Label7_6.Text = "Disease/Cause :"
        Me._Label7_6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.BackColor = System.Drawing.SystemColors.Control
        Me.lblFilePath.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFilePath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilePath.Location = New System.Drawing.Point(312, 138)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFilePath.Size = New System.Drawing.Size(0, 14)
        Me.lblFilePath.TabIndex = 31
        '
        '_Label9_4
        '
        Me._Label9_4.AutoSize = True
        Me._Label9_4.BackColor = System.Drawing.SystemColors.Control
        Me._Label9_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label9_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label9_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.SetIndex(Me._Label9_4, CType(4, Short))
        Me._Label9_4.Location = New System.Drawing.Point(72, 67)
        Me._Label9_4.Name = "_Label9_4"
        Me._Label9_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label9_4.Size = New System.Drawing.Size(63, 14)
        Me._Label9_4.TabIndex = 30
        Me._Label9_4.Text = "Emp Name :"
        Me._Label9_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label1_3
        '
        Me._Label1_3.BackColor = System.Drawing.SystemColors.Control
        Me._Label1_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.SetIndex(Me._Label1_3, CType(3, Short))
        Me._Label1_3.Location = New System.Drawing.Point(4, 40)
        Me._Label1_3.Name = "_Label1_3"
        Me._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_3.Size = New System.Drawing.Size(131, 13)
        Me._Label1_3.TabIndex = 29
        Me._Label1_3.Text = "Emp Type :"
        Me._Label1_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label5_1
        '
        Me._Label5_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label5_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label5_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label5_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.SetIndex(Me._Label5_1, CType(1, Short))
        Me._Label5_1.Location = New System.Drawing.Point(4, 14)
        Me._Label5_1.Name = "_Label5_1"
        Me._Label5_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label5_1.Size = New System.Drawing.Size(131, 13)
        Me._Label5_1.TabIndex = 28
        Me._Label5_1.Text = "Ref No :"
        Me._Label5_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label6_2
        '
        Me._Label6_2.AutoSize = True
        Me._Label6_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label6_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label6_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label6_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.SetIndex(Me._Label6_2, CType(2, Short))
        Me._Label6_2.Location = New System.Drawing.Point(424, 14)
        Me._Label6_2.Name = "_Label6_2"
        Me._Label6_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label6_2.Size = New System.Drawing.Size(35, 14)
        Me._Label6_2.TabIndex = 27
        Me._Label6_2.Text = "Date :"
        Me._Label6_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label7_5
        '
        Me._Label7_5.BackColor = System.Drawing.SystemColors.Control
        Me._Label7_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label7_5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label7_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.SetIndex(Me._Label7_5, CType(5, Short))
        Me._Label7_5.Location = New System.Drawing.Point(4, 92)
        Me._Label7_5.Name = "_Label7_5"
        Me._Label7_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label7_5.Size = New System.Drawing.Size(131, 13)
        Me._Label7_5.TabIndex = 26
        Me._Label7_5.Text = "Department :"
        Me._Label7_5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label8_8
        '
        Me._Label8_8.BackColor = System.Drawing.SystemColors.Control
        Me._Label8_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label8_8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label8_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.SetIndex(Me._Label8_8, CType(8, Short))
        Me._Label8_8.Location = New System.Drawing.Point(4, 168)
        Me._Label8_8.Name = "_Label8_8"
        Me._Label8_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label8_8.Size = New System.Drawing.Size(131, 13)
        Me._Label8_8.TabIndex = 25
        Me._Label8_8.Text = "Remarks :"
        Me._Label8_8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label3_7
        '
        Me._Label3_7.BackColor = System.Drawing.SystemColors.Control
        Me._Label3_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label3_7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label3_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.SetIndex(Me._Label3_7, CType(7, Short))
        Me._Label3_7.Location = New System.Drawing.Point(4, 142)
        Me._Label3_7.Name = "_Label3_7"
        Me._Label3_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label3_7.Size = New System.Drawing.Size(131, 13)
        Me._Label3_7.TabIndex = 24
        Me._Label3_7.Text = "Medicine Name :"
        Me._Label3_7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(600, 229)
        Me.SprdView.TabIndex = 19
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 224)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(601, 51)
        Me.FraMovement.TabIndex = 20
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(4, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 19
        '
        'lblBookType
        '
        Me.lblBookType.BackColor = System.Drawing.SystemColors.Control
        Me.lblBookType.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBookType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBookType.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBookType.Location = New System.Drawing.Point(684, 16)
        Me.lblBookType.Name = "lblBookType"
        Me.lblBookType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBookType.Size = New System.Drawing.Size(47, 21)
        Me.lblBookType.TabIndex = 22
        Me.lblBookType.Text = "lblBookType"
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
        Me.lblMkey.TabIndex = 21
        Me.lblMkey.Text = "lblMkey"
        '
        'frmMedicineIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(601, 276)
        Me.Controls.Add(Me.FraCustSupp)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.FraMovement)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMedicineIssue"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Medicine Issue Entry"
        Me.FraCustSupp.ResumeLayout(False)
        Me.FraCustSupp.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label9, System.ComponentModel.ISupportInitialize).EndInit()
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