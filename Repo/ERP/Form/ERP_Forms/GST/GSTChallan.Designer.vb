Imports Microsoft.VisualBasic.Compatibility
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class frmGSTChallan
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
        'Me.MDIParent = SalesGST.Master
        'SalesGST.Master.Show()
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
    Public WithEvents txtRemarks As System.Windows.Forms.TextBox
    Public WithEvents txtRefDate As System.Windows.Forms.TextBox
    Public WithEvents txtChallanNo As System.Windows.Forms.TextBox
    Public WithEvents txtChallanDate As System.Windows.Forms.TextBox
    Public WithEvents txtTotAmount As System.Windows.Forms.TextBox
    Public WithEvents txtRefNo As System.Windows.Forms.TextBox
    Public WithEvents txtBSRCode As System.Windows.Forms.TextBox
    Public WithEvents lblIsReverseCharge As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents LblMKey As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents fraMain As System.Windows.Forms.GroupBox
    Public WithEvents txtVNo As System.Windows.Forms.TextBox
    Public WithEvents txtVDate As System.Windows.Forms.TextBox
    Public WithEvents txtVType As System.Windows.Forms.TextBox
    Public WithEvents chkAccountPost As System.Windows.Forms.CheckBox
    Public WithEvents SprdMain As AxFPSpreadADO.AxfpSpread
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents lblBankMKey As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents SprdView As AxFPSpreadADO.AxfpSpread
    Public WithEvents CmdClose As System.Windows.Forms.Button
    Public WithEvents CmdView As System.Windows.Forms.Button
    Public WithEvents CmdPreview As System.Windows.Forms.Button
    Public WithEvents cmdPrint As System.Windows.Forms.Button
    Public WithEvents cmdDelete As System.Windows.Forms.Button
    Public WithEvents cmdAdd As System.Windows.Forms.Button
    Public WithEvents cmdModify As System.Windows.Forms.Button
    Public WithEvents cmdSave As System.Windows.Forms.Button
    Public WithEvents cmdSavePrint As System.Windows.Forms.Button
    Public WithEvents Report1 As AxCrystal.AxCrystalReport
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGSTChallan))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdClose = New System.Windows.Forms.Button()
        Me.CmdView = New System.Windows.Forms.Button()
        Me.CmdPreview = New System.Windows.Forms.Button()
        Me.cmdPrint = New System.Windows.Forms.Button()
        Me.cmdDelete = New System.Windows.Forms.Button()
        Me.cmdAdd = New System.Windows.Forms.Button()
        Me.cmdModify = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdSavePrint = New System.Windows.Forms.Button()
        Me.fraMain = New System.Windows.Forms.GroupBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtRefDate = New System.Windows.Forms.TextBox()
        Me.txtChallanNo = New System.Windows.Forms.TextBox()
        Me.txtChallanDate = New System.Windows.Forms.TextBox()
        Me.txtTotAmount = New System.Windows.Forms.TextBox()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.txtBSRCode = New System.Windows.Forms.TextBox()
        Me.lblIsReverseCharge = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LblMKey = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.txtVNo = New System.Windows.Forms.TextBox()
        Me.txtVDate = New System.Windows.Forms.TextBox()
        Me.txtVType = New System.Windows.Forms.TextBox()
        Me.chkAccountPost = New System.Windows.Forms.CheckBox()
        Me.SprdMain = New AxFPSpreadADO.AxfpSpread()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblBankMKey = New System.Windows.Forms.Label()
        Me.SprdView = New AxFPSpreadADO.AxfpSpread()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Report1 = New AxCrystal.AxCrystalReport()
        Me.fraMain.SuspendLayout()
        Me.Frame1.SuspendLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Frame3.SuspendLayout()
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdClose
        '
        Me.CmdClose.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdClose.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClose.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdClose.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClose.Image = CType(resources.GetObject("CmdClose.Image"), System.Drawing.Image)
        Me.CmdClose.Location = New System.Drawing.Point(612, 10)
        Me.CmdClose.Name = "CmdClose"
        Me.CmdClose.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClose.Size = New System.Drawing.Size(67, 37)
        Me.CmdClose.TabIndex = 18
        Me.CmdClose.Text = "&Close"
        Me.CmdClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdClose, "Close the form")
        Me.CmdClose.UseVisualStyleBackColor = False
        '
        'CmdView
        '
        Me.CmdView.BackColor = System.Drawing.SystemColors.Menu
        Me.CmdView.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdView.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdView.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdView.Image = CType(resources.GetObject("CmdView.Image"), System.Drawing.Image)
        Me.CmdView.Location = New System.Drawing.Point(546, 10)
        Me.CmdView.Name = "CmdView"
        Me.CmdView.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdView.Size = New System.Drawing.Size(67, 37)
        Me.CmdView.TabIndex = 19
        Me.CmdView.Text = "List &View"
        Me.CmdView.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.CmdView, "View Listing")
        Me.CmdView.UseVisualStyleBackColor = False
        '
        'CmdPreview
        '
        Me.CmdPreview.BackColor = System.Drawing.SystemColors.Control
        Me.CmdPreview.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdPreview.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdPreview.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdPreview.Image = CType(resources.GetObject("CmdPreview.Image"), System.Drawing.Image)
        Me.CmdPreview.Location = New System.Drawing.Point(480, 10)
        Me.CmdPreview.Name = "CmdPreview"
        Me.CmdPreview.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdPreview.Size = New System.Drawing.Size(67, 37)
        Me.CmdPreview.TabIndex = 20
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
        Me.cmdPrint.Location = New System.Drawing.Point(413, 10)
        Me.cmdPrint.Name = "cmdPrint"
        Me.cmdPrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdPrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdPrint.TabIndex = 21
        Me.cmdPrint.Text = "&Print"
        Me.cmdPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdPrint, "Print")
        Me.cmdPrint.UseVisualStyleBackColor = False
        '
        'cmdDelete
        '
        Me.cmdDelete.BackColor = System.Drawing.SystemColors.Control
        Me.cmdDelete.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdDelete.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdDelete.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdDelete.Image = CType(resources.GetObject("cmdDelete.Image"), System.Drawing.Image)
        Me.cmdDelete.Location = New System.Drawing.Point(347, 10)
        Me.cmdDelete.Name = "cmdDelete"
        Me.cmdDelete.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdDelete.Size = New System.Drawing.Size(67, 37)
        Me.cmdDelete.TabIndex = 22
        Me.cmdDelete.Text = "&Delete"
        Me.cmdDelete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdDelete, "Delete")
        Me.cmdDelete.UseVisualStyleBackColor = False
        '
        'cmdAdd
        '
        Me.cmdAdd.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdAdd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdAdd.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdAdd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAdd.Image = CType(resources.GetObject("cmdAdd.Image"), System.Drawing.Image)
        Me.cmdAdd.Location = New System.Drawing.Point(80, 10)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdAdd.Size = New System.Drawing.Size(67, 37)
        Me.cmdAdd.TabIndex = 26
        Me.cmdAdd.Text = "&Add"
        Me.cmdAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdAdd, "Add New")
        Me.cmdAdd.UseVisualStyleBackColor = False
        '
        'cmdModify
        '
        Me.cmdModify.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdModify.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdModify.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdModify.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdModify.Image = CType(resources.GetObject("cmdModify.Image"), System.Drawing.Image)
        Me.cmdModify.Location = New System.Drawing.Point(147, 10)
        Me.cmdModify.Name = "cmdModify"
        Me.cmdModify.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdModify.Size = New System.Drawing.Size(67, 37)
        Me.cmdModify.TabIndex = 25
        Me.cmdModify.Text = "&Modify"
        Me.cmdModify.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdModify, "Modify ")
        Me.cmdModify.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Menu
        Me.cmdSave.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSave.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Image = CType(resources.GetObject("cmdSave.Image"), System.Drawing.Image)
        Me.cmdSave.Location = New System.Drawing.Point(214, 10)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSave.Size = New System.Drawing.Size(67, 37)
        Me.cmdSave.TabIndex = 24
        Me.cmdSave.Text = "&Save"
        Me.cmdSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSave, "Save Current Record")
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdSavePrint
        '
        Me.cmdSavePrint.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSavePrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdSavePrint.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSavePrint.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSavePrint.Image = CType(resources.GetObject("cmdSavePrint.Image"), System.Drawing.Image)
        Me.cmdSavePrint.Location = New System.Drawing.Point(281, 10)
        Me.cmdSavePrint.Name = "cmdSavePrint"
        Me.cmdSavePrint.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdSavePrint.Size = New System.Drawing.Size(67, 37)
        Me.cmdSavePrint.TabIndex = 23
        Me.cmdSavePrint.Text = "Save&&Print"
        Me.cmdSavePrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.cmdSavePrint, "Save & Print")
        Me.cmdSavePrint.UseVisualStyleBackColor = False
        '
        'fraMain
        '
        Me.fraMain.BackColor = System.Drawing.SystemColors.Control
        Me.fraMain.Controls.Add(Me.txtRemarks)
        Me.fraMain.Controls.Add(Me.txtRefDate)
        Me.fraMain.Controls.Add(Me.txtChallanNo)
        Me.fraMain.Controls.Add(Me.txtChallanDate)
        Me.fraMain.Controls.Add(Me.txtTotAmount)
        Me.fraMain.Controls.Add(Me.txtRefNo)
        Me.fraMain.Controls.Add(Me.txtBSRCode)
        Me.fraMain.Controls.Add(Me.lblIsReverseCharge)
        Me.fraMain.Controls.Add(Me.Label8)
        Me.fraMain.Controls.Add(Me.Label9)
        Me.fraMain.Controls.Add(Me.LblMKey)
        Me.fraMain.Controls.Add(Me.Label2)
        Me.fraMain.Controls.Add(Me.Label7)
        Me.fraMain.Controls.Add(Me.Label6)
        Me.fraMain.Controls.Add(Me.Label3)
        Me.fraMain.Controls.Add(Me.Label1)
        Me.fraMain.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraMain.ForeColor = System.Drawing.SystemColors.ControlText
        Me.fraMain.Location = New System.Drawing.Point(0, -4)
        Me.fraMain.Name = "fraMain"
        Me.fraMain.Padding = New System.Windows.Forms.Padding(0)
        Me.fraMain.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.fraMain.Size = New System.Drawing.Size(749, 107)
        Me.fraMain.TabIndex = 0
        Me.fraMain.TabStop = False
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsReturn = True
        Me.txtRemarks.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRemarks.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRemarks.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRemarks.Location = New System.Drawing.Point(163, 82)
        Me.txtRemarks.MaxLength = 0
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRemarks.Size = New System.Drawing.Size(471, 20)
        Me.txtRemarks.TabIndex = 7
        '
        'txtRefDate
        '
        Me.txtRefDate.AcceptsReturn = True
        Me.txtRefDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefDate.Location = New System.Drawing.Point(539, 16)
        Me.txtRefDate.MaxLength = 0
        Me.txtRefDate.Name = "txtRefDate"
        Me.txtRefDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefDate.Size = New System.Drawing.Size(95, 20)
        Me.txtRefDate.TabIndex = 2
        '
        'txtChallanNo
        '
        Me.txtChallanNo.AcceptsReturn = True
        Me.txtChallanNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChallanNo.Location = New System.Drawing.Point(163, 38)
        Me.txtChallanNo.MaxLength = 0
        Me.txtChallanNo.Name = "txtChallanNo"
        Me.txtChallanNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanNo.Size = New System.Drawing.Size(177, 20)
        Me.txtChallanNo.TabIndex = 3
        '
        'txtChallanDate
        '
        Me.txtChallanDate.AcceptsReturn = True
        Me.txtChallanDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtChallanDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtChallanDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtChallanDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtChallanDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtChallanDate.Location = New System.Drawing.Point(539, 38)
        Me.txtChallanDate.MaxLength = 0
        Me.txtChallanDate.Name = "txtChallanDate"
        Me.txtChallanDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtChallanDate.Size = New System.Drawing.Size(95, 20)
        Me.txtChallanDate.TabIndex = 4
        '
        'txtTotAmount
        '
        Me.txtTotAmount.AcceptsReturn = True
        Me.txtTotAmount.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotAmount.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotAmount.Enabled = False
        Me.txtTotAmount.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotAmount.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotAmount.Location = New System.Drawing.Point(539, 60)
        Me.txtTotAmount.MaxLength = 0
        Me.txtTotAmount.Name = "txtTotAmount"
        Me.txtTotAmount.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotAmount.Size = New System.Drawing.Size(95, 20)
        Me.txtTotAmount.TabIndex = 6
        '
        'txtRefNo
        '
        Me.txtRefNo.AcceptsReturn = True
        Me.txtRefNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRefNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRefNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRefNo.Enabled = False
        Me.txtRefNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRefNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRefNo.Location = New System.Drawing.Point(163, 16)
        Me.txtRefNo.MaxLength = 0
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRefNo.Size = New System.Drawing.Size(83, 20)
        Me.txtRefNo.TabIndex = 1
        '
        'txtBSRCode
        '
        Me.txtBSRCode.AcceptsReturn = True
        Me.txtBSRCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtBSRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBSRCode.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtBSRCode.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBSRCode.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBSRCode.Location = New System.Drawing.Point(163, 60)
        Me.txtBSRCode.MaxLength = 0
        Me.txtBSRCode.Name = "txtBSRCode"
        Me.txtBSRCode.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtBSRCode.Size = New System.Drawing.Size(177, 20)
        Me.txtBSRCode.TabIndex = 5
        '
        'lblIsReverseCharge
        '
        Me.lblIsReverseCharge.BackColor = System.Drawing.SystemColors.Control
        Me.lblIsReverseCharge.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblIsReverseCharge.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblIsReverseCharge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIsReverseCharge.Location = New System.Drawing.Point(670, 26)
        Me.lblIsReverseCharge.Name = "lblIsReverseCharge"
        Me.lblIsReverseCharge.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblIsReverseCharge.Size = New System.Drawing.Size(47, 21)
        Me.lblIsReverseCharge.TabIndex = 36
        Me.lblIsReverseCharge.Text = "lblIsReverseCharge"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(6, 82)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(153, 13)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Remarks :"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(6, 62)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(153, 13)
        Me.Label9.TabIndex = 31
        Me.Label9.Text = "BSR / BRN Code :"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblMKey
        '
        Me.LblMKey.AutoSize = True
        Me.LblMKey.BackColor = System.Drawing.SystemColors.Control
        Me.LblMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblMKey.Location = New System.Drawing.Point(356, 16)
        Me.LblMKey.Name = "LblMKey"
        Me.LblMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblMKey.Size = New System.Drawing.Size(48, 14)
        Me.LblMKey.TabIndex = 29
        Me.LblMKey.Text = "LblMKey"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(479, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(55, 14)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Ref Date :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(95, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(64, 14)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Challan No :"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(461, 40)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(73, 14)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Challan Date :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(437, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(97, 14)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Total Paid Amount :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(110, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(49, 14)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Ref No. :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.txtVNo)
        Me.Frame1.Controls.Add(Me.txtVDate)
        Me.Frame1.Controls.Add(Me.txtVType)
        Me.Frame1.Controls.Add(Me.chkAccountPost)
        Me.Frame1.Controls.Add(Me.SprdMain)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.lblBankMKey)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(0, 98)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(749, 289)
        Me.Frame1.TabIndex = 30
        Me.Frame1.TabStop = False
        '
        'txtVNo
        '
        Me.txtVNo.AcceptsReturn = True
        Me.txtVNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtVNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVNo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVNo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVNo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVNo.Location = New System.Drawing.Point(264, 264)
        Me.txtVNo.MaxLength = 0
        Me.txtVNo.Name = "txtVNo"
        Me.txtVNo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVNo.Size = New System.Drawing.Size(79, 20)
        Me.txtVNo.TabIndex = 11
        '
        'txtVDate
        '
        Me.txtVDate.AcceptsReturn = True
        Me.txtVDate.BackColor = System.Drawing.SystemColors.Window
        Me.txtVDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVDate.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVDate.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVDate.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVDate.Location = New System.Drawing.Point(404, 264)
        Me.txtVDate.MaxLength = 0
        Me.txtVDate.Name = "txtVDate"
        Me.txtVDate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVDate.Size = New System.Drawing.Size(75, 20)
        Me.txtVDate.TabIndex = 12
        '
        'txtVType
        '
        Me.txtVType.AcceptsReturn = True
        Me.txtVType.BackColor = System.Drawing.SystemColors.Window
        Me.txtVType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVType.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVType.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtVType.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVType.Location = New System.Drawing.Point(214, 264)
        Me.txtVType.MaxLength = 0
        Me.txtVType.Name = "txtVType"
        Me.txtVType.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVType.Size = New System.Drawing.Size(49, 20)
        Me.txtVType.TabIndex = 10
        '
        'chkAccountPost
        '
        Me.chkAccountPost.AutoSize = True
        Me.chkAccountPost.BackColor = System.Drawing.SystemColors.Control
        Me.chkAccountPost.Cursor = System.Windows.Forms.Cursors.Default
        Me.chkAccountPost.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAccountPost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.chkAccountPost.Location = New System.Drawing.Point(14, 268)
        Me.chkAccountPost.Name = "chkAccountPost"
        Me.chkAccountPost.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkAccountPost.Size = New System.Drawing.Size(91, 18)
        Me.chkAccountPost.TabIndex = 9
        Me.chkAccountPost.Text = "Account Post"
        Me.chkAccountPost.UseVisualStyleBackColor = False
        '
        'SprdMain
        '
        Me.SprdMain.DataSource = Nothing
        Me.SprdMain.Location = New System.Drawing.Point(2, 8)
        Me.SprdMain.Name = "SprdMain"
        Me.SprdMain.OcxState = CType(resources.GetObject("SprdMain.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdMain.Size = New System.Drawing.Size(745, 253)
        Me.SprdMain.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(354, 266)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(46, 14)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "V Date :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(156, 268)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(34, 14)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "VNo :"
        '
        'lblBankMKey
        '
        Me.lblBankMKey.AutoSize = True
        Me.lblBankMKey.BackColor = System.Drawing.SystemColors.Control
        Me.lblBankMKey.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBankMKey.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBankMKey.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBankMKey.Location = New System.Drawing.Point(582, 266)
        Me.lblBankMKey.Name = "lblBankMKey"
        Me.lblBankMKey.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBankMKey.Size = New System.Drawing.Size(68, 14)
        Me.lblBankMKey.TabIndex = 32
        Me.lblBankMKey.Text = "lblBankMKey"
        '
        'SprdView
        '
        Me.SprdView.DataSource = Nothing
        Me.SprdView.Location = New System.Drawing.Point(0, 0)
        Me.SprdView.Name = "SprdView"
        Me.SprdView.OcxState = CType(resources.GetObject("SprdView.OcxState"), System.Windows.Forms.AxHost.State)
        Me.SprdView.Size = New System.Drawing.Size(747, 387)
        Me.SprdView.TabIndex = 27
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.CmdClose)
        Me.Frame3.Controls.Add(Me.CmdView)
        Me.Frame3.Controls.Add(Me.CmdPreview)
        Me.Frame3.Controls.Add(Me.cmdPrint)
        Me.Frame3.Controls.Add(Me.cmdDelete)
        Me.Frame3.Controls.Add(Me.cmdAdd)
        Me.Frame3.Controls.Add(Me.cmdModify)
        Me.Frame3.Controls.Add(Me.cmdSave)
        Me.Frame3.Controls.Add(Me.cmdSavePrint)
        Me.Frame3.Controls.Add(Me.Report1)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(0, 382)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.Padding = New System.Windows.Forms.Padding(0)
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(751, 51)
        Me.Frame3.TabIndex = 17
        Me.Frame3.TabStop = False
        '
        'Report1
        '
        Me.Report1.Enabled = True
        Me.Report1.Location = New System.Drawing.Point(14, 12)
        Me.Report1.Name = "Report1"
        Me.Report1.OcxState = CType(resources.GetObject("Report1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.Report1.Size = New System.Drawing.Size(28, 28)
        Me.Report1.TabIndex = 27
        '
        'frmGSTChallan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(751, 434)
        Me.Controls.Add(Me.fraMain)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.SprdView)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(21, 91)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGSTChallan"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Payment of Tax (GST Challan)"
        Me.fraMain.ResumeLayout(False)
        Me.fraMain.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        CType(Me.SprdMain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SprdView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Frame3.ResumeLayout(False)
        CType(Me.Report1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support"
    Public Sub VB6_AddADODataBinding()
        ''SprdView.DataSource = CType(AdoDCMain, MSDATASRC.DataSource)
    End Sub
    Public Sub VB6_RemoveADODataBinding()
        SprdView.DataSource = Nothing
    End Sub
#End Region
End Class