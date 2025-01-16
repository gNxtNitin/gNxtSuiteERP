Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmVisitorEntry
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
    Public WithEvents Timer1 As System.Windows.Forms.Timer
    Public WithEvents chkOut As System.Windows.Forms.CheckBox
    Public WithEvents TxtOutTm As System.Windows.Forms.MaskedTextBox
    Public WithEvents txtOutTime As System.Windows.Forms.MaskedTextBox
    Public WithEvents cmdSearchMobile As System.Windows.Forms.Button
    Public WithEvents txtMobileNo As System.Windows.Forms.TextBox
    Public WithEvents cmdStop As System.Windows.Forms.Button
    Public WithEvents cmdCapture As System.Windows.Forms.Button
    Public WithEvents cmdVideoFormat As System.Windows.Forms.Button
    Public WithEvents txtEmailID As System.Windows.Forms.TextBox
    Public WithEvents cmdStart As System.Windows.Forms.Button
    Public CDialogOpen As System.Windows.Forms.OpenFileDialog
    Public CDialogSave As System.Windows.Forms.SaveFileDialog
    Public CDialogFont As System.Windows.Forms.FontDialog
    Public CDialogColor As System.Windows.Forms.ColorDialog
    Public CDialogPrint As System.Windows.Forms.PrintDialog
    Public WithEvents cboCardType As System.Windows.Forms.ComboBox
    Public WithEvents txtCardNo As System.Windows.Forms.TextBox
    Public WithEvents TxtWhomToMeet As System.Windows.Forms.TextBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtVisitorName As System.Windows.Forms.TextBox
    Public WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Public WithEvents cboPurpose As System.Windows.Forms.ComboBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents ImagePhoto As System.Windows.Forms.PictureBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblFilePath As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label11 As System.Windows.Forms.Label
    Public WithEvents FraCustSupp As System.Windows.Forms.GroupBox
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdeMailResend As System.Windows.Forms.Button
    Public WithEvents CmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents CmdSave As System.Windows.Forms.Button
    Public WithEvents CmdModify As System.Windows.Forms.Button
    Public WithEvents CmdAdd As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents lblMkey As System.Windows.Forms.Label
    Public WithEvents lblBookType As System.Windows.Forms.Label
    Public WithEvents FraMovement As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVisitorEntry))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdSearchMobile = New System.Windows.Forms.Button()
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdeMailResend = New System.Windows.Forms.Button()
        Me.CmdDelete = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.CmdSave = New System.Windows.Forms.Button()
        Me.CmdModify = New System.Windows.Forms.Button()
        Me.CmdAdd = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.FraCustSupp = New System.Windows.Forms.GroupBox()
        Me.chkOut = New System.Windows.Forms.CheckBox()
        Me.TxtOutTm = New System.Windows.Forms.MaskedTextBox()
        Me.txtOutTime = New System.Windows.Forms.MaskedTextBox()
        Me.txtMobileNo = New System.Windows.Forms.TextBox()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.cmdCapture = New System.Windows.Forms.Button()
        Me.cmdVideoFormat = New System.Windows.Forms.Button()
        Me.txtEmailID = New System.Windows.Forms.TextBox()
        Me.cmdStart = New System.Windows.Forms.Button()
        Me.cboCardType = New System.Windows.Forms.ComboBox()
        Me.txtCardNo = New System.Windows.Forms.TextBox()
        Me.TxtWhomToMeet = New System.Windows.Forms.TextBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.txtVisitorName = New System.Windows.Forms.TextBox()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.cboPurpose = New System.Windows.Forms.ComboBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.ImagePhoto = New System.Windows.Forms.PictureBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblFilePath = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.CDialogOpen = New System.Windows.Forms.OpenFileDialog()
        Me.CDialogSave = New System.Windows.Forms.SaveFileDialog()
        Me.CDialogFont = New System.Windows.Forms.FontDialog()
        Me.CDialogColor = New System.Windows.Forms.ColorDialog()
        Me.CDialogPrint = New System.Windows.Forms.PrintDialog()
        Me.FraMovement = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.lblMkey = New System.Windows.Forms.Label()
        Me.lblBookType = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.FraCustSupp.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImagePhoto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FraMovement.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdSearchMobile
        '
        Me.cmdSearchMobile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSearchMobile.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSearchMobile.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSearchMobile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSearchMobile.Image = CType(resources.GetObject("cmdSearchMobile.Image"), System.Drawing.Image)
        Me.cmdSearchMobile.Location = New System.Drawing.Point(294, 34)
        Me.cmdSearchMobile.Name = "cmdSearchMobile"
        Me.cmdSearchMobile.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSearchMobile.Size = New System.Drawing.Size(47, 19)
        Me.cmdSearchMobile.TabIndex = 44
        Me.cmdSearchMobile.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSearchMobile, "Show Record")
        Me.cmdSearchMobile.UseVisualStyleBackColor = False
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Control
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(572, 11)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(63, 37)
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
        Me.CmdView.Location = New System.Drawing.Point(510, 11)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(63, 37)
        Me.CmdView.TabIndex = 18
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
        Me.CmdPreview.Location = New System.Drawing.Point(448, 11)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(63, 37)
        Me.CmdPreview.TabIndex = 17
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
        Me.cmdPrint.Location = New System.Drawing.Point(386, 11)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(63, 37)
        Me.cmdPrint.TabIndex = 16
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdeMailResend
        '
        Me.cmdeMailResend.BackColor = System.Drawing.SystemColors.Control
        Me.cmdeMailResend.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdeMailResend.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdeMailResend.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdeMailResend.Image = CType(resources.GetObject("cmdeMailResend.Image"), System.Drawing.Image)
        Me.cmdeMailResend.Location = New System.Drawing.Point(324, 11)
        Me.cmdeMailResend.Name = "cmdeMailResend"
        Me.cmdeMailResend.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdeMailResend.Size = New System.Drawing.Size(63, 37)
        Me.cmdeMailResend.TabIndex = 40
        Me.cmdeMailResend.Text = "&eMail Resend"
        Me.cmdeMailResend.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdeMailResend, "Save and Print Record")
        Me.cmdeMailResend.UseVisualStyleBackColor = False
        '
        'CmdDelete
        '
        Me.CmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.CmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdDelete.Image = CType(resources.GetObject("CmdDelete.Image"), System.Drawing.Image)
        Me.CmdDelete.Location = New System.Drawing.Point(262, 11)
        Me.CmdDelete.Name = "CmdDelete"
        Me.CmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdDelete.Size = New System.Drawing.Size(63, 37)
        Me.CmdDelete.TabIndex = 15
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
        Me.cmdSavePrint.Location = New System.Drawing.Point(200, 11)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(63, 37)
        Me.cmdSavePrint.TabIndex = 14
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
        Me.CmdSave.Location = New System.Drawing.Point(138, 11)
        Me.CmdSave.Name = "CmdSave"
        Me.CmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdSave.Size = New System.Drawing.Size(63, 37)
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
        Me.CmdModify.Location = New System.Drawing.Point(76, 11)
        Me.CmdModify.Name = "CmdModify"
        Me.CmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdModify.Size = New System.Drawing.Size(63, 37)
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
        Me.CmdAdd.Location = New System.Drawing.Point(14, 11)
        Me.CmdAdd.Name = "CmdAdd"
        Me.CmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAdd.Size = New System.Drawing.Size(63, 37)
        Me.CmdAdd.TabIndex = 0
        Me.CmdAdd.Text = "&Add"
        Me.CmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdAdd, "Add New Record")
        Me.CmdAdd.UseVisualStyleBackColor = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'FraCustSupp
        '
        Me.FraCustSupp.BackColor = System.Drawing.SystemColors.Control
        Me.FraCustSupp.Controls.Add(Me.chkOut)
        Me.FraCustSupp.Controls.Add(Me.TxtOutTm)
        Me.FraCustSupp.Controls.Add(Me.txtOutTime)
        Me.FraCustSupp.Controls.Add(Me.cmdSearchMobile)
        Me.FraCustSupp.Controls.Add(Me.txtMobileNo)
        Me.FraCustSupp.Controls.Add(Me.cmdStop)
        Me.FraCustSupp.Controls.Add(Me.cmdCapture)
        Me.FraCustSupp.Controls.Add(Me.cmdVideoFormat)
        Me.FraCustSupp.Controls.Add(Me.txtEmailID)
        Me.FraCustSupp.Controls.Add(Me.cmdStart)
        Me.FraCustSupp.Controls.Add(Me.cboCardType)
        Me.FraCustSupp.Controls.Add(Me.txtCardNo)
        Me.FraCustSupp.Controls.Add(Me.TxtWhomToMeet)
        Me.FraCustSupp.Controls.Add(Me.txtVNo)
        Me.FraCustSupp.Controls.Add(Me.txtVDate)
        Me.FraCustSupp.Controls.Add(Me.txtVisitorName)
        Me.FraCustSupp.Controls.Add(Me.txtCompanyName)
        Me.FraCustSupp.Controls.Add(Me.cboPurpose)
        Me.FraCustSupp.Controls.Add(Me.SprdMain)
        Me.FraCustSupp.Controls.Add(Me.ImagePhoto)
        Me.FraCustSupp.Controls.Add(Me.Label10)
        Me.FraCustSupp.Controls.Add(Me.Label4)
        Me.FraCustSupp.Controls.Add(Me.lblFilePath)
        Me.FraCustSupp.Controls.Add(Me.Label9)
        Me.FraCustSupp.Controls.Add(Me.Label1)
        Me.FraCustSupp.Controls.Add(Me.Label2)
        Me.FraCustSupp.Controls.Add(Me.Label5)
        Me.FraCustSupp.Controls.Add(Me.Label6)
        Me.FraCustSupp.Controls.Add(Me.Label7)
        Me.FraCustSupp.Controls.Add(Me.Label8)
        Me.FraCustSupp.Controls.Add(Me.Label3)
        Me.FraCustSupp.Controls.Add(Me.Label11)
        Me.FraCustSupp.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraCustSupp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraCustSupp.Location = New System.Drawing.Point(0, 0)
        Me.FraCustSupp.Name = "FraCustSupp"
        Me.FraCustSupp.Padding = New System.Windows.Forms.Padding(0)
        Me.FraCustSupp.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraCustSupp.Size = New System.Drawing.Size(653, 401)
        Me.FraCustSupp.TabIndex = 23
        Me.FraCustSupp.TabStop = False
        '
        'chkOut
        '
        Me.chkOut.BackColor = System.Drawing.SystemColors.Control
        Me.chkOut.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkOut.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkOut.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkOut.Location = New System.Drawing.Point(306, 380)
        Me.chkOut.Name = "chkOut"
        Me.chkOut.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkOut.Size = New System.Drawing.Size(51, 15)
        Me.chkOut.TabIndex = 45
        Me.chkOut.Text = "Out"
        Me.chkOut.UseVisualStyleBackColor = False
        '
        'TxtOutTm
        '
        Me.TxtOutTm.AllowPromptAsInput = False
        Me.TxtOutTm.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOutTm.Location = New System.Drawing.Point(248, 378)
        Me.TxtOutTm.Mask = "##:##"
        Me.TxtOutTm.Name = "TxtOutTm"
        Me.TxtOutTm.Size = New System.Drawing.Size(45, 20)
        Me.TxtOutTm.TabIndex = 39
        '
        'txtOutTime
        '
        Me.txtOutTime.AllowPromptAsInput = False
        Me.txtOutTime.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutTime.Location = New System.Drawing.Point(158, 378)
        Me.txtOutTime.Mask = "##/##/####"
        Me.txtOutTime.Name = "txtOutTime"
        Me.txtOutTime.Size = New System.Drawing.Size(87, 20)
        Me.txtOutTime.TabIndex = 38
        '
        'txtMobileNo
        '
        Me.txtMobileNo.AcceptsReturn = True
        Me.txtMobileNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMobileNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMobileNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobileNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMobileNo.Location = New System.Drawing.Point(162, 34)
        Me.txtMobileNo.MaxLength = 0
        Me.txtMobileNo.Name = "txtMobileNo"
        Me.txtMobileNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMobileNo.Size = New System.Drawing.Size(131, 19)
        Me.txtMobileNo.TabIndex = 3
        '
        'cmdStop
        '
        Me.cmdStop.BackColor = System.Drawing.SystemColors.Control
        Me.cmdStop.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdStop.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStop.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdStop.Location = New System.Drawing.Point(588, 182)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdStop.Size = New System.Drawing.Size(61, 21)
        Me.cmdStop.TabIndex = 42
        Me.cmdStop.Text = "Stop"
        Me.cmdStop.UseVisualStyleBackColor = False
        '
        'cmdCapture
        '
        Me.cmdCapture.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCapture.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCapture.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCapture.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCapture.Location = New System.Drawing.Point(528, 182)
        Me.cmdCapture.Name = "cmdCapture"
        Me.cmdCapture.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCapture.Size = New System.Drawing.Size(61, 21)
        Me.cmdCapture.TabIndex = 33
        Me.cmdCapture.Text = "Capture"
        Me.cmdCapture.UseVisualStyleBackColor = False
        '
        'cmdVideoFormat
        '
        Me.cmdVideoFormat.BackColor = System.Drawing.SystemColors.Control
        Me.cmdVideoFormat.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdVideoFormat.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdVideoFormat.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdVideoFormat.Location = New System.Drawing.Point(382, 182)
        Me.cmdVideoFormat.Name = "cmdVideoFormat"
        Me.cmdVideoFormat.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdVideoFormat.Size = New System.Drawing.Size(85, 21)
        Me.cmdVideoFormat.TabIndex = 41
        Me.cmdVideoFormat.Text = "Video Format"
        Me.cmdVideoFormat.UseVisualStyleBackColor = False
        '
        'txtEmailID
        '
        Me.txtEmailID.AcceptsReturn = True
        Me.txtEmailID.BackColor = System.Drawing.SystemColors.Window
        Me.txtEmailID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmailID.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEmailID.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmailID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEmailID.Location = New System.Drawing.Point(162, 144)
        Me.txtEmailID.MaxLength = 0
        Me.txtEmailID.Name = "txtEmailID"
        Me.txtEmailID.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEmailID.Size = New System.Drawing.Size(319, 19)
        Me.txtEmailID.TabIndex = 36
        '
        'cmdStart
        '
        Me.cmdStart.BackColor = System.Drawing.SystemColors.Control
        Me.cmdStart.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdStart.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdStart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdStart.Location = New System.Drawing.Point(468, 182)
        Me.cmdStart.Name = "cmdStart"
        Me.cmdStart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdStart.Size = New System.Drawing.Size(61, 21)
        Me.cmdStart.TabIndex = 35
        Me.cmdStart.Text = "Start"
        Me.cmdStart.UseVisualStyleBackColor = False
        '
        'cboCardType
        '
        Me.cboCardType.BackColor = System.Drawing.SystemColors.Window
        Me.cboCardType.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCardType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCardType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCardType.Location = New System.Drawing.Point(370, 54)
        Me.cboCardType.Name = "cboCardType"
        Me.cboCardType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCardType.Size = New System.Drawing.Size(109, 22)
        Me.cboCardType.TabIndex = 5
        '
        'txtCardNo
        '
        Me.txtCardNo.AcceptsReturn = True
        Me.txtCardNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCardNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCardNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCardNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCardNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCardNo.Location = New System.Drawing.Point(162, 56)
        Me.txtCardNo.MaxLength = 0
        Me.txtCardNo.Name = "txtCardNo"
        Me.txtCardNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCardNo.Size = New System.Drawing.Size(131, 19)
        Me.txtCardNo.TabIndex = 4
        '
        'TxtWhomToMeet
        '
        Me.TxtWhomToMeet.AcceptsReturn = True
        Me.TxtWhomToMeet.BackColor = System.Drawing.SystemColors.Window
        Me.TxtWhomToMeet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TxtWhomToMeet.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtWhomToMeet.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWhomToMeet.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtWhomToMeet.Location = New System.Drawing.Point(162, 122)
        Me.TxtWhomToMeet.MaxLength = 0
        Me.TxtWhomToMeet.Name = "TxtWhomToMeet"
        Me.TxtWhomToMeet.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtWhomToMeet.Size = New System.Drawing.Size(319, 19)
        Me.TxtWhomToMeet.TabIndex = 8
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVNo.Location = New System.Drawing.Point(162, 12)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(131, 19)
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
        Me.txtVDate.Location = New System.Drawing.Point(370, 12)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(107, 19)
        Me.txtVDate.TabIndex = 2
        '
        'txtVisitorName
        '
        Me.txtVisitorName.AcceptsReturn = True
        Me.txtVisitorName.BackColor = System.Drawing.SystemColors.Window
        Me.txtVisitorName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVisitorName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVisitorName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVisitorName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVisitorName.Location = New System.Drawing.Point(162, 78)
        Me.txtVisitorName.MaxLength = 0
        Me.txtVisitorName.Name = "txtVisitorName"
        Me.txtVisitorName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVisitorName.Size = New System.Drawing.Size(319, 19)
        Me.txtVisitorName.TabIndex = 6
        '
        'txtCompanyName
        '
        Me.txtCompanyName.AcceptsReturn = True
        Me.txtCompanyName.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCompanyName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCompanyName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCompanyName.Location = New System.Drawing.Point(162, 100)
        Me.txtCompanyName.MaxLength = 0
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCompanyName.Size = New System.Drawing.Size(319, 19)
        Me.txtCompanyName.TabIndex = 7
        '
        'cboPurpose
        '
        Me.cboPurpose.BackColor = System.Drawing.SystemColors.Window
        Me.cboPurpose.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboPurpose.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPurpose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPurpose.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboPurpose.Location = New System.Drawing.Point(162, 166)
        Me.cboPurpose.Name = "cboPurpose"
        Me.cboPurpose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboPurpose.Size = New System.Drawing.Size(133, 22)
        Me.cboPurpose.TabIndex = 9
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 206)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(647, 169)
        Me.SprdMain.TabIndex = 10
        '
        'ImagePhoto
        '
        Me.ImagePhoto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.ImagePhoto.Cursor = System.Windows.Forms.Cursors.Default
        Me.ImagePhoto.Location = New System.Drawing.Point(486, 10)
        Me.ImagePhoto.Name = "ImagePhoto"
        Me.ImagePhoto.Size = New System.Drawing.Size(163, 167)
        Me.ImagePhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImagePhoto.TabIndex = 46
        Me.ImagePhoto.TabStop = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(4, 36)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(155, 13)
        Me.Label10.TabIndex = 43
        Me.Label10.Text = "Search by Mobile No :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(4, 146)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(155, 13)
        Me.Label4.TabIndex = 37
        Me.Label4.Text = "Employee's eMail Id :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.BackColor = System.Drawing.SystemColors.Control
        Me.lblFilePath.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblFilePath.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePath.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilePath.Location = New System.Drawing.Point(312, 164)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblFilePath.Size = New System.Drawing.Size(0, 14)
        Me.lblFilePath.TabIndex = 34
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(302, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(62, 14)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "Card Type :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(4, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(155, 13)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "Card No :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 124)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(155, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Whom to Meet :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(4, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(155, 13)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "Ref No :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(333, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(35, 14)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "Date :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(4, 82)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(155, 13)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "Visitor Name :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(4, 104)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(155, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Company Name && Address :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(4, 168)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(155, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Purpose :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.SystemColors.Control
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(0, 380)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(155, 13)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "Out Time :"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FraMovement
        '
        Me.FraMovement.BackColor = System.Drawing.SystemColors.Control
        Me.FraMovement.Controls.Add(Me.CmdClose)
        Me.FraMovement.Controls.Add(Me.CmdView)
        Me.FraMovement.Controls.Add(Me.CmdPreview)
        Me.FraMovement.Controls.Add(Me.cmdPrint)
        Me.FraMovement.Controls.Add(Me.cmdeMailResend)
        Me.FraMovement.Controls.Add(Me.CmdDelete)
        Me.FraMovement.Controls.Add(Me.cmdSavePrint)
        Me.FraMovement.Controls.Add(Me.CmdSave)
        Me.FraMovement.Controls.Add(Me.CmdModify)
        Me.FraMovement.Controls.Add(Me.CmdAdd)
        Me.FraMovement.Controls.Add(Me.Report1)
        Me.FraMovement.Controls.Add(Me.lblMkey)
        Me.FraMovement.Controls.Add(Me.lblBookType)
        Me.FraMovement.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FraMovement.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FraMovement.Location = New System.Drawing.Point(0, 396)
        Me.FraMovement.Name = "FraMovement"
        Me.FraMovement.Padding = New System.Windows.Forms.Padding(0)
        Me.FraMovement.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FraMovement.Size = New System.Drawing.Size(653, 51)
        Me.FraMovement.TabIndex = 11
        Me.FraMovement.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(4, 14)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 41
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
        Me.lblBookType.TabIndex = 20
        Me.lblBookType.Text = "lblBookType"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(654, 395)
        Me.SprdView.TabIndex = 22
        '
        'frmVisitorEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(654, 447)
        Me.Controls.Add(Me.FraCustSupp)
        Me.Controls.Add(Me.FraMovement)
        Me.Controls.Add(Me.SprdView)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmVisitorEntry"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Visitor Entry"
        Me.FraCustSupp.ResumeLayout(False)
        Me.FraCustSupp.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImagePhoto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FraMovement.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
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