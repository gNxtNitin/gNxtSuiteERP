Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmTaxiINOUT
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
    Public WithEvents chkAutoInTime As System.Windows.Forms.CheckBox
    Public WithEvents chkAutoOutTime As System.Windows.Forms.CheckBox
    Public WithEvents txtOPReading As System.Windows.Forms.TextBox
    Public WithEvents txtCLReading As System.Windows.Forms.TextBox
    Public WithEvents txtRunningKM As System.Windows.Forms.TextBox
    Public WithEvents txtInTime As System.Windows.Forms.TextBox
    Public WithEvents txtInDate As System.Windows.Forms.TextBox
    Public WithEvents txtOutTime As System.Windows.Forms.TextBox
    Public WithEvents txtAname As System.Windows.Forms.TextBox
    Public WithEvents txtusername As System.Windows.Forms.TextBox
    Public WithEvents txtTaxiNo As System.Windows.Forms.TextBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtDrivername As System.Windows.Forms.TextBox
    Public WithEvents txtOutDate As System.Windows.Forms.TextBox
    Public WithEvents cboPurpose As System.Windows.Forms.ComboBox
    Public WithEvents _Label8_4 As System.Windows.Forms.Label
    Public WithEvents _Label8_3 As System.Windows.Forms.Label
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents _Label8_2 As System.Windows.Forms.Label
    Public WithEvents _Label8_1 As System.Windows.Forms.Label
    Public WithEvents _Label7_1 As System.Windows.Forms.Label
    Public WithEvents lblFilePath As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents _Label6_0 As System.Windows.Forms.Label
    Public WithEvents _Label7_0 As System.Windows.Forms.Label
    Public WithEvents _Label8_0 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
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
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents Label6 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents Label7 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    Public WithEvents Label8 As Microsoft.VisualBasic.Compatibility.VB6.LabelArray
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTaxiINOUT))
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
        Me.chkAutoInTime = New System.Windows.Forms.CheckBox()
        Me.chkAutoOutTime = New System.Windows.Forms.CheckBox()
        Me.txtOPReading = New System.Windows.Forms.TextBox()
        Me.txtCLReading = New System.Windows.Forms.TextBox()
        Me.txtRunningKM = New System.Windows.Forms.TextBox()
        Me.txtInTime = New System.Windows.Forms.TextBox()
        Me.txtInDate = New System.Windows.Forms.TextBox()
        Me.txtOutTime = New System.Windows.Forms.TextBox()
        Me.txtAname = New System.Windows.Forms.TextBox()
        Me.txtusername = New System.Windows.Forms.TextBox()
        Me.txtTaxiNo = New System.Windows.Forms.TextBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.txtDrivername = New System.Windows.Forms.TextBox()
        Me.txtOutDate = New System.Windows.Forms.TextBox()
        Me.cboPurpose = New System.Windows.Forms.ComboBox()
        Me._Label8_4 = New System.Windows.Forms.Label()
        Me._Label8_3 = New System.Windows.Forms.Label()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me._Label8_2 = New System.Windows.Forms.Label()
        Me._Label8_1 = New System.Windows.Forms.Label()
        Me._Label7_1 = New System.Windows.Forms.Label()
        Me.lblFilePath = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me._Label6_0 = New System.Windows.Forms.Label()
        Me._Label7_0 = New System.Windows.Forms.Label()
        Me._Label8_0 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.Label6 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label7 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.Label8 = New Microsoft.VisualBasic.Compatibility.VB6.LabelArray(Me.components)
        Me.FraCustSupp.SuspendLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(556, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(63, 37)
        Me.CmdClose.TabIndex = 29
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
        Me.CmdView.Location = New System.Drawing.Point(494, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(63, 37)
        Me.CmdView.TabIndex = 30
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
        Me.CmdPreview.Location = New System.Drawing.Point(432, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(63, 37)
        Me.CmdPreview.TabIndex = 31
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
        Me.cmdPrint.Location = New System.Drawing.Point(370, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(63, 37)
        Me.cmdPrint.TabIndex = 32
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
        Me.CmdDelete.Location = New System.Drawing.Point(308, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(63, 37)
        Me.CmdDelete.TabIndex = 33
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(246, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(63, 37)
        Me.cmdSavePrint.TabIndex = 34
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
        Me.CmdSave.Location = New System.Drawing.Point(184, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(63, 37)
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
        Me.CmdModify.Location = New System.Drawing.Point(122, 12)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(63, 37)
        Me.CmdModify.TabIndex = 36
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
        Me.CmdAdd.Location = New System.Drawing.Point(60, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(63, 37)
        Me.CmdAdd.TabIndex = 37
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'FraCustSupp
        '
        Me.FraCustSupp.BackColor = System.Drawing.SystemColors.Control
        Me.FraCustSupp.Controls.Add(Me.chkAutoInTime)
        Me.FraCustSupp.Controls.Add(Me.chkAutoOutTime)
        Me.FraCustSupp.Controls.Add(Me.txtOPReading)
        Me.FraCustSupp.Controls.Add(Me.txtCLReading)
        Me.FraCustSupp.Controls.Add(Me.txtRunningKM)
        Me.FraCustSupp.Controls.Add(Me.txtInTime)
        Me.FraCustSupp.Controls.Add(Me.txtInDate)
        Me.FraCustSupp.Controls.Add(Me.txtOutTime)
        Me.FraCustSupp.Controls.Add(Me.txtAname)
        Me.FraCustSupp.Controls.Add(Me.txtusername)
        Me.FraCustSupp.Controls.Add(Me.txtTaxiNo)
        Me.FraCustSupp.Controls.Add(Me.txtVNo)
        Me.FraCustSupp.Controls.Add(Me.txtVDate)
        Me.FraCustSupp.Controls.Add(Me.txtDrivername)
        Me.FraCustSupp.Controls.Add(Me.txtOutDate)
        Me.FraCustSupp.Controls.Add(Me.cboPurpose)
        Me.FraCustSupp.Controls.Add(Me._Label8_4)
        Me.FraCustSupp.Controls.Add(Me._Label8_3)
        Me.FraCustSupp.Controls.Add(Me.lblMkey)
        Me.FraCustSupp.Controls.Add(Me._Label8_2)
        Me.FraCustSupp.Controls.Add(Me._Label8_1)
        Me.FraCustSupp.Controls.Add(Me._Label7_1)
        Me.FraCustSupp.Controls.Add(Me.lblFilePath)
        Me.FraCustSupp.Controls.Add(Me.Label9)
        Me.FraCustSupp.Controls.Add(Me.Label1)
        Me.FraCustSupp.Controls.Add(Me.Label5)
        Me.FraCustSupp.Controls.Add(Me._Label6_0)
        Me.FraCustSupp.Controls.Add(Me._Label7_0)
        Me.FraCustSupp.Controls.Add(Me._Label8_0)
        Me.FraCustSupp.Controls.Add(Me.Label3)
        Me.FraCustSupp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCustSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraCustSupp.Location = New System.Drawing.Point(-2, -2)
        Me.FraCustSupp.Name = "FraCustSupp"
        Me.FraCustSupp.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCustSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCustSupp.Size = New System.Drawing.Size(655, 403)
        Me.FraCustSupp.TabIndex = 20
        Me.FraCustSupp.TabStop = False
        '
        'chkAutoInTime
        '
        Me.chkAutoInTime.AutoSize = True
        Me.chkAutoInTime.BackColor = System.Drawing.SystemColors.Control
        Me.chkAutoInTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoInTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoInTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoInTime.Location = New System.Drawing.Point(298, 328)
        Me.chkAutoInTime.Name = "chkAutoInTime"
        Me.chkAutoInTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoInTime.Size = New System.Drawing.Size(120, 18)
        Me.chkAutoInTime.TabIndex = 42
        Me.chkAutoInTime.Text = "Auto  IN Date / Time"
        Me.chkAutoInTime.UseVisualStyleBackColor = False
        '
        'chkAutoOutTime
        '
        Me.chkAutoOutTime.AutoSize = True
        Me.chkAutoOutTime.BackColor = System.Drawing.SystemColors.Control
        Me.chkAutoOutTime.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAutoOutTime.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoOutTime.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAutoOutTime.Location = New System.Drawing.Point(298, 294)
        Me.chkAutoOutTime.Name = "chkAutoOutTime"
        Me.chkAutoOutTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAutoOutTime.Size = New System.Drawing.Size(132, 18)
        Me.chkAutoOutTime.TabIndex = 41
        Me.chkAutoOutTime.Text = "Auto  OUT Date / Time"
        Me.chkAutoOutTime.UseVisualStyleBackColor = False
        '
        'txtOPReading
        '
        Me.txtOPReading.AcceptsReturn = True
        Me.txtOPReading.BackColor = System.Drawing.SystemColors.Window
        Me.txtOPReading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOPReading.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOPReading.Enabled = False
        Me.txtOPReading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOPReading.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOPReading.Location = New System.Drawing.Point(138, 228)
        Me.txtOPReading.MaxLength = 0
        Me.txtOPReading.Name = "txtOPReading"
        Me.txtOPReading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOPReading.Size = New System.Drawing.Size(119, 19)
        Me.txtOPReading.TabIndex = 16
        '
        'txtCLReading
        '
        Me.txtCLReading.AcceptsReturn = True
        Me.txtCLReading.BackColor = System.Drawing.SystemColors.Window
        Me.txtCLReading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCLReading.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCLReading.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCLReading.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCLReading.Location = New System.Drawing.Point(138, 260)
        Me.txtCLReading.MaxLength = 0
        Me.txtCLReading.Name = "txtCLReading"
        Me.txtCLReading.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCLReading.Size = New System.Drawing.Size(119, 19)
        Me.txtCLReading.TabIndex = 17
        '
        'txtRunningKM
        '
        Me.txtRunningKM.AcceptsReturn = True
        Me.txtRunningKM.BackColor = System.Drawing.SystemColors.Window
        Me.txtRunningKM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRunningKM.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRunningKM.Enabled = False
        Me.txtRunningKM.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRunningKM.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRunningKM.Location = New System.Drawing.Point(138, 357)
        Me.txtRunningKM.MaxLength = 0
        Me.txtRunningKM.Name = "txtRunningKM"
        Me.txtRunningKM.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRunningKM.Size = New System.Drawing.Size(119, 19)
        Me.txtRunningKM.TabIndex = 27
        '
        'txtInTime
        '
        Me.txtInTime.AcceptsReturn = True
        Me.txtInTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtInTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInTime.Location = New System.Drawing.Point(258, 324)
        Me.txtInTime.MaxLength = 0
        Me.txtInTime.Name = "txtInTime"
        Me.txtInTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInTime.Size = New System.Drawing.Size(35, 19)
        Me.txtInTime.TabIndex = 25
        '
        'txtInDate
        '
        Me.txtInDate.AcceptsReturn = True
        Me.txtInDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtInDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInDate.Location = New System.Drawing.Point(138, 324)
        Me.txtInDate.MaxLength = 0
        Me.txtInDate.Name = "txtInDate"
        Me.txtInDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInDate.Size = New System.Drawing.Size(119, 19)
        Me.txtInDate.TabIndex = 23
        '
        'txtOutTime
        '
        Me.txtOutTime.AcceptsReturn = True
        Me.txtOutTime.BackColor = System.Drawing.SystemColors.Window
        Me.txtOutTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutTime.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOutTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutTime.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOutTime.Location = New System.Drawing.Point(258, 292)
        Me.txtOutTime.MaxLength = 0
        Me.txtOutTime.Name = "txtOutTime"
        Me.txtOutTime.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOutTime.Size = New System.Drawing.Size(35, 19)
        Me.txtOutTime.TabIndex = 21
        '
        'txtAname
        '
        Me.txtAname.AcceptsReturn = True
        Me.txtAname.BackColor = System.Drawing.SystemColors.Window
        Me.txtAname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAname.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtAname.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAname.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtAname.Location = New System.Drawing.Point(138, 160)
        Me.txtAname.MaxLength = 0
        Me.txtAname.Name = "txtAname"
        Me.txtAname.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtAname.Size = New System.Drawing.Size(455, 19)
        Me.txtAname.TabIndex = 14
        '
        'txtusername
        '
        Me.txtusername.AcceptsReturn = True
        Me.txtusername.BackColor = System.Drawing.SystemColors.Window
        Me.txtusername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtusername.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtusername.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtusername.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtusername.Location = New System.Drawing.Point(138, 127)
        Me.txtusername.MaxLength = 0
        Me.txtusername.Name = "txtusername"
        Me.txtusername.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtusername.Size = New System.Drawing.Size(455, 19)
        Me.txtusername.TabIndex = 13
        '
        'txtTaxiNo
        '
        Me.txtTaxiNo.AcceptsReturn = True
        Me.txtTaxiNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTaxiNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTaxiNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTaxiNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTaxiNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTaxiNo.Location = New System.Drawing.Point(138, 60)
        Me.txtTaxiNo.MaxLength = 0
        Me.txtTaxiNo.Name = "txtTaxiNo"
        Me.txtTaxiNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTaxiNo.Size = New System.Drawing.Size(179, 19)
        Me.txtTaxiNo.TabIndex = 11
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVNo.Location = New System.Drawing.Point(138, 28)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(83, 19)
        Me.txtVNo.TabIndex = 7
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
        Me.txtVDate.Location = New System.Drawing.Point(448, 28)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(137, 19)
        Me.txtVDate.TabIndex = 10
        '
        'txtDrivername
        '
        Me.txtDrivername.AcceptsReturn = True
        Me.txtDrivername.BackColor = System.Drawing.SystemColors.Window
        Me.txtDrivername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDrivername.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDrivername.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDrivername.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDrivername.Location = New System.Drawing.Point(138, 92)
        Me.txtDrivername.MaxLength = 0
        Me.txtDrivername.Name = "txtDrivername"
        Me.txtDrivername.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDrivername.Size = New System.Drawing.Size(455, 19)
        Me.txtDrivername.TabIndex = 12
        '
        'txtOutDate
        '
        Me.txtOutDate.AcceptsReturn = True
        Me.txtOutDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtOutDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOutDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtOutDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtOutDate.Location = New System.Drawing.Point(138, 292)
        Me.txtOutDate.MaxLength = 0
        Me.txtOutDate.Name = "txtOutDate"
        Me.txtOutDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtOutDate.Size = New System.Drawing.Size(119, 19)
        Me.txtOutDate.TabIndex = 19
        '
        'cboPurpose
        '
        Me.cboPurpose.BackColor = System.Drawing.SystemColors.Window
        Me.cboPurpose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPurpose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurpose.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPurpose.Location = New System.Drawing.Point(138, 194)
        Me.cboPurpose.Name = "cboPurpose"
        Me.cboPurpose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPurpose.Size = New System.Drawing.Size(173, 22)
        Me.cboPurpose.TabIndex = 15
        '
        '_Label8_4
        '
        Me._Label8_4.BackColor = System.Drawing.SystemColors.Control
        Me._Label8_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label8_4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label8_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.SetIndex(Me._Label8_4, CType(4, Short))
        Me._Label8_4.Location = New System.Drawing.Point(2, 228)
        Me._Label8_4.Name = "_Label8_4"
        Me._Label8_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label8_4.Size = New System.Drawing.Size(123, 13)
        Me._Label8_4.TabIndex = 40
        Me._Label8_4.Text = "Opening Reading :"
        Me._Label8_4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label8_3
        '
        Me._Label8_3.BackColor = System.Drawing.SystemColors.Control
        Me._Label8_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label8_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label8_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.SetIndex(Me._Label8_3, CType(3, Short))
        Me._Label8_3.Location = New System.Drawing.Point(2, 260)
        Me._Label8_3.Name = "_Label8_3"
        Me._Label8_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label8_3.Size = New System.Drawing.Size(123, 21)
        Me._Label8_3.TabIndex = 39
        Me._Label8_3.Text = "Closing Reading :"
        Me._Label8_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblMkey
        '
        Me.lblMkey.BackColor = System.Drawing.SystemColors.Control
        Me.lblMkey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblMkey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMkey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblMkey.Location = New System.Drawing.Point(396, 182)
        Me.lblMkey.Name = "lblMkey"
        Me.lblMkey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblMkey.Size = New System.Drawing.Size(131, 17)
        Me.lblMkey.TabIndex = 26
        Me.lblMkey.Text = "lblMkey"
        Me.lblMkey.Visible = False
        '
        '_Label8_2
        '
        Me._Label8_2.BackColor = System.Drawing.SystemColors.Control
        Me._Label8_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label8_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label8_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.SetIndex(Me._Label8_2, CType(2, Short))
        Me._Label8_2.Location = New System.Drawing.Point(4, 359)
        Me._Label8_2.Name = "_Label8_2"
        Me._Label8_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label8_2.Size = New System.Drawing.Size(123, 13)
        Me._Label8_2.TabIndex = 9
        Me._Label8_2.Text = "Running KM :"
        Me._Label8_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label8_1
        '
        Me._Label8_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label8_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label8_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label8_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.SetIndex(Me._Label8_1, CType(1, Short))
        Me._Label8_1.Location = New System.Drawing.Point(4, 324)
        Me._Label8_1.Name = "_Label8_1"
        Me._Label8_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label8_1.Size = New System.Drawing.Size(123, 21)
        Me._Label8_1.TabIndex = 8
        Me._Label8_1.Text = "In Date &&  Time :"
        Me._Label8_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label7_1
        '
        Me._Label7_1.BackColor = System.Drawing.SystemColors.Control
        Me._Label7_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label7_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label7_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.SetIndex(Me._Label7_1, CType(1, Short))
        Me._Label7_1.Location = New System.Drawing.Point(4, 160)
        Me._Label7_1.Name = "_Label7_1"
        Me._Label7_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label7_1.Size = New System.Drawing.Size(123, 13)
        Me._Label7_1.TabIndex = 5
        Me._Label7_1.Text = "Approved By :"
        Me._Label7_1.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.lblFilePath.TabIndex = 22
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(4, 92)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(72, 14)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Driver Name :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(131, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Taxi No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(131, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Ref No :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label6_0
        '
        Me._Label6_0.AutoSize = True
        Me._Label6_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label6_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label6_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label6_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.SetIndex(Me._Label6_0, CType(0, Short))
        Me._Label6_0.Location = New System.Drawing.Point(406, 28)
        Me._Label6_0.Name = "_Label6_0"
        Me._Label6_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label6_0.Size = New System.Drawing.Size(35, 14)
        Me._Label6_0.TabIndex = 1
        Me._Label6_0.Text = "Date :"
        Me._Label6_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label7_0
        '
        Me._Label7_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label7_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label7_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label7_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.SetIndex(Me._Label7_0, CType(0, Short))
        Me._Label7_0.Location = New System.Drawing.Point(4, 127)
        Me._Label7_0.Name = "_Label7_0"
        Me._Label7_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label7_0.Size = New System.Drawing.Size(123, 13)
        Me._Label7_0.TabIndex = 4
        Me._Label7_0.Text = "Use By :"
        Me._Label7_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label8_0
        '
        Me._Label8_0.BackColor = System.Drawing.SystemColors.Control
        Me._Label8_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label8_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label8_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.SetIndex(Me._Label8_0, CType(0, Short))
        Me._Label8_0.Location = New System.Drawing.Point(4, 292)
        Me._Label8_0.Name = "_Label8_0"
        Me._Label8_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label8_0.Size = New System.Drawing.Size(123, 13)
        Me._Label8_0.TabIndex = 24
        Me._Label8_0.Text = "Out Date && Time :"
        Me._Label8_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 194)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(123, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Purpose :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 2)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(654, 395)
        Me.SprdView.TabIndex = 18
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
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 396)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(653, 51)
        Me.FraMovement.TabIndex = 28
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(4, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 38
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
        Me.lblBookType.TabIndex = 38
        Me.lblBookType.Text = "lblBookType"
        '
        'frmTaxiINOUT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(653, 445)
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
        Me.Name = "frmTaxiINOUT"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Company Taxi In & Out"
        Me.FraCustSupp.ResumeLayout(False)
        Me.FraCustSupp.PerformLayout()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Label8, System.ComponentModel.ISupportInitialize).EndInit()
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